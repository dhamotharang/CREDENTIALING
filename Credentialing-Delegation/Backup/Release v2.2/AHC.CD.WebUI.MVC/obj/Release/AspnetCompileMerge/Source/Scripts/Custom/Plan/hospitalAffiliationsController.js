//================= angular app healthApp licenseController.js controller licenseController==========================
healthApp.controller("HospitalAffiliationController", function ($scope) {
    $scope.hospitallocation = 0;
    $scope.hospitallocationList = [];

    $scope.$watch("hospitallocation", function (newValue, oldValue) {
        for (var i = oldValue; i < newValue; i++) {
            $scope.hospitallocationList.push(i);
        }
    });

    $scope.removeHospitallocation = function (index) {
        for (var i in $scope.hospitallocationList) {
            if (index == i) {
                $scope.hospitallocationList.splice(index, 1);
                break;
            }
        }
    };
});