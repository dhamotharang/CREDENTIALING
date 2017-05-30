using System.Web.Mvc;

namespace UIThemes_Layout.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LayoutFirst()
        {
            return View();
        }

        public ActionResult LayoutSecond()
        {
            return View();
        }

        public ActionResult LayoutThird()
        {
            return View();
        }

        public ActionResult FourthLayout()
        {
            return View();
        }

        public ActionResult FifthLayout()
        {

            return View();
        }


        public ActionResult GetHeader()
        {
            return PartialView("~/Views/LayoutFive/header.cshtml");
        }

        public ActionResult GetMenu()
        {
            return PartialView("~/Views/LayoutFive/MenuItems.cshtml");
        }


    }
}