

var SearchProviderPanelToggle = function () {
    $("#SearchProviderPanel").slideToggle();    
}

//Module declaration
var providerapp = angular.module('ProviderApp', ['ngTable', 'ngTableResizableColumns']);

//For Error messages
providerapp.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

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

//Controller declaration
providerapp.controller('ProviderCtrl', function ($scope, $http, $location, $filter, ngTableParams, messageAlertEngine) {
    $scope.data = []; //data in scope is declared
    $scope.progressbar = false;    
    $scope.error_message = "";
    $scope.groupBySelected = "none";
    
    ////To get provider levels master data
    $http.get('/Profile/MasterData/GetAllProviderLevels').
      success(function (data, status, headers, config) {
          //console.log("Provider Levels");
          //console.log(data);
          $scope.ProviderLevels = data;
          
      }).
      error(function (data, status, headers, config) {
          //console.log("Sorry internal master data cont able to fetch.");
      });

    $http.get('/Profile/MasterData/GetAllProviderTypes').
      success(function (data, status, headers, config) {
          //console.log("Provider Types");
          //console.log(data);
          $scope.masterProviderTypes = data;
         
      }).
      error(function (data, status, headers, config) {
          //console.log("Sorry internal master data cont able to fetch.");
      });

   

    //To Display the drop down div
    $scope.searchCumDropDown = function (divId) {
        $("#" + divId).show();
    };

    //Bind the IPA name with model class to achieve search cum drop down
    $scope.addIntoIPADropDown = function (ipa, div) {

        $scope.searchProvider.IPAGroupNameDup = ipa;
        $scope.searchProvider.IPAGroupName = ipa;
        
        $("#" + div).hide();
    }

    //Bind the IPA name with model class to achieve search cum drop down
    $scope.addIntoTypeDropDown = function (type, div) {
        $scope.searchProvider.ProviderType = type.Title;
        $scope.searchProvider.ProviderTypeDup = type.Title;
        $("#" + div).hide();
    }
    
    //============================= Data From Master Data Table  ======================    
    //----------------------------- Get List Of Groups --------------------------    
    $http.get('/MasterData/Organization/GetGroups').
      success(function (data, status, headers, config) {
          $scope.PracticingGroups = data;          
      }).
      error(function (data, status, headers, config) {
          console.log("Sorry internal master data cont able to fetch.");
      });
    

    //Created function to be called when data loaded dynamically
    $scope.init_table = function (data) {
        
        $scope.data = data;
        var counts = [];
        
        if ($scope.data.length <= 10) {
            counts = [];
        }
        else if ($scope.data.length <= 25) {
            counts = [10, 25];
        }
        else if ($scope.data.length <= 50) {
            counts = [10, 25, 50];
        }
        else if ($scope.data.length <= 100) {
            counts = [10, 25, 50, 100];
        }
        else if ($scope.data.length > 100) {
            counts = [10, 25, 50, 100];
        }

        $scope.tableParams1 = new ngTableParams({
            page: 1,            // show first page
            count: 10,          // count per page
            filter: {
                //name: 'M'       // initial filter
            },
            sorting: {
                //name: 'asc'     // initial sorting
            }
        }, {
            counts: counts,            
            total: $scope.data.length, // length of data
            getData: function ($defer, params) {                
                // use build-in angular filter
                var filteredData = params.filter() ?
                        $filter('filter')($scope.data, params.filter()) :
                        $scope.data;
                var orderedData = params.sorting() ?
                        $filter('orderBy')(filteredData, params.orderBy()) :
                        $scope.data;
               
                params.total(orderedData.length); // set total for recalc pagination
                $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));

            }
        }); 
       
    };


    //if filter is on
    $scope.ifFilter = function () {
        try {
            var bar;
            var obj = $scope.tableParams1.$params.filter;
            for (bar in obj) {
                if (obj[bar] != "") {
                    return false;
                }
            }
            return true;
        }
        catch (e) { return true; }

    }
     
    //Get index first in table
    $scope.getIndexFirst = function () {
        try {
            if ($scope.groupBySelected == 'none') {
                return ($scope.tableParams1.$params.page * $scope.tableParams1.$params.count) - ($scope.tableParams1.$params.count - 1);
            }            
        }
        catch (e) { }
    }
    //Get index Last in table
    $scope.getIndexLast = function () {
        try {
            if ($scope.groupBySelected == 'none') {
                return { true: ($scope.data.length), false: ($scope.tableParams1.$params.page * $scope.tableParams1.$params.count) }[(($scope.tableParams1.$params.page * $scope.tableParams1.$params.count) > ($scope.data.length))];
            }            
        }
        catch (e) { }
    }

    
    
    //Get data on basis of the parameters ajax call
    $scope.new_search = function () {
        $scope.data = [];       
        $scope.error_message = "";
        $scope.progressbar = true;
       
        $http({
            method: "POST",
            url: "/SearchProvider/SearchProviderJson",
            data: {
                NPINumber: $scope.searchProvider.NPINumber, FirstName: $scope.searchProvider.FirstName,
                LastName: $scope.searchProvider.LastName, ProviderRelationship: $scope.searchProvider.ProviderRelationship, IPAGroupName: $scope.searchProvider.IPAGroupName,
                ProviderLevel: $scope.searchProvider.ProviderLevel, ProviderType: $scope.searchProvider.ProviderType
            }
        }).success(function (resultData) {    
            if (resultData.searchResults.length != 0) {                
                SearchProviderPanelToggle();
                $scope.data = resultData.searchResults;
                $scope.init_table(resultData.searchResults);                
                $scope.searchProvider = "";
                $scope.progressbar = false;               
            }
            else {
                $scope.progressbar = false;
                messageAlertEngine.callAlertMessage('noProviderDetails', "No Record Available for the Given Option", "danger", true);
                $scope.data = "";
            }

        }).error(function () { $scope.progressbar = false; $scope.error_message = "An Error occured !! Please Try Again !!"; })
    

    }
    
   
    $scope.clearSearch = function () {

        $scope.searchProvider = "";
        $scope.data = "";        
    }
    
})


//================================= Hide All country code popover =========================
$(document).click(function (event) {    
    if (!$(event.target).hasClass("form-control") && $(event.target).parents(".ProviderTypeSelectAutoList").length === 0) {
        $(".ProviderTypeSelectAutoList").hide();
    }
});


$(document).ready(function () {   
    $(".ProviderTypeSelectAutoList").hide();
});

function showLocationList(ele) {
    $(ele).parent().find(".ProviderTypeSelectAutoList").first().show();
}