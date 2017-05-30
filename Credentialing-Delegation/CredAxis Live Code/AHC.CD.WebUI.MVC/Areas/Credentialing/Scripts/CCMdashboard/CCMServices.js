CCMDashboard.service('CCMDashboardService', ['$http', '$q', '$log', '$window', function ($http, $q, $log, $window) {

    this.GetCCMAppointments = function () {
        var deferObject;
        deferObject = deferObject || $q.defer();
        var promise = $http.get(rootDir + '/Credentialing/CCM/GetAllAppointmentsList');
        promise.then(function (results) {
            deferObject.resolve(results);
        },
        function (error) {
            deferObject.reject(error);
        });
        return deferObject.promise;
    }
    this.GetAppointmentInfo = function (CredInfoId) {
        var deferObject;
        deferObject = deferObject || $q.defer();
        var promise = $http.get(rootDir + '/Credentialing/CCM/GetCCMActionData?CredInfoID=' + CredInfoId);
        promise.then(function (results) {
            deferObject.resolve(results);
        },
        function (error) {
            deferObject.reject(error);
        });
        return deferObject.promise;
    }

    this.GetProfileVerificationParameter = function () {
        var deferObject;
        deferObject = deferObject || $q.defer();
        var promise = $http.get(rootDir + '/Profile/MasterData/GetAllProfileVerificationParameter');
        promise.then(function (results) {
            deferObject.resolve(results);
        },
        function (error) {
            deferObject.reject(error);
        });
        return deferObject.promise;
    }

    this.GetPSVInfo = function (CredInfoId) {
        var deferObject;
        deferObject = deferObject || $q.defer();
        var promise = $http.get(rootDir + '/Credentialing/Verification/GetPSVReport?credinfoId=' + CredInfoId);
        promise.then(function (results) {
            deferObject.resolve(results);
        },
        function (error) {
            deferObject.reject(error);
        });
        return deferObject.promise;
    }
    this.GetDocumentsInfo = function (ProfileId) {
        var deferObject;
        deferObject = deferObject || $q.defer();
        var promise = $http.get(rootDir + '/Credentialing/DocChecklist/GetDocumentProfileDataAsync?profileId=' + ProfileId);
        promise.then(function (results) {
            deferObject.resolve(results);
        },
        function (error) {
            deferObject.reject(error);
        });
        return deferObject.promise;
    }

    this.SaveAppointmentResult = function (FormData) {
        var deferObject;
        deferObject = deferObject || $q.defer();
        var promise = $http({
            method: 'POST',
            url: rootDir + '/Credentialing/CCM/CCMActionUploadAsync',
            data: FormData,
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        });
        promise.then(function (answer) {
            deferObject.resolve(answer);
        },
        function (reason) {
            deferObject.reject(reason);
        });
        return deferObject.promise;
    }

    this.SaveQuickActionAppointments = function (FormData) {
        var deferObject;
        deferObject = deferObject || $q.defer();
        var promise = $http({
            method: 'POST',
            url: rootDir + '/Credentialing/CCM/SaveCCMQuickActionResultsAsync',
            data: FormData,
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        });
        promise.then(function (answer) {
            deferObject.resolve(answer);
        },
        function (reason) {
            deferObject.reject(reason);
        });
        return deferObject.promise;
    }

    


}]);