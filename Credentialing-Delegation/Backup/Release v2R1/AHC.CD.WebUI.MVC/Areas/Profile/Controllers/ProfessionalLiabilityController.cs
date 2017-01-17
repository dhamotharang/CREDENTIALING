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
using AHC.CD.Business.Notification;
using AHC.CD.Entities.Notification;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class ProfessionalLiabilityController : Controller
    {
        // GET: Profile/ProfessionalLiability
        private IProfileManager profileManager = null;
        private IErrorLogger errorLogger = null;
        private IChangeNotificationManager notificationManager;


        public ProfessionalLiabilityController(IProfileManager profileManager, IErrorLogger errorLogger, IChangeNotificationManager notificationManager)
        {
            this.profileManager = profileManager;
            this.errorLogger = errorLogger;
            this.notificationManager = notificationManager;
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

                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Professional Liability Details", "Added");
                    await notificationManager.SaveNotificationDetailAsync(notification);

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

                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Professional Liability Details", "Updated");
                    await notificationManager.SaveNotificationDetailAsync(notification);

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

        [HttpPost]
        public async Task<ActionResult> RenewProfessionalLiabilityAsync(int profileId, AHC.CD.WebUI.MVC.Areas.Profile.Models.ProfessionalLiability.ProfessionalLiabilityInfoViewModel professionalLiability)
        {
            string status = "true";
            ProfessionalLiabilityInfo dataModelProfessionalLiability = null;

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelProfessionalLiability = AutoMapper.Mapper.Map<ProfessionalLiabilityInfoViewModel, ProfessionalLiabilityInfo>(professionalLiability);
                    DocumentDTO document = CreateDocument(professionalLiability.InsuranceCertificateFile);
                    //DocumentDTO document = null;

                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Professional Liability Details", "Renewed");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    await profileManager.RenewProfessionalLiabilityAsync(profileId, dataModelProfessionalLiability, document);
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
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> RemoveProfessionalLiability(int profileId, ProfessionalLiabilityInfoViewModel professionalLiability)
        {
            string status = "true";
            ProfessionalLiabilityInfo dataModelProfessionalLiability = null;

            try
            {
                dataModelProfessionalLiability = AutoMapper.Mapper.Map<ProfessionalLiabilityInfoViewModel, ProfessionalLiabilityInfo>(professionalLiability);
                //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Other Legal Name Details", "Removed");
                //await notificationManager.SaveNotificationDetailAsync(notification);

                await profileManager.RemoveProfessionalLiabilityAsync(profileId, dataModelProfessionalLiability);
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
                status = ExceptionMessage.HOME_ADDRESS_REMOVE_EXCEPTION;
            }

            return Json(new { status = status, professionalLiability = dataModelProfessionalLiability }, JsonRequestBehavior.AllowGet);
        }

        #region Private Methods

        private DocumentDTO CreateDocument(HttpPostedFileBase file, bool isRemoved = false)
        {
            DocumentDTO document = new DocumentDTO() { IsRemoved = isRemoved };
            if (file != null)
            {
                document.FileName = file.FileName;
                document.InputStream = file.InputStream;
            }

            return document;
        }

        #endregion
    }
}