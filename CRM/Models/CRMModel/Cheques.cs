using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models.CRMModel
{
	public class Cheques
	{
		[Key]
		public string ChequeNumber { get; set; }
		public string BankCode { get; set; }
		public string VNO { get; set; }
		public DateTime DateTimeStamp { get; set; }
		public string ChequeType { get; set; }
		public string ChequeStatus { get; set; }
		public DateTime ClearanceDate { get; set; }
		public string ClearanceBank { get; set; }
		public string ClearingStatus { get; set; }
		public bool PrintStatus { get; set; }
		public bool DepositStatus { get; set; }
		public string CheqDepositeBank { get; set; }
		public string CheqAction { get; set; }
	}
}
