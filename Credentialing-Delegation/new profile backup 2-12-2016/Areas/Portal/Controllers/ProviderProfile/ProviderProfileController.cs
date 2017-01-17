using PortalTemplate.Areas.Portal.IServices;
using PortalTemplate.Areas.Portal.Models.ProviderProfile;
using PortalTemplate.Areas.Portal.Services.ProviderProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Areas.Portal.Controllers.ProviderProfile
{
    public class ProviderProfileController : Controller
    {

        IProviderProfileService _providerprofileservice = null;
        ProviderProfileViewModel ProviderProfile = null;

        public ProviderProfileController()
        {
            _providerprofileservice = new ProviderProfileService();
            ProviderProfile = new ProviderProfileViewModel();

        }
        //
        // GET: /Portal/ProviderProfile/
        public ActionResult Index(int profileId)
        { 
           var data = _providerprofileservice.GetProfile(1);
            return PartialView("~/Areas/Portal/Views/ProviderProfile/_ProviderProfile.cshtml", data);
        }
        #region PersonalInformation
        public ActionResult GetPersonalInfoEditPartial(int ProfileId, string type)
        {

            PersonalInformationViewModel _PersonalInformationModel = new PersonalInformationViewModel();
            if (type.ToLower() != "add")
            {
                var data = _providerprofileservice.GetProfile(ProfileId);
                _PersonalInformationModel = data.PersonalInformation;
            }
           
            return PartialView("~/Areas/Portal/Views/ProviderProfile/PersonalInformation/_EditPersonalInformation.cshtml", _PersonalInformationModel);
        }
        public ActionResult CancelPersonalInfoEdit(int profileId)
        {
            var data = _providerprofileservice.GetProfile(profileId);
            return PartialView("~/Areas/Portal/Views/ProviderProfile/PersonalInformation/_ViewPersonalInformation.cshtml", data.PersonalInformation);
        }
        [HttpPost]
        public ActionResult SavePersonalInfo(PersonalInformationViewModel data)
        {
            var data1 = _providerprofileservice.GetProfile(1);
            if (ModelState.IsValid)
            {
                return PartialView("~/Areas/Portal/Views/ProviderProfile/PersonalInformation/_ViewPersonalInformation.cshtml", data1.PersonalInformation);
            }
            return PartialView("~/Areas/Portal/Views/ProviderProfile/PersonalInformation/_EditPersonalInformation.cshtml", data1.PersonalInformation);
        }
        #endregion
        #region ContactInformation
        public ActionResult GetContactInfoEditPartial(int ProfileId, string type)
        {

            ContactInformationViewModal _ContactInformationModel = new ContactInformationViewModal();
            if (type.ToLower() != "add")
            {
                var data = _providerprofileservice.GetProfile(ProfileId);
                _ContactInformationModel = data.ContactInformation;
            }

            return PartialView("~/Areas/Portal/Views/ProviderProfile/ContactInformation/_EditContactInformation.cshtml", _ContactInformationModel);
        }
        public ActionResult CancelContactInfoEdit(int profileId)
        {
            var data = _providerprofileservice.GetProfile(profileId);
            return PartialView("~/Areas/Portal/Views/ProviderProfile/ContactInformation/_ViewContactInformation.cshtml", data.ContactInformation);
        }
        [HttpPost]
        public ActionResult SaveContactInfo(ContactInformationViewModal data)
        {
            var data1 = _providerprofileservice.GetProfile(1);
            if (ModelState.IsValid)
            {
                return PartialView("~/Areas/Portal/Views/ProviderProfile/ContactInformation/_ViewContactInformation.cshtml", data1.ContactInformation);
            }
            return PartialView("~/Areas/Portal/Views/ProviderProfile/ContactInformation/_EditContactInformation.cshtml", data1.ContactInformation);
        }
        #endregion

        #region WorkInformation
        public ActionResult GetWorkInfoEditPartial(int ProfileId, string type)
        {

            List<WorkInformationViewModel> _WorkInformationModel = new List<WorkInformationViewModel>();
            if (type.ToLower() != "add")
            {
                var data = _providerprofileservice.GetProfile(ProfileId);
                _WorkInformationModel = data.WorkInformation;
            }

            return PartialView("~/Areas/Portal/Views/ProviderProfile/WorkInformation/_EditWorkInformation.cshtml", _WorkInformationModel);
        }
        public ActionResult CancelWorkInfoEdit(int profileId)
        {
            var data = _providerprofileservice.GetProfile(profileId);
            return PartialView("~/Areas/Portal/Views/ProviderProfile/WorkInformation/_ViewWorkInformation.cshtml", data.WorkInformation);
        }
        [HttpPost]
        public ActionResult SaveWorkInfo(List<WorkInformationViewModel> data)
        {
            var data1 = _providerprofileservice.GetProfile(1);
            if (ModelState.IsValid)
            {
                return PartialView("~/Areas/Portal/Views/ProviderProfile/WorkInformation/_ViewWorkInformation.cshtml", data1.WorkInformation);
            }
            return PartialView("~/Areas/Portal/Views/ProviderProfile/WorkInformation/_EditWorkInformation.cshtml", data1.WorkInformation);
        }
        #endregion

        #region ContractInformation
        public ActionResult GetContractInfoEditPartial(int ProfileId, string type)
        {

            ContractInformationViewModel _ConractInformationModel = new ContractInformationViewModel();
            if (type.ToLower() != "add")
            {
                var data = _providerprofileservice.GetProfile(ProfileId);
                _ConractInformationModel = data.ContractInformation;
            }

            return PartialView("~/Areas/Portal/Views/ProviderProfile/ContractInformation/_EditContractInformation.cshtml", _ConractInformationModel);
        }
        public ActionResult CancelContractInfoEdit(int profileId)
        {
            var data = _providerprofileservice.GetProfile(profileId);
            return PartialView("~/Areas/Portal/Views/ProviderProfile/ContractInformation/_ViewContractInformation.cshtml", data.ContractInformation);
        }
        [HttpPost]
        public ActionResult SaveContractInfo(ContractInformationViewModel data)
        {
            if (ModelState.IsValid)
            {
                return PartialView("~/Areas/Portal/Views/ProviderProfile/ContractInformation/_ViewContractInformation.cshtml", data);
            }
            return PartialView("~/Areas/Portal/Views/ProviderProfile/ContractInformation/_EditContractInformation.cshtml", data);
        }
        #endregion

    }
}