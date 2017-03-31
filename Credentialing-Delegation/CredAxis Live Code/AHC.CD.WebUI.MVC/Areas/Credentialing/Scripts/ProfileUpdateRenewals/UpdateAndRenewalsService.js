UpdateAndRenewalsApp.service('UpdateAndRenewalsService', ['$http', '$q', function ($http, $q) {

    this.GetAllUpdatesAndRenewals = function () {
        var deferObject;
        deferObject = deferObject || $q.defer();
        var promise = $http.get(rootDir + '/Credentialing/RequestForApproval/GetAllUpdatesAndRenewals');
        promise.then(function (results) {
            deferObject.resolve(results);
        },
        function (error) {
            deferObject.reject(error);
        });
        return deferObject.promise;
    }

    this.GetAllCredentialingRequests = function () {
        var deferObject;
        deferObject = deferObject || $q.defer();
        var promise = $http.get(rootDir + '/Credentialing/RequestForApproval/GetAllCredentialingRequest');
        promise.then(function (results) {
            deferObject.resolve(results);
        },
        function (error) {
            deferObject.reject(error);
        });
        return deferObject.promise;
    }

    this.GetAllUpdatesAndRenewalsHistory = function () {
        var deferObject;
        deferObject = deferObject || $q.defer();
        var promise = $http.get(rootDir + '/Credentialing/RequestForApproval/GetAllHistory');
        promise.then(function (results) {
            deferObject.resolve(results);
        },
        function (error) {
            deferObject.reject(error);
        });
        return deferObject.promise;
    }

    this.GetProfileUpdateDataByID = function (data) {
        var deferObject;
        deferObject = deferObject || $q.defer();
        var promise = $http.post(rootDir + '/Credentialing/ProfileUpdates/GetDataById', data);
        promise.then(function (results) {
            deferObject.resolve(results);
        },
        function (error) {
            deferObject.reject(error);
        });
        return deferObject.promise;
    }

    this.GetCredRequestDataByID = function (ID, StatusType) {
        var deferObject;
        var url = StatusType == 'Pending' ? '/Credentialing/RequestForApproval/GetCredRequestDataByID?ID=' : '/Credentialing/RequestForApproval/GetCredRequestHistoryDataByID?ID=';
        deferObject = deferObject || $q.defer();
        var promise = $http.get(rootDir + url + ID);
        promise.then(function (results) {
            deferObject.resolve(results);
        },
        function (error) {
            deferObject.reject(error);
        });
        return deferObject.promise;
    }

    this.SetDecesionForCredRequestByID = function (data) {
        var deferObject;
        deferObject = deferObject || $q.defer();
        var promise = $http.post(rootDir + '/Credentialing/RequestForApproval/SetDecesionForCredRequestByID', data);
        promise.then(function (results) {
            deferObject.resolve(results);
        },
        function (error) {
            deferObject.reject(error);
        });
        return deferObject.promise;
    }

    this.SetApprovalForProfileUpdatesAndRenewal = function (data) {
        var deferObject;
        deferObject = deferObject || $q.defer();
        var promise = $http.post(rootDir + '/Credentialing/ProfileUpdates/SetApproval', data);
        promise.then(function (results) {
            deferObject.resolve(results);
        },
        function (error) {
            deferObject.reject(error);
        });
        return deferObject.promise;
    }

    this.ApprovalServices = function (data) {
        var deferObject;
        deferObject = deferObject || $q.defer();


        var promise = $http({
            method: 'POST',
            url: rootDir + data.Url + data.ProfileID,
            data: data.NewData,
            headers: { 'Content-Type': 'application/json; charset=utf-8' }
        });

        promise.then(function (results) {
            deferObject.resolve(results);
        },
        function (error) {
            deferObject.reject(error);
        });
        return deferObject.promise;
    }

    this.MultipleAsyncServices = function (data) {
        var deferredArray = $q.defer();
        var Pormises = [];
        function DeferredObjectMapper(data) {
            var deferred = $q.defer();
            $http.post(rootDir + data.Url + data.ProfileID,data.NewData).then(function (response) {
                deferred.resolve({ ID: data.ProfileUpdatesTrackerId ,Status:true})
            }, function (error) {
                deferred.resolve({ ID: null, Status: false });
            })
            return deferred.promise;
        }
        angular.forEach(data, function (d) {
            Pormises.push(DeferredObjectMapper(d));
        });
        $q.all(Pormises).then(function (results) {
            deferredArray.resolve(results);
        },
        function (errors) {
            deferredArray.reject(errors);
        });
        return deferredArray.promise;
    }

    this.SetMultipleApproval = function (data) {
        var deferObject;
        deferObject = deferObject || $q.defer();
        var promise = $http.post(rootDir + '/Credentialing/RequestForApproval/SetMultipleApproval', data);
        promise.then(function (results) {
            deferObject.resolve(results);
        },
        function (error) {
            deferObject.reject(error);
        });
        return deferObject.promise;
    }

}]);