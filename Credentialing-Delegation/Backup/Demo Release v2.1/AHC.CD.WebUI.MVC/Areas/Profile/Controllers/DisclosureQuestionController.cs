using AHC.CD.Business;
using AHC.CD.Business.DocumentWriter;
using AHC.CD.Business.Notification;
using AHC.CD.Entities.MasterProfile.DisclosureQuestions;
using AHC.CD.Entities.Notification;
using AHC.CD.ErrorLogging;
using AHC.CD.Exceptions;
using AHC.CD.Resources.Messages;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.DisclosureQuestions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class DisclosureQuestionController : Controller
    {
        private IProfileManager profileManager = null;
        private IErrorLogger errorLogger = null;
        private IChangeNotificationManager notificationManager;

        public DisclosureQuestionController(IProfileManager profileManager, IErrorLogger errorLogger, IChangeNotificationManager notificationManager)
        {
            this.profileManager = profileManager;
            this.errorLogger = errorLogger;
            this.notificationManager = notificationManager;
        }

        [HttpPost]
        public async Task<ActionResult> UpdateDisclosureQuestionAsync(int profileId, ProfileDisclosureViewModel disclosureQuestion)
        {
            string status = "true";
            ProfileDisclosure dataModelDisclosureQuestion = null;
        
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelDisclosureQuestion = AutoMapper.Mapper.Map<ProfileDisclosureViewModel, ProfileDisclosure>(disclosureQuestion);
                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Disclosure Questions", "Updated");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    await profileManager.AddEditDisclosureQuestionAnswersAsync(profileId, dataModelDisclosureQuestion);
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
                //Will have to add an exception message
                status = ExceptionMessage.DISCLOSURE_QUESTIONS_CREATE_EXCEPTION;
            }

            return Json(new { status = status, disclosureQuestion = dataModelDisclosureQuestion }, JsonRequestBehavior.AllowGet);
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