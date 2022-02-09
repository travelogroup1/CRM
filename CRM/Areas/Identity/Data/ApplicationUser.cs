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
        public string FullName { get; set; }

        public int UsernameChangeLimit { get; set; } = 10;
        public string LanguageId { get; set; }
        public byte[] ProfilePicture { get; set; }
        public string Category { get; set; }
        public int Branch  { get; set; }
        public string Address { get; set; }
        public string Emergency_Contact { get; set; }
        public string CNIC { get; set; }
        public byte[] CNIC_Front_Image { get; set; }
        public byte[] CNIC_Back_Image { get; set; }
        public string House_Location { get; set; }
        public DateTime Joining_Date { get; set; }

        public string OfficialMobile { get; set; }
        public string Status { get; set; }
        public string Personal_Mobile { get; set; }
        public string Added_By { get; set; }
        public string Added_Date { get; set; }
        public string Modified_By { get; set; }
        public string Modified_Date { get; set; }
        public int  IsDeleted { get; set; }
        public string CNICFrontBase64 { get; set; }
        public string CNICBackBase64 { get; set; }
        public string ProfileBase64 { get; set; }
        public string AgreementFilePath { get; set; }
    }
}
