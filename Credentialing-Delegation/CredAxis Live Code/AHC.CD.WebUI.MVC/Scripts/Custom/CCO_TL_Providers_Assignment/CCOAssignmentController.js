

ccoassingmentApp.controller('ccoAssignmentCtrl', ["$scope", "$rootScope", "$http", "$q", "$filter", "Resource", "messageAlertEngine", function ($scope, $rootScope, $http, $q, $filter, Resource, messageAlertEngine) {
    var pa = this;
    this.displayed = [];
    $scope.selectedProviders = [];
    $scope.selectedProvidersProfileIDs = [];
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
        //var number = pagination.number || 10;
        var number = 10;
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
            $scope.selectedProvidersProfileIDs.splice($scope.selectedProvidersProfileIDs.indexOf(provider.ProfileID), 1);
        }
        else {
            provider.SelectStatus = true;
            $scope.selectedProviders.push(provider);
            $scope.selectedProvidersProfileIDs.push(provider.ProfileID);
        };
        //provider.SelectStatus = provider.SelectStatus == true ? false : true;
        //$scope.selectedProviders.push(provider);
        console.log($scope.selectedProviders);
    }

    // for cards data display
    $scope.displayData = function (assignedPerson, role) {
        if (assignedPerson == 'CCOAssignedData' && role == "CCO") {
            $scope.CCOorTL = "CCO";
            $scope.ccoAssign = true;
            ccoList();
            $scope.CCOCardsDisplayData = angular.copy($scope.ccoList);
        }
        else if (role == "TL") {
            $scope.ccoAssign = false;
            $scope.CCOorTL = "TL";
            //if ($scope.TLlist.length < 1)
            tlList();
            $scope.TLCardsDisplayData = angular.copy($scope.TLlist);
        }
    }

    $scope.Name = "";
    // for deactivate pop-up
    $scope.deactiveProfileInfo = function (profileId, FirstName, LastName, index) {
        $scope.Name = FirstName + " " + LastName;
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
                            for (var i in pa.displayed) {
                                if (pa.displayed[i].ProfileID == profileID) {
                                    pa.displayed[i].Status = "Inactive";
                                    pa.displayed[i].StatusType = 2;
                                    pa.displayed[i].SelectStatus = false;
                                    break;

                                }
                            }
                            //$scope.init_table($scope.data, 1);
                        }
                    } catch (e) {

                    }
                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                });
        $('#profileModal').modal('hide');
        $('#DeactivatedUserDIVID').modal();


    };




    // for reactivate pop-up
    $scope.reactiveProfileInfo = function (profileId, FirstName, LastName, index) {
        $scope.Name = FirstName + " " + LastName;

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
                            for (var i in pa.displayed) {
                                if (pa.displayed[i].ProfileID == profileID) {

                                    pa.displayed[i].Status = "Active";
                                    pa.displayed[i].StatusType = 1;

                                    break;
                                }
                            }
                            //$scope.init_table($scope.data, 1);
                        }
                    } catch (e) {


                    }
                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                });
        $('#profileReactiveModal').modal('hide');
        $('#ReactivatedUserDIVID').modal();
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
        $scope.selectedProvidersProfileIDs = [];
        $http({
            method: "POST",
            url: rootDir + "/AssignToCCOorTL/SearchProfile",
            data: {
                NPINumber: $scope.searchProvider.NPINumber, FirstName: $scope.searchProvider.FirstName,
                LastName: $scope.searchProvider.LastName, ProviderRelationship: $scope.searchProvider.ProviderRelationship, IPAGroupName: $scope.searchProvider.IPAGroupName,
                ProviderLevel: $scope.searchProvider.ProviderLevel, ProviderType: $scope.searchProvider.ProviderType
            }
        }).success(function (resultData) {
            try {
                if (resultData.result.length != 0) {
                    SearchProviderPanelToggle();
                    $rootScope.ProviderData = angular.copy(resultData.result);
                    $scope.searchProvider = "";
                    $scope.progressbar = false;
                    $scope.init_table(resultData.result, condition);
                    var State = {
                        sort: {},
                        search: {},
                        pagination: {
                            start: 0
                        }
                    };
                    callServer(State);
                    //removing selected providers

                }
                else {
                    $scope.progressbar = false;
                    messageAlertEngine.callAlertMessage('noProviderDetails', "No Record Available for the Given Option", "danger", true);
                    $scope.data = "";
                }


            } catch (e) {

            }


        }).error(function () { $scope.progressbar = false; $scope.error_message = "An Error occurred !! Please Try Again !!"; })
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

    $scope.CCOorTL = "";

    var ccoList = function () {
        //$scope.CCOprogressbar = true;
        $scope.CCOorTL = "CCO";
        $http({
            method: 'GET',
            url: '/AssignToCCOorTL/GetAllCCOData'
        }).then(function successCallback(response) {
            $scope.CCOprogressbar = false;
            for (var i = 0; i < response.data.length; i++) {
                var cal = (response.data[i].Pending / (response.data[i].TaskAssigned < 1 ? 1 : response.data[i].TaskAssigned) * 100);
                response.data[i].Performance = Math.round(cal * 100) / 100;
            }
            $scope.ccoList = angular.copy(response.data);
            $scope.CCOCardsDisplayData = angular.copy(response.data);
        }, function errorCallback(response) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
        });
    }

    var tlList = function () {
        $scope.TLprogressbar = true;
        $scope.CCOorTL = "TL";
        $http({
            method: 'GET',
            url: '/AssignToCCOorTL/GetAllTLData'
        }).then(function successCallback(response) {
            $scope.TLprogressbar = false;
            $scope.TLlist = angular.copy(response.data);
            $scope.TLCardsDisplayData = angular.copy(response.data);
        }, function errorCallback(response) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
        });
    }

    //method to will call on click of cco
    $scope.AssignToCCO = function () {
        if ($scope.CCOorTL != "TL") {
            $scope.ShowProfileDelegation = true;
            //if ($scope.ccoList.length < 1)
            $scope.CCOprogressbar = true;
            ccoList();
        }
        else
            $scope.displayData("TLAssignedData");
        SearchTLPanelToggle();
    }

    var CCOOrTLProfileUserID;
    $scope.TotalProvidersSelected = 0;
    $scope.CountofAssignedProviders = 0;
    $scope.CountofNonAssignedProviders = 0;

    $scope.NameofCCOorTLAssigned = "";
    // when user choose the cco/tl
    $scope.showAssignmentConfirmation = function (Proname, ProfileUserID, whomtoassigned) {
        $scope.NameofCCOorTLAssigned = Proname;
        var validationStatus = "true";
        var SelectedProviders = $scope.selectedProvidersProfileIDs;
        ProfileIDs = JSON.stringify(SelectedProviders)
        if (validationStatus) {
            $.ajax({
                url: rootDir + '/AssignToCCOorTL/GetCountOfAlreadyAssignedProviders',
                type: "POST",
                dataType: 'json',
                data: { "ProfileIDs": $scope.selectedProvidersProfileIDs, "CCorTL": $scope.CCOorTL },

                success: function (data, status, headers, config) {
                    try {

                        if (!$scope.$$phase) {
                            $scope.$apply(function () {
                                $scope.TotalProvidersSelected = $scope.selectedProvidersProfileIDs.length;
                                $scope.CountofAssignedProviders = data;
                                $scope.CountofNonAssignedProviders = $scope.selectedProvidersProfileIDs.length - $scope.CountofAssignedProviders;
                                CCOOrTLProfileUserID = ProfileUserID;
                                $scope.ccoName = Proname;
                                $scope.toWhomAssigned = whomtoassigned;
                                $('#inactiveWarningModal1').modal();
                            });

                        }


                    } catch (e) {

                    }
                }
            })
        }
    }


    $scope.NamesofAlreadyAssignedProviders = [];
    $scope.assignTask = function () {
        var profileUserId = CCOOrTLProfileUserID;
        var validationStatus = "true";
        var url = "";
        if (validationStatus) {
            if ($scope.CCOorTL == "CCO") {
                url = rootDir + '/AssignToCCOorTL/AssignProviderstoCCO'
            }
            else if ($scope.CCOorTL == "TL") {
                url = rootDir + '/AssignToCCOorTL/AssignProviderstoTL'
            }

            $scope.selectedProviders1 = [];
            if (document.getElementById('checkbox') != null && document.getElementById('checkbox').checked == true) {
                $scope.Ignorestatus = false;

                for (var pro in $scope.selectedProviders) {
                    if ($scope.CCOorTL == "CCO")
                        if ($scope.selectedProviders[pro].CCO == "") {
                            $scope.selectedProviders1.push($scope.selectedProviders[pro]);
                        }
                    if ($scope.CCOorTL == "TL") {
                        if ($scope.selectedProviders[pro].TL == "") {
                            $scope.selectedProviders1.push($scope.selectedProviders[pro]);
                        }
                    }
                }

            }
            else if (document.getElementById('checkbox') == null) {
                $scope.Ignorestatus = true;
                $scope.selectedProviders1 = $scope.selectedProviders;
            }
            else {
                $scope.Ignorestatus = true;
                $scope.selectedProviders1 = $scope.selectedProviders;
            }
            if ($scope.Ignorestatus == true) {
                if ($scope.CCOorTL == "CCO") {
                    ccoList();
                    for (var i = 0; i < $rootScope.ProviderData.length; i++) {
                        for (var j = 0; j < $scope.selectedProviders.length; j++) {
                            if ($rootScope.ProviderData[i].ProfileID == $scope.selectedProviders[j].ProfileID) {
                                $rootScope.ProviderData[i].CCO = $scope.NameofCCOorTLAssigned;
                            }
                        }

                    }
                }
                else if ($scope.CCOorTL == "TL") {
                    tlList();
                    for (var i = 0; i < $rootScope.ProviderData.length; i++) {
                        for (var j = 0; j < $scope.selectedProviders.length; j++) {
                            if ($rootScope.ProviderData[i].ProfileID == $scope.selectedProviders[j].ProfileID) {
                                $rootScope.ProviderData[i].TL = $scope.NameofCCOorTLAssigned;
                            }
                        }

                    }
                }
            }
            if ($scope.Ignorestatus == false) {
                if ($scope.CCOorTL == "CCO") {
                    ccoList();
                    for (var i = 0; i < $rootScope.ProviderData.length; i++) {
                        for (var j = 0; j < $scope.selectedProviders.length; j++) {
                            if ($rootScope.ProviderData[i].ProfileID == $scope.selectedProviders[j].ProfileID && $rootScope.ProviderData[i].CCO=="") {
                                $rootScope.ProviderData[i].CCO = $scope.NameofCCOorTLAssigned;
                            }
                        }   

                    }
                }
                else if ($scope.CCOorTL == "TL") {
                    tlList();
                    for (var i = 0; i < $rootScope.ProviderData.length; i++) {
                        for (var j = 0; j < $scope.selectedProviders.length; j++) {
                            if ($rootScope.ProviderData[i].ProfileID == $scope.selectedProviders[j].ProfileID && $rootScope.ProviderData[i].TL == "") {
                                $rootScope.ProviderData[i].TL = $scope.NameofCCOorTLAssigned;
                            }
                        }

                    }
                }
            }
            $.ajax({
                url: url,
                type: "POST",
                dataType: 'json',
                data: { "ProfileIDs": $scope.selectedProvidersProfileIDs, "profileUserId": profileUserId, "Status": $scope.Ignorestatus, "CCorTL": $scope.CCOorTL },
                //data: { "selectedProviders": $scope.selectedProviders, "profileUserId": profileUserId },

                success: function (data, status, headers, config) {
                    if (!Ignorestatus) {
                        $scope.tempSelectedProviders = [];
                        for (var s in $scope.selectedProviders) {
                            for (var id in data.ProfileIds) {
                                if (($scope.selectedProviders[s].ProfileID) == data.ProfileIds[id]) {
                                    $scope.tempSelectedProviders.push($scope.selectedProviders[s]);
                                }
                            }
                        }
                        $scope.selectedProviders = [];
                        $scope.selectedProviders = $scope.tempSelectedProviders;
                        $scope.tempSelectedProviders = [];
                    }
                    //$scope.ShowProfileDelegation = false;



                    try {
                        if (data.status == "true") {
                            //$scope.selectedProviders1 = $scope.selectedProviders;
                            //$scope.selectedProvidersProfileIDs = [];


                            //$scope.ShowProfileDelegation = false;
                            //$('#ccoAssignemntArea').remove();
                            //$('#TLAssignemntArea').remove();
                            //$('#SearchResultsDiv').hide();
                            messageAlertEngine.callAlertMessage("TL", "New User Assigned Successfully !!!!", "success", true);
                        }
                        else {
                            messageAlertEngine.callAlertMessage("TLError", data.status, "danger", true);
                        }


                        //$scope.CCOorTL = "";
                        if (document.getElementById("checkbox"))
                            document.getElementById("checkbox").checked = false;
                        Ignorestatus = true;
                        $('#inactiveWarningModal2').modal();


                    } catch (e) {

                    }
                }

            });
        }

    }

}]);

var SearchProviderPanelToggle = function () {
    $("#SearchProviderPanel").slideToggle();
}
var SearchTLPanelToggle = function () {
    $("#searchTLResultID").slideToggle();
}

var ProfileDelegationPanelToggle = function () {
    $("#CCOTLPanel").slideToggle();
}

var closeConfirmationModal = function () {
    $('#removeTask').modal('hide');
    SearchProviderPanelToggle();
}


var closeDeactivateConfirmationModal = function () {
    $('#DeactivatedUserDIVID').modal('hide');
}


var closeReactivateConfirmationModal = function () {
    $('#ReactivatedUserDIVID').modal('hide');
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
var Ignorestatus = true;
var CheckBoxFunction = function () {
    var checkboxvalue = document.getElementById('checkbox').checked;
    if (checkboxvalue) {
        //$('#appTimes').find('input').attr('disabled', true);
        Ignorestatus = false;
    } else {
        //$('#appTimes').find('input').removeAttr('disabled');
        Ignorestatus = true;
    }



}
