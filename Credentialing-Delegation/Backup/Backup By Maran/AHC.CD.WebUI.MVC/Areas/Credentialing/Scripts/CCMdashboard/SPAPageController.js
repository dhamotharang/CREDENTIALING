CCMDashboard.controller("SPAIndexController", ["$rootScope", "$scope", "toaster", "$timeout", "$filter", "CCMDashboardService", "CCMDashboardFactory", function ($rootScope, $scope, toaster, $timeout, $filter, CCMDashboardService, CCMDashboardFactory) {
    $scope.Status = "dfsdf";
    $scope.SubmitApproval = function (ActionType) {
        $scope.Status = ActionType;
    };
    $scope.ConfirmAppointmentApproval = function () {
        $('#StatusAction').modal('hide');
        $('body').removeClass('modal-open');
        $('.modal-backdrop').remove();
    };
}]);