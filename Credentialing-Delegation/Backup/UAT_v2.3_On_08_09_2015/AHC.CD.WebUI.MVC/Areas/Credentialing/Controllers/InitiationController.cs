using AHC.CD.Business.Credentialing;
using AHC.CD.Business.Search;
using AHC.CD.Entities.Credentialing.Loading;
using AHC.CD.WebUI.MVC.Areas.Credentialing.Models.Initiation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using AHC.CD.WebUI.MVC.Models;
using AHC.CD.Exceptions;
using AHC.CD.ErrorLogging;
using AHC.CD.Resources.Messages;
using AHC.CD.Entities.Credentialing.LoadingInformation;
using Newtonsoft.Json;
using AHC.CD.Entities.Credentialing;
using Microsoft.Owin;
using AHC.CD.Business;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Controllers
{
    public class InitiationController : Controller
    {
        //
        private ISearchManager searchManager = null;
        private IIndividualCredentialingManager individualCredentialingManager = null;
        private IErrorLogger errorLogger = null;
        private IProfileManager profileManager = null;

        protected ApplicationUserManager _authUserManager;
        protected ApplicationUserManager AuthUserManager
        {
            get
            {
                return _authUserManager ?? System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _authUserManager = value;
            }
        }

        public InitiationController(IProfileManager profileManager, ISearchManager searchManager, IErrorLogger errorLogger, IIndividualCredentialingManager individualCredentialingManager)
        {
            this.profileManager = profileManager;
            this.errorLogger = errorLogger;
            this.searchManager = searchManager;
            this.individualCredentialingManager = individualCredentialingManager;
        }

        // GET: /Credentialing/Initiation/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CredentialingList()
        {
            return View();
        }

        public ActionResult DeCredentialingList()
        {
            return View();
        }

        [HttpPost]
        public string SearchProviderJson(int id, SearchProviderForCred search)
        {
            //ProviderSearchRequestDTO searchRequest = null;
            //string status = "true";
            List<SearchResultForCred> searchResults2 = null;
            List<CredentialingInfo> searchResults1 = null;
            switch (id)
            {
                case 1:
                    searchResults2 = searchManager.SearchProviderProfileForCred(search.NPINumber, search.FirstName, search.LastName, search.Specialty, search.IPAGroupName, search.CAQH, search.ProviderType);
                    return JsonConvert.SerializeObject(searchResults2);

                case 2:
                    searchResults1 = searchManager.SearchProviderProfileForReCred(search.NPINumber, search.FirstName, search.LastName, search.Specialty, search.IPAGroupName, search.CAQH, search.ProviderType);
                    return JsonConvert.SerializeObject(searchResults1);
                //return Json(new { searchResults = searchResults1 }, JsonRequestBehavior.AllowGet);

                case 3:
                    searchResults1 = searchManager.SearchProviderProfileForDeCred(search.NPINumber, search.FirstName, search.LastName, search.Specialty, search.IPAGroupName, search.CAQH, search.ProviderType);
                    return JsonConvert.SerializeObject(searchResults1);

                default: break;

            }

            return JsonConvert.SerializeObject("");
        }

        [HttpPost]
        public JsonResult InitiateCredentialing(CredentialingInitiationInfoViewModel credentialingInitiationInfo)
        {
            string status = "true";
            credentialingInitiationInfo.InitiationDate = DateTime.Now;
            CredentialingInfo dataCredentialingInitiationInfo = null;
            dataCredentialingInitiationInfo = AutoMapper.Mapper.Map<CredentialingInitiationInfoViewModel, CredentialingInfo>(credentialingInitiationInfo);
            try
            {
                if (credentialingInitiationInfo.PlanID == null)
                {
                    throw new AHC.CD.Exceptions.Credentialing.CredentialingException(ExceptionMessage.NO_PLAN_EXCEPTION);
                }
                else
                    individualCredentialingManager.InitiateCredentialingAsync(dataCredentialingInitiationInfo, GetUserAuthId());
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

            return Json(new { status = status, credentialingInfo = dataCredentialingInitiationInfo }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult InitiateReCredentialing(int id, CredentialingInitiationInfoViewModel credentialingInitiationInfo, int[] CredentialingContractRequestsArray)
        {
            string status = "true";

            credentialingInitiationInfo.InitiationDate = DateTime.Now;
            CredentialingInfo dataCredentialingInitiationInfo = null;
            int id1 = 0;
            dataCredentialingInitiationInfo = AutoMapper.Mapper.Map<CredentialingInitiationInfoViewModel, CredentialingInfo>(credentialingInitiationInfo);
            try
            {
                id1 = individualCredentialingManager.InitiateReCredentialingAsync(id, dataCredentialingInitiationInfo, GetUserAuthId(), CredentialingContractRequestsArray);
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
                status = ExceptionMessage.INITIATE_RECREDENTIALING_EXCEPTION;
            }

            return Json(new { status = status, ID = id1 }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult InitiateDeCredentialing(int[] InfoidArray, int[] ContractidArray, int[] GrididArray, CredentialingLogViewModel obj1)
        {
            string status = "true";

            //LoadedContractViewModel loadedContract = new LoadedContractViewModel { };
            //loadedContract.LoadedContractID = obj1;
            CredentialingLog DataCredentialingLog = null;
            var count = 0;
            int id = 0;
            try
            {
                foreach (int Infoid in InfoidArray)
                {
                    DataCredentialingLog = AutoMapper.Mapper.Map<CredentialingLogViewModel, CredentialingLog>(obj1);
                    id = individualCredentialingManager.InitiateDeCredentialingAsync(count, Infoid, ContractidArray[count], GrididArray[count], GetUserAuthId(), DataCredentialingLog);
                    count++;
                }
                individualCredentialingManager.CompleteDeCredentialingAsync(InfoidArray, GetUserAuthId());
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
                status = ExceptionMessage.INITIATE_DECREDENTIALING_EXCEPTION;
            }

            return Json(new { status = status, ID = id }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> InitiateDrop(int[] InfoidArray)
        {
            string status = "true";
            string UserAuthId = GetUserAuthId();
            int CDUserId = profileManager.GetCredentialingUserId(UserAuthId);
            List<CredentialingInfo> dataCredentialingInitiationInfo = null;
            try
            {
                dataCredentialingInitiationInfo = await individualCredentialingManager.InitiateDrop(CDUserId, InfoidArray);
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

            return Json(new { status = status, credentialingInfo = dataCredentialingInitiationInfo }, JsonRequestBehavior.AllowGet);
        }

        #region CredentialingContractRequest


        public async Task<JsonResult> getCredentialingContractRequest(int ProviderID, int PlanID)
        {
            var status = true;
            IEnumerable<Object> data = null;
            
            try
            {
                data = await individualCredentialingManager.getAllCredentialinginfoByContractRequest(ProviderID, PlanID);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Json(new { status = status, data1 = data }, JsonRequestBehavior.AllowGet);
        }


        public async Task<JsonResult> getCredentialingContractRequestForAllPlan(int ProviderID, int[] PlanIDs)
        {
            var status = true;
            IEnumerable<CredentialingInfo> data = null; ;
            try
            {
                data = await individualCredentialingManager.getCredentialingContractRequestForAllPlan(ProviderID, PlanIDs);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Json(new { status = status, data1 = data }, JsonRequestBehavior.AllowGet);
        }



        #endregion


        #region getPlanListforCredentialingContractRequest


        public async Task<JsonResult> getPlanListforCredentialingContractRequest(int ProviderID)
        {
            var status = true;
            IEnumerable<Plan> data = null; ;
            try
            {
                data = await individualCredentialingManager.getAllPlanListforCredentialinginfoByContractRequest(ProviderID);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Json(new { status = status, data1 = data }, JsonRequestBehavior.AllowGet);
        }



        #endregion




        #region Private Methods

        private string GetUserAuthId()
        {
            var currentUser = HttpContext.User.Identity.Name;
            var appUser = new ApplicationUser() { UserName = currentUser };
            ApplicationUser user = AuthUserManager.FindByNameSync(appUser);
            return user.Id;
        }

        #endregion

        public async Task<string> GetAllCredentialings()
        {
            var data = await individualCredentialingManager.GetAllCredentialingsAsync();
            //return Json(data, JsonRequestBehavior.AllowGet);
            return JsonConvert.SerializeObject(data);
        }

        public async Task<string> GetAllDeCredentialings()
        {
            var data = await individualCredentialingManager.GetAllDeCredentialingAsync();
            return JsonConvert.SerializeObject(data);
            //return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<string> GetAllReCredentialings()
        {
            var data = await individualCredentialingManager.GetAllReCredentialingAsync();
            return JsonConvert.SerializeObject(data);
            //return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<string> GetCredInfo(int id)
        {
            var data = await individualCredentialingManager.GetCredInfoAsync(id);
            return JsonConvert.SerializeObject(data);
        }

        public async Task<ActionResult> DecredentialingSummary(int id)
        {
            var data = await individualCredentialingManager.GetAllDeCredentialingAsync();

            foreach (CredentialingInfo c in data)
            {
                if (c.CredentialingInfoID == id)
                {
                    ViewBag.infoSummary = JsonConvert.SerializeObject(c);
                }
            }
            return View("DecredentialingSummary");
        }


    }
}