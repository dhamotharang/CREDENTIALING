
//--------------------- Angular Module ----------------------
var credentialingList = angular.module('CredentialingList', ['InitCredApp', 'ngTable']);

//=========================== Controller declaration ==========================
credentialingList.controller('credentialingListController',['$scope', '$http', '$filter', 'ngTableParams','$rootScope', 'messageAlertEngine', function ($scope, $http, $filter, ngTableParams,$rootScope, messageAlertEngine) {

    $scope.data = [];
    $scope.groupBySelected = "none";
    $scope.progressbar = false;
    $scope.error_message = "";
    $scope.showInit = false;
    $scope.status = [];

    $scope.isInitiated = false;
    $scope.isVerified = false;
    $scope.isCCMDone = false;
    $scope.isLoaded = false;
    $scope.isreCredentialed = false;
    $scope.isdeCredentialed = false;
    $scope.isCompleted = false;

    $scope.getStatus = function (data, index) {        

        if (data[data.length - 1].CredentialingActivityLogs != null && data[data.length - 1].CredentialingActivityLogs.length != 0) {

            if (data[data.length - 1].CredentialingActivityLogs[data[data.length - 1].CredentialingActivityLogs.length - 1].Activity == "Initiation") {
                if (data[data.length - 1].CredentialingActivityLogs[data[data.length - 1].CredentialingActivityLogs.length - 1].ActivityStatus == "Completed") {

                    //$scope.status.push("Initiated");
                    $scope.data[index].CurrentStatus = "Initiated";
                    $scope.isInitiated = true;
                    $scope.credInitDate =data[data.length - 1].CredentialingActivityLogs[data[data.length - 1].CredentialingActivityLogs.length - 1].LastModifiedDate;
                }

            } else if (data[data.length - 1].CredentialingActivityLogs[data[data.length - 1].CredentialingActivityLogs.length - 1].Activity == "PSV") {

                if (data[data.length - 1].CredentialingActivityLogs[data[data.length - 1].CredentialingActivityLogs.length - 1].ActivityStatus == "Completed") {

                    //$scope.status.push("Verified");
                    $scope.data[index].CurrentStatus = "Verified";
                    $scope.isVerified = true;
                    $scope.psvInitDate = data[data.length - 1].CredentialingActivityLogs[data[data.length - 1].CredentialingActivityLogs.length - 1].LastModifiedDate;

                } else {

                    //$scope.status.push("Initiated");
                    $scope.data[index].CurrentStatus = "Initiated";

                }

            } else if (data[data.length - 1].CredentialingActivityLogs[data[data.length - 1].CredentialingActivityLogs.length - 1].Activity == "CCMAppointment") {

                if (data[data.length - 1].CredentialingActivityLogs[data[data.length - 1].CredentialingActivityLogs.length - 1].ActivityStatus == "Completed") {

                    //$scope.status.push("CCM");
                    $scope.data[index].CurrentStatus = "CCM";
                    $scope.isCCMDone = true;
                    $scope.ccmInitDate = data[data.length - 1].CredentialingActivityLogs[data[data.length - 1].CredentialingActivityLogs.length - 1].LastModifiedDate;

                } else {

                    //$scope.status.push("Verified");
                    $scope.data[index].CurrentStatus = "Verified";

                }

            } else if (data[data.length - 1].CredentialingActivityLogs[data[data.length - 1].CredentialingActivityLogs.length - 1].Activity == "Loading") {

                if (data[data.length - 1].CredentialingActivityLogs[data[data.length - 1].CredentialingActivityLogs.length - 1].ActivityStatus == "Completed") {

                    //$scope.status.push("Submitted");
                    $scope.data[index].CurrentStatus = "Submitted";
                    $scope.isLoaded = true;
                    $scope.loadToPlanInitDate = data[data.length - 1].CredentialingActivityLogs[data[data.length - 1].CredentialingActivityLogs.length - 1].LastModifiedDate;

                } else {

                    //$scope.status.push("CCM");    
                    $scope.data[index].CurrentStatus = "CCM";

                }

            } else if (data[data.length - 1].CredentialingActivityLogs[data[data.length - 1].CredentialingActivityLogs.length - 1].Activity == "Report") {

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


    $scope.ConvertDateFormat = function (value) {
        var today = new Date(value);
        var dd = today.getDate();
        var mm = today.getMonth() + 1;
        var yyyy = today.getFullYear();
        if (dd < 10) { dd = '0' + dd }
        if (mm < 10) { mm = '0' + mm }
        var today = mm + '-' + dd + '-' + yyyy;
        return today;
    };

    $scope.setCredInfo = function (id) {
        sessionStorage.setItem('credentialingInfoId', id);
        //sessionStorage.setItem('ButtonCLickName', "1");
    };

    $scope.currentTabStatus = function (status) {
        sessionStorage.setItem('tabStatus', status);
        $scope.tb = sessionStorage.getItem('tabStatus');
    }

    $scope.setCreListId = function (id) {
       //$scope.currentTabStatus(stat);
       sessionStorage.setItem('CreListId', id);
        $scope.selectedAction = id;
    }


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
        
        //$scope.selectedAction = id;
        messageAlertEngine.setActionID1(id);
        //$rootScope.selectedAction1 = id;
        $scope.selectedAction = id;       
        $scope.setCreListId(id);
        //alert($scope.selectedAction);
    };
    $scope.setCredData = function () {
        
        $scope.loadingAjax = true;
        $http.get(rootDir+ '/Credentialing/Initiation/GetAllCredentialings').
         success(function (data, status, headers, config) {
             //console.log("Pandu");
             //console.log(data);
             $scope.data = data;

             for (var i = 0; i < $scope.data.length ; i++) {
                 $scope.data[i].InitiationDate = $scope.ConvertDateFormat($scope.data[i].InitiationDate);
                 $scope.data[i].FirstName = $scope.data[i].Profile.PersonalDetail.FirstName;
                 $scope.data[i].LastName = $scope.data[i].Profile.PersonalDetail.LastName;
                 $scope.data[i].PlanName = $scope.data[i].Plan.PlanName;
                 $scope.getStatus($scope.data[i].CredentialingLogs, i);
                 if ($scope.data[i].Status=="Inactive")
                 {
                     $scope.data[i].CurrentStatus = "Completed";
                 }
                 $scope.data[i].initType = $scope.data[i].CredentialingLogs[$scope.data[i].CredentialingLogs.length - 1].Credentialing;
             }
             $scope.loadingAjax = false;
             $scope.init_table($scope.data, $scope.selectedAction);
         }).
         error(function (data, status, headers, config) {
             console.log("Error fetching data");
         });
    };

    //-------------- selection ---------------
   

    $scope.SearchProviderPanelToggle = function (divId) {

        $("#" + divId).slideToggle();
    };

    //==========credentialing list start===================
    $scope.tabID = 1;
    $scope.getCredList = function () {
        $scope.tabID = 1;
        $scope.loadingAjax = true;
        $http.get(rootDir + '/Credentialing/Initiation/GetAllCredentialings').
         success(function (data, status, headers, config) {
             $scope.data = data;
             for (var i = 0; i < $scope.data.length ; i++) {
                 $scope.data[i].InitiationDate = $scope.ConvertDateFormat($scope.data[i].InitiationDate);
                 $scope.data[i].FirstName = $scope.data[i].Profile.PersonalDetail.FirstName;
                 $scope.data[i].LastName = $scope.data[i].Profile.PersonalDetail.LastName;
                 $scope.data[i].PlanName = $scope.data[i].Plan.PlanName;
                 $scope.data[i].initType = $scope.data[i].CredentialingLogs[$scope.data[i].CredentialingLogs.length - 1].Credentialing;
                 $scope.getStatus($scope.data[i].CredentialingLogs, i);
                 if ($scope.data[i].Status == "Inactive") {
                     $scope.data[i].CurrentStatus = "Completed";
                 }
             }
             $scope.loadingAjax = false;
             $scope.init_table($scope.data, $scope.selectedAction);
         }).
         error(function (data, status, headers, config) {
             console.log("Error fetching data");
         });
    }

    $scope.getReCredList = function () {
        $scope.tabID = 2;
        $scope.loadingAjax = true;
        $http.get(rootDir + '/Credentialing/Initiation/GetAllReCredentialings').
          success(function (data, status, headers, config) {
              $scope.data = data;
              for (var i = 0; i < $scope.data.length ; i++) {
                  //$scope.data[i].InitiationDate = ($scope.data[i].InitiationDate).getMonth() + 1 + "/" + ($scope.data[i].InitiationDate).getDate() + "/" + ($scope.data[i].InitiationDate).getYear();
                  $scope.data[i].InitiationDate = $scope.ConvertDateFormat($scope.data[i].InitiationDate);
                  //$scope.data[i].InitiationDate =$scope.data[i].InitiationDate.toDateString();
                  $scope.data[i].FirstName = $scope.data[i].Profile.PersonalDetail.FirstName;
                  $scope.data[i].LastName = $scope.data[i].Profile.PersonalDetail.LastName;
                  $scope.data[i].PlanName = $scope.data[i].Plan.PlanName;
                  
                  $scope.getStatus($scope.data[i].CredentialingLogs, i);
                  if ($scope.data[i].Status == "Inactive") {
                      $scope.data[i].CurrentStatus = "Completed";
                  }
                  //$scope.data[i].initType = $scope.data[i].CredentialingLogs[$scope.data[i].CredentialingLogs.length - 1].Credentialing;
              }
              console.log('recred data');
              console.log($scope.data);
              $scope.loadingAjax = false;
              $scope.init_table($scope.data, $scope.selectedAction);

          }).
          error(function (data, status, headers, config) {
              console.log("Error fetching data");
          });
    }

   


    $scope.deCredList = [];
    $scope.getDeCredList = function () {
        $scope.tabID = 3;
        $scope.loadingAjax = true;
        $http.get(rootDir + '/Credentialing/Initiation/GetAllDeCredentialings').
         success(function (data, status, headers, config) {
             $scope.deCredList = angular.copy(data);
             $scope.data = data;
             $scope.Title = "";
             for (var i = 0; i < $scope.data.length ; i++) {
                 $scope.data[i].InitiationDate = $scope.ConvertDateFormat($scope.data[i].InitiationDate);
                 $scope.data[i].FirstName = $scope.data[i].Profile.PersonalDetail.FirstName;
                 $scope.data[i].LastName = $scope.data[i].Profile.PersonalDetail.LastName;
                 $scope.data[i].PlanName = $scope.data[i].Plan.PlanName;
                 $scope.data[i].LastModifiedDate = $scope.ConvertDateFormat($scope.data[i].LastModifiedDate);
                 $scope.data[i].Title = "";
                 for (var j = 0; j < $scope.data[i].Profile.PersonalDetail.ProviderTitles.length; j++) {
                     if ($scope.data[i].Title == "")
                         $scope.data[i].Title = $scope.data[i].Profile.PersonalDetail.ProviderTitles[j].ProviderType.Title;
                     else 
                         $scope.data[i].Title +=", "+ $scope.data[i].Profile.PersonalDetail.ProviderTitles[j].ProviderType.Title;

                 }
                 $scope.getStatus($scope.data[i].CredentialingLogs, i);
                 if ($scope.data[i].Status == "Inactive") {
                     $scope.data[i].CurrentStatus = "Completed";
                 }
             }
            // $scope.fillData(data);
             $scope.loadingAjax = false;
             $scope.init_table($scope.data, $scope.selectedAction);
         }).
         error(function (data, status, headers, config) {
             console.log("Error fetching data");
         });
    }

    if (sessionStorage.getItem('CreListId') == 1 || sessionStorage.getItem('CreListId') == 2 || sessionStorage.getItem('CreListId') == 3) {
        $scope.selectedAction = sessionStorage.getItem('CreListId');
        if (sessionStorage.getItem('CreListId') == 1) {
            $scope.getCredList();
        }
        else if (sessionStorage.getItem('CreListId') == 2) {
            $scope.getReCredList();
        }
        else if (sessionStorage.getItem('CreListId') == 3) {
            $scope.getDeCredList();
        }
    } else {
        $scope.setActionID(1);
        $scope.getCredList();
    }


    $scope.profileInfo = [];

    $scope.fillData = function (data) {

        $scope.ProfileID = data.ProfileID;

        for (var i = 0; i < $scope.data.length; i++) {

            if ($scope.data[i].CredentialingContractRequests != null && $scope.data[i].CredentialingContractRequests != 0) {

                for (var j = 0; j < $scope.data[i].CredentialingContractRequests.length; j++) {

                    if ($scope.data[i].CredentialingContractRequests[j].ContractGrid != null && $scope.data[i].CredentialingContractRequests[j].ContractGrid != 0) {

                        for (var k = 0; k < $scope.data[i].CredentialingContractRequests[j].ContractGrid.length; k++) {

                            if ($scope.data[i].CredentialingContractRequests[j].ContractGrid[k].Report != null) {

                                $scope.profileInfo.push($scope.data[i].CredentialingContractRequests[j].ContractGrid[k]);
                                $scope.profileInfo[$scope.profileInfo.length - 1].FirstName = $scope.data[i].FirstName;
                                $scope.profileInfo[$scope.profileInfo.length - 1].LastName = $scope.data[i].LastName;
                                $scope.profileInfo[$scope.profileInfo.length - 1].CredentialingInfoID = $scope.data[i].CredentialingInfoID;
                                $scope.profileInfo[$scope.profileInfo.length - 1].CredentialingContractRequestID = $scope.data[i].CredentialingContractRequests[j].CredentialingContractRequestID;
                                $scope.profileInfo[$scope.profileInfo.length - 1].PlanName = $scope.data[i].Plan.PlanName;
                                $scope.profileInfo[$scope.profileInfo.length - 1].Speciality = $scope.data[i].CredentialingContractRequests[j].ContractGrid[k].ProfileSpecialty.Specialty.Name;
                                $scope.profileInfo[$scope.profileInfo.length - 1].LOB = $scope.data[i].CredentialingContractRequests[j].ContractGrid[k].LOB.LOBName;
                                $scope.profileInfo[$scope.profileInfo.length - 1].Location = $scope.data[i].CredentialingContractRequests[j].ContractGrid[k].ProfilePracticeLocation.Facility.FacilityName;
                                $scope.profileInfo[$scope.profileInfo.length - 1].GroupName = $scope.data[i].CredentialingContractRequests[j].ContractGrid[k].BusinessEntity.GroupName;
                                $scope.profileInfo[$scope.profileInfo.length - 1].Check = false;
                                $scope.profileInfo[$scope.profileInfo.length - 1].InitiationDate = $scope.ConvertDateFormat($scope.data[i].InitiationDate);

                            }

                        }

                    }

                }

            }

            //$scope.profileDetail.push($scope.data[i]);

        }

        $scope.data = $scope.profileInfo;
        $scope.profileInfo = [];
        //console.log($scope.profileInfo);
    }

    //==========credential list end========================


    //==============Plan Report===========================
    $scope.planReportList = [];
    $scope.planReportObj = [];
    $scope.getPlanReport = function (obj) {
        console.log('abhi');
        console.log(obj);
        //edited by pritam
        $scope.credentialingInfo = obj;
        $scope.FullName = obj.Profile.PersonalDetail.FirstName + ' ' + obj.Profile.PersonalDetail.LastName;
        $scope.planName = obj.Plan.PlanName;
        
        for (var i = 0; i < obj.CredentialingContractRequests.length; i++) {
            for (var j = 0; j < obj.CredentialingContractRequests[i].ContractGrid.length; j++) {
                if (obj.CredentialingContractRequests[i].ContractGrid[j].Status == 'Inactive') {
                    $scope.planReportList.push(obj.CredentialingContractRequests[i].ContractGrid[j]);
                }
            }
        }

        for (var i = 0; i < obj.CredentialingLogs.length; i++) {
            $scope.getStatus(obj.CredentialingLogs[i], i);
        }

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
        else if (condition == 3) {
            if ($scope.tableParams3 == null) {
                $scope.tableParams3 = new ngTableParams({
                    page: 1,            // show first page
                    count: 10,          // count per page
                    filter: {
                        //name: 'M'       // initial filter
                        //FirstName : ''
                    },
                    sorting: {
                        LastModifiedDate: 'desc'
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
            } else {
                $scope.tableParams3.reload();
            }
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
                else if ($scope.selectedAction == 2) {
                    return ($scope.tableParams2.$params.page * $scope.tableParams2.$params.count) - ($scope.tableParams2.$params.count - 1);
                }
                else if ($scope.selectedAction == 3) {
                    return ($scope.tableParams3.$params.page * $scope.tableParams3.$params.count) - ($scope.tableParams3.$params.count - 1);
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
                else if ($scope.selectedAction == 2) {
                    return { true: ($scope.data.length), false: ($scope.tableParams2.$params.page * $scope.tableParams2.$params.count) }[(($scope.tableParams2.$params.page * $scope.tableParams2.$params.count) > ($scope.data.length))];
                }
                else if ($scope.selectedAction == 3) {
                    return { true: ($scope.data.length), false: ($scope.tableParams3.$params.page * $scope.tableParams3.$params.count) }[(($scope.tableParams3.$params.page * $scope.tableParams3.$params.count) > ($scope.data.length))];
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

}]);