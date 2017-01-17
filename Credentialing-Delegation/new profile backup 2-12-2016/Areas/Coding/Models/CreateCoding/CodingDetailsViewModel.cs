using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Coding.Models.CreateCoding
{
    public class CodingDetailsViewModel
    {
        public CodingDetailsViewModel()
        {
            this.CPTCodeDetails = new CPTCodeDetailsViewModel();
            this.ICDCodeDetails = new ICDCodeDetailsViewModel();
        }
        public ICDCodeDetailsViewModel ICDCodeDetails { get; set; }
        public CPTCodeDetailsViewModel CPTCodeDetails { get; set; }
        public NotesViewModel Notes { get; set; }
    }
}