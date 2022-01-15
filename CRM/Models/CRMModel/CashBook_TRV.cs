using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models.CRMModel
{
	public class CashBook_TRV
	{
		[Key]
		public int AutoID { get; set; }
		public DateTime Dated { get; set; }
		public string BranchRef { get; set; }
		public double OpeningBalance { get; set; }
		public double Diff { get; set; }
		public double Damt { get; set; }
		public double Camt { get; set; }
		public bool Closed { get; set; }
		public string ClosedBy { get; set; }
	}
}
