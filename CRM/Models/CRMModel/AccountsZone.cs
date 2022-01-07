using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models.CRMModel
{
    public class AccountsZone
    {
        [Key]
        public long ZoneId{ get; set; }
        public string ZoneDescription{ get; set; }
        public string ZoneSuperVisor{ get; set; }
    }
}
