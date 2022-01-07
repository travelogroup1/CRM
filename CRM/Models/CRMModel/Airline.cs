using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models.CRMModel
{
    public class Airline
    {
		public string AL_Code{ get; set; }
		public string AL_DCode{ get; set; }
		public string AL_Type{ get; set; }
		public string AL_Name{ get; set; }
		public string AL_Address{ get; set; }
		public string AL_City{ get; set; }
		public string AL_Phone{ get; set; }
		public string AL_Fax{ get; set; }
		public string AL_Gl_Code{ get; set; }
		public bool AL_DFS{ get; set; }
		public string UPD_CHK{ get; set; }
		public int AL_DCOM{ get; set; }
		public int AL_ICOM{ get; set; }
		public string AL_DFH{ get; set; }
		public string AL_CURRENCY{ get; set; }
		public string AL_RATE_BASIS{ get; set; }
		public string AL_DSP_Name{ get; set; }
		public string AL_CPerson{ get; set; }
		public string BSPType{ get; set; }
		public string AL_BSPCode{ get; set; }
		public string AL_BSPDCode{ get; set; }
		public string AL_CRMDesc{ get; set; }
	}
}
