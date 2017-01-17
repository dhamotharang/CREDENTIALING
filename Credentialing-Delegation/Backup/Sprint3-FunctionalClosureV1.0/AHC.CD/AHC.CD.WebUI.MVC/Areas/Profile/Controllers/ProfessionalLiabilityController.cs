using AHC.CD.Business;using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AHC.CD.Entities.MasterProfile.ProfessionalLiability;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ProfessionalLiability;
using System.Web.Mvc;
using System.Threading.Tasks;
using AHC.CD.Business.DocumentWriter;
using AHC.CD.Resources.Messages;
using AHC.CD.Exceptions;
using AHC.CD.ErrorLogging;
using AHC.CD.WebUI.MVC.CustomHelpers;
using AHC.CD.WebUI.MVC.Areas.Profile.Attributes;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class ProfessionalLiabilityController : Controller
    {
        // GET: Profile/ProfessionalLiability
        private IProfileManager profileManager = null;
        private IErrorLogger errorLogger = null;

        public ProfessionalLiabilityController(IProfileManager profileManager, IErrorLogger errorLogger)
        {
            this.profileManager = profileManager;
            this.errorLogger = errorLogger;
        }       
       

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add,true)]
        public async Task<ActionResult> AddProfessionalLiabilityAsync(int profileId, AHC.CD.WebUI.MVC.Areas.Profile.Models.ProfessionalLiability.ProfessionalLiabilityInfoViewModel professionalLiability)
        {
            string status = "true";
            ProfessionalLiabilityInfo dataModelProfessionalLiability = null;

            try
            {

                if (ModelState.IsValid)
                {
                    dataModelProfessionalLiability = AutoMapper.Mapper.Map<ProfessionalLiabilityInfoViewModel, ProfessionalLiabilityInfo>(professionalLiability);
                    DocumentDTO document = CreateDocument(professionalLiability.InsuranceCertificateFile);
                    await profileManager.AddProfessionalLiabilityAsync(profileId, dataModelProfessionalLiability, document);

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

            return Json(new { status = status, professionalLiability = dataModelProfessionalLiability }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit,true)]
        public async Task<ActionResult> UpdateProfessionalLiabilityAsync(int profileId, AHC.CD.WebUI.MVC.Areas.Profile.Models.ProfessionalLiability.ProfessionalLiabilityInfoViewModel professionalLiability)
        {
            string status = "true";
            ProfessionalLiabilityInfo dataModelProfessionalLiability = null;

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelProfessionalLiability = AutoMapper.Mapper.Map<ProfessionalLiabilityInfoViewModel, ProfessionalLiabilityInfo>(professionalLiability);
                    DocumentDTO document = CreateDocument(professionalLiability.InsuranceCertificateFile);
                    await profileManager.UpdateProfessionalLiabilityAsync(profileId, dataModelProfessionalLiability, document);
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

            return Json(new { status = status, professionalLiability = dataModelProfessionalLiability }, JsonRequestBehavior.AllowGet);
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