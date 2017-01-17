using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Auditing.Models.CreateAuditing
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }
        public string CategoryCode { get; set; }
        [DisplayName("Category")]
        public string CategoryName { get; set; }
        [DisplayName("Remarks")]
        public string Remarks { get; set; }
        public bool selected { get; set; }
    }
}