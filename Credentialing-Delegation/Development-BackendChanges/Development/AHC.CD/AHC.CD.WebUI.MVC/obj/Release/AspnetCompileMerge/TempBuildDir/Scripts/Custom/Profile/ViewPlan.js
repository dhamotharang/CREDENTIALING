
profileApp.controller('viewPlanAppCtrl', function ($scope, $filter, ngTableParams) {


    data = [{ PlanName: "Name1", PlanImage: "Freedom.jpg", PlanType: "Type1", Location: "Location1", Group: "Group1", Specialty: "Specialty1", Status: "Initiated" },
    { PlanName: "Name2", PlanImage: "Coventry.jpg", PlanType: "Type2", Location: "Location2", Group: "Group2", Specialty: "Specialty2", Status: "Submitted" },
    { PlanName: "Name3", PlanImage: "Humana.jpg", PlanType: "Type3", Location: "Location3", Group: "Group3", Specialty: "Specialty3", Status: "Initiated" },
    { PlanName: "Name4", PlanImage: "Optimum.jpg", PlanType: "Type4", Location: "Location4", Group: "Group4", Specialty: "Specialty4", Status: "Credentialed" },
    { PlanName: "Name5", PlanImage: "WellCare.jpg", PlanType: "Type5", Location: "Location5", Group: "Group5", Specialty: "Specialty5", Status: "Submitted" }
    ];

    $scope.data = data;

    $scope.tableParams = new ngTableParams({
        page: 1,            // show first page
        count: 10,          // count per page
        filter: {
            //name: 'M'       // initial filter
        },
        sorting: {
            //name: 'asc'     // initial sorting
        }
    }, {
        total: data.length, // length of data
        getData: function ($defer, params) {
            // use build-in angular filter
            var filteredData = params.filter() ?
                    $filter('filter')(data, params.filter()) :
                    data;
            var orderedData = params.sorting() ?
                    $filter('orderBy')(filteredData, params.orderBy()) :
                    data;

            params.total(orderedData.length); // set total for recalc pagination
            $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
        }
    });

    //if filter is on
    $scope.ifFilter = function () {
        try {
            var bar;
            var obj = $scope.tableParams.$params.filter;
            for (bar in obj) {
                if (obj[bar] != "") {
                    return false;
                }
            }
            return true;
        }
        catch (e) { return true; }
    }

    //Get index first in table
    $scope.getIndexFirst = function () {
        try {
            return ($scope.tableParams.$params.page * $scope.tableParams.$params.count) - ($scope.tableParams.$params.count - 1);
        }
        catch (e) { }
    }
    //Get index Last in table
    $scope.getIndexLast = function () {
        try {
            return { true: ($scope.data.length), false: ($scope.tableParams.$params.page * $scope.tableParams.$params.count) }[(($scope.tableParams.$params.page * $scope.tableParams.$params.count) > ($scope.data.length))];
        }
        catch (e) { }
    }

});