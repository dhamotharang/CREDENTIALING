using AHC.CD.Business.Profiles;
using AHC.CD.Data.Repository;
using AHC.CD.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using AHC.CD.WebUI.MVC.Models;
using AHC.CD.WebUI.MVC.CustomHelpers;
using AHC.CD.Entities.MasterData.Enums;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Controllers
{
    public class RequestForApprovalController : Controller
    {
        private readonly IRequestForApprovalManager iRequestForApprovalManager = null;

        public RequestForApprovalController(IRequestForApprovalManager iRequestForApprovalManager)
        {
            this.iRequestForApprovalManager = iRequestForApprovalManager;
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

        [HttpGet]
        [Authorize(Roles = "CCO,PRO,CRA")]
        public async Task<ActionResult> Index()
        {
            ViewBag.IsProvider = await GetUserRole();
            return View();
        }

        [HttpGet]
        [AjaxAction]
        [Authorize(Roles = "CCO,PRO,CRA")]
        public async Task<string> GetAllUpdatesAndRenewals()
        {
            dynamic UpdateAndRenwalDTO = null;
            try
            {
                bool isPRO = await GetUserRole();
                ViewBag.providerorCCO = isPRO;
                if (isPRO)
                {
                    string UserAuthId = await GetUserAuthId();
                    int ProfileID = Convert.ToInt32(await iRequestForApprovalManager.GetProfileID(UserAuthId));
                    UpdateAndRenwalDTO = await iRequestForApprovalManager.GetAllUpdatesAndRenewalsForProviderAsync(ProfileID);
                }
                else{
                    UpdateAndRenwalDTO = await iRequestForApprovalManager.GetAllUpdatesAndRenewalsAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return JsonConvert.SerializeObject(UpdateAndRenwalDTO);
        }

        [HttpGet]
        [AjaxAction]
        [Authorize(Roles = "CCO,PRO,CRA")]
        public async Task<string> GetAllCredentialingRequest()
        {
            dynamic CredentialRequestDTO = null;
            try
            {
                bool isPRO = await GetUserRole();
                if (isPRO)
                {
                    string UserAuthId = await GetUserAuthId();
                    int ProfileID = Convert.ToInt32(await iRequestForApprovalManager.GetProfileID(UserAuthId));
                    CredentialRequestDTO = await iRequestForApprovalManager.GetAllCredentialRequestsForProviderAsync(ProfileID);
                }
                else
                {
                    CredentialRequestDTO = await iRequestForApprovalManager.GetAllCredentialRequestsAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return JsonConvert.SerializeObject(CredentialRequestDTO);
        }

        [HttpGet]
        [AjaxAction]
        [Authorize(Roles = "CCO,PRO,CRA")]
        public async Task<string> GetAllHistory()
        {
            dynamic HistoryDTO = null;
            try
            {
                bool isPRO = await GetUserRole();
                if (isPRO)
                {
                    string UserAuthId = await GetUserAuthId();
                    int ProfileID = Convert.ToInt32(await iRequestForApprovalManager.GetProfileID(UserAuthId));
                    HistoryDTO = await iRequestForApprovalManager.GetAllHistoryForProviderAsync(ProfileID);                    
                }
                else
                {
                    HistoryDTO = await iRequestForApprovalManager.GetAllHistoryAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return JsonConvert.SerializeObject(HistoryDTO);
        }

        [HttpGet]
        [AjaxAction]
        [Authorize(Roles = "CCO,PRO,CRA")]
        public async Task<string> GetCredRequestDataByID(int ID)
        {
            dynamic CredRequestDataDTO = null;
            try
            {
                CredRequestDataDTO = await iRequestForApprovalManager.GetCredRequestDataByIDAsync(ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return JsonConvert.SerializeObject(CredRequestDataDTO);
        }

        [HttpGet]
        [AjaxAction]
        [Authorize(Roles = "CCO,PRO,CRA")]
        public async Task<string> GetCredRequestHistoryDataByID(int ID)
        {
            dynamic CredRequestHistoryDataDTO = null;
            try
            {
                CredRequestHistoryDataDTO = await iRequestForApprovalManager.GetCredRequestHistoryDataByIDAsync(ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return JsonConvert.SerializeObject(CredRequestHistoryDataDTO);
        }

        [HttpPost]
        [AjaxAction]
        [Authorize(Roles = "CCO,CRA,PRO")]
        public async Task<string> SetDecesionForCredRequestByID(int ID, string ApprovalType,string Reason)
        {
            bool Status = false;
            try
            {
                Status = await iRequestForApprovalManager.SetDecesionForCredRequestByIDAsync(ID, ApprovalType, Reason, await GetUserAuthId());
                List<int> credIDs = new List<int>();
                credIDs.Add(ID);
                await iRequestForApprovalManager.AddCredRequestTrackerNotification(credIDs, ApprovalType, HttpContext.User.Identity.Name);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return JsonConvert.SerializeObject(Status);
        }

        [HttpPost]
        [AjaxAction]
        [Authorize(Roles = "CCO,CRA")]
        public async Task<string> SetMultipleApproval(List<int> ProfileUpdatesTrackerIds)
        {
            bool Status = false;
            try
            {
                Status = await iRequestForApprovalManager.SetApprovalByIDs(ProfileUpdatesTrackerIds, await GetUserAuthId());
                await iRequestForApprovalManager.AddUpdatesRequestTrackerNotification(ProfileUpdatesTrackerIds, ApprovalStatusType.Approved.ToString(), HttpContext.User.Identity.Name);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return JsonConvert.SerializeObject(Status);
        }

        [HttpGet]
        [AjaxAction]
        [Authorize(Roles = "CCO,PRO,CRA")]
        public async Task<string> GetAllUpdateHistory()
        {
            dynamic HistoryDTO = null;
            try
            {
                bool isPRO = await GetUserRole();
                if (isPRO)
                {
                    string UserAuthId = await GetUserAuthId();
                    int ProfileID = Convert.ToInt32(await iRequestForApprovalManager.GetProfileID(UserAuthId));
                    HistoryDTO = await iRequestForApprovalManager.GetAllUpdateRequestHistoryByIDAsync(ProfileID);
                }
                else
                {
                    HistoryDTO = await iRequestForApprovalManager.GetAllUpdateRequestHistoryAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return JsonConvert.SerializeObject(HistoryDTO);
        }

        [HttpGet]
        [AjaxAction]
        [Authorize(Roles = "CCO,PRO,CRA")]
        public async Task<string> GetAllRenewalHistory()
        {
            dynamic HistoryDTO = null;
            try
            {
                bool isPRO = await GetUserRole();
                if (isPRO)
                {
                    string UserAuthId = await GetUserAuthId();
                    int ProfileID = Convert.ToInt32(await iRequestForApprovalManager.GetProfileID(UserAuthId));
                    HistoryDTO = await iRequestForApprovalManager.GetAllRenewalRequestHistoryByIDAsync(ProfileID);
                }
                else
                {
                    HistoryDTO = await iRequestForApprovalManager.GetAllRenewalRequestHistoryAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return JsonConvert.SerializeObject(HistoryDTO);
        }

        [HttpGet]
        [AjaxAction]
        [Authorize(Roles = "CCO,PRO,CRA")]
        public async Task<string> GetAllCredRequestHistory()
        {
            dynamic HistoryDTO = null;
            try
            {
                bool isPRO = await GetUserRole();
                if (isPRO)
                {
                    string UserAuthId = await GetUserAuthId();
                    int ProfileID = Convert.ToInt32(await iRequestForApprovalManager.GetProfileID(UserAuthId));
                    HistoryDTO = await iRequestForApprovalManager.GetAllCredRequestHistoryByIDAsync(ProfileID);
                }
                else
                {
                    HistoryDTO = await iRequestForApprovalManager.GetAllCredRequestHistoryAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return JsonConvert.SerializeObject(HistoryDTO);
        }


        [HttpPost]
        [AjaxAction]
        [Authorize(Roles = "CCO,CRA")]
        public async Task<string> SetMultipleApprovalForCredRequest(string CredentialingRequestIDs)
        {
            Object ApprovalForCredRequests = null;
            try
            {
                ApprovalForCredRequests = await iRequestForApprovalManager.SetApprovalForCredRequestByIDsAsync(CredentialingRequestIDs, await GetUserAuthId());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return JsonConvert.SerializeObject(ApprovalForCredRequests);
        }
        

        #region Private Methods

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
            var Role = RoleManager.Roles.FirstOrDefault(r => r.Name == "PRO");

            return user.Roles.Any(r => r.RoleId == Role.Id);
        }

        #endregion


    }
}