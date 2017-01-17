using AHC.CD.Business;
using AHC.CD.Entities.MasterProfile.PracticeLocation;
using AHC.CD.ErrorLogging;
using AHC.CD.Exceptions;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.PracticeLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class PracticeLocationController : Controller
    {
        private IProfileManager profileManager = null;
        private IErrorLogger errorLogger = null;

        public PracticeLocationController(IProfileManager profileManager, IErrorLogger errorLogger)
        {
            this.profileManager = profileManager;
            this.errorLogger = errorLogger;
        }

        // GET: Profile/PracticeLocation
        public ActionResult Index()
        {
            return View();
        }

        #region Workers Compensation Information

        [HttpPost]
        public async Task<ActionResult> UpdateWorkersCompensationInformationAsync(int profileId, WorkersCompensationInformationViewModel workersCompensationInformation)
        {
            string status = "true";

            WorkersCompensationInformation dataModelWorkersCompensationInformation = null;

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelWorkersCompensationInformation = AutoMapper.Mapper.Map<WorkersCompensationInformationViewModel, WorkersCompensationInformation>(workersCompensationInformation);

                   // await profileManager.UpdateWorkersCompensationInformationAsync(profileId, dataModelWorkersCompensationInformation);
                }
                else
                {
                    status = String.Join(", ", ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage));
                }
            }
            catch (DatabaseValidationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (Exception ex)
            {
                errorLogger.LogError(ex);
            }

            return Json(new { status = status, workersCompensationInformation = dataModelWorkersCompensationInformation }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Primary Credentialing Contact

        [HttpPost]
        public async Task<ActionResult> UpdateCredentialingContactAsync(int profileId, PrimaryCredentialingContactViewModel credentialingContact)
        {
            string status = "true";

            PrimaryCredentialingContact dataModelcredentialingContact = null;

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelcredentialingContact = AutoMapper.Mapper.Map<PrimaryCredentialingContactViewModel, PrimaryCredentialingContact>(credentialingContact);

                    // await profileManager.UpdateWorkersCompensationInformationAsync(profileId, dataModelWorkersCompensationInformation);
                }
                else
                {
                    status = String.Join(", ", ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage));
                }
            }
            catch (DatabaseValidationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (Exception ex)
            {
                errorLogger.LogError(ex);
            }

            return Json(new { status = status, credentialingContact = dataModelcredentialingContact }, JsonRequestBehavior.AllowGet);
        }

        #endregion
        
    }
}