using AHC.CD.Business;
using AHC.CD.Business.Notification;
using AHC.CD.Entities.MasterProfile.ProfessionalReference;
using AHC.CD.Entities.Notification;
using AHC.CD.ErrorLogging;
using AHC.CD.Exceptions;
using AHC.CD.Resources.Messages;
using AHC.CD.WebUI.MVC.Areas.Profile.Attributes;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ProfessionalReference;
using AHC.CD.WebUI.MVC.CustomHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class ProfessionalReferenceController : Controller
    {
        private IProfileManager profileManager = null;
        private IErrorLogger errorLogger = null;
        private IChangeNotificationManager notificationManager;


        public ProfessionalReferenceController(IProfileManager profileManager, IErrorLogger errorLogger, IChangeNotificationManager notificationManager)
        {
            this.profileManager = profileManager;
            this.errorLogger = errorLogger;
            this.notificationManager = notificationManager;
        }

        [HttpPost]

        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task <ActionResult> AddProfessionalReference( int profileId, AHC.CD.WebUI.MVC.Areas.Profile.Models.ProfessionalReference.ProfessionalReferenceViewModel professionalReference)
        {
            string status = "true";
            ProfessionalReferenceInfo dataModelProfessionalReference = null;

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelProfessionalReference = AutoMapper.Mapper.Map<ProfessionalReferenceViewModel, ProfessionalReferenceInfo>(professionalReference);

                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Professional Reference Details", "Added");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    await profileManager.AddProfessionalReferenceAsync(profileId, dataModelProfessionalReference);
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

            return Json(new { status = status, professionalReference = dataModelProfessionalReference }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task <ActionResult> UpdateProfessionalReference(int profileId,AHC.CD.WebUI.MVC.Areas.Profile.Models.ProfessionalReference.ProfessionalReferenceViewModel professionalReference)
        {
            string status = "true";
            ProfessionalReferenceInfo dataModelProfessionalReference = null;

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelProfessionalReference = AutoMapper.Mapper.Map<ProfessionalReferenceViewModel, ProfessionalReferenceInfo>(professionalReference);

                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Professional Reference Details", "Updated");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    await profileManager.UpdateProfessionalReferenceAsync(profileId, dataModelProfessionalReference);
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

            return Json(new { status = status, professionalReference = dataModelProfessionalReference }, JsonRequestBehavior.AllowGet);
        }
    }
}