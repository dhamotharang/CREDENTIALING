using AHC.CD.Business.Profiles;
using AHC.CD.Data.ADO.CoreRepository;
using AHC.CD.Data.Repository;
using AHC.CD.Entities.MasterData.Account;
using AHC.CD.Entities.MasterData.Account.Branch;
using AHC.CD.Entities.MasterData.Account.Staff;
using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using AHC.CD.Entities.MasterProfile;
using AHC.CD.Entities.MasterProfile.BoardSpecialty;
using AHC.CD.Entities.MasterProfile.Contract;
using AHC.CD.Entities.MasterProfile.Demographics;
using AHC.CD.Entities.MasterProfile.IdentificationAndLicenses;
using AHC.CD.Entities.MasterProfile.PracticeLocation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace AHC.CD.WebApi.Controllers
{
    public class MigrationController : ApiController
    {
        //
        // GET: /Migration/

        IUnitOfWork uow;

        IProfileDataDuplicateManager ProfileDataDuplicateManager;

        IGenericRepository<Profile> ProfileRepository;

        IGenericRepository<ProviderLevel> ProviderLevelRepo;

        public MigrationController(IUnitOfWork uow, IProfileDataDuplicateManager ProfileDataDuplicateManager)
        {
            this.uow = uow;
            this.ProfileDataDuplicateManager = ProfileDataDuplicateManager;
            this.ProfileRepository = uow.GetGenericRepository<Profile>();
            this.ProviderLevelRepo = uow.GetGenericRepository<ProviderLevel>();
        }

         [HttpGet]
        public string InitiateMigration()
        {
            OptimumMigration();
            UltimateMigration();
            IPAProviderMigration();

            return "All Migration Completed";
        }

        #region OptimumMigration
        [HttpGet]
        public string OptimumMigration()
        {

            try
            {
                DataTable dataTable = new DataTable();

                // Read from ADO code 
                // Connection to edw db 
                using (SqlConnection dBConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EDW"].ToString()))
                {
                    using (var dBCommand = dBConnection.CreateCommand())
                    {
                        dBCommand.CommandText = @"SELECT * FROM [MDM].[dbo].[Optimum Provider Network 6-29-2016]";
                        dataTable = ADORepository.GetData(dBCommand);
                    }
                    // Iterate through the cursor 
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        string LastName = String.IsNullOrEmpty(dataRow["LAST_NAME"].ToString()) ? null : dataRow["LAST_NAME"].ToString().Trim();;

                        string FirstName = String.IsNullOrEmpty(dataRow["FIRST_NAME"].ToString()) ? null : dataRow["FIRST_NAME"].ToString().Trim();;

                        string MiddleName = String.IsNullOrEmpty(dataRow["MIDDLE_NAME"].ToString()) ? null : dataRow["MIDDLE_NAME"].ToString().Trim();;


                        Profile Profile = null;

                        // Check if the provider exist by fname , lname and mname

                        if (FirstName != null)
                        {
                            bool providerExist = uow.GetGenericRepository<PersonalDetail>().Any(p => p.FirstName.Equals(FirstName) && p.LastName.Equals(LastName) && p.MiddleName.Equals(MiddleName));

                            if (!providerExist)
                            {

                                Profile = new Profile();


                                #region Personal_Details

                                PersonalDetail PersonalDetail = null;

                                PersonalDetail = new PersonalDetail();

                                PersonalDetail.LastName = LastName;

                                PersonalDetail.FirstName = FirstName;

                                PersonalDetail.MiddleName = MiddleName;

                                PersonalDetail.Suffix = String.IsNullOrEmpty(dataRow["SUFFIX"].ToString()) ? " " : dataRow["SUFFIX"].ToString().Trim();;

                                var genderDBData = dataRow["GENDER"].ToString().Trim();

                                switch (genderDBData)
                                {

                                    case "M":
                                        genderDBData = "Male";
                                        break;

                                    case "F":
                                        genderDBData = "Female";
                                        break;

                                    default:
                                        genderDBData = "NotAvailable";

                                        break;
                                }

                                PersonalDetail.GenderType = (GenderType)Enum.Parse(typeof(GenderType), genderDBData);

                                PersonalDetail.SalutationType = SalutationType.NotAvailable;

                                // Language info starts 

                                string LANGUAGE_1 = String.IsNullOrEmpty(dataRow["LANGUAGE_1"].ToString()) ? " " : dataRow["LANGUAGE_1"].ToString().Trim();

                                string LANGUAGE_2 = String.IsNullOrEmpty(dataRow["LANGUAGE_2"].ToString()) ? " " : dataRow["LANGUAGE_2"].ToString().Trim();

                                string LANGUAGE_3 = String.IsNullOrEmpty(dataRow["LANGUAGE_3"].ToString()) ? " " : dataRow["LANGUAGE_3"].ToString().Trim();

                                string LANGUAGE_4 = String.IsNullOrEmpty(dataRow["LANGUAGE_4"].ToString()) ? " " : dataRow["LANGUAGE_4"].ToString().Trim();

                                LanguageInfo LanguageInfo = null;



                                LanguageInfo = new LanguageInfo();

                                LanguageInfo.KnownLanguages = new List<KnownLanguage>();

                                if (!String.IsNullOrEmpty(LANGUAGE_1))
                                {
                                    LanguageInfo.KnownLanguages.Add(new KnownLanguage { Language = LANGUAGE_1 });
                                }
                                if (!String.IsNullOrEmpty(LANGUAGE_2))
                                {
                                    LanguageInfo.KnownLanguages.Add(new KnownLanguage { Language = LANGUAGE_2 });
                                }
                                if (!String.IsNullOrEmpty(LANGUAGE_3))
                                {
                                    LanguageInfo.KnownLanguages.Add(new KnownLanguage { Language = LANGUAGE_3 });
                                }
                                if (!String.IsNullOrEmpty(LANGUAGE_4))
                                {
                                    LanguageInfo.KnownLanguages.Add(new KnownLanguage { Language = LANGUAGE_4 });
                                }

                                Profile.LanguageInfo = LanguageInfo;

                                // Language info ends


                                #endregion

                                //Masters Data

                                #region OtherData

                                string providerType = String.IsNullOrEmpty(dataRow["DEGREE"].ToString()) ? null : dataRow["DEGREE"].ToString().Trim();

                                ProviderTitle ProviderTitle = null;

                                ProviderTitle = new ProviderTitle();

                                ProviderType ProviderType = null;
                                if (uow.GetGenericRepository<ProviderType>().Any(p => p.Code.Equals(providerType)))
                                {
                                    ProviderType = uow.GetGenericRepository<ProviderType>().Get(p => p.Code.Equals(providerType)).First();
                                    ProviderTitle.ProviderTypeId = ProviderType.ProviderTypeID;
                                }


                                #endregion

                                //Update facility //Office manager 

                                // Assign providertype and provider title dedtails 

                                Profile.PersonalDetail = PersonalDetail;


                                #region Facilitydetails

                                Facility facility = null;

                                string PRACTICE_NAME = String.IsNullOrEmpty(dataRow["PRACTICE_NAME"].ToString()) ? null : dataRow["PRACTICE_NAME"].ToString().Trim();
                                string ADDRESS_1 = String.IsNullOrEmpty(dataRow["ADDRESS_1"].ToString()) ? null : dataRow["ADDRESS_1"].ToString().Trim();

                                if (PRACTICE_NAME != null)
                                {
                                    // Check if the facility already exist

                                    var facilityRepo = uow.GetGenericRepository<Facility>();



                                    if (facilityRepo.Any(f => f.FacilityName.Equals(PRACTICE_NAME) && f.Street.Equals(ADDRESS_1)))
                                    {
                                        facility = facilityRepo.Get(f => f.FacilityName.Equals(PRACTICE_NAME) && f.Street.Equals(ADDRESS_1)).First();
                                    }
                                    else
                                    {
                                        facility = new Facility();

                                        facility.FacilityName = PRACTICE_NAME;

                                        facility.Name = PRACTICE_NAME;

                                        facility.Street = ADDRESS_1;

                                        facility.City = String.IsNullOrEmpty(dataRow["CITY"].ToString()) ? null : dataRow["CITY"].ToString().Trim();

                                        facility.State = String.IsNullOrEmpty(dataRow["STATE"].ToString()) ? null : dataRow["STATE"].ToString().Trim();

                                        facility.ZipCode = String.IsNullOrEmpty(dataRow["ZIP_CODE"].ToString()) ? null : dataRow["ZIP_CODE"].ToString().Trim();

                                        facility.MobileNumber = String.IsNullOrEmpty(dataRow["PHONE"].ToString()) ? null : dataRow["PHONE"].ToString().Trim();

                                        facility.Fax = String.IsNullOrEmpty(dataRow["FAX"].ToString()) ? null : dataRow["FAX"].ToString().Trim();

                                        facilityRepo.Create(facility);

                                        facilityRepo.Save();
                                    }


                                    PracticeLocationDetail PracticeLocationDetail = null;

                                    PracticeLocationDetail = new PracticeLocationDetail();

                                    PracticeLocationDetail.PrimaryYesNoOption = YesNoOption.YES;

                                    PracticeLocationDetail.CurrentlyPracticingYesNoOption = YesNoOption.YES;

                                    PracticeLocationDetail.FacilityId = facility.FacilityID;

                                    if (Profile.PracticeLocationDetails == null)
                                    {
                                        Profile.PracticeLocationDetails = new List<PracticeLocationDetail>();
                                    }

                                    Profile.PracticeLocationDetails.Add(PracticeLocationDetail);

                                }

                                #endregion

                                ProfileRepository.Create(Profile);

                                ProfileRepository.Save();

                                // Save sync to db for Profile Object 

                            }
                        }

                        //if exist update other details 
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }


            return "Migration Completed Successfully";
        }
        #endregion

        #region UltimateMIgration


        [HttpGet]
        public string UltimateMigration()
        {

            try
            {
                DataTable dataTable = new DataTable();

                // Read from ADO code 
                // Connection to edw db 
                using (SqlConnection dBConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EDW"].ToString()))
                {
                    using (var dBCommand = dBConnection.CreateCommand())
                    {
                        dBCommand.CommandText = @"SELECT * FROM [MDM].[dbo].[Ultimate Health Plans Practitioner Report]";
                        dataTable = ADORepository.GetData(dBCommand);
                    }
                    // Iterate through the cursor 
                    dBConnection.Dispose();
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        string LastName = String.IsNullOrEmpty(dataRow["LAST_NAME"].ToString()) ? null : dataRow["LAST_NAME"].ToString().Trim();

                        string FirstName = String.IsNullOrEmpty(dataRow["FIRST_NAME"].ToString()) ? null : dataRow["FIRST_NAME"].ToString().Trim();

                        string MiddleName = String.IsNullOrEmpty(dataRow["MIDDLE_NAME"].ToString()) ? null : dataRow["MIDDLE_NAME"].ToString().Trim();

                        string NPI = String.IsNullOrEmpty(dataRow["NPI"].ToString()) ? null : dataRow["NPI"].ToString().Trim();

                        Profile Profile = null;

                        // Check if the provider exist by fname , lname and mname

                        bool providerExist = uow.GetGenericRepository<PersonalDetail>().Any(p => p.FirstName.Equals(FirstName) && p.LastName.Equals(LastName) && p.MiddleName.Equals(MiddleName));

                        if (!providerExist)
                        {
                            providerExist=!ProfileDataDuplicateManager.IsNPINumberDoesNotExists(NPI);
                        }

                        if (!providerExist)
                        {

                            Profile = new Profile();

                            PersonalDetail PersonalDetail = null;

                            PersonalDetail = new PersonalDetail();

                            #region OtherIdentificationLicenses

                            OtherIdentificationNumber OtherIdentificationNumber = null;

                            OtherIdentificationNumber = new OtherIdentificationNumber();

                            OtherIdentificationNumber.NPINumber = NPI;

                            Profile.OtherIdentificationNumber = OtherIdentificationNumber;


                            #endregion

                            #region Personal_Details

                            #region ProviderLevel

                            string providerPracticeAs = String.IsNullOrEmpty(dataRow["Practice_As"].ToString()) ? " " : dataRow["Practice_As"].ToString().Trim();

                            bool leveExist = ProviderLevelRepo.Any(pl => pl.Name.Equals(providerPracticeAs));

                            if (leveExist)
                            {
                                PersonalDetail.ProviderLevelID = ProviderLevelRepo.Get(pl => pl.Name.Equals(providerPracticeAs)).First().ProviderLevelID;
                            }
                            else
                            {
                                var providerLevel = new ProviderLevel();

                                providerLevel.Name = providerPracticeAs;
                                providerLevel.StatusType = StatusType.Active;
                                ProviderLevelRepo.Create(providerLevel);
                                ProviderLevelRepo.Save();
                                PersonalDetail.ProviderLevelID = providerLevel.ProviderLevelID;

                            }


                            #endregion



                            PersonalDetail.LastName = LastName;

                            PersonalDetail.FirstName = FirstName;

                            PersonalDetail.MiddleName = MiddleName;

                            var genderDBData = dataRow["Gender"].ToString().Trim();

                            switch (genderDBData)
                            {

                                case "M":
                                    genderDBData = "Male";
                                    break;

                                case "F":
                                    genderDBData = "Female";
                                    break;

                                default:
                                    genderDBData = "NotAvailable";

                                    break;
                            }

                            PersonalDetail.GenderType = (GenderType)Enum.Parse(typeof(GenderType), genderDBData);

                            PersonalDetail.SalutationType = SalutationType.NotAvailable;

                            // Language info starts 

                            LanguageInfo LanguageInfo = null;

                            LanguageInfo = new LanguageInfo();

                            LanguageInfo.KnownLanguages = new List<KnownLanguage>();

                            string languages = String.IsNullOrEmpty(dataRow["Languages"].ToString()) ? null : dataRow["Languages"].ToString().Trim();

                            if (languages != null)
                            {
                                foreach (var lang in languages.Split('|'))
                                {
                                    LanguageInfo.KnownLanguages.Add(new KnownLanguage { Language = lang });
                                }
                            }
                            Profile.LanguageInfo = LanguageInfo;

                            // Language info ends


                            #endregion

                            //Masters Data

                            #region OtherData

                            string providerType = String.IsNullOrEmpty(dataRow["Practitioner_Type"].ToString()) ? null : dataRow["Practitioner_Type"].ToString().Trim();

                            ProviderTitle ProviderTitle = null;

                            ProviderTitle = new ProviderTitle();

                            ProviderType ProviderType = null;
                            if (uow.GetGenericRepository<ProviderType>().Any(p => p.Code.Equals(providerType)))
                            {
                                ProviderType = uow.GetGenericRepository<ProviderType>().Get(p => p.Code.Equals(providerType)).First();
                                ProviderTitle.ProviderTypeId = ProviderType.ProviderTypeID;
                            }


                            #endregion

                            //Update facility //Office manager 

                            // Assign providertype and provider title dedtails 

                            Profile.PersonalDetail = PersonalDetail;


                            #region Facilitydetails

                            Facility facility = null;

                            string PRACTICE_NAME = String.IsNullOrEmpty(dataRow["Location"].ToString()) ? null : dataRow["Location"].ToString().Trim();
                            string ADDRESS_1 = String.IsNullOrEmpty(dataRow["Address"].ToString()) ? null : dataRow["Address"].ToString().Trim();

                            if (PRACTICE_NAME != null)
                            {
                                // Check if the facility already exist

                                var facilityRepo = uow.GetGenericRepository<Facility>();



                                if (facilityRepo.Any(f => f.FacilityName.Equals(PRACTICE_NAME) && f.Street.Equals(ADDRESS_1)))
                                {
                                    facility = facilityRepo.Get(f => f.FacilityName.Equals(PRACTICE_NAME) && f.Street.Equals(ADDRESS_1)).First();
                                }
                                else
                                {
                                    facility = new Facility();

                                    facility.FacilityName = PRACTICE_NAME;

                                    facility.Name = PRACTICE_NAME;

                                    facility.Street = ADDRESS_1;

                                    facility.City = String.IsNullOrEmpty(dataRow["CITY"].ToString()) ? null : dataRow["CITY"].ToString().Trim();

                                    facility.City = String.IsNullOrEmpty(dataRow["County"].ToString()) ? null : dataRow["County"].ToString().Trim();

                                    facility.State = String.IsNullOrEmpty(dataRow["STATE"].ToString()) ? null : dataRow["STATE"].ToString().Trim();

                                    facility.ZipCode = String.IsNullOrEmpty(dataRow["Zip"].ToString()) ? null : dataRow["Zip"].ToString().Trim();

                                    facility.MobileNumber = String.IsNullOrEmpty(dataRow["PHONE"].ToString()) ? null : dataRow["PHONE"].ToString().Trim();

                                    facility.Fax = String.IsNullOrEmpty(dataRow["FAX"].ToString()) ? null : dataRow["FAX"].ToString().Trim();

                                    facilityRepo.Create(facility);

                                    facilityRepo.Save();
                                }


                                PracticeLocationDetail PracticeLocationDetail = null;

                                PracticeLocationDetail = new PracticeLocationDetail();

                                PracticeLocationDetail.PrimaryYesNoOption = YesNoOption.YES;

                                PracticeLocationDetail.CurrentlyPracticingYesNoOption = YesNoOption.YES;

                                PracticeLocationDetail.FacilityId = facility.FacilityID;

                                if (Profile.PracticeLocationDetails == null)
                                {
                                    Profile.PracticeLocationDetails = new List<PracticeLocationDetail>();
                                }

                                Profile.PracticeLocationDetails.Add(PracticeLocationDetail);

                            }

                            #endregion
                            var profileRepository = uow.GetGenericRepository<Profile>();
                            profileRepository.Create(Profile);

                            ProfileRepository.Save();

                            // Save sync to db for Profile Object 

                        }


                        //if exist update other details 
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }


            return "Ultimate Migration Completed Successfully";
        }


        #endregion

        #region IPAProviderMigration

        [HttpGet]
        public string IPAProviderMigration()
        {

            try
            {
                DataTable dataTable = new DataTable();

                // Read from ADO code 
                // Connection to edw db 
                using (SqlConnection dBConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["EDW"].ToString()))
                {
                    using (var dBCommand = dBConnection.CreateCommand())
                    {
                        dBCommand.CommandText = @"SELECT * FROM [IPA_June_2016_Complete]";
                        dataTable = ADORepository.GetData(dBCommand);
                    }
                    // Iterate through the cursor 
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        try
                        {
                            string LastName = String.IsNullOrEmpty(dataRow["Physician_last_name"].ToString()) ? null : dataRow["Physician_last_name"].ToString().Trim();

                            string FirstName = String.IsNullOrEmpty(dataRow["Physician_first_name"].ToString()) ? null : dataRow["Physician_first_name"].ToString().Trim();


                            string NPI = String.IsNullOrEmpty(dataRow["NPI"].ToString()) ? null : dataRow["NPI"].ToString().Trim();

                            Profile Profile = null;

                            // Check if the provider exist by NPI

                            bool providerDoesNotExist = ProfileDataDuplicateManager.IsNPINumberDoesNotExists(NPI);

                            if (providerDoesNotExist)
                            {
                                // Check if the provider exist by fname , lname and mname

                                bool providerExist = uow.GetGenericRepository<PersonalDetail>().Any(p => p.FirstName.Equals(FirstName) && p.LastName.Equals(LastName));
                                if (!providerExist)
                                {

                                    Profile = new Profile();

                                    PersonalDetail PersonalDetail = null;

                                    PersonalDetail = new PersonalDetail();

                                    #region OtherIdentificationLicenses

                                    if (!String.IsNullOrEmpty(NPI))
                                    {
                                        OtherIdentificationNumber OtherIdentificationNumber = null;

                                        OtherIdentificationNumber = new OtherIdentificationNumber();

                                        OtherIdentificationNumber.NPINumber = NPI;

                                        Profile.OtherIdentificationNumber = OtherIdentificationNumber; 
                                    }

                                    #endregion

                                    #region Personal_Details

                                    #region ProviderLevel

                                    string providerPracticeAs = String.IsNullOrEmpty(dataRow["Title"].ToString()) ? " " : dataRow["Title"].ToString().Trim();

                                    bool leveExist = ProviderLevelRepo.Any(pl => pl.Name.Equals(providerPracticeAs));

                                    if (leveExist)
                                    {
                                        PersonalDetail.ProviderLevelID = ProviderLevelRepo.Get(pl => pl.Name.Equals(providerPracticeAs)).First().ProviderLevelID;
                                    }
                                    else
                                    {
                                        var providerLevel = new ProviderLevel();

                                        providerLevel.Name = providerPracticeAs;
                                        providerLevel.StatusType = StatusType.Active;
                                        ProviderLevelRepo.Create(providerLevel);
                                        ProviderLevelRepo.Save();
                                        PersonalDetail.ProviderLevelID = providerLevel.ProviderLevelID;

                                    }

                                    #endregion

                                    PersonalDetail.LastName = LastName;

                                    PersonalDetail.FirstName = FirstName;

                                    PersonalDetail.GenderType = GenderType.NotAvailable;

                                    PersonalDetail.SalutationType = SalutationType.NotAvailable;

                                    #endregion

                                    //Masters Data

                                    #region OtherData

                                    string providerType = String.IsNullOrEmpty(dataRow["Title"].ToString()) ? null : dataRow["Title"].ToString().Trim();

                                    ProviderTitle ProviderTitle = null;

                                    ProviderTitle = new ProviderTitle();

                                    ProviderTitle.StatusType = StatusType.Active;

                                    ProviderType ProviderType = null;

                                    var providerTypeRepository = this.uow.GetGenericRepository<ProviderType>();

                                    if (!providerTypeRepository.Any(p => p.Title.Equals(providerType)))
                                    {
                                        ProviderType = new ProviderType();
                                        ProviderType.Title = providerType;
                                        providerTypeRepository.Create(ProviderType);
                                        providerTypeRepository.Save();
                                    }

                                    ProviderType = providerTypeRepository.Get(p => p.Title.Equals(providerType)).First();
                                    ProviderTitle.ProviderTypeId = ProviderType.ProviderTypeID;

                                    PersonalDetail.ProviderTitles = new List<ProviderTitle>();

                                    PersonalDetail.ProviderTitles.Add(ProviderTitle);

                                    #endregion

                                    //Update facility //Office manager 

                                    // Assign providertype and provider title dedtails 

                                    Profile.PersonalDetail = PersonalDetail;


                                    #region Facilitydetails

                                    Facility facility = null;

                                    string ADDRESS_1 = String.IsNullOrEmpty(dataRow["Address_1"].ToString()) ? null : dataRow["Address_1"].ToString().Trim();
                                    string CITY = String.IsNullOrEmpty(dataRow["city"].ToString()) ? null : dataRow["city"].ToString().Trim();

                                    if (ADDRESS_1 != null)
                                    {
                                        // Check if the facility already exist

                                        var facilityRepo = uow.GetGenericRepository<Facility>();



                                        if (facilityRepo.Any(f => f.Street.Equals(ADDRESS_1) && f.City.Equals(CITY)))
                                        {
                                            facility = facilityRepo.Get(f => f.Street.Equals(ADDRESS_1) && f.City.Equals(CITY)).First();
                                        }
                                        else
                                        {
                                            facility = new Facility();

                                            //facility.FacilityName = PRACTICE_NAME;

                                            facility.Name = "Unknown Facility "+Guid.NewGuid();

                                            facility.Street = ADDRESS_1;

                                            facility.Building = String.IsNullOrEmpty(dataRow["Address_2"].ToString()) ? null : dataRow["Address_2"].ToString().Trim();

                                            facility.City = String.IsNullOrEmpty(dataRow["CITY"].ToString()) ? null : dataRow["CITY"].ToString().Trim();

                                            facility.City = String.IsNullOrEmpty(dataRow["County"].ToString()) ? null : dataRow["County"].ToString().Trim();

                                            facility.State = String.IsNullOrEmpty(dataRow["STATE"].ToString()) ? null : dataRow["STATE"].ToString().Trim();

                                            facility.ZipCode = String.IsNullOrEmpty(dataRow["Zip"].ToString()) ? null : dataRow["Zip"].ToString().Trim();

                                            facility.MobileNumber = String.IsNullOrEmpty(dataRow["PHONE"].ToString()) ? null : dataRow["PHONE"].ToString().Trim();

                                            facility.Fax = String.IsNullOrEmpty(dataRow["FAX"].ToString()) ? null : dataRow["FAX"].ToString().Trim();

                                            facilityRepo.Create(facility);

                                            facilityRepo.Save();
                                        }


                                        PracticeLocationDetail PracticeLocationDetail = null;

                                        PracticeLocationDetail = new PracticeLocationDetail();

                                        PracticeLocationDetail.PrimaryYesNoOption = YesNoOption.YES;

                                        PracticeLocationDetail.CurrentlyPracticingYesNoOption = YesNoOption.YES;

                                        PracticeLocationDetail.FacilityId = facility.FacilityID;


                                        //Office Manager
                                        string OfficeManagerFullName, officeManagerLastName = null, officeManagerFirstName = null, officeManagerMiddleName = null;

                                        OfficeManagerFullName = String.IsNullOrEmpty(dataRow["Office_Manager"].ToString()) ? null : dataRow["Office_Manager"].ToString().Trim();

                                        if (!string.IsNullOrEmpty(OfficeManagerFullName))
                                        {

                                            officeManagerLastName = OfficeManagerFullName.Split(' ').FirstOrDefault().Trim();

                                            officeManagerFirstName = OfficeManagerFullName.Replace(officeManagerLastName, "");

                                            if (!string.IsNullOrEmpty(officeManagerFirstName))
                                            {
                                                officeManagerFirstName = officeManagerFirstName.Trim().Split(' ').FirstOrDefault().Trim();
                                                officeManagerMiddleName = OfficeManagerFullName.Replace(officeManagerFirstName, "").Replace(OfficeManagerFullName, "");
                                                if (!string.IsNullOrEmpty(officeManagerMiddleName))
                                                {
                                                    officeManagerMiddleName = officeManagerMiddleName.Trim();
                                                }
                                            }
                                        }
                                        Employee employee = null;

                                        var EmployeeRepository = uow.GetGenericRepository<Employee>();

                                        if (EmployeeRepository.Any(e => e.FirstName.Equals(officeManagerFirstName) && e.LastName.Equals(officeManagerLastName)))
                                        {
                                            employee = EmployeeRepository.Get(e => e.FirstName.Equals(officeManagerFirstName) && e.LastName.Equals(officeManagerLastName)).FirstOrDefault();
                                            PracticeLocationDetail.BusinessOfficeManagerOrStaffId = employee.EmployeeID;
                                        }

                                        else
                                        {
                                            employee = new Employee();
                                            employee.FirstName = officeManagerFirstName;
                                            employee.LastName = officeManagerLastName;
                                            employee.MiddleName = officeManagerMiddleName;
                                            employee.StatusType = StatusType.Active;
                                            EmployeeRepository.Create(employee);
                                            EmployeeRepository.Save();
                                            PracticeLocationDetail.BusinessOfficeManagerOrStaffId = employee.EmployeeID;
                                        }



                                        if (Profile.PracticeLocationDetails == null)
                                        {
                                            Profile.PracticeLocationDetails = new List<PracticeLocationDetail>();
                                        }

                                        PracticeLocationDetail.StatusType = StatusType.Active;

                                        Profile.PracticeLocationDetails.Add(PracticeLocationDetail);

                                    }



                                    //contact details

                                    EmailDetail emailDetail = null;

                                    string EmailAddress = String.IsNullOrEmpty(dataRow["Physican_E-mail_or_alt_email_for_office_mgr."].ToString()) ? null : dataRow["Physican_E-mail_or_alt_email_for_office_mgr."].ToString().Trim();

                                    emailDetail = new EmailDetail();

                                    Profile.ContactDetail = new ContactDetail();
                                    Profile.ContactDetail.EmailIDs = new List<EmailDetail>();
                                    if (uow.GetGenericRepository<EmailDetail>().Any(e => e.EmailAddress.Equals(EmailAddress)))
                                    {
                                        emailDetail = uow.GetGenericRepository<EmailDetail>().Get(e => e.EmailAddress.Equals(EmailAddress)).First();
                                        Profile.ContactDetail.EmailIDs.Add(emailDetail);
                                    }
                                    else
                                    {
                                        var EmailDetailRepo = uow.GetGenericRepository<EmailDetail>();
                                        emailDetail = new EmailDetail();
                                        emailDetail.EmailAddress = EmailAddress;
                                        emailDetail.StatusType = StatusType.Active;
                                        EmailDetailRepo.Create(emailDetail);
                                        EmailDetailRepo.Save();
                                        Profile.ContactDetail.EmailIDs.Add(emailDetail);
                                    }

                                    PhoneDetail PhoneDetail = null;
                                    Profile.ContactDetail.PhoneDetails = new List<PhoneDetail>();
                                    string PhoneNo = String.IsNullOrEmpty(dataRow["Physician_Cell_#"].ToString()) ? null : dataRow["Physician_Cell_#"].ToString().Trim();

                                    PhoneDetail = new PhoneDetail();

                                    if (uow.GetGenericRepository<PhoneDetail>().Any(p => p.PhoneNumber.Equals(PhoneNo)))
                                    {
                                        PhoneDetail = uow.GetGenericRepository<PhoneDetail>().Get(p => p.PhoneNumber.Equals(PhoneNo)).First();
                                        Profile.ContactDetail.PhoneDetails.Add(PhoneDetail);
                                    }
                                    else
                                    {
                                        var PhoneDetailRepo = uow.GetGenericRepository<PhoneDetail>();
                                        emailDetail = new EmailDetail();
                                        emailDetail.EmailAddress = PhoneNo;
                                        emailDetail.StatusType = StatusType.Active;
                                        PhoneDetailRepo.Create(PhoneDetail);
                                        PhoneDetailRepo.Save();
                                        Profile.ContactDetail.PhoneDetails.Add(PhoneDetail);
                                    }



                                    SpecialtyDetail SpecialtyDetail = null;

                                    string specialtyName = String.IsNullOrEmpty(dataRow["Specialty"].ToString()) ? null : dataRow["Specialty"].ToString().Trim();

                                    if (!String.IsNullOrEmpty(specialtyName))
                                    {
                                        SpecialtyDetail = null;

                                        SpecialtyDetail = new SpecialtyDetail();

                                        SpecialtyDetail.PreferenceType = PreferenceType.Primary;
                                        SpecialtyDetail.BoardCertifiedYesNoOption = YesNoOption.YES;

                                        Profile.SpecialtyDetails = new List<SpecialtyDetail>();

                                        var SpecialityRepo = uow.GetGenericRepository<Specialty>();
                                        if (SpecialityRepo.Any(s => s.Name.Equals(specialtyName)))
                                        {
                                            SpecialtyDetail.SpecialtyID = uow.GetGenericRepository<Specialty>().Get(s => s.Name.Equals(specialtyName)).First().SpecialtyID;

                                        }
                                        else
                                        {
                                            Specialty Speciality = null;
                                            Speciality = new Specialty();
                                            Speciality.StatusType = StatusType.Active;
                                            Speciality.Name = specialtyName;
                                            SpecialityRepo.Create(Speciality);
                                            SpecialityRepo.Save();
                                            SpecialtyDetail.SpecialtyID = Speciality.SpecialtyID;
                                        }
                                        Profile.SpecialtyDetails.Add(SpecialtyDetail); 
                                    }
                                    #region ContractInfo

                                    //ContractInfo contract = null;

                                    //contract = new ContractInfo();

                                    //contract.ProviderRelationship = String.IsNullOrEmpty(dataRow["Owned/Affiliate"].ToString()) ? null : dataRow["Owned/Affiliate"].ToString().Trim();


                                    //ContractGroupInfo contractGroupInfo = null;

                                    //contractGroupInfo = new ContractGroupInfo();

                                    //string GroupName = String.IsNullOrEmpty(dataRow["Group_Name"].ToString()) ? null : dataRow["Group_Name"].ToString().Trim();

                                    //contractGroupInfo.PracticingGroup = new PracticingGroup();

                                    //contract.ContractGroupInfoes = new List<ContractGroupInfo>();

                                    // var contractInfoes = new List<ContractInfo>();

                                    //Group group = null;

                                    //var GroupInfoRepo = uow.GetGenericRepository<Group>();

                                    //if (!GroupInfoRepo.Any(g => g.Name == GroupName))
                                    //{


                                    //    group = new Group();
                                    //    group.Name = GroupName;
                                    //    GroupInfoRepo.Create(group);
                                    //    GroupInfoRepo.Save();

                                    //}
                                    //var PracticingGroupRepo = uow.GetGenericRepository<PracticingGroup>();

                                    //group = GroupInfoRepo.Get(g => g.Name.Equals(GroupName)).FirstOrDefault();

                                    //PracticingGroup practicingGroup = null;

                                    //practicingGroup = new PracticingGroup();

                                    //practicingGroup.Group = group;

                                    //if (!PracticingGroupRepo.Any(g => g.Group.Name == group.Name))
                                    //{
                                    //    PracticingGroupRepo.Create(practicingGroup);

                                    //}

                                    //practicingGroup = PracticingGroupRepo.Get(g => g.GroupId == group.GroupID).FirstOrDefault();


                                    //var ContractGroupInfoRepo = uow.GetGenericRepository<ContractGroupInfo>();

                                    //ContractGroupInfo ContractGroupInfo = null;

                                    //ContractGroupInfo = new ContractGroupInfo();

                                    //ContractGroupInfo.PracticingGroup = practicingGroup;

                                    //if (!ContractGroupInfoRepo.Any(g => g.ContractGroupInfoId == practicingGroup.GroupId))
                                    //{
                                    //    ContractGroupInfoRepo.Create(ContractGroupInfo);
                                    //}

                                    //ContractGroupInfo = ContractGroupInfoRepo.Get(g => g.ContractGroupInfoId == practicingGroup.GroupId).FirstOrDefault();



                                    //contract.ContractGroupInfoes.Add(contractGroupInfo);

                                    //var ContractInfoRepo = uow.GetGenericRepository<ContractInfo>();

                                    ////ContractInfo.ContractInfo = ContractGroupInfo.ContractGroupInfoId;

                                    ////if (!ContractInfoRepo.Any(g => g.ContractInfoID == ContractGroupInfo.ContractGroupInfoId))
                                    ////{
                                    ////    ContractInfoRepo.Create(ContractInfo);

                                    ////}

                                    ////ContractInfo = ContractInfoRepo.Get(g => g.ContractInfoID == ContractGroupInfo.ContractGroupInfoId).FirstOrDefault();


                                    //Profile.ContractInfoes.Add(contract); 
                                    #endregion

                                    string LicenseNumber=String.IsNullOrEmpty(dataRow["License_#"].ToString()) ? null : dataRow["License_#"].ToString().Trim();

                                    if (LicenseNumber!=null)
                                    {
                                        StateLicenseInformation stateLicenseInformation = null;

                                        stateLicenseInformation = new StateLicenseInformation();

                                        stateLicenseInformation.StatusType = StatusType.Active;

                                        Profile.StateLicenses = new List<StateLicenseInformation>();

                                        stateLicenseInformation.LicenseNumber = LicenseNumber;

                                        Profile.StateLicenses.Add(stateLicenseInformation); 
                                    }


                                    #endregion

                                    Profile.StatusType = StatusType.Active;

                                    try
                                    {
                                        ProfileRepository.Create(Profile);

                                        ProfileRepository.Save();
                                    }
                                    catch (Exception)
                                    {

                                        throw;
                                    }


                                    // Save sync to db for Profile Object 

                                }
                                //if exist update other details 

                            }
                        }
                        catch (Exception)
                        {
                            
                            throw;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return "IPA Provider Migration Completed Successfully";
        }

        #endregion 
    }

    public class ADORepository
    {
        public static DataTable GetData(SqlCommand cmd)
        {
            try
            {
                DataTable resultSet;
                using (SqlDataAdapter dataAdapterObject = new SqlDataAdapter(cmd))
                {
                    resultSet = new DataTable();
                    dataAdapterObject.Fill(resultSet);
                }
                return resultSet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetConnectionString(DataBaseSchemaEnum type)
        {
            try
            {
                string connectionString = null;

                switch (type)
                {
                    case DataBaseSchemaEnum.CredentialingConnectionString:
                        connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["EFEntityContext"].ToString().Trim();
                        break;
                }

                return connectionString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}