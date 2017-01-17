using AHC.CD.Business;
using AHC.CD.Business.DocumentWriter;
using AHC.CD.Entities.MasterProfile.DisclosureQuestions;
using AHC.CD.ErrorLogging;
using AHC.CD.Resources.Messages;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.DisclosureQuestions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class DisclosureQuestionController : Controller
    {
        private IProfileManager profileManager = null;
        private IErrorLogger errorLogger = null;

        public DisclosureQuestionController(IProfileManager profileManager, IErrorLogger errorLogger)
        {
            this.profileManager = profileManager;
            this.errorLogger = errorLogger;
        }

        //[HttpPost]
        //public async Task<ActionResult> UpdatePracticeInterestAsync(int profileId, ProviderDisclosureQuestionAnswerViewModel disclosureQuestion)
        //{
        //    string status = "true";
        //    ProfileDisclosureQuestionAnswer dataModelDisclosureQuestion = null;
        //
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            dataModelDisclosureQuestion = AutoMapper.Mapper.Map<ProviderDisclosureQuestionAnswerViewModel, ProfileDisclosureQuestionAnswer>(disclosureQuestion);
        //            await profileManager.
        //        }
        //        else
        //        {
        //            status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
        //        }
        //    }
        //    catch (DatabaseValidationException ex)
        //    {
        //        errorLogger.LogError(ex);
        //        status = ex.ValidationError;
        //    }
        //    catch (Exception ex)
        //    {
        //        errorLogger.LogError(ex);
        //        status = ExceptionMessage.;
        //    }
        //
        //    return Json(new { status = status, practiceInterest = dataModelDisclosureQuestion }, JsonRequestBehavior.AllowGet);
        //}


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