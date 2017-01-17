using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Models.PSVInfo
{
    public class VerificationResultViewModel
    {
        public int VerificationResultId { get; set; }

        public string Remark { get; set; }

        public string Source { get; set; }

        public VerificationResultStatusType? VerificationResultStatusType { get; set; }

        public string VerificationDocumentPath { get; set; }

        public HttpPostedFileBase VerificationResultDocument { get; set; }
          
    }
}