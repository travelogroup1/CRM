using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Model.CRMModel
{
    public class Branch
    {
        [Key]
        public int BranchId { get; set; }
        public string BranchCity { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string BranchAddress { get; set; }
        public string BranchContactNo { get; set; }
        public string Added_By { get; set; }
        public string Added_Date { get; set; }
        public string Modified_By { get; set; }
        public string Modified_Date { get; set; }

    }
}
