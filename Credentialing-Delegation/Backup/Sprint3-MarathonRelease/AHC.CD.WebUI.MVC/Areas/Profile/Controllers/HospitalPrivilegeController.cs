using AHC.CD.Business;
using AHC.CD.Business.DocumentWriter;
using AHC.CD.Entities.MasterProfile.HospitalPrivilege;
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

        public HospitalPrivilegeController(IProfileManager profileManager, IErrorLogger errorLogger)
        {
            this.profileManager = profileManager;
            this.errorLogger = errorLogger;
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

        private DocumentDTO CreateDocument(HttpPostedFileBase file)
        {
            DocumentDTO document = null;
            if (file != null)
                document = ConstructDocumentDTO(file.FileName, file.InputStream);
            return document;
        }

        private DocumentDTO ConstructDocumentDTO(string fileName, System.IO.Stream stream)
        {
            return new DocumentDTO() { FileName = fileName, InputStream = stream };
        }

        #endregion
    }
}