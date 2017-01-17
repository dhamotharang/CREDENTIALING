using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.MasterDataEntities
{
    public class LetterTemplateViewModel
    {
        public int LetterTemplateID { get; set; }

        public string LetterTemplateName { get; set; }

        public string LetterTemplateDescription { get; set; }
    }
}