using PortalTemplate.Areas.ETL.Models.FilesAvailability;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PortalTemplate.Areas.ETL.Controllers
{
    public class FileAvailabilityController : Controller
    {
        //
        // GET: /ETL/FileAvailability/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadFilesAvailabilityTable()
        {

            string file = HostingEnvironment.MapPath("~/Areas/ETL/Resources/JSONData/FilesAvailabilty/FilesAvailabiltyData.txt");
            string json = System.IO.File.ReadAllText(file);
            JavaScriptSerializer serial = new JavaScriptSerializer();
            List<FileAvailabiltyDTO> _FileAvailabilty = new List<FileAvailabiltyDTO>();
            _FileAvailabilty = serial.Deserialize<List<FileAvailabiltyDTO>>(json);
            ViewData["_FileAvailabilty"] = _FileAvailabilty;


            return PartialView("~/Areas/ETL/Views/FilesAvailability/_FilesAvailabiltyTable.cshtml");
        }

        public ActionResult LaodFileMissingTable()
        {
            string file = HostingEnvironment.MapPath("~/Areas/ETL/Resources/JSONData/FilesAvailabilty/FilesMissingData.txt");
            string json = System.IO.File.ReadAllText(file);
            JavaScriptSerializer serial = new JavaScriptSerializer();
            List<FilesMissingDTO> _FilesMissing = new List<FilesMissingDTO>();
            _FilesMissing = serial.Deserialize<List<FilesMissingDTO>>(json);
            ViewData["_FilesMissing"] = _FilesMissing;


            return PartialView("~/Areas/ETL/Views/FilesAvailability/_FileMissingTable.cshtml");
        }
	}
}