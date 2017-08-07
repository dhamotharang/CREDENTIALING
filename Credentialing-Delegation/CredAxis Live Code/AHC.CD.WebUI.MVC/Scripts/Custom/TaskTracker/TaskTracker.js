//Module declaration
var trackerApp = angular.module('TrackerApp', ['ui.bootstrap', 'smart-table', 'mgcrea.ngStrap', 'loadingInteceptor', 'toaster']);

//$(document).ready(function () {
//    $("#sidemenu").addClass("menu-in");
//    $("#page-wrapper").addClass("menuup");
//});

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
trackerApp.run(function ($rootScope) {
    $rootScope.trackerItems = [];
    //location.hash = '';

})
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

trackerApp.directive('elastic', [
    '$timeout',
    function ($timeout) {
        return {
            restrict: 'A',
            link: function ($scope, element) {
                $scope.initialHeight = $scope.initialHeight || element[0].style.height;
                var resize = function () {
                    element[0].style.height = $scope.initialHeight;
                    element[0].style.height = "" + element[0].scrollHeight + "px";
                };
                element.on("input change", resize);
                $timeout(resize, 0);
            }
        };
    }
]);

trackerApp.directive('stRatio', function () {
    return {
        link: function (scope, element, attr) {
            var ratio = +(attr.stRatio);

            element.css('width', ratio + '%');

        }
    };
});

trackerApp.value("sortData", "");
trackerApp.value("names", []);
names = ['Subject', 'ProviderName', 'SubSectionName', 'NextFollowUpDate', 'AssignedTo', 'ModifiedDate', 'LastUpdatedBy', 'CompleteStatus', 'PlanName', 'ModeOfFollowUp'];
trackerApp.factory('Resource', ['$q', '$rootScope', '$filter', '$timeout', function ($q, $rootScope, $filter, $timeout) {

    function getPage(start, number, params) {

        var deferred = $q.defer();

        //if ($rootScope.tabNames == 'DailyTask') {
        //    $rootScope.filtered = params.search.predicateObject ? $filter('filter')($rootScope.DailyTasksTracker, params.search.predicateObject) : $rootScope.DailyTasksTracker;
        //    //$rootScope.a.sort.predicate = "NextFollowUpDate";
        //    //$rootScope.a.sort.reverse = true;
        //}
        //if ($rootScope.tabNames == 'TasksAssigne') {
        //    $rootScope.filtered = params.search.predicateObject ? $filter('filter')($rootScope.TasksAssignedTracker, params.search.predicateObject) : $rootScope.TasksAssignedTracker;
        //}
        //if ($rootScope.tabNames == 'AllTask') {
        //    $rootScope.filtered = params.search.predicateObject ? $filter('filter')($rootScope.AllTasksTracker, params.search.predicateObject) : $rootScope.AllTasksTracker;
        //}
        //if ($rootScope.tabNames == 'CloseTasks') {
        //    $rootScope.filtered = params.search.predicateObject ? $filter('filter')($rootScope.trackerItems, params.search.predicateObject) : $rootScope.trackerItems;
        //}
        //if ($rootScope.tabNames == 'HistoryTask') {
        //    $rootScope.filtered = params.search.predicateObject ? $filter('filter')($rootScope.trackerItems, params.search.predicateObject) : $rootScope.trackerItems;
        //}
        //else {
        //    $rootScope.filtered = params.search.predicateObject ? $filter('filter')($rootScope.trackerItems, params.search.predicateObject) : $rootScope.trackerItems;
        //}
        $rootScope.filtered = params.search.predicateObject ? $filter('filter')($rootScope.trackerItems, params.search.predicateObject) : $rootScope.trackerItems;
        //names = ['Subject', 'ProviderName', 'SubSectionName', 'NextFollowUpDate', 'AssignedTo', 'ModifiedDate', 'UpdatedBy', 'CompleteStatus', 'InsuranceCompanyName', 'ModeOfFollowUp']
        for (var i in names) {

            sortData = ((typeof params.search.predicateObject != "undefined") && (params.search.predicateObject.hasOwnProperty(names[i]))) ? true : false;
            if (sortData == true) break;
        }

        if (params.sort.predicate) {
            $rootScope.filtered = $filter('orderBy')($rootScope.filtered, params.sort.predicate, params.sort.reverse);
        }

        var result = $rootScope.filtered.slice(start, start + number);
        //console.log($rootScope.filtered.length);
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


trackerApp.directive('stPersist', function ($rootScope) {
    return {
        require: '^stTable',
        link: function (scope, element, attr, ctrl) {
            if ($rootScope.tabNames == 'DailyTask') {
                var nameSpace1 = attr.stPersist;

                //save the table state every time it changes
                scope.$watch(function () {
                    return ctrl.tableState();
                }, function (newValue, oldValue) {
                    if (newValue !== oldValue) {
                        localStorage.setItem(nameSpace1, JSON.stringify(newValue));
                    }
                }, true);

                //fetch the table state when the directive is loaded
                if (localStorage.getItem(nameSpace1)) {
                    var savedState = JSON.parse(localStorage.getItem(nameSpace1));
                    var tableState = ctrl.tableState();

                    angular.extend(tableState, savedState);
                    ctrl.pipe();

                }

            }
            if ($rootScope.tabNames == 'TasksAssigne') {
                var nameSpace2 = attr.stPersist;

                //save the table state every time it changes
                scope.$watch(function () {
                    return ctrl.tableState();
                }, function (newValue, oldValue) {
                    if (newValue !== oldValue) {
                        localStorage.setItem(nameSpace2, JSON.stringify(newValue));
                    }
                }, true);

                //fetch the table state when the directive is loaded
                if (localStorage.getItem(nameSpace2)) {
                    var savedState = JSON.parse(localStorage.getItem(nameSpace2));
                    var tableState = ctrl.tableState();

                    angular.extend(tableState, savedState);
                    ctrl.pipe();

                }

            }
            if ($rootScope.tabNames == 'AllTask') {
                var nameSpace3 = attr.stPersist;

                //save the table state every time it changes
                scope.$watch(function () {
                    return ctrl.tableState();
                }, function (newValue, oldValue) {
                    if (newValue !== oldValue) {
                        localStorage.setItem(nameSpace3, JSON.stringify(newValue));
                    }
                }, true);

                //fetch the table state when the directive is loaded
                if (localStorage.getItem(nameSpace3)) {
                    var savedState = JSON.parse(localStorage.getItem(nameSpace3));
                    var tableState = ctrl.tableState();

                    angular.extend(tableState, savedState);
                    ctrl.pipe();

                }

            }

            if ($rootScope.tabNames == 'CloseTasks') {
                var nameSpace4 = attr.stPersist;

                //save the table state every time it changes
                scope.$watch(function () {
                    return ctrl.tableState();
                }, function (newValue, oldValue) {
                    if (newValue !== oldValue) {
                        localStorage.setItem(nameSpace4, JSON.stringify(newValue));
                    }
                }, true);

                //fetch the table state when the directive is loaded
                if (localStorage.getItem(nameSpace4)) {
                    var savedState = JSON.parse(localStorage.getItem(nameSpace4));
                    var tableState = ctrl.tableState();

                    angular.extend(tableState, savedState);
                    ctrl.pipe();

                }

            }
            if ($rootScope.tabNames == 'AdminTasks') {
                var nameSpace5 = attr.stPersist;

                //save the table state every time it changes
                scope.$watch(function () {
                    return ctrl.tableState();
                }, function (newValue, oldValue) {
                    if (newValue !== oldValue) {
                        localStorage.setItem(nameSpace5, JSON.stringify(newValue));
                    }
                }, true);

                //fetch the table state when the directive is loaded
                if (localStorage.getItem(nameSpace5)) {
                    var savedState = JSON.parse(localStorage.getItem(nameSpace5));
                    var tableState = ctrl.tableState();

                    angular.extend(tableState, savedState);
                    ctrl.pipe();

                }

            }


        }
    };
});


trackerApp.value("tabStatus", [{ tabName: "DailyTask", Displayed: [], daysleft: true, Assigned: false, Assignedby: false, timeElapsed: false, subSection: true, PlanName: true, followup: true, followUpDate: true, updatedOn: false, updatedby: false, editBtn: true, removeBtn: true, historyBtn: true, TabStatus: true, DailyTableStatus: {}, predicate: {}, reset: { sort: {}, search: {}, pagination: { start: 0 } } },
{ tabName: "TaskAssigned", Displayed: [], daysleft: true, Assigned: true, Assignedby: false, timeElapsed: false, subSection: true, PlanName: true, followup: true, followUpDate: true, updatedOn: false, updatedby: false, editBtn: true, removeBtn: true, historyBtn: true, TabStatus: true, DailyTableStatus: {}, predicate: {}, reset: { sort: {}, search: {}, pagination: { start: 0 } } },
{ tabName: "AllTask", Displayed: [], daysleft: true, Assigned: true, Assignedby: false, timeElapsed: false, subSection: true, PlanName: true, followup: true, followUpDate: true, updatedOn: false, updatedby: false, editBtn: true, removeBtn: true, historyBtn: true, TabStatus: true, DailyTableStatus: {}, predicate: {}, reset: { sort: {}, search: {}, pagination: { start: 0 } } },
{ tabName: "ClosedTask", Displayed: [], daysleft: false, Assigned: true, Assignedby: false, timeElapsed: false, subSection: true, PlanName: true, followup: true, followUpDate: false, updatedOn: false, updatedby: false, editBtn: true, removeBtn: true, historyBtn: true, TabStatus: true, DailyTableStatus: {}, predicate: {}, reset: { sort: {}, search: {}, pagination: { start: 0 } } },
{ tabName: "HistoryTask", Displayed: [], daysleft: false, Assigned: true, Assignedby: false, timeElapsed: false, subSection: false, PlanName: false, followup: false, followUpDate: false, updatedOn: true, updatedby: true, editBtn: false, removeBtn: false, historyBtn: false, TabStatus: true, DailyTableStatus: {}, predicate: {}, reset: { sort: {}, search: {}, pagination: { start: 0 } } },
{ tabName: "AllTasksForAdmin", Displayed: [], daysleft: true, Assigned: true, Assignedby: true, timeElapsed: true, subSection: true, PlanName: true, followup: true, followUpDate: true, updatedOn: false, updatedby: false, editBtn: false, removeBtn: false, historyBtn: true, TabStatus: true, DailyTableStatus: {}, predicate: {}, reset: { sort: {}, search: {}, pagination: { start: 0 } } }])

trackerApp.filter("ResetTabStatus", function (tabStatus) {
    return function (status) {
        return tabStatus.filter(function (data) { return data.tabName == status })[0];
    }
})
trackerApp.filter("TimeDiff", function () {
    return function (followupDate) {
        var followup = moment(moment(followupDate).format('MM/DD/YYYY'));
        var currentDate = moment(moment(new Date()).format('MM/DD/YYYY'));
        var duration = moment.duration(followup.diff(currentDate));
        var days = duration.asDays();
        return Math.floor(days);
    }
})
var TempFollowupHelper = [];
//Controller declaration
trackerApp.controller('TrackerCtrl', function ($scope, $rootScope, $anchorScroll, $location, $http, $q, $filter, messageAlertEngine, Resource, toaster) {
    //$interval, $timeout,

    $scope.DailyTasks = [];
    $scope.Tasks = [];
    $scope.CDUsers = [];
    $scope.LoginUsers = [];
    $scope.Providers = [];
    $scope.Hospitals = [];
    $scope.InsuranceCompanies = [];
    $scope.PlanNames = [];
    $scope.users = [];
    $scope.userAuthID = -1;
    $scope.FollowupHelper = [];
    $scope.TasksAssigned = [];
    $scope.ClosedTasks = [];
    $scope.TasksAssignedtoMe = [];
    $scope.TasksAssignedByMe = [];
    $scope.AllTasks = [];
    $scope.RemainingTasks = [];
    $scope.TaskHistory = [];
    $scope.AllTasksForAdmin = [];
    $rootScope.DailyTasksTracker = [];
    $rootScope.TasksAssignedTracker = [];
    $rootScope.AllTasksTracker = [];
    $scope.tableDailyValue1 = {};
    $scope.tableDailyValue2 = {};
    $scope.tableDailyValue3 = {};
    $scope.tableDailyValue4 = {};
    $scope.tableDailyValue5 = {};
    $scope.Disablewatch = false;
    $scope.empty = " ";
    $rootScope.tabNames = 'DailyTask';
    $scope.currentDate = moment(new Date()).format('MM/DD/YYYY');
    $scope.history = false;
    $scope.TableView = true;
    $scope.showLoading = true;
    $scope.showOnClose = true;
    //$scope.InsuranceCompanyHelper = [];
    var currentcduserdata = "";
    $scope.InactiveCDUsers = [];
    $scope.InactiveProviders = [];
    var AllProviders = [];
    $http.get(rootDir + "/MasterDataNew/GetAllNotesTemplates").success(function (value) {
        try {

            for (var i = 0; i < value.length; i++) {
                if (value[i] != null) {
                    value[i].LastModifiedDate = ($scope.ConvertDateFormat(value[i].LastModifiedDate)).toString();
                    value[i].CreatedDate = ($scope.ConvertDateFormat(value[i].CreatedDate)).toString();

                }

            }

            $scope.NotesTemplates = angular.copy(value);
        } catch (e) {

        }
    });
    $scope.ConvertDateFormat = function (value) {
        var returnValue = value;
        var dateData = "";
        try {
            if (value.indexOf("/Date(") == 0) {
                returnValue = new Date(parseInt(value.replace("/Date(", "").replace(")/", ""), 10));
                var day = returnValue.getDate() < 10 ? '0' + returnValue.getDate() : returnValue.getDate();
                var tempo = returnValue.getMonth() + 1;
                var month = tempo < 10 ? '0' + tempo : tempo;
                var year = returnValue.getFullYear();
                dateData = month + "-" + day + "-" + year;
            }
            return dateData;
        } catch (e) {
            return dateData;
        }
        return dateData;
    };
    // HTTP calls to get data ///////
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
    //function InsuranceCompaniesData() {
    //    var defer = $q.defer();
    //    $http.get(rootDir + '/MasterDataNew/GetAllInsuranceCompanies').
    //    success(function (data, status, headers, config) {
    //        $scope.InsuranceCompanies = data;
    //        //$scope.InsuranceCompanyHelper = data;
    //        defer.resolve(data);
    //    }).
    //    error(function (data, status, headers, config) {
    //        defer.reject();
    //    });
    //    return defer.promise;
    //}

    function PlanNamesData() {
        var defer = $q.defer();
        $http.get(rootDir + '/MasterDataNew/GetAllPlanNames').
            success(function (data, status, headers, config) {
                $scope.PlanNames = data;
                //$scope.InsuranceCompanyHelper = data;
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
                for (var i in $scope.Tasks) {
                    $scope.Tasks[i].daysleft = $filter("TimeDiff")($scope.Tasks[i].NextFollowUpDate);

                    //// Bug fix code: Sort the notes by recent on top in task tracker. 
                    $scope.Tasks[i].TaskTrackerHistories = $scope.Tasks[i].TaskTrackerHistories.sort(function (a, b) {
                        var dateA = new Date(a.LastModifiedDate), dateB = new Date(b.LastModifiedDate)
                        return dateA - dateB //sort by date ascending
                    });
                    //for (var j in $scope.PlanNames) {
                    //    if($scope.Tasks[i].PlanID==$scope.PlanNames[j].PlanID)
                    //    {
                    //        $scope.Tasks[i].PlanName = $scope.PlanNames[j].PlanName;
                    //    }
                    //}
                }

                defer.resolve(data);
            }).
            error(function (data, status, headers, config) {
                defer.reject();
            });
        return defer.promise;
    }
    $scope.AllProvidersData = [];
    function ProvidersData() {
        var defer = $q.defer();
        $http.get(rootDir + '/TaskTracker/GetAllProviders').
            success(function (data, status, headers, config) {
                AllProviders = data;
                $scope.AllProvidersData = data;
                $scope.Providers = jQuery.grep(data, function (ele) { return ele.Status == "Active" || ele.Status == "Inactive" });
                $scope.InactiveProviders = jQuery.grep(data, function (ele) { return ele.Status == "Inactive" });
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
    /////////////////////////////////

    var ctrl = this;
    this.displayed = [];
    $scope.order = "reverse";
    //$scope.predicate = '-NextFollowUpDate';
    //$rootScope.AllData = false;


    this.callServer = function callServer(tableState) {
        ctrl.isLoading = true;

        var pagination = tableState.pagination;
        //sessionStorage.setItem('dailyValue', $scope.t.search.predicateObject);
        //console.log(pagination.number);

        var start = pagination.start || 0;
        var number = pagination.number || 10;
        $scope.t = tableState;
        //if ($rootScope.tabNames == 'DailyTask'){
        //    $scope.tableDailyValue = angular.copy(tableState);
        //    tableState = $scope.tableDailyValue || tableState;
        //}
        //if ($rootScope.tabNames == 'TasksAssigne') {
        //    $scope.tableTaskValue = angular.copy(tableState);
        //}
        //if ($rootScope.tabNames == 'AllTask') {
        //    $scope.tableAllValue = angular.copy(tableState);
        //}
        //tableState = $scope.tableStateValue || tableState;        

        Resource.getPage(start, number, tableState).then(function (result) {
            ctrl.displayed = result.data;
            ctrl.temp = ctrl.displayed;
            console.log(result.data);
            //ctrl.displayed = $scope.setInitialTaskData(ctrl.displayed);

            if ($rootScope.tabNames == 'DailyTask' && sortData == true) {
                //ctrl.temp1 = ctrl.displayed;
                //ctrl.temp = ctrl.temp1 || ctrl.displayed;
                $scope.tableDailyValue1 = angular.copy(tableState);
            }

            if ($rootScope.tabNames == 'DailyTask' && sortData == false) {
                //ctrl.temp1 = ctrl.displayed;
                //ctrl.temp = ctrl.temp1 || ctrl.displayed;
                if ($scope.tableDailyValue1.hasOwnProperty('search')) {
                    $scope.tableDailyValue1.search.predicateObject = {};
                }
            }
            if ($rootScope.tabNames == 'TasksAssigne' && sortData == true) {
                $scope.tableDailyValue2 = angular.copy(tableState);
            }
            if ($rootScope.tabNames == 'AllTask' && sortData == true) {
                $scope.tableDailyValue3 = angular.copy(tableState);
            }
            if ($rootScope.tabNames == 'CloseTasks' && sortData == true) {
                $scope.tableDailyValue4 = angular.copy(tableState);
            }
            if ($rootScope.tabNames == 'AdminTasks' && sortData == true) {
                $scope.tableDailyValue5 = angular.copy(tableState);
            }

            if ($rootScope.tabNames == 'TasksAssigne' && sortData == false) {
                //ctrl.temp1 = ctrl.displayed;
                //ctrl.temp = ctrl.temp1 || ctrl.displayed;
                if ($scope.tableDailyValue2.hasOwnProperty('search')) {
                    $scope.tableDailyValue2.search.predicateObject = {};
                }
            }
            if ($rootScope.tabNames == 'AllTask' && sortData == false) {
                //ctrl.temp1 = ctrl.displayed;
                //ctrl.temp = ctrl.temp1 || ctrl.displayed;
                if ($scope.tableDailyValue3.hasOwnProperty('search')) {
                    $scope.tableDailyValue3.search.predicateObject = {};
                }
            }
            if ($rootScope.tabNames == 'CloseTasks' && sortData == false) {
                //ctrl.temp1 = ctrl.displayed;
                //ctrl.temp = ctrl.temp1 || ctrl.displayed;
                if ($scope.tableDailyValue4.hasOwnProperty('search')) {
                    $scope.tableDailyValue4.search.predicateObject = {};
                }
            }
            if ($rootScope.tabNames == 'AdminTasks' && sortData == false) {
                //ctrl.temp1 = ctrl.displayed;
                //ctrl.temp = ctrl.temp1 || ctrl.displayed;
                if ($scope.tableDailyValue5.hasOwnProperty('search')) {
                    $scope.tableDailyValue5.search.predicateObject = {};
                }
            }

            //if ($rootScope.tabNames == 'DailyTask') {
            //    $scope.tabStatus.Displayed = ($scope.tabStatus.Displayed.length == 0 || ($scope.tabStatus.Displayed.length > result.data.length)) ? result.data : $scope.tabStatus.Displayed;
            //    //$scope.tableStatus1 = angular.copy(tableState);
            //    //ctrl.displayed = angular.copy($scope.tabStatus.Displayed) ;
            //}
            //if ($rootScope.tabNames == 'TasksAssigne') {
            //    $scope.tabStatus.Displayed = ($scope.tabStatus.Displayed.length == 0 || ($scope.tabStatus.Displayed.length > result.data)) ? result.data : $scope.tabStatus.Displayed;
            //    //ctrl.displayed = angular.copy($scope.tabStatus.Displayed);
            //    //$scope.tableStatus2 = angular.copy(tableState);
            //}
            //if ($rootScope.tabNames == 'AllTask') {
            //    $scope.tabStatus.Displayed = result.data;
            //    ctrl.displayed = angular.copy($scope.tabStatus.Displayed);
            //}
            //if ($rootScope.tabNames == 'CloseTasks') {
            //    $scope.tabStatus.Displayed = result.data;
            //    ctrl.displayed = angular.copy($scope.tabStatus.Displayed);
            //}

            tableState.pagination.numberOfPages = result.numberOfPages;//set the number of pages so the pagination can update
            ctrl.isLoading = false;
        });
    };

    $rootScope.tabName = function (name) {
        $rootScope.tabNames = name;
        if (name == "History") {
            $scope.showOnClose = false;
        }
        else {
            $scope.showOnClose = true;
        }
        $rootScope.$broadcast(name);
        // $scope.ShowSetRemainderButton(name);
    };

    //$scope.ShowSetRemainderButton = function (name) {

    //    if (name == 'DailyTasks') {
    //        if ($rootScope.trackerItems.length>0)
    //            $('#AddScheduleBtn').show();
    //        else
    //            $('#AddScheduleBtn').hide();
    //    }
    //    else if (name == 'AllTasks') {
    //        if ($scope.RemainingTasks.length > 0)
    //            $('#AddScheduleBtn').show();
    //        else
    //            $('#AddScheduleBtn').hide();
    //    }
    //    else {
    //        $('#AddScheduleBtn').hide();
    //    }
    //}
    // functions to catch the data after broadcasting/////
    $rootScope.$on('DailyTasks', function () {
        $scope.tabStatus = $filter('ResetTabStatus')('DailyTask');
        onLoad++;
        if (onLoad == 1) {
            //localStorage.clear();
            for (var s = 0; s < Object.keys(localStorage).length; s++) {
                if (Object.keys(localStorage)[s] == "mc.displayed") {
                    localStorage.removeItem("mc.displayed");
                }
            }
            //var hash = location.hash.replace('#', '');
            //if (hash != '') {            
            //    location.hash = '';
            //}
            //if (history.pushState) {
            //    history.pushState(null, null, '#myhash');
            //}
            //else {
            //    location.hash = '#myhash';
            //}
        }
        $rootScope.tabNames = 'DailyTask';
        $scope.showOnClose = true;
        $scope.cancelEditViewforTab();
        $rootScope.trackerItems = $scope.TasksAssigned;
        $rootScope.DailyTasksTracker = angular.copy($rootScope.trackerItems);
        $rootScope.trackerItems = $scope.setInitialTaskData($rootScope.trackerItems)
        //var s = sessionStorage.getItem('dailyValue');
        $scope.t.pagination.start = 0;
        $scope.t.search.predicateObject = {};
        $scope.t.pagination.numberOfPages = 1;
        //tableState.sort = {};        
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

        //ctrl.temp = $scope.TasksAssigned;
    });
    $rootScope.$on('TasksAssigned', function () {
        $scope.tabStatus = $filter('ResetTabStatus')('TaskAssigned');
        //j++;
        $rootScope.tabNames = 'TasksAssigne';
        $scope.showOnClose = true;
        $scope.cancelEditViewforTab();
        //$scope.t.sort.predicate = "NextFollowUpDate";
        //$scope.t.sort.reverse = false;
        $rootScope.trackerItems = $scope.DailyTasks;
        $rootScope.TasksAssignedTracker = angular.copy($rootScope.trackerItems);
        $scope.t.pagination.start = 0;
        $scope.t.pagination.numberOfPages = 0;
        //tableState.sort = {};        
        $scope.t.search.predicateObject = {};
        $rootScope.trackerItems = $scope.setInitialTaskData($rootScope.trackerItems)
        for (var i in names) {
            if ($scope.tableDailyValue2.hasOwnProperty('search')) {
                if ($scope.tableDailyValue2.search.predicateObject.hasOwnProperty(names[i])) {
                    $scope.t.search.predicateObject = $scope.tableDailyValue2.search.predicateObject;
                }
            }
        }
        $scope.t.sort.predicate = "NextFollowUpDate";
        $scope.t.sort.reverse = false;

        ctrl.callServer($scope.t);

        //ctrl.temp = $scope.DailyTasks;
    });
    $rootScope.$on('AllTasks', function () {
        $scope.tabStatus = $filter('ResetTabStatus')('AllTask');
        $rootScope.tabNames = 'AllTask';
        $rootScope.trackerItems = $scope.RemainingTasks;
        $scope.cancelEditViewforTab();
        $scope.showOnClose = true;

        //$rootScope.trackerItems = $scope.RemainingTasksPagination;
        $rootScope.AllTasksTracker = angular.copy($rootScope.trackerItems);
        $scope.t.pagination.start = 0;
        $scope.t.pagination.numberOfPages = 0;
        $scope.t.search.predicateObject = {};
        $scope.t.sort.predicate = "NextFollowUpDate";
        $rootScope.trackerItems = $scope.setInitialTaskData($rootScope.trackerItems)
        for (var i in names) {
            if ($scope.tableDailyValue3.hasOwnProperty('search')) {
                if ($scope.tableDailyValue3.search.predicateObject.hasOwnProperty(names[i])) {
                    $scope.t.search.predicateObject = $scope.tableDailyValue3.search.predicateObject;
                }
            }
        }
        $scope.t.sort.reverse = false;
        ctrl.callServer($scope.t);
        //ctrl.temp = $scope.RemainingTasks;
    });
    $rootScope.$on('ClosedTasks', function () {
        $scope.tabStatus = $filter('ResetTabStatus')('ClosedTask');
        $rootScope.tabNames = 'CloseTasks';
        $scope.showOnClose = false;
        // $rootScope.tabNames = 'ClosedTasks';
        $scope.cancelEditViewforTab();
        $rootScope.trackerItems = $scope.ClosedTasks;
        $rootScope.AllTasksTracker = angular.copy($rootScope.trackerItems);
        $scope.t.pagination.start = 0;
        //$scope.t.pagination.numberOfPages = 0;
        $scope.t.search.predicateObject = {};
        $rootScope.trackerItems = $scope.setInitialTaskData($rootScope.trackerItems)
        for (var i in names) {
            if ($scope.tableDailyValue4.hasOwnProperty('search')) {
                if ($scope.tableDailyValue4.search.predicateObject.hasOwnProperty(names[i])) {
                    $scope.t.search.predicateObject = $scope.tableDailyValue4.search.predicateObject;
                }
            }
        }
        ctrl.callServer($scope.t);
        //ctrl.temp = $scope.ClosedTasks;
    });
    $rootScope.$on('AllTaskForAdmin', function () {
        $scope.tabStatus = $filter('ResetTabStatus')('AllTasksForAdmin');
        $rootScope.tabNames = 'AdminTasks';
        // $rootScope.tabNames = 'ClosedTasks';
        $scope.showOnClose = false;
        $scope.cancelEditViewforTab();
        $rootScope.trackerItems = $scope.AllTasksForAdmin;
        $rootScope.AllTasksTracker = angular.copy($rootScope.trackerItems);
        $scope.t.pagination.start = 0;
        //$scope.t.pagination.numberOfPages = 0;
        $scope.t.search.predicateObject = {};
        $rootScope.trackerItems = $scope.setInitialTaskData($rootScope.trackerItems)
        for (var i in names) {
            if ($scope.tableDailyValue5.hasOwnProperty('search')) {
                if ($scope.tableDailyValue5.search.predicateObject.hasOwnProperty(names[i])) {
                    $scope.t.search.predicateObject = $scope.tableDailyValue5.search.predicateObject;
                }
            }
        }
        ctrl.callServer($scope.t);
        //ctrl.temp = $scope.ClosedTasks;
    });
    $rootScope.$on('History', function () {
        $scope.tabStatus = $filter('ResetTabStatus')('HistoryTask');
        $rootScope.tabNames = 'HistoryTask';
        $scope.cancelEditViewforTab();
        $scope.showOnClose = false;
        $rootScope.trackerItems = $scope.TaskHistory;
        $scope.t.pagination.start = 0;
        $scope.t.pagination.numberOfPages = 0;
        $scope.t.search.predicateObject = {};
        ctrl.callServer($scope.t);
        ctrl.temp = $scope.TaskHistory;
    });
    //$rootScope.$on("evn", function (event, args) {
    //    $scope.DailyTasks = angular.copy(args);
    //    $scope.DailyTasksPagination = angular.copy($scope.DailyTasks);
    //    $rootScope.$broadcast('TasksAssigned');

    //});
    $rootScope.$on("evnt", function (event, args) {
        $scope.TasksAssigned = angular.copy(args);
        $scope.TasksAssignedPagination = angular.copy($scope.TasksAssigned);
        $rootScope.DailyTasksTracker = angular.copy($scope.TasksAssigned);
        $rootScope.trackerItems = angular.copy($scope.TasksAssigned);
        //ctrl.callServer();
        //ctrl.temp = $rootScope.trackerItems;
        //$rootScope.$broadcast('DailyTasks');
        $rootScope.$broadcast('DailyTasks');
    });
    $rootScope.$on("event", function (event, args) {
        $scope.RemainingTasks = angular.copy(args);
        $scope.RemainingTasksPagination = angular.copy($scope.RemainingTasks);
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
    $rootScope.$on("PlanName", function (event, args) {
        $scope.PlanNames = angular.copy(args);
    });
    $rootScope.$on("Users", function (event, args) {
        $scope.users = angular.copy(args);
    });
    //////////////////////////////////////////////////////
    //$scope.clock = '';
    //$scope.tickInterval = 1000 //ms
    //$scope.tick = function (date1) {
    //    //$scope.clock = Date.now();
    //    //$scope.clock = new Date(date1);       
    //    $scope.click = new Date(date1.split('T')[0]);
    //}    

    //$scope.tick = function () {

    //    for (var i in $scope.Tasks) {
    //        $scope.clock = new Date($scope.Tasks[i].LastModifiedDate.split('T')[0]);
    //       // $scope.Tasks[i].ElapsedTime = $scope.showDiff($scope.ConvertDateForTimeElapse($scope.Tasks[i].LastModifiedDate));
    //        //$scope.tick($scope.Tasks[i].LastModifiedDate);
    //        //tick();
    //        //$interval($scope.tick, 1000);
    //        $timeout($scope.tick, $scope.tickInterval);
    //    }

    //}       

    $scope.addIntoNotesTextBox = function (Desc, div) {
        $scope.task.Notes = Desc;
        $(".ProviderTypeSelectAutoList").hide();
        $scope.NotesTemplateDropdownflag = false;
        $scope.Disablewatch = true;
    }
    $scope.showHistory = function (task) {
        $rootScope.tab = $rootScope.tabNames;
        $rootScope.tabNames = "History";
        //if (task.TaskTrackerHistories == undefined)
        //{
        //    task.TaskTrackerHistories = $rootScope.tasktrackerhistories;
        //}
        $scope.selTask = [];
        console.log($scope.selTaskIDs);
        for (var t = 0; t < $scope.selTaskIDs.length; t++) {
            if ($scope.selTaskIDs[t].SelectStatus != null && $scope.selTaskIDs[t].SelectStatus != undefined)
                $scope.selTaskIDs[t].SelectStatus = false;
        }
        $rootScope.previousTabName = $rootScope.tab;
        $scope.TaskHistory = [];
        $('.nav-tabs a:last').tab('show');
        $scope.history = true;
        //$scope.tabStatus = $filter('ResetTabStatus')('HistoryTask');
        var tabID = [];
        var sectionID = [];
        var histories = [];

        if ($rootScope.tab == 'DailyTask') {
            histories = $scope.TasksAssigned[$scope.TasksAssigned.indexOf($scope.TasksAssigned.filter(function (taskId) { return taskId.TaskTrackerId == task.TaskTrackerId })[0])];
        }
        //else if ($rootScope.tabNames == 'TasksAssigne') {
        //    histories = $scope.DailyTasks[$scope.DailyTasks.indexOf($scope.DailyTasks.filter(function (taskId) { return taskId.TaskTrackerId == task.TaskTrackerId })[0])];
        //}
        else if ($rootScope.tab == 'AllTask') {
            histories = $scope.RemainingTasks[$scope.RemainingTasks.indexOf($scope.RemainingTasks.filter(function (taskId) { return taskId.TaskTrackerId == task.TaskTrackerId })[0])];
        }
        else if ($rootScope.tab == 'CloseTasks') {
            histories = $scope.ClosedTasks[$scope.ClosedTasks.indexOf($scope.ClosedTasks.filter(function (taskId) { return taskId.TaskTrackerId == task.TaskTrackerId })[0])];
        }
        else if ($rootScope.tab == 'AdminTasks') {
            histories = $scope.AllTasksForAdmin[$scope.AllTasksForAdmin.indexOf($scope.AllTasksForAdmin.filter(function (taskId) { return taskId.TaskTrackerId == task.TaskTrackerId })[0])];
        }


        //if (task.AssignedToId == currentcduserdata.cdUser.AuthenicateUserId) {
        //   histories = $scope.TasksAssignedtoMe[$scope.TasksAssignedtoMe.indexOf($scope.TasksAssignedtoMe.filter(function (taskId) { return taskId.TaskTrackerId == task.TaskTrackerId })[0])];
        //}
        //else if (task.AssignedById == currentcduserdata.cdUser.AuthenicateUserId) {
        //    histories = $scope.TasksAssignedByMe[$scope.TasksAssignedByMe.indexOf($scope.TasksAssignedByMe.filter(function (taskId) { return taskId.TaskTrackerId == task.TaskTrackerId })[0])];
        //}


        for (var j in histories.TaskTrackerHistories) {
            var ProviderName = [];
            if (histories.TaskTrackerHistories[j].ProviderID != null)
                for (var m in $scope.AllProvidersData) {
                    if ($scope.AllProvidersData[m].ProfileId == histories.TaskTrackerHistories[j].ProviderID) { ProviderName[j] = $scope.Providers[m].Name; }
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
            for (var z in $scope.InactiveCDUsers) {
                if (histories.TaskTrackerHistories[j].AssignToCCOID == $scope.InactiveCDUsers[z].CDUserID) {
                    AssignedTo[j] = $scope.InactiveCDUsers[z].UserName
                    AssignedToId[j] = $scope.InactiveCDUsers[z].AuthenicateUserId;
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
                PlanName: TasksAssignedplanname != "" ? TasksAssignedplanname : "Not Available",
                LastUpdatedBy: histories.TaskTrackerHistories[j].LastUpdatedBy,
                //AssignedToId: $scope.users.filter(function (users) { return users.CDUserID == $scope.TasksAssignedtoMe[k].AssignedToId })[0].AuthenicateUserId, AssignedTo: AssignedTo, HospitalID: $scope.TasksAssignedtoMe[k].HospitalID,
                AssignedToId: AssignedToId[j], AssignedTo: AssignedTo[j],
                Hospital: HospitalNamefortaskassigned, Notes: note, ModifiedDate: $scope.ConvertDate(histories.TaskTrackerHistories[j].LastModifiedDate),
                TabID: tabID,
                SubSectionID: sectionID,
                CompleteStatus: histories.TaskTrackerHistories[j].Status
                //== "Active" ? "Open" : "Closed"
            });
        }
        $scope.historyMsg = false;
        if ($scope.TaskHistory.length == 0) {
            $scope.historyMsg = true;
        }

        $rootScope.trackerItems = ($scope.TaskHistory).reverse();
        //ctrl.callServer();
        //ctrl.temp = $scope.TaskHistory;

        $rootScope.$broadcast('History');
    }
    var count = 0;
    for (var i = 0; i < $rootScope.trackerItems.length; i++) {
        count++;
    }
    $scope.closeHistory = function () {
        //console.log($(this).parent().prevObject);
        ////console.log($(this).parent().find(".nav nav-tabs").attr("class"));
        //console.log($(this).parent().find(".tab"));
        //$('.nav-tabs li').removeClass('active')
        ////console.log($('.nav-tabs li'))        
        ////$("a[href='#webmaster']").parent("li").addClass("active");        
        //console.log($("a[id='tabs2']").parent("li"))       
        ////$('.nav-tabs li')[1].show();
        $('.nav-tabs li').removeClass('active')
        $scope.cancelViewforTab3();
        $scope.TableView = (($scope.VisibilityControl == "") || (typeof $scope.VisibilityControl == "undefined")) ? true : false;
        $scope.history = false;
        //$('.nav-tabs a:first').tab('show')
        //$(".nav-tabs >li:first-child").addClass("active");
        //$(".nav-tabs").tabs("option", "active", 2);

        if ($rootScope.previousTabName == 'DailyTask') {
            $("a[id='tabs1']").parent("li").tab('show')
            $rootScope.$broadcast('DailyTasks');
        }
        else if ($rootScope.previousTabName == 'TasksAssigne') {
            $("a[id='tabs2']").parent("li").tab('show')
            $rootScope.$broadcast('TasksAssigned');
        }
        else if ($rootScope.previousTabName == 'AllTask') {
            $("a[id='tabs3']").parent("li").tab('show')
            $rootScope.$broadcast('AllTasks');
        }
        else if ($rootScope.previousTabName == 'CloseTasks') {
            $("a[id='tabs4']").parent("li").tab('show')
            $rootScope.$broadcast('ClosedTasks');
        }
        else if ($rootScope.previousTabName == 'AdminTasks') {
            $("a[id='tabs5']").parent("li").tab('show')
            $rootScope.$broadcast('AllTaskForAdmin');
        }
    }
    $scope.ProviderNameforNotes = "";
    $scope.DailyTasksPagination = [];
    $scope.TasksAssigned = [];
    $scope.RemainingTasksPagination = [];
    $scope.c = [];

    // Getting all Rquired Data ----------------------------------------------------------------------
    $scope.LoadData = function () {
        $scope.progressbar = false;
        //var onLoad = 0;
        $q.all([$q.when(GetCurrentUser()),
        $q.when(CDUsersData()),
        $q.when(HospitalsData()),
        $q.when(TasksData()),
        $q.when(PlanNamesData()),
        $q.when(ProvidersData()),
        //$q.when(InsuranceCompaniesData()),
        $q.when(LoginUsersData()),
        $q.when(SubSectionData())
        ]).then(function (response) {
            for (var i in $scope.Tasks) {
                if ($scope.Tasks[i].ModeOfFollowUp != "") {
                    $scope.Tasks[i].ModeOfFollowUp = JSON.parse($scope.Tasks[i].ModeOfFollowUp);
                }
                //if ($scope.Tasks[i].InsuaranceCompanyNames != null && $scope.Tasks[i].InsuaranceCompanyNames != "") {
                //    $scope.Tasks[i].InsuaranceCompanyNames = JSON.parse($scope.Tasks[i].InsuaranceCompanyNames);
                //}
                if ($scope.Tasks[i].AssignedToId == currentcduserdata.cdUser.CDUserID) {
                    $scope.TasksAssignedtoMe.push($scope.Tasks[i]);
                }
                //if ($scope.Tasks[i].AssignedById == currentcduserdata.cdUser.CDUserID && $scope.Tasks[i].AssignedToId != currentcduserdata.cdUser.CDUserID) {
                //    $scope.TasksAssignedByMe.push($scope.Tasks[i]);
                //}
                //if ($scope.Tasks[i].AssignedToId !== currentcduserdata.cdUser.CDUserID && $scope.Tasks[i].AssignedById !== currentcduserdata.cdUser.CDUserID) {
                //    $scope.AllTasks.push($scope.Tasks[i]);
                //}
                //else {
                $scope.AllTasks.push($scope.Tasks[i]);
                //}
                $scope.Tasks[i].ElapsedTime = $scope.showDiff($scope.ConvertDateForTimeElapse($scope.Tasks[i].LastModifiedDate));
                //$scope.tick($scope.Tasks[i].LastModifiedDate);
                //tick();
                // Start the timer
                //$timeout($scope.tick, $scope.tickInterval);
                //$scope.tick();
                //$interval($scope.tick, 1000);
            }
            $scope.c = JSON.parse(localStorage.getItem('randoms'));
            if ($scope.c == null) {
                $scope.c = [];
            }
            //$scope.c.push({ Time: $scope.ConvertDateForTE($scope.Tasks[6].LastModifiedDate) });

            //localStorage.setItem('randoms', JSON.stringify($scope.c));
            $scope.userAuthID = currentcduserdata.cdUser.CDUserID;

            //$scope.SubSections = [
            //                  { Name: "State License", TabName: "#identification", SubSectionID: "#StateLicense" },
            //                  { Name: "Federal DEA Information", TabName: "#identification", SubSectionID: "#FederalDEA" },
            //                  { Name: "Medicare Information", TabName: "#identification", SubSectionID: "#MedicareInformation" },
            //                  { Name: "Medicaid Information", TabName: "#identification", SubSectionID: "#MedicaidInformation" },
            //                  { Name: "Provider Missing Items Documentation", TabName: "", SubSectionID: "" },
            //                  { Name: "Insurance Enrollment", TabName: "", SubSectionID: "" },
            //                  { Name: "Hospital Enrollment", TabName: "", SubSectionID: "" },
            //                  { Name: "Insurance Re-Enrollment", TabName: "", SubSectionID: "" },
            //                  { Name: "Hospital Re-Enrollment", TabName: "", SubSectionID: "" },
            //                  { Name: "CAQH Update", TabName: "", SubSectionID: "" },
            //                  { Name: "Location Change", TabName: "", SubSectionID: "" },
            //                  { Name: "Panel Closing", TabName: "", SubSectionID: "" },
            //                  { Name: "Provider Termination", TabName: "", SubSectionID: "" },
            //];
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
                    if ($scope.LoginUsers[i].Id == currentcduserdata.cdUser.AuthenicateUserId) {
                        $scope.ProviderNameforNotes = $scope.LoginUsers[i].UserName;
                    }
                    if (($scope.LoginUsers[i].Id == $scope.CDUsers[j].AuthenicateUserId) && ($scope.CDUsers[j].Status == "Active")) {
                        $scope.users.push({ CDUserID: $scope.CDUsers[j].CDUserID, AuthenicateUserId: $scope.CDUsers[j].AuthenicateUserId, FullName: $scope.LoginUsers[i].FullName, Email: $scope.LoginUsers[i].Email, UserName: $scope.LoginUsers[i].UserName, Status: $scope.CDUsers[j].Status });
                    }
                }
            }
            //for (var j = 0; j < $scope.CDUsers.length; j++) {
            //    if ($scope.CDUsers[j].Status == "Inactive") {
            //        $scope.InactiveCDUsers.push($scope.CDUsers[j]);
            //        $scope.CDUsers.splice(j, 1);
            //    }
            //}
            $rootScope.$broadcast("providers", $scope.Providers);
            $rootScope.$broadcast("hospitals", $scope.Hospitals);
            $rootScope.$broadcast("Insurance", $scope.InsuranceCompanies);
            $rootScope.$broadcast("Users", $scope.users);

            for (var k = 0; k < $scope.TasksAssignedtoMe.length; k++) {
                var ProviderName = "";
                if ($scope.TasksAssignedtoMe[k].ProfileID != null) {
                    for (var i in $scope.Providers) {
                        if ($scope.Providers[i].ProfileId == $scope.TasksAssignedtoMe[k].ProfileID) { ProviderName = $scope.Providers[i].Name; }
                    }
                    if (ProviderName == "") { jQuery.grep($scope.InactiveProviders, function (ele) { if (ele.ProfileId == $scope.TasksAssignedtoMe[k].ProfileID) ProviderName = ele.Name }); }
                }
                var AssignedToId = "";
                var AssignedTo = "";
                if ($scope.TasksAssignedtoMe[k].AssignedToId != null) {
                    for (var i in $scope.users) {
                        if ($scope.TasksAssignedtoMe[k].AssignedToId == $scope.users[i].CDUserID) {
                            AssignedTo = $scope.users[i].UserName
                            AssignedToId = $scope.users[i].AuthenicateUserId;
                        }
                    }
                    //var AssignedTo = $scope.users.filter(function (users) { return users.CDUserID == $scope.TasksAssignedtoMe[k].AssignedToId })[0].UserName;
                }
                var HospitalNamefortaskassigned = "";
                for (var h in $scope.Hospitals) {
                    if ($scope.TasksAssignedtoMe[k].HospitalID == $scope.Hospitals[h].HospitalID) {
                        HospitalNamefortaskassigned = $scope.Hospitals[h].HospitalName;
                    }
                }
                //var TasksAssignedinsurancecompanyname = "";
                //for (var p in $scope.InsuranceCompanies) {
                //    if ($scope.TasksAssignedtoMe[k].InsuaranceCompanyNameID == $scope.InsuranceCompanies[p].InsuaranceCompanyNameID) {
                //        TasksAssignedinsurancecompanyname = $scope.InsuranceCompanies[p].CompanyName;
                //    }
                //}
                var TasksAssignedinsurancecompanyname = [];
                TasksAssignedinsurancecompanyname = $scope.TasksAssignedtoMe[k].InsuranceCompanyName == null ? "" : $scope.TasksAssignedtoMe[k].InsuranceCompanyName.CompanyName;

                var TasksAssignedplanname = [];
                TasksAssignedplanname = $scope.TasksAssignedtoMe[k].PlanName == null ? "" : $scope.TasksAssignedtoMe[k].PlanName;

                var tabID = "";
                var sectionID = "";
                var Followups = "";
                for (var i in $scope.SubSections) {
                    if ($scope.SubSections[i].SubSectionName == $scope.TasksAssignedtoMe[k].SubSectionName) {
                        //tabID = $scope.SubSections[i].TabName;
                        //sectionID = $scope.SubSections[i].SubSectionID;
                    }
                }
                var followupdatefortasksassigned = $scope.ConvertDate($scope.TasksAssignedtoMe[k].NextFollowUpDate);
                if ($scope.TasksAssignedtoMe[k].Status == "OPEN" || $scope.TasksAssignedtoMe[k].Status == "REOPEN") {

                    $scope.TasksAssigned.push({
                        TaskTrackerId: $scope.TasksAssignedtoMe[k].TaskTrackerId, ProfileID: $scope.TasksAssignedtoMe[k].ProfileID,
                        ProviderName: ProviderName, SubSectionName: $scope.TasksAssignedtoMe[k].SubSectionName,
                        Subject: $scope.TasksAssignedtoMe[k].Subject, NextFollowUpDate: followupdatefortasksassigned,
                        ModeOfFollowUp: $scope.TasksAssignedtoMe[k].ModeOfFollowUp, FollowUp: Followups,
                        InsuranceCompanyName: TasksAssignedinsurancecompanyname,
                        PlanName: TasksAssignedplanname != "" ? TasksAssignedplanname.PlanName : "Not Available",
                        //AssignedToId: $scope.users.filter(function (users) { return users.CDUserID == $scope.TasksAssignedtoMe[k].AssignedToId })[0].AuthenicateUserId, AssignedTo: AssignedTo, HospitalID: $scope.TasksAssignedtoMe[k].HospitalID,
                        AssignedToId: AssignedToId, AssignedTo: AssignedTo, HospitalID: $scope.TasksAssignedtoMe[k].HospitalID,
                        Hospital: HospitalNamefortaskassigned, Notes: $scope.TasksAssignedtoMe[k].Notes, ModifiedDate: $scope.TasksAssignedtoMe[k].LastModifiedDate,
                        TaskTrackerHistories: $scope.TasksAssignedtoMe[k].TaskTrackerHistories,
                        AssignedBy: $scope.TasksAssignedtoMe[k].AssignedBy, AssignedById: $scope.TasksAssignedtoMe[k].AssignedById,
                        TabID: tabID,
                        SubSectionID: sectionID,
                        CompleteStatus: $scope.TasksAssignedtoMe[k].Status,
                        //== "Active" ? "Open" : "Closed",
                        LastUpdatedBy: $scope.TasksAssignedtoMe[k].LastUpdatedBy,
                        daysleft: $scope.TasksAssignedtoMe[k].daysleft
                    });
                }
                //else if ($scope.TasksAssignedtoMe[k].Status == "CLOSED") {
                //    $scope.ClosedTasks.push({
                //        TaskTrackerId: $scope.TasksAssignedtoMe[k].TaskTrackerId, ProfileID: $scope.TasksAssignedtoMe[k].ProfileID,
                //        ProviderName: ProviderName, SubSectionName: $scope.TasksAssignedtoMe[k].SubSectionName,
                //        Subject: $scope.TasksAssignedtoMe[k].Subject, NextFollowUpDate: followupdatefortasksassigned,
                //        ModeOfFollowUp: $scope.TasksAssignedtoMe[k].ModeOfFollowUp, FollowUp: Followups,
                //        InsuranceCompanyName: TasksAssignedinsurancecompanyname,
                //        PlanName:TasksAssignedplanname.PlanName,
                //        //AssignedToId: $scope.users.filter(function (users) { return users.CDUserID == $scope.TasksAssignedtoMe[k].AssignedToId })[0].AuthenicateUserId, AssignedTo: AssignedTo, HospitalID: $scope.TasksAssignedtoMe[k].HospitalID,
                //        AssignedToId: AssignedToId, AssignedTo: AssignedTo, HospitalID: $scope.TasksAssignedtoMe[k].HospitalID,
                //        Hospital: HospitalNamefortaskassigned, Notes: $scope.TasksAssignedtoMe[k].Notes, ModifiedDate: $scope.TasksAssignedtoMe[k].LastModifiedDate,
                //        TaskTrackerHistories: $scope.TasksAssignedtoMe[k].TaskTrackerHistories,
                //        AssignedBy: $scope.TasksAssignedtoMe[k].AssignedBy, AssignedById: $scope.TasksAssignedtoMe[k].AssignedById,
                //        TabID: tabID,
                //        SubSectionID: sectionID,
                //        CompleteStatus: $scope.TasksAssignedtoMe[k].Status,
                //        //== "Active" ? "Open" : "Closed",
                //        LastUpdatedBy: $scope.TasksAssignedtoMe[k].LastUpdatedBy,
                //        daysleft: $scope.TasksAssignedtoMe[k].daysleft
                //    });
                //}
            }

            $rootScope.$broadcast("evnt", $scope.TasksAssigned);

            //for (var k = 0; k < $scope.TasksAssignedByMe.length; k++) {
            //    var ProviderName = "";
            //    if ($scope.TasksAssignedByMe[k].ProfileID != null)
            //        for (var i in $scope.Providers) {
            //            if ($scope.Providers[i].ProfileId == $scope.TasksAssignedByMe[k].ProfileID) { var ProviderName = $scope.Providers[i].Name; }
            //        }
            //    //var ProviderName = $scope.Providers.filter(function (Providers) { return Providers.ProfileId == $scope.TasksAssignedByMe[k].ProfileID })[0].Name;
            //    var AssignedTo = "";
            //    var Assignedtoid = "";
            //    if ($scope.TasksAssignedByMe[k].AssignedToId != null) {
            //        for (var i in $scope.users) {
            //            if ($scope.users[i].CDUserID == $scope.TasksAssignedByMe[k].AssignedToId) {
            //                AssignedTo = $scope.users[i].UserName;
            //                Assignedtoid = $scope.users[i].AuthenicateUserId;
            //            }
            //        }
            //        //var AssignedTo = $scope.users.filter(function (users) { return users.CDUserID == $scope.TasksAssignedByMe[k].AssignedToId })[0].UserName;
            //    }
            //    var DailyTaskHospitalName = "";
            //    for (var h in $scope.Hospitals) {
            //        if ($scope.Hospitals[h].HospitalID == $scope.TasksAssignedByMe[k].HospitalID) {
            //            DailyTaskHospitalName = $scope.Hospitals[h].HospitalName;
            //        }
            //    }
            //    var DailyTasksinsurancecompanyname = [];
            //    DailyTasksinsurancecompanyname = $scope.TasksAssignedByMe[k].InsuranceCompanyName == null ? "" : $scope.TasksAssignedByMe[k].InsuranceCompanyName.CompanyName;
            //    var tabID = "";
            //    var sectionID = "";
            //    var Followups = "";
            //    for (var i in $scope.SubSections) {
            //        if ($scope.SubSections[i].SubSectionName == $scope.TasksAssignedByMe[k].SubSectionName) {
            //            //tabID = $scope.SubSections[i].TabName;
            //            //sectionID = $scope.SubSections[i].SubSectionID;
            //        }
            //    }
            //    //for (var p in $scope.InsuranceCompanies) {
            //    //    if ($scope.TasksAssignedByMe[k].InsuaranceCompanyNameID == $scope.InsuranceCompanies[p].InsuaranceCompanyNameID) {
            //    //        DailyTasksinsurancecompanyname = $scope.InsuranceCompanies[p].CompanyName;
            //    //    }
            //    //}
            //    var followupdatefordailytasks = $scope.ConvertDate($scope.TasksAssignedByMe[k].NextFollowUpDate);
            //    if ($scope.TasksAssignedByMe[k].Status == "Active") {
            //        $scope.DailyTasks.push({
            //            TaskTrackerId: $scope.TasksAssignedByMe[k].TaskTrackerId, ProfileID: $scope.TasksAssignedByMe[k].ProfileID,
            //            ProviderName: ProviderName, SubSectionName: $scope.TasksAssignedByMe[k].SubSectionName,
            //            Subject: $scope.TasksAssignedByMe[k].Subject, NextFollowUpDate: followupdatefordailytasks,
            //            ModeOfFollowUp: $scope.TasksAssignedByMe[k].ModeOfFollowUp, FollowUp: Followups,
            //            InsuranceCompanyName: DailyTasksinsurancecompanyname,
            //            AssignedToId: Assignedtoid, AssignedTo: AssignedTo, HospitalID: $scope.TasksAssignedByMe[k].HospitalID,
            //            Hospital: DailyTaskHospitalName, Notes: $scope.TasksAssignedByMe[k].Notes, ModifiedDate: $scope.TasksAssignedByMe[k].LastModifiedDate,
            //            TaskTrackerHistories: $scope.TasksAssignedByMe[k].TaskTrackerHistories,
            //            AssignedBy: $scope.TasksAssignedByMe[k].AssignedBy, AssignedById: $scope.TasksAssignedByMe[k].AssignedById,
            //            TabID: tabID,
            //            SubSectionID: sectionID,
            //            CompleteStatus: $scope.TasksAssignedByMe[k].Status == "Active" ? "Open" : "Closed",
            //            LastUpdatedBy: $scope.TasksAssignedByMe[k].LastUpdatedBy,
            //            daysleft: $scope.TasksAssignedByMe[k].daysleft
            //        });
            //    }
            //    else if ($scope.TasksAssignedByMe[k].Status == "Inactive") {
            //        $scope.ClosedTasks.push({
            //            TaskTrackerId: $scope.TasksAssignedByMe[k].TaskTrackerId, ProfileID: $scope.TasksAssignedByMe[k].ProfileID,
            //            ProviderName: ProviderName, SubSectionName: $scope.TasksAssignedByMe[k].SubSectionName,
            //            Subject: $scope.TasksAssignedByMe[k].Subject, NextFollowUpDate: followupdatefortasksassigned,
            //            ModeOfFollowUp: $scope.TasksAssignedByMe[k].ModeOfFollowUp, FollowUp: Followups,
            //            InsuranceCompanyName: TasksAssignedinsurancecompanyname,
            //            //AssignedToId: $scope.users.filter(function (users) { return users.CDUserID == $scope.TasksAssignedtoMe[k].AssignedToId })[0].AuthenicateUserId, AssignedTo: AssignedTo, HospitalID: $scope.TasksAssignedtoMe[k].HospitalID,
            //            AssignedToId: AssignedToId, AssignedTo: AssignedTo, HospitalID: $scope.TasksAssignedByMe[k].HospitalID,
            //            Hospital: HospitalNamefortaskassigned, Notes: $scope.TasksAssignedByMe[k].Notes, ModifiedDate: $scope.TasksAssignedByMe[k].LastModifiedDate,
            //            TaskTrackerHistories: $scope.TasksAssignedByMe[k].TaskTrackerHistories,
            //            AssignedBy: $scope.TasksAssignedByMe[k].AssignedBy, AssignedById: $scope.TasksAssignedByMe[k].AssignedById,
            //            TabID: tabID,
            //            SubSectionID: sectionID,
            //            CompleteStatus: $scope.TasksAssignedByMe[k].Status == "Active" ? "Open" : "Closed",
            //            LastUpdatedBy: $scope.TasksAssignedByMe[k].LastUpdatedBy,
            //            daysleft: $scope.TasksAssignedByMe[k].daysleft
            //        });
            //    }

            //}
            //for (var i in $scope.TasksAssigned) {
            //    for (var j in $scope.DailyTasks) {
            //        if ($scope.TasksAssigned[i].TaskTrackerId == $scope.DailyTasks[j].TaskTrackerId) {
            //            $scope.DailyTasks.splice(j, 1);
            //        }
            //    }
            //}
            //$rootScope.$broadcast("evn", $scope.DailyTasks);
            for (var k = 0; k < $scope.AllTasks.length; k++) {
                var ProviderName = "";
                if ($scope.AllTasks[k].ProfileID != null) {
                    for (var i in $scope.Providers) {
                        if ($scope.Providers[i].ProfileId == $scope.AllTasks[k].ProfileID) { ProviderName = $scope.Providers[i].Name; }
                    }
                    if (ProviderName == "") { jQuery.grep($scope.InactiveProviders, function (ele) { if (ele.ProfileId == $scope.AllTasks[k].ProfileID) ProviderName = ele.Name }); }
                }
                // var ProviderName = $scope.Providers.filter(function (Providers) { return Providers.ProfileId == $scope.AllTasks[k].ProfileID })[0].Name;
                var AssignedTo = "";
                var Assignedtoid = "";
                if ($scope.AllTasks[k].AssignedToId != null) {
                    for (var i in $scope.users) {
                        if ($scope.users[i].CDUserID == $scope.AllTasks[k].AssignedToId) {
                            AssignedTo = $scope.users[i].UserName;
                            Assignedtoid = $scope.users[i].AuthenicateUserId;
                        }
                    }
                    //var AssignedTo = $scope.users.filter(function (users) { return users.CDUserID == $scope.AllTasks[k].AssignedToId })[0].UserName;
                }
                var HospitalNameforremainingtasks = "";
                for (var h in $scope.Hospitals) {
                    if ($scope.Hospitals[h].HospitalID == $scope.AllTasks[k].HospitalID) {
                        HospitalNameforremainingtasks = $scope.Hospitals[h].HospitalName;
                    }
                }
                var InsuranceCompanyforalltasks = [];
                InsuranceCompanyforalltasks = $scope.AllTasks[k].InsuranceCompanyName == null ? "" : $scope.AllTasks[k].InsuranceCompanyName.CompanyName;

                var Planforalltasks = [];
                Planforalltasks = $scope.AllTasks[k].PlanName == null ? "" : $scope.AllTasks[k].PlanName;
                //for (var i in $scope.InsuranceCompanies) {
                //    if ($scope.AllTasks[k].InsuaranceCompanyNameID == $scope.InsuranceCompanies[i].InsuaranceCompanyNameID) {
                //        InsuranceCompanyforalltasks = $scope.InsuranceCompanies[i].CompanyName;
                //    }
                //}
                var tabID = "";
                var sectionID = "";
                var Followups = "";
                for (var i in $scope.SubSections) {
                    if ($scope.SubSections[i].SubSectionName == $scope.AllTasks[k].SubSectionName) {
                        //tabID = $scope.SubSections[i].TabName;
                        //sectionID = $scope.SubSections[i].SubSectionID;
                    }
                }
                var date3 = [];
                var followupdateforalltasks = $scope.ConvertDate($scope.AllTasks[k].NextFollowUpDate);
                if ($scope.AllTasks[k].Status == "OPEN" || $scope.AllTasks[k].Status == "REOPEN") {
                    $scope.RemainingTasks.push({
                        TaskTrackerId: $scope.AllTasks[k].TaskTrackerId, ProfileID: $scope.AllTasks[k].ProfileID,
                        ProviderName: ProviderName, SubSectionName: $scope.AllTasks[k].SubSectionName,
                        Subject: $scope.AllTasks[k].Subject, NextFollowUpDate: followupdateforalltasks,
                        ModeOfFollowUp: $scope.AllTasks[k].ModeOfFollowUp, FollowUp: Followups,
                        InsuranceCompanyName: InsuranceCompanyforalltasks,
                        PlanName: Planforalltasks != "" ? Planforalltasks.PlanName : "Not Available",
                        //AssignedToId: $scope.users.filter(function (users) { return users.CDUserID == $scope.AllTasks[k].AssignedToId })[0].AuthenicateUserId, AssignedTo: AssignedTo, HospitalID: $scope.AllTasks[k].HospitalID,
                        AssignedToId: Assignedtoid, AssignedTo: AssignedTo, HospitalID: $scope.AllTasks[k].HospitalID,
                        Hospital: HospitalNameforremainingtasks, Notes: $scope.AllTasks[k].Notes, ModifiedDate: $scope.AllTasks[k].LastModifiedDate,
                        TaskTrackerHistories: $scope.AllTasks[k].TaskTrackerHistories,
                        AssignedBy: $scope.AllTasks[k].AssignedBy, AssignedById: $scope.AllTasks[k].AssignedById,
                        TabID: tabID,
                        SubSectionID: sectionID,
                        CompleteStatus: $scope.AllTasks[k].Status,
                        //== "Active" ? "Open" : "Closed",
                        LastUpdatedBy: $scope.AllTasks[k].LastUpdatedBy,
                        daysleft: $scope.AllTasks[k].daysleft
                    });
                }
                else if ($scope.AllTasks[k].Status == "CLOSED") {
                    $scope.ClosedTasks.push({
                        TaskTrackerId: $scope.AllTasks[k].TaskTrackerId, ProfileID: $scope.AllTasks[k].ProfileID,
                        ProviderName: ProviderName, SubSectionName: $scope.AllTasks[k].SubSectionName,
                        Subject: $scope.AllTasks[k].Subject, NextFollowUpDate: followupdateforalltasks,
                        ModeOfFollowUp: $scope.AllTasks[k].ModeOfFollowUp, FollowUp: Followups,
                        InsuranceCompanyName: TasksAssignedinsurancecompanyname,
                        PlanName: Planforalltasks != "" ? Planforalltasks.PlanName : "Not Available",
                        //AssignedToId: $scope.users.filter(function (users) { return users.CDUserID == $scope.TasksAssignedtoMe[k].AssignedToId })[0].AuthenicateUserId, AssignedTo: AssignedTo, HospitalID: $scope.TasksAssignedtoMe[k].HospitalID,
                        AssignedToId: AssignedToId, AssignedTo: AssignedTo, HospitalID: $scope.AllTasks[k].HospitalID,
                        Hospital: HospitalNamefortaskassigned, Notes: $scope.AllTasks[k].Notes, ModifiedDate: $scope.AllTasks[k].LastModifiedDate,
                        TaskTrackerHistories: $scope.AllTasks[k].TaskTrackerHistories,
                        AssignedBy: $scope.AllTasks[k].AssignedBy, AssignedById: $scope.AllTasks[k].AssignedById,
                        TabID: tabID,
                        SubSectionID: sectionID,
                        CompleteStatus: $scope.AllTasks[k].Status,
                        //== "Active" ? "Open" : "Closed",
                        LastUpdatedBy: $scope.AllTasks[k].LastUpdatedBy,
                        daysleft: $scope.AllTasks[k].daysleft
                    });
                }
            }
            $rootScope.$broadcast("event", $scope.RemainingTasks);

            for (var k = 0; k < $scope.Tasks.length; k++) {
                var ProviderName = "";
                if ($scope.Tasks[k].ProfileID != null) {
                    for (var i in $scope.Providers) {
                        if ($scope.Providers[i].ProfileId == $scope.Tasks[k].ProfileID) { ProviderName = $scope.Providers[i].Name; }
                    }
                    if (ProviderName == "") { jQuery.grep($scope.InactiveProviders, function (ele) { if (ele.ProfileId == $scope.Tasks[k].ProfileID) ProviderName = ele.Name }); }
                }
                var AssignedToId = "";
                var AssignedTo = "";
                if ($scope.Tasks[k].AssignedToId != null) {
                    for (var i in $scope.users) {
                        if ($scope.Tasks[k].AssignedToId == $scope.users[i].CDUserID) {
                            AssignedTo = $scope.users[i].UserName
                            AssignedToId = $scope.users[i].AuthenicateUserId;
                        }
                    }

                }
                var AssignedById = "";
                var AssignedBy = "";
                if ($scope.Tasks[k].AssignedById != null) {
                    for (var i in $scope.users) {
                        if ($scope.Tasks[k].AssignedById == $scope.users[i].CDUserID) {
                            AssignedBy = $scope.users[i].UserName
                            AssignedById = $scope.users[i].AuthenicateUserId;
                        }
                    }
                }

                var HospitalNamefortaskassigned = "";
                for (var h in $scope.Hospitals) {
                    if ($scope.Tasks[k].HospitalID == $scope.Hospitals[h].HospitalID) {
                        HospitalNamefortaskassigned = $scope.Hospitals[h].HospitalName;
                    }
                }

                var TasksAssignedinsurancecompanyname = [];
                TasksAssignedinsurancecompanyname = $scope.Tasks[k].InsuranceCompanyName == null ? "" : $scope.Tasks[k].InsuranceCompanyName.CompanyName;

                var TasksAssignedplanname = [];
                TasksAssignedplanname = $scope.Tasks[k].PlanName == null ? "" : $scope.Tasks[k].PlanName;


                var tabID = "";
                var sectionID = "";
                var Followups = "";
                for (var i in $scope.SubSections) {
                    if ($scope.SubSections[i].SubSectionName == $scope.Tasks[k].SubSectionName) {
                        //tabID = $scope.SubSections[i].TabName;
                        //sectionID = $scope.SubSections[i].SubSectionID;
                    }
                }
                var followupdatefortasksassigned = $scope.ConvertDate($scope.Tasks[k].NextFollowUpDate);

                $scope.AllTasksForAdmin.push({
                    TaskTrackerId: $scope.Tasks[k].TaskTrackerId, ProfileID: $scope.Tasks[k].ProfileID,
                    ProviderName: ProviderName, SubSectionName: $scope.Tasks[k].SubSectionName,
                    Subject: $scope.Tasks[k].Subject, NextFollowUpDate: followupdatefortasksassigned,
                    ModeOfFollowUp: $scope.Tasks[k].ModeOfFollowUp, FollowUp: Followups,
                    InsuranceCompanyName: TasksAssignedinsurancecompanyname,
                    PlanName: TasksAssignedplanname != "" ? TasksAssignedplanname.PlanName : "Not Available",
                    //AssignedToId: $scope.users.filter(function (users) { return users.CDUserID == $scope.TasksAssignedtoMe[k].AssignedToId })[0].AuthenicateUserId, AssignedTo: AssignedTo, HospitalID: $scope.TasksAssignedtoMe[k].HospitalID,
                    AssignedToId: AssignedToId, AssignedTo: AssignedTo, AssignedById: AssignedById, AssignedBy: AssignedBy, HospitalID: $scope.Tasks[k].HospitalID,
                    Hospital: HospitalNamefortaskassigned, Notes: $scope.Tasks[k].Notes, ModifiedDate: $scope.Tasks[k].LastModifiedDate,
                    ElapsedTime: $scope.Tasks[k].ElapsedTime,
                    TaskTrackerHistories: $scope.Tasks[k].TaskTrackerHistories,
                    TabID: tabID,
                    SubSectionID: sectionID,
                    CompleteStatus: $scope.Tasks[k].Status,
                    //== "Active" ? "Open" : "Closed",
                    LastUpdatedBy: $scope.Tasks[k].LastUpdatedBy,
                    daysleft: $scope.Tasks[k].daysleft
                });

            }
            $scope.InactiveCDUsers = $.grep($scope.users, function (element) { return element.Status == "CLOSED"; });
            $scope.users = $.grep($scope.users, function (element) { return element.Status != "CLOSED"; });
            $scope.progressbar = true;
            $scope.showLoading = false;
            $('#tasktrackerdiv').show();


        }, function (error) {
            $scope.progressbar = true;
            $scope.showLoading = false;
            $('#tasktrackerdiv').show();
        });
    }

    $scope.hideHospitalSearchDiv = false;

    //To Display the drop down div
    $scope.searchCumDropDown = function (divId) {
        //if (divId == "ForNotes") {
        //    $scope.hidenotesdiv = false;
        //}
        $(".ProviderTypeSelectAutoList").hide();
        $("#" + divId).show();
    };
    $scope.errormessage = false;
    $scope.Tempfollowup = [];
    $scope.addView = false;
    $scope.editView = false;
    $scope.errormessageforprovider = false;
    $scope.errormessageforprovider = false;
    $scope.errormessageforsubsection = false;
    $scope.errormessageforHospitalName = false;
    $scope.errormessageforInsurancecompany = false;
    $scope.errormessageforAssignedto = false;

    // Select functions for dropdowns
    $scope.SelectProvider = function (Provider) {
        $scope.errormessageforprovider = false;
        $scope.task.ProfileID = Provider.ProfileId;
        $scope.task.ProviderName = Provider.Name;

        $(".ProviderTypeSelectAutoList").hide();
    }
    $scope.SelectUser = function (User) {
        $scope.errormessageforAssignedto = false;
        $scope.task.AssignedToId = User.AuthenicateUserId;
        $scope.task.AssignedTo = User.UserName;
        $(".ProviderTypeSelectAutoList").hide();
    }
    $scope.SelectHospital = function (Hospital) {
        $scope.task.HospitalID = Hospital.HospitalID;
        $scope.task.Hospital = Hospital.HospitalName;
        $(".ProviderTypeSelectAutoList").hide();
    }
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
        //$scope.
        $scope.Tempfollowup1 = JSON.stringify(angular.copy($scope.Tempfollowup));

    }
    $scope.SelectSubSection = function (SubSection) {
        $scope.errormessageforsubsection = false;
        $scope.task.SubSectionName = SubSection.SubSectionName;
        $(".ProviderTypeSelectAutoList").hide();
    }
    $scope.SelectInsurance = function (InsuranceCompany) {
        $scope.task.PlanName = InsuranceCompany.PlanName;
        $(".ProviderTypeSelectAutoList").hide();
    }
    ////////////////////////////////////////

    // Manage partial Views
    $scope.showAddView = function () {
        //$scope.validationfunction = true;
        //$scope.errormessageforsubject = false;
        //$scope.errormessageforfollowupdate = false;
        $scope.VisibilityControl = "";
        $scope.Tempfollowup1 = "";
        $scope.notetask = true;
        $scope.Tempfollowup = [];
        $scope.visible = 'add';
        $scope.Followups = TempFollowupHelper;
        $scope.task = {};
        $scope.temp = [];
        var rand = '';
        var date = Date();
        var Time = date.split(" ")[4];
        var AMPM = $scope.changeTimeAmPm(Time);
        date = new Date();
        $scope.Providers = jQuery.grep(AllProviders, function (ele) { return ele.Status == "Active" });
        //$scope.task.Notes = $scope.ProviderNameforNotes + " - " + (date.getMonth() + 1) + "/" + date.getDate() + "/" + date.getFullYear() + " - " + AMPM;
        //$scope.temp.push($scope.ProviderNameforNotes + " - " + (date.getMonth() + 1) + "/" + date.getDate() + "/" + date.getFullYear() + " - " + AMPM);
        $scope.TempNotes = (date.getMonth() + 1) + "/" + date.getDate() + "/" + date.getFullYear() + "~" + AMPM + "~" + $scope.ProviderNameforNotes + "~";
        //$('#prefill').addClass('non-editable').val(rand);
        //$scope.detailView = false;
        $scope.addView = true;
        //$scope.editView = false;
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
            hours = parseInt(hours) == 12 ? 12 : (12 + parseInt(hours));
            minutes = (minutes.indexOf('AM') != -1) ? minutes.split('AM')[0].trim() : minutes.split('PM')[0].trim();
        }
        else {
            minutes = (minutes.indexOf('AM') != -1) ? minutes.split('AM')[0].trim() : minutes.split('PM')[0].trim();

        }
        var hours = hours.toString();
        var minutes = minutes.toString();

        if (hours < 10) hours = "0" + hours;
        if (minutes < 10) minutes = "0" + minutes;

        //minutes = minutes.length == 1 ? minutes < 10 ? '0' + minutes : minutes : minutes;

        //minutes = minutes < 9 ? '00' : minutes;
        var strTime = hours.replace(" ", "") + ':' + minutes;
        return strTime;
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
        $rootScope.trackerItems = $scope.TasksAssigned;
        $rootScope.$broadcast('DailyTasks');
        //ctrl.callServer();
        //ctrl.temp = $rootScope.trackerItems;
    }

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
            if (Insurancename == $scope.InsuranceCompanies[p].PlanName) {
                Insurancecompanyresult = $scope.InsuranceCompanies[p].PlanName
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
    /////////////////////////////////////////////////////////////

    //CRUD functions/////////
    $scope.addNewTask = function (task) {
        //$scope.validationfunction = true;
        //if (task.NextFollowUpDate == undefined)
        //    $scope.errormessageforfollowupdate = true;
        //if (task.Subject == undefined)
        //    $scope.errormessageforsubject = true;
        //ctrl.callServer($scope.t);
        $scope.notetask = false;
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
                        var Providername = "";
                        for (var i in $scope.Providers) {
                            if (data.ProfileID == $scope.Providers[i].ProfileId) {
                                Providername = $scope.Providers[i].Name;
                            }
                        }
                        //var AssignedTo = $scope.users.filter(function (users) { return users.CDUserID == $scope.AllTasks[k].AssignedToId })[0].UserName;
                        if (data.AssignedToId != currentcduserdata.cdUser.CDUserID) {
                            var DailyTasksforaddedtask = {
                                TaskTrackerId: data.TaskTrackerId, ProfileID: data.ProfileID,
                                //ProviderName: $scope.Providers.filter(function (Providers) { return Providers.ProfileId == data.ProfileID })[0].Name, SubSectionName: data.SubSectionName,
                                ProviderName: Providername, SubSectionName: data.SubSectionName,
                                //Subject: data.Subject, NextFollowUpDate: data.NextFollowUpDate = null ? "" : data.NextFollowUpDate,
                                Subject: data.Subject, NextFollowUpDate: followupdateforaddtask,
                                ModeOfFollowUp: data.ModeOfFollowUp, FollowUp: Followups,
                                //InsuranceCompanyName: Insurancecompanyforaddtask,
                                InsuranceCompanyName: Insurancecompanyforaddtask,
                                PlanName: PlanNameforaddtask != "" ? PlanNameforaddtask : "Not Available",
                                AssignedById: data.AssignedById,
                                //AssignedToId: $scope.users.filter(function (users) { return users.CDUserID == data.AssignedToId })[0].AuthenicateUserId, AssignedTo: $scope.users.filter(function (users) { return users.CDUserID == data.AssignedToId })[0].UserName, HospitalID: data.HospitalID,
                                AssignedToId: Assignedtoid, AssignedTo: AssignedTo, HospitalID: data.HospitalID,
                                Hospital: Hospitalforaddtask, Notes: data.Notes, ModifiedDate: data.LastModifiedDate,
                                TabID: tabID,
                                TaskTrackerHistories: data.TaskTrackerHistories,
                                SubSectionID: sectionID,
                                CompleteStatus: "OPEN",
                                LastUpdatedBy: data.LastUpdatedBy,
                                daysleft: data.daysleft
                            };
                            $scope.RemainingTasks.splice(0, 0, DailyTasksforaddedtask);
                            $rootScope.$broadcast("event", $scope.RemainingTasks);
                        }
                        if (data.AssignedToId == currentcduserdata.cdUser.CDUserID) {
                            var TasksAssignedforaddedtask = {
                                TaskTrackerId: data.TaskTrackerId, ProfileID: data.ProfileID,
                                //ProviderName: $scope.Providers.filter(function (Providers) { return Providers.ProfileId == data.ProfileID })[0].Name, SubSectionName: data.SubSectionName,
                                ProviderName: Providername, SubSectionName: data.SubSectionName,
                                //Subject: data.Subject, NextFollowUpDate: data.NextFollowUpDate = null ? "" : data.NextFollowUpDate,
                                Subject: data.Subject, NextFollowUpDate: followupdateforaddtask,
                                ModeOfFollowUp: data.ModeOfFollowUp, FollowUp: Followups,
                                InsuranceCompanyName: Insurancecompanyforaddtask,
                                PlanName: PlanNameforaddtask != "" ? PlanNameforaddtask : "Not Available",
                                AssignedById: data.AssignedById,
                                //AssignedToId: $scope.users.filter(function (users) { return users.CDUserID == data.AssignedToId })[0].AuthenicateUserId, AssignedTo: $scope.users.filter(function (users) { return users.CDUserID == data.AssignedToId })[0].UserName, HospitalID: data.HospitalID,
                                AssignedToId: Assignedtoid, AssignedTo: AssignedTo, HospitalID: data.HospitalID,
                                //Hospital: data.HospitalID == null ? "" : $scope.Hospitals.filter(function (Hospitals) { return Hospitals.HospitalID == data.HospitalID })[0].HospitalName, Notes: data.Notes, ModifiedDate: data.LastModifiedDate,
                                Hospital: Hospitalforaddtask, Notes: data.Notes, ModifiedDate: data.LastModifiedDate,
                                TabID: tabID,
                                TaskTrackerHistories: data.TaskTrackerHistories,
                                SubSectionID: sectionID,
                                CompleteStatus: "OPEN",
                                LastUpdatedBy: data.LastUpdatedBy,
                                daysleft: data.daysleft
                            };
                            $scope.TasksAssigned.splice(0, 0, TasksAssignedforaddedtask);
                            $rootScope.$broadcast("evnt", $scope.TasksAssigned);
                            $scope.RemainingTasks.splice(0, 0, TasksAssignedforaddedtask);
                            $rootScope.$broadcast("event", $scope.RemainingTasks);
                            //$("#numberOfNotifications").text(parseInt($("#numberOfNotifications")[0].textContent) + 1);
                        }


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
        //$scope.validationfunction = false;
    }
    $scope.editTask = function () {
        //$scope.validationfunction = true;
        //$scope.remVal();
        //$scope.remnextfollowupVal($scope.followupdatevalue);
        //if ($scope.validation == "")
        //    $scope.errormessageforfollowupdate = true;
        //if ($scope.subjval == "")
        //    $scope.errormessageforsubject = true;
        $scope.CancelReminder();//for closing reminder data
        $scope.flag = true;
        var d1 = new $.Deferred();
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
                        data.daysleft = $filter("TimeDiff")($scope.task.NextFollowUpDate);

                        //here comes new data for Next followUp date
                        //$.ajax({
                        //    url: rootDir + '/Dashboard/GetTaskExpiryCounts?cdUserID=' + localStorage.getItem("UserID"),
                        //    //data: {
                        //    //    format:  'json'
                        //    //},
                        //    error: function () {
                        //        //$('#info').html('<p>An error has occurred</p>');
                        //    },
                        //    dataType: 'json',
                        //    success: function (data) {
                        //        var expired = data.Result.ExpiredCount;
                        //        var expiringToday = data.Result.ExpiringTodayCount;
                        //        localStorage.setItem("UserID", cduserdata.cdUser.CDUserID);
                        //        if (expired == "" || expired == null || expired == undefined) {
                        //            expired = 0;
                        //        }
                        //        if (expiringToday == "" || expiringToday == null || expiringToday == undefined) {
                        //            expiringToday = 0;
                        //        }
                        //        //sessionStorage.setItem("expired_Task", data.Result.ExpiredCount);
                        //        //sessionStorage.setItem("expiring_Task", data.Result.ExpiringTodayCount);
                        //        //sessionStorage.setItem("DataStatus", "Updated");

                        //        localStorage.setItem("expired_Task", expired);
                        //        localStorage.setItem("expiring_Task", expiringToday);
                        //        $("#expired_Task").html("");
                        //        $("#expiring_Task").html("");
                        //        $("#expired_Task").html(expired);
                        //        $("#expiring_Task").html(expiringToday);
                        //    },
                        //})

                        data.ModeOfFollowUp = JSON.parse(data.ModeOfFollowUp);
                        var editedTask = [];
                        //$rootScope.tasktrackerhistories = data.TaskTrackerHistories;
                        //data.InsuaranceCompanyNames = JSON.parse(data.InsuaranceCompanyNames);
                        var tabID = "";
                        var sectionID = "";
                        var Followups = "";
                        $scope.Tempfollowup = data.ModeOfFollowUp;
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
                        var AssignedTo = "";
                        var Assignedtoid = "";
                        for (var i in $scope.users) {
                            if ($scope.users[i].CDUserID == data.AssignedToId) {
                                AssignedTo = $scope.users[i].UserName;
                                Assignedtoid = $scope.users[i].AuthenicateUserId;
                            }
                        }
                        var Providername = "";
                        for (var i in $scope.Providers) {
                            if (data.ProfileID == $scope.Providers[i].ProfileId) {
                                Providername = $scope.Providers[i].Name;
                            }
                        }
                        var TasksAssignedplanname = "";
                        for (var p in $scope.PlanNames) {
                            if (data.PlanID == $scope.PlanNames[p].PlanID) {
                                TasksAssignedplanname = $scope.PlanNames[p].PlanName;
                            }
                        }
                        if ($rootScope.tabNames == 'DailyTask') {
                            $scope.TasksAssigned[$scope.TasksAssigned.indexOf($scope.TasksAssigned.filter(function (updates) { return updates.TaskTrackerId == data.TaskTrackerId })[0])].TaskTrackerHistories = (data.TaskTrackerHistories);
                        }
                        //else if ($rootScope.tabNames == 'TasksAssigne') {
                        //    $scope.DailyTasks[$scope.DailyTasks.indexOf($scope.DailyTasks.filter(function (updates) { return updates.TaskTrackerId == data.TaskTrackerId })[0])].TaskTrackerHistories = (data.TaskTrackerHistories);
                        //}
                        else if ($rootScope.tabNames == 'AllTask') {
                            $scope.RemainingTasks[$scope.RemainingTasks.indexOf($scope.RemainingTasks.filter(function (updates) { return updates.TaskTrackerId == data.TaskTrackerId })[0])].TaskTrackerHistories = (data.TaskTrackerHistories);
                            //var task = $.grep($scope.RemainingTasks, function (e) { return e.TaskTrackerId == data.TaskTrackerId; });
                            //var task = $scope.RemainingTasks.indexOf($scope.RemainingTasks.filter(function (datas) { return datas.TaskTrackerId == data.TaskTrackerId })[0]);
                            //if (task != null) {
                            //    $scope.RemainingTasks.splice(task, 1);
                            //}
                        }
                        //var updateHistory=$scope.TasksAssignedtoMe[$scope.TasksAssignedtoMe.indexOf($scope.TasksAssignedtoMe.filter(function (updates) { return updates.TaskTrackerId == data.TaskTrackerId })[0])].TaskTrackerHistories
                        //$scope.TasksAssignedtoMe.Push($.grep($scope.TasksAssignedtoMe,function(items){return items.}))
                        var updateddate = $scope.ConvertDate(data.NextFollowUpDate);
                        if (($scope.VisibilityControl == 'editViewforTab') && ($rootScope.tabNames == 'DailyTask')) {
                            if (data.AssignedToId == currentcduserdata.cdUser.CDUserID) {
                                $scope.TasksAssigned.splice($scope.TasksAssigned.indexOf($scope.TasksAssigned.filter(function (datas) { return datas.TaskTrackerId == data.TaskTrackerId })[0]), 1);
                                var TasksAssignedforedittask = {
                                    TaskTrackerId: data.TaskTrackerId, ProfileID: data.ProfileID,
                                    //ProviderName: $scope.Providers.filter(function (Providers) { return Providers.ProfileId == data.ProfileID })[0].Name == 'undefined' ? "" : $scope.Providers.filter(function (Providers) { return Providers.ProfileId == data.ProfileID })[0].Name, SubSectionName: data.SubSectionName,
                                    ProviderName: Providername, SubSectionName: data.SubSectionName,
                                    Subject: data.Subject, NextFollowUpDate: updateddate,
                                    ModeOfFollowUp: data.ModeOfFollowUp, FollowUp: Followups,
                                    //InsuranceCompanyName: updatedinsurancecompany,
                                    InsuranceCompanyName: updatedinsurancecompany,
                                    PlanName: TasksAssignedplanname != "" ? TasksAssignedplanname : "Not Available",
                                    //AssignedToId: $scope.users.filter(function (users) { return users.CDUserID == data.AssignedToId })[0].AuthenicateUserId, AssignedTo: $scope.users.filter(function (users) { return users.CDUserID == data.AssignedToId })[0].UserName, HospitalID: data.HospitalID,
                                    AssignedToId: Assignedtoid, AssignedTo: AssignedTo, HospitalID: data.HospitalID,
                                    AssignedById: data.AssignedById,
                                    Hospital: taskassignedhospital, Notes: data.Notes, ModifiedDate: data.LastModifiedDate,
                                    TaskTrackerHistories: data.TaskTrackerHistories,
                                    TabID: tabID,
                                    SubSectionID: sectionID,
                                    CompleteStatus: "Open",
                                    LastUpdatedBy: data.LastUpdatedBy,
                                    daysleft: data.daysleft
                                };
                                editedTask = angular.copy(TasksAssignedforedittask);
                                $scope.TasksAssigned.splice(0, 0, TasksAssignedforedittask);
                                $rootScope.$broadcast("evnt", $scope.TasksAssigned);
                                //if (data.AssignedToId != $scope.result[0].CDUserID)
                                var task = $scope.RemainingTasks.indexOf($scope.RemainingTasks.filter(function (datas) { return datas.TaskTrackerId == data.TaskTrackerId })[0]);
                                $scope.RemainingTasks.splice(task, 1);
                                $scope.RemainingTasks.splice(0, 0, TasksAssignedforedittask);
                                $rootScope.$broadcast("event", $scope.RemainingTasks);
                                //$rootScope.deepu = false;
                            }
                            else {
                                //$scope.TasksAssignedPagination.splice($scope.TasksAssignedPagination.indexOf($scope.TasksAssignedPagination.filter(function (datas) { return datas.TaskTrackerId == data.TaskTrackerId })[0]), 1);
                                $scope.TasksAssigned.splice($scope.TasksAssigned.indexOf($scope.TasksAssigned.filter(function (datas) { return datas.TaskTrackerId == data.TaskTrackerId })[0]), 1);
                                $rootScope.$broadcast("evnt", $scope.TasksAssigned);
                                $rootScope.tabNames = 'AllTask';
                                $rootScope.deepu = true;
                            }

                            if (data.AssignedToId != currentcduserdata.cdUser.CDUserID) {
                                //for (var i in $scope.RemainingTasks) {
                                //    if ($scope.RemainingTasks[i].TaskTrackerId == data.TaskTrackerId) {
                                //        $scope.RemainingTasks.splice(i, 1);
                                //    }
                                //}
                                //$scope.DailyTasks.splice($scope.DailyTasks.indexOf($scope.DailyTasks.filter(function (DailyTasks) { return DailyTasks.TaskTrackerId == data.TaskTrackerId })[0]), 1);
                                var DailyTasksforedittask = {
                                    TaskTrackerId: data.TaskTrackerId, ProfileID: data.ProfileID,
                                    //ProviderName: $scope.Providers.filter(function (Providers) { return Providers.ProfileId == data.ProfileID })[0].Name == 'undefined' ? "" : $scope.Providers.filter(function (Providers) { return Providers.ProfileId == data.ProfileID })[0].Name, SubSectionName: data.SubSectionName,
                                    ProviderName: Providername, SubSectionName: data.SubSectionName,
                                    Subject: data.Subject, NextFollowUpDate: updateddate,
                                    ModeOfFollowUp: data.ModeOfFollowUp, FollowUp: Followups,
                                    InsuranceCompanyName: updatedinsurancecompany,
                                    PlanName: TasksAssignedplanname != "" ? TasksAssignedplanname : "Not Available",
                                    //InsuranceCompanyName: data.InsuranceCompanyName,
                                    //AssignedToId: $scope.users.filter(function (users) { return users.CDUserID == data.AssignedToId })[0].AuthenicateUserId, AssignedTo: $scope.users.filter(function (users) { return users.CDUserID == data.AssignedToId })[0].UserName, HospitalID: data.HospitalID,
                                    AssignedToId: Assignedtoid, AssignedTo: AssignedTo, HospitalID: data.HospitalID,
                                    AssignedById: data.AssignedById,
                                    Hospital: taskassignedhospital, Notes: data.Notes, ModifiedDate: data.LastModifiedDate,
                                    TaskTrackerHistories: data.TaskTrackerHistories,
                                    TabID: tabID,
                                    SubSectionID: sectionID,
                                    CompleteStatus: "Open",
                                    LastUpdatedBy: data.LastUpdatedBy,
                                    daysleft: data.daysleft
                                };
                                editedTask = angular.copy(DailyTasksforedittask);
                                var task = $scope.RemainingTasks.indexOf($scope.RemainingTasks.filter(function (datas) { return datas.TaskTrackerId == data.TaskTrackerId })[0]);
                                $scope.RemainingTasks.splice(task, 1);
                                $scope.RemainingTasks.splice(0, 0, DailyTasksforedittask);
                                //$rootScope.$broadcast("evn", $scope.DailyTasks);
                                $rootScope.$broadcast("event", $scope.RemainingTasks);
                                //$rootScope.$broadcast("evnt", $scope.TasksAssigned);

                            }
                        }
                        else if (($scope.VisibilityControl == "editViewforTab") && ($rootScope.tabNames == 'AllTask')) {


                            if (data.AssignedToId == currentcduserdata.cdUser.CDUserID) {
                                // $scope.AllTasks.splice($scope.AllTasks.indexOf($scope.AllTasks.filter(function (datas) { return datas.TaskTrackerId == data.TaskTrackerId })[0]), 1);
                                //for (var i in $scope.RemainingTasks) {
                                //    if ($scope.RemainingTasks[i].TaskTrackerId == data.TaskTrackerId) {
                                //        $scope.RemainingTasks.splice(i, 1);
                                //    }
                                //}
                                var TasksAssignedforedittask = {
                                    TaskTrackerId: data.TaskTrackerId, ProfileID: data.ProfileID,
                                    //ProviderName: $scope.Providers.filter(function (Providers) { return Providers.ProfileId == data.ProfileID })[0].Name == 'undefined' ? "" : $scope.Providers.filter(function (Providers) { return Providers.ProfileId == data.ProfileID })[0].Name, SubSectionName: data.SubSectionName,
                                    ProviderName: Providername, SubSectionName: data.SubSectionName,
                                    Subject: data.Subject, NextFollowUpDate: updateddate,
                                    ModeOfFollowUp: data.ModeOfFollowUp, FollowUp: Followups,
                                    //InsuranceCompanyName: updatedinsurancecompany,
                                    InsuranceCompanyName: updatedinsurancecompany,
                                    PlanName: TasksAssignedplanname != "" ? TasksAssignedplanname : "Not Available",
                                    //AssignedToId: $scope.users.filter(function (users) { return users.CDUserID == data.AssignedToId })[0].AuthenicateUserId, AssignedTo: $scope.users.filter(function (users) { return users.CDUserID == data.AssignedToId })[0].UserName, HospitalID: data.HospitalID,
                                    AssignedToId: Assignedtoid, AssignedTo: AssignedTo, HospitalID: data.HospitalID,
                                    AssignedById: data.AssignedById,
                                    Hospital: taskassignedhospital, Notes: data.Notes, ModifiedDate: data.LastModifiedDate,
                                    TabID: tabID,
                                    SubSectionID: sectionID,
                                    CompleteStatus: "Open",
                                    LastUpdatedBy: data.LastUpdatedBy,
                                    daysleft: data.daysleft,
                                    TaskTrackerHistories: data.TaskTrackerHistories,
                                };

                                editedTask = angular.copy(TasksAssignedforedittask);
                                //if (data.AssignedToId != $scope.result[0].CDUserID)
                                var task = $scope.TasksAssigned.indexOf($scope.TasksAssigned.filter(function (datas) { return datas.TaskTrackerId == data.TaskTrackerId })[0]);
                                if (task != null && TasksAssignedforedittask.AssignedTo == currentcduserdata.cdUser.EmailId) {
                                    $scope.TasksAssigned.splice(task, 1);
                                }

                                $scope.TasksAssigned.splice(0, 0, TasksAssignedforedittask);
                                $rootScope.$broadcast("evnt", $scope.TasksAssigned);
                                //if (data.AssignedToId != $scope.result[0].CDUserID)
                                //var kjhkh = $.grep($scope.RemainingTasks, function (e) { return e.TaskTrackerId == data.TaskTrackerId; })[0];
                                var remtask = $scope.RemainingTasks.indexOf($scope.RemainingTasks.filter(function (datas) { return datas.TaskTrackerId == data.TaskTrackerId })[0]);
                                if (remtask != null && TasksAssignedforedittask.AssignedTo == currentcduserdata.cdUser.EmailId) {
                                    $scope.RemainingTasks.splice(remtask, 1);
                                }
                                $scope.RemainingTasks.splice(0, 0, TasksAssignedforedittask);
                                $rootScope.$broadcast("event", $scope.RemainingTasks);
                            }
                            if (data.AssignedToId != currentcduserdata.cdUser.CDUserID) {
                                //for (var i in $scope.RemainingTasks) {
                                //    if ($scope.RemainingTasks[i].TaskTrackerId == data.TaskTrackerId) {
                                //        $scope.RemainingTasks.splice(i, 1);
                                //    }
                                //}
                                //$scope.DailyTasks.splice($scope.DailyTasks.indexOf($scope.DailyTasks.filter(function (DailyTasks) { return DailyTasks.TaskTrackerId == data.TaskTrackerId })[0]), 1);
                                var AllTasksforedittask = {
                                    TaskTrackerId: data.TaskTrackerId, ProfileID: data.ProfileID,
                                    //ProviderName: $scope.Providers.filter(function (Providers) { return Providers.ProfileId == data.ProfileID })[0].Name == 'undefined' ? "" : $scope.Providers.filter(function (Providers) { return Providers.ProfileId == data.ProfileID })[0].Name, SubSectionName: data.SubSectionName,
                                    ProviderName: Providername, SubSectionName: data.SubSectionName,
                                    Subject: data.Subject, NextFollowUpDate: updateddate,
                                    ModeOfFollowUp: data.ModeOfFollowUp, FollowUp: Followups,
                                    InsuranceCompanyName: updatedinsurancecompany,
                                    PlanName: TasksAssignedplanname != "" ? TasksAssignedplanname : "Not Available",
                                    //AssignedToId: $scope.users.filter(function (users) { return users.CDUserID == data.AssignedToId })[0].AuthenicateUserId, AssignedTo: $scope.users.filter(function (users) { return users.CDUserID == data.AssignedToId })[0].UserName, HospitalID: data.HospitalID,
                                    AssignedToId: Assignedtoid, AssignedTo: AssignedTo, HospitalID: data.HospitalID,
                                    AssignedById: data.AssignedById,
                                    Hospital: taskassignedhospital, Notes: data.Notes, ModifiedDate: data.LastModifiedDate,
                                    TaskTrackerHistories: data.TaskTrackerHistories,
                                    TabID: tabID,
                                    SubSectionID: sectionID,
                                    CompleteStatus: "Open",
                                    LastUpdatedBy: data.LastUpdatedBy,
                                    daysleft: data.daysleft
                                };

                                //var kjhkh = $.grep($scope.TasksAssigned, function (e) { return e.TaskTrackerId == data.TaskTrackerId; })[0];
                                var task = $scope.TasksAssigned.indexOf($scope.TasksAssigned.filter(function (datas) { return datas.TaskTrackerId == data.TaskTrackerId })[0]);;
                                if (task != null && task != -1 && AllTasksforedittask.AssignedTo != currentcduserdata.cdUser.EmailId) {
                                    $scope.TasksAssigned.splice(task, 1);
                                }
                                var remtask = $scope.RemainingTasks.indexOf($scope.RemainingTasks.filter(function (datas) { return datas.TaskTrackerId == data.TaskTrackerId })[0]);;
                                editedTask = angular.copy(AllTasksforedittask);
                                $scope.RemainingTasks.splice(remtask, 1);
                                $scope.RemainingTasks.splice(0, 0, AllTasksforedittask);
                                ctrl.temp = $scope.RemainingTasks;
                                // ctrl.callServer();
                                //$rootScope.$broadcast("evn", $scope.DailyTasks);
                            }
                            $rootScope.$broadcast('AllTasks');

                        }
                        //else if (($scope.VisibilityControl = "editViewforTab") && ($rootScope.tabNames == 'TasksAssigne')) {

                        //    if (data.AssignedToId == currentcduserdata.cdUser.CDUserID) {
                        //        for (var i in $scope.DailyTasks) {
                        //            if ($scope.DailyTasks[i].TaskTrackerId == data.TaskTrackerId) {
                        //                $scope.DailyTasks.splice(i, 1);
                        //            }
                        //        }
                        //        //$scope.TasksAssigned.splice($scope.TasksAssigned.indexOf($scope.TasksAssigned.filter(function (datas) { return datas.TaskTrackerId == data.TaskTrackerId })[0]), 1);
                        //        var TasksAssignedforedittask = {
                        //            TaskTrackerId: data.TaskTrackerId, ProfileID: data.ProfileID,
                        //            //ProviderName: $scope.Providers.filter(function (Providers) { return Providers.ProfileId == data.ProfileID })[0].Name == 'undefined' ? "" : $scope.Providers.filter(function (Providers) { return Providers.ProfileId == data.ProfileID })[0].Name, SubSectionName: data.SubSectionName,
                        //            ProviderName: Providername, SubSectionName: data.SubSectionName,
                        //            Subject: data.Subject, NextFollowUpDate: updateddate,
                        //            ModeOfFollowUp: data.ModeOfFollowUp, FollowUp: Followups,
                        //            //InsuranceCompanyName: updatedinsurancecompany,
                        //            InsuranceCompanyName: updatedinsurancecompany,
                        //            //AssignedToId: $scope.users.filter(function (users) { return users.CDUserID == data.AssignedToId })[0].AuthenicateUserId, AssignedTo: $scope.users.filter(function (users) { return users.CDUserID == data.AssignedToId })[0].UserName, HospitalID: data.HospitalID,
                        //            AssignedToId: Assignedtoid, AssignedTo: AssignedTo, HospitalID: data.HospitalID,
                        //            AssignedById: data.AssignedById,
                        //            Hospital: taskassignedhospital, Notes: data.Notes, ModifiedDate: data.LastModifiedDate,
                        //            TabID: tabID,
                        //            SubSectionID: sectionID,
                        //            CompleteStatus: "Open",
                        //            LastUpdatedBy: data.LastUpdatedBy,
                        //            daysleft: data.daysleft
                        //        };
                        //        $scope.TasksAssigned.splice(0, 0, TasksAssignedforedittask);
                        //        //ctrl.temp = $scope.DailyTasks;
                        //        //$rootScope.$broadcast("evnt", $scope.TasksAssigned);
                        //        $rootScope.$broadcast("evn", $scope.DailyTasks);
                        //    }
                        //    if (data.AssignedToId != currentcduserdata.cdUser.CDUserID) {
                        //        for (var i in $scope.DailyTasks) {
                        //            if ($scope.DailyTasks[i].TaskTrackerId == data.TaskTrackerId) {
                        //                $scope.DailyTasks.splice(i, 1);
                        //            }
                        //        }
                        //        //$scope.DailyTasks.splice($scope.DailyTasks.indexOf($scope.DailyTasks.filter(function (DailyTasks) { return DailyTasks.TaskTrackerId == data.TaskTrackerId })[0]), 1);
                        //        var Assignedtoothersforedittask = {
                        //            TaskTrackerId: data.TaskTrackerId, ProfileID: data.ProfileID,
                        //            //ProviderName: $scope.Providers.filter(function (Providers) { return Providers.ProfileId == data.ProfileID })[0].Name == 'undefined' ? "" : $scope.Providers.filter(function (Providers) { return Providers.ProfileId == data.ProfileID })[0].Name, SubSectionName: data.SubSectionName,
                        //            ProviderName: Providername, SubSectionName: data.SubSectionName,
                        //            Subject: data.Subject, NextFollowUpDate: updateddate,
                        //            ModeOfFollowUp: data.ModeOfFollowUp, FollowUp: Followups,
                        //            InsuranceCompanyName: updatedinsurancecompany,
                        //            //AssignedToId: $scope.users.filter(function (users) { return users.CDUserID == data.AssignedToId })[0].AuthenicateUserId, AssignedTo: $scope.users.filter(function (users) { return users.CDUserID == data.AssignedToId })[0].UserName, HospitalID: data.HospitalID,
                        //            AssignedToId: Assignedtoid, AssignedTo: AssignedTo, HospitalID: data.HospitalID,
                        //            AssignedById: data.AssignedById,
                        //            Hospital: taskassignedhospital, Notes: data.Notes, ModifiedDate: data.LastModifiedDate,
                        //            TaskTrackerHistories: data.TaskTrackerHistories,
                        //            TabID: tabID,
                        //            SubSectionID: sectionID,
                        //            CompleteStatus: "Open",
                        //            LastUpdatedBy: data.LastUpdatedBy,
                        //            daysleft: data.daysleft
                        //        };
                        //        $scope.DailyTasks.splice(0, 0, Assignedtoothersforedittask);
                        //        $rootScope.$broadcast("evn", $scope.DailyTasks);
                        //    }
                        //}
                        $scope.canceleditupdate();
                        messageAlertEngine.callAlertMessage("successfullySaved", "Task updated successfully", "success", true);
                        $scope.progressbar = true;
                        $scope.showDetailViewforTab1(editedTask);
                        //$scope.editedTask = data;
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
            else {
                $scope.flag = false;
                $scope.progressbar = true;
            }
            return d1.promise();
            ctrl.callServer($scope.t);
            // $formData.reset();
        }

        else {
            $scope.errormessage = true;
        }
        $location.$$hash = ''
    }
    $scope.removeTask = function () {
        var TaskData = JSON.parse(localStorage.getItem("TaskReminders"));
        for (var task = 0; task < TaskData.length; task++) {
            var remainderstring = "TaskTrackerId" + ":" + $scope.TemporaryTask.TaskTrackerId;
            if ((TaskData[task].ReminderInfo).includes($scope.TemporaryTask.TaskTrackerId)) {
                $scope.remainderid = TaskData[task].TaskReminderID;
            }
        }
        DismissSingleTaskforremoveTask($scope.remainderid);
        $scope.progressbar = false;
        var closed = [];
        $http.post(rootDir + '/TaskTracker/Inactivetask?taskTrackerID=' + $scope.TemporaryTask.TaskTrackerId).
            success(function (data, status, headers, config) {
                if (data == "true") {
                    messageAlertEngine.callAlertMessage("successfullySaved", "Task closed successfully", "success", true);
                }
                for (var j in $scope.TasksAssigned) {
                    if ($scope.TasksAssigned[j].TaskTrackerId == $scope.TemporaryTask.TaskTrackerId) {
                        $scope.TasksAssigned[j].CompleteStatus = "CLOSED";
                        if ($rootScope.tabNames != 'DailyTask') {
                            $scope.TasksAssigned.splice(j, 1);
                        }
                    }
                }
                if ($rootScope.tabNames == 'DailyTask') {
                    if ($scope.TasksAssigned.filter(function (tasks) { return tasks.TaskTrackerId == $scope.TemporaryTask.TaskTrackerId })[0].SelectStatus != undefined)
                        $scope.TasksAssigned.filter(function (tasks) { return tasks.TaskTrackerId == $scope.TemporaryTask.TaskTrackerId })[0].SelectStatus = false;
                    closed = $scope.TasksAssigned.splice($scope.TasksAssigned.indexOf($scope.TasksAssigned.filter(function (tasks) { return tasks.TaskTrackerId == $scope.TemporaryTask.TaskTrackerId })[0]), 1);
                    var tempclosed = $scope.RemainingTasks.splice($scope.RemainingTasks.indexOf($scope.RemainingTasks.filter(function (tasks) { return tasks.TaskTrackerId == $scope.TemporaryTask.TaskTrackerId })[0]), 1);
                    $scope.ClosedTasks.push(closed[0]);
                }
                //$scope.ClosedTasks.push($scope.TasksAssigned.splice($scope.TasksAssigned.indexOf($scope.TasksAssigned.filter(function (tasks) { return tasks.TaskTrackerId == $scope.TemporaryTask.TaskTrackerId })[0]), 1));
                //$rootScope.$broadcast('DailyTasks');
                //for (var i in $scope.DailyTasks) {
                //    if ($scope.DailyTasks[i].TaskTrackerId == $scope.TemporaryTask.TaskTrackerId) {
                //        $scope.DailyTasks[i].CompleteStatus = "Closed";
                //    }
                //}
                for (var i in $scope.RemainingTasks) {
                    if ($scope.RemainingTasks[i].TaskTrackerId == $scope.TemporaryTask.TaskTrackerId) {
                        $scope.RemainingTasks[i].CompleteStatus = "CLOSED";
                    }
                }

                if ($rootScope.tabNames == 'DailyTask') {
                    //ctrl.temp = $scope.TasksAssigned;
                    //$rootScope.trackerItems = $scope.TasksAssigned;
                    $rootScope.$broadcast('DailyTasks');
                }
                //else if ($rootScope.tabNames == 'TasksAssigne') {
                //    closed = $scope.DailyTasks.splice($scope.DailyTasks.indexOf($scope.DailyTasks.filter(function (tasks) { return tasks.TaskTrackerId == $scope.TemporaryTask.TaskTrackerId })[0]), 1);
                //    $scope.ClosedTasks.push(closed[0]);
                //    //ctrl.temp = $scope.DailyTasks;
                //    //$rootScope.trackerItems = $scope.DailyTasks;
                //    $rootScope.$broadcast('TasksAssigned');
                //}
                else if ($rootScope.tabNames == 'AllTask') {
                    if ($scope.RemainingTasks.filter(function (tasks) { return tasks.TaskTrackerId == $scope.TemporaryTask.TaskTrackerId })[0].SelectStatus != undefined)
                        $scope.RemainingTasks.filter(function (tasks) { return tasks.TaskTrackerId == $scope.TemporaryTask.TaskTrackerId })[0].SelectStatus = false;

                    closed = $scope.RemainingTasks.splice($scope.RemainingTasks.indexOf($scope.RemainingTasks.filter(function (tasks) { return tasks.TaskTrackerId == $scope.TemporaryTask.TaskTrackerId })[0]), 1);
                    $scope.ClosedTasks.push(closed[0]);
                    //ctrl.temp = $scope.RemainingTasks;
                    //$rootScope.trackerItems = $scope.RemainingTasks;
                    $rootScope.$broadcast('AllTasks');
                }
                //$scope.DailyTasks.splice($scope.DailyTasks.indexOf($scope.DailyTasks.filter(function (DailyTasks) { return DailyTasks.TaskTrackerId == $scope.TemporaryTask.TaskTrackerId })[0]), 1);
            }).


            error(function (data, status, headers, config) {
                messageAlertEngine.callAlertMessage("errorInitiated", "Please try after sometime !!!!", "danger", true);
            });
        $scope.progressbar = true;
        $scope.selTask = [];
        $scope.showOnClose = false;
    }




    $scope.reopenTask = function () {
        $scope.progressbar = false;
        var opened = [];
        $http.post(rootDir + '/TaskTracker/Reactivetask?taskTrackerID=' + $scope.TemporaryTask.TaskTrackerId).
            success(function (data, status, headers, config) {
                if (data == "true") {
                    messageAlertEngine.callAlertMessage("successfullySaved", "Task reopened successfully", "success", true);
                }

                if ($rootScope.tabNames == 'CloseTasks') {
                    opened = $scope.ClosedTasks.splice($scope.ClosedTasks.indexOf($scope.ClosedTasks.filter(function (tasks) { return tasks.TaskTrackerId == $scope.TemporaryTask.TaskTrackerId })[0]), 1);
                    //Added By Manideep Innamuri
                    opened[0].AssignedTo = opened[0].LastUpdatedBy;
                    $scope.TasksAssigned.push(opened[0]);
                    $scope.RemainingTasks.push(opened[0]);
                    for (var j in $scope.TasksAssigned) {
                        if ($scope.TasksAssigned[j].TaskTrackerId == $scope.TemporaryTask.TaskTrackerId) {
                            $scope.TasksAssigned[j].CompleteStatus = "Open";
                        }
                    }
                    $rootScope.$broadcast('ClosedTasks');
                }

                //for (var i in $scope.DailyTasks) {
                //    if ($scope.DailyTasks[i].TaskTrackerId == $scope.TemporaryTask.TaskTrackerId) {
                //        $scope.DailyTasks[i].CompleteStatus = "Open";
                //    }
                //}
                for (var i in $scope.RemainingTasks) {
                    if ($scope.RemainingTasks[i].TaskTrackerId == $scope.TemporaryTask.TaskTrackerId) {
                        $scope.RemainingTasks[i].CompleteStatus = "Open";
                    }
                }

                if ($rootScope.tabNames == 'DailyTask') {
                    //ctrl.temp = $scope.TasksAssigned;
                    //$rootScope.trackerItems = $scope.TasksAssigned;
                    $rootScope.$broadcast('DailyTasks');
                }
                //else if ($rootScope.tabNames == 'TasksAssigne') {
                //    //ctrl.temp = $scope.DailyTasks;
                //    //$rootScope.trackerItems = $scope.DailyTasks;
                //    $rootScope.$broadcast('TasksAssigned');
                //}
                else if ($rootScope.tabNames == 'AllTask') {
                    //ctrl.temp = $scope.RemainingTasks;
                    //$rootScope.trackerItems = $scope.RemainingTasks;
                    $rootScope.$broadcast('AllTasks');
                }

                //$rootScope.$broadcast("evnt", $scope.TasksAssigned);
                //$rootScope.$broadcast("evn", $scope.DailyTasks);
                //$scope.DailyTasks.splice($scope.DailyTasks.indexOf($scope.DailyTasks.filter(function (DailyTasks) { return DailyTasks.TaskTrackerId == $scope.TemporaryTask.TaskTrackerId })[0]), 1);
            }).


            error(function (data, status, headers, config) {
                messageAlertEngine.callAlertMessage("errorInitiated", "Please try after sometime !!!!", "danger", true);
            });
        $scope.progressbar = true;
    }




    $scope.CloseTask = function () {
        var promise = $scope.editTask().then(function () {
            $scope.removeTask();
            $scope.detailViewfortab = false;
        });
    }
    ////////////////////////////
    //$scope.errormessageforsubject = false;
    //$scope.errormessageforfollowupdate = false;
    //To initiate Removal Confirmation Modal
    $scope.inactiveWarning = function (task) {
        //$scope.validationfunction = true;
        //$scope.remVal();
        //$scope.remnextfollowupVal($scope.followupdatevalue);
        //$scope.errormessageforfollowupdate = false;
        //if (task.ProviderName == null || task.ProviderName == "") {
        //    $scope.errormessageforprovider = true;
        //}
        //if ((task.SubSectionName == null || task.SubSectionName == "")) {
        //    $scope.errormessageforsubsection = true;
        //}
        //if (task.Subject == null || task.Subject == "") {
        //    $scope.errormessageforsubject = true;
        //}
        //if ((task.NextFollowUpDate == null || task.NextFollowUpDate == "" || task.NextFollowUpDate == undefined) && !$scope.errormessageforfollowupdate) {
        //    $scope.errormessageforfollowupdate = false;
        //    $scope.errormessageforfollowupdate = true;
        //}

        //if (task.ModeOfFollowUp.length == 0 || $scope.Followups.length == 4) {
        //    $scope.errormessage = true;
        //}
        //if (task.AssignedTo == null || task.AssignedTo == "") {
        //    $scope.errormessageforAssignedto = true;
        //}

        $scope.errormessage = false;
        $scope.errormessageforAssignedto = false;
        $scope.errormessageforprovider = false;
        $scope.errormessageforsubsection = false;
        $scope.errormessageforHospitalName = false;
        $scope.errormessageforInsurancecompany = false;
        var $formData;
        $formData = $('#newTaskFormDiv');
        var validationCount = 0;

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

        if (angular.isObject(task)) {
            $scope.TemporaryTask = {};
            $scope.TemporaryTask = angular.copy(task);
        }

        if ($scope.Followups.length == 4) {
            $scope.errormessage = true;
        }
        ResetFormForValidation($formData);
        validationStatus = $formData.valid();

        //if (!$scope.errormessageforprovider && !$scope.errormessageforsubsection && !$scope.errormessageforsubject && !$scope.errormessageforfollowupdate && !$scope.errormessage && !$scope.errormessageforAssignedto)
        if (validationCount == 0 && !$scope.errormessage && validationStatus)
            $('#inactiveWarningModal').modal();

        //$scope.validationfunction = false;
    }
    $scope.QuickRemove = function (task) {
        if (angular.isObject(task)) {
            $scope.TemporaryTask = {};
            $scope.TemporaryTask = angular.copy(task);
        }
        $('#inactiveWarningModal1').modal();
    }
    $scope.Quickreopen = function (task) {
        if (angular.isObject(task)) {
            $scope.TemporaryTask = {};
            $scope.TemporaryTask = angular.copy(task);
        }
        $('#reactiveWarningModal2').modal();
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
        $('#addButton').show();
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

    $scope.ConvertDateForTE = function (date) {
        var followupdateforupdatetask = "";
        if (date !== null || date !== "") {
            var date5 = date.split('T');
            var dates1 = date5;
            var date5 = date5[0].split('-');
            followupdateforupdatetask = date5[2] + "/" + date5[1] + "/" + date5[0] + " " + dates1[1];
        }
        return followupdateforupdatetask;
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
        $scope.showSetReminder = false;
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
        $scope.detailViewfortab = false;
        $scope.VisibilityControl = "";
        //$scope.editViewforTab1 = false;
        $scope.detailViewfortab2 = false;
        $scope.detailViewfortab3 = false;
        $scope.progressbar = true;
        $scope.TableView = true;
        $scope.ResetRemData();

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


    //$scope.showDetailViewforTab1 = function (task) {
    //    $scope.TableView = false;
    //    $scope.detailViewfortab = true;
    //    $scope.viewTemp = [];
    //    $scope.viewTempLatest = [];

    //    $scope.taskView = task;

    //    if (task.Notes.indexOf('-') != -1) {
    //        var dateTimeForNote = task.Notes.split('-');
    //        dateTimeForNote = dateTimeForNote[1] + " " + ((dateTimeForNote.indexOf('AM') != -1) ? dateTimeForNote[2].split('AM')[0] : dateTimeForNote[2].split('PM')[0]);
    //    }

    //    var authID = $scope.Tasks.filter(function (filtertask) { return filtertask.TaskTrackerId == task.TaskTrackerId })[0].AssignedBy.AuthenicateUserId;

    //    for (var i = 0; i < $scope.LoginUsers.length; i++) {
    //        if ($scope.LoginUsers[i].Id == authID) {
    //            NoteBy = $scope.LoginUsers[i].UserName;
    //        }
    //    }
    //    if ((!task.hasOwnProperty('TaskTrackerHistories')) || (task.TaskTrackerHistories == null) || (task.TaskTrackerHistories.length == 0)) {
    //        $scope.viewTempLatest.push({ stamp: task.Notes, number: 0, modifiedDate: dateTimeForNote, By: NoteBy });
    //    }
    //    else {
    //        for (var t = 0; t < task.TaskTrackerHistories.length; t++) {
    //            $scope.viewTemp.push({ stamp: task.TaskTrackerHistories[t].Notes, number: t, modifiedDate: $scope.ConvertDateForNotes(task.TaskTrackerHistories[t].LastModifiedDate), By: task.TaskTrackerHistories[t].LastUpdatedBy });
    //        }
    //        $scope.viewTempLatest.push({ stamp: task.Notes, number: 0, modifiedDate: dateTimeForNote, By: NoteBy });
    //    }


    //}
    $rootScope.tabname = "";

    $scope.showDetailViewforTab1 = function (task) {
        if (task.tasktrackerhistories == undefined) {
            //task.TaskTrackerHistories = $rootScope.tasktrackerhistories;
        }
        $scope.showOnClose = false;
        $scope.TableView = false;
        $scope.detailViewfortab = true;
        $scope.viewTemp = [];
        $scope.viewTemp1 = [];
        $scope.viewTempLatest = [];
        var tempAuthId = '';
        $scope.selectedTask = task;
        $scope.taskView = task;

        if ($rootScope.tabNames == 'HistoryTask') {
            $rootScope.tabname = "History"
            var dateTimeForNote = [];
            for (var h in task.Notes) {
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
                    dateTimeForNote = dateTimeForNote[0] + " " + $scope.changeTimeTo24Hr(dateTimeForNote[1]);
                }
            }
        }

        if ($rootScope.tabNames == 'DailyTask') {
            var authID = $scope.TasksAssigned.filter(function (filterTask) { return filterTask.TaskTrackerId == task.TaskTrackerId })[0].AssignedById;
        }
        //else if ($rootScope.tabNames == 'TasksAssigne') {
        //    var authID = $scope.DailyTasks.filter(function (filterTask) { return filterTask.TaskTrackerId == task.TaskTrackerId })[0].AssignedById;
        //}
        else if ($rootScope.tabNames == 'AllTask') {
            var authID = $scope.RemainingTasks.filter(function (filterTask) { return filterTask.TaskTrackerId == task.TaskTrackerId })[0].AssignedById;
        }
        // var authID = $scope.Tasks.filter(function (filterTask) { return filterTask.TaskTrackerId == task.TaskTrackerId })[0].AssignedBy.AuthenicateUserId;

        //for (var i = 0; i < $scope.LoginUsers.length; i++) {
        //    if ($scope.LoginUsers[i].Id == authID) {
        //        NoteBy = $scope.LoginUsers[i].UserName;
        //    }
        //}

        //for (var j in $scope.CDUsers) {
        //    if ($scope.CDUsers[j].CDUserID == authID) {
        //        tempAuthId = $scope.CDUsers[j].AuthenicateUserId;
        //    }
        //}

        //for (var i in $scope.LoginUsers) {
        //    if ($scope.LoginUsers[i].Id == tempAuthId) {
        //        NoteBy = $scope.LoginUsers[i].UserName;
        //    }
        //}

        //for (var t = 0; t < task.TaskTrackerHistories.length; t++) {
        //    for (var j in $scope.CDUsers) {
        //        if ($scope.CDUsers[j].CDUserID == task.TaskTrackerHistories[t].AssignedByCCOID) {
        //            task.TaskTrackerHistories[t].AssignedByCCOAuthID = $scope.CDUsers[j].AuthenicateUserId;
        //        }
        //    }

        //    for (var i in $scope.LoginUsers) {
        //        if ($scope.LoginUsers[i].Id == task.TaskTrackerHistories[t].AssignedByCCOAuthID) {
        //            task.TaskTrackerHistories[t].NoteBy = $scope.LoginUsers[i].UserName;
        //        }
        //    }
        //}
        //for (var t = 0; t < task.TaskTrackerHistories.length; t++) {
        //    if ((task.TaskTrackerHistories[t].Notes!=null)&&(task.TaskTrackerHistories[t].Notes.indexOf('-') != -1)) {
        //        task.TaskTrackerHistories[t].NoteBy = task.TaskTrackerHistories[t].Notes.split('-')[0];
        //    }
        //}

        if (task.TaskTrackerHistories == undefined) {
            //task.TaskTrackerHistories = $rootScope.tasktrackerhistories;
        }
        if ($rootScope.tabNames == 'HistoryTask') {
            for (var h in task.Notes) {
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
        }
        else {
            if ((!task.hasOwnProperty('TaskTrackerHistories')) || (task.TaskTrackerHistories == null) || (task.TaskTrackerHistories.length == 0)) {
                if (task.Notes != null) {
                    if (task.Notes.indexOf('~') != -1) {
                        var notes = task.Notes.split('~');
                        notes = notes[3];

                    }
                }


                //$scope.viewTemp.push({ stamp: task.Notes, number: 0, modifiedDate: moment(dateTimeForNote).format('MM/DD/YYYY hh:mm a'), By: task.LastUpdatedBy });
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
                    //$scope.viewTemp.push({ stamp: task.TaskTrackerHistories[t].Notes, number: t, modifiedDate: moment(task.TaskTrackerHistories[t].LastModifiedDate).format('MM/DD/YYYY hh:mm a'), By: task.TaskTrackerHistories[t].LastUpdatedBy });
                    $scope.viewTemp.push({ stamp: notes, number: t, modifiedDate: moment(dateTimeForHistory).format('MM/DD/YYYY hh:mm a'), By: task.TaskTrackerHistories[t].LastUpdatedBy });
                }
                if (task.Notes != null) {
                    if (task.Notes.indexOf('~') != -1) {
                        var notes = task.Notes.split('~');
                        notes = notes[3];

                    }
                }


                //$scope.viewTemp.push({ stamp: task.Notes, number: 0, modifiedDate: moment(dateTimeForNote).format('MM/DD/YYYY hh:mm a'), By: task.LastUpdatedBy });
                $scope.viewTemp.push({ stamp: notes, number: 0, modifiedDate: moment(dateTimeForNote).format('MM/DD/YYYY hh:mm a'), By: task.LastUpdatedBy });
            }
        }

        //var historyindex = $scope.TaskHistory.indexOf(task);
        //for (var t = 0; t <= historyindex; t++) {
        //    $scope.viewTemp.push({ stamp: $scope.TaskHistory[t].Notes, number: t, modifiedDate: moment($scope.TaskHistory[t].LastModifiedDate).format('MM/DD/YYYY hh:mm a'), By: $scope.TaskHistory[t].LastUpdatedBy });
        //}
        $scope.viewTemp = $scope.viewTemp.reverse();
        for (i = 0; i < $scope.viewTemp.length; i++) {

            if ($scope.viewTemp[i].stamp == undefined || $scope.viewTemp[i].stamp == " ") {

            }
            else {
                $scope.viewTemp1.push({ stamp: $scope.viewTemp[i].stamp, number: $scope.viewTemp[i].number, modifiedDate: $scope.viewTemp[i].modifiedDate, By: $scope.viewTemp[i].By });
            }
        }

    }
    $scope.cancelViewforTab1 = function () {
        $scope.CancelReminder();
        if ($rootScope.deepu) {
            $rootScope.tabNames = 'DailyTask';
        }
        $scope.selTask = [];
        if ($rootScope.tabNames == "HistoryTask")
            $scope.showOnClose = false;
        else
            $scope.showOnClose = true;
        $scope.detailViewfortab = false;
        $scope.TableView = true;
        //if ($rootScope.tabNames == 'DailyTask') {
        //    $rootScope.$broadcast('DailyTasks');
        //}
        //else if ($rootScope.tabNames == 'TasksAssigne') {
        //    $rootScope.$broadcast('TasksAssigned');
        //}
        if ($rootScope.tabNames == 'CloseTasks') {
            $scope.showOnClose = false;
        }
        if ($rootScope.tabNames == 'DailyTask') {
            $("a[id='tabs1']").parent("li").tab('show')
            //$rootScope.$broadcast('DailyTasks');
        }
        //$scope.t.search.predicateObject = $scope.tableDailyValue.search.predicateObject;
        //ctrl.callServer($scope.t);
    }

    $scope.cancelViewforTabhistory = function () {
        $scope.showOnClose = false;
        $scope.detailViewfortab = false;
        $scope.TableView = true;
        //if ($rootScope.tabNames == 'DailyTask') {
        //    $rootScope.$broadcast('DailyTasks');
        //}
        //else if ($rootScope.tabNames == 'TasksAssigne') {
        //    $rootScope.$broadcast('TasksAssigned');
        //}
        if ($rootScope.tabNames == 'CloseTasks') {
            $scope.showOnClose = false;
        }
        if ($rootScope.tabNames == 'DailyTask') {
            $("a[id='tabs1']").parent("li").tab('show')
            //$rootScope.$broadcast('DailyTasks');
        }
        //$scope.t.search.predicateObject = $scope.tableDailyValue.search.predicateObject;
        //ctrl.callServer($scope.t);
    }

    $scope.showEditViewforTab1 = function (task) {
        $('#addButton').hide();
        $scope.showOnClose = false;
        $scope.errormessage = false;
        $scope.errormessageforAssignedto = false;
        $scope.errormessageforprovider = false;
        $scope.errormessageforsubsection = false;
        $scope.errormessageforHospitalName = false;
        $scope.errormessageforInsurancecompany = false;
        $scope.visible = 'edit';
        $scope.Followups = angular.copy(TempFollowupHelper);
        $scope.Tempfollowup = [];
        $scope.Tempfollowup1 = "";

        //if (task.PlanName == "Not Available")
        //    task.PlanName = "";

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
        //$scope.TempInsuranceCompanies = [];
        //$scope.TempInsuranceCompanies1 = "";
        //if (task.InsuranceCompanyName != "" && task.InsuranceCompanyName != null) {
        //    $scope.TempInsuranceCompanies = angular.copy(task.InsuranceCompanyName);
        //    $scope.TempInsuranceCompanies1 = JSON.stringify(angular.copy(task.InsuranceCompanyName));
        //    for (var i in $scope.TempInsuranceCompanies) {
        //        for (var j in $scope.InsuranceCompanies) {
        //            if ($scope.InsuranceCompanies[j].CompanyName == $scope.TempInsuranceCompanies[i]) {
        //                $scope.InsuranceCompanies.splice(j, 1);
        //            }
        //        }
        //    }
        //}
        $scope.TableView = false;
        $scope.editViewforTab1 = true;
        $scope.task = angular.copy(task);
        currentTask = angular.copy(task);
    }

    $scope.cancelForRemoveTask = function () {
        if ($scope.flag) {
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
            $scope.detailViewfortab = false;
            $scope.detailView = false;
            $scope.TableView = true;
        }
        else {
            $scope.detailViewfortab = false;
        }
    }


    $scope.cancelEditViewforTab = function () {
        $scope.editViewforTab1 = false;
        $scope.editViewforTab3 = false;
        $scope.editViewforTab2 = false;
    }
    $scope.cancelEditViewforTab1 = function () {
        $scope.CancelReminder();
        $('#addButton').show();
        $location.$$hash = ''
        //var hash = location.hash.replace('#', '');
        //if (hash != '') {            
        //    location.hash = '';
        //}
        //location.hash = '';
        //event.preventDefault();        

        $scope.NoteTime = false;
        $scope.notetask = false;
        $scope.errormessage = false;
        $scope.errormessageforAssignedto = false;
        $scope.errormessageforprovider = false;
        $scope.errormessageforsubsection = false;
        $scope.errormessageforHospitalName = false;
        $scope.errormessageforInsurancecompany = false;
        $scope.editViewforTab1 = false;
        $scope.editViewforTab3 = false;
        $scope.editViewforTab2 = false;
        $scope.VisibilityControl = "";
        $scope.progressbar = false;
        $scope.Tempfollowup = [];
        $scope.showOnClose = true;
        $scope.task = angular.copy(currentTask);
        for (var i in $scope.RemainingTasks) {
            if (currentTask.TaskTrackerId == $scope.RemainingTasks[i].TaskTrackerId) {
                $scope.RemainingTasks[i] = angular.copy(currentTask);
            }
        }
        $scope.Followup = angular.copy($scope.FollowupHelper);
        //$scope.TempInsuranceCompanies = [];
        //$scope.InsuranceCompanies = angular.copy($scope.InsuranceCompanyHelper);
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
        $scope.detailViewfortab = false;
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


    $scope.hideDropDown = function () {
        $(".ProviderTypeSelectAutoList").hide();
    }

    //$scope.showeditView = function (task, editviewfortab) {
    //    $('#addButton').hide();
    //    $scope.NoteTime = true;
    //    $scope.errormessage = false;
    //    $scope.visible = 'edit';
    //    $scope.errormessageforAssignedto = false;
    //    $scope.errormessageforprovider = false;
    //    $scope.errormessageforsubsection = false;
    //    $scope.errormessageforHospitalName = false;
    //    $scope.errormessageforInsurancecompany = false;
    //    $scope.Followups = angular.copy(TempFollowupHelper);
    //    $scope.Tempfollowup = [];
    //    $scope.Tempfollowup1 = "";
    //    $scope.temp = [];
    //    var temp = [];
    //    $scope.TempNotes = '';
    //    var tempforscroll = '';
    //    var NoteBy = '';

    //    if (task.ModeOfFollowUp != "" && task.ModeOfFollowUp != null) {
    //        $scope.Tempfollowup = angular.copy(task.ModeOfFollowUp);
    //        $scope.Tempfollowup1 = JSON.stringify(angular.copy(task.ModeOfFollowUp));
    //        for (var i in $scope.Tempfollowup) {
    //            for (var j in $scope.Followups)
    //                if ($scope.Tempfollowup[i].Name == $scope.Followups[j].Name) {
    //                    $scope.Followups.splice(j, 1);
    //                }
    //        }
    //    }
    //    else {
    //        $scope.Followups = angular.copy($scope.FollowupHelper);
    //        $scope.Tempfollowup = [];
    //    }
    //    //temp = $scope.Tasks.filter(function (items) { return items.TaskTrackerId == task.TaskTrackerId })[0];
    //    //if (task.TaskTrackerHistories.length == 0) {
    //    //    $scope.temp.push(task.Notes);
    //    //}
    //    //else {
    //    //    for (var t = 0; t < task.TaskTrackerHistories.length; t++) {
    //    //        $scope.temp.push(task.TaskTrackerHistories[t].Notes);
    //    //    }
    //    //    $scope.temp.push(task.Notes);
    //    //}
    //    var dateTimeForNote = task.Notes.split('-');

    //    if (task.hasOwnProperty('TaskTrackerHistories') && (task.TaskTrackerHistories != null) && (task.TaskTrackerHistories.length != 0)) {
    //        var dateTimeForNote1 = task.TaskTrackerHistories[task.TaskTrackerHistories.length - 1].Notes.split('-');
    //        dateTimeForNote1 = dateTimeForNote1[1] + " " + $scope.changeTimeTo24Hr(dateTimeForNote[2]);
    //    }

    //    dateTimeForNote = dateTimeForNote[1] + " " + $scope.changeTimeTo24Hr(dateTimeForNote[2]);

    //    var authID = $scope.Tasks.filter(function (i) { return i.TaskTrackerId == 4 })[0].AssignedBy.AuthenicateUserId;       

    //    for (var i = 0; i < $scope.LoginUsers.length; i++) {            
    //            if ($scope.LoginUsers[i].Id == authID) {
    //                NoteBy = $scope.LoginUsers[i].UserName;
    //            }                            
    //    }

    //    if ((task.TaskTrackerHistories == null)||(task.TaskTrackerHistories.length == 0)) {
    //        $scope.temp.push({ stamp: task.Notes, number: 0, modifiedDate: dateTimeForNote, By: NoteBy });
    //    }
    //    else {
    //        for (var t = 0; t < task.TaskTrackerHistories.length-1; t++) {
    //            $scope.temp.push({ stamp: task.TaskTrackerHistories[t].Notes, number: t, modifiedDate: $scope.ConvertDateForNotes(task.TaskTrackerHistories[t].LastModifiedDate), By: task.TaskTrackerHistories[t].LastUpdatedBy });
    //        }
    //        $scope.temp.push({ stamp: task.TaskTrackerHistories[task.TaskTrackerHistories.length - 1].Notes, number: t, modifiedDate: dateTimeForNote1, By: task.TaskTrackerHistories[task.TaskTrackerHistories.length - 1].LastUpdatedBy });
    //        $scope.temp.push({ stamp: task.Notes, number: 0, modifiedDate: dateTimeForNote, By: NoteBy });
    //    }
    //    $scope.TableView = false;
    //    $scope.VisibilityControl = editviewfortab;
    //    currentTask = angular.copy(task);
    //    $scope.task = angular.copy(task);
    //    $scope.task.Notes = '';

    //    var rand = '';
    //    var date = Date();
    //    var Time = date.split(" ")[4];
    //    var AMPM = $scope.changeTimeAmPm(Time);
    //    date = new Date();
    //    $scope.TempNotes = $scope.ProviderNameforNotes + " - " + (date.getMonth() + 1) + "/" + date.getDate() + "/" + date.getFullYear() + " - " + AMPM;
    //    //$scope.temp.push($scope.ProviderNameforNotes + " - " + (date.getMonth() + 1) + "/" + date.getDate() + "/" + date.getFullYear() + " - " + AMPM);
    //    //$scope.temp.push({ stamp: $scope.ProviderNameforNotes + " - " + (date.getMonth() + 1) + "/" + date.getDate() + "/" + date.getFullYear() + " - " + AMPM, number: $scope.temp.length });
    //    //tempforscroll = $scope.temp[$scope.temp.length - 1];
    //    //$location.hash('anchor' + tempforscroll.number);
    //    //$anchorScroll();
    //    //history.replaceState({}, '', '/');
    //    //$location.$$hash = ''
    //}

    //$scope.validationfunction = false;
    //$scope.remVal = function () {
    //    if ($scope.validationfunction) {
    //        if ($('#TempSUb').val().length != 0) {
    //            $scope.errormessageforsubject = false;
    //        }
    //        else {
    //            $scope.subjval = "";
    //            $scope.errormessageforsubject = true;
    //        }
    //    }
    //}

    //$scope.followupdatevalue = "";
    //$scope.remnextfollowupVal = function (NextFollowUpDate) {
    //    $scope.followupdatevalue = NextFollowUpDate;
    //    if ($scope.validationfunction) {
    //        if (($('#Tempfollowup').val().length != 0 && $('#Tempfollowup').val() != "") || (NextFollowUpDate != "" && NextFollowUpDate != undefined)) {
    //            $scope.errormessageforfollowupdate = false;

    //        }
    //        else {
    //            $scope.validation = "";
    //            $scope.errormessageforfollowupdate = true;
    //        }
    //    }
    //}

    $scope.closenotesDiv = function () {
        $("#HospitalList").hide();
        $scope.hidenotesdiv = true;
    }

    $scope.result = {};
    $scope.showeditView = function (task, editviewfortab) {
        $scope.result = $.grep($scope.CDUsers, function (e) { return e.AuthenicateUserId == task.AssignedToId; });
        //if (task.TaskTrackerHistories == undefined)
        //{
        //    $scope.showDetailViewforTab1(task);
        //    $rootScope.break = true;
        //}
        //$rootScope.tasktrackerhistories = task.TaskTrackerHistories;        
        //$scope.validationfunction = false;
        //$scope.errormessageforsubject = false;
        //$scope.errormessageforfollowupdate = false;
        $('#addButton').hide();
        $scope.NoteTime = true;
        $scope.errormessage = false;
        $scope.visible = 'edit';
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
        $scope.showOnClose = false;
        $scope.Providers = angular.copy(AllProviders);
        if (task.PlanName == "Not Available")
            task.PlanName = "";

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
        //temp = $scope.Tasks.filter(function (items) { return items.TaskTrackerId == task.TaskTrackerId })[0];
        //if (task.TaskTrackerHistories.length == 0) {
        //    $scope.temp.push(task.Notes);
        //}
        //else {
        //    for (var t = 0; t < task.TaskTrackerHistories.length; t++) {
        //        $scope.temp.push(task.TaskTrackerHistories[t].Notes);
        //    }
        //    $scope.temp.push(task.Notes);
        //}
        if (task.Notes != null) {
            if (task.Notes.indexOf('~') != -1) {
                var dateTimeForNote = task.Notes.split('~');
                dateTimeForNote = dateTimeForNote[0] + " " + $scope.changeTimeTo24Hr(dateTimeForNote[1]);
            }
        }
        if (task.Notes) {

        }
        if (task.hasOwnProperty('TaskTrackerHistories') && (task.TaskTrackerHistories != null) && (task.TaskTrackerHistories.length != 0)) {
            if (task.TaskTrackerHistories[task.TaskTrackerHistories.length - 1].Notes != null) {
                var dateTimeForNote1 = task.TaskTrackerHistories[task.TaskTrackerHistories.length - 1].Notes.split('~');
                dateTimeForNote1 = dateTimeForNote1[0] + " " + $scope.changeTimeTo24Hr(dateTimeForNote1[1]);
            }
        }
        //if ($rootScope.deepu) {
        //    $rootScope.tabNames = 'AllTask';
        //}
        if ($rootScope.tabNames == 'DailyTask') {
            var authID = $scope.TasksAssigned.filter(function (filterTask) { return filterTask.TaskTrackerId == task.TaskTrackerId })[0].AssignedById;

            //else if ($rootScope.tabNames == 'TasksAssigne') {
            //    var authID = $scope.DailyTasks.filter(function (filterTask) { return filterTask.TaskTrackerId == task.TaskTrackerId })[0].AssignedById;
        }
        else if ($rootScope.tabNames == 'AllTask') {
            var authID = $scope.RemainingTasks.filter(function (filterTask) { return filterTask.TaskTrackerId == task.TaskTrackerId })[0].AssignedById;
        }
        //var authID = $scope.Tasks.filter(function (filterTask) { return filterTask.TaskTrackerId == task.TaskTrackerId })[0].AssignedBy.AuthenicateUserId;

        //for (var i = 0; i < $scope.LoginUsers.length; i++) {
        //    if ($scope.LoginUsers[i].Id == authID) {
        //        NoteBy = $scope.LoginUsers[i].UserName;
        //    }
        //}
        //var NoteByCCO = [];
        for (var j in $scope.InactiveCDUsers) {
            if ($scope.InactiveCDUsers[j].AuthenicateUserId == task.AssignedToId) {
                if ($scope.InactiveCDUsers[j].Status == "Inactive") {
                    task.AssignedTo = "";
                    task.AssignedToId = "";
                }
            }
        }
        //tempAuthID = currentcduserdata.cdUser.AuthenicateUserId;

        //for (var i in $scope.LoginUsers) {
        //    if ($scope.LoginUsers[i].Id == tempAuthId) {
        //        NoteBy = $scope.LoginUsers[i].UserName;
        //    }
        //}

        //for (var d = 0; d < task.TaskTrackerHistories.length - 1; d++) {
        //    for (var c in $scope.users) {
        //        if ($scope.users[c].CDUserID == task.TaskTrackerHistories[d].AssignedByCCOID) {
        //            NoteByCCO[d] = $scope.LoginUsers[i].UserName;
        //        }
        //    }
        //}
        //AssignedByCCOID
        //var authID = $scope.Tasks.filter(function (filtertasks) { return filtertasks.TaskTrackerId == task.TaskTrackerId })[0].AssignedBy.AuthenicateUserId;       

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
                    var notes = task.TaskTrackerHistories[t].Notes.split('~');
                    notes = notes[3];
                }
            }
            $scope.temp.push({ stamp: notes, number: t, modifiedDate: moment(dateTimeForNote1).format('MM/DD/YYYY hh:mm a'), By: task.TaskTrackerHistories[task.TaskTrackerHistories.length - 1].LastUpdatedBy });
            if (task.Notes != null || task.Notes != '') {
                if (task.Notes.indexOf('~') != -1) {
                    var notes = task.Notes.split('~');
                    notes = notes[3];
                }
            }

            $scope.temp.push({ stamp: notes, number: 0, modifiedDate: moment(dateTimeForNote).format('MM/DD/YYYY hh:mm a'), By: task.LastUpdatedBy });
        }
        $scope.temp = $scope.temp.reverse();
        var newindex = 0;
        for (i = 0; i < $scope.temp.length; i++) {

            if ($scope.temp[i].stamp == undefined || $scope.temp[i].stamp == " ") {

            }
            else {

                $scope.temp1.push({ stamp: $scope.temp[i].stamp, number: $scope.temp[i].number, modifiedDate: $scope.temp[i].modifiedDate, By: $scope.temp[i].By });

            }
        }

        $scope.TableView = false;
        $scope.VisibilityControl = editviewfortab;
        currentTask = angular.copy(task);
        $scope.task = angular.copy(task);
        $scope.task.Notes = '';

        var rand = '';
        var date = Date();
        var Time = date.split(" ")[4];
        var AMPM = $scope.changeTimeAmPm(Time);
        date = new Date();
        $scope.TempNotes = (date.getMonth() + 1) + "/" + date.getDate() + "/" + date.getFullYear() + "~" + AMPM + "~" + $scope.ProviderNameforNotes + "~";
        //$scope.temp.push($scope.ProviderNameforNotes + " - " + (date.getMonth() + 1) + "/" + date.getDate() + "/" + date.getFullYear() + " - " + AMPM);
        //$scope.temp.push({ stamp: $scope.ProviderNameforNotes + " - " + (date.getMonth() + 1) + "/" + date.getDate() + "/" + date.getFullYear() + " - " + AMPM, number: $scope.temp.length });
        //tempforscroll = $scope.temp[$scope.temp.length - 1];
        //$location.hash('anchor' + tempforscroll.number);
        //$anchorScroll();
        //history.replaceState({}, '', '/');
        //$location.$$hash = ''
        $scope.detailViewfortab = false;
    }
    $scope.initPop = function () {
        $('[data-toggle="popover"]').popover();
    };
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
    //////////Time Elapsed///////////////
    $scope.ConvertDateForTimeElapse = function (date) {
        var timeelapsed = "";
        if (date !== null || date !== "") {
            var date5 = date.split('T');
            var time = date5[1].split('.');
            var date5 = date5[0].split('-');
            timeelapsed = date5[1] + "/" + date5[2] + "/" + date5[0] + " " + time[0];
        }
        return timeelapsed;
    }
    //$scope.$watch('task.Notes', function (newvalue, oldvalue) {

    //        $scope.NotesTemplateDropdownflag = false;
    //        if (newvalue.length >1) {
    //            $http.get(rootDir + '/MasterDataNew/GetNotesTemplateByCode?Code=' + newvalue).
    //               success(function (data) {
    //                   $scope.NotesTemplate = data;
    //                   $scope.NotesTemplateDropdownflag = true;
    //               }
    //               )
    //        }

    //})

    $scope.showDiff = function (date) {
        var date1 = new Date(date);
        var date2 = new Date();
        var diff = (date2 - date1) / 1000;
        var diff = Math.abs(Math.floor(diff));

        var days = Math.floor(diff / (24 * 60 * 60));
        var leftSec = diff - days * 24 * 60 * 60;

        var hrs = Math.floor(leftSec / (60 * 60));
        var leftSec = leftSec - hrs * 60 * 60;

        var min = Math.floor(leftSec / (60));
        var leftSec = leftSec - min * 60;

        //console.log(days + " days " + hrs + " hours " + min + " minutes and " + leftSec + " seconds");
        return days + " days " + hrs + " hours " + min + " minutes and " + leftSec + " seconds";
    }

    // Method to set the row selected 
    $scope.showSetReminder = false;

    $scope.selTask = [];
    $scope.selTaskIDs = [];
    $scope.selectRemTask = function (event, provider) {
        if (!$scope.showOnClose) {
            return;
        }
        if (provider.SelectStatus == true) {
            provider.SelectStatus = false;
            $scope.selTask.splice($scope.selTask.indexOf(provider), 1);
            $scope.selTaskIDs.splice($scope.selTaskIDs.indexOf(provider), 1);
            if ($scope.selTask.length == 0) {
                $scope.CancelReminder();
            }
        }
        else {
            provider.SelectStatus = true;
            $scope.selTask.push(provider);
            $scope.selTaskIDs.push(provider);
        }
        $scope.taskList.tasks = angular.copy($scope.selTask);
        //provider.SelectStatus = provider.SelectStatus == true ? false : true;
        //$scope.selectedProviders.push(provider);
        //console.log($scope.selTask);
    }
    $scope.setInitialTaskData = function (data) {
        $.each(data, function (Mkey, Mvalue) {
            Mvalue["SetReminder"] = false;
        });
        if (localStorage.getItem("TaskReminders") != [] || localStorage.getItem("TaskReminders") != "" || localStorage.getItem("TaskReminders") != undefined) {
            var StoredReminderData = JSON.parse(localStorage.getItem("TaskReminders"));
            var taskDataFromApp = [];
            $.each(StoredReminderData, function (k, v) {
                v = JSON.parse(v.ReminderInfo);
                taskDataFromApp.push(v);
            });
            if ((data != [] || data != "") && (taskDataFromApp != [] || taskDataFromApp != "" || taskDataFromApp != undefined)) {
                $.each(data, function (Mkey, Mvalue) {
                    $.each(taskDataFromApp, function (Skey, Svalue) {
                        if (Mvalue.TaskTrackerId == Svalue.TaskTrackerId) {
                            //   console.log('matched Value: original data' + Mvalue.TaskTrackerId + 'local storage data:' + Svalue.TaskTrackerId);
                            Mvalue["SetReminder"] = true;
                            if (Svalue.ScheduledDateTime != null && Svalue.ScheduledDateTime != undefined)
                                Mvalue["ScheduledDateTime"] = new Date(parseInt(Svalue.ScheduledDateTime.replace("/Date(", "").replace(")/", ""), 10));
                        }
                        //else {
                        //    Mvalue["SetReminder"] = false;
                        //  returnValue = new Date(parseInt(StoredReminderData[0].ScheduledDateTime.replace("/Date(", "").replace(")/", ""), 10));
                        //}
                    });
                });
            }
        }
        else {
            if ((data != [] || data != "")) {
                $.each(data, function (key, value) {
                    value["SetReminder"] = false;
                });
            }
        }
        return data;
    };
    $scope.selectedTasks = function () {
        $scope.taskList.tasks = $filter('filter')($scope.mc.displayed, { checked: true });
    }
    $scope.SetReminderModal = function () {
        $scope.showSetReminder = true;
        $scope.ShowAddButton = true;
    }
    $scope.ReSetReminderModal = function (id) {
        $scope.showSetReminder = true;
        $scope.ShowAddButton = false;
        $scope.RescheduleID = id;
    }

    $scope.OpenReminderModalForReschedule = function () { };
    //$scope.RescheduleReminderFromTaskTracker = function (taskID) {
    //    var ReminderDateTime = new Date($('#datetimepicker3').val());
    //    $.ajax({
    //        url: rootDir + '/TaskTracker/RescheduleReminder',
    //        data: JSON.stringify({ 'taskID': taskID, 'scheduledDateTime': date }),
    //        type: "POST",
    //        contentType: "application/json",
    //        success: function (response) {
    //            if (response) {
    //                $(".RemainderBody").addClass('show');
    //                $('#ReminderFullBody').remove();
    //                clearInterval(reminderInterval);
    //                TaskNotificationReminder();
    //            }
    //        },
    //        error: function (error) {
    //            //alert("Sorry, there is some problem!");
    //        }
    //    });
    //};

    //-----check box for Tasks--------------------
    $scope.taskList = {
        tasks: [],
        remainingTime: '',
        reminderDate: '',
        reminderDateTime: '',
        taskCount: 0
    };

    //Close the Set Reminder Calender
    $scope.CancelReminder = function () {
        $scope.showSetReminder = false;
        $scope.ResetRemData();
    }

    $scope.ReSetReminder = function () {
        $scope.showSetReminder = false;
        var TaskData = JSON.parse(localStorage.getItem("TaskReminders"));
        var IDToReschedule;
        $.each(TaskData, function (key, value) {
            var temp = JSON.parse(value.ReminderInfo);
            if (temp.TaskTrackerId == $scope.RescheduleID) {
                IDToReschedule = value.TaskReminderID;
            }
        })
        var ReminderDateTime = new Date($('#datetimepicker3').val());
        var DateToReschedule = ReminderDateTime.getUTCFullYear() + '/' + (ReminderDateTime.getMonth() + 1) + '/' + ReminderDateTime.getDate() + ' ' + ReminderDateTime.getHours() + ':' + ReminderDateTime.getMinutes() + ':' + ReminderDateTime.getSeconds();

        $.ajax({
            url: rootDir + '/TaskTracker/RescheduleReminder',
            data: JSON.stringify({ 'taskID': IDToReschedule, 'scheduledDateTime': DateToReschedule }),
            type: "POST",
            contentType: "application/json",
            success: function (response) {
                if (response) {
                    $(".RemainderBody").addClass('show');
                    $('#ReminderFullBody').remove();
                    clearInterval(reminderInterval);
                    TaskNotificationReminder();
                }
            },
            error: function (error) {
                //alert("Sorry, there is some problem!");
            }
        });
    };

    
    //Set the Date time for task reminder
    $scope.SetReminder = function () {
        $scope.showSetReminder = false;
        $scope.TaskReminder = [];
        //console.log($scope.taskList.tasks);
        var ReminderDateTime = new Date($('#datetimepicker3').val());
        for (var i = 0; i < $scope.taskList.tasks.length; i++) {
            $scope.TaskReminder.push({ ReminderInfo: JSON.stringify($scope.taskList.tasks[i]), CreatedDate: new Date(), ScheduledDateTime: ReminderDateTime });
            $scope.taskList.reminderDateTime = ReminderDateTime;
            //$scope.TaskReminder = { ReminderInfo: JSON.stringify($scope.taskList.tasks[i]), CreatedDate: new Date(), ScheduledDateTime: ReminderDateTime };
        }

        $http.post(rootDir + '/TaskTracker/SetReminder', $scope.TaskReminder).success(function (data) {
            toaster.pop('Success', "Success", 'Reminder Set Successfully');
            $.each(ctrl.displayed, function (Mkey, Mvalue) {
                $.each($scope.taskList.tasks, function (Skey, Svalue) {
                    if (Mvalue.TaskTrackerId == Svalue.TaskTrackerId) {
                        Mvalue["SetReminder"] = true;
                    }
                });
            });
            //console.log(ctrl.displayed)
            $scope.ResetRemData();
            TaskNotificationReminder();
            if ($rootScope.tabNames == "AllTask")
                $scope.clerCheckBoxForAllTasks();
            else if ($rootScope.tabNames == "DailyTask")
                $scope.clerCheckBoxForDailyTasks();
        }).error(function (data) {
            toaster.pop('error', "error", 'Some Error Occured, please try again later');
        });
        $scope.taskList.reminderDateTime = ReminderDateTime;
        localStorage.setItem("TaskReminders", JSON.stringify($scope.taskList));
        $scope.taskStored = JSON.parse(localStorage.getItem("TaskReminders"));

    }

    $scope.ResetRemData = function () {
        angular.forEach($scope.selTask, function (value, key) {
            value.SelectStatus = false;
        });
        $scope.selTask = [];
        $scope.taskList.tasks = angular.copy($scope.selTask);
    };

    $scope.clerCheckBoxForAllTasks = function () {

        for (var i = 0; i < $scope.RemainingTasks.length; i++) {
            $scope.RemainingTasks[i].selected = false;
        }
    }

    $scope.clerCheckBoxForDailyTasks = function () {

        for (var i = 0; i < $scope.TasksAssigned.length; i++) {
            $scope.TasksAssigned[i].selected = false;
        }
    }

    $scope.checkedTask = false; // to set the status of checked tasks


    //Method to add/remove the checked/unchecked tasks from the list
    $scope.getCheckedTask = function (evt, task, checkedStatus) {
        //console.log(evt);
        $scope.changeClass();
        //$scope.newTask = angular.copy(task);
        //task.selected ? task.selected = false : task.selected = true;
        //// If task is checked, add it to the List
        //if (task.selected == true) {
        //    $scope.newTask.ProviderName = $scope.newTask.ProviderName.substr(0, $scope.newTask.ProviderName.indexOf('-'));
        //    $scope.taskList.tasks.push($scope.newTask);
        //    $scope.taskList.taskCount++;
        //}


        //// If task is unchecked, remove it from the List
        //if (task.selected == false) {
        //    $scope.taskList.taskCount--;
        //    for (var i = 0; i < $scope.taskList.tasks.length; i++) {
        //        if ($scope.taskList.tasks[i].TaskTrackerId == $scope.newTask.TaskTrackerId)
        //            $scope.taskList.tasks.splice($scope.taskList.tasks[i], 1);
        //    }

        //}


    }


});

function ResetFormForValidation(form) {
    form.removeData('validator');
    form.removeData('unobtrusiveValidation');
    $.validator.setDefaults({ ignore: null });
    $.validator.unobtrusive.parse(form);
}


function DismissSingleTaskforremoveTask(remainderid) {

    $.ajax({
        url: rootDir + '/TaskTracker/DismissReminder?taskID=' + remainderid,
        type: "POST",
        success: function (response) {
            if (response) {
                var TaskData = JSON.parse(localStorage.getItem("TaskReminders"));
                $.each(TaskData, function (key, value) {
                    if (value.TaskReminderID == SelectedTaskID) {
                        taskToDismiss = value;
                        TaskData.splice(key, 1);
                        TaskNotificationReminder();

                    }
                })
                //localStorage.setItem("TaskReminders", JSON.stringify(TaskData));
            }
        },
        error: function (error) {
            //alert("Sorry, there is some problem!");
        }
    });

};


var TaskNotificationReminder = function () {
    $.ajax({
        //url: rootDir + '/Prototypes/GetReminderNotification',
        url: rootDir + '/TaskTracker/GetReminders',
        type: "GET",
        success: function (response) {
            //var taskStored = JSON.parse(localStorage.getItem("TaskReminders"));
            var taskStored = response.reminders;
            localStorage.setItem("TaskReminders", JSON.stringify(response.reminders)); // setting the reminder objects into local storage
            view = response.responseView;
            getReminderView(taskStored);

            reminderInterval = setInterval(function () { CheckReminder() }, 30000); // calling function repeatedly to check reminder
        },
        error: function (error) {
            //alert("Sorry, there is some problem!");
        }
    });
}


//function ToggleSelectionClass(This) {
//    if ($(This).hasClass('danger')) {
//    $(This).toggleClass('danger');
//    }
//    $('.ActionButton').toggleClass('show hide');
//    $('.SetRemButton').toggleClass('hide show');
//}



$(document).ready(function () {
    $(".ProviderTypeSelectAutoList").hide();
    $("body").tooltip({ selector: '[data-toggle=tooltip]' });
    $("#sidemenu").addClass("menu-in");
    $("#page-wrapper").addClass("menuup");

    //$('#TempSUb').change(function () {
    //    if ($('#TempSUb').val().length != 0 && $('#TempSUb').val() != "") {
    //        $scope.errormessageforsubject = false;
    //    }
    //});


});

$(document).click(function (event) {
    if (!$(event.target).hasClass("form-control") && $(event.target).parents(".ProviderTypeSelectAutoList").length === 0) {
        $(".ProviderTypeSelectAutoList").hide();
    }
});