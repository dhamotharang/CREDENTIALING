using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Resources.Messages;
using AHC.CD.Resources.Rules;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ValidtionAttribute;
using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.HospitalPrivilege
{
    public class HospitalPrivilegeDetailViewModel
    {
        public int HospitalPrivilegeDetailID { get; set; }


        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        [Display(Name = "Hospital Name *")]
        public int HospitalID { get; set; }
        
        #region Preference
        [Required(ErrorMessage = "Please select the type.")]
        [Display(Name = "Primary *")]
        public PreferenceType PreferenceType  { get; set; }

        #endregion

        #region StaffCategory

        //[Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [Display(Name = "Staff Category")]
        public int? StaffCategoryID { get; set; }        

        #endregion        
        
        [Display(Name = "Department Name")]
        [StringLength(50, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX)]
        [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_ALPHABETS_SPACE_COMMA_HYPHEN)]
        public string DepartmentName { get; set; }
        
        [Display(Name = "Department Chief")]
        [StringLength(50, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX)]
        [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_ALPHABETS_SPACE_COMMA_HYPHEN)]
        public string DepartmentChief { get; set; }

        #region AdmittingPrivilege

        //[Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [Display(Name = "Admitting Privilege Status")]
        public int? AdmittingPrivilegeID { get; set; }
        
        #endregion        
      
        [Display(Name = "Annual Admission Percentage")]
        [Range(0.00, 100.00)]
        [RegularExpression(@"(^[0-9]\d{0,4}(\.\d{1,1})?%?)\d{0,1}$", ErrorMessage = "Please enter valid Annual Admission Percentage.Only numbers and decimal accepted.")]
        public double? AnnualAdmisionPercentage { get; set; }

        #region ArePrevilegesTemporary

        
        
        [Display(Name = "Are Privileges Temporary?")]
        public YesNoOption? PrevilegesTemporaryYesNoOption { get; set; }

        #endregion

        #region FullUnrestrictedPrevilages



        //[Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [Display(Name = "Full Unrestricted Privileges")]
        public YesNoOption? FullUnrestrictedPrevilagesYesNoOption { get; set; }
        #endregion

        #region Specialty

        //[Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [Display(Name = "Specialty")]
        public int? SpecialtyID { get; set; }
        

        #endregion

        //[Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        [Display(Name = "Affiliation Start Date")]
        //[Column(TypeName = "datetime2")]
        //[DateStart(MaxPastYear = "0", MinPastYear = "-50", ErrorMessage = ValidationErrorMessage.START_DATE_RANGE)]
        ////[RegularExpression(RegularExpression.FOR_DATE_FORMAT, ErrorMessage = ValidationErrorMessage.FOR_DATE_FORMAT)]
        public DateTime? AffilicationStartDate { get; set; }

        [Display(Name = "Affiliation End Date")]
        //[Column(TypeName = "datetime2")]
        [DateEnd(DateStartProperty = "AffilicationStartDate", IsRequired = false, IsGreaterThan = true, ErrorMessage = ValidationErrorMessage.DATE_GREATER_THAN_START_DATE)]
        //[DateStart(MaxPastYear = "50", MinPastYear = "0", ErrorMessage = ValidationErrorMessage.START_DATE_RANGE)]
        ////[RegularExpression(RegularExpression.FOR_DATE_FORMAT, ErrorMessage = ValidationErrorMessage.FOR_DATE_FORMAT)]
        public DateTime? AffiliationEndDate { get; set; }       
        
        [Display(Name = "Hospital Privilege Letter")]
        public string HospitalPrevilegeLetterPath { get; set; }
        
        [Display(Name = "Hospital Privilege Letter")]
        //[RequiredIf("HospitalPrevilegeLetterPath", "", ErrorMessage = "Upload a supporting Document")]
        [PostedFileExtension(AllowedFileExtensions = "pdf,jpeg,jpg,png,bmp,PNG,JPEG,PDF,JPG,BMP", ErrorMessage = "Please select the file of type .pdf, jpeg, .png, .jpg, .bmp")]
//        [PostedFileExtension(AllowedFileExtensions = "pdf,jpeg,png,jpg,bmp", ErrorMessage = ValidationErrorMessage.UPLOAD_FILE_EXTENSION_ELIGIBLE)]
        [PostedFileSize(AllowedSize = 10485760, ErrorMessage = ValidationErrorMessage.UPLOAD_FILE_SIZE_ELIGIBLE)]
        public HttpPostedFileBase HospitalPrevilegeLetterFile { get; set; }

        #region HospitalContactInfo

        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [Display(Name = "Hospital Location *")]
        public int HospitalContactInfoID { get; set; }
        
        #endregion

        #region HospitalContactPerson

        //[Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [Display(Name = "Contact Person")]
        public int? HospitalContactPersonID { get; set; }
        
        #endregion

        public StatusType? StatusType { get; set; }
    }
}
