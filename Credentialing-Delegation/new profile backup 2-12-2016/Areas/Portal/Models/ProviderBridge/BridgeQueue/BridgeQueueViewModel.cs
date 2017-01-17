using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Portal.Models.ProviderBridge.BridgeQueue
{
    public class BridgeQueueViewModel
    {
        [Display(Name = "NPI")]
        public string NPINumber { get; set; }

        [Display(Name = "STATUS")]
        public string Status { get; set; }

        [Display(Name = "ASSIGNED")]
        public string AssignedTo { get; set; }

        [Display(Name = "NP")]
        public string NetworkParticipation { get; set; }

        [Display(Name = "LAST NAME")]
        public string LastName { get; set; }

        [Display(Name = "FIRST NAME")]
        public string FirstName { get; set; }

        [Display(Name = "REQ BY")]
        public string ReqBy { get; set; }

        [Display(Name = "GENDER")]
        public string Gender { get; set; }

        [Display(Name = "REQ FROM")]
        public string Module { get; set; }

        [Display(Name = "PHONE")]
        public string PhoneNumber { get; set; }

        [Display(Name = "REQ DATE")]
        public string ReqDate { get; set; }

        [Display(Name = "ADDRESS")]
        public string Address { get; set; }

        [Display(Name = "CITY")]
        public string City { get; set; }

        [Display(Name = "ST")]
        public string State { get; set; }
       
        [Display(Name = "APP DATE")]
        public string AppDate { get; set; }

        [Display(Name = "APP BY")]
        public string AppBy { get; set; }

        [Display(Name = "REJ DATE")]
        public string RejDate { get; set; }

        [Display(Name = "REQUEST")]
        public string Request { get; set; }

        [Display(Name = "TIME")]
        public string TimeLeft { get; set; }

        [Display(Name = "Filter Type")]
        public string FilterType { get; set; }

        [Display(Name = "Assigned By")]
        public string AssignedBy { get; set; }

        [Display(Name = "Queue Status")]
        public string QueueStatus { get; set; }

         [Display(Name = "Timer")]
        public string Timer { get; set; }

         [Display(Name = "Assigned To Status")]
         public string AssignedToStatus { get; set; }

         public string ContactPhoneNumber { get; set; }
        
    }
}