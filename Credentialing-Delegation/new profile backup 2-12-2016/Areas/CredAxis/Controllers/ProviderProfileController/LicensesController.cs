using PortalTemplate.Areas.CredAxis.Models.LicensesViewModel;
using PortalTemplate.Areas.CredAxis.Services;
using PortalTemplate.Areas.CredAxis.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CredAxis.Controllers.ProviderProfileController
{
    public class LicensesController : Controller
    {
        private ILicensesService _LicensesCode = null;
        private LicenseMainViewModel tempModel = null;
        string Action1 = "Edit";
        string Action2 = "Add";
        string Action3 = "View";
        public LicensesController()
        {
            _LicensesCode = new LicenseService();
            LicenseMainViewModel tempModel = new LicenseMainViewModel();

        }

        //
        // GET: /CredAxis/Demographics/
        public ActionResult Index(string ModeRequested)
        {
            LicenseMainViewModel theModel = new LicenseMainViewModel();

            theModel = _LicensesCode.GetAllLicensesCode();
            if (ModeRequested == "EDIT")
            {
                return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Licenses/_LicensesIndexEditMode.cshtml", theModel);
            }
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Licenses/_LicensesIndex.cshtml", theModel);
        }

        [HttpPost]
        public JsonResult AddEditLicensesCode(LicenseMainViewModel licenseMainViewModel)
        {
            if (ModelState.IsValid)
            {
                var AddEditLicensesCode = _LicensesCode.AddEditLicensesCode(licenseMainViewModel);
                return Json(new { Result = AddEditLicensesCode, status = "done" });
            }

            return Json(new { status = "false" });
        }

        // Method for Get Empty CDS Info Partial View
        //Added by Sai Jaswanth
        public ActionResult GetCDSInformationPartial(CdsInfo model, string ActionType)
        {
            List<CdsInfo> CdsInfoEmptyModel = new List<CdsInfo>();
            if (ActionType == Action2)
            {
                CdsInfoEmptyModel.Add(model);
            }
            if (ActionType == Action1 || ActionType == Action3)
            {
                tempModel = _LicensesCode.GetAllLicensesCode();
                CdsInfoEmptyModel = tempModel.CDS;
            }
            if (ActionType == Action3)
            {
                return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Licenses/CDSInfo/_ViewCDSInfo.cshtml", CdsInfoEmptyModel);
            }
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Licenses/CDSInfo/_AddEditCDSInformation.cshtml", CdsInfoEmptyModel);

            // CdsInfo CdsInfoEmptyModel = new CdsInfo();
            //return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Licenses/CDSInfo/_AddEditCDSInformation.cshtml", CdsInfoEmptyModel);
        }

        // Method for Add CDS Info
        //Added by Sai Jaswanth
        [HttpPost]
        public ActionResult AddCDSInfo(CdsInfo CDSInfoData)
        {
            LicenseMainViewModel theModel = new LicenseMainViewModel();
            theModel = _LicensesCode.GetAllLicensesCode();
            theModel.CDS.Add(CDSInfoData);
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Licenses/CDSInfo/_ViewCDSInfo.cshtml", theModel.CDS);
        }


        // Method for Get Empty Medicaid Info Partial View
        //Added by Sai Jaswanth
        public ActionResult GetMedicaidInformationPartial(MedicaidInfo model, string ActionType)
        {
            List<MedicaidInfo> MdeicaidEmptyModel = new List<MedicaidInfo>();
            if (ActionType == Action2)
            {
                MdeicaidEmptyModel.Add(model);
            }
            if (ActionType == Action1 || ActionType == Action3)
            {
                tempModel = _LicensesCode.GetAllLicensesCode();
                MdeicaidEmptyModel = tempModel.mediciad;
            }
            if (ActionType == Action3)
            {
                return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Licenses/MedicaidInfo/_ViewMedicaidInfo.cshtml", MdeicaidEmptyModel);
            }
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Licenses/MedicaidInfo/_AddEditMedicaidInfo.cshtml", MdeicaidEmptyModel);
        }

        // Method for Add Medicaid Info
        //Added by Sai Jaswanth
        [HttpPost]
        public ActionResult AddMedicaidInfo(MedicaidInfo MediciadInfoData)
        {
            LicenseMainViewModel theModel = new LicenseMainViewModel();
            theModel = _LicensesCode.GetAllLicensesCode();
            theModel.mediciad.Add(MediciadInfoData);
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Licenses/MedicaidInfo/_ViewMedicaidInfo.cshtml", theModel.mediciad);
        }

        // Method for Get Empty Medicare Info Partial View
        //Added by Sai Jaswanth
        public ActionResult GetMedicareInformationPartial(MedicareInfo model, string ActionType)
        {
            List<MedicareInfo> MdeicareEmptyModel = new List<MedicareInfo>();
            if (ActionType == Action2)
            {
                MdeicareEmptyModel.Add(model);
            }
            if (ActionType == Action1 || ActionType == Action3)
            {
                tempModel = _LicensesCode.GetAllLicensesCode();
                MdeicareEmptyModel = tempModel.medicare;
            }
            if (ActionType == Action3)
            {
                return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Licenses/MedicareInfo/_ViewMedicareInfo.cshtml", MdeicareEmptyModel);
            }
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Licenses/MedicareInfo/_AddEditMedicareInformation.cshtml", MdeicareEmptyModel);
        }

        // Method for Add Medicare Info
        //Added by Sai Jaswanth
        [HttpPost]
        public ActionResult AddMedicareInfo(MedicareInfo MedicareInfoData)
        {
            LicenseMainViewModel theModel = new LicenseMainViewModel();
            theModel = _LicensesCode.GetAllLicensesCode();
            theModel.medicare.Add(MedicareInfoData);
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Licenses/MedicareInfo/_ViewMedicareInfo.cshtml", theModel.medicare);
        }

        // Method for Get Empty Other Identification Partial View
        //Added by Sai Jaswanth
        public ActionResult GetOtherIdentificationPartial(OtherIdentification model, string ActionType)
        {
            List<OtherIdentification> OtherIdentificationEmptyModel = new List<OtherIdentification>();
            if (ActionType == Action2)
            {
                OtherIdentificationEmptyModel.Add(model);
            }
            if (ActionType == Action1 || ActionType == Action3)
            {
                tempModel = _LicensesCode.GetAllLicensesCode();
                OtherIdentificationEmptyModel = tempModel.otherIdentification;
            }
            if (ActionType == Action3)
            {
                return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Licenses/OtherIdentification/_ViewOtherIdentificationNumbers.cshtml", OtherIdentificationEmptyModel);
            }
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Licenses/OtherIdentification/_AddEditOtherIdentification.cshtml", OtherIdentificationEmptyModel);
        }

        // Method for Add Other Identification Info
        //Added by Sai Jaswanth
        [HttpPost]
        public ActionResult AddOtherIdentificationInfo(OtherIdentification OtherIdentificationData)
        {
            LicenseMainViewModel theModel = new LicenseMainViewModel();
            theModel = _LicensesCode.GetAllLicensesCode();
            theModel.otherIdentification.Add(OtherIdentificationData);
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Licenses/OtherIdentification/_ViewOtherIdentificationNumbers.cshtml", theModel.otherIdentification);
        }

        // Method for Get Empty State License Partial View
        //Added by Sai Jaswanth
        public ActionResult GetStateLicensePartial(StateLicense model, string ActionType)
        {
            List<StateLicense> StateLicenseEmptyModel = new List<StateLicense>();
            if (ActionType == Action2)
            {
                StateLicenseEmptyModel.Add(model);
            }
            if (ActionType == Action1 || ActionType == Action3)
            {
                tempModel = _LicensesCode.GetAllLicensesCode();
                StateLicenseEmptyModel = tempModel.stateLicense;
            }
            if (ActionType == Action3)
            {
                return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Licenses/StateLicense/_ViewStateLicense.cshtml", StateLicenseEmptyModel);
            }
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Licenses/StateLicense/_AddEditStateLicense.cshtml", StateLicenseEmptyModel);
        }

        // Method for Add State License Info
        //Added by Sai Jaswanth
        [HttpPost]
        public ActionResult AddStateLicenseInfo(StateLicense StateLicenseData)
        {
            LicenseMainViewModel theModel = new LicenseMainViewModel();
            theModel = _LicensesCode.GetAllLicensesCode();
            theModel.stateLicense.Add(StateLicenseData);
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Licenses/StateLicense/_StateLicenseViewMore.cshtml", theModel.stateLicense);
        }


        //----------------- GET ADD EDIT STATE LICENSE --------------------------------
        public ActionResult GetAddEditStateLicense(StateLicense model, string type)
        {
            List<StateLicense> _StateLicense = new List<StateLicense>();
            if (type.ToString() == "ViewMore")
            {
                tempModel = _LicensesCode.GetAllLicensesCode();
                _StateLicense = tempModel.stateLicense.OrderBy(i => i.StateLicenseID).Skip(2).ToList();
                return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Licenses/StateLicense/_StateLicenseViewMore.cshtml", _StateLicense);
            }
            if (type.ToString() == "View")
            {
                tempModel = _LicensesCode.GetAllLicensesCode();
                ViewBag.stateLicenseCount = tempModel.stateLicense.Count();
                return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Licenses/StateLicense/_ViewStateLicense.cshtml", tempModel.stateLicense);
            }
            if (type.ToString() == "Add")
            {
                _StateLicense.Add(model);
            }
            else
            {
                tempModel = _LicensesCode.GetAllLicensesCode();
                _StateLicense = tempModel.stateLicense;
            }
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Licenses/StateLicense/_AddEditStateLicense.cshtml", _StateLicense);
        }



        // Method for Get Empty Federal DEA Partial View
        //Added by Sai Jaswanth
        public ActionResult GetFederalDEAPartial(FederalDea model, string ActionType)
        {
            List<FederalDea> FederalDEAEmptyModel = new List<FederalDea>();
            if (ActionType == Action2)
            {
                FederalDEAEmptyModel.Add(model);
            }
            if (ActionType == Action1 || ActionType == Action3)
            {
                tempModel = _LicensesCode.GetAllLicensesCode();
                FederalDEAEmptyModel = tempModel.federalDEA;
            }
            if (ActionType == Action3)
            {
                return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Licenses/FederalDea/_ViewFederalDEA.cshtml", FederalDEAEmptyModel);
            }
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Licenses/FederalDea/_AddEditFederalDEAInfo.cshtml", FederalDEAEmptyModel);
            //  FederalDea FederalDeaEmptyModel = new FederalDea();
            //  return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Licenses/FederalDea/_AddEditFederalDEAInfo.cshtml", FederalDeaEmptyModel);
        }

        // Method for Add Federal DEA Info
        //Added by Sai Jaswanth
        [HttpPost]
        public ActionResult AddFederalDEAInfo(FederalDea FederalDeaData)
        {
            LicenseMainViewModel theModel = new LicenseMainViewModel();
            theModel = _LicensesCode.GetAllLicensesCode();
            theModel.federalDEA.Add(FederalDeaData);
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Licenses/FederalDea/_ViewFederalDEA.cshtml", theModel.federalDEA);
        }


        //----------------- GET ADD EDIT FEDERAL DEA --------------------------------
        public ActionResult GetAddEditFederalDEA(FederalDea model, string type)
        {
            List<FederalDea> _FederalDEA = new List<FederalDea>();
            if (type.ToString() == "ViewMore")
            {
                tempModel = _LicensesCode.GetAllLicensesCode();
                _FederalDEA = tempModel.federalDEA.OrderBy(i => i.FederalDeaID).Skip(2).ToList();
                return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Licenses/FederalDea/_FederalDEAViewMore.cshtml", _FederalDEA);
            }
            if (type.ToString() == "View")
            {
                tempModel = _LicensesCode.GetAllLicensesCode();
                ViewBag.FederalDeaCount = tempModel.federalDEA.Count();
                return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Licenses/FederalDea/_ViewFederalDEA.cshtml", tempModel.federalDEA);
            }
            if (type.ToString() == "Add")
            {
                _FederalDEA.Add(model);
            }
            else
            {
                tempModel = _LicensesCode.GetAllLicensesCode();
                _FederalDEA = tempModel.federalDEA;
            }
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Licenses/FederalDea/_AddEditFederalDEAInfo.cshtml", _FederalDEA);
        }

        //---------method to get the state license history data----------
        public ActionResult getStateHistory(int value)
        {
            List<StateLicense> GroupHistoryModel = new List<StateLicense>();
            LicenseMainViewModel LicenseModel = new LicenseMainViewModel();
            LicenseModel = _LicensesCode.GetAllLicensesCode();
            foreach (StateLicense groupmodel in LicenseModel.stateLicense)
            {
                if (value == groupmodel.StateLicenseID)
                {
                    GroupHistoryModel.Add(groupmodel);
                }
            }
            return PartialView("/Areas/CredAxis/Views/ProviderProfile/Tabs/Licenses/StateLicense/_HistoryStateLicense.cshtml", GroupHistoryModel);
        }
        //---------method to get the state license history data----------
        public ActionResult getFederalDEAHistory(int value)
        {
            List<FederalDea> GroupHistoryModel = new List<FederalDea>();
            LicenseMainViewModel LicenseModel = new LicenseMainViewModel();
            LicenseModel = _LicensesCode.GetAllLicensesCode();
            foreach (FederalDea groupmodel in LicenseModel.federalDEA)
            {
                if (value == groupmodel.FederalDeaID)
                {
                    GroupHistoryModel.Add(groupmodel);
                }
            }
            return PartialView("/Areas/CredAxis/Views/ProviderProfile/Tabs/Licenses/FederalDea/_HistoryFederalDEA.cshtml", GroupHistoryModel);
        }
        //---------method to get the state license history data----------
        public ActionResult getMedicaidHisotry(int value)
        {
            List<MedicaidInfo> GroupHistoryModel = new List<MedicaidInfo>();
            LicenseMainViewModel LicenseModel = new LicenseMainViewModel();
            LicenseModel = _LicensesCode.GetAllLicensesCode();
            foreach (MedicaidInfo groupmodel in LicenseModel.mediciad)
            {
                if (value == groupmodel.MedicaidInfoID)
                {
                    GroupHistoryModel.Add(groupmodel);
                }
            }
            return PartialView("/Areas/CredAxis/Views/ProviderProfile/Tabs/Licenses/MedicaidInfo/_HistoryMedicaidInfo.cshtml", GroupHistoryModel);
        }
        //---------method to get the state license history data----------
        public ActionResult getMedicareHistory(int value)
        {
            List<MedicareInfo> GroupHistoryModel = new List<MedicareInfo>();
            LicenseMainViewModel LicenseModel = new LicenseMainViewModel();
            LicenseModel = _LicensesCode.GetAllLicensesCode();
            foreach (MedicareInfo groupmodel in LicenseModel.medicare)
            {
                if (value == groupmodel.MedicareInfoID)
                {
                    GroupHistoryModel.Add(groupmodel);
                }
            }
            return PartialView("/Areas/CredAxis/Views/ProviderProfile/Tabs/Licenses/MedicareInfo/_HistoryMedicareInfo.cshtml", GroupHistoryModel);
        }
        //---------method to get the state license history data----------
        public ActionResult getOtherIdentificationHistory(int value)
        {
            List<OtherIdentification> GroupHistoryModel = new List<OtherIdentification>();
            LicenseMainViewModel LicenseModel = new LicenseMainViewModel();
            LicenseModel = _LicensesCode.GetAllLicensesCode();
            foreach (OtherIdentification groupmodel in LicenseModel.otherIdentification)
            {
                if (value == groupmodel.OtherIdentificationID)
                {
                    GroupHistoryModel.Add(groupmodel);
                }
            }
            return PartialView("/Areas/CredAxis/Views/ProviderProfile/Tabs/Licenses/OtherIdentification/_HistoryOtherIdentification.cshtml", GroupHistoryModel);
        }
        //---------method to get the state license history data----------
        public ActionResult getCDSHistory(int value)
        {
            List<CdsInfo> GroupHistoryModel = new List<CdsInfo>();
            LicenseMainViewModel LicenseModel = new LicenseMainViewModel();
            LicenseModel = _LicensesCode.GetAllLicensesCode();
            foreach (CdsInfo groupmodel in LicenseModel.CDS)
            {
                if (value == groupmodel.CdsInfoID)
                {
                    GroupHistoryModel.Add(groupmodel);
                }
            }
            return PartialView("/Areas/CredAxis/Views/ProviderProfile/Tabs/Licenses/CDSInfo/_HistoryCDSInfo.cshtml", GroupHistoryModel);
        }

        public ActionResult removeStateInfo(int value)
        {
            List<StateLicense> StateLicenseHistoryModel = new List<StateLicense>();
            LicenseMainViewModel LicenseModel = new LicenseMainViewModel();
            LicenseModel = _LicensesCode.GetAllLicensesCode();
            StateLicenseHistoryModel = LicenseModel.stateLicense.Where(i => i.StateLicenseID != value).ToList();
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Licenses/StateLicense/_ViewStateLicense.cshtml", StateLicenseHistoryModel);
        }

        public ActionResult removeFederalDEAInfo(int value)
        {
            List<FederalDea> FederalDEAHistoryModel = new List<FederalDea>();
            LicenseMainViewModel FederalModel = new LicenseMainViewModel();
            FederalModel = _LicensesCode.GetAllLicensesCode();
            FederalDEAHistoryModel = FederalModel.federalDEA.Where(i => i.FederalDeaID != value).ToList();
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Licenses/FederalDea/_ViewFederalDEA.cshtml", FederalDEAHistoryModel);
        }

        public ActionResult removeMedicareInfo(int value)
        {
            List<MedicareInfo> MedicareHistoryModel = new List<MedicareInfo>();
            LicenseMainViewModel MedicareModel = new LicenseMainViewModel();
            MedicareModel = _LicensesCode.GetAllLicensesCode();
            MedicareHistoryModel = MedicareModel.medicare.Where(i => i.MedicareInfoID != value).ToList();
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Licenses/MedicareInfo/_ViewMedicareInfo.cshtml", MedicareHistoryModel);
        }

        public ActionResult removeMedicaidInfo(int value)
        {
            List<MedicaidInfo> MedicaidHistoryModel = new List<MedicaidInfo>();
            LicenseMainViewModel MedicaidModel = new LicenseMainViewModel();
            MedicaidModel = _LicensesCode.GetAllLicensesCode();
            MedicaidHistoryModel = MedicaidModel.mediciad.Where(i => i.MedicaidInfoID != value).ToList();
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Licenses/MedicaidInfo/_ViewMedicaidInfo.cshtml", MedicaidHistoryModel);
        }

        public ActionResult removeCDSInfo(int value)
        {
            List<CdsInfo> CDSHistoryModel = new List<CdsInfo>();
            LicenseMainViewModel CDSModel = new LicenseMainViewModel();
            CDSModel = _LicensesCode.GetAllLicensesCode();
            CDSHistoryModel = CDSModel.CDS.Where(i => i.CdsInfoID != value).ToList();
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Licenses/CDSInfo/_ViewCDSInfo.cshtml", CDSHistoryModel);
        }
    }
}