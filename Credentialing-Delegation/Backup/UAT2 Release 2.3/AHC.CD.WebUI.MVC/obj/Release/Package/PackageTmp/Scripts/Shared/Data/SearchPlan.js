var searchApp = angular.module('searchApp',  ['ngTable']);
searchApp.controller('SearchPlanController', function ($scope, $filter, ngTableParams) {

    $scope.searchPlanData = [{
        "companyName": "Wellcare",
        "planName": "Plan1",
        "planCategory": "Type1",
        "providers": "A,B,C,D",
        "locations": "Florida,NewYork",
        "groupName": "Group1"
    }, {
        "companyName": "ACO",
        "planName": "Plan2",
        "planCategory": "Type2",
        "providers": "A,B,C,D",
        "locations": "Florida,NewYork",
        "groupName": "Group2"
    }, {
        "companyName": "Ultimate",
        "planName": "Plan1",
        "planCategory": "Type1",
        "providers": "A,B,C,D",
        "locations": "Florida,NewYork",
        "groupName": "Group1"
    },
    ];
    $scope.$watch("searchPlanData", function () {
        $scope.tableParams1.reload();
    });

    $scope.tableParams1 = new ngTableParams({
        page: 1,            // show first page
        count: 10,
        // count per page

    }, {
        total: $scope.searchPlanData.length, // length of data
        getData: function ($defer, params) {
            var filteredData = $scope.searchPlanData;
            //var filtered_data = params.filter() ? $filter('filter')(data, params.filter().myfilter) : data;
            var orderedData = params.filter() ?
                                $filter('filter')(filteredData, params.filter()) :
                                filteredData;

            $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
        },
        $scope: { $searchPlanData: {} }
    });

});