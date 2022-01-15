using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models.CRMModel
{
	public class ClosingBalance
	{
		[Key]
		public int ClosingbalID { get; set; }
		public DateTime PostedDate { get; set; }
		public string BranchRef { get; set; }
		public decimal ClosingBalances  { get; set; }
	}
}
