using CRM.Areas.Identity.Data;
using CRM.Data;
using CRM.Model.CRMModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Controllers
{
    public class SettingController : Controller
    {
        // Current User Info
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        // Constructor Initialize
        private readonly ILogger<SettingController> _logger;
        private readonly CRMContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        // Define Constructor
        public SettingController(ILogger<SettingController> logger, CRMContext context, UserManager<ApplicationUser> userManager)
        {
            _logger = logger; 
            _userManager = userManager;
            _context = context;
        }
        #region Branch
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetBranch()
        {
            var list = _context.branch.ToList();
             return Json(list); 
        }
        [HttpPost]
        public JsonResult CreateBranch(string BranchName, string BranchAddress, string BranchCity, string BranchCode, string BranchContactNo)
        {
            Branch branchModel = new Branch
            {
                BranchName = BranchName,
                BranchAddress = BranchAddress,
                BranchCity = BranchCity,
                BranchCode = BranchCode,
                BranchContactNo = BranchContactNo
            };
            branchModel.Added_By = GetCurrentUserAsync()?.Result.FullName;
            branchModel.Added_Date = Convert.ToString(DateTime.Now);
            _context.Add(branchModel);   
            _context.SaveChanges();  
            return Json(true); 
        }
        [HttpPost]
        public JsonResult EditBranch(string BranchId, string BranchName, string BranchAddress, string BranchCity, string BranchCode, string BranchContactNo, string Added_By, string Added_Date)
        {
            Branch branchModel = new Branch
            {
                BranchId =Convert.ToInt32(BranchId),
                BranchName = BranchName,
                BranchAddress = BranchAddress,
                BranchCity = BranchCity,
                BranchCode = BranchCode,
                BranchContactNo = BranchContactNo,
                Added_By = Added_By,
                Added_Date = Added_Date
            };
            branchModel.Modified_By = GetCurrentUserAsync()?.Result.FullName;
            branchModel.Modified_Date = Convert.ToString(DateTime.Now);
            _context.Update(branchModel);
            _context.SaveChanges();
            return Json(true);
        } 
        [HttpPost] 
        public JsonResult DeleteBranch(string BranchId) 
        {
            Branch branchModel = new Branch
            {
                BranchId = Convert.ToInt32(BranchId),
            };
            _context.Remove<Branch>(branchModel); 
            _context.SaveChanges();
            return Json(true); 
        }
        #endregion
      
    }
}
 