
//=========================== Controller declaration ==========================
profileApp.controller('ProfessionalReference', ['$scope', '$rootScope', '$http', 'masterDataService', 'locationService', 'messageAlertEngine', function ($scope, $rootScope, $http, masterDataService, locationService, messageAlertEngine) {

    $scope.ProfessionalReferences = [];

    //calling the method using $on(PSP-public subscriber pattern)
    $rootScope.$on('ProfessionalReferenceInfos', function (event, val) {
        $scope.ProfessionalReferences = val;
        for (var i = 0; i < $scope.ProfessionalReferences.length ; i++) {

            if (!$scope.ProfessionalReferences[i].SpecialtyID) { $scope.ProfessionalReferences[i].SpecialtyID = ""; }
            if (!$scope.ProfessionalReferences[i].Degree) { $scope.ProfessionalReferences[i].Degree = ""; }
        }
        //console.log(val);
      
    });

    //------------------------------------------------------------------Address Auto-Complete---------------------------------------------------------------------------//

    /*
     Method addressAutocomplete() gets the details of a location
         Method takes input of location details entered in the text box.
  */

    $scope.addressAutocomplete = function (location) {
        if (location.length == 0) {
            $scope.resetAddressModels();
        }
        $scope.tempObject.CityOfBirth = location;
        if (location.length > 1 && !angular.isObject(location)) {
            locationService.getLocations(location).then(function (val) {
                $scope.Locations = val;
            });
        } else if (angular.isObject(location)) {
            $scope.setAddressModels(location);
        }
    };

    $scope.selectedLocation = function (location) {
        $scope.setAddressModels(location);
        $(".ProviderTypeSelectAutoList").hide();
    };

    $scope.resetAddressModels = function () {
        $scope.tempObject.City = "";
        $scope.tempObject.State = "";
        $scope.tempObject.Country = "";
    };

    $scope.setAddressModels = function (location) {
        $scope.tempObject.City = location.City;
        $scope.tempObject.State = location.State;
        $scope.tempObject.Country = location.Country;

    }
    $scope.CountryDialCodes = countryDailCodes;
    //---------------------------------------------------------------------------------------------------------------------------------------------------------
    var providerType;

    $scope.removeDegree = function (val) {
        if (val = "? undefined:undefined ?" || val == "? object:null ?") {
            return 'Not Available';
        }
        else {
            $scope.tempObject.Degree =val;
            return val;
        }
    }

  $scope.SelectProviderTitle = function(titleType){
  
      $scope.tempObject.ProviderTypeID = titleType.ProviderTypeID;
  
  }

    $scope.shoLanguageList = function (divId) {
        $("#" + divId).show();
    };

    $scope.submitButtonText = "Add";

    $scope.changeButtonText = function () {
        if ($scope.submitButtonText == "Update")
            $scope.submitButtonText = "Add";
        else
            $scope.submitButtonText = "Update";
    }

    //------------------------------- Country Code Popover Show by Id ---------------------------------------
    $scope.showContryCodeDiv = function (countryCodeDivId) {
        changeVisibilityOfCountryCode();
        $("#" + countryCodeDivId).show();
    };
    ////******************************Master Data*****************************
    $scope.masterSpecialties = [];
    $scope.masterDegrees = [];
    $scope.masterProviderTypes = [];


    masterDataService.getMasterData("/Profile/MasterData/getAllSpecialities").then(function (masterSpecialties) {
        $scope.masterSpecialties = masterSpecialties;
    });

      masterDataService.getMasterData("/Profile/MasterData/GetAllProviderTypes").then(function (masterProviderTypes) {
        $scope.masterProviderTypes = masterProviderTypes;
    });

    masterDataService.getMasterData("/Profile/MasterData/GetAllQualificationDegrees").then(function (masterDegrees) {
        $scope.masterDegrees = masterDegrees;
    });

    $scope.saveProfessionalReference = function (professionalReference,index) {
        loadingOn();
        var validationStatus;
        var url;
        var formData1;
        var providerTypeId;

        var tempSpecialtyID;
        var tempSpecialty;

        tempSpecialtyID = professionalReference.SpecialtyID;
        for (var spl in $scope.masterSpecialties) {
            if ($scope.masterSpecialties[spl].SpecialtyID == tempSpecialtyID) {
                tempSpecialty = $scope.masterSpecialties[spl];
                break;
            }
        }

        providerTypeId = professionalReference.ProviderTypeID;

        for (var provider in $scope.masterProviderTypes) {
            if ($scope.masterProviderTypes[provider].ProviderTypeID == providerTypeId) {
                providerType = $scope.masterProviderTypes[provider];
                break;
            }
        }

        if ($scope.visibilityControl == 'addpr') {
    
            formData1 = $('#newProfessionalReferenceFormDiv').find('form');
            url = "/Profile/ProfessionalReference/AddProfessionalReference?profileId=" + profileId;
        }
        else if ($scope.visibilityControl == (index + '_editpr')) {
            //Update Details - Denote the URL
          
            formData1 = $('#professionalReferenceEditDiv' + index).find('form');
            console.log(formData1);
            url = "/Profile/ProfessionalReference/UpdateProfessionalReference?profileId=" + profileId;
        }

        ResetFormForValidation(formData1);
        validationStatus = formData1.valid();

        if (validationStatus) {
            // Simple POST request example (passing data) :
            $.ajax({
                url: url,
                type: 'POST',
                data: new FormData(formData1[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {

                    if (data.status == "true") {
                        data.professionalReference.Specialty = tempSpecialty;
                        data.professionalReference.ProviderType = providerType;
                        if ($scope.visibilityControl != (index + '_editpr')) {
                            $scope.ProfessionalReferences.push(data.professionalReference);
                            for (var i = 0; i < $scope.ProfessionalReferences.length ; i++) {

                                if (!$scope.ProfessionalReferences[i].SpecialtyID) { $scope.ProfessionalReferences[i].SpecialtyID = ""; }
                                if (!$scope.ProfessionalReferences[i].Degree) { $scope.ProfessionalReferences[i].Degree = ""; }
                            }
                            $rootScope.operateCancelControl('');
                            messageAlertEngine.callAlertMessage("addedNewProfessionalReference", "Professional Reference saved successfully !!!!", "success", true);
                        }
                        else {
                          

                            $scope.ProfessionalReferences[index] = data.professionalReference;
                            for (var i = 0; i < $scope.ProfessionalReferences.length ; i++) {

                                if (!$scope.ProfessionalReferences[i].SpecialtyID) { $scope.ProfessionalReferences[i].SpecialtyID = ""; }
                                if (!$scope.ProfessionalReferences[i].Degree) { $scope.ProfessionalReferences[i].Degree = ""; }
                            }
                            //console.log($scope.ProfessionalReferences);
                            $rootScope.operateViewAndAddControl(index + '_viewpr');
                            messageAlertEngine.callAlertMessage("updatedProfessionalReference" + index, "Professional Reference updated successfully !!!!", "success", true);
                        }

                        $scope.IsProfessionalReferenceHasError = false;
                        FormReset(formData1);
                      
                    } else {
                        messageAlertEngine.callAlertMessage('errorProfessionalReference' + index, "", "danger", true);
                        $scope.errorProfessionalReference = data.status.split(",");
                    }
                },
                error: function (e) {
                    messageAlertEngine.callAlertMessage('errorProfessionalReference' + index, "", "danger", true);
                    $scope.errorProfessionalReference = "Sorry for Inconvenience !!!! Please Try Again Later...";
                }
            });
        }
        loadingOff();
    };

  

    function ResetProfessionalReferenceForm() {
        $('#newShowProfessionalReferenceDiv').find('.professionalReferenceForm')[0].reset();
        $('#newShowProfessionalReferenceDiv').find('span').html('');
    }
}]);

