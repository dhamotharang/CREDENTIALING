
//=========================== Controller declaration ==========================
profileApp.controller('educationController', function ($scope, $http, dynamicFormGenerateService, countryDropDownService) {


    //=============================Country/State/County/City dropdown==============================

    $scope.Countries = Countries;
    $scope.States = $scope.Countries[1].States;
    $scope.CountryDialCodes = countryDailCodes;
    //---------------------- get states ---------------------
    $scope.getStates = function (countryCode) {
        $scope.States = countryDropDownService.getStates($scope.Countries, countryCode);
        $scope.Counties = [];
        $scope.Cities = [];
        $scope.educationDetailViewModel.EducationAddress.State = $scope.putempty;
        $scope.educationDetailViewModel.EducationAddress.County = $scope.putempty;
        $scope.educationDetailViewModel.EducationAddress.City = $scope.putempty;
        //resetStateSelectTwoStyle();
    };
    $scope.getCounties = function (state) {
        $scope.Counties = countryDropDownService.getCounties($scope.States, state);
        $scope.Cities = [];
        $scope.educationDetailViewModel.EducationAddress.County = $scope.putempty;
        $scope.educationDetailViewModel.EducationAddress.City = $scope.putempty;
    };
    $scope.getCities = function (county) {
        $scope.Cities = countryDropDownService.getCities($scope.Counties, county);
        $scope.educationDetailViewModel.EducationAddress.City = $scope.putempty;
    };

    //===========================================================================

    $scope.SchoolDetailViewModel = [{
        GraduateType: "false",
        SchoolName: "Saint Xavier School",
        QualificationDegree: "Other",
        Email: "",
        Fax: "",
        Phone: "",
        StartDate: new Date(1975, 12, 01),
        EndDate: new Date(1984, 12, 01),
        UGCertificatePath: "certificat.pdf",
        UGCompletedInThisSchool: "Yes",
        EducationAddress: {
            Number: "",
            Building: "",
            Street: "Bhagwan Das Road",
            Country: "India",
            County: "",
            State: "Rajasthan",
            City: "Jaipur",
            Zipcode: "_ _ _ _ _ _"
        }
    }];

    //============== Graduation Data  ================
    $scope.Graduations = [{
        GraduateType: "U.S. Graduate",
        School: {
            SchoolCode: "SC7548",
            SchoolName: "Whiteney School",
            DegreeAwarded: "Master Degree",
            FaxNumber: "7877766565",
            TelephoneNumber: "7846758898",
            StartDate: "24-09-1999",
            EndDate: "04-03-2003",
            Certificate: "certificat.pdf"
        },
        Address: {
            UnitNumber: "A746",
            Building: "Tapaswiji Arcade",
            Street: "SilkBoard",
            Country: "United State",
            State: "Florida",
            City: "Florida",
            ZipCode: "67654-_ _ _ _"
        },
        ECFMG: {
            Number: "5354",
            IssueDate: "11-08-2011",
            Certificate: "certificat.pdf"
        }
    }];
    $scope.UGs = [{
        School: {
            SchoolCode: "SC7548",
            SchoolName: "Whiteney School",
            DegreeAwarded: "Engineers Degree",
            FaxNumber: "7877766565",
            TelephoneNumber: "7846758898",
            StartDate: "24-09-1999",
            EndDate: "04-03-2003",
            Certificate: "certificat.pdf"
        },
        Address: {
            UnitNumber: "A746",
            Building: "",
            Street: "SilkBoard",
            Country: "United State",
            State: "Florida",
            City: "Florida",
            ZipCode: "67654-_ _ _ _"
        },
    }];

    $scope.GraduationDetailViewModel = [{
        GraduateType: "Non US Graduate",
        SchoolName: "Sawai Mansingh Medical College",
        QualificationDegree: "MD",
        Fax: "",
        Phone: "",
        StartDate: new Date(1984, 12, 01),
        EndDate: new Date(1993, 12, 01),
        EducationAddress: {
            Number: "",
            Building: "",
            Street: "Rajasthan University",
            Country: "India",
            State: "Rajasthan",
            City: "Jaipur",
            ZipCode: "30203-_ _ _ _",
            County: ""
        },
        GraduationCertPath: "certificat.pdf",
        ECFMGDetail: {
            ECFMGNumber: "04815080",
            ECFMGIssueDate: new Date(1992, 10, 07),
            ECFMGCertPath: "certificat.pdf",

        },
        GraduationCompletedAtThisSchool: "Yes"
    }]



    //============== Residency/ InternShip  ================
    $scope.Programs = [{
        ProgramType: "Internship",
        School: {
            SchoolCode: "SC7548",
            SchoolName: "Whiteney School",
            DegreeAwarded: "Master Degree",
            FaxNumber: "7877766565",
            TelephoneNumber: "7846758898",
            StartDate: new Date(1999, 08, 24),
            EndDate: new Date(2003, 02, 04)
        },
        Address: {
            UnitNumber: "A746",
            Building: "",
            Street: "SilkBoard",
            Country: "United State",
            State: "Florida",
            City: "Florida",
            ZipCode: "67654-_ _ _ _"
        },
        Other: {
            DepartmentSpeciality: "Dentist",
            DItrectorName: "Director 1",
            Certificate: "certificat.pdf"
        }
    }];

    $scope.ResidencyInternshipViewModel = [{
        SchoolDetail: {
            SchoolName: "New York Medical College",
            Fax: "",
            Phone: "",
            StartDate: new Date(1993, 06, 01),
            EndDate: new Date(1996, 05, 30),
            SchoolAddress: {
                Number: "",
                Building: "",
                Street: "One Gustave Levy Place",
                Country: "United States",
                County: "",
                State: "New York",
                City: "New York",
                ZipCode: "10019-_ _ _ _"
            },
        },
        FellowshipDetail: [{
            Specialty: "Resident in Internal Medicine",
            StartDate: new Date(1993, 06, 01),
            EndDate: new Date(1996, 05, 30),
            ProgramType: "Residency",
            Document: "SINGH - RESIDENCY CERT.pdf",
            DocumentName: "Residency cert",
            Department: ""
        }],
        DirectorName: "",
        CompletedTrainingHere: "Yes",
        Reason: "",
        AffiliatedUniversityHospital: "Mount Sinai School of Medicine",
    },
    {
        SchoolDetail: {
            SchoolName: "All India Institute of Medical Services",
            Fax: "",
            Phone: "",
            StartDate: new Date(1972, 05, 30),
            EndDate: new Date(1973, 05, 30),
            SchoolAddress: {
                Number: "29",
                Building: "",
                Street: "Aurobindo Marg Ansari Nagar",
                Country: "India",
                County: "",
                State: "",
                City: "New Delhi",
                ZipCode: "11029-_ _ _ _"
            },
        },
        FellowshipDetail: [{
            Specialty: "Ophthalmology",
            StartDate: new Date(1972, 05, 30),
            EndDate: new Date(1973, 05, 30),
            ProgramType: "Resident",
            Document: "SINGH - INTERNSHIP.pdf",
            DocumentName: "Internship",
            Department: ""
        }],
        DirectorName: "",
        CompletedTrainingHere: "Yes",
        Reason: "",
        AffiliatedUniversityHospital: "All India Institute of Medical Services",
    }
    ];

    //============== Residency/ InternShip  ================
    $scope.Specialities = [{
        Type: "Primary",
        Code: "Dentist",
        Board: {
            Code: "SC7548",
            Name: "Whiteney School",
            InitialCertificateDate: "DegreeAwaarded1",
            LastReCertificationDate: "7877766565",
            ExpirationDate: "7846758898",
            Certificate: "certificat.pdf"
        }
    }];

    //$scope.showingDetails = false;
    //================================ Under Graduation Conditions =======================

    //$scope.ugFormStatus = false;
    //$scope.newUGForm = false;
    $scope.showDropDown = function () {
        this.dropDown = false;
        this.textBox = false;
    }

    $scope.showTextBox = function () {
        this.dropDown = true;
        this.textBox = true;
    }

    $scope.graduate = function () {

        if (this.graduationDetailViewModel.GraduateType == "USGraduate") {
            this.dropDown = false;
            this.textBox = false;
            this.showECFMG = false;
        }
        else if (this.graduationDetailViewModel.GraduateType == "NonUSGraduate") {
            this.dropDown = true;
            this.textBox = true;
            this.showECFMG = true;
        }
        else if (this.graduationDetailViewModel.GraduateType == "FifthPathwayGraduate") {
            this.dropDown = false;
            this.textBox = false;
            this.showECFMG = false;
        }

    }

    $scope.addUG = function () {
        $scope.ugFormStatus = false;
        $scope.newUGForm = true;
        $scope.viewUGForm = false;
        $scope.UGFormStatus = true;
        $scope.showPreview = false;
        $scope.showingUGDetails = false;
        $scope.schoolDetailViewModel = {};
        //$("#newUGFormDiv").html(dynamicFormGenerateService.getForm($scope, $("#ugForm").html()));
    };

    $scope.saveUG = function (schoolDetailViewModel) {
        //================== Save Here ============
        //$scope.Graduations.push(Graduation);
        //================== hide Show Condition ============
        $scope.ugFormStatus = false;
        $scope.newUGForm = false;
        $scope.SchoolDetailViewModel.push(schoolDetailViewModel);
        $scope.UG = {};
    };

    $scope.updateUG = function (UG) {
        $scope.showingUGDetails = false;
        $scope.ugFormStatus = false;
        $scope.newUGForm = false;
        $scope.schoolDetailViewModel = {};
    };

    $scope.editUG = function (index, UG) {
        if (UG.GraduateType == "false") {

            $scope.dropDown = true;
            $scope.textBox = true;
        }
        else {
            $scope.dropDown = false;
            $scope.textBox = false;
        }
        $scope.showingUGDetails = true;
        $scope.ugFormStatus = true;
        $scope.newUGForm = false;
        $scope.viewUGForm = false;
        $scope.showViewUGBtn = false;
        $scope.hideEditUGBtn = true;
        $scope.showPreview = true;
        $scope.UG = UG;
        //$("#ugEditDiv" + index).html(dynamicFormGenerateService.getForm($scope, $("#ugForm").html()));
    };

    $scope.cancelUG = function (condition) {
        if (condition == "editCancel") {
            $scope.showingUGDetails = false;
            $scope.ugFormStatus = false;
            $scope.newUGForm = false;
            $scope.hideEditUGBtn = false;
            $scope.UGFormStatus = false;
            $scope.dropDown = false;
            $scope.textBox = false;
            $scope.schoolDetailViewModel = {};
        } else {
            $scope.showingUGDetails = false;
            $scope.newUGForm = false;
            $scope.newLegalNameForm = false;
            $scope.hideEditUGBtn = false;
            $scope.UGFormStatus = false;
            $scope.dropDown = false;
            $scope.textBox = false;
            $scope.schoolDetailViewModel = {};
        }
    };

    $scope.removeUG = function (index) {
        for (var i in $scope.UGs) {
            if (index == i) {
                $scope.UGs.splice(index, 1);
                break;
            }
        }
    };

    $scope.showViewUG = function (index, UG) {
        $scope.showingUGDetails = false;
        $scope.newUGForm = false;
        $scope.showViewUGBtn = true;
        $scope.UGFormStatus = true;
        $scope.schoolDetailViewModel = UG;
        $scope.viewUGForm = true;
        $scope.hideEditUGBtn = false;
    }

    $scope.cancelViewUG = function () {
        $scope.viewUGForm = false;
        $scope.showViewUGBtn = false;
        $scope.showingUGDetails = false;
        $scope.newUGForm = false;
        $scope.UGFormStatus = false;
        $scope.hideEditUGBtn = false;
    }

    //=============== Graduation Conditions ==================
    $scope.graduationFormStatus = false;
    $scope.newGraduationForm = false;

    $scope.addGraduation = function () {
        $scope.graduationFormStatus = false;
        $scope.newGraduationForm = true;
        $scope.showPreview = false;
        $scope.viewPG = false;
        $scope.showViewPGBtn = false;
        $scope.showingGDetails = false;
        $scope.graduationDetailViewModel = {};
        //$("#newGraduationFormDiv").html(dynamicFormGenerateService.getForm($scope, $("#graduationForm").html()));
    };

    $scope.saveGraduation = function (Graduation) {
        //================== Save Here ============
        //$scope.Graduations.push(Graduation);
        //================== hide Show Condition ============
        $scope.graduationFormStatus = false;
        $scope.newGraduationForm = false;
        $scope.Graduation = {};
    };

    $scope.updateGraduation = function (Graduation) {
        $scope.showingGDetails = false;
        $scope.graduationFormStatus = false;
        $scope.newGraduationForm = false;
        $scope.Graduation = {};
    };

    $scope.editGraduation = function (index, Graduation) {

        if (Graduation.GraduateType == "NonUSGraduate") {
            $scope.showECFMG = true;
            $scope.dropDown = true;
            $scope.textBox = true;
        }
        else {
            $scope.showECFMG = false;
            $scope.dropDown = false;
            $scope.textBox = false;
        }

        $scope.showingGDetails = true;
        $scope.graduationFormStatus = true;
        $scope.newGraduationForm = false;
        $scope.viewPG = false;
        //$scope.showECFMG = true;
        $scope.showViewPGBtn = false;
        $scope.showPreview = true;
        $scope.hideEditPGBtn = true;
        $scope.Graduation = Graduation;
        //$("#graduationEditDiv" + index).html(dynamicFormGenerateService.getForm($scope, $("#graduationForm").html()));
    };

    $scope.cancelGraduation = function (condition) {
        if (condition == "editCancel") {
            $scope.showingGDetails = false;
            $scope.graduationFormStatus = false;
            $scope.newGraduationForm = false;
            $scope.hideEditPGBtn = false;
            $scope.showECFMG = false;
            $scope.dropDown = false;
            $scope.textBox = false;
            $scope.graduationDetailViewModel = {};
        } else {
            $scope.showingGDetails = false;
            $scope.newGraduationForm = false;
            $scope.newLegalNameForm = false;
            $scope.hideEditPGBtn = false;
            $scope.showECFMG = false;
            $scope.dropDown = false;
            $scope.textBox = false;
            $scope.graduationDetailViewModel = {};
        }
    };

    $scope.removeGraduation = function (index) {
        for (var i in $scope.Graduations) {
            if (index == i) {
                $scope.Graduations.splice(index, 1);
                break;
            }
        }
    };

    $scope.showViewPG = function (index, g) {
        if (g.GraduateType == "NonUSGraduate") {
            this.viewECFMGDetail = true;
        }
        else {
            this.viewECFMGDetail = false;
        }
        $scope.graduationFormStatus = true;
        $scope.showViewPGBtn = true;
        $scope.viewPG = true;
        $scope.graduationDetailViewModel = g;
        $scope.eCFMGDetailViewModel = $scope.ECFMGDetailViewModel;
        $scope.hideEditPGBtn = false;
        $scope.showingGDetails = false;
        $scope.newGraduationForm = false;
    }

    $scope.cancelViewPG = function () {
        $scope.graduationFormStatus = false;
        $scope.viewPG = false;
        $scope.showViewPGBtn = false;
        $scope.graduationFormStatus = false;
        $scope.hideEditPGBtn = false;
        $scope.showingGDetails = false;
    }


    //============ Internship Client Side MVC ==================
    $scope.programFormStatus = false;
    $scope.newProgramForm = false;

    $scope.showReason = function () {

        if (this.residencyInternshipViewModel.CompletedTrainingHere == "No") {
            this.reason = true;
        }
        else if (this.residencyInternshipViewModel.CompletedTrainingHere == "Yes") {
            this.reason = false;
        }
    }

    $scope.addProgram = function () {
        $scope.programFormStatus = false;
        $scope.newProgramForm = true;
        $scope.viewProgramForm = false;
        $scope.showPreview = false;
        $scope.showingIDetails = false;
        $scope.viewProgram = false;
        $scope.Program = {};
        // $("#newProgramFormDiv").html(dynamicFormGenerateService.getForm($scope, $("#programForm").html()));
    };

    $scope.saveProgram = function (Program) {
        //================== Save Here ============
        //$scope.Programs.push(Program);
        //================== hide Show Condition ============
        $scope.programFormStatus = false;
        $scope.newProgramForm = false;
        $scope.viewProgram = false;
        $scope.Program = {};
    };

    $scope.updateProgram = function (Program) {
        $scope.showingIDetails = false;
        $scope.programFormStatus = false;
        $scope.newProgramForm = false;
        $scope.Program = {};
    };

    $scope.editProgram = function (index, Program) {
        if (Program.CompletedTrainingHere == "No") {
            this.reason = true;
        }
        else if (Program.CompletedTrainingHere == "Yes") {
            this.reason = false;
        }
        this.showingIDetails = true;
        $scope.programFormStatus = true;
        $scope.newProgramForm = false;
        $scope.viewProgram = false;
        this.showViewProgramBtn = false;
        this.hideEditProgramBtn = true;
        $scope.showPreview = true;
        $scope.Program = Program;
        //$("#programEditDiv" + index).html(dynamicFormGenerateService.getForm($scope, $("#programForm").html()));
    };

    $scope.cancelProgram = function (condition) {
        if (condition == "editCancel") {
            $scope.cancelViewResidency();
            $scope.cancelResidency("editCancel");
            this.showingIDetails = false;
            $scope.programFormStatus = false;
            $scope.newProgramForm = false;
            this.hideEditProgramBtn = false;
            $scope.reason = false;
            $scope.Program = {};
        } else {
            $scope.cancelViewResidency();
            $scope.cancelResidency("saveCancel");
            this.showingIDetails = false;
            $scope.newProgramForm = false;
            $scope.newLegalNameForm = false;
            this.hideEditProgramBtn = false;
            $scope.programFormStatus = false;
            $scope.reason = false;
            $scope.Program = {};
        }
    };

    $scope.removeProgram = function (index) {
        for (var i in $scope.Programs) {
            if (index == i) {
                $scope.Programs.splice(index, 1);
                break;
            }
        }
    };

    $scope.showViewProgram = function (index, p) {
        if (p.CompletedTrainingHere == "No") {
            this.reason = true;
        }
        else if (p.CompletedTrainingHere == "Yes") {
            this.reason = false;
        }
        $scope.programFormStatus = false;
        this.viewProgram = true;
        this.showingIDetails = false;
        this.showViewProgramBtn = true;
        this.hideEditProgramBtn = false;
        $scope.programFormStatus = true;
        $scope.residencyInternshipViewModel = p;
        $scope.newProgramForm = false;


    }

    $scope.cancelViewProgram = function () {
        $scope.cancelViewResidency();
        $scope.cancelResidency("saveCancel");
        $scope.cancelResidency("editCancel");
        $scope.programFormStatus = false;
        this.viewProgram = false;
        this.showViewProgramBtn = false;
        $scope.programFormStatus = false;
        this.hideEditProgramBtn = false;
    }

    //======================Residency/Internship/Fellowship/Other================

    $scope.residencies = null;

    $scope.viewResidency = function (index, residency) {
        this.showViewResidency = true;
        $scope.showEditResidency = false;
        $scope.showAddResidency = false;
        $scope.hideViewBtn = true;
        $scope.hideAddBtn = true;
        $scope.hideEditBtn = false;
        $scope.residencyInternshipViewModel = residency;
    }

    $scope.editResidency = function (index, residency) {

        this.showEditResidency = true;
        $scope.showAddResidency = false;
        $scope.showViewResidency = false;
        $scope.showPreview = true;
        this.hideEditBtn = true;
        $scope.hideAddBtn = true;
        this.hideViewBtn = false;
        $scope.saveCancelBtn = false;
        $scope.editCancelBtn = true;
        $scope.addBtn = false;
        $scope.updateBtn = true;
        $scope.residencyInternshipViewModel = residency;
    }

    $scope.addResidency = function () {
        $scope.showAddResidency = true;
        $scope.showEditResidency = false;
        $scope.showViewResidency = false;
        $scope.hideAddBtn = true;
        $scope.hideEditBtn = false;
        $scope.hideViewBtn = false;
        $scope.saveCancelBtn = true;
        $scope.editCancelBtn = false;
        $scope.addBtn = true;
        $scope.updateBtn = false;
        $scope.showPreview = false;
        $scope.residencyInternshipViewModel = {};
    }


    $scope.cancelResidency = function (condition) {

        if (condition == "editCancel") {
            this.showEditResidency = false;
            this.hideViewBtn = false;
            this.hideEditBtn = false;
            $scope.hideAddBtn = false;
            $scope.saveCancelBtn = false;
            $scope.editCancelBtn = false;
            $scope.addBtn = false;
            $scope.updateBtn = false;
            $scope.showPreview = false;

        } else {
            this.showAddResidency = false;
            $scope.hideViewBtn = false;
            $scope.hideEditBtn = false;
            $scope.hideAddBtn = false;
            $scope.saveCancelBtn = false;
            $scope.editCancelBtn = false;
            $scope.addBtn = false;
            $scope.updateBtn = false;
            $scope.showPreview = false;

        }
        $scope.residencyInternshipViewModel = {};

    }

    $scope.cancelViewResidency = function () {
        this.showViewResidency = false;
        this.hideViewBtn = false;
        this.hideEditBtn = false;
        $scope.hideAddBtn = false;
        $scope.residencyInternshipViewModel = {};
    }

    $scope.viewResidency = function (index, residency) {

        this.showEditResidency = false;
        $scope.showAddResidency = false;
        this.showViewResidency = true;
        this.hideEditBtn = false;
        $scope.hideAddBtn = true;
        this.hideViewBtn = true;
        $scope.viewCancelBtn = true;
        $scope.editCancelBtn = false;
        $scope.addBtn = false;
        $scope.updateBtn = false;        
        $scope.ViewBtn = false;
        $scope.residencyInternshipViewModel = residency;
    }

    $scope.cancelResidencyFellowShip = function () {
        this.showViewResidency = false;
        $scope.ViewBtn = false;
        $scope.residencyInternshipViewModel = {};
    }


    //====================== Fifth Path Data =======================
    $scope.FifthPaths = [{
        HospitalName: "Hospital1",
        Street: "5350 Spring Hill Drive",
        State: "Florida",
        City: "Putname",
        Zipcode: "44544-_ _ _ _"
    }];

    $scope.FifthPathwayDetailViewModel = [{
        Institution: "Hospital1",
        Address: "Silk Board",
        Street: "5350 Spring Hill Drive",
        State: "Florida",
        Country: "USA",
        County: "Bethel",
        City: "Kipnuk",
        ZipCode: "44544-_ _ _ _",
        Telephone: "7846758898",
        Fax: "7846750078",
        CompletedEducationAtThisSchool: "No",
        StartDate: "mm/dd/yy",
        EndDate: "mm/dd/yy"
    }];

    //============ FifthPath Client Side MVC ==================
    $scope.fifthPathFormStatus = false;
    $scope.newFifthPathForm = false;

    $scope.addFifthPath = function () {
        $scope.fifthPathFormStatus = false;
        $scope.newFifthPathForm = true;
        $scope.viewFifthPathForm = false;
        $scope.viewFifth = false;
        $scope.FifthPath = {};
        //$("#newFifthPathFormDiv").html(dynamicFormGenerateService.getForm($scope, $("#fifthPathForm").html()));
    };

    $scope.saveFifthPath = function (FifthPath) {
        //================== Save Here ============
        //$scope.FifthPaths.push(FifthPath);
        //================== hide Show Condition ============
        $scope.fifthPathFormStatus = false;
        $scope.newFifthPathForm = false;
        $scope.FifthPath = {};
    };

    $scope.updateFifthPath = function (FifthPath) {
        $scope.showingDetails = false;
        $scope.fifthPathFormStatus = false;
        $scope.newFifthPathForm = false;
        $scope.FifthPath = {};
    };

    $scope.editFifthPath = function (index, FifthPath) {
        $scope.showingDetails = true;
        $scope.fifthPathFormStatus = true;
        $scope.newFifthPathForm = false;
        $scope.viewFifthPathForm = false;
        $scope.viewFifth = false;
        $scope.showViewFifthBtn = false;
        $scope.hideEditFifthBtn = true;
        $scope.FifthPath = FifthPath;
        //$("#fifthPathEditDiv" + index).html(dynamicFormGenerateService.getForm($scope, $("#fifthPathForm").html()));
    };

    $scope.cancelFifthPath = function (condition) {
        if (condition == "editCancel") {
            $scope.showingDetails = false;
            $scope.fifthPathFormStatus = false;
            $scope.newFifthPathForm = false;
            $scope.hideEditFifthBtn = false;
            $scope.FifthPath = {};
        } else {
            $scope.showingDetails = false;
            $scope.newFifthPathForm = false;
            $scope.newLegalNameForm = false;
            $scope.hideEditFifthBtn = false;
            $scope.FifthPath = {};
        }
    };

    $scope.removeFifthPath = function (index) {
        for (var i in $scope.FifthPaths) {
            if (index == i) {
                $scope.FifthPaths.splice(index, 1);
                break;
            }
        }
    };

    $scope.cancelViewFifth = function () {
        $scope.fifthPathFormStatus = false;
        $scope.viewFifth = false;
        $scope.showViewFifthBtn = false;
        $scope.showingDetails = false;
        $scope.newFifthPathForm = false;
        $scope.fifthPathFormStatus = false;
        $scope.hideEditFifthBtn = false;
    }

    $scope.showViewFifth = function (index, fp) {
        $scope.fifthPathFormStatus = false;
        $scope.viewFifth = true;
        $scope.showViewFifthBtn = true;
        $scope.fifthPathFormStatus = true;
        $scope.fifthPathwayDetailViewModel = fp;
        $scope.showingDetails = false;
        $scope.hideEditFifthBtn = false;

    }

    $("#selectDegree").select2({
        placeholder: "Select a Degree"
    });

    function resetCitySelection(value) {
        $("#selectDegree").select2("val", value);
    }

    $("#selectCity").select2({
        placeholder: "Select a City"
    });

    function resetCitySelection(value) {
        $("#selectCity").select2("val", value);
    }




    //======================Certificate==========================

    
    //$scope.Certificates = [{
    //    CertificateName: "Basic Life Support",
    //    ExpiryDate: new Date(2016, 07, 01),
    //    CreditHours: "",
    //    Number: "",
    //    Building: "",
    //    Street: "",
    //    Country: "",
    //    State: "",
    //    City: "",
    //    ZipCode: "",
    //    County: "",
    //    StartDate: "",
    //    EndDate: "",
    //    DegreeAwarded: "",
    //    InstituteName:"",
    //    SponsorName:""
    //}, {
    //    CertificateName: "#4090 ACUTE CORONARY SYNDROME",
    //    ExpiryDate: "",
    //    CreditHours: "15.00",
    //    Number: "",
    //    Building: "",
    //    Street: "",
    //    Country: "",
    //    State: "",
    //    City: "",
    //    ZipCode: "",
    //    County: "",
    //    StartDate: new Date(2009, 10, 16),
    //    EndDate: new Date(2009, 10, 16),
    //    DegreeAwarded: "",
    //    InstituteName: "",
    //    SponsorName: "CONTINUE MEDICAL EDUCATION"
    //}, {
    //    CertificateName: "MORE EVIDENCE THAT NSAIDs ARE HARMFUL TO ",
    //    ExpiryDate: "",
    //    CreditHours: "0.25",
    //    Number: "",
    //    Building: "",
    //    Street: "",
    //    Country: "",
    //    State: "",
    //    City: "",
    //    ZipCode: "",
    //    County: "",
    //    StartDate: new Date(2009, 10, 24),
    //    EndDate: new Date(2009, 10, 24),
    //    DegreeAwarded: "",
    //    InstituteName: "",
    //    SponsorName: "MEDSCAPE CME"
    //}, {
    //    CertificateName: "HEAAL: HIGH-DOSE TRUMPS LOW-DOSE ARB FOR H",
    //    ExpiryDate: "",
    //    CreditHours: "0.25",
    //    Number: "",
    //    Building: "",
    //    Street: "",
    //    Country: "",
    //    State: "",
    //    City: "",
    //    ZipCode: "",
    //    County: "",
    //    StartDate: new Date(2009, 10, 25),
    //    EndDate: new Date(2009, 10, 25),
    //    DegreeAwarded: "",
    //    InstituteName: "",
    //    SponsorName: "MEDSCAPE CME"
    //}, {
    //    CertificateName: "NATRIURETIC PEPTIDS IN HEART FAILURE: SHOULD",
    //    ExpiryDate: "",
    //    CreditHours: "0.75",
    //    Number: "",
    //    Building: "",
    //    Street: "",
    //    Country: "",
    //    State: "",
    //    City: "",
    //    ZipCode: "",
    //    County: "",
    //    StartDate: new Date(2009, 10, 30),
    //    EndDate: new Date(2009, 10, 30),
    //    DegreeAwarded: "",
    //    InstituteName: "",
    //    SponsorName: "MEDSCAPE CME"
    //}, {
    //    CertificateName: "DASH DIET MAY HELP PREVENT  HEART FAILURE IN",
    //    ExpiryDate: "",
    //    CreditHours: "0.25",
    //    Number: "",
    //    Building: "",
    //    Street: "",
    //    Country: "",
    //    State: "",
    //    City: "",
    //    ZipCode: "",
    //    County: "",
    //    StartDate: new Date(2009, 10, 30),
    //    EndDate: new Date(2009, 10, 30),
    //    DegreeAwarded: "",
    //    InstituteName: "",
    //    SponsorName: "MEDSCAPE CME"
    //}, {
    //    CertificateName: "ACC 2009: HF-ACTION ON EXECRISE TRAINING IN H",
    //    ExpiryDate: "",
    //    CreditHours: "0.25",
    //    Number: "",
    //    Building: "",
    //    Street: "",
    //    Country: "",
    //    State: "",
    //    City: "",
    //    ZipCode: "",
    //    County: "",
    //    StartDate: new Date(2009, 10, 30),
    //    EndDate: new Date(2009, 10, 30),
    //    DegreeAwarded: "",
    //    InstituteName: "",
    //    SponsorName: "MEDSCAPE"
    //}, {
    //    CertificateName: "TIME-CHF: FUTURE OF BIOMARKER-GUIDED HEART",
    //    ExpiryDate: "",
    //    CreditHours: "0.25",
    //    Number: "",
    //    Building: "",
    //    Street: "",
    //    Country: "",
    //    State: "",
    //    City: "",
    //    ZipCode: "",
    //    County: "",
    //    StartDate: new Date(2009, 10, 30),
    //    EndDate: new Date(2009, 10, 30),
    //    DegreeAwarded: "",
    //    InstituteName: "",
    //    SponsorName: "MEDSCAPE"
    //}, {
    //    CertificateName: "#9441 INFLUENZA:A COMPREHENSIVE REVIEW",
    //    ExpiryDate: "",
    //    CreditHours: "10.00",
    //    Number: "",
    //    Building: "",
    //    Street: "",
    //    Country: "",
    //    State: "",
    //    City: "",
    //    ZipCode: "",
    //    County: "",
    //    StartDate: new Date(2009, 10, 29),
    //    EndDate: new Date(2009, 10, 29),
    //    DegreeAwarded: "",
    //    InstituteName: "",
    //    SponsorName: "CONTINUE MEDICAL EDUCATION"
    //}, {
    //    CertificateName: "#9787 DOMESTIC VIOLENCE",
    //    ExpiryDate: "",
    //    CreditHours: "2.00",
    //    Number: "",
    //    Building: "",
    //    Street: "",
    //    Country: "",
    //    State: "",
    //    City: "",
    //    ZipCode: "",
    //    County: "",
    //    StartDate: new Date(2009, 11, 31),
    //    EndDate: new Date(2009, 11, 31),
    //    DegreeAwarded: "",
    //    InstituteName: "",
    //    SponsorName: "CONTINUE MEDICAL EDUCATION"
    //}, {
    //    CertificateName: "#9182 OPIATE ABUSE AND DEPENDENCE",
    //    ExpiryDate: "",
    //    CreditHours: "10.00",
    //    Number: "",
    //    Building: "",
    //    Street: "",
    //    Country: "",
    //    State: "",
    //    City: "",
    //    ZipCode: "",
    //    County: "",
    //    StartDate: new Date(2009, 11, 31),
    //    EndDate: new Date(2009, 11, 31),
    //    DegreeAwarded: "",
    //    InstituteName: "",
    //    SponsorName: "CONTINUE MEDICAL EDUCATION"
    //}, {
    //    CertificateName: "American Safety & Health Institute Training Program",
    //    ExpiryDate: "",
    //    CreditHours: "8.00",
    //    Number: "",
    //    Building: "",
    //    Street: "",
    //    Country: "",
    //    State: "",
    //    City: "",
    //    ZipCode: "",
    //    County: "",
    //    StartDate: new Date(2010, 03, 17),
    //    EndDate: new Date(2010, 03, 17),
    //    DegreeAwarded: "",
    //    InstituteName: "",
    //    SponsorName: "David Akers Training Center"
    //}, {
    //    CertificateName: "#9042 Seizures and Epilepsy Syndromes",
    //    ExpiryDate: "",
    //    CreditHours: "10.00",
    //    Number: "",
    //    Building: "",
    //    Street: "",
    //    Country: "",
    //    State: "",
    //    City: "",
    //    ZipCode: "",
    //    County: "",
    //    StartDate: new Date(2011, 05, 29),
    //    EndDate: new Date(2011, 05, 29),
    //    DegreeAwarded: "",
    //    InstituteName: "",
    //    SponsorName: "CME Resource"
    //}, {
    //    CertificateName: "#9133 Medical Error Prevention",
    //    ExpiryDate: "",
    //    CreditHours: "2.00",
    //    Number: "",
    //    Building: "",
    //    Street: "",
    //    Country: "",
    //    State: "",
    //    City: "",
    //    ZipCode: "",
    //    County: "",
    //    StartDate: new Date(2011, 05, 30),
    //    EndDate: new Date(2011, 05, 30),
    //    DegreeAwarded: "",
    //    InstituteName: "",
    //    SponsorName: "CME Resource"
    //}, {
    //    CertificateName: "#4149 Risk Management",
    //    ExpiryDate: "",
    //    CreditHours: "5.00",
    //    Number: "",
    //    Building: "",
    //    Street: "",
    //    Country: "",
    //    State: "",
    //    City: "",
    //    ZipCode: "",
    //    County: "",
    //    StartDate: new Date(2011, 05, 30),
    //    EndDate: new Date(2011, 05, 30),
    //    DegreeAwarded: "",
    //    InstituteName: "",
    //    SponsorName: "CME Resource"
    //}, {
    //    CertificateName: "#9478 HIV/AIDS: Epidemic Update for Florida",
    //    ExpiryDate: "",
    //    CreditHours: "1.00",
    //    Number: "",
    //    Building: "",
    //    Street: "",
    //    Country: "",
    //    State: "",
    //    City: "",
    //    ZipCode: "",
    //    County: "",
    //    StartDate: new Date(2011, 06, 07),
    //    EndDate: new Date(2011, 06, 07),
    //    DegreeAwarded: "",
    //    InstituteName: "",
    //    SponsorName: "CME Resource"
    //}, {
    //    CertificateName: "#9478 HIV/AIDS: Epidemic Update for Florida",
    //    ExpiryDate: "",
    //    CreditHours: "1.00",
    //    Number: "",
    //    Building: "",
    //    Street: "",
    //    Country: "",
    //    State: "",
    //    City: "",
    //    ZipCode: "",
    //    County: "",
    //    StartDate: new Date(2011, 06, 08),
    //    EndDate: new Date(2011, 06, 08),
    //    DegreeAwarded: "",
    //    InstituteName: "",
    //    SponsorName: "CME"
    //}, {
    //    CertificateName: "#9084 Hyperlipidemias and Cardiovascular Disease",
    //    ExpiryDate: "",
    //    CreditHours: "10.00",
    //    Number: "",
    //    Building: "",
    //    Street: "",
    //    Country: "",
    //    State: "",
    //    City: "",
    //    ZipCode: "",
    //    County: "",
    //    StartDate: new Date(2011, 06, 08),
    //    EndDate: new Date(2011, 06, 08),
    //    DegreeAwarded: "",
    //    InstituteName: "",
    //    SponsorName: "CME Resource"
    //}, {
    //    CertificateName: "#9421 Basics of Bacterial Resistance",
    //    ExpiryDate: "",
    //    CreditHours: "4.00",
    //    Number: "",
    //    Building: "",
    //    Street: "",
    //    Country: "",
    //    State: "",
    //    City: "",
    //    ZipCode: "",
    //    County: "",
    //    StartDate: new Date(2011, 10, 18),
    //    EndDate: new Date(2011, 10, 18),
    //    DegreeAwarded: "",
    //    InstituteName: "",
    //    SponsorName: "CME Resource"
    //}, {
    //    CertificateName: "#9787 Domestic Violence",
    //    ExpiryDate: "",
    //    CreditHours: "2.00",
    //    Number: "",
    //    Building: "",
    //    Street: "",
    //    Country: "",
    //    State: "",
    //    City: "",
    //    ZipCode: "",
    //    County: "",
    //    StartDate: new Date(2012, 12, 16),
    //    EndDate: new Date(2012, 12, 16),
    //    DegreeAwarded: "",
    //    InstituteName: "",
    //    SponsorName: "CME"
    //}, {
    //    CertificateName: "#9037 Clinical Management of ventricular Arrythmias",
    //    ExpiryDate: "",
    //    CreditHours: "15.00",
    //    Number: "",
    //    Building: "",
    //    Street: "",
    //    Country: "",
    //    State: "",
    //    City: "",
    //    ZipCode: "",
    //    County: "",
    //    StartDate: new Date(2012, 05, 24),
    //    EndDate: new Date(2012, 05, 24),
    //    DegreeAwarded: "",
    //    InstituteName: "",
    //    SponsorName: "CME"
    //}, {
    //    CertificateName: "#9445 Autoimmune Diseases",
    //    ExpiryDate: "",
    //    CreditHours: "15.00",
    //    Number: "",
    //    Building: "",
    //    Street: "",
    //    Country: "",
    //    State: "",
    //    City: "",
    //    ZipCode: "",
    //    County: "",
    //    StartDate: new Date(2012, 06, 03),
    //    EndDate: new Date(2012, 06, 03),
    //    DegreeAwarded: "",
    //    InstituteName: "",
    //    SponsorName: "CME"
    //}, {
    //    CertificateName: "#9492 Animal-Related Health Risks",
    //    ExpiryDate: "",
    //    CreditHours: "15.00",
    //    Number: "",
    //    Building: "",
    //    Street: "",
    //    Country: "",
    //    State: "",
    //    City: "",
    //    ZipCode: "",
    //    County: "",
    //    StartDate: new Date(2012, 06, 16),
    //    EndDate: new Date(2012, 06, 16),
    //    DegreeAwarded: "",
    //    InstituteName:"",
    //    SponsorName:"CME"
    //}]


    //=============== Certificate Conditions ==================
    $scope.certificateFormStatus = false;
    $scope.newCertificateForm = false;

    $scope.showCertificateTextBox = function () {
        if (this.certificationCMEViewModel.CertificateName == "Others") {
            this.certificateTextBox = true;
        }
        else {
            this.certificateTextBox = false;
        }
    }

    $scope.addCertificate = function () {
        $scope.certificateFormStatus = false;
        $scope.newCertificateForm = true;
        $scope.showPreview = false;
        $scope.hideEditBtn = false;
        $scope.certificationCMEViewModel = {};
        //$("#newCertificateFormDiv").html(dynamicFormGenerateService.getForm($scope, $("#certificateForm").html()));
    };

    $scope.saveCertificate = function (Certificate) {
        //================== Save Here ============

        //================== hide Show Condition ============
        $scope.certificateFormStatus = false;
        $scope.newCertificateForm = false;
        $scope.Certificate = {};
    };

    $scope.updateCertificate = function (Certificate) {
        $scope.showingCDetails = false;
        $scope.certificateFormStatus = false;
        $scope.newCertificateForm = false;
        $scope.Certificate = {};
    };

    $scope.editCertificate = function (index, Certificate) {
        this.showingCDetails = true;
        $scope.certificateFormStatus = true;
        $scope.newCertificateForm = false;
        $scope.hideEditBtn = true;
        $scope.showViewCMEBtn = false;
        $scope.viewCME = false;
        $scope.showPreview = true;
        $scope.certificationCMEViewModel = certificate;
        //$("#certificateEditDiv" + index).html(dynamicFormGenerateService.getForm($scope, $("#certificateForm").html()));
    };

    $scope.cancelCertificate = function (condition) {
        if (condition == "editCancel") {
            this.showingCDetails = false;
            $scope.certificateFormStatus = false;
            $scope.newCertificateForm = false;
            $scope.hideEditBtn = false;
            $scope.showViewCMEBtn = false;
            $scope.certificateTextBox = false;
            $scope.certificationCMEViewModel = {};
        } else {
            $scope.showingCDetails = false;
            $scope.newCertificateForm = false;
            $scope.newLegalNameForm = false;
            $scope.hideEditBtn = false;
            $scope.showViewCMEBtn = false;
            $scope.certificateTextBox = false;
            $scope.certificationCMEViewModel = {};
        }
    };

    $scope.removeCertificate = function (index) {
        for (var i in $scope.Certificate) {
            if (index == i) {
                $scope.Certificate.splice(index, 1);
                break;
            }
        }
    };

    $scope.showViewCME = function (index, certificate) {
        $scope.showingCDetails = false;
        $scope.certificateFormStatus = true;
        $scope.newCertificateForm = false;
        this.viewCME = true;
        $scope.showViewCMEBtn = true;
        $scope.certificationCMEViewModel = certificate;
        $scope.hideEditBtn = false;
    }

    $scope.cancelViewCME = function () {
        $scope.showingCDetails = false;
        $scope.certificateFormStatus = false;
        $scope.newCertificateForm = false;
        this.viewCME = false;
        $scope.showViewCMEBtn = false;
        $scope.certificationCMEViewModel = {};
    }

    //======================Other==========================

    $scope.Others = [{
        Degree: "MBBS",
        Institute: "American Board",
        CerdictHours: "3",
    }];

    $scope.showingDetails = false;

    //=============== Other Conditions ==================
    $scope.otherFormStatus = false;
    $scope.newOtherForm = false;

    $scope.addOther = function () {
        $scope.otherFormStatus = false;
        $scope.newOtherForm = true;
        $scope.Other = {};
        $("#newOtherFormDiv").html(dynamicFormGenerateService.getForm($scope, $("#otherForm").html()));
    };

    $scope.saveOther = function (Other) {
        //================== Save Here ============

        //================== hide Show Condition ============
        $scope.otherFormStatus = false;
        $scope.newOtherForm = false;
        $scope.Other = {};
    };

    $scope.updateOther = function (Other) {
        $scope.showingODetails = false;
        $scope.otherFormStatus = false;
        $scope.newOtherForm = false;
        $scope.Other = {};
    };

    $scope.editOther = function (index, Other) {
        $scope.showingODetails = true;
        $scope.otherFormStatus = true;
        $scope.newOtherForm = false;
        $scope.Other = Other;
        $("#otherEditDiv" + index).html(dynamicFormGenerateService.getForm($scope, $("#otherForm").html()));
    };

    $scope.cancelOther = function (condition) {
        if (condition == "editCancel") {
            $scope.showingODetails = false;
            $scope.otherFormStatus = false;
            $scope.newOtherForm = false;
            $scope.Other = {};
        } else {
            $scope.showingODetails = false;
            $scope.newOtherForm = false;
            $scope.newLegalNameForm = false;
            $scope.Other = {};
        }
    };

    $scope.removeOther = function (index) {
        for (var i in $scope.Other) {
            if (index == i) {
                $scope.Other.splice(index, 1);
                break;
            }
        }
    };

    $scope.Certificates = [{
        CertificateName: "Basic Life Support",
        ExpiryDate: new Date(2016, 07, 01),
        CreditHours: "",
        Number: "",
        Building: "",
        Street: "",
        Country: "",
        State: "",
        City: "",
        ZipCode: "",
        County: "",
        StartDate: "",
        EndDate: "",
        DegreeAwarded: "",
        InstituteName: "",
        SponsorName: ""
    },
    {
        CertificateName: "#9134 Medical Error Prevention And Root CA",
        ExpiryDate: "",
        CreditHours: "2.00",
        Number: "",
        Building: "",
        Street: "",
        Country: "",
        State: "",
        City: "",
        ZipCode: "",
        County: "",
        StartDate: new Date(2007, 10, 23),
        EndDate: new Date(2007, 10, 23),
        DegreeAwarded: "",
        InstituteName: "",
        SponsorName: "Continue Medical Education"
    },
    {
        CertificateName: "#9043 A Review Of Interventional Radiology",
        ExpiryDate: "",
        CreditHours: "10.00",
        Number: "",
        Building: "",
        Street: "",
        Country: "",
        State: "",
        City: "",
        ZipCode: "",
        County: "",
        StartDate: new Date(2007, 10, 23),
        EndDate: new Date(2007, 10, 23),
        DegreeAwarded: "",
        InstituteName: "",
        SponsorName: "Continue Medical Education"
    },
    {
        CertificateName: "Immunomodulation Helps Certain Patients",
        ExpiryDate: "",
        CreditHours: "0.25",
        Number: "",
        Building: "",
        Street: "",
        Country: "",
        State: "",
        City: "",
        ZipCode: "",
        County: "",
        StartDate: new Date(2008, 01, 20),
        EndDate: new Date(2008, 01, 20),
        DegreeAwarded: "",
        InstituteName: "",
        SponsorName: "University Of Pensilvania School Of Medicine"
    },
    {
        CertificateName: "Patients: Friend Or Foe?",
        ExpiryDate: "",
        CreditHours: "1.00",
        Number: "",
        Building: "",
        Street: "",
        Country: "",
        State: "",
        City: "",
        ZipCode: "",
        County: "",
        StartDate: new Date(2008, 04, 28),
        EndDate: new Date(2008, 04, 28),
        DegreeAwarded: "",
        InstituteName: "",
        SponsorName: "Oak Hill Hospital"
    },
    {
        CertificateName: "#9838 Herbal Medications: An Evidence",
        ExpiryDate: "",
        CreditHours: "10.00",
        Number: "",
        Building: "",
        Street: "",
        Country: "",
        State: "",
        City: "",
        ZipCode: "",
        County: "",
        StartDate: new Date(2008, 08, 29),
        EndDate: new Date(2008, 08, 29),
        DegreeAwarded: "",
        InstituteName: "",
        SponsorName: "Continue Medical Education"
    },
    {
        CertificateName: "#9793 Domestic Violence: The Florida",
        ExpiryDate: "",
        CreditHours: "2.00",
        Number: "",
        Building: "",
        Street: "",
        Country: "",
        State: "",
        City: "",
        ZipCode: "",
        County: "",
        StartDate: new Date(2008, 09, 03),
        EndDate: new Date(2008, 09, 03),
        DegreeAwarded: "",
        InstituteName: "",
        SponsorName: "Continue Medical Education"
    },
    {
        CertificateName: "76-Year Old Women Presenting With Shortan",
        ExpiryDate: "",
        CreditHours: "2.00",
        Number: "",
        Building: "",
        Street: "",
        Country: "",
        State: "",
        City: "",
        ZipCode: "",
        County: "",
        StartDate: new Date(2009, 01, 10),
        EndDate: new Date(2009, 01, 10),
        DegreeAwarded: "",
        InstituteName: "",
        SponsorName: "Pri-Med Institute"
    },
    {
        CertificateName: "Patient With Preexisting Renal Disease ANI",
        ExpiryDate: "",
        CreditHours: "2.00",
        Number: "",
        Building: "",
        Street: "",
        Country: "",
        State: "",
        City: "",
        ZipCode: "",
        County: "",
        StartDate: new Date(2009, 01, 11),
        EndDate: new Date(2009, 01, 11),
        DegreeAwarded: "",
        InstituteName: "",
        SponsorName: "Pri-Med Institute"
    },
    //},
    {
        CertificateName: "Diagnosis And Treatment Of Alzheimers Disease",
        ExpiryDate: "",
        CreditHours: "2.00",
        Number: "",
        Building: "",
        Street: "",
        Country: "",
        State: "",
        City: "",
        ZipCode: "",
        County: "",
        StartDate: new Date(2009, 01, 12),
        EndDate: new Date(2009, 01, 12),
        DegreeAwarded: "",
        InstituteName: "",
        SponsorName: "Pri-Med Institute"
    },
    {
        CertificateName: "63-Year Old Male With Type 2Diabetes And C",
        ExpiryDate: "",
        CreditHours: "2.00",
        Number: "",
        Building: "",
        Street: "",
        Country: "",
        State: "",
        City: "",
        ZipCode: "",
        County: "",
        StartDate: new Date(2009, 01, 13),
        EndDate: new Date(2009, 01, 13),
        DegreeAwarded: "",
        InstituteName: "",
        SponsorName: "Pri-Med Institute"
    },
    {
        CertificateName: "#9486 HIV/AIDS: Epidemic",
        ExpiryDate: "",
        CreditHours: "1.00",
        Number: "",
        Building: "",
        Street: "",
        Country: "",
        State: "",
        City: "",
        ZipCode: "",
        County: "",
        StartDate: new Date(2009, 10, 16),
        EndDate: new Date(2009, 10, 16),
        DegreeAwarded: "",
        InstituteName: "",
        SponsorName: "Continue Medical Education"
    }, {
        CertificateName: "#4090 Acute Coronary Syndrome",
        ExpiryDate: "",
        CreditHours: "15.00",
        Number: "",
        Building: "",
        Street: "",
        Country: "",
        State: "",
        City: "",
        ZipCode: "",
        County: "",
        StartDate: new Date(2009, 10, 16),
        EndDate: new Date(2009, 10, 16),
        DegreeAwarded: "",
        InstituteName: "",
        SponsorName: "Continue Medical Education"
    }, {
        CertificateName: "More Evidence That NSAIDs Are Harmful To ",
        ExpiryDate: "",
        CreditHours: "0.25",
        Number: "",
        Building: "",
        Street: "",
        Country: "",
        State: "",
        City: "",
        ZipCode: "",
        County: "",
        StartDate: new Date(2009, 10, 24),
        EndDate: new Date(2009, 10, 24),
        DegreeAwarded: "",
        InstituteName: "",
        SponsorName: "Medscape CME"
    }, {
        CertificateName: "HEAAL: High-Dose Trumps Low-Dose ARB For H",
        ExpiryDate: "",
        CreditHours: "0.25",
        Number: "",
        Building: "",
        Street: "",
        Country: "",
        State: "",
        City: "",
        ZipCode: "",
        County: "",
        StartDate: new Date(2009, 10, 25),
        EndDate: new Date(2009, 10, 25),
        DegreeAwarded: "",
        InstituteName: "",
        SponsorName: "Medscape CME"
    }, {
        CertificateName: "Natriuretic Peptides In Heart Failure: Should",
        ExpiryDate: "",
        CreditHours: "0.75",
        Number: "",
        Building: "",
        Street: "",
        Country: "",
        State: "",
        City: "",
        ZipCode: "",
        County: "",
        StartDate: new Date(2009, 10, 30),
        EndDate: new Date(2009, 10, 30),
        DegreeAwarded: "",
        InstituteName: "",
        SponsorName: "Medscape CME"
    }, {
        CertificateName: "Dash Diet May Help Prevent  Heart Failure In",
        ExpiryDate: "",
        CreditHours: "0.25",
        Number: "",
        Building: "",
        Street: "",
        Country: "",
        State: "",
        City: "",
        ZipCode: "",
        County: "",
        StartDate: new Date(2009, 10, 30),
        EndDate: new Date(2009, 10, 30),
        DegreeAwarded: "",
        InstituteName: "",
        SponsorName: "Medscape CME"
    }, {
        CertificateName: "ACC 2009: HF-Action On Exercise Training In H",
        ExpiryDate: "",
        CreditHours: "0.25",
        Number: "",
        Building: "",
        Street: "",
        Country: "",
        State: "",
        City: "",
        ZipCode: "",
        County: "",
        StartDate: new Date(2009, 10, 30),
        EndDate: new Date(2009, 10, 30),
        DegreeAwarded: "",
        InstituteName: "",
        SponsorName: "Medscape"
    }, {
        CertificateName: "TIME-CHF: Future Of Biomarker-Guided Heart",
        ExpiryDate: "",
        CreditHours: "0.25",
        Number: "",
        Building: "",
        Street: "",
        Country: "",
        State: "",
        City: "",
        ZipCode: "",
        County: "",
        StartDate: new Date(2009, 10, 30),
        EndDate: new Date(2009, 10, 30),
        DegreeAwarded: "",
        InstituteName: "",
        SponsorName: "Medscape"
    }, {
        CertificateName: "#9441 INnfluenza:A Comprehensive Review",
        ExpiryDate: "",
        CreditHours: "10.00",
        Number: "",
        Building: "",
        Street: "",
        Country: "",
        State: "",
        City: "",
        ZipCode: "",
        County: "",
        StartDate: new Date(2009, 10, 29),
        EndDate: new Date(2009, 10, 29),
        DegreeAwarded: "",
        InstituteName: "",
        SponsorName: "Continue Medical Education"
    }, {
        CertificateName: "#9787 Domestic Violence",
        ExpiryDate: "",
        CreditHours: "2.00",
        Number: "",
        Building: "",
        Street: "",
        Country: "",
        State: "",
        City: "",
        ZipCode: "",
        County: "",
        StartDate: new Date(2009, 11, 31),
        EndDate: new Date(2009, 11, 31),
        DegreeAwarded: "",
        InstituteName: "",
        SponsorName: "Continue Medical Education"
    }, {
        CertificateName: "#9182 Opiate Abuse And Dependence",
        ExpiryDate: "",
        CreditHours: "10.00",
        Number: "",
        Building: "",
        Street: "",
        Country: "",
        State: "",
        City: "",
        ZipCode: "",
        County: "",
        StartDate: new Date(2009, 11, 31),
        EndDate: new Date(2009, 11, 31),
        DegreeAwarded: "",
        InstituteName: "",
        SponsorName: "Continue Medical Education"
    }, {
        CertificateName: "American Safety & Health Institute Training Program",
        ExpiryDate: "",
        CreditHours: "8.00",
        Number: "",
        Building: "",
        Street: "",
        Country: "",
        State: "",
        City: "",
        ZipCode: "",
        County: "",
        StartDate: new Date(2010, 03, 17),
        EndDate: new Date(2010, 03, 17),
        DegreeAwarded: "",
        InstituteName: "",
        SponsorName: "David Akers Training Center"
    }, {
        CertificateName: "#9042 Seizures and Epilepsy Syndromes",
        ExpiryDate: "",
        CreditHours: "10.00",
        Number: "",
        Building: "",
        Street: "",
        Country: "",
        State: "",
        City: "",
        ZipCode: "",
        County: "",
        StartDate: new Date(2011, 05, 29),
        EndDate: new Date(2011, 05, 29),
        DegreeAwarded: "",
        InstituteName: "",
        SponsorName: "CME Resource"
    }, {
        CertificateName: "#9133 Medical Error Prevention",
        ExpiryDate: "",
        CreditHours: "2.00",
        Number: "",
        Building: "",
        Street: "",
        Country: "",
        State: "",
        City: "",
        ZipCode: "",
        County: "",
        StartDate: new Date(2011, 05, 30),
        EndDate: new Date(2011, 05, 30),
        DegreeAwarded: "",
        InstituteName: "",
        SponsorName: "CME Resource"
    }, {
        CertificateName: "#4149 Risk Management",
        ExpiryDate: "",
        CreditHours: "5.00",
        Number: "",
        Building: "",
        Street: "",
        Country: "",
        State: "",
        City: "",
        ZipCode: "",
        County: "",
        StartDate: new Date(2011, 05, 30),
        EndDate: new Date(2011, 05, 30),
        DegreeAwarded: "",
        InstituteName: "",
        SponsorName: "CME Resource"
    }, {
        CertificateName: "#9478 HIV/AIDS: Epidemic Update for Florida",
        ExpiryDate: "",
        CreditHours: "1.00",
        Number: "",
        Building: "",
        Street: "",
        Country: "",
        State: "",
        City: "",
        ZipCode: "",
        County: "",
        StartDate: new Date(2011, 06, 07),
        EndDate: new Date(2011, 06, 07),
        DegreeAwarded: "",
        InstituteName: "",
        SponsorName: "CME Resource"
    }, {
        CertificateName: "#9478 HIV/AIDS: Epidemic Update for Florida",
        ExpiryDate: "",
        CreditHours: "1.00",
        Number: "",
        Building: "",
        Street: "",
        Country: "",
        State: "",
        City: "",
        ZipCode: "",
        County: "",
        StartDate: new Date(2011, 06, 08),
        EndDate: new Date(2011, 06, 08),
        DegreeAwarded: "",
        InstituteName: "",
        SponsorName: "CME"
    }, {
        CertificateName: "#9084 Hyperlipidemias and Cardiovascular Disease",
        ExpiryDate: "",
        CreditHours: "10.00",
        Number: "",
        Building: "",
        Street: "",
        Country: "",
        State: "",
        City: "",
        ZipCode: "",
        County: "",
        StartDate: new Date(2011, 06, 08),
        EndDate: new Date(2011, 06, 08),
        DegreeAwarded: "",
        InstituteName: "",
        SponsorName: "CME Resource"
    }, {
        CertificateName: "#9421 Basics of Bacterial Resistance",
        ExpiryDate: "",
        CreditHours: "4.00",
        Number: "",
        Building: "",
        Street: "",
        Country: "",
        State: "",
        City: "",
        ZipCode: "",
        County: "",
        StartDate: new Date(2011, 10, 18),
        EndDate: new Date(2011, 10, 18),
        DegreeAwarded: "",
        InstituteName: "",
        SponsorName: "CME Resource"
    }, {
        CertificateName: "#9787 Domestic Violence",
        ExpiryDate: "",
        CreditHours: "2.00",
        Number: "",
        Building: "",
        Street: "",
        Country: "",
        State: "",
        City: "",
        ZipCode: "",
        County: "",
        StartDate: new Date(2012, 12, 16),
        EndDate: new Date(2012, 12, 16),
        DegreeAwarded: "",
        InstituteName: "",
        SponsorName: "CME"
    }, {
        CertificateName: "#9037 Clinical Management of ventricular Arrhythmias",
        ExpiryDate: "",
        CreditHours: "15.00",
        Number: "",
        Building: "",
        Street: "",
        Country: "",
        State: "",
        City: "",
        ZipCode: "",
        County: "",
        StartDate: new Date(2012, 05, 24),
        EndDate: new Date(2012, 05, 24),
        DegreeAwarded: "",
        InstituteName: "",
        SponsorName: "CME"
    }, {
        CertificateName: "#9445 Autoimmune Diseases",
        ExpiryDate: "",
        CreditHours: "15.00",
        Number: "",
        Building: "",
        Street: "",
        Country: "",
        State: "",
        City: "",
        ZipCode: "",
        County: "",
        StartDate: new Date(2012, 06, 03),
        EndDate: new Date(2012, 06, 03),
        DegreeAwarded: "",
        InstituteName: "",
        SponsorName: "CME"
    }, {
        CertificateName: "#9492 Animal-Related Health Risks",
        ExpiryDate: "",
        CreditHours: "15.00",
        Number: "",
        Building: "",
        Street: "",
        Country: "",
        State: "",
        City: "",
        ZipCode: "",
        County: "",
        StartDate: new Date(2012, 06, 16),
        EndDate: new Date(2012, 06, 16),
        DegreeAwarded: "",
        InstituteName: "",
        SponsorName: "CME"
        }
    ]
});