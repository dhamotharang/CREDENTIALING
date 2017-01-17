using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.ViewModels.QueueBucket
{
    public class QueueBucketViewModel
    {
        public List<string> Roles { get; set; }

        public string MemberName { get; set; }

        public string MemberId { get; set; }

        public string AuthType { get; set; }

        public string POS { get; set; }

        public string OutPatientType { get; set; }

        public string ActionPerformed { get; set; }

        public string CurrenUserRole { get; set; }

        public string AuthStatus { get; set; }

        //[Display(Name="TOTAL")]
        //public string TotalCount { get; set; }

        //[Display(Name = "Expedite")]
        //public string ExpeditedCount { get; set; }

        //[Display(Name = "Standard")]
        //public string StandardCount { get; set; } 
            
    }
}