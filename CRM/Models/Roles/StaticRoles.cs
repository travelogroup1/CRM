﻿using CRM.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using CRM.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CRM.Models
{
    public static class StaticRoles
    {
        public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Roles.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Moderator.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Basic.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));
        }
        public enum Roles
        {
            SuperAdmin,
            Admin,
            Moderator,
            Basic,
            User
        }
        public static List<Claim> AllClaims = new List<Claim>()
        {
            new Claim("canadduser","canadduser"),
            new Claim("canupdateuser","canupdateuser"),
            new Claim("canviewuser","canviewuser"),
            new Claim("superadminpermission","superadminpermission")
        };
        public static async Task SeedSuperAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ClaimsViewModel claimsViewModel = new ClaimsViewModel();
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "superadmin",
                Email = "superadmin@gmail.com",
                FirstName = "awais",
                LastName = "ali",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Pass@123");
                    await userManager.AddToRoleAsync(defaultUser, Roles.SuperAdmin.ToString());
                    var id = roleManager.Roles.Where(x => x.Name == Roles.SuperAdmin.ToString()).Select(x => x.Id).FirstOrDefault();
                    ClaimsListViewModel cla = new ClaimsListViewModel();


                    List<ClaimsListViewModel> inputData = new List<ClaimsListViewModel>();

                    // going through collection from which i want to copy
                    foreach (var parameter in AllClaims)
                    {
                        inputData.Add(new ClaimsListViewModel() { IsSelected = true, ClaimsType = parameter.Value });
                    }
                    claimsViewModel.RoleId = id;
                    claimsViewModel.ClaimsList = inputData;



                    var Role = await roleManager.FindByIdAsync(claimsViewModel.RoleId);
                    if (Role.Id != "" && Role.Id != null)
                    {
                        IList<Claim> cl = await roleManager.GetClaimsAsync(Role);
                        for (var i = 0; i < cl.Count; i++)
                        {
                            var result = await roleManager.RemoveClaimAsync(Role, cl[i]);
                        }

                        foreach (var item in claimsViewModel.ClaimsList.Where(c => c.IsSelected == true))
                        {
                            Claim c = new Claim(item.ClaimsType, item.ClaimsType);
                            var r = await roleManager.AddClaimAsync(Role, c);
                        }
                    }





                }
            }
        }
    }
  
}
