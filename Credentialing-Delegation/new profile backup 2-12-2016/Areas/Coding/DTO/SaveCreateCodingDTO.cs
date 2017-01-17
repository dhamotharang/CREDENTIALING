using PortalTemplate.Areas.Coding.Models.CreateCoding;
using PortalTemplate.Areas.Coding.Models.ICDCodes;
using PortalTemplate.Areas.Coding.Models.ICDCPTMapping;
using PortalTemplate.Areas.Coding.Models.Notes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Coding.DTO
{
    public class SaveCreateCodingDTO
    {
        public int EncounterId { get; set; }
        public bool IsICD10 { get; set; }
        public List<ICDCodeViewModel> ICDCodes { get; set; }
        public List<ICDCPTCodemappingViewModel> CPTCodes { get; set; }
        public List<CodingNotesViewModel> CodingNotes { get; set; }
        public string Status { get; set; }
       
    }
}