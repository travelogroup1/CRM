using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models.CRMModel
{
	public class BankAccounts
	{
		[Key]
		public long BankAccID { get; set; }
		public long BankID { get; set; }
		public string ACC_CODE { get; set; }
		public string BankACCNumber { get; set; }
		public string ACCType { get; set; }
		public string BankACCNoShort { get; set; }
	}
}
