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

namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class ContractController : Controller
    {

        private IProfileManager profileManager = null;
        private IErrorLogger errorLogger = null;
        private IChangeNotificationManager notificationManager;


        public ContractController(IProfileManager profileManager, IErrorLogger errorLogger, IChangeNotificationManager notificationManager)
        {
            this.profileManager = profileManager;
            this.errorLogger = errorLogger;
            this.notificationManager = notificationManager;

        }

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
        public async Task<ActionResult> UpdateContractInformation(int profileId, AHC.CD.WebUI.MVC.Areas.Profile.Models.ContractInformation.ContractInfoViewModel contractInfo)
        {
            string status = "true";
            ContractInfo dataModelContractInfo = null;

            if(contractInfo.OrganizationId==0){

                contractInfo.OrganizationId = 1;
            }

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelContractInfo = AutoMapper.Mapper.Map<ContractInfoViewModel,ContractInfo>(contractInfo);
                    DocumentDTO document = CreateDocument(contractInfo.ContractFile);
                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Contract Details", "Updated");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    await profileManager.UpdateContractInformationAsync(profileId,dataModelContractInfo, document);
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
        public async Task<ActionResult> UpdateContractGroupInformation(int profileId,int contractInfoId ,ContractGroupInfoViewModel contractGroupInfo)
        {
            string status = "true";
            ContractGroupInfo dataModelContractGroupInfo = null;
            try
            {

                if (ModelState.IsValid)
                {
                    dataModelContractGroupInfo = AutoMapper.Mapper.Map<ContractGroupInfoViewModel,ContractGroupInfo>(contractGroupInfo);
                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Contract Details - Group Information", "Updated");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    await profileManager.UpdateContractGroupInformationAsync(profileId,contractInfoId,dataModelContractGroupInfo);
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
       #endregion

    }
}