using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Models.BridgeQueue
{
    public class BridgeQueueModel
    {
        [Key]
        public int? Id { get; set; }

        [Display(Name = "NPINumber")]
        [DisplayFormat(NullDisplayText = "-")]
          public string NPINumber { get; set; }

        [Display(Name = "LastName")]
        [DisplayFormat(NullDisplayText = "-")]
          public string LastName { get; set; }


        [Display(Name = "FirstName")]
        [DisplayFormat(NullDisplayText = "-")]
        public string FirstName { get; set; }

        [Display(Name = "ReqBy")]
        [DisplayFormat(NullDisplayText = "-")]
        public string ReqBy { get; set; }

        [Display(Name = "Gender")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Gender { get; set; }

        [Display(Name = "PhoneNumber")]
        [DisplayFormat(NullDisplayText = "-")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Module")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Module { get; set; }

        [Display(Name = "ReqDate")]
        [DisplayFormat(NullDisplayText = "-")]
        public string ReqDate { get; set; }

        [Display(Name = "Request")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Request { get; set; }

        [Display(Name = "Address")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Address { get; set; }

        [Display(Name = "City")]
        [DisplayFormat(NullDisplayText = "-")]
        public string City { get; set; }

        [Display(Name = "State")]
        [DisplayFormat(NullDisplayText = "-")]
        public string State { get; set; }

        [Display(Name = "Status")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Status { get; set; }

        [Display(Name = "TimeLeft")]
        [DisplayFormat(NullDisplayText = "-")]
        public string TimeLeft { get; set; }

        [Display(Name = "Timer")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Timer { get; set; }

        [Display(Name = "FilterType")]
        [DisplayFormat(NullDisplayText = "-")]
        public string FilterType { get; set; }

        [Display(Name = "AssignedTo")]
        [DisplayFormat(NullDisplayText = "-")]
        public string AssignedTo { get; set; }

        [Display(Name = "NetworkParticipation")]
        [DisplayFormat(NullDisplayText = "-")]
        public string NetworkParticipation { get; set; }

        [Display(Name = "QueueStatus")]
        [DisplayFormat(NullDisplayText = "-")]
        public string QueueStatus { get; set; }

    }
}
