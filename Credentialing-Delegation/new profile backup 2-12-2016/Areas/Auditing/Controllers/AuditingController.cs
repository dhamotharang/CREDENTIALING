using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PortalTemplate.Areas.Auditing.Models.AuditingList;
using PortalTemplate.Areas.Auditing.Models.CreateAuditing;
using PortalTemplate.Areas.SharedView.Models.Encounter;
using PortalTemplate.Areas.Auditing.Services;
using PortalTemplate.Areas.Auditing.Services.IServices;


namespace PortalTemplate.Areas.Auditing.Controllers
{
    public class AuditingController : Controller
    {
        readonly IAuditingService _AuditingService;


        public AuditingController()
        {
            _AuditingService = new AuditingService();

        }

        //Auditing List----------------------------------------------//
        #region Auditing List
        public ActionResult GetAuditingList()
        {           
            return PartialView("~/Areas/Auditing/Views/AuditingList/_Index.cshtml",_AuditingService.GetAuditingListStatusCount());
        }


        public ActionResult GetCoderList()
        {
            return PartialView("~/Areas/Auditing/Views/AuditingList/_Coder.cshtml", _AuditingService.GetCoderList());
        }

        public ActionResult GetCommitteeList()
        {
            return PartialView("~/Areas/Auditing/Views/AuditingList/_Committee.cshtml", _AuditingService.GetCommitteeList());
        }

        public ActionResult GetProviderList()
        {
            return PartialView("~/Areas/Auditing/Views/AuditingList/_Provider.cshtml", _AuditingService.GetProviderList());
        }

        public ActionResult GetQCList()
        {
            return PartialView("~/Areas/Auditing/Views/AuditingList/_QC.cshtml", _AuditingService.GetQCList());
        }

        public ActionResult GetRBAuditingList()
        {
            return PartialView("~/Areas/Auditing/Views/AuditingList/_ReadytoBill.cshtml", _AuditingService.GetRBAuditingList());
        }

        public ActionResult GetDraftAuditingList()
        {
            return PartialView("~/Areas/Auditing/Views/AuditingList/_DraftAuditingList.cshtml", _AuditingService.GetDraftAuditingList());
        }
       
        public ActionResult GetInactiveList()
        {
            return PartialView("~/Areas/Auditing/Views/AuditingList/_Inactive.cshtml", _AuditingService.GetInactiveList());
        }
        #endregion
        //Auditing List----------------------------------------------// 

        //******************View Auditing*******************************//
        #region View Auditing
        public ActionResult ViewAuditing()
        {
            return PartialView("~/Areas/Auditing/Views/ViewAuditing/_ViewAuditing.cshtml", _AuditingService.ViewAuditing());
        }
        #endregion
        //******************View Auditing*******************************//

        //******************Edit Auditing*******************************//
        #region Edit Auditing
        public ActionResult EditAuditing()
        {
            ViewBag.AllCategories = _AuditingService.GetAllCategories();
            return PartialView("~/Areas/Auditing/Views/EditAuditing/_EditAuditing.cshtml", _AuditingService.EditAuditing());
        }


        public ActionResult SaveAuditDetails(EncounterDetailsViewModel EncounterDetails, ICDCodesInfoViewModel ICDCodeDetails, CPTCodesInfoViewModel CPTCodeDetails)
        {                                    
            return PartialView("~/Areas/Auditing/Views/EditAuditing/_EditAuditing.cshtml", _AuditingService.EditAuditing());
        }

        #endregion
        //******************Edit Auditing*******************************//

    }
}