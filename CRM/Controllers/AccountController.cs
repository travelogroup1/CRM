using CRM.Areas.Identity.Data;
using CRM.Data;
using CRM.Models;
using CRM.Models.ViewModels;
using CRM.Utility.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
//using System.Web.Script.Serialization;

namespace CRM.Controllers
{
    public class AccountController : Controller
    {
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _rolemanger;
        private IHttpContextAccessor _httpContextAccessor;
        private CRMContext _context;
        private IUpoloadFile _uploadfile;

        public AccountController(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> rolemanager,
            IHttpContextAccessor httpContextAccessor,
            CRMContext context,
            IUpoloadFile uploadfile)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _rolemanger = rolemanager;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _uploadfile = uploadfile;
        }

        #region Roles
        public IActionResult RoleList()
        {
            var roles = _rolemanger.Roles.ToList();
            List<UserRolesViewModel> vm = new List<UserRolesViewModel>();
            foreach (var item in roles)
            {
                vm.Add(new UserRolesViewModel() { Id = item.Id, RoleName = item.Name });
            }
            return View(vm);
        }
        [HttpGet]
        public IActionResult CreateRole(string id)
        {
            var rol = _rolemanger.Roles.Where(x => x.Id == id);
            UserRolesViewModel vm = new UserRolesViewModel();
            if (rol.Count() > 0)
            {
                vm.Id = rol.FirstOrDefault().Id;
                vm.RoleName = rol.FirstOrDefault().Name;
            }
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(UserRolesViewModel model)
        {
            if (model.Id != "" && model.Id != null)
            {
                IdentityRole role = await _rolemanger.FindByIdAsync(model.Id);
                role.Name = model.RoleName;
                await _rolemanger.UpdateAsync(role);
            }
            else
            {
                await _rolemanger.CreateAsync(new IdentityRole(model.RoleName));
            }
            return RedirectToAction("RoleList");
        }
        public async Task<IActionResult> DeleteRole(string id)
        {
            if (id != "" || id != null)
            {
                IdentityRole role = await _rolemanger.FindByIdAsync(id);
                await _rolemanger.DeleteAsync(role);
            }

            return RedirectToAction("RoleList");
        }
        #endregion

        #region Claims
        [HttpGet]
        public async Task<IActionResult> GetAllClaims(string roleid)
        {
            IdentityRole rol = await _rolemanger.FindByIdAsync(roleid);
            var existingclaims = await _rolemanger.GetClaimsAsync(rol);
            var model = new ClaimsViewModel()
            {
                RoleId = roleid
            };
            foreach (Claim claim in ClaimStore.AllClaims)
            {
                ClaimsListViewModel claimlist = new ClaimsListViewModel()
                {
                    ClaimsType = claim.Type
                };
                if (existingclaims.Any(c => c.Type == claim.Type))
                {
                    claimlist.IsSelected = true;
                }
                model.ClaimsList.Add(claimlist);
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateAllClaims(ClaimsViewModel model)
        {
            var Role = await _rolemanger.FindByIdAsync(model.RoleId);
            if (Role.Id != "" && Role.Id != null)
            {
                IList<Claim> cl = await _rolemanger.GetClaimsAsync(Role);
                for (var i = 0; i < cl.Count; i++)
                {
                    var result = await _rolemanger.RemoveClaimAsync(Role, cl[i]);
                }

                foreach (var item in model.ClaimsList.Where(c => c.IsSelected == true))
                {
                    Claim c = new Claim(item.ClaimsType, item.ClaimsType);
                    var r = await _rolemanger.AddClaimAsync(Role, c);
                }
            }
            return RedirectToAction("RoleList", "Account");
        }

        #endregion
        //[AllowAnonymous]
        //[Route("google-login")]
        //public IActionResult GoogleLogin()
        //{
        //    var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") };
        //    return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        //}
        [AllowAnonymous]
        [Route("google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            var claims = result.Principal.Identities
                .FirstOrDefault().Claims.Select(claim => new
                {
                    claim.Issuer,
                    claim.OriginalIssuer,
                    claim.Type,
                    claim.Value
                });

            return Json(claims);
        }
        #region register
        public IActionResult Register()
        {
            ViewBag.BranchList = _context.branch.ToList();

            MemoryStream ms = new MemoryStream();
            //File.CopyTo(ms);
            //img.ImageData = ms.ToArray();
            return View();
        }

        [HttpPost]
        public JsonResult RegisterPost()
      {

            ApplicationUser user = new ApplicationUser();
            string IsCnicFront = Request.Form.Where(x => x.Key == "iscnicfrontfile").FirstOrDefault().Value;
            string IsCnicBack = Request.Form.Where(x => x.Key == "iscnicbackfile").FirstOrDefault().Value;
            string IsPofile = Request.Form.Where(x => x.Key == "isprofilefile").FirstOrDefault().Value;
            string IsAgreement = Request.Form.Where(x => x.Key == "IsAgreement").FirstOrDefault().Value;
            if (IsCnicFront == "1")
            {
                var files1 = Request.Form.Files["frntimgfile"];
                MemoryStream cnicfim = new MemoryStream();
                files1.CopyTo(cnicfim);

                user.CNIC_Front_Image = cnicfim.ToArray();
                user.CNICFrontBase64 = Convert.ToBase64String(user.CNIC_Front_Image);

            }
            if (IsCnicBack == "1")
            {

                var files2 = Request.Form.Files["backimgfile"];
                MemoryStream cnicbim = new MemoryStream();
                files2.CopyTo(cnicbim);
                user.CNIC_Back_Image = cnicbim.ToArray();

                user.CNICBackBase64 = Convert.ToBase64String(user.CNIC_Back_Image);
            }
            if (IsPofile == "1")
            {
                var files3 = Request.Form.Files["profileimgfile"];
                MemoryStream profileim = new MemoryStream();
                files3.CopyTo(profileim);
                user.ProfilePicture = profileim.ToArray();
                user.ProfileBase64 = Convert.ToBase64String(user.ProfilePicture);

            }
            if (IsAgreement == "1")
            {
                var files4 = Request.Form.Files["AgreementFile"];
              user.AgreementFilePath= _uploadfile.UpoloadFile(files4);
            }




            user.FullName = Request.Form.Where(x => x.Key == "FullName").FirstOrDefault().Value;
            user.UserName = Request.Form.Where(x => x.Key == "UserName").FirstOrDefault().Value;
            user.Category = Request.Form.Where(x => x.Key == "Category").FirstOrDefault().Value;
            user.Branch = Convert.ToInt32(Request.Form.Where(x => x.Key == "Branch").FirstOrDefault().Value.ToString());
            user.Address = Request.Form.Where(x => x.Key == "Address").FirstOrDefault().Value;
            user.Emergency_Contact = Request.Form.Where(x => x.Key == "Emergency_Contact").FirstOrDefault().Value;
            user.CNIC = Request.Form.Where(x => x.Key == "CNIC").FirstOrDefault().Value;
            user.House_Location = Request.Form.Where(x => x.Key == "House_Location").FirstOrDefault().Value;
            user.Joining_Date = DateTime.Now.Date;

            user.OfficialMobile = Request.Form.Where(x => x.Key == "OfficialMobile").FirstOrDefault().Value;
            user.Status = Request.Form.Where(x => x.Key == "Status").FirstOrDefault().Value;
            user.Personal_Mobile = Request.Form.Where(x => x.Key == "Personal_Mobile").FirstOrDefault().Value;



            user.Added_By = GetCurrentUserAsync()?.Result.FullName;
            user.Added_Date = Convert.ToString(DateTime.Now);
            user.IsDeleted = 0;
            _context.Add(user);

            _context.SaveChanges();





            return Json(true);
        }

        #endregion
        #region get userlist
        [HttpGet]
        public JsonResult GetAllUsers(ApplicationUser user)
        {     var UserList = (from a in _context.applicationUsers

                         join b in _context.branch on a.Branch equals b.BranchId
                         select new {
                             a.Id,
                            a.FullName,
                            a.UserName,
                            a.CNIC,
                            a.Address,
                            a.OfficialMobile,
                            a.Category,
                            a.Status,
                            b.BranchName,
                            a.IsDeleted,
                            a.ProfileBase64,
                            a.CNICFrontBase64,
                            a.CNICBackBase64,
                            a.AgreementFilePath
                         }
                         ).Where(x=>x.IsDeleted== user.IsDeleted).ToList();
            foreach (var item in UserList)
            {

            }
           // var list = _context.applicationUsers.ToList();
            return Json(UserList);
        }
        #endregion
        #region delete user
        [HttpPost]
        public JsonResult DeleteUser(string Id)
        {
            //ApplicationUser branchModel = new Branch
            //{
            //    BranchId = Convert.ToInt32(BranchId),
            //};
          ApplicationUser apuser=  _context.applicationUsers.Where(x=>x.Id== Id).FirstOrDefault();
            apuser.IsDeleted = 1;
            _context.SaveChanges();
            return Json(true);
        }
        #endregion
        #region get user by id
        [HttpPost]
        public JsonResult GetUserById(string Id)
        {
            var User = (from a in _context.applicationUsers

                            join b in _context.branch on a.Branch equals b.BranchId
                            select new
                            {
                                a.Id,
                                a.FullName,
                                a.UserName,
                                a.CNIC,
                                a.Address,
                                a.OfficialMobile,
                                a.Category,
                                a.Status,

                                a.IsDeleted,
                                a.ProfileBase64,
                                a.CNICFrontBase64,
                                a.CNICBackBase64,
                                a.Emergency_Contact,
                                a.House_Location,


                               a.Personal_Mobile,


                               b.BranchId



                            }
                        ).Where(x => x.IsDeleted == 0 && x.Id==Id).FirstOrDefault();
            return Json(User);
        }
        #endregion
        #region updateuser
        [HttpPost]
        public JsonResult UpdateUser()
        {

            string UserId = Request.Form.Where(x => x.Key == "UserId").FirstOrDefault().Value;
            ApplicationUser user = _context.applicationUsers.Where(x => x.Id == UserId).FirstOrDefault();

            string IsCnicFront = Request.Form.Where(x => x.Key == "iscnicfrontfile").FirstOrDefault().Value;
            string IsCnicBack = Request.Form.Where(x => x.Key == "iscnicbackfile").FirstOrDefault().Value;
            string IsPofile = Request.Form.Where(x => x.Key == "isprofilefile").FirstOrDefault().Value;
            string IsAgreement = Request.Form.Where(x => x.Key == "IsAgreement").FirstOrDefault().Value;
            if (IsCnicFront == "1")
            {
                var files1 = Request.Form.Files["frntimgfile"];
                MemoryStream cnicfim = new MemoryStream();
                files1.CopyTo(cnicfim);

                user.CNIC_Front_Image = cnicfim.ToArray();
            }
            if (IsCnicBack == "1")
            {

                var files2 = Request.Form.Files["backimgfile"];
                MemoryStream cnicbim = new MemoryStream();
                files2.CopyTo(cnicbim);
                user.CNIC_Back_Image = cnicbim.ToArray();
            }
            if (IsPofile == "1")
            {
                var files3 = Request.Form.Files["profileimgfile"];
                MemoryStream profileim = new MemoryStream();
                files3.CopyTo(profileim);
                user.ProfilePicture = profileim.ToArray();
            }
            if (IsAgreement == "1")
            {
                var files4 = Request.Form.Files["AgreementFile"];
                user.AgreementFilePath = _uploadfile.UpoloadFile(files4);
            }



            user.FullName = Request.Form.Where(x => x.Key == "FullName").FirstOrDefault().Value;
            user.UserName = Request.Form.Where(x => x.Key == "UserName").FirstOrDefault().Value;
            user.Category = Request.Form.Where(x => x.Key == "Category").FirstOrDefault().Value;
            user.Branch = Convert.ToInt32(Request.Form.Where(x => x.Key == "Branch").FirstOrDefault().Value.ToString());
            user.Address = Request.Form.Where(x => x.Key == "Address").FirstOrDefault().Value;
            user.Emergency_Contact = Request.Form.Where(x => x.Key == "Emergency_Contact").FirstOrDefault().Value;
            user.CNIC = Request.Form.Where(x => x.Key == "CNIC").FirstOrDefault().Value;
            user.House_Location = Request.Form.Where(x => x.Key == "House_Location").FirstOrDefault().Value;


            user.OfficialMobile = Request.Form.Where(x => x.Key == "OfficialMobile").FirstOrDefault().Value;
            user.Status = Request.Form.Where(x => x.Key == "Status").FirstOrDefault().Value;
            user.Personal_Mobile = Request.Form.Where(x => x.Key == "Personal_Mobile").FirstOrDefault().Value;



            user.Modified_By = GetCurrentUserAsync()?.Result.FullName;
            user.Modified_Date = Convert.ToString(DateTime.Now);
            user.IsDeleted = 0;


            _context.SaveChanges();





            return Json(true);
        }

        #endregion


        #region check user exist
        [HttpPost]
        public JsonResult CheckUserExist(string FullName)
        {
            int exist = 0;
            if (_context.applicationUsers.Any(o => o.FullName == FullName))
            {
                exist = 1;
            }
            else
            {
                exist = 0;
            }

            return Json(exist);
        }
        #endregion

    }

}
