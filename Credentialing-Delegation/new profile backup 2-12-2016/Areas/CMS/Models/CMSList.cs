using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.CMS.Models
{
    public class CMSList
    {
        [Display(Name = "EntityName")]
        public string EntityName { get; set; }
        [Display(Name = "ActiveCount")]
        public int ActiveCount { get; set; }
        [Display(Name = "InActiveCount")]
        public int InActiveCount { get; set; }
        [Display(Name = "ClickLink")]
        public string ClickLink { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}