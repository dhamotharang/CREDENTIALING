using AHC.CD.Business;
using AHC.CD.Business.DocumentWriter;
using AHC.CD.Business.Notification;
using AHC.CD.Entities.MasterProfile.HospitalPrivilege;
using AHC.CD.Entities.Notification;
using AHC.CD.ErrorLogging;
using AHC.CD.Exceptions;
using AHC.CD.Resources.Messages;
using AHC.CD.WebUI.MVC.Areas.Profile.Attributes;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.HospitalPrivilege;
using AHC.CD.WebUI.MVC.CustomHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class HospitalPrivilegeController : Controller
    {
        private IProfileManager profileManager = null;
        private IErrorLogger errorLogger = null;
        private IChangeNotificationManager notificationManager;

        public HospitalPrivilegeController(IProfileManager profileManager, IErrorLogger errorLogger, IChangeNotificationManager notificationManager)
        {
            this.profileManager = profileManager;
            this.errorLogger = errorLogger;
            this.notificationManager = notificationManager;
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add , false)]
        public async Task<ActionResult> AddHospitalPrivilegeAsync(int profileId, AHC.CD.WebUI.MVC.Areas.Profile.Models.HospitalPrivilege.HospitalPrivilegeDetailViewModel hospitalPrivilege)
        {
            string status = "true";

            string Id = "";

            HospitalPrivilegeDetail dataModelHospitalPrivilegeDetail = null;
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelHospitalPrivilegeDetail = AutoMapper.Mapper.Map<HospitalPrivilegeDetailViewModel, HospitalPrivilegeDetail>(hospitalPrivilege);
                    DocumentDTO hospitalDocument = CreateDocument(hospitalPrivilege.HospitalPrevilegeLetterFile);
                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Hospital Privilege Details", "Added");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    var result = await profileManager.AddHospitalPrivilegeDetailAsync(profileId, dataModelHospitalPrivilegeDetail, hospitalDocument);
                    Id = result.ToString();
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
            return Json(new { status = status,id=Id, hospitalPrivilegeDetail = dataModelHospitalPrivilegeDetail }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> UpdateHospitalPrivilegeAsync(int profileId, AHC.CD.WebUI.MVC.Areas.Profile.Models.HospitalPrivilege.HospitalPrivilegeDetailViewModel hospitalPrivilege)
        {
            string status = "true";

            HospitalPrivilegeDetail dataModelHospitalPrivilegeDetail = null;
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelHospitalPrivilegeDetail = AutoMapper.Mapper.Map<HospitalPrivilegeDetailViewModel, HospitalPrivilegeDetail>(hospitalPrivilege);
                    DocumentDTO hospitalDocument = CreateDocument(hospitalPrivilege.HospitalPrevilegeLetterFile);
                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Hospital Privilege Details", "Updated");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    await profileManager.UpdateHospitalPrivilegeDetailAsync(profileId, dataModelHospitalPrivilegeDetail, hospitalDocument);
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
            return Json(new { status = status, hospitalPrivilegeDetail = dataModelHospitalPrivilegeDetail }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> RenewHospitalPrivilegeAsync(int profileId, AHC.CD.WebUI.MVC.Areas.Profile.Models.HospitalPrivilege.HospitalPrivilegeDetailViewModel hospitalPrivilege)
        {
            string status = "true";
            HospitalPrivilegeDetail dataModelHospitalPrivilegeDetail = null;

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelHospitalPrivilegeDetail = AutoMapper.Mapper.Map<HospitalPrivilegeDetailViewModel, HospitalPrivilegeDetail>(hospitalPrivilege);
                    DocumentDTO document = CreateDocument(hospitalPrivilege.HospitalPrevilegeLetterFile);
                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Hospital Privilege Details", "Renewed");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    //DocumentDTO document = null;
                    await profileManager.RenewHospitalPrivilegeDetailAsync(profileId, dataModelHospitalPrivilegeDetail, document);
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

            return Json(new { status = status, hospitalPrivilege = dataModelHospitalPrivilegeDetail }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit , false)]
        public async Task<ActionResult> UpdateHospitalPrivilegeInfoAsync(int profileId, AHC.CD.WebUI.MVC.Areas.Profile.Models.HospitalPrivilege.HospitalPrivilegeInformationViewModel hospitalPrivilegeInfo)
        {
            string status = "true";

            HospitalPrivilegeInformation dataModelHospitalPrivilegeInformation = null;
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelHospitalPrivilegeInformation = AutoMapper.Mapper.Map<HospitalPrivilegeInformationViewModel, HospitalPrivilegeInformation>(hospitalPrivilegeInfo);
                    await profileManager.UpdateHospitalPrivilegeInformationAsync(profileId, dataModelHospitalPrivilegeInformation);
                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Hospital Privilege Details", "Updated");
                    await notificationManager.SaveNotificationDetailAsync(notification);


                    if(dataModelHospitalPrivilegeInformation.HospitalPrivilegeDetails==null||dataModelHospitalPrivilegeInformation.HospitalPrivilegeDetails.Count==0)
                    {
                        dataModelHospitalPrivilegeInformation.HospitalPrivilegeDetails = new List<HospitalPrivilegeDetail>();
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
            return Json(new { status = status, hospitalPrivilegeInformation = dataModelHospitalPrivilegeInformation }, JsonRequestBehavior.AllowGet);
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

        #endregion
    }
}