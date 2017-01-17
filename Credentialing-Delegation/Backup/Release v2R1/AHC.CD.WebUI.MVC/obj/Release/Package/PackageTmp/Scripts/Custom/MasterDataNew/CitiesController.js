//--------------------- Angular Module ----------------------
var masterDataCities = angular.module("masterDataCities", ['ui.bootstrap']);

//Service for getting master data
masterDataCities.service('masterDataService', ['$http', '$q', function ($http, $q) {

    this.getMasterData = function (URL) {
        return $http.get(URL).then(function (value) { return value.data; });
    };
}]);
//----------------------------- message notification alert service -----------------------------
masterDataCities.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {
    $rootScope.messageDesc = "";
    $rootScope.activeMessageDiv = "";
    $rootScope.messageType = "";

    var animateMessageAlertOff = function () {
        $rootScope.closeAlertMessage();
    };
    this.callAlertMessage = function (calledDiv, msg, msgType, dismissal) { //messageAlertEngine.callAlertMessage('updateHospitalPrivilege' + IndexValue, "Data Updated Successfully !!!!", "success", true);                            
        $rootScope.activeMessageDiv = calledDiv;
        $rootScope.messageDesc = msg;
        $rootScope.messageType = msgType;
        if (dismissal) {
            $timeout(animateMessageAlertOff, 5000);
        }
    }
    $rootScope.closeAlertMessage = function () {
        $rootScope.messageDesc = "";
        $rootScope.activeMessageDiv = "";
        $rootScope.messageType = "";
    }
}])


//=========================== Controller declaration ==========================
masterDataCities.controller('masterDataCitiesController', ['$scope', '$http', '$filter', '$rootScope', 'masterDataService', 'messageAlertEngine', function ($scope, $http, $filter, $rootScope, masterDataService, messageAlertEngine) {

    $scope.AllCities = [];

    $scope.tempCities = {};

    $scope.CountriesLoaded = true;
    $scope.disableAdd = true;

    try {
        $http.get("/Location/GetCountry").then(function (response) {
            $scope.Countries = response.data;
            $scope.CountriesLoaded = false;
        });
    }
    catch (e) {
        $scope.CountriesLoaded = true;
    };

    //----------------- get states by country ID -------------
    $scope.getStates = function (SelectedCountryID) {
        $scope.States = [];
        $scope.AllCities = [];
        $scope.disableAdd = true;

        for (var i in $scope.Countries) {
            if ($scope.Countries[i].CountryID == SelectedCountryID) {
                $scope.States = $scope.Countries[i].States;
                break;
            }
        }
    };

    //------------------ get cities by State ID ----------------
    $scope.getCities = function (SelectedStateID) {
        $scope.AllCities = [];

        for (var i in $scope.States) {
            if ($scope.States[i].StateID == SelectedStateID) {
                $scope.AllCities = $scope.States[i].Cities;
                break;
            }
        }
    };

    //------------ gets the template to ng-include for a table row / item -------------------
    $scope.getTemplate = function (cities) {
        if (cities.CityID === $scope.tempCities.CityID) return 'editCities';
        else return 'displayCities';
    };

    //------------------- Add City ---------------------
    $scope.addCity = function () {
        $scope.disableEdit = !$scope.disableEdit;
        $scope.disableAdd = true;
        var temp = {
            CityID: 0,
            Code: "",
            Name: "",
            StateID: $scope.SelectedStateID,
        };
        $scope.AllCities.splice(0, 0, angular.copy(temp));
        $scope.tempCities = angular.copy(temp);
    };

    //------------------- Save City ---------------------
    $scope.saveCity = function (addData, idx) {
        var isExist = true;

        for (var i = 0; i < $scope.AllCities.length; i++) {
            if (addData.Name && $scope.AllCities[i].Name.replace(" ", "").toLowerCase() == addData.Name.replace(" ", "").toLowerCase()) {
                isExist = false;
                $scope.existErr = "The City already Exist";
                break;
            }
        }
        if (!addData.Name) { $scope.errorName = "Please Enter the City Name"; }

        if (addData.Name && isExist) {
            $http.post('/MasterDataNew/AddCities', addData).
                success(function (data, status, headers, config) {
                    //----------- success message -----------
                    if (data.status == "true") {
                        messageAlertEngine.callAlertMessage("addedCity", "New City Details Added Successfully !!!!", "success", true);

                        $scope.AllCities[0] = data.cityDetails;
                        $scope.reset();
                        $scope.error = "";
                        $scope.existErr = "";
                    }
                    else {
                        messageAlertEngine.callAlertMessage("errorCity", "Sorry Unable To Update City !!!!", "danger", true);
                        $scope.AllCities.splice(0, 1);
                    }
                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("errorCity", "Sorry Unable To Update City !!!!", "danger", true);
                    $scope.AllCities.splice(0, 1);
                });
        }
    };

    //----------------- City new add cancel ---------------
    $scope.cancelAdd = function () {
        $scope.AllCities.splice(0, 1);
        $scope.tempCities = {};
        $scope.disableEdit = false;
        $scope.disableAdd = false;
        $scope.errorName = "";
        $scope.existErr = "";
    };

    //-------------------- Reset City ----------------------
    $scope.reset = function () {
        $scope.tempCities = {};
        $scope.disableAdd = false;
        $scope.disableEdit = false;
        $scope.error = "";
        $scope.existErr = "";
    };

    $scope.CurrentPage = [];

    //-------------------------- angular bootstrap pagger with custom-----------------
    $scope.maxSize = 5;
    $scope.bigTotalItems = 0;
    $scope.bigCurrentPage = 1;

    //-------------------- page change action ---------------------
    $scope.pageChanged = function (pagnumber) {
        $scope.bigCurrentPage = pagnumber;
    };

    //-------------- current page change Scope Watch ---------------------
    $scope.$watch('bigCurrentPage', function (newValue, oldValue) {
        $scope.CurrentPage = [];
        var startIndex = (newValue - 1) * 10;
        var endIndex = startIndex + 9;
        if ($scope.AllCities) {
            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.AllCities[startIndex]) {
                    $scope.CurrentPage.push($scope.AllCities[startIndex]);
                } else {
                    break;
                }
            }
        }
        //console.log($scope.CurrentPageProviders);
    });
    //-------------- License Scope Watch ---------------------
    $scope.$watchCollection('AllCities', function (newValue, oldValue) {
        if (newValue) {
            $scope.bigTotalItems = newValue.length;

            $scope.CurrentPage = [];
            $scope.bigCurrentPage = 1;

            var startIndex = ($scope.bigCurrentPage - 1) * 10;
            var endIndex = startIndex + 9;

            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.AllCities[startIndex]) {
                    $scope.CurrentPage.push($scope.AllCities[startIndex]);
                } else {
                    break;
                }
            }
            //console.log($scope.CurrentPageProviders);
        }
    });
    //------------------- end ------------------
}]);
