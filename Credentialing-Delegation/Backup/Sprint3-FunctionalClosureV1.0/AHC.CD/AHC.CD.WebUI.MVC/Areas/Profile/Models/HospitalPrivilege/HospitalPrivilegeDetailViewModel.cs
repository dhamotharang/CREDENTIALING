using AHC.CD.Entities.MasterData.Enums;
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

        [Required(ErrorMessage = "Please enter the Hospital Name.")]
        [Display(Name = "Hospital Name *")]
        public int HospitalID { get; set; }
        
        #region Preference
        [Required(ErrorMessage = "Please select the type.")]
        [Display(Name = "Primary *")]
        public PreferenceType PreferenceType  { get; set; }

        #endregion

        #region StaffCategory

        [Required(ErrorMessage = "Please select the Staff Category.")]
        [Display(Name = "Staff Category *")]
        public int StaffCategoryID { get; set; }        

        #endregion        

        
        [Display(Name = "Department Name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Department Name should not exceed 50 characters.")]
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "Please enter valid Department Name. Only characters accepted.")]
        public string DepartmentName { get; set; }

        
        [Display(Name = "Department Chief")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Department Chief Name should not exceed 50 characters.")]
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "Please enter valid Department Chief. Only characters accepted.")]
        public string DepartmentChief { get; set; }

        #region AdmittingPrivilege

     
        [Required]
        [Display(Name = "Admitting Privilege Status *")]
        public int AdmittingPrivilegeID { get; set; }
        
        #endregion
        
      
        [Display(Name = "Annual Admission Percentage")]
        [Range(0.00, 100.00)]
        [RegularExpression(@"(^[0-9]\d{0,4}(\.\d{1,1})?%?)\d{0,1}$", ErrorMessage = "Please enter valid Annual Admission Percentage.Only numbers and decimal accepted.")]
        public double ? AnnualAdmisionPercentage { get; set; }

        #region ArePrevilegesTemporary

        
        
        [Display(Name = "Are Privileges Temporary?")]
        public YesNoOption ? PrevilegesTemporaryYesNoOption { get; set; }

        #endregion

        #region FullUnrestrictedPrevilages



        [Required(ErrorMessage = "Please select the Full Unrestricted privileges.")]
        [Display(Name = "Full Unrestricted Privileges *")]
        public YesNoOption FullUnrestrictedPrevilagesYesNoOption { get; set; }
        #endregion

        #region Specialty

        [Required(ErrorMessage = "Please select the Specialty.")]
        [Display(Name = "Specialty *")]
        public int SpecialtyID { get; set; }
        

        #endregion
        [Required(ErrorMessage = "Please enter Affiliation Start Date.")]
        [Display(Name = "Affiliation Start Date *")]
        [Column(TypeName = "datetime2")]
        [DateStart(MaxPastYear = "0", MinPastYear = "-50", ErrorMessage = "Start Date should fall between 50 years from now!!")]
        public DateTime AffilicationStartDate { get; set; }

        [Display(Name = "Affiliation End Date")]
        [Column(TypeName = "datetime2")]
        [DateEnd(DateStartProperty = "AffilicationStartDate", ErrorMessage = "Affiliation End Date should be Greater than start date.")]
        [DateStart(MaxPastYear = "50", MinPastYear = "0", ErrorMessage = "End Date should fall between 50 years from now!!")]
        public DateTime ? AffiliationEndDate { get; set; }
       
        
        [Display(Name = "Hospital Privilege Letter")]
        public string HospitalPrevilegeLetterPath { get; set; }

        
        [Display(Name = "Hospital Privilege Letter")]
        //[RequiredIf("HospitalPrevilegeLetterPath", "", ErrorMessage = "Upload a supporting Document")]
        [PostedFileExtension(AllowedFileExtensions = "pdf,jpeg,png,jpg,bmp", ErrorMessage = "Please select the file of type .pdf, .jpeg, .png, .jpg, .bmp .")]
        [PostedFileSize(AllowedSize = 10485760, ErrorMessage = "Hospital Privilege letter should be less than 10mb in size.")]
        public HttpPostedFileBase HospitalPrevilegeLetterFile { get; set; }

        #region HospitalContactInfo

        [Required]
        [Display(Name = "Hospital Location *")]
        public int HospitalContactInfoID { get; set; }
        
        #endregion

        #region HospitalContactPerson

        [Required(ErrorMessage = "Please select the Contact Person.")]
        [Display(Name = "Contact Person *")]
        public int HospitalContactPersonID { get; set; }
        
        #endregion
    }
}
