using AHC.CD.Entities.MasterData.Account;
using AHC.CD.Entities.MasterData.Enums;
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
    public class ContractGroupInfoViewModel
    {
        
        public int ContractGroupInfoId { get; set; }

        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        public int PracticingGroupId { get; set; }

        [Display(Name = "Start Date *")]
        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        ////[RegularExpression(RegularExpression.FOR_DATE_FORMAT, ErrorMessage = ValidationErrorMessage.FOR_DATE_FORMAT)]
        public DateTime? JoiningDate { get; set; }

        [Display(Name = "End Date")]
        [DateEnd(DateStartProperty = "JoiningDate", IsRequired = false, IsGreaterThan = true, ErrorMessage = ValidationErrorMessage.DATE_GREATER_THAN_START_DATE)]
        ////[RegularExpression(RegularExpression.FOR_DATE_FORMAT, ErrorMessage = ValidationErrorMessage.FOR_DATE_FORMAT)]
        public DateTime? ExpiryDate { get; set; }

        public string ContractGroupCerificatePath { get; set; }

        #region Contract Group Status
      
        [Range(1, int.MaxValue, ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [Display(Name="Status *")]
        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        public ContractGroupStatus? ContractGroupStatusOption { get; set; }

        #endregion        

        public StatusType? StatusType { get; set; }

        public ICollection<ContractGroupInfoHistoryViewModel> ContractGroupInfoHistory { get; set; }

        public string UpdateHistory { get; set; }
    }
}