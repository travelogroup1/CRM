using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models.CRMModel
{
	public class Banks
	{
		[Key]
		public long Bnk_ID { get; set; }
		public string Bank_Name { get; set; }
		public string Bank_Logo { get; set; }
		public string BankCode { get; set; }
		public bool Status { get; set; }
		public string ACC_CODE { get; set; }
	}
}
