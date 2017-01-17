using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Models.DelegationProfileReport
{
    public class ProfileReportViewModel
    {        
        public int ProfileReportId { get; set; }

        public string TemplateName { get; set; }

        public string TemplateCode { get; set; }

        public string ProfileReportData { get; set; }

        public string Remarks { get; set; }

        public StatusType? StatusType {get; set; }   
       
    }
}