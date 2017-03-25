PlanApp.service('PlanService', ['$http', '$q', '$log', '$window', function ($http, $q, $log, $window) {

    this.ViewPlanDataByID = function (ID) {
            var deferObject;
            deferObject = deferObject || $q.defer();
            var promise = $http.get(rootDir + '/Plans/PlanList/GetPlanDataByID?ID='+ID);
            promise.then(function (answer) {
                deferObject.resolve(answer);
            },
            function (reason) {
                deferObject.reject(reason);
            });
        return deferObject.promise;
    }

    this.RemovePlanByID = function (ID) {
        var deferObject;
        deferObject = deferObject || $q.defer();
        var promise = $http.post(rootDir + '/Plans/PlanList/RemovePlanDataByID?ID='+ID);
        promise.then(function (answer) {
            deferObject.resolve(answer);
        },
        function (reason) {
            deferObject.reject(reason);
        });
        return deferObject.promise;
    }

    this.ReactivePlanByID = function (ID) {
        var deferObject;
        deferObject = deferObject || $q.defer();
        var promise = $http.post(rootDir + '/Plans/PlanList/ReactivePlanDataByID?ID=' + ID);
        promise.then(function (answer) {
            deferObject.resolve(answer);
        },
        function (reason) {
            deferObject.reject(reason);
        });
        return deferObject.promise;
    }

    this.UpdatePlanData = function (FormData) {
        var deferObject;
        deferObject = deferObject || $q.defer();
        var promise = $http({
            method: 'POST',
            url: rootDir + '/Plans/PlanList/UpdatePlanData',
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

    this.AddPlanData = function (FormData) {
        var deferObject;
        deferObject = deferObject || $q.defer();
        var promise = $http({
            method: 'POST',
            url: rootDir + '/Plans/PlanList/SavePlanData',
            data: FormData,
            transformRequest: angular.identity,
            headers:{'Content-Type': undefined}
        });
        promise.then(function (answer) {
            deferObject.resolve(answer);
        },
        function (reason) {
            deferObject.reject(reason);
        });
        return deferObject.promise;
    }

    this.getLocations = function (QueryString) {
        var deferObject;
        deferObject = deferObject || $q.defer();
        var promise = $http.get(rootDir + "/Location/GetCities?city=" + QueryString);
        promise.then(function (answer) {
            deferObject.resolve(answer);
        },
        function (reason) {
            deferObject.reject(reason);
        });
        return deferObject.promise;
    }

    this.MasterDataForPlan = function () {
        var deferObject;
        deferObject = deferObject || $q.defer();
        var promise = $http.get(rootDir + '/Plans/PlanList/LoadMasterDataForPlan');
        promise.then(function (answer) {
            deferObject.resolve(answer);
        },
        function (reason) {
            deferObject.reject(reason);
        });
        return deferObject.promise;
    }

    this.AddPlanContracts = function (PlanContracts) {
        var deferObject;
        deferObject = deferObject || $q.defer();
        var promise = $http.post(rootDir + '/Plans/PlanList/AddPlanContract', { PlanContracts: PlanContracts, async: false })
        promise.then(function (answer) {
            deferObject.resolve(answer);
        },
        function (reason) {
            deferObject.reject(reason);
        });
        return deferObject.promise;
    }

    this.UpdatePlanContracts = function (PlanID, PlanContracts) {
        var deferObject;
        deferObject = deferObject || $q.defer();
        var promise = $http.post(rootDir + '/Plans/PlanList/UpdatePlanContract', { PlanID: PlanID, PlanContracts: PlanContracts,async:false})
        promise.then(function (answer) {
            deferObject.resolve(answer);
        },
        function (reason) {
            deferObject.reject(reason);
        });
        return deferObject.promise;
    }

    this.GetPlanContarctDataByID = function (PlanID) {
        var deferObject;
        deferObject = deferObject || $q.defer();
        var promise = $http.get(rootDir + '/Plans/PlanList/GetPlanContarctDataByID?PlanID=' + PlanID);
        promise.then(function (answer) {
            deferObject.resolve(answer);
        },
        function (reason) {
            deferObject.reject(reason);
        });
        return deferObject.promise;
    }

}]);