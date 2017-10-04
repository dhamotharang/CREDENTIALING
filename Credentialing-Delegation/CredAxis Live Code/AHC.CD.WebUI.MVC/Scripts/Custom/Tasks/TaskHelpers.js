var trackerApp = angular.module('TrackerApp', ['mgcrea.ngStrap', 'ui.bootstrap', 'smart-table']);
trackerApp.service('DOMManager', ['$rootScope', 'TabConfig', '$timeout', function ($rootScope, TabConfig, $timeout) {
    this.TabChanged = function (tabName, arr) {
        var temp = [];
        $rootScope.DangerStyle = true;
        switch (tabName) {
            case "MyDailyTasks":
                angular.element("a[id='tabs1']").parent("li").tab('show');
                for (var i = 0; i < arr.length; i++) {
                    if (arr[i].AssignedToId == currentUserCdUserId)
                        temp.push(arr[i]);
                }
                break;
            case "AllTasks":
                angular.element("a[id='tabs3']").parent("li").tab('show');
                for (var i = 0; i < arr.length; i++) {
                    if (arr[i].Status != 'CLOSED')
                        temp.push(arr[i]);
                }
                break;
            case "ClosedTasks":
                angular.element("a[id='tabs4']").parent("li").tab('show');
                for (var i = 0; i < arr.length; i++) {
                    if (arr[i].Status == 'CLOSED')
                        temp.push(arr[i]);
                }
                break;
            case "HistoryTasks":
                $rootScope.DangerStyle = false;
                angular.element('.nav-tabs a:last').parent("li").tab('show');
                temp = angular.copy($rootScope.HistoryTasks);
                break;
        };
        this.ModifyTabConfiguration(tabName);
        return temp;
    };
    this.ModifyTabConfiguration = function (tabName) {
        $rootScope.CurrentTabConfig = TabConfig.filter(function (x) { return x.TabName == tabName })[0];
        $rootScope.CurrentTabColsCount = 3 + Object.keys($rootScope.CurrentTabConfig.Columns).filter(function (prop) { return $rootScope.CurrentTabConfig.Columns[prop] == true; }).length;
    };
    this.ViewHandler = function (loadPartialViewName) {
        $rootScope.PartialViewControl = loadPartialViewName;
    };
}]);
trackerApp.service('Helpers', ['$rootScope', '$timeout', '$http', '$q', function ($rootScope, $timeout, $http, $q) {
    $rootScope.messageDesc = "";
    $rootScope.activeMessageDiv = "";
    $rootScope.messageType = "";
    var animateMessageAlertOff = function () {
        $rootScope.closeAlertMessage();
    };
    this.callAlertMessage = function (calledDiv, msg, msgType, dismissal) {
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
    this.ConvertDateFormat = function (value) {
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
    this.ConvertDate = function (date) {
        var formattedDate = "";
        if (date !== null || date !== "") {
            var date5 = date.split('T');
            var date5 = date5[0].split('-');
            formattedDate = date5[1] + "/" + date5[2] + "/" + date5[0];
        }
        return formattedDate;
    }
    this.ConvertDateForNotes = function (date) {
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
    this.ConvertDateForTimeElapse = function (date) {
        var timeelapsed = "";
        if (date !== null || date !== "") {
            var date5 = date.split('T');
            var time = date5[1].split('.');
            var date5 = date5[0].split('-');
            timeelapsed = date5[1] + "/" + date5[2] + "/" + date5[0] + " " + time[0];
        }
        return timeelapsed;
    }
    this.showDiff = function (date) {
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
        return days + " days " + hrs + " hours " + min + " minutes and " + leftSec + " seconds";
    };
    this.FormatTimePeriod = function (value) {
        var timearr = value.split(":");
        var hours = timearr[0];
        var minutes = timearr[1];
        var ampm = hours >= 12 ? 'PM' : 'AM';
        hours = hours % 12;
        hours = hours ? hours : 12;
        minutes = minutes.length == 1 ? minutes < 10 ? '0' + minutes : minutes : minutes;
        return hours + ':' + minutes + ' ' + ampm;
    };
    this.changeTimeTo24Hr = function (value) {
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
        var strTime = hours.replace(" ", "") + ':' + minutes;
        return strTime;
    }
    this.resetTableState = function (tableState) {
        if (tableState !== undefined) {
            tableState.sort = {};
            tableState.pagination.start = 0;
            tableState.search.predicateObject = {};
            return tableState;
        }
    }
    this.Get = function (url) {
        var deferObj = $q.defer();
        var promise = $http.get(rootDir + url);
        promise.then(function (result) {
            deferObj.resolve(result.data);
        },
            function (error) {
                deferObj.reject(error);
            });
        return deferObj.promise;
    };
    this.GetNotesSignatureForAdd = function () {
        var date = new Date();
        var timeMeridian = this.FormatTimePeriod(date.toString().split(" ")[4]);
        return (date.getMonth() + 1) + "/" + date.getDate() + "/" + date.getFullYear() + "~" + timeMeridian + "~" + LoggedInUserName + "~";
    };
}]);
trackerApp.run(['$rootScope', 'Helpers', function ($rootScope, Helpers) {
    for (var i = 0; i < Tasks.length; i++) {
        Tasks[i].NextFollowUpDate = Helpers.ConvertDate(Tasks[i].NextFollowUpDate);
        Tasks[i].ModeOfFollowUp = JSON.parse(Tasks[i].FollowUps);
    }
    $rootScope.Tasks = Tasks;
    $rootScope.SafeSourceTasks = Tasks; //// Safe Copy of Initial Tasks for Smart Table to Reload.
}]);
trackerApp.directive('stRatio', function () {
    return {
        link: function (scope, element, attr) {
            var ratio = +(attr.stRatio);
            element.css('width', ratio + '%');
        }
    };
});
trackerApp.value("TabConfig", [
    { TabName: "MyDailyTasks", Displayed: [], Columns: { DaysLeft: true, Assigned: false, Assignedby: false, TimeElapsed: false, subSection: true, PlanName: true, followup: true, NextFollowUpDate: true, updatedOn: false, updatedby: false }, editBtn: true, removeBtn: true, quickReopenbtn: false, navigateProfilebtn: true, historyBtn: true, TabStatus: true, DailyTableStatus: {}, predicate: {}, reset: { sort: {}, search: {}, pagination: { start: 0 } } },
    { TabName: "AllTasks", Displayed: [], Columns: { DaysLeft: true, Assigned: true, Assignedby: false, TimeElapsed: false, subSection: true, PlanName: true, followup: true, NextFollowUpDate: true, updatedOn: false, updatedby: false }, editBtn: true, removeBtn: true, quickReopenbtn: false, navigateProfilebtn: true, historyBtn: true, TabStatus: true, DailyTableStatus: {}, predicate: {}, reset: { sort: {}, search: {}, pagination: { start: 0 } } },
    { TabName: "ClosedTasks", Displayed: [], Columns: { DaysLeft: false, Assigned: true, Assignedby: false, TimeElapsed: false, subSection: true, PlanName: true, followup: true, NextFollowUpDate: false, updatedOn: false, updatedby: false }, editBtn: false, removeBtn: false, quickReopenbtn: true, navigateProfilebtn: true, historyBtn: true, TabStatus: true, DailyTableStatus: {}, predicate: {}, reset: { sort: {}, search: {}, pagination: { start: 0 } } },
    { TabName: "HistoryTasks", Displayed: [], Columns: { DaysLeft: false, Assigned: true, Assignedby: false, TimeElapsed: false, subSection: true, PlanName: false, followup: false, NextFollowUpDate: false, updatedOn: true, updatedby: true }, editBtn: false, removeBtn: false, quickReopenbtn: false, navigateProfilebtn: false, historyBtn: false, TabStatus: true, DailyTableStatus: {}, predicate: {}, reset: { sort: {}, search: {}, pagination: { start: 0 } } },
    { TabName: "AdminTasks", Displayed: [], Columns: { DaysLeft: true, Assigned: true, Assignedby: true, TimeElapsed: true, subSection: true, PlanName: true, followup: true, NextFollowUpDate: true, updatedOn: false, updatedby: false }, editBtn: false, removeBtn: false, quickReopenbtn: false, navigateProfilebtn: true, historyBtn: true, TabStatus: true, DailyTableStatus: {}, predicate: {}, reset: { sort: {}, search: {}, pagination: { start: 0 } } }
]);
trackerApp.factory('Resource', ['$q', '$rootScope', '$filter', '$timeout', function ($q, $rootScope, $filter, $timeout) {
    function getPage(start, number, params) {
        var deferred = $q.defer();
        $rootScope.filtered = params.search.predicateObject ? $filter('filter')($rootScope.Tasks, params.search.predicateObject) : $rootScope.Tasks;
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
trackerApp.filter("TimeDiff", function () {
    return function (followupDate) {
        var followup = moment(moment(followupDate).format('MM/DD/YYYY'));
        var currentDate = moment(moment(new Date()).format('MM/DD/YYYY'));
        var duration = moment.duration(followup.diff(currentDate));
        var days = duration.asDays();
        return Math.floor(days);
    }
})