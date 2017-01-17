using AHC.CD.Business.BusinessModels.PDFGenerator;
using AHC.CD.Business.DocumentWriter;
using AHC.CD.Data.Repository;
using AHC.CD.Data.Repository.Profiles;
using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using AHC.CD.Entities.MasterProfile;
using AHC.CD.Entities.MasterProfile.DisclosureQuestions;
using AHC.CD.Exceptions.PDFGenerator;
using AHC.CD.Resources.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.ComponentModel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using System.Configuration;
using AHC.CD.Entities.MasterProfile.BoardSpecialty;
using AHC.CD.Entities.MasterProfile.HospitalPrivilege;
using AHC.CD.Entities.ProviderInfo;
using System.Web.Mvc;
using AHC.CD.Entities.DocumentRepository;
using AHC.CD.Business.Credentialing.CnD;
using AHC.CD.Entities.MasterProfile.Demographics;

namespace AHC.CD.Business.PdfGeneration
{
    internal class PdfMappingManager : IPdfMappingManager
    {

        private IProfileRepository profileRepository = null;
        private IUnitOfWork uow = null;

        public PdfMappingManager(IUnitOfWork uow)
        {
            this.profileRepository = uow.GetProfileRepository();
            this.uow = uow;
        }

        string pname = null;
        int profileId;

        private PersonalDetail GetProviderData(int profileID)
        {
            var profileRepo = uow.GetGenericRepository<Profile>();
            var personalInfo = profileRepo.Find(p => p.ProfileID == profileID).PersonalDetail;

            return personalInfo;
        }

        private Profile GetProfileList(int profileID)
        {
            try
            {
                profileId = profileID;
                var includeProperties = new string[]
                {
                    //Specialty
                    "SpecialtyDetails.Specialty",
                    "SpecialtyDetails.SpecialtyBoardCertifiedDetail.SpecialtyBoard",
                    "SpecialtyDetails.SpecialtyBoardNotCertifiedDetail",
                    "PracticeInterest",

                    //hospital Privilege
                    "HospitalPrivilegeInformation.HospitalPrivilegeDetails.Hospital", 
                    "HospitalPrivilegeInformation.HospitalPrivilegeDetails.HospitalContactInfo", 
                    "HospitalPrivilegeInformation.HospitalPrivilegeDetails.HospitalContactPerson", 
                    "HospitalPrivilegeInformation.HospitalPrivilegeDetails.AdmittingPrivilege", 
                    "HospitalPrivilegeInformation.HospitalPrivilegeDetails.StaffCategory",

                    //Visa Detail
                    "VisaDetail.VisaInfo.VisaStatus", 
                    "VisaDetail.VisaInfo.VisaType", 

                    //Professional Reference
                    "ProfessionalReferenceInfos.ProviderType", 
                    "ProfessionalReferenceInfos.Specialty",

                    //Professional Liability
                    "ProfessionalLiabilityInfoes.InsuranceCarrier",
                    "ProfessionalLiabilityInfoes.InsuranceCarrierAddress",

                    //State License
                    "StateLicenses.ProviderType",
                    "StateLicenses.StateLicenseStatus",

                    //Personal Detail                    
                    "PersonalDetail.ProviderLevel",
                    "PersonalDetail.ProviderTitles.ProviderType",

                    //PersonalIdentification
                    "PersonalIdentification",

                    //ProfileDisclosure
                    "ProfileDisclosure.ProfileDisclosureQuestionAnswers",

                    //Languages
                    "LanguageInfo.KnownLanguages",  
                  
                    //BirthInformation
                    "BirthInformation",

                    //ECFMG
                    "ECFMGDetail",

                    //MedicareInformations
                    "MedicareInformations",

                    //MedicaidInformations
                    "MedicaidInformations",

                    //FederalDEAInformations
                    "FederalDEAInformations",

                    //Resindency Internship
                    "TrainingDetails.ResidencyInternshipDetails.Specialty",
                    "ProgramDetails.Specialty",

                    //Disclosure Questions
                    //"ProfileDisclosureQuestionAnswer.Question.QuestionCategory",

                    // Practice Locations 
                    "PracticeLocationDetails.Organization",
                    "PracticeLocationDetails.Group",
                    "PracticeLocationDetails.Group.Group",
                    "PracticeLocationDetails.WorkersCompensationInformation",

                    "PracticeLocationDetails.Facility",
                    "PracticeLocationDetails.Facility.FacilityDetail",
                    "PracticeLocationDetails.Facility.FacilityDetail.Language",
                    "PracticeLocationDetails.Facility.FacilityDetail.Language.NonEnglishLanguages",
                    "PracticeLocationDetails.Facility.FacilityDetail.Accessibility",
                    "PracticeLocationDetails.Facility.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers",
                    "PracticeLocationDetails.Facility.FacilityDetail.Service",
                    "PracticeLocationDetails.Facility.FacilityDetail.Service.FacilityServiceQuestionAnswers",
                    "PracticeLocationDetails.Facility.FacilityDetail.Service.PracticeType",

                    "PracticeLocationDetails.Facility.FacilityDetail.FacilityPracticeProviders",
                    "PracticeLocationDetails.Facility.FacilityDetail.FacilityPracticeProviders.FacilityPracticeProviderTypes",
                    "PracticeLocationDetails.Facility.FacilityDetail.FacilityPracticeProviders.FacilityPracticeProviderSpecialties",

                    "PracticeLocationDetails.Facility.FacilityDetail.PracticeOfficeHour",
                    "PracticeLocationDetails.Facility.FacilityDetail.PracticeOfficeHour.PracticeDays",
                    "PracticeLocationDetails.Facility.FacilityDetail.PracticeOfficeHour.PracticeDays.DailyHours",

                    "PracticeLocationDetails.OfficeHour",
                    "PracticeLocationDetails.OfficeHour.PracticeDays",
                    "PracticeLocationDetails.OfficeHour.PracticeDays.DailyHours",
                    
                    "PracticeLocationDetails.OpenPracticeStatus",
                    "PracticeLocationDetails.OpenPracticeStatus.PracticeQuestionAnswers",

                    "PracticeLocationDetails.BillingContactPerson",
                    "PracticeLocationDetails.BusinessOfficeManagerOrStaff",
                    "PracticeLocationDetails.PaymentAndRemittance",
                    "PracticeLocationDetails.PaymentAndRemittance.PaymentAndRemittancePerson",
                    "PracticeLocationDetails.PrimaryCredentialingContactPerson",
                    "PracticeLocationDetails.PracticeProviders",

                    
                    //Disclosure Questions,

                    //"ProfileDisclosureQuestionAnswer.Question.QuestionCategory",

                    //Contract Information
                    "ContractInfoes.ContractGroupInfoes",
                      "ContractInfoes.ContractGroupInfoes.PracticingGroup",
                        "ContractInfoes.ContractGroupInfoes.PracticingGroup.Group",

                    //work history
                    //"WorkExperience.ProviderType"
                    "ProfessionalWorkExperiences.ProviderType"
                };
                var profileRepo = uow.GetGenericRepository<Profile>();
                Profile profile = profileRepo.Find(s => s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString() && s.ProfileID == profileID, includeProperties);

                if (profile.StateLicenses.Count > 0)
                    profile.StateLicenses = profile.StateLicenses.Where(s => (s.Status != null && s.Status != StatusType.Inactive.ToString())).ToList();

                if (profile.CDSCInformations.Count > 0)
                    profile.CDSCInformations = profile.CDSCInformations.Where(c => (c.Status != null && c.Status != StatusType.Inactive.ToString())).ToList();

                if (profile.CMECertifications.Count > 0)
                    profile.CMECertifications = profile.CMECertifications.Where(c => (c.Status != null && c.Status != StatusType.Inactive.ToString())).ToList();

                if (profile.MedicaidInformations.Count > 0)
                    profile.MedicaidInformations = profile.MedicaidInformations.Where(c => (c.Status != null && c.Status != StatusType.Inactive.ToString())).ToList();

                if (profile.MedicareInformations.Count > 0)
                    profile.MedicareInformations = profile.MedicareInformations.Where(c => (c.Status != null && c.Status != StatusType.Inactive.ToString())).ToList();

                if (profile.ProgramDetails.Count > 0)
                    profile.ProgramDetails = profile.ProgramDetails.Where(p => (p.Status != null && p.Status != StatusType.Inactive.ToString())).ToList();

                if (profile.EducationDetails.Count > 0)
                    profile.EducationDetails = profile.EducationDetails.Where(e => (e.Status != null && e.Status != StatusType.Inactive.ToString())).ToList();

                if (profile.SpecialtyDetails.Count > 0)
                    profile.SpecialtyDetails = profile.SpecialtyDetails.Where(s => (s.Status != null && s.Status != StatusType.Inactive.ToString())).ToList();

                if (profile.PracticeLocationDetails.Count > 0)
                    profile.PracticeLocationDetails = profile.PracticeLocationDetails.Where(p => (p.Status != null && p.Status != StatusType.Inactive.ToString())).ToList();

                if (profile.HospitalPrivilegeInformation != null && profile.HospitalPrivilegeInformation.HospitalPrivilegeDetails.Count > 0)
                    profile.HospitalPrivilegeInformation.HospitalPrivilegeDetails = profile.HospitalPrivilegeInformation.HospitalPrivilegeDetails.Where(h => (h.Status != null && h.Status != StatusType.Inactive.ToString())).ToList();

                if (profile.PublicHealthServices.Count > 0)
                    profile.PublicHealthServices = profile.PublicHealthServices.Where(p => (p.Status != null && p.Status != StatusType.Inactive.ToString())).ToList();

                if (profile.MilitaryServiceInformations.Count > 0)
                    profile.MilitaryServiceInformations = profile.MilitaryServiceInformations.Where(m => (m.Status != null && m.Status != StatusType.Inactive.ToString())).ToList();

                if (profile.ProfessionalWorkExperiences.Count > 0)
                    profile.ProfessionalWorkExperiences = profile.ProfessionalWorkExperiences.Where(w => (w.Status != null && w.Status != StatusType.Inactive.ToString())).ToList();

                if (profile.ProfessionalLiabilityInfoes.Count > 0)
                    profile.ProfessionalLiabilityInfoes = profile.ProfessionalLiabilityInfoes.Where(l => (l.Status != null && l.Status != StatusType.Inactive.ToString())).ToList();

                if (profile.OtherLegalNames.Count > 0)
                    profile.OtherLegalNames = profile.OtherLegalNames.Where(o => (o.Status != StatusType.Inactive.ToString())).ToList();

                if (profile.HomeAddresses.Count > 0)
                    profile.HomeAddresses = profile.HomeAddresses.Where(h => (h.Status != null && h.Status != StatusType.Inactive.ToString())).ToList();

                if (profile.WorkGaps.Count > 0)
                    profile.WorkGaps = profile.WorkGaps.Where(w => (w.Status != null && w.Status != StatusType.Inactive.ToString())).ToList();

                if (profile.ProfessionalReferenceInfos.Count > 0)
                    profile.ProfessionalReferenceInfos = profile.ProfessionalReferenceInfos.Where(r => (r.Status != null && r.Status != StatusType.Inactive.ToString())).ToList();

                if (profile.ProfessionalAffiliationInfos.Count > 0)
                    profile.ProfessionalAffiliationInfos = profile.ProfessionalAffiliationInfos.Where(a => (a.Status != null && a.Status != StatusType.Inactive.ToString())).ToList();

                if (profile.ContractInfoes.Count > 0)
                    profile.ContractInfoes = profile.ContractInfoes.Where(c => c.ContractStatus != null && !c.ContractStatus.Equals(Entities.MasterData.Enums.ContractStatus.Inactive.ToString())).ToList();

                if (profile.FederalDEAInformations.Count > 0)
                    profile.FederalDEAInformations = profile.FederalDEAInformations.Where(c => c.Status != null && !c.Status.Equals(StatusType.Inactive.ToString())).ToList();


                pname = profile.PersonalDetail.FirstName + "_" + profile.PersonalDetail.LastName;

                return profile;
            }
            catch (Exception)
            {
                throw;
            }
        }
       
        //[HttpPost]
        public void AddDocument(int profileId, string documentTitle)
        {            
            OtherDocument otherDocument = new OtherDocument();

            try
            {
                string includeProperties = "OtherDocuments";
                var profileRepo = uow.GetGenericRepository<Profile>();
                Profile profile = profileRepo.Find(s => s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString() && s.ProfileID == profileId, includeProperties);

                otherDocument.IsPrivate =true;
                otherDocument.DocumentPath = "\\ApplicationDocument\\GeneratedTemplatePdf\\" + pname;
                otherDocument.Title = documentTitle;
                otherDocument.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;
                otherDocument.DocumentCategoryType = AHC.CD.Entities.MasterData.Enums.DocumentCategoryType.PlanForm;
                profile.OtherDocuments.Add(otherDocument);
                profileRepo.Update(profile);
                profileRepo.Save();
               
            }            
            catch (Exception ex)
            {                
                throw;
            }            
        }

        private void SaveGeneratedPackage(int profileId, string documentTitle, string outputFileName)
        {
            OtherDocument otherDocument = new OtherDocument();

            try
            {
                string includeProperties = "OtherDocuments";
                var profileRepo = uow.GetGenericRepository<Profile>();
                Profile profile = profileRepo.Find(s => s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString() && s.ProfileID == profileId, includeProperties);

                otherDocument.IsPrivate = true;
                otherDocument.DocumentPath = outputFileName;
                otherDocument.Title = documentTitle;
               
                profile.OtherDocuments.Add(otherDocument);
                profileRepo.Update(profile);
                profileRepo.Save();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private string CreatePDF(Dictionary<string, string> dataObj, string pdfName, string templateName)
        {

            try
            {
                string pdfTemplate = HttpContext.Current.Server.MapPath("~/ApplicationDocument/PlanTemplatePdf/" + templateName + ".pdf");
                //pdfName = pname +"_"+ DateTime.Today.Date;
                string newFile = HttpContext.Current.Server.MapPath("~/ApplicationDocument/GeneratedTemplatePdf/" + pdfName + ".pdf");

                PdfReader pdfReader = new PdfReader(pdfTemplate);
                PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(
                            newFile, FileMode.Create), '\0', true);

                AcroFields pdfFormFields = pdfStamper.AcroFields;

                foreach (KeyValuePair<string, string> entry in dataObj)
                {
                    if (entry.Value != null)
                    {
                        //Debug.WriteLine(prop.Name.ToString() + " is " + prop.GetValue(dataObj).ToString());
                        pdfFormFields.SetField(entry.Key.ToString(), entry.Value.ToString());
                    }
                }


                // report by reading values from completed PDF
                //string sTmp = "PDF Completed";


                // flatten the form to remove editting options, set it to false
                // to leave the form open to subsequent manual edits
                pdfStamper.FormFlattening = false;

                // close the pdf
                pdfStamper.Close();
                pname = pdfName + ".pdf";
                SaveGeneratedPackage(profileId, "Generated Form", "\\ApplicationDocument\\GeneratedTemplatePdf\\" + templateName);
                return pname;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private string readXml(PDFMappingDataBusinessModel pmodel, string templateName)
        {
            try
            {
                Dictionary<string, string> dictionary = new Dictionary<string, string>();

                var xmldoc = new System.Xml.XmlDocument();
                var xmlPath = HttpContext.Current.Server.MapPath("~/ApplicationDocument/PlanTemplateXml/" + templateName + ".xml");
                xmldoc.Load(xmlPath);

                foreach (XmlNode node in xmldoc.GetElementsByTagName("Property"))
                {
                    string GenericVariableName = node.Attributes["GenericVariable"].Value; //or loop through its children as well
                    string PlanVariableName = node.Attributes["PlanVariable"].Value; //or loop through its children as well

                    foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(pmodel))
                    {
                        if (prop.Name.ToString() == GenericVariableName)
                        {
                            if (prop.GetValue(pmodel) != null)
                            {
                                try
                                {
                                    dictionary.Add(PlanVariableName, prop.GetValue(pmodel).ToString());
                                }
                                catch (Exception)
                                {
                                    throw new Exception(PlanVariableName + "Variable is already added");
                                }
                            }
                            break;
                        }
                    }
                }
                string date = DateTime.Now.ToString("MM-dd-yyyy");
                string timeHour = DateTime.Now.Hour.ToString();
                string timeMin = DateTime.Now.Minute.ToString();
                string timeSec = DateTime.Now.Second.ToString();

                string fileName = pmodel.Personal_ProviderName + "_" + templateName + "_Date" + date + "_Hour" + timeHour + "_Min" + timeMin + "_Sec" + timeSec;

                string pdfName = CreatePDF(dictionary, fileName, templateName);
                return pdfName;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public string GeneratePlanFormPDF(int profileId, string templateName)
        {
            Profile profile = GetProfileList(profileId);

            PDFMappingDataBusinessModel pmodel = new PDFMappingDataBusinessModel();

            #region Demographics

            #region Personal Details

            pmodel.Personal_ProviderProfileImage = profile.ProfilePhotoPath;
            if (profile.PersonalDetail.ProviderTitles.Count > 0)
            {
                profile.PersonalDetail.ProviderTitles = profile.PersonalDetail.ProviderTitles.Where(s => (s.Status != null && s.StatusType != StatusType.Inactive)).ToList();
                if (profile.PersonalDetail.ProviderTitles.Count > 0)
                {
                    var titleCount = profile.PersonalDetail.ProviderTitles.Count;
                    if (profile.PersonalDetail.ProviderTitles.ElementAt(titleCount - 1).ProviderType != null)
                        pmodel.Personal_ProviderType = profile.PersonalDetail.ProviderTitles.ElementAt(titleCount - 1).ProviderType.Code;
                }
            }
            if (profile.PersonalDetail.MiddleName != null)
                pmodel.Personal_ProviderName = profile.PersonalDetail.FirstName + " " + profile.PersonalDetail.MiddleName + " " + profile.PersonalDetail.LastName;
            else
                pmodel.Personal_ProviderName = profile.PersonalDetail.FirstName + " " + profile.PersonalDetail.LastName;

            if (pmodel.Personal_ProviderType != null)
            {
                pmodel.Provider_FullNameTitle = pmodel.Personal_ProviderName +" "+ pmodel.Personal_ProviderType;
                pmodel.Personal_ProviderTitle = pmodel.Personal_ProviderType;
            }
            else
            {
                pmodel.Provider_FullNameTitle = pmodel.Personal_ProviderName;
            }                

            pmodel.Personal_ProviderFirstName = profile.PersonalDetail.FirstName;
            pmodel.Personal_ProviderMiddleName = profile.PersonalDetail.MiddleName;
            pmodel.Personal_ProviderLastName = profile.PersonalDetail.LastName;
            pmodel.Personal_ProviderSuffix = profile.PersonalDetail.Suffix;
            pmodel.Personal_ProviderGender = profile.PersonalDetail.Gender;
            pmodel.Personal_MaidenName = profile.PersonalDetail.MaidenName;
            pmodel.Personal_MaritalStatus = profile.PersonalDetail.MaritalStatus;
            pmodel.Personal_SpouseName = profile.PersonalDetail.SpouseName;

            if (profile.BirthInformation != null)
            {
                //DateTime birth = Convert.ToDateTime(profile.BirthInformation.DateOfBirth);
                //pmodel.Personal_ProviderDOB = ConvertToDateString(birth);
                pmodel.Personal_ProviderDOB = profile.BirthInformation.DateOfBirth.Split(' ')[0];

            }


            //foreach (var ptitle in profile.PersonalDetail.ProviderTitles)
            //{
            //    pmodel.Personal_ProviderTitle += ptitle.ProviderType.Title + ", ";
            //}

            if (profile.LanguageInfo != null && profile.LanguageInfo.KnownLanguages.Count > 0)
            {
                pmodel.Personal_LanguageKnown1 = profile.LanguageInfo.KnownLanguages.ElementAt(profile.LanguageInfo.KnownLanguages.Count - 1).Language;
            }


            #endregion

            #region Other Legal Names

            if (profile.OtherLegalNames.Count > 0)
            {
                profile.OtherLegalNames = profile.OtherLegalNames.Where(s => (s.Status != null && s.StatusType != StatusType.Inactive)).ToList();
                var otherLegalNameCount = profile.OtherLegalNames.Count;
                pmodel.OtherLegalName_FirstName1 = profile.OtherLegalNames.ElementAt(otherLegalNameCount - 1).OtherFirstName;
                pmodel.OtherLegalName_MiddleName1 = profile.OtherLegalNames.ElementAt(otherLegalNameCount - 1).OtherMiddleName;
                pmodel.OtherLegalName_LastName1 = profile.OtherLegalNames.ElementAt(otherLegalNameCount - 1).OtherLastName;
                pmodel.OtherLegalName_Suffix1 = profile.OtherLegalNames.ElementAt(otherLegalNameCount - 1).Suffix;
                pmodel.OtherLegalName_StartDate1 = ConvertToDateString(profile.OtherLegalNames.ElementAt(otherLegalNameCount - 1).StartDate);
                pmodel.OtherLegalName_EndDate1 = ConvertToDateString(profile.OtherLegalNames.ElementAt(otherLegalNameCount - 1).EndDate);
                pmodel.OtherLegalName_Certificate1 = profile.OtherLegalNames.ElementAt(otherLegalNameCount - 1).DocumentPath;
            }

            #endregion

            #region Contact Information

            if (profile.HomeAddresses.Count > 0)
            {
                profile.HomeAddresses = profile.HomeAddresses.Where(s => (s.Status != null && s.StatusType != StatusType.Inactive)).ToList();
                var homeCount = profile.HomeAddresses.Count;
                pmodel.Personal_ApartmentNumber1 = profile.HomeAddresses.ElementAt(homeCount - 1).UnitNumber;
                pmodel.Personal_StreetAddress1 = profile.HomeAddresses.ElementAt(homeCount - 1).Street;
                pmodel.Personal_Country1 = profile.HomeAddresses.ElementAt(homeCount - 1).Country;
                pmodel.Personal_State1 = profile.HomeAddresses.ElementAt(homeCount - 1).State;
                pmodel.Personal_City1 = profile.HomeAddresses.ElementAt(homeCount - 1).City;
                pmodel.Personal_County1 = profile.HomeAddresses.ElementAt(homeCount - 1).County;
                pmodel.Personal_ZipCode1 = profile.HomeAddresses.ElementAt(homeCount - 1).ZipCode;
                pmodel.Personal_LivingStartDate1 = ConvertToDateString(profile.HomeAddresses.ElementAt(homeCount - 1).LivingFromDate);
                pmodel.Personal_LivingEndDate1 = ConvertToDateString(profile.HomeAddresses.ElementAt(homeCount - 1).LivingEndDate);
            }

            string currDate = DateTime.Now.ToString("MM-dd-yyyy");
            pmodel.currentDate = currDate;

            #endregion

            #region Contact Details


            if (profile.ContactDetail != null)
            {
                if (profile.ContactDetail.PhoneDetails.Count > 0)
                {
                    profile.ContactDetail.PhoneDetails = profile.ContactDetail.PhoneDetails.Where(s => (s.Status != null && s.StatusType != StatusType.Inactive)).ToList();
                    var Count = profile.HomeAddresses.Count;
                    if (profile.ContactDetail.PhoneDetails.Count > 0)
                    {
                        var IsPhone = profile.ContactDetail.PhoneDetails.Any(p => p.PhoneType == AHC.CD.Entities.MasterData.Enums.PhoneTypeEnum.Mobile.ToString());
                        var IsFax = profile.ContactDetail.PhoneDetails.Any(p => p.PhoneType == AHC.CD.Entities.MasterData.Enums.PhoneTypeEnum.Fax.ToString());
                        
                        if(IsPhone)
                            pmodel.Personal_HomePhone1 = profile.ContactDetail.PhoneDetails.Where(p => p.PhoneType == AHC.CD.Entities.MasterData.Enums.PhoneTypeEnum.Mobile.ToString()).Last().Number.ToString();

                        if (IsFax)
                            pmodel.Personal_HomeFax1 = profile.ContactDetail.PhoneDetails.Where(p => p.PhoneType == AHC.CD.Entities.MasterData.Enums.PhoneTypeEnum.Fax.ToString()).Last().Number.ToString();
                        
                    }                    
                }

                
                if (profile.ContactDetail.EmailIDs.Count > 0)
                {
                    var emails = profile.ContactDetail.EmailIDs.Where(e => e.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();

                    if(emails.Count > 0)
                        pmodel.Personal_EmailId1 = emails.Last().EmailAddress.ToString();
                }
            }

            #endregion

            #region Personal Identification

            if (profile.PersonalIdentification != null)
            {
                pmodel.Personal_SocialSecurityNumber = profile.PersonalIdentification.SSN;
                pmodel.Personal_DriverLicense = profile.PersonalIdentification.DL;
                pmodel.Personal_SSNCertificate = profile.PersonalIdentification.SSNCertificatePath;
                pmodel.Personal_DriverLicenseCertificate = profile.PersonalIdentification.DLCertificatePath;
            }


            #endregion

            #region Birth Information

            if (profile.BirthInformation != null)
            {
                pmodel.Personal_BirthDate = profile.BirthInformation.DateOfBirth.Split(' ')[0];
                pmodel.Personal_BirthCountry = profile.BirthInformation.CountryOfBirth;
                pmodel.Personal_BirthState = profile.BirthInformation.StateOfBirth;
                pmodel.Personal_BirthCounty = profile.BirthInformation.CountyOfBirth;
                pmodel.Personal_BirthCity = profile.BirthInformation.CityOfBirth;
                pmodel.Personal_BirthCertificate = profile.BirthInformation.BirthCertificatePath;
            }

            #endregion

            #region Visa Information

            if (profile.VisaDetail != null)
            {
                pmodel.Personal_IsUSCitizen = profile.VisaDetail.IsResidentOfUSA;
                pmodel.Personal_IsUSAuthorized = profile.VisaDetail.IsAuthorizedToWorkInUS;
                if (profile.VisaDetail.VisaInfo != null)
                {
                    pmodel.Personal_VisaNumber = profile.VisaDetail.VisaInfo.VisaNumber;
                    if (profile.VisaDetail.VisaInfo.VisaType != null)
                        pmodel.Personal_VisaType = profile.VisaDetail.VisaInfo.VisaType.Title;

                    if (profile.VisaDetail.VisaInfo.VisaStatus != null)
                        pmodel.Personal_VisaStatus = profile.VisaDetail.VisaInfo.VisaStatus.Title;

                    pmodel.Personal_VisaSponsor = profile.VisaDetail.VisaInfo.VisaSponsor;
                    pmodel.Personal_VisaExpiration = ConvertToDateString(profile.VisaDetail.VisaInfo.VisaExpirationDate);
                    pmodel.Personal_VisaDocument = profile.VisaDetail.VisaInfo.VisaCertificatePath;
                    pmodel.Personal_GreenCardNumber = profile.VisaDetail.VisaInfo.GreenCardNumber;
                    pmodel.Personal_GreenCardDocument = profile.VisaDetail.VisaInfo.GreenCardCertificatePath;
                    pmodel.Personal_NationalIdentificationNo = profile.VisaDetail.VisaInfo.NationalIDNumber;
                    pmodel.Personal_NationalIdentificationDoc = profile.VisaDetail.VisaInfo.NationalIDCertificatePath;
                    pmodel.Personal_IssueCountry = profile.VisaDetail.VisaInfo.CountryOfIssue;
                }
            }

            #endregion

            #endregion

            #region Specialty


            if (profile.SpecialtyDetails.Count > 0)
            {
                var primaryInfo = profile.SpecialtyDetails.FirstOrDefault(p => p.SpecialtyPreference == AHC.CD.Entities.MasterData.Enums.PreferenceType.Primary.ToString() && !p.StatusType.Equals(AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()));

                var secondaryInfo = profile.SpecialtyDetails.Where(p => p.SpecialtyPreference == AHC.CD.Entities.MasterData.Enums.PreferenceType.Secondary.ToString() && !p.StatusType.Equals(AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString())).ToList();

                var secondaryCount = secondaryInfo.Count;

                if (primaryInfo != null)
                {
                    if (primaryInfo.Specialty != null)
                    {
                        pmodel.Specialty_PrimarySpecialtyName = primaryInfo.Specialty.Name;
                    }

                    pmodel.Specialty_PrimaryIsBoardCertified = primaryInfo.IsBoardCertified.ToString();

                    if (primaryInfo.SpecialtyBoardCertifiedDetail != null)
                    {
                        pmodel.Specialty_PrimaryCertificate = primaryInfo.SpecialtyBoardCertifiedDetail.CertificateNumber;
                        pmodel.Specialty_PrimaryLastRecertificationDate = ConvertToDateString(primaryInfo.SpecialtyBoardCertifiedDetail.LastReCerificationDate);
                        pmodel.Specialty_PrimaryExpirationDate = ConvertToDateString(primaryInfo.SpecialtyBoardCertifiedDetail.ExpirationDate);

                        if (primaryInfo.SpecialtyBoardCertifiedDetail.SpecialtyBoard != null)
                            pmodel.Specialty_PrimaryBoardName = primaryInfo.SpecialtyBoardCertifiedDetail.SpecialtyBoard.Name;
                    }

                    if (secondaryInfo.Count > 0)
                    {
                        pmodel.Specialty_IsBoardCertified1 = secondaryInfo.ElementAt(secondaryCount - 1).IsBoardCertified.ToString();

                        if (secondaryInfo.ElementAt(secondaryCount - 1).SpecialtyBoardCertifiedDetail != null)
                        {
                            pmodel.Specialty_Certificate1 = secondaryInfo.ElementAt(secondaryCount - 1).SpecialtyBoardCertifiedDetail.CertificateNumber;
                            pmodel.Specialty_LastRecertificationDate1 = ConvertToDateString(primaryInfo.SpecialtyBoardCertifiedDetail.LastReCerificationDate);
                            pmodel.Specialty_ExpirationDate1 = ConvertToDateString(primaryInfo.SpecialtyBoardCertifiedDetail.ExpirationDate);

                            if (secondaryInfo.ElementAt(secondaryCount - 1).SpecialtyBoardCertifiedDetail.SpecialtyBoard != null)                            
                                pmodel.Specialty_BoardName1 = secondaryInfo.ElementAt(secondaryCount - 1).SpecialtyBoardCertifiedDetail.SpecialtyBoard.Name;
                        }

                        if (secondaryInfo.ElementAt(secondaryCount - 1) != null && secondaryInfo.ElementAt(secondaryCount - 1).Specialty != null)
                        {
                            pmodel.Specialty_SpecialtyName1 = secondaryInfo.ElementAt(secondaryCount - 1).Specialty.Name;
                        }
                    }
                }
                else
                {
                    if (secondaryInfo.Count > 0)
                    {
                        pmodel.Specialty_PrimaryIsBoardCertified = secondaryInfo.ElementAt(secondaryCount - 1).IsBoardCertified.ToString();

                        if (secondaryInfo.ElementAt(secondaryCount - 1).SpecialtyBoardCertifiedDetail != null)
                        {
                            pmodel.Specialty_PrimaryCertificate = secondaryInfo.ElementAt(secondaryCount - 1).SpecialtyBoardCertifiedDetail.CertificateNumber;
                            pmodel.Specialty_PrimaryLastRecertificationDate = ConvertToDateString(primaryInfo.SpecialtyBoardCertifiedDetail.LastReCerificationDate);
                            pmodel.Specialty_PrimaryExpirationDate = ConvertToDateString(primaryInfo.SpecialtyBoardCertifiedDetail.ExpirationDate);

                            if (secondaryInfo.ElementAt(secondaryCount - 1).SpecialtyBoardCertifiedDetail.SpecialtyBoard != null)   
                                pmodel.Specialty_PrimaryBoardName = secondaryInfo.ElementAt(secondaryCount - 1).SpecialtyBoardCertifiedDetail.SpecialtyBoard.Name;
                        }

                        if (secondaryInfo.ElementAt(secondaryCount - 1) != null && secondaryInfo.ElementAt(secondaryCount - 1).Specialty != null)
                        {
                            pmodel.Specialty_PrimarySpecialtyName = secondaryInfo.ElementAt(secondaryCount - 1).Specialty.Name;
                        }
                    }
                    if (secondaryInfo.Count > 1)
                    {
                        pmodel.Specialty_IsBoardCertified1 = secondaryInfo.ElementAt(secondaryCount - 2).IsBoardCertified.ToString();

                        if (secondaryInfo.ElementAt(secondaryCount - 2).SpecialtyBoardCertifiedDetail != null && secondaryInfo.ElementAt(secondaryCount - 2).SpecialtyBoardCertifiedDetail.SpecialtyBoard != null)
                        {
                            pmodel.Specialty_Certificate1 = secondaryInfo.ElementAt(secondaryCount - 2).SpecialtyBoardCertifiedDetail.CertificateNumber;
                            pmodel.Specialty_LastRecertificationDate1 = ConvertToDateString(primaryInfo.SpecialtyBoardCertifiedDetail.LastReCerificationDate);
                            pmodel.Specialty_ExpirationDate1 = ConvertToDateString(primaryInfo.SpecialtyBoardCertifiedDetail.ExpirationDate);
                            
                            if (secondaryInfo.ElementAt(secondaryCount - 2).SpecialtyBoardCertifiedDetail.SpecialtyBoard != null)
                                pmodel.Specialty_BoardName1 = secondaryInfo.ElementAt(secondaryCount - 2).SpecialtyBoardCertifiedDetail.SpecialtyBoard.Name;
                        }

                        if (secondaryInfo.ElementAt(secondaryCount - 2) != null && secondaryInfo.ElementAt(secondaryCount - 2).Specialty != null)
                        {
                            pmodel.Specialty_SpecialtyName1 = secondaryInfo.ElementAt(secondaryCount - 2).Specialty.Name;
                        }
                    }
                }
            }

            #endregion

            #region Practice Location

            #region General Information

            if (profile.PracticeLocationDetails.Count > 0)
            {
                var practiceLocation = profile.PracticeLocationDetails.Where(s => s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();
                var practiceLocationCount = practiceLocation.Count;

                if (practiceLocation.Count > 0 && practiceLocation[practiceLocationCount - 1] != null)
                {
                    if (practiceLocation[practiceLocationCount - 1].Facility != null)
                    {
                        pmodel.General_PracticeLocationAddress1 = practiceLocation[practiceLocationCount - 1].Facility.Street + " " + practiceLocation[practiceLocationCount - 1].Facility.City + ", " + practiceLocation[practiceLocationCount - 1].Facility.State + ", " + practiceLocation[practiceLocationCount - 1].Facility.ZipCode;

                        if (practiceLocation[practiceLocationCount - 1].Facility.Telephone != null)
                        {
                            pmodel.General_PhoneFirstThreeDigit1 = practiceLocation[practiceLocationCount - 1].Facility.Telephone.Substring(0, practiceLocation[practiceLocationCount - 1].Facility.Telephone.Length - 7);
                            pmodel.General_PhoneSecondThreeDigit1 = practiceLocation[practiceLocationCount - 1].Facility.Telephone.Substring(3, 3);
                            pmodel.General_PhoneLastFourDigit1 = practiceLocation[practiceLocationCount - 1].Facility.Telephone.Substring(6);

                            pmodel.General_Phone1 = pmodel.General_PhoneFirstThreeDigit1 + "-" + pmodel.General_PhoneSecondThreeDigit1 + "-" + pmodel.General_PhoneLastFourDigit1;
                        }
                            
                        
                        pmodel.General_Email1 = practiceLocation[practiceLocationCount - 1].Facility.EmailAddress;

                        if (practiceLocation[practiceLocationCount - 1].Facility.Fax != null)
                        {
                            //pmodel.General_Fax1 = practiceLocation[practiceLocationCount - 1].Facility.Fax.Substring(3, practiceLocation[practiceLocationCount - 1].Facility.Fax.Length - 3);

                            pmodel.General_FaxFirstThreeDigit1 = practiceLocation[practiceLocationCount - 1].Facility.Fax.Substring(0, practiceLocation[practiceLocationCount - 1].Facility.Telephone.Length - 7);
                            pmodel.General_FaxSecondThreeDigit1 = practiceLocation[practiceLocationCount - 1].Facility.Fax.Substring(3, 3);
                            pmodel.General_FaxLastFourDigit1 = practiceLocation[practiceLocationCount - 1].Facility.Fax.Substring(6);

                            pmodel.General_Fax1 = pmodel.General_FaxFirstThreeDigit1 + "-" + pmodel.General_FaxSecondThreeDigit1 + "-" + pmodel.General_FaxLastFourDigit1;
                        }
                            
                        
                        
                        pmodel.General_Suite1 = practiceLocation[practiceLocationCount - 1].Facility.Building;
                        pmodel.General_Street1 = practiceLocation[practiceLocationCount - 1].Facility.Street;
                        pmodel.General_City1 = practiceLocation[practiceLocationCount - 1].Facility.City;
                        pmodel.General_State1 = practiceLocation[practiceLocationCount - 1].Facility.State;
                        pmodel.General_ZipCode1 = practiceLocation[practiceLocationCount - 1].Facility.ZipCode;
                        pmodel.General_Country1 = practiceLocation[practiceLocationCount - 1].Facility.Country;
                        pmodel.General_County1 = practiceLocation[practiceLocationCount - 1].Facility.County;
                        pmodel.General_IsCurrentlyPracticing1 = practiceLocation[practiceLocationCount - 1].CurrentlyPracticingAtThisAddress;
                        pmodel.LocationAddress_Line1 = pmodel.General_Suite1 + " " + pmodel.General_Street1;
                        pmodel.LocationAddress_Line2 = pmodel.General_City1 + "," + pmodel.General_State1 + "," + pmodel.General_ZipCode1;
                        //pmodel.LocationAddress_Line3 = "";

                        if (practiceLocation[practiceLocationCount - 1].Facility.FacilityDetail != null)
                        {
                            if (practiceLocation[practiceLocationCount - 1].Facility.FacilityDetail.Language != null)
                            {
                                var languages = practiceLocation[practiceLocationCount - 1].Facility.FacilityDetail.Language.NonEnglishLanguages.Where(l => l.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();
                                if (languages.Count > 0)
                                {
                                    foreach (var language in languages)
                                    {
                                        if (language != null)
                                            pmodel.Languages_Known1 += language.Language + ",";
                                    }
                                }
                            }

                            if (practiceLocation[practiceLocationCount - 1].Facility.FacilityDetail.Service.PracticeType != null && practiceLocation[practiceLocationCount - 1].Facility.FacilityDetail.Service.PracticeType.Title == "Solo Practice")
                                pmodel.Provider_SoloPractice = practiceLocation[practiceLocationCount - 1].Facility.FacilityDetail.Service.PracticeType.Title;
                        }
                            
                    }

                    if (practiceLocation[practiceLocationCount - 1].OpenPracticeStatus != null)
                    {
                        pmodel.OpenPractice_AgeLimitations1 = practiceLocation[practiceLocationCount - 1].OpenPracticeStatus.MinimumAge + " - " + practiceLocation[practiceLocationCount - 1].OpenPracticeStatus.MaximumAge;
                    }

                    if (practiceLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff != null)
                    {
                        if (practiceLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.MiddleName != null)
                            pmodel.OfficeManager_Name1 = practiceLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.FirstName + " " + practiceLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.MiddleName + " " + practiceLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.LastName;
                        else
                            pmodel.OfficeManager_Name1 = practiceLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.FirstName + " "+ practiceLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.LastName;

                        pmodel.OfficeManager_FirstName1 = practiceLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.FirstName;
                        pmodel.OfficeManager_MiddleName1 = practiceLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.MiddleName;
                        pmodel.OfficeManager_LastName1 = practiceLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.LastName;
                        //pmodel.OfficeManager_Phone1 = practiceLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.MobileNumber;
                        pmodel.OfficeManager_Email1 = practiceLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.EmailAddress;
                        pmodel.OfficeManager_PoBoxAddress1 = practiceLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.POBoxAddress;
                        pmodel.OfficeManager_Building1 = practiceLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.Building;
                        pmodel.OfficeManager_Street1 = practiceLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.Street;
                        pmodel.OfficeManager_City1 = practiceLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.City;
                        pmodel.OfficeManager_State1 = practiceLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.State;
                        pmodel.OfficeManager_ZipCode1 = practiceLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.ZipCode;
                        pmodel.OfficeManager_Country1 = practiceLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.Country;
                        pmodel.OfficeManager_County1 = practiceLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.County;
                        //pmodel.OfficeManager_Fax1 = practiceLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.FaxNumber;

                        if (practiceLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.Telephone != null)
                        {
                            pmodel.OfficeManager_PhoneFirstThreeDigit1 = practiceLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.Telephone.Substring(0, practiceLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.Telephone.Length - 7);
                            pmodel.OfficeManager_PhoneSecondThreeDigit1 = practiceLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.Telephone.Substring(3, 3);
                            pmodel.OfficeManager_PhoneLastFourDigit1 = practiceLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.Telephone.Substring(6);

                            pmodel.OfficeManager_Phone1 = pmodel.OfficeManager_PhoneFirstThreeDigit1 + "-" + pmodel.OfficeManager_PhoneSecondThreeDigit1 + "-" + pmodel.OfficeManager_PhoneLastFourDigit1;
                        }




                        if (practiceLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.Fax != null)
                        {
                            //pmodel.General_Fax1 = practiceLocation[practiceLocationCount - 1].Facility.Fax.Substring(3, practiceLocation[practiceLocationCount - 1].Facility.Fax.Length - 3);

                            pmodel.OfficeManager_FaxFirstThreeDigit1 = practiceLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.Fax.Substring(0, practiceLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.Telephone.Length - 7);
                            pmodel.OfficeManager_FaxSecondThreeDigit1 = practiceLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.Fax.Substring(3, 3);
                            pmodel.OfficeManager_FaxLastFourDigit1 = practiceLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.Fax.Substring(6);

                            pmodel.OfficeManager_Fax1 = pmodel.OfficeManager_FaxFirstThreeDigit1 + "-" + pmodel.OfficeManager_FaxSecondThreeDigit1 + "-" + pmodel.OfficeManager_FaxLastFourDigit1;
                        }
                    }

                    if (practiceLocation[practiceLocationCount - 1].BillingContactPerson != null)
                    {
                        if (practiceLocation[practiceLocationCount - 1].BillingContactPerson.MiddleName != null)
                            pmodel.BillingContact_Name1 = practiceLocation[practiceLocationCount - 1].BillingContactPerson.FirstName + " " + practiceLocation[practiceLocationCount - 1].BillingContactPerson.MiddleName + " " + practiceLocation[practiceLocationCount - 1].BillingContactPerson.LastName;
                        else
                            pmodel.BillingContact_Name1 = practiceLocation[practiceLocationCount - 1].BillingContactPerson.FirstName + " " + practiceLocation[practiceLocationCount - 1].BillingContactPerson.LastName;
                        
                        pmodel.BillingContact_FirstName1 = practiceLocation[practiceLocationCount - 1].BillingContactPerson.FirstName;
                        pmodel.BillingContact_MiddleName1 = practiceLocation[practiceLocationCount - 1].BillingContactPerson.MiddleName;
                        pmodel.BillingContact_LastName1 = practiceLocation[practiceLocationCount - 1].BillingContactPerson.LastName;
                        pmodel.BillingContact_Email1 = practiceLocation[practiceLocationCount - 1].BillingContactPerson.EmailAddress;
                        //pmodel.BillingContact_Phone1 = practiceLocation[practiceLocationCount - 1].BillingContactPerson.MobileNumber;
                        //pmodel.BillingContact_Fax1 = practiceLocation[practiceLocationCount - 1].BillingContactPerson.FaxNumber;
                        pmodel.BillingContact_POBoxAddress1 = practiceLocation[practiceLocationCount - 1].BillingContactPerson.POBoxAddress;
                        pmodel.BillingContact_Suite1 = practiceLocation[practiceLocationCount - 1].BillingContactPerson.Building;
                        pmodel.BillingContact_Street1 = practiceLocation[practiceLocationCount - 1].BillingContactPerson.Street;
                        pmodel.BillingContact_City1 = practiceLocation[practiceLocationCount - 1].BillingContactPerson.City;
                        pmodel.BillingContact_State1 = practiceLocation[practiceLocationCount - 1].BillingContactPerson.State;
                        pmodel.BillingContact_ZipCode1 = practiceLocation[practiceLocationCount - 1].BillingContactPerson.ZipCode;
                        pmodel.BillingContact_Country1 = practiceLocation[practiceLocationCount - 1].BillingContactPerson.Country;
                        pmodel.BillingContact_County1 = practiceLocation[practiceLocationCount - 1].BillingContactPerson.County;

                        if (practiceLocation[practiceLocationCount - 1].BillingContactPerson.Telephone != null)
                        {
                            pmodel.BillingContact_PhoneFirstThreeDigit1 = practiceLocation[practiceLocationCount - 1].BillingContactPerson.Telephone.Substring(0, practiceLocation[practiceLocationCount - 1].BillingContactPerson.Telephone.Length - 7);
                            pmodel.BillingContact_PhoneSecondThreeDigit1 = practiceLocation[practiceLocationCount - 1].BillingContactPerson.Telephone.Substring(3, 3);
                            pmodel.BillingContact_PhoneLastFourDigit1 = practiceLocation[practiceLocationCount - 1].BillingContactPerson.Telephone.Substring(6);

                            pmodel.BillingContact_Phone1 = pmodel.BillingContact_PhoneFirstThreeDigit1 + "-" + pmodel.BillingContact_PhoneSecondThreeDigit1 + "-" + pmodel.BillingContact_PhoneLastFourDigit1;
                        }




                        if (practiceLocation[practiceLocationCount - 1].BillingContactPerson.Fax != null)
                        {
                            //pmodel.General_Fax1 = practiceLocation[practiceLocationCount - 1].Facility.Fax.Substring(3, practiceLocation[practiceLocationCount - 1].Facility.Fax.Length - 3);

                            pmodel.BillingContact_FaxFirstThreeDigit1 = practiceLocation[practiceLocationCount - 1].BillingContactPerson.Fax.Substring(0, practiceLocation[practiceLocationCount - 1].BillingContactPerson.Telephone.Length - 7);
                            pmodel.BillingContact_FaxSecondThreeDigit1 = practiceLocation[practiceLocationCount - 1].BillingContactPerson.Fax.Substring(3, 3);
                            pmodel.BillingContact_FaxLastFourDigit1 = practiceLocation[practiceLocationCount - 1].BillingContactPerson.Fax.Substring(6);

                            pmodel.BillingContact_Fax1 = pmodel.OfficeManager_FaxFirstThreeDigit1 + "-" + pmodel.BillingContact_FaxSecondThreeDigit1 + "-" + pmodel.BillingContact_FaxLastFourDigit1;
                        }
                    }

                    if (practiceLocation[practiceLocationCount - 1].PaymentAndRemittance != null)
                    {
                        if (practiceLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson != null)
                        {
                            if (practiceLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.MiddleName != null)
                                pmodel.PaymentRemittance_Name1 = practiceLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.FirstName + " " + practiceLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.MiddleName + " " + practiceLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.LastName;
                            else
                                pmodel.PaymentRemittance_Name1 = practiceLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.FirstName + " "+ practiceLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.LastName;
                            
                            pmodel.PaymentRemittance_FirstName1 = practiceLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.FirstName;
                            pmodel.PaymentRemittance_MiddleName1 = practiceLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.MiddleName;
                            pmodel.PaymentRemittance_LastName1 = practiceLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.LastName;
                            pmodel.PaymentRemittance_Email1 = practiceLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.EmailAddress;
                            pmodel.PaymentRemittance_POBoxAddress1 = practiceLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.POBoxAddress;
                            pmodel.PaymentRemittance_Suite1 = practiceLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.Building;
                            pmodel.PaymentRemittance_Street1 = practiceLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.Street;
                            pmodel.PaymentRemittance_City1 = practiceLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.City;
                            pmodel.PaymentRemittance_State1 = practiceLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.State;
                            pmodel.PaymentRemittance_ZipCode1 = practiceLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.ZipCode;
                            pmodel.PaymentRemittance_Country1 = practiceLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.Country;
                            pmodel.PaymentRemittance_County1 = practiceLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.County;

                            if (practiceLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.Telephone != null)
                            {
                                pmodel.PaymentRemittance_PhoneFirstThreeDigit1 = practiceLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.Telephone.Substring(0, practiceLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.Telephone.Length - 7);
                                pmodel.PaymentRemittance_PhoneSecondThreeDigit1 = practiceLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.Telephone.Substring(3, 3);
                                pmodel.PaymentRemittance_PhoneLastFourDigit1 = practiceLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.Telephone.Substring(6);

                                pmodel.PaymentRemittance_Phone1 = pmodel.PaymentRemittance_PhoneFirstThreeDigit1 + "-" + pmodel.PaymentRemittance_PhoneSecondThreeDigit1 + "-" + pmodel.PaymentRemittance_PhoneLastFourDigit1;
                            }




                            if (practiceLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.Fax != null)
                            {
                                //pmodel.General_Fax1 = practiceLocation[practiceLocationCount - 1].Facility.Fax.Substring(3, practiceLocation[practiceLocationCount - 1].Facility.Fax.Length - 3);

                                pmodel.PaymentRemittance_FaxFirstThreeDigit1 = practiceLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.Fax.Substring(0, practiceLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.Telephone.Length - 7);
                                pmodel.PaymentRemittance_FaxSecondThreeDigit1 = practiceLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.Fax.Substring(3, 3);
                                pmodel.PaymentRemittance_FaxLastFourDigit1 = practiceLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.Fax.Substring(6);

                                pmodel.PaymentRemittance_Fax1 = pmodel.PaymentRemittance_FaxFirstThreeDigit1 + "-" + pmodel.PaymentRemittance_FaxSecondThreeDigit1 + "-" + pmodel.PaymentRemittance_FaxLastFourDigit1;
                            }
                        }
                        pmodel.PaymentRemittance_ElectronicBillCapability1 = practiceLocation[practiceLocationCount - 1].PaymentAndRemittance.ElectronicBillingCapability;
                        pmodel.PaymentRemittance_BillingDepartment1 = practiceLocation[practiceLocationCount - 1].PaymentAndRemittance.BillingDepartment;
                        pmodel.PaymentRemittance_ChekPayableTo1 = practiceLocation[practiceLocationCount - 1].PaymentAndRemittance.CheckPayableTo;
                        pmodel.PaymentRemittance_Office1 = practiceLocation[practiceLocationCount - 1].PaymentAndRemittance.Office;
                    }

                    

                }
                if (practiceLocation.Count > 1 && practiceLocation[practiceLocationCount - 2] != null)
                {
                    if (practiceLocation[practiceLocationCount - 2].Facility != null)
                    {
                        pmodel.General_PracticeLocationAddress2 = practiceLocation[practiceLocationCount - 2].Facility.Street + " " + practiceLocation[practiceLocationCount - 2].Facility.City + ", " + practiceLocation[practiceLocationCount - 2].Facility.State + ", " + practiceLocation[practiceLocationCount - 2].Facility.ZipCode;
                        //pmodel.General_Phone2 = practiceLocation[practiceLocationCount - 2].Facility.MobileNumber;
                        //pmodel.General_Fax2 = practiceLocation[practiceLocationCount - 2].Facility.FaxNumber;
                        pmodel.General_Suite2 = practiceLocation[practiceLocationCount - 2].Facility.Building;
                        pmodel.General_Street2 = practiceLocation[practiceLocationCount - 2].Facility.Street;
                        pmodel.General_City2 = practiceLocation[practiceLocationCount - 2].Facility.City;
                        pmodel.General_State2 = practiceLocation[practiceLocationCount - 2].Facility.State;
                        pmodel.General_ZipCode2 = practiceLocation[practiceLocationCount - 2].Facility.ZipCode;
                        pmodel.General_Country2 = practiceLocation[practiceLocationCount - 2].Facility.Country;
                        pmodel.General_County2 = practiceLocation[practiceLocationCount - 2].Facility.County;
                        pmodel.LocationAddress2_Line1 = pmodel.General_Suite2 + " " + pmodel.General_Street2;
                        pmodel.LocationAddress2_Line2 = pmodel.General_City2 + "," + pmodel.General_State2 + "," + pmodel.General_ZipCode2;

                        if (practiceLocation[practiceLocationCount - 2].Facility.Telephone != null)
                        {
                            pmodel.General_PhoneFirstThreeDigit2 = practiceLocation[practiceLocationCount - 2].Facility.Telephone.Substring(0, practiceLocation[practiceLocationCount - 2].Facility.Telephone.Length - 7);
                            pmodel.General_PhoneSecondThreeDigit2 = practiceLocation[practiceLocationCount - 2].Facility.Telephone.Substring(3, 3);
                            pmodel.General_PhoneLastFourDigit2 = practiceLocation[practiceLocationCount - 2].Facility.Telephone.Substring(6);

                            pmodel.General_Phone2 = pmodel.General_PhoneFirstThreeDigit2 + "-" + pmodel.General_PhoneSecondThreeDigit2 + "-" + pmodel.General_PhoneLastFourDigit2;
                        }
                            
                        
                        pmodel.General_Email2 = practiceLocation[practiceLocationCount - 2].Facility.EmailAddress;

                        if (practiceLocation[practiceLocationCount - 2].Facility.Fax != null)
                        {
                            //pmodel.General_Fax1 = practiceLocation[practiceLocationCount - 1].Facility.Fax.Substring(3, practiceLocation[practiceLocationCount - 1].Facility.Fax.Length - 3);

                            pmodel.General_FaxFirstThreeDigit2 = practiceLocation[practiceLocationCount - 2].Facility.Fax.Substring(0, practiceLocation[practiceLocationCount - 2].Facility.Telephone.Length - 7);
                            pmodel.General_FaxSecondThreeDigit2 = practiceLocation[practiceLocationCount - 2].Facility.Fax.Substring(3, 3);
                            pmodel.General_FaxLastFourDigit2 = practiceLocation[practiceLocationCount - 2].Facility.Fax.Substring(6);

                            pmodel.General_Fax2 = pmodel.General_FaxFirstThreeDigit2 + "-" + pmodel.General_FaxSecondThreeDigit2 + "-" + pmodel.General_FaxLastFourDigit2;
                        }

                        if (practiceLocation[practiceLocationCount - 2].Facility.FacilityDetail != null && practiceLocation[practiceLocationCount - 2].Facility.FacilityDetail.Language != null)
                        {
                            var languages = practiceLocation[practiceLocationCount - 2].Facility.FacilityDetail.Language.NonEnglishLanguages.Where(l => l.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();
                            if (languages.Count > 0)
                            {
                                foreach (var language in languages)
                                {
                                    if (language != null)
                                        pmodel.Languages_Known2 += language.Language + ",";
                                }
                            }
                        }

                        if (practiceLocation[practiceLocationCount - 1].Facility.FacilityDetail.Service.PracticeType != null && practiceLocation[practiceLocationCount - 1].Facility.FacilityDetail.Service.PracticeType.Title == "Solo Practice")
                            pmodel.Provider_SoloPractice = practiceLocation[practiceLocationCount - 1].Facility.FacilityDetail.Service.PracticeType.Title;
                    }

                    if (practiceLocation[practiceLocationCount - 2].BusinessOfficeManagerOrStaff != null)
                    {
                        if (practiceLocation[practiceLocationCount - 2].BusinessOfficeManagerOrStaff.MiddleName != null)
                            pmodel.OfficeManager_Name2 = practiceLocation[practiceLocationCount - 2].BusinessOfficeManagerOrStaff.FirstName + " " + practiceLocation[practiceLocationCount - 2].BusinessOfficeManagerOrStaff.MiddleName + " " + practiceLocation[practiceLocationCount - 2].BusinessOfficeManagerOrStaff.LastName;
                        else
                            pmodel.OfficeManager_Name2 = practiceLocation[practiceLocationCount - 2].BusinessOfficeManagerOrStaff.FirstName + " " + practiceLocation[practiceLocationCount - 2].BusinessOfficeManagerOrStaff.LastName;

                        //pmodel.OfficeManager_Name2 = practiceLocation[practiceLocationCount - 2].BusinessOfficeManagerOrStaff.FirstName + practiceLocation[practiceLocationCount - 2].BusinessOfficeManagerOrStaff.MiddleName + practiceLocation[practiceLocationCount - 2].BusinessOfficeManagerOrStaff.LastName;
                        pmodel.OfficeManager_Email2 = practiceLocation[practiceLocationCount - 2].BusinessOfficeManagerOrStaff.EmailAddress;
                        pmodel.OfficeManager_PoBoxAddress2 = practiceLocation[practiceLocationCount - 2].BusinessOfficeManagerOrStaff.POBoxAddress;
                        pmodel.OfficeManager_Building2 = practiceLocation[practiceLocationCount - 2].BusinessOfficeManagerOrStaff.Building;
                        pmodel.OfficeManager_Street2 = practiceLocation[practiceLocationCount - 2].BusinessOfficeManagerOrStaff.Street;
                        pmodel.OfficeManager_City2 = practiceLocation[practiceLocationCount - 2].BusinessOfficeManagerOrStaff.City;
                        pmodel.OfficeManager_State2 = practiceLocation[practiceLocationCount - 2].BusinessOfficeManagerOrStaff.State;
                        pmodel.OfficeManager_ZipCode2 = practiceLocation[practiceLocationCount - 2].BusinessOfficeManagerOrStaff.ZipCode;
                        pmodel.OfficeManager_Country2 = practiceLocation[practiceLocationCount - 2].BusinessOfficeManagerOrStaff.Country;
                        pmodel.OfficeManager_County2 = practiceLocation[practiceLocationCount - 2].BusinessOfficeManagerOrStaff.County;

                        if (practiceLocation[practiceLocationCount - 2].BusinessOfficeManagerOrStaff.Telephone != null)
                        {
                            pmodel.OfficeManager_PhoneFirstThreeDigit2 = practiceLocation[practiceLocationCount - 2].BusinessOfficeManagerOrStaff.Telephone.Substring(0, practiceLocation[practiceLocationCount - 2].BusinessOfficeManagerOrStaff.Telephone.Length - 7);
                            pmodel.OfficeManager_PhoneSecondThreeDigit2 = practiceLocation[practiceLocationCount - 2].BusinessOfficeManagerOrStaff.Telephone.Substring(3, 3);
                            pmodel.OfficeManager_PhoneLastFourDigit2 = practiceLocation[practiceLocationCount - 2].BusinessOfficeManagerOrStaff.Telephone.Substring(6);

                            pmodel.OfficeManager_Phone2 = pmodel.OfficeManager_PhoneFirstThreeDigit2 + "-" + pmodel.OfficeManager_PhoneSecondThreeDigit2 + "-" + pmodel.OfficeManager_PhoneLastFourDigit2;
                        }




                        if (practiceLocation[practiceLocationCount - 2].BusinessOfficeManagerOrStaff.Fax != null)
                        {
                            //pmodel.General_Fax1 = practiceLocation[practiceLocationCount - 1].Facility.Fax.Substring(3, practiceLocation[practiceLocationCount - 1].Facility.Fax.Length - 3);

                            pmodel.OfficeManager_FaxFirstThreeDigit2 = practiceLocation[practiceLocationCount - 2].BusinessOfficeManagerOrStaff.Fax.Substring(0, practiceLocation[practiceLocationCount - 2].BusinessOfficeManagerOrStaff.Telephone.Length - 7);
                            pmodel.OfficeManager_FaxSecondThreeDigit2 = practiceLocation[practiceLocationCount - 2].BusinessOfficeManagerOrStaff.Fax.Substring(3, 3);
                            pmodel.OfficeManager_FaxLastFourDigit2 = practiceLocation[practiceLocationCount - 2].BusinessOfficeManagerOrStaff.Fax.Substring(6);

                            pmodel.OfficeManager_Fax2 = pmodel.OfficeManager_FaxFirstThreeDigit2 + "-" + pmodel.OfficeManager_FaxSecondThreeDigit2 + "-" + pmodel.OfficeManager_FaxLastFourDigit2;
                        }
                    }

                    if (practiceLocation[practiceLocationCount - 2].BillingContactPerson != null)
                    {
                        if (practiceLocation[practiceLocationCount - 2].BillingContactPerson.MiddleName != null)
                            pmodel.BillingContact_Name2 = practiceLocation[practiceLocationCount - 2].BillingContactPerson.FirstName + " " + practiceLocation[practiceLocationCount - 2].BillingContactPerson.MiddleName + " " + practiceLocation[practiceLocationCount - 2].BillingContactPerson.LastName;
                        else
                            pmodel.BillingContact_Name2 = practiceLocation[practiceLocationCount - 2].BillingContactPerson.FirstName + " " + practiceLocation[practiceLocationCount - 2].BillingContactPerson.LastName;
                        
                        //pmodel.BillingContact_Name2 = practiceLocation[practiceLocationCount - 2].BillingContactPerson.FirstName + practiceLocation[practiceLocationCount - 2].BillingContactPerson.MiddleName + practiceLocation[practiceLocationCount - 2].BillingContactPerson.LastName;
                        pmodel.BillingContact_FirstName2 = practiceLocation[practiceLocationCount - 2].BillingContactPerson.FirstName;
                        pmodel.BillingContact_MiddleName2 = practiceLocation[practiceLocationCount - 2].BillingContactPerson.MiddleName;
                        pmodel.BillingContact_LastName2 = practiceLocation[practiceLocationCount - 2].BillingContactPerson.LastName;
                        pmodel.BillingContact_Email2 = practiceLocation[practiceLocationCount - 2].BillingContactPerson.EmailAddress;
                        pmodel.BillingContact_POBoxAddress2 = practiceLocation[practiceLocationCount - 2].BillingContactPerson.POBoxAddress;
                        pmodel.BillingContact_Suite2 = practiceLocation[practiceLocationCount - 2].BillingContactPerson.Building;
                        pmodel.BillingContact_Street2 = practiceLocation[practiceLocationCount - 2].BillingContactPerson.Street;
                        pmodel.BillingContact_City2 = practiceLocation[practiceLocationCount - 2].BillingContactPerson.City;
                        pmodel.BillingContact_State2 = practiceLocation[practiceLocationCount - 2].BillingContactPerson.State;
                        pmodel.BillingContact_ZipCode2 = practiceLocation[practiceLocationCount - 2].BillingContactPerson.ZipCode;
                        pmodel.BillingContact_Country2 = practiceLocation[practiceLocationCount - 2].BillingContactPerson.Country;
                        pmodel.BillingContact_County2 = practiceLocation[practiceLocationCount - 2].BillingContactPerson.County;

                        if (practiceLocation[practiceLocationCount - 2].BillingContactPerson.Telephone != null)
                        {
                            pmodel.BillingContact_PhoneFirstThreeDigit2 = practiceLocation[practiceLocationCount - 2].BillingContactPerson.Telephone.Substring(0, practiceLocation[practiceLocationCount - 2].BillingContactPerson.Telephone.Length - 7);
                            pmodel.BillingContact_PhoneSecondThreeDigit2 = practiceLocation[practiceLocationCount - 2].BillingContactPerson.Telephone.Substring(3, 3);
                            pmodel.BillingContact_PhoneLastFourDigit1 = practiceLocation[practiceLocationCount - 2].BillingContactPerson.Telephone.Substring(6);

                            pmodel.BillingContact_Phone2 = pmodel.BillingContact_PhoneFirstThreeDigit2 + "-" + pmodel.BillingContact_PhoneSecondThreeDigit2 + "-" + pmodel.BillingContact_PhoneLastFourDigit2;
                        }




                        if (practiceLocation[practiceLocationCount - 2].BillingContactPerson.Fax != null)
                        {
                            //pmodel.General_Fax1 = practiceLocation[practiceLocationCount - 1].Facility.Fax.Substring(3, practiceLocation[practiceLocationCount - 1].Facility.Fax.Length - 3);

                            pmodel.BillingContact_FaxFirstThreeDigit2 = practiceLocation[practiceLocationCount - 2].BillingContactPerson.Fax.Substring(0, practiceLocation[practiceLocationCount - 2].BillingContactPerson.Telephone.Length - 7);
                            pmodel.BillingContact_FaxSecondThreeDigit2 = practiceLocation[practiceLocationCount - 2].BillingContactPerson.Fax.Substring(3, 3);
                            pmodel.BillingContact_FaxLastFourDigit2 = practiceLocation[practiceLocationCount - 2].BillingContactPerson.Fax.Substring(6);

                            pmodel.BillingContact_Fax2 = pmodel.OfficeManager_FaxFirstThreeDigit2 + "-" + pmodel.BillingContact_FaxSecondThreeDigit2 + "-" + pmodel.BillingContact_FaxLastFourDigit2;
                        }

                    }

                    if (practiceLocation[practiceLocationCount - 2].PaymentAndRemittance != null)
                    {
                        if (practiceLocation[practiceLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson != null)
                        {
                            if (practiceLocation[practiceLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.MiddleName != null)
                                pmodel.PaymentRemittance_Name2 = practiceLocation[practiceLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.FirstName + " " + practiceLocation[practiceLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.MiddleName + " " + practiceLocation[practiceLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.LastName;
                            else
                                pmodel.PaymentRemittance_Name2 = practiceLocation[practiceLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.FirstName + " " + practiceLocation[practiceLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.LastName;
                            
                            //pmodel.PaymentRemittance_Name2 = practiceLocation[practiceLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.FirstName + practiceLocation[practiceLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.MiddleName + practiceLocation[practiceLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.LastName;
                            pmodel.PaymentRemittance_FirstName2 = practiceLocation[practiceLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.FirstName;
                            pmodel.PaymentRemittance_MiddleName2 = practiceLocation[practiceLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.MiddleName;
                            pmodel.PaymentRemittance_LastName2 = practiceLocation[practiceLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.LastName;
                            pmodel.PaymentRemittance_Email2 = practiceLocation[practiceLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.EmailAddress;
                            pmodel.PaymentRemittance_POBoxAddress2 = practiceLocation[practiceLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.POBoxAddress;
                            pmodel.PaymentRemittance_Suite2 = practiceLocation[practiceLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.Building;
                            pmodel.PaymentRemittance_Street2 = practiceLocation[practiceLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.Street;
                            pmodel.PaymentRemittance_City2 = practiceLocation[practiceLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.City;
                            pmodel.PaymentRemittance_State2 = practiceLocation[practiceLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.State;
                            pmodel.PaymentRemittance_ZipCode2 = practiceLocation[practiceLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.ZipCode;
                            pmodel.PaymentRemittance_Country2 = practiceLocation[practiceLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.Country;
                            pmodel.PaymentRemittance_County2 = practiceLocation[practiceLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.County;

                            if (practiceLocation[practiceLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.Telephone != null)
                            {
                                pmodel.PaymentRemittance_PhoneFirstThreeDigit2 = practiceLocation[practiceLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.Telephone.Substring(0, practiceLocation[practiceLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.Telephone.Length - 7);
                                pmodel.PaymentRemittance_PhoneSecondThreeDigit2 = practiceLocation[practiceLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.Telephone.Substring(3, 3);
                                pmodel.PaymentRemittance_PhoneLastFourDigit2 = practiceLocation[practiceLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.Telephone.Substring(6);

                                pmodel.PaymentRemittance_Phone2 = pmodel.PaymentRemittance_PhoneFirstThreeDigit2 + "-" + pmodel.PaymentRemittance_PhoneSecondThreeDigit2 + "-" + pmodel.PaymentRemittance_PhoneLastFourDigit2;
                            }




                            if (practiceLocation[practiceLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.Fax != null)
                            {
                                //pmodel.General_Fax1 = practiceLocation[practiceLocationCount - 1].Facility.Fax.Substring(3, practiceLocation[practiceLocationCount - 1].Facility.Fax.Length - 3);

                                pmodel.PaymentRemittance_FaxFirstThreeDigit2 = practiceLocation[practiceLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.Fax.Substring(0, practiceLocation[practiceLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.Telephone.Length - 7);
                                pmodel.PaymentRemittance_FaxSecondThreeDigit2 = practiceLocation[practiceLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.Fax.Substring(3, 3);
                                pmodel.PaymentRemittance_FaxLastFourDigit2 = practiceLocation[practiceLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.Fax.Substring(6);

                                pmodel.PaymentRemittance_Fax2 = pmodel.PaymentRemittance_FaxFirstThreeDigit2 + "-" + pmodel.PaymentRemittance_FaxSecondThreeDigit2 + "-" + pmodel.PaymentRemittance_FaxLastFourDigit2;
                            }
                        }
                        pmodel.PaymentRemittance_ElectronicBillCapability2 = practiceLocation[practiceLocationCount - 2].PaymentAndRemittance.ElectronicBillingCapability;
                        pmodel.PaymentRemittance_BillingDepartment2 = practiceLocation[practiceLocationCount - 2].PaymentAndRemittance.BillingDepartment;
                        pmodel.PaymentRemittance_ChekPayableTo2 = practiceLocation[practiceLocationCount - 2].PaymentAndRemittance.CheckPayableTo;
                        pmodel.PaymentRemittance_Office2 = practiceLocation[practiceLocationCount - 2].PaymentAndRemittance.Office;

                    }

                    if (practiceLocation[practiceLocationCount - 2].OpenPracticeStatus != null)
                    {
                        pmodel.OpenPractice_AgeLimitations2 = practiceLocation[practiceLocationCount - 2].OpenPracticeStatus.MinimumAge + " - " + practiceLocation[practiceLocationCount - 2].OpenPracticeStatus.MaximumAge;
                    }
                }
            }


            #endregion

            #region Supervising provider

            if (profile.PracticeLocationDetails.Count > 0)
            {
                var supervisingProvider = profile.PracticeLocationDetails.ElementAt(profile.PracticeLocationDetails.Count - 1).PracticeProviders.
                    Where(s => s.Practice == AHC.CD.Entities.MasterData.Enums.PracticeType.Supervisor.ToString() && 
                        s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString()).ToList();

                var supervisorCount = supervisingProvider.Count;

                if (supervisorCount > 0)
                {
                    pmodel.CoveringColleague_FirstName1 = supervisingProvider.ElementAt(supervisorCount - 1).FirstName;
                    pmodel.CoveringColleague_MiddleName1 = supervisingProvider.ElementAt(supervisorCount - 1).MiddleName;
                    pmodel.CoveringColleague_LastName1 = supervisingProvider.ElementAt(supervisorCount - 1).LastName;

                    var specialities = supervisingProvider.ElementAt(supervisorCount - 1).PracticeProviderSpecialties.Where(s => s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString()).ToList();

                    if (specialities.Count > 0)
                    {
                        if (specialities.ElementAt(specialities.Count - 1).Specialty != null)
                            pmodel.CoveringColleague_Specialty1 = specialities.ElementAt(specialities.Count - 1).Specialty.Name;
                    }
                }
            }

            #endregion

            #region Office Hours

            if (profile.PracticeLocationDetails.Count > 0)
            {
                if (profile.PracticeLocationDetails.ElementAt(profile.PracticeLocationDetails.Count - 1).OfficeHour != null)
                {
                    if (profile.PracticeLocationDetails.ElementAt(profile.PracticeLocationDetails.Count - 1).OfficeHour.PracticeDays.Count > 0 && profile.PracticeLocationDetails.ElementAt(profile.PracticeLocationDetails.Count - 1).OfficeHour.PracticeDays.First().DailyHours.Count > 0)
                    {
                        pmodel.OfficeHour_Monday1 = profile.PracticeLocationDetails.ElementAt(profile.PracticeLocationDetails.Count - 1).OfficeHour.PracticeDays.ElementAt(0).DailyHours.First().StartTime;
                        pmodel.OfficeHour_EndMonday1 = profile.PracticeLocationDetails.ElementAt(profile.PracticeLocationDetails.Count - 1).OfficeHour.PracticeDays.ElementAt(0).DailyHours.First().EndTime;
                        pmodel.OfficeHour_Tuesday1 = profile.PracticeLocationDetails.ElementAt(profile.PracticeLocationDetails.Count - 1).OfficeHour.PracticeDays.ElementAt(1).DailyHours.First().StartTime;
                        pmodel.OfficeHour_EndTuesday1 = profile.PracticeLocationDetails.ElementAt(profile.PracticeLocationDetails.Count - 1).OfficeHour.PracticeDays.ElementAt(1).DailyHours.First().EndTime;
                        pmodel.OfficeHour_Wednesday1 = profile.PracticeLocationDetails.ElementAt(profile.PracticeLocationDetails.Count - 1).OfficeHour.PracticeDays.ElementAt(2).DailyHours.First().StartTime;
                        pmodel.OfficeHour_EndWednesday1 = profile.PracticeLocationDetails.ElementAt(profile.PracticeLocationDetails.Count - 1).OfficeHour.PracticeDays.ElementAt(2).DailyHours.First().EndTime;
                        pmodel.OfficeHour_Thursday1 = profile.PracticeLocationDetails.ElementAt(profile.PracticeLocationDetails.Count - 1).OfficeHour.PracticeDays.ElementAt(3).DailyHours.First().StartTime;
                        pmodel.OfficeHour_EndThursday1 = profile.PracticeLocationDetails.ElementAt(profile.PracticeLocationDetails.Count - 1).OfficeHour.PracticeDays.ElementAt(3).DailyHours.First().EndTime;
                        pmodel.OfficeHour_Friday1 = profile.PracticeLocationDetails.ElementAt(profile.PracticeLocationDetails.Count - 1).OfficeHour.PracticeDays.ElementAt(4).DailyHours.First().StartTime;
                        pmodel.OfficeHour_EndFriday1 = profile.PracticeLocationDetails.ElementAt(profile.PracticeLocationDetails.Count - 1).OfficeHour.PracticeDays.ElementAt(4).DailyHours.First().EndTime;
                        pmodel.OfficeHour_Saturday1 = profile.PracticeLocationDetails.ElementAt(profile.PracticeLocationDetails.Count - 1).OfficeHour.PracticeDays.ElementAt(5).DailyHours.First().StartTime;
                        pmodel.OfficeHour_EndSaturday1 = profile.PracticeLocationDetails.ElementAt(profile.PracticeLocationDetails.Count - 1).OfficeHour.PracticeDays.ElementAt(5).DailyHours.First().EndTime;
                        pmodel.OfficeHour_Sunday1 = profile.PracticeLocationDetails.ElementAt(profile.PracticeLocationDetails.Count - 1).OfficeHour.PracticeDays.ElementAt(6).DailyHours.First().StartTime;
                        pmodel.OfficeHour_EndSunday1 = profile.PracticeLocationDetails.ElementAt(profile.PracticeLocationDetails.Count - 1).OfficeHour.PracticeDays.ElementAt(6).DailyHours.First().EndTime;
                    }
                }

                
            }

            #endregion

            #region Disclosure Questions

            List<Question> filteredQuestions = new List<Question>();

            var question = uow.GetGenericRepository<Question>();
            var questions = question.GetAll().Where(q => q.Status != null && q.Status == AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString());

            //var licensureQuestions = questions.Where(l => l.QuestionCategoryId == 1).ToList();
            //filteredQuestions.Add(licensureQuestions.ElementAt(0));
            //filteredQuestions.Add(licensureQuestions.ElementAt(1));

            //var hospitalQuestions = questions.Where(l => l.QuestionCategoryId == 2).ToList();
            //filteredQuestions.Add(hospitalQuestions.ElementAt(0));
            //filteredQuestions.Add(hospitalQuestions.ElementAt(1));
            //filteredQuestions.Add(hospitalQuestions.ElementAt(2));

            //var educationQuestions = questions.Where(l => l.QuestionCategoryId == 3).ToList();
            //filteredQuestions.Add(educationQuestions.ElementAt(0));
            //filteredQuestions.Add(educationQuestions.ElementAt(1));
            //filteredQuestions.Add(educationQuestions.ElementAt(2));
            //filteredQuestions.Add(educationQuestions.ElementAt(3));

            //var deaQuestions = questions.Where(l => l.QuestionCategoryId == 4).ToList();
            //filteredQuestions.Add(deaQuestions.ElementAt(0));

            //var medicareQuestions = questions.Where(l => l.QuestionCategoryId == 5).ToList();
            //filteredQuestions.Add(medicareQuestions.ElementAt(0));


            //var otherQuestions = questions.Where(l => l.QuestionCategoryId == 6).ToList();
            //filteredQuestions.Add(otherQuestions.ElementAt(0));
            //filteredQuestions.Add(otherQuestions.ElementAt(1));
            //filteredQuestions.Add(otherQuestions.ElementAt(2));
            //filteredQuestions.Add(otherQuestions.ElementAt(3));
            //filteredQuestions.Add(otherQuestions.ElementAt(4));

            //var professionalQuestions = questions.Where(l => l.QuestionCategoryId == 7).ToList();
            //filteredQuestions.Add(professionalQuestions.ElementAt(0));
            //filteredQuestions.Add(professionalQuestions.ElementAt(1));

            //var malpracticeQuestions = questions.Where(l => l.QuestionCategoryId == 8).ToList();
            //filteredQuestions.Add(malpracticeQuestions.ElementAt(0));

            //var criminalQuestions = questions.Where(l => l.QuestionCategoryId == 9).ToList();
            //filteredQuestions.Add(criminalQuestions.ElementAt(0));
            //filteredQuestions.Add(criminalQuestions.ElementAt(1));
            //filteredQuestions.Add(criminalQuestions.ElementAt(2));

            //var abiityQuestions = questions.Where(l => l.QuestionCategoryId == 10).ToList();
            //filteredQuestions.Add(abiityQuestions.ElementAt(0));
            //filteredQuestions.Add(abiityQuestions.ElementAt(1));
            //filteredQuestions.Add(abiityQuestions.ElementAt(2));
            //filteredQuestions.Add(abiityQuestions.ElementAt(3));


            //List<ProfileDisclosureQuestionAnswer> answers = new List<ProfileDisclosureQuestionAnswer>();



            //if (profile.ProfileDisclosure != null && profile.ProfileDisclosure.ProfileDisclosureQuestionAnswers.Count > 0)
            //{

            //    foreach (var item in profile.ProfileDisclosure.ProfileDisclosureQuestionAnswers)
            //    {
            //        foreach (var item1 in filteredQuestions)
            //        {
            //            if (item.QuestionID == item1.QuestionID)
            //            {
            //                answers.Add(item);
            //            }
            //        }

            //    }

            //    profileData.LicensureQn1 = answers.ElementAt(0).ProviderDisclousreAnswer;
            //    profileData.LicensureQn2 = answers.ElementAt(1).ProviderDisclousreAnswer;
            //    profileData.HOSPITALPRIVILEGESANDOTHERAFFILIATIONSQn1 = answers.ElementAt(2).ProviderDisclousreAnswer;
            //    profileData.HOSPITALPRIVILEGESANDOTHERAFFILIATIONSQn2 = answers.ElementAt(3).ProviderDisclousreAnswer;
            //    profileData.HOSPITALPRIVILEGESANDOTHERAFFILIATIONSQn3 = answers.ElementAt(4).ProviderDisclousreAnswer;
            //    profileData.EDUCATIONTRAININGANDBOARDCERTIFICATIONQn1 = answers.ElementAt(5).ProviderDisclousreAnswer;
            //    profileData.EDUCATIONTRAININGANDBOARDCERTIFICATIONQn2 = answers.ElementAt(6).ProviderDisclousreAnswer;
            //    profileData.EDUCATIONTRAININGANDBOARDCERTIFICATIONQn3 = answers.ElementAt(7).ProviderDisclousreAnswer;
            //    profileData.EDUCATIONTRAININGANDBOARDCERTIFICATIONQn4 = answers.ElementAt(8).ProviderDisclousreAnswer;
            //    profileData.DEAORSTATECONTROLLEDSUBSTANCEREGISTRATIONQn1 = answers.ElementAt(9).ProviderDisclousreAnswer;
            //    profileData.MEDICAREMEDICAIDOROTHERGOVERNMENTALPROGRAMPARTICIPATIONQn1 = answers.ElementAt(10).ProviderDisclousreAnswer;
            //    profileData.OTHERSANCTIONSORINVESTIGATIONSQn1 = answers.ElementAt(11).ProviderDisclousreAnswer;
            //    profileData.OTHERSANCTIONSORINVESTIGATIONSQn2 = answers.ElementAt(12).ProviderDisclousreAnswer;
            //    profileData.OTHERSANCTIONSORINVESTIGATIONSQn3 = answers.ElementAt(13).ProviderDisclousreAnswer;
            //    profileData.OTHERSANCTIONSORINVESTIGATIONSQn4 = answers.ElementAt(14).ProviderDisclousreAnswer;
            //    profileData.OTHERSANCTIONSORINVESTIGATIONSQn5 = answers.ElementAt(15).ProviderDisclousreAnswer;
            //    profileData.PROFESSIONALLIABILITYINSURANCEINFORMATIONANDCLAIMSHISTORYQn1 = answers.ElementAt(16).ProviderDisclousreAnswer;
            //    profileData.PROFESSIONALLIABILITYINSURANCEINFORMATIONANDCLAIMSHISTORYQn2 = answers.ElementAt(17).ProviderDisclousreAnswer;
            //    profileData.MalpractiseClaimHistoryQn1 = answers.ElementAt(18).ProviderDisclousreAnswer;
            //    profileData.CRIMINALCIVILHISTORYQn1 = answers.ElementAt(19).ProviderDisclousreAnswer;
            //    profileData.CRIMINALCIVILHISTORYQn2 = answers.ElementAt(20).ProviderDisclousreAnswer;
            //    profileData.CRIMINALCIVILHISTORYQn3 = answers.ElementAt(21).ProviderDisclousreAnswer;
            //    profileData.ABILITYTOPERFORMJOBQn1 = answers.ElementAt(22).ProviderDisclousreAnswer;
            //    profileData.ABILITYTOPERFORMJOBQn2 = answers.ElementAt(23).ProviderDisclousreAnswer;
            //    profileData.ABILITYTOPERFORMJOBQn3 = answers.ElementAt(24).ProviderDisclousreAnswer;
            //    profileData.ABILITYTOPERFORMJOBQn4 = answers.ElementAt(25).ProviderDisclousreAnswer;

            //}

            #endregion

            #endregion

            #region Identification & Licenses

            #region State License Information details

            if (profile.StateLicenses.Count > 0)
            {
                var stateLicense = profile.StateLicenses.Where(s => s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();
                var stateLicenseCount = stateLicense.Count;

                if (stateLicense.ElementAt(stateLicenseCount - 1) != null)
                {
                    if (stateLicense.ElementAt(stateLicenseCount - 1).ProviderType != null)
                    {
                        pmodel.StateLicense_Type1 = stateLicense.ElementAt(stateLicenseCount - 1).ProviderType.Title;
                    }
                    pmodel.StateLicense_Number1 = stateLicense.ElementAt(stateLicenseCount - 1).LicenseNumber;
                    pmodel.StateLicense_ExpirationDate1 = ConvertToDateString(stateLicense.ElementAt(stateLicenseCount - 1).ExpiryDate);
                    pmodel.StateLicense_CurrentIssueDate1 = ConvertToDateString(stateLicense.ElementAt(stateLicenseCount - 1).CurrentIssueDate);
                    pmodel.StateLicense_OriginalIssueDate1 = ConvertToDateString(stateLicense.ElementAt(stateLicenseCount - 1).IssueDate);
                    pmodel.StateLicense_IssuingState1 = stateLicense.ElementAt(stateLicenseCount - 1).IssueState;
                    if (stateLicense.ElementAt(stateLicenseCount - 1).StateLicenseStatus != null)
                    {
                        pmodel.StateLicense_Status1 = stateLicense.ElementAt(stateLicenseCount - 1).StateLicenseStatus.Title.ToString();
                    }

                    if (pmodel.StateLicense_ExpirationDate1 != null)
                    {
                        pmodel.StateLicense_ExpirationDate1_MM = pmodel.StateLicense_ExpirationDate1.Split('-')[0];
                        pmodel.StateLicense_ExpirationDate1_dd = pmodel.StateLicense_ExpirationDate1.Split('-')[1];
                        pmodel.StateLicense_ExpirationDate1_yyyy = pmodel.StateLicense_ExpirationDate1.Split('-')[2];
                    }

                    if (pmodel.StateLicense_CurrentIssueDate1 != null)
                    {
                        pmodel.StateLicense_CurrentIssueDate1_MM = pmodel.StateLicense_CurrentIssueDate1.Split('-')[0];
                        pmodel.StateLicense_CurrentIssueDate1_dd = pmodel.StateLicense_CurrentIssueDate1.Split('-')[1];
                        pmodel.StateLicense_CurrentIssueDate1_yyyy = pmodel.StateLicense_CurrentIssueDate1.Split('-')[2];
                    }

                    if (pmodel.StateLicense_OriginalIssueDate1 != null)
                    {
                        pmodel.StateLicense_OriginalIssueDate1_MM = pmodel.StateLicense_OriginalIssueDate1.Split('-')[0];
                        pmodel.StateLicense_OriginalIssueDate1_dd = pmodel.StateLicense_OriginalIssueDate1.Split('-')[1];
                        pmodel.StateLicense_OriginalIssueDate1_yyyy = pmodel.StateLicense_OriginalIssueDate1.Split('-')[2];
                    }

                }
            }

            #endregion

            #region Federal DEA Information

            if (profile.FederalDEAInformations.Count > 0)
            {
                var federalDEAInformations = profile.FederalDEAInformations.Where(s => s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();
                var federalDEAInformationCount = federalDEAInformations.Count;
                if (federalDEAInformations.ElementAt(federalDEAInformationCount - 1) != null)
                {
                    pmodel.DEA_Number1 = federalDEAInformations.ElementAt(federalDEAInformationCount - 1).DEANumber;
                    pmodel.DEA_RegistrationState1 = federalDEAInformations.ElementAt(federalDEAInformationCount - 1).StateOfReg;
                    pmodel.DEA_IssueDate1 = ConvertToDateString(federalDEAInformations.ElementAt(federalDEAInformationCount - 1).IssueDate);
                    pmodel.DEA_ExpirationDate1 = ConvertToDateString(federalDEAInformations.ElementAt(federalDEAInformationCount - 1).ExpiryDate);
                    pmodel.DEA_IsGoodStanding1 = federalDEAInformations.ElementAt(federalDEAInformationCount - 1).IsInGoodStanding;
                    pmodel.DEA_Certificate1 = federalDEAInformations.ElementAt(federalDEAInformationCount - 1).DEALicenceCertPath;
                }
            }

            #endregion

            #region Medicare and Medicaid Information

            if (profile.MedicareInformations.Count > 0)
            {
                var medicareInfo = profile.MedicareInformations.Where(s => s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();
                var medicareInfoCount = medicareInfo.Count;
                if (medicareInfo.ElementAt(medicareInfoCount - 1) != null)
                {
                    pmodel.Medicare_Number1 = medicareInfo[0].LicenseNumber;
                }
            }

            if (profile.MedicaidInformations.Count > 0)
            {
                var medicaidInfo = profile.MedicaidInformations.Where(s => s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();

                if (medicaidInfo[0] != null)
                {
                    pmodel.MedicaidNumber1 = medicaidInfo[0].LicenseNumber;
                    pmodel.Medicaid_State1 = medicaidInfo[0].State;
                }
            }

            #endregion

            #region CDS Information

            if (profile.CDSCInformations.Count > 0)
            {
                var CDSCInformations = profile.CDSCInformations.Where(s => s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();
                var CDSCInformationsCount = CDSCInformations.Count;
                if (profile.CDSCInformations.ElementAt(CDSCInformationsCount - 1) != null)
                {
                    pmodel.CDS_Number1 = profile.CDSCInformations.ElementAt(CDSCInformationsCount - 1).CertNumber;
                    pmodel.CDS_RegistrationState1 = profile.CDSCInformations.ElementAt(CDSCInformationsCount - 1).State;
                    pmodel.CDS_IssueDate1 = ConvertToDateString(profile.CDSCInformations.ElementAt(CDSCInformationsCount - 1).IssueDate);
                    pmodel.CDS_ExpirationDate1 = ConvertToDateString(profile.CDSCInformations.ElementAt(CDSCInformationsCount - 1).ExpiryDate);
                    pmodel.CDS_Certificate1 = profile.CDSCInformations.ElementAt(CDSCInformationsCount - 1).CDSCCerificatePath;

                }
            }

            #endregion

            #region Other Identification Numbers

            if (profile.OtherIdentificationNumber != null)
            {
                pmodel.NPI_Number = profile.OtherIdentificationNumber.NPINumber;
                pmodel.NPI_Username = profile.OtherIdentificationNumber.NPIUserName;
                pmodel.NPI_Password = profile.OtherIdentificationNumber.NPIPassword;
                pmodel.CAQH_Number = profile.OtherIdentificationNumber.CAQHNumber;
                pmodel.CAQH_Username = profile.OtherIdentificationNumber.CAQHUserName;
                pmodel.CAQH_Password = profile.OtherIdentificationNumber.CAQHPassword;
                pmodel.UPIN_Number = profile.OtherIdentificationNumber.UPINNumber;
                pmodel.USMLE_Number = profile.OtherIdentificationNumber.USMLENumber;
            }


            #endregion            

            #endregion

            #region Education History

            #region Under Graduation / Professional School Details

            if (profile.EducationDetails.Count > 0)
            {
                var educationDetails = profile.EducationDetails.Where(s => s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString() && s.EducationQualificationType == AHC.CD.Entities.MasterData.Enums.EducationQualificationType.UnderGraduate).ToList();
                var educationDetailsCount = educationDetails.Count;

                if (educationDetailsCount > 0 && educationDetails.ElementAt(educationDetailsCount - 1) != null)
                {
                    pmodel.UnderGraduation_Type1 = educationDetails.ElementAt(educationDetailsCount - 1).GraduationType;

                    if (educationDetails.ElementAt(educationDetailsCount - 1).SchoolInformation != null)
                    {
                        pmodel.UnderGraduation_SchoolName1 = educationDetails.ElementAt(educationDetailsCount - 1).SchoolInformation.SchoolName;
                        pmodel.UnderGraduation_EmailId1 = educationDetails.ElementAt(educationDetailsCount - 1).SchoolInformation.Email;
                        pmodel.UnderGraduation_Fax1 = educationDetails.ElementAt(educationDetailsCount - 1).SchoolInformation.Fax;
                        pmodel.UnderGraduation_TelephoneNo1 = educationDetails.ElementAt(educationDetailsCount - 1).SchoolInformation.PhoneNumber;
                        pmodel.UnderGraduation_EmailAddress1 = educationDetails.ElementAt(educationDetailsCount - 1).SchoolInformation.Email;
                        //pmodel.UnderGraduation_Number1 = educationDetails.ElementAt(educationDetailsCount - 1).SchoolInformation.SchoolInformationID.ToString();
                        pmodel.UnderGraduation_Building1 = educationDetails.ElementAt(educationDetailsCount - 1).SchoolInformation.Building;
                        pmodel.UnderGraduation_Street1 = educationDetails.ElementAt(educationDetailsCount - 1).SchoolInformation.Street;
                        pmodel.UnderGraduation_Country1 = educationDetails.ElementAt(educationDetailsCount - 1).SchoolInformation.Country;
                        pmodel.UnderGraduation_State1 = educationDetails.ElementAt(educationDetailsCount - 1).SchoolInformation.State;
                        pmodel.UnderGraduation_County1 = educationDetails.ElementAt(educationDetailsCount - 1).SchoolInformation.County;
                        pmodel.UnderGraduation_City1 = educationDetails.ElementAt(educationDetailsCount - 1).SchoolInformation.City;
                        pmodel.UnderGraduation_ZipCode1 = educationDetails.ElementAt(educationDetailsCount - 1).SchoolInformation.ZipCode;
                    }
                    pmodel.Provider_Highest_Degree = educationDetails.ElementAt(educationDetailsCount - 1).QualificationDegree;
                    pmodel.UnderGraduation_Degree1 = educationDetails.ElementAt(educationDetailsCount - 1).QualificationDegree;
                    pmodel.UnderGraduation_StartDate1 = ConvertToDateString(educationDetails.ElementAt(educationDetailsCount - 1).StartDate);
                    pmodel.UnderGraduation_EndDate1 = ConvertToDateString(educationDetails.ElementAt(educationDetailsCount - 1).EndDate);
                    pmodel.IsUnderGraduatedFromSchool1 = educationDetails.ElementAt(educationDetailsCount - 1).IsUSGraduate;
                    pmodel.UnderGraduation_Certificate1 = educationDetails.ElementAt(educationDetailsCount - 1).CertificatePath;
                }
            }


            #endregion

            #region Graduation Details

            if (profile.EducationDetails.Count > 0)
            {
                var educationDetails = profile.EducationDetails.Where(s => s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString() && s.EducationQualificationType == AHC.CD.Entities.MasterData.Enums.EducationQualificationType.Graduate).ToList();
                var educationDetailsCount = educationDetails.Count;

                if (educationDetailsCount > 0 && educationDetails.ElementAt(educationDetailsCount - 1) != null)
                {
                    if (educationDetails.ElementAt(educationDetailsCount - 1).SchoolInformation != null)
                    {
                        
                        pmodel.Graduation_SchoolName1 = educationDetails.ElementAt(educationDetailsCount - 1).SchoolInformation.SchoolName;
                        pmodel.UnderGraduation_EmailId1 = educationDetails.ElementAt(educationDetailsCount - 1).SchoolInformation.Email;
                        pmodel.Graduation_Fax1 = educationDetails.ElementAt(educationDetailsCount - 1).SchoolInformation.Fax;
                        pmodel.Graduation_TelephoneNumber1 = educationDetails.ElementAt(educationDetailsCount - 1).SchoolInformation.PhoneNumber;
                        pmodel.Graduation_EmailAddress1 = educationDetails.ElementAt(educationDetailsCount - 1).SchoolInformation.Email;
                        pmodel.Graduation_Number1 = educationDetails.ElementAt(educationDetailsCount - 1).SchoolInformation.SchoolInformationID.ToString();
                        pmodel.UnderGraduation_Building1 = educationDetails.ElementAt(educationDetailsCount - 1).SchoolInformation.Building;
                        pmodel.Graduation_Street1 = educationDetails.ElementAt(educationDetailsCount - 1).SchoolInformation.Street;
                        pmodel.Graduation_Country1 = educationDetails.ElementAt(educationDetailsCount - 1).SchoolInformation.Country;
                        pmodel.Graduation_State1 = educationDetails.ElementAt(educationDetailsCount - 1).SchoolInformation.State;
                        pmodel.Graduation_County1 = educationDetails.ElementAt(educationDetailsCount - 1).SchoolInformation.County;
                        pmodel.Graduation_City1 = educationDetails.ElementAt(educationDetailsCount - 1).SchoolInformation.City;
                        pmodel.Graduation_ZipCode1 = educationDetails.ElementAt(educationDetailsCount - 1).SchoolInformation.ZipCode;
                    }

                    pmodel.Provider_Highest_Degree = educationDetails.ElementAt(educationDetailsCount - 1).QualificationDegree;
                    pmodel.Graduation_Degree1 = educationDetails.ElementAt(educationDetailsCount - 1).QualificationDegree;
                    pmodel.Graduation_StartDate1 = ConvertToDateString(educationDetails.ElementAt(educationDetailsCount - 1).StartDate);
                    pmodel.Graduation_EndDate1 = ConvertToDateString(educationDetails.ElementAt(educationDetailsCount - 1).EndDate);
                    pmodel.IsGraduatedFromSchool1 = educationDetails.ElementAt(educationDetailsCount - 1).IsUSGraduate;
                    pmodel.IsUSGraduate1 = educationDetails.ElementAt(educationDetailsCount - 1).IsUSGraduate;
                    pmodel.Graduation_Certificate1 = educationDetails.ElementAt(educationDetailsCount - 1).CertificatePath;
                    
                }
            }

            #endregion

            #region ECFMG Details

            if (profile.ECFMGDetail != null)
            {
                pmodel.ECFMG_Number = profile.ECFMGDetail.ECFMGNumber;
                pmodel.ECFMG_IssueDate = ConvertToDateString(profile.ECFMGDetail.ECFMGIssueDate);
                pmodel.ECFMG_Certificate = profile.ECFMGDetail.ECFMGCertPath;
            }

            #endregion

            #region Residency/Internship/Fellowship details

            if (profile.TrainingDetails.Count > 0)
            {
                var trainingDetails = profile.EducationDetails.ToList();
                var trainingDetailsCount = trainingDetails.Count;
                var residencyInternshipDetails = trainingDetails.Where(s => s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();
                var residencyInternshipDetailsCount = residencyInternshipDetails.Count;


                if (trainingDetailsCount > 0 && trainingDetails.ElementAt(trainingDetailsCount - 1) != null && trainingDetails.ElementAt(trainingDetailsCount - 1).SchoolInformation != null)
                {
                    //pmodel.TrainingProgram_Type1=trainingDetails.ElementAt(trainingDetailsCount - 1).SchoolInformation
                    pmodel.IsTrainingProgramCompleted1 = trainingDetails.ElementAt(trainingDetailsCount - 1).IsCompleted;
                    //pmodel.TrainingProgramCompletedExplaination1=trainingDetails.ElementAt(trainingDetailsCount - 1).InCompleteReason;
                    //pmodel.IsPrimarySecondary1=trainingDetails.ElementAt(trainingDetailsCount - 1);
                    pmodel.TrainingProgram_SchoolName1 = trainingDetails.ElementAt(trainingDetailsCount - 1).SchoolInformation.SchoolName;
                    pmodel.TrainingProgram_PrimaryPracticingDegree1 = trainingDetails.ElementAt(trainingDetailsCount - 1).QualificationDegree;
                    pmodel.TrainingProgram_Degree1 = trainingDetails.ElementAt(trainingDetailsCount - 1).QualificationDegree;
                    pmodel.TrainingProgram_Fax1 = trainingDetails.ElementAt(trainingDetailsCount - 1).SchoolInformation.Fax;
                    pmodel.TrainingProgram_TelephoneNumber1 = trainingDetails.ElementAt(trainingDetailsCount - 1).SchoolInformation.PhoneNumber;
                    pmodel.TrainingProgram_StartDate1 = ConvertToDateString(trainingDetails.ElementAt(trainingDetailsCount - 1).StartDate);
                    pmodel.TrainingProgram_EndDate1 = ConvertToDateString(trainingDetails.ElementAt(trainingDetailsCount - 1).EndDate);
                    pmodel.TrainingProgram_Street1 = trainingDetails.ElementAt(trainingDetailsCount - 1).SchoolInformation.Street;
                    pmodel.TrainingProgram_Country1 = trainingDetails.ElementAt(trainingDetailsCount - 1).SchoolInformation.Country;
                    pmodel.TrainingProgram_State1 = trainingDetails.ElementAt(trainingDetailsCount - 1).SchoolInformation.State;
                    pmodel.TrainingProgram_County1 = trainingDetails.ElementAt(trainingDetailsCount - 1).SchoolInformation.County;
                    pmodel.TrainingProgram_City1 = trainingDetails.ElementAt(trainingDetailsCount - 1).SchoolInformation.City;
                    pmodel.TrainingProgram_ZipCode1 = trainingDetails.ElementAt(trainingDetailsCount - 1).SchoolInformation.ZipCode;
                    pmodel.TrainingProgram_Certificate1 = trainingDetails.ElementAt(trainingDetailsCount - 1).CertificatePath;
                }
            }

            #endregion

            #endregion

            #region Hospital Pivilages


            if (profile.HospitalPrivilegeInformation != null && profile.HospitalPrivilegeInformation.HospitalPrivilegeDetails.Count > 0)
            {

                var primaryHospitalInfo = profile.HospitalPrivilegeInformation.HospitalPrivilegeDetails.FirstOrDefault(p => p.Preference == AHC.CD.Entities.MasterData.Enums.PreferenceType.Primary.ToString()
                                && p.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString());

                var secondaryHospitalInfo = profile.HospitalPrivilegeInformation.HospitalPrivilegeDetails.Where(p => p.Preference == AHC.CD.Entities.MasterData.Enums.PreferenceType.Secondary.ToString()
                                && p.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();

                var secondaryHospitalCount = secondaryHospitalInfo.Count;

                if (primaryHospitalInfo != null)
                {
                    if (primaryHospitalInfo != null && primaryHospitalInfo.Hospital != null)
                    {
                        pmodel.HospitalPrivilege_PrimaryHospitalName = primaryHospitalInfo.Hospital.HospitalName;
                    }
                    if (secondaryHospitalInfo.Count > 0)
                    {
                        if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1) != null && secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).Hospital != null )
                        {
                            pmodel.HospitalPrivilege_HospitalName1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).Hospital.HospitalName;

                            pmodel.HospitalPrivilege_StartDate1 = ConvertToDateString(secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).AffilicationStartDate);
                            pmodel.HospitalPrivilege_HospitalStatus1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).Hospital.Status;
                            pmodel.HospitalPrivilege_EndDate1 = ConvertToDateString(secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).AffiliationEndDate);
                            if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).HospitalContactInfo != null)
                            {
                                pmodel.HospitalPrivilege_Suite1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).HospitalContactInfo.LocationName;
                                pmodel.HospitalPrivilege_State1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).HospitalContactInfo.State;
                                pmodel.HospitalPrivilege_City1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).HospitalContactInfo.City;
                                pmodel.HospitalPrivilege_ZipCode1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).HospitalContactInfo.ZipCode;
                                pmodel.HospitalPrivilege_County1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).HospitalContactInfo.Country;
                                pmodel.HospitalPrivilege_Phone1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).HospitalContactInfo.PhoneNumber;
                                pmodel.HospitalPrivilege_Fax1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).HospitalContactInfo.Fax;
                                pmodel.HospitalPrivilege_Email1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).HospitalContactInfo.Email;
                                pmodel.HospitalPrivilege_County1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).HospitalContactInfo.County;
                            }
                            if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).StaffCategory != null)
                            {
                                pmodel.HospitalPrivilege_StaffCategory1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).StaffCategory.Title;
                            }
                            pmodel.HospitalPrivilege_Number1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).Hospital.HospitalID.ToString();
                            if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).Specialty!=null)
                            {
                                pmodel.HospitalPrivilege_Specialty1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).Specialty.Name;
                            }
                            pmodel.HospitalPrivilege_AffiliationStartDate1 = ConvertToDateString(secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).AffilicationStartDate);
                            pmodel.HospitalPrivilege_AffiliationEndDate1 = ConvertToDateString(secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).AffiliationEndDate);
                            pmodel.HospitalPrivilege_TerminatedAffiliationExplanation1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).Hospital.HospitalName;
                            pmodel.HospitalPrivilege_Department1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).DepartmentName;
                            if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).HospitalContactPerson != null)
                            {
                                pmodel.HospitalPrivilege_ContactPerson1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).HospitalContactPerson.ContactPersonName;
                            }

                        }
                    }
                }
                else
                {
                    if (secondaryHospitalInfo.Count > 0)
                    {
                        if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1) != null && secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).Hospital != null)
                        {
                            pmodel.HospitalPrivilege_PrimaryHospitalName = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).Hospital.HospitalName;
                        }
                    }
                    if (secondaryHospitalInfo.Count > 1)
                    {
                        if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2) != null && secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).Hospital != null)
                        {
                            pmodel.HospitalPrivilege_HospitalName1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).Hospital.HospitalName;
                        }
                    }
                }

            }


            #endregion

            #region Professional Liability

            if (profile.ProfessionalLiabilityInfoes.Count > 0)
            {
                var professionalLiabilities = profile.ProfessionalLiabilityInfoes.Where(s => s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();
                var professionalLiabilitiesCount = professionalLiabilities.Count;

                if (professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1) != null)
                {
                    if (professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).InsuranceCarrier != null)
                        pmodel.ProfLiability_CarrierName1 = professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).InsuranceCarrier.Name;

                    pmodel.ProfLiability_IsSelfInsured1 = professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).SelfInsured;
                    pmodel.ProfLiability_PolicyNumber1 = professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).PolicyNumber;
                    pmodel.ProfLiability_OriginalEffectiveDate1 = ConvertToDateString(professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).OriginalEffectiveDate);
                    pmodel.ProfLiability_EffectiveDate1 = ConvertToDateString(professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).EffectiveDate);
                    pmodel.ProfLiability_ExpiryDate1 = ConvertToDateString(professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).ExpirationDate);
                    pmodel.ProfLiability_InsuranceDocument1 = professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).InsuranceCertificatePath;
                    //pmodel.ProfLiability_Number1 = professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).ProfessionalLiabilityInfoID.ToString();
                    pmodel.ProfLiability_Phone1 = professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).PhoneNumber;
                    pmodel.ProfLiability_AggregateCoverageAmount1 = professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).AmountOfCoverageAggregate.ToString();
                    pmodel.ProfLiability_CoverageAmountPerOccurrence1 = professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).AmountOfCoveragePerOccurance.ToString();

                    if (professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).InsuranceCarrierAddress != null)
                    {
                        pmodel.ProfLiability_Country1 = professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).InsuranceCarrierAddress.Country;
                        pmodel.ProfLiability_State1 = professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).InsuranceCarrierAddress.State;
                        pmodel.ProfLiability_County1 = professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).InsuranceCarrierAddress.County;
                        pmodel.ProfLiability_City1 = professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).InsuranceCarrierAddress.City;
                        pmodel.ProfLiability_Street1 = professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).InsuranceCarrierAddress.Street;
                        pmodel.ProfLiability_ZipCode1 = professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).InsuranceCarrierAddress.ZipCode;
                        pmodel.ProfLiability_Suite1 = professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).InsuranceCarrierAddress.LocationName;
                    }
                }             
                

            }

            #endregion

            #region Organization Info/Contract Info

            if (profile.ContractInfoes.Count > 0)
            {
                var contractCount = profile.ContractInfoes.Count;
                if (profile.ContractInfoes.ElementAt(contractCount - 1).ContractGroupInfoes.Count > 0)
                {
                    pmodel.ProviderJoiningDate1 = ConvertToDateString(profile.ContractInfoes.ElementAt(contractCount - 1).JoiningDate);
                    if (pmodel.ProviderJoiningDate1 != null)
                    {
                        pmodel.ProviderJoiningDate1_MM = pmodel.ProviderJoiningDate1.Split('-')[0];
                        pmodel.ProviderJoiningDate1_dd = pmodel.ProviderJoiningDate1.Split('-')[1];
                        pmodel.ProviderJoiningDate1_yyyy = pmodel.ProviderJoiningDate1.Split('-')[2];
                    }
                    var contractGroupCount = profile.ContractInfoes.ElementAt(contractCount - 1).ContractGroupInfoes.Count;
                    if (profile.ContractInfoes.ElementAt(contractCount - 1).ContractGroupInfoes.ElementAt(contractGroupCount - 1).PracticingGroup != null && profile.ContractInfoes.ElementAt(contractCount - 1).ContractGroupInfoes.ElementAt(contractGroupCount - 1).PracticingGroup.Group != null)
                    {
                        pmodel.ProviderGroupName1 = profile.ContractInfoes.ElementAt(contractCount - 1).ContractGroupInfoes.ElementAt(contractGroupCount - 1).PracticingGroup.Group.Name;
                        
                    }
                }

            }

            #endregion

            string pdfFileName = readXml(pmodel, templateName);

            return pdfFileName;
        }       

        public string CombinePdfs(int profileID, List<string> pdflist)
        {
            profileId = profileID;
            PersonalDetail personalInfo = GetProviderData(profileID);

            string date = DateTime.Now.ToString("MM-dd-yyyy");

            string timeHour = DateTime.Now.Hour.ToString();
            string timeMin = DateTime.Now.Minute.ToString();
            string timeSec = DateTime.Now.Second.ToString();

            string outputFileName = personalInfo.FirstName + personalInfo.LastName + "_" + "GeneratedPackage" + "_Date" + date + "_Hour" + timeHour + "_Min" + timeMin + "_Sec" + timeSec + ".pdf";
            //string outputFileName = "GeneratedPackage" + "_" + date + ".pdf";
            string generatedPdfPath = HttpContext.Current.Request.MapPath("~/ApplicationDocument/GeneratedPackagePdf/" + outputFileName);
            //string tempFilePath = HttpContext.Current.Server.MapPath("~/ApplicationDocument/TempGeneratedPdf");
            
            // step 1: creation of a document-object
            iTextSharp.text.Document document = new iTextSharp.text.Document(); ;
            // step 2: we create a writer that listens to the document
            PdfCopy writer = new PdfCopy(document, new FileStream(generatedPdfPath, FileMode.Create));
            if (writer == null)
            {
                return null;
            }
            // step 3: we open the document
            document.Open();


            //check for a NonPDF image file and convert to pdf first before use
            for (int i = 0; i < pdflist.Count; i++)
            {
                PdfReader reader = null;
                try
                {
                    if (pdflist[i] != "" && pdflist[i].Contains(".pdf"))
                    {
                        reader = new PdfReader(HttpContext.Current.Request.MapPath(pdflist[i]));                       

                    }
                    else
                    {
                        iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(HttpContext.Current.Request.MapPath(pdflist[i]));
                        FileStream fs = new FileStream(Path.GetFullPath(HttpContext.Current.Request.MapPath(pdflist[i])) + Path.GetFileNameWithoutExtension(HttpContext.Current.Request.MapPath(pdflist[i])) + ".pdf", FileMode.Create, FileAccess.Write, FileShare.None);

                        iTextSharp.text.Document doc = new iTextSharp.text.Document(image);
                        PdfWriter writerPdf = PdfWriter.GetInstance(doc, fs);

                        doc.Open();
                        doc.NewPage();
                        image.SetAbsolutePosition(0, 0);
                        writerPdf.DirectContent.AddImage(image);
                        writerPdf.ResetPageCount();
                        doc.Close();
                        pdflist[i] = Path.GetFullPath(HttpContext.Current.Server.MapPath(pdflist[i])) + Path.GetFileNameWithoutExtension(HttpContext.Current.Server.MapPath(pdflist[i])) + ".pdf";
                        //pdflist[i] = Path.GetFileNameWithoutExtension(HttpContext.Current.Request.MapPath(pdflist[i])) + ".pdf";

                        //string newFile = HttpContext.Current.Server.MapPath("~/Documents/OtherDocument/" + pdflist[i]);
                        reader = new PdfReader(pdflist[i]);
                                              
                    }

                    try
                    {
                    // we create a reader for a certain document
                    reader.ConsolidateNamedDestinations();
                    // step 4: we add content
                    for (int j = 1; j <= reader.NumberOfPages; j++)
                    {
                        PdfImportedPage page = writer.GetImportedPage(reader, j);
                        writer.AddPage(page);
                    }
                    PRAcroForm form = reader.AcroForm;
                    if (form != null)
                    {
                        writer.CopyAcroForm(reader);
                    }
                        reader.Close();
                    }
                    catch (Exception e)
                    { }
                    finally
                    {
                    reader.Close();
                }
                    
                }
                catch
                {

                    throw;
                    
                }


            }

            writer.Close();
            document.Close();
            SaveGeneratedPackage(profileID, "Generated Package", "\\ApplicationDocument\\GeneratedPackagePdf\\" + outputFileName);
            return outputFileName;

        }


        public List<PackageGeneratorBusinessModel> GenerateBulkPackage(List<int> ProfileIDs, List<string> GenricList)
        {
            var profileRepo = uow.GetGenericRepository<Profile>();
            List<PackageGeneratorBusinessModel> bulkPackage = new List<PackageGeneratorBusinessModel>();

            foreach (var id in ProfileIDs)
            {
                if (id != 0)
                {
                    var documents = profileRepo.Find(p => p.ProfileID == id, "ProfileDocuments").ProfileDocuments;
                    List<string> pdfList = new List<string>();
                    foreach (var doc in documents)
                    {
                        if (doc != null)
                        {
                            foreach (var list in GenricList)
                            {
                                if (list != null && doc.Title == list)
                                {
                                    pdfList.Add(doc.DocPath);
                                }
                            }
                        }

                    }

                    string GeneratedPackagePath = CombinePdfs(id, pdfList);
                    PackageGeneratorBusinessModel pacakage = new PackageGeneratorBusinessModel();
                    pacakage.ProfileID = id;
                    pacakage.PackageFilePath = GeneratedPackagePath;
                    bulkPackage.Add(pacakage);
                }                

            }

            return bulkPackage;
        }

        public List<PlanFormGenerationBusinessModel> GenerateBulkForm(List<int> ProfileIDs, List<string> templateNames)
        {
           
            List<PlanFormGenerationBusinessModel> bulkPlan = new List<PlanFormGenerationBusinessModel>();

            foreach (var id in ProfileIDs)
            {
                if (id != 0)
                {
                    PlanFormGenerationBusinessModel formGeneration = new PlanFormGenerationBusinessModel();
                    formGeneration.ProfileID = id;
                    formGeneration.GeneratedFilePaths = new List<string>();
                    foreach (var template in templateNames)
                    {
                        if (template != null)
                        {   
                         
                            string filePath = "\\ApplicationDocument\\GeneratedTemplatePdf\\"+GeneratePlanFormPDF(id, template);
                            formGeneration.GeneratedFilePaths.Add(filePath);
                        }
                    }

                    bulkPlan.Add(formGeneration);
                }

            }

            return bulkPlan;
        }
        

        private string ConvertToDateString(DateTime? date)
        {
            try
            {
                if (date != null)
                {
                    string format = "MM-dd-yyyy";
                    System.Globalization.DateTimeFormatInfo dti = new System.Globalization.DateTimeFormatInfo();

                    var stringDate = Convert.ToString(date);
                    DateTime convertedDate = Convert.ToDateTime(stringDate).Date;
                    return convertedDate.ToString(format, dti);
                }
                else
                {
                    return null;
                }
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new PDFProfileDataGeneratorManagerException(ExceptionMessage.PROFILE_DATA_DATE_TO_STRING_CONVERSION_EXCEPTION, ex);
            }


        }
    }
}
