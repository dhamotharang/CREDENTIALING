//using AHC.CD.DataMigration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Controllers
{
    public class DataMigrationController : Controller
    {
        // GET: DataMigration
        public ActionResult Index()
        {
            //new Processor().Process();
            
            return View();
        }
    }
}