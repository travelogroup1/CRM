using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models.CRMModel
{
	public class City
	{
		[Key]
		public int Id { get; set; }
		public int AreaId { get; set; }
		public string CityCode { get; set; }
		public string CityName { get; set; }
		public int CountryId { get; set; }
		public bool IsActive { get; set; }
	}
}
