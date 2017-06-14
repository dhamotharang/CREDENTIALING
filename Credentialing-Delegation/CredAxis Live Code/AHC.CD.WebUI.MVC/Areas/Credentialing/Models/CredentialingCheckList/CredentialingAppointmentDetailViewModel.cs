using AHC.CD.Entities.MasterData.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AHC.CD.Entities.MasterData.Enums;
using System.ComponentModel.DataAnnotations;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ValidtionAttribute;
using AHC.CD.Resources.Rules;
using AHC.CD.Resources.Messages;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Models.CredentialingCheckList
{
    public class CredentialingAppointmentDetailViewModel
    {
        public int CredentialingAppointmentDetailID { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public AppointmentProviderType? AppointmentProviderType { get; set; }

        public string County { get; set; }

        public int? SpecialtyID { get; set; }

        public YesNoOption? BoardCertifiedYesNoOption { get; set; }

        public string RemarksForBoardCertification { get; set; }

        public YesNoOption? HospitalPrivilegesYesNoOption { get; set; }

        public string RemarksForHospitalPrivileges { get; set; }

        public YesNoOption? GapsInPracticeYesNoOption { get; set; }

        public string RemarksForGapsInPractice { get; set; }

        public YesNoOption? CleanLicenseYesNoOption { get; set; }

        public string RemarksForCleanLicense { get; set; }

        public YesNoOption? NPDBIssueYesNoOption { get; set; }

        public string RemarksForNPDBIssue { get; set; }

        public YesNoOption? MalpracticeIssueYesNoOption { get; set; }

        public string RemarksForMalpracticeIssue { get; set; }

        [RegularExpression(RegularExpression.FOR_NUMBERS, ErrorMessage = "Please enter valid Years in Practice. Only Numeric Digits accepted.")]
        public string YearsInPractice { get; set; }

        public YesNoOption? SiteVisitRequiredYesNoOption { get; set; }

        public string RemarksForSiteVisitRequired { get; set; }

        public CredentialingLevel? RecommendedCredentialingLevel { get; set; }

        public StatusType? StatusType { get; set; }

        public ICollection<CredentialingCoveringPhysicianViewModel> CredentialingCoveringPhysicians { get; set; }

        public CredentialingAppointmentResultViewModel CredentialingAppointmentResult { get; set; }

        public ICollection<CredentialingSpecialityListViewModel> CredentialingSpecialityLists { get; set; }

        public CredentialingAppointmentScheduleViewModel CredentialingAppointmentSchedule { get; set; }

        public string FileUploadPath { get; set; }

        //[Required(ErrorMessage = "Please select Document.")]
        [PostedFileExtension(AllowedFileExtensions = "jpeg,jpg,png,bmp,PNG,JPEG,JPG,BMP,pdf,doc,xls,PDF,DOC,XLS,docx,gif,GIF,DOCX", ErrorMessage = "Please select the file of type jpeg, .png, .jpg, .bmp, .pdf, .xls, .doc")]
        [PostedFileSize(AllowedSize = 10485760, ErrorMessage = "Document should be less than 10 MB of size.")]
        public HttpPostedFileBase FileUpload { get; set; }

        public int? AssignedToCCOID { get; set; }

        public string CCOName { get; set; }

        public string WelcomeLetterpath { get; set; }

        public DateTime? WelcomeLetterMailedDate { get; set; }

        public string ServiceCommencingDate { get; set; }

        public DateTime? WelcomeLetterPreparedDate { get; set; }


    }
}