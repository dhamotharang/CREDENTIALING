
profileApp.controller('CredentialingContactCtrl', function ($scope, $http, dynamicFormGenerateService) {


    $scope.PrimaryCredentialingContacts = [{
        OfficeManagerAvail: true,
        OfficeManager: "Annmarie Emerick",
        FirstName: "Annmarie",
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
        EmailId: "americk9a6@medenet.net"

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

        console.log(credentialingContact);

        var validationStatus;
        var url;

        if ($scope.newShowCredentialingContact) {
            //Add Details - Denote the URL
            validationStatus = $('#newShowCredentialingContactDiv').find('form').valid();
            url = "/Profile/CredentialingContact/AddCredentialingContactAsync?profileId=1";
        }
        else if ($scope.editShowCredentialingContact) {
            //Update Details - Denote the URL
            validationStatus = $('#credentialingContactEditDiv' + $scope.IndexValue).find('form').valid();
            url = "/Profile/CredentialingContact/UpdateCredentialingContact?profileId=1";
        }

        console.log(credentialingContact);

        if (validationStatus) {
            //hospitalPrivilegeInformation.HospitalPhone = "fghjk";
            //hospitalPrivilegeInformation.HospitalFax = "fghjk";
            //hospitalPrivilegeInformation.ContactPersonPhone = "fghjk";
            //hospitalPrivilegeInformation.ContactPersonFax = "fghjk";

            // Simple POST request example (passing data) :
            $http.post(url, credentialingContact).
              success(function (data, status, headers, config) {

                  alert("Success");
                  if ($scope.newShowCredentialingContact) {
                      //Add Details - Denote the URL
                      credentialingContact.CredentialingContactID = data;
                      $scope.CredentialingContact.push(credentialingContact);
                  }
                  setCredentialingContactCancelParameters();
              }).
              error(function (data, status, headers, config) {
                  alert("Error");
              });
        }
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


    //===================================================================================



    //======================  ===================

    //$scope.addCredentialingContact = function () {
    //    $scope.newShowCredentialingContact = true;
    //    $scope.submitButtonText = "Add";
    //    $scope.CredentialingContact = {};
    //    ResetCredentialingContactForm();
    //};

    //$scope.editCredentialingContact = function (index, CredentialingContact) {
    //    $scope.newShowCredentialingContact = false;
    //    $scope.editShowCredentialingContact = true;
    //    $scope.submitButtonText = "Update";
    //    $scope.CredentialingContact = CredentialingContact;
    //    $scope.IndexValue = index;
    //};

    //$scope.viewCredentialingContact = function (index, CredentialingContact) {
    //    $scope.editShowCredentialingContact = false;
    //    $scope.viewShowCredentialingContact = true;
    //    $scope.CredentialingContact = CredentialingContact;
    //    $scope.IndexValue = index;
    //};

    //$scope.cancelCredentialingContact = function (condition) {
    //    setCredentialingContactCancelParameters();
    //};

    //$scope.saveCredentialingContact = function (CredentialingContact) {

    //    console.log(CredentialingContact);

    //    var validationStatus;
    //    var url;

    //    if ($scope.newShowCredentialingContact) {
    //        //Add Details - Denote the URL
    //        validationStatus = $('#newShowCredentialingContactDiv').find('form').valid();
    //        url = "/Profile/CredentialingContact/AddCredentialingContact?profileId=1";
    //    }
    //    else if ($scope.editShowCredentialingContact) {
    //        //Update Details - Denote the URL
    //        validationStatus = $('#CredentialingContactEditDiv' + $scope.IndexValue).find('form').valid();
    //        url = "/Profile/CredentialingContact/UpdateCredentialingContact?profileId=1";
    //    }

    //    console.log(CredentialingContact);

    //    if (validationStatus) {
    //        // Simple POST request example (passing data) :
    //        $http.post(url, CredentialingContact).
    //          success(function (data, status, headers, config) {

    //              alert("Success");
    //              if ($scope.newShowCredentialingContact) {
    //                  //Add Details - Denote the URL
    //                  CredentialingContact.CredentialingContactInfoID = data;
    //                  $scope.CredentialingContacts.push(CredentialingContact);
    //              }
    //              setCredentialingContactCancelParameters();
    //          }).
    //          error(function (data, status, headers, config) {
    //              alert("Error");
    //          });
    //    }
    //};

    //function setCredentialingContactCancelParameters() {
    //    $scope.viewShowCredentialingContact = false;
    //    $scope.editShowCredentialingContact = false;
    //    $scope.newShowCredentialingContact = false;
    //    $scope.CredentialingContact = {};
    //    $scope.IndexValue = 0;
    //}

    //function ResetCredentialingContactForm() {
    //    $('#newShowCredentialingContactDiv').find('.CredentialingContactForm')[0].reset();
    //    $('#newShowCredentialingContactDiv').find('span').html('');
    //}
});

$(document).ready(function () {
});