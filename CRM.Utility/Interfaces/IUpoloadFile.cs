using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Utility.Interfaces
{
   public interface IUpoloadFile
    {
        string UpoloadFile(IFormFile file);
    }
}
