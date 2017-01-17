using PortalTemplate.Areas.ETL.Models.StagingTracker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PortalTemplate.Areas.ETL.Controllers
{
    public class StagingTrackerController : Controller
    {
        //
        // GET: /ETL/StagingTracker/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DisplayStagingTracker()
        {
            string file = HostingEnvironment.MapPath("~/Areas/ETL/Resources/JSONData/EdwLogger/LoggerProjects.txt");
            string json = System.IO.File.ReadAllText(file);
            JavaScriptSerializer serial = new JavaScriptSerializer();
            List<string> LoggerProjects = new List<string>();
            LoggerProjects = serial.Deserialize<List<string>>(json);
            ViewData["LoggerProjects"] = LoggerProjects;
            return PartialView("~/Areas/ETL/Views/StagingTracker/_StagingTracker.cshtml");
        }

        public ActionResult LoadStagingTrackerTable(string ProjectName)
        {

            string file = GetStagingDataBasedOnProject(ProjectName);
            string json = System.IO.File.ReadAllText(file);
            JavaScriptSerializer serial = new JavaScriptSerializer();
            List<StagingTrackerDTO> _StagingTrackerDTO = new List<StagingTrackerDTO>();
            _StagingTrackerDTO = serial.Deserialize<List<StagingTrackerDTO>>(json);
            ViewData["_StagingTrackerDTO"] = _StagingTrackerDTO;
            ViewData["_StagingTrackerount"] = _StagingTrackerDTO.Count();
            return PartialView("~/Areas/ETL/Views/StagingTracker/StagingTrackerTable/_StagingTrackerTableData.cshtml");
        }

        public string GetStagingDataBasedOnProject(string ProjectName)
        {
            //call method for getting the data based on project name

            string file = "";
            if (ProjectName == null)
                file = HostingEnvironment.MapPath("~/Areas/ETL/Resources/JSONData/StagingTracker/StagingTrackerDataMin.txt");
            else if (ProjectName == "ACO")
                file = HostingEnvironment.MapPath("~/Areas/ETL/Resources/JSONData/StagingTracker/StagingTrackerDataMin.txt");
            else if (ProjectName == "Census")
                file = HostingEnvironment.MapPath("~/Areas/ETL/Resources/JSONData/StagingTracker/StagingTrackerDataMin.txt");
            else if (ProjectName == "Code")
                file = HostingEnvironment.MapPath("~/Areas/ETL/Resources/JSONData/StagingTracker/StagingTrackerData.txt");
            else if (ProjectName == "EMR")
                file = HostingEnvironment.MapPath("~/Areas/ETL/Resources/JSONData/StagingTracker/StagingTrackerData.txt");
            else if (ProjectName == "MAO")
                file = HostingEnvironment.MapPath("~/Areas/ETL/Resources/JSONData/StagingTracker/StagingTrackerData.txt");
            else if (ProjectName == "Monthly Claims")
                file = HostingEnvironment.MapPath("~/Areas/ETL/Resources/JSONData/StagingTracker/StagingTrackerData.txt");
            else if (ProjectName == "Provider Directory")
                file = HostingEnvironment.MapPath("~/Areas/ETL/Resources/JSONData/StagingTracker/StagingTrackerData.txt");
            else if (ProjectName == "Raps")
                file = HostingEnvironment.MapPath("~/Areas/ETL/Resources/JSONData/StagingTracker/StagingTrackerData.txt");
            else if (ProjectName == "Roster")
                file = HostingEnvironment.MapPath("~/Areas/ETL/Resources/JSONData/StagingTracker/StagingTrackerData.txt");
            else if (ProjectName == "Summit")
                file = HostingEnvironment.MapPath("~/Areas/ETL/Resources/JSONData/StagingTracker/StagingTrackerData.txt");
            else
                file = HostingEnvironment.MapPath("~/Areas/ETL/Resources/JSONData/StagingTracker/StagingTrackerData.txt");


            return file;
        }


	}
}