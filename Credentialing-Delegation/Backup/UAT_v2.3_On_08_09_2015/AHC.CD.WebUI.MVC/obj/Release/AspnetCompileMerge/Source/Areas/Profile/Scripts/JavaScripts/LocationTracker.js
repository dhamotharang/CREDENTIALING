
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

    $scope.previousSearchString=null;
    $scope.getDetails = function (searchFor) {
        $scope.previousSearchString = searchFor;
        $scope.showLocationDetails = false;
        $scope.showLocationList = false;
        $scope.showProviderDetails = false;        
        $scope.providerName = '';
        if (searchFor != '') {
            if ($scope.selectedOption == 'Provider') {
                $scope.progressbar = true;
                $http.get('/Profile/LocationTracker/GetAllProvidersByName?name=' + searchFor).
                success(function (data, status, headers, config) {
                    console.log(data);
                    $scope.providersList = data;
                    $scope.showProviderDetails = true;
                    $scope.progressbar = false;
                }).
                error(function (data, status, headers, config) {
                    //console.log("Sorry internal master data cont able to fetch.");
                });
            } else if ($scope.selectedOption == 'Location') {
                $scope.progressbar = true;
                $http.get('/Profile/LocationTracker/GetAllPracticeLocations?facilityName=' + searchFor).
                success(function (data, status, headers, config) {
                    console.log(data);
                    $scope.locationList = data;
                    $scope.providersList = [];
                    $scope.showLocationDetails = true;
                    //$scope.showProviderDetails = false;
                    //$scope.showLocationList = false;
                    $scope.progressbar = false;
                    //$scope.providerName = '';
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
    $scope.providerName = '';
    $scope.getPracticeLocations = function (profile) {
        $scope.providerName = profile.PersonalDetail.FirstName+" "+ profile.PersonalDetail.LastName;
        $scope.locationList = [];
        for (var i = 0; i < profile.PracticeLocationDetails.length; i++) {
            if (profile.PracticeLocationDetails[i].Status != 'Inactive') {               
                $scope.locationList.push(profile.PracticeLocationDetails[i].Facility);                
            }
        }
        $scope.showProviderDetails = false;
        $scope.showLocationDetails = true;
        $scope.showLocationList = false;
    }
    $scope.practiceProvidersList = [];
    $scope.getProvidersList = function (facility) {
        $scope.detailprogressbar = true;
        $scope.location = facility;
        $http.get('/Profile/LocationTracker/GetAllProvidersByLocation?facilityID=' + facility.FacilityID).
        success(function (data, status, headers, config) {
            $scope.detailprogressbar = false;
            console.log(data);
            $scope.practiceProvidersList = data;            
            $scope.showLocationList = true;
        }).
        error(function (data, status, headers, config) {
            //console.log("Sorry internal master data cont able to fetch.");
        });
    }

    $scope.printData = function (id, title) {
        var divToPrint = document.getElementById(id);
        $('#hiddenPrintDiv').empty();
        $('#hiddenPrintDiv').append(divToPrint.innerHTML);

        // Removing the last column of the table
        $('#hiddenPrintDiv .hideData').remove();

        $('#hiddenPrintDiv .changeWidth').removeAttr("colspan");
        $('#hiddenPrintDiv .changeWidth').attr("colspan", 3);

        // Creating a window for printing
        var mywindow = window.open('', $('#hiddenPrintDiv').html(), 'height=800,width=800');
        mywindow.document.write('<center><b style="font-size:large">' + title + '</b></center></br>');
        mywindow.document.write('<html><head><title>' + title + '</title>');
        mywindow.document.write('<link rel="stylesheet" href="/Content/SharedCss/app.css" type="text/css" media="all"/>');
        mywindow.document.write('<link rel="stylesheet" href="/Content/SharedCss/bootstrap.min.css" type="text/css" />');
        mywindow.document.write('<style>.ng-hide:not(.ng-hide-animate) {display: none !important;}@page{size: auto;margin-bottom: 5mm;margin-top:7mm;}th{text-align:center;}</style>');
        mywindow.document.write('<style>table { table-layout: fixed; } table th, table td { overflow: hidden; word-wrap: break-word; }</style>');
        mywindow.document.write('</head><body media="print" style="background-color:white"><table class="table table-bordered"></td>');
        mywindow.document.write($('#hiddenPrintDiv').html());
        mywindow.document.write('</table></body></html>');
        mywindow.document.close();
        mywindow.focus();
        setTimeout(function () {
            mywindow.print();
            mywindow.close();
        }, 1000);
        return true;
    }

}]);

$(document).keypress(function (e) {
    if (e.which == 13) {
        $("#searchbtn").click();
    }
});