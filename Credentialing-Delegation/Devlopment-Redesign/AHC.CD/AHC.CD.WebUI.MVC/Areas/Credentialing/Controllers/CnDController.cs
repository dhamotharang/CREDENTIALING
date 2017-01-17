using AHC.CD.Business;
using AHC.CD.Business.Credentialing.AppointmentInfo;
using AHC.CD.Business.Credentialing.CnD;
using AHC.CD.Entities.Credentialing.AppointmentInformation;
using AHC.CD.ErrorLogging;
using AHC.CD.Resources.Messages;
using AHC.CD.WebUI.MVC.Areas.Credentialing.Models.CredentialingCheckList;
using AHC.CD.WebUI.MVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.IO;
using AHC.CD.Business.Credentialing.PlanManager;
using AHC.CD.WebUI.MVC.Areas.Credentialing.Models.Loading;
using AHC.CD.Exceptions;
using AHC.CD.Entities.Credentialing.LoadingInformation;
using AHC.CD.Business.Credentialing.Loading;
using AHC.CD.Business.DocumentWriter;
using AHC.CD.Entities.Notification;
using AHC.CD.Business.Notification;
using AHC.CD.Business.MasterData;
using AHC.CD.Business.PackageGeneration;
using AHC.CD.Entities.PackageGenerate;
using AHC.CD.Entities.Credentialing.Loading;
using AHC.CD.Data.Repository;
using AHC.CD.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using AHC.CD.WebUI.MVC.Areas.Credentialing.Models.Timeline;
using AHC.CD.Entities.EmailNotifications;
using AHC.CD.WebUI.MVC.Models.EmailService;
using AHC.CD.Business.Email;
using AHC.MailService;
using AHC.CD.WebUI.MVC.CustomHelpers;
using System.Transactions;
using AHC.CD.Entities.Credentialing.DTO;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Controllers
{
    public class CnDController : Controller
    {
        private IPlanManager planManager = null;
        private IApplicationRepositoryManager formManager = null;
        private IAppointmentManager appointmentManager = null;
        private IApplicationManager applicationManager = null;
        private IProfileManager profileManager = null;
        private IErrorLogger errorLogger = null;
        private ICredentialingContractManager credentialingContractManager = null;
        private IChangeNotificationManager notificationManager;
        private IMasterDataManager masterDataManager;
        private IPackageGeneratorManager packageGeneratorManager;
        private IEmailServiceManager emailServiceManager = null;
        IUnitOfWork uow = null;

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

        public CnDController(IUnitOfWork uow, IEmailServiceManager emailServiceManager, IPackageGeneratorManager packageGeneratorManager, IMasterDataManager masterDataManager, ICredentialingContractManager credentialingContractManager, IChangeNotificationManager notificationManager, IApplicationManager applicationManager, IApplicationRepositoryManager formManager, IProfileManager profileManager, IAppointmentManager appointmentManager, IPlanManager planManager, IErrorLogger errorLogger)
        {
            this.uow = uow;
            this.emailServiceManager = emailServiceManager;
            this.packageGeneratorManager = packageGeneratorManager;
            this.applicationManager = applicationManager;
            this.formManager = formManager;
            this.profileManager = profileManager;
            this.errorLogger = errorLogger;
            this.appointmentManager = appointmentManager;
            this.notificationManager = notificationManager;
            this.planManager = planManager;
            this.credentialingContractManager = credentialingContractManager;
            this.masterDataManager = masterDataManager;
        }

        // GET: Credentialing/Index

        public async Task<ActionResult> Index(int id)
        {
            AHC.CD.Entities.MasterProfile.Profile profileData = null;
            profileData = await profileManager.GetByIdAsync(id);

            ViewBag.profileData = Json(new { profileData }, JsonRequestBehavior.AllowGet);
            return View();
        }
        public ActionResult PrintCheckList()
        {
            return PartialView();
        }
        //---- Credentialing Action Appointment -----------
        public ActionResult CredentialingAppointment()
        {
            return View();
        }

        //---- Auditing -----------
        public ActionResult Auditing()
        {
            return View();
        }

        public async Task<ActionResult> Application(int id)
        {
            try
            {
                ViewBag.CredentialingInfoID = id;

                ViewBag.CredentialingInfo = JsonConvert.SerializeObject(await applicationManager.GetCredentialingInfoByIdAsync(id),
                        new JsonSerializerSettings
                        {
                            DateTimeZoneHandling = DateTimeZoneHandling.Local
                        });
                ViewBag.CredentialingFilterInfo = JsonConvert.SerializeObject(await applicationManager.GetCredentialingFilterInfoByIdAsync(id),
                        new JsonSerializerSettings
                        {
                            DateTimeZoneHandling = DateTimeZoneHandling.Local
                        });
                ViewBag.Roles = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().GetRoles(User.Identity.GetUserId());
                return View("Index");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<JsonResult> ApplicationRepository(int profileId, string template)
        {
            var status = "true";
            var path = "";
            //var template = "AHC Provider Profile for Wellcare - BLANK_new.pdf";
            try
            {

                path = await formManager.GetProfileDataByIdAsync(profileId, template);

            }
            catch (Exception ex)
            {
                errorLogger.LogError(ex);
                status = ExceptionMessage.PROFILE_PDF_CREATION_EXCEPTION;
            }

            return Json(new { status = status, path = path }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public async Task<JsonResult> CCMAction(int credentialingInfoID, CredentialingAppointmentDetailViewModel credentialingAppointmentDetail)
        {
            var status = "true";
            CredentialingAppointmentDetail data = null;
            CredentialingAppointmentDetail dataCredentialingAppointmentDetail = null;
            try
            {
                dataCredentialingAppointmentDetail = AutoMapper.Mapper.Map<CredentialingAppointmentDetailViewModel, CredentialingAppointmentDetail>(credentialingAppointmentDetail, dataCredentialingAppointmentDetail);
                DocumentDTO document = CreateDocument(credentialingAppointmentDetail.FileUpload);
                data = await appointmentManager.UpdateAppointmentDetails(credentialingInfoID, document, dataCredentialingAppointmentDetail, await GetUserAuthId());
            }
            catch (Exception ex)
            {
                errorLogger.LogError(ex);
                status = ExceptionMessage.PROFILE_PDF_CREATION_EXCEPTION;
            }
            return Json(new { status = status, data = data }, JsonRequestBehavior.AllowGet);
        }

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

        [HttpPost]
        public async Task<JsonResult> SetAppointment(int[] CredentialingInfoIDs, int[] ProviderIDArray, string AppointmentDate)
        {
            var status = "true";
            List<int> scheduledAppointments = null;
            //using (TransactionScope tran = new TransactionScope())
            //{
                try
                {
                    var CCMIds = await profileManager.GetCCMUserID();

                    List<int?> profileIds = await masterDataManager.GetAllProfileIDsFromCredentialingInfoIDsAsync(CredentialingInfoIDs);

                    if (profileIds != null && CCMIds != null)
                    {
                        await notificationManager.SaveNotificationDetailsAsyncForCCM(profileIds, CCMIds, AppointmentDate, User.Identity.Name);
                    }
                    //foreach (int profileId in profileIds)
                    //{
                    //    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "CCM Appointment", "Scheduled");

                    //    await notificationManager.SaveNotificationDetailAsync(notification);
                    //}

                    scheduledAppointments = await appointmentManager.ScheduleAppointmentForMany(ProviderIDArray.ToList(), DateTime.ParseExact(AppointmentDate, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture), await GetUserAuthId());
                    //tran.Complete();
                }
                catch (Exception ex)
                {
                    //tran.Dispose();
                    errorLogger.LogError(ex);
                    status = ExceptionMessage.PROFILE_PDF_CREATION_EXCEPTION;
                }
            //}


            return Json(new { status = status, data = scheduledAppointments }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public async Task<JsonResult> RemoveAppointment(int ProviderID, int ProfileID, string AppointmentDate)
        {
            var status = "true";
            //using (TransactionScope tran = new TransactionScope())
            //{
                try
                {
                    var CCMIds = await profileManager.GetCCMUserID();
                    int id = await appointmentManager.RemoveScheduledAppointmentForIndividual(ProviderID, await GetUserAuthId());
                    List<int> profilesID = new List<int>();
                    profilesID.Add(ProfileID);
                    await notificationManager.CancelMeetingNotificationAsyncForCCM(profilesID, CCMIds, AppointmentDate, User.Identity.Name);
                    //tran.Complete();
                }
                catch (Exception ex)
                {
                    //tran.Dispose();
                    errorLogger.LogError(ex);
                    status = ExceptionMessage.PROFILE_PDF_CREATION_EXCEPTION;
                }
            //}
            return Json(new { status = status, data = ProviderID }, JsonRequestBehavior.AllowGet);

        }

        public async Task<string> GetPlanAsync(int planId)
        {
            string UserAuthId = await GetUserAuthId();
            //int CDUserId = profileManager.GetCredentialingUserId(UserAuthId);
            //bool isCCO = await GetUserRole();
            return JsonConvert.SerializeObject(await planManager.GetPlanDetail(planId));

        }

        public async Task<ActionResult> GetCredentialingInfoAsync(int credInfoID)
        {
            string UserAuthId = await GetUserAuthId();
            //int CDUserId = profileManager.GetCredentialingUserId(UserAuthId);
            //bool isCCO = await GetUserRole();
            var credentialingInfo = await applicationManager.GetCredentialingInfoById(credInfoID);
            return Json(credentialingInfo, JsonRequestBehavior.AllowGet);

        }

        private async Task<string> GetUserAuthId()
        {
            var currentUser = HttpContext.User.Identity.Name;
            var appUser = new ApplicationUser() { UserName = currentUser };
            var user = await AuthUserManager.FindByNameAsync(appUser.UserName);

            return user.Id;
        }

        public async Task<ActionResult> GetAllCredentialInfoList()
        {
            var data = await appointmentManager.GetAllCredentialInfoList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetAllCredentialInfoListHistory()
        {
            var data = await appointmentManager.GetAllCredentialInfoListHistory();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<string> GetContractGrid(int credentialingInfoID)
        {
            var data = await credentialingContractManager.GetContractGridForCredentialingInfo(credentialingInfoID);
            return JsonConvert.SerializeObject(data);
        }
        public async Task<string> GetContractGridById(int ContractGridID)
        {
            try
            {
                var data = await credentialingContractManager.GetContractGridById(ContractGridID);
                return JsonConvert.SerializeObject(data);
            }
            catch (Exception ex )
            {
                
                throw ex;
            }
        }

        /// <summary>
        /// Function to Get List of Contract Grid using EF
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetAllContractGrid()
        {
            IEnumerable<CredentialingInfo> data = null;

            bool isPRO = await GetUserRole();
            string UserAuthId = await GetUserAuthId();
            int ProfileID = Convert.ToInt32(GetCredentialingUserId(UserAuthId));

            if (isPRO)
            {
                data = await credentialingContractManager.GetAllContractGridByID(ProfileID);
            }
            else
            {
                data = await credentialingContractManager.GetAllContractGrid();
            }

            return JsonConvert.SerializeObject(data);
        }

        /// <summary>
        /// Function to Get List of Contract Grid using ADO
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetAllContractGridADO()
        {
            IEnumerable<AHC.CD.Data.ADO.DTO.ContractGridDTO> data = null;

            bool isPRO = await GetUserRole();
            string UserAuthId = await GetUserAuthId();
            int ProfileID = Convert.ToInt32(GetCredentialingUserId(UserAuthId));

            if (isPRO)
            {
                data = await credentialingContractManager.GetAllContractGridDTO();
            }
            else
            {
                data = await credentialingContractManager.GetAllContractGridDTO();
            }
            
            return JsonConvert.SerializeObject(data);
        }

        /// <summary>
        /// Function to get a list of Participating Status List
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetAllParticipatingStatus()
        {
            try
            {
                var data = await credentialingContractManager.GetAllParticipatingStatus();
                return JsonConvert.SerializeObject(data);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<JsonResult> RemoveRequestAndGrid(int credentialingContractRequestID)
        {
            string status = "true";
            try
            {
                await credentialingContractManager.RemoveRequestAndGrid(credentialingContractRequestID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(new { status = status, credentialingContractRequestID = credentialingContractRequestID }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> RemoveGrid(int contractGridID)
        {
            string status = "true";
            try
            {
                await credentialingContractManager.RemoveGrid(contractGridID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(new { status = status, contractGridID = contractGridID }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> QuickSaveReport(ContractGridViewModel contractGrid)
        {
            string status = "true";
            string UserAuthId = await GetUserAuthId();
            ContractGrid dataContractGrid = new ContractGrid();
            try
            {
                dataContractGrid.ContractGridID = contractGrid.ContractGridID;
                dataContractGrid.InitialCredentialingDate = contractGrid.InitialCredentialingDate;
                if (dataContractGrid.Report == null)
                {
                    dataContractGrid.Report = new CredentialingContractInfoFromPlan();
                }
                dataContractGrid.Report.ProviderID = contractGrid.Report.ProviderID;
                dataContractGrid.Report.CredentialingContractInfoFromPlanID = contractGrid.Report.CredentialingContractInfoFromPlanID;
                dataContractGrid = await credentialingContractManager.QuickSaveContractInfoFromPlan(dataContractGrid, UserAuthId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(new { status = status, dataContractGrid = dataContractGrid }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> SaveReport(ContractGridViewModel contractGrid)
        {
            string status = "true";
            string UserAuthId = await GetUserAuthId();
            ContractGrid dataContractGrid = new ContractGrid();
            try
            {
                dataContractGrid.ContractGridID = contractGrid.ContractGridID;
                dataContractGrid.BusinessEntityID = contractGrid.BusinessEntityID;
                dataContractGrid.CredentialingInfoID = contractGrid.CredentialingInfoID;
                dataContractGrid.LOBID = contractGrid.LOBID;
                dataContractGrid.ProfilePracticeLocationID = contractGrid.ProfilePracticeLocationID;
                dataContractGrid.ProfileSpecialtyID = contractGrid.ProfileSpecialtyID;
                dataContractGrid.InitialCredentialingDate = contractGrid.InitialCredentialingDate;
                if (dataContractGrid.Report == null)
                {
                    dataContractGrid.Report = new CredentialingContractInfoFromPlan();
                }
                dataContractGrid.Report.AdminFee = contractGrid.Report.AdminFee;
                dataContractGrid.Report.CAP = contractGrid.Report.CAP;
                dataContractGrid.Report.CredentialedDate = contractGrid.Report.CredentialedDate;
                dataContractGrid.Report.CredentialingApprovalStatusType = contractGrid.Report.CredentialingApprovalStatusType;
                dataContractGrid.Report.GroupID = contractGrid.Report.GroupID;
                dataContractGrid.Report.InitiatedDate = contractGrid.Report.InitiatedDate;
                dataContractGrid.Report.PanelStatusType = contractGrid.Report.PanelStatusType;
                dataContractGrid.Report.PercentageOfRisk = contractGrid.Report.PercentageOfRisk;
                dataContractGrid.Report.StopLossFee = contractGrid.Report.StopLossFee;
                dataContractGrid.Report.TerminationDate = contractGrid.Report.TerminationDate;
                dataContractGrid.Report.WelcomeLetterPath = contractGrid.Report.WelcomeLetterPath;
                dataContractGrid.Report.ProviderID = contractGrid.Report.ProviderID;
                dataContractGrid.Report.CredentialingContractInfoFromPlanID = contractGrid.Report.CredentialingContractInfoFromPlanID;
                dataContractGrid.Report.ReCredentialingDate = contractGrid.Report.ReCredentialingDate;
                dataContractGrid.Report.ParticipatingStatus = contractGrid.Report.ParticipatingStatus;

                DocumentDTO welcomeLetterDocument = null;
                if (contractGrid.Report.WelcomeLetter != null)
                {
                    welcomeLetterDocument = CreateDocument(contractGrid.Report.WelcomeLetter);
                }

                string ReasonForPanelChange = contractGrid.ReasonForPanelChange;

                dataContractGrid = await credentialingContractManager.AddContractInfoFromPlan(dataContractGrid, welcomeLetterDocument, UserAuthId, ReasonForPanelChange);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(new { status = status, dataContractGrid = dataContractGrid }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<string> GetPlanContractReport(int ReportID)
        {
            var data = new CredentialingContractInfoFromPlan();
            try
            {
                data = await credentialingContractManager.ViewContractInfoFromPlan(ReportID);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return JsonConvert.SerializeObject(data);
        }
        [HttpPost]
        public async Task<ActionResult> AddLoadedData(int credentialingInfoID, CredentialingContractRequestViewModel tempObject)
        {
            string status = "true";

            CredentialingContractRequest dataCredentialingContractRequest = new CredentialingContractRequest();
            try
            {
                if (tempObject.ContractLOBs != null)
                {
                    foreach (var item in tempObject.ContractLOBs)
                    {
                        if (dataCredentialingContractRequest.ContractLOBs == null)
                        {
                            dataCredentialingContractRequest.ContractLOBs = new List<ContractLOB>();
                        }
                        ContractLOB addContractLOB = new ContractLOB();
                        addContractLOB.LOBID = item.LOBID;
                        dataCredentialingContractRequest.ContractLOBs.Add(addContractLOB);
                    }
                }
                if (tempObject.ContractPracticeLocations != null)
                {
                    foreach (var item in tempObject.ContractPracticeLocations)
                    {
                        if (dataCredentialingContractRequest.ContractPracticeLocations == null)
                        {
                            dataCredentialingContractRequest.ContractPracticeLocations = new List<ContractPracticeLocation>();
                        }
                        ContractPracticeLocation addContractPracticeLocation = new ContractPracticeLocation();
                        addContractPracticeLocation.ProfilePracticeLocationID = item.ProfilePracticeLocationID;
                        dataCredentialingContractRequest.ContractPracticeLocations.Add(addContractPracticeLocation);
                    }
                }
                if (tempObject.ContractSpecialties != null)
                {
                    foreach (var item in tempObject.ContractSpecialties)
                    {
                        if (dataCredentialingContractRequest.ContractSpecialties == null)
                        {
                            dataCredentialingContractRequest.ContractSpecialties = new List<ContractSpecialty>();
                        }
                        ContractSpecialty addContractSpecialty = new ContractSpecialty();
                        addContractSpecialty.ProfileSpecialtyID = item.ProfileSpecialtyID;
                        dataCredentialingContractRequest.ContractSpecialties.Add(addContractSpecialty);
                    }
                }
                dataCredentialingContractRequest.BusinessEntityID = tempObject.BusinessEntityID;
                dataCredentialingContractRequest.AllLOBsSelectedYesNoOption = tempObject.AllLOBsSelectedYesNoOption;
                dataCredentialingContractRequest.AllPracticeLocationsSelectedYesNoOption = tempObject.AllPracticeLocationsSelectedYesNoOption;
                dataCredentialingContractRequest.AllSpecialtiesSelectedYesNoOption = tempObject.AllSpecialtiesSelectedYesNoOption;
                dataCredentialingContractRequest.InitialCredentialingDate = tempObject.InitialCredentialingDate;
                dataCredentialingContractRequest.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;
                dataCredentialingContractRequest.ContractRequestStatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;
                dataCredentialingContractRequest = await credentialingContractManager.AddCredentialingContractRequest(credentialingInfoID, dataCredentialingContractRequest, await GetUserAuthId());
                if (dataCredentialingContractRequest.ContractGrid != null)
                {
                    foreach (var item in dataCredentialingContractRequest.ContractGrid)
                    {
                        item.CredentialingInfo.CredentialingContractRequests = null;
                    }
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
                status = ExceptionMessage.INITIATE_CREDENTIALING_EXCEPTION;
            }

            return Json(new { status = status, dataCredentialingContractRequest = dataCredentialingContractRequest }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> SubmitSPA(int credentialingInfoID)
        {
            string status;
            try
            {
                await applicationManager.SetCredentialingInfoStatusById(credentialingInfoID, await GetUserAuthId());
                status = "true";

            }
            catch (Exception)
            {
                throw;
            }

            return Json(new { status = status }, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> ContractInfo()
        {
            ViewBag.ContractsData = await GetAllContractGridADO();
            ViewBag.Roles = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().GetRoles(User.Identity.GetUserId());
            return View();
        }
        public async Task<string> GetAllDocuments(int profileID)
        {
            var data = await packageGeneratorManager.GetAllDocuments(profileID);
            return JsonConvert.SerializeObject(data);
        }

        public async Task<string> AddPackageToContractRequest(int ContractRequestID, string PackageGeneratorReportCode)
        {
            try
            {
                PackageGeneratorReport packageGeneratorReport = await packageGeneratorManager.AddPackageGeneratorReport(ContractRequestID, PackageGeneratorReportCode);
                return JsonConvert.SerializeObject(packageGeneratorReport);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> GeneratePackage(int profileId, int LastCount, List<string> pdflist, int planId)
        {
            try
            {
                string UserAuthId = await GetUserAuthId();
                PackageGenerator packageGenerator = new PackageGenerator();
                packageGenerator = packageGeneratorManager.CombinePdfs(profileId, LastCount, pdflist, UserAuthId, planId);
                return JsonConvert.SerializeObject(packageGenerator);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public int? GetCredentialingUserId(string UserAuthId)
        {
            var userRepo = uow.GetGenericRepository<CDUser>();
            var user = userRepo.Find(u => u.AuthenicateUserId == UserAuthId);

            return user.ProfileId;
        }

        [HttpPost]
        public async Task<ActionResult> AddTimelineActivity(int credInfoId, TimelineActivityViewModel timelineActivity)
        {
            string status = "true";
            TimelineActivity dataModelTimelineActivity = null;

            try
            {
                if (ModelState.IsValid)
                {
                    string UserAuthId = await GetUserAuthId();
                    int CDUserId = profileManager.GetCredentialingUserId(UserAuthId);
                    timelineActivity.ActivityByID = CDUserId;

                    dataModelTimelineActivity = AutoMapper.Mapper.Map<TimelineActivityViewModel, TimelineActivity>(timelineActivity);

                    var result = await applicationManager.AddTimelineActivityAsync(credInfoId, dataModelTimelineActivity);
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
                //status = ExceptionMessage.PROFILE_ADD_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, timelineActivity = dataModelTimelineActivity }, JsonRequestBehavior.AllowGet);
        }




        //[HttpPost]
        public async Task<ActionResult> SendEmail(int credRequestId, EmailServiceViewModel email)
        {
            //EmailServiceViewModel email = new EmailServiceViewModel();
            //email.BCC = "dadf";
            //email.Body = "Body";
            //email.CC = "dasdaww";
            //email.IsRecurrenceEnabledYesNoOption = Entities.MasterData.Enums.YesNoOption.NO;
            //email.StatusType = Entities.MasterData.Enums.StatusType.Active;
            //email.Subject = "afsgfrg";
            //email.To = "dewfwe";

            string status = "true";
            EmailInfo dataEmailInfo = null;
            if (email.Body != null)
            {
                email.Body = email.Body.Replace("&lt;", "<");
                email.Body = email.Body.Replace("&gt;", ">");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    dataEmailInfo = AutoMapper.Mapper.Map<EmailServiceViewModel, EmailInfo>(email);
                    dataEmailInfo.SendingDate = DateTime.Now;
                    dataEmailInfo.From = "abcdd1536@gmail.com";
                    if (dataEmailInfo.IsRecurrenceEnabled == Entities.MasterData.Enums.YesNoOption.YES.ToString())
                    {
                        dataEmailInfo.EmailRecurrenceDetail = new EmailRecurrenceDetail();
                        dataEmailInfo.EmailRecurrenceDetail = AutoMapper.Mapper.Map<EmailServiceViewModel, EmailRecurrenceDetail>(email);
                        dataEmailInfo.EmailRecurrenceDetail.IsRecurrenceScheduledYesNoOption = Entities.MasterData.Enums.YesNoOption.NO;
                        if (dataEmailInfo.EmailRecurrenceDetail.RecurrenceIntervalType == Entities.MasterData.Enums.RecurrenceIntervalType.Daily.ToString())
                        {
                            dataEmailInfo.EmailRecurrenceDetail.NextMailingDate = dataEmailInfo.SendingDate.Value.AddDays(dataEmailInfo.EmailRecurrenceDetail.IntervalFactor.Value);
                        }
                        else if (dataEmailInfo.EmailRecurrenceDetail.RecurrenceIntervalType == Entities.MasterData.Enums.RecurrenceIntervalType.Weekly.ToString())
                        {
                            dataEmailInfo.EmailRecurrenceDetail.NextMailingDate = dataEmailInfo.SendingDate.Value.AddDays(7 * dataEmailInfo.EmailRecurrenceDetail.IntervalFactor.Value);
                        }
                        else if (dataEmailInfo.EmailRecurrenceDetail.RecurrenceIntervalType == Entities.MasterData.Enums.RecurrenceIntervalType.Monthly.ToString())
                        {
                            dataEmailInfo.EmailRecurrenceDetail.NextMailingDate = dataEmailInfo.SendingDate.Value.AddMonths(dataEmailInfo.EmailRecurrenceDetail.IntervalFactor.Value);
                        }
                        else if (dataEmailInfo.EmailRecurrenceDetail.RecurrenceIntervalType == Entities.MasterData.Enums.RecurrenceIntervalType.Quarterly.ToString())
                        {
                            dataEmailInfo.EmailRecurrenceDetail.IntervalFactor = 1;
                            dataEmailInfo.EmailRecurrenceDetail.NextMailingDate = dataEmailInfo.SendingDate.Value.AddMonths(4 + dataEmailInfo.EmailRecurrenceDetail.IntervalFactor.Value);
                        }
                        else if (dataEmailInfo.EmailRecurrenceDetail.RecurrenceIntervalType == Entities.MasterData.Enums.RecurrenceIntervalType.Yearly.ToString())
                        {
                            dataEmailInfo.EmailRecurrenceDetail.NextMailingDate = dataEmailInfo.SendingDate.Value.AddYears(dataEmailInfo.EmailRecurrenceDetail.IntervalFactor.Value);
                        }
                        else
                        {
                            dataEmailInfo.EmailRecurrenceDetail.NextMailingDate = email.DateForCustomRecurrence;
                        }
                    }

                    dataEmailInfo.EmailRecipients = new List<EmailRecipientDetail>();
                    List<string> toList = null;
                    email.To = "testingsingh285@gmail.com";
                    toList = email.To.Split(',').ToList();
                    foreach (var to in toList)
                    {
                        EmailRecipientDetail recipient = new EmailRecipientDetail();
                        recipient.Recipient = to;
                        recipient.RecipientTypeCategory = Entities.MasterData.Enums.RecipientType.To;
                        recipient.StatusType = Entities.MasterData.Enums.StatusType.Active;
                        dataEmailInfo.EmailRecipients.Add(recipient);
                    }

                    List<string> ccList = null;
                    if (email.CC != null)
                    {
                        email.CC = "testingsingh285@gmail.com";
                        ccList = email.CC.Split(',').ToList();
                        foreach (var cc in ccList)
                        {
                            EmailRecipientDetail recipient = new EmailRecipientDetail();
                            recipient.Recipient = cc;
                            recipient.RecipientTypeCategory = Entities.MasterData.Enums.RecipientType.CC;
                            recipient.StatusType = Entities.MasterData.Enums.StatusType.Active;
                            dataEmailInfo.EmailRecipients.Add(recipient);
                        }
                    }

                    List<string> bccList = null;
                    if (email.BCC != null)
                    {
                        email.BCC = "testingsingh285@gmail.com";
                        bccList = email.BCC.Split(',').ToList();
                        foreach (var bcc in bccList)
                        {
                            EmailRecipientDetail recipient = new EmailRecipientDetail();
                            recipient.Recipient = bcc;
                            recipient.RecipientTypeCategory = Entities.MasterData.Enums.RecipientType.BCC;
                            recipient.StatusType = Entities.MasterData.Enums.StatusType.Active;
                            dataEmailInfo.EmailRecipients.Add(recipient);
                        }
                    }

                    List<EmailAttachment> allAttachments = await applicationManager.GetGeneratedPackagesAsync(credRequestId);

                    if (allAttachments != null && allAttachments.Count > 0)
                    {
                        dataEmailInfo.EmailAttachments = new List<EmailAttachment>();
                        dataEmailInfo.EmailAttachments = allAttachments;
                    }

                    await emailServiceManager.SaveComposedEmail(dataEmailInfo);
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
                status = ExceptionMessage.EMAIL_SENT_EXCEPTION;
            }
            return Json(new { status = status }, JsonRequestBehavior.AllowGet);
        }

        #region Private Methods

        //private async Task<string> GetUserAuthId()
        //{
        //    var currentUser = HttpContext.User.Identity.Name;
        //    var appUser = new ApplicationUser() { UserName = currentUser };
        //    var user = await AuthUserManager.FindByNameAsync(appUser.UserName);

        //    return user.Id;
        //}

        private async Task<bool> GetUserRole()
        {
            var currentUser = HttpContext.User.Identity.Name;
            var appUser = new ApplicationUser() { UserName = currentUser };
            var user = await AuthUserManager.FindByNameAsync(appUser.UserName);
            var Role = RoleManager.Roles.FirstOrDefault(r => r.Name == "PRO");

            return user.Roles.Any(r => r.RoleId == Role.Id);
        }

        #endregion
    }
}