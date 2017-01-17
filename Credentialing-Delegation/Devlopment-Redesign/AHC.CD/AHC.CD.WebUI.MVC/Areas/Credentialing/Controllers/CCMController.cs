using AHC.CD.Business.Credentialing.AppointmentInfo;
using AHC.CD.Business.DocumentWriter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using AHC.CD.WebUI.MVC.Models;
using Newtonsoft.Json;
using AHC.CD.Business.Credentialing.CnD;
using AHC.CD.Business;
using AHC.CD.WebUI.MVC.Areas.Credentialing.Models.CredentialingCheckList;
using AHC.CD.WebUI.MVC.CustomHelpers;
using AHC.CD.Entities.Credentialing.AppointmentInformation;
using AHC.CD.Entities.Notification;
using AHC.CD.Business.Notification;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Web.Script.Serialization;
using System.Drawing.Imaging;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;
using AHC.UtilityService;
using AHC.CD.Entities;
using AHC.CD.Resources.Document;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Controllers
{
    public class CCMController : Controller
    {
        private IProfileManager profileManager = null;
        private IApplicationManager applicationManager = null;
        private IAppointmentManager appointmentManager = null;
        protected ApplicationUserManager _authUserManager;
        private IChangeNotificationManager notificationManager;
        private IDocumentsManager documentmanager = null;
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

        public CCMController(ProviderDocumentManager documentmanager, IAppointmentManager appointmentManager, IChangeNotificationManager notificationManager, IApplicationManager applicationManager, IProfileManager profileManager)
        {
            this.appointmentManager = appointmentManager;
            this.applicationManager = applicationManager;
            this.profileManager = profileManager;
            this.notificationManager = notificationManager;
            this.documentmanager = documentmanager;
        }

        //
        // GET: /Credentialing/Index/
        [Authorize(Roles = "CCM")]
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Credentialing/SPA/
        public async Task<ActionResult> SPA(int id)
        {
            AHC.CD.Entities.MasterProfile.Profile profileData = null;
            profileData = await profileManager.GetByIdAsync(id);

            ViewBag.profileData = Json(new { profileData }, JsonRequestBehavior.AllowGet);
            return View();
        }


        public async Task<ActionResult> Application(int id)
        {
            try
            {
                string userid = await GetUserAuthId();
                ViewBag.signaturepath = appointmentManager.GetCCMSignature(userid);
                ViewBag.CredentialingInfoID = id;
                ViewBag.CredentialingInfo = JsonConvert.SerializeObject(await appointmentManager.GetCredentialinfoByID(id));
                
                //CredentialingAppointmentDetailViewModel credentialingAppointmentDetailViewModel=
                return View("SPA");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ActionResult> GetAllCredentialingFilterList()
        {
            var data = await appointmentManager.GetAllFilteredCredentialInfoList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [AjaxAction]
        public async Task<ActionResult> CCMActionUploadAsync(int profileId, CredentialingAppointmentDetailViewModel credentialingAppointmentDetailViewModel)
        {
            string status = "";
            CredentialingAppointmentDetail credentialingAppointmentDetail = null;
            try
            {
                if (credentialingAppointmentDetailViewModel.CredentialingAppointmentResult.SignaturePath != null && !credentialingAppointmentDetailViewModel.CredentialingAppointmentResult.SignaturePath.Contains("\\ApplicationDocuments\\"))
                {

                    Bitmap bmpReturn = null;
                    string filename = UniqueKeyGenerator.GetUniqueKey() + "_CCMSignature.png";
                    //string path1 = HttpContext.Request.MapPath(@"\ApplicationDocuments\CCMSignatureDocuments\" + filename);
                    string path = HttpContext.Request.MapPath("~/ApplicationDocuments/CCMSignatureDocuments/" + filename);
                    var base64Data = Regex.Match(credentialingAppointmentDetailViewModel.CredentialingAppointmentResult.SignaturePath, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
                    byte[] byteBuffer = Convert.FromBase64String(base64Data);
                    MemoryStream memoryStream = new MemoryStream(byteBuffer);
                    using (memoryStream)
                    {
                        memoryStream.Position = 0;
                        bmpReturn = (Bitmap)Bitmap.FromStream(memoryStream);
                        
                        bmpReturn.Save(path, ImageFormat.Png);
                        memoryStream.Close();
                    }

                    
                        ChangeNotificationDetail notification = null;
                        if (credentialingAppointmentDetailViewModel.CredentialingAppointmentResult.ApprovalStatusType == Entities.MasterData.Enums.CCMApprovalStatusType.Approved)
                        {
                            notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "CCM Appointment Result", "Approved");
                        }
                        else if (credentialingAppointmentDetailViewModel.CredentialingAppointmentResult.ApprovalStatusType == Entities.MasterData.Enums.CCMApprovalStatusType.Rejected)
                        {
                            notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "CCM Appointment Result", "Rejected");
                        }

                        await notificationManager.SaveNotificationDetailAsyncForCCO(notification, credentialingAppointmentDetailViewModel.CredentialingAppointmentResult.ApprovalStatusType.ToString(), credentialingAppointmentDetailViewModel.CredentialingAppointmentDetailID);
                        credentialingAppointmentDetailViewModel.CredentialingAppointmentResult.SignaturePath = filename;
                        //credentialingAppointmentDetailViewModel.FileUploadPath = null;

                        credentialingAppointmentDetail = AutoMapper.Mapper.Map<CredentialingAppointmentDetailViewModel, CredentialingAppointmentDetail>(credentialingAppointmentDetailViewModel);
                        // Change Notifications
                        await appointmentManager.SaveResultForScheduledAppointmentwithdigitalsignature(credentialingAppointmentDetailViewModel.CredentialingAppointmentDetailID, credentialingAppointmentDetail.CredentialingAppointmentResult, await GetUserAuthId());
                        status = "true";
                    
                }
                else if (credentialingAppointmentDetailViewModel.CredentialingAppointmentResult.SignaturePath != null && credentialingAppointmentDetailViewModel.CredentialingAppointmentResult.SignaturePath.Contains("\\ApplicationDocuments\\"))
                {

                    ChangeNotificationDetail notification = null;
                    if (credentialingAppointmentDetailViewModel.CredentialingAppointmentResult.ApprovalStatusType == Entities.MasterData.Enums.CCMApprovalStatusType.Approved)
                    {
                        notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "CCM Appointment Result", "Approved");
                    }
                    else if (credentialingAppointmentDetailViewModel.CredentialingAppointmentResult.ApprovalStatusType == Entities.MasterData.Enums.CCMApprovalStatusType.Rejected)
                    {
                        notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "CCM Appointment Result", "Rejected");
                    }

                    await notificationManager.SaveNotificationDetailAsyncForCCO(notification, credentialingAppointmentDetailViewModel.CredentialingAppointmentResult.ApprovalStatusType.ToString(), credentialingAppointmentDetailViewModel.CredentialingAppointmentDetailID);
                    //credentialingAppointmentDetailViewModel.FileUploadPath = null;

                    credentialingAppointmentDetail = AutoMapper.Mapper.Map<CredentialingAppointmentDetailViewModel, CredentialingAppointmentDetail>(credentialingAppointmentDetailViewModel);
                    // Change Notifications
                    await appointmentManager.SaveResultForScheduledAppointmentwithdigitalsignature(credentialingAppointmentDetailViewModel.CredentialingAppointmentDetailID, credentialingAppointmentDetail.CredentialingAppointmentResult, await GetUserAuthId());
                    status = "true";

                }
                else
                {
                    status = "Signature is null";
                    if (ModelState.IsValid)
                    {
                        ChangeNotificationDetail notification = null;
                        if (credentialingAppointmentDetailViewModel.CredentialingAppointmentResult.ApprovalStatusType == Entities.MasterData.Enums.CCMApprovalStatusType.Approved)
                        {
                            notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "CCM Appointment Result", "Approved");
                        }
                        else if (credentialingAppointmentDetailViewModel.CredentialingAppointmentResult.ApprovalStatusType == Entities.MasterData.Enums.CCMApprovalStatusType.Rejected)
                        {
                            notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "CCM Appointment Result", "Rejected");
                        }

                        await notificationManager.SaveNotificationDetailAsyncForCCO(notification, credentialingAppointmentDetailViewModel.CredentialingAppointmentResult.ApprovalStatusType.ToString(), credentialingAppointmentDetailViewModel.CredentialingAppointmentDetailID);

                        credentialingAppointmentDetail = AutoMapper.Mapper.Map<CredentialingAppointmentDetailViewModel, CredentialingAppointmentDetail>(credentialingAppointmentDetailViewModel);
                        DocumentDTO document = CreateDocument(credentialingAppointmentDetailViewModel.CredentialingAppointmentResult.SignatureFile);
                        // Change Notifications
                        await appointmentManager.SaveResultForScheduledAppointment(credentialingAppointmentDetailViewModel.CredentialingAppointmentDetailID, document, credentialingAppointmentDetail.CredentialingAppointmentResult, await GetUserAuthId());
                        status = "true";
                    }
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return Json(new { status = status, data = credentialingAppointmentDetail }, JsonRequestBehavior.AllowGet);
        }

        public async Task<string> GetSignatureforCCM()
        {
            string userid = await GetUserAuthId();
            string signaturepath = appointmentManager.GetCCMSignature(userid);
            return signaturepath;
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

        private async Task<string> GetUserAuthId()
        {
            var currentUser = HttpContext.User.Identity.Name;
            var appUser = new ApplicationUser() { UserName = currentUser };
            var user = await AuthUserManager.FindByNameAsync(appUser.UserName);

            return user.Id;
        }


        #endregion

    }
}






