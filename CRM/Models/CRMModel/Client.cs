using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models.CRMModel
{
	public class Client
	{
		[Key]
		public string ACC_CODE { get; set; }
		public string Client_Name { get; set; }
		public string ADD { get; set; }
		public string PHONE { get; set; }
		public string SPONO { get; set; }
		public string FAX { get; set; }
		public string CAT { get; set; }
		public double Cr_Limit { get; set; }
		public string Cr_Terms { get; set; }
		public string USR_NA { get; set; }
		public Single SP_Dom { get; set; }
		public Single SP_Int { get; set; }
		public Single SP2_Dom { get; set; }
		public Single SP2_Int { get; set; }
		public string UPD_CHK { get; set; }
		public string CLN_MASK { get; set; }
		public bool CLN_CHK { get; set; }
		public string CLN_DSP_NA { get; set; }
		public string CPERSON { get; set; }
		public string E_MAIL { get; set; }
		public long LEAD_ID { get; set; }
		public string CLN_REC_BAL_TYPE { get; set; }
		public string CLN_PAY_BAL_TYPE { get; set; }
	}
}
