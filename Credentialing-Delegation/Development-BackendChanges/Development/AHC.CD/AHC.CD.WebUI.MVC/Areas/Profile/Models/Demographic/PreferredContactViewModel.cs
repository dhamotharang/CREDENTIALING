using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.Demographic
{
    public class PreferredContactViewModel
    {
        public PreferredContactViewModel()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int? PreferredContactID { get; set; }

        #region ContactType

        //[Required]
        public PreferredContactType PreferredWrittenContactType { get; set; }

        #endregion  

        #region Status

        public StatusType StatusType { get; set; }

        #endregion

        //[Required]
        public int PreferredIndex { get; set; }

        public DateTime? LastModifiedDate { get; set; }
    }
}