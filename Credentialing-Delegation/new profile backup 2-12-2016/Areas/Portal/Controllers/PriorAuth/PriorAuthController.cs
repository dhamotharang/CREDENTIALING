using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PortalTemplate.Areas.Portal.Services.PriorAuth.MasterData;
using PortalTemplate.Helper;

namespace PortalTemplate.Areas.Portal.Controllers.PriorAuth
{
    public class PriorAuthController : Controller
    {
        PriorAuthMasterDataServices service = new PriorAuthMasterDataServices();
        public ActionResult Index()
        {
            SetAllMasterData();
            return View("~/Areas/Portal/Views/PriorAuth/PriorAuthForm/Create/_CreatePortalRequest.cshtml");
        }

        private void SetAllMasterData()
        {
            var MasterData = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/Areas/Portal/Views/web.config").AppSettings.Settings["Portal"].Value.Split(',');
            foreach (string md in MasterData)
            {
                var method = md.Split(':')[0];
                var variable = md.Split(':')[1];
                ViewData[variable] = typeof(PriorAuthMasterDataServices).GetMethod(method).Invoke(service, new object[0]);
            }
        }
        public string ChangeCPTSection(string type)
        {
            string url = CombinedCPTURL(type);
            string Template = CustomHelper.RenderPartialToString(this.ControllerContext, url, null);
            return Template;
        }
        public string CombinedCPTURL(string type)
        {
            return "~/Areas/Portal/Views/PriorAuth/PriorAuthForm/Create/CPTSubTypes/_CPTSection" + type + ".cshtml";
        }
	}
}