using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using AHC.CD.Resources.Messages;
using AHC.CD.Resources.Rules;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ValidtionAttribute;
using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.ProfessionalLiability
{
    public class ProfessionalLiabilityInfoViewModel
    {
          
        public int ProfessionalLiabilityInfoID { get; set; }

        #region InsuranceCarrier

        [Required]
        [Display(Name = "Insurance Carrier *")]
        public int InsuranceCarrierID { get; set; }

        #endregion

        #region Insurance Carrier Address

        [Required]
        [Display(Name = "Insurance Carrier Address *")]
        public int InsuranceCarrierAddressID { get; set; }
      
        #endregion

        [Required(ErrorMessage= ValidationErrorMessage.REQUIRED_ENTER)]
        [RegularExpression(@"[a-zA-Z0-9]*$", ErrorMessage = "Please Enter valid Policy number only alpha-numeric characters can be used.")]
        [Display(Name = "Policy Number *")]
        public string PolicyNumber { get; set; }

        #region SelfInsured


        public string SelfInsured { get; set; }

        [Display(Name="Self Insured?")]
        //[Required(ErrorMessage = "Please Specify if the insurance is Self-Insured?")]
        public YesNoOption? SelfInsuredYesNoOption { get; set; }
   

        #endregion

        [Column(TypeName = "datetime2")]
        [Display(Name = "Original Effective Date")]
        //[DateStart(MaxPastYear = "0", MinPastYear = "-100", ErrorMessage = "Original Effective Date should fall between 10 years from now!!")]
        //[RegularExpression(RegularExpression.FOR_DATE_FORMAT, ErrorMessage = ValidationErrorMessage.FOR_DATE_FORMAT)]
        public DateTime? OriginalEffectiveDate { get; set; }

        [Column(TypeName = "datetime2")]
        //[Required(ErrorMessage=ValidationErrorMessage.REQUIRED_ENTER)]
        [Display(Name = "Effective Date")]
        [DateEnd(DateStartProperty = "OriginalEffectiveDate", MaxYear = "100", IsRequired = false, ErrorMessage = ValidationErrorMessage.STOP_EFFECTIVEDATE_RANGE)]
        //[RegularExpression(RegularExpression.FOR_DATE_FORMAT, ErrorMessage = ValidationErrorMessage.FOR_DATE_FORMAT)]
        public DateTime? EffectiveDate { get; set; }

        [Column(TypeName = "datetime2")]
        //[Required(ErrorMessage= ValidationErrorMessage.REQUIRED_ENTER)]
        [Display(Name = "Expiration Date")]
        //[GreaterThan("EffectiveDate", ErrorMessage = "Expiration date should be greater than Effective date")]
        [DateEnd(DateStartProperty = "EffectiveDate", IsRequired = false, ErrorMessage = "Expiration date should be greater than Effective date")]
        //[RegularExpression(RegularExpression.FOR_DATE_FORMAT, ErrorMessage = ValidationErrorMessage.FOR_DATE_FORMAT)]
        public DateTime? ExpirationDate { get; set; }

        #region CoverageType

       

        [Display(Name = "Type Of Coverage ?")]
        //[Required(ErrorMessage = "Please select the Type of coverage.")]
        public InsuranceCoverageType? InsuranceCoverageType { get; set; }

        #endregion

        #region UnlimitedCoverageWithInsuranceCarrier

   
        [Display(Name = "Unlimited Coverage With Insurance Carrier")]
        public YesNoOption? UnlimitedCoverageWithInsuranceCarrierOption { get; set; }
     

        #endregion
        [Display(Name = " Amount Of Coverage Per Occurrence")]
        [RegularExpression(@"[0-9]*[.]{0,1}[0-9]{1,2}$", ErrorMessage = "Please enter valid Amount of coverage per Occurrence. Only whole number or decimals upto two decimal places  accepted.")]
        public double? AmountOfCoveragePerOccurance { get; set; }

         [Display(Name = " Amount Of Coverage Aggregate")]
         [RegularExpression(@"[0-9]*[.]{0,1}[0-9]{1,2}$", ErrorMessage = "Please enter valid Amount of coverage aggregate.  Only whole number or decimals upto two decimal places accepted.")]
        public double? AmountOfCoverageAggregate { get; set; }

         [Display(Name = "Phone Number")]
         //[Required(ErrorMessage=ValidationErrorMessage.REQUIRED_ENTER )]
         [RegularExpression(@"^\d+$", ErrorMessage = "Mobile number must be only number")]
         [MaxLength(10, ErrorMessage = "Mobile number should be 10 characters")]
         [MinLength(10, ErrorMessage = "Mobile number should be 10 characters")]
        public string Phone { get; set; }

        public string PhoneCountryCode { get; set; }


        #region PolicyIncludesTailCoverage

           [Display(Name = "Policy Includes Tail Coverage")]
         public YesNoOption? PolicyIncludesTailCoverageOption { get; set; }
     

        #endregion

        public string InsuranceCertificatePath { get; set; }

        [PostedFileExtension(AllowedFileExtensions = "pdf,jpeg,jpg,png,bmp,PNG,JPEG,PDF,JPG,BMP", ErrorMessage = "Please select the file of type .pdf, jpeg, .png, .jpg, .bmp")]
//        [PostedFileExtension(AllowedFileExtensions = "pdf,jpeg,png,jpg,bmp", ErrorMessage = "Upload pdf, jpeg, jpg, png, bmp files only")]
        [PostedFileSize(AllowedSize = 10485760, ErrorMessage = "File too large..!!")]
        public HttpPostedFileBase InsuranceCertificateFile { get; set;}

        #region Status

        public string Status { get; private set; }


        public StatusType? StatusType { get; set; }
  

        #endregion
    }
}