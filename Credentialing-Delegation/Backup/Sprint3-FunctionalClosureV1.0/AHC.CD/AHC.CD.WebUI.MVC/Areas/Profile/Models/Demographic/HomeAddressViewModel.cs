using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ValidtionAttribute;
using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.Demographic
{
    public class HomeAddressViewModel
    {
        public HomeAddressViewModel()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int HomeAddressID { get; set; }

        [Display(Name = "Apartment/Unit Number *")]
        [Required(ErrorMessage = "Please enter the Apartment/Unit Number.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Apartment/Unit Number must be between {2}-{1} characters.")]
        public string UnitNumber { get; set; }

        [Display(Name = "Street Address *")]
        [Required(ErrorMessage = "Please enter the Street Address.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Street Address must be between {2}-{1} characters.")]
        public string Street { get; set; }

        [Display(Name = "State *")]
        [Required(ErrorMessage = "Please enter the State.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "State must be between {2}-{1} characters.")]
        public string State { get; set; }

        [Display(Name = "City *")]
        [Required(ErrorMessage = "Please enter the City.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "City must be between {2}-{1} characters.")]
        public string City { get; set; }

        [Display(Name = "County")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "County must be between {2}-{1} characters.")]
        public string County { get; set; }

        [Display(Name = "Country *")]
        [Required(ErrorMessage = "Please enter the Country.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Country must be between {2}-{1} characters.")]
        public string Country { get; set; }

        //[Required]
        [Display(Name = "Zip Code")]
        [RegularExpression(@"^[a-zA-Z0-9.-]*$", ErrorMessage = "Please enter valid Zip code. Only Alphabets, numbers, dot and hyphen accepted.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Country must be between {2}-{1} characters.")]
        public string ZipCode { get; set; }

        #region IsPresentlyStaying

        [Display(Name = "Is Present Home Address ?")]
        public string IsPresentlyStaying 
        {
            get
            {
                return this.IsPresentlyStayingYesNoOption.ToString();
            }
            private set
            {
                this.IsPresentlyStayingYesNoOption = (YesNoOption)Enum.Parse(typeof(YesNoOption), value);
            }
        }

        //[Required]
        [Display(Name = "Is Present Home Address ?")]
        public YesNoOption IsPresentlyStayingYesNoOption { get; set; }

        #endregion

        [Display(Name = "Living Here From *")]
        [Required(ErrorMessage = "Please specify since when have you been Living Here.")]
        [DateStart(MaxPastYear = "0", MinPastYear = "-100", ErrorMessage = "Living Here From Date should not be greater than current date.")]
        public DateTime? LivingFromDate { get; set; }

        [Display(Name = "Living Here To *")]
        [RequiredIf("IsPresentlyStayingYesNoOption", (int)YesNoOption.NO, ErrorMessage = "Please specify till when you Lived Here.")]
        [DateEnd(DateStartProperty = "LivingFromDate", MaxYear = "0", ErrorMessage = "Living Here To Date should be less than current date And should be greater than Living Here From Date.")]
        public DateTime? LivingEndDate { get; set; }

        #region AddressPreference

        [Display(Name = "Is Primary Home Address ?")]
        public string AddressPreference
        {
            get
            {
                return this.AddressPreferenceType.ToString();
            }
            private set
            {
                this.AddressPreferenceType = (PreferenceType)Enum.Parse(typeof(PreferenceType), value);
            }
        }

        //[Required]
        [Display(Name = "Is Primary Home Address ?")]
        public PreferenceType AddressPreferenceType { get; set; }

        #endregion

        #region Status

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

        [Required]
        public StatusType StatusType { get; set; }
        
        #endregion  

        public DateTime LastModifiedDate { get; set; }
    }

    public partial class Notice { public DateTime Today { get { return DateTime.Today; } } }
}
