using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.PlanPDF.Models
{
    public class PlanFormViewModel
    {
        public int PlanFormID { get; set; }

        public string PlanFormName { get; set; }

        public string FileName { get; set; }

        public HttpPostedFileBase PlanFormFile { get; set; }

        public string PlanFormBelongsTo { get; set; }

        public string PlanFormPath { get; set; }

        public string PlanFormXmlPath { get; set; }

        public string IsXmlGenerated { get; set; }

        public string Status { get; private set; }
    }
}