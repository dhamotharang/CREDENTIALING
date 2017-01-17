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

    var orderBy = $filter('orderBy');
    $scope.selectedSection = 6;

    //--------------- data show current page function -------------------
    $scope.GetCurrentPageData = function (data, pageNumber) {
        $scope.bigTotalItems = data.length;
        $scope.CurrentPage = [];

        var startIndex = (pageNumber - 1) * 10;
        var endIndex = startIndex + 9;
        if (data) {
            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if (data[startIndex]) {
                    $scope.CurrentPage.push(data[startIndex]);
                } else {
                    break;
                }
            }
        }
    };

    $scope.AllCities = [];

    $scope.tempCities = {};

    $scope.CountriesLoaded = true;
    $scope.disableAdd = true;

    try {
        $http.get(rootDir + "/Location/GetCountry").then(function (response) {
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
        $scope.CurrentPage = [];
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

        $scope.bigCurrentPage = 1;

        for (var i in $scope.States) {
            if ($scope.States[i].StateID == SelectedStateID) {
                $scope.AllCities = $scope.States[i].Cities;
                $scope.selectedSection = 2;
                $scope.AllCities = orderBy($scope.AllCities, 'Name', false);
                $scope.GetCurrentPageData($scope.AllCities, 1);
                //$scope.bigCurrentPage = 1;
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
        $scope.bigCurrentPage = 1;
        $scope.GetCurrentPageData($scope.AllCities, 1);
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
        if (!addData.Name) {
            $scope.errorName = "Please Enter the City Name *";
            $scope.error = true;
        }

        if (addData.Name && isExist) {
            $http.post(rootDir + '/MasterDataNew/AddCities', addData).
                success(function (data, status, headers, config) {
                    //----------- success message -----------
                    if (data.status == "true") {
                        messageAlertEngine.callAlertMessage("addedCity", "New City Details Added Successfully !!!!", "success", true);

                        $scope.AllCities[0] = data.cityDetails;
                        $scope.AllCities = orderBy($scope.AllCities, 'Name', false);
                        $scope.GetCurrentPageData($scope.AllCities, 1);
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
        if ($scope.AllCities) {
            $scope.GetCurrentPageData($scope.AllCities, newValue);
        }
    });

    //-------------- Angular sorting filter --------------
    $scope.order = function (predicate, reverse, section) {
        $scope.selectedSection = section;
        $scope.AllCities = orderBy($scope.AllCities, predicate, reverse);
        $scope.GetCurrentPageData($scope.AllCities, $scope.bigCurrentPage);
    };

    //----------------- City new add cancel ---------------
    $scope.cancelAdd = function () {
        $scope.AllCities.splice(0, 1);
        $scope.GetCurrentPageData($scope.AllCities, $scope.bigCurrentPage);
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
    $scope.filterData = function () {
        $scope.pageChanged(1);
    }

    $scope.IsValidIndex = function (index) {

        var startIndex = ($scope.bigCurrentPage - 1) * 10;
        var endIndex = startIndex + 9;

        if (index >= startIndex && index <= endIndex)
            return true;
        else
            return false;

    }

}]);
