using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Resources.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.Demographic
{
    public class EmailDetailViewModel
    {
        public int EmailDetailID { get; set; }

        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        //[StringLength(50, MinimumLength = 6, ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        [DataType(DataType.EmailAddress, ErrorMessage = ValidationErrorMessage.INVALID_EMAIL_FORMAT)]
        //[RegularExpression(@"^[a-z]+[a-z0-9._]+@[a-z]+\.[a-z.]{2,5}$", ErrorMessage = "Invalid Email-Id!!")]
        [Display(Name="Email Id")]
        public string EmailAddress { get; set; }

        #region Preference

        //public string Preference
        //{
        //    get
        //    {
        //        return this.PreferenceType.ToString();
        //    }
        //    private set
        //    {
        //        this.PreferenceType = (PreferenceType)Enum.Parse(typeof(PreferenceType), value);
        //    }
        //}

        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [Range(1, int.MaxValue, ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [Display(Name = "Email Preference")]
        public PreferenceType PreferenceType { get; set; }

        #endregion


        #region Status

        //public string Status
        //{
        //    get
        //    {
        //        return this.StatusType.ToString();
        //    }
        //    private set
        //    {
        //        this.StatusType = (StatusType)Enum.Parse(typeof(StatusType), value);
        //    }
        //}

        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [Range(1, int.MaxValue, ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [Display(Name = "Status")]
        public StatusType StatusType { get; set; }

        #endregion 
    }
}