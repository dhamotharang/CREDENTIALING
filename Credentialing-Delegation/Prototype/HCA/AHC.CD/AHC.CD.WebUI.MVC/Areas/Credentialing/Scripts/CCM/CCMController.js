// -------------------- CCM controller Angular Module-------------------------------
//---------- Author : KRGLV -------------

//------------------------- angular module ----------------------------
var CCMApp = angular.module('CCMApp', []);

//------------- angular tool tip recall directive ---------------------------
CCMApp.directive('tooltip', function () {
    return function (scope, elem) {
        elem.tooltip();
    };
});

//------------------------------- convert date format -----------------------------
var ConvertDateFormat = function (value) {
    if (value) {
        return new Date(parseInt(value.replace("/Date(", "").replace(")/", ""), 10));
    } else {
        return value;
    }
};

// ------------------ dashboard root scope ------------------
CCMApp.run(['$rootScope', function ($rootScope) {
    //----------------- filter day left ranges --------------------
    //$rootScope.days = [5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 70, 80, 90, 180];
}]);

CCMApp.controller("CCMController", ["$scope", function($scope){
    //-------------- CCM Action Item Lists -----------------
    $scope.Actions = [
        {
            ActionID: 1,
            ActionName: "Credentialing"
        },
        {
            ActionID: 2,
            ActionName: "Re-Credentialing"
        },
        {
            ActionID: 3,
            ActionName: "De-Credentialing"
        }
    ];

    //-------------------- CCM Required Data List -----------------------
    $scope.CCMData = [
        {
            ActionID: 1,
            CredentialingList: [
                {
                    FirstName: " PARIKSITH",
                    LastName: "SINGH",
                    PersonalDetailID: 96,
                    ProviderTitles: "MD",
                    Speciality: "Chiropractor",
                    Plan: "ULTIMATE HEALTH PLANS",
                    CredentialingDate: new Date(2014, 02, 09),
                    Status: "Verified"
                },
                {
                    FirstName: " QAHTAN A.",
                    LastName: "ABDULFATTAH",
                    PersonalDetailID: 96,
                    ProviderTitles: "DO",
                    Speciality: "Allergy & Immunology",
                    Plan: "FREEDOM VIP CARE COPD (HMO SNP)",
                    CredentialingDate: new Date(2015, 11, 20),
                    Status: "Verified"
                },
                {
                    FirstName: " IAN",
                    LastName: "ADAM",
                    PersonalDetailID: 96,
                    ProviderTitles: "MD",
                    Speciality: "Dentist",
                    Plan: "OPTIMUM GOLD REWARDS PLAN (HMO-POS)",
                    CredentialingDate: new Date(2015, 12, 09),
                    Status: "Verified"
                },
                {
                    FirstName: " MARC A",
                    LastName: "ALESSANDRONI",
                    PersonalDetailID: 96,
                    ProviderTitles: "DO",
                    Speciality: "Dentist",
                    Plan: "OPTIMUM DIAMOND REWARDS (HMO-POS SNP)",
                    CredentialingDate: new Date(2014, 09, 28),
                    Status: "Verified"
                },
                {
                    FirstName: " CRIDER",
                    LastName: "NORMA",
                    PersonalDetailID: 96,
                    ProviderTitles: "MD",
                    Speciality: "Allergy",
                    Plan: "OPTIMUM PLATINUM PLAN (HMO-POS)",
                    CredentialingDate: new Date(2015, 04, 19),
                    Status: "Verified"
                },
                {
                    FirstName: " John",
                    LastName: "Smith",
                    PersonalDetailID: 96,
                    ProviderTitles: "DO",
                    Speciality: "Dentist",
                    Plan: "FREEDOM VIP CARE COPD (HMO SNP)",
                    CredentialingDate: new Date(2014, 02, 09),
                    Status: "Verified"
                }
            ]
        },
        {
            ActionID: 2,
            CredentialingList: [
                {
                    FirstName: " QAHTAN A.",
                    LastName: "ABDULFATTAH",
                    PersonalDetailID: 96,
                    ProviderTitles: "DO",
                    Speciality: "Allergy & Immunology",
                    Plan: "FREEDOM VIP CARE COPD (HMO SNP)",
                    CredentialingDate: new Date(2015, 11, 20),
                    Status: "Verified"
                },
                {
                    FirstName: " PARIKSITH",
                    LastName: "SINGH",
                    PersonalDetailID: 96,
                    ProviderTitles: "MD",
                    Speciality: "Chiropractor",
                    Plan: "ULTIMATE HEALTH PLANS",
                    CredentialingDate: new Date(2014, 02, 09),
                    Status: "Verified"
                },
                {
                    FirstName: " CRIDER",
                    LastName: "NORMA",
                    PersonalDetailID: 96,
                    ProviderTitles: "MD",
                    Speciality: "Allergy",
                    Plan: "OPTIMUM PLATINUM PLAN (HMO-POS)",
                    CredentialingDate: new Date(2015, 04, 19),
                    Status: "Verified"
                },
                {
                    FirstName: " IAN",
                    LastName: "ADAM",
                    PersonalDetailID: 96,
                    ProviderTitles: "MD",
                    Speciality: "Dentist",
                    Plan: "OPTIMUM GOLD REWARDS PLAN (HMO-POS)",
                    CredentialingDate: new Date(2015, 12, 09),
                    Status: "Verified"
                },
                {
                    FirstName: " MARC A",
                    LastName: "ALESSANDRONI",
                    PersonalDetailID: 96,
                    ProviderTitles: "DO",
                    Speciality: "Dentist",
                    Plan: "OPTIMUM DIAMOND REWARDS (HMO-POS SNP)",
                    CredentialingDate: new Date(2014, 09, 28),
                    Status: "Verified"
                },
                {
                    FirstName: " John",
                    LastName: "Smith",
                    PersonalDetailID: 96,
                    ProviderTitles: "DO",
                    Speciality: "Dentist",
                    Plan: "FREEDOM VIP CARE COPD (HMO SNP)",
                    CredentialingDate: new Date(2014, 02, 09),
                    Status: "Verified"
                }
            ]
        },
        {
            ActionID: 3,
            CredentialingList: [
                {
                    FirstName: " PARIKSITH",
                    LastName: "SINGH",
                    PersonalDetailID: 96,
                    ProviderTitles: "MD",
                    Speciality: "Chiropractor",
                    Plan: "ULTIMATE HEALTH PLANS",
                    CredentialingDate: new Date(2014, 02, 09),
                    Status: "Verified"
                },
                {
                    FirstName: " QAHTAN A.",
                    LastName: "ABDULFATTAH",
                    PersonalDetailID: 96,
                    ProviderTitles: "DO",
                    Speciality: "Allergy & Immunology",
                    Plan: "FREEDOM VIP CARE COPD (HMO SNP)",
                    CredentialingDate: new Date(2015, 11, 20),
                    Status: "Verified"
                },
                {
                    FirstName: " MARC A",
                    LastName: "ALESSANDRONI",
                    PersonalDetailID: 96,
                    ProviderTitles: "DO",
                    Speciality: "Dentist",
                    Plan: "OPTIMUM DIAMOND REWARDS (HMO-POS SNP)",
                    CredentialingDate: new Date(2014, 09, 28),
                    Status: "Verified"
                },
                {
                    FirstName: " IAN",
                    LastName: "ADAM",
                    PersonalDetailID: 96,
                    ProviderTitles: "MD",
                    Speciality: "Dentist",
                    Plan: "OPTIMUM GOLD REWARDS PLAN (HMO-POS)",
                    CredentialingDate: new Date(2015, 12, 09),
                    Status: "Verified"
                },
                {
                    FirstName: " John",
                    LastName: "Smith",
                    PersonalDetailID: 96,
                    ProviderTitles: "DO",
                    Speciality: "Dentist",
                    Plan: "FREEDOM VIP CARE COPD (HMO SNP)",
                    CredentialingDate: new Date(2014, 02, 09),
                    Status: "Verified"
                },
                {
                    FirstName: " CRIDER",
                    LastName: "NORMA",
                    PersonalDetailID: 96,
                    ProviderTitles: "MD",
                    Speciality: "Allergy",
                    Plan: "OPTIMUM PLATINUM PLAN (HMO-POS)",
                    CredentialingDate: new Date(2015, 04, 19),
                    Status: "Verified"
                }
            ]
        }
    ];

    //----------------- Filter Data According to Required -----------------------
    $scope.getDataByMenu = function (data) {
        $scope.selectedAction = data.ActionID;
        $scope.CreadentialingData = data.CredentialingList
    };

    $scope.getDataByMenu($scope.CCMData[0]);
}]);
