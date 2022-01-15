using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models.CRMModel
{
    public class BranchData
    {
        [Key]
		public string CityCode { get; set; }
		public string BranchCode { get; set; }
		public string BranchName { get; set; }
		public string BranchType { get; set; }
		public string FriendlyName { get; set; }
		public string Address { get; set; }
		public string Phone { get; set; }
		public string Fax { get; set; }
		public string Email { get; set; }
		public string Website { get; set; }
		public int OrderID { get; set; }
		public bool Status { get; set; }
		public string Br_Code { get; set; }
		public string ACC_CODE { get; set; }
	}
}
