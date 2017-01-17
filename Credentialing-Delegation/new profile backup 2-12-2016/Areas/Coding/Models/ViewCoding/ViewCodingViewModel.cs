using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PortalTemplate.Areas.SharedView.Models.Encounter;
using PortalTemplate.Areas.Coding.Models.ICDCPTMapping;

namespace PortalTemplate.Areas.Coding.Models.ViewCoding
{
    public class ViewCodingViewModel
    {
        public ViewCodingViewModel()
        {
            this.ICDCodes = new List<ICDCodes.ICDCodeViewModel>();
            this.CPTCodes = new List<ICDCPTCodemappingViewModel>();
            this.EncounterDetails = new EncounterDetailsViewModel();
        }

    public List<ICDCodes.ICDCodeViewModel> ICDCodes { get; set; }
    public List<ICDCPTCodemappingViewModel> CPTCodes { get; set; }
    public EncounterDetailsViewModel EncounterDetails { get; set; }
    }
}