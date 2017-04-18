CCMDashboard.service('CCMDashboardService', ['$http', '$q', '$log', '$window', function ($http, $q, $log, $window) {

    this.GetCCMAppointments = function () {
        var deferObject;
        deferObject = deferObject || $q.defer();
        var promise = $http.get(rootDir + '/Credentialing/CCMPortal/GetAllAppointmentsList');
        promise.then(function (results) {
            deferObject.resolve(results);
        },
        function (error) {
            deferObject.reject(error);
        });
        return deferObject.promise;
    }

}]);