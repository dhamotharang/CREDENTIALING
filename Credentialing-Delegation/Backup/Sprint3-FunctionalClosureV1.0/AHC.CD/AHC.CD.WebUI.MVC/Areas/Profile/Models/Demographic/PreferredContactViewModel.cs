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

        public int PreferredContactID { get; set; }

        #region ContactType

        public string ContactType
        {
            get
            {
                return this.PreferredWrittenContactType.ToString();
            }
            private set
            {
                this.PreferredWrittenContactType = (PreferredContactType)Enum.Parse(typeof(PreferredContactType), value);
            }
        }

        //[Required]
        public PreferredContactType PreferredWrittenContactType { get; set; }

        #endregion  

        #region Status

        [Display(Name = "Status")]
        public string Status
        {
            get
            {
                return this.StatusType.ToString();
            }
            private set
            {
                this.StatusType = (StatusType)Enum.Parse(typeof(StatusType), value);
            }
        }

        public StatusType StatusType { get; set; }

        #endregion

        //[Required]
        public int PreferredIndex { get; set; }

        public DateTime LastModifiedDate { get; set; }
    }
}