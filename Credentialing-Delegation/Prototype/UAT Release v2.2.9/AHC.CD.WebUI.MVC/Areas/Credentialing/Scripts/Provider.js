angular.module('appList', []).controller('listController', function ($scope, $http) {

    $scope.allProviders = [];

    $http.get("/InitCredentialing/GetAllProviders").success(function (data) {
        console.log(data);
        $scope.allProviders = data;
    });
});