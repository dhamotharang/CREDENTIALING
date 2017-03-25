UpdateAndRenewalsApp.service('UpdateAndRenewalsMasterService', ['$http', '$rootScope', function ($http, $rootScope) {
    $http.get(rootDir + '/Profile/MasterData/GetAllLicenseStatus').then(function (value) {
        $rootScope.LicenseStatus = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllStaffCategories').then(function (value) {
        $rootScope.StaffCategories = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllspecialtyBoards').then(function (value) {
        $rootScope.SpecialtyBoards = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllSpecialities').then(function (value) {
        $rootScope.Specialties = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllSchools').then(function (value) {
        $rootScope.Schools = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllQualificationDegrees').then(function (value) {
        $rootScope.QualificationDegrees = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllProviderTypes').then(function (value) {
        $rootScope.ProviderTypes = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllProfileDisclosureQuestions').then(function (value) {
        $rootScope.DisclosureQuestions = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllLoacationPracticeTypes').then(function (value) {
        $rootScope.LocationPracticeTypes = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllPracticeServiceQuestions').then(function (value) {
        $rootScope.PracticeServiceQuestions = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllPracticeOpenStatusQuestions').then(function (value) {
        $rootScope.PracticeOpenStatusQuestions = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllPracticeAccessibilityQuestions').then(function (value) {
        $rootScope.PracticeAccessibilityQuestions = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllMilitaryRanks').then(function (value) {
        $rootScope.MilitaryRanks = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllMilitaryPresentDuties').then(function (value) {
        $rootScope.MilitaryPresentDuties = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllMilitaryDischarges').then(function (value) {
        $rootScope.MilitaryDischarges = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllMilitaryBranches').then(function (value) {
        $rootScope.MilitaryBranches = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllInsuranceCarriers').then(function (value) {
        $rootScope.InsuranceCarriers = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllInsuranceCarrierAddresses').then(function (value) {
        $rootScope.InsuranceCarrierAddresses = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllHospitalContactPersons').then(function (value) {
        $rootScope.HospitalContactPersons = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllHospitals').then(function (value) {
        $rootScope.Hospitals = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllDEASchedules').then(function (value) {
        $rootScope.DEASchedules = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllCertificates').then(function (value) {
        $rootScope.Certificates = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllAdmittingPrivileges').then(function (value) {
        $rootScope.AdmittingPrivileges = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllVisaTypes').then(function (value) {
        $rootScope.VisaTypes = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllVisaStatus').then(function (value) {
        $rootScope.VisaStatus = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllQuestions').then(function (value) {
        $rootScope.Questions = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllQuestionCategories').then(function (value) {
        $rootScope.QuestionCategories = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllProviderLevels').then(function (value) {
        $rootScope.ProviderLevels = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllOrganizations').then(function (value) {
        $rootScope.Organizations = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllAccessibilityQuestions').then(function (value) {
        $rootScope.AccessibilityQuestions = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllPracticeTypes').then(function (value) {
        $rootScope.PracticeTypes = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllServiceQuestions').then(function (value) {
        $rootScope.ServiceQuestions = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllOpenPracticeStatusQuestions').then(function (value) {
        $rootScope.OpenPracticeStatusQuestions = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllPaymentAndRemittance').then(function (value) {
        $rootScope.PaymentAndRemittance = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllBusinessContactPerson').then(function (value) {
        $rootScope.BusinessContactPerson = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllOrganizationGroups').then(function (value) {
        $rootScope.OrganizationGroups = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllBillingContact').then(function (value) {
        $rootScope.BillingContact = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllFacilities').then(function (value) {
        $rootScope.Facilities = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllCredentialingContact').then(function (value) {
        $rootScope.CredentialingContact = value;
    });
}]);




