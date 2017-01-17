using PortalTemplate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PortalTemplate.Controllers
{
    public class DashBoardsController : Controller
    {
        //
        // GET: /DashBoards/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ClaimsDashboard()
        {
            return View();
        }
        public ActionResult IntakeDashboard() 
        {
            return View();
        }
        public ActionResult IntakeLeadDashboard()
        {
            return View();
        }
        public ActionResult UmDashboard()
        {
            return View();
        }

        //This Method Gets the JSON Data from JSON Files for the Authorization Summary Charts and Tables
        //Receives Arguments for Pending/Submitted/Completed Authorizations and returns required
        public object GetGraphData(string input)
        {
            try
            {
                String path;
                if (input == "SubmittedData")
                {
                     path = HttpContext.Server.MapPath("~/Resources/JSONData/ChartsDataSubmitted.json");
                }
                else if (input == "CompletedData")
                {
                     path = HttpContext.Server.MapPath("~/Resources/JSONData/ChartsDataCompleted.json");
                }
                else
                {
                     path = HttpContext.Server.MapPath("~/Resources/JSONData/ChartsDataPending.json");
                }
               
                using (TextReader reader = System.IO.File.OpenText(path))
                {
                    string text = reader.ReadToEnd();
                    var data = JsonConvert.DeserializeObject(text);
                    return data;
                }
            }
            catch(Exception ex){
                throw ex;
            }

        }

	}
}