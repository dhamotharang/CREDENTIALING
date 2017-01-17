using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AHC.CD.Entities.MasterData.Enums;
using System.ComponentModel.DataAnnotations;
using AHC.CD.Resources.Messages;
using AHC.CD.Resources.Rules;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.PracticeLocationInformation
{
    public class FacilityServiceViewModel
    {
        public int FacilityServiceID { get; set; }

        [Display(Name = "Laboratory Services?")]
        public YesNoOption? LaboratoryServicesYesNoOption { get; set; }

        [Display(Name = "Provide Accrediting/Certifying Program")]
        [StringLength(500, MinimumLength = 2, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        [RegularExpression(@"^[a-zA-Z0-9- ]*$", ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_ALPHABETS_NUMBERS_SPACE_HYPHEN)]
        public string LaboratoryAccreditingCertifyingProgram { get; set; }

        [Display(Name = "Radiology Services?")]
        public YesNoOption? RadiologyServicesYesNoOption { get; set; }

        [Display(Name = "Provide X-Ray Certificate Type")]
        [StringLength(500, MinimumLength = 2, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        [RegularExpression(@"^[a-zA-Z0-9- ]*$", ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_ALPHABETS_NUMBERS_SPACE_HYPHEN)]
        public string RadiologyXRayCertificateType { get; set; }

        public ICollection<FacilityServiceQuestionAnswerViewModel> FacilityServiceQuestionAnswers { get; set; }

        [Display(Name = "Is Anesthesia Administered In Your Office?")]
        public YesNoOption? IsAnesthesiaAdministeredYesNoOption { get; set; }

        [Display(Name = "What Class/Category Do You Use")]
        [StringLength(500, MinimumLength = 2, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        public string AnesthesiaCategory { get; set; }

        [Display(Name = "First Name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_ALPHABETS_SPACE_COMMA_HYPHEN_DOT)]
        public string AnesthesiaAdminFirstName { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_ALPHABETS_SPACE_COMMA_HYPHEN_DOT)]
        public string AnesthesiaAdminLastName { get; set; }

        [Display(Name = "Type Of Practice?")]
        public int? PracticeTypeID { get; set; }

        [Display(Name = "Additional Office Procedures Provided (Including Surgical Procedures)")]
        [StringLength(500, MinimumLength = 2, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        [RegularExpression(RegularExpression.FOR_ALPHABETS_NUMBER_SPACE_COMMA_HYPHEN_DOT, ErrorMessage = ValidationErrorMessage.ALPHABETS_NUMBER_SPACE_COMMA_HYPHEN_DOT)]
        public string AdditionalOfficeProcedures { get; set; }
    }
}
