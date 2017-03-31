var AuditDashboard = angular.module("AuditDashboard", ['toaster', 'smart-table', 'ui.rCalendar', 'AngularSpeach']);

AuditDashboard.run(["$rootScope", "$timeout", "$window", "$filter", "speech", function ($rootScope, $timeout, $window, $filter, speech) {
    $rootScope.support = false;
    $rootScope.AuditData = [];
    $rootScope.filtered = [];
    $rootScope.TempAuditData = [];
    $rootScope.AuditData = AuditLogData;
    $rootScope.TempAuditData = AuditLogData;

    $rootScope.TotalLog = AuditLogData.length;
    $rootScope.Information = $filter('filter')($rootScope.AuditData, { Category: "Information" }).length;
    $rootScope.Alert = $filter('filter')($rootScope.AuditData, { Category: "Alert" }).length;
    if ($window.speechSynthesis) {
        $rootScope.support = true;
        $timeout(function () {
            $rootScope.voices = speech.getVoices();
        }, 500);
    }
    $rootScope.config = {
        rate: 1,
        pitch: 1,
        volume: 1
    };

}])


