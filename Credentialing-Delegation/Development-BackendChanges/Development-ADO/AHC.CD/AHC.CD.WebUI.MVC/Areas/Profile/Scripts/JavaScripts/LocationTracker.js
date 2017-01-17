
var locationApp = angular.module('locationApp', []);

locationApp.controller('locationController', ['$scope', '$http', '$filter', function ($scope, $http, $filter) {

    $scope.masterProviders = [];
    $scope.searchFor = '';
    //masterDataService.getMasterData(rootDir + "/Profile/LocationTracker/GetAllProviders").then(function (masterProviders) {
    //    $scope.masterProviders = masterProviders;
    //    console.log($scope.masterProviders);
    //    $scope.tempmMsterProviders = angular.copy($scope.masterProviders);
    //});

    //$http.get('/Profile/LocationTracker/GetAllProviders').
    //   success(function (data, status, headers, config) {           
    //       console.log(data);
    //       $scope.masterProviders = data;
    //       $scope.tempMasterProviders = angular.copy($scope.masterProviders);
    //   }).
    //   error(function (data, status, headers, config) {
    //       //console.log("Sorry internal master data cont able to fetch.");
    //   });

    $scope.selectedProvider = null;
    $scope.selectedOption = null;
    $scope.locationList = [];

    $scope.addIntoProviderDropDown = function (provider) {
        $scope.selectedProvider = provider.PersonalDetail.FirstName;
        $scope.selectedProviderId = provider.ProfileID;
        $scope.masterProviders = angular.copy($scope.tempMasterProviders);
        $("#ForProvider").hide();
    }

    $scope.searchCumDropDown = function () {
        $scope.masterProviders = angular.copy($scope.tempMasterProviders);
        $("#ForProvider").show();
    };

    
    $scope.getDetails = function (searchFor) {
        if (searchFor != '') {
            if ($scope.selectedOption == 'Provider') {
                $http.get('/Profile/LocationTracker/GetAllProvidersByName?name=' + searchFor).
                success(function (data, status, headers, config) {
                    console.log(data);
                    $scope.providersList = data;
                    $scope.showProviderDetails = true;
                    $scope.showLocationDetails = false;
                }).
                error(function (data, status, headers, config) {
                    //console.log("Sorry internal master data cont able to fetch.");
                });
            } else if ($scope.selectedOption == 'Location') {
                $http.get('/Profile/LocationTracker/GetAllPracticeLocations?facilityName=' + searchFor).
                success(function (data, status, headers, config) {
                    console.log(data);
                    $scope.locationList = data;
                    $scope.showLocationDetails = true;

                }).
                error(function (data, status, headers, config) {
                    //console.log("Sorry internal master data cont able to fetch.");
                });

            }
            else {
                $scope.error = true;
            }
        }
        else {
            $scope.errorText = true;
        }
    }
    
    $scope.getPracticeLocations = function (profile) {
       
        for (var i = 0; i < profile.PracticeLocationDetails.length; i++) {
            if (profile.PracticeLocationDetails[i].Status != 'Inactive') {
                $scope.locationList.push(profile.PracticeLocationDetails[i]);                
            }
        }
       // $scope.showProviderDetails = false;
        $scope.showLocationDetails = true;
        var ele1 = angular.element(document.querySelector('#div1'));
        ele1.removeClass('active');
        var ele2 = angular.element(document.querySelector('#div2'));
        ele2.addClass('active');
    }
    $scope.practiceProvidersList = [];
    $scope.getProvidersList = function (facility) {
        $scope.location = facility;
        $http.get('/Profile/LocationTracker/GetAllProvidersByLocation?facilityID=' + facility.FacilityId).
        success(function (data, status, headers, config) {
            console.log(data);
            $scope.practiceProvidersList = data;
            $scope.showLocationDetails = false;
            $scope.showLocationList = true;
            var ele2 = angular.element(document.querySelector('#div2'));
            ele2.removeClass('active');
            var ele2 = angular.element(document.querySelector('#div3'));
            ele2.addClass('active');

        }).
        error(function (data, status, headers, config) {
            //console.log("Sorry internal master data cont able to fetch.");
        });
    }

}]);

