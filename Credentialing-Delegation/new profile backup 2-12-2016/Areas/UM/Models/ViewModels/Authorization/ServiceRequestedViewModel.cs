using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.ViewModels.Authorization
{
    public class ServiceRequestedViewModel
    {
        public ServiceRequestedViewModel()
        {
            Services = new List<ServiceViewModel>();
        }
        public int? ServiceRequestedID { get; set; }

        public string Name { get; set; }

        List<ServiceViewModel> Services { get; set; }
    }
}