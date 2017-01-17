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

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Controllers
{
    public class InitiationController : Controller
    {
        //
        private ISearchManager searchManager = null;
        private IIndividualCredentialingManager individualCredentialingManager = null;
        private IErrorLogger errorLogger = null;

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

        public InitiationController(ISearchManager searchManager, IErrorLogger errorLogger, IIndividualCredentialingManager individualCredentialingManager)
        {
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
        public JsonResult SearchProviderJson(int id, SearchProviderForCred search)
        {
            //ProviderSearchRequestDTO searchRequest = null;
            //string status = "true";
            List<SearchResultForCred> searchResults2 = null;
            List<CredentialingInfo> searchResults1 = null;
            switch (id)
            { 
                case 1:
                    searchResults2 = searchManager.SearchProviderProfileForCred(search.NPINumber, search.FirstName, search.LastName, search.Specialty, search.IPAGroupName, search.CAQH, search.ProviderType);
                    return Json(new { searchResults = searchResults2 }, JsonRequestBehavior.AllowGet);
                    
                case 2:
                    searchResults1 = searchManager.SearchProviderProfileForReCred(search.NPINumber, search.FirstName, search.LastName, search.Specialty, search.IPAGroupName, search.CAQH, search.ProviderType);
                    return Json(new { searchResults = searchResults1 }, JsonRequestBehavior.AllowGet);
                   
                case 3:
                     searchResults1 = searchManager.SearchProviderProfileForReCred(search.NPINumber, search.FirstName, search.LastName, search.Specialty, search.IPAGroupName, search.CAQH, search.ProviderType);
                    return Json(new { searchResults = searchResults1 }, JsonRequestBehavior.AllowGet);
                   
                default: break;

            }
           
            return Json(new { searchResults = "" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> InitiateCredentialing(CredentialingInitiationInfoViewModel credentialingInitiationInfo)
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
                    await individualCredentialingManager.InitiateCredentialingAsync(dataCredentialingInitiationInfo, await GetUserAuthId());
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
        public async Task<JsonResult> InitiateReCredentialing(int id, CredentialingLogViewModel obj1)
        {
            string status = "true";

            //LoadedContractViewModel loadedContract = new LoadedContractViewModel { };
            //loadedContract.LoadedContractID = obj1;
            CredentialingLog DataCredentialingLog = null;         

            try
            {
                DataCredentialingLog = AutoMapper.Mapper.Map<CredentialingLogViewModel, CredentialingLog>(obj1);
                await individualCredentialingManager.InitiateReCredentialingAsync(id,await GetUserAuthId(), DataCredentialingLog);
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

            return Json(new { status = status, credentialingInfo = "" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> InitiateDeCredentialing(int id, CredentialingLogViewModel obj1)
        {
            string status = "true";

            //LoadedContractViewModel loadedContract = new LoadedContractViewModel { };
            //loadedContract.LoadedContractID = obj1;
            CredentialingLog DataCredentialingLog = null;

            try
            {
                DataCredentialingLog = AutoMapper.Mapper.Map<CredentialingLogViewModel, CredentialingLog>(obj1);
                await individualCredentialingManager.InitiateDeCredentialingAsync(id, await GetUserAuthId(), DataCredentialingLog);
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

            return Json(new { status = status, credentialingInfo = "" }, JsonRequestBehavior.AllowGet);
        }


        #region Private Methods

        private async Task<string> GetUserAuthId()
        {
            var currentUser = HttpContext.User.Identity.Name;
            var appUser = new ApplicationUser() { UserName = currentUser };
            var user = await AuthUserManager.FindByNameAsync(appUser.UserName);

            return user.Id;
        }

        #endregion
        
        public async Task<string> GetAllCredentialings()
        {
            var data = await individualCredentialingManager.GetAllCredentialingsAsync();
            //return Json(data, JsonRequestBehavior.AllowGet);
            return JsonConvert.SerializeObject(data);
        }

        public async Task<JsonResult> GetAllDeCredentialings()
        {
            var data = await individualCredentialingManager.GetAllDeCredentialingAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAllReCredentialings()
        {
            var data = await individualCredentialingManager.GetAllReCredentialingAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

    }
}