using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models.CRMModel
{
	public class ACCOUNTS
	{
		[Key]
		public string ACC_CODE{ get; set; }
		public string ACC_NAME{ get; set; }
		public string ACC_TYPE{ get; set; }
		public string ACC_LINK{ get; set; }
		public bool ACC_CHK{ get; set; }
		public string USR_NA{ get; set; }
		public string ACC_MASK{ get; set; }
		public string UPD_CHK{ get; set; }
		public bool NNR{ get; set; }
		public double Op_Bal{ get; set; }
		public double OP_DAMT{ get; set; }
		public double OP_CAMT{ get; set; }
	}
}
