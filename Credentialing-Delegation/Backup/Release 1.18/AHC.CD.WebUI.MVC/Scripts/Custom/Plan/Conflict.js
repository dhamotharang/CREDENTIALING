var planApp = angular.module('planApp', []);

planApp.controller('conflict', function ($scope) {

    // scope valiable init

    $scope.Conflict = 0;
    $scope.ConflictsList = [];


    // ProfessionalReference add-remove

    $scope.$watch("Conflict", function (newValue, oldValue) {
        for (var i = oldValue; i < newValue; i++) {
            $scope.ConflictsList.push(i);
        }
    });

    $scope.deleteConflict = function (index) {
        for (var i in $scope.ConflictsList) {
            if (index == i) {
                $scope.ConflictsList.splice(index, 1);
                break;
            }
        }
    };




});