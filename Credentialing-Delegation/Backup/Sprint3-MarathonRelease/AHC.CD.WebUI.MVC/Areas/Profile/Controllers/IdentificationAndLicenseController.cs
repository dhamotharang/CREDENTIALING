using AHC.CD.Business;
using AHC.CD.Business.DocumentWriter;
using AHC.CD.Entities.MasterProfile.IdentificationAndLicenses;
using AHC.CD.ErrorLogging;
using AHC.CD.Exceptions;
using AHC.CD.Resources.Messages;
using AHC.CD.WebUI.MVC.Areas.Profile.Attributes;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses;
using AHC.CD.WebUI.MVC.CustomHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class IdentificationAndLicenseController : Controller
    {
        private IProfileManager profileManager = null;
        private IErrorLogger errorLogger = null;

        public IdentificationAndLicenseController(IProfileManager profileManager, IErrorLogger errorLogger)
        {
            this.profileManager = profileManager;
            this.errorLogger = errorLogger;
        }

        #region State License

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add,false)]
        public async Task<ActionResult> AddStateLicenseAsync(int profileId, AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses.StateLicenseViewModel stateLicense)
        {
            string status = "true";
            StateLicenseInformation dataModelStateLicense = null;

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelStateLicense = AutoMapper.Mapper.Map<StateLicenseViewModel, StateLicenseInformation>(stateLicense);
                    DocumentDTO document = CreateDocument(stateLicense.StateLicenseDocumentFile);
                    await profileManager.AddStateLicenseAsync(profileId, dataModelStateLicense, document);
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

            return Json(new { status = status, stateLicense = dataModelStateLicense }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> UpdateStateLicenseAsync(int profileId, AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses.StateLicenseViewModel stateLicense)
        {
            string status = "true";
            StateLicenseInformation dataModelStateLicense = null;

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelStateLicense = AutoMapper.Mapper.Map<StateLicenseViewModel, StateLicenseInformation>(stateLicense);
                    DocumentDTO document = CreateDocument(stateLicense.StateLicenseDocumentFile);
                    await profileManager.UpdateStateLicenseAsync(profileId, dataModelStateLicense, document);
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

            return Json(new { status = status, stateLicense = dataModelStateLicense }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> RenewStateLicenseAsync(int profileId, AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses.StateLicenseViewModel stateLicense)
        {
            string status = "true";
            StateLicenseInformation dataModelStateLicense = null;

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelStateLicense = AutoMapper.Mapper.Map<StateLicenseViewModel, StateLicenseInformation>(stateLicense);
                    //DocumentDTO document = CreateDocument(stateLicense.File);
                    DocumentDTO document = null;
                    await profileManager.RenewStateLicenseAsync(profileId, dataModelStateLicense, document);
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
//                status = ExceptionMessage.STATE_LICENSE_RENEW_EXCEPTION;
            }

            return Json(new { status = status, stateLicense = dataModelStateLicense }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Federal DEA

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> AddFederalDEALicenseAsync(int profileId, FederalDEAInformationViewModel federalDea)
        {
            string status = "true";
            FederalDEAInformation dataModelFederalDEA = null;

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelFederalDEA = AutoMapper.Mapper.Map<FederalDEAInformationViewModel, FederalDEAInformation>(federalDea);
                    DocumentDTO document = CreateDocument(federalDea.DEALicenceCertFile);

                   await profileManager.AddFederalDEALicenseAsync(profileId, dataModelFederalDEA, document);
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
               
                //status = ExceptionMessage.FEDERAL_DEA_LICENSE_CREATE_EXCEPTION;
            }

            return Json(new { status = status, federalDea = dataModelFederalDEA }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> UpdateFederalDEALicenseAsync(int profileId, FederalDEAInformationViewModel federalDea)
        {
            string status = "true";
            FederalDEAInformation dataModelFederalDEA = null;

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelFederalDEA = AutoMapper.Mapper.Map<FederalDEAInformationViewModel, FederalDEAInformation>(federalDea);
                    DocumentDTO document = CreateDocument(federalDea.DEALicenceCertFile);

                    await profileManager.UpdateFederalDEALicenseAsync(profileId, dataModelFederalDEA, document);
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
            catch(ApplicationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                errorLogger.LogError(ex);
                status = ExceptionMessage.PROFILE_ADD_UPDATE_EXCEPTION;
//                status = ExceptionMessage.FEDERAL_DEA_LICENSE_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, federalDea = dataModelFederalDEA }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> RenewFederalDEALicenseAsync(int profileId, FederalDEAInformationViewModel federalDea)
        {
            string status = "true";
            FederalDEAInformation dataModelFederalDEA = null;

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelFederalDEA = AutoMapper.Mapper.Map<FederalDEAInformationViewModel, FederalDEAInformation>(federalDea);
                    //DocumentDTO document = CreateDocument(stateLicense.File);
                    DocumentDTO document = null;
                    await profileManager.RenewFederalDEALicenseAsync(profileId, dataModelFederalDEA, document);
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
                //              status = ExceptionMessage.FEDERAL_DEA_LICENSE_RENEW_EXCEPTION;
            }

            return Json(new { status = status, federalDea = dataModelFederalDEA }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region CDSCInformation

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> AddCDSCLicenseAsync(int profileId, AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses.CDSCInformationViewModel CDSCInformation)
        {
            string status = "true";
            CDSCInformation dataModelCDSCLicense = null;

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelCDSCLicense = AutoMapper.Mapper.Map<CDSCInformationViewModel, CDSCInformation>(CDSCInformation);
                   
                    DocumentDTO document = CreateDocument(CDSCInformation.CDSCCerificateFile);

                    await profileManager.AddCDSCLicenseAsync(profileId, dataModelCDSCLicense, document);
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
//                status = ExceptionMessage.CDSC_LICENSE_CREATE_EXCEPTION;
            }

            return Json(new { status = status, CDSCInformation = dataModelCDSCLicense }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> UpdateCDSCLicenseAsync(int profileId, AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses.CDSCInformationViewModel CDSCInformation)
        {
            string status = "true";
            CDSCInformation dataModelCDSCLicense = null;

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelCDSCLicense = AutoMapper.Mapper.Map<CDSCInformationViewModel, CDSCInformation>(CDSCInformation);
                    DocumentDTO document = CreateDocument(CDSCInformation.CDSCCerificateFile);

                    await profileManager.UpdateCDSCLicenseAsync(profileId, dataModelCDSCLicense, document);
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
//                status = ExceptionMessage.CDSC_LICENSE_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, CDSCInformation = dataModelCDSCLicense }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> RenewCDSCLicenseAsync(int profileId, AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses.CDSCInformationViewModel CDSCInformation)
        {
            string status = "true";
            CDSCInformation dataModelCDSCLicense = null;

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelCDSCLicense = AutoMapper.Mapper.Map<CDSCInformationViewModel, CDSCInformation>(CDSCInformation);
                    DocumentDTO document = CreateDocument(CDSCInformation.CDSCCerificateFile);

                    await profileManager.RenewCDSCLicenseAsync(profileId, dataModelCDSCLicense, document);
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
//                status = ExceptionMessage.CDSC_LICENSE_RENEW_EXCEPTION;
            }

            return Json(new { status = status, CDSCInformation = dataModelCDSCLicense }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Medicare Information

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> AddMedicareInformationAsync(int profileId, AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses.MedicareInformationViewModel MedicareInformation)
        {
            string status = "true";
            MedicareInformation dataModelMedicareInformation = null;

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelMedicareInformation = AutoMapper.Mapper.Map<MedicareInformationViewModel, MedicareInformation>(MedicareInformation);
             DocumentDTO document = CreateDocument(MedicareInformation.CertificateFile);

                    await profileManager.AddMedicareInformationAsync(profileId, dataModelMedicareInformation, document);
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
//                status = ExceptionMessage.MEDICARE_CREATE_EXCEPTION;
            }

            return Json(new { status = status, MedicareInformation = dataModelMedicareInformation }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> UpdateMedicareInformationAsync(int profileId, AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses.MedicareInformationViewModel MedicareInformation)
        {
            string status = "true";
            MedicareInformation dataModelMedicareInformation = null;

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelMedicareInformation = AutoMapper.Mapper.Map<MedicareInformationViewModel, MedicareInformation>(MedicareInformation);
                    DocumentDTO document = CreateDocument(MedicareInformation.CertificateFile);
                   
                    await profileManager.UpdateMedicareInformationAsync(profileId, dataModelMedicareInformation, document);
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
//                status = ExceptionMessage.MEDICARE_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, MedicareInformation = dataModelMedicareInformation }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Medicaid Information

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> AddMedicaidInformationAsync(int profileId, AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses.MedicaidInformationViewModel MedicaidInformation)
        {
            string status = "true";
            MedicaidInformation dataModelMedicaidInformation = null;

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelMedicaidInformation = AutoMapper.Mapper.Map<MedicaidInformationViewModel, MedicaidInformation>(MedicaidInformation);
                    DocumentDTO document = CreateDocument(MedicaidInformation.CertificateFile);
                    await profileManager.AddMedicaidInformationAsync(profileId, dataModelMedicaidInformation, document);
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
//                status = ExceptionMessage.MEDICAID_CREATE_EXCEPTION;
            }

            return Json(new { status = status, MedicaidInformation = dataModelMedicaidInformation }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> UpdateMedicaidInformationAsync(int profileId, AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses.MedicaidInformationViewModel MedicaidInformation)
        {
            string status = "true";
            MedicaidInformation dataModelMedicaidInformation = null;

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelMedicaidInformation = AutoMapper.Mapper.Map<MedicaidInformationViewModel, MedicaidInformation>(MedicaidInformation);
                    DocumentDTO document = CreateDocument(MedicaidInformation.CertificateFile);

                    await profileManager.UpdateMedicaidInformationAsync(profileId, dataModelMedicaidInformation, document);
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
//                status = ExceptionMessage.MEDICAID_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, MedicaidInformation = dataModelMedicaidInformation }, JsonRequestBehavior.AllowGet);
        }

        #endregion
     
        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, true)]
        public async Task<ActionResult> UpdateOtherIdentificationNumberAsync(int profileId, AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses.OtherIdentificationNumberViewModel OtherIdentificationNumber)
        {
            string status = "true";
            OtherIdentificationNumber dataModelOtherIdentificationNumber = null;

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelOtherIdentificationNumber = AutoMapper.Mapper.Map<OtherIdentificationNumberViewModel, OtherIdentificationNumber>(OtherIdentificationNumber);
                    await profileManager.UpdateOtherIdentificationNumberAsync(profileId, dataModelOtherIdentificationNumber);
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
//                status = ExceptionMessage.OTHER_IDENTIFICATION_NUMBER_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, OtherIdentificationNumber = dataModelOtherIdentificationNumber }, JsonRequestBehavior.AllowGet);
        }
         
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