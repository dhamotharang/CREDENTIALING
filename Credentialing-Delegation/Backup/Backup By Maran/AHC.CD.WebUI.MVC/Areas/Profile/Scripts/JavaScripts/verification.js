
//--------------------- Angular Module ----------------------
var masterPlans = angular.module("verificationApp", []);

masterPlans.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {


}])

//=========================== Controller declaration ==========================
masterPlans.controller('verificationController', ['$scope', '$http', '$filter', '$rootScope', 'messageAlertEngine', function ($scope, $http, $filter, $rootScope, messageAlertEngine) {

    $scope.verificationData = [];
    $http.get(rootDir + "/MasterDataNew/GetAllVerificationLinks").success(function (value) {
        $scope.verificationData = angular.copy(value);
    })
}]);