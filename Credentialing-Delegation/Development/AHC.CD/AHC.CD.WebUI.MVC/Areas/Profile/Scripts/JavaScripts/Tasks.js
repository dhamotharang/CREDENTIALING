//manideep
profileApp.factory('Resource', ['$q', '$rootScope', '$filter', '$timeout', function ($q, $rootScope, $filter, $timeout) {

    function getPage(start, number, params) {

        var deferred = $q.defer();

        $rootScope.filtered = params.search.predicateObject ? $filter('filter')($rootScope.trackerItems1, params.search.predicateObject) : $rootScope.trackerItems1;
        for (var i in names) {
            sortData = ((typeof params.search.predicateObject != "undefined") && (params.search.predicateObject.hasOwnProperty(names[i]))) ? true : false;
            if (sortData == true) break;
        }
        if (params.sort.predicate) {
            $rootScope.filtered = $filter('orderBy')($rootScope.filtered, params.sort.predicate, params.sort.reverse);
        }

        var result = $rootScope.filtered.slice(start, start + number);

        $timeout(function () {
            deferred.resolve({
                data: result,
                numberOfPages: Math.ceil($rootScope.filtered.length / number)
            });
        });
        return deferred.promise;
    }

    return {
        getPage: getPage
    };

}]);

profileApp.value("names", []);
profileApp.value("sortData", "");
names = ['Subject', 'ProviderName', 'SubSectionName', 'NextFollowUpDate', 'AssignedTo', 'ModifiedDate', 'UpdatedBy', 'CompleteStatus', 'PlanName', 'ModeOfFollowUp'];


//profileApp.config(function ($datepickerProvider) {
//    var nowDate = new Date();
//    var today = new Date(nowDate.getFullYear(), nowDate.getMonth(), nowDate.getDate(), 0, 0, 0, 0);
//    angular.extend($datepickerProvider.defaults, {
//        //startDate: 'today',
//        minDate: today,
//        autoclose: true,
//        useNative: true
//    });
//})

$(document).ready(function () {
    $("#sidemenu").addClass("menu-in");
    $("#page-wrapper").addClass("menuup");
});
profileApp.value("tabStatus", [{ tabName: "ProviderTask", Displayed: [], daysleft: true, Assigned: false, subSection: true, PlanName: true, followup: true, followUpDate: true, updatedOn: false, updatedby: false, editBtn: true, removeBtn: true, historyBtn: true, TabStatus: true, DailyTableStatus: {}, predicate: {}, reset: { sort: {}, search: {}, pagination: { start: 0 } } },
                                { tabName: "HistoryTask", Displayed: [], daysleft: false, Assigned: false, subSection: false, PlanName: false, followup: false, followUpDate: false, updatedOn: true, updatedby: true, editBtn: false, removeBtn: false, historyBtn: false, TabStatus: true, DailyTableStatus: {}, predicate: {}, reset: { sort: {}, search: {}, pagination: { start: 0 } } },
                                { tabName: "ClosedTask", Displayed: [], daysleft: false, Assigned: true, Assignedby: false, subSection: true, PlanName: true, followup: true, followUpDate: false, updatedOn: false, updatedby: false, editBtn: true, removeBtn: true, historyBtn: true, TabStatus: true, DailyTableStatus: {}, predicate: {}, reset: { sort: {}, search: {}, pagination: { start: 0 } } }
])

profileApp.filter("ResetTabStatus", function (tabStatus) {
    return function (status) {
        return tabStatus.filter(function (data) { return data.tabName == status })[0];
    }
});
profileApp.filter("TimeDiff", function () {
    return function (followupDate) {
        var followup = moment(moment(followupDate).format('MM/DD/YYYY'));
        var currentDate = moment(moment(new Date()).format('MM/DD/YYYY'));
        var duration = moment.duration(followup.diff(currentDate));
        var days = duration.asDays();
        return Math.floor(days);
    }
});
var TempFollowupHelper = [];
profileApp.controller('ProviderTasksController', ['$scope', '$http', '$q', '$rootScope', '$filter', 'Resource', 'messageAlertEngine', function ($scope, $http, $q, $rootScope, $filter, Resource, messageAlertEngine) {
    $scope.Tasks = [];
    $rootScope.TasksLoaded = false;
    $scope.TableView = true;
    var ctrl = this;
    this.displayed = [];
    $scope.history = false;
    $scope.users = [];
    $rootScope.tabNames = 'ProviderTasks';

    $http.get(rootDir + "/MasterDataNew/GetAllNotesTemplates").success(function (value) {
        try {

            for (var i = 0; i < value.length ; i++) {
                if (value[i] != null) {
                    value[i].LastModifiedDate = ($scope.ConvertDateFormat(value[i].LastModifiedDate)).toString();
                    value[i].CreatedDate = ($scope.ConvertDateFormat(value[i].CreatedDate)).toString();

                }

            }

            $scope.NotesTemplates = angular.copy(value);
        } catch (e) {

        }
    });
    $scope.searchCumDropDown = function (divId) {
        $(".ProviderTypeSelectAutoList").hide();
        $("#" + divId).show();
    };
    $scope.addIntoNotesTextBox = function (Desc, div) {
        $scope.task.Notes = Desc;
        $(".ProviderTypeSelectAutoList").hide();
        $scope.NotesTemplateDropdownflag = false;
        $scope.Disablewatch = true;
    }
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
            defer.resolve(data);
        }).
        error(function (data, status, headers, config) {
            defer.reject();
        });
        return defer.promise;
    }
    //function InsuranceCompaniesData() {
    //    var defer = $q.defer();
    //    $http.get(rootDir + '/TaskTracker/GetAllInsuranceCompanies').
    //    success(function (data, status, headers, config) {
    //        $scope.InsuranceCompanies = data;

    //        defer.resolve(data);
    //    }).
    //    error(function (data, status, headers, config) {
    //        defer.reject();
    //    });
    //    return defer.promise;
    //}

    function PlansData() {
        var defer = $q.defer();
        $http.get(rootDir + '/MasterDataNew/GetAllPlanNames').
        success(function (data, status, headers, config) {
            $scope.PlanNames = data;

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
    function SubSectionData() {
        var defer = $q.defer();
        $http.get(rootDir + "/MasterDataNew/GetAllProfileSubSections").
            success(function (data, status, headers, config) {
                $scope.SubSections = data;
                defer.resolve(data);
            }).
            error(function (data, status, headers, config) {
                defer.reject();
            });
        return defer.promise;
    }
    $scope.currentProfileId = profileId;
    $rootScope.TasksLoaded = true;
    $scope.dataLoaded = false;

    $scope.tabName = function (name) {
        $rootScope.$broadcast(name);
    }
    $scope.tableDailyValue1 = {};
    $scope.tableDailyValue2 = {};

    $rootScope.$on('ProviderTasks', function () {
        $scope.tabStatus = $filter('ResetTabStatus')('ProviderTask');
        $scope.addView = false;
        $scope.detailViewfortab = false;
        $scope.showeditbtn = true;
        $scope.TabName = 'Provider Tasks';
        $rootScope.tabNames = 'ProviderTasks';
        $("a[id='tabs1']").parent("li").tab('show')
        $rootScope.trackerItems1 = angular.copy($scope.Tasks);
        //$scope.t = { sort: {}, search: {}, pagination: { start: 0 } }
        for (var i in names) {
            if ($scope.tableDailyValue1.hasOwnProperty('search')) {
                if ($scope.tableDailyValue1.search.predicateObject.hasOwnProperty(names[i])) {
                    $scope.t.search.predicateObject = $scope.tableDailyValue1.search.predicateObject;
                }
            }
        }
        $scope.t.sort.predicate = "NextFollowUpDate";
        $scope.t.sort.reverse = false;
        ctrl.callServer($scope.t);
        $scope.TableView = true;
        $scope.taskhistorytab = false;
    });

    $rootScope.$on('Historytab', function () {
        $scope.tabStatus = $filter('ResetTabStatus')('HistoryTask');
        $scope.addView = false;
        $scope.detailViewfortab = false;
        $scope.showeditbtn = false;
        $scope.TabName = 'History Tasks';
        $rootScope.tabNames = 'HistoryTasks';
        $rootScope.trackerItems1 = $scope.TaskHistory;
        //ctrl.callServer();
        ctrl.temp = $scope.TaskHistory;
        $scope.t.pagination.start = 0;
        ctrl.callServer($scope.t);

        //$rootScope.$broadcast('History');
        $scope.TableView = false;
        $scope.taskhistorytab = true;

        $scope.history = true;
        $('.nav-tabs a:last').tab('show');
    })
    $scope.TabName = 'Provider Tasks';
    $rootScope.$on("Tasks", function () {
        if (!$scope.dataLoaded) {
            $rootScope.TasksLoaded = false;
            $http.get(rootDir + '/TaskTracker/GetAllTasksByProfileId?profileid=' + profileId).
                success(function (data, status, headers, config) {
                    $scope.Tasks = angular.copy(data);
                    for (var i in $scope.Tasks) {
                        $scope.Tasks[i].daysleft = $filter("TimeDiff")($scope.Tasks[i].NextFollowUpDate);
                    }
                    $scope.tabStatus = $filter('ResetTabStatus')('ProviderTask');
                }).
            error(function (data, status, headers, config) {
                $rootScope.TasksLoaded = true;
            });
            $scope.LoadData();
            $scope.dataLoaded = true;
        }

    });
    $rootScope.$on('ClosedTasks', function () {
        $scope.addView = false;
        $scope.detailViewfortab = false;
        $scope.showeditbtn = false;
        $scope.TabName = 'Closed Tasks';
        $scope.tabStatus = $filter('ResetTabStatus')('ClosedTask');
        $rootScope.tabNames = 'ClosedTasks';
        $rootScope.trackerItems1 = angular.copy($scope.ClosedTasks);
        $rootScope.AllTasksTracker = angular.copy($rootScope.trackerItems1);
        $scope.t.pagination.start = 0;
        $scope.t.search.predicateObject = {};
        for (var i in names) {
            if ($scope.tableDailyValue2.hasOwnProperty('search')) {
                if ($scope.tableDailyValue2.search.predicateObject.hasOwnProperty(names[i])) {
                    $scope.t.search.predicateObject = $scope.tableDailyValue2.search.predicateObject;
                }
            }
        }
        ctrl.callServer($scope.t);
        $scope.TableView = true;
        $scope.taskhistorytab = false;
    });
    $rootScope.$on('History', function () {
        $scope.cancelEditViewforTab();
        $rootScope.trackerItems = $scope.TaskHistory;
        $scope.t.pagination.start = 0;
        $scope.t.pagination.numberOfPages = 0;
        $scope.t.search.predicateObject = {};
        ctrl.callServer($scope.t);
        ctrl.temp = $scope.TaskHistory;
    });
    $scope.LoadData = function () {
        $scope.progressbar = false;
        $q.all([$q.when(CDUsersData()),
            $q.when(HospitalsData()),
            $q.when(ProvidersData()),
            $q.when(PlansData()),
            //$q.when(InsuranceCompaniesData()),
            $q.when(LoginUsersData()),
            $q.when(SubSectionData())
        ]).then(function (response) {
            $scope.Followups = [{ Name: "Phone Call", Type: "PhoneCall" }, { Name: "Email", Type: "Email" },
                                { Name: "Online Form Completion", Type: "OnlineFormCompletion" }, { Name: "Paper Form Completion", Type: "PaperFormCompletion" }];
            $scope.FollowupHelper = angular.copy($scope.Followups);
            TempFollowupHelper = angular.copy($scope.Followups);
            for (var i = 0; i < $scope.LoginUsers.length; i++) {
                for (var j = 0; j < $scope.CDUsers.length; j++) {
                    if ($scope.LoginUsers[i].Id == cduserdata.cdUser.AuthenicateUserId) {
                        $scope.ProviderNameforNotes = $scope.LoginUsers[i].UserName;
                    }
                    if (($scope.LoginUsers[i].Id == $scope.CDUsers[j].AuthenicateUserId) && ($scope.CDUsers[j].Status == "Active")) {
                        $scope.users.push({ CDUserID: $scope.CDUsers[j].CDUserID, AuthenicateUserId: $scope.CDUsers[j].AuthenicateUserId, FullName: $scope.LoginUsers[i].FullName, Email: $scope.LoginUsers[i].Email, UserName: $scope.LoginUsers[i].UserName })
                    }
                }
            }

            for (var i in $scope.Tasks) {
                if ($scope.Tasks[i].ModeOfFollowUp != "") {
                    $scope.Tasks[i].ModeOfFollowUp = JSON.parse($scope.Tasks[i].ModeOfFollowUp);
                    //$scope.Tasks[i].Status = ($scope.Tasks[i].Status == "OPEN") ? "OPEN" : "CLOSED";
                }
                if ($scope.Tasks[i].PlanName != null) {
                    $scope.Tasks[i].ViewPlanName = $scope.Tasks[i].PlanName.PlanName;
                }
                for (var j in $scope.users) {
                    if ($scope.Tasks[i].AssignedTo.AuthenicateUserId == $scope.users[j].AuthenicateUserId)
                        $scope.Tasks[i].AssignedToName = $scope.users[j].UserName;
                }

                var converteddate = $scope.ConvertDate($scope.Tasks[i].NextFollowUpDate);
                $scope.Tasks[i].NextFollowUpDate = converteddate;
            }
            //for (var i = 0; i < $scope.Tasks.length; i++) {
            //    for (var j = 0; j < $scope.InsuranceCompanies.length; j++) {
            //        if ($scope.Tasks[i].InsuaranceCompanyNameID == $scope.InsuranceCompanies[j].InsuaranceCompanyNameID) {
            //            $scope.Tasks[i].InsuranceCompanyName = $scope.InsuranceCompanies[j].CompanyName;
            //        }
            //    }
            //}

            for (var i = 0; i < $scope.Tasks.length; i++) {
                for (var j = 0; j < $scope.PlanNames.length; j++) {
                    if ($scope.Tasks[i].PlanID == $scope.PlanNames[j].PlanID) {
                        $scope.Tasks[i].PlanID = $scope.PlanNames[j].PlanID;
                    }
                }
            }

            $scope.ClosedTasks = $.grep($scope.Tasks, function (element) { return element.Status == "CLOSED"; });
            $scope.Tasks = $.grep($scope.Tasks, function (element) { return (element.Status == "OPEN" || element.Status == "REOPEN"); });
            $rootScope.trackerItems1 = angular.copy($scope.Tasks);
            $scope.t = { sort: {}, search: {}, pagination: { start: 0 } }
            ctrl.callServer($scope.t);
            $rootScope.TasksLoaded = true;
        })
    };
    //$scope.cancelEditViewforTab = function () {
    //    $scope.editViewforTab1 = false;
    //    $scope.editViewforTab3 = false;
    //    $scope.editViewforTab2 = false;
    //}

    $scope.cancelEditViewforTasks = function () {
        $('#addButton').show();
        $scope.errormessage = false;
        $scope.errormessageforAssignedto = false;
        $scope.errormessageforprovider = false;
        $scope.errormessageforsubsection = false;
        $scope.forHospitalName = false;
        $scope.errormessageforInsurancecompany = false;
        $scope.taskViewforTab1 = false;
        $scope.editViewforTab3 = false;
        $scope.editViewforTab2 = false;
        $scope.VisibilityControl = "";
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
    this.callServer = function callServer(tableState) {
        ctrl.isLoading = true;
        var pagination = tableState.pagination;
        var start = pagination.start || 0;
        var number = pagination.number || 10;
        $scope.t = tableState;
        Resource.getPage(start, number, tableState).then(function (result) {
            ctrl.displayed = result.data;
            ctrl.temp = ctrl.displayed;
            if ($rootScope.tabNames == 'ProviderTasks' && sortData == true) {
                $scope.tableDailyValue1 = angular.copy(tableState);
            }
            if ($rootScope.tabNames == 'ProviderTasks' && sortData == false) {
                if ($scope.tableDailyValue1.hasOwnProperty('search')) {
                    $scope.tableDailyValue1.search.predicateObject = {};
                }
            }
            if ($rootScope.tabNames == 'ClosedTasks' && sortData == true) {
                $scope.tableDailyValue2 = angular.copy(tableState);
            }
            if ($rootScope.tabNames == 'ClosedTasks' && sortData == false) {
                if ($scope.tableDailyValue2.hasOwnProperty('search')) {
                    $scope.tableDailyValue2.search.predicateObject = {};
                }
            }
            tableState.pagination.numberOfPages = result.numberOfPages;
            ctrl.isLoading = false;
        });
    };
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
    //$scope.searchCumDropDown = function (divId) {
    //    $(".ProviderTypeSelectAutoList").hide();
    //    $("#" + divId).show();
    //};
    //$scope.SelectInsurance = function (InsuranceCompany) {
    //    //$scope.task.InsuaranceCompanyNameID = InsuranceCompany.InsuaranceCompanyNameID;
    //    //$scope.task.InsuranceCompanyName = angular.copy(InsuranceCompany);
    //    $scope.task.InsuranceCompanyName = InsuranceCompany.CompanyName;
    //    $(".ProviderTypeSelectAutoList").hide();
    //}

    $scope.SelectPlan = function (PlanName) {
        //$scope.task.InsuaranceCompanyNameID = InsuranceCompany.InsuaranceCompanyNameID;
        //$scope.task.InsuranceCompanyName = angular.copy(InsuranceCompany);
        $scope.task.PlanID = PlanName.PlanID;
        $scope.task.PlanName = PlanName.PlanName;
        $(".ProviderTypeSelectAutoList").hide();
    }

    $scope.SelectHospital = function (Hospital) {
        $scope.task.HospitalID = Hospital.HospitalID;
        $scope.task.Hospital = Hospital.HospitalName;
        $(".ProviderTypeSelectAutoList").hide();
    }
    $scope.SelectSubSection = function (SubSection) {
        $scope.task.SubSectionName = SubSection.SubSectionName;
        $(".ProviderTypeSelectAutoList").hide();
    }
    $scope.showeditView = function (task, editviewfortab) {
        $scope.detailViewfortab = false;
        $('#addButton').hide();
        $scope.visible = 'edit';
        $scope.errormessage = false;
        $scope.errormessageforAssignedto = false;
        $scope.errormessageforprovider = false;
        $scope.errormessageforsubsection = false;
        $scope.errormessageforHospitalName = false;
        $scope.errormessageforInsurancecompany = false;
        $scope.Followups = angular.copy(TempFollowupHelper);
        $scope.Tempfollowup = [];
        $scope.Tempfollowup1 = "";
        $scope.temp = [];
        $scope.temp1 = [];

        var temp = [];
        $scope.TempNotes = '';
        var tempforscroll = '';
        var NoteBy = '';
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
        if (task.Notes != null) {
            if (task.Notes.indexOf('~') != -1) {
                var dateTimeForNote = task.Notes.split('~');
                dateTimeForNote = dateTimeForNote[0] + " " + $scope.changeTimeTo24Hr(dateTimeForNote[1]);
            }
        }

        if (task.hasOwnProperty('TaskTrackerHistories') && (task.TaskTrackerHistories != null) && (task.TaskTrackerHistories.length != 0)) {
            if (task.TaskTrackerHistories[task.TaskTrackerHistories.length - 1].Notes != null) {
                var dateTimeForNote1 = task.TaskTrackerHistories[task.TaskTrackerHistories.length - 1].Notes.split('~');
                dateTimeForNote1 = dateTimeForNote1[0] + " " + $scope.changeTimeTo24Hr(dateTimeForNote1[1]);
            }
        }
        var authID = $scope.Tasks.filter(function (filterTask) { return filterTask.TaskTrackerId == task.TaskTrackerId })[0].AssignedById;
        for (var i in $scope.users) {
            if ($scope.users[i].CDUserID == authID) {
                NoteBy = $scope.users[i].UserName;
            }
            if ($scope.users[i].CDUserID == task.AssignedToId) {
                task.AssignedToId = $scope.users[i].AuthenicateUserId;
            }
        }

        if ((task.TaskTrackerHistories == null) || (task.TaskTrackerHistories.length == 0)) {
            if (task.Notes != null) {
                if (task.Notes.indexOf('~') != -1) {
                    var notes = task.Notes.split('~');
                    notes = notes[3];
                }
            }
            $scope.temp.push({ stamp: notes, number: 0, modifiedDate: moment(dateTimeForNote).format('MM/DD/YYYY hh:mm a'), By: task.LastUpdatedBy });
        }
        else {
            for (var t = 0; t < task.TaskTrackerHistories.length - 1; t++) {
                if (task.TaskTrackerHistories[t].Notes != null) {
                    if (task.TaskTrackerHistories[t].Notes.indexOf('~') != -1) {
                        var notes = task.TaskTrackerHistories[t].Notes.split('~');
                        notes = notes[3];
                    }
                }
                $scope.temp.push({ stamp: notes, number: t, modifiedDate: moment($scope.ConvertDateForNotes(task.TaskTrackerHistories[t].LastModifiedDate)).format('MM/DD/YYYY hh:mm a'), By: task.TaskTrackerHistories[t].LastUpdatedBy });
            }
            if (task.TaskTrackerHistories[task.TaskTrackerHistories.length - 1].Notes != null) {
                if (task.TaskTrackerHistories[task.TaskTrackerHistories.length - 1].Notes.indexOf('~') != -1) {
                    var notes = task.TaskTrackerHistories[task.TaskTrackerHistories.length - 1].Notes.split('~');
                    notes = notes[3];
                }
            }
            $scope.temp.push({ stamp: notes, number: t, modifiedDate: moment(dateTimeForNote1).format('MM/DD/YYYY hh:mm a'), By: task.TaskTrackerHistories[task.TaskTrackerHistories.length - 1].LastUpdatedBy });
            if (task.Notes != null) {
                if (task.Notes.indexOf('~') != -1) {
                    var notes = task.Notes.split('~');
                    notes = notes[3];
                }
            }
            $scope.temp.push({ stamp: notes, number: 0, modifiedDate: moment(dateTimeForNote).format('MM/DD/YYYY hh:mm a'), By: task.LastUpdatedBy });
        }
        $scope.temp = $scope.temp.reverse();
        for (i = 0; i < $scope.temp.length; i++) {

            if ($scope.temp[i].stamp == undefined || $scope.temp[i].stamp == " ") {

            }
            else {
                $scope.temp1.push({ stamp: $scope.temp[i].stamp, number: $scope.temp[i].number, modifiedDate: $scope.temp[i].modifiedDate, By: $scope.temp[i].By });
            }
        }


        for (var i = 0; i < $scope.users.length; i++) {
            if ($scope.users[i].CDUserID == task.AssignedToId) {
                task.AssignedToName = $scope.users[i].UserName;
            }
        }
        for (var i = 0; i < $scope.Hospitals.length; i++) {
            if (task.HospitalID == $scope.Hospitals[i].HospitalID) {
                task.Hospital = $scope.Hospitals[i].HospitalName;
            }
        }

        //for (var i = 0; i < $scope.InsuranceCompanies.length; i++) {
        //    if (task.InsuaranceCompanyNameID == $scope.InsuranceCompanies[i].InsuaranceCompanyNameID) {
        //        task.InsuranceCompanyName = $scope.InsuranceCompanies[i].CompanyName;
        //    }
        //}

        for (var i = 0; i < $scope.PlanNames.length; i++) {
            if (task.PlanID == $scope.PlanNames[i].PlanID) {
                task.PlanName = null;
                task.PlanName = $scope.PlanNames[i].PlanName;
            }
        }

        for (var p = 0; p < $scope.Providers.length; p++) {
            if (task.ProfileID == $scope.Providers[p].ProfileId) {
                task.Provider = $scope.Providers[p].Name;
            }
        }

        //$scope.task.Notes = $scope.ProviderNameforNotes + " - " + (date.getMonth() + 1) + "/" + date.getDate() + "/" + date.getFullYear() + " - " + AMPM;
        var rand = '';
        var date = Date();
        var Time = date.split(" ")[4];
        var AMPM = $scope.changeTimeAmPm(Time);
        date = new Date();
        $scope.TempNotes = (date.getMonth() + 1) + "/" + date.getDate() + "/" + date.getFullYear() + "~" + AMPM + "~" + $scope.ProviderNameforNotes + "~";
        $scope.TableView = false;
        $scope.VisibilityControl = editviewfortab;
        $scope.task = angular.copy(task);
        $scope.task.Notes = '';
        currentTask = angular.copy(task);
    }
    $scope.initPop = function () {
        $('[data-toggle="popover"]').popover();
    };
    $scope.changeTimeTo24Hr = function (value) {
        if (value == 'Not Available' || value == 'Invalid Date' || value == 'Day Off') { return 'Day Off'; }
        if (!value) { return ''; }
        if (angular.isDate(value)) {
            value = value.getHours() + ":" + value.getMinutes();
        }

        var time = value.split(":");
        var hours = time[0];
        var minutes = time[1];
        var status = (minutes.indexOf('AM') != -1) ? 'am' : 'pm';

        if (status == 'pm') {
            if (hours == 12) { hours = 0; }
            else { hours = 12 + parseInt(hours); }
            minutes = (minutes.indexOf('AM') != -1) ? minutes.split('AM')[0] : minutes.split('PM')[0];
        }
        var hours = hours.toString();
        var minutes = minutes.toString();
        if (hours < 10) hours = "0" + hours;
        if (minutes < 10) minutes = "0" + minutes;

        //minutes = minutes.length == 1 ? minutes < 10 ? '0' + minutes : minutes : minutes;

        //minutes = minutes < 9 ? '00' : minutes;
        var strTime = hours + ':' + minutes;
        return strTime;
    }
    $scope.SelectUser = function (User) {
        $scope.task.AssignedToId = User.AuthenicateUserId;
        $scope.task.AssignedToName = User.UserName;
        $(".ProviderTypeSelectAutoList").hide();
    }
    $scope.AddSelectUser = function (User) {
        $scope.task.AssignedToId = User.AuthenicateUserId;
        $scope.task.AssignedTo = User.UserName;
        $(".ProviderTypeSelectAutoList").hide();
    }
    $scope.QuickRemove = function (task) {
        if (angular.isObject(task)) {
            $scope.TemporaryTask = {};
            $scope.TemporaryTask = angular.copy(task);
        }
        $('#inactiveWarningModal1').modal();
    }


    $scope.removeTask = function () {
        $scope.progressbar = false;
        $http.post(rootDir + '/TaskTracker/Inactivetask?taskTrackerID=' + $scope.TemporaryTask.TaskTrackerId).
   success(function (data, status, headers, config) {
       if (data == "true") {
           messageAlertEngine.callAlertMessage("successfullySaved", "Task closed successfully", "success", true);
       }
       //var closedtask = $.grep($scope.Tasks, function (element) { return element.TaskTrackerId == $scope.TemporaryTask.TaskTrackerId; });
       $.grep($scope.Tasks, function (element) { return element.TaskTrackerId == $scope.TemporaryTask.TaskTrackerId; })[0].Status = "CLOSED";
       $scope.ClosedTasks.push($.grep($scope.Tasks, function (element) { return element.TaskTrackerId == $scope.TemporaryTask.TaskTrackerId; })[0]);
       $scope.Tasks.splice($scope.Tasks.indexOf($scope.Tasks.filter(function (tasks) { return tasks.TaskTrackerId == $scope.TemporaryTask.TaskTrackerId })[0]), 1);
       if ($rootScope.tabNames == 'ProviderTasks') {
           $rootScope.$broadcast('ProviderTasks');
       }
       else if ($rootScope.tabNames == 'ClosedTasks') {
           $rootScope.$broadcast('ClosedTasks');
       }
       //for (var j in $scope.Tasks) {
       //    if ($scope.Tasks[j].TaskTrackerId == $scope.TemporaryTask.TaskTrackerId) {
       //        $scope.Tasks[j].Status = "CLOSED";
       //    }
       //}
       //var updateddate = $scope.ConvertDate(data.NextFollowUpDate);

       //for (var j in $scope.Tasks) {
       //    if ($scope.Tasks[j].TaskTrackerId == data.TaskTrackerId) {
       //        $scope.Tasks[j].Subject = data.Subject;
       //        $scope.Tasks[j].SubSectionName = data.SubSectionName;
       //        $scope.Tasks[j].NextFollowUpDate = updateddate;
       //        $scope.Tasks[j].Status = data.Status;
       //        $scope.Tasks[j].InsuranceCompanyName.CompanyName = updatedinsurancecompany;
       //        $scope.Tasks[j].ModeOfFollowUp = data.ModeOfFollowUp;
       //        $scope.Tasks[j].AssignedToName = assignedtoName;
       //    }

       //}

       //$rootScope.trackerItems1 = angular.copy($scope.Tasks);

       //ctrl.temp = $scope.Tasks;
       //$scope.t.pagination.start = 0;
       //ctrl.callServer($scope.t);

   }).
   error(function (data, status, headers, config) {
       messageAlertEngine.callAlertMessage("errorInitiated", "Please try after sometime !!!!", "danger", true);
   });
        $scope.progressbar = true;
    }
    $scope.Quickreopen = function (task) {
        if (angular.isObject(task)) {
            $scope.TemporaryTask = {};
            $scope.TemporaryTask = angular.copy(task);
        }
        $('#reactiveWarningModal2').modal();
    }
    $scope.reopenTask = function () {
        $scope.progressbar = false;
        var opened = [];
        $http.post(rootDir + '/TaskTracker/Reactivetask?taskTrackerID=' + $scope.TemporaryTask.TaskTrackerId).
           success(function (data, status, headers, config) {
               if (data == "true") {
                   messageAlertEngine.callAlertMessage("successfullySaved", "Task reopened successfully", "success", true);
               }
               $.grep($scope.ClosedTasks, function (element) { return element.TaskTrackerId == $scope.TemporaryTask.TaskTrackerId; })[0].Status = "OPEN";
               $scope.Tasks.push($.grep($scope.ClosedTasks, function (element) { return element.TaskTrackerId == $scope.TemporaryTask.TaskTrackerId; })[0]);
               $scope.ClosedTasks.splice($scope.ClosedTasks.indexOf($scope.ClosedTasks.filter(function (tasks) { return tasks.TaskTrackerId == $scope.TemporaryTask.TaskTrackerId })[0]), 1);

               //for (var j in $scope.Tasks) {
               //    if ($scope.Tasks[j].TaskTrackerId == $scope.TemporaryTask.TaskTrackerId) {
               //        $scope.Tasks[j].Status = "Open";
               //    }
               //}
               if ($rootScope.tabNames == 'ProviderTasks') {
                   $rootScope.$broadcast('ProviderTasks');
               }
               else if ($rootScope.tabNames == 'ClosedTasks') {
                   $rootScope.$broadcast('ClosedTasks');
               }
               //$rootScope.trackerItems1 = angular.copy($scope.Tasks);

               //ctrl.temp = $scope.Tasks;
               //$scope.t.pagination.start = 0;
               //ctrl.callServer($scope.t);

           }).


           error(function (data, status, headers, config) {
               messageAlertEngine.callAlertMessage("errorInitiated", "Please try after sometime !!!!", "danger", true);
           });
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
    $scope.validatesubsectionName = function (subsectionname) {
        var subsectionnameresult = "";
        for (var p in $scope.SubSections) {
            if (subsectionname == $scope.SubSections[p].SubSectionName) {
                subsectionnameresult = $scope.SubSections[p].SubSectionName;
            }
        }
        return subsectionnameresult;
    }
    $scope.validateHospitalName = function (Hospitalname) {
        var Hospitalnameresult = "invalid";
        if (Hospitalname == null || $.trim(Hospitalname) == "") {
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
        $scope.VisibilityControl = "";
        $scope.editViewforTab1 = false;
        $scope.editViewforTab2 = false;
        $scope.editViewforTab3 = false;
        $scope.Tempfollowup = [];
        $scope.TableView = true;
        $scope.progressbar = true;
    }

    $scope.$watch('task.Hospital', function (newValue, oldValue) {
        if (newValue != undefined && newValue.length == 0) {
            $scope.task.HospitalID = null;
        }
    });

    $scope.editTask = function () {
        var d1 = new $.Deferred();
        $scope.errormessage = false;
        $scope.progressbar = false;
        var validationStatus;
        var validationCount = 0;
        var url;
        var $formData;
        $formData = $('#newTaskFormDiv');
        url = rootDir + "/TaskTracker/UpdateTask"
        var validatemodeoffollowupforedit = $($formData[0]).find($("[name='ModeOfFollowUp']")).val();
        var validatesubsectionnameforedit = $($formData[0]).find($("[name='SubSectionName']")).val();
        var validatehospitalnameforedit = $($formData[0]).find($("[name='HospitalName']")).val();
        var validateinsurancecompanynameforedit = $($formData[0]).find($("[name='InsuranceCompany']")).val();
        var validateassignedtoforedit = $($formData[0]).find($("[name='AssignedTo']")).val();
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
                        data.daysleft = $filter("TimeDiff")($scope.task.NextFollowUpDate);
                        data.ModeOfFollowUp = JSON.parse(data.ModeOfFollowUp);
                        var tabID = "";
                        var sectionID = "";
                        var Followups = "";
                        $scope.Tempfollowup = data.ModeOfFollowUp;
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

                        var updatedPlanName = "";
                        for (var p in $scope.PlanNames) {
                            if (data.PlanID == $scope.PlanNames[p].PlanID) { updatedPlanName = $scope.PlanNames[p].PlanName; }
                        }

                        var Providername = "";
                        for (var i in $scope.Providers) {
                            if (data.ProfileID == $scope.Providers[i].ProfileId) {
                                Providername = $scope.Providers[i].Name;
                            }
                        }
                        var assignedtoName = "";
                        for (var i in $scope.users) {
                            if (data.AssignedToId == $scope.users[i].CDUserID) { assignedtoName = $scope.users[i].UserName }
                        }
                        var updateddate = $scope.ConvertDate(data.NextFollowUpDate);
                        var newStatus = data.Status;
                        //== "Active") ? "Open" : "Closed";
                        for (var j in $scope.Tasks) {
                            if ($scope.Tasks[j].TaskTrackerId == data.TaskTrackerId) {
                                $scope.Tasks[j].Subject = data.Subject;
                                $scope.Tasks[j].SubSectionName = data.SubSectionName;
                                $scope.Tasks[j].NextFollowUpDate = updateddate;
                                $scope.Tasks[j].daysleft = data.daysleft;
                                $scope.Tasks[j].Status = newStatus;
                                $scope.Tasks[j].AssignedToId = data.AssignedToId;
                                $scope.Tasks[j].AssignedById = data.AssignedById;
                                $scope.Tasks[j].Hospital = taskassignedhospital;
                                $scope.Tasks[j].HospitalID = data.HospitalID;
                                $scope.Tasks[j].InsuaranceCompanyNameID = data.InsuaranceCompanyNameID;
                                $scope.Tasks[j].InsuranceCompanyName = updatedinsurancecompany;
                                $scope.Tasks[j].PlanID = data.PlanID;
                                $scope.Tasks[j].ViewPlanName = updatedPlanName;
                                $scope.Tasks[j].ModeOfFollowUp = data.ModeOfFollowUp;
                                $scope.Tasks[j].AssignedToName = assignedtoName;
                                $scope.Tasks[j].Notes = data.Notes;
                                $scope.Tasks[j].LastUpdatedBy = data.LastUpdatedBy;
                                $scope.Tasks[j].TaskTrackerHistories = data.TaskTrackerHistories;
                            }
                        }

                        $.ajax({
                            url: rootDir + '/Dashboard/GetTaskExpiryCounts?cdUserID=' + localStorage.getItem("UserID"),
                            //data: {
                            //    format:  'json'
                            //},
                            error: function () {
                                //$('#info').html('<p>An error has occurred</p>');
                            },
                            dataType: 'json',
                            success: function (data) {

                                localStorage.setItem("expired_Task", data.Result.ExpiredCount);
                                localStorage.setItem("expiring_Task", data.Result.ExpiringTodayCount);
                                localStorage.setItem("DataStatus", "Updated");
                                $("#expired_Task").html("");
                                $("#expiring_Task").html("");
                                $("#expired_Task").html(localStorage.getItem("expired_Task"));
                                $("#expiring_Task").html(localStorage.getItem("expiring_Task"));
                            },
                        })
                        $rootScope.trackerItems1 = angular.copy($scope.Tasks);
                        ctrl.temp = $scope.Tasks;
                        $scope.canceleditupdate();
                        messageAlertEngine.callAlertMessage("successfullySaved", "Task updated successfully", "success", true);
                        $scope.progressbar = true;
                        $scope.task = {};
                        document.getElementById("newTaskFormDiv").reset();
                        d1.resolve(data);
                    },
                    error: function (e) {
                        $scope.canceleditupdate();
                        messageAlertEngine.callAlertMessage("errorInitiated", "Please try after sometime !!!!", "danger", true);
                        $scope.progressbar = true;
                        document.getElementById("newTaskFormDiv").reset();

                    }
                });
            }
            return d1.promise();

            // $formData.reset();
        }

        else {
            $scope.errormessage = true;
        }
    }

    $scope.TasksData = function () {
        $http.get(rootDir + '/TaskTracker/GetAllUsers').
           success(function (data, status, headers, config) {
               $scope.LoginUsers = data;
           }).
           error(function (data, status, headers, config) {
           });
    }
    $scope.showDetailViewforTab1 = function (task) {
        $scope.TableView = false;
        $scope.history = false;
        $scope.detailViewfortab = true;

        $scope.viewTemp = [];
        $scope.viewTempLatest = [];
        $scope.viewTemp1 = [];
        $scope.selectedTask = task;

        if ($rootScope.tabNames == 'HistoryTasks') {
            var dateTimeForNote = [];
            for (var h = 0; h < task.Notes.length; h++) {
                if (task.Notes[h] != null) {
                    if (task.Notes[h].indexOf('~') != -1) {
                        var dateTimeForNote1 = task.Notes[h].split('~');
                        dateTimeForNote.push(dateTimeForNote1[0] + " " + $scope.changeTimeTo24Hr(dateTimeForNote1[1]));
                    }
                }
            }
        }
        else {
            if (task.Notes != null) {
                if (task.Notes.indexOf('~') != -1) {
                    var dateTimeForNote = task.Notes.split('~');
                    //dateTimeForNote = dateTimeForNote[0] + " " + ((dateTimeForNote.indexOf('AM') != -1) ? dateTimeForNote[1].split('AM')[0] : dateTimeForNote[1].split('PM')[0]);
                    dateTimeForNote = dateTimeForNote[0] + " " + $scope.changeTimeTo24Hr(dateTimeForNote[1]);
                }
            }
        }
        $scope.checkhistory = false;
        if (!task.hasOwnProperty('TaskTrackerHistories')) {
            $scope.checkhistory = true;
        }
        if ($rootScope.tabNames == 'ProviderTasks') {
            var authID = $scope.Tasks.filter(function (filtertask) { return filtertask.TaskTrackerId == task.TaskTrackerId })[0].AssignedById;
            $scope.checkhistory = false;
        }
        else if ($rootScope.tabNames == 'ClosedTasks') {
            var authID = $scope.ClosedTasks.filter(function (filtertask) { return filtertask.TaskTrackerId == task.TaskTrackerId })[0].AssignedById;
            $scope.checkhistory = false;
        }

        for (var i = 0; i < $scope.users.length; i++) {
            if ($scope.users[i].CDUserID == authID) {
                NoteBy = $scope.users[i].UserName;
            }
        }
        if ($rootScope.tabNames == 'HistoryTasks') {
            for (var h = 0; h < task.Notes.length; h++) {
                if (task.Notes[h] != null) {
                    if (task.Notes[h].indexOf('~') != -1) {
                        var notes = task.Notes[h].split('~');
                        var notesDesc = notes[3];
                        var NotesBy = notes[2];


                    }
                }
                //$scope.viewTemp.push({ stamp: task.Notes[h], number: 0, modifiedDate: moment(dateTimeForNote[h]).format('MM/DD/YYYY hh:mm a'), By: task.LastUpdatedBy });
                $scope.viewTemp.push({ stamp: notesDesc, number: 0, modifiedDate: moment(dateTimeForNote[h]).format('MM/DD/YYYY hh:mm a'), By: NotesBy });

            }
        } else {
            if ((!task.hasOwnProperty('TaskTrackerHistories')) || (task.TaskTrackerHistories == null) || (task.TaskTrackerHistories.length == 0)) {
                if (task.Notes != null) {
                    if (task.Notes.indexOf('~') != -1) {
                        var notes = task.Notes.split('~');
                        notes = notes[3];
                    }
                }
                //$scope.viewTemp.push({ stamp: task.Notes, number: 0, modifiedDate: dateTimeForNote, By: NoteBy });
                $scope.viewTemp.push({ stamp: notes, number: 0, modifiedDate: moment(dateTimeForNote).format('MM/DD/YYYY hh:mm a'), By: task.LastUpdatedBy });
            }
            else {
                for (var t = 0; t < task.TaskTrackerHistories.length; t++) {
                    if (task.TaskTrackerHistories[t].Notes != null) {
                        if (task.TaskTrackerHistories[t].Notes.indexOf('~') != -1) {
                            var notes = task.TaskTrackerHistories[t].Notes.split('~');
                            var dateTimeForHistory = notes[0] + " " + $scope.changeTimeTo24Hr(notes[1]);
                            notes = notes[3];
                        }
                    }
                    //$scope.viewTemp.push({ stamp: task.TaskTrackerHistories[t].Notes, number: t, modifiedDate: $scope.ConvertDateForNotes(task.TaskTrackerHistories[t].LastModifiedDate), By: task.TaskTrackerHistories[t].LastUpdatedBy });
                    //$scope.viewTemp.push({ stamp: notes, number: t, modifiedDate: moment($scope.ConvertDateForNotes(task.TaskTrackerHistories[t].LastModifiedDate)).format('MM/DD/YYYY hh:mm a'), By: task.TaskTrackerHistories[t].LastUpdatedBy });
                    $scope.viewTemp.push({ stamp: notes, number: t, modifiedDate: moment(dateTimeForHistory).format('MM/DD/YYYY hh:mm a'), By: task.TaskTrackerHistories[t].LastUpdatedBy });
                }
                if (task.Notes != null) {
                    if (task.Notes.indexOf('~') != -1) {
                        var notes = task.Notes.split('~');
                        notes = notes[3];
                    }
                }
                //$scope.viewTemp.push({ stamp: task.Notes, number: 0, modifiedDate: dateTimeForNote, By: NoteBy });
                $scope.viewTemp.push({ stamp: notes, number: 0, modifiedDate: moment(dateTimeForNote).format('MM/DD/YYYY hh:mm a'), By: task.LastUpdatedBy });
            }
        }
        $scope.viewTemp = $scope.viewTemp.reverse();
        for (i = 0; i < $scope.viewTemp.length; i++) {

            if ($scope.viewTemp[i].stamp == undefined || $scope.viewTemp[i].stamp == " ") {

            }
            else {
                $scope.viewTemp1.push({ stamp: $scope.viewTemp[i].stamp, number: $scope.viewTemp[i].number, modifiedDate: $scope.viewTemp[i].modifiedDate, By: $scope.viewTemp[i].By });
            }
        }

        for (var i = 0; i < $scope.Hospitals.length; i++) {
            if (task.HospitalID == $scope.Hospitals[i].HospitalID) {
                task.Hospital = $scope.Hospitals[i].HospitalName;
            }
        }
        $scope.taskView = angular.copy(task);
        if ($scope.taskView.PlanName == undefined) {
            $scope.taskViewPlanName = angular.copy(task.ViewPlanName);
        }
        else {
            $scope.taskViewPlanName = angular.copy($scope.taskView.PlanName.PlanName);
        }
    }
    $scope.cancelViewforTab1 = function () {
        $scope.detailViewfortab = false;
        $scope.TableView = true;
        if ($scope.checkhistory != true) {
            if ($rootScope.tabNames == 'ProviderTasks') {
                $("a[id='tabs1']").parent("li").tab('show')
                $scope.tabName('ProviderTasks');
            }
            else if ($rootScope.tabNames == 'ClosedTasks') {
                $("a[id='tabs2']").parent("li").tab('show')
                $scope.tabName('ClosedTasks');
            }
        } else {
            $scope.tabName('Historytab');
            $scope.history = true;
        }
        //$rootScope.trackerItems1 = angular.copy($scope.Tasks);
        //ctrl.temp = $rootScope.trackerItems1;
    }
    $scope.ConvertDateForNotes = function (date) {
        var followupdateforupdatetask = "";
        if (date !== null || date !== "") {
            var date5 = date.split('T');
            var time = date5[1];
            var date5 = date5[0].split('-');
            time = time.split(':');
            followupdateforupdatetask = date5[1] + "/" + date5[2] + "/" + date5[0] + " " + time[0] + ":" + time[1];
        }
        return followupdateforupdatetask;
    }
    $scope.showAddView = function () {
        $scope.VisibilityControl = "";
        $scope.Tempfollowup1 = "";
        $scope.Tempfollowup = [];
        $scope.Followups = TempFollowupHelper;
        $scope.task = {};
        $scope.visible = 'add';
        var date = Date();
        var Time = date.split(" ")[4];
        var AMPM = $scope.changeTimeAmPm(Time);
        date = new Date();
        $scope.TempNotes = (date.getMonth() + 1) + "/" + date.getDate() + "/" + date.getFullYear() + "~" + AMPM + "~" + $scope.ProviderNameforNotes + "~";
        //$scope.detailView = false;
        $scope.addView = true;
        $scope.TableView = false;
        //$scope.editView = false;
        if ($scope.Tasks != null && $scope.Tasks.length != 0) {
            for (var p = 0; p < $scope.Providers.length; p++) {
                if ($scope.Tasks[0].ProfileID == $scope.Providers[p].ProfileId) {
                    $scope.task.Provider = $scope.Providers[p].Name;
                    $scope.task.ProfileID = $scope.Providers[p].ProfileId;
                }
            }
        }
    }
    $scope.changeTimeAmPm = function (value) {
        if (value == 'Not Available' || value == 'Invalid Date' || value == 'Day Off') { return 'Day Off'; }
        if (!value) { return ''; }
        if (angular.isDate(value)) {
            value = value.getHours() + ":" + value.getMinutes();
        }

        var time = value.split(":");
        var hours = time[0];
        var minutes = time[1];
        var ampm = hours >= 12 ? 'PM' : 'AM';
        hours = hours % 12;
        hours = hours ? hours : 12; // the hour '0' should be '12'

        minutes = minutes.length == 1 ? minutes < 10 ? '0' + minutes : minutes : minutes;

        //minutes = minutes < 9 ? '00' : minutes;
        var strTime = hours + ':' + minutes + ' ' + ampm;
        return strTime;
    }
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
            if (subsectionname == $scope.SubSections[p].SubSectionName) {
                subsectionnameresult = $scope.SubSections[p].SubSectionName;
            }
        }
        return subsectionnameresult;
    }
    $scope.validateHospitalName = function (Hospitalname) {
        var Hospitalnameresult = "invalid";
        if (Hospitalname == null || $.trim(Hospitalname) == "") {
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
    $scope.cancelAdd = function () {
        $scope.errormessage = false;
        $scope.errormessageforAssignedto = false;
        $scope.errormessageforprovider = false;
        $scope.errormessageforsubsection = false;
        $scope.errormessageforHospitalName = false;
        $scope.errormessageforInsurancecompany = false;
        $scope.Tempfollowup = [];
        $scope.detailView = false;
        $scope.TableView = true;
        $scope.addView = false;
        $scope.editView = false;
        $scope.progressbar = true;
        //$rootScope.trackerItems = $scope.TasksAssigned;
        $rootScope.$broadcast('ProviderTasks');
        //ctrl.callServer();
        //ctrl.temp = $rootScope.trackerItems;
    }
    $scope.addNewTask = function (task) {
        //ctrl.callServer($scope.t);
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
        //var validateprovidername = $($formData[0]).find($("[name='ProviderName']")).val();
        var validatesubsectionname = $($formData[0]).find($("[name='SubSectionName']")).val();
        var validatehospitalname = $($formData[0]).find($("[name='HospitalName']")).val();
        var validateinsurancecompanyname = $($formData[0]).find($("[name='InsuranceCompany']")).val();
        var validateassignedto = $($formData[0]).find($("[name='AssignedTo']")).val();
        var assignedtoresult = $scope.validateAssignedTo(validateassignedto);
        if (assignedtoresult == "") {
            $scope.errormessageforAssignedto = true;
            validationCount++;
        }
        //var providernameresult = $scope.validateproviderName(validateprovidername);
        //if (providernameresult == "") {
        //    $scope.errormessageforprovider = true;
        //    validationCount++;
        //}
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
                        data.daysleft = $filter("TimeDiff")(data.NextFollowUpDate);
                        data.ModeOfFollowUp = JSON.parse(data.ModeOfFollowUp);
                        //data.InsuaranceCompanyNames = JSON.parse(data.InsuaranceCompanyNames);
                        var tabID = "";
                        var sectionID = "";
                        var Followups = "";
                        for (var i in $scope.SubSections) {
                            if ($scope.SubSections[i].SubSectionName == data.SubSectionName) {
                                //tabID = $scope.SubSections[i].TabName;
                                //sectionID = $scope.SubSections[i].SubSectionID;
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
                        var Insurancecompanyforaddtask = [];
                        if (data.InsuaranceCompanyNameID != null) {
                            for (var i in $scope.InsuranceCompanies) {
                                if (data.InsuaranceCompanyNameID == $scope.InsuranceCompanies[i].InsuaranceCompanyNameID) {
                                    Insurancecompanyforaddtask = $scope.InsuranceCompanies[i].CompanyName;
                                }
                            }
                        }
                        else {
                            Insurancecompanyforaddtask = "";
                        }

                        var PlanNameforaddtask = [];
                        if (data.PlanID != null) {
                            for (var i in $scope.PlanNames) {
                                if (data.PlanID == $scope.PlanNames[i].PlanID) {
                                    PlanNameforaddtask = $scope.PlanNames[i].PlanName;
                                }
                            }
                        }
                        else {
                            PlanNameforaddtask = "";
                        }

                        var AssignedTo = "";
                        var Assignedtoid = "";
                        for (var i in $scope.users) {
                            if ($scope.users[i].CDUserID == data.AssignedToId) {
                                AssignedTo = $scope.users[i].UserName;
                                Assignedtoid = $scope.users[i].AuthenicateUserId;
                            }
                        }

                        //var AssignedTo = $scope.users.filter(function (users) { return users.CDUserID == $scope.AllTasks[k].AssignedToId })[0].UserName;                        

                        var Tasks = {
                            TaskTrackerId: data.TaskTrackerId, ProfileID: data.ProfileID,
                            //ProviderName: $scope.Providers.filter(function (Providers) { return Providers.ProfileId == data.ProfileID })[0].Name, SubSectionName: data.SubSectionName,
                            //ProviderName: Providername,
                            SubSectionName: data.SubSectionName,
                            //Subject: data.Subject, NextFollowUpDate: data.NextFollowUpDate = null ? "" : data.NextFollowUpDate,
                            Subject: data.Subject, NextFollowUpDate: followupdateforaddtask,
                            ModeOfFollowUp: data.ModeOfFollowUp, FollowUp: Followups,
                            daysleft: data.daysleft,
                            InsuranceCompanyName: Insurancecompanyforaddtask,
                            ViewPlanName: PlanNameforaddtask,
                            PlanName: PlanNameforaddtask,
                            LastUpdatedBy: data.LastUpdatedBy,
                            //AssignedToId: $scope.users.filter(function (users) { return users.CDUserID == data.AssignedToId })[0].AuthenicateUserId, AssignedTo: $scope.users.filter(function (users) { return users.CDUserID == data.AssignedToId })[0].UserName, HospitalID: data.HospitalID,
                            AssignedToId: Assignedtoid, AssignedToName: AssignedTo, HospitalID: data.HospitalID,
                            AssignedById: data.AssignedById,
                            //Hospital: data.HospitalID == null ? "" : $scope.Hospitals.filter(function (Hospitals) { return Hospitals.HospitalID == data.HospitalID })[0].HospitalName, Notes: data.Notes, ModifiedDate: data.LastModifiedDate,
                            Hospital: Hospitalforaddtask, Notes: data.Notes, ModifiedDate: data.LastModifiedDate,
                            TabID: tabID,
                            SubSectionID: sectionID,
                            Status: "OPEN"
                        };
                        $scope.Tasks.splice(0, 0, Tasks);
                        //$rootScope.$broadcast("evnt", $scope.Tasks);
                        //$("#numberOfNotifications").text(parseInt($("#numberOfNotifications")[0].textContent) + 1);
                        $rootScope.trackerItems1 = angular.copy($scope.Tasks);
                        ctrl.temp = angular.copy($scope.Tasks);


                        $scope.cancelAdd();
                        //var cnd = $.connection.cnDHub;
                        //cnd.server.notificationMessage();
                        messageAlertEngine.callAlertMessage("successfullySaved", "Task successfully assigned", "success", true);
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
    $scope.showHistorytab = false;
    $scope.showeditbtn = true;
    $scope.showHistory = function (task) {
        $rootScope.previousTabName = $rootScope.tabNames;
        $('.nav-tabs a:last').tab('show');
        $scope.taskhistorytab = true;
        $scope.TaskHistory = [];
        $scope.history = true;
        $scope.showHistorytab = true;
        $scope.showeditbtn = false;
        //$scope.tabStatus = $filter('ResetTabStatus')('HistoryTask');
        var tabID = [];
        var sectionID = [];
        var histories = [];
        var taskstatus = "";
        if ($rootScope.tabNames == 'ProviderTasks') {
            histories = $scope.Tasks[$scope.Tasks.indexOf($scope.Tasks.filter(function (taskId) { return taskId.TaskTrackerId == task.TaskTrackerId })[0])];
            taskstatus = "OPEN";
        }
        else if ($rootScope.tabNames == 'ClosedTasks') {
            histories = $scope.ClosedTasks[$scope.ClosedTasks.indexOf($scope.ClosedTasks.filter(function (taskId) { return taskId.TaskTrackerId == task.TaskTrackerId })[0])];
            taskstatus = "CLOSED";
        }

        for (var j in histories.TaskTrackerHistories) {
            var ProviderName = [];
            if (histories.TaskTrackerHistories[j].ProviderID != null)
                for (var m in $scope.Providers) {
                    if ($scope.Providers[m].ProfileId == histories.TaskTrackerHistories[j].ProviderID) { ProviderName[j] = $scope.Providers[m].Name; }
                }

            for (var i in $scope.SubSections) {
                if (histories.TaskTrackerHistories[j].SubSectionName == $scope.SubSections[i].Name) {
                    tabID[j] = $scope.SubSections[i].TabName;
                    sectionID[j] = $scope.SubSections[i].SubSectionID;
                }
            }

            var AssignedToId = [];
            var AssignedTo = [];
            if (histories.TaskTrackerHistories[j].AssignToCCOID != null) {
                for (var u in $scope.users) {
                    if (histories.TaskTrackerHistories[j].AssignToCCOID == $scope.users[u].CDUserID) {
                        AssignedTo[j] = $scope.users[u].UserName
                        AssignedToId[j] = $scope.users[u].AuthenicateUserId;
                    }
                }
            }

            var HospitalNamefortaskassigned = "";
            for (var h in $scope.Hospitals) {
                if (histories.TaskTrackerHistories[j].HospitalId == $scope.Hospitals[h].HospitalID) {
                    HospitalNamefortaskassigned = $scope.Hospitals[h].HospitalName;
                }
            }

            var TasksAssignedinsurancecompanyname = "";
            for (var p in $scope.InsuranceCompanies) {
                if (histories.TaskTrackerHistories[j].InsuaranceCompanyNameID == $scope.InsuranceCompanies[p].InsuaranceCompanyNameID) {
                    TasksAssignedinsurancecompanyname = $scope.InsuranceCompanies[p].CompanyName;
                }
            }
            var note = [];
            for (var n = 0; n <= j; n++) {
                note.push(histories.TaskTrackerHistories[n].Notes);
            }
            var TasksAssignedplanname = "";
            for (var p in $scope.PlanNames) {
                if (histories.TaskTrackerHistories[j].PlanID == $scope.PlanNames[p].PlanID) {
                    TasksAssignedplanname = $scope.PlanNames[p].PlanName;
                }
            }

            $scope.TaskHistory.push({
                TaskTrackerId: histories.TaskTrackerId, ProfileID: histories.TaskTrackerHistories.ProfileID,
                ProviderName: ProviderName[j], SubSectionName: histories.TaskTrackerHistories[j].SubSectionName,
                Subject: histories.TaskTrackerHistories[j].Subject, NextFollowUpDate: $scope.ConvertDate(histories.TaskTrackerHistories[j].NextFollowUpDate),
                ModeOfFollowUp: JSON.parse(histories.TaskTrackerHistories[j].ModeOfFollowUp),
                InsuranceCompanyName: TasksAssignedinsurancecompanyname,
                ViewPlanName: TasksAssignedplanname,
                LastUpdatedBy: histories.TaskTrackerHistories[j].LastUpdatedBy,
                //AssignedToId: $scope.users.filter(function (users) { return users.CDUserID == $scope.TasksAssignedtoMe[k].AssignedToId })[0].AuthenicateUserId, AssignedTo: AssignedTo, HospitalID: $scope.TasksAssignedtoMe[k].HospitalID,
                AssignedToId: AssignedToId[j], AssignedTo: AssignedTo[j],
                Hospital: HospitalNamefortaskassigned, Notes: note, ModifiedDate: $scope.ConvertDate(histories.TaskTrackerHistories[j].LastModifiedDate),
                TabID: tabID, AssignedToName: AssignedTo[j],
                SubSectionID: sectionID,
                //Status: histories.TaskTrackerHistories[j].Status == "Active" ? "Open" : "Closed"
                Status: taskstatus
            });
        }

        $rootScope.tabNames = 'HistoryTasks';
        $rootScope.trackerItems1 = ($scope.TaskHistory).reverse();
        //ctrl.callServer();
        ctrl.temp = $scope.TaskHistory;
        $scope.t.pagination.start = 0;
        ctrl.callServer($scope.t);
        //$rootScope.tabNames = 'history';
        //$rootScope.$broadcast('History');
        $scope.TableView = false;
        //$("#" + loadingId).css('display', 'none');
    }
    $scope.closeHistory = function () {
        $('.nav-tabs li').removeClass('active')
        $scope.TableView = true;
        $scope.history = false;
        $scope.showHistorytab = false;
        if ($rootScope.previousTabName == 'ProviderTasks') {
            $("a[id='tabs1']").parent("li").tab('show')
            //$scope.tabName('ProviderTasks');
            $rootScope.$broadcast('ProviderTasks');
        }
        else if ($rootScope.previousTabName == 'ClosedTasks') {
            $("a[id='tabs2']").parent("li").tab('show');
            //$scope.tabName('ClosedTasks');
            $rootScope.$broadcast('ClosedTasks');
        }
        //$rootScope.trackerItems1 = angular.copy($scope.Tasks);
        //ctrl.temp = $rootScope.trackerItems1;
    }

    $scope.CloseTask = function () {
        var promise = $scope.editTask().then(function () {
            $scope.removeTask();
        });
    }

    $scope.cancelForRemoveTask = function () {
        $('#addButton').show();
        $scope.errormessage = false;
        $scope.errormessageforAssignedto = false;
        $scope.errormessageforprovider = false;
        $scope.errormessageforsubsection = false;
        $scope.errormessageforHospitalName = false;
        $scope.errormessageforInsurancecompany = false;
        $scope.editViewforTab1 = false;
        $scope.VisibilityControl = "";
        $scope.addView = false;
        $scope.TableView = true;

    }
    $scope.inactiveWarning = function (task) {
        if (angular.isObject(task)) {
            $scope.TemporaryTask = {};
            $scope.TemporaryTask = angular.copy(task);
        }
        $('#inactiveWarningModal').modal();
    }

}])

