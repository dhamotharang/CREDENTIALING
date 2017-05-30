CCMDashboard.controller("CCMDashboardController", ["$rootScope", "$scope", "toaster", "$timeout", "$filter", "CCMDashboardService", "CCMDashboardFactory", "$http", "$location", function ($rootScope, $scope, toaster, $timeout, $filter, CCMDashboardService, CCMDashboardFactory, $http, $location) {
    toaster.pop('Success', "Success", 'Welcome');
    var AppointmentDate = new Date();
    var infoId = 0;
    var profileId = 0;
    $scope.GridType = "";
    $scope.BisuctCounts = { All: 0, Approved: 0, Rejected: 0, Pending: 0 };
    //================================== Temporary function Declaration Start ===============================================================================

    $rootScope.MasterData = function () {
        CCMDashboardService.GetCCMAppointments().then(function (result) {
            console.log(result);
            $scope.BisuctCounts = CCMDashboardFactory.LoadCounts(result.data.AppointmentsInfo);
         
            for (var i in result.data.AppointmentsInfo) {
                result.data.AppointmentsInfo[i].AppointmentDate = CCMDashboardFactory.ConvertDate(result.data.AppointmentsInfo[i].AppointmentDate);
                result.data.AppointmentsInfo[i].CredInitiationDate = CCMDashboardFactory.ConvertDate(result.data.AppointmentsInfo[i].CredInitiationDate);
            }
            $rootScope.CCMAppointments = result.data.AppointmentsInfo;
            $rootScope.SignaturePath = result.data.SignaturePath;
            $scope.loadEvents();
            $rootScope.TempCCMAppointments = result.data;
            if ($rootScope.tableCaption!="") { CCMDashboardFactory.ResetTable() };
            AppointmentDate = $filter('date')(AppointmentDate, "yyyy-MM-dd");
            $rootScope.filteredCCMAppointmentsByDate = $filter('CCMDashboardFilterByAppointmentDate')(AppointmentDate);
        }, function (error) {
            toaster.pop('error', "", 'Please try after sometime !!!');
        });
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


   
    $rootScope.tempObject = {};
    //-------- Method to fetch the CCM action result ----
    $scope.GetCredentialingDetails = function (ProviderCredentialingInfoID, ProfileId) {
        $location.url('/CCM_ACTION');
        $rootScope.tempObject = {};
        $rootScope.ProfileId = ProfileId;
        infoId = ProviderCredentialingInfoID;
        profileId = ProfileId
        CCMDashboardService.GetAppointmentInfo(ProviderCredentialingInfoID).then(function (response) {
            //$rootScope.tempObject = angular.copy(response.data);
            CCMDashboardFactory.FormatCCMdata(response.data);
        }, function (error) {
            toaster.pop('error', "", 'Please try after sometime !!!');
        });
    };
    //--------- Method to fetch the PSV Result ----------
    $scope.GetPSVDetails = function () {
        if ($rootScope.tempObject.PSVdata != undefined) {
            return;
        }
        CCMDashboardService.GetProfileVerificationParameter().then(function (response) {
            CCMDashboardFactory.FormatPSVId(response.data);
        });

        CCMDashboardService.GetPSVInfo(infoId).then(function (response) {
            response.data.psvReport = JSON.parse(response.data.psvReport);
            CCMDashboardFactory.FormatPSVData(response.data.psvReport);
        }, function (error) {
            toaster.pop('error', "", 'Please try after sometime !!!');
        });
    };
    //--------- Method to fetch the Documents -----------
    $scope.GetDocumentsDetails = function () {
        if ($rootScope.tempObject.Docs != undefined) {
            return;
        }
        CCMDashboardService.GetDocumentsInfo(profileId).then(function (response) {
            CCMDashboardFactory.FormatDocuments(response.data);
        }, function (error) {
            toaster.pop('error', "", 'Please try after sometime !!!');
        });
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
