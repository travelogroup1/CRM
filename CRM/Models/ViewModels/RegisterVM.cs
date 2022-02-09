using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models.ViewModels
{
    public class RegisterVM
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
      //  public int UsernameChangeLimit { get; set; } = 10;
      //  public string LanguageId { get; set; }
        public IFormFile ProfilePicture { get; set; }
        public string Category { get; set; }
        public int Branch { get; set; }
        public string Address { get; set; }
        public string Emergency_Contact { get; set; }
        public string CNIC { get; set; }
        public IFormFile CNIC_Front_Image { get; set; }
       public IFormFile CNIC_Back_Image { get; set; }
        public string House_Location { get; set; }
        public DateTime Joining_Date { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Mobile { get; set; }
        public string Status { get; set; }
        public string Personal_Mobile { get; set; }


      

    }
}
