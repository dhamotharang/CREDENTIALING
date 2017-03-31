﻿using AHC.CD.Business.BusinessModels.DelegationProfileReport;
using AHC.CD.Data.Repository;
using AHC.CD.Data.Repository.Profiles;
using AHC.CD.Entities.Credentialing.DelegationProfileReport;
using AHC.CD.Entities.Credentialing.Loading;
using AHC.CD.Entities.Credentialing.LoadingInformation;
using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterProfile;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Credentialing.DelegationProfileReport
{
    internal class DelegationProfileReportManager : IDelegationProfileReportManager
    {
        private IProfileRepository profileRepository = null;
        private IUnitOfWork uow = null;

        public DelegationProfileReportManager(IUnitOfWork uow)
        {
            this.profileRepository = uow.GetProfileRepository();
            this.uow = uow;
        }

        public async Task<ProviderGeneralInfoBussinessModel> GetProfileDataByIdAsync(int profileId, List<ProviderPracitceInfoBusinessModel> locations, List<ProviderProfessionalDetailBusinessModel> specialtis)
        {
            try
            {
                ProviderGeneralInfoBussinessModel providerProfile = new ProviderGeneralInfoBussinessModel();

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

                    //State License
                    "StateLicenses.ProviderType",
                    "StateLicenses.StateLicenseStatus",

                    //Personal Detail                    
                    "PersonalDetail.ProviderLevel",
                    "PersonalDetail.ProviderTitles.ProviderType",

                    //PersonalIdentification
                    "PersonalIdentification",
                    
                    //Languages
                    "LanguageInfo.KnownLanguages",  
                  
                    //BirthInformation
                    "BirthInformation",

                    
                    //MedicareInformations
                    "MedicareInformations",

                    //MedicaidInformations
                    "MedicaidInformations",

                    //FederalDEAInformations
                    "FederalDEAInformations",

                    //Resindency Internship
                    "TrainingDetails.ResidencyInternshipDetails.Specialty",
                    "ProgramDetails.Specialty",
                    
                    "PracticeLocationDetails.Facility",
                    "PracticeLocationDetails.Facility.FacilityDetail",

                    "PracticeLocationDetails.OfficeHour",
                    "PracticeLocationDetails.OfficeHour.PracticeDays",
                    "PracticeLocationDetails.OfficeHour.PracticeDays.DailyHours",
                    
                    "PracticeLocationDetails.OpenPracticeStatus",
                    "PracticeLocationDetails.OpenPracticeStatus.PracticeQuestionAnswers",

                    "PracticeLocationDetails.BillingContactPerson",                    
                    "PracticeLocationDetails.PracticeProviders",

                    
                    
                };

                var profile = await profileRepository.FindAsync(p => p.ProfileID == profileId, includeProperties);

                if (profile.StateLicenses.Count > 0)
                    profile.StateLicenses = profile.StateLicenses.Where(s => (s.Status != null && s.Status != StatusType.Inactive.ToString())).ToList();

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


                if (profile.FederalDEAInformations.Count > 0)
                    profile.FederalDEAInformations = profile.FederalDEAInformations.Where(c => c.Status != null && !c.Status.Equals(StatusType.Inactive.ToString())).ToList();



                providerProfile = ConstructDelegationProfileReport(profile, locations, specialtis);

                return providerProfile;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private ProviderGeneralInfoBussinessModel ConstructDelegationProfileReport(Profile profile, List<ProviderPracitceInfoBusinessModel> locations, List<ProviderProfessionalDetailBusinessModel> specialtis)
        {
            ProviderGeneralInfoBussinessModel providerProfile = new ProviderGeneralInfoBussinessModel();


            if (profile.PersonalDetail != null)
            {
                if (profile.PersonalDetail.MiddleName != null)
                    providerProfile.ProviderName = profile.PersonalDetail.FirstName + " " + profile.PersonalDetail.MiddleName + " " + profile.PersonalDetail.LastName;
                else
                    providerProfile.ProviderName = profile.PersonalDetail.FirstName + " " + profile.PersonalDetail.LastName;

                providerProfile.Gender = profile.PersonalDetail.Gender;

            }

            if (profile.BirthInformation != null)
            {
                DateTime birth = Convert.ToDateTime(profile.BirthInformation.DateOfBirth, CultureInfo.InvariantCulture);

                string birth2 = birth.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                providerProfile.DOB = birth2;
            }

            if (profile.PersonalIdentification != null)
            {
                providerProfile.SSN = profile.PersonalIdentification.SSN;
            }

            List<string> languages = new List<string>();

            if (profile.LanguageInfo != null && profile.LanguageInfo.KnownLanguages.Count > 0)
            {
                languages.Add("English");
                foreach (var lang in profile.LanguageInfo.KnownLanguages)
                {
                    languages.Add(lang.Language);
                }
                providerProfile.Languages = languages;
            }
            else
            {
                languages.Add("English");
                providerProfile.Languages = languages;
            }

            if (profile.FederalDEAInformations.Count > 0)
            {
                List<string> Dea = new List<string>();
                foreach (var dea in profile.FederalDEAInformations)
                {
                    Dea.Add(dea.DEANumber);
                }
                providerProfile.DEA = Dea;
            }

            if (profile.StateLicenses.Count > 0)
            {
                List<string> Medicallicense = new List<string>();
                foreach (var license in profile.StateLicenses)
                {
                    Medicallicense.Add(license.LicenseNumber);
                }
                providerProfile.MedicalLicense = Medicallicense;
            }

            if (profile.OtherIdentificationNumber != null)
            {
                providerProfile.NPINumber = profile.OtherIdentificationNumber.NPINumber;
            }

            List<ProviderMedicalEducationBusinessModel> ProviderMedicalEducations = new List<ProviderMedicalEducationBusinessModel>();

            if (profile.EducationDetails.Count > 0 && profile.EducationDetails.Any(p => p.QualificationType == AHC.CD.Entities.MasterData.Enums.EducationQualificationType.Graduate.ToString()))
            {
                foreach (var graduate in profile.EducationDetails)
                {
                    if (graduate != null && graduate.QualificationType == AHC.CD.Entities.MasterData.Enums.EducationQualificationType.Graduate.ToString())
                    {
                        ProviderMedicalEducationBusinessModel education = new ProviderMedicalEducationBusinessModel();
                        if (graduate.SchoolInformation != null)
                        {
                            education.SchoolName = graduate.SchoolInformation.SchoolName;
                            education.DegreeEarned = graduate.QualificationDegree;

                            string startDate = ConvertToDateString(graduate.StartDate);
                            string endDate = ConvertToDateString(graduate.EndDate);

                            education.YearsAttended = startDate + " - " + endDate;
                            ProviderMedicalEducations.Add(education);
                        }

                    }
                }
            }
            else
            {
                foreach (var graduate in profile.EducationDetails)
                {
                    if (graduate != null && graduate.QualificationType == AHC.CD.Entities.MasterData.Enums.EducationQualificationType.UnderGraduate.ToString())
                    {
                        ProviderMedicalEducationBusinessModel education = new ProviderMedicalEducationBusinessModel();
                        if (graduate.SchoolInformation != null)
                        {
                            education.SchoolName = graduate.SchoolInformation.SchoolName;
                            education.DegreeEarned = graduate.QualificationDegree;

                            string startDate = ConvertToDateString(graduate.StartDate);
                            string endDate = ConvertToDateString(graduate.EndDate);

                            education.YearsAttended = startDate + " - " + endDate;
                            ProviderMedicalEducations.Add(education);
                        }

                    }
                }
            }

            providerProfile.ProviderMedicalEducations = ProviderMedicalEducations;

            if (profile.ProgramDetails.Count > 0)
            {
                List<ProviderProgramDetailBusinessModel> providerProgramDetails = new List<ProviderProgramDetailBusinessModel>();
                foreach (var program in profile.ProgramDetails)
                {
                    ProviderProgramDetailBusinessModel programDetail = new ProviderProgramDetailBusinessModel();
                    if (program.SchoolInformation != null)
                    {
                        programDetail.Facility = program.SchoolInformation.SchoolName;
                    }
                    if (program.Specialty != null)
                    {
                        programDetail.Specialty = program.Specialty.Name;
                    }
                    programDetail.ProgramName = program.ProgramType;

                    string startDate = ConvertToDateString(program.StartDate);
                    string endDate = ConvertToDateString(program.EndDate);
                    programDetail.FromToDate = startDate + " - " + endDate;

                    providerProgramDetails.Add(programDetail);

                }
                providerProfile.ProviderProgramDetails = providerProgramDetails;
            }

            //if (profile.SpecialtyDetails.Count > 0)
            //{
            //    List<ProviderProfessionalDetailBusinessModel> providerProfessionalDetail = new List<ProviderProfessionalDetailBusinessModel>();
            //    foreach (var specialty in profile.SpecialtyDetails)
            //    {

            //        if (specialty != null && specialty.SpecialtyPreference == AHC.CD.Entities.MasterData.Enums.PreferenceType.Primary.ToString())
            //        {
            //            ProviderProfessionalDetailBusinessModel professional = new ProviderProfessionalDetailBusinessModel();
            //            professional.Preference = specialty.SpecialtyPreference;
            //            if (specialty.Specialty != null)
            //            {
            //                professional.Specialty = specialty.Specialty.Name;                                                    

            //            }
            //            professional.BoardCertified = specialty.IsBoardCertified;  

            //            if (specialty.SpecialtyBoardCertifiedDetail != null)
            //            {
            //                string startDate = ConvertToDateString(specialty.SpecialtyBoardCertifiedDetail.InitialCertificationDate);
            //                string endDate = ConvertToDateString(specialty.SpecialtyBoardCertifiedDetail.ExpirationDate);

            //                if (endDate == null)
            //                    professional.SpecialtyEffectiveDates = null;
            //                else
            //                    professional.SpecialtyEffectiveDates = startDate + " - " + endDate;

            //                professional.BoardName = specialty.SpecialtyBoardCertifiedDetail.SpecialtyBoard.Name;
            //            }
            //            providerProfessionalDetail.Add(professional);
            //            break;
            //        }                    

            //    }

            //    foreach (var specialty in profile.SpecialtyDetails)
            //    {
            //        if (specialty != null && specialty.SpecialtyPreference == AHC.CD.Entities.MasterData.Enums.PreferenceType.Secondary.ToString())
            //        {
            //            ProviderProfessionalDetailBusinessModel professional = new ProviderProfessionalDetailBusinessModel();
            //            professional.Preference = specialty.SpecialtyPreference;

            //            if (specialty.Specialty != null)
            //            {
            //                professional.Specialty = specialty.Specialty.Name;
            //            }
            //            professional.BoardCertified = specialty.IsBoardCertified;
            //            if (specialty.SpecialtyBoardCertifiedDetail != null)
            //            {
            //                string startDate = ConvertToDateString(specialty.SpecialtyBoardCertifiedDetail.InitialCertificationDate);
            //                string endDate = ConvertToDateString(specialty.SpecialtyBoardCertifiedDetail.ExpirationDate);
            //                professional.SpecialtyEffectiveDates = startDate + " - " + endDate;
            //            }
            //            providerProfessionalDetail.Add(professional);
            //        }
            //    }
            //    providerProfile.ProviderProfessionalDetails = providerProfessionalDetail;
            //}

            List<ProviderProfessionalDetailBusinessModel> providerProfessionalDetails = new List<ProviderProfessionalDetailBusinessModel>();
            if (specialtis.Count > 0)
            {
                for (int i = 0; i < specialtis.Count; i++)
                {

                    if (specialtis.ElementAt(i) != null)
                    {
                        ProviderProfessionalDetailBusinessModel professional = new ProviderProfessionalDetailBusinessModel();
                        professional.Preference = specialtis.ElementAt(i).Preference;
                        if (specialtis.ElementAt(i).Specialty != null)
                        {
                            professional.Specialty = specialtis.ElementAt(i).Specialty;

                        }
                        professional.BoardCertified = specialtis.ElementAt(i).BoardCertified;
                        professional.BoardName = specialtis.ElementAt(i).BoardName;

                        professional.SpecialtyEffectiveDates = specialtis.ElementAt(i).SpecialtyEffectiveDates;
                        providerProfessionalDetails.Add(professional);

                    }

                    //i++;
                    //if (specialtis.Count > 1 && specialtis.ElementAt(i) != null)
                    //{
                    //    ProviderProfessionalDetailBusinessModel professional = new ProviderProfessionalDetailBusinessModel();
                    //    professional.Preference = specialtis.ElementAt(i).Preference;
                    //    if (specialtis.ElementAt(i).Specialty != null)
                    //    {
                    //        professional.Specialty = specialtis.ElementAt(i).Specialty;

                    //    }
                    //    professional.BoardCertified = specialtis.ElementAt(i).BoardCertified;

                    //    professional.SpecialtyEffectiveDates = specialtis.ElementAt(i).SpecialtyEffectiveDates;
                    //    providerProfile.ProviderProfessionalDetails = providerProfessionalDetails;
                    //    providerProfile.ProviderProfessionalDetails.Add(professional);
                    //    break;
                    //}

                }
                providerProfile.ProviderProfessionalDetails = providerProfessionalDetails;
            }

            if (profile.HospitalPrivilegeInformation != null && profile.HospitalPrivilegeInformation.HospitalPrivilegeDetails.Count > 0)
            {
                List<ProviderHospitalAffiliationBusinessModel> providerHospitalAffiliationBusinessModel = new List<ProviderHospitalAffiliationBusinessModel>();
                foreach (var hospital in profile.HospitalPrivilegeInformation.HospitalPrivilegeDetails)
                {
                    ProviderHospitalAffiliationBusinessModel affiliation = new ProviderHospitalAffiliationBusinessModel();

                    if (hospital.Hospital != null)
                    {
                        affiliation.HospitalName = hospital.Hospital.HospitalName;
                    }
                    string startDate = ConvertToDateString(hospital.AffilicationStartDate);
                    string endDate = ConvertToDateString(hospital.AffiliationEndDate);
                    affiliation.EffectiveDates = startDate + " - " + endDate;
                    providerHospitalAffiliationBusinessModel.Add(affiliation);
                }
                providerProfile.ProviderHospitalAffiliations = providerHospitalAffiliationBusinessModel;

            }


            // my code
            if (locations.Count > 0)
            {
                List<ProviderPracitceInfoBusinessModel> ProviderPracitceInfos = new List<ProviderPracitceInfoBusinessModel>();
                foreach (var practice in locations)
                {
                    ProviderPracitceInfoBusinessModel location = new ProviderPracitceInfoBusinessModel();
                    if (practice != null)
                    {
                        location.Address = practice.Address;
                        location.PhoneNumber = practice.PhoneNumber;
                        location.FaxNumber = practice.FaxNumber;
                    }

                    foreach (var temp in profile.PracticeLocationDetails)
                    {
                        if (temp.BillingContactPerson != null && temp.FacilityId == practice.FacilityID)
                        {
                            location.BillingAddress = temp.BillingContactPerson.Street + ", " + temp.BillingContactPerson.Building + ", " + temp.BillingContactPerson.City + ", " + temp.BillingContactPerson.State + ", " + temp.BillingContactPerson.ZipCode;
                            location.BillingPhoneNumber = temp.BillingContactPerson.CountryCodeTelephone + "-" + temp.BillingContactPerson.Telephone;
                            location.BillingFaxNumber = temp.BillingContactPerson.CountryCodeFax + "-" + temp.BillingContactPerson.Fax;
                            //if (temp.OfficeHour.PracticeDays.Count > 0 && temp.OfficeHour.PracticeDays.First().DailyHours.Count > 0)
                            //{
                            //    location.OfficeHourMondays = new List<string>();
                            //    location.OfficeHourTuesdays = new List<string>();
                            //    location.OfficeHourWednesdays = new List<string>();
                            //    location.OfficeHourThursdays = new List<string>();
                            //    location.OfficeHourFridays = new List<string>();


                            //    foreach(var item in temp.OfficeHour.PracticeDays.ElementAt(0).DailyHours){

                            //        location.OfficeHourMondays.Add(item.StartTime + " - " + item.EndTime);
                                    
                            //        }
                            //    foreach (var item in temp.OfficeHour.PracticeDays.ElementAt(1).DailyHours)
                            //    {

                            //        location.OfficeHourTuesdays.Add(item.StartTime + " - " + item.EndTime);

                            //    }
                            //    foreach (var item in temp.OfficeHour.PracticeDays.ElementAt(2).DailyHours)
                            //    {

                            //        location.OfficeHourWednesdays.Add(item.StartTime + " - " + item.EndTime);

                            //    }
                            //    foreach (var item in temp.OfficeHour.PracticeDays.ElementAt(3).DailyHours)
                            //    {

                            //        location.OfficeHourThursdays.Add(item.StartTime + " - " + item.EndTime);

                            //    }
                            //    foreach (var item in temp.OfficeHour.PracticeDays.ElementAt(4).DailyHours)
                            //    {

                            //        location.OfficeHourFridays.Add(item.StartTime + " - " + item.EndTime);

                            //    }
                            //    //location.OfficeHourTuesday = temp.OfficeHour.PracticeDays.ElementAt(1).DailyHours.First().StartTime + " - " + temp.OfficeHour.PracticeDays.ElementAt(1).DailyHours.First().EndTime;
                            //    //location.OfficeHourWednesday = temp.OfficeHour.PracticeDays.ElementAt(2).DailyHours.First().StartTime + " - " + temp.OfficeHour.PracticeDays.ElementAt(2).DailyHours.First().EndTime;
                            //    //location.OfficeHourThursday = temp.OfficeHour.PracticeDays.ElementAt(3).DailyHours.First().StartTime + " - " + temp.OfficeHour.PracticeDays.ElementAt(3).DailyHours.First().EndTime;
                            //    //location.OfficeHourFridayday = temp.OfficeHour.PracticeDays.ElementAt(4).DailyHours.First().StartTime + " - " + temp.OfficeHour.PracticeDays.ElementAt(4).DailyHours.First().EndTime;
                            //    // }
                            //}
                        }
                        if (temp.FacilityId == practice.FacilityID)
                        {
                            if (temp.OfficeHour!=null && temp.OfficeHour.PracticeDays.Count > 0 && temp.OfficeHour.PracticeDays.First().DailyHours.Count > 0)
                            {
                                location.OfficeHourMondays = new List<string>();
                                location.OfficeHourTuesdays = new List<string>();
                                location.OfficeHourWednesdays = new List<string>();
                                location.OfficeHourThursdays = new List<string>();
                                location.OfficeHourFridays = new List<string>();


                                foreach (var item in temp.OfficeHour.PracticeDays.ElementAt(0).DailyHours)
                                {

                                    location.OfficeHourMondays.Add(item.StartTime + " - " + item.EndTime);

                                }
                                foreach (var item in temp.OfficeHour.PracticeDays.ElementAt(1).DailyHours)
                                {

                                    location.OfficeHourTuesdays.Add(item.StartTime + " - " + item.EndTime);

                                }
                                foreach (var item in temp.OfficeHour.PracticeDays.ElementAt(2).DailyHours)
                                {

                                    location.OfficeHourWednesdays.Add(item.StartTime + " - " + item.EndTime);

                                }
                                foreach (var item in temp.OfficeHour.PracticeDays.ElementAt(3).DailyHours)
                                {

                                    location.OfficeHourThursdays.Add(item.StartTime + " - " + item.EndTime);

                                }
                                foreach (var item in temp.OfficeHour.PracticeDays.ElementAt(4).DailyHours)
                                {

                                    location.OfficeHourFridays.Add(item.StartTime + " - " + item.EndTime);

                                }
                                //location.OfficeHourTuesday = temp.OfficeHour.PracticeDays.ElementAt(1).DailyHours.First().StartTime + " - " + temp.OfficeHour.PracticeDays.ElementAt(1).DailyHours.First().EndTime;
                                //location.OfficeHourWednesday = temp.OfficeHour.PracticeDays.ElementAt(2).DailyHours.First().StartTime + " - " + temp.OfficeHour.PracticeDays.ElementAt(2).DailyHours.First().EndTime;
                                //location.OfficeHourThursday = temp.OfficeHour.PracticeDays.ElementAt(3).DailyHours.First().StartTime + " - " + temp.OfficeHour.PracticeDays.ElementAt(3).DailyHours.First().EndTime;
                                //location.OfficeHourFridayday = temp.OfficeHour.PracticeDays.ElementAt(4).DailyHours.First().StartTime + " - " + temp.OfficeHour.PracticeDays.ElementAt(4).DailyHours.First().EndTime;
                                // }
                            }
                        }

                    }


                    ProviderPracitceInfos.Add(location);

                }
                providerProfile.ProviderPracitceInfos = ProviderPracitceInfos;
            }




            return providerProfile;
        }

        public int SaveDelegationProfileReport(int requestId, Entities.Credentialing.DelegationProfileReport.ProfileReport report)
        {
            try
            {
                var contractRequestRepo = uow.GetGenericRepository<CredentialingContractRequest>();
                var contractRequest = contractRequestRepo.Find(c => c.CredentialingContractRequestID == requestId, "ProfileReports");

                if (report.ProfileReportId == 0)
                {
                    if (contractRequest.ProfileReports.Count == 0)
                    {
                        contractRequest.ProfileReports = new List<ProfileReport>();
                        contractRequest.ProfileReports.Add(report);
                    }
                    else
                    {
                        contractRequest.ProfileReports.Add(report);
                    }

                }
                else
                {
                    var profileReport = contractRequest.ProfileReports.FirstOrDefault(p => p.ProfileReportId == report.ProfileReportId);
                    profileReport = AutoMapper.Mapper.Map<ProfileReport, ProfileReport>(report, profileReport);
                }

                contractRequestRepo.Update(contractRequest);
                contractRequestRepo.Save();

                return report.ProfileReportId;

            }
            catch (Exception)
            {
                throw;
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
            catch (Exception)
            {
                throw;
            }


        }


        public async Task<List<ProfileReport>> GetDelegationProfileReport(int CredContractRequestId)
        {

            try
            {
                var credentialingContractRequestRepo = uow.GetGenericRepository<CredentialingContractRequest>();
                var credentialingContractRequest = await credentialingContractRequestRepo.FindAsync(c => c.CredentialingContractRequestID == CredContractRequestId, "ProfileReports");

                return credentialingContractRequest.ProfileReports.ToList();
            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}