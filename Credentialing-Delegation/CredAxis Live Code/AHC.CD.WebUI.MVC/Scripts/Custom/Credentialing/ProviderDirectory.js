/* 
    Provider Directory Angular Module and Controller
    Author : Kalaram Verma
*/


(function () {
    "use strict";

    $(function () {

        $('.single_dual').on('click', function () {
            $('.single_dual').each(function () {
                $(this).removeClass('active').addClass('notActive');
            });
            $(this).removeClass('notActive').addClass('active');
        })

        $('.biscuit').on('click', function () {
            $('.biscuit').each(function () {
                $(this).removeClass('active');
            });
            $(this).addClass('active');
        })

        $('.provider_active').on('click', function () {
            $('.provider_active').each(function () {
                $(this).removeClass('active').addClass('notActive');
            });
            $(this).removeClass('notActive').addClass('active');
        })

        var options = [];

        $('.customize_column a').on('click', function (event) {

            var $target = $(event.currentTarget),
                val = $target.attr('data-value'),
                $inp = $target.find('input'),
                idx;

            if ((idx = options.indexOf(val)) > -1) {
                options.splice(idx, 1);
                setTimeout(function () { $inp.prop('checked', false) }, 0);
            } else {
                options.push(val);
                setTimeout(function () { $inp.prop('checked', true) }, 0);
            }

            $(event.target).blur();

            console.log(options);
            return false;
        });

    });



    // Provider Directory Module
    var PDApp = angular.module("PDApp", ["ngTable"]);

    // Provider Directory Controller
    PDApp.controller("PDController", PDController);

    // Provider Directory Controller Injection
    PDController.$inject = ["$scope", "$http", "$filter", "NgTableParams"];

    function PDController($scope, $http, $filter, NgTableParams) {

        $scope.GetProviderData = function (count) {
            $http.get(rootDir + '/Prototypes/GetProviders?count=' + count).success(function (data) {
                console.log(data);
                $scope.init_table(data);
            }).error(function () {
                console.log('method not available.......');
            });
        };

        $scope.ProviderTitle = "Providers";

        //Created function to be called when data loaded dynamically
        $scope.init_table = function (data) {
            $scope.tableParams = new NgTableParams({}, {
                dataset: data
            });
        };

        $scope.GetSpecialities = function (con) {
            if (con == 2) {
                $scope.GetProviderData(35);
                $scope.ProviderTitle = "Dual Specialities";
            } else {
                $scope.GetProviderData(20);
                $scope.ProviderTitle = "Single Specialities";
            }
        };

        $scope.GetMidLevels = function () {
            $scope.GetProviderData(70);
            $scope.ProviderTitle = "Mid Levels";
        };

        $scope.GetPCPs = function () {
            $scope.GetProviderData(60);
            $scope.ProviderTitle = "PCPs";
        };

        $scope.GetAll = function () {
            $scope.GetProviderData(212);
            $scope.ProviderTitle = "Providers";
        };

        $scope.GetActiveInActive = function (con) {
            if (con == 2) {
                $scope.GetProviderData(36);
                $scope.ProviderTitle = "Inactive Providers";

            } else {
                $scope.GetProviderData(212);
                $scope.ProviderTitle = "Active Providers";
            }
        };

        //$scope.GetSpecialtiesData = function (count) {
        //    $http.get(rootDir + '/Prototypes/GetSpecialties').success(function (data) {
        //        console.log(data);
        //        $scope.Specialties = data;
        //        $scope.GetAll();
        //    }).error(function () {
        //        console.log('method not available.......');
        //    });
        //};

        $scope.GetAll();
        //$scope.GetSpecialtiesData();
    }

    //angular.module("PDApp").run(configureDefaults);
    //configureDefaults.$inject = ["ngTableDefaults"];

    //function configureDefaults(ngTableDefaults) {
    //    ngTableDefaults.params.count = 10;
    //    ngTableDefaults.settings.counts = [];
    //}
})();