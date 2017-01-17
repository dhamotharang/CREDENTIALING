using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using AHC.CD.Entities.MasterProfile;
using AHC.CD.Entities.MasterProfile.BoardSpecialty;
using AHC.CD.Entities.MasterProfile.Demographics;
using AHC.CD.Entities.MasterProfile.DisclosureQuestions;
using AHC.CD.Entities.MasterProfile.EducationHistory;
using AHC.CD.Entities.MasterProfile.IdentificationAndLicenses;
using AHC.CD.Entities.MasterProfile.ProfessionalReference;
using AHC.UtilityService;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

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
            xmlFiles = Directory.GetFiles(@"D:\AHCDBMigration\XML\SampleTest", "*.xml");
            progressBar1.Minimum= 1;
            progressBar1.Maximum = xmlFiles.Count();
            lblProfilesCount.Text = xmlFiles.Count().ToString();
            
            //Task t = Task.Factory.StartNew(() => { DoMigration(); });
            //t.Wait();
            DoMigration();
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
                                           FirstName = GetNameFromFile(fileName),
                                           LastName = "Not Available",

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

                #region Demographics - Birth Information

                // Birth Information
                var birthInformation = (from x in xmlDoc.Descendants("d_prf_personal_row")
                                        select
                                        new AHC.CD.Entities.MasterProfile.Demographics.BirthInformation
                                        {
                                            //DateOfBirthStored = x.Element("pd_basic_date_of_birth").Value.Length > 0 ? DateTime.Parse(x.Element("pd_basic_date_of_birth").Value) : DateTime.Parse("01/10/1900"),
                                            DateOfBirth = x.Element("pd_basic_date_of_birth").Value.Length > 0 ? DateTime.Parse(x.Element("pd_basic_date_of_birth").Value) : DateTime.Parse("01/10/1900"),
                                            CityOfBirth = x.Element("pd_basic_pob_city").Value.Length > 0 ? x.Element("pd_basic_pob_city").Value : "Not Available",
                                            CountryOfBirth = x.Element("cpob_country").Value.Length > 0 ? x.Element("cpob_country").Value : "Not Available",
                                            StateOfBirth = x.Element("cpob_state").Value.Length > 0 ? x.Element("cpob_state").Value : "Not Available",
                                            BirthCertificatePath = GetFilePath(ProviderNPINumber,"Birth.pdf", "Birth Certificate"),
                                            // Mandatory Fields
                                            CountyOfBirth = "Not Available",

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

                #region Demographics - Personal Identification
                // Personal Identification
                
                var personalIdentification = (from x in xmlDoc.Descendants("d_prf_personal_row")
                                              select
                                              new AHC.CD.Entities.MasterProfile.Demographics.PersonalIdentification
                                              {
                                                  SSN = x.Element("pd_basic_ssn").Value.Length > 0 ? x.Element("pd_basic_ssn").Value : "Not Available",
                                                  SSNCertificatePath = GetFilePath(ProviderNPINumber,"SSN.pdf","SSN Certificate"),
                                                  DL = "Not Available",
                                                  DLCertificatePath = GetFilePath(ProviderNPINumber, "DL.pdf","Driving License"),

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
                                                     PhoneNumber = FormatPhone(x.Element("cphone").Value.Length > 0 ? x.Element("cphone").Value : "Not Available - " + UniqueKeyGenerator.GetUniqueKey()), 
                                                     
                                                     // Mandatory Fields
                                                     CountryCode = "Not Available", 
                                                     PhoneTypeEnum = PhoneTypeEnum.Mobile, 
                                                     PreferenceType = PreferenceType.Secondary,
                                                     StatusType = StatusType.Inactive
                                                 } 
                                             }

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

                #region Professional Liability - Insurance
                // Professional Liability
                int addressid;
                var professionalLiabilities = (from x1 in xmlDoc.Descendants("d_prf_insurance")
                                               from x in x1.Descendants("d_prf_insurance_row")
                                               select
                                               new AHC.CD.Entities.MasterProfile.ProfessionalLiability.ProfessionalLiabilityInfo
                                               {
                                                   InsuranceCarrierID = GetInsuranceCarrierID(x.Element("address_lookup_carrier_code").Value, out addressid),
                                                   InsuranceCarrierAddressID = addressid,
                                                   PolicyNumber = x.Element("pd_insurance_policy_number").Value.Length > 0 ? x.Element("pd_insurance_policy_number").Value : "Not Available",
                                                   EffectiveDate = x.Element("pd_insurance_coverage_from").Value.Length > 0 ? DateTime.Parse(x.Element("pd_insurance_coverage_from").Value) : DateTime.Parse("01/01/1900"),
                                                   PolicyIncludesTailCoverage = x.Element("pd_insurance_tail_coverage").Value.Length > 0 ? x.Element("pd_insurance_tail_coverage").Value : "Not Available",
                                                   AmountOfCoveragePerOccurance = x.Element("pd_insurance_coverage_limit_from").Value.Length > 0 ? double.Parse(x.Element("pd_insurance_coverage_limit_from").Value) : 0.0,
                                                   AmountOfCoverageAggregate = x.Element("pd_insurance_coverage_limit_to").Value.Length > 0 ? double.Parse(x.Element("pd_insurance_coverage_limit_to").Value) : 0.0,
                                                   //DenialReason = x.Element("pd_insurance_denied_explain").Value.Length > 0 ? x.Element("pd_insurance_denied_explain").Value : "Not Available",
                                                   ExpirationDate = x.Element("pd_insurance_coverage_to").Value.Length > 0 ? DateTime.Parse(x.Element("pd_insurance_coverage_to").Value) : DateTime.Parse("01/01/1900"),

                                                   InsuranceCertificatePath = GetFilePath(ProviderNPINumber, "IC1.pdf", "Insurance Certificate", x.Element("pd_insurance_coverage_to").Value.Length > 0 ? DateTime.Parse(x.Element("pd_insurance_coverage_to").Value) : DateTime.Parse("01/01/1900")),
                                                   // Mandatory Fields
                                                   PhoneNumber = "not Available",
                                                   SelfInsured = "Not Available",
                                                   OriginalEffectiveDate = DateTime.Parse("01/01/1900"),
                                                   CoverageType = "Not Available",
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

                #region DEA CSR 
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
                                  DEALicenceCertPath = GetFilePath(ProviderNPINumber,"DEA1.pdf","DEA License",x.Element("expiration_date").Value.Length > 0 ? DateTime.Parse(x.Element("expiration_date").Value) : DateTime.Parse("01/01/1900") ),
                                  
                                  // Mandatory Fields
                                  IssueDate = DateTime.Parse("01/01/1900"),
                                  IsInGoodStanding = "Not Available",


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

                #region ECFMG Details

                // ECFMG
                listBox1.Items.Add("ECFMG Details");
                var ecfmg = (from x in xmlDoc.Descendants("d_prf_ecfmg_row")
                             select
                             new AHC.CD.Entities.MasterProfile.EducationHistory.ECFMGDetail
                             {
                                 ECFMGNumber = x.Element("ecfmg_number").Value.Length > 0 ? x.Element("ecfmg_number").Value : "Not Available",
                                 ECFMGIssueDate = x.Element("date_issued").Value.Length > 0 ? DateTime.Parse(x.Element("date_issued").Value) : DateTime.Parse("01/01/1900"),
                                 ECFMGCertPath = GetFilePath(ProviderNPINumber,"ECFMG1.pdf","ECFMG Certificate"),
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

                #region Education Details
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
                                           Building = "Not Available", 
                                           Street = "Not Available", 
                                           City = "Not Available",
                                           State = "Not Available", 
                                           Country = "Not Available",
                                           PhoneNumber = "Not Available",
                                           ZipCode = "Not Available",
                                           
                                       },
                                       StartDate = x.Element("start_date").Value.Length > 0 ? DateTime.Parse(x.Element("start_date").Value) : DateTime.Parse("01/01/1900"),
                                       EndDate = x.Element("end_date").Value.Length > 0 ? DateTime.Parse(x.Element("end_date").Value) : DateTime.Parse("01/01/1900"),
                                       QualificationType = GetQualificationType(x.Element("code_lookup_education_type").Value),
                                       //.Length > 0 ? x.Element("code_lookup_education_type").Value : "Not Available",
                                       // Mandatory Fields
                                       IsUSGraduate = "Not Available",
                                       //QualificationType = "Not Available",
                                       GraduationType = "Not Available",
                                       IsCompleted = "Not Available",
                                       CertificatePath = GetFilePath(ProviderNPINumber, "GC1.pdf", "Graduation Certificate"),
                                       


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

                #region CME Certifications - Specialities Certificate

                listBox1.Items.Add("Adding CME Certifications");
                var cmes = (from x1 in xmlDoc.Descendants("d_prf_special_certs_dates")
                            from x in x1.Descendants("d_prf_special_certs_dates_row")
                            select
                            new AHC.CD.Entities.MasterProfile.EducationHistory.CMECertification
                            {
                                StartDate = x.Element("pd_special_certs_start_date").Value.Length > 0 ? DateTime.Parse(x.Element("pd_special_certs_start_date").Value) : DateTime.Parse("01/01/1900"),
                                EndDate = x.Element("pd_special_certs_end_date").Value.Length > 0 ? DateTime.Parse(x.Element("pd_special_certs_end_date").Value) : DateTime.Parse("01/01/1900"),
                                Certification = x.Element("certified_in").Value.Length > 0 ? x.Element("certified_in").Value : "Not Available",
                                //CertificatePath = 
                                // Mandatory Fields
                                QualificationDegree = "Not Available",
                                CreditHours = 0,
                                SponsorName = "Not Available",
                                

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


                #endregion

                #region Education History - Training

                // Education History - Training

                listBox1.Items.Add("Adding Education History Information");
                var residencyInternshipDetails = (from x1 in xmlDoc.Descendants("d_prf_training_dates")
                                                  from x in x1.Descendants("d_prf_training_dates_row")
                                                  select
                                                  new AHC.CD.Entities.MasterProfile.EducationHistory.ResidencyInternshipDetail
                                                  {
                                                      ProgramType = x.Element("code_lookup_train_type").Value.Length > 0 ? x.Element("code_lookup_train_type").Value : "Not Available",
                                                      SpecialtyID = GetSpecialityID(x.Element("code_lookup_specialty").Value),
                                                      StartDate = x.Element("start_date").Value.Length > 0 ? DateTime.Parse(x.Element("start_date").Value) : DateTime.Parse("01/01/1900"),
                                                      EndDate = x.Element("end_date").Value.Length > 0 ? DateTime.Parse(x.Element("end_date").Value) : DateTime.Parse("01/01/1900"),

                                                      // Mandatory Fields
                                                      Preference = "Not Available",
                                                      DirectorName = "Not Available",
                                                      DocumentPath = "Not Available",
                                                      

                                                  }).ToList();


                var schoolNames = (from x1 in xmlDoc.Descendants("d_prf_training_dates")
                                   from x in x1.Descendants("d_prf_training_dates_row")
                                   select
                                   new AHC.CD.Entities.MasterProfile.EducationHistory.SchoolInformation
                                   {
                                       SchoolName = x.Element("address_lookup_institution_code").Value.Length > 0 ? x.Element("address_lookup_institution_code").Value : "Not Available",

                                       // Mandatory Fields
                                       Building = "Not Available",
                                       Street = "Not Available",
                                       City = "Not Available",
                                       State = "Not Available",
                                       Country = "Not Available",
                                       PhoneNumber = "Not Available",


                                   }).ToList();




                if (profile.TrainingDetails == null)
                    profile.TrainingDetails = new List<TrainingDetail>();



                int index = 0;

                foreach (var item in residencyInternshipDetails)
                {
                    TrainingDetail td = new TrainingDetail {HospitalName = "Not Available" };
                    td.IsCompleted = "Not Available";

                    if (td.ResidencyInternshipDetails == null)
                        td.ResidencyInternshipDetails = new List<ResidencyInternshipDetail>();


                    listBox1.Items.Add("\tAdding Training Information");
                    listBox1.Items.Add("\t\tProgram Type: " + item.ProgramType);
                    listBox1.Items.Add("\t\tSpecialty ID: " + item.SpecialtyID);
                    listBox1.Items.Add("\t\tSpecialty : " + context.Specialities.Find(item.SpecialtyID).Name);
                    listBox1.Items.Add("\t\tStart Date: " + item.StartDate);
                    listBox1.Items.Add("\t\tEnd Date: " + item.EndDate);
                    
                    listBox1.Items.Add("\t\tSchool Name: " + schoolNames[index].SchoolName);
                    listBox1.Items.Add("\t\tIs Completed: " + td.IsCompleted);
                    listBox1.Items.Add("\t\tIs Director Name: " + item.DirectorName);
                    td.ResidencyInternshipDetails.Add(item);
                    td.SchoolInformation = schoolNames[index++];
                    profile.TrainingDetails.Add(td);
                }


                #endregion

                #region Work Experience
                // Work Experience

                listBox1.Items.Add("Adding Work History");
                var workExperiences = (from x1 in xmlDoc.Descendants("d_scr_prof_experience_dates")
                                       from x in x1.Descendants("d_scr_prof_experience_dates_row")
                                       select new AHC.CD.Entities.MasterProfile.WorkHistory.ProfessionalWorkExperience
                                       {
                                           StartDate = x.Element("start_date").Value.Length > 0 ? DateTime.Parse(x.Element("start_date").Value) : DateTime.Parse("01/01/1900"),
                                           EndDate = x.Element("end_date").Value.Length > 0 ? DateTime.Parse(x.Element("end_date").Value) : DateTime.Parse("01/01/1900"),
                                           EmployerName = x.Element("organization").Value.Length > 0 ? x.Element("organization").Value : "Not Available",
                                           Street = x.Element("street_1").Value.Length > 0 ? x.Element("street_1").Value : "Not Available",
                                           City = x.Element("city").Value.Length > 0 ? x.Element("city").Value : "Not Available",
                                           ZipCode = x.Element("zip").Value.Length > 0 ? x.Element("zip").Value : "Not Available",
                                           State = GetState(x.Element("state").Value.Length > 0 ? x.Element("state").Value : "Not Available"),
                                           EmployerEmail = x.Element("e_mail_address").Value.Length > 0 ? x.Element("e_mail_address").Value : "Not Available",
                                           EmployerFax = FormatPhone(x.Element("fax").Value.Length > 0 ? x.Element("fax").Value : "Not Available"),
                                           Country = GetCountry(x.Element("country").Value.Length > 0 ? x.Element("country").Value : "Not Available"),
                                           WorkExperienceDocPath = GetFilePath(ProviderNPINumber,"WE1.pdf","Work Experience"),
                                           // Mandatory Fields
                                           Building = "Not Available",
                                           
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

                #region Hospital Affiliation
                // Hospital Privilate Details

                listBox1.Items.Add("Adding Hospital Privilage Information");
                var hospitalPrivilages = (from x1 in xmlDoc.Descendants("d_prf_hospital_affil_dates")
                                          from x in x1.Descendants("d_prf_hospital_affil_dates_row")
                                          select new AHC.CD.Entities.MasterProfile.HospitalPrivilege.HospitalPrivilegeDetail
                                          {
                                              HospitalID = GetHospitalID(x.Element("address_lookup_hospital_code").Value),
                                              StaffCategoryID = GetStaffCateogryID(x.Element("code_lookup_staff_category").Value),
                                              AffilicationStartDate = x.Element("pd_hosp_affil_start_date").Value.Length > 0 ? DateTime.Parse(x.Element("pd_hosp_affil_start_date").Value) : DateTime.Parse("01/01/1900"),
                                              AffiliationEndDate = x.Element("pd_hosp_affil_end_date").Value.Length > 0 ? DateTime.Parse(x.Element("pd_hosp_affil_end_date").Value) : DateTime.Parse("01/01/1900"),
                                              AdmittingPrivilegeID = GetAdmittingPrivilegeID(x.Element("code_lookup_admitting_priv").Value),
                                              // Mandatory Fields
                                              Preference = "Not Available",
                                              DepartmentName = "Not Available",
                                              DepartmentChief = "Not Available",
                                              FullUnrestrictedPrevilages = "Not Available",
                                              HospitalPrevilegeLetterPath = GetFilePath(ProviderNPINumber,"HPL1.pdf","Hospital Previlege Letter"),
                                          }).ToList();

                if (profile.HospitalPrivilegeInformation == null)
                    profile.HospitalPrivilegeInformation = new AHC.CD.Entities.MasterProfile.HospitalPrivilege.HospitalPrivilegeInformation {HasHospitalPrivilege = "Not Available" };

                if (profile.HospitalPrivilegeInformation.HospitalPrivilegeDetails == null)
                {
                    profile.HospitalPrivilegeInformation.HospitalPrivilegeDetails = new List<AHC.CD.Entities.MasterProfile.HospitalPrivilege.HospitalPrivilegeDetail>();
                }

                foreach (var item in hospitalPrivilages)
                {

                    profile.HospitalPrivilegeInformation.HospitalPrivilegeDetails.Add(item);
                    listBox1.Items.Add("\tAdding Hospital Privilege Details");
                    listBox1.Items.Add("\t\tHospital ID: " + item.HospitalID);
                    listBox1.Items.Add("\t\tHospital Name: " + context.Hospitals.Find(item.HospitalID).HospitalName);
                    listBox1.Items.Add("\t\tStaff Category ID: " + item.StaffCategoryID);
                    listBox1.Items.Add("\t\tStaff Category : " + context.StaffCategories.Find(item.StaffCategoryID).Title);
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

                #endregion

                #region State License
                // License

                listBox1.Items.Add("Adding State License Information");
                var stateLicenseInformations = (from x1 in xmlDoc.Descendants("d_scr_license_dates")
                                                from x in x1.Descendants("d_scr_license_dates_row")
                                                select new AHC.CD.Entities.MasterProfile.IdentificationAndLicenses.StateLicenseInformation
                                                {
                                                    LicenseNumber = x.Element("license_number").Value.Length > 0 ? x.Element("license_number").Value : "Not Available" + Guid.NewGuid() ,
                                                    ExpiryDate = x.Element("expiration_date").Value.Length > 0 ? DateTime.Parse(x.Element("expiration_date").Value) : DateTime.Parse("01/01/1900"),
                                                    IssueDate = x.Element("issue_date").Value.Length > 0 ? DateTime.Parse(x.Element("issue_date").Value) : DateTime.Parse("01/01/1900"),
                                                    
                                                    IssueState = GetState( x.Element("state").Value.Length > 0 ? x.Element("state").Value : "Not Available"),
                                                    StateLicenseStatus = GetStateLicenseStatus(x.Element("active_status").Value),
                                                    StateLicenseDocumentPath = GetFilePath(ProviderNPINumber,"SL1.pdf","State License"),
                                                    // Mandatory Fields
                                                    ProviderTypeID = GetProviderTypeID("Not Available"),
                                                    IsCurrentPracticeState = "Not Available",
                                                    LicenseInGoodStanding = "Not Available",
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
                    listBox1.Items.Add("\t\tProvider Type : " +  context.ProviderTypes.Find(item.ProviderTypeID).Description);
                }
                #endregion

                #region Specialities

                // Specialities
                listBox1.Items.Add("Adding Specialities");
                var specialityDetails = (from x1 in xmlDoc.Descendants("d_prf_specialties_dates")
                                         from x in x1.Descendants("d_prf_specialties_dates_row")

                                         select new AHC.CD.Entities.MasterProfile.BoardSpecialty.SpecialtyDetail
                                         {
                                             SpecialtyID = GetSpecialityID(x.Element("code_lookup_specialty").Value),

                                             SpecialtyBoardCertifiedDetail = new SpecialtyBoardCertifiedDetail()
                                             {
                                                 
                                                 ExpirationDate = x.Element("expiration_date").Value.Length > 0 ? DateTime.Parse(x.Element("expiration_date").Value) : DateTime.Parse("01/01/1900"),
                                                 // Mandatory Fields
                                                 InitialCertificationDate = DateTime.Parse("01/01/1900"),
                                                 LastReCerificationDate = DateTime.Parse("01/01/1900"),
                                                 BoardCertificatePath = GetFilePath(ProviderNPINumber,"Board1.pdf","Board Certificate",x.Element("expiration_date").Value.Length > 0 ? DateTime.Parse(x.Element("expiration_date").Value) : DateTime.Parse("01/01/1900")),
                                             },
                                             SpecialtyBoardID = GetSpecialityBoardID(x.Element("address_lookup_board_code").Value),
                                             // Mandatory Fields
                                             SpecialtyPreference = "Not Available",
                                             IsCurrentlyPracting = "Not Available",
                                             PercentageOfTime = 0.0,
                                             ListedInHMO = "Not Available",
                                             ListedInPPO = "Not Available",
                                             ListedInPOS = "Not Available",
                                             IsBoardCertified = "Not Available",

                                         }).ToList();

                if (profile.SpecialtyDetails == null)
                    profile.SpecialtyDetails = new List<SpecialtyDetail>();

                foreach (var item in specialityDetails)
                {
                    listBox1.Items.Add("\tAdding Speciality Details");
                    profile.SpecialtyDetails.Add(item);
                    listBox1.Items.Add("\t\tSpeciality ID: " + item.SpecialtyID);
                    listBox1.Items.Add("\t\tSpeciality : " + context.Specialities.Find(item.SpecialtyID).Name);
                    listBox1.Items.Add("\t\tSpecialty Preference: " + item.SpecialtyPreference);
                    listBox1.Items.Add("\t\tIsCurrentlyPracting: " + item.IsCurrentlyPracting);
                    listBox1.Items.Add("\t\tPercentageOfTime: " + item.PercentageOfTime);
                    listBox1.Items.Add("\t\tListedInHMO: " + item.ListedInHMO);
                    listBox1.Items.Add("\t\tListedInPPO: " + item.ListedInPPO);
                    listBox1.Items.Add("\t\tListedInPOS: " + item.ListedInPOS);
                    listBox1.Items.Add("\t\tIsBoardCertified: " + item.IsBoardCertified);
                    listBox1.Items.Add("\t\tSpeciality Board ID: " + item.SpecialtyBoardID);
                    listBox1.Items.Add("\t\tSpeciality Board : " + context.SpecialtyBoards.Find(item.SpecialtyBoardID).Name);
                    listBox1.Items.Add("\t\tExpiration Date: " + item.SpecialtyBoardCertifiedDetail.ExpirationDate);
                    listBox1.Items.Add("\t\tInitial Certification Date: " + item.SpecialtyBoardCertifiedDetail.InitialCertificationDate);
                    listBox1.Items.Add("\t\tLast ReCerification Date: " + item.SpecialtyBoardCertifiedDetail.LastReCerificationDate);
                    listBox1.Items.Add("\t\tBoard Certificate Path: " + item.SpecialtyBoardCertifiedDetail.BoardCertificatePath);

                }

                #endregion

                #region Professional References

                listBox1.Items.Add("Adding Professional References");
                var profRefs = (from x1 in xmlDoc.Descendants("d_prf_references")
                                from x in x1.Descendants("d_prf_references_row")
                                select new AHC.CD.Entities.MasterProfile.ProfessionalReference.ProfessionalReferenceInfo
                                {
                                    State = GetState(x.Element("pd_references_state").Value.Length > 0 ? x.Element("pd_references_state").Value : "Not Available"),
                                    LastName = x.Element("pd_references_last_name").Value.Length > 0 ? x.Element("pd_references_title").Value + "" + x.Element("pd_references_last_name").Value : "Not Available",
                                    Zipcode = x.Element("pd_references_zip").Value.Length > 0 ? x.Element("pd_references_zip").Value : "Not Available",
                                    PhoneNumber = FormatPhone(x.Element("pd_references_cust_5").Value.Length > 0 ? x.Element("pd_references_cust_5").Value : "Not Available"),
                                    Street = x.Element("pd_references_street").Value.Length > 0 ? x.Element("pd_references_street").Value : "Not Available",
                                    City = x.Element("pd_references_city").Value.Length > 0 ? x.Element("pd_references_city").Value : "Not Available",
                                    County = GetCountry(x.Element("pd_references_country").Value.Length > 0 ? x.Element("pd_references_country").Value : "Not Available"),
                                    FirstName = x.Element("pd_references_first_middle_name").Value.Length > 0 ? x.Element("pd_references_first_middle_name").Value : "Not Available",
                                    ProviderTypeID = GetProviderTypeID(x.Element("prof_suf_code").Value),
                                    
                                    // Mandatory Fields
                                    Degree = "Not Available",
                                    //UnitNumber = "Not Available",
                                    Building = "Not Available",
                                    
                                    Country = "Not Available",
                                    //Zipcode = "Not Available",
                                    Fax = "Not Available",
                                    IsBoardCerified = "Not Available",
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
                                     ProficiencyIndex = 0,
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
                                        ProviderDisclousreAnswer = x.Element("code_lookup_description").Value.Length > 0 ? x.Element("code_lookup_description").Value : "Not Available",
                                        Reason = x.Element("explain_yes").Value,
                                        // Mandatory Fields
                                     
                                    }).ToList();
                if (profile.ProfileDisclosure == null)
                    profile.ProfileDisclosure = new AHC.CD.Entities.MasterProfile.DisclosureQuestions.ProfileDisclosure();

                if (profile.ProfileDisclosure.ProfileDisclosureQuestionAnswers == null)
                    profile.ProfileDisclosure.ProfileDisclosureQuestionAnswers = new List<ProfileDisclosureQuestionAnswer>();

                foreach (var item in disclouserAns)
                {
                    profile.ProfileDisclosure.ProfileDisclosureQuestionAnswers.Add(item);
                    listBox1.Items.Add("\tAdding Disclouser Answers");
                    listBox1.Items.Add("\t\tQuestion :" + context.Questions.Find(item.QuestionID).Title );
                    listBox1.Items.Add("\tProvider Answer : " + item.ProviderDisclousreAnswer);
                    listBox1.Items.Add("\tReason : " + item.Reason);
                }

                #endregion

                #region Save Changes to DB

                //context.Profiles.Add(profile);

                try
                {
                    //Creata new login user with role as provider - getting back the UserID
                    
                    AHC.CD.Entities.User user = new AHC.CD.Entities.User();
                    user.StatusType = StatusType.Active;
                    user.Profile = profile;
                    
                    context.Users.Add(user);

                    context.SaveChanges();
                  
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
                    #endregion

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
                catch(Exception ex)
                {
                    #region Log the Migration status
                    FileStream stream = File.Create(@"D:\AHCDBMigration\Logs3\ErrorLogs.txt");
                    StreamWriter writer = new StreamWriter(stream);
                    writer.WriteLine(file);
                    writer.WriteLine(ex.Message);
                    writer.Close();
                    stream.Close();
                    MessageBox.Show(ex.Message);   
                    #endregion
                }
            }
        }

        private string FormatPhone(string phone)
        {
            string result="";
            if (phone.Contains("Not Available"))
                return phone;

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
                return "NotAvailable";

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
        
        private int GetProviderTypeID(string data)
        {
            ProviderType providerType = null;

            providerType = context.ProviderTypes.FirstOrDefault(pt => pt.Title.Replace(" ", "").ToLower() == data.Replace(" ", "").ToLower());
            if (providerType == null)
            {
                providerType = new ProviderType { Title = data};
                context.ProviderTypes.Add(providerType);
                context.SaveChanges();
            }
            return providerType.ProviderTypeID;
        }
        
        private string GetFilePath(string ProviderNPINumber, string documentName, string title, DateTime? expiryDate = null)
        {
            if (profile.ProfileDocuments == null)
                profile.ProfileDocuments = new List<ProfileDocument>();

            string filePath = @"D:\AHCDBMigration\Documents\" + ProviderNPINumber + @"\" + documentName;
            if (File.Exists(filePath))
            {
                // Create Document Folder
                string migrationProfileDocPath = @"D:\AHCDBMigration\Documents\MigrationDocs\" + title;
                if(!Directory.Exists(migrationProfileDocPath))
                {
                    Directory.CreateDirectory(migrationProfileDocPath);
                }
                // Copy file to application folder
                string uniqueKey = AHC.UtilityService.UniqueKeyGenerator.GetUniqueKey();
                string docPath = @"\Documents\" + title + @"\" + ProviderNPINumber +"-"+ documentName;
                File.Copy(filePath, migrationProfileDocPath + "\\" + ProviderNPINumber + "-" + documentName,true);
                //Store in Profile Documents
                ProfileDocument document = new ProfileDocument
                {
                    Title = title,
                    DocPath = docPath,
                    ExpiryDate = expiryDate
                };
                profile.ProfileDocuments.Add(document);
                return docPath;
            }
            else
            {
                return null;
            }
        }

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
                Question q = new Question { Title = questionTitle };
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
                    var stateLicense = context.StateLicenseStatuses.FirstOrDefault(sls => sls.Title.Equals("Active"));
                    if (stateLicense == null)
                    {
                        return new StateLicenseStatus{Title = " Active" , StatusType=StatusType.Active};
                    }
                    return stateLicense;
                }
                else if (stateLicenseStatus == "0")
                {
                    var stateLicense = context.StateLicenseStatuses.FirstOrDefault(sls => sls.Title.Equals("Inactive"));
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

        private int GetInsuranceCarrierID(string insuranceCarrierName,out int insurranceCarrierAddressID)
        {
            var insuranceCarrier = context.InsuranceCarriers.Include("InsuranceCarrierAddresses").FirstOrDefault(ic => ic.Name.Replace(" ", "").ToLower().Equals(insuranceCarrierName.Replace(" ", "").ToLower()));
            
            if (insuranceCarrier != null) // Master record found
            {
                insurranceCarrierAddressID = insuranceCarrier.InsuranceCarrierAddresses.ElementAt(0).InsuranceCarrierAddressID;
                return insuranceCarrier.InsuranceCarrierID;
            }
            else // Create master data
            {
   
    
                AHC.CD.Entities.MasterData.Tables.InsuranceCarrier ic = new AHC.CD.Entities.MasterData.Tables.InsuranceCarrier { Name = insuranceCarrierName, Code = "Not Available" };
                InsuranceCarrierAddress insuranceCarrierAddress = new InsuranceCarrierAddress();
                if (ic.InsuranceCarrierAddresses == null)
                {
                    ic.InsuranceCarrierAddresses = new List<InsuranceCarrierAddress>();
                    //insuranceCarrierAddress.UnitNumber = "Not Available";
                    //insuranceCarrierAddress.Number = "Not Available";
                    insuranceCarrierAddress.Street = "Not Available";
                    insuranceCarrierAddress.State = "Not Available";
                    insuranceCarrierAddress.City = "Not Available";
                    insuranceCarrierAddress.County = "Not Available";
                    insuranceCarrierAddress.Country = "Not Available";
                    insuranceCarrierAddress.ZipCode = "Not Available";
                    insuranceCarrierAddress.Building = "Not Available";
                }
                ic.InsuranceCarrierAddresses.Add(insuranceCarrierAddress);
                context.InsuranceCarriers.Add(ic);
                context.SaveChanges();
                insurranceCarrierAddressID = insuranceCarrierAddress.InsuranceCarrierAddressID;
                return ic.InsuranceCarrierID;
            }
        }

        private int GetSpecialityID(string specialityName)
        {
            var speciality = context.Specialities.FirstOrDefault(sp => sp.Name.Replace(" ", "").ToLower().Equals(specialityName.Replace(" ", "").ToLower()));
            if(speciality == null) //Speciality not found in master db, create a new speciality
            {
                Specialty s = new Specialty {Name = specialityName };
                context.Specialities.Add(s);
                context.SaveChanges();
                return s.SpecialtyID;
            }
            return speciality.SpecialtyID;

        }

        private int GetStaffCateogryID(string staffTitle)
        {
            var staffCategory = context.StaffCategories.FirstOrDefault(sc => sc.Title.Replace(" ", "").ToLower().Equals(staffTitle.Replace(" ", "").ToLower()));
            if (staffCategory == null)
            {
                StaffCategory sc = new StaffCategory { Title = staffTitle };
                context.StaffCategories.Add(sc);
                context.SaveChanges();
                return sc.StaffCategoryID;
            }
            else
            {
                return staffCategory.StaffCategoryID;
            }
        }

        private int GetHospitalID(string hospitalName)
        {
            var hospital = context.Hospitals.FirstOrDefault(h => h.HospitalName.Replace(" ", "").ToLower().Equals(hospitalName.Replace(" ", "").ToLower()));
            if(hospital == null)
            {
                Hospital h = new Hospital { HospitalName = hospitalName, Code = "Not Available" };
                context.Hospitals.Add(h);
                context.SaveChanges();
                return h.HospitalID;
            }
            else
            {
                return hospital.HospitalID;
            }
        }

        private int GetSpecialityBoardID(string boardName)
        {
            if (boardName.Length == 0)
                boardName = "Not Available";
            var specialityBoard = context.SpecialtyBoards.FirstOrDefault(sb => sb.Name.Replace(" ", "").ToLower().Equals(boardName.Replace(" ", "").ToLower()));
            if (specialityBoard == null)
            {
                
                SpecialtyBoard sb = new SpecialtyBoard { Name = boardName };
                context.SpecialtyBoards.Add(sb);
                context.SaveChanges();
                return sb.SpecialtyBoardID;
            }
            else
                return specialityBoard.SpecialtyBoardID;
        }

        #endregion

    }
}
