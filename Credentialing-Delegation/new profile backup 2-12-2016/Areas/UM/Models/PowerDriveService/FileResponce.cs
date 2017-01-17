using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.UM.Models.PowerDriveService
{
    public class FileResponce
    {
       
            public Result Result { get; set; }
            public string AccountName { get; set; }
            public string UserName { get; set; }
            public string Source { get; set; }
            public string ClaimType { get; set; }
        
    }
}
