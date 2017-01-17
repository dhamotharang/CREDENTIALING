
profileApp.controller('professionalAppCtrl', function ($scope, $http, $rootScope, dynamicFormGenerateService) {

    $scope.ProfessionalAffiliations = [
    //    {
    //    OrganizationName: "American Association for Laboratory Accreditation",
    //    StartDate: "01-03-2007",
    //    EndDate: "01-03-2009",
    //    PositionOfficeHeld: "Director",
    //    Member: "Shalabh"
    //}, {
    //    OrganizationName: "St. Jude Medical",
    //    StartDate: "06-01-2005",
    //    EndDate: "05-30-2006",
    //    PositionOfficeHeld: "Assistant Director",
    //    Member: "Shalabh"
    //}
    ];

    $rootScope.$on('ProfessionalAffiliationInfos', function (event, val) {
        $scope.ProfessionalAffiliations = val;
    });

    //=============== Professional Affiliation Conditions ==================
    $scope.editShowProfessionalAffiliation = false;
    $scope.newShowProfessionalAffiliation = false;
    $scope.submitButtonText = "Add";
    $scope.IndexValue = 0;

    //====================== Professional Affiliation ===================

    $scope.addProfessionalAffiliation = function () {
        $scope.newShowProfessionalAffiliation = true;
        $scope.submitButtonText = "Add";
        $scope.professionalAffiliation = {};
        ResetProfessionalAffiliationForm();
    };

    $scope.editProfessionalAffiliation = function (index, professionalAffiliation) {
        $scope.newShowProfessionalAffiliation = false;
        $scope.editShowProfessionalAffiliation = true;
        $scope.submitButtonText = "Update";
        $scope.professionalAffiliation = professionalAffiliation;
        $scope.IndexValue = index;
    };

    $scope.cancelProfessionalAffiliation = function (condition) {
        setProfessionalAffiliationCancelParameters();
    };

    $scope.saveProfessionalAffiliation = function (professionalAffiliation) {

        console.log(professionalAffiliation);

        var validationStatus;
        var url;

        if ($scope.newShowProfessionalAffiliation) {
            //Add Details - Denote the URL
            validationStatus = $('#newShowProfessionalAffiliationDiv').find('form').valid();
            url = "/Profile/ProfessionalAffiliation/AddProfessionalAffiliation?profileId=" + profileId;
        }
        else if ($scope.editShowProfessionalAffiliation) {
            //Update Details - Denote the URL
            validationStatus = $('#professionalAffiliationEditDiv' + $scope.IndexValue).find('form').valid();
            url = "/Profile/ProfessionalAffiliation/UpdateProfessionalAffiliation?profileId=" + profileId;
        }

        console.log(professionalAffiliation);

        if (validationStatus) {
            // Simple POST request example (passing data) :
            $http.post(url, professionalAffiliation).
              success(function (data, status, headers, config) {

                  alert("Success");
                  if ($scope.newShowProfessionalAffiliation) {
                      //Add Details - Denote the URL
                      professionalAffiliation.ProfessionalAffiliationInfoID = data;
                      $scope.ProfessionalAffiliations.push(professionalAffiliation);
                  }
                  setProfessionalAffiliationCancelParameters();
              }).
              error(function (data, status, headers, config) {
                  alert("Error");
              });
        }
    };

    function setProfessionalAffiliationCancelParameters() {
        $scope.editShowProfessionalAffiliation = false;
        $scope.newShowProfessionalAffiliation = false;
        $scope.professionalAffiliation = {};
        $scope.IndexValue = 0;
    }

    function ResetProfessionalAffiliationForm() {
        $('#newShowProfessionalAffiliationDiv').find('.professionalAffiliationForm')[0].reset();
        $('#newShowProfessionalAffiliationDiv').find('span').html('');
    }
});

$(document).ready(function () {
});