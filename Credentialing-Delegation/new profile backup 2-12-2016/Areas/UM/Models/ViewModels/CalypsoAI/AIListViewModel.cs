using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.ViewModels.CalypsoAI
{
    public class AIListViewModel
    {
        public AIListViewModel()
        {
            VisitDuplicacyResult = new List<AICalypsoOutputViewModel>();
            AdmissionDuplicacyResult = new List<AICalypsoOutputViewModel>();
        }
        public List<AICalypsoOutputViewModel> VisitDuplicacyResult { get; set; }
        public List<AICalypsoOutputViewModel> AdmissionDuplicacyResult { get; set; } 
    }
}