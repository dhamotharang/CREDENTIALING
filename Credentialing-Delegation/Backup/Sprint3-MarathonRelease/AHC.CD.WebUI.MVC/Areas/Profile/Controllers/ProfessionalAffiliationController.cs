using AHC.CD.Business;
using AHC.CD.Entities.MasterProfile.ProfessionalAffiliation;
using AHC.CD.ErrorLogging;
using AHC.CD.Exceptions;
using AHC.CD.Resources.Messages;
using AHC.CD.WebUI.MVC.Areas.Profile.Attributes;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ProfessionalAffiliation;
using AHC.CD.WebUI.MVC.CustomHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class ProfessionalAffiliationController : Controller
    {
        private IProfileManager profileManager = null;
        private IErrorLogger errorLogger = null;

        public ProfessionalAffiliationController(IProfileManager profileManager, IErrorLogger errorLogger)
	    {
            this.profileManager = profileManager;
            this.errorLogger = errorLogger;
	    }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add,false)]
        public async Task<ActionResult> AddProfessionalAffiliation(int profileId, ProfessionalAffiliationDetailViewModel professionalAffiliation)
        {
            string status = "true";
            ProfessionalAffiliationInfo dataModelProfessionalAffiliation = null;

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelProfessionalAffiliation = AutoMapper.Mapper.Map<ProfessionalAffiliationDetailViewModel, ProfessionalAffiliationInfo>(professionalAffiliation);
                await profileManager.AddProfessionalAffiliationAsync(profileId, dataModelProfessionalAffiliation);
                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch(DatabaseValidationException ex)
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

            return Json( new {status = status, professionalAffiliation = dataModelProfessionalAffiliation  }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> UpdateProfessionalAffiliation(int profileId, ProfessionalAffiliationDetailViewModel professionalAffiliation)
        {
            string status  = "true";
            ProfessionalAffiliationInfo dataModelProfessionalAffiliation = null;

            try
            {

                if (true)
                {
                    dataModelProfessionalAffiliation = AutoMapper.Mapper.Map<ProfessionalAffiliationDetailViewModel, ProfessionalAffiliationInfo>(professionalAffiliation);
                    await profileManager.UpdateProfessionalAffiliationAsync(profileId, dataModelProfessionalAffiliation);
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

            return Json(new { status = status, professionalAffiliation = dataModelProfessionalAffiliation }, JsonRequestBehavior.AllowGet);
        }
    }
}