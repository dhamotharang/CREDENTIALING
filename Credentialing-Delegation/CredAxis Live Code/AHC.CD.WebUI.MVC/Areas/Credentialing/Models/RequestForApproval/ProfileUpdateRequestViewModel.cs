using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Models.RequestForApproval
{
    public class ProfileUpdateRequestViewModel
    {
        public string NPINumber { get; set; }
        public string Section { get; set; }
        public string SubSection { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ApprovalStatus { get; set; }
    }
}