using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Encounters.Models.CreateEncounter
{
    public class CodeDetailsViewModel
    {
        public PrimaryEncounterViewModel EncounterDetails { get; set; }


        public PortalTemplate.Areas.Coding.Models.CreateCoding.CodingDetailsViewModel CodingDetails { get; set; }

        //public ICDCodeDetailsViewModel ICDCodeDetails { get; set; }

        //public CPTCodeDetailsViewModel CPTCodeDetails { get; set; }
    }
}