//Module declaration
var trackerApp = angular.module('TrackerApp', ['ui.bootstrap', 'smart-table', 'mgcrea.ngStrap', 'loadingInteceptor']);

$(document).ready(function () {
    $("#sidemenu").addClass("menu-in");
    $("#page-wrapper").addClass("menuup");
});

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
names = ['Subject', 'ProviderName', 'SubSectionName', 'NextFollowUpDate', 'AssignedTo', 'ModifiedDate', 'UpdatedBy', 'CompleteStatus', 'InsuranceCompanyName', 'ModeOfFollowUp'];
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

        }
    };
});


trackerApp.value("tabStatus", [{ tabName: "DailyTask", Displayed: [], Assigned: false, subSection: true, insurance: true, followup: true, followUpDate: true, updatedOn: false, updatedby: false, editBtn: true, removeBtn: true, historyBtn: true, TabStatus: true, DailyTableStatus: {}, predicate: {}, reset: { sort: {}, search: {}, pagination: { start: 0 } } },
                               { tabName: "TaskAssigned", Displayed: [], Assigned: true, subSection: true, insurance: true, followup: true, followUpDate: true, updatedOn: false, updatedby: false, editBtn: true, removeBtn: true, historyBtn: true, TabStatus: true, DailyTableStatus: {}, predicate: {}, reset: { sort: {}, search: {}, pagination: { start: 0 } } },
                               { tabName: "AllTask", Displayed: [], Assigned: true, subSection: true, insurance: true, followup: true, followUpDate: true, updatedOn: false, updatedby: false, editBtn: true, removeBtn: true, historyBtn: true, TabStatus: true, DailyTableStatus: {}, predicate: {}, reset: { sort: {}, search: {}, pagination: { start: 0 } } },
                               { tabName: "ClosedTask", Displayed: [], Assigned: false, subSection: true, insurance: true, followup: true, followUpDate: true, updatedOn: false, updatedby: false, editBtn: true, removeBtn: true, historyBtn: true, TabStatus: true, DailyTableStatus: {}, predicate: {}, reset: { sort: {}, search: {}, pagination: { start: 0 } } },
                               { tabName: "HistoryTask", Displayed: [], Assigned: false, subSection: false, insurance: false, followup: false, followUpDate: false, updatedOn: true, updatedby: true, editBtn: false, removeBtn: false, historyBtn: false, TabStatus: true, DailyTableStatus: {}, predicate: {}, reset: { sort: {}, search: {}, pagination: { start: 0 } } }])

trackerApp.filter("ResetTabStatus", function (tabStatus) {
    return function (status) {
        return tabStatus.filter(function (data) { return data.tabName == status })[0];
    }
})

var TempFollowupHelper = [];
//Controller declaration
trackerApp.controller('TrackerCtrl', function ($scope, $rootScope, $http, $q, $filter, messageAlertEngine, Resource) {

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
    $scope.TasksAssigned = [];
    $scope.ClosedTasks = [];
    $scope.TasksAssignedtoMe = [];
    $scope.TasksAssignedByMe = [];
    $scope.AllTasks = [];
    $scope.RemainingTasks = [];
    $scope.TaskHistory = [];
    $rootScope.DailyTasksTracker = [];
    $rootScope.TasksAssignedTracker = [];
    $rootScope.AllTasksTracker = [];
    $scope.tableDailyValue1 = {};
    $scope.tableDailyValue2 = {};
    $scope.tableDailyValue3 = {};
    $scope.tableDailyValue4 = {};

    $rootScope.tabNames = 'DailyTask';
    $scope.history = false;
    $scope.TableView = true;
    $scope.showLoading = true;
    //$scope.InsuranceCompanyHelper = [];
    var currentcduserdata = "";

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
    function InsuranceCompaniesData() {
        var defer = $q.defer();
        $http.get(rootDir + '/TaskTracker/GetAllInsuranceCompanies').
        success(function (data, status, headers, config) {
            $scope.InsuranceCompanies = data;
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
        console.log(pagination.number);

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
        $rootScope.$broadcast(name);
    };

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
        }
        $rootScope.tabNames = 'DailyTask';
        $scope.cancelEditViewforTab();
        $rootScope.trackerItems = $scope.TasksAssigned;
        $rootScope.DailyTasksTracker = angular.copy($rootScope.trackerItems);
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
        $scope.cancelEditViewforTab();
        //$scope.t.sort.predicate = "NextFollowUpDate";
        //$scope.t.sort.reverse = false;
        $rootScope.trackerItems = $scope.DailyTasks;
        $rootScope.TasksAssignedTracker = angular.copy($rootScope.trackerItems);
        $scope.t.pagination.start = 0;
        $scope.t.pagination.numberOfPages = 0;
        //tableState.sort = {};        
        $scope.t.search.predicateObject = {};
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
        //$rootScope.trackerItems = $scope.RemainingTasksPagination;
        $rootScope.AllTasksTracker = angular.copy($rootScope.trackerItems);
        $scope.t.pagination.start = 0;
        $scope.t.pagination.numberOfPages = 0;
        $scope.t.search.predicateObject = {};
        $scope.t.sort.predicate = "NextFollowUpDate";
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
        // $rootScope.tabNames = 'ClosedTasks';
        $scope.cancelEditViewforTab();
        $rootScope.trackerItems = $scope.ClosedTasks;
        $rootScope.AllTasksTracker = angular.copy($rootScope.trackerItems);
        $scope.t.pagination.start = 0;
        //$scope.t.pagination.numberOfPages = 0;
        $scope.t.search.predicateObject = {};
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
    $rootScope.$on('History', function () {
        $scope.tabStatus = $filter('ResetTabStatus')('HistoryTask');
        $rootScope.tabNames = 'HistoryTask';
        $scope.cancelEditViewforTab();
        $rootScope.trackerItems = $scope.TaskHistory;
        $scope.t.pagination.start = 0;
        $scope.t.pagination.numberOfPages = 0;
        $scope.t.search.predicateObject = {};
        ctrl.callServer($scope.t);
        ctrl.temp = $scope.TaskHistory;
    });
    $rootScope.$on("evn", function (event, args) {
        $scope.DailyTasks = angular.copy(args);
        $scope.DailyTasksPagination = angular.copy($scope.DailyTasks);
        $rootScope.$broadcast('TasksAssigned');

    });
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
    $rootScope.$on("Users", function (event, args) {
        $scope.users = angular.copy(args);
    });
    //////////////////////////////////////////////////////

    $scope.showHistory = function (task) {
        $rootScope.previousTabName = $rootScope.tabNames;
        $scope.TaskHistory = [];
        $('.nav-tabs a:last').tab('show');
        $scope.history = true;
        //$scope.tabStatus = $filter('ResetTabStatus')('HistoryTask');
        var tabID = [];
        var sectionID = [];
        var histories = [];

        if ($rootScope.tabNames == 'DailyTask') {
            histories = $scope.TasksAssigned[$scope.TasksAssigned.indexOf($scope.TasksAssigned.filter(function (taskId) { return taskId.TaskTrackerId == task.TaskTrackerId })[0])];
        }
        else if ($rootScope.tabNames == 'TasksAssigne') {
            histories = $scope.DailyTasks[$scope.DailyTasks.indexOf($scope.DailyTasks.filter(function (taskId) { return taskId.TaskTrackerId == task.TaskTrackerId })[0])];
        }
        else if ($rootScope.tabNames == 'AllTask') {
            histories = $scope.RemainingTasks[$scope.RemainingTasks.indexOf($scope.RemainingTasks.filter(function (taskId) { return taskId.TaskTrackerId == task.TaskTrackerId })[0])];
        }
        else if ($rootScope.tabNames == 'CloseTasks') {
            histories = $scope.ClosedTasks[$scope.ClosedTasks.indexOf($scope.ClosedTasks.filter(function (taskId) { return taskId.TaskTrackerId == task.TaskTrackerId })[0])];
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

            $scope.TaskHistory.push({
                TaskTrackerId: histories.TaskTrackerId, ProfileID: histories.TaskTrackerHistories.ProfileID,
                ProviderName: ProviderName[j], SubSectionName: histories.TaskTrackerHistories[j].SubSectionName,
                Subject: histories.TaskTrackerHistories[j].Subject, NextFollowUpDate: $scope.ConvertDate(histories.TaskTrackerHistories[j].NextFollowUpDate),
                ModeOfFollowUp: JSON.parse(histories.TaskTrackerHistories[j].ModeOfFollowUp),
                InsuranceCompanyName: TasksAssignedinsurancecompanyname,
                UpdatedBy: histories.TaskTrackerHistories[j].LastUpdatedBy,
                //AssignedToId: $scope.users.filter(function (users) { return users.CDUserID == $scope.TasksAssignedtoMe[k].AssignedToId })[0].AuthenicateUserId, AssignedTo: AssignedTo, HospitalID: $scope.TasksAssignedtoMe[k].HospitalID,
                AssignedToId: AssignedToId[j], AssignedTo: AssignedTo[j],
                Hospital: HospitalNamefortaskassigned, Notes: histories.TaskTrackerHistories[j].Notes, ModifiedDate: $scope.ConvertDate(histories.TaskTrackerHistories[j].LastModifiedDate),
                TabID: tabID,
                SubSectionID: sectionID,
                CompleteStatus: histories.TaskTrackerHistories[j].Status == "Active" ? "Open" : "Closed"
            });
        }
        $rootScope.trackerItems = $scope.TaskHistory;
        //ctrl.callServer();
        //ctrl.temp = $scope.TaskHistory;

        $rootScope.$broadcast('History');
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

    }
    $scope.ProviderNameforNotes = "";
    $scope.DailyTasksPagination = [];
    $scope.TasksAssigned = [];
    $scope.RemainingTasksPagination = [];

    // Getting all Rquired Data ----------------------------------------------------------------------
    $scope.LoadData = function () {
        $scope.progressbar = false;
        //var onLoad = 0;
        $q.all([$q.when(GetCurrentUser()),
            $q.when(CDUsersData()),
            $q.when(HospitalsData()),
            $q.when(TasksData()),
            $q.when(ProvidersData()),
            $q.when(InsuranceCompaniesData()),
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
                if ($scope.Tasks[i].AssignedById == currentcduserdata.cdUser.CDUserID && $scope.Tasks[i].AssignedToId != currentcduserdata.cdUser.CDUserID) {
                    $scope.TasksAssignedByMe.push($scope.Tasks[i]);
                }
                if ($scope.Tasks[i].AssignedToId !== currentcduserdata.cdUser.CDUserID && $scope.Tasks[i].AssignedById !== currentcduserdata.cdUser.CDUserID) {
                    $scope.AllTasks.push($scope.Tasks[i]);
                }
            }
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
                    var ProviderName = "";
                for (var i in $scope.Providers) {
                    if ($scope.Providers[i].ProfileId == $scope.TasksAssignedtoMe[k].ProfileID) { ProviderName = $scope.Providers[i].Name; }
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
                if ($scope.TasksAssignedtoMe[k].Status == "Active") {

                    $scope.TasksAssigned.push({
                        TaskTrackerId: $scope.TasksAssignedtoMe[k].TaskTrackerId, ProfileID: $scope.TasksAssignedtoMe[k].ProfileID,
                        ProviderName: ProviderName, SubSectionName: $scope.TasksAssignedtoMe[k].SubSectionName,
                        Subject: $scope.TasksAssignedtoMe[k].Subject, NextFollowUpDate: followupdatefortasksassigned,
                        ModeOfFollowUp: $scope.TasksAssignedtoMe[k].ModeOfFollowUp, FollowUp: Followups,
                        InsuranceCompanyName: TasksAssignedinsurancecompanyname,
                        //AssignedToId: $scope.users.filter(function (users) { return users.CDUserID == $scope.TasksAssignedtoMe[k].AssignedToId })[0].AuthenicateUserId, AssignedTo: AssignedTo, HospitalID: $scope.TasksAssignedtoMe[k].HospitalID,
                        AssignedToId: AssignedToId, AssignedTo: AssignedTo, HospitalID: $scope.TasksAssignedtoMe[k].HospitalID,
                        Hospital: HospitalNamefortaskassigned, Notes: $scope.TasksAssignedtoMe[k].Notes, ModifiedDate: $scope.TasksAssignedtoMe[k].LastModifiedDate,
                        TaskTrackerHistories: $scope.TasksAssignedtoMe[k].TaskTrackerHistories,
                        TabID: tabID,
                        SubSectionID: sectionID,
                        CompleteStatus: $scope.TasksAssignedtoMe[k].Status == "Active" ? "Open" : "Closed"
                    });
                }
                else if ($scope.TasksAssignedtoMe[k].Status == "Inactive") {
                    $scope.ClosedTasks.push({
                        TaskTrackerId: $scope.TasksAssignedtoMe[k].TaskTrackerId, ProfileID: $scope.TasksAssignedtoMe[k].ProfileID,
                        ProviderName: ProviderName, SubSectionName: $scope.TasksAssignedtoMe[k].SubSectionName,
                        Subject: $scope.TasksAssignedtoMe[k].Subject, NextFollowUpDate: followupdatefortasksassigned,
                        ModeOfFollowUp: $scope.TasksAssignedtoMe[k].ModeOfFollowUp, FollowUp: Followups,
                        InsuranceCompanyName: TasksAssignedinsurancecompanyname,
                        //AssignedToId: $scope.users.filter(function (users) { return users.CDUserID == $scope.TasksAssignedtoMe[k].AssignedToId })[0].AuthenicateUserId, AssignedTo: AssignedTo, HospitalID: $scope.TasksAssignedtoMe[k].HospitalID,
                        AssignedToId: AssignedToId, AssignedTo: AssignedTo, HospitalID: $scope.TasksAssignedtoMe[k].HospitalID,
                        Hospital: HospitalNamefortaskassigned, Notes: $scope.TasksAssignedtoMe[k].Notes, ModifiedDate: $scope.TasksAssignedtoMe[k].LastModifiedDate,
                        TaskTrackerHistories: $scope.TasksAssignedtoMe[k].TaskTrackerHistories,
                        TabID: tabID,
                        SubSectionID: sectionID,
                        CompleteStatus: $scope.TasksAssignedtoMe[k].Status == "Active" ? "Open" : "Closed"
                    });
                }
            }

            $rootScope.$broadcast("evnt", $scope.TasksAssigned);

            for (var k = 0; k < $scope.TasksAssignedByMe.length; k++) {
                var ProviderName = "";
                if ($scope.TasksAssignedByMe[k].ProfileID != null)
                    for (var i in $scope.Providers) {
                        if ($scope.Providers[i].ProfileId == $scope.TasksAssignedByMe[k].ProfileID) { var ProviderName = $scope.Providers[i].Name; }
                    }
                //var ProviderName = $scope.Providers.filter(function (Providers) { return Providers.ProfileId == $scope.TasksAssignedByMe[k].ProfileID })[0].Name;
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
                var DailyTasksinsurancecompanyname = [];
                DailyTasksinsurancecompanyname = $scope.TasksAssignedByMe[k].InsuranceCompanyName == null ? "" : $scope.TasksAssignedByMe[k].InsuranceCompanyName.CompanyName;
                var tabID = "";
                var sectionID = "";
                var Followups = "";
                for (var i in $scope.SubSections) {
                    if ($scope.SubSections[i].SubSectionName == $scope.TasksAssignedByMe[k].SubSectionName) {
                        //tabID = $scope.SubSections[i].TabName;
                        //sectionID = $scope.SubSections[i].SubSectionID;
                    }
                }
                //for (var p in $scope.InsuranceCompanies) {
                //    if ($scope.TasksAssignedByMe[k].InsuaranceCompanyNameID == $scope.InsuranceCompanies[p].InsuaranceCompanyNameID) {
                //        DailyTasksinsurancecompanyname = $scope.InsuranceCompanies[p].CompanyName;
                //    }
                //}
                var followupdatefordailytasks = $scope.ConvertDate($scope.TasksAssignedByMe[k].NextFollowUpDate);
                if ($scope.TasksAssignedByMe[k].Status == "Active") {
                    $scope.DailyTasks.push({
                        TaskTrackerId: $scope.TasksAssignedByMe[k].TaskTrackerId, ProfileID: $scope.TasksAssignedByMe[k].ProfileID,
                        ProviderName: ProviderName, SubSectionName: $scope.TasksAssignedByMe[k].SubSectionName,
                        Subject: $scope.TasksAssignedByMe[k].Subject, NextFollowUpDate: followupdatefordailytasks,
                        ModeOfFollowUp: $scope.TasksAssignedByMe[k].ModeOfFollowUp, FollowUp: Followups,
                        InsuranceCompanyName: DailyTasksinsurancecompanyname,
                        AssignedToId: Assignedtoid, AssignedTo: AssignedTo, HospitalID: $scope.TasksAssignedByMe[k].HospitalID,
                        Hospital: DailyTaskHospitalName, Notes: $scope.TasksAssignedByMe[k].Notes, ModifiedDate: $scope.TasksAssignedByMe[k].LastModifiedDate,
                        TaskTrackerHistories: $scope.TasksAssignedByMe[k].TaskTrackerHistories,
                        TabID: tabID,
                        SubSectionID: sectionID,
                        CompleteStatus: $scope.TasksAssignedByMe[k].Status == "Active" ? "Open" : "Closed"
                    });
                }
                else if ($scope.TasksAssignedByMe[k].Status == "Inactive") {
                    $scope.ClosedTasks.push({
                        TaskTrackerId: $scope.TasksAssignedByMe[k].TaskTrackerId, ProfileID: $scope.TasksAssignedByMe[k].ProfileID,
                        ProviderName: ProviderName, SubSectionName: $scope.TasksAssignedByMe[k].SubSectionName,
                        Subject: $scope.TasksAssignedByMe[k].Subject, NextFollowUpDate: followupdatefortasksassigned,
                        ModeOfFollowUp: $scope.TasksAssignedByMe[k].ModeOfFollowUp, FollowUp: Followups,
                        InsuranceCompanyName: TasksAssignedinsurancecompanyname,
                        //AssignedToId: $scope.users.filter(function (users) { return users.CDUserID == $scope.TasksAssignedtoMe[k].AssignedToId })[0].AuthenicateUserId, AssignedTo: AssignedTo, HospitalID: $scope.TasksAssignedtoMe[k].HospitalID,
                        AssignedToId: AssignedToId, AssignedTo: AssignedTo, HospitalID: $scope.TasksAssignedByMe[k].HospitalID,
                        Hospital: HospitalNamefortaskassigned, Notes: $scope.TasksAssignedByMe[k].Notes, ModifiedDate: $scope.TasksAssignedByMe[k].LastModifiedDate,
                        TaskTrackerHistories: $scope.TasksAssignedByMe[k].TaskTrackerHistories,
                        TabID: tabID,
                        SubSectionID: sectionID,
                        CompleteStatus: $scope.TasksAssignedByMe[k].Status == "Active" ? "Open" : "Closed"
                    });
                }
            }
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
                if ($scope.AllTasks[k].ProfileID != null)
                    for (var i in $scope.Providers) {
                        if ($scope.Providers[i].ProfileId == $scope.AllTasks[k].ProfileID) { ProviderName = $scope.Providers[i].Name; }
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
                if ($scope.AllTasks[k].Status == "Active") {
                    $scope.RemainingTasks.push({
                        TaskTrackerId: $scope.AllTasks[k].TaskTrackerId, ProfileID: $scope.AllTasks[k].ProfileID,
                        ProviderName: ProviderName, SubSectionName: $scope.AllTasks[k].SubSectionName,
                        Subject: $scope.AllTasks[k].Subject, NextFollowUpDate: followupdateforalltasks,
                        ModeOfFollowUp: $scope.AllTasks[k].ModeOfFollowUp, FollowUp: Followups,
                        InsuranceCompanyName: InsuranceCompanyforalltasks,
                        //AssignedToId: $scope.users.filter(function (users) { return users.CDUserID == $scope.AllTasks[k].AssignedToId })[0].AuthenicateUserId, AssignedTo: AssignedTo, HospitalID: $scope.AllTasks[k].HospitalID,
                        AssignedToId: Assignedtoid, AssignedTo: AssignedTo, HospitalID: $scope.AllTasks[k].HospitalID,
                        Hospital: HospitalNameforremainingtasks, Notes: $scope.AllTasks[k].Notes, ModifiedDate: $scope.AllTasks[k].LastModifiedDate,
                        TaskTrackerHistories: $scope.AllTasks[k].TaskTrackerHistories,
                        TabID: tabID,
                        SubSectionID: sectionID,
                        CompleteStatus: $scope.AllTasks[k].Status == "Active" ? "Open" : "Closed"
                    });
                }
                else if ($scope.AllTasks[k].Status == "Inactive") {
                    $scope.ClosedTasks.push({
                        TaskTrackerId: $scope.AllTasks[k].TaskTrackerId, ProfileID: $scope.AllTasks[k].ProfileID,
                        ProviderName: ProviderName, SubSectionName: $scope.AllTasks[k].SubSectionName,
                        Subject: $scope.AllTasks[k].Subject, NextFollowUpDate: followupdatefortasksassigned,
                        ModeOfFollowUp: $scope.AllTasks[k].ModeOfFollowUp, FollowUp: Followups,
                        InsuranceCompanyName: TasksAssignedinsurancecompanyname,
                        //AssignedToId: $scope.users.filter(function (users) { return users.CDUserID == $scope.TasksAssignedtoMe[k].AssignedToId })[0].AuthenicateUserId, AssignedTo: AssignedTo, HospitalID: $scope.TasksAssignedtoMe[k].HospitalID,
                        AssignedToId: AssignedToId, AssignedTo: AssignedTo, HospitalID: $scope.AllTasks[k].HospitalID,
                        Hospital: HospitalNamefortaskassigned, Notes: $scope.AllTasks[k].Notes, ModifiedDate: $scope.AllTasks[k].LastModifiedDate,
                        TaskTrackerHistories: $scope.AllTasks[k].TaskTrackerHistories,
                        TabID: tabID,
                        SubSectionID: sectionID,
                        CompleteStatus: $scope.AllTasks[k].Status == "Active" ? "Open" : "Closed"
                    });
                }
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


    //To Display the drop down div
    $scope.searchCumDropDown = function (divId) {
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
        $scope.task.ProfileID = Provider.ProfileId;
        $scope.task.ProviderName = Provider.Name;

        $(".ProviderTypeSelectAutoList").hide();
    }
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
        $scope.task.SubSectionName = SubSection.SubSectionName;
        $(".ProviderTypeSelectAutoList").hide();
    }
    $scope.SelectInsurance = function (InsuranceCompany) {
        $scope.task.InsuranceCompanyName = InsuranceCompany.CompanyName;
        $(".ProviderTypeSelectAutoList").hide();
    }
    ////////////////////////////////////////

    // Manage partial Views
    $scope.showAddView = function () {
        $scope.VisibilityControl = "";
        $scope.Tempfollowup1 = "";
        $scope.Tempfollowup = [];
        $scope.Followups = TempFollowupHelper;
        $scope.task = {};
        var date = Date();
        var Time = date.split(" ")[4];
        var AMPM = $scope.changeTimeAmPm(Time);
        date = new Date();
        $scope.task.Notes = $scope.ProviderNameforNotes + " - " + (date.getMonth() + 1) + "/" + date.getDate() + "/" + date.getFullYear() + " - " + AMPM;
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
    /////////////////////////////////////////////////////////////

    //CRUD functions/////////
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
                                //AssignedToId: $scope.users.filter(function (users) { return users.CDUserID == data.AssignedToId })[0].AuthenicateUserId, AssignedTo: $scope.users.filter(function (users) { return users.CDUserID == data.AssignedToId })[0].UserName, HospitalID: data.HospitalID,
                                AssignedToId: Assignedtoid, AssignedTo: AssignedTo, HospitalID: data.HospitalID,
                                Hospital: Hospitalforaddtask, Notes: data.Notes, ModifiedDate: data.LastModifiedDate,
                                TabID: tabID,
                                SubSectionID: sectionID,
                                CompleteStatus: "Open"
                            };
                            $scope.DailyTasks.splice(0, 0, DailyTasksforaddedtask);
                            $rootScope.$broadcast("evn", $scope.DailyTasks);
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
                                //AssignedToId: $scope.users.filter(function (users) { return users.CDUserID == data.AssignedToId })[0].AuthenicateUserId, AssignedTo: $scope.users.filter(function (users) { return users.CDUserID == data.AssignedToId })[0].UserName, HospitalID: data.HospitalID,
                                AssignedToId: Assignedtoid, AssignedTo: AssignedTo, HospitalID: data.HospitalID,
                                //Hospital: data.HospitalID == null ? "" : $scope.Hospitals.filter(function (Hospitals) { return Hospitals.HospitalID == data.HospitalID })[0].HospitalName, Notes: data.Notes, ModifiedDate: data.LastModifiedDate,
                                Hospital: Hospitalforaddtask, Notes: data.Notes, ModifiedDate: data.LastModifiedDate,
                                TabID: tabID,
                                SubSectionID: sectionID,
                                CompleteStatus: "Open"
                            };
                            $scope.TasksAssigned.splice(0, 0, TasksAssignedforaddedtask);
                            $rootScope.$broadcast("evnt", $scope.TasksAssigned);
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
    }
    $scope.editTask = function () {
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
                        data.ModeOfFollowUp = JSON.parse(data.ModeOfFollowUp);
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
                        if ($rootScope.tabNames == 'DailyTask') {
                            $scope.TasksAssigned[$scope.TasksAssigned.indexOf($scope.TasksAssigned.filter(function (updates) { return updates.TaskTrackerId == data.TaskTrackerId })[0])].TaskTrackerHistories = (data.TaskTrackerHistories);
                        }
                        else if ($rootScope.tabNames == 'TasksAssigne') {
                            $scope.DailyTasks[$scope.DailyTasks.indexOf($scope.DailyTasks.filter(function (updates) { return updates.TaskTrackerId == data.TaskTrackerId })[0])].TaskTrackerHistories = (data.TaskTrackerHistories);
                        }
                        else if ($rootScope.tabNames == 'AllTask') {
                            $scope.RemainingTasks[$scope.RemainingTasks.indexOf($scope.RemainingTasks.filter(function (updates) { return updates.TaskTrackerId == data.TaskTrackerId })[0])].TaskTrackerHistories = (data.TaskTrackerHistories);
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
                                    //AssignedToId: $scope.users.filter(function (users) { return users.CDUserID == data.AssignedToId })[0].AuthenicateUserId, AssignedTo: $scope.users.filter(function (users) { return users.CDUserID == data.AssignedToId })[0].UserName, HospitalID: data.HospitalID,
                                    AssignedToId: Assignedtoid, AssignedTo: AssignedTo, HospitalID: data.HospitalID,
                                    Hospital: taskassignedhospital, Notes: data.Notes, ModifiedDate: data.LastModifiedDate,
                                    TaskTrackerHistories: data.TaskTrackerHistories,
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
                                for (var i in $scope.DailyTasks) {
                                    if ($scope.DailyTasks[i].TaskTrackerId == data.TaskTrackerId) {
                                        $scope.DailyTasks.splice(i, 1);
                                    }
                                }
                                //$scope.DailyTasks.splice($scope.DailyTasks.indexOf($scope.DailyTasks.filter(function (DailyTasks) { return DailyTasks.TaskTrackerId == data.TaskTrackerId })[0]), 1);
                                var DailyTasksforedittask = {
                                    TaskTrackerId: data.TaskTrackerId, ProfileID: data.ProfileID,
                                    //ProviderName: $scope.Providers.filter(function (Providers) { return Providers.ProfileId == data.ProfileID })[0].Name == 'undefined' ? "" : $scope.Providers.filter(function (Providers) { return Providers.ProfileId == data.ProfileID })[0].Name, SubSectionName: data.SubSectionName,
                                    ProviderName: Providername, SubSectionName: data.SubSectionName,
                                    Subject: data.Subject, NextFollowUpDate: updateddate,
                                    ModeOfFollowUp: data.ModeOfFollowUp, FollowUp: Followups,
                                    InsuranceCompanyName: updatedinsurancecompany,
                                    //InsuranceCompanyName: data.InsuranceCompanyName,
                                    //AssignedToId: $scope.users.filter(function (users) { return users.CDUserID == data.AssignedToId })[0].AuthenicateUserId, AssignedTo: $scope.users.filter(function (users) { return users.CDUserID == data.AssignedToId })[0].UserName, HospitalID: data.HospitalID,
                                    AssignedToId: Assignedtoid, AssignedTo: AssignedTo, HospitalID: data.HospitalID,
                                    Hospital: taskassignedhospital, Notes: data.Notes, ModifiedDate: data.LastModifiedDate,
                                    TaskTrackerHistories: data.TaskTrackerHistories,
                                    TabID: tabID,
                                    SubSectionID: sectionID,
                                    CompleteStatus: "Open"
                                };
                                $scope.DailyTasks.splice(0, 0, DailyTasksforedittask);
                                //$rootScope.$broadcast("evn", $scope.DailyTasks);
                                $rootScope.$broadcast("evnt", $scope.TasksAssigned);

                            }
                        }
                        else if (($scope.VisibilityControl == "editViewforTab") && ($rootScope.tabNames == 'AllTask')) {

                            if (data.AssignedToId == currentcduserdata.cdUser.CDUserID) {
                                // $scope.AllTasks.splice($scope.AllTasks.indexOf($scope.AllTasks.filter(function (datas) { return datas.TaskTrackerId == data.TaskTrackerId })[0]), 1);
                                for (var i in $scope.RemainingTasks) {
                                    if ($scope.RemainingTasks[i].TaskTrackerId == data.TaskTrackerId) {
                                        $scope.RemainingTasks.splice(i, 1);
                                    }
                                }
                                var TasksAssignedforedittask = {
                                    TaskTrackerId: data.TaskTrackerId, ProfileID: data.ProfileID,
                                    //ProviderName: $scope.Providers.filter(function (Providers) { return Providers.ProfileId == data.ProfileID })[0].Name == 'undefined' ? "" : $scope.Providers.filter(function (Providers) { return Providers.ProfileId == data.ProfileID })[0].Name, SubSectionName: data.SubSectionName,
                                    ProviderName: Providername, SubSectionName: data.SubSectionName,
                                    Subject: data.Subject, NextFollowUpDate: updateddate,
                                    ModeOfFollowUp: data.ModeOfFollowUp, FollowUp: Followups,
                                    //InsuranceCompanyName: updatedinsurancecompany,
                                    InsuranceCompanyName: updatedinsurancecompany,
                                    //AssignedToId: $scope.users.filter(function (users) { return users.CDUserID == data.AssignedToId })[0].AuthenicateUserId, AssignedTo: $scope.users.filter(function (users) { return users.CDUserID == data.AssignedToId })[0].UserName, HospitalID: data.HospitalID,
                                    AssignedToId: Assignedtoid, AssignedTo: AssignedTo, HospitalID: data.HospitalID,
                                    Hospital: taskassignedhospital, Notes: data.Notes, ModifiedDate: data.LastModifiedDate,
                                    TabID: tabID,
                                    SubSectionID: sectionID,
                                    CompleteStatus: "Open"
                                };
                                $scope.TasksAssigned.splice(0, 0, TasksAssignedforedittask);
                                //if ($rootScope.tabNames == 'AllTask') {
                                //    ctrl.temp = $scope.RemainingTasks;
                                //}
                                $rootScope.$broadcast("evnt", $scope.TasksAssigned);
                            }
                            if (data.AssignedToId != currentcduserdata.cdUser.CDUserID) {
                                for (var i in $scope.RemainingTasks) {
                                    if ($scope.RemainingTasks[i].TaskTrackerId == data.TaskTrackerId) {
                                        $scope.RemainingTasks.splice(i, 1);
                                    }
                                }
                                //$scope.DailyTasks.splice($scope.DailyTasks.indexOf($scope.DailyTasks.filter(function (DailyTasks) { return DailyTasks.TaskTrackerId == data.TaskTrackerId })[0]), 1);
                                var AllTasksforedittask = {
                                    TaskTrackerId: data.TaskTrackerId, ProfileID: data.ProfileID,
                                    //ProviderName: $scope.Providers.filter(function (Providers) { return Providers.ProfileId == data.ProfileID })[0].Name == 'undefined' ? "" : $scope.Providers.filter(function (Providers) { return Providers.ProfileId == data.ProfileID })[0].Name, SubSectionName: data.SubSectionName,
                                    ProviderName: Providername, SubSectionName: data.SubSectionName,
                                    Subject: data.Subject, NextFollowUpDate: updateddate,
                                    ModeOfFollowUp: data.ModeOfFollowUp, FollowUp: Followups,
                                    InsuranceCompanyName: updatedinsurancecompany,
                                    //AssignedToId: $scope.users.filter(function (users) { return users.CDUserID == data.AssignedToId })[0].AuthenicateUserId, AssignedTo: $scope.users.filter(function (users) { return users.CDUserID == data.AssignedToId })[0].UserName, HospitalID: data.HospitalID,
                                    AssignedToId: Assignedtoid, AssignedTo: AssignedTo, HospitalID: data.HospitalID,
                                    Hospital: taskassignedhospital, Notes: data.Notes, ModifiedDate: data.LastModifiedDate,
                                    TaskTrackerHistories: data.TaskTrackerHistories,
                                    TabID: tabID,
                                    SubSectionID: sectionID,
                                    CompleteStatus: "Open"
                                };
                                $scope.RemainingTasks.splice(0, 0, AllTasksforedittask);
                                ctrl.temp = $scope.RemainingTasks;
                                // ctrl.callServer();
                                //$rootScope.$broadcast("evn", $scope.DailyTasks);
                            }
                            $rootScope.$broadcast('AllTasks');

                        }
                        else if (($scope.VisibilityControl = "editViewforTab") && ($rootScope.tabNames == 'TasksAssigne')) {

                            if (data.AssignedToId == currentcduserdata.cdUser.CDUserID) {
                                for (var i in $scope.DailyTasks) {
                                    if ($scope.DailyTasks[i].TaskTrackerId == data.TaskTrackerId) {
                                        $scope.DailyTasks.splice(i, 1);
                                    }
                                }
                                //$scope.TasksAssigned.splice($scope.TasksAssigned.indexOf($scope.TasksAssigned.filter(function (datas) { return datas.TaskTrackerId == data.TaskTrackerId })[0]), 1);
                                var TasksAssignedforedittask = {
                                    TaskTrackerId: data.TaskTrackerId, ProfileID: data.ProfileID,
                                    //ProviderName: $scope.Providers.filter(function (Providers) { return Providers.ProfileId == data.ProfileID })[0].Name == 'undefined' ? "" : $scope.Providers.filter(function (Providers) { return Providers.ProfileId == data.ProfileID })[0].Name, SubSectionName: data.SubSectionName,
                                    ProviderName: Providername, SubSectionName: data.SubSectionName,
                                    Subject: data.Subject, NextFollowUpDate: updateddate,
                                    ModeOfFollowUp: data.ModeOfFollowUp, FollowUp: Followups,
                                    //InsuranceCompanyName: updatedinsurancecompany,
                                    InsuranceCompanyName: updatedinsurancecompany,
                                    //AssignedToId: $scope.users.filter(function (users) { return users.CDUserID == data.AssignedToId })[0].AuthenicateUserId, AssignedTo: $scope.users.filter(function (users) { return users.CDUserID == data.AssignedToId })[0].UserName, HospitalID: data.HospitalID,
                                    AssignedToId: Assignedtoid, AssignedTo: AssignedTo, HospitalID: data.HospitalID,
                                    Hospital: taskassignedhospital, Notes: data.Notes, ModifiedDate: data.LastModifiedDate,
                                    TabID: tabID,
                                    SubSectionID: sectionID,
                                    CompleteStatus: "Open"
                                };
                                $scope.TasksAssigned.splice(0, 0, TasksAssignedforedittask);
                                //ctrl.temp = $scope.DailyTasks;
                                //$rootScope.$broadcast("evnt", $scope.TasksAssigned);
                                $rootScope.$broadcast("evn", $scope.DailyTasks);
                            }
                            if (data.AssignedToId != currentcduserdata.cdUser.CDUserID) {
                                for (var i in $scope.DailyTasks) {
                                    if ($scope.DailyTasks[i].TaskTrackerId == data.TaskTrackerId) {
                                        $scope.DailyTasks.splice(i, 1);
                                    }
                                }
                                //$scope.DailyTasks.splice($scope.DailyTasks.indexOf($scope.DailyTasks.filter(function (DailyTasks) { return DailyTasks.TaskTrackerId == data.TaskTrackerId })[0]), 1);
                                var Assignedtoothersforedittask = {
                                    TaskTrackerId: data.TaskTrackerId, ProfileID: data.ProfileID,
                                    //ProviderName: $scope.Providers.filter(function (Providers) { return Providers.ProfileId == data.ProfileID })[0].Name == 'undefined' ? "" : $scope.Providers.filter(function (Providers) { return Providers.ProfileId == data.ProfileID })[0].Name, SubSectionName: data.SubSectionName,
                                    ProviderName: Providername, SubSectionName: data.SubSectionName,
                                    Subject: data.Subject, NextFollowUpDate: updateddate,
                                    ModeOfFollowUp: data.ModeOfFollowUp, FollowUp: Followups,
                                    InsuranceCompanyName: updatedinsurancecompany,
                                    //AssignedToId: $scope.users.filter(function (users) { return users.CDUserID == data.AssignedToId })[0].AuthenicateUserId, AssignedTo: $scope.users.filter(function (users) { return users.CDUserID == data.AssignedToId })[0].UserName, HospitalID: data.HospitalID,
                                    AssignedToId: Assignedtoid, AssignedTo: AssignedTo, HospitalID: data.HospitalID,
                                    Hospital: taskassignedhospital, Notes: data.Notes, ModifiedDate: data.LastModifiedDate,
                                    TaskTrackerHistories: data.TaskTrackerHistories,
                                    TabID: tabID,
                                    SubSectionID: sectionID,
                                    CompleteStatus: "Open"
                                };
                                $scope.DailyTasks.splice(0, 0, Assignedtoothersforedittask);
                                $rootScope.$broadcast("evn", $scope.DailyTasks);
                            }
                        }
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
            ctrl.callServer($scope.t);
            // $formData.reset();
        }

        else {
            $scope.errormessage = true;
        }
    }
    $scope.removeTask = function () {
        $scope.progressbar = false;
        var closed = [];
        $http.post(rootDir + '/TaskTracker/Inactivetask?taskTrackerID=' + $scope.TemporaryTask.TaskTrackerId).
           success(function (data, status, headers, config) {
               if (data == "true") {
                   messageAlertEngine.callAlertMessage("successfullySaved", "Task closed successfully", "success", true);
               }
               for (var j in $scope.TasksAssigned) {
                   if ($scope.TasksAssigned[j].TaskTrackerId == $scope.TemporaryTask.TaskTrackerId) {
                       $scope.TasksAssigned[j].CompleteStatus = "Closed";
                   }
               }
               if ($rootScope.tabNames == 'DailyTask') {
                   closed = $scope.TasksAssigned.splice($scope.TasksAssigned.indexOf($scope.TasksAssigned.filter(function (tasks) { return tasks.TaskTrackerId == $scope.TemporaryTask.TaskTrackerId })[0]), 1);
                   $scope.ClosedTasks.push(closed[0]);
               }
               //$scope.ClosedTasks.push($scope.TasksAssigned.splice($scope.TasksAssigned.indexOf($scope.TasksAssigned.filter(function (tasks) { return tasks.TaskTrackerId == $scope.TemporaryTask.TaskTrackerId })[0]), 1));
               //$rootScope.$broadcast('DailyTasks');
               for (var i in $scope.DailyTasks) {
                   if ($scope.DailyTasks[i].TaskTrackerId == $scope.TemporaryTask.TaskTrackerId) {
                       $scope.DailyTasks[i].CompleteStatus = "Closed";
                   }
               }
               for (var i in $scope.RemainingTasks) {
                   if ($scope.RemainingTasks[i].TaskTrackerId == $scope.TemporaryTask.TaskTrackerId) {
                       $scope.RemainingTasks[i].CompleteStatus = "Closed";
                   }
               }

               if ($rootScope.tabNames == 'DailyTask') {
                   //ctrl.temp = $scope.TasksAssigned;
                   //$rootScope.trackerItems = $scope.TasksAssigned;
                   $rootScope.$broadcast('DailyTasks');
               }
               else if ($rootScope.tabNames == 'TasksAssigne') {
                   closed = $scope.DailyTasks.splice($scope.DailyTasks.indexOf($scope.DailyTasks.filter(function (tasks) { return tasks.TaskTrackerId == $scope.TemporaryTask.TaskTrackerId })[0]), 1);
                   $scope.ClosedTasks.push(closed[0]);
                   //ctrl.temp = $scope.DailyTasks;
                   //$rootScope.trackerItems = $scope.DailyTasks;
                   $rootScope.$broadcast('TasksAssigned');
               }
               else if ($rootScope.tabNames == 'AllTask') {
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
                   $scope.TasksAssigned.push(opened[0]);
                   for (var j in $scope.TasksAssigned) {
                       if ($scope.TasksAssigned[j].TaskTrackerId == $scope.TemporaryTask.TaskTrackerId) {
                           $scope.TasksAssigned[j].CompleteStatus = "Open";
                       }
                   }
                   $rootScope.$broadcast('ClosedTasks');
               }

               for (var i in $scope.DailyTasks) {
                   if ($scope.DailyTasks[i].TaskTrackerId == $scope.TemporaryTask.TaskTrackerId) {
                       $scope.DailyTasks[i].CompleteStatus = "Open";
                   }
               }
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
               else if ($rootScope.tabNames == 'TasksAssigne') {
                   //ctrl.temp = $scope.DailyTasks;
                   //$rootScope.trackerItems = $scope.DailyTasks;
                   $rootScope.$broadcast('TasksAssigned');
               }
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
        });
    }
    ////////////////////////////

    //To initiate Removal Confirmation Modal
    $scope.inactiveWarning = function (task) {
        if (angular.isObject(task)) {
            $scope.TemporaryTask = {};
            $scope.TemporaryTask = angular.copy(task);
        }
        $('#inactiveWarningModal').modal();
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
        $scope.detailViewfortab = false;
        $scope.VisibilityControl = "";
        //$scope.editViewforTab1 = false;
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

    $scope.showDetailViewforTab1 = function (task) {
        $scope.TableView = false;
        $scope.detailViewfortab = true;
        $scope.taskView = task;
    }
    $scope.cancelViewforTab1 = function () {

        $scope.detailViewfortab = false;
        $scope.TableView = true;
        //if ($rootScope.tabNames == 'DailyTask') {
        //    $rootScope.$broadcast('DailyTasks');
        //}
        //else if ($rootScope.tabNames == 'TasksAssigne') {
        //    $rootScope.$broadcast('TasksAssigned');
        //}
        if ($rootScope.tabNames == 'DailyTask') {
            $("a[id='tabs1']").parent("li").tab('show')
            //$rootScope.$broadcast('DailyTasks');
        }
        //$scope.t.search.predicateObject = $scope.tableDailyValue.search.predicateObject;
        //ctrl.callServer($scope.t);
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


    $scope.cancelEditViewforTab = function () {
        $scope.editViewforTab1 = false;
        $scope.editViewforTab3 = false;
        $scope.editViewforTab2 = false;
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

    $scope.showeditView = function (task, editviewfortab) {
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
        $scope.VisibilityControl = editviewfortab;
        $scope.task = angular.copy(task);
        currentTask = angular.copy(task);
    }

});

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