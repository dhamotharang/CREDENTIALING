using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Models
{
    public class LocationViewModel
    {
        public int LocationID { get; set; }

        public string LocationCode { get; set; }

        public string Stree { get; set; }

        public string UnitNumber { get; set; }

        public string State { get; set; }

        public string County { get; set; }

        public string ZipCode { get; set; }

        public string Country { get; set; }
    }
}
