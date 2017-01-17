
using PortalTemplate.Areas.MH.Models.ViewModels.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace PortalTemplate.Areas.MH.IServices
{
    public interface IMasterDataService
    {
      string GetAllPlans(string searchTerm);
      string GetAllInsuranceCompanies(string searchTerm);
      string GetAllStates(string searchTerm);
      string GetAllCities(string searchTerm);
      string GetAllCounties(string searchTerm);
      string GetAllCountries(string searchTerm);
      string GetAllPatientRelationships(string searchTerm);

      string GetAllCitiesByStateCode();
      string GetAllCountiesByStateCode();
      string GetAllSources();
      string GetSourceBySourceCode();
      string GetAllBankAccountTypes();
      string GetAllBanks();
      string GetAllDeductionsTypes();
      string GetAllDiseaseCategories();
      string GetAllDiseases();
      string GetAllDocumentCategories();
      string GetAllEducationCourses();
      string GetAllEducationInstitutions();
      string GetAllEmploymentTypes();
      string GetAllEmployers();
      string GetAllIdentificationTypes();
      string GetAllIncomeSources();
      string GetAllLanguages();
      string GetAllLobs();
      string GetLobDetailsByLobId();
      string GetAllInsuranceCompanies();
      string GetAllPlansByInsuranceCompanyCode();
      string GetAllPbps();
      string GetPlanDetailsByPbpId();
      string GetAllNoteCategories();
      string GetAllAttachmentCategory();
      string GetAllEthnicities();
      string GetAllRaces();
      string GetAllReligions();
      string GetAllPremiumTypes();
      string GetAllIpas();
      string GetAllFacities();
      string GetFacilityDetailsByFacilityCode();
    }
}