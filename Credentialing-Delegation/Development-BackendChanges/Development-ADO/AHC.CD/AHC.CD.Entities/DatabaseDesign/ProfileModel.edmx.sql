
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/05/2015 18:12:17
-- Generated from EDMX file: F:\Office\HealthCare\Credentialing\Source\Credentialing-Delegation\Development\AHC.CD\AHC.CD.Entities\DatabaseDesign\ProfileModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [AHCCDProfile];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_RoleUser_Role]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RoleUser] DROP CONSTRAINT [FK_RoleUser_Role];
GO
IF OBJECT_ID(N'[dbo].[FK_RoleUser_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RoleUser] DROP CONSTRAINT [FK_RoleUser_User];
GO
IF OBJECT_ID(N'[dbo].[FK_UserProfile_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserProfile] DROP CONSTRAINT [FK_UserProfile_User];
GO
IF OBJECT_ID(N'[dbo].[FK_UserProfile_Profile]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserProfile] DROP CONSTRAINT [FK_UserProfile_Profile];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonalDetailProviderType_PersonalDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonalDetailProviderType] DROP CONSTRAINT [FK_PersonalDetailProviderType_PersonalDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonalDetailProviderType_ProviderType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonalDetailProviderType] DROP CONSTRAINT [FK_PersonalDetailProviderType_ProviderType];
GO
IF OBJECT_ID(N'[dbo].[FK_ProfileOtherLegalName]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OtherLegalNames] DROP CONSTRAINT [FK_ProfileOtherLegalName];
GO
IF OBJECT_ID(N'[dbo].[FK_ProfilePersonalDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Profiles] DROP CONSTRAINT [FK_ProfilePersonalDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_HospitalContactInfoHospitalContactPerson]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HospitalContactPersons] DROP CONSTRAINT [FK_HospitalContactInfoHospitalContactPerson];
GO
IF OBJECT_ID(N'[dbo].[FK_HospitalHospitalContactInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HospitalContactInfoes] DROP CONSTRAINT [FK_HospitalHospitalContactInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_HospitalPrivilegeHospital]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HospitalPrivileges] DROP CONSTRAINT [FK_HospitalPrivilegeHospital];
GO
IF OBJECT_ID(N'[dbo].[FK_HospitalPrivilegeHospitalContactInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HospitalPrivileges] DROP CONSTRAINT [FK_HospitalPrivilegeHospitalContactInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_HospitalPrivilegeHospitalContactPerson]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HospitalPrivileges] DROP CONSTRAINT [FK_HospitalPrivilegeHospitalContactPerson];
GO
IF OBJECT_ID(N'[dbo].[FK_HospitalPrivilegeStaffCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HospitalPrivileges] DROP CONSTRAINT [FK_HospitalPrivilegeStaffCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_HospitalPrivilegeAdmittingPrivilege]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HospitalPrivileges] DROP CONSTRAINT [FK_HospitalPrivilegeAdmittingPrivilege];
GO
IF OBJECT_ID(N'[dbo].[FK_SpecialtyProviderType_Specialty]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SpecialtyProviderType] DROP CONSTRAINT [FK_SpecialtyProviderType_Specialty];
GO
IF OBJECT_ID(N'[dbo].[FK_SpecialtyProviderType_ProviderType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SpecialtyProviderType] DROP CONSTRAINT [FK_SpecialtyProviderType_ProviderType];
GO
IF OBJECT_ID(N'[dbo].[FK_HospitalPrivilegeSpecialty]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HospitalPrivileges] DROP CONSTRAINT [FK_HospitalPrivilegeSpecialty];
GO
IF OBJECT_ID(N'[dbo].[FK_ProfileHospitalPrivilege]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HospitalPrivileges] DROP CONSTRAINT [FK_ProfileHospitalPrivilege];
GO
IF OBJECT_ID(N'[dbo].[FK_StateLicenseStateLicenseType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StateLicenses] DROP CONSTRAINT [FK_StateLicenseStateLicenseType];
GO
IF OBJECT_ID(N'[dbo].[FK_StateLicenseStateLicenceStatus]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StateLicenses] DROP CONSTRAINT [FK_StateLicenseStateLicenceStatus];
GO
IF OBJECT_ID(N'[dbo].[FK_ProfileStateLicense]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StateLicenses] DROP CONSTRAINT [FK_ProfileStateLicense];
GO
IF OBJECT_ID(N'[dbo].[FK_DEAScheduleDEAScheduleType_DEASchedule]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DEAScheduleDEAScheduleType] DROP CONSTRAINT [FK_DEAScheduleDEAScheduleType_DEASchedule];
GO
IF OBJECT_ID(N'[dbo].[FK_DEAScheduleDEAScheduleType_DEAScheduleType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DEAScheduleDEAScheduleType] DROP CONSTRAINT [FK_DEAScheduleDEAScheduleType_DEAScheduleType];
GO
IF OBJECT_ID(N'[dbo].[FK_DEAScheduleInfoDEASchedule]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DEAScheduleInfoes] DROP CONSTRAINT [FK_DEAScheduleInfoDEASchedule];
GO
IF OBJECT_ID(N'[dbo].[FK_FederalDEAInfoDEAScheduleInfo_FederalDEAInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FederalDEAInfoDEAScheduleInfo] DROP CONSTRAINT [FK_FederalDEAInfoDEAScheduleInfo_FederalDEAInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_FederalDEAInfoDEAScheduleInfo_DEAScheduleInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FederalDEAInfoDEAScheduleInfo] DROP CONSTRAINT [FK_FederalDEAInfoDEAScheduleInfo_DEAScheduleInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_DEAScheduleInfoDEAScheduleType_DEAScheduleInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DEAScheduleInfoDEAScheduleType] DROP CONSTRAINT [FK_DEAScheduleInfoDEAScheduleType_DEAScheduleInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_DEAScheduleInfoDEAScheduleType_DEAScheduleType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DEAScheduleInfoDEAScheduleType] DROP CONSTRAINT [FK_DEAScheduleInfoDEAScheduleType_DEAScheduleType];
GO
IF OBJECT_ID(N'[dbo].[FK_ProfileFederalDEAInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FederalDEAInfoes] DROP CONSTRAINT [FK_ProfileFederalDEAInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_ProfileMedicareMedicaidInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MedicareMedicaidInfoes] DROP CONSTRAINT [FK_ProfileMedicareMedicaidInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_ProfileCDSCInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CDSCInfoes] DROP CONSTRAINT [FK_ProfileCDSCInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_ProfileOtherIdentification]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Profiles] DROP CONSTRAINT [FK_ProfileOtherIdentification];
GO
IF OBJECT_ID(N'[dbo].[FK_ProfileAddress]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HomeAddresses] DROP CONSTRAINT [FK_ProfileAddress];
GO
IF OBJECT_ID(N'[dbo].[FK_ProfilePersonalIdentification]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Profiles] DROP CONSTRAINT [FK_ProfilePersonalIdentification];
GO
IF OBJECT_ID(N'[dbo].[FK_ProfileVisaDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Profiles] DROP CONSTRAINT [FK_ProfileVisaDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_ProfileLanguageInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LanguageInfoes] DROP CONSTRAINT [FK_ProfileLanguageInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_ContactDetailsPhoneDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PhoneDetails] DROP CONSTRAINT [FK_ContactDetailsPhoneDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_ContactDetailsEmailDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmailDetails] DROP CONSTRAINT [FK_ContactDetailsEmailDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_ProfileContactDetails]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ContactDetails] DROP CONSTRAINT [FK_ProfileContactDetails];
GO
IF OBJECT_ID(N'[dbo].[FK_StateLiceDcocuments]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StateLice] DROP CONSTRAINT [FK_StateLiceDcocuments];
GO
IF OBJECT_ID(N'[dbo].[FK_Profile1Dcocuments]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Dcocuments] DROP CONSTRAINT [FK_Profile1Dcocuments];
GO
IF OBJECT_ID(N'[dbo].[FK_AccountService_Account]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AccountService] DROP CONSTRAINT [FK_AccountService_Account];
GO
IF OBJECT_ID(N'[dbo].[FK_AccountService_Service]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AccountService] DROP CONSTRAINT [FK_AccountService_Service];
GO
IF OBJECT_ID(N'[dbo].[FK_AccountUser_Account]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AccountUser] DROP CONSTRAINT [FK_AccountUser_Account];
GO
IF OBJECT_ID(N'[dbo].[FK_AccountUser_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AccountUser] DROP CONSTRAINT [FK_AccountUser_User];
GO
IF OBJECT_ID(N'[dbo].[FK_UserAccountInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_UserAccountInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonalDetailSpouseName]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SpouseNames] DROP CONSTRAINT [FK_PersonalDetailSpouseName];
GO
IF OBJECT_ID(N'[dbo].[FK_DcocumentsDocumentCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Dcocuments] DROP CONSTRAINT [FK_DcocumentsDocumentCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_ProfileBirthInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Profiles] DROP CONSTRAINT [FK_ProfileBirthInfo];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[PersonalDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PersonalDetails];
GO
IF OBJECT_ID(N'[dbo].[ProviderTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProviderTypes];
GO
IF OBJECT_ID(N'[dbo].[OtherLegalNames]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OtherLegalNames];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Profiles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Profiles];
GO
IF OBJECT_ID(N'[dbo].[Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Roles];
GO
IF OBJECT_ID(N'[dbo].[HomeAddresses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[HomeAddresses];
GO
IF OBJECT_ID(N'[dbo].[Hospitals]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Hospitals];
GO
IF OBJECT_ID(N'[dbo].[HospitalContactInfoes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[HospitalContactInfoes];
GO
IF OBJECT_ID(N'[dbo].[HospitalContactPersons]', 'U') IS NOT NULL
    DROP TABLE [dbo].[HospitalContactPersons];
GO
IF OBJECT_ID(N'[dbo].[HospitalPrivileges]', 'U') IS NOT NULL
    DROP TABLE [dbo].[HospitalPrivileges];
GO
IF OBJECT_ID(N'[dbo].[StaffCategories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StaffCategories];
GO
IF OBJECT_ID(N'[dbo].[AdmittingPrivileges]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AdmittingPrivileges];
GO
IF OBJECT_ID(N'[dbo].[Specialties]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Specialties];
GO
IF OBJECT_ID(N'[dbo].[StateLicenses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StateLicenses];
GO
IF OBJECT_ID(N'[dbo].[StateLicenseTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StateLicenseTypes];
GO
IF OBJECT_ID(N'[dbo].[StateLicenceStatus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StateLicenceStatus];
GO
IF OBJECT_ID(N'[dbo].[FederalDEAInfoes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FederalDEAInfoes];
GO
IF OBJECT_ID(N'[dbo].[DEASchedules]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DEASchedules];
GO
IF OBJECT_ID(N'[dbo].[DEAScheduleTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DEAScheduleTypes];
GO
IF OBJECT_ID(N'[dbo].[DEAScheduleInfoes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DEAScheduleInfoes];
GO
IF OBJECT_ID(N'[dbo].[MedicareMedicaidInfoes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MedicareMedicaidInfoes];
GO
IF OBJECT_ID(N'[dbo].[CDSCInfoes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CDSCInfoes];
GO
IF OBJECT_ID(N'[dbo].[OtherIdentifications]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OtherIdentifications];
GO
IF OBJECT_ID(N'[dbo].[BirthInfoes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BirthInfoes];
GO
IF OBJECT_ID(N'[dbo].[PersonalIdentifications]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PersonalIdentifications];
GO
IF OBJECT_ID(N'[dbo].[VisaDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VisaDetails];
GO
IF OBJECT_ID(N'[dbo].[LanguageInfoes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LanguageInfoes];
GO
IF OBJECT_ID(N'[dbo].[ContactDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ContactDetails];
GO
IF OBJECT_ID(N'[dbo].[PhoneDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PhoneDetails];
GO
IF OBJECT_ID(N'[dbo].[EmailDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmailDetails];
GO
IF OBJECT_ID(N'[dbo].[Dcocuments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Dcocuments];
GO
IF OBJECT_ID(N'[dbo].[StateLice]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StateLice];
GO
IF OBJECT_ID(N'[dbo].[Profile1]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Profile1];
GO
IF OBJECT_ID(N'[dbo].[ProfessionalLiabilties]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProfessionalLiabilties];
GO
IF OBJECT_ID(N'[dbo].[Accounts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Accounts];
GO
IF OBJECT_ID(N'[dbo].[Services]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Services];
GO
IF OBJECT_ID(N'[dbo].[UserInfoes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserInfoes];
GO
IF OBJECT_ID(N'[dbo].[SpouseNames]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SpouseNames];
GO
IF OBJECT_ID(N'[dbo].[DocumentCategories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DocumentCategories];
GO
IF OBJECT_ID(N'[dbo].[RoleUser]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RoleUser];
GO
IF OBJECT_ID(N'[dbo].[UserProfile]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserProfile];
GO
IF OBJECT_ID(N'[dbo].[PersonalDetailProviderType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PersonalDetailProviderType];
GO
IF OBJECT_ID(N'[dbo].[SpecialtyProviderType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SpecialtyProviderType];
GO
IF OBJECT_ID(N'[dbo].[DEAScheduleDEAScheduleType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DEAScheduleDEAScheduleType];
GO
IF OBJECT_ID(N'[dbo].[FederalDEAInfoDEAScheduleInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FederalDEAInfoDEAScheduleInfo];
GO
IF OBJECT_ID(N'[dbo].[DEAScheduleInfoDEAScheduleType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DEAScheduleInfoDEAScheduleType];
GO
IF OBJECT_ID(N'[dbo].[AccountService]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AccountService];
GO
IF OBJECT_ID(N'[dbo].[AccountUser]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AccountUser];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'PersonalDetails'
CREATE TABLE [dbo].[PersonalDetails] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [MiddleName] nvarchar(max)  NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [Suffix] nvarchar(max)  NULL,
    [Gender] nvarchar(max)  NOT NULL,
    [MaritalStatus] nvarchar(max)  NOT NULL,
    [MaidenName] nvarchar(max)  NULL,
    [SpouseName] nvarchar(max)  NULL,
    [ImagePath] nvarchar(max)  NULL,
    [LastUpdatedOn] datetime  NOT NULL
);
GO

-- Creating table 'ProviderTypes'
CREATE TABLE [dbo].[ProviderTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Code] nvarchar(max)  NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [LastUpdatedOn] datetime  NOT NULL
);
GO

-- Creating table 'OtherLegalNames'
CREATE TABLE [dbo].[OtherLegalNames] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [MiddleName] nvarchar(max)  NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [Suffix] nvarchar(max)  NULL,
    [StartDate] datetime  NOT NULL,
    [EndDate] datetime  NOT NULL,
    [CertificatePath] nvarchar(max)  NULL,
    [Status] nvarchar(max)  NOT NULL,
    [LastUpdatedOn] datetime  NOT NULL,
    [ProfileOtherLegalName_OtherLegalName_Id] int  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LastUpdatedOn] datetime  NOT NULL,
    [AccountInfoes_Id] int  NOT NULL
);
GO

-- Creating table 'Profiles'
CREATE TABLE [dbo].[Profiles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [IsCitizenOfUSA] bit  NULL,
    [LastUpdatedOn] datetime  NOT NULL,
    [PersonalDetail_Id] int  NOT NULL,
    [OtherIdentifications_Id] int  NOT NULL,
    [PersonalIdentifications_Id] int  NOT NULL,
    [VisaDetails_Id] int  NOT NULL,
    [BirthInfoes_Id] int  NOT NULL
);
GO

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [LastUpdatedOn] datetime  NOT NULL
);
GO

-- Creating table 'HomeAddresses'
CREATE TABLE [dbo].[HomeAddresses] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Number] nvarchar(max)  NOT NULL,
    [ApartmentOrBuildingNumber] nvarchar(max)  NOT NULL,
    [Street] nvarchar(max)  NOT NULL,
    [Country] nvarchar(max)  NOT NULL,
    [State] nvarchar(max)  NOT NULL,
    [County] nvarchar(max)  NOT NULL,
    [City] nvarchar(max)  NOT NULL,
    [ZipCode] nvarchar(max)  NOT NULL,
    [IsPresentHomeAddress] bit  NOT NULL,
    [LivingFrom] datetime  NOT NULL,
    [LivingTill] datetime  NOT NULL,
    [AddressPreference] nvarchar(max)  NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [LastUpdatedOn] datetime  NOT NULL,
    [PreferenceType] nvarchar(max)  NOT NULL,
    [ProfileAddress_Address_Id] int  NOT NULL
);
GO

-- Creating table 'Hospitals'
CREATE TABLE [dbo].[Hospitals] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [LastUpdatedOn] datetime  NOT NULL
);
GO

-- Creating table 'HospitalContactInfoes'
CREATE TABLE [dbo].[HospitalContactInfoes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LocationName] nvarchar(max)  NOT NULL,
    [Number] nvarchar(max)  NOT NULL,
    [SuiteOrBuilding] nvarchar(max)  NOT NULL,
    [Street] nvarchar(max)  NOT NULL,
    [County] nvarchar(max)  NOT NULL,
    [State] nvarchar(max)  NOT NULL,
    [Country] nvarchar(max)  NOT NULL,
    [ZipCode] nvarchar(max)  NOT NULL,
    [Phone] nvarchar(max)  NOT NULL,
    [Fax] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [LastUpdatedOn] datetime  NOT NULL,
    [HospitalHospitalContactInfo_HospitalContactInfo_Id] int  NOT NULL
);
GO

-- Creating table 'HospitalContactPersons'
CREATE TABLE [dbo].[HospitalContactPersons] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ContactPersonName] nvarchar(max)  NOT NULL,
    [ContactPersonPhone] nvarchar(max)  NOT NULL,
    [ContactPersonFax] nvarchar(max)  NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [LastUpdatedOn] datetime  NOT NULL,
    [HospitalContactInfoHospitalContactPerson_HospitalContactPerson_Id] int  NOT NULL
);
GO

-- Creating table 'HospitalPrivileges'
CREATE TABLE [dbo].[HospitalPrivileges] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [StatusExplanation] nvarchar(max)  NOT NULL,
    [DepartmentName] nvarchar(max)  NOT NULL,
    [DepartmentChief] nvarchar(max)  NOT NULL,
    [StaffChief] nvarchar(max)  NOT NULL,
    [AnnualAdmittingPercentage] float  NOT NULL,
    [IsPrivilegeTemporary] bit  NOT NULL,
    [IsPrimary] bit  NOT NULL,
    [IsPrivilegeFullUnrestricted] bit  NOT NULL,
    [AffiliationStartDate] datetime  NOT NULL,
    [AffiliationEndDate] datetime  NOT NULL,
    [ExplanationForAffiliationTermination] nvarchar(max)  NOT NULL,
    [PrivilegeLetterPath] nvarchar(max)  NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [LastUpdatedOn] datetime  NOT NULL,
    [PreferenceType] nvarchar(max)  NOT NULL,
    [Hospital_Id] int  NOT NULL,
    [HospitalContactInfo_Id] int  NOT NULL,
    [HospitalContactPerson_Id] int  NOT NULL,
    [StaffCategory_Id] int  NOT NULL,
    [AdmittingPrivilege_Id] int  NOT NULL,
    [Specialty_Id] int  NOT NULL,
    [ProfileHospitalPrivilege_HospitalPrivilege_Id] int  NOT NULL
);
GO

-- Creating table 'StaffCategories'
CREATE TABLE [dbo].[StaffCategories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [LastUpdatedOn] datetime  NOT NULL
);
GO

-- Creating table 'AdmittingPrivileges'
CREATE TABLE [dbo].[AdmittingPrivileges] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [LastUpdatedOn] datetime  NOT NULL
);
GO

-- Creating table 'Specialties'
CREATE TABLE [dbo].[Specialties] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [LastUpdatedOn] datetime  NOT NULL
);
GO

-- Creating table 'StateLicenses'
CREATE TABLE [dbo].[StateLicenses] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Number] nvarchar(max)  NOT NULL,
    [IssueState] nvarchar(max)  NOT NULL,
    [PracticeState] nvarchar(max)  NOT NULL,
    [OriginalIssueDate] datetime  NOT NULL,
    [ExpiryDate] datetime  NOT NULL,
    [CurrentIssueDate] datetime  NOT NULL,
    [IsInGoodStanding] bit  NOT NULL,
    [IsRelinquished] bit  NOT NULL,
    [Date] datetime  NOT NULL,
    [RelinquishedDate] datetime  NOT NULL,
    [CertificatePath] nvarchar(max)  NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [LastUpdatedOn] datetime  NOT NULL,
    [StateLicenseType_Id] int  NOT NULL,
    [StateLicenceStatus_Id] int  NOT NULL,
    [ProfileStateLicense_StateLicense_Id] int  NOT NULL
);
GO

-- Creating table 'StateLicenseTypes'
CREATE TABLE [dbo].[StateLicenseTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [LastUpdatedOn] datetime  NOT NULL
);
GO

-- Creating table 'StateLicenceStatus'
CREATE TABLE [dbo].[StateLicenceStatus] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [LastUpdatedOn] datetime  NOT NULL
);
GO

-- Creating table 'FederalDEAInfoes'
CREATE TABLE [dbo].[FederalDEAInfoes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Number] nvarchar(max)  NOT NULL,
    [RegistrationState] nvarchar(max)  NOT NULL,
    [IssueDate] datetime  NOT NULL,
    [ExpiryDate] datetime  NOT NULL,
    [IsInGoodStanding] bit  NOT NULL,
    [LimitedOrRestricted] nvarchar(max)  NOT NULL,
    [RestrictionExplanation] nvarchar(max)  NOT NULL,
    [HasSCRCertificate] bit  NOT NULL,
    [CertificatePath] nvarchar(max)  NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [LastUpdatedOn] datetime  NOT NULL,
    [ProfileFederalDEAInfo_FederalDEAInfo_Id] int  NOT NULL
);
GO

-- Creating table 'DEASchedules'
CREATE TABLE [dbo].[DEASchedules] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [LastUpdatedOn] datetime  NOT NULL
);
GO

-- Creating table 'DEAScheduleTypes'
CREATE TABLE [dbo].[DEAScheduleTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [LastUpdatedOn] datetime  NOT NULL
);
GO

-- Creating table 'DEAScheduleInfoes'
CREATE TABLE [dbo].[DEAScheduleInfoes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LastUpdatedOn] datetime  NOT NULL,
    [DEAScheduleInfoDEASchedule_DEAScheduleInfo_Id] int  NOT NULL
);
GO

-- Creating table 'MedicareMedicaidInfoes'
CREATE TABLE [dbo].[MedicareMedicaidInfoes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [IsApproved] bit  NOT NULL,
    [Number] nvarchar(max)  NOT NULL,
    [State] nvarchar(max)  NOT NULL,
    [IssueDate] datetime  NOT NULL,
    [ExpiryDate] datetime  NOT NULL,
    [IsSanctionImposed] bit  NOT NULL,
    [CertificatePath] nvarchar(max)  NOT NULL,
    [MedicareOrMedicaidType] nvarchar(max)  NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [LastUpdatedOn] datetime  NOT NULL,
    [ProfileMedicareMedicaidInfo_MedicareMedicaidInfo_Id] int  NOT NULL
);
GO

-- Creating table 'CDSCInfoes'
CREATE TABLE [dbo].[CDSCInfoes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Number] nvarchar(max)  NOT NULL,
    [State] nvarchar(max)  NOT NULL,
    [IssueDate] datetime  NOT NULL,
    [ExpiryDate] datetime  NOT NULL,
    [CertificatePath] nvarchar(max)  NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [LastUpdatedOn] datetime  NOT NULL,
    [ProfileCDSCInfo_CDSCInfo_Id] int  NOT NULL
);
GO

-- Creating table 'OtherIdentifications'
CREATE TABLE [dbo].[OtherIdentifications] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [NPINumber] nvarchar(max)  NOT NULL,
    [CAQHNumber] nvarchar(max)  NULL,
    [UPINNumber] nvarchar(max)  NULL,
    [USMLENumber] nvarchar(max)  NULL,
    [LastUpdatedOn] datetime  NOT NULL
);
GO

-- Creating table 'BirthInfoes'
CREATE TABLE [dbo].[BirthInfoes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DateOfBirth] datetime  NOT NULL,
    [BirthCountry] nvarchar(max)  NOT NULL,
    [BirthCity] nvarchar(max)  NOT NULL,
    [BirthState] nvarchar(max)  NOT NULL,
    [BirthCounty] nvarchar(max)  NULL,
    [CertificatePath] nvarchar(max)  NULL
);
GO

-- Creating table 'PersonalIdentifications'
CREATE TABLE [dbo].[PersonalIdentifications] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SocialSecurityNumber] nvarchar(max)  NOT NULL,
    [SSNCertificatePath] nvarchar(max)  NULL,
    [DriverLicenseNumber] nvarchar(max)  NOT NULL,
    [DLCertificatePath] nvarchar(max)  NULL,
    [LastUpdatedOn] datetime  NOT NULL
);
GO

-- Creating table 'VisaDetails'
CREATE TABLE [dbo].[VisaDetails] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [VisaNumber] nvarchar(max)  NOT NULL,
    [VisaType] nvarchar(max)  NOT NULL,
    [VisaStatus] nvarchar(max)  NOT NULL,
    [VisaSponsor] nvarchar(max)  NOT NULL,
    [ExpiryDate] datetime  NOT NULL,
    [CertificatePath] nvarchar(max)  NOT NULL,
    [GreenCardNumber] nvarchar(max)  NULL,
    [NationalIdNumber] nvarchar(max)  NULL,
    [IssuedCountry] nvarchar(max)  NOT NULL,
    [IsAuthorizedToWorkInUs] bit  NOT NULL,
    [GreenCardCertificatePath] nvarchar(max)  NULL,
    [NationalIdCertificatePath] nvarchar(max)  NULL,
    [LastUpdatedOn] datetime  NOT NULL
);
GO

-- Creating table 'LanguageInfoes'
CREATE TABLE [dbo].[LanguageInfoes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CanSpeak] bit  NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [CanWrite] bit  NOT NULL,
    [CanSpeakSign] bit  NOT NULL,
    [ProficiencyIndex] smallint  NOT NULL,
    [LastUpdatedOn] datetime  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [ProfileLanguageInfo_LanguageInfo_Id] int  NOT NULL
);
GO

-- Creating table 'ContactDetails'
CREATE TABLE [dbo].[ContactDetails] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PreferredWrittenContact] nvarchar(max)  NOT NULL,
    [PreferredContact] nvarchar(max)  NOT NULL,
    [LastUpdatedOn] datetime  NOT NULL,
    [ProfileContactDetails_ContactDetails_Id] int  NOT NULL
);
GO

-- Creating table 'PhoneDetails'
CREATE TABLE [dbo].[PhoneDetails] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Number] nvarchar(max)  NOT NULL,
    [CountryCode] nvarchar(max)  NOT NULL,
    [Type] nvarchar(max)  NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [IsPrimary] bit  NOT NULL,
    [LastUpdatedOn] datetime  NOT NULL,
    [ContactDetailsPhoneDetail_PhoneDetail_Id] int  NOT NULL
);
GO

-- Creating table 'EmailDetails'
CREATE TABLE [dbo].[EmailDetails] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [EmailId] nvarchar(max)  NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [LastUpdatedOn] datetime  NOT NULL,
    [ContactDetailsEmailDetail_EmailDetail_Id] int  NOT NULL
);
GO

-- Creating table 'Dcocuments'
CREATE TABLE [dbo].[Dcocuments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Path] nvarchar(max)  NOT NULL,
    [Profile1Dcocuments_Dcocuments_Id] int  NOT NULL,
    [DocumentCategories_Id] int  NOT NULL
);
GO

-- Creating table 'StateLice'
CREATE TABLE [dbo].[StateLice] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Dcocuments_Id] int  NOT NULL
);
GO

-- Creating table 'Profile1'
CREATE TABLE [dbo].[Profile1] (
    [Id] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'ProfessionalLiabilties'
CREATE TABLE [dbo].[ProfessionalLiabilties] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LastUpdatedOn] datetime  NOT NULL
);
GO

-- Creating table 'Accounts'
CREATE TABLE [dbo].[Accounts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LastUpdatedOn] datetime  NOT NULL
);
GO

-- Creating table 'Services'
CREATE TABLE [dbo].[Services] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LastUpdatedOn] datetime  NOT NULL
);
GO

-- Creating table 'UserInfoes'
CREATE TABLE [dbo].[UserInfoes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastUpdatedOn] datetime  NOT NULL
);
GO

-- Creating table 'SpouseNames'
CREATE TABLE [dbo].[SpouseNames] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [LastUpdatedOn] datetime  NOT NULL,
    [PersonalDetailSpouseName_SpouseName_Id] int  NOT NULL
);
GO

-- Creating table 'DocumentCategories'
CREATE TABLE [dbo].[DocumentCategories] (
    [Id] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'RoleUser'
CREATE TABLE [dbo].[RoleUser] (
    [Roles_Id] int  NOT NULL,
    [Users_Id] int  NOT NULL
);
GO

-- Creating table 'UserProfile'
CREATE TABLE [dbo].[UserProfile] (
    [UserProfile_Profile_Id] int  NOT NULL,
    [Profile_Id] int  NOT NULL
);
GO

-- Creating table 'PersonalDetailProviderType'
CREATE TABLE [dbo].[PersonalDetailProviderType] (
    [PersonalDetailProviderType_ProviderType_Id] int  NOT NULL,
    [ProviderTypes_Id] int  NOT NULL
);
GO

-- Creating table 'SpecialtyProviderType'
CREATE TABLE [dbo].[SpecialtyProviderType] (
    [SpecialtyProviderType_ProviderType_Id] int  NOT NULL,
    [ProviderTypes_Id] int  NOT NULL
);
GO

-- Creating table 'DEAScheduleDEAScheduleType'
CREATE TABLE [dbo].[DEAScheduleDEAScheduleType] (
    [DEAScheduleDEAScheduleType_DEAScheduleType_Id] int  NOT NULL,
    [DEAScheduleTypes_Id] int  NOT NULL
);
GO

-- Creating table 'FederalDEAInfoDEAScheduleInfo'
CREATE TABLE [dbo].[FederalDEAInfoDEAScheduleInfo] (
    [FederalDEAInfo_Id] int  NOT NULL,
    [DEAScheduleInfoes_Id] int  NOT NULL
);
GO

-- Creating table 'DEAScheduleInfoDEAScheduleType'
CREATE TABLE [dbo].[DEAScheduleInfoDEAScheduleType] (
    [DEAScheduleInfoDEAScheduleType_DEAScheduleType_Id] int  NOT NULL,
    [DEAScheduleTypes_Id] int  NOT NULL
);
GO

-- Creating table 'AccountService'
CREATE TABLE [dbo].[AccountService] (
    [Account_Id] int  NOT NULL,
    [Services_Id] int  NOT NULL
);
GO

-- Creating table 'AccountUser'
CREATE TABLE [dbo].[AccountUser] (
    [Account_Id] int  NOT NULL,
    [Users_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'PersonalDetails'
ALTER TABLE [dbo].[PersonalDetails]
ADD CONSTRAINT [PK_PersonalDetails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProviderTypes'
ALTER TABLE [dbo].[ProviderTypes]
ADD CONSTRAINT [PK_ProviderTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OtherLegalNames'
ALTER TABLE [dbo].[OtherLegalNames]
ADD CONSTRAINT [PK_OtherLegalNames]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Profiles'
ALTER TABLE [dbo].[Profiles]
ADD CONSTRAINT [PK_Profiles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'HomeAddresses'
ALTER TABLE [dbo].[HomeAddresses]
ADD CONSTRAINT [PK_HomeAddresses]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Hospitals'
ALTER TABLE [dbo].[Hospitals]
ADD CONSTRAINT [PK_Hospitals]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'HospitalContactInfoes'
ALTER TABLE [dbo].[HospitalContactInfoes]
ADD CONSTRAINT [PK_HospitalContactInfoes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'HospitalContactPersons'
ALTER TABLE [dbo].[HospitalContactPersons]
ADD CONSTRAINT [PK_HospitalContactPersons]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'HospitalPrivileges'
ALTER TABLE [dbo].[HospitalPrivileges]
ADD CONSTRAINT [PK_HospitalPrivileges]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StaffCategories'
ALTER TABLE [dbo].[StaffCategories]
ADD CONSTRAINT [PK_StaffCategories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AdmittingPrivileges'
ALTER TABLE [dbo].[AdmittingPrivileges]
ADD CONSTRAINT [PK_AdmittingPrivileges]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Specialties'
ALTER TABLE [dbo].[Specialties]
ADD CONSTRAINT [PK_Specialties]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StateLicenses'
ALTER TABLE [dbo].[StateLicenses]
ADD CONSTRAINT [PK_StateLicenses]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StateLicenseTypes'
ALTER TABLE [dbo].[StateLicenseTypes]
ADD CONSTRAINT [PK_StateLicenseTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StateLicenceStatus'
ALTER TABLE [dbo].[StateLicenceStatus]
ADD CONSTRAINT [PK_StateLicenceStatus]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'FederalDEAInfoes'
ALTER TABLE [dbo].[FederalDEAInfoes]
ADD CONSTRAINT [PK_FederalDEAInfoes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DEASchedules'
ALTER TABLE [dbo].[DEASchedules]
ADD CONSTRAINT [PK_DEASchedules]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DEAScheduleTypes'
ALTER TABLE [dbo].[DEAScheduleTypes]
ADD CONSTRAINT [PK_DEAScheduleTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DEAScheduleInfoes'
ALTER TABLE [dbo].[DEAScheduleInfoes]
ADD CONSTRAINT [PK_DEAScheduleInfoes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MedicareMedicaidInfoes'
ALTER TABLE [dbo].[MedicareMedicaidInfoes]
ADD CONSTRAINT [PK_MedicareMedicaidInfoes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CDSCInfoes'
ALTER TABLE [dbo].[CDSCInfoes]
ADD CONSTRAINT [PK_CDSCInfoes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OtherIdentifications'
ALTER TABLE [dbo].[OtherIdentifications]
ADD CONSTRAINT [PK_OtherIdentifications]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BirthInfoes'
ALTER TABLE [dbo].[BirthInfoes]
ADD CONSTRAINT [PK_BirthInfoes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PersonalIdentifications'
ALTER TABLE [dbo].[PersonalIdentifications]
ADD CONSTRAINT [PK_PersonalIdentifications]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'VisaDetails'
ALTER TABLE [dbo].[VisaDetails]
ADD CONSTRAINT [PK_VisaDetails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LanguageInfoes'
ALTER TABLE [dbo].[LanguageInfoes]
ADD CONSTRAINT [PK_LanguageInfoes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ContactDetails'
ALTER TABLE [dbo].[ContactDetails]
ADD CONSTRAINT [PK_ContactDetails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PhoneDetails'
ALTER TABLE [dbo].[PhoneDetails]
ADD CONSTRAINT [PK_PhoneDetails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EmailDetails'
ALTER TABLE [dbo].[EmailDetails]
ADD CONSTRAINT [PK_EmailDetails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Dcocuments'
ALTER TABLE [dbo].[Dcocuments]
ADD CONSTRAINT [PK_Dcocuments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StateLice'
ALTER TABLE [dbo].[StateLice]
ADD CONSTRAINT [PK_StateLice]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Profile1'
ALTER TABLE [dbo].[Profile1]
ADD CONSTRAINT [PK_Profile1]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProfessionalLiabilties'
ALTER TABLE [dbo].[ProfessionalLiabilties]
ADD CONSTRAINT [PK_ProfessionalLiabilties]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Accounts'
ALTER TABLE [dbo].[Accounts]
ADD CONSTRAINT [PK_Accounts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Services'
ALTER TABLE [dbo].[Services]
ADD CONSTRAINT [PK_Services]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserInfoes'
ALTER TABLE [dbo].[UserInfoes]
ADD CONSTRAINT [PK_UserInfoes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SpouseNames'
ALTER TABLE [dbo].[SpouseNames]
ADD CONSTRAINT [PK_SpouseNames]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DocumentCategories'
ALTER TABLE [dbo].[DocumentCategories]
ADD CONSTRAINT [PK_DocumentCategories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Roles_Id], [Users_Id] in table 'RoleUser'
ALTER TABLE [dbo].[RoleUser]
ADD CONSTRAINT [PK_RoleUser]
    PRIMARY KEY CLUSTERED ([Roles_Id], [Users_Id] ASC);
GO

-- Creating primary key on [UserProfile_Profile_Id], [Profile_Id] in table 'UserProfile'
ALTER TABLE [dbo].[UserProfile]
ADD CONSTRAINT [PK_UserProfile]
    PRIMARY KEY CLUSTERED ([UserProfile_Profile_Id], [Profile_Id] ASC);
GO

-- Creating primary key on [PersonalDetailProviderType_ProviderType_Id], [ProviderTypes_Id] in table 'PersonalDetailProviderType'
ALTER TABLE [dbo].[PersonalDetailProviderType]
ADD CONSTRAINT [PK_PersonalDetailProviderType]
    PRIMARY KEY CLUSTERED ([PersonalDetailProviderType_ProviderType_Id], [ProviderTypes_Id] ASC);
GO

-- Creating primary key on [SpecialtyProviderType_ProviderType_Id], [ProviderTypes_Id] in table 'SpecialtyProviderType'
ALTER TABLE [dbo].[SpecialtyProviderType]
ADD CONSTRAINT [PK_SpecialtyProviderType]
    PRIMARY KEY CLUSTERED ([SpecialtyProviderType_ProviderType_Id], [ProviderTypes_Id] ASC);
GO

-- Creating primary key on [DEAScheduleDEAScheduleType_DEAScheduleType_Id], [DEAScheduleTypes_Id] in table 'DEAScheduleDEAScheduleType'
ALTER TABLE [dbo].[DEAScheduleDEAScheduleType]
ADD CONSTRAINT [PK_DEAScheduleDEAScheduleType]
    PRIMARY KEY CLUSTERED ([DEAScheduleDEAScheduleType_DEAScheduleType_Id], [DEAScheduleTypes_Id] ASC);
GO

-- Creating primary key on [FederalDEAInfo_Id], [DEAScheduleInfoes_Id] in table 'FederalDEAInfoDEAScheduleInfo'
ALTER TABLE [dbo].[FederalDEAInfoDEAScheduleInfo]
ADD CONSTRAINT [PK_FederalDEAInfoDEAScheduleInfo]
    PRIMARY KEY CLUSTERED ([FederalDEAInfo_Id], [DEAScheduleInfoes_Id] ASC);
GO

-- Creating primary key on [DEAScheduleInfoDEAScheduleType_DEAScheduleType_Id], [DEAScheduleTypes_Id] in table 'DEAScheduleInfoDEAScheduleType'
ALTER TABLE [dbo].[DEAScheduleInfoDEAScheduleType]
ADD CONSTRAINT [PK_DEAScheduleInfoDEAScheduleType]
    PRIMARY KEY CLUSTERED ([DEAScheduleInfoDEAScheduleType_DEAScheduleType_Id], [DEAScheduleTypes_Id] ASC);
GO

-- Creating primary key on [Account_Id], [Services_Id] in table 'AccountService'
ALTER TABLE [dbo].[AccountService]
ADD CONSTRAINT [PK_AccountService]
    PRIMARY KEY CLUSTERED ([Account_Id], [Services_Id] ASC);
GO

-- Creating primary key on [Account_Id], [Users_Id] in table 'AccountUser'
ALTER TABLE [dbo].[AccountUser]
ADD CONSTRAINT [PK_AccountUser]
    PRIMARY KEY CLUSTERED ([Account_Id], [Users_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Roles_Id] in table 'RoleUser'
ALTER TABLE [dbo].[RoleUser]
ADD CONSTRAINT [FK_RoleUser_Role]
    FOREIGN KEY ([Roles_Id])
    REFERENCES [dbo].[Roles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Users_Id] in table 'RoleUser'
ALTER TABLE [dbo].[RoleUser]
ADD CONSTRAINT [FK_RoleUser_User]
    FOREIGN KEY ([Users_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoleUser_User'
CREATE INDEX [IX_FK_RoleUser_User]
ON [dbo].[RoleUser]
    ([Users_Id]);
GO

-- Creating foreign key on [UserProfile_Profile_Id] in table 'UserProfile'
ALTER TABLE [dbo].[UserProfile]
ADD CONSTRAINT [FK_UserProfile_User]
    FOREIGN KEY ([UserProfile_Profile_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Profile_Id] in table 'UserProfile'
ALTER TABLE [dbo].[UserProfile]
ADD CONSTRAINT [FK_UserProfile_Profile]
    FOREIGN KEY ([Profile_Id])
    REFERENCES [dbo].[Profiles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserProfile_Profile'
CREATE INDEX [IX_FK_UserProfile_Profile]
ON [dbo].[UserProfile]
    ([Profile_Id]);
GO

-- Creating foreign key on [PersonalDetailProviderType_ProviderType_Id] in table 'PersonalDetailProviderType'
ALTER TABLE [dbo].[PersonalDetailProviderType]
ADD CONSTRAINT [FK_PersonalDetailProviderType_PersonalDetail]
    FOREIGN KEY ([PersonalDetailProviderType_ProviderType_Id])
    REFERENCES [dbo].[PersonalDetails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ProviderTypes_Id] in table 'PersonalDetailProviderType'
ALTER TABLE [dbo].[PersonalDetailProviderType]
ADD CONSTRAINT [FK_PersonalDetailProviderType_ProviderType]
    FOREIGN KEY ([ProviderTypes_Id])
    REFERENCES [dbo].[ProviderTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonalDetailProviderType_ProviderType'
CREATE INDEX [IX_FK_PersonalDetailProviderType_ProviderType]
ON [dbo].[PersonalDetailProviderType]
    ([ProviderTypes_Id]);
GO

-- Creating foreign key on [ProfileOtherLegalName_OtherLegalName_Id] in table 'OtherLegalNames'
ALTER TABLE [dbo].[OtherLegalNames]
ADD CONSTRAINT [FK_ProfileOtherLegalName]
    FOREIGN KEY ([ProfileOtherLegalName_OtherLegalName_Id])
    REFERENCES [dbo].[Profiles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProfileOtherLegalName'
CREATE INDEX [IX_FK_ProfileOtherLegalName]
ON [dbo].[OtherLegalNames]
    ([ProfileOtherLegalName_OtherLegalName_Id]);
GO

-- Creating foreign key on [PersonalDetail_Id] in table 'Profiles'
ALTER TABLE [dbo].[Profiles]
ADD CONSTRAINT [FK_ProfilePersonalDetail]
    FOREIGN KEY ([PersonalDetail_Id])
    REFERENCES [dbo].[PersonalDetails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProfilePersonalDetail'
CREATE INDEX [IX_FK_ProfilePersonalDetail]
ON [dbo].[Profiles]
    ([PersonalDetail_Id]);
GO

-- Creating foreign key on [HospitalContactInfoHospitalContactPerson_HospitalContactPerson_Id] in table 'HospitalContactPersons'
ALTER TABLE [dbo].[HospitalContactPersons]
ADD CONSTRAINT [FK_HospitalContactInfoHospitalContactPerson]
    FOREIGN KEY ([HospitalContactInfoHospitalContactPerson_HospitalContactPerson_Id])
    REFERENCES [dbo].[HospitalContactInfoes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_HospitalContactInfoHospitalContactPerson'
CREATE INDEX [IX_FK_HospitalContactInfoHospitalContactPerson]
ON [dbo].[HospitalContactPersons]
    ([HospitalContactInfoHospitalContactPerson_HospitalContactPerson_Id]);
GO

-- Creating foreign key on [HospitalHospitalContactInfo_HospitalContactInfo_Id] in table 'HospitalContactInfoes'
ALTER TABLE [dbo].[HospitalContactInfoes]
ADD CONSTRAINT [FK_HospitalHospitalContactInfo]
    FOREIGN KEY ([HospitalHospitalContactInfo_HospitalContactInfo_Id])
    REFERENCES [dbo].[Hospitals]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_HospitalHospitalContactInfo'
CREATE INDEX [IX_FK_HospitalHospitalContactInfo]
ON [dbo].[HospitalContactInfoes]
    ([HospitalHospitalContactInfo_HospitalContactInfo_Id]);
GO

-- Creating foreign key on [Hospital_Id] in table 'HospitalPrivileges'
ALTER TABLE [dbo].[HospitalPrivileges]
ADD CONSTRAINT [FK_HospitalPrivilegeHospital]
    FOREIGN KEY ([Hospital_Id])
    REFERENCES [dbo].[Hospitals]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_HospitalPrivilegeHospital'
CREATE INDEX [IX_FK_HospitalPrivilegeHospital]
ON [dbo].[HospitalPrivileges]
    ([Hospital_Id]);
GO

-- Creating foreign key on [HospitalContactInfo_Id] in table 'HospitalPrivileges'
ALTER TABLE [dbo].[HospitalPrivileges]
ADD CONSTRAINT [FK_HospitalPrivilegeHospitalContactInfo]
    FOREIGN KEY ([HospitalContactInfo_Id])
    REFERENCES [dbo].[HospitalContactInfoes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_HospitalPrivilegeHospitalContactInfo'
CREATE INDEX [IX_FK_HospitalPrivilegeHospitalContactInfo]
ON [dbo].[HospitalPrivileges]
    ([HospitalContactInfo_Id]);
GO

-- Creating foreign key on [HospitalContactPerson_Id] in table 'HospitalPrivileges'
ALTER TABLE [dbo].[HospitalPrivileges]
ADD CONSTRAINT [FK_HospitalPrivilegeHospitalContactPerson]
    FOREIGN KEY ([HospitalContactPerson_Id])
    REFERENCES [dbo].[HospitalContactPersons]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_HospitalPrivilegeHospitalContactPerson'
CREATE INDEX [IX_FK_HospitalPrivilegeHospitalContactPerson]
ON [dbo].[HospitalPrivileges]
    ([HospitalContactPerson_Id]);
GO

-- Creating foreign key on [StaffCategory_Id] in table 'HospitalPrivileges'
ALTER TABLE [dbo].[HospitalPrivileges]
ADD CONSTRAINT [FK_HospitalPrivilegeStaffCategory]
    FOREIGN KEY ([StaffCategory_Id])
    REFERENCES [dbo].[StaffCategories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_HospitalPrivilegeStaffCategory'
CREATE INDEX [IX_FK_HospitalPrivilegeStaffCategory]
ON [dbo].[HospitalPrivileges]
    ([StaffCategory_Id]);
GO

-- Creating foreign key on [AdmittingPrivilege_Id] in table 'HospitalPrivileges'
ALTER TABLE [dbo].[HospitalPrivileges]
ADD CONSTRAINT [FK_HospitalPrivilegeAdmittingPrivilege]
    FOREIGN KEY ([AdmittingPrivilege_Id])
    REFERENCES [dbo].[AdmittingPrivileges]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_HospitalPrivilegeAdmittingPrivilege'
CREATE INDEX [IX_FK_HospitalPrivilegeAdmittingPrivilege]
ON [dbo].[HospitalPrivileges]
    ([AdmittingPrivilege_Id]);
GO

-- Creating foreign key on [SpecialtyProviderType_ProviderType_Id] in table 'SpecialtyProviderType'
ALTER TABLE [dbo].[SpecialtyProviderType]
ADD CONSTRAINT [FK_SpecialtyProviderType_Specialty]
    FOREIGN KEY ([SpecialtyProviderType_ProviderType_Id])
    REFERENCES [dbo].[Specialties]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ProviderTypes_Id] in table 'SpecialtyProviderType'
ALTER TABLE [dbo].[SpecialtyProviderType]
ADD CONSTRAINT [FK_SpecialtyProviderType_ProviderType]
    FOREIGN KEY ([ProviderTypes_Id])
    REFERENCES [dbo].[ProviderTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SpecialtyProviderType_ProviderType'
CREATE INDEX [IX_FK_SpecialtyProviderType_ProviderType]
ON [dbo].[SpecialtyProviderType]
    ([ProviderTypes_Id]);
GO

-- Creating foreign key on [Specialty_Id] in table 'HospitalPrivileges'
ALTER TABLE [dbo].[HospitalPrivileges]
ADD CONSTRAINT [FK_HospitalPrivilegeSpecialty]
    FOREIGN KEY ([Specialty_Id])
    REFERENCES [dbo].[Specialties]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_HospitalPrivilegeSpecialty'
CREATE INDEX [IX_FK_HospitalPrivilegeSpecialty]
ON [dbo].[HospitalPrivileges]
    ([Specialty_Id]);
GO

-- Creating foreign key on [ProfileHospitalPrivilege_HospitalPrivilege_Id] in table 'HospitalPrivileges'
ALTER TABLE [dbo].[HospitalPrivileges]
ADD CONSTRAINT [FK_ProfileHospitalPrivilege]
    FOREIGN KEY ([ProfileHospitalPrivilege_HospitalPrivilege_Id])
    REFERENCES [dbo].[Profiles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProfileHospitalPrivilege'
CREATE INDEX [IX_FK_ProfileHospitalPrivilege]
ON [dbo].[HospitalPrivileges]
    ([ProfileHospitalPrivilege_HospitalPrivilege_Id]);
GO

-- Creating foreign key on [StateLicenseType_Id] in table 'StateLicenses'
ALTER TABLE [dbo].[StateLicenses]
ADD CONSTRAINT [FK_StateLicenseStateLicenseType]
    FOREIGN KEY ([StateLicenseType_Id])
    REFERENCES [dbo].[StateLicenseTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StateLicenseStateLicenseType'
CREATE INDEX [IX_FK_StateLicenseStateLicenseType]
ON [dbo].[StateLicenses]
    ([StateLicenseType_Id]);
GO

-- Creating foreign key on [StateLicenceStatus_Id] in table 'StateLicenses'
ALTER TABLE [dbo].[StateLicenses]
ADD CONSTRAINT [FK_StateLicenseStateLicenceStatus]
    FOREIGN KEY ([StateLicenceStatus_Id])
    REFERENCES [dbo].[StateLicenceStatus]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StateLicenseStateLicenceStatus'
CREATE INDEX [IX_FK_StateLicenseStateLicenceStatus]
ON [dbo].[StateLicenses]
    ([StateLicenceStatus_Id]);
GO

-- Creating foreign key on [ProfileStateLicense_StateLicense_Id] in table 'StateLicenses'
ALTER TABLE [dbo].[StateLicenses]
ADD CONSTRAINT [FK_ProfileStateLicense]
    FOREIGN KEY ([ProfileStateLicense_StateLicense_Id])
    REFERENCES [dbo].[Profiles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProfileStateLicense'
CREATE INDEX [IX_FK_ProfileStateLicense]
ON [dbo].[StateLicenses]
    ([ProfileStateLicense_StateLicense_Id]);
GO

-- Creating foreign key on [DEAScheduleDEAScheduleType_DEAScheduleType_Id] in table 'DEAScheduleDEAScheduleType'
ALTER TABLE [dbo].[DEAScheduleDEAScheduleType]
ADD CONSTRAINT [FK_DEAScheduleDEAScheduleType_DEASchedule]
    FOREIGN KEY ([DEAScheduleDEAScheduleType_DEAScheduleType_Id])
    REFERENCES [dbo].[DEASchedules]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [DEAScheduleTypes_Id] in table 'DEAScheduleDEAScheduleType'
ALTER TABLE [dbo].[DEAScheduleDEAScheduleType]
ADD CONSTRAINT [FK_DEAScheduleDEAScheduleType_DEAScheduleType]
    FOREIGN KEY ([DEAScheduleTypes_Id])
    REFERENCES [dbo].[DEAScheduleTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DEAScheduleDEAScheduleType_DEAScheduleType'
CREATE INDEX [IX_FK_DEAScheduleDEAScheduleType_DEAScheduleType]
ON [dbo].[DEAScheduleDEAScheduleType]
    ([DEAScheduleTypes_Id]);
GO

-- Creating foreign key on [DEAScheduleInfoDEASchedule_DEAScheduleInfo_Id] in table 'DEAScheduleInfoes'
ALTER TABLE [dbo].[DEAScheduleInfoes]
ADD CONSTRAINT [FK_DEAScheduleInfoDEASchedule]
    FOREIGN KEY ([DEAScheduleInfoDEASchedule_DEAScheduleInfo_Id])
    REFERENCES [dbo].[DEASchedules]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DEAScheduleInfoDEASchedule'
CREATE INDEX [IX_FK_DEAScheduleInfoDEASchedule]
ON [dbo].[DEAScheduleInfoes]
    ([DEAScheduleInfoDEASchedule_DEAScheduleInfo_Id]);
GO

-- Creating foreign key on [FederalDEAInfo_Id] in table 'FederalDEAInfoDEAScheduleInfo'
ALTER TABLE [dbo].[FederalDEAInfoDEAScheduleInfo]
ADD CONSTRAINT [FK_FederalDEAInfoDEAScheduleInfo_FederalDEAInfo]
    FOREIGN KEY ([FederalDEAInfo_Id])
    REFERENCES [dbo].[FederalDEAInfoes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [DEAScheduleInfoes_Id] in table 'FederalDEAInfoDEAScheduleInfo'
ALTER TABLE [dbo].[FederalDEAInfoDEAScheduleInfo]
ADD CONSTRAINT [FK_FederalDEAInfoDEAScheduleInfo_DEAScheduleInfo]
    FOREIGN KEY ([DEAScheduleInfoes_Id])
    REFERENCES [dbo].[DEAScheduleInfoes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FederalDEAInfoDEAScheduleInfo_DEAScheduleInfo'
CREATE INDEX [IX_FK_FederalDEAInfoDEAScheduleInfo_DEAScheduleInfo]
ON [dbo].[FederalDEAInfoDEAScheduleInfo]
    ([DEAScheduleInfoes_Id]);
GO

-- Creating foreign key on [DEAScheduleInfoDEAScheduleType_DEAScheduleType_Id] in table 'DEAScheduleInfoDEAScheduleType'
ALTER TABLE [dbo].[DEAScheduleInfoDEAScheduleType]
ADD CONSTRAINT [FK_DEAScheduleInfoDEAScheduleType_DEAScheduleInfo]
    FOREIGN KEY ([DEAScheduleInfoDEAScheduleType_DEAScheduleType_Id])
    REFERENCES [dbo].[DEAScheduleInfoes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [DEAScheduleTypes_Id] in table 'DEAScheduleInfoDEAScheduleType'
ALTER TABLE [dbo].[DEAScheduleInfoDEAScheduleType]
ADD CONSTRAINT [FK_DEAScheduleInfoDEAScheduleType_DEAScheduleType]
    FOREIGN KEY ([DEAScheduleTypes_Id])
    REFERENCES [dbo].[DEAScheduleTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DEAScheduleInfoDEAScheduleType_DEAScheduleType'
CREATE INDEX [IX_FK_DEAScheduleInfoDEAScheduleType_DEAScheduleType]
ON [dbo].[DEAScheduleInfoDEAScheduleType]
    ([DEAScheduleTypes_Id]);
GO

-- Creating foreign key on [ProfileFederalDEAInfo_FederalDEAInfo_Id] in table 'FederalDEAInfoes'
ALTER TABLE [dbo].[FederalDEAInfoes]
ADD CONSTRAINT [FK_ProfileFederalDEAInfo]
    FOREIGN KEY ([ProfileFederalDEAInfo_FederalDEAInfo_Id])
    REFERENCES [dbo].[Profiles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProfileFederalDEAInfo'
CREATE INDEX [IX_FK_ProfileFederalDEAInfo]
ON [dbo].[FederalDEAInfoes]
    ([ProfileFederalDEAInfo_FederalDEAInfo_Id]);
GO

-- Creating foreign key on [ProfileMedicareMedicaidInfo_MedicareMedicaidInfo_Id] in table 'MedicareMedicaidInfoes'
ALTER TABLE [dbo].[MedicareMedicaidInfoes]
ADD CONSTRAINT [FK_ProfileMedicareMedicaidInfo]
    FOREIGN KEY ([ProfileMedicareMedicaidInfo_MedicareMedicaidInfo_Id])
    REFERENCES [dbo].[Profiles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProfileMedicareMedicaidInfo'
CREATE INDEX [IX_FK_ProfileMedicareMedicaidInfo]
ON [dbo].[MedicareMedicaidInfoes]
    ([ProfileMedicareMedicaidInfo_MedicareMedicaidInfo_Id]);
GO

-- Creating foreign key on [ProfileCDSCInfo_CDSCInfo_Id] in table 'CDSCInfoes'
ALTER TABLE [dbo].[CDSCInfoes]
ADD CONSTRAINT [FK_ProfileCDSCInfo]
    FOREIGN KEY ([ProfileCDSCInfo_CDSCInfo_Id])
    REFERENCES [dbo].[Profiles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProfileCDSCInfo'
CREATE INDEX [IX_FK_ProfileCDSCInfo]
ON [dbo].[CDSCInfoes]
    ([ProfileCDSCInfo_CDSCInfo_Id]);
GO

-- Creating foreign key on [OtherIdentifications_Id] in table 'Profiles'
ALTER TABLE [dbo].[Profiles]
ADD CONSTRAINT [FK_ProfileOtherIdentification]
    FOREIGN KEY ([OtherIdentifications_Id])
    REFERENCES [dbo].[OtherIdentifications]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProfileOtherIdentification'
CREATE INDEX [IX_FK_ProfileOtherIdentification]
ON [dbo].[Profiles]
    ([OtherIdentifications_Id]);
GO

-- Creating foreign key on [ProfileAddress_Address_Id] in table 'HomeAddresses'
ALTER TABLE [dbo].[HomeAddresses]
ADD CONSTRAINT [FK_ProfileAddress]
    FOREIGN KEY ([ProfileAddress_Address_Id])
    REFERENCES [dbo].[Profiles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProfileAddress'
CREATE INDEX [IX_FK_ProfileAddress]
ON [dbo].[HomeAddresses]
    ([ProfileAddress_Address_Id]);
GO

-- Creating foreign key on [PersonalIdentifications_Id] in table 'Profiles'
ALTER TABLE [dbo].[Profiles]
ADD CONSTRAINT [FK_ProfilePersonalIdentification]
    FOREIGN KEY ([PersonalIdentifications_Id])
    REFERENCES [dbo].[PersonalIdentifications]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProfilePersonalIdentification'
CREATE INDEX [IX_FK_ProfilePersonalIdentification]
ON [dbo].[Profiles]
    ([PersonalIdentifications_Id]);
GO

-- Creating foreign key on [VisaDetails_Id] in table 'Profiles'
ALTER TABLE [dbo].[Profiles]
ADD CONSTRAINT [FK_ProfileVisaDetail]
    FOREIGN KEY ([VisaDetails_Id])
    REFERENCES [dbo].[VisaDetails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProfileVisaDetail'
CREATE INDEX [IX_FK_ProfileVisaDetail]
ON [dbo].[Profiles]
    ([VisaDetails_Id]);
GO

-- Creating foreign key on [ProfileLanguageInfo_LanguageInfo_Id] in table 'LanguageInfoes'
ALTER TABLE [dbo].[LanguageInfoes]
ADD CONSTRAINT [FK_ProfileLanguageInfo]
    FOREIGN KEY ([ProfileLanguageInfo_LanguageInfo_Id])
    REFERENCES [dbo].[Profiles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProfileLanguageInfo'
CREATE INDEX [IX_FK_ProfileLanguageInfo]
ON [dbo].[LanguageInfoes]
    ([ProfileLanguageInfo_LanguageInfo_Id]);
GO

-- Creating foreign key on [ContactDetailsPhoneDetail_PhoneDetail_Id] in table 'PhoneDetails'
ALTER TABLE [dbo].[PhoneDetails]
ADD CONSTRAINT [FK_ContactDetailsPhoneDetail]
    FOREIGN KEY ([ContactDetailsPhoneDetail_PhoneDetail_Id])
    REFERENCES [dbo].[ContactDetails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ContactDetailsPhoneDetail'
CREATE INDEX [IX_FK_ContactDetailsPhoneDetail]
ON [dbo].[PhoneDetails]
    ([ContactDetailsPhoneDetail_PhoneDetail_Id]);
GO

-- Creating foreign key on [ContactDetailsEmailDetail_EmailDetail_Id] in table 'EmailDetails'
ALTER TABLE [dbo].[EmailDetails]
ADD CONSTRAINT [FK_ContactDetailsEmailDetail]
    FOREIGN KEY ([ContactDetailsEmailDetail_EmailDetail_Id])
    REFERENCES [dbo].[ContactDetails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ContactDetailsEmailDetail'
CREATE INDEX [IX_FK_ContactDetailsEmailDetail]
ON [dbo].[EmailDetails]
    ([ContactDetailsEmailDetail_EmailDetail_Id]);
GO

-- Creating foreign key on [ProfileContactDetails_ContactDetails_Id] in table 'ContactDetails'
ALTER TABLE [dbo].[ContactDetails]
ADD CONSTRAINT [FK_ProfileContactDetails]
    FOREIGN KEY ([ProfileContactDetails_ContactDetails_Id])
    REFERENCES [dbo].[Profiles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProfileContactDetails'
CREATE INDEX [IX_FK_ProfileContactDetails]
ON [dbo].[ContactDetails]
    ([ProfileContactDetails_ContactDetails_Id]);
GO

-- Creating foreign key on [Dcocuments_Id] in table 'StateLice'
ALTER TABLE [dbo].[StateLice]
ADD CONSTRAINT [FK_StateLiceDcocuments]
    FOREIGN KEY ([Dcocuments_Id])
    REFERENCES [dbo].[Dcocuments]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StateLiceDcocuments'
CREATE INDEX [IX_FK_StateLiceDcocuments]
ON [dbo].[StateLice]
    ([Dcocuments_Id]);
GO

-- Creating foreign key on [Profile1Dcocuments_Dcocuments_Id] in table 'Dcocuments'
ALTER TABLE [dbo].[Dcocuments]
ADD CONSTRAINT [FK_Profile1Dcocuments]
    FOREIGN KEY ([Profile1Dcocuments_Dcocuments_Id])
    REFERENCES [dbo].[Profile1]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Profile1Dcocuments'
CREATE INDEX [IX_FK_Profile1Dcocuments]
ON [dbo].[Dcocuments]
    ([Profile1Dcocuments_Dcocuments_Id]);
GO

-- Creating foreign key on [Account_Id] in table 'AccountService'
ALTER TABLE [dbo].[AccountService]
ADD CONSTRAINT [FK_AccountService_Account]
    FOREIGN KEY ([Account_Id])
    REFERENCES [dbo].[Accounts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Services_Id] in table 'AccountService'
ALTER TABLE [dbo].[AccountService]
ADD CONSTRAINT [FK_AccountService_Service]
    FOREIGN KEY ([Services_Id])
    REFERENCES [dbo].[Services]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AccountService_Service'
CREATE INDEX [IX_FK_AccountService_Service]
ON [dbo].[AccountService]
    ([Services_Id]);
GO

-- Creating foreign key on [Account_Id] in table 'AccountUser'
ALTER TABLE [dbo].[AccountUser]
ADD CONSTRAINT [FK_AccountUser_Account]
    FOREIGN KEY ([Account_Id])
    REFERENCES [dbo].[Accounts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Users_Id] in table 'AccountUser'
ALTER TABLE [dbo].[AccountUser]
ADD CONSTRAINT [FK_AccountUser_User]
    FOREIGN KEY ([Users_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AccountUser_User'
CREATE INDEX [IX_FK_AccountUser_User]
ON [dbo].[AccountUser]
    ([Users_Id]);
GO

-- Creating foreign key on [AccountInfoes_Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_UserAccountInfo]
    FOREIGN KEY ([AccountInfoes_Id])
    REFERENCES [dbo].[UserInfoes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserAccountInfo'
CREATE INDEX [IX_FK_UserAccountInfo]
ON [dbo].[Users]
    ([AccountInfoes_Id]);
GO

-- Creating foreign key on [PersonalDetailSpouseName_SpouseName_Id] in table 'SpouseNames'
ALTER TABLE [dbo].[SpouseNames]
ADD CONSTRAINT [FK_PersonalDetailSpouseName]
    FOREIGN KEY ([PersonalDetailSpouseName_SpouseName_Id])
    REFERENCES [dbo].[PersonalDetails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonalDetailSpouseName'
CREATE INDEX [IX_FK_PersonalDetailSpouseName]
ON [dbo].[SpouseNames]
    ([PersonalDetailSpouseName_SpouseName_Id]);
GO

-- Creating foreign key on [DocumentCategories_Id] in table 'Dcocuments'
ALTER TABLE [dbo].[Dcocuments]
ADD CONSTRAINT [FK_DcocumentsDocumentCategory]
    FOREIGN KEY ([DocumentCategories_Id])
    REFERENCES [dbo].[DocumentCategories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DcocumentsDocumentCategory'
CREATE INDEX [IX_FK_DcocumentsDocumentCategory]
ON [dbo].[Dcocuments]
    ([DocumentCategories_Id]);
GO

-- Creating foreign key on [BirthInfoes_Id] in table 'Profiles'
ALTER TABLE [dbo].[Profiles]
ADD CONSTRAINT [FK_ProfileBirthInfo]
    FOREIGN KEY ([BirthInfoes_Id])
    REFERENCES [dbo].[BirthInfoes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProfileBirthInfo'
CREATE INDEX [IX_FK_ProfileBirthInfo]
ON [dbo].[Profiles]
    ([BirthInfoes_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------