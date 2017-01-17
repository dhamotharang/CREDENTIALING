using PortalTemplate.Areas.Portal.Enums.Facility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Portal.Models.Facility
{
    public class NonEnglishLanguageViewModel
    {

        public int NonEnglishLanguageID { get; set; }

        public string Language { get; set; }

        public string InterpretersAvailable { get; set; }

        public YesNoOption InterpretersAvailableYesNoOption { get; set; }
    }
}