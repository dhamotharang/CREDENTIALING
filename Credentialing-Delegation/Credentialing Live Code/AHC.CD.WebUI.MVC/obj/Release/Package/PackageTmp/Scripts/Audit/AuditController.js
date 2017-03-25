AuditDashboard.controller("AuditDashboardController", ["$rootScope", "$scope", "toaster", "$timeout", "$filter", "AuditDashboardService", "AuditDashboardFactory", "speech", function ($rootScope, $scope, toaster, $timeout, $filter, AuditDashboardService, AuditDashboardFactory, speech) {
    if ($rootScope.support) {
        speech.sayText("Welcome To User Activity Logger", $rootScope.config);
    }
    //================================= Variable Declaration Satrt====================================================================================

    var $self = this;
    this.displayed = [];
    $scope.tableCaption = "LOGS";
    //================================= Variable Declaration End====================================================================================



    //================================== Temporary function Declaration Start ===============================================================================


    

    this.callServer = function callServer(tableState) {
        $self.isLoading = true;
        var pagination = tableState.pagination;
        var start = pagination.start || 0;
        var number = pagination.number || 5;
        $scope.tableStateValue = tableState;
        AuditDashboardFactory.getPage(start, number, tableState).then(function (result) {
            $self.displayed = result.data;
            tableState.pagination.numberOfPages = result.numberOfPages;
            $self.isLoading = false;
        });

    };

    //--Method to check the pending item is selected or not

    $scope.GridData = function (type) {
        var TotalSpeech = "There are total " + $rootScope.TotalLog + "Activities Found";
        var InformationSpeech = "There are total " + $rootScope.Information + "Information Found";
        var AlertSpeech = "There are total " + $rootScope.Alert + "Suspicious Activities Found";
        switch (type) {
            case "LOGS":
                if ($rootScope.support) {
                    speech.sayText(TotalSpeech, $rootScope.config);
                }
                $rootScope.TempAuditData = $rootScope.AuditData;
                $scope.tableCaption = "LOGS";
                break;
            case "Information":
                if ($rootScope.support) {
                    speech.sayText(InformationSpeech, $rootScope.config);
                }
                $rootScope.TempAuditData = $filter('filter')($rootScope.AuditData, { Category: "Information" });
                $scope.tableCaption = "Information";
                break;
            case "Alert":
                if ($rootScope.support) {
                    speech.sayText(AlertSpeech, $rootScope.config);
                }
                $rootScope.TempAuditData = $filter('filter')($rootScope.AuditData, { Category: "Alert" });
                $scope.tableCaption = "Alert";
                break;
        }
        $scope.tableStateValue = AuditDashboardFactory.resetTableState($scope.tableStateValue);
        $self.callServer($scope.tableStateValue);
    }



    $scope.TableExport = function (type, tableID) {
        AuditDashboardFactory.exportToTable(type, tableID);
    }
}]);