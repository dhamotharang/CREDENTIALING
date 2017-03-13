using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace AHC.CD.WebUI.MVC.Helper
{
    /// <summary>
    /// Prototype Required Json Data Helper
    /// </summary>
    public static class PrototypeHelper
    {

        #region Master Data Of Providers
        private static string[] providerNames = { "HEND FARAG ABDELMALEK", "QAHTAN ABDUL FATTAH", "HUSAM ABUZARAD", "Ian Adam", "SYED ALI", "SUMBUL ARSHAD ALI", "VINCENT ALIA", "AZIZ A. AL KHAFAJI", "EYAD ALSABBAGH", "AYHAM ALSHAAR", "LINGAPPA AMARCHAND", "MICHELLE ARRIETA", "SHAMMI BALI", "DALTON BENSON", "ANIL BHATIA", "MARTIN CANILLAS, MD", "AMY B. CAPOOCIA", "GANESH CHARI", "LUIS CONTRERAS", "MARK A. DENNER", "IVAN DIAZ, M.D.", "ODELSA DIAZ", "SANKARA DINAVAHI", "PAL DURAI", "DHAMMIKA EKANAYAKE", "CAMERON ESMKHANI", "MIGUEL A. FANA", "HAJERA FATIMA", "Javier Rafael Gonzalez", "CHAD GORMAN", "ALBERT GUTIERREZ JR.", "ROBERT HARTZELL", "ELIZABETH HATZ", "DAVID HERNDON", "ANTHONY ISENALUMHE, JR.", "ABDEL K. JIBAWI,MD", "BRUNEL JOSEPH", "LUIS A. JOVEL ", "RAGHU JUVVADI", "BRIAN C. KROLL", "JAYACHANDRA KUMAR", "LUKE C. KUNG", "JENNIFER LAMAN", "MAYRA LORENZO", "HARISH M. MADNANI", "GHIATH MAHMALJY", "YULIYA MARKOVA-ACEVEDO", "MATTHEW D. MARSH", "ASIF MASOOD", "GAURAV MATHUR", "BRIAN E. MCCARTHY", "LAKSHMI MENEZES", "NARESH PETER MENEZES", "DAVID R. MILLER", "EMADELDIN A. MOHAMED", "ABU SAYEED MOHAMMAD", "GAYATHRI MORRAREDDY", "DANIEL P. MOYNIHAN", "VALDELINE MUEHL", "AZZAM MUFTAH", "MOHAMMED SAMI MUGHNI", "MAYRA N. MUNOZ-DELGADO", "HOA VAN NGUYEN", "NARIMAN NIKTASH", "MAHMOUD A. NIMER", "SEEMA NISHAT", "STEPHEN P. NYSTROM", "ROBIN G. O’ROURKE", "KEVIN V. PALMER", "ASHISH PATEL", "SHEETAL PATEL", "MALLIK A. PIDURU", "JUDE A. PIERRE ", "NADJA M. PIERRE", "SHAHEEN PIRANI", "SANTOSH POTDAR", "DAN W. PULSIPHER", "BHUPATIRAJU RAJU", "IVAN A. RAMOS", "VINOD RAXWAL", "MICHAEL BRYAN REYNOLDS", "AASMA RIAZ", "JOSE JESUS RODRIGUEZ", "RASHID SABA", "SHEREEN I. SABA", "FARAH SAGHEER", "DAVID J. SASSANO", "Maria Scunziano-Singh", "APURVA SHAH", "JATIN SHETH", "DANISH MUMTAZ SIDDIQ", "Pariksith Singh", "CRAIG SPERGEL", "BRENT E. SQUIRES", "VESELIN STOYANOV", "ALEX A. TAMBRINI", "IMAD TARABISHY", "JAIME LUIS TORRES", "GREG VARLAKOV ", "RAFAEL VELASQUEZ", "MANJUSRI VENNAMANENI", "NATHANIEL VINJE", "MITCHELL A. WEINER", "HENRY J. WEISS", "JOSEPH C. WILLIAMS", "STEVEN GRANT WILLIS", "FARRUKH ZAIDI" };
        private static string[] specialties = { "FAMILY MEDICINE GERIATRIC MEDICINE", "BOARD CERTIFIED IN NEUROLOGY & PSYCHIATRY", "INTERNAL MEDICINE", "INTERNAL MEDICINE", "INTERNAL MEDICINE", "BOARD CERTIFIED IN INTERNAL MEDICINE", "FAMILY MEDICINE", "FELLOW OF THE AMERICAN COLLEGE OF SURGEONS", "PHYSICAL MEDICINE & REHABILITATION", "BOARD CERTIFIED IN ALLERGY, ASTHMA & IMMUNOLOGY", "SERVED AS CHIEF OF STAFF AT BROOKSVILLE REGIONAL HOSPITAL", "INTERNAL MEDICINE", "INTERNAL MEDICINE", "INTERNAL MEDICINE", "PRIMARY CARE", "SPORTS MEDICINE PHYSICIAN", "FAMILY MEDICINE GERIATRIC MEDICINE", "BOARD CERTIFIED BY THE AMERICAN BOARD OF PSYCHIATRY & NEUROLOGY", "INTERNAL MEDICINE", "BOARD CERTIFIED – FAMILY MEDICINE", "BOARD CERTIFIED – FAMILY MEDICINE", "GENERAL PRACTITIONER", "INTERNAL MEDICINE", "Board Certified in Internal Medicine & Geriatric Medicine", "INTERNAL MEDICINE", "ORTHOPEDICS - SPORTS MEDICINE PHYSICIAN", "Internal Medicine", "BOARD CERTIFIED IN FAMILY MEDICINE", "GENERAL PRACTICE", "INTERNAL MEDICINE", "Radiologist", "Family Medicine Geriatric Medicine", "Board Certified in Family Medicine", "Family Medicine", "BOARD CERTIFIED IN ANESTHESIOLOGY", "HOSPITALIST", "Family Medicine", "Board Certified in Family Medicine", "Internal Medicine", "FAMILY MEDICINE", "BOARD CERTIFIED IN PULMONARY MEDICINE", "INTERNAL MEDICINE", "Family Medicine Geriatric Medicine", "INTERNAL MEDICINE", "INTERNAL MEDICINE", "BOARD CERTIFIED IN INTERNAL MEDICINE", "BOARD CERTIFIED HOSPITALIST", "SPORTS MEDICINE", "BOARD CERTIFIED IN FAMILY PRACTICE", "MEMBER OF THE ROYAL COLLEGE OF PHYSICIANS, UK", "PODIATRIC PHYSICIAN AND SURGEON", "Internal Medicine", "INTERNAL MEDICINE and Infectious Disease", "BOARD CERTIFIED – FAMILY MEDICINE", "PHYSICAL THERAPIST", "HOSPITALIST", "Board Certified in Internal Medicine", "ORTHOPEDICS - SURGEON", "FAMILY MEDICINE", "BOARD CERTIFIED IN INTERNAL MEDICINE AND GASTROENTEROLOGY", "FELLOW OF THE AMERICAN COLLEGE OF RHEUMATOLOGY", "FAMILY MEDICINE", "SPECIALTY IN OTOLARYNGOLOGY", "BOARD CERTIFIED IN INTERNAL MEDICINE", "BOARD CERTIFIED IN CARDIOVASCULAR DISEASE", "INTERNAL MEDICINE", "ORTHOPEDICS - SURGEON", "CLINICAL DIETITIAN/NUTRITION SPECIALIST", "GENERAL AND VASCULAR SURGEON", "HOSPITALIST", "FAMILY MEDICINE", "BOARD CERTIFIED IN GENERAL SURGERY", "Internal Medicine", "PODIATRIC MEDICINE", "GENERAL MEDICINE", "BOARD CERTIFIED IN GENERAL SURGERY", "FAMILY MEDICINE", "CARDIOVASCULAR DISEASE", "BOARD CERTIFIED IN INTERNAL MEDICINE", "SPECIALTY IN INTERVENTIONAL CARDIOLOGY", "FAMILY MEDICINE", "Family Medicine", "INTERNAL MEDICINE", "INTERNAL MEDICINE", "SPECIALTY IN INFECTIOUS DISEASES", "PULMONARY/CRITICAL CARE PHYSICIAN", "PRIMARY CARE", "Internal Medicine", "Internal Medicine", "FAMILY MEDICINE", "INTERNAL MEDICINE & INFECTIOUS DISEASE", "Internal Medicine", "HOSPITALIST", "LICENSED PEDORTHIST & ORTHOTIC FITTER", "FAMILY MEDICINE", "FAMILY PRACTICE (PRIMARILY SEES ADULTS)", "BOARD CERTIFIED IN ORTHOPEDICS", "INTERNAL MEDICINE", "Family Medicine", "BOARD CERTIFIED IN FAMILY MEDICINE", "INTERNAL MEDICINE", "INTERNAL MEDICINE", "HOSPITALIST & INTERNAL MEDICINE", "HOSPITALIST & INTERNAL MEDICINE", "BOARD CERTIFIED IN PSYCHIATRY & NEUROLOGY", "CHIROPRACTIC MEDICINE", "RHEUMATOLOGIST" };
        private static string[] primaryPracticLocations = { "13911 Lakeshore Blvd., Suite 111 Hudson, FL 34667", "11323 Cortez Blvd. Brooksville, FL 34613", "11373 Cortez Blvd., Suite 300 Brooksville, FL 34613", "5323 Grand Blvd. New Port Richey, FL 34652", "11451 Cortez Blvd. Brooksville, FL 34613", "5382 Spring Hill Dr. Spring Hill, FL 34606", "5344 Spring Hill Dr.  Spring Hill, FL 34606 Hudson, FL 34669", "5411 Grand Blvd., Suite 109 New Port Richey, FL 34652", "12148 Cortez Blvd. Brooksville, FL 34613", "11333 Cortez Blvd. Brooksville, FL 34613", "750 DeSoto Ave. Brooksville, FL 34601", "6450 38th Avenue North St. Petersburg, FL 33710", "1100 SW Saint Lucie West Blvd., Suite 209 Port Saint Lucie, FL 34986", "1903 Hwy. 44 West Inverness, FL 34453", "11479 Cortez Blvd Brooksville, FL 34613", "14555 Cortez Blvd. Brooksville, FL 34613", "13235 State Rd. 52 Homosassa, FL 34669", "8172 Chaucer Dr., Weeki Wachee, FL 34607", "10441 Quality Dr., Suite 100 Spring Hill, FL 34609X", "10200 Yale Ave. Brooksville, FL 34613", "10200 Yale Ave. Weeki Wachee, FL 34613", "Beverly Hills, Florida 34465 ", "11321 Cortez Blvd. ", "7505 Rottingham Rd Port Richey, FL 34668", "10045 Cortez Blvd., Suite 110 Weeki Wachee, FL 34613", "9555 Seminole Blvd., Suite 103 Seminole, FL 33772", "5798 38th Ave. St. Petersburg, FL 33710", "1801 S 23 Rd St. 9 Fort Pierce, FL 34950", "1194 Mariner Blvd. Spring Hill, FL 34609", "14555 Cortez Blvd. Brooksville, FL 34613", "12037 Cortez Blvd. Brooksville, FL 34613", "11479 Cortez Blvd. Brooksville, FL 34613", "1700 Hillmoore Dr., Suite 501 Port St. Lucie, FL 34986", "7633 Cita Ln. New Port Richey, FL 34653", "11151 Spring Hill Dr. Spring Hill, FL 34609", "5350 Spring Hill Dr. Spring Hill, FL 34606", "2137 W Martin Luther King Blvd. Tampa, FL 33607  ", "2323 1st Ave. North St. Petersburg, FL 33713", "5362 Spring Hill Dr. Spring Hill, FL 34606", "120 Medical Blvd, Suite 102 Spring Hill, FL 34609", "10075 Cortez Blvd Weeki Wachee, FL 34613", "634 NE Jensen Beach Blvd. Jensen Beach, FL 34957", "13235 State Rd. 52, Suite 102 Hudson, FL 34669", "12150 Seminole Blvd. Largo, FL 33778", "13911 Lakeshore Blvd. Suite 111 Hudson, FL 34667", "11373 Cortez Blvd., Suite 302 Brooksville, FL 34613", "5350 Spring Hill Dr. Spring Hill, FL 34606", "14555 Cortez Blvd. Brooksville, FL 34613", "5350 Spring Hill Dr. Spring Hill FL 34606", "14540 Cortez Blvd Ste 113 Brooksville, FL 34613", "11349 Cortez Blvd. Brooksville, FL 34613", "10494 Northcliffe Blvd. Spring Hill, FL 34608", "10494 Northcliffe Blvd. Spring Hill, FL 34608", "10200 Yale Ave. Brooksville, FL 34613", "Brooksville, FL 34613 ", "5350 Spring Hill Dr. Spring Hill, FL 34606", "5537 Gulf Drive New Port Richey, Florida 34652", "14555 Cortez Blvd. Brooksville, FL 34613", "12150 Seminole Blvd. Largo, FL 33778", "12900 Cortez Blvd, Suite 203 Brooksville, FL 34613", "8029 Washington St. Port Richey, FL 34668", "5362 Spring Hill Dr. Spring Hill, FL 34606", "3510 Mariner Blvd Spring Hill, FL 34609", "5350 Spring Hill Dr Spring Hill, FL 34606", "14540 Cortez Blvd., Suite 113 Brooksville, FL 34613", "11451 Cortez Blvd. Brooksville, FL 34613", "14555 Cortez Blvd. Brooksville, FL 34613", "5382 Spring Hill Dr. Spring Hill, FL 34606", "11373 Cortez Blvd., Suite 201 Brooksville, FL 34613", "5350 Spring Hill Dr. Spring Hill, FL 34606", "13944 Lakeshore Blvd., Suite A Hudson, FL 34667", "11373 Cortez Blvd., Suite 201 Brooksville, FL 34613", " 5290 Applegate Dr.  Spring Hill, FL 34606", "3502 Mariner Blvd. Spring Hill, FL 34609", "1801 South 23rd Str., Suite 9 Ft. Pierce, FL 34950", "11343 Cortez Blvd. Brooksville, FL 34613", "7556 Spring Hill Dr. Spring Hill, FL 34606", "750 Desoto Ave. Brooksville, FL 34601", "11910 Little Rd. New Port Richey, FL 34654", "14100 Fivay Rd., Suite 130 Hudson, FL 3466", "3480 Deltona Blvd. Spring Hill, FL 34606", "5350 Spring Hill Dr. Spring Hill, FL 34606", "401 N. Central Avenue Inverness, Florida 34453", "5350 Spring Hill Dr Spring Hill, FL 34606", "11331 Cortez Blvd. Brooksville, FL 34613", "17222 Hospital Blvd., Suite 318 Spring Hill, FL 34606 Brooksville, FL 34601", "13911 Lakeshore Blvd., Suite 107 Hudson FL, 34667", "5350 Spring Hill Dr. Spring Hill, FL 34606", "13220 Belcher Rd., Suite 11 Largo, FL 33773", "920 W Jefferson Str. Brooksville, FL 34601", "14690 Spring Hill Dr., Suite 206 Spring Hill, FL 34609", "5350 Spring Hill Dr. Spring Hill, FL 34606", "5798 38th Ave North,  Saint Petersburg  FL 33710", "3502 Mariner Blvd. Spring Hill, FL 34609", "7633 Cita Lane. New Port Richey, FL 34653", "92 Cypress Blvd. West Homosassa, FL 34446", "11339 Cortez Blvd. Brooksville, FL 34613", "6279 N. Lecanto Hwy Beverly Hills, FL 34465", "8365 South Suncoast Blvd. Homosassa, FL 34446", "5798 38th Avenue North St. Petersburg, FL 33710", "5350 Spring Hill Dr. Spring Hill, FL 34608", "401 N Central Ave. Inverness, FL 34453", "5350 Spring Hill Dr. Spring Hill, FL 34606", "5350 Spring Hill Dr. Spring Hill, FL 34606", "8172 Chaucer Dr. Weeki Wachee, FL 34607", "7633 Cita Ln. New Port Richey, FL 34653", "6475 Oregon Jay Rd. Weeki Wachee, FL 34613" };
        #endregion

        private static TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

        /// <summary>
        /// Static Type <seealso cref="Random"/> object Reference
        /// </summary>
        private static Random r = new Random();

        /// <summary>
        /// Return Random String for dumy data
        /// </summary>
        /// <param name="length">Length of string</param>
        /// <returns>string, random</returns>
        private static string RandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[r.Next(s.Length)]).ToArray());
        }

        #region Dynamic JSON Data Methods

        #region Get All Providers For Provider Directory

        /// <summary>
        /// Return List Of Provider Object
        /// </summary>
        /// <param name="providerCount">Required Provider Count</param>
        /// <returns>object, Provider List</returns>
        public static List<object> GetProviders(int providerCount)
        {
            List<object> providers = new List<object>();
            string[] genders = { "F", "M" };
            string[] groups = { "Access", "Access2", "MIRRA", "ACO" };
            string[] LOBs = { "HMO", "ACO", "FFS", "Hospitalist" };

            for (int i = 0; i < providerCount; i++)
            {
                int index = r.Next(0, 107);
                string name = providerNames[index];

                object provider = new
                {
                    Salutation = "Dr.",
                    Name = textInfo.ToTitleCase(name.ToLower()),
                    Email = name.Replace(" ", String.Empty).ToLower() + "@ahcp.com",
                    Specialty = textInfo.ToTitleCase(specialties[index].ToLower()),
                    NPI = r.Next(1000, 9999).ToString() + r.Next(1000, 9999).ToString() + r.Next(10, 99),
                    PrimaryPracticeLocation = primaryPracticLocations[index],
                    Groups = groups[r.Next(0, 4)],
                    Plan = "Humana",
                    LOB = LOBs[r.Next(0, 4)] + ", "+ LOBs[r.Next(0, 4)],
                    DateOfBirth = new DateTime(r.Next(1960, 2000), r.Next(1, 11), r.Next(1, 28)),
                    Gender = genders[r.Next(0, 2)]
                };

                providers.Add(provider);
            }
            return providers;
        }
        #endregion

        #region Get All Addresses

        /// <summary>
        /// Return List Of Address Object
        /// </summary>
        /// <param name="addressCount">Required Address Count</param>
        /// <returns>object, Address List</returns>
        public static List<object> GetAddresses(int addressCount)
        {
            List<object> addresses = new List<object>();
            for (int i = 0; i < addressCount; i++)
            {
                object address = new
                {
                    AddressID = i + 1,
                    City = RandomString(r.Next(4, 15)) + " " + RandomString(r.Next(4, 15)),
                    Country = RandomString(r.Next(4, 10)),
                    County = RandomString(r.Next(4, 10)),
                    State = RandomString(r.Next(4, 10)),
                    Street = "#" + r.Next(4, 200) + RandomString(r.Next(4, 15)) + " " + RandomString(r.Next(4, 15)),
                    UnitNumber = RandomString(r.Next(4, 5)) + r.Next(4, 200),
                    ZipCode = r.Next(1000, 9999).ToString()
                };
                addresses.Add(address);
            }
            return addresses;
        }
        #endregion

        #region Get All Contacts

        /// <summary>
        /// Return List Of Contact Object
        /// </summary>
        /// <param name="contactCount">Required Contacts Count</param>
        /// <returns>object, Contact List</returns>
        public static List<object> GetContacts(int contactCount)
        {
            List<string> contactTypes = new List<string>() { "Phone", "EMail", "Fax" };
            List<object> contacts = new List<object>();

            for (int j = 0; j < contactCount; j++)
            {
                object contact = new
                {
                    ContactID = j + 1,
                    ContactName = RandomString(r.Next(4, 15)) + " " + RandomString(r.Next(4, 15)),
                    ContactType = contactTypes[r.Next(0, 2)],
                    PhoneNumber = "+1-" + r.Next(78000, 99999).ToString() + r.Next(00000, 99999).ToString()
                };
                contacts.Add(contact);
            }
            return contacts;
        }

        #endregion

        #region Get Profile Links

        /// <summary>
        /// Return List Of ProfileLinks Object
        /// </summary>
        /// <returns>object, ProfileLinks</returns>
        public static object GetProfileLinks()
        {
            object profile = null;

            int flag = r.Next(0, 2);
            if (flag == 1 || flag == 2)
            {
                profile = new
                {
                    //ProfileID = 1,
                    FacebookURL = "http://www.facebook.com/" + RandomString(r.Next(4, 15)),
                    LinkedInURL = "http://www.linkedin.com/" + RandomString(r.Next(4, 15)),
                    TwitterURL = "http://www.twitter.com/" + RandomString(r.Next(4, 15))
                };
            }
            return profile;
        }
        #endregion

        #region Get All Data For CCO and Provider Summary

        /// <summary>
        /// Return List Of Provider Object
        /// </summary>
        /// <param name="providerCount">Required Provider Count</param>
        /// <returns>object, Provider List</returns>
        public static List<object> GetProvidersSummary(int providerCount, string profileStatus)
        {
            List<object> providers = new List<object>();
            string[] genders = { "F", "M" };
            string[] groups = { "Access", "Access2", "MIRRA", "ACO" };
            string[] LOBs = { "HMO", "ACO", "FFS", "Hospitalist" };
            string[] providerCategory = { "New", "Old" };

            for (int i = 0; i < providerCount; i++)
            {
                int index = r.Next(0, 107);
                int ccoIndex = r.Next(0, 107);
                string name = providerNames[index];
                string CCOName = providerNames[ccoIndex];

                object provider = new
                {
                    Salutation = "Dr.",
                    Name = textInfo.ToTitleCase(name.ToLower()),
                    Email = name.Replace(" ", String.Empty).ToLower() + "@ahcp.com",
                    Specialty = textInfo.ToTitleCase(specialties[index].ToLower()),
                    NPI = r.Next(1000, 9999).ToString() + r.Next(1000, 9999).ToString() + r.Next(10, 99),
                    PrimaryPracticeLocation = primaryPracticLocations[index],
                    Groups = groups[r.Next(0, 4)],
                    Plan = "Humana",
                    LOB = LOBs[r.Next(0, 4)] + ", " + LOBs[r.Next(0, 4)],
                    DateOfBirth = new DateTime(r.Next(1960, 2000), r.Next(1, 11), r.Next(1, 28)),
                    Gender = genders[r.Next(0, 2)],
                    CCO = textInfo.ToTitleCase(CCOName.ToLower()),
                    ProviderCategory = providerCategory[r.Next(0, 2)],
                    ProfileStatus = profileStatus == "true" ? 100 : (r.Next(0, 100)),                   
                    CredentialingCompletedStatus = (r.Next(10, 20)),
                    CredentialingIncompletedStatus = (r.Next(0, 5))
                };

                providers.Add(provider);
            }
            return providers;
        }
        #endregion

        #endregion

    }
}