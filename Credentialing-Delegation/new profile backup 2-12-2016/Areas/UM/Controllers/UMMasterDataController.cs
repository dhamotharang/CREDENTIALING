using PortalTemplate.Areas.UM.Models.MasterDataEntities;
using PortalTemplate.Areas.UM.Models.ServiceModels;
using PortalTemplate.Areas.UM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PortalTemplate.Areas.UM.Controllers
{
    public class UMMasterDataController : Controller
    {
        readonly MasterDataService masterDataService = new MasterDataService();
        //
        // GET: /UM/UMMasterData/
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult GetDocumentName()
        {
            List<DocumentNameViewModel> ListDocs = masterDataService.GetDocumentName();
            JavaScriptSerializer serial = new JavaScriptSerializer();
            var ListDocName = serial.Serialize(ListDocs);

            return Json(ListDocName);
        }

        public JsonResult GetICDCodesFromService()
        {
            List<ICDServiceModel> ListICDs= masterDataService.GetICDCodesFromService();
            JavaScriptSerializer serial = new JavaScriptSerializer();
            var ListICDName = serial.Serialize(ListICDs);
            return Json(ListICDName);
        }
	}
}