using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Controllers
{
    public class ManageCarePlanController : Controller
    {
        //
        // GET: /ManageCarePlan/
        public ActionResult Index()
        {
            return View();
        }
        public object GetAllCarePlans()
        {
           
            try
            {
                String path;
                path = HttpContext.Server.MapPath("~/Resources/CM_JSON/CarePlanList.json");
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
        public object GetMasterCarePlan(string CarePlanID)
        {

            try
            {
                String path;
                path = HttpContext.Server.MapPath("~/Resources/CM_JSON/MasterCarePlan.json");
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

        public object GetMasterData()
        {
            try
            {
                String path;
                path = HttpContext.Server.MapPath("~/Resources/CM_JSON/CarePlanMasterData.json");
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