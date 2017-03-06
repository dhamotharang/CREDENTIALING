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
    });

    // Provider Directory Module
    var PDApp = angular.module("PDApp", ["ngTable", "ui.bootstrap"]);

    // Provider Directory Controller
    PDApp.controller("PDController", PDController);

    // Provider Directory Controller Injection
    PDController.$inject = ["$scope", "$http", "$filter", "ngTableParams"];

    function PDController($scope, $http, $filter, ngTableParams) {
        var dualSpecialities = [
      { ProviderID: 4, Photo: '/Resources/Images/images (4).jpg', Name: 'Dr. Parikshit Singh', NPI: 3333333333, Specialty: 'Dentist, Neuro Surgon', Location: 'Alaska', Group: 'Access2' },
      { ProviderID: 6, Photo: '/Resources/Images/images (2).jpg', Name: 'Dr. Barbara Joy', NPI: 1223344556, Specialty: 'Dentist, Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' },
      { ProviderID: 1, Photo: "/Resources/Images/author.jpg", Name: 'Dr. Jennine Martin', NPI: 1223344556, Specialty: 'Dentist, Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' },
      { ProviderID: 2, Photo: '/Resources/Images/images (2).jpg', Name: 'Dr. Barbara Joy', NPI: 1111111111, Specialty: 'Dentist, Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' },
      { ProviderID: 3, Photo: '/Resources/Images/images (3).jpg', Name: 'Dr. Daina Jeccob', NPI: 2222222222, Specialty: 'Dentist, Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' },
      { ProviderID: 5, Photo: '/Resources/Images/images (1).jpg', Name: 'Dr. Sanjay Singh', NPI: 4444444444, Specialty: 'Dentist, Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' },
      { ProviderID: 8, Photo: '/Resources/Images/images (4).jpg', Name: 'Dr. Parikshit Singh', NPI: 1223344556, Specialty: 'Dentist, Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' },
      { ProviderID: 1, Photo: "/Resources/Images/author.jpg", Name: 'Dr. Jennine Martin', NPI: 1223344556, Specialty: 'Dentist, Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' },
      { ProviderID: 7, Photo: '/Resources/Images/images (3).jpg', Name: 'Dr. Daina Jeccob', NPI: 1223344556, Specialty: 'Dentist, Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' },
        ];

        var singleSpecialities = [
            { ProviderID: 1, Photo: "/Resources/Images/author.jpg", Name: 'Dr. Jennine Martin', NPI: 1223344556, Specialty: 'Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' },
      { ProviderID: 2, Photo: '/Resources/Images/images (2).jpg', Name: 'Dr. Barbara Joy', NPI: 1111111111, Specialty: 'Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' },
      { ProviderID: 3, Photo: '/Resources/Images/images (3).jpg', Name: 'Dr. Daina Jeccob', NPI: 2222222222, Specialty: 'Dentist', Location: 'Spring Hill, Florida', Group: 'Access' },
      { ProviderID: 5, Photo: '/Resources/Images/images (1).jpg', Name: 'Dr. Sanjay Singh', NPI: 4444444444, Specialty: 'Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' },
      { ProviderID: 7, Photo: '/Resources/Images/images (3).jpg', Name: 'Dr. Daina Jeccob', NPI: 1223344556, Specialty: 'Dentist', Location: 'Spring Hill, Florida', Group: 'Access' },
      { ProviderID: 1, Photo: "/Resources/Images/author.jpg", Name: 'Dr. Jennine Martin', NPI: 1223344556, Specialty: 'Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' },
      { ProviderID: 2, Photo: '/Resources/Images/images (2).jpg', Name: 'Dr. Barbara Joy', NPI: 1111111111, Specialty: 'Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' },
      { ProviderID: 4, Photo: '/Resources/Images/images (4).jpg', Name: 'Dr. Parikshit Singh', NPI: 3333333333, Specialty: 'Dentist', Location: 'Alaska', Group: 'Access2' },
      { ProviderID: 6, Photo: '/Resources/Images/images (2).jpg', Name: 'Dr. Barbara Joy', NPI: 1223344556, Specialty: 'Dentist', Location: 'Spring Hill, Florida', Group: 'Access' },
      { ProviderID: 8, Photo: '/Resources/Images/images (4).jpg', Name: 'Dr. Parikshit Singh', NPI: 1223344556, Specialty: 'Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' },
        ];

        var midLeve = [
      { ProviderID: 4, Photo: '/Resources/Images/images (4).jpg', Name: 'Dr. Parikshit Singh', NPI: 3333333333, Specialty: 'Dentist', Location: 'Alaska', Group: 'Access2' },
      { ProviderID: 6, Photo: '/Resources/Images/images (2).jpg', Name: 'Dr. Barbara Joy', NPI: 1223344556, Specialty: 'Dentist', Location: 'Spring Hill, Florida', Group: 'Access' },
      { ProviderID: 7, Photo: '/Resources/Images/images (3).jpg', Name: 'Dr. Daina Jeccob', NPI: 1223344556, Specialty: 'Dentist', Location: 'Spring Hill, Florida', Group: 'Access' },
      { ProviderID: 8, Photo: '/Resources/Images/images (4).jpg', Name: 'Dr. Parikshit Singh', NPI: 1223344556, Specialty: 'Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' },
        ];

        $scope.ProviderTitle = "Providers";

        //Created function to be called when data loaded dynamically
        $scope.init_table = function (data) {

            var counts = [10, 25, 50, 100];
            $scope.tableParams = null;
            $scope.tableParams = new ngTableParams({
                page: 2,            // show first page
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
                total: data.length, // length of data
                getData: function ($defer, params) {
                    // use build-in angular filter
                    var filteredData = params.filter() ?
                            $filter('filter')(data, params.filter()) :
                            data;
                    var orderedData = params.sorting() ?
                            $filter('orderBy')(filteredData, params.orderBy()) :
                            data;

                    params.total(orderedData.length);
                    $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                }
            });
            setTimeout(function () {
                $scope.$apply(function () {
                    $scope.tableParams.page(1);
                });
            }, 100);
        };

        $scope.GetSpecialities = function (con) {
            if (con == 2) {
                $scope.init_table(dualSpecialities);
                $scope.ProviderTitle = "Dual Specialities";
            } else {
                $scope.init_table(singleSpecialities);
                $scope.ProviderTitle = "Single Specialities";
            }
        };

        $scope.GetMidLevels = function () {
            $scope.init_table(angular.copy(dualSpecialities).concat(angular.copy(midLeve)));
            $scope.ProviderTitle = "Mid Levels";
        };

        $scope.GetPCPs = function () {
            $scope.init_table(angular.copy(midLeve).concat(angular.copy(singleSpecialities)));
            $scope.ProviderTitle = "PCPs";
        };

        $scope.GetAll = function () {
            $scope.init_table(angular.copy(dualSpecialities).concat(angular.copy(singleSpecialities)).concat(angular.copy(midLeve)));
            $scope.ProviderTitle = "Providers";
        };

        $scope.GetActiveInActive = function (con) {
            if (con == 2) {
                $scope.GetMidLevels();
                $scope.ProviderTitle = "Inactive Providers";

            } else {
                $scope.GetAll();
                $scope.ProviderTitle = "Active Providers";
            }
        };

        $scope.GetSpecialities(null);
    }
})();