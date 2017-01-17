using AHC.CD.Business.Profiles;
using AHC.CD.Entities.MasterProfile.ProfileUpdateRenewal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AHC.CD.WebUI.MVC.Models;
using System.Threading.Tasks;
using AHC.CD.Business.BusinessModels.ProfileUpdates;
using AHC.CD.Entities;
using AHC.CD.Data.Repository;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using AHC.CD.Entities.MasterData.Tables;
using Newtonsoft.Json;
using AHC.CD.Entities.Credentialing.CredentialingRequestTracker;
using AHC.CD.WebUI.MVC.Areas.Credentialing.Models.CredentialingRequestTracker;
using AHC.CD.Exceptions;
using AHC.CD.Resources.Messages;
using AHC.CD.ErrorLogging;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Controllers
{
    public class ProfileUpdatesController : Controller
    {
        private IProfileUpdateManager profileUpdateManager = null;
        private IErrorLogger errorLogger = null;
        IUnitOfWork uow = null;

        public ProfileUpdatesController(IUnitOfWork uow, IProfileUpdateManager profileUpdateManager, IErrorLogger errorLogger)
        {
            this.profileUpdateManager = profileUpdateManager;
            this.errorLogger = errorLogger;
            this.uow = uow;
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

        // GET: CredentialingDelegation/ProfileUpdates
        [Authorize(Roles = "CCO,PRO")]
        public ActionResult Index()
        {
            ViewBag.Roles = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().GetRoles(User.Identity.GetUserId());
            return View();
        }

        [HttpGet]
        public async Task<string> GetAllUpdates()
        {
            List<ProfileUpdatesTracker> upadtedData = null;

            bool isPRO = await GetUserRole();           

            if (isPRO)
            {
                string UserAuthId = await GetUserAuthId();
                int ProfileID = Convert.ToInt32(GetCredentialingUserId(UserAuthId));

                upadtedData = profileUpdateManager.GetUpdatesById(ProfileID);

            }
            else
            {

                upadtedData = profileUpdateManager.GetAllUpdates();

            }

            //return Json(upadtedData, JsonRequestBehavior.AllowGet);
            return JsonConvert.SerializeObject(upadtedData);
        }

        [HttpGet]
        public async Task<string> GetAllUpdatesHistory()
        {
            List<ProfileUpdatesTracker> upadtedData = null;
            bool isPRO = await GetUserRole();            

            if (isPRO)
            {
                string UserAuthId = await GetUserAuthId();
                int ProfileID = Convert.ToInt32(GetCredentialingUserId(UserAuthId));

                upadtedData = profileUpdateManager.GetUpdatesHistoryById(ProfileID);
            }
            else
            {
                upadtedData = profileUpdateManager.GetAllUpdatesHistory();

            }

            return JsonConvert.SerializeObject(upadtedData);
        }

        [HttpPost]
        public ActionResult GetDataById(int trackerId, string status, string modificationType)
        {
            List<ProfileUpdatesTracker> profileUpdates = new List<ProfileUpdatesTracker>();
            List<ProfileUpdatedData> uniqueUdatedData = new List<ProfileUpdatedData>();
            List<ProfileUpdatedData> updatedDatas = new List<ProfileUpdatedData>();
            ProfileUpdatedData currentUpdatedData = new ProfileUpdatedData();

            if (status.Equals("Approved") || status.Equals("Rejected"))
            {
                uniqueUdatedData = profileUpdateManager.GetDataById(trackerId);
            }
            else
            {
                profileUpdates = profileUpdateManager.GetUpdatesByTrackerId(trackerId, status, modificationType);
                foreach (var item in profileUpdates)
                {
                    List<ProfileUpdatedData> eachUpadtedDatas = new List<ProfileUpdatedData>();
                    eachUpadtedDatas = profileUpdateManager.GetDataById(item.ProfileUpdatesTrackerId);
                    foreach (var innerItem in eachUpadtedDatas)
                    {
                        updatedDatas.Add(innerItem);
                    }
                }

                //get lst field value
                if (updatedDatas.Count != 0)
                {
                    uniqueUdatedData.Add(updatedDatas[0]);
                    for (int i = 1; i < updatedDatas.Count; i++)
                    {
                        var flag = 0;
                        currentUpdatedData = updatedDatas[i];
                        for (int j = 0; j < uniqueUdatedData.Count; j++)
                        {
                            if (currentUpdatedData.FieldName.Equals(uniqueUdatedData[j].FieldName))
                            {
                                flag = 1;
                                break;
                            }
                        }
                        if (flag == 0)
                        {
                            uniqueUdatedData.Add(updatedDatas[i]);
                        }
                    }

                    for (int i = 0; i < uniqueUdatedData.Count; i++)
                    {
                        for (int j = 0; j < updatedDatas.Count; j++)
                        {
                            if (updatedDatas[j].FieldName.Equals(uniqueUdatedData[i].FieldName))
                            {
                                uniqueUdatedData[i].OldValue = updatedDatas[j].OldValue;
                                uniqueUdatedData[i].NewValue = updatedDatas[j].NewValue;

                            }
                        }
                    }
                }
            }




            return Json(uniqueUdatedData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> SetApproval(ApprovalSubmission tracker, string modification, string approvedStatus)
        {
            var status = "true";
            var currentUser = HttpContext.User.Identity.Name;
            var appUser = new ApplicationUser() { UserName = currentUser };
            var user = await AuthUserManager.FindByNameAsync(appUser.UserName);

            List<ProfileUpdatesTracker> profileUpdates = new List<ProfileUpdatesTracker>();
            profileUpdates = profileUpdateManager.GetUpdatesByTrackerId(tracker.TrackerId, approvedStatus, modification);

            //List<ProfileUpdatesTracker> onHoldProfileUpdates = new List<ProfileUpdatesTracker>();
            //onHoldProfileUpdates = profileUpdateManager.GetUpdatesByTrackerId(tracker.TrackerId, "OnHold", modification);

            //foreach (var item in onHoldProfileUpdates)
            //{
            //    profileUpdates.Add(item);
            //}

            List<ApprovalSubmission> trackers = new List<ApprovalSubmission>();
            foreach (var item in profileUpdates)
            {
                ApprovalSubmission innerTracker = new ApprovalSubmission();
                innerTracker.TrackerId = item.ProfileUpdatesTrackerId;
                innerTracker.ApprovalStatus = tracker.ApprovalStatus;
                innerTracker.RejectionReason = tracker.RejectionReason;
                trackers.Add(innerTracker);
            }

            await profileUpdateManager.SetApproval(trackers, user.Id);


            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public int? GetCredentialingUserId(string UserAuthId)
        {
            var userRepo = uow.GetGenericRepository<CDUser>();
            var user = userRepo.Find(u => u.AuthenicateUserId == UserAuthId);

            return user.ProfileId;
        }

        public string getHospitalContactInfoById(int[] contactInfoIds)
        {
            var hospitalContactInfos = profileUpdateManager.GetHospitalContactInfoByIds(contactInfoIds);

            return JsonConvert.SerializeObject(hospitalContactInfos);
        }

        [HttpPost]
        public ActionResult CredentialingRequestTrackerSetApproval(CredentialingRequestTrackerViewModel tracker)  
        {
            string status = "true";
            CredentialingRequestTracker dataCredentialingRequestTracker = null; 
            try
            {
                dataCredentialingRequestTracker = AutoMapper.Mapper.Map<CredentialingRequestTrackerViewModel, CredentialingRequestTracker>(tracker);
                profileUpdateManager.CredentialingRequestTrackerSetApprovalAsync(dataCredentialingRequestTracker);

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
                status = ExceptionMessage.CREDENTIALING_REQUEST_TRACKER_EXCEPTION;  
            }

            return Json(new { status = status }, JsonRequestBehavior.AllowGet);
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