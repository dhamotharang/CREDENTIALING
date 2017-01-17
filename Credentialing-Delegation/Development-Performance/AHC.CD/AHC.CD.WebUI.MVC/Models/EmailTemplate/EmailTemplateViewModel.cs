using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Models.EmailTemplate
{
    public class EmailTemplateViewModel
    {
        public int EmailTemplateID { get; set; }

        [Required(ErrorMessage = "Title field is required.")]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Subject field is required.")]
        public string Subject { get; set; }

        public string Body { get; set; }

        public StatusType? StatusType { get; set; }
    }
}