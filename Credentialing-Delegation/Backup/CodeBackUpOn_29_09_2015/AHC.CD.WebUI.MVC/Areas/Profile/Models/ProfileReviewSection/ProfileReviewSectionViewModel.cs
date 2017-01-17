using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.ProfileReviewSection
{
    public class ProfileReviewSectionViewModel
    {
        public int ProfileReviewSectionID { get; set; }

        public int ProfileSectionID { get; set; }

        public int ProfileID { get; set; }

        #region Display

        public DisplayType? DisplayType { get; set; }

        #endregion  

        #region Status

        public StatusType? StatusType { get; set; } 

        #endregion  
    }
}