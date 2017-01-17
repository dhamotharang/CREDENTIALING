using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.SharedView.Models.Notes
{
    public class EncounterNotesViewModel
    {
        public string Title { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Module { get; set; }
        public string Notify { get; set; }
        public string AddedBy { get; set; }
        public DateTime AddedOn { get; set; }
    }
}