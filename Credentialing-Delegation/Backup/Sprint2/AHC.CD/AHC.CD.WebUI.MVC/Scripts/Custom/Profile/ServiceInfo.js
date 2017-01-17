
profileApp.controller('ServiceInfoController', function ($scope, $http, dynamicFormGenerateService) {

    $scope.MilitaryServiceInformations = [{
        MilitaryBranch: "Army",
        StartDate: "02-02-2003",
        EndDate: "04-02-2006"
    }];

    $scope.PublicHealthServices = [{
        Location: "Florida",
        StartDate: "01-01-2000",
        EndDate: "02-02-2010"
    }];


    //=============== Service Information Conditions ==================
    $scope.militaryServiceInformationFormStatus = false;
    $scope.newMilitaryServiceInformationForm = false;
    $scope.publicHealthServiceFormStatus = false;
    $scope.newPublicHealthServiceForm = false;
    $scope.showingDetails = false;

    //====================== Service Information ===================

    $scope.addMilitaryServiceInformation = function () {
        $scope.militaryServiceInformationFormStatus = false;
        $scope.newMilitaryServiceInformationForm = true;
        $scope.militaryServiceInformation = {};
        $("#newMilitaryServiceInformationFormDiv").html(dynamicFormGenerateService.getForm($scope, $("#militaryServiceInformationForm").html()));
    };

    $scope.addPublicHealthService = function () {
        $scope.publicHealthServiceFormStatus = false;
        $scope.newPublicHealthServiceForm = true;
        $scope.publicHealthService = {};
        $("#newPublicHealthServiceFormDiv").html(dynamicFormGenerateService.getForm($scope, $("#publicHealthServiceForm").html()));
    };

    $scope.saveMilitaryServiceInformation = function (militaryServiceInformation) {
        //================== Save Here ============
        //$scope.MilitaryServiceInformations.push(militaryServiceInformation);
        //================== hide Show Condition ============
        $scope.militaryServiceInformationFormStatus = false;
        $scope.newMilitaryServiceInformationForm = false;
        $scope.militaryServiceInformation = {};
    };

    $scope.savePublicHealthService = function (publicHealthService) {
        //================== Save Here ============
        //$scope.PublicHealthServices.push(publicHealthService);
        //================== hide Show Condition ============
        $scope.publicHealthServiceFormStatus = false;
        $scope.newPublicHealthServiceForm = false;
        $scope.publicHealthService = {};
    };

    $scope.updateMilitaryServiceInformation = function (militaryServiceInformation) {
        $scope.showingDetails = false;
        $scope.militaryServiceInformationFormStatus = false;
        $scope.newMilitaryServiceInformationForm = false;
        $scope.militaryServiceInformation = {};
    };

    $scope.updatePublicHealthService = function (publicHealthService) {
        $scope.showingDetails = false;
        $scope.publicHealthServiceFormStatus = false;
        $scope.newPublicHealthServiceForm = false;
        $scope.publicHealthService = {};
    };

    $scope.editMilitaryServiceInformation = function (index, militaryServiceInformation) {
        $scope.showingDetails = true;
        $scope.militaryServiceInformationFormStatus = true;
        $scope.newMilitaryServiceInformationForm = false;
        $scope.militaryServiceInformation = militaryServiceInformation;
        $("#militaryServiceInformationEditDiv" + index).html(dynamicFormGenerateService.getForm($scope, $("#militaryServiceInformationForm").html()));
    };

    $scope.editPublicHealthService = function (index, publicHealthService) {
        $scope.showingDetails = true;
        $scope.publicHealthServiceFormStatus = true;
        $scope.newPublicHealthServiceForm = false;
        $scope.publicHealthService = publicHealthService;
        $("#publicHealthServiceEditDiv" + index).html(dynamicFormGenerateService.getForm($scope, $("#publicHealthServiceForm").html()));
    };

    $scope.cancelMilitaryServiceInformation = function (condition) {
        if (condition == "editCancel") {
            $scope.showingDetails = false;
            $scope.militaryServiceInformationFormStatus = false;
            $scope.newMilitaryServiceInformationForm = false;
            $scope.militaryServiceInformation = {};
        } else {
            $scope.showingDetails = false;
            $scope.militaryServiceInformationFormStatus = false;
            $scope.newMilitaryServiceInformationForm = false;
            $scope.militaryServiceInformation = {};
        }
    };

    $scope.cancelPublicHealthService = function (condition) {
        if (condition == "editCancel") {
            $scope.showingDetails = false;
            $scope.publicHealthServiceFormStatus = false;
            $scope.newPublicHealthServiceForm = false;
            $scope.publicHealthService = {};
        } else {
            $scope.showingDetails = false;
            $scope.publicHealthServiceFormStatus = false;
            $scope.newPublicHealthServiceForm = false;
            $scope.publicHealthService = {};
        }
    };

    $scope.removeMilitaryServiceInformation = function (index) {
        for (var i in $scope.MilitaryServiceInformations) {
            if (index == i) {
                $scope.MilitaryServiceInformations.splice(index, 1);
                break;
            }
        }
    };

    $scope.removePublicHealthService = function (index) {
        for (var i in $scope.PublicHealthServices) {
            if (index == i) {
                $scope.PublicHealthServices.splice(index, 1);
                break;
            }
        }
    };
});