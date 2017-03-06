var CCMDashboard = angular.module("CCMDashboard", ['toaster', 'smart-table', 'ui.rCalendar', 'nvd3']);

CCMDashboard.run(["$rootScope", "$timeout", "$window", function ($rootScope, $timeout, $window) {
    $rootScope.CCMAppointments = [];
    $rootScope.filtered = [];
    $rootScope.TempCCMAppointments = [];
    $rootScope.filteredCCMAppointmentsByDate = [];
    $rootScope.TempObjectForStatus = { CredebtailingApprovalReqest: false, AppointmentDashboard: true };
}])


