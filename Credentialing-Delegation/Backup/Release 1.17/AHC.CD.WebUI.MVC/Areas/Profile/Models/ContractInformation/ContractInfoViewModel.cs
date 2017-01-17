using AHC.CD.Entities.MasterData.Account;
using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterProfile.Contract;
using AHC.CD.Resources.Messages;
using AHC.CD.Resources.Rules;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ValidtionAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.ContractInformation
{
    public class ContractInfoViewModel
    {
        public int ContractInfoID { get; set; }

        #region Provider Relationship

       

        [Display(Name = "Provider Relationship *")]
        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [Range(1, int.MaxValue, ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        public ProviderRelationshipOption? ProviderRelationshipOption { get; set; }

        #endregion

        public int OrganizationId { get; set; }

        [Display(Name = "Join Date *")]
        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        public DateTime? JoiningDate { get; set; }

        [Display(Name = "End Date")] 
        [DateEnd(DateStartProperty = "JoiningDate", IsRequired = false, IsGreaterThan = true, ErrorMessage = "End date should be greater than joining date")]
        public DateTime? ExpiryDate { get; set; }

        // ----------------cv and tax id location can change in future-----------

        [Display(Name = "Individual Tax ID")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_FIXED)]
        [RegularExpression(RegularExpression.ALPHA_NUMERIC, ErrorMessage = ValidationErrorMessage.ALPHA_NUMERIC)]
        [ProfileRemote("Validation", "IsIndividualTaxIDDoesNotExists", false, "ContractInfoID", ErrorMessage = "Individual Tax ID Used!!")]
        public string IndividualTaxId { get; set; }

        // ------------------------------ end ------------------------

        [Display(Name = "Contract Document")]
        public string ContractFilePath { get; set; }

        [Display(Name = "Contract Document")]
        [PostedFileExtension(AllowedFileExtensions = "pdf,jpeg,jpg,png,bmp", ErrorMessage = ValidationErrorMessage.UPLOAD_FILE_EXTENSION_ELIGIBLE)]
        [PostedFileSize(AllowedSize = 10485760, ErrorMessage = ValidationErrorMessage.UPLOAD_FILE_SIZE_ELIGIBLE)]
        public HttpPostedFileBase ContractFile { get; set; }

        #region Contract Status

        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [Range(1, int.MaxValue, ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [Display(Name="Status *")]
        public ContractStatus? ContractStatusOption { get; set; }

        #endregion

        [Display(Name = "Are you a part of any Group?")]
        public virtual ICollection<ContractGroupInfoViewModel> ContractGroupInfoes { get; set; }

    }
}