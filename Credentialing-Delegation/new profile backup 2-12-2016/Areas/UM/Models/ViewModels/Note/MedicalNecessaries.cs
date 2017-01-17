using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.ViewModels.Note
{
    public class MedicalNecessaries
    {
          [Display(Name = "REF")]
        public string Description { get; set; } 
    }
}