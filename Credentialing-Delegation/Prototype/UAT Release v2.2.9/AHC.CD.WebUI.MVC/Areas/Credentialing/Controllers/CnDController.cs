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
using Microsoft.AspNet.Identity.Owin;
using System.IO;
using AHC.CD.Business.Credentialing.PlanManager;
using AHC.CD.WebUI.MVC.Areas.Credentialing.Models.Loading;
using AHC.CD.Exceptions;
using AHC.CD.Entities.Credentialing.LoadingInformation;
using AHC.CD.Business.Credentialing.Loading;
using AHC.CD.Business.DocumentWriter;

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

        public CnDController(ICredentialingContractManager credentialingContractManager, IApplicationManager applicationManager, IApplicationRepositoryManager formManager, IProfileManager profileManager, IAppointmentManager appointmentManager,IPlanManager planManager, IErrorLogger errorLogger)
        {
            this.applicationManager = applicationManager;
            this.formManager = formManager;
            this.profileManager = profileManager;
            this.errorLogger = errorLogger;
            this.appointmentManager = appointmentManager;
            this.planManager = planManager;
            this.credentialingContractManager = credentialingContractManager;
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
                ViewBag.CredentialingInfo = JsonConvert.SerializeObject(await applicationManager.GetCredentialingInfoByIdAsync(id));
                ViewBag.CredentialingFilterInfo = JsonConvert.SerializeObject(await applicationManager.GetCredentialingFilterInfoByIdAsync(id));
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
        public async Task<JsonResult> AddApplication(int profileId, string path)
        {
            var status = "true";
            

            try
            {
                //path = await formManager.GetProfileDataByIdAsync(profileId, path);

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
                data = await appointmentManager.UpdateAppointmentDetails(credentialingInfoID,document, dataCredentialingAppointmentDetail, await GetUserAuthId());                
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
        public async Task<JsonResult> SetAppointment(int[] ProviderIDArray, string AppointmentDate)
        {
            var status = "true";
            List<int> scheduledAppointments = null;

            try
            {
                scheduledAppointments = await appointmentManager.ScheduleAppointmentForMany(ProviderIDArray.ToList(), DateTime.ParseExact(AppointmentDate, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture), await GetUserAuthId());
            }
            catch (Exception ex)
            {
                errorLogger.LogError(ex);
                status = ExceptionMessage.PROFILE_PDF_CREATION_EXCEPTION;
            }

            return Json(new { status = status, data = scheduledAppointments }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public async Task<JsonResult> RemoveAppointment(int ProviderID)
        {
            var status = "true";

            try
            {
                int id = await appointmentManager.RemoveScheduledAppointmentForIndividual(ProviderID, await GetUserAuthId());
            }
            catch (Exception ex)
            {
                errorLogger.LogError(ex);
                status = ExceptionMessage.PROFILE_PDF_CREATION_EXCEPTION;
            }

            return Json(new { status = status, data = ProviderID }, JsonRequestBehavior.AllowGet);

        }

        public async Task<string> GetPlanAsync(int planId)
        {
            string UserAuthId = await GetUserAuthId();
            //int CDUserId = profileManager.GetCredentialingUserId(UserAuthId);
            //bool isCCO = await GetUserRole();
            return JsonConvert.SerializeObject(await planManager.GetPlanDetail(planId));

        }

        public async Task<ActionResult> GetCredentialingInfoAsync(int profileId, int planId)
        {
            string UserAuthId = await GetUserAuthId();
            //int CDUserId = profileManager.GetCredentialingUserId(UserAuthId);
            //bool isCCO = await GetUserRole();
            var credentialingInfo = await applicationManager.GetCredentialingInfoById(profileId, planId);
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

        public async Task<JsonResult> GetContractGrid(int credentialingInfoID)
        {
            var data = await credentialingContractManager.GetContractGridForCredentialingInfo(credentialingInfoID);
            return Json(data, JsonRequestBehavior.AllowGet);
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
                dataContractGrid = await credentialingContractManager.QuickSaveContractInfoFromPlan(dataContractGrid);
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

                DocumentDTO welcomeLetterDocument = null;
                if (contractGrid.Report.WelcomeLetter != null)
                {
                    welcomeLetterDocument = CreateDocument(contractGrid.Report.WelcomeLetter);
                }                

                dataContractGrid = await credentialingContractManager.AddContractInfoFromPlan(dataContractGrid, welcomeLetterDocument);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(new { status = status, dataContractGrid = dataContractGrid }, JsonRequestBehavior.AllowGet);
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
    }
}