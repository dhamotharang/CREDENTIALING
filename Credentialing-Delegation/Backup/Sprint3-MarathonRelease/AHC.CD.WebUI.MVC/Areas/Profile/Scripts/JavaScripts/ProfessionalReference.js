
//=========================== Controller declaration ==========================
profileApp.controller('ProfessionalReference', ['$scope', '$rootScope', '$http', 'masterDataService', 'locationService', 'messageAlertEngine', function ($scope, $rootScope, $http, masterDataService, locationService, messageAlertEngine) {

    $scope.ProfessionalReferences = [];

    //calling the method using $on(PSP-public subscriber pattern)
    $rootScope.$on('ProfessionalReferenceInfos', function (event, val) {
        $scope.ProfessionalReferences = val;
      
        //console.log(val);
      
    });

    //------------------------------------------------------------------Address Auto-Complete---------------------------------------------------------------------------//

    /*
     Method addressAutocomplete() gets the details of a location
         Method takes input of location details entered in the text box.
  */
    $scope.addressAutocomplete = function (location) {
        
        $scope.tempObject.City = location;
        if (location.length < 3 && !angular.isObject(location)) {         //As soon as the length of the string reaches 3 and the location is not an object
            $scope.Locations = [];      //A call is made to the locations service which returns a list of relevant locations
            //console.log($scope.Locations);
            $scope.resetAddressModels();
        }
        if (location.length > 2 && !angular.isObject(location)) {         //As soon as the length of the string reaches 3 and the location is not an object
            $scope.Locations = locationService.getLocations(location);      //A call is made to the locations service which returns a list of relevant locations
            //console.log($scope.Locations);

        }
        else if (angular.isObject(location)) {                      //When user select a city the location variable then holds the object of the respective location.
            if (!$scope.tempObject) {
                $scope.tempObject = {};
            }
            $scope.setAddressModels(location);
        }
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
            //$scope.tempObject.Degree = "Not Available"
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
        //console.log("Degree");
        //console.log( $scope.masterDegrees);
    });

    $scope.saveProfessionalReference = function (professionalReference,index) {
 
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
            //if (professionalReference.Degree == null) {
            //    professionalReference.Degree = "Null";
            //}
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
                  
                    //if (data.professionalReference.Degree == null)

                    //    data.professionalReference.Degree = '';

                    if (data.status == "true") {
                        data.professionalReference.Specialty = tempSpecialty;
                        data.professionalReference.ProviderType = providerType;
                        if ($scope.visibilityControl != (index + '_editpr')) {
                            $scope.ProfessionalReferences.push(data.professionalReference);
                            $rootScope.operateCancelControl('');
                            messageAlertEngine.callAlertMessage("addedNewProfessionalReference", "Professional Reference saved successfully !!!!", "success", true);
                        }
                        else {
                          

                            $scope.ProfessionalReferences[index] = data.professionalReference;
                            //console.log($scope.ProfessionalReferences);
                            $rootScope.operateViewAndAddControl(index + '_viewpr');
                            messageAlertEngine.callAlertMessage("updatedProfessionalReference" + index, "Professional Reference updated successfully !!!!", "success", true);
                        }

                        $scope.IsProfessionalReferenceHasError = false;
                        FormReset(formData1);
                      
                    } else {
                        messageAlertEngine.callAlertMessage('errorProfessionalReference' + index, "", "danger", true);
                        $scope.errorProfessionalReference = data.status.split(",");
                        //$scope.IsProfessionalReferenceHasError = true;
                        //$scope.ProfessionalReferenceErrorList = data.status.split(",");
                    }
                },
                error: function (e) {
                    messageAlertEngine.callAlertMessage('errorProfessionalReference' + index, "", "danger", true);
                    $scope.errorProfessionalReference = "Sorry for Inconvenience !!!! Please Try Again Later...";
                }
            });
        }

    };

  

    function ResetProfessionalReferenceForm() {
        $('#newShowProfessionalReferenceDiv').find('.professionalReferenceForm')[0].reset();
        $('#newShowProfessionalReferenceDiv').find('span').html('');
    }
}]);

