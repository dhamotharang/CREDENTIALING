using AHC.CD.Data.EFRepository;
using AHC.CD.Entities;
using AHC.CD.Entities.MasterData.Account;
using AHC.CD.Entities.MasterData.Account.Branch;
using AHC.CD.Entities.MasterData.Account.Service;
using AHC.CD.Entities.MasterData.Account.Staff;
using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using AHC.CD.Entities.MasterProfile;
using AHC.CD.Entities.MasterProfile.BoardSpecialty;
using AHC.CD.Entities.MasterProfile.Contract;
using AHC.CD.Entities.MasterProfile.Demographics;
using AHC.CD.Entities.MasterProfile.DisclosureQuestions;
using AHC.CD.Entities.MasterProfile.EducationHistory;
using AHC.CD.Entities.MasterProfile.HospitalPrivilege;
using AHC.CD.Entities.MasterProfile.IdentificationAndLicenses;
using AHC.CD.Entities.MasterProfile.PracticeLocation;
using AHC.CD.Entities.MasterProfile.ProfessionalReference;
using AHC.CD.Entities.MasterProfile.WorkHistory;
using AHC.UtilityService;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

// https://docs.google.com/spreadsheets/d/1Ei6WeoXwTtpfBvuNuL_ZOBm9DW-JPbDKMZ2rj-wLrag/edit#gid=461693128



namespace DBMigrationApplication
{
    public partial class Form1 : Form
    {
        AHC.CD.Data.EFRepository.EFEntityContext context = new AHC.CD.Data.EFRepository.EFEntityContext();
        AHC.CD.Entities.MasterProfile.Profile profile = null;
        string[] xmlFiles;
        int counter = 1;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //context.Database.Delete();
            xmlFiles = Directory.GetFiles(@"D:\AHCDBMigration\XML\FinalXML", "*.xml");
            progressBar1.Minimum= 1;
            progressBar1.Maximum = xmlFiles.Count();
            lblProfilesCount.Text = xmlFiles.Count().ToString();
            
            Task t = Task.Factory.StartNew(() => { DoMigration(); });
            t.Wait();
            //DoMigration();
            MessageBox.Show("Migration Completed");
            
        }
        string ProviderNPINumber;
        private void DoMigration()
        {
            foreach (var file in xmlFiles)
            {
                #region Initializer
                lblDone.Text = counter.ToString();
                progressBar1.Value = counter++;
                XDocument xmlDoc = XDocument.Load(file);
                string fileName = Path.GetFileName(file);
                lblCurrentProfile.Text = fileName;

                listBox1.Items.Add("Migrating Profile of :" + fileName);

                profile = new AHC.CD.Entities.MasterProfile.Profile();
                profile.StatusType = StatusType.Active;



                #endregion

                #region GetNPI Number
                var npis = (from x in xmlDoc.Descendants("d_prf_personal_row")
                            select
                            new //AHC.CD.Entities.MasterProfile.IdentificationAndLicenses.OtherIdentificationNumber
                            {
                                NPINumber = x.Element("pd_basic_npi_number").Value.Length > 0 ? x.Element("pd_basic_npi_number").Value : null,
                            }).ToList();
                ProviderNPINumber = npis.First().NPINumber;
                #endregion

                #region Demographics - Personal Details
                listBox1.Items.Add("Adding Demographics Information");
                // Personal Details
                var personalDetails = (from x in xmlDoc.Descendants("d_prf_personal_row")
                                       select
                                       new AHC.CD.Entities.MasterProfile.Demographics.PersonalDetail
                                       {
                                           MaritalStatus = x.Element("pd_basic_married").Value.Length > 0 ? x.Element("pd_basic_married").Value : "Not Available",
                                           SpouseName = x.Element("pd_basic_spouse_name").Value.Length > 0 ? x.Element("pd_basic_spouse_name").Value : "Not Available",
                                           Gender = x.Element("sex").Value.Length > 0 ? x.Element("sex").Value : "Not Available",
                                           MaidenName = x.Element("pd_basic_maiden_other_name").Value.Length > 0 ? x.Element("pd_basic_maiden_other_name").Value : "Not Available",
                                           // Mandatory Fields
                                           Salutation = "Not Available",
                                           FirstName = GetFirstNameFromSignature(xmlDoc),
                                           LastName = GetLastNameFromSignature(xmlDoc),
                                           ProviderLevelID = GetProviderLevel(file),

                                       }).ToList();

                profile.PersonalDetail = new AHC.CD.Entities.MasterProfile.Demographics.PersonalDetail();
                listBox1.Items.Add("\tAdding Personal Details");
                foreach (var item in personalDetails)
                {
                    profile.PersonalDetail = item;
                    listBox1.Items.Add("\t\tMarital Status\t" + profile.PersonalDetail.MaritalStatus);
                    listBox1.Items.Add("\t\tSpouse Name\t" + profile.PersonalDetail.SpouseName);
                    listBox1.Items.Add("\t\tGender\t" + profile.PersonalDetail.Gender);
                    listBox1.Items.Add("\t\tSalutation\t" + profile.PersonalDetail.Salutation);
                    listBox1.Items.Add("\t\tFirst Name\t" + profile.PersonalDetail.FirstName);
                    listBox1.Items.Add("\t\tLast Name\t" + profile.PersonalDetail.LastName);
                    listBox1.Items.Add("\t\tMaiden Name\t" + profile.PersonalDetail.MaidenName);
                }
                #endregion

                #region Other Legal Names - Only Documents Done

                if (profile.OtherLegalNames == null)
                    profile.OtherLegalNames = new List<OtherLegalName>();

                OtherLegalName oln = new OtherLegalName();
                oln.DocumentPath = GetFilePath(ProviderNPINumber, "OLN.pdf", "Other Legal Names");
                if (oln.DocumentPath != null)
                {
                    listBox1.Items.Add("\tAdding Other Legal Name Document");
                    oln.OtherFirstName = "Not Available";
                    oln.OtherLastName = "Not Available";
                    profile.OtherLegalNames.Add(oln);
                    listBox1.Items.Add("\t\tOther First Name: " + oln.OtherFirstName);
                    listBox1.Items.Add("\t\tOther Last Name: " + oln.OtherLastName);
                    listBox1.Items.Add("\t\tDocument Path: " + oln.DocumentPath);
                }
                #endregion

                #region Demographics - Birth Information - Documents Done

                // Birth Information
                var birthInformation = (from x in xmlDoc.Descendants("d_prf_personal_row")
                                        select
                                        new AHC.CD.Entities.MasterProfile.Demographics.BirthInformation
                                        {
                                            //DateOfBirthStored = x.Element("pd_basic_date_of_birth").Value.Length > 0 ? DateTime.Parse(x.Element("pd_basic_date_of_birth").Value) : DateTime.Parse("01/10/1900"),
                                            DateOfBirth = x.Element("pd_basic_date_of_birth").Value.Length > 0 ? x.Element("pd_basic_date_of_birth").Value : null,
                                            CityOfBirth = x.Element("pd_basic_pob_city").Value.Length > 0 ? x.Element("pd_basic_pob_city").Value : null,
                                            CountryOfBirth = x.Element("cpob_country").Value.Length > 0 ? x.Element("cpob_country").Value : null,
                                            StateOfBirth = x.Element("cpob_state").Value.Length > 0 ? x.Element("cpob_state").Value : null,
                                            BirthCertificatePath = GetFilePath(ProviderNPINumber, "Birth.pdf", "Birth Certificate"),
                                            // Mandatory Fields
                                            //CountyOfBirth = "Not Available",

                                        }).ToList();

                profile.BirthInformation = new AHC.CD.Entities.MasterProfile.Demographics.BirthInformation();
                listBox1.Items.Add("\tAdding Birth Information");
                foreach (var item in birthInformation)
                {
                    profile.BirthInformation = item;
                    listBox1.Items.Add("\t\tDate Of Birth\t" + profile.BirthInformation.DateOfBirth);
                    listBox1.Items.Add("\t\tCity of Birth\t" + profile.BirthInformation.CityOfBirth);
                    listBox1.Items.Add("\t\tState Of Birth\t" + profile.BirthInformation.StateOfBirth);
                    listBox1.Items.Add("\t\tCountry Of Birth\t" + profile.BirthInformation.CountryOfBirth);
                    listBox1.Items.Add("\t\tCounty Of Birth\t" + profile.BirthInformation.CountyOfBirth);
                    listBox1.Items.Add("\t\tBirth Certificate Path\t" + profile.BirthInformation.BirthCertificatePath);
                }
                #endregion

                #region Demographics - Personal Identification - Documents Done
                // Personal Identification

                var personalIdentification = (from x in xmlDoc.Descendants("d_prf_personal_row")
                                              select
                                              new AHC.CD.Entities.MasterProfile.Demographics.PersonalIdentification
                                              {
                                                  SSN = x.Element("pd_basic_ssn").Value.Length > 0 ? x.Element("pd_basic_ssn").Value : "Not Available",
                                                  SSNCertificatePath = GetFilePath(ProviderNPINumber, "SSN.pdf", "SSN Certificate"),
                                                  //DL = "Not Available",
                                                  DLCertificatePath = GetFilePath(ProviderNPINumber, "DL.pdf", "Driving License"),

                                              }).ToList();

                profile.PersonalIdentification = new AHC.CD.Entities.MasterProfile.Demographics.PersonalIdentification();
                listBox1.Items.Add("\tAdding Personal Identification");
                foreach (var item in personalIdentification)
                {
                    profile.PersonalIdentification = item;
                    listBox1.Items.Add("\t\tSSN Number\t" + profile.PersonalIdentification.SSN);
                    listBox1.Items.Add("\t\tSSN Certificate Path\t" + profile.PersonalIdentification.SSNCertificatePath);
                    listBox1.Items.Add("\t\tDL Number\t" + profile.PersonalIdentification.DL);
                    listBox1.Items.Add("\t\tDL Certificate Path\t" + profile.PersonalIdentification.DLCertificatePath);
                }

                #endregion

                #region Demopgraphics - Contact Details
                // Contact Details

                var contactDetails = (from x in xmlDoc.Descendants("d_prf_personal_row")
                                      select
                                      new AHC.CD.Entities.MasterProfile.Demographics.ContactDetail
                                      {
                                          PersonalPager = x.Element("pd_basic_personal_pager").Value.Length > 0 ? x.Element("pd_basic_personal_pager").Value : "Not Available",
                                          PhoneDetails = new List<AHC.CD.Entities.MasterProfile.Demographics.PhoneDetail>
                                             { 
                                                 new AHC.CD.Entities.MasterProfile.Demographics.PhoneDetail
                                                 {
                                                     PhoneNumber = FormatPhone(x.Element("cphone").Value), 
                                                     
                                                     // Mandatory Fields
                                                     //CountryCode = "Not Available", 
                                                     PhoneTypeEnum = PhoneTypeEnum.Mobile, 
                                                     Preference = "Primary",
                                                     Status = "Active"
                                                 } 
                                             },
                                          EmailIDs = GetEmailIds(ProviderNPINumber),
                                          PreferredContacts = new List<PreferredContact> { 
                                                new PreferredContact {ContactType = "Mobile", Status = "Active"},
                                                new PreferredContact {ContactType = "Email", Status = "Active"},
                                          },
                                         

                                      }).ToList();

                profile.ContactDetail = new AHC.CD.Entities.MasterProfile.Demographics.ContactDetail();
                listBox1.Items.Add("\tAdding Contact Details");
                foreach (var item in contactDetails)
                {
                    profile.ContactDetail = item;
                    listBox1.Items.Add("\t\tPhone Number\t" + profile.ContactDetail.PhoneDetails.First().Number);
                    listBox1.Items.Add("\t\tCountry Code\t" + profile.ContactDetail.PhoneDetails.First().CountryCode);
                    listBox1.Items.Add("\t\tPhone Type\t" + profile.ContactDetail.PhoneDetails.First().PhoneType);
                    listBox1.Items.Add("\t\tPreference\t" + profile.ContactDetail.PhoneDetails.First().Preference);
                    listBox1.Items.Add("\t\tStatus\t" + profile.ContactDetail.PhoneDetails.First().Status);
                    listBox1.Items.Add("\t\tPersonal Pager\t" + profile.ContactDetail.PersonalPager);

                }
                #endregion

                #region Demographics - Other Identification Number - NPI Number
                // Other Identification Number - NPI Number

                var npiNumbers = (from x in xmlDoc.Descendants("d_prf_personal_row")
                                  select
                                  new AHC.CD.Entities.MasterProfile.IdentificationAndLicenses.OtherIdentificationNumber
                                  {
                                      NPINumber = x.Element("pd_basic_npi_number").Value.Length > 0 ? x.Element("pd_basic_npi_number").Value : "Not Available",
                                  }).ToList();

                profile.OtherIdentificationNumber = new AHC.CD.Entities.MasterProfile.IdentificationAndLicenses.OtherIdentificationNumber();
                listBox1.Items.Add("\tAdding Other Identification Number - NPI");
                foreach (var item in npiNumbers)
                {
                    profile.OtherIdentificationNumber = item;
                    listBox1.Items.Add("\t\tNPI Number\t" + profile.OtherIdentificationNumber.NPINumber);
                }

                #endregion

                #region Professional Liability - Insurance - Documents Done
                // Professional Liability
                int? addressid;
                var professionalLiabilities = (from x1 in xmlDoc.Descendants("d_prf_insurance")
                                               from x in x1.Descendants("d_prf_insurance_row")
                                               select
                                               new AHC.CD.Entities.MasterProfile.ProfessionalLiability.ProfessionalLiabilityInfo
                                               {
                                                   InsuranceCarrierID = GetInsuranceCarrierID(x.Element("address_lookup_carrier_code").Value, out addressid),
                                                   InsuranceCarrierAddressID = addressid,
                                                   PolicyNumber = x.Element("pd_insurance_policy_number").Value.Length > 0 ? x.Element("pd_insurance_policy_number").Value : "Not Available",
                                                   EffectiveDate = x.Element("pd_insurance_coverage_from").Value.Length > 0 ? DateTime.Parse(x.Element("pd_insurance_coverage_from").Value) : default(DateTime?),
                                                   PolicyIncludesTailCoverage = x.Element("pd_insurance_tail_coverage").Value.Length > 0 ? x.Element("pd_insurance_tail_coverage").Value : "Not Available",
                                                   AmountOfCoveragePerOccurance = x.Element("pd_insurance_coverage_limit_from").Value.Length > 0 ? double.Parse(x.Element("pd_insurance_coverage_limit_from").Value) : default(double?),
                                                   AmountOfCoverageAggregate = x.Element("pd_insurance_coverage_limit_to").Value.Length > 0 ? double.Parse(x.Element("pd_insurance_coverage_limit_to").Value) : default(double?),
                                                   //DenialReason = x.Element("pd_insurance_denied_explain").Value.Length > 0 ? x.Element("pd_insurance_denied_explain").Value : "Not Available",
                                                   ExpirationDate = x.Element("pd_insurance_coverage_to").Value.Length > 0 ? DateTime.Parse(x.Element("pd_insurance_coverage_to").Value) : default(DateTime?),

                                                   InsuranceCertificatePath = GetFilePath(ProviderNPINumber, "LIC.pdf", "Insurance Certificate", x.Element("pd_insurance_coverage_to").Value.Length > 0 ? DateTime.Parse(x.Element("pd_insurance_coverage_to").Value) : DateTime.Parse("01/01/1900")),
                                                   // Mandatory Fields
                                                   //PhoneNumber = "not Available",
                                                   //SelfInsured = "Not Available",
                                                   //OriginalEffectiveDate = DateTime.Parse("01/01/1900"),
                                                   //CoverageType = "Not Available",
                                               }).ToList();

                profile.ProfessionalLiabilityInfoes = new List<AHC.CD.Entities.MasterProfile.ProfessionalLiability.ProfessionalLiabilityInfo>();

                listBox1.Items.Add("Adding Professional Liabilities");
                foreach (var item in professionalLiabilities)
                {
                    profile.ProfessionalLiabilityInfoes.Add(item);

                    foreach (var pli in profile.ProfessionalLiabilityInfoes)
                    {
                        listBox1.Items.Add("\t\tInsurance Carrier ID\t" + pli.InsuranceCarrierID);
                        listBox1.Items.Add("\t\tInsurance Carrier Name\t" + context.InsuranceCarriers.Find(pli.InsuranceCarrierID).Name);
                        listBox1.Items.Add("\t\tPolicy Number\t" + pli.PolicyNumber);
                        listBox1.Items.Add("\t\tEffective Date\t" + pli.EffectiveDate);
                        listBox1.Items.Add("\t\tPolicy Includes Tail Coverage\t" + pli.PolicyIncludesTailCoverage);
                        listBox1.Items.Add("\t\tAmount Of Coverage Per Occurance\t" + pli.AmountOfCoveragePerOccurance);
                        listBox1.Items.Add("\t\tAmount Of Coverage Aggregate\t" + pli.AmountOfCoverageAggregate);
                        //listBox1.Items.Add("\t\tDenial Reason\t" + pli.DenialReason);
                        listBox1.Items.Add("\t\tExpiration Date\t" + pli.ExpirationDate);
                        listBox1.Items.Add("\t\tSelfInsured\t" + pli.SelfInsured);
                        listBox1.Items.Add("\t\tOriginal Effective Date\t" + pli.OriginalEffectiveDate);
                        listBox1.Items.Add("\t\tCoverage Type\t" + pli.CoverageType);
                        listBox1.Items.Add("\t\tInsurance Certificate Path\t" + pli.InsuranceCertificatePath);
                    }

                }

                #endregion

                #region DEA Schedules  - CSR - Documents Done
                // DEA CSR

                listBox1.Items.Add("Adding DEA CSR Information");
                var deaCSR = (from x1 in xmlDoc.Descendants("d_prf_csr")
                              from x in x1.Descendants("d_prf_csr_row")
                              select
                              new AHC.CD.Entities.MasterProfile.IdentificationAndLicenses.FederalDEAInformation
                              {
                                  StateOfReg = GetState(x.Element("code_lookup_state").Value.Length > 0 ? x.Element("code_lookup_state").Value : "Not Available"),
                                  ExpiryDate = x.Element("expiration_date").Value.Length > 0 ? DateTime.Parse(x.Element("expiration_date").Value) : DateTime.Parse("01/01/1900"),
                                  DEANumber = x.Element("license_number").Value.Length > 0 ? x.Element("license_number").Value : "Not Available",
                                  DEAScheduleInfoes = GetDeaSchedules(
                                  x.Element("code_lookup_cat_1").Value,
                                  x.Element("code_lookup_cat_2").Value,
                                  x.Element("code_lookup_cat_3").Value,
                                  x.Element("code_lookup_cat_4").Value,
                                  x.Element("code_lookup_cat_5").Value,
                                  x.Element("code_lookup_cat_6").Value,
                                  x.Element("code_lookup_cat_7").Value,
                                  x.Element("code_lookup_cat_8").Value,
                                  x.Element("code_lookup_cat_9").Value,
                                  x.Element("code_lookup_cat_10").Value
                                  ),
                                  DEALicenceCertPath = GetFilePath(ProviderNPINumber, "DEA1.pdf", "DEA License", x.Element("expiration_date").Value.Length > 0 ? DateTime.Parse(x.Element("expiration_date").Value) : DateTime.Parse("01/01/1900")),

                                  // Mandatory Fields
                                  //IssueDate = DateTime.Parse("01/01/1900"),
                                  //IsInGoodStanding = "Not Available",


                              }).ToList();



                profile.FederalDEAInformations = new List<AHC.CD.Entities.MasterProfile.IdentificationAndLicenses.FederalDEAInformation>();

                listBox1.Items.Add("\tAdding Federal DEA Information");
                foreach (var item in deaCSR)
                {
                    profile.FederalDEAInformations.Add(item);
                    foreach (var dea in profile.FederalDEAInformations)
                    {
                        listBox1.Items.Add("\t\tState Of Registration\t" + dea.StateOfReg);
                        listBox1.Items.Add("\t\tExpiry Date\t" + dea.ExpiryDate);
                        listBox1.Items.Add("\t\tDEA Number\t" + dea.DEANumber);
                        listBox1.Items.Add("\t\tIssue Date\t" + dea.IssueDate);
                        listBox1.Items.Add("\t\tIs In Good Standing\t" + dea.IsInGoodStanding);
                        listBox1.Items.Add("\t\tDEA Certificate Path\t" + dea.DEALicenceCertPath);
                        listBox1.Items.Add("\t\tAdding DEA Schedules");
                        foreach (var d in dea.DEAScheduleInfoes)
                        {
                            if (d != null)
                            {
                                listBox1.Items.Add("\t\t\t Schedule: " + d.DEASchedule.ScheduleTitle);
                                listBox1.Items.Add("\t\t\t Schedule Type: " + d.DEASchedule.ScheduleTypeTitle);
                                listBox1.Items.Add("\t\t\t Is Eligible: " + d.IsEligible);
                            }
                        }
                    }




                }


                #endregion

                #region ECFMG Details - Documents Done

                // ECFMG
                listBox1.Items.Add("ECFMG Details");
                var ecfmg = (from x in xmlDoc.Descendants("d_prf_ecfmg_row")
                             select
                             new AHC.CD.Entities.MasterProfile.EducationHistory.ECFMGDetail
                             {
                                 ECFMGNumber = x.Element("ecfmg_number").Value.Length > 0 ? x.Element("ecfmg_number").Value : "Not Available-" + Guid.NewGuid(),
                                 ECFMGIssueDate = x.Element("date_issued").Value.Length > 0 ? DateTime.Parse(x.Element("date_issued").Value) : default(DateTime?),
                                 ECFMGCertPath = GetFilePath(ProviderNPINumber, "ECFMG.pdf", "ECFMG Certificate"),
                                 // Mandatory Fields


                             }).ToList();
                if (profile.ECFMGDetail == null)
                    profile.ECFMGDetail = new ECFMGDetail();
                foreach (var item in ecfmg)
                {
                    profile.ECFMGDetail = item;
                    listBox1.Items.Add("\tAdding ECFMG Details");
                    listBox1.Items.Add("\t\tECFMG Number\t" + ecfmg.First().ECFMGNumber);
                    listBox1.Items.Add("\t\tECFMG Issue Date\t" + ecfmg.First().ECFMGIssueDate);
                    listBox1.Items.Add("\t\tECFMG Certificate Path\t" + ecfmg.First().ECFMGCertPath);

                }
                #endregion

                #region Education Details - Multiple Documents done
                // School Information

                var schoolInfos = (from x1 in xmlDoc.Descendants("d_prf_education_dates")
                                   from x in x1.Descendants("d_prf_education_dates_row")

                                   select

                                   new AHC.CD.Entities.MasterProfile.EducationHistory.EducationDetail
                                   {
                                       QualificationDegree = x.Element("code_lookup_degree").Value.Length > 0 ? x.Element("code_lookup_degree").Value : "Not Available",
                                       SchoolInformation = new SchoolInformation
                                       {
                                           SchoolName = x.Element("address_lookup_school_code").Value.Length > 0 ? x.Element("address_lookup_school_code").Value : "Not Available",
                                           //Building = "Not Available",
                                           //Street = "Not Available",
                                           //City = "Not Available",
                                           //State = "Not Available",
                                           //Country = "Not Available",
                                           //PhoneNumber = "Not Available",
                                           //ZipCode = "Not Available",

                                       },
                                       StartDate = x.Element("start_date").Value.Length > 0 ? DateTime.Parse(x.Element("start_date").Value) : default(DateTime?),
                                       StartYear = GetYear(x.Element("start_date").Value.Length > 0 ? DateTime.Parse(x.Element("start_date").Value) : DateTime.Parse("01/01/1900")),
                                       StartMonth = GetMonth(x.Element("start_date").Value.Length > 0 ? DateTime.Parse(x.Element("start_date").Value) : DateTime.Parse("01/01/1900")),
                                       EndDate = x.Element("end_date").Value.Length > 0 ? DateTime.Parse(x.Element("end_date").Value) : default(DateTime?),
                                       EndYear = GetYear(x.Element("end_date").Value.Length > 0 ? DateTime.Parse(x.Element("end_date").Value) : DateTime.Parse("01/01/1900")),
                                       EndMonth = GetMonth(x.Element("end_date").Value.Length > 0 ? DateTime.Parse(x.Element("end_date").Value) : DateTime.Parse("01/01/1900")),
                                       QualificationType = GetQualificationType(x.Element("code_lookup_education_type").Value),
                                       //.Length > 0 ? x.Element("code_lookup_education_type").Value : "Not Available",
                                       // Mandatory Fields
                                       IsUSGraduate = "Not Available",
                                       //QualificationType = "Not Available",
                                       //GraduationType = "Not Available",
                                       //IsCompleted = "Not Available",
                                       CertificatePath = GetFilePath(ProviderNPINumber, GetGraduationFileName(ProviderNPINumber, x.Element("address_lookup_school_code").Value, x.Element("code_lookup_degree").Value), "Graduation Certificate"),



                                   }).ToList();


                if (profile.EducationDetails == null)
                    profile.EducationDetails = new List<AHC.CD.Entities.MasterProfile.EducationHistory.EducationDetail>();



                foreach (var item in schoolInfos)
                {
                    //if (ecfmg != null && item.GraduationType == "Medical Training")
                    //    item.ECFMGDetail = ecfmg.First();
                    profile.EducationDetails.Add(item);
                    listBox1.Items.Add("\tAdding Education Details");
                    listBox1.Items.Add("\t\tQualification Degree: " + item.QualificationDegree);
                    listBox1.Items.Add("\t\tSchool Name: " + item.SchoolInformation.SchoolName);
                    listBox1.Items.Add("\t\tSchool Phone: " + item.SchoolInformation.Phone);
                    listBox1.Items.Add("\t\tStart Date: " + item.StartDate);
                    listBox1.Items.Add("\t\tEnd Date: " + item.EndDate);
                    listBox1.Items.Add("\t\tGraduation Type: " + item.GraduationType);
                    listBox1.Items.Add("\t\tGraduation Certificate: " + item.CertificatePath);

                }

                #endregion

                #region CME Certifications - Specialities Certificate - Documents Done
                //string qd;
                //int ch;
                //string sn;
                listBox1.Items.Add("Adding CME Certifications");
                var cmes = (from x1 in xmlDoc.Descendants("d_prf_special_certs_dates")
                            from x in x1.Descendants("d_prf_special_certs_dates_row")
                            select
                            new AHC.CD.Entities.MasterProfile.EducationHistory.CMECertification
                            {
                                StartDate = x.Element("pd_special_certs_start_date").Value.Length > 0 ? DateTime.Parse(x.Element("pd_special_certs_start_date").Value) : default(DateTime?),
                                StartYear = GetYear(x.Element("pd_special_certs_start_date").Value.Length > 0 ? DateTime.Parse(x.Element("pd_special_certs_start_date").Value) : DateTime.Parse("01/01/1900")),
                                StartMonth = GetMonth(x.Element("pd_special_certs_start_date").Value.Length > 0 ? DateTime.Parse(x.Element("pd_special_certs_start_date").Value) : DateTime.Parse("01/01/1900")),
                                EndDate = x.Element("pd_special_certs_end_date").Value.Length > 0 ? DateTime.Parse(x.Element("pd_special_certs_end_date").Value) : default(DateTime?),
                                EndYear = GetYear(x.Element("pd_special_certs_end_date").Value.Length > 0 ? DateTime.Parse(x.Element("pd_special_certs_end_date").Value) : DateTime.Parse("01/01/1900")),
                                EndMonth = GetMonth(x.Element("pd_special_certs_end_date").Value.Length > 0 ? DateTime.Parse(x.Element("pd_special_certs_end_date").Value) : DateTime.Parse("01/01/1900")),
                                Certification = x.Element("certified_in").Value.Length > 0 ? x.Element("certified_in").Value : "Not Available",
                                //CertificatePath = GetFilePath(ProviderNPINumber, GetCMEFileName(out qd, out ch, out sn), "CME Certification",
                                //x.Element("pd_special_certs_end_date").Value.Length > 0 ? DateTime.Parse(x.Element("pd_special_certs_end_date").Value) : DateTime.Parse("01/01/1900")

                                //),
                                // Mandatory Fields

                                QualificationDegree = "Not Available",
                                //CreditHours = 0,
                                //SponsorName = "Not Available"


                            }).ToList();
                if (profile.CMECertifications == null)
                    profile.CMECertifications = new List<CMECertification>();

                foreach (var item in cmes)
                {
                    profile.CMECertifications.Add(item);
                    listBox1.Items.Add("\tAdding Speciality Certificates");
                    listBox1.Items.Add("\t\tCertification\t" + item.Certification);
                    listBox1.Items.Add("\t\tStart Date\t" + item.StartDate);
                    listBox1.Items.Add("\t\tEnd Date\t" + item.EndDate);
                    listBox1.Items.Add("\t\tQualification Degree\t" + item.QualificationDegree);
                    listBox1.Items.Add("\t\tCredit Hours\t" + item.CreditHours);
                }

                // Add the extra CME Certification Details and Documents from XL Sheet
                AddCMECertificationExtraDetails(profile.CMECertifications, ProviderNPINumber);

                #endregion

                #region Education History - Training - Residency Internship - Multiple Documents Done

                // Education History - Training

                listBox1.Items.Add("Adding Education History Information");
                var residencyInternshipDetails = (from x1 in xmlDoc.Descendants("d_prf_training_dates")
                                                  from x in x1.Descendants("d_prf_training_dates_row")
                                                  select
                                                  new AHC.CD.Entities.MasterProfile.EducationHistory.ResidencyInternshipDetail
                                                  {
                                                      ProgramType = x.Element("code_lookup_train_type").Value.Length > 0 ? x.Element("code_lookup_train_type").Value : "Not Available",
                                                      SpecialtyID = GetSpecialityID(x.Element("code_lookup_specialty").Value.Length > 0 ? x.Element("code_lookup_specialty").Value : "Not Available"),
                                                      StartDate = x.Element("start_date").Value.Length > 0 ? DateTime.Parse(x.Element("start_date").Value) : default(DateTime?),
                                                      StartYear = GetYear(x.Element("start_date").Value.Length > 0 ? DateTime.Parse(x.Element("start_date").Value) : DateTime.Parse("01/01/1900")),
                                                      StartMonth = GetMonth(x.Element("start_date").Value.Length > 0 ? DateTime.Parse(x.Element("start_date").Value) : DateTime.Parse("01/01/1900")),
                                                      EndDate = x.Element("end_date").Value.Length > 0 ? DateTime.Parse(x.Element("end_date").Value) : default(DateTime?),
                                                      EndYear = GetYear(x.Element("end_date").Value.Length > 0 ? DateTime.Parse(x.Element("end_date").Value) : DateTime.Parse("01/01/1900")),
                                                      EndMonth = GetMonth(x.Element("end_date").Value.Length > 0 ? DateTime.Parse(x.Element("end_date").Value) : DateTime.Parse("01/01/1900")),
                                                      // Mandatory Fields
                                                      Preference = "Not Available",
                                                      //DirectorName = "Not Available",
                                                      //DocumentPath = "Not Available",


                                                  }).ToList();


                var schoolNames = (from x1 in xmlDoc.Descendants("d_prf_training_dates")
                                   from x in x1.Descendants("d_prf_training_dates_row")
                                   select
                                   new AHC.CD.Entities.MasterProfile.EducationHistory.SchoolInformation
                                   {
                                       SchoolName = x.Element("address_lookup_institution_code").Value.Length > 0 ? x.Element("address_lookup_institution_code").Value : "Not Available",

                                       // Mandatory Fields
                                       //Building = "Not Available",
                                       //Street = "Not Available",
                                       //City = "Not Available",
                                       //State = "Not Available",
                                       //Country = "Not Available",
                                       //PhoneNumber = "Not Available",


                                   }).ToList();




                if (profile.TrainingDetails == null)
                    profile.TrainingDetails = new List<TrainingDetail>();



                int index = 0;

                foreach (var item in residencyInternshipDetails)
                {
                    TrainingDetail td = new TrainingDetail { HospitalName = "Not Available" };
                    td.IsCompleted = "Not Available";

                    if (td.ResidencyInternshipDetails == null)
                        td.ResidencyInternshipDetails = new List<ResidencyInternshipDetail>();


                    listBox1.Items.Add("\tAdding Training Information");
                    listBox1.Items.Add("\t\tProgram Type: " + item.ProgramType);
                    listBox1.Items.Add("\t\tSpecialty ID: " + item.SpecialtyID);
                    //listBox1.Items.Add("\t\tSpecialty : " + context.Specialities.Find(item.SpecialtyID).Name);
                    listBox1.Items.Add("\t\tStart Date: " + item.StartDate);
                    listBox1.Items.Add("\t\tEnd Date: " + item.EndDate);

                    listBox1.Items.Add("\t\tSchool Name: " + schoolNames[index].SchoolName);
                    listBox1.Items.Add("\t\tIs Completed: " + td.IsCompleted);
                    listBox1.Items.Add("\t\tIs Director Name: " + item.DirectorName);
                    td.ResidencyInternshipDetails.Add(item);
                    td.SchoolInformation = schoolNames[index++];
                    profile.TrainingDetails.Add(td);
                }

                // Add Aditional Details from XL with Documents
                AddResidencyInternshipDetails(ProviderNPINumber, profile.TrainingDetails);

                #endregion

                #region Education History - Under Graduation - Documents done
                //AddUnderGraduateDetails(ProviderNPINumber, profile);

                // Read the CSV file
                StreamReader reader = new StreamReader(@"D:\AHCDBMigration\CSV\UnderGraduate.csv");
                while (!reader.EndOfStream)
                {
                    // Create the EducationDetail and add to Profile
                    string ugData = reader.ReadLine();
                    string[] data = ugData.Split(',');
                    if (data[0] == ProviderNPINumber)
                    {
                        // 0-NPI, 1-File, 2-School Name, 3-Degree Awarded, 4-Start Date, 5-End Date, 6-Completed Here, 7-Street Address, 8-City, 9-State, 10-Country, 11-Zip

                        EducationDetail ed = new EducationDetail();
                        ed.CertificatePath = GetFilePath(ProviderNPINumber, data[1] + ".pdf", "Under Graduation");
                        ed.IsUSGraduate = "Not Available";
                        ed.EducationQualificationType = EducationQualificationType.UnderGraduate;
                        ed.QualificationDegree = data[3];
                        ////ed.StartDate = DateTime.Parse(data[4]));
                        ////ed.EndDate = DateTime.Parse(data[5]));
                        ed.StartYear = GetYear(DateTime.Parse(data[4]));
                        ed.StartMonth = GetMonth(DateTime.Parse(data[4]));
                        ed.EndYear = GetYear(DateTime.Parse(data[5]));
                        ed.EndMonth = GetMonth(DateTime.Parse(data[5]));

                        if (!string.IsNullOrEmpty(data[4])) 
                        { 
                            ed.IsCompleted = data[6];
                        }
                        SchoolInformation si = new SchoolInformation();
                        si.SchoolName = data[2];
                        si.Street = data[7];
                        si.Country = data[10];
                        si.ZipCode = data[11];
                        ed.SchoolInformation = si;
                        if (profile.EducationDetails == null)
                        { 
                            profile.EducationDetails = new List<EducationDetail>();
                        }
                        profile.EducationDetails.Add(ed);
                    }
                }

                #endregion

                    #region Work Experience - No Documents Available
                    // Work Experience

                    listBox1.Items.Add("Adding Work History");
                    var workExperiences = (from x1 in xmlDoc.Descendants("d_scr_prof_experience_dates")
                                           from x in x1.Descendants("d_scr_prof_experience_dates_row")
                                           select new AHC.CD.Entities.MasterProfile.WorkHistory.ProfessionalWorkExperience
                                           {
                                               StartDate = x.Element("start_date").Value.Length > 0 ? DateTime.Parse(x.Element("start_date").Value) : default(DateTime?),
                                               EndDate = x.Element("end_date").Value.Length > 0 ? DateTime.Parse(x.Element("end_date").Value) : default(DateTime?),
                                               EmployerName = x.Element("organization").Value.Length > 0 ? x.Element("organization").Value : "Not Available",
                                               Street = x.Element("street_1").Value.Length > 0 ? x.Element("street_1").Value : null,
                                               City = x.Element("city").Value.Length > 0 ? x.Element("city").Value : null,
                                               ZipCode = x.Element("zip").Value.Length > 0 ? x.Element("zip").Value : null,
                                               State = GetState(x.Element("state").Value.Length > 0 ? x.Element("state").Value : "Not Available"),
                                               EmployerEmail = x.Element("e_mail_address").Value.Length > 0 ? x.Element("e_mail_address").Value : null,
                                               EmployerFax = FormatPhone(x.Element("fax").Value.Length > 0 ? x.Element("fax").Value : null),
                                               Country = GetCountry(x.Element("country").Value.Length > 0 ? x.Element("country").Value : null),
                                               WorkExperienceDocPath = GetFilePath(ProviderNPINumber, "WE1.pdf", "Work Experience"),
                                               // Mandatory Fields
                                               //Building = "Not Available",
                                               CanContactEmployer = "Not Available",
                                               CurrentlyWorking = "Not Available",
                                               ProviderTypeID = GetProviderTypeID("Not Available")
                                           }).ToList();


                    if (profile.ProfessionalWorkExperiences == null)
                        profile.ProfessionalWorkExperiences = new List<AHC.CD.Entities.MasterProfile.WorkHistory.ProfessionalWorkExperience>();
                    foreach (var item in workExperiences)
                    {
                        profile.ProfessionalWorkExperiences.Add(item);
                        listBox1.Items.Add("\tAdding Professional Work Experience");
                        listBox1.Items.Add("\t\tStart Date: " + item.StartDate);
                        listBox1.Items.Add("\t\tEnd Date: " + item.EndDate);
                        listBox1.Items.Add("\t\tEmployeer Name: " + item.EmployerName);
                        listBox1.Items.Add("\t\tEmployeer Email: " + item.EmployerEmail);
                        listBox1.Items.Add("\t\tEmployeer Fax: " + item.EmployerFax);
                        listBox1.Items.Add("\t\tStreet : " + item.Street);
                        listBox1.Items.Add("\t\tCity: " + item.City);
                        listBox1.Items.Add("\t\tState: " + item.State);
                        listBox1.Items.Add("\t\tCountry: " + item.Country);
                        listBox1.Items.Add("\t\tZipCode: " + item.ZipCode);
                        listBox1.Items.Add("\t\tWork Experience Doc Path: " + item.WorkExperienceDocPath);

                    }


                    #endregion

                    #region Hospital Affiliation - Privilage - Documents Done
                    // Hospital Privilate Details

                    listBox1.Items.Add("Adding Hospital Privilage Information");
                    var hospitalPrivilages = (from x1 in xmlDoc.Descendants("d_prf_hospital_affil_dates")
                                              from x in x1.Descendants("d_prf_hospital_affil_dates_row")
                                              select new AHC.CD.Entities.MasterProfile.HospitalPrivilege.HospitalPrivilegeDetail
                                              {
                                                  
                                                  HospitalID = GetHospitalID(x.Element("address_lookup_hospital_code").Value.Length > 0 ? x.Element("address_lookup_hospital_code").Value : "Not Available"),
                                                  StaffCategoryID = GetStaffCateogryID(x.Element("code_lookup_staff_category").Value.Length > 0 ? x.Element("code_lookup_staff_category").Value : "Not Available"),
                                                  AffilicationStartDate = x.Element("pd_hosp_affil_start_date").Value.Length > 0 ? DateTime.Parse(x.Element("pd_hosp_affil_start_date").Value) : default(DateTime?),
                                                  AffiliationEndDate = x.Element("pd_hosp_affil_end_date").Value.Length > 0 ? DateTime.Parse(x.Element("pd_hosp_affil_end_date").Value) : default(DateTime?),
                                                  AdmittingPrivilegeID = GetAdmittingPrivilegeID(x.Element("code_lookup_admitting_priv").Value),
                                                  // Mandatory Fields
                                                  //SpecialtyID = null,
                                                  Preference = "Not Available",
                                                  //DepartmentName = "Not Available",
                                                  //DepartmentChief = "Not Available",
                                                  //FullUnrestrictedPrevilages = "Not Available",
                                                  ///////HospitalPrevilegeLetterPath = GetFilePath(ProviderNPINumber,"HPL1.pdf","Hospital Previlege Letter"),
                                              }).ToList();

                    if (profile.HospitalPrivilegeInformation == null)
                        profile.HospitalPrivilegeInformation = new AHC.CD.Entities.MasterProfile.HospitalPrivilege.HospitalPrivilegeInformation { HasHospitalPrivilege = "Not Available" };

                    if (profile.HospitalPrivilegeInformation.HospitalPrivilegeDetails == null)
                    {
                        profile.HospitalPrivilegeInformation.HospitalPrivilegeDetails = new List<AHC.CD.Entities.MasterProfile.HospitalPrivilege.HospitalPrivilegeDetail>();
                    }

                    foreach (var item in hospitalPrivilages)
                    {
                        profile.HospitalPrivilegeInformation.HospitalPrivilegeDetails.Add(item);
                        listBox1.Items.Add("\tAdding Hospital Privilege Details");
                        listBox1.Items.Add("\t\tHospital ID: " + item.HospitalID);
                        //listBox1.Items.Add("\t\tHospital Name: " + context.Hospitals.Find(item.HospitalID).HospitalName);
                        listBox1.Items.Add("\t\tStaff Category ID: " + item.StaffCategoryID);
                        //listBox1.Items.Add("\t\tStaff Category : " + context.StaffCategories.Find(item.StaffCategoryID).Title);
                        listBox1.Items.Add("\t\tAdmitting Privilage Status ID: " + item.AdmittingPrivilegeID);
                        //listBox1.Items.Add("\t\tAdmitting Privilage Status : " + context.AdmittingPrivileges.Find(item.AdmittingPrivilegeID).Status);
                        //listBox1.Items.Add("\t\tAdmitting Privilage Status : " + (item.AdmittingPrivilegeID == true ? context.AdmittingPrivileges.Find(item.AdmittingPrivilegeID).Title : ""));
                        listBox1.Items.Add("\t\tAffiliation Start Date: " + item.AffilicationStartDate);
                        listBox1.Items.Add("\t\tAffiliation End Date: " + item.AffiliationEndDate);
                        listBox1.Items.Add("\t\tPreference : " + item.Preference);
                        listBox1.Items.Add("\t\tDepartment Name: " + item.DepartmentName);
                        listBox1.Items.Add("\t\tDepartment Chief: " + item.DepartmentChief);
                        listBox1.Items.Add("\t\tFull Unrestricted Previlages: " + item.FullUnrestrictedPrevilages);
                        listBox1.Items.Add("\t\tHospital Previlege Letter Path: " + item.HospitalPrevilegeLetterPath);
                    }

                    if (hospitalPrivilages.Count > 0)
                        profile.HospitalPrivilegeInformation.HospitalPrivilegeYesNoOption = YesNoOption.YES;

                    // Add Aditional Hospital Privilage Information

                     // Read the CSV file
                    StreamReader sreader = new StreamReader(@"D:\AHCDBMigration\CSV\HospitalPrev.csv");
                    while (!sreader.EndOfStream)
                    {
                        // 0-NPI,1-File,2-Hospital Name, 3-Department Chief, 4-Affiliation Start Date, 
                        // 5-Affiliation End Date, 6-Privilage Status, 7-Speciality, 8-Street, 9-City, 10-State, 11-Zip
                        string hospitalData = sreader.ReadLine();
                        string[] hdata = hospitalData.Split(',');
                        if (hdata[0] == ProviderNPINumber)
                        {
                            HospitalPrivilegeDetail hpd = new HospitalPrivilegeDetail();
                            hpd.DepartmentChief = hdata[3];
                            hpd.HospitalPrevilegeLetterPath = GetFilePath(ProviderNPINumber, hdata[1] + ".pdf", "Hospital Prievilage");
                            hpd.SpecialtyID = GetSpecialityID(hdata[7]);
                            hpd.HospitalID = GetHospitalIDWithContact(hdata[2], hdata[8], hdata[9], hdata[10], hdata[11]);
                            //var culture = System.Globalization.CultureInfo.InvariantCulture;
                            //IFormatProvider culture = new System.Globalization.CultureInfo(String.Empty, false);
                            try
                            {
                                hpd.AffilicationStartDate = DateTime.Parse(hdata[4]);
                                hpd.AffiliationEndDate = DateTime.Parse(hdata[5]);
                            }
                            catch{}

                            profile.HospitalPrivilegeInformation.HospitalPrivilegeDetails.Add(hpd);
                        }
                    }


                    #endregion

                    #region State License
                    // License

                    listBox1.Items.Add("Adding State License Information");
                    var stateLicenseInformations = (from x1 in xmlDoc.Descendants("d_scr_license_dates")
                                                    from x in x1.Descendants("d_scr_license_dates_row")
                                                    select new AHC.CD.Entities.MasterProfile.IdentificationAndLicenses.StateLicenseInformation
                                                    {
                                                        LicenseNumber = GetNumber(x.Element("license_number").Value.Length > 0 ? x.Element("license_number").Value : "Not Available-" + Guid.NewGuid()),
                                                        ExpiryDate = x.Element("expiration_date").Value.Length > 0 ? DateTime.Parse(x.Element("expiration_date").Value) : default(DateTime?),
                                                        IssueDate = x.Element("issue_date").Value.Length > 0 ? DateTime.Parse(x.Element("issue_date").Value) : default(DateTime?),

                                                        IssueState = GetState(x.Element("state").Value.Length > 0 ? x.Element("state").Value : null),
                                                        StateLicenseStatus = GetStateLicenseStatus(x.Element("active_status").Value),
                                                        //StateLicenseDocumentPath = GetFilePath(ProviderNPINumber, "ML1.pdf","State License"),
                                                        // Mandatory Fields
                                                        ProviderTypeID = GetProviderTypeID("Not Available"),
                                                        //IsCurrentPracticeState = "Not Available",
                                                        //LicenseInGoodStanding = "Not Available",
                                                        StateLicenseStatusID = GetStateLicenseStatus(x.Element("active_status").Value).StateLicenseStatusID
                                                    }).ToList();

                    if (profile.StateLicenses == null)
                        profile.StateLicenses = new List<StateLicenseInformation>();

                    foreach (var item in stateLicenseInformations)
                    {
                        listBox1.Items.Add("\tState License Information");
                        profile.StateLicenses.Add(item);
                        listBox1.Items.Add("\t\tLicense Number: " + item.LicenseNumber);
                        listBox1.Items.Add("\t\tExpiry Date: " + item.ExpiryDate);
                        listBox1.Items.Add("\t\tIssue Date: " + item.IssueDate);
                        //listBox1.Items.Add("\t\tIssuing State: " + item.IssuingState);
                        listBox1.Items.Add("\t\tIs Current Practice State: " + item.IsCurrentPracticeState);
                        listBox1.Items.Add("\t\tLicense In Good Standing: " + item.LicenseInGoodStanding);
                        listBox1.Items.Add("\t\tState License Status: " + item.StateLicenseStatus.Status);
                        listBox1.Items.Add("\t\tState License Path: " + item.StateLicenseDocumentPath);
                        listBox1.Items.Add("\t\tProvider Type : " + context.ProviderTypes.Find(item.ProviderTypeID).Description);
                    }
                    #endregion

                    #region Board Specialities

                    // Specialities
                    listBox1.Items.Add("Adding Specialities");
                    var specialityDetails = (from x1 in xmlDoc.Descendants("d_prf_specialties_dates")
                                             from x in x1.Descendants("d_prf_specialties_dates_row")

                                             select new AHC.CD.Entities.MasterProfile.BoardSpecialty.SpecialtyDetail
                                             {
                                                 SpecialtyID = GetSpecialityID(x.Element("code_lookup_specialty").Value),

                                                 SpecialtyBoardCertifiedDetail = new SpecialtyBoardCertifiedDetail()
                                                 {

                                                     ExpirationDate = x.Element("expiration_date").Value.Length > 0 ? DateTime.Parse(x.Element("expiration_date").Value) : default(DateTime?),
                                                     // Mandatory Fields
                                                     //InitialCertificationDate = DateTime.Parse("01/01/1900"),
                                                     //LastReCerificationDate = DateTime.Parse("01/01/1900"),
                                                     ///////BoardCertificatePath = GetFilePath(ProviderNPINumber,"Board1.pdf","Board Certificate",x.Element("expiration_date").Value.Length > 0 ? DateTime.Parse(x.Element("expiration_date").Value) : DateTime.Parse("01/01/1900")),
                                                     SpecialtyBoardID = GetSpecialityBoardID(x.Element("address_lookup_board_code").Value),
                                                 },

                                                 // Mandatory Fields
                                                 SpecialtyPreference = "Not Available",
                                                 //IsCurrentlyPracting = "Not Available",
                                                 //PercentageOfTime = 0.0,
                                                 //ListedInHMO = "Not Available",
                                                 //ListedInPPO = "Not Available",
                                                 //ListedInPOS = "Not Available",
                                                 IsBoardCertified = "Not Available",

                                             }).ToList();

                    if (profile.SpecialtyDetails == null)
                        profile.SpecialtyDetails = new List<SpecialtyDetail>();

                    foreach (var item in specialityDetails)
                    {
                        listBox1.Items.Add("\tAdding Speciality Details");
                        profile.SpecialtyDetails.Add(item);
                        listBox1.Items.Add("\t\tSpeciality ID: " + item.SpecialtyID);
                       // listBox1.Items.Add("\t\tSpeciality : " + context.Specialities.Find(item.SpecialtyID).Name);
                        listBox1.Items.Add("\t\tSpecialty Preference: " + item.SpecialtyPreference);
                        listBox1.Items.Add("\t\tIsCurrentlyPracting: " + item.IsCurrentlyPracting);
                        listBox1.Items.Add("\t\tPercentageOfTime: " + item.PercentageOfTime);
                        listBox1.Items.Add("\t\tListedInHMO: " + item.ListedInHMO);
                        listBox1.Items.Add("\t\tListedInPPO: " + item.ListedInPPO);
                        listBox1.Items.Add("\t\tListedInPOS: " + item.ListedInPOS);
                        listBox1.Items.Add("\t\tIsBoardCertified: " + item.IsBoardCertified);
                        listBox1.Items.Add("\t\tSpeciality Board ID: " + item.SpecialtyBoardCertifiedDetail.SpecialtyBoardID);
                       // listBox1.Items.Add("\t\tSpeciality Board : " + context.SpecialtyBoards.Find(item.SpecialtyBoardCertifiedDetail.SpecialtyBoardID).Name);
                        listBox1.Items.Add("\t\tExpiration Date: " + item.SpecialtyBoardCertifiedDetail.ExpirationDate);
                        listBox1.Items.Add("\t\tInitial Certification Date: " + item.SpecialtyBoardCertifiedDetail.InitialCertificationDate);
                        listBox1.Items.Add("\t\tLast ReCerification Date: " + item.SpecialtyBoardCertifiedDetail.LastReCerificationDate);
                        listBox1.Items.Add("\t\tBoard Certificate Path: " + item.SpecialtyBoardCertifiedDetail.BoardCertificatePath);

                    }

                    AddBoardSpecialities(ProviderNPINumber, profile.SpecialtyDetails);

                    #endregion

                    #region Professional References

                    listBox1.Items.Add("Adding Professional References");
                    var profRefs = (from x1 in xmlDoc.Descendants("d_prf_references")
                                    from x in x1.Descendants("d_prf_references_row")
                                    select new AHC.CD.Entities.MasterProfile.ProfessionalReference.ProfessionalReferenceInfo
                                    {
                                        State = GetState(x.Element("pd_references_state").Value.Length > 0 ? x.Element("pd_references_state").Value : null),
                                        LastName = x.Element("pd_references_last_name").Value.Length > 0 ? x.Element("pd_references_last_name").Value : "Not Available",
                                        Zipcode = x.Element("pd_references_zip").Value.Length > 0 ? x.Element("pd_references_zip").Value : null,
                                        PhoneNumber = FormatPhone(x.Element("pd_references_cust_5").Value.Length > 0 ? x.Element("pd_references_cust_5").Value : null),
                                        Street = x.Element("pd_references_street").Value.Length > 0 ? x.Element("pd_references_street").Value : null,
                                        City = x.Element("pd_references_city").Value.Length > 0 ? x.Element("pd_references_city").Value : null,
                                        County = GetCountry(x.Element("pd_references_country").Value.Length > 0 ? x.Element("pd_references_country").Value : null),
                                        FirstName = x.Element("pd_references_first_middle_name").Value.Length > 0 ? x.Element("pd_references_first_middle_name").Value : "Not Available",
                                        ProviderTypeID = GetProviderTypeID(x.Element("prof_suf_code").Value),
                                        //Status = StatusType.Active,
                                        // Mandatory Fields
                                        //Degree = "Not Available",
                                        //UnitNumber = "Not Available",
                                        //Building = "Not Available",

                                        //Country = "Not Available",
                                        //Zipcode = "Not Available",
                                        //Fax = "Not Available",
                                        //IsBoardCerified = "Not Available",
                                    }).ToList();

                    if (profile.ProfessionalReferenceInfos == null)
                        profile.ProfessionalReferenceInfos = new List<ProfessionalReferenceInfo>();

                    foreach (var item in profRefs)
                    {
                        listBox1.Items.Add("\tAdding Professional Reference");
                        profile.ProfessionalReferenceInfos.Add(item);
                        listBox1.Items.Add("\t\tFirst Name: " + item.FirstName);
                        listBox1.Items.Add("\t\tLast Name: " + item.LastName);
                        listBox1.Items.Add("\t\tDegree: " + item.Degree);
                        listBox1.Items.Add("\t\tTelephone: " + item.Telephone);
                        //listBox1.Items.Add("\t\tUnit Number: " + item.UnitNumber);
                        listBox1.Items.Add("\t\tBuilding: " + item.Building);
                        listBox1.Items.Add("\t\tStreet: " + item.Street);
                        listBox1.Items.Add("\t\tCity: " + item.City);
                        listBox1.Items.Add("\t\tState: " + item.State);
                        listBox1.Items.Add("\t\tCounty: " + item.County);
                        listBox1.Items.Add("\t\tCountry: " + item.Country);
                        listBox1.Items.Add("\t\tZipcode: " + item.Zipcode);
                        listBox1.Items.Add("\t\tFax: " + item.Fax);
                    }

                    #endregion

                    #region Languages

                    listBox1.Items.Add("Adding Languages");
                    var languages = (from x1 in xmlDoc.Descendants("d_prf_languages")
                                     from x in x1.Descendants("d_prf_languages_row")
                                     select new AHC.CD.Entities.MasterProfile.Demographics.KnownLanguage
                                     {
                                         Language = x.Element("code_lookup_foreign_language").Value.Length > 0 ? x.Element("code_lookup_foreign_language").Value : "Not Available",
                                         // Mandatory Fields
                                         //ProficiencyIndex = 0,
                                     }).ToList();

                    if (profile.LanguageInfo == null)
                    {
                        profile.LanguageInfo = new LanguageInfo();
                        profile.LanguageInfo.CanSpeakAmericanSignLanguage = "Not Available";
                        profile.LanguageInfo.KnownLanguages = new List<KnownLanguage>();
                    }

                    foreach (var item in languages)
                    {
                        listBox1.Items.Add("\tLanguages");
                        profile.LanguageInfo.KnownLanguages.Add(item);
                        listBox1.Items.Add("\t\tLanguage: " + item.Language);
                        listBox1.Items.Add("\t\tProficiency Index: " + item.ProficiencyIndex);
                    }



                    #endregion

                    #region Provider Disclouser Answers

                    listBox1.Items.Add("Adding Provider Disclouser Answers");
                    var disclouserAns = (from x1 in xmlDoc.Descendants("d_prf_att_quest_new")
                                         from x in x1.Descendants("d_prf_att_quest_new_row")

                                         select new AHC.CD.Entities.MasterProfile.DisclosureQuestions.ProfileDisclosureQuestionAnswer
                                        {
                                            QuestionID = GetQuestionID(x.Element("question_lookup_full_quest").Value),
                                            ProviderDisclousreAnswer = x.Element("code_lookup_description").Value.Length > 0 ? x.Element("code_lookup_description").Value : null,
                                            Reason = x.Element("explain_yes").Value,
                                            // Mandatory Fields

                                        }).ToList();
                    if (profile.ProfileDisclosure == null)
                    {
                        profile.ProfileDisclosure = new ProfileDisclosure();
                        profile.ProfileDisclosure.ProfileDisclosureQuestionAnswers = new List<AHC.CD.Entities.MasterProfile.DisclosureQuestions.ProfileDisclosureQuestionAnswer>();
                    }


                    foreach (var item in disclouserAns)
                    {
                        profile.ProfileDisclosure.ProfileDisclosureQuestionAnswers.Add(item);
                        listBox1.Items.Add("\tAdding Disclouser Answers");
                        listBox1.Items.Add("\t\tQuestion :" + context.Questions.Find(item.QuestionID).Title);
                        listBox1.Items.Add("\tProvider Answer : " + item.ProviderDisclousreAnswer);
                        listBox1.Items.Add("\tReason : " + item.Reason);
                    }

                    #endregion

                    #region Practice Locations - Office - Practice Location Detail and Office Hours - Billing Contact

                    listBox1.Items.Add("Adding Practice Locations Details");
                    var practiceLocationDetails = (from x1 in xmlDoc.Descendants("d_prf_office")
                                                   from x in x1.Descendants("d_prf_office_row")
                                                   select new AHC.CD.Entities.MasterProfile.PracticeLocation.PracticeLocationDetail
                                                  {

                                                      FacilityId = GetFacility(x.Element("code_lookup_practice_type").Value,
                                                                              x.Element("pd_address_street").Value,
                                                                              x.Element("pd_address_city").Value,
                                                                              GetState(x.Element("pd_address_state").Value),
                                                                              GetCountry(x.Element("pd_address_country").Value),
                                                                              FormatPhone(x.Element("pd_address_phone").Value),
                                                                              FormatPhone(x.Element("pd_address_fax").Value),
                                                                              x.Element("pd_address_zip").Value,
                                                                              x.Element("code_lookup_county").Value
                                                                              ),
                                                      // Mandatory Fields
                                                      IsPrimary = "Not Available",
                                                      CurrentlyPracticingAtThisAddress = "Not Available",
                                                      OfficeHour = GetOfficeHour(
                                                      x.Element("pd_address_mon_from").Value.Length > 0 ? x.Element("pd_address_mon_from").Value : "Not Available",
                                                      x.Element("pd_address_tue_from").Value.Length > 0 ? x.Element("pd_address_tue_from").Value : "Not Available",
                                                      x.Element("pd_address_wed_from").Value.Length > 0 ? x.Element("pd_address_wed_from").Value : "Not Available",
                                                      x.Element("pd_address_thu_from").Value.Length > 0 ? x.Element("pd_address_thu_from").Value : "Not Available",
                                                      x.Element("pd_address_fri_from").Value.Length > 0 ? x.Element("pd_address_fri_from").Value : "Not Available",
                                                      x.Element("pd_address_sat_from").Value.Length > 0 ? x.Element("pd_address_sat_from").Value : "Not Available",
                                                      x.Element("pd_address_sun_from").Value.Length > 0 ? x.Element("pd_address_sun_from").Value : "Not Available",
                                                      x.Element("pd_address_mon_to2").Value.Length > 0 ? x.Element("pd_address_mon_to2").Value : "Not Available",
                                                      x.Element("pd_address_tue_to2").Value.Length > 0 ? x.Element("pd_address_tue_to2").Value : "Not Available",
                                                      x.Element("pd_address_wed_to2").Value.Length > 0 ? x.Element("pd_address_wed_to2").Value : "Not Available",
                                                      x.Element("pd_address_thu_to2").Value.Length > 0 ? x.Element("pd_address_thu_to2").Value : "Not Available",
                                                      x.Element("pd_address_fri_to2").Value.Length > 0 ? x.Element("pd_address_fri_to2").Value : "Not Available",
                                                      x.Element("pd_address_sat_to2").Value.Length > 0 ? x.Element("pd_address_sat_to2").Value : "Not Available",
                                                      x.Element("pd_address_sun_to2").Value.Length > 0 ? x.Element("pd_address_sun_to2").Value : "Not Available"
                                                      ),
                                                      //BillingContactPersonId = GetBillingContactPerson
                                                      //(
                                                      //x.Element("pd_address_street").Value.Length > 0 ? x.Element("pd_address_street").Value : "Not Available",
                                                      //x.Element("pd_address_city").Value.Length > 0 ? x.Element("pd_address_city").Value : "Not Available",
                                                      //x.Element("pd_address_state").Value.Length > 0 ? x.Element("pd_address_state").Value : "Not Available",
                                                      //x.Element("pd_address_country").Value.Length > 0 ? x.Element("pd_address_country").Value : "Not Available",
                                                      //x.Element("pd_address_phone").Value.Length > 0 ? x.Element("pd_address_phone").Value : "Not Available",
                                                      //x.Element("pd_address_fax").Value.Length > 0 ? x.Element("pd_address_fax").Value : "Not Available",
                                                      //x.Element("pd_address_zip").Value.Length > 0 ? x.Element("pd_address_zip").Value : "Not Available",
                                                      //    //x.Element("pd_address_county").Value.Length > 0 ? x.Element("pd_address_county").Value : "Not Available"
                                                      //x.Element("pd_address_e_mail_address").Value.Length > 0 ? x.Element("pd_address_e_mail_address").Value : "Not Available",
                                                      //x.Element("pd_address_street_2").Value.Length > 0 ? x.Element("pd_address_street_2").Value : "Not Available"
                                                      //),
                                                  }).ToList();

                    if (profile.PracticeLocationDetails == null)
                    {
                        profile.PracticeLocationDetails = new List<PracticeLocationDetail>();
                    }
                    foreach (var item in practiceLocationDetails)
                    {
                        profile.PracticeLocationDetails.Add(item);
                        //listBox1.Items.Add("\tAdding Facility Details");
                        //listBox1.Items.Add("\t\tName: " + f.Name);
                        //listBox1.Items.Add("\t\tCity: " + f.City);
                        //listBox1.Items.Add("\t\tState: " + f.State);
                        //listBox1.Items.Add("\t\tCountry: " + f.Country);
                        //listBox1.Items.Add("\t\tTelephone: " + f.Telephone);
                        //listBox1.Items.Add("\t\tFax: " + f.Fax);
                        //listBox1.Items.Add("\t\tZipCode: " + f.ZipCode);
                        ////listBox1.Items.Add("\t\tCounty: "+ f.County);
                        //listBox1.Items.Add("\tAdding Office Hours");
                        //listBox1.Items.Add("\tAdding Billing Contact Person");


                    }


                    #endregion

                    #region Contract Info - Documents done

                    ContractInfo contractInfo = new ContractInfo();
                    contractInfo.ContractFilePath = GetFilePath(ProviderNPINumber, "Contract.pdf", "Contract Info");
                    //contractInfo.CVDocumentPath = GetFilePath(ProviderNPINumber, "CV.pdf", "Contract Info");
                    if (contractInfo.ContractFilePath != null)// || contractInfo.CVDocumentPath != null)
                    {

                        contractInfo.ProviderRelationship = "Not Available";
                        //contractInfo.OrganizationId = GetOrganizationID();
                        if (profile.ContractInfoes == null)
                            profile.ContractInfoes = new List<ContractInfo>();
                        profile.ContractInfoes.Add(contractInfo);
                        listBox1.Items.Add("\tAdding Contract Info: ");
                        //listBox1.Items.Add("\t\tCV Document: " + contractInfo.CVDocumentPath);
                        listBox1.Items.Add("\t\tContract Document: " + contractInfo.ContractFilePath);
                        listBox1.Items.Add("\t\tProvider Relationship: " + contractInfo.ProviderRelationship);

                    }

                    #endregion

                    #region CV Document 
                    CVInformation cv = new CVInformation();
                    cv.CVDocumentPath = GetFilePath(ProviderNPINumber, "CV.pdf", "CV Information");
                    profile.CVInformation = cv;
                    #endregion

                    #region Save Changes to DB

                    context.Profiles.Add(profile);

                    try
                    {
                        //Creata new login user with role as provider - getting back the UserID

                        AHC.CD.Entities.CDUser user = new AHC.CD.Entities.CDUser();
                        user.StatusType = StatusType.Active;

                        EFUnitOfWork uow = new EFUnitOfWork();

                        //var cdRoleRepository = uow.GetGenericRepository<CDRole>();

                        //var providerRole = cdRoleRepository.Find(r => r.Code.Equals("PRO"), "CDUSers");
                        //if (providerRole == null)
                        //{
                        //    providerRole = new CDRole() { Code = "PRO", Name = "Provider", Status = "Active" };
                        //    cdRoleRepository.Create(providerRole);
                        //    cdRoleRepository.SaveAsync();
                        //}

                        //if (providerRole.CDUsers == null)
                        //{
                        //    providerRole.CDUsers = new List<CDUserRole>();
                        //}

                        //providerRole.CDUsers.Add(user);
                        //cdRoleRepository.Create(providerRole);
                        //cdRoleRepository.SaveAsync();

                        user.Profile = profile;
                        context.Users.Add(user);
                        context.SaveChanges();

                        AHC.CD.Data.Repository.IGenericRepository<CDRole> cdRoleRepository = new EFUnitOfWork().GetGenericRepository<CDRole>();

                        var providerRole = cdRoleRepository.Find(r => r.Code.Equals("PRO"), "CDUSers");
                        if (providerRole == null)
                        {
                            providerRole = new CDRole() { Code = "PRO", Name = "Provider", StatusType = StatusType.Active };
                            cdRoleRepository.Create(providerRole);
                            cdRoleRepository.Save();

                            if (providerRole.CDUsers == null)
                            {
                                providerRole.CDUsers = new List<CDUserRole>();
                            }
                        }
                            CDUserRole userRole = new CDUserRole() { CDUserId = user.CDUserID, CDRoleId = providerRole.CDRoleID };

                            providerRole.CDUsers.Add(userRole);
                            cdRoleRepository.Update(providerRole);
                            cdRoleRepository.Save();
                        

                        

                        

                        //Task.FromResult(new AHC.CD.Business.Users.CDRoleManager(uow).AssignProviderRoleAsync(user));
                        profile.ProviderID = SetProviderID(profile.ProfileID);

                        context.SaveChanges();
                        //MessageBox.Show("Migrated " + fileName);
                        File.Move(file, file + ".done");
                        continue;

                    #endregion

                        #region Log the Migration status
                        FileStream stream = File.Create(@"D:\AHCDBMigration\Logs3\" + fileName + ".txt");
                        StreamWriter writer = new StreamWriter(stream);
                        foreach (var item in listBox1.Items)
                        {
                            writer.WriteLine(item.ToString());
                        }
                        writer.Close();
                        stream.Close();
                        listBox1.Items.Clear();


                    }
                    catch (DbEntityValidationException ex)
                    {
                        // Retrieve the error messages as a list of strings.
                        var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                        // Join the list to a single string.
                        var fullErrorMessage = string.Join("; ", errorMessages);

                        // Combine the original exception message with the new one.
                        var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                        MessageBox.Show(exceptionMessage);
                    }
                    catch (Exception ex)
                    {
                    //    #region Log the Migration status
                        FileStream stream = File.Create(@"D:\AHCDBMigration\Logs3\ErrorLogs.txt");
                        StreamWriter writer = new StreamWriter(stream);
                        writer.WriteLine(file);
                        writer.WriteLine(ex.Message);
                        writer.Close();
                        stream.Close();
                        MessageBox.Show(ex.Message);
                        #endregion
                    }
                    //finally 
                    //{
                    //    try
                    //    {
                    //        File.Move(file, file + ".done");
                    //    }
                    //    catch { }
                    //}
                //}
                        //#endregion
            }
        }

        private ICollection<EmailDetail> GetEmailIds(string ProviderNPINumber)
        {
            var emails = new List<EmailDetail>();
            // Read the CSV file
            StreamReader reader = new StreamReader(@"D:\AHCDBMigration\CSV\Emails.csv");
            while (!reader.EndOfStream)
            {
                string emailData = reader.ReadLine();
                string[] data = emailData.Split(',');
                
                if (data[1] == ProviderNPINumber)
                {
                    EmailDetail ed = new EmailDetail { EmailAddress = data[0], Preference = "Primary", Status = "Active" };
                    emails.Add(ed);
                }
            }
            reader.Close();
            return emails.ToList();
        }

        private int? GetProviderLevel(string providerFile)
        {
            string providerLevel = providerFile.Contains("ARNP") ? "Mid-Level" : "Doctor";
            ProviderLevel pLevel = context.ProviderLevels.FirstOrDefault(pl => pl.Name == providerLevel);
            if(pLevel != null) // Master data found
            {
                return pLevel.ProviderLevelID;
            }
            else
            {
                pLevel = new ProviderLevel {Name = providerLevel, StatusType = StatusType.Active };
                context.ProviderLevels.Add(pLevel);
                context.SaveChanges();
                return pLevel.ProviderLevelID;
            }

        }

        private string GetLastNameFromSignature(XDocument xmlDoc)
        {
            var name = (from x in xmlDoc.Descendants("d_prf_profile_signature_row")

                        select new
                        {
                            LName = x.Element("v_full_name_full_name").Value.Length > 0 ? x.Element("v_full_name_full_name").Value : "Not Available"
                        });
            var lname = name.First().LName.Split(',')[0];
            return lname;
        }

        private string GetFirstNameFromSignature(XDocument  xmlDoc)
        {
            var name = (from x in xmlDoc.Descendants("d_prf_profile_signature_row")
                                                   
                                                   select new 
                                                  {
                                                      FName = x.Element("v_full_name_full_name").Value.Length > 0 ? x.Element("v_full_name_full_name").Value : "Not Available"
                                                  });
            var fname = name.First().FName.Split(',')[1];
            return fname;
        }

        private int GetHospitalIDWithContact(string hospitalName, string street, string city, string state, string zip)
        {
            
                this.hospitalName = hospitalName;
                var hospital = context.Hospitals.FirstOrDefault(h => h.HospitalName.Replace(" ", "").ToLower().Equals(hospitalName.Replace(" ", "").ToLower()));
                if (hospital == null)
                {
                    Hospital h = new Hospital { HospitalName = hospitalName };
                    HospitalContactInfo hcinfo = new HospitalContactInfo { Street = street, State = state, ZipCode = zip, City = city};
                    if (h.HospitalContactInfoes == null)
                        h.HospitalContactInfoes = new List<HospitalContactInfo>();
                    h.HospitalContactInfoes.Add(hcinfo);
                    context.Hospitals.Add(h);
                    context.SaveChanges();
                    return h.HospitalID;
                }
                else
                {
                    return hospital.HospitalID;
                }
            
        }

        private void AddBoardSpecialities(string ProviderNPINumber, ICollection<SpecialtyDetail> collection)
        {
             // Read the CSV file
            StreamReader reader = new StreamReader(@"D:\AHCDBMigration\CSV\SpecialityBoard.csv");
            while (!reader.EndOfStream)
            {
                // Create the Specialty Details and add to collection

                // 0-Npi, 1-File, 2-Specialty, 3-Board Name, 4-Certificate Number, 5-Init Cert Date, 6-Exp Date
                string spaData = reader.ReadLine();
                string[] data = spaData.Split(',');
                if (data[0] == ProviderNPINumber)
                {
                    SpecialtyDetail sd = new SpecialtyDetail();
                    sd.IsBoardCertified = "Not Available";
                    sd.SpecialtyPreference = "Not Available";
                    string spe = data[2];
                    Specialty sp = context.Specialities.FirstOrDefault(s => s.Name == spe);
                    if(sp == null)
                    {
                        sp = new Specialty() {Name = data[2] };
                    }
                    sd.Specialty = sp;
                    SpecialtyBoardCertifiedDetail sbcd = new SpecialtyBoardCertifiedDetail();
                    sbcd.CertificateNumber = data[4];
                    sbcd.InitialCertificationDate = DateTime.Parse(data[5]);
                    sbcd.ExpirationDate = DateTime.Parse(data[6]);
                    sbcd.BoardCertificatePath = GetFilePath(data[0], data[1] + ".pdf", "Board Specialty", DateTime.Parse(data[6]));
                    string bn = data[3];
                    SpecialtyBoard sb = context.SpecialtyBoards.FirstOrDefault(ss => ss.Name == bn);
                    if(sb == null)
                    {
                        sb = new SpecialtyBoard { Name = data[3] };
                    }
                    sbcd.SpecialtyBoard = sb;
                    sd.SpecialtyBoardCertifiedDetail = sbcd;
                    collection.Add(sd);
                }
            }
        }

        //private int GetOrganizationID()
        //{
        //    if(profile)
        //    Organization organization = new Organization();
        //}

        private void AddResidencyInternshipDetails(string ProviderNPINumber, ICollection<TrainingDetail> collection)
        {
            // Read the CSV file
            StreamReader reader = new StreamReader(@"D:\AHCDBMigration\CSV\ResidencyInternship.csv");
            while (!reader.EndOfStream)
            {
                // Create the ResidencyInternship and add to collection
                string resData = reader.ReadLine();
                string[] data = resData.Split(',');
                if (data[0] == ProviderNPINumber)
                {
                    //0-NPI, 1-file, 2-program type, 3-preference type, 4-speciality, 5-start date, 6-end date, 7-completed here, 8-hospital, 9-school name, 10-building, 11-city, 12-state, 13-country, 14-zip, 
                    if (string.IsNullOrEmpty(data[3])) data[3] = "Not Available";
                    if (string.IsNullOrEmpty(data[9])) data[9] = "Not Available";
                    var rid = new AHC.CD.Entities.MasterProfile.EducationHistory.ResidencyInternshipDetail
                                                  {
                                                      ProgramType = data[2],
                                                      SpecialtyID = GetSpecialityID(data[4]),
                                                      StartDate = (data[5].Length > 0 && !string.IsNullOrEmpty(data[6])) ? DateTime.Parse(data[6]) : DateTime.Parse("01/01/1900"),
                                                      StartYear = GetYear(data[5].Length > 0 ? DateTime.Parse(data[5]) : DateTime.Parse("01/01/1900")),
                                                      StartMonth = GetMonth(data[5].Length > 0 ? DateTime.Parse(data[5]) : DateTime.Parse("01/01/1900")),
                                                      EndDate = (data[6].Length > 0 && !string.IsNullOrEmpty(data[6])) ? DateTime.Parse(data[6]) : DateTime.Parse("01/01/1900"),
                                                      EndYear = GetYear(data[6].Length > 0 ? DateTime.Parse(data[6]) : DateTime.Parse("01/01/1900")),
                                                      EndMonth = GetMonth(data[6].Length > 0 ? DateTime.Parse(data[6]) : DateTime.Parse("01/01/1900")),
                                                      // Mandatory Fields
                                                      Preference = data[3],
                                                      //DirectorName = "Not Available",
                                                      DocumentPath = GetFilePath(ProviderNPINumber, data[1] + ".pdf", "Residency Internship", (data[6].Length > 0 && !string.IsNullOrEmpty(data[6])) ? DateTime.Parse(data[6]) : DateTime.Parse("01/01/1900"))
                                                  };

                    var school = new AHC.CD.Entities.MasterProfile.EducationHistory.SchoolInformation
                                   {
                                       SchoolName = data[9],
                                       County = data[13],
                                       ZipCode = data[14],
                                       // Mandatory Fields
                                       Building = data[10],
                                       Street = "Not Available",
                                       City = data[11],
                                       State = data[12],
                                       Country = data[13],
                                       PhoneNumber = "Not Available",
                                   };
                    TrainingDetail td = new TrainingDetail { HospitalName = data[8], IsCompleted=data[7] };
                    if (td.ResidencyInternshipDetails == null)
                        td.ResidencyInternshipDetails = new List<ResidencyInternshipDetail>();
                    td.ResidencyInternshipDetails.Add(rid);
                    td.SchoolInformation = school;
                    collection.Add(td);
                }
            }
        }

        private void AddCMECertificationExtraDetails(ICollection<CMECertification> collection, string npi)
        {
            // Read the CSV file
            StreamReader reader = new StreamReader(@"D:\AHCDBMigration\CSV\CMECertification.csv");
            while(!reader.EndOfStream)
            {
                // Create the CMECertification and add to collection
                string cmeData = reader.ReadLine();
                string[] data = cmeData.Split(',');
                if (data[0] == npi)
                {
                    //NPINumber, FileName, Degree Awarded, Training CME Name, Sponcer, Credit Hrs, Start Date, End Date, Expiration Date
                    CMECertification cme = new CMECertification();
                    cme.QualificationDegree = (data[2].Length>0)?data[2]:"Not Available";
                    cme.Certification = (data[3].Length > 0) ? data[3] : "Not Available";
                    cme.SponsorName = (data[4].Length > 0) ? data[4] : "Not Available";
                    cme.CreditHours = (data[5].Length > 0 && !string.IsNullOrEmpty(data[5])) ? double.Parse(data[5]) : 0;
                    cme.StartDate = (data[6].Length > 0 && !string.IsNullOrEmpty(data[6])) ? DateTime.Parse(data[6]) : DateTime.Parse("01/01/1900");
                    cme.EndDate = (data[7].Length > 0) && !string.IsNullOrEmpty(data[7]) ? DateTime.Parse(data[7]) : DateTime.Parse("01/01/1900");
                    cme.ExpiryDate = (data[8].Length > 0) && !string.IsNullOrEmpty(data[8]) ? DateTime.Parse(data[8]) : DateTime.Parse("01/01/1900");
                    cme.CertificatePath = GetFilePath(npi, data[1] + ".pdf", "CME Certification", (data[8].Length > 0) ? DateTime.Parse(data[8]) : DateTime.Parse("01/01/1900"));
                    collection.Add(cme);
                }
            }
            reader.Close(); 
        }

        private string GetCMEFileName()
        {
            throw new NotImplementedException();
        }

        private string GetGraduationFileName(string ProviderNPINumber, string schoolName, string degreeName)
        {
            string fileName = null;
            string[] graduationMapDetails = 
            {
                "1922277227,Grad1.pdf,UNIVERSITY OF BOMBAY,Bachelor of Medicine and Bachelor og Surgery",
                "1922277227,Grad2.pdf,University of South Florida,Master of Public Health",
                "1578694832,Grad1.pdf,University of South Florida,Master of Science",
                "1578694832,Grad2.pdf,University of South Florida,Post-Master's Nurse Practitioner: Family Health Nursing"
            };

            foreach (var line in graduationMapDetails)
            {
                string[] data = line.Split(',');
                if(data[0]==ProviderNPINumber && data[2].ToLower().Contains(schoolName.ToLower()) && data[3].ToLower().Contains(degreeName))
                {
                    fileName = data[1];
                    break;
                }
            }
            return fileName;
        }

        private int GetBillingContactPerson(string street, string city, string state, string country, string phone, string fax, string zip, string email,  string poBox)//string county)//, string email, string poBox)
        {
            //return 1;
            Employee e = new Employee();
            e.Street = street;
            e.City = city;
            e.State = GetState(state);
            e.Country = GetCountry(country);
            e.Telephone = FormatPhone(phone);
            e.Fax = fax;
            e.ZipCode = zip;
            //e.County = county;
            e.EmailAddress = email;
            e.POBoxAddress = poBox;
            
            context.Employees.Add(e);
            context.SaveChanges();
            return e.EmployeeID;
        }

        private ProviderPracticeOfficeHour GetOfficeHour(string monFrom, string tueFrom, string wedFrom, string thurFrom, string friFrom, string satFrom, string sunFrom, string monTo, string tueTo, string wedTo, string ThurTo, string friTo, string satTo, string sunTo)
        {
            PracticeDay mon = new PracticeDay 
            {
                DayName = "Monday",
                DayOff = "NO"
            };
            PracticeDailyHour Mhour = new PracticeDailyHour 
            {
                StartTime = monFrom,
                EndTime = monTo
            };
            mon.DailyHours = new List<PracticeDailyHour>();
            mon.DailyHours.Add(Mhour);


            PracticeDay tue = new PracticeDay
            {
                DayName = "Tuesday",
                DayOff = "NO"
            };
            PracticeDailyHour Thour = new PracticeDailyHour
            {
                StartTime = tueFrom,
                EndTime = tueTo
            };
            tue.DailyHours = new List<PracticeDailyHour>();
            tue.DailyHours.Add(Thour);

            PracticeDay wed = new PracticeDay
            {
                DayName = "Wednesday",
                DayOff = "NO"
            };
            PracticeDailyHour whour = new PracticeDailyHour
            {
                StartTime = wedFrom,
                EndTime = wedTo
            };
            wed.DailyHours = new List<PracticeDailyHour>();
            wed.DailyHours.Add(whour);

            PracticeDay thur = new PracticeDay
            {
                DayName = "Thursday",
                DayOff = "NO"
            };
            PracticeDailyHour tthour = new PracticeDailyHour
            {
                StartTime = thurFrom,
                EndTime = ThurTo
            };
            thur.DailyHours = new List<PracticeDailyHour>();
            thur.DailyHours.Add(tthour);

            PracticeDay fri = new PracticeDay
            {
                DayName = "Friday",
                DayOff = "NO"
            };
            PracticeDailyHour fhour = new PracticeDailyHour
            {
                StartTime = friFrom,
                EndTime = friTo
            };
            fri.DailyHours = new List<PracticeDailyHour>();
            fri.DailyHours.Add(fhour);

            PracticeDay sat = new PracticeDay
            {
                DayName = "Saturday",
                DayOff = "YES"
            };
            PracticeDailyHour shour = new PracticeDailyHour
            {
                StartTime = satFrom,
                EndTime = satTo
            };
            sat.DailyHours = new List<PracticeDailyHour>();
            sat.DailyHours.Add(shour);

            PracticeDay sun = new PracticeDay
            {
                DayName = "Sunday",
                DayOff = "YES"
            };
            PracticeDailyHour sshour = new PracticeDailyHour
            {
                StartTime = sunFrom,
                EndTime = sunTo
            };
            sun.DailyHours = new List<PracticeDailyHour>();
            sun.DailyHours.Add(sshour);

            ProviderPracticeOfficeHour ppoh = new ProviderPracticeOfficeHour() {AnyTimePhoneCoverage = "Not Available" };
            ppoh.PracticeDays = new List<PracticeDay>();
            ppoh.PracticeDays.Add(mon);
            ppoh.PracticeDays.Add(tue);
            ppoh.PracticeDays.Add(wed);
            ppoh.PracticeDays.Add(thur);
            ppoh.PracticeDays.Add(fri);
            ppoh.PracticeDays.Add(sat);
            ppoh.PracticeDays.Add(sun);

            return ppoh;
        }

        private Facility f;
        private int GetFacility(string practiceType, string practiceName, string city, string state, string country, string phone, string fax, string zip, string county)
        {
            Facility fac = context.Facilities.FirstOrDefault(f => f.Name == practiceName);
            if (fac == null)
            {
                fac = new Facility
               {
                   Name = practiceName,
                   City = city,
                   State = state,
                   Country = country,
                   Telephone = phone,
                   Fax = fax,
                   ZipCode = zip,
                   County = county,
                   FacilityDetail = GetFacilityDetail(practiceType),
               };

                context.Facilities.Add(fac);
                context.SaveChanges();
                f = fac;
            }
            return fac.FacilityID;
        }

        private FacilityDetail GetFacilityDetail(string practiceType)
        {
            FacilityDetail fd = new FacilityDetail();
            fd.Service = new FacilityService();
            fd.Service.PracticeTypeID = GetFacilityID(practiceType);
            return fd;
        }

        private int? GetFacilityID(string practiceType)
        {
            //try
            {
                FacilityPracticeType fpt = context.FacilityPracticeTypes.FirstOrDefault(pt => pt.Title == practiceType);
                if (fpt == null)
                {
                    fpt = new FacilityPracticeType { Title = practiceType, StatusType = StatusType.Active };
                    context.FacilityPracticeTypes.Add(fpt);
                    try
                    {
                        context.SaveChanges();
                        return fpt.FacilityPracticeTypeID;
                    }
                    catch { return null; }
                }
                return fpt.FacilityPracticeTypeID;
                
            }
            //catch { return null; }
        }

        string number;
        //Get License and Certificate Numbers
        private string GetNumber(string p)
        {
            number = p;
            return number;
        }

        private string SetProviderID(int profileID)
        {
            string providerID;
            if (profileID <= 9)
                providerID = "PR000" + profileID;
            else if (profileID <= 99)
                providerID = "PR00" + profileID;
            else if (profileID <= 999)
                providerID = "PR0" + profileID;
            else
                providerID = "PR" + profileID;

            return providerID;
        }

        private int GetMonth(DateTime dateTime)
        {
            return dateTime.Month;
        }

        private int GetYear(DateTime dateTime)
        {
            return dateTime.Year;
        }

        private string FormatPhone(string phone)
        {
            string result="";
            if (string.IsNullOrEmpty(phone)) return phone;
            if (phone == null) return null;
            if (phone.Contains("Not Available")) return null;

            
            foreach (char chr in phone)
            {
                if (Char.IsDigit(chr))
                    result += chr;
            }
            return result;
        }

        private string GetCountry(string countryId)
        {
            if (countryId == "151")
                return "United States";
            return countryId;
        }

        private string GetState(string stateId)
        {
            if (stateId == "43")
                return "Florida";

            if (stateId == "FL")
                return "Florida";

            return stateId;
        }

        private string GetQualificationType(string graduationType)
        {
            if (graduationType.Length <= 0)
                return "Not Available";

            if (graduationType.Equals("Medical Training"))
                return "Graduate";

            if (graduationType.Equals("Under Graduate"))
                return "UnderGraduate";

            if (graduationType.Equals("Post Graduate"))
                return "PostGraduate";

            return graduationType;
        }

        #region Master Data Helpers

        private int? GetStateLicenseStatusID()
        {
            StateLicenseStatus licenseStatus = context.StateLicenseStatuses.FirstOrDefault(sl => sl.Title == "Not Available");
            if (licenseStatus == null)
            {
                licenseStatus = new StateLicenseStatus { Title = "Not Available" };

                context.StateLicenseStatuses.Add(licenseStatus);
                context.SaveChanges();
            }
            return licenseStatus.StateLicenseStatusID;
        }

        private int? GetAdmittingPrivilegeID(string privilageStatus)
        {
            //AdmittingPrivilege ap = context.AdmittingPrivileges.FirstOrDefault(a => a.Title == "Not Available");
            //if (ap == null)
            //{
            //    ap = new AdmittingPrivilege();
            //    if (privilageStatus == "YES")
            //        ap.Status = "Active";
            //    else if (privilageStatus == "NO")
            //        ap.Status = "Inactive";
            //    else
            //        ap.Status = privilageStatus;

            //    ap.Title = "Not Available";
            //    context.AdmittingPrivileges.Add(ap);
            //    //listBox1.Items.Add("\t\t Admiting Privilate Status : " + ap.Status);
            //    context.SaveChanges();
            //}

            AdmittingPrivilege ap = context.AdmittingPrivileges.FirstOrDefault(a => a.Title.Replace(" ", "").ToLower().Equals(privilageStatus.Replace(" ", "").ToLower()));
            if (ap == null)
                return null;
           
                return ap.AdmittingPrivilegeID;
        }
        
        private int? GetProviderTypeID(string data)
        {
            ProviderType providerType = null;
            if (string.IsNullOrWhiteSpace(data))
                data = "Not Available";
            providerType = context.ProviderTypes.FirstOrDefault(pt => pt.Title.Replace(" ", "").ToLower() == data.Replace(" ", "").ToLower());
            if (providerType == null)
            {
                providerType = new ProviderType { Title = data};
                context.ProviderTypes.Add(providerType);
                //try
                {
                    context.SaveChanges();
                    return providerType.ProviderTypeID;
                }
                //catch { return null; }
            }
            return providerType.ProviderTypeID;
        }


        #region Save Documents - Documents Migration

        
        /// <summary>
        /// Profile documents are saved with [Documents/{NPINumber}/{All Files belongs to this provider}]
        /// </summary>
        /// <param name="ProviderNPINumber"></param>
        /// <param name="documentName"></param>
        /// <param name="title"></param>
        /// <param name="expiryDate"></param>
        /// <returns></returns>
        
        private string GetFilePath(string ProviderNPINumber, string documentName, string title, DateTime? expiryDate = null)
        {
            if (profile.ProfileDocuments == null)
                profile.ProfileDocuments = new List<ProfileDocument>();

            string fromFilePath = @"D:\AHCDBMigration\Providers\" + ProviderNPINumber + @"\" + documentName;
            if (File.Exists(fromFilePath))
            {
                // Create Document Folder
                string migrationProfileDocPath = @"D:\AHCDBMigration\Documents\MigrationDocs\" + ProviderNPINumber;
                if(!Directory.Exists(migrationProfileDocPath))
                {
                    Directory.CreateDirectory(migrationProfileDocPath);
                }
                // Copy file to application folder
                string uniqueKey = AHC.UtilityService.UniqueKeyGenerator.GetUniqueKey();
                string dbDocPath = @"\Documents\" + ProviderNPINumber +"\\"+ uniqueKey + "-" + documentName;
                File.Copy(fromFilePath, migrationProfileDocPath + "\\" + uniqueKey + "-" + documentName,true);
                //Store in Profile Documents
                ProfileDocument document = new ProfileDocument
                {
                    Title = title,
                    DocPath = dbDocPath,
                    ExpiryDate = expiryDate
                };
                profile.ProfileDocuments.Add(document);
                return dbDocPath;
            }
            else
            {
                return null;
            }
        }

        #endregion

        bool isDeaScheduleAdded = false;

        private ICollection<DEAScheduleInfo> GetDeaSchedules(string p1, string p2, string p3, string p4, string p5, string p6, string p7, string p8, string p9, string p10)
        {

            List<DEAScheduleInfo> scheduleInfos = new List<DEAScheduleInfo>();

            // Master Data
            DEASchedule schedule1N = context.DEASchedules.Find(1);
            DEASchedule schedule1NN = context.DEASchedules.Find(2); 
            DEASchedule schedule2N = context.DEASchedules.Find(3);
            DEASchedule schedule2NN = context.DEASchedules.Find(4);
            DEASchedule schedule3N = context.DEASchedules.Find(5);
            DEASchedule schedule3NN = context.DEASchedules.Find(6);
            DEASchedule schedule4NN = context.DEASchedules.Find(7);
            DEASchedule schedule5NN = context.DEASchedules.Find(8);

            if (!isDeaScheduleAdded && schedule1N == null && schedule5NN == null)
            {
                schedule1N = new DEASchedule { ScheduleTitle = "Schedule I", ScheduleTypeTitle = "Narcotic", StatusType = StatusType.Inactive };
                schedule1NN = new DEASchedule { ScheduleTitle = "Schedule I", ScheduleTypeTitle = "Non-Narcotic", StatusType = StatusType.Active };
                schedule2N = new DEASchedule { ScheduleTitle = "Schedule II", ScheduleTypeTitle = "Narcotic", StatusType = StatusType.Active };
                schedule2NN = new DEASchedule { ScheduleTitle = "Schedule II", ScheduleTypeTitle = "Non-Narcotic", StatusType = StatusType.Active };
                schedule3N = new DEASchedule { ScheduleTitle = "Schedule III", ScheduleTypeTitle = "Narcotic", StatusType = StatusType.Active };
                schedule3NN = new DEASchedule { ScheduleTitle = "Schedule III", ScheduleTypeTitle = "Non-Narcotic", StatusType = StatusType.Active };
                schedule4NN = new DEASchedule { ScheduleTitle = "Schedule IV", ScheduleTypeTitle = "Non-Narcotic", StatusType = StatusType.Active };
                schedule5NN = new DEASchedule { ScheduleTitle = "Schedule V", ScheduleTypeTitle = "Non-Narcotic", StatusType = StatusType.Active };

                context.DEASchedules.Add(schedule1N);
                context.DEASchedules.Add(schedule1NN);
                context.DEASchedules.Add(schedule2N);
                context.DEASchedules.Add(schedule2NN);
                context.DEASchedules.Add(schedule3N);
                context.DEASchedules.Add(schedule3NN);
                context.DEASchedules.Add(schedule4NN);
                context.DEASchedules.Add(schedule5NN);
                context.SaveChanges();
                isDeaScheduleAdded = true;
            }
            DEAScheduleInfo dea1 = null;

            if (p1.Length > 0)
            {
                dea1 = new DEAScheduleInfo
                {
                    IsEligible = p1,
                    DEAScheduleID = schedule1N.DEAScheduleID,
                    DEASchedule = schedule1N,
                };
            }
            scheduleInfos.Add(dea1);

            DEAScheduleInfo dea2 = null;
            if (p2.Length > 0)
            {
                dea2 = new DEAScheduleInfo
                {
                    IsEligible = p2,
                    DEAScheduleID = schedule1NN.DEAScheduleID,
                    DEASchedule = schedule1NN,
                };
            }
            scheduleInfos.Add(dea2);

            DEAScheduleInfo dea3 = null;
            if (p3.Length > 0)
            {
                dea3 = new DEAScheduleInfo
                {
                    IsEligible = p3,
                    DEAScheduleID = schedule2NN.DEAScheduleID,
                    DEASchedule = schedule2NN,
                };
            }
            scheduleInfos.Add(dea3);


            DEAScheduleInfo dea4 = null;
            if (p4.Length > 0)
            {
                dea4 = new DEAScheduleInfo
                {
                    IsEligible = p4,
                    DEAScheduleID = schedule2N.DEAScheduleID,
                    DEASchedule = schedule2N
                };
            }
            scheduleInfos.Add(dea4);

            DEAScheduleInfo dea5 = null;
            if (p5.Length > 0)
            {
                dea5 = new DEAScheduleInfo
                {
                    IsEligible = p5,
                    DEAScheduleID = schedule3N.DEAScheduleID,
                    DEASchedule = schedule3N
                };
            }
            scheduleInfos.Add(dea5);

            DEAScheduleInfo dea6 = null;
            if (p6.Length > 0)
            {
                dea6 = new DEAScheduleInfo
                {
                    IsEligible = p6,
                    DEAScheduleID = schedule3NN.DEAScheduleID,
                    DEASchedule = schedule3NN
                };
            }
            scheduleInfos.Add(dea6);

            DEAScheduleInfo dea7 = null;
            if (p7.Length > 0)
            {
                dea7 = new DEAScheduleInfo
                {
                    IsEligible = p7,
                    DEAScheduleID = schedule4NN.DEAScheduleID,
                    DEASchedule = schedule4NN
                };
            }
            scheduleInfos.Add(dea7);

            DEAScheduleInfo dea8 = null;
            if (p8.Length > 0)
            {
                dea8 = new DEAScheduleInfo
                {
                    IsEligible = p8,
                    DEAScheduleID = schedule5NN.DEAScheduleID,
                    DEASchedule = schedule5NN
                };
            }
            scheduleInfos.Add(dea8);

            return scheduleInfos;

        }

        private int GetQuestionID(string questionTitle)
        {

            Question question = context.Questions.FirstOrDefault(q => q.Title.Replace(" ", "").ToLower().Equals(questionTitle.Replace(" ", "").ToLower()));
            if (question == null)
            {
                Question q = new Question { Title = questionTitle, StatusType = StatusType.Active };
                context.Questions.Add(q);
                context.SaveChanges();
                return q.QuestionID;
            }

            return question.QuestionID;
        }

        private StateLicenseStatus GetStateLicenseStatus(string stateLicenseStatus)
        {
            if (stateLicenseStatus.Length > 0)
            {
                if (stateLicenseStatus == "1")
                {
                    var stateLicense = context.StateLicenseStatuses.FirstOrDefault(sls => sls.Title.ToUpper().Trim().Contains("ACTIVE"));
                    if (stateLicense == null)
                    {
                        return new StateLicenseStatus{Title = "Active" , StatusType=StatusType.Active};
                    }
                    return stateLicense;
                }
                else if (stateLicenseStatus == "0")
                {
                    var stateLicense = context.StateLicenseStatuses.FirstOrDefault(sls => sls.Title.ToUpper().Trim().Contains("INACTIVE"));
                    if (stateLicense == null)
                    {
                        return new StateLicenseStatus { Title = "Inactive", StatusType = StatusType.Active };
                    }
                    return stateLicense;
                }
            }
            //return new StateLicenseStatus { Status = "Not Available", Title = "Not Available" };
            return null;
        }

        private string GetNameFromFile(string fileName)
        {
            int len = fileName.Length;
            return fileName.Substring(0, len - 4);
        }

        private int? GetInsuranceCarrierID(string insuranceCarrierName,out int? insurranceCarrierAddressID)
        {
            var insuranceCarrier = context.InsuranceCarriers.Include("InsuranceCarrierAddresses").FirstOrDefault(ic => ic.Name.Replace(" ", "").ToLower().Equals(insuranceCarrierName.Replace(" ", "").ToLower()));

            if (insuranceCarrier != null && insuranceCarrier.InsuranceCarrierAddresses != null && insuranceCarrier.InsuranceCarrierAddresses.Count >0) // Master record found
            {
                    insurranceCarrierAddressID = insuranceCarrier.InsuranceCarrierAddresses.ElementAt(0).InsuranceCarrierAddressID;
                    return insuranceCarrier.InsuranceCarrierID;
                
            }
            else // Create master data
            {
                AHC.CD.Entities.MasterData.Tables.InsuranceCarrier ic = new AHC.CD.Entities.MasterData.Tables.InsuranceCarrier { Name = insuranceCarrierName };
                InsuranceCarrierAddress insuranceCarrierAddress = new InsuranceCarrierAddress();
                if (ic.InsuranceCarrierAddresses == null)
                {
                    ic.InsuranceCarrierAddresses = new List<InsuranceCarrierAddress>();
                    //insuranceCarrierAddress.UnitNumber = "Not Available";
                    //insuranceCarrierAddress.Number = "Not Available";
                    insuranceCarrierAddress.LocationName = "Not Available";
                    //insuranceCarrierAddress.Street = "Not Available";
                    //insuranceCarrierAddress.State = "Not Available";
                    //insuranceCarrierAddress.City = "Not Available";
                    //insuranceCarrierAddress.County = "Not Available";
                    //insuranceCarrierAddress.Country = "Not Available";
                    //insuranceCarrierAddress.ZipCode = "Not Available";
                    //insuranceCarrierAddress.Building = "Not Available";
                }
                ic.InsuranceCarrierAddresses.Add(insuranceCarrierAddress);
                context.InsuranceCarriers.Add(ic);
                  context.SaveChanges();

                    insurranceCarrierAddressID = insuranceCarrierAddress.InsuranceCarrierAddressID;
                    return ic.InsuranceCarrierID;
                
                
            }
        }

        private int? GetSpecialityID(string specialityName)
        {
            var speciality = context.Specialities.FirstOrDefault(sp => sp.Name.Replace(" ", "").ToLower().Equals(specialityName.Replace(" ", "").ToLower()));
            if(speciality == null) //Speciality not found in master db, create a new speciality
            {
                Specialty s = new Specialty {Name = specialityName };
                context.Set<Specialty>().Add(s);
                //context.Specialities.Add(s);
                try
                {
                    
                    context.SaveChanges();
                    return s.SpecialtyID;
                }
                catch { return null; }
                
            }
            return speciality.SpecialtyID;

        }

        private int? GetStaffCateogryID(string staffTitle)
        {
            //try
            {
                var staffCategory = context.StaffCategories.FirstOrDefault(sc => sc.Title.Replace(" ", "").ToLower().Equals(staffTitle.Replace(" ", "").ToLower()));
                if (staffCategory == null)
                {
                    StaffCategory sc = new StaffCategory { Title = staffTitle, StatusType = StatusType.Active };
                    context.StaffCategories.Add(sc);

                    context.SaveChanges();
                    return sc.StaffCategoryID;
                }
                else
                {
                    return staffCategory.StaffCategoryID;
                }
            }
            //catch { return null; }
        }

        string hospitalName;
        private int? GetHospitalID(string hospitalName)
        {
            //try
            {
                this.hospitalName = hospitalName;
                var hospital = context.Hospitals.FirstOrDefault(h => h.HospitalName.Replace(" ", "").ToLower().Equals(hospitalName.Replace(" ", "").ToLower()));
                if (hospital == null)
                {
                    Hospital h = new Hospital { HospitalName = hospitalName };
                    context.Hospitals.Add(h);
                    try
                    {
                        context.SaveChanges();
                        return h.HospitalID;
                    }
                    catch { return null; }

                }
                else
                {
                    return hospital.HospitalID;
                }
            }
            //catch { return null; }
        }

        private int? GetSpecialityBoardID(string boardName)
        {
            if (boardName.Length == 0)
                boardName = "Not Available";
            var specialityBoard = context.SpecialtyBoards.FirstOrDefault(sb => sb.Name.Replace(" ", "").ToLower().Equals(boardName.Replace(" ", "").ToLower()));
            if (specialityBoard == null)
            {
                
                SpecialtyBoard sb = new SpecialtyBoard { Name = boardName };
                //context.Set<SpecialtyBoard>().Add(sb);
                context.SpecialtyBoards.Add(sb);
                //try
                {
                    context.SaveChanges();
                    return sb.SpecialtyBoardID;
                }
                //catch { return null; }
            }
            else
                return specialityBoard.SpecialtyBoardID;
        }

        #endregion

    }
}
