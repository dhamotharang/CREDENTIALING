using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Models
{
    public class PlanDataViewModel
    {
        public ICollection<PlanDocumentViewModel> Documents { get; set; }

        public ICollection<SpecialtyViewModel> Specialties { get; set; }

        public ICollection<QuestionViewodel> Questions { get; set; }

        public ApplicationFormDataViewModel ApplicationForm { get; set; }
    }
}
