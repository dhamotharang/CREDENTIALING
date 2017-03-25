function isNumber(n) {
    return !isNaN(parseFloat(n)) && isFinite(n);
}

//============================ Angular Module View Controller ==========================
var planApp = angular.module("planApp", ['ngTable', 'ngResource', 'ngMockE2E']);

//============================= run =====================================
planApp.run(function ($httpBackend, $filter, $log, ngTableParams) {
    // emulation of api server
    $httpBackend.whenGET(/data.*/).respond(function (method, url, data, headers) {
        var query = url.split('?')[1], requestParams = {};
        $log.log('Ajax request: ', url);
        var vars = query.split('&');
        for (var i = 0; i < vars.length; i++) {
            var pair = vars[i].split('=');
            requestParams[decodeURIComponent(pair[0])] = decodeURIComponent(pair[1]);
        }
        // parse url params
        for (var key in requestParams) {
            if (key.indexOf('[') >= 0) {
                var params = key.split(/\[(.*)\]/), value = requestParams[key], lastKey = '';
                angular.forEach(params.reverse(), function (name) {
                    if (name != '') {
                        var v = value;
                        value = {};
                        value[lastKey = name] = isNumber(v) ? parseFloat(v) : v;
                    }
                });
                requestParams[lastKey] = angular.extend(requestParams[lastKey] || {}, value[lastKey]);
            } else {
                requestParams[key] = isNumber(requestParams[key]) ? parseFloat(requestParams[key]) : requestParams[key];
            }
        }
        data = [{
            planName: "FREEDOM SAVINGS PLAN RX (HMO)",
            ICName: "FREEDOM",
            planType: "MEDICARE RISK",
            planStatus: "Active"
        }, {
            planName: "FREEDOM VIP CARE COPD (HMO SNP)",
            ICName: "FREEDOM",
            planType: "MEDICARE LOB",
            planStatus: "Draft"
        }, {
            planName: "OPTIMUM GOLD REWARDS PLAN (HMO-POS)",
            ICName: "OPTIMUM",
            planType: "MEDICARE RISK",
            planStatus: "Deactive"
        }, {
            planName: "OPTIMUM DIAMOND REWARDS (HMO-POS SNP)",
            ICName: "OPTIMUM",
            planType: "MEDICARE LOB",
            planStatus: "Active"
        }, {
            planName: "OPTIMUM PLATINUM PLAN (HMO-POS)",
            ICName: "OPTIMUM",
            planType: "MEDICARE RISK",
            planStatus: "Active"
        }];

        var params = new ngTableParams(requestParams);
        data = params.filter() ? $filter('filter')(data, params.filter()) : data;
        data = params.sorting() ? $filter('orderBy')(data, params.orderBy()) : data;
        var total = data.length;
        data = data.slice((params.page() - 1) * params.count(), params.page() * params.count());
        return [200, {
            result: data,
            total: total
        }];
    });
    $httpBackend.whenGET(/.*/).passThrough();
})
//============================= loading Container Directive =====================================
planApp.directive('loadingContainer', function () {
    return {
        restrict: 'A',
        scope: false,
        link: function (scope, element, attrs) {
            var loadingLayer = angular.element('<div class="loading"></div>');
            element.append(loadingLayer);
            element.addClass('loading-container');
            scope.$watch(attrs.loadingContainer, function (value) {
                loadingLayer.toggleClass('ng-hide', !value);
            });
        }
    };
})
//============================= Angular controller =====================================
planApp.controller('planController', function ($scope, $timeout, $resource, ngTableParams, $compile, $http) {

    var Api = $resource('/data');
    $scope.tableParams = new ngTableParams({
        page: 1, // show first page
        count: 10, // count per page
        sorting: {
            name: 'asc' // initial sorting
        }
    }, {
        total: 0, // length of data
        getData: function ($defer, params) {
            // ajax request to api
            Api.get(params.url(), function (data) {
                $timeout(function () {
                    // update table params
                    params.total(data.total);
                    // set new data
                    $defer.resolve(data.result);
                }, 500);
            });
        }
    });

    $scope.getPartialView = function (viewName) {

        $scope.newPlanPartialRender = 1;

        $("#newPlanDiv").load("/Plan/GetPartialView", { viewName: viewName }, function (response, status, xhr) {
            if (status == "success") {
                //var msg = "Sorry but there was an error: ";
                //$("#error").html(msg + xhr.status + " " + xhr.statusText);

                var element = $compile(response)($scope);

                $("#newPlanDiv").html(element);
                $('#newPlanModal').modal();
            }
        });

    };
});