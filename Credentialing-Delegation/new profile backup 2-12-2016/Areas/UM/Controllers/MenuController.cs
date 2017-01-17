using PortalTemplate.Areas.UM.Models.ViewModels.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Areas.UM.Controllers
{
    public class MenuController : Controller
    {
        //
        // GET: /UM/Menu/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetMenu(string SubscriberID){
            MenuViewModel menu = new MenuViewModel();
            menu.SubscriberID = SubscriberID;
            return PartialView("~/Areas/UM/Views/Shared/_UMFloatMenu.cshtml",menu);
        }
	}
}