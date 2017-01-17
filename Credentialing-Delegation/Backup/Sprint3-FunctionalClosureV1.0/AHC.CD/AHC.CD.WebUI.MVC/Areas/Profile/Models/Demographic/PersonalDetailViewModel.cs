using AHC.CD.Entities.MasterData.Enums;
using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.Demographic
{
    public class PersonalDetailViewModel
    {
        public PersonalDetailViewModel()
        {
            LastUpdatedDateTime = DateTime.Now;
        }
        public int PersonalDetailID { get; set; }

        #region Salutation

        [Display(Name = "Salutation")]
        public string Salutation 
        {
            get
            {
                return this.SalutationType.ToString();
            }
            private set
            {
                this.SalutationType = (SalutationType)Enum.Parse(typeof(SalutationType), value);
            }
        }
        //[Required]
        //[Range(1, int.MaxValue, ErrorMessage = "Select a Salutation!!")]
        [Display(Name = "Salutation")]
        public SalutationType SalutationType { get; set; }
        
        #endregion

        [Required(ErrorMessage = "Please enter First Name.")]
        [Display(Name = "First Name *")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First Name must be between {2} and {1} characters.")]
        [RegularExpression(@"^[a-zA-Z ,-]*$", ErrorMessage = "Please enter valid First Name. Only characters, spaces, comma and hyphen accepted.")]
        public string FirstName { get; set; }
        
        [Display(Name = "Middle Name")]
        [StringLength(50, ErrorMessage = "Middle Name must be less than {1} characters.")]
        [RegularExpression(@"^[a-zA-Z ,-]*$", ErrorMessage = "Please enter valid Middle Name. Only characters, spaces, comma and hyphen accepted.")]
        public string MiddleName { get; set; }

        [Display(Name = "Last Name *")]
        [Required(ErrorMessage = "Please enter Last Name.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Last Name must be between {2} and {1} characters.")]
        [RegularExpression(@"^[a-zA-Z ,-]*$", ErrorMessage = "Please enter valid Last Name.(a-z, A-Z, space, comma, hyphen)")]
        public string LastName { get; set; }

        [Display(Name = "Suffix (JR,III)")]
        [StringLength(10, ErrorMessage = "Suffix must be less than {1} characters.")]
        [RegularExpression(@"^[a-zA-Z ,-]*$", ErrorMessage = "Please enter valid Suffix. Only characters, spaces, comma and hyphen accepted.")]
        public string Suffix { get; set; }

        #region Gender

        [Display(Name = "Gender")]
        public string Gender
        {
            get
            {
                return this.GenderType.ToString();
            }
            private set
            {
                this.GenderType = (GenderType)Enum.Parse(typeof(GenderType), value);
            }
        }

        [Required(ErrorMessage = "Please select the Gender.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select the Gender.")]
        [Display(Name = "Gender *")]
        public GenderType GenderType { get; set; }

        #endregion  
        
        [Display(Name = "Maiden Name *")]
        [RequiredIf("GenderType", (int)GenderType.Female, ErrorMessage = "Please enter Maiden Name.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Maiden Name must be between {2} and {1} characters.")]
        [RegularExpression(@"^[a-zA-Z ,-.]*$", ErrorMessage = "Please enter valid Maiden Name. Only characters, spaces, comma and hyphen accepted.")]
        public string MaidenName { get; set; }

        #region MaritalStatus

        [Display(Name = "Marital Status")]
        public string MaritalStatus
        {
            get
            {
                return this.MaritalStatusType.ToString();
            }
            private set
            {
                this.MaritalStatusType = (MaritalStatusType)Enum.Parse(typeof(MaritalStatusType), value);
            }
        }

        [Required(ErrorMessage = "Please select the Marital Status")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select the Marital Status")]
        [Display(Name = "Marital Status *")]
        public MaritalStatusType MaritalStatusType { get; set; }
        
        #endregion 

        [Display(Name = "Spouse Name *")]
        [RequiredIf("MaritalStatusType", (int)MaritalStatusType.Married, ErrorMessage = "Please enter Spouse Name.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Spouse Name must be between {2} and {1} characters.")]
        [RegularExpression(@"^[a-zA-Z ,-.]*$", ErrorMessage = "Please enter valid Spouse Name. Only characters, spaces, comma and hyphen accepted.")]
        public string SpouseName { get; set; }

        [Required(ErrorMessage = "Please select the Title.")]
        [Display(Name = "Title *")]
        public ICollection<ProviderTitleViewModel> ProviderTitles { get; set; }
        
        public DateTime LastUpdatedDateTime { get; private set; }
    }
}
