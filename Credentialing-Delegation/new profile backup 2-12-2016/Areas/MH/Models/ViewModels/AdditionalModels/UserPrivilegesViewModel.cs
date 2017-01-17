using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.MH.Models.ViewModels.AdditionalModels
{
    public class UserPrivilegesViewModel
    {
        public string UserId { get; set; }

        public string UserType { get; set; }

        public string Status { get; set; }

        public bool IsPrivilegedUser { get; set; }

        public string Role { get; set; }
    }
}