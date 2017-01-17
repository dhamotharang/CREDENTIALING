using PortalTemplate.Areas.UM.Models.ViewModels.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.DTO
{
    public class BucketDTO
    {
        public String Action { get; set; }
        public String CurrentUserRole { get; set; }
        public String CurrentUserId { get; set; }
        public String CurrentUserName { get; set; }
        public AuthorizationStatusViewModel CurrentAuthStatus { get; set; }
    }
}