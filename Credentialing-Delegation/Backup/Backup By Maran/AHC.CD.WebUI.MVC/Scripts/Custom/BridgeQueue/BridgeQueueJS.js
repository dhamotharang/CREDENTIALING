//Script for Bridge Queue functionalities

var bridgeQueueApp = angular.module('bridgeQueueApp', ['ui.bootstrap', 'smart-table', 'mgcrea.ngStrap']);
bridgeQueueApp.factory('Resource', ['$q', '$rootScope', '$filter', '$timeout', function ($q, $rootScope, $filter, $timeout) {

    function getPage(start, number, params) {

        var deferred = $q.defer();

        $rootScope.filtered = params.search.predicateObject ? $filter('filter')($rootScope.BridgeQueueData, params.search.predicateObject) : $rootScope.BridgeQueueData;
        //for (var i in names) {

        //    sortData = ((typeof params.search.predicateObject != "undefined") && (params.search.predicateObject.hasOwnProperty(names[i]))) ? true : false;
        //    if (sortData == true) break;
        //}

        if (params.sort.predicate) {
            $rootScope.filtered = $filter('orderBy')($rootScope.filtered, params.sort.predicate, params.sort.reverse);
        }

        var result = $rootScope.filtered.slice(start, start + number);
        $timeout(function () {
            deferred.resolve({
                data: result,
                numberOfPages: Math.ceil($rootScope.filtered.length / number)
            });
        });


        return deferred.promise;
    }

    return {
        getPage: getPage
    };


}]);
bridgeQueueApp.directive('stRatio', function () {
    return {
        link: function (scope, element, attr) {
            var ratio = +(attr.stRatio);

            element.css('width', ratio + '%');

        }
    };
});

bridgeQueueApp.directive('pageSelect', function () {
    return {
        restrict: 'E',
        template: '<input type="text" class="select-page" ng-model="inputPage" ng-change="PageResize(inputPage,numPages)">',
        controller: function ($scope) {
            $scope.$watch('inputPage', function (newV, oldV) {
                if (newV === oldV) {
                    return;
                }
                else if (newV >= oldV) {
                    $scope.inputPage = newV;
                    $scope.selectPage(newV);
                    //$scope.currentPage = newV;
                }
                else {
                    $scope.selectPage(newV);
                }

            });
            $scope.PageResize = function (currentPage, maxPage) {
                if (currentPage >= maxPage) {
                    $scope.inputPage = maxPage;
                    $scope.selectPage(maxPage);
                }
                else {
                    $scope.selectPage(currentPage);
                }
            }
        },
        link: function (scope, element, attrs) {
            scope.$watch('currentPage', function (c) {
                scope.inputPage = c;
            });
        }
    }
});



bridgeQueueApp.run(function ($rootScope, $http) {
    $rootScope.BridgeQueueData = [];
    $rootScope.JsonData = [];
    $http.get(rootDir + '/BridgeQueue/GetData').
            success(function (data) {
                $rootScope.JsonData = data;
                console.log("DATA ==================");
                console.log($rootScope.BridgeQueueData);
                $rootScope.BridgeQueueData = angular.copy($rootScope.JsonData);
                $rootScope.tabName('Open', 'RequestOpen');
            }).
            error(function (data) {
            });
    //$rootScope.BridgeQueueData = angular.copy($rootScope.JsonData);
    
});

//bridgeQueueApp.value("bridgeQueueData", $rootScope.BridgeQueueData);
bridgeQueueApp.value("tabCols", [{ tabName: "RequestOpen", TimeLeft: true, AppDate: false, RejDate: false, Assigned: false,filters:true },
                               { tabName: "RequestAssigned", TimeLeft: true, AppDate: false, RejDate: false, Assigned: true, filters: true },
                               { tabName: "RequestPending", TimeLeft: true, AppDate: false, RejDate: false, Assigned: true, filters: true },
                               { tabName: "Approved", TimeLeft: false, AppDate: false, RejDate: false, Assigned: false, filters: false }]);

bridgeQueueApp.filter("ShowTabCols", function (tabCols) {
    return function (status) {
        return tabCols.filter(function (data) { return data.tabName == status })[0];
    }
});


bridgeQueueApp.filter("GetTabData", function ($rootScope) {
    return function (status) {
        return $rootScope.JsonData.filter(function (data) { return data.QueueStatus.trim().toLowerCase() == status.trim().toLowerCase() });
    }
});

bridgeQueueApp.controller('BridgeQueueCtrl', function ($scope, $rootScope, $anchorScroll, $location, $http, $q, $filter, Resource) {
    var ctrl = this;
    this.displayed = [];
    $rootScope.ShowTempProfile = false;

    this.callServer = function callServer(tableState) {
        ctrl.isLoading = true;
        var pagination = tableState.pagination;
        console.log(pagination.number);
        var start = pagination.start || 0;
        var number = pagination.number || 10;
        $scope.t = tableState;
        Resource.getPage(start, number, tableState).then(function (result) {
            ctrl.displayed = result.data;
            ctrl.temp = ctrl.displayed;
            console.log(result.data);           
            tableState.pagination.numberOfPages = result.numberOfPages;//set the number of pages so the pagination can update
            ctrl.isLoading = false;
        });
    };    

    $rootScope.tabName = function (name, tabName) {
        $rootScope.ShowTempProfile = false;
        $rootScope.BridgeQueueData = angular.copy($filter('GetTabData')(name));
        $rootScope.ColStatus = angular.copy($filter('ShowTabCols')(tabName));
        //$rootScope.SwitchCount = $rootScope.BridgeQueueData.length;
        var tableState = {
            sort: {},
            search: {},
            pagination: {
                start: 0
            }
        };
        ctrl.callServer(tableState);
    }

    $rootScope.showDetailViewforTab2 = function () {
        $http.get(rootDir + '/BridgeQueue/GetTempProfile').
           success(function (data) {
               $rootScope.TempProfile = data;
               $rootScope.ShowTempProfile = true;
               $('.nav-tabs a:last').tab('show');
               $rootScope.ColStatus = angular.copy($filter('ShowTabCols')('Approved'));
           }).
           error(function (data) {
           });

    };
});

$(document).ready(function () {
    $("#sidemenu").addClass("menu-in");
    $("#page-wrapper").addClass("menuup");
});