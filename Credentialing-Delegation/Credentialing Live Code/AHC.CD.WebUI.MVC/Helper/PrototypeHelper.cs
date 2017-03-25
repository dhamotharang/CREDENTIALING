using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

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
        private static string[] licenseTypeNames = { "State License", "Federal DEA", "CDS Information", "Specialty/Board", "Hospital Privileges", "Professional Liability", "Worker Compensation", "Medicare Information", "Medicaid Information", "CAQH", "UpComing Recredentials" };
        private static string[] licensetypeCodes = { "StateLicense", "FederalDEA", "CDSInformation", "SpecialityBoard", "HospitalPrivilages", "ProfessionalLiability", "WorkerCompensation", "MedicareInformation", "MedicaidInformation", "CAQHExpiries", "UpComingRecredentials" };
        private static string[] ccoNames = { "Keishla", "Elizabeth Duffin", "lcrego", "Jfish", "Nedjie Pierre", "Nikeesha Khobani", "Lenora", "kurt Carriveau", "Jocelyn Mejia", "Ms. Keyla Nunez", "Lex Harris", "Ankit Garg", "Jennifer Nichol", "CATHERINE MEDEL", "Barbara Joy", "Angela Atehortua", "Anirudh Chakravartty", "Dana Chorvat", "Jeanine Martin", "Cristina Mule", "Kimberly Harris-Williams", "Rohit Ahooja", "Phil" };
        private static string[] usStates = { "Alabama", "Alaska", "American Samoa", "Arizona", "Arkansas", "California", "Colorado", "Connecticut", "Delaware", "District Of Columbia", "Federated States Of Micronesia", "Florida", "Georgia", "Guam", "Hawaii", "Idaho", "Illinois", "Indiana", "Iowa", "Kansas", "Kentucky", "Louisiana", "Maine", "Marshall Islands", "Maryland", "Massachusetts", "Michigan", "Minnesota", "Mississippi", "Missouri", "Montana", "Nebraska", "Nevada", "New Hampshire", "New Jersey", "New Mexico", "New York", "North Carolina", "North Dakota", "Northern Mariana Islands", "Ohio", "Oklahoma", "Oregon", "Palau", "Pennsylvania", "Puerto Rico", "Rhode Island", "South Carolina", "South Dakota", "Tennessee", "Texas", "Utah", "Vermont", "Virgin Islands", "Virginia", "Washington", "West Virginia", "Wisconsin", "Wyoming" };
        private static string[] specialtyBoards = { "Board of Certification in Anesthesiology", "Board of Certification in Dermatology", "Board of Certification in Diagnostic Radiology", "American Board of Disaster Medicine", "Board of Certification in Emergency Medicine", "American Board of Family Medicine Obstetrics", "Board of Certification in Family Practice", "Board of Certification in Geriatric Medicine", "American Board of Hospital Medicine", "Board of Certification in Internal Medicine", "Board of Certification in Obstetrics and Gynecology", "Board of Certification in Ophthalmology", "Board of Certification in Orthopedic Surgery", "Board of Certification in Psychiatry", "Board of Certification in Radiation Oncology", "Board of Certification in Surgery" };
        private static string[] usaHospitals = { "Mayo Clinic, Rochester, Minn.", "Cleveland Clinic", "Massachusetts General Hospital, Boston", "Johns Hopkins Hospital, Baltimore", "UCLA Medical Center", "New York-Presbyterian University Hospital of Columbia and Cornell", "UCSF Medical Center, San Francisco", "Northwestern Memorial Hospital, Chicago", "Hospitals of the University of Pennsylvania-Penn Presbyterian, Philadelphia", "NYU Langone Medical Center", "Barnes-Jewish Hospital/Washington University, St. Louis", "UPMC Presbyterian Shadyside, Pittsburgh", "Brigham and Women's Hospital, Boston", "Stanford Health Care-Stanford Hospital, Stanford, Calif.", "Mount Sinai Hospital, New York", "Duke University Hospital, Durham, N.C.", "Cedars-Sinai Medical Center, Los Angeles", "University of Michigan Hospitals and Health Centers, Ann Arbor", "Houston Methodist Hospital", "University of Colorado Hospital, Aurora" };
        private static string[] usaInsuranceCarriers = { "Unitedhealth Group", "Wellpoint Inc. Group", "Kaiser Foundation Group", "Humana Group", "Aetna Group", "HCSC Group", "Cigna Health Group", "Highmark Group", "Coventry Corp. Group", "HIP Insurance Group", "Independence Blue Cross Group", "Blue Cross Blue Shield of New Jersey Group", "Blue Cross Blue Shield of Michigan Group", "California Physicians' Service", "Blue Cross Blue Shield of Florida Group", "Health Net of California, Inc.", "Centene Corp. Group", "Carefirst Inc. Group", "Wellcare Group", "Blue Cross Blue Shield of Massachusetts Group", "UHC of California", "Lifetime Healthcare Group", "Cambia Health Solutions Inc.", "Metropolitan Group", "Molina Healthcare Inc. Group" };
        private static string[] plans = { "Humana", "Wellcare", "Freedom", "Ultimate", "Atena", "Medicare", "BlueCross" };
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
            string[] Status = { "Profile Management", "Credentialing Initiation", "Submited Form" };

            for (int i = 0; i < providerCount; i++)
            {
                int index = r.Next(0, 107);
                string name = providerNames[index];

                object provider = new
                {
                    ProviderID = i + 1,
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
                    Status = Status[r.Next(0, 3)],
                    AssignedCCO = ccoNames[r.Next(0, 23)],
                    AddedDays = r.Next(1, 40)
                };

                providers.Add(provider);
            }
            return providers;
        }
        #endregion

        #region Get All Specialties

        /// <summary>
        /// Return List Of Specialty Object
        /// </summary>
        /// <returns>object, Specialty List</returns>
        public static List<string> GetSpecialties()
        {
            var specialties1 = specialties.ToList().Distinct();
            List<string> specialties2 = new List<string>();
            foreach (var str in specialties1)
            {
                specialties2.Add(textInfo.ToTitleCase(str.ToLower()));

            }
            return specialties2;
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
                int ccoIndex = r.Next(0, 23);
                string name = providerNames[index];
                string CCOName = ccoNames[i];
                string category = providerCategory[r.Next(0, 2)];

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
                    ProviderCategory = category,
                    ProfileStatus = profileStatus == "true" ? 100 : (r.Next(0, 101)),
                    CredentialingCompletedStatus = category == "New"? 0 : (r.Next(10, 20)),
                    CredentialingIncompletedStatus = (r.Next(1, 5))
                };

                providers.Add(provider);
            }
            return providers;
        }
        #endregion

        #region Get All CCO Data
        public static List<object> GetCCOList()
        {
            int index = r.Next(0, 107);

            int ccoIndex = r.Next(0, 107);
            List<object> CCOData = new List<object>();
            Parallel.ForEach(ccoNames, (name) =>
            {
                object cco = new
                {
                    Name = name,
                    ProAssigned = r.Next(5, 20),
                    TaskAssigned = r.Next(79, 250),
                    Pending = r.Next(5, 80),
                    Performance = 0
                };
                CCOData.Add(cco);
            });

            return CCOData;
        }
        #endregion

        #region Get data for CCO Summary

        /// <summary>
        /// Return List Of data for CCO Summary
        /// </summary>
        /// <param name="ccoCount">Required cco Count</param>
        /// <returns>object, CCO List</returns>
        public static List<object> GetCCOSummary(int ccoCount)
        {
            List<object> ccolist = new List<object>();
            List<object> PendingCredentialList = new List<object>();
            List<object> ProviderList = new List<object>();

            string[] genders = { "F", "M" };
            string[] groups = { "Access", "Access2", "MIRRA", "ACO" };
            string[] LOBs = { "HMO", "ACO", "FFS", "Hospitalist" };
            string[] providerCategory = { "New", "Old" };
            string[] imageURLs = { "/Content/Images/Providers/provider3.jpg", "/Content/Images/Providers/provider2.jpg", "/Content/Images/Providers/Pariksith_Singh.jpg", "/Content/Images/Providers/provider1.jpg" };
            string[] credentialingPhase = { "Initiated", "Plan Enrollment", "Submit to plan", "PSV completed", "Package Generated" };
            string[] credStatus = { "Waiting for plan form", "Completed", "Waiting for plan announcement", "PSV failed", "Waiting for document from provider" };

            for (int j = 0; j < 6; j++)
            {
                int index = r.Next(0, 107);
                string name = providerNames[index];
                object pendingCred = new
                {
                    Plan = plans[r.Next(0, 7)],
                    StartDate = (new DateTime(r.Next(1999, 2017), r.Next(1, 11), r.Next(1, 28))).ToString("MM-dd-yyyy"),
                    InitiatedTo = textInfo.ToTitleCase(name.ToLower()),
                    PhaseOfCredentialing = credentialingPhase[r.Next(0, 5)],
                    CredentialingStatus = credStatus[r.Next(0, 5)],
                    DaysPendingFrom = r.Next(0, 300),
                    AvgTimeToComplete = r.Next(0, 30)
                };
                PendingCredentialList.Add(pendingCred);
            }

            for (int k = 0; k < 7; k++)
            {
                //Data for Providers Worked On to get details of providers
                int index = r.Next(0, 107);
                string name = providerNames[index];
                object provider = new
                {
                    ProfilePicURL = imageURLs[r.Next(0, 4)],
                    Salutation = "Dr.",
                    ProviderName = textInfo.ToTitleCase(name.ToLower()),
                    NPI = r.Next(1000, 9999).ToString() + r.Next(1000, 9999).ToString() + r.Next(10, 99),
                    PrimaryPracticeLocation = primaryPracticLocations[index],
                    Specialty = textInfo.ToTitleCase(specialties[index].ToLower()),
                    ProviderCategory = providerCategory[r.Next(0, 2)] // for New or Old provider
                };
                ProviderList.Add(provider);

            }

            for (int i = 0; i < ccoCount; i++)
            {
                int index = r.Next(0, 107);
                int ccoIndex = r.Next(0, 23);
                string name = providerNames[index];
                string nameOfCCO = ccoNames[i];

                var cco = new
                {
                    //CCO Summary
                    CCOName = textInfo.ToTitleCase(nameOfCCO.ToLower()),
                    ProviderWorkedOn = (r.Next(1, 20)),
                    TasksPerformed = (r.Next(1, 100)),
                    ApplicationsSubmitted = (r.Next(1, 100)),
                    PendingCredentialing = (r.Next(1, 20)),
                    CompletedCredentialing = (r.Next(1, 20)),
                    ActivitiesPErformed = (r.Next(1, 200)),
                    PendingCredentialingDetails = PendingCredentialList,
                    ProviderDetails = ProviderList
                };



                ccolist.Add(cco);
            }
            return ccolist;
        }

        #endregion

        #region Get Expiry License Details

        /// <summary>
        /// Get License Data
        /// </summary>
        /// <returns></returns>
        public static List<object> GetLicenseData()
        {
            List<object> licenses = new List<object>();

            for (int i = 0; i < licenseTypeNames.Count(); i++)
            {
                int vl = r.Next(0, 80);
                int pdllt90 = r.Next(0, 80);
                int pdllt60 = r.Next(0, 80);
                int pdllt30 = r.Next(0, 80);
                int el = r.Next(0, 80);

                object license = new
                {
                    LicenseType = licenseTypeNames[i],
                    LicenseTypeCode = licensetypeCodes[i],
                    Licenses = new string[0],
                    LicenseStatus = new
                    {
                        ValidLicense = vl,
                        PendingDaylicenseLessThan90 = pdllt90,
                        PendingDaylicenseLessThan60 = pdllt60,
                        PendingDaylicenseLessThan30 = pdllt30,
                        ExpiredLicense = el,
                    },
                    TotalLicenses = (vl + pdllt90 + pdllt60 + pdllt30 + el),
                };
                licenses.Add(license);
            }
            return licenses;
        }

        #endregion

        #region Get Expiry License Details Of Provider

        /// <summary>
        /// Get Provider licenses Information list
        /// </summary>
        /// <param name="providerCount">Number Of Provider Need</param>
        /// <param name="minDaysLeft">Min days left</param>
        /// <param name="maxDaysLeft">max days left</param>
        /// <param name="licenseTypeCode">license type code</param>
        /// <returns></returns>
        public static List<object> GetProvidersLicenseInformation(int providerCount, int minDaysLeft, int maxDaysLeft, string licenseTypeCode)
        {
            List<object> providers = new List<object>();
            string[] genders = { "F", "M" };
            string[] groups = { "Access", "Access2", "MIRRA", "ACO" };
            string[] LOBs = { "HMO", "ACO", "FFS", "Hospitalist" };
            string[] status = { "Active", "Inactive" };
            string[] relationship = { "Employee", "Affiliate" };

            for (int i = 0; i < providerCount; i++)
            {
                int index = r.Next(0, 107);
                string name = providerNames[index];
                int day = r.Next(minDaysLeft, maxDaysLeft);

                object provider = new
                {
                    ProviderID = i + 1,
                    NPI = r.Next(1000, 9999).ToString() + r.Next(1000, 9999).ToString() + r.Next(10, 99),
                    Relationship = relationship[r.Next(0, 2)],
                    Name = textInfo.ToTitleCase(name.ToLower()),
                    LicenseNumber = r.Next(100, 999).ToString() + r.Next(100, 999).ToString() + r.Next(10, 99),
                    IssueState = usStates[r.Next(0, 59)],
                    Status = status[r.Next(0, 2)],
                    ExpiryDate = DateTime.Now.AddDays(day).ToString("MM/dd/yyyy"),
                    NextAttestationDate = DateTime.Now.AddDays(r.Next(10, 50)).ToString("MM/dd/yyyy"),
                    DaysLeft = day,
                    SpecialtyBoardName = specialtyBoards[r.Next(0, 16)],
                    HospitalName = usaHospitals[r.Next(0, 20)],
                    InsuranceCarrier = usaInsuranceCarriers[r.Next(0, 25)],
                    Plan = "Humana",
                    LOB = LOBs[r.Next(0, 4)],
                    GroupID = groups[r.Next(0, 4)],
                };

                providers.Add(provider);
            }
            return providers;
        }

        #endregion

        #region Get CCO Rank Data

        /// <summary>
        /// Get CCO Rank For Management Dashboard
        /// </summary>
        /// <param name="CCOCount">Required Number Of CCO Counts</param>
        /// <returns>List, CCO Rank Object List</returns>
        public static List<object> GetCCORanks(int CCOCount)
        {
            List<object> CCORanks = new List<object>();
            double[] avgTimes = { 5, 5.3, 5.5, 6, 6, 4.9, 6.2, 5.2, 5.6 };
            int[] taskCompleted = { 6, 4, 4, 5, 6, 6, 5, 5, 4 };

            for (int i = 0; i < CCOCount; i++)
            {
                string name = ccoNames[i];

                object cco = new
                {
                    CCOID = i + 1,
                    Salutation = "Dr.",
                    Name = textInfo.ToTitleCase(name.ToLower()),
                    Email = name.Replace(" ", String.Empty).ToLower() + "@ahcp.com",
                    AvgTime = avgTimes[i],
                    TaskCompleted = taskCompleted[i]
                };

                CCORanks.Add(cco);
            }
            return CCORanks;
        }

        #endregion

        #region Get CCO Report Data

        /// <summary>
        /// Get CCO Report For Management Dashboard
        /// </summary>
        /// <param name="CCOCount">Required Number Of CCO Counts</param>
        /// <returns>List, CCO Report List</returns>
        public static List<object> GetCCOReports(int CCOCount)
        {
            List<object> CCOReports = new List<object>();

            for (int i = 0; i < CCOCount; i++)
            {
                string name = ccoNames[i];

                object report = new
                {
                    CCOID = i + 1,
                    Salutation = "Dr.",
                    Name = textInfo.ToTitleCase(name.ToLower()),
                    Email = name.Replace(" ", String.Empty).ToLower() + "@ahcp.com",
                    ProvidersCount = r.Next(2, 12)
                };

                CCOReports.Add(report);
            }
            return CCOReports;
        }

        #endregion

        #region Get Profile Completion Data Counts For Management Dashboard

        /// <summary>
        /// Get Profile Completion Data Counts For Management Dashboard
        /// </summary>
        /// <returns>List, Object</returns>
        public static List<object> GetProfileCompletionDataCount()
        {
            List<object> data = new List<object>();
            string[] legends = { "< 20%", "20% - 50%", "50% - 80%", "> 80%", "= 100%" };
            int[] Counts = { 37, 43, 69, 56, 37 };

            for (int i = 0; i < 5; i++)
            {
                object obj = new
                {
                    Label = legends[i],
                    ProviderCounts = Counts[i]
                };
                data.Add(obj);
            }
            return data;
        }

        #endregion

        #region GetTLData
        internal static List<object> GetTLData()
        {
            int index = r.Next(0, 107);
            string[] genders = { "F", "M" };
            int tlIndex = r.Next(0, 107);
            string[] tlNames = { "JHON J GREY", "Ted S Koch", "Steven L Cordell", "Joe R Harris", "William Corcoran", "Mike W Goodin", "Marie S Davis", "Susan J Jackson", "Judith C Streater" };
            List<object> tlData = new List<object>();
            Parallel.ForEach(tlNames, (name) =>
            {
                object tl = new
                {
                    Name = name,
                    ProAssigned = r.Next(5, 20),
                    EMail = name.Replace(" ", "").ToLower() + "@ahcpllc.com",
                    Gender = genders[r.Next(0, 2)],
                    Phone = "+1-" + r.Next(78000, 99999).ToString() + r.Next(00000, 99999).ToString(),

                };
                tlData.Add(tl);
            });

            return tlData;
        }

        #endregion

        #region Get Provider Person Details

        /// <summary>
        /// Provider Personal Details
        /// </summary>
        /// <param name="ProfileID">Profile ID Of Provider</param>
        /// <returns>object, Provider Personal Details</returns>
        public static object GetProviderPersonalDetails(int ProfileID)
        {
            List<object> providers = new List<object>();

            string[] trueFalse = { "YES", "NO" };
            string[] LOBs = { "HMO", "ACO", "FFS", "Hospitalist" };
            string[] status = { "Active", "Inactive", "Completed", "Incompleted", "Credentialing Initiation" };

            int index = r.Next(0, 107);
            string name = providerNames[index];

            object provider = new
            {
                ProviderID = r.Next(1, 80),
                NPI = r.Next(1000, 9999).ToString() + r.Next(1000, 9999).ToString() + r.Next(10, 99),
                Name = textInfo.ToTitleCase(name.ToLower()),
                ProviderType = "Medical director",
                Specialty = textInfo.ToTitleCase(specialties[index].ToLower()),
                LOB = LOBs[r.Next(0, 4)] + ", " + LOBs[r.Next(0, 4)],
                BoardCertified = trueFalse[r.Next(0, 2)],
                BoardName = specialtyBoards[r.Next(0, 16)],
                PhoneNumber = "+1-" + r.Next(100, 999) + "-" + r.Next(100, 999) + " " + r.Next(1000, 9999),
                ProfileStatus = new
                {
                    LastPSVDate = new DateTime(r.Next(2005, 2016), r.Next(1, 11), r.Next(1, 28)).ToString("MM/dd/yyyy"),
                    Status = status[r.Next(0, 4)],
                    ActiveContracts = r.Next(2, 50),
                    InactiveContracts = r.Next(2, 50),
                }
            };

            return provider;
        }

        #endregion

        #region Get Task Status of Provider

        /// <summary>
        /// Provider Recent Task Performed
        /// </summary>
        /// <param name="taskCount">Number Of Task Counts</param>
        /// <param name="ProfileID">Profile ID Of Provider</param>
        /// <returns>List Object, Task Performed for Provider</returns>
        public static List<object> GetProviderTasks(int taskCount, int ProfileID)
        {
            List<object> tasks = new List<object>();
            string[] taskTitles = { "UPDATED DEA INFORMATION", "ADDED NEW CONTRACT WITH AETNA PLAN", "UPDATED CONTRACT GRID", "INITIATED RE-CREDENTIALING FOR WELCARE" };
            string[] taskDescriptions = { "Recently on demand of the provider, updated the DEA number of the provider.", "The provider got a new contrat with AETNA on his location.", "After the confirmation with the plan, updated the contract gird.", "Initiated new credentialing for the plan WELCARE." };

            for (int i = 0; i < taskCount; i++)
            {
                int index = r.Next(0, 4);

                object task = new
                {
                    TaskID = r.Next(1, 80),
                    ActionDate = new DateTime(r.Next(2005, 2016), r.Next(1, 11), r.Next(1, 28)).ToString("MM/dd/yyyy"),
                    Title = textInfo.ToTitleCase(taskTitles[index].ToLower()),
                    Description = textInfo.ToTitleCase(taskDescriptions[index].ToLower()),
                };
                tasks.Add(task);
            }

            return tasks;
        }

        #endregion

        #region Get Credentialing Details of Provider

        /// <summary>
        /// Provider Credentialing Details
        /// </summary>
        /// <param name="count">Number Of Credentialing</param>
        /// <param name="ProfileID">Profile ID Of Provider</param>
        /// <returns>List Object, Provider Credentialing Details</returns>
        public static List<object> GetCredentialingDetails(int count, int ProfileID)
        {
            List<object> creds = new List<object>();

            for (int i = 0; i < count; i++)
            {
                string LocationType = "Secondary";
                if (i == 0)
                    LocationType = "Primary";

                object cred = new
                {
                    CredentialID = r.Next(1, 80),
                    LocationType = LocationType,
                    Location = textInfo.ToTitleCase(primaryPracticLocations[r.Next(0, 107)].ToLower()),
                    Specilaty = textInfo.ToTitleCase(specialties[r.Next(0, 107)].ToLower()),
                    AgeLimit = r.Next(0, 20) + "-" + r.Next(30, 60) + " Years",
                    OfficeHours = new List<object> {
                        new {
                            Day = new DateTime(r.Next(2005, 2016), r.Next(1, 11), r.Next(1, 28)).DayOfWeek.ToString(),
                            Timing = "9:00AM - 3:00PM"
                        },
                        new {
                            Day = new DateTime(r.Next(2005, 2016), r.Next(1, 11), r.Next(1, 28)).DayOfWeek.ToString(),
                            Timing = "11:00AM - 5:00PM"
                        }
                    },
                    Plans = new List<string> { plans[r.Next(0, 2)], plans[r.Next(2, 4)], plans[r.Next(5, 7)], plans[r.Next(3, 7)] },
                };
                creds.Add(cred);
            }

            return creds;
        }

        #endregion

        #region Get Hospital List of Provider

        /// <summary>
        /// Provider Hospitals
        /// </summary>
        /// <param name="count">Number Of Hospitals</param>
        /// <param name="ProfileID">Profile ID Of Provider</param>
        /// <returns>List Object, Provider Hospitals</returns>
        public static List<string> GetProviderHospitals(int count, int ProfileID)
        {
            List<string> hospitals = new List<string>();

            for (int i = 0; i < count; i++)
            {
                hospitals.Add(usaHospitals[r.Next(0, 20)]);
            }
            return hospitals;
        }

        #endregion

        #region Get IPA/Group of Provider

        /// <summary>
        /// Provider IPA/Groups
        /// </summary>
        /// <param name="count">Number Of IPA/Groups</param>
        /// <param name="ProfileID">Profile ID Of Provider</param>
        /// <returns>List Object, Provider Hospitals</returns>
        public static List<string> GetProviderGroups(int count, int ProfileID)
        {
            List<string> groups = new List<string>();
            string[] localGroups = { "Champion State of mind", "Access", "Primecare", "FIVE POINT MD CENTER", "ACCESS 2 HEALTHCARE", "MIRRA", "ACO" };

            for (int i = 0; i < count; i++)
            {
                groups.Add(localGroups[r.Next(0, 7)]);
            }
            return groups;
        }

        #endregion

        #region Helper Protected

        private static string GetLOBs(int count)
        {
            string[] LOBs = { "HMO", "ACO", "FFS", "Hospitalist" };
            string LOBsString = "";
            for (int i = 0; i < count; i++)
            {
                //LOBsString += 
            }

            return LOBsString;
        }

        #endregion

        #endregion
    }
}