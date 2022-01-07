using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models.CRMModel
{
	public class ACC_PL_LNKS
	{
		[Key]
		public string ACC_CODE{ get; set; }
		public string ACC_P_LINK{ get; set; }
	}
}
