using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models.CRMModel
{
	public class City_Taxes
	{
		[Key]
		public string CTX_Type { get; set; }
		public string CTX_Code { get; set; }
		public string CTX_Name { get; set; }
		public short CTX_Amt { get; set; }
		public string UPD_CHK { get; set; }
	}
}
