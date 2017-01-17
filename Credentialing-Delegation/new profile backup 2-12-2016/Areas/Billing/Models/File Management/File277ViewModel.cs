using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Billing.Models.File_Management
{
    public class File277ViewModel
    {
        public string ControlNumber { get; set; }
        public string FileName { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string FilePath { get; set; }
        public DateTime? DateOfCreation { get; set; }
        public DateTime? ReceivedDate { get; set; }
        //public int? AgeInDays { get; set; }
        public string Status { get; set; }
        public int? IncomeFileLoggerID { get; set; }
        public string FileKey { get; set; }
        public string ApplicaionOrGroupName { get; set; }
        public string RegisterUserName { get; set; } 
    }
}