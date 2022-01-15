using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models.CRMModel
{
	public class ConsultancyCountries
	{
		[Key]
		public long ID { get; set; }
		public string CountryName { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }
		public bool Status { get; set; }
	}
}
