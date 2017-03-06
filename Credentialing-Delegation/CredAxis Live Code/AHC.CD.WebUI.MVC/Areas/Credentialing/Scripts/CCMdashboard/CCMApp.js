var CCMDashboard = angular.module("CCMDashboard", ['toaster', 'smart-table', 'ui.rCalendar', 'nvd3', 'ngSignaturePad','ngRoute']);

CCMDashboard.run(["$rootScope", "$timeout", "$window", function ($rootScope, $timeout, $window) {
    $rootScope.CCMAppointments = [];
    $rootScope.filtered = [];
    $rootScope.TempCCMAppointments = [];
    $rootScope.filteredCCMAppointmentsByDate = [];
    $rootScope.TempObjectForStatus = { CredebtailingApprovalRequest: false, AppointmentDashboard: true, QuickApprovalAction: false };
}]);
CCMDashboard.config(["$routeProvider", function ($routeProvider) {
    $routeProvider
    .when("/", {
        templateUrl: "main.htm"
    })
    .when("/red", {
        templateUrl: "red.htm"
    })
    .when("/green", {
        templateUrl: "green.htm"
    })
    .when("/blue", {
        templateUrl: "blue.htm"
    });
}]);

