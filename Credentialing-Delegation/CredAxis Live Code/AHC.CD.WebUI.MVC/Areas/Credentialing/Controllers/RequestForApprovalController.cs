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
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "CCO,PRO,CRA")]
        public async Task<string> GetAllUpdatesAndRenewals()
        {
            dynamic UpdateAndRenwalDTO = null;
            try
            {
                UpdateAndRenwalDTO = await iRequestForApprovalManager.GetAllUpdatesAndRenewalsAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return JsonConvert.SerializeObject(UpdateAndRenwalDTO);
        }

        [HttpGet]
        [Authorize(Roles = "CCO,PRO,CRA")]
        public async Task<string> GetAllCredentialingRequest()
        {
            dynamic CredentialRequestDTO = null;
            try
            {
                CredentialRequestDTO = await iRequestForApprovalManager.GetAllCredentialRequestsAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return JsonConvert.SerializeObject(CredentialRequestDTO);
        }

        [HttpGet]
        [Authorize(Roles = "CCO,PRO,CRA")]
        public async Task<string> GetAllHistory()
        {
            dynamic HistoryDTO = null;
            try
            {
                HistoryDTO = await iRequestForApprovalManager.GetAllHistoryAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return JsonConvert.SerializeObject(HistoryDTO);
        }

        [HttpGet]
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
        [Authorize(Roles = "CCO,PRO,CRA")]
        public async Task<string> SetDecesionForCredRequestByID(int ID, string ApprovalType,string Reason)
        {
            bool Status = false;
            try
            {
                Status = await iRequestForApprovalManager.SetDecesionForCredRequestByIDAsync(ID, ApprovalType, Reason);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return JsonConvert.SerializeObject(Status);
        }

    }
}