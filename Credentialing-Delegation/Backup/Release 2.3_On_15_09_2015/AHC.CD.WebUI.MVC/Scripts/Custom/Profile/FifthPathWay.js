var profileApp = angular.module('profileApp', []);

profileApp.controller('fifthPathCtrl', function ($scope) {

    $scope.fifthPath = 0;
    $scope.fifthPaths = [];

    $scope.$watch("fifthPath", function (newValue,oldValue) {

        for (var i = oldValue; i < newValue; i++) {

            $scope.fifthPaths.push(i);
        }

    });


    $scope.deleteproffess = function (index) {
        for (var i in $scope.fifthPaths) {
            if (index == i) {
                $scope.fifthPaths.splice(index, 1);
                break;
            }
        }
    };


});