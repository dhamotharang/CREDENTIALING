AuditDashboard.service('AuditDashboardService', ['$http', '$q', '$log', '$window', function ($http, $q, $log, $window) {

    this.GetAuditLogs = function () {
        var deferObject;
        deferObject = deferObject || $q.defer();
        var promise = $http.get(rootDir + '/Audit/GetAllAuditLog');
        promise.then(function (results) {
            deferObject.resolve(results);
        },
        function (error) {
            deferObject.reject(error);
        });
        return deferObject.promise;
    }
}]);