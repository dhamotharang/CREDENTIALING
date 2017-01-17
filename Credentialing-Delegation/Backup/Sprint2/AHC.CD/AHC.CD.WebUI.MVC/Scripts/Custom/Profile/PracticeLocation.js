

profileApp.controller('locationcltr', function ($scope, $http, dynamicFormGenerateService) {

    $scope.PrimaryPracticeLocations = [{
        PracticeName: "Access",
        CorporateName: "MIRA",
        StartDate: "02-02-2003"
    }];

    $scope.MidLevelPractitioners = [{
        Name: "John Paul",
        PractitionerLicense: "ME2579",
    }, {
        Name: "Mc Smith",
        PractitionerLicense: "ME12729",
    }];

    $scope.Colleagues = [{
        EmployerName: "Henry Clarke",
        SpecialtyCode: "MD",
        ProviderType: "Employee",
    }, {
        EmployerName: "Jonathan James",
        SpecialtyCode: "DPD",
        ProviderType: "Affiliate",
    }];


    //=============== Practice Location Information Conditions ==================
    $scope.primaryPracticeLocationFormStatus = false;
    $scope.newPrimaryPracticeLocationForm = false;

    $scope.midLevelPractitionerFormStatus = false;
    $scope.newMidLevelPractitionerForm = false;

    $scope.colleagueFormStatus = false;
    $scope.newColleagueForm = false;

    $scope.showingDetails = false;

    //====================== Practice Location Information ===================

    $scope.addPrimaryPracticeLocation = function () {
        $scope.primaryPracticeLocationFormStatus = false;
        $scope.newPrimaryPracticeLocationForm = true;
        $scope.primaryPracticeLocation = {};
        $("#newPrimaryPracticeLocationFormDiv").html(dynamicFormGenerateService.getForm($scope, $("#primaryPracticeLocationForm").html()));
    };

    $scope.addMidLevelPractitioner = function () {
        $scope.midLevelPractitionerFormStatus = false;
        $scope.newMidLevelPractitionerForm = true;
        $scope.midLevelPractitioner = {};
        $("#newMidLevelPractitionerFormDiv").html(dynamicFormGenerateService.getForm($scope, $("#midLevelPractitionerForm").html()));
    };

    $scope.addColleague = function () {
        $scope.colleagueFormStatus = false;
        $scope.newColleagueForm = true;
        $scope.colleague = {};
        $("#newColleagueFormDiv").html(dynamicFormGenerateService.getForm($scope, $("#colleagueForm").html()));
    };

    $scope.savePrimaryPracticeLocation = function (primaryPracticeLocation) {
        //================== Save Here ============
        //$scope.PrimaryPracticeLocations.push(primaryPracticeLocation);
        //================== hide Show Condition ============
        $scope.primaryPracticeLocationFormStatus = false;
        $scope.newPrimaryPracticeLocationForm = false;
        $scope.primaryPracticeLocation = {};
    };

    $scope.saveMidLevelPractitioner = function (midLevelPractitioner) {
        //================== Save Here ============
        //$scope.MidLevelPractitioners.push(midLevelPractitioner);
        //================== hide Show Condition ============
        $scope.midLevelPractitionerFormStatus = false;
        $scope.newMidLevelPractitionerForm = false;
        $scope.midLevelPractitioner = {};
    };

    $scope.saveColleague = function (colleague) {
        //================== Save Here ============
        //$scope.Colleagues.push(colleague);
        //================== hide Show Condition ============
        $scope.colleagueFormStatus = false;
        $scope.newColleagueForm = false;
        $scope.colleague = {};
    };

    $scope.updatePrimaryPracticeLocation = function (primaryPracticeLocation) {
        $scope.showingDetails = false;
        $scope.primaryPracticeLocationFormStatus = false;
        $scope.newPrimaryPracticeLocationForm = false;
        $scope.primaryPracticeLocation = {};
    };

    $scope.updateMidLevelPractitioner = function (midLevelPractitioner) {
        $scope.showingDetails = false;
        $scope.midLevelPractitionerFormStatus = false;
        $scope.newMidLevelPractitionerForm = false;
        $scope.midLevelPractitioner = {};
    };

    $scope.updateColleague = function (colleague) {
        $scope.showingDetails = false;
        $scope.colleagueFormStatus = false;
        $scope.newColleagueForm = false;
        $scope.colleague = {};
    };

    $scope.editPrimaryPracticeLocation = function (index, primaryPracticeLocation) {
        $scope.showingDetails = true;
        $scope.primaryPracticeLocationFormStatus = true;
        $scope.newPrimaryPracticeLocationForm = false;
        $scope.primaryPracticeLocation = primaryPracticeLocation;
        $("#primaryPracticeLocationEditDiv" + index).html(dynamicFormGenerateService.getForm($scope, $("#primaryPracticeLocationForm").html()));
    };

    $scope.editMidLevelPractitioner = function (index, midLevelPractitioner) {
        $scope.showingDetails = true;
        $scope.midLevelPractitionerFormStatus = true;
        $scope.newMidLevelPractitionerForm = false;
        $scope.midLevelPractitioner = midLevelPractitioner;
        $("#midLevelPractitionerEditDiv" + index).html(dynamicFormGenerateService.getForm($scope, $("#midLevelPractitionerForm").html()));
    };

    $scope.editColleague = function (index, colleague) {
        $scope.showingDetails = true;
        $scope.colleagueFormStatus = true;
        $scope.newColleagueForm = false;
        $scope.colleague = colleague;
        $("#colleagueEditDiv" + index).html(dynamicFormGenerateService.getForm($scope, $("#colleagueForm").html()));
    };

    $scope.cancelPrimaryPracticeLocation = function (condition) {
        if (condition == "editCancel") {
            $scope.showingDetails = false;
            $scope.primaryPracticeLocationFormStatus = false;
            $scope.newPrimaryPracticeLocationForm = false;
            $scope.primaryPracticeLocation = {};
        } else {
            $scope.showingDetails = false;
            $scope.primaryPracticeLocationFormStatus = false;
            $scope.newPrimaryPracticeLocationForm = false;
            $scope.primaryPracticeLocation = {};
        }
    };

    $scope.cancelMidLevelPractitioner = function (condition) {
        if (condition == "editCancel") {
            $scope.showingDetails = false;
            $scope.midLevelPractitionerFormStatus = false;
            $scope.newMidLevelPractitionerForm = false;
            $scope.midLevelPractitioner = {};
        } else {
            $scope.showingDetails = false;
            $scope.midLevelPractitionerFormStatus = false;
            $scope.newMidLevelPractitionerForm = false;
            $scope.midLevelPractitioner = {};
        }
    };

    $scope.cancelColleague = function (condition) {
        if (condition == "editCancel") {
            $scope.showingDetails = false;
            $scope.colleagueFormStatus = false;
            $scope.newColleagueForm = false;
            $scope.colleague = {};
        } else {
            $scope.showingDetails = false;
            $scope.colleagueFormStatus = false;
            $scope.newColleagueForm = false;
            $scope.colleague = {};
        }
    };

    $scope.removePrimaryPracticeLocation = function (index) {
        for (var i in $scope.PrimaryPracticeLocations) {
            if (index == i) {
                $scope.PrimaryPracticeLocations.splice(index, 1);
                break;
            }
        }
    };

    $scope.removeMidLevelPractitioner = function (index) {
        for (var i in $scope.MidLevelPractitioners) {
            if (index == i) {
                $scope.MidLevelPractitioners.splice(index, 1);
                break;
            }
        }
    };

    $scope.removeColleague = function (index) {
        for (var i in $scope.Colleagues) {
            if (index == i) {
                $scope.Colleagues.splice(index, 1);
                break;
            }
        }
    };
});