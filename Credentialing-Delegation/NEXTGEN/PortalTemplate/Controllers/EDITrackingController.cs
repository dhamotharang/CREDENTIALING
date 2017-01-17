using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Controllers
{
    public class EDITrackingController : Controller
    {
        //
        // GET: /EDITracking/
        public ActionResult Index()
        {
            return View();
        }
        //This Method Gets the JSON Data from JSON Files for the Authorization Summary Charts and Tables
        //Receives Arguments for Pending/Submitted/Completed Authorizations and returns required
        public object GetTrackingData(int input)
        {
            try
            {
                String path;
                switch (input)
                {
                    case 0:
                        path = HttpContext.Server.MapPath("~/Resources/EDITrackingJSONData/StatusPanel0.json");
                        break;
                    case 1:
                        path = HttpContext.Server.MapPath("~/Resources/EDITrackingJSONData/StatusPanel1.json");
                        break;
                    case 2:
                        path = HttpContext.Server.MapPath("~/Resources/EDITrackingJSONData/StatusPanel2.json");
                        break;
                    case 3:
                        path = HttpContext.Server.MapPath("~/Resources/EDITrackingJSONData/StatusPanel3.json");
                        break;
                    case 4:
                        path = HttpContext.Server.MapPath("~/Resources/EDITrackingJSONData/StatusPanel4.json");
                        break;
                    case 5:
                        path = HttpContext.Server.MapPath("~/Resources/EDITrackingJSONData/StatusPanel5.json");
                        break;
                    case 6:
                        path = HttpContext.Server.MapPath("~/Resources/EDITrackingJSONData/StatusPanel6.json");
                        break;
                    case 7:
                        path = HttpContext.Server.MapPath("~/Resources/EDITrackingJSONData/StatusPanel7.json");
                        break;
                    case 8:
                        path = HttpContext.Server.MapPath("~/Resources/EDITrackingJSONData/StatusPanel8.json");
                        break;
                    case 9:
                        path = HttpContext.Server.MapPath("~/Resources/EDITrackingJSONData/StatusPanel9.json");
                        break;
                    case 10:
                        path = HttpContext.Server.MapPath("~/Resources/EDITrackingJSONData/StatusPanel10.json");
                        break;
                    case 11:
                        path = HttpContext.Server.MapPath("~/Resources/EDITrackingJSONData/StatusPanel11.json");
                        break;
                    case 12:
                        path = HttpContext.Server.MapPath("~/Resources/EDITrackingJSONData/StatusPanel12.json");
                        break;
                    case 13:
                        path = HttpContext.Server.MapPath("~/Resources/EDITrackingJSONData/StatusPanel13.json");
                        break;
                    case 14:
                        path = HttpContext.Server.MapPath("~/Resources/EDITrackingJSONData/StatusPanel14.json");
                        break;
                    default:
                        path = HttpContext.Server.MapPath("~/Resources/EDITrackingJSONData/StatusPanel1.json");
                        break;

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