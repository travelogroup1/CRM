using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models.CRMModel
{
	public class BossMessages
	{
		[Key]
		public int AutoID { get; set; }
		public DateTime Dated { get; set; }
		public string TextData { get; set; }
		public string UserID { get; set; }
		public bool Status { get; set; }
	}
}
