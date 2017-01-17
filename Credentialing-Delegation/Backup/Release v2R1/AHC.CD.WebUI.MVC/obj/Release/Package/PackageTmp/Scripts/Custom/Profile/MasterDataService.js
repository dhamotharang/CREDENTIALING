// service responsible for getting all active profile master data

profileApp.service('masterDataService', function ($http, $q) {

    this.getMasterData = function (URL) {
        return $http.get(URL).then(function (value) { return value.data; });
    };

    //this.getAllAdmittingPrivileges = function () {

    //    if (!admittingPrivileges) {
    //        $http.get("/Profile/MasterData/GetAllAdmittingPrivileges").then(function (value) { admittingPrivileges = value.data; });
    //    }
    //    return admittingPrivileges;
    //};

    //this.getAllDEAScheduleTypes = function () {

    //    if (!DEAScheduleTypes) {
    //        $http.get("/Profile/MasterData/GetAllDEAScheduleTypes").then(function (value) { DEAScheduleTypes = value.data; });
    //    }
    //    return DEAScheduleTypes;
    //};

    //this.getAllDEASchedules = function () {

    //    if (!DEASchedules) {
    //        $http.get("/Profile/MasterData/GetAllDEASchedules").then(function (value) { DEASchedules = value.data; });
    //    }
    //    return DEASchedules;
    //};

    //this.getAllHospitals = function () {

    //    if (!hospitals) {
    //        $http.get("/Profile/MasterData/GetAllHospitals").then(function (value) { hospitals = value.data; });
    //    }
    //    return hospitals;
    //};

    //this.getAllHospitalContactInfoes = function () {

    //    if (!hospitalContactInfoes) {
    //        $http.get("/Profile/MasterData/GetAllHospitalContactInfoes").then(function (value) { hospitalContactInfoes = value.data; });
    //    }
    //    return hospitalContactInfoes;
    //};


    //this.getAllHospitalContactPersons = function () {

    //    if (!hospitalContactPersons) {
    //        $http.get("/Profile/MasterData/GetAllHospitalContactPersons").then(function (value) { hospitalContactPersons = value.data; });
    //    }
    //    return hospitalContactPersons;
    //};

    //this.getAllInsuranceCarriers = function () {

    //    if (!insuranceCarriers) {
    //        $http.get("/Profile/MasterData/GetAllInsuranceCarriers").then(function (value) { insuranceCarriers = value.data; });
    //    }
    //    return insuranceCarriers;
    //};

    //this.getAllMilitaryBranches = function () {
    //    return $http.get("/Profile/MasterData/GetAllMilitaryBranches").then(function (value) { return value.data; });
    //};

    //this.getAllMilitaryDischarges = function () {
    //    return $http.get("/Profile/MasterData/GetAllMilitaryDischarges").then(function (value) { return value.data; });
    //};

    //this.getAllMilitaryPresentDuties = function () {
    //    return $http.get("/Profile/MasterData/GetAllMilitaryPresentDuties").then(function (value) { return value.data; });
    //};

    //this.getAllMilitaryRanks = function () {
    //    return $http.get("/Profile/MasterData/GetAllMilitaryRanks").then(function (value) { return value.data; });
    //};

    //this.getAllPracticeAccessibilityQuestions = function () {
    //    return $http.get("/Profile/MasterData/GetAllPracticeAccessibilityQuestions").then(function (value) { return value.data; });
    //};

    //this.getAllPracticeOpenStatusQuestions = function () {
    //    return $http.get("/Profile/MasterData/GetAllPracticeOpenStatusQuestions").then(function (value) { return value.data; });
    //};

    //this.getAllPracticeServiceQuestions = function () {
    //    return $http.get("/Profile/MasterData/GetAllPracticeServiceQuestions").then(function (value) { return value.data; });
    //};

    //this.getAllPracticeTypes = function () {
    //    return $http.get("/Profile/MasterData/GetAllPracticeTypes").then(function (value) { return value.data; });
    //};

    //this.getAllProfileDisclosureQuestions = function () {
    //    return $http.get("/Profile/MasterData/GetAllProfileDisclosureQuestions").then(function (value) { return value.data; });
    //};

    //this.getAllProviderTypes = function () {
    //    return $http.get("/Profile/MasterData/GetAllProviderTypes").then(function (value) { return value.data; });
    //};

    //this.getAllQualificationDegrees = function () {
    //    return $http.get("/Profile/MasterData/GetAllQualificationDegrees").then(function (value) { return value.data; });
    //};

    //this.getAllSchools = function () {
    //   return $http.get("/Profile/MasterData/GetAllSchools").then(function (value) { return value.data; });
    //};

    //this.getAllSpecialities = function () {
    //    return $http.get("/Profile/MasterData/GetAllSpecialities").then(function (value) { return value.data; });
    //};

    //this.getAllspecialtyBoards = function () {
    //   return $http.get("/Profile/MasterData/GetAllspecialtyBoards").then(function (value) { return value.data; });
    //};

    //this.getAllStaffCategories = function () {
    //    return $http.get("/Profile/MasterData/GetAllStaffCategories").then(function (value) { return value.data; });
    //};

    //this.getAllLicenseStatuses = function () {
    //    return $http.get("/Profile/MasterData/GetAllLicenseStatus").then(function (value) { return value.data; });
    //};

    //this.getAllCertifications = function () {
    //    return $http.get("/Profile/MasterData/GetAllCertificates").then(function (value) { return value.data; });
    //};
});