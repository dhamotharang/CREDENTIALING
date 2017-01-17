using PortalTemplate.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Areas.Portal.Controllers.PriorAuth
{
    public class PriorAuthAddRowController : Controller
    {
        //
        // GET: /Portal/PriorAuthAddRow/
        public string AddNewRow(int index,string url)
        {
            ViewBag.index = index;
            string Template = CustomHelper.RenderPartialToString(this.ControllerContext, url, null);

            return Template;
        }
	}
}