using AHC.CD.Business;
using AHC.CD.ErrorLogging;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ContractInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AHC.CD.Entities.MasterProfile.Contract;
using AHC.CD.Exceptions;
using AHC.CD.Resources.Messages;
using AHC.CD.Business.DocumentWriter;
using AHC.CD.Business.Notification;
using AHC.CD.Entities.Notification;
using AHC.CD.WebUI.MVC.Areas.Profile.Attributes;
using AHC.CD.WebUI.MVC.CustomHelpers;
using AHC.CD.Business.Profiles;
using Microsoft.AspNet.Identity.Owin;
using AHC.CD.WebUI.MVC.Models;
using AHC.CD.Business.BusinessModels.ProfileUpdates;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class ContractController : Controller
    {

        private IProfileManager profileManager = null;
        private IErrorLogger errorLogger = null;
        private IChangeNotificationManager notificationManager;
        private IProfileUpdateManager profileUpdateManager = null;

        public ContractController(IProfileManager profileManager, IErrorLogger errorLogger, IChangeNotificationManager notificationManager, IProfileUpdateManager profileUpdateManager)
        {
            this.profileManager = profileManager;
            this.errorLogger = errorLogger;
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

        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> AddContractInformation(int profileId,ContractInfoViewModel contractInfo)
        {
            string status = "true";
            ContractInfo dataModelContractInfo = null;


            if (contractInfo.OrganizationId == 0)
            {

                contractInfo.OrganizationId = 1;
            }


            try {

                if (ModelState.IsValid)
                {
                    dataModelContractInfo = AutoMapper.Mapper.Map<ContractInfoViewModel, ContractInfo>(contractInfo);
                    DocumentDTO document = CreateDocument(contractInfo.ContractFile);
                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Contract Details", "Added");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    await profileManager.AddContractInformationAsync(profileId, dataModelContractInfo, document);
                
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

            return Json(new { status = status, contractInformation = dataModelContractInfo }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> UpdateContractInformation(int profileId, AHC.CD.WebUI.MVC.Areas.Profile.Models.ContractInformation.ContractInfoViewModel contractInfo)
        {
            string status = "true";
            ContractInfo dataModelContractInfo = null;
            bool isCCO = await GetUserRole();

            if(contractInfo.OrganizationId==0){

                contractInfo.OrganizationId = 1;
            }

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelContractInfo = AutoMapper.Mapper.Map<ContractInfoViewModel, ContractInfo>(contractInfo);

                    //DocumentDTO document = CreateDocument(contractInfo.ContractFile);
                    //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Contract Details", "Updated");
                    //await notificationManager.SaveNotificationDetailAsync(notification);

                    //await profileManager.UpdateContractInformationAsync(profileId, dataModelContractInfo, document);

                    if (isCCO)
                    {
                        DocumentDTO document = CreateDocument(contractInfo.ContractFile);
                        ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Contract Details", "Updated");
                        await notificationManager.SaveNotificationDetailAsync(notification);

                        await profileManager.UpdateContractInformationAsync(profileId, dataModelContractInfo, document);
                    }
                    else
                    {
                        string userId = await GetUserAuthId();
                        ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                        tracker.ProfileId = profileId;
                        tracker.Section = "Contract Information";
                        tracker.SubSection = "Contract Info";
                        tracker.userAuthId = userId;
                        tracker.objId = contractInfo.ContractInfoID;
                        tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                        tracker.url = "/Profile/Contract/UpdateContractInformation?profileId=";

                        profileUpdateManager.AddProfileUpdateForProvider(contractInfo, dataModelContractInfo, tracker);
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

            return Json(new { status = status, contractInformation = dataModelContractInfo }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> UpdateContractGroupInformation(int contractInfoId, int profileId,ContractGroupInfoViewModel contractGroupInfo)
        {
            string status = "true";
            ContractGroupInfo dataModelContractGroupInfo = null;
            bool isCCO = await GetUserRole();
            try
            {

                if (ModelState.IsValid)
                {
                    dataModelContractGroupInfo = AutoMapper.Mapper.Map<ContractGroupInfoViewModel,ContractGroupInfo>(contractGroupInfo);

                    //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Contract Details - Group Information", "Updated");
                    //await notificationManager.SaveNotificationDetailAsync(notification);

                    //await profileManager.UpdateContractGroupInformationAsync(profileId, contractInfoId, dataModelContractGroupInfo);

                    if (isCCO)
                    {
                        ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Contract Details - Group Information", "Updated");
                        await notificationManager.SaveNotificationDetailAsync(notification);

                        await profileManager.UpdateContractGroupInformationAsync(profileId, contractInfoId, dataModelContractGroupInfo);
                    }
                    else
                    {
                        string userId = await GetUserAuthId();
                        ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                        tracker.ProfileId = profileId;
                        tracker.Section = "Contract Information";
                        tracker.SubSection = "Contract Group Info";
                        tracker.userAuthId = userId;
                        tracker.objId = contractGroupInfo.ContractGroupInfoId;
                        tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                        tracker.url = "/Profile/Contract/UpdateContractGroupInformation?contractInfoId=" + contractInfoId + "&profileId=";

                        profileUpdateManager.AddProfileUpdateForProvider(contractGroupInfo, dataModelContractGroupInfo, tracker);
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

            return Json(new { status = status, contractGroupInformation = dataModelContractGroupInfo }, JsonRequestBehavior.AllowGet);
        }

        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> AddContractGroupInformation(int profileId, int contractInfoId, ContractGroupInfoViewModel contractGroupInfo)
        {

            string status = "true";
            ContractGroupInfo dataModelContractGroupInfo = null;

              try
              {

                if (ModelState.IsValid)
                {
                    dataModelContractGroupInfo = AutoMapper.Mapper.Map<ContractGroupInfoViewModel,ContractGroupInfo>(contractGroupInfo);
                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Contract Details - Group Information", "Added");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    await profileManager.AddContractGroupInformationAsync(profileId,contractInfoId,dataModelContractGroupInfo);
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

              return Json(new { status = status, contractGroupInformation = dataModelContractGroupInfo }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> RemoveContractGroupInformationAsync(int profileId, int contractInfoId, ContractGroupInfoViewModel contractGroupInfo)
        {
            string status = "true";
            ContractGroupInfo dataModelContractGroupInfo = null;

            try
            {
                dataModelContractGroupInfo = AutoMapper.Mapper.Map<ContractGroupInfoViewModel, ContractGroupInfo>(contractGroupInfo);
                //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Contract Group Information", "Removed");
                //await notificationManager.SaveNotificationDetailAsync(notification);

                await profileManager.RemoveContractGroupInformationAsync(profileId, contractInfoId, dataModelContractGroupInfo);
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
                status = ExceptionMessage.CONTRACT_GROUP_INFORMATION_REMOVE_EXCEPTION;
            }

            return Json(new { status = status, groupInfo = dataModelContractGroupInfo }, JsonRequestBehavior.AllowGet);
        }

        #region private methods
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