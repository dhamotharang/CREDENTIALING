var CCMDashboard = angular.module("CCMDashboard", ['toaster', 'smart-table', 'ui.rCalendar','ngSignaturePad', 'ngRoute', 'mgcrea.ngStrap', 'chieffancypants.loadingBar']);

CCMDashboard.run(["$rootScope", "$timeout", "$window", "$route", function ($rootScope, $timeout, $window, $route) {
    $route.reload();
    $rootScope.CCMAppointments = [];
    $rootScope.filtered = [];
    $rootScope.TempCCMAppointments = [];
    $rootScope.filteredCCMAppointmentsByDate = [];
    $rootScope.TempObjectForStatus = { CredentailingApprovalRequest: false, AppointmentDashboard: true, QuickApprovalAction: false, SingleDetailedApprovalAction: false };
    $rootScope.VisibilityControl = '';
    $rootScope.tableCaption = "Appointments";
}]);
CCMDashboard.config(["$routeProvider", "cfpLoadingBarProvider", "$httpProvider", function ($routeProvider, cfpLoadingBarProvider, $httpProvider) {
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
    cfpLoadingBarProvider.includeSpinner = true;
    $httpProvider.defaults.headers.common["X-Requested-With"] = 'XMLHttpRequest';
}]);