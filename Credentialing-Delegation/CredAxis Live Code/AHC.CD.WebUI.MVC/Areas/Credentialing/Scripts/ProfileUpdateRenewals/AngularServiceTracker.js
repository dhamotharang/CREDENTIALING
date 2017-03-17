var ServiceTracker = angular.module("ServiceTracker", []);
//ServiceTracker.config(["$httpProvider", function setupConfig($httpProvider) {
//    $httpProvider.interceptors.push('interceptHttp');
//}]);
ServiceTracker.service("tracking", function () {
    var total = {
        all: 0,
        get: 0,
        post: 0,
        delete: 0,
        put: 0,
        head: 0
    };
    var pending = {
        all: 0,
        get: 0,
        post: 0,
        delete: 0,
        put: 0,
        head: 0
    };
    function endRequest(httpMethod) {
        httpMethod = normalizedHttpMethod(httpMethod);
        pending.all--;
        pending[httpMethod]--;
        if (pending[httpMethod] < 0) {
            redistributePendingCounts(httpMethod);
        }
    }
    function startRequest(httpMethod) {
        httpMethod = normalizedHttpMethod(httpMethod);
        total.all++;
        total[httpMethod]++;
        pending.all++;
        pending[httpMethod]++;
    }
    function normalizedHttpMethod(httpMethod) {
        httpMethod = (httpMethod || "").toLowerCase();
        switch (httpMethod) {
            case "get":
            case "post":
            case "delete":
            case "put":
            case "head":
                return (httpMethod);
                break;
        }
        return ("get");
    }
    function redistributePendingCounts(negativeMethod) {
        var overflow = Math.abs(pending[negativeMethod]);
        pending[negativeMethod] = 0;
        var methods = ["get", "post", "delete", "put", "head"];
        for (var i = 0 ; i < methods.length ; i++) {
            var method = methods[i];
            if (overflow && pending[method]) {
                pending[method] -= overflow;
                if (pending[method] < 0) {
                    overflow = Math.abs(pending[method]);
                    pending[method] = 0;
                } else {
                    overflow = 0;
                }
            }
        }
    }
    return ({
        pending: pending,
        total: total,
        endRequest: endRequest,
        startRequest: startRequest,
    });
});
ServiceTracker.factory("interceptHttp", ["$q", "tracking", function ($q, tracking) {

    function request(config) {
        tracking.startRequest(config.method);
        return (config);
    }
    function requestError(rejection) {
        tracking.startRequest("get");
        return ($q.reject(rejection));
    }
    function response(response) {
        tracking.endRequest(extractMethod(response));
        return (response);
    }
    function responseError(response) {
        tracking.endRequest(extractMethod(response));
        return ($q.reject(response));
    }
    function extractMethod(response) {
        try {
            return (response.config.method);
        } catch (error) {
            return ("get");
        }
    }
    return {
        request: request,
        requestError: requestError,
        response: response,
        responseError: responseError
    };
}]);