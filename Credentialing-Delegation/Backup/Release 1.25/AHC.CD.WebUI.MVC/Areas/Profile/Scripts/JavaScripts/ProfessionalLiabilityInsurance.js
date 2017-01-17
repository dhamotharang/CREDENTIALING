
profileApp.controller('LiabilityCtrl', ['$scope', '$rootScope', '$http', 'masterDataService', 'messageAlertEngine', function ($scope, $rootScope, $http, masterDataService, messageAlertEngine) {

    $scope.ProfessionalLiabilityInfoes = [];

    $scope.submitButtonText = "Add";

  

    $scope.getLocation = function () {
        $scope.tempObject.InsuranceCarrierAddressID = "";
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

    //to show renew div
    $scope.ShowRenewDiv = false;

    $scope.RenewDiv = function (Li) {
        if (Li.EffectiveDate == null || Li.ExpirationDate == null || Li.InsuranceCertificatePath == null)
        { $scope.ShowRenewDiv = false; }
        else
        {
            $scope.Location = $scope.masterInsuranceCarriers.filter(function (masterInsuranceCarriers) { return masterInsuranceCarriers.InsuranceCarrierID == Li.InsuranceCarrierID })[0];
            $scope.ShowRenewDiv = true;
        }
    };

    ////******************************Master Data*****************************
   
    
    $rootScope.$on("LoadRequireMasterData", function () {
        masterDataService.getMasterData("/Profile/MasterData/GetAllInsuranceCarriers").then(function (masterInsuranceCarriers) {
            $scope.masterInsuranceCarriers = masterInsuranceCarriers;
            //console.log("Addressssss...Location............");
            //console.log(masterInsuranceCarriers);
        });

    });
    //calling the method using $on(PSP-public subscriber pattern)
    $rootScope.$on('ProfessionalLiabilityInfoes', function (event, val) {
        $scope.ProfessionalLiabilityInfoes = val;
        //console.log("Aagaya Data");
        //console.log(val);
        //$scope.Location = $scope.ProfessionalLiabilityInfoes;
        //$scope.LocationAddress = $scope.ProfessionalLiabilityInfoes;
        //for (var i = 0; i < $scope.ProfessionalLiabilityInfoes.length ; i++) {            
        //    $scope.ProfessionalLiabilityInfoes[i].OriginalEffectiveDate = ConvertDateFormat($scope.ProfessionalLiabilityInfoes[i].OriginalEffectiveDate);
        //    $scope.ProfessionalLiabilityInfoes[i].EffectiveDate = ConvertDateFormat($scope.ProfessionalLiabilityInfoes[i].EffectiveDate);
        //    $scope.ProfessionalLiabilityInfoes[i].ExpirationDate = ConvertDateFormat($scope.ProfessionalLiabilityInfoes[i].ExpirationDate);
        //}
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
        loadingOn();
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
            $scope.getLocation();
            
        }
        else{
            //Update Details - Denote the URL
            formData1 = $('#professionalLiabiltyRenewDiv' + index).find('form');
            url = "/Profile/ProfessionalLiability/RenewProfessionalLiabilityAsync?profileId=" + profileId;
            //$scope.getLocation();

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
                    console.log(data);
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

                        if ($scope.visibilityControl == (index + '_editLi')) {
                            $scope.ProfessionalLiabilityInfoes[index] = data.professionalLiability;
                            $rootScope.operateViewAndAddControl(index + '_viewLi');
                            messageAlertEngine.callAlertMessage("updatedProfessionalLiability" + index, "Professional Liability updated successfully !!!!", "success", true);

                           }
                        else if ($scope.visibilityControl == (index + '_RenewLiability')) {

                            $scope.ProfessionalLiabilityInfoes[index] = data.professionalLiability;
                            $rootScope.operateViewAndAddControl(index + '_viewLi');
                            messageAlertEngine.callAlertMessage("renewedProfessionalLiability" + index, "Professional Liability information renewed successfully !!!!", "success", true);
                        }
                        else {
                            $scope.ProfessionalLiabilityInfoes.push(data.professionalLiability);
                            $rootScope.operateCancelControl('');
                            messageAlertEngine.callAlertMessage("addedNewProfessionalLiability", "Professional Liability saved successfully !!!!", "success", true);

                             }

                        $scope.IsProfLiabilityHasError = false;
                        $rootScope.tempObject = {};
                        $scope.resetDates();
                         FormReset(formData1);

                    } else {
                        messageAlertEngine.callAlertMessage('errorProfessionalLiability' + index, "", "danger", true);
                        $scope.errorProfessionalLiability = data.status.split(",");
                    }
                },
                error: function (e) {
                    messageAlertEngine.callAlertMessage('errorProfessionalLiability' + index, "", "danger", true);
                    $scope.errorProfessionalLiability = "Sorry for Inconvenience !!!! Please Try Again Later...";
                }
            });
        }
        loadingOff();
    };


    function ResetProfessionalLiabiltyForm() {
        $('#newShowProfessionalLiabiltyDiv').find('.professionalLiabiltyForm')[0].reset();
        $('#newShowProfessionalLiabiltyDiv').find('span').html('');
    }
}]);

$(document).ready(function () {
});