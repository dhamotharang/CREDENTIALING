using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Models.EmailService
{
    public class EmailGroupViewModel
    {
        [Required]
        public string EmailGroupName { get; set; }

        public string EmailIds { get; set; }

        public List<string> Emailiddetails { get; set; }

    }
}