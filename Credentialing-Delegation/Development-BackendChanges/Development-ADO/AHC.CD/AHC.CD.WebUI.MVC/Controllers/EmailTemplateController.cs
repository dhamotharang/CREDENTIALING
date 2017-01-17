using AHC.CD.Business.MasterData;
using AHC.CD.Entities.EmailNotifications;
using AHC.CD.ErrorLogging;
using AHC.CD.Exceptions;
using AHC.CD.Resources.Messages;
using AHC.CD.WebUI.MVC.Models.EmailTemplate;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Controllers
{
    public class EmailTemplateController : Controller
    {
        IMasterDataAddEdit masterDataAddEditManager = null;
        IMasterDataManager masterDataManager = null;
        private IErrorLogger errorLogger = null;

        public EmailTemplateController(IMasterDataAddEdit masterDataAddEditManager, IErrorLogger errorLogger, IMasterDataManager masterDataManager)
        {
            this.masterDataManager = masterDataManager;
            this.masterDataAddEditManager = masterDataAddEditManager;
            this.errorLogger = errorLogger;
        }

        // GET: EmailTemplate
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateNotification()
        {

            return View();
        }

        public ActionResult EmailTemplate()
        {
            return View();
        }

        public async Task<ActionResult> SaveEmailTemplate(EmailTemplateViewModel emailTemplate)
        {
            string status = "true";
            EmailTemplate dataModelEmailTemplate = null;

            try
            {
                emailTemplate.Body = emailTemplate.Body.Replace("&lt;", "<");
                emailTemplate.Body = emailTemplate.Body.Replace("&gt;", ">");
                dataModelEmailTemplate = AutoMapper.Mapper.Map<EmailTemplateViewModel, EmailTemplate>(emailTemplate,dataModelEmailTemplate);

                dataModelEmailTemplate = await masterDataAddEditManager.SaveEmailTemplateAsync(dataModelEmailTemplate);
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
                status = ExceptionMessage.EMAIL_TEMPLATE_ADD_EXCEPTION;
            }

            return Json(new { status = status, emailTemplate = dataModelEmailTemplate }, JsonRequestBehavior.AllowGet);
        }


        public async Task<string> GetAllSaveEmailTemplate()
        {
            IEnumerable<EmailTemplate> EmailTemplateList = null;
            try
            {
                EmailTemplateList = await masterDataManager.GetAllEmailTemplatesAsync();
            }
            catch (Exception ex)
            {                
                throw ex;
            }            
            return JsonConvert.SerializeObject(EmailTemplateList);
        }
    }
}