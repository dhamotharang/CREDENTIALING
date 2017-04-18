using AHC.CD.Business;
using AHC.CD.Business.Search;
using AHC.CD.Entities.UserInfo;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.SearchProvider;
using AHC.CD.WebUI.MVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using AHC.CD.Exceptions;
using AHC.CD.ErrorLogging;
using AHC.CD.Resources.Messages;

namespace AHC.CD.WebUI.MVC.Controllers
{
    public class AssignToCCOorTLController : Controller
    {
        private ISearchManager _searchManager = null;
        private IAssignToCCOorTLManager _AssignToCCOorTLManager = null;
        private IErrorLogger ErrorLogger { get; set; }

        public AssignToCCOorTLController(ISearchManager searchManager, IAssignToCCOorTLManager AssigneToCCOorTLManager, IErrorLogger errorLogger)
        {
            this._searchManager = searchManager;
            this._AssignToCCOorTLManager = AssigneToCCOorTLManager;
            this.ErrorLogger = errorLogger;
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

        //
        // GET: /AssignToCCOorTL/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CCOAssignment()
        {
            return View("~/Views/AdminConfig/CCOAssignment/CCOAssignment.cshtml");
        }

        /// <summary>
        /// Author : Manideep Innamuri
        /// Description : Method to Get the Search Result of a Profile
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SearchProfile(SearchProviderViewModel search)
        {
            //string status = "true";
            List<AHC.CD.Entities.MasterProfile.Profile> searchResults = null;

            searchResults = _searchManager.SearchProfileForViewEdit(search.NPINumber, search.FirstName, search.LastName, search.ProviderRelationship, search.IPAGroupName, search.ProviderLevel, search.ProviderType);

            var result = searchResults.Select(x =>
                new
                {
                    ProfileID = x.ProfileID,
                    NPINumber = x.OtherIdentificationNumber.NPINumber,
                    FullTitles = x.PersonalDetail.ProviderTitles,
                    FirstName = x.PersonalDetail.FirstName,
                    LastName = x.PersonalDetail.LastName,
                    FullRelations = x.ContractInfoes.Count() != 0 ? (x.ContractInfoes.FirstOrDefault().ProviderRelationship) : null,
                    Status = x.Status,
                    StatusType = x.StatusType,
                    CCO = _AssignToCCOorTLManager.GetAssignedCCOForaProvider(x.ProfileID),
                    TL = _AssignToCCOorTLManager.GetAssignedTLForaProvider(x.ProfileID)
                });
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Author : Manideep Innamuri
        /// Description : Method to Get All the CCo's
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> GetAllCCOData()
        {
            List<ProfileUser> Users = new List<ProfileUser>();
            try
            {
                Users = (await _AssignToCCOorTLManager.GetAllCCOs()).ToList();
                var res = Users.Select(x =>
                          new
                          {
                              ProfileUserID = x.ProfileUserID,
                              Name = x.Name,
                              NoofProvidersAssigned = _AssignToCCOorTLManager.NumberOfProvidersAssigned(x.ProfileUserID),
                              TaskAssigned = _AssignToCCOorTLManager.GetNoofTasksAssigned(x.CDUserID),
                              Pending = _AssignToCCOorTLManager.GetNoofTasksPending(x.CDUserID)
                          }).ToList();

                return JsonConvert.SerializeObject(res, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Author : Manideep Innamuri
        /// Description : Method to Get All the TL's
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> GetAllTLData()
        {
            List<ProfileUser> Users = new List<ProfileUser>();
            try
            {
                Users = (await _AssignToCCOorTLManager.GetAllTLs()).ToList();
                var res = Users.Select(x =>
                          new
                          {
                              ProfileUserId = x.ProfileUserID,
                              Name = x.Name,
                              Gender = x.Gender,
                              Email = x.Email,
                              Phone = x.MobileNumber,
                              NoofProvidersAssigned = _AssignToCCOorTLManager.NumberOfProvidersAssigned(x.ProfileUserID),
                              TaskAssigned = _AssignToCCOorTLManager.GetNoofTasksAssigned(x.ProfileUserID),
                              Pending = _AssignToCCOorTLManager.GetNoofTasksPending(x.ProfileUserID)
                          });

                return JsonConvert.SerializeObject(res, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Author : Manideep Innamuri
        /// Description : Method to Assign Providers to CCo
        /// </summary>
        /// <param name="ProfileIDs"></param>
        /// <param name="profileUserId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> AssignProviderstoCCO(List<int?> ProfileIDs, int profileUserId, string Status)
        {
            string status = "true";
            try
            {
                var currentUser = HttpContext.User.Identity.Name;
                var appUser = new ApplicationUser() { UserName = currentUser };
                var user = await AuthUserManager.FindByNameAsync(appUser.UserName);
                string userId = user.Id;
                await _AssignToCCOorTLManager.AssignMultipleProfilesToCCo(ProfileIDs, profileUserId, userId, Status);
            }
            #region CatchBlock
            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.TL_ASSIGN_EXCEPTION;
            }
            #endregion
            return Json(new { status = status }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Author : Manideep Innamuri
        /// Description : Method to Assign Providers to TL
        /// </summary>
        /// <param name="ProfileIDs"></param>
        /// <param name="profileUserId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> AssignProviderstoTL(List<int?> ProfileIDs, int profileUserId, string Status)
        {
            string status = "true";
            try
            {
                var currentUser = HttpContext.User.Identity.Name;
                var appUser = new ApplicationUser() { UserName = currentUser };
                var user = await AuthUserManager.FindByNameAsync(appUser.UserName);
                string userId = user.Id;
                await _AssignToCCOorTLManager.AssignMultipleProfilesToTL(ProfileIDs, profileUserId, userId, Status);
            }
            #region CatchBlock
            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.TL_ASSIGN_EXCEPTION;
            }
            #endregion
            return Json(new { status = status }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Author : Manideep Innamuri
        /// Description : Method 
        /// </summary>
        /// <param name="ProfileIDs"></param>
        /// <returns></returns>
        public int GetCountOfAlreadyAssignedProviders(List<int?> ProfileIDs, string CCorTL)
        {
            int count = 0;
            try
            {
                count = _AssignToCCOorTLManager.GetCountOfAlreadyAssignedProviders(ProfileIDs, CCorTL);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return count;
        }
    }
}