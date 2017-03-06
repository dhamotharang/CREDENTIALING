using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Plans.Models
{
    public class PreferredContactsViewModel
    {
        public PreferredContactsViewModel()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int? PreferredContactID { get; set; }

        #region ContactType

        public PreferredContactType PreferredWrittenContactType { get; set; }

        #endregion  

        #region Status

        public StatusType StatusType { get; set; }

        #endregion

        public int PreferredIndex { get; set; }

        public DateTime? LastModifiedDate { get; set; }
    }
}