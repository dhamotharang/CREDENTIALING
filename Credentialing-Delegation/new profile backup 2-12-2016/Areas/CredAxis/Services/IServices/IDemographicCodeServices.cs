using PortalTemplate.Areas.CredAxis.Models.DemographisViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CredAxis.Services.IServices
{
    public interface IDemographicCodeServices
    {
        DemographicsMainViewModel GetAllDemographicsCode();
        DemographicsMainViewModel AddEditDemographicsCode(DemographicsMainViewModel demographicsCode);
        PersonalDetailsViewModel GetAllPersonalDetails(int id);
        List<PersonalDetailsViewModel> GetAllPersonalDetailsHistory();
        List<OtherLegalNameViewModel> GetAllOtherLegalNameHistory(int id);
        List<ContactInformationViewModel> GetAllContactInfoHistory();
        List<BirthInformationViewModel> GetBirthInfoHistory();
        List<PersonalIdentificationViewModel> GetPersonalIdentificationHistory();
        List<HomeAddressViewModel> GetHomeAddressHistory();
        List<CitizenshipInformationViewModel> GetCitizenshipInfoHistory();
    }
}
