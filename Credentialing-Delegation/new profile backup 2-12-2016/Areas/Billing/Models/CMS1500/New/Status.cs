using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class Status
    {
        [Key]
        public int StatusID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
    
    }
}