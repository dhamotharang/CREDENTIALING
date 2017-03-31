using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Models.EmailService
{
    public class EmailGroupViewModel
    {
        public int? EmailGroupID { get; set; }

        [Required]
        public string EmailGroupName { get; set; }

        [Required]
        public string Description { get; set; }

        public string EmailIds { get; set; }

        public int? LastUpdatedBy { get; set; }

        public int? CreatedBy { get; set; }

        public List<string> Emailiddetails { get; set; }

        public Dictionary<int, string> CduserIds { get; set; }

    }
}