//Temp data
var data1 = [];
//Toggle the search provider div
var SearchProviderPanelToggle = function () {
    $("#SearchProviderPanel").slideToggle();
    //($("#collapse_btn").text() == "[+]") ? $("#collapse_btn").text("[-]") : $("#collapse_btn").text("[+]");
}


//Module declaration
var providerapp = angular.module('ProviderApp', ['ngTable', 'ngTableResizableColumns']);
//Controller declaration
providerapp.controller('ProviderCtrl', function ($scope, $http, $location, $filter, ngTableParams) {
    $scope.data = []; //data in scope is declared
    $scope.progressbar = false;
    $scope.CategoryID = "1";
    $scope.ProviderStatus = "Active";
    $scope.error_message = "";
    $scope.advancesearchcaret = true;

    $scope.groupBySelected = 'none';

    //$scope.animateAdvanceSearch = function () {
    //    $("#advanceSearchPanel").slideToggle(function () {
    //        valuecaret = $("#advanceSearchPanel").css('display') == 'none' ? true : false;
    //        $scope.advancesearchcaret = valuecaret;
    //    });
    //}

    //$scope.$watch('groupBySelected', function (value) {

    //    if ($scope.groupBySelected != 'none') {
    //        console.log($scope.groupBySelected);
    //        $scope.groupByParam($scope.data);
    //    }
    //});

    //$scope.groupByParam = function (data) {

    //    $scope.tableParams2 = new ngTableParams({
    //        page: 1,            // show first page
    //        count: 10          // count per page
    //    }, {
    //        groupBy: function (item) { if ($scope.group == '') return null; else return item[$scope.groupBySelected]; },
    //        total: data.length,
    //        getData: function ($defer, params) {
    //            var orderedData = params.sorting() ?
    //                    $filter('orderBy')(data, $scope.tableParams2.orderBy()) :
    //                    data;
    //            params.$groups = $scope.groupBySelected
    //            $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
    //        }
    //    });
    //    $scope.tableParams2.reload();
    //}


    //Created function to be called when data loaded dynamically
    $scope.init_table = function (data) {


        $scope.tableParams1 = new ngTableParams({
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
                $scope.call_for_profile_update('');
                params.total(orderedData.length); // set total for recalc pagination
                $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
            }
        });
        //$scope.tableParams1.reload()
        //$scope.changeSelection = function (user) {
        //    console.info(user);
        //}

    };

    //if filter is on
    //$scope.ifFilter = function () {
    //    try {
    //        var bar;
    //        var obj = $scope.tableParams1.$params.filter;
    //        for (bar in obj) {
    //            if (obj[bar] != "") {
    //                return false;
    //            }
    //        }
    //        return true;
    //    }
    //    catch (e) { return true; }

    //}

    //Get index first in table
    //$scope.getIndexFirst = function () {
    //    try {
    //        if ($scope.groupBySelected == 'none') {
    //            return ($scope.tableParams1.$params.page * $scope.tableParams1.$params.count) - ($scope.tableParams1.$params.count - 1);
    //        } else {
    //            return ($scope.tableParams2.$params.page * $scope.tableParams2.$params.count) - ($scope.tableParams2.$params.count - 1);
    //        }
    //    }
    //    catch (e) { }
    //}
    //Get index Last in table
    //$scope.getIndexLast = function () {
    //    try {
    //        if ($scope.groupBySelected == 'none') {
    //            return { true: ($scope.data.length), false: ($scope.tableParams1.$params.page * $scope.tableParams1.$params.count) }[(($scope.tableParams1.$params.page * $scope.tableParams1.$params.count) > ($scope.data.length))];
    //        } else {
    //            return { true: ($scope.data.length), false: ($scope.tableParams2.$params.page * $scope.tableParams2.$params.count) }[(($scope.tableParams2.$params.page * $scope.tableParams2.$params.count) > ($scope.data.length))];
    //        }
    //    }
    //    catch (e) { }
    //}
    //Get data on basis of the parameters ajax call

    $(document).ready(function () {

        $scope.data = [];


        $http({
            method: "Get",
            url: rootDir + "/Profile/MasterProfile/GetAllProfileAsync"
        }).success(function (resultData) {
            console.log(resultData);
            if (resultData.length != 0) {
                $scope.data = resultData;
                $scope.init_table(resultData);
            }
            else {

            }

        }).error(function () { $scope.progressbar = false; $scope.error_message = "An Error occured !! Please Try Again !!"; })


    });
    var oldindex = '';
    //Call the update profile page for the row clicked
    $scope.call_for_profile_update = function (index) {
        if ("divval" + index == oldindex) {
            $("#" + oldindex).hide();
            oldindex = '';
        }
        else {
            $("#" + oldindex).hide();
            oldindex = "divval" + index;
            var heightdiv = $("#" + oldindex).parent().height() + "px";
            var paddingtop = parseInt((($("#" + oldindex).parent().height()) / 2) - 15) + "px";
            $("#" + oldindex).css('height', heightdiv).css('padding-top', paddingtop).slideDown();
        }
    }
})
