using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models.CRMModel
{
	public class BranchAndStaff
	{
		[Key]
		public string StaffID { get; set; }
		public string BranchID { get; set; }
	}
}
