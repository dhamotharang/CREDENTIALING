using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.ViewModels.QueueBucket
{
    public class UserViewModel
    {
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Display(Name = "Total")]
        public string TotalCount { get; set; }

        [Display(Name = "Standard")]
        public string StandardCount { get; set; }

        [Display(Name = "Expedited")]
        public string ExpeditedCount { get; set; }

        public string UserRole { get; set; }


    }
}