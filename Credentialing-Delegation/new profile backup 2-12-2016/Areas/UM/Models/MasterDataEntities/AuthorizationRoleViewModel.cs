using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortalTemplate.Areas.UM.Models.MasterDataEntities
{
   public class AuthorizationRoleViewModel
    {
        public int AuthorizationRoleID { get; set; }

        public string Role { get; set; }

        public string Module { get; set; }
    }
}
