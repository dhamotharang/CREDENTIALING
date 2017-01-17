using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Billing.Models.File_Management
{
    public class GenerateProvider
    {
        public GenerateProvider()
        {
            this.IsChecked = true;
            this.FileType = "005010X221A1";
        }
        public string NPI { get; set; }

        public bool IsChecked { get; set; }

        public int InterchangeKey { get; set; }

        public string CheckNumber { get; set; }

        public string FileType { get; set; }
    }
}