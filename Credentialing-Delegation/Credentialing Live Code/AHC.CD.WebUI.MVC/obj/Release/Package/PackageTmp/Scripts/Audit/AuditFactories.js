


AuditDashboard.factory('AuditDashboardFactory', ['$q', '$rootScope', '$filter', '$timeout', '$window', '$log', function ($q, $rootScope, $filter, $timeout, $window, $log) {

    function getPage(start, number, params) {
        var deferred = $q.defer();
        $rootScope.filtered = $filter('orderBy')($rootScope.TempAuditData, params.sort.predicate, params.sort.reverse);
        var result = $rootScope.filtered.slice(start, start + number);
        deferred.resolve({
            data: result,
            numberOfPages: Math.ceil($rootScope.filtered.length / number)
        });
        return deferred.promise;
    }

    function resetTableState(tableState) {
        tableState.sort = {};
        tableState.pagination.start = 0;
        tableState.search.predicateObject = {};
        return tableState;
    }
    function exportToTable(type, tableId) {
        switch (type) {
            case "Excel":
                angular.element(tableId).tableExport({ type: 'excel', escape: 'false' })
                break;
            case "CSV":
                angular.element(tableId).tableExport({ type: 'csv', escape: 'false'})
                break;
            case "Pdf":
                angular.element(tableId).tableExport({ type: 'pdf', pdfFontSize: '10', escape: 'false', htmlContent: 'true' })
                break;
        }
    }
    return {
        getPage: getPage,
        resetTableState: resetTableState,
        exportToTable: exportToTable
    }

}]);