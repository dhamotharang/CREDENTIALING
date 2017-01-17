using PortalTemplate.Areas.Portal.Models.PriorAuth.View;
using PortalTemplate.Areas.Portal.Services.PriorAuth.ViewPriorAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Areas.Portal.Controllers.PriorAuth
{
    public class PriorAuthViewController : Controller
    {
        readonly ViewPriorAuthService service = new ViewPriorAuthService();
        public ActionResult GetViewAuth(int ID)
        {
            ViewPriorAuthorizationViewModel ViewAuthModel = new ViewPriorAuthorizationViewModel();
            ViewAuthModel = service.GetAuthByID(ID);
            return View("~/Areas/Portal/Views/PriorAuth/ViewPriorAuth/_ViewPriorAuth.cshtml", ViewAuthModel);
        }
    }
}