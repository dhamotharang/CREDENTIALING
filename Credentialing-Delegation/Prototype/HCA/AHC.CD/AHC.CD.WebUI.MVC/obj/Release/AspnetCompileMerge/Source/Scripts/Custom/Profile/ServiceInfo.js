
profileApp.controller('ServiceInfoController', function ($scope, $http, dynamicFormGenerateService) {
    $scope.IsMilitary = "No";
    $scope.IsPublicHealthService="No";
    $scope.MilitaryServiceInformations = null;

    $scope.PublicHealthServices = null;


  
 //============Public Health Service===============================


    $scope.editShowPublicHealthService = false;
    $scope.newShowPublicHealthService = false;
    $scope.submitButtonText = "Add";
    $scope.IndexValue = 0;

    $scope.addPublicHealthService = function () {
        $scope.newShowPublicHealthService = true;
        $scope.submitButtonText = "Add";
        $scope.publicHealthService = {};
        ResetPublicHealthServiceForm();
    };

    $scope.editPublicHealthService = function (index, publicHealthService) {
        $scope.editShowPublicHealthService = true;
        $scope.submitButtonText = "Update";
        $scope.publicHealthService = publicHealthService;
        $scope.IndexValue = index;
    };

    $scope.cancelPublicHealthService = function (condition) {
        setPublicHealthServiceCancelParameters();
    };

    $scope.savePublicHealthService = function (publicHealthService) {

        console.log(publicHealthService);

        var validationStatus;
        var url;

        if ($scope.newShowPublicHealthService) {
            //Add Details - Denote the URL
            validationStatus = $('#newShowPublicHealthServiceDiv').find('form').valid();
            url = "/Profile/ServiceInformation/AddPublicHealthService?profileId=1";
        }
        else if ($scope.editShowPublicHealthService) {
            //Update Details - Denote the URL
            validationStatus = $('#publicHealthServiceEditDiv' + $scope.IndexValue).find('form').valid();
            url = "/Profile/ServiceInformation/UpdatePublicHealthService?profileId=1";
        }

        console.log(publicHealthService);

        if (validationStatus) {
            // Simple POST request example (passing data) :
            $http.post(url, publicHealthService).
              success(function (data, status, headers, config) {

                  alert("Success");
                  if ($scope.newShowPublicHealthService) {
                      //Add Details - Denote the URL
                      publicHealthService.PublicHealthServiceID = data;
                      $scope.PublicHealthService.push(publicHealthService);
                  }
                  setPublicHealthServiceCancelParameters();
              }).
              error(function (data, status, headers, config) {
                  alert("Error");
              });
        }
    };

    function setPublicHealthServiceCancelParameters() {
        $scope.editShowPublicHealthService = false;
        $scope.newShowPublicHealthService = false;
        $scope.publicHealthService = {};
        $scope.IndexValue = 0;
    }

    function ResetPublicHealthServiceForm() {
        $('#newShowPublicHealthServiceDiv').find('.publicHealthServiceForm')[0].reset();
        $('#newShowPublicHealthServiceDiv').find('span').html('');
    }

 

    //======================Military Service Information ===================

    $scope.editShowMilitaryServiceInformation = false;
    $scope.newShowMilitaryServiceInformation = false;
    $scope.submitButtonText = "Add";
    $scope.IndexValue = 0;

    $scope.addMilitaryServiceInformation = function () {
        $scope.newShowMilitaryServiceInformation = true;
        $scope.submitButtonText = "Add";
        $scope.militaryServiceInformation = {};
        ResetMilitaryServiceInformationForm();
    };

    $scope.editMilitaryServiceInformation = function (index, militaryServiceInformation) {
        $scope.editShowMilitaryServiceInformation = true;
        $scope.submitButtonText = "Update";
        $scope.militaryServiceInformation = militaryServiceInformation;
        $scope.IndexValue = index;
    };

    $scope.cancelMilitaryServiceInformation = function (condition) {
        setMilitaryServiceInformationCancelParameters();
    };

    $scope.saveMilitaryServiceInformation = function (militaryServiceInformation) {

        console.log(militaryServiceInformation);

        var validationStatus;
        var url;

        if ($scope.newShowMilitaryServiceInformation) {
            //Add Details - Denote the URL
            validationStatus = $('#newShowMilitaryServiceInformationDiv').find('form').valid();
            url = "/Profile/ServiceInformation/AddMilitaryServiceInformation?profileId=1";
        }
        else if ($scope.editShowMilitaryServiceInformation) {
            //Update Details - Denote the URL
            validationStatus = $('#militaryServiceInformationEditDiv' + $scope.IndexValue).find('form').valid();
            url = "/Profile/ServiceInformation/UpdateMilitaryServiceInformation?profileId=1";
        }

        console.log(militaryServiceInformation);

        if (validationStatus) {
            // Simple POST request example (passing data) :
            $http.post(url, militaryServiceInformation).
              success(function (data, status, headers, config) {

                  alert("Success");
                  if ($scope.newShowPublicHealthService) {
                      //Add Details - Denote the URL
                      militaryServiceInformation.MilitaryServiceInformationID = data;
                      $scope.MilitaryServiceInformation.push(militaryServiceInformation);
                  }
                  setMilitaryServiceInformationCancelParameters();
              }).
              error(function (data, status, headers, config) {
                  alert("Error");
              });
        }
    };

    function setMilitaryServiceInformationCancelParameters() {
        $scope.editShowMilitaryServiceInformation = false;
        $scope.newShowMilitaryServiceInformation = false;
        $scope.militaryServiceInformation = {};
        $scope.IndexValue = 0;
    }

    function ResetMilitaryServiceInformationForm() {
        $('#newShowMilitaryServiceInformationDiv').find('.militaryServiceInformationForm')[0].reset();
        $('#newShowMilitaryServiceInformationDiv').find('span').html('');
    }  
});

$(document).ready(function () {
    //$.validator.setDefaults({ ignore: [] });
    //$('.militaryServiceInformationForm').submit(AddEditMilitaryServiceInformationSubmit);
    //$('.addMilitaryServiceInformationBtn').click(ResetMilitaryServiceInformationForm);

});