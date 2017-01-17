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
using AHC.CD.Entities;
using AHC.CD.Entities.PackageGenerate;
using AHC.CD.Entities.MasterProfile.PracticeLocation;
using System.Collections;
using AHC.CD.Resources.Document;
using AHC.CD.Business.Profiles;
using AHC.CD.Exceptions.Profiles;
using AHC.CD.Business.BusinessModels.WelcomeLetter;
using AHC.CD.Entities.Credentialing.LoadingInformation;
using AHC.CD.Entities.Credentialing.Loading;
using System.Globalization;
using AHC.CD.Entities.Credentialing.AppointmentInformation;
using AHC.CD.Data.ADO.FormGeneration;
using AHC.CD.Data.ADO.DTO;
using AHC.CD.Resources.DatabaseQueries;
using AHC.UtilityService;
using AHC.CD.Data.ADO.DTO.FormDTO;
using Itenso.TimePeriod;

namespace AHC.CD.Business.PdfGeneration
{
    internal class PdfMappingManager : IPdfMappingManager
    {

        private IProfileRepository profileRepository = null;
        private IFormGeneration formRepository = null;
        private IUnitOfWork uow = null;
        IDocumentsManager documentManager = null;
        private ProfileDocumentManager profileDocumentManager = null;

        readonly static Dictionary<string, string> formQueryList = null;

        public PdfMappingManager(IUnitOfWork uow, IDocumentsManager documentManager)
        {
            this.profileRepository = uow.GetProfileRepository();
            this.formRepository = new FormGeneration();
            this.uow = uow;
            this.documentManager = documentManager;
            profileDocumentManager = new ProfileDocumentManager(profileRepository, documentManager);
        }
        static PdfMappingManager()
        {
            formQueryList = new Dictionary<string, string> { 
                                                         {"GHI_HOSPITALIST_FORM",AdoQueries.GHI_Hospitalist_Query},
                                                         {"GHI_Provider_Form",AdoQueries.GHI_Provider_Query},
                                                         {"AETNACOVENTRYTemplate",AdoQueries.aetna_Query},
                                                         {"ATPL2016",AdoQueries.atpl_Query},
                                                         {"TRICARE_PROVIDER_APPLICATION",AdoQueries.Tricare_Provider_Query},
                                                         {"TRICARE_PROVIDER_APPLICATION_SOUTH",AdoQueries.Tricare_Provider_Query},
                                                         {"Letter_of_Intent",AdoQueries.Letter_of_Intent_QUERY},
                                                         {"TRICARE_PA_APPLICATION",AdoQueries.Tricare_Pa_Query},
                                                         {"TRICARE_PA_APPLICATION_SOUTH",AdoQueries.Tricare_Pa_Query},
                                                         {"ALLIED_CREDENTIALING_APPLICATION_ACCESS2",AdoQueries.alliedcred2_Query},
                                                         {"ALLIED_CREDENTIALING_APPLICATION_ACCESS",AdoQueries.alliedcred_Query},
                                                         {"Ultimate_Credentialing_Application",AdoQueries.Ultimate_Cred_Query},
                                                         {"Ultimate_Re-Credentialing_Application",AdoQueries.Ultimate_ReCred_Query},
                                                         {"BCBS_PAYMENT_AUTH_FORM",AdoQueries.bcbs_auth_Query},
                                                         {"BCBS_PROVIDER_UPDATE_FORM",AdoQueries.bcbs_update_Query},
                                                         {"BCBS_PROVIDER_REGISTRATION_FORM",AdoQueries.bcbs_reg_Query},
                                                         {"MEDICAID_GROUP_MEMBERSHIP_AUTHORIZATION_FORM",AdoQueries.Medicaid_Group_membership_Query},
                                                         {"TRICARE_ARNP_APPLICATION_FORM",AdoQueries.Tricare_Arnp_Query},
                                                         {"TRICARE_ARNP_APPLICATION_FORM_SOUTH",AdoQueries.Tricare_Arnp_Query},
                                                         {"FL_INSURANCE_PROVIDER_ATTESTATION",AdoQueries.FL_INSURENCE_PROVIDER_ATTESTATION_Query},
                                                         {"Freedom_Optimum_IPA_Enrollment_Provider_PCP",AdoQueries.Freedom_IPA_Query},
                                                         {"Freedom_Optimum_Specialist_Package",AdoQueries.Freedom_optimum_specialist_Query},
                                                         {"Humana_IPA_New_PCP_Package",AdoQueries.Humana_IPA_Query},
                                                         {"Humana_Specialist_New_Provider",AdoQueries.Humana_Specialist_Query},
                                                         {"FL_HOSPITAL_ADMIT_ATTESTATION",AdoQueries.FL_Hospital_Admit_Query},
                                                         {"FL_3000_PCP_Attestation_of_Patient_Load_2016_Global",AdoQueries.fl_3000_Query},
                                                         {"WELLCARE_MIDLEVEL_FORMS",AdoQueries.Wellcare_Midlevel_Query},
                                                         {"Admitting_Arrangement_Form",AdoQueries.Admmitting_Arrangement_Query},
                                                         {"ATTESTATION_OF_SITE_VISIT",AdoQueries.AtestationofSiteVisited_Query},
                                                         {"AHC_Provider_Profile_for_Wellcare",AdoQueries.AHC_Provider_profile_Query},
                                                         {"A2HC_Provider_Profile_for_Wellcare",AdoQueries.A2HC_Provider_profile_Query},
                                                         {"LOI_Template_2016",AdoQueries.LOI_QUERY},
                                                         {"Tricare_Prime_Credentialing_Application",AdoQueries.Tricare_Prime_Credentialing_Application},
                                                         {"Authorization_Attestation_and_Release",AdoQueries.Standard_Authorisation_and_Atestation_QUERY},
                                                         {"TRICARE_CLINICAL_PSYCHOLOGIST_PROVIDER_APPLICATION",AdoQueries.Clinical_psychologist_QUERY},
                                                         {"TRICARE_CLINICAL_SOCIAL_WORKER_PROVIDER_APPLICATION",AdoQueries.Clinical_social_QUERY},
                                                         {"TRICARE_PHYSICIAN_DENTIST_PROVIDER_APPLICATION",AdoQueries.Physical_Dentist_QUERY},
                                                         {"TRICARE_PHYSICIAN_ASSISTANT_PROVIDER_APPLICATION",AdoQueries.physical_Assistant_provider_QUERY},
                                                         {"TRICARE_RN_LPN_NP_PROVIDER_APPLICATION",AdoQueries.RL_LNP_NP_QUERY},

                                                         {"NON-NETWORK_TRICARE_PROVIDER_FILE_GROUP_APPLICATION",AdoQueries.NON_NETWORK_TRICARE_PROVIDER_FILE_GROUP_APPLICATION_QUERY},

                                                         {"ELECTRONIC_FUNDS_TRANSFER_AUTHORIZATION_AGREEMENT",AdoQueries.ELECTRONIC_FUNDS_TRANSFER_AUTHORIZATION_AGREEMENT_Query},
                                                         {"CAREFIRST_CAQH_FORM",AdoQueries.CAQH_QUERY},
                                                         {"NPI_REGISTRATION_FORM",AdoQueries.NPI_Registration_QUERY},
                                                         {"CAREFIRST_PRACTICE_QUESTIONNAIRE",AdoQueries.Practice_Questionaire_Query},

                                                         {"CONFIDENTIAL_PROTECTED_PEER_REVIEW_DOCUMENT",AdoQueries.PARALLON_PEER_ReferenceLetter_Query},
                                                         {"HCA-REQUEST_FOR_CONSIDERATION",AdoQueries.HCA_REQUEST_Consideration_Query},
                                                         {"OHH_REFLEX_TESTING_ACKNOWLEDGEMENT",AdoQueries.OHH_REFLEX_TESTING_ACKNOWLEDGEMENT_Query},
                                                         {"PROVIDER_SIGNATURE_STATEMENT",AdoQueries.PROVIDER_SIGNATURE_STATEMENT_Query},
                                                         {"LARGO_MEDICAL_CENTER_REFLEX_TESTING_ACKNOWLEDGEMENT",AdoQueries.LARGO_REFLEX_TESTING_ACKNOWLEDGEMENT_Query},
                                                         {"LARGO_MEDICAL_CENTER_MEDICAL_STAFF_COVERAGE_AGREEMENT",AdoQueries.LARGO_MEDICAL_CENTER_MEDICAL_STAFF_COVERAGE_AGREEMENT_Query},
                                                         {"NORTHSIDE_HOSPITAL_CODE_OF_CONDUCT_AGREEMENT",AdoQueries.NORTHSIDE_CODE_OF_CONDUCT_AGREEMENT_Query},
                                                         {"NORTHSIDE_HOSPITAL_HAND_HYGIENE",AdoQueries.NORTHSIDE_HOSPITAL_HAND_HYGIENE_Query},
                                                         {"NORTHSIDE_HOSPITAL_REFLEX_TESTING_ACKNOWLEDGEMENT",AdoQueries.NORTHSIDE_REFLEX_TESTING_ACKNOWLEDGEMENT_Query},
                                                         {"HCA-PINELLAS_MARKET_BYLAWS_AGREEMENT",AdoQueries.HCA_PINELLAS_MARKET_BYLAWS_AGREEMENT_Query},
                                                         {"ST_PETE_GENERAL_HOSPITAL_REFLEX_TESTING_ACKNOWLEDGEMENT",AdoQueries.SPGH_REFLEX_TESTING_ACKNOWLEDGEMENT_Query} ,

                                                         {"NORTHSIDE_HOSPITAL_ACKNOWLEDGMENT_CARD",AdoQueries.NORTHSIDE_HOSPITAL_ACKNOWLEDGMENT_CARD_Query},
                                                         {"BAYFRONT-ALLIED_PRE_APPLICATION",AdoQueries.BAYFRONT_ALLIED_PRE_APPLICATION_Query},
                                                         {"BAYFRONT-PHYSICIAN_PRE_APPLICATION",AdoQueries.BAYFRONT_PHYSICIAN_PRE_APPLICATION_Query},
                                                         {"FLORIDA_FINANCIAL_RESPONSIBILITY_FORM",AdoQueries.FLORIDA_FINANCIAL_RESPONSIBILITY_FORM_Query},


                                                         {"BCBS_NC_RELEASE_ATTESTATION_STATEMENT",AdoQueries.BCBS_NC_RELEASE_ATTESTATION_STATEMENT_Query},
                                                         {"North_Carolina_Coventry_Uniform_Credentialing_Application",AdoQueries.North_Carolina_Coventry_Uniform_Credentialing_Application_Query},
                                                         {"FIRST_CAROLINA_CARE_PROVIDER_INFORMATION_CHANGE_FORM",AdoQueries.FIRST_CAROLINA_CARE_PROVIDER_INFORMATION_CHANGE_FORM_Query},
                                                         {"FEDERAL_COMMUNICATIONS_COMMISSION_ATTESTATION_STATEMENT",AdoQueries.FEDERAL_COMMUNICATIONS_COMMISSION_ATTESTATION_STATEMENT_Query},
                                                         {"BMMS_CompCare_Provider_Network_Initial_Provider_Application",AdoQueries.BMMS_CompCare_Provider_Network_Initial_Provider_Application},
                                                         {"CAQH_Pro_View_Application",AdoQueries.CAQH_Pro_View_Application},
                                                         {"Group_LOI",AdoQueries.Group_LOI}                                                         
            };
        }
        string pname = null;
        int profileId;

        private PersonalDetail GetProviderData(int profileID)
        {
            var profileRepo = uow.GetGenericRepository<Profile>();
            var personalInfo = profileRepo.Find(p => p.ProfileID == profileID).PersonalDetail;

            return personalInfo;
        }

        private async Task<Profile> GetProfileObject(int profileID)
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
                    "ProfessionalWorkExperiences.ProviderType",

                    //Custom Field
                    "CustomFieldTransaction.CustomFieldTransactionDatas",
                    "CustomFieldTransaction.CustomFieldTransactionDatas.CustomField"
                };
                var profileRepo = uow.GetGenericRepository<Profile>();
                Profile profile = await profileRepo.FindAsync(s => s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString() && s.ProfileID == profileID, includeProperties);
                //var JsonSerizeData = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(profile);
                //System.Diagnostics.Debug.WriteLine(JsonSerizeData);
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

        //Generate Pdf for Welcome Letter
        #region WelcomeLetter

        private WelcomeLetterBusinessModel SaveGeneratedPDFforWelcomeLetter(int profileId, string documentTitle, string path, int credLogId, WelcomeLetterBusinessModel welcomeletter)
        {
            OtherDocument otherDocument = new OtherDocument();
            try
            {
                var credRepository = uow.GetGenericRepository<CredentialingLog>();
                CredentialingLog res = credRepository.Find(x => x.CredentialingLogID == credLogId, "CredentialingAppointmentDetail");
                if (res.CredentialingAppointmentDetail == null)
                {
                    res.CredentialingAppointmentDetail = new CredentialingAppointmentDetail();
                }
                res.CredentialingAppointmentDetail.WelcomeLetterPath = path;
                res.CredentialingAppointmentDetail.WelcomeLetterPreparedDate = date;
                res.CredentialingAppointmentDetail.ServiceCommencingDate = welcomeletter.ServiceCommencingDate;
                credRepository.Update(res);
                credRepository.Save();
                welcomeletter.WelcomeLetterPreparedDate = date.ToShortDateString();
                welcomeletter.WelcomeLetterPath = path;
                return welcomeletter;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private WelcomeLetterBusinessModel CreatePDFforWelcomeLetter(Dictionary<string, string> dataObj, string pdfName, string templateName, int credLogId, WelcomeLetterBusinessModel welcomeletter)
        {

            try
            {
                string pdfTemplate = HttpContext.Current.Server.MapPath("~/ApplicationDocument/WelcomeLetterPdfTemplate/" + templateName + ".pdf");
                //pdfName = pname +"_"+ DateTime.Today.Date;
                string newFile = HttpContext.Current.Server.MapPath("~/ApplicationDocument/WelcomeLetterGeneratedPdf/" + pdfName + ".pdf");

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
                string storedFilePath = "\\ApplicationDocument\\WelcomeLetterGeneratedPdf\\" + pname;
                WelcomeLetterBusinessModel letter = SaveGeneratedPDFforWelcomeLetter(profileId, templateName, storedFilePath, credLogId, welcomeletter);
                return letter;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private WelcomeLetterBusinessModel readXmlForWelcomeLetter(WelcomeLetterBusinessModel pmodel, string templateName, int credLogId)
        {
            try
            {
                Dictionary<string, string> dictionary = new Dictionary<string, string>();

                var xmldoc = new System.Xml.XmlDocument();
                var xmlPath = HttpContext.Current.Server.MapPath("~/ApplicationDocument/WelcomeLetterXml/" + templateName + ".xml");
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

                string fileName = pmodel.ProviderName + "_" + templateName + "_Date" + date + "_Hour" + timeHour + "_Min" + timeMin + "_Sec" + timeSec;

                WelcomeLetterBusinessModel letter = CreatePDFforWelcomeLetter(dictionary, fileName, templateName, credLogId, pmodel);
                return letter;
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// Funtion to get Initial Credentialing Date of a provider for WELLCARE plan
        /// </summary>
        /// <param name="profileId"></param>
        /// <returns></returns>
        public string GetCredentialingInitiationDate(int profileId)
        {
            try
            {
                var credRepo = uow.GetGenericRepository<CredentialingInfo>();
                CredentialingInfo credentialingInfo = credRepo.Find(s => s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString() && s.ProfileID == profileId && s.Plan.PlanName == "WELLCARE");
                return credentialingInfo.InitiationDate.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }
        DateTime date;
        public WelcomeLetterBusinessModel GenerateWelcomeLetterPDF(WelcomeLetterBusinessModel welcomeletterdata, string templateName, string cduserid, int credLogId)
        {
            date = Convert.ToDateTime(welcomeletterdata.WelcomeLetterPreparedDate, CultureInfo.InvariantCulture);
            var res = welcomeletterdata.WelcomeLetterPreparedDate.Split(' ');
            welcomeletterdata.WelcomeLetterPreparedDate = res[0];
            WelcomeLetterBusinessModel letter = readXmlForWelcomeLetter(welcomeletterdata, templateName, credLogId);

            return letter;
        }

        #endregion

        public void AddDocument(int profileId, string documentTitle)
        {
            OtherDocument otherDocument = new OtherDocument();

            try
            {
                string includeProperties = "OtherDocuments";
                var profileRepo = uow.GetGenericRepository<Profile>();
                Profile profile = profileRepo.Find(s => s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString() && s.ProfileID == profileId, includeProperties);

                otherDocument.IsPrivate = true;
                otherDocument.DocumentPath = "\\ApplicationDocument\\GeneratedTemplatePdf\\" + pname;
                otherDocument.Title = documentTitle;
                otherDocument.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;
                otherDocument.DocumentCategoryType = AHC.CD.Entities.MasterData.Enums.DocumentCategoryType.PlanForm;
                profile.OtherDocuments.Add(otherDocument);
                profileRepo.Update(profile);
                profileRepo.Save();

            }
            catch (Exception)
            {
                throw;
            }
        }


        private void SaveGeneratedPDF(int profileId, string documentTitle, string outputFileName, string CDUserId)
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
                otherDocument.ModifiedBy = CDUserId;
                otherDocument.DocumentCategoryType = AHC.CD.Entities.MasterData.Enums.DocumentCategoryType.PlanFormPdf;
                profile.OtherDocuments.Add(otherDocument);
                profileRepo.Update(profile);
                profileRepo.Save();

            }
            catch (Exception)
            {
                throw;
            }
        }

        private void SaveGeneratedPackage(int profileId, string documentTitle, string outputFileName, string CDUserId)
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
                otherDocument.ModifiedBy = CDUserId;
                //otherDocument.DocumentCategoryType = AHC.CD.Entities.MasterData.Enums.DocumentCategoryType.PlanFormPdf;
                profile.OtherDocuments.Add(otherDocument);
                profileRepo.Update(profile);
                profileRepo.Save();

            }
            catch (Exception)
            {
                throw;
            }
        }

        private string CreatePDF(Dictionary<string, string> dataObj, string pdfName, string templateName, string CDUserId)
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
                SaveGeneratedPDF(profileId, templateName, "\\ApplicationDocument\\GeneratedTemplatePdf\\" + pname, CDUserId);
                return pname;
            }
            catch (Exception)
            {
                throw;
            }
        }



        private string readXml(PDFMappingDataBusinessModel pmodel, string templateName, string CDUserId)
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
                                    throw new Exception(PlanVariableName + "  Variable is already added");
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

                string pdfName = CreatePDF(dictionary, fileName, templateName, CDUserId);
                return pdfName;
            }
            catch (Exception)
            {
                throw;
            }

        }


        public async Task<string> GenerateGenericPlanFormPDF(int profileId, string templateName, string UserAuthId)
        {
            this.profileId = profileId;
            try
            {
                List<FormData> formdata = await formRepository.GetData(profileId, formQueryList[templateName]);

                var personaldetails = (from data in formdata
                                       group data by new { data.ProfilePhotoPath, data.Gender, data.MaidenName, data.MaritalStatus, data.SpouseName, data.FirstName, data.LastName, data.MiddleName, data.Suffix, data.NPINumber, data.CAQHNumber, data.ProfileID, data.NationalIDNumber } into personaldata
                                       select new
                                       {
                                           ProfilePhotoPath = personaldata.Key.ProfilePhotoPath,
                                           Gender = personaldata.Key.Gender,
                                           MaidenName = personaldata.Key.MaidenName,
                                           MaritalStatus = personaldata.Key.MaritalStatus,
                                           SpouseName = personaldata.Key.SpouseName,
                                           FirstName = personaldata.Key.FirstName,
                                           LastName = personaldata.Key.LastName,
                                           MiddleName = personaldata.Key.MiddleName,
                                           Suffix = personaldata.Key.Suffix,
                                           ProfileID = personaldata.Key.ProfileID

                                       }).ToList().FirstOrDefault();

                var providerTitleData = (from data in formdata
                                         group data by new { data.ProviderTitle, data.ProviderTitleCode } into titledata
                                         select new
                                         {
                                             ProviderTitle = titledata.Key.ProviderTitle,
                                             ProviderTitleCode = titledata.Key.ProviderTitleCode

                                         }).ToList().LastOrDefault();
                var birthInformation = (from data in formdata
                                        group data by new { data.DateOfBirth, data.StateOfBirth, data.CityOfBirth, data.BirthCertificatePath, data.CountryOfBirth, data.CountyOfBirth } into birthdata
                                        select new
                                        {
                                            DateOfBirth = birthdata.Key.DateOfBirth,
                                            StateOfBirth = birthdata.Key.StateOfBirth,
                                            CityOfBirth = birthdata.Key.CityOfBirth,
                                            BirthCertificatePath = birthdata.Key.BirthCertificatePath,
                                            CountryOfBirth = birthdata.Key.CountryOfBirth,
                                            CountyOfBirth = birthdata.Key.CountyOfBirth
                                        }).ToList().FirstOrDefault();
                var languageInformation = (from data in formdata
                                           group data by new { data.Language, data.ProficiencyIndex } into langugaedata
                                           select new
                                           {
                                               Language = langugaedata.Key.Language,
                                               ProficiencyIndex = langugaedata.Key.ProficiencyIndex

                                           }).ToList();

                var otherLegalNames = (from data in formdata
                                       group data by new
                                       {
                                           data.OtherFirstName,
                                           data.OtherMiddleName,
                                           data.OtherLastName,
                                           data.OtherLegalNameSuffix,
                                           data.OtherlegalNameStatus,
                                           data.OtherLegalName_DocumentPath,
                                           data.OtherLegalName_EndDate,
                                           data.OtherLegalName_StartDate,
                                           data.OtherLegalName_StatusType
                                       } into otherlegalnamedata
                                       select new
                                       {
                                           OtherFirstName = otherlegalnamedata.Key.OtherFirstName,
                                           OtherMiddleName = otherlegalnamedata.Key.OtherMiddleName,
                                           OtherLastName = otherlegalnamedata.Key.OtherLastName,
                                           OtherLegalNameSuffix = otherlegalnamedata.Key.OtherLegalNameSuffix,
                                           OtherlegalNameStatus = otherlegalnamedata.Key.OtherlegalNameStatus,
                                           OtherLegalName_DocumentPath = otherlegalnamedata.Key.OtherLegalName_DocumentPath,
                                           OtherLegalName_EndDate = otherlegalnamedata.Key.OtherLegalName_EndDate,
                                           OtherLegalName_StartDate = otherlegalnamedata.Key.OtherLegalName_StartDate,
                                           OtherLegalName_StatusType = otherlegalnamedata.Key.OtherLegalName_StatusType
                                       }).ToList();
                var homeAddress = (from data in formdata
                                   group data by new
                                   {
                                       data.HomeAddress_City,
                                       data.HomeAddress_Country,
                                       data.HomeAddress_County,
                                       data.HomeAddress_LivingEndDate,
                                       data.HomeAddress_LivingFromDate,
                                       data.HomeAddress_State,
                                       data.HomeAddress_Street,
                                       data.HomeAddress_UnitNumber,
                                       data.HomeAddress_ZipCode,
                                       data.HomeAddress_Status,
                                       data.HomeAddress_Preference,
                                       data.HomeAddress_StatusType
                                   } into homeAddressdata
                                   select new
                                   {
                                       HomeAddress_City = homeAddressdata.Key.HomeAddress_City,
                                       HomeAddress_Country = homeAddressdata.Key.HomeAddress_Country,
                                       HomeAddress_County = homeAddressdata.Key.HomeAddress_County,
                                       HomeAddress_LivingEndDate = homeAddressdata.Key.HomeAddress_LivingEndDate,
                                       HomeAddress_LivingFromDate = homeAddressdata.Key.HomeAddress_LivingFromDate,
                                       HomeAddress_State = homeAddressdata.Key.HomeAddress_State,
                                       HomeAddress_Street = homeAddressdata.Key.HomeAddress_Street,
                                       HomeAddress_UnitNumber = homeAddressdata.Key.HomeAddress_UnitNumber,
                                       HomeAddress_ZipCode = homeAddressdata.Key.HomeAddress_ZipCode,
                                       HomeAddress_Status = homeAddressdata.Key.HomeAddress_Status,
                                       HomeAddress_StatusType = homeAddressdata.Key.HomeAddress_StatusType
                                   }).ToList();

                var contactPhoneDetails = (from data in formdata
                                           group data by new
                                           {
                                               data.Number,
                                               data.PhoneType,
                                               data.PhoneDetails_Status,
                                               data.PhoneDetails_StatusType

                                           } into contactPhoneDetailsdata
                                           select new
                                           {
                                               Number = contactPhoneDetailsdata.Key.Number,
                                               PhoneType = contactPhoneDetailsdata.Key.PhoneType,
                                               PhoneDetails_Status = contactPhoneDetailsdata.Key.PhoneDetails_Status,
                                               PhoneDetails_StatusType = contactPhoneDetailsdata.Key.PhoneDetails_StatusType
                                           }).ToList();
                var contactEmailDetails = (from data in formdata
                                           group data by new
                                           {
                                               data.EmailAddress,

                                               data.EmailAddress_Status,
                                               data.EmailAddress_StatusType

                                           } into contactEmailDetailsdata
                                           select new
                                           {
                                               EmailAddress = contactEmailDetailsdata.Key.EmailAddress,
                                               EmailAddress_Status = contactEmailDetailsdata.Key.EmailAddress_Status,
                                               EmailAddress_StatusType = contactEmailDetailsdata.Key.EmailAddress_StatusType
                                           }).ToList();
                var personalIdentificationDetails = (from data in formdata
                                                     group data by new
                                                     {
                                                         data.DL,

                                                         data.SSN,
                                                         data.SSNCertificatePath,
                                                         data.DLCertificatePath

                                                     } into personalIdentificationDetailsdata
                                                     select new
                                                     {
                                                         DL = personalIdentificationDetailsdata.Key.DL,
                                                         SSN = personalIdentificationDetailsdata.Key.SSN,
                                                         SSNCertificatePath = personalIdentificationDetailsdata.Key.SSNCertificatePath,
                                                         DLCertificatePath = personalIdentificationDetailsdata.Key.DLCertificatePath
                                                     }).ToList().FirstOrDefault();
                var visaDetails = (from data in formdata
                                   group data by new { data.IsResidentOfUSA, data.IsAuthorizedToWorkInUS } into visaDetailsdata
                                   select new
                                   {
                                       IsResidentOfUSA = visaDetailsdata.Key.IsResidentOfUSA,
                                       IsAuthorizedToWorkInUS = visaDetailsdata.Key.IsAuthorizedToWorkInUS

                                   }).ToList().FirstOrDefault();
                var visaInfo = (from data in formdata
                                group data by new
                                {
                                    data.VisaNumber,
                                    data.VisaType_Title,
                                    data.VisaStatus_Title,
                                    data.VisaSponsor,
                                    data.VisaExpirationDate,
                                    data.VisaCertificatePath,
                                    data.GreenCardNumber,
                                    data.GreenCardCertificatePath,
                                    data.NationalIDNumber,
                                    data.NationalIDCertificatePath,
                                    data.CountryOfIssue
                                } into visaInfodata
                                select new
                                {
                                    VisaNumber = visaInfodata.Key.VisaNumber,
                                    VisaType_Title = visaInfodata.Key.VisaType_Title,
                                    VisaStatus_Title = visaInfodata.Key.VisaStatus_Title,
                                    VisaSponsor = visaInfodata.Key.VisaSponsor,
                                    VisaExpirationDate = visaInfodata.Key.VisaExpirationDate,
                                    VisaCertificatePath = visaInfodata.Key.VisaCertificatePath,
                                    GreenCardNumber = visaInfodata.Key.GreenCardNumber,
                                    GreenCardCertificatePath = visaInfodata.Key.GreenCardCertificatePath,
                                    NationalIDNumber = visaInfodata.Key.NationalIDNumber,
                                    NationalIDCertificatePath = visaInfodata.Key.NationalIDCertificatePath,
                                    CountryOfIssue = visaInfodata.Key.CountryOfIssue
                                }).ToList().FirstOrDefault();


                var specialitylist = (from data in formdata
                                      group data by new
                                      {
                                          data.SpecialityName,
                                          data.SpecialityPreference,
                                          data.SpecialityStatus,
                                          data.TaxonomyCode,

                                          data.IsBoardCertified,
                                          data.BoardCertifiedYesNoOption,
                                          data.CertificateNumber,
                                          data.InitialCertificationDate,
                                          data.LastReCerificationDate,
                                          data.ExpirationDate,
                                          data.BoardCertificatePath,
                                          data.SpecialityBoard_Name


                                      } into specialitydata
                                      select new
                                      {
                                          SpecialityName = specialitydata.Key.SpecialityName,
                                          SpecialityPreference = specialitydata.Key.SpecialityPreference,
                                          SpecialityStatus = specialitydata.Key.SpecialityStatus,
                                          TaxonomyCode = specialitydata.Key.TaxonomyCode,
                                          IsBoardCertified = specialitydata.Key.IsBoardCertified,
                                          BoardCertifiedYesNoOption = specialitydata.Key.BoardCertifiedYesNoOption,
                                          CertificateNumber = specialitydata.Key.CertificateNumber,
                                          InitialCertificationDate = specialitydata.Key.InitialCertificationDate,
                                          LastReCerificationDate = specialitydata.Key.LastReCerificationDate,
                                          ExpirationDate = specialitydata.Key.ExpirationDate,
                                          BoardCertificatePath = specialitydata.Key.BoardCertificatePath,
                                          SpecialityBoard_Name = specialitydata.Key.SpecialityBoard_Name

                                      }).ToList();


                var practicelocationlist = (from data in formdata
                                            group data by new
                                            {

                                                data.PracticeLocationDetailID,
                                                data.CurrentlyPracticingAtThisAddress,
                                                data.PracticeLocation_StartDate,
                                                data.PracticeLocation_CorporateName,
                                                data.PracticeLocation_FacilityName,
                                                data.PracticeLocation_OtherPracticeName,
                                                data.PracticeLocation_PrimaryTaxId,
                                                data.Practicelocation_Building,
                                                data.Practicelocation_City,
                                                data.Practicelocation_Country,
                                                data.Practicelocation_County,
                                                data.Practicelocation_IsPrimary,
                                                data.Practicelocation_State,
                                                data.Practicelocation_status,
                                                data.Practicelocation_Street,
                                                data.Practicelocation_ZipCode,
                                                data.Practicelocation_EmailAddress,
                                                data.Practicelocation_Fax,
                                                data.Practicelocation_Telephone,
                                                data.NonEnglishLanguage_Language,
                                                data.NonEnglishLanguage_Status,
                                                data.NonEnglishLanguage_StatusType,
                                                data.FacilityPracticeType_Title,
                                                data.OpenPracticeStatus_MaximumAge,
                                                data.OpenPracticeStatus_MinimumAge,
                                                data.BusinessOfficeManagerOrStaff_Building,
                                                data.BusinessOfficeManagerOrStaff_City,
                                                data.BusinessOfficeManagerOrStaff_Country,
                                                data.BusinessOfficeManagerOrStaff_CountryCodeFax,
                                                data.BusinessOfficeManagerOrStaff_CountryCodeTelephone,
                                                data.BusinessOfficeManagerOrStaff_County,
                                                BusinessOfficeManagerOrStaff_EmailAddress = data.Credentialing_EmailAddress,
                                                data.BusinessOfficeManagerOrStaff_Fax,
                                                data.BusinessOfficeManagerOrStaff_FaxNumber,
                                                data.BusinessOfficeManagerOrStaff_FirstName,
                                                data.BusinessOfficeManagerOrStaff_LastName,
                                                data.BusinessOfficeManagerOrStaff_MiddleName,
                                                data.BusinessOfficeManagerOrStaff_MobileNumber,
                                                data.BusinessOfficeManagerOrStaff_POBoxAddress,
                                                data.BusinessOfficeManagerOrStaff_State,
                                                data.BusinessOfficeManagerOrStaff_Street,
                                                data.BusinessOfficeManagerOrStaff_Telephone,
                                                data.BusinessOfficeManagerOrStaff_ZipCode,
                                                data.BillingContactPerson_POBoxAddress,
                                                data.BillingContactPerson_Building,
                                                data.BillingContactPerson_City,
                                                data.BillingContactPerson_Country,
                                                data.BillingContactPerson_CountryCodeFax,
                                                data.BillingContactPerson_CountryCodeTelephone,
                                                data.BillingContactPerson_County,
                                                data.BillingContactPerson_EmailAddress,
                                                data.BillingContactPerson_Fax,
                                                data.BillingContactPerson_FaxNumber,
                                                data.BillingContactPerson_FirstName,
                                                data.BillingContactPerson_LastName,
                                                data.BillingContactPerson_MiddleName,
                                                data.BillingContactPerson_MobileNumber,
                                                data.BillingContactPerson_State,
                                                data.BillingContactPerson_Street,
                                                data.BillingContactPerson_Telephone,
                                                data.BillingContactPerson_ZipCode,
                                                data.PracticePaymentAndRemittance_Building,
                                                data.PracticePaymentAndRemittance_City,
                                                data.PracticePaymentAndRemittance_Country,
                                                data.PracticePaymentAndRemittance_CountryCodeFax,
                                                data.PracticePaymentAndRemittance_CountryCodeTelephone,
                                                data.PracticePaymentAndRemittance_County,
                                                data.PracticePaymentAndRemittance_EmailAddress,
                                                data.PracticePaymentAndRemittance_Fax,
                                                data.PracticePaymentAndRemittance_FaxNumber,
                                                data.PracticePaymentAndRemittance_FirstName,
                                                data.PracticePaymentAndRemittance_LastName,
                                                data.PracticePaymentAndRemittance_MiddleName,
                                                data.PracticePaymentAndRemittance_MobileNumber,
                                                data.PracticePaymentAndRemittance_POBoxAddress,
                                                data.PracticePaymentAndRemittance_State,
                                                data.PracticePaymentAndRemittance_Street,
                                                data.PracticePaymentAndRemittance_Telephone,
                                                data.PracticePaymentAndRemittance_ZipCode,
                                                data.ElectronicBillingCapability,
                                                data.BillingDepartment,
                                                data.CheckPayableTo,
                                                data.Office,
                                                data.PracticeDailyHour_Day,
                                                data.PracticeDailyHour_StartTime,
                                                data.PracticeDailyHour_EndTime,
                                                data.PracticeProvider_FirstName,
                                                data.PracticeProvider_LastName,
                                                data.PracticeProvider_MiddleName,
                                                data.PracticeProvider_Practice,
                                                data.PracticeProvider_PracticeType,
                                                data.PracticeProvider_Status,
                                                data.PracticeProvider_StatusType,
                                                data.PracticeProviderSpecialty_Name,
                                                data.PracticeProviderSpecialty_Status,
                                                data.PracticeProviderSpecialty_StatusType,
                                                data.PracticeProvider_Telephone,
                                                data.Credentialing_FirstName,
                                                data.Credentialing_MiddleName,
                                                data.Credentialing_LastName,
                                                data.Credentialing_Street,
                                                data.Credentialing_Building,
                                                data.Credentialing_City,
                                                data.Credentialing_County,
                                                data.Credentialing_State,
                                                data.Credentialing_ZipCode,
                                                data.Credentialing_Country,
                                                data.Credentialing_EmailAddress,
                                                data.Credentialing_FaxNumber,
                                                data.Credentialing_Fax,
                                                data.Credentialing_Telephone,
                                                data.Credentialing_MobileNumber,
                                                data.Credentialing_POBoxAddress,
                                                data.Credentialing_CountryCodeTelephone,
                                                data.Credentialing_CountryCodeFax
                                            } into addressdata
                                            select new
                                            {
                                                PracticeLocationDetailID = addressdata.Key.PracticeLocationDetailID,
                                                CurrentlyPracticingAtThisAddress = addressdata.Key.CurrentlyPracticingAtThisAddress,
                                                StartDate = addressdata.Key.PracticeLocation_StartDate,
                                                PracticeLocation_CorporateName = addressdata.Key.PracticeLocation_CorporateName,
                                                PracticeLocation_FacilityName = addressdata.Key.PracticeLocation_FacilityName,
                                                PracticeLocation_OtherPracticeName = addressdata.Key.PracticeLocation_OtherPracticeName,
                                                PrimaryTaxId = addressdata.Key.PracticeLocation_PrimaryTaxId,
                                                EmailAddress = addressdata.Key.Practicelocation_EmailAddress,
                                                Fax = addressdata.Key.Practicelocation_Fax,
                                                Telephone = addressdata.Key.Practicelocation_Telephone,
                                                State = addressdata.Key.Practicelocation_State,
                                                Street = addressdata.Key.Practicelocation_Street,
                                                ZipCode = addressdata.Key.Practicelocation_ZipCode,
                                                Building = addressdata.Key.Practicelocation_Building,
                                                City = addressdata.Key.Practicelocation_City,
                                                Country = addressdata.Key.Practicelocation_Country,
                                                County = addressdata.Key.Practicelocation_County,
                                                IsPrimary = addressdata.Key.Practicelocation_IsPrimary,
                                                Practicelocationstatus = addressdata.Key.Practicelocation_status,
                                                NonEnglishLanguage_Language = addressdata.Key.NonEnglishLanguage_Language,
                                                NonEnglishLanguage_Status = addressdata.Key.NonEnglishLanguage_Status,
                                                NonEnglishLanguage_StatusType = addressdata.Key.NonEnglishLanguage_StatusType,
                                                PracticeType_Title = addressdata.Key.FacilityPracticeType_Title,
                                                MaximumAge = addressdata.Key.OpenPracticeStatus_MaximumAge,
                                                MinimumAge = addressdata.Key.OpenPracticeStatus_MinimumAge,
                                                BusinessOfficeManagerOrStaff_Building = addressdata.Key.BusinessOfficeManagerOrStaff_Building,
                                                BusinessOfficeManagerOrStaff_City = addressdata.Key.BusinessOfficeManagerOrStaff_City,
                                                BusinessOfficeManagerOrStaff_Country = addressdata.Key.BusinessOfficeManagerOrStaff_Country,
                                                BusinessOfficeManagerOrStaff_CountryCodeFax = addressdata.Key.BusinessOfficeManagerOrStaff_CountryCodeFax,
                                                BusinessOfficeManagerOrStaff_CountryCodeTelephone = addressdata.Key.BusinessOfficeManagerOrStaff_CountryCodeTelephone,
                                                BusinessOfficeManagerOrStaff_County = addressdata.Key.BusinessOfficeManagerOrStaff_County,
                                                BusinessOfficeManagerOrStaff_EmailAddress = addressdata.Key.BusinessOfficeManagerOrStaff_EmailAddress,
                                                BusinessOfficeManagerOrStaff_Fax = addressdata.Key.BusinessOfficeManagerOrStaff_Fax,
                                                BusinessOfficeManagerOrStaff_FaxNumber = addressdata.Key.BusinessOfficeManagerOrStaff_FaxNumber,
                                                BusinessOfficeManagerOrStaff_FirstName = addressdata.Key.BusinessOfficeManagerOrStaff_FirstName,
                                                BusinessOfficeManagerOrStaff_LastName = addressdata.Key.BusinessOfficeManagerOrStaff_LastName,
                                                BusinessOfficeManagerOrStaff_MiddleName = addressdata.Key.BusinessOfficeManagerOrStaff_MiddleName,
                                                BusinessOfficeManagerOrStaff_MobileNumber = addressdata.Key.BusinessOfficeManagerOrStaff_MobileNumber,
                                                BusinessOfficeManagerOrStaff_POBoxAddress = addressdata.Key.BusinessOfficeManagerOrStaff_POBoxAddress,
                                                BusinessOfficeManagerOrStaff_State = addressdata.Key.BusinessOfficeManagerOrStaff_State,
                                                BusinessOfficeManagerOrStaff_Street = addressdata.Key.BusinessOfficeManagerOrStaff_Street,
                                                BusinessOfficeManagerOrStaff_Telephone = addressdata.Key.BusinessOfficeManagerOrStaff_Telephone,
                                                BusinessOfficeManagerOrStaff_ZipCode = addressdata.Key.BusinessOfficeManagerOrStaff_ZipCode,
                                                BillingContactPerson_POBoxAddress = addressdata.Key.BillingContactPerson_POBoxAddress,
                                                BillingContactPerson_Building = addressdata.Key.BillingContactPerson_Building,
                                                BillingContactPerson_City = addressdata.Key.BillingContactPerson_City,
                                                BillingContactPerson_Country = addressdata.Key.BillingContactPerson_Country,
                                                BillingContactPerson_CountryCodeFax = addressdata.Key.BillingContactPerson_CountryCodeFax,
                                                BillingContactPerson_CountryCodeTelephone = addressdata.Key.BillingContactPerson_CountryCodeTelephone,
                                                BillingContactPerson_County = addressdata.Key.BillingContactPerson_County,
                                                BillingContactPerson_EmailAddress = addressdata.Key.BillingContactPerson_EmailAddress,
                                                BillingContactPerson_Fax = addressdata.Key.BillingContactPerson_Fax,
                                                BillingContactPerson_FaxNumber = addressdata.Key.BillingContactPerson_FaxNumber,
                                                BillingContactPerson_FirstName = addressdata.Key.BillingContactPerson_FirstName,
                                                BillingContactPerson_LastName = addressdata.Key.BillingContactPerson_LastName,
                                                BillingContactPerson_MiddleName = addressdata.Key.BillingContactPerson_MiddleName,
                                                BillingContactPerson_MobileNumber = addressdata.Key.BillingContactPerson_MobileNumber,
                                                BillingContactPerson_State = addressdata.Key.BillingContactPerson_State,
                                                BillingContactPerson_Street = addressdata.Key.BillingContactPerson_Street,
                                                BillingContactPerson_Telephone = addressdata.Key.BillingContactPerson_Telephone,
                                                BillingContactPerson_ZipCode = addressdata.Key.BillingContactPerson_ZipCode,
                                                PracticePaymentAndRemittance_Building = addressdata.Key.PracticePaymentAndRemittance_Building,
                                                PracticePaymentAndRemittance_City = addressdata.Key.PracticePaymentAndRemittance_City,
                                                PracticePaymentAndRemittance_Country = addressdata.Key.PracticePaymentAndRemittance_Country,
                                                PracticePaymentAndRemittance_CountryCodeFax = addressdata.Key.PracticePaymentAndRemittance_CountryCodeFax,
                                                PracticePaymentAndRemittance_CountryCodeTelephone = addressdata.Key.PracticePaymentAndRemittance_CountryCodeTelephone,
                                                PracticePaymentAndRemittance_County = addressdata.Key.PracticePaymentAndRemittance_County,
                                                PracticePaymentAndRemittance_EmailAddress = addressdata.Key.PracticePaymentAndRemittance_EmailAddress,
                                                PracticePaymentAndRemittance_Fax = addressdata.Key.PracticePaymentAndRemittance_Fax,
                                                PracticePaymentAndRemittance_FaxNumber = addressdata.Key.PracticePaymentAndRemittance_FaxNumber,
                                                PracticePaymentAndRemittance_FirstName = addressdata.Key.PracticePaymentAndRemittance_FirstName,
                                                PracticePaymentAndRemittance_LastName = addressdata.Key.PracticePaymentAndRemittance_LastName,
                                                PracticePaymentAndRemittance_MiddleName = addressdata.Key.PracticePaymentAndRemittance_MiddleName,
                                                PracticePaymentAndRemittance_MobileNumber = addressdata.Key.PracticePaymentAndRemittance_MobileNumber,
                                                PracticePaymentAndRemittance_POBoxAddress = addressdata.Key.PracticePaymentAndRemittance_POBoxAddress,
                                                PracticePaymentAndRemittance_State = addressdata.Key.PracticePaymentAndRemittance_State,
                                                PracticePaymentAndRemittance_Street = addressdata.Key.PracticePaymentAndRemittance_Street,
                                                PracticePaymentAndRemittance_Telephone = addressdata.Key.PracticePaymentAndRemittance_Telephone,
                                                PracticePaymentAndRemittance_ZipCode = addressdata.Key.PracticePaymentAndRemittance_ZipCode,
                                                ElectronicBillingCapability = addressdata.Key.ElectronicBillingCapability,
                                                BillingDepartment = addressdata.Key.BillingDepartment,
                                                CheckPayableTo = addressdata.Key.CheckPayableTo,
                                                Office = addressdata.Key.Office,
                                                PracticeDailyHour_Day = addressdata.Key.PracticeDailyHour_Day,
                                                PracticeDailyHour_StartTime = addressdata.Key.PracticeDailyHour_StartTime,
                                                PracticeDailyHour_EndTime = addressdata.Key.PracticeDailyHour_EndTime,
                                                PracticeProvider_FirstName = addressdata.Key.PracticeProvider_FirstName,
                                                PracticeProvider_LastName = addressdata.Key.PracticeProvider_LastName,
                                                PracticeProvider_MiddleName = addressdata.Key.PracticeProvider_MiddleName,
                                                PracticeProvider_Practice = addressdata.Key.PracticeProvider_Practice,
                                                PracticeProvider_PracticeType = addressdata.Key.PracticeProvider_PracticeType,
                                                PracticeProvider_Status = addressdata.Key.PracticeProvider_Status,
                                                PracticeProvider_StatusType = addressdata.Key.PracticeProvider_StatusType,
                                                PracticeProviderSpecialty_Name = addressdata.Key.PracticeProviderSpecialty_Name,
                                                PracticeProviderSpecialty_Status = addressdata.Key.PracticeProviderSpecialty_Status,
                                                PracticeProviderSpecialty_StatusType = addressdata.Key.PracticeProviderSpecialty_StatusType,
                                                PracticeProvider_Telephone = addressdata.Key.PracticeProvider_Telephone,
                                                CredentialingContact_FirstName = addressdata.Key.Credentialing_FirstName,
                                                CredentialingContact_MiddleName = addressdata.Key.Credentialing_MiddleName,
                                                CredentialingContact_LastName = addressdata.Key.Credentialing_LastName,
                                                CredentialingContact_Building = addressdata.Key.Credentialing_Building,
                                                CredentialingContact_City = addressdata.Key.Credentialing_City,
                                                CredentialingContact_Country = addressdata.Key.Credentialing_Country,
                                                CredentialingContact_CountryCodeFax = addressdata.Key.Credentialing_CountryCodeFax,
                                                CredentialingContact_CountryCodeTelephone = addressdata.Key.Credentialing_CountryCodeTelephone,
                                                CredentialingContact_County = addressdata.Key.Credentialing_County,
                                                CredentialingContact_EmailAddress = addressdata.Key.Credentialing_EmailAddress,
                                                CredentialingContact_Fax = addressdata.Key.Credentialing_Fax,
                                                CredentialingContact_FaxNumber = addressdata.Key.Credentialing_FaxNumber,
                                                CredentialingContact_MobileNumber = addressdata.Key.Credentialing_MobileNumber,
                                                CredentialingContact_POBoxAddress = addressdata.Key.Credentialing_POBoxAddress,
                                                CredentialingContact_State = addressdata.Key.Credentialing_State,
                                                CredentialingContact_Street = addressdata.Key.Credentialing_Street,
                                                CredentialingContact_Telephone = addressdata.Key.Credentialing_Telephone,
                                                CredentialingContact_ZipCode = addressdata.Key.Credentialing_ZipCode,
                                            }).ToList();
                var StateLicensesInfo = (from data in formdata
                                         group data by new
                                         {
                                             data.StateLicenses_Status,

                                             data.StateLicenses_StatusType,
                                             data.ProviderType_Title,
                                             data.LicenseNumber,
                                             data.IssueState,
                                             data.IssueDate,
                                             data.CurrentIssueDate,
                                             data.ExpiryDate,
                                             data.StateLicenseStatus_Title


                                         } into StateLicensesdata
                                         select new
                                         {
                                             Status = StateLicensesdata.Key.StateLicenses_Status,
                                             StatusType = StateLicensesdata.Key.StateLicenses_StatusType,
                                             ProviderType_Title = StateLicensesdata.Key.ProviderType_Title,
                                             LicenseNumber = StateLicensesdata.Key.LicenseNumber,
                                             IssueState = StateLicensesdata.Key.IssueState,
                                             IssueDate = StateLicensesdata.Key.IssueDate,
                                             CurrentIssueDate = StateLicensesdata.Key.CurrentIssueDate,
                                             ExpiryDate = StateLicensesdata.Key.ExpiryDate,
                                             StateLicenseStatus_Title = StateLicensesdata.Key.StateLicenseStatus_Title
                                         }).ToList();

                var FederalDEAInformations = (from data in formdata
                                              group data by new
                                              {
                                                  data.FederalDEAInformation_Status,
                                                  data.StateOfReg,
                                                  data.FederalDEAInformation_StatusType,
                                                  data.DEANumber,
                                                  data.FederalDEA_IssueDate,
                                                  data.FederalDEA_ExpiryDate,
                                                  data.DEALicenceCertPath,
                                                  data.IsInGoodStanding


                                              } into FederalDEAdata
                                              select new
                                              {
                                                  Status = FederalDEAdata.Key.FederalDEAInformation_Status,
                                                  StatusType = FederalDEAdata.Key.FederalDEAInformation_StatusType,
                                                  DEANumber = FederalDEAdata.Key.DEANumber,
                                                  IssueDate = FederalDEAdata.Key.FederalDEA_IssueDate,
                                                  ExpiryDate = FederalDEAdata.Key.FederalDEA_ExpiryDate,
                                                  DEALicenceCertPath = FederalDEAdata.Key.DEALicenceCertPath,
                                                  IsInGoodStanding = FederalDEAdata.Key.IsInGoodStanding,
                                                  StateOfReg = FederalDEAdata.Key.StateOfReg
                                              }).ToList();

                var MedicareInformations = (from data in formdata
                                            group data by new
                                            {
                                                data.Medicare_LicenseNumber,
                                                data.Medicare_Status,
                                                data.Medicare_StatusType


                                            } into FederalDEAdata
                                            select new
                                            {
                                                LicenseNumber = FederalDEAdata.Key.Medicare_LicenseNumber,
                                                StatusType = FederalDEAdata.Key.Medicare_StatusType,
                                                Status = FederalDEAdata.Key.Medicare_Status

                                            }).ToList();
                var MedicaidInformations = (from data in formdata
                                            group data by new
                                            {
                                                data.Medicaid_LicenseNumber,
                                                data.Medicaid_Status,
                                                data.Medicaid_StatusType,
                                                data.Medicaid_State


                                            } into FederalDEAdata
                                            select new
                                            {
                                                LicenseNumber = FederalDEAdata.Key.Medicaid_LicenseNumber,
                                                StatusType = FederalDEAdata.Key.Medicaid_StatusType,
                                                Status = FederalDEAdata.Key.Medicaid_Status,
                                                State = FederalDEAdata.Key.Medicaid_State

                                            }).ToList();

                var CDSCInformationData = (from data in formdata
                                           group data by new
                                           {
                                               data.CDSCInformation_CertNumber,
                                               data.CDSCInformation_State,
                                               data.CDSCInformation_IssueDate,
                                               data.CDSCInformation_ExpiryDate,
                                               data.CDSCCerificatePath,
                                               data.CDSCInformation_Status,
                                               data.CDSCInformation_StatusType

                                           } into FederalDEAdata
                                           select new
                                           {
                                               CertNumber = FederalDEAdata.Key.CDSCInformation_CertNumber,
                                               StatusType = FederalDEAdata.Key.CDSCInformation_StatusType,
                                               Status = FederalDEAdata.Key.CDSCInformation_Status,
                                               State = FederalDEAdata.Key.CDSCInformation_State,
                                               IssueDate = FederalDEAdata.Key.CDSCInformation_IssueDate,
                                               ExpiryDate = FederalDEAdata.Key.CDSCInformation_ExpiryDate,
                                               CDSCCerificatePath = FederalDEAdata.Key.CDSCCerificatePath
                                           }).ToList();

                var OtherIdentificationNumberData = (from data in formdata
                                                     group data by new
                                                     {
                                                         data.NPINumber,
                                                         data.CAQHNumber,
                                                         data.NPIUserName,
                                                         data.NPIPassword,
                                                         data.CAQHUserName,
                                                         data.CAQHPassword,
                                                         data.UPINNumber,
                                                         data.USMLENumber

                                                     } into FederalDEAdata
                                                     select new
                                                     {
                                                         NPINumber = FederalDEAdata.Key.NPINumber,
                                                         CAQHNumber = FederalDEAdata.Key.CAQHNumber,
                                                         NPIUserName = FederalDEAdata.Key.NPIUserName,
                                                         NPIPassword = FederalDEAdata.Key.NPIPassword,
                                                         CAQHUserName = FederalDEAdata.Key.CAQHUserName,
                                                         CAQHPassword = FederalDEAdata.Key.CAQHPassword,
                                                         UPINNumber = FederalDEAdata.Key.UPINNumber,
                                                         USMLENumber = FederalDEAdata.Key.USMLENumber
                                                     }).ToList().FirstOrDefault();
                var ProfessionalLiabilityInfoes = (from data in formdata
                                                   group data by new
                                                   {
                                                       data.InsuranceCarrier_Name,
                                                       data.ProfessionalLiability_OriginalEffectiveDate,
                                                       data.ProfessionalLiability_EffectiveDate,
                                                       data.ProfessionalLiability_ExpirationDate,
                                                       data.InsuranceCertificatePath,
                                                       data.AmountOfCoveragePerOccurance,
                                                       data.AmountOfCoverageAggregate,
                                                       data.SelfInsured,
                                                       data.PolicyNumber,
                                                       data.LocationName,
                                                       data.Street,
                                                       data.Country,
                                                       data.State,
                                                       data.County,
                                                       data.City,
                                                       data.ZipCode,
                                                       data.ProfessionalLiability_PhoneNumber,
                                                       data.ProfessionalLiability_Status,
                                                       data.ProfessionalLiability_StatusType

                                                   } into ProfessionalLiabilitydata
                                                   select new
                                                   {
                                                       NPINumber = ProfessionalLiabilitydata.Key.InsuranceCarrier_Name,
                                                       OriginalEffectiveDate = ProfessionalLiabilitydata.Key.ProfessionalLiability_OriginalEffectiveDate,
                                                       EffectiveDate = ProfessionalLiabilitydata.Key.ProfessionalLiability_EffectiveDate,
                                                       ExpirationDate = ProfessionalLiabilitydata.Key.ProfessionalLiability_ExpirationDate,
                                                       InsuranceCertificatePath = ProfessionalLiabilitydata.Key.InsuranceCertificatePath,
                                                       InsuranceCarrier_Name = ProfessionalLiabilitydata.Key.InsuranceCarrier_Name,
                                                       SelfInsured = ProfessionalLiabilitydata.Key.SelfInsured,
                                                       PolicyNumber = ProfessionalLiabilitydata.Key.PolicyNumber,
                                                       LocationName = ProfessionalLiabilitydata.Key.LocationName,
                                                       Street = ProfessionalLiabilitydata.Key.Street,
                                                       Country = ProfessionalLiabilitydata.Key.Country,
                                                       State = ProfessionalLiabilitydata.Key.State,
                                                       County = ProfessionalLiabilitydata.Key.County,
                                                       City = ProfessionalLiabilitydata.Key.City,
                                                       ZipCode = ProfessionalLiabilitydata.Key.ZipCode,
                                                       Phone = ProfessionalLiabilitydata.Key.ProfessionalLiability_PhoneNumber,
                                                       AmountOfCoveragePerOccurance = ProfessionalLiabilitydata.Key.AmountOfCoveragePerOccurance,
                                                       AmountOfCoverageAggregate = ProfessionalLiabilitydata.Key.AmountOfCoverageAggregate,
                                                       Status = ProfessionalLiabilitydata.Key.ProfessionalLiability_Status,
                                                       StatusType = ProfessionalLiabilitydata.Key.ProfessionalLiability_StatusType
                                                   }).ToList();
                var ProfessionalWorkExperiences = (from data in formdata
                                                   group data by new
                                                   {
                                                       data.ProfessionalWorkExperience_EmployerName,
                                                       data.ProfessionalWorkExperience_StartDate,
                                                       data.ProfessionalWorkExperience_EndDate,
                                                       data.ProfessionalWorkExperience_JobTitle,
                                                       data.ProfessionalWorkExperience_EmployerEmail,
                                                       data.ProfessionalWorkExperience_State,
                                                       data.ProfessionalWorkExperience_County,
                                                       data.ProfessionalWorkExperience_City,
                                                       data.ProfessionalWorkExperience_ZipCode

                                                   } into ProfessionalWorkdata
                                                   select new
                                                   {
                                                       EmployerName = ProfessionalWorkdata.Key.ProfessionalWorkExperience_EmployerName,
                                                       StartDate = ProfessionalWorkdata.Key.ProfessionalWorkExperience_StartDate,
                                                       EndDate = ProfessionalWorkdata.Key.ProfessionalWorkExperience_EndDate,
                                                       JobTitle = ProfessionalWorkdata.Key.ProfessionalWorkExperience_JobTitle,
                                                       EmployerEmail = ProfessionalWorkdata.Key.ProfessionalWorkExperience_EmployerEmail,
                                                       State = ProfessionalWorkdata.Key.ProfessionalWorkExperience_State,
                                                       County = ProfessionalWorkdata.Key.ProfessionalWorkExperience_County,
                                                       City = ProfessionalWorkdata.Key.ProfessionalWorkExperience_City,
                                                       ZipCode = ProfessionalWorkdata.Key.ProfessionalWorkExperience_ZipCode
                                                   }).ToList();

                //var ContractInfoesJoiningDate = (from data in formdata
                //                                 group data by new
                //                                 {

                //                                     data.ContractInfo_Status,
                //                                     JoiningDate = data.ContractInfo_JoiningDate

                //                                 } into ProfessionalWorkdata
                //                                 select new
                //                                 {
                //                                     JoiningDate = ProfessionalWorkdata.Key.JoiningDate,
                //                                     Status =ProfessionalWorkdata.Key.ContractInfo_Status

                //                                 }).ToList();
                //var ContractInfoesGroupData = (from data in formdata
                //                               group data by new
                //                               {
                //                                   data.ContractGroupInfo_Status,
                //                                   data.ContractGroupInfo_GroupName,
                //                                   data.ContractGroupInfo_Accepted

                //                               } into ProfessionalWorkdata
                //                               select new
                //                               {
                //                                   GroupStatus = ProfessionalWorkdata.Key.ContractGroupInfo_Status,
                //                                   GroupName = ProfessionalWorkdata.Key.ContractGroupInfo_GroupName,
                //                                   GroupAccepted=ProfessionalWorkdata.Key.ContractGroupInfo_Accepted
                //                               }).ToList();


                var ContractInfoes = (from data in formdata
                                      group data by new
                                      {
                                          data.ContractGroupInfo_GroupTaxId,
                                          data.ContractInfo_IndividualTaxId,
                                          data.ContractGroupInfo_GroupNPI,
                                          data.ContractInfo_Status,
                                          JoiningDate = data.ContractInfo_JoiningDate,

                                          data.ContractGroupInfo_Status,
                                          data.ContractGroupInfo_GroupName,
                                          data.ContractGroupInfo_Accepted
                                      } into ProfessionalWorkdata
                                      select new
                                      {
                                          JoiningDate = ProfessionalWorkdata.Key.JoiningDate,
                                          Status = ProfessionalWorkdata.Key.ContractInfo_Status,
                                          GroupStatus = ProfessionalWorkdata.Key.ContractGroupInfo_Status,
                                          GroupName = ProfessionalWorkdata.Key.ContractGroupInfo_GroupName,
                                          GroupAccepted = ProfessionalWorkdata.Key.ContractGroupInfo_Accepted,
                                          GroupNPI = ProfessionalWorkdata.Key.ContractGroupInfo_GroupNPI,
                                          GroupTaxID = ProfessionalWorkdata.Key.ContractGroupInfo_GroupTaxId,
                                          IndividualTaxID = ProfessionalWorkdata.Key.ContractInfo_IndividualTaxId
                                      }).ToList();


                var EducationDetails = (from data in formdata
                                        group data by new
                                        {
                                            data.Certification,
                                            data.EducationHistory_Status,
                                            data.EducationHistory_StatusType,
                                            data.IsCompleted,
                                            data.IsUSGraduate,

                                            data.QualificationDegree,
                                            data.QualificationType,
                                            data.StartDate,
                                            data.EndDate,
                                            data.CertificatePath,
                                            data.GraduationType,

                                            data.EducationQualificationType,
                                            data.SchoolInformationID,
                                            data.SchoolInformation_SchoolName,
                                            data.SchoolInformation_Email,
                                            data.SchoolInformation_PhoneNumber,
                                            data.SchoolInformation_FaxNumber,
                                            data.SchoolInformation_Building,
                                            data.SchoolInformation_Street,
                                            data.SchoolInformation_Country,
                                            data.SchoolInformation_State,
                                            data.SchoolInformation_County,
                                            data.SchoolInformation_City,
                                            data.SchoolInformation_ZipCode


                                        } into EducationDetailsdata
                                        select new
                                        {
                                            Certification = EducationDetailsdata.Key.Certification,

                                            IsCompleted = EducationDetailsdata.Key.IsCompleted,
                                            Status = EducationDetailsdata.Key.EducationHistory_Status,
                                            StatusType = EducationDetailsdata.Key.EducationHistory_StatusType,
                                            IsUSGraduate = EducationDetailsdata.Key.IsUSGraduate,
                                            QualificationDegree = EducationDetailsdata.Key.QualificationDegree,
                                            EducationQualificationType = EducationDetailsdata.Key.EducationQualificationType,
                                            QualificationType = EducationDetailsdata.Key.QualificationType,
                                            StartDate = EducationDetailsdata.Key.StartDate,
                                            EndDate = EducationDetailsdata.Key.EndDate,
                                            CertificatePath = EducationDetailsdata.Key.CertificatePath,
                                            GraduationType = EducationDetailsdata.Key.GraduationType,
                                            SchoolName = EducationDetailsdata.Key.SchoolInformation_SchoolName,
                                            SchoolInformationID = EducationDetailsdata.Key.SchoolInformationID,
                                            Email = EducationDetailsdata.Key.SchoolInformation_Email,
                                            PhoneNumber = EducationDetailsdata.Key.SchoolInformation_PhoneNumber,
                                            Fax = EducationDetailsdata.Key.SchoolInformation_FaxNumber,
                                            Building = EducationDetailsdata.Key.SchoolInformation_Building,
                                            Street = EducationDetailsdata.Key.SchoolInformation_Street,
                                            Country = EducationDetailsdata.Key.SchoolInformation_Country,
                                            State = EducationDetailsdata.Key.SchoolInformation_State,
                                            County = EducationDetailsdata.Key.SchoolInformation_County,
                                            City = EducationDetailsdata.Key.SchoolInformation_City,
                                            ZipCode = EducationDetailsdata.Key.SchoolInformation_ZipCode
                                        }).ToList();

                var ProgramDetails = (from data in formdata
                                      group data by new
                                      {

                                          data.ProgramDetail_SpecialtyName,
                                          data.ProgramType,
                                          data.ProgramDetail_StartDate,
                                          data.ProgramDetail_EndDate,
                                          data.ProgramDetail_Preference,

                                          data.ProgramDetail_SchoolInformationID,
                                          data.ProgramDetail_SchoolInformation_SchoolName,
                                          data.ProgramDetail_SchoolInformation_Email,
                                          data.ProgramDetail_SchoolInformation_PhoneNumber,
                                          data.ProgramDetail_SchoolInformation_FaxNumber,
                                          data.ProgramDetail_SchoolInformation_Building,
                                          data.ProgramDetail_SchoolInformation_Street,
                                          data.ProgramDetail_SchoolInformation_Country,
                                          data.ProgramDetail_SchoolInformation_State,
                                          data.ProgramDetail_SchoolInformation_County,
                                          data.ProgramDetail_SchoolInformation_City,
                                          data.ProgramDetail_SchoolInformation_ZipCode


                                      } into EducationDetailsdata
                                      select new
                                      {

                                          Preference = EducationDetailsdata.Key.ProgramDetail_Preference,
                                          ProgramType = EducationDetailsdata.Key.ProgramType,
                                          ProgramDetail_StartDate = EducationDetailsdata.Key.ProgramDetail_StartDate,
                                          ProgramDetail_EndDate = EducationDetailsdata.Key.ProgramDetail_EndDate,
                                          Speciality = EducationDetailsdata.Key.ProgramDetail_SpecialtyName,


                                          SchoolName = EducationDetailsdata.Key.ProgramDetail_SchoolInformation_SchoolName,
                                          SchoolInformationID = EducationDetailsdata.Key.ProgramDetail_SchoolInformationID,
                                          Email = EducationDetailsdata.Key.ProgramDetail_SchoolInformation_Email,
                                          PhoneNumber = EducationDetailsdata.Key.ProgramDetail_SchoolInformation_PhoneNumber,
                                          Fax = EducationDetailsdata.Key.ProgramDetail_SchoolInformation_FaxNumber,
                                          Building = EducationDetailsdata.Key.ProgramDetail_SchoolInformation_Building,
                                          Street = EducationDetailsdata.Key.ProgramDetail_SchoolInformation_Street,
                                          Country = EducationDetailsdata.Key.ProgramDetail_SchoolInformation_Country,
                                          State = EducationDetailsdata.Key.ProgramDetail_SchoolInformation_State,
                                          County = EducationDetailsdata.Key.ProgramDetail_SchoolInformation_County,
                                          City = EducationDetailsdata.Key.ProgramDetail_SchoolInformation_City,
                                          ZipCode = EducationDetailsdata.Key.ProgramDetail_SchoolInformation_ZipCode
                                      }).ToList();

                var ECFMGDetails = (from data in formdata
                                    group data by new
                                    {

                                        data.ECFMGNumber,
                                        data.ECFMGCertPath,
                                        data.ECFMGIssueDate


                                    } into ECFMGDetailsdata
                                    select new
                                    {

                                        ECFMGNumber = ECFMGDetailsdata.Key.ECFMGNumber,
                                        ECFMGCertPath = ECFMGDetailsdata.Key.ECFMGCertPath,
                                        ECFMGIssueDate = ECFMGDetailsdata.Key.ECFMGIssueDate
                                    }).ToList().LastOrDefault();

                var HospitalPrivilegeInformation = (from data in formdata
                                                    group data by new
                                                    {
                                                        data.HospitalID,
                                                        data.HospitalPrivilege_HospitalName,
                                                        data.AffilicationStartDate,
                                                        data.AffiliationEndDate,
                                                        data.HospitalPrivilege_LocationName,
                                                        data.HospitalPrivilege_Email,
                                                        data.HospitalContactInfo_UnitNumber,
                                                        data.HospitalContactInfo_Street,
                                                        data.HospitalContactInfo_Country,
                                                        data.HospitalContactInfo_State,
                                                        data.HospitalContactInfo_County,
                                                        data.HospitalContactInfo_City,
                                                        data.HospitalContactInfo_ZipCode,
                                                        data.HospitalContactInfo_Phone,
                                                        data.HospitalContactInfo_Fax,
                                                        data.HospitalPrivilege_Preference,
                                                        data.HospitalPrivilege_Status,
                                                        data.HospitalPrivilege_StatusType,
                                                        data.HospitalPrivilege_ContactPersonName,
                                                        data.HospitalPrivilege_DepartmentName,
                                                        data.StaffCategory_Title,
                                                        data.HospitalPrivilege_SpecialityName


                                                    } into HospitalPrivilegedata
                                                    select new
                                                    {
                                                        HospitalID = HospitalPrivilegedata.Key.HospitalID,
                                                        HospitalName = HospitalPrivilegedata.Key.HospitalPrivilege_HospitalName,
                                                        AffilicationStartDate = HospitalPrivilegedata.Key.AffilicationStartDate,
                                                        AffiliationEndDate = HospitalPrivilegedata.Key.AffiliationEndDate,
                                                        UnitNumber = HospitalPrivilegedata.Key.HospitalContactInfo_UnitNumber,
                                                        Street = HospitalPrivilegedata.Key.HospitalContactInfo_Street,
                                                        Country = HospitalPrivilegedata.Key.HospitalContactInfo_Country,
                                                        State = HospitalPrivilegedata.Key.HospitalContactInfo_State,
                                                        Status = HospitalPrivilegedata.Key.HospitalPrivilege_Status,
                                                        StatusType = HospitalPrivilegedata.Key.HospitalPrivilege_StatusType,
                                                        County = HospitalPrivilegedata.Key.HospitalContactInfo_County,
                                                        City = HospitalPrivilegedata.Key.HospitalContactInfo_City,
                                                        ZipCode = HospitalPrivilegedata.Key.HospitalContactInfo_ZipCode,
                                                        PhoneNumber = HospitalPrivilegedata.Key.HospitalContactInfo_Phone,
                                                        Fax = HospitalPrivilegedata.Key.HospitalContactInfo_Fax,
                                                        Preference = HospitalPrivilegedata.Key.HospitalPrivilege_Preference,
                                                        LocationName = HospitalPrivilegedata.Key.HospitalPrivilege_LocationName,
                                                        Email = HospitalPrivilegedata.Key.HospitalPrivilege_Email,
                                                        ContactPersonName = HospitalPrivilegedata.Key.HospitalPrivilege_ContactPersonName,
                                                        DepartmentName = HospitalPrivilegedata.Key.HospitalPrivilege_DepartmentName,
                                                        StaffCategory_Title = HospitalPrivilegedata.Key.StaffCategory_Title,
                                                        SpecialityName = HospitalPrivilegedata.Key.HospitalPrivilege_SpecialityName
                                                    }).ToList();

                var ProfessionalAffiliationInfos = (from data in formdata
                                                    group data by new
                                                    {

                                                        data.ProfessionalAffiliation_OrganizationName,
                                                        data.ProfessionalAffiliation_StartDate,
                                                        data.ProfessionalAffiliation_EndDate,
                                                        data.ProfessionalAffiliation_PositionOfficeHeld,
                                                        data.ProfessionalAffiliation_Member,
                                                        data.ProfessionalAffiliation_Status,
                                                        data.ProfessionalAffiliation_StatusType

                                                    } into ProfessionalAffiliationdata
                                                    select new
                                                    {

                                                        OrganizationName = ProfessionalAffiliationdata.Key.ProfessionalAffiliation_OrganizationName,
                                                        StartDate = ProfessionalAffiliationdata.Key.ProfessionalAffiliation_StartDate,
                                                        EndDate = ProfessionalAffiliationdata.Key.ProfessionalAffiliation_EndDate,
                                                        PositionOfficeHeld = ProfessionalAffiliationdata.Key.ProfessionalAffiliation_PositionOfficeHeld,
                                                        Member = ProfessionalAffiliationdata.Key.ProfessionalAffiliation_Member,
                                                        Status = ProfessionalAffiliationdata.Key.ProfessionalAffiliation_Status,
                                                        StatusType = ProfessionalAffiliationdata.Key.ProfessionalAffiliation_StatusType
                                                    }).ToList();

                var CustomFieldInformation = (from data in formdata
                                              group data by new { data.CustomFieldTitle, data.CustomFieldTransactionDataValue } into CustomFieldData
                                              select new
                                              {
                                                  CustomFieldTitle = CustomFieldData.Key.CustomFieldTitle,
                                                  CustomFieldDataValue = CustomFieldData.Key.CustomFieldTransactionDataValue
                                              }).ToList();


                string CDUserId = GetCredentialingUserId(UserAuthId).ToString();
                PDFMappingDataBusinessModel pmodel = new PDFMappingDataBusinessModel();

                #region TriCare Prime Credentialing Prefilled properties
                pmodel.CorrespondenceAddressName = "Credentialing";
                pmodel.CorrespondenceAddressStreetAddress = "14690 Spring Hill Drive";
                pmodel.CorrespondenceAddressSuiteNumber = "101";
                pmodel.CorrespondenceAddressCity = "Spring Hill";
                pmodel.CorrespondenceAddressState = "FL";
                pmodel.CorrespondenceAddressSZipCode = "34609-8102";
                pmodel.CorrespondenceAddressCounty = "Hernando";
                pmodel.CorrespondenceAddressOfficePhoneNumber = "(352)799-0046";
                pmodel.CorrespondenceAddressOfficeFaxNumber = "(352)799-0042";
                pmodel.BillingAddressName = "Access Health Care Physicians, LLC";
                pmodel.LegalPracticeName = "Access Health Care Physicians, LLC";
                pmodel.BillingAddressStreetAddress = "P.O.Box 919469";
                pmodel.BillingAddressCity = "Orlando";
                pmodel.BillingAddressState = "FL";
                pmodel.BillingAddressZipCode = "32891-9469";
                pmodel.BillingAddressCounty = "Orange";
                pmodel.BillingAddressOfficePhoneNumber = "(727)823-2188";
                pmodel.BillingAddressOfficeFaxNumber = "(727)828-0723";
                pmodel.BillingAddressSuiteNumber = "";
                pmodel.OfficeHoursMondayFrom = "8 AM";
                pmodel.OfficeHourMondayTo = "5 PM";
                pmodel.OfficeHoursTuesdayFrom = "8 AM";
                pmodel.OfficeHourTuesdayTo = "5 PM";
                pmodel.OfficeHoursWednesdayFrom = "8 AM";
                pmodel.OfficeHourWednesdayTo = "5 PM";
                pmodel.OfficeHoursThursdayFrom = "8 AM";
                pmodel.OfficeHourThursdayTo = "5 PM";
                pmodel.OfficeHoursFridayFrom = "8 AM";
                pmodel.OfficeHourFridayTo = "5 PM";
                pmodel.Practice_EmailAddress = "credentialing@accesshealthcarellc.net";
                pmodel.Practice_HowManyTriCarePatientsWillYouAccept = "100";
                pmodel.Practice_AgeRangeFrom = "18";
                pmodel.Practice_AgeRangeTo = "100";
                pmodel.Practice_CredentialingContactName = "Credentialing Department";
                pmodel.Practice_CredentialingContactEmailAddress = "credentialing@accesshealthcarellc.net";
                pmodel.Practice_CredentialingContactPhoneNumber = "3527990046";
                pmodel.Practice_CredentialingContactFax = "3527990042";
                #endregion

                #region Demographics

                #region Personal Details

                pmodel.Personal_ProviderProfileImage = personaldetails.ProfilePhotoPath;

                pmodel.Personal_ProviderMiddleName = personaldetails.MiddleName;

                if (personaldetails.MiddleName != null)
                    pmodel.Personal_ProviderName = personaldetails.FirstName + " " + personaldetails.MiddleName + " " + personaldetails.LastName;
                else
                    pmodel.Personal_ProviderName = personaldetails.FirstName + " " + personaldetails.LastName;

                if (providerTitleData != null)
                {
                    pmodel.Personal_ProviderType = providerTitleData.ProviderTitleCode;
                    pmodel.Personal_ProviderTitleName = providerTitleData.ProviderTitle;
                }

                if (pmodel.Personal_ProviderTitleName != null)
                {
                    pmodel.Provider_FullName_Title = pmodel.Personal_ProviderName + " " + pmodel.Personal_ProviderTitleName;
                }
                else
                {
                    pmodel.Provider_FullName_Title = pmodel.Personal_ProviderName;
                }

                if (pmodel.Personal_ProviderType != null)
                {
                    pmodel.Provider_FullNameTitle = pmodel.Personal_ProviderName + " " + pmodel.Personal_ProviderType;
                    pmodel.Personal_ProviderTitle = pmodel.Personal_ProviderType;
                }
                else
                {
                    pmodel.Provider_FullNameTitle = pmodel.Personal_ProviderName;
                }
                if (pmodel.Personal_ProviderMiddleName != null)
                {
                    pmodel.Personal_ProviderMiddleNameFirstLetter = pmodel.Personal_ProviderMiddleName.Substring(0, 1);
                }
                else
                {
                    pmodel.Personal_ProviderMiddleNameFirstLetter = null;
                }
                pmodel.Personal_ProviderLastName = personaldetails.LastName;

                pmodel.Personal_ProviderFirstName = personaldetails.FirstName;

                pmodel.Personal_ProviderSuffix = personaldetails.Suffix;

                pmodel.Personal_ProviderGender = personaldetails.Gender;
                pmodel.Personal_MaidenName = personaldetails.MaidenName;
                pmodel.Personal_MaritalStatus = personaldetails.MaritalStatus;
                pmodel.Personal_SpouseName = personaldetails.SpouseName;
                pmodel.Personal_LastFirstMiddleName = pmodel.Personal_ProviderLastName + " " + pmodel.Personal_ProviderFirstName + " " + pmodel.Personal_ProviderMiddleName;


                #region Birth Information

                if (birthInformation != null)
                {
                    if (birthInformation.DateOfBirth != null)
                    {
                        pmodel.Personal_ProviderDOB = birthInformation.DateOfBirth.Split(' ')[0];
                        pmodel.Personal_BirthDate = birthInformation.DateOfBirth.Split(' ')[0];
                    }
                    pmodel.Personal_BirthCountry = birthInformation.CountryOfBirth;
                    pmodel.Personal_BirthState = birthInformation.StateOfBirth;
                    pmodel.Personal_BirthCounty = birthInformation.CountyOfBirth;
                    pmodel.Personal_BirthCity = birthInformation.CityOfBirth;
                    pmodel.Personal_BirthCertificate = birthInformation.BirthCertificatePath;
                }

                #endregion

                if (languageInformation != null && languageInformation.Count > 0)
                {
                    var languages = languageInformation.OrderBy(o => o.ProficiencyIndex).ToList();
                    foreach (var item in languages)
                    {
                        if (item != null)
                            pmodel.Personal_LanguageKnown1 += item.Language + ", ";
                    }

                    if (languages.Count > 0)
                    {
                        pmodel.Personal_LanguageKnown2 = languages.ElementAt(0).Language;
                    }
                    if (languages.Count > 1)
                    {
                        pmodel.Personal_LanguageKnown3 = languages.ElementAt(1).Language;
                    }
                    if (languages.Count > 2)
                    {
                        pmodel.Personal_LanguageKnown4 = languages.ElementAt(2).Language;
                    }
                }


                #endregion

                #region Other Legal Names

                if (otherLegalNames != null && otherLegalNames.Count > 0)
                {
                    var activeOtherLegalNames = otherLegalNames.Where(s => (s.OtherlegalNameStatus != null && s.OtherLegalName_StatusType != StatusType.Inactive)).ToList().LastOrDefault();
                    if (activeOtherLegalNames != null)
                    {
                        pmodel.OtherLegalName_FirstName1 = activeOtherLegalNames.OtherFirstName;
                        pmodel.OtherLegalName_MiddleName1 = activeOtherLegalNames.OtherMiddleName;
                        pmodel.OtherLegalName_LastName1 = activeOtherLegalNames.OtherLastName;
                        pmodel.OtherLegalName_Suffix1 = activeOtherLegalNames.OtherLegalNameSuffix;
                        pmodel.OtherLegalName_StartDate1 = ConvertToDateString(activeOtherLegalNames.OtherLegalName_StartDate);
                        pmodel.OtherLegalName_EndDate1 = ConvertToDateString(activeOtherLegalNames.OtherLegalName_EndDate);
                        pmodel.OtherLegalName_Certificate1 = activeOtherLegalNames.OtherLegalName_DocumentPath;
                    }
                }

                #endregion

                #region Contact Information

                if (homeAddress != null && homeAddress.Count > 0)
                {
                    var HomeAddresses = homeAddress.Where(s => (s.HomeAddress_StatusType != StatusType.Inactive)).ToList().LastOrDefault();
                    if (HomeAddresses != null)
                    {
                        pmodel.Personal_ApartmentNumber1 = HomeAddresses.HomeAddress_UnitNumber;
                        pmodel.Personal_StreetAddress1 = HomeAddresses.HomeAddress_Street;
                        pmodel.Personal_Country1 = HomeAddresses.HomeAddress_Country;
                        pmodel.Personal_State1 = HomeAddresses.HomeAddress_State;
                        pmodel.Personal_City1 = HomeAddresses.HomeAddress_City;
                        pmodel.Personal_County1 = HomeAddresses.HomeAddress_County;
                        pmodel.Personal_ZipCode1 = HomeAddresses.HomeAddress_ZipCode;
                        pmodel.Personal_LivingStartDate1 = ConvertToDateString(HomeAddresses.HomeAddress_LivingFromDate);
                        pmodel.Personal_LivingEndDate1 = ConvertToDateString(HomeAddresses.HomeAddress_LivingEndDate);
                        pmodel.Personal_Address1 = pmodel.Personal_StreetAddress1 + "  " + pmodel.Personal_ApartmentNumber1 + ", " + pmodel.Personal_City1 + ", " + pmodel.Personal_State1 + ", " + pmodel.Personal_ZipCode1;
                    }
                }

                string currDate = DateTime.Now.ToString("MM-dd-yyyy");
                pmodel.currentDate = currDate;
                pmodel.Primecare = "PRIMECARE";
                #endregion

                #region Contact Details


                if (contactPhoneDetails != null)
                {
                    if (contactPhoneDetails.Count > 0)
                    {
                        var PhoneDetails = contactPhoneDetails.Where(s => (s.PhoneDetails_Status != null && s.PhoneDetails_StatusType != StatusType.Inactive)).ToList();

                        if (PhoneDetails.Count > 0)
                        {
                            var IsPhone = PhoneDetails.Any(p => p.PhoneType == AHC.CD.Entities.MasterData.Enums.PhoneTypeEnum.Mobile.ToString() && p.Number != null);
                            var IsFax = PhoneDetails.Any(p => p.PhoneType == AHC.CD.Entities.MasterData.Enums.PhoneTypeEnum.Fax.ToString() && p.Number != null);
                            var IsHome = PhoneDetails.Any(p => p.PhoneType == AHC.CD.Entities.MasterData.Enums.PhoneTypeEnum.Home.ToString() && p.Number != null);

                            if (IsPhone)
                            {
                                pmodel.Personal_MobileNumber1 = PhoneDetails.Where(p => p.PhoneType == AHC.CD.Entities.MasterData.Enums.PhoneTypeEnum.Mobile.ToString()).Last().Number.ToString();
                            }

                            if (IsFax)
                            {
                                pmodel.Personal_HomeFax1 = PhoneDetails.Where(p => p.PhoneType == AHC.CD.Entities.MasterData.Enums.PhoneTypeEnum.Fax.ToString()).Last().Number.ToString();
                            }
                            if (IsHome)
                            {
                                pmodel.Personal_HomePhone1 = PhoneDetails.Where(p => p.PhoneType == AHC.CD.Entities.MasterData.Enums.PhoneTypeEnum.Home.ToString()).Last().Number.ToString();

                            }
                        }
                    }


                    if (contactEmailDetails != null && contactEmailDetails.Count > 0)
                    {
                        var emails = contactEmailDetails.Where(e => (e.EmailAddress_Status != null && e.EmailAddress_Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString())).ToList();

                        if (emails.Count > 0)
                            pmodel.Personal_EmailId1 = emails.Last().EmailAddress.ToString();
                    }
                }

                #endregion

                #region Personal Identification

                if (personalIdentificationDetails != null)
                {
                    pmodel.Personal_SocialSecurityNumber = personalIdentificationDetails.SSN;
                    pmodel.Personal_DriverLicense = personalIdentificationDetails.DL;
                    pmodel.Personal_SSNCertificate = personalIdentificationDetails.SSNCertificatePath;
                    pmodel.Personal_DriverLicenseCertificate = personalIdentificationDetails.DLCertificatePath;
                }


                #endregion

                #region Visa Information

                if (visaDetails != null)
                {
                    pmodel.Personal_IsUSCitizen = visaDetails.IsResidentOfUSA;
                    pmodel.Personal_IsUSAuthorized = visaDetails.IsAuthorizedToWorkInUS;
                    if (visaInfo != null)
                    {
                        pmodel.Personal_VisaNumber = visaInfo.VisaNumber;

                        pmodel.Personal_VisaType = visaInfo.VisaType_Title;
                        pmodel.Personal_VisaStatus = visaInfo.VisaStatus_Title;

                        pmodel.Personal_VisaSponsor = visaInfo.VisaSponsor;
                        pmodel.Personal_VisaExpiration = ConvertToDateString(visaInfo.VisaExpirationDate);
                        pmodel.Personal_VisaDocument = visaInfo.VisaCertificatePath;
                        pmodel.Personal_GreenCardNumber = visaInfo.GreenCardNumber;
                        pmodel.Personal_GreenCardDocument = visaInfo.GreenCardCertificatePath;
                        pmodel.Personal_NationalIdentificationNo = visaInfo.NationalIDNumber;
                        pmodel.Personal_NationalIdentificationDoc = visaInfo.NationalIDCertificatePath;
                        pmodel.Personal_IssueCountry = visaInfo.CountryOfIssue;
                    }
                }

                #endregion
                #endregion

                #region Specialty New
                if (specialitylist.Count > 0)
                {
                    var primaryInfo = specialitylist.FirstOrDefault(p => p.SpecialityPreference == AHC.CD.Entities.MasterData.Enums.PreferenceType.Primary.ToString() && !p.SpecialityStatus.Equals(AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()));

                    var secondaryInfo = specialitylist.Where(p => p.SpecialityPreference == AHC.CD.Entities.MasterData.Enums.PreferenceType.Secondary.ToString() && !p.SpecialityStatus.Equals(AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString())).ToList();

                    var secondaryCount = secondaryInfo.Count;

                    #region Specialty Board Name Check

                    if (specialitylist != null)
                    {
                        foreach (var specialty in specialitylist)
                        {
                            if (specialty.SpecialityBoard_Name != null)
                            {
                                if (specialty.SpecialityBoard_Name == "Natl Commission on Certification of Physician Assistants")
                                {
                                    pmodel.PA_Specialty_Certificate = specialty.CertificateNumber;
                                    pmodel.PA_Specialty_LastRecertificationDate = ConvertToDateString(specialty.LastReCerificationDate);
                                    pmodel.PA_Specialty_InitialCertificationDate = ConvertToDateString(specialty.InitialCertificationDate);
                                    pmodel.PA_Specialty_ExpirationDate = ConvertToDateString(specialty.ExpirationDate);
                                }
                                else if (specialty.SpecialityBoard_Name == "Certificate of membership in the American Speech, Language and Hearing Association")
                                {
                                    pmodel.Physical_Specialty_Certificate = specialty.CertificateNumber;
                                    pmodel.Physical_Specialty_LastRecertificationDate = ConvertToDateString(specialty.LastReCerificationDate);
                                    pmodel.Physical_Specialty_InitialCertificationDate = ConvertToDateString(specialty.InitialCertificationDate);
                                    pmodel.Physical_Specialty_ExpirationDate = ConvertToDateString(specialty.ExpirationDate);
                                }

                                else if (specialty.SpecialityBoard_Name == "The Council on Certification of Nurse Anesthetists")
                                {
                                    pmodel.CRNA_Specialty_Certificate = specialty.CertificateNumber;
                                    pmodel.CRNA_Specialty_LastRecertificationDate = ConvertToDateString(specialty.LastReCerificationDate);
                                    pmodel.CRNA_Specialty_InitialCertificationDate = ConvertToDateString(specialty.InitialCertificationDate);
                                    pmodel.CRNA_Specialty_ExpirationDate = ConvertToDateString(specialty.ExpirationDate);
                                }
                                else if (specialty.SpecialityBoard_Name == "The American College of Nurse Midwives or American Midwifery Certification Board")
                                {
                                    pmodel.CNM_Specialty_Certificate = specialty.CertificateNumber;
                                    pmodel.CNM_Specialty_LastRecertificationDate = ConvertToDateString(specialty.LastReCerificationDate);
                                    pmodel.CNM_Specialty_InitialCertificationDate = ConvertToDateString(specialty.InitialCertificationDate);
                                    pmodel.CNM_Specialty_ExpirationDate = ConvertToDateString(specialty.ExpirationDate);
                                }
                                else if (specialty.SpecialityBoard_Name == "National nurse practitioner board")
                                {
                                    pmodel.Nurse_Specialty_Certificate = specialty.CertificateNumber;
                                    pmodel.Nurse_Specialty_LastRecertificationDate = ConvertToDateString(specialty.LastReCerificationDate);
                                    pmodel.Nurse_Specialty_InitialCertificationDate = ConvertToDateString(specialty.InitialCertificationDate);
                                    pmodel.Nurse_Specialty_ExpirationDate = ConvertToDateString(specialty.ExpirationDate);
                                }
                            }
                        }
                    }
                    #endregion

                    var IsBoardCertifiedValue = false;

                    if (primaryInfo != null)
                    {
                        #region Primary

                        pmodel.Specialty_PrimarySpecialtyName = primaryInfo.SpecialityName;
                        pmodel.Specialty_PrimaryTaxonomyCode = primaryInfo.TaxonomyCode;

                        if (primaryInfo.IsBoardCertified != null && primaryInfo.IsBoardCertified == AHC.CD.Entities.MasterData.Enums.YesNoOption.YES.ToString())
                        {
                            pmodel.Specialty_PrimaryIsBoardCertified = primaryInfo.IsBoardCertified.ToString();
                            pmodel.Specialty_PrimaryCertificate = primaryInfo.CertificateNumber;
                            pmodel.Specialty_PrimaryLastRecertificationDate = ConvertToDateString(primaryInfo.LastReCerificationDate);
                            pmodel.Specialty_PrimaryInitialCertificationDate = ConvertToDateString(primaryInfo.InitialCertificationDate);
                            pmodel.Specialty_PrimaryExpirationDate = ConvertToDateString(primaryInfo.ExpirationDate);
                            pmodel.Specialty_PrimaryBoardName = primaryInfo.SpecialityBoard_Name;
                        }
                        if (pmodel.Specialty_PrimaryLastRecertificationDate != null)
                        {
                            pmodel.Specialty_CurrentIssueDate1_MM = pmodel.Specialty_PrimaryLastRecertificationDate.Split('-')[0];
                            pmodel.Specialty_CurrentIssueDate1_dd = pmodel.Specialty_PrimaryLastRecertificationDate.Split('-')[1];
                            pmodel.Specialty_CurrentIssueDate1_yyyy = pmodel.Specialty_PrimaryLastRecertificationDate.Split('-')[2].Substring(2);
                        }
                        if (pmodel.Specialty_PrimarySpecialtyName != "National Commission on Certification of Physician Assistants" && pmodel.Specialty_PrimaryLastRecertificationDate != null)
                        {
                            pmodel.NationalCommissionSpecialty_CurrentIssueDate1_MM = pmodel.Specialty_PrimaryLastRecertificationDate.Split('-')[0];
                            pmodel.NationalCommissionSpecialty_CurrentIssueDate1_dd = pmodel.Specialty_PrimaryLastRecertificationDate.Split('-')[1];
                            pmodel.NationalCommissionSpecialty_CurrentIssueDate1_yyyy = pmodel.Specialty_PrimaryLastRecertificationDate.Split('-')[2].Substring(2);
                        }
                        if (pmodel.Specialty_PrimaryBoardName == "National Commission on Certification of Physician Assistants")
                        {
                            pmodel.Specialty_NCCPAIsBoardCertified = primaryInfo.IsBoardCertified.ToString();
                            pmodel.Specialty_NCCPACertificate = primaryInfo.CertificateNumber;
                            pmodel.Specialty_NCCPALastRecertificationDate = ConvertToDateString(primaryInfo.LastReCerificationDate);
                            pmodel.Specialty_NCCPAInitialCertificationDate = ConvertToDateString(primaryInfo.InitialCertificationDate);
                            pmodel.Specialty_NCCPAExpirationDate = ConvertToDateString(primaryInfo.ExpirationDate);
                            pmodel.Specialty_Primary_NCCPABoardName = primaryInfo.SpecialityBoard_Name;
                        }

                        #endregion

                        #region Specific Primary Specialty

                        pmodel.Specialty_PrimarySpecialtyName1 = primaryInfo.SpecialityName;
                        pmodel.Specialty_PrimaryTaxonomyCode1 = primaryInfo.TaxonomyCode;

                        if (primaryInfo.IsBoardCertified != null && primaryInfo.IsBoardCertified == AHC.CD.Entities.MasterData.Enums.YesNoOption.YES.ToString())
                        {
                            pmodel.Specialty_PrimaryIsBoardCertified1 = primaryInfo.IsBoardCertified.ToString();
                            pmodel.Specialty_PrimaryCertificate1 = primaryInfo.CertificateNumber;
                            pmodel.Specialty_PrimaryLastRecertificationDate1 = ConvertToDateString(primaryInfo.LastReCerificationDate);
                            pmodel.Specialty_PrimaryInitialCertificationDate1 = ConvertToDateString(primaryInfo.InitialCertificationDate);
                            pmodel.Specialty_PrimaryExpirationDate1 = ConvertToDateString(primaryInfo.ExpirationDate);
                            pmodel.Specialty_PrimaryBoardName1 = primaryInfo.SpecialityBoard_Name;
                        }
                        #endregion

                        #region Secondary Specialty
                        if (secondaryInfo != null && secondaryInfo.Count > 0)
                        {
                            if (secondaryInfo.Count > 0)
                            {
                                if (secondaryInfo.ElementAt(secondaryCount - 1).IsBoardCertified != null && secondaryInfo.ElementAt(secondaryCount - 1).IsBoardCertified == AHC.CD.Entities.MasterData.Enums.YesNoOption.YES.ToString())
                                {
                                    pmodel.Specialty_IsBoardCertified1 = secondaryInfo.ElementAt(secondaryCount - 1).IsBoardCertified.ToString();
                                    pmodel.Specialty_Certificate1 = secondaryInfo.ElementAt(secondaryCount - 1).CertificateNumber;
                                    pmodel.Specialty_LastRecertificationDate1 = ConvertToDateString(secondaryInfo.ElementAt(secondaryCount - 1).LastReCerificationDate);
                                    pmodel.Specialty_InitialCertificationDate1 = ConvertToDateString(secondaryInfo.ElementAt(secondaryCount - 1).InitialCertificationDate);
                                    pmodel.Specialty_ExpirationDate1 = ConvertToDateString(secondaryInfo.ElementAt(secondaryCount - 1).ExpirationDate);
                                    pmodel.Specialty_BoardName1 = secondaryInfo.ElementAt(secondaryCount - 1).SpecialityBoard_Name;
                                }
                                if (secondaryInfo.ElementAt(secondaryCount - 1) != null)
                                {
                                    pmodel.Specialty_SpecialtyName1 = secondaryInfo.ElementAt(secondaryCount - 1).SpecialityName;
                                    pmodel.Specialty_TaxonomyCode1 = secondaryInfo.ElementAt(secondaryCount - 1).TaxonomyCode;
                                }

                                if (pmodel.Specialty_BoardName1 == "National Commission on Certification of Physician Assistants")
                                {
                                    pmodel.Specialty_NCCPAIsBoardCertified1 = secondaryInfo.ElementAt(secondaryCount - 1).IsBoardCertified.ToString();
                                    pmodel.Specialty_NCCPACertificate1 = secondaryInfo.ElementAt(secondaryCount - 1).CertificateNumber;
                                    pmodel.Specialty_NCCPALastRecertificationDate1 = ConvertToDateString(secondaryInfo.ElementAt(secondaryCount - 1).LastReCerificationDate);
                                    pmodel.Specialty_NCCPAInitialCertificationDate1 = ConvertToDateString(secondaryInfo.ElementAt(secondaryCount - 1).InitialCertificationDate);
                                    pmodel.Specialty_NCCPAExpirationDate1 = ConvertToDateString(secondaryInfo.ElementAt(secondaryCount - 1).ExpirationDate);
                                    pmodel.Specialty_NCCPABoardName1 = secondaryInfo.ElementAt(secondaryCount - 1).SpecialityBoard_Name;
                                }

                            }
                            if (secondaryInfo.Count > 1)
                            {
                                if (secondaryInfo.ElementAt(secondaryCount - 2) != null)
                                {
                                    pmodel.Specialty_SpecialtyName2 = secondaryInfo.ElementAt(secondaryCount - 2).SpecialityName;
                                    pmodel.Specialty_TaxonomyCode2 = secondaryInfo.ElementAt(secondaryCount - 2).TaxonomyCode;
                                }
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        if (secondaryInfo.Count > 0)
                        {
                            for (int i = 0; i < secondaryInfo.Count; i++)
                            {
                                if (secondaryInfo[i].IsBoardCertified != null && secondaryInfo[i].IsBoardCertified == AHC.CD.Entities.MasterData.Enums.YesNoOption.YES.ToString())
                                {
                                    IsBoardCertifiedValue = true;

                                    pmodel.Specialty_PrimarySpecialtyName = secondaryInfo[i].SpecialityName;
                                    pmodel.Specialty_PrimaryTaxonomyCode = secondaryInfo[i].TaxonomyCode;

                                    pmodel.Specialty_PrimaryIsBoardCertified = secondaryInfo[i].IsBoardCertified.ToString();
                                    pmodel.Specialty_PrimaryCertificate = secondaryInfo[i].CertificateNumber;
                                    pmodel.Specialty_PrimaryLastRecertificationDate = ConvertToDateString(secondaryInfo[i].LastReCerificationDate);
                                    pmodel.Specialty_PrimaryInitialCertificationDate = ConvertToDateString(secondaryInfo[i].InitialCertificationDate);
                                    pmodel.Specialty_PrimaryExpirationDate = ConvertToDateString(secondaryInfo[i].ExpirationDate);
                                    pmodel.Specialty_PrimaryBoardName = secondaryInfo[i].SpecialityBoard_Name;

                                    if (pmodel.Specialty_PrimaryLastRecertificationDate != null)
                                    {
                                        pmodel.Specialty_CurrentIssueDate1_MM = pmodel.Specialty_PrimaryLastRecertificationDate.Split('-')[0];
                                        pmodel.Specialty_CurrentIssueDate1_dd = pmodel.Specialty_PrimaryLastRecertificationDate.Split('-')[1];
                                        pmodel.Specialty_CurrentIssueDate1_yyyy = pmodel.Specialty_PrimaryLastRecertificationDate.Split('-')[2].Substring(2);
                                    }
                                    if (pmodel.Specialty_PrimarySpecialtyName != "National Commission on Certification of Physician Assistants" && pmodel.Specialty_PrimaryLastRecertificationDate != null)
                                    {
                                        pmodel.NationalCommissionSpecialty_CurrentIssueDate1_MM = pmodel.Specialty_PrimaryLastRecertificationDate.Split('-')[0];
                                        pmodel.NationalCommissionSpecialty_CurrentIssueDate1_dd = pmodel.Specialty_PrimaryLastRecertificationDate.Split('-')[1];
                                        pmodel.NationalCommissionSpecialty_CurrentIssueDate1_yyyy = pmodel.Specialty_PrimaryLastRecertificationDate.Split('-')[2].Substring(2);
                                    }
                                    if (pmodel.Specialty_PrimaryBoardName == "National Commission on Certification of Physician Assistants")
                                    {
                                        pmodel.Specialty_NCCPAIsBoardCertified = secondaryInfo[i].IsBoardCertified.ToString();
                                        pmodel.Specialty_NCCPACertificate = secondaryInfo[i].CertificateNumber;
                                        pmodel.Specialty_NCCPALastRecertificationDate = ConvertToDateString(secondaryInfo[i].LastReCerificationDate);
                                        pmodel.Specialty_NCCPAInitialCertificationDate = ConvertToDateString(secondaryInfo[i].InitialCertificationDate);
                                        pmodel.Specialty_NCCPAExpirationDate = ConvertToDateString(secondaryInfo[i].ExpirationDate);
                                        pmodel.Specialty_Primary_NCCPABoardName = secondaryInfo[i].SpecialityBoard_Name;
                                    }
                                }
                                if (IsBoardCertifiedValue != true)
                                {
                                    if (secondaryInfo.ElementAt(secondaryCount - 1).IsBoardCertified != null && secondaryInfo.ElementAt(secondaryCount - 1).IsBoardCertified == AHC.CD.Entities.MasterData.Enums.YesNoOption.YES.ToString())
                                    {
                                        pmodel.Specialty_PrimaryIsBoardCertified = secondaryInfo.ElementAt(secondaryCount - 1).IsBoardCertified.ToString();
                                        pmodel.Specialty_PrimaryCertificate = secondaryInfo.ElementAt(secondaryCount - 1).CertificateNumber;
                                        pmodel.Specialty_PrimaryLastRecertificationDate = ConvertToDateString(secondaryInfo.ElementAt(secondaryCount - 1).LastReCerificationDate);
                                        pmodel.Specialty_PrimaryInitialCertificationDate = ConvertToDateString(secondaryInfo.ElementAt(secondaryCount - 1).InitialCertificationDate);
                                        pmodel.Specialty_PrimaryExpirationDate = ConvertToDateString(secondaryInfo.ElementAt(secondaryCount - 1).ExpirationDate);
                                        pmodel.Specialty_PrimaryBoardName = secondaryInfo.ElementAt(secondaryCount - 1).SpecialityBoard_Name;
                                    }
                                    if (secondaryInfo.ElementAt(secondaryCount - 1) != null)
                                    {
                                        pmodel.Specialty_PrimarySpecialtyName = secondaryInfo.ElementAt(secondaryCount - 1).SpecialityName;
                                        pmodel.Specialty_PrimaryTaxonomyCode = secondaryInfo.ElementAt(secondaryCount - 1).TaxonomyCode;
                                    }

                                    if (pmodel.Specialty_PrimaryBoardName == "National Commission on Certification of Physician Assistants")
                                    {
                                        pmodel.Specialty_NCCPAIsBoardCertified = secondaryInfo.ElementAt(secondaryCount - 1).IsBoardCertified.ToString();
                                        pmodel.Specialty_NCCPACertificate = secondaryInfo.ElementAt(secondaryCount - 1).CertificateNumber;
                                        pmodel.Specialty_NCCPALastRecertificationDate = ConvertToDateString(secondaryInfo.ElementAt(secondaryCount - 1).LastReCerificationDate);
                                        pmodel.Specialty_NCCPAInitialCertificationDate = ConvertToDateString(secondaryInfo.ElementAt(secondaryCount - 1).InitialCertificationDate);
                                        pmodel.Specialty_NCCPAExpirationDate = ConvertToDateString(secondaryInfo.ElementAt(secondaryCount - 1).ExpirationDate);
                                        pmodel.Specialty_Primary_NCCPABoardName = secondaryInfo.ElementAt(secondaryCount - 1).SpecialityBoard_Name;
                                    }

                                }
                            }
                            if (secondaryInfo.Count == 1)
                            {
                                if (secondaryInfo.ElementAt(secondaryCount - 1) != null)
                                {
                                    pmodel.Specialty_SpecialtyName1 = secondaryInfo.ElementAt(secondaryCount - 1).SpecialityName;
                                    pmodel.Specialty_TaxonomyCode1 = secondaryInfo.ElementAt(secondaryCount - 1).TaxonomyCode;

                                    if (secondaryInfo.ElementAt(secondaryCount - 1).IsBoardCertified != null && secondaryInfo.ElementAt(secondaryCount - 1).IsBoardCertified == AHC.CD.Entities.MasterData.Enums.YesNoOption.YES.ToString())
                                    {
                                        pmodel.Specialty_IsBoardCertified1 = secondaryInfo.ElementAt(secondaryCount - 1).IsBoardCertified.ToString();
                                        pmodel.Specialty_Certificate1 = secondaryInfo.ElementAt(secondaryCount - 1).CertificateNumber;
                                        pmodel.Specialty_LastRecertificationDate1 = ConvertToDateString(secondaryInfo.ElementAt(secondaryCount - 1).LastReCerificationDate);
                                        pmodel.Specialty_InitialCertificationDate1 = ConvertToDateString(secondaryInfo.ElementAt(secondaryCount - 1).InitialCertificationDate);
                                        pmodel.Specialty_ExpirationDate1 = ConvertToDateString(secondaryInfo.ElementAt(secondaryCount - 1).ExpirationDate);
                                        pmodel.Specialty_BoardName1 = secondaryInfo.ElementAt(secondaryCount - 1).SpecialityBoard_Name;

                                    }
                                }
                            }
                        }
                        if (secondaryInfo.Count > 1)
                        {
                            if (secondaryInfo.ElementAt(secondaryCount - 2) != null)
                            {
                                pmodel.Specialty_SpecialtyName1 = secondaryInfo.ElementAt(secondaryCount - 2).SpecialityName;
                                pmodel.Specialty_TaxonomyCode1 = secondaryInfo.ElementAt(secondaryCount - 2).TaxonomyCode;

                                if (secondaryInfo.ElementAt(secondaryCount - 2).IsBoardCertified != null && secondaryInfo.ElementAt(secondaryCount - 2).IsBoardCertified == AHC.CD.Entities.MasterData.Enums.YesNoOption.YES.ToString())
                                {
                                    pmodel.Specialty_IsBoardCertified1 = secondaryInfo.ElementAt(secondaryCount - 2).IsBoardCertified.ToString();
                                    pmodel.Specialty_Certificate1 = secondaryInfo.ElementAt(secondaryCount - 2).CertificateNumber;
                                    pmodel.Specialty_LastRecertificationDate1 = ConvertToDateString(secondaryInfo.ElementAt(secondaryCount - 2).LastReCerificationDate);
                                    pmodel.Specialty_InitialCertificationDate1 = ConvertToDateString(secondaryInfo.ElementAt(secondaryCount - 2).InitialCertificationDate);
                                    pmodel.Specialty_ExpirationDate1 = ConvertToDateString(secondaryInfo.ElementAt(secondaryCount - 2).ExpirationDate);
                                    pmodel.Specialty_BoardName1 = secondaryInfo.ElementAt(secondaryCount - 2).SpecialityBoard_Name;

                                }
                            }

                        }
                        if (secondaryInfo.Count > 2)
                        {
                            if (secondaryInfo.ElementAt(secondaryCount - 3) != null)
                            {
                                pmodel.Specialty_SpecialtyName2 = secondaryInfo.ElementAt(secondaryCount - 3).SpecialityName;
                                pmodel.Specialty_TaxonomyCode2 = secondaryInfo.ElementAt(secondaryCount - 3).TaxonomyCode;
                            }
                        }
                    }
                }
                #endregion

                #region New Practice Location
                if (practicelocationlist.Count > 0)
                {
                    var primaryPracticeLocationDetails = practicelocationlist.Where(s => s.IsPrimary == AHC.CD.Entities.MasterData.Enums.YesNoOption.YES.ToString() && s.Practicelocationstatus == AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString()).ToList();
                    var secondaryPracticeLocationDetails = practicelocationlist.Where(s => s.IsPrimary == AHC.CD.Entities.MasterData.Enums.YesNoOption.NO.ToString() && s.Practicelocationstatus == AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString()).ToList();

                    if (primaryPracticeLocationDetails.Count > 0)
                    {

                        #region Primary primaryPracticeLocationInfo

                        var primaryPracticeLocationInfo = (from data in primaryPracticeLocationDetails
                                                           group data by new
                                                           {
                                                               data.PracticeLocationDetailID,
                                                               data.CurrentlyPracticingAtThisAddress,
                                                               data.Building,
                                                               data.City,
                                                               data.Country,
                                                               data.County,
                                                               data.Telephone,
                                                               data.State,
                                                               data.Fax,
                                                               data.Street,
                                                               data.ZipCode,
                                                               data.EmailAddress,
                                                               data.PracticeType_Title,
                                                               data.MaximumAge,
                                                               data.MinimumAge,
                                                               data.PracticeLocation_CorporateName,
                                                               data.PracticeLocation_FacilityName,
                                                               data.PracticeLocation_OtherPracticeName,
                                                               data.PrimaryTaxId,
                                                               data.StartDate
                                                           } into addressdata
                                                           select new
                                                           {
                                                               PracticeLocationDetailID = addressdata.Key.PracticeLocationDetailID,
                                                               CurrentlyPracticingAtThisAddress = addressdata.Key.CurrentlyPracticingAtThisAddress,
                                                               PracticeLocationCorporateName = addressdata.Key.PracticeLocation_CorporateName,
                                                               PracticeLocation_FacilityName = addressdata.Key.PracticeLocation_FacilityName,
                                                               PracticeLocation_OtherPracticeName = addressdata.Key.PracticeLocation_OtherPracticeName,
                                                               PrimaryTaxId = addressdata.Key.PrimaryTaxId,
                                                               StartDate = addressdata.Key.StartDate,
                                                               PracticeType_Title = addressdata.Key.PracticeType_Title,
                                                               State = addressdata.Key.State,
                                                               Street = addressdata.Key.Street,
                                                               ZipCode = addressdata.Key.ZipCode,
                                                               Building = addressdata.Key.Building,
                                                               City = addressdata.Key.City,
                                                               Country = addressdata.Key.Country,
                                                               County = addressdata.Key.County,
                                                               Telephone = addressdata.Key.Telephone,
                                                               Fax = addressdata.Key.Fax,
                                                               EmailAddress = addressdata.Key.EmailAddress,
                                                               MaximumAge = addressdata.Key.MaximumAge,
                                                               MinimumAge = addressdata.Key.MinimumAge

                                                           }).ToList();

                        #endregion

                        #region Primary BusinessOfficeManagerOrStaff
                        var NonEnglishlanguageData = (from data in primaryPracticeLocationDetails
                                                      group data by new { data.NonEnglishLanguage_Language, data.NonEnglishLanguage_Status, data.NonEnglishLanguage_StatusType } into languagesdata
                                                      select new
                                                      {
                                                          Status = languagesdata.Key.NonEnglishLanguage_Status,
                                                          StatusType = languagesdata.Key.NonEnglishLanguage_StatusType,
                                                          Language = languagesdata.Key.NonEnglishLanguage_Language
                                                      }).ToList();
                        #endregion

                        #region Primary BusinessOfficeManagerOrStaff
                        var BusinessOfficeManagerOrStaff = (from data in primaryPracticeLocationDetails
                                                            group data by new
                                                            {
                                                                data.BusinessOfficeManagerOrStaff_Building,
                                                                data.BusinessOfficeManagerOrStaff_City,
                                                                data.BusinessOfficeManagerOrStaff_Country,
                                                                data.BusinessOfficeManagerOrStaff_CountryCodeFax,
                                                                data.BusinessOfficeManagerOrStaff_CountryCodeTelephone,
                                                                data.BusinessOfficeManagerOrStaff_County,
                                                                data.BusinessOfficeManagerOrStaff_EmailAddress,
                                                                data.BusinessOfficeManagerOrStaff_Fax,
                                                                data.BusinessOfficeManagerOrStaff_FaxNumber,
                                                                data.BusinessOfficeManagerOrStaff_FirstName,
                                                                data.BusinessOfficeManagerOrStaff_LastName,
                                                                data.BusinessOfficeManagerOrStaff_MiddleName,
                                                                data.BusinessOfficeManagerOrStaff_MobileNumber,
                                                                data.BusinessOfficeManagerOrStaff_POBoxAddress,
                                                                data.BusinessOfficeManagerOrStaff_State,
                                                                data.BusinessOfficeManagerOrStaff_Street,
                                                                data.BusinessOfficeManagerOrStaff_Telephone,
                                                                data.BusinessOfficeManagerOrStaff_ZipCode


                                                            } into BOMdata
                                                            select new
                                                            {
                                                                Building = BOMdata.Key.BusinessOfficeManagerOrStaff_Building,
                                                                City = BOMdata.Key.BusinessOfficeManagerOrStaff_City,
                                                                Country = BOMdata.Key.BusinessOfficeManagerOrStaff_Country,
                                                                CountryCodeFax = BOMdata.Key.BusinessOfficeManagerOrStaff_CountryCodeFax,
                                                                CountryCodeTelephone = BOMdata.Key.BusinessOfficeManagerOrStaff_CountryCodeTelephone,
                                                                County = BOMdata.Key.BusinessOfficeManagerOrStaff_County,
                                                                EmailAddress = BOMdata.Key.BusinessOfficeManagerOrStaff_EmailAddress,
                                                                Fax = BOMdata.Key.BusinessOfficeManagerOrStaff_Fax,
                                                                FaxNumber = BOMdata.Key.BusinessOfficeManagerOrStaff_FaxNumber,
                                                                FirstName = BOMdata.Key.BusinessOfficeManagerOrStaff_FirstName,
                                                                LastName = BOMdata.Key.BusinessOfficeManagerOrStaff_LastName,
                                                                MiddleName = BOMdata.Key.BusinessOfficeManagerOrStaff_MiddleName,
                                                                MobileNumber = BOMdata.Key.BusinessOfficeManagerOrStaff_MobileNumber,
                                                                POBoxAddress = BOMdata.Key.BusinessOfficeManagerOrStaff_POBoxAddress,
                                                                State = BOMdata.Key.BusinessOfficeManagerOrStaff_State,
                                                                Street = BOMdata.Key.BusinessOfficeManagerOrStaff_Street,
                                                                Telephone = BOMdata.Key.BusinessOfficeManagerOrStaff_Telephone,
                                                                ZipCode = BOMdata.Key.BusinessOfficeManagerOrStaff_ZipCode
                                                            }).ToList().LastOrDefault();
                        #endregion

                        #region Primary BillingContactPerson
                        var BillingContactPerson = (from data in primaryPracticeLocationDetails
                                                    group data by new
                                                    {
                                                        data.BillingContactPerson_POBoxAddress,
                                                        data.BillingContactPerson_Building,
                                                        data.BillingContactPerson_City,
                                                        data.BillingContactPerson_Country,
                                                        data.BillingContactPerson_CountryCodeFax,
                                                        data.BillingContactPerson_CountryCodeTelephone,
                                                        data.BillingContactPerson_County,
                                                        data.BillingContactPerson_EmailAddress,
                                                        data.BillingContactPerson_Fax,
                                                        data.BillingContactPerson_FaxNumber,
                                                        data.BillingContactPerson_FirstName,
                                                        data.BillingContactPerson_LastName,
                                                        data.BillingContactPerson_MiddleName,
                                                        data.BillingContactPerson_MobileNumber,
                                                        data.BillingContactPerson_State,
                                                        data.BillingContactPerson_Street,
                                                        data.BillingContactPerson_Telephone,
                                                        data.BillingContactPerson_ZipCode


                                                    } into BOMdata
                                                    select new
                                                    {
                                                        Building = BOMdata.Key.BillingContactPerson_Building,
                                                        City = BOMdata.Key.BillingContactPerson_City,
                                                        Country = BOMdata.Key.BillingContactPerson_Country,
                                                        CountryCodeFax = BOMdata.Key.BillingContactPerson_CountryCodeFax,
                                                        CountryCodeTelephone = BOMdata.Key.BillingContactPerson_CountryCodeTelephone,
                                                        County = BOMdata.Key.BillingContactPerson_County,
                                                        EmailAddress = BOMdata.Key.BillingContactPerson_EmailAddress,
                                                        Fax = BOMdata.Key.BillingContactPerson_Fax,
                                                        FaxNumber = BOMdata.Key.BillingContactPerson_FaxNumber,
                                                        FirstName = BOMdata.Key.BillingContactPerson_FirstName,
                                                        LastName = BOMdata.Key.BillingContactPerson_LastName,
                                                        MiddleName = BOMdata.Key.BillingContactPerson_MiddleName,
                                                        MobileNumber = BOMdata.Key.BillingContactPerson_MobileNumber,
                                                        POBoxAddress = BOMdata.Key.BillingContactPerson_POBoxAddress,
                                                        State = BOMdata.Key.BillingContactPerson_State,
                                                        Street = BOMdata.Key.BillingContactPerson_Street,
                                                        Telephone = BOMdata.Key.BillingContactPerson_Telephone,
                                                        ZipCode = BOMdata.Key.BillingContactPerson_ZipCode
                                                    }).ToList().LastOrDefault();
                        #endregion

                        #region Primary PaymentAndRemittance
                        var PaymentAndRemittance = (from data in primaryPracticeLocationDetails
                                                    group data by new
                                                    {
                                                        data.PracticePaymentAndRemittance_Building,
                                                        data.PracticePaymentAndRemittance_City,
                                                        data.PracticePaymentAndRemittance_Country,
                                                        data.PracticePaymentAndRemittance_CountryCodeFax,
                                                        data.PracticePaymentAndRemittance_CountryCodeTelephone,
                                                        data.PracticePaymentAndRemittance_County,
                                                        data.PracticePaymentAndRemittance_EmailAddress,
                                                        data.PracticePaymentAndRemittance_Fax,
                                                        data.PracticePaymentAndRemittance_FaxNumber,
                                                        data.PracticePaymentAndRemittance_FirstName,
                                                        data.PracticePaymentAndRemittance_LastName,
                                                        data.PracticePaymentAndRemittance_MiddleName,
                                                        data.PracticePaymentAndRemittance_MobileNumber,
                                                        data.PracticePaymentAndRemittance_POBoxAddress,
                                                        data.PracticePaymentAndRemittance_State,
                                                        data.PracticePaymentAndRemittance_Street,
                                                        data.PracticePaymentAndRemittance_Telephone,
                                                        data.PracticePaymentAndRemittance_ZipCode,
                                                        data.ElectronicBillingCapability,
                                                        data.BillingDepartment,
                                                        data.CheckPayableTo,
                                                        data.Office


                                                    } into PPRdata
                                                    select new
                                                    {
                                                        Building = PPRdata.Key.PracticePaymentAndRemittance_Building,
                                                        City = PPRdata.Key.PracticePaymentAndRemittance_City,
                                                        Country = PPRdata.Key.PracticePaymentAndRemittance_Country,
                                                        CountryCodeFax = PPRdata.Key.PracticePaymentAndRemittance_CountryCodeFax,
                                                        CountryCodeTelephone = PPRdata.Key.PracticePaymentAndRemittance_CountryCodeTelephone,
                                                        County = PPRdata.Key.PracticePaymentAndRemittance_County,
                                                        EmailAddress = PPRdata.Key.PracticePaymentAndRemittance_EmailAddress,
                                                        Fax = PPRdata.Key.PracticePaymentAndRemittance_Fax,
                                                        FaxNumber = PPRdata.Key.PracticePaymentAndRemittance_FaxNumber,
                                                        FirstName = PPRdata.Key.PracticePaymentAndRemittance_FirstName,
                                                        LastName = PPRdata.Key.PracticePaymentAndRemittance_LastName,
                                                        MiddleName = PPRdata.Key.PracticePaymentAndRemittance_MiddleName,
                                                        MobileNumber = PPRdata.Key.PracticePaymentAndRemittance_MobileNumber,
                                                        POBoxAddress = PPRdata.Key.PracticePaymentAndRemittance_POBoxAddress,
                                                        State = PPRdata.Key.PracticePaymentAndRemittance_State,
                                                        Street = PPRdata.Key.PracticePaymentAndRemittance_Street,
                                                        Telephone = PPRdata.Key.PracticePaymentAndRemittance_Telephone,
                                                        ZipCode = PPRdata.Key.PracticePaymentAndRemittance_ZipCode,

                                                        ElectronicBillingCapability = PPRdata.Key.ElectronicBillingCapability,
                                                        BillingDepartment = PPRdata.Key.BillingDepartment,
                                                        CheckPayableTo = PPRdata.Key.CheckPayableTo,
                                                        Office = PPRdata.Key.Office

                                                    }).ToList().LastOrDefault();
                        #endregion

                        #region Primary OfficeHours
                        var OfficeHours = (from data in primaryPracticeLocationDetails
                                           group data by new { data.PracticeLocationDetailID, data.PracticeDailyHour_StartTime, data.PracticeDailyHour_EndTime, data.PracticeDailyHour_Day } into officehoursdata
                                           select new
                                           {
                                               PracticeLocationDetailID = officehoursdata.Key.PracticeLocationDetailID,
                                               Day = officehoursdata.Key.PracticeDailyHour_Day,
                                               StartTime = officehoursdata.Key.PracticeDailyHour_StartTime,
                                               EndTime = officehoursdata.Key.PracticeDailyHour_EndTime

                                           }).ToList();
                        #endregion

                        #region Primary PracticeProviders
                        var PracticeProviders = (from data in primaryPracticeLocationDetails
                                                 group data by new
                                                 {
                                                     data.PracticeProvider_FirstName,
                                                     data.PracticeProvider_LastName,
                                                     data.PracticeProvider_MiddleName,
                                                     data.PracticeProvider_Practice,
                                                     data.PracticeProvider_PracticeType,
                                                     data.PracticeProvider_Status,
                                                     data.PracticeProvider_StatusType,
                                                     data.PracticeProvider_Telephone
                                                 } into practiceprovidersdata
                                                 select new
                                                 {
                                                     FirstName = practiceprovidersdata.Key.PracticeProvider_FirstName,
                                                     LastName = practiceprovidersdata.Key.PracticeProvider_LastName,
                                                     MiddleName = practiceprovidersdata.Key.PracticeProvider_MiddleName,
                                                     Practice = practiceprovidersdata.Key.PracticeProvider_Practice,
                                                     PracticeType = practiceprovidersdata.Key.PracticeProvider_PracticeType,
                                                     Status = practiceprovidersdata.Key.PracticeProvider_Status,
                                                     StatusType = practiceprovidersdata.Key.PracticeProvider_StatusType,
                                                     Telephone = practiceprovidersdata.Key.PracticeProvider_Telephone
                                                 }).ToList();
                        #endregion

                        #region Primary PracticeProvidersSpeciality
                        var PracticeProvidersSpeciality = (from data in primaryPracticeLocationDetails
                                                           group data by new
                                                           {
                                                               data.PracticeProviderSpecialty_Name,
                                                               data.PracticeProviderSpecialty_Status,
                                                               data.PracticeProviderSpecialty_StatusType
                                                           } into practiceprovidersdata
                                                           select new
                                                           {
                                                               Name = practiceprovidersdata.Key.PracticeProviderSpecialty_Name,
                                                               Status = practiceprovidersdata.Key.PracticeProviderSpecialty_Status,
                                                               StatusType = practiceprovidersdata.Key.PracticeProviderSpecialty_StatusType
                                                           }).ToList();
                        #endregion

                        #region Primary CredentialingContactInformation
                        var CredentialingContactInformation = (from data in primaryPracticeLocationDetails
                                                               group data by new
                                                               {
                                                                   data.CredentialingContact_FirstName,
                                                                   data.CredentialingContact_MiddleName,
                                                                   data.CredentialingContact_LastName,
                                                                   data.CredentialingContact_Building,
                                                                   data.CredentialingContact_City,
                                                                   data.CredentialingContact_Country,
                                                                   data.CredentialingContact_CountryCodeFax,
                                                                   data.CredentialingContact_CountryCodeTelephone,
                                                                   data.CredentialingContact_County,
                                                                   data.CredentialingContact_EmailAddress,
                                                                   data.CredentialingContact_Fax,
                                                                   data.CredentialingContact_FaxNumber,
                                                                   data.CredentialingContact_MobileNumber,
                                                                   data.CredentialingContact_POBoxAddress,
                                                                   data.CredentialingContact_State,
                                                                   data.CredentialingContact_Street,
                                                                   data.CredentialingContact_Telephone,
                                                                   data.CredentialingContact_ZipCode
                                                               } into CCInfo
                                                               select new
                                                               {
                                                                   Building = CCInfo.Key.CredentialingContact_Building,
                                                                   City = CCInfo.Key.CredentialingContact_City,
                                                                   Country = CCInfo.Key.CredentialingContact_Country,
                                                                   CountryCodeFax = CCInfo.Key.CredentialingContact_CountryCodeFax,
                                                                   CountryCodeTelephone = CCInfo.Key.CredentialingContact_CountryCodeTelephone,
                                                                   County = CCInfo.Key.CredentialingContact_County,
                                                                   EmailAddress = CCInfo.Key.CredentialingContact_EmailAddress,
                                                                   Fax = CCInfo.Key.CredentialingContact_Fax,
                                                                   FaxNumber = CCInfo.Key.CredentialingContact_FaxNumber,
                                                                   FirstName = CCInfo.Key.CredentialingContact_FirstName,
                                                                   LastName = CCInfo.Key.CredentialingContact_LastName,
                                                                   MiddleName = CCInfo.Key.CredentialingContact_MiddleName,
                                                                   MobileNumber = CCInfo.Key.CredentialingContact_MobileNumber,
                                                                   POBoxAddress = CCInfo.Key.CredentialingContact_POBoxAddress,
                                                                   State = CCInfo.Key.CredentialingContact_State,
                                                                   Street = CCInfo.Key.CredentialingContact_Street,
                                                                   Telephone = CCInfo.Key.CredentialingContact_Telephone,
                                                                   ZipCode = CCInfo.Key.CredentialingContact_ZipCode
                                                               }).ToList().LastOrDefault();
                        #endregion

                        #region Primary Practice Location 1

                        var practiceLocationCount = primaryPracticeLocationInfo.Count;
                        var primaryPracticeLocation = primaryPracticeLocationDetails;
                        var practiceLocationID = primaryPracticeLocation[practiceLocationCount - 1].PracticeLocationDetailID;


                        //var secondarypracticeLocationID = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PracticeLocationDetailID;

                        if (practiceLocationCount > 0)
                        {
                            #region Address 1

                            if (primaryPracticeLocationInfo != null)
                            {
                                pmodel.General_PracticeLocationAddress1 = primaryPracticeLocationInfo[practiceLocationCount - 1].Street + " " + primaryPracticeLocationInfo[practiceLocationCount - 1].City + ", " + primaryPracticeLocationInfo[practiceLocationCount - 1].State + ", " + primaryPracticeLocationInfo[practiceLocationCount - 1].ZipCode;

                                if (primaryPracticeLocationInfo[practiceLocationCount - 1].Telephone != null)
                                {
                                    pmodel.General_PhoneFirstThreeDigit1 = primaryPracticeLocationInfo[practiceLocationCount - 1].Telephone.Substring(0, primaryPracticeLocationInfo[practiceLocationCount - 1].Telephone.Length - 7);
                                    pmodel.General_PhoneSecondThreeDigit1 = primaryPracticeLocationInfo[practiceLocationCount - 1].Telephone.Substring(3, 3);
                                    pmodel.General_PhoneLastFourDigit1 = primaryPracticeLocationInfo[practiceLocationCount - 1].Telephone.Substring(6);

                                    pmodel.General_Phone1 = pmodel.General_PhoneFirstThreeDigit1 + "-" + pmodel.General_PhoneSecondThreeDigit1 + "-" + pmodel.General_PhoneLastFourDigit1;
                                    pmodel.LocationAddress_Line3 = "Phone : " + pmodel.General_Phone1;
                                }

                                pmodel.General_Email1 = primaryPracticeLocationInfo[practiceLocationCount - 1].EmailAddress;

                                if (primaryPracticeLocationInfo[practiceLocationCount - 1].Fax != null)
                                {
                                    pmodel.General_FaxFirstThreeDigit1 = primaryPracticeLocationInfo[practiceLocationCount - 1].Fax.Substring(0, primaryPracticeLocationInfo[practiceLocationCount - 1].Fax.Length - 7);
                                    pmodel.General_FaxSecondThreeDigit1 = primaryPracticeLocationInfo[practiceLocationCount - 1].Fax.Substring(3, 3);
                                    pmodel.General_FaxLastFourDigit1 = primaryPracticeLocationInfo[practiceLocationCount - 1].Fax.Substring(6);

                                    pmodel.General_Fax1 = pmodel.General_FaxFirstThreeDigit1 + "-" + pmodel.General_FaxSecondThreeDigit1 + "-" + pmodel.General_FaxLastFourDigit1;
                                    pmodel.LocationAddress_Line3 = pmodel.LocationAddress_Line3 + " " + "Fax : " + pmodel.General_Fax1;
                                }

                                pmodel.General_AccessGroupName1 = "Access Healthcare Physicians, LLC";
                                pmodel.General_Access2GroupName1 = "Access 2 Healthcare Physicians, LLC";

                                pmodel.General_AccessGroupTaxId1 = "451444883";
                                pmodel.General_Access2GroupTaxId1 = "451024515";
                                pmodel.General_PracticeOrCorporateName1 = primaryPracticeLocationInfo[practiceLocationCount - 1].PracticeLocationCorporateName;
                                pmodel.General_FacilityName1 = primaryPracticeLocationInfo[practiceLocationCount - 1].PracticeLocation_FacilityName;
                                pmodel.General_Suite1 = primaryPracticeLocationInfo[practiceLocationCount - 1].Building;
                                pmodel.General_Street1 = primaryPracticeLocationInfo[practiceLocationCount - 1].Street;
                                pmodel.General_City1 = primaryPracticeLocationInfo[practiceLocationCount - 1].City;
                                pmodel.General_State1 = primaryPracticeLocationInfo[practiceLocationCount - 1].State;
                                pmodel.General_ZipCode1 = primaryPracticeLocationInfo[practiceLocationCount - 1].ZipCode;
                                pmodel.General_Country1 = primaryPracticeLocationInfo[practiceLocationCount - 1].Country;
                                pmodel.General_County1 = primaryPracticeLocationInfo[practiceLocationCount - 1].County;
                                pmodel.General_IsCurrentlyPracticing1 = primaryPracticeLocationInfo[practiceLocationCount - 1].CurrentlyPracticingAtThisAddress;
                                pmodel.LocationAddress_Line1 = pmodel.General_Street1 + " " + pmodel.General_Suite1;
                                pmodel.LocationAddress_Line2 = pmodel.General_City1 + ", " + pmodel.General_State1 + " " + pmodel.General_ZipCode1;

                                pmodel.General_City1State1 = pmodel.General_City1 + ", " + pmodel.General_State1 + ", " + pmodel.General_ZipCode1;
                                pmodel.General_FacilityPracticeName1 = pmodel.General_FacilityName1 + " ," + pmodel.General_PracticeOrCorporateName1;

                            }

                            #endregion

                            #region Languages 1

                            if (primaryPracticeLocationInfo[practiceLocationCount - 1] != null)
                            {

                                if (NonEnglishlanguageData != null)
                                {
                                    var languages = NonEnglishlanguageData.Where(l => l.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();
                                    if (languages.Count > 0)
                                    {
                                        foreach (var language in languages)
                                        {
                                            if (language != null && language.Language != null)
                                                pmodel.Languages_Known1 += language.Language + ",";
                                        }
                                    }
                                }
                            }

                            #endregion

                            #region Primary Credentialing Contact Information 1

                            if (CredentialingContactInformation != null)
                            {

                                if (CredentialingContactInformation.MiddleName != null)
                                {
                                    pmodel.PrimaryCredContact_FullName = CredentialingContactInformation.FirstName + " " + CredentialingContactInformation.MiddleName + " " + CredentialingContactInformation.LastName;
                                }
                                else
                                {
                                    pmodel.PrimaryCredContact_FullName = CredentialingContactInformation.FirstName + " " + CredentialingContactInformation.LastName;
                                }
                                pmodel.PrimaryCredContact_FirstName = CredentialingContactInformation.FirstName;
                                pmodel.PrimaryCredContact_MI = CredentialingContactInformation.MiddleName;
                                pmodel.PrimaryCredContact_LastName = CredentialingContactInformation.LastName;

                                pmodel.PrimaryCredContact_Street = CredentialingContactInformation.Street;
                                pmodel.PrimaryCredContact_Suite = CredentialingContactInformation.Building;
                                pmodel.PrimaryCredContact_City = CredentialingContactInformation.City;
                                pmodel.PrimaryCredContact_State = CredentialingContactInformation.State;
                                pmodel.PrimaryCredContact_ZipCode = CredentialingContactInformation.ZipCode;
                                pmodel.PrimaryCredContact_Phone = CredentialingContactInformation.Telephone;
                                pmodel.PrimaryCredContact_Fax = CredentialingContactInformation.FaxNumber;
                                pmodel.PrimaryCredContact_Email = CredentialingContactInformation.EmailAddress;
                                pmodel.PrimaryCredContact_MobileNumber = CredentialingContactInformation.MobileNumber;
                                pmodel.PrimaryCredContact_Address1 = pmodel.PrimaryCredContact_Street + ", " + pmodel.PrimaryCredContact_Suite;
                            }
                            #endregion

                            #region Open Practice Status 1

                            pmodel.OpenPractice_AgeLimitations1 = primaryPracticeLocationInfo[practiceLocationCount - 1].MinimumAge + " - " + primaryPracticeLocationInfo[practiceLocationCount - 1].MaximumAge;

                            #endregion

                            #region Office Manager 1

                            if (BusinessOfficeManagerOrStaff != null)
                            {
                                if (BusinessOfficeManagerOrStaff.MiddleName != null)
                                    pmodel.OfficeManager_Name1 = BusinessOfficeManagerOrStaff.FirstName + " " + BusinessOfficeManagerOrStaff.MiddleName + " " + BusinessOfficeManagerOrStaff.LastName;
                                else
                                    pmodel.OfficeManager_Name1 = BusinessOfficeManagerOrStaff.FirstName + " " + BusinessOfficeManagerOrStaff.LastName;

                                pmodel.OfficeManager_FirstName1 = BusinessOfficeManagerOrStaff.FirstName;
                                pmodel.OfficeManager_MiddleName1 = BusinessOfficeManagerOrStaff.MiddleName;
                                pmodel.OfficeManager_LastName1 = BusinessOfficeManagerOrStaff.LastName;
                                pmodel.OfficeManager_Email1 = BusinessOfficeManagerOrStaff.EmailAddress;
                                pmodel.OfficeManager_PoBoxAddress1 = BusinessOfficeManagerOrStaff.POBoxAddress;
                                pmodel.OfficeManager_Building1 = BusinessOfficeManagerOrStaff.Building;
                                pmodel.OfficeManager_Street1 = BusinessOfficeManagerOrStaff.Street;
                                pmodel.OfficeManager_City1 = BusinessOfficeManagerOrStaff.City;
                                pmodel.OfficeManager_State1 = BusinessOfficeManagerOrStaff.State;
                                pmodel.OfficeManager_ZipCode1 = BusinessOfficeManagerOrStaff.ZipCode;
                                pmodel.OfficeManager_Country1 = BusinessOfficeManagerOrStaff.Country;
                                pmodel.OfficeManager_County1 = BusinessOfficeManagerOrStaff.County;

                                if (BusinessOfficeManagerOrStaff.Telephone != null)
                                {
                                    pmodel.OfficeManager_PhoneFirstThreeDigit1 = BusinessOfficeManagerOrStaff.Telephone.Substring(0, BusinessOfficeManagerOrStaff.Telephone.Length - 7);
                                    pmodel.OfficeManager_PhoneSecondThreeDigit1 = BusinessOfficeManagerOrStaff.Telephone.Substring(3, 3);
                                    pmodel.OfficeManager_PhoneLastFourDigit1 = BusinessOfficeManagerOrStaff.Telephone.Substring(6);

                                    pmodel.OfficeManager_Phone1 = pmodel.OfficeManager_PhoneFirstThreeDigit1 + "-" + pmodel.OfficeManager_PhoneSecondThreeDigit1 + "-" + pmodel.OfficeManager_PhoneLastFourDigit1;

                                }
                                if (BusinessOfficeManagerOrStaff.Fax != null)
                                {
                                    pmodel.OfficeManager_FaxFirstThreeDigit1 = BusinessOfficeManagerOrStaff.Fax.Substring(0, BusinessOfficeManagerOrStaff.Fax.Length - 7);
                                    pmodel.OfficeManager_FaxSecondThreeDigit1 = BusinessOfficeManagerOrStaff.Fax.Substring(3, 3);
                                    pmodel.OfficeManager_FaxLastFourDigit1 = BusinessOfficeManagerOrStaff.Fax.Substring(6);

                                    pmodel.OfficeManager_Fax1 = pmodel.OfficeManager_FaxFirstThreeDigit1 + "-" + pmodel.OfficeManager_FaxSecondThreeDigit1 + "-" + pmodel.OfficeManager_FaxLastFourDigit1;

                                }
                            }

                            #endregion

                            #region Billing Contact 1

                            if (BillingContactPerson != null)
                            {
                                if (BillingContactPerson.MiddleName != null)
                                    pmodel.BillingContact_Name1 = BillingContactPerson.FirstName + " " + BillingContactPerson.MiddleName + " " + BillingContactPerson.LastName;
                                else
                                    pmodel.BillingContact_Name1 = BillingContactPerson.FirstName + " " + BillingContactPerson.LastName;

                                pmodel.BillingContact_FirstName1 = BillingContactPerson.FirstName;
                                pmodel.BillingContact_MiddleName1 = BillingContactPerson.MiddleName;
                                pmodel.BillingContact_LastName1 = BillingContactPerson.LastName;
                                pmodel.BillingContact_Email1 = BillingContactPerson.EmailAddress;
                                //pmodel.BillingContact_Phone1 = BillingContactPerson.MobileNumber;
                                //pmodel.BillingContact_Fax1 = BillingContactPerson.FaxNumber;
                                pmodel.BillingContact_POBoxAddress1 = BillingContactPerson.POBoxAddress;
                                pmodel.BillingContact_Suite1 = BillingContactPerson.Building;
                                pmodel.BillingContact_Street1 = BillingContactPerson.Street;
                                pmodel.BillingContact_City1 = BillingContactPerson.City;
                                pmodel.BillingContact_State1 = BillingContactPerson.State;
                                pmodel.BillingContact_ZipCode1 = BillingContactPerson.ZipCode;
                                pmodel.BillingContact_Country1 = BillingContactPerson.Country;
                                pmodel.BillingContact_County1 = BillingContactPerson.County;
                                pmodel.BillingContact_Line1 = pmodel.BillingContact_Street1 + " " + pmodel.BillingContact_Suite1;
                                pmodel.BillingContact_Line2 = pmodel.BillingContact_City1 + ", " + pmodel.BillingContact_State1 + " " + pmodel.BillingContact_ZipCode1;
                                pmodel.BillingContact_City1State1 = pmodel.BillingContact_City1 + " ," + pmodel.BillingContact_State1 + ", " + pmodel.BillingContact_ZipCode1;
                                if (BillingContactPerson.Telephone != null)
                                {
                                    pmodel.BillingContact_PhoneFirstThreeDigit1 = BillingContactPerson.Telephone.Substring(0, BillingContactPerson.Telephone.Length - 7);
                                    pmodel.BillingContact_PhoneSecondThreeDigit1 = BillingContactPerson.Telephone.Substring(3, 3);
                                    pmodel.BillingContact_PhoneLastFourDigit1 = BillingContactPerson.Telephone.Substring(6);
                                    pmodel.BillingContact_Phone1 = pmodel.BillingContact_PhoneFirstThreeDigit1 + "-" + pmodel.BillingContact_PhoneSecondThreeDigit1 + "-" + pmodel.BillingContact_PhoneLastFourDigit1;

                                }
                                if (BillingContactPerson.Fax != null)
                                {
                                    pmodel.BillingContact_FaxFirstThreeDigit1 = BillingContactPerson.Fax.Substring(0, BillingContactPerson.Fax.Length - 7);
                                    pmodel.BillingContact_FaxSecondThreeDigit1 = BillingContactPerson.Fax.Substring(3, 3);
                                    pmodel.BillingContact_FaxLastFourDigit1 = BillingContactPerson.Fax.Substring(6);

                                    pmodel.BillingContact_Fax1 = pmodel.BillingContact_FaxFirstThreeDigit1 + "-" + pmodel.BillingContact_FaxSecondThreeDigit1 + "-" + pmodel.BillingContact_FaxLastFourDigit1;
                                }
                            }

                            #endregion

                            #region Payment and Remittance 1

                            if (PaymentAndRemittance != null)
                            {
                                if (PaymentAndRemittance.MiddleName != null)
                                    pmodel.PaymentRemittance_Name1 = PaymentAndRemittance.FirstName + " " + PaymentAndRemittance.MiddleName + " " + PaymentAndRemittance.LastName;
                                else
                                    pmodel.PaymentRemittance_Name1 = PaymentAndRemittance.FirstName + " " + PaymentAndRemittance.LastName;

                                pmodel.PaymentRemittance_FirstName1 = PaymentAndRemittance.FirstName;
                                pmodel.PaymentRemittance_MiddleName1 = PaymentAndRemittance.MiddleName;
                                pmodel.PaymentRemittance_LastName1 = PaymentAndRemittance.LastName;
                                pmodel.PaymentRemittance_Email1 = PaymentAndRemittance.EmailAddress;
                                pmodel.PaymentRemittance_POBoxAddress1 = PaymentAndRemittance.POBoxAddress;
                                pmodel.PaymentRemittance_Suite1 = PaymentAndRemittance.Building;
                                pmodel.PaymentRemittance_Street1 = PaymentAndRemittance.Street;
                                pmodel.PaymentRemittance_City1 = PaymentAndRemittance.City;
                                pmodel.PaymentRemittance_State1 = PaymentAndRemittance.State;
                                pmodel.PaymentRemittance_ZipCode1 = PaymentAndRemittance.ZipCode;
                                pmodel.PaymentRemittance_Country1 = PaymentAndRemittance.Country;
                                pmodel.PaymentRemittance_County1 = PaymentAndRemittance.County;

                                if (PaymentAndRemittance.Telephone != null)
                                {
                                    pmodel.PaymentRemittance_PhoneFirstThreeDigit1 = PaymentAndRemittance.Telephone.Substring(0, PaymentAndRemittance.Telephone.Length - 7);
                                    pmodel.PaymentRemittance_PhoneSecondThreeDigit1 = PaymentAndRemittance.Telephone.Substring(3, 3);
                                    pmodel.PaymentRemittance_PhoneLastFourDigit1 = PaymentAndRemittance.Telephone.Substring(6);

                                    pmodel.PaymentRemittance_Phone1 = pmodel.PaymentRemittance_PhoneFirstThreeDigit1 + "-" + pmodel.PaymentRemittance_PhoneSecondThreeDigit1 + "-" + pmodel.PaymentRemittance_PhoneLastFourDigit1;
                                    if (pmodel.PaymentRemittance_Phone1.Length > 13)
                                    {
                                        pmodel.PaymentRemittance_Phone1 = PaymentAndRemittance.Telephone;
                                    }

                                }




                                if (PaymentAndRemittance.Fax != null)
                                {
                                    //pmodel.General_Fax1 = practiceLocation[practiceLocationCount - 1].Facility.Fax.Substring(3, practiceLocation[practiceLocationCount - 1].Facility.Fax.Length - 3);

                                    pmodel.PaymentRemittance_FaxFirstThreeDigit1 = PaymentAndRemittance.Fax.Substring(0, PaymentAndRemittance.Fax.Length - 7);
                                    pmodel.PaymentRemittance_FaxSecondThreeDigit1 = PaymentAndRemittance.Fax.Substring(3, 3);
                                    pmodel.PaymentRemittance_FaxLastFourDigit1 = PaymentAndRemittance.Fax.Substring(6);

                                    pmodel.PaymentRemittance_Fax1 = PaymentAndRemittance.CountryCodeFax+ "-"+pmodel.PaymentRemittance_FaxFirstThreeDigit1 + "-" + pmodel.PaymentRemittance_FaxSecondThreeDigit1 + "-" + pmodel.PaymentRemittance_FaxLastFourDigit1;
                                }

                                pmodel.PaymentRemittance_ElectronicBillCapability1 = PaymentAndRemittance.ElectronicBillingCapability;
                                pmodel.PaymentRemittance_BillingDepartment1 = PaymentAndRemittance.BillingDepartment;
                                pmodel.PaymentRemittance_ChekPayableTo1 = PaymentAndRemittance.CheckPayableTo;
                                pmodel.PaymentRemittance_Office1 = PaymentAndRemittance.Office;
                            }

                            #endregion

                            #region Office Hours 1

                            if (OfficeHours != null)
                            {
                                if (OfficeHours.Count > 0)
                                {
                                    var officeHourDetails = (from office in OfficeHours
                                                             where office.PracticeLocationDetailID == practiceLocationID
                                                             group new { office.StartTime, office.EndTime, office.Day } by office.PracticeLocationDetailID into officeHoursData
                                                             select new { PracticeLocationID = officeHoursData.Key, OfficeHours = officeHoursData.ToList() }).FirstOrDefault();



                                    if (officeHourDetails.OfficeHours.Count > 1)
                                    {
                                        if (officeHourDetails.OfficeHours.ElementAt(0).StartTime != null)
                                            pmodel.OfficeHour_StartMonday1 = ConvertTimeFormat(officeHourDetails.OfficeHours.ElementAt(0).StartTime);
                                        if (officeHourDetails.OfficeHours.ElementAt(0).EndTime != null)
                                            pmodel.OfficeHour_EndMonday1 = ConvertTimeFormat(officeHourDetails.OfficeHours.ElementAt(0).EndTime);

                                        pmodel.OfficeHour_Monday1 = pmodel.OfficeHour_StartMonday1 + " - " + pmodel.OfficeHour_EndMonday1;

                                        if (officeHourDetails.OfficeHours.ElementAt(1).StartTime != null)
                                            pmodel.OfficeHour_StartTuesday1 = ConvertTimeFormat(officeHourDetails.OfficeHours.ElementAt(1).StartTime);
                                        if (officeHourDetails.OfficeHours.ElementAt(1).EndTime != null)
                                            pmodel.OfficeHour_EndTuesday1 = ConvertTimeFormat(officeHourDetails.OfficeHours.ElementAt(1).EndTime);

                                        pmodel.OfficeHour_Tuesday1 = pmodel.OfficeHour_StartTuesday1 + " - " + pmodel.OfficeHour_EndTuesday1;

                                        if (officeHourDetails.OfficeHours.ElementAt(2).StartTime != null)
                                            pmodel.OfficeHour_StartWednesday1 = ConvertTimeFormat(officeHourDetails.OfficeHours.ElementAt(2).StartTime);
                                        if (officeHourDetails.OfficeHours.ElementAt(2).EndTime != null)

                                            pmodel.OfficeHour_EndWednesday1 = ConvertTimeFormat(officeHourDetails.OfficeHours.ElementAt(2).EndTime);

                                        pmodel.OfficeHour_Wednesday1 = pmodel.OfficeHour_StartWednesday1 + " - " + pmodel.OfficeHour_EndWednesday1;


                                        if (officeHourDetails.OfficeHours.ElementAt(3).StartTime != null)
                                            pmodel.OfficeHour_StartThursday1 = ConvertTimeFormat(officeHourDetails.OfficeHours.ElementAt(3).StartTime);
                                        if (officeHourDetails.OfficeHours.ElementAt(3).EndTime != null)
                                            pmodel.OfficeHour_EndThursday1 = ConvertTimeFormat(officeHourDetails.OfficeHours.ElementAt(3).EndTime);

                                        pmodel.OfficeHour_Thursday1 = pmodel.OfficeHour_StartThursday1 + " - " + pmodel.OfficeHour_EndThursday1;



                                        if (officeHourDetails.OfficeHours.ElementAt(4).StartTime != null)
                                            pmodel.OfficeHour_StartFriday1 = ConvertTimeFormat(officeHourDetails.OfficeHours.ElementAt(4).StartTime);
                                        if (officeHourDetails.OfficeHours.ElementAt(4).EndTime != null)
                                            pmodel.OfficeHour_EndFriday1 = ConvertTimeFormat(officeHourDetails.OfficeHours.ElementAt(4).EndTime);

                                        pmodel.OfficeHour_Friday1 = pmodel.OfficeHour_StartFriday1 + " - " + pmodel.OfficeHour_EndFriday1;


                                        if (officeHourDetails.OfficeHours.ElementAt(5).StartTime != null)
                                            pmodel.OfficeHour_StartSaturday1 = ConvertTimeFormat(officeHourDetails.OfficeHours.ElementAt(5).StartTime);
                                        if (officeHourDetails.OfficeHours.ElementAt(5).EndTime != null)

                                            pmodel.OfficeHour_EndSaturday1 = ConvertTimeFormat(officeHourDetails.OfficeHours.ElementAt(5).EndTime);

                                        pmodel.OfficeHour_Saturday1 = pmodel.OfficeHour_StartSaturday1 + " - " + pmodel.OfficeHour_EndSaturday1;


                                        if (officeHourDetails.OfficeHours.ElementAt(6).StartTime != null)
                                            pmodel.OfficeHour_StartSunday1 = ConvertTimeFormat(officeHourDetails.OfficeHours.ElementAt(6).StartTime);
                                        if (officeHourDetails.OfficeHours.ElementAt(6).EndTime != null)
                                            pmodel.OfficeHour_EndSunday1 = ConvertTimeFormat(officeHourDetails.OfficeHours.ElementAt(6).EndTime);

                                        pmodel.OfficeHour_Sunday1 = pmodel.OfficeHour_StartSunday1 + " - " + pmodel.OfficeHour_EndSunday1;
                                    }
                                }

                            }

                            #endregion

                            #region Supervising Provider 1

                            var supervisingProvider = PracticeProviders.
                            Where(s => s.Practice == AHC.CD.Entities.MasterData.Enums.PracticeType.Supervisor.ToString() &&
                                s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();

                            var supervisorCount = supervisingProvider.Count;

                            if (supervisorCount > 0)
                            {
                                pmodel.CoveringColleague_FirstName1 = supervisingProvider.ElementAt(supervisorCount - 1).FirstName;
                                pmodel.CoveringColleague_MiddleName1 = supervisingProvider.ElementAt(supervisorCount - 1).MiddleName;
                                pmodel.CoveringColleague_LastName1 = supervisingProvider.ElementAt(supervisorCount - 1).LastName;

                                if (pmodel.CoveringColleague_MiddleName1 != null)
                                    pmodel.CoveringColleague_FullName1 = pmodel.CoveringColleague_FirstName1 + " " + pmodel.CoveringColleague_MiddleName1 + " " + pmodel.CoveringColleague_LastName1;
                                else
                                    pmodel.CoveringColleague_FullName1 = pmodel.CoveringColleague_FirstName1 + " " + pmodel.CoveringColleague_LastName1;


                                if (supervisingProvider.ElementAt(supervisorCount - 1).Telephone != null)
                                {
                                    pmodel.CoveringColleague_PhoneFirstThreeDigit1 = supervisingProvider.ElementAt(supervisorCount - 1).Telephone.Substring(0, supervisingProvider.ElementAt(supervisorCount - 1).Telephone.Length - 7);
                                    pmodel.CoveringColleague_PhoneSecondThreeDigit1 = supervisingProvider.ElementAt(supervisorCount - 1).Telephone.Substring(3, 3);
                                    pmodel.CoveringColleague_PhoneLastFourDigit1 = supervisingProvider.ElementAt(supervisorCount - 1).Telephone.Substring(6);

                                    pmodel.CoveringColleague_PhoneNumber1 = pmodel.CoveringColleague_PhoneFirstThreeDigit1 + "-" + pmodel.CoveringColleague_PhoneSecondThreeDigit1 + "-" + pmodel.CoveringColleague_PhoneLastFourDigit1;

                                }

                                var specialities = PracticeProvidersSpeciality.Where(s => s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString()).ToList();

                                if (specialities.Count > 0)
                                {
                                    if (specialities.ElementAt(specialities.Count - 1) != null)
                                        pmodel.CoveringColleague_Specialty1 = specialities.ElementAt(specialities.Count - 1).Name;
                                }
                            }

                            #endregion

                            #region Covering Colleagues/Partners 1

                            var patners = PracticeProviders.
                            Where(s => s.Practice == AHC.CD.Entities.MasterData.Enums.PracticeType.CoveringColleague.ToString() &&
                                s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();

                            var patnersCount = patners.Count;

                            if (patnersCount > 0)
                            {
                                pmodel.Patners_FirstName1 = patners.ElementAt(patnersCount - 1).FirstName;
                                pmodel.Patners_MiddleName1 = patners.ElementAt(patnersCount - 1).MiddleName;
                                pmodel.Patners_LastName1 = patners.ElementAt(patnersCount - 1).LastName;

                                if (pmodel.Patners_MiddleName1 != null)
                                    pmodel.Patners_FullName1 = pmodel.Patners_FirstName1 + " " + pmodel.Patners_MiddleName1 + " " + pmodel.Patners_LastName1;
                                else
                                    pmodel.Patners_FullName1 = pmodel.Patners_FirstName1 + " " + pmodel.Patners_LastName1;

                            }

                            #endregion

                            #region Primary Practice Location Specific

                            #region Primary Address

                            if (primaryPracticeLocationInfo != null)
                            {
                                pmodel.General_PracticeLocationAddress = primaryPracticeLocationInfo[practiceLocationCount - 1].Street + " " + primaryPracticeLocationInfo[practiceLocationCount - 1].City + ", " + primaryPracticeLocationInfo[practiceLocationCount - 1].State + ", " + primaryPracticeLocationInfo[practiceLocationCount - 1].ZipCode;

                                if (primaryPracticeLocationInfo[practiceLocationCount - 1].Telephone != null)
                                {
                                    pmodel.General_PhoneFirstThreeDigit = primaryPracticeLocationInfo[practiceLocationCount - 1].Telephone.Substring(0, primaryPracticeLocationInfo[practiceLocationCount - 1].Telephone.Length - 7);                                    
                                    pmodel.General_PhoneSecondThreeDigit = primaryPracticeLocationInfo[practiceLocationCount - 1].Telephone.Substring(3, 3);
                                    pmodel.General_PhoneLastFourDigit = primaryPracticeLocationInfo[practiceLocationCount - 1].Telephone.Substring(6);

                                    pmodel.General_Phone = pmodel.General_PhoneFirstThreeDigit + "-" + pmodel.General_PhoneSecondThreeDigit + "-" + pmodel.General_PhoneLastFourDigit;
                                    if (pmodel.General_Phone.Length > 13)
                                    {
                                        pmodel.General_Phone = primaryPracticeLocationInfo[practiceLocationCount - 1].Telephone;
                                    }

                                    pmodel.Primary_LocationAddress_Line3 = "Phone : " + pmodel.General_Phone;
                                }

                                pmodel.General_Email1 = primaryPracticeLocationInfo[practiceLocationCount - 1].EmailAddress;

                                if (primaryPracticeLocationInfo[practiceLocationCount - 1].Fax != null)
                                {
                                    pmodel.General_FaxFirstThreeDigit = primaryPracticeLocationInfo[practiceLocationCount - 1].Fax.Substring(0, primaryPracticeLocationInfo[practiceLocationCount - 1].Fax.Length - 7);
                                    pmodel.General_FaxSecondThreeDigit = primaryPracticeLocationInfo[practiceLocationCount - 1].Fax.Substring(3, 3);
                                    pmodel.General_FaxLastFourDigit = primaryPracticeLocationInfo[practiceLocationCount - 1].Fax.Substring(6);

                                    pmodel.General_Fax = pmodel.General_FaxFirstThreeDigit + "-" + pmodel.General_FaxSecondThreeDigit + "-" + pmodel.General_FaxLastFourDigit;
                                    if (pmodel.General_Fax.Length > 13)
                                    {
                                        pmodel.General_Fax = primaryPracticeLocationInfo[practiceLocationCount - 1].Fax;
                                    }
                                    pmodel.Primary_LocationAddress_Line3 = pmodel.Primary_LocationAddress_Line3 + " " + "Fax : " + pmodel.General_Fax;
                                }

                                pmodel.General_AccessGroupName = "Access Healthcare Physicians, LLC";
                                pmodel.General_Access2GroupName = "Access 2 Healthcare Physicians, LLC";

                                pmodel.General_AccessGroupTaxId = "451444883";
                                pmodel.General_Access2GroupTaxId = "451024515";

                                pmodel.General_PracticeOrCorporateName = primaryPracticeLocationInfo[practiceLocationCount - 1].PracticeLocationCorporateName;
                                pmodel.General_FacilityName = primaryPracticeLocationInfo[practiceLocationCount - 1].PracticeLocation_FacilityName;
                                pmodel.General_Suite = primaryPracticeLocationInfo[practiceLocationCount - 1].Building;
                                pmodel.General_Street = primaryPracticeLocationInfo[practiceLocationCount - 1].Street;
                                pmodel.General_City = primaryPracticeLocationInfo[practiceLocationCount - 1].City;
                                pmodel.General_State = primaryPracticeLocationInfo[practiceLocationCount - 1].State;
                                pmodel.General_ZipCode = primaryPracticeLocationInfo[practiceLocationCount - 1].ZipCode;
                                pmodel.General_Country = primaryPracticeLocationInfo[practiceLocationCount - 1].Country;
                                pmodel.General_County = primaryPracticeLocationInfo[practiceLocationCount - 1].County;
                                pmodel.General_IsCurrentlyPracticing = primaryPracticeLocationInfo[practiceLocationCount - 1].CurrentlyPracticingAtThisAddress;
                                pmodel.Primary_LocationAddress_Line1 = pmodel.General_Street + " " + pmodel.General_Suite;
                                pmodel.Primary_LocationAddress_Line2 = pmodel.General_City + ", " + pmodel.General_State + " " + pmodel.General_ZipCode;

                                pmodel.General_CityState = pmodel.General_City + ", " + pmodel.General_State + ", " + pmodel.General_ZipCode;
                                pmodel.General_FacilityPracticeName = pmodel.General_FacilityName + " ," + pmodel.General_PracticeOrCorporateName;

                            }

                            #endregion

                            #region Primary Credentialing Contact Information

                            if (CredentialingContactInformation != null)
                            {

                                if (CredentialingContactInformation.MiddleName != null)
                                {
                                    pmodel.PrimaryCredContact_FullName1 = CredentialingContactInformation.FirstName + " " + CredentialingContactInformation.MiddleName + " " + CredentialingContactInformation.LastName;
                                }
                                else
                                {
                                    pmodel.PrimaryCredContact_FullName1 = CredentialingContactInformation.FirstName + " " + CredentialingContactInformation.LastName;
                                }
                                pmodel.PrimaryCredContact_FirstName1 = CredentialingContactInformation.FirstName;
                                pmodel.PrimaryCredContact_MI1 = CredentialingContactInformation.MiddleName;
                                pmodel.PrimaryCredContact_LastName1 = CredentialingContactInformation.LastName;

                                pmodel.PrimaryCredContact_Street1 = CredentialingContactInformation.Street;
                                pmodel.PrimaryCredContact_Suite1 = CredentialingContactInformation.Building;
                                pmodel.PrimaryCredContact_City1 = CredentialingContactInformation.City;
                                pmodel.PrimaryCredContact_State1 = CredentialingContactInformation.State;
                                pmodel.PrimaryCredContact_ZipCode1 = CredentialingContactInformation.ZipCode;
                                pmodel.PrimaryCredContact_Phone1 = CredentialingContactInformation.Telephone;
                                pmodel.PrimaryCredContact_Fax1 = CredentialingContactInformation.FaxNumber;
                                pmodel.PrimaryCredContact_Email1 = CredentialingContactInformation.EmailAddress;
                                pmodel.PrimaryCredContact_MobileNumber1 = CredentialingContactInformation.MobileNumber;
                                pmodel.PrimaryCredContact1_Address1 = pmodel.PrimaryCredContact_Street + ", " + pmodel.PrimaryCredContact_Suite;
                            }
                            #endregion

                            #region Primary Open Practice Status

                            pmodel.OpenPractice_AgeLimitations = primaryPracticeLocationInfo[practiceLocationCount - 1].MinimumAge + " - " + primaryPracticeLocationInfo[practiceLocationCount - 1].MaximumAge;

                            #endregion

                            #region Primary Billing Contact 1

                            if (BillingContactPerson != null)
                            {
                                if (BillingContactPerson.MiddleName != null)
                                    pmodel.BillingContact_Name = BillingContactPerson.FirstName + " " + BillingContactPerson.MiddleName + " " + BillingContactPerson.LastName;
                                else
                                    pmodel.BillingContact_Name = BillingContactPerson.FirstName + " " + BillingContactPerson.LastName;

                                pmodel.BillingContact_FirstName = BillingContactPerson.FirstName;
                                pmodel.BillingContact_MiddleName = BillingContactPerson.MiddleName;
                                pmodel.BillingContact_LastName = BillingContactPerson.LastName;
                                pmodel.BillingContact_Email = BillingContactPerson.EmailAddress;
                                //pmodel.BillingContact_Phone = BillingContactPerson.MobileNumber;
                                //pmodel.BillingContact_Fax = BillingContactPerson.FaxNumber;
                                pmodel.BillingContact_POBoxAddress = BillingContactPerson.POBoxAddress;
                                pmodel.BillingContact_Suite = BillingContactPerson.Building;
                                pmodel.BillingContact_Street = BillingContactPerson.Street;
                                pmodel.BillingContact_City = BillingContactPerson.City;
                                pmodel.BillingContact_State = BillingContactPerson.State;
                                pmodel.BillingContact_ZipCode = BillingContactPerson.ZipCode;
                                pmodel.BillingContact_Country = BillingContactPerson.Country;
                                pmodel.BillingContact_County = BillingContactPerson.County;
                                pmodel.Primary_BillingContact_Line1 = pmodel.BillingContact_Street + " " + pmodel.BillingContact_Suite;
                                pmodel.Primary_BillingContact_Line2 = pmodel.BillingContact_City + ", " + pmodel.BillingContact_State + " " + pmodel.BillingContact_ZipCode;
                                pmodel.BillingContact_CityState = pmodel.BillingContact_City + " ," + pmodel.BillingContact_State + ", " + pmodel.BillingContact_ZipCode;
                                if (BillingContactPerson.Telephone != null)
                                {
                                    pmodel.BillingContact_PhoneFirstThreeDigit = BillingContactPerson.Telephone.Substring(0, BillingContactPerson.Telephone.Length - 7);
                                    pmodel.BillingContact_PhoneSecondThreeDigit = BillingContactPerson.Telephone.Substring(3, 3);
                                    pmodel.BillingContact_PhoneLastFourDigit = BillingContactPerson.Telephone.Substring(6);
                                    pmodel.BillingContact_Phone = pmodel.BillingContact_PhoneFirstThreeDigit + "-" + pmodel.BillingContact_PhoneSecondThreeDigit + "-" + pmodel.BillingContact_PhoneLastFourDigit;

                                }
                                if (BillingContactPerson.Fax != null)
                                {
                                    pmodel.BillingContact_FaxFirstThreeDigit = BillingContactPerson.Fax.Substring(0, BillingContactPerson.Fax.Length - 7);
                                    pmodel.BillingContact_FaxSecondThreeDigit = BillingContactPerson.Fax.Substring(3, 3);
                                    pmodel.BillingContact_FaxLastFourDigit = BillingContactPerson.Fax.Substring(6);

                                    pmodel.BillingContact_Fax = pmodel.BillingContact_FaxFirstThreeDigit + "-" + pmodel.BillingContact_FaxSecondThreeDigit + "-" + pmodel.BillingContact_FaxLastFourDigit;
                                }
                            }

                            #endregion

                            #region Primary Office Hours

                            if (OfficeHours != null)
                            {
                                if (OfficeHours.Count > 0)
                                {
                                    var officeHourDetails = (from office in OfficeHours
                                                             where office.PracticeLocationDetailID == practiceLocationID
                                                             group new { office.StartTime, office.EndTime, office.Day } by office.PracticeLocationDetailID into officeHoursData
                                                             select new { PracticeLocationID = officeHoursData.Key, OfficeHours = officeHoursData.ToList() }).FirstOrDefault();



                                    if (officeHourDetails.OfficeHours.Count > 1)
                                    {
                                        if (officeHourDetails.OfficeHours.ElementAt(0).StartTime != null)
                                            pmodel.OfficeHour_StartMonday = ConvertTimeFormat(officeHourDetails.OfficeHours.ElementAt(0).StartTime);
                                        if (officeHourDetails.OfficeHours.ElementAt(0).EndTime != null)
                                            pmodel.OfficeHour_EndMonday = ConvertTimeFormat(officeHourDetails.OfficeHours.ElementAt(0).EndTime);

                                        pmodel.OfficeHour_Monday = pmodel.OfficeHour_StartMonday + " - " + pmodel.OfficeHour_EndMonday;

                                        if (officeHourDetails.OfficeHours.ElementAt(1).StartTime != null)
                                            pmodel.OfficeHour_StartTuesday = ConvertTimeFormat(officeHourDetails.OfficeHours.ElementAt(1).StartTime);
                                        if (officeHourDetails.OfficeHours.ElementAt(1).EndTime != null)
                                            pmodel.OfficeHour_EndTuesday = ConvertTimeFormat(officeHourDetails.OfficeHours.ElementAt(1).EndTime);

                                        pmodel.OfficeHour_Tuesday = pmodel.OfficeHour_StartTuesday + " - " + pmodel.OfficeHour_EndTuesday;

                                        if (officeHourDetails.OfficeHours.ElementAt(2).StartTime != null)
                                            pmodel.OfficeHour_StartWednesday = ConvertTimeFormat(officeHourDetails.OfficeHours.ElementAt(2).StartTime);
                                        if (officeHourDetails.OfficeHours.ElementAt(2).EndTime != null)

                                            pmodel.OfficeHour_EndWednesday = ConvertTimeFormat(officeHourDetails.OfficeHours.ElementAt(2).EndTime);

                                        pmodel.OfficeHour_Wednesday = pmodel.OfficeHour_StartWednesday + " - " + pmodel.OfficeHour_EndWednesday;


                                        if (officeHourDetails.OfficeHours.ElementAt(3).StartTime != null)
                                            pmodel.OfficeHour_StartThursday = ConvertTimeFormat(officeHourDetails.OfficeHours.ElementAt(3).StartTime);
                                        if (officeHourDetails.OfficeHours.ElementAt(3).EndTime != null)
                                            pmodel.OfficeHour_EndThursday = ConvertTimeFormat(officeHourDetails.OfficeHours.ElementAt(3).EndTime);

                                        pmodel.OfficeHour_Thursday = pmodel.OfficeHour_StartThursday + " - " + pmodel.OfficeHour_EndThursday;



                                        if (officeHourDetails.OfficeHours.ElementAt(4).StartTime != null)
                                            pmodel.OfficeHour_StartFriday = ConvertTimeFormat(officeHourDetails.OfficeHours.ElementAt(4).StartTime);
                                        if (officeHourDetails.OfficeHours.ElementAt(4).EndTime != null)
                                            pmodel.OfficeHour_EndFriday = ConvertTimeFormat(officeHourDetails.OfficeHours.ElementAt(4).EndTime);

                                        pmodel.OfficeHour_Friday = pmodel.OfficeHour_StartFriday + " - " + pmodel.OfficeHour_EndFriday;


                                        if (officeHourDetails.OfficeHours.ElementAt(5).StartTime != null)
                                            pmodel.OfficeHour_StartSaturday = ConvertTimeFormat(officeHourDetails.OfficeHours.ElementAt(5).StartTime);
                                        if (officeHourDetails.OfficeHours.ElementAt(5).EndTime != null)

                                            pmodel.OfficeHour_EndSaturday = ConvertTimeFormat(officeHourDetails.OfficeHours.ElementAt(5).EndTime);

                                        pmodel.OfficeHour_Saturday = pmodel.OfficeHour_StartSaturday + " - " + pmodel.OfficeHour_EndSaturday;


                                        if (officeHourDetails.OfficeHours.ElementAt(6).StartTime != null)
                                            pmodel.OfficeHour_StartSunday = ConvertTimeFormat(officeHourDetails.OfficeHours.ElementAt(6).StartTime);
                                        if (officeHourDetails.OfficeHours.ElementAt(6).EndTime != null)
                                            pmodel.OfficeHour_EndSunday = ConvertTimeFormat(officeHourDetails.OfficeHours.ElementAt(6).EndTime);

                                        pmodel.OfficeHour_Sunday = pmodel.OfficeHour_StartSunday + " - " + pmodel.OfficeHour_EndSunday;
                                    }
                                }

                            }

                            #endregion

                            #endregion
                        }
                        #endregion
                        // SECONDARY PRACTICE LOCATION HAVING PRIMARY PRACTICE LOCATION
                        if (secondaryPracticeLocationDetails.Count > 0)
                        {
                            #region SecondaryPracticeLocationInfo
                            var SecondaryPracticeLocationInfo = (from data in secondaryPracticeLocationDetails
                                                                 group data by new
                                                                 {
                                                                     data.CurrentlyPracticingAtThisAddress,
                                                                     data.Building,
                                                                     data.City,
                                                                     data.Country,
                                                                     data.County,
                                                                     data.Telephone,
                                                                     data.State,
                                                                     data.Fax,
                                                                     data.Street,
                                                                     data.ZipCode,
                                                                     data.EmailAddress,
                                                                     data.PracticeType_Title,
                                                                     data.MaximumAge,
                                                                     data.MinimumAge,
                                                                     data.PracticeLocation_CorporateName,
                                                                     data.PracticeLocation_OtherPracticeName,
                                                                     data.PracticeLocation_FacilityName,
                                                                     data.PrimaryTaxId,
                                                                     data.StartDate
                                                                 } into addressdata
                                                                 select new
                                                                 {
                                                                     CurrentlyPracticingAtThisAddress = addressdata.Key.CurrentlyPracticingAtThisAddress,
                                                                     PracticeLocationCorporateName = addressdata.Key.PracticeLocation_CorporateName,
                                                                     PracticeLocation_OtherPracticeName = addressdata.Key.PracticeLocation_OtherPracticeName,
                                                                     PracticeLocation_FacilityName = addressdata.Key.PracticeLocation_FacilityName,
                                                                     PrimaryTaxId = addressdata.Key.PrimaryTaxId,
                                                                     StartDate = addressdata.Key.StartDate,
                                                                     PracticeType_Title = addressdata.Key.PracticeType_Title,
                                                                     State = addressdata.Key.State,
                                                                     Street = addressdata.Key.Street,
                                                                     ZipCode = addressdata.Key.ZipCode,
                                                                     Building = addressdata.Key.Building,
                                                                     City = addressdata.Key.City,
                                                                     Country = addressdata.Key.Country,
                                                                     County = addressdata.Key.County,
                                                                     Telephone = addressdata.Key.Telephone,
                                                                     Fax = addressdata.Key.Fax,
                                                                     EmailAddress = addressdata.Key.EmailAddress,
                                                                     MaximumAge = addressdata.Key.MaximumAge,
                                                                     MinimumAge = addressdata.Key.MinimumAge
                                                                 }).ToList();
                            #endregion

                            #region  SecondaryNonEnglishlanguageData

                            var SecondaryNonEnglishlanguageData = (from data in secondaryPracticeLocationDetails
                                                                   group data by new { data.NonEnglishLanguage_Language, data.NonEnglishLanguage_Status, data.NonEnglishLanguage_StatusType } into languagesdata
                                                                   select new
                                                                   {
                                                                       Status = languagesdata.Key.NonEnglishLanguage_Status,
                                                                       StatusType = languagesdata.Key.NonEnglishLanguage_StatusType,
                                                                       Language = languagesdata.Key.NonEnglishLanguage_Language
                                                                   }).ToList();
                            #endregion

                            #region  SecondaryBusinessOfficeManagerOrStaff

                            var SecondaryBusinessOfficeManagerOrStaff = (from data in secondaryPracticeLocationDetails
                                                                         group data by new
                                                                         {
                                                                             data.BusinessOfficeManagerOrStaff_Building,
                                                                             data.BusinessOfficeManagerOrStaff_City,
                                                                             data.BusinessOfficeManagerOrStaff_Country,
                                                                             data.BusinessOfficeManagerOrStaff_CountryCodeFax,
                                                                             data.BusinessOfficeManagerOrStaff_CountryCodeTelephone,
                                                                             data.BusinessOfficeManagerOrStaff_County,
                                                                             data.BusinessOfficeManagerOrStaff_EmailAddress,
                                                                             data.BusinessOfficeManagerOrStaff_Fax,
                                                                             data.BusinessOfficeManagerOrStaff_FaxNumber,
                                                                             data.BusinessOfficeManagerOrStaff_FirstName,
                                                                             data.BusinessOfficeManagerOrStaff_LastName,
                                                                             data.BusinessOfficeManagerOrStaff_MiddleName,
                                                                             data.BusinessOfficeManagerOrStaff_MobileNumber,
                                                                             data.BusinessOfficeManagerOrStaff_POBoxAddress,
                                                                             data.BusinessOfficeManagerOrStaff_State,
                                                                             data.BusinessOfficeManagerOrStaff_Street,
                                                                             data.BusinessOfficeManagerOrStaff_Telephone,
                                                                             data.BusinessOfficeManagerOrStaff_ZipCode


                                                                         } into BOMdata
                                                                         select new
                                                                         {
                                                                             Building = BOMdata.Key.BusinessOfficeManagerOrStaff_Building,
                                                                             City = BOMdata.Key.BusinessOfficeManagerOrStaff_City,
                                                                             Country = BOMdata.Key.BusinessOfficeManagerOrStaff_Country,
                                                                             CountryCodeFax = BOMdata.Key.BusinessOfficeManagerOrStaff_CountryCodeFax,
                                                                             CountryCodeTelephone = BOMdata.Key.BusinessOfficeManagerOrStaff_CountryCodeTelephone,
                                                                             County = BOMdata.Key.BusinessOfficeManagerOrStaff_County,
                                                                             EmailAddress = BOMdata.Key.BusinessOfficeManagerOrStaff_EmailAddress,
                                                                             Fax = BOMdata.Key.BusinessOfficeManagerOrStaff_Fax,
                                                                             FaxNumber = BOMdata.Key.BusinessOfficeManagerOrStaff_FaxNumber,
                                                                             FirstName = BOMdata.Key.BusinessOfficeManagerOrStaff_FirstName,
                                                                             LastName = BOMdata.Key.BusinessOfficeManagerOrStaff_LastName,
                                                                             MiddleName = BOMdata.Key.BusinessOfficeManagerOrStaff_MiddleName,
                                                                             MobileNumber = BOMdata.Key.BusinessOfficeManagerOrStaff_MobileNumber,
                                                                             POBoxAddress = BOMdata.Key.BusinessOfficeManagerOrStaff_POBoxAddress,
                                                                             State = BOMdata.Key.BusinessOfficeManagerOrStaff_State,
                                                                             Street = BOMdata.Key.BusinessOfficeManagerOrStaff_Street,
                                                                             Telephone = BOMdata.Key.BusinessOfficeManagerOrStaff_Telephone,
                                                                             ZipCode = BOMdata.Key.BusinessOfficeManagerOrStaff_ZipCode
                                                                         }).ToList().LastOrDefault();
                            #endregion

                            #region  SecondaryBillingContactPerson
                            var SecondaryBillingContactPerson = (from data in secondaryPracticeLocationDetails
                                                                 group data by new
                                                                 {
                                                                     data.BillingContactPerson_POBoxAddress,
                                                                     data.BillingContactPerson_Building,
                                                                     data.BillingContactPerson_City,
                                                                     data.BillingContactPerson_Country,
                                                                     data.BillingContactPerson_CountryCodeFax,
                                                                     data.BillingContactPerson_CountryCodeTelephone,
                                                                     data.BillingContactPerson_County,
                                                                     data.BillingContactPerson_EmailAddress,
                                                                     data.BillingContactPerson_Fax,
                                                                     data.BillingContactPerson_FaxNumber,
                                                                     data.BillingContactPerson_FirstName,
                                                                     data.BillingContactPerson_LastName,
                                                                     data.BillingContactPerson_MiddleName,
                                                                     data.BillingContactPerson_MobileNumber,
                                                                     data.BillingContactPerson_State,
                                                                     data.BillingContactPerson_Street,
                                                                     data.BillingContactPerson_Telephone,
                                                                     data.BillingContactPerson_ZipCode


                                                                 } into BOMdata
                                                                 select new
                                                                 {
                                                                     Building = BOMdata.Key.BillingContactPerson_Building,
                                                                     City = BOMdata.Key.BillingContactPerson_City,
                                                                     Country = BOMdata.Key.BillingContactPerson_Country,
                                                                     CountryCodeFax = BOMdata.Key.BillingContactPerson_CountryCodeFax,
                                                                     CountryCodeTelephone = BOMdata.Key.BillingContactPerson_CountryCodeTelephone,
                                                                     County = BOMdata.Key.BillingContactPerson_County,
                                                                     EmailAddress = BOMdata.Key.BillingContactPerson_EmailAddress,
                                                                     Fax = BOMdata.Key.BillingContactPerson_Fax,
                                                                     FaxNumber = BOMdata.Key.BillingContactPerson_FaxNumber,
                                                                     FirstName = BOMdata.Key.BillingContactPerson_FirstName,
                                                                     LastName = BOMdata.Key.BillingContactPerson_LastName,
                                                                     MiddleName = BOMdata.Key.BillingContactPerson_MiddleName,
                                                                     MobileNumber = BOMdata.Key.BillingContactPerson_MobileNumber,
                                                                     POBoxAddress = BOMdata.Key.BillingContactPerson_POBoxAddress,
                                                                     State = BOMdata.Key.BillingContactPerson_State,
                                                                     Street = BOMdata.Key.BillingContactPerson_Street,
                                                                     Telephone = BOMdata.Key.BillingContactPerson_Telephone,
                                                                     ZipCode = BOMdata.Key.BillingContactPerson_ZipCode
                                                                 }).ToList().LastOrDefault();
                            #endregion

                            #region  SecondaryPaymentAndRemittance
                            var SecondaryPaymentAndRemittance = (from data in secondaryPracticeLocationDetails
                                                                 group data by new
                                                                 {
                                                                     data.PracticePaymentAndRemittance_Building,
                                                                     data.PracticePaymentAndRemittance_City,
                                                                     data.PracticePaymentAndRemittance_Country,
                                                                     data.PracticePaymentAndRemittance_CountryCodeFax,
                                                                     data.PracticePaymentAndRemittance_CountryCodeTelephone,
                                                                     data.PracticePaymentAndRemittance_County,
                                                                     data.PracticePaymentAndRemittance_EmailAddress,
                                                                     data.PracticePaymentAndRemittance_Fax,
                                                                     data.PracticePaymentAndRemittance_FaxNumber,
                                                                     data.PracticePaymentAndRemittance_FirstName,
                                                                     data.PracticePaymentAndRemittance_LastName,
                                                                     data.PracticePaymentAndRemittance_MiddleName,
                                                                     data.PracticePaymentAndRemittance_MobileNumber,
                                                                     data.PracticePaymentAndRemittance_POBoxAddress,
                                                                     data.PracticePaymentAndRemittance_State,
                                                                     data.PracticePaymentAndRemittance_Street,
                                                                     data.PracticePaymentAndRemittance_Telephone,
                                                                     data.PracticePaymentAndRemittance_ZipCode,
                                                                     data.ElectronicBillingCapability,
                                                                     data.BillingDepartment,
                                                                     data.CheckPayableTo,
                                                                     data.Office


                                                                 } into PPRdata
                                                                 select new
                                                                 {
                                                                     Building = PPRdata.Key.PracticePaymentAndRemittance_Building,
                                                                     City = PPRdata.Key.PracticePaymentAndRemittance_City,
                                                                     Country = PPRdata.Key.PracticePaymentAndRemittance_Country,
                                                                     CountryCodeFax = PPRdata.Key.PracticePaymentAndRemittance_CountryCodeFax,
                                                                     CountryCodeTelephone = PPRdata.Key.PracticePaymentAndRemittance_CountryCodeTelephone,
                                                                     County = PPRdata.Key.PracticePaymentAndRemittance_County,
                                                                     EmailAddress = PPRdata.Key.PracticePaymentAndRemittance_EmailAddress,
                                                                     Fax = PPRdata.Key.PracticePaymentAndRemittance_Fax,
                                                                     FaxNumber = PPRdata.Key.PracticePaymentAndRemittance_FaxNumber,
                                                                     FirstName = PPRdata.Key.PracticePaymentAndRemittance_FirstName,
                                                                     LastName = PPRdata.Key.PracticePaymentAndRemittance_LastName,
                                                                     MiddleName = PPRdata.Key.PracticePaymentAndRemittance_MiddleName,
                                                                     MobileNumber = PPRdata.Key.PracticePaymentAndRemittance_MobileNumber,
                                                                     POBoxAddress = PPRdata.Key.PracticePaymentAndRemittance_POBoxAddress,
                                                                     State = PPRdata.Key.PracticePaymentAndRemittance_State,
                                                                     Street = PPRdata.Key.PracticePaymentAndRemittance_Street,
                                                                     Telephone = PPRdata.Key.PracticePaymentAndRemittance_Telephone,
                                                                     ZipCode = PPRdata.Key.PracticePaymentAndRemittance_ZipCode,

                                                                     ElectronicBillingCapability = PPRdata.Key.ElectronicBillingCapability,
                                                                     BillingDepartment = PPRdata.Key.BillingDepartment,
                                                                     CheckPayableTo = PPRdata.Key.CheckPayableTo,
                                                                     Office = PPRdata.Key.Office


                                                                 }).ToList().LastOrDefault();
                            #endregion

                            #region SecondaryOfficeHours

                            var SecondaryOfficeHours = (from data in secondaryPracticeLocationDetails
                                 group data by new { data.PracticeLocationDetailID, data.PracticeDailyHour_StartTime, data.PracticeDailyHour_EndTime, data.PracticeDailyHour_Day } into officehoursdata
                                 select new
                                 {
                                     PracticeLocationDetailID = officehoursdata.Key.PracticeLocationDetailID,
                                     Day = officehoursdata.Key.PracticeDailyHour_Day,
                                     StartTime = officehoursdata.Key.PracticeDailyHour_StartTime,
                                     EndTime = officehoursdata.Key.PracticeDailyHour_EndTime

                                 }).ToList();
                                
                                //(from data in secondaryPracticeLocationDetails
                                //                        group data by new { data.PracticeDailyHour_StartTime, data.PracticeDailyHour_EndTime }
                                //                        into officehoursdata
                                //                        select new
                                //                        {
                                //                            StartTime = officehoursdata.Key.PracticeDailyHour_StartTime,
                                //                            EndTime = officehoursdata.Key.PracticeDailyHour_EndTime

                                //                        }).ToList();
                            #endregion

                            #region SecondaryPracticeProviders
                            var SecondaryPracticeProviders = (from data in secondaryPracticeLocationDetails
                                                              group data by new
                                                              {
                                                                  data.PracticeProvider_FirstName,
                                                                  data.PracticeProvider_LastName,
                                                                  data.PracticeProvider_MiddleName,
                                                                  data.PracticeProvider_Practice,
                                                                  data.PracticeProvider_PracticeType,
                                                                  data.PracticeProvider_Status,
                                                                  data.PracticeProvider_StatusType,
                                                                  data.PracticeProvider_Telephone
                                                              } into practiceprovidersdata
                                                              select new
                                                              {
                                                                  FirstName = practiceprovidersdata.Key.PracticeProvider_FirstName,
                                                                  LastName = practiceprovidersdata.Key.PracticeProvider_LastName,
                                                                  MiddleName = practiceprovidersdata.Key.PracticeProvider_MiddleName,
                                                                  Practice = practiceprovidersdata.Key.PracticeProvider_Practice,
                                                                  PracticeType = practiceprovidersdata.Key.PracticeProvider_PracticeType,
                                                                  Status = practiceprovidersdata.Key.PracticeProvider_Status,
                                                                  StatusType = practiceprovidersdata.Key.PracticeProvider_StatusType,
                                                                  Telephone = practiceprovidersdata.Key.PracticeProvider_Telephone
                                                              }).ToList();
                            #endregion

                            #region SecondaryPracticeProvidersSpeciality
                            var SecondaryPracticeProvidersSpeciality = (from data in secondaryPracticeLocationDetails
                                                                        group data by new
                                                                        {
                                                                            data.PracticeProviderSpecialty_Name,
                                                                            data.PracticeProviderSpecialty_Status,
                                                                            data.PracticeProviderSpecialty_StatusType
                                                                        } into practiceprovidersdata
                                                                        select new
                                                                        {

                                                                            Name = practiceprovidersdata.Key.PracticeProviderSpecialty_Name,
                                                                            Status = practiceprovidersdata.Key.PracticeProviderSpecialty_Status,
                                                                            StatusType = practiceprovidersdata.Key.PracticeProviderSpecialty_StatusType
                                                                        }).ToList();

                            #endregion

                            #region SecondaryCredentialingContactInformation

                            var SecondaryCredentialingContactInformation = (from data in secondaryPracticeLocationDetails
                                                                            group data by new
                                                                            {
                                                                                data.CredentialingContact_FirstName,
                                                                                data.CredentialingContact_MiddleName,
                                                                                data.CredentialingContact_LastName,
                                                                                data.CredentialingContact_Building,
                                                                                data.CredentialingContact_City,
                                                                                data.CredentialingContact_Country,
                                                                                data.CredentialingContact_CountryCodeFax,
                                                                                data.CredentialingContact_CountryCodeTelephone,
                                                                                data.CredentialingContact_County,
                                                                                data.CredentialingContact_EmailAddress,
                                                                                data.CredentialingContact_Fax,
                                                                                data.CredentialingContact_FaxNumber,
                                                                                data.CredentialingContact_MobileNumber,
                                                                                data.CredentialingContact_POBoxAddress,
                                                                                data.CredentialingContact_State,
                                                                                data.CredentialingContact_Street,
                                                                                data.CredentialingContact_Telephone,
                                                                                data.CredentialingContact_ZipCode
                                                                            } into CCInfo
                                                                            select new
                                                                            {
                                                                                Building = CCInfo.Key.CredentialingContact_Building,
                                                                                City = CCInfo.Key.CredentialingContact_City,
                                                                                Country = CCInfo.Key.CredentialingContact_Country,
                                                                                CountryCodeFax = CCInfo.Key.CredentialingContact_CountryCodeFax,
                                                                                CountryCodeTelephone = CCInfo.Key.CredentialingContact_CountryCodeTelephone,
                                                                                County = CCInfo.Key.CredentialingContact_County,
                                                                                EmailAddress = CCInfo.Key.CredentialingContact_EmailAddress,
                                                                                Fax = CCInfo.Key.CredentialingContact_Fax,
                                                                                FaxNumber = CCInfo.Key.CredentialingContact_FaxNumber,
                                                                                FirstName = CCInfo.Key.CredentialingContact_FirstName,
                                                                                LastName = CCInfo.Key.CredentialingContact_LastName,
                                                                                MiddleName = CCInfo.Key.CredentialingContact_MiddleName,
                                                                                MobileNumber = CCInfo.Key.CredentialingContact_MobileNumber,
                                                                                POBoxAddress = CCInfo.Key.CredentialingContact_POBoxAddress,
                                                                                State = CCInfo.Key.CredentialingContact_State,
                                                                                Street = CCInfo.Key.CredentialingContact_Street,
                                                                                Telephone = CCInfo.Key.CredentialingContact_Telephone,
                                                                                ZipCode = CCInfo.Key.CredentialingContact_ZipCode
                                                                            }).ToList().LastOrDefault();
                            #endregion

                            // SECONDARY PRACTICE LOCATION DETAILS
                            var secondaryPracticeLocationCount = SecondaryPracticeLocationInfo.Count;
                            var secondaryPracticeLocation = secondaryPracticeLocationDetails;

                            if (SecondaryPracticeLocationInfo != null)
                            {
                                if (secondaryPracticeLocationCount > 0)
                                {
                                    #region Address 2

                                    if (SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 1] != null)
                                    {
                                        pmodel.General_PracticeLocationAddress2 = SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 1].Street + " " + SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 1].City + ", " + SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 1].State + ", " + SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 1].ZipCode;

                                        if (SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 1].Telephone != null)
                                        {
                                            pmodel.General_PhoneFirstThreeDigit2 = SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 1].Telephone.Substring(0, SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 1].Telephone.Length - 7);
                                            pmodel.General_PhoneSecondThreeDigit2 = SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 1].Telephone.Substring(3, 3);
                                            pmodel.General_PhoneLastFourDigit2 = SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 1].Telephone.Substring(6);

                                            pmodel.General_Phone2 = pmodel.General_PhoneFirstThreeDigit2 + "-" + pmodel.General_PhoneSecondThreeDigit2 + "-" + pmodel.General_PhoneLastFourDigit2;
                                            if (pmodel.General_Phone2.Length > 13)
                                            {
                                                pmodel.General_Phone2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Telephone;
                                            }
                                            pmodel.LocationAddress2_Line3 = "Phone : " + pmodel.General_Phone2;
                                        }

                                        pmodel.General_Email2 = SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 1].EmailAddress;

                                        if (SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 1].Fax != null)
                                        {
                                            pmodel.General_FaxFirstThreeDigit2 = SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 1].Fax.Substring(0, SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 1].Fax.Length - 7);
                                            pmodel.General_FaxSecondThreeDigit2 = SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 1].Fax.Substring(3, 3);
                                            pmodel.General_FaxLastFourDigit2 = SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 1].Fax.Substring(6);

                                            pmodel.General_Fax2 = pmodel.General_FaxFirstThreeDigit2 + "-" + pmodel.General_FaxSecondThreeDigit2 + "-" + pmodel.General_FaxLastFourDigit2;
                                            if (pmodel.General_Fax2.Length > 13)
                                            {
                                                pmodel.General_Fax2 = SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 1].Fax;
                                            }
                                            pmodel.LocationAddress2_Line3 = pmodel.LocationAddress2_Line3 + " " + "Fax : " + pmodel.General_Fax2;
                                        }

                                        pmodel.General_AccessGroupName2 = "Access Healthcare Physicians, LLC";
                                        pmodel.General_Access2GroupName2 = "Access 2 Healthcare Physicians, LLC";

                                        pmodel.General_AccessGroupTaxId2 = "451444883";
                                        pmodel.General_Access2GroupTaxId2 = "451024515";
                                        pmodel.General_FacilityName2 = SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 1].PracticeLocation_FacilityName;
                                        pmodel.General_PracticeOrCorporateName2 = SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 1].PracticeLocationCorporateName;
                                        pmodel.General_Suite2 = SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 1].Building;
                                        pmodel.General_Street2 = SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 1].Street;
                                        pmodel.General_City2 = SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 1].City;
                                        pmodel.General_State2 = SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 1].State;
                                        pmodel.General_ZipCode2 = SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 1].ZipCode;
                                        pmodel.General_Country2 = SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 1].Country;
                                        pmodel.General_County2 = SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 1].County;
                                        pmodel.LocationAddress2_Line1 = pmodel.General_Street2 + " " + pmodel.General_Suite2;
                                        pmodel.LocationAddress2_Line2 = pmodel.General_City2 + ", " + pmodel.General_State2 + ", " + pmodel.General_ZipCode2;
                                        pmodel.General_City2State2 = pmodel.General_City2 + ", " + pmodel.General_State2 + ", " + pmodel.General_ZipCode2;
                                        pmodel.General_FacilityPracticeName2 = pmodel.General_FacilityName2 + ", " + pmodel.General_PracticeOrCorporateName2;

                                    }

                                    #endregion

                                    #region Languages 2

                                    if (SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 1] != null)
                                    {

                                        if (SecondaryNonEnglishlanguageData != null)
                                        {
                                            var languages = SecondaryNonEnglishlanguageData.Where(l => l.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();
                                            if (languages.Count > 0)
                                            {
                                                foreach (var language in languages)
                                                {
                                                    if (language != null)
                                                        pmodel.Languages_Known1 += language.Language + ",";
                                                }
                                            }
                                        }
                                    }

                                    #endregion

                                    #region Open Practice Status 2

                                    pmodel.OpenPractice_AgeLimitations2 = SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 1].MinimumAge + " - " + SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 1].MaximumAge;

                                    #endregion

                                    #region Office Manager 2

                                    if (SecondaryBusinessOfficeManagerOrStaff != null)
                                    {
                                        if (SecondaryBusinessOfficeManagerOrStaff.MiddleName != null)
                                            pmodel.OfficeManager_Name2 = SecondaryBusinessOfficeManagerOrStaff.FirstName + " " + SecondaryBusinessOfficeManagerOrStaff.MiddleName + " " + SecondaryBusinessOfficeManagerOrStaff.LastName;
                                        else
                                            pmodel.OfficeManager_Name2 = SecondaryBusinessOfficeManagerOrStaff.FirstName + " " + SecondaryBusinessOfficeManagerOrStaff.LastName;

                                        pmodel.OfficeManager_FirstName2 = SecondaryBusinessOfficeManagerOrStaff.FirstName;
                                        pmodel.OfficeManager_MiddleName2 = SecondaryBusinessOfficeManagerOrStaff.MiddleName;
                                        pmodel.OfficeManager_LastName2 = SecondaryBusinessOfficeManagerOrStaff.LastName;
                                        pmodel.OfficeManager_Email2 = SecondaryBusinessOfficeManagerOrStaff.EmailAddress;
                                        pmodel.OfficeManager_PoBoxAddress2 = SecondaryBusinessOfficeManagerOrStaff.POBoxAddress;
                                        pmodel.OfficeManager_Building2 = SecondaryBusinessOfficeManagerOrStaff.Building;
                                        pmodel.OfficeManager_Street2 = SecondaryBusinessOfficeManagerOrStaff.Street;
                                        pmodel.OfficeManager_City2 = SecondaryBusinessOfficeManagerOrStaff.City;
                                        pmodel.OfficeManager_State2 = SecondaryBusinessOfficeManagerOrStaff.State;
                                        pmodel.OfficeManager_ZipCode2 = SecondaryBusinessOfficeManagerOrStaff.ZipCode;
                                        pmodel.OfficeManager_Country2 = SecondaryBusinessOfficeManagerOrStaff.Country;
                                        pmodel.OfficeManager_County2 = SecondaryBusinessOfficeManagerOrStaff.County;

                                        if (SecondaryBusinessOfficeManagerOrStaff.Telephone != null)
                                        {
                                            pmodel.OfficeManager_PhoneFirstThreeDigit2 = SecondaryBusinessOfficeManagerOrStaff.Telephone.Substring(0, SecondaryBusinessOfficeManagerOrStaff.Telephone.Length - 7);
                                            pmodel.OfficeManager_PhoneSecondThreeDigit2 = SecondaryBusinessOfficeManagerOrStaff.Telephone.Substring(3, 3);
                                            pmodel.OfficeManager_PhoneLastFourDigit2 = SecondaryBusinessOfficeManagerOrStaff.Telephone.Substring(6);

                                            pmodel.OfficeManager_Phone2 = pmodel.OfficeManager_PhoneFirstThreeDigit2 + "-" + pmodel.OfficeManager_PhoneSecondThreeDigit2 + "-" + pmodel.OfficeManager_PhoneLastFourDigit2;

                                        }

                                        if (SecondaryBusinessOfficeManagerOrStaff.Fax != null)
                                        {

                                            pmodel.OfficeManager_FaxFirstThreeDigit2 = SecondaryBusinessOfficeManagerOrStaff.Fax.Substring(0, SecondaryBusinessOfficeManagerOrStaff.Fax.Length - 7);
                                            pmodel.OfficeManager_FaxSecondThreeDigit2 = SecondaryBusinessOfficeManagerOrStaff.Fax.Substring(3, 3);
                                            pmodel.OfficeManager_FaxLastFourDigit2 = SecondaryBusinessOfficeManagerOrStaff.Fax.Substring(6);

                                            pmodel.OfficeManager_Fax1 = pmodel.OfficeManager_FaxFirstThreeDigit2 + "-" + pmodel.OfficeManager_FaxSecondThreeDigit2 + "-" + pmodel.OfficeManager_FaxLastFourDigit2;


                                        }
                                    }

                                    #endregion

                                    #region Billing Contact 2

                                    if (SecondaryBillingContactPerson != null)
                                    {
                                        if (SecondaryBillingContactPerson.MiddleName != null)
                                            pmodel.BillingContact_Name2 = SecondaryBillingContactPerson.FirstName + " " + SecondaryBillingContactPerson.MiddleName + " " + SecondaryBillingContactPerson.LastName;
                                        else
                                            pmodel.BillingContact_Name2 = SecondaryBillingContactPerson.FirstName + " " + SecondaryBillingContactPerson.LastName;

                                        pmodel.BillingContact_FirstName2 = SecondaryBillingContactPerson.FirstName;
                                        pmodel.BillingContact_MiddleName2 = SecondaryBillingContactPerson.MiddleName;
                                        pmodel.BillingContact_LastName2 = SecondaryBillingContactPerson.LastName;
                                        pmodel.BillingContact_Email2 = SecondaryBillingContactPerson.EmailAddress;
                                        //pmodel.BillingContact_Phone1 = SecondaryBillingContactPerson.MobileNumber;
                                        //pmodel.BillingContact_Fax1 = SecondaryBillingContactPerson.FaxNumber;
                                        pmodel.BillingContact_POBoxAddress2 = SecondaryBillingContactPerson.POBoxAddress;
                                        pmodel.BillingContact_Suite2 = SecondaryBillingContactPerson.Building;
                                        pmodel.BillingContact_Street2 = SecondaryBillingContactPerson.Street;
                                        pmodel.BillingContact_City2 = SecondaryBillingContactPerson.City;
                                        pmodel.BillingContact_State2 = SecondaryBillingContactPerson.State;
                                        pmodel.BillingContact_ZipCode2 = SecondaryBillingContactPerson.ZipCode;
                                        pmodel.BillingContact_Country2 = SecondaryBillingContactPerson.Country;
                                        pmodel.BillingContact_County2 = SecondaryBillingContactPerson.County;
                                        pmodel.BillingContact_City2State2 = pmodel.BillingContact_City2 + " ," + pmodel.BillingContact_State2 + ", " + pmodel.BillingContact_ZipCode2;
                                        if (SecondaryBillingContactPerson.Telephone != null)
                                        {
                                            pmodel.BillingContact_PhoneFirstThreeDigit2 = SecondaryBillingContactPerson.Telephone.Substring(0, SecondaryBillingContactPerson.Telephone.Length - 7);
                                            pmodel.BillingContact_PhoneSecondThreeDigit2 = SecondaryBillingContactPerson.Telephone.Substring(3, 3);
                                            pmodel.BillingContact_PhoneLastFourDigit2 = SecondaryBillingContactPerson.Telephone.Substring(6);

                                            pmodel.BillingContact_Phone2 = pmodel.BillingContact_PhoneFirstThreeDigit2 + "-" + pmodel.BillingContact_PhoneSecondThreeDigit2 + "-" + pmodel.BillingContact_PhoneLastFourDigit2;
                                        }
                                        if (SecondaryBillingContactPerson.Fax != null)
                                        {
                                            //pmodel.General_Fax1 = practiceLocation[practiceLocationCount - 2].Facility.Fax.Substring(3, practiceLocation[practiceLocationCount - 2].Facility.Fax.Length - 3);

                                            pmodel.BillingContact_FaxFirstThreeDigit2 = SecondaryBillingContactPerson.Fax.Substring(0, SecondaryBillingContactPerson.Fax.Length - 7);
                                            pmodel.BillingContact_FaxSecondThreeDigit2 = SecondaryBillingContactPerson.Fax.Substring(3, 3);
                                            pmodel.BillingContact_FaxLastFourDigit2 = SecondaryBillingContactPerson.Fax.Substring(6);

                                            pmodel.BillingContact_Fax2 = pmodel.BillingContact_FaxFirstThreeDigit2 + "-" + pmodel.BillingContact_FaxSecondThreeDigit2 + "-" + pmodel.BillingContact_FaxLastFourDigit2;
                                        }
                                    }

                                    #endregion

                                    #region Payment and Remittance 2

                                    if (SecondaryPaymentAndRemittance != null)
                                    {
                                        if (SecondaryPaymentAndRemittance.MiddleName != null)
                                            pmodel.PaymentRemittance_Name2 = SecondaryPaymentAndRemittance.FirstName + " " + SecondaryPaymentAndRemittance.MiddleName + " " + SecondaryPaymentAndRemittance.LastName;
                                        else
                                            pmodel.PaymentRemittance_Name2 = SecondaryPaymentAndRemittance.FirstName + " " + SecondaryPaymentAndRemittance.LastName;

                                        pmodel.PaymentRemittance_FirstName2 = SecondaryPaymentAndRemittance.FirstName;
                                        pmodel.PaymentRemittance_MiddleName2 = SecondaryPaymentAndRemittance.MiddleName;
                                        pmodel.PaymentRemittance_LastName2 = SecondaryPaymentAndRemittance.LastName;
                                        pmodel.PaymentRemittance_Email2 = SecondaryPaymentAndRemittance.EmailAddress;
                                        pmodel.PaymentRemittance_POBoxAddress2 = SecondaryPaymentAndRemittance.POBoxAddress;
                                        pmodel.PaymentRemittance_Suite2 = SecondaryPaymentAndRemittance.Building;
                                        pmodel.PaymentRemittance_Street2 = SecondaryPaymentAndRemittance.Street;
                                        pmodel.PaymentRemittance_City2 = SecondaryPaymentAndRemittance.City;
                                        pmodel.PaymentRemittance_State2 = SecondaryPaymentAndRemittance.State;
                                        pmodel.PaymentRemittance_ZipCode2 = SecondaryPaymentAndRemittance.ZipCode;
                                        pmodel.PaymentRemittance_Country2 = SecondaryPaymentAndRemittance.Country;
                                        pmodel.PaymentRemittance_County2 = SecondaryPaymentAndRemittance.County;

                                        if (SecondaryPaymentAndRemittance.Telephone != null)
                                        {
                                            pmodel.PaymentRemittance_PhoneFirstThreeDigit2 = SecondaryPaymentAndRemittance.Telephone.Substring(0, SecondaryPaymentAndRemittance.Telephone.Length - 7);
                                            pmodel.PaymentRemittance_PhoneSecondThreeDigit2 = SecondaryPaymentAndRemittance.Telephone.Substring(3, 3);
                                            pmodel.PaymentRemittance_PhoneLastFourDigit2 = SecondaryPaymentAndRemittance.Telephone.Substring(6);

                                            pmodel.PaymentRemittance_Phone2 = pmodel.PaymentRemittance_PhoneFirstThreeDigit2 + "-" + pmodel.PaymentRemittance_PhoneSecondThreeDigit2 + "-" + pmodel.PaymentRemittance_PhoneLastFourDigit2;
                                        }

                                        if (SecondaryPaymentAndRemittance.Fax != null)
                                        {
                                            //pmodel.General_Fax1 = practiceLocation[practiceLocationCount - 2].Facility.Fax.Substring(3, practiceLocation[practiceLocationCount - 2].Facility.Fax.Length - 3);

                                            pmodel.PaymentRemittance_FaxFirstThreeDigit2 = SecondaryPaymentAndRemittance.Fax.Substring(0, SecondaryPaymentAndRemittance.Fax.Length - 7);
                                            pmodel.PaymentRemittance_FaxSecondThreeDigit2 = SecondaryPaymentAndRemittance.Fax.Substring(3, 3);
                                            pmodel.PaymentRemittance_FaxLastFourDigit2 = SecondaryPaymentAndRemittance.Fax.Substring(6);

                                            pmodel.PaymentRemittance_Fax2 = pmodel.PaymentRemittance_FaxFirstThreeDigit2 + "-" + pmodel.PaymentRemittance_FaxSecondThreeDigit2 + "-" + pmodel.PaymentRemittance_FaxLastFourDigit2;
                                        }

                                        pmodel.PaymentRemittance_ElectronicBillCapability2 = SecondaryPaymentAndRemittance.ElectronicBillingCapability;
                                        pmodel.PaymentRemittance_BillingDepartment2 = SecondaryPaymentAndRemittance.BillingDepartment;
                                        pmodel.PaymentRemittance_ChekPayableTo2 = SecondaryPaymentAndRemittance.CheckPayableTo;
                                        pmodel.PaymentRemittance_Office2 = SecondaryPaymentAndRemittance.Office;
                                    }

                                    #endregion

                                    #region Office Hours 2

                                    if (SecondaryOfficeHours != null)
                                    {
                                        if (SecondaryOfficeHours.Count > 0)
                                        {
                                            if (SecondaryOfficeHours.ElementAt(0).StartTime != null)
                                                pmodel.OfficeHour_StartMonday2 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(0).StartTime);
                                            if (SecondaryOfficeHours.ElementAt(0).EndTime != null)
                                                pmodel.OfficeHour_EndMonday2 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(0).EndTime);

                                            pmodel.OfficeHour_Monday2 = pmodel.OfficeHour_StartMonday2 + " - " + pmodel.OfficeHour_EndMonday2;
                                            if (SecondaryOfficeHours.Count > 1)
                                            {
                                                if (SecondaryOfficeHours.ElementAt(1).StartTime != null)
                                                    pmodel.OfficeHour_StartTuesday2 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(1).StartTime);
                                                if (SecondaryOfficeHours.ElementAt(1).EndTime != null)
                                                    pmodel.OfficeHour_EndTuesday2 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(1).EndTime);
                                            }
                                            pmodel.OfficeHour_Tuesday2 = pmodel.OfficeHour_StartTuesday2 + " - " + pmodel.OfficeHour_EndTuesday2;
                                            if (SecondaryOfficeHours.Count > 2)
                                            {
                                                if (SecondaryOfficeHours.ElementAt(2).StartTime != null)
                                                    pmodel.OfficeHour_StartWednesday2 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(2).StartTime);
                                                if (SecondaryOfficeHours.ElementAt(2).EndTime != null)

                                                    pmodel.OfficeHour_EndWednesday2 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(2).EndTime);

                                                pmodel.OfficeHour_Wednesday2 = pmodel.OfficeHour_StartWednesday2 + " - " + pmodel.OfficeHour_EndWednesday2;
                                            }
                                            if (SecondaryOfficeHours.Count > 3)
                                            {


                                                if (SecondaryOfficeHours.ElementAt(3).StartTime != null)
                                                    pmodel.OfficeHour_StartThursday2 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(3).StartTime);
                                                if (SecondaryOfficeHours.ElementAt(3).EndTime != null)
                                                    pmodel.OfficeHour_EndThursday2 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(3).EndTime);

                                                pmodel.OfficeHour_Thursday2 = pmodel.OfficeHour_StartThursday2 + " - " + pmodel.OfficeHour_EndThursday2;
                                            }
                                            if (SecondaryOfficeHours.Count > 4)
                                            {


                                                if (SecondaryOfficeHours.ElementAt(4).StartTime != null)
                                                    pmodel.OfficeHour_StartFriday2 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(4).StartTime);
                                                if (SecondaryOfficeHours.ElementAt(4).EndTime != null)
                                                    pmodel.OfficeHour_EndFriday2 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(4).EndTime);

                                                pmodel.OfficeHour_Friday2 = pmodel.OfficeHour_StartFriday2 + " - " + pmodel.OfficeHour_EndFriday2;
                                            }
                                            if (SecondaryOfficeHours.Count > 5)
                                            {
                                                if (SecondaryOfficeHours.ElementAt(5).StartTime != null)
                                                    pmodel.OfficeHour_StartSaturday2 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(5).StartTime);
                                                if (SecondaryOfficeHours.ElementAt(5).EndTime != null)

                                                    pmodel.OfficeHour_EndSaturday2 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(5).EndTime);

                                                pmodel.OfficeHour_Saturday2 = pmodel.OfficeHour_StartSaturday2 + " - " + pmodel.OfficeHour_EndSaturday2;
                                            }
                                            if (SecondaryOfficeHours.Count > 6)
                                            {
                                                if (SecondaryOfficeHours.ElementAt(6).StartTime != null)
                                                    pmodel.OfficeHour_StartSunday2 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(6).StartTime);
                                                if (SecondaryOfficeHours.ElementAt(6).EndTime != null)
                                                    pmodel.OfficeHour_EndSunday2 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(6).EndTime);

                                                pmodel.OfficeHour_Sunday2 = pmodel.OfficeHour_StartSunday2 + " - " + pmodel.OfficeHour_EndSunday2;
                                            }
                                        }

                                    }

                                    #endregion

                                    #region Supervising Provider 2

                                    var supervisingProvider = SecondaryPracticeProviders.
                                    Where(s => s.Practice == AHC.CD.Entities.MasterData.Enums.PracticeType.Supervisor.ToString() &&
                                        s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();

                                    var supervisorCount = supervisingProvider.Count;

                                    if (supervisorCount > 0)
                                    {
                                        pmodel.CoveringColleague_FirstName2 = supervisingProvider.ElementAt(supervisorCount - 1).FirstName;
                                        pmodel.CoveringColleague_MiddleName2 = supervisingProvider.ElementAt(supervisorCount - 1).MiddleName;
                                        pmodel.CoveringColleague_LastName2 = supervisingProvider.ElementAt(supervisorCount - 1).LastName;

                                        if (pmodel.CoveringColleague_MiddleName2 != null)
                                            pmodel.CoveringColleague_FullName2 = pmodel.CoveringColleague_FirstName2 + " " + pmodel.CoveringColleague_MiddleName2 + " " + pmodel.CoveringColleague_LastName2;
                                        else
                                            pmodel.CoveringColleague_FullName2 = pmodel.CoveringColleague_FirstName2 + " " + pmodel.CoveringColleague_LastName2;


                                        if (supervisingProvider.ElementAt(supervisorCount - 1).Telephone != null)
                                        {
                                            pmodel.CoveringColleague_PhoneFirstThreeDigit2 = supervisingProvider.ElementAt(supervisorCount - 1).Telephone.Substring(0, supervisingProvider.ElementAt(supervisorCount - 1).Telephone.Length - 7);
                                            pmodel.CoveringColleague_PhoneSecondThreeDigit2 = supervisingProvider.ElementAt(supervisorCount - 1).Telephone.Substring(3, 3);
                                            pmodel.CoveringColleague_PhoneLastFourDigit2 = supervisingProvider.ElementAt(supervisorCount - 1).Telephone.Substring(6);

                                            pmodel.CoveringColleague_PhoneNumber2 = pmodel.CoveringColleague_PhoneFirstThreeDigit2 + "-" + pmodel.CoveringColleague_PhoneSecondThreeDigit2 + "-" + pmodel.CoveringColleague_PhoneLastFourDigit2;

                                        }

                                        var specialities = SecondaryPracticeProvidersSpeciality.Where(s => s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString()).ToList();

                                        if (specialities.Count > 0)
                                        {
                                            if (specialities.ElementAt(specialities.Count - 1) != null)
                                                pmodel.CoveringColleague_Specialty2 = specialities.ElementAt(specialities.Count - 1).Name;
                                        }
                                    }

                                    #endregion

                                    #region Covering Colleagues/Partners 2

                                    var patners = SecondaryPracticeProviders.
                                    Where(s => s.Practice == AHC.CD.Entities.MasterData.Enums.PracticeType.CoveringColleague.ToString() &&
                                        s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();

                                    var patnersCount = patners.Count;

                                    if (patnersCount > 0)
                                    {
                                        pmodel.Patners_FirstName2 = patners.ElementAt(patnersCount - 1).FirstName;
                                        pmodel.Patners_MiddleName2 = patners.ElementAt(patnersCount - 1).MiddleName;
                                        pmodel.Patners_LastName2 = patners.ElementAt(patnersCount - 1).LastName;

                                        if (pmodel.Patners_MiddleName2 != null)
                                            pmodel.Patners_FullName2 = pmodel.Patners_FirstName2 + " " + pmodel.Patners_MiddleName2 + " " + pmodel.Patners_LastName2;
                                        else
                                            pmodel.Patners_FullName2 = pmodel.Patners_FirstName2 + " " + pmodel.Patners_LastName2;

                                    }

                                    #endregion

                                    #region Credentialing Contact Information 2

                                    if (SecondaryCredentialingContactInformation != null)
                                    {

                                        if (SecondaryCredentialingContactInformation.MiddleName != null)
                                        {
                                            pmodel.PrimaryCredContact_FullName2 = SecondaryCredentialingContactInformation.FirstName + " " + SecondaryCredentialingContactInformation.MiddleName + " " + SecondaryCredentialingContactInformation.LastName;
                                        }
                                        else
                                        {
                                            pmodel.PrimaryCredContact_FullName2 = SecondaryCredentialingContactInformation.FirstName + " " + SecondaryCredentialingContactInformation.LastName;
                                        }
                                        pmodel.PrimaryCredContact_FirstName2 = SecondaryCredentialingContactInformation.FirstName;
                                        pmodel.PrimaryCredContact_MI2 = SecondaryCredentialingContactInformation.MiddleName;
                                        pmodel.PrimaryCredContact_LastName2 = SecondaryCredentialingContactInformation.LastName;

                                        pmodel.PrimaryCredContact_Street2 = SecondaryCredentialingContactInformation.Street;
                                        pmodel.PrimaryCredContact_Suite2 = SecondaryCredentialingContactInformation.Building;
                                        pmodel.PrimaryCredContact_City2 = SecondaryCredentialingContactInformation.City;
                                        pmodel.PrimaryCredContact_State2 = SecondaryCredentialingContactInformation.State;
                                        pmodel.PrimaryCredContact_ZipCode2 = SecondaryCredentialingContactInformation.ZipCode;
                                        pmodel.PrimaryCredContact_Phone2 = SecondaryCredentialingContactInformation.Telephone;
                                        pmodel.PrimaryCredContact_Fax2 = SecondaryCredentialingContactInformation.FaxNumber;
                                        pmodel.PrimaryCredContact_Email2 = SecondaryCredentialingContactInformation.EmailAddress;
                                        pmodel.PrimaryCredContact_MobileNumber2 = SecondaryCredentialingContactInformation.MobileNumber;
                                        pmodel.PrimaryCredContact2_Address1 = pmodel.PrimaryCredContact_Street2 + ", " + pmodel.PrimaryCredContact_Suite2;
                                    }
                                    #endregion
                                }
                                #region 3 Practice Location
                                if (secondaryPracticeLocationCount > 1)
                                {
                                    #region Address 3

                                    if (SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 2] != null)
                                    {
                                        pmodel.General_PracticeLocationAddress3 = SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 2].Street + " " + SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 2].City + ", " + SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 2].State + ", " + SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 2].ZipCode;

                                        if (SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 2].Telephone != null)
                                        {
                                            pmodel.General_PhoneFirstThreeDigit3 = SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 2].Telephone.Substring(0, SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 2].Telephone.Length - 7);
                                            pmodel.General_PhoneSecondThreeDigit3 = SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 2].Telephone.Substring(3, 3);
                                            pmodel.General_PhoneLastFourDigit3 = SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 2].Telephone.Substring(6);

                                            pmodel.General_Phone3 = pmodel.General_PhoneFirstThreeDigit3 + "-" + pmodel.General_PhoneSecondThreeDigit3 + "-" + pmodel.General_PhoneLastFourDigit3;
                                            pmodel.LocationAddress3_Line3 = "Phone : " + pmodel.General_Phone3;
                                        }

                                        pmodel.General_Email3 = SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 2].EmailAddress;

                                        if (SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 2].Fax != null)
                                        {
                                            pmodel.General_FaxFirstThreeDigit3 = SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 2].Fax.Substring(0, SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 2].Fax.Length - 7);
                                            pmodel.General_FaxSecondThreeDigit3 = SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 2].Fax.Substring(3, 3);
                                            pmodel.General_FaxLastFourDigit3 = SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 2].Fax.Substring(6);

                                            pmodel.General_Fax3 = pmodel.General_FaxFirstThreeDigit3 + "-" + pmodel.General_FaxSecondThreeDigit3 + "-" + pmodel.General_FaxLastFourDigit3;
                                            pmodel.LocationAddress3_Line3 = pmodel.LocationAddress3_Line3 + " " + "Fax : " + pmodel.General_Fax3;
                                        }

                                        pmodel.General_AccessGroupName3 = "Access Healthcare Physicians, LLC";
                                        pmodel.General_Access2GroupName3 = "Access 2 Healthcare Physicians, LLC";

                                        pmodel.General_AccessGroupTaxId3 = "451444883";
                                        pmodel.General_Access2GroupTaxId3 = "451024515";
                                        pmodel.General_FacilityName3 = SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 2].PracticeLocation_FacilityName;
                                        pmodel.General_PracticeOrCorporateName3 = SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 2].PracticeLocationCorporateName;
                                        pmodel.General_Suite3 = SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 2].Building;
                                        pmodel.General_Street3 = SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 2].Street;
                                        pmodel.General_City3 = SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 2].City;
                                        pmodel.General_State3 = SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 2].State;
                                        pmodel.General_ZipCode3 = SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 2].ZipCode;
                                        pmodel.General_Country3 = SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 2].Country;
                                        pmodel.General_County3 = SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 2].County;
                                        pmodel.LocationAddress3_Line1 = pmodel.General_Street3 + " " + pmodel.General_Suite3;
                                        pmodel.LocationAddress3_Line2 = pmodel.General_City3 + ", " + pmodel.General_State3 + ", " + pmodel.General_ZipCode3;
                                        pmodel.General_City3State3 = pmodel.General_City3 + ", " + pmodel.General_State3 + ", " + pmodel.General_ZipCode3;
                                        pmodel.General_FacilityPracticeName3 = pmodel.General_FacilityName3 + ", " + pmodel.General_PracticeOrCorporateName3;

                                    }

                                    #endregion

                                    #region Languages 3

                                    if (SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 2] != null)
                                    {

                                        if (SecondaryNonEnglishlanguageData != null)
                                        {
                                            var languages = SecondaryNonEnglishlanguageData.Where(l => l.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();
                                            if (languages.Count > 0)
                                            {
                                                foreach (var language in languages)
                                                {
                                                    if (language != null)
                                                        pmodel.Languages_Known3 += language.Language + ",";
                                                }
                                            }
                                        }
                                    }

                                    #endregion

                                    #region Open Practice Status 3

                                    pmodel.OpenPractice_AgeLimitations3 = SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 2].MinimumAge + " - " + SecondaryPracticeLocationInfo[secondaryPracticeLocationCount - 2].MaximumAge;

                                    #endregion

                                    #region Office Manager 3

                                    if (SecondaryBusinessOfficeManagerOrStaff != null)
                                    {
                                        if (SecondaryBusinessOfficeManagerOrStaff.MiddleName != null)
                                            pmodel.OfficeManager_Name3 = SecondaryBusinessOfficeManagerOrStaff.FirstName + " " + SecondaryBusinessOfficeManagerOrStaff.MiddleName + " " + SecondaryBusinessOfficeManagerOrStaff.LastName;
                                        else
                                            pmodel.OfficeManager_Name3 = SecondaryBusinessOfficeManagerOrStaff.FirstName + " " + SecondaryBusinessOfficeManagerOrStaff.LastName;

                                        pmodel.OfficeManager_FirstName3 = SecondaryBusinessOfficeManagerOrStaff.FirstName;
                                        pmodel.OfficeManager_MiddleName3 = SecondaryBusinessOfficeManagerOrStaff.MiddleName;
                                        pmodel.OfficeManager_LastName3 = SecondaryBusinessOfficeManagerOrStaff.LastName;
                                        pmodel.OfficeManager_Email3 = SecondaryBusinessOfficeManagerOrStaff.EmailAddress;
                                        pmodel.OfficeManager_PoBoxAddress3 = SecondaryBusinessOfficeManagerOrStaff.POBoxAddress;
                                        pmodel.OfficeManager_Building3 = SecondaryBusinessOfficeManagerOrStaff.Building;
                                        pmodel.OfficeManager_Street3 = SecondaryBusinessOfficeManagerOrStaff.Street;
                                        pmodel.OfficeManager_City3 = SecondaryBusinessOfficeManagerOrStaff.City;
                                        pmodel.OfficeManager_State3 = SecondaryBusinessOfficeManagerOrStaff.State;
                                        pmodel.OfficeManager_ZipCode3 = SecondaryBusinessOfficeManagerOrStaff.ZipCode;
                                        pmodel.OfficeManager_Country3 = SecondaryBusinessOfficeManagerOrStaff.Country;
                                        pmodel.OfficeManager_County3 = SecondaryBusinessOfficeManagerOrStaff.County;

                                        if (SecondaryBusinessOfficeManagerOrStaff.Telephone != null)
                                        {
                                            pmodel.OfficeManager_PhoneFirstThreeDigit3 = SecondaryBusinessOfficeManagerOrStaff.Telephone.Substring(0, SecondaryBusinessOfficeManagerOrStaff.Telephone.Length - 7);
                                            pmodel.OfficeManager_PhoneSecondThreeDigit3 = SecondaryBusinessOfficeManagerOrStaff.Telephone.Substring(3, 3);
                                            pmodel.OfficeManager_PhoneLastFourDigit3 = SecondaryBusinessOfficeManagerOrStaff.Telephone.Substring(6);

                                            pmodel.OfficeManager_Phone3 = pmodel.OfficeManager_PhoneFirstThreeDigit3 + "-" + pmodel.OfficeManager_PhoneSecondThreeDigit3 + "-" + pmodel.OfficeManager_PhoneLastFourDigit3;

                                        }

                                        if (SecondaryBusinessOfficeManagerOrStaff.Fax != null)
                                        {

                                            pmodel.OfficeManager_FaxFirstThreeDigit3 = SecondaryBusinessOfficeManagerOrStaff.Fax.Substring(0, SecondaryBusinessOfficeManagerOrStaff.Fax.Length - 7);
                                            pmodel.OfficeManager_FaxSecondThreeDigit3 = SecondaryBusinessOfficeManagerOrStaff.Fax.Substring(3, 3);
                                            pmodel.OfficeManager_FaxLastFourDigit3 = SecondaryBusinessOfficeManagerOrStaff.Fax.Substring(6);

                                            pmodel.OfficeManager_Fax3 = pmodel.OfficeManager_FaxFirstThreeDigit3 + "-" + pmodel.OfficeManager_FaxSecondThreeDigit3 + "-" + pmodel.OfficeManager_FaxLastFourDigit3;


                                        }
                                    }

                                    #endregion

                                    #region Billing Contact 3

                                    if (SecondaryBillingContactPerson != null)
                                    {
                                        if (SecondaryBillingContactPerson.MiddleName != null)
                                            pmodel.BillingContact_Name3 = SecondaryBillingContactPerson.FirstName + " " + SecondaryBillingContactPerson.MiddleName + " " + SecondaryBillingContactPerson.LastName;
                                        else
                                            pmodel.BillingContact_Name3 = SecondaryBillingContactPerson.FirstName + " " + SecondaryBillingContactPerson.LastName;

                                        pmodel.BillingContact_FirstName3 = SecondaryBillingContactPerson.FirstName;
                                        pmodel.BillingContact_MiddleName3 = SecondaryBillingContactPerson.MiddleName;
                                        pmodel.BillingContact_LastName3 = SecondaryBillingContactPerson.LastName;
                                        pmodel.BillingContact_Email3 = SecondaryBillingContactPerson.EmailAddress;
                                        //pmodel.BillingContact_Phone1 = SecondaryBillingContactPerson.MobileNumber;
                                        //pmodel.BillingContact_Fax1 = SecondaryBillingContactPerson.FaxNumber;
                                        pmodel.BillingContact_POBoxAddress3 = SecondaryBillingContactPerson.POBoxAddress;
                                        pmodel.BillingContact_Suite3 = SecondaryBillingContactPerson.Building;
                                        pmodel.BillingContact_Street3 = SecondaryBillingContactPerson.Street;
                                        pmodel.BillingContact_City3 = SecondaryBillingContactPerson.City;
                                        pmodel.BillingContact_State3 = SecondaryBillingContactPerson.State;
                                        pmodel.BillingContact_ZipCode3 = SecondaryBillingContactPerson.ZipCode;
                                        pmodel.BillingContact_Country3 = SecondaryBillingContactPerson.Country;
                                        pmodel.BillingContact_County3 = SecondaryBillingContactPerson.County;
                                        pmodel.BillingContact_City3State3 = pmodel.BillingContact_City3 + " ," + pmodel.BillingContact_State3 + ", " + pmodel.BillingContact_ZipCode3;
                                        if (SecondaryBillingContactPerson.Telephone != null)
                                        {
                                            pmodel.BillingContact_PhoneFirstThreeDigit3 = SecondaryBillingContactPerson.Telephone.Substring(0, SecondaryBillingContactPerson.Telephone.Length - 7);
                                            pmodel.BillingContact_PhoneSecondThreeDigit3 = SecondaryBillingContactPerson.Telephone.Substring(3, 3);
                                            pmodel.BillingContact_PhoneLastFourDigit3 = SecondaryBillingContactPerson.Telephone.Substring(6);

                                            pmodel.BillingContact_Phone3 = pmodel.BillingContact_PhoneFirstThreeDigit3 + "-" + pmodel.BillingContact_PhoneSecondThreeDigit3 + "-" + pmodel.BillingContact_PhoneLastFourDigit3;
                                        }

                                        if (SecondaryBillingContactPerson.Fax != null)
                                        {
                                            pmodel.BillingContact_FaxFirstThreeDigit3 = SecondaryBillingContactPerson.Fax.Substring(0, SecondaryBillingContactPerson.Fax.Length - 7);
                                            pmodel.BillingContact_FaxSecondThreeDigit3 = SecondaryBillingContactPerson.Fax.Substring(3, 3);
                                            pmodel.BillingContact_FaxLastFourDigit3 = SecondaryBillingContactPerson.Fax.Substring(6);

                                            pmodel.BillingContact_Fax3 = pmodel.BillingContact_FaxFirstThreeDigit3 + "-" + pmodel.BillingContact_FaxSecondThreeDigit3 + "-" + pmodel.BillingContact_FaxLastFourDigit3;
                                        }
                                    }

                                    #endregion

                                    #region Payment and Remittance 3

                                    if (SecondaryPaymentAndRemittance != null)
                                    {
                                        if (SecondaryPaymentAndRemittance.MiddleName != null)
                                            pmodel.PaymentRemittance_Name3 = SecondaryPaymentAndRemittance.FirstName + " " + SecondaryPaymentAndRemittance.MiddleName + " " + SecondaryPaymentAndRemittance.LastName;
                                        else
                                            pmodel.PaymentRemittance_Name3 = SecondaryPaymentAndRemittance.FirstName + " " + SecondaryPaymentAndRemittance.LastName;

                                        pmodel.PaymentRemittance_FirstName3 = SecondaryPaymentAndRemittance.FirstName;
                                        pmodel.PaymentRemittance_MiddleName3 = SecondaryPaymentAndRemittance.MiddleName;
                                        pmodel.PaymentRemittance_LastName3 = SecondaryPaymentAndRemittance.LastName;
                                        pmodel.PaymentRemittance_Email3 = SecondaryPaymentAndRemittance.EmailAddress;
                                        pmodel.PaymentRemittance_POBoxAddress3 = SecondaryPaymentAndRemittance.POBoxAddress;
                                        pmodel.PaymentRemittance_Suite3 = SecondaryPaymentAndRemittance.Building;
                                        pmodel.PaymentRemittance_Street3 = SecondaryPaymentAndRemittance.Street;
                                        pmodel.PaymentRemittance_City3 = SecondaryPaymentAndRemittance.City;
                                        pmodel.PaymentRemittance_State3 = SecondaryPaymentAndRemittance.State;
                                        pmodel.PaymentRemittance_ZipCode3 = SecondaryPaymentAndRemittance.ZipCode;
                                        pmodel.PaymentRemittance_Country3 = SecondaryPaymentAndRemittance.Country;
                                        pmodel.PaymentRemittance_County3 = SecondaryPaymentAndRemittance.County;

                                        if (SecondaryPaymentAndRemittance.Telephone != null)
                                        {
                                            pmodel.PaymentRemittance_PhoneFirstThreeDigit3 = SecondaryPaymentAndRemittance.Telephone.Substring(0, SecondaryPaymentAndRemittance.Telephone.Length - 7);
                                            pmodel.PaymentRemittance_PhoneSecondThreeDigit3 = SecondaryPaymentAndRemittance.Telephone.Substring(3, 3);
                                            pmodel.PaymentRemittance_PhoneLastFourDigit3 = SecondaryPaymentAndRemittance.Telephone.Substring(6);

                                            pmodel.PaymentRemittance_Phone3 = pmodel.PaymentRemittance_PhoneFirstThreeDigit3 + "-" + pmodel.PaymentRemittance_PhoneSecondThreeDigit3 + "-" + pmodel.PaymentRemittance_PhoneLastFourDigit3;
                                        }

                                        if (SecondaryPaymentAndRemittance.Fax != null)
                                        {
                                            //pmodel.General_Fax1 = practiceLocation[practiceLocationCount - 2].Facility.Fax.Substring(3, practiceLocation[practiceLocationCount - 2].Facility.Fax.Length - 3);

                                            pmodel.PaymentRemittance_FaxFirstThreeDigit3 = SecondaryPaymentAndRemittance.Fax.Substring(0, SecondaryPaymentAndRemittance.Fax.Length - 7);
                                            pmodel.PaymentRemittance_FaxSecondThreeDigit3 = SecondaryPaymentAndRemittance.Fax.Substring(3, 3);
                                            pmodel.PaymentRemittance_FaxLastFourDigit3 = SecondaryPaymentAndRemittance.Fax.Substring(6);

                                            pmodel.PaymentRemittance_Fax3 = pmodel.PaymentRemittance_FaxFirstThreeDigit3 + "-" + pmodel.PaymentRemittance_FaxSecondThreeDigit3 + "-" + pmodel.PaymentRemittance_FaxLastFourDigit3;
                                        }

                                        pmodel.PaymentRemittance_ElectronicBillCapability3 = SecondaryPaymentAndRemittance.ElectronicBillingCapability;
                                        pmodel.PaymentRemittance_BillingDepartment3 = SecondaryPaymentAndRemittance.BillingDepartment;
                                        pmodel.PaymentRemittance_ChekPayableTo3 = SecondaryPaymentAndRemittance.CheckPayableTo;
                                        pmodel.PaymentRemittance_Office3 = SecondaryPaymentAndRemittance.Office;
                                    }

                                    #endregion

                                    #region Office Hours 3

                                    if (SecondaryOfficeHours != null)
                                    {
                                        if (SecondaryOfficeHours.Count > 0)
                                        {
                                            if (SecondaryOfficeHours.ElementAt(0).StartTime != null)
                                                pmodel.OfficeHour_StartMonday3 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(0).StartTime);
                                            if (SecondaryOfficeHours.ElementAt(0).EndTime != null)
                                                pmodel.OfficeHour_EndMonday3 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(0).EndTime);

                                            pmodel.OfficeHour_Monday3 = pmodel.OfficeHour_StartMonday3 + " - " + pmodel.OfficeHour_EndMonday3;
                                            if (SecondaryOfficeHours.Count > 1)
                                            {
                                                if (SecondaryOfficeHours.ElementAt(1).StartTime != null)
                                                    pmodel.OfficeHour_StartTuesday3 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(1).StartTime);
                                                if (SecondaryOfficeHours.ElementAt(1).EndTime != null)
                                                    pmodel.OfficeHour_EndTuesday3 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(1).EndTime);
                                            }
                                            pmodel.OfficeHour_Tuesday3 = pmodel.OfficeHour_StartTuesday3 + " - " + pmodel.OfficeHour_EndTuesday3;
                                            if (SecondaryOfficeHours.Count > 2)
                                            {
                                                if (SecondaryOfficeHours.ElementAt(2).StartTime != null)
                                                    pmodel.OfficeHour_StartWednesday3 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(2).StartTime);
                                                if (SecondaryOfficeHours.ElementAt(2).EndTime != null)

                                                    pmodel.OfficeHour_EndWednesday3 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(2).EndTime);

                                                pmodel.OfficeHour_Wednesday3 = pmodel.OfficeHour_StartWednesday3 + " - " + pmodel.OfficeHour_EndWednesday3;
                                            }
                                            if (SecondaryOfficeHours.Count > 3)
                                            {
                                                if (SecondaryOfficeHours.ElementAt(3).StartTime != null)
                                                    pmodel.OfficeHour_StartThursday3 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(3).StartTime);
                                                if (SecondaryOfficeHours.ElementAt(3).EndTime != null)
                                                    pmodel.OfficeHour_EndThursday3 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(3).EndTime);

                                                pmodel.OfficeHour_Thursday3 = pmodel.OfficeHour_StartThursday3 + " - " + pmodel.OfficeHour_EndThursday3;
                                            }
                                            if (SecondaryOfficeHours.Count > 4)
                                            {


                                                if (SecondaryOfficeHours.ElementAt(4).StartTime != null)
                                                    pmodel.OfficeHour_StartFriday3 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(4).StartTime);
                                                if (SecondaryOfficeHours.ElementAt(4).EndTime != null)
                                                    pmodel.OfficeHour_EndFriday3 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(4).EndTime);

                                                pmodel.OfficeHour_Friday3 = pmodel.OfficeHour_StartFriday3 + " - " + pmodel.OfficeHour_EndFriday3;
                                            }
                                            if (SecondaryOfficeHours.Count > 5)
                                            {
                                                if (SecondaryOfficeHours.ElementAt(5).StartTime != null)
                                                    pmodel.OfficeHour_StartSaturday3 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(5).StartTime);
                                                if (SecondaryOfficeHours.ElementAt(5).EndTime != null)

                                                    pmodel.OfficeHour_EndSaturday3 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(5).EndTime);

                                                pmodel.OfficeHour_Saturday3 = pmodel.OfficeHour_StartSaturday3 + " - " + pmodel.OfficeHour_EndSaturday3;
                                            }
                                            if (SecondaryOfficeHours.Count > 6)
                                            {
                                                if (SecondaryOfficeHours.ElementAt(6).StartTime != null)
                                                    pmodel.OfficeHour_StartSunday3 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(6).StartTime);
                                                if (SecondaryOfficeHours.ElementAt(6).EndTime != null)
                                                    pmodel.OfficeHour_EndSunday3 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(6).EndTime);

                                                pmodel.OfficeHour_Sunday3 = pmodel.OfficeHour_StartSunday3 + " - " + pmodel.OfficeHour_EndSunday3;
                                            }
                                        }

                                    }

                                    #endregion

                                    #region Supervising Provider 3

                                    var supervisingProvider = SecondaryPracticeProviders.
                                    Where(s => s.Practice == AHC.CD.Entities.MasterData.Enums.PracticeType.Supervisor.ToString() &&
                                        s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();

                                    var supervisorCount = supervisingProvider.Count;

                                    if (supervisorCount > 0)
                                    {
                                        pmodel.CoveringColleague_FirstName3 = supervisingProvider.ElementAt(supervisorCount - 1).FirstName;
                                        pmodel.CoveringColleague_MiddleName3 = supervisingProvider.ElementAt(supervisorCount - 1).MiddleName;
                                        pmodel.CoveringColleague_LastName3 = supervisingProvider.ElementAt(supervisorCount - 1).LastName;

                                        if (pmodel.CoveringColleague_MiddleName3 != null)
                                            pmodel.CoveringColleague_FullName3 = pmodel.CoveringColleague_FirstName3 + " " + pmodel.CoveringColleague_MiddleName3 + " " + pmodel.CoveringColleague_LastName3;
                                        else
                                            pmodel.CoveringColleague_FullName3 = pmodel.CoveringColleague_FirstName3 + " " + pmodel.CoveringColleague_LastName3;


                                        if (supervisingProvider.ElementAt(supervisorCount - 1).Telephone != null)
                                        {
                                            pmodel.CoveringColleague_PhoneFirstThreeDigit3 = supervisingProvider.ElementAt(supervisorCount - 1).Telephone.Substring(0, supervisingProvider.ElementAt(supervisorCount - 1).Telephone.Length - 7);
                                            pmodel.CoveringColleague_PhoneSecondThreeDigit3 = supervisingProvider.ElementAt(supervisorCount - 1).Telephone.Substring(3, 3);
                                            pmodel.CoveringColleague_PhoneLastFourDigit3 = supervisingProvider.ElementAt(supervisorCount - 1).Telephone.Substring(6);

                                            pmodel.CoveringColleague_PhoneNumber3 = pmodel.CoveringColleague_PhoneFirstThreeDigit3 + "-" + pmodel.CoveringColleague_PhoneSecondThreeDigit3 + "-" + pmodel.CoveringColleague_PhoneLastFourDigit3;

                                        }

                                        var specialities = SecondaryPracticeProvidersSpeciality.Where(s => s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString()).ToList();

                                        if (specialities.Count > 0)
                                        {
                                            if (specialities.ElementAt(specialities.Count - 1) != null)
                                                pmodel.CoveringColleague_Specialty3 = specialities.ElementAt(specialities.Count - 1).Name;
                                        }
                                    }

                                    #endregion

                                    #region Covering Colleagues/Partners 3

                                    var patners = SecondaryPracticeProviders.
                                    Where(s => s.Practice == AHC.CD.Entities.MasterData.Enums.PracticeType.CoveringColleague.ToString() &&
                                        s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();

                                    var patnersCount = patners.Count;

                                    if (patnersCount > 0)
                                    {
                                        pmodel.Patners_FirstName3 = patners.ElementAt(patnersCount - 1).FirstName;
                                        pmodel.Patners_MiddleName3 = patners.ElementAt(patnersCount - 1).MiddleName;
                                        pmodel.Patners_LastName3 = patners.ElementAt(patnersCount - 1).LastName;

                                        if (pmodel.Patners_MiddleName3 != null)
                                            pmodel.Patners_FullName3 = pmodel.Patners_FirstName3 + " " + pmodel.Patners_MiddleName3 + " " + pmodel.Patners_LastName3;
                                        else
                                            pmodel.Patners_FullName3 = pmodel.Patners_FirstName3 + " " + pmodel.Patners_LastName3;

                                    }

                                    #endregion
                                }
                                #endregion
                            }
                        }

                    }
                    // PRIMARY PRACTICE LOCATION == NULL OR  IS NOT PRESENT 
                    #region Secondary Practice Location as Primary Location
                    else
                    {
                        if (secondaryPracticeLocationDetails.Count > 0)
                        {
                            var secpracticeLocationCount = secondaryPracticeLocationDetails.Count;
                            var secondaryPracticeLocation = secondaryPracticeLocationDetails;

                            #region SecondaryPracticeLocationInfo
                            var SecondaryPracticeLocationInfo = (from data in secondaryPracticeLocationDetails
                                                                 group data by new
                                                                 {
                                                                     data.CurrentlyPracticingAtThisAddress,
                                                                     data.Building,
                                                                     data.City,
                                                                     data.Country,
                                                                     data.County,
                                                                     data.Telephone,
                                                                     data.State,
                                                                     data.Fax,
                                                                     data.Street,
                                                                     data.ZipCode,
                                                                     data.EmailAddress,
                                                                     data.PracticeType_Title,
                                                                     data.MaximumAge,
                                                                     data.MinimumAge,
                                                                     data.PracticeLocation_CorporateName,
                                                                     data.PracticeLocation_OtherPracticeName,
                                                                     data.PracticeLocation_FacilityName,
                                                                     data.PrimaryTaxId,
                                                                     data.StartDate
                                                                 } into addressdata
                                                                 select new
                                                                 {
                                                                     CurrentlyPracticingAtThisAddress = addressdata.Key.CurrentlyPracticingAtThisAddress,
                                                                     PracticeLocationCorporateName = addressdata.Key.PracticeLocation_CorporateName,
                                                                     PracticeLocation_OtherPracticeName = addressdata.Key.PracticeLocation_OtherPracticeName,
                                                                     PracticeLocation_FacilityName = addressdata.Key.PracticeLocation_FacilityName,
                                                                     PrimaryTaxId = addressdata.Key.PrimaryTaxId,
                                                                     StartDate = addressdata.Key.StartDate,
                                                                     PracticeType_Title = addressdata.Key.PracticeType_Title,
                                                                     State = addressdata.Key.State,
                                                                     Street = addressdata.Key.Street,
                                                                     ZipCode = addressdata.Key.ZipCode,
                                                                     Building = addressdata.Key.Building,
                                                                     City = addressdata.Key.City,
                                                                     Country = addressdata.Key.Country,
                                                                     County = addressdata.Key.County,
                                                                     Telephone = addressdata.Key.Telephone,
                                                                     Fax = addressdata.Key.Fax,
                                                                     EmailAddress = addressdata.Key.EmailAddress,
                                                                     MaximumAge = addressdata.Key.MaximumAge,
                                                                     MinimumAge = addressdata.Key.MinimumAge
                                                                 }).ToList();
                            secpracticeLocationCount = SecondaryPracticeLocationInfo.Count;
                            #endregion

                            #region  SecondaryNonEnglishlanguageData

                            var SecondaryNonEnglishlanguageData = (from data in secondaryPracticeLocationDetails
                                                                   group data by new { data.NonEnglishLanguage_Language, data.NonEnglishLanguage_Status, data.NonEnglishLanguage_StatusType } into languagesdata
                                                                   select new
                                                                   {
                                                                       Status = languagesdata.Key.NonEnglishLanguage_Status,
                                                                       StatusType = languagesdata.Key.NonEnglishLanguage_StatusType,
                                                                       Language = languagesdata.Key.NonEnglishLanguage_Language
                                                                   }).ToList();
                            #endregion

                            #region  SecondaryBusinessOfficeManagerOrStaff

                            var SecondaryBusinessOfficeManagerOrStaff = (from data in secondaryPracticeLocationDetails
                                                                         group data by new
                                                                         {
                                                                             data.BusinessOfficeManagerOrStaff_Building,
                                                                             data.BusinessOfficeManagerOrStaff_City,
                                                                             data.BusinessOfficeManagerOrStaff_Country,
                                                                             data.BusinessOfficeManagerOrStaff_CountryCodeFax,
                                                                             data.BusinessOfficeManagerOrStaff_CountryCodeTelephone,
                                                                             data.BusinessOfficeManagerOrStaff_County,
                                                                             data.BusinessOfficeManagerOrStaff_EmailAddress,
                                                                             data.BusinessOfficeManagerOrStaff_Fax,
                                                                             data.BusinessOfficeManagerOrStaff_FaxNumber,
                                                                             data.BusinessOfficeManagerOrStaff_FirstName,
                                                                             data.BusinessOfficeManagerOrStaff_LastName,
                                                                             data.BusinessOfficeManagerOrStaff_MiddleName,
                                                                             data.BusinessOfficeManagerOrStaff_MobileNumber,
                                                                             data.BusinessOfficeManagerOrStaff_POBoxAddress,
                                                                             data.BusinessOfficeManagerOrStaff_State,
                                                                             data.BusinessOfficeManagerOrStaff_Street,
                                                                             data.BusinessOfficeManagerOrStaff_Telephone,
                                                                             data.BusinessOfficeManagerOrStaff_ZipCode


                                                                         } into BOMdata
                                                                         select new
                                                                         {
                                                                             Building = BOMdata.Key.BusinessOfficeManagerOrStaff_Building,
                                                                             City = BOMdata.Key.BusinessOfficeManagerOrStaff_City,
                                                                             Country = BOMdata.Key.BusinessOfficeManagerOrStaff_Country,
                                                                             CountryCodeFax = BOMdata.Key.BusinessOfficeManagerOrStaff_CountryCodeFax,
                                                                             CountryCodeTelephone = BOMdata.Key.BusinessOfficeManagerOrStaff_CountryCodeTelephone,
                                                                             County = BOMdata.Key.BusinessOfficeManagerOrStaff_County,
                                                                             EmailAddress = BOMdata.Key.BusinessOfficeManagerOrStaff_EmailAddress,
                                                                             Fax = BOMdata.Key.BusinessOfficeManagerOrStaff_Fax,
                                                                             FaxNumber = BOMdata.Key.BusinessOfficeManagerOrStaff_FaxNumber,
                                                                             FirstName = BOMdata.Key.BusinessOfficeManagerOrStaff_FirstName,
                                                                             LastName = BOMdata.Key.BusinessOfficeManagerOrStaff_LastName,
                                                                             MiddleName = BOMdata.Key.BusinessOfficeManagerOrStaff_MiddleName,
                                                                             MobileNumber = BOMdata.Key.BusinessOfficeManagerOrStaff_MobileNumber,
                                                                             POBoxAddress = BOMdata.Key.BusinessOfficeManagerOrStaff_POBoxAddress,
                                                                             State = BOMdata.Key.BusinessOfficeManagerOrStaff_State,
                                                                             Street = BOMdata.Key.BusinessOfficeManagerOrStaff_Street,
                                                                             Telephone = BOMdata.Key.BusinessOfficeManagerOrStaff_Telephone,
                                                                             ZipCode = BOMdata.Key.BusinessOfficeManagerOrStaff_ZipCode
                                                                         }).ToList().LastOrDefault();
                            #endregion

                            #region  SecondaryBillingContactPerson
                            var SecondaryBillingContactPerson = (from data in secondaryPracticeLocationDetails
                                                                 group data by new
                                                                 {
                                                                     data.BillingContactPerson_POBoxAddress,
                                                                     data.BillingContactPerson_Building,
                                                                     data.BillingContactPerson_City,
                                                                     data.BillingContactPerson_Country,
                                                                     data.BillingContactPerson_CountryCodeFax,
                                                                     data.BillingContactPerson_CountryCodeTelephone,
                                                                     data.BillingContactPerson_County,
                                                                     data.BillingContactPerson_EmailAddress,
                                                                     data.BillingContactPerson_Fax,
                                                                     data.BillingContactPerson_FaxNumber,
                                                                     data.BillingContactPerson_FirstName,
                                                                     data.BillingContactPerson_LastName,
                                                                     data.BillingContactPerson_MiddleName,
                                                                     data.BillingContactPerson_MobileNumber,
                                                                     data.BillingContactPerson_State,
                                                                     data.BillingContactPerson_Street,
                                                                     data.BillingContactPerson_Telephone,
                                                                     data.BillingContactPerson_ZipCode


                                                                 } into BOMdata
                                                                 select new
                                                                 {
                                                                     Building = BOMdata.Key.BillingContactPerson_Building,
                                                                     City = BOMdata.Key.BillingContactPerson_City,
                                                                     Country = BOMdata.Key.BillingContactPerson_Country,
                                                                     CountryCodeFax = BOMdata.Key.BillingContactPerson_CountryCodeFax,
                                                                     CountryCodeTelephone = BOMdata.Key.BillingContactPerson_CountryCodeTelephone,
                                                                     County = BOMdata.Key.BillingContactPerson_County,
                                                                     EmailAddress = BOMdata.Key.BillingContactPerson_EmailAddress,
                                                                     Fax = BOMdata.Key.BillingContactPerson_Fax,
                                                                     FaxNumber = BOMdata.Key.BillingContactPerson_FaxNumber,
                                                                     FirstName = BOMdata.Key.BillingContactPerson_FirstName,
                                                                     LastName = BOMdata.Key.BillingContactPerson_LastName,
                                                                     MiddleName = BOMdata.Key.BillingContactPerson_MiddleName,
                                                                     MobileNumber = BOMdata.Key.BillingContactPerson_MobileNumber,
                                                                     POBoxAddress = BOMdata.Key.BillingContactPerson_POBoxAddress,
                                                                     State = BOMdata.Key.BillingContactPerson_State,
                                                                     Street = BOMdata.Key.BillingContactPerson_Street,
                                                                     Telephone = BOMdata.Key.BillingContactPerson_Telephone,
                                                                     ZipCode = BOMdata.Key.BillingContactPerson_ZipCode
                                                                 }).ToList().LastOrDefault();
                            #endregion

                            #region  SecondaryPaymentAndRemittance
                            var SecondaryPaymentAndRemittance = (from data in secondaryPracticeLocationDetails
                                                                 group data by new
                                                                 {
                                                                     data.PracticePaymentAndRemittance_Building,
                                                                     data.PracticePaymentAndRemittance_City,
                                                                     data.PracticePaymentAndRemittance_Country,
                                                                     data.PracticePaymentAndRemittance_CountryCodeFax,
                                                                     data.PracticePaymentAndRemittance_CountryCodeTelephone,
                                                                     data.PracticePaymentAndRemittance_County,
                                                                     data.PracticePaymentAndRemittance_EmailAddress,
                                                                     data.PracticePaymentAndRemittance_Fax,
                                                                     data.PracticePaymentAndRemittance_FaxNumber,
                                                                     data.PracticePaymentAndRemittance_FirstName,
                                                                     data.PracticePaymentAndRemittance_LastName,
                                                                     data.PracticePaymentAndRemittance_MiddleName,
                                                                     data.PracticePaymentAndRemittance_MobileNumber,
                                                                     data.PracticePaymentAndRemittance_POBoxAddress,
                                                                     data.PracticePaymentAndRemittance_State,
                                                                     data.PracticePaymentAndRemittance_Street,
                                                                     data.PracticePaymentAndRemittance_Telephone,
                                                                     data.PracticePaymentAndRemittance_ZipCode,
                                                                     data.ElectronicBillingCapability,
                                                                     data.BillingDepartment,
                                                                     data.CheckPayableTo,
                                                                     data.Office


                                                                 } into PPRdata
                                                                 select new
                                                                 {
                                                                     Building = PPRdata.Key.PracticePaymentAndRemittance_Building,
                                                                     City = PPRdata.Key.PracticePaymentAndRemittance_City,
                                                                     Country = PPRdata.Key.PracticePaymentAndRemittance_Country,
                                                                     CountryCodeFax = PPRdata.Key.PracticePaymentAndRemittance_CountryCodeFax,
                                                                     CountryCodeTelephone = PPRdata.Key.PracticePaymentAndRemittance_CountryCodeTelephone,
                                                                     County = PPRdata.Key.PracticePaymentAndRemittance_County,
                                                                     EmailAddress = PPRdata.Key.PracticePaymentAndRemittance_EmailAddress,
                                                                     Fax = PPRdata.Key.PracticePaymentAndRemittance_Fax,
                                                                     FaxNumber = PPRdata.Key.PracticePaymentAndRemittance_FaxNumber,
                                                                     FirstName = PPRdata.Key.PracticePaymentAndRemittance_FirstName,
                                                                     LastName = PPRdata.Key.PracticePaymentAndRemittance_LastName,
                                                                     MiddleName = PPRdata.Key.PracticePaymentAndRemittance_MiddleName,
                                                                     MobileNumber = PPRdata.Key.PracticePaymentAndRemittance_MobileNumber,
                                                                     POBoxAddress = PPRdata.Key.PracticePaymentAndRemittance_POBoxAddress,
                                                                     State = PPRdata.Key.PracticePaymentAndRemittance_State,
                                                                     Street = PPRdata.Key.PracticePaymentAndRemittance_Street,
                                                                     Telephone = PPRdata.Key.PracticePaymentAndRemittance_Telephone,
                                                                     ZipCode = PPRdata.Key.PracticePaymentAndRemittance_ZipCode,

                                                                     ElectronicBillingCapability = PPRdata.Key.ElectronicBillingCapability,
                                                                     BillingDepartment = PPRdata.Key.BillingDepartment,
                                                                     CheckPayableTo = PPRdata.Key.CheckPayableTo,
                                                                     Office = PPRdata.Key.Office


                                                                 }).ToList().LastOrDefault();
                            #endregion

                            #region SecondaryOfficeHours

                            var SecondaryOfficeHours = (from data in secondaryPracticeLocationDetails
                                                        group data by new { data.PracticeDailyHour_StartTime, data.PracticeDailyHour_EndTime } into officehoursdata
                                                        select new
                                                        {
                                                            StartTime = officehoursdata.Key.PracticeDailyHour_StartTime,
                                                            EndTime = officehoursdata.Key.PracticeDailyHour_EndTime

                                                        }).ToList();
                            #endregion

                            #region SecondaryPracticeProviders
                            var SecondaryPracticeProviders = (from data in secondaryPracticeLocationDetails
                                                              group data by new
                                                              {
                                                                  data.PracticeProvider_FirstName,
                                                                  data.PracticeProvider_LastName,
                                                                  data.PracticeProvider_MiddleName,
                                                                  data.PracticeProvider_Practice,
                                                                  data.PracticeProvider_PracticeType,
                                                                  data.PracticeProvider_Status,
                                                                  data.PracticeProvider_StatusType,
                                                                  data.PracticeProvider_Telephone
                                                              } into practiceprovidersdata
                                                              select new
                                                              {
                                                                  FirstName = practiceprovidersdata.Key.PracticeProvider_FirstName,
                                                                  LastName = practiceprovidersdata.Key.PracticeProvider_LastName,
                                                                  MiddleName = practiceprovidersdata.Key.PracticeProvider_MiddleName,
                                                                  Practice = practiceprovidersdata.Key.PracticeProvider_Practice,
                                                                  PracticeType = practiceprovidersdata.Key.PracticeProvider_PracticeType,
                                                                  Status = practiceprovidersdata.Key.PracticeProvider_Status,
                                                                  StatusType = practiceprovidersdata.Key.PracticeProvider_StatusType,
                                                                  Telephone = practiceprovidersdata.Key.PracticeProvider_Telephone
                                                              }).ToList();
                            #endregion

                            #region SecondaryPracticeProvidersSpeciality
                            var SecondaryPracticeProvidersSpeciality = (from data in secondaryPracticeLocationDetails
                                                                        group data by new
                                                                        {
                                                                            data.PracticeProviderSpecialty_Name,
                                                                            data.PracticeProviderSpecialty_Status,
                                                                            data.PracticeProviderSpecialty_StatusType
                                                                        } into practiceprovidersdata
                                                                        select new
                                                                        {

                                                                            Name = practiceprovidersdata.Key.PracticeProviderSpecialty_Name,
                                                                            Status = practiceprovidersdata.Key.PracticeProviderSpecialty_Status,
                                                                            StatusType = practiceprovidersdata.Key.PracticeProviderSpecialty_StatusType
                                                                        }).ToList();

                            #endregion

                            #region SecondaryCredentialingContactInformation

                            var SecondaryCredentialingContactInformation = (from data in secondaryPracticeLocationDetails
                                                                            group data by new
                                                                            {
                                                                                data.CredentialingContact_FirstName,
                                                                                data.CredentialingContact_MiddleName,
                                                                                data.CredentialingContact_LastName,
                                                                                data.CredentialingContact_Building,
                                                                                data.CredentialingContact_City,
                                                                                data.CredentialingContact_Country,
                                                                                data.CredentialingContact_CountryCodeFax,
                                                                                data.CredentialingContact_CountryCodeTelephone,
                                                                                data.CredentialingContact_County,
                                                                                data.CredentialingContact_EmailAddress,
                                                                                data.CredentialingContact_Fax,
                                                                                data.CredentialingContact_FaxNumber,
                                                                                data.CredentialingContact_MobileNumber,
                                                                                data.CredentialingContact_POBoxAddress,
                                                                                data.CredentialingContact_State,
                                                                                data.CredentialingContact_Street,
                                                                                data.CredentialingContact_Telephone,
                                                                                data.CredentialingContact_ZipCode
                                                                            } into CCInfo
                                                                            select new
                                                                            {
                                                                                Building = CCInfo.Key.CredentialingContact_Building,
                                                                                City = CCInfo.Key.CredentialingContact_City,
                                                                                Country = CCInfo.Key.CredentialingContact_Country,
                                                                                CountryCodeFax = CCInfo.Key.CredentialingContact_CountryCodeFax,
                                                                                CountryCodeTelephone = CCInfo.Key.CredentialingContact_CountryCodeTelephone,
                                                                                County = CCInfo.Key.CredentialingContact_County,
                                                                                EmailAddress = CCInfo.Key.CredentialingContact_EmailAddress,
                                                                                Fax = CCInfo.Key.CredentialingContact_Fax,
                                                                                FaxNumber = CCInfo.Key.CredentialingContact_FaxNumber,
                                                                                FirstName = CCInfo.Key.CredentialingContact_FirstName,
                                                                                LastName = CCInfo.Key.CredentialingContact_LastName,
                                                                                MiddleName = CCInfo.Key.CredentialingContact_MiddleName,
                                                                                MobileNumber = CCInfo.Key.CredentialingContact_MobileNumber,
                                                                                POBoxAddress = CCInfo.Key.CredentialingContact_POBoxAddress,
                                                                                State = CCInfo.Key.CredentialingContact_State,
                                                                                Street = CCInfo.Key.CredentialingContact_Street,
                                                                                Telephone = CCInfo.Key.CredentialingContact_Telephone,
                                                                                ZipCode = CCInfo.Key.CredentialingContact_ZipCode
                                                                            }).ToList().LastOrDefault();
                            #endregion

                            if (secpracticeLocationCount > 0)
                            {

                                #region Address 1

                                if (SecondaryPracticeLocationInfo[secpracticeLocationCount - 1] != null)
                                {
                                    pmodel.General_PracticeLocationAddress1 = SecondaryPracticeLocationInfo[secpracticeLocationCount - 1].Street + " " + SecondaryPracticeLocationInfo[secpracticeLocationCount - 1].City + ", " + SecondaryPracticeLocationInfo[secpracticeLocationCount - 1].State + ", " + SecondaryPracticeLocationInfo[secpracticeLocationCount - 1].ZipCode;

                                    if (SecondaryPracticeLocationInfo[secpracticeLocationCount - 1].Telephone != null)
                                    {
                                        pmodel.General_PhoneFirstThreeDigit1 = SecondaryPracticeLocationInfo[secpracticeLocationCount - 1].Telephone.Substring(0, SecondaryPracticeLocationInfo[secpracticeLocationCount - 1].Telephone.Length - 7);
                                        pmodel.General_PhoneSecondThreeDigit1 = SecondaryPracticeLocationInfo[secpracticeLocationCount - 1].Telephone.Substring(3, 3);
                                        pmodel.General_PhoneLastFourDigit1 = SecondaryPracticeLocationInfo[secpracticeLocationCount - 1].Telephone.Substring(6);

                                        pmodel.General_Phone1 = pmodel.General_PhoneFirstThreeDigit1 + "-" + pmodel.General_PhoneSecondThreeDigit1 + "-" + pmodel.General_PhoneLastFourDigit1;
                                        pmodel.LocationAddress_Line3 = "Phone : " + pmodel.General_Phone1;
                                    }


                                    pmodel.General_Email1 = SecondaryPracticeLocationInfo[secpracticeLocationCount - 1].EmailAddress;

                                    if (SecondaryPracticeLocationInfo[secpracticeLocationCount - 1].Fax != null)
                                    {


                                        pmodel.General_FaxFirstThreeDigit1 = SecondaryPracticeLocationInfo[secpracticeLocationCount - 1].Fax.Substring(0, SecondaryPracticeLocationInfo[secpracticeLocationCount - 1].Fax.Length - 7);
                                        pmodel.General_FaxSecondThreeDigit1 = SecondaryPracticeLocationInfo[secpracticeLocationCount - 1].Fax.Substring(3, 3);
                                        pmodel.General_FaxLastFourDigit1 = SecondaryPracticeLocationInfo[secpracticeLocationCount - 1].Fax.Substring(6);

                                        pmodel.General_Fax1 = pmodel.General_FaxFirstThreeDigit1 + "-" + pmodel.General_FaxSecondThreeDigit1 + "-" + pmodel.General_FaxLastFourDigit1;
                                        pmodel.LocationAddress_Line3 = pmodel.LocationAddress_Line3 + " " + "Fax : " + pmodel.General_Fax1;
                                    }

                                    pmodel.General_AccessGroupName1 = "Access Healthcare Physicians, LLC";
                                    pmodel.General_Access2GroupName1 = "Access 2 Healthcare Physicians, LLC";

                                    pmodel.General_AccessGroupTaxId1 = "451444883";
                                    pmodel.General_Access2GroupTaxId1 = "451024515";
                                    pmodel.General_PracticeOrCorporateName1 = SecondaryPracticeLocationInfo[secpracticeLocationCount - 1].PracticeLocationCorporateName;
                                    pmodel.General_FacilityName1 = SecondaryPracticeLocationInfo[secpracticeLocationCount - 1].PracticeLocation_FacilityName;
                                    pmodel.General_Suite1 = SecondaryPracticeLocationInfo[secpracticeLocationCount - 1].Building;
                                    pmodel.General_Street1 = SecondaryPracticeLocationInfo[secpracticeLocationCount - 1].Street;
                                    pmodel.General_City1 = SecondaryPracticeLocationInfo[secpracticeLocationCount - 1].City;
                                    pmodel.General_State1 = SecondaryPracticeLocationInfo[secpracticeLocationCount - 1].State;
                                    pmodel.General_ZipCode1 = SecondaryPracticeLocationInfo[secpracticeLocationCount - 1].ZipCode;
                                    pmodel.General_Country1 = SecondaryPracticeLocationInfo[secpracticeLocationCount - 1].Country;
                                    pmodel.General_County1 = SecondaryPracticeLocationInfo[secpracticeLocationCount - 1].County;
                                    pmodel.LocationAddress_Line1 = pmodel.General_Street1 + "  " + pmodel.General_Suite1;
                                    pmodel.LocationAddress_Line2 = pmodel.General_City1 + ", " + pmodel.General_State1 + ", " + pmodel.General_ZipCode1;

                                    pmodel.General_City1State1 = pmodel.General_City1 + ", " + pmodel.General_State1 + ", " + pmodel.General_ZipCode1;
                                    pmodel.General_FacilityPracticeName1 = pmodel.General_FacilityName1 + ", " + pmodel.General_PracticeOrCorporateName1;

                                }

                                #endregion

                                #region Languages 1

                                if (SecondaryPracticeLocationInfo[secpracticeLocationCount - 1] != null)
                                {

                                    if (SecondaryNonEnglishlanguageData != null)
                                    {
                                        var languages = SecondaryNonEnglishlanguageData.Where(l => l.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();
                                        if (languages.Count > 0)
                                        {
                                            foreach (var language in languages)
                                            {
                                                if (language != null)
                                                    pmodel.Languages_Known1 += language.Language + ",";
                                            }
                                        }
                                    }
                                }

                                #endregion

                                #region Primary Credentialing Contact Information 1

                                if (SecondaryCredentialingContactInformation != null)
                                {

                                    if (SecondaryCredentialingContactInformation.MiddleName != null)
                                    {
                                        pmodel.PrimaryCredContact_FullName = SecondaryCredentialingContactInformation.FirstName + " " + SecondaryCredentialingContactInformation.MiddleName + " " + SecondaryCredentialingContactInformation.LastName;
                                    }
                                    else
                                    {
                                        pmodel.PrimaryCredContact_FullName = SecondaryCredentialingContactInformation.FirstName + " " + SecondaryCredentialingContactInformation.LastName;
                                    }
                                    pmodel.PrimaryCredContact_FirstName = SecondaryCredentialingContactInformation.FirstName;
                                    pmodel.PrimaryCredContact_MI = SecondaryCredentialingContactInformation.MiddleName;
                                    pmodel.PrimaryCredContact_LastName = SecondaryCredentialingContactInformation.LastName;

                                    pmodel.PrimaryCredContact_Street = SecondaryCredentialingContactInformation.Street;
                                    pmodel.PrimaryCredContact_Suite = SecondaryCredentialingContactInformation.Building;
                                    pmodel.PrimaryCredContact_City = SecondaryCredentialingContactInformation.City;
                                    pmodel.PrimaryCredContact_State = SecondaryCredentialingContactInformation.State;
                                    pmodel.PrimaryCredContact_ZipCode = SecondaryCredentialingContactInformation.ZipCode;
                                    pmodel.PrimaryCredContact_Phone = SecondaryCredentialingContactInformation.Telephone;
                                    pmodel.PrimaryCredContact_Fax = SecondaryCredentialingContactInformation.FaxNumber;
                                    pmodel.PrimaryCredContact_Email = SecondaryCredentialingContactInformation.EmailAddress;
                                    pmodel.PrimaryCredContact_MobileNumber = SecondaryCredentialingContactInformation.MobileNumber;
                                    pmodel.PrimaryCredContact_Address1 = pmodel.PrimaryCredContact_Street + ", " + pmodel.PrimaryCredContact_Suite;
                                }
                                #endregion

                                #region Open Practice Status


                                pmodel.OpenPractice_AgeLimitations1 = SecondaryPracticeLocationInfo[secpracticeLocationCount - 1].MinimumAge + " - " + SecondaryPracticeLocationInfo[secpracticeLocationCount - 1].MaximumAge;


                                #endregion

                                #region Office Manager 1

                                if (SecondaryBusinessOfficeManagerOrStaff != null)
                                {
                                    if (SecondaryBusinessOfficeManagerOrStaff.MiddleName != null)
                                        pmodel.OfficeManager_Name1 = SecondaryBusinessOfficeManagerOrStaff.FirstName + " " + SecondaryBusinessOfficeManagerOrStaff.MiddleName + " " + SecondaryBusinessOfficeManagerOrStaff.LastName;
                                    else
                                        pmodel.OfficeManager_Name1 = SecondaryBusinessOfficeManagerOrStaff.FirstName + " " + SecondaryBusinessOfficeManagerOrStaff.LastName;

                                    pmodel.OfficeManager_FirstName1 = SecondaryBusinessOfficeManagerOrStaff.FirstName;
                                    pmodel.OfficeManager_MiddleName1 = SecondaryBusinessOfficeManagerOrStaff.MiddleName;
                                    pmodel.OfficeManager_LastName1 = SecondaryBusinessOfficeManagerOrStaff.LastName;
                                    pmodel.OfficeManager_Email1 = SecondaryBusinessOfficeManagerOrStaff.EmailAddress;
                                    pmodel.OfficeManager_PoBoxAddress1 = SecondaryBusinessOfficeManagerOrStaff.POBoxAddress;
                                    pmodel.OfficeManager_Building1 = SecondaryBusinessOfficeManagerOrStaff.Building;
                                    pmodel.OfficeManager_Street1 = SecondaryBusinessOfficeManagerOrStaff.Street;
                                    pmodel.OfficeManager_City1 = SecondaryBusinessOfficeManagerOrStaff.City;
                                    pmodel.OfficeManager_State1 = SecondaryBusinessOfficeManagerOrStaff.State;
                                    pmodel.OfficeManager_ZipCode1 = SecondaryBusinessOfficeManagerOrStaff.ZipCode;
                                    pmodel.OfficeManager_Country1 = SecondaryBusinessOfficeManagerOrStaff.Country;
                                    pmodel.OfficeManager_County1 = SecondaryBusinessOfficeManagerOrStaff.County;

                                    if (SecondaryBusinessOfficeManagerOrStaff.Telephone != null)
                                    {
                                        pmodel.OfficeManager_PhoneFirstThreeDigit1 = SecondaryBusinessOfficeManagerOrStaff.Telephone.Substring(0, SecondaryBusinessOfficeManagerOrStaff.Telephone.Length - 7);
                                        pmodel.OfficeManager_PhoneSecondThreeDigit1 = SecondaryBusinessOfficeManagerOrStaff.Telephone.Substring(3, 3);
                                        pmodel.OfficeManager_PhoneLastFourDigit1 = SecondaryBusinessOfficeManagerOrStaff.Telephone.Substring(6);

                                        pmodel.OfficeManager_Phone1 = pmodel.OfficeManager_PhoneFirstThreeDigit1 + "-" + pmodel.OfficeManager_PhoneSecondThreeDigit1 + "-" + pmodel.OfficeManager_PhoneLastFourDigit1;

                                    }




                                    if (SecondaryBusinessOfficeManagerOrStaff.Fax != null)
                                    {

                                        pmodel.OfficeManager_FaxFirstThreeDigit1 = SecondaryBusinessOfficeManagerOrStaff.Fax.Substring(0, SecondaryBusinessOfficeManagerOrStaff.Fax.Length - 7);
                                        pmodel.OfficeManager_FaxSecondThreeDigit1 = SecondaryBusinessOfficeManagerOrStaff.Fax.Substring(3, 3);
                                        pmodel.OfficeManager_FaxLastFourDigit1 = SecondaryBusinessOfficeManagerOrStaff.Fax.Substring(6);

                                        pmodel.OfficeManager_Fax1 = pmodel.OfficeManager_FaxFirstThreeDigit1 + "-" + pmodel.OfficeManager_FaxSecondThreeDigit1 + "-" + pmodel.OfficeManager_FaxLastFourDigit1;


                                    }
                                }

                                #endregion

                                #region Billing Contact 1

                                if (SecondaryBillingContactPerson != null)
                                {
                                    if (SecondaryBillingContactPerson.MiddleName != null)
                                        pmodel.BillingContact_Name1 = SecondaryBillingContactPerson.FirstName + " " + SecondaryBillingContactPerson.MiddleName + " " + SecondaryBillingContactPerson.LastName;
                                    else
                                        pmodel.BillingContact_Name1 = SecondaryBillingContactPerson.FirstName + " " + SecondaryBillingContactPerson.LastName;

                                    pmodel.BillingContact_FirstName1 = SecondaryBillingContactPerson.FirstName;
                                    pmodel.BillingContact_MiddleName1 = SecondaryBillingContactPerson.MiddleName;
                                    pmodel.BillingContact_LastName1 = SecondaryBillingContactPerson.LastName;
                                    pmodel.BillingContact_Email1 = SecondaryBillingContactPerson.EmailAddress;
                                    //pmodel.BillingContact_Phone1 = BillingContactPerson.MobileNumber;
                                    //pmodel.BillingContact_Fax1 = BillingContactPerson.FaxNumber;
                                    pmodel.BillingContact_POBoxAddress1 = SecondaryBillingContactPerson.POBoxAddress;
                                    pmodel.BillingContact_Suite1 = SecondaryBillingContactPerson.Building;
                                    pmodel.BillingContact_Street1 = SecondaryBillingContactPerson.Street;
                                    pmodel.BillingContact_City1 = SecondaryBillingContactPerson.City;
                                    pmodel.BillingContact_State1 = SecondaryBillingContactPerson.State;
                                    pmodel.BillingContact_ZipCode1 = SecondaryBillingContactPerson.ZipCode;
                                    pmodel.BillingContact_Country1 = SecondaryBillingContactPerson.Country;
                                    pmodel.BillingContact_County1 = SecondaryBillingContactPerson.County;
                                    pmodel.BillingContact_Line1 = pmodel.BillingContact_Street1 + " " + pmodel.BillingContact_Suite1;
                                    pmodel.BillingContact_Line2 = pmodel.BillingContact_City1 + ", " + pmodel.BillingContact_State1 + " " + pmodel.BillingContact_ZipCode1;
                                    pmodel.BillingContact_City1State1 = pmodel.BillingContact_City1 + " ," + pmodel.BillingContact_State1 + ", " + pmodel.BillingContact_ZipCode1;

                                    if (SecondaryBillingContactPerson.Telephone != null)
                                    {
                                        pmodel.BillingContact_PhoneFirstThreeDigit1 = SecondaryBillingContactPerson.Telephone.Substring(0, SecondaryBillingContactPerson.Telephone.Length - 7);
                                        pmodel.BillingContact_PhoneSecondThreeDigit1 = SecondaryBillingContactPerson.Telephone.Substring(3, 3);
                                        pmodel.BillingContact_PhoneLastFourDigit1 = SecondaryBillingContactPerson.Telephone.Substring(6);
                                        pmodel.BillingContact_Phone1 = pmodel.BillingContact_PhoneFirstThreeDigit1 + "-" + pmodel.BillingContact_PhoneSecondThreeDigit1 + "-" + pmodel.BillingContact_PhoneLastFourDigit1;

                                    }

                                    if (SecondaryBillingContactPerson.Fax != null)
                                    {
                                        pmodel.BillingContact_FaxFirstThreeDigit1 = SecondaryBillingContactPerson.Fax.Substring(0, SecondaryBillingContactPerson.Fax.Length - 7);
                                        pmodel.BillingContact_FaxSecondThreeDigit1 = SecondaryBillingContactPerson.Fax.Substring(3, 3);
                                        pmodel.BillingContact_FaxLastFourDigit1 = SecondaryBillingContactPerson.Fax.Substring(6);

                                        pmodel.BillingContact_Fax1 = pmodel.BillingContact_FaxFirstThreeDigit1 + "-" + pmodel.BillingContact_FaxSecondThreeDigit1 + "-" + pmodel.BillingContact_FaxLastFourDigit1;

                                    }
                                }

                                #endregion

                                #region Payment and Remittance 1

                                if (SecondaryPaymentAndRemittance != null)
                                {
                                    if (SecondaryPaymentAndRemittance.MiddleName != null)
                                        pmodel.PaymentRemittance_Name1 = SecondaryPaymentAndRemittance.FirstName + " " + SecondaryPaymentAndRemittance.MiddleName + " " + SecondaryPaymentAndRemittance.LastName;
                                    else
                                        pmodel.PaymentRemittance_Name1 = SecondaryPaymentAndRemittance.FirstName + " " + SecondaryPaymentAndRemittance.LastName;

                                    pmodel.PaymentRemittance_FirstName1 = SecondaryPaymentAndRemittance.FirstName;
                                    pmodel.PaymentRemittance_MiddleName1 = SecondaryPaymentAndRemittance.MiddleName;
                                    pmodel.PaymentRemittance_LastName1 = SecondaryPaymentAndRemittance.LastName;
                                    pmodel.PaymentRemittance_Email1 = SecondaryPaymentAndRemittance.EmailAddress;
                                    pmodel.PaymentRemittance_POBoxAddress1 = SecondaryPaymentAndRemittance.POBoxAddress;
                                    pmodel.PaymentRemittance_Suite1 = SecondaryPaymentAndRemittance.Building;
                                    pmodel.PaymentRemittance_Street1 = SecondaryPaymentAndRemittance.Street;
                                    pmodel.PaymentRemittance_City1 = SecondaryPaymentAndRemittance.City;
                                    pmodel.PaymentRemittance_State1 = SecondaryPaymentAndRemittance.State;
                                    pmodel.PaymentRemittance_ZipCode1 = SecondaryPaymentAndRemittance.ZipCode;
                                    pmodel.PaymentRemittance_Country1 = SecondaryPaymentAndRemittance.Country;
                                    pmodel.PaymentRemittance_County1 = SecondaryPaymentAndRemittance.County;

                                    if (SecondaryPaymentAndRemittance.Telephone != null)
                                    {
                                        pmodel.PaymentRemittance_PhoneFirstThreeDigit1 = SecondaryPaymentAndRemittance.Telephone.Substring(0, SecondaryPaymentAndRemittance.Telephone.Length - 7);
                                        pmodel.PaymentRemittance_PhoneSecondThreeDigit1 = SecondaryPaymentAndRemittance.Telephone.Substring(3, 3);
                                        pmodel.PaymentRemittance_PhoneLastFourDigit1 = SecondaryPaymentAndRemittance.Telephone.Substring(6);

                                        pmodel.PaymentRemittance_Phone1 = pmodel.PaymentRemittance_PhoneFirstThreeDigit1 + "-" + pmodel.PaymentRemittance_PhoneSecondThreeDigit1 + "-" + pmodel.PaymentRemittance_PhoneLastFourDigit1;
                                        if (pmodel.PaymentRemittance_Phone1.Length > 13)
                                        {
                                            pmodel.PaymentRemittance_Phone1 = SecondaryPaymentAndRemittance.Telephone;
                                        }

                                    }




                                    if (SecondaryPaymentAndRemittance.Fax != null)
                                    {
                                        //pmodel.General_Fax1 = practiceLocation[practiceLocationCount - 1].Facility.Fax.Substring(3, practiceLocation[practiceLocationCount - 1].Facility.Fax.Length - 3);

                                        pmodel.PaymentRemittance_FaxFirstThreeDigit1 = SecondaryPaymentAndRemittance.Fax.Substring(0, SecondaryPaymentAndRemittance.Fax.Length - 7);
                                        pmodel.PaymentRemittance_FaxSecondThreeDigit1 = SecondaryPaymentAndRemittance.Fax.Substring(3, 3);
                                        pmodel.PaymentRemittance_FaxLastFourDigit1 = SecondaryPaymentAndRemittance.Fax.Substring(6);

                                        pmodel.PaymentRemittance_Fax1 = pmodel.PaymentRemittance_FaxFirstThreeDigit1 + "-" + pmodel.PaymentRemittance_FaxSecondThreeDigit1 + "-" + pmodel.PaymentRemittance_FaxLastFourDigit1;
                                    }

                                    pmodel.PaymentRemittance_ElectronicBillCapability1 = SecondaryPaymentAndRemittance.ElectronicBillingCapability;
                                    pmodel.PaymentRemittance_BillingDepartment1 = SecondaryPaymentAndRemittance.BillingDepartment;
                                    pmodel.PaymentRemittance_ChekPayableTo1 = SecondaryPaymentAndRemittance.CheckPayableTo;
                                    pmodel.PaymentRemittance_Office1 = SecondaryPaymentAndRemittance.Office;
                                }

                                #endregion

                                #region Office Hours 1

                                if (SecondaryOfficeHours != null)
                                {
                                    if (SecondaryOfficeHours.Count > 0)
                                    {
                                        if (SecondaryOfficeHours.ElementAt(0).StartTime != null)
                                            pmodel.OfficeHour_StartMonday1 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(0).StartTime);
                                        if (SecondaryOfficeHours.ElementAt(0).EndTime != null)
                                            pmodel.OfficeHour_EndMonday1 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(0).EndTime);

                                        pmodel.OfficeHour_Monday1 = pmodel.OfficeHour_StartMonday1 + " - " + pmodel.OfficeHour_EndMonday1;
                                        if (SecondaryOfficeHours.Count > 1)
                                        {
                                            if (SecondaryOfficeHours.ElementAt(1).StartTime != null)
                                                pmodel.OfficeHour_StartTuesday1 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(1).StartTime);
                                            if (SecondaryOfficeHours.ElementAt(1).EndTime != null)
                                                pmodel.OfficeHour_EndTuesday1 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(1).EndTime);
                                        }
                                        pmodel.OfficeHour_Tuesday1 = pmodel.OfficeHour_StartTuesday1 + " - " + pmodel.OfficeHour_EndTuesday1;
                                        if (SecondaryOfficeHours.Count > 2)
                                        {
                                            if (SecondaryOfficeHours.ElementAt(2).StartTime != null)
                                                pmodel.OfficeHour_StartWednesday1 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(2).StartTime);
                                            if (SecondaryOfficeHours.ElementAt(2).EndTime != null)

                                                pmodel.OfficeHour_EndWednesday1 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(2).EndTime);

                                            pmodel.OfficeHour_Wednesday1 = pmodel.OfficeHour_StartWednesday1 + " - " + pmodel.OfficeHour_EndWednesday1;
                                        }
                                        if (SecondaryOfficeHours.Count > 3)
                                        {
                                            if (SecondaryOfficeHours.ElementAt(3).StartTime != null)
                                                pmodel.OfficeHour_StartThursday1 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(3).StartTime);
                                            if (SecondaryOfficeHours.ElementAt(3).EndTime != null)
                                                pmodel.OfficeHour_EndThursday1 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(3).EndTime);

                                            pmodel.OfficeHour_Thursday1 = pmodel.OfficeHour_StartThursday1 + " - " + pmodel.OfficeHour_EndThursday1;
                                        }
                                        if (SecondaryOfficeHours.Count > 4)
                                        {
                                            if (SecondaryOfficeHours.ElementAt(4).StartTime != null)
                                                pmodel.OfficeHour_StartFriday1 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(4).StartTime);
                                            if (SecondaryOfficeHours.ElementAt(4).EndTime != null)
                                                pmodel.OfficeHour_EndFriday1 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(4).EndTime);

                                            pmodel.OfficeHour_Friday1 = pmodel.OfficeHour_StartFriday1 + " - " + pmodel.OfficeHour_EndFriday1;
                                        }
                                        if (SecondaryOfficeHours.Count > 5)
                                        {
                                            if (SecondaryOfficeHours.ElementAt(5).StartTime != null)
                                                pmodel.OfficeHour_StartSaturday1 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(5).StartTime);
                                            if (SecondaryOfficeHours.ElementAt(5).EndTime != null)

                                                pmodel.OfficeHour_EndSaturday1 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(5).EndTime);

                                            pmodel.OfficeHour_Saturday1 = pmodel.OfficeHour_StartSaturday1 + " - " + pmodel.OfficeHour_EndSaturday1;
                                        }
                                        if (SecondaryOfficeHours.Count > 6)
                                        {
                                            if (SecondaryOfficeHours.ElementAt(6).StartTime != null)
                                                pmodel.OfficeHour_StartSunday1 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(6).StartTime);
                                            if (SecondaryOfficeHours.ElementAt(6).EndTime != null)
                                                pmodel.OfficeHour_EndSunday1 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(6).EndTime);

                                            pmodel.OfficeHour_Sunday1 = pmodel.OfficeHour_StartSunday1 + " - " + pmodel.OfficeHour_EndSunday1;
                                        }
                                    }
                                }

                                #endregion

                                #region Supervising Provider 1

                                var supervisingProvider = SecondaryPracticeProviders.
                                Where(s => s.Practice == AHC.CD.Entities.MasterData.Enums.PracticeType.Supervisor.ToString() &&
                                    s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();

                                var supervisorCount = supervisingProvider.Count;

                                if (supervisorCount > 0)
                                {
                                    pmodel.CoveringColleague_FirstName1 = supervisingProvider.ElementAt(supervisorCount - 1).FirstName;
                                    pmodel.CoveringColleague_MiddleName1 = supervisingProvider.ElementAt(supervisorCount - 1).MiddleName;
                                    pmodel.CoveringColleague_LastName1 = supervisingProvider.ElementAt(supervisorCount - 1).LastName;

                                    if (pmodel.CoveringColleague_MiddleName1 != null)
                                        pmodel.CoveringColleague_FullName1 = pmodel.CoveringColleague_FirstName1 + " " + pmodel.CoveringColleague_MiddleName1 + " " + pmodel.CoveringColleague_LastName1;
                                    else
                                        pmodel.CoveringColleague_FullName1 = pmodel.CoveringColleague_FirstName1 + " " + pmodel.CoveringColleague_LastName1;


                                    if (supervisingProvider.ElementAt(supervisorCount - 1).Telephone != null)
                                    {
                                        pmodel.CoveringColleague_PhoneFirstThreeDigit1 = supervisingProvider.ElementAt(supervisorCount - 1).Telephone.Substring(0, supervisingProvider.ElementAt(supervisorCount - 1).Telephone.Length - 7);
                                        pmodel.CoveringColleague_PhoneSecondThreeDigit1 = supervisingProvider.ElementAt(supervisorCount - 1).Telephone.Substring(3, 3);
                                        pmodel.CoveringColleague_PhoneLastFourDigit1 = supervisingProvider.ElementAt(supervisorCount - 1).Telephone.Substring(6);

                                        pmodel.CoveringColleague_PhoneNumber1 = pmodel.CoveringColleague_PhoneFirstThreeDigit1 + "-" + pmodel.CoveringColleague_PhoneSecondThreeDigit1 + "-" + pmodel.CoveringColleague_PhoneLastFourDigit1;

                                    }

                                    var specialities = SecondaryPracticeProvidersSpeciality.Where(s => s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString()).ToList();

                                    if (specialities.Count > 0)
                                    {
                                        if (specialities.ElementAt(specialities.Count - 1) != null)
                                            pmodel.CoveringColleague_Specialty1 = specialities.ElementAt(specialities.Count - 1).Name;
                                    }
                                }

                                #endregion

                                #region Covering Colleagues/Partners 1

                                var patners = SecondaryPracticeProviders.
                                Where(s => s.Practice == AHC.CD.Entities.MasterData.Enums.PracticeType.CoveringColleague.ToString() &&
                                    s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();

                                var patnersCount = patners.Count;

                                if (patnersCount > 0)
                                {
                                    pmodel.Patners_FirstName1 = patners.ElementAt(patnersCount - 1).FirstName;
                                    pmodel.Patners_MiddleName1 = patners.ElementAt(patnersCount - 1).MiddleName;
                                    pmodel.Patners_LastName1 = patners.ElementAt(patnersCount - 1).LastName;

                                    if (pmodel.Patners_MiddleName1 != null)
                                        pmodel.Patners_FullName1 = pmodel.Patners_FirstName1 + " " + pmodel.Patners_MiddleName1 + " " + pmodel.Patners_LastName1;
                                    else
                                        pmodel.Patners_FullName1 = pmodel.Patners_FirstName1 + " " + pmodel.Patners_LastName1;

                                }

                                #endregion

                                #region Specific Secondary Practice Location When Primary is Null

                                if (secpracticeLocationCount == 1)
                                {
                                    #region Specific Secondary Practice Location Address 1

                                    if (SecondaryPracticeLocationInfo[secpracticeLocationCount - 1] != null)
                                    {
                                        pmodel.General_PracticeLocationAddress2 = SecondaryPracticeLocationInfo[secpracticeLocationCount - 1].Street + " " + SecondaryPracticeLocationInfo[secpracticeLocationCount - 1].City + ", " + SecondaryPracticeLocationInfo[secpracticeLocationCount - 1].State + ", " + SecondaryPracticeLocationInfo[secpracticeLocationCount - 1].ZipCode;

                                        if (SecondaryPracticeLocationInfo[secpracticeLocationCount - 1].Telephone != null)
                                        {
                                            pmodel.General_PhoneFirstThreeDigit2 = SecondaryPracticeLocationInfo[secpracticeLocationCount - 1].Telephone.Substring(0, SecondaryPracticeLocationInfo[secpracticeLocationCount - 1].Telephone.Length - 7);
                                            pmodel.General_PhoneSecondThreeDigit2 = SecondaryPracticeLocationInfo[secpracticeLocationCount - 1].Telephone.Substring(3, 3);
                                            pmodel.General_PhoneLastFourDigit2 = SecondaryPracticeLocationInfo[secpracticeLocationCount - 1].Telephone.Substring(6);

                                            pmodel.General_Phone2 = pmodel.General_PhoneFirstThreeDigit2 + "-" + pmodel.General_PhoneSecondThreeDigit2 + "-" + pmodel.General_PhoneLastFourDigit2;
                                            if (pmodel.General_Phone2.Length > 13)
                                            {
                                                pmodel.General_Phone2 = secondaryPracticeLocation[secpracticeLocationCount - 1].Telephone;
                                            }
                                            pmodel.LocationAddress2_Line3 = "Phone : " + pmodel.General_Phone2;
                                        }

                                        pmodel.General_Email2 = SecondaryPracticeLocationInfo[secpracticeLocationCount - 1].EmailAddress;

                                        if (SecondaryPracticeLocationInfo[secpracticeLocationCount - 1].Fax != null)
                                        {
                                            pmodel.General_FaxFirstThreeDigit2 = SecondaryPracticeLocationInfo[secpracticeLocationCount - 1].Fax.Substring(0, SecondaryPracticeLocationInfo[secpracticeLocationCount - 1].Fax.Length - 7);
                                            pmodel.General_FaxSecondThreeDigit2 = SecondaryPracticeLocationInfo[secpracticeLocationCount - 1].Fax.Substring(3, 3);
                                            pmodel.General_FaxLastFourDigit2 = SecondaryPracticeLocationInfo[secpracticeLocationCount - 1].Fax.Substring(6);

                                            pmodel.General_Fax2 = pmodel.General_FaxFirstThreeDigit2 + "-" + pmodel.General_FaxSecondThreeDigit2 + "-" + pmodel.General_FaxLastFourDigit2;
                                            if (pmodel.General_Fax2.Length > 13)
                                            {
                                                pmodel.General_Fax2 = SecondaryPracticeLocationInfo[secpracticeLocationCount - 1].Fax;
                                            }
                                            pmodel.LocationAddress2_Line3 = pmodel.LocationAddress2_Line3 + " " + "Fax : " + pmodel.General_Fax2;
                                        }

                                        pmodel.General_AccessGroupName2 = "Access Healthcare Physicians, LLC";
                                        pmodel.General_Access2GroupName2 = "Access 2 Healthcare Physicians, LLC";

                                        pmodel.General_AccessGroupTaxId2 = "451444883";
                                        pmodel.General_Access2GroupTaxId2 = "451024515";
                                        pmodel.General_FacilityName2 = SecondaryPracticeLocationInfo[secpracticeLocationCount - 1].PracticeLocation_FacilityName;
                                        pmodel.General_PracticeOrCorporateName2 = SecondaryPracticeLocationInfo[secpracticeLocationCount - 1].PracticeLocationCorporateName;
                                        pmodel.General_Suite2 = SecondaryPracticeLocationInfo[secpracticeLocationCount - 1].Building;
                                        pmodel.General_Street2 = SecondaryPracticeLocationInfo[secpracticeLocationCount - 1].Street;
                                        pmodel.General_City2 = SecondaryPracticeLocationInfo[secpracticeLocationCount - 1].City;
                                        pmodel.General_State2 = SecondaryPracticeLocationInfo[secpracticeLocationCount - 1].State;
                                        pmodel.General_ZipCode2 = SecondaryPracticeLocationInfo[secpracticeLocationCount - 1].ZipCode;
                                        pmodel.General_Country2 = SecondaryPracticeLocationInfo[secpracticeLocationCount - 1].Country;
                                        pmodel.General_County2 = SecondaryPracticeLocationInfo[secpracticeLocationCount - 1].County;
                                        pmodel.LocationAddress2_Line1 = pmodel.General_Street2 + " " + pmodel.General_Suite2;
                                        pmodel.LocationAddress2_Line2 = pmodel.General_City2 + ", " + pmodel.General_State2 + ", " + pmodel.General_ZipCode2;
                                        pmodel.General_City2State2 = pmodel.General_City2 + ", " + pmodel.General_State2 + ", " + pmodel.General_ZipCode2;
                                        pmodel.General_FacilityPracticeName2 = pmodel.General_FacilityName2 + ", " + pmodel.General_PracticeOrCorporateName2;

                                    }

                                    #endregion

                                    #region Specific Secondary Practice Location Billing Contact 1

                                    if (SecondaryBillingContactPerson != null)
                                    {
                                        if (SecondaryBillingContactPerson.MiddleName != null)
                                            pmodel.BillingContact_Name2 = SecondaryBillingContactPerson.FirstName + " " + SecondaryBillingContactPerson.MiddleName + " " + SecondaryBillingContactPerson.LastName;
                                        else
                                            pmodel.BillingContact_Name2 = SecondaryBillingContactPerson.FirstName + " " + SecondaryBillingContactPerson.LastName;

                                        pmodel.BillingContact_FirstName2 = SecondaryBillingContactPerson.FirstName;
                                        pmodel.BillingContact_MiddleName2 = SecondaryBillingContactPerson.MiddleName;
                                        pmodel.BillingContact_LastName2 = SecondaryBillingContactPerson.LastName;
                                        pmodel.BillingContact_Email2 = SecondaryBillingContactPerson.EmailAddress;
                                        //pmodel.BillingContact_Phone1 = SecondaryBillingContactPerson.MobileNumber;
                                        //pmodel.BillingContact_Fax1 = SecondaryBillingContactPerson.FaxNumber;
                                        pmodel.BillingContact_POBoxAddress2 = SecondaryBillingContactPerson.POBoxAddress;
                                        pmodel.BillingContact_Suite2 = SecondaryBillingContactPerson.Building;
                                        pmodel.BillingContact_Street2 = SecondaryBillingContactPerson.Street;
                                        pmodel.BillingContact_City2 = SecondaryBillingContactPerson.City;
                                        pmodel.BillingContact_State2 = SecondaryBillingContactPerson.State;
                                        pmodel.BillingContact_ZipCode2 = SecondaryBillingContactPerson.ZipCode;
                                        pmodel.BillingContact_Country2 = SecondaryBillingContactPerson.Country;
                                        pmodel.BillingContact_County2 = SecondaryBillingContactPerson.County;
                                        pmodel.BillingContact_Country2 = SecondaryBillingContactPerson.Country;
                                        pmodel.BillingContact_County2 = SecondaryBillingContactPerson.County;
                                        pmodel.BillingContact_City2State2 = pmodel.BillingContact_City2 + " ," + pmodel.BillingContact_State2 + ", " + pmodel.BillingContact_ZipCode2;
                                        if (SecondaryBillingContactPerson.Telephone != null)
                                        {
                                            pmodel.BillingContact_PhoneFirstThreeDigit2 = SecondaryBillingContactPerson.Telephone.Substring(0, SecondaryBillingContactPerson.Telephone.Length - 7);
                                            pmodel.BillingContact_PhoneSecondThreeDigit2 = SecondaryBillingContactPerson.Telephone.Substring(3, 3);
                                            pmodel.BillingContact_PhoneLastFourDigit2 = SecondaryBillingContactPerson.Telephone.Substring(6);

                                            pmodel.BillingContact_Phone2 = pmodel.BillingContact_PhoneFirstThreeDigit2 + "-" + pmodel.BillingContact_PhoneSecondThreeDigit2 + "-" + pmodel.BillingContact_PhoneLastFourDigit2;
                                        }

                                        if (SecondaryBillingContactPerson.Fax != null)
                                        {
                                            //pmodel.General_Fax1 = practiceLocation[practiceLocationCount - 2].Facility.Fax.Substring(3, practiceLocation[practiceLocationCount - 2].Facility.Fax.Length - 3);

                                            pmodel.BillingContact_FaxFirstThreeDigit2 = SecondaryBillingContactPerson.Fax.Substring(0, SecondaryBillingContactPerson.Fax.Length - 7);
                                            pmodel.BillingContact_FaxSecondThreeDigit2 = SecondaryBillingContactPerson.Fax.Substring(3, 3);
                                            pmodel.BillingContact_FaxLastFourDigit2 = SecondaryBillingContactPerson.Fax.Substring(6);

                                            pmodel.BillingContact_Fax2 = pmodel.BillingContact_FaxFirstThreeDigit2 + "-" + pmodel.BillingContact_FaxSecondThreeDigit2 + "-" + pmodel.BillingContact_FaxLastFourDigit2;
                                        }
                                    }

                                    #endregion

                                    #region Specific Secondary Practice Location Office Hours 1

                                    if (SecondaryOfficeHours != null)
                                    {
                                        if (SecondaryOfficeHours.Count > 0)
                                        {
                                            if (SecondaryOfficeHours.ElementAt(0).StartTime != null)
                                                pmodel.OfficeHour_StartMonday2 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(0).StartTime);
                                            if (SecondaryOfficeHours.ElementAt(0).EndTime != null)
                                                pmodel.OfficeHour_EndMonday2 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(0).EndTime);

                                            pmodel.OfficeHour_Monday2 = pmodel.OfficeHour_StartMonday2 + " - " + pmodel.OfficeHour_EndMonday2;
                                            if (SecondaryOfficeHours.Count > 1)
                                            {
                                                if (SecondaryOfficeHours.ElementAt(1).StartTime != null)
                                                    pmodel.OfficeHour_StartTuesday2 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(1).StartTime);
                                                if (SecondaryOfficeHours.ElementAt(1).EndTime != null)
                                                    pmodel.OfficeHour_EndTuesday2 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(1).EndTime);
                                            }
                                            pmodel.OfficeHour_Tuesday2 = pmodel.OfficeHour_StartTuesday2 + " - " + pmodel.OfficeHour_EndTuesday2;
                                            if (SecondaryOfficeHours.Count > 2)
                                            {
                                                if (SecondaryOfficeHours.ElementAt(2).StartTime != null)
                                                    pmodel.OfficeHour_StartWednesday2 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(2).StartTime);
                                                if (SecondaryOfficeHours.ElementAt(2).EndTime != null)

                                                    pmodel.OfficeHour_EndWednesday2 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(2).EndTime);

                                                pmodel.OfficeHour_Wednesday2 = pmodel.OfficeHour_StartWednesday2 + " - " + pmodel.OfficeHour_EndWednesday2;
                                            }
                                            if (SecondaryOfficeHours.Count > 3)
                                            {
                                                if (SecondaryOfficeHours.ElementAt(3).StartTime != null)
                                                    pmodel.OfficeHour_StartThursday2 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(3).StartTime);
                                                if (SecondaryOfficeHours.ElementAt(3).EndTime != null)
                                                    pmodel.OfficeHour_EndThursday2 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(3).EndTime);

                                                pmodel.OfficeHour_Thursday2 = pmodel.OfficeHour_StartThursday2 + " - " + pmodel.OfficeHour_EndThursday2;
                                            }
                                            if (SecondaryOfficeHours.Count > 4)
                                            {
                                                if (SecondaryOfficeHours.ElementAt(4).StartTime != null)
                                                    pmodel.OfficeHour_StartFriday2 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(4).StartTime);
                                                if (SecondaryOfficeHours.ElementAt(4).EndTime != null)
                                                    pmodel.OfficeHour_EndFriday2 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(4).EndTime);

                                                pmodel.OfficeHour_Friday2 = pmodel.OfficeHour_StartFriday2 + " - " + pmodel.OfficeHour_EndFriday2;
                                            }
                                            if (SecondaryOfficeHours.Count > 5)
                                            {
                                                if (SecondaryOfficeHours.ElementAt(5).StartTime != null)
                                                    pmodel.OfficeHour_StartSaturday2 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(5).StartTime);
                                                if (SecondaryOfficeHours.ElementAt(5).EndTime != null)

                                                    pmodel.OfficeHour_EndSaturday2 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(5).EndTime);

                                                pmodel.OfficeHour_Saturday2 = pmodel.OfficeHour_StartSaturday2 + " - " + pmodel.OfficeHour_EndSaturday2;
                                            }
                                            if (SecondaryOfficeHours.Count > 6)
                                            {
                                                if (SecondaryOfficeHours.ElementAt(6).StartTime != null)
                                                    pmodel.OfficeHour_StartSunday2 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(6).StartTime);
                                                if (SecondaryOfficeHours.ElementAt(6).EndTime != null)
                                                    pmodel.OfficeHour_EndSunday2 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(6).EndTime);

                                                pmodel.OfficeHour_Sunday2 = pmodel.OfficeHour_StartSunday2 + " - " + pmodel.OfficeHour_EndSunday2;
                                            }
                                        }
                                    }

                                    #endregion
                                }
                                #endregion
                            }
                            //IF MORE SECONDARY INFORMATION 
                            if (secpracticeLocationCount > 1)
                            {

                                #region  2 Secondary Practice Location as Secondary Practice Location

                                #region Address 2

                                if (SecondaryPracticeLocationInfo[secpracticeLocationCount - 2] != null)
                                {
                                    pmodel.General_PracticeLocationAddress2 = SecondaryPracticeLocationInfo[secpracticeLocationCount - 2].Street + " " + SecondaryPracticeLocationInfo[secpracticeLocationCount - 2].City + ", " + SecondaryPracticeLocationInfo[secpracticeLocationCount - 2].State + ", " + SecondaryPracticeLocationInfo[secpracticeLocationCount - 2].ZipCode;

                                    if (SecondaryPracticeLocationInfo[secpracticeLocationCount - 2].Telephone != null)
                                    {
                                        pmodel.General_PhoneFirstThreeDigit2 = SecondaryPracticeLocationInfo[secpracticeLocationCount - 2].Telephone.Substring(0, SecondaryPracticeLocationInfo[secpracticeLocationCount - 2].Telephone.Length - 7);
                                        pmodel.General_PhoneSecondThreeDigit2 = SecondaryPracticeLocationInfo[secpracticeLocationCount - 2].Telephone.Substring(3, 3);
                                        pmodel.General_PhoneLastFourDigit2 = SecondaryPracticeLocationInfo[secpracticeLocationCount - 2].Telephone.Substring(6);

                                        pmodel.General_Phone2 = pmodel.General_PhoneFirstThreeDigit2 + "-" + pmodel.General_PhoneSecondThreeDigit2 + "-" + pmodel.General_PhoneLastFourDigit2;
                                        if (pmodel.General_Phone2.Length > 13)
                                        {
                                            pmodel.General_Phone2 = secondaryPracticeLocation[secpracticeLocationCount - 2].Telephone;
                                        }
                                        pmodel.LocationAddress2_Line3 = "Phone : " + pmodel.General_Phone2;
                                    }

                                    pmodel.General_Email2 = SecondaryPracticeLocationInfo[secpracticeLocationCount - 2].EmailAddress;

                                    if (SecondaryPracticeLocationInfo[secpracticeLocationCount - 2].Fax != null)
                                    {
                                        pmodel.General_FaxFirstThreeDigit2 = SecondaryPracticeLocationInfo[secpracticeLocationCount - 2].Fax.Substring(0, SecondaryPracticeLocationInfo[secpracticeLocationCount - 2].Fax.Length - 7);
                                        pmodel.General_FaxSecondThreeDigit2 = SecondaryPracticeLocationInfo[secpracticeLocationCount - 2].Fax.Substring(3, 3);
                                        pmodel.General_FaxLastFourDigit2 = SecondaryPracticeLocationInfo[secpracticeLocationCount - 2].Fax.Substring(6);

                                        pmodel.General_Fax2 = pmodel.General_FaxFirstThreeDigit2 + "-" + pmodel.General_FaxSecondThreeDigit2 + "-" + pmodel.General_FaxLastFourDigit2;
                                        if (pmodel.General_Fax2.Length > 13)
                                        {
                                            pmodel.General_Fax2 = SecondaryPracticeLocationInfo[secpracticeLocationCount - 2].Fax;
                                        }
                                        pmodel.LocationAddress2_Line3 = pmodel.LocationAddress2_Line3 + " " + "Fax : " + pmodel.General_Fax2;
                                    }

                                    pmodel.General_AccessGroupName2 = "Access Healthcare Physicians, LLC";
                                    pmodel.General_Access2GroupName2 = "Access 2 Healthcare Physicians, LLC";

                                    pmodel.General_AccessGroupTaxId2 = "451444883";
                                    pmodel.General_Access2GroupTaxId2 = "451024515";
                                    pmodel.General_FacilityName2 = SecondaryPracticeLocationInfo[secpracticeLocationCount - 2].PracticeLocation_FacilityName;
                                    pmodel.General_PracticeOrCorporateName2 = SecondaryPracticeLocationInfo[secpracticeLocationCount - 2].PracticeLocationCorporateName;
                                    pmodel.General_Suite2 = SecondaryPracticeLocationInfo[secpracticeLocationCount - 2].Building;
                                    pmodel.General_Street2 = SecondaryPracticeLocationInfo[secpracticeLocationCount - 2].Street;
                                    pmodel.General_City2 = SecondaryPracticeLocationInfo[secpracticeLocationCount - 2].City;
                                    pmodel.General_State2 = SecondaryPracticeLocationInfo[secpracticeLocationCount - 2].State;
                                    pmodel.General_ZipCode2 = SecondaryPracticeLocationInfo[secpracticeLocationCount - 2].ZipCode;
                                    pmodel.General_Country2 = SecondaryPracticeLocationInfo[secpracticeLocationCount - 2].Country;
                                    pmodel.General_County2 = SecondaryPracticeLocationInfo[secpracticeLocationCount - 2].County;
                                    pmodel.LocationAddress2_Line1 = pmodel.General_Street2 + " " + pmodel.General_Suite2;
                                    pmodel.LocationAddress2_Line2 = pmodel.General_City2 + ", " + pmodel.General_State2 + ", " + pmodel.General_ZipCode2;
                                    pmodel.General_City2State2 = pmodel.General_City2 + ", " + pmodel.General_State2 + ", " + pmodel.General_ZipCode2;
                                    pmodel.General_FacilityPracticeName2 = pmodel.General_FacilityName2 + ", " + pmodel.General_PracticeOrCorporateName2;

                                }

                                #endregion

                                #region Languages 2

                                if (SecondaryPracticeLocationInfo[secpracticeLocationCount - 2] != null)
                                {

                                    if (SecondaryNonEnglishlanguageData != null)
                                    {
                                        var languages = SecondaryNonEnglishlanguageData.Where(l => l.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();
                                        if (languages.Count > 0)
                                        {
                                            foreach (var language in languages)
                                            {
                                                if (language != null)
                                                    pmodel.Languages_Known1 += language.Language + ",";
                                            }
                                        }
                                    }
                                }

                                #endregion

                                #region Open Practice Status 2

                                pmodel.OpenPractice_AgeLimitations2 = SecondaryPracticeLocationInfo[secpracticeLocationCount - 2].MinimumAge + " - " + SecondaryPracticeLocationInfo[secpracticeLocationCount - 2].MaximumAge;

                                #endregion

                                #region Office Manager 2

                                if (SecondaryBusinessOfficeManagerOrStaff != null)
                                {
                                    if (SecondaryBusinessOfficeManagerOrStaff.MiddleName != null)
                                        pmodel.OfficeManager_Name2 = SecondaryBusinessOfficeManagerOrStaff.FirstName + " " + SecondaryBusinessOfficeManagerOrStaff.MiddleName + " " + SecondaryBusinessOfficeManagerOrStaff.LastName;
                                    else
                                        pmodel.OfficeManager_Name2 = SecondaryBusinessOfficeManagerOrStaff.FirstName + " " + SecondaryBusinessOfficeManagerOrStaff.LastName;

                                    pmodel.OfficeManager_FirstName2 = SecondaryBusinessOfficeManagerOrStaff.FirstName;
                                    pmodel.OfficeManager_MiddleName2 = SecondaryBusinessOfficeManagerOrStaff.MiddleName;
                                    pmodel.OfficeManager_LastName2 = SecondaryBusinessOfficeManagerOrStaff.LastName;
                                    pmodel.OfficeManager_Email2 = SecondaryBusinessOfficeManagerOrStaff.EmailAddress;
                                    pmodel.OfficeManager_PoBoxAddress2 = SecondaryBusinessOfficeManagerOrStaff.POBoxAddress;
                                    pmodel.OfficeManager_Building2 = SecondaryBusinessOfficeManagerOrStaff.Building;
                                    pmodel.OfficeManager_Street2 = SecondaryBusinessOfficeManagerOrStaff.Street;
                                    pmodel.OfficeManager_City2 = SecondaryBusinessOfficeManagerOrStaff.City;
                                    pmodel.OfficeManager_State2 = SecondaryBusinessOfficeManagerOrStaff.State;
                                    pmodel.OfficeManager_ZipCode2 = SecondaryBusinessOfficeManagerOrStaff.ZipCode;
                                    pmodel.OfficeManager_Country2 = SecondaryBusinessOfficeManagerOrStaff.Country;
                                    pmodel.OfficeManager_County2 = SecondaryBusinessOfficeManagerOrStaff.County;

                                    if (SecondaryBusinessOfficeManagerOrStaff.Telephone != null)
                                    {
                                        pmodel.OfficeManager_PhoneFirstThreeDigit2 = SecondaryBusinessOfficeManagerOrStaff.Telephone.Substring(0, SecondaryBusinessOfficeManagerOrStaff.Telephone.Length - 7);
                                        pmodel.OfficeManager_PhoneSecondThreeDigit2 = SecondaryBusinessOfficeManagerOrStaff.Telephone.Substring(3, 3);
                                        pmodel.OfficeManager_PhoneLastFourDigit2 = SecondaryBusinessOfficeManagerOrStaff.Telephone.Substring(6);

                                        pmodel.OfficeManager_Phone2 = pmodel.OfficeManager_PhoneFirstThreeDigit2 + "-" + pmodel.OfficeManager_PhoneSecondThreeDigit2 + "-" + pmodel.OfficeManager_PhoneLastFourDigit2;

                                    }

                                    if (SecondaryBusinessOfficeManagerOrStaff.Fax != null)
                                    {

                                        pmodel.OfficeManager_FaxFirstThreeDigit2 = SecondaryBusinessOfficeManagerOrStaff.Fax.Substring(0, SecondaryBusinessOfficeManagerOrStaff.Fax.Length - 7);
                                        pmodel.OfficeManager_FaxSecondThreeDigit2 = SecondaryBusinessOfficeManagerOrStaff.Fax.Substring(3, 3);
                                        pmodel.OfficeManager_FaxLastFourDigit2 = SecondaryBusinessOfficeManagerOrStaff.Fax.Substring(6);

                                        pmodel.OfficeManager_Fax1 = pmodel.OfficeManager_FaxFirstThreeDigit2 + "-" + pmodel.OfficeManager_FaxSecondThreeDigit2 + "-" + pmodel.OfficeManager_FaxLastFourDigit2;


                                    }
                                }

                                #endregion

                                #region Billing Contact 2

                                if (SecondaryBillingContactPerson != null)
                                {
                                    if (SecondaryBillingContactPerson.MiddleName != null)
                                        pmodel.BillingContact_Name2 = SecondaryBillingContactPerson.FirstName + " " + SecondaryBillingContactPerson.MiddleName + " " + SecondaryBillingContactPerson.LastName;
                                    else
                                        pmodel.BillingContact_Name2 = SecondaryBillingContactPerson.FirstName + " " + SecondaryBillingContactPerson.LastName;

                                    pmodel.BillingContact_FirstName2 = SecondaryBillingContactPerson.FirstName;
                                    pmodel.BillingContact_MiddleName2 = SecondaryBillingContactPerson.MiddleName;
                                    pmodel.BillingContact_LastName2 = SecondaryBillingContactPerson.LastName;
                                    pmodel.BillingContact_Email2 = SecondaryBillingContactPerson.EmailAddress;
                                    //pmodel.BillingContact_Phone1 = SecondaryBillingContactPerson.MobileNumber;
                                    //pmodel.BillingContact_Fax1 = SecondaryBillingContactPerson.FaxNumber;
                                    pmodel.BillingContact_POBoxAddress2 = SecondaryBillingContactPerson.POBoxAddress;
                                    pmodel.BillingContact_Suite2 = SecondaryBillingContactPerson.Building;
                                    pmodel.BillingContact_Street2 = SecondaryBillingContactPerson.Street;
                                    pmodel.BillingContact_City2 = SecondaryBillingContactPerson.City;
                                    pmodel.BillingContact_State2 = SecondaryBillingContactPerson.State;
                                    pmodel.BillingContact_ZipCode2 = SecondaryBillingContactPerson.ZipCode;
                                    pmodel.BillingContact_Country2 = SecondaryBillingContactPerson.Country;
                                    pmodel.BillingContact_County2 = SecondaryBillingContactPerson.County;
                                    pmodel.BillingContact_Country2 = SecondaryBillingContactPerson.Country;
                                    pmodel.BillingContact_County2 = SecondaryBillingContactPerson.County;
                                    pmodel.BillingContact_City2State2 = pmodel.BillingContact_City2 + " ," + pmodel.BillingContact_State2 + ", " + pmodel.BillingContact_ZipCode2;
                                    if (SecondaryBillingContactPerson.Telephone != null)
                                    {
                                        pmodel.BillingContact_PhoneFirstThreeDigit2 = SecondaryBillingContactPerson.Telephone.Substring(0, SecondaryBillingContactPerson.Telephone.Length - 7);
                                        pmodel.BillingContact_PhoneSecondThreeDigit2 = SecondaryBillingContactPerson.Telephone.Substring(3, 3);
                                        pmodel.BillingContact_PhoneLastFourDigit2 = SecondaryBillingContactPerson.Telephone.Substring(6);

                                        pmodel.BillingContact_Phone2 = pmodel.BillingContact_PhoneFirstThreeDigit2 + "-" + pmodel.BillingContact_PhoneSecondThreeDigit2 + "-" + pmodel.BillingContact_PhoneLastFourDigit2;
                                    }

                                    if (SecondaryBillingContactPerson.Fax != null)
                                    {
                                        //pmodel.General_Fax1 = practiceLocation[practiceLocationCount - 2].Facility.Fax.Substring(3, practiceLocation[practiceLocationCount - 2].Facility.Fax.Length - 3);

                                        pmodel.BillingContact_FaxFirstThreeDigit2 = SecondaryBillingContactPerson.Fax.Substring(0, SecondaryBillingContactPerson.Fax.Length - 7);
                                        pmodel.BillingContact_FaxSecondThreeDigit2 = SecondaryBillingContactPerson.Fax.Substring(3, 3);
                                        pmodel.BillingContact_FaxLastFourDigit2 = SecondaryBillingContactPerson.Fax.Substring(6);

                                        pmodel.BillingContact_Fax2 = pmodel.BillingContact_FaxFirstThreeDigit2 + "-" + pmodel.BillingContact_FaxSecondThreeDigit2 + "-" + pmodel.BillingContact_FaxLastFourDigit2;
                                    }
                                }

                                #endregion

                                #region Payment and Remittance 2

                                if (SecondaryPaymentAndRemittance != null)
                                {
                                    if (SecondaryPaymentAndRemittance.MiddleName != null)
                                        pmodel.PaymentRemittance_Name2 = SecondaryPaymentAndRemittance.FirstName + " " + SecondaryPaymentAndRemittance.MiddleName + " " + SecondaryPaymentAndRemittance.LastName;
                                    else
                                        pmodel.PaymentRemittance_Name2 = SecondaryPaymentAndRemittance.FirstName + " " + SecondaryPaymentAndRemittance.LastName;

                                    pmodel.PaymentRemittance_FirstName2 = SecondaryPaymentAndRemittance.FirstName;
                                    pmodel.PaymentRemittance_MiddleName2 = SecondaryPaymentAndRemittance.MiddleName;
                                    pmodel.PaymentRemittance_LastName2 = SecondaryPaymentAndRemittance.LastName;
                                    pmodel.PaymentRemittance_Email2 = SecondaryPaymentAndRemittance.EmailAddress;
                                    pmodel.PaymentRemittance_POBoxAddress2 = SecondaryPaymentAndRemittance.POBoxAddress;
                                    pmodel.PaymentRemittance_Suite2 = SecondaryPaymentAndRemittance.Building;
                                    pmodel.PaymentRemittance_Street2 = SecondaryPaymentAndRemittance.Street;
                                    pmodel.PaymentRemittance_City2 = SecondaryPaymentAndRemittance.City;
                                    pmodel.PaymentRemittance_State2 = SecondaryPaymentAndRemittance.State;
                                    pmodel.PaymentRemittance_ZipCode2 = SecondaryPaymentAndRemittance.ZipCode;
                                    pmodel.PaymentRemittance_Country2 = SecondaryPaymentAndRemittance.Country;
                                    pmodel.PaymentRemittance_County2 = SecondaryPaymentAndRemittance.County;

                                    if (SecondaryPaymentAndRemittance.Telephone != null)
                                    {
                                        pmodel.PaymentRemittance_PhoneFirstThreeDigit2 = SecondaryPaymentAndRemittance.Telephone.Substring(0, SecondaryPaymentAndRemittance.Telephone.Length - 7);
                                        pmodel.PaymentRemittance_PhoneSecondThreeDigit2 = SecondaryPaymentAndRemittance.Telephone.Substring(3, 3);
                                        pmodel.PaymentRemittance_PhoneLastFourDigit2 = SecondaryPaymentAndRemittance.Telephone.Substring(6);

                                        pmodel.PaymentRemittance_Phone2 = pmodel.PaymentRemittance_PhoneFirstThreeDigit2 + "-" + pmodel.PaymentRemittance_PhoneSecondThreeDigit2 + "-" + pmodel.PaymentRemittance_PhoneLastFourDigit2;
                                    }

                                    if (SecondaryPaymentAndRemittance.Fax != null)
                                    {
                                        //pmodel.General_Fax1 = practiceLocation[practiceLocationCount - 2].Facility.Fax.Substring(3, practiceLocation[practiceLocationCount - 2].Facility.Fax.Length - 3);

                                        pmodel.PaymentRemittance_FaxFirstThreeDigit2 = SecondaryPaymentAndRemittance.Fax.Substring(0, SecondaryPaymentAndRemittance.Fax.Length - 7);
                                        pmodel.PaymentRemittance_FaxSecondThreeDigit2 = SecondaryPaymentAndRemittance.Fax.Substring(3, 3);
                                        pmodel.PaymentRemittance_FaxLastFourDigit2 = SecondaryPaymentAndRemittance.Fax.Substring(6);

                                        pmodel.PaymentRemittance_Fax2 = pmodel.PaymentRemittance_FaxFirstThreeDigit2 + "-" + pmodel.PaymentRemittance_FaxSecondThreeDigit2 + "-" + pmodel.PaymentRemittance_FaxLastFourDigit2;
                                    }

                                    pmodel.PaymentRemittance_ElectronicBillCapability2 = SecondaryPaymentAndRemittance.ElectronicBillingCapability;
                                    pmodel.PaymentRemittance_BillingDepartment2 = SecondaryPaymentAndRemittance.BillingDepartment;
                                    pmodel.PaymentRemittance_ChekPayableTo2 = SecondaryPaymentAndRemittance.CheckPayableTo;
                                    pmodel.PaymentRemittance_Office2 = SecondaryPaymentAndRemittance.Office;
                                }

                                #endregion

                                #region Office Hours 2

                                if (SecondaryOfficeHours != null)
                                {
                                    if (SecondaryOfficeHours.Count > 0)
                                    {
                                        if (SecondaryOfficeHours.ElementAt(0).StartTime != null)
                                            pmodel.OfficeHour_StartMonday2 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(0).StartTime);
                                        if (SecondaryOfficeHours.ElementAt(0).EndTime != null)
                                            pmodel.OfficeHour_EndMonday2 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(0).EndTime);

                                        pmodel.OfficeHour_Monday2 = pmodel.OfficeHour_StartMonday2 + " - " + pmodel.OfficeHour_EndMonday2;
                                        if (SecondaryOfficeHours.Count > 1)
                                        {
                                            if (SecondaryOfficeHours.ElementAt(1).StartTime != null)
                                                pmodel.OfficeHour_StartTuesday2 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(1).StartTime);
                                            if (SecondaryOfficeHours.ElementAt(1).EndTime != null)
                                                pmodel.OfficeHour_EndTuesday2 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(1).EndTime);
                                        }
                                        pmodel.OfficeHour_Tuesday2 = pmodel.OfficeHour_StartTuesday2 + " - " + pmodel.OfficeHour_EndTuesday2;
                                        if (SecondaryOfficeHours.Count > 2)
                                        {
                                            if (SecondaryOfficeHours.ElementAt(2).StartTime != null)
                                                pmodel.OfficeHour_StartWednesday2 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(2).StartTime);
                                            if (SecondaryOfficeHours.ElementAt(2).EndTime != null)

                                                pmodel.OfficeHour_EndWednesday2 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(2).EndTime);

                                            pmodel.OfficeHour_Wednesday2 = pmodel.OfficeHour_StartWednesday2 + " - " + pmodel.OfficeHour_EndWednesday2;
                                        }
                                        if (SecondaryOfficeHours.Count > 3)
                                        {
                                            if (SecondaryOfficeHours.ElementAt(3).StartTime != null)
                                                pmodel.OfficeHour_StartThursday2 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(3).StartTime);
                                            if (SecondaryOfficeHours.ElementAt(3).EndTime != null)
                                                pmodel.OfficeHour_EndThursday2 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(3).EndTime);

                                            pmodel.OfficeHour_Thursday2 = pmodel.OfficeHour_StartThursday2 + " - " + pmodel.OfficeHour_EndThursday2;
                                        }
                                        if (SecondaryOfficeHours.Count > 4)
                                        {
                                            if (SecondaryOfficeHours.ElementAt(4).StartTime != null)
                                                pmodel.OfficeHour_StartFriday2 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(4).StartTime);
                                            if (SecondaryOfficeHours.ElementAt(4).EndTime != null)
                                                pmodel.OfficeHour_EndFriday2 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(4).EndTime);

                                            pmodel.OfficeHour_Friday2 = pmodel.OfficeHour_StartFriday2 + " - " + pmodel.OfficeHour_EndFriday2;
                                        }
                                        if (SecondaryOfficeHours.Count > 5)
                                        {
                                            if (SecondaryOfficeHours.ElementAt(5).StartTime != null)
                                                pmodel.OfficeHour_StartSaturday2 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(5).StartTime);
                                            if (SecondaryOfficeHours.ElementAt(5).EndTime != null)

                                                pmodel.OfficeHour_EndSaturday2 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(5).EndTime);

                                            pmodel.OfficeHour_Saturday2 = pmodel.OfficeHour_StartSaturday2 + " - " + pmodel.OfficeHour_EndSaturday2;
                                        }
                                        if (SecondaryOfficeHours.Count > 6)
                                        {
                                            if (SecondaryOfficeHours.ElementAt(6).StartTime != null)
                                                pmodel.OfficeHour_StartSunday2 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(6).StartTime);
                                            if (SecondaryOfficeHours.ElementAt(6).EndTime != null)
                                                pmodel.OfficeHour_EndSunday2 = ConvertTimeFormat(SecondaryOfficeHours.ElementAt(6).EndTime);

                                            pmodel.OfficeHour_Sunday2 = pmodel.OfficeHour_StartSunday2 + " - " + pmodel.OfficeHour_EndSunday2;
                                        }
                                    }

                                }

                                #endregion

                                #region Supervising Provider 2

                                var supervisingProvider2 = SecondaryPracticeProviders.
                                Where(s => s.Practice == AHC.CD.Entities.MasterData.Enums.PracticeType.Supervisor.ToString() &&
                                    s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();

                                var supervisorCount2 = supervisingProvider2.Count;

                                if (supervisorCount2 > 0)
                                {
                                    pmodel.CoveringColleague_FirstName2 = supervisingProvider2.ElementAt(supervisorCount2 - 1).FirstName;
                                    pmodel.CoveringColleague_MiddleName2 = supervisingProvider2.ElementAt(supervisorCount2 - 1).MiddleName;
                                    pmodel.CoveringColleague_LastName2 = supervisingProvider2.ElementAt(supervisorCount2 - 1).LastName;

                                    if (pmodel.CoveringColleague_MiddleName2 != null)
                                        pmodel.CoveringColleague_FullName2 = pmodel.CoveringColleague_FirstName2 + " " + pmodel.CoveringColleague_MiddleName2 + " " + pmodel.CoveringColleague_LastName2;
                                    else
                                        pmodel.CoveringColleague_FullName2 = pmodel.CoveringColleague_FirstName2 + " " + pmodel.CoveringColleague_LastName2;


                                    if (supervisingProvider2.ElementAt(supervisorCount2 - 1).Telephone != null)
                                    {
                                        pmodel.CoveringColleague_PhoneFirstThreeDigit2 = supervisingProvider2.ElementAt(supervisorCount2 - 1).Telephone.Substring(0, supervisingProvider2.ElementAt(supervisorCount2 - 1).Telephone.Length - 7);
                                        pmodel.CoveringColleague_PhoneSecondThreeDigit2 = supervisingProvider2.ElementAt(supervisorCount2 - 1).Telephone.Substring(3, 3);
                                        pmodel.CoveringColleague_PhoneLastFourDigit2 = supervisingProvider2.ElementAt(supervisorCount2 - 1).Telephone.Substring(6);

                                        pmodel.CoveringColleague_PhoneNumber2 = pmodel.CoveringColleague_PhoneFirstThreeDigit2 + "-" + pmodel.CoveringColleague_PhoneSecondThreeDigit2 + "-" + pmodel.CoveringColleague_PhoneLastFourDigit2;

                                    }

                                    var specialities = SecondaryPracticeProvidersSpeciality.Where(s => s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString()).ToList();

                                    if (specialities.Count > 0)
                                    {
                                        if (specialities.ElementAt(specialities.Count - 1) != null)
                                            pmodel.CoveringColleague_Specialty2 = specialities.ElementAt(specialities.Count - 1).Name;
                                    }
                                }

                                #endregion

                                #region Covering Colleagues/Partners 2

                                var patners2 = SecondaryPracticeProviders.
                                Where(s => s.Practice == AHC.CD.Entities.MasterData.Enums.PracticeType.CoveringColleague.ToString() &&
                                    s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();

                                var patnersCount2 = patners2.Count;

                                if (patnersCount2 > 0)
                                {
                                    pmodel.Patners_FirstName2 = patners2.ElementAt(patnersCount2 - 1).FirstName;
                                    pmodel.Patners_MiddleName2 = patners2.ElementAt(patnersCount2 - 1).MiddleName;
                                    pmodel.Patners_LastName2 = patners2.ElementAt(patnersCount2 - 1).LastName;

                                    if (pmodel.Patners_MiddleName2 != null)
                                        pmodel.Patners_FullName2 = pmodel.Patners_FirstName2 + " " + pmodel.Patners_MiddleName2 + " " + pmodel.Patners_LastName2;
                                    else
                                        pmodel.Patners_FullName2 = pmodel.Patners_FirstName2 + " " + pmodel.Patners_LastName2;

                                }

                                #endregion

                                #region Credentialing Contact Information 2

                                if (SecondaryCredentialingContactInformation != null)
                                {

                                    if (SecondaryCredentialingContactInformation.MiddleName != null)
                                    {
                                        pmodel.PrimaryCredContact_FullName2 = SecondaryCredentialingContactInformation.FirstName + " " + SecondaryCredentialingContactInformation.MiddleName + " " + SecondaryCredentialingContactInformation.LastName;
                                    }
                                    else
                                    {
                                        pmodel.PrimaryCredContact_FullName2 = SecondaryCredentialingContactInformation.FirstName + " " + SecondaryCredentialingContactInformation.LastName;
                                    }
                                    pmodel.PrimaryCredContact_FirstName2 = SecondaryCredentialingContactInformation.FirstName;
                                    pmodel.PrimaryCredContact_MI2 = SecondaryCredentialingContactInformation.MiddleName;
                                    pmodel.PrimaryCredContact_LastName2 = SecondaryCredentialingContactInformation.LastName;

                                    pmodel.PrimaryCredContact_Street2 = SecondaryCredentialingContactInformation.Street;
                                    pmodel.PrimaryCredContact_Suite2 = SecondaryCredentialingContactInformation.Building;
                                    pmodel.PrimaryCredContact_City2 = SecondaryCredentialingContactInformation.City;
                                    pmodel.PrimaryCredContact_State2 = SecondaryCredentialingContactInformation.State;
                                    pmodel.PrimaryCredContact_ZipCode2 = SecondaryCredentialingContactInformation.ZipCode;
                                    pmodel.PrimaryCredContact_Phone2 = SecondaryCredentialingContactInformation.Telephone;
                                    pmodel.PrimaryCredContact_Fax2 = SecondaryCredentialingContactInformation.FaxNumber;
                                    pmodel.PrimaryCredContact_Email2 = SecondaryCredentialingContactInformation.EmailAddress;
                                    pmodel.PrimaryCredContact_MobileNumber2 = SecondaryCredentialingContactInformation.MobileNumber;
                                    pmodel.PrimaryCredContact2_Address1 = pmodel.PrimaryCredContact_Street2 + ", " + pmodel.PrimaryCredContact_Suite2;
                                }
                                #endregion

                                #endregion
                            }

                        }
                    }
                    #endregion

                }
                #endregion

                #region Identification & Licenses

                #region State License Information details

                if (StateLicensesInfo.Count > 0)
                {
                    var stateLicense = StateLicensesInfo.Where(s => s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();
                    var stateLicenseCount = stateLicense.Count;

                    if (stateLicenseCount > 0 && stateLicense.ElementAt(stateLicenseCount - 1) != null)
                    {
                        pmodel.StateLicense_Type1 = stateLicense.ElementAt(stateLicenseCount - 1).ProviderType_Title;

                        pmodel.StateLicense_Number1 = stateLicense.ElementAt(stateLicenseCount - 1).LicenseNumber;
                        pmodel.StateLicense_ExpirationDate1 = ConvertToDateString(stateLicense.ElementAt(stateLicenseCount - 1).ExpiryDate);
                        pmodel.StateLicense_CurrentIssueDate1 = ConvertToDateString(stateLicense.ElementAt(stateLicenseCount - 1).CurrentIssueDate);
                        pmodel.StateLicense_OriginalIssueDate1 = ConvertToDateString(stateLicense.ElementAt(stateLicenseCount - 1).IssueDate);
                        pmodel.StateLicense_IssuingState1 = stateLicense.ElementAt(stateLicenseCount - 1).IssueState;
                        if (stateLicense.ElementAt(stateLicenseCount - 1).StateLicenseStatus_Title != null)
                            pmodel.StateLicense_Status1 = stateLicense.ElementAt(stateLicenseCount - 1).StateLicenseStatus_Title.ToString();


                        if (pmodel.StateLicense_ExpirationDate1 != null)
                        {
                            pmodel.StateLicense_ExpirationDate1_MM = pmodel.StateLicense_ExpirationDate1.Split('-')[0];
                            pmodel.StateLicense_ExpirationDate1_dd = pmodel.StateLicense_ExpirationDate1.Split('-')[1];
                            pmodel.StateLicense_ExpirationDate1_yyyy = pmodel.StateLicense_ExpirationDate1.Split('-')[2].Substring(2);
                        }

                        if (pmodel.StateLicense_CurrentIssueDate1 != null)
                        {
                            pmodel.StateLicense_CurrentIssueDate1_MM = pmodel.StateLicense_CurrentIssueDate1.Split('-')[0];
                            pmodel.StateLicense_CurrentIssueDate1_dd = pmodel.StateLicense_CurrentIssueDate1.Split('-')[1];
                            pmodel.StateLicense_CurrentIssueDate1_yyyy = pmodel.StateLicense_CurrentIssueDate1.Split('-')[2].Substring(2);
                        }

                        if (pmodel.StateLicense_OriginalIssueDate1 != null)
                        {
                            pmodel.StateLicense_OriginalIssueDate1_MM = pmodel.StateLicense_OriginalIssueDate1.Split('-')[0];
                            pmodel.StateLicense_OriginalIssueDate1_dd = pmodel.StateLicense_OriginalIssueDate1.Split('-')[1];
                            pmodel.StateLicense_OriginalIssueDate1_yyyy = pmodel.StateLicense_OriginalIssueDate1.Split('-')[2].Substring(2);
                        }
                    }

                    if (stateLicenseCount > 1)
                    {
                        pmodel.StateLicense_Type2 = stateLicense.ElementAt(stateLicenseCount - 2).ProviderType_Title;
                        pmodel.StateLicense_Number2 = stateLicense.ElementAt(stateLicenseCount - 2).LicenseNumber;
                        pmodel.StateLicense_ExpirationDate2 = ConvertToDateString(stateLicense.ElementAt(stateLicenseCount - 2).ExpiryDate);
                        pmodel.StateLicense_CurrentIssueDate2 = ConvertToDateString(stateLicense.ElementAt(stateLicenseCount - 2).CurrentIssueDate);
                        pmodel.StateLicense_OriginalIssueDate2 = ConvertToDateString(stateLicense.ElementAt(stateLicenseCount - 2).IssueDate);
                        pmodel.StateLicense_IssuingState2 = stateLicense.ElementAt(stateLicenseCount - 2).IssueState;
                        if (stateLicense.ElementAt(stateLicenseCount - 2).StateLicenseStatus_Title != null)
                            pmodel.StateLicense_Status2 = stateLicense.ElementAt(stateLicenseCount - 2).StateLicenseStatus_Title.ToString();
                    }

                    if (stateLicenseCount > 2)
                    {
                        pmodel.StateLicense_Type3 = stateLicense.ElementAt(stateLicenseCount - 3).ProviderType_Title;
                        pmodel.StateLicense_Number3 = stateLicense.ElementAt(stateLicenseCount - 3).LicenseNumber;
                        pmodel.StateLicense_ExpirationDate3 = ConvertToDateString(stateLicense.ElementAt(stateLicenseCount - 3).ExpiryDate);
                        pmodel.StateLicense_CurrentIssueDate3 = ConvertToDateString(stateLicense.ElementAt(stateLicenseCount - 3).CurrentIssueDate);
                        pmodel.StateLicense_OriginalIssueDate3 = ConvertToDateString(stateLicense.ElementAt(stateLicenseCount - 3).IssueDate);
                        pmodel.StateLicense_IssuingState3 = stateLicense.ElementAt(stateLicenseCount - 3).IssueState;
                        if (stateLicense.ElementAt(stateLicenseCount - 3).StateLicenseStatus_Title != null)
                            pmodel.StateLicense_Status3 = stateLicense.ElementAt(stateLicenseCount - 3).StateLicenseStatus_Title.ToString();
                    }
                    if (stateLicenseCount > 3)
                    {
                        pmodel.StateLicense_Type4 = stateLicense.ElementAt(stateLicenseCount - 4).ProviderType_Title;
                        pmodel.StateLicense_Number4 = stateLicense.ElementAt(stateLicenseCount - 4).LicenseNumber;
                        pmodel.StateLicense_ExpirationDate4 = ConvertToDateString(stateLicense.ElementAt(stateLicenseCount - 4).ExpiryDate);
                        pmodel.StateLicense_CurrentIssueDate4 = ConvertToDateString(stateLicense.ElementAt(stateLicenseCount - 4).CurrentIssueDate);
                        pmodel.StateLicense_OriginalIssueDate4 = ConvertToDateString(stateLicense.ElementAt(stateLicenseCount - 4).IssueDate);
                        pmodel.StateLicense_IssuingState4 = stateLicense.ElementAt(stateLicenseCount - 4).IssueState;
                        if (stateLicense.ElementAt(stateLicenseCount - 4).StateLicenseStatus_Title != null)
                            pmodel.StateLicense_Status4 = stateLicense.ElementAt(stateLicenseCount - 4).StateLicenseStatus_Title.ToString();

                    }
                    #region  State License Region
                    for (var l = 0; l < stateLicenseCount; l++)
                    {
                        if (stateLicense[l].ProviderType_Title != null)
                        {

                            if (stateLicense[l].ProviderType_Title == "RN" || stateLicense[l].ProviderType_Title == "LPN" || stateLicense[l].ProviderType_Title == "NP" || stateLicense[l].ProviderType_Title == "ARNP" || stateLicense[l].ProviderType_Title == "Registered Nurse" || stateLicense[l].ProviderType_Title == "Licensed Practical Nurse" || stateLicense[l].ProviderType_Title == "Nurse Practitioner")
                            {
                                pmodel.RN_LPN_NP_ProviderType_Title = stateLicense[l].ProviderType_Title;
                                pmodel.RN_LPN_NP_StateLicense_Number = stateLicense[l].LicenseNumber;
                                pmodel.RN_LPN_NP_StateLicense_OriginalIssueDate = ConvertToDateString(stateLicense[l].IssueDate);
                                pmodel.RN_LPN_NP_StateLicense_ExpirationDate = ConvertToDateString(stateLicense[l].ExpiryDate);
                                pmodel.RN_LPN_NP_StateLicense_CurrentIssueDate = ConvertToDateString(stateLicense[l].CurrentIssueDate);
                            }
                            if (stateLicense[l].ProviderType_Title == "PA" || stateLicense[l].ProviderType_Title.ToUpper() == "PHYSICIAN ASSISTANT" || stateLicense[l].ProviderType_Title == "Physician Assistant ")
                            {
                                pmodel.PA_ProviderType_Title = stateLicense[l].ProviderType_Title;
                                pmodel.PAStateLicense_Number = stateLicense[l].LicenseNumber;
                                pmodel.PAStateLicense_ExpirationDate = ConvertToDateString(stateLicense[l].ExpiryDate);
                                pmodel.PAStateLicense_CurrentIssueDate = ConvertToDateString(stateLicense[l].CurrentIssueDate);
                                pmodel.PAStateLicense_OriginalIssueDate = ConvertToDateString(stateLicense[l].IssueDate);
                                pmodel.PAStateLicense_IssuingState = stateLicense[l].IssueState;
                            }
                            else if (stateLicense[l].ProviderType_Title == "CNM" || stateLicense[l].ProviderType_Title.ToUpper() == "CERTIFIED NURSE MIDWIFE" || stateLicense[l].ProviderType_Title == "Certified Nurse Midwife")
                            {
                                pmodel.CNM_ProviderType_Title = stateLicense[l].ProviderType_Title;
                                pmodel.CNMStateLicense_Number = stateLicense[l].LicenseNumber;
                                pmodel.CNMStateLicense_ExpirationDate = ConvertToDateString(stateLicense[l].ExpiryDate);
                                pmodel.CNMStateLicense_CurrentIssueDate = ConvertToDateString(stateLicense[l].CurrentIssueDate);
                                pmodel.CNMStateLicense_OriginalIssueDate = ConvertToDateString(stateLicense[l].IssueDate);
                                pmodel.CNMStateLicense_IssuingState = stateLicense[l].IssueState;
                            }
                            else if (stateLicense[l].ProviderType_Title == "CP" || stateLicense[l].ProviderType_Title.ToUpper() == "CLINICAL PSYCHOLOGIST" || stateLicense[l].ProviderType_Title == "Clinical Psychologist")
                            {
                                pmodel.CP_ProviderType_Title = stateLicense[l].ProviderType_Title;
                                pmodel.CPStateLicense_Number = stateLicense[l].LicenseNumber;
                                pmodel.CPStateLicense_ExpirationDate = ConvertToDateString(stateLicense[l].ExpiryDate);
                                pmodel.CPStateLicense_CurrentIssueDate = ConvertToDateString(stateLicense[l].CurrentIssueDate);
                                pmodel.CPStateLicense_OriginalIssueDate = ConvertToDateString(stateLicense[l].IssueDate);
                                pmodel.CPStateLicense_IssuingState = stateLicense[l].IssueState;
                            }
                            else if (stateLicense[l].ProviderType_Title == "CSW" || stateLicense[l].ProviderType_Title.ToUpper() == "CLINICAL SOCIAL WORKERS" || stateLicense[l].ProviderType_Title == "Clinical Social Worker")
                            {
                                pmodel.CSW_ProviderType_Title = stateLicense[l].ProviderType_Title;
                                pmodel.CSWStateLicense_Number = stateLicense[l].LicenseNumber;
                                pmodel.CSWStateLicense_ExpirationDate = ConvertToDateString(stateLicense[l].ExpiryDate);
                                pmodel.CSWStateLicense_CurrentIssueDate = ConvertToDateString(stateLicense[l].CurrentIssueDate);
                                pmodel.CSWStateLicense_OriginalIssueDate = ConvertToDateString(stateLicense[l].IssueDate);
                                pmodel.CSWStateLicense_IssuingState = stateLicense[l].IssueState;
                            }
                            else if (stateLicense[l].ProviderType_Title == "TCMHC" || stateLicense[l].ProviderType_Title.ToUpper() == "TRICARE CERTIFIED MENTAL HEALTH COUNSELOR" || stateLicense[l].ProviderType_Title == "TRICARE Certified Mental Health Counselor")
                            {
                                pmodel.TCMHC_ProviderType_Title = stateLicense[l].ProviderType_Title;
                                pmodel.TCMHCStateLicense_Number = stateLicense[l].LicenseNumber;
                                pmodel.TCMHCStateLicense_ExpirationDate = ConvertToDateString(stateLicense[l].ExpiryDate);
                                pmodel.TCMHCStateLicense_CurrentIssueDate = ConvertToDateString(stateLicense[l].CurrentIssueDate);
                                pmodel.TCMHCStateLicense_OriginalIssueDate = ConvertToDateString(stateLicense[l].IssueDate);
                                pmodel.TCMHCStateLicense_IssuingState = stateLicense[l].IssueState;
                            }
                            else if (stateLicense[l].ProviderType_Title.ToUpper() == "REGISTERED DIETICIAN" || stateLicense[l].ProviderType_Title.ToUpper() == "DIETICIAN")
                            {
                                pmodel.Dietician_ProviderType_Title = stateLicense[l].ProviderType_Title;
                                pmodel.DieticianStateLicense_Number = stateLicense[l].LicenseNumber;
                                pmodel.DieticianStateLicense_ExpirationDate = ConvertToDateString(stateLicense[l].ExpiryDate);
                                pmodel.DieticianStateLicense_CurrentIssueDate = ConvertToDateString(stateLicense[l].CurrentIssueDate);
                                pmodel.DieticianStateLicense_OriginalIssueDate = ConvertToDateString(stateLicense[l].IssueDate);
                                pmodel.DieticianStateLicense_IssuingState = stateLicense[l].IssueState;

                            }
                            else if (stateLicense[l].ProviderType_Title.ToUpper() == "NUTRITIONIST")
                            {
                                pmodel.Nutritionist_ProviderType_Title = stateLicense[l].ProviderType_Title;
                                pmodel.NutritionistStateLicense_Number = stateLicense[l].LicenseNumber;
                                pmodel.NutritionistStateLicense_ExpirationDate = ConvertToDateString(stateLicense[l].ExpiryDate);
                                pmodel.NutritionistStateLicense_CurrentIssueDate = ConvertToDateString(stateLicense[l].CurrentIssueDate);
                                pmodel.NutritionistStateLicense_OriginalIssueDate = ConvertToDateString(stateLicense[l].IssueDate);
                                pmodel.NutritionistStateLicense_IssuingState = stateLicense[l].IssueState;

                            }
                            else if (stateLicense[l].ProviderType_Title == "CRNA" || stateLicense[l].ProviderType_Title.ToUpper() == "CERTIFIED REGISTERED NURSE ANESTHETIST" || stateLicense[l].ProviderType_Title == "Certified Registered Nurse Anesthetist")
                            {
                                pmodel.CRNA_ProviderType_Title = stateLicense[l].ProviderType_Title;
                                pmodel.CRNAStateLicense_Number = stateLicense[l].LicenseNumber;
                                pmodel.CRNAStateLicense_State = stateLicense[l].IssueState;
                                pmodel.CRNAStateLicense_ExpirationDate = ConvertToDateString(stateLicense[l].ExpiryDate);
                                pmodel.CRNAStateLicense_CurrentIssueDate = ConvertToDateString(stateLicense[l].CurrentIssueDate);
                                pmodel.CRNAStateLicense_OriginalIssueDate = ConvertToDateString(stateLicense[l].IssueDate);
                                pmodel.CRNAStateLicense_IssuingState = stateLicense[l].IssueState;
                            }
                            else if (stateLicense[l].ProviderType_Title.ToUpper() == "ANESTHESIOLOGIST ASSISTANT" || stateLicense[l].ProviderType_Title == "AA" || stateLicense[l].ProviderType_Title == "Anesthesiologist Assistant")
                            {
                                pmodel.AA_ProviderType_Title = stateLicense[l].ProviderType_Title;
                                pmodel.AAStateLicense_Number = stateLicense[l].LicenseNumber;
                                pmodel.AAStateLicense_ExpirationDate = ConvertToDateString(stateLicense[l].ExpiryDate);
                                pmodel.AAStateLicense_CurrentIssueDate = ConvertToDateString(stateLicense[l].CurrentIssueDate);
                                pmodel.AAStateLicense_OriginalIssueDate = ConvertToDateString(stateLicense[l].IssueDate);
                                pmodel.AAStateLicense_IssuingState = stateLicense[l].IssueState;
                            }
                            else if (stateLicense[l].ProviderType_Title == "SMHC" || stateLicense[l].ProviderType_Title.ToUpper() == "SUPERVISED MENTAL HEALTH COUNSELOR" || stateLicense[l].ProviderType_Title == "Supervised Mental Health Counselor")
                            {
                                pmodel.SMHC_ProviderType_Title = stateLicense[l].ProviderType_Title;
                                pmodel.SMHCStateLicense_Number = stateLicense[l].LicenseNumber;
                                pmodel.SMHCStateLicense_ExpirationDate = ConvertToDateString(stateLicense[l].ExpiryDate);
                                pmodel.SMHCStateLicense_CurrentIssueDate = ConvertToDateString(stateLicense[l].CurrentIssueDate);
                                pmodel.SMHCStateLicense_OriginalIssueDate = ConvertToDateString(stateLicense[l].IssueDate);
                                pmodel.SMHCStateLicense_IssuingState = stateLicense[l].IssueState;
                            }

                            else if (stateLicense[l].ProviderType_Title.ToUpper() == "CERTIFIED PSYCHIATRIC NURSE SPECIALIST" || stateLicense[l].ProviderType_Title == "Certified Psychiatric Nurse Specialist")
                            {
                                pmodel.CPNS_ProviderType_Title = stateLicense[l].ProviderType_Title;
                                pmodel.CPNS_StateLicense_Number = stateLicense[l].LicenseNumber;
                                pmodel.CPNS_StateLicense_ExpirationDate = ConvertToDateString(stateLicense[l].ExpiryDate);
                                pmodel.CPNS_StateLicense_CurrentIssueDate = ConvertToDateString(stateLicense[l].CurrentIssueDate);
                                pmodel.CPNS_StateLicense_OriginalIssueDate = ConvertToDateString(stateLicense[l].IssueDate);
                                pmodel.CPNS_StateLicense_IssuingState = stateLicense[l].IssueState;
                            }

                            else if (stateLicense[l].ProviderType_Title == "Physical Therapist" || stateLicense[l].ProviderType_Title.ToUpper() == "PHYSICAL THERAPIST" || stateLicense[l].ProviderType_Title.ToUpper() == "SPEECH PATHOLOGIST" || stateLicense[l].ProviderType_Title.ToUpper() == "OCCUPATIONAL THERAPIST" || stateLicense[l].ProviderType_Title.ToUpper() == "AUDIOLOGIST" || stateLicense[l].ProviderType_Title == "Audiologist" || stateLicense[l].ProviderType_Title == "Hippotherapy Physical Therapist" || stateLicense[l].ProviderType_Title == "Hippotherapy Physical Occupational")
                            {
                                pmodel.Physical_ProviderType_Title = stateLicense[l].ProviderType_Title;
                                pmodel.Physical_StateLicense_Number = stateLicense[l].LicenseNumber;
                                pmodel.Physical_StateLicense_ExpirationDate = ConvertToDateString(stateLicense[l].ExpiryDate);
                                pmodel.Physical_StateLicense_CurrentIssueDate = ConvertToDateString(stateLicense[l].CurrentIssueDate);
                                pmodel.Physical_StateLicense_OriginalIssueDate = ConvertToDateString(stateLicense[l].IssueDate);
                                pmodel.Physical_StateLicense_IssuingState = stateLicense[l].IssueState;
                            }
                        }

                    }
                    #endregion
                }

                #endregion

                #region Federal DEA Information

                if (FederalDEAInformations.Count > 0)
                {
                    var federalDEAInformations = FederalDEAInformations.Where(s => s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();
                    var federalDEAInformationCount = federalDEAInformations.Count;
                    if (federalDEAInformationCount > 0 && federalDEAInformations.ElementAt(federalDEAInformationCount - 1) != null)
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

                if (MedicareInformations.Count > 0)
                {
                    var medicareInfo = MedicareInformations.Where(s => s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();
                    var medicareInfoCount = medicareInfo.Count;
                    if (medicareInfoCount > 0 && medicareInfo.ElementAt(medicareInfoCount - 1) != null)
                    {
                        pmodel.Medicare_Number1 = medicareInfo.ElementAt(medicareInfoCount - 1).LicenseNumber;

                    }
                }

                if (MedicaidInformations.Count > 0)
                {
                    var medicaidInfo = MedicaidInformations.Where(s => s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();
                    var medicidInfoCount = medicaidInfo.Count;
                    if (medicidInfoCount > 0 && medicaidInfo.ElementAt(medicidInfoCount - 1) != null)
                    {

                        pmodel.MedicaidNumber1 = medicaidInfo.ElementAt(medicidInfoCount - 1).LicenseNumber;
                        pmodel.Medicaid_State1 = medicaidInfo.ElementAt(medicidInfoCount - 1).State;

                    }
                }

                #endregion

                #region CDS Information

                if (CDSCInformationData.Count > 0)
                {
                    var CDSCInformations = CDSCInformationData.Where(s => s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();
                    var CDSCInformationsCount = CDSCInformations.Count;
                    if (CDSCInformationsCount > 0 && CDSCInformations.ElementAt(CDSCInformationsCount - 1) != null)
                    {


                        pmodel.CDS_Number1 = CDSCInformations.ElementAt(CDSCInformationsCount - 1).CertNumber;
                        pmodel.CDS_RegistrationState1 = CDSCInformations.ElementAt(CDSCInformationsCount - 1).State;
                        pmodel.CDS_IssueDate1 = ConvertToDateString(CDSCInformations.ElementAt(CDSCInformationsCount - 1).IssueDate);
                        pmodel.CDS_ExpirationDate1 = ConvertToDateString(CDSCInformations.ElementAt(CDSCInformationsCount - 1).ExpiryDate);
                        pmodel.CDS_Certificate1 = CDSCInformations.ElementAt(CDSCInformationsCount - 1).CDSCCerificatePath;


                    }
                }

                #endregion

                #region Other Identification Numbers

                if (OtherIdentificationNumberData != null)
                {
                    pmodel.NPI_Number = OtherIdentificationNumberData.NPINumber;
                    pmodel.NPI_Username = OtherIdentificationNumberData.NPIUserName;
                    pmodel.NPI_Password = OtherIdentificationNumberData.NPIPassword;
                    pmodel.CAQH_Number = OtherIdentificationNumberData.CAQHNumber;
                    pmodel.CAQH_Username = OtherIdentificationNumberData.CAQHUserName;
                    pmodel.CAQH_Password = OtherIdentificationNumberData.CAQHPassword;
                    pmodel.UPIN_Number = OtherIdentificationNumberData.UPINNumber;
                    pmodel.USMLE_Number = OtherIdentificationNumberData.USMLENumber;
                }


                #endregion

                #endregion

                #region Professional Liability

                if (ProfessionalLiabilityInfoes.Count > 0)
                {
                    var professionalLiabilities = ProfessionalLiabilityInfoes.Where(s => s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();
                    var professionalLiabilitiesCount = professionalLiabilities.Count;
                    if (professionalLiabilitiesCount > 0 && professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1) != null)
                    {



                        pmodel.ProfLiability_CarrierName1 = professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).InsuranceCarrier_Name;

                        pmodel.ProfLiability_IsSelfInsured1 = professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).SelfInsured;
                        pmodel.ProfLiability_PolicyNumber1 = professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).PolicyNumber;
                        pmodel.ProfLiability_OriginalEffectiveDate1 = ConvertToDateString(professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).OriginalEffectiveDate);
                        pmodel.ProfLiability_EffectiveDate1 = ConvertToDateString(professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).EffectiveDate);
                        pmodel.ProfLiability_ExpiryDate1 = ConvertToDateString(professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).ExpirationDate);
                        pmodel.ProfLiability_InsuranceDocument1 = professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).InsuranceCertificatePath;
                        //pmodel.ProfLiability_Number1 = professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).ProfessionalLiabilityInfoID.ToString();
                        //pmodel.ProfLiability_Phone1 = professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).PhoneNumber;

                        if (professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).Phone != null)
                        {
                            pmodel.ProfLiability_PhoneFirstThreeDigit1 = professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).Phone.Substring(0, professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).Phone.Length - 7);
                            pmodel.ProfLiability_PhoneSecondThreeDigit1 = professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).Phone.Substring(3, 3);
                            pmodel.ProfLiability_PhoneLastFourDigit1 = professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).Phone.Substring(6);

                            pmodel.ProfLiability_Phone1 = pmodel.ProfLiability_PhoneFirstThreeDigit1 + "-" + pmodel.ProfLiability_PhoneSecondThreeDigit1 + "-" + pmodel.ProfLiability_PhoneLastFourDigit1;
                            pmodel.ProfLiability1_Address3 = "Phone : " + pmodel.ProfLiability_Phone1;
                        }


                        pmodel.ProfLiability_AggregateCoverageAmount1 = professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).AmountOfCoverageAggregate.ToString();
                        pmodel.ProfLiability_CoverageAmountPerOccurrence1 = professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).AmountOfCoveragePerOccurance.ToString();
                        if (pmodel.ProfLiability_CoverageAmountPerOccurrence1 != "" && pmodel.ProfLiability_CoverageAmountPerOccurrence1 != null)
                        {
                            pmodel.TriCareAmountsOfCoverage = pmodel.ProfLiability_CoverageAmountPerOccurrence1 + " - " + pmodel.ProfLiability_AggregateCoverageAmount1;
                        }
                        else
                        {
                            pmodel.TriCareAmountsOfCoverage = pmodel.ProfLiability_AggregateCoverageAmount1;
                        }
                        if (professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1) != null)
                        {
                            pmodel.ProfLiability_Country1 = professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).Country;
                            pmodel.ProfLiability_State1 = professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).State;
                            pmodel.ProfLiability_County1 = professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).County;
                            pmodel.ProfLiability_City1 = professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).City;
                            pmodel.ProfLiability_Street1 = professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).Street;
                            pmodel.ProfLiability_ZipCode1 = professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).ZipCode;
                            pmodel.ProfLiability_Suite1 = professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).LocationName;
                            pmodel.ProfLiability1_Address1 = pmodel.ProfLiability_Street1 + " " + pmodel.ProfLiability_Suite1;
                            pmodel.ProfLiability1_Address2 = pmodel.ProfLiability_City1 + ", " + pmodel.ProfLiability_State1 + " " + pmodel.ProfLiability_ZipCode1;
                        }

                    }

                }

                #endregion

                #region Professional Affiliation
                if (ProfessionalAffiliationInfos.Count > 0)
                {
                    var professionalAffiliations = ProfessionalAffiliationInfos.Where(s => s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();
                    var professionalAffiliationsCount = professionalAffiliations.Count;

                    if (professionalAffiliationsCount > 0 && professionalAffiliations.ElementAt(professionalAffiliationsCount - professionalAffiliationsCount) != null)
                    {
                        pmodel.ProviderOrganizationName1 = professionalAffiliations.ElementAt(professionalAffiliationsCount - professionalAffiliationsCount).OrganizationName;
                        pmodel.ProfessionalAffiliationOfficePosition1 = professionalAffiliations.ElementAt(professionalAffiliationsCount - professionalAffiliationsCount).PositionOfficeHeld;
                        pmodel.ProfessionalAffiliationMember1 = professionalAffiliations.ElementAt(professionalAffiliationsCount - professionalAffiliationsCount).Member;
                        pmodel.ProfessionalAffiliationStartDate1 = ConvertToDateString(professionalAffiliations.ElementAt(professionalAffiliationsCount - professionalAffiliationsCount).StartDate);
                        pmodel.ProfessionalAffiliationEndDate1 = ConvertToDateString(professionalAffiliations.ElementAt(professionalAffiliationsCount - professionalAffiliationsCount).EndDate);

                    }
                    if (professionalAffiliationsCount > 1 && professionalAffiliations.ElementAt(professionalAffiliationsCount - (professionalAffiliationsCount - 1)) != null)
                    {
                        pmodel.ProviderOrganizationName2 = professionalAffiliations.ElementAt(professionalAffiliationsCount - (professionalAffiliationsCount - 1)).OrganizationName;
                        pmodel.ProfessionalAffiliationOfficePosition2 = professionalAffiliations.ElementAt(professionalAffiliationsCount - (professionalAffiliationsCount - 1)).PositionOfficeHeld;
                        pmodel.ProfessionalAffiliationMember2 = professionalAffiliations.ElementAt(professionalAffiliationsCount - (professionalAffiliationsCount - 1)).Member;
                        pmodel.ProfessionalAffiliationStartDate2 = ConvertToDateString(professionalAffiliations.ElementAt(professionalAffiliationsCount - (professionalAffiliationsCount - 1)).StartDate);
                        pmodel.ProfessionalAffiliationEndDate2 = ConvertToDateString(professionalAffiliations.ElementAt(professionalAffiliationsCount - (professionalAffiliationsCount - 1)).EndDate);

                    }
                    if (professionalAffiliationsCount > 2 && professionalAffiliations.ElementAt(professionalAffiliationsCount - (professionalAffiliationsCount - 2)) != null)
                    {
                        pmodel.ProviderOrganizationName3 = professionalAffiliations.ElementAt(professionalAffiliationsCount - (professionalAffiliationsCount - 2)).OrganizationName;
                        pmodel.ProfessionalAffiliationOfficePosition3 = professionalAffiliations.ElementAt(professionalAffiliationsCount - (professionalAffiliationsCount - 2)).PositionOfficeHeld;
                        pmodel.ProfessionalAffiliationMember3 = professionalAffiliations.ElementAt(professionalAffiliationsCount - (professionalAffiliationsCount - 2)).Member;
                        pmodel.ProfessionalAffiliationStartDate3 = ConvertToDateString(professionalAffiliations.ElementAt(professionalAffiliationsCount - (professionalAffiliationsCount - 2)).StartDate);
                        pmodel.ProfessionalAffiliationEndDate3 = ConvertToDateString(professionalAffiliations.ElementAt(professionalAffiliationsCount - (professionalAffiliationsCount - 2)).EndDate);

                    }
                    if (professionalAffiliationsCount > 3 && professionalAffiliations.ElementAt(professionalAffiliationsCount - (professionalAffiliationsCount - 3)) != null)
                    {
                        pmodel.ProviderOrganizationName4 = professionalAffiliations.ElementAt(professionalAffiliationsCount - (professionalAffiliationsCount - 3)).OrganizationName;
                        pmodel.ProfessionalAffiliationOfficePosition4 = professionalAffiliations.ElementAt(professionalAffiliationsCount - (professionalAffiliationsCount - 3)).PositionOfficeHeld;
                        pmodel.ProfessionalAffiliationMember4 = professionalAffiliations.ElementAt(professionalAffiliationsCount - (professionalAffiliationsCount - 3)).Member;
                        pmodel.ProfessionalAffiliationStartDate4 = ConvertToDateString(professionalAffiliations.ElementAt(professionalAffiliationsCount - (professionalAffiliationsCount - 3)).StartDate);
                        pmodel.ProfessionalAffiliationEndDate4 = ConvertToDateString(professionalAffiliations.ElementAt(professionalAffiliationsCount - (professionalAffiliationsCount - 3)).EndDate);

                    }
                    if (professionalAffiliationsCount > 4 && professionalAffiliations.ElementAt(professionalAffiliationsCount - (professionalAffiliationsCount - 4)) != null)
                    {
                        pmodel.ProviderOrganizationName5 = professionalAffiliations.ElementAt(professionalAffiliationsCount - (professionalAffiliationsCount - 4)).OrganizationName;
                        pmodel.ProfessionalAffiliationOfficePosition5 = professionalAffiliations.ElementAt(professionalAffiliationsCount - (professionalAffiliationsCount - 4)).PositionOfficeHeld;
                        pmodel.ProfessionalAffiliationMember5 = professionalAffiliations.ElementAt(professionalAffiliationsCount - (professionalAffiliationsCount - 4)).Member;
                        pmodel.ProfessionalAffiliationStartDate5 = ConvertToDateString(professionalAffiliations.ElementAt(professionalAffiliationsCount - (professionalAffiliationsCount - 4)).StartDate);
                        pmodel.ProfessionalAffiliationEndDate5 = ConvertToDateString(professionalAffiliations.ElementAt(professionalAffiliationsCount - (professionalAffiliationsCount - 1)).EndDate);

                    }
                }
                #endregion

                #region Work History
                if (ProfessionalWorkExperiences != null && ProfessionalWorkExperiences.Count > 0)
                {
                    var professionalworkexperienceCount = ProfessionalWorkExperiences.Count;
                    int workexperienceCount = 0;
                    foreach (var item in ProfessionalWorkExperiences)
                    {
                        if (workexperienceCount == 0)
                        {
                            pmodel.WorkExp_StartDate1 = ConvertToDateString(item.StartDate);
                            pmodel.WorkExp_EndDate1 = ConvertToDateString(item.EndDate);
                            pmodel.WorkExp_EmpName1 = item.EmployerName;
                            pmodel.WorkExp_MailingAddress1 = item.EmployerEmail;
                            pmodel.WorkExp_City1 = item.City;
                            pmodel.WorkExp_State1 = item.State;
                            pmodel.WorkExp_ZipCode1 = item.ZipCode;
                            pmodel.WorkExp_County1 = item.County;
                            pmodel.WorkExp_JobTitle1 = item.JobTitle;
                            workexperienceCount++;
                        }
                        else if (workexperienceCount == 1)
                        {
                            pmodel.WorkExp_StartDate2 = ConvertToDateString(item.StartDate);
                            pmodel.WorkExp_EndDate2 = ConvertToDateString(item.EndDate);
                            pmodel.WorkExp_EmpName2 = item.EmployerName;
                            pmodel.WorkExp_MailingAddress2 = item.EmployerEmail;
                            pmodel.WorkExp_City2 = item.City;
                            pmodel.WorkExp_State2 = item.State;
                            pmodel.WorkExp_ZipCode2 = item.ZipCode;
                            pmodel.WorkExp_County2 = item.County;
                            pmodel.WorkExp_JobTitle2 = item.JobTitle;
                            workexperienceCount++;
                        }
                        else if (workexperienceCount == 2)
                        {
                            pmodel.WorkExp_StartDate3 = ConvertToDateString(item.StartDate);
                            pmodel.WorkExp_EndDate3 = ConvertToDateString(item.EndDate);
                            pmodel.WorkExp_EmpName3 = item.EmployerName;
                            pmodel.WorkExp_MailingAddress3 = item.EmployerEmail;
                            pmodel.WorkExp_City3 = item.City;
                            pmodel.WorkExp_State3 = item.State;
                            pmodel.WorkExp_ZipCode3 = item.ZipCode;
                            pmodel.WorkExp_County3 = item.County;
                            pmodel.WorkExp_JobTitle3 = item.JobTitle;
                            workexperienceCount++;
                        }
                        else if (workexperienceCount == 3)
                        {
                            pmodel.WorkExp_StartDate4 = ConvertToDateString(item.StartDate);
                            pmodel.WorkExp_EndDate4 = ConvertToDateString(item.EndDate);
                            pmodel.WorkExp_EmpName4 = item.EmployerName;
                            pmodel.WorkExp_MailingAddress4 = item.EmployerEmail;
                            pmodel.WorkExp_City4 = item.City;
                            pmodel.WorkExp_State4 = item.State;
                            pmodel.WorkExp_ZipCode4 = item.ZipCode;
                            pmodel.WorkExp_County4 = item.County;
                            pmodel.WorkExp_JobTitle4 = item.JobTitle;
                            workexperienceCount++;
                        }
                    }
                }
                #endregion

                #region Organization Info/Contract Info

                if (ContractInfoes != null && ContractInfoes.Count > 0)
                {
                    var contractInfoes = ContractInfoes.Where(s => s.Status == AHC.CD.Entities.MasterData.Enums.ContractStatus.Accepted.ToString() && s.GroupStatus != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();
                    var contractGroupInfoes = contractInfoes.Where(s => s.Status == AHC.CD.Entities.MasterData.Enums.ContractGroupStatus.Accepted.ToString() && s.GroupStatus != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();



                    if (contractGroupInfoes.Count > 0)
                    {

                        if (contractGroupInfoes.LastOrDefault().JoiningDate != null)
                        {
                            pmodel.ProviderJoiningDate1 = ConvertToDateString(contractGroupInfoes.LastOrDefault().JoiningDate);
                        }

                        if (pmodel.ProviderJoiningDate1 != null)
                        {
                            pmodel.ProviderJoiningDate1 = ConvertToDateString(contractGroupInfoes.LastOrDefault().JoiningDate);
                            pmodel.ProviderJoiningDate1_MM = pmodel.ProviderJoiningDate1.Split('-')[0];
                            pmodel.ProviderJoiningDate1_dd = pmodel.ProviderJoiningDate1.Split('-')[1];
                            pmodel.ProviderJoiningDate1_yyyy = pmodel.ProviderJoiningDate1.Split('-')[2];
                        }
                        pmodel.ProviderGroupName1 = contractGroupInfoes.LastOrDefault().GroupName;
                        pmodel.ProviderGroupNPI = contractGroupInfoes.LastOrDefault().GroupNPI;
                        pmodel.ProviderGroupTaxID = contractGroupInfoes.LastOrDefault().GroupTaxID;
                        pmodel.ProviderIndividualTaxID = contractGroupInfoes.LastOrDefault().IndividualTaxID;
                    }

                }
                #endregion

                #region Education History

                #region Under Graduation / Professional School Details

                if (EducationDetails.Count > 0)
                {
                    var educationDetails = EducationDetails.Where(s => s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString() && s.EducationQualificationType == AHC.CD.Entities.MasterData.Enums.EducationQualificationType.UnderGraduate).ToList();
                    var educationDetailsCount = educationDetails.Count;

                    if (educationDetailsCount > 0 && educationDetails.ElementAt(educationDetailsCount - 1) != null)
                    {
                        var SchoolInformation = (from data in educationDetails
                                                 group data by new
                                                 {
                                                     data.SchoolName,
                                                     data.Email,
                                                     data.PhoneNumber,
                                                     data.Fax,
                                                     data.Building,
                                                     data.Street,
                                                     data.Country,
                                                     data.State,
                                                     data.County,
                                                     data.City,
                                                     data.ZipCode

                                                 } into EducationDetailsdata
                                                 select new
                                                 {
                                                     SchoolName = EducationDetailsdata.Key.SchoolName,
                                                     Email = EducationDetailsdata.Key.Email,
                                                     PhoneNumber = EducationDetailsdata.Key.PhoneNumber,
                                                     Fax = EducationDetailsdata.Key.Fax,
                                                     Building = EducationDetailsdata.Key.Building,
                                                     Street = EducationDetailsdata.Key.Street,
                                                     Country = EducationDetailsdata.Key.Country,
                                                     State = EducationDetailsdata.Key.State,
                                                     County = EducationDetailsdata.Key.County,
                                                     City = EducationDetailsdata.Key.City,
                                                     ZipCode = EducationDetailsdata.Key.ZipCode
                                                 }).ToList().LastOrDefault();

                        pmodel.UnderGraduation_Type1 = educationDetails.ElementAt(educationDetailsCount - 1).QualificationType;
                        pmodel.Graduation_Type1 = pmodel.UnderGraduation_Type1;

                        if (SchoolInformation != null)
                        {
                            pmodel.UnderGraduation_SchoolName1 = SchoolInformation.SchoolName;
                            pmodel.UnderGraduation_EmailId1 = SchoolInformation.Email;
                            pmodel.UnderGraduation_Fax1 = SchoolInformation.Fax;
                            pmodel.UnderGraduation_TelephoneNo1 = SchoolInformation.PhoneNumber;
                            pmodel.UnderGraduation_EmailAddress1 = SchoolInformation.Email;
                            pmodel.UnderGraduation_Building1 = SchoolInformation.Building;
                            pmodel.UnderGraduation_Street1 = SchoolInformation.Street;
                            pmodel.UnderGraduation_Country1 = SchoolInformation.Country;
                            pmodel.UnderGraduation_State1 = SchoolInformation.State;
                            pmodel.UnderGraduation_County1 = SchoolInformation.County;
                            pmodel.UnderGraduation_City1 = SchoolInformation.City;
                            pmodel.UnderGraduation_ZipCode1 = SchoolInformation.ZipCode;
                        }
                        pmodel.UnderGraduation_Degree1 = educationDetails.ElementAt(educationDetailsCount - 1).QualificationDegree;
                        pmodel.UnderGraduation_StartDate1 = ConvertToDateString(educationDetails.ElementAt(educationDetailsCount - 1).StartDate);
                        pmodel.UnderGraduation_EndDate1 = ConvertToDateString(educationDetails.ElementAt(educationDetailsCount - 1).EndDate);


                        pmodel.IsUnderGraduatedFromSchool1 = educationDetails.ElementAt(educationDetailsCount - 1).IsUSGraduate;
                        pmodel.UnderGraduation_Certificate1 = educationDetails.ElementAt(educationDetailsCount - 1).CertificatePath;
                    }
                }


                #endregion

                #region Graduation Details

                if (EducationDetails.Count > 0)
                {
                    var educationDetails = EducationDetails.Where(s => s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString() && s.EducationQualificationType == AHC.CD.Entities.MasterData.Enums.EducationQualificationType.Graduate).ToList();
                    var educationDetailsCount = educationDetails.Count;

                    if (educationDetailsCount > 0 && educationDetails.ElementAt(educationDetailsCount - 1) != null)
                    {

                        var SchoolInformation = (from data in educationDetails
                                                 group data by new
                                                 {
                                                     data.SchoolInformationID,
                                                     data.SchoolName,
                                                     data.Email,
                                                     data.PhoneNumber,
                                                     data.Fax,
                                                     data.Building,
                                                     data.Street,
                                                     data.Country,
                                                     data.State,
                                                     data.County,
                                                     data.City,
                                                     data.ZipCode


                                                 } into EducationDetailsdata
                                                 select new
                                                 {

                                                     SchoolInformationID = EducationDetailsdata.Key.SchoolInformationID,
                                                     SchoolName = EducationDetailsdata.Key.SchoolName,
                                                     Email = EducationDetailsdata.Key.Email,
                                                     PhoneNumber = EducationDetailsdata.Key.PhoneNumber,
                                                     Fax = EducationDetailsdata.Key.Fax,
                                                     Building = EducationDetailsdata.Key.Building,
                                                     Street = EducationDetailsdata.Key.Street,
                                                     Country = EducationDetailsdata.Key.Country,
                                                     State = EducationDetailsdata.Key.State,
                                                     County = EducationDetailsdata.Key.County,
                                                     City = EducationDetailsdata.Key.City,
                                                     ZipCode = EducationDetailsdata.Key.ZipCode
                                                 }).ToList().LastOrDefault();


                        pmodel.Graduation_Type1 = educationDetails.ElementAt(educationDetailsCount - 1).GraduationType;

                        if (SchoolInformation != null)
                        {

                            pmodel.Graduation_SchoolName1 = SchoolInformation.SchoolName;
                            pmodel.Provider_Highest_Degree_SchoolName = pmodel.Graduation_SchoolName1;
                            pmodel.UnderGraduation_EmailId1 = SchoolInformation.Email;
                            pmodel.Graduation_Fax1 = SchoolInformation.Fax;
                            pmodel.Graduation_TelephoneNumber1 = SchoolInformation.PhoneNumber;
                            pmodel.Graduation_EmailAddress1 = SchoolInformation.Email;
                            pmodel.Graduation_Number1 = SchoolInformation.SchoolInformationID.ToString();
                            pmodel.UnderGraduation_Building1 = SchoolInformation.Building;
                            pmodel.Graduation_Street1 = SchoolInformation.Street;
                            pmodel.Graduation_Country1 = SchoolInformation.Country;
                            pmodel.Graduation_State1 = SchoolInformation.State;
                            pmodel.Graduation_County1 = SchoolInformation.County;
                            pmodel.Graduation_City1 = SchoolInformation.City;
                            pmodel.Graduation_ZipCode1 = SchoolInformation.ZipCode;
                        }
                        pmodel.Graduation_Degree1 = educationDetails.ElementAt(educationDetailsCount - 1).QualificationDegree;
                        pmodel.Provider_Highest_Degree = pmodel.Graduation_Degree1;
                        pmodel.Graduation_StartDate1 = ConvertToDateString(educationDetails.ElementAt(educationDetailsCount - 1).StartDate);
                        pmodel.Graduation_EndDate1 = ConvertToDateString(educationDetails.ElementAt(educationDetailsCount - 1).EndDate);
                        pmodel.Provider_Highest_Degree_StartDate = pmodel.Graduation_StartDate1;
                        pmodel.Provider_Highest_Degree_EndDate = pmodel.Graduation_EndDate1;
                        pmodel.IsGraduatedFromSchool1 = educationDetails.ElementAt(educationDetailsCount - 1).IsUSGraduate;
                        pmodel.IsUSGraduate1 = educationDetails.ElementAt(educationDetailsCount - 1).IsUSGraduate;
                        pmodel.Graduation_Certificate1 = educationDetails.ElementAt(educationDetailsCount - 1).CertificatePath;

                    }
                    //if (educationDetails != null && educationDetails.Count > 0)
                    //{

                    //        foreach (var education in educationDetails)
                    //        {
                    //            if (education.SchoolName != null)
                    //            {
                    //                if (education.SchoolName == " ")
                    //                {

                    //                }
                    //            }
                    //        }
                    //  }
                }

                #endregion

                #region ECFMG Details

                if (ECFMGDetails != null)
                {
                    pmodel.ECFMG_Number = ECFMGDetails.ECFMGNumber;
                    pmodel.ECFMG_IssueDate = ConvertToDateString(ECFMGDetails.ECFMGIssueDate);
                    pmodel.ECFMG_Certificate = ECFMGDetails.ECFMGCertPath;
                }

                #endregion

                #region Residency/Internship/Fellowship details

                if (EducationDetails.Count > 0)
                {
                    var trainingDetails = EducationDetails.ToList();
                    var trainingDetailsCount = trainingDetails.Count;
                    var residencyInternshipDetails = trainingDetails.Where(s => s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();
                    var residencyInternshipDetailsCount = residencyInternshipDetails.Count;


                    if (trainingDetailsCount > 0 && trainingDetails.ElementAt(trainingDetailsCount - 1) != null)
                    {
                        var SchoolInformation = (from data in residencyInternshipDetails
                                                 group data by new
                                                 {
                                                     data.SchoolName,
                                                     data.Email,
                                                     data.PhoneNumber,
                                                     data.Fax,
                                                     data.Building,
                                                     data.Street,
                                                     data.Country,
                                                     data.State,
                                                     data.County,
                                                     data.City,
                                                     data.ZipCode

                                                 } into EducationDetailsdata
                                                 select new
                                                 {
                                                     SchoolName = EducationDetailsdata.Key.SchoolName,
                                                     Email = EducationDetailsdata.Key.Email,
                                                     PhoneNumber = EducationDetailsdata.Key.PhoneNumber,
                                                     Fax = EducationDetailsdata.Key.Fax,
                                                     Building = EducationDetailsdata.Key.Building,
                                                     Street = EducationDetailsdata.Key.Street,
                                                     Country = EducationDetailsdata.Key.Country,
                                                     State = EducationDetailsdata.Key.State,
                                                     County = EducationDetailsdata.Key.County,
                                                     City = EducationDetailsdata.Key.City,
                                                     ZipCode = EducationDetailsdata.Key.ZipCode
                                                 }).ToList().LastOrDefault();

                        //pmodel.TrainingProgram_Type1=trainingDetails.ElementAt(trainingDetailsCount - 1).SchoolInformation
                        pmodel.IsTrainingProgramCompleted1 = trainingDetails.ElementAt(trainingDetailsCount - 1).IsCompleted;
                        //pmodel.TrainingProgramCompletedExplaination1=trainingDetails.ElementAt(trainingDetailsCount - 1).InCompleteReason;
                        //pmodel.IsPrimarySecondary1=trainingDetails.ElementAt(trainingDetailsCount - 1);
                        pmodel.TrainingProgram_SchoolName1 = SchoolInformation.SchoolName;
                        pmodel.TrainingProgram_PrimaryPracticingDegree1 = trainingDetails.ElementAt(trainingDetailsCount - 1).QualificationDegree;
                        pmodel.TrainingProgram_Degree1 = trainingDetails.ElementAt(trainingDetailsCount - 1).QualificationDegree;
                        pmodel.TrainingProgram_Fax1 = SchoolInformation.Fax;
                        pmodel.TrainingProgram_TelephoneNumber1 = SchoolInformation.PhoneNumber;
                        pmodel.TrainingProgram_StartDate1 = ConvertToDateString(trainingDetails.ElementAt(trainingDetailsCount - 1).StartDate);
                        pmodel.TrainingProgram_EndDate1 = ConvertToDateString(trainingDetails.ElementAt(trainingDetailsCount - 1).EndDate);
                        pmodel.TrainingProgram_Street1 = SchoolInformation.Street;
                        pmodel.TrainingProgram_Country1 = SchoolInformation.Country;
                        pmodel.TrainingProgram_State1 = SchoolInformation.State;
                        pmodel.TrainingProgram_County1 = SchoolInformation.County;
                        pmodel.TrainingProgram_City1 = SchoolInformation.City;
                        pmodel.TrainingProgram_ZipCode1 = SchoolInformation.ZipCode;
                        pmodel.TrainingProgram_Certificate1 = trainingDetails.ElementAt(trainingDetailsCount - 1).CertificatePath;
                    }
                }

                if (EducationDetails.Count > 0)
                {
                    int residencyCount = 0;
                    int fellowshipCount = 0;
                    int internshipCount = 0;
                    var programDetails = (from data in ProgramDetails
                                          group data by new
                                          {
                                              data.Speciality,
                                              data.Preference,
                                              data.ProgramType,
                                              data.ProgramDetail_StartDate,
                                              data.ProgramDetail_EndDate,
                                              data.SchoolName,
                                              data.Email,
                                              data.PhoneNumber,
                                              data.Fax,
                                              data.Building,
                                              data.Street,
                                              data.Country,
                                              data.State,
                                              data.County,
                                              data.City,
                                              data.ZipCode

                                          } into EducationDetailsdata
                                          select new
                                          {
                                              SpecialityName = EducationDetailsdata.Key.Speciality,
                                              ProgramType = EducationDetailsdata.Key.ProgramType,
                                              StartDate = EducationDetailsdata.Key.ProgramDetail_StartDate,
                                              EndDate = EducationDetailsdata.Key.ProgramDetail_EndDate,
                                              Preference = EducationDetailsdata.Key.Preference,
                                              SchoolName = EducationDetailsdata.Key.SchoolName,
                                              Email = EducationDetailsdata.Key.Email,
                                              PhoneNumber = EducationDetailsdata.Key.PhoneNumber,
                                              Fax = EducationDetailsdata.Key.Fax,
                                              Building = EducationDetailsdata.Key.Building,
                                              Street = EducationDetailsdata.Key.Street,
                                              Country = EducationDetailsdata.Key.Country,
                                              State = EducationDetailsdata.Key.State,
                                              County = EducationDetailsdata.Key.County,
                                              City = EducationDetailsdata.Key.City,
                                              ZipCode = EducationDetailsdata.Key.ZipCode
                                          }).ToList();

                    foreach (var program in programDetails)
                    {
                        if (program.ProgramType == "Internship")
                        {
                            if (internshipCount == 0 && program != null)
                            {
                                pmodel.InternshipFacility = program.SchoolName;
                                pmodel.InternshipFacility_City = program.City;
                                pmodel.InternshipFacility_Street = program.Street;
                                pmodel.InternshipFacility_State = program.State;
                                pmodel.InternshipFacility_ZipCode = program.ZipCode;
                                pmodel.InternshipFacility_Country = program.Country;
                                pmodel.InternshipFacility_EmailAddress = program.Email;

                                pmodel.InternshipSpecialty = program.SpecialityName;

                                string startDate = ConvertToDateString(program.StartDate);
                                string endDate = ConvertToDateString(program.EndDate);
                                pmodel.InternshipStartDate = startDate;
                                pmodel.InternshipEndDate = endDate;
                                pmodel.InternshipAttendedDate = startDate + " - " + endDate;

                                internshipCount++;
                            }
                            else if (internshipCount == 1 && program != null)
                            {
                                pmodel.InternshipFacility1 = program.SchoolName;
                                pmodel.InternshipFacility_City1 = program.City;
                                pmodel.InternshipFacility_Street1 = program.Street;
                                pmodel.InternshipFacility_State1 = program.State;
                                pmodel.InternshipFacility_ZipCode1 = program.ZipCode;
                                pmodel.InternshipFacility_Country1 = program.Country;
                                pmodel.InternshipFacility_EmailAddress1 = program.Email;

                                pmodel.InternshipSpecialty1 = program.SpecialityName;

                                string startDate1 = ConvertToDateString(program.StartDate);
                                string endDate1 = ConvertToDateString(program.EndDate);
                                pmodel.InternshipStartDate1 = startDate1;
                                pmodel.InternshipEndDate1 = endDate1;
                                pmodel.InternshipAttendedDate1 = startDate1 + " - " + endDate1;
                            }
                        }
                        else if (program.ProgramType == "Resident")
                        {
                            if (residencyCount == 0 && program != null)
                            {
                                pmodel.ResidencyFacility = program.SchoolName;
                                pmodel.ResidencyFacility_City = program.City;
                                pmodel.ResidencyFacility_Street = program.Street;
                                pmodel.ResidencyFacility_State = program.State;
                                pmodel.ResidencyFacility_ZipCode = program.ZipCode;
                                pmodel.ResidencyFacility_Country = program.Country;
                                pmodel.ResidencyFacility_EmailAddress = program.Email;


                                pmodel.ResidencySpecialty = program.SpecialityName;

                                string startDate = ConvertToDateString(program.StartDate);
                                string endDate = ConvertToDateString(program.EndDate);
                                pmodel.ResidencyStartDate = startDate;
                                pmodel.ResidencyEndDate = endDate;
                                pmodel.ResidencyAttendedDate = startDate + " - " + endDate;
                                residencyCount++;
                            }
                            else if (residencyCount == 1 && program != null)
                            {
                                pmodel.ResidencyFacility1 = program.SchoolName;
                                pmodel.ResidencyFacility_City1 = program.City;
                                pmodel.ResidencyFacility_State1 = program.State;
                                pmodel.ResidencyFacility_Street1 = program.Street;
                                pmodel.ResidencyFacility_ZipCode1 = program.ZipCode;
                                pmodel.ResidencyFacility_Country1 = program.Country;
                                pmodel.ResidencyFacility_EmailAddress1 = program.Email;


                                pmodel.ResidencySpecialty1 = program.SpecialityName;

                                string startDate1 = ConvertToDateString(program.StartDate);
                                string endDate1 = ConvertToDateString(program.EndDate);
                                pmodel.ResidencyStartDate1 = startDate1;
                                pmodel.ResidencyEndDate1 = endDate1;
                                pmodel.ResidencyAttendedDate1 = startDate1 + " - " + endDate1;
                            }

                        }
                        else if (program.ProgramType == "Fellowship")
                        {
                            if (fellowshipCount == 0 && program != null)
                            {
                                pmodel.FellowshipFacility = program.SchoolName;
                                pmodel.FellowshipFacility_City = program.City;
                                pmodel.FellowshipFacility_State = program.State;
                                pmodel.FellowshipFacility_ZipCode = program.ZipCode;
                                pmodel.FellowshipFacility_Country = program.Country;
                                pmodel.FellowshipFacility_EmailAddress = program.Email;

                                pmodel.FellowshipSpecialty = program.SpecialityName;

                                string startDate = ConvertToDateString(program.StartDate);
                                string endDate = ConvertToDateString(program.EndDate);
                                pmodel.FellowshipStartDate = startDate;
                                pmodel.FellowshipEndDate = endDate;
                                pmodel.FellowshipAttendedDate = startDate + " - " + endDate;
                                fellowshipCount++;
                            }
                            if (fellowshipCount == 1 && program != null)
                            {

                                pmodel.FellowshipFacility1 = program.SchoolName;
                                pmodel.FellowshipFacility_City1 = program.City;
                                pmodel.FellowshipFacility_State1 = program.State;
                                pmodel.FellowshipFacility_ZipCode1 = program.ZipCode;
                                pmodel.FellowshipFacility_Country1 = program.Country;
                                pmodel.FellowshipFacility_EmailAddress1 = program.Email;

                                pmodel.FellowshipSpecialty1 = program.SpecialityName;

                                string startDate1 = ConvertToDateString(program.StartDate);
                                string endDate1 = ConvertToDateString(program.EndDate);
                                pmodel.FellowshipStartDate1 = startDate1;
                                pmodel.FellowshipEndDate1 = endDate1;
                                pmodel.FellowshipAttendedDate1 = startDate1 + " - " + endDate1;
                            }

                        }
                    }
                }

                #endregion

                #region Post Graduation

                if (EducationDetails.Count > 0)
                {
                    var educationDetails = EducationDetails.Where(s => s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString() && s.EducationQualificationType == AHC.CD.Entities.MasterData.Enums.EducationQualificationType.PostGraduate).ToList();
                    var educationDetailsCount = educationDetails.Count;

                    if (educationDetailsCount > 0 && educationDetails.ElementAt(educationDetailsCount - 1) != null)
                    {
                        var SchoolInformation = (from data in educationDetails
                                                 group data by new
                                                 {

                                                     data.SchoolName,
                                                     data.Email,
                                                     data.PhoneNumber,
                                                     data.Fax,
                                                     data.Building,
                                                     data.Street,
                                                     data.Country,
                                                     data.State,
                                                     data.County,
                                                     data.City,
                                                     data.ZipCode


                                                 } into EducationDetailsdata
                                                 select new
                                                 {


                                                     SchoolName = EducationDetailsdata.Key.SchoolName,
                                                     Email = EducationDetailsdata.Key.Email,
                                                     PhoneNumber = EducationDetailsdata.Key.PhoneNumber,
                                                     Fax = EducationDetailsdata.Key.Fax,
                                                     Building = EducationDetailsdata.Key.Building,
                                                     Street = EducationDetailsdata.Key.Street,
                                                     Country = EducationDetailsdata.Key.Country,
                                                     State = EducationDetailsdata.Key.State,
                                                     County = EducationDetailsdata.Key.County,
                                                     City = EducationDetailsdata.Key.City,
                                                     ZipCode = EducationDetailsdata.Key.ZipCode
                                                 }).ToList().LastOrDefault();
                        pmodel.UnderGraduation_Type1 = educationDetails.ElementAt(educationDetailsCount - 1).QualificationType;
                        pmodel.Graduation_Type1 = pmodel.UnderGraduation_Type1;

                        if (SchoolInformation != null)
                        {
                            pmodel.UnderGraduation_SchoolName3 = SchoolInformation.SchoolName;
                            //pmodel.Provider_Highest_Degree_SchoolName = pmodel.UnderGraduation_SchoolName1;
                            pmodel.UnderGraduation_EmailId3 = SchoolInformation.Email;
                            pmodel.UnderGraduation_Fax3 = SchoolInformation.Fax;
                            pmodel.UnderGraduation_TelephoneNo3 = SchoolInformation.PhoneNumber;
                            pmodel.UnderGraduation_EmailAddress3 = SchoolInformation.Email;
                            //pmodel.UnderGraduation_Number1 = SchoolInformation.SchoolInformationID.ToString();
                            pmodel.UnderGraduation_Building3 = SchoolInformation.Building;
                            pmodel.UnderGraduation_Street3 = SchoolInformation.Street;
                            pmodel.UnderGraduation_Country3 = SchoolInformation.Country;
                            pmodel.UnderGraduation_State3 = SchoolInformation.State;
                            pmodel.UnderGraduation_County3 = SchoolInformation.County;
                            pmodel.UnderGraduation_City3 = SchoolInformation.City;
                            pmodel.UnderGraduation_ZipCode3 = SchoolInformation.ZipCode;
                        }
                        pmodel.UnderGraduation_Degree3 = educationDetails.ElementAt(educationDetailsCount - 1).QualificationDegree;
                        //pmodel.Provider_Highest_Degree = pmodel.UnderGraduation_Degree1;
                        pmodel.UnderGraduation_StartDate3 = ConvertToDateString(educationDetails.ElementAt(educationDetailsCount - 1).StartDate);
                        pmodel.UnderGraduation_EndDate3 = ConvertToDateString(educationDetails.ElementAt(educationDetailsCount - 1).EndDate);
                        //pmodel.Provider_Highest_Degree_StartDate = pmodel.UnderGraduation_StartDate1;
                        //pmodel.Provider_Highest_Degree_EndDate = pmodel.UnderGraduation_EndDate1;
                        pmodel.IsUnderGraduatedFromSchool3 = educationDetails.ElementAt(educationDetailsCount - 1).IsUSGraduate;
                        pmodel.UnderGraduation_Certificate3 = educationDetails.ElementAt(educationDetailsCount - 1).CertificatePath;
                    }
                }


                #endregion

                #region CME Certification
                var CMECertifications = (from data in EducationDetails
                                         group data by new
                                         {
                                             data.Certification,
                                             data.StartDate,
                                             data.EndDate,
                                             data.SchoolName,
                                             data.Email,
                                             data.PhoneNumber,
                                             data.Fax,
                                             data.Building,
                                             data.Street,
                                             data.Country,
                                             data.State,
                                             data.County,
                                             data.City,
                                             data.ZipCode


                                         } into EducationDetailsdata
                                         select new
                                         {

                                             Certification = EducationDetailsdata.Key.Certification,
                                             StartDate = EducationDetailsdata.Key.StartDate,
                                             EndDate = EducationDetailsdata.Key.EndDate,
                                             SchoolName = EducationDetailsdata.Key.SchoolName,
                                             Email = EducationDetailsdata.Key.Email,
                                             PhoneNumber = EducationDetailsdata.Key.PhoneNumber,
                                             Fax = EducationDetailsdata.Key.Fax,
                                             Building = EducationDetailsdata.Key.Building,
                                             Street = EducationDetailsdata.Key.Street,
                                             Country = EducationDetailsdata.Key.Country,
                                             State = EducationDetailsdata.Key.State,
                                             County = EducationDetailsdata.Key.County,
                                             City = EducationDetailsdata.Key.City,
                                             ZipCode = EducationDetailsdata.Key.ZipCode
                                         }).ToList().LastOrDefault();

                if (CMECertifications != null)
                {

                    pmodel.CME_CertificateName1 = CMECertifications.Certification;
                    if (CMECertifications != null)
                    {
                        pmodel.CME_SchoolName = CMECertifications.SchoolName;
                        pmodel.CME_MailingAddress = CMECertifications.Email;
                        pmodel.CME_City1 = CMECertifications.City;
                        pmodel.CME_State1 = CMECertifications.State;
                        pmodel.CME_ZipCode1 = CMECertifications.ZipCode;
                        pmodel.CME_Country1 = CMECertifications.Country;
                    }
                    pmodel.CME_StartDate1 = ConvertToDateString(CMECertifications.StartDate);
                    pmodel.CME_EndDate1 = ConvertToDateString(CMECertifications.EndDate);
                }
                #endregion

                #endregion

                #region Hospital Privileges

                if (HospitalPrivilegeInformation != null && HospitalPrivilegeInformation.Count > 0)
                {
                    var primaryHospitalInfo = HospitalPrivilegeInformation.FirstOrDefault(p => p.Preference == AHC.CD.Entities.MasterData.Enums.PreferenceType.Primary.ToString()
                                    && p.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString());

                    var secondaryHospitalInfo = HospitalPrivilegeInformation.Where(p => p.Preference != AHC.CD.Entities.MasterData.Enums.PreferenceType.Primary.ToString()
                                    && p.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();

                    var secondaryHospitalCount = secondaryHospitalInfo.Count;

                    var HospitalPrivilegeInfo = HospitalPrivilegeInformation.Where(p => p.Preference == AHC.CD.Entities.MasterData.Enums.PreferenceType.Secondary.ToString() && p.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString() || p.Preference == AHC.CD.Entities.MasterData.Enums.PreferenceType.Primary.ToString() && p.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).Take(3).ToList();
                    var HospitalCount = HospitalPrivilegeInfo.Count;
                    if (HospitalPrivilegeInfo != null)
                    {
                        if (HospitalCount > 0)
                        {
                            pmodel.HP_HospitalName1 = HospitalPrivilegeInfo.ElementAt(HospitalCount - 1).HospitalName;
                            pmodel.HP_Street1 = HospitalPrivilegeInfo.ElementAt(HospitalCount - 1).Street;
                            pmodel.HP_Suite1 = HospitalPrivilegeInfo.ElementAt(HospitalCount - 1).LocationName;
                            pmodel.HP_State1 = HospitalPrivilegeInfo.ElementAt(HospitalCount - 1).State;
                            pmodel.HP_City1 = HospitalPrivilegeInfo.ElementAt(HospitalCount - 1).City;
                            pmodel.HP_ZipCode1 = HospitalPrivilegeInfo.ElementAt(HospitalCount - 1).ZipCode;

                            pmodel.HP_Location1_Line1 = pmodel.HP_Street1 + " " + pmodel.HP_Suite1;
                            pmodel.HP_Primary_Location1_Line2 = pmodel.HP_City1 + " " + pmodel.HP_State1 + " " + pmodel.HP_ZipCode1;
                        }
                        if (HospitalCount > 1)
                        {
                            pmodel.HP_HospitalName2 = HospitalPrivilegeInfo.ElementAt(HospitalCount - 2).HospitalName;
                            pmodel.HP_Street2 = HospitalPrivilegeInfo.ElementAt(HospitalCount - 2).Street;
                            pmodel.HP_Suite2 = HospitalPrivilegeInfo.ElementAt(HospitalCount - 2).LocationName;
                            pmodel.HP_State2 = HospitalPrivilegeInfo.ElementAt(HospitalCount - 2).State;
                            pmodel.HP_City2 = HospitalPrivilegeInfo.ElementAt(HospitalCount - 2).City;
                            pmodel.HP_ZipCode2 = HospitalPrivilegeInfo.ElementAt(HospitalCount - 2).ZipCode;

                            pmodel.HP_Location2_Line1 = pmodel.HP_Street2 + " " + pmodel.HP_Suite2;
                            pmodel.HP_Location2_Line2 = pmodel.HP_City2 + " " + pmodel.HP_State2 + " " + pmodel.HP_ZipCode2;
                        }
                        if (HospitalCount > 2)
                        {
                            pmodel.HP_HospitalName3 = HospitalPrivilegeInfo.ElementAt(HospitalCount - 3).HospitalName;
                            pmodel.HP_Street3 = HospitalPrivilegeInfo.ElementAt(HospitalCount - 3).Street;
                            pmodel.HP_Suite3 = HospitalPrivilegeInfo.ElementAt(HospitalCount - 3).LocationName;
                            pmodel.HP_State3 = HospitalPrivilegeInfo.ElementAt(HospitalCount - 3).State;
                            pmodel.HP_City3 = HospitalPrivilegeInfo.ElementAt(HospitalCount - 3).City;
                            pmodel.HP_ZipCode3 = HospitalPrivilegeInfo.ElementAt(HospitalCount - 3).ZipCode;

                            pmodel.HP_Location3_Line1 = pmodel.HP_Street3 + " " + pmodel.HP_Suite3;
                            pmodel.HP_Location3_Line2 = pmodel.HP_City3 + " " + pmodel.HP_State3 + " " + pmodel.HP_ZipCode3;
                        }
                    }

                    #region Primary Hospital Information
                    if (primaryHospitalInfo != null)
                    {

                        pmodel.HospitalPrivilege_PrimaryHospitalName = primaryHospitalInfo.HospitalName;
                        pmodel.HospitalPrivilege_PrimaryDepartment = primaryHospitalInfo.DepartmentName;
                        pmodel.HospitalPrivilege_PrimaryCity = primaryHospitalInfo.City;

                        pmodel.HospitalPrivilege_PrimaryStreet = primaryHospitalInfo.Street;
                        pmodel.HospitalPrivilege_PrimaryCity = primaryHospitalInfo.City;
                        pmodel.HospitalPrivilege_PrimarySuite = primaryHospitalInfo.LocationName;
                        pmodel.HospitalPrivilege_PrimaryState = primaryHospitalInfo.State;
                        pmodel.HospitalPrivilege_PrimaryZipCode = primaryHospitalInfo.ZipCode;
                        pmodel.HospitalPrivilege_PrimaryCountry = primaryHospitalInfo.Country;
                        pmodel.HospitalPrivilege_PrimaryPhone = primaryHospitalInfo.PhoneNumber;
                        pmodel.HospitalPrivilege_PrimaryFax = primaryHospitalInfo.Fax;
                        pmodel.HospitalPrivilege_PrimaryEmail = primaryHospitalInfo.Email;
                        pmodel.HospitalPrivilege_PrimaryCounty = primaryHospitalInfo.County;
                        pmodel.HospitalPrivilege_PrimaryAffiliationStartDate = ConvertToDateString(primaryHospitalInfo.AffilicationStartDate);
                        pmodel.HospitalPrivilege_PrimaryAffiliationEndDate = ConvertToDateString(primaryHospitalInfo.AffiliationEndDate);

                        pmodel.HospitalPrivilege_PrimaryLocation_Line1 = pmodel.HospitalPrivilege_PrimaryStreet + " " + pmodel.HospitalPrivilege_PrimarySuite;
                        pmodel.HospitalPrivilege_PrimaryLocation_Line2 = pmodel.HospitalPrivilege_PrimaryCity + "  " + pmodel.HospitalPrivilege_PrimaryState + " " + pmodel.HospitalPrivilege_PrimaryZipCode;

                    # endregion

                        #region primary Specific Hospital Privileges

                        pmodel.HospitalPrivilege_PrimaryHospitalName1 = primaryHospitalInfo.HospitalName;

                        pmodel.HospitalPrivilege_PrimaryCity1 = primaryHospitalInfo.City;

                        pmodel.HospitalPrivilege_PrimaryStreet1 = primaryHospitalInfo.Street;
                        pmodel.HospitalPrivilege_PrimaryCity1 = primaryHospitalInfo.City;
                        pmodel.HospitalPrivilege_PrimarySuite1 = primaryHospitalInfo.LocationName;
                        pmodel.HospitalPrivilege_PrimaryState1 = primaryHospitalInfo.State;
                        pmodel.HospitalPrivilege_PrimaryZipCode1 = primaryHospitalInfo.ZipCode;
                        pmodel.HospitalPrivilege_PrimaryCountry1 = primaryHospitalInfo.Country;
                        pmodel.HospitalPrivilege_PrimaryPhone1 = primaryHospitalInfo.PhoneNumber;
                        pmodel.HospitalPrivilege_PrimaryFax1 = primaryHospitalInfo.Fax;
                        pmodel.HospitalPrivilege_PrimaryEmail1 = primaryHospitalInfo.Email;
                        pmodel.HospitalPrivilege_PrimaryCounty1 = primaryHospitalInfo.County;
                        pmodel.HospitalPrivilege_PrimaryAffiliationStartDate1 = ConvertToDateString(primaryHospitalInfo.AffilicationStartDate);
                        pmodel.HospitalPrivilege_PrimaryAffiliationEndDate1 = ConvertToDateString(primaryHospitalInfo.AffiliationEndDate);

                        # endregion


                        #region Secondary Hospital Information
                        if (secondaryHospitalInfo.Count > 0)
                        {
                            if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1) != null)
                            {
                                pmodel.HospitalPrivilege_HospitalName1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).HospitalName;

                                pmodel.HospitalPrivilege_StartDate1 = ConvertToDateString(secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).AffilicationStartDate);
                                pmodel.HospitalPrivilege_HospitalStatus1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).Status;
                                pmodel.HospitalPrivilege_EndDate1 = ConvertToDateString(secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).AffiliationEndDate);

                                pmodel.HospitalPrivilege_Suite1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).LocationName;
                                pmodel.HospitalPrivilege_State1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).State;
                                pmodel.HospitalPrivilege_City1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).City;
                                pmodel.HospitalPrivilege_ZipCode1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).ZipCode;
                                pmodel.HospitalPrivilege_County1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).Country;
                                pmodel.HospitalPrivilege_Phone1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).PhoneNumber;
                                pmodel.HospitalPrivilege_Fax1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).Fax;
                                pmodel.HospitalPrivilege_Email1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).Email;
                                pmodel.HospitalPrivilege_County1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).County;

                                pmodel.HospitalPrivilege_Address1_Line1 = pmodel.HospitalPrivilege_Street1 + " " + pmodel.HospitalPrivilege_Suite1;
                                pmodel.HospitalPrivilege_Address1_Line2 = pmodel.HospitalPrivilege_City1 + ", " + pmodel.HospitalPrivilege_State1 + " " + pmodel.HospitalPrivilege_ZipCode1;
                                pmodel.HospitalPrivilege_StaffCategory1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).StaffCategory_Title;

                                pmodel.HospitalPrivilege_Number1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).HospitalID.ToString();
                                pmodel.HospitalPrivilege_Specialty1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).SpecialityName;

                                pmodel.HospitalPrivilege_AffiliationStartDate1 = ConvertToDateString(secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).AffilicationStartDate);
                                pmodel.HospitalPrivilege_AffiliationEndDate1 = ConvertToDateString(secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).AffiliationEndDate);
                                pmodel.HospitalPrivilege_TerminatedAffiliationExplanation1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).HospitalName;
                                pmodel.HospitalPrivilege_Department1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).DepartmentName;

                                pmodel.HospitalPrivilege_ContactPerson1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).ContactPersonName;


                            }
                        }
                        if (secondaryHospitalInfo.Count > 1)
                        {
                            if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2) != null)
                            {
                                pmodel.HospitalPrivilege_HospitalName2 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).HospitalName;

                                pmodel.HospitalPrivilege_City2 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).City;

                                pmodel.HospitalPrivilege_Street2 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).Street;
                                pmodel.HospitalPrivilege_Suite2 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).LocationName;
                                pmodel.HospitalPrivilege_State2 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).State;
                                pmodel.HospitalPrivilege_ZipCode2 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).ZipCode;
                                pmodel.HospitalPrivilege_County2 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).Country;
                                pmodel.HospitalPrivilege_Phone2 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).PhoneNumber;
                                pmodel.HospitalPrivilege_Fax2 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).Fax;
                                pmodel.HospitalPrivilege_Email2 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).Email;
                                pmodel.HospitalPrivilege_County2 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).County;

                                pmodel.HospitalPrivilege_Address2_Line1 = pmodel.HospitalPrivilege_Street2 + " " + pmodel.HospitalPrivilege_Suite2;
                                pmodel.HospitalPrivilege_Address2_Line2 = pmodel.HospitalPrivilege_City2 + "  " + pmodel.HospitalPrivilege_State2 + " " + pmodel.HospitalPrivilege_ZipCode2;

                            }
                        }
                        if (secondaryHospitalInfo.Count > 2)
                        {
                            if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3) != null)
                            {
                                pmodel.HospitalPrivilege_HospitalName3 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3).HospitalName;

                                pmodel.HospitalPrivilege_City3 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3).City;

                                pmodel.HospitalPrivilege_Street3 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3).Street;
                                pmodel.HospitalPrivilege_Suite3 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3).LocationName;
                                pmodel.HospitalPrivilege_State3 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3).State;
                                pmodel.HospitalPrivilege_ZipCode3 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3).ZipCode;
                                pmodel.HospitalPrivilege_County3 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3).Country;
                                pmodel.HospitalPrivilege_Phone3 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3).PhoneNumber;
                                pmodel.HospitalPrivilege_Fax3 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3).Fax;
                                pmodel.HospitalPrivilege_Email3 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3).Email;
                                pmodel.HospitalPrivilege_County3 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3).County;

                                pmodel.HospitalPrivilege_Address3_Line1 = pmodel.HospitalPrivilege_Street3 + " " + pmodel.HospitalPrivilege_Suite3;
                                pmodel.HospitalPrivilege_Address3_Line2 = pmodel.HospitalPrivilege_City3 + " " + pmodel.HospitalPrivilege_State3 + " " + pmodel.HospitalPrivilege_ZipCode3;
                            }
                        }
                        if (secondaryHospitalInfo.Count > 3)
                        {
                            if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4) != null)
                            {
                                pmodel.HospitalPrivilege_HospitalName4 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4).HospitalName;

                                pmodel.HospitalPrivilege_City4 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4).City;

                                pmodel.HospitalPrivilege_Street4 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4).Street;
                                pmodel.HospitalPrivilege_Suite4 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4).LocationName;
                                pmodel.HospitalPrivilege_State4 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4).State;
                                pmodel.HospitalPrivilege_ZipCode4 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4).ZipCode;
                                pmodel.HospitalPrivilege_County4 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4).Country;
                                pmodel.HospitalPrivilege_Phone4 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4).PhoneNumber;
                                pmodel.HospitalPrivilege_Fax4 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4).Fax;
                                pmodel.HospitalPrivilege_Email4 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4).Email;
                                pmodel.HospitalPrivilege_County4 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4).County;

                                pmodel.HospitalPrivilege_Address4_Line1 = pmodel.HospitalPrivilege_Street4 + " " + pmodel.HospitalPrivilege_Suite4;
                                pmodel.HospitalPrivilege_Address4_Line2 = pmodel.HospitalPrivilege_City4 + " " + pmodel.HospitalPrivilege_State4 + " " + pmodel.HospitalPrivilege_ZipCode4;

                            }
                        }
                        #endregion


                    }

                    else
                    {
                        if (secondaryHospitalInfo.Count > 0)
                        {
                            if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1) != null)
                            {
                                pmodel.HospitalPrivilege_PrimaryHospitalName = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).HospitalName;

                                pmodel.HospitalPrivilege_PrimaryCity = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).City;

                                pmodel.HospitalPrivilege_PrimaryStreet = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).Street;
                                pmodel.HospitalPrivilege_PrimarySuite = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).LocationName;
                                pmodel.HospitalPrivilege_PrimaryState = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).State;
                                pmodel.HospitalPrivilege_PrimaryZipCode = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).ZipCode;
                                pmodel.HospitalPrivilege_PrimaryCountry = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).Country;
                                pmodel.HospitalPrivilege_PrimaryPhone = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).PhoneNumber;
                                pmodel.HospitalPrivilege_PrimaryFax = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).Fax;
                                pmodel.HospitalPrivilege_PrimaryEmail = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).Email;
                                pmodel.HospitalPrivilege_PrimaryCounty = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).County;
                                pmodel.HospitalPrivilege_PrimaryAffiliationStartDate = ConvertToDateString(secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).AffilicationStartDate);
                                pmodel.HospitalPrivilege_PrimaryAffiliationEndDate = ConvertToDateString(secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).AffiliationEndDate);

                                pmodel.HospitalPrivilege_PrimaryLocation_Line1 = pmodel.HospitalPrivilege_PrimaryStreet + " " + pmodel.HospitalPrivilege_PrimarySuite;
                                pmodel.HospitalPrivilege_PrimaryLocation_Line2 = pmodel.HospitalPrivilege_PrimaryCity + "  " + pmodel.HospitalPrivilege_PrimaryState + " " + pmodel.HospitalPrivilege_PrimaryZipCode;

                            }

                            if (secondaryHospitalInfo.Count == 1)
                            {
                                pmodel.HospitalPrivilege_HospitalName1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).HospitalName;

                                pmodel.HospitalPrivilege_City1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).City;

                                pmodel.HospitalPrivilege_Street1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).Street;
                                pmodel.HospitalPrivilege_Suite1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).LocationName;
                                pmodel.HospitalPrivilege_State1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).State;
                                pmodel.HospitalPrivilege_ZipCode1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).ZipCode;
                                pmodel.HospitalPrivilege_County1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).Country;
                                pmodel.HospitalPrivilege_Phone1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).PhoneNumber;
                                pmodel.HospitalPrivilege_Fax1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).Fax;
                                pmodel.HospitalPrivilege_Email1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).Email;
                                pmodel.HospitalPrivilege_County1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).County;
                                pmodel.HospitalPrivilege_PrimaryStartDate1 = ConvertToDateString(secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).AffilicationStartDate);
                                pmodel.HospitalPrivilege_PrimaryEndDate1 = ConvertToDateString(secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).AffiliationEndDate);

                                pmodel.HospitalPrivilege_Address1_Line1 = pmodel.HospitalPrivilege_Street1 + " " + pmodel.HospitalPrivilege_Suite1;
                                pmodel.HospitalPrivilege_Address1_Line2 = pmodel.HospitalPrivilege_City1 + ", " + pmodel.HospitalPrivilege_State1 + " " + pmodel.HospitalPrivilege_ZipCode1;

                            }
                        }
                        if (secondaryHospitalInfo.Count > 1)
                        {
                            if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2) != null)
                            {
                                pmodel.HospitalPrivilege_HospitalName1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).HospitalName;

                                pmodel.HospitalPrivilege_City1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).City;

                                pmodel.HospitalPrivilege_Street1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).Street;
                                pmodel.HospitalPrivilege_Suite1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).LocationName;
                                pmodel.HospitalPrivilege_State1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).State;
                                pmodel.HospitalPrivilege_ZipCode1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).ZipCode;
                                pmodel.HospitalPrivilege_County1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).Country;
                                pmodel.HospitalPrivilege_Phone1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).PhoneNumber;
                                pmodel.HospitalPrivilege_Fax1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).Fax;
                                pmodel.HospitalPrivilege_Email1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).Email;
                                pmodel.HospitalPrivilege_County1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).County;

                                pmodel.HospitalPrivilege_PrimaryStartDate1 = ConvertToDateString(secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).AffilicationStartDate);
                                pmodel.HospitalPrivilege_PrimaryEndDate1 = ConvertToDateString(secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).AffiliationEndDate);

                                pmodel.HospitalPrivilege_Address1_Line1 = pmodel.HospitalPrivilege_Street1 + " " + pmodel.HospitalPrivilege_Suite1;
                                pmodel.HospitalPrivilege_Address1_Line2 = pmodel.HospitalPrivilege_City1 + ", " + pmodel.HospitalPrivilege_State1 + " " + pmodel.HospitalPrivilege_ZipCode1;
                            }
                        }
                        if (secondaryHospitalInfo.Count > 2)
                        {
                            if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3) != null)
                            {
                                pmodel.HospitalPrivilege_HospitalName2 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3).HospitalName;

                                pmodel.HospitalPrivilege_City2 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3).City;

                                pmodel.HospitalPrivilege_Street2 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3).Street;
                                pmodel.HospitalPrivilege_Suite2 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3).LocationName;
                                pmodel.HospitalPrivilege_State2 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3).State;
                                pmodel.HospitalPrivilege_ZipCode2 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3).ZipCode;
                                pmodel.HospitalPrivilege_County2 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3).Country;
                                pmodel.HospitalPrivilege_Phone2 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3).PhoneNumber;
                                pmodel.HospitalPrivilege_Fax2 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3).Fax;
                                pmodel.HospitalPrivilege_Email2 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3).Email;
                                pmodel.HospitalPrivilege_County2 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3).County;

                                pmodel.HospitalPrivilege_Address2_Line1 = pmodel.HospitalPrivilege_Street2 + " " + pmodel.HospitalPrivilege_Suite2;
                                pmodel.HospitalPrivilege_Address2_Line2 = pmodel.HospitalPrivilege_City2 + ", " + pmodel.HospitalPrivilege_State2 + " " + pmodel.HospitalPrivilege_ZipCode2;
                            }
                        }
                        if (secondaryHospitalInfo.Count > 3)
                        {
                            if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4) != null)
                            {
                                pmodel.HospitalPrivilege_HospitalName3 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4).HospitalName;

                                pmodel.HospitalPrivilege_City3 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4).City;

                                pmodel.HospitalPrivilege_Street3 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4).Street;
                                pmodel.HospitalPrivilege_Suite3 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4).LocationName;
                                pmodel.HospitalPrivilege_State3 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4).State;
                                pmodel.HospitalPrivilege_ZipCode3 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4).ZipCode;
                                pmodel.HospitalPrivilege_County3 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4).Country;
                                pmodel.HospitalPrivilege_Phone3 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4).PhoneNumber;
                                pmodel.HospitalPrivilege_Fax3 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4).Fax;
                                pmodel.HospitalPrivilege_Email3 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4).Email;
                                pmodel.HospitalPrivilege_County3 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4).County;

                                pmodel.HospitalPrivilege_Address3_Line1 = pmodel.HospitalPrivilege_Street3 + " " + pmodel.HospitalPrivilege_Suite3;
                                pmodel.HospitalPrivilege_Address3_Line2 = pmodel.HospitalPrivilege_City3 + ", " + pmodel.HospitalPrivilege_State3 + " " + pmodel.HospitalPrivilege_ZipCode3;
                            }
                        }
                    }

                }


                #endregion

                #region Custom Field
                if (CustomFieldInformation.Count > 0 && CustomFieldInformation != null)
                {
                    var CustomFieldInformationCount = CustomFieldInformation.Count;
                    foreach (var item in CustomFieldInformation)
                    {
                        if (item.CustomFieldTitle == "TAX ID")
                        {
                            pmodel.GroupTaxId = item.CustomFieldDataValue;
                        }
                        if (item.CustomFieldTitle == "GROUP NAME")
                        {
                            pmodel.GroupName = item.CustomFieldDataValue;
                        }
                        if (item.CustomFieldTitle == "GROUP NPI NUMBER")
                        {
                            pmodel.GroupNPI = item.CustomFieldDataValue;
                        }
                    }
                }

                #endregion

                string pdfFileName = readXml(pmodel, templateName, CDUserId);

                return pdfFileName;


            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public async Task<List<PlanFormGenerationBusinessModel>> GenerateFormsUsingADO(List<int> ProfileIDs, List<string> templateNames, string UserAuthId)
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

                            string filePath = "\\ApplicationDocument\\GeneratedTemplatePdf\\" + await GenerateGenericPlanFormPDF(id, template, UserAuthId);
                            formGeneration.GeneratedFilePaths.Add(filePath);
                        }
                    }

                    bulkPlan.Add(formGeneration);
                }

            }

            return bulkPlan;
        }
        public async Task<string> GeneratePlanFormPDF(int profileId, string templateName, string UserAuthId)
        {
            Profile profile = await GetProfileObject(profileId);
            string CDUserId = GetCredentialingUserId(UserAuthId).ToString();
            PDFMappingDataBusinessModel pmodel = new PDFMappingDataBusinessModel();

            try
            {
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
                        {
                            pmodel.Personal_ProviderType = profile.PersonalDetail.ProviderTitles.ElementAt(titleCount - 1).ProviderType.Code;
                            pmodel.Personal_ProviderTitleName = profile.PersonalDetail.ProviderTitles.ElementAt(titleCount - 1).ProviderType.Title;
                        }


                    }
                }
                if (profile.PersonalDetail.MiddleName != null)
                    pmodel.Personal_ProviderName = profile.PersonalDetail.FirstName + " " + profile.PersonalDetail.MiddleName + " " + profile.PersonalDetail.LastName;
                else
                    pmodel.Personal_ProviderName = profile.PersonalDetail.FirstName + " " + profile.PersonalDetail.LastName;

                if (pmodel.Personal_ProviderType != null)
                {
                    pmodel.Provider_FullNameTitle = pmodel.Personal_ProviderName + " " + pmodel.Personal_ProviderType;
                    pmodel.Personal_ProviderTitle = pmodel.Personal_ProviderType;
                }
                else
                {
                    pmodel.Provider_FullNameTitle = pmodel.Personal_ProviderName;
                }

                pmodel.Personal_ProviderFirstName = profile.PersonalDetail.FirstName;
                pmodel.Personal_ProviderMiddleName = profile.PersonalDetail.MiddleName;
                if (pmodel.Personal_ProviderMiddleName != null)
                {
                    pmodel.Personal_ProviderMiddleNameFirstLetter = pmodel.Personal_ProviderMiddleName.Substring(0, 1);
                }
                else
                {
                    pmodel.Personal_ProviderMiddleNameFirstLetter = null;
                }

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


                if (profile.LanguageInfo != null && profile.LanguageInfo.KnownLanguages.Count > 0)
                {
                    foreach (var item in profile.LanguageInfo.KnownLanguages)
                    {
                        if (item != null)
                            pmodel.Personal_LanguageKnown1 += item.Language + ", ";
                    }
                    //pmodel.Personal_LanguageKnown1 = profile.LanguageInfo.KnownLanguages.FirstOrDefault(l => l.ProficiencyIndex == 1).Language;

                    var languages = profile.LanguageInfo.KnownLanguages.OrderBy(o => o.ProficiencyIndex).ToList();

                    if (languages.Count > 0)
                    {
                        pmodel.Personal_LanguageKnown2 = languages.ElementAt(0).Language;
                    }
                    if (languages.Count > 1)
                    {
                        pmodel.Personal_LanguageKnown3 = languages.ElementAt(1).Language;
                    }
                    if (languages.Count > 2)
                    {
                        pmodel.Personal_LanguageKnown4 = languages.ElementAt(2).Language;
                    }
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
                    pmodel.Personal_Address1 = pmodel.Personal_StreetAddress1 + " ," + pmodel.Personal_ApartmentNumber1 + " ," + pmodel.Personal_City1 + " ," + pmodel.Personal_State1 + " ," + pmodel.Personal_ZipCode1;
                }

                string currDate = DateTime.Now.ToString("MM-dd-yyyy");
                pmodel.currentDate = currDate;
                pmodel.North_Carolina_PlanName = "CONVENTRY HEALTH PLAN";

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
                            var IsPhone = profile.ContactDetail.PhoneDetails.Any(p => p.PhoneType == AHC.CD.Entities.MasterData.Enums.PhoneTypeEnum.Mobile.ToString() && p.Number != null);
                            var IsFax = profile.ContactDetail.PhoneDetails.Any(p => p.PhoneType == AHC.CD.Entities.MasterData.Enums.PhoneTypeEnum.Fax.ToString() && p.Number != null);
                            var IsHome = profile.ContactDetail.PhoneDetails.Any(p => p.PhoneType == AHC.CD.Entities.MasterData.Enums.PhoneTypeEnum.Home.ToString() && p.Number != null);

                            if (IsPhone)
                            {
                                pmodel.Personal_MobileNumber1 = profile.ContactDetail.PhoneDetails.Where(p => p.PhoneType == AHC.CD.Entities.MasterData.Enums.PhoneTypeEnum.Mobile.ToString()).Last().Number.ToString();
                            }
                            if (IsFax)
                            {
                                pmodel.Personal_HomeFax1 = profile.ContactDetail.PhoneDetails.Where(p => p.PhoneType == AHC.CD.Entities.MasterData.Enums.PhoneTypeEnum.Fax.ToString()).Last().Number.ToString();
                            }
                            if (IsHome)
                            {
                                pmodel.Personal_HomePhone1 = profile.ContactDetail.PhoneDetails.Where(p => p.PhoneType == AHC.CD.Entities.MasterData.Enums.PhoneTypeEnum.Home.ToString()).Last().Number.ToString();
                            }
                        }
                    }


                    if (profile.ContactDetail.EmailIDs.Count > 0)
                    {
                        var emails = profile.ContactDetail.EmailIDs.Where(e => e.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();

                        if (emails.Count > 0)
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
                    pmodel.Personal_BirthPlace = pmodel.Personal_BirthCity + " ," + pmodel.Personal_BirthCounty + " ," + pmodel.Personal_BirthState;
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
                            pmodel.Specialty_PrimaryTaxonomyCode = primaryInfo.Specialty.TaxonomyCode;
                        }

                        if (primaryInfo.IsBoardCertified != null && primaryInfo.IsBoardCertified == AHC.CD.Entities.MasterData.Enums.YesNoOption.YES.ToString())
                        {
                            pmodel.Specialty_PrimaryIsBoardCertified = primaryInfo.IsBoardCertified.ToString();
                        }
                        else if (primaryInfo.SpecialtyBoardNotCertifiedDetail != null)
                        {
                            pmodel.ExamDate = ConvertToDateString(primaryInfo.SpecialtyBoardNotCertifiedDetail.ExamDate);
                            if (primaryInfo.SpecialtyBoardNotCertifiedDetail.ReasonForNotTakingExam == "I do not intend to take exam")
                            {
                                pmodel.ReasonForNotTakingExam = primaryInfo.SpecialtyBoardNotCertifiedDetail.ReasonForNotTakingExam;
                            }
                        }


                        if (primaryInfo.SpecialtyBoardCertifiedDetail != null)
                        {
                            pmodel.Specialty_PrimaryCertificate = primaryInfo.SpecialtyBoardCertifiedDetail.CertificateNumber;
                            pmodel.Specialty_PrimaryLastRecertificationDate = ConvertToDateString(primaryInfo.SpecialtyBoardCertifiedDetail.LastReCerificationDate);
                            pmodel.Specialty_PrimaryInitialCertificationDate = ConvertToDateString(primaryInfo.SpecialtyBoardCertifiedDetail.InitialCertificationDate);
                            pmodel.Specialty_PrimaryExpirationDate = ConvertToDateString(primaryInfo.SpecialtyBoardCertifiedDetail.ExpirationDate);

                            if (primaryInfo.SpecialtyBoardCertifiedDetail.SpecialtyBoard != null)
                                pmodel.Specialty_PrimaryBoardName = primaryInfo.SpecialtyBoardCertifiedDetail.SpecialtyBoard.Name;


                            if (pmodel.Specialty_PrimaryLastRecertificationDate != null)
                            {
                                pmodel.Specialty_CurrentIssueDate1_MM = pmodel.Specialty_PrimaryLastRecertificationDate.Split('-')[0];
                                pmodel.Specialty_CurrentIssueDate1_dd = pmodel.Specialty_PrimaryLastRecertificationDate.Split('-')[1];
                                pmodel.Specialty_CurrentIssueDate1_yyyy = pmodel.Specialty_PrimaryLastRecertificationDate.Split('-')[2].Substring(2);
                            }

                            if (pmodel.Specialty_PrimarySpecialtyName != "National Commission on Certification of Physician Assistants" && pmodel.Specialty_PrimaryLastRecertificationDate != null)
                            {
                                pmodel.NationalCommissionSpecialty_CurrentIssueDate1_MM = pmodel.Specialty_PrimaryLastRecertificationDate.Split('-')[0];
                                pmodel.NationalCommissionSpecialty_CurrentIssueDate1_dd = pmodel.Specialty_PrimaryLastRecertificationDate.Split('-')[1];
                                pmodel.NationalCommissionSpecialty_CurrentIssueDate1_yyyy = pmodel.Specialty_PrimaryLastRecertificationDate.Split('-')[2].Substring(2);
                            }

                        }

                        #region Specific Primary Specialty

                        if (primaryInfo.Specialty != null)
                        {
                            pmodel.Specialty_PrimarySpecialtyName1 = primaryInfo.Specialty.Name;
                            pmodel.Specialty_PrimaryTaxonomyCode1 = primaryInfo.Specialty.TaxonomyCode;
                        }

                        //if (primaryInfo.IsBoardCertified != null && primaryInfo.IsBoardCertified == AHC.CD.Entities.MasterData.Enums.YesNoOption.YES.ToString())
                        //{
                        //    pmodel.Specialty_PrimaryIsBoardCertified = primaryInfo.IsBoardCertified.ToString();
                        //}
                        //else if (primaryInfo.SpecialtyBoardNotCertifiedDetail != null)
                        //{
                        //    pmodel.ExamDate = ConvertToDateString(primaryInfo.SpecialtyBoardNotCertifiedDetail.ExamDate);
                        //    if (primaryInfo.SpecialtyBoardNotCertifiedDetail.ReasonForNotTakingExam == "I do not intend to take exam")
                        //    {
                        //        pmodel.ReasonForNotTakingExam = primaryInfo.SpecialtyBoardNotCertifiedDetail.ReasonForNotTakingExam;
                        //    }
                        //}


                        if (primaryInfo.SpecialtyBoardCertifiedDetail != null)
                        {
                            pmodel.Specialty_PrimaryCertificate1 = primaryInfo.SpecialtyBoardCertifiedDetail.CertificateNumber;
                            pmodel.Specialty_PrimaryLastRecertificationDate1 = ConvertToDateString(primaryInfo.SpecialtyBoardCertifiedDetail.LastReCerificationDate);
                            pmodel.Specialty_PrimaryInitialCertificationDate1 = ConvertToDateString(primaryInfo.SpecialtyBoardCertifiedDetail.InitialCertificationDate);
                            pmodel.Specialty_PrimaryExpirationDate1 = ConvertToDateString(primaryInfo.SpecialtyBoardCertifiedDetail.ExpirationDate);

                            if (primaryInfo.SpecialtyBoardCertifiedDetail.SpecialtyBoard != null)
                                pmodel.Specialty_PrimaryBoardName1 = primaryInfo.SpecialtyBoardCertifiedDetail.SpecialtyBoard.Name;

                        }
                        #endregion

                        if (secondaryInfo.Count > 0)
                        {
                            if (secondaryInfo.ElementAt(secondaryCount - 1).IsBoardCertified != null && secondaryInfo.ElementAt(secondaryCount - 1).IsBoardCertified == AHC.CD.Entities.MasterData.Enums.YesNoOption.YES.ToString())
                            {
                                pmodel.Specialty_IsBoardCertified1 = secondaryInfo.ElementAt(secondaryCount - 1).IsBoardCertified.ToString();
                            }
                            else if (secondaryInfo.ElementAt(secondaryCount - 1).SpecialtyBoardNotCertifiedDetail != null)
                            {
                                pmodel.ExamDate = ConvertToDateString(secondaryInfo.ElementAt(secondaryCount - 1).SpecialtyBoardNotCertifiedDetail.ExamDate);
                                if (secondaryInfo.ElementAt(secondaryCount - 1).SpecialtyBoardNotCertifiedDetail.ReasonForNotTakingExam == "I do not intend to take exam")
                                {
                                    pmodel.ReasonForNotTakingExam = secondaryInfo.ElementAt(secondaryCount - 1).SpecialtyBoardNotCertifiedDetail.ReasonForNotTakingExam;
                                }
                            }
                            if (secondaryInfo.ElementAt(secondaryCount - 1).SpecialtyBoardCertifiedDetail != null)
                            {
                                pmodel.Specialty_Certificate1 = secondaryInfo.ElementAt(secondaryCount - 1).SpecialtyBoardCertifiedDetail.CertificateNumber;
                                pmodel.Specialty_LastRecertificationDate1 = ConvertToDateString(secondaryInfo.ElementAt(secondaryCount - 1).SpecialtyBoardCertifiedDetail.LastReCerificationDate);
                                pmodel.Specialty_ExpirationDate1 = ConvertToDateString(secondaryInfo.ElementAt(secondaryCount - 1).SpecialtyBoardCertifiedDetail.ExpirationDate);
                                pmodel.Specialty_InitialCertificationDate1 = ConvertToDateString(secondaryInfo.ElementAt(secondaryCount - 1).SpecialtyBoardCertifiedDetail.InitialCertificationDate);
                                if (secondaryInfo.ElementAt(secondaryCount - 1).SpecialtyBoardCertifiedDetail.SpecialtyBoard != null)
                                    pmodel.Specialty_BoardName1 = secondaryInfo.ElementAt(secondaryCount - 1).SpecialtyBoardCertifiedDetail.SpecialtyBoard.Name;
                            }

                            if (secondaryInfo.ElementAt(secondaryCount - 1) != null && secondaryInfo.ElementAt(secondaryCount - 1).Specialty != null)
                            {
                                pmodel.Specialty_SpecialtyName1 = secondaryInfo.ElementAt(secondaryCount - 1).Specialty.Name;
                                pmodel.Specialty_TaxonomyCode1 = secondaryInfo.ElementAt(secondaryCount - 1).Specialty.TaxonomyCode;
                            }
                        }
                        if (secondaryInfo.Count > 1)
                        {
                            if (secondaryInfo.ElementAt(secondaryCount - 2) != null && secondaryInfo.ElementAt(secondaryCount - 2).Specialty != null)
                            {
                                pmodel.Specialty_SpecialtyName2 = secondaryInfo.ElementAt(secondaryCount - 2).Specialty.Name;
                                pmodel.Specialty_TaxonomyCode2 = secondaryInfo.ElementAt(secondaryCount - 2).Specialty.TaxonomyCode;
                            }
                        }
                    }
                    else
                    {
                        if (secondaryInfo.Count > 0)
                        {
                            if (secondaryInfo.ElementAt(secondaryCount - 1).IsBoardCertified != null && secondaryInfo.ElementAt(secondaryCount - 1).IsBoardCertified == AHC.CD.Entities.MasterData.Enums.YesNoOption.YES.ToString())
                            {
                                pmodel.Specialty_PrimaryIsBoardCertified = secondaryInfo.ElementAt(secondaryCount - 1).IsBoardCertified.ToString();
                            }
                            else if (secondaryInfo.ElementAt(secondaryCount - 1).SpecialtyBoardNotCertifiedDetail != null)
                            {
                                pmodel.ExamDate = ConvertToDateString(secondaryInfo.ElementAt(secondaryCount - 1).SpecialtyBoardNotCertifiedDetail.ExamDate);
                                if (secondaryInfo.ElementAt(secondaryCount - 1).SpecialtyBoardNotCertifiedDetail.ReasonForNotTakingExam == "I do not intend to take exam")
                                {
                                    pmodel.ReasonForNotTakingExam = secondaryInfo.ElementAt(secondaryCount - 1).SpecialtyBoardNotCertifiedDetail.ReasonForNotTakingExam;
                                }
                            }
                            if (secondaryInfo.ElementAt(secondaryCount - 1).SpecialtyBoardCertifiedDetail != null)
                            {
                                pmodel.Specialty_PrimaryCertificate = secondaryInfo.ElementAt(secondaryCount - 1).SpecialtyBoardCertifiedDetail.CertificateNumber;
                                pmodel.Specialty_PrimaryLastRecertificationDate = ConvertToDateString(secondaryInfo.ElementAt(secondaryCount - 1).SpecialtyBoardCertifiedDetail.LastReCerificationDate);
                                pmodel.Specialty_PrimaryExpirationDate = ConvertToDateString(secondaryInfo.ElementAt(secondaryCount - 1).SpecialtyBoardCertifiedDetail.ExpirationDate);
                                pmodel.Specialty_PrimaryInitialCertificationDate = ConvertToDateString(secondaryInfo.ElementAt(secondaryCount - 1).SpecialtyBoardCertifiedDetail.InitialCertificationDate);

                                if (secondaryInfo.ElementAt(secondaryCount - 1).SpecialtyBoardCertifiedDetail.SpecialtyBoard != null)
                                    pmodel.Specialty_PrimaryBoardName = secondaryInfo.ElementAt(secondaryCount - 1).SpecialtyBoardCertifiedDetail.SpecialtyBoard.Name;
                            }

                            if (secondaryInfo.ElementAt(secondaryCount - 1) != null && secondaryInfo.ElementAt(secondaryCount - 1).Specialty != null)
                            {
                                pmodel.Specialty_PrimarySpecialtyName = secondaryInfo.ElementAt(secondaryCount - 1).Specialty.Name;
                                pmodel.Specialty_PrimaryTaxonomyCode = secondaryInfo.ElementAt(secondaryCount - 1).Specialty.TaxonomyCode;
                            }

                            if (secondaryInfo.Count == 1)
                            {
                                if (secondaryInfo.ElementAt(secondaryCount - 1) != null && secondaryInfo.ElementAt(secondaryCount - 1).Specialty != null)
                                {
                                    pmodel.Specialty_SpecialtyName1 = secondaryInfo.ElementAt(secondaryCount - 1).Specialty.Name;
                                    pmodel.Specialty_TaxonomyCode1 = secondaryInfo.ElementAt(secondaryCount - 1).Specialty.TaxonomyCode;
                                }
                            }
                        }
                        if (secondaryInfo.Count > 1)
                        {
                            if (secondaryInfo.ElementAt(secondaryCount - 2).IsBoardCertified != null && secondaryInfo.ElementAt(secondaryCount - 2).IsBoardCertified == AHC.CD.Entities.MasterData.Enums.YesNoOption.YES.ToString())
                            {
                                pmodel.Specialty_IsBoardCertified1 = secondaryInfo.ElementAt(secondaryCount - 2).IsBoardCertified.ToString();
                            }
                            else if (secondaryInfo.ElementAt(secondaryCount - 2).SpecialtyBoardNotCertifiedDetail != null)
                            {
                                pmodel.ExamDate = ConvertToDateString(secondaryInfo.ElementAt(secondaryCount - 2).SpecialtyBoardNotCertifiedDetail.ExamDate);
                                if (secondaryInfo.ElementAt(secondaryCount - 2).SpecialtyBoardNotCertifiedDetail.ReasonForNotTakingExam == "I do not intend to take exam")
                                {
                                    pmodel.ReasonForNotTakingExam = secondaryInfo.ElementAt(secondaryCount - 2).SpecialtyBoardNotCertifiedDetail.ReasonForNotTakingExam;
                                }
                            }
                            if (secondaryInfo.ElementAt(secondaryCount - 2).SpecialtyBoardCertifiedDetail != null && secondaryInfo.ElementAt(secondaryCount - 2).SpecialtyBoardCertifiedDetail.SpecialtyBoard != null)
                            {
                                pmodel.Specialty_Certificate1 = secondaryInfo.ElementAt(secondaryCount - 2).SpecialtyBoardCertifiedDetail.CertificateNumber;
                                pmodel.Specialty_LastRecertificationDate1 = ConvertToDateString(secondaryInfo.ElementAt(secondaryCount - 2).SpecialtyBoardCertifiedDetail.LastReCerificationDate);
                                pmodel.Specialty_ExpirationDate1 = ConvertToDateString(secondaryInfo.ElementAt(secondaryCount - 2).SpecialtyBoardCertifiedDetail.ExpirationDate);
                                pmodel.Specialty_InitialCertificationDate1 = ConvertToDateString(secondaryInfo.ElementAt(secondaryCount - 1).SpecialtyBoardCertifiedDetail.InitialCertificationDate);

                                if (secondaryInfo.ElementAt(secondaryCount - 2).SpecialtyBoardCertifiedDetail.SpecialtyBoard != null)
                                    pmodel.Specialty_BoardName1 = secondaryInfo.ElementAt(secondaryCount - 2).SpecialtyBoardCertifiedDetail.SpecialtyBoard.Name;
                            }

                            if (secondaryInfo.ElementAt(secondaryCount - 2) != null && secondaryInfo.ElementAt(secondaryCount - 2).Specialty != null)
                            {
                                pmodel.Specialty_SpecialtyName1 = secondaryInfo.ElementAt(secondaryCount - 2).Specialty.Name;
                                pmodel.Specialty_TaxonomyCode1 = secondaryInfo.ElementAt(secondaryCount - 2).Specialty.TaxonomyCode;
                            }
                        }
                        if (secondaryInfo.Count > 2)
                        {
                            if (secondaryInfo.ElementAt(secondaryCount - 3) != null && secondaryInfo.ElementAt(secondaryCount - 3).Specialty != null)
                            {
                                pmodel.Specialty_SpecialtyName2 = secondaryInfo.ElementAt(secondaryCount - 3).Specialty.Name;
                                pmodel.Specialty_TaxonomyCode2 = secondaryInfo.ElementAt(secondaryCount - 3).Specialty.TaxonomyCode;
                            }
                        }
                    }
                }

                #endregion


                #region New Practice Location

                if (profile.PracticeLocationDetails.Count > 0)
                {
                    var primaryPracticeLocation = profile.PracticeLocationDetails.Where(s => s.IsPrimary == AHC.CD.Entities.MasterData.Enums.YesNoOption.YES.ToString() && s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();

                    var secondaryPracticeLocation = profile.PracticeLocationDetails.Where(s => s.IsPrimary == AHC.CD.Entities.MasterData.Enums.YesNoOption.NO.ToString() && s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();

                    var practiceLocationCount = primaryPracticeLocation.Count;

                    var secondaryPracticeLocationCount = secondaryPracticeLocation.Count;

                    if (primaryPracticeLocation.Count > 0 && primaryPracticeLocation != null)
                    {
                        #region Practice Location Details 1
                        if (primaryPracticeLocation[practiceLocationCount - 1].PracticeLocationCorporateName != null)
                        {
                            pmodel.General_OtherPracticeName1 = primaryPracticeLocation[practiceLocationCount - 1].PracticeLocationCorporateName;

                        }
                        if (primaryPracticeLocation[practiceLocationCount - 1].PrimaryTaxId != null)
                        {
                            pmodel.General_PrimaryTaxId1 = primaryPracticeLocation[practiceLocationCount - 1].PrimaryTaxId;
                        }
                        if (primaryPracticeLocation[practiceLocationCount - 1].StartDate != null)
                        {
                            pmodel.General_StartDate1 = ConvertToDateString(primaryPracticeLocation[practiceLocationCount - 1].StartDate);
                        }
                        #endregion

                        #region Practice Location 1

                        if (practiceLocationCount > 0 && primaryPracticeLocation[practiceLocationCount - 1] != null)
                        {

                            #region Address 1

                            if (primaryPracticeLocation[practiceLocationCount - 1].Facility != null)
                            {
                                pmodel.General_PracticeLocationAddress1 = primaryPracticeLocation[practiceLocationCount - 1].Facility.Street + " " + primaryPracticeLocation[practiceLocationCount - 1].Facility.City + ", " + primaryPracticeLocation[practiceLocationCount - 1].Facility.County + ", " + primaryPracticeLocation[practiceLocationCount - 1].Facility.State + ", " + primaryPracticeLocation[practiceLocationCount - 1].Facility.ZipCode;

                                if (primaryPracticeLocation[practiceLocationCount - 1].Facility.Telephone != null)
                                {
                                    pmodel.General_PhoneFirstThreeDigit1 = primaryPracticeLocation[practiceLocationCount - 1].Facility.Telephone.Substring(0, primaryPracticeLocation[practiceLocationCount - 1].Facility.Telephone.Length - 7);
                                    pmodel.General_PhoneSecondThreeDigit1 = primaryPracticeLocation[practiceLocationCount - 1].Facility.Telephone.Substring(3, 3);
                                    pmodel.General_PhoneLastFourDigit1 = primaryPracticeLocation[practiceLocationCount - 1].Facility.Telephone.Substring(6);

                                    pmodel.General_Phone1 = pmodel.General_PhoneFirstThreeDigit1 + "-" + pmodel.General_PhoneSecondThreeDigit1 + "-" + pmodel.General_PhoneLastFourDigit1;
                                    pmodel.LocationAddress_Line3 = "Phone : " + pmodel.General_Phone1;
                                }


                                pmodel.General_Email1 = primaryPracticeLocation[practiceLocationCount - 1].Facility.EmailAddress;

                                if (primaryPracticeLocation[practiceLocationCount - 1].Facility.Fax != null)
                                {
                                    //pmodel.General_Fax1 = practiceLocation[practiceLocationCount - 1].Facility.Fax.Substring(3, practiceLocation[practiceLocationCount - 1].Facility.Fax.Length - 3);

                                    pmodel.General_FaxFirstThreeDigit1 = primaryPracticeLocation[practiceLocationCount - 1].Facility.Fax.Substring(0, primaryPracticeLocation[practiceLocationCount - 1].Facility.Fax.Length - 7);
                                    pmodel.General_FaxSecondThreeDigit1 = primaryPracticeLocation[practiceLocationCount - 1].Facility.Fax.Substring(3, 3);
                                    pmodel.General_FaxLastFourDigit1 = primaryPracticeLocation[practiceLocationCount - 1].Facility.Fax.Substring(6);

                                    pmodel.General_Fax1 = pmodel.General_FaxFirstThreeDigit1 + "-" + pmodel.General_FaxSecondThreeDigit1 + "-" + pmodel.General_FaxLastFourDigit1;
                                    pmodel.LocationAddress_Line3 = pmodel.LocationAddress_Line3 + " " + "Fax : " + pmodel.General_Fax1;
                                }

                                pmodel.General_AccessGroupName1 = "Access Healthcare Physicians, LLC";
                                pmodel.General_Access2GroupName1 = "Access 2 Healthcare Physicians, LLC";

                                pmodel.General_AccessGroupTaxId1 = "451444883";
                                pmodel.General_Access2GroupTaxId1 = "451024515";

                                pmodel.General_PracticeOrCorporateName1 = primaryPracticeLocation[practiceLocationCount - 1].Facility.Name;
                                pmodel.General_FacilityName1 = primaryPracticeLocation[practiceLocationCount - 1].Facility.FacilityName;
                                pmodel.General_Suite1 = primaryPracticeLocation[practiceLocationCount - 1].Facility.Building;
                                pmodel.General_Street1 = primaryPracticeLocation[practiceLocationCount - 1].Facility.Street;
                                pmodel.General_City1 = primaryPracticeLocation[practiceLocationCount - 1].Facility.City;
                                pmodel.General_State1 = primaryPracticeLocation[practiceLocationCount - 1].Facility.State;
                                pmodel.General_ZipCode1 = primaryPracticeLocation[practiceLocationCount - 1].Facility.ZipCode;
                                pmodel.General_Country1 = primaryPracticeLocation[practiceLocationCount - 1].Facility.Country;
                                pmodel.General_County1 = primaryPracticeLocation[practiceLocationCount - 1].Facility.County;
                                pmodel.General_IsCurrentlyPracticing1 = primaryPracticeLocation[practiceLocationCount - 1].CurrentlyPracticingAtThisAddress;
                                pmodel.LocationAddress_Line1 = pmodel.General_Street1 + " " + pmodel.General_Suite1;
                                pmodel.LocationAddress_Line2 = pmodel.General_City1 + ", " + pmodel.General_State1 + " " + pmodel.General_ZipCode1;
                                pmodel.General_City1State1 = pmodel.General_City1 + ", " + pmodel.General_State1 + ", " + pmodel.General_ZipCode1;
                                pmodel.General_FacilityPracticeName1 = pmodel.General_FacilityName1 + " ," + pmodel.General_PracticeOrCorporateName1;


                                #region Languages

                                if (primaryPracticeLocation[practiceLocationCount - 1].Facility.FacilityDetail != null)
                                {
                                    if (primaryPracticeLocation[practiceLocationCount - 1].Facility.FacilityDetail.Language != null)
                                    {
                                        var languages = primaryPracticeLocation[practiceLocationCount - 1].Facility.FacilityDetail.Language.NonEnglishLanguages.Where(l => l.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();
                                        if (languages.Count > 0)
                                        {
                                            foreach (var language in languages)
                                            {
                                                if (language != null)
                                                    pmodel.Languages_Known1 += language.Language + ",";
                                            }
                                        }
                                    }

                                }

                                #endregion
                            }

                            #endregion

                            #region Primary Credentialing Contact Information 1

                            if (practiceLocationCount > 0 && primaryPracticeLocation[practiceLocationCount - 1].PrimaryCredentialingContactPerson != null)
                            {

                                if (primaryPracticeLocation[practiceLocationCount - 1].PrimaryCredentialingContactPerson.MiddleName != null)
                                {
                                    pmodel.PrimaryCredContact_FullName = primaryPracticeLocation[practiceLocationCount - 1].PrimaryCredentialingContactPerson.FirstName + " " + primaryPracticeLocation[practiceLocationCount - 1].PrimaryCredentialingContactPerson.MiddleName + " " + primaryPracticeLocation[practiceLocationCount - 1].PrimaryCredentialingContactPerson.LastName;
                                }
                                else
                                {
                                    pmodel.PrimaryCredContact_FullName = primaryPracticeLocation[practiceLocationCount - 1].PrimaryCredentialingContactPerson.FirstName + " " + primaryPracticeLocation[practiceLocationCount - 1].PrimaryCredentialingContactPerson.LastName;
                                }
                                pmodel.PrimaryCredContact_FirstName = primaryPracticeLocation[practiceLocationCount - 1].PrimaryCredentialingContactPerson.FirstName;
                                pmodel.PrimaryCredContact_MI = primaryPracticeLocation[practiceLocationCount - 1].PrimaryCredentialingContactPerson.MiddleName;
                                pmodel.PrimaryCredContact_LastName = primaryPracticeLocation[practiceLocationCount - 1].PrimaryCredentialingContactPerson.LastName;

                                pmodel.PrimaryCredContact_Street = primaryPracticeLocation[practiceLocationCount - 1].PrimaryCredentialingContactPerson.Street;
                                pmodel.PrimaryCredContact_Suite = primaryPracticeLocation[practiceLocationCount - 1].PrimaryCredentialingContactPerson.Building;
                                pmodel.PrimaryCredContact_City = primaryPracticeLocation[practiceLocationCount - 1].PrimaryCredentialingContactPerson.City;
                                pmodel.PrimaryCredContact_State = primaryPracticeLocation[practiceLocationCount - 1].PrimaryCredentialingContactPerson.State;
                                pmodel.PrimaryCredContact_ZipCode = primaryPracticeLocation[practiceLocationCount - 1].PrimaryCredentialingContactPerson.ZipCode;
                                pmodel.PrimaryCredContact_Phone = primaryPracticeLocation[practiceLocationCount - 1].PrimaryCredentialingContactPerson.Telephone;
                                pmodel.PrimaryCredContact_Fax = primaryPracticeLocation[practiceLocationCount - 1].PrimaryCredentialingContactPerson.FaxNumber;
                                pmodel.PrimaryCredContact_Email = primaryPracticeLocation[practiceLocationCount - 1].PrimaryCredentialingContactPerson.EmailAddress;
                                pmodel.PrimaryCredContact_MobileNumber = primaryPracticeLocation[practiceLocationCount - 1].PrimaryCredentialingContactPerson.MobileNumber;
                                pmodel.PrimaryCredContact_Address1 = pmodel.PrimaryCredContact_Street + ", " + pmodel.PrimaryCredContact_Suite;
                            }
                            #endregion

                            #region Open Practice Status 1

                            if (primaryPracticeLocation[practiceLocationCount - 1].OpenPracticeStatus != null)
                            {
                                pmodel.OpenPractice_AgeLimitations1 = primaryPracticeLocation[practiceLocationCount - 1].OpenPracticeStatus.MinimumAge + " - " + primaryPracticeLocation[practiceLocationCount - 1].OpenPracticeStatus.MaximumAge;
                            }

                            #endregion

                            #region Office Manager 1

                            if (primaryPracticeLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff != null)
                            {
                                if (primaryPracticeLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.MiddleName != null)
                                    pmodel.OfficeManager_Name1 = primaryPracticeLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.FirstName + " " + primaryPracticeLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.MiddleName + " " + primaryPracticeLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.LastName;
                                else
                                    pmodel.OfficeManager_Name1 = primaryPracticeLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.FirstName + " " + primaryPracticeLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.LastName;

                                pmodel.OfficeManager_FirstName1 = primaryPracticeLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.FirstName;
                                pmodel.OfficeManager_MiddleName1 = primaryPracticeLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.MiddleName;
                                pmodel.OfficeManager_LastName1 = primaryPracticeLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.LastName;
                                //pmodel.OfficeManager_Phone1 = primaryPracticeLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.MobileNumber;
                                pmodel.OfficeManager_Email1 = primaryPracticeLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.EmailAddress;
                                pmodel.OfficeManager_PoBoxAddress1 = primaryPracticeLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.POBoxAddress;
                                pmodel.OfficeManager_Building1 = primaryPracticeLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.Building;
                                pmodel.OfficeManager_Street1 = primaryPracticeLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.Street;
                                pmodel.OfficeManager_City1 = primaryPracticeLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.City;
                                pmodel.OfficeManager_State1 = primaryPracticeLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.State;
                                pmodel.OfficeManager_ZipCode1 = primaryPracticeLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.ZipCode;
                                pmodel.OfficeManager_Country1 = primaryPracticeLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.Country;
                                pmodel.OfficeManager_County1 = primaryPracticeLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.County;
                                //pmodel.OfficeManager_Fax1 = practiceLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.FaxNumber;

                                if (primaryPracticeLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.Telephone != null)
                                {
                                    pmodel.OfficeManager_PhoneFirstThreeDigit1 = primaryPracticeLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.Telephone.Substring(0, primaryPracticeLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.Telephone.Length - 7);
                                    pmodel.OfficeManager_PhoneSecondThreeDigit1 = primaryPracticeLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.Telephone.Substring(3, 3);
                                    pmodel.OfficeManager_PhoneLastFourDigit1 = primaryPracticeLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.Telephone.Substring(6);

                                    pmodel.OfficeManager_Phone1 = pmodel.OfficeManager_PhoneFirstThreeDigit1 + "-" + pmodel.OfficeManager_PhoneSecondThreeDigit1 + "-" + pmodel.OfficeManager_PhoneLastFourDigit1;

                                }
                                if (primaryPracticeLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.Fax != null)
                                {
                                    //pmodel.General_Fax1 = practiceLocation[practiceLocationCount - 1].Facility.Fax.Substring(3, practiceLocation[practiceLocationCount - 1].Facility.Fax.Length - 3);

                                    pmodel.OfficeManager_FaxFirstThreeDigit1 = primaryPracticeLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.Fax.Substring(0, primaryPracticeLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.Fax.Length - 7);
                                    pmodel.OfficeManager_FaxSecondThreeDigit1 = primaryPracticeLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.Fax.Substring(3, 3);
                                    pmodel.OfficeManager_FaxLastFourDigit1 = primaryPracticeLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.Fax.Substring(6);

                                    pmodel.OfficeManager_Fax1 = pmodel.OfficeManager_FaxFirstThreeDigit1 + "-" + pmodel.OfficeManager_FaxSecondThreeDigit1 + "-" + pmodel.OfficeManager_FaxLastFourDigit1;


                                }
                            }

                            #endregion

                            #region Billing Contact 1

                            if (primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson != null)
                            {
                                if (primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson.MiddleName != null)
                                    pmodel.BillingContact_Name1 = primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson.FirstName + " " + primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson.MiddleName + " " + primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson.LastName;
                                else
                                    pmodel.BillingContact_Name1 = primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson.FirstName + " " + primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson.LastName;

                                pmodel.BillingContact_FirstName1 = primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson.FirstName;
                                pmodel.BillingContact_MiddleName1 = primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson.MiddleName;
                                pmodel.BillingContact_LastName1 = primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson.LastName;
                                pmodel.BillingContact_Email1 = primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson.EmailAddress;
                                //pmodel.BillingContact_Phone1 = practiceLocation[practiceLocationCount - 1].BillingContactPerson.MobileNumber;
                                //pmodel.BillingContact_Fax1 = practiceLocation[practiceLocationCount - 1].BillingContactPerson.FaxNumber;
                                pmodel.BillingContact_POBoxAddress1 = primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson.POBoxAddress;
                                pmodel.BillingContact_Suite1 = primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson.Building;
                                pmodel.BillingContact_Street1 = primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson.Street;
                                pmodel.BillingContact_City1 = primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson.City;
                                pmodel.BillingContact_State1 = primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson.State;
                                pmodel.BillingContact_ZipCode1 = primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson.ZipCode;
                                pmodel.BillingContact_Country1 = primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson.Country;
                                pmodel.BillingContact_County1 = primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson.County;

                                pmodel.BillingContact_Line1 = pmodel.BillingContact_Street1 + " " + pmodel.BillingContact_Suite1;
                                pmodel.BillingContact_Line2 = pmodel.BillingContact_City1 + ", " + pmodel.BillingContact_State1 + " " + pmodel.BillingContact_County1 + " " + pmodel.BillingContact_ZipCode1;
                                pmodel.BillingContact_City1State1 = pmodel.BillingContact_City1 + " ," + pmodel.BillingContact_State1 + ", " + pmodel.BillingContact_ZipCode1;

                                if (primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson.Telephone != null)
                                {
                                    pmodel.BillingContact_PhoneFirstThreeDigit1 = primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson.Telephone.Substring(0, primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson.Telephone.Length - 7);
                                    pmodel.BillingContact_PhoneSecondThreeDigit1 = primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson.Telephone.Substring(3, 3);
                                    pmodel.BillingContact_PhoneLastFourDigit1 = primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson.Telephone.Substring(6);

                                    pmodel.BillingContact_Phone1 = pmodel.BillingContact_PhoneFirstThreeDigit1 + "-" + pmodel.BillingContact_PhoneSecondThreeDigit1 + "-" + pmodel.BillingContact_PhoneLastFourDigit1;
                                }

                                if (primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson.Fax != null)
                                {
                                    //pmodel.General_Fax1 = practiceLocation[practiceLocationCount - 1].Facility.Fax.Substring(3, practiceLocation[practiceLocationCount - 1].Facility.Fax.Length - 3);

                                    pmodel.BillingContact_FaxFirstThreeDigit1 = primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson.Fax.Substring(0, primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson.Fax.Length - 7);
                                    pmodel.BillingContact_FaxSecondThreeDigit1 = primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson.Fax.Substring(3, 3);
                                    pmodel.BillingContact_FaxLastFourDigit1 = primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson.Fax.Substring(6);

                                    pmodel.BillingContact_Fax1 = pmodel.BillingContact_FaxFirstThreeDigit1 + "-" + pmodel.BillingContact_FaxSecondThreeDigit1 + "-" + pmodel.BillingContact_FaxLastFourDigit1;
                                }
                            }

                            #endregion

                            #region Payment and Remittance 1

                            if (primaryPracticeLocation[practiceLocationCount - 1].PaymentAndRemittance != null)
                            {
                                if (primaryPracticeLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson != null)
                                {
                                    if (primaryPracticeLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.MiddleName != null)
                                        pmodel.PaymentRemittance_Name1 = primaryPracticeLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.FirstName + " " + primaryPracticeLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.MiddleName + " " + primaryPracticeLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.LastName;
                                    else
                                        pmodel.PaymentRemittance_Name1 = primaryPracticeLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.FirstName + " " + primaryPracticeLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.LastName;

                                    pmodel.PaymentRemittance_FirstName1 = primaryPracticeLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.FirstName;
                                    pmodel.PaymentRemittance_MiddleName1 = primaryPracticeLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.MiddleName;
                                    pmodel.PaymentRemittance_LastName1 = primaryPracticeLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.LastName;
                                    pmodel.PaymentRemittance_Email1 = primaryPracticeLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.EmailAddress;
                                    pmodel.PaymentRemittance_POBoxAddress1 = primaryPracticeLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.POBoxAddress;
                                    pmodel.PaymentRemittance_Suite1 = primaryPracticeLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.Building;
                                    pmodel.PaymentRemittance_Street1 = primaryPracticeLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.Street;
                                    pmodel.PaymentRemittance_City1 = primaryPracticeLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.City;
                                    pmodel.PaymentRemittance_State1 = primaryPracticeLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.State;
                                    pmodel.PaymentRemittance_ZipCode1 = primaryPracticeLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.ZipCode;
                                    pmodel.PaymentRemittance_Country1 = primaryPracticeLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.Country;
                                    pmodel.PaymentRemittance_County1 = primaryPracticeLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.County;

                                    if (primaryPracticeLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.Telephone != null)
                                    {
                                        pmodel.PaymentRemittance_PhoneFirstThreeDigit1 = primaryPracticeLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.Telephone.Substring(0, primaryPracticeLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.Telephone.Length - 7);
                                        pmodel.PaymentRemittance_PhoneSecondThreeDigit1 = primaryPracticeLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.Telephone.Substring(3, 3);
                                        pmodel.PaymentRemittance_PhoneLastFourDigit1 = primaryPracticeLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.Telephone.Substring(6);

                                        pmodel.PaymentRemittance_Phone1 = pmodel.PaymentRemittance_PhoneFirstThreeDigit1 + "-" + pmodel.PaymentRemittance_PhoneSecondThreeDigit1 + "-" + pmodel.PaymentRemittance_PhoneLastFourDigit1;
                                        if (pmodel.PaymentRemittance_Phone1.Length > 13)
                                        {
                                            pmodel.General_Phone = primaryPracticeLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.Telephone;
                                        }
                                    }

                                    if (primaryPracticeLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.Fax != null)
                                    {
                                        //pmodel.General_Fax1 = practiceLocation[practiceLocationCount - 1].Facility.Fax.Substring(3, practiceLocation[practiceLocationCount - 1].Facility.Fax.Length - 3);

                                        pmodel.PaymentRemittance_FaxFirstThreeDigit1 = primaryPracticeLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.Fax.Substring(0, primaryPracticeLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.Fax.Length - 7);
                                        pmodel.PaymentRemittance_FaxSecondThreeDigit1 = primaryPracticeLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.Fax.Substring(3, 3);
                                        pmodel.PaymentRemittance_FaxLastFourDigit1 = primaryPracticeLocation[practiceLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.Fax.Substring(6);

                                        pmodel.PaymentRemittance_Fax1 = pmodel.PaymentRemittance_FaxFirstThreeDigit1 + "-" + pmodel.PaymentRemittance_FaxSecondThreeDigit1 + "-" + pmodel.PaymentRemittance_FaxLastFourDigit1;
                                    }
                                }
                                pmodel.PaymentRemittance_ElectronicBillCapability1 = primaryPracticeLocation[practiceLocationCount - 1].PaymentAndRemittance.ElectronicBillingCapability;
                                pmodel.PaymentRemittance_BillingDepartment1 = primaryPracticeLocation[practiceLocationCount - 1].PaymentAndRemittance.BillingDepartment;
                                pmodel.PaymentRemittance_ChekPayableTo1 = primaryPracticeLocation[practiceLocationCount - 1].PaymentAndRemittance.CheckPayableTo;
                                pmodel.PaymentRemittance_Office1 = primaryPracticeLocation[practiceLocationCount - 1].PaymentAndRemittance.Office;
                            }

                            #endregion

                            #region Office Hours 1

                            if (primaryPracticeLocation[practiceLocationCount - 1].OfficeHour != null)
                            {
                                if (primaryPracticeLocation[practiceLocationCount - 1].OfficeHour.PracticeDays.Count > 0 && primaryPracticeLocation[practiceLocationCount - 1].OfficeHour.PracticeDays.First().DailyHours.Count > 0)
                                {

                                    pmodel.OfficeHour_StartMonday1 = ConvertTimeFormat(primaryPracticeLocation[practiceLocationCount - 1].OfficeHour.PracticeDays.ElementAt(0).DailyHours.First().StartTime);
                                    pmodel.OfficeHour_EndMonday1 = ConvertTimeFormat(primaryPracticeLocation[practiceLocationCount - 1].OfficeHour.PracticeDays.ElementAt(0).DailyHours.First().EndTime);

                                    pmodel.OfficeHour_Monday1 = pmodel.OfficeHour_StartMonday1 + " - " + pmodel.OfficeHour_EndMonday1;

                                    pmodel.OfficeHour_StartTuesday1 = ConvertTimeFormat(primaryPracticeLocation[practiceLocationCount - 1].OfficeHour.PracticeDays.ElementAt(1).DailyHours.First().StartTime);
                                    pmodel.OfficeHour_EndTuesday1 = ConvertTimeFormat(primaryPracticeLocation[practiceLocationCount - 1].OfficeHour.PracticeDays.ElementAt(1).DailyHours.First().EndTime);

                                    pmodel.OfficeHour_Tuesday1 = pmodel.OfficeHour_StartTuesday1 + " - " + pmodel.OfficeHour_EndTuesday1;

                                    pmodel.OfficeHour_StartWednesday1 = ConvertTimeFormat(primaryPracticeLocation[practiceLocationCount - 1].OfficeHour.PracticeDays.ElementAt(2).DailyHours.First().StartTime);
                                    pmodel.OfficeHour_EndWednesday1 = ConvertTimeFormat(primaryPracticeLocation[practiceLocationCount - 1].OfficeHour.PracticeDays.ElementAt(2).DailyHours.First().EndTime);

                                    pmodel.OfficeHour_Wednesday1 = pmodel.OfficeHour_StartWednesday1 + " - " + pmodel.OfficeHour_EndWednesday1;

                                    pmodel.OfficeHour_StartThursday1 = ConvertTimeFormat(primaryPracticeLocation[practiceLocationCount - 1].OfficeHour.PracticeDays.ElementAt(3).DailyHours.First().StartTime);
                                    pmodel.OfficeHour_EndThursday1 = ConvertTimeFormat(primaryPracticeLocation[practiceLocationCount - 1].OfficeHour.PracticeDays.ElementAt(3).DailyHours.First().EndTime);

                                    pmodel.OfficeHour_Thursday1 = pmodel.OfficeHour_StartThursday1 + " - " + pmodel.OfficeHour_EndThursday1;

                                    pmodel.OfficeHour_StartFriday1 = ConvertTimeFormat(primaryPracticeLocation[practiceLocationCount - 1].OfficeHour.PracticeDays.ElementAt(4).DailyHours.First().StartTime);
                                    pmodel.OfficeHour_EndFriday1 = ConvertTimeFormat(primaryPracticeLocation[practiceLocationCount - 1].OfficeHour.PracticeDays.ElementAt(4).DailyHours.First().EndTime);

                                    pmodel.OfficeHour_Friday1 = pmodel.OfficeHour_StartFriday1 + " - " + pmodel.OfficeHour_EndFriday1;

                                    pmodel.OfficeHour_StartSaturday1 = ConvertTimeFormat(primaryPracticeLocation[practiceLocationCount - 1].OfficeHour.PracticeDays.ElementAt(5).DailyHours.First().StartTime);
                                    pmodel.OfficeHour_EndSaturday1 = ConvertTimeFormat(primaryPracticeLocation[practiceLocationCount - 1].OfficeHour.PracticeDays.ElementAt(5).DailyHours.First().EndTime);

                                    pmodel.OfficeHour_Saturday1 = pmodel.OfficeHour_StartSaturday1 + " - " + pmodel.OfficeHour_EndSaturday1;

                                    pmodel.OfficeHour_StartSunday1 = ConvertTimeFormat(primaryPracticeLocation[practiceLocationCount - 1].OfficeHour.PracticeDays.ElementAt(6).DailyHours.First().StartTime);
                                    pmodel.OfficeHour_EndSunday1 = ConvertTimeFormat(primaryPracticeLocation[practiceLocationCount - 1].OfficeHour.PracticeDays.ElementAt(6).DailyHours.First().EndTime);

                                    pmodel.OfficeHour_Sunday1 = pmodel.OfficeHour_StartSunday1 + " - " + pmodel.OfficeHour_EndSunday1;
                                }
                            }

                            #endregion

                            #region Supervising Provider 1

                            var supervisingProvider = primaryPracticeLocation[practiceLocationCount - 1].PracticeProviders.
                            Where(s => s.Practice == AHC.CD.Entities.MasterData.Enums.PracticeType.Supervisor.ToString() &&
                                s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();

                            var supervisorCount = supervisingProvider.Count;

                            if (supervisorCount > 0)
                            {
                                pmodel.CoveringColleague_FirstName1 = supervisingProvider.ElementAt(supervisorCount - 1).FirstName;
                                pmodel.CoveringColleague_MiddleName1 = supervisingProvider.ElementAt(supervisorCount - 1).MiddleName;
                                pmodel.CoveringColleague_LastName1 = supervisingProvider.ElementAt(supervisorCount - 1).LastName;

                                if (pmodel.CoveringColleague_MiddleName1 != null)
                                    pmodel.CoveringColleague_FullName1 = pmodel.CoveringColleague_FirstName1 + " " + pmodel.CoveringColleague_MiddleName1 + " " + pmodel.CoveringColleague_LastName1;
                                else
                                    pmodel.CoveringColleague_FullName1 = pmodel.CoveringColleague_FirstName1 + " " + pmodel.CoveringColleague_LastName1;


                                if (supervisingProvider.ElementAt(supervisorCount - 1).Telephone != null)
                                {
                                    pmodel.CoveringColleague_PhoneFirstThreeDigit1 = supervisingProvider.ElementAt(supervisorCount - 1).Telephone.Substring(0, supervisingProvider.ElementAt(supervisorCount - 1).Telephone.Length - 7);
                                    pmodel.CoveringColleague_PhoneSecondThreeDigit1 = supervisingProvider.ElementAt(supervisorCount - 1).Telephone.Substring(3, 3);
                                    pmodel.CoveringColleague_PhoneLastFourDigit1 = supervisingProvider.ElementAt(supervisorCount - 1).Telephone.Substring(6);

                                    pmodel.CoveringColleague_PhoneNumber1 = pmodel.CoveringColleague_PhoneFirstThreeDigit1 + "-" + pmodel.CoveringColleague_PhoneSecondThreeDigit1 + "-" + pmodel.CoveringColleague_PhoneLastFourDigit1;

                                }

                                var specialities = supervisingProvider.ElementAt(supervisorCount - 1).PracticeProviderSpecialties.Where(s => s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString()).ToList();

                                if (specialities.Count > 0)
                                {
                                    if (specialities.ElementAt(specialities.Count - 1).Specialty != null)
                                        pmodel.CoveringColleague_Specialty1 = specialities.ElementAt(specialities.Count - 1).Specialty.Name;
                                }
                            }

                            #endregion

                            #region Covering Colleagues/Partners 1

                            var patners = primaryPracticeLocation[practiceLocationCount - 1].PracticeProviders.
                            Where(s => s.Practice == AHC.CD.Entities.MasterData.Enums.PracticeType.CoveringColleague.ToString() &&
                                s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();

                            var patnersCount = patners.Count;

                            if (patnersCount > 0)
                            {
                                pmodel.Patners_FirstName1 = patners.ElementAt(patnersCount - 1).FirstName;
                                pmodel.Patners_MiddleName1 = patners.ElementAt(patnersCount - 1).MiddleName;
                                pmodel.Patners_LastName1 = patners.ElementAt(patnersCount - 1).LastName;

                                if (pmodel.Patners_MiddleName1 != null)
                                    pmodel.Patners_FullName1 = pmodel.Patners_FirstName1 + " " + pmodel.Patners_MiddleName1 + " " + pmodel.Patners_LastName1;
                                else
                                    pmodel.Patners_FullName1 = pmodel.Patners_FirstName1 + " " + pmodel.Patners_LastName1;

                                if (patners.ElementAt(patnersCount - 1).Telephone != null)
                                {
                                    pmodel.Patners_PhoneFirstThreeDigit1 = patners.ElementAt(patnersCount - 1).Telephone.Substring(0, patners.ElementAt(patnersCount - 1).Telephone.Length - 7);
                                    pmodel.Patners_PhoneSecondThreeDigit1 = patners.ElementAt(patnersCount - 1).Telephone.Substring(3, 3);
                                    pmodel.Patners_PhoneLastFourDigit1 = patners.ElementAt(patnersCount - 1).Telephone.Substring(6);

                                    pmodel.Patners_PhoneNumber1 = pmodel.CoveringColleague_PhoneFirstThreeDigit1 + "-" + pmodel.CoveringColleague_PhoneSecondThreeDigit1 + "-" + pmodel.CoveringColleague_PhoneLastFourDigit1;

                                }

                                var specialities = patners.ElementAt(patnersCount - 1).PracticeProviderSpecialties.Where(s => s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString()).ToList();

                                if (specialities.Count > 0)
                                {
                                    if (specialities.ElementAt(specialities.Count - 1).Specialty != null)
                                        pmodel.Patners_Specialty1 = specialities.ElementAt(specialities.Count - 1).Specialty.Name;
                                }
                            }

                            #endregion

                            #region Primary Practice Location Specific


                            #region   Primary Practice Location Details 1
                            if (primaryPracticeLocation[practiceLocationCount - 1].PracticeLocationCorporateName != null)
                            {
                                pmodel.General_OtherPracticeName = primaryPracticeLocation[practiceLocationCount - 1].PracticeLocationCorporateName;

                            }
                            if (primaryPracticeLocation[practiceLocationCount - 1].PrimaryTaxId != null)
                            {
                                pmodel.General_PrimaryTaxId = primaryPracticeLocation[practiceLocationCount - 1].PrimaryTaxId;
                            }
                            if (primaryPracticeLocation[practiceLocationCount - 1].StartDate != null)
                            {
                                pmodel.General_StartDate = ConvertToDateString(primaryPracticeLocation[practiceLocationCount - 1].StartDate);
                            }
                            #endregion

                            #region Primary Address

                            if (primaryPracticeLocation[practiceLocationCount - 1].Facility != null)
                            {
                                pmodel.General_PracticeLocationAddress = primaryPracticeLocation[practiceLocationCount - 1].Facility.Street + " " + primaryPracticeLocation[practiceLocationCount - 1].Facility.City + ", " + primaryPracticeLocation[practiceLocationCount - 1].Facility.County + ", " + primaryPracticeLocation[practiceLocationCount - 1].Facility.State + ", " + primaryPracticeLocation[practiceLocationCount - 1].Facility.ZipCode;

                                if (primaryPracticeLocation[practiceLocationCount - 1].Facility.Telephone != null)
                                {
                                    pmodel.General_PhoneFirstThreeDigit = primaryPracticeLocation[practiceLocationCount - 1].Facility.Telephone.Substring(0, primaryPracticeLocation[practiceLocationCount - 1].Facility.Telephone.Length - 7);
                                    pmodel.General_PhoneSecondThreeDigit = primaryPracticeLocation[practiceLocationCount - 1].Facility.Telephone.Substring(3, 3);
                                    pmodel.General_PhoneLastFourDigit = primaryPracticeLocation[practiceLocationCount - 1].Facility.Telephone.Substring(6);

                                    pmodel.General_Phone = pmodel.General_PhoneFirstThreeDigit + "-" + pmodel.General_PhoneSecondThreeDigit + "-" + pmodel.General_PhoneLastFourDigit;
                                    pmodel.Primary_LocationAddress_Line3 = "Phone : " + pmodel.General_Phone;
                                }


                                pmodel.General_Email = primaryPracticeLocation[practiceLocationCount - 1].Facility.EmailAddress;

                                if (primaryPracticeLocation[practiceLocationCount - 1].Facility.Fax != null)
                                {
                                    //pmodel.General_Fax = practiceLocation[practiceLocationCount - 1].Facility.Fax.Substring(3, practiceLocation[practiceLocationCount - 1].Facility.Fax.Length - 3);

                                    pmodel.General_FaxFirstThreeDigit = primaryPracticeLocation[practiceLocationCount - 1].Facility.Fax.Substring(0, primaryPracticeLocation[practiceLocationCount - 1].Facility.Fax.Length - 7);
                                    pmodel.General_FaxSecondThreeDigit = primaryPracticeLocation[practiceLocationCount - 1].Facility.Fax.Substring(3, 3);
                                    pmodel.General_FaxLastFourDigit = primaryPracticeLocation[practiceLocationCount - 1].Facility.Fax.Substring(6);

                                    pmodel.General_Fax = pmodel.General_FaxFirstThreeDigit + "-" + pmodel.General_FaxSecondThreeDigit + "-" + pmodel.General_FaxLastFourDigit;
                                    if (pmodel.General_Fax.Length > 13)
                                    {
                                        pmodel.General_Fax = primaryPracticeLocation[practiceLocationCount - 1].Facility.Fax;
                                    }

                                    pmodel.Primary_LocationAddress_Line3 = pmodel.Primary_LocationAddress_Line3 + " " + "Fax : " + pmodel.General_Fax;
                                }

                                pmodel.General_AccessGroupName = "Access Healthcare Physicians, LLC";
                                pmodel.General_Access2GroupName = "Access 2 Healthcare Physicians, LLC";

                                pmodel.General_AccessGroupTaxId = "451444883";
                                pmodel.General_Access2GroupTaxId = "451024515";

                                pmodel.General_PracticeOrCorporateName = primaryPracticeLocation[practiceLocationCount - 1].Facility.Name;
                                pmodel.General_FacilityName = primaryPracticeLocation[practiceLocationCount - 1].Facility.FacilityName;
                                pmodel.General_Suite = primaryPracticeLocation[practiceLocationCount - 1].Facility.Building;
                                pmodel.General_Street = primaryPracticeLocation[practiceLocationCount - 1].Facility.Street;
                                pmodel.General_City = primaryPracticeLocation[practiceLocationCount - 1].Facility.City;
                                pmodel.General_State = primaryPracticeLocation[practiceLocationCount - 1].Facility.State;
                                pmodel.General_ZipCode = primaryPracticeLocation[practiceLocationCount - 1].Facility.ZipCode;
                                pmodel.General_Country = primaryPracticeLocation[practiceLocationCount - 1].Facility.Country;
                                pmodel.General_County = primaryPracticeLocation[practiceLocationCount - 1].Facility.County;
                                pmodel.General_IsCurrentlyPracticing = primaryPracticeLocation[practiceLocationCount - 1].CurrentlyPracticingAtThisAddress;
                                pmodel.Primary_LocationAddress_Line1 = pmodel.General_Street + " " + pmodel.General_Suite;
                                pmodel.Primary_LocationAddress_Line2 = pmodel.General_City + ", " + pmodel.General_State + " " + pmodel.General_ZipCode;
                                pmodel.General_CityState = pmodel.General_City + ", " + pmodel.General_State + ", " + pmodel.General_ZipCode;
                                pmodel.General_FacilityPracticeName = pmodel.General_FacilityName + " ," + pmodel.General_PracticeOrCorporateName;


                            }

                            #endregion

                            #region Primary Open Practice Status

                            if (primaryPracticeLocation[practiceLocationCount - 1].OpenPracticeStatus != null)
                            {
                                pmodel.OpenPractice_AgeLimitations = primaryPracticeLocation[practiceLocationCount - 1].OpenPracticeStatus.MinimumAge + " - " + primaryPracticeLocation[practiceLocationCount - 1].OpenPracticeStatus.MaximumAge;
                            }

                            #endregion

                            #region Primary Billing Contact

                            if (primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson != null)
                            {
                                if (primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson.MiddleName != null)
                                    pmodel.BillingContact_Name = primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson.FirstName + " " + primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson.MiddleName + " " + primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson.LastName;
                                else
                                    pmodel.BillingContact_Name = primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson.FirstName + " " + primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson.LastName;

                                pmodel.BillingContact_FirstName = primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson.FirstName;
                                pmodel.BillingContact_MiddleName = primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson.MiddleName;
                                pmodel.BillingContact_LastName = primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson.LastName;
                                pmodel.BillingContact_Email = primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson.EmailAddress;
                                //pmodel.BillingContact_Phone = practiceLocation[practiceLocationCount - 1].BillingContactPerson.MobileNumber;
                                //pmodel.BillingContact_Fax = practiceLocation[practiceLocationCount - 1].BillingContactPerson.FaxNumber;
                                pmodel.BillingContact_POBoxAddress = primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson.POBoxAddress;
                                pmodel.BillingContact_Suite = primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson.Building;
                                pmodel.BillingContact_Street = primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson.Street;
                                pmodel.BillingContact_City = primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson.City;
                                pmodel.BillingContact_State = primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson.State;
                                pmodel.BillingContact_ZipCode = primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson.ZipCode;
                                pmodel.BillingContact_Country = primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson.Country;
                                pmodel.BillingContact_County = primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson.County;

                                pmodel.BillingContact_Line1 = pmodel.BillingContact_Street + " " + pmodel.BillingContact_Suite;
                                pmodel.BillingContact_Line2 = pmodel.BillingContact_City + ", " + pmodel.BillingContact_State + " " + pmodel.BillingContact_County + " " + pmodel.BillingContact_ZipCode;
                                pmodel.BillingContact_CityState = pmodel.BillingContact_City + " ," + pmodel.BillingContact_State + ", " + pmodel.BillingContact_ZipCode;

                                if (primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson.Telephone != null)
                                {
                                    pmodel.BillingContact_PhoneFirstThreeDigit = primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson.Telephone.Substring(0, primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson.Telephone.Length - 7);
                                    pmodel.BillingContact_PhoneSecondThreeDigit = primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson.Telephone.Substring(3, 3);
                                    pmodel.BillingContact_PhoneLastFourDigit = primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson.Telephone.Substring(6);

                                    pmodel.BillingContact_Phone = pmodel.BillingContact_PhoneFirstThreeDigit + "-" + pmodel.BillingContact_PhoneSecondThreeDigit + "-" + pmodel.BillingContact_PhoneLastFourDigit;
                                }

                                if (primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson.Fax != null)
                                {
                                    //pmodel.General_Fax1 = practiceLocation[practiceLocationCount - 1].Facility.Fax.Substring(3, practiceLocation[practiceLocationCount - 1].Facility.Fax.Length - 3);

                                    pmodel.BillingContact_FaxFirstThreeDigit = primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson.Fax.Substring(0, primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson.Fax.Length - 7);
                                    pmodel.BillingContact_FaxSecondThreeDigit = primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson.Fax.Substring(3, 3);
                                    pmodel.BillingContact_FaxLastFourDigit = primaryPracticeLocation[practiceLocationCount - 1].BillingContactPerson.Fax.Substring(6);

                                    pmodel.BillingContact_Fax = pmodel.BillingContact_FaxFirstThreeDigit + "-" + pmodel.BillingContact_FaxSecondThreeDigit + "-" + pmodel.BillingContact_FaxLastFourDigit;
                                }
                            }

                            #endregion

                            #region Primary Office Hours

                            if (primaryPracticeLocation[practiceLocationCount - 1].OfficeHour != null)
                            {
                                if (primaryPracticeLocation[practiceLocationCount - 1].OfficeHour.PracticeDays.Count > 0 && primaryPracticeLocation[practiceLocationCount - 1].OfficeHour.PracticeDays.First().DailyHours.Count > 0)
                                {

                                    pmodel.OfficeHour_StartMonday = ConvertTimeFormat(primaryPracticeLocation[practiceLocationCount - 1].OfficeHour.PracticeDays.ElementAt(0).DailyHours.First().StartTime);
                                    pmodel.OfficeHour_EndMonday = ConvertTimeFormat(primaryPracticeLocation[practiceLocationCount - 1].OfficeHour.PracticeDays.ElementAt(0).DailyHours.First().EndTime);

                                    pmodel.OfficeHour_Monday = pmodel.OfficeHour_StartMonday + " - " + pmodel.OfficeHour_EndMonday;

                                    pmodel.OfficeHour_StartTuesday = ConvertTimeFormat(primaryPracticeLocation[practiceLocationCount - 1].OfficeHour.PracticeDays.ElementAt(1).DailyHours.First().StartTime);
                                    pmodel.OfficeHour_EndTuesday = ConvertTimeFormat(primaryPracticeLocation[practiceLocationCount - 1].OfficeHour.PracticeDays.ElementAt(1).DailyHours.First().EndTime);

                                    pmodel.OfficeHour_Tuesday = pmodel.OfficeHour_StartTuesday + " - " + pmodel.OfficeHour_EndTuesday;

                                    pmodel.OfficeHour_StartWednesday = ConvertTimeFormat(primaryPracticeLocation[practiceLocationCount - 1].OfficeHour.PracticeDays.ElementAt(2).DailyHours.First().StartTime);
                                    pmodel.OfficeHour_EndWednesday = ConvertTimeFormat(primaryPracticeLocation[practiceLocationCount - 1].OfficeHour.PracticeDays.ElementAt(2).DailyHours.First().EndTime);

                                    pmodel.OfficeHour_Wednesday = pmodel.OfficeHour_StartWednesday + " - " + pmodel.OfficeHour_EndWednesday;

                                    pmodel.OfficeHour_StartThursday = ConvertTimeFormat(primaryPracticeLocation[practiceLocationCount - 1].OfficeHour.PracticeDays.ElementAt(3).DailyHours.First().StartTime);
                                    pmodel.OfficeHour_EndThursday = ConvertTimeFormat(primaryPracticeLocation[practiceLocationCount - 1].OfficeHour.PracticeDays.ElementAt(3).DailyHours.First().EndTime);

                                    pmodel.OfficeHour_Thursday = pmodel.OfficeHour_StartThursday + " - " + pmodel.OfficeHour_EndThursday;

                                    pmodel.OfficeHour_StartFriday = ConvertTimeFormat(primaryPracticeLocation[practiceLocationCount - 1].OfficeHour.PracticeDays.ElementAt(4).DailyHours.First().StartTime);
                                    pmodel.OfficeHour_EndFriday = ConvertTimeFormat(primaryPracticeLocation[practiceLocationCount - 1].OfficeHour.PracticeDays.ElementAt(4).DailyHours.First().EndTime);

                                    pmodel.OfficeHour_Friday = pmodel.OfficeHour_StartFriday + " - " + pmodel.OfficeHour_EndFriday;

                                    pmodel.OfficeHour_StartSaturday = ConvertTimeFormat(primaryPracticeLocation[practiceLocationCount - 1].OfficeHour.PracticeDays.ElementAt(5).DailyHours.First().StartTime);
                                    pmodel.OfficeHour_EndSaturday = ConvertTimeFormat(primaryPracticeLocation[practiceLocationCount - 1].OfficeHour.PracticeDays.ElementAt(5).DailyHours.First().EndTime);

                                    pmodel.OfficeHour_Saturday = pmodel.OfficeHour_StartSaturday + " - " + pmodel.OfficeHour_EndSaturday;

                                    pmodel.OfficeHour_StartSunday = ConvertTimeFormat(primaryPracticeLocation[practiceLocationCount - 1].OfficeHour.PracticeDays.ElementAt(6).DailyHours.First().StartTime);
                                    pmodel.OfficeHour_EndSunday = ConvertTimeFormat(primaryPracticeLocation[practiceLocationCount - 1].OfficeHour.PracticeDays.ElementAt(6).DailyHours.First().EndTime);

                                    pmodel.OfficeHour_Sunday = pmodel.OfficeHour_StartSunday + " - " + pmodel.OfficeHour_EndSunday;
                                }
                            }

                            #endregion

                            #endregion


                        }

                        #endregion

                        #region Secondary Practice Location

                        if (secondaryPracticeLocationCount > 0 && secondaryPracticeLocation != null)
                        {
                            pmodel.Primecare1 = "PRIMECARE";

                            #region Practice Location Details 2
                            if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PracticeLocationCorporateName != null)
                            {
                                pmodel.General_OtherPracticeName2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PracticeLocationCorporateName;

                            }
                            if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PrimaryTaxId != null)
                            {
                                pmodel.General_PrimaryTaxId2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PrimaryTaxId;
                            }
                            if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].StartDate != null)
                            {
                                pmodel.General_StartDate2 = ConvertToDateString(secondaryPracticeLocation[secondaryPracticeLocationCount - 1].StartDate);
                            }
                            #endregion

                            #region TriCare Prime Credentialing Prefilled properties
                            pmodel.CorrespondenceAddressName = "Credentialing";
                            pmodel.CorrespondenceAddressStreetAddress = "14690 Spring Hill Drive";
                            pmodel.CorrespondenceAddressSuiteNumber = "101";
                            pmodel.CorrespondenceAddressCity = "Spring Hill";
                            pmodel.CorrespondenceAddressState = "FL";
                            pmodel.CorrespondenceAddressSZipCode = "34609-8102";
                            pmodel.CorrespondenceAddressCounty = "Hernando";
                            pmodel.CorrespondenceAddressOfficePhoneNumber = "(352)799-0046";
                            pmodel.CorrespondenceAddressOfficeFaxNumber = "(352)799-0042";
                            pmodel.CorrespondenceAddressEmailId = "credentialing@accesshealthcarellc.net";
                            pmodel.BillingAddressName = "Access Health Care Physicians, LLC";
                            pmodel.LegalPracticeName = "Access Health Care Physicians, LLC";
                            pmodel.BillingAddressStreetAddress = "P.O.Box 919469";
                            pmodel.BillingAddressCity = "Orlando";
                            pmodel.BillingAddressState = "FL";
                            pmodel.BillingAddressZipCode = "32891-9469";
                            pmodel.BillingAddressCounty = "Orange";
                            pmodel.BillingAddressOfficePhoneNumber = "(727)823-2188";
                            pmodel.BillingAddressOfficeFaxNumber = "(727)828-0723";
                            pmodel.BillingAddressSuiteNumber = "";
                            pmodel.BillingAddressEmailId = "generalinquiry@medenet.net";
                            pmodel.OfficeHoursMondayFrom = "8 AM";
                            pmodel.OfficeHourMondayTo = "5 PM";
                            pmodel.OfficeHoursTuesdayFrom = "8 AM";
                            pmodel.OfficeHourTuesdayTo = "5 PM";
                            pmodel.OfficeHoursWednesdayFrom = "8 AM";
                            pmodel.OfficeHourWednesdayTo = "5 PM";
                            pmodel.OfficeHoursThursdayFrom = "8 AM";
                            pmodel.OfficeHourThursdayTo = "5 PM";
                            pmodel.OfficeHoursFridayFrom = "8 AM";
                            pmodel.OfficeHourFridayTo = "5 PM";
                            pmodel.Practice_EmailAddress = "credentialing@accesshealthcarellc.net";
                            pmodel.Practice_HowManyTriCarePatientsWillYouAccept = "100";
                            pmodel.Practice_AgeRangeFrom = "18";
                            pmodel.Practice_AgeRangeTo = "100";
                            pmodel.Practice_CredentialingContactName = "Credentialing Department";
                            pmodel.Practice_CredentialingContactEmailAddress = "credentialing@accesshealthcarellc.net";
                            pmodel.Practice_CredentialingContactPhoneNumber = "3527990046";
                            pmodel.Practice_CredentialingContactFax = "3527990042";

                            #endregion

                            #region Address 2

                            if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility != null)
                            {
                                pmodel.General_PracticeOrCorporateName2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.Name;
                                pmodel.General_FacilityName2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.FacilityName;
                                pmodel.General_PracticeLocationAddress2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.Street + " " + secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.City + ", " + secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.State + ", " + secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.ZipCode;
                                //pmodel.General_Phone2 = practiceLocation[secondaryPracticeLocationCount - 1].Facility.MobileNumber;
                                //pmodel.General_Fax2 = practiceLocation[secondaryPracticeLocationCount - 1].Facility.FaxNumber;
                                pmodel.General_Suite2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.Building;
                                pmodel.General_Street2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.Street;
                                pmodel.General_City2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.City;
                                pmodel.General_State2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.State;
                                pmodel.General_ZipCode2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.ZipCode;
                                pmodel.General_Country2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.Country;
                                pmodel.General_County2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.County;
                                pmodel.LocationAddress2_Line1 = pmodel.General_Street2 + "  " + pmodel.General_Suite2;
                                pmodel.LocationAddress2_Line2 = pmodel.General_City2 + ", " + pmodel.General_State2 + " " + pmodel.General_ZipCode2;
                                pmodel.General_FacilityPracticeName2 = pmodel.General_FacilityName2 + ", " + pmodel.General_PracticeOrCorporateName2;

                                pmodel.General_AccessGroupName2 = "Access Healthcare Physicians, LLC";
                                pmodel.General_Access2GroupName2 = "Access 2 Healthcare Physicians, LLC";

                                pmodel.General_AccessGroupTaxId2 = "451444883";
                                pmodel.General_Access2GroupTaxId2 = "451024515";

                                pmodel.Provider_FullNameTitleBCBS = pmodel.Provider_FullNameTitle;
                                pmodel.Provider_FullNameTitle_Additional = pmodel.Provider_FullNameTitle;

                                if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.Telephone != null)
                                {
                                    pmodel.General_PhoneFirstThreeDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.Telephone.Substring(0, secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.Telephone.Length - 7);
                                    pmodel.General_PhoneSecondThreeDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.Telephone.Substring(3, 3);
                                    pmodel.General_PhoneLastFourDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.Telephone.Substring(6);

                                    pmodel.General_Phone2 = pmodel.General_PhoneFirstThreeDigit2 + "-" + pmodel.General_PhoneSecondThreeDigit2 + "-" + pmodel.General_PhoneLastFourDigit2;
                                    if (pmodel.General_Phone2.Length > 13)
                                    {
                                        pmodel.General_Phone2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.Telephone;
                                    }

                                }


                                pmodel.General_Email2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.EmailAddress;

                                if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.Fax != null)
                                {
                                    //pmodel.General_Fax1 = practiceLocation[secondaryPracticeLocationCount - 1].Facility.Fax.Substring(3, practiceLocation[secondaryPracticeLocationCount - 1].Facility.Fax.Length - 3);

                                    pmodel.General_FaxFirstThreeDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.Fax.Substring(0, secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.Fax.Length - 7);
                                    pmodel.General_FaxSecondThreeDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.Fax.Substring(3, 3);
                                    pmodel.General_FaxLastFourDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.Fax.Substring(6);

                                    pmodel.General_Fax2 = pmodel.General_FaxFirstThreeDigit2 + "-" + pmodel.General_FaxSecondThreeDigit2 + "-" + pmodel.General_FaxLastFourDigit2;
                                    if(pmodel.General_Fax2.Length>13)
                                    {
                                        pmodel.General_Fax2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.Fax;
                                    }
                                }

                                #region Languages

                                if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.FacilityDetail != null && secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.FacilityDetail.Language != null)
                                {
                                    var languages = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.FacilityDetail.Language.NonEnglishLanguages.Where(l => l.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();
                                    if (languages.Count > 0)
                                    {
                                        foreach (var language in languages)
                                        {
                                            if (language != null)
                                                pmodel.Languages_Known2 += language.Language + ",";
                                        }
                                    }
                                }
                                #endregion
                            }

                            #endregion

                            #region Office Manager 2

                            if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff != null)
                            {
                                if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.MiddleName != null)
                                    pmodel.OfficeManager_Name2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.FirstName + " " + secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.MiddleName + " " + secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.LastName;
                                else
                                    pmodel.OfficeManager_Name2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.FirstName + " " + secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.LastName;

                                //pmodel.OfficeManager_Name2 = practiceLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.FirstName + practiceLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.MiddleName + practiceLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.LastName;
                                pmodel.OfficeManager_Email2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.EmailAddress;
                                pmodel.OfficeManager_PoBoxAddress2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.POBoxAddress;
                                pmodel.OfficeManager_Building2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.Building;
                                pmodel.OfficeManager_Street2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.Street;
                                pmodel.OfficeManager_City2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.City;
                                pmodel.OfficeManager_State2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.State;
                                pmodel.OfficeManager_ZipCode2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.ZipCode;
                                pmodel.OfficeManager_Country2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.Country;
                                pmodel.OfficeManager_County2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.County;

                                if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.Telephone != null)
                                {
                                    pmodel.OfficeManager_PhoneFirstThreeDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.Telephone.Substring(0, secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.Telephone.Length - 7);
                                    pmodel.OfficeManager_PhoneSecondThreeDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.Telephone.Substring(3, 3);
                                    pmodel.OfficeManager_PhoneLastFourDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.Telephone.Substring(6);

                                    pmodel.OfficeManager_Phone2 = pmodel.OfficeManager_PhoneFirstThreeDigit2 + "-" + pmodel.OfficeManager_PhoneSecondThreeDigit2 + "-" + pmodel.OfficeManager_PhoneLastFourDigit2;
                                }
                                if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.Fax != null)
                                {

                                    pmodel.OfficeManager_FaxFirstThreeDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.Fax.Substring(0, secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.Fax.Length - 7);
                                    pmodel.OfficeManager_FaxSecondThreeDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.Fax.Substring(3, 3);
                                    pmodel.OfficeManager_FaxLastFourDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.Fax.Substring(6);

                                    pmodel.OfficeManager_Fax2 = pmodel.OfficeManager_FaxFirstThreeDigit2 + "-" + pmodel.OfficeManager_FaxSecondThreeDigit2 + "-" + pmodel.OfficeManager_FaxLastFourDigit2;
                                }
                            }

                            #endregion

                            #region Billing Contact 2

                            if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson != null)
                            {
                                if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.MiddleName != null)
                                    pmodel.BillingContact_Name2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.FirstName + " " + secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.MiddleName + " " + secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.LastName;
                                else
                                    pmodel.BillingContact_Name2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.FirstName + " " + secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.LastName;

                                //pmodel.BillingContact_Name2 = practiceLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.FirstName + practiceLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.MiddleName + practiceLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.LastName;
                                pmodel.BillingContact_FirstName2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.FirstName;
                                pmodel.BillingContact_MiddleName2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.MiddleName;
                                pmodel.BillingContact_LastName2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.LastName;
                                pmodel.BillingContact_Email2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.EmailAddress;
                                pmodel.BillingContact_POBoxAddress2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.POBoxAddress;
                                pmodel.BillingContact_Suite2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.Building;
                                pmodel.BillingContact_Street2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.Street;
                                pmodel.BillingContact_City2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.City;
                                pmodel.BillingContact_State2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.State;
                                pmodel.BillingContact_ZipCode2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.ZipCode;
                                pmodel.BillingContact_Country2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.Country;
                                pmodel.BillingContact_County2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.County;


                                if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.Telephone != null)
                                {
                                    pmodel.BillingContact_PhoneFirstThreeDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.Telephone.Substring(0, secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.Telephone.Length - 7);
                                    pmodel.BillingContact_PhoneSecondThreeDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.Telephone.Substring(3, 3);
                                    pmodel.BillingContact_PhoneLastFourDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.Telephone.Substring(6);

                                    pmodel.BillingContact_Phone2 = pmodel.BillingContact_PhoneFirstThreeDigit2 + "-" + pmodel.BillingContact_PhoneSecondThreeDigit2 + "-" + pmodel.BillingContact_PhoneLastFourDigit2;
                                }

                                if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.Fax != null)
                                {
                                    //pmodel.General_Fax1 = practiceLocation[secondaryPracticeLocationCount - 1].Facility.Fax.Substring(3, practiceLocation[secondaryPracticeLocationCount - 1].Facility.Fax.Length - 3);

                                    pmodel.BillingContact_FaxFirstThreeDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.Fax.Substring(0, secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.Fax.Length - 7);
                                    pmodel.BillingContact_FaxSecondThreeDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.Fax.Substring(3, 3);
                                    pmodel.BillingContact_FaxLastFourDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.Fax.Substring(6);

                                    pmodel.BillingContact_Fax2 = pmodel.BillingContact_FaxFirstThreeDigit2 + "-" + pmodel.BillingContact_FaxSecondThreeDigit2 + "-" + pmodel.BillingContact_FaxLastFourDigit2;
                                }

                            }

                            #endregion

                            #region Payment and Remittance 2

                            if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance != null)
                            {
                                if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson != null)
                                {
                                    if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.MiddleName != null)
                                        pmodel.PaymentRemittance_Name2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.FirstName + " " + secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.MiddleName + " " + secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.LastName;
                                    else
                                        pmodel.PaymentRemittance_Name2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.FirstName + " " + secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.LastName;

                                    //pmodel.PaymentRemittance_Name2 = practiceLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.FirstName + practiceLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.MiddleName + practiceLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.LastName;
                                    pmodel.PaymentRemittance_FirstName2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.FirstName;
                                    pmodel.PaymentRemittance_MiddleName2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.MiddleName;
                                    pmodel.PaymentRemittance_LastName2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.LastName;
                                    pmodel.PaymentRemittance_Email2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.EmailAddress;
                                    pmodel.PaymentRemittance_POBoxAddress2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.POBoxAddress;
                                    pmodel.PaymentRemittance_Suite2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.Building;
                                    pmodel.PaymentRemittance_Street2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.Street;
                                    pmodel.PaymentRemittance_City2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.City;
                                    pmodel.PaymentRemittance_State2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.State;
                                    pmodel.PaymentRemittance_ZipCode2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.ZipCode;
                                    pmodel.PaymentRemittance_Country2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.Country;
                                    pmodel.PaymentRemittance_County2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.County;

                                    if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.Telephone != null)
                                    {
                                        pmodel.PaymentRemittance_PhoneFirstThreeDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.Telephone.Substring(0, secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.Telephone.Length - 7);
                                        pmodel.PaymentRemittance_PhoneSecondThreeDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.Telephone.Substring(3, 3);
                                        pmodel.PaymentRemittance_PhoneLastFourDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.Telephone.Substring(6);

                                        pmodel.PaymentRemittance_Phone2 = pmodel.PaymentRemittance_PhoneFirstThreeDigit2 + "-" + pmodel.PaymentRemittance_PhoneSecondThreeDigit2 + "-" + pmodel.PaymentRemittance_PhoneLastFourDigit2;
                                    }




                                    if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.Fax != null)
                                    {
                                        //pmodel.General_Fax1 = practiceLocation[secondaryPracticeLocationCount - 1].Facility.Fax.Substring(3, practiceLocation[practiceLocationCount - 1].Facility.Fax.Length - 3);

                                        pmodel.PaymentRemittance_FaxFirstThreeDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.Fax.Substring(0, secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.Fax.Length - 7);
                                        pmodel.PaymentRemittance_FaxSecondThreeDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.Fax.Substring(3, 3);
                                        pmodel.PaymentRemittance_FaxLastFourDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.Fax.Substring(6);

                                        pmodel.PaymentRemittance_Fax2 = pmodel.PaymentRemittance_FaxFirstThreeDigit2 + "-" + pmodel.PaymentRemittance_FaxSecondThreeDigit2 + "-" + pmodel.PaymentRemittance_FaxLastFourDigit2;
                                    }
                                }
                                pmodel.PaymentRemittance_ElectronicBillCapability2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.ElectronicBillingCapability;
                                pmodel.PaymentRemittance_BillingDepartment2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.BillingDepartment;
                                pmodel.PaymentRemittance_ChekPayableTo2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.CheckPayableTo;
                                pmodel.PaymentRemittance_Office2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.Office;

                            }

                            #endregion

                            #region Open Practice Status 2

                            if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OpenPracticeStatus != null)
                            {
                                pmodel.OpenPractice_AgeLimitations2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OpenPracticeStatus.MinimumAge + " - " + secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OpenPracticeStatus.MaximumAge;
                            }

                            #endregion

                            #region Office Hours 2

                            if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OfficeHour != null)
                            {
                                if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OfficeHour.PracticeDays.Count > 0 && secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OfficeHour.PracticeDays.First().DailyHours.Count > 0)
                                {


                                    pmodel.OfficeHour_StartMonday2 = ConvertTimeFormat(secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OfficeHour.PracticeDays.ElementAt(0).DailyHours.First().StartTime);
                                    pmodel.OfficeHour_EndMonday2 = ConvertTimeFormat(secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OfficeHour.PracticeDays.ElementAt(0).DailyHours.First().EndTime);

                                    pmodel.OfficeHour_Monday2 = pmodel.OfficeHour_StartMonday2 + " - " + pmodel.OfficeHour_EndMonday2;

                                    pmodel.OfficeHour_StartTuesday2 = ConvertTimeFormat(secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OfficeHour.PracticeDays.ElementAt(1).DailyHours.First().StartTime);
                                    pmodel.OfficeHour_EndTuesday2 = ConvertTimeFormat(secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OfficeHour.PracticeDays.ElementAt(1).DailyHours.First().EndTime);

                                    pmodel.OfficeHour_Tuesday2 = pmodel.OfficeHour_StartTuesday2 + " - " + pmodel.OfficeHour_EndTuesday2;

                                    pmodel.OfficeHour_StartWednesday2 = ConvertTimeFormat(secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OfficeHour.PracticeDays.ElementAt(2).DailyHours.First().StartTime);
                                    pmodel.OfficeHour_EndWednesday2 = ConvertTimeFormat(secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OfficeHour.PracticeDays.ElementAt(2).DailyHours.First().EndTime);

                                    pmodel.OfficeHour_Wednesday2 = pmodel.OfficeHour_StartWednesday2 + " - " + pmodel.OfficeHour_EndWednesday2;

                                    pmodel.OfficeHour_StartThursday2 = ConvertTimeFormat(secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OfficeHour.PracticeDays.ElementAt(3).DailyHours.First().StartTime);
                                    pmodel.OfficeHour_EndThursday2 = ConvertTimeFormat(secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OfficeHour.PracticeDays.ElementAt(3).DailyHours.First().EndTime);

                                    pmodel.OfficeHour_Thursday2 = pmodel.OfficeHour_StartThursday2 + " - " + pmodel.OfficeHour_EndThursday2;

                                    pmodel.OfficeHour_StartFriday2 = ConvertTimeFormat(secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OfficeHour.PracticeDays.ElementAt(4).DailyHours.First().StartTime);
                                    pmodel.OfficeHour_EndFriday2 = ConvertTimeFormat(secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OfficeHour.PracticeDays.ElementAt(4).DailyHours.First().EndTime);

                                    pmodel.OfficeHour_Friday2 = pmodel.OfficeHour_StartFriday2 + " - " + pmodel.OfficeHour_EndFriday2;

                                    pmodel.OfficeHour_StartSaturday2 = ConvertTimeFormat(secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OfficeHour.PracticeDays.ElementAt(5).DailyHours.First().StartTime);
                                    pmodel.OfficeHour_EndSaturday2 = ConvertTimeFormat(secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OfficeHour.PracticeDays.ElementAt(5).DailyHours.First().EndTime);

                                    pmodel.OfficeHour_Saturday2 = pmodel.OfficeHour_StartSaturday2 + " - " + pmodel.OfficeHour_EndSaturday2;

                                    pmodel.OfficeHour_StartSunday2 = ConvertTimeFormat(secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OfficeHour.PracticeDays.ElementAt(6).DailyHours.First().StartTime);
                                    pmodel.OfficeHour_EndSunday2 = ConvertTimeFormat(secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OfficeHour.PracticeDays.ElementAt(6).DailyHours.First().EndTime);

                                    pmodel.OfficeHour_Sunday2 = pmodel.OfficeHour_StartSunday2 + " - " + pmodel.OfficeHour_EndSunday2;
                                }
                            }

                            #endregion

                            #region Supervising Provider 2

                            List<PracticeProvider> supervisingProvider = new List<PracticeProvider>();

                            if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PracticeProviders.Count > 0)
                            {
                                supervisingProvider = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PracticeProviders.
                            Where(s => s.Practice == AHC.CD.Entities.MasterData.Enums.PracticeType.Supervisor.ToString() &&
                                s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();
                            }


                            var supervisorCount = supervisingProvider.Count;

                            if (supervisorCount > 0)
                            {
                                pmodel.CoveringColleague_FirstName2 = supervisingProvider.ElementAt(supervisorCount - 1).FirstName;
                                pmodel.CoveringColleague_MiddleName2 = supervisingProvider.ElementAt(supervisorCount - 1).MiddleName;
                                pmodel.CoveringColleague_LastName2 = supervisingProvider.ElementAt(supervisorCount - 1).LastName;

                                if (pmodel.CoveringColleague_MiddleName2 != null)
                                    pmodel.CoveringColleague_FullName2 = pmodel.CoveringColleague_FirstName2 + " " + pmodel.CoveringColleague_MiddleName2 + " " + pmodel.CoveringColleague_LastName2;
                                else
                                    pmodel.CoveringColleague_FullName2 = pmodel.CoveringColleague_FirstName2 + " " + pmodel.CoveringColleague_LastName2;


                                if (supervisingProvider.ElementAt(supervisorCount - 1).Telephone != null)
                                {
                                    pmodel.CoveringColleague_PhoneFirstThreeDigit2 = supervisingProvider.ElementAt(supervisorCount - 1).Telephone.Substring(0, supervisingProvider.ElementAt(supervisorCount - 1).Telephone.Length - 7);
                                    pmodel.CoveringColleague_PhoneSecondThreeDigit2 = supervisingProvider.ElementAt(supervisorCount - 1).Telephone.Substring(3, 3);
                                    pmodel.CoveringColleague_PhoneLastFourDigit2 = supervisingProvider.ElementAt(supervisorCount - 1).Telephone.Substring(6);

                                    pmodel.CoveringColleague_PhoneNumber2 = pmodel.CoveringColleague_PhoneFirstThreeDigit2 + "-" + pmodel.CoveringColleague_PhoneSecondThreeDigit2 + "-" + pmodel.CoveringColleague_PhoneLastFourDigit2;

                                }

                                var specialities = supervisingProvider.ElementAt(supervisorCount - 1).PracticeProviderSpecialties.Where(s => s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString()).ToList();

                                if (specialities.Count > 0)
                                {
                                    if (specialities.ElementAt(specialities.Count - 1).Specialty != null)
                                        pmodel.CoveringColleague_Specialty2 = specialities.ElementAt(specialities.Count - 1).Specialty.Name;
                                }
                            }

                            #endregion

                            #region Covering Colleagues/Partners 2

                            List<PracticeProvider> patners = new List<PracticeProvider>();

                            if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PracticeProviders.Count > 0)
                            {

                                patners = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PracticeProviders.
                                Where(s => s.Practice == AHC.CD.Entities.MasterData.Enums.PracticeType.CoveringColleague.ToString() &&
                                    s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();
                            }
                            var patnersCount = patners.Count;

                            if (patnersCount > 0)
                            {
                                pmodel.Patners_FirstName2 = patners.ElementAt(patnersCount - 1).FirstName;
                                pmodel.Patners_MiddleName2 = patners.ElementAt(patnersCount - 1).MiddleName;
                                pmodel.Patners_LastName2 = patners.ElementAt(patnersCount - 1).LastName;

                                if (pmodel.Patners_MiddleName2 != null)
                                    pmodel.Patners_FullName2 = pmodel.Patners_FirstName2 + " " + pmodel.Patners_MiddleName2 + " " + pmodel.Patners_LastName2;
                                else
                                    pmodel.Patners_FullName2 = pmodel.Patners_FirstName2 + " " + pmodel.Patners_LastName2;

                                if (supervisingProvider.ElementAt(supervisorCount - 1).Telephone != null)
                                {
                                    pmodel.Patners_PhoneFirstThreeDigit2 = supervisingProvider.ElementAt(supervisorCount - 1).Telephone.Substring(0, supervisingProvider.ElementAt(supervisorCount - 1).Telephone.Length - 7);
                                    pmodel.Patners_PhoneSecondThreeDigit2 = supervisingProvider.ElementAt(supervisorCount - 1).Telephone.Substring(3, 3);
                                    pmodel.Patners_PhoneLastFourDigit2 = supervisingProvider.ElementAt(supervisorCount - 1).Telephone.Substring(6);

                                    pmodel.Patners_PhoneNumber2 = pmodel.CoveringColleague_PhoneFirstThreeDigit2 + "-" + pmodel.CoveringColleague_PhoneSecondThreeDigit2 + "-" + pmodel.CoveringColleague_PhoneLastFourDigit2;

                                }

                                var specialities = supervisingProvider.ElementAt(supervisorCount - 1).PracticeProviderSpecialties.Where(s => s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString()).ToList();

                                if (specialities.Count > 0)
                                {
                                    if (specialities.ElementAt(specialities.Count - 1).Specialty != null)
                                        pmodel.Patners_Specialty2 = specialities.ElementAt(specialities.Count - 1).Specialty.Name;
                                }
                            }

                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        if (secondaryPracticeLocationCount > 0 && secondaryPracticeLocation != null)
                        {
                            #region Practice Location Details 1
                            if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PracticeLocationCorporateName != null)
                            {
                                pmodel.General_OtherPracticeName1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PracticeLocationCorporateName;
                            }
                            if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PrimaryTaxId != null)
                            {
                                pmodel.General_PrimaryTaxId1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PrimaryTaxId;
                            }
                            if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].StartDate != null)
                            {
                                pmodel.General_StartDate1 = ConvertToDateString(secondaryPracticeLocation[secondaryPracticeLocationCount - 1].StartDate);
                            }
                            #endregion

                            #region Practice Location 1

                            if (secondaryPracticeLocation.Count > 0 && secondaryPracticeLocation[secondaryPracticeLocationCount - 1] != null)
                            {

                                #region Address 1

                                if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility != null)
                                {
                                    pmodel.General_PracticeLocationAddress1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.Street + " " + secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.City + ", " + secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.County + ", " + secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.State + ", " + secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.ZipCode;

                                    if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.Telephone != null)
                                    {
                                        pmodel.General_PhoneFirstThreeDigit1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.Telephone.Substring(0, secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.Telephone.Length - 7);
                                        pmodel.General_PhoneSecondThreeDigit1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.Telephone.Substring(3, 3);
                                        pmodel.General_PhoneLastFourDigit1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.Telephone.Substring(6);

                                        pmodel.General_Phone1 = pmodel.General_PhoneFirstThreeDigit1 + "-" + pmodel.General_PhoneSecondThreeDigit1 + "-" + pmodel.General_PhoneLastFourDigit1;
                                        pmodel.LocationAddress_Line3 = "Phone : " + pmodel.General_Phone1;
                                    }


                                    pmodel.General_Email1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.EmailAddress;

                                    if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.Fax != null)
                                    {
                                        //pmodel.General_Fax1 = practiceLocation[secondaryPracticeLocationCount - 1].Facility.Fax.Substring(3, practiceLocation[secondaryPracticeLocationCount - 1].Facility.Fax.Length - 3);

                                        pmodel.General_FaxFirstThreeDigit1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.Fax.Substring(0, secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.Fax.Length - 7);
                                        pmodel.General_FaxSecondThreeDigit1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.Fax.Substring(3, 3);
                                        pmodel.General_FaxLastFourDigit1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.Fax.Substring(6);

                                        pmodel.General_Fax1 = pmodel.General_FaxFirstThreeDigit1 + "-" + pmodel.General_FaxSecondThreeDigit1 + "-" + pmodel.General_FaxLastFourDigit1;
                                        pmodel.LocationAddress_Line3 = pmodel.LocationAddress_Line3 + " " + "Fax : " + pmodel.General_Fax1;
                                    }

                                    pmodel.General_AccessGroupName1 = "Access Healthcare Physicians, LLC";
                                    pmodel.General_Access2GroupName1 = "Access 2 Healthcare Physicians, LLC";

                                    pmodel.General_AccessGroupTaxId1 = "451444883";
                                    pmodel.General_Access2GroupTaxId1 = "451024515";

                                    pmodel.General_PracticeOrCorporateName1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.Name;
                                    pmodel.General_FacilityName1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.FacilityName;
                                    pmodel.General_Suite1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.Building;
                                    pmodel.General_Street1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.Street;
                                    pmodel.General_City1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.City;
                                    pmodel.General_State1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.State;
                                    pmodel.General_ZipCode1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.ZipCode;
                                    pmodel.General_Country1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.Country;
                                    pmodel.General_County1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.County;
                                    pmodel.General_IsCurrentlyPracticing1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].CurrentlyPracticingAtThisAddress;
                                    pmodel.LocationAddress_Line1 = pmodel.General_Street1 + " " + pmodel.General_Suite1;
                                    pmodel.LocationAddress_Line2 = pmodel.General_City1 + ", " + pmodel.General_State1 + " " + pmodel.General_ZipCode1;
                                    pmodel.General_City1State1 = pmodel.General_City1 + ", " + pmodel.General_State1 + ", " + pmodel.General_ZipCode1;
                                    pmodel.General_FacilityPracticeName1 = pmodel.General_FacilityName1 + " ," + pmodel.General_PracticeOrCorporateName1;


                                    #region Languages

                                    if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.FacilityDetail != null)
                                    {
                                        if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.FacilityDetail.Language != null)
                                        {
                                            var languages = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.FacilityDetail.Language.NonEnglishLanguages.Where(l => l.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();
                                            if (languages.Count > 0)
                                            {
                                                foreach (var language in languages)
                                                {
                                                    if (language != null)
                                                        pmodel.Languages_Known1 += language.Language + ",";
                                                }
                                            }
                                        }
                                    }

                                    #endregion
                                }

                                #endregion

                                #region Primary Credentialing Contact Information 1

                                if (secondaryPracticeLocationCount > 0 && secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PrimaryCredentialingContactPerson != null)
                                {

                                    if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PrimaryCredentialingContactPerson.MiddleName != null)
                                    {
                                        pmodel.PrimaryCredContact_FullName = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PrimaryCredentialingContactPerson.FirstName + " " + secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PrimaryCredentialingContactPerson.MiddleName + " " + secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PrimaryCredentialingContactPerson.LastName;
                                    }
                                    else
                                    {
                                        pmodel.PrimaryCredContact_FullName = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PrimaryCredentialingContactPerson.FirstName + " " + secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PrimaryCredentialingContactPerson.LastName;
                                    }
                                    pmodel.PrimaryCredContact_FirstName = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PrimaryCredentialingContactPerson.FirstName;
                                    pmodel.PrimaryCredContact_MI = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PrimaryCredentialingContactPerson.MiddleName;
                                    pmodel.PrimaryCredContact_LastName = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PrimaryCredentialingContactPerson.LastName;

                                    pmodel.PrimaryCredContact_Street = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PrimaryCredentialingContactPerson.Street;
                                    pmodel.PrimaryCredContact_Suite = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PrimaryCredentialingContactPerson.Building;
                                    pmodel.PrimaryCredContact_City = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PrimaryCredentialingContactPerson.City;
                                    pmodel.PrimaryCredContact_State = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PrimaryCredentialingContactPerson.State;
                                    pmodel.PrimaryCredContact_ZipCode = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PrimaryCredentialingContactPerson.ZipCode;
                                    pmodel.PrimaryCredContact_Phone = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PrimaryCredentialingContactPerson.Telephone;
                                    pmodel.PrimaryCredContact_Fax = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PrimaryCredentialingContactPerson.FaxNumber;
                                    pmodel.PrimaryCredContact_Email = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PrimaryCredentialingContactPerson.EmailAddress;
                                    pmodel.PrimaryCredContact_MobileNumber = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PrimaryCredentialingContactPerson.MobileNumber;
                                    pmodel.PrimaryCredContact_Address1 = pmodel.PrimaryCredContact_Street + ", " + pmodel.PrimaryCredContact_Suite;
                                }
                                #endregion

                                #region Open Practice Status 1

                                if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OpenPracticeStatus != null)
                                {
                                    pmodel.OpenPractice_AgeLimitations1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OpenPracticeStatus.MinimumAge + " - " + secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OpenPracticeStatus.MaximumAge;
                                }

                                #endregion

                                #region Office Manager 1

                                if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff != null)
                                {
                                    if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.MiddleName != null)
                                        pmodel.OfficeManager_Name1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.FirstName + " " + secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.MiddleName + " " + secondaryPracticeLocation[practiceLocationCount - 1].BusinessOfficeManagerOrStaff.LastName;
                                    else
                                        pmodel.OfficeManager_Name1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.FirstName + " " + secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.LastName;

                                    pmodel.OfficeManager_FirstName1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.FirstName;
                                    pmodel.OfficeManager_MiddleName1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.MiddleName;
                                    pmodel.OfficeManager_LastName1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.LastName;
                                    //pmodel.OfficeManager_Phone1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.MobileNumber;
                                    pmodel.OfficeManager_Email1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.EmailAddress;
                                    pmodel.OfficeManager_PoBoxAddress1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.POBoxAddress;
                                    pmodel.OfficeManager_Building1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.Building;
                                    pmodel.OfficeManager_Street1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.Street;
                                    pmodel.OfficeManager_City1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.City;
                                    pmodel.OfficeManager_State1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.State;
                                    pmodel.OfficeManager_ZipCode1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.ZipCode;
                                    pmodel.OfficeManager_Country1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.Country;
                                    pmodel.OfficeManager_County1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.County;
                                    //pmodel.OfficeManager_Fax1 = practiceLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.FaxNumber;

                                    if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.Telephone != null)
                                    {
                                        pmodel.OfficeManager_PhoneFirstThreeDigit1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.Telephone.Substring(0, secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.Telephone.Length - 7);
                                        pmodel.OfficeManager_PhoneSecondThreeDigit1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.Telephone.Substring(3, 3);
                                        pmodel.OfficeManager_PhoneLastFourDigit1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.Telephone.Substring(6);

                                        pmodel.OfficeManager_Phone1 = pmodel.OfficeManager_PhoneFirstThreeDigit1 + "-" + pmodel.OfficeManager_PhoneSecondThreeDigit1 + "-" + pmodel.OfficeManager_PhoneLastFourDigit1;

                                    }




                                    if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.Fax != null)
                                    {
                                        //pmodel.General_Fax1 = practiceLocation[secondaryPracticeLocationCount - 1].Facility.Fax.Substring(3, practiceLocation[secondaryPracticeLocationCount - 1].Facility.Fax.Length - 3);

                                        pmodel.OfficeManager_FaxFirstThreeDigit1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.Fax.Substring(0, secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.Fax.Length - 7);
                                        pmodel.OfficeManager_FaxSecondThreeDigit1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.Fax.Substring(3, 3);
                                        pmodel.OfficeManager_FaxLastFourDigit1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.Fax.Substring(6);

                                        pmodel.OfficeManager_Fax1 = pmodel.OfficeManager_FaxFirstThreeDigit1 + "-" + pmodel.OfficeManager_FaxSecondThreeDigit1 + "-" + pmodel.OfficeManager_FaxLastFourDigit1;


                                    }
                                }

                                #endregion

                                #region Billing Contact 1

                                if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson != null)
                                {
                                    if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.MiddleName != null)
                                        pmodel.BillingContact_Name1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.FirstName + " " + secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.MiddleName + " " + secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.LastName;
                                    else
                                        pmodel.BillingContact_Name1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.FirstName + " " + secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.LastName;

                                    pmodel.BillingContact_FirstName1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.FirstName;
                                    pmodel.BillingContact_MiddleName1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.MiddleName;
                                    pmodel.BillingContact_LastName1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.LastName;
                                    pmodel.BillingContact_Email1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.EmailAddress;
                                    //pmodel.BillingContact_Phone1 = practiceLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.MobileNumber;
                                    //pmodel.BillingContact_Fax1 = practiceLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.FaxNumber;
                                    pmodel.BillingContact_POBoxAddress1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.POBoxAddress;
                                    pmodel.BillingContact_Suite1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.Building;
                                    pmodel.BillingContact_Street1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.Street;
                                    pmodel.BillingContact_City1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.City;
                                    pmodel.BillingContact_State1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.State;
                                    pmodel.BillingContact_ZipCode1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.ZipCode;
                                    pmodel.BillingContact_Country1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.Country;
                                    pmodel.BillingContact_County1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.County;

                                    pmodel.BillingContact_Line1 = pmodel.BillingContact_Street1 + " " + pmodel.BillingContact_Suite1;
                                    pmodel.BillingContact_Line2 = pmodel.BillingContact_City1 + ", " + pmodel.BillingContact_State1 + " " + pmodel.BillingContact_County1 + " " + pmodel.BillingContact_ZipCode1;
                                    pmodel.BillingContact_City1State1 = pmodel.BillingContact_City1 + " ," + pmodel.BillingContact_State1 + ", " + pmodel.BillingContact_ZipCode1;

                                    if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.Telephone != null)
                                    {
                                        pmodel.BillingContact_PhoneFirstThreeDigit1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.Telephone.Substring(0, secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.Telephone.Length - 7);
                                        pmodel.BillingContact_PhoneSecondThreeDigit1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.Telephone.Substring(3, 3);
                                        pmodel.BillingContact_PhoneLastFourDigit1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.Telephone.Substring(6);

                                        pmodel.BillingContact_Phone1 = pmodel.BillingContact_PhoneFirstThreeDigit1 + "-" + pmodel.BillingContact_PhoneSecondThreeDigit1 + "-" + pmodel.BillingContact_PhoneLastFourDigit1;
                                    }




                                    if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.Fax != null)
                                    {
                                        //pmodel.General_Fax1 = practiceLocation[secondaryPracticeLocationCount - 1].Facility.Fax.Substring(3, practiceLocation[secondaryPracticeLocationCount - 1].Facility.Fax.Length - 3);

                                        pmodel.BillingContact_FaxFirstThreeDigit1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.Fax.Substring(0, secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.Fax.Length - 7);
                                        pmodel.BillingContact_FaxSecondThreeDigit1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.Fax.Substring(3, 3);
                                        pmodel.BillingContact_FaxLastFourDigit1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.Fax.Substring(6);

                                        pmodel.BillingContact_Fax1 = pmodel.BillingContact_FaxFirstThreeDigit1 + "-" + pmodel.BillingContact_FaxSecondThreeDigit1 + "-" + pmodel.BillingContact_FaxLastFourDigit1;
                                    }
                                }

                                #endregion

                                #region Payment and Remittance 1

                                if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance != null)
                                {
                                    if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson != null)
                                    {
                                        if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.MiddleName != null)
                                            pmodel.PaymentRemittance_Name1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.FirstName + " " + secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.MiddleName + " " + secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.LastName;
                                        else
                                            pmodel.PaymentRemittance_Name1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.FirstName + " " + secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.LastName;

                                        pmodel.PaymentRemittance_FirstName1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.FirstName;
                                        pmodel.PaymentRemittance_MiddleName1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.MiddleName;
                                        pmodel.PaymentRemittance_LastName1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.LastName;
                                        pmodel.PaymentRemittance_Email1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.EmailAddress;
                                        pmodel.PaymentRemittance_POBoxAddress1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.POBoxAddress;
                                        pmodel.PaymentRemittance_Suite1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.Building;
                                        pmodel.PaymentRemittance_Street1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.Street;
                                        pmodel.PaymentRemittance_City1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.City;
                                        pmodel.PaymentRemittance_State1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.State;
                                        pmodel.PaymentRemittance_ZipCode1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.ZipCode;
                                        pmodel.PaymentRemittance_Country1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.Country;
                                        pmodel.PaymentRemittance_County1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.County;

                                        if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.Telephone != null)
                                        {
                                            pmodel.PaymentRemittance_PhoneFirstThreeDigit1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.Telephone.Substring(0, secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.Telephone.Length - 7);
                                            pmodel.PaymentRemittance_PhoneSecondThreeDigit1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.Telephone.Substring(3, 3);
                                            pmodel.PaymentRemittance_PhoneLastFourDigit1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.Telephone.Substring(6);

                                            pmodel.PaymentRemittance_Phone1 = pmodel.PaymentRemittance_PhoneFirstThreeDigit1 + "-" + pmodel.PaymentRemittance_PhoneSecondThreeDigit1 + "-" + pmodel.PaymentRemittance_PhoneLastFourDigit1;
                                            if (pmodel.PaymentRemittance_Phone1.Length > 13)
                                            {
                                                pmodel.General_Phone = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.Telephone;
                                            }
                                        }




                                        if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.Fax != null)
                                        {
                                            //pmodel.General_Fax1 = practiceLocation[secondaryPracticeLocationCount - 1].Facility.Fax.Substring(3, practiceLocation[secondaryPracticeLocationCount - 1].Facility.Fax.Length - 3);

                                            pmodel.PaymentRemittance_FaxFirstThreeDigit1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.Fax.Substring(0, secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.Fax.Length - 7);
                                            pmodel.PaymentRemittance_FaxSecondThreeDigit1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.Fax.Substring(3, 3);
                                            pmodel.PaymentRemittance_FaxLastFourDigit1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.Fax.Substring(6);

                                            pmodel.PaymentRemittance_Fax1 = pmodel.PaymentRemittance_FaxFirstThreeDigit1 + "-" + pmodel.PaymentRemittance_FaxSecondThreeDigit1 + "-" + pmodel.PaymentRemittance_FaxLastFourDigit1;
                                        }
                                    }
                                    pmodel.PaymentRemittance_ElectronicBillCapability1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.ElectronicBillingCapability;
                                    pmodel.PaymentRemittance_BillingDepartment1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.BillingDepartment;
                                    pmodel.PaymentRemittance_ChekPayableTo1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.CheckPayableTo;
                                    pmodel.PaymentRemittance_Office1 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.Office;
                                }

                                #endregion

                                #region Office Hours 1

                                if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OfficeHour != null)
                                {
                                    if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OfficeHour.PracticeDays.Count > 0 && secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OfficeHour.PracticeDays.First().DailyHours.Count > 0)
                                    {

                                        pmodel.OfficeHour_StartMonday1 = ConvertTimeFormat(secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OfficeHour.PracticeDays.ElementAt(0).DailyHours.First().StartTime);
                                        pmodel.OfficeHour_EndMonday1 = ConvertTimeFormat(secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OfficeHour.PracticeDays.ElementAt(0).DailyHours.First().EndTime);

                                        pmodel.OfficeHour_Monday1 = pmodel.OfficeHour_StartMonday1 + " - " + pmodel.OfficeHour_EndMonday1;

                                        pmodel.OfficeHour_StartTuesday1 = ConvertTimeFormat(secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OfficeHour.PracticeDays.ElementAt(1).DailyHours.First().StartTime);
                                        pmodel.OfficeHour_EndTuesday1 = ConvertTimeFormat(secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OfficeHour.PracticeDays.ElementAt(1).DailyHours.First().EndTime);

                                        pmodel.OfficeHour_Tuesday1 = pmodel.OfficeHour_StartTuesday1 + " - " + pmodel.OfficeHour_EndTuesday1;

                                        pmodel.OfficeHour_StartWednesday1 = ConvertTimeFormat(secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OfficeHour.PracticeDays.ElementAt(2).DailyHours.First().StartTime);
                                        pmodel.OfficeHour_EndWednesday1 = ConvertTimeFormat(secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OfficeHour.PracticeDays.ElementAt(2).DailyHours.First().EndTime);

                                        pmodel.OfficeHour_Wednesday1 = pmodel.OfficeHour_StartWednesday1 + " - " + pmodel.OfficeHour_EndWednesday1;

                                        pmodel.OfficeHour_StartThursday1 = ConvertTimeFormat(secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OfficeHour.PracticeDays.ElementAt(3).DailyHours.First().StartTime);
                                        pmodel.OfficeHour_EndThursday1 = ConvertTimeFormat(secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OfficeHour.PracticeDays.ElementAt(3).DailyHours.First().EndTime);

                                        pmodel.OfficeHour_Thursday1 = pmodel.OfficeHour_StartThursday1 + " - " + pmodel.OfficeHour_EndThursday1;

                                        pmodel.OfficeHour_StartFriday1 = ConvertTimeFormat(secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OfficeHour.PracticeDays.ElementAt(4).DailyHours.First().StartTime);
                                        pmodel.OfficeHour_EndFriday1 = ConvertTimeFormat(secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OfficeHour.PracticeDays.ElementAt(4).DailyHours.First().EndTime);

                                        pmodel.OfficeHour_Friday1 = pmodel.OfficeHour_StartFriday1 + " - " + pmodel.OfficeHour_EndFriday1;

                                        pmodel.OfficeHour_StartSaturday1 = ConvertTimeFormat(secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OfficeHour.PracticeDays.ElementAt(5).DailyHours.First().StartTime);
                                        pmodel.OfficeHour_EndSaturday1 = ConvertTimeFormat(secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OfficeHour.PracticeDays.ElementAt(5).DailyHours.First().EndTime);

                                        pmodel.OfficeHour_Saturday1 = pmodel.OfficeHour_StartSaturday1 + " - " + pmodel.OfficeHour_EndSaturday1;

                                        pmodel.OfficeHour_StartSunday1 = ConvertTimeFormat(secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OfficeHour.PracticeDays.ElementAt(6).DailyHours.First().StartTime);
                                        pmodel.OfficeHour_EndSunday1 = ConvertTimeFormat(secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OfficeHour.PracticeDays.ElementAt(6).DailyHours.First().EndTime);

                                        pmodel.OfficeHour_Sunday1 = pmodel.OfficeHour_StartSunday1 + " - " + pmodel.OfficeHour_EndSunday1;
                                    }
                                }

                                #endregion

                                #region Supervising Provider 1

                                var supervisingProvider = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PracticeProviders.
                                Where(s => s.Practice == AHC.CD.Entities.MasterData.Enums.PracticeType.Supervisor.ToString() &&
                                    s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();

                                var supervisorCount = supervisingProvider.Count;

                                if (supervisorCount > 0)
                                {
                                    pmodel.CoveringColleague_FirstName1 = supervisingProvider.ElementAt(supervisorCount - 1).FirstName;
                                    pmodel.CoveringColleague_MiddleName1 = supervisingProvider.ElementAt(supervisorCount - 1).MiddleName;
                                    pmodel.CoveringColleague_LastName1 = supervisingProvider.ElementAt(supervisorCount - 1).LastName;

                                    if (pmodel.CoveringColleague_MiddleName1 != null)
                                        pmodel.CoveringColleague_FullName1 = pmodel.CoveringColleague_FirstName1 + " " + pmodel.CoveringColleague_MiddleName1 + " " + pmodel.CoveringColleague_LastName1;
                                    else
                                        pmodel.CoveringColleague_FullName1 = pmodel.CoveringColleague_FirstName1 + " " + pmodel.CoveringColleague_LastName1;


                                    if (supervisingProvider.ElementAt(supervisorCount - 1).Telephone != null)
                                    {
                                        pmodel.CoveringColleague_PhoneFirstThreeDigit1 = supervisingProvider.ElementAt(supervisorCount - 1).Telephone.Substring(0, supervisingProvider.ElementAt(supervisorCount - 1).Telephone.Length - 7);
                                        pmodel.CoveringColleague_PhoneSecondThreeDigit1 = supervisingProvider.ElementAt(supervisorCount - 1).Telephone.Substring(3, 3);
                                        pmodel.CoveringColleague_PhoneLastFourDigit1 = supervisingProvider.ElementAt(supervisorCount - 1).Telephone.Substring(6);

                                        pmodel.CoveringColleague_PhoneNumber1 = pmodel.CoveringColleague_PhoneFirstThreeDigit1 + "-" + pmodel.CoveringColleague_PhoneSecondThreeDigit1 + "-" + pmodel.CoveringColleague_PhoneLastFourDigit1;

                                    }

                                    var specialities = supervisingProvider.ElementAt(supervisorCount - 1).PracticeProviderSpecialties.Where(s => s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString()).ToList();

                                    if (specialities.Count > 0)
                                    {
                                        if (specialities.ElementAt(specialities.Count - 1).Specialty != null)
                                            pmodel.CoveringColleague_Specialty1 = specialities.ElementAt(specialities.Count - 1).Specialty.Name;
                                    }
                                }

                                #endregion

                                #region Covering Colleagues/Partners 1

                                var patners = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PracticeProviders.
                                Where(s => s.Practice == AHC.CD.Entities.MasterData.Enums.PracticeType.CoveringColleague.ToString() &&
                                    s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();

                                var patnersCount = patners.Count;

                                if (patnersCount > 0)
                                {
                                    pmodel.Patners_FirstName1 = patners.ElementAt(patnersCount - 1).FirstName;
                                    pmodel.Patners_MiddleName1 = patners.ElementAt(patnersCount - 1).MiddleName;
                                    pmodel.Patners_LastName1 = patners.ElementAt(patnersCount - 1).LastName;

                                    if (pmodel.Patners_MiddleName1 != null)
                                        pmodel.Patners_FullName1 = pmodel.Patners_FirstName1 + " " + pmodel.Patners_MiddleName1 + " " + pmodel.Patners_LastName1;
                                    else
                                        pmodel.Patners_FullName1 = pmodel.Patners_FirstName1 + " " + pmodel.Patners_LastName1;

                                    if (patners.ElementAt(patnersCount - 1).Telephone != null)
                                    {
                                        pmodel.Patners_PhoneFirstThreeDigit1 = patners.ElementAt(patnersCount - 1).Telephone.Substring(0, patners.ElementAt(patnersCount - 1).Telephone.Length - 7);
                                        pmodel.Patners_PhoneSecondThreeDigit1 = patners.ElementAt(patnersCount - 1).Telephone.Substring(3, 3);
                                        pmodel.Patners_PhoneLastFourDigit1 = patners.ElementAt(patnersCount - 1).Telephone.Substring(6);

                                        pmodel.Patners_PhoneNumber1 = pmodel.CoveringColleague_PhoneFirstThreeDigit1 + "-" + pmodel.CoveringColleague_PhoneSecondThreeDigit1 + "-" + pmodel.CoveringColleague_PhoneLastFourDigit1;

                                    }

                                    var specialities = patners.ElementAt(patnersCount - 1).PracticeProviderSpecialties.Where(s => s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString()).ToList();

                                    if (specialities.Count > 0)
                                    {
                                        if (specialities.ElementAt(specialities.Count - 1).Specialty != null)
                                            pmodel.Patners_Specialty1 = specialities.ElementAt(specialities.Count - 1).Specialty.Name;
                                    }
                                }

                                #endregion

                                #region Specific Secondary Practice Location When Primary Practice Location is Null.

                                if (secondaryPracticeLocation.Count == 1)
                                {
                                    #region Specific Secondary Practice Location Address 1

                                    if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility != null)
                                    {
                                        pmodel.General_PracticeOrCorporateName2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.Name;
                                        pmodel.General_FacilityName2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.FacilityName;
                                        pmodel.General_PracticeLocationAddress2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.Street + " " + secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.City + ", " + secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.State + ", " + secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.ZipCode;

                                        pmodel.General_Suite2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.Building;
                                        pmodel.General_Street2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.Street;
                                        pmodel.General_City2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.City;
                                        pmodel.General_State2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.State;
                                        pmodel.General_ZipCode2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.ZipCode;
                                        pmodel.General_Country2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.Country;
                                        pmodel.General_County2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.County;
                                        pmodel.LocationAddress2_Line1 = pmodel.General_Street2 + "  " + pmodel.General_Suite2;
                                        pmodel.LocationAddress2_Line2 = pmodel.General_City2 + ", " + pmodel.General_State2 + " " + pmodel.General_ZipCode2;
                                        pmodel.General_FacilityPracticeName2 = pmodel.General_FacilityName2 + ", " + pmodel.General_PracticeOrCorporateName2;

                                        pmodel.General_AccessGroupName2 = "Access Healthcare Physicians, LLC";
                                        pmodel.General_Access2GroupName2 = "Access 2 Healthcare Physicians, LLC";

                                        pmodel.General_AccessGroupTaxId2 = "451444883";
                                        pmodel.General_Access2GroupTaxId2 = "451024515";

                                        pmodel.Provider_FullNameTitleBCBS = pmodel.Provider_FullNameTitle;
                                        pmodel.Provider_FullNameTitle_Additional = pmodel.Provider_FullNameTitle;

                                        if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.Telephone != null)
                                        {
                                            pmodel.General_PhoneFirstThreeDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.Telephone.Substring(0, secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.Telephone.Length - 7);
                                            pmodel.General_PhoneSecondThreeDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.Telephone.Substring(3, 3);
                                            pmodel.General_PhoneLastFourDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.Telephone.Substring(6);

                                            pmodel.General_Phone2 = pmodel.General_PhoneFirstThreeDigit2 + "-" + pmodel.General_PhoneSecondThreeDigit2 + "-" + pmodel.General_PhoneLastFourDigit2;
                                            if (pmodel.General_Phone2.Length > 13)
                                            {
                                                pmodel.General_Phone2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.Telephone;
                                            }
                                        }


                                        pmodel.General_Email2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.EmailAddress;

                                        if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.Fax != null)
                                        {
                                            //pmodel.General_Fax1 = practiceLocation[secondaryPracticeLocationCount - 1].Facility.Fax.Substring(3, practiceLocation[secondaryPracticeLocationCount - 1].Facility.Fax.Length - 3);

                                            pmodel.General_FaxFirstThreeDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.Fax.Substring(0, secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.Fax.Length - 7);
                                            pmodel.General_FaxSecondThreeDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.Fax.Substring(3, 3);
                                            pmodel.General_FaxLastFourDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.Fax.Substring(6);

                                            pmodel.General_Fax2 = pmodel.General_FaxFirstThreeDigit2 + "-" + pmodel.General_FaxSecondThreeDigit2 + "-" + pmodel.General_FaxLastFourDigit2;
                                            if (pmodel.General_Fax2.Length > 13)
                                            {
                                                pmodel.General_Fax2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.Fax;
                                            }
                                        }

                                        #region Languages

                                        if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.FacilityDetail != null && secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.FacilityDetail.Language != null)
                                        {
                                            var languages = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.FacilityDetail.Language.NonEnglishLanguages.Where(l => l.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();
                                            if (languages.Count > 0)
                                            {
                                                foreach (var language in languages)
                                                {
                                                    if (language != null)
                                                        pmodel.Languages_Known2 += language.Language + ",";
                                                }
                                            }
                                        }
                                        #endregion
                                    }

                                    #endregion

                                    #region  Specific Secondary Practice Location Office Hours 1

                                    if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OfficeHour != null)
                                    {
                                        if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OfficeHour.PracticeDays.Count > 0 && secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OfficeHour.PracticeDays.First().DailyHours.Count > 0)
                                        {


                                            pmodel.OfficeHour_StartMonday2 = ConvertTimeFormat(secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OfficeHour.PracticeDays.ElementAt(0).DailyHours.First().StartTime);
                                            pmodel.OfficeHour_EndMonday2 = ConvertTimeFormat(secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OfficeHour.PracticeDays.ElementAt(0).DailyHours.First().EndTime);

                                            pmodel.OfficeHour_Monday2 = pmodel.OfficeHour_StartMonday2 + " - " + pmodel.OfficeHour_EndMonday2;

                                            pmodel.OfficeHour_StartTuesday2 = ConvertTimeFormat(secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OfficeHour.PracticeDays.ElementAt(1).DailyHours.First().StartTime);
                                            pmodel.OfficeHour_EndTuesday2 = ConvertTimeFormat(secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OfficeHour.PracticeDays.ElementAt(1).DailyHours.First().EndTime);

                                            pmodel.OfficeHour_Tuesday2 = pmodel.OfficeHour_StartTuesday2 + " - " + pmodel.OfficeHour_EndTuesday2;

                                            pmodel.OfficeHour_StartWednesday2 = ConvertTimeFormat(secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OfficeHour.PracticeDays.ElementAt(2).DailyHours.First().StartTime);
                                            pmodel.OfficeHour_EndWednesday2 = ConvertTimeFormat(secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OfficeHour.PracticeDays.ElementAt(2).DailyHours.First().EndTime);

                                            pmodel.OfficeHour_Wednesday2 = pmodel.OfficeHour_StartWednesday2 + " - " + pmodel.OfficeHour_EndWednesday2;

                                            pmodel.OfficeHour_StartThursday2 = ConvertTimeFormat(secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OfficeHour.PracticeDays.ElementAt(3).DailyHours.First().StartTime);
                                            pmodel.OfficeHour_EndThursday2 = ConvertTimeFormat(secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OfficeHour.PracticeDays.ElementAt(3).DailyHours.First().EndTime);

                                            pmodel.OfficeHour_Thursday2 = pmodel.OfficeHour_StartThursday2 + " - " + pmodel.OfficeHour_EndThursday2;

                                            pmodel.OfficeHour_StartFriday2 = ConvertTimeFormat(secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OfficeHour.PracticeDays.ElementAt(4).DailyHours.First().StartTime);
                                            pmodel.OfficeHour_EndFriday2 = ConvertTimeFormat(secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OfficeHour.PracticeDays.ElementAt(4).DailyHours.First().EndTime);

                                            pmodel.OfficeHour_Friday2 = pmodel.OfficeHour_StartFriday2 + " - " + pmodel.OfficeHour_EndFriday2;

                                            pmodel.OfficeHour_StartSaturday2 = ConvertTimeFormat(secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OfficeHour.PracticeDays.ElementAt(5).DailyHours.First().StartTime);
                                            pmodel.OfficeHour_EndSaturday2 = ConvertTimeFormat(secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OfficeHour.PracticeDays.ElementAt(5).DailyHours.First().EndTime);

                                            pmodel.OfficeHour_Saturday2 = pmodel.OfficeHour_StartSaturday2 + " - " + pmodel.OfficeHour_EndSaturday2;

                                            pmodel.OfficeHour_StartSunday2 = ConvertTimeFormat(secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OfficeHour.PracticeDays.ElementAt(6).DailyHours.First().StartTime);
                                            pmodel.OfficeHour_EndSunday2 = ConvertTimeFormat(secondaryPracticeLocation[secondaryPracticeLocationCount - 1].OfficeHour.PracticeDays.ElementAt(6).DailyHours.First().EndTime);

                                            pmodel.OfficeHour_Sunday2 = pmodel.OfficeHour_StartSunday2 + " - " + pmodel.OfficeHour_EndSunday2;
                                        }
                                    }

                                    #endregion

                                    #region Specific Secondary Practice Location Billing Contact 2

                                    if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson != null)
                                    {
                                        if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.MiddleName != null)
                                            pmodel.BillingContact_Name2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.FirstName + " " + secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.MiddleName + " " + secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.LastName;
                                        else
                                            pmodel.BillingContact_Name2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.FirstName + " " + secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.LastName;

                                        //pmodel.BillingContact_Name2 = practiceLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.FirstName + practiceLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.MiddleName + practiceLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.LastName;
                                        pmodel.BillingContact_FirstName2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.FirstName;
                                        pmodel.BillingContact_MiddleName2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.MiddleName;
                                        pmodel.BillingContact_LastName2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.LastName;
                                        pmodel.BillingContact_Email2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.EmailAddress;
                                        pmodel.BillingContact_POBoxAddress2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.POBoxAddress;
                                        pmodel.BillingContact_Suite2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.Building;
                                        pmodel.BillingContact_Street2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.Street;
                                        pmodel.BillingContact_City2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.City;
                                        pmodel.BillingContact_State2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.State;
                                        pmodel.BillingContact_ZipCode2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.ZipCode;
                                        pmodel.BillingContact_Country2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.Country;
                                        pmodel.BillingContact_County2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.County;


                                        if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.Telephone != null)
                                        {
                                            pmodel.BillingContact_PhoneFirstThreeDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.Telephone.Substring(0, secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.Telephone.Length - 7);
                                            pmodel.BillingContact_PhoneSecondThreeDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.Telephone.Substring(3, 3);
                                            pmodel.BillingContact_PhoneLastFourDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.Telephone.Substring(6);

                                            pmodel.BillingContact_Phone2 = pmodel.BillingContact_PhoneFirstThreeDigit2 + "-" + pmodel.BillingContact_PhoneSecondThreeDigit2 + "-" + pmodel.BillingContact_PhoneLastFourDigit2;
                                        }

                                        if (secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.Fax != null)
                                        {
                                            //pmodel.General_Fax1 = practiceLocation[secondaryPracticeLocationCount - 1].Facility.Fax.Substring(3, practiceLocation[secondaryPracticeLocationCount - 1].Facility.Fax.Length - 3);

                                            pmodel.BillingContact_FaxFirstThreeDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.Fax.Substring(0, secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.Fax.Length - 7);
                                            pmodel.BillingContact_FaxSecondThreeDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.Fax.Substring(3, 3);
                                            pmodel.BillingContact_FaxLastFourDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.Fax.Substring(6);

                                            pmodel.BillingContact_Fax2 = pmodel.BillingContact_FaxFirstThreeDigit2 + "-" + pmodel.BillingContact_FaxSecondThreeDigit2 + "-" + pmodel.BillingContact_FaxLastFourDigit2;
                                        }

                                    }

                                    #endregion
                                }

                                #endregion

                            }

                            #endregion

                            #region Secondary Practice Location

                            if (secondaryPracticeLocationCount > 1 && secondaryPracticeLocation[secondaryPracticeLocationCount - 2] != null)
                            {
                                pmodel.Primecare1 = "PRIMECARE";

                                #region Practice Location Details 2
                                if (secondaryPracticeLocation[secondaryPracticeLocationCount - 2].PracticeLocationCorporateName != null)
                                {
                                    pmodel.General_OtherPracticeName2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].PracticeLocationCorporateName;

                                }
                                if (secondaryPracticeLocation[secondaryPracticeLocationCount - 2].PrimaryTaxId != null)
                                {
                                    pmodel.General_PrimaryTaxId2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].PrimaryTaxId;
                                }
                                if (secondaryPracticeLocation[secondaryPracticeLocationCount - 2].StartDate != null)
                                {
                                    pmodel.General_StartDate2 = ConvertToDateString(secondaryPracticeLocation[secondaryPracticeLocationCount - 2].StartDate);
                                }
                                #endregion

                                #region TriCare Prime Credentialing Prefilled properties
                                pmodel.CorrespondenceAddressName = "Credentialing";
                                pmodel.CorrespondenceAddressStreetAddress = "14690 Spring Hill Drive";
                                pmodel.CorrespondenceAddressSuiteNumber = "101";
                                pmodel.CorrespondenceAddressCity = "Spring Hill";
                                pmodel.CorrespondenceAddressState = "FL";
                                pmodel.CorrespondenceAddressSZipCode = "34609-8102";
                                pmodel.CorrespondenceAddressCounty = "Hernando";
                                pmodel.CorrespondenceAddressOfficePhoneNumber = "(352)799-0046";
                                pmodel.CorrespondenceAddressOfficeFaxNumber = "(352)799-0042";
                                pmodel.CorrespondenceAddressEmailId = "credentialing@accesshealthcarellc.net";
                                pmodel.BillingAddressName = "Access Health Care Physicians, LLC";
                                pmodel.LegalPracticeName = "Access Health Care Physicians, LLC";
                                pmodel.BillingAddressStreetAddress = "P.O.Box 919469";
                                pmodel.BillingAddressCity = "Orlando";
                                pmodel.BillingAddressState = "FL";
                                pmodel.BillingAddressZipCode = "32891-9469";
                                pmodel.BillingAddressCounty = "Orange";
                                pmodel.BillingAddressOfficePhoneNumber = "(727)823-2188";
                                pmodel.BillingAddressOfficeFaxNumber = "(727)828-0723";
                                pmodel.BillingAddressSuiteNumber = "";
                                pmodel.BillingAddressEmailId = "generalinquiry@medenet.net";
                                pmodel.OfficeHoursMondayFrom = "8 AM";
                                pmodel.OfficeHourMondayTo = "5 PM";
                                pmodel.OfficeHoursTuesdayFrom = "8 AM";
                                pmodel.OfficeHourTuesdayTo = "5 PM";
                                pmodel.OfficeHoursWednesdayFrom = "8 AM";
                                pmodel.OfficeHourWednesdayTo = "5 PM";
                                pmodel.OfficeHoursThursdayFrom = "8 AM";
                                pmodel.OfficeHourThursdayTo = "5 PM";
                                pmodel.OfficeHoursFridayFrom = "8 AM";
                                pmodel.OfficeHourFridayTo = "5 PM";
                                pmodel.Practice_EmailAddress = "credentialing@accesshealthcarellc.net";
                                pmodel.Practice_HowManyTriCarePatientsWillYouAccept = "100";
                                pmodel.Practice_AgeRangeFrom = "18";
                                pmodel.Practice_AgeRangeTo = "100";
                                pmodel.Practice_CredentialingContactName = "Credentialing Department";
                                pmodel.Practice_CredentialingContactEmailAddress = "credentialing@accesshealthcarellc.net";
                                pmodel.Practice_CredentialingContactPhoneNumber = "3527990046";
                                pmodel.Practice_CredentialingContactFax = "3527990042";

                                #endregion

                                #region Address 2

                                if (secondaryPracticeLocation[secondaryPracticeLocationCount - 2].Facility != null)
                                {
                                    pmodel.General_PracticeOrCorporateName2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].Facility.Name;
                                    pmodel.General_FacilityName2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].Facility.FacilityName;
                                    pmodel.General_PracticeLocationAddress2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].Facility.Street + " " + secondaryPracticeLocation[secondaryPracticeLocationCount - 2].Facility.City + ", " + secondaryPracticeLocation[secondaryPracticeLocationCount - 2].Facility.State + ", " + secondaryPracticeLocation[secondaryPracticeLocationCount - 2].Facility.ZipCode;
                                    //pmodel.General_Phone2 = practiceLocation[secondaryPracticeLocationCount - 2].Facility.MobileNumber;
                                    //pmodel.General_Fax2 = practiceLocation[secondaryPracticeLocationCount - 2].Facility.FaxNumber;
                                    pmodel.General_Suite2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].Facility.Building;
                                    pmodel.General_Street2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].Facility.Street;
                                    pmodel.General_City2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].Facility.City;
                                    pmodel.General_State2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].Facility.State;
                                    pmodel.General_ZipCode2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].Facility.ZipCode;
                                    pmodel.General_Country2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].Facility.Country;
                                    pmodel.General_County2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].Facility.County;
                                    pmodel.LocationAddress2_Line1 = pmodel.General_Street2 + "  " + pmodel.General_Suite2;
                                    pmodel.LocationAddress2_Line2 = pmodel.General_City2 + ", " + pmodel.General_State2 + " " + pmodel.General_ZipCode2;
                                    pmodel.General_FacilityPracticeName2 = pmodel.General_FacilityName2 + ", " + pmodel.General_PracticeOrCorporateName2;

                                    pmodel.General_AccessGroupName2 = "Access Healthcare Physicians, LLC";
                                    pmodel.General_Access2GroupName2 = "Access 2 Healthcare Physicians, LLC";

                                    pmodel.General_AccessGroupTaxId2 = "451444883";
                                    pmodel.General_Access2GroupTaxId2 = "451024515";

                                    pmodel.Provider_FullNameTitleBCBS = pmodel.Provider_FullNameTitle;
                                    pmodel.Provider_FullNameTitle_Additional = pmodel.Provider_FullNameTitle;

                                    if (secondaryPracticeLocation[secondaryPracticeLocationCount - 2].Facility.Telephone != null)
                                    {
                                        pmodel.General_PhoneFirstThreeDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].Facility.Telephone.Substring(0, secondaryPracticeLocation[secondaryPracticeLocationCount - 2].Facility.Telephone.Length - 7);
                                        pmodel.General_PhoneSecondThreeDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].Facility.Telephone.Substring(3, 3);
                                        pmodel.General_PhoneLastFourDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].Facility.Telephone.Substring(6);

                                        pmodel.General_Phone2 = pmodel.General_PhoneFirstThreeDigit2 + "-" + pmodel.General_PhoneSecondThreeDigit2 + "-" + pmodel.General_PhoneLastFourDigit2;
                                        if (pmodel.General_Phone2.Length > 13)
                                        {
                                            pmodel.General_Phone2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.Telephone;
                                        }
                                    }


                                    pmodel.General_Email2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].Facility.EmailAddress;

                                    if (secondaryPracticeLocation[secondaryPracticeLocationCount - 2].Facility.Fax != null)
                                    {
                                        //pmodel.General_Fax1 = practiceLocation[secondaryPracticeLocationCount - 1].Facility.Fax.Substring(3, practiceLocation[secondaryPracticeLocationCount - 2].Facility.Fax.Length - 3);

                                        pmodel.General_FaxFirstThreeDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].Facility.Fax.Substring(0, secondaryPracticeLocation[secondaryPracticeLocationCount - 2].Facility.Fax.Length - 7);
                                        pmodel.General_FaxSecondThreeDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].Facility.Fax.Substring(3, 3);
                                        pmodel.General_FaxLastFourDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].Facility.Fax.Substring(6);

                                        pmodel.General_Fax2 = pmodel.General_FaxFirstThreeDigit2 + "-" + pmodel.General_FaxSecondThreeDigit2 + "-" + pmodel.General_FaxLastFourDigit2;
                                        if (pmodel.General_Fax2.Length > 13)
                                        {
                                            pmodel.General_Fax2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 1].Facility.Fax;
                                        }
                                    }

                                    #region Languages

                                    if (secondaryPracticeLocation[secondaryPracticeLocationCount - 2].Facility.FacilityDetail != null && secondaryPracticeLocation[secondaryPracticeLocationCount - 2].Facility.FacilityDetail.Language != null)
                                    {
                                        var languages = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].Facility.FacilityDetail.Language.NonEnglishLanguages.Where(l => l.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();
                                        if (languages.Count > 0)
                                        {
                                            foreach (var language in languages)
                                            {
                                                if (language != null)
                                                    pmodel.Languages_Known2 += language.Language + ",";
                                            }
                                        }
                                    }
                                    #endregion
                                }

                                #endregion

                                #region Office Manager 2

                                if (secondaryPracticeLocation[secondaryPracticeLocationCount - 2].BusinessOfficeManagerOrStaff != null)
                                {
                                    if (secondaryPracticeLocation[secondaryPracticeLocationCount - 2].BusinessOfficeManagerOrStaff.MiddleName != null)
                                        pmodel.OfficeManager_Name2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].BusinessOfficeManagerOrStaff.FirstName + " " + secondaryPracticeLocation[secondaryPracticeLocationCount - 2].BusinessOfficeManagerOrStaff.MiddleName + " " + secondaryPracticeLocation[secondaryPracticeLocationCount - 2].BusinessOfficeManagerOrStaff.LastName;
                                    else
                                        pmodel.OfficeManager_Name2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].BusinessOfficeManagerOrStaff.FirstName + " " + secondaryPracticeLocation[secondaryPracticeLocationCount - 2].BusinessOfficeManagerOrStaff.LastName;

                                    //pmodel.OfficeManager_Name2 = practiceLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.FirstName + practiceLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.MiddleName + practiceLocation[secondaryPracticeLocationCount - 1].BusinessOfficeManagerOrStaff.LastName;
                                    pmodel.OfficeManager_Email2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].BusinessOfficeManagerOrStaff.EmailAddress;
                                    pmodel.OfficeManager_PoBoxAddress2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].BusinessOfficeManagerOrStaff.POBoxAddress;
                                    pmodel.OfficeManager_Building2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].BusinessOfficeManagerOrStaff.Building;
                                    pmodel.OfficeManager_Street2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].BusinessOfficeManagerOrStaff.Street;
                                    pmodel.OfficeManager_City2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].BusinessOfficeManagerOrStaff.City;
                                    pmodel.OfficeManager_State2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].BusinessOfficeManagerOrStaff.State;
                                    pmodel.OfficeManager_ZipCode2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].BusinessOfficeManagerOrStaff.ZipCode;
                                    pmodel.OfficeManager_Country2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].BusinessOfficeManagerOrStaff.Country;
                                    pmodel.OfficeManager_County2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].BusinessOfficeManagerOrStaff.County;

                                    if (secondaryPracticeLocation[secondaryPracticeLocationCount - 2].BusinessOfficeManagerOrStaff.Telephone != null)
                                    {
                                        pmodel.OfficeManager_PhoneFirstThreeDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].BusinessOfficeManagerOrStaff.Telephone.Substring(0, secondaryPracticeLocation[secondaryPracticeLocationCount - 2].BusinessOfficeManagerOrStaff.Telephone.Length - 7);
                                        pmodel.OfficeManager_PhoneSecondThreeDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].BusinessOfficeManagerOrStaff.Telephone.Substring(3, 3);
                                        pmodel.OfficeManager_PhoneLastFourDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].BusinessOfficeManagerOrStaff.Telephone.Substring(6);

                                        pmodel.OfficeManager_Phone2 = pmodel.OfficeManager_PhoneFirstThreeDigit2 + "-" + pmodel.OfficeManager_PhoneSecondThreeDigit2 + "-" + pmodel.OfficeManager_PhoneLastFourDigit2;
                                    }




                                    if (secondaryPracticeLocation[secondaryPracticeLocationCount - 2].BusinessOfficeManagerOrStaff.Fax != null)
                                    {
                                        //pmodel.General_Fax1 = practiceLocation[secondaryPracticeLocationCount - 2].Facility.Fax.Substring(3, practiceLocation[secondaryPracticeLocationCount - 2].Facility.Fax.Length - 3);

                                        pmodel.OfficeManager_FaxFirstThreeDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].BusinessOfficeManagerOrStaff.Fax.Substring(0, secondaryPracticeLocation[secondaryPracticeLocationCount - 2].BusinessOfficeManagerOrStaff.Fax.Length - 7);
                                        pmodel.OfficeManager_FaxSecondThreeDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].BusinessOfficeManagerOrStaff.Fax.Substring(3, 3);
                                        pmodel.OfficeManager_FaxLastFourDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].BusinessOfficeManagerOrStaff.Fax.Substring(6);

                                        pmodel.OfficeManager_Fax2 = pmodel.OfficeManager_FaxFirstThreeDigit2 + "-" + pmodel.OfficeManager_FaxSecondThreeDigit2 + "-" + pmodel.OfficeManager_FaxLastFourDigit2;
                                    }
                                }

                                #endregion

                                #region Billing Contact 2

                                if (secondaryPracticeLocation[secondaryPracticeLocationCount - 2].BillingContactPerson != null)
                                {
                                    if (secondaryPracticeLocation[secondaryPracticeLocationCount - 2].BillingContactPerson.MiddleName != null)
                                        pmodel.BillingContact_Name2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].BillingContactPerson.FirstName + " " + secondaryPracticeLocation[secondaryPracticeLocationCount - 2].BillingContactPerson.MiddleName + " " + secondaryPracticeLocation[secondaryPracticeLocationCount - 2].BillingContactPerson.LastName;
                                    else
                                        pmodel.BillingContact_Name2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].BillingContactPerson.FirstName + " " + secondaryPracticeLocation[secondaryPracticeLocationCount - 2].BillingContactPerson.LastName;

                                    //pmodel.BillingContact_Name2 = practiceLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.FirstName + practiceLocation[secondaryPracticeLocationCount - 1].BillingContactPerson.MiddleName + practiceLocation[secondaryPracticeLocationCount - 2].BillingContactPerson.LastName;
                                    pmodel.BillingContact_FirstName2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].BillingContactPerson.FirstName;
                                    pmodel.BillingContact_MiddleName2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].BillingContactPerson.MiddleName;
                                    pmodel.BillingContact_LastName2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].BillingContactPerson.LastName;
                                    pmodel.BillingContact_Email2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].BillingContactPerson.EmailAddress;
                                    pmodel.BillingContact_POBoxAddress2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].BillingContactPerson.POBoxAddress;
                                    pmodel.BillingContact_Suite2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].BillingContactPerson.Building;
                                    pmodel.BillingContact_Street2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].BillingContactPerson.Street;
                                    pmodel.BillingContact_City2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].BillingContactPerson.City;
                                    pmodel.BillingContact_State2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].BillingContactPerson.State;
                                    pmodel.BillingContact_ZipCode2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].BillingContactPerson.ZipCode;
                                    pmodel.BillingContact_Country2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].BillingContactPerson.Country;
                                    pmodel.BillingContact_County2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].BillingContactPerson.County;


                                    if (secondaryPracticeLocation[secondaryPracticeLocationCount - 2].BillingContactPerson.Telephone != null)
                                    {
                                        pmodel.BillingContact_PhoneFirstThreeDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].BillingContactPerson.Telephone.Substring(0, secondaryPracticeLocation[secondaryPracticeLocationCount - 2].BillingContactPerson.Telephone.Length - 7);
                                        pmodel.BillingContact_PhoneSecondThreeDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].BillingContactPerson.Telephone.Substring(3, 3);
                                        pmodel.BillingContact_PhoneLastFourDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].BillingContactPerson.Telephone.Substring(6);

                                        pmodel.BillingContact_Phone2 = pmodel.BillingContact_PhoneFirstThreeDigit2 + "-" + pmodel.BillingContact_PhoneSecondThreeDigit2 + "-" + pmodel.BillingContact_PhoneLastFourDigit2;
                                    }




                                    if (secondaryPracticeLocation[secondaryPracticeLocationCount - 2].BillingContactPerson.Fax != null)
                                    {
                                        //pmodel.General_Fax1 = practiceLocation[secondaryPracticeLocationCount - 1].Facility.Fax.Substring(3, practiceLocation[secondaryPracticeLocationCount - 1].Facility.Fax.Length - 3);

                                        pmodel.BillingContact_FaxFirstThreeDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].BillingContactPerson.Fax.Substring(0, secondaryPracticeLocation[secondaryPracticeLocationCount - 2].BillingContactPerson.Fax.Length - 7);
                                        pmodel.BillingContact_FaxSecondThreeDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].BillingContactPerson.Fax.Substring(3, 3);
                                        pmodel.BillingContact_FaxLastFourDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].BillingContactPerson.Fax.Substring(6);

                                        pmodel.BillingContact_Fax2 = pmodel.BillingContact_FaxFirstThreeDigit2 + "-" + pmodel.BillingContact_FaxSecondThreeDigit2 + "-" + pmodel.BillingContact_FaxLastFourDigit2;
                                    }

                                }

                                #endregion

                                #region Payment and Remittance 2

                                if (secondaryPracticeLocation[secondaryPracticeLocationCount - 2].PaymentAndRemittance != null)
                                {
                                    if (secondaryPracticeLocation[secondaryPracticeLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson != null)
                                    {
                                        if (secondaryPracticeLocation[secondaryPracticeLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.MiddleName != null)
                                            pmodel.PaymentRemittance_Name2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.FirstName + " " + secondaryPracticeLocation[secondaryPracticeLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.MiddleName + " " + secondaryPracticeLocation[secondaryPracticeLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.LastName;
                                        else
                                            pmodel.PaymentRemittance_Name2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.FirstName + " " + secondaryPracticeLocation[secondaryPracticeLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.LastName;

                                        //pmodel.PaymentRemittance_Name2 = practiceLocation[secondaryPracticeLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.FirstName + practiceLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.MiddleName + practiceLocation[secondaryPracticeLocationCount - 1].PaymentAndRemittance.PaymentAndRemittancePerson.LastName;
                                        pmodel.PaymentRemittance_FirstName2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.FirstName;
                                        pmodel.PaymentRemittance_MiddleName2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.MiddleName;
                                        pmodel.PaymentRemittance_LastName2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.LastName;
                                        pmodel.PaymentRemittance_Email2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.EmailAddress;
                                        pmodel.PaymentRemittance_POBoxAddress2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.POBoxAddress;
                                        pmodel.PaymentRemittance_Suite2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.Building;
                                        pmodel.PaymentRemittance_Street2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.Street;
                                        pmodel.PaymentRemittance_City2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.City;
                                        pmodel.PaymentRemittance_State2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.State;
                                        pmodel.PaymentRemittance_ZipCode2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.ZipCode;
                                        pmodel.PaymentRemittance_Country2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.Country;
                                        pmodel.PaymentRemittance_County2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.County;

                                        if (secondaryPracticeLocation[secondaryPracticeLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.Telephone != null)
                                        {
                                            pmodel.PaymentRemittance_PhoneFirstThreeDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.Telephone.Substring(0, secondaryPracticeLocation[secondaryPracticeLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.Telephone.Length - 7);
                                            pmodel.PaymentRemittance_PhoneSecondThreeDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.Telephone.Substring(3, 3);
                                            pmodel.PaymentRemittance_PhoneLastFourDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.Telephone.Substring(6);

                                            pmodel.PaymentRemittance_Phone2 = pmodel.PaymentRemittance_PhoneFirstThreeDigit2 + "-" + pmodel.PaymentRemittance_PhoneSecondThreeDigit2 + "-" + pmodel.PaymentRemittance_PhoneLastFourDigit2;
                                        }




                                        if (secondaryPracticeLocation[secondaryPracticeLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.Fax != null)
                                        {
                                            //pmodel.General_Fax1 = practiceLocation[secondaryPracticeLocationCount - 1].Facility.Fax.Substring(3, practiceLocation[practiceLocationCount - 1].Facility.Fax.Length - 3);

                                            pmodel.PaymentRemittance_FaxFirstThreeDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.Fax.Substring(0, secondaryPracticeLocation[secondaryPracticeLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.Fax.Length - 7);
                                            pmodel.PaymentRemittance_FaxSecondThreeDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.Fax.Substring(3, 3);
                                            pmodel.PaymentRemittance_FaxLastFourDigit2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.Fax.Substring(6);

                                            pmodel.PaymentRemittance_Fax2 = pmodel.PaymentRemittance_FaxFirstThreeDigit2 + "-" + pmodel.PaymentRemittance_FaxSecondThreeDigit2 + "-" + pmodel.PaymentRemittance_FaxLastFourDigit2;
                                        }
                                    }
                                    pmodel.PaymentRemittance_ElectronicBillCapability2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].PaymentAndRemittance.ElectronicBillingCapability;
                                    pmodel.PaymentRemittance_BillingDepartment2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].PaymentAndRemittance.BillingDepartment;
                                    pmodel.PaymentRemittance_ChekPayableTo2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].PaymentAndRemittance.CheckPayableTo;
                                    pmodel.PaymentRemittance_Office2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].PaymentAndRemittance.Office;

                                }

                                #endregion

                                #region Open Practice Status 2

                                if (secondaryPracticeLocation[secondaryPracticeLocationCount - 2].OpenPracticeStatus != null)
                                {
                                    pmodel.OpenPractice_AgeLimitations2 = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].OpenPracticeStatus.MinimumAge + " - " + secondaryPracticeLocation[secondaryPracticeLocationCount - 2].OpenPracticeStatus.MaximumAge;
                                }

                                #endregion

                                #region Office Hours 2

                                if (secondaryPracticeLocation[secondaryPracticeLocationCount - 2].OfficeHour != null)
                                {
                                    if (secondaryPracticeLocation[secondaryPracticeLocationCount - 2].OfficeHour.PracticeDays.Count > 0 && secondaryPracticeLocation[secondaryPracticeLocationCount - 2].OfficeHour.PracticeDays.First().DailyHours.Count > 0)
                                    {


                                        pmodel.OfficeHour_StartMonday2 = ConvertTimeFormat(secondaryPracticeLocation[secondaryPracticeLocationCount - 2].OfficeHour.PracticeDays.ElementAt(0).DailyHours.First().StartTime);
                                        pmodel.OfficeHour_EndMonday2 = ConvertTimeFormat(secondaryPracticeLocation[secondaryPracticeLocationCount - 2].OfficeHour.PracticeDays.ElementAt(0).DailyHours.First().EndTime);

                                        pmodel.OfficeHour_Monday2 = pmodel.OfficeHour_StartMonday2 + " - " + pmodel.OfficeHour_EndMonday2;

                                        pmodel.OfficeHour_StartTuesday2 = ConvertTimeFormat(secondaryPracticeLocation[secondaryPracticeLocationCount - 2].OfficeHour.PracticeDays.ElementAt(1).DailyHours.First().StartTime);
                                        pmodel.OfficeHour_EndTuesday2 = ConvertTimeFormat(secondaryPracticeLocation[secondaryPracticeLocationCount - 2].OfficeHour.PracticeDays.ElementAt(1).DailyHours.First().EndTime);

                                        pmodel.OfficeHour_Tuesday2 = pmodel.OfficeHour_StartTuesday2 + " - " + pmodel.OfficeHour_EndTuesday2;

                                        pmodel.OfficeHour_StartWednesday2 = ConvertTimeFormat(secondaryPracticeLocation[secondaryPracticeLocationCount - 2].OfficeHour.PracticeDays.ElementAt(2).DailyHours.First().StartTime);
                                        pmodel.OfficeHour_EndWednesday2 = ConvertTimeFormat(secondaryPracticeLocation[secondaryPracticeLocationCount - 2].OfficeHour.PracticeDays.ElementAt(2).DailyHours.First().EndTime);

                                        pmodel.OfficeHour_Wednesday2 = pmodel.OfficeHour_StartWednesday2 + " - " + pmodel.OfficeHour_EndWednesday2;

                                        pmodel.OfficeHour_StartThursday2 = ConvertTimeFormat(secondaryPracticeLocation[secondaryPracticeLocationCount - 2].OfficeHour.PracticeDays.ElementAt(3).DailyHours.First().StartTime);
                                        pmodel.OfficeHour_EndThursday2 = ConvertTimeFormat(secondaryPracticeLocation[secondaryPracticeLocationCount - 2].OfficeHour.PracticeDays.ElementAt(3).DailyHours.First().EndTime);

                                        pmodel.OfficeHour_Thursday2 = pmodel.OfficeHour_StartThursday2 + " - " + pmodel.OfficeHour_EndThursday2;

                                        pmodel.OfficeHour_StartFriday2 = ConvertTimeFormat(secondaryPracticeLocation[secondaryPracticeLocationCount - 2].OfficeHour.PracticeDays.ElementAt(4).DailyHours.First().StartTime);
                                        pmodel.OfficeHour_EndFriday2 = ConvertTimeFormat(secondaryPracticeLocation[secondaryPracticeLocationCount - 2].OfficeHour.PracticeDays.ElementAt(4).DailyHours.First().EndTime);

                                        pmodel.OfficeHour_Friday2 = pmodel.OfficeHour_StartFriday2 + " - " + pmodel.OfficeHour_EndFriday2;

                                        pmodel.OfficeHour_StartSaturday2 = ConvertTimeFormat(secondaryPracticeLocation[secondaryPracticeLocationCount - 2].OfficeHour.PracticeDays.ElementAt(5).DailyHours.First().StartTime);
                                        pmodel.OfficeHour_EndSaturday2 = ConvertTimeFormat(secondaryPracticeLocation[secondaryPracticeLocationCount - 2].OfficeHour.PracticeDays.ElementAt(5).DailyHours.First().EndTime);

                                        pmodel.OfficeHour_Saturday2 = pmodel.OfficeHour_StartSaturday2 + " - " + pmodel.OfficeHour_EndSaturday2;

                                        pmodel.OfficeHour_StartSunday2 = ConvertTimeFormat(secondaryPracticeLocation[secondaryPracticeLocationCount - 2].OfficeHour.PracticeDays.ElementAt(6).DailyHours.First().StartTime);
                                        pmodel.OfficeHour_EndSunday2 = ConvertTimeFormat(secondaryPracticeLocation[secondaryPracticeLocationCount - 2].OfficeHour.PracticeDays.ElementAt(6).DailyHours.First().EndTime);

                                        pmodel.OfficeHour_Sunday2 = pmodel.OfficeHour_StartSunday2 + " - " + pmodel.OfficeHour_EndSunday2;
                                    }
                                }

                                #endregion

                                #region Supervising Provider 2

                                List<PracticeProvider> supervisingProvider = new List<PracticeProvider>();

                                if (secondaryPracticeLocation[secondaryPracticeLocationCount - 2].PracticeProviders.Count > 0)
                                {
                                    supervisingProvider = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].PracticeProviders.
                                Where(s => s.Practice == AHC.CD.Entities.MasterData.Enums.PracticeType.Supervisor.ToString() &&
                                    s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();
                                }


                                var supervisorCount = supervisingProvider.Count;

                                if (supervisorCount > 0)
                                {
                                    pmodel.CoveringColleague_FirstName2 = supervisingProvider.ElementAt(supervisorCount - 1).FirstName;
                                    pmodel.CoveringColleague_MiddleName2 = supervisingProvider.ElementAt(supervisorCount - 1).MiddleName;
                                    pmodel.CoveringColleague_LastName2 = supervisingProvider.ElementAt(supervisorCount - 1).LastName;

                                    if (pmodel.CoveringColleague_MiddleName2 != null)
                                        pmodel.CoveringColleague_FullName2 = pmodel.CoveringColleague_FirstName2 + " " + pmodel.CoveringColleague_MiddleName2 + " " + pmodel.CoveringColleague_LastName2;
                                    else
                                        pmodel.CoveringColleague_FullName2 = pmodel.CoveringColleague_FirstName2 + " " + pmodel.CoveringColleague_LastName2;


                                    if (supervisingProvider.ElementAt(supervisorCount - 1).Telephone != null)
                                    {
                                        pmodel.CoveringColleague_PhoneFirstThreeDigit2 = supervisingProvider.ElementAt(supervisorCount - 1).Telephone.Substring(0, supervisingProvider.ElementAt(supervisorCount - 1).Telephone.Length - 7);
                                        pmodel.CoveringColleague_PhoneSecondThreeDigit2 = supervisingProvider.ElementAt(supervisorCount - 1).Telephone.Substring(3, 3);
                                        pmodel.CoveringColleague_PhoneLastFourDigit2 = supervisingProvider.ElementAt(supervisorCount - 1).Telephone.Substring(6);

                                        pmodel.CoveringColleague_PhoneNumber2 = pmodel.CoveringColleague_PhoneFirstThreeDigit2 + "-" + pmodel.CoveringColleague_PhoneSecondThreeDigit2 + "-" + pmodel.CoveringColleague_PhoneLastFourDigit2;

                                    }

                                    var specialities = supervisingProvider.ElementAt(supervisorCount - 1).PracticeProviderSpecialties.Where(s => s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString()).ToList();

                                    if (specialities.Count > 0)
                                    {
                                        if (specialities.ElementAt(specialities.Count - 1).Specialty != null)
                                            pmodel.CoveringColleague_Specialty2 = specialities.ElementAt(specialities.Count - 1).Specialty.Name;
                                    }
                                }

                                #endregion

                                #region Covering Colleagues/Partners 2

                                List<PracticeProvider> patners = new List<PracticeProvider>();

                                if (secondaryPracticeLocation[secondaryPracticeLocationCount - 2].PracticeProviders.Count > 0)
                                {

                                    patners = secondaryPracticeLocation[secondaryPracticeLocationCount - 2].PracticeProviders.
                                    Where(s => s.Practice == AHC.CD.Entities.MasterData.Enums.PracticeType.CoveringColleague.ToString() &&
                                        s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();
                                }
                                var patnersCount = patners.Count;

                                if (patnersCount > 0)
                                {
                                    pmodel.Patners_FirstName2 = patners.ElementAt(patnersCount - 1).FirstName;
                                    pmodel.Patners_MiddleName2 = patners.ElementAt(patnersCount - 1).MiddleName;
                                    pmodel.Patners_LastName2 = patners.ElementAt(patnersCount - 1).LastName;

                                    if (pmodel.Patners_MiddleName2 != null)
                                        pmodel.Patners_FullName2 = pmodel.Patners_FirstName2 + " " + pmodel.Patners_MiddleName2 + " " + pmodel.Patners_LastName2;
                                    else
                                        pmodel.Patners_FullName2 = pmodel.Patners_FirstName2 + " " + pmodel.Patners_LastName2;
                                    if (supervisorCount > 0)
                                    {
                                        if (supervisingProvider.ElementAt(supervisorCount - 1).Telephone != null)
                                        {
                                            pmodel.Patners_PhoneFirstThreeDigit2 = supervisingProvider.ElementAt(supervisorCount - 1).Telephone.Substring(0, supervisingProvider.ElementAt(supervisorCount - 1).Telephone.Length - 7);
                                            pmodel.Patners_PhoneSecondThreeDigit2 = supervisingProvider.ElementAt(supervisorCount - 1).Telephone.Substring(3, 3);
                                            pmodel.Patners_PhoneLastFourDigit2 = supervisingProvider.ElementAt(supervisorCount - 1).Telephone.Substring(6);

                                            pmodel.Patners_PhoneNumber2 = pmodel.CoveringColleague_PhoneFirstThreeDigit2 + "-" + pmodel.CoveringColleague_PhoneSecondThreeDigit2 + "-" + pmodel.CoveringColleague_PhoneLastFourDigit2;

                                        }


                                        var specialities = supervisingProvider.ElementAt(supervisorCount - 1).PracticeProviderSpecialties.Where(s => s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString()).ToList();

                                        if (specialities.Count > 0)
                                        {
                                            if (specialities.ElementAt(specialities.Count - 1).Specialty != null)
                                                pmodel.Patners_Specialty2 = specialities.ElementAt(specialities.Count - 1).Specialty.Name;
                                        }
                                    }
                                }

                                #endregion
                            }
                            #endregion
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
                            pmodel.StateLicense_ExpirationDate1_yyyy = pmodel.StateLicense_ExpirationDate1.Split('-')[2].Substring(2);
                        }

                        if (pmodel.StateLicense_CurrentIssueDate1 != null)
                        {
                            pmodel.StateLicense_CurrentIssueDate1_MM = pmodel.StateLicense_CurrentIssueDate1.Split('-')[0];
                            pmodel.StateLicense_CurrentIssueDate1_dd = pmodel.StateLicense_CurrentIssueDate1.Split('-')[1];
                            pmodel.StateLicense_CurrentIssueDate1_yyyy = pmodel.StateLicense_CurrentIssueDate1.Split('-')[2].Substring(2);
                        }

                        if (pmodel.StateLicense_OriginalIssueDate1 != null)
                        {
                            pmodel.StateLicense_OriginalIssueDate1_MM = pmodel.StateLicense_OriginalIssueDate1.Split('-')[0];
                            pmodel.StateLicense_OriginalIssueDate1_dd = pmodel.StateLicense_OriginalIssueDate1.Split('-')[1];
                            pmodel.StateLicense_OriginalIssueDate1_yyyy = pmodel.StateLicense_OriginalIssueDate1.Split('-')[2].Substring(2);
                        }

                    }

                    if (stateLicenseCount > 1)
                    {
                        if (stateLicense.ElementAt(stateLicenseCount - 2).ProviderType != null)
                        {
                            pmodel.StateLicense_Type2 = stateLicense.ElementAt(stateLicenseCount - 2).ProviderType.Title;
                        }
                        pmodel.StateLicense_Number2 = stateLicense.ElementAt(stateLicenseCount - 2).LicenseNumber;
                        pmodel.StateLicense_ExpirationDate2 = ConvertToDateString(stateLicense.ElementAt(stateLicenseCount - 2).ExpiryDate);
                        pmodel.StateLicense_CurrentIssueDate2 = ConvertToDateString(stateLicense.ElementAt(stateLicenseCount - 2).CurrentIssueDate);
                        pmodel.StateLicense_OriginalIssueDate2 = ConvertToDateString(stateLicense.ElementAt(stateLicenseCount - 2).IssueDate);
                        pmodel.StateLicense_IssuingState2 = stateLicense.ElementAt(stateLicenseCount - 2).IssueState;
                        if (stateLicense.ElementAt(stateLicenseCount - 2).StateLicenseStatus != null)
                        {
                            pmodel.StateLicense_Status2 = stateLicense.ElementAt(stateLicenseCount - 2).StateLicenseStatus.Title.ToString();
                        }
                    }

                    if (stateLicenseCount > 2)
                    {
                        if (stateLicense.ElementAt(stateLicenseCount - 3).ProviderType != null)
                        {
                            pmodel.StateLicense_Type3 = stateLicense.ElementAt(stateLicenseCount - 3).ProviderType.Title;
                        }
                        pmodel.StateLicense_Number3 = stateLicense.ElementAt(stateLicenseCount - 3).LicenseNumber;
                        pmodel.StateLicense_ExpirationDate3 = ConvertToDateString(stateLicense.ElementAt(stateLicenseCount - 3).ExpiryDate);
                        pmodel.StateLicense_CurrentIssueDate3 = ConvertToDateString(stateLicense.ElementAt(stateLicenseCount - 3).CurrentIssueDate);
                        pmodel.StateLicense_OriginalIssueDate3 = ConvertToDateString(stateLicense.ElementAt(stateLicenseCount - 3).IssueDate);
                        pmodel.StateLicense_IssuingState3 = stateLicense.ElementAt(stateLicenseCount - 3).IssueState;
                        if (stateLicense.ElementAt(stateLicenseCount - 3).StateLicenseStatus != null)
                        {
                            pmodel.StateLicense_Status3 = stateLicense.ElementAt(stateLicenseCount - 3).StateLicenseStatus.Title.ToString();
                        }
                    }
                    if (stateLicenseCount > 3)
                    {
                        if (stateLicense.ElementAt(stateLicenseCount - 4).ProviderType != null)
                        {
                            pmodel.StateLicense_Type4 = stateLicense.ElementAt(stateLicenseCount - 4).ProviderType.Title;
                        }
                        pmodel.StateLicense_Number4 = stateLicense.ElementAt(stateLicenseCount - 4).LicenseNumber;
                        pmodel.StateLicense_ExpirationDate4 = ConvertToDateString(stateLicense.ElementAt(stateLicenseCount - 4).ExpiryDate);
                        pmodel.StateLicense_CurrentIssueDate4 = ConvertToDateString(stateLicense.ElementAt(stateLicenseCount - 4).CurrentIssueDate);
                        pmodel.StateLicense_OriginalIssueDate4 = ConvertToDateString(stateLicense.ElementAt(stateLicenseCount - 4).IssueDate);
                        pmodel.StateLicense_IssuingState4 = stateLicense.ElementAt(stateLicenseCount - 4).IssueState;
                        if (stateLicense.ElementAt(stateLicenseCount - 4).StateLicenseStatus != null)
                        {
                            pmodel.StateLicense_Status4 = stateLicense.ElementAt(stateLicenseCount - 4).StateLicenseStatus.Title.ToString();
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
                        pmodel.Medicare_Number1 = medicareInfo.ElementAt(medicareInfoCount - 1).LicenseNumber;
                    }

                    foreach (var item in profile.MedicareInformations)
                    {
                        if (item != null)
                            pmodel.MedicareLicenceNoList += item.LicenseNumber + ", ";
                    }
                }

                if (profile.MedicaidInformations.Count > 0)
                {
                    var medicaidInfo = profile.MedicaidInformations.Where(s => s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();
                    var medicidInfoCount = medicaidInfo.Count;
                    if (medicaidInfo.ElementAt(medicidInfoCount - 1) != null)
                    {
                        pmodel.MedicaidNumber1 = medicaidInfo.ElementAt(medicidInfoCount - 1).LicenseNumber;
                        pmodel.Medicaid_State1 = medicaidInfo.ElementAt(medicidInfoCount - 1).State;
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
                        pmodel.UnderGraduation_Type1 = educationDetails.ElementAt(educationDetailsCount - 1).QualificationType;
                        pmodel.Graduation_Type1 = pmodel.UnderGraduation_Type1;

                        if (educationDetails.ElementAt(educationDetailsCount - 1).SchoolInformation != null)
                        {
                            pmodel.UnderGraduation_SchoolName1 = educationDetails.ElementAt(educationDetailsCount - 1).SchoolInformation.SchoolName;
                            //pmodel.Provider_Highest_Degree_SchoolName = pmodel.UnderGraduation_SchoolName1;
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
                        pmodel.UnderGraduation_Degree1 = educationDetails.ElementAt(educationDetailsCount - 1).QualificationDegree;
                        //pmodel.Provider_Highest_Degree = pmodel.UnderGraduation_Degree1;
                        pmodel.UnderGraduation_StartDate1 = ConvertToDateString(educationDetails.ElementAt(educationDetailsCount - 1).StartDate);
                        pmodel.UnderGraduation_EndDate1 = ConvertToDateString(educationDetails.ElementAt(educationDetailsCount - 1).EndDate);
                        //pmodel.Provider_Highest_Degree_StartDate = pmodel.UnderGraduation_StartDate1;
                        //pmodel.Provider_Highest_Degree_EndDate = pmodel.UnderGraduation_EndDate1;
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

                        pmodel.Graduation_Type1 = educationDetails.ElementAt(educationDetailsCount - 1).GraduationType;

                        if (educationDetails.ElementAt(educationDetailsCount - 1).SchoolInformation != null)
                        {

                            pmodel.Graduation_SchoolName1 = educationDetails.ElementAt(educationDetailsCount - 1).SchoolInformation.SchoolName;
                            pmodel.Provider_Highest_Degree_SchoolName = pmodel.Graduation_SchoolName1;
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
                        pmodel.Graduation_Degree1 = educationDetails.ElementAt(educationDetailsCount - 1).QualificationDegree;
                        pmodel.Provider_Highest_Degree = pmodel.Graduation_Degree1;
                        pmodel.Graduation_StartDate1 = ConvertToDateString(educationDetails.ElementAt(educationDetailsCount - 1).StartDate);
                        pmodel.Graduation_EndDate1 = ConvertToDateString(educationDetails.ElementAt(educationDetailsCount - 1).EndDate);
                        pmodel.Provider_Highest_Degree_StartDate = pmodel.Graduation_StartDate1;
                        pmodel.Provider_Highest_Degree_EndDate = pmodel.Graduation_EndDate1;
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

                if (profile.ProgramDetails.Count > 0)
                {
                    int residencyCount = 0;
                    foreach (var program in profile.ProgramDetails)
                    {
                        if (program.ProgramType == "Internship")
                        {
                            if (program.SchoolInformation != null)
                            {
                                pmodel.InternshipFacility = program.SchoolInformation.SchoolName;
                                pmodel.InternshipFacility_City = program.SchoolInformation.City;
                                pmodel.InternshipFacility_Street = program.SchoolInformation.Street;
                                pmodel.InternshipFacility_State = program.SchoolInformation.State;
                                pmodel.InternshipFacility_ZipCode = program.SchoolInformation.ZipCode;
                                pmodel.InternshipFacility_Country = program.SchoolInformation.Country;
                                pmodel.InternshipFacility_EmailAddress = program.SchoolInformation.Email;
                            }
                            if (program.Specialty != null)
                            {
                                pmodel.InternshipSpecialty = program.Specialty.Name;
                            }
                            string startDate = ConvertToDateString(program.StartDate);
                            string endDate = ConvertToDateString(program.EndDate);
                            pmodel.InternshipStartDate = startDate;
                            pmodel.InternshipEndDate = endDate;
                            pmodel.InternshipAttendedDate = startDate + " - " + endDate;
                        }
                        else if (program.ProgramType == "Resident")
                        {
                            if (residencyCount == 0 && program.SchoolInformation != null)
                            {
                                pmodel.ResidencyFacility = program.SchoolInformation.SchoolName;
                                pmodel.ResidencyFacility_City = program.SchoolInformation.City;
                                pmodel.ResidencyFacility_Street = program.SchoolInformation.Street;
                                pmodel.ResidencyFacility_State = program.SchoolInformation.State;
                                pmodel.ResidencyFacility_ZipCode = program.SchoolInformation.ZipCode;
                                pmodel.ResidencyFacility_Country = program.SchoolInformation.Country;
                                pmodel.ResidencyFacility_EmailAddress = program.SchoolInformation.Email;
                            }
                            if (program.Specialty != null)
                            {
                                pmodel.ResidencySpecialty = program.Specialty.Name;
                            }
                            string startDate = ConvertToDateString(program.StartDate);
                            string endDate = ConvertToDateString(program.EndDate);
                            pmodel.ResidencyStartDate = startDate;
                            pmodel.ResidencyEndDate = endDate;
                            pmodel.ResidencyAttendedDate = startDate + " - " + endDate;
                            if (residencyCount == 1)
                            {
                                if (program.SchoolInformation != null)
                                {
                                    pmodel.ResidencyFacility1 = program.SchoolInformation.SchoolName;
                                    pmodel.ResidencyFacility_City1 = program.SchoolInformation.City;
                                    pmodel.ResidencyFacility_Street1 = program.SchoolInformation.Street;
                                    pmodel.ResidencyFacility_State1 = program.SchoolInformation.State;
                                    pmodel.ResidencyFacility_ZipCode1 = program.SchoolInformation.ZipCode;
                                    pmodel.ResidencyFacility_Country1 = program.SchoolInformation.Country;
                                    pmodel.ResidencyFacility_EmailAddress1 = program.SchoolInformation.Email;
                                }
                                if (program.Specialty != null)
                                {
                                    pmodel.ResidencySpecialty1 = program.Specialty.Name;
                                }
                                string startDate1 = ConvertToDateString(program.StartDate);
                                string endDate1 = ConvertToDateString(program.EndDate);
                                pmodel.ResidencyStartDate1 = startDate1;
                                pmodel.ResidencyEndDate1 = endDate1;
                                pmodel.ResidencyAttendedDate1 = startDate1 + " - " + endDate1;
                            }
                            residencyCount++;
                        }
                        else if (program.ProgramType == "Fellowship")
                        {
                            if (program.SchoolInformation != null)
                            {
                                pmodel.FellowshipFacility = program.SchoolInformation.SchoolName;
                                pmodel.FellowshipFacility_City = program.SchoolInformation.City;
                                pmodel.FellowshipFacility_Street = program.SchoolInformation.Street;
                                pmodel.FellowshipFacility_State = program.SchoolInformation.State;
                                pmodel.FellowshipFacility_ZipCode = program.SchoolInformation.ZipCode;
                                pmodel.FellowshipFacility_Country = program.SchoolInformation.Country;
                                pmodel.FellowshipFacility_EmailAddress = program.SchoolInformation.Email;
                            }
                            if (program.Specialty != null)
                            {
                                pmodel.FellowshipSpecialty = program.Specialty.Name;
                            }
                            string startDate = ConvertToDateString(program.StartDate);
                            string endDate = ConvertToDateString(program.EndDate);
                            pmodel.FellowshipStartDate = startDate;
                            pmodel.FellowshipEndDate = endDate;
                            pmodel.FellowshipAttendedDate = startDate + " - " + endDate;
                        }
                    }
                }

                #endregion

                #region Post Graduation

                if (profile.EducationDetails.Count > 0)
                {
                    var educationDetails = profile.EducationDetails.Where(s => s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString() && s.EducationQualificationType == AHC.CD.Entities.MasterData.Enums.EducationQualificationType.PostGraduate).ToList();
                    var educationDetailsCount = educationDetails.Count;

                    if (educationDetailsCount > 0 && educationDetails.ElementAt(educationDetailsCount - 1) != null)
                    {
                        pmodel.UnderGraduation_Type1 = educationDetails.ElementAt(educationDetailsCount - 1).QualificationType;
                        pmodel.Graduation_Type1 = pmodel.UnderGraduation_Type1;

                        if (educationDetails.ElementAt(educationDetailsCount - 1).SchoolInformation != null)
                        {
                            pmodel.UnderGraduation_SchoolName3 = educationDetails.ElementAt(educationDetailsCount - 1).SchoolInformation.SchoolName;
                            //pmodel.Provider_Highest_Degree_SchoolName = pmodel.UnderGraduation_SchoolName1;
                            pmodel.UnderGraduation_EmailId3 = educationDetails.ElementAt(educationDetailsCount - 1).SchoolInformation.Email;
                            pmodel.UnderGraduation_Fax3 = educationDetails.ElementAt(educationDetailsCount - 1).SchoolInformation.Fax;
                            pmodel.UnderGraduation_TelephoneNo3 = educationDetails.ElementAt(educationDetailsCount - 1).SchoolInformation.PhoneNumber;
                            pmodel.UnderGraduation_EmailAddress3 = educationDetails.ElementAt(educationDetailsCount - 1).SchoolInformation.Email;
                            //pmodel.UnderGraduation_Number1 = educationDetails.ElementAt(educationDetailsCount - 1).SchoolInformation.SchoolInformationID.ToString();
                            pmodel.UnderGraduation_Building3 = educationDetails.ElementAt(educationDetailsCount - 1).SchoolInformation.Building;
                            pmodel.UnderGraduation_Street3 = educationDetails.ElementAt(educationDetailsCount - 1).SchoolInformation.Street;
                            pmodel.UnderGraduation_Country3 = educationDetails.ElementAt(educationDetailsCount - 1).SchoolInformation.Country;
                            pmodel.UnderGraduation_State3 = educationDetails.ElementAt(educationDetailsCount - 1).SchoolInformation.State;
                            pmodel.UnderGraduation_County3 = educationDetails.ElementAt(educationDetailsCount - 1).SchoolInformation.County;
                            pmodel.UnderGraduation_City3 = educationDetails.ElementAt(educationDetailsCount - 1).SchoolInformation.City;
                            pmodel.UnderGraduation_ZipCode3 = educationDetails.ElementAt(educationDetailsCount - 1).SchoolInformation.ZipCode;
                        }
                        pmodel.UnderGraduation_Degree3 = educationDetails.ElementAt(educationDetailsCount - 1).QualificationDegree;
                        //pmodel.Provider_Highest_Degree = pmodel.UnderGraduation_Degree1;
                        pmodel.UnderGraduation_StartDate3 = ConvertToDateString(educationDetails.ElementAt(educationDetailsCount - 1).StartDate);
                        pmodel.UnderGraduation_EndDate3 = ConvertToDateString(educationDetails.ElementAt(educationDetailsCount - 1).EndDate);
                        //pmodel.Provider_Highest_Degree_StartDate = pmodel.UnderGraduation_StartDate1;
                        //pmodel.Provider_Highest_Degree_EndDate = pmodel.UnderGraduation_EndDate1;
                        pmodel.IsUnderGraduatedFromSchool3 = educationDetails.ElementAt(educationDetailsCount - 1).IsUSGraduate;
                        pmodel.UnderGraduation_Certificate3 = educationDetails.ElementAt(educationDetailsCount - 1).CertificatePath;
                    }
                }


                #endregion

                #region CME Certification
                if (profile.CMECertifications != null && profile.CMECertifications.Count != 0)
                {
                    var cmeCount = profile.CMECertifications.Count;
                    var cmeCertifications = from e in profile.CMECertifications orderby e.StartDate descending select e;

                    if (templateName == "North_Carolina_Coventry_Uniform_Credentialing_Application")
                    {
                        int cme_experienceCount = 0;
                        foreach (var item in cmeCertifications)
                        {
                            int days = 3 * 365;
                            double res = 0.0;
                            if (item.EndDate != null)
                            {
                                res = CalculateDateDifference(item.EndDate);
                            }
                            else if (item.StartDate != null)
                            {
                                res = CalculateDateDifference(item.StartDate);
                            }
                            if (res <= days)
                            {
                                if (cme_experienceCount == 0)
                                {
                                    pmodel.CME_CertificateName1 = item.Certification;
                                    pmodel.CME_StartDate1 = ConvertToDateString(item.StartDate);
                                    pmodel.CME_EndDate1 = ConvertToDateString(item.EndDate);

                                    cme_experienceCount++;
                                }
                                else if (cme_experienceCount == 1)
                                {
                                    pmodel.CME_CertificateName2 = item.Certification;
                                    pmodel.CME_StartDate2 = ConvertToDateString(item.StartDate);
                                    pmodel.CME_EndDate2 = ConvertToDateString(item.EndDate);

                                    cme_experienceCount++;
                                }
                                else if (cme_experienceCount == 2)
                                {
                                    pmodel.CME_CertificateName3 = item.Certification;
                                    pmodel.CME_StartDate3 = ConvertToDateString(item.StartDate);
                                    pmodel.CME_EndDate3 = ConvertToDateString(item.EndDate);
                                    cme_experienceCount++;
                                }
                                else if (cme_experienceCount == 3)
                                {
                                    pmodel.CME_CertificateName4 = item.Certification;
                                    pmodel.CME_StartDate4 = ConvertToDateString(item.StartDate);
                                    pmodel.CME_EndDate4 = ConvertToDateString(item.EndDate);
                                    cme_experienceCount++;
                                }
                            }
                        }
                    }
                    else
                    {
                        int cme_experienceCount = 0;
                        foreach (var item in cmeCertifications)
                        {
                            if (cme_experienceCount == 0)
                            {
                                pmodel.CME_CertificateName1 = item.Certification;
                                pmodel.CME_StartDate1 = ConvertToDateString(item.StartDate);
                                pmodel.CME_EndDate1 = ConvertToDateString(item.EndDate);

                                if (item.SchoolInformation != null)
                                {
                                    pmodel.CME_SchoolName = item.SchoolInformation.SchoolName;
                                    pmodel.CME_MailingAddress = item.SchoolInformation.Email;
                                    pmodel.CME_City1 = item.SchoolInformation.City;
                                    pmodel.CME_State1 = item.SchoolInformation.State;
                                    pmodel.CME_ZipCode1 = item.SchoolInformation.ZipCode;
                                    pmodel.CME_Country1 = item.SchoolInformation.Country;
                                }
                                cme_experienceCount++;
                            }
                            else if (cme_experienceCount == 1)
                            {
                                pmodel.CME_CertificateName2 = item.Certification;
                                pmodel.CME_StartDate2 = ConvertToDateString(item.StartDate);
                                pmodel.CME_EndDate2 = ConvertToDateString(item.EndDate);

                                cme_experienceCount++;
                            }
                        }
                    }
                    #region Old CME Details
                    //if (profile.CMECertifications.ElementAt(cmeCount - 1).SchoolInformation != null)
                    //{
                    //    pmodel.CME_SchoolName = profile.CMECertifications.ElementAt(cmeCount - 1).SchoolInformation.SchoolName;
                    //    pmodel.CME_MailingAddress = profile.CMECertifications.ElementAt(cmeCount - 1).SchoolInformation.Email;
                    //    pmodel.CME_City1 = profile.CMECertifications.ElementAt(cmeCount - 1).SchoolInformation.City;
                    //    pmodel.CME_State1 = profile.CMECertifications.ElementAt(cmeCount - 1).SchoolInformation.State;
                    //    pmodel.CME_ZipCode1 = profile.CMECertifications.ElementAt(cmeCount - 1).SchoolInformation.ZipCode;
                    //    pmodel.CME_Country1 = profile.CMECertifications.ElementAt(cmeCount - 1).SchoolInformation.Country;
                    //}
                    //pmodel.CME_CertificateName1 = profile.CMECertifications.ElementAt(cmeCount - 1).Certification;
                    //pmodel.CME_StartDate1 = ConvertToDateString(profile.CMECertifications.ElementAt(cmeCount - 1).StartDate);
                    //pmodel.CME_EndDate1 = ConvertToDateString(profile.CMECertifications.ElementAt(cmeCount - 1).EndDate);
                    //pmodel.CME_Detail1 = pmodel.CME_CertificateName1;

                    //if (profile.CMECertifications.Count > 1)
                    //{
                    //    pmodel.CME_CertificateName2 = profile.CMECertifications.ElementAt(cmeCount - 2).Certification;
                    //    pmodel.CME_StartDate2 = ConvertToDateString(profile.CMECertifications.ElementAt(cmeCount - 2).StartDate);
                    //    pmodel.CME_EndDate2 = ConvertToDateString(profile.CMECertifications.ElementAt(cmeCount - 2).EndDate);
                    //    pmodel.CME_Detail2 = pmodel.CME_CertificateName2;
                    //}
                    //if (profile.CMECertifications.Count > 2)
                    //{
                    //    pmodel.CME_CertificateName3 = profile.CMECertifications.ElementAt(cmeCount - 3).Certification;
                    //    pmodel.CME_StartDate3 = ConvertToDateString(profile.CMECertifications.ElementAt(cmeCount - 3).StartDate);
                    //    pmodel.CME_EndDate3 = ConvertToDateString(profile.CMECertifications.ElementAt(cmeCount - 3).EndDate);
                    //    pmodel.CME_Detail3 = pmodel.CME_CertificateName3;
                    //}
                    //if (profile.CMECertifications.Count > 3)
                    //{
                    //    pmodel.CME_CertificateName4 = profile.CMECertifications.ElementAt(cmeCount - 4).Certification;
                    //    pmodel.CME_StartDate4 = ConvertToDateString(profile.CMECertifications.ElementAt(cmeCount - 4).StartDate);
                    //    pmodel.CME_EndDate4 = ConvertToDateString(profile.CMECertifications.ElementAt(cmeCount - 4).EndDate);
                    //    pmodel.CME_Detail4 = pmodel.CME_CertificateName4;
                    //}
                    #endregion
                }
                #endregion

                #endregion

                #region Hospital Privileges


                if (profile.HospitalPrivilegeInformation != null && profile.HospitalPrivilegeInformation.HospitalPrivilegeDetails.Count > 0)
                {

                    var primaryHospitalInfo = profile.HospitalPrivilegeInformation.HospitalPrivilegeDetails.FirstOrDefault(p => p.Preference == AHC.CD.Entities.MasterData.Enums.PreferenceType.Primary.ToString()
                                    && p.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString());

                    var secondaryHospitalInfo = profile.HospitalPrivilegeInformation.HospitalPrivilegeDetails.Where(p => p.Preference != AHC.CD.Entities.MasterData.Enums.PreferenceType.Primary.ToString()
                                    && p.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();

                    var secondaryHospitalCount = secondaryHospitalInfo.Count;

                    if (primaryHospitalInfo != null)
                    {
                        #region Primary Hospital Info
                        if (primaryHospitalInfo != null && primaryHospitalInfo.Hospital != null)
                        {
                            pmodel.HospitalPrivilege_PrimaryHospitalName = primaryHospitalInfo.Hospital.HospitalName;
                            pmodel.HospitalPrivilege_PrimaryAffiliationEndDate = ConvertToDateString(primaryHospitalInfo.AffiliationEndDate);
                            pmodel.HospitalPrivilege_PrimaryAffiliationStartDate = ConvertToDateString(primaryHospitalInfo.AffilicationStartDate);

                            if (primaryHospitalInfo.HospitalContactInfo != null)
                            {
                                pmodel.HospitalPrivilege_PrimaryStreet = primaryHospitalInfo.HospitalContactInfo.Street;
                                pmodel.HospitalPrivilege_PrimaryCity = primaryHospitalInfo.HospitalContactInfo.City;
                                pmodel.HospitalPrivilege_PrimarySuite = primaryHospitalInfo.HospitalContactInfo.LocationName;
                                pmodel.HospitalPrivilege_PrimaryState = primaryHospitalInfo.HospitalContactInfo.State;
                                pmodel.HospitalPrivilege_PrimaryZipCode = primaryHospitalInfo.HospitalContactInfo.ZipCode;
                                pmodel.HospitalPrivilege_PrimaryCountry = primaryHospitalInfo.HospitalContactInfo.Country;
                                pmodel.HospitalPrivilege_PrimaryPhone = primaryHospitalInfo.HospitalContactInfo.PhoneNumber;
                                pmodel.HospitalPrivilege_PrimaryFax = primaryHospitalInfo.HospitalContactInfo.Fax;
                                pmodel.HospitalPrivilege_PrimaryEmail = primaryHospitalInfo.HospitalContactInfo.Email;
                                pmodel.HospitalPrivilege_PrimaryCounty = primaryHospitalInfo.HospitalContactInfo.County;


                                pmodel.HospitalPrivilege_PrimaryLocation_Line1 = pmodel.HospitalPrivilege_PrimaryStreet + " " + pmodel.HospitalPrivilege_PrimarySuite;
                                pmodel.HospitalPrivilege_PrimaryLocation_Line2 = pmodel.HospitalPrivilege_PrimaryCity + ", " + pmodel.HospitalPrivilege_PrimaryState + " " + pmodel.HospitalPrivilege_PrimaryZipCode;
                                if (primaryHospitalInfo.AdmittingPrivilege != null)
                                {
                                    pmodel.HospitalPrivilege_PrimaryAdmitPrivilegeStatus = primaryHospitalInfo.AdmittingPrivilege.Title;

                                }
                                if (primaryHospitalInfo.AnnualAdmisionPercentage != null)
                                {
                                    pmodel.HospitalPrivilege_PrimaryAnnualAddmissionPercentage = primaryHospitalInfo.AnnualAdmisionPercentage.ToString();

                                }
                            }

                            #region Specific Primary Hospital Privileges

                            pmodel.HospitalPrivilege_PrimaryHospitalName1 = primaryHospitalInfo.Hospital.HospitalName;
                            pmodel.HospitalPrivilege_PrimaryAffiliationEndDate1 = ConvertToDateString(primaryHospitalInfo.AffiliationEndDate);
                            pmodel.HospitalPrivilege_PrimaryAffiliationStartDate1 = ConvertToDateString(primaryHospitalInfo.AffilicationStartDate);

                            if (primaryHospitalInfo.HospitalContactInfo != null)
                            {
                                pmodel.HospitalPrivilege_PrimaryStreet1 = primaryHospitalInfo.HospitalContactInfo.Street;
                                pmodel.HospitalPrivilege_PrimaryCity1 = primaryHospitalInfo.HospitalContactInfo.City;
                                pmodel.HospitalPrivilege_PrimarySuite1 = primaryHospitalInfo.HospitalContactInfo.LocationName;
                                pmodel.HospitalPrivilege_PrimaryState1 = primaryHospitalInfo.HospitalContactInfo.State;
                                pmodel.HospitalPrivilege_PrimaryZipCode1 = primaryHospitalInfo.HospitalContactInfo.ZipCode;
                                pmodel.HospitalPrivilege_PrimaryCountry1 = primaryHospitalInfo.HospitalContactInfo.Country;
                                pmodel.HospitalPrivilege_PrimaryPhone1 = primaryHospitalInfo.HospitalContactInfo.PhoneNumber;
                                pmodel.HospitalPrivilege_PrimaryFax1 = primaryHospitalInfo.HospitalContactInfo.Fax;
                                pmodel.HospitalPrivilege_PrimaryEmail1 = primaryHospitalInfo.HospitalContactInfo.Email;
                                pmodel.HospitalPrivilege_PrimaryCounty1 = primaryHospitalInfo.HospitalContactInfo.County;


                                pmodel.HospitalPrivilege_PrimaryLocation1_Line1 = pmodel.HospitalPrivilege_PrimaryStreet + " " + pmodel.HospitalPrivilege_PrimarySuite;
                                pmodel.HospitalPrivilege_PrimaryLocation1_Line2 = pmodel.HospitalPrivilege_PrimaryCity + ", " + pmodel.HospitalPrivilege_PrimaryState + " " + pmodel.HospitalPrivilege_PrimaryZipCode;
                                if (primaryHospitalInfo.AdmittingPrivilege != null)
                                {
                                    pmodel.HospitalPrivilege_PrimaryAdmitPrivilegeStatus1 = primaryHospitalInfo.AdmittingPrivilege.Title;

                                }
                                if (primaryHospitalInfo.AnnualAdmisionPercentage != null)
                                {
                                    pmodel.HospitalPrivilege_PrimaryAnnualAddmissionPercentage1 = primaryHospitalInfo.AnnualAdmisionPercentage.ToString();

                                }
                            }

                            #endregion
                        }
                        #endregion

                        #region Secondary Hospital Info
                        if (secondaryHospitalInfo.Count > 0)
                        {
                            if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1) != null && secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).Hospital != null)
                            {
                                pmodel.HospitalPrivilege_HospitalName1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).Hospital.HospitalName;

                                pmodel.HospitalPrivilege_StartDate1 = ConvertToDateString(secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).AffilicationStartDate);
                                pmodel.HospitalPrivilege_HospitalStatus1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).Hospital.Status;
                                pmodel.HospitalPrivilege_EndDate1 = ConvertToDateString(secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).AffiliationEndDate);
                                if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).HospitalContactInfo != null)
                                {
                                    pmodel.HospitalPrivilege_Street1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).HospitalContactInfo.Street;
                                    pmodel.HospitalPrivilege_Suite1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).HospitalContactInfo.LocationName;
                                    pmodel.HospitalPrivilege_State1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).HospitalContactInfo.State;
                                    pmodel.HospitalPrivilege_City1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).HospitalContactInfo.City;
                                    pmodel.HospitalPrivilege_ZipCode1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).HospitalContactInfo.ZipCode;
                                    pmodel.HospitalPrivilege_County1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).HospitalContactInfo.Country;
                                    pmodel.HospitalPrivilege_Phone1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).HospitalContactInfo.PhoneNumber;
                                    pmodel.HospitalPrivilege_Fax1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).HospitalContactInfo.Fax;
                                    pmodel.HospitalPrivilege_Email1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).HospitalContactInfo.Email;
                                    pmodel.HospitalPrivilege_County1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).HospitalContactInfo.County;

                                    pmodel.HospitalPrivilege_Address1_Line1 = pmodel.HospitalPrivilege_Street1 + " " + pmodel.HospitalPrivilege_Suite1;
                                    pmodel.HospitalPrivilege_Address1_Line2 = pmodel.HospitalPrivilege_City1 + ", " + pmodel.HospitalPrivilege_State1 + " " + pmodel.HospitalPrivilege_ZipCode1;

                                    if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).AdmittingPrivilege != null)
                                    {
                                        pmodel.HospitalPrivilege_AdmitPrivilegeStatus1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).AdmittingPrivilege.Title;

                                    }
                                    if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).AnnualAdmisionPercentage != null)
                                    {
                                        pmodel.HospitalPrivilege_AnnualAddmissionPercentage1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).AnnualAdmisionPercentage.ToString();

                                    }
                                }
                                if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).StaffCategory != null)
                                {
                                    pmodel.HospitalPrivilege_StaffCategory1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).StaffCategory.Title;
                                }
                                pmodel.HospitalPrivilege_Number1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).Hospital.HospitalID.ToString();
                                if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).Specialty != null)
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
                        if (secondaryHospitalInfo.Count > 1)
                        {
                            if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2) != null && secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).Hospital != null)
                            {
                                pmodel.HospitalPrivilege_HospitalName2 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).Hospital.HospitalName;
                                if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).HospitalContactInfo != null)
                                {
                                    pmodel.HospitalPrivilege_City2 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).HospitalContactInfo.City;

                                    pmodel.HospitalPrivilege_Street2 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).HospitalContactInfo.Street;
                                    pmodel.HospitalPrivilege_Suite2 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).HospitalContactInfo.LocationName;
                                    pmodel.HospitalPrivilege_State2 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).HospitalContactInfo.State;
                                    pmodel.HospitalPrivilege_ZipCode2 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).HospitalContactInfo.ZipCode;
                                    pmodel.HospitalPrivilege_County2 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).HospitalContactInfo.Country;
                                    pmodel.HospitalPrivilege_Phone2 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).HospitalContactInfo.PhoneNumber;
                                    pmodel.HospitalPrivilege_Fax2 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).HospitalContactInfo.Fax;
                                    pmodel.HospitalPrivilege_Email2 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).HospitalContactInfo.Email;
                                    pmodel.HospitalPrivilege_County2 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).HospitalContactInfo.County;

                                    pmodel.HospitalPrivilege_Address2_Line1 = pmodel.HospitalPrivilege_Street2 + " " + pmodel.HospitalPrivilege_Suite2;
                                    pmodel.HospitalPrivilege_Address2_Line2 = pmodel.HospitalPrivilege_City2 + ", " + pmodel.HospitalPrivilege_State2 + " " + pmodel.HospitalPrivilege_ZipCode2;


                                    if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).AdmittingPrivilege != null)
                                    {
                                        pmodel.HospitalPrivilege_AdmitPrivilegeStatus2 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).AdmittingPrivilege.Title;

                                    }
                                    if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).AnnualAdmisionPercentage != null)
                                    {
                                        pmodel.HospitalPrivilege_AnnualAddmissionPercentage2 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).AnnualAdmisionPercentage.ToString();

                                    }
                                }
                            }
                        }
                        if (secondaryHospitalInfo.Count > 2)
                        {
                            if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3) != null && secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3).Hospital != null)
                            {
                                pmodel.HospitalPrivilege_HospitalName3 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3).Hospital.HospitalName;

                                if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3).HospitalContactInfo != null)
                                {
                                    pmodel.HospitalPrivilege_City3 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3).HospitalContactInfo.City;

                                    pmodel.HospitalPrivilege_Street3 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3).HospitalContactInfo.Street;
                                    pmodel.HospitalPrivilege_Suite3 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3).HospitalContactInfo.LocationName;
                                    pmodel.HospitalPrivilege_State3 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3).HospitalContactInfo.State;
                                    pmodel.HospitalPrivilege_ZipCode3 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3).HospitalContactInfo.ZipCode;
                                    pmodel.HospitalPrivilege_County3 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3).HospitalContactInfo.Country;
                                    pmodel.HospitalPrivilege_Phone3 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3).HospitalContactInfo.PhoneNumber;
                                    pmodel.HospitalPrivilege_Fax3 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3).HospitalContactInfo.Fax;
                                    pmodel.HospitalPrivilege_Email3 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3).HospitalContactInfo.Email;
                                    pmodel.HospitalPrivilege_County3 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3).HospitalContactInfo.County;

                                    pmodel.HospitalPrivilege_Address3_Line1 = pmodel.HospitalPrivilege_Street3 + " " + pmodel.HospitalPrivilege_Suite3;
                                    pmodel.HospitalPrivilege_Address3_Line2 = pmodel.HospitalPrivilege_City3 + ", " + pmodel.HospitalPrivilege_State3 + " " + pmodel.HospitalPrivilege_ZipCode3;

                                    if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3).AdmittingPrivilege != null)
                                    {
                                        pmodel.HospitalPrivilege_AdmitPrivilegeStatus3 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3).AdmittingPrivilege.Title;

                                    }
                                    if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3).AnnualAdmisionPercentage != null)
                                    {
                                        pmodel.HospitalPrivilege_AnnualAddmissionPercentage3 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3).AnnualAdmisionPercentage.ToString();

                                    }
                                }
                            }
                        }
                        if (secondaryHospitalInfo.Count > 3)
                        {
                            if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4) != null && secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4).Hospital != null)
                            {
                                pmodel.HospitalPrivilege_HospitalName4 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4).Hospital.HospitalName;
                                if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4).HospitalContactInfo != null)
                                {
                                    pmodel.HospitalPrivilege_City4 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4).HospitalContactInfo.City;

                                    pmodel.HospitalPrivilege_Street4 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4).HospitalContactInfo.Street;
                                    pmodel.HospitalPrivilege_Suite4 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4).HospitalContactInfo.LocationName;
                                    pmodel.HospitalPrivilege_State4 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4).HospitalContactInfo.State;
                                    pmodel.HospitalPrivilege_ZipCode4 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4).HospitalContactInfo.ZipCode;
                                    pmodel.HospitalPrivilege_County4 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4).HospitalContactInfo.Country;
                                    pmodel.HospitalPrivilege_Phone4 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4).HospitalContactInfo.PhoneNumber;
                                    pmodel.HospitalPrivilege_Fax4 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4).HospitalContactInfo.Fax;
                                    pmodel.HospitalPrivilege_Email4 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4).HospitalContactInfo.Email;
                                    pmodel.HospitalPrivilege_County4 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4).HospitalContactInfo.County;

                                    pmodel.HospitalPrivilege_Address4_Line1 = pmodel.HospitalPrivilege_Street4 + " " + pmodel.HospitalPrivilege_Suite4;
                                    pmodel.HospitalPrivilege_Address4_Line2 = pmodel.HospitalPrivilege_City4 + ", " + pmodel.HospitalPrivilege_State4 + " " + pmodel.HospitalPrivilege_ZipCode4;

                                    if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4).AdmittingPrivilege != null)
                                    {
                                        pmodel.HospitalPrivilege_AdmitPrivilegeStatus4 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4).AdmittingPrivilege.Title;

                                    }
                                    if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4).AnnualAdmisionPercentage != null)
                                    {
                                        pmodel.HospitalPrivilege_AnnualAddmissionPercentage4 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4).AnnualAdmisionPercentage.ToString();

                                    }
                                }
                            }
                        }
                        #endregion

                    }
                    else
                    {
                        #region Secondary Hospital Info Primary Null
                        if (secondaryHospitalInfo.Count > 0)
                        {
                            if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1) != null && secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).Hospital != null)
                            {
                                pmodel.HospitalPrivilege_PrimaryHospitalName = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).Hospital.HospitalName;
                                pmodel.HospitalPrivilege_PrimaryAffiliationEndDate = ConvertToDateString(secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).AffiliationEndDate);
                                pmodel.HospitalPrivilege_PrimaryAffiliationStartDate = ConvertToDateString(secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).AffilicationStartDate);


                                if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).HospitalContactInfo != null)
                                {
                                    pmodel.HospitalPrivilege_PrimaryCity = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).HospitalContactInfo.City;

                                    pmodel.HospitalPrivilege_PrimaryStreet = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).HospitalContactInfo.Street;
                                    pmodel.HospitalPrivilege_PrimarySuite = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).HospitalContactInfo.LocationName;
                                    pmodel.HospitalPrivilege_PrimaryState = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).HospitalContactInfo.State;
                                    pmodel.HospitalPrivilege_PrimaryZipCode = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).HospitalContactInfo.ZipCode;
                                    pmodel.HospitalPrivilege_PrimaryCountry = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).HospitalContactInfo.Country;
                                    pmodel.HospitalPrivilege_PrimaryPhone = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).HospitalContactInfo.PhoneNumber;
                                    pmodel.HospitalPrivilege_PrimaryFax = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).HospitalContactInfo.Fax;
                                    pmodel.HospitalPrivilege_PrimaryEmail = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).HospitalContactInfo.Email;
                                    pmodel.HospitalPrivilege_PrimaryCounty = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).HospitalContactInfo.County;

                                    pmodel.HospitalPrivilege_PrimaryLocation_Line1 = pmodel.HospitalPrivilege_PrimaryStreet + " " + pmodel.HospitalPrivilege_PrimarySuite;
                                    pmodel.HospitalPrivilege_PrimaryLocation_Line2 = pmodel.HospitalPrivilege_PrimaryCity + ", " + pmodel.HospitalPrivilege_PrimaryState + " " + pmodel.HospitalPrivilege_PrimaryZipCode;

                                    if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).AdmittingPrivilege != null)
                                    {
                                        pmodel.HospitalPrivilege_PrimaryAdmitPrivilegeStatus = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).AdmittingPrivilege.Title;

                                    }
                                    if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).AnnualAdmisionPercentage != null)
                                    {
                                        pmodel.HospitalPrivilege_PrimaryAnnualAddmissionPercentage = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).AnnualAdmisionPercentage.ToString();

                                    }
                                }
                            }
                            if (secondaryHospitalInfo.Count == 1)
                            {
                                if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1) != null && secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).Hospital != null)
                                {
                                    pmodel.HospitalPrivilege_HospitalName1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).Hospital.HospitalName;
                                    pmodel.HospitalPrivilege_StartDate1 = ConvertToDateString(secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).AffilicationStartDate);
                                    pmodel.HospitalPrivilege_EndDate1 = ConvertToDateString(secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).AffiliationEndDate);

                                    if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).HospitalContactInfo != null)
                                    {
                                        pmodel.HospitalPrivilege_City1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).HospitalContactInfo.City;

                                        pmodel.HospitalPrivilege_Street1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).HospitalContactInfo.Street;
                                        pmodel.HospitalPrivilege_Suite1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).HospitalContactInfo.LocationName;
                                        pmodel.HospitalPrivilege_State1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).HospitalContactInfo.State;
                                        pmodel.HospitalPrivilege_ZipCode1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).HospitalContactInfo.ZipCode;
                                        pmodel.HospitalPrivilege_County1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).HospitalContactInfo.Country;
                                        pmodel.HospitalPrivilege_Phone1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).HospitalContactInfo.PhoneNumber;
                                        pmodel.HospitalPrivilege_Fax1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).HospitalContactInfo.Fax;
                                        pmodel.HospitalPrivilege_Email1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).HospitalContactInfo.Email;
                                        pmodel.HospitalPrivilege_County1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).HospitalContactInfo.County;

                                        pmodel.HospitalPrivilege_Address1_Line1 = pmodel.HospitalPrivilege_Street1 + " " + pmodel.HospitalPrivilege_Suite1;
                                        pmodel.HospitalPrivilege_Address1_Line2 = pmodel.HospitalPrivilege_City1 + ", " + pmodel.HospitalPrivilege_State1 + " " + pmodel.HospitalPrivilege_ZipCode1;

                                        if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).AdmittingPrivilege != null)
                                        {
                                            pmodel.HospitalPrivilege_AdmitPrivilegeStatus1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).AdmittingPrivilege.Title;

                                        }
                                        if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).AnnualAdmisionPercentage != null)
                                        {
                                            pmodel.HospitalPrivilege_AnnualAddmissionPercentage1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).AnnualAdmisionPercentage.ToString();

                                        }
                                    }
                                }
                            }
                        }
                        if (secondaryHospitalInfo.Count > 1)
                        {
                            if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2) != null && secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).Hospital != null)
                            {
                                pmodel.HospitalPrivilege_HospitalName1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).Hospital.HospitalName;
                                pmodel.HospitalPrivilege_StartDate1 = ConvertToDateString(secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).AffilicationStartDate);
                                pmodel.HospitalPrivilege_EndDate1 = ConvertToDateString(secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).AffiliationEndDate);

                                if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).HospitalContactInfo != null)
                                {
                                    pmodel.HospitalPrivilege_City1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).HospitalContactInfo.City;

                                    pmodel.HospitalPrivilege_Street1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).HospitalContactInfo.Street;
                                    pmodel.HospitalPrivilege_Suite1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).HospitalContactInfo.LocationName;
                                    pmodel.HospitalPrivilege_State1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).HospitalContactInfo.State;
                                    pmodel.HospitalPrivilege_ZipCode1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).HospitalContactInfo.ZipCode;
                                    pmodel.HospitalPrivilege_County1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).HospitalContactInfo.Country;
                                    pmodel.HospitalPrivilege_Phone1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).HospitalContactInfo.PhoneNumber;
                                    pmodel.HospitalPrivilege_Fax1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).HospitalContactInfo.Fax;
                                    pmodel.HospitalPrivilege_Email1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).HospitalContactInfo.Email;
                                    pmodel.HospitalPrivilege_County1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).HospitalContactInfo.County;

                                    pmodel.HospitalPrivilege_Address1_Line1 = pmodel.HospitalPrivilege_Street1 + " " + pmodel.HospitalPrivilege_Suite1;
                                    pmodel.HospitalPrivilege_Address1_Line2 = pmodel.HospitalPrivilege_City1 + ", " + pmodel.HospitalPrivilege_State1 + " " + pmodel.HospitalPrivilege_ZipCode1;

                                    if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).AdmittingPrivilege != null)
                                    {
                                        pmodel.HospitalPrivilege_AdmitPrivilegeStatus1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).AdmittingPrivilege.Title;

                                    }
                                    if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).AnnualAdmisionPercentage != null)
                                    {
                                        pmodel.HospitalPrivilege_AnnualAddmissionPercentage1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).AnnualAdmisionPercentage.ToString();

                                    }
                                }
                            }
                        }
                        if (secondaryHospitalInfo.Count > 2)
                        {
                            if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3) != null && secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3).Hospital != null)
                            {
                                pmodel.HospitalPrivilege_HospitalName2 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3).Hospital.HospitalName;
                                if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3).HospitalContactInfo != null)
                                {
                                    pmodel.HospitalPrivilege_City2 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3).HospitalContactInfo.City;

                                    pmodel.HospitalPrivilege_Street2 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3).HospitalContactInfo.Street;
                                    pmodel.HospitalPrivilege_Suite2 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3).HospitalContactInfo.LocationName;
                                    pmodel.HospitalPrivilege_State2 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3).HospitalContactInfo.State;
                                    pmodel.HospitalPrivilege_ZipCode2 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3).HospitalContactInfo.ZipCode;
                                    pmodel.HospitalPrivilege_County2 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3).HospitalContactInfo.Country;
                                    pmodel.HospitalPrivilege_Phone2 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3).HospitalContactInfo.PhoneNumber;
                                    pmodel.HospitalPrivilege_Fax2 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3).HospitalContactInfo.Fax;
                                    pmodel.HospitalPrivilege_Email2 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3).HospitalContactInfo.Email;
                                    pmodel.HospitalPrivilege_County2 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3).HospitalContactInfo.County;

                                    pmodel.HospitalPrivilege_Address2_Line1 = pmodel.HospitalPrivilege_Street2 + " " + pmodel.HospitalPrivilege_Suite2;
                                    pmodel.HospitalPrivilege_Address2_Line2 = pmodel.HospitalPrivilege_City2 + ", " + pmodel.HospitalPrivilege_State2 + " " + pmodel.HospitalPrivilege_ZipCode2;

                                    if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3).AdmittingPrivilege != null)
                                    {
                                        pmodel.HospitalPrivilege_AdmitPrivilegeStatus2 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3).AdmittingPrivilege.Title;

                                    }
                                    if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3).AnnualAdmisionPercentage != null)
                                    {
                                        pmodel.HospitalPrivilege_AnnualAddmissionPercentage2 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 3).AnnualAdmisionPercentage.ToString();

                                    }
                                }
                            }
                        }
                        if (secondaryHospitalInfo.Count > 3)
                        {
                            if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4) != null && secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4).Hospital != null)
                            {
                                pmodel.HospitalPrivilege_HospitalName3 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4).Hospital.HospitalName;
                                if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4).HospitalContactInfo != null)
                                {
                                    pmodel.HospitalPrivilege_City3 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4).HospitalContactInfo.City;

                                    pmodel.HospitalPrivilege_Street3 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4).HospitalContactInfo.Street;
                                    pmodel.HospitalPrivilege_Suite3 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4).HospitalContactInfo.LocationName;
                                    pmodel.HospitalPrivilege_State3 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4).HospitalContactInfo.State;
                                    pmodel.HospitalPrivilege_ZipCode3 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4).HospitalContactInfo.ZipCode;
                                    pmodel.HospitalPrivilege_County3 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4).HospitalContactInfo.Country;
                                    pmodel.HospitalPrivilege_Phone3 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4).HospitalContactInfo.PhoneNumber;
                                    pmodel.HospitalPrivilege_Fax3 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4).HospitalContactInfo.Fax;
                                    pmodel.HospitalPrivilege_Email3 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4).HospitalContactInfo.Email;
                                    pmodel.HospitalPrivilege_County3 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4).HospitalContactInfo.County;

                                    pmodel.HospitalPrivilege_Address3_Line1 = pmodel.HospitalPrivilege_Street3 + " " + pmodel.HospitalPrivilege_Suite3;
                                    pmodel.HospitalPrivilege_Address3_Line2 = pmodel.HospitalPrivilege_City3 + ", " + pmodel.HospitalPrivilege_State3 + " " + pmodel.HospitalPrivilege_ZipCode3;

                                    if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4).AdmittingPrivilege != null)
                                    {
                                        pmodel.HospitalPrivilege_AdmitPrivilegeStatus3 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4).AdmittingPrivilege.Title;

                                    }
                                    if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4).AnnualAdmisionPercentage != null)
                                    {
                                        pmodel.HospitalPrivilege_AnnualAddmissionPercentage3 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 4).AnnualAdmisionPercentage.ToString();

                                    }
                                }
                            }
                        }
                        #endregion

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
                        //pmodel.ProfLiability_Phone1 = professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).PhoneNumber;

                        if (professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).Phone != null)
                        {
                            pmodel.ProfLiability_PhoneFirstThreeDigit1 = professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).Phone.Substring(0, professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).Phone.Length - 7);
                            pmodel.ProfLiability_PhoneSecondThreeDigit1 = professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).Phone.Substring(3, 3);
                            pmodel.ProfLiability_PhoneLastFourDigit1 = professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).Phone.Substring(6);

                            pmodel.ProfLiability_Phone1 = pmodel.ProfLiability_PhoneFirstThreeDigit1 + "-" + pmodel.ProfLiability_PhoneSecondThreeDigit1 + "-" + pmodel.ProfLiability_PhoneLastFourDigit1;
                            pmodel.ProfLiability1_Address3 = "Phone : " + pmodel.ProfLiability_Phone1;
                        }


                        pmodel.ProfLiability_AggregateCoverageAmount1 = professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).AmountOfCoverageAggregate.ToString();
                        pmodel.ProfLiability_CoverageAmountPerOccurrence1 = professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).AmountOfCoveragePerOccurance.ToString();
                        if (pmodel.ProfLiability_CoverageAmountPerOccurrence1 != "" && pmodel.ProfLiability_CoverageAmountPerOccurrence1 != null)
                        {
                            if (pmodel.ProfLiability_AggregateCoverageAmount1 != null && pmodel.ProfLiability_AggregateCoverageAmount1 != "")
                            {
                                pmodel.TriCareAmountsOfCoverage = "$ " + pmodel.ProfLiability_CoverageAmountPerOccurrence1 + " - " + "$ " + pmodel.ProfLiability_AggregateCoverageAmount1;
                            }
                            else
                            {
                                pmodel.TriCareAmountsOfCoverage = "$ " + pmodel.ProfLiability_CoverageAmountPerOccurrence1;
                            }
                        }
                        else if (pmodel.ProfLiability_AggregateCoverageAmount1 != null)
                        {
                            pmodel.TriCareAmountsOfCoverage = "$ " + pmodel.ProfLiability_AggregateCoverageAmount1;

                        }
                        if (professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).InsuranceCarrierAddress != null)
                        {
                            pmodel.ProfLiability_Country1 = professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).InsuranceCarrierAddress.Country;
                            pmodel.ProfLiability_State1 = professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).InsuranceCarrierAddress.State;
                            pmodel.ProfLiability_County1 = professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).InsuranceCarrierAddress.County;
                            pmodel.ProfLiability_City1 = professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).InsuranceCarrierAddress.City;
                            pmodel.ProfLiability_Street1 = professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).InsuranceCarrierAddress.Street;
                            pmodel.ProfLiability_ZipCode1 = professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).InsuranceCarrierAddress.ZipCode;
                            pmodel.ProfLiability_Suite1 = professionalLiabilities.ElementAt(professionalLiabilitiesCount - 1).InsuranceCarrierAddress.LocationName;
                            pmodel.ProfLiability1_Address1 = pmodel.ProfLiability_Street1 + " " + pmodel.ProfLiability_Suite1;
                            pmodel.ProfLiability1_Address2 = pmodel.ProfLiability_City1 + ", " + pmodel.ProfLiability_State1 + " " + pmodel.ProfLiability_ZipCode1;
                        }
                    }
                }

                #endregion

                #region Professional Affiliation
                if (profile.ProfessionalAffiliationInfos.Count > 0)
                {
                    var professionalAffiliations = profile.ProfessionalAffiliationInfos.Where(s => s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();
                    var professionalAffiliationsCount = professionalAffiliations.Count;
                    if (professionalAffiliationsCount > 0 && professionalAffiliations.ElementAt(professionalAffiliationsCount - 1) != null)
                    {
                        pmodel.ProviderOrganizationName1 = professionalAffiliations.ElementAt(professionalAffiliationsCount - 1).OrganizationName;
                        pmodel.ProfessionalAffiliationOfficePosition1 = professionalAffiliations.ElementAt(professionalAffiliationsCount - 1).PositionOfficeHeld;
                        pmodel.ProfessionalAffiliationMember1 = professionalAffiliations.ElementAt(professionalAffiliationsCount - 1).Member;
                        pmodel.ProfessionalAffiliationStartDate1 = ConvertToDateString(professionalAffiliations.ElementAt(professionalAffiliationsCount - 1).StartDate);
                        if (ConvertToDateString(professionalAffiliations.ElementAt(professionalAffiliationsCount - 1).EndDate) != null)
                        {
                            pmodel.ProfessionalAffiliationEndDate1 = ConvertToDateString(professionalAffiliations.ElementAt(professionalAffiliationsCount - 1).EndDate);
                        }
                        else
                        {
                            pmodel.ProfessionalAffiliationEndDate1 = "CURRENT";
                        }
                    }
                    if (professionalAffiliationsCount > 1 && professionalAffiliations.ElementAt(professionalAffiliationsCount - 2) != null)
                    {
                        pmodel.ProviderOrganizationName2 = professionalAffiliations.ElementAt(professionalAffiliationsCount - 2).OrganizationName;
                        pmodel.ProfessionalAffiliationOfficePosition2 = professionalAffiliations.ElementAt(professionalAffiliationsCount - 2).PositionOfficeHeld;
                        pmodel.ProfessionalAffiliationMember2 = professionalAffiliations.ElementAt(professionalAffiliationsCount - 2).Member;
                        pmodel.ProfessionalAffiliationStartDate2 = ConvertToDateString(professionalAffiliations.ElementAt(professionalAffiliationsCount - 2).StartDate);
                        if (ConvertToDateString(professionalAffiliations.ElementAt(professionalAffiliationsCount - 2).EndDate) != null)
                        {
                            pmodel.ProfessionalAffiliationEndDate2 = ConvertToDateString(professionalAffiliations.ElementAt(professionalAffiliationsCount - 2).EndDate);
                        }
                        else
                        {
                            pmodel.ProfessionalAffiliationEndDate2 = "CURRENT";
                        }
                    }
                    if (professionalAffiliationsCount > 2 && professionalAffiliations.ElementAt(professionalAffiliationsCount - 3) != null)
                    {
                        pmodel.ProviderOrganizationName3 = professionalAffiliations.ElementAt(professionalAffiliationsCount - 3).OrganizationName;
                        pmodel.ProfessionalAffiliationOfficePosition3 = professionalAffiliations.ElementAt(professionalAffiliationsCount - 3).PositionOfficeHeld;
                        pmodel.ProfessionalAffiliationMember3 = professionalAffiliations.ElementAt(professionalAffiliationsCount - 3).Member;
                        pmodel.ProfessionalAffiliationStartDate3 = ConvertToDateString(professionalAffiliations.ElementAt(professionalAffiliationsCount - 3).StartDate);
                        if (ConvertToDateString(professionalAffiliations.ElementAt(professionalAffiliationsCount - 3).EndDate) != null)
                        {
                            pmodel.ProfessionalAffiliationEndDate3 = ConvertToDateString(professionalAffiliations.ElementAt(professionalAffiliationsCount - 3).EndDate);
                        }
                        else
                        {
                            pmodel.ProfessionalAffiliationEndDate3 = "CURRENT";
                        }

                    }
                    if (professionalAffiliationsCount > 3 && professionalAffiliations.ElementAt(professionalAffiliationsCount - 4) != null)
                    {
                        pmodel.ProviderOrganizationName4 = professionalAffiliations.ElementAt(professionalAffiliationsCount - 4).OrganizationName;
                        pmodel.ProfessionalAffiliationOfficePosition4 = professionalAffiliations.ElementAt(professionalAffiliationsCount - 4).PositionOfficeHeld;
                        pmodel.ProfessionalAffiliationMember4 = professionalAffiliations.ElementAt(professionalAffiliationsCount - 4).Member;
                        pmodel.ProfessionalAffiliationStartDate4 = ConvertToDateString(professionalAffiliations.ElementAt(professionalAffiliationsCount - 4).StartDate);
                        if (ConvertToDateString(professionalAffiliations.ElementAt(professionalAffiliationsCount - 4).EndDate) != null)
                        {
                            pmodel.ProfessionalAffiliationEndDate4 = ConvertToDateString(professionalAffiliations.ElementAt(professionalAffiliationsCount - 4).EndDate);
                        }
                        else
                        {
                            pmodel.ProfessionalAffiliationEndDate4 = "CURRENT";
                        }
                    }
                    if (professionalAffiliationsCount > 4 && professionalAffiliations.ElementAt(professionalAffiliationsCount - 5) != null)
                    {
                        pmodel.ProviderOrganizationName5 = professionalAffiliations.ElementAt(professionalAffiliationsCount - 5).OrganizationName;
                        pmodel.ProfessionalAffiliationOfficePosition5 = professionalAffiliations.ElementAt(professionalAffiliationsCount - 5).PositionOfficeHeld;
                        pmodel.ProfessionalAffiliationMember5 = professionalAffiliations.ElementAt(professionalAffiliationsCount - 5).Member;
                        pmodel.ProfessionalAffiliationStartDate5 = ConvertToDateString(professionalAffiliations.ElementAt(professionalAffiliationsCount - 5).StartDate);
                        if (ConvertToDateString(professionalAffiliations.ElementAt(professionalAffiliationsCount - 5).EndDate) != null)
                        {
                            pmodel.ProfessionalAffiliationEndDate5 = ConvertToDateString(professionalAffiliations.ElementAt(professionalAffiliationsCount - 5).EndDate);

                        }
                        else
                        {
                            pmodel.ProfessionalAffiliationEndDate5 = "CURRENT";
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

                #region Credentialing Info

                //pmodel.WellcareInitialCredentialingDate = GetCredentialingInitiationDate(profile.ProfileID);

                #endregion

                #region Work History
                if (profile.ProfessionalWorkExperiences != null && profile.ProfessionalWorkExperiences.Count > 0)
                {
                    var professionalworkexperienceCount = profile.ProfessionalWorkExperiences.Count;
                    var workHistory = from e in profile.ProfessionalWorkExperiences orderby e.StartDate descending select e;

                    if (templateName == "North_Carolina_Coventry_Uniform_Credentialing_Application")
                    {
                        int workexperienceCount = 0;
                        foreach (var item in workHistory)
                        {
                            if (item.EndDate == null)
                            {
                                if (workexperienceCount == 0)
                                {
                                    pmodel.WorkExp_StartDate1 = ConvertToDateString(item.StartDate);
                                    pmodel.WorkExp_EndDate1 = "CURRENT";
                                    pmodel.WorkExp_EmpName1 = item.EmployerName;
                                    pmodel.WorkExp_MailingAddress1 = item.EmployerEmail;
                                    pmodel.WorkExp_City1 = item.City;
                                    pmodel.WorkExp_State1 = item.State;
                                    pmodel.WorkExp_ZipCode1 = item.ZipCode;
                                    pmodel.WorkExp_County1 = item.County;
                                    pmodel.WorkExp_JobTitle1 = item.JobTitle;
                                    workexperienceCount++;
                                }
                            }

                            else if (workexperienceCount == 1)
                            {
                                pmodel.WorkExp_StartDate2 = ConvertToDateString(item.StartDate);
                                pmodel.WorkExp_EndDate2 = ConvertToDateString(item.EndDate);
                                pmodel.WorkExp_EmpName2 = item.EmployerName;
                                pmodel.WorkExp_MailingAddress2 = item.EmployerEmail;
                                pmodel.WorkExp_City2 = item.City;
                                pmodel.WorkExp_State2 = item.State;
                                pmodel.WorkExp_ZipCode2 = item.ZipCode;
                                pmodel.WorkExp_County2 = item.County;
                                pmodel.WorkExp_JobTitle2 = item.JobTitle;
                                workexperienceCount++;
                            }
                            else if (workexperienceCount == 2)
                            {
                                pmodel.WorkExp_StartDate3 = ConvertToDateString(item.StartDate);
                                pmodel.WorkExp_EndDate3 = ConvertToDateString(item.EndDate);
                                pmodel.WorkExp_EmpName3 = item.EmployerName;
                                pmodel.WorkExp_MailingAddress3 = item.EmployerEmail;
                                pmodel.WorkExp_City3 = item.City;
                                pmodel.WorkExp_State3 = item.State;
                                pmodel.WorkExp_ZipCode3 = item.ZipCode;
                                pmodel.WorkExp_County3 = item.County;
                                pmodel.WorkExp_JobTitle3 = item.JobTitle;
                                workexperienceCount++;
                            }
                            else if (workexperienceCount == 3)
                            {
                                pmodel.WorkExp_StartDate4 = ConvertToDateString(item.StartDate);
                                pmodel.WorkExp_EndDate4 = ConvertToDateString(item.EndDate);
                                pmodel.WorkExp_EmpName4 = item.EmployerName;
                                pmodel.WorkExp_MailingAddress4 = item.EmployerEmail;
                                pmodel.WorkExp_City4 = item.City;
                                pmodel.WorkExp_State4 = item.State;
                                pmodel.WorkExp_ZipCode4 = item.ZipCode;
                                pmodel.WorkExp_County4 = item.County;
                                pmodel.WorkExp_JobTitle4 = item.JobTitle;
                                workexperienceCount++;
                            }
                            else if (workexperienceCount == 4)
                            {
                                pmodel.WorkExp_StartDate5 = ConvertToDateString(item.StartDate);
                                pmodel.WorkExp_EndDate5 = ConvertToDateString(item.EndDate);
                                pmodel.WorkExp_EmpName5 = item.EmployerName;
                                pmodel.WorkExp_MailingAddress5 = item.EmployerEmail;
                                pmodel.WorkExp_City5 = item.City;
                                pmodel.WorkExp_State5 = item.State;
                                pmodel.WorkExp_ZipCode5 = item.ZipCode;
                                pmodel.WorkExp_County5 = item.County;
                                pmodel.WorkExp_JobTitle5 = item.JobTitle;
                                workexperienceCount++;
                            }
                        }
                    }
                    else
                    {
                        int workexperienceCount = 0;
                        foreach (var item in workHistory)
                        {
                            int days = 5 * 365;
                            double res = 0.0;
                            if (item.EndDate != null)
                            {
                                res = CalculateDateDifference(item.EndDate);
                            }
                            else if (item.StartDate != null)
                            {
                                res = CalculateDateDifference(item.StartDate);
                            }
                            if (res <= days)
                            {
                                if (workexperienceCount == 0)
                                {
                                    pmodel.WorkExp_StartDate1 = ConvertToDateString(item.StartDate);
                                    pmodel.WorkExp_EndDate1 = ConvertToDateString(item.EndDate);
                                    pmodel.WorkExp_EmpName1 = item.EmployerName;
                                    pmodel.WorkExp_MailingAddress1 = item.EmployerEmail;
                                    pmodel.WorkExp_City1 = item.City;
                                    pmodel.WorkExp_State1 = item.State;
                                    pmodel.WorkExp_ZipCode1 = item.ZipCode;
                                    pmodel.WorkExp_County1 = item.County;
                                    pmodel.WorkExp_JobTitle1 = item.JobTitle;
                                    workexperienceCount++;
                                }
                                else if (workexperienceCount == 1)
                                {
                                    pmodel.WorkExp_StartDate2 = ConvertToDateString(item.StartDate);
                                    pmodel.WorkExp_EndDate2 = ConvertToDateString(item.EndDate);
                                    pmodel.WorkExp_EmpName2 = item.EmployerName;
                                    pmodel.WorkExp_MailingAddress2 = item.EmployerEmail;
                                    pmodel.WorkExp_City2 = item.City;
                                    pmodel.WorkExp_State2 = item.State;
                                    pmodel.WorkExp_ZipCode2 = item.ZipCode;
                                    pmodel.WorkExp_County2 = item.County;
                                    pmodel.WorkExp_JobTitle2 = item.JobTitle;
                                    workexperienceCount++;
                                }
                                else if (workexperienceCount == 2)
                                {
                                    pmodel.WorkExp_StartDate3 = ConvertToDateString(item.StartDate);
                                    pmodel.WorkExp_EndDate3 = ConvertToDateString(item.EndDate);
                                    pmodel.WorkExp_EmpName3 = item.EmployerName;
                                    pmodel.WorkExp_MailingAddress3 = item.EmployerEmail;
                                    pmodel.WorkExp_City3 = item.City;
                                    pmodel.WorkExp_State3 = item.State;
                                    pmodel.WorkExp_ZipCode3 = item.ZipCode;
                                    pmodel.WorkExp_County3 = item.County;
                                    pmodel.WorkExp_JobTitle3 = item.JobTitle;
                                    workexperienceCount++;
                                }
                                else if (workexperienceCount == 3)
                                {
                                    pmodel.WorkExp_StartDate4 = ConvertToDateString(item.StartDate);
                                    pmodel.WorkExp_EndDate4 = ConvertToDateString(item.EndDate);
                                    pmodel.WorkExp_EmpName4 = item.EmployerName;
                                    pmodel.WorkExp_MailingAddress4 = item.EmployerEmail;
                                    pmodel.WorkExp_City4 = item.City;
                                    pmodel.WorkExp_State4 = item.State;
                                    pmodel.WorkExp_ZipCode4 = item.ZipCode;
                                    pmodel.WorkExp_County4 = item.County;
                                    pmodel.WorkExp_JobTitle4 = item.JobTitle;
                                    workexperienceCount++;
                                }
                            }
                        }

                    }
                }
                #endregion


                #region Custom Field
                if (profile.CustomFieldTransaction != null)
                {
                    if (profile.CustomFieldTransaction.CustomFieldTransactionDatas.Count > 0)
                    {
                        var customFieldsRepo = uow.GetGenericRepository<AHC.CD.Entities.CustomField.CustomField>();

                        var customFields = customFieldsRepo.Get(c => c.Status != null && c.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();

                        foreach (var field in profile.CustomFieldTransaction.CustomFieldTransactionDatas)
                        {

                            foreach (var masterField in customFields)
                            {
                                if (field.CustomField != null)
                                {
                                    if (field.CustomField.CustomFieldTitle == "TAX ID")
                                    {
                                        pmodel.GroupTaxId = field.CustomFieldTransactionDataValue;
                                    }
                                    if (field.CustomField.CustomFieldTitle == "GROUP NAME")
                                    {
                                        pmodel.GroupName = field.CustomFieldTransactionDataValue;
                                    }
                                    if (field.CustomField.CustomFieldTitle == "GROUP NPI NUMBER")
                                    {
                                        pmodel.GroupNPI = field.CustomFieldTransactionDataValue;
                                    }
                                }
                            }

                        }
                    }
                }

                #endregion

                string pdfFileName = readXml(pmodel, templateName, CDUserId);

                return pdfFileName;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private double CalculateDateDifference(DateTime? dateTime1)
        {
            try
            {
                DateTime currentDate = DateTime.Now;
                var timespan = (TimeSpan)(currentDate - dateTime1);
                return timespan.TotalDays;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CombinePdfs(int profileID, List<string> pdflist, string UserAuthId, string name)
        {
            profileId = profileID;
            PersonalDetail personalInfo = GetProviderData(profileID);
            string CDUserId = GetCredentialingUserId(UserAuthId).ToString();
            var timeUtc = DateTime.UtcNow;
            TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            DateTime easternTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, easternZone);

            string date = easternTime.ToString("MM-dd-yyyy");

            string timeHour = easternTime.Hour.ToString();
            string timeMin = easternTime.Minute.ToString();
            string timeSec = easternTime.Second.ToString();



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
                    catch (Exception)
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
            SaveGeneratedPackage(profileID, "Generated Package" + "_Date" + date + "_Hour" + timeHour + "_Min" + timeMin, "\\ApplicationDocument\\GeneratedPackagePdf\\" + outputFileName, CDUserId);
            AddIntoPackageGenerationTracker(profileID, "\\ApplicationDocument\\GeneratedPackagePdf\\" + outputFileName, CDUserId, name);
            return outputFileName;

        }



        public List<PackageGeneratorBusinessModel> GenerateBulkPackage(List<int> ProfileIDs, List<string> GenricList, string UserAuthId, string name)
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

                    string GeneratedPackagePath = CombinePdfs(id, pdfList, UserAuthId, name);
                    PackageGeneratorBusinessModel pacakage = new PackageGeneratorBusinessModel();
                    pacakage.ProfileID = id;
                    pacakage.PackageFilePath = GeneratedPackagePath;
                    bulkPackage.Add(pacakage);
                }

            }

            return bulkPackage;
        }

        public async Task<List<PlanFormGenerationBusinessModel>> GenerateBulkForm(List<int> ProfileIDs, List<string> templateNames, string UserAuthId)
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

                            string filePath = "\\ApplicationDocument\\GeneratedTemplatePdf\\" + await GeneratePlanFormPDF(id, template, UserAuthId);
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

        private int GetCredentialingUserId(string UserAuthId)
        {
            var userRepo = uow.GetGenericRepository<CDUser>();
            var user = userRepo.Find(u => u.AuthenicateUserId == UserAuthId);

            return user.CDUserID;
        }

        private void AddIntoPackageGenerationTracker(int profileID, string filePath, string CDUserID, string name)
        {
            var trackerRepo = uow.GetGenericRepository<AuditingPackageGenerationTracker>();

            AuditingPackageGenerationTracker tracker = new AuditingPackageGenerationTracker();
            tracker.GeneratedByID = Convert.ToInt32(CDUserID);
            tracker.GeneratedByName = name;
            tracker.ProfileID = profileID;
            tracker.PackageFilePath = filePath;

            trackerRepo.Create(tracker);

            trackerRepo.Save();

        }

        private string ConvertTimeFormat(string time)
        {
            string hour = "";
            string min = "";

            if (time != "Day Off" && time != "Not Available")
            {
                hour = time.Split(':')[0];
                min = time.Split(':')[1];
            }
            else
            {
                return null;
            }

            switch (hour)
            {
                case "01": return time = "1 : " + min + " am";
                case "02": return time = "2 : " + min + " am";
                case "03": return time = "3 : " + min + " am";
                case "04": return time = "4 : " + min + " am";
                case "05": return time = "5 : " + min + " am";
                case "06": return time = "6 : " + min + " am";
                case "07": return time = "7 : " + min + " am";
                case "08": return time = "8 : " + min + " am";
                case "09": return time = "9 : " + min + " am";
                case "10": return time = "10 : " + min + " am";
                case "11": return time = "11 : " + min + " am";
                case "12": return time = "12 : " + min + " pm";
                case "13": return time = "1 : " + min + " pm";
                case "14": return time = "2 : " + min + " pm";
                case "15": return time = "3 : " + min + " pm";
                case "16": return time = "4 : " + min + " pm";
                case "17": return time = "5 : " + min + " pm";
                case "18": return time = "6 : " + min + " pm";
                case "19": return time = "7 : " + min + " pm";
                case "20": return time = "8 : " + min + " pm";
                case "21": return time = "9 : " + min + " pm";
                case "22": return time = "10 : " + min + " pm";
                case "23": return time = "11 : " + min + " pm";
                case "24": return time = "12 : " + min + " am";
                default: break;
            }

            return time;
        }

        public List<PlanForm> GetAllPlanForms()
        {
            try
            {
                var PlanFormRepo = uow.GetGenericRepository<PlanForm>().GetAll().ToList();
                return PlanFormRepo;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<string> GetAllPdfFields(string PlanFormName)
        {
            try
            {
                List<string> pdfFields = new List<string>();
                string pdfTemplate = HttpContext.Current.Server.MapPath("~/ApplicationDocument/PlanTemplatePdf/" + PlanFormName + ".pdf");
                PdfReader pdfReader = new PdfReader(pdfTemplate);
                string TempFilename = Path.GetTempFileName();
                AcroFields pdfFormFields = pdfReader.AcroFields;
                foreach (DictionaryEntry entry in pdfFormFields.Fields)
                {
                    pdfFields.Add(entry.Key.ToString());
                    Console.WriteLine(entry.Key + " " + entry.Value);
                }
                pdfReader.Close();
                return pdfFields;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string CreatePlanFormXml(string PlanFormName, List<string> GenericVariableList, List<string> PlanVariableList)
        {
            try
            {
                var pp = uow.GetGenericRepository<PlanForm>();
                var PlanFormRepo = uow.GetGenericRepository<PlanForm>().GetAll().ToList();
                PlanForm planForm = PlanFormRepo.Where(p => p.FileName.Contains(PlanFormName)).FirstOrDefault();

                XmlDocument doc = new XmlDocument();
                XmlDeclaration xDeclare = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                XmlElement documentRoot = doc.DocumentElement;
                doc.InsertBefore(xDeclare, documentRoot);
                XmlElement rootEl = (XmlElement)doc.AppendChild(doc.CreateElement("Root"));
                XmlElement child1 = (XmlElement)rootEl.AppendChild(doc.CreateElement("Template"));
                XmlElement child2 = (XmlElement)child1.AppendChild(doc.CreateElement("Properties"));

                XmlAttribute templateAttribute = doc.CreateAttribute("Name");
                templateAttribute.Value = PlanFormName;
                child1.Attributes.Append(templateAttribute);

                for (var i = 0; i < GenericVariableList.Count; i++)
                {
                    XmlElement child3 = (XmlElement)child2.AppendChild(doc.CreateElement("Property"));
                    XmlAttribute propertyAttribute1 = doc.CreateAttribute("GenericVariable");
                    propertyAttribute1.Value = GenericVariableList[i];
                    child3.Attributes.Append(propertyAttribute1);
                    XmlAttribute propertyAttribute2 = doc.CreateAttribute("PlanVariable");
                    propertyAttribute2.Value = PlanVariableList[i];
                    child3.Attributes.Append(propertyAttribute2);
                }

                //doc.Save("D://sample.xml");
                string newFile = HttpContext.Current.Server.MapPath("~/ApplicationDocument/AIPlanTemplateXml/" + PlanFormName + ".xml");
                doc.Save(newFile);
                newFile = "\\ApplicationDocument\\AIPlanTemplateXml\\" + PlanFormName + ".xml";
                planForm.PlanFormXmlPath = newFile;
                //pp.Update(planForm);
                //pp.Save();
                SaveGeneratedXml(planForm.PlanFormID, planForm.PlanFormXmlPath);
                return doc.InnerXml;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void SaveGeneratedXml(int planFormID, string xmlFilePath)
        {
            OtherDocument otherDocument = new OtherDocument();

            try
            {
                var planFormRepo = uow.GetGenericRepository<PlanForm>();
                PlanForm planForm = planFormRepo.Find(s => s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString() && s.PlanFormID == planFormID);

                planForm.PlanFormXmlPath = xmlFilePath;
                planFormRepo.Update(planForm);
                planFormRepo.Save();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PlanForm> AddPlanForm(PlanForm planForm, DocumentDTO document)
        {
            try
            {
                if (document.FileName != null)
                {
                    planForm.PlanFormPath = AddDocument(DocumentRootPath.PLANFORM_DOCUMENT_PATH, DocumentTitle.PLAN_FORM, null, document);

                    //Add Plan Form Information
                    var PlanFormRepo = uow.GetGenericRepository<PlanForm>();
                    string fileName = document.FileName;
                    if (fileName.EndsWith(".pdf"))
                    {
                        fileName = fileName.Substring(0, fileName.LastIndexOf(".pdf"));
                    }
                    planForm.FileName = fileName;
                    planForm.IsXmlGenerated = AHC.CD.Entities.MasterData.Enums.YesNoOption.NO.ToString();
                    planForm.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;
                    PlanFormRepo.Create(planForm);

                    //save the information in the repository
                    await PlanFormRepo.SaveAsync();
                }
                else
                {
                    throw new ProfileManagerException("Unable to upload!!! Please Upload a Plan Form.");
                }

                return planForm;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.PLAN_FORM_EXCEPTION, ex);
            }
        }

        private string AddDocument(string docRootPath, string docTitle, DateTime? expiryDate, DocumentDTO document)
        {
            try
            {
                //Create a profile document object
                ProfileDocument profileDocument = CreateProfileDocumentObject(docTitle, docRootPath, expiryDate);

                //Assign the Doc root path
                document.DocRootPath = docRootPath;

                //Add the document if uploaded
                return profileDocumentManager.AddDocumentInPath(document);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private ProfileDocument CreateProfileDocumentObject(string title, string docPath, DateTime? expiryDate)
        {
            try
            {
                return new ProfileDocument()
                {
                    DocPath = docPath,
                    Title = title,
                    ExpiryDate = expiryDate
                };
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
