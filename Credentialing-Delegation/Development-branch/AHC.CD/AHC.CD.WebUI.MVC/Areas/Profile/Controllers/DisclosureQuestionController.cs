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
using Microsoft.AspNet.Identity.Owin;
using AHC.CD.Business.Profiles;
using AHC.CD.WebUI.MVC.Models;
using AHC.CD.Business.BusinessModels.ProfileUpdates;
using System.IO;
using System.Threading;
using AHC.UtilityService;
using Newtonsoft.Json;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class DisclosureQuestionController : Controller
    {
        private IProfileManager profileManager = null;
        private IErrorLogger errorLogger = null;
        private IChangeNotificationManager notificationManager;
        private IProfileUpdateManager profileUpdateManager = null;
        private IDocumentRootLocator iDocumentRootLocatorManager= null;

        public DisclosureQuestionController(IDocumentRootLocator iDocumentRootLocatorManager, IProfileManager profileManager, IErrorLogger errorLogger, IChangeNotificationManager notificationManager, IProfileUpdateManager profileUpdateManager)
        {
            this.iDocumentRootLocatorManager = iDocumentRootLocatorManager;
            this.profileManager = profileManager;
            this.errorLogger = errorLogger;
            this.notificationManager = notificationManager;
            this.profileUpdateManager = profileUpdateManager;
        }

        protected ApplicationUserManager _authUserManager;
        protected ApplicationUserManager AuthUserManager
        {
            get
            {
                return _authUserManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _authUserManager = value;
            }
        }

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }



        [HttpPost]
        public string FileUpload(object obj)
        {
            var length = Request.ContentLength;
            var bytes = new byte[length];
            Request.InputStream.Read(bytes, 0, length);
            var fileName = Request.Headers["X-File-Name"];
            var fileSize = Request.Headers["X-File-Size"];
            var fileType = Request.Headers["X-File-Type"];
            string path = "\\ApplicationDocuments\\" + UniqueKeyGenerator.GetUniqueKey() + fileName;
            //Thread.Sleep(2000);
            var tempoaraypath = iDocumentRootLocatorManager.GetDocumentRootFolder() + path;
            var fileStream = new FileStream(tempoaraypath, FileMode.Create, FileAccess.ReadWrite);
            
            //Thread.Sleep(2000);
            fileStream.Write(bytes, 0, length);
            fileStream.Close();

            return JsonConvert.SerializeObject(new { FileLength = bytes.Length, FilePath = path, RelativePath = tempoaraypath });
        }




        [HttpPost]
        public async Task<ActionResult> UpdateDisclosureQuestionAsync(int profileId, ProfileDisclosureViewModel disclosureQuestion)
        {
            string status = "true";
            ProfileDisclosure dataModelDisclosureQuestion = null;
            bool isCCO = await GetUserRole();
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelDisclosureQuestion = AutoMapper.Mapper.Map<ProfileDisclosureViewModel, ProfileDisclosure>(disclosureQuestion);

                    //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Disclosure Questions", "Updated");
                    //await notificationManager.SaveNotificationDetailAsync(notification);

                    //await profileManager.AddEditDisclosureQuestionAnswersAsync(profileId, dataModelDisclosureQuestion);

                    if (disclosureQuestion.ProfileDisclosureID == 0)
                    {
                      
                        await profileManager.AddEditDisclosureQuestionAnswersAsync(profileId, dataModelDisclosureQuestion);
                        ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Disclosure Questions", "Added");
                        await notificationManager.SaveNotificationDetailAsyncForAdd(notification, isCCO);
                    }
                    else if (isCCO && disclosureQuestion.ProfileDisclosureID != 0)
                    {
                       
                        await profileManager.AddEditDisclosureQuestionAnswersAsync(profileId, dataModelDisclosureQuestion);
                        ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Disclosure Questions", "Updated");
                        await notificationManager.SaveNotificationDetailAsync(notification);
                    }
                    else if (!isCCO && disclosureQuestion.ProfileDisclosureID != 0)
                    {
                        string userId = await GetUserAuthId();
                        ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                        tracker.ProfileId = profileId;
                        tracker.Section = "Disclosure Question";
                        tracker.SubSection = "Profile Disclosure";
                        tracker.userAuthId = userId;
                        tracker.objId = disclosureQuestion.ProfileDisclosureID;
                        tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                        tracker.url = "/Profile/DisclosureQuestion/UpdateDisclosureQuestionAsync?profileId=";

                        profileUpdateManager.AddProfileUpdateForProvider(disclosureQuestion, dataModelDisclosureQuestion, tracker);
                    }
                    
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

        private async Task<string> GetUserAuthId()
        {
            var currentUser = HttpContext.User.Identity.Name;
            var appUser = new ApplicationUser() { UserName = currentUser };
            var user = await AuthUserManager.FindByNameAsync(appUser.UserName);

            return user.Id;
        }

        private async Task<bool> GetUserRole()
        {
            var currentUser = HttpContext.User.Identity.Name;
            var appUser = new ApplicationUser() { UserName = currentUser };
            var user = await AuthUserManager.FindByNameAsync(appUser.UserName);
            var Role = RoleManager.Roles.FirstOrDefault(r => r.Name == "CCO");

            return user.Roles.Any(r => r.RoleId == Role.Id);
        }

        #endregion
    }
}