using PortalTemplate.Areas.CredAxis.Models.DemographisViewModels;
using PortalTemplate.Areas.CredAxis.Services;
using PortalTemplate.Areas.CredAxis.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CredAxis.Controllers.ProviderProfileController
{
    public class DemographicsController : Controller
    {
        private readonly IDemographicCodeServices _DemographicsCode = null;

        public DemographicsController()
        {
            _DemographicsCode = new DemographicCodeServices();
        }
        DemographicsMainViewModel theModel = new DemographicsMainViewModel();
        //
        // GET: /CredAxis/Demographics/
        public ActionResult Index(string ModeRequested)
        {
            theModel = _DemographicsCode.GetAllDemographicsCode();
            if (ModeRequested == "EDIT")
            {
                return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Demographics/_DemographicsEditMode.cshtml", theModel);
            }
            ViewBag.HomeAddressCount = theModel.HomeAddresses.Count();
            if (theModel.HomeAddresses.Count() > 2)
            {
                theModel.HomeAddresses = theModel.HomeAddresses.OrderBy(i => i.HomeAddressId).Take(2).ToList();
            }
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Demographics/_Demographic.cshtml", theModel);
        }
        //public ActionResult IndexEditMode()
        //{
        //    theModel = _DemographicsCode.GetAllDemographicsCode();            
        //}
        /// <summary>
        /// Method to return Add Partial for Professional Affiliation
        /// </summary>
        /// <param name="professionalAffiliationMainModel"></param>
        /// <returns></returns>
        //   [HttpPost]
        public ActionResult GetPersonalDetailsEditPartial(PersonalDetailsViewModel model, string type)
        {
            PersonalDetailsViewModel _PersonalDetailsModel = new PersonalDetailsViewModel();
            if (type.ToString() == "Add")
            {
                _PersonalDetailsModel = model;
            }
            else
            {
                theModel = _DemographicsCode.GetAllDemographicsCode();
                _PersonalDetailsModel = theModel.PersonalDetails;
            }
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Demographics/PersonalDetails/_AddEditPersonalDetails.cshtml", _PersonalDetailsModel);
        }
        public ActionResult CanceltPersonalDetailsEdit()
        {
            theModel = _DemographicsCode.GetAllDemographicsCode();
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Demographics/PersonalDetails/_PersonalDetailView.cshtml", theModel.PersonalDetails);
        }
        public ActionResult GetPersonalDetailsHistoryPartial()
        {
            List<PersonalDetailsViewModel> _PersonalDetailsHistoryModel = new List<PersonalDetailsViewModel>();
            _PersonalDetailsHistoryModel = _DemographicsCode.GetAllPersonalDetailsHistory();
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Demographics/PersonalDetails/_PersonalDetailsHistory.cshtml", _PersonalDetailsHistoryModel);
        }
        [HttpPost]
        public ActionResult AddPersonalDetails(PersonalDetailsViewModel data)
        {
            //DemographicsMainViewModel theModel = new DemographicsMainViewModel();
            //theModel = _DemographicsCode.GetAllDemographicsCode();
            //theModel.PersonalDetails= data;
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Demographics/PersonalDetails/_PersonalDetailView.cshtml", data);
        }
        /// <summary>
        /// Contact Info
        /// </summary>
        /// <param name="model"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult GetContactInfoEditPartial(ContactInformationViewModel model, string type)
        {

            ContactInformationViewModel _ContactInformationModel = new ContactInformationViewModel();
            if (type.ToString() == "Add")
            {
                _ContactInformationModel = model;
            }
            else
            {
                theModel = _DemographicsCode.GetAllDemographicsCode();
                _ContactInformationModel = theModel.ContactInformations;
            }
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Demographics/ContactInformation/_AddEditContactInformation.cshtml", _ContactInformationModel);
        }
        public ActionResult CancelContactInfoEdit()
        {
            theModel = _DemographicsCode.GetAllDemographicsCode();
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Demographics/ContactInformation/_ContactInformationView.cshtml", theModel.ContactInformations);
        }
        public ActionResult GetContactInfoHistoryPartial()
        {
            List<ContactInformationViewModel> _ContactInfoHistoryModel = new List<ContactInformationViewModel>();
            _ContactInfoHistoryModel = _DemographicsCode.GetAllContactInfoHistory();
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Demographics/ContactInformation/_ContactInformationHistory.cshtml", _ContactInfoHistoryModel);
        }
        [HttpPost]
        public ActionResult AddContactInfo(ContactInformationViewModel data)
        {
            //DemographicsMainViewModel theModel = new DemographicsMainViewModel();
            //theModel = _DemographicsCode.GetAllDemographicsCode();
            //theModel.PersonalDetails= data;
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Demographics/ContactInformation/_ContactInformationView.cshtml", data);
        }

        /// <summary>
        /// Birth Info
        /// </summary>
        /// <param name="model"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult GetBirthInfoEditPartial(BirthInformationViewModel model, string type)
        {

            BirthInformationViewModel _BirthInformationModel = new BirthInformationViewModel();
            if (type.ToString() == "Add")
            {
                _BirthInformationModel = model;
            }
            else
            {
                theModel = _DemographicsCode.GetAllDemographicsCode();
                _BirthInformationModel = theModel.BirthInformations;
            }
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Demographics/BirthInformation/_AddEditBirthInformation.cshtml", _BirthInformationModel);
        }
        public ActionResult CancelBirthInfoEdit()
        {
            theModel = _DemographicsCode.GetAllDemographicsCode();
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Demographics/BirthInformation/_BirthInformationView.cshtml", theModel.BirthInformations);
        }
        public ActionResult AddBirthInfo(BirthInformationViewModel data)
        {
            //DemographicsMainViewModel theModel = new DemographicsMainViewModel();
            //theModel = _DemographicsCode.GetAllDemographicsCode();
            //theModel.PersonalDetails= data;
            if (ModelState.IsValid)
            {
                return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Demographics/BirthInformation/_BirthInformationView.cshtml", data);
            }
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Demographics/BirthInformation/_AddEditBirthInformation.cshtml", data);
        }
        public ActionResult GetBirthInfoHistoryPartial()
        {
            List<BirthInformationViewModel> _BirthInfoHistoryModel = new List<BirthInformationViewModel>();
            _BirthInfoHistoryModel = _DemographicsCode.GetBirthInfoHistory();
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Demographics/BirthInformation/_BirthInformationHistory.cshtml", _BirthInfoHistoryModel);
        }

        [HttpPost]
        public JsonResult AddEditDemographicsCode(DemographicsMainViewModel demographicsCode)
        {
            if (ModelState.IsValid)
            {
                var AddEditDemographicsCode = _DemographicsCode.AddEditDemographicsCode(demographicsCode);
                return Json(new { Result = AddEditDemographicsCode, status = "done" });
            }

            return Json(new { status = "false" });
        }
        /// <summary>
        /// Other Legal Names
        /// </summary>
        /// <param name="model"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult GetAddEditOtherNames(OtherLegalNameViewModel model, string type)
        {
            List<OtherLegalNameViewModel> _OtherNamesModel = new List<OtherLegalNameViewModel>();
            //if (type.ToString() == "ViewMore")
            //{
            //    theModel = _DemographicsCode.GetAllDemographicsCode();
            //    _HomeAddressModel = theModel.HomeAddresses.OrderBy(i => i.HomeAddressId).Skip(2).ToList();
            //    return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Demographics/HomeAddress/_HomeAddressMoreView.cshtml", _HomeAddressModel);
            //}
            if (type.ToString() == "View")
            {
                theModel = _DemographicsCode.GetAllDemographicsCode();
                ViewBag.OtherNamesCount = theModel.OtherlegalName.Count();
                return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Demographics/OtherLegalName/_ViewOtherLegalName.cshtml", theModel.OtherlegalName);
            }
            if (type.ToString() == "Add")
            {
                _OtherNamesModel.Add(model);
            }
            else
            {
                theModel = _DemographicsCode.GetAllDemographicsCode();
                _OtherNamesModel = theModel.OtherlegalName;
            }
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Demographics/OtherLegalName/_AddEditOtherLegalName.cshtml", _OtherNamesModel);
        }
        public ActionResult GetOtherLegalNamesHistoryPartial(int id)
        {
            List<OtherLegalNameViewModel> _OtherLegalNameHistoryModel = new List<OtherLegalNameViewModel>();
            _OtherLegalNameHistoryModel = _DemographicsCode.GetAllOtherLegalNameHistory(id);
            ViewBag.OtherLegalNameHistoryDivId = id;
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Demographics/OtherLegalName/_OtherLegalNameHistory.cshtml", _OtherLegalNameHistoryModel);
        }
        /// <summary>
        /// Home Address
        /// </summary>
        /// <param name="model"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult GetAddEditHomeAddress(HomeAddressViewModel model, string type)
        {
            List<HomeAddressViewModel> _HomeAddressModel = new List<HomeAddressViewModel>();
            if (type.ToString() == "ViewMore")
            {
                theModel = _DemographicsCode.GetAllDemographicsCode();
                _HomeAddressModel = theModel.HomeAddresses.OrderBy(i => i.HomeAddressId).Skip(2).ToList();
                return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Demographics/HomeAddress/_HomeAddressMoreView.cshtml", _HomeAddressModel);
            }
            if (type.ToString() == "View")
            {
                theModel = _DemographicsCode.GetAllDemographicsCode();
                ViewBag.HomeAddressCount = theModel.HomeAddresses.Count();
                return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Demographics/HomeAddress/_HomeAddressView.cshtml", theModel.HomeAddresses);
            }
            if (type.ToString() == "Add")
            {
                _HomeAddressModel.Add(model);
            }
            else
            {
                theModel = _DemographicsCode.GetAllDemographicsCode();
                _HomeAddressModel = theModel.HomeAddresses;
            }
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Demographics/HomeAddress/_AddEditHomeAddress.cshtml", _HomeAddressModel);
        }        
        public ActionResult AddHomeAddress(List<HomeAddressViewModel> homeAddressViewModel)
        {
            List<HomeAddressViewModel> homeAddressList = new List<HomeAddressViewModel>();
            homeAddressList = _DemographicsCode.GetAllDemographicsCode().HomeAddresses; 
            if (homeAddressViewModel.Count() > 1 && homeAddressViewModel != null)
            { 
           
            }
            else
                homeAddressList.Add(homeAddressViewModel[0]);
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Demographics/HomeAddress/_HomeAddressView.cshtml", homeAddressList);
        }
        public ActionResult GetHomeAddressHistoryPartial()
        {
            List<HomeAddressViewModel> _HomeAddressHistoryModel = new List<HomeAddressViewModel>();
            _HomeAddressHistoryModel = _DemographicsCode.GetHomeAddressHistory();
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Demographics/HomeAddress/_HomeAddressHistory.cshtml", _HomeAddressHistoryModel);
        }
        /// <summary>
        /// Language Known
        /// </summary>
        /// <param name="ActionType"></param>
        /// <returns></returns>
        public ActionResult GetLanguageKnownDetailsEditPartial(string ActionType)
        {
            LanguagesKnownViewModel _LanguageKnownDetailsModel = new LanguagesKnownViewModel();
            theModel = _DemographicsCode.GetAllDemographicsCode();
            _LanguageKnownDetailsModel = theModel.LanguagesKnown;
            if (ActionType.ToString() == "Edit")
            {
                return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Demographics/LanguagesKnown/_AddEditLanguages.cshtml", _LanguageKnownDetailsModel);
            }
            else
            {
                return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Demographics/LanguagesKnown/_ViewLanguagesKnown.cshtml", _LanguageKnownDetailsModel);
            }
        }
        /// <summary>
        /// Personal Identification
        /// </summary>
        /// <param name="ActionType"></param>
        /// <returns></returns>
        public ActionResult GetPersonalIdentificationInfoEditPartial(string ActionType)
        {
            PersonalIdentificationViewModel _PersonalIdentificationInfoDetailsModel = new PersonalIdentificationViewModel();
            theModel = _DemographicsCode.GetAllDemographicsCode();
            _PersonalIdentificationInfoDetailsModel = theModel.PersonalIdentifications;
            if (ActionType.ToString() == "Edit")
            {
                return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Demographics/PersonalIdentification/_AddEditPersonalIdentification.cshtml", _PersonalIdentificationInfoDetailsModel);
            }
            else
            {

                return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Demographics/PersonalIdentification/_ViewPersonalIdentification.cshtml", _PersonalIdentificationInfoDetailsModel);
            }
        }
        public ActionResult GetPersonalIdentificationInfoHistoryPartial()
        {
            List<PersonalIdentificationViewModel> _PersonalIdentificationInfoHistoryModel = new List<PersonalIdentificationViewModel>();
            _PersonalIdentificationInfoHistoryModel = _DemographicsCode.GetPersonalIdentificationHistory();
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Demographics/PersonalIdentification/_HistoryPersonalIdentification.cshtml", _PersonalIdentificationInfoHistoryModel);
        }
        /// <summary>
        /// Citizenship Information
        /// </summary>
        /// <param name="ActionType"></param>
        /// <returns></returns>
        public ActionResult GetCitizenShipInfoEditPartial(string ActionType)
        {
            CitizenshipInformationViewModel _CitizenShipInfoDetailsModel = new CitizenshipInformationViewModel();
            theModel = _DemographicsCode.GetAllDemographicsCode();
            _CitizenShipInfoDetailsModel = theModel.CitizenshipInformations;
            if (ActionType.ToString() == "Edit")
            {
                return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Demographics/CetizienInformation/_AddEditCitizenInformation.cshtml", _CitizenShipInfoDetailsModel);
            }
            else
            {
                return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Demographics/CetizienInformation/_ViewCitizenInformation.cshtml", _CitizenShipInfoDetailsModel);
            }
        }
        public ActionResult GetCitizenShipInfoPartial()
        {
            List<CitizenshipInformationViewModel> _CitizenShipInfoHistoryModel = new List<CitizenshipInformationViewModel>();
            _CitizenShipInfoHistoryModel = _DemographicsCode.GetCitizenshipInfoHistory();
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Demographics/CetizienInformation/_HistoryCitizenInformation.cshtml", _CitizenShipInfoHistoryModel);
        }

        public ActionResult removehomeaddress(int value)
        {
            List<HomeAddressViewModel> homeAddressList = new List<HomeAddressViewModel>();
            theModel = _DemographicsCode.GetAllDemographicsCode();
            homeAddressList = theModel.HomeAddresses.Where(i => i.HomeAddressId != value).ToList();
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Demographics/HomeAddress/_HomeAddressView.cshtml", homeAddressList);
        } 
    }
}