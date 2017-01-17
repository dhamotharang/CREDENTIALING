using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Billing.Models.File_Management
{
    public class File835
    {
        public int? FileID { get; set; }

        public string PayerName { get; set; }

        public string PayeeName { get; set; }

        public string PayeeID { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime? CheckIssueDate { get; set; }

        public string TotalPaymentAmount { get; set; }

        public string CheckNumber { get; set; }

        public string FileName { get; set; }

        public string FileKey { get; set; }

        public string ApplicaionOrGroupName { get; set; }

        public string RegisterUserName { get; set; } 

    }
}