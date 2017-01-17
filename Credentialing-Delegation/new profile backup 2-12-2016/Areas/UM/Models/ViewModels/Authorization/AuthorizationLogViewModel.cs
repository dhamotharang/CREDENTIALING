using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.ViewModels.Authorization
{
    public class AuthorizationLogViewModel
    {
        public int AuthorizationHistoryID { get; set; }

        public string UniqueID { get; set; }

        [Display(Name="MODULE")]
        public string Module { get; set; }

        [Display(Name = "ACTION")]
        public string Action { get; set; }

        [Display(Name = "CATEGORY")]
        public string Category { get; set; }

        [Display(Name = "REF #")]
        public int ReferenceNo { get; set; }

        [Display(Name = "SCREEN")]
        public string Screen { get; set; }

        [Display(Name = "USER")]
        public string FullNameCurrentUser { get; set; }

        [Display(Name = "OUTCOME")]
        public string OutCome { get; set; }

        public int AuthorizationID { get; set; }

        [Display(Name = "ACTIVITY LOG")]
        public string ActivityLog { get; set; }

        [Display(Name = "DATE TIME")]
        public DateTime LastModifiedDate { get; set; }
    }
}