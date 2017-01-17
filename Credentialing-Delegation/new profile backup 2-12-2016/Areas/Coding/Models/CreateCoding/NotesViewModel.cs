using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PortalTemplate.Areas.Coding.Models.Notes;

namespace PortalTemplate.Areas.Coding.Models.CreateCoding
{
    public class NotesViewModel
    {
        public List<CodingNotesViewModel> CodingNotes { get; set; }
    }
}