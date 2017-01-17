using Newtonsoft.Json;
using PortalTemplate.Areas.CredAxis.Models.DemographisViewModels;
using PortalTemplate.Areas.CredAxis.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Hosting;

namespace PortalTemplate.Areas.CredAxis.Services
{
    public class DemographicCodeServices : IDemographicCodeServices
    {
        private DemographicsMainViewModel _DemographicsCode;

        public DemographicCodeServices()
        {
            _DemographicsCode = new DemographicsMainViewModel();
        }
        DemographicsMainViewModel DemographicDetails = new DemographicsMainViewModel();
        //getting demographics all partial views with data
        public DemographicsMainViewModel GetAllDemographicsCode()
        {
            try
            {
                string pathBirth, pathCitizen, pathContact, pathHome, pathLang, pathPersonal, pathIdentification, OtherlegalName;
                //path = Path.Combine(HttpRuntime.AppDomainAppPath, "~/Areas/CredAxis/Resources/CredAxisJson/PersonalDetails.json");
                pathBirth = HostingEnvironment.MapPath("~/Areas/CredAxis/Resources/CredAxisJson/Demographics/BirthInformation.json");

                pathCitizen = HostingEnvironment.MapPath("~/Areas/CredAxis/Resources/CredAxisJson/Demographics/CitizenshipInformation.json");

                pathContact = HostingEnvironment.MapPath("~/Areas/CredAxis/Resources/CredAxisJson/Demographics/ContactInformation.json");

                pathHome = HostingEnvironment.MapPath("~/Areas/CredAxis/Resources/CredAxisJson/Demographics/HomeAddress.json");

                pathLang = HostingEnvironment.MapPath("~/Areas/CredAxis/Resources/CredAxisJson/Demographics/LanguagesKnown.json");

                pathPersonal = HostingEnvironment.MapPath("~/Areas/CredAxis/Resources/CredAxisJson/Demographics/PersonalDetails.json");

                pathIdentification = HostingEnvironment.MapPath("~/Areas/CredAxis/Resources/CredAxisJson/Demographics/PersonalIdentification.json");

                OtherlegalName = HostingEnvironment.MapPath("~/Areas/CredAxis/Resources/CredAxisJson/Demographics/OtherLegalName.json");

                using (System.IO.TextReader reader = System.IO.File.OpenText(pathBirth))
                {
                    string text = reader.ReadToEnd();
                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    DemographicDetails.BirthInformations = serial.Deserialize<BirthInformationViewModel>(text);
                }

                using (System.IO.TextReader reader = System.IO.File.OpenText(pathCitizen))
                {
                    string text = reader.ReadToEnd();
                    JavaScriptSerializer serial = new JavaScriptSerializer();                  
                    DemographicDetails.CitizenshipInformations = serial.Deserialize<CitizenshipInformationViewModel>(text);
                }

                using (System.IO.TextReader reader = System.IO.File.OpenText(pathContact))
                {
                    string text = reader.ReadToEnd();
                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    DemographicDetails.ContactInformations = serial.Deserialize<ContactInformationViewModel>(text);
                }

                using (System.IO.TextReader reader = System.IO.File.OpenText(pathHome))
                {
                    string text = reader.ReadToEnd();
                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    List<HomeAddressViewModel> HomeData = new List<HomeAddressViewModel>();
                    DemographicDetails.HomeAddresses = serial.Deserialize<List<HomeAddressViewModel>>(text);
                    //for (int i = 0; i < HomeData.Count(); i++)
                    //{
                    //   // if (i < 2)
                    //        DemographicDetails.HomeAddresses.Add(HomeData[i]);
                    //}
                }

                using (System.IO.TextReader reader = System.IO.File.OpenText(pathLang))
                {
                    string text = reader.ReadToEnd();
                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    DemographicDetails.LanguagesKnown = serial.Deserialize<LanguagesKnownViewModel>(text);
                }

                using (System.IO.TextReader reader = System.IO.File.OpenText(pathPersonal))
                {
                    string text = reader.ReadToEnd();
                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    DemographicDetails.PersonalDetails = serial.Deserialize<PersonalDetailsViewModel>(text);

                }

                using (System.IO.TextReader reader = System.IO.File.OpenText(pathIdentification))
                {
                    string text = reader.ReadToEnd();
                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    DemographicDetails.PersonalIdentifications = serial.Deserialize<PersonalIdentificationViewModel>(text);
                }
                using (System.IO.TextReader reader = System.IO.File.OpenText(OtherlegalName))
                {
                    string text = reader.ReadToEnd();
                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    DemographicDetails.OtherlegalName = serial.Deserialize<List<OtherLegalNameViewModel>>(text);
                }

                return DemographicDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /*AddEdit for Demographics personal detail 
         @input params - profile ID, personal detail view model object
         */
        PersonalDetailsViewModel AddEditPersonalDetails(PersonalDetailsViewModel personalDetail, int ProfileId)
        {
            return null;
        }

        /*
         * Add Edit for Birth Information
         * @input params- birth information view model object
         * 
         */
        BirthInformationViewModel AddEditBirthInformation(BirthInformationViewModel birthInformation, int ProfileId)
        {
            return null;
        }
        public DemographicsMainViewModel AddEditDemographicsCode(DemographicsMainViewModel demographicsCode)
        {
            return null;
        }


        public PersonalDetailsViewModel GetAllPersonalDetails(int id)
        {
            var pathPersonal = HostingEnvironment.MapPath("~/Areas/CredAxis/Resources/CredAxisJson/Demographics/PersonalDetails.json");

            List<PersonalDetailsViewModel> PersonalData = new List<PersonalDetailsViewModel>();
            using (System.IO.TextReader reader = System.IO.File.OpenText(pathPersonal))
            {
                string text = reader.ReadToEnd();
                JavaScriptSerializer serial = new JavaScriptSerializer();

                PersonalData = serial.Deserialize<List<PersonalDetailsViewModel>>(text);

            }
            return PersonalData.Where(i => i.PersonalDetailID == id).FirstOrDefault();
        }


        public List<PersonalDetailsViewModel> GetAllPersonalDetailsHistory()
        {
            string pathPersonalHistory;
            List<PersonalDetailsViewModel> PersonalDetailHistory = new List<PersonalDetailsViewModel>();
            pathPersonalHistory = HostingEnvironment.MapPath("~/Areas/CredAxis/Resources/CredAxisJson/Demographics/PersonalDetailsHistory.json");
            using (System.IO.TextReader reader = System.IO.File.OpenText(pathPersonalHistory))
            {
                string text = reader.ReadToEnd();
                JavaScriptSerializer serial = new JavaScriptSerializer();
                PersonalDetailHistory = serial.Deserialize<List<PersonalDetailsViewModel>>(text);
            }
            return PersonalDetailHistory;
        }


        public List<OtherLegalNameViewModel> GetAllOtherLegalNameHistory(int id)
        {
            var pathPersonal = HostingEnvironment.MapPath("~/Areas/CredAxis/Resources/CredAxisJson/Demographics/OtherLegalNamesHistory.json");

            List<OtherLegalNameViewModel> OtherLegalNames = new List<OtherLegalNameViewModel>();
            using (System.IO.TextReader reader = System.IO.File.OpenText(pathPersonal))
            {
                string text = reader.ReadToEnd();
                JavaScriptSerializer serial = new JavaScriptSerializer();
                OtherLegalNames = serial.Deserialize<List<OtherLegalNameViewModel>>(text);
            }
            return OtherLegalNames.Where(i => i.OtherLegalNameID == id).ToList();
        }


        public List<ContactInformationViewModel> GetAllContactInfoHistory()
        {
            string pathContactInfoHistory;
            List<ContactInformationViewModel> ContactInfoHistory = new List<ContactInformationViewModel>();
            pathContactInfoHistory = HostingEnvironment.MapPath("~/Areas/CredAxis/Resources/CredAxisJson/Demographics/ContactInfoHistory.json");
            using (System.IO.TextReader reader = System.IO.File.OpenText(pathContactInfoHistory))
            {
                string text = reader.ReadToEnd();
                JavaScriptSerializer serial = new JavaScriptSerializer();
                ContactInfoHistory = serial.Deserialize<List<ContactInformationViewModel>>(text);
            }
            return ContactInfoHistory;
        }


        public List<BirthInformationViewModel> GetBirthInfoHistory()
        {
            string pathBirthInfoHistory;
            List<BirthInformationViewModel> BirthInfoHistory = new List<BirthInformationViewModel>();
            pathBirthInfoHistory = HostingEnvironment.MapPath("~/Areas/CredAxis/Resources/CredAxisJson/Demographics/BirthInfoHistory.json");
            using (System.IO.TextReader reader = System.IO.File.OpenText(pathBirthInfoHistory))
            {
                string text = reader.ReadToEnd();
                JavaScriptSerializer serial = new JavaScriptSerializer();
                BirthInfoHistory = serial.Deserialize<List<BirthInformationViewModel>>(text);
            }
            return BirthInfoHistory;
        }


        public List<PersonalIdentificationViewModel> GetPersonalIdentificationHistory()
        {
            string pathPersonalIdentificationHistory;
            List<PersonalIdentificationViewModel> PersonalIdentificationHistory = new List<PersonalIdentificationViewModel>();
            pathPersonalIdentificationHistory = HostingEnvironment.MapPath("~/Areas/CredAxis/Resources/CredAxisJson/Demographics/PersonalIdentificationHistory.json");
            using (System.IO.TextReader reader = System.IO.File.OpenText(pathPersonalIdentificationHistory))
            {
                string text = reader.ReadToEnd();
                JavaScriptSerializer serial = new JavaScriptSerializer();
                PersonalIdentificationHistory = serial.Deserialize<List<PersonalIdentificationViewModel>>(text);
            }
            return PersonalIdentificationHistory;
        }


        public List<HomeAddressViewModel> GetHomeAddressHistory()
        {
            string pathHomeAddressHistory;
            List<HomeAddressViewModel> HomeAddressHistory = new List<HomeAddressViewModel>();
            pathHomeAddressHistory = HostingEnvironment.MapPath("~/Areas/CredAxis/Resources/CredAxisJson/Demographics/HomeAddress.json");
            using (System.IO.TextReader reader = System.IO.File.OpenText(pathHomeAddressHistory))
            {
                string text = reader.ReadToEnd();
                JavaScriptSerializer serial = new JavaScriptSerializer();
                HomeAddressHistory = serial.Deserialize<List<HomeAddressViewModel>>(text);
            }
            return HomeAddressHistory;
        }


        public List<CitizenshipInformationViewModel> GetCitizenshipInfoHistory()
        {
            string pathCitizenshipInfoHistory;
            List<CitizenshipInformationViewModel> CitizenshipInfoHistory = new List<CitizenshipInformationViewModel>();
            pathCitizenshipInfoHistory = HostingEnvironment.MapPath("~/Areas/CredAxis/Resources/CredAxisJson/Demographics/CitizenshipInfoHistory.json");
            using (System.IO.TextReader reader = System.IO.File.OpenText(pathCitizenshipInfoHistory))
            {
                string text = reader.ReadToEnd();
                JavaScriptSerializer serial = new JavaScriptSerializer();
                CitizenshipInfoHistory = serial.Deserialize<List<CitizenshipInformationViewModel>>(text);
            }
            return CitizenshipInfoHistory;
        }
    }
}