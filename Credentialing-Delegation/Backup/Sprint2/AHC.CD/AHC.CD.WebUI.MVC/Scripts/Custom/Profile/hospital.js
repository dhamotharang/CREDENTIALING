
profileApp.controller('hospiatlcltr', function ($scope, $http, dynamicFormGenerateService) {

    $scope.HospitalPrivilegeInformations = [{
        HospitalName: "Florida Hospital",
        Specialty: "Dentist",
        StreetAddress: "601 East Rollins Street"
    }];

    //=============== Hospital Privilege Information Conditions ==================
    $scope.hospitalPrivilegeInformationFormStatus = false;
    $scope.newHospitalPrivilegeInformationForm = false;
    $scope.showingDetails = false;

    //====================== Hospital Privilege Information ===================

    $scope.addHospitalPrivilegeInformation = function () {
        $scope.hospitalPrivilegeInformationFormStatus = false;
        $scope.newHospitalPrivilegeInformationForm = true;
        $scope.hospitalPrivilegeInformation = {};
        $("#newHospitalPrivilegeInformationFormDiv").html(dynamicFormGenerateService.getForm($scope, $("#hospitalPrivilegeInformationForm").html()));
    };

    $scope.saveHospitalPrivilegeInformation = function (hospitalPrivilegeInformation) {
        //================== Save Here ============
        //$scope.HospitalPrivilegeInformations.push(hospitalPrivilegeInformation);
        //================== hide Show Condition ============
        $scope.hospitalPrivilegeInformationFormStatus = false;
        $scope.newHospitalPrivilegeInformationForm = false;
        $scope.hospitalPrivilegeInformation = {};
    };

    $scope.updateHospitalPrivilegeInformation = function (hospitalPrivilegeInformation) {
        $scope.showingDetails = false;
        $scope.hospitalPrivilegeInformationFormStatus = false;
        $scope.newHospitalPrivilegeInformationForm = false;
        $scope.hospitalPrivilegeInformation = {};
    };

    $scope.editHospitalPrivilegeInformation = function (index, hospitalPrivilegeInformation) {
        $scope.showingDetails = true;
        $scope.hospitalPrivilegeInformationFormStatus = true;
        $scope.newHospitalPrivilegeInformationForm = false;
        $scope.hospitalPrivilegeInformation = hospitalPrivilegeInformation;
        $("#hospitalPrivilegeInformationEditDiv" + index).html(dynamicFormGenerateService.getForm($scope, $("#hospitalPrivilegeInformationForm").html()));
    };

    $scope.cancelHospitalPrivilegeInformation = function (condition) {
        if (condition == "editCancel") {
            $scope.showingDetails = false;
            $scope.hospitalPrivilegeInformationFormStatus = false;
            $scope.newHospitalPrivilegeInformationForm = false;
            $scope.hospitalPrivilegeInformation = {};
        } else {
            $scope.showingDetails = false;
            $scope.hospitalPrivilegeInformationFormStatus = false;
            $scope.newHospitalPrivilegeInformationForm = false;
            $scope.hospitalPrivilegeInformation = {};
        }
    };

    $scope.removeHospitalPrivilegeInformation = function (index) {
        for (var i in $scope.HospitalPrivilegeInformations) {
            if (index == i) {
                $scope.HospitalPrivilegeInformations.splice(index, 1);
                break;
            }
        }
    };
});