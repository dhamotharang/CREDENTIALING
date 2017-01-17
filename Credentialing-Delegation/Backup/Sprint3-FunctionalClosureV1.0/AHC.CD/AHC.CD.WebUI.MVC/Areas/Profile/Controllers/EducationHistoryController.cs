using AHC.CD.Business;
using AHC.CD.Business.DocumentWriter;
using AHC.CD.Entities.MasterProfile.EducationHistory;
using AHC.CD.ErrorLogging;
using AHC.CD.Exceptions;
using AHC.CD.Resources.Messages;
using AHC.CD.WebUI.MVC.Areas.Profile.Attributes;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.EducationHistory;
using AHC.CD.WebUI.MVC.CustomHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class EducationHistoryController : Controller
    {
         private IProfileManager profileManager = null;
        private IErrorLogger errorLogger = null;

        public EducationHistoryController(IProfileManager profileManager, IErrorLogger errorLogger)
        {
            this.profileManager = profileManager;
            this.errorLogger = errorLogger;
        }

        #region Education Details

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> AddEducationDetailAsync(int profileId, EducationDetailViewModel educationDetails)
        {
            string status = "true";
            EducationDetail education = null;
           
            try
            {
                if (ModelState.IsValid)
                {
                    education = AutoMapper.Mapper.Map<EducationDetailViewModel, EducationDetail>(educationDetails);
                    DocumentDTO educationDocument = CreateDocument(educationDetails.CertificateDocumentFile);
                    await profileManager.AddEducationDetailAsync(profileId, education, educationDocument);
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

            return Json(new { status = status, educationDetails = education }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> UpdateEducationDetailAsync(int profileId, EducationDetailViewModel educationDetails)
        {
            string status = "true";
            EducationDetail education = null;

            try
            {
                if (ModelState.IsValid)
                {
                    education = AutoMapper.Mapper.Map<EducationDetailViewModel, EducationDetail>(educationDetails);
                    DocumentDTO document = CreateDocument(educationDetails.CertificateDocumentFile);
                    await profileManager.UpdateEducationDetailAsync(profileId, education, document);
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

            return Json(new { status = status, educationDetails = education }, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region ECFMG Details

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> UpdateECFMGDetailAsync(int profileId, ECFMGDetailViewModel ecfmgDetails)
        {
            string status = "true";
            ECFMGDetail ecfmg = null;

            try
            {
                if (ModelState.IsValid)
                {
                    ecfmg = AutoMapper.Mapper.Map<ECFMGDetailViewModel, ECFMGDetail>(ecfmgDetails);
                    DocumentDTO document = CreateDocument(ecfmgDetails.ECFMGCertificateDocumentFile);
                    await profileManager.UpdateECFMGDetailAsync(profileId, ecfmg, document);
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

            return Json(new { status = status, ecfmgDetails = ecfmg }, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region Residency/Internship/Fellowship Details

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> AddTrainingDetailAsync(int profileId, TrainingDetailViewModel TrainingDetails)
        {
            string status = "true";
            TrainingDetail training = null;   
            try
            {
                if (ModelState.IsValid)
                {
                    training = AutoMapper.Mapper.Map<TrainingDetailViewModel, TrainingDetail>(TrainingDetails);
                    await profileManager.AddTrainingDetailAsync(profileId, training, null);
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

            return Json(new { status = status, TrainingDetails = training }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> UpdateTrainingDetailAsync(int profileId, TrainingDetailViewModel TrainingDetails)
        {
            string status = "true";
            TrainingDetail training = null;

            try
            {
                if (ModelState.IsValid)
                {
                    training = AutoMapper.Mapper.Map<TrainingDetailViewModel, TrainingDetail>(TrainingDetails);                    
                    await profileManager.UpdateTrainingDetailAsync(profileId, training);
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

            return Json(new { status = status, TrainingDetails = training }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //[AjaxAction]
        //[ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> AddResidencyInternshipDetailAsync(int profileId, int trainingId, ResidencyInternshipDetailViewModel ResidencyDetails)
        {
            string status = "true";
            ResidencyInternshipDetail residency = null;

            try
            {
                var data = Request;
                if (ModelState.IsValid)
                {
                    residency = AutoMapper.Mapper.Map<ResidencyInternshipDetailViewModel, ResidencyInternshipDetail>(ResidencyDetails);
                    DocumentDTO document = CreateDocument(ResidencyDetails.ResidecncyCertificateDocumentFile);
                    await profileManager.AddResidencyInternshipDetailAsync(profileId, trainingId, residency, document);
                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }            
            catch (ApplicationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                //Log it

                status = ExceptionMessage.PROFILE_ADD_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, ResidencyDetails = residency }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> UpdateResidencyInternshipDetailAsync(int profileId,int trainingId, ResidencyInternshipDetailViewModel ResidencyDetails)
        {
            string status = "true";
            ResidencyInternshipDetail residency = null;

            try
            {
                if (ModelState.IsValid)
                {
                    residency = AutoMapper.Mapper.Map<ResidencyInternshipDetailViewModel, ResidencyInternshipDetail>(ResidencyDetails);
                    DocumentDTO document = CreateDocument(ResidencyDetails.ResidecncyCertificateDocumentFile);
                    await profileManager.UpdateResidencyInternshipDetailAsync(profileId, trainingId, residency, document);
                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }           
            catch (ApplicationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                //Log it

                status = ExceptionMessage.PROFILE_ADD_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, ResidencyDetails = residency }, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region PostGraduate Training/CME Details

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> AddCMECErtificationAsync(int profileId, CMECertificationViewModel CMEDetails)
        {
            string status = "true";
            CMECertification CME = null;

            try
            {
                if (ModelState.IsValid)
                {
                    CME = AutoMapper.Mapper.Map<CMECertificationViewModel, CMECertification>(CMEDetails);
                    DocumentDTO document = CreateDocument(CMEDetails.CertificateDocumentFile);
                    await profileManager.AddCMECertificationAsync(profileId, CME, document);
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

            return Json(new { status = status, CMEDetails = CME }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> UpdateCMECertificationAsync(int profileId, CMECertificationViewModel CMEDetails)
        {
            string status = "true";
            CMECertification CME = null;

            try
            {
                if (ModelState.IsValid)
                {
                    CME = AutoMapper.Mapper.Map<CMECertificationViewModel, CMECertification>(CMEDetails);
                    DocumentDTO document = CreateDocument(CMEDetails.CertificateDocumentFile);
                    await profileManager.UpdateCMECertificationAsync(profileId, CME, document);
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

            return Json(new { status = status, CMEDetails = CME }, JsonRequestBehavior.AllowGet);
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