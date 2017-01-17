var emailconfigurationapp = angular.module('emailconfigurationapp', ['ngAnimate', 'toaster', 'ngTable']);
emailconfigurationapp.controller('emailconfigurationController', ['$scope', 'toaster', '$filter','ngTableParams', function ($scope, toaster,$filter, ngTableParams) {
    $scope.SaveConfiguration = function () {
        toaster.pop('Success', "Success", 'Configuration saved Successfully');
    }
    $scope.providersList = false;
    $scope.EmailConfiguration = false;
    $scope.Providers = [
        {
            NPINumber: '1212313123', Title: 'Medical Doctor', FirstName: 'Parikshit', LastName: 'Singh', Specialty: 'Allergy and Immunology'
        },
        {
            NPINumber: '1212313123', Title: 'Acupunturist', FirstName: 'dhfkhs', LastName: 'sdfsdf', Specialty: 'Allergy and Immunology'
        },
        {
            NPINumber: '1212313123', Title: 'Osteopathic Doctor', FirstName: 'sdfsdf', LastName: 'sdfsdf', Specialty: 'Allergy and Immunology'
        },
        {
            NPINumber: '1212313123', Title: 'ARNP', FirstName: 'sdfsdf', LastName: 'sdfsdf', Specialty: 'Allergy and Immunology'
        },
        {
            NPINumber: '1212313123', Title: 'Osteopathic Doctor', FirstName: 'sdfsddsf', LastName: 'sdfsdf', Specialty: 'Allergy and Immunology'
        }
    ];
    $scope.Intervals = [15, 30, 60, 90, 180];
    $scope.setInterval = function (id,index) {
        $('#' +id+ index).toggleClass('btn-primary');
    }
    $scope.new_search = function () {
        $scope.EmailConfiguration = false;
        $scope.init_table($scope.Providers);
        $scope.SearchProviderPanelToggle('AuditSearch');
        $scope.providersList = true;
    }

    $scope.init_table = function (data) {

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
    $scope.SearchProviderPanelToggle = function (divId) {
        $('div').tooltip.title = "show";
        $("#" + divId).slideToggle();
        $scope.initiate = false;
    };
    $scope.showConfiguration = function () {
        $scope.SearchProviderPanelToggle('SearchProviderResultPanel');
        $scope.EmailConfiguration = true;
    }

}]);
$(document).ready(function () {
    $('.ProviderTypeSelectAutoList1').hide();
    $(".interval").slider({
        ticks: [15, 30, 60, 90, 180],
        ticks_positions: [0, 20, 40, 70, 100],
        ticks_labels: ['15', '30', '60', '90', '180'],
        ticks_snap_bounds: 0
    });
});