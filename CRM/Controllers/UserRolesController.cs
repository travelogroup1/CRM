using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CRM.Areas.Identity.Data;
using CRM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRM.Controllers
{
    public class UserRolesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;


        public UserRolesController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        //Logout
        public async Task<IActionResult> Logout()
        {
            var returnUrl = Url.Action("Index","Home");
            await _signInManager.SignOutAsync();
                return LocalRedirect(returnUrl);
        }
        //Edit USer
        public  ActionResult EditUser()
        {
            // Get Current Userid
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var users =  _userManager.Users.Where(x=>x.Id == userId).FirstOrDefault();
            ViewData["Title"] = "Edit Profile";
            return View(users); 
        }
        [HttpPost]
        public async Task<ActionResult> EditUserAsync(ApplicationUser applicationUser)
        {
            // Get Current Userid
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var users = _userManager.Users.Where(x => x.Id == userId).FirstOrDefault();
            if (Request.Form.Files.Count > 0)
            {
                IFormFile file = Request.Form.Files.FirstOrDefault();
                using (var dataStream = new MemoryStream())
                {
                    await file.CopyToAsync(dataStream);
                    applicationUser.ProfilePicture = dataStream.ToArray();
                }
            }
            else
            {
                var img_path = "~/Admin Template/dist/img/avatar.png";
                using (Image image = Image.FromFile(img_path))
                {
                    using (MemoryStream m = new MemoryStream())
                    {
                        image.Save(m, image.RawFormat);
                        byte[] imageBytes = m.ToArray();
                        applicationUser.ProfilePicture = imageBytes;
                    }
                }
            }
            ApplicationUser userdata = await _userManager.FindByIdAsync(userId);
            userdata.UserName = applicationUser.UserName;
            userdata.FullName = applicationUser.FullName;
          
            userdata.ProfilePicture = applicationUser.ProfilePicture;
            userdata.Email = applicationUser.Email;
            var result = await _userManager.UpdateAsync(userdata);
            return View(userdata);  
        }
        // Change Password
        public ActionResult ChangePassword()
        { 
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> ChangePassword(ApplicationUser applicationUser)
        { 
            // Get Current Userid
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var users = _userManager.Users.Where(x => x.Id == userId).FirstOrDefault();
            ApplicationUser userdata = await _userManager.FindByIdAsync(userId);
            userdata.PasswordHash = applicationUser.PasswordHash;
            var result = await _userManager.UpdateAsync(userdata); 
            return View(userdata); 
        }
        public async Task<IActionResult> Index() 
        {
            var users = await _userManager.Users.ToListAsync();
            var userRolesViewModel = new List<RoleAsignToUser>();
            foreach (ApplicationUser user in users)
            {
                var thisViewModel = new RoleAsignToUser();
                thisViewModel.UserId = user.Id;
                thisViewModel.Email = user.Email;
                thisViewModel.FullName= user.FullName;
                thisViewModel.UserName = user.UserName;
                thisViewModel.Roles = await GetUserRoles(user);
                userRolesViewModel.Add(thisViewModel);
            }
            return View(userRolesViewModel);
        }
       [Authorize(Roles = "SuperAdmin")] 
        public async Task<IActionResult> Manage(string userId)
        {
            ViewBag.userId = userId;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }
            ViewBag.UserName = user.UserName;
            var model = new List<ManageUserRolesViewModel>();
            foreach (var role in _roleManager.Roles)
            {
                var userRolesViewModel = new ManageUserRolesViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.Selected = true;
                }
                else
                {
                    userRolesViewModel.Selected = false;
                }
                model.Add(userRolesViewModel);
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Manage(List<ManageUserRolesViewModel> model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View();
            }
            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(model);
            }
            result = await _userManager.AddToRolesAsync(user, model.Where(x => x.Selected).Select(y => y.RoleName));
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(model);
            }
            return RedirectToAction("Index");
        }
        private async Task<List<string>> GetUserRoles(ApplicationUser user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }
    }
}