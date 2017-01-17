using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Portal.Models.PriorAuth.PriorAuthorization
{
    public class AuthorizationStatusViewModel
    {
        public string CurrentStatus { get; set; }

        public string CurrentQueue { get; set; }

        public string ReferFromUserRole { get; set; }

        public string ReferFromUserName { get; set; }

        public string ReferFromUserId { get; set; }

        public string ReferToUserRole { get; set; }

        public string ReferToUserName { get; set; }

        public string ReferToUserId { get; set; }

        public string ReasonForPend { get; set; }
    }
}