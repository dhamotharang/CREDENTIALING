using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.CredAxis.Models.DocumentRepoViewModel
{
    public class DoumentRepoMainViewModel
    {
        public DoumentRepoMainViewModel()
        {
            ProfileDoc = new List<ProfileDocViewModel>();
            PSV = new List<PSVViewModel>();
            GeneratedForms = new List<GeneratedFormsViewModel>();
            Forms = new List<FormsViewModel>();
        }

        public List<ProfileDocViewModel> ProfileDoc { get; set; }
        public List<PSVViewModel> PSV { get; set; }
        public List<GeneratedFormsViewModel> GeneratedForms { get; set; }
        public List<FormsViewModel> Forms { get; set; } 

    }
}