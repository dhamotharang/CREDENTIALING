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
        public int PersonalDetailID { get; set; }

        [NotMapped]
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Select a Title!!")]
        [Display(Name = "Title *")]
        public virtual TitleType TitleType { get; set; }

        [Required]
        [Display(Name = "First Name *")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "{0} must be between {2} and {1} characters.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage="Only Alphabets Accepted!!")]
        public virtual string FirstName
        {
            get;
            set;
        }

        [Display(Name = "Middle Name")]
        [StringLength(50, ErrorMessage = "{0} must be of max {1} characters.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only Alphabets Accepted!!")]
        public virtual string MiddleName
        {
            get;
            set;
        }

        [Required]
        [Display(Name = "Last Name *")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "{0} must be between {2} and {1} characters.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only Alphabets Accepted!!")]
        public virtual string LastName
        {
            get;
            set;
        }

        [Display(Name = "Suffix (JR,III)")]
        public string Suffix { get; set; }

        
        public string Gender { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Select a Gender!!")]
        [Display(Name = "Gender *")]
        public GenderType GenderType { get; set; }

        [Display(Name = "Maiden Name")]
        [StringLength(50, ErrorMessage = "{0} must be of max {1} characters.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only Alphabets Accepted!!")]
        [RequiredIf("GenderType", (int)GenderType.Female, ErrorMessage = "Maiden Name is required!!")]
        public string MaidenName { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Select a Marital Status!!")]
        [Display(Name = "Marital Status")]
        public MaritalStatusType MaritalStatusType { get; set; }

        [Display(Name = "Spouse Name")]
        [StringLength(50, ErrorMessage = "{0} must be of max {1} characters.")]
        [RegularExpression(@"^[a-zA-Z ,-]+$", ErrorMessage = "Only Alphabets Accepted!!")]
        [RequiredIf("MaritalStatusType", (int)MaritalStatusType.Married, ErrorMessage = "Spouse Name is required!!")]
        public string SpouseName { get; set; }

        public string ProfilePhotoPath { get; set; }

        public HttpPostedFileBase PhotoImage { get; set; }

        public bool HasOtherName { get; set; }
    }

    public enum TitleType
    {
        Mr = 1,
        Miss,
        Mrs,
        Dr
    }

    public enum MaritalStatusType
    {
        Married = 1,
        Unmarried,
        Divorced
    }

    public enum GenderType
    {
        Male=1,
        Female
    }
}
