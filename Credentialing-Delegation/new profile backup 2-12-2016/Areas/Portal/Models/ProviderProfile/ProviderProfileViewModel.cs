using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Portal.Models.ProviderProfile
{
    public class ProviderProfileViewModel
    {
        public ProviderProfileViewModel()
        {
            PersonalInformation = new PersonalInformationViewModel();
            ContactInformation = new ContactInformationViewModal();
            WorkInformation = new List<WorkInformationViewModel>();
            ContractInformation = new ContractInformationViewModel();
        }
        public int ProfileId { get; set; }
        public PersonalInformationViewModel PersonalInformation { get; set; }
        public ContactInformationViewModal ContactInformation { get; set; }
        public List<WorkInformationViewModel> WorkInformation { get; set; }
        public ContractInformationViewModel ContractInformation { get; set; }
    }
}