using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Areas.UM.Controllers
{
    public class NoteManagementController : Controller
    {
        //
        // GET: /UM/NoteManagement/
        public ActionResult Index()
        {
            return PartialView("~/Areas/UM/Views/NoteManagement/_NoteManagement.cshtml");
        }
	}
}