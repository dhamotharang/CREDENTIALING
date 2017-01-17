//--------------------- Angular Module ----------------------
var deCredentialingList = angular.module('DeCredentialingList', ['ngTable', 'ngTableResizableColumns']);



//=========================== Controller declaration ==========================
deCredentialingList.controller('deCredentialingListController', function ($scope, $http, $filter, $rootScope, ngTableParams) {

    $scope.data = [];
    $scope.groupBySelected = "none";
    $scope.ConvertDateFormat = function (value) {
        ///console.log(value);
        var returnValue = value;
        try {
            if (value.indexOf("/Date(") == 0) {
                returnValue = new Date(parseInt(value.replace("/Date(", "").replace(")/", ""), 10));
            }
            return returnValue;
        } catch (e) {
            return returnValue;
        }
        return returnValue;
    };

    $scope.ConvertDateFormat1 = function (value) {
        var shortDate = null;
        if (value) {
            var regex = /-?\d+/;
            var matches = regex.exec(value);
            var dt = new Date(parseInt(matches[0]));
            var month = dt.getMonth() + 1;
            var monthString = month > 9 ? month : '0' + month;
            var day = dt.getDate();
            var dayString = day > 9 ? day : '0' + day;
            var year = dt.getFullYear();
            shortDate = monthString + '/' + dayString + '/' + year;
        }
        return shortDate;
    };

    $http.get(rootDir + '/Credentialing/Initiation/GetAllDeCredentialings').
     success(function (data, status, headers, config) {

         $scope.data = data;

         for (var i = 0; i < $scope.data.length ; i++) {
             $scope.data[i].InitiationDate = $scope.ConvertDateFormat1($scope.data[i].InitiationDate);
             $scope.data[i].FirstName = $scope.data[i].Profile.PersonalDetail.FirstName;
             $scope.data[i].LastName = $scope.data[i].Profile.PersonalDetail.LastName;
             $scope.data[i].PlanName = $scope.data[i].Plan.PlanName;
         }         
         $scope.init_table(data);
     }).
     error(function (data, status, headers, config) {
         console.log("Error fetching data");
     });

    //table

    //Created function to be called when data loaded dynamically
    $scope.init_table = function (data, condition) {

        $scope.data = data;
        var counts = [];

        if ($scope.data.length <= 10) {
            counts = [];
        }
        else if ($scope.data.length <= 25) {
            counts = [10, 25];
        }
        else if ($scope.data.length <= 50) {
            counts = [10, 25, 50];
        }
        else if ($scope.data.length <= 100) {
            counts = [10, 25, 50, 100];
        }
        else if ($scope.data.length > 100) {
            counts = [10, 25, 50, 100];
        }
        console.log($scope.data);
        if (condition == 1) {
            for (var i = 0; i < $scope.data.length; i++) {
                $scope.data[i].FirstName = ""; //initialization of new property 
                $scope.data[i].LastName = "";
                $scope.data[i].Plan.PlanTitle = "";
                $scope.data[i].InitiationDate = "";
                $scope.data[i].Status = "";


            }
        }

        $scope.tableParams1 = new ngTableParams({
            page: 1,            // show first page
            count: 10,          // count per page
            filter: {
                //name: 'M'       // initial filter
                //FirstName : ''
            },
            sorting: {
                //name: 'asc'     // initial sorting
            }
        }, {
            counts: counts,
            total: $scope.data.length, // length of data
            getData: function ($defer, params) {
                // use build-in angular filter
                var filteredData = params.filter() ?
                        $filter('filter')($scope.data, params.filter()) :
                        $scope.data;
                var orderedData = params.sorting() ?
                        $filter('orderBy')(filteredData, params.orderBy()) :
                        $scope.data;

                params.total(orderedData.length); // set total for recalc pagination
                $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
            }
        });

    };


    //if filter is on
    $scope.ifFilter = function () {
        try {
            var bar;
            var obj = $scope.tableParams1.$params.filter;
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
            if ($scope.groupBySelected == 'none') {
                return ($scope.tableParams1.$params.page * $scope.tableParams1.$params.count) - ($scope.tableParams1.$params.count - 1);
            }
        }
        catch (e) { }
    }
    //Get index Last in table
    $scope.getIndexLast = function () {
        try {
            if ($scope.groupBySelected == 'none') {
                return { true: ($scope.data.length), false: ($scope.tableParams1.$params.page * $scope.tableParams1.$params.count) }[(($scope.tableParams1.$params.page * $scope.tableParams1.$params.count) > ($scope.data.length))];
            }
        }
        catch (e) { }
    }


}
);
