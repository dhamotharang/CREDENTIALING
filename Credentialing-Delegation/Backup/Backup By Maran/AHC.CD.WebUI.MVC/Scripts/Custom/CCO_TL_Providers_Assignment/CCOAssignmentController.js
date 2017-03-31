

ccoassingmentApp.controller('ccoAssignmentCtrl', ["$scope", "$rootScope", "$http", "$q", "$filter", "Resource", "messageAlertEngine", function ($scope, $rootScope, $http, $q, $filter, Resource, messageAlertEngine) {
    var pa = this;
    this.displayed = [];
    $scope.selectedProviders = [];
    $scope.TLlist = [];
    $scope.ccoList = [];
    $scope.progressbar = false;
    $scope.ccoAssign = true;
    $scope.error_message = "";
    $scope.groupBySelected = "none";
    $scope.ccoAssign = true;
    $scope.data = [];

   
 


    this.callServer = function callServer(tableState) {
        pa.isLoading = true;
        var pagination = tableState.pagination;
        console.log(pagination.number);
        var start = pagination.start || 0;
        var number = pagination.number || 10;
        $scope.t = tableState;
        Resource.getPage(start, number, tableState).then(function (result) {
            pa.displayed = result.data;
            pa.temp = pa.displayed;
            console.log(result.data);
            tableState.pagination.numberOfPages = result.numberOfPages;//set the number of pages so the pagination can update
            pa.isLoading = false;
        });
    };

    $scope.selectProvider = function (event, provider) {
        if (provider.Status.indexOf("Inactive") > -1) {
            provider.SelectStatus = false
        }
        if (event.target.className.indexOf('skip') > -1 || provider.Status.indexOf("Inactive") > -1) {
            return;
        }
        if (provider.SelectStatus == true) {
            provider.SelectStatus = false;
            $scope.selectedProviders.splice($scope.selectedProviders.indexOf(provider), 1);
        }
        else {
            provider.SelectStatus = true;
            $scope.selectedProviders.push(provider)
        };
        //provider.SelectStatus = provider.SelectStatus == true ? false : true;
        //$scope.selectedProviders.push(provider);
        console.log($scope.selectedProviders);
    }

    // for cards data display
    $scope.displayData = function (assignedPerson) {
        if (assignedPerson == 'CCOAssignedData') {
            $scope.ccoAssign = true;

            $scope.CardsDisplayData = angular.copy($scope.ccoList);
        }
        else {
            $scope.ccoAssign = false;
            if ($scope.TLlist.length < 1)
                tlList();
            $scope.CardsDisplayData = angular.copy($scope.TLlist);
        }
    }


    // for deactivate pop-up
    $scope.deactiveProfileInfo = function (profileId, index) {
        tempindex = index;
        $scope.SelectedProfileID = profileId;
        $('#profileModal').modal();
    };

    // for deactivate the profile
    $scope.deactiveProfile = function (profileID) {
        $('#temp' + tempindex).attr('disabled', true);
        $http.post(rootDir + '/Profile/MasterProfile/DeactivateProfile?profileID=' + profileID).
                success(function (data, status, headers, config) {
                    try {
                        //----------- success message -----------
                        if (data) {
                            for (var i in $scope.data) {
                                if ($scope.data[i].ProfileID == profileID) {
                                    $scope.data[i].Status = "Inactive";
                                    $scope.data[i].StatusType = 2;
                                    $scope.data[i].SelectStatus = false;
                                    break;
                                }
                            }
                            $scope.init_table($scope.data, 1);
                        }
                    } catch (e) {

                    }
                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                });
        $('#profileModal').modal('hide');
    };

    // for reactivate pop-up
    $scope.reactiveProfileInfo = function (profileId, index) {
        tempindex = index;
        $('#temp' + tempindex).attr('disabled', true);
        $scope.SelectedProfileID = profileId;
        $('#profileReactiveModal').modal();
    };


    // for reactivte the profile
    $scope.reactiveProfile = function (profileID) {
        $http.post(rootDir + '/Profile/MasterProfile/ReactivateProfile?profileID=' + profileID).
                success(function (data, status, headers, config) {
                    try {
                        //----------- success message -----------
                        if (data) {
                            for (var i in $scope.data) {
                                if ($scope.data[i].ProfileID == profileID) {
                                    $scope.data[i].Status = "Active";
                                    $scope.data[i].StatusType = 1;
                                    break;
                                }
                            }
                            $scope.init_table($scope.data, 1);
                        }
                    } catch (e) {


                    }
                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                });
        $('#profileReactiveModal').modal('hide');
    };
    //--Master data area---
    $http.get(rootDir + '/Profile/MasterData/GetAllProviderLevels').
      success(function (data, status, headers, config) {
          $scope.ProviderLevels = data;

      }).
      error(function (data, status, headers, config) {
      });

    $http.get(rootDir + '/Profile/MasterData/GetAllProviderTypes').
      success(function (data, status, headers, config) {
          $scope.masterProviderTypes = data;

      }).
      error(function (data, status, headers, config) {
      });


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
    $http.get(rootDir + '/MasterData/Organization/GetGroups').
      success(function (data, status, headers, config) {
          $scope.PracticingGroups = data;
      }).
      error(function (data, status, headers, config) {
      });

    // new provider profile search 
    $scope.new_search_Profile = function (condition) {
        $scope.data = [];
        $scope.error_message = "";
        $scope.progressbar = true;
        $scope.selectedProviders = [];
        $http({
            method: "POST",
            url: rootDir + "/SearchProfile/SearchProfileJson",
            data: {
                NPINumber: $scope.searchProvider.NPINumber, FirstName: $scope.searchProvider.FirstName,
                LastName: $scope.searchProvider.LastName, ProviderRelationship: $scope.searchProvider.ProviderRelationship, IPAGroupName: $scope.searchProvider.IPAGroupName,
                ProviderLevel: $scope.searchProvider.ProviderLevel, ProviderType: $scope.searchProvider.ProviderType
            }
        }).success(function (resultData) {
            try {
                if (resultData.searchResults.length != 0) {
                    SearchProviderPanelToggle();
                    $rootScope.ProviderData = resultData.searchResults;
                    $scope.searchProvider = "";
                    $scope.progressbar = false;
                    $scope.init_table(resultData.searchResults, condition);
                   
                    //removing selected providers

                }
                else {
                    $scope.progressbar = false;
                    messageAlertEngine.callAlertMessage('noProviderDetails', "No Record Available for the Given Option", "danger", true);
                    $scope.data = "";
                }
            } catch (e) {

            }


        }).error(function () { $scope.progressbar = false; $scope.error_message = "An Error occured !! Please Try Again !!"; })
    }


    //Created function to be called when data loaded dynamically
    $scope.init_table = function (data, condition) {

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
        if (condition == 1) {
            for (var i = 0; i < $scope.data.length; i++) {
                $scope.data[i].FirstName = ""; //initialization of new property 
                $scope.data[i].LastName = "";
                $scope.data[i].NPINumber = "";
                $scope.data[i].FullTitles = "";
                $scope.data[i].FullRelations = "";

                $scope.data[i].FirstName = $scope.data[i].PersonalDetail.FirstName;  //set the data from nested obj into new property
                $scope.data[i].LastName = $scope.data[i].PersonalDetail.LastName;
                $scope.data[i].NPINumber = $scope.data[i].OtherIdentificationNumber.NPINumber;
                //------------------ for full titles ---------------------
                var fulltitles = "";
                for (var j in $scope.data[i].PersonalDetail.ProviderTitles) {
                    if (j > 0) {
                        fulltitles += ", ";
                    }
                    fulltitles += $scope.data[i].PersonalDetail.ProviderTitles[j].ProviderType.Title;
                }
                $scope.data[i].FullTitles = fulltitles;
                //-------------- for full relations ---------------
                var fullrelations = "";
                for (var j in $scope.data[i].ContractInfoes) {
                    if (j > 0) {
                        fullrelations += ", ";
                    }
                    fullrelations += $scope.data[i].ContractInfoes[j].ProviderRelationship;
                }
                $scope.data[i].FullRelations = fullrelations;
            }
        }

        $scope.tableParams1 = new ngTableParams({
            page: 1,            // show first page
            count: 10,          // count per page
            filter: {
                //name: 'M'       // initial filter
                //FirstName : ''
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

    $scope.clearSearch = function () {
        $scope.searchProvider = "";
        $scope.data = "";
    }



    var ccoList = function () {
        $http({
            method: 'GET',
            url: '/Prototypes/GetCCOData'
        }).then(function successCallback(response) {
            for (var i = 0; i < response.data.length; i++) {
                var cal = (response.data[i].Pending / (response.data[i].TaskAssigned < 1 ? 1 : response.data[i].TaskAssigned) * 100);
                response.data[i].Performance = Math.round(cal * 100) / 100;
            }
            $scope.ccoList = angular.copy(response.data);
            $scope.CardsDisplayData = angular.copy(response.data);
        }, function errorCallback(response) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
        });
    }

    var tlList = function () {
        $http({
            method: 'GET',
            url: '/Prototypes/GetTLData'
        }).then(function successCallback(response) {
            $scope.TLlist = angular.copy(response.data);
            $scope.CardsDisplayData = angular.copy(response.data);
        }, function errorCallback(response) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
        });
    }

    //method to will call on click of cco
    $scope.AssignToCCO = function () {
        $scope.ShowProfileDelegation = true;
        if ($scope.ccoList.length < 1)
            ccoList();

        SearchTLPanelToggle();
    }

    // when user choose the cco/tl
    $scope.showAssignmentConfirmation = function (Proname, whomtoassigned) {
        $('#inactiveWarningModal1').modal();
        $scope.ccoName = Proname;
        $scope.toWhomAssigned = whomtoassigned;
    }

    $scope.assignTask = function () {
        $('#inactiveWarningModal2').modal();
    }


}]);

var SearchProviderPanelToggle = function () {
    $("#SearchProviderPanel").slideToggle();
}
var SearchTLPanelToggle = function () {
    $("#searchTLResultID").slideToggle();
}

$(document).ready(function () {
    $("body").tooltip({ selector: '[data-toggle=tooltip]' });
    $("#sidemenu").addClass("menu-in");
    $("#page-wrapper").addClass("menuup");
    $(".ProviderTypeSelectAutoList").hide();
});

$(document).click(function (event) {
    if (!$(event.target).hasClass("form-control") && $(event.target).parents(".ProviderTypeSelectAutoList").length === 0) {
        $(".ProviderTypeSelectAutoList").hide();
    }
});

function showLocationList(ele) {
    $(ele).parent().find(".ProviderTypeSelectAutoList").first().show();
}