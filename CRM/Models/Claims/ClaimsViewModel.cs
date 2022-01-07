using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public class ClaimsViewModel
    { 
        public ClaimsViewModel()
        {
            ClaimsList = new List<ClaimsListViewModel>();
        }
        public string RoleId { get; set; }
        public List<ClaimsListViewModel> ClaimsList { get; set;}
    }
}
