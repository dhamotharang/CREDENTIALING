var CCMDashboard = angular.module("CCMDashboard", ['toaster', 'smart-table', 'ui.rCalendar', 'nvd3', 'ngSignaturePad', 'ngRoute', 'mgcrea.ngStrap']);

CCMDashboard.run(["$rootScope", "$timeout", "$window", "$route", function ($rootScope, $timeout, $window, $route) {
    $route.reload();
    $rootScope.CCMAppointments = [];
    $rootScope.filtered = [];
    $rootScope.TempCCMAppointments = [];
    $rootScope.filteredCCMAppointmentsByDate = [];
    $rootScope.TempObjectForStatus = { CredebtailingApprovalRequest: false, AppointmentDashboard: true, QuickApprovalAction: false, SingleDetailedApprovalAction: false };
    $rootScope.VisibilityControl = '';
}]);
CCMDashboard.config(["$routeProvider", function ($routeProvider) {
    $routeProvider
    .when("/CCM_ACTION", {
        templateUrl: "/Credentialing/CCMPortal/SPA_CCMAction",
        controller: "SPAIndexController"
    })
    .when("/CCM_PSV", {
        templateUrl: "/Credentialing/CCMPortal/SPA_PSV",
        controller: "SPAIndexController"
    })
    .when("/CCM_DOCUMENTS", {
        templateUrl: "/Credentialing/CCMPortal/SPA_Document",
        controller: "SPAIndexController"
    })
    .otherwise({
        templateUrl: "/Credentialing/CCMPortal/SPA_CCMAction",
        controller: "SPAIndexController"
    });
}]);