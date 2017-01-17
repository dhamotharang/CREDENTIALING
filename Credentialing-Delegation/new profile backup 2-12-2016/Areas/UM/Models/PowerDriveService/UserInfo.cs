using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.UM.Models.PowerDriveService
{
    public class UserInfo
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string ApplicaionOrGroupName { get; set; }

        public string Token { get; set; }

        public string Path { get; set; }
    }
}
