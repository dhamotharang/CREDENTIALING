using AHC.CD.Business.EmailService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Controllers
{
    public class EmailController : Controller
    {
        private IEmailManager emailManager = null;
        public EmailController(IEmailManager emailManager)
        {
            this.emailManager = emailManager;
        }

        //
        // GET: /Email/
        public async Task<ActionResult> Index()
        {
            ViewBag.SentMails = GetAllSentMails();
            ViewBag.FollowUpMails = GetAllFollowUpMails();
            return View();
        }

        public async Task<string> GetIndividualEmailDetail(int EmailInfoId)
        {
            try
            {
               return JsonConvert.SerializeObject(await emailManager.GetIndividualEmailDetail(EmailInfoId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #region Private Methods
        private async Task<string> GetAllSentMails()
        {
            try
            {
                return JsonConvert.SerializeObject(await emailManager.GetAllSentMails());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<string> GetAllFollowUpMails()
        {
            try
            {
                return JsonConvert.SerializeObject(await emailManager.GetAllFollowUpMails());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}