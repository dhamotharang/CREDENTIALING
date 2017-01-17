using AHC.CD.Entities.MasterData.Enums;
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
        public EmailDetailViewModel()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int EmailDetailID { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "{0} must be between {2} and {1} characters.")]
        [RegularExpression(@"^[a-z]+[a-z0-9._]+@[a-z]+\.[a-z.]{2,5}$", ErrorMessage = "Invalid Email-Id!!")]
        [Index(IsUnique = true)]
        public string EmailAddress { get; set; }

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
        [Range(1, int.MaxValue, ErrorMessage = "Select a Preference Type!!")]
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