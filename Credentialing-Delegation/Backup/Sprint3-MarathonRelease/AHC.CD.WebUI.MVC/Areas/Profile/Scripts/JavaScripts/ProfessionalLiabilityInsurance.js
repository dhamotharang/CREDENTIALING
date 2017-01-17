
profileApp.controller('LiabilityCtrl', ['$scope', '$rootScope', '$http', 'dynamicFormGenerateService', 'masterDataService', 'messageAlertEngine', function ($scope, $rootScope, $http, dynamicFormGenerateService, masterDataService, messageAlertEngine) {

    $scope.ProfessionalLiabilityInfoes = [];

    $scope.submitButtonText = "Add";

  

    $scope.getLocation = function () {
        //$rootScope.tempObject.InsuranceCarrierAddressID = $scope.ClearSelect;
        $scope.Location = $scope.masterInsuranceCarriers.filter(function (masterInsuranceCarriers) { return masterInsuranceCarriers.InsuranceCarrierID == $scope.tempObject.InsuranceCarrierID })[0];
      
    };

    $scope.getAddress = function () {
        
        $scope.LocationAddress = $scope.Location.InsuranceCarrierAddresses.filter(function (Location) { return Location.InsuranceCarrierAddressID == $scope.tempObject.InsuranceCarrierAddressID })[0];
        //console.log("getting data");
        //console.log($scope.LocationAddress);
        $scope.tempObject.InsuranceCarrierAddress = $scope.LocationAddress;
        
    };

    $scope.resetDates = function () {
        try {
            $scope.tempObject.OriginalEffectiveDate = new Date();
            $scope.tempObject.EffectiveDate = new Date();
            $scope.tempObject.ExpirationDate = new Date();

        }
        catch (e)
        { }
    };

    ////******************************Master Data*****************************
   
    

    masterDataService.getMasterData("/Profile/MasterData/GetAllInsuranceCarriers").then(function (masterInsuranceCarriers) {
        $scope.masterInsuranceCarriers = masterInsuranceCarriers;
        //console.log("Addressssss...Location............");
        //console.log(masterInsuranceCarriers);
    });

  
    //calling the method using $on(PSP-public subscriber pattern)
    $rootScope.$on('ProfessionalLiabilityInfoes', function (event, val) {
        $scope.ProfessionalLiabilityInfoes = val;
        console.log("Aagaya Data");
        console.log(val);
        for (var i = 0; i < $scope.ProfessionalLiabilityInfoes.length ; i++) {
            $scope.ProfessionalLiabilityInfoes[i].OriginalEffectiveDate = ConvertDateFormat($scope.ProfessionalLiabilityInfoes[i].OriginalEffectiveDate);
            $scope.ProfessionalLiabilityInfoes[i].EffectiveDate = ConvertDateFormat($scope.ProfessionalLiabilityInfoes[i].EffectiveDate);
            $scope.ProfessionalLiabilityInfoes[i].ExpirationDate = ConvertDateFormat($scope.ProfessionalLiabilityInfoes[i].ExpirationDate);
        }
    });



    //====================== Professional Liability ===================
    $scope.CountryDialCodes = countryDailCodes;

    $scope.showContryCodeDiv = function (countryCodeDivId) {
        changeVisibilityOfCountryCode();
        $("#" + countryCodeDivId).show();
    };

    $scope.cancelProfessionalLiabilty = function (condition) {
        setProfessionalLiabiltyCancelParameters();
    };

    $scope.saveProfessionalLiabilty = function (professionalLiabilty,index) {

     
        

        var validationStatus;
        var url;
        var formData1;

        if ($scope.visibilityControl == 'addLi') {
            //Add Details - Denote the URL
            formData1 = $('#newShowProfessionalLiabiltyDiv').find('form');
            url = "/Profile/ProfessionalLiability/AddProfessionalLiabilityAsync?profileId=" + profileId;
        }
        else if ($scope.visibilityControl == (index + '_editLi')) {
            //Update Details - Denote the URL
            formData1 = $('#professionalLiabiltyEditDiv' + index).find('form');
            url = "/Profile/ProfessionalLiability/UpdateProfessionalLiabilityAsync?profileId=" + profileId;
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
                    //console.log("dekh data");
                    //console.log(data);
                    if (data.status == "true") {
                        data.professionalLiability.OriginalEffectiveDate = ConvertDateFormat(data.professionalLiability.OriginalEffectiveDate);
                        data.professionalLiability.EffectiveDate = ConvertDateFormat(data.professionalLiability.EffectiveDate);
                        data.professionalLiability.ExpirationDate = ConvertDateFormat(data.professionalLiability.ExpirationDate);

                        data.professionalLiability.InsuranceCarrierAddress = $scope.Location.InsuranceCarrierAddresses.filter(function (Location) { return Location.InsuranceCarrierAddressID == data.professionalLiability.InsuranceCarrierAddressID })[0];
                        for (var i = 0; i < $scope.masterInsuranceCarriers.length; i++) {
                            if ($scope.masterInsuranceCarriers[i].InsuranceCarrierID == data.professionalLiability.InsuranceCarrierID) {

                                data.professionalLiability.InsuranceCarrier = $scope.masterInsuranceCarriers[i];
                                break;
                            }

                        }

                        //data.professionalLiability

                        if ($scope.visibilityControl != (index + '_editLi')) {
                        
                            $scope.ProfessionalLiabilityInfoes.push(data.professionalLiability);
                            $rootScope.operateCancelControl('');
                            messageAlertEngine.callAlertMessage("addedNewProfessionalLiability", "Professional Liability saved successfully !!!!", "success", true);
                        }
                        else {
                            $scope.ProfessionalLiabilityInfoes[index] = data.professionalLiability;
                            $rootScope.operateViewAndAddControl(index + '_viewLi');
                            messageAlertEngine.callAlertMessage("updatedProfessionalLiability" + index, "Professional Liability updated successfully !!!!", "success", true);
                        }

                        $scope.IsProfLiabilityHasError = false;
                        $rootScope.tempObject = {};
                        $scope.resetDates();
                         FormReset(formData1);

                    } else {
                        messageAlertEngine.callAlertMessage('errorProfessionalLiability' + index, "", "danger", true);
                        $scope.errorProfessionalLiability = data.status.split(",");
                        //$scope.IsProfLiabilityHasError = true;
                        //$scope.ProfLiabilityErrorList = data.status.split(",");
                    }
                },
                error: function (e) {
                    messageAlertEngine.callAlertMessage('errorProfessionalLiability' + index, "", "danger", true);
                    $scope.errorProfessionalLiability = "Sorry for Inconvenience !!!! Please Try Again Later...";
                }
            });
        }
    };

   

    function ResetProfessionalLiabiltyForm() {
        $('#newShowProfessionalLiabiltyDiv').find('.professionalLiabiltyForm')[0].reset();
        $('#newShowProfessionalLiabiltyDiv').find('span').html('');
    }
}]);

$(document).ready(function () {
});