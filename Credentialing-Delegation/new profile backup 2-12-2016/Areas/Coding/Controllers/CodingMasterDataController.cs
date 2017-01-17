using PortalTemplate.Areas.Coding.Models.CPTCodes;
using PortalTemplate.Areas.Coding.Models.CreateCoding;
using PortalTemplate.Areas.Coding.Models.ICDCodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PortalTemplate.Areas.Coding.Controllers
{
    public class CodingMasterDataController : Controller
    {
        public JsonResult GetICDCodesMasterData(string searchTerm)
        {
            List<HCCCodeViewModel> HccCodeList1 = new List<HCCCodeViewModel>();
            List<HCCCodeViewModel> HccCodeList2 = new List<HCCCodeViewModel>();
            HCCCodeViewModel HccCode1 = new HCCCodeViewModel();
            HCCCodeViewModel HccCode2 = new HCCCodeViewModel();
            HccCode1.Code = "23";
            HccCode1.Description = "Septicemia/Shock";
            HccCode1.Type = "Medical";
            HccCode1.Version = "v22";
            HccCode1.Weight = "0.2";

            HccCode2.Code = "23";
            HccCode2.Description = "Septicemia/Shock";
            HccCode2.Type = "Medical";
            HccCode2.Version = "v22";
            HccCode2.Weight = "0.2";

            HccCodeList1.Add(HccCode1);
            HccCodeList1.Add(HccCode2);
            HccCodeList2.Add(HccCode2);

          
            List<ICDCodeViewModel> ICDCodeLists = new List<ICDCodeViewModel>();
            ICDCodeLists.Add(new ICDCodeViewModel { ICDCode = "A207", Description = "Septicemic Plague", HCCCodes = HccCodeList1, IsChronic = true, ChronicCount = 2 });
            ICDCodeLists.Add(new ICDCodeViewModel { ICDCode = "B207", Description = "Septicemic Plague", HCCCodes = HccCodeList2, IsChronic = false });
            ICDCodeLists.Add(new ICDCodeViewModel { ICDCode = "C207", Description = "Septicemic Plague", HCCCodes = HccCodeList2 });
            ICDCodeLists.Add(new ICDCodeViewModel { ICDCode = "D207", Description = "Septicemic Plague", HCCCodes = HccCodeList2, IsChronic = true, ChronicCount = 0 });
            ICDCodeLists.Add(new ICDCodeViewModel { ICDCode = "E207", Description = "Septicemic Plague", HCCCodes = HccCodeList2, IsChronic = true, ChronicCount = 1 });

            var FilteredICDs = ICDCodeLists.FindAll(m => m.ICDCode.Contains(searchTerm));
            JavaScriptSerializer ser = new JavaScriptSerializer();
            var filteredListInJson = ser.Serialize(FilteredICDs);
            return Json(filteredListInJson, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCPTCodesMasterData(string searchTerm)
        {
            List<CPTCodeViewModel> CPTCodeLists = new List<CPTCodeViewModel>();
            CPTCodeLists.Add(new CPTCodeViewModel { Code = "77852", Description = "ANESTH SPECIAL HEAD SURGERY", Fee = 35.66 });
            CPTCodeLists.Add(new CPTCodeViewModel { Code = "77853", Description = "ANESTH HEAD VESSEL SURGERY", Fee = 52.66 });
            CPTCodeLists.Add(new CPTCodeViewModel { Code = "77854", Description = "ANESTH SKULL REPAIR/FRACT", Fee = 85.66 });
            CPTCodeLists.Add(new CPTCodeViewModel { Code = "99201", Description = "ANESTH SKULL DRAINAGE", Fee = 45.66 });
            CPTCodeLists.Add(new CPTCodeViewModel { Code = "99202", Description = "ANESTH SKULL DRAINAGE", Fee = 28.66 });
            var FilteredCPTs = CPTCodeLists.FindAll(m => m.Code.Contains(searchTerm));
            JavaScriptSerializer ser = new JavaScriptSerializer();
            var filteredListInJson = ser.Serialize(FilteredCPTs);
            return Json(filteredListInJson, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult GetDocumentCategories(string SearchCategory)
        //{
        //    List<DocumentCategoryDTO> categories = new List<DocumentCategoryDTO>();
        //    categories.Add(new DocumentCategoryDTO { CategoryName = "LAB REPORT" });
        //    categories.Add(new DocumentCategoryDTO { CategoryName = "IMAGE" });
        //    categories.Add(new DocumentCategoryDTO { CategoryName = "CONSULTATION" });
        //    categories.Add(new DocumentCategoryDTO { CategoryName = "HOSPITAL" });
        //    categories.Add(new DocumentCategoryDTO { CategoryName = "CHART" });
        //    categories.Add(new DocumentCategoryDTO { CategoryName = "PRESCRIPTION" });
        //    categories.Add(new DocumentCategoryDTO { CategoryName = "REFERENCE NOTES" });
        //    categories.Add(new DocumentCategoryDTO { CategoryName = "HOSPITAL DISCHARGE" });
        //    categories.Add(new DocumentCategoryDTO { CategoryName = "ECW PHYSICIAN TO PHYSICIAN" });
        //    categories.Add(new DocumentCategoryDTO { CategoryName = "SPECIALITY FORMS" });
        //    categories.Add(new DocumentCategoryDTO { CategoryName = "EXAMINATION" });
        //    categories.Add(new DocumentCategoryDTO { CategoryName = "PHYSICAL EXAM DRAWING" });
        //    categories.Add(new DocumentCategoryDTO { CategoryName = "PROCEDURE DRAWING" });
        //    categories.Add(new DocumentCategoryDTO { CategoryName = "PROCEDURE DOCUMENT" });
        //    categories.Add(new DocumentCategoryDTO { CategoryName = "EOB" });
        //    categories.Add(new DocumentCategoryDTO { CategoryName = "ADVANCE DIRECTORY" });
        //    categories.Add(new DocumentCategoryDTO { CategoryName = "OTHERS" });
        //    var FilteredCategories = categories.FindAll(m => m.CategoryName.Contains(SearchCategory));
        //    JavaScriptSerializer serialiser = new JavaScriptSerializer();
        //    var categoryFilteredListJson = serialiser.Serialize(FilteredCategories);
        //    return Json(categoryFilteredListJson, JsonRequestBehavior.AllowGet);
        //}
    }
}
