//--------------------- Angular Module ----------------------
var masterDataCities = angular.module("masterDataCities", ['ui.bootstrap']);

//Service for getting master data
masterDataCities.service('masterDataService', ['$http', '$q', function ($http, $q) {

    this.getMasterData = function (URL) {
        return $http.get(URL).then(function (value) { return value.data; });
    };

}]);

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

    $scope.CountriesLoaded = true;
    $scope.disableAdd = true;
    try {
        $http.get("/Location/GetCountry")     //locationService makes an ajax call to fetch all the cities which have relevent names as the query string.
    .then(function (response) {
       console.log(response.data);
       $scope.Countries = response.data;
     $scope.CountriesLoaded = false;

    });
        $scope.CountriesLoaded = false;
    }

    catch (e) {
        $scope.CountriesLoaded = true;
    };
    $scope.CountriesLoaded = true;

    $scope.getStates = function (SelectedCountryID, condition) {

        $scope.States = [];
      
        for (var i in $scope.Countries) {
            if ($scope.Countries[i].CountryID == SelectedCountryID) {
                $scope.States = $scope.Countries[i].States;

                break;
            }
        }
        
    };
    $scope.getCities = function (SelectedStateID) {

        $scope.AllCities = [];

        for (var i in $scope.States) {
            if ($scope.States[i].StateID == SelectedStateID) {
                $scope.AllCities = $scope.States[i].Cities;
                break;
            }
        }

    };
   
    $scope.tempCities = {};

    //------------ gets the template to ng-include for a table row / item -------------------
    $scope.getTemplate = function (cities) {
        if (cities.Name === $scope.tempCities.Name) return 'editCities';
        else return 'displayCities';
       
    };

   

    //------------------- Add City ---------------------
    $scope.addCity = function (city) {
        $scope.disableEdit = !$scope.disableEdit;
        $scope.addNewCity = true;
        $scope.disableAdd = true;
        var temp = {
            CityID: 0,
           Code: "",
            Name: "",
            StateID: 0,

        };
       
    };

    //------------------- Save City ---------------------
    $scope.saveCity = function (idx) {

        var addData = {
            CityID: 0,
            Code: $scope.tempCities.Code,
            Name: $scope.tempCities.Name,
            StateID: $scope.SelectedStateID,
        };

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
                        //data.stateCityDetails.LastModifiedDate = $scope.ConvertDateFormat(data.stateCityDetails.LastModifiedDate);
                        //$scope.AllCities[idx] = angular.copy(data.stateCityDetails);
                        //for (var i = 0; i < $scope.AllCities.length; i++) {

                        //    if ($scope.AllCities[i].CityID == data.cityDetails.CityID) {
                        //        $scope.AllCities[i] = angular.copy(data.cityDetails);
                        //    }
                        //}
                        $scope.AllCities.push(data.cityDetails);
                        $scope.addNewCity = false;
                        $scope.reset();
                        $scope.error = "";
                        $scope.existErr = "";
                       
                    }
                    else {
                        messageAlertEngine.callAlertMessage("errorCity", "Sorry Unable To Update City !!!!", "danger", true);
                        $scope.AllCities.splice(idx, 1);
                    }
                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("errorCity", "Sorry Unable To Update City !!!!", "danger", true);
                    $scope.AllCities.splice(idx, 1);
                });
        }

    
    };

    //------------------- Update City ---------------------
    //$scope.updateCity = function (idx) {

    //    var updateData = {
    //        CityID: $scope.tempCities.CityID,
    //        Code: $scope.tempCities.Code,
    //        Name: $scope.tempCities.Name,
    //    };

    //    var isExist = true;

    //    for (var i = 0; i < $scope.AllCities.length; i++) {

    //        if (updateData.Name && $scope.AllCities[i].CityID != updateData.CityID && $scope.AllCities[i].Name.replace(" ", "").toLowerCase() == updateData.Name.replace(" ", "").toLowerCase()) {

    //            isExist = false;
    //            $scope.existErr = "The City already Exist";
    //            break;
    //        }
    //    }
    //    if (!updateData.Name) { $scope.error = "Please Enter the City Name"; }

    //    console.log("Updating City");

    //    if (updateData.Name && isExist) {
    //        $http.post('/MasterDataNew/UpdateCity', updateData).
    //            success(function (data, status, headers, config) {
    //                //----------- success message -----------
    //                if (data.status == "true") {
    //                    messageAlertEngine.callAlertMessage("updateCity", "City Details Updated Successfully !!!!", "success", true);
    //                    //data.stateCityDetails.LastModifiedDate = $scope.ConvertDateFormat(data.stateCityDetails.LastModifiedDate);
    //                    //$scope.AllCities[idx] = angular.copy(data.stateCityDetails);
    //                    $scope.reset();
    //                    $scope.error = "";
    //                    $scope.existErr = "";
    //                }
    //            }).
    //            error(function (data, status, headers, config) {
    //                //----------- error message -----------
    //                messageAlertEngine.callAlertMessage("errorCity", "Sorry Unable To Update City !!!!", "danger", true);
    //            });
    //    }


    //};

    //----------------- City new add cancel ---------------
    $scope.cancelAdd = function () {
        $scope.addNewCity = false;
        $scope.tempCities = {};
        $scope.disableEdit = false;
        $scope.disableAdd = false;
        $scope.errorName = "";
        $scope.existErr = "";
    };

    //-------------------- Reset City ----------------------
    $scope.reset = function () {
        $scope.tempCities = {};
        $scope.addNewCity = false;
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
