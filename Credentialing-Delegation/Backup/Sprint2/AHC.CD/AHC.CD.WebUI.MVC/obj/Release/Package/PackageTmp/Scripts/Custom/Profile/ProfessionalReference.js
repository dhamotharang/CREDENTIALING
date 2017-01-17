
//=========================== Controller declaration ==========================
profileApp.controller('ProfessionalReference', function ($scope, $http, dynamicFormGenerateService) {

    $scope.ProfessionalReferences = [{
        FirstName: "John",
        MiddleName: "K",
        LastName: "Paul",
        Title: "MD",
        Number: "5350",
        Street: "Spring Hill Drive",
        SuiteBuilding: "Suite",
        Telephone: "5554323241",
        State: "Florida",
        City: "Putname",
        Zipcode: "2846",
        Fax: "5647382910"
    }];

    
    //=============== Professional Reference Conditions ==================
    $scope.professionalReferenceFormStatus = false;
    $scope.newProfessionalReferenceForm = false;
    $scope.showingDetails = false;

    //====================== Professional Reference ===================

    $scope.addProfessionalReference = function () {
        $scope.professionalReferenceFormStatus = false;
        $scope.newProfessionalReferenceForm = true;
        $scope.professionalReference = {};
        $("#newProfessionalReferenceFormDiv").html(dynamicFormGenerateService.getForm($scope, $("#professionalReferenceForm").html()));
    };

    $scope.saveProfessionalReference = function (professionalReference) {
        //================== Save Here ============
        //$scope.ProfessionalReferences.push(professionalReference);
        //================== hide Show Condition ============
        $scope.professionalReferenceFormStatus = false;
        $scope.newProfessionalReferenceForm = false;
        $scope.professionalReference = {};
    };

    $scope.updateProfessionalReference = function (professionalReference) {
        $scope.showingDetails = false;
        $scope.professionalReferenceFormStatus = false;
        $scope.newProfessionalReferenceForm = false;
        $scope.professionalReference = {};
    };

    $scope.editProfessionalReference = function (index, professionalReference) {
        $scope.showingDetails = true;
        $scope.professionalReferenceFormStatus = true;
        $scope.newProfessionalReferenceForm = false;
        $scope.professionalReference = professionalReference;
        $("#professionalReferenceEditDiv" + index).html(dynamicFormGenerateService.getForm($scope, $("#professionalReferenceForm").html()));
    };

    $scope.cancelProfessionalReference = function (condition) {
        if (condition == "editCancel") {
            $scope.showingDetails = false;
            $scope.professionalReferenceFormStatus = false;
            $scope.newProfessionalReferenceForm = false;
            $scope.professionalReference = {};
        } else {
            $scope.showingDetails = false;
            $scope.professionalReferenceFormStatus = false;
            $scope.newProfessionalReferenceForm = false;
            $scope.professionalReference = {};
        }

    };

    $scope.removeProfessionalReference = function (index) {
        for (var i in $scope.ProfessionalReferences) {
            if (index == i) {
                $scope.ProfessionalReferences.splice(index, 1);
                break;
            }
        }
    };

});