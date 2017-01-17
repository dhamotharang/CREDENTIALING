using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Areas.UM.Controllers
{
    public class ContactManagementController : Controller
    {
        //
        // GET: /UM/ContactManagement/
        public ActionResult Index()
        {
            return PartialView("~/Areas/UM/Views/ContactManagement/_ContactManagement.cshtml");
        }
	}
}