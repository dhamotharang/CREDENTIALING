using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Areas.Coding.Models.CreateCoding
{
    public class CreateCodingViewModel
    {
        public CreateCodingViewModel()
        {
            this.EncounterDetails = new EncounterViewModel();
            this.CodingDetails = new CodingDetailsViewModel();
        }
        public CodingDetailsViewModel CodingDetails { get; set; }
        public EncounterViewModel EncounterDetails { get; set; }
    }
}