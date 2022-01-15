using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models.CRMModel
{
	public class Attachments
	{
		[Key]
		public long FileID { get; set; }
		public string FileName { get; set; }
		public string FilePath { get; set; }
		public DateTime FileDate { get; set; }
	}
}
