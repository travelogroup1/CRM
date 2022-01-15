using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models.CRMModel
{
	public class CashBook_Data
	{
		[Key]
		public int AutoID { get; set; }
		public DateTime Dated { get; set; }
		public string BranchRef { get; set; }
		public decimal OpeningBalance { get; set; }
		public bool Closed { get; set; }
		public string ClosedBy { get; set; }
	}
}
