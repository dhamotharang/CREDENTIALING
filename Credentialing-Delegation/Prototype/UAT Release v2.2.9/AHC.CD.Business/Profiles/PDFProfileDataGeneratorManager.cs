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

namespace AHC.CD.Business.Profiles
{
    internal class PDFProfileDataGeneratorManager : IPDFProfileDataGeneratorManager
    {

        private IProfileRepository profileRepository = null;
        private IUnitOfWork uow = null;
        
        public PDFProfileDataGeneratorManager(IUnitOfWork uow)
        {
            this.profileRepository = uow.GetProfileRepository();
            this.uow = uow;
        }

        public async Task<string> GetProfileDataByIdAsync(int profileId)
        {
            try
            {
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

                var profile = await profileRepository.FindAsync(p => p.ProfileID == profileId, includeProperties);

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
                    profile.ContractInfoes = profile.ContractInfoes.Where(c => c.ContractStatus != null && !c.ContractStatus.Equals(ContractStatus.Inactive.ToString())).ToList();

                if (profile.FederalDEAInformations.Count > 0)
                    profile.FederalDEAInformations = profile.FederalDEAInformations.Where(c => c.Status != null && !c.Status.Equals(StatusType.Inactive.ToString())).ToList();

                List<string> pdfList = new List<string>();

                string primaryPDFName = ConstructProfilePrimaryData(profile);
                pdfList.Add(primaryPDFName);

                List<string> suplementProfessionalPDF = ConstructSuplementProfessionaIDs(profile);
                foreach (var ID in suplementProfessionalPDF)
                {
                    pdfList.Add(ID);
                }


                List<string> trainingPDF = ConstructTrainingPDF(profile);
                foreach (var training in trainingPDF)
                {
                    pdfList.Add(training);
                }


                List<string> specialtyPDF = ConstructSpecialtyPDF(profile);
                foreach (var specialty in specialtyPDF)
                {
                    pdfList.Add(specialty);
                }
                

                List<string> hospitalPDF = ConstructHospitalPDF(profile);
                foreach (var hospital in hospitalPDF)
                {
                    pdfList.Add(hospital);
                }
                

                List<string> liabilityPDF = ConvertProfessionalLiabilityPDF(profile);
                foreach (var liability in liabilityPDF)
                {
                    pdfList.Add(liability);
                }
               

                List<string> historyPDF = ConstructWorkHistoryPDF(profile);
                foreach (var history in historyPDF)
                {
                    pdfList.Add(history);
                }


                List<string> colleaguePrimaryPDF = ConstructPrimaryCoveringColleaguePDF(profile);
                foreach (var colleague in colleaguePrimaryPDF)
                {
                    pdfList.Add(colleague);
                }                
                

                List<string> questionPDF = ConstructDisclosurePDF(profile);
                foreach (var question in questionPDF)
                {
                    pdfList.Add(question);
                }
                

                List<string> practiceInfoPDF = ConstructPracticeInfoPDF(profile);
                foreach (var practice in practiceInfoPDF)
                {
                    pdfList.Add(practice);
                }

                List<string> colleagueSecondaryPDF = ConstructSecondaryCoveringColleaguePDF(profile);
                foreach (var colleague in colleagueSecondaryPDF)
                {
                    pdfList.Add(colleague);
                }
                

                string date = DateTime.Now.ToString("MM-dd-yyyy");

                string fileName = profile.PersonalDetail.FirstName + "_" + date + ".pdf";

                GeneratePdf pdf = new GeneratePdf();
                primaryPDFName = pdf.CombinePdfs(pdfList, fileName);

                return primaryPDFName;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new PDFProfileDataGeneratorManagerException(ExceptionMessage.PROFILE_BY_ID_GET_EXCEPTION, ex);
            }
        }

        private string ConstructProfilePrimaryData(Profile profile)
        {
            string pdfName = "";            
            PDFProfileDataBusinessModel profileData = new PDFProfileDataBusinessModel();

            try
            {

                #region Personal Information and Professional IDs

                if (profile.PersonalDetail != null && profile.PersonalDetail.ProviderTitles.Count > 0)
                {
                    profileData.PersonalInfoProviderType = profile.PersonalDetail.ProviderTitles.FirstOrDefault(p => p.Status == AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString()).ProviderType.Title;
                }


                #region Name

                if (profile.PersonalDetail != null)
                {
                    profileData.PersonalInfoFirstName = profile.PersonalDetail.FirstName;
                    profileData.PersonalInfoLastName = profile.PersonalDetail.LastName;
                    profileData.PersonalInfoMiddleName = profile.PersonalDetail.MiddleName;
                    profileData.PersonalInfoGender = profile.PersonalDetail.Gender;
                    profileData.PersonalInfoSuffix = profile.PersonalDetail.Suffix;

                }

                if (profile.OtherLegalNames.Count > 0)
                {
                    profileData.PersonalInfoAnotherNameExist = "YES";
                    profileData.PersonalInfoOtherFirstName = profile.OtherLegalNames.First().OtherFirstName;
                    profileData.PersonalInfoOtherMiddleName = profile.OtherLegalNames.First().OtherMiddleName;
                    profileData.PersonalInfoOtherLastName = profile.OtherLegalNames.First().OtherLastName;
                   
                    profileData.PersonalInfoDateStartOfOtherName = ConvertToDateString(profile.OtherLegalNames.First().StartDate);
                    profileData.PersonalInfoDateEndOfOtherName = ConvertToDateString(profile.OtherLegalNames.First().EndDate);
                    profileData.PersonalInfoOtherSuffix = profile.OtherLegalNames.First().Suffix;
                }
                else
                {
                    profileData.PersonalInfoAnotherNameExist = "NO";
                }


                #endregion

                #region General Information

                if (profile.BirthInformation != null)
                {

                    DateTime birth = Convert.ToDateTime(profile.BirthInformation.DateOfBirth);
                    profileData.PersonalInfoDateOfBirth = birth.ToShortDateString();
                    profileData.PersonalInfoCityOfBirth = profile.BirthInformation.CityOfBirth;
                    profileData.PersonalInfoStateOfBirth = profile.BirthInformation.StateOfBirth;
                    profileData.PersonalInfoCountryOfBirth = profile.BirthInformation.CountryOfBirth;

                }

                if (profile.PersonalIdentification != null)
                {
                    profileData.PersonalInfoSSN = profile.PersonalIdentification.SSN;
                }



                if (profile.LanguageInfo != null && profile.LanguageInfo.KnownLanguages.Count > 0)
                {
                    if (profile.LanguageInfo.KnownLanguages.Count > 0)
                        profileData.PersonalInfoNonEnglishLanguage1 = profile.LanguageInfo.KnownLanguages.First().Language;

                    if (profile.LanguageInfo.KnownLanguages.Count > 1)
                        profileData.PersonalInfoNonEnglishLanguage2 = profile.LanguageInfo.KnownLanguages.ElementAt(1).Language;

                    if (profile.LanguageInfo.KnownLanguages.Count > 2)
                        profileData.PersonalInfoNonEnglishLanguage3 = profile.LanguageInfo.KnownLanguages.ElementAt(2).Language;

                    if (profile.LanguageInfo.KnownLanguages.Count > 3)
                        profileData.PersonalInfoNonEnglishLanguage4 = profile.LanguageInfo.KnownLanguages.ElementAt(3).Language;

                    if (profile.LanguageInfo.KnownLanguages.Count > 4)
                        profileData.PersonalInfoNonEnglishLanguage5 = profile.LanguageInfo.KnownLanguages.ElementAt(4).Language;

                }

                #endregion

                #region Home Address

                if (profile.HomeAddresses.Count > 0)
                {
                    profileData.PersonalInfoAptNumber = profile.HomeAddresses.First().UnitNumber;
                    profileData.PersonalInfoStreet = profile.HomeAddresses.First().Street;
                    profileData.PersonalInfoCity = profile.HomeAddresses.First().City;
                    profileData.PersonalInfoState = profile.HomeAddresses.First().State;
                    profileData.PersonalInfoZipCode = profile.HomeAddresses.First().ZipCode;

                }

                if (profile.ContactDetail != null)
                {

                    if (profile.ContactDetail.PreferredContacts.Count > 0)
                    {
                        profileData.PersonalInfoPreferredMethodOfContact = profile.ContactDetail.PreferredContacts.First().ContactType;

                    }

                    if (profile.ContactDetail.PhoneDetails.Count > 0)
                    {
                        profileData.PersonalInfoTelephone = profile.ContactDetail.PhoneDetails.First().PhoneNumber;                        

                    }

                    if (profile.ContactDetail.EmailIDs.Count > 0)
                    {
                        profileData.PersonalInfoEmail = profile.ContactDetail.EmailIDs.First().EmailAddress;
                    }
                }

                #endregion

                #region Professional IDs

                #region DEA

                if (profile.FederalDEAInformations.Count > 0)
                {
                    profileData.ProfessionalIDsFederalDEANumber = profile.FederalDEAInformations.First().DEANumber;
                    profileData.ProfessionalIDsFederalDEAIssueDate = ConvertToDateString(profile.FederalDEAInformations.First().IssueDate);
                    profileData.ProfessionalIDsFederalDEAStateOfReg = profile.FederalDEAInformations.First().StateOfReg;
                    profileData.ProfessionalIDsFederalDEAExpirationDate = ConvertToDateString(profile.FederalDEAInformations.First().ExpiryDate);
                }


                #endregion

                #region CDS

                if (profile.CDSCInformations.Count > 0)
                {
                    profileData.ProfessionalIDsCDSCertificateNumber = profile.CDSCInformations.First().CertNumber;
                    profileData.ProfessionalIDsCDSIssueDate = ConvertToDateString(profile.CDSCInformations.First().IssueDate);
                    profileData.ProfessionalIDsCDSStateOfReg = profile.CDSCInformations.First().State;
                    profileData.ProfessionalIDsCDSExpirationDate = ConvertToDateString(profile.CDSCInformations.First().ExpiryDate);
                }


                #endregion

                #region State License1

                if (profile.StateLicenses.Count > 0)
                {
                    profileData.ProfessionalIDsStateLicenseNumber1 = profile.StateLicenses.First().LicenseNumber;
                    profileData.ProfessionalIDsStateLicenseIssuingState1 = profile.StateLicenses.First().IssueState;
                    profileData.ProfessionalIDsStateLicenseIssueDate1 = ConvertToDateString(profile.StateLicenses.First().IssueDate);
                    profileData.ProfessionalIDsStateLicenseAreYouPractisingInThisState1 = profile.StateLicenses.First().IsCurrentPracticeState;
                    profileData.ProfessionalIDsStateLicenseExpirationDate1 = ConvertToDateString(profile.StateLicenses.First().ExpiryDate);

                    if (profile.StateLicenses.First().StateLicenseStatus != null)
                    {
                        profileData.ProfessionalIDsStateLicenseStatusCode1 = profile.StateLicenses.First().StateLicenseStatus.Title;
                    }

                    if (profile.StateLicenses.First().ProviderType != null)
                    {
                        profileData.ProfessionalIDsStateLicenseType1 = profile.StateLicenses.First().ProviderType.Title;
                    }

                }

                #endregion

                #region State License2

                if (profile.StateLicenses.Count > 1)
                {
                    profileData.ProfessionalIDsStateLicenseNumber2 = profile.StateLicenses.ElementAt(1).LicenseNumber;
                    profileData.ProfessionalIDsStateLicenseIssuingState2 = profile.StateLicenses.ElementAt(1).IssueState;
                    profileData.ProfessionalIDsStateLicenseIssueDate2 = ConvertToDateString(profile.StateLicenses.ElementAt(1).IssueDate);
                    profileData.ProfessionalIDsStateLicenseAreYouPractisingInThisState2 = profile.StateLicenses.ElementAt(1).IsCurrentPracticeState;
                    profileData.ProfessionalIDsStateLicenseExpirationDate2 = ConvertToDateString(profile.StateLicenses.ElementAt(1).ExpiryDate);

                    if (profile.StateLicenses.First().StateLicenseStatus != null)
                    {
                        profileData.ProfessionalIDsStateLicenseStatusCode2 = profile.StateLicenses.ElementAt(1).StateLicenseStatus.Title;
                    }

                    if (profile.StateLicenses.First().ProviderType != null)
                    {
                        profileData.ProfessionalIDsStateLicenseType2 = profile.StateLicenses.ElementAt(1).ProviderType.Title;
                    }

                }

                #endregion

                #endregion

                #region Other ID Numbers

                if (profile.MedicareInformations.Count > 0)
                {
                    profileData.OtherIDNumbersMedicareNumber = profile.MedicareInformations.First().LicenseNumber;
                    profileData.OtherIDNumbersMedicareApproved = "YES";
                }

                if (profile.MedicaidInformations.Count > 0)
                {
                    profileData.OtherIDNumbersMedicaidNumber = profile.MedicaidInformations.First().LicenseNumber;
                    profileData.OtherIDNumbersMedicaidApproved = "YES";
                    profileData.OtherIDNumbersMedicaidState = profile.MedicaidInformations.First().State;
                }

                if (profile.OtherIdentificationNumber != null)
                {
                    profileData.OtherIDNumbersUPIN = profile.OtherIdentificationNumber.UPINNumber;
                    profileData.OtherIDNumbersNPINumber = profile.OtherIdentificationNumber.NPINumber;
                    profileData.OtherIDNumbersUSMLENumber = profile.OtherIdentificationNumber.USMLENumber;
                }

                if (profile.ECFMGDetail != null)
                {
                    profileData.OtherIDNumbersECFMGNumber = profile.ECFMGDetail.ECFMGNumber;
                    profileData.OtherIDNumbersECFMGIssueDate = ConvertToDateString(profile.ECFMGDetail.ECFMGIssueDate);
                }

                if (profile.PracticeLocationDetails.Count > 0)
                {
                    //Worker Compensation Information
                    if (profile.PracticeLocationDetails.First().WorkersCompensationInformation != null)
                    {
                        profileData.OtherIDNumbersWorkersCompensationNumber = profile.PracticeLocationDetails.First().WorkersCompensationInformation.WorkersCompensationNumber;
                    }
                }

                #endregion

                #endregion

                #region Education & Training

                #region UnderGraduate School

                foreach (var item in profile.EducationDetails)
                {
                    if (item != null && item.QualificationType == AHC.CD.Entities.MasterData.Enums.EducationQualificationType.UnderGraduate.ToString())
                    {
                        if (item.SchoolInformation != null)
                        {
                            profileData.UnderGraduateSchoolOfficialName = item.SchoolInformation.SchoolName;
                            profileData.UnderGraduateSchoolAddress = item.SchoolInformation.Building + ", " + item.SchoolInformation.Street;
                            profileData.UnderGraduateSchoolCity = item.SchoolInformation.City;
                            profileData.UnderGraduateSchoolState = item.SchoolInformation.State;
                            profileData.UnderGraduateSchoolZip = item.SchoolInformation.ZipCode;
                            profileData.UnderGraduateSchoolCountryCode = item.SchoolInformation.Country;
                            profileData.UnderGraduateSchoolTelephone = item.SchoolInformation.PhoneNumber;
                            profileData.UnderGraduateSchoolFax = item.SchoolInformation.FaxNumber;
                        }

                        profileData.UnderGraduateSchoolStartDate = ConvertToDateString(item.StartDate);
                        profileData.UnderGraduateSchoolEndDate = ConvertToDateString(item.EndDate);
                        profileData.UnderGraduateSchoolDegreeAwarded = item.QualificationDegree;
                        profileData.UnderGraduateSchoolCompleteUnderGraduation = item.IsCompleted;
                        break;

                    }

                }

                #endregion

                #region Professional School

                foreach (var item in profile.EducationDetails)
                {
                    if (item != null && item.QualificationType == AHC.CD.Entities.MasterData.Enums.EducationQualificationType.Graduate.ToString())
                    {
                        profileData.ProfessionalSchoolGraduateType = item.GraduateType.ToString();

                        if (item.IsUSGraduate == AHC.CD.Entities.MasterData.Enums.YesNoOption.YES.ToString())
                        {
                            if (item.SchoolInformation != null)
                            {
                                profileData.USorCanadianSchoolName = item.SchoolInformation.SchoolName;
                            }
                            profileData.USorCanadianSchoolStartDate = item.StartDate;
                            profileData.USorCanadianSchoolEndDate = item.EndDate;
                            profileData.USorCanadianSchoolDegreeAwarded = item.QualificationDegree;
                            profileData.USorCanadianSchoolCompleteGraduation = item.IsCompleted;
                            break;
                        }
                        else
                        {
                            if (item.SchoolInformation != null)
                            {
                                profileData.NonUSorCanadianSchoolOfficialName = item.SchoolInformation.SchoolName;
                                profileData.NonUSorCanadianSchoolAddress = item.SchoolInformation.Building + ", " + item.SchoolInformation.Street;
                                profileData.NonUSorCanadianSchoolCity = item.SchoolInformation.City;
                                profileData.NonUSorCanadianSchoolCountryCode = item.SchoolInformation.Country;
                                profileData.NonUSorCanadianSchoolPostalCode = item.SchoolInformation.ZipCode;
                            }
                            profileData.NonUSorCanadianSchoolStartDate = ConvertToDateString(item.StartDate);
                            profileData.NonUSorCanadianSchoolEndDate = ConvertToDateString(item.EndDate);
                            profileData.NonUSorCanadianSchoolDegreeAwarded = item.QualificationDegree;
                            profileData.NonUSorCanadianSchoolCompleteUnderGraduation = item.IsCompleted;
                            break;
                        }

                    }

                }



                #endregion

                #region Training

                if (profile.ProgramDetails.Count > 0)
                {
                    if (profile.ProgramDetails.First().SchoolInformation != null)
                    {
                        profileData.TrainingSchoolCode = profile.ProgramDetails.First().SchoolInformation.SchoolName;
                        profileData.TrainingStreet = profile.ProgramDetails.First().SchoolInformation.Street;
                        profileData.TrainingSuiteBuilding = profile.ProgramDetails.First().SchoolInformation.Building;
                        profileData.TrainingCity = profile.ProgramDetails.First().SchoolInformation.City;
                        profileData.TrainingState = profile.ProgramDetails.First().SchoolInformation.State;
                        profileData.TrainingZip = profile.ProgramDetails.First().SchoolInformation.ZipCode;
                        profileData.TrainingCountryCode = profile.ProgramDetails.First().SchoolInformation.Country;
                        profileData.TrainingTelephone = profile.ProgramDetails.First().SchoolInformation.PhoneNumber;
                        profileData.TrainingFax = profile.ProgramDetails.First().SchoolInformation.FaxNumber;
                    }

                    profileData.TrainingInstitutionOrHospitalName = profile.ProgramDetails.First().HospitalName;
                    profileData.TrainingCompleteInSchool = profile.ProgramDetails.First().IsCompleted;
                    profileData.TrainingIfNotCompleteExplain = profile.ProgramDetails.First().InCompleteReason;
                }


                #region Internship/Residency1

                if (profile.ProgramDetails.Count > 0)
                {
                    profileData.Type1 = profile.ProgramDetails.First().ResidencyInternshipProgramType.ToString();
                    profileData.StartDate1 = ConvertToDateString(profile.ProgramDetails.First().StartDate);
                    profileData.EndDate1 = ConvertToDateString(profile.ProgramDetails.First().EndDate);
                    if (profile.ProgramDetails.First().Specialty != null)
                    {
                        profileData.DepartmentSpecialty1 = profile.ProgramDetails.First().Specialty.Name;
                    }
                    
                    profileData.NameOfDirector1 = profile.ProgramDetails.First().DirectorName;
                }


                #endregion


                #endregion

                #endregion

                #region Professional / Medical Specialty Information

                #region Primary Specialty

                foreach (var item in profile.SpecialtyDetails)
                {
                    if (item != null && item.SpecialtyPreference == AHC.CD.Entities.MasterData.Enums.PreferenceType.Primary.ToString())
                    {
                        profileData.PrimarySpecialtyCode = item.Specialty.Name;

                        if (item.SpecialtyBoardCertifiedDetail != null)
                        {
                            profileData.PrimarySpecialtyInitialCertificationDate = ConvertToDateString(item.SpecialtyBoardCertifiedDetail.InitialCertificationDate);
                            profileData.PrimarySpecialtyReCertificationDate = ConvertToDateString(item.SpecialtyBoardCertifiedDetail.LastReCerificationDate);
                            profileData.PrimarySpecialtyExpirationDate = ConvertToDateString(item.SpecialtyBoardCertifiedDetail.ExpirationDate);
                            profileData.PrimarySpecialtyCertifyingBoardCode = item.SpecialtyBoardCertifiedDetail.SpecialtyBoard.Name;
                        }

                        if (item.SpecialtyBoardNotCertifiedDetail != null)
                        {
                            profileData.PrimarySpecialtyExamStatus = item.SpecialtyBoardNotCertifiedDetail.SpecialtyBoardExamStatus.ToString();
                            profileData.PrimarySpecialtyReasonForNotTakingExam = item.SpecialtyBoardNotCertifiedDetail.ReasonForNotTakingExam;
                        }
                        profileData.PrimarySpecialtyBoardCertified = item.IsBoardCertified;
                        profileData.PrimarySpecialtyHMO = item.ListedInHMO.ToString();
                        profileData.PrimarySpecialtyPPO = item.ListedInPPO.ToString();
                        profileData.PrimarySpecialtyPOS = item.ListedInPOS.ToString();

                        break;
                    }

                }

                #endregion

                #region Secondary Specialty

                foreach (var item in profile.SpecialtyDetails)
                {
                    if (item != null && item.SpecialtyPreference == AHC.CD.Entities.MasterData.Enums.PreferenceType.Secondary.ToString())
                    {
                        profileData.SecondarySpecialtyCode = item.Specialty.Name;
                        if (item.SpecialtyBoardCertifiedDetail != null)
                        {
                            profileData.SecondarySpecialtyInitialCertificationDate = ConvertToDateString(item.SpecialtyBoardCertifiedDetail.InitialCertificationDate);
                            profileData.SecondarySpecialtyReCertificationDate = ConvertToDateString(item.SpecialtyBoardCertifiedDetail.LastReCerificationDate);
                            profileData.SecondarySpecialtyExpirationDate = ConvertToDateString(item.SpecialtyBoardCertifiedDetail.ExpirationDate);
                            profileData.SecondarySpecialtyCertifyingBoardCode = item.SpecialtyBoardCertifiedDetail.SpecialtyBoard.Name;
                        }

                        if (item.SpecialtyBoardNotCertifiedDetail != null)
                        {
                            profileData.SecondarySpecialtyExamStatus = item.SpecialtyBoardNotCertifiedDetail.SpecialtyBoardExamStatus.ToString();
                            profileData.SecondarySpecialtyReasonForNotTakingExam = item.SpecialtyBoardNotCertifiedDetail.ReasonForNotTakingExam;
                        }
                        profileData.SecondarySpecialtyBoardCertified = item.IsBoardCertified;
                        profileData.SecondarySpecialtyHMO = item.ListedInHMO.ToString();
                        profileData.SecondarySpecialtyPPO = item.ListedInPPO.ToString();
                        profileData.SecondarySpecialtyPOS = item.ListedInPOS.ToString();

                        break;
                    }

                }

                #endregion


                #region PractiseInterest

                if (profile.PracticeInterest != null)
                {
                    profileData.PractiseInterest = profile.PracticeInterest.Interest;
                }


                #endregion

                #region Primary Credentialing Contact


                #endregion

                #endregion

                #region Practise Location Information

                //Primary Practise Location

                foreach (var item in profile.PracticeLocationDetails)
                {
                    if (item != null && item.IsPrimary == AHC.CD.Entities.MasterData.Enums.YesNoOption.YES.ToString())
                    {
                        //primary credentialing contact
                        if (item.PrimaryCredentialingContactPerson != null)
                        {
                            profileData.PrimaryCredentialingContactLastName = item.PrimaryCredentialingContactPerson.LastName;
                            profileData.PrimaryCredentialingContactFirstName = item.PrimaryCredentialingContactPerson.FirstName;
                            profileData.PrimaryCredentialingContactStreet = item.PrimaryCredentialingContactPerson.Street;
                            profileData.PrimaryCredentialingContactSuiteBuilding = item.PrimaryCredentialingContactPerson.Building;
                            profileData.PrimaryCredentialingContactCity = item.PrimaryCredentialingContactPerson.City;
                            profileData.PrimaryCredentialingContactState = item.PrimaryCredentialingContactPerson.State;
                            profileData.PrimaryCredentialingContactZip = item.PrimaryCredentialingContactPerson.ZipCode;
                            profileData.PrimaryCredentialingContactTelephone = item.PrimaryCredentialingContactPerson.MobileNumber;
                            profileData.PrimaryCredentialingContactFax = item.PrimaryCredentialingContactPerson.FaxNumber;
                            profileData.PrimaryCredentialingContactEmail = item.PrimaryCredentialingContactPerson.EmailAddress;
                        }


                        profileData.CurrentlyPractisingAtThisAddress = item.CurrentlyPracticingAtThisAddress.ToString();
                        //profileData.PrimaryPractiseLocationStartDate = ConvertToDateString(item.StartDate);
                        profileData.PrimaryPractiseLocationPhysicianGroup = item.GroupName;
                        profileData.PrimaryPractiseLocationCorporateName = item.PracticeLocationCorporateName;

                        if (item.Facility != null)
                        {
                            profileData.PrimaryPractiseLocationStreet = item.Facility.Street;
                            profileData.PrimaryPractiseLocationSuiteBuilding = item.Facility.Building;
                            profileData.PrimaryPractiseLocationCity = item.Facility.City;
                            profileData.PrimaryPractiseLocationState = item.Facility.State;
                            profileData.PrimaryPractiseLocationZip = item.Facility.ZipCode;
                            profileData.PrimaryPractiseLocationTelephone = item.Facility.MobileNumber;
                            profileData.PrimaryPractiseLocationFax = item.Facility.FaxNumber;
                            profileData.PrimaryPractiseLocationOfficialEmail = item.Facility.EmailAddress;
                        }

                        profileData.PrimaryPractiseLocationSendGeneralCorrespondance = item.GeneralCorrespondenceYesNoOption.ToString();
                        profileData.PrimaryPractiseLocationPrimaryTaxId = item.PrimaryTaxId;

                        //Office Manager data
                        if (item.BusinessOfficeManagerOrStaff != null)
                        {
                            profileData.OfficeManagerLastName = item.BusinessOfficeManagerOrStaff.LastName;
                            profileData.OfficeManagerFirstName = item.BusinessOfficeManagerOrStaff.FirstName;
                            profileData.OfficeManagerTelephone = item.BusinessOfficeManagerOrStaff.MobileNumber;
                            profileData.OfficeManagerFax = item.BusinessOfficeManagerOrStaff.FaxNumber;
                            profileData.OfficeManagerEmail = item.BusinessOfficeManagerOrStaff.EmailAddress;
                        }

                        //Billing Contact data
                        if (item.BillingContactPerson != null)
                        {
                            profileData.BillingContactLastName = item.BillingContactPerson.LastName;
                            profileData.BillingContactFirstName = item.BillingContactPerson.FirstName;
                            profileData.BillingContactStreet = item.BillingContactPerson.Street;
                            profileData.BillingContactSuiteBuilding = item.BillingContactPerson.Building;
                            profileData.BillingContactCity = item.BillingContactPerson.City;
                            profileData.BillingContactState = item.BillingContactPerson.State;
                            profileData.BillingContactZip = item.BillingContactPerson.ZipCode;
                            profileData.BillingContactTelephone = item.BillingContactPerson.MobileNumber;
                            profileData.BillingContactFax = item.BillingContactPerson.FaxNumber;
                            profileData.BillingContactEmail = item.BillingContactPerson.EmailAddress;
                        }

                        //Payment Remittance data
                        if (item.PaymentAndRemittance != null)
                        {
                            profileData.PaymentRemittanceElectronicBillingCapabilities = item.PaymentAndRemittance.ElectronicBillingCapability;
                            profileData.PaymentRemittancBillingDepartment = item.PaymentAndRemittance.BillingDepartment;
                            profileData.PaymentRemittanceCheckPayableTo = item.PaymentAndRemittance.CheckPayableTo;

                            if (item.PaymentAndRemittance.PaymentAndRemittancePerson != null)
                            {
                                profileData.PaymentRemittanceLastName = item.PaymentAndRemittance.PaymentAndRemittancePerson.LastName;
                                profileData.PaymentRemittanceFirstName = item.PaymentAndRemittance.PaymentAndRemittancePerson.FirstName;
                                profileData.PaymentRemittanceStreet = item.PaymentAndRemittance.PaymentAndRemittancePerson.Street;
                                profileData.PaymentRemittanceSuiteBuilding = item.PaymentAndRemittance.PaymentAndRemittancePerson.Building;
                                profileData.PaymentRemittanceCity = item.PaymentAndRemittance.PaymentAndRemittancePerson.City;
                                profileData.PaymentRemittanceState = item.PaymentAndRemittance.PaymentAndRemittancePerson.State;
                                profileData.PaymentRemittanceZip = item.PaymentAndRemittance.PaymentAndRemittancePerson.ZipCode;
                                profileData.PaymentRemittanceTelephone = item.PaymentAndRemittance.PaymentAndRemittancePerson.MobileNumber;
                                profileData.PaymentRemittanceFax = item.PaymentAndRemittance.PaymentAndRemittancePerson.FaxNumber;
                                profileData.PaymentRemittanceEmail = item.PaymentAndRemittance.PaymentAndRemittancePerson.EmailAddress;
                            }

                        }

                        //OfficeHours data
                        if (item.OfficeHour != null)
                        {
                            profileData.PhoneCoverage = item.OfficeHour.AnyTimePhoneCoverage;
                            profileData.TypeOfAnsweringService = item.OfficeHour.AnsweringService;
                            profileData.AfterOfficeHoursOfficeTelephone = item.OfficeHour.AfterHoursTelephoneNumber;

                            if (item.OfficeHour.PracticeDays.Count > 0 && item.OfficeHour.PracticeDays.First().DailyHours.Count > 0)
                            {
                                profileData.StartMonday = item.OfficeHour.PracticeDays.ElementAt(0).DailyHours.First().StartTime;
                                profileData.EndMonday = item.OfficeHour.PracticeDays.ElementAt(0).DailyHours.First().EndTime;
                                profileData.StartTuesday = item.OfficeHour.PracticeDays.ElementAt(1).DailyHours.First().StartTime;
                                profileData.EndTuesday = item.OfficeHour.PracticeDays.ElementAt(1).DailyHours.First().EndTime;
                                profileData.StartWednesday = item.OfficeHour.PracticeDays.ElementAt(2).DailyHours.First().StartTime;
                                profileData.EndWednesday = item.OfficeHour.PracticeDays.ElementAt(2).DailyHours.First().EndTime;
                                profileData.StartThursday = item.OfficeHour.PracticeDays.ElementAt(3).DailyHours.First().StartTime;
                                profileData.EndThursday = item.OfficeHour.PracticeDays.ElementAt(3).DailyHours.First().EndTime;
                                profileData.StartFriday = item.OfficeHour.PracticeDays.ElementAt(4).DailyHours.First().StartTime;
                                profileData.EndFriday = item.OfficeHour.PracticeDays.ElementAt(4).DailyHours.First().EndTime;
                                profileData.StartSaturday = item.OfficeHour.PracticeDays.ElementAt(5).DailyHours.First().StartTime;
                                profileData.EndSaturday = item.OfficeHour.PracticeDays.ElementAt(5).DailyHours.First().EndTime;
                                profileData.StartSunday = item.OfficeHour.PracticeDays.ElementAt(6).DailyHours.First().StartTime;
                                profileData.EndSunday = item.OfficeHour.PracticeDays.ElementAt(6).DailyHours.First().EndTime;
                            }

                        }

                        //OpenPractiseStatus data
                        if (item.OpenPracticeStatus != null)
                        {

                            profileData.OpenPractiseStatusExplain = item.OpenPracticeStatus.AnyInformationVariesByPlan;
                            profileData.OpenPractiseStatusPRACTICELIMITATIONS = item.OpenPracticeStatus.AnyPracticeLimitation;
                            profileData.OpenPractiseStatusGENDERLIMITATIONS = item.OpenPracticeStatus.GenderLimitation;
                            profileData.OpenPractiseStatusMINIMUMAGELIMITATIONS = item.OpenPracticeStatus.MinimumAge;
                            profileData.OpenPractiseStatusMAXIMUMAGELIMITATIONS = item.OpenPracticeStatus.MaximumAge;
                            profileData.OpenPractiseStatusOtherLIMITATIONS = item.OpenPracticeStatus.OtherLimitations;

                            if (item.OpenPracticeStatus.PracticeQuestionAnswers.Count > 0)
                            {
                                profileData.OpenPractiseStatusACCEPTNEWPATIENTSINTOTHISPRACTICE = item.OpenPracticeStatus.PracticeQuestionAnswers.ElementAt(0).Answer;
                                profileData.OpenPractiseStatusACCEPTEXISTINGPATIENTSWITHCHANGEOFPAYOR = item.OpenPracticeStatus.PracticeQuestionAnswers.ElementAt(1).Answer;
                                profileData.OpenPractiseStatusACCEPTNEWPATIENTSWITHPHYSICIANREFERRAL = item.OpenPracticeStatus.PracticeQuestionAnswers.ElementAt(2).Answer;
                                profileData.OpenPractiseStatusACCEPTALLNEWPATIENTS = item.OpenPracticeStatus.PracticeQuestionAnswers.ElementAt(3).Answer;
                                profileData.OpenPractiseStatusACCEPTNEWMEDICAREPATIENTS = item.OpenPracticeStatus.PracticeQuestionAnswers.ElementAt(4).Answer;
                                profileData.OpenPractiseStatusACCEPTNEWMEDICAIDPATIENTS = item.OpenPracticeStatus.PracticeQuestionAnswers.ElementAt(5).Answer;
                            }

                        }

                        //Mid-Level Practitioner data                       
                        if (item.PracticeProviders.Count > 0)
                        {
                            var midLevels = item.PracticeProviders.Where(m => m.Practice == AHC.CD.Entities.MasterData.Enums.PracticeType.Midlevel.ToString() && m.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();
                            for (int i = 0; i < midLevels.Count; i++ )
                            {
                                if (midLevels.Count > i)
                                {
                                    profileData.MidLevelPractitionerLastName1 = midLevels.First().LastName;
                                    profileData.MidLevelPractitionerFirstName1 = midLevels.First().FirstName;
                                    
                                }
                                i++;
                                if (midLevels.Count > i)
                                {
                                    profileData.MidLevelPractitionerLastName2 = midLevels.ElementAt(i).LastName;
                                    profileData.MidLevelPractitionerFirstName2 = midLevels.ElementAt(i).FirstName;
                                    
                                }
                                i++;
                                if (midLevels.Count > i)
                                {
                                    profileData.MidLevelPractitionerLastName3 = midLevels.ElementAt(i).LastName;
                                    profileData.MidLevelPractitionerFirstName3 = midLevels.ElementAt(i).FirstName;
                                    
                                }
                                i++;
                                if (midLevels.Count > i)
                                {
                                    profileData.MidLevelPractitionerLastName4 = midLevels.ElementAt(i).LastName;
                                    profileData.MidLevelPractitionerFirstName4 = midLevels.ElementAt(i).FirstName;
                                    
                                }
                                i++;
                                if (midLevels.Count > i)
                                {
                                    profileData.MidLevelPractitionerLastName5 = midLevels.ElementAt(i).LastName;
                                    profileData.MidLevelPractitionerFirstName5 = midLevels.ElementAt(i).FirstName;
                                    break;
                                }

                            }

                        }


                        //Languages data                  

                        if (item.Facility != null && item.Facility.FacilityDetail != null && item.Facility.FacilityDetail.Language != null && item.Facility.FacilityDetail.Language.NonEnglishLanguages.Count > 0)
                        {
                            var activeLanguages = item.Facility.FacilityDetail.Language.NonEnglishLanguages.Where(l => l.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();

                            if (activeLanguages.Count > 0)
                                profileData.NonEnglishLanguage1 = item.Facility.FacilityDetail.Language.NonEnglishLanguages.First().Language;


                            if (activeLanguages.Count > 1)
                                profileData.NonEnglishLanguage2 = activeLanguages.ElementAt(1).Language;

                            if (activeLanguages.Count > 2)
                                profileData.NonEnglishLanguage3 = activeLanguages.ElementAt(2).Language;

                            if (activeLanguages.Count > 3)
                                profileData.NonEnglishLanguage4 = activeLanguages.ElementAt(3).Language;

                            if (activeLanguages.Count > 4)
                                profileData.NonEnglishLanguage5 = activeLanguages.ElementAt(4).Language;

                            if (activeLanguages.Count > 0 && activeLanguages.First().InterpretersAvailableYesNoOption.Equals(YesNoOption.YES))
                                profileData.LanguagesInterpreted1 = item.Facility.FacilityDetail.Language.NonEnglishLanguages.First().Language;

                            if (activeLanguages.Count > 1 && activeLanguages.ElementAt(1).InterpretersAvailableYesNoOption.Equals(YesNoOption.YES))
                                profileData.LanguagesInterpreted2 = item.Facility.FacilityDetail.Language.NonEnglishLanguages.ElementAt(1).Language;

                            if (activeLanguages.Count > 2 && activeLanguages.ElementAt(2).InterpretersAvailableYesNoOption.Equals(YesNoOption.YES))
                                profileData.LanguagesInterpreted3 = item.Facility.FacilityDetail.Language.NonEnglishLanguages.ElementAt(2).Language;

                            if (activeLanguages.Count > 3 && activeLanguages.ElementAt(3).InterpretersAvailableYesNoOption.Equals(YesNoOption.YES))
                                profileData.LanguagesInterpreted4 = item.Facility.FacilityDetail.Language.NonEnglishLanguages.ElementAt(3).Language;

                        }

                        //Accessibilities data
                        if (item.Facility != null && item.Facility.FacilityDetail != null && item.Facility.FacilityDetail.Accessibility != null)
                        {
                            if (item.Facility.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.Count > 0)
                            {
                                profileData.ADAREQUIREMENTS = item.Facility.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(0).Answer;
                                profileData.HANDICAPPEDACCESSForBuilding = item.Facility.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(1).Answer;
                                profileData.HANDICAPPEDACCESSForPARKING = item.Facility.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(2).Answer;
                                profileData.HANDICAPPEDACCESSForRESTROOM = item.Facility.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(3).Answer;
                                profileData.OTHERSERVICESForDISABLED = item.Facility.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(4).Answer;
                                profileData.TextTELEPHONY = item.Facility.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(5).Answer;
                                //profileData.AMERICANSIGNLANGUAGE = item.Facility.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(6).Answer;
                                profileData.MENTALPhysicalIMPAIRMENTSERVICES = item.Facility.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(6).Answer;
                                profileData.AccessibleByPublicTransport = item.Facility.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(7).Answer;
                                profileData.Bus = item.Facility.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(8).Answer;
                                profileData.Subway = item.Facility.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(9).Answer;
                                profileData.RegionalTrain = item.Facility.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(10).Answer;
                            }

                            if (item.Facility.FacilityDetail.Language != null)
                            {
                                profileData.AMERICANSIGNLANGUAGE = item.Facility.FacilityDetail.Language.AmericanSignLanguage;
                            }

                            profileData.OtherHANDICAPPEDAccess = item.Facility.FacilityDetail.Accessibility.OtherHandicappedAccess;
                            profileData.OtherDisabilityServices = item.Facility.FacilityDetail.Accessibility.OtherDisabilityServices;
                            profileData.OtherTransportationAccess = item.Facility.FacilityDetail.Accessibility.OtherTransportationAccess;

                        }

                        //Services data
                        if (item.Facility != null && item.Facility.FacilityDetail != null && item.Facility.FacilityDetail.Service != null)
                        {
                            profileData.LaboratoryServices = item.Facility.FacilityDetail.Service.LaboratoryServices;
                            profileData.LaboratoryServicesAccreditingProgram = item.Facility.FacilityDetail.Service.LaboratoryAccreditingCertifyingProgram;
                            profileData.RadiologyServices = item.Facility.FacilityDetail.Service.RadiologyServices;
                            profileData.RadiologyServicesXrayCertificationType = item.Facility.FacilityDetail.Service.RadiologyXRayCertificateType;
                            profileData.AnesthesiaAdministered = item.Facility.FacilityDetail.Service.IsAnesthesiaAdministered;
                            profileData.ClassCategory = item.Facility.FacilityDetail.Service.AnesthesiaCategory;
                            profileData.AdministerLastName = item.Facility.FacilityDetail.Service.AnesthesiaAdminLastName;
                            profileData.AdministerFirstName = item.Facility.FacilityDetail.Service.AnesthesiaAdminFirstName;
                            profileData.AdditionalOfficeProcedures = item.Facility.FacilityDetail.Service.AdditionalOfficeProcedures;

                            if (item.Facility.FacilityDetail.Service.PracticeType != null)
                            {
                                profileData.PractiseType = item.Facility.FacilityDetail.Service.PracticeType.Title;
                            }


                            if (item.Facility.FacilityDetail.Service.FacilityServiceQuestionAnswers.Count > 0)
                            {
                                var serviceCount = 0;

                                foreach (var service in item.Facility.FacilityDetail.Service.FacilityServiceQuestionAnswers)
                                {
                                    if (service != null && serviceCount < 1)
                                    {
                                        profileData.EKGS = item.Facility.FacilityDetail.Service.FacilityServiceQuestionAnswers.ElementAt(0).Answer;
                                        profileData.AllergyInjections = item.Facility.FacilityDetail.Service.FacilityServiceQuestionAnswers.ElementAt(1).Answer;
                                        profileData.AllergySkinTesting = item.Facility.FacilityDetail.Service.FacilityServiceQuestionAnswers.ElementAt(2).Answer;
                                        profileData.RoutineOfficeGynecology = item.Facility.FacilityDetail.Service.FacilityServiceQuestionAnswers.ElementAt(3).Answer;
                                        profileData.DrawingBlood = item.Facility.FacilityDetail.Service.FacilityServiceQuestionAnswers.ElementAt(4).Answer;
                                        profileData.AgeImmunizations = item.Facility.FacilityDetail.Service.FacilityServiceQuestionAnswers.ElementAt(5).Answer;
                                        profileData.FlexibleSIGMOIDOSCOPY = item.Facility.FacilityDetail.Service.FacilityServiceQuestionAnswers.ElementAt(6).Answer;
                                        profileData.TYMPANOMETRYAUDIOMETRYSCREENING = item.Facility.FacilityDetail.Service.FacilityServiceQuestionAnswers.ElementAt(7).Answer;
                                        profileData.ASTHMATreatment = item.Facility.FacilityDetail.Service.FacilityServiceQuestionAnswers.ElementAt(8).Answer;
                                        profileData.OSTEOPATHICManipulation = item.Facility.FacilityDetail.Service.FacilityServiceQuestionAnswers.ElementAt(9).Answer;
                                        profileData.IVHYDRATIONTREATMENT = item.Facility.FacilityDetail.Service.FacilityServiceQuestionAnswers.ElementAt(10).Answer;
                                        profileData.CARDIACSTRESSTEST = item.Facility.FacilityDetail.Service.FacilityServiceQuestionAnswers.ElementAt(11).Answer;
                                        profileData.PULMONARYFUNCTIONTESTING = item.Facility.FacilityDetail.Service.FacilityServiceQuestionAnswers.ElementAt(12).Answer;
                                        profileData.PhysicalTheraphy = item.Facility.FacilityDetail.Service.FacilityServiceQuestionAnswers.ElementAt(13).Answer;
                                        profileData.MinorLaseractions = item.Facility.FacilityDetail.Service.FacilityServiceQuestionAnswers.ElementAt(14).Answer;
                                        serviceCount = 1;
                                    }


                                }
                            }

                        }

                        //Covering colleagues data
                        if (item.PracticeProviders.Count > 0)
                        {

                            var colleagues = item.PracticeProviders.Where(p => p.Practice == AHC.CD.Entities.MasterData.Enums.PracticeType.CoveringColleague.ToString() && 
                                p.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();

                            for (int i = 0; i < colleagues.Count; i++)
                            {
                                if (colleagues.Count > i)
                                {

                                    profileData.CoveringColleguesLastName1 = colleagues.ElementAt(i).LastName;
                                    profileData.CoveringColleguesFirstName1 = colleagues.ElementAt(i).FirstName;
                                    if (colleagues.ElementAt(i).PracticeProviderSpecialties.Count > 0 && colleagues.ElementAt(i).PracticeProviderSpecialties.First().Specialty != null)
                                    {
                                        profileData.CoveringColleguesSpecialtyCode1 = colleagues.ElementAt(i).PracticeProviderSpecialties.First().Specialty.Name;
                                    }
                                    if (colleagues.ElementAt(i).PracticeProviderTypes.Count > 0 && colleagues.ElementAt(i).PracticeProviderTypes.First().ProviderType != null)
                                    {
                                        profileData.CoveringColleguesProviderType1 = colleagues.ElementAt(i).PracticeProviderTypes.First().ProviderType.Title;
                                    }
                                    
                                    

                                }
                                i++;
                                if (colleagues.Count > i)
                                {

                                    profileData.CoveringColleguesLastName2 = colleagues.ElementAt(i).LastName;
                                    profileData.CoveringColleguesFirstName2 = colleagues.ElementAt(i).FirstName;
                                    if (colleagues.ElementAt(i).PracticeProviderSpecialties.Count > 0 && colleagues.ElementAt(i).PracticeProviderSpecialties.First().Specialty != null)
                                    {
                                        profileData.CoveringColleguesSpecialtyCode2 = colleagues.ElementAt(i).PracticeProviderSpecialties.First().Specialty.Name;
                                    }
                                    if (colleagues.ElementAt(i).PracticeProviderTypes.Count > 0 && colleagues.ElementAt(i).PracticeProviderTypes.First().ProviderType != null)
                                    {
                                        profileData.CoveringColleguesProviderType2 = colleagues.ElementAt(i).PracticeProviderTypes.First().ProviderType.Title;
                                    }
                                   

                                }
                                i++;
                                if (colleagues.Count > i)
                                {
                                    profileData.CoveringColleguesLastName3 = colleagues.ElementAt(i).LastName;
                                    profileData.CoveringColleguesFirstName3 = colleagues.ElementAt(i).FirstName;
                                    if (colleagues.ElementAt(i).PracticeProviderSpecialties.Count > 0 && colleagues.ElementAt(i).PracticeProviderSpecialties.First().Specialty != null)
                                    {
                                        profileData.CoveringColleguesSpecialtyCode3 = colleagues.ElementAt(i).PracticeProviderSpecialties.First().Specialty.Name;
                                    }
                                    if (colleagues.ElementAt(i).PracticeProviderTypes.Count > 0 && colleagues.ElementAt(i).PracticeProviderTypes.First().ProviderType != null)
                                    {
                                        profileData.CoveringColleguesProviderType3 = colleagues.ElementAt(i).PracticeProviderTypes.First().ProviderType.Title;
                                    }
                                    
                                    break;
                                }

                            }

                        }
                    }
                }


                #endregion

                #region Hospital Affiliations

                //Admitting Arrangements
                if (profile.HospitalPrivilegeInformation != null)
                {
                    profileData.HaveHospitalPrivilege = profile.HospitalPrivilegeInformation.HasHospitalPrivilege;
                    profileData.AdmittingArrangementType = profile.HospitalPrivilegeInformation.OtherAdmittingArrangements;

                    var HospitalPrivilegeDetail = profile.HospitalPrivilegeInformation.HospitalPrivilegeDetails.FirstOrDefault(h => h.Preference == AHC.CD.Entities.MasterData.Enums.PreferenceType.Primary.ToString());
                    //Hospital Privileges data
                    if (HospitalPrivilegeDetail != null)
                    {
                        profileData.HospitalDepartmentName = HospitalPrivilegeDetail.DepartmentName;
                        profileData.HospitalDepartmentDirectorFirstName = HospitalPrivilegeDetail.DepartmentChief;
                        profileData.HospitalAffiliationStartDate = ConvertToDateString(HospitalPrivilegeDetail.AffilicationStartDate);
                        profileData.HospitalAffiliationEndDate = ConvertToDateString(HospitalPrivilegeDetail.AffiliationEndDate);
                        profileData.HospitalUnrestrictedPrivilege = HospitalPrivilegeDetail.FullUnrestrictedPrevilages;
                        profileData.HospitalPrivilegesTemporary = HospitalPrivilegeDetail.ArePrevilegesTemporary;
                        profileData.HospitalPercentageAnnualAdmission = HospitalPrivilegeDetail.AnnualAdmisionPercentage;

                        if (HospitalPrivilegeDetail.AdmittingPrivilege != null)
                        {
                            profileData.HospitalAdmittingPrivilegeStatus = HospitalPrivilegeDetail.AdmittingPrivilege.Title;
                        }

                        if (HospitalPrivilegeDetail.Hospital != null)
                        {
                            profileData.HospitalName = HospitalPrivilegeDetail.Hospital.HospitalName;

                            if (HospitalPrivilegeDetail.Hospital.HospitalContactInfoes.Count > 0)
                            {
                                profileData.HospitalNumber = HospitalPrivilegeDetail.Hospital.HospitalContactInfoes.First().UnitNumber;
                                profileData.HospitalStreet = HospitalPrivilegeDetail.Hospital.HospitalContactInfoes.First().Street;
                                profileData.HospitalCity = HospitalPrivilegeDetail.Hospital.HospitalContactInfoes.First().City;
                                profileData.HospitalState = HospitalPrivilegeDetail.Hospital.HospitalContactInfoes.First().State;
                                profileData.HospitalZip = HospitalPrivilegeDetail.Hospital.HospitalContactInfoes.First().ZipCode;
                                profileData.HospitalTelephone = HospitalPrivilegeDetail.Hospital.HospitalContactInfoes.First().Phone;
                                profileData.HospitalFax = HospitalPrivilegeDetail.Hospital.HospitalContactInfoes.First().Fax;
                            }

                        }


                    }

                    var SecondaryHospitalPrivilegeDetail = profile.HospitalPrivilegeInformation.HospitalPrivilegeDetails.Where(h => h.Preference == AHC.CD.Entities.MasterData.Enums.PreferenceType.Secondary.ToString()).ToList();

                    //Other Hospital Name
                    if (SecondaryHospitalPrivilegeDetail.Count > 0)
                    {
                        profileData.OtherHospitalDepartmentName = SecondaryHospitalPrivilegeDetail.ElementAt(0).DepartmentName;
                        profileData.OtherHospitalDepartmentDirectorFirstName = SecondaryHospitalPrivilegeDetail.ElementAt(0).DepartmentChief;
                        profileData.OtherHospitalAffiliationStartDate = ConvertToDateString(SecondaryHospitalPrivilegeDetail.ElementAt(0).AffilicationStartDate);
                        profileData.OtherHospitalAffiliationEndDate = ConvertToDateString(SecondaryHospitalPrivilegeDetail.ElementAt(0).AffiliationEndDate);
                        profileData.OtherHospitalUnrestrictedPrivilege = SecondaryHospitalPrivilegeDetail.ElementAt(0).FullUnrestrictedPrevilages;
                        profileData.OtherHospitalPrivilegesTemporary = SecondaryHospitalPrivilegeDetail.ElementAt(0).ArePrevilegesTemporary;
                        profileData.OtherHospitalPercentageAnnualAdmission = SecondaryHospitalPrivilegeDetail.ElementAt(0).AnnualAdmisionPercentage;

                        if (SecondaryHospitalPrivilegeDetail.ElementAt(0).AdmittingPrivilege != null)
                        {
                            profileData.OtherHospitalAdmittingPrivilegeStatus = SecondaryHospitalPrivilegeDetail.ElementAt(0).AdmittingPrivilege.Title;
                        }

                        if (SecondaryHospitalPrivilegeDetail.ElementAt(0).Hospital != null)
                        {
                            profileData.OtherHospitalName = SecondaryHospitalPrivilegeDetail.ElementAt(0).Hospital.HospitalName;

                            if (SecondaryHospitalPrivilegeDetail.ElementAt(0).Hospital.HospitalContactInfoes.Count > 0)
                            {
                                profileData.OtherHospitalNumber = SecondaryHospitalPrivilegeDetail.ElementAt(0).Hospital.HospitalContactInfoes.First().UnitNumber;
                                profileData.OtherHospitalStreet = SecondaryHospitalPrivilegeDetail.ElementAt(0).Hospital.HospitalContactInfoes.First().Street;
                                profileData.OtherHospitalCity = SecondaryHospitalPrivilegeDetail.ElementAt(0).Hospital.HospitalContactInfoes.First().City;
                                profileData.OtherHospitalState = SecondaryHospitalPrivilegeDetail.ElementAt(0).Hospital.HospitalContactInfoes.First().State;
                                profileData.OtherHospitalZip = SecondaryHospitalPrivilegeDetail.ElementAt(0).Hospital.HospitalContactInfoes.First().ZipCode;
                                profileData.OtherHospitalTelephone = SecondaryHospitalPrivilegeDetail.ElementAt(0).Hospital.HospitalContactInfoes.First().Phone;
                                profileData.OtherHospitalFax = SecondaryHospitalPrivilegeDetail.ElementAt(0).Hospital.HospitalContactInfoes.First().Fax;
                            }

                        }


                    }
                }

                #endregion

                #region Professional Liability Insurance Carrier1

                if (profile.ProfessionalLiabilityInfoes.Count > 0)
                {

                    if (profile.ProfessionalLiabilityInfoes.First().InsuranceCarrier != null)
                    {
                        profileData.ProfessionalLiabilityCarrierName1 = profile.ProfessionalLiabilityInfoes.First().InsuranceCarrier.Name;
                    }

                    if (profile.ProfessionalLiabilityInfoes.First().InsuranceCarrierAddress != null)
                    {
                        profileData.ProfessionalLiabilityStreet1 = profile.ProfessionalLiabilityInfoes.First().InsuranceCarrierAddress.Street;
                        profileData.ProfessionalLiabilitySuiteBuilding1 = profile.ProfessionalLiabilityInfoes.First().InsuranceCarrierAddress.Building;
                        profileData.ProfessionalLiabilityCity1 = profile.ProfessionalLiabilityInfoes.First().InsuranceCarrierAddress.City;
                        profileData.ProfessionalLiabilityState1 = profile.ProfessionalLiabilityInfoes.First().InsuranceCarrierAddress.State;
                        profileData.ProfessionalLiabilityZip1 = profile.ProfessionalLiabilityInfoes.First().InsuranceCarrierAddress.ZipCode;

                    }
                    profileData.ProfessionalLiabilityEffectiveDate1 = ConvertToDateString(profile.ProfessionalLiabilityInfoes.First().EffectiveDate);
                    profileData.ProfessionalLiabilityExpirationDate1 = ConvertToDateString(profile.ProfessionalLiabilityInfoes.First().ExpirationDate);
                    profileData.ProfessionalLiabilityCoverageType1 = profile.ProfessionalLiabilityInfoes.First().CoverageType;
                    profileData.ProfessionalLiabilityHAVEUNLIMITEDCOVERAGE1 = profile.ProfessionalLiabilityInfoes.First().UnlimitedCoverageWithInsuranceCarrier;
                    profileData.ProfessionalLiabilityTAILCOVERAGE1 = profile.ProfessionalLiabilityInfoes.First().PolicyIncludesTailCoverage;
                    profileData.AMOUNTOFCOVERAGEPEROCCURRENCE1 = profile.ProfessionalLiabilityInfoes.First().AmountOfCoveragePerOccurance;
                    profileData.AMOUNTOFCOVERAGEAGGREGATE1 = profile.ProfessionalLiabilityInfoes.First().AmountOfCoverageAggregate;
                    profileData.ProfessionalLiabilityOriginalEffectiveDate1 = ConvertToDateString(profile.ProfessionalLiabilityInfoes.First().OriginalEffectiveDate);
                    profileData.ProfessionalLiabilitySelfInsured1 = profile.ProfessionalLiabilityInfoes.First().SelfInsured;
                    profileData.POLICYNUMBER1 = profile.ProfessionalLiabilityInfoes.First().PolicyNumber;
                }



                #endregion

                #region Professional Liability Insurance Carrier2

                if (profile.ProfessionalLiabilityInfoes.Count > 1)
                {

                    if (profile.ProfessionalLiabilityInfoes.ElementAt(1).InsuranceCarrier != null)
                    {
                        profileData.ProfessionalLiabilityCarrierName2 = profile.ProfessionalLiabilityInfoes.ElementAt(1).InsuranceCarrier.Name;
                    }

                    if (profile.ProfessionalLiabilityInfoes.ElementAt(1).InsuranceCarrierAddress != null)
                    {
                        profileData.ProfessionalLiabilityStreet2 = profile.ProfessionalLiabilityInfoes.ElementAt(1).InsuranceCarrierAddress.Street;
                        profileData.ProfessionalLiabilitySuiteBuilding2 = profile.ProfessionalLiabilityInfoes.ElementAt(1).InsuranceCarrierAddress.Building;
                        profileData.ProfessionalLiabilityCity2 = profile.ProfessionalLiabilityInfoes.ElementAt(1).InsuranceCarrierAddress.City;
                        profileData.ProfessionalLiabilityState2 = profile.ProfessionalLiabilityInfoes.ElementAt(1).InsuranceCarrierAddress.State;
                        profileData.ProfessionalLiabilityZip2 = profile.ProfessionalLiabilityInfoes.ElementAt(1).InsuranceCarrierAddress.ZipCode;
                    }

                    profileData.ProfessionalLiabilitySelfInsured2 = profile.ProfessionalLiabilityInfoes.ElementAt(1).SelfInsured;
                    profileData.ProfessionalLiabilityOriginalEffectiveDate2 = ConvertToDateString(profile.ProfessionalLiabilityInfoes.ElementAt(1).OriginalEffectiveDate);
                    profileData.ProfessionalLiabilityEffectiveDate2 = ConvertToDateString(profile.ProfessionalLiabilityInfoes.ElementAt(1).EffectiveDate);
                    profileData.ProfessionalLiabilityExpirationDate2 = ConvertToDateString(profile.ProfessionalLiabilityInfoes.ElementAt(1).ExpirationDate);
                    profileData.ProfessionalLiabilityCoverageType2 = profile.ProfessionalLiabilityInfoes.ElementAt(1).CoverageType;
                    profileData.ProfessionalLiabilityHAVEUNLIMITEDCOVERAGE2 = profile.ProfessionalLiabilityInfoes.ElementAt(1).UnlimitedCoverageWithInsuranceCarrier;
                    profileData.ProfessionalLiabilityTAILCOVERAGE2 = profile.ProfessionalLiabilityInfoes.ElementAt(1).PolicyIncludesTailCoverage;
                    profileData.AMOUNTOFCOVERAGEPEROCCURRENCE2 = profile.ProfessionalLiabilityInfoes.ElementAt(1).AmountOfCoveragePerOccurance;
                    profileData.AMOUNTOFCOVERAGEAGGREGATE2 = profile.ProfessionalLiabilityInfoes.ElementAt(1).AmountOfCoverageAggregate;
                    profileData.POLICYNUMBER2 = profile.ProfessionalLiabilityInfoes.ElementAt(1).PolicyNumber;

                }



                #endregion

                #region Work History and Reference

                #region Military Duty

                //if (profile.MilitaryServiceInformations.Count > 0)
                //{                    
                //    profileData.ActiveMilitaryDuty = profile.MilitaryServiceInformations.First().MilitaryPresentDuty;
                //}


                #endregion

                #region Work History1

                if (profile.ProfessionalWorkExperiences.Count > 0)
                {
                    profileData.WorkHistoryPractiseName1 = profile.ProfessionalWorkExperiences.First().EmployerName;
                    profileData.WorkHistoryStreet1 = profile.ProfessionalWorkExperiences.First().Street;
                    profileData.WorkHistorySuiteBuilding1 = profile.ProfessionalWorkExperiences.First().Building;
                    profileData.WorkHistoryCity1 = profile.ProfessionalWorkExperiences.First().City;
                    profileData.WorkHistoryState1 = profile.ProfessionalWorkExperiences.First().State;
                    profileData.WorkHistoryZip1 = profile.ProfessionalWorkExperiences.First().ZipCode;
                    profileData.WorkHistoryTelephone1 = profile.ProfessionalWorkExperiences.First().MobileNumber;
                    profileData.WorkHistoryFax1 = profile.ProfessionalWorkExperiences.First().FaxNumber;
                    profileData.WorkHistoryCountryCode1 = profile.ProfessionalWorkExperiences.First().Country;
                    profileData.WorkHistoryStartDate1 = ConvertToDateString(profile.ProfessionalWorkExperiences.First().StartDate);
                    profileData.WorkHistoryEndDate1 = ConvertToDateString(profile.ProfessionalWorkExperiences.First().EndDate);
                    profileData.WorkHistoryReason1 = profile.ProfessionalWorkExperiences.First().DepartureReason;
                }


                #endregion

                #region Work History2

                if (profile.ProfessionalWorkExperiences.Count > 1)
                {
                    profileData.WorkHistoryPractiseName2 = profile.ProfessionalWorkExperiences.ElementAt(1).EmployerName;
                    profileData.WorkHistoryStreet2 = profile.ProfessionalWorkExperiences.ElementAt(1).Street;
                    profileData.WorkHistorySuiteBuilding2 = profile.ProfessionalWorkExperiences.ElementAt(1).Building;
                    profileData.WorkHistoryCity2 = profile.ProfessionalWorkExperiences.ElementAt(1).City;
                    profileData.WorkHistoryState2 = profile.ProfessionalWorkExperiences.ElementAt(1).State;
                    profileData.WorkHistoryZip2 = profile.ProfessionalWorkExperiences.ElementAt(1).ZipCode;
                    profileData.WorkHistoryTelephone2 = profile.ProfessionalWorkExperiences.ElementAt(1).MobileNumber;
                    profileData.WorkHistoryFax2 = profile.ProfessionalWorkExperiences.ElementAt(1).FaxNumber;
                    profileData.WorkHistoryCountryCode2 = profile.ProfessionalWorkExperiences.ElementAt(1).Country;
                    profileData.WorkHistoryStartDate2 = ConvertToDateString(profile.ProfessionalWorkExperiences.ElementAt(1).StartDate);
                    profileData.WorkHistoryEndDate2 = ConvertToDateString(profile.ProfessionalWorkExperiences.ElementAt(1).EndDate);
                    profileData.WorkHistoryReason2 = profile.ProfessionalWorkExperiences.ElementAt(1).DepartureReason;
                }



                #endregion

                #region Work History3

                if (profile.ProfessionalWorkExperiences.Count > 2)
                {
                    profileData.WorkHistoryPractiseName3 = profile.ProfessionalWorkExperiences.ElementAt(2).EmployerName;
                    profileData.WorkHistoryStreet3 = profile.ProfessionalWorkExperiences.ElementAt(2).Street;
                    profileData.WorkHistorySuiteBuilding3 = profile.ProfessionalWorkExperiences.ElementAt(2).Building;
                    profileData.WorkHistoryCity3 = profile.ProfessionalWorkExperiences.ElementAt(2).City;
                    profileData.WorkHistoryState3 = profile.ProfessionalWorkExperiences.ElementAt(2).State;
                    profileData.WorkHistoryZip3 = profile.ProfessionalWorkExperiences.ElementAt(2).ZipCode;
                    profileData.WorkHistoryTelephone3 = profile.ProfessionalWorkExperiences.ElementAt(2).MobileNumber;
                    profileData.WorkHistoryFax3 = profile.ProfessionalWorkExperiences.ElementAt(2).FaxNumber;
                    profileData.WorkHistoryCountryCode3 = profile.ProfessionalWorkExperiences.ElementAt(2).Country;
                    profileData.WorkHistoryStartDate3 = ConvertToDateString(profile.ProfessionalWorkExperiences.ElementAt(2).StartDate);
                    profileData.WorkHistoryEndDate3 = ConvertToDateString(profile.ProfessionalWorkExperiences.ElementAt(2).EndDate);
                    profileData.WorkHistoryReason3 = profile.ProfessionalWorkExperiences.ElementAt(2).DepartureReason;
                }

                #endregion

                #region Gaps in Professional / Work History

                if (profile.WorkGaps.Count > 0)
                {
                    profileData.GapStartDate = ConvertToDateString(profile.WorkGaps.First().StartDate);
                    profileData.GapEndDate = ConvertToDateString(profile.WorkGaps.First().EndDate);
                    profileData.GapReason = profile.WorkGaps.First().Description;
                }

                #endregion

                #endregion

                #region Professional References

                if (profile.ProfessionalReferenceInfos.Count > 0)
                {

                    if (profile.ProfessionalReferenceInfos.Count > 0)
                    {
                        profileData.ProfessionalReferenceLastName1 = profile.ProfessionalReferenceInfos.First().LastName;
                        profileData.ProfessionalReferenceFirstName1 = profile.ProfessionalReferenceInfos.First().FirstName;
                        profileData.ProfessionalReferenceStreet1 = profile.ProfessionalReferenceInfos.First().Street;
                        profileData.ProfessionalReferenceSuiteBuilding1 = profile.ProfessionalReferenceInfos.First().Building;
                        profileData.ProfessionalReferenceCity1 = profile.ProfessionalReferenceInfos.First().City;
                        profileData.ProfessionalReferenceState1 = profile.ProfessionalReferenceInfos.First().State;
                        profileData.ProfessionalReferenceZip1 = profile.ProfessionalReferenceInfos.First().Zipcode;
                        profileData.ProfessionalReferenceTelephone1 = profile.ProfessionalReferenceInfos.First().PhoneNumber;
                        profileData.ProfessionalReferenceFax1 = profile.ProfessionalReferenceInfos.First().FaxNumber;

                        if (profile.ProfessionalReferenceInfos.First().ProviderType != null)
                        {
                            profileData.ProfessionalReferenceProviderType1 = profile.ProfessionalReferenceInfos.First().ProviderType.Title;
                        }

                    }

                    if (profile.ProfessionalReferenceInfos.Count > 1)
                    {
                        profileData.ProfessionalReferenceLastName2 = profile.ProfessionalReferenceInfos.ElementAt(1).LastName;
                        profileData.ProfessionalReferenceFirstName2 = profile.ProfessionalReferenceInfos.ElementAt(1).FirstName;
                        profileData.ProfessionalReferenceStreet2 = profile.ProfessionalReferenceInfos.ElementAt(1).Street;
                        profileData.ProfessionalReferenceSuiteBuilding2 = profile.ProfessionalReferenceInfos.ElementAt(1).Building;
                        profileData.ProfessionalReferenceCity2 = profile.ProfessionalReferenceInfos.ElementAt(1).City;
                        profileData.ProfessionalReferenceState2 = profile.ProfessionalReferenceInfos.ElementAt(1).State;
                        profileData.ProfessionalReferenceZip2 = profile.ProfessionalReferenceInfos.ElementAt(1).Zipcode;
                        profileData.ProfessionalReferenceTelephone2 = profile.ProfessionalReferenceInfos.ElementAt(1).PhoneNumber;
                        profileData.ProfessionalReferenceFax2 = profile.ProfessionalReferenceInfos.ElementAt(1).Fax;

                        if (profile.ProfessionalReferenceInfos.ElementAt(1).ProviderType != null)
                        {
                            profileData.ProfessionalReferenceProviderType2 = profile.ProfessionalReferenceInfos.ElementAt(1).ProviderType.Title;
                        }

                    }

                    if (profile.ProfessionalReferenceInfos.Count > 2)
                    {
                        profileData.ProfessionalReferenceLastName3 = profile.ProfessionalReferenceInfos.ElementAt(2).LastName;
                        profileData.ProfessionalReferenceFirstName3 = profile.ProfessionalReferenceInfos.ElementAt(2).FirstName;
                        profileData.ProfessionalReferenceStreet3 = profile.ProfessionalReferenceInfos.ElementAt(2).Street;
                        profileData.ProfessionalReferenceSuiteBuilding3 = profile.ProfessionalReferenceInfos.ElementAt(2).Building;
                        profileData.ProfessionalReferenceCity3 = profile.ProfessionalReferenceInfos.ElementAt(2).City;
                        profileData.ProfessionalReferenceState3 = profile.ProfessionalReferenceInfos.ElementAt(2).State;
                        profileData.ProfessionalReferenceZip3 = profile.ProfessionalReferenceInfos.ElementAt(2).Zipcode;
                        profileData.ProfessionalReferenceTelephone3 = profile.ProfessionalReferenceInfos.ElementAt(2).PhoneNumber;
                        profileData.ProfessionalReferenceFax3 = profile.ProfessionalReferenceInfos.ElementAt(2).Fax;

                        if (profile.ProfessionalReferenceInfos.ElementAt(2).ProviderType != null)
                        {
                            profileData.ProfessionalReferenceProviderType3 = profile.ProfessionalReferenceInfos.ElementAt(2).ProviderType.Title;
                        }

                    }

                }


                #endregion


                #region Disclosure Questions                
                
                List<Question> filteredQuestions = new List<Question>();

                var question = uow.GetGenericRepository<Question>();
                var questions = question.GetAll().Where(q => q.Status != null && q.Status == AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString());

                var licensureQuestions = questions.Where(l => l.QuestionCategoryId == 1).ToList();
                filteredQuestions.Add(licensureQuestions.ElementAt(0));
                filteredQuestions.Add(licensureQuestions.ElementAt(1));

                var hospitalQuestions = questions.Where(l => l.QuestionCategoryId == 2).ToList();
                filteredQuestions.Add(hospitalQuestions.ElementAt(0));
                filteredQuestions.Add(hospitalQuestions.ElementAt(1));
                filteredQuestions.Add(hospitalQuestions.ElementAt(2));

                var educationQuestions = questions.Where(l => l.QuestionCategoryId == 3).ToList();
                filteredQuestions.Add(educationQuestions.ElementAt(0));
                filteredQuestions.Add(educationQuestions.ElementAt(1));
                filteredQuestions.Add(educationQuestions.ElementAt(2));
                filteredQuestions.Add(educationQuestions.ElementAt(3));

                var deaQuestions = questions.Where(l => l.QuestionCategoryId == 4).ToList();
                filteredQuestions.Add(deaQuestions.ElementAt(0));                

                var medicareQuestions = questions.Where(l => l.QuestionCategoryId == 5).ToList();
                filteredQuestions.Add(medicareQuestions.ElementAt(0));
                

                var otherQuestions = questions.Where(l => l.QuestionCategoryId == 6).ToList();
                filteredQuestions.Add(otherQuestions.ElementAt(0));
                filteredQuestions.Add(otherQuestions.ElementAt(1));
                filteredQuestions.Add(otherQuestions.ElementAt(2));
                filteredQuestions.Add(otherQuestions.ElementAt(3));
                filteredQuestions.Add(otherQuestions.ElementAt(4));                

                var professionalQuestions = questions.Where(l => l.QuestionCategoryId == 7).ToList();
                filteredQuestions.Add(professionalQuestions.ElementAt(0));
                filteredQuestions.Add(professionalQuestions.ElementAt(1));

                var malpracticeQuestions = questions.Where(l => l.QuestionCategoryId == 8).ToList();
                filteredQuestions.Add(malpracticeQuestions.ElementAt(0));                

                var criminalQuestions = questions.Where(l => l.QuestionCategoryId == 9).ToList();
                filteredQuestions.Add(criminalQuestions.ElementAt(0));
                filteredQuestions.Add(criminalQuestions.ElementAt(1));
                filteredQuestions.Add(criminalQuestions.ElementAt(2));

                var abiityQuestions = questions.Where(l => l.QuestionCategoryId == 10).ToList();
                filteredQuestions.Add(abiityQuestions.ElementAt(0));
                filteredQuestions.Add(abiityQuestions.ElementAt(1));
                filteredQuestions.Add(abiityQuestions.ElementAt(2));
                filteredQuestions.Add(abiityQuestions.ElementAt(3));


                List<ProfileDisclosureQuestionAnswer> answers = new List<ProfileDisclosureQuestionAnswer>();

                

                if (profile.ProfileDisclosure != null && profile.ProfileDisclosure.ProfileDisclosureQuestionAnswers.Count > 0)
                {

                    foreach (var item in profile.ProfileDisclosure.ProfileDisclosureQuestionAnswers)
                    {
                        foreach (var item1 in filteredQuestions)
                        {
                            if(item.QuestionID == item1.QuestionID)
                            {
                                answers.Add(item);
                            }
                        }

                    }

                    profileData.LicensureQn1 = answers.ElementAt(0).ProviderDisclousreAnswer;
                    profileData.LicensureQn2 = answers.ElementAt(1).ProviderDisclousreAnswer;
                    profileData.HOSPITALPRIVILEGESANDOTHERAFFILIATIONSQn1 = answers.ElementAt(2).ProviderDisclousreAnswer;
                    profileData.HOSPITALPRIVILEGESANDOTHERAFFILIATIONSQn2 = answers.ElementAt(3).ProviderDisclousreAnswer;
                    profileData.HOSPITALPRIVILEGESANDOTHERAFFILIATIONSQn3 = answers.ElementAt(4).ProviderDisclousreAnswer;
                    profileData.EDUCATIONTRAININGANDBOARDCERTIFICATIONQn1 = answers.ElementAt(5).ProviderDisclousreAnswer;
                    profileData.EDUCATIONTRAININGANDBOARDCERTIFICATIONQn2 = answers.ElementAt(6).ProviderDisclousreAnswer;
                    profileData.EDUCATIONTRAININGANDBOARDCERTIFICATIONQn3 = answers.ElementAt(7).ProviderDisclousreAnswer;
                    profileData.EDUCATIONTRAININGANDBOARDCERTIFICATIONQn4 = answers.ElementAt(8).ProviderDisclousreAnswer;
                    profileData.DEAORSTATECONTROLLEDSUBSTANCEREGISTRATIONQn1 = answers.ElementAt(9).ProviderDisclousreAnswer;
                    profileData.MEDICAREMEDICAIDOROTHERGOVERNMENTALPROGRAMPARTICIPATIONQn1 = answers.ElementAt(10).ProviderDisclousreAnswer;
                    profileData.OTHERSANCTIONSORINVESTIGATIONSQn1 = answers.ElementAt(11).ProviderDisclousreAnswer;
                    profileData.OTHERSANCTIONSORINVESTIGATIONSQn2 = answers.ElementAt(12).ProviderDisclousreAnswer;
                    profileData.OTHERSANCTIONSORINVESTIGATIONSQn3 = answers.ElementAt(13).ProviderDisclousreAnswer;
                    profileData.OTHERSANCTIONSORINVESTIGATIONSQn4 = answers.ElementAt(14).ProviderDisclousreAnswer;
                    profileData.OTHERSANCTIONSORINVESTIGATIONSQn5 = answers.ElementAt(15).ProviderDisclousreAnswer;
                    profileData.PROFESSIONALLIABILITYINSURANCEINFORMATIONANDCLAIMSHISTORYQn1 = answers.ElementAt(16).ProviderDisclousreAnswer;
                    profileData.PROFESSIONALLIABILITYINSURANCEINFORMATIONANDCLAIMSHISTORYQn2 = answers.ElementAt(17).ProviderDisclousreAnswer;
                    profileData.MalpractiseClaimHistoryQn1 = answers.ElementAt(18).ProviderDisclousreAnswer;
                    profileData.CRIMINALCIVILHISTORYQn1 = answers.ElementAt(19).ProviderDisclousreAnswer;
                    profileData.CRIMINALCIVILHISTORYQn2 = answers.ElementAt(20).ProviderDisclousreAnswer;
                    profileData.CRIMINALCIVILHISTORYQn3 = answers.ElementAt(21).ProviderDisclousreAnswer;
                    profileData.ABILITYTOPERFORMJOBQn1 = answers.ElementAt(22).ProviderDisclousreAnswer;
                    profileData.ABILITYTOPERFORMJOBQn2 = answers.ElementAt(23).ProviderDisclousreAnswer;
                    profileData.ABILITYTOPERFORMJOBQn3 = answers.ElementAt(24).ProviderDisclousreAnswer;
                    profileData.ABILITYTOPERFORMJOBQn4 = answers.ElementAt(25).ProviderDisclousreAnswer;

                }

                #endregion

                GeneratePdf pdfCall = new GeneratePdf();
                string date = DateTime.Now.ToString("MM-dd-yyyy");

                pdfName = profileData.PersonalInfoFirstName + "_Primary_" + date + ".pdf";
                var path = pdfCall.FillForm(profileData, pdfName, "CAQH_Template.pdf");

                return path;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new PDFProfileDataGeneratorManagerException(ExceptionMessage.PROFILE_PRIMARY_DATA_PDF_CREATION_EXCEPTION, ex);
            }
        }


        private List<string> ConstructSuplementProfessionaIDs(Profile profile)
        {
            var name = "";
            List<string> nameList = new List<string>();

            int federalCount = profile.FederalDEAInformations.Count;
            int cdsCount = profile.CDSCInformations.Count;
            int liscenseCount = profile.StateLicenses.Count;
            int largestCount = 0;

            try
            {

                if ((federalCount > cdsCount) && (federalCount > liscenseCount))
                {
                    largestCount = federalCount;
                }
                else if ((cdsCount > federalCount) && (cdsCount > liscenseCount))
                {
                    largestCount = cdsCount;
                }
                else if ((liscenseCount > federalCount) && (liscenseCount > cdsCount))
                {
                    largestCount = liscenseCount;
                }


                for (int i = 1; i < largestCount; i++)
                {
                    PDFSupplementDataProf_IDsBusinessModel suplement = new PDFSupplementDataProf_IDsBusinessModel();

                    if (profile.FederalDEAInformations.Count > i)
                    {
                        suplement.SupplementFederalDEANumber1 = profile.FederalDEAInformations.ElementAt(i).DEANumber;
                        suplement.SupplementFederalDEAIssueDate1 = ConvertToDateString(profile.FederalDEAInformations.ElementAt(i).IssueDate);
                        suplement.SupplementFederalDEAStateOfReg1 = profile.FederalDEAInformations.ElementAt(i).StateOfReg;
                        suplement.SupplementFederalDEAExpirationDate1 = ConvertToDateString(profile.FederalDEAInformations.ElementAt(i).ExpiryDate);
                    }

                    if (profile.CDSCInformations.Count > i)
                    {
                        suplement.SupplementCDSCertificateNumber1 = profile.CDSCInformations.ElementAt(i).CertNumber;
                        suplement.SupplementCDSIssueDate1 = ConvertToDateString(profile.CDSCInformations.ElementAt(i).IssueDate);
                        suplement.SupplementCDSStateOfReg1 = profile.CDSCInformations.ElementAt(i).State;
                        suplement.SupplementCDSExpirationDate1 = ConvertToDateString(profile.CDSCInformations.ElementAt(i).ExpiryDate);
                    }
                    i = i + 1;
                    if (profile.StateLicenses.Count > i)
                    {
                        suplement.SupplementStateLicenseNumber1 = profile.StateLicenses.ElementAt(i).LicenseNumber;
                        suplement.SupplementStateLicenseIssuingState1 = profile.StateLicenses.ElementAt(i).IssueState;
                        suplement.SupplementStateLicenseIssueDate1 = ConvertToDateString(profile.StateLicenses.ElementAt(i).IssueDate);
                        suplement.SupplementStateLicenseAreYouPractisingInThisState1 = profile.StateLicenses.ElementAt(i).IsCurrentPracticeState;
                        suplement.SupplementStateLicenseExpirationDate1 = ConvertToDateString(profile.StateLicenses.ElementAt(i).ExpiryDate);

                        if (profile.StateLicenses.Count > i)
                        {
                            suplement.SupplementStateLicenseStatusCode1 = profile.StateLicenses.ElementAt(i).StateLicenseStatus.Title;
                        }

                        if (profile.StateLicenses.Count > i)
                        {
                            suplement.SupplementStateLicenseType1 = profile.StateLicenses.ElementAt(i).ProviderType.Title;
                        }

                    }


                    if (profile.FederalDEAInformations.Count > i)
                    {
                        suplement.SupplementFederalDEANumber2 = profile.FederalDEAInformations.ElementAt(i).DEANumber;
                        suplement.SupplementFederalDEAIssueDate2 = ConvertToDateString(profile.FederalDEAInformations.ElementAt(i).IssueDate);
                        suplement.SupplementFederalDEAStateOfReg2 = profile.FederalDEAInformations.ElementAt(i).StateOfReg;
                        suplement.SupplementFederalDEAExpirationDate2 = ConvertToDateString(profile.FederalDEAInformations.ElementAt(i).ExpiryDate);
                    }

                    if (profile.CDSCInformations.Count > i)
                    {
                        suplement.SupplementCDSCertificateNumber2 = profile.CDSCInformations.ElementAt(i).CertNumber;
                        suplement.SupplementCDSIssueDate2 = ConvertToDateString(profile.CDSCInformations.ElementAt(i).IssueDate);
                        suplement.SupplementCDSStateOfReg2 = profile.CDSCInformations.ElementAt(i).State;
                        suplement.SupplementCDSExpirationDate2 = ConvertToDateString(profile.CDSCInformations.ElementAt(i).ExpiryDate);
                    }

                    i = i + 1;
                    if (profile.StateLicenses.Count > i)
                    {
                        suplement.SupplementStateLicenseNumber2 = profile.StateLicenses.ElementAt(i).LicenseNumber;
                        suplement.SupplementStateLicenseIssuingState2 = profile.StateLicenses.ElementAt(i).IssueState;
                        suplement.SupplementStateLicenseIssueDate2 = ConvertToDateString(profile.StateLicenses.ElementAt(i).IssueDate);
                        suplement.SupplementStateLicenseAreYouPractisingInThisState2 = profile.StateLicenses.ElementAt(i).IsCurrentPracticeState;
                        suplement.SupplementStateLicenseExpirationDate2 = ConvertToDateString(profile.StateLicenses.ElementAt(i).ExpiryDate);

                        if (profile.StateLicenses.Count > i)
                        {
                            suplement.SupplementStateLicenseStatusCode2 = profile.StateLicenses.ElementAt(i).StateLicenseStatus.Title;
                        }

                        if (profile.StateLicenses.Count > i)
                        {
                            suplement.SupplementStateLicenseType2 = profile.StateLicenses.ElementAt(i).ProviderType.Title;
                        }

                    }

                    i = i - 1;

                    GeneratePdf pdfCall = new GeneratePdf();
                    string date = DateTime.Now.ToString("MM-dd-yyyy");

                    string pdfName = profile.PersonalDetail.FirstName + "_SuplementID" + "_" + i + "_" + date + ".pdf";
                    name = pdfCall.FillForm(suplement, pdfName, "IDs_new.pdf");

                    nameList.Add(name);
                }


                return nameList;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new PDFProfileDataGeneratorManagerException(ExceptionMessage.PROFILE_SUPLEMENT_PROFESSIONAL_DATA_PDF_CREATION_EXCEPTION, ex);
            }
        }


        private List<string> ConstructTrainingPDF(Profile profile)
        {
            var name = "";
            List<string> trainingNAme = new List<string>();
            try
            {
                for (int i = 1; i < profile.ProgramDetails.Count; i++)
                {
                    PDFSupplementData_TrainingBusinessModel training = new PDFSupplementData_TrainingBusinessModel();

                    if (profile.ProgramDetails.Count > i)
                    {
                        training.SupplementTrainingInstitutionOrHospitalName = profile.ProgramDetails.ElementAt(i).HospitalName;
                        training.SupplementTrainingCompleteInSchool = profile.ProgramDetails.ElementAt(i).IsCompleted;
                        training.SupplementTrainingIfNotCompleteExplain = profile.ProgramDetails.ElementAt(i).InCompleteReason;
                        if (profile.ProgramDetails.ElementAt(i).SchoolInformation != null)
                        {
                            training.SupplementTrainingSchoolCode = profile.ProgramDetails.ElementAt(i).SchoolInformation.SchoolName;
                            training.SupplementTrainingStreet = profile.ProgramDetails.ElementAt(i).SchoolInformation.Street;
                            training.SupplementTrainingSuiteBuilding = profile.ProgramDetails.ElementAt(i).SchoolInformation.Building;
                            training.SupplementTrainingCity = profile.ProgramDetails.ElementAt(i).SchoolInformation.City;
                            training.SupplementTrainingState = profile.ProgramDetails.ElementAt(i).SchoolInformation.State;
                            training.SupplementTrainingZip = profile.ProgramDetails.ElementAt(i).SchoolInformation.ZipCode;
                            training.SupplementTrainingCountryCode = profile.ProgramDetails.ElementAt(i).SchoolInformation.Country;
                            training.SupplementTrainingTelephone = profile.ProgramDetails.ElementAt(i).SchoolInformation.PhoneNumber;
                            training.SupplementTrainingFax = profile.ProgramDetails.ElementAt(i).SchoolInformation.FaxNumber;
                        }

                        training.SupplementType1 = profile.ProgramDetails.ElementAt(i).ResidencyInternshipProgramType.ToString();
                        training.SupplementStartDate1 = ConvertToDateString(profile.ProgramDetails.ElementAt(i).StartDate);
                        training.SupplementEndDate1 = ConvertToDateString(profile.ProgramDetails.ElementAt(i).EndDate);
                        if (profile.ProgramDetails.ElementAt(i).Specialty != null)
                        {
                            training.SupplementDepartmentSpecialty1 = profile.ProgramDetails.ElementAt(i).Specialty.Name;
                        }
                        
                        training.SupplementNameOfDirector1 = profile.ProgramDetails.ElementAt(i).DirectorName;

                        GeneratePdf pdfCall = new GeneratePdf();
                        string date = DateTime.Now.ToString("MM-dd-yyyy");

                        string pdfName = profile.PersonalDetail.FirstName + "_Training" + "_" + i + "_" + date + ".pdf";
                        name = pdfCall.FillForm(training, pdfName, "Training_new.pdf");
                        trainingNAme.Add(name);

                    }


                }

                return trainingNAme;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new PDFProfileDataGeneratorManagerException(ExceptionMessage.PROFILE_SUPLEMENT_TRAINING_DATA_PDF_CREATION_EXCEPTION, ex);
            }
        }


        private List<string> ConstructSpecialtyPDF(Profile profile)
        {
            var name = "";
            List<string> specialtyName = new List<string>();
            try
            {
                var SpecialtyDetails = profile.SpecialtyDetails.Where(s => s.SpecialtyPreference == AHC.CD.Entities.MasterData.Enums.PreferenceType.Secondary.ToString()).ToList();
                if (SpecialtyDetails.Count > 1)
                {

                    for (int i = 1; i < SpecialtyDetails.Count; i++)
                    {
                        PDFSupplementData_SpecialtyBusinessModel specialty = new PDFSupplementData_SpecialtyBusinessModel();

                        if (SpecialtyDetails.Count > i)
                        {
                            specialty.SupplementSpecialtyCode1 = SpecialtyDetails.ElementAt(i).Specialty.Name;

                            if (profile.SpecialtyDetails.ElementAt(i).SpecialtyBoardCertifiedDetail != null)
                            {
                                specialty.SupplementSpecialtyInitialCertificationDate1 = ConvertToDateString(SpecialtyDetails.ElementAt(i).SpecialtyBoardCertifiedDetail.InitialCertificationDate);
                                specialty.SupplementSpecialtyReCertificationDate1 = ConvertToDateString(SpecialtyDetails.ElementAt(i).SpecialtyBoardCertifiedDetail.LastReCerificationDate);
                                specialty.SupplementSpecialtyExpirationDate1 = ConvertToDateString(SpecialtyDetails.ElementAt(i).SpecialtyBoardCertifiedDetail.ExpirationDate);
                                specialty.SupplementSpecialtyBoardCertified1 = SpecialtyDetails.ElementAt(i).SpecialtyBoardCertifiedDetail.SpecialtyBoard.Name;
                            }

                            if (profile.SpecialtyDetails.ElementAt(i).SpecialtyBoardNotCertifiedDetail != null)
                            {
                                specialty.SupplementSpecialtyExamStatus1 = SpecialtyDetails.ElementAt(i).SpecialtyBoardNotCertifiedDetail.SpecialtyBoardExamStatus.ToString();
                                specialty.SupplementSpecialtyReasonForNotTakingExam1 = SpecialtyDetails.ElementAt(i).SpecialtyBoardNotCertifiedDetail.ReasonForNotTakingExam;
                            }
                            specialty.SupplementSpecialtyCertifyingBoardCode1 = SpecialtyDetails.ElementAt(i).IsBoardCertified;
                            specialty.SupplementSpecialtyHMO1 = SpecialtyDetails.ElementAt(i).ListedInHMO.ToString();
                            specialty.SupplementSpecialtyPPO1 = SpecialtyDetails.ElementAt(i).ListedInPPO.ToString();
                            specialty.SupplementSpecialtyPOS1 = SpecialtyDetails.ElementAt(i).ListedInPOS.ToString();
                        }

                        i++;

                        if (profile.SpecialtyDetails.Count > i)
                        {
                            specialty.SupplementSpecialtyCode2 = SpecialtyDetails.ElementAt(i).Specialty.Name;

                            if (profile.SpecialtyDetails.ElementAt(i).SpecialtyBoardCertifiedDetail != null)
                            {
                                specialty.SupplementSpecialtyInitialCertificationDate2 = ConvertToDateString(SpecialtyDetails.ElementAt(i).SpecialtyBoardCertifiedDetail.InitialCertificationDate);
                                specialty.SupplementSpecialtyReCertificationDate2 = ConvertToDateString(SpecialtyDetails.ElementAt(i).SpecialtyBoardCertifiedDetail.LastReCerificationDate);
                                specialty.SupplementSpecialtyExpirationDate2 = ConvertToDateString(SpecialtyDetails.ElementAt(i).SpecialtyBoardCertifiedDetail.ExpirationDate);
                                specialty.SupplementSpecialtyBoardCertified2 = SpecialtyDetails.ElementAt(i).SpecialtyBoardCertifiedDetail.SpecialtyBoard.Name;
                            }

                            if (profile.SpecialtyDetails.ElementAt(i).SpecialtyBoardNotCertifiedDetail != null)
                            {
                                specialty.SupplementSpecialtyExamStatus2 = SpecialtyDetails.ElementAt(i).SpecialtyBoardNotCertifiedDetail.SpecialtyBoardExamStatus.ToString();
                                specialty.SupplementSpecialtyReasonForNotTakingExam2 = SpecialtyDetails.ElementAt(i).SpecialtyBoardNotCertifiedDetail.ReasonForNotTakingExam;
                            }
                            specialty.SupplementSpecialtyCertifyingBoardCode2 = SpecialtyDetails.ElementAt(i).IsBoardCertified;
                            specialty.SupplementSpecialtyHMO2 = SpecialtyDetails.ElementAt(i).ListedInHMO.ToString();
                            specialty.SupplementSpecialtyPPO2 = SpecialtyDetails.ElementAt(i).ListedInPPO.ToString();
                            specialty.SupplementSpecialtyPOS2 = SpecialtyDetails.ElementAt(i).ListedInPOS.ToString();
                        }

                        GeneratePdf pdfCall = new GeneratePdf();
                        string date = DateTime.Now.ToString("MM-dd-yyyy");

                        string pdfName = profile.PersonalDetail.FirstName + "_Specialty" + "_" + i + "_" + date + ".pdf";
                        name = pdfCall.FillForm(specialty, pdfName, "Specialty_new.pdf");
                        specialtyName.Add(name);

                    }
                }

                return specialtyName;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new PDFProfileDataGeneratorManagerException(ExceptionMessage.PROFILE_SUPLEMENT_SPECIALTY_DATA_PDF_CREATION_EXCEPTION, ex);
            }
        }


        private List<string> ConstructHospitalPDF(Profile profile)
        {
            var name = "";
            List<string> hospitalName = new List<string>();
            try
            {                
                
                if (profile.HospitalPrivilegeInformation != null)
                {
                    var SecondaryHospitalPrivilegeDetail = profile.HospitalPrivilegeInformation.HospitalPrivilegeDetails.Where(h => h.Preference == AHC.CD.Entities.MasterData.Enums.PreferenceType.Secondary.ToString()).ToList();

                    if (SecondaryHospitalPrivilegeDetail.Count > 1)
                    {

                        for (int i = 1; i < SecondaryHospitalPrivilegeDetail.Count; i++)
                        {
                            PDFSupplementData_HospitalPrivilegeBusinessModel hospital = new PDFSupplementData_HospitalPrivilegeBusinessModel();

                            if (SecondaryHospitalPrivilegeDetail.Count > i)
                            {
                                hospital.SupplementHospitalDepartmentName = SecondaryHospitalPrivilegeDetail.ElementAt(i).DepartmentName;
                                hospital.SupplementHospitalDepartmentDirectorFirstName = SecondaryHospitalPrivilegeDetail.ElementAt(i).DepartmentChief;
                                hospital.SupplementHospitalAffiliationStartDate = ConvertToDateString(SecondaryHospitalPrivilegeDetail.ElementAt(i).AffilicationStartDate);
                                hospital.SupplementHospitalAffiliationEndDate = ConvertToDateString(SecondaryHospitalPrivilegeDetail.ElementAt(i).AffiliationEndDate);
                                hospital.SupplementHospitalUnrestrictedPrivilege = SecondaryHospitalPrivilegeDetail.ElementAt(i).FullUnrestrictedPrevilages;
                                hospital.SupplementHospitalPrivilegesTemporary = SecondaryHospitalPrivilegeDetail.ElementAt(i).ArePrevilegesTemporary;

                                hospital.SupplementHospitalPercentageAnnualAdmission = SecondaryHospitalPrivilegeDetail.ElementAt(i).AnnualAdmisionPercentage;

                                if (SecondaryHospitalPrivilegeDetail.ElementAt(i).AdmittingPrivilege != null)
                                {
                                    hospital.SupplementHospitalAdmittingPrivilegeStatus = SecondaryHospitalPrivilegeDetail.ElementAt(i).AdmittingPrivilege.Title;
                                }

                                if (SecondaryHospitalPrivilegeDetail.ElementAt(i).Hospital != null)
                                {
                                    hospital.SupplementHospitalName = SecondaryHospitalPrivilegeDetail.ElementAt(i).Hospital.HospitalName;
                                    if (SecondaryHospitalPrivilegeDetail.ElementAt(i).Hospital.HospitalContactInfoes.Count > 0)
                                    {
                                        hospital.SupplementHospitalNumber = SecondaryHospitalPrivilegeDetail.ElementAt(i).Hospital.HospitalContactInfoes.First().UnitNumber;
                                        hospital.SupplementHospitalStreet = SecondaryHospitalPrivilegeDetail.ElementAt(i).Hospital.HospitalContactInfoes.First().Street;
                                        hospital.SupplementHospitalCity = SecondaryHospitalPrivilegeDetail.ElementAt(i).Hospital.HospitalContactInfoes.First().City;
                                        hospital.SupplementHospitalState = SecondaryHospitalPrivilegeDetail.ElementAt(i).Hospital.HospitalContactInfoes.First().State;
                                        hospital.SupplementHospitalZip = SecondaryHospitalPrivilegeDetail.ElementAt(i).Hospital.HospitalContactInfoes.First().ZipCode;
                                        hospital.SupplementHospitalTelephone = SecondaryHospitalPrivilegeDetail.ElementAt(i).Hospital.HospitalContactInfoes.First().Phone;
                                        hospital.SupplementHospitalFax = SecondaryHospitalPrivilegeDetail.ElementAt(i).Hospital.HospitalContactInfoes.First().Fax;
                                    }

                                }


                            }

                            GeneratePdf pdfCall = new GeneratePdf();
                            string date = DateTime.Now.ToString("MM-dd-yyyy");

                            string pdfName = profile.PersonalDetail.FirstName + "_Hospital" + "_" + i + "_" + date + ".pdf";
                            name = pdfCall.FillForm(hospital, pdfName, "Hosp_new.pdf");
                            hospitalName.Add(name);
                        }
                    }

                }


                return hospitalName;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new PDFProfileDataGeneratorManagerException(ExceptionMessage.PROFILE_SUPLEMENT_HOSPITAL_DATA_PDF_CREATION_EXCEPTION, ex);
            }
        }


        private List<string> ConvertProfessionalLiabilityPDF(Profile profile)
        {
            var name = "";
            List<string> liabilityName = new List<string>();
            try
            {
                for (int i = 2; i < profile.ProfessionalLiabilityInfoes.Count; i++)
                {
                    PDFSupplementData_Prof_LiabilityBusinessModel liability = new PDFSupplementData_Prof_LiabilityBusinessModel();

                    if (profile.ProfessionalLiabilityInfoes.Count > i)
                    {
                        if (profile.ProfessionalLiabilityInfoes.ElementAt(i).InsuranceCarrier != null)
                        {
                            liability.SupplementProfessionalLiabilityCarrierName1 = profile.ProfessionalLiabilityInfoes.ElementAt(i).InsuranceCarrier.Name;
                        }

                        if (profile.ProfessionalLiabilityInfoes.ElementAt(i).InsuranceCarrierAddress != null)
                        {
                            liability.SupplementProfessionalLiabilityStreet1 = profile.ProfessionalLiabilityInfoes.ElementAt(i).InsuranceCarrierAddress.Street;
                            liability.SupplementProfessionalLiabilitySuiteBuilding1 = profile.ProfessionalLiabilityInfoes.ElementAt(i).InsuranceCarrierAddress.Building;
                            liability.SupplementProfessionalLiabilityCity1 = profile.ProfessionalLiabilityInfoes.ElementAt(i).InsuranceCarrierAddress.City;
                            liability.SupplementProfessionalLiabilityState1 = profile.ProfessionalLiabilityInfoes.ElementAt(i).InsuranceCarrierAddress.State;
                            liability.SupplementProfessionalLiabilityZip1 = profile.ProfessionalLiabilityInfoes.ElementAt(i).InsuranceCarrierAddress.ZipCode;

                        }
                        liability.SupplementProfessionalLiabilityEffectiveDate1 = ConvertToDateString(profile.ProfessionalLiabilityInfoes.ElementAt(i).EffectiveDate);
                        liability.SupplementProfessionalLiabilityExpirationDate1 = ConvertToDateString(profile.ProfessionalLiabilityInfoes.ElementAt(i).ExpirationDate);
                        liability.SupplementProfessionalLiabilityCoverageType1 = profile.ProfessionalLiabilityInfoes.ElementAt(i).CoverageType;
                        liability.SupplementProfessionalLiabilityHAVEUNLIMITEDCOVERAGE1 = profile.ProfessionalLiabilityInfoes.ElementAt(i).UnlimitedCoverageWithInsuranceCarrier;
                        liability.SupplementProfessionalLiabilityTAILCOVERAGE1 = profile.ProfessionalLiabilityInfoes.ElementAt(i).PolicyIncludesTailCoverage;
                        liability.SupplementAMOUNTOFCOVERAGEPEROCCURRENCE1 = profile.ProfessionalLiabilityInfoes.ElementAt(i).AmountOfCoveragePerOccurance;
                        liability.SupplementAMOUNTOFCOVERAGEAGGREGATE1 = profile.ProfessionalLiabilityInfoes.ElementAt(i).AmountOfCoverageAggregate;
                        liability.SupplementProfessionalLiabilityOriginalEffectiveDate1 = ConvertToDateString(profile.ProfessionalLiabilityInfoes.ElementAt(i).OriginalEffectiveDate);
                        liability.SupplementProfessionalLiabilitySelfInsured1 = profile.ProfessionalLiabilityInfoes.ElementAt(i).SelfInsured;
                        liability.SupplementPOLICYNUMBER1 = profile.ProfessionalLiabilityInfoes.ElementAt(i).PolicyNumber;

                    }

                    i++;

                    if (profile.ProfessionalLiabilityInfoes.Count > i)
                    {
                        if (profile.ProfessionalLiabilityInfoes.ElementAt(i).InsuranceCarrier != null)
                        {
                            liability.SupplementProfessionalLiabilityCarrierName2 = profile.ProfessionalLiabilityInfoes.ElementAt(i).InsuranceCarrier.Name;
                        }

                        if (profile.ProfessionalLiabilityInfoes.ElementAt(i).InsuranceCarrierAddress != null)
                        {
                            liability.SupplementProfessionalLiabilityStreet2 = profile.ProfessionalLiabilityInfoes.ElementAt(i).InsuranceCarrierAddress.Street;
                            liability.SupplementProfessionalLiabilitySuiteBuilding2 = profile.ProfessionalLiabilityInfoes.ElementAt(i).InsuranceCarrierAddress.Building;
                            liability.SupplementProfessionalLiabilityCity2 = profile.ProfessionalLiabilityInfoes.ElementAt(i).InsuranceCarrierAddress.City;
                            liability.SupplementProfessionalLiabilityState2 = profile.ProfessionalLiabilityInfoes.ElementAt(i).InsuranceCarrierAddress.State;
                            liability.SupplementProfessionalLiabilityZip2 = profile.ProfessionalLiabilityInfoes.ElementAt(i).InsuranceCarrierAddress.ZipCode;

                        }
                        liability.SupplementProfessionalLiabilityEffectiveDate2 = ConvertToDateString(profile.ProfessionalLiabilityInfoes.ElementAt(i).EffectiveDate);
                        liability.SupplementProfessionalLiabilityExpirationDate2 = ConvertToDateString(profile.ProfessionalLiabilityInfoes.ElementAt(i).ExpirationDate);
                        liability.SupplementProfessionalLiabilityCoverageType2 = profile.ProfessionalLiabilityInfoes.ElementAt(i).CoverageType;
                        liability.SupplementProfessionalLiabilityHAVEUNLIMITEDCOVERAGE2 = profile.ProfessionalLiabilityInfoes.ElementAt(i).UnlimitedCoverageWithInsuranceCarrier;
                        liability.SupplementProfessionalLiabilityTAILCOVERAGE2 = profile.ProfessionalLiabilityInfoes.ElementAt(i).PolicyIncludesTailCoverage;
                        liability.SupplementAMOUNTOFCOVERAGEPEROCCURRENCE2 = profile.ProfessionalLiabilityInfoes.ElementAt(i).AmountOfCoveragePerOccurance;
                        liability.SupplementAMOUNTOFCOVERAGEAGGREGATE2 = profile.ProfessionalLiabilityInfoes.ElementAt(i).AmountOfCoverageAggregate;
                        liability.SupplementProfessionalLiabilityOriginalEffectiveDate2 = ConvertToDateString(profile.ProfessionalLiabilityInfoes.ElementAt(i).OriginalEffectiveDate);
                        liability.SupplementProfessionalLiabilitySelfInsured2 = profile.ProfessionalLiabilityInfoes.ElementAt(i).SelfInsured;
                        liability.SupplementPOLICYNUMBER2 = profile.ProfessionalLiabilityInfoes.ElementAt(i).PolicyNumber;

                    }

                    GeneratePdf pdfCall = new GeneratePdf();
                    string date = DateTime.Now.ToString("MM-dd-yyyy");

                    string pdfName = profile.PersonalDetail.FirstName + "_Liability" + "_" + i + "_" + date + ".pdf";
                    name = pdfCall.FillForm(liability, pdfName, "Liability_new.pdf");
                    liabilityName.Add(name);
                }

                return liabilityName;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new PDFProfileDataGeneratorManagerException(ExceptionMessage.PROFILE_SUPLEMENT_LIABILITY_DATA_PDF_CREATION_EXCEPTION, ex);
            }
        }

        private List<string> ConstructWorkHistoryPDF(Profile profile)
        {
            var name = "";
            List<string> historyName = new List<string>();
            try
            {
                for (int i = 3; i < profile.ProfessionalWorkExperiences.Count; i++)
                {
                    PDFSupplementData_WorkHistoryBusinessModel history = new PDFSupplementData_WorkHistoryBusinessModel();

                    if (profile.ProfessionalWorkExperiences.Count > i)
                    {
                        history.SupplementWorkHistoryPractiseName1 = profile.ProfessionalWorkExperiences.ElementAt(i).EmployerName;
                        history.SupplementWorkHistoryStreet1 = profile.ProfessionalWorkExperiences.ElementAt(i).Street;
                        history.SupplementWorkHistorySuiteBuilding1 = profile.ProfessionalWorkExperiences.ElementAt(i).Building;
                        history.SupplementWorkHistoryCity1 = profile.ProfessionalWorkExperiences.ElementAt(i).City;
                        history.SupplementWorkHistoryState1 = profile.ProfessionalWorkExperiences.ElementAt(i).State;
                        history.SupplementWorkHistoryZip1 = profile.ProfessionalWorkExperiences.ElementAt(i).ZipCode;
                        history.SupplementWorkHistoryTelephone1 = profile.ProfessionalWorkExperiences.ElementAt(i).MobileNumber;
                        history.SupplementWorkHistoryFax1 = profile.ProfessionalWorkExperiences.ElementAt(i).FaxNumber;
                        history.SupplementWorkHistoryCountryCode1 = profile.ProfessionalWorkExperiences.ElementAt(i).Country;
                        history.SupplementWorkHistoryStartDate1 = ConvertToDateString(profile.ProfessionalWorkExperiences.ElementAt(i).StartDate);
                        history.SupplementWorkHistoryEndDate1 = ConvertToDateString(profile.ProfessionalWorkExperiences.ElementAt(i).EndDate);
                        history.SupplementWorkHistoryReason1 = profile.ProfessionalWorkExperiences.ElementAt(i).DepartureReason;
                    }

                    i++;

                    if (profile.ProfessionalWorkExperiences.Count > i)
                    {
                        history.SupplementWorkHistoryPractiseName2 = profile.ProfessionalWorkExperiences.ElementAt(i).EmployerName;
                        history.SupplementWorkHistoryStreet2 = profile.ProfessionalWorkExperiences.ElementAt(i).Street;
                        history.SupplementWorkHistorySuiteBuilding2 = profile.ProfessionalWorkExperiences.ElementAt(i).Building;
                        history.SupplementWorkHistoryCity2 = profile.ProfessionalWorkExperiences.ElementAt(i).City;
                        history.SupplementWorkHistoryState2 = profile.ProfessionalWorkExperiences.ElementAt(i).State;
                        history.SupplementWorkHistoryZip2 = profile.ProfessionalWorkExperiences.ElementAt(i).ZipCode;
                        history.SupplementWorkHistoryTelephone2 = profile.ProfessionalWorkExperiences.ElementAt(i).MobileNumber;
                        history.SupplementWorkHistoryFax2 = profile.ProfessionalWorkExperiences.ElementAt(i).FaxNumber;
                        history.SupplementWorkHistoryCountryCode2 = profile.ProfessionalWorkExperiences.ElementAt(i).Country;
                        history.SupplementWorkHistoryStartDate2 = ConvertToDateString(profile.ProfessionalWorkExperiences.ElementAt(i).StartDate);
                        history.SupplementWorkHistoryEndDate2 = ConvertToDateString(profile.ProfessionalWorkExperiences.ElementAt(i).EndDate);
                        history.SupplementWorkHistoryReason2 = profile.ProfessionalWorkExperiences.ElementAt(i).DepartureReason;
                    }

                    GeneratePdf pdfCall = new GeneratePdf();
                    string date = DateTime.Now.ToString("MM-dd-yyyy");

                    string pdfName = profile.PersonalDetail.FirstName + "_History" + "_" + i + "_" + date + ".pdf";
                    name = pdfCall.FillForm(history, pdfName, "WorkHistory_new.pdf");
                    historyName.Add(name);
                }

                return historyName;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new PDFProfileDataGeneratorManagerException(ExceptionMessage.PROFILE_SUPLEMENT_WORKHISTORY_DATA_PDF_CREATION_EXCEPTION, ex);
            }
        }

        private List<string> ConstrustWorkGapsPdf(Profile profile)
        {
            var name = "";
            List<string> gapName = new List<string>();
            try
            {
                for (int i = 1; i < profile.WorkGaps.Count; i++)
                {
                    PDFSupplementData_WorkGapBusinessModel workGaps = new PDFSupplementData_WorkGapBusinessModel();

                    if (profile.WorkGaps.Count > i)
                    {
                        workGaps.GapStartDate1 = ConvertToDateString(profile.WorkGaps.ElementAt(i).StartDate);
                        workGaps.GapEndDate1 = ConvertToDateString(profile.WorkGaps.ElementAt(i).EndDate);
                        workGaps.GapReason1 = profile.WorkGaps.ElementAt(i).Description;
                    }

                    i++;

                    if (profile.WorkGaps.Count > i)
                    {
                        workGaps.GapStartDate2 = ConvertToDateString(profile.WorkGaps.ElementAt(i).StartDate);
                        workGaps.GapEndDate2 = ConvertToDateString(profile.WorkGaps.ElementAt(i).EndDate);
                        workGaps.GapReason2 = profile.WorkGaps.ElementAt(i).Description;
                    }

                    i++;

                    if (profile.WorkGaps.Count > i)
                    {
                        workGaps.GapStartDate3 = ConvertToDateString(profile.WorkGaps.ElementAt(i).StartDate);
                        workGaps.GapEndDate3 = ConvertToDateString(profile.WorkGaps.ElementAt(i).EndDate);
                        workGaps.GapReason3 = profile.WorkGaps.ElementAt(i).Description;
                    }

                    i++;

                    if (profile.WorkGaps.Count > i)
                    {
                        workGaps.GapStartDate4 = ConvertToDateString(profile.WorkGaps.ElementAt(i).StartDate);
                        workGaps.GapEndDate4 = ConvertToDateString(profile.WorkGaps.ElementAt(i).EndDate);
                        workGaps.GapReason4 = profile.WorkGaps.ElementAt(i).Description;
                    }

                    i++;

                    if (profile.WorkGaps.Count > i)
                    {
                        workGaps.GapStartDate5 = ConvertToDateString(profile.WorkGaps.ElementAt(i).StartDate);
                        workGaps.GapEndDate5 = ConvertToDateString(profile.WorkGaps.ElementAt(i).EndDate);
                        workGaps.GapReason5 = profile.WorkGaps.ElementAt(i).Description;
                    }

                    GeneratePdf pdfCall = new GeneratePdf();
                    string date = DateTime.Now.ToString("MM-dd-yyyy");

                    string pdfName = profile.PersonalDetail.FirstName + "_WorkGaps" + "_" + i + "_" + date + ".pdf";
                    name = pdfCall.FillForm(workGaps, pdfName, "Gap_new.pdf");
                    gapName.Add(name);
                }

                return gapName;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new PDFProfileDataGeneratorManagerException(ExceptionMessage.PROFILE_SUPLEMENT_WORKGAPS_DATA_PDF_CREATION_EXCEPTION, ex);
            }
        }

        private List<string> ConstructPrimaryCoveringColleaguePDF(Profile profile)
        {
            var name = "";
            List<string> colleagueName = new List<string>();
            try
            {
                var primaryPracticeLocation = profile.PracticeLocationDetails.FirstOrDefault(p => p.IsPrimary == AHC.CD.Entities.MasterData.Enums.YesNoOption.YES.ToString());


                if (primaryPracticeLocation != null)
                {
                    var coveringColleagues1 = primaryPracticeLocation.PracticeProviders.Where(p => p.Practice == AHC.CD.Entities.MasterData.Enums.PracticeType.CoveringColleague.ToString() && p.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();

                    for (int j = 3; j < coveringColleagues1.Count; j++)
                    {

                        PDFSupplementData_CoveringColleagueBusinessModel colleague = new PDFSupplementData_CoveringColleagueBusinessModel();

                        if (primaryPracticeLocation.Facility != null)
                        {
                            colleague.SupplementCoveringColleaguePrimaryPractiseLocation = primaryPracticeLocation.Facility.Building;

                            colleague.SupplementCoveringColleaguePractiseLocationName = primaryPracticeLocation.PracticeLocationCorporateName;
                            colleague.SupplementCoveringColleaguePrimaryPractiseLocation = primaryPracticeLocation.IsPrimary;
                        }

                        if (coveringColleagues1.Count > j)
                        {
                            colleague.SupplementCoveringColleguesLastName1 = coveringColleagues1.ElementAt(j).LastName;
                            colleague.SupplementCoveringColleguesFirstName1 = coveringColleagues1.ElementAt(j).FirstName;

                            if (coveringColleagues1.ElementAt(j).PracticeProviderSpecialties.Count > 0 && coveringColleagues1.ElementAt(j).PracticeProviderSpecialties.First().Specialty != null)
                                colleague.SupplementCoveringColleguesSpecialtyCode1 = coveringColleagues1.ElementAt(j).PracticeProviderSpecialties.First().Specialty.Name;

                            if (coveringColleagues1.ElementAt(j).PracticeProviderTypes.Count > 0 && coveringColleagues1.ElementAt(j).PracticeProviderTypes.First().ProviderType != null)
                                colleague.SupplementCoveringColleguesProviderType1 = coveringColleagues1.ElementAt(j).PracticeProviderTypes.First().ProviderType.Title;

                        }
                        j++;
                        if (coveringColleagues1.Count > j)
                        {
                            colleague.SupplementCoveringColleguesLastName2 = coveringColleagues1.ElementAt(j).LastName;
                            colleague.SupplementCoveringColleguesFirstName2 = coveringColleagues1.ElementAt(j).FirstName;

                            if (coveringColleagues1.ElementAt(j).PracticeProviderSpecialties.Count > 0 && coveringColleagues1.ElementAt(j).PracticeProviderSpecialties.First().Specialty != null)
                                colleague.SupplementCoveringColleguesSpecialtyCode2 = coveringColleagues1.ElementAt(j).PracticeProviderSpecialties.First().Specialty.Name;

                            if (coveringColleagues1.ElementAt(j).PracticeProviderTypes.Count > 0 && coveringColleagues1.ElementAt(j).PracticeProviderTypes.First().ProviderType != null)
                                colleague.SupplementCoveringColleguesProviderType2 = coveringColleagues1.ElementAt(j).PracticeProviderTypes.First().ProviderType.Title;

                        }
                        j++;
                        if (coveringColleagues1.Count > j)
                        {
                            colleague.SupplementCoveringColleguesLastName3 = coveringColleagues1.ElementAt(j).LastName;
                            colleague.SupplementCoveringColleguesFirstName3 = coveringColleagues1.ElementAt(j).FirstName;

                            if (coveringColleagues1.ElementAt(j).PracticeProviderSpecialties.Count > 0 && coveringColleagues1.ElementAt(j).PracticeProviderSpecialties.First().Specialty != null)
                                colleague.SupplementCoveringColleguesSpecialtyCode3 = coveringColleagues1.ElementAt(j).PracticeProviderSpecialties.First().Specialty.Name;

                            if (coveringColleagues1.ElementAt(j).PracticeProviderTypes.Count > 0 && coveringColleagues1.ElementAt(j).PracticeProviderTypes.First().ProviderType != null)
                                colleague.SupplementCoveringColleguesProviderType3 = coveringColleagues1.ElementAt(j).PracticeProviderTypes.First().ProviderType.Title;

                        }
                        j++;
                        if (coveringColleagues1.Count > j)
                        {
                            colleague.SupplementCoveringColleguesLastName4 = coveringColleagues1.ElementAt(j).LastName;
                            colleague.SupplementCoveringColleguesFirstName4 = coveringColleagues1.ElementAt(j).FirstName;

                            if (coveringColleagues1.ElementAt(j).PracticeProviderSpecialties.Count > 0 && coveringColleagues1.ElementAt(j).PracticeProviderSpecialties.First().Specialty != null)
                                colleague.SupplementCoveringColleguesSpecialtyCode4 = coveringColleagues1.ElementAt(j).PracticeProviderSpecialties.First().Specialty.Name;

                            if (coveringColleagues1.ElementAt(j).PracticeProviderTypes.Count > 0 && coveringColleagues1.ElementAt(j).PracticeProviderTypes.First().ProviderType != null)
                                colleague.SupplementCoveringColleguesProviderType4 = coveringColleagues1.ElementAt(j).PracticeProviderTypes.First().ProviderType.Title;

                        }
                        j++;
                        if (coveringColleagues1.Count > j)
                        {
                            colleague.SupplementCoveringColleguesLastName5 = coveringColleagues1.ElementAt(j).LastName;
                            colleague.SupplementCoveringColleguesFirstName5 = coveringColleagues1.ElementAt(j).FirstName;

                            if (coveringColleagues1.ElementAt(j).PracticeProviderSpecialties.Count > 0 && coveringColleagues1.ElementAt(j).PracticeProviderSpecialties.First().Specialty != null)
                                colleague.SupplementCoveringColleguesSpecialtyCode5 = coveringColleagues1.ElementAt(j).PracticeProviderSpecialties.First().Specialty.Name;

                            if (coveringColleagues1.ElementAt(j).PracticeProviderTypes.Count > 0 && coveringColleagues1.ElementAt(j).PracticeProviderTypes.First().ProviderType != null)
                                colleague.SupplementCoveringColleguesProviderType5 = coveringColleagues1.ElementAt(j).PracticeProviderTypes.First().ProviderType.Title;

                        }
                        j++;
                        if (coveringColleagues1.Count > j)
                        {
                            colleague.SupplementCoveringColleguesLastName6 = coveringColleagues1.ElementAt(j).LastName;
                            colleague.SupplementCoveringColleguesFirstName6 = coveringColleagues1.ElementAt(j).FirstName;

                            if (coveringColleagues1.ElementAt(j).PracticeProviderSpecialties.Count > 0 && coveringColleagues1.ElementAt(j).PracticeProviderSpecialties.First().Specialty != null)
                                colleague.SupplementCoveringColleguesSpecialtyCode6 = coveringColleagues1.ElementAt(j).PracticeProviderSpecialties.First().Specialty.Name;

                            if (coveringColleagues1.ElementAt(j).PracticeProviderTypes.Count > 0 && coveringColleagues1.ElementAt(j).PracticeProviderTypes.First().ProviderType != null)
                                colleague.SupplementCoveringColleguesProviderType6 = coveringColleagues1.ElementAt(j).PracticeProviderTypes.First().ProviderType.Title;

                        }
                        j++;
                        if (coveringColleagues1.Count > j)
                        {
                            colleague.SupplementCoveringColleguesLastName7 = coveringColleagues1.ElementAt(j).LastName;
                            colleague.SupplementCoveringColleguesFirstName7 = coveringColleagues1.ElementAt(j).FirstName;

                            if (coveringColleagues1.ElementAt(j).PracticeProviderSpecialties.Count > 0 && coveringColleagues1.ElementAt(j).PracticeProviderSpecialties.First().Specialty != null)
                                colleague.SupplementCoveringColleguesSpecialtyCode7 = coveringColleagues1.ElementAt(j).PracticeProviderSpecialties.First().Specialty.Name;

                            if (coveringColleagues1.ElementAt(j).PracticeProviderTypes.Count > 0 && coveringColleagues1.ElementAt(j).PracticeProviderTypes.First().ProviderType != null)
                                colleague.SupplementCoveringColleguesProviderType7 = coveringColleagues1.ElementAt(j).PracticeProviderTypes.First().ProviderType.Title;

                        }
                        j++;
                        if (coveringColleagues1.Count > j)
                        {
                            colleague.SupplementCoveringColleguesLastName8 = coveringColleagues1.ElementAt(j).LastName;
                            colleague.SupplementCoveringColleguesFirstName8 = coveringColleagues1.ElementAt(j).FirstName;

                            if (coveringColleagues1.ElementAt(j).PracticeProviderSpecialties.Count > 0 && coveringColleagues1.ElementAt(j).PracticeProviderSpecialties.First().Specialty != null)
                                colleague.SupplementCoveringColleguesSpecialtyCode8 = coveringColleagues1.ElementAt(j).PracticeProviderSpecialties.First().Specialty.Name;

                            if (coveringColleagues1.ElementAt(j).PracticeProviderTypes.Count > 0 && coveringColleagues1.ElementAt(j).PracticeProviderTypes.First().ProviderType != null)
                                colleague.SupplementCoveringColleguesProviderType8 = coveringColleagues1.ElementAt(j).PracticeProviderTypes.First().ProviderType.Title;

                        }


                        GeneratePdf pdfCall = new GeneratePdf();
                        string date = DateTime.Now.ToString("MM-dd-yyyy");

                        string pdfName = profile.PersonalDetail.FirstName + "_PrimaryColleague" + "_" + j + "_" + date + ".pdf";
                        name = pdfCall.FillForm(colleague, pdfName, "Covering Collg_new.pdf");
                        colleagueName.Add(name);
                    }


                }

                return colleagueName;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new PDFProfileDataGeneratorManagerException(ExceptionMessage.PROFILE_SUPLEMENT_COLLEAGUES_DATA_PDF_CREATION_EXCEPTION, ex);
            }
        }

        private List<string> ConstructSecondaryCoveringColleaguePDF(Profile profile)
        {
            var name = "";
            List<string> colleagueName = new List<string>();
            try
            {
                var secondaryPracticeLocation = profile.PracticeLocationDetails.FirstOrDefault(p => p.IsPrimary == AHC.CD.Entities.MasterData.Enums.YesNoOption.NO.ToString());


                if (secondaryPracticeLocation != null)
                {
                    var coveringColleagues1 = secondaryPracticeLocation.PracticeProviders.Where(p => p.Practice == AHC.CD.Entities.MasterData.Enums.PracticeType.CoveringColleague.ToString() && p.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();

                    for (int j = 4; j < coveringColleagues1.Count; j++)
                    {
                        PDFSupplementData_CoveringColleagueBusinessModel colleague = new PDFSupplementData_CoveringColleagueBusinessModel();

                        if (secondaryPracticeLocation.Facility != null)
                        {
                            colleague.SupplementCoveringColleaguePrimaryPractiseLocation = secondaryPracticeLocation.Facility.Building;

                            colleague.SupplementCoveringColleaguePractiseLocationName = secondaryPracticeLocation.PracticeLocationCorporateName;
                            colleague.SupplementCoveringColleaguePrimaryPractiseLocation = secondaryPracticeLocation.IsPrimary;
                        }

                        if (coveringColleagues1.Count > j)
                        {
                            colleague.SupplementCoveringColleguesLastName1 = coveringColleagues1.ElementAt(j).LastName;
                            colleague.SupplementCoveringColleguesFirstName1 = coveringColleagues1.ElementAt(j).FirstName;

                            if (coveringColleagues1.ElementAt(j).PracticeProviderSpecialties.Count > 0 && coveringColleagues1.ElementAt(j).PracticeProviderSpecialties.First().Specialty != null)
                                colleague.SupplementCoveringColleguesSpecialtyCode1 = coveringColleagues1.ElementAt(j).PracticeProviderSpecialties.First().Specialty.Name;

                            if (coveringColleagues1.ElementAt(j).PracticeProviderTypes.Count > 0 && coveringColleagues1.ElementAt(j).PracticeProviderTypes.First().ProviderType != null)
                                colleague.SupplementCoveringColleguesProviderType1 = coveringColleagues1.ElementAt(j).PracticeProviderTypes.First().ProviderType.Title;

                        }
                        j++;
                        if (coveringColleagues1.Count > j)
                        {
                            colleague.SupplementCoveringColleguesLastName2 = coveringColleagues1.ElementAt(j).LastName;
                            colleague.SupplementCoveringColleguesFirstName2 = coveringColleagues1.ElementAt(j).FirstName;

                            if (coveringColleagues1.ElementAt(j).PracticeProviderSpecialties.Count > 0 && coveringColleagues1.ElementAt(j).PracticeProviderSpecialties.First().Specialty != null)
                                colleague.SupplementCoveringColleguesSpecialtyCode2 = coveringColleagues1.ElementAt(j).PracticeProviderSpecialties.First().Specialty.Name;

                            if (coveringColleagues1.ElementAt(j).PracticeProviderTypes.Count > 0 && coveringColleagues1.ElementAt(j).PracticeProviderTypes.First().ProviderType != null)
                                colleague.SupplementCoveringColleguesProviderType2 = coveringColleagues1.ElementAt(j).PracticeProviderTypes.First().ProviderType.Title;

                        }
                        j++;
                        if (coveringColleagues1.Count > j)
                        {
                            colleague.SupplementCoveringColleguesLastName3 = coveringColleagues1.ElementAt(j).LastName;
                            colleague.SupplementCoveringColleguesFirstName3 = coveringColleagues1.ElementAt(j).FirstName;

                            if (coveringColleagues1.ElementAt(j).PracticeProviderSpecialties.Count > 0 && coveringColleagues1.ElementAt(j).PracticeProviderSpecialties.First().Specialty != null)
                                colleague.SupplementCoveringColleguesSpecialtyCode3 = coveringColleagues1.ElementAt(j).PracticeProviderSpecialties.First().Specialty.Name;

                            if (coveringColleagues1.ElementAt(j).PracticeProviderTypes.Count > 0 && coveringColleagues1.ElementAt(j).PracticeProviderTypes.First().ProviderType != null)
                                colleague.SupplementCoveringColleguesProviderType3 = coveringColleagues1.ElementAt(j).PracticeProviderTypes.First().ProviderType.Title;

                        }
                        j++;
                        if (coveringColleagues1.Count > j)
                        {
                            colleague.SupplementCoveringColleguesLastName4 = coveringColleagues1.ElementAt(j).LastName;
                            colleague.SupplementCoveringColleguesFirstName4 = coveringColleagues1.ElementAt(j).FirstName;

                            if (coveringColleagues1.ElementAt(j).PracticeProviderSpecialties.Count > 0 && coveringColleagues1.ElementAt(j).PracticeProviderSpecialties.First().Specialty != null)
                                colleague.SupplementCoveringColleguesSpecialtyCode4 = coveringColleagues1.ElementAt(j).PracticeProviderSpecialties.First().Specialty.Name;

                            if (coveringColleagues1.ElementAt(j).PracticeProviderTypes.Count > 0 && coveringColleagues1.ElementAt(j).PracticeProviderTypes.First().ProviderType != null)
                                colleague.SupplementCoveringColleguesProviderType4 = coveringColleagues1.ElementAt(j).PracticeProviderTypes.First().ProviderType.Title;

                        }
                        j++;
                        if (coveringColleagues1.Count > j)
                        {
                            colleague.SupplementCoveringColleguesLastName5 = coveringColleagues1.ElementAt(j).LastName;
                            colleague.SupplementCoveringColleguesFirstName5 = coveringColleagues1.ElementAt(j).FirstName;

                            if (coveringColleagues1.ElementAt(j).PracticeProviderSpecialties.Count > 0 && coveringColleagues1.ElementAt(j).PracticeProviderSpecialties.First().Specialty != null)
                                colleague.SupplementCoveringColleguesSpecialtyCode5 = coveringColleagues1.ElementAt(j).PracticeProviderSpecialties.First().Specialty.Name;

                            if (coveringColleagues1.ElementAt(j).PracticeProviderTypes.Count > 0 && coveringColleagues1.ElementAt(j).PracticeProviderTypes.First().ProviderType != null)
                                colleague.SupplementCoveringColleguesProviderType5 = coveringColleagues1.ElementAt(j).PracticeProviderTypes.First().ProviderType.Title;

                        }                        
                        j++;
                        if (coveringColleagues1.Count > j)
                        {
                            colleague.SupplementCoveringColleguesLastName6 = coveringColleagues1.ElementAt(j).LastName;
                            colleague.SupplementCoveringColleguesFirstName6 = coveringColleagues1.ElementAt(j).FirstName;

                            if (coveringColleagues1.ElementAt(j).PracticeProviderSpecialties.Count > 0 && coveringColleagues1.ElementAt(j).PracticeProviderSpecialties.First().Specialty != null)
                                colleague.SupplementCoveringColleguesSpecialtyCode6 = coveringColleagues1.ElementAt(j).PracticeProviderSpecialties.First().Specialty.Name;

                            if (coveringColleagues1.ElementAt(j).PracticeProviderTypes.Count > 0 && coveringColleagues1.ElementAt(j).PracticeProviderTypes.First().ProviderType != null)
                                colleague.SupplementCoveringColleguesProviderType6 = coveringColleagues1.ElementAt(j).PracticeProviderTypes.First().ProviderType.Title;

                        }
                        j++;
                        if (coveringColleagues1.Count > j)
                        {
                            colleague.SupplementCoveringColleguesLastName7 = coveringColleagues1.ElementAt(j).LastName;
                            colleague.SupplementCoveringColleguesFirstName7 = coveringColleagues1.ElementAt(j).FirstName;

                            if (coveringColleagues1.ElementAt(j).PracticeProviderSpecialties.Count > 0 && coveringColleagues1.ElementAt(j).PracticeProviderSpecialties.First().Specialty != null)
                                colleague.SupplementCoveringColleguesSpecialtyCode7 = coveringColleagues1.ElementAt(j).PracticeProviderSpecialties.First().Specialty.Name;

                            if (coveringColleagues1.ElementAt(j).PracticeProviderTypes.Count > 0 && coveringColleagues1.ElementAt(j).PracticeProviderTypes.First().ProviderType != null)
                                colleague.SupplementCoveringColleguesProviderType7 = coveringColleagues1.ElementAt(j).PracticeProviderTypes.First().ProviderType.Title;

                        }
                        j++;
                        if (coveringColleagues1.Count > j)
                        {
                            colleague.SupplementCoveringColleguesLastName8 = coveringColleagues1.ElementAt(j).LastName;
                            colleague.SupplementCoveringColleguesFirstName8 = coveringColleagues1.ElementAt(j).FirstName;

                            if (coveringColleagues1.ElementAt(j).PracticeProviderSpecialties.Count > 0 && coveringColleagues1.ElementAt(j).PracticeProviderSpecialties.First().Specialty != null)
                                colleague.SupplementCoveringColleguesSpecialtyCode8 = coveringColleagues1.ElementAt(j).PracticeProviderSpecialties.First().Specialty.Name;

                            if (coveringColleagues1.ElementAt(j).PracticeProviderTypes.Count > 0 && coveringColleagues1.ElementAt(j).PracticeProviderTypes.First().ProviderType != null)
                                colleague.SupplementCoveringColleguesProviderType8 = coveringColleagues1.ElementAt(j).PracticeProviderTypes.First().ProviderType.Title;

                        }


                        GeneratePdf pdfCall = new GeneratePdf();
                        string date = DateTime.Now.ToString("MM-dd-yyyy");

                        string pdfName = profile.PersonalDetail.FirstName + "_SecondaryColleague" + "_" + j + "_" + date + ".pdf";
                        name = pdfCall.FillForm(colleague, pdfName, "Covering Collg_new.pdf");
                        colleagueName.Add(name);
                    }


                }

                return colleagueName;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new PDFProfileDataGeneratorManagerException(ExceptionMessage.PROFILE_SUPLEMENT_COLLEAGUES_DATA_PDF_CREATION_EXCEPTION, ex);
            }
        }

        private List<string> ConstructDisclosurePDF(Profile profile)
        {
            var name = "";
            List<string> disclosureName = new List<string>();
            try
            {
                if (profile.ProfileDisclosure != null)
                {

                    List<Question> filteredQuestions = new List<Question>();

                    var questionRepo = uow.GetGenericRepository<Question>();
                    var questions = questionRepo.GetAll().Where(q => q.Status != null && q.Status == AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString()).ToList();

                    if (questions.Count > 0)
                    {

                        var licensureQuestions = questions.Where(l => l.QuestionCategoryId == 1).ToList();
                        if (licensureQuestions.Count > 1)
                        {
                            filteredQuestions.Add(licensureQuestions.ElementAt(0));
                            filteredQuestions.Add(licensureQuestions.ElementAt(1));
                        }
                       

                        var hospitalQuestions = questions.Where(l => l.QuestionCategoryId == 2).ToList();
                        if (hospitalQuestions.Count > 2)
                        {
                            filteredQuestions.Add(hospitalQuestions.ElementAt(0));
                            filteredQuestions.Add(hospitalQuestions.ElementAt(1));
                            filteredQuestions.Add(hospitalQuestions.ElementAt(2));
                        }
                        

                        var educationQuestions = questions.Where(l => l.QuestionCategoryId == 3).ToList();
                        if (educationQuestions.Count > 3)
                        {
                            filteredQuestions.Add(educationQuestions.ElementAt(0));
                            filteredQuestions.Add(educationQuestions.ElementAt(1));
                            filteredQuestions.Add(educationQuestions.ElementAt(2));
                            filteredQuestions.Add(educationQuestions.ElementAt(3));
                        }
                        

                        var deaQuestions = questions.Where(l => l.QuestionCategoryId == 4).ToList();
                        if (deaQuestions.Count > 0)
                        {
                            filteredQuestions.Add(deaQuestions.ElementAt(0));
                        }
                        

                        var medicareQuestions = questions.Where(l => l.QuestionCategoryId == 5).ToList();
                        if (medicareQuestions.Count > 0)
                        {
                            filteredQuestions.Add(medicareQuestions.ElementAt(0));
                        }
                        


                        var otherQuestions = questions.Where(l => l.QuestionCategoryId == 6).ToList();
                        if (otherQuestions.Count > 3)
                        {
                            filteredQuestions.Add(otherQuestions.ElementAt(0));
                            filteredQuestions.Add(otherQuestions.ElementAt(1));
                            filteredQuestions.Add(otherQuestions.ElementAt(2));
                            filteredQuestions.Add(otherQuestions.ElementAt(3));
                            filteredQuestions.Add(otherQuestions.ElementAt(4));
                        }
                        

                        var professionalQuestions = questions.Where(l => l.QuestionCategoryId == 7).ToList();
                        if (professionalQuestions.Count > 1)
                        {
                            filteredQuestions.Add(professionalQuestions.ElementAt(0));
                            filteredQuestions.Add(professionalQuestions.ElementAt(1));
                        }
                        

                        var malpracticeQuestions = questions.Where(l => l.QuestionCategoryId == 8).ToList();
                        if (malpracticeQuestions.Count > 0)
                        {
                            filteredQuestions.Add(malpracticeQuestions.ElementAt(0));
                        }
                        

                        var criminalQuestions = questions.Where(l => l.QuestionCategoryId == 9).ToList();
                        if (criminalQuestions.Count > 2)
                        {
                            filteredQuestions.Add(criminalQuestions.ElementAt(0));
                            filteredQuestions.Add(criminalQuestions.ElementAt(1));
                            filteredQuestions.Add(criminalQuestions.ElementAt(2));
                        }
                        

                        var abiityQuestions = questions.Where(l => l.QuestionCategoryId == 10).ToList();
                        if (abiityQuestions.Count > 3)
                        {
                            filteredQuestions.Add(abiityQuestions.ElementAt(0));
                            filteredQuestions.Add(abiityQuestions.ElementAt(1));
                            filteredQuestions.Add(abiityQuestions.ElementAt(2));
                            filteredQuestions.Add(abiityQuestions.ElementAt(3));
                        }
                        
                    }


                    List<ProfileDisclosureQuestionAnswer> answers = new List<ProfileDisclosureQuestionAnswer>();

                    if (profile.ProfileDisclosure.ProfileDisclosureQuestionAnswers.Count > 0)
                    {
                        foreach (var item in profile.ProfileDisclosure.ProfileDisclosureQuestionAnswers)
                        {
                            foreach (var item1 in filteredQuestions)
                            {
                                if (item != null && item1 !=null && item.QuestionID == item1.QuestionID)
                                {
                                    answers.Add(item);
                                }
                            }

                        }
                    }

                    List<int> questionNumber = new List<int>();
                    List<ProfileDisclosureQuestionAnswer> answerYes = new List<ProfileDisclosureQuestionAnswer>();

                    for (int i = 0; i < answers.Count; i++)
                    {
                        if (answers.ElementAt(i).ProviderDisclousreAnswer == AHC.CD.Entities.MasterData.Enums.YesNoOption.YES.ToString())
                        {
                            answerYes.Add(answers.ElementAt(i));
                            questionNumber.Add(i+1);
                        }
                    }

                    //var answerYes = answers.Where(q => q.ProviderDisclousreAnswer == AHC.CD.Entities.MasterData.Enums.YesNoOption.YES.ToString()).ToList();

                    for (int i = 0; i < answerYes.Count; i++)
                    {
                        PDFSupplementData_DisclosureQnsBusinessModel question = new PDFSupplementData_DisclosureQnsBusinessModel();

                        if (answerYes.Count > i)
                        {
                            question.QuestionCode1 = questionNumber.ElementAt(i);
                            question.QuestionExplanation1 = answerYes.Where(q => q.ProviderDisclousreAnswer == AHC.CD.Entities.MasterData.Enums.YesNoOption.YES.ToString()).ElementAt(i).Reason;

                        }
                        i++;
                        if (answerYes.Count > i)
                        {
                            question.QuestionCode2 = questionNumber.ElementAt(i);
                            question.QuestionExplanation2 = answerYes.ElementAt(i).Reason;

                        }
                        i++;
                        if (answerYes.Count > i)
                        {
                            question.QuestionCode3 = questionNumber.ElementAt(i);
                            question.QuestionExplanation3 = answerYes.ElementAt(i).Reason;
                        }

                        GeneratePdf pdfCall = new GeneratePdf();
                        string date = DateTime.Now.ToString("MM-dd-yyyy");

                        string pdfName = profile.PersonalDetail.FirstName + "_Disclosure" + "_" + i + "_" + date + ".pdf";
                        name = pdfCall.FillForm(question, pdfName, "Qn_new.pdf");
                        disclosureName.Add(name);
                    }
                }

                return disclosureName;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new PDFProfileDataGeneratorManagerException(ExceptionMessage.PROFILE_SUPLEMENT_DISCLOSURE_DATA_PDF_CREATION_EXCEPTION, ex);
            }
        }

        private List<string> ConstructPracticeInfoPDF(Profile profile)
        {
            var name = "";
            List<string> practiceName = new List<string>();

            try
            {
                var praticeInfo = profile.PracticeLocationDetails.Where(p => p.IsPrimary == AHC.CD.Entities.MasterData.Enums.YesNoOption.NO.ToString()).ToList();

                for (int i = 0; i < praticeInfo.Count; i++)
                {
                    if (praticeInfo.Count > i)
                    {
                        PDFSupplementData_PracticeLocBusinessModel practice = new PDFSupplementData_PracticeLocBusinessModel();

                        practice.SupplementCurrentlyPractisingAtThisAddress = praticeInfo.ElementAt(i).CurrentlyPracticingAtThisAddress.ToString();
                        practice.SupplementPractiseLocationStartDate = ConvertToDateString(praticeInfo.ElementAt(i).StartDate);
                        practice.SupplementPractiseLocationPhysicianGroup = praticeInfo.ElementAt(i).GroupName;
                        practice.SupplementPractiseLocationCorporateName = praticeInfo.ElementAt(i).PracticeLocationCorporateName;

                        if (praticeInfo.ElementAt(i).Facility != null)
                        {
                            practice.SupplementPractiseLocationStreet = praticeInfo.ElementAt(i).Facility.Street;
                            practice.SupplementPractiseLocationSuiteBuilding = praticeInfo.ElementAt(i).Facility.Building;
                            practice.SupplementPractiseLocationCity = praticeInfo.ElementAt(i).Facility.City;
                            practice.SupplementPractiseLocationState = praticeInfo.ElementAt(i).Facility.State;
                            practice.SupplementPractiseLocationZip = praticeInfo.ElementAt(i).Facility.ZipCode;
                            practice.SupplementPractiseLocationTelephone = praticeInfo.ElementAt(i).Facility.MobileNumber;
                            practice.SupplementPractiseLocationFax = praticeInfo.ElementAt(i).Facility.FaxNumber;
                            practice.SupplementPractiseLocationOfficialEmail = praticeInfo.ElementAt(i).Facility.EmailAddress;
                        }

                        practice.SupplementPractiseLocationSendGeneralCorrespondance = praticeInfo.ElementAt(i).GeneralCorrespondenceYesNoOption.ToString();
                        practice.SupplementPractiseLocationPrimaryTaxId = praticeInfo.ElementAt(i).PrimaryTaxId;

                        //Office Manager data
                        if (praticeInfo.ElementAt(i).BusinessOfficeManagerOrStaff != null)
                        {
                            practice.SupplementOfficeManagerLastName = praticeInfo.ElementAt(i).BusinessOfficeManagerOrStaff.LastName;
                            practice.SupplementOfficeManagerFirstName = praticeInfo.ElementAt(i).BusinessOfficeManagerOrStaff.FirstName;
                            practice.SupplementOfficeManagerTelephone = praticeInfo.ElementAt(i).BusinessOfficeManagerOrStaff.MobileNumber;
                            practice.SupplementOfficeManagerFax = praticeInfo.ElementAt(i).BusinessOfficeManagerOrStaff.FaxNumber;
                            practice.SupplementOfficeManagerEmail = praticeInfo.ElementAt(i).BusinessOfficeManagerOrStaff.EmailAddress;
                        }

                        //Billing Contact data
                        if (praticeInfo.ElementAt(i).BillingContactPerson != null)
                        {
                            practice.SupplementBillingContactLastName = praticeInfo.ElementAt(i).BillingContactPerson.LastName;
                            practice.SupplementBillingContactFirstName = praticeInfo.ElementAt(i).BillingContactPerson.FirstName;
                            practice.SupplementBillingContactStreet = praticeInfo.ElementAt(i).BillingContactPerson.Street;
                            practice.SupplementBillingContactSuiteBuilding = praticeInfo.ElementAt(i).BillingContactPerson.Building;
                            practice.SupplementBillingContactCity = praticeInfo.ElementAt(i).BillingContactPerson.City;
                            practice.SupplementBillingContactState = praticeInfo.ElementAt(i).BillingContactPerson.State;
                            practice.SupplementBillingContactZip = praticeInfo.ElementAt(i).BillingContactPerson.ZipCode;
                            practice.SupplementBillingContactTelephone = praticeInfo.ElementAt(i).BillingContactPerson.MobileNumber;
                            practice.SupplementBillingContactFax = praticeInfo.ElementAt(i).BillingContactPerson.FaxNumber;
                            practice.SupplementBillingContactEmail = praticeInfo.ElementAt(i).BillingContactPerson.EmailAddress;
                        }

                        //Payment Remittance data
                        if (praticeInfo.ElementAt(i).PaymentAndRemittance != null)
                        {
                            practice.SupplementPaymentRemittanceElectronicBillingCapabilities = praticeInfo.ElementAt(i).PaymentAndRemittance.ElectronicBillingCapability;
                            practice.SupplementPaymentRemittancBillingDepartment = praticeInfo.ElementAt(i).PaymentAndRemittance.BillingDepartment;
                            practice.SupplementPaymentRemittanceCheckPayableTo = praticeInfo.ElementAt(i).PaymentAndRemittance.CheckPayableTo;

                            if (praticeInfo.ElementAt(i).PaymentAndRemittance.PaymentAndRemittancePerson != null)
                            {
                                practice.SupplementPaymentRemittanceLastName = praticeInfo.ElementAt(i).PaymentAndRemittance.PaymentAndRemittancePerson.LastName;
                                practice.SupplementPaymentRemittanceFirstName = praticeInfo.ElementAt(i).PaymentAndRemittance.PaymentAndRemittancePerson.FirstName;
                                practice.SupplementPaymentRemittanceStreet = praticeInfo.ElementAt(i).PaymentAndRemittance.PaymentAndRemittancePerson.Street;
                                practice.SupplementPaymentRemittanceSuiteBuilding = praticeInfo.ElementAt(i).PaymentAndRemittance.PaymentAndRemittancePerson.Building;
                                practice.SupplementPaymentRemittanceCity = praticeInfo.ElementAt(i).PaymentAndRemittance.PaymentAndRemittancePerson.City;
                                practice.SupplementPaymentRemittanceState = praticeInfo.ElementAt(i).PaymentAndRemittance.PaymentAndRemittancePerson.State;
                                practice.SupplementPaymentRemittanceZip = praticeInfo.ElementAt(i).PaymentAndRemittance.PaymentAndRemittancePerson.ZipCode;
                                practice.SupplementPaymentRemittanceTelephone = praticeInfo.ElementAt(i).PaymentAndRemittance.PaymentAndRemittancePerson.MobileNumber;
                                practice.SupplementPaymentRemittanceFax = praticeInfo.ElementAt(i).PaymentAndRemittance.PaymentAndRemittancePerson.FaxNumber;
                                practice.SupplementPaymentRemittanceEmail = praticeInfo.ElementAt(i).PaymentAndRemittance.PaymentAndRemittancePerson.EmailAddress;
                            }

                        }

                        //OfficeHours data
                        if (praticeInfo.ElementAt(i).OfficeHour != null)
                        {
                            practice.SupplementPhoneCoverage = praticeInfo.ElementAt(i).OfficeHour.AnyTimePhoneCoverage;
                            practice.SupplementTypeOfAnsweringService = praticeInfo.ElementAt(i).OfficeHour.AnsweringService;
                            practice.SupplementAfterOfficeHoursOfficeTelephone = praticeInfo.ElementAt(i).OfficeHour.AfterHoursTelephoneNumber;

                            if (praticeInfo.ElementAt(i).OfficeHour.PracticeDays.Count > 0 && praticeInfo.ElementAt(i).OfficeHour.PracticeDays.First().DailyHours.Count > 0)
                            {
                                practice.SupplementStartMonday = praticeInfo.ElementAt(i).OfficeHour.PracticeDays.ElementAt(0).DailyHours.First().StartTime;
                                practice.SupplementEndMonday = praticeInfo.ElementAt(i).OfficeHour.PracticeDays.ElementAt(0).DailyHours.First().EndTime;
                                practice.SupplementStartTuesday = praticeInfo.ElementAt(i).OfficeHour.PracticeDays.ElementAt(1).DailyHours.First().StartTime;
                                practice.SupplementEndTuesday = praticeInfo.ElementAt(i).OfficeHour.PracticeDays.ElementAt(1).DailyHours.First().EndTime;
                                practice.SupplementStartWednesday = praticeInfo.ElementAt(i).OfficeHour.PracticeDays.ElementAt(2).DailyHours.First().StartTime;
                                practice.SupplementEndWednesday = praticeInfo.ElementAt(i).OfficeHour.PracticeDays.ElementAt(2).DailyHours.First().EndTime;
                                practice.SupplementStartThursday = praticeInfo.ElementAt(i).OfficeHour.PracticeDays.ElementAt(3).DailyHours.First().StartTime;
                                practice.SupplementEndThursday = praticeInfo.ElementAt(i).OfficeHour.PracticeDays.ElementAt(3).DailyHours.First().EndTime;
                                practice.SupplementStartFriday = praticeInfo.ElementAt(i).OfficeHour.PracticeDays.ElementAt(4).DailyHours.First().StartTime;
                                practice.SupplementEndFriday = praticeInfo.ElementAt(i).OfficeHour.PracticeDays.ElementAt(4).DailyHours.First().EndTime;
                                practice.SupplementStartSaturday = praticeInfo.ElementAt(i).OfficeHour.PracticeDays.ElementAt(5).DailyHours.First().StartTime;
                                practice.SupplementEndSaturday = praticeInfo.ElementAt(i).OfficeHour.PracticeDays.ElementAt(5).DailyHours.First().EndTime;
                                practice.SupplementStartSunday = praticeInfo.ElementAt(i).OfficeHour.PracticeDays.ElementAt(6).DailyHours.First().StartTime;
                                practice.SupplementEndSunday = praticeInfo.ElementAt(i).OfficeHour.PracticeDays.ElementAt(6).DailyHours.First().EndTime;
                            }

                        }

                        //OpenPractiseStatus data
                        if (praticeInfo.ElementAt(i).OpenPracticeStatus != null)
                        {

                            practice.SupplementOpenPractiseStatusExplain = praticeInfo.ElementAt(i).OpenPracticeStatus.AnyInformationVariesByPlan;
                            practice.SupplementOpenPractiseStatusPRACTICELIMITATIONS = praticeInfo.ElementAt(i).OpenPracticeStatus.AnyPracticeLimitation;
                            practice.SupplementOpenPractiseStatusGENDERLIMITATIONS = praticeInfo.ElementAt(i).OpenPracticeStatus.GenderLimitation;
                            practice.SupplementOpenPractiseStatusMINIMUMAGELIMITATIONS = praticeInfo.ElementAt(i).OpenPracticeStatus.MinimumAge;
                            practice.SupplementOpenPractiseStatusMAXIMUMAGELIMITATIONS = praticeInfo.ElementAt(i).OpenPracticeStatus.MaximumAge;
                            practice.SupplementOpenPractiseStatusOtherLIMITATIONS = praticeInfo.ElementAt(i).OpenPracticeStatus.OtherLimitations;

                            if (praticeInfo.ElementAt(i).OpenPracticeStatus.PracticeQuestionAnswers.Count > 0)
                            {
                                practice.SupplementOpenPractiseStatusACCEPTNEWPATIENTSINTOTHISPRACTICE = praticeInfo.ElementAt(i).OpenPracticeStatus.PracticeQuestionAnswers.ElementAt(0).Answer;
                                practice.SupplementOpenPractiseStatusACCEPTEXISTINGPATIENTSWITHCHANGEOFPAYOR = praticeInfo.ElementAt(i).OpenPracticeStatus.PracticeQuestionAnswers.ElementAt(1).Answer;
                                practice.SupplementOpenPractiseStatusACCEPTNEWPATIENTSWITHPHYSICIANREFERRAL = praticeInfo.ElementAt(i).OpenPracticeStatus.PracticeQuestionAnswers.ElementAt(2).Answer;
                                practice.SupplementOpenPractiseStatusACCEPTALLNEWPATIENTS = praticeInfo.ElementAt(i).OpenPracticeStatus.PracticeQuestionAnswers.ElementAt(3).Answer;
                                practice.SupplementOpenPractiseStatusACCEPTNEWMEDICAREPATIENTS = praticeInfo.ElementAt(i).OpenPracticeStatus.PracticeQuestionAnswers.ElementAt(4).Answer;
                                practice.SupplementOpenPractiseStatusACCEPTNEWMEDICAIDPATIENTS = praticeInfo.ElementAt(i).OpenPracticeStatus.PracticeQuestionAnswers.ElementAt(5).Answer;
                            }

                        }

                        //Mid-Level Practitioner data              

                        var midLevels = praticeInfo.ElementAt(i).PracticeProviders.Where(m => m.Practice == AHC.CD.Entities.MasterData.Enums.PracticeType.Midlevel.ToString()).ToList();

                        for (int m = 0; m < midLevels.Count; m++)
                        {
                            if (midLevels.Count > m)
                            {
                                practice.SupplementMidLevelPractitionerLastName1 = midLevels.ElementAt(m).LastName;
                                practice.SupplementMidLevelPractitionerFirstName1 = midLevels.ElementAt(m).FirstName;

                            }
                            m++;
                            if (midLevels.Count > m)
                            {
                                practice.SupplementMidLevelPractitionerFirstName2 = midLevels.ElementAt(m).LastName;
                                practice.SupplementMidLevelPractitionerFirstName2 = midLevels.ElementAt(m).FirstName;

                            }
                            m++;
                            if (midLevels.Count > m)
                            {
                                practice.SupplementMidLevelPractitionerFirstName3 = midLevels.ElementAt(m).LastName;
                                practice.SupplementMidLevelPractitionerFirstName3 = midLevels.ElementAt(m).FirstName;

                            }
                            m++;
                            if (midLevels.Count > m)
                            {
                                practice.SupplementMidLevelPractitionerFirstName4 = midLevels.ElementAt(m).LastName;
                                practice.SupplementMidLevelPractitionerFirstName4 = midLevels.ElementAt(m).FirstName;

                            }
                            m++;
                            if (midLevels.Count > m)
                            {
                                practice.SupplementMidLevelPractitionerFirstName5 = midLevels.ElementAt(m).LastName;
                                practice.SupplementMidLevelPractitionerFirstName5 = midLevels.ElementAt(m).FirstName;

                            }

                        }


                        //Languages data                 

                        if (praticeInfo.ElementAt(i).Facility != null && praticeInfo.ElementAt(i).Facility.FacilityDetail != null && praticeInfo.ElementAt(i).Facility.FacilityDetail.Language != null && praticeInfo.ElementAt(i).Facility.FacilityDetail.Language.NonEnglishLanguages.Count > 0)
                        {
                            if (praticeInfo.ElementAt(i).Facility.FacilityDetail.Language.NonEnglishLanguages.Count > 0)
                                practice.SupplementNonEnglishLanguage1 = praticeInfo.ElementAt(i).Facility.FacilityDetail.Language.NonEnglishLanguages.First().Language;


                            if (praticeInfo.ElementAt(i).Facility.FacilityDetail.Language.NonEnglishLanguages.Count > 1)
                                practice.SupplementNonEnglishLanguage2 = praticeInfo.ElementAt(i).Facility.FacilityDetail.Language.NonEnglishLanguages.ElementAt(1).Language;

                            if (praticeInfo.ElementAt(i).Facility.FacilityDetail.Language.NonEnglishLanguages.Count > 2)
                                practice.SupplementNonEnglishLanguage3 = praticeInfo.ElementAt(i).Facility.FacilityDetail.Language.NonEnglishLanguages.ElementAt(2).Language;

                            if (praticeInfo.ElementAt(i).Facility.FacilityDetail.Language.NonEnglishLanguages.Count > 3)
                                practice.SupplementNonEnglishLanguage4 = praticeInfo.ElementAt(i).Facility.FacilityDetail.Language.NonEnglishLanguages.ElementAt(3).Language;

                            if (praticeInfo.ElementAt(i).Facility.FacilityDetail.Language.NonEnglishLanguages.Count > 4)
                                practice.SupplementNonEnglishLanguage5 = praticeInfo.ElementAt(i).Facility.FacilityDetail.Language.NonEnglishLanguages.ElementAt(4).Language;

                            if (praticeInfo.ElementAt(i).Facility.FacilityDetail.Language.NonEnglishLanguages.Count > 0 && praticeInfo.ElementAt(i).Facility.FacilityDetail.Language.NonEnglishLanguages.First().InterpretersAvailableYesNoOption.Equals(YesNoOption.YES))
                                practice.SupplementLanguagesInterpreted1 = praticeInfo.ElementAt(i).Facility.FacilityDetail.Language.NonEnglishLanguages.First().Language;

                            if (praticeInfo.ElementAt(i).Facility.FacilityDetail.Language.NonEnglishLanguages.Count > 1 && praticeInfo.ElementAt(i).Facility.FacilityDetail.Language.NonEnglishLanguages.ElementAt(1).InterpretersAvailableYesNoOption.Equals(YesNoOption.YES))
                                practice.SupplementLanguagesInterpreted2 = praticeInfo.ElementAt(i).Facility.FacilityDetail.Language.NonEnglishLanguages.ElementAt(1).Language;

                            if (praticeInfo.ElementAt(i).Facility.FacilityDetail.Language.NonEnglishLanguages.Count > 2 && praticeInfo.ElementAt(i).Facility.FacilityDetail.Language.NonEnglishLanguages.ElementAt(2).InterpretersAvailableYesNoOption.Equals(YesNoOption.YES))
                                practice.SupplementLanguagesInterpreted3 = praticeInfo.ElementAt(i).Facility.FacilityDetail.Language.NonEnglishLanguages.ElementAt(2).Language;

                            if (praticeInfo.ElementAt(i).Facility.FacilityDetail.Language.NonEnglishLanguages.Count > 3 && praticeInfo.ElementAt(i).Facility.FacilityDetail.Language.NonEnglishLanguages.ElementAt(3).InterpretersAvailableYesNoOption.Equals(YesNoOption.YES))
                                practice.SupplementLanguagesInterpreted4 = praticeInfo.ElementAt(i).Facility.FacilityDetail.Language.NonEnglishLanguages.ElementAt(3).Language;

                        }

                        //Accessibilities data
                        if (praticeInfo.ElementAt(i).Facility != null && praticeInfo.ElementAt(i).Facility.FacilityDetail != null && praticeInfo.ElementAt(i).Facility.FacilityDetail.Accessibility != null)
                        {
                            if (praticeInfo.ElementAt(i).Facility.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.Count > 0)
                            {
                                practice.SupplementADAREQUIREMENTS = praticeInfo.ElementAt(i).Facility.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(0).Answer;
                                practice.SupplementHANDICAPPEDACCESSForBuilding = praticeInfo.ElementAt(i).Facility.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(1).Answer;
                                practice.SupplementHANDICAPPEDACCESSForPARKING = praticeInfo.ElementAt(i).Facility.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(2).Answer;
                                practice.SupplementHANDICAPPEDACCESSForRESTROOM = praticeInfo.ElementAt(i).Facility.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(3).Answer;
                                practice.SupplementOTHERSERVICESForDISABLED = praticeInfo.ElementAt(i).Facility.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(4).Answer;
                                practice.SupplementTextTELEPHONY = praticeInfo.ElementAt(i).Facility.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(5).Answer;
                                practice.SupplementMENTALPhysicalIMPAIRMENTSERVICES = praticeInfo.ElementAt(i).Facility.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(6).Answer;
                                practice.SupplementAccessibleByPublicTransport = praticeInfo.ElementAt(i).Facility.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(7).Answer;
                                practice.SupplementBus = praticeInfo.ElementAt(i).Facility.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(8).Answer;
                                practice.SupplementSubway = praticeInfo.ElementAt(i).Facility.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(9).Answer;
                                practice.SupplementRegionalTrain = praticeInfo.ElementAt(i).Facility.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(10).Answer;
                            }

                            if (praticeInfo.ElementAt(i).Facility.FacilityDetail.Language != null)
                            {
                                practice.SupplementAMERICANSIGNLANGUAGE = praticeInfo.ElementAt(i).Facility.FacilityDetail.Language.AmericanSignLanguage;
                            }

                            practice.SupplementOtherHANDICAPPEDAccess = praticeInfo.ElementAt(i).Facility.FacilityDetail.Accessibility.OtherHandicappedAccess;
                            practice.SupplementOtherDisabilityServices = praticeInfo.ElementAt(i).Facility.FacilityDetail.Accessibility.OtherDisabilityServices;
                            practice.SupplementOtherTransportationAccess = praticeInfo.ElementAt(i).Facility.FacilityDetail.Accessibility.OtherTransportationAccess;

                        }

                        //Services data
                        if (praticeInfo.ElementAt(i).Facility != null && praticeInfo.ElementAt(i).Facility.FacilityDetail != null && praticeInfo.ElementAt(i).Facility.FacilityDetail.Service != null)
                        {
                            practice.SupplementLaboratoryServices = praticeInfo.ElementAt(i).Facility.FacilityDetail.Service.LaboratoryServices;
                            practice.SupplementLaboratoryServicesAccreditingProgram = praticeInfo.ElementAt(i).Facility.FacilityDetail.Service.LaboratoryAccreditingCertifyingProgram;
                            practice.SupplementRadiologyServices = praticeInfo.ElementAt(i).Facility.FacilityDetail.Service.RadiologyServices;
                            practice.SupplementRadiologyServicesXrayCertificationType = praticeInfo.ElementAt(i).Facility.FacilityDetail.Service.RadiologyXRayCertificateType;
                            practice.SupplementAnesthesiaAdministered = praticeInfo.ElementAt(i).Facility.FacilityDetail.Service.IsAnesthesiaAdministered;
                            practice.SupplementClassCategory = praticeInfo.ElementAt(i).Facility.FacilityDetail.Service.AnesthesiaCategory;
                            practice.SupplementAdministerLastName = praticeInfo.ElementAt(i).Facility.FacilityDetail.Service.AnesthesiaAdminLastName;
                            practice.SupplementAdministerFirstName = praticeInfo.ElementAt(i).Facility.FacilityDetail.Service.AnesthesiaAdminFirstName;
                            practice.SupplementAdditionalOfficeProcedures = praticeInfo.ElementAt(i).Facility.FacilityDetail.Service.AdditionalOfficeProcedures;

                            if (praticeInfo.ElementAt(i).Facility.FacilityDetail.Service.PracticeType != null)
                            {
                                practice.SupplementPractiseType = praticeInfo.ElementAt(i).Facility.FacilityDetail.Service.PracticeType.Title;
                            }


                            if (praticeInfo.ElementAt(i).Facility.FacilityDetail.Service.FacilityServiceQuestionAnswers.Count > 0)
                            {
                                var serviceCount = 0;

                                foreach (var service in praticeInfo.ElementAt(i).Facility.FacilityDetail.Service.FacilityServiceQuestionAnswers)
                                {
                                    if (service != null && serviceCount < 1)
                                    {
                                        practice.SupplementEKGS = praticeInfo.ElementAt(i).Facility.FacilityDetail.Service.FacilityServiceQuestionAnswers.ElementAt(0).Answer;
                                        practice.SupplementAllergyInjections = praticeInfo.ElementAt(i).Facility.FacilityDetail.Service.FacilityServiceQuestionAnswers.ElementAt(1).Answer;
                                        practice.SupplementAllergySkinTesting = praticeInfo.ElementAt(i).Facility.FacilityDetail.Service.FacilityServiceQuestionAnswers.ElementAt(2).Answer;
                                        practice.SupplementRoutineOfficeGynecology = praticeInfo.ElementAt(i).Facility.FacilityDetail.Service.FacilityServiceQuestionAnswers.ElementAt(3).Answer;
                                        practice.SupplementDrawingBlood = praticeInfo.ElementAt(i).Facility.FacilityDetail.Service.FacilityServiceQuestionAnswers.ElementAt(4).Answer;
                                        practice.SupplementAgeImmunizations = praticeInfo.ElementAt(i).Facility.FacilityDetail.Service.FacilityServiceQuestionAnswers.ElementAt(5).Answer;
                                        practice.SupplementFlexibleSIGMOIDOSCOPY = praticeInfo.ElementAt(i).Facility.FacilityDetail.Service.FacilityServiceQuestionAnswers.ElementAt(6).Answer;
                                        practice.SupplementTYMPANOMETRYAUDIOMETRYSCREENING = praticeInfo.ElementAt(i).Facility.FacilityDetail.Service.FacilityServiceQuestionAnswers.ElementAt(7).Answer;
                                        practice.SupplementASTHMATreatment = praticeInfo.ElementAt(i).Facility.FacilityDetail.Service.FacilityServiceQuestionAnswers.ElementAt(8).Answer;
                                        practice.SupplementOSTEOPATHICManipulation = praticeInfo.ElementAt(i).Facility.FacilityDetail.Service.FacilityServiceQuestionAnswers.ElementAt(9).Answer;
                                        practice.SupplementIVHYDRATIONTREATMENT = praticeInfo.ElementAt(i).Facility.FacilityDetail.Service.FacilityServiceQuestionAnswers.ElementAt(10).Answer;
                                        practice.SupplementCARDIACSTRESSTEST = praticeInfo.ElementAt(i).Facility.FacilityDetail.Service.FacilityServiceQuestionAnswers.ElementAt(11).Answer;
                                        practice.SupplementPULMONARYFUNCTIONTESTING = praticeInfo.ElementAt(i).Facility.FacilityDetail.Service.FacilityServiceQuestionAnswers.ElementAt(12).Answer;
                                        practice.SupplementPhysicalTheraphy = praticeInfo.ElementAt(i).Facility.FacilityDetail.Service.FacilityServiceQuestionAnswers.ElementAt(13).Answer;
                                        practice.SupplementMinorLaseractions = praticeInfo.ElementAt(i).Facility.FacilityDetail.Service.FacilityServiceQuestionAnswers.ElementAt(14).Answer;
                                        serviceCount = 1;
                                    }


                                }
                            }

                        }

                        //Covering colleagues data
                        if (praticeInfo.ElementAt(i).PracticeProviders.Count > 0)
                        {
                            var coveringColleagues1 = praticeInfo.ElementAt(i).PracticeProviders.Where(p => p.Practice == AHC.CD.Entities.MasterData.Enums.PracticeType.CoveringColleague.ToString() && p.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();

                            for (int j = 0; j < coveringColleagues1.Count; j++)
                            {
                                if (coveringColleagues1.Count > j)
                                {
                                    practice.SupplementCoveringColleguesLastName1 = coveringColleagues1.ElementAt(j).LastName;
                                    practice.SupplementCoveringColleguesFirstName1 = coveringColleagues1.ElementAt(j).FirstName;

                                    if (coveringColleagues1.ElementAt(j).PracticeProviderSpecialties.Count > 0)
                                        practice.SupplementCoveringColleguesSpecialtyCode1 = coveringColleagues1.ElementAt(j).PracticeProviderSpecialties.First().Specialty.Name;

                                    if (coveringColleagues1.ElementAt(j).PracticeProviderTypes.Count > 0)
                                        practice.SupplementCoveringColleguesProviderType1 = coveringColleagues1.ElementAt(j).PracticeProviderTypes.First().ProviderType.Title;

                                }
                                j++;
                                if (coveringColleagues1.Count > j)
                                {
                                    practice.SupplementCoveringColleguesLastName2 = coveringColleagues1.ElementAt(j).LastName;
                                    practice.SupplementCoveringColleguesFirstName2 = coveringColleagues1.ElementAt(j).FirstName;

                                    if (coveringColleagues1.ElementAt(j).PracticeProviderSpecialties.Count > 0)
                                        practice.SupplementCoveringColleguesSpecialtyCode2 = coveringColleagues1.ElementAt(j).PracticeProviderSpecialties.First().Specialty.Name;

                                    if (coveringColleagues1.ElementAt(j).PracticeProviderTypes.Count > 0)
                                        practice.SupplementCoveringColleguesProviderType2 = coveringColleagues1.ElementAt(j).PracticeProviderTypes.First().ProviderType.Title;

                                }
                                j++;
                                if (coveringColleagues1.Count > j)
                                {
                                    practice.SupplementCoveringColleguesLastName3 = coveringColleagues1.ElementAt(j).LastName;
                                    practice.SupplementCoveringColleguesFirstName3 = coveringColleagues1.ElementAt(j).FirstName;

                                    if (coveringColleagues1.ElementAt(j).PracticeProviderSpecialties.Count > 0)
                                        practice.SupplementCoveringColleguesSpecialtyCode3 = coveringColleagues1.ElementAt(j).PracticeProviderSpecialties.First().Specialty.Name;

                                    if (coveringColleagues1.ElementAt(j).PracticeProviderTypes.Count > 0)
                                        practice.SupplementCoveringColleguesProviderType3 = coveringColleagues1.ElementAt(j).PracticeProviderTypes.First().ProviderType.Title;

                                }
                                j++;
                                if (coveringColleagues1.Count > j)
                                {
                                    practice.SupplementCoveringColleguesLastName4 = coveringColleagues1.ElementAt(j).LastName;
                                    practice.SupplementCoveringColleguesFirstName4 = coveringColleagues1.ElementAt(j).FirstName;

                                    if (coveringColleagues1.ElementAt(j).PracticeProviderSpecialties.Count > 0)
                                        practice.SupplementCoveringColleguesSpecialtyCode4 = coveringColleagues1.ElementAt(j).PracticeProviderSpecialties.First().Specialty.Name;

                                    if (coveringColleagues1.ElementAt(j).PracticeProviderTypes.Count > 0)
                                        practice.SupplementCoveringColleguesProviderType4 = coveringColleagues1.ElementAt(j).PracticeProviderTypes.First().ProviderType.Title;

                                }
                            }

                        }

                        GeneratePdf pdfCall = new GeneratePdf();
                        string date = DateTime.Now.ToString("MM-dd-yyyy");

                        string pdfName = profile.PersonalDetail.FirstName + "_Practice" + "_" + i + "_" + date + ".pdf";
                        name = pdfCall.FillForm(practice, pdfName, "PLoc_new.pdf");
                        practiceName.Add(name);
                    }
                }

                return practiceName;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new PDFProfileDataGeneratorManagerException(ExceptionMessage.PROFILE_SUPLEMENT_PRACTICE_DATA_PDF_CREATION_EXCEPTION, ex);
            }
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


           

