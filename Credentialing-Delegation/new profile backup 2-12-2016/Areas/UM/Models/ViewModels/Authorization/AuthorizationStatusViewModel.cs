using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.ViewModels.Authorization
{
    public class AuthorizationStatusViewModel
    {
        public string ReferToUserRole { get; set; }

        public string ReferToUserName { get; set; }

        public string ReferToUserId { get; set; }

        [Display(Name = "STANDARD")]
        public string ReferToUserStandardCount { get; set; }

        [Display(Name = "EXPEDITED")]
        public string ReferToUserExpeditedCount { get; set; }

        public string ReferFromUserRole { get; set; }

        public string ReferFromUserName { get; set; }

        public string ReferFromUserId { get; set; }

        [Display(Name = "STANDARD")]
        public string ReferFromUserStandardCount { get; set; }

        [Display(Name = "EXPEDITED")]
        public string ReferFromUserExpeditedCount { get; set; }

        public string ActionPerformed { get; set; }

    }

}