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
using AHC.CD.Entities.Notification;


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

        public static T GetValueFromAnonymousType<T>(object dataitem, string itemkey)
        {
            System.Type type = dataitem.GetType();
            T itemvalue = (T)type.GetProperty(itemkey).GetValue(dataitem, null);
            return itemvalue;
        }
        public async Task<string> GetDocumentProfileDataAsync(int profileId)
        {
            string UserAuthId = await GetUserAuthId();
            int CDUserId = profileManager.GetCredentialingUserId(UserAuthId);
            bool isCCO = await GetUserRole();

            
            var profileData = await profileManager.GetDocumentRepositoryDataAsync(profileId, CDUserId, isCCO);

            string ServerPath = "";

            if (profileData.PersonalIdentification != null && profileData.PersonalIdentification.DLCertificatePath != null)
            {
                ServerPath = HttpContext.Server.MapPath(profileData.PersonalIdentification.DLCertificatePath);
                if (System.IO.File.Exists(ServerPath))
                    profileData.PersonalIdentification.DLCertificateSize = new FileInfo(ServerPath).Length;
            }

            if (profileData.PersonalIdentification != null && profileData.PersonalIdentification.SSNCertificatePath != null)
            {
                ServerPath = HttpContext.Server.MapPath(profileData.PersonalIdentification.SSNCertificatePath);
                if (System.IO.File.Exists(ServerPath))
                    profileData.PersonalIdentification.SSNCertificateSize = new FileInfo(ServerPath).Length;
            }

            if (profileData.BirthInformation != null && profileData.BirthInformation.BirthCertificatePath != null)
            {
                ServerPath = HttpContext.Server.MapPath(profileData.BirthInformation.BirthCertificatePath);
                if (System.IO.File.Exists(ServerPath))
                    profileData.BirthInformation.BirthCertificateSize = new FileInfo(ServerPath).Length;
            }

            foreach (var license in profileData.OtherLegalNames)
            {
                if (license.DocumentPath != null)
                {
                    ServerPath = HttpContext.Server.MapPath(license.DocumentPath);
                    if (System.IO.File.Exists(ServerPath))
                      license.DocumentSize = new FileInfo(ServerPath).Length;
                }
            }

            if (profileData.CVInformation.CVDocumentPath != null)
                {
                    ServerPath = HttpContext.Server.MapPath(profileData.CVInformation.CVDocumentPath);
                    if (System.IO.File.Exists(ServerPath))
                        profileData.CVInformation.CVDocumentSize = new FileInfo(ServerPath).Length;
                }
            if (profileData.VisaDetail != null && profileData.VisaDetail.VisaInfo != null)
            {
                if (profileData.VisaDetail.VisaInfo.GreenCardCertificatePath != null)
                {
                    ServerPath = HttpContext.Server.MapPath(profileData.VisaDetail.VisaInfo.GreenCardCertificatePath);
                    if (System.IO.File.Exists(ServerPath))
                        profileData.VisaDetail.VisaInfo.GreenCardCertificateSize = new FileInfo(ServerPath).Length;
                }

                if (profileData.VisaDetail.VisaInfo.NationalIDCertificatePath != null)
                {
                    ServerPath = HttpContext.Server.MapPath(profileData.VisaDetail.VisaInfo.NationalIDCertificatePath);
                    if (System.IO.File.Exists(ServerPath))
                        profileData.VisaDetail.VisaInfo.NationalIDCertificateSize = new FileInfo(ServerPath).Length;
                }

                if (profileData.VisaDetail.VisaInfo.VisaCertificatePath != null)
                {
                    ServerPath = HttpContext.Server.MapPath(profileData.VisaDetail.VisaInfo.VisaCertificatePath);
                    if (System.IO.File.Exists(ServerPath))
                        profileData.VisaDetail.VisaInfo.VisaCertificateSize = new FileInfo(ServerPath).Length;
                }
            }  

            foreach (var license in profileData.StateLicenses)
            {
                if (license.StateLicenseDocumentPath != null)
                {
                    ServerPath = HttpContext.Server.MapPath(license.StateLicenseDocumentPath);
                    if (System.IO.File.Exists(ServerPath))
                        license.StateLicenseDocumentSize = new FileInfo(ServerPath).Length;
                }
            }

            foreach (var license in profileData.FederalDEAInformations)
            {
                if (license.DEALicenceCertPath != null)
                {
                    ServerPath = HttpContext.Server.MapPath(license.DEALicenceCertPath);
                    if (System.IO.File.Exists(ServerPath))
                        license.DEALicenceCertSize = new FileInfo(ServerPath).Length;
                }
            }

            foreach (var license in profileData.MedicareInformations)
            {
                if (license.CertificatePath != null)
                {
                    ServerPath = HttpContext.Server.MapPath(license.CertificatePath);
                    if (System.IO.File.Exists(ServerPath))
                        license.CertificateSize = new FileInfo(ServerPath).Length;
                }
            }


            foreach (var license in profileData.MedicaidInformations)
            {
                if (license.CertificatePath != null)
                {
                    ServerPath = HttpContext.Server.MapPath(license.CertificatePath);
                    if (System.IO.File.Exists(ServerPath))
                        license.CertificateSize = new FileInfo(ServerPath).Length;
                }
            }


            foreach (var license in profileData.CDSCInformations)
            {
                if (license.CDSCCerificatePath != null)
                {
                    ServerPath = HttpContext.Server.MapPath(license.CDSCCerificatePath);
                    if (System.IO.File.Exists(ServerPath))
                        license.CDSCCerificateSize = new FileInfo(ServerPath).Length;
                }
            }

            foreach (var license in profileData.EducationDetails)
            {
                if (license.CertificatePath != null)
                {
                    ServerPath = HttpContext.Server.MapPath(license.CertificatePath);
                    if (System.IO.File.Exists(ServerPath))
                        license.CertificateSize = new FileInfo(ServerPath).Length;
                }
            }

            if (profileData.ECFMGDetail != null && profileData.ECFMGDetail.ECFMGCertPath != null)
            {
                ServerPath = HttpContext.Server.MapPath(profileData.ECFMGDetail.ECFMGCertPath);
                if (System.IO.File.Exists(ServerPath))
                    profileData.ECFMGDetail.ECFMGCertSize = new FileInfo(ServerPath).Length;
            }

            foreach (var license in profileData.ProgramDetails)
            {
                if (license.DocumentPath != null)
                {
                    ServerPath = HttpContext.Server.MapPath(license.DocumentPath);
                    if (System.IO.File.Exists(ServerPath))
                        license.DocumentSize = new FileInfo(ServerPath).Length;
                }
            }

            foreach (var license in profileData.CMECertifications)
            {
                if (license.CertificatePath != null)
                {
                    ServerPath = HttpContext.Server.MapPath(license.CertificatePath);
                    if (System.IO.File.Exists(ServerPath))
                        license.CertificateSize = new FileInfo(ServerPath).Length;
                }
            }
            foreach (var speciality in profileData.SpecialtyDetails)
            {
                if (speciality.SpecialtyBoardCertifiedDetail != null && speciality.SpecialtyBoardCertifiedDetail.BoardCertificatePath != null)
                {
                    ServerPath = HttpContext.Server.MapPath(speciality.SpecialtyBoardCertifiedDetail.BoardCertificatePath);
                    if (System.IO.File.Exists(ServerPath))
                        speciality.SpecialtyBoardCertifiedDetail.BoardCertificateSize = new FileInfo(ServerPath).Length;
                }
            }

            if (profileData.HospitalPrivilegeInformation != null)
            {
                foreach (var hospitalprivilege in profileData.HospitalPrivilegeInformation.HospitalPrivilegeDetails)
                {
                    if (hospitalprivilege.HospitalPrevilegeLetterPath != null)
                    {
                        ServerPath = HttpContext.Server.MapPath(hospitalprivilege.HospitalPrevilegeLetterPath);
                        if (System.IO.File.Exists(ServerPath))
                            hospitalprivilege.HospitalPrevilegeLetterSize = new FileInfo(ServerPath).Length;
                    }
                }

            }



            foreach (var license in profileData.ProfessionalLiabilityInfoes)
            {
                if (license.InsuranceCertificatePath != null)
                {
                    ServerPath = HttpContext.Server.MapPath(license.InsuranceCertificatePath);
                    if (System.IO.File.Exists(ServerPath))
                        license.InsuranceCertificateSize = new FileInfo(ServerPath).Length;
                }
            }

            foreach (var license in profileData.ProfessionalWorkExperiences)
            {
                if (license.WorkExperienceDocPath != null)
                {
                    ServerPath = HttpContext.Server.MapPath(license.WorkExperienceDocPath);
                    if (System.IO.File.Exists(ServerPath))
                        license.WorkExperienceDocSize = new FileInfo(ServerPath).Length;
                }
            }

            foreach (var license in profileData.ContractInfoes)
            {
                if (license.ContractFilePath != null)
                {
                    ServerPath = HttpContext.Server.MapPath(license.ContractFilePath);
                    if (System.IO.File.Exists(ServerPath))
                        license.ContractFileSize = new FileInfo(ServerPath).Length;
                }
            }

            foreach (var license in profileData.OtherDocuments)
            {
                if (license.DocumentPath != null)
                {
                    ServerPath = HttpContext.Server.MapPath(license.DocumentPath);
                    if (System.IO.File.Exists(ServerPath))
                        license.DocumentSize = new FileInfo(ServerPath).Length;
                }
            }

            foreach (var verificationInfo in profileData.ProfileVerificationInfo)
            {
                foreach (var verificationDetails in verificationInfo.ProfileVerificationDetails)
                {
                    if (verificationDetails.VerificationResult != null && verificationDetails.VerificationResult.VerificationDocumentPath != null)
                    {
                        ServerPath = HttpContext.Server.MapPath(verificationDetails.VerificationResult.VerificationDocumentPath);
                        if (System.IO.File.Exists(ServerPath))
                            verificationDetails.VerificationResult.VerificationDocumentSize = new FileInfo(ServerPath).Length;
                    }
                }
            }

            return JsonConvert.SerializeObject(profileData);

        }

        [HttpPost]
        public async Task<ActionResult> AddOtherDocumentAsync(int profileId, OtherDocumentViewModel otherDocument)
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
                    DocumentDTO document = CreateDocument(otherDocument.File);
                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Document Repository - Other Document", "Added");
                    await notificationManager.SaveNotificationDetailAsyncForAdd(notification, isCCO);

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

                        profileManager.UpdateOtherDocumentAsync(profileId, dataModelOtherDocument, document);
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
            try
            {

                string CurrentFileName = System.Web.HttpContext.Current.Request.MapPath(DocumentPath);
                //path = path.Substring(0, path.Length - 1);
                //string CurrentFileName = path + DocumentPath;
                //Uri remote = new Uri(path);
                //var remoteUri = remote.AbsolutePath;

                Process printjob = new Process();

                printjob.StartInfo.FileName = @CurrentFileName; //path of your file;

                if (CurrentFileName.Contains(".pdf"))
                {

                    printjob.StartInfo.Verb = "";

                }
                else
                {

                    printjob.StartInfo.Verb = "Print";

                }

                printjob.StartInfo.CreateNoWindow = true;

                printjob.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

                PrinterSettings setting = new PrinterSettings();

                setting.DefaultPageSettings.Landscape = true;

                printjob.Start();

            }
            catch (ApplicationException ex)
            {
                errorLogger.LogError(ex);
            }
            catch (Exception ex)
            {
                errorLogger.LogError(ex);
            }

            //if (Path.GetExtension(remoteUri) != ".pdf")
            //{

            //    ProcessStartInfo info = new ProcessStartInfo();
            //    info.Verb = "print";
            //    info.FileName = remoteUri;
            //    info.CreateNoWindow = true;
            //    info.WindowStyle = ProcessWindowStyle.Hidden;

            //    Process p = new Process();
            //    p.StartInfo = info;
            //    p.Start();

            //}
            //else
            //{
            //    PrintDocument printDoc = new PrintDocument();
            //    PrintDialog printDialog = new PrintDialog();
            //    printDoc.DocumentName = remoteUri;// PrintDocument printDocument1
            //    printDialog.Document = printDoc;
            //    printDialog.AllowPrintToFile = true;
            //    printDialog.AllowSelection = true;
            //    printDialog.AllowSomePages = true;
            //    printDialog.PrintToFile = true;
            //    if (printDialog.ShowDialog() == DialogResult.OK)
            //        printDoc.Print();

            //}

        }

        public FileResult DownloadFile(string CurrentFilePath)
        {
            CurrentFilePath = System.Web.HttpContext.Current.Request.MapPath(CurrentFilePath);
            //path = path.Substring(0, path.Length - 1);
            //CurrentFilePath = path + CurrentFilePath;

            string CurrentFileName = Path.GetFileName(CurrentFilePath);

            //string CurrentFileName = "D:\\Pics\\gfs_85710_2_2 (2).jpg";

            //Uri remote = new Uri(path);
            //var remoteUri = remote.AbsolutePath;

            string contentType = string.Empty;

            if (CurrentFileName.Contains(".pdf"))
            {
                contentType = "application/pdf";
            }

            else if (CurrentFileName.Contains(".docx"))
            {
                contentType = "application/docx";
            }

            else if (CurrentFileName.Contains(".jpg"))
            {
                contentType = "image/jpeg";
            }

            return File(CurrentFilePath, contentType, CurrentFileName);

        }

        //public void DownloadFile(string DocumentPath)
        //{
        //    string path = Server.MapPath("~");
        //    path = path.Substring(0, path.Length - 1);
        //    path = path + DocumentPath;
        //    Uri remote = new Uri(path);
        //    var remoteUri = remote.AbsolutePath;
        //    string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\";
        //    remote = new Uri(desktop);
        //    var remoteDesktop = remote.AbsolutePath;

        //    string fileName = Path.GetFileName(DocumentPath), myStringWebResource = null;
        //    // Create a new WebClient instance.
        //    WebClient myWebClient = new WebClient();
        //    // Concatenate the domain with the Web resource filename.
        //    myStringWebResource = remoteUri + fileName;
        //    // Download the Web resource and save it into the current filesystem folder.
        //    myWebClient.DownloadFile(path, remoteDesktop + fileName);

        //}

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