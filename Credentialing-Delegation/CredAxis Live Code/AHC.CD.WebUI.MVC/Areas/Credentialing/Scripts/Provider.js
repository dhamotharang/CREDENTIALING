angular.module('appList', []).controller('listController', function ($scope, $http) {

    $scope.allProviders = [];
    //
    $http.get(rootDir + "/InitCredentialing/GetAllProviders").success(function (data) {
       
        $scope.allProviders = data;
    });
});