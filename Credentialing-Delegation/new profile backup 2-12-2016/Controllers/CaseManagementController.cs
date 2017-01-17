using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Controllers
{
    public class CaseManagementController : Controller
    {
        //
        // GET: /Episode/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DashBoard()
        {
            return View();
        }
        public ActionResult CreateNewEpisode()
        {
            return View();
        }
        public ActionResult MemberSummary()
        {
            return View();
        }
        public object GetMemberData(string MemberId)
        {
            try
            {
                String path;
                path = HttpContext.Server.MapPath("~/Resources/CM_JSON/MemberData.json");
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
        public object GetDashboardData(string UserId)
        {
            try
            {
                String path;
                path = HttpContext.Server.MapPath("~/Resources/CM_JSON/DashboardData.json");
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