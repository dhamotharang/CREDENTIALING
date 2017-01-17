//Module declaration
var trackerApp = angular.module('TrackerApp', ['ngTable', 'ui.bootstrap', 'mgcrea.ngStrap', 'loadingInteceptor']);

trackerApp.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

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
}]);

trackerApp.config(function ($datepickerProvider) {
    var nowDate = new Date();
    var today = new Date(nowDate.getFullYear(), nowDate.getMonth(), nowDate.getDate(), 0, 0, 0, 0);
    angular.extend($datepickerProvider.defaults, {
        //startDate: 'today',
        minDate: today,
        autoclose: true,
        useNative: true
    });
})

var TempFollowupHelper = [];
//Controller declaration
trackerApp.controller('TrackerCtrl', function ($scope, $rootScope, $http, $q, $filter, ngTableParams, messageAlertEngine) {

    $scope.DailyTasks = [];
    $scope.Tasks = [];
    $scope.CDUsers = [];
    $scope.LoginUsers = [];
    $scope.Providers = [];
    $scope.Hospitals = [];
    $scope.InsuranceCompanies = [];
    $scope.users = [];
    $scope.userAuthID = -1;
    $scope.FollowupHelper = [];
    var GlobalHospitalData = [];
    $scope.TasksAssigned = [];

    $scope.TasksAssignedtoMe = [];
    $scope.TasksAssignedByMe = [];
    $scope.AllTasks = [];
    $scope.RemainingTasks = [];
    $scope.showLoading = true;


    function CDUsersData() {
        var defer = $q.defer();
        $http.get(rootDir + '/TaskTracker/GetAllCDUsers').
            success(function (data, status, headers, config) {
                $scope.CDUsers = data;
                defer.resolve(data);
            }).
            error(function (data, status, headers, config) {
                defer.reject();
            });
        return defer.promise;
    }

    function HospitalsData() {
        var defer = $q.defer();
        $http.get(rootDir + '/Profile/MasterData/GetAllHospitals').
        success(function (data, status, headers, config) {
            $scope.Hospitals = data;
            GlobalHospitalData = data;
            defer.resolve(data);
        }).
        error(function (data, status, headers, config) {
            defer.reject();
        });
        return defer.promise;
    }
    var currentcduserdata = "";
    function GetCurrentUser() {
        var defer = $q.defer();
        $http.get(rootDir + '/Dashboard/GetMyNotification').
        success(function (data, status, headers, config) {
            currentcduserdata = data;
            defer.resolve(data);
        })
        .error(function (data, status, headers, config) {
            defer.reject();
        });
        return defer.promise;
    }

    function InsuranceCompaniesData() {
        var defer = $q.defer();
        $http.get(rootDir + '/TaskTracker/GetAllInsuranceCompanies').
        success(function (data, status, headers, config) {
            $scope.InsuranceCompanies = data;
            defer.resolve(data);
        }).
        error(function (data, status, headers, config) {
            defer.reject();
        });
        return defer.promise;
    }

    function TasksData() {
        var defer = $q.defer();
        $http.get(rootDir + '/TaskTracker/GetAllTasksByUserId').
            success(function (data, status, headers, config) {
                $scope.Tasks = angular.copy(data);
                defer.resolve(data);
            }).
error(function (data, status, headers, config) {
    defer.reject();
});
        return defer.promise;
    }

    function ProvidersData() {
        var defer = $q.defer();
        $http.get(rootDir + '/TaskTracker/GetAllProviders').
            success(function (data, status, headers, config) {
                $scope.Providers = data;
                defer.resolve(data);
            }).
            error(function (data, status, headers, config) {
                defer.reject();
            });
        return defer.promise;
    }

    function LoginUsersData() {
        var defer = $q.defer();
        $http.get(rootDir + '/TaskTracker/GetAllUsers').
            success(function (data, status, headers, config) {
                $scope.LoginUsers = data;
                defer.resolve(data);
            }).
            error(function (data, status, headers, config) {
                defer.reject();
            });
        return defer.promise;
    }

    $scope.DailyTasksPagination = [];

    // Data For Daily Tasks ----------------------------------------------------------------------
    $scope.LoadData = function () {
        $scope.progressbar = false;
        $q.all([$q.when(GetCurrentUser()),
            $q.when(CDUsersData()),
            $q.when(HospitalsData()),
            $q.when(TasksData()),
            $q.when(ProvidersData()),
            $q.when(InsuranceCompaniesData()),
            $q.when(LoginUsersData())
        ]).then(function (response) {
            for (var i in $scope.Tasks) {
                if ($scope.Tasks[i].ModeOfFollowUp != "") {
                    $scope.Tasks[i].ModeOfFollowUp = JSON.parse($scope.Tasks[i].ModeOfFollowUp);
                }
                if ($scope.Tasks[i].AssignedToId == currentcduserdata.cdUser.CDUserID) {
                    $scope.TasksAssignedtoMe.push($scope.Tasks[i]);
                }
                if ($scope.Tasks[i].AssignedById == currentcduserdata.cdUser.CDUserID) {
                    $scope.TasksAssignedByMe.push($scope.Tasks[i]);
                }
                if ($scope.Tasks[i].AssignedToId !== currentcduserdata.cdUser.CDUserID && $scope.Tasks[i].AssignedById !== currentcduserdata.cdUser.CDUserID) {
                    $scope.AllTasks.push($scope.Tasks[i]);
                }
            }
            $scope.userAuthID = currentcduserdata.cdUser.CDUserID;

            $scope.SubSections = [
                              { Name: "State License", TabName: "#identification", SubSectionID: "#StateLicense" },
                              { Name: "Federal DEA Information", TabName: "#identification", SubSectionID: "#FederalDEA" },
                              { Name: "Medicare Information", TabName: "#identification", SubSectionID: "#MedicareInformation" },
                              { Name: "Medicaid Information", TabName: "#identification", SubSectionID: "#MedicaidInformation" },
                              { Name: "Provider Missing Items Documentation", TabName: "", SubSectionID: "" },
                              { Name: "Insurance Enrollment", TabName: "", SubSectionID: "" },
                              { Name: "Hospital Enrollment", TabName: "", SubSectionID: "" },
                              { Name: "Insurance Re-Enrollment", TabName: "", SubSectionID: "" },
                              { Name: "Hospital Re-Enrollment", TabName: "", SubSectionID: "" },
                              { Name: "CAQH Update", TabName: "", SubSectionID: "" },
                              { Name: "Location Change", TabName: "", SubSectionID: "" },
                              { Name: "Panel Closing", TabName: "", SubSectionID: "" },
                              { Name: "Provider Termination", TabName: "", SubSectionID: "" },
            ];
            $scope.Followups = [
                                      { Name: "Phone Call", Type: "PhoneCall" },
                                      { Name: "Email", Type: "Email" },
                                      { Name: "Online Form Completion", Type: "OnlineFormCompletion" },
                                      { Name: "Paper Form Completion", Type: "PaperFormCompletion" }
            ];
            $scope.FollowupHelper = angular.copy($scope.Followups);
            TempFollowupHelper = angular.copy($scope.Followups);
            for (var i = 0; i < $scope.LoginUsers.length; i++) {

                for (var j = 0; j < $scope.CDUsers.length; j++) {

                    if ($scope.LoginUsers[i].Id == $scope.CDUsers[j].AuthenicateUserId) {

                        $scope.users.push({ CDUserID: $scope.CDUsers[j].CDUserID, AuthenicateUserId: $scope.CDUsers[j].AuthenicateUserId, FullName: $scope.LoginUsers[i].FullName, Email: $scope.LoginUsers[i].Email, UserName: $scope.LoginUsers[i].UserName })
                    }
                }
            }
            $rootScope.$broadcast("providers", $scope.Providers);
            $rootScope.$broadcast("hospitals", $scope.Hospitals);
            $rootScope.$broadcast("Insurance", $scope.InsuranceCompanies);
            $rootScope.$broadcast("Users", $scope.users);
            
            for (var k = 0; k < $scope.TasksAssignedtoMe.length; k++) {

                if ($scope.TasksAssignedtoMe[k].ProfileID != null)
                    var ProviderName = $scope.Providers.filter(function (Providers) { return Providers.ProfileId == $scope.TasksAssignedtoMe[k].ProfileID })[0].Name;

                if ($scope.TasksAssignedtoMe[k].AssignedToId != null) {
                    var AssignedTo = $scope.users.filter(function (users) { return users.CDUserID == $scope.TasksAssignedtoMe[k].AssignedToId })[0].UserName;
                }
                var HospitalNamefortaskassigned = "";
                for (var h in $scope.Hospitals) {
                    if ($scope.TasksAssignedtoMe[k].HospitalID == $scope.Hospitals[h].HospitalID) {
                        HospitalNamefortaskassigned = $scope.Hospitals[h].HospitalName;
                    }
                }
                var TasksAssignedinsurancecompanyname = "";
                for (var p in $scope.InsuranceCompanies) {
                    if ($scope.TasksAssignedtoMe[k].InsuaranceCompanyNameID == $scope.InsuranceCompanies[p].InsuaranceCompanyNameID) {
                        TasksAssignedinsurancecompanyname = $scope.InsuranceCompanies[p].CompanyName;
                    }
                }

                var tabID = "";
                var sectionID = "";
                var Followups = "";
                for (var i in $scope.SubSections) {
                    if ($scope.SubSections[i].Name == $scope.TasksAssignedtoMe[k].SubSectionName) {
                        tabID = $scope.SubSections[i].TabName;
                        sectionID = $scope.SubSections[i].SubSectionID;
                    }
                }
                var followupdatefortasksassigned = $scope.ConvertDate($scope.TasksAssignedtoMe[k].NextFollowUpDate);
                $scope.TasksAssigned.push({
                    TaskTrackerId: $scope.TasksAssignedtoMe[k].TaskTrackerId, ProfileID: $scope.TasksAssignedtoMe[k].ProfileID,
                    ProviderName: ProviderName, SubSectionName: $scope.TasksAssignedtoMe[k].SubSectionName,
                    Subject: $scope.TasksAssignedtoMe[k].Subject, NextFollowUpDate: followupdatefortasksassigned,
                    ModeOfFollowUp: $scope.TasksAssignedtoMe[k].ModeOfFollowUp, FollowUp: Followups,
                    InsuranceCompanyName: TasksAssignedinsurancecompanyname,
                    AssignedToId: $scope.users.filter(function (users) { return users.CDUserID == $scope.TasksAssignedtoMe[k].AssignedToId })[0].AuthenicateUserId, AssignedTo: AssignedTo, HospitalID: $scope.TasksAssignedtoMe[k].HospitalID,
                    Hospital: HospitalNamefortaskassigned, Notes: $scope.TasksAssignedtoMe[k].Notes, ModifiedDate: $scope.TasksAssignedtoMe[k].LastModifiedDate,
                    TabID: tabID,
                    SubSectionID: sectionID,
                    CompleteStatus: $scope.TasksAssignedtoMe[k].Status == "Active" ? "Open" : "Closed"
                });
            }
            $rootScope.$broadcast("evnt", $scope.TasksAssigned);
            for (var k = 0; k < $scope.TasksAssignedByMe.length; k++) {

                if ($scope.TasksAssignedByMe[k].ProfileID != null)
                    var ProviderName = $scope.Providers.filter(function (Providers) { return Providers.ProfileId == $scope.TasksAssignedByMe[k].ProfileID })[0].Name;
                var AssignedTo = "";
                var Assignedtoid = "";
                if ($scope.TasksAssignedByMe[k].AssignedToId != null) {
                    for (var i in $scope.users) {
                        if ($scope.users[i].CDUserID == $scope.TasksAssignedByMe[k].AssignedToId) {
                            AssignedTo = $scope.users[i].UserName;
                            Assignedtoid = $scope.users[i].AuthenicateUserId;
                        }
                    }
                    //var AssignedTo = $scope.users.filter(function (users) { return users.CDUserID == $scope.TasksAssignedByMe[k].AssignedToId })[0].UserName;
                }
                var DailyTaskHospitalName = "";
                for (var h in $scope.Hospitals) {
                    if ($scope.Hospitals[h].HospitalID == $scope.TasksAssignedByMe[k].HospitalID) {
                        DailyTaskHospitalName = $scope.Hospitals[h].HospitalName;
                    }
                }
                var DailyTasksinsurancecompanyname = "";
                var tabID = "";
                var sectionID = "";
                var Followups = "";
                for (var i in $scope.SubSections) {
                    if ($scope.SubSections[i].Name == $scope.TasksAssignedByMe[k].SubSectionName) {
                        tabID = $scope.SubSections[i].TabName;
                        sectionID = $scope.SubSections[i].SubSectionID;
                    }
                }
                for (var p in $scope.InsuranceCompanies) {
                    if ($scope.TasksAssignedByMe[k].InsuaranceCompanyNameID == $scope.InsuranceCompanies[p].InsuaranceCompanyNameID) {
                        DailyTasksinsurancecompanyname = $scope.InsuranceCompanies[p].CompanyName;
                    }
                }
                var followupdatefordailytasks = $scope.ConvertDate($scope.TasksAssignedByMe[k].NextFollowUpDate);
                $scope.DailyTasks.push({
                    TaskTrackerId: $scope.TasksAssignedByMe[k].TaskTrackerId, ProfileID: $scope.TasksAssignedByMe[k].ProfileID,
                    ProviderName: ProviderName, SubSectionName: $scope.TasksAssignedByMe[k].SubSectionName,
                    Subject: $scope.TasksAssignedByMe[k].Subject, NextFollowUpDate: followupdatefordailytasks,
                    ModeOfFollowUp: $scope.TasksAssignedByMe[k].ModeOfFollowUp, FollowUp: Followups,
                    InsuranceCompanyName: DailyTasksinsurancecompanyname,
                    AssignedToId: Assignedtoid, AssignedTo: AssignedTo, HospitalID: $scope.TasksAssignedByMe[k].HospitalID,
                    Hospital: DailyTaskHospitalName, Notes: $scope.TasksAssignedByMe[k].Notes, ModifiedDate: $scope.TasksAssignedByMe[k].LastModifiedDate,
                    TabID: tabID,
                    SubSectionID: sectionID,
                    CompleteStatus: $scope.TasksAssignedByMe[k].Status == "Active" ? "Open" : "Closed"
                });
            }
            for (var i in $scope.TasksAssigned)
            {
                for(var j in $scope.DailyTasks)
                {
                    if($scope.TasksAssigned[i].TaskTrackerId ==$scope.DailyTasks[j].TaskTrackerId )
                    {
                        $scope.DailyTasks.splice(j,1);
                    }
                }
            }
            $rootScope.$broadcast("evn", $scope.DailyTasks);
            for (var k = 0; k < $scope.AllTasks.length; k++) {

                if ($scope.AllTasks[k].ProfileID != null)
                    var ProviderName = $scope.Providers.filter(function (Providers) { return Providers.ProfileId == $scope.AllTasks[k].ProfileID })[0].Name;

                if ($scope.AllTasks[k].AssignedToId != null) {
                    var AssignedTo = $scope.users.filter(function (users) { return users.CDUserID == $scope.AllTasks[k].AssignedToId })[0].UserName;
                }
                var HospitalNameforremainingtasks = "";
                for (var h in $scope.Hospitals) {
                    if ($scope.Hospitals[h].HospitalID == $scope.AllTasks[k].HospitalID) {
                        HospitalNameforremainingtasks = $scope.Hospitals[h].HospitalName;
                    }
                }
                var InsuranceCompanyforalltasks = "";
                for (var i in $scope.InsuranceCompanies) {
                    if ($scope.AllTasks[k].InsuaranceCompanyNameID == $scope.InsuranceCompanies[i].InsuaranceCompanyNameID) {
                        InsuranceCompanyforalltasks = $scope.InsuranceCompanies[i].CompanyName;
                    }
                }
                var tabID = "";
                var sectionID = "";
                var Followups = "";
                for (var i in $scope.SubSections) {
                    if ($scope.SubSections[i].Name == $scope.AllTasks[k].SubSectionName) {
                        tabID = $scope.SubSections[i].TabName;
                        sectionID = $scope.SubSections[i].SubSectionID;
                    }
                }
                var date3 = [];
                var followupdateforalltasks = $scope.ConvertDate($scope.AllTasks[k].NextFollowUpDate);
                $scope.RemainingTasks.push({
                    TaskTrackerId: $scope.AllTasks[k].TaskTrackerId, ProfileID: $scope.AllTasks[k].ProfileID,
                    ProviderName: ProviderName, SubSectionName: $scope.AllTasks[k].SubSectionName,
                    Subject: $scope.AllTasks[k].Subject, NextFollowUpDate: followupdateforalltasks,
                    ModeOfFollowUp: $scope.AllTasks[k].ModeOfFollowUp, FollowUp: Followups,
                    InsuranceCompanyName: InsuranceCompanyforalltasks,
                    AssignedToId: $scope.users.filter(function (users) { return users.CDUserID == $scope.AllTasks[k].AssignedToId })[0].AuthenicateUserId, AssignedTo: AssignedTo, HospitalID: $scope.AllTasks[k].HospitalID,
                    Hospital: HospitalNameforremainingtasks, Notes: $scope.AllTasks[k].Notes, ModifiedDate: $scope.AllTasks[k].LastModifiedDate,
                    TabID: tabID,
                    SubSectionID: sectionID,
                    CompleteStatus: $scope.AllTasks[k].Status == "Active" ? "Open" : "Closed"
                });
            }
            $rootScope.$broadcast("event", $scope.RemainingTasks);
            $scope.progressbar = true;
            $scope.showLoading = false;
            $('#tasktrackerdiv').show();


        }, function (error) {
            $scope.progressbar = true;
            $scope.showLoading = false;
            $('#tasktrackerdiv').show();
        });
    }
    $rootScope.$on("evn", function (event, args) {
        $scope.DailyTasks = angular.copy(args);
        if ($scope.DailyTasks.length > 9) {
            for (i = 0; i < 10; i++) {
                $scope.DailyTasksPagination[i] = $scope.DailyTasks[i];
            }
            $scope.bigTotalItems = $scope.DailyTasks.length;

            $scope.CurrentPage = [];
            $scope.bigCurrentPage = 1;

            var startIndex = ($scope.bigCurrentPage - 1) * 10;
            var endIndex = startIndex + 9;

            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.DailyTasks[startIndex]) {
                    $scope.CurrentPage.push($scope.DailyTasks[startIndex]);
                } else {
                    break;
                }
            }
        }
        else {
            $scope.DailyTasksPagination = angular.copy($scope.DailyTasks);
            $scope.bigTotalItems = $scope.DailyTasks.length;
        }

    });
    $scope.TasksAssignedPagination = [];
    $rootScope.$on("evnt", function (event, args) {
        $scope.TasksAssigned = angular.copy(args);
        if ($scope.TasksAssigned.length > 9) {
            for (i = 0; i < 10; i++) {
                $scope.TasksAssignedPagination[i] = $scope.TasksAssigned[i];
            }
            $scope.bigTotalItemsforTasksAssigned = $scope.TasksAssigned.length;

            $scope.CurrentPageforTasksAssigned = [];
            $scope.bigCurrentPageforTasksAssigned = 1;

            var startIndexforTasksAssigned = ($scope.bigCurrentPageforTasksAssigned - 1) * 10;
            var endIndexforTasksAssigned = startIndexforTasksAssigned + 9;

            for (startIndexforTasksAssigned; startIndexforTasksAssigned <= endIndexforTasksAssigned ; startIndexforTasksAssigned++) {
                if ($scope.TasksAssigned[startIndexforTasksAssigned]) {
                    $scope.CurrentPageforTasksAssigned.push($scope.TasksAssigned[startIndexforTasksAssigned]);
                } else {
                    break;
                }
            }
        } else {
            $scope.TasksAssignedPagination = angular.copy($scope.TasksAssigned);
            $scope.bigTotalItemsforTasksAssigned = $scope.TasksAssigned.length;
        }

    });
    $scope.RemainingTasksPagination = [];
    $rootScope.$on("event", function (event, args) {
        $scope.RemainingTasks = angular.copy(args);
        if ($scope.RemainingTasks.length > 9) {
            for (i = 0; i < 10; i++) {
                $scope.RemainingTasksPagination[i] = $scope.RemainingTasks[i];
            }
            $scope.bigTotalItemsforRemainingTasks = $scope.RemainingTasks.length;

            $scope.CurrentPageforRemainingTasks = [];
            $scope.bigCurrentPageforRemainingTasks = 1;

            var startIndexforRemainingTasks = ($scope.bigCurrentPageforRemainingTasks - 1) * 10;
            var endIndexforRemainingTasks = startIndexforRemainingTasks + 9;

            for (startIndexforRemainingTasks; startIndexforRemainingTasks <= endIndexforRemainingTasks ; startIndexforRemainingTasks++) {
                if ($scope.RemainingTasks[startIndexforRemainingTasks]) {
                    $scope.CurrentPageforRemainingTasks.push($scope.RemainingTasks[startIndexforRemainingTasks]);
                } else {
                    break;
                }
            }
        } else {
            $scope.RemainingTasksPagination = angular.copy($scope.RemainingTasks);
            $scope.bigTotalItemsforRemainingTasks = $scope.RemainingTasks.length;
        }
    });
    $rootScope.$on("providers", function (event, args) {
        $scope.Providers = angular.copy(args);
    });
    $rootScope.$on("hospitals", function (event, args) {
        $scope.Hospitals = angular.copy(args);
    });
    $rootScope.$on("Insurance", function (event, args) {
        $scope.InsuranceCompanies = angular.copy(args);
    });
    $rootScope.$on("Users", function (event, args) {
        $scope.users = angular.copy(args);
    });

    //To Display the drop down div
    $scope.searchCumDropDown = function (divId) {
        $(".ProviderTypeSelectAutoList").hide();
        $("#" + divId).show();
    };
    $scope.SelectProvider = function (Provider) {
        $scope.task.ProfileID = Provider.ProfileId;
        $scope.task.ProviderName = Provider.Name;

        $(".ProviderTypeSelectAutoList").hide();
    }
    $scope.errormessage = false;


    $scope.SelectUser = function (User) {
        $scope.task.AssignedToId = User.AuthenicateUserId;
        $scope.task.AssignedTo = User.UserName;
        $(".ProviderTypeSelectAutoList").hide();
    }
    $scope.SelectHospital = function (Hospital) {
        $scope.task.HospitalID = Hospital.HospitalID;
        $scope.task.Hospital = Hospital.HospitalName;
        $(".ProviderTypeSelectAutoList").hide();
    }
    $scope.Tempfollowup = [];
    $scope.SelectFollowup = function (Followup) {
        $scope.errormessage = false;
        $scope.Tempfollowup1 = "";

        $scope.Tempfollowup.push(Followup);
        $scope.Tempfollowup1 = JSON.stringify(angular.copy($scope.Tempfollowup));
        $scope.Followups = $.grep($scope.Followups, function (element) {
            return element.Name != Followup.Name;
        });
        //$scope.task.FollowUpDetail = $scope.Followups.filter(function (Followups) { return Followups.Name == Followup.Name })[0].Type;
        //$scope.task.FollowUp = Followup.Name
        $(".ProviderTypeSelectAutoList").hide();
    }

    $scope.RemoveFollowup = function (followup) {
        $scope.Tempfollowup1 = "";
        $scope.Followups.push(followup);
        $scope.Tempfollowup = $.grep($scope.Tempfollowup, function (element) {
            return element.Name != followup.Name;
        });
        $scope.Tempfollowup1 = JSON.stringify(angular.copy($scope.Tempfollowup));

    }

    $scope.SelectSubSection = function (SubSection) {
        $scope.task.SubSectionName = SubSection.Name;
        $(".ProviderTypeSelectAutoList").hide();
    }

    $scope.SelectInsurance = function (InsuranceCompany) {
        $scope.task.InsuranceCompanyName = InsuranceCompany.CompanyName;
        $(".ProviderTypeSelectAutoList").hide();
    }

    $scope.addView = false;
    $scope.editView = false;

    $scope.showAddView = function () {
        $scope.Tempfollowup1 = "";
        $scope.editViewforTab1 = false;
        $scope.Tempfollowup = [];
        $scope.Followups = TempFollowupHelper;
        $scope.task = {};
        //$scope.detailView = false;
        $scope.addView = true;
        //$scope.editView = false;
    }

    $scope.cancelAdd = function () {
        $scope.errormessage = false;
        $scope.errormessageforAssignedto = false;
        $scope.errormessageforprovider = false;
        $scope.errormessageforsubsection = false;
        $scope.errormessageforHospitalName = false;
        $scope.errormessageforInsurancecompany = false;
        $scope.showtabdata();
        $scope.Tempfollowup = [];
        $scope.detailView = false;
        $scope.addView = false;
        $scope.editView = false;
        $scope.progressbar = true;
    }
    $scope.errormessageforprovider = false;
    /////////// Custom Validations///////////////////////////////
    $scope.validateproviderName = function (providername) {
        var providernameresult = "";
        for (var p in $scope.Providers) {
            if (providername == $scope.Providers[p].Name) {
                providernameresult = $scope.Providers[p].Name;
            }
        }
        return providernameresult;
    }
    $scope.validatesubsectionName = function (subsectionname) {
        var subsectionnameresult = "";
        for (var p in $scope.SubSections) {
            if (subsectionname == $scope.SubSections[p].Name) {
                subsectionnameresult = $scope.SubSections[p].Name;
            }
        }
        return subsectionnameresult;
    }
    $scope.validateHospitalName = function (Hospitalname) {
        var Hospitalnameresult = "invalid";
        if (Hospitalname == null || $.trim(Hospitalname) =="") {
            Hospitalnameresult = "valid";
        }
        for (var p in $scope.Hospitals) {
            if (Hospitalname == $scope.Hospitals[p].HospitalName) {
                Hospitalnameresult = $scope.Hospitals[p].HospitalName;
            }
        }
        return Hospitalnameresult;
    }
    $scope.validateInsuranceCompany = function (Insurancename) {
        var Insurancecompanyresult = "invalid";
        if (Insurancename == null || $.trim(Insurancename) == "") {
            Insurancecompanyresult = "valid";
        }
        for (var p in $scope.InsuranceCompanies) {
            if (Insurancename == $scope.InsuranceCompanies[p].CompanyName) {
                Insurancecompanyresult = $scope.InsuranceCompanies[p].CompanyName
            }
        }
        return Insurancecompanyresult;
    }
    $scope.validateAssignedTo = function (assignedto) {
        var assignedtoresult = "";
        for (var p in $scope.users) {
            if (assignedto == $scope.users[p].UserName) {
                assignedtoresult = $scope.users[p].UserName;
            }
        }
        return assignedtoresult;
    }
    $scope.errormessageforprovider = false;
    $scope.errormessageforsubsection = false;
    $scope.errormessageforHospitalName = false;
    $scope.errormessageforInsurancecompany = false;
    $scope.errormessageforAssignedto = false;
    
    /////////////////////////////////////////////////////////////
    $scope.addNewTask = function (task) {
        $scope.errormessage = false;
        $scope.errormessageforAssignedto = false;
        $scope.errormessageforprovider = false;
        $scope.errormessageforsubsection = false;
        $scope.errormessageforHospitalName = false;
        $scope.errormessageforInsurancecompany = false;
        $scope.progressbar = false;
        var validationCount = 0;
        var validationStatus;
        var url;
        var $formData;
        $formData = $('#newTaskFormDiv');
        var validatemodeoffollowup = $($formData[0]).find($("[name='ModeOfFollowUp']")).val();
        var validateprovidername = $($formData[0]).find($("[name='ProviderName']")).val();
        var validatesubsectionname = $($formData[0]).find($("[name='SubSectionName']")).val();
        var validatehospitalname = $($formData[0]).find($("[name='HospitalName']")).val();
        var validateinsurancecompanyname = $($formData[0]).find($("[name='InsuranceCompany']")).val();
        var validateassignedto = $($formData[0]).find($("[name='AssignedTo']")).val();
        var assignedtoresult = $scope.validateAssignedTo(validateassignedto);
        if (assignedtoresult == "") {
            $scope.errormessageforAssignedto = true;
            validationCount++;
        }
        var providernameresult = $scope.validateproviderName(validateprovidername);
        if (providernameresult == "") {
            $scope.errormessageforprovider = true;
            validationCount++;
        }
        subsectionresult = $scope.validatesubsectionName(validatesubsectionname);
        if (subsectionresult == "") {
            $scope.errormessageforsubsection = true;
            validationCount++;
        }
        var hospitalnameresult = $scope.validateHospitalName(validatehospitalname);
        if (hospitalnameresult == "invalid") {
            $scope.errormessageforHospitalName = true;
            validationCount++;
        }
        var insurancecompanyresult = $scope.validateInsuranceCompany(validateinsurancecompanyname)
        if (insurancecompanyresult == "invalid") {
            $scope.errormessageforInsurancecompany = true;
            validationCount++;
        }

        url = rootDir + "/TaskTracker/AddTask"
        ResetFormForValidation($formData);
        validationStatus = $formData.valid();
        if (validatemodeoffollowup != "" && validatemodeoffollowup != null && validatemodeoffollowup != "[]") {
            if (validationStatus && validationCount == 0) {
                $scope.Tempfollowup1 = "";
                //Simple POST request example (passing data) :
                $.ajax({
                    url: url,
                    type: 'POST',
                    data: new FormData($formData[0]),
                    async: false,
                    cache: false,
                    contentType: false,
                    processData: false,
                    success: function (data) {
                        data = JSON.parse(data);
                        data.ModeOfFollowUp = JSON.parse(data.ModeOfFollowUp);
                        var tabID = "";
                        var sectionID = "";
                        var Followups = "";
                        for (var i in $scope.SubSections) {
                            if ($scope.SubSections[i].Name == data.SubSectionName) {
                                tabID = $scope.SubSections[i].TabName;
                                sectionID = $scope.SubSections[i].SubSectionID;
                            }
                        }
                        for (var i in $scope.Followups) {
                            if ($scope.Followups[i].Type == data.ModeOfFollowUp) {
                                Followups = $scope.Followups[i].Name;
                            }
                        }
                        var followupdateforaddtask = $scope.ConvertDate(data.NextFollowUpDate);

                        var Hospitalforaddtask = "";
                        for (var h in $scope.Hospitals) {
                            if (data.HospitalID == $scope.Hospitals[h].HospitalID) {
                                Hospitalforaddtask = $scope.Hospitals[h].HospitalName;
                            }
                        }
                        var Insurancecompanyforaddtask = "";
                        for (var i in $scope.InsuranceCompanies) {
                            if (data.InsuaranceCompanyNameID == $scope.InsuranceCompanies[i].InsuaranceCompanyNameID) {
                                Insurancecompanyforaddtask = $scope.InsuranceCompanies[i].CompanyName
                            }
                        }
                        if (data.AssignedToId != currentcduserdata.cdUser.CDUserID) {
                            var DailyTasksforaddedtask = {
                                TaskTrackerId: data.TaskTrackerId, ProfileID: data.ProfileID,
                                ProviderName: $scope.Providers.filter(function (Providers) { return Providers.ProfileId == data.ProfileID })[0].Name, SubSectionName: data.SubSectionName,
                                //Subject: data.Subject, NextFollowUpDate: data.NextFollowUpDate = null ? "" : data.NextFollowUpDate,
                                Subject: data.Subject, NextFollowUpDate: followupdateforaddtask,
                                ModeOfFollowUp: data.ModeOfFollowUp, FollowUp: Followups,
                                InsuranceCompanyName: Insurancecompanyforaddtask,
                                AssignedToId: $scope.users.filter(function (users) { return users.CDUserID == data.AssignedToId })[0].AuthenicateUserId, AssignedTo: $scope.users.filter(function (users) { return users.CDUserID == data.AssignedToId })[0].UserName, HospitalID: data.HospitalID,
                                Hospital: Hospitalforaddtask, Notes: data.Notes, ModifiedDate: data.LastModifiedDate,
                                TabID: tabID,
                                SubSectionID: sectionID,
                                CompleteStatus: "Open"
                            };
                            $scope.DailyTasks.splice(0, 0, DailyTasksforaddedtask);
                        }
                        if (data.AssignedToId == currentcduserdata.cdUser.CDUserID) {
                            var TasksAssignedforaddedtask = {
                                TaskTrackerId: data.TaskTrackerId, ProfileID: data.ProfileID,
                                ProviderName: $scope.Providers.filter(function (Providers) { return Providers.ProfileId == data.ProfileID })[0].Name, SubSectionName: data.SubSectionName,
                                //Subject: data.Subject, NextFollowUpDate: data.NextFollowUpDate = null ? "" : data.NextFollowUpDate,
                                Subject: data.Subject, NextFollowUpDate: followupdateforaddtask,
                                ModeOfFollowUp: data.ModeOfFollowUp, FollowUp: Followups,
                                InsuranceCompanyName: Insurancecompanyforaddtask,
                                AssignedToId: $scope.users.filter(function (users) { return users.CDUserID == data.AssignedToId })[0].AuthenicateUserId, AssignedTo: $scope.users.filter(function (users) { return users.CDUserID == data.AssignedToId })[0].UserName, HospitalID: data.HospitalID,
                                Hospital: data.HospitalID == null ? "" : $scope.Hospitals.filter(function (Hospitals) { return Hospitals.HospitalID == data.HospitalID })[0].HospitalName, Notes: data.Notes, ModifiedDate: data.LastModifiedDate,
                                TabID: tabID,
                                SubSectionID: sectionID,
                                CompleteStatus: "Open"
                            };
                            $scope.TasksAssigned.splice(0, 0, TasksAssignedforaddedtask);
                            $rootScope.$broadcast("evnt", $scope.TasksAssigned);
                            //$("#numberOfNotifications").text(parseInt($("#numberOfNotifications")[0].textContent) + 1);
                        }
                        $rootScope.$broadcast("evn", $scope.DailyTasks);

                        $scope.cancelAdd();
                        //var cnd = $.connection.cnDHub;
                        //cnd.server.notificationMessage();
                        messageAlertEngine.callAlertMessage("successfullySaved", "Task successfully assingned", "success", true);
                        $scope.progressbar = true;
                        document.getElementById("newTaskFormDiv").reset();
                    },
                    error: function (e) {
                        $scope.cancelAdd();
                        messageAlertEngine.callAlertMessage("errorInitiated", "Please try after sometime !!!!", "danger", true);
                        $scope.progressbar = true;
                        document.getElementById("newTaskFormDiv").reset();
                    }
                });
            }
        }
        else {
            $scope.errormessage = true;
        }
    }

    $scope.canceleditupdate = function () {
        $scope.errormessage = false;
        $scope.errormessageforAssignedto = false;
        $scope.errormessageforprovider = false;
        $scope.errormessageforsubsection = false;
        $scope.errormessageforHospitalName = false;
        $scope.errormessageforInsurancecompany = false;
        $scope.progressbar = false;
        $scope.addView = false;
        $scope.editView = false;
        $scope.editViewforTab1 = false;
        $scope.Tempfollowup = [];
        $scope.TableView = true;
        $scope.progressbar = true;
    }

    $scope.ConvertDate = function (date) {
        var followupdateforupdatetask = "";
        if (date !== null || date !== "") {
            var date5 = date.split('T');
            var date5 = date5[0].split('-');
            followupdateforupdatetask = date5[1] + "/" + date5[2] + "/" + date5[0];
        }
        return followupdateforupdatetask;
    }

    $scope.editTask = function () {
        $scope.errormessage = false;
        $scope.errormessageforAssignedto = false;
        $scope.errormessageforprovider = false;
        $scope.errormessageforsubsection = false;
        $scope.errormessageforHospitalName = false;
        $scope.errormessageforInsurancecompany = false;
        $scope.progressbar = false;
        var validationStatus;
        var validationCount = 0;
        var url;
        var $formData;
        $formData = $('#newTaskFormDiv');
        url = rootDir + "/TaskTracker/UpdateTask"
        var validatemodeoffollowupforedit = $($formData[0]).find($("[name='ModeOfFollowUp']")).val();
        var validateprovidernameforedit = $($formData[0]).find($("[name='ProviderName']")).val();
        var validatesubsectionnameforedit = $($formData[0]).find($("[name='SubSectionName']")).val();
        var validatehospitalnameforedit = $($formData[0]).find($("[name='HospitalName']")).val();
        var validateinsurancecompanynameforedit = $($formData[0]).find($("[name='InsuranceCompany']")).val();
        var validateassignedtoforedit = $($formData[0]).find($("[name='AssignedTo']")).val();
        var assignedtoresultforedit = $scope.validateAssignedTo(validateassignedtoforedit);
        if (assignedtoresultforedit == "") {
            $scope.errormessageforAssignedto = true;
            validationCount++;
        }
        var providernameresultforedit = $scope.validateproviderName(validateprovidernameforedit);
        if (providernameresultforedit == "") {
            $scope.errormessageforprovider = true;
            validationCount++;
        }
        var subsectionresultforedit = $scope.validatesubsectionName(validatesubsectionnameforedit);
        if (subsectionresultforedit == "") {
            $scope.errormessageforsubsection = true;
            validationCount++;
        }
        var hospitalnameresultforedit = $scope.validateHospitalName(validatehospitalnameforedit);
        if (hospitalnameresultforedit == "invalid") {
            $scope.errormessageforHospitalName = true;
            validationCount++;
        }
        var insurancecompanyresultforedit = $scope.validateInsuranceCompany(validateinsurancecompanynameforedit)
        if (insurancecompanyresultforedit == "invalid") {
            $scope.errormessageforInsurancecompany = true;
            validationCount++;
        }

        ResetFormForValidation($formData);
        validationStatus = $formData.valid();
        if (validatemodeoffollowupforedit != "" && validatemodeoffollowupforedit != null && validatemodeoffollowupforedit != "[]") {
            if (validationStatus && validationCount == 0) {
                $scope.Tempfollowup1 = "";
                //Simple POST request example (passing data) :
                $.ajax({
                    url: url,
                    type: 'POST',
                    data: new FormData($formData[0]),
                    async: false,
                    cache: false,
                    contentType: false,
                    processData: false,
                    success: function (data) {
                        $('#addButton').show();
                        data = JSON.parse(data);
                        data.ModeOfFollowUp = JSON.parse(data.ModeOfFollowUp);
                        var tabID = "";
                        var sectionID = "";
                        var Followups = "";
                        $scope.Tempfollowup = data.ModeOfFollowUp;
                        for (var i in $scope.SubSections) {
                            if ($scope.SubSections[i].Name == data.SubSectionName) {
                                tabID = $scope.SubSections[i].TabName;
                                sectionID = $scope.SubSections[i].SubSectionID;
                            }
                        }
                        for (var i in $scope.Followups) {
                            if ($scope.Followups[i].Type == data.ModeOfFollowUp) {
                                Followups = $scope.Followups[i].Name;
                            }
                        }
                        var taskassignedhospital = "";
                        for (var h in $scope.Hospitals) {
                            if (data.HospitalID == $scope.Hospitals[h].HospitalID) {
                                taskassignedhospital = $scope.Hospitals[h].HospitalName;
                            }
                        }
                        var updatedinsurancecompany = "";
                        for (var p in $scope.InsuranceCompanies) {
                            if (data.InsuaranceCompanyNameID == $scope.InsuranceCompanies[p].InsuaranceCompanyNameID) { updatedinsurancecompany = $scope.InsuranceCompanies[p].CompanyName; }
                        }
                        var updateddate = $scope.ConvertDate(data.NextFollowUpDate);
                        if (data.AssignedToId == currentcduserdata.cdUser.CDUserID) {
                            $scope.TasksAssigned.splice($scope.TasksAssigned.indexOf($scope.TasksAssigned.filter(function (datas) { return datas.TaskTrackerId == data.TaskTrackerId })[0]), 1);
                            var TasksAssignedforedittask = {
                                TaskTrackerId: data.TaskTrackerId, ProfileID: data.ProfileID,
                                ProviderName: $scope.Providers.filter(function (Providers) { return Providers.ProfileId == data.ProfileID })[0].Name == 'undefined' ? "" : $scope.Providers.filter(function (Providers) { return Providers.ProfileId == data.ProfileID })[0].Name, SubSectionName: data.SubSectionName,
                                Subject: data.Subject, NextFollowUpDate: updateddate,
                                ModeOfFollowUp: data.ModeOfFollowUp, FollowUp: Followups,
                                InsuranceCompanyName: updatedinsurancecompany,
                                AssignedToId: $scope.users.filter(function (users) { return users.CDUserID == data.AssignedToId })[0].AuthenicateUserId, AssignedTo: $scope.users.filter(function (users) { return users.CDUserID == data.AssignedToId })[0].UserName, HospitalID: data.HospitalID,
                                Hospital: taskassignedhospital, Notes: data.Notes, ModifiedDate: data.LastModifiedDate,
                                TabID: tabID,
                                SubSectionID: sectionID,
                                CompleteStatus: "Open"
                            };
                            $scope.TasksAssigned.splice(0, 0, TasksAssignedforedittask);
                            $rootScope.$broadcast("evnt", $scope.TasksAssigned);
                        }
                        else {
                            //$scope.TasksAssignedPagination.splice($scope.TasksAssignedPagination.indexOf($scope.TasksAssignedPagination.filter(function (datas) { return datas.TaskTrackerId == data.TaskTrackerId })[0]), 1);
                            $scope.TasksAssigned.splice($scope.TasksAssigned.indexOf($scope.TasksAssigned.filter(function (datas) { return datas.TaskTrackerId == data.TaskTrackerId })[0]), 1);
                            $rootScope.$broadcast("evnt", $scope.TasksAssigned);
                        }
                        if (data.AssignedToId != currentcduserdata.cdUser.CDUserID) {
                            for (var i in $scope.DailyTasks)
                            {
                                if($scope.DailyTasks[i].TaskTrackerId == data.TaskTrackerId)
                                {
                                    $scope.DailyTasks.splice(i, 1);
                                }
                            }
                            //$scope.DailyTasks.splice($scope.DailyTasks.indexOf($scope.DailyTasks.filter(function (DailyTasks) { return DailyTasks.TaskTrackerId == data.TaskTrackerId })[0]), 1);
                            var DailyTasksforedittask = {
                                TaskTrackerId: data.TaskTrackerId, ProfileID: data.ProfileID,
                                ProviderName: $scope.Providers.filter(function (Providers) { return Providers.ProfileId == data.ProfileID })[0].Name == 'undefined' ? "" : $scope.Providers.filter(function (Providers) { return Providers.ProfileId == data.ProfileID })[0].Name, SubSectionName: data.SubSectionName,
                                Subject: data.Subject, NextFollowUpDate: updateddate,
                                ModeOfFollowUp: data.ModeOfFollowUp, FollowUp: Followups,
                                InsuranceCompanyName: updatedinsurancecompany,
                                AssignedToId: $scope.users.filter(function (users) { return users.CDUserID == data.AssignedToId })[0].AuthenicateUserId, AssignedTo: $scope.users.filter(function (users) { return users.CDUserID == data.AssignedToId })[0].UserName, HospitalID: data.HospitalID,
                                Hospital: taskassignedhospital, Notes: data.Notes, ModifiedDate: data.LastModifiedDate,
                                TabID: tabID,
                                SubSectionID: sectionID,
                                CompleteStatus: "Open"
                            };
                            $scope.DailyTasks.splice(0, 0, DailyTasksforedittask);
                            $rootScope.$broadcast("evn", $scope.DailyTasks);
                        }
                        $scope.canceleditupdate();
                        messageAlertEngine.callAlertMessage("successfullySaved", "Task updated successfully", "success", true);
                        $scope.progressbar = true;
                        $scope.task = {};
                        document.getElementById("newTaskFormDiv").reset();

                    },
                    error: function (e) {
                        $scope.canceleditupdate();
                        messageAlertEngine.callAlertMessage("errorInitiated", "Please try after sometime !!!!", "danger", true);
                        $scope.progressbar = true;
                        document.getElementById("newTaskFormDiv").reset();

                    }
                });
            }
        }
        else {
            $scope.errormessage = true;
        }
    }

    $scope.detailView = false;

    $scope.$watch("task.Hospital", function (newV, oldV) {
        if (newV === oldV) {
            return;
        }
        else {
            if (newV == "") {
                $scope.task.HospitalID = null;
            }
        }
    });

    $scope.showtabdata = function () {
        $('#addButton').show();
        $scope.errormessage = false;
        $scope.errormessageforAssignedto = false;
        $scope.errormessageforprovider = false;
        $scope.errormessageforsubsection = false;
        $scope.errormessageforHospitalName = false;
        $scope.errormessageforInsurancecompany = false;
        $scope.progressbar = false;
        $scope.detailView = false;
        $scope.addView = false;
        $scope.editView = false;
        $scope.detailViewfortab1 = false;
        $scope.editViewforTab1 = false;
        $scope.detailViewfortab2 = false;
        $scope.detailViewfortab3 = false;
        $scope.progressbar = true;
        $scope.TableView = true;

    }

    $scope.showDetailView = function (task) {
        $scope.taskView = task;
        $scope.detailView = true;
    }

    $scope.cancelView = function () {
        $scope.detailView = false;
        $scope.addView = false;
        $scope.editView = false;
    }
    $scope.TemporaryTask = {};
    //To initiate Removal Confirmation Modal
    $scope.inactiveWarning = function (task) {
        if (angular.isObject(task)) {

            $scope.TemporaryTask = {};
            $scope.TemporaryTask = angular.copy(task);
        }
        $('#inactiveWarningModal').modal();
    };


    $scope.removeTask = function () {
        $scope.progressbar = false;
        $http.post(rootDir + '/TaskTracker/Inactivetask?taskTrackerID=' + $scope.TemporaryTask.TaskTrackerId).
           success(function (data, status, headers, config) {
               if (data == "true") {
                   messageAlertEngine.callAlertMessage("successfullySaved", "Task Closed successfully", "success", true);
               }
               for (var j in $scope.TasksAssigned) {
                   if ($scope.TasksAssigned[j].TaskTrackerId == $scope.TemporaryTask.TaskTrackerId) {
                       $scope.TasksAssigned[j].CompleteStatus = "Closed";
                   }
               }
               for (var i in $scope.DailyTasks) {
                   if ($scope.DailyTasks[i].TaskTrackerId == $scope.TemporaryTask.TaskTrackerId) {
                       $scope.DailyTasks[i].CompleteStatus = "Closed";
                   }
               }
               $rootScope.$broadcast("evnt", $scope.TasksAssigned);
               $rootScope.$broadcast("evn", $scope.DailyTasks);
               //$scope.DailyTasks.splice($scope.DailyTasks.indexOf($scope.DailyTasks.filter(function (DailyTasks) { return DailyTasks.TaskTrackerId == $scope.TemporaryTask.TaskTrackerId })[0]), 1);
           }).
           error(function (data, status, headers, config) {
               messageAlertEngine.callAlertMessage("errorInitiated", "Please try after sometime !!!!", "danger", true);
           });
        $scope.progressbar = true;
    }

    $scope.showDetailViewforTab1 = function (task) {
        $scope.TableView = false;
        $scope.detailViewfortab1 = true;
        $scope.taskView = task;
    }
    $scope.cancelViewforTab1 = function () {
        $scope.detailViewfortab1 = false;
        $scope.TableView = true;
    }

    $scope.showEditViewforTab1 = function (task) {
        $('#addButton').hide();
        $scope.errormessage = false;
        $scope.errormessageforAssignedto = false;
        $scope.errormessageforprovider = false;
        $scope.errormessageforsubsection = false;
        $scope.errormessageforHospitalName = false;
        $scope.errormessageforInsurancecompany = false;
        $scope.Followups = angular.copy(TempFollowupHelper);
        $scope.Tempfollowup = [];
        $scope.Tempfollowup1 = "";
        if (task.ModeOfFollowUp != "" && task.ModeOfFollowUp != null) {
            $scope.Tempfollowup = angular.copy(task.ModeOfFollowUp);
            $scope.Tempfollowup1 = JSON.stringify(angular.copy(task.ModeOfFollowUp));
            for (var i in $scope.Tempfollowup) {
                for (var j in $scope.Followups)
                    if ($scope.Tempfollowup[i].Name == $scope.Followups[j].Name) {
                        $scope.Followups.splice(j, 1);
                    }
            }
        }
        else {
            $scope.Followups = angular.copy($scope.FollowupHelper);
            $scope.Tempfollowup = [];
        }
        $scope.TableView = false;
        $scope.editViewforTab1 = true;
        $scope.task = angular.copy(task);
        currentTask = angular.copy(task);
    }
    $scope.cancelEditViewforTab1 = function () {
        $('#addButton').show();
        $scope.errormessage = false;
        $scope.errormessageforAssignedto = false;
        $scope.errormessageforprovider = false;
        $scope.errormessageforsubsection = false;
        $scope.errormessageforHospitalName = false;
        $scope.errormessageforInsurancecompany = false;
        $scope.editViewforTab1 = false;
        $scope.progressbar = false;
        $scope.Tempfollowup = [];
        $scope.task = angular.copy(currentTask);
        for (var i in $scope.DailyTasks) {
            if (currentTask.TaskTrackerId == $scope.DailyTasks[i].TaskTrackerId) {
                $scope.DailyTasks[i] = angular.copy(currentTask);
            }
        }
        $scope.Followup = angular.copy($scope.FollowupHelper);
        $scope.addView = false;
        $scope.TableView = true;
        $scope.progressbar = true;
    }

    $scope.showDetailViewforTab3 = function (task) {
        $scope.TableView = false;
        $scope.detailViewfortab3 = true;
        $scope.taskView = task;
    }
    $scope.cancelViewforTab3 = function () {
        $scope.detailViewfortab3 = false;
        $scope.TableView = true;
    }

    $scope.showDetailViewforTab2 = function (task) {
        $scope.TableView = false;
        $scope.detailViewfortab2 = true;
        $scope.taskView = task;
    }
    $scope.cancelViewforTab2 = function () {
        $scope.detailViewfortab2 = false;
        $scope.TableView = true;
    }

    $scope.TableView = true;


    //-------------------------- angular bootstrap pagger with custom-----------------
    $scope.CurrentPage = [];
    $scope.maxSize = 5;
    $scope.bigTotalItems = $scope.DailyTasks.length;
    $scope.bigCurrentPage = 1;

    //-------------------- page change action ---------------------
    $scope.pageChanged = function (pagnumber) {
        $scope.bigCurrentPage = pagnumber;
    };

    //-------------- current page change Scope Watch ---------------------
    $scope.$watch('bigCurrentPage', function (newValue, oldValue) {
        $scope.DailyTasksPagination = [];
        $scope.CurrentPage = [];
        var startIndex = (newValue - 1) * 10;
        var endIndex = startIndex + 9;
        if ($scope.DailyTasks) {
            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.DailyTasks[startIndex]) {
                    $scope.CurrentPage.push($scope.DailyTasks[startIndex]);
                    $scope.DailyTasksPagination.push($scope.DailyTasks[startIndex]);
                } else {
                    break;
                }
            }
        }
    });
    //-------------------------- angular bootstrap pagger with custom-----------------
    $scope.CurrentPageforTasksAssigned = [];
    //$scope.maxSize = 5;
    $scope.bigTotalItemsforTasksAssigned = $scope.TasksAssigned.length;
    $scope.bigCurrentPageforTasksAssigned = 1;

    //-------------------- page change action ---------------------
    $scope.pageChangedforTasksAssigned = function (pagnumber) {
        $scope.bigCurrentPageforTasksAssigned = pagnumber;
    };

    //-------------- current page change Scope Watch ---------------------
    $scope.$watch('bigCurrentPageforTasksAssigned', function (newValue, oldValue) {
        $scope.TasksAssignedPagination = [];
        $scope.CurrentPageforTasksAssigned = [];
        var startIndexforTasksAssigned = (newValue - 1) * 10;
        var endIndexforTasksAssigned = startIndexforTasksAssigned + 9;
        if ($scope.TasksAssigned) {
            for (startIndexforTasksAssigned; startIndexforTasksAssigned <= endIndexforTasksAssigned ; startIndexforTasksAssigned++) {
                if ($scope.TasksAssigned[startIndexforTasksAssigned]) {
                    $scope.CurrentPageforTasksAssigned.push($scope.TasksAssigned[startIndexforTasksAssigned]);
                    $scope.TasksAssignedPagination.push($scope.TasksAssigned[startIndexforTasksAssigned]);
                } else {
                    break;
                }
            }
        }
    });
    //-------------------------- angular bootstrap pagger with custom-----------------
    $scope.CurrentPageforRemainingTasks = [];
    //$scope.maxSize = 5;
    $scope.bigTotalItemsforRemainingTasks = $scope.RemainingTasks.length;
    $scope.bigCurrentPageforRemainingTasks = 1;

    //-------------------- page change action ---------------------
    $scope.pageChangedforRemainingTasks = function (pagnumber) {
        $scope.bigCurrentPageforRemainingTasks = pagnumber;
    };

    //-------------- current page change Scope Watch ---------------------
    $scope.$watch('bigCurrentPageforRemainingTasks', function (newValue, oldValue) {
        $scope.RemainingTasksPagination = [];
        $scope.CurrentPageforRemainingTasks = [];
        var startIndexforRemainingTasks = (newValue - 1) * 10;
        var endIndexforRemainingTasks = startIndexforRemainingTasks + 9;
        if ($scope.RemainingTasks) {
            for (startIndexforRemainingTasks; startIndexforRemainingTasks <= endIndexforRemainingTasks ; startIndexforRemainingTasks++) {
                if ($scope.RemainingTasks[startIndexforRemainingTasks]) {
                    $scope.CurrentPageforRemainingTasks.push($scope.RemainingTasks[startIndexforRemainingTasks]);
                    $scope.RemainingTasksPagination.push($scope.RemainingTasks[startIndexforRemainingTasks]);
                } else {
                    break;
                }
            }
        }
    });

    $scope.hideDropDown = function () {
        $(".ProviderTypeSelectAutoList").hide();
    }


})

function ResetFormForValidation(form) {
    form.removeData('validator');
    form.removeData('unobtrusiveValidation');
    $.validator.setDefaults({ ignore: null });
    $.validator.unobtrusive.parse(form);
}



$(document).ready(function () {
    $(".ProviderTypeSelectAutoList").hide();
    $("body").tooltip({ selector: '[data-toggle=tooltip]' });
});

$(document).click(function (event) {
    if (!$(event.target).hasClass("form-control") && $(event.target).parents(".ProviderTypeSelectAutoList").length === 0) {
        $(".ProviderTypeSelectAutoList").hide();
    }
});