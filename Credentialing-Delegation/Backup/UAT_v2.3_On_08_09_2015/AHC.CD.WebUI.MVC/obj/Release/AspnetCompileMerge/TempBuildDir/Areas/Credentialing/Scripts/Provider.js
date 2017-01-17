angular.module('appList', []).controller('listController', function ($scope, $http) {

    $scope.allProviders = [];

    $http.get(rootDir + "/InitCredentialing/GetAllProviders").success(function (data) {
        console.log(data);
        $scope.allProviders = data;
    });
});