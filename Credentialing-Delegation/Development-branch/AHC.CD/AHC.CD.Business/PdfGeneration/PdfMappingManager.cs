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

namespace AHC.CD.Business.PdfGeneration
{
    internal class PdfMappingManager : IPdfMappingManager
    {

        private IProfileRepository profileRepository = null;
        private IUnitOfWork uow = null;
        IDocumentsManager documentManager = null;
        private ProfileDocumentManager profileDocumentManager = null;

        public PdfMappingManager(IUnitOfWork uow, IDocumentsManager documentManager)
        {
            this.profileRepository = uow.GetProfileRepository();
            this.uow = uow;
            this.documentManager = documentManager;
            profileDocumentManager = new ProfileDocumentManager(profileRepository, documentManager);
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
                    "ProfessionalWorkExperiences.ProviderType"
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
                if(res.CredentialingAppointmentDetail == null)
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
            catch (Exception ex)
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
            catch (Exception ex)
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
        public async Task<WelcomeLetterBusinessModel> GenerateWelcomeLetterPDF(WelcomeLetterBusinessModel welcomeletterdata, string templateName, string cduserid, int credLogId)
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
            catch (Exception ex)
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
            catch (Exception ex)
            {
                throw;
            }
        }

        private string CreatePDF(Dictionary<string, string> dataObj, string pdfName, string templateName,string CDUserId)
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

                string pdfName = CreatePDF(dictionary, fileName, templateName, CDUserId);
                return pdfName;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        

        public async Task<string> GeneratePlanFormPDF(int profileId, string templateName, string UserAuthId)
        {
            Profile profile = await GetProfileObject(profileId);
            string CDUserId = GetCredentialingUserId(UserAuthId).ToString();
            PDFMappingDataBusinessModel pmodel = new PDFMappingDataBusinessModel();

           

            #region LOI
            pmodel.CorrespondenceAddress = "14690 SPRING HILL DRIVE SUITE 101";
            pmodel.CorrespondenceAddressCityLOI = "SPRING HILL";
            pmodel.CorrespondenceAddressStateLOI = "FL";
            pmodel.CorrespondenceAddressZipCode = "34609-8102";
            pmodel.CorrespondenceAddressPhone = "352-799-0046";
            pmodel.CorrespondenceAddressFax = "352-799-0042";

            pmodel.RemittanceAddress = "PO BOX 919469";
            pmodel.RemittanceCity = "ORLANDO";
            pmodel.RemittanceState = "FL";
            pmodel.RemittanceZipCode = "32891-9469";
            pmodel.RemittancePhone = "727-823-2188";
            pmodel.RemittanceFax = "727-828-0723";

            pmodel.PrimaryAddress = "Access HealthCare Physicians LLC";
            pmodel.SecondaryAddress = "Access HealthCare Physicians LLC";
            pmodel.INC_Name = "Access HealthCare Physicians LLC";
            pmodel.INC_Address = "PO BOX 919469";
            pmodel.INC_City = "ORLANDO";
            pmodel.INC_State = "FL";
            pmodel.INC_ZipCode = "32891-9469";
            pmodel.EmployerIDentificationNumber = "45-1444883";
            #endregion

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
                foreach (var item in profile.LanguageInfo.KnownLanguages)
                {
                    if(item != null)
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
                        pmodel.Specialty_PrimaryTaxonomyCode = primaryInfo.Specialty.TaxonomyCode;
                    }

                    if (primaryInfo.IsBoardCertified != null && primaryInfo.IsBoardCertified == AHC.CD.Entities.MasterData.Enums.YesNoOption.YES.ToString())
                        pmodel.Specialty_PrimaryIsBoardCertified = primaryInfo.IsBoardCertified.ToString();

                    if (primaryInfo.SpecialtyBoardCertifiedDetail != null)
                    {
                        pmodel.Specialty_PrimaryCertificate = primaryInfo.SpecialtyBoardCertifiedDetail.CertificateNumber;
                        pmodel.Specialty_PrimaryLastRecertificationDate = ConvertToDateString(primaryInfo.SpecialtyBoardCertifiedDetail.LastReCerificationDate);
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

                    if (secondaryInfo.Count > 0)
                    {
                        if (secondaryInfo.ElementAt(secondaryCount - 1).IsBoardCertified != null && secondaryInfo.ElementAt(secondaryCount - 1).IsBoardCertified == AHC.CD.Entities.MasterData.Enums.YesNoOption.YES.ToString())
                            pmodel.Specialty_IsBoardCertified1 = secondaryInfo.ElementAt(secondaryCount - 1).IsBoardCertified.ToString();

                        if (secondaryInfo.ElementAt(secondaryCount - 1).SpecialtyBoardCertifiedDetail != null)
                        {
                            pmodel.Specialty_Certificate1 = secondaryInfo.ElementAt(secondaryCount - 1).SpecialtyBoardCertifiedDetail.CertificateNumber;
                            pmodel.Specialty_LastRecertificationDate1 = ConvertToDateString(secondaryInfo.ElementAt(secondaryCount - 1).SpecialtyBoardCertifiedDetail.LastReCerificationDate);
                            pmodel.Specialty_ExpirationDate1 = ConvertToDateString(secondaryInfo.ElementAt(secondaryCount - 1).SpecialtyBoardCertifiedDetail.ExpirationDate);

                            if (secondaryInfo.ElementAt(secondaryCount - 1).SpecialtyBoardCertifiedDetail.SpecialtyBoard != null)                            
                                pmodel.Specialty_BoardName1 = secondaryInfo.ElementAt(secondaryCount - 1).SpecialtyBoardCertifiedDetail.SpecialtyBoard.Name;
                        }

                        if (secondaryInfo.ElementAt(secondaryCount - 1) != null && secondaryInfo.ElementAt(secondaryCount - 1).Specialty != null)
                        {
                            pmodel.Specialty_SpecialtyName1 = secondaryInfo.ElementAt(secondaryCount - 1).Specialty.Name;
                            pmodel.Specialty_TaxonomyCode1 = secondaryInfo.ElementAt(secondaryCount - 1).Specialty.TaxonomyCode;
                        }
                    }
                }
                else
                {
                    if (secondaryInfo.Count > 0)
                    {
                        if (secondaryInfo.ElementAt(secondaryCount - 1).IsBoardCertified != null && secondaryInfo.ElementAt(secondaryCount - 1).IsBoardCertified == AHC.CD.Entities.MasterData.Enums.YesNoOption.YES.ToString())
                            pmodel.Specialty_PrimaryIsBoardCertified = secondaryInfo.ElementAt(secondaryCount - 1).IsBoardCertified.ToString();

                        if (secondaryInfo.ElementAt(secondaryCount - 1).SpecialtyBoardCertifiedDetail != null)
                        {
                            pmodel.Specialty_PrimaryCertificate = secondaryInfo.ElementAt(secondaryCount - 1).SpecialtyBoardCertifiedDetail.CertificateNumber;
                            pmodel.Specialty_PrimaryLastRecertificationDate = ConvertToDateString(secondaryInfo.ElementAt(secondaryCount - 1).SpecialtyBoardCertifiedDetail.LastReCerificationDate);
                            pmodel.Specialty_PrimaryExpirationDate = ConvertToDateString(secondaryInfo.ElementAt(secondaryCount - 1).SpecialtyBoardCertifiedDetail.ExpirationDate);

                            if (secondaryInfo.ElementAt(secondaryCount - 1).SpecialtyBoardCertifiedDetail.SpecialtyBoard != null)   
                                pmodel.Specialty_PrimaryBoardName = secondaryInfo.ElementAt(secondaryCount - 1).SpecialtyBoardCertifiedDetail.SpecialtyBoard.Name;
                        }

                        if (secondaryInfo.ElementAt(secondaryCount - 1) != null && secondaryInfo.ElementAt(secondaryCount - 1).Specialty != null)
                        {
                            pmodel.Specialty_PrimarySpecialtyName = secondaryInfo.ElementAt(secondaryCount - 1).Specialty.Name;
                            pmodel.Specialty_PrimaryTaxonomyCode = secondaryInfo.ElementAt(secondaryCount - 1).Specialty.TaxonomyCode;
                        }
                    }
                    if (secondaryInfo.Count > 1)
                    {
                        if (secondaryInfo.ElementAt(secondaryCount - 2).IsBoardCertified != null && secondaryInfo.ElementAt(secondaryCount - 2).IsBoardCertified == AHC.CD.Entities.MasterData.Enums.YesNoOption.YES.ToString())
                            pmodel.Specialty_IsBoardCertified1 = secondaryInfo.ElementAt(secondaryCount - 2).IsBoardCertified.ToString();

                        if (secondaryInfo.ElementAt(secondaryCount - 2).SpecialtyBoardCertifiedDetail != null && secondaryInfo.ElementAt(secondaryCount - 2).SpecialtyBoardCertifiedDetail.SpecialtyBoard != null)
                        {
                            pmodel.Specialty_Certificate1 = secondaryInfo.ElementAt(secondaryCount - 2).SpecialtyBoardCertifiedDetail.CertificateNumber;
                            pmodel.Specialty_LastRecertificationDate1 = ConvertToDateString(secondaryInfo.ElementAt(secondaryCount - 2).SpecialtyBoardCertifiedDetail.LastReCerificationDate);
                            pmodel.Specialty_ExpirationDate1 = ConvertToDateString(secondaryInfo.ElementAt(secondaryCount - 2).SpecialtyBoardCertifiedDetail.ExpirationDate);
                            
                            if (secondaryInfo.ElementAt(secondaryCount - 2).SpecialtyBoardCertifiedDetail.SpecialtyBoard != null)
                                pmodel.Specialty_BoardName1 = secondaryInfo.ElementAt(secondaryCount - 2).SpecialtyBoardCertifiedDetail.SpecialtyBoard.Name;
                        }

                        if (secondaryInfo.ElementAt(secondaryCount - 2) != null && secondaryInfo.ElementAt(secondaryCount - 2).Specialty != null)
                        {
                            pmodel.Specialty_SpecialtyName1 = secondaryInfo.ElementAt(secondaryCount - 2).Specialty.Name;
                            pmodel.Specialty_TaxonomyCode1 = secondaryInfo.ElementAt(secondaryCount - 2).Specialty.TaxonomyCode;
                        }
                    }
                }
            }

            #endregion

            #region Practice Location

            if (profile.PracticeLocationDetails.Count > 0)
            {
                List<PracticeLocationDetail> practiceLocation = new List<PracticeLocationDetail>();

                var primaryPracticeLocation = profile.PracticeLocationDetails.Where(s => s.IsPrimary == AHC.CD.Entities.MasterData.Enums.YesNoOption.YES.ToString() && s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();
                var secondaryPracticeLocation = profile.PracticeLocationDetails.Where(s => s.IsPrimary == AHC.CD.Entities.MasterData.Enums.YesNoOption.NO.ToString() && s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();
                

                if (primaryPracticeLocation.Count > 0)
                {
                    practiceLocation = primaryPracticeLocation;

                }
                else
                {
                    practiceLocation = secondaryPracticeLocation;
                }

                var practiceLocationCount = practiceLocation.Count;

                #region Practice Location 1

                if (practiceLocationCount > 0 && practiceLocation[practiceLocationCount - 1] != null)
                {

                    #region Address 1

                    if (practiceLocation[practiceLocationCount - 1].Facility != null)
                    {
                        pmodel.General_PracticeLocationAddress1 = practiceLocation[practiceLocationCount - 1].Facility.Street + " " + practiceLocation[practiceLocationCount - 1].Facility.City + ", " + practiceLocation[practiceLocationCount - 1].Facility.State + ", " + practiceLocation[practiceLocationCount - 1].Facility.ZipCode;

                        if (practiceLocation[practiceLocationCount - 1].Facility.Telephone != null)
                        {
                            pmodel.General_PhoneFirstThreeDigit1 = practiceLocation[practiceLocationCount - 1].Facility.Telephone.Substring(0, practiceLocation[practiceLocationCount - 1].Facility.Telephone.Length - 7);
                            pmodel.General_PhoneSecondThreeDigit1 = practiceLocation[practiceLocationCount - 1].Facility.Telephone.Substring(3, 3);
                            pmodel.General_PhoneLastFourDigit1 = practiceLocation[practiceLocationCount - 1].Facility.Telephone.Substring(6);

                            pmodel.General_Phone1 = pmodel.General_PhoneFirstThreeDigit1 + "-" + pmodel.General_PhoneSecondThreeDigit1 + "-" + pmodel.General_PhoneLastFourDigit1;
                            pmodel.LocationAddress_Line3 = "Phone : " + pmodel.General_Phone1;
                        }
                            
                        
                        pmodel.General_Email1 = practiceLocation[practiceLocationCount - 1].Facility.EmailAddress;

                        if (practiceLocation[practiceLocationCount - 1].Facility.Fax != null)
                        {
                            //pmodel.General_Fax1 = practiceLocation[practiceLocationCount - 1].Facility.Fax.Substring(3, practiceLocation[practiceLocationCount - 1].Facility.Fax.Length - 3);

                            pmodel.General_FaxFirstThreeDigit1 = practiceLocation[practiceLocationCount - 1].Facility.Fax.Substring(0, practiceLocation[practiceLocationCount - 1].Facility.Telephone.Length - 7);
                            pmodel.General_FaxSecondThreeDigit1 = practiceLocation[practiceLocationCount - 1].Facility.Fax.Substring(3, 3);
                            pmodel.General_FaxLastFourDigit1 = practiceLocation[practiceLocationCount - 1].Facility.Fax.Substring(6);

                            pmodel.General_Fax1 = pmodel.General_FaxFirstThreeDigit1 + "-" + pmodel.General_FaxSecondThreeDigit1 + "-" + pmodel.General_FaxLastFourDigit1;
                            pmodel.LocationAddress_Line3 = pmodel.LocationAddress_Line3 + " " + "Fax : " + pmodel.General_Fax1;
                        }

                        pmodel.General_AccessGroupName1 = "Access Healthcare Physicians, LLC";
                        pmodel.General_Access2GroupName1 = "Access 2 Healthcare Physicians, LLC";

                        pmodel.General_AccessGroupTaxId1 = "451444883";
                        pmodel.General_Access2GroupTaxId1 = "451024515";
                        
                        pmodel.General_Suite1 = practiceLocation[practiceLocationCount - 1].Facility.Building;
                        pmodel.General_Street1 = practiceLocation[practiceLocationCount - 1].Facility.Street;
                        pmodel.General_City1 = practiceLocation[practiceLocationCount - 1].Facility.City;
                        pmodel.General_State1 = practiceLocation[practiceLocationCount - 1].Facility.State;
                        pmodel.General_ZipCode1 = practiceLocation[practiceLocationCount - 1].Facility.ZipCode;
                        pmodel.General_Country1 = practiceLocation[practiceLocationCount - 1].Facility.Country;
                        pmodel.General_County1 = practiceLocation[practiceLocationCount - 1].Facility.County;
                        pmodel.General_IsCurrentlyPracticing1 = practiceLocation[practiceLocationCount - 1].CurrentlyPracticingAtThisAddress;
                        pmodel.LocationAddress_Line1 = pmodel.General_Street1 + " " + pmodel.General_Suite1;
                        pmodel.LocationAddress_Line2 = pmodel.General_City1 + ", " + pmodel.General_State1 + " " + pmodel.General_ZipCode1;


                        #region Languages                        

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

                        #endregion

                    }

                    #endregion

                    #region Open Practice Status

                    if (practiceLocation[practiceLocationCount - 1].OpenPracticeStatus != null)
                    {
                        pmodel.OpenPractice_AgeLimitations1 = practiceLocation[practiceLocationCount - 1].OpenPracticeStatus.MinimumAge + " - " + practiceLocation[practiceLocationCount - 1].OpenPracticeStatus.MaximumAge;
                    }

                    #endregion

                    #region Office Manager 1

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

                    #endregion

                    #region Billing Contact 1

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

                            pmodel.BillingContact_Fax1 = pmodel.BillingContact_FaxFirstThreeDigit1 + "-" + pmodel.BillingContact_FaxSecondThreeDigit1 + "-" + pmodel.BillingContact_FaxLastFourDigit1;
                        }
                    }

                    #endregion

                    #region Payment and Remittance 1

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

                    #endregion

                    #region Office Hours 1

                    if (practiceLocation[practiceLocationCount - 1].OfficeHour != null)
                    {
                        if (practiceLocation[practiceLocationCount - 1].OfficeHour.PracticeDays.Count > 0 && practiceLocation[practiceLocationCount - 1].OfficeHour.PracticeDays.First().DailyHours.Count > 0)
                        {

                            pmodel.OfficeHour_StartMonday1 = ConvertTimeFormat(practiceLocation[practiceLocationCount - 1].OfficeHour.PracticeDays.ElementAt(0).DailyHours.First().StartTime);
                            pmodel.OfficeHour_EndMonday1 = ConvertTimeFormat(practiceLocation[practiceLocationCount - 1].OfficeHour.PracticeDays.ElementAt(0).DailyHours.First().EndTime);

                            pmodel.OfficeHour_Monday1 = pmodel.OfficeHour_StartMonday1 + " - " + pmodel.OfficeHour_EndMonday1;

                            pmodel.OfficeHour_StartTuesday1 = ConvertTimeFormat(practiceLocation[practiceLocationCount - 1].OfficeHour.PracticeDays.ElementAt(1).DailyHours.First().StartTime);
                            pmodel.OfficeHour_EndTuesday1 = ConvertTimeFormat(practiceLocation[practiceLocationCount - 1].OfficeHour.PracticeDays.ElementAt(1).DailyHours.First().EndTime);

                            pmodel.OfficeHour_Tuesday1 = pmodel.OfficeHour_StartTuesday1 + " - " + pmodel.OfficeHour_EndTuesday1;

                            pmodel.OfficeHour_StartWednesday1 = ConvertTimeFormat(practiceLocation[practiceLocationCount - 1].OfficeHour.PracticeDays.ElementAt(2).DailyHours.First().StartTime);
                            pmodel.OfficeHour_EndWednesday1 = ConvertTimeFormat(practiceLocation[practiceLocationCount - 1].OfficeHour.PracticeDays.ElementAt(2).DailyHours.First().EndTime);

                            pmodel.OfficeHour_Wednesday1 = pmodel.OfficeHour_StartWednesday1 + " - " + pmodel.OfficeHour_EndWednesday1;

                            pmodel.OfficeHour_StartThursday1 = ConvertTimeFormat(practiceLocation[practiceLocationCount - 1].OfficeHour.PracticeDays.ElementAt(3).DailyHours.First().StartTime);
                            pmodel.OfficeHour_EndThursday1 = ConvertTimeFormat(practiceLocation[practiceLocationCount - 1].OfficeHour.PracticeDays.ElementAt(3).DailyHours.First().EndTime);

                            pmodel.OfficeHour_Thursday1 = pmodel.OfficeHour_StartThursday1 + " - " + pmodel.OfficeHour_EndThursday1;

                            pmodel.OfficeHour_StartFriday1 = ConvertTimeFormat(practiceLocation[practiceLocationCount - 1].OfficeHour.PracticeDays.ElementAt(4).DailyHours.First().StartTime);
                            pmodel.OfficeHour_EndFriday1 = ConvertTimeFormat(practiceLocation[practiceLocationCount - 1].OfficeHour.PracticeDays.ElementAt(4).DailyHours.First().EndTime);

                            pmodel.OfficeHour_Friday1 = pmodel.OfficeHour_StartFriday1 + " - " + pmodel.OfficeHour_EndFriday1;

                            pmodel.OfficeHour_StartSaturday1 = ConvertTimeFormat(practiceLocation[practiceLocationCount - 1].OfficeHour.PracticeDays.ElementAt(5).DailyHours.First().StartTime);
                            pmodel.OfficeHour_EndSaturday1 = ConvertTimeFormat(practiceLocation[practiceLocationCount - 1].OfficeHour.PracticeDays.ElementAt(5).DailyHours.First().EndTime);

                            pmodel.OfficeHour_Saturday1 = pmodel.OfficeHour_StartSaturday1 + " - " + pmodel.OfficeHour_EndSaturday1;

                            pmodel.OfficeHour_StartSunday1 = ConvertTimeFormat(practiceLocation[practiceLocationCount - 1].OfficeHour.PracticeDays.ElementAt(6).DailyHours.First().StartTime);
                            pmodel.OfficeHour_EndSunday1 = ConvertTimeFormat(practiceLocation[practiceLocationCount - 1].OfficeHour.PracticeDays.ElementAt(6).DailyHours.First().EndTime);

                            pmodel.OfficeHour_Sunday1 = pmodel.OfficeHour_StartSunday1 + " - " + pmodel.OfficeHour_EndSunday1;
                        }
                    }

                    #endregion

                    #region Supervisiong Provider 1

                    var supervisingProvider = practiceLocation[practiceLocationCount - 1].PracticeProviders.
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

                    var patners = practiceLocation[practiceLocationCount - 1].PracticeProviders.
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

                }

                #endregion

                if (primaryPracticeLocation.Count > 0)
                {
                    practiceLocation = profile.PracticeLocationDetails.Where(s => s.IsPrimary == AHC.CD.Entities.MasterData.Enums.YesNoOption.NO.ToString() && s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList(); ;
                    practiceLocationCount = practiceLocation.Count + 1;
                }

                #region Practice Location 2

                if (practiceLocationCount > 1 && practiceLocation[practiceLocationCount - 2] != null)
                {

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

                    #region Address 2

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
                        pmodel.LocationAddress2_Line1 = pmodel.General_Street2 + " " + pmodel.General_Suite2;
                        pmodel.LocationAddress2_Line2 = pmodel.General_City2 + ", " + pmodel.General_State2 + " " + pmodel.General_ZipCode2;

                        pmodel.General_AccessGroupName2 = "Access Healthcare Physicians, LLC";
                        pmodel.General_Access2GroupName2 = "Access 2 Healthcare Physicians, LLC";

                        pmodel.General_AccessGroupTaxId2 = "451444883";
                        pmodel.General_Access2GroupTaxId2 = "451024515";

                        pmodel.Provider_FullNameTitleBCBS = pmodel.Provider_FullNameTitle;
                        pmodel.Provider_FullNameTitle_Additional = pmodel.Provider_FullNameTitle;

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

                        #region Languages

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

                        if (practiceLocation[practiceLocationCount - 2].Facility.FacilityDetail.Service.PracticeType != null && practiceLocation[practiceLocationCount - 2].Facility.FacilityDetail.Service.PracticeType.Title == "Solo Practice")
                            pmodel.Provider_SoloPractice2 = practiceLocation[practiceLocationCount - 2].Facility.FacilityDetail.Service.PracticeType.Title;

                        #endregion
                    }

                    #endregion

                    #region Office Manager 2

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

                    #endregion

                    #region Billing Contact 2

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
                            pmodel.BillingContact_PhoneLastFourDigit2 = practiceLocation[practiceLocationCount - 2].BillingContactPerson.Telephone.Substring(6);

                            pmodel.BillingContact_Phone2 = pmodel.BillingContact_PhoneFirstThreeDigit2 + "-" + pmodel.BillingContact_PhoneSecondThreeDigit2 + "-" + pmodel.BillingContact_PhoneLastFourDigit2;
                        }




                        if (practiceLocation[practiceLocationCount - 2].BillingContactPerson.Fax != null)
                        {
                            //pmodel.General_Fax1 = practiceLocation[practiceLocationCount - 1].Facility.Fax.Substring(3, practiceLocation[practiceLocationCount - 1].Facility.Fax.Length - 3);

                            pmodel.BillingContact_FaxFirstThreeDigit2 = practiceLocation[practiceLocationCount - 2].BillingContactPerson.Fax.Substring(0, practiceLocation[practiceLocationCount - 2].BillingContactPerson.Telephone.Length - 7);
                            pmodel.BillingContact_FaxSecondThreeDigit2 = practiceLocation[practiceLocationCount - 2].BillingContactPerson.Fax.Substring(3, 3);
                            pmodel.BillingContact_FaxLastFourDigit2 = practiceLocation[practiceLocationCount - 2].BillingContactPerson.Fax.Substring(6);

                            pmodel.BillingContact_Fax2 = pmodel.BillingContact_FaxFirstThreeDigit2 + "-" + pmodel.BillingContact_FaxSecondThreeDigit2 + "-" + pmodel.BillingContact_FaxLastFourDigit2;
                        }

                    }

                    #endregion

                    #region Payment and Remittance 2

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

                    #endregion

                    #region Open Practice Status 2

                    if (practiceLocation[practiceLocationCount - 2].OpenPracticeStatus != null)
                    {
                        pmodel.OpenPractice_AgeLimitations2 = practiceLocation[practiceLocationCount - 2].OpenPracticeStatus.MinimumAge + " - " + practiceLocation[practiceLocationCount - 2].OpenPracticeStatus.MaximumAge;
                    }

                    #endregion

                    #region Office Hours 2

                    if (practiceLocation[practiceLocationCount - 2].OfficeHour != null)
                    {
                        if (practiceLocation[practiceLocationCount - 2].OfficeHour.PracticeDays.Count > 0 && practiceLocation[practiceLocationCount - 2].OfficeHour.PracticeDays.First().DailyHours.Count > 0)
                        {


                            pmodel.OfficeHour_StartMonday2 = ConvertTimeFormat(practiceLocation[practiceLocationCount - 2].OfficeHour.PracticeDays.ElementAt(0).DailyHours.First().StartTime);
                            pmodel.OfficeHour_EndMonday2 = ConvertTimeFormat(practiceLocation[practiceLocationCount - 2].OfficeHour.PracticeDays.ElementAt(0).DailyHours.First().EndTime);

                            pmodel.OfficeHour_Monday2 = pmodel.OfficeHour_StartMonday2 + " - " + pmodel.OfficeHour_EndMonday2;

                            pmodel.OfficeHour_StartTuesday2 = ConvertTimeFormat(practiceLocation[practiceLocationCount - 2].OfficeHour.PracticeDays.ElementAt(1).DailyHours.First().StartTime);
                            pmodel.OfficeHour_EndTuesday2 = ConvertTimeFormat(practiceLocation[practiceLocationCount - 2].OfficeHour.PracticeDays.ElementAt(1).DailyHours.First().EndTime);

                            pmodel.OfficeHour_Tuesday2 = pmodel.OfficeHour_StartTuesday2 + " - " + pmodel.OfficeHour_EndTuesday2;

                            pmodel.OfficeHour_StartWednesday2 = ConvertTimeFormat(practiceLocation[practiceLocationCount - 2].OfficeHour.PracticeDays.ElementAt(2).DailyHours.First().StartTime);
                            pmodel.OfficeHour_EndWednesday2 = ConvertTimeFormat(practiceLocation[practiceLocationCount - 2].OfficeHour.PracticeDays.ElementAt(2).DailyHours.First().EndTime);

                            pmodel.OfficeHour_Wednesday2 = pmodel.OfficeHour_StartWednesday2 + " - " + pmodel.OfficeHour_EndWednesday2;

                            pmodel.OfficeHour_StartThursday2 = ConvertTimeFormat(practiceLocation[practiceLocationCount - 2].OfficeHour.PracticeDays.ElementAt(3).DailyHours.First().StartTime);
                            pmodel.OfficeHour_EndThursday2 = ConvertTimeFormat(practiceLocation[practiceLocationCount - 2].OfficeHour.PracticeDays.ElementAt(3).DailyHours.First().EndTime);

                            pmodel.OfficeHour_Thursday2 = pmodel.OfficeHour_StartThursday2 + " - " + pmodel.OfficeHour_EndThursday2;

                            pmodel.OfficeHour_StartFriday2 = ConvertTimeFormat(practiceLocation[practiceLocationCount - 2].OfficeHour.PracticeDays.ElementAt(4).DailyHours.First().StartTime);
                            pmodel.OfficeHour_EndFriday2 = ConvertTimeFormat(practiceLocation[practiceLocationCount - 2].OfficeHour.PracticeDays.ElementAt(4).DailyHours.First().EndTime);

                            pmodel.OfficeHour_Friday2 = pmodel.OfficeHour_StartFriday2 + " - " + pmodel.OfficeHour_EndFriday2;

                            pmodel.OfficeHour_StartSaturday2 = ConvertTimeFormat(practiceLocation[practiceLocationCount - 2].OfficeHour.PracticeDays.ElementAt(5).DailyHours.First().StartTime);
                            pmodel.OfficeHour_EndSaturday2 = ConvertTimeFormat(practiceLocation[practiceLocationCount - 2].OfficeHour.PracticeDays.ElementAt(5).DailyHours.First().EndTime);

                            pmodel.OfficeHour_Saturday2 = pmodel.OfficeHour_StartSaturday2 + " - " + pmodel.OfficeHour_EndSaturday2;

                            pmodel.OfficeHour_StartSunday2 = ConvertTimeFormat(practiceLocation[practiceLocationCount - 2].OfficeHour.PracticeDays.ElementAt(6).DailyHours.First().StartTime);
                            pmodel.OfficeHour_EndSunday2 = ConvertTimeFormat(practiceLocation[practiceLocationCount - 2].OfficeHour.PracticeDays.ElementAt(6).DailyHours.First().EndTime);

                            pmodel.OfficeHour_Sunday2 = pmodel.OfficeHour_StartSunday2 + " - " + pmodel.OfficeHour_EndSunday2;
                        }
                    }

                    #endregion

                    #region Supervisiong Provider 2

                    var supervisingProvider = practiceLocation[practiceLocationCount - 1].PracticeProviders.
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

                        var specialities = supervisingProvider.ElementAt(supervisorCount - 1).PracticeProviderSpecialties.Where(s => s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString()).ToList();

                        if (specialities.Count > 0)
                        {
                            if (specialities.ElementAt(specialities.Count - 1).Specialty != null)
                                pmodel.CoveringColleague_Specialty2 = specialities.ElementAt(specialities.Count - 1).Specialty.Name;
                        }
                    }

                    #endregion

                    #region Covering Colleagues/Partners 2

                    var patners = practiceLocation[practiceLocationCount - 1].PracticeProviders.
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

                #region Practice Location 3 

                if (practiceLocationCount > 2 && practiceLocation[practiceLocationCount - 3] != null)
                {

                    #region Address 3

                    if (practiceLocation[practiceLocationCount - 3].Facility != null)
                    {
                        pmodel.General_PracticeLocationAddress3 = practiceLocation[practiceLocationCount - 3].Facility.Street + " " + practiceLocation[practiceLocationCount - 3].Facility.City + ", " + practiceLocation[practiceLocationCount - 3].Facility.State + ", " + practiceLocation[practiceLocationCount - 3].Facility.ZipCode;
                        //pmodel.General_Phone2 = practiceLocation[practiceLocationCount - 2].Facility.MobileNumber;
                        //pmodel.General_Fax2 = practiceLocation[practiceLocationCount - 2].Facility.FaxNumber;
                        pmodel.General_Suite3 = practiceLocation[practiceLocationCount - 3].Facility.Building;
                        pmodel.General_Street3 = practiceLocation[practiceLocationCount - 3].Facility.Street;
                        pmodel.General_City3 = practiceLocation[practiceLocationCount - 3].Facility.City;
                        pmodel.General_State3 = practiceLocation[practiceLocationCount - 3].Facility.State;
                        pmodel.General_ZipCode3 = practiceLocation[practiceLocationCount - 3].Facility.ZipCode;
                        pmodel.General_Country3= practiceLocation[practiceLocationCount - 3].Facility.Country;
                        pmodel.General_County3 = practiceLocation[practiceLocationCount - 3].Facility.County;
                        pmodel.LocationAddress3_Line1 = pmodel.General_Street3 + " " + pmodel.General_Suite3;
                        pmodel.LocationAddress3_Line2 = pmodel.General_City3 + ", " + pmodel.General_State3 + " " + pmodel.General_ZipCode3;

                        pmodel.General_AccessGroupName3 = "Access Healthcare Physicians, LLC";
                        pmodel.General_Access2GroupName3 = "Access 2 Healthcare Physicians, LLC";

                        pmodel.General_AccessGroupTaxId3 = "451444883";
                        pmodel.General_Access2GroupTaxId3 = "451024515";

                        if (practiceLocation[practiceLocationCount - 3].Facility.Telephone != null)
                        {
                            pmodel.General_PhoneFirstThreeDigit3 = practiceLocation[practiceLocationCount - 3].Facility.Telephone.Substring(0, practiceLocation[practiceLocationCount - 3].Facility.Telephone.Length - 7);
                            pmodel.General_PhoneSecondThreeDigit3 = practiceLocation[practiceLocationCount - 3].Facility.Telephone.Substring(3, 3);
                            pmodel.General_PhoneLastFourDigit3 = practiceLocation[practiceLocationCount - 3].Facility.Telephone.Substring(6);

                            pmodel.General_Phone3 = pmodel.General_PhoneFirstThreeDigit3 + "-" + pmodel.General_PhoneSecondThreeDigit3 + "-" + pmodel.General_PhoneLastFourDigit3;
                        }


                        pmodel.General_Email3 = practiceLocation[practiceLocationCount - 3].Facility.EmailAddress;

                        if (practiceLocation[practiceLocationCount - 3].Facility.Fax != null)
                        {
                            //pmodel.General_Fax1 = practiceLocation[practiceLocationCount - 1].Facility.Fax.Substring(3, practiceLocation[practiceLocationCount - 1].Facility.Fax.Length - 3);

                            pmodel.General_FaxFirstThreeDigit3 = practiceLocation[practiceLocationCount - 3].Facility.Fax.Substring(0, practiceLocation[practiceLocationCount - 3].Facility.Telephone.Length - 7);
                            pmodel.General_FaxSecondThreeDigit3 = practiceLocation[practiceLocationCount - 3].Facility.Fax.Substring(3, 3);
                            pmodel.General_FaxLastFourDigit3 = practiceLocation[practiceLocationCount - 3].Facility.Fax.Substring(6);

                            pmodel.General_Fax3 = pmodel.General_FaxFirstThreeDigit3 + "-" + pmodel.General_FaxSecondThreeDigit3 + "-" + pmodel.General_FaxLastFourDigit3;
                        }

                        #region Languages

                        if (practiceLocation[practiceLocationCount - 3].Facility.FacilityDetail != null && practiceLocation[practiceLocationCount - 3].Facility.FacilityDetail.Language != null)
                        {
                            var languages = practiceLocation[practiceLocationCount - 3].Facility.FacilityDetail.Language.NonEnglishLanguages.Where(l => l.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();
                            if (languages.Count > 0)
                            {
                                foreach (var language in languages)
                                {
                                    if (language != null)
                                        pmodel.Languages_Known3 += language.Language + ",";
                                }
                            }
                        }

                        if (practiceLocation[practiceLocationCount - 3].Facility.FacilityDetail.Service.PracticeType != null && practiceLocation[practiceLocationCount - 3].Facility.FacilityDetail.Service.PracticeType.Title == "Solo Practice")
                            pmodel.Provider_SoloPractice3 = practiceLocation[practiceLocationCount - 3].Facility.FacilityDetail.Service.PracticeType.Title;

                        #endregion
                    }

                    #endregion

                    #region Office Manager 3

                    if (practiceLocation[practiceLocationCount - 3].BusinessOfficeManagerOrStaff != null)
                    {
                        if (practiceLocation[practiceLocationCount - 3].BusinessOfficeManagerOrStaff.MiddleName != null)
                            pmodel.OfficeManager_Name3 = practiceLocation[practiceLocationCount - 3].BusinessOfficeManagerOrStaff.FirstName + " " + practiceLocation[practiceLocationCount - 3].BusinessOfficeManagerOrStaff.MiddleName + " " + practiceLocation[practiceLocationCount - 3].BusinessOfficeManagerOrStaff.LastName;
                        else
                            pmodel.OfficeManager_Name3 = practiceLocation[practiceLocationCount - 3].BusinessOfficeManagerOrStaff.FirstName + " " + practiceLocation[practiceLocationCount - 3].BusinessOfficeManagerOrStaff.LastName;

                        //pmodel.OfficeManager_Name3 = practiceLocation[practiceLocationCount - 3].BusinessOfficeManagerOrStaff.FirstName + practiceLocation[practiceLocationCount - 3].BusinessOfficeManagerOrStaff.MiddleName + practiceLocation[practiceLocationCount - 3].BusinessOfficeManagerOrStaff.LastName;
                        pmodel.OfficeManager_Email3 = practiceLocation[practiceLocationCount - 3].BusinessOfficeManagerOrStaff.EmailAddress;
                        pmodel.OfficeManager_PoBoxAddress3 = practiceLocation[practiceLocationCount - 3].BusinessOfficeManagerOrStaff.POBoxAddress;
                        pmodel.OfficeManager_Building3 = practiceLocation[practiceLocationCount - 3].BusinessOfficeManagerOrStaff.Building;
                        pmodel.OfficeManager_Street3 = practiceLocation[practiceLocationCount - 3].BusinessOfficeManagerOrStaff.Street;
                        pmodel.OfficeManager_City3 = practiceLocation[practiceLocationCount - 3].BusinessOfficeManagerOrStaff.City;
                        pmodel.OfficeManager_State3 = practiceLocation[practiceLocationCount - 3].BusinessOfficeManagerOrStaff.State;
                        pmodel.OfficeManager_ZipCode3 = practiceLocation[practiceLocationCount - 3].BusinessOfficeManagerOrStaff.ZipCode;
                        pmodel.OfficeManager_Country3 = practiceLocation[practiceLocationCount - 3].BusinessOfficeManagerOrStaff.Country;
                        pmodel.OfficeManager_County3 = practiceLocation[practiceLocationCount - 3].BusinessOfficeManagerOrStaff.County;

                        if (practiceLocation[practiceLocationCount - 3].BusinessOfficeManagerOrStaff.Telephone != null)
                        {
                            pmodel.OfficeManager_PhoneFirstThreeDigit3 = practiceLocation[practiceLocationCount - 3].BusinessOfficeManagerOrStaff.Telephone.Substring(0, practiceLocation[practiceLocationCount - 3].BusinessOfficeManagerOrStaff.Telephone.Length - 7);
                            pmodel.OfficeManager_PhoneSecondThreeDigit3 = practiceLocation[practiceLocationCount - 3].BusinessOfficeManagerOrStaff.Telephone.Substring(3, 3);
                            pmodel.OfficeManager_PhoneLastFourDigit3 = practiceLocation[practiceLocationCount - 3].BusinessOfficeManagerOrStaff.Telephone.Substring(6);

                            pmodel.OfficeManager_Phone3 = pmodel.OfficeManager_PhoneFirstThreeDigit3 + "-" + pmodel.OfficeManager_PhoneSecondThreeDigit3 + "-" + pmodel.OfficeManager_PhoneLastFourDigit3;
                        }




                        if (practiceLocation[practiceLocationCount - 3].BusinessOfficeManagerOrStaff.Fax != null)
                        {
                            //pmodel.General_Fax1 = practiceLocation[practiceLocationCount - 1].Facility.Fax.Substring(3, practiceLocation[practiceLocationCount - 1].Facility.Fax.Length - 3);

                            pmodel.OfficeManager_FaxFirstThreeDigit3 = practiceLocation[practiceLocationCount - 3].BusinessOfficeManagerOrStaff.Fax.Substring(0, practiceLocation[practiceLocationCount - 3].BusinessOfficeManagerOrStaff.Telephone.Length - 7);
                            pmodel.OfficeManager_FaxSecondThreeDigit3 = practiceLocation[practiceLocationCount - 3].BusinessOfficeManagerOrStaff.Fax.Substring(3, 3);
                            pmodel.OfficeManager_FaxLastFourDigit3 = practiceLocation[practiceLocationCount - 3].BusinessOfficeManagerOrStaff.Fax.Substring(6);

                            pmodel.OfficeManager_Fax3 = pmodel.OfficeManager_FaxFirstThreeDigit3 + "-" + pmodel.OfficeManager_FaxSecondThreeDigit3 + "-" + pmodel.OfficeManager_FaxLastFourDigit3;
                        }
                    }

                    #endregion

                    #region Billing Contact 3

                    if (practiceLocation[practiceLocationCount - 3].BillingContactPerson != null)
                    {
                        if (practiceLocation[practiceLocationCount - 3].BillingContactPerson.MiddleName != null)
                            pmodel.BillingContact_Name3 = practiceLocation[practiceLocationCount - 3].BillingContactPerson.FirstName + " " + practiceLocation[practiceLocationCount - 3].BillingContactPerson.MiddleName + " " + practiceLocation[practiceLocationCount - 3].BillingContactPerson.LastName;
                        else
                            pmodel.BillingContact_Name3 = practiceLocation[practiceLocationCount - 3].BillingContactPerson.FirstName + " " + practiceLocation[practiceLocationCount - 3].BillingContactPerson.LastName;

                        //pmodel.BillingContact_Name2 = practiceLocation[practiceLocationCount - 2].BillingContactPerson.FirstName + practiceLocation[practiceLocationCount - 2].BillingContactPerson.MiddleName + practiceLocation[practiceLocationCount - 2].BillingContactPerson.LastName;
                        pmodel.BillingContact_FirstName3 = practiceLocation[practiceLocationCount - 3].BillingContactPerson.FirstName;
                        pmodel.BillingContact_MiddleName3 = practiceLocation[practiceLocationCount - 3].BillingContactPerson.MiddleName;
                        pmodel.BillingContact_LastName3 = practiceLocation[practiceLocationCount - 3].BillingContactPerson.LastName;
                        pmodel.BillingContact_Email3 = practiceLocation[practiceLocationCount - 3].BillingContactPerson.EmailAddress;
                        pmodel.BillingContact_POBoxAddress3 = practiceLocation[practiceLocationCount - 3].BillingContactPerson.POBoxAddress;
                        pmodel.BillingContact_Suite3 = practiceLocation[practiceLocationCount - 3].BillingContactPerson.Building;
                        pmodel.BillingContact_Street3 = practiceLocation[practiceLocationCount - 3].BillingContactPerson.Street;
                        pmodel.BillingContact_City3 = practiceLocation[practiceLocationCount - 3].BillingContactPerson.City;
                        pmodel.BillingContact_State3 = practiceLocation[practiceLocationCount - 3].BillingContactPerson.State;
                        pmodel.BillingContact_ZipCode3 = practiceLocation[practiceLocationCount - 3].BillingContactPerson.ZipCode;
                        pmodel.BillingContact_Country3 = practiceLocation[practiceLocationCount - 3].BillingContactPerson.Country;
                        pmodel.BillingContact_County3 = practiceLocation[practiceLocationCount - 3].BillingContactPerson.County;

                        if (practiceLocation[practiceLocationCount - 3].BillingContactPerson.Telephone != null)
                        {
                            pmodel.BillingContact_PhoneFirstThreeDigit3 = practiceLocation[practiceLocationCount - 3].BillingContactPerson.Telephone.Substring(0, practiceLocation[practiceLocationCount - 3].BillingContactPerson.Telephone.Length - 7);
                            pmodel.BillingContact_PhoneSecondThreeDigit3 = practiceLocation[practiceLocationCount - 3].BillingContactPerson.Telephone.Substring(3, 3);
                            pmodel.BillingContact_PhoneLastFourDigit3 = practiceLocation[practiceLocationCount - 3].BillingContactPerson.Telephone.Substring(6);

                            pmodel.BillingContact_Phone3 = pmodel.BillingContact_PhoneFirstThreeDigit3 + "-" + pmodel.BillingContact_PhoneSecondThreeDigit3 + "-" + pmodel.BillingContact_PhoneLastFourDigit3;
                        }




                        if (practiceLocation[practiceLocationCount - 3].BillingContactPerson.Fax != null)
                        {
                            //pmodel.General_Fax1 = practiceLocation[practiceLocationCount - 1].Facility.Fax.Substring(3, practiceLocation[practiceLocationCount - 1].Facility.Fax.Length - 3);

                            pmodel.BillingContact_FaxFirstThreeDigit3 = practiceLocation[practiceLocationCount - 3].BillingContactPerson.Fax.Substring(0, practiceLocation[practiceLocationCount - 3].BillingContactPerson.Telephone.Length - 7);
                            pmodel.BillingContact_FaxSecondThreeDigit3 = practiceLocation[practiceLocationCount - 3].BillingContactPerson.Fax.Substring(3, 3);
                            pmodel.BillingContact_FaxLastFourDigit3 = practiceLocation[practiceLocationCount - 3].BillingContactPerson.Fax.Substring(6);

                            pmodel.BillingContact_Fax3 = pmodel.BillingContact_FaxFirstThreeDigit3 + "-" + pmodel.BillingContact_FaxSecondThreeDigit3 + "-" + pmodel.BillingContact_FaxLastFourDigit3;
                        }

                    }

                    #endregion

                    #region Payment and Remittance 3

                    if (practiceLocation[practiceLocationCount - 3].PaymentAndRemittance != null)
                    {
                        if (practiceLocation[practiceLocationCount - 3].PaymentAndRemittance.PaymentAndRemittancePerson != null)
                        {
                            if (practiceLocation[practiceLocationCount - 3].PaymentAndRemittance.PaymentAndRemittancePerson.MiddleName != null)
                                pmodel.PaymentRemittance_Name3 = practiceLocation[practiceLocationCount - 3].PaymentAndRemittance.PaymentAndRemittancePerson.FirstName + " " + practiceLocation[practiceLocationCount - 3].PaymentAndRemittance.PaymentAndRemittancePerson.MiddleName + " " + practiceLocation[practiceLocationCount - 3].PaymentAndRemittance.PaymentAndRemittancePerson.LastName;
                            else
                                pmodel.PaymentRemittance_Name3 = practiceLocation[practiceLocationCount - 3].PaymentAndRemittance.PaymentAndRemittancePerson.FirstName + " " + practiceLocation[practiceLocationCount - 3].PaymentAndRemittance.PaymentAndRemittancePerson.LastName;

                            //pmodel.PaymentRemittance_Name2 = practiceLocation[practiceLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.FirstName + practiceLocation[practiceLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.MiddleName + practiceLocation[practiceLocationCount - 2].PaymentAndRemittance.PaymentAndRemittancePerson.LastName;
                            pmodel.PaymentRemittance_FirstName3 = practiceLocation[practiceLocationCount - 3].PaymentAndRemittance.PaymentAndRemittancePerson.FirstName;
                            pmodel.PaymentRemittance_MiddleName3 = practiceLocation[practiceLocationCount - 3].PaymentAndRemittance.PaymentAndRemittancePerson.MiddleName;
                            pmodel.PaymentRemittance_LastName3 = practiceLocation[practiceLocationCount - 3].PaymentAndRemittance.PaymentAndRemittancePerson.LastName;
                            pmodel.PaymentRemittance_Email3 = practiceLocation[practiceLocationCount - 3].PaymentAndRemittance.PaymentAndRemittancePerson.EmailAddress;
                            pmodel.PaymentRemittance_POBoxAddress3 = practiceLocation[practiceLocationCount - 3].PaymentAndRemittance.PaymentAndRemittancePerson.POBoxAddress;
                            pmodel.PaymentRemittance_Suite3 = practiceLocation[practiceLocationCount - 3].PaymentAndRemittance.PaymentAndRemittancePerson.Building;
                            pmodel.PaymentRemittance_Street3 = practiceLocation[practiceLocationCount - 3].PaymentAndRemittance.PaymentAndRemittancePerson.Street;
                            pmodel.PaymentRemittance_City3 = practiceLocation[practiceLocationCount - 3].PaymentAndRemittance.PaymentAndRemittancePerson.City;
                            pmodel.PaymentRemittance_State3 = practiceLocation[practiceLocationCount - 3].PaymentAndRemittance.PaymentAndRemittancePerson.State;
                            pmodel.PaymentRemittance_ZipCode3 = practiceLocation[practiceLocationCount - 3].PaymentAndRemittance.PaymentAndRemittancePerson.ZipCode;
                            pmodel.PaymentRemittance_Country3 = practiceLocation[practiceLocationCount - 3].PaymentAndRemittance.PaymentAndRemittancePerson.Country;
                            pmodel.PaymentRemittance_County3 = practiceLocation[practiceLocationCount - 3].PaymentAndRemittance.PaymentAndRemittancePerson.County;

                            if (practiceLocation[practiceLocationCount - 3].PaymentAndRemittance.PaymentAndRemittancePerson.Telephone != null)
                            {
                                pmodel.PaymentRemittance_PhoneFirstThreeDigit3 = practiceLocation[practiceLocationCount - 3].PaymentAndRemittance.PaymentAndRemittancePerson.Telephone.Substring(0, practiceLocation[practiceLocationCount - 3].PaymentAndRemittance.PaymentAndRemittancePerson.Telephone.Length - 7);
                                pmodel.PaymentRemittance_PhoneSecondThreeDigit3 = practiceLocation[practiceLocationCount - 3].PaymentAndRemittance.PaymentAndRemittancePerson.Telephone.Substring(3, 3);
                                pmodel.PaymentRemittance_PhoneLastFourDigit3 = practiceLocation[practiceLocationCount - 3].PaymentAndRemittance.PaymentAndRemittancePerson.Telephone.Substring(6);

                                pmodel.PaymentRemittance_Phone3 = pmodel.PaymentRemittance_PhoneFirstThreeDigit3 + "-" + pmodel.PaymentRemittance_PhoneSecondThreeDigit3 + "-" + pmodel.PaymentRemittance_PhoneLastFourDigit3;
                            }




                            if (practiceLocation[practiceLocationCount - 3].PaymentAndRemittance.PaymentAndRemittancePerson.Fax != null)
                            {
                                //pmodel.General_Fax1 = practiceLocation[practiceLocationCount - 1].Facility.Fax.Substring(3, practiceLocation[practiceLocationCount - 1].Facility.Fax.Length - 3);

                                pmodel.PaymentRemittance_FaxFirstThreeDigit3 = practiceLocation[practiceLocationCount - 3].PaymentAndRemittance.PaymentAndRemittancePerson.Fax.Substring(0, practiceLocation[practiceLocationCount - 3].PaymentAndRemittance.PaymentAndRemittancePerson.Telephone.Length - 7);
                                pmodel.PaymentRemittance_FaxSecondThreeDigit3 = practiceLocation[practiceLocationCount - 3].PaymentAndRemittance.PaymentAndRemittancePerson.Fax.Substring(3, 3);
                                pmodel.PaymentRemittance_FaxLastFourDigit3 = practiceLocation[practiceLocationCount - 3].PaymentAndRemittance.PaymentAndRemittancePerson.Fax.Substring(6);

                                pmodel.PaymentRemittance_Fax3 = pmodel.PaymentRemittance_FaxFirstThreeDigit3 + "-" + pmodel.PaymentRemittance_FaxSecondThreeDigit3 + "-" + pmodel.PaymentRemittance_FaxLastFourDigit3;
                            }
                        }
                        pmodel.PaymentRemittance_ElectronicBillCapability3 = practiceLocation[practiceLocationCount - 3].PaymentAndRemittance.ElectronicBillingCapability;
                        pmodel.PaymentRemittance_BillingDepartment3 = practiceLocation[practiceLocationCount - 3].PaymentAndRemittance.BillingDepartment;
                        pmodel.PaymentRemittance_ChekPayableTo3 = practiceLocation[practiceLocationCount - 3].PaymentAndRemittance.CheckPayableTo;
                        pmodel.PaymentRemittance_Office3 = practiceLocation[practiceLocationCount - 3].PaymentAndRemittance.Office;

                    }

                    #endregion

                    #region Open Practice Status 3

                    if (practiceLocation[practiceLocationCount - 3].OpenPracticeStatus != null)
                    {
                        pmodel.OpenPractice_AgeLimitations3 = practiceLocation[practiceLocationCount - 3].OpenPracticeStatus.MinimumAge + " - " + practiceLocation[practiceLocationCount - 3].OpenPracticeStatus.MaximumAge;
                    }

                    #endregion

                    #region Office Hours 3

                    if (practiceLocation[practiceLocationCount - 3].OfficeHour != null)
                    {
                        if (practiceLocation[practiceLocationCount - 3].OfficeHour.PracticeDays.Count > 0 && practiceLocation[practiceLocationCount - 3].OfficeHour.PracticeDays.First().DailyHours.Count > 0)
                        {

                            pmodel.OfficeHour_StartMonday3 = ConvertTimeFormat(practiceLocation[practiceLocationCount - 3].OfficeHour.PracticeDays.ElementAt(0).DailyHours.First().StartTime);
                            pmodel.OfficeHour_EndMonday3 = ConvertTimeFormat(practiceLocation[practiceLocationCount - 3].OfficeHour.PracticeDays.ElementAt(0).DailyHours.First().EndTime);

                            pmodel.OfficeHour_Monday3 = pmodel.OfficeHour_StartMonday3 + " - " + pmodel.OfficeHour_EndMonday3;

                            pmodel.OfficeHour_StartTuesday3 = ConvertTimeFormat(practiceLocation[practiceLocationCount - 3].OfficeHour.PracticeDays.ElementAt(1).DailyHours.First().StartTime);
                            pmodel.OfficeHour_EndTuesday3 = ConvertTimeFormat(practiceLocation[practiceLocationCount - 3].OfficeHour.PracticeDays.ElementAt(1).DailyHours.First().EndTime);

                            pmodel.OfficeHour_Tuesday3 = pmodel.OfficeHour_StartTuesday3 + " - " + pmodel.OfficeHour_EndTuesday3;

                            pmodel.OfficeHour_StartWednesday3 = ConvertTimeFormat(practiceLocation[practiceLocationCount - 3].OfficeHour.PracticeDays.ElementAt(2).DailyHours.First().StartTime);
                            pmodel.OfficeHour_EndWednesday3 = ConvertTimeFormat(practiceLocation[practiceLocationCount - 3].OfficeHour.PracticeDays.ElementAt(2).DailyHours.First().EndTime);

                            pmodel.OfficeHour_Wednesday3 = pmodel.OfficeHour_StartWednesday3 + " - " + pmodel.OfficeHour_EndWednesday3;

                            pmodel.OfficeHour_StartThursday3 = ConvertTimeFormat(practiceLocation[practiceLocationCount - 3].OfficeHour.PracticeDays.ElementAt(3).DailyHours.First().StartTime);
                            pmodel.OfficeHour_EndThursday3 = ConvertTimeFormat(practiceLocation[practiceLocationCount - 3].OfficeHour.PracticeDays.ElementAt(3).DailyHours.First().EndTime);

                            pmodel.OfficeHour_Thursday3 = pmodel.OfficeHour_StartThursday3 + " - " + pmodel.OfficeHour_EndThursday3;

                            pmodel.OfficeHour_StartFriday3 = ConvertTimeFormat(practiceLocation[practiceLocationCount - 3].OfficeHour.PracticeDays.ElementAt(4).DailyHours.First().StartTime);
                            pmodel.OfficeHour_EndFriday3 = ConvertTimeFormat(practiceLocation[practiceLocationCount - 3].OfficeHour.PracticeDays.ElementAt(4).DailyHours.First().EndTime);

                            pmodel.OfficeHour_Friday3 = pmodel.OfficeHour_StartFriday3 + " - " + pmodel.OfficeHour_EndFriday3;

                            pmodel.OfficeHour_StartSaturday3 = ConvertTimeFormat(practiceLocation[practiceLocationCount - 3].OfficeHour.PracticeDays.ElementAt(5).DailyHours.First().StartTime);
                            pmodel.OfficeHour_EndSaturday3 = ConvertTimeFormat(practiceLocation[practiceLocationCount - 3].OfficeHour.PracticeDays.ElementAt(5).DailyHours.First().EndTime);

                            pmodel.OfficeHour_Saturday3 = pmodel.OfficeHour_StartSaturday3 + " - " + pmodel.OfficeHour_EndSaturday3;

                            pmodel.OfficeHour_StartSunday3 = ConvertTimeFormat(practiceLocation[practiceLocationCount - 3].OfficeHour.PracticeDays.ElementAt(6).DailyHours.First().StartTime);
                            pmodel.OfficeHour_EndSunday3 = ConvertTimeFormat(practiceLocation[practiceLocationCount - 3].OfficeHour.PracticeDays.ElementAt(6).DailyHours.First().EndTime);

                            pmodel.OfficeHour_Sunday3 = pmodel.OfficeHour_StartSunday3 + " - " + pmodel.OfficeHour_EndSunday3;
                        }
                    }

                    #endregion
                }

                #endregion
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
                    if (program.ProgramType == "Internship" && program.Preference==AHC.CD.Entities.MasterData.Enums.PreferenceType.Primary.ToString())
                    {
                        if (program.SchoolInformation != null)
                        {
                            pmodel.InternshipFacility = program.SchoolInformation.SchoolName;
                            pmodel.InternshipFacility_City = program.SchoolInformation.City;
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
                    else if (program.ProgramType == "Resident" && program.Preference == AHC.CD.Entities.MasterData.Enums.PreferenceType.Primary.ToString())
                    {
                        if (residencyCount == 0 && program.SchoolInformation != null)
                        {
                            pmodel.ResidencyFacility = program.SchoolInformation.SchoolName;
                            pmodel.ResidencyFacility_City = program.SchoolInformation.City;
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
                    else if (program.ProgramType == "Fellowship" && program.Preference == AHC.CD.Entities.MasterData.Enums.PreferenceType.Primary.ToString())
                    {
                        if (program.SchoolInformation != null)
                        {
                            pmodel.FellowshipFacility = program.SchoolInformation.SchoolName;
                            pmodel.FellowshipFacility_City = program.SchoolInformation.City;
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
            if(profile.CMECertifications != null && profile.CMECertifications.Count != 0)
            {
                var cmeCertificationCount = profile.CMECertifications.Count;
                pmodel.CME_CertificateName1 = profile.CMECertifications.ElementAt(cmeCertificationCount - 1).Certification;
                if(profile.CMECertifications.ElementAt(cmeCertificationCount - 1).SchoolInformation != null)
                {
                    pmodel.CME_SchoolName = profile.CMECertifications.ElementAt(cmeCertificationCount - 1).SchoolInformation.SchoolName;
                    pmodel.CME_MailingAddress = profile.CMECertifications.ElementAt(cmeCertificationCount - 1).SchoolInformation.Email;
                    pmodel.CME_City1 = profile.CMECertifications.ElementAt(cmeCertificationCount - 1).SchoolInformation.City;
                    pmodel.CME_State1 = profile.CMECertifications.ElementAt(cmeCertificationCount - 1).SchoolInformation.State;
                    pmodel.CME_ZipCode1 = profile.CMECertifications.ElementAt(cmeCertificationCount - 1).SchoolInformation.ZipCode;
                    pmodel.CME_Country1 = profile.CMECertifications.ElementAt(cmeCertificationCount - 1).SchoolInformation.Country;
                }
                pmodel.CME_StartDate1 = ConvertToDateString(profile.CMECertifications.ElementAt(cmeCertificationCount - 1).StartDate);
                pmodel.CME_EndDate1 = ConvertToDateString(profile.CMECertifications.ElementAt(cmeCertificationCount - 1).EndDate);
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
                        if (primaryHospitalInfo.HospitalContactInfo != null) {
                            pmodel.HospitalPrivilege_PrimaryCity = primaryHospitalInfo.HospitalContactInfo.City;
                        }
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
                    if (secondaryHospitalInfo.Count > 1)
                    {
                        if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2) != null && secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).Hospital != null)
                        {
                            pmodel.HospitalPrivilege_HospitalName2 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).Hospital.HospitalName;
                            if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).HospitalContactInfo != null)
                            {
                                pmodel.HospitalPrivilege_City2 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).HospitalContactInfo.City;
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
                            if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).HospitalContactInfo != null)
                            {
                                pmodel.HospitalPrivilege_PrimaryCity = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 1).HospitalContactInfo.City;
                            }
                        }
                    }
                    if (secondaryHospitalInfo.Count > 1)
                    {
                        if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2) != null && secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).Hospital != null)
                        {
                            pmodel.HospitalPrivilege_HospitalName1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).Hospital.HospitalName;
                            if (secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).HospitalContactInfo != null)
                            {
                                pmodel.HospitalPrivilege_City1 = secondaryHospitalInfo.ElementAt(secondaryHospitalCount - 2).HospitalContactInfo.City;
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
                            }
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

                if (professionalAffiliations.ElementAt(professionalAffiliationsCount - 1) != null)
                {
                    pmodel.ProviderOrganizationName1 = professionalAffiliations.ElementAt(professionalAffiliationsCount - 1).OrganizationName;
                    pmodel.ProfessionalAffiliationOfficePosition1 = professionalAffiliations.ElementAt(professionalAffiliationsCount - 1).PositionOfficeHeld;
                    pmodel.ProfessionalAffiliationMember1 = professionalAffiliations.ElementAt(professionalAffiliationsCount - 1).Member;
                    pmodel.ProfessionalAffiliationStartDate1 = ConvertToDateString(professionalAffiliations.ElementAt(professionalAffiliationsCount - 1).StartDate);
                    pmodel.ProfessionalAffiliationEndDate1 = ConvertToDateString(professionalAffiliations.ElementAt(professionalAffiliationsCount - 1).EndDate);
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
            if(profile.ProfessionalWorkExperiences != null && profile.ProfessionalWorkExperiences.Count > 0)
            {
                var professionalworkexperienceCount = profile.ProfessionalWorkExperiences.Count;
                int workexperienceCount = 0;
                foreach (var item in profile.ProfessionalWorkExperiences)
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
                    else if(workexperienceCount == 2)
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
                    else if(workexperienceCount == 3)
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

            string pdfFileName = readXml(pmodel, templateName, CDUserId);

            return pdfFileName;
        }       



        public string CombinePdfs(int profileID, List<string> pdflist, string UserAuthId, string name)
        {
            profileId = profileID;
            PersonalDetail personalInfo = GetProviderData(profileID);
            string CDUserId = GetCredentialingUserId(UserAuthId).ToString();
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
            SaveGeneratedPackage(profileID, "Generated Package" + "_Date" + date + "_Hour" + timeHour + "_Min" + timeMin, "\\ApplicationDocument\\GeneratedPackagePdf\\" + outputFileName, CDUserId);
            AddIntoPackageGenerationTracker(profileID, "\\ApplicationDocument\\GeneratedPackagePdf\\" + outputFileName, CDUserId, name);
            return outputFileName;

        }

        public List<PackageGeneratorBusinessModel> GenerateBulkPackage(List<int> ProfileIDs, List<string> GenricList, string UserAuthId,string name)
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
                         
                            string filePath = "\\ApplicationDocument\\GeneratedTemplatePdf\\"+ await GeneratePlanFormPDF(id, template, UserAuthId);
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

            if (time != "Day Off" && time!="Not Available")
            {
                hour = time.Split(':')[0];
                min = time.Split(':')[1];
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
                var pp=uow.GetGenericRepository<PlanForm>();
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
                SaveGeneratedXml(planForm.PlanFormID,planForm.PlanFormXmlPath);
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
            catch (Exception ex)
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
