using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PortalTemplate.Areas.Coding.Models.CodingList;
using PortalTemplate.Areas.Coding.Models.ICDCodes;
using PortalTemplate.Areas.Coding.Models.CPTCodes;
using Newtonsoft.Json;
using PortalTemplate.Areas.Coding.Models.ICDCPTMapping;
using PortalTemplate.Areas.Coding.Models.CreateCoding;
using PortalTemplate.Areas.SharedView.Models.Encounter;
using PortalTemplate.Areas.Coding.Services.IServices;
using PortalTemplate.Areas.Coding.Services;
using PortalTemplate.Areas.Coding.Models.DashBoard;
using PortalTemplate.Areas.Coding.Models.PowerDriveService;
using System.IO;
using PortalTemplate.Areas.Coding.DTO;
using System.Threading.Tasks;


namespace PortalTemplate.Areas.Coding.Controllers
{
    public class CodingController : Controller
    {
        ICodingService _CodingService;
        IPowerDriveService _powerDriveService;
        public CodingController()
        {
            _CodingService = new CodingService();
            _powerDriveService = new PowerDriveService();
        }

        #region Coding DashBoard
        public ActionResult GetCodingDashBoardDetails()
        {
            return PartialView("~/Areas/Coding/Views/CodingDashBoard/_CodingDashBoard.cshtml", _CodingService.GetCodingDashBoardDetails());
        }
        #endregion

        #region Coding List
        //To get Data for Open List in Coding..
        public ActionResult GetOpenCodingList()
        {
            //var OpenList = _CodingService.GetCodingListByStatus("Open");
            var OpenLists = new List<CodingListViewModel>();
            OpenLists.Add(new CodingListViewModel { EncounterID = "116244953", MemberID = "1607132022", MemberFirstName = "Kameisha", MemberLastName = "Oliver", ProviderNPI = "3282556434", ProviderFirstName = "Ignazio", ProviderLastName = "Rubino", Facility = "ALL SAINTS...", DateOfService = new DateTime(2015, 05, 06), DateOfCreation = new DateTime(2015, 06, 08), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "Open" });
            OpenLists.Add(new CodingListViewModel { EncounterID = "116244953", MemberID = "1607132022", MemberFirstName = "Kameisha", MemberLastName = "Oliver", ProviderNPI = "3282556434", ProviderFirstName = "Ignazio", ProviderLastName = "Rubino", Facility = "ALL SAINTS...", DateOfService = new DateTime(2015, 05, 06), DateOfCreation = new DateTime(2015, 06, 08), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "Open" });
            OpenLists.Add(new CodingListViewModel { EncounterID = "116244953", MemberID = "1607132022", MemberFirstName = "Kameisha", MemberLastName = "Oliver", ProviderNPI = "3282556434", ProviderFirstName = "Ignazio", ProviderLastName = "Rubino", Facility = "ALL SAINTS...", DateOfService = new DateTime(2015, 05, 06), DateOfCreation = new DateTime(2015, 06, 08), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "Open" });
            OpenLists.Add(new CodingListViewModel { EncounterID = "116244953", MemberID = "1607132022", MemberFirstName = "Kameisha", MemberLastName = "Oliver", ProviderNPI = "3282556434", ProviderFirstName = "Ignazio", ProviderLastName = "Rubino", Facility = "ALL SAINTS...", DateOfService = new DateTime(2015, 05, 06), DateOfCreation = new DateTime(2015, 06, 08), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "Open" });
            OpenLists.Add(new CodingListViewModel { EncounterID = "116244953", MemberID = "1607132022", MemberFirstName = "Kameisha", MemberLastName = "Oliver", ProviderNPI = "3282556434", ProviderFirstName = "Ignazio", ProviderLastName = "Rubino", Facility = "ALL SAINTS...", DateOfService = new DateTime(2015, 05, 06), DateOfCreation = new DateTime(2015, 06, 08), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "Open" });
            OpenLists.Add(new CodingListViewModel { EncounterID = "116244953", MemberID = "1607132022", MemberFirstName = "Kameisha", MemberLastName = "Oliver", ProviderNPI = "3282556434", ProviderFirstName = "Ignazio", ProviderLastName = "Rubino", Facility = "ALL SAINTS...", DateOfService = new DateTime(2015, 05, 06), DateOfCreation = new DateTime(2015, 06, 08), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "Open" });
            OpenLists.Add(new CodingListViewModel { EncounterID = "116244953", MemberID = "1607132022", MemberFirstName = "Kameisha", MemberLastName = "Oliver", ProviderNPI = "3282556434", ProviderFirstName = "Ignazio", ProviderLastName = "Rubino", Facility = "ALL SAINTS...", DateOfService = new DateTime(2015, 05, 06), DateOfCreation = new DateTime(2015, 06, 08), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "Open" });
            OpenLists.Add(new CodingListViewModel { EncounterID = "116244953", MemberID = "1607132022", MemberFirstName = "Kameisha", MemberLastName = "Oliver", ProviderNPI = "3282556434", ProviderFirstName = "Ignazio", ProviderLastName = "Rubino", Facility = "ALL SAINTS...", DateOfService = new DateTime(2015, 05, 06), DateOfCreation = new DateTime(2015, 06, 08), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "Open" });

            return PartialView("~/Areas/Coding/Views/CodingList/_OpenCodingList.cshtml", OpenLists);
        }

        public ActionResult GetOpenEncounters(int index, string sortingType, string sortBy, CodingListViewModel SearchObject)
        {
            var OpenLists = new List<CodingListViewModel>();
            OpenLists.Add(new CodingListViewModel { EncounterID = "116244953", MemberID = "1607132022", MemberFirstName = "Kameisha", MemberLastName = "Oliver", ProviderNPI = "3282556434", ProviderFirstName = "Ignazio", ProviderLastName = "Rubino", Facility = "ALL SAINTS...", DateOfService = new DateTime(2015, 05, 06), DateOfCreation = new DateTime(2015, 06, 08), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "Open" });
            OpenLists.Add(new CodingListViewModel { EncounterID = "116244953", MemberID = "1607132022", MemberFirstName = "Kameisha", MemberLastName = "Oliver", ProviderNPI = "3282556434", ProviderFirstName = "Ignazio", ProviderLastName = "Rubino", Facility = "ALL SAINTS...", DateOfService = new DateTime(2015, 05, 06), DateOfCreation = new DateTime(2015, 06, 08), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "Open" });
            OpenLists.Add(new CodingListViewModel { EncounterID = "116244953", MemberID = "1607132022", MemberFirstName = "Kameisha", MemberLastName = "Oliver", ProviderNPI = "3282556434", ProviderFirstName = "Ignazio", ProviderLastName = "Rubino", Facility = "ALL SAINTS...", DateOfService = new DateTime(2015, 05, 06), DateOfCreation = new DateTime(2015, 06, 08), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "Open" });
            OpenLists.Add(new CodingListViewModel { EncounterID = "116244953", MemberID = "1607132022", MemberFirstName = "Kameisha", MemberLastName = "Oliver", ProviderNPI = "3282556434", ProviderFirstName = "Ignazio", ProviderLastName = "Rubino", Facility = "ALL SAINTS...", DateOfService = new DateTime(2015, 05, 06), DateOfCreation = new DateTime(2015, 06, 08), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "Open" });
            OpenLists.Add(new CodingListViewModel { EncounterID = "116244953", MemberID = "1607132022", MemberFirstName = "Kameisha", MemberLastName = "Oliver", ProviderNPI = "3282556434", ProviderFirstName = "Ignazio", ProviderLastName = "Rubino", Facility = "ALL SAINTS...", DateOfService = new DateTime(2015, 05, 06), DateOfCreation = new DateTime(2015, 06, 08), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "Open" });
            OpenLists.Add(new CodingListViewModel { EncounterID = "116244953", MemberID = "1607132022", MemberFirstName = "Kameisha", MemberLastName = "Oliver", ProviderNPI = "3282556434", ProviderFirstName = "Ignazio", ProviderLastName = "Rubino", Facility = "ALL SAINTS...", DateOfService = new DateTime(2015, 05, 06), DateOfCreation = new DateTime(2015, 06, 08), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "Open" });
            OpenLists.Add(new CodingListViewModel { EncounterID = "116244953", MemberID = "1607132022", MemberFirstName = "Kameisha", MemberLastName = "Oliver", ProviderNPI = "3282556434", ProviderFirstName = "Ignazio", ProviderLastName = "Rubino", Facility = "ALL SAINTS...", DateOfService = new DateTime(2015, 05, 06), DateOfCreation = new DateTime(2015, 06, 08), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "Open" });
            OpenLists.Add(new CodingListViewModel { EncounterID = "116244953", MemberID = "1607132022", MemberFirstName = "Kameisha", MemberLastName = "Oliver", ProviderNPI = "3282556434", ProviderFirstName = "Ignazio", ProviderLastName = "Rubino", Facility = "ALL SAINTS...", DateOfService = new DateTime(2015, 05, 06), DateOfCreation = new DateTime(2015, 06, 08), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "Open" });

            return PartialView("~/Areas/Coding/Views/CodingListPartials/OpenListPartial.cshtml", OpenLists);
        }


        //To get Data for Draft List in Coding..
        public ActionResult GetDraftCodingList()
        {
            var DraftList = _CodingService.GetCodingListByStatus("Draft");
            return PartialView("~/Areas/Coding/Views/CodingList/_DraftCodingList.cshtml", DraftList);
        }
        public ActionResult GetDraftEncounters()
        {
            var DraftList = _CodingService.GetCodingListByStatus("Draft");
            return PartialView("~/Areas/Coding/Views/CodingListPartials/_DraftListPartial.cshtml", DraftList);
        }
        //To get Data for Ready to Audit List in Coding..
        public ActionResult GetRACodingList()
        {
            var RAList = _CodingService.GetCodingListByStatus("ReadyToAudit");
            return PartialView("~/Areas/Coding/Views/CodingList/_ReadytoAuditCodingList.cshtml", RAList);
        }
        public ActionResult GetRAEncounters()
        {
            var RAList = _CodingService.GetCodingListByStatus("ReadyToAudit");
            return PartialView("~/Areas/Coding/Views/CodingListPartials/_ReadyToAuditListPartial.cshtml", RAList);
        }


        //To get Data for Inactive List in Coding..
        public ActionResult GetInactiveCodingList()
        {
            var InActiveList = _CodingService.GetCodingListByStatus("InActive");
            return PartialView("~/Areas/Coding/Views/CodingList/_DeactivateCodingList.cshtml", InActiveList);
        }
        public ActionResult GetInactiveCodedEncounters()
        {
            var InActiveList = _CodingService.GetCodingListByStatus("InActive");
            return PartialView("~/Areas/Coding/Views/CodingListPartials/_DeactiveListPartial.cshtml", InActiveList);
        }

        //Get the Rejected Coding tab Data..
        public ActionResult GetRejectedCodingList()
        {
            var RejectedList = _CodingService.GetCodingListByStatus("Rejected");
            return PartialView("~/Areas/Coding/Views/CodingList/_RejectedCodingList.cshtml", RejectedList);
        }
        public ActionResult GetRejectedCodedEncounters()
        {
            var RejectedList = _CodingService.GetCodingListByStatus("Rejected");
            return PartialView("~/Areas/Coding/Views/CodingListPartials/_RejectedListPartial.cshtml", RejectedList);
        }
        //To get Counts of all Tabs in Coding List..
        public ActionResult GetCodingList()
        {
            return PartialView("~/Areas/Coding/Views/CodingList/Index.cshtml", _CodingService.GetCodingListStatusCount());
        }

        //Get the Deactivate Required Data..
        public ActionResult GetDeactivatemodalData()
        {
            return PartialView("~/Areas/Coding/Views/CodingList/_DeactivateEncounterModal.cshtml", _CodingService.GetDeactivatemodalData());
        }



        //Deactivate an Encounter..
        public Boolean DeactivateEncounter()
        {
            return true;
        }

        #endregion

        #region Create Coding
        public ActionResult Create()
        {
            ViewBag.Categories = _CodingService.GetAllCategories();
            return PartialView("~/Areas/Coding/Views/EditCoding/_EditCoding.cshtml", _CodingService.Create());
        }

        public ActionResult GetICDCodeHistory()
        {
            return PartialView("~/Areas/Coding/Views/CreateCoding/_ICDHistory.cshtml", _CodingService.GetICDCodeHistory());
        }

        public JsonResult GetIcdHistoryData()
        {
            return Json(new { status = true, Icdcodes = _CodingService.GetIcdHistoryData() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCPTCodeHistory()
        {
            return PartialView("~/Areas/Coding/Views/CreateCoding/_CPTHistory.cshtml", _CodingService.GetCPTCodeHistory());
        }

        public JsonResult GetCptHistoryData()
        {
            return Json(new { status = true, Cptcodes = _CodingService.GetCptHistoryData() }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Edit Coding
        public ActionResult CreateCPT(int index)
        {
            ViewData.TemplateInfo.HtmlFieldPrefix = "CPTCodes[" + index + "]";
            return PartialView("~/Areas/Coding/Views/EditCoding/_AddCPT.cshtml");
        }
        public ActionResult CreateICD(int index)
        {
            ViewData.TemplateInfo.HtmlFieldPrefix = "ICDCodes[" + index + "]";
            return PartialView("~/Areas/Coding/Views/EditCoding/_AddICD.cshtml");
        }
        public ActionResult EditDetails()
        {
            ViewBag.Categories = _CodingService.GetAllCategories();
            return PartialView("~/Areas/Coding/Views/EditCoding/_EditCoding.cshtml", _CodingService.EditDetails());
        }
        public async Task<bool> SaveDetails(SaveCreateCodingDTO SaveCoding)
        {

            var result = await _CodingService.SaveCoding(SaveCoding);
            return true;
        }


        public void SaveDocument(DocumentCreateViewModel Document)
        {
            FileService fileService = new FileService();
            fileService.UserInfo = new UserInfo { UserName = "tj@gmail.com", ApplicaionOrGroupName = "EDI", Token = "" };

            fileService.DocumentAndStreams = new List<DocumentAndStream>();

            DocumentAndStream DocumentAndStream = new DocumentAndStream { Document = new Document { FileName = Document.File.FileName, TransferType = TransferType.Stream, Extension = GetExtension(Document.File.FileName) }, InputStream = Document.File.InputStream };
            fileService.DocumentAndStreams.Add(DocumentAndStream);

            FileUploadResponse response = _powerDriveService.UploadFileService(fileService);

        }
        #endregion

        #region View Coding
        public ActionResult ViewDetails()
        {
            return PartialView("~/Areas/Coding/Views/ViewCoding/_ViewCoding.cshtml", _CodingService.ViewDetails());
        }

        #endregion

        public ActionResult AddICDFromHistory(ICDCodeDetailsViewModel ICDCodDetails, ICDCodeViewModel ICDHistoryDetails)
        {
            return PartialView("~/Areas/Coding/Views/EditCoding/_ICDCodeDetails.cshtml", ICDCodDetails);
        }

        private string GetExtension(string FileName)
        {
            string extension = FileName.Substring(FileName.LastIndexOf('.') + 1, FileName.Length - FileName.LastIndexOf('.') - 1);
            return extension;
        }

        public ActionResult OpenDeactivateModal(CodingListViewModel data)
        {
            DeactivateEncounter DeactiveDetails = new DeactivateEncounter();
            DeactiveDetails.EncounterID = data.EncounterID;
            DeactiveDetails.Address = data.Address;
            DeactiveDetails.CurrentStatus = data.Status;
            DeactiveDetails.DeactiveReason = "";
            DeactiveDetails.DOB = data.MemberDOB;
            DeactiveDetails.Gender = data.MemberGender;
            DeactiveDetails.MemberFirstName = data.MemberFirstName;
            DeactiveDetails.MemberID = data.MemberID;
            DeactiveDetails.MemberLastName = data.MemberLastName;
            DeactiveDetails.PlanName = data.PlanName;
            DeactiveDetails.PreviousStatus = data.Status;
            return PartialView("~/Areas/Coding/Views/CodingList/_DeactivateEncounterModal.cshtml", DeactiveDetails);
        }

        public ActionResult DeactivateCodedEncounter(string EncounterID)
        {
            return PartialView("~/Areas/Coding/Views/CodingList/Index.cshtml", _CodingService.GetCodingListStatusCount());
        }

        public ActionResult ReactivateCodedEncounter(string EncounterID)
        {
            return PartialView("~/Areas/Coding/Views/CodingList/Index.cshtml", _CodingService.GetCodingListStatusCount());
        }


        public ActionResult GetEncounterCategoryList()
        {
            CategoryReasonViewModel EncounterCategoriesList = new CategoryReasonViewModel();
            List<CategoryViewModel> Categorires = _CodingService.GetAllCategories();
            EncounterCategoriesList.selectedCategories = Categorires;
            return PartialView("~/Areas/Coding/Views/EditCoding/_AddCategoryRemarksmodal.cshtml",EncounterCategoriesList);
        }

        public bool SaveEncounterCategories(CategoryReasonViewModel data)
        {
            return true;
        }
    }
}
