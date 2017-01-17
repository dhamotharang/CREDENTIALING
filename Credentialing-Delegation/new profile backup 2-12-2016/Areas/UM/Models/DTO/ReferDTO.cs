using PortalTemplate.Areas.UM.Models.ViewModels.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.DTO
{
    public class ReferDTO
    {
        public  AuthorizationStatusViewModel CurrentAuthStatus {get; set; }
        public int AuthID { get; set; }
        public String Reason { get; set; }
        public String ReasonType { get; set; }
    }
}