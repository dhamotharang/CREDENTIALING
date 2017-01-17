
//=========================== Controller declaration ==========================
profileApp.controller('identificationLicenseController', function ($scope, $http, dynamicFormGenerateService) {


    $scope.StateLicenses = [{
        Number: "63AB54",
        Type: "Type 1",
        Status: "Active",
        IssuingState: "Arizona",
        PracticeState: "Active",
        OriginalIssueDate: "04-03-2003",
        ExpirationDate: "23-Feb-2021",
        CurrentIssueDate: "11-11-2011",
        LicenseGoodStanding: "Y",
        LicenseRelinquished: "Y",
        Date: "31-01-2002",
        Certificate: "certificat.pdf"
    }];

    //=============== State License Conditions ==================
    $scope.stateLicenseFormStatus = false;
    $scope.newStateLicenseForm = false;
    $scope.showingDetails = false;

    $scope.addStateLicense = function () {
        $scope.stateLicenseFormStatus = false;
        $scope.newStateLicenseForm = true;
        $scope.StateLicense = {};
        $("#newStateLicenseFormDiv").html(dynamicFormGenerateService.getForm($scope, $("#stateLicenseForm").html()));
    };

    $scope.saveStateLicense = function (StateLicense) {
        //================== Save Here ============
        //$scope.StateLicenses.push(StateLicense);
        //================== hide Show Condition ============
        $scope.stateLicenseFormStatus = false;
        $scope.newStateLicenseForm = false;
        $scope.StateLicense = {};
    };

    $scope.updateStateLicense = function (StateLicense) {
        $scope.showingDetails = false;
        $scope.stateLicenseFormStatus = false;
        $scope.newStateLicenseForm = false;
        $scope.StateLicense = {};
    };

    $scope.editStateLicense = function (index, StateLicense) {
        $scope.showingDetails = true;
        $scope.stateLicenseFormStatus = true;
        $scope.newStateLicenseForm = false;
        $scope.StateLicense = StateLicense;
        $("#stateLicenseEditDiv" + index).html(dynamicFormGenerateService.getForm($scope, $("#stateLicenseForm").html()));
    };

    $scope.cancelStateLicense = function (condition) {
        if (condition == "editCancel") {
            $scope.showingDetails = false;
            $scope.stateLicenseFormStatus = false;
            $scope.newStateLicenseForm = false;
            $scope.StateLicense = {};
        } else {
            $scope.showingDetails = false;
            $scope.stateLicenseFormStatus = false;
            $scope.newStateLicenseForm = false;
            $scope.StateLicense = {};
        }
    };

    $scope.removeStateLicense = function (index) {
        for (var i in $scope.StateLicenses) {
            if (index == i) {
                $scope.StateLicenses.splice(index, 1);
                break;
            }
        }
    };
});


profileApp.controller('IdentificationLicense', function ($scope) {

    // scope valiable init

    $scope.stateLicenses = 0;
    $scope.stateLicensesList = [];


    // state license add-remove

    $scope.$watch("stateLicenses", function (newValue, oldValue) {
        for (var i = oldValue; i < newValue; i++) {
            $scope.stateLicensesList.push(i);
        }
    });

    $scope.deleteSateLicence= function (index) {
        for (var i in $scope.stateLicensesList) {
            if (index == i) {
                $scope.stateLicensesList.splice(index, 1);
                break;
            }
        }
    };




});