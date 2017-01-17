using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.CredAxis.Models.EducationViewModel
{
    public class EducationMainViewModel
    {
        public EducationMainViewModel()
        {
            ECFMGs = new ECFMGViewModel();
            GradSchools = new List<GraduateSchoolViewModel>();
            PostGradSchools = new List<PostGraduationViewModel>();
            Residency = new List<ResidencyViewModel>();
            UnderGradSchools = new List<UnderGraduationViewModel>();

        }
        public ECFMGViewModel ECFMGs { set; get; }
        public List<GraduateSchoolViewModel> GradSchools { set; get; }
        public List<PostGraduationViewModel> PostGradSchools { set; get; }
        public List<ResidencyViewModel> Residency { set; get; }
        public List<UnderGraduationViewModel> UnderGradSchools { set; get; }
    }
}