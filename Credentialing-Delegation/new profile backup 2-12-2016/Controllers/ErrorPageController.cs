using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Controllers
{
    public class ErrorPageController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.ErrorIcon = "Cross.png";
            ViewBag.ErrorTitle = "Error";
            ViewBag.ErrorDesc = "There was an error. Please Try again Later.";
            return PartialView("~/Views/ErrorPage/Error.cshtml");
        }

        public ActionResult Error404()
        {
            ViewBag.ErrorIcon = "Caution.png";
            ViewBag.ErrorTitle = "404 : Page not found";
            ViewBag.ErrorDesc = "The requested URL/Page was not found on this server.";
            return PartialView("~/Views/ErrorPage/Index.cshtml");
        }

        public ActionResult Error500()
        {
            ViewBag.ErrorIcon = "Cross.png";
            ViewBag.ErrorTitle = "500 : This is an error";
            ViewBag.ErrorDesc = "There was an error. Please try again later.";
            return PartialView("~/Views/ErrorPage/Error.cshtml");
        }

        public ActionResult AccessDenied()
        {
            ViewBag.ErrorIcon = "Lock.png";
            ViewBag.ErrorTitle = "Access Denied";
            ViewBag.ErrorDesc = "You are not authorize to use this URL/Page. Please contact the administrator.";
            return PartialView("~/Views/ErrorPage/Error.cshtml");
        }

        public ActionResult SubscriptionError()
        {
            ViewBag.ErrorIcon = "Lock.png";
            ViewBag.ErrorTitle = "Subscription Error";
            ViewBag.ErrorDesc = "You are not subscribe to this module. Please contact the administrator.";
            return PartialView("~/Views/ErrorPage/Error.cshtml");
        }

        public ActionResult CookieDisabled()
        {
            ViewBag.ErrorIcon = "Cross.png";
            ViewBag.ErrorTitle = "We can't sign you in";
            ViewBag.ErrorDesc = "Your browser is currently set to block cookies. You need to allow cookies to use this service.<br/><br/>Cookies are small text files stored on your computer that tell us when you're signed in. To learn how to allow cookies, check the online help in your web browser.";
            return PartialView("~/Views/ErrorPage/Index.cshtml");
        }

        public ActionResult IncompatibleBrowser()
        {
            return View();
        }

        public ActionResult JavaScriptDisabled()
        {
            return View();
        }
    }
}