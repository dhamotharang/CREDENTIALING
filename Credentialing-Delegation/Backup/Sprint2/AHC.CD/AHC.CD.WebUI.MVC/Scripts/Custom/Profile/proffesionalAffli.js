
profileApp.controller('professionalAppCtrl', function ($scope, $http, dynamicFormGenerateService) {

    $scope.ProfessionalAffiliations = [{
        OrganizationName: "American Association for Laboratory Accreditation (A2LA)",
        StartDate: "01-03-2007",
        EndDate: "01-03-2009"
    }, {
        OrganizationName: "St. Jude Medical",
        StartDate: "06-01-2005",
        EndDate: "05-30-2006"
    }];

    //=============== Professional Affiliation Conditions ==================
    $scope.professionalAffiliationFormStatus = false;
    $scope.newProfessionalAffiliationForm = false;
    $scope.showingDetails = false;

    //====================== Professional Affiliation ===================

    $scope.addProfessionalAffiliation = function () {
        $scope.professionalAffiliationFormStatus = false;
        $scope.newProfessionalAffiliationForm = true;
        $scope.professionalAffiliation = {};
        $("#newProfessionalAffiliationFormDiv").html(dynamicFormGenerateService.getForm($scope, $("#professionalAffiliationForm").html()));
    };

    $scope.saveProfessionalAffiliation = function (professionalAffiliation) {
        //================== Save Here ============
        //$scope.ProfessionalAffiliations.push(professionalAffiliation);
        //================== hide Show Condition ============
        $scope.professionalAffiliationFormStatus = false;
        $scope.newProfessionalAffiliationForm = false;
        $scope.professionalAffiliation = {};
    };

    $scope.updateProfessionalAffiliation = function (professionalAffiliation) {
        $scope.showingDetails = false;
        $scope.professionalAffiliationFormStatus = false;
        $scope.newProfessionalAffiliationForm = false;
        $scope.professionalAffiliation = {};
    };

    $scope.editProfessionalAffiliation = function (index, professionalAffiliation) {
        $scope.showingDetails = true;
        $scope.professionalAffiliationFormStatus = true;
        $scope.newProfessionalAffiliationForm = false;
        $scope.professionalAffiliation = professionalAffiliation;
        $("#professionalAffiliationEditDiv" + index).html(dynamicFormGenerateService.getForm($scope, $("#professionalAffiliationForm").html()));
    };

    $scope.cancelProfessionalAffiliation = function (condition) {
        if (condition == "editCancel") {
            $scope.showingDetails = false;
            $scope.professionalAffiliationFormStatus = false;
            $scope.newProfessionalAffiliationForm = false;
            $scope.professionalAffiliation = {};
        } else {
            $scope.showingDetails = false;
            $scope.professionalAffiliationFormStatus = false;
            $scope.newProfessionalAffiliationForm = false;
            $scope.professionalAffiliation = {};
        }
    };

    $scope.removeProfessionalAffiliation = function (index) {
        for (var i in $scope.ProfessionalAffiliations) {
            if (index == i) {
                $scope.ProfessionalAffiliations.splice(index, 1);
                break;
            }
        }
    };
});