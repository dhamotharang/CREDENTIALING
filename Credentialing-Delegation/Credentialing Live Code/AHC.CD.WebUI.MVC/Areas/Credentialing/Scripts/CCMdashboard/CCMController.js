CCMDashboard.controller("CCMDashboardController", ["$rootScope", "$scope", "toaster", "$timeout", "$filter", "CCMDashboardService", "CCMDashboardFactory", function ($rootScope, $scope, toaster, $timeout, $filter, CCMDashboardService, CCMDashboardFactory) {
    toaster.pop('Success', "Success", 'Welcome');
    var AppointmentDate = new Date();
    $scope.GridType = "";
    //================================== Temporary function Declaration Start ===============================================================================
    
    $scope.MasterData = function () {
        CCMDashboardService.GetCCMAppointments().then(function (result) {
            $rootScope.CCMAppointments = result.data;
            $rootScope.TempCCMAppointments = result.data;
            $scope.loadEvents();
            AppointmentDate = $filter('date')(AppointmentDate, "yyyy-MM-dd");
            $rootScope.filteredCCMAppointmentsByDate = $filter('CCMDashboardFilterByAppointmentDate')(AppointmentDate);
        }, function (error) {
            toaster.pop('error', "", 'Please try after sometime !!!');
        })
    }
    $rootScope.GridData = function (AppointmentType, RowObject) {
        $scope.GridType = AppointmentType;
        $rootScope.$broadcast('AppointmentsGrid', { type: AppointmentType, RowObject: RowObject });
    };
    //================================== Temporary function Declaration End ===============================================================================
    //===================================CalendarPlugIn Script=============================================================================================
    'use strict';
    $scope.changeMode = function (mode) {
        $scope.mode = mode;
    };
    $scope.selectedDate = new Date();
    $scope.today = function () {
        $scope.currentDate = new Date();
    };

    $scope.isToday = function () {
        var today = new Date(),
            currentCalendarDate = new Date($scope.currentDate);

        today.setHours(0, 0, 0, 0);
        currentCalendarDate.setHours(0, 0, 0, 0);

        return today.getTime() === currentCalendarDate.getTime();
    };

    $scope.loadEvents = function () {
        $scope.eventSource = createRandomEvents();
        console.log($scope.eventSource);
    };

    $scope.onEventSelected = function (event) {
        $scope.event = event;
    };

    $scope.onTimeSelected = function (selectedTime) {
        $scope.selectedDate = selectedTime;
        AppointmentDate = $filter('date')($scope.selectedDate, "yyyy-MM-dd");
        $rootScope.filteredCCMAppointmentsByDate = $filter('CCMDashboardFilterByAppointmentDate')(AppointmentDate);
    };

    function createRandomEvents() {
        var events = [];
        var startDay = Math.floor(Math.random() * 90) - 45;
        for (var i = 0; i < $rootScope.CCMAppointments.length; i++) {
            var Sdate = new Date($rootScope.CCMAppointments[i].AppointmentDate);
            var Edate = new Date($rootScope.CCMAppointments[i].AppointmentDate);
            startTime = new Date(Date.UTC(Sdate.getUTCFullYear(), Sdate.getUTCMonth(), Sdate.getUTCDate()));
            endTime = startTime + 1;
            events.push({
                title: "Provider Credentialing Examination",
                startTime: startTime,
                endTime: endTime,
                allDay: false
            });
        }
        return events;
    }
    //==================================================Script END=========================================================================================
}]);