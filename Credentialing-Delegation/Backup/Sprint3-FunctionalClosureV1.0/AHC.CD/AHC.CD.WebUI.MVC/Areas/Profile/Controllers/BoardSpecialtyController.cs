using AHC.CD.Business;
using AHC.CD.Business.DocumentWriter;
using AHC.CD.Entities.MasterProfile.BoardSpecialty;
using AHC.CD.ErrorLogging;
using AHC.CD.Exceptions;
using AHC.CD.Resources.Messages;
using AHC.CD.WebUI.MVC.Areas.Profile.Attributes;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.BoardSpecialty;
using AHC.CD.WebUI.MVC.CustomHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class BoardSpecialtyController : Controller
    {
        private IProfileManager profileManager = null;
        private IErrorLogger errorLogger = null;

        public BoardSpecialtyController(IProfileManager profileManager, IErrorLogger errorLogger)
        {
            this.profileManager = profileManager;
            this.errorLogger = errorLogger;
        }

        #region Specialty

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> AddSpecialityDetailAsync(int profileId, SpecialtyDetailViewModel specialty)
        {
            string status = "true";
            SpecialtyDetail dataModelSpecialty = null;

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelSpecialty = AutoMapper.Mapper.Map<SpecialtyDetailViewModel, SpecialtyDetail>(specialty);
                    DocumentDTO document = null;
                    if(specialty.SpecialtyBoardCertifiedDetail != null)
                    { 
                        document = CreateDocument(specialty.SpecialtyBoardCertifiedDetail.BoardCertificateDocumentFile);
                    }
                    await profileManager.AddSpecialtyDetailAsync(profileId, dataModelSpecialty, document);
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

            return Json(new { status = status, specialty = dataModelSpecialty }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> UpdateSpecialityDetailAsync(int profileId, SpecialtyDetailViewModel specialty)
        {
            string status = "true";
            SpecialtyDetail dataModelSpecialty = null;

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelSpecialty = AutoMapper.Mapper.Map<SpecialtyDetailViewModel, SpecialtyDetail>(specialty);
                    DocumentDTO document = null;
                    if (specialty.SpecialtyBoardCertifiedDetail != null)
                    {
                        document = CreateDocument(specialty.SpecialtyBoardCertifiedDetail.BoardCertificateDocumentFile);
                    }
                    await profileManager.UpdateSpecialtyDetailAsync(profileId, dataModelSpecialty, document);
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

            return Json(new { status = status, specialty = dataModelSpecialty }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        public async Task<ActionResult> RenewSpecialityDetailAsync(int profileId, SpecialtyDetailViewModel specialty)
        {
            string status = "true";
            SpecialtyDetail dataModelSpecialty = null;

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelSpecialty = AutoMapper.Mapper.Map<SpecialtyDetailViewModel, SpecialtyDetail>(specialty);
                    DocumentDTO document = null;
                    if (specialty.SpecialtyBoardCertifiedDetail != null)
                    {
                        document = CreateDocument(specialty.SpecialtyBoardCertifiedDetail.BoardCertificateDocumentFile);
                    }
                    await profileManager.RenewSpecialtyDetailAsync(profileId, dataModelSpecialty, document);
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

            return Json(new { status = status, specialty = dataModelSpecialty }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Practice Interest

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> UpdatePracticeInterestAsync(int profileId, PracticeInterestViewModel practiceInterest)
        {
            string status = "true";
            PracticeInterest dataModelPracticeInterest = null;

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelPracticeInterest = AutoMapper.Mapper.Map<PracticeInterestViewModel, PracticeInterest>(practiceInterest);
                    await profileManager.UpdatePracticeInterestAsync(profileId, dataModelPracticeInterest);
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

            return Json(new { status = status, practiceInterest = dataModelPracticeInterest }, JsonRequestBehavior.AllowGet);
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