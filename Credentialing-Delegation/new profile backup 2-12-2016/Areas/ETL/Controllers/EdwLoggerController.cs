using PortalTemplate.Areas.ETL.Models.EdwLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PortalTemplate.Areas.ETL.Controllers
{
    public class EdwLoggerController : Controller
    {
        //
        // GET: /ETL/EdwLogger/
        public ActionResult Index()
        {
            return PartialView("~/Areas/ETL/Views/EdwLogger/LivePrestageLogger.cshtml");
        }

        public ActionResult DisplayEdwLogger()
        {
            //get total file Count
            ViewData["FileCount"] = 14840;
            //get Total file size
            ViewData["TotalFileSize"] = 2021;
            return PartialView("~/Areas/ETL/Views/EdwLogger/_EdwLogger.cshtml");
        }


        public ActionResult DisplayLivePrestageLogger()
        {


            string file = HostingEnvironment.MapPath("~/Areas/ETL/Resources/JSONData/EdwLogger/LoggerProjects.txt");
            string json = System.IO.File.ReadAllText(file);
            JavaScriptSerializer serial = new JavaScriptSerializer();
            List<string> LoggerProjects = new List<string>();
            LoggerProjects = serial.Deserialize<List<string>>(json);
            ViewData["LoggerProjects"] = LoggerProjects;


            return PartialView("~/Areas/ETL/Views/EdwLogger/_LivePrestageLogger.cshtml");
        }

        public ActionResult DisplayLiveFileTracker()
        {
            string file = HostingEnvironment.MapPath("~/Areas/ETL/Resources/JSONData/EdwLogger/LoggerProjects.txt");
            string json = System.IO.File.ReadAllText(file);
            JavaScriptSerializer serial = new JavaScriptSerializer();
            List<string> LoggerProjects = new List<string>();
            LoggerProjects = serial.Deserialize<List<string>>(json);
            ViewData["LoggerProjects"] = LoggerProjects;


            return PartialView("~/Areas/ETL/Views/EdwLogger/_LiveFileTracker.cshtml");
        }

        public ActionResult DisplayEdwDatabaseModalBody()
        {
            string file = HostingEnvironment.MapPath("~/Areas/ETL/Resources/JSONData/EdwLogger/EdwDataBases.txt");
            string json = System.IO.File.ReadAllText(file);
            JavaScriptSerializer serial = new JavaScriptSerializer();
            List<EdwDatabases> _EdwDatabases = new List<EdwDatabases>();
            _EdwDatabases = serial.Deserialize<List<EdwDatabases>>(json);
            ViewData["_EdwDatabases"] = _EdwDatabases;
            ViewData["_EdwDatabaseLength"] = _EdwDatabases.Count();
            return PartialView("~/Areas/ETL/Views/EdwLogger/EdwDatabasesModal/_EdwDatabasesModalBody.cshtml");
        }

        public ActionResult PrestageLoggerDataForProject(string ProjectName)
        {
            //get data based on project Name
            string file = GetLoggerDataBasedOnProject(ProjectName);
            string json = System.IO.File.ReadAllText(file);
            JavaScriptSerializer serial = new JavaScriptSerializer();
            List<PreStageLoggerData> _PreStageLoggerData = new List<PreStageLoggerData>();
            _PreStageLoggerData = serial.Deserialize<List<PreStageLoggerData>>(json);
            ViewData["_PreStageLoggerData"] = _PreStageLoggerData;
            ViewData["_PreStageLoggerDataCount"] = _PreStageLoggerData.Count();


            return PartialView("~/Areas/ETL/Views/EdwLogger/LivePrestageLogger/_LivePrestageLoggerTableData.cshtml");
        }

        public ActionResult FileTrackerDataForProject(string ProjectName)
        {
            //get data based on project Name
            string file = GetTrackerDataBasedOnProject(ProjectName);
            
            string json = System.IO.File.ReadAllText(file);
            JavaScriptSerializer serial = new JavaScriptSerializer();
            List<FileTrackerDTO> _FileTrackerDTO = new List<FileTrackerDTO>();
            _FileTrackerDTO = serial.Deserialize<List<FileTrackerDTO>>(json);
            ViewData["_FileTrackerDTO"] = _FileTrackerDTO;
            ViewData["_FileTrackerCount"] = _FileTrackerDTO.Count();
            return PartialView("~/Areas/ETL/Views/EdwLogger/LiveFileTracker/_LiveFileTrackerTableData.cshtml");
        }


        public string GetLoggerDataBasedOnProject(string ProjectName)
        {
            //call method for getting the data based on project name

            string file = "";
            if (ProjectName == null)
                file = HostingEnvironment.MapPath("~/Areas/ETL/Resources/JSONData/EdwLogger/LivePrestageLogger/LivePrestageLoggerData.txt");
            else if (ProjectName == "ACO")
                file = HostingEnvironment.MapPath("~/Areas/ETL/Resources/JSONData/EdwLogger/LivePrestageLogger/LivePrestageLoggerData.txt");
            else if (ProjectName == "Census")
                file = HostingEnvironment.MapPath("~/Areas/ETL/Resources/JSONData/EdwLogger/LivePrestageLogger/PrestageLoggerDataAco.txt");
            else if (ProjectName == "Code")
                file = HostingEnvironment.MapPath("~/Areas/ETL/Resources/JSONData/EdwLogger/LivePrestageLogger/PrestageLoggerDataAco.txt");
            else if (ProjectName == "EMR")
                file = HostingEnvironment.MapPath("~/Areas/ETL/Resources/JSONData/EdwLogger/LivePrestageLogger/PrestageLoggerDataAco.txt");
            else if (ProjectName == "MAO")
                file = HostingEnvironment.MapPath("~/Areas/ETL/Resources/JSONData/EdwLogger/LivePrestageLogger/PrestageLoggerDataAco.txt");
            else if (ProjectName == "Monthly Claims")
                file = HostingEnvironment.MapPath("~/Areas/ETL/Resources/JSONData/EdwLogger/LivePrestageLogger/PrestageLoggerDataAco.txt");
            else if (ProjectName == "Provider Directory")
                file = HostingEnvironment.MapPath("~/Areas/ETL/Resources/JSONData/EdwLogger/LivePrestageLogger/PrestageLoggerDataAco.txt");
            else if (ProjectName == "Raps")
                file = HostingEnvironment.MapPath("~/Areas/ETL/Resources/JSONData/EdwLogger/LivePrestageLogger/PrestageLoggerDataAco.txt");
            else if (ProjectName == "Roster")
                file = HostingEnvironment.MapPath("~/Areas/ETL/Resources/JSONData/EdwLogger/LivePrestageLogger/PrestageLoggerDataAco.txt");
            else if (ProjectName == "Summit")
                file = HostingEnvironment.MapPath("~/Areas/ETL/Resources/JSONData/EdwLogger/LivePrestageLogger/PrestageLoggerDataAco.txt");
            else
                file = HostingEnvironment.MapPath("~/Areas/ETL/Resources/JSONData/EdwLogger/LivePrestageLogger/PrestageLoggerDataAco.txt");


            return file;
        }

        public string GetTrackerDataBasedOnProject(string ProjectName)
        {
            //call method for getting the data based on project name

            string file = "";
            if (ProjectName == null)
                file = HostingEnvironment.MapPath("~/Areas/ETL/Resources/JSONData/EdwLogger/LiveFileTracker/FileTrackerData.txt");
            else if (ProjectName == "ACO")
                file = HostingEnvironment.MapPath("~/Areas/ETL/Resources/JSONData/EdwLogger/LiveFileTracker/FileTrackerData.txt");
            else if (ProjectName == "Census")
                file = HostingEnvironment.MapPath("~/Areas/ETL/Resources/JSONData/EdwLogger/LiveFileTracker/FileTrackerData.txt");
            else if (ProjectName == "Code")
                file = HostingEnvironment.MapPath("~/Areas/ETL/Resources/JSONData/EdwLogger/LiveFileTracker/FileTrackerDataMin.txt");
            else if (ProjectName == "EMR")
                file = HostingEnvironment.MapPath("~/Areas/ETL/Resources/JSONData/EdwLogger/LiveFileTracker/FileTrackerDataMin.txt");
            else if (ProjectName == "MAO")
                file = HostingEnvironment.MapPath("~/Areas/ETL/Resources/JSONData/EdwLogger/LiveFileTracker/FileTrackerDataMin.txt");
            else if (ProjectName == "Monthly Claims")
                file = HostingEnvironment.MapPath("~/Areas/ETL/Resources/JSONData/EdwLogger/LiveFileTracker/FileTrackerDataMin.txt");
            else if (ProjectName == "Provider Directory")
                file = HostingEnvironment.MapPath("~/Areas/ETL/Resources/JSONData/EdwLogger/LiveFileTracker/FileTrackerDataMin.txt");
            else if (ProjectName == "Raps")
                file = HostingEnvironment.MapPath("~/Areas/ETL/Resources/JSONData/EdwLogger/LiveFileTracker/FileTrackerDataMin.txt");
            else if (ProjectName == "Roster")
                file = HostingEnvironment.MapPath("~/Areas/ETL/Resources/JSONData/EdwLogger/LiveFileTracker/FileTrackerDataMin.txt");
            else if (ProjectName == "Summit")
                file = HostingEnvironment.MapPath("~/Areas/ETL/Resources/JSONData/EdwLogger/LiveFileTracker/FileTrackerDataMin.txt");
            else
                file = HostingEnvironment.MapPath("~/Areas/ETL/Resources/JSONData/EdwLogger/LiveFileTracker/FileTrackerDataMin.txt");


            return file;
        }
        

	}
}