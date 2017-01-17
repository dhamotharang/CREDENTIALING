using AHC.CD.Entities.MasterData.Enums;
using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.Demographic
{
    public class PhoneDetailViewModel
    {
        public PhoneDetailViewModel()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int PhoneDetailID { get; set; }

        [Required(ErrorMessage = "Please enter the number.")]
        [RegularExpression(@"\d{10}", ErrorMessage = "Please enter only 10 digit numbers.")]
        public string Number { get; set; }

        [Required]
        public string CountryCode { get; set; }

        #region PhoneType

        [Display(Name = "PhoneType *")]
        public string PhoneType
        {
            get
            {
                return this.PhoneTypeEnum.ToString();
            }
            private set
            {
                this.PhoneTypeEnum = (PhoneTypeEnum)Enum.Parse(typeof(PhoneTypeEnum), value);
            }
        }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Select a PhoneType!!")]
        [Display(Name = "PhoneType *")]
        public PhoneTypeEnum PhoneTypeEnum { get; set; }

        #endregion


        #region Preference

        public string Preference 
        {
            get
            {
                return this.PreferenceType.ToString();
            }
            private set
            {
                this.PreferenceType = (PreferenceType)Enum.Parse(typeof(PreferenceType), value);
            }
        }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Select a PhoneType!!")]
        [Display(Name = "Preference *")]
        public PreferenceType PreferenceType { get; set; }
        
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

        public ICollection<ContactDetailViewModel> ContactDetails { get; set; }
    }
}