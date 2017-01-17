//================= Work History Controller ==========================
healthApp.controller('workhistoryController', function ($scope) {

    $scope.workDetails = 0;
    $scope.workDetailsList = [];

    $scope.$watch("workDetails", function (newValue, oldValue) {
        for (var i = oldValue; i < newValue; i++) {
            $scope.workDetailsList.push(i);
        }
    });

    $scope.deleteWork = function (index) {
        for (var i in $scope.workDetailsList) {
            if (index == i) {
                $scope.workDetailsList.splice(index, 1);
                break;
            }
        }
    };

});