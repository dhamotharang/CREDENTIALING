using AHC.CD.Entities.MasterData.Enums;
using AHC.UtilityService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.ADO.DTO.FormDTO
{
    public class FormData
    {
        public int ProfileID { get; set; }
        public string ProfilePhotoPath { get; set; }
        public int PersonalDetailID { get; set; }
        #region Demographics
        #region Personal Details
        #region Title

        public string Salutation { get; set; }

        
        public SalutationType? SalutationType
        {
            get
            {
                if (String.IsNullOrEmpty(this.Salutation))
                    return null;

                if (this.Salutation.Equals("Not Available"))
                    return null;

                return (SalutationType)Enum.Parse(typeof(SalutationType), this.Salutation);
            }
            set
            {
                this.Salutation = value.ToString();
            }
        }

        #endregion    

        
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

       
        public string LastName { get; set; }

        public string Suffix { get; set; }

        #region Gender

       
        public string Gender { get; set; }

        
        public GenderType? GenderType
        {
            get
            {
                if (String.IsNullOrEmpty(this.Gender))
                    return null;

                if (this.Gender.Equals("Not Available"))
                    return null;

                return (GenderType)Enum.Parse(typeof(GenderType), this.Gender);
            }
            set
            {
                this.Gender = value.ToString();
            }
        }

        #endregion

        public virtual string MaidenName { get; set; }

        #region MaritalStatus

        public string MaritalStatus { get; set; }

       
        public MaritalStatusType? MaritalStatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.MaritalStatus))
                    return null;

                if (this.MaritalStatus.Equals("Not Available"))
                    return null;

                return (MaritalStatusType)Enum.Parse(typeof(MaritalStatusType), this.MaritalStatus);
            }
            set
            {
                this.MaritalStatus = value.ToString();
            }
        }

        #endregion

        public string SpouseName { get; set; }

        #region ProviderTitle
        public string ProviderTitleCode { get; set; }

        public string ProviderTitle { get; set; }

        #endregion

        #endregion

        #region Birth Details

        public string CityOfBirth { get; set; }

        public string StateOfBirth { get; set; }  
 
        public string CountryOfBirth { get; set; }

        public string CountyOfBirth { get; set; }

        public string BirthCertificatePath { get; set; }

        public string DateOfBirthStored { get; private set; }

        
        public string DateOfBirth
        {
            get
            {
                var encryptData = EncryptorDecryptor.Decrypt(this.DateOfBirthStored);
                //CultureInfo culture = new CultureInfo("en-US");
                // return date as string
                //return Convert.ToDateTime(encryptData,culture);
                return encryptData;
            }
            set { this.DateOfBirthStored = EncryptorDecryptor.Encrypt(value.ToString()); }
        }

             
        

        #endregion

        #region LanguageInfo
        public string Language { get; set; }
        public int ProficiencyIndex { get; set; }
        #endregion

        #region Other legal Names
        
        public string OtherFirstName { get; set; }

        public string OtherMiddleName { get; set; }

        
        public string OtherLastName { get; set; }

        public string OtherLegalNameSuffix { get; set; }

        
        public DateTime? OtherLegalName_StartDate { get; set; }

      
        public DateTime? OtherLegalName_EndDate { get; set; }

        public string OtherLegalName_DocumentPath { get; set; }

        #region Status

        public string OtherlegalNameStatus { get; private set; }

        
        public StatusType? OtherLegalName_StatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.OtherlegalNameStatus))
                    return null;

                if (this.OtherlegalNameStatus.Equals("Not Available"))
                    return null;

                return (StatusType)Enum.Parse(typeof(StatusType), this.OtherlegalNameStatus);
            }
            set
            {
                this.OtherlegalNameStatus = value.ToString();
            }
        }

        #endregion        
        #endregion 
        #region Contact Information

        #region Address

       
        public string HomeAddress_UnitNumber { get; set; }

        
        public string HomeAddress_Street { get; set; }


        public string HomeAddress_Country { get; set; }


        public string HomeAddress_State { get; set; }

        public string HomeAddress_County { get; set; }


        public string HomeAddress_City { get; set; }


        public string HomeAddress_ZipCode { get; set; }


        #endregion        

        #region Status


        public string HomeAddress_Status { get; private set; }

        public string HomeAddress_Preference { get; private set; }

        public StatusType? HomeAddress_StatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.HomeAddress_Status))
                    return null;

                if (this.HomeAddress_Status.Equals("Not Available"))
                    return null;

                return (StatusType)Enum.Parse(typeof(StatusType), this.HomeAddress_Status);
            }
            set
            {
                this.HomeAddress_Status = value.ToString();
            }
        }

        #endregion        
        public DateTime? HomeAddress_LivingFromDate { get; set; }

        public DateTime? HomeAddress_LivingEndDate { get; set; }
        #endregion
        #region Contact Details

        #region Phone Number

       
        public string PhoneNumber
        {
            get
            {
                if (String.IsNullOrEmpty(this.CountryCode))
                    return this.Number;
                else if (!String.IsNullOrEmpty(this.Number))
                    return this.CountryCode + "-" + this.Number;

                return null;
            }
            set
            {
               
                if (value != null)
                {
                    var numbers = value.Split(new char[] { '-' }, 2);
                    if (numbers.Length == 1)
                        this.Number = numbers[0];
                    else
                    {
                        this.CountryCode = numbers[0];
                        this.Number = numbers[1];
                    }
                }
            }
        }

       
        public string Number { get; set; }

       
        public string CountryCode { get; set; }


        #endregion
        #region PhoneType

      
        public string PhoneType { get; set; }

       
        public virtual PhoneTypeEnum? PhoneTypeEnum
        {
            get
            {
                if (String.IsNullOrEmpty(this.PhoneType))
                    return null;

                if (this.PhoneType.Equals("Not Available"))
                    return null;

                return (PhoneTypeEnum)Enum.Parse(typeof(PhoneTypeEnum), this.PhoneType);
            }
            set
            {
                this.PhoneType = value.ToString();
            }
        }

        #endregion
        #region Status

        
        public string PhoneDetails_Status { get; set; }

        
        public virtual StatusType? PhoneDetails_StatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.PhoneDetails_Status))
                    return null;

                if (this.PhoneDetails_Status.Equals("Not Available"))
                    return null;

                return (StatusType)Enum.Parse(typeof(StatusType), this.PhoneDetails_Status);
            }
            set
            {
                this.PhoneDetails_Status = value.ToString();
            }
        }

        #endregion        
        public string EmailAddress { get; set; }
        #region Status

        
        public string EmailAddress_Status { get; set; }

      
        public virtual StatusType? EmailAddress_StatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.EmailAddress_Status))
                    return null;

                if (this.EmailAddress_Status.Equals("Not Available"))
                    return null;

                return (StatusType)Enum.Parse(typeof(StatusType), this.EmailAddress_Status);
            }
            set
            {
                this.EmailAddress_Status = value.ToString();
            }
        }

        #endregion        
        #endregion

        #region Personal Identification
       
        public string SocialSecurityNumber { get; private set; }

       
        public string SSN
        {
            get { return EncryptorDecryptor.Decrypt(this.SocialSecurityNumber); }
            set { this.SocialSecurityNumber = EncryptorDecryptor.Encrypt(value); }
        }

        public string DL { get; set; }

        public string DLState { get; set; }

        public string SSNCertificatePath { get; set; }
        public string DLCertificatePath { get; set; }
        #endregion
        #region Visa Information
        #region IsResidentOfUSA

      
        public string IsResidentOfUSA { get; private set; }

        
        public YesNoOption? IsResidentOfUSAYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.IsResidentOfUSA))
                    return null;

                if (this.IsResidentOfUSA.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.IsResidentOfUSA);
            }
            set
            {
                this.IsResidentOfUSA = value.ToString();
            }
        }

        #endregion

        #region IsAuthorizedToWorkInUS

        
        public string IsAuthorizedToWorkInUS { get; private set; }

        
        public YesNoOption? IsAuthorizedToWorkInUSYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.IsAuthorizedToWorkInUS))
                    return null;

                if (this.IsAuthorizedToWorkInUS.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.IsAuthorizedToWorkInUS);
            }
            set
            {
                this.IsAuthorizedToWorkInUS = value.ToString();
            }
        }

        #endregion
        #region Visa Number
       
        public string VisaNumberStored { get; private set; }

       
        public string VisaNumber
        {
            get { return EncryptorDecryptor.Decrypt(this.VisaNumberStored); }
            set { this.VisaNumberStored = EncryptorDecryptor.Encrypt(value); }
        }

        #endregion

        #region Green Card Number

        public string GreenCardNumberStored { get; private set; }

        
        public string GreenCardNumber
        {
            get { return EncryptorDecryptor.Decrypt(this.GreenCardNumberStored); }
            set { this.GreenCardNumberStored = EncryptorDecryptor.Encrypt(value); }
        }

        #endregion

        #region National ID Number

        public string NationalIDNumberNumberStored { get; private set; }

        
        public string NationalIDNumber
        {
            get { return EncryptorDecryptor.Decrypt(this.NationalIDNumberNumberStored); }
            set { this.NationalIDNumberNumberStored = EncryptorDecryptor.Encrypt(value); }
        }

        #endregion

        #region VisaStatus

        public string VisaStatus_Title { get; set; }
       
        

        #endregion

        #region VisaType

        public string VisaType_Title { get; set; }

        #endregion

        public string VisaSponsor { get; set; }

       
        public DateTime? VisaExpirationDate { get; set; }

        public string VisaCertificatePath { get; set; }

        public string CountryOfIssue { get; set; }

        public string GreenCardCertificatePath { get; set; }

        public string NationalIDCertificatePath { get; set; }
        #endregion
        #endregion
        #region Practice Location

        public int PracticeLocationDetailID { get; set; }
        public string Practicelocation_IsPrimary { get; set; }
        #region PrimaryTaxId

        public string PracticeLocation_PrimaryTaxId { get; private set; }


        public PrimaryTaxId? PracticeLocation_PrimaryTax
        {
            get
            {
                if (String.IsNullOrEmpty(this.PracticeLocation_PrimaryTaxId))
                    return null;

                if (this.PracticeLocation_PrimaryTaxId.Equals("Not Available"))
                    return null;

                return (PrimaryTaxId)Enum.Parse(typeof(PrimaryTaxId), this.PracticeLocation_PrimaryTaxId);
            }
            set
            {
                this.PracticeLocation_PrimaryTaxId = value.ToString();
            }
        }

        #endregion

        public string PracticeLocation_CorporateName { get; set; }
        public string PracticeLocation_FacilityName { get; set; }
        public string PracticeLocation_OtherPracticeName { get; set; }
       

        public DateTime? PracticeLocation_StartDate { get; set; }

        public YesNoOption? PrimaryYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.Practicelocation_IsPrimary))
                    return null;

                if (this.Practicelocation_IsPrimary.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.Practicelocation_IsPrimary);
            }
            set
            {
                this.Practicelocation_IsPrimary = value.ToString();
            }
        }
        public string Practicelocation_State { get; set; }

        public string Practicelocation_Street { get; set; }

        public string Practicelocation_ZipCode { get; set; }

        public string Practicelocation_City { get; set; }

        public string Practicelocation_Country { get; set; }

        public string Practicelocation_County { get; set; }

        public string Practicelocation_status { get; set; }

        public string Practicelocation_Building { get; set; }

        #region CurrentlyPracticingAtThisAddress

       
        public string CurrentlyPracticingAtThisAddress { get; set; }

       
        public YesNoOption? CurrentlyPracticingYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.CurrentlyPracticingAtThisAddress))
                    return null;

                if (this.CurrentlyPracticingAtThisAddress.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.CurrentlyPracticingAtThisAddress);
            }
            set
            {
                this.CurrentlyPracticingAtThisAddress = value.ToString();
            }
        }

        #endregion

        #region Mobile Number

        public string Practicelocation_MobileNumber
        {
            get
            {
                if (String.IsNullOrEmpty(this.Practicelocation_CountryCodeTelephone))
                    return this.Practicelocation_Telephone;
                else if (!String.IsNullOrEmpty(this.Practicelocation_Telephone))
                    return this.Practicelocation_CountryCodeTelephone + "-" + this.Practicelocation_Telephone;

                return null;
            }
            set
            {
                if (value != null)
                {
                    var numbers = value.Split(new char[] { '-' }, 2);
                    if (numbers.Length == 1)
                        this.Practicelocation_Telephone = numbers[0];
                    else
                    {
                        this.Practicelocation_CountryCodeTelephone = numbers[0];
                        this.Practicelocation_Telephone = numbers[1];
                    }

                }
            }
        }


        public string Practicelocation_Telephone { get; set; }


        public string Practicelocation_CountryCodeTelephone { get; set; }


        #endregion

        #region Fax Number

        public string Practicelocation_FaxNumber
        {
            get
            {


                if (String.IsNullOrEmpty(this.Practicelocation_CountryCodeFax))
                    return this.Practicelocation_Fax;
                else if (!String.IsNullOrEmpty(this.Practicelocation_Fax))
                    return this.Practicelocation_CountryCodeFax + "-" + this.Practicelocation_Fax;

                return null;
            }
            set
            {
                if (value != null)
                {
                    var numbers = value.Split(new char[] { '-' }, 2);
                    if (numbers.Length == 1)
                        this.Practicelocation_Fax = numbers[0];
                    else
                    {
                        this.Practicelocation_CountryCodeFax = numbers[0];
                        this.Practicelocation_Fax = numbers[1];
                    }
                }
            }
        }


        public string Practicelocation_Fax { get; set; }


        public string Practicelocation_CountryCodeFax { get; set; }

        #endregion

        public string Practicelocation_EmailAddress { get; set; }

        #region NonEnglishLanguage
        public string NonEnglishLanguage_Language { get; set; }
        #region Status

        public string NonEnglishLanguage_Status { get; set; }

       
        public StatusType? NonEnglishLanguage_StatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.NonEnglishLanguage_Status))
                    return null;

                if (this.NonEnglishLanguage_Status.Equals("Not Available"))
                    return null;

                return (StatusType)Enum.Parse(typeof(StatusType), this.NonEnglishLanguage_Status);
            }
            set
            {
                this.NonEnglishLanguage_Status = value.ToString();
            }
        }

        #endregion
        #endregion

        public string FacilityPracticeType_Title { get; set; }

        public int? OpenPracticeStatus_MinimumAge { get; set; }
        public int? OpenPracticeStatus_MaximumAge { get; set; }


        #region BusinessOfficeManagerOrStaff

        public string BusinessOfficeManagerOrStaff_LastName { get; set; }
        public string BusinessOfficeManagerOrStaff_MiddleName { get; set; }
        public string BusinessOfficeManagerOrStaff_FirstName { get; set; }

        #region Address

        public string BusinessOfficeManagerOrStaff_Building { get; set; }

        public string BusinessOfficeManagerOrStaff_Street { get; set; }

        public string BusinessOfficeManagerOrStaff_Country { get; set; }

        public string BusinessOfficeManagerOrStaff_State { get; set; }

        public string BusinessOfficeManagerOrStaff_County { get; set; }

        public string BusinessOfficeManagerOrStaff_City { get; set; }

        public string BusinessOfficeManagerOrStaff_ZipCode { get; set; }

        public string BusinessOfficeManagerOrStaff_POBoxAddress { get; set; }

        #endregion

        #region Mobile Number

        public string BusinessOfficeManagerOrStaff_MobileNumber
        {
            get
            {
                if (String.IsNullOrEmpty(this.BusinessOfficeManagerOrStaff_CountryCodeTelephone))
                    return this.BusinessOfficeManagerOrStaff_Telephone;
                else if (!String.IsNullOrEmpty(this.BusinessOfficeManagerOrStaff_Telephone))
                    return this.BusinessOfficeManagerOrStaff_CountryCodeTelephone + "-" + this.BusinessOfficeManagerOrStaff_Telephone;

                return null;
            }
            set
            {
                if (value != null)
                {
                    var numbers = value.Split(new char[] { '-' }, 2);
                    if (numbers.Length == 1)
                        this.BusinessOfficeManagerOrStaff_Telephone = numbers[0];
                    else
                    {
                        this.BusinessOfficeManagerOrStaff_CountryCodeTelephone = numbers[0];
                        this.BusinessOfficeManagerOrStaff_Telephone = numbers[1];
                    }

                }
            }
        }


        public string BusinessOfficeManagerOrStaff_Telephone { get; set; }


        public string BusinessOfficeManagerOrStaff_CountryCodeTelephone { get; set; }


        #endregion

        #region Fax Number

        public string BusinessOfficeManagerOrStaff_FaxNumber
        {
            get
            {


                if (String.IsNullOrEmpty(this.BusinessOfficeManagerOrStaff_CountryCodeFax))
                    return this.BusinessOfficeManagerOrStaff_Fax;
                else if (!String.IsNullOrEmpty(this.BusinessOfficeManagerOrStaff_Fax))
                    return this.BusinessOfficeManagerOrStaff_CountryCodeFax + "-" + this.BusinessOfficeManagerOrStaff_Fax;

                return null;
            }
            set
            {
                if (value != null)
                {
                    var numbers = value.Split(new char[] { '-' }, 2);
                    if (numbers.Length == 1)
                        this.BusinessOfficeManagerOrStaff_Fax = numbers[0];
                    else
                    {
                        this.BusinessOfficeManagerOrStaff_CountryCodeFax = numbers[0];
                        this.BusinessOfficeManagerOrStaff_Fax = numbers[1];
                    }
                }
            }
        }


        public string BusinessOfficeManagerOrStaff_Fax { get; set; }


        public string BusinessOfficeManagerOrStaff_CountryCodeFax { get; set; }

        #endregion

        public string BusinessOfficeManagerOrStaff_EmailAddress { get; set; }
        #endregion

        #region BillingContactPerson

        public string BillingContactPerson_LastName { get; set; }
        public string BillingContactPerson_MiddleName { get; set; }
        public string BillingContactPerson_FirstName { get; set; }

        #region Address

        public string BillingContactPerson_Building { get; set; }

        public string BillingContactPerson_Street { get; set; }

        public string BillingContactPerson_Country { get; set; }

        public string BillingContactPerson_State { get; set; }

        public string BillingContactPerson_County { get; set; }

        public string BillingContactPerson_City { get; set; }

        public string BillingContactPerson_ZipCode { get; set; }

        public string BillingContactPerson_POBoxAddress { get; set; }

        #endregion

        #region Mobile Number

        public string BillingContactPerson_MobileNumber
        {
            get
            {
                if (String.IsNullOrEmpty(this.BillingContactPerson_CountryCodeTelephone))
                    return this.BillingContactPerson_Telephone;
                else if (!String.IsNullOrEmpty(this.BillingContactPerson_Telephone))
                    return this.BillingContactPerson_CountryCodeTelephone + "-" + this.BillingContactPerson_Telephone;

                return null;
            }
            set
            {
                if (value != null)
                {
                    var numbers = value.Split(new char[] { '-' }, 2);
                    if (numbers.Length == 1)
                        this.BillingContactPerson_Telephone = numbers[0];
                    else
                    {
                        this.BillingContactPerson_CountryCodeTelephone = numbers[0];
                        this.BillingContactPerson_Telephone = numbers[1];
                    }

                }
            }
        }

        
        public string BillingContactPerson_Telephone { get; set; }

       
        public string BillingContactPerson_CountryCodeTelephone { get; set; }


        #endregion

        #region Fax Number

        public string BillingContactPerson_FaxNumber
        {
            get
            {


                if (String.IsNullOrEmpty(this.BillingContactPerson_CountryCodeFax))
                    return this.BillingContactPerson_Fax;
                else if (!String.IsNullOrEmpty(this.BillingContactPerson_Fax))
                    return this.BillingContactPerson_CountryCodeFax + "-" + this.BillingContactPerson_Fax;

                return null;
            }
            set
            {
                if (value != null)
                {
                    var numbers = value.Split(new char[] { '-' }, 2);
                    if (numbers.Length == 1)
                        this.BillingContactPerson_Fax = numbers[0];
                    else
                    {
                        this.BillingContactPerson_CountryCodeFax = numbers[0];
                        this.BillingContactPerson_Fax = numbers[1];
                    }
                }
            }
        }

        
        public string BillingContactPerson_Fax { get; set; }

        
        public string BillingContactPerson_CountryCodeFax { get; set; }

        #endregion

        public string BillingContactPerson_EmailAddress { get; set; }
        #endregion

        #region PracticePaymentAndRemittance

        public string PracticePaymentAndRemittance_LastName { get; set; }
        public string PracticePaymentAndRemittance_MiddleName { get; set; }
        public string PracticePaymentAndRemittance_FirstName { get; set; }
        #region ElectronicBillingCapability

       
        public string ElectronicBillingCapability { get; set; }

        
        public YesNoOption? ElectronicBillingCapabilityYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.ElectronicBillingCapability))
                    return null;

                if (this.ElectronicBillingCapability.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.ElectronicBillingCapability);
            }
            set
            {
                this.ElectronicBillingCapability = value.ToString();
            }
        }

        #endregion

        public string BillingDepartment { get; set; }

        
        public string CheckPayableTo { get; set; }

        
        public string Office { get; set; }
        #region Address

        public string PracticePaymentAndRemittance_Building { get; set; }

        public string PracticePaymentAndRemittance_Street { get; set; }

        public string PracticePaymentAndRemittance_Country { get; set; }

        public string PracticePaymentAndRemittance_State { get; set; }

        public string PracticePaymentAndRemittance_County { get; set; }

        public string PracticePaymentAndRemittance_City { get; set; }

        public string PracticePaymentAndRemittance_ZipCode { get; set; }

        public string PracticePaymentAndRemittance_POBoxAddress { get; set; }

        #endregion

        #region Mobile Number

        public string PracticePaymentAndRemittance_MobileNumber
        {
            get
            {
                if (String.IsNullOrEmpty(this.PracticePaymentAndRemittance_CountryCodeTelephone))
                    return this.PracticePaymentAndRemittance_Telephone;
                else if (!String.IsNullOrEmpty(this.PracticePaymentAndRemittance_Telephone))
                    return this.PracticePaymentAndRemittance_CountryCodeTelephone + "-" + this.PracticePaymentAndRemittance_Telephone;

                return null;
            }
            set
            {
                if (value != null)
                {
                    var numbers = value.Split(new char[] { '-' }, 2);
                    if (numbers.Length == 1)
                        this.PracticePaymentAndRemittance_Telephone = numbers[0];
                    else
                    {
                        this.PracticePaymentAndRemittance_CountryCodeTelephone = numbers[0];
                        this.PracticePaymentAndRemittance_Telephone = numbers[1];
                    }

                }
            }
        }


        public string PracticePaymentAndRemittance_Telephone { get; set; }


        public string PracticePaymentAndRemittance_CountryCodeTelephone { get; set; }


        #endregion

        #region Fax Number

        public string PracticePaymentAndRemittance_FaxNumber
        {
            get
            {


                if (String.IsNullOrEmpty(this.PracticePaymentAndRemittance_CountryCodeFax))
                    return this.PracticePaymentAndRemittance_Fax;
                else if (!String.IsNullOrEmpty(this.PracticePaymentAndRemittance_Fax))
                    return this.PracticePaymentAndRemittance_CountryCodeFax + "-" + this.PracticePaymentAndRemittance_Fax;

                return null;
            }
            set
            {
                if (value != null)
                {
                    var numbers = value.Split(new char[] { '-' }, 2);
                    if (numbers.Length == 1)
                        this.PracticePaymentAndRemittance_Fax = numbers[0];
                    else
                    {
                        this.PracticePaymentAndRemittance_CountryCodeFax = numbers[0];
                        this.PracticePaymentAndRemittance_Fax = numbers[1];
                    }
                }
            }
        }


        public string PracticePaymentAndRemittance_Fax { get; set; }


        public string PracticePaymentAndRemittance_CountryCodeFax { get; set; }

        #endregion

        public string PracticePaymentAndRemittance_EmailAddress { get; set; }
        #endregion

        public string PracticeDailyHour_Day { get; set; }
        public string PracticeDailyHour_StartTime { get; set; }


        public string PracticeDailyHour_EndTime { get; set; }

        #region PracticeProvider

        public string PracticeProvider_FirstName { get; set; }

        public string PracticeProvider_MiddleName { get; set; }

        public string PracticeProvider_LastName { get; set; }

        #region Practice Type

        public string PracticeProvider_Practice { get; private set; }


        public PracticeType? PracticeProvider_PracticeType
        {
            get
            {
                if (String.IsNullOrEmpty(this.PracticeProvider_Practice))
                    return null;

                if (this.PracticeProvider_Practice.Equals("Not Available"))
                    return null;

                return (PracticeType)Enum.Parse(typeof(PracticeType), this.PracticeProvider_Practice);
            }
            set
            {
                this.PracticeProvider_Practice = value.ToString();
            }
        }

        #endregion
        #region Mobile Number

        public string PracticeProvider_MobileNumber
        {
            get
            {
                if (String.IsNullOrEmpty(this.PracticeProvider_CountryCodeTelephone))
                    return this.PracticeProvider_Telephone;
                else if (!String.IsNullOrEmpty(this.PracticeProvider_Telephone))
                    return this.PracticeProvider_CountryCodeTelephone + "-" + this.PracticeProvider_Telephone;

                return null;
            }
            set
            {
                if (value != null)
                {
                    var numbers = value.Split(new char[] { '-' }, 2);
                    if (numbers.Length == 1)
                        this.PracticeProvider_Telephone = numbers[0];
                    else
                    {
                        this.PracticeProvider_CountryCodeTelephone = numbers[0];
                        this.PracticeProvider_Telephone = numbers[1];
                    }

                }
            }
        }

        
        public string PracticeProvider_Telephone { get; set; }

        
        public string PracticeProvider_CountryCodeTelephone { get; set; }


        #endregion

        #region Status

        public string PracticeProvider_Status { get; private set; }


        public StatusType? PracticeProvider_StatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.PracticeProvider_Status))
                    return null;

                if (this.PracticeProvider_Status.Equals("Not Available"))
                    return null;

                return (StatusType)Enum.Parse(typeof(StatusType), this.PracticeProvider_Status);
            }
            set
            {
                this.PracticeProvider_Status = value.ToString();
            }
        }

        #endregion
        #endregion

        #region PracticeProviderSpeciality

        public string PracticeProviderSpecialty_Name { get; set; }

        public string PracticeProviderSpecialty_Status { get; set; }


        public StatusType? PracticeProviderSpecialty_StatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.PracticeProviderSpecialty_Status))
                    return null;

                if (this.PracticeProviderSpecialty_Status.Equals("Not Available"))
                    return null;

                return (StatusType)Enum.Parse(typeof(StatusType), this.PracticeProviderSpecialty_Status);
            }
            set
            {
                this.PracticeProviderSpecialty_Status = value.ToString();
            }
        }
        #endregion

        #region Credentialing Information

        public string Credentialing_LastName { get; set; }
        public string Credentialing_MiddleName { get; set; }
        public string Credentialing_FirstName { get; set; }

        #region Address

        public string Credentialing_Building { get; set; }

        public string Credentialing_Street { get; set; }

        public string Credentialing_Country { get; set; }

        public string Credentialing_State { get; set; }

        public string Credentialing_County { get; set; }

        public string Credentialing_City { get; set; }

        public string Credentialing_ZipCode { get; set; }

        public string Credentialing_POBoxAddress { get; set; }

        #endregion

        #region Mobile Number

        public string Credentialing_MobileNumber
        { 
            get
            {
                if (String.IsNullOrEmpty(this.Credentialing_CountryCodeTelephone))
                    return this.Credentialing_Telephone;
                else if (!String.IsNullOrEmpty(this.Credentialing_Telephone))
                    return this.Credentialing_CountryCodeTelephone + "-" + this.Credentialing_Telephone;

                return null;
            }
            set
            {
                if (value != null)
                {
                    var numbers = value.Split(new char[] { '-' }, 2);
                    if (numbers.Length == 1)
                        this.Credentialing_Telephone = numbers[0];
                    else
                    {
                        this.Credentialing_CountryCodeTelephone = numbers[0];
                        this.Credentialing_Telephone = numbers[1];
                    }

                }
            }
        }


        public string Credentialing_Telephone { get; set; }


        public string Credentialing_CountryCodeTelephone { get; set; }


        #endregion

        #region Fax Number

        public string Credentialing_FaxNumber
        {
            get
            {


                if (String.IsNullOrEmpty(this.Credentialing_CountryCodeFax))
                    return this.Credentialing_Fax;
                else if (!String.IsNullOrEmpty(this.Credentialing_Fax))
                    return this.Credentialing_CountryCodeFax + "-" + this.Credentialing_Fax;

                return null;
            }
            set
            {
                if (value != null)
                {
                    var numbers = value.Split(new char[] { '-' }, 2);
                    if (numbers.Length == 1)
                        this.Credentialing_Fax = numbers[0];
                    else
                    {
                        this.Credentialing_CountryCodeFax = numbers[0];
                        this.Credentialing_Fax = numbers[1];
                    }
                }
            }
        }


        public string Credentialing_Fax { get; set; }


        public string Credentialing_CountryCodeFax { get; set; }

        #endregion

        public string Credentialing_EmailAddress { get; set; }
        #endregion

        #endregion


        #region Specialty
        public string SpecialityName { get; set; }
        public string TaxonomyCode { get; set; }

        #region BoardCertified Enum Mapping

       
        public string IsBoardCertified { get; set; }

      
        public YesNoOption? BoardCertifiedYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.IsBoardCertified))
                    return null;

                if (this.IsBoardCertified.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.IsBoardCertified);
            }
            set
            {
                this.IsBoardCertified = value.ToString();
            }
        }

        #endregion

        #region SpecialityBoardCertifiedDetails
        public string CertificateNumber { get; set; }

       
        public DateTime? InitialCertificationDate { get; set; }

      
        public DateTime? LastReCerificationDate { get; set; }

       
        public DateTime? ExpirationDate { get; set; }

       
        public string BoardCertificatePath { get; set; }
        #endregion

        public string SpecialityBoard_Name { get; set; }

        public string SpecialityPreference { get; set; }


        public PreferenceType? PreferenceType
        {
            get
            {
                if (String.IsNullOrEmpty(this.SpecialityPreference))
                    return null;

                if (this.SpecialityPreference.Equals("Not Available"))
                    return null;

                return (PreferenceType)Enum.Parse(typeof(PreferenceType), this.SpecialityPreference);
            }
            set
            {
                this.SpecialityPreference = value.ToString();
            }
        }
        public string SpecialityStatus { get; set; }

        public StatusType? Speciality_StatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.SpecialityStatus))
                    return null;

                return (StatusType)Enum.Parse(typeof(StatusType), this.SpecialityStatus);
            }
            set
            {
                this.SpecialityStatus = value.ToString();
            }
        }
        #endregion
       

        #region ProfessionalLiability
        public string InsuranceCarrier_Name { get; set; }

        public DateTime? ProfessionalLiability_OriginalEffectiveDate { get; set; }


        public DateTime? ProfessionalLiability_EffectiveDate { get; set; }


        public DateTime? ProfessionalLiability_ExpirationDate { get; set; }
        public string InsuranceCertificatePath { get; set; }
        public double? AmountOfCoveragePerOccurance { get; set; }

        public double? AmountOfCoverageAggregate { get; set; }

        

        #region Address
        public string LocationName { get; set; }

        public string Street { get; set; }

      
        public string Country { get; set; }

       
        public string State { get; set; }

        public string County { get; set; }

        
        public string City { get; set; }

        public string ZipCode { get; set; }

        #endregion        
        #region Phone Number


        public string ProfessionalLiability_PhoneNumber
        {
            get
            {
                if (String.IsNullOrEmpty(this.ProfessionalLiability_PhoneCountryCode))
                    return this.ProfessionalLiability_Phone;
                else if (!String.IsNullOrEmpty(this.ProfessionalLiability_Phone))
                    return this.ProfessionalLiability_PhoneCountryCode + "-" + this.ProfessionalLiability_Phone;

                return null;
            }
            set
            {
                if (value != null)
                {
                    var numbers = value.Split(new char[] { '-' }, 2);
                    if (numbers.Length == 1)
                        this.ProfessionalLiability_Phone = numbers[0];
                    else
                    {
                        this.ProfessionalLiability_PhoneCountryCode = numbers[0];
                        this.ProfessionalLiability_Phone = numbers[1];
                    }

                }
            }
        }


        public string ProfessionalLiability_Phone { get; set; }


        public string ProfessionalLiability_PhoneCountryCode { get; set; }


        #endregion
        #region Status

        public string ProfessionalLiability_Status { get; private set; }


        public StatusType? ProfessionalLiability_StatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.ProfessionalLiability_Status))
                    return null;

                if (this.ProfessionalLiability_Status.Equals("Not Available"))
                    return null;

                return (StatusType)Enum.Parse(typeof(StatusType), this.ProfessionalLiability_Status);
            }
            set
            {
                this.ProfessionalLiability_Status = value.ToString();
            }
        }

        #endregion
        #region SelfInsured

       
        public string SelfInsured { get; set; }

       
        public YesNoOption? SelfInsuredYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.SelfInsured))
                    return null;

                if (this.SelfInsured.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.SelfInsured);
            }
            set
            {
                this.SelfInsured = value.ToString();
            }
        }

        #endregion
        #region Policy Number
        
        public string PolicyNumberStored { get; private set; }

        
        public string PolicyNumber
        {
            get
            {
                if (String.IsNullOrEmpty(this.PolicyNumberStored))
                    return null;

                if (this.PolicyNumberStored.Equals("Not Available"))
                    return null;

                return EncryptorDecryptor.Decrypt(this.PolicyNumberStored);
            }
            set { this.PolicyNumberStored = EncryptorDecryptor.Encrypt(value); }
        }

        #endregion
        #endregion

        #region ProfessionalAffiliation_

        public string ProfessionalAffiliation_OrganizationName { get; set; }


        public DateTime? ProfessionalAffiliation_StartDate { get; set; }


        public DateTime? ProfessionalAffiliation_EndDate { get; set; }

        public string ProfessionalAffiliation_PositionOfficeHeld { get; set; }

        public string ProfessionalAffiliation_Member { get; set; }

        #region Status

        public string ProfessionalAffiliation_Status { get; private set; }


        public StatusType? ProfessionalAffiliation_StatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.ProfessionalAffiliation_Status))
                    return null;

                if (this.ProfessionalAffiliation_Status.Equals("Not Available"))
                    return null;

                return (StatusType)Enum.Parse(typeof(StatusType), this.ProfessionalAffiliation_Status);
            }
            set
            {
                this.ProfessionalAffiliation_Status = value.ToString();
            }
        }

        #endregion
        #endregion

        #region HospitalPrivilege_
        public string HospitalPrivilege_HospitalName { get; set; }
        public DateTime? AffilicationStartDate { get; set; }

        public int? HospitalID { get; set; }
        public DateTime? AffiliationEndDate { get; set; }
        public string HospitalPrivilege_LocationName { get; set; }
        public string HospitalPrivilege_Email { get; set; }
        public string HospitalPrivilege_ContactPersonName { get; set; }
        public string HospitalPrivilege_DepartmentName { get; set; }
        public string StaffCategory_Title { get; set; }
        public string HospitalPrivilege_SpecialityName { get; set; }

        #region Address

        //[Required]
        public string HospitalContactInfo_UnitNumber { get; set; }

        //[Required]
        public string HospitalContactInfo_Street { get; set; }
        public string HospitalContactInfo_Suite { get; set; }
        //[Required]
        public string HospitalContactInfo_Country { get; set; }

        //[Required]
        public string HospitalContactInfo_State { get; set; }

        public string HospitalContactInfo_County { get; set; }

        //[Required]
        public string HospitalContactInfo_City { get; set; }

        public string HospitalContactInfo_ZipCode { get; set; }

        #endregion

        #region Phone Number


        public string HospitalContactInfo_Phone
        {
            get
            {
                if (String.IsNullOrEmpty(this.HospitalContactInfo_PhoneCountryCode))
                    return this.HospitalContactInfo_PhoneNumber;
                else if (!String.IsNullOrEmpty(this.HospitalContactInfo_PhoneNumber))
                    return this.HospitalContactInfo_PhoneCountryCode + "-" + this.HospitalContactInfo_PhoneNumber;

                return null;
            }
            set
            {
                if (value != null)
                {
                    var numbers = value.Split(new char[] { '-' }, 2);
                    if (numbers.Length == 1)
                        this.HospitalContactInfo_PhoneNumber = numbers[0];
                    else
                    {
                        this.HospitalContactInfo_PhoneCountryCode = numbers[0];
                        this.HospitalContactInfo_PhoneNumber = numbers[1];
                    }
                }
            }
        }


        public string HospitalContactInfo_PhoneNumber { get; set; }


        public string HospitalContactInfo_PhoneCountryCode { get; set; }


        #endregion

        #region Fax Number

        public string HospitalContactInfo_Fax
        {
            get
            {
                if (String.IsNullOrEmpty(this.HospitalContactInfo_FaxCountryCode))
                    return this.HospitalContactInfo_FaxNumber;
                else if (!String.IsNullOrEmpty(this.HospitalContactInfo_FaxNumber))
                    return this.HospitalContactInfo_FaxCountryCode + "-" + this.HospitalContactInfo_FaxNumber;

                return null;
            }
            set
            {
                if (value != null)
                {
                    var numbers = value.Split(new char[] { '-' }, 2);
                    if (numbers.Length == 1)
                        this.HospitalContactInfo_FaxNumber = numbers[0];
                    else
                    {
                        this.HospitalContactInfo_FaxCountryCode = numbers[0];
                        this.HospitalContactInfo_FaxNumber = numbers[1];
                    }
                }
            }
        }


        public string HospitalContactInfo_FaxNumber { get; set; }
        public string HospitalContactInfo_Email { get; set; }

        public string HospitalContactInfo_FaxCountryCode { get; set; }

        #endregion
        #region Preference


        public string HospitalPrivilege_Preference { get; set; }


        public virtual PreferenceType? HospitalPrivilege_PreferenceType
        {
            get
            {
                if (String.IsNullOrEmpty(this.HospitalPrivilege_Preference))
                    return null;

                if (this.HospitalPrivilege_Preference.Equals("Not Available"))
                    return null;

                return (PreferenceType)Enum.Parse(typeof(PreferenceType), this.HospitalPrivilege_Preference);
            }
            set
            {
                this.HospitalPrivilege_Preference = value.ToString();
            }
        }

        #endregion
        #region Status

        public string HospitalPrivilege_Status { get; private set; }


        public StatusType? HospitalPrivilege_StatusType
        { 
            get
            {
                if (String.IsNullOrEmpty(this.HospitalPrivilege_Status))
                    return null;

                if (this.HospitalPrivilege_Status.Equals("Not Available"))
                    return null;

                return (StatusType)Enum.Parse(typeof(StatusType), this.HospitalPrivilege_Status);
            }
            set
            {
                this.HospitalPrivilege_Status = value.ToString();
            }
        }

        #endregion

        #endregion

        #region EducationHistory

        #region IsUSGraduate

        
        public string IsUSGraduate { get; set; }

       
        public YesNoOption? USGraduateYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.IsUSGraduate))
                    return null;

                if (this.IsUSGraduate.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.IsUSGraduate);
            }
            set
            {
                this.IsUSGraduate = value.ToString();
            }
        }

        #endregion

        public string QualificationDegree { get; set; }

        
        //[Required]
        public DateTime? StartDate { get; set; }

      
        //[Required]
        public DateTime? EndDate { get; set; }

        

        //[Required]
        public string CertificatePath { get; set; }

        #region GraduationType

        //[Required]
        public string GraduationType { get; set; }

        
        public EducationGraduateType? GraduateType
        {
            get
            {
                if (String.IsNullOrEmpty(this.GraduationType))
                    return null;

                if (this.GraduationType.Equals("Not Available"))
                    return null;

                return (EducationGraduateType)Enum.Parse(typeof(EducationGraduateType), this.GraduationType);
            }
            set
            {
                this.GraduationType = value.ToString();
            }
        }

        #endregion
        #region School Information
        public string SchoolInformation_SchoolName { get; set; }

        public string SchoolInformation_Email { get; set; }

        public int SchoolInformationID { get; set; }


        #region Phone Number

        //[Required]
        public string SchoolInformation_PhoneNumber
        {
            get
            {
                if (String.IsNullOrEmpty(this.SchoolInformation_PhoneCountryCode))
                    return this.SchoolInformation_Phone;
                else if (!String.IsNullOrEmpty(this.SchoolInformation_Phone))
                    return this.SchoolInformation_PhoneCountryCode + "-" + this.SchoolInformation_Phone;

                return null;
            }
            set
            {
                if (value != null)
                {
                    var numbers = value.Split(new char[] { '-' }, 2);
                    if (numbers.Length == 1)
                        this.SchoolInformation_Phone = numbers[0];
                    else
                    {
                        this.SchoolInformation_PhoneCountryCode = numbers[0];
                        this.SchoolInformation_Phone = numbers[1];
                    }
                }
            }
        }


        public string SchoolInformation_Phone { get; set; }


        public string SchoolInformation_PhoneCountryCode { get; set; }


        #endregion

        #region Fax Number

        public string SchoolInformation_FaxNumber
        {
            get
            {
                if (String.IsNullOrEmpty(this.SchoolInformation_FaxCountryCode))
                    return this.SchoolInformation_Fax;
                else if (!String.IsNullOrEmpty(this.SchoolInformation_Fax))
                    return this.SchoolInformation_FaxCountryCode + "-" + this.SchoolInformation_Fax;

                return null;
            }
            set
            {
                if (value != null)
                {
                    var numbers = value.Split(new char[] { '-' }, 2);
                    if (numbers.Length == 1)
                        this.SchoolInformation_Fax = numbers[0];
                    else
                    {
                        this.SchoolInformation_FaxCountryCode = numbers[0];
                        this.SchoolInformation_Fax = numbers[1];
                    }
                }

            }
        }


        public string SchoolInformation_Fax { get; set; }


        public string SchoolInformation_FaxCountryCode { get; set; }

        #endregion

        #region Address

        //[Required]
        public string SchoolInformation_Building { get; set; }

        //[Required]
        public string SchoolInformation_Street { get; set; }

        //[Required]
        public string SchoolInformation_Country { get; set; }

        //[Required]
        public string SchoolInformation_State { get; set; }

        public string SchoolInformation_County { get; set; }

        //[Required]
        public string SchoolInformation_City { get; set; }

        // [Required]
        public string SchoolInformation_ZipCode { get; set; }

        #endregion        
        #endregion
        #region QualificationType


        public string QualificationType { get; set; }

      
        public EducationQualificationType? EducationQualificationType
        {
            get
            {
                if (String.IsNullOrEmpty(this.QualificationType))
                    return null;

                if (this.QualificationType.Equals("Not Available"))
                    return null;

                return (EducationQualificationType)Enum.Parse(typeof(EducationQualificationType), this.QualificationType);
            }
            set
            {
                this.QualificationType = value.ToString();
            }
        }

        #endregion        

        #region Status

        public string EducationHistory_Status { get; private set; }


        public StatusType? EducationHistory_StatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.EducationHistory_Status))
                    return null;

                if (this.EducationHistory_Status.Equals("Not Available"))
                    return null;

                return (StatusType)Enum.Parse(typeof(StatusType), this.EducationHistory_Status);
            }
            set
            {
                this.EducationHistory_Status = value.ToString();
            }
        }

        #endregion        

        #region ECFMG Details

        public string ECFMGNumber { get; set; }

       
        
        public DateTime? ECFMGIssueDate { get; set; }

       
        public string ECFMGCertPath { get; set; }

        #endregion

        #region Residency/Internship/Fellowship details

        #region IsCompleted

        //[Required]
        public string IsCompleted { get; set; }

       
        public YesNoOption? CompletedYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.IsCompleted))
                    return null;

                if (this.IsCompleted.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.IsCompleted);
            }
            set
            {
                this.IsCompleted = value.ToString();
            }
        }

        #endregion

        #region ProgramType

       
        public string ProgramType { get; set; }

       
        public ResidencyInternshipProgramType? ResidencyInternshipProgramType
        {
            get
            {
                if (String.IsNullOrEmpty(this.ProgramType))
                    return null;

                if (this.ProgramType.Equals("Not Available"))
                    return null;

                return (ResidencyInternshipProgramType)Enum.Parse(typeof(ResidencyInternshipProgramType), this.ProgramType);
            }
            set
            {
                this.ProgramType = value.ToString();
            }
        }

        #endregion

        #region Specialty



        public string ProgramDetail_SpecialtyName { get; set; }

        #endregion

        #region Preference

        //[Required]
        public string ProgramDetail_Preference { get; set; }


        public PreferenceType? ProgramDetail_PreferenceType
        {
            get
            {
                if (String.IsNullOrEmpty(this.ProgramDetail_Preference))
                    return null;

                if (this.ProgramDetail_Preference.Equals("Not Available"))
                    return null;

                return (PreferenceType)Enum.Parse(typeof(PreferenceType), this.ProgramDetail_Preference);
            }
            set
            {
                this.ProgramDetail_Preference = value.ToString();
            }
        }

        #endregion        

        #region Program Detail School Information
        public string ProgramDetail_SchoolInformation_SchoolName { get; set; }

        public string ProgramDetail_SchoolInformation_Email { get; set; }

        public int ProgramDetail_SchoolInformationID { get; set; }


        #region Phone Number

        //[Required]
        public string ProgramDetail_SchoolInformation_PhoneNumber
        {
            get
            {
                if (String.IsNullOrEmpty(this.ProgramDetail_SchoolInformation_PhoneCountryCode))
                    return this.ProgramDetail_SchoolInformation_Phone;
                else if (!String.IsNullOrEmpty(this.ProgramDetail_SchoolInformation_Phone))
                    return this.ProgramDetail_SchoolInformation_PhoneCountryCode + "-" + this.ProgramDetail_SchoolInformation_Phone;

                return null;
            }
            set
            {
                if (value != null)
                {
                    var numbers = value.Split(new char[] { '-' }, 2);
                    if (numbers.Length == 1)
                        this.ProgramDetail_SchoolInformation_Phone = numbers[0];
                    else
                    {
                        this.ProgramDetail_SchoolInformation_PhoneCountryCode = numbers[0];
                        this.ProgramDetail_SchoolInformation_Phone = numbers[1];
                    }
                }
            }
        }


        public string ProgramDetail_SchoolInformation_Phone { get; set; }


        public string ProgramDetail_SchoolInformation_PhoneCountryCode { get; set; }


        #endregion

        #region Fax Number

        public string ProgramDetail_SchoolInformation_FaxNumber
        {
            get
            {
                if (String.IsNullOrEmpty(this.ProgramDetail_SchoolInformation_FaxCountryCode))
                    return this.ProgramDetail_SchoolInformation_Fax;
                else if (!String.IsNullOrEmpty(this.ProgramDetail_SchoolInformation_Fax))
                    return this.ProgramDetail_SchoolInformation_FaxCountryCode + "-" + this.ProgramDetail_SchoolInformation_Fax;

                return null;
            }
            set
            {
                if (value != null)
                {
                    var numbers = value.Split(new char[] { '-' }, 2);
                    if (numbers.Length == 1)
                        this.ProgramDetail_SchoolInformation_Fax = numbers[0];
                    else
                    {
                        this.ProgramDetail_SchoolInformation_FaxCountryCode = numbers[0];
                        this.ProgramDetail_SchoolInformation_Fax = numbers[1];
                    }
                }

            }
        }


        public string ProgramDetail_SchoolInformation_Fax { get; set; }


        public string ProgramDetail_SchoolInformation_FaxCountryCode { get; set; }

        #endregion

        #region Address

        //[Required]
        public string ProgramDetail_SchoolInformation_Building { get; set; }

        //[Required]
        public string ProgramDetail_SchoolInformation_Street { get; set; }

        //[Required]
        public string ProgramDetail_SchoolInformation_Country { get; set; }

        //[Required]
        public string ProgramDetail_SchoolInformation_State { get; set; }

        public string ProgramDetail_SchoolInformation_County { get; set; }

        //[Required]
        public string ProgramDetail_SchoolInformation_City { get; set; }

        // [Required]
        public string ProgramDetail_SchoolInformation_ZipCode { get; set; }

        #endregion
        #endregion

        public DateTime? ProgramDetail_StartDate { get; set; }


        public DateTime? ProgramDetail_EndDate { get; set; }
        #endregion


        #region CMECertification_

        public string Certification { get; set; }
        #region Program Detail School Information
        public string CMECertification_SchoolInformation_SchoolName { get; set; }

        public string CMECertification_SchoolInformation_Email { get; set; }

        public int CMECertification_SchoolInformationID { get; set; }
        public DateTime? CMECertification_StartDate { get; set; }


        public DateTime? CMECertification_EndDate { get; set; }

        #region Phone Number

        //[Required]
        public string CMECertification_SchoolInformation_PhoneNumber
        {
            get
            {
                if (String.IsNullOrEmpty(this.CMECertification_SchoolInformation_PhoneCountryCode))
                    return this.CMECertification_SchoolInformation_Phone;
                else if (!String.IsNullOrEmpty(this.CMECertification_SchoolInformation_Phone))
                    return this.CMECertification_SchoolInformation_PhoneCountryCode + "-" + this.CMECertification_SchoolInformation_Phone;

                return null;
            }
            set
            {
                if (value != null)
                {
                    var numbers = value.Split(new char[] { '-' }, 2);
                    if (numbers.Length == 1)
                        this.CMECertification_SchoolInformation_Phone = numbers[0];
                    else
                    {
                        this.CMECertification_SchoolInformation_PhoneCountryCode = numbers[0];
                        this.CMECertification_SchoolInformation_Phone = numbers[1];
                    }
                }
            }
        }


        public string CMECertification_SchoolInformation_Phone { get; set; }


        public string CMECertification_SchoolInformation_PhoneCountryCode { get; set; }


        #endregion

        #region Fax Number

        public string CMECertification_SchoolInformation_FaxNumber
        {
            get
            {
                if (String.IsNullOrEmpty(this.CMECertification_SchoolInformation_FaxCountryCode))
                    return this.CMECertification_SchoolInformation_Fax;
                else if (!String.IsNullOrEmpty(this.CMECertification_SchoolInformation_Fax))
                    return this.CMECertification_SchoolInformation_FaxCountryCode + "-" + this.CMECertification_SchoolInformation_Fax;

                return null;
            }
            set
            {
                if (value != null)
                {
                    var numbers = value.Split(new char[] { '-' }, 2);
                    if (numbers.Length == 1)
                        this.CMECertification_SchoolInformation_Fax = numbers[0];
                    else
                    {
                        this.CMECertification_SchoolInformation_FaxCountryCode = numbers[0];
                        this.CMECertification_SchoolInformation_Fax = numbers[1];
                    }
                }

            }
        }


        public string CMECertification_SchoolInformation_Fax { get; set; }


        public string CMECertification_SchoolInformation_FaxCountryCode { get; set; }

        #endregion

        #region Address

        //[Required]
        public string CMECertification_SchoolInformation_Building { get; set; }

        //[Required]
        public string CMECertification_SchoolInformation_Street { get; set; }

        //[Required]
        public string CMECertification_SchoolInformation_Country { get; set; }

        //[Required]
        public string CMECertification_SchoolInformation_State { get; set; }

        public string CMECertification_SchoolInformation_County { get; set; }

        //[Required]
        public string CMECertification_SchoolInformation_City { get; set; }

        // [Required]
        public string CMECertification_SchoolInformation_ZipCode { get; set; }

        #endregion
        #endregion

        #endregion


        #endregion

        #region Identification & Licenses

        #region State License Information details


        #region Status

        public string StateLicenses_Status { get; private set; }


        public StatusType? StateLicenses_StatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.StateLicenses_Status))
                    return null;

                if (this.StateLicenses_Status.Equals("Not Available"))
                    return null;

                return (StatusType)Enum.Parse(typeof(StatusType), this.StateLicenses_Status);
            }
            set
            {
                this.StateLicenses_Status = value.ToString();
            }
        }

        #endregion
        public string ProviderType_Title { get; set; }
        public string LicenseNumber { get; set; }
        public string IssueState { get; set; }
        public DateTime? IssueDate { get; set; }

        public DateTime? CurrentIssueDate { get; set; }
        public DateTime? ExpiryDate { get; set; }

        public string StateLicenseStatus_Title { get; set; }

        #endregion


        #region FederalDEA Information

        #region Status

        public string FederalDEAInformation_Status { get; private set; }


        public StatusType? FederalDEAInformation_StatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.FederalDEAInformation_Status))
                    return null;

                if (this.FederalDEAInformation_Status.Equals("Not Available"))
                    return null;

                return (StatusType)Enum.Parse(typeof(StatusType), this.FederalDEAInformation_Status);
            }
            set
            {
                this.FederalDEAInformation_Status = value.ToString();
            }
        }

        #endregion

        public string DEANumber { get; set; }

       
        public string StateOfReg { get; set; }


        public DateTime? FederalDEA_IssueDate { get; set; }


        public DateTime? FederalDEA_ExpiryDate { get; set; }

        #region IsInGoodStanding

        //[Required]
        public string IsInGoodStanding { get; set; }

       
        public YesNoOption? GoodStandingYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.IsInGoodStanding))
                    return null;

                if (this.IsInGoodStanding.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.IsInGoodStanding);
            }
            set
            {
                this.IsInGoodStanding = value.ToString();
            }
        }

        #endregion

        public string DEALicenceCertPath { get; set; }



        #endregion


        #region Medicare and Medicaid Information

        #region Medicare Information
        public string Medicare_LicenseNumber { get; set; }

       
       

        #region Status

        public string Medicare_Status { get; private set; }


        public StatusType? Medicare_StatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.Medicare_Status))
                    return null;

                if (this.Medicare_Status.Equals("Not Available"))
                    return null;

                return (StatusType)Enum.Parse(typeof(StatusType), this.Medicare_Status);
            }
            set
            {
                this.Medicare_Status = value.ToString();
            }
        }

        #endregion
        #endregion

        #region Medicaid Information
        public string Medicaid_LicenseNumber { get; set; }


        public string Medicaid_State { get; set; }
        #region Status

        public string Medicaid_Status { get; private set; }


        public StatusType? Medicaid_StatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.Medicaid_Status))
                    return null;

                if (this.Medicaid_Status.Equals("Not Available"))
                    return null;

                return (StatusType)Enum.Parse(typeof(StatusType), this.Medicaid_Status);
            }
            set
            {
                this.Medicaid_Status = value.ToString();
            }
        }

        #endregion
        #endregion



        #region CDS Information

        public string CDSCInformation_CertNumber { get; set; }


        public string CDSCInformation_State { get; set; }


        public DateTime? CDSCInformation_IssueDate { get; set; }


        public DateTime? CDSCInformation_ExpiryDate { get; set; }

        public string CDSCCerificatePath { get; set; }


        #region Status

        public string CDSCInformation_Status { get; private set; }


        public StatusType? CDSCInformation_StatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.CDSCInformation_Status))
                    return null;

                if (this.CDSCInformation_Status.Equals("Not Available"))
                    return null;

                return (StatusType)Enum.Parse(typeof(StatusType), this.CDSCInformation_Status);
            }
            set
            {
                this.CDSCInformation_Status = value.ToString();
            }
        }

        #endregion



        #endregion



        #region Other Identification Numbers

        public string NPINumber { get; set; }

        public string CAQHNumber { get; set; }

        #region NPI UserName & Password

        public string NPIUserName { get; set; }


        public string NPIPasswordStored { get; private set; }

       
        public string NPIPassword
        {
            get { return EncryptorDecryptor.Decrypt(this.NPIPasswordStored); }
            set { this.NPIPasswordStored = EncryptorDecryptor.Encrypt(value); }
        }

        #endregion

        #region CAQH UserName & Password

        public string CAQHUserName { get; set; }


        public string CAQHPasswordStored { get; set; }


       
        public string CAQHPassword
        {
            get { return EncryptorDecryptor.Decrypt(this.CAQHPasswordStored); }
            set { this.CAQHPasswordStored = EncryptorDecryptor.Encrypt(value); }
        }

        #endregion

        public string UPINNumber { get; set; }

        public string USMLENumber { get; set; }

        #endregion
        #endregion
        #endregion

        #region Organization Info/Contract Info

        public DateTime? ContractInfo_JoiningDate { get; set; }
        public string ContractInfo_Status { get; set; }
        public string ContractInfo_IndividualTaxId { get; set; }
       
        #region Organization Info

        public string ContractGroupInfo_GroupName { get; set; }
        public string ContractGroupInfo_Status { get; set; }
        public string ContractGroupInfo_Accepted { get; set; }
        public string ContractGroupInfo_GroupTaxId { get; set; }
        public string ContractGroupInfo_GroupNPI { get; set; }

        #endregion
        #endregion

        #region Work History
        public string ProfessionalWorkExperience_EmployerName { get; set; }

        #region Address



        public string ProfessionalWorkExperience_State { get; set; }

        public string ProfessionalWorkExperience_County { get; set; }

        public string ProfessionalWorkExperience_City { get; set; }

        public string ProfessionalWorkExperience_ZipCode { get; set; }

        #endregion

        public DateTime? ProfessionalWorkExperience_StartDate { get; set; }


        public DateTime? ProfessionalWorkExperience_EndDate { get; set; }

        public string ProfessionalWorkExperience_JobTitle { get; set; }



        public string ProfessionalWorkExperience_EmployerEmail { get; set; }

        #endregion

        #region Custom Field 
        public string CustomFieldTitle { get; set; }

        public string CustomFieldTransactionDataValue { get; set; }
        #endregion
    }
}
