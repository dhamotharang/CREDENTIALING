profileApp.controller('OrgInfoAppCtrl', function ($scope, $http, dynamicFormGenerateService) {
    
    $scope.OrganisationInfos = [{
        ProviderRelationship: "Employee",
        IsPartOfGroup:"true",
       GroupName: "Access",
       Certificate: "/Content/Document/SINGH - CV.pdf"
    }];

    $scope.groups = ['Access', 'Access2', 'Meera'];

    $scope.providerRelations = [{
        relationId: 0,
        relationName: "Employee"
    }, {
        relationId: 1,
        relationName: "Affiliate"
    }];
    
    //=============== Organisation info Conditions ==================
    $scope.editOrganisationInfo = false;
    $scope.newOrganisationInfo = false;
    $scope.submitButtonText = "Add";
    $scope.IndexValue = 0;

 

    $scope.editOrganisationInfo = function (index, OrganisationInfo) {
        $scope.newShowOrganisationInfo = false;
        $scope.editShowOrganisationInfo = true;
        $scope.submitButtonText = "Update";
        $scope.OrganisationInfo = OrganisationInfo;
        $scope.IndexValue = index;
    };

    $scope.isPartOfGroup = function () {
        $scope.Provider.GroupID = $scope.puempty;
        resetGroupNameSelection("");
    };

    function resetGroupNameSelectTag(value) {
        $("#selectGroup").select2("val", value);
    }


    $scope.cancelOrganisationInfo = function (condition) {
        setOrganisationInfoCancelParameters();
    };

    $scope.saveStateLicense = function (stateLicense) {

        console.log(stateLicense);

        var validationStatus;
        var url;

        if ($scope.newShowStateLicense) {
            //Add Details - Denote the URL
            validationStatus = $('#newShowStateLicenseDiv').find('form').valid();
            url = "/Profile/IdentificationAndLicense/AddStateLicense";
        }
        else if ($scope.editShowStateLicense) {
            //Update Details - Denote the URL
            validationStatus = $('#stateLicenseEditDiv' + $scope.IndexValue).find('form').valid();
            url = "/Profile/IdentificationAndLicense/UpdateStateLicense";
        }

        console.log(stateLicense);

        if (validationStatus) {
            // Simple POST request example (passing data) :
            $http.post(url, stateLicense).
              success(function (data, status, headers, config) {
                  alert("Success");
                  setStateLicenseCancelParameters();
              }).
              error(function (data, status, headers, config) {
                  alert("Error");
              });
        }
    };

    function setOrganisationInfoCancelParameters() {
        $scope.editShowOrganisationInfo = false;
        $scope.newShowOrganisationInfo = false;
        $scope.OrganisationInfo = {};
        $scope.IndexValue = 0;
    }

    function ResetOrganisationInfoForm() {
        $('#newShowOrganisationInfoDiv').find('.OrganisationInfoForm')[0].reset();
        $('#newShowOrganisationInfoDiv').find('span').html('');
    }
});
$(document).ready(function () {
 
    $("#selectGroup").select2({
        placeholder: "Select a Group"
    });
});