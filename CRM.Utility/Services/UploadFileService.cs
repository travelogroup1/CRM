using CRM.Utility.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace CRM.Utility.Services
{
   public class UploadFileService: IUpoloadFile
    {
        private IHostingEnvironment Environment;

        public UploadFileService(IHostingEnvironment _environment)
        {
            Environment = _environment;
        }
        //public string UploadImg(IFormFile fle)
        //{
        //    string wwwPath = this.Environment.WebRootPath;
        //    string contentPath = this.Environment.ContentRootPath;

        //    var newguid = ""; var filepath = "";
        //    if (fle != null)
        //    {
        //        string FileExtension = Path.GetExtension(fle.FileName).ToLower();
        //        newguid = Convert.ToString(Guid.NewGuid());// + FileExtension;
        //        string newName = fle.FileName.Replace(fle.FileName, newguid);
        //        newName += FileExtension;
        //        // Get the complete folder path and store the file inside it.
        //        string Date = DateTime.Now.ToString("dd-MM-yyyy");
        //        string Hour = DateTime.Now.ToString("HH");
        //        string path = "~/AgreementFiles/" + Date + "/" + Hour + "/";
        //        string p = "/AgreementFiles/" + Date + "/" + Hour + "/";
        //        string DirectoryPath = Path.Combine(this.Environment.WebRootPath, path);

        //       // string DirectoryPath = Path.Combine(HttpContext);
        //        if (!Directory.Exists(DirectoryPath))
        //        {
        //            Directory.CreateDirectory(DirectoryPath);
        //        }

        //        using (var fileSteam = new FileStream(DirectoryPath, FileMode.Create))
        //        {
        //             fle.CopyToAsync(fileSteam);
        //        }


        //        filepath = "";
        //        filepath = p + newName;
        //    }
        //    return filepath;
        //}

        string IUpoloadFile.UpoloadFile(IFormFile file)
        {
            string wwwPath = Environment.WebRootPath;
            //string contentPathronment.ContentRootPath;

            var newguid = ""; var filepath = "";
            if (file != null)
            {
                string FileExtension = Path.GetExtension(file.FileName).ToLower();
                newguid = Convert.ToString(Guid.NewGuid());// + FileExtension;
                string newName = file.FileName.Replace(file.FileName, newguid);
                newName += FileExtension;
                // Get the complete folder path and store the file inside it.
                string Date = DateTime.Now.ToString("dd-MM-yyyy");
                string Hour = DateTime.Now.ToString("HH");
                string path = "AgreementFiles/" + Date+ "/" + Hour+"/";
               // string path = "AgreementFiles/d" + Date+ "/";
                string p = "/AgreementFiles/"+Date+"/"+Hour+"/";
                string DirectoryPath = Path.Combine(this.Environment.WebRootPath, path);

                AppDomain.CurrentDomain.SetData("UploadPath", DirectoryPath);

                if (!Directory.Exists(DirectoryPath))
                {
                    Directory.CreateDirectory(DirectoryPath);
                }
                filepath = Path.Combine(DirectoryPath, newName);

                using (var fileSteam = new FileStream(filepath, FileMode.Create))
                {
                    file.CopyTo(fileSteam);
                }


                filepath = "";
                filepath = p + newName;
                //filepath = newName;
            }
            return filepath;
        }
    }
}
