using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.CredAxis.Models.DemographisViewModels
{
    public class DemographicsMainViewModel
    {
        public DemographicsMainViewModel()
        {
            BirthInformations = new BirthInformationViewModel();
            CitizenshipInformations = new CitizenshipInformationViewModel();
            ContactInformations = new ContactInformationViewModel();
            HomeAddresses = new List<HomeAddressViewModel>();
            LanguagesKnown = new LanguagesKnownViewModel();
            PersonalDetails = new PersonalDetailsViewModel();
            PersonalIdentifications = new PersonalIdentificationViewModel();
            OtherlegalName = new List<OtherLegalNameViewModel>();
        }
        public BirthInformationViewModel BirthInformations { get; set; }
        public CitizenshipInformationViewModel CitizenshipInformations { get; set; }
        public ContactInformationViewModel ContactInformations { get; set; }
        public List<HomeAddressViewModel> HomeAddresses { get; set; }
        public LanguagesKnownViewModel LanguagesKnown { get; set; }
        public PersonalDetailsViewModel PersonalDetails { get; set; }
        public PersonalIdentificationViewModel PersonalIdentifications { get; set; }
        public List<OtherLegalNameViewModel> OtherlegalName { get; set; }
    }
}