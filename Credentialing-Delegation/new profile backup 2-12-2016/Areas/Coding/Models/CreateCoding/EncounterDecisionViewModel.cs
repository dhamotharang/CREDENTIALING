using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Coding.Models.CreateCoding
{
    public class EncounterDecisionViewModel
    {
        public EncounterDecisionViewModel()
        {
            this.Categories = new List<CategoryViewModel>();
        }
        public bool IsAgree { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
    }
}