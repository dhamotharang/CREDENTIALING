using AHC.CD.Business;
using AHC.CD.Business.DocumentWriter;
using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterProfile.Demographics;
using AHC.CD.ErrorLogging;
using AHC.CD.Exceptions;
using AHC.CD.Resources.Messages;
using AHC.CD.WebUI.MVC.Areas.Profile.Attributes;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.Demographic;
using AHC.CD.WebUI.MVC.CustomHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class DemographicController : Controller
    {
        private IProfileManager profileManager = null;
        private IErrorLogger errorLogger = null;

        public DemographicController(IProfileManager profileManager, IErrorLogger errorLogger)
        {
            this.profileManager = profileManager;
            this.errorLogger = errorLogger;
        }        
        
        #region Personal Detail

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> UpdatePersonalDetailsAsync(int profileId, PersonalDetailViewModel personalDetail)
        {
            string status = "true";
            PersonalDetail dataModelPersonalDetail = null;

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelPersonalDetail = AutoMapper.Mapper.Map<PersonalDetailViewModel, PersonalDetail>(personalDetail);
                    await profileManager.UpdatePersonalDetailAsync(profileId, dataModelPersonalDetail);
                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                errorLogger.LogError(ex);
                status = ExceptionMessage.PROFILE_ADD_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, personalDetail = dataModelPersonalDetail }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region File Upload

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> FileUploadAsync(int profileId, ProfilePictureViewModel ProfilePic)
        {
            string status = "true";
            string newPath = "";
            try
            {
                if (ModelState.IsValid)
                {
                    DocumentDTO document = CreateDocument(ProfilePic.ProfilePictureFile);
                    newPath = await profileManager.UpdateProfileImageAsync(profileId, document);
                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                errorLogger.LogError(ex);
                status = ExceptionMessage.PROFILE_ADD_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, ProfileImagePath = newPath }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region File Remove

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> FileRemoveAsync(int profileId, string imagePath)
        {
            string status = "true";
            try
            {
                if (ModelState.IsValid)
                {
                    await profileManager.RemoveProfileImageAsync(profileId, imagePath);
                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                errorLogger.LogError(ex);
                status = ExceptionMessage.PROFILE_ADD_UPDATE_EXCEPTION;
            }

            return Json(new { status = status}, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Other Legal Name

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> AddOtherLegalNameAsync(int profileId, OtherLegalNameViewModel otherLegalName)
        {
            string status = "true";
            OtherLegalName dataModelOtherLegalName = null;

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelOtherLegalName = AutoMapper.Mapper.Map<OtherLegalNameViewModel, OtherLegalName>(otherLegalName);
                    DocumentDTO document = CreateDocument(otherLegalName.File);
                    var result = await profileManager.AddOtherLegalNamesAsync(profileId, dataModelOtherLegalName, document);
                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                errorLogger.LogError(ex);
                status = ExceptionMessage.PROFILE_ADD_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, otherLegalName = dataModelOtherLegalName }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> UpdateOtherLegalNameAsync(int profileId, OtherLegalNameViewModel otherLegalName)
        {
            string status = "true";
            OtherLegalName dataModelOtherLegalName = null;

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelOtherLegalName = AutoMapper.Mapper.Map<OtherLegalNameViewModel, OtherLegalName>(otherLegalName);
                    DocumentDTO document = CreateDocument(otherLegalName.File);
                    await profileManager.UpdateOtherLegalNamesAsync(profileId, dataModelOtherLegalName, document);
                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                errorLogger.LogError(ex);
                status = ExceptionMessage.PROFILE_ADD_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, otherLegalName = dataModelOtherLegalName }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Home Address

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, true)]
        public async Task<ActionResult> AddHomeAddressAsync(int profileId, HomeAddressViewModel homeAddress)
        {
            string status = "true";
            HomeAddress dataModelHomeAddress = null;

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelHomeAddress = AutoMapper.Mapper.Map<HomeAddressViewModel, HomeAddress>(homeAddress);
                    var result = await profileManager.AddHomeAddressAsync(profileId, dataModelHomeAddress);
                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                errorLogger.LogError(ex);
                status = ExceptionMessage.PROFILE_ADD_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, homeAddress = dataModelHomeAddress }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, true)]
        public async Task<ActionResult> UpdateHomeAddressAsync(int profileId, HomeAddressViewModel homeAddress)
        {
            string status = "true";
            HomeAddress dataModelHomeAddress = null;

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelHomeAddress = AutoMapper.Mapper.Map<HomeAddressViewModel, HomeAddress>(homeAddress);
                    await profileManager.UpdateHomeAddressAsync(profileId, dataModelHomeAddress);
                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                errorLogger.LogError(ex);
                status = ExceptionMessage.PROFILE_ADD_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, homeAddress = dataModelHomeAddress }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Contact Detail

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> UpdateContactDetailsAsync(int profileId, ContactDetailViewModel contactDetail)
        {
            string status = "true";
            ContactDetail dataModelContactDetail = null;

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelContactDetail = AutoMapper.Mapper.Map<ContactDetailViewModel, ContactDetail>(contactDetail);
                    await profileManager.UpdateContactDetailAsync(profileId, dataModelContactDetail);
                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                errorLogger.LogError(ex);
                status = ExceptionMessage.PROFILE_ADD_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, contactDetail = dataModelContactDetail }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Personal Identification

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, true)]
        public async Task<ActionResult> UpdatePersonalIdentificationAsync(int profileId, PersonalIdentificationViewModel personalIdentification)
        {
            string status = "true";
            PersonalIdentification dataModelPersonalIdentification = null;

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelPersonalIdentification = AutoMapper.Mapper.Map<PersonalIdentificationViewModel, PersonalIdentification>(personalIdentification);
                    DocumentDTO dlDocument = CreateDocument(personalIdentification.DLCertificateFile);
                    DocumentDTO ssnDocument = CreateDocument(personalIdentification.SSNCertificateFile);
                    await profileManager.UpdatePersonalIdentificationAsync(profileId, dataModelPersonalIdentification, dlDocument, ssnDocument);
                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                errorLogger.LogError(ex);
                status = ExceptionMessage.PROFILE_ADD_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, personalIdentification = dataModelPersonalIdentification }, JsonRequestBehavior.AllowGet);
        }  

        #endregion

        #region Birth Information

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> UpdateBirthInformationAsync(int profileId, BirthInformationViewModel birthInformation)
        {
            string status = "true";
            BirthInformation dataModelBirthInformation = null;

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelBirthInformation = AutoMapper.Mapper.Map<BirthInformationViewModel, BirthInformation>(birthInformation);
                    DocumentDTO document = CreateDocument(birthInformation.BirthCertificateFile);
                    await profileManager.UpdateBirthInformationAsync(profileId, dataModelBirthInformation, document);
                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                errorLogger.LogError(ex);
                status = ExceptionMessage.PROFILE_ADD_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, birthInformation = dataModelBirthInformation }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Visa Details

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, true)]
        public async Task<ActionResult> UpdateVisaDetailAsync(int profileId, VisaDetailViewModel visaDetail)
        {
            string status = "true";
            VisaDetail dataModelVisaDetail = null;

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelVisaDetail = AutoMapper.Mapper.Map<VisaDetailViewModel, VisaDetail>(visaDetail);
                    DocumentDTO visaDocument = null;
                    DocumentDTO greenCarddocument = null;
                    DocumentDTO nationalIDdocument = null;
                    if (visaDetail.VisaInfo != null)
                    {
                        visaDocument = CreateDocument(visaDetail.VisaInfo.VisaCertificateFile);
                        greenCarddocument = CreateDocument(visaDetail.VisaInfo.GreenCardCertificateFile);
                        nationalIDdocument = CreateDocument(visaDetail.VisaInfo.NationalIDCertificateFile);
                    }
                    await profileManager.UpdateVisaInformationAsync(profileId, dataModelVisaDetail, visaDocument, greenCarddocument, nationalIDdocument);
                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                errorLogger.LogError(ex);
                status = ExceptionMessage.PROFILE_ADD_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, visaDetail = dataModelVisaDetail }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Language

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> UpdateLanguagesAsync(int profileId, LanguageInfoViewModel languageInfo)
        {
            string status = "true";
            LanguageInfo dataModelLanguageInfo = null;

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelLanguageInfo = AutoMapper.Mapper.Map<LanguageInfoViewModel, LanguageInfo>(languageInfo);
                    await profileManager.UpdateLanguageInformationAsync(profileId, dataModelLanguageInfo);
                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                errorLogger.LogError(ex);
                status = ExceptionMessage.PROFILE_ADD_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, languageInfo = dataModelLanguageInfo }, JsonRequestBehavior.AllowGet);
        }

        #endregion        

        #region Private Methods

        private DocumentDTO CreateDocument(HttpPostedFileBase file)
        {
            DocumentDTO document = null;
            if (file != null)
                document = ConstructDocumentDTO(file.FileName, file.InputStream);
            return document;
        }

        private DocumentDTO ConstructDocumentDTO(string fileName, System.IO.Stream stream)
        {
            return new DocumentDTO() { FileName = fileName, InputStream = stream };
        }

        #endregion
    }
}