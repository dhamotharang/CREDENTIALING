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
        var url = StatusType == 'Active' ? '/Credentialing/RequestForApproval/GetCredRequestDataByID?ID=' : '/Credentialing/RequestForApproval/GetCredRequestHistoryDataByID?ID=';
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
            url: rootDir + data.Url + data.ProfileId,
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
        var deferred = $q.defer();
        var urlCalls = [];
        angular.forEach(data, function (d) {
            urlCalls.push($http.post(rootDir + d.url + data.ProfileId, d.NewData));
        });
        $q.all(urlCalls).then(function (results) {
            deferObject.resolve(results);
        },
        function (errors) {
            deferred.reject(errors);
        },
        function (updates) {
            deferred.update(updates);
        });
        return deferred.promise;
    }

}]);