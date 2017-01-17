using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.ViewModels.InsertText
{
    public class InsertTextViewModel
    {

        public InsertTextViewModel()
        {
            ListOfMedicalService = new List<string>();
        }

        [Display(Name = "ServiceSubjectToNotice", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string ServiceSubjectToNotice { get; set; }

        [Display(Name = "ListOfMedicalService", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public List<string> ListOfMedicalService { get; set; }

        [Display(Name = "Rationale", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string Rationale { get; set; }

        [Display(Name = "CriteriaUsed", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string CriteriaUsed { get; set; }

        [Display(Name = "AlternaltePlanOfCare", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string AlternaltePlanOfCare { get; set; }

        [Display(Name = "Note", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string Note { get; set; }

        [Display(Name = "ReviewType", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string ReviewType { get; set; }

        [Display(Name = "Decision", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string Decision { get; set; }

        [Display(Name = "Module", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string Module { get; set; }

    }
}