profileApp.service('masterDataService', function ($http) {

    // service methods are responsible for getting all active master data

    this.certifications = null;
    this.licenseStatuses = null;
    this.staffCategories = null;
    this.specialityBoards = null;
    this.specialities = null;
    this.schools = null;
    this.qualificationDegrees = null;
    this.providerTypes = null
    this.disclosureQuestions = null;
    this.practiceTypes = null;
    this.practiceServiceQuestions = null;

    this.getAllPracticeServiceQuestions = function () {

        if (!practiceServiceQuestions) {
            practiceServiceQuestions = getData("GetAllPracticeServiceQuestions");
        }
        return practiceServiceQuestions;
    };

    this.getAllPracticeTypes = function () {

        if (!practiceTypes) {
            practiceTypes = getData("GetAllPracticeTypes");
        }
        return practiceTypes;
    };

    this.getAllProfileDisclosureQuestions = function () {

        if (!disclosureQuestions) {
            disclosureQuestions = getData("GetAllProfileDisclosureQuestions");
        }
        return disclosureQuestions;
    };

    this.getAllProviderTypes = function () {

        if (!providerTypes) {
            providerTypes = getData("GetAllProviderTypes");
        }
        return providerTypes;
    };

    this.getAllQualificationDegrees = function () {

        if (!qualificationDegrees) {
            qualificationDegrees = getData("GetAllQualificationDegrees");
        }
        return qualificationDegrees;
    };

    this.getAllSchools = function () {

        if (!schools) {
            schools = getData("GetAllSchools");
        }
        return schools;
    };

    this.getAllSpecialities = function () {

        if (!specialities) {
            specialities = getData("GetAllSpecialities");
        }
        return specialities;
    };

    this.getAllSpecialities = function () {

        if (!specialities) {
            specialities = getData("GetAllSpecialities");
        }
        return specialities;
    };

    this.getAllspecialityBoards = function () {

        if (!specialityBoards) {
            specialityBoards = getData("GetAllspecialityBoards");
        }
        return specialityBoards;
    };

    this.getAllStaffCategories = function () {

        if (!staffCategories) {
            staffCategories = getData("GetAllStaffCategories");
        }
        return staffCategories;
    };

    this.getAllLicenseStatuses = function () {

        if (!licenseStatuses) {
            licenseStatuses=getData("GetAllLicenseStatus");
        }
        return licenseStatuses;
    };

    this.getAllCertifications = function () {

        if (!certifications) {
            certifications.getData("GetAllCertificates");
        }
        return certifications;
    };

    this.getData() = function (Action) {

        var masterDataControllerURL = "~/Profile/MasterData/";

        $http.get(masterDataControllerURL+Action).success(function (data, status, headers, config) {
            return data;
        }).
        error(function (data, status, headers, config) {
            console.log(data + " " + status);
        });
    };
});