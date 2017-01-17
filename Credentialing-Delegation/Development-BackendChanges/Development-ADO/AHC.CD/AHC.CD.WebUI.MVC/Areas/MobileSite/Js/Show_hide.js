var myApp = angular.module('myApp', []);


function MyCtrl($scope) {

    $scope.changeme = function () {
        alert('here');
    }
}