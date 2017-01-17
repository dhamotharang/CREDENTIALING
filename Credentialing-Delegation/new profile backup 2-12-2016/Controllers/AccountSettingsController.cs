using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Controllers
{
    public class AccountSettingsController : Controller
    {
        
        public ActionResult GetChangePassword()
        {
            return PartialView("~/Views/AccountSettings/_ProfileSettings.cshtml");
        }

	}
}