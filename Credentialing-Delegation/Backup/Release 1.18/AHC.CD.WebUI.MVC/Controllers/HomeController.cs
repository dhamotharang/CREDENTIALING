using System.Globalization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AHC.CD.WebUI.MVC;
using AHC.CD.WebUI.MVC.Models;
using AHC.CD.WebUI.MVC.CustomHelpers;

namespace IdentitySample.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {

        }
        
        //public HomeController(ApplicationUserManager userManager)
        //{
        //    UserManager = userManager;
        //}
        
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
