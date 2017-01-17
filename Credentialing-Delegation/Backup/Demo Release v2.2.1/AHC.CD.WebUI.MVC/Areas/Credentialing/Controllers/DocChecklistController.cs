using AHC.CD.Business;
using AHC.CD.Business.DocumentWriter;
using AHC.CD.Business.Notification;
using AHC.CD.Business.Profiles;
using AHC.CD.ErrorLogging;
using AHC.CD.WebUI.MVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.DocumentRepository;
using AHC.CD.Entities.DocumentRepository;
using AHC.CD.Exceptions;
using AHC.CD.Resources.Messages;
using AHC.CD.Business.BusinessModels.ProfileUpdates;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.IO;
using System.Net;
using System.Threading;


namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Controllers
{
    public class DocChecklistController : Controller
    {
        private IProfileManager profileManager = null;
        private IErrorLogger errorLogger = null;
        // Change Notifications
        private IChangeNotificationManager notificationManager;
        private IProfileUpdateManager profileUpdateManager = null;

        public DocChecklistController(IProfileManager profileManager, IErrorLogger errorLogger, IChangeNotificationManager notificationManager, IProfileUpdateManager profileUpdateManager) // Change Notifications
        {
            this.profileManager = profileManager;
            this.errorLogger = errorLogger;
            // Change Notifications
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

        //
        // GET: /Credentialing/DocChecklist/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Credentialing/DocChecklistPlanMapping/
        public ActionResult DocChecklistPlanMapping()
        {
            return View();
        }

        public async Task<string> GetDocumentProfileDataAsync(int profileId)
        {
            string UserAuthId = await GetUserAuthId();
            int CDUserId = profileManager.GetCredentialingUserId(UserAuthId);
            bool isCCO = await GetUserRole();
            return JsonConvert.SerializeObject(await profileManager.GetDocumentRepositoryDataAsync(profileId, CDUserId, isCCO));

        }

        [HttpPost]
        public async Task<ActionResult> AddOtherDocumentAsync(int profileId, OtherDocumentViewModel otherDocument)
        {
            string status = "true";
            OtherDocument dataModelOtherDocument = null;

            try
            {
                if (ModelState.IsValid)
                {
                    string UserAuthId = await GetUserAuthId();
                    int CDUserId = profileManager.GetCredentialingUserId(UserAuthId);
                    otherDocument.ModifiedBy = CDUserId.ToString();

                    dataModelOtherDocument = AutoMapper.Mapper.Map<OtherDocumentViewModel, OtherDocument>(otherDocument);
                    DocumentDTO document = CreateDocument(otherDocument.File);
                    //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Other Legal Names", "Added");
                    //await notificationManager.SaveNotificationDetailAsync(notification);

                    var result = await profileManager.AddOtherDocumentAsync(profileId, dataModelOtherDocument, document);
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

            return Json(new { status = status, otherDocument = dataModelOtherDocument }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateOtherDocumentAsync(int profileId, OtherDocumentViewModel otherDocument)
        {
            string status = "true";
            OtherDocument dataModelOtherDocument = null;
            bool isCCO = await GetUserRole();

            try
            {
                if (ModelState.IsValid)
                {
                    string UserAuthId = await GetUserAuthId();
                    int CDUserId = profileManager.GetCredentialingUserId(UserAuthId);
                    otherDocument.ModifiedBy = CDUserId.ToString();

                    dataModelOtherDocument = AutoMapper.Mapper.Map<OtherDocumentViewModel, OtherDocument>(otherDocument);
                    if (isCCO)
                    {
                        DocumentDTO document = CreateDocument(otherDocument.File);
                        //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Other Legal Names", "Updated");
                        //await notificationManager.SaveNotificationDetailAsync(notification);

                        await profileManager.UpdateOtherDocumentAsync(profileId, dataModelOtherDocument, document);
                    }
                    else
                    {
                        string userId = await GetUserAuthId();
                        ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                        tracker.ProfileId = profileId;
                        tracker.Section = "Document Repository";
                        tracker.SubSection = "Other Document";
                        tracker.userAuthId = userId;
                        tracker.objId = otherDocument.OtherDocumentID;
                        tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                        tracker.url = "/Profile/DocumentRepository/UpdateOtherDocumentAsync?profileId=";

                        profileUpdateManager.AddProfileUpdateForProvider(otherDocument, dataModelOtherDocument, tracker);
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
                status = ExceptionMessage.PROFILE_ADD_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, otherDocument = dataModelOtherDocument }, JsonRequestBehavior.AllowGet);
        }

        public void SendToPrinter(string DocumentPath)
        {
            string path = Server.MapPath("~");
            path = path.Substring(0, path.Length - 1);
            path = path + DocumentPath;
            Uri remote = new Uri(path);
            var remoteUri = remote.AbsolutePath;

            if (Path.GetExtension(remoteUri) != ".pdf")
            {

                ProcessStartInfo info = new ProcessStartInfo();
                info.Verb = "print";
                info.FileName = remoteUri;
                info.CreateNoWindow = true;
                info.WindowStyle = ProcessWindowStyle.Hidden;

                Process p = new Process();
                p.StartInfo = info;
                p.Start();

            }
            else
            {
                PrintDocument printDoc = new PrintDocument();
                PrintDialog printDialog = new PrintDialog();
                printDoc.DocumentName = remoteUri;// PrintDocument printDocument1
                printDialog.Document = printDoc;
                printDialog.AllowPrintToFile = true;
                printDialog.AllowSelection = true;
                printDialog.AllowSomePages = true;
                printDialog.PrintToFile = true;
                if (printDialog.ShowDialog() == DialogResult.OK)
                    printDoc.Print();

            }

        }

        public void DownloadFile(string DocumentPath)
        {
            string path = Server.MapPath("~");
            path = path.Substring(0, path.Length - 1);
            path = path + DocumentPath;
            Uri remote = new Uri(path);
            var remoteUri = remote.AbsolutePath;
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\";
            remote = new Uri(desktop);
            var remoteDesktop = remote.AbsolutePath;

            string fileName = Path.GetFileName(DocumentPath), myStringWebResource = null;
            // Create a new WebClient instance.
            WebClient myWebClient = new WebClient();
            // Concatenate the domain with the Web resource filename.
            myStringWebResource = remoteUri + fileName;
            // Download the Web resource and save it into the current filesystem folder.
            myWebClient.DownloadFile(path, remoteDesktop + fileName);

        }

        //public FileResult DownloadFile(string CurrentFilePath)
        //{
        //    //int CurrentFileID = Convert.ToInt32(FileID);
        //    var filesCol = obj.GetFiles();
        //    string path = Server.MapPath("~");
        //    path = path.Substring(0, path.Length - 1);
        //    path = path + CurrentFilePath;
        //    string CurrentFileName = (from fls in filesCol
        //                              where fls.FilePath == path
        //                              select fls.FilePath).First();

        //    string contentType = string.Empty;

        //    if (CurrentFileName.Contains(".pdf"))
        //    {
        //        contentType = "application/pdf";
        //    }

        //    else if (CurrentFileName.Contains(".docx"))
        //    {
        //        contentType = "application/docx";
        //    }
        //    else if (CurrentFileName.Contains(".jpg"))
        //    {
        //        contentType = "application/octet-stream";
        //    }
        //    return File(CurrentFileName, contentType, CurrentFileName);
        //}

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