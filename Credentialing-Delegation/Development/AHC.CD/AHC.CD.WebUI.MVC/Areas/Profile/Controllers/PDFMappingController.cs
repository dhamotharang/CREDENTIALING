using AHC.CD.Business.Profiles;
using AHC.CD.ErrorLogging;
using AHC.CD.Resources.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AHC.CD.Business.BusinessModels.PDFGenerator;
using AHC.CD.Entities.MasterProfile;
using AHC.CD.Entities.MasterData.Enums;
using System.Configuration;
using System.Xml;
using System.ComponentModel;
using iTextSharp.text.pdf;
using System.IO;


namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class PDFMappingController : Controller
    {
        private IPDFProfileDataGeneratorManager PDFManager = null;
        private IErrorLogger errorLogger = null;

        public PDFMappingController(IPDFProfileDataGeneratorManager PDFManager, IErrorLogger errorLogger)
        {
            this.PDFManager = PDFManager;
            this.errorLogger = errorLogger;
        }

        // GET: /Credentialing/PDFMapping/

        public ActionResult Index()
        {
            return View();
        }

        //Get profile data based on id
        public async Task<AHC.CD.Entities.MasterProfile.Profile> GetPDFMappingProfileData(int profileId,string templateName)
        {
            try
            {
                AHC.CD.Entities.MasterProfile.Profile profile = await PDFManager.GetProfileList(profileId);
                PDFMappingDataBusinessModel pmodel = new PDFMappingDataBusinessModel();

                #region Demographics

                #region Personal Details

                pmodel.Personal_ProviderProfileImage = profile.ProfilePhotoPath;
                if (profile.PersonalDetail.ProviderTitles.Count > 0)
                {
                    profile.PersonalDetail.ProviderTitles = profile.PersonalDetail.ProviderTitles.Where(s => (s.Status != null && s.StatusType != StatusType.Inactive)).ToList();
                    foreach (var ptype in profile.PersonalDetail.ProviderTitles)
                    {
                        pmodel.Personal_ProviderType = ptype.ProviderType.ToString() + ", ";
                    }
                }
                pmodel.Personal_ProviderFirstName = profile.PersonalDetail.FirstName;
                pmodel.Personal_ProviderMiddleName = profile.PersonalDetail.MaidenName;
                pmodel.Personal_ProviderLastName = profile.PersonalDetail.LastName;
                pmodel.Personal_ProviderSuffix = profile.PersonalDetail.Suffix;
                pmodel.Personal_ProviderGender = profile.PersonalDetail.Gender;
                pmodel.Personal_MaidenName = profile.PersonalDetail.MaidenName;
                pmodel.Personal_MaritalStatus = profile.PersonalDetail.MaritalStatus;
                pmodel.Personal_SpouseName = profile.PersonalDetail.SpouseName;
                pmodel.Personal_ProviderDOB = profile.BirthInformation.DateOfBirth;
                foreach (var ptitle in profile.PersonalDetail.ProviderTitles)
                {
                    pmodel.Personal_ProviderTitle += ptitle.ProviderType.Title + ", ";
                }

                #endregion

                #region Other Legal Names

                if (profile.OtherLegalNames.Count > 0)
                {
                    profile.OtherLegalNames = profile.OtherLegalNames.Where(s => (s.Status != null && s.StatusType != StatusType.Inactive)).ToList();

                    pmodel.OtherLegalName_FirstName = new List<string>();
                    pmodel.OtherLegalName_MiddleName = new List<string>();
                    pmodel.OtherLegalName_LastName = new List<string>();
                    pmodel.OtherLegalName_Suffix = new List<string>();
                    pmodel.OtherLegalName_StartDate = new List<DateTime?>();
                    pmodel.OtherLegalName_EndDate = new List<DateTime?>();
                    pmodel.OtherLegalName_Certificate = new List<string>();


                    foreach (var oname in profile.OtherLegalNames)
                    {
                        pmodel.OtherLegalName_FirstName.Add(oname.OtherFirstName);
                        pmodel.OtherLegalName_MiddleName.Add(oname.OtherMiddleName);
                        pmodel.OtherLegalName_LastName.Add(oname.OtherLastName);
                        pmodel.OtherLegalName_Suffix.Add(oname.Suffix);
                        pmodel.OtherLegalName_StartDate.Add(oname.StartDate);
                        pmodel.OtherLegalName_EndDate.Add(oname.EndDate);
                        pmodel.OtherLegalName_Certificate.Add(oname.DocumentPath);
                    }
                }

                #endregion

                #region Contact Information

                if (profile.HomeAddresses.Count > 0)
                {
                    profile.HomeAddresses = profile.HomeAddresses.Where(s => (s.Status != null && s.StatusType != StatusType.Inactive)).ToList();

                    pmodel.Personal_ApartmentNumber = new List<string>();
                    pmodel.Personal_StreetAddress = new List<string>();
                    pmodel.Personal__Country = new List<string>();
                    pmodel.Personal_State = new List<string>();
                    pmodel.Personal_City = new List<string>();
                    pmodel.Personal__County = new List<string>();
                    pmodel.Personal_ZipCode = new List<string>();
                    pmodel.Personal_LivingStartDate = new List<DateTime?>();
                    pmodel.Personal_LivingStartDate = new List<DateTime?>();

                    foreach (var address in profile.HomeAddresses)
                    {
                        pmodel.Personal_ApartmentNumber.Add(address.UnitNumber);
                        pmodel.Personal_StreetAddress.Add(address.Street);
                        pmodel.Personal__Country.Add(address.Country);
                        pmodel.Personal_State.Add(address.Street);
                        pmodel.Personal_City.Add(address.City);
                        pmodel.Personal__County.Add(address.County);
                        pmodel.Personal_ZipCode.Add(address.ZipCode);
                        pmodel.Personal_LivingStartDate.Add(address.LivingFromDate);
                        pmodel.Personal_LivingStartDate.Add(address.LivingEndDate);
                    }
                }

                #endregion

                #region Contact Details

                if (profile.ContactDetail.PhoneDetails.Count > 0)
                {
                    profile.ContactDetail.PhoneDetails = profile.ContactDetail.PhoneDetails.Where(s => (s.Status != null && s.StatusType != StatusType.Inactive)).ToList();

                    pmodel.Personal_HomePhone = new List<string>();
                    pmodel.Personal_HomeFax = new List<string>();
                    pmodel.Personal_MobileNumber = new List<string>();
                    pmodel.Personal_PreferredContactMethod = new List<string>();
                    pmodel.Personal_EmailId = new List<string>();



                    foreach (var c in profile.ContactDetail.PhoneDetails)
                    {
                        if (c.PhoneType == PhoneTypeEnum.Home.ToString())
                        {
                            pmodel.Personal_HomePhone.Add(c.PhoneNumber);
                        }
                        else if (c.PhoneType == PhoneTypeEnum.Fax.ToString())
                        {
                            pmodel.Personal_HomeFax.Add(c.PhoneNumber);
                        }
                        else if (c.PhoneType == PhoneTypeEnum.Mobile.ToString())
                        {
                            pmodel.Personal_MobileNumber.Add(c.PhoneNumber);
                        }
                    }
                }
                pmodel.Personal_PagerNumber = profile.ContactDetail.PagerNumber;
                if (profile.ContactDetail.PreferredContacts.Count > 0)
                {
                    profile.ContactDetail.PreferredContacts = profile.ContactDetail.PreferredContacts.Where(s => (s.Status != null && s.StatusType != StatusType.Inactive)).ToList();

                    foreach (var c in profile.ContactDetail.PreferredContacts)
                    {
                        pmodel.Personal_PreferredContactMethod.Add(c.ContactType);
                    }
                }
                if (profile.ContactDetail.EmailIDs.Count > 0)
                {
                    profile.ContactDetail.EmailIDs = profile.ContactDetail.EmailIDs.Where(s => (s.Status != null && s.StatusType != StatusType.Inactive)).ToList();

                    foreach (var c in profile.ContactDetail.EmailIDs)
                    {
                        pmodel.Personal_EmailId.Add(c.EmailAddress);
                    }
                }
                #endregion

                #region Personal Identification

                pmodel.Personal_SocialSecurityNumber = profile.PersonalIdentification.SocialSecurityNumber;
                pmodel.Personal_DriverLicense = profile.PersonalIdentification.DL;
                pmodel.Personal_SSNCertificate = profile.PersonalIdentification.SSNCertificatePath;
                pmodel.Personal_DriverLicenseCertificate = profile.PersonalIdentification.DLCertificatePath;

                #endregion

                #region Birth Information

                pmodel.Personal_BirthDate = profile.BirthInformation.DateOfBirth;
                pmodel.Personal_BirthCountry = profile.BirthInformation.CountryOfBirth;
                pmodel.Personal_BirthState = profile.BirthInformation.StateOfBirth;
                pmodel.Personal_BirthCounty = profile.BirthInformation.CountyOfBirth;
                pmodel.Personal_BirthCity = profile.BirthInformation.CityOfBirth;
                pmodel.Personal_BirthCertificate = profile.BirthInformation.BirthCertificatePath;

                #endregion

                #region Visa Information

                //pmodel.Personal_IsUSCitizen = profile.VisaDetail.IsResidentOfUSA;
                //pmodel.Personal_IsUSAuthorized = profile.VisaDetail.IsAuthorizedToWorkInUS;
                //pmodel.Personal_VisaNumber = profile.VisaDetail.VisaInfo.VisaNumber;
                ////pmodel.Personal_VisaType = profile.VisaDetail.VisaInfo.VisaType.ToString();
                ////pmodel.Personal_VisaStatus = profile.VisaDetail.VisaInfo.VisaStatus.ToString();
                //pmodel.Personal_VisaSponsor = profile.VisaDetail.VisaInfo.VisaSponsor;
                //pmodel.Personal_VisaExpiration = profile.VisaDetail.VisaInfo.VisaExpirationDate;
                //pmodel.Personal_VisaDocument = profile.VisaDetail.VisaInfo.VisaCertificatePath;
                //pmodel.Personal_GreenCardNumber = profile.VisaDetail.VisaInfo.GreenCardNumber;
                //pmodel.Personal_GreenCardDocument = profile.VisaDetail.VisaInfo.GreenCardCertificatePath;
                //pmodel.Personal_NationalIdentificationNo = profile.VisaDetail.VisaInfo.NationalIDNumber;
                //pmodel.Personal_NationalIdentificationDoc = profile.VisaDetail.VisaInfo.NationalIDCertificatePath;
                //pmodel.Personal_IssueCountry = profile.VisaDetail.VisaInfo.CountryOfIssue;

                #endregion

                #region Languages Known

                pmodel.Personal_LanguageKnown = new List<string>();
                if (profile.LanguageInfo.KnownLanguages.Count > 0)
                {
                    foreach (var l in profile.LanguageInfo.KnownLanguages)
                    {
                        pmodel.Personal_LanguageKnown.Add(l.Language);
                    }
                }
                pmodel.Personal_IsAmericanSignLanguage = profile.LanguageInfo.CanSpeakAmericanSignLanguage;

                #endregion

                #endregion

                //#region Specialty

                //if (profile.SpecialtyDetails.Count > 0)
                //{
                //    profile.SpecialtyDetails = profile.SpecialtyDetails.Where(s => (s.Status != null && s.StatusType != StatusType.Inactive)).ToList();
                //    foreach (var s in profile.SpecialtyDetails)
                //    {
                //        pmodel.Specialty_Type.Add(s.SpecialtyPreference);
                //        pmodel.Specialty_SpecialtyName.Add(s.Specialty.Name);
                //        pmodel.Specialty_BoardName.Add(s.SpecialtyBoardCertifiedDetail.SpecialtyBoard.Name);
                //        pmodel.Specialty_InitialCertificationDate.Add(s.SpecialtyBoardCertifiedDetail.InitialCertificationDate);
                //        pmodel.Specialty_LastRecertificationDate.Add(s.SpecialtyBoardCertifiedDetail.LastReCerificationDate);
                //        pmodel.Specialty_ExpirationDate.Add(s.SpecialtyBoardCertifiedDetail.ExpirationDate);
                //        pmodel.Specialty_ListedInHMO.Add(s.ListedInHMO);
                //        pmodel.Specialty_ListedInPOS.Add(s.ListedInPOS);
                //        pmodel.Specialty_ListedInPPO.Add(s.ListedInPPO);
                //        pmodel.Specialty_PercentOfTime.Add(s.PercentageOfTime);
                //        pmodel.Specialty_IsBoardCertified.Add(s.IsBoardCertified);
                //        pmodel.Specialty_DateOfExam.Add(s.SpecialtyBoardNotCertifiedDetail.ExamDate);
                //        pmodel.Specialty_ReasonForNotTakingExam.Add(s.SpecialtyBoardNotCertifiedDetail.ReasonForNotTakingExam);
                //        pmodel.Specialty_RemarkForExamFail.Add(s.SpecialtyBoardNotCertifiedDetail.RemarkForExamFail);
                //        pmodel.Specialty_Certificate.Add(s.SpecialtyBoardCertifiedDetail.BoardCertificatePath);
                //    }
                //}
                //pmodel.Specialty_PracticeInterest = profile.PracticeInterest.Interest;

                //#endregion

                //#region Practice Location

                //#region General Information
                //if (profile.PracticeLocationDetails.Count > 0)
                //{
                //    profile.PracticeLocationDetails = profile.PracticeLocationDetails.Where(s => (s.Status != null && s.StatusType != StatusType.Inactive)).ToList();
                //    foreach (var p in profile.PracticeLocationDetails)
                //    {
                //        pmodel.General_IsPracticeExclusively = p.PracticeExclusively;
                //        pmodel.General_LocationType = p.PracticeLocationCorporateName;
                //        pmodel.General_IsCurrentlyPracticing = p.CurrentlyPracticingAtThisAddress;
                //        pmodel.General_ExpectedStartDate = p.StartDate;
                //        //pmodel.General_ProviderType=p.PracticeProviders
                //        pmodel.General_PracticeName = p.PracticeLocationCorporateName;
                //        pmodel.General_Phone = p.Facility.Telephone;
                //        pmodel.General_Fax = p.Facility.Fax;
                //        pmodel.General_Email = p.Facility.EmailAddress;
                //        pmodel.General_Number = p.Facility.MobileNumber;
                //        pmodel.General_Street = p.Facility.Street;
                //        //pmodel.General_Suite=p.Facility.
                //        pmodel.General_County = p.Facility.State;
                //        pmodel.General_State = p.Facility.State;
                //        pmodel.General_City = p.Facility.City;
                //        pmodel.General_Country = p.Facility.Country;
                //        pmodel.General_ZipCode = p.Facility.ZipCode;
                //        //pmodel.General_IndividualTaxId=p.
                //        //pmodel.General_GroupTaxId=p.
                //        //pmodel.General_PrimaryTaxId=p
                //        //pmodel.General_GeneralCorrespondence=p
                //        //pmodel.General_UseTaxId=p
                //    }
                //}

                //#endregion

                //#endregion

                //#region Identification & Licenses

                //#region State License Information details

                //if (profile.StateLicenses.Count > 0)
                //{
                //    profile.StateLicenses = profile.StateLicenses.Where(s => (s.Status != null && s.StatusType != StatusType.Inactive)).ToList();
                //    foreach (var s in profile.StateLicenses)
                //    {
                //        pmodel.StateLicense_Number.Add(s.LicenseNumber);
                //        pmodel.StateLicense_Type.Add(s.ProviderType.Title);
                //        pmodel.StateLicense_Status.Add(s.Status);
                //        pmodel.StateLicense_IssuingState.Add(s.IssueState);
                //        //pmodel.StateLicense_PracticeState.Add(s.);
                //        pmodel.StateLicense_OriginalIssueDate.Add(s.IssueDate);
                //        pmodel.StateLicense_ExpirationDate.Add(s.ExpiryDate);
                //        pmodel.StateLicense_CurrentIssueDate.Add(s.CurrentIssueDate);
                //        pmodel.StateLicense_IsGoodStanding.Add(s.LicenseInGoodStanding);
                //        //pmodel.StateLicense_RelinquishedDate.Add(s.);
                //        pmodel.StateLicense_Certificate.Add(s.StateLicenseDocumentPath);
                //    }
                //}

                //#endregion

                //#region Federal DEA Information

                //if (profile.FederalDEAInformations.Count > 0)
                //{
                //    profile.FederalDEAInformations = profile.FederalDEAInformations.Where(s => (s.Status != null && s.StatusType != StatusType.Inactive)).ToList();
                //    foreach (var f in profile.FederalDEAInformations)
                //    {
                //        string str = "";
                //        pmodel.DEA_Number.Add(f.DEANumber);
                //        pmodel.DEA_RegistrationState.Add(f.StateOfReg);
                //        pmodel.DEA_IssueDate.Add(f.IssueDate);
                //        pmodel.DEA_ExpirationDate.Add(f.ExpiryDate);
                //        foreach (var g in f.DEAScheduleInfoes)
                //        {
                //            str = g.DEASchedule.ScheduleTitle + ", ";
                //        }
                //        pmodel.DEA_Schedules.Add(str);
                //        pmodel.DEA_IsGoodStanding.Add(f.IsInGoodStanding);
                //        pmodel.DEA_Certificate.Add(f.DEALicenceCertPath);
                //        //pmodel.DEA_IsLimited.Add(f.);
                //        //pmodel.DEA_RestrictionExplaination.Add(f.);
                //        //pmodel.DEA_IsSubstanceRegCertificate.Add(f.);
                //    }
                //}

                //#endregion

                //#region Medicare and Medicaid Information

                //if (profile.MedicareInformations.Count > 0)
                //{
                //    profile.MedicareInformations = profile.MedicareInformations.Where(s => (s.Status != null && s.StatusType != StatusType.Inactive)).ToList();
                //    foreach (var m in profile.MedicareInformations)
                //    {
                //        //pmodel.IsMedicareApproved.Add(m);
                //        pmodel.Medicare_Number.Add(m.LicenseNumber);
                //        pmodel.Medicare_IssueDate.Add(m.IssueDate);
                //        //pmodel.Medicare_ExpirationDate.Add(m.e);
                //        //pmodel.Medicare_State.Add(m.);
                //    }
                //}

                //if (profile.MedicaidInformations.Count > 0)
                //{
                //    profile.MedicaidInformations = profile.MedicaidInformations.Where(s => (s.Status != null && s.StatusType != StatusType.Inactive)).ToList();
                //    foreach (var m in profile.MedicaidInformations)
                //    {
                //        //pmodel.IsMedicaidApproved.Add(m);
                //        pmodel.MedicaidNumber.Add(m.LicenseNumber);
                //        //pmodel.Medicaid_State.Add(m.);
                //        pmodel.Medicare_IssueDate.Add(m.IssueDate);
                //        pmodel.MedicareMedicaid_Certificate.Add(m.CertificatePath);
                //        //pmodel.MedicareMedicaid_Sanctions.Add();
                //        //pmodel.Medicaid_ExpirationDate(m.);
                //        //pmodel.Medicare_ExpirationDate.Add(m.e);
                //        //pmodel.Medicare_State.Add(m.);
                //    }
                //}

                //#endregion

                //#region CDS Information

                //if (profile.CDSCInformations.Count > 0)
                //{
                //    profile.CDSCInformations = profile.CDSCInformations.Where(s => (s.Status != null && s.StatusType != StatusType.Inactive)).ToList();
                //    foreach (var c in profile.CDSCInformations)
                //    {
                //        pmodel.CDS_Number.Add(c.CertNumber);
                //        pmodel.CDS_RegistrationState.Add(c.State);
                //        pmodel.CDS_IssueDate.Add(c.IssueDate);
                //        pmodel.CDS_ExpirationDate.Add(c.ExpiryDate);
                //        pmodel.CDS_Certificate.Add(c.CDSCCerificatePath);
                //    }
                //}

                //#endregion

                //#region Other Identification Numbers

                //pmodel.NPI_Number = profile.OtherIdentificationNumber.NPINumber;
                //pmodel.NPI_Username = profile.OtherIdentificationNumber.NPIUserName;
                //pmodel.NPI_Password = profile.OtherIdentificationNumber.NPIPassword;
                //pmodel.CAQH_Number = profile.OtherIdentificationNumber.CAQHNumber;
                //pmodel.CAQH_Username = profile.OtherIdentificationNumber.CAQHUserName;
                //pmodel.CAQH_Password = profile.OtherIdentificationNumber.CAQHPassword;
                //pmodel.UPIN_Number = profile.OtherIdentificationNumber.UPINNumber;
                //pmodel.USMLE_Number = profile.OtherIdentificationNumber.USMLENumber;

                //#endregion

                //#endregion

                readXml(pmodel, templateName);


                return profile;
            }
            catch (Exception)
            {
                
                throw;
            }

        }

        public string CreatePDF(Dictionary<string,string> dataObj, string pdfName, string templateName)
        {

            try
            {
                string pdfTemplate = @"D:\Office\New Projects\Development\AHC.CD\AHC.CD.WebUI.MVC\App_Data\PlanTemplate\Tricare_PdfTemplate.pdf";
                string newFile = @"D:\Office\New Projects\Development\AHC.CD\AHC.CD.WebUI.MVC\App_Data\SavedPDF\" + pdfName + ".pdf";

                PdfReader pdfReader = new PdfReader(pdfTemplate);
                PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(
                            newFile, FileMode.Create));

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

                return pdfName;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    

        [HttpPost]
        public ActionResult readXml(PDFMappingDataBusinessModel pmodel, string templateName)
        {
            try
            {
                Dictionary<string, string> dictionary = new Dictionary<string, string>();

                var xmldoc = new System.Xml.XmlDocument();
                xmldoc.Load(ConfigurationManager.AppSettings[templateName]);

                foreach (XmlNode node in xmldoc.GetElementsByTagName("Property"))
                {
                    string GenericVariableName = node.Attributes["GenericVariable"].Value; //or loop through its children as well
                    string PlanVariableName = node.Attributes["PlanVariable"].Value; //or loop through its children as well

                    foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(pmodel))
                    {
                        if (prop.Name.ToString() == GenericVariableName)
                        {
                            if (prop.GetValue(pmodel) != null) { 
                            dictionary.Add(PlanVariableName, prop.GetValue(pmodel).ToString());
                            }
                            break;
                        }
                    }
                }


                CreatePDF(dictionary,"testPdf",templateName); 
            }
            catch (Exception ex)
            {
                return View();
            }

            return View();
        }


    }
}
