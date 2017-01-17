using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.MasterDataEntities
{
    public class ContactOutcomeViewModel
    {
        public int ContactOutcomeID { get; set; }

        public int? OutComeID { get; set; }
        [ForeignKey("OutComeID")]
        public OutcomeViewModel Outcome { get; set; }

        public int? ContactEntityTypeID { get; set; }
        [ForeignKey("ContactEntityTypeID")]
        public ContactEntityTypeViewModel ContactEntityType { get; set; }

        public int? ContactTypeID { get; set; }
        [ForeignKey("ContactTypeID")]
        public ContactTypeViewModel ContactType { get; set; }

        public int? OutcomeTypeID { get; set; }
        [ForeignKey("OutcomeTypeID")]
        public OutcomeTypeViewModel OutcomeTypes { get; set; }
    }
}