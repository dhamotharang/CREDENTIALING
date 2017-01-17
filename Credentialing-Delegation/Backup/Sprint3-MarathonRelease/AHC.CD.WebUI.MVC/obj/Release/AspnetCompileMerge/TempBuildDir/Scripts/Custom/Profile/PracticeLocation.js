
$(document).ready(function () {
    $(".MultiSelect").select2({ placeholder: "Select option" });
});

profileApp.controller('locationcltr', function ($scope, $http, dynamicFormGenerateService) {

    $scope.answer = "Yes";
    $scope.PracticeExclusively = "No";
    $scope.PrimaryPracticeLocations = [{
        Type: "Primary",
        CurrentlyPracticing: "Yes",
        ExpectedStartDate: "",
        PracticeName: "Access Health Care Physicians LLC",
        Telephone: "352-688-8116",
        Fax: "352-686-9477",
        EmailId: "-",
        GeneralCorrespondence: "Yes",
        Number: "5350",
        Street: "Spring Hill DR",
        Suite: "",
        County: "Hernando",
        Country: "USA",
        State: "Florida",
        City: "Spring Hill",
        ZIP: "346064562",
        IndividualTaxID: "59-3682760",
        GroupTaxID: "45-1444883",
        Primary: "Use group tax ID",



        OfficeStaffContact: {
            PracticeLocation: "",
            First: "Diane",
            Middle: "",
            Last: "Burkhardt",
            Telephone: "3526888116",
            Fax: "3526869477",
            MailAddress: "dburkhardt@accesshealthcarellc.net",
        },


        OfficeHours: {
            practicelocation: "",
            Day: "",
            StartTime: "8:30",
            StartAMPM: "AM",
            EndTime: "3:30",
            EndAMPM: "PM",
            PhoneCoverage: "Yes",
            BackOfficeTelephone: "3526669912",
        },

        OpenPracticeStatus: {
            PracticeLocation: "",
            newpatients: "Yes",
            AllNewPatients: "Yes",
            ExistingPatients: "Yes",
            Medicare: "No",
            PhysicianReferral: "Yes",
            Medicaid: "Yes",
            Informationvaries: "No longer accepting patients with Medicaid and Quality Health Plans",
            Limitations: "No",
            Gender: "None",
            Minimum: "12",
            Maximum: "112",
            OtherLimitations: "",

        },


        BillingContact: [
            {
                Location: "",
                First: "Ann",
                Middle: "",
                Last: "Marie",
                Number: "",
                Street: "",
                Suite: "",
                County: "",
                Country: "USA",
                State: "OHIO",
                City: "Cincinnati",
                ZIP: "452636233",
                BoxAddress: "P.O Box 636233",
                Telephone: "352-799-0046",
                Fax: "352-799-0042",
                MailAddress: ""
                
            }
        ],

        PaymentandRemittance: [
            {
                BillingCapabilities: "Yes",
                BillingDepartment: "",
                Check: "Access Health Care Physicians LLC",
                Office: "" , 
                Location: "",
                First: "Val",
                Middle: "",
                Last: "Patel",
                Number: "",
                Street: "P.O Box 636233",
                Suite: "",
                State: "OHIO",
                City: "Cincinnati",
                ZIP: "45263",
                Telephone: "3525934101",
                Fax: "3525934994",
                MailAddress: "valp@medenet.net",
            }
        ],


        Accessibilities: {
            PracticeLocation: "",
            ADA: "Yes",
            Building: "Yes",
            Parking: "Yes",
            Restroom: "Yes",
            Handicapped: "Yes",
            OtherServices: "No",
            TTY: "No",
            SingLanguage: "No",
            Impairment: "No",
            Transportation: "Yes",
            Bus: "Yes",
            Subway: "No",
            Train: "No",
            OtherHandicapped: "handicapped",
            OtherDisability: "disability",
            OtherTransportation: "Taxi",
        },


        Services: {
            PracticeLocation: "",
            LaboratoryServices: "Yes",
            Accrediting: "",
            RadiologyServices: "No",
            XRay: "",
            EKGS: "Yes",
            AllergyInjection: "Yes",
            SkinTesting: "No",
            Gynecology: "Yes",
            Blood: "Yes",
            Immunization: "No",
            Sigmoidoscopy: "No",
            Tympanometry: "No",
            Asthma: "Yes",
            Osteopathic: "No",
            Hydration: "Yes",
            Cardiac: "Yes",
            Pulmonary: "Yes",
            Physical: "No",
            Lacerations: "Yes",
            Anesthesia: "No",
            Category: "",
            FirstName: "",
            LastName: "",
            TypeOfPractice: "Multi-Specialty Group",
            AdditionalOffice: "",
        },


        Partners: [
            {
                Location: "",
                First: "Maria",
                Middle: "",
                Last: "Scunziano MD",
                Number: "",
                Street: "",
                Suite: "",
                State: "",
                City: "",
                ZIP: "",
                Phonenumber: "",
                Specialty: "Internal Medicine",
                ProviderType: "MD",
            }
        ],

        MidLevel: [
            {
                PratitionerName: "",
                MidLevelPractitioners: "Yes",
                PractitionerFirstName: "Sharona",
                PractitionerMiddleName: "",
                PractitionerLastName: "Lowenstein",
                PractitionerLicense: "PA9103282",
                PractitionerState: "Florida",
                PractitionerType: "PA",

            }
        ]
    },



    {
        Type: "Secondary",
        CurrentlyPracticing: "Yes",
        ExpectedStartDate: "",
        PracticeName: "Access Health Care Physicians LLC",
        Telephone: "727-378-8588",
        Fax: "727-857-5991",
        EmailId: "-",
        GeneralCorrespondence: "",
        Number: "15120",
        Street: "County Line Rd Ste",
        Suite: "",
        County: "Hernando, FL",
        Country: "USA",
        State: "Florida",
        City: "Spring Hill",
        ZIP: "346106726",
        IndividualTaxID: "59-3682760",
        GroupTaxID: "45-1444883",
        Primary: "Use group tax ID",



        OfficeStaffContact: {
            PracticeLocation: "",
            First: "Traci",
            Middle: "",
            Last: "Brasky",
            Telephone: "",
            Fax: "",
            MailAddress: "tbrasky@accesshealthcarellc.net",

        },


        OfficeHours: {
            practicelocation: "",
            Day: "",
            StartTime: "8:30",
            StartAMPM: "AM",
            EndTime: "3:30",
            EndAMPM: "PM",
            PhoneCoverage: "Yes",
            BackOfficeTelephone: "3526669912",
        },

        OpenPracticeStatus: {
            PracticeLocation: "",
            newpatients: "Yes",
            AllNewPatients: "Yes",
            ExistingPatients: "Yes",
            Medicare: "No",
            PhysicianReferral: "Yes",
            Medicaid: "Yes",
            Informationvaries: "No longer accepting patients with Medicaid and Quality Health Plans",
            Limitations: "No",
            Gender: "None",
            Minimum: "12",
            Maximum: "112",
            OtherLimitations: "",

        },


        BillingContact: [
            {
                Location: "",
                First: "Ann",
                Middle: "",
                Last: "Marie",
                Number: "",
                Street: "",
                Suite: "",
                County: "",
                Country: "USA",
                State: "OHIO",
                City: "Cincinnati",
                ZIP: "452636233",
                BoxAddress: "P.O Box 636233",
                Telephone: "352-799-0046",
                Fax: "352-799-0042",
                MailAddress: ""
              
            }
        ],

        PaymentandRemittance: [
            {
                BillingCapabilities: "Yes",
                BillingDepartment: "",
                Check: "Access Health Care Physicians LLC",
                Office: "" ,
                Location: "",
                First: "Val",
                Middle: "",
                Last: "Patel",
                Number: "",
                Street: "P.O Box 636233",
                Suite: "",
                State: "OH",
                City: "Cincinnati",
                ZIP: "45263",
                Telephone: "3525934101",
                Fax: "3525934994",
                MailAddress: "valp@medinet.net",
            }
        ],


        Accessibilities: {
            PracticeLocation: "",
            ADA: "Yes",
            Building: "Yes",
            Parking: "Yes",
            Restroom: "Yes",
            Handicapped: "Yes",
            OtherServices: "No",
            TTY: "No",
            SingLanguage: "No",
            Impairment: "No",
            Transportation: "Yes",
            Bus: "Yes",
            Subway: "No",
            Train: "No",
            OtherHandicapped: "handicapped",
            OtherDisability: "disability",
            OtherTransportation: "Taxi",
        },


        Services: {
            PracticeLocation: "",
            LaboratoryServices: "Yes",
            Accrediting: "",
            RadiologyServices: "No",
            XRay: "",
            EKGS: "Yes",
            AllergyInjection: "Yes",
            SkinTesting: "No",
            Gynecology: "Yes",
            Blood: "Yes",
            Immunization: "No",
            Sigmoidoscopy: "No",
            Tympanometry: "No",
            Asthma: "Yes",
            Osteopathic: "No",
            Hydration: "Yes",
            Cardiac: "Yes",
            Pulmonary: "Yes",
            Physical: "No",
            Lacerations: "Yes",
            Anesthesia: "No",
            Category: "",
            FirstName: "",
            LastName: "",
            TypeOfPractice: "Ultimate Insurance",
            AdditionalOffice: "",
        },


        Partners: [
            {
                Location: "",
                First: "Maria",
                Middle: "",
                Last: "Scunziano MD",
                Number: "",
                Street: "",
                Suite: "",
                State: "",
                City: "",
                ZIP: "",
                Phonenumber: "",
                Specialty: "Internal Medicine",
                ProviderType: "MD",
            }
        ],

        MidLevel: [
            {
                PratitionerName: "",
                MidLevelPractitioners: "Yes",
                PractitionerFirstName: "Sharona",
                PractitionerMiddleName: "",
                PractitionerLastName: "Lowenstein",
                PractitionerLicense: "PA9103282",
                PractitionerState: "Florida",
                PractitionerType: "PA",

            }
        ]
    },


    {
        Type: "Secondary",
        CurrentlyPracticing: "Yes",
        ExpectedStartDate: "",
        PracticeName: "Access Health Care Physicians LLC",
        Telephone: "352-688-3379",
        Fax: "352-398-1333",
        EmailId: "-",
        GeneralCorrespondence: "",
        Number: "5362",
        Street: "Spring Hill DR",
        Suite: "",
        County: "Hernando, FL",
        Country: "USA",
        State: "Florida",
        City: "Spring Hill",
        ZIP: "346064562",
        IndividualTaxID: "59-3682760",
        GroupTaxID: "45-1444883",
        Primary: "Use group tax ID",



        OfficeStaffContact: {
            PracticeLocation: "",
            First: "Wanda",
            Middle: "",
            Last: "Casillas",
            Telephone: "",
            Fax: "",
            MailAddress: "wcasilla@accesshealthcarellc.net",

        },


        OfficeHours: {
            practicelocation: "",
            Day: "",
            StartTime: "8:30",
            StartAMPM: "AM",
            EndTime: "3:30",
            EndAMPM: "PM",
            PhoneCoverage: "Yes",
            BackOfficeTelephone: "3526669912",
        },

        OpenPracticeStatus: {
            PracticeLocation: "",
            newpatients: "Yes",
            AllNewPatients: "Yes",
            ExistingPatients: "Yes",
            Medicare: "No",
            PhysicianReferral: "Yes",
            Medicaid: "Yes",
            Informationvaries: "No longer accepting patients with Medicaid and Quality Health Plans",
            Limitations: "No",
            Gender: "None",
            Minimum: "12",
            Maximum: "112",
            OtherLimitations: "",

        },


        BillingContact: [
            {
                Location: "",
                First: "Ann",
                Middle: "",
                Last: "Marie",
                Number: "",
                Street: "",
                Suite: "",
                County: "",
                Country: "USA",
                State: "OHIO",
                City: "Cincinnati",
                ZIP: "452636233",
                BoxAddress: "P.O Box 636233",
                Telephone: "352-799-0046",
                Fax: "352-799-0042",
                MailAddress: ""
              
            }
        ],

        PaymentandRemittance: [
            {
                BillingCapabilities: "Yes",
                BillingDepartment: "",
                Check: "Access Health Care Physicians LLC",
                Office: "" , 
                Location: "",
                First: "Val",
                Middle: "",
                Last: "Patel",
                Number: "",
                Street: "P.O Box 636233",
                Suite: "",
                State: "OH",
                City: "Cincinnati",
                ZIP: "45263",
                Telephone: "3525934101",
                Fax: "3525934994",
                MailAddress: "valp@medinet.net",
            }
        ],


        Accessibilities: {
            PracticeLocation: "",
            ADA: "Yes",
            Building: "Yes",
            Parking: "Yes",
            Restroom: "Yes",
            Handicapped: "Yes",
            OtherServices: "No",
            TTY: "No",
            SingLanguage: "No",
            Impairment: "No",
            Transportation: "Yes",
            Bus: "Yes",
            Subway: "No",
            Train: "No",
            OtherHandicapped: "handicapped",
            OtherDisability: "disability",
            OtherTransportation: "Taxi",
        },


        Services: {
            PracticeLocation: "",
            LaboratoryServices: "Yes",
            Accrediting: "",
            RadiologyServices: "No",
            XRay: "",
            EKGS: "Yes",
            AllergyInjection: "Yes",
            SkinTesting: "No",
            Gynecology: "Yes",
            Blood: "Yes",
            Immunization: "No",
            Sigmoidoscopy: "No",
            Tympanometry: "No",
            Asthma: "Yes",
            Osteopathic: "No",
            Hydration: "Yes",
            Cardiac: "Yes",
            Pulmonary: "Yes",
            Physical: "No",
            Lacerations: "Yes",
            Anesthesia: "No",
            Category: "",
            FirstName: "",
            LastName: "",
            TypeOfPractice: "Multi Specialty Group",
            AdditionalOffice: "",
        },


        Partners: [
            {
                Location: "",
                First: "Maria",
                Middle: "",
                Last: "Scunziano MD",
                Number: "",
                Street: "",
                Suite: "",
                State: "",
                City: "",
                ZIP: "",
                Phonenumber: "",
                Specialty: "Internal Medicine",
                ProviderType: "MD",
            }
        ],

        MidLevel: [
            {
                PratitionerName: "",
                MidLevelPractitioners: "Yes",
                PractitionerFirstName: "Sharona",
                PractitionerMiddleName: "",
                PractitionerLastName: "Lowenstein",
                PractitionerLicense: "PA9103282",
                PractitionerState: "Florida",
                PractitionerType: "PA",

            }
        ]
    },



     {
         Type: "Secondary",
         CurrentlyPracticing: "Yes",
         ExpectedStartDate: "",
         PracticeName: "Unity Healthcare LLC / Mirra Health Care LL",
         Telephone: "352-835-7968",
         Fax: "352-835-7984",
         EmailId: "-",
         GeneralCorrespondence: "",
         Number: "5429",
         Street: "Commercial Way",
         Suite: "",
         County: "Hernando, FL",
         Country: "USA",
         State: "Florida",
         City: "Spring Hill",
         ZIP: "346061110",
         IndividualTaxID: "455171702",
         GroupTaxID: "45-1444883",
         Primary: "Use group tax ID",



         OfficeStaffContact: {
             PracticeLocation: "",
             First: "Chastity",
             Middle: "",
             Last: "Bishop",
             Telephone: "",
             Fax: "",
             MailAddress: "cbishop@accesshealthcarellc.net",

         },


         OfficeHours: {
             practicelocation: "",
             Day: "",
             StartTime: "8:30",
             StartAMPM: "AM",
             EndTime: "3:30",
             EndAMPM: "PM",
             PhoneCoverage: "Yes",
             BackOfficeTelephone: "3526669912",
         },

         OpenPracticeStatus: {
             PracticeLocation: "",
             newpatients: "Yes",
             AllNewPatients: "Yes",
             ExistingPatients: "Yes",
             Medicare: "No",
             PhysicianReferral: "Yes",
             Medicaid: "Yes",
             Informationvaries: "No longer accepting patients with Medicaid and Quality Health Plans",
             Limitations: "No",
             Gender: "None",
             Minimum: "12",
             Maximum: "112",
             OtherLimitations: "",

         },


         BillingContact: [
             {
                 Location: "",
                 First: "Ann",
                 Middle: "",
                 Last: "Marie",
                 Number: "",
                 Street: "",
                 Suite: "",
                 County: "",
                 Country: "USA",
                 State: "OHIO",
                 City: "Cincinnati",
                 ZIP: "452636233",
                 BoxAddress: "P.O Box 636233",
                 Telephone: "352-799-0046",
                 Fax: "352-799-0042",
                 MailAddress: ""
               
             }
         ],

         PaymentandRemittance: [
             {
                 BillingCapabilities: "Yes",
                 BillingDepartment: "",
                 Check: "Access Health Care Physicians LLC",
                 Office: "" , 
                 Location: "",
                 First: "Val",
                 Middle: "",
                 Last: "Patel",
                 Number: "",
                 Street: "P.O Box 636233",
                 Suite: "",
                 State: "OH",
                 City: "Cincinnati",
                 ZIP: "45263",
                 Telephone: "3525934101",
                 Fax: "3525934994",
                 MailAddress: "valp@medinet.net",
             }
         ],


         Accessibilities: {
             PracticeLocation: "",
             ADA: "Yes",
             Building: "Yes",
             Parking: "Yes",
             Restroom: "Yes",
             Handicapped: "Yes",
             OtherServices: "No",
             TTY: "No",
             SingLanguage: "No",
             Impairment: "No",
             Transportation: "Yes",
             Bus: "Yes",
             Subway: "No",
             Train: "No",
             OtherHandicapped: "handicapped",
             OtherDisability: "disability",
             OtherTransportation: "Taxi",
         },


         Services: {
             PracticeLocation: "",
             LaboratoryServices: "Yes",
             Accrediting: "",
             RadiologyServices: "No",
             XRay: "",
             EKGS: "Yes",
             AllergyInjection: "Yes",
             SkinTesting: "No",
             Gynecology: "Yes",
             Blood: "Yes",
             Immunization: "No",
             Sigmoidoscopy: "No",
             Tympanometry: "No",
             Asthma: "Yes",
             Osteopathic: "No",
             Hydration: "Yes",
             Cardiac: "Yes",
             Pulmonary: "Yes",
             Physical: "No",
             Lacerations: "Yes",
             Anesthesia: "No",
             Category: "",
             FirstName: "",
             LastName: "",
             TypeOfPractice: "Ultimate & Simply Insurance",
             AdditionalOffice: "",
         },


         Partners: [
             {
                 Location: "",
                 First: "Maria",
                 Middle: "",
                 Last: "Scunziano MD",
                 Number: "",
                 Street: "",
                 Suite: "",
                 State: "",
                 City: "",
                 ZIP: "",
                 Phonenumber: "",
                 Specialty: "Internal Medicine",
                 ProviderType: "MD",
             }
         ],

         MidLevel: [
             {
                 PratitionerName: "",
                 MidLevelPractitioners: "Yes",
                 PractitionerFirstName: "Sharona",
                 PractitionerMiddleName: "",
                 PractitionerLastName: "Lowenstein",
                 PractitionerLicense: "PA9103282",
                 PractitionerState: "Florida",
                 PractitionerType: "PA",

             }
         ]
     },


        {
            Type: "Secondary",
            CurrentlyPracticing: "Yes",
            ExpectedStartDate: "",
            PracticeName: "Access Health Care Physicians LLC",
            Telephone: "352-398-4573",
            Fax: "352-398-4591",
            EmailId: "-",
            GeneralCorrespondence: "",
            Number: "5378",
            Street: "Spring Hill Drive",
            Suite: "",
            County: "Hernando, FL",
            Country: "USA",
            State: "Florida",
            City: "Spring Hill",
            ZIP: "346064562",
            IndividualTaxID: "45-1444883",
            GroupTaxID: "45-1444883",
            Primary: "Use group tax ID",



            OfficeStaffContact: {
                PracticeLocation: "",
                First: "Enid",
                Middle: "",
                Last: "Cedre",
                Telephone: "",
                Fax: "",
                MailAddress: "ecedre@accesshealthcarellc.net",

            },


            OfficeHours: {
                practicelocation: "",
                Day: "",
                StartTime: "8:30",
                StartAMPM: "AM",
                EndTime: "3:30",
                EndAMPM: "PM",
                PhoneCoverage: "Yes",
                BackOfficeTelephone: "3526669912",
            },

            OpenPracticeStatus: {
                PracticeLocation: "",
                newpatients: "Yes",
                AllNewPatients: "Yes",
                ExistingPatients: "Yes",
                Medicare: "No",
                PhysicianReferral: "Yes",
                Medicaid: "Yes",
                Informationvaries: "No longer accepting patients with Medicaid and Quality Health Plans",
                Limitations: "No",
                Gender: "None",
                Minimum: "12",
                Maximum: "112",
                OtherLimitations: "",

            },


            BillingContact: [
                {
                    Location: "",
                    First: "Ann",
                    Middle: "",
                    Last: "Marie",
                    Number: "",
                    Street: "",
                    Suite: "",
                    County: "",
                    Country: "USA",
                    State: "OHIO",
                    City: "Cincinnati",
                    ZIP: "452636233",
                    BoxAddress: "P.O Box 636233",
                    Telephone: "352-799-0046",
                    Fax: "352-799-0042",
                    MailAddress: ""
                  
                }
            ],

            PaymentandRemittance: [
                {
                    BillingCapabilities: "Yes",
                    BillingDepartment: "",
                    Check: "Access Health Care Physicians LLC",
                    Office: "" , 
                    Location: "",
                    First: "Val",
                    Middle: "",
                    Last: "Patel",
                    Number: "",
                    Street: "P.O Box 636233",
                    Suite: "",
                    State: "OH",
                    City: "Cincinnati",
                    ZIP: "45263",
                    Telephone: "3525934101",
                    Fax: "3525934994",
                    MailAddress: "valp@medinet.net",
                }
            ],


            Accessibilities: {
                PracticeLocation: "",
                ADA: "Yes",
                Building: "Yes",
                Parking: "Yes",
                Restroom: "Yes",
                Handicapped: "Yes",
                OtherServices: "No",
                TTY: "No",
                SingLanguage: "No",
                Impairment: "No",
                Transportation: "Yes",
                Bus: "Yes",
                Subway: "No",
                Train: "No",
                OtherHandicapped: "handicapped",
                OtherDisability: "disability",
                OtherTransportation: "Taxi",
            },


            Services: {
                PracticeLocation: "",
                LaboratoryServices: "Yes",
                Accrediting: "",
                RadiologyServices: "No",
                XRay: "",
                EKGS: "Yes",
                AllergyInjection: "Yes",
                SkinTesting: "No",
                Gynecology: "Yes",
                Blood: "Yes",
                Immunization: "No",
                Sigmoidoscopy: "No",
                Tympanometry: "No",
                Asthma: "Yes",
                Osteopathic: "No",
                Hydration: "Yes",
                Cardiac: "Yes",
                Pulmonary: "Yes",
                Physical: "No",
                Lacerations: "Yes",
                Anesthesia: "No",
                Category: "",
                FirstName: "",
                LastName: "",
                TypeOfPractice: "Ultimate Insurance",
                AdditionalOffice: "",
            },


            Partners: [
                {
                    Location: "",
                    First: "Maria",
                    Middle: "",
                    Last: "Scunziano MD",
                    Number: "",
                    Street: "",
                    Suite: "",
                    State: "",
                    City: "",
                    ZIP: "",
                    Phonenumber: "",
                    Specialty: "Internal Medicine",
                    ProviderType: "MD",
                }
            ],

            MidLevel: [
                {
                    PratitionerName: "",
                    MidLevelPractitioners: "Yes",
                    PractitionerFirstName: "Sharona",
                    PractitionerMiddleName: "",
                    PractitionerLastName: "Lowenstein",
                    PractitionerLicense: "PA9103282",
                    PractitionerState: "Florida",
                    PractitionerType: "PA",

                }
            ]
        },




        {
            Type: "Secondary",
            CurrentlyPracticing: "Yes",
            ExpectedStartDate: "",
            PracticeName: "Access Health Care Physicians LLC",
            Telephone: "352-200-2190",
            Fax: "352-200-2191",
            EmailId: "-",
            GeneralCorrespondence: "",
            Number: "5382",
            Street: "Spring Hill Drive",
            Suite: "",
            County: "Hernando, FL",
            Country: "USA",
            State: "Florida",
            City: "Spring Hill",
            ZIP: "346064562",
            IndividualTaxID: "45-1444883",
            GroupTaxID: "45-1444883",
            Primary: "Use group tax ID",



            OfficeStaffContact: {
                PracticeLocation: "",
                First: "",
                Middle: "",
                Last: "",
                Telephone: "",
                Fax: "",
                MailAddress: "dburkhardt@accesshealthcarellc.net",

            },


            OfficeHours: {
                practicelocation: "",
                Day: "",
                StartTime: "8:30",
                StartAMPM: "AM",
                EndTime: "3:30",
                EndAMPM: "PM",
                PhoneCoverage: "Yes",
                BackOfficeTelephone: "3526669912",
            },

            OpenPracticeStatus: {
                PracticeLocation: "",
                newpatients: "Yes",
                AllNewPatients: "Yes",
                ExistingPatients: "Yes",
                Medicare: "No",
                PhysicianReferral: "Yes",
                Medicaid: "Yes",
                Informationvaries: "No longer accepting patients with Medicaid and Quality Health Plans",
                Limitations: "No",
                Gender: "None",
                Minimum: "12",
                Maximum: "112",
                OtherLimitations: "",

            },


            BillingContact: [
                {
                    Location: "",
                    First: "Ann",
                    Middle: "",
                    Last: "Marie",
                    Number: "",
                    Street: "",
                    Suite: "",
                    County: "",
                    Country: "USA",
                    State: "OHIO",
                    City: "Cincinnati",
                    ZIP: "452636233",
                    BoxAddress: "P.O Box 636233",
                    Telephone: "352-799-0046",
                    Fax: "352-799-0042",
                    MailAddress: ""
                   
                }
            ],

            PaymentandRemittance: [
                {
                    BillingCapabilities: "Yes",
                    BillingDepartment: "",
                    Check: "Access Health Care Physicians LLC",
                    Office: "" , 
                    Location: "",
                    First: "Val",
                    Middle: "",
                    Last: "Patel",
                    Number: "",
                    Street: "P.O Box 636233",
                    Suite: "",
                    State: "OH",
                    City: "Cincinnati",
                    ZIP: "45263",
                    Telephone: "3525934101",
                    Fax: "3525934994",
                    MailAddress: "valp@medinet.net",
                }
            ],


            Accessibilities: {
                PracticeLocation: "",
                ADA: "Yes",
                Building: "Yes",
                Parking: "Yes",
                Restroom: "Yes",
                Handicapped: "Yes",
                OtherServices: "No",
                TTY: "No",
                SingLanguage: "No",
                Impairment: "No",
                Transportation: "Yes",
                Bus: "Yes",
                Subway: "No",
                Train: "No",
                OtherHandicapped: "handicapped",
                OtherDisability: "disability",
                OtherTransportation: "Taxi",
            },


            Services: {
                PracticeLocation: "",
                LaboratoryServices: "Yes",
                Accrediting: "",
                RadiologyServices: "No",
                XRay: "",
                EKGS: "Yes",
                AllergyInjection: "Yes",
                SkinTesting: "No",
                Gynecology: "Yes",
                Blood: "Yes",
                Immunization: "No",
                Sigmoidoscopy: "No",
                Tympanometry: "No",
                Asthma: "Yes",
                Osteopathic: "No",
                Hydration: "Yes",
                Cardiac: "Yes",
                Pulmonary: "Yes",
                Physical: "No",
                Lacerations: "Yes",
                Anesthesia: "No",
                Category: "",
                FirstName: "",
                LastName: "",
                TypeOfPractice: "Multi Specialty Group",
                AdditionalOffice: "",
            },


            Partners: [
                {
                    Location: "",
                    First: "Maria",
                    Middle: "",
                    Last: "Scunziano MD",
                    Number: "",
                    Street: "",
                    Suite: "",
                    State: "",
                    City: "",
                    ZIP: "",
                    Phonenumber: "",
                    Specialty: "Internal Medicine",
                    ProviderType: "MD",
                }
            ],

            MidLevel: [
                {
                    PratitionerName: "",
                    MidLevelPractitioners: "Yes",
                    PractitionerFirstName: "Sharona",
                    PractitionerMiddleName: "",
                    PractitionerLastName: "Lowenstein",
                    PractitionerLicense: "PA9103282",
                    PractitionerState: "Florida",
                    PractitionerType: "PA",

                }
            ]
        },




        {
            Type: "Secondary",
            CurrentlyPracticing: "Yes",
            ExpectedStartDate: "",
            PracticeName: "Access Health Care Physicians LLC",
            Telephone: "352-600-7900",
            Fax: "352-600-8994",
            EmailId: "-",
            GeneralCorrespondence: "",
            Number: "3480",
            Street: "Deltona Blvd",
            Suite: "",
            County: "Hernando, FL",
            Country: "USA",
            State: "Florida",
            City: "Spring Hill",
            ZIP: "346064562",
            IndividualTaxID: "45-1444883",
            GroupTaxID: "45-1444883",
            Primary: "Use group tax ID",



            OfficeStaffContact: {
                PracticeLocation: "",
                First: "Theresa",
                Middle: "",
                Last: "Montmarquet",
                Telephone: "",
                Fax: "",
                MailAddress: "",

            },


            OfficeHours: {
                practicelocation: "",
                Day: "",
                StartTime: "8:30",
                StartAMPM: "AM",
                EndTime: "3:30",
                EndAMPM: "PM",
                PhoneCoverage: "Yes",
                BackOfficeTelephone: "3526669912",
            },

            OpenPracticeStatus: {
                PracticeLocation: "",
                newpatients: "Yes",
                AllNewPatients: "Yes",
                ExistingPatients: "Yes",
                Medicare: "No",
                PhysicianReferral: "Yes",
                Medicaid: "Yes",
                Informationvaries: "No longer accepting patients with Medicaid and Quality Health Plans",
                Limitations: "No",
                Gender: "None",
                Minimum: "12",
                Maximum: "112",
                OtherLimitations: "",

            },


            BillingContact: [
                {
                    Location: "",
                    First: "Ann",
                    Middle: "",
                    Last: "Marie",
                    Number: "",
                    Street: "",
                    Suite: "",
                    County: "",
                    Country: "USA",
                    State: "OHIO",
                    City: "Cincinnati",
                    ZIP: "452636233",
                    BoxAddress: "P.O Box 636233",
                    Telephone: "352-799-0046",
                    Fax: "352-799-0042",
                    MailAddress: ""
                   
                }
            ],

            PaymentandRemittance: [
                {
                    BillingCapabilities: "Yes",
                    BillingDepartment: "",
                    Check: "Access Health Care Physicians LLC",
                    Office: "" , 
                    Location: "",
                    First: "Val",
                    Middle: "",
                    Last: "Patel",
                    Number: "",
                    Street: "P.O Box 636233",
                    Suite: "",
                    State: "OH",
                    City: "Cincinnati",
                    ZIP: "45263",
                    Telephone: "3525934101",
                    Fax: "3525934994",
                    MailAddress: "valp@medinet.net",
                }
            ],


            Accessibilities: {
                PracticeLocation: "",
                ADA: "Yes",
                Building: "Yes",
                Parking: "Yes",
                Restroom: "Yes",
                Handicapped: "Yes",
                OtherServices: "No",
                TTY: "No",
                SingLanguage: "No",
                Impairment: "No",
                Transportation: "Yes",
                Bus: "Yes",
                Subway: "No",
                Train: "No",
                OtherHandicapped: "handicapped",
                OtherDisability: "disability",
                OtherTransportation: "Taxi",
            },


            Services: {
                PracticeLocation: "",
                LaboratoryServices: "Yes",
                Accrediting: "",
                RadiologyServices: "No",
                XRay: "",
                EKGS: "Yes",
                AllergyInjection: "Yes",
                SkinTesting: "No",
                Gynecology: "Yes",
                Blood: "Yes",
                Immunization: "No",
                Sigmoidoscopy: "No",
                Tympanometry: "No",
                Asthma: "Yes",
                Osteopathic: "No",
                Hydration: "Yes",
                Cardiac: "Yes",
                Pulmonary: "Yes",
                Physical: "No",
                Lacerations: "Yes",
                Anesthesia: "No",
                Category: "",
                FirstName: "",
                LastName: "",
                TypeOfPractice: "Simply HC",
                AdditionalOffice: "",
            },


            Partners: [
                {
                    Location: "",
                    First: "Maria",
                    Middle: "",
                    Last: "Scunziano MD",
                    Number: "",
                    Street: "",
                    Suite: "",
                    State: "",
                    City: "",
                    ZIP: "",
                    Phonenumber: "",
                    Specialty: "Internal Medicine",
                    ProviderType: "MD",
                }
            ],

            MidLevel: [
                {
                    PratitionerName: "",
                    MidLevelPractitioners: "Yes",
                    PractitionerFirstName: "Sharona",
                    PractitionerMiddleName: "",
                    PractitionerLastName: "Lowenstein",
                    PractitionerLicense: "PA9103282",
                    PractitionerState: "Florida",
                    PractitionerType: "PA",

                }
            ]
        },



        {
            Type: "Secondary",
            CurrentlyPracticing: "Yes",
            ExpectedStartDate: "",
            PracticeName: "Access Health Care Physicians LLC",
            Telephone: "352-610-9960",
            Fax: "352-610-9965",
            EmailId: "-",
            GeneralCorrespondence: "",
            Number: "7556",
            Street: "Spring Hill DR",
            Suite: "",
            County: "Hernando, FL",
            Country: "USA",
            State: "Florida",
            City: "Spring Hill",
            ZIP: "346064349",
            IndividualTaxID: "45-1444883",
            GroupTaxID: "45-1444883",
            Primary: "Use group tax ID",



            OfficeStaffContact: {
                PracticeLocation: "",
                First: "Joan Kosanovich",
                Middle: "",
                Last: "Montmarquet",
                Telephone: "",
                Fax: "",
                MailAddress: "jkosanovich@accesshealthcarellc.net",

            },


            OfficeHours: {
                practicelocation: "",
                Day: "",
                StartTime: "8:30",
                StartAMPM: "AM",
                EndTime: "3:30",
                EndAMPM: "PM",
                PhoneCoverage: "Yes",
                BackOfficeTelephone: "3526669912",
            },

            OpenPracticeStatus: {
                PracticeLocation: "",
                newpatients: "Yes",
                AllNewPatients: "Yes",
                ExistingPatients: "Yes",
                Medicare: "No",
                PhysicianReferral: "Yes",
                Medicaid: "Yes",
                Informationvaries: "No longer accepting patients with Medicaid and Quality Health Plans",
                Limitations: "No",
                Gender: "None",
                Minimum: "12",
                Maximum: "112",
                OtherLimitations: "",

            },


            BillingContact: [
                {
                    Location: "",
                    First: "Ann",
                    Middle: "",
                    Last: "Marie",
                    Number: "",
                    Street: "",
                    Suite: "",
                    County: "",
                    Country: "USA",
                    State: "OHIO",
                    City: "Cincinnati",
                    ZIP: "452636233",
                    BoxAddress: "P.O Box 636233",
                    Telephone: "352-799-0046",
                    Fax: "352-799-0042",
                    MailAddress: ""
                   
                }
            ],

            PaymentandRemittance: [
                {
                    BillingCapabilities: "Yes",
                    BillingDepartment: "",
                    Check: "Access Health Care Physicians LLC",
                    Office: "" ,
                    Location: "",
                    First: "Val",
                    Middle: "",
                    Last: "Patel",
                    Number: "",
                    Street: "P.O Box 636233",
                    Suite: "",
                    State: "OH",
                    City: "Cincinnati",
                    ZIP: "45263",
                    Telephone: "3525934101",
                    Fax: "3525934994",
                    MailAddress: "valp@medinet.net",
                }
            ],


            Accessibilities: {
                PracticeLocation: "",
                ADA: "Yes",
                Building: "Yes",
                Parking: "Yes",
                Restroom: "Yes",
                Handicapped: "Yes",
                OtherServices: "No",
                TTY: "No",
                SingLanguage: "No",
                Impairment: "No",
                Transportation: "Yes",
                Bus: "Yes",
                Subway: "No",
                Train: "No",
                OtherHandicapped: "handicapped",
                OtherDisability: "disability",
                OtherTransportation: "Taxi",
            },


            Services: {
                PracticeLocation: "",
                LaboratoryServices: "Yes",
                Accrediting: "",
                RadiologyServices: "No",
                XRay: "",
                EKGS: "Yes",
                AllergyInjection: "Yes",
                SkinTesting: "No",
                Gynecology: "Yes",
                Blood: "Yes",
                Immunization: "No",
                Sigmoidoscopy: "No",
                Tympanometry: "No",
                Asthma: "Yes",
                Osteopathic: "No",
                Hydration: "Yes",
                Cardiac: "Yes",
                Pulmonary: "Yes",
                Physical: "No",
                Lacerations: "Yes",
                Anesthesia: "No",
                Category: "",
                FirstName: "",
                LastName: "",
                TypeOfPractice: "Multi Specialty Group",
                AdditionalOffice: "",
            },


            Partners: [
                {
                    Location: "",
                    First: "Maria",
                    Middle: "",
                    Last: "Scunziano MD",
                    Number: "",
                    Street: "",
                    Suite: "",
                    State: "",
                    City: "",
                    ZIP: "",
                    Phonenumber: "",
                    Specialty: "Internal Medicine",
                    ProviderType: "MD",
                }
            ],

            MidLevel: [
                {
                    PratitionerName: "",
                    MidLevelPractitioners: "Yes",
                    PractitionerFirstName: "Sharona",
                    PractitionerMiddleName: "",
                    PractitionerLastName: "Lowenstein",
                    PractitionerLicense: "PA9103282",
                    PractitionerState: "Florida",
                    PractitionerType: "PA",
                }
            ]
        }
    ]



    $scope.Days = [{
        Day: 0, Times: [{ id: "" }]
    }, {
        Day: 1, Times: [{ id: "" }]
    },
    {
        Day: 2, Times: [{ id: "" }]
    },
    {
        Day: 3, Times: [{ id: "" }]
    },
    {
        Day: 4, Times: [{ id: "" }]
    },
    {
        Day: 5, Times: [{ id: "" }]
    },
    {
        Day: 6, Times: [{ id: "" }]
    }
    ]


    $scope.PrimaryCredentialingContacts = [{
        OfficeManagerAvail: true,
        OfficeManager: "Annmarie Emerick",
        FirstName: "Annmarie",
        MiddleName: "",
        LastName: "Emerick",
        Number: "12225",
        Street: "28 Street North",
        Building: "Suite A",
        City: "St Petersburg",
        State: "Florida",
        ZipCode: "337161860",
        Telephone: "7278232188",
        Fax: "7278280723",
        EmailId: "americk9a6@medinet.net"

    }];


    $scope.showDenied = function () {

        $scope.showInput = true;
    }

    $scope.hideDenied = function () {

        $scope.showInput = false;
    }

    //=============== Practice Location Information Conditions ==================
    $scope.primaryPracticeLocationFormStatus = false;
    $scope.newPrimaryPracticeLocationForm = false;

    $scope.midLevelPractitionerFormStatus = false;
    $scope.newMidLevelPractitionerForm = false;

    $scope.colleagueFormStatus = false;
    $scope.newColleagueForm = false;

    $scope.showingDetails = false;
    $scope.ShowMidLevels = false;


    //===============  Conditions ==================
    $scope.editShowPrimaryLocationInformation = false;
    $scope.newShowPrimaryLocationInformation = false;
    $scope.submitButtonText = "Add";
    $scope.IndexValue = 0;

    //==================================== Primary Practice Location Information ===============================================

    $scope.addPrimaryLocationInformation = function () {
        $scope.newShowPrimaryLocationInformation = true;
        $scope.submitButtonText = "Add";
        $scope.primaryLocationInformation = {};
    };

    $scope.editPrimaryLocationInformation = function (index, primaryLocationInformation) {
        $scope.viewShowPrimaryLocationInformation = false;
        $scope.editShowPrimaryLocationInformation = true;
        $scope.submitButtonText = "Update";
        $scope.primaryLocationInformation = primaryLocationInformation;
        $scope.IndexValue = index;
    };

    $scope.viewPrimaryLocationInformation = function (index, primaryLocationInformation) {
        $scope.editShowPrimaryLocationInformation = false;
        $scope.viewShowPrimaryLocationInformation = true;
        $scope.primaryLocationInformation = primaryLocationInformation;
        $scope.IndexValue = index;
    };

    $scope.savePrimaryLocationInformation = function (primaryLocationInformation) {

        console.log(primaryLocationInformation);

        url = "/Profile/PracticeLocation/UpdateWorkersCompensationInformationAsync?profileId=1";

        $http.post(url, primaryLocationInformation).
         success(function (data, status, headers, config) {

             alert("Success");

         }).
         error(function (data, status, headers, config) {
             alert(data);
         });

    };

    $scope.cancelPrimaryLocationInformation = function () {
        setPrimaryLocationInformationCancelParameters();
    };

    function setPrimaryLocationInformationCancelParameters() {
        $scope.viewShowPrimaryLocationInformation = false;
        $scope.editShowPrimaryLocationInformation = false;
        $scope.newShowPrimaryLocationInformation = false;
        $scope.primaryLocationInformation = {};
        $scope.IndexValue = 0;
    }

    function ResetHosPrvForm() {
        $('#newShowPrimaryLocationInformationtDiv').find('.primaryLocationInformationForm')[0].reset();
        $('#newShowPrimaryLocationInformationDiv').find('span').html('');
    }

    $scope.updatePrimaryPracticeLocation = function (primaryPracticeLocation) {
        $scope.showingDetails = false;
        $scope.primaryPracticeLocationFormStatus = false;
        $scope.newPrimaryPracticeLocationForm = false;
        $scope.primaryPracticeLocation = {};
    };

    //=======================================================================================================================================


    //==================================== Office manager/Business Office Staff Contact =====================================================

    $scope.cancelOfficeStaffContact = function () {
        setOfficeStaffContactCancelParameters();
    };

    //=======================================================================================================================================



    //==================================== Office Hours ====================================================================================


    //=======================================================================================================================================



    //==================================== Open Practice Status=============================================================================


    //=======================================================================================================================================



    //==================================== Billing Contact=============================================================================


    //===============  Conditions ==================
    $scope.editShowBillingContact = false;
    $scope.newShowBillingContact = false;
    $scope.submitButtonText = "Add";
    $scope.IndexValue = 0;

    //===============================================

    $scope.addBillingContact = function () {
        $scope.newShowBillingContact = true;
        $scope.submitButtonText = "Add";
        $scope.billingContact = {};
    };

    $scope.editBillingContact = function (index, BillingContact) {
        $scope.viewShowBillingContact = false;
        $scope.editShowBillingContact = true;
        $scope.submitButtonText = "Update";
        $scope.billingContact = billingContact;
        $scope.IndexValue = index;
    };

    $scope.viewBillingContact = function (index, billingContact) {
        $scope.editShowBillingContact = false;
        $scope.viewShowBillingContact = true;
        $scope.billingContact = billingContact;
        $scope.IndexValue = index;
    };

    $scope.cancelBillingContact = function (condition) {
        setBillingContactCancelParameters();
    };

    function setBillingContactCancelParameters() {
        $scope.viewShowBillingContact = false;
        $scope.editShowBillingContact = false;
        $scope.newShowBillingContact = false;
        $scope.billingContact = {};
        $scope.IndexValue = 0;
    }

    function ResetHosPrvForm() {
        $('#newShowBillingContactDiv').find('.billingContactForm')[0].reset();
        $('#newShowBillingContactDiv').find('span').html('');
    }

    //=======================================================================================================================================



    //====================================Payment and Remittance=============================================================================

    //===============  Conditions ==================
    $scope.editShowPayment = false;
    $scope.newShowPayment = false;
    $scope.submitButtonText = "Add";
    $scope.IndexValue = 0;

    //===============================================

    $scope.addPayment = function () {
        $scope.newShowPayment = true;
        $scope.submitButtonText = "Add";
        $scope.payment = {};
    };

    $scope.editPayment = function (index, Payment) {
        $scope.viewShowPayment = false;
        $scope.editShowPayment = true;
        $scope.submitButtonText = "Update";
        $scope.payment = payment;
        $scope.IndexValue = index;
    };

    $scope.viewPayment = function (index, payment) {
        $scope.editShowPayment = false;
        $scope.viewShowPayment = true;
        $scope.payment = payment;
        $scope.IndexValue = index;
    };

    $scope.cancelPayment = function (condition) {
        setPaymentCancelParameters();
    };

    function setPaymentCancelParameters() {
        $scope.viewShowPayment = false;
        $scope.editShowPayment = false;
        $scope.newShowPayment = false;
        $scope.payment = {};
        $scope.IndexValue = 0;
    }

    function ResetHosPrvForm() {
        $('#newShowPaymentDiv').find('.paymentForm')[0].reset();
        $('#newShowPaymentDiv').find('span').html('');
    }

    //=======================================================================================================================================



    //====================================Languages=============================================================================


    //=======================================================================================================================================




    //====================================Accessibilities=============================================================================


    //=======================================================================================================================================




    //====================================Services=============================================================================


    //=======================================================================================================================================


    //====================================Partners/Associates/Covering Colleagues=============================================================================
    //===============  Conditions ==================
    $scope.editShowPartners = false;
    $scope.newShowPartners = false;
    $scope.submitButtonText = "Add";
    $scope.IndexValue = 0;

    //===============================================

    $scope.addPartners = function () {
        $scope.newShowPartners = true;
        $scope.submitButtonText = "Add";
        $scope.partners = {};
    };

    $scope.editPartners = function (index, Partners) {
        $scope.viewShowPartners = false;
        $scope.editShowPartners = true;
        $scope.submitButtonText = "Update";
        $scope.partners = partners;
        $scope.IndexValue = index;
    };

    $scope.viewPartners = function (index, partners) {
        $scope.editShowPartners = false;
        $scope.viewShowPartners = true;
        $scope.partners = partners;
        $scope.IndexValue = index;
    };

    $scope.cancelPartners = function (condition) {
        setPartnersCancelParameters();
    };

    function setPartnersCancelParameters() {
        $scope.viewShowPartners = false;
        $scope.editShowPartners = false;
        $scope.newShowPartners = false;
        $scope.partners = {};
        $scope.IndexValue = 0;
    }

    function ResetHosPrvForm() {
        $('#newShowPartnersDiv').find('.partnersForm')[0].reset();
        $('#newShowPartnersDiv').find('span').html('');
    } 

    //=======================================================================================================================================



    //====================================Mid-Level Practitioners=============================================================================

    //===============  Conditions ==================
    $scope.editShowMidLevel = false;
    $scope.newShowMidLevel = false;
    $scope.submitButtonText = "Add";
    $scope.IndexValue = 0;

    //===============================================

    $scope.addMidLevel = function () {
        $scope.newShowMidLevel = true;
        $scope.submitButtonText = "Add";
        $scope.midLevel = {};
    };

    $scope.editMidLevel = function (index, MidLevel) {
        $scope.viewShowMidLevel = false;
        $scope.editShowMidLevel = true;
        $scope.submitButtonText = "Update";
        $scope.midLevel = midLevel;
        $scope.IndexValue = index;
    };

    $scope.viewMidLevel = function (index, midLevel) {
        $scope.editShowMidLevel = false;
        $scope.viewShowMidLevel = true;
        $scope.midLevel = midLevel;
        $scope.IndexValue = index;
    };

    $scope.cancelMidLevel = function (condition) {
        setMidLevelCancelParameters();
    };

    function setMidLevelCancelParameters() {
        $scope.viewShowMidLevel = false;
        $scope.editShowMidLevel = false;
        $scope.newShowMidLevel = false;
        $scope.midLevel = {};
        $scope.IndexValue = 0;
    }

    function ResetHosPrvForm() {
        $('#newShowMidLevelDiv').find('.midLevelForm')[0].reset();
        $('#newShowMidLevelDiv').find('span').html('');
    }

    //=======================================================================================================================================

    $scope.addTime = function (dayId) {
        console.log(dayId);
        console.log(7);
        console.log($scope.Days)
        console.log($scope.Days[dayId])
        console.log($scope.Days[dayId].Times)
        $scope.Days[dayId].Times.push({});
    };
    $scope.removeTime = function (dayId, index) {
        $scope.Days[dayId].Times.splice(index, 1);
    };
    $scope.showMidLevel = function () {

        $scope.ShowMidLevels = true;
    };
    $scope.hideMidLevel = function () {

        $scope.ShowMidLevels = false;
    };

    $scope.cancelPrimaryPracticeLocation = function (condition) {
        if (condition == "editCancel") {
            $scope.showingDetails = false;
            $scope.primaryPracticeLocationFormStatus = false;
            $scope.newPrimaryPracticeLocationForm = false;
            $scope.primaryPracticeLocation = {};
        } else {
            $scope.showingDetails = false;
            $scope.primaryPracticeLocationFormStatus = false;
            $scope.newPrimaryPracticeLocationForm = false;
            $scope.primaryPracticeLocation = {};
        }
    };

    $scope.removePrimaryPracticeLocation = function (index) {
        for (var i in $scope.PrimaryPracticeLocations) {
            if (index == i) {
                $scope.PrimaryPracticeLocations.splice(index, 1);
                break;
            }
        }
    };

    $scope.showDenied = function () {

        $scope.showInput = true;
    }

    $scope.hideDenied = function () {

        $scope.showInput = false;
    }

    $scope.showClass = function () {

        $scope.admin = true;
    }

    $scope.hideClass = function () {

        $scope.admin = false;
    }

    $scope.showDetails = function () {

        $scope.details = true;
    }

    $scope.hideDetails = function () {

        $scope.details = false;
    }

    $scope.showCalls = function () {

        $scope.answer = true;
    }

    $scope.hideCalls = function () {

        $scope.answer = false;
    }

    $scope.showProgram = function () {

        $scope.program = true;
    }

    $scope.hideProgram = function () {

        $scope.program = false;
    }

    $scope.showXray = function () {

        $scope.xray = true;
    }

    $scope.hideXray = function () {

        $scope.xray = false;
    }

    $scope.showLanguages = function () {

        $scope.languages = true;
    }

    $scope.hideLanguages = function () {

        $scope.languages = false;
    }
});