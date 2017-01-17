
//--------------------- Angular Module ----------------------
var masterPlans = angular.module("verificationApp", []);

masterPlans.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {


}])

//=========================== Controller declaration ==========================
masterPlans.controller('verificationController', ['$scope', '$http', '$filter', '$rootScope', 'messageAlertEngine', function ($scope, $http, $filter, $rootScope, messageAlertEngine) {
    $scope.verificationData;
    $scope.getVerificationLinks1 = function () {
        $http.get(rootDir + '/Profile/MasterData/GetAllVerificationLinks')
        .success(function (data) {
            $scope.verificationData = data;
        }).
         error(function (data, status, headers, config) {
         });
    }
}]);



