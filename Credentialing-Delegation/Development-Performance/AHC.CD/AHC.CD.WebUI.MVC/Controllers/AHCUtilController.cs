using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Controllers
{
    public class AHCUtilController : Controller
    {
        //
        // GET: /AHCUtil/
        //------------- method for get partial page on user Required -----------------
        public PartialViewResult GetPartial(string partialUrl)
        {
            if (String.IsNullOrEmpty(partialUrl))
                return null;

            return PartialView(partialUrl);
        }
	}
}