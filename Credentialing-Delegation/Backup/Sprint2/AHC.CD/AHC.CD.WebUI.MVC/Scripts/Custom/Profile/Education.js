
//=========================== Controller declaration ==========================
profileApp.controller('educationController', function ($scope, $http, dynamicFormGenerateService) {

    //============== Graduation Data  ================
    $scope.Graduations = [{
        GraduateType: "U.S. Graduate",
        School: {
            SchoolCode: "SC7548",
            SchoolName: "Whiteney School",
            DegreeAwarded: "DegreeAwaarded1",
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
            ZipCode: "67654"
        },
        ECFMG: {
            Number: "5354",
            IssueDate: "11-08-2011",
            Certificate: "certificat.pdf"
        }
    }];
    //============== Residency/ InternShip  ================
    $scope.Programs = [{
        ProgramType: "Internship",
        School: {
            SchoolCode: "SC7548",
            SchoolName: "Whiteney School",
            DegreeAwarded: "DegreeAwaarded1",
            FaxNumber: "7877766565",
            TelephoneNumber: "7846758898",
            StartDate: "24-09-1999",
            EndDate: "04-03-2003"
        },
        Address: {
            UnitNumber: "A746",
            Building: "",
            Street: "SilkBoard",
            Country: "United State",
            State: "Florida",
            City: "Florida",
            ZipCode: "67654"
        },
        Other: {
            DepartmentSpeciality: "Dentist",
            DItrectorName: "Director 1",
            Certificate: "certificat.pdf"
        }
    }];
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

    $scope.showingDetails = false;

    //=============== Graduation Conditions ==================
    $scope.graduationFormStatus = false;
    $scope.newGraduationForm = false;
    
    $scope.addGraduation = function () {
        $scope.graduationFormStatus = false;
        $scope.newGraduationForm = true;
        $scope.Graduation = {};
        $("#newGraduationFormDiv").html(dynamicFormGenerateService.getForm($scope, $("#graduationForm").html()));
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
        $scope.showingDetails = false;
        $scope.graduationFormStatus = false;
        $scope.newGraduationForm = false;
        $scope.Graduation = {};
    };

    $scope.editGraduation = function (index, Graduation) {
        $scope.showingDetails = true;
        $scope.graduationFormStatus = true;
        $scope.newGraduationForm = false;
        $scope.Graduation = Graduation;
        $("#graduationEditDiv" + index).html(dynamicFormGenerateService.getForm($scope, $("#graduationForm").html()));
    };

    $scope.cancelGraduation = function (condition) {
        if (condition == "editCancel") {
            $scope.showingDetails = false;
            $scope.graduationFormStatus = false;
            $scope.newGraduationForm = false;
            $scope.Graduation = {};
        } else {
            $scope.showingDetails = false;
            $scope.newGraduationForm = false;
            $scope.newLegalNameForm = false;
            $scope.Graduation = {};
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

    //============ Internship Client Side MVC ==================
    $scope.programFormStatus = false;
    $scope.newProgramForm = false;

    $scope.addProgram = function () {
        $scope.programFormStatus = false;
        $scope.newProgramForm = true;
        $scope.Program = {};
        $("#newProgramFormDiv").html(dynamicFormGenerateService.getForm($scope, $("#programForm").html()));
    };

    $scope.saveProgram = function (Program) {
        //================== Save Here ============
        //$scope.Programs.push(Program);
        //================== hide Show Condition ============
        $scope.programFormStatus = false;
        $scope.newProgramForm = false;
        $scope.Program = {};
    };

    $scope.updateProgram = function (Program) {
        $scope.showingDetails = false;
        $scope.programFormStatus = false;
        $scope.newProgramForm = false;
        $scope.Program = {};
    };

    $scope.editProgram = function (index, Program) {
        $scope.showingDetails = true;
        $scope.programFormStatus = true;
        $scope.newProgramForm = false;
        $scope.Program = Program;
        $("#programEditDiv" + index).html(dynamicFormGenerateService.getForm($scope, $("#programForm").html()));
    };

    $scope.cancelProgram = function (condition) {
        if (condition == "editCancel") {
            $scope.showingDetails = false;
            $scope.programFormStatus = false;
            $scope.newProgramForm = false;
            $scope.Program = {};
        } else {
            $scope.showingDetails = false;
            $scope.newProgramForm = false;
            $scope.newLegalNameForm = false;
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

    //============ Speciality Client Side MVC ==================
    $scope.specialityFormStatus = false;
    $scope.newSpecialityForm = false;

    $scope.addSpeciality = function () {
        $scope.specialityFormStatus = false;
        $scope.newSpecialityForm = true;
        $scope.Speciality = {};
        $("#newSpecialityFormDiv").html(dynamicFormGenerateService.getForm($scope, $("#specialityForm").html()));
    };

    $scope.saveSpeciality = function (Speciality) {
        //================== Save Here ============
        //$scope.Specialities.push(Speciality);
        //================== hide Show Condition ============
        $scope.specialityFormStatus = false;
        $scope.newSpecialityForm = false;
        $scope.Speciality = {};
    };

    $scope.updateSpeciality = function (Speciality) {
        $scope.showingDetails = false;
        $scope.specialityFormStatus = false;
        $scope.newSpecialityForm = false;
        $scope.Speciality = {};
    };

    $scope.editSpeciality = function (index, Speciality) {
        $scope.showingDetails = true;
        $scope.specialityFormStatus = true;
        $scope.newSpecialityForm = false;
        $scope.Speciality = Speciality;
        $("#specialityEditDiv" + index).html(dynamicFormGenerateService.getForm($scope, $("#specialityForm").html()));
    };

    $scope.cancelSpeciality = function (condition) {
        if (condition == "editCancel") {
            $scope.showingDetails = false;
            $scope.specialityFormStatus = false;
            $scope.newSpecialityForm = false;
            $scope.Speciality = {};
        } else {
            $scope.showingDetails = false;
            $scope.newSpecialityForm = false;
            $scope.newLegalNameForm = false;
            $scope.Speciality = {};
        }
    };

    $scope.removeSpeciality = function (index) {
        for (var i in $scope.Specialities) {
            if (index == i) {
                $scope.Specialities.splice(index, 1);
                break;
            }
        }
    };

    //====================== Fifth Path Data =======================
    $scope.FifthPaths = [{
        HospitalName: "Hospital1",
        Street: "5350 Spring Hill Drive",
        State: "Florida",
        City: "Putname",
        Zipcode: "44544"
    }];

    //============ FifthPath Client Side MVC ==================
    $scope.fifthPathFormStatus = false;
    $scope.newFifthPathForm = false;

    $scope.addFifthPath = function () {
        $scope.fifthPathFormStatus = false;
        $scope.newFifthPathForm = true;
        $scope.FifthPath = {};
        $("#newFifthPathFormDiv").html(dynamicFormGenerateService.getForm($scope, $("#fifthPathForm").html()));
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
        $scope.FifthPath = FifthPath;
        $("#fifthPathEditDiv" + index).html(dynamicFormGenerateService.getForm($scope, $("#fifthPathForm").html()));
    };

    $scope.cancelFifthPath = function (condition) {
        if (condition == "editCancel") {
            $scope.showingDetails = false;
            $scope.fifthPathFormStatus = false;
            $scope.newFifthPathForm = false;
            $scope.FifthPath = {};
        } else {
            $scope.showingDetails = false;
            $scope.newFifthPathForm = false;
            $scope.newLegalNameForm = false;
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
});