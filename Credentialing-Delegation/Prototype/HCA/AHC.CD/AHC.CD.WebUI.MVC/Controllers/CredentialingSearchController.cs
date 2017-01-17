using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Controllers
{
    public class CredentialingSearchController : Controller
    {
        // GET: CredentialingSearch
        public ActionResult Search(string form)
        {
            return PartialView(form);
        }
    }
}