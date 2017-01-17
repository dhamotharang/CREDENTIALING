using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Controllers
{
    public class PACLeadDashboardController : Controller
    {
        //
        // GET: /PACLeadDashboard/
        public ActionResult Index()
        {
            return View();
        }

        //This Method Gets the JSON Data from JSON Files for the Authorization Summary Charts and Tables
        //Receives Arguments for Pending/Submitted/Completed Authorizations and returns required
        public object GetData(string input)
        {
            try
            {
                String path;
                if (input == "REF1")
                {
                    path = HttpContext.Server.MapPath("~/Resources/Data/PACDashboard/referrals1.json");
                }
                else if (input == "OMT2")
                {
                    path = HttpContext.Server.MapPath("~/Resources/Data/PACDashboard/referrals1.json");
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
            catch (Exception ex)
            {
                throw ex;
            }

        }
	}
}