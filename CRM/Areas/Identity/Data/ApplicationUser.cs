using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CRM.Areas.Identity.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UsernameChangeLimit { get; set; } = 10;
        public string LanguageId { get; set; }
        public byte[] ProfilePicture { get; set; }
        public string Category { get; set; }
        public string Branch  { get; set; }
        public string Address { get; set; }
        public string Emergency_Contact { get; set; }
        public string CNIC { get; set; }
        public byte[] CNIC_Front_Image { get; set; }
        public byte[] CNIC_Back_Image { get; set; }
        public string House_Location { get; set; }
        public DateTime Joining_Date { get; set; } 
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Mobile { get; set; } 
        public string Status { get; set; }
        public string Personal_Mobile { get; set; }
    }
}
