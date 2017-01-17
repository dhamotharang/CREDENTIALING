
//--------------------- Angular Module ----------------------
var credentialingList = angular.module('CredentialingList', ['ngTable']);

//=========================== Controller declaration ==========================
credentialingList.controller('credentialingListController', function ($scope, $http, $filter, $rootScope, ngTableParams) {

    $scope.data = [];
    $scope.groupBySelected = "none";
    $scope.progressbar = false;
    $scope.error_message = "";
    $scope.showInit = false;
    $scope.status = [];

    $scope.getStatus = function (data, index) {
        //console.log(data);

        if (data[data.length - 1].CredentialingActivityLogs != null && data[data.length - 1].CredentialingActivityLogs.length != 0) {

            if (data[data.length - 1].CredentialingActivityLogs[data[data.length - 1].CredentialingActivityLogs.length - 1].Activity == "Initiation") {

                if (data[data.length - 1].CredentialingActivityLogs[data[data.length - 1].CredentialingActivityLogs.length - 1].ActivityStatus == "Completed") {

                    //$scope.status.push("Initiated");
                    $scope.data[index].CurrentStatus = "Initiated";

                }

            } else if (data[data.length - 1].CredentialingActivityLogs[data[data.length - 1].CredentialingActivityLogs.length - 1].Activity == "PSV") {

                if (data[data.length - 1].CredentialingActivityLogs[data[data.length - 1].CredentialingActivityLogs.length - 1].ActivityStatus == "Completed") {

                    //$scope.status.push("Verified");
                    $scope.data[index].CurrentStatus = "Verified";

                } else {

                    //$scope.status.push("Initiated");
                    $scope.data[index].CurrentStatus = "Initiated";

                }

            } else if (data[data.length - 1].CredentialingActivityLogs[data[data.length - 1].CredentialingActivityLogs.length - 1].Activity == "CCMAppointment") {

                if (data[data.length - 1].CredentialingActivityLogs[data[data.length - 1].CredentialingActivityLogs.length - 1].ActivityStatus == "Completed") {

                    //$scope.status.push("CCM");
                    $scope.data[index].CurrentStatus = "CCM";

                } else {

                    //$scope.status.push("Verified");
                    $scope.data[index].CurrentStatus = "Verified";

                }

            } else if (data[data.length - 1].CredentialingActivityLogs[data[data.length - 1].CredentialingActivityLogs.length - 1].Activity == "Loading") {

                if (data[data.length - 1].CredentialingActivityLogs[data[data.length - 1].CredentialingActivityLogs.length - 1].ActivityStatus == "Completed") {

                    //$scope.status.push("Submitted");
                    $scope.data[index].CurrentStatus = "Submitted";

                } else {

                    //$scope.status.push("CCM");
                    $scope.data[index].CurrentStatus = "CCM";

                }

            } else if (data[data.length - 1].CredentialingActivityLogs[data[data.length - 1].CredentialingActivityLogs.length - 1].Activity == "Completed") {

                if (data[data.length - 1].CredentialingActivityLogs[data[data.length - 1].CredentialingActivityLogs.length - 1].ActivityStatus == "Completed") {

                    //$scope.status.push("Completed");
                    $scope.data[index].CurrentStatus = "Completed";

                } else {

                    //$scope.status.push("Submitted");
                    $scope.data[index].CurrentStatus = "Submitted";

                }

            }

        }

    }

    //Convert the date from database to normal
    $scope.ConvertDateFormat = function (value) {
        ////console.log(value);
        var returnValue = value;
        try {
            if (value.indexOf("/Date(") == 0) {
                returnValue = new Date(parseInt(value.replace("/Date(", "").replace(")/", ""), 10));
            }
            return returnValue;
        } catch (e) {
            return returnValue;
        }
        return returnValue;
    };


    $scope.setCredInfo = function (id) {

        sessionStorage.setItem('credentialingInfoId', id);

    };

    $scope.clearAction = function () {
        $scope.SearchProviderPanelToggleDown('SearchProviderPanel');
        $scope.data = "";
        $scope.showInit = false;
    };

    $scope.resetSelection = function (data) {
        var temp = [];
        for (var i in data) {
            temp[i] = false;
        }
        return temp;
    };
    $scope.setActionID = function (id) {
        $scope.selectedAction = angular.copy(id);
    };
    $scope.setCredData = function () {
        $http.get('/Credentialing/Initiation/GetAllCredentialings').
         success(function (data, status, headers, config) {

             console.log(data);
             $scope.data = data;

             for (var i = 0; i < $scope.data.length ; i++) {
                 $scope.data[i].InitiationDate = $scope.ConvertDateFormat($scope.data[i].InitiationDate);
                 $scope.data[i].FirstName = $scope.data[i].Profile.PersonalDetail.FirstName;
                 $scope.data[i].LastName = $scope.data[i].Profile.PersonalDetail.LastName;
                 $scope.data[i].PlanName = $scope.data[i].Plan.PlanName;
                 $scope.getStatus($scope.data[i].CredentialingLogs, i)
             }
             $scope.init_table($scope.data, $scope.selectedAction);
         }).
         error(function (data, status, headers, config) {
             console.log("Error fetching data");
         });
    };


    //-------------- selection ---------------
    $scope.setActionID(1);
    $scope.setCredData();

    $scope.SearchProviderPanelToggle = function (divId) {

        $("#" + divId).slideToggle();
    };

    //==========credentialing list start===================

    $scope.getCredList = function () {
        $http.get('/Credentialing/Initiation/GetAllCredentialings').
         success(function (data, status, headers, config) {

             console.log(data);
             $scope.data = data;

             for (var i = 0; i < $scope.data.length ; i++) {
                 $scope.data[i].InitiationDate = $scope.ConvertDateFormat($scope.data[i].InitiationDate);
                 $scope.data[i].FirstName = $scope.data[i].Profile.PersonalDetail.FirstName;
                 $scope.data[i].LastName = $scope.data[i].Profile.PersonalDetail.LastName;
                 $scope.data[i].PlanName = $scope.data[i].Plan.PlanName;
             }
             $scope.init_table($scope.data, $scope.selectedAction);
         }).
         error(function (data, status, headers, config) {
             console.log("Error fetching data");
         });
    }

    $scope.getReCredList = function () {
        $http.get('/Credentialing/Initiation/GetAllReCredentialings').
          success(function (data, status, headers, config) {

              console.log(data);
              $scope.data = data;

              for (var i = 0; i < $scope.data.length ; i++) {
                  $scope.data[i].InitiationDate = $scope.ConvertDateFormat($scope.data[i].InitiationDate);
                  $scope.data[i].FirstName = $scope.data[i].Profile.PersonalDetail.FirstName;
                  $scope.data[i].LastName = $scope.data[i].Profile.PersonalDetail.LastName;
                  $scope.data[i].PlanName = $scope.data[i].Plan.PlanName;
              }
              $scope.init_table(data, $scope.selectedAction);
          }).
          error(function (data, status, headers, config) {
              console.log("Error fetching data");
          });
    }

    $scope.getDeCredList = function () {
        $http.get('/Credentialing/Initiation/GetAllDeCredentialings').
         success(function (data, status, headers, config) {

             console.log(data);
             $scope.data = data;

             for (var i = 0; i < $scope.data.length ; i++) {
                 $scope.data[i].InitiationDate = $scope.ConvertDateFormat($scope.data[i].InitiationDate);
                 $scope.data[i].FirstName = $scope.data[i].Profile.PersonalDetail.FirstName;
                 $scope.data[i].LastName = $scope.data[i].Profile.PersonalDetail.LastName;
                 $scope.data[i].PlanName = $scope.data[i].Plan.PlanName;
             }
             $scope.init_table(data, $scope.selectedAction);
         }).
         error(function (data, status, headers, config) {
             console.log("Error fetching data");
         });
    }

    //==========credential list end========================

    //Created function to be called when data loaded dynamically

    $scope.init_table = function (data, condition)  {

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
            $scope.tableParams1 = new ngTableParams({
                page: 1,            // show first page
                count: 10,          // count per page
                filter: {
                    //name: 'M'       // initial filter
                    //FirstName : ''
                },
                sorting: {

                    InitiationDate: 'desc'
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
        }
        else if (condition == 2) {
            $scope.tableParams2 = new ngTableParams({
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
        }
        else if (condition == 3) {
            $scope.tableParams3 = new ngTableParams({
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
        }
    };


    //if filter is on
    $scope.ifFilter = function () {
        try {
            var bar;
            var obj;
            if ($scope.selectedAction == 1) {
                obj = $scope.tableParams1.$params.filter;
            }
            else if ($scope.selectedAction == 2 || $scope.selectedAction == 3) {
                obj = $scope.tableParams2.$params.filter;
            }
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
                if ($scope.selectedAction == 1) {
                    return ($scope.tableParams1.$params.page * $scope.tableParams1.$params.count) - ($scope.tableParams1.$params.count - 1);
                }
                else if ($scope.selectedAction == 2 || $scope.selectedAction == 3) {
                    return ($scope.tableParams2.$params.page * $scope.tableParams2.$params.count) - ($scope.tableParams2.$params.count - 1);
                }
            }
        }
        catch (e) { }
    }
    //Get index Last in table
    $scope.getIndexLast = function () {
        try {
            if ($scope.groupBySelected == 'none') {
                if ($scope.selectedAction == 1) {
                    return { true: ($scope.data.length), false: ($scope.tableParams1.$params.page * $scope.tableParams1.$params.count) }[(($scope.tableParams1.$params.page * $scope.tableParams1.$params.count) > ($scope.data.length))];
                }
                else if ($scope.selectedAction == 2 || $scope.selectedAction == 3) {
                    return { true: ($scope.data.length), false: ($scope.tableParams2.$params.page * $scope.tableParams2.$params.count) }[(($scope.tableParams2.$params.page * $scope.tableParams2.$params.count) > ($scope.data.length))];
                }
            }
        }
        catch (e) { }
    }

    $scope.SearchProviderPanelToggleDown = function (divId) {
        $(".closePanel").slideUp();
        $("#" + divId).slideToggle();
    };

    $scope.clearSearch = function () {
        $scope.tempObject = "";
        $scope.data = "";
        $scope.allProviders = null;
        //$('a[href=#SearchResult]').trigger('click');
        // $scope.Npi = null;
    }
    $scope.ClearInit = function () {
        $scope.showInit = false;

    }
    $scope.msgAlert = false;

   

    //================================= Hide All country code popover =========================
    $(document).click(function (event) {
        if (!$(event.target).hasClass("form-control") && $(event.target).parents(".ProviderTypeSelectAutoList").length === 0) {
            $(".ProviderTypeSelectAutoList").hide();
        }
        if (!$(event.target).attr("data-searchdropdown") && $(event.target).parents(".ProviderTypeSelectAutoList1").length === 0) {
            $(".ProviderTypeSelectAutoList1").hide();
        }
    });

    $(document).ready(function () {
        $(".ProviderTypeSelectAutoList").hide();
        $(".ProviderTypeSelectAutoList1").hide();
        $scope.SearchProviderPanelToggleDown('SearchProviderPanel');

    });

    function showLocationList(ele) {
        $(ele).parent().find(".ProviderTypeSelectAutoList").first().show();
    }

});