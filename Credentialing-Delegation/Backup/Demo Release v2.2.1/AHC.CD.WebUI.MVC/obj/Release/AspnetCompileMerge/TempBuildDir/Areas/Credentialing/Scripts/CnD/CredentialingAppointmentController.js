// -------------------- CCM controller Angular Module-------------------------------
//---------- Author : KRGLV -------------

//------------------------- angular module ----------------------------
var CCOApp = angular.module('CCOApp', ['ui.bootstrap']);

//------------- angular tool tip recall directive ---------------------------
CCOApp.directive('tooltip', function () {
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
CCOApp.run(['$rootScope', function ($rootScope) {
    //----------------- filter day left ranges --------------------
    //$rootScope.days = [5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 70, 80, 90, 180];
}]);

CCOApp.controller("CCOController",function ($scope, $filter,$http) {
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
    $scope.CCOAppointDataData = [
        {
            ActionID: 1,
            CredentialingList: [
                {
                    ProviderID:1,
                    FirstName: " PARIKSITH",
                    LastName: "SINGH",
                    PersonalDetailID: 91,
                    ProviderTitles: "MD",
                    Speciality: "Chiropractor",
                    Plan: "ULTIMATE HEALTH PLANS",
                    CredentialingDate: new Date(2014, 02, 09),
                    Status: "Verified"
                },
                {
                    ProviderID: 2,
                    FirstName: " QAHTAN A.",
                    LastName: "ABDULFATTAH",
                    PersonalDetailID: 92,
                    ProviderTitles: "DO",
                    Speciality: "Allergy & Immunology",
                    Plan: "FREEDOM VIP CARE COPD (HMO SNP)",
                    CredentialingDate: new Date(2015, 11, 20),
                    Status: "Verified"
                },
                {
                    ProviderID: 3,
                    FirstName: " IAN",
                    LastName: "ADAM",
                    PersonalDetailID: 93,
                    ProviderTitles: "MD",
                    Speciality: "Dentist",
                    Plan: "OPTIMUM GOLD REWARDS PLAN (HMO-POS)",
                    CredentialingDate: new Date(2015, 12, 09),
                    Status: "Verified"
                },
                {
                    ProviderID: 4,
                    FirstName: " MARC A",
                    LastName: "ALESSANDRONI",
                    PersonalDetailID: 94,
                    ProviderTitles: "DO",
                    Speciality: "Dentist",
                    Plan: "OPTIMUM DIAMOND REWARDS (HMO-POS SNP)",
                    CredentialingDate: new Date(2014, 09, 28),
                    Status: "Verified"
                },
                {
                    ProviderID: 5,
                    FirstName: " CRIDER",
                    LastName: "NORMA",
                    PersonalDetailID: 95,
                    ProviderTitles: "MD",
                    Speciality: "Allergy",
                    Plan: "OPTIMUM PLATINUM PLAN (HMO-POS)",
                    CredentialingDate: new Date(2015, 04, 19),
                    Status: "Verified"
                },
                {
                    ProviderID: 6,
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
                    PersonalDetailID: 91,
                    ProviderTitles: "DO",
                    Speciality: "Allergy & Immunology",
                    Plan: "FREEDOM VIP CARE COPD (HMO SNP)",
                    CredentialingDate: new Date(2015, 11, 20),
                    Status: "Verified"
                },
                {
                    FirstName: " PARIKSITH",
                    LastName: "SINGH",
                    PersonalDetailID: 92,
                    ProviderTitles: "MD",
                    Speciality: "Chiropractor",
                    Plan: "ULTIMATE HEALTH PLANS",
                    CredentialingDate: new Date(2014, 02, 09),
                    Status: "Verified"
                },
                {
                    FirstName: " CRIDER",
                    LastName: "NORMA",
                    PersonalDetailID: 93,
                    ProviderTitles: "MD",
                    Speciality: "Allergy",
                    Plan: "OPTIMUM PLATINUM PLAN (HMO-POS)",
                    CredentialingDate: new Date(2015, 04, 19),
                    Status: "Verified7"
                },
                {
                    FirstName: " IAN",
                    LastName: "ADAM",
                    PersonalDetailID: 94,
                    ProviderTitles: "MD",
                    Speciality: "Dentist",
                    Plan: "OPTIMUM GOLD REWARDS PLAN (HMO-POS)",
                    CredentialingDate: new Date(2015, 12, 09),
                    Status: "Verified"
                },
                {
                    FirstName: " MARC A",
                    LastName: "ALESSANDRONI",
                    PersonalDetailID: 95,
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
                    PersonalDetailID: 91,
                    ProviderTitles: "MD",
                    Speciality: "Chiropractor",
                    Plan: "ULTIMATE HEALTH PLANS",
                    CredentialingDate: new Date(2014, 02, 09),
                    Status: "Verified"
                },
                {
                    FirstName: " QAHTAN A.",
                    LastName: "ABDULFATTAH",
                    PersonalDetailID: 92,
                    ProviderTitles: "DO",
                    Speciality: "Allergy & Immunology",
                    Plan: "FREEDOM VIP CARE COPD (HMO SNP)",
                    CredentialingDate: new Date(2015, 11, 20),
                    Status: "Verified"
                },
                {
                    FirstName: " MARC A",
                    LastName: "ALESSANDRONI",
                    PersonalDetailID: 93,
                    ProviderTitles: "DO",
                    Speciality: "Dentist",
                    Plan: "OPTIMUM DIAMOND REWARDS (HMO-POS SNP)",
                    CredentialingDate: new Date(2014, 09, 28),
                    Status: "Verified"
                },
                {
                    FirstName: " IAN",
                    LastName: "ADAM",
                    PersonalDetailID: 94,
                    ProviderTitles: "MD",
                    Speciality: "Dentist",
                    Plan: "OPTIMUM GOLD REWARDS PLAN (HMO-POS)",
                    CredentialingDate: new Date(2015, 12, 09),
                    Status: "Verified"
                },
                {
                    FirstName: " John",
                    LastName: "Smith",
                    PersonalDetailID: 95,
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

    //---------------- already scheduled data -----------------------
    $scope.selectedCredentialingForDate = [
        {
            ActionID: 1,
            CredentialingList: [
                {
                    AppointmentDate: new Date(2015, 07, 10),
                    Credentialings: [
                        {
                            FirstName: " PARIKSITH",
                            LastName: "SINGH",
                            PersonalDetailID: 91,
                            ProviderTitles: "MD",
                            Speciality: "Chiropractor",
                            Plan: "ULTIMATE HEALTH PLANS",
                            CredentialingDate: new Date(2014, 02, 09),
                            Status: "Verified"
                        }
                    ]
                },
                {
                    AppointmentDate: new Date(2015, 08, 10),
                    Credentialings: [
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
                },
                {
                    AppointmentDate: new Date(2015, 06, 10),
                    Credentialings: []
                }
            ]
        },
        {
            ActionID: 2,
            CredentialingList: [
                {
                    AppointmentDate: new Date(2015, 07, 10),
                    Credentialings: []
                },
                {
                    AppointmentDate: new Date(2015, 08, 10),
                    Credentialings: []
                },
                {
                    AppointmentDate: new Date(2015, 06, 10),
                    Credentialings: []
                }
            ]
        },
        {
            ActionID: 3,
            CredentialingList: [
                {
                    AppointmentDate: new Date(2015, 07, 10),
                    Credentialings: []
                },
                {
                    AppointmentDate: new Date(2015, 08, 10),
                    Credentialings: []
                },
                {
                    AppointmentDate: new Date(2015, 06, 10),
                    Credentialings: []
                }
            ]
        }
    ];

    $scope.resetSelection = function (data) {
        var temp = [];
        for (var i in data) {
            temp[i] = false;
        }
        return temp;
    };

    //----------------- Filter Data According to Required -----------------------
    $scope.getDataByMenu = function (data) {
        $scope.selectedAction = angular.copy(data.ActionID);
        $scope.CreadentialingData = angular.copy(data.CredentialingList);
        $scope.SelectedDetails = $scope.resetSelection($scope.CreadentialingData);
        $scope.DoneCreadentialing = [];

        $scope.dt = new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate())
    };

    //------------------ init --------------
    $scope.DoneCreadentialing = [];
    $scope.SelectedDetails = [];
    $scope.SelectedDetailsID = [];
    $scope.SelectedDetailsRemoveID=null;
    
    //-------------- selection ---------------
    $scope.getDataByMenu($scope.CCOAppointDataData[0]);

    $scope.convert=function(str) {
        var date = new Date(str),
            mnth = ("0" + (date.getMonth() + 1)).slice(-2),
            day = ("0" + date.getDate()).slice(-2);
        return [mnth, day, date.getFullYear()].join("/");
    }

    $scope.AddSelectedCredentialing = function () {
        //console.log($scope.SelectedDetails);
        //alert($scope.convert($scope.dt));
        var selectedCredentialing = [];
        selectedCredentialing = angular.copy($scope.DoneCreadentialing);
        var NotSeletedCreadentialing = [];
        for (var i in $scope.SelectedDetails) {
            if ($scope.SelectedDetails[i]) {
                $scope.CreadentialingData[i].AppointmentDate= $scope.convert($scope.dt);
                selectedCredentialing.push($scope.CreadentialingData[i]);
                $scope.SelectedDetailsID.push($scope.CreadentialingData[i].ProviderID);
            }
            if (!$scope.SelectedDetails[i]) {
                $scope.CreadentialingData[i].AppointmentDate = "";
                NotSeletedCreadentialing.push(angular.copy($scope.CreadentialingData[i]));
            }
        }
        $scope.DoneCreadentialing=selectedCredentialing;
        $scope.CreadentialingData = NotSeletedCreadentialing;
        $scope.SelectedDetails = $scope.resetSelection($scope.CreadentialingData);
        $scope.SelectedDetailsRemoveID = null;
        
        var obj = {
            ProviderIDArray: $scope.SelectedDetailsID,
            AppointmentDate: $scope.dt
        }
        $http.post('/Credentialing/CnD/SetAppointment', obj).success(function (data, status, headers, config) {
            //----------- success message -----------
            $scope.SelectedDetailsID.splice(0, $scope.SelectedDetailsID.length);
        }).error(function (data, status, headers, config) {

        });



    };

    $scope.RemoveAppoinment = function (index, cred) {
        $scope.CreadentialingData.push(angular.copy(cred));

        $scope.SelectedDetailsRemoveID=$scope.DoneCreadentialing[index].ProviderID;
        $scope.DoneCreadentialing.splice(index, 1);
        $scope.SelectedDetailsID.splice(index,1);
        $scope.SelectedDetails = $scope.resetSelection($scope.CreadentialingData);
        //console.log($scope.SelectedDetailsID);
        //console.log($scope.SelectedDetailsRemoveID);
        var obj = {
            ProviderID: $scope.SelectedDetailsRemoveID
        }
        $http.post('/Credentialing/CnD/RemoveAppointment', obj).success(function (data, status, headers, config) {
            //----------- success message -----------

        }).error(function (data, status, headers, config) {

        });
    };

  

});
