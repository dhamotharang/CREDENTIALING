
profileApp.controller('CredentialingContactCtrl', function ($scope, $http, dynamicFormGenerateService) {


    $scope.PrimaryCredentialingContacts = [{
        OfficeManagerAvail: true,
        OfficeManager: "John Paul",
        FirstName: "Annmaria",
        MiddleName: "",
        LastName: "Emerick",
        Number: "12225",
        Street: "28 Street North",
        Building: "Suite A",
        City: "St Petersburg",
        State: "Florida",
        ZipCode: "337161860",
        Telephone: "7278232188",
        Fax: "7278280723",
        EmailId: "americk9a6@medinet.net"

    }];



    $scope.showDenied = function () {

        $scope.showInput = true;
    }

    $scope.hideDenied = function () {

        $scope.showInput = false;
    }
    //===============  Conditions ==================
    $scope.editShowCredentialingContact = false;
    $scope.newShowCredentialingContact = false;
    $scope.submitButtonText = "Add";
    $scope.IndexValue = 0;

    //===================================================================================

    $scope.addCredentialingContact = function () {
        $scope.newShowCredentialingContact = true;
        $scope.submitButtonText = "Add";
        $scope.credentialingContact = {};
        //ResetHosPrvForm();
    };

    $scope.editCredentialingContact = function (index, credentialingContact) {
        $scope.viewShowCredentialingContact = false;
        $scope.editShowCredentialingContact = true;
        $scope.submitButtonText = "Update";
        $scope.credentialingContact = credentialingContact;
        $scope.IndexValue = index;
    };

    $scope.viewCredentialingContact = function (index, credentialingContact) {
        $scope.editShowCredentialingContact = false;
        $scope.viewShowCredentialingContact = true;
        $scope.credentialingContact = credentialingContact;
        $scope.IndexValue = index;
    };

    $scope.cancelCredentialingContact = function (condition) {
        setCredentialingContactCancelParameters();
    };

    $scope.saveCredentialingContact = function (credentialingContact) {


        url = "/Profile/PracticeLocation/UpdateCredentialingContactAsync?profileId=1";

        $http.post(rootDir + url, credentialingContact).
         success(function (data, status, headers, config) {

            //// alert("Success");

             credentialingContact = {};

         }).
         error(function (data, status, headers, config) {
             //alert(data);
         });
      
    };

    function setCredentialingContactCancelParameters() {
        $scope.viewShowCredentialingContact = false;
        $scope.editShowCredentialingContact = false;
        $scope.newShowCredentialingContact = false;
        $scope.credentialingContact = {};
        $scope.IndexValue = 0;
    }

    function ResetHosPrvForm() {
        $('#newShowCredentialingContactDiv').find('.credentialingContactForm')[0].reset();
        $('#newShowCredentialingContactDiv').find('span').html('');
    }

});

$(document).ready(function () {
});