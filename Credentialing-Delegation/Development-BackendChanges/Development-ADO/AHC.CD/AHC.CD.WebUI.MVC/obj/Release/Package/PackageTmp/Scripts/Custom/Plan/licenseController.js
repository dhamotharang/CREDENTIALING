//================= angular app healthApp licenseController.js controller licenseController==========================
healthApp.controller("licenseController", function ($scope) {
    $scope.StateLicense = 0;
    $scope.stateLicenseList = [];

    $scope.$watch("StateLicense", function (newValue, oldValue) {
       for (var i = oldValue; i < newValue; i++) {
          $scope.stateLicenseList.push(i);
       }
    });

    $scope.removeStateLicense = function (index) {
        for (var i in $scope.stateLicenseList) {
            if (index == i) {
                $scope.stateLicenseList.splice(index, 1);
                break;
            }
        }
    };
});