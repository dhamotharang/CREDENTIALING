
profileApp.controller('LiabilityCtrl', ['$scope', '$rootScope', '$http', 'masterDataService', 'messageAlertEngine', '$filter', function ($scope, $rootScope, $http, masterDataService, messageAlertEngine, $filter) {

    $scope.ProfessionalLiabilityInfoes = [];
    $scope.submitButtonText = "Add";

    //------------------ author : krglv --------------------
    // ---------------------- clear on change ----------------------
    $scope.clearInsuranceCarrierLevel = function () {
        $scope.Location = [];
        $rootScope.tempObject.InsuranceCarrierAddress = {};
    };

    $scope.getLocation = function () {
        $rootScope.tempObject.InsuranceCarrierAddressID = "";
        var temp = $scope.masterInsuranceCarriers.filter(function (masterInsuranceCarriers) { return masterInsuranceCarriers.InsuranceCarrierID == $rootScope.tempObject.InsuranceCarrierID })[0];
        return temp.InsuranceCarrierAddresses;
    };

    $scope.getAddress = function () {        
        $scope.LocationAddress = $scope.Location.filter(function (location) { return location.InsuranceCarrierAddressID == $rootScope.tempObject.InsuranceCarrierAddressID })[0];
        $rootScope.tempObject.InsuranceCarrierAddress = $scope.LocationAddress;
    };

    //To Display the drop down div
    $scope.searchCumDropDown = function (divId) {
        $("#" + divId).show();
    };

    $scope.addIntoInsuranceDropDown = function (inscurance, div) {
        $rootScope.tempObject.InsuranceCarrier = angular.copy(inscurance);
        $rootScope.tempObject.InsuranceCarrierAddress = {};
        $scope.Location = angular.copy(inscurance.InsuranceCarrierAddresses);
        $("#" + div).hide();
    }

    $scope.addIntoAddressDropDown = function (insuranceCarrierAddress, div) {
        $rootScope.tempObject.InsuranceCarrierAddress = angular.copy(insuranceCarrierAddress);
        $("#" + div).hide();
    }

    //to show renew div
    $scope.ShowRenewDiv = false;

    $scope.RenewDiv = function (Li) {
        if (Li.ExpirationDate == null)
        { $scope.ShowRenewDiv = false; }
        else
        {
            $scope.Location = $scope.masterInsuranceCarriers.filter(function (masterInsuranceCarriers) { return masterInsuranceCarriers.InsuranceCarrierID == Li.InsuranceCarrierID })[0];
            $scope.ShowRenewDiv = true;
        }
    };

    ////******************************Master Data*****************************
    
    $rootScope.$on("LoadRequireMasterDataProfessionalLiability", function () {
        masterDataService.getMasterData("/Profile/MasterData/GetAllInsuranceCarriers").then(function (masterInsuranceCarriers) {
            $scope.masterInsuranceCarriers = masterInsuranceCarriers;
        });
    });
    //calling the method using $on(PSP-public subscriber pattern)
    $rootScope.$on('ProfessionalLiabilityInfoes', function (event, val) {
        $scope.ProfessionalLiabilityInfoes = val;
        for (var i = 0; i < $scope.ProfessionalLiabilityInfoes.length ; i++) {
            if (!$scope.ProfessionalLiabilityInfoes[i].InsuranceCarrierAddressID) { $scope.ProfessionalLiabilityInfoes[i].InsuranceCarrierAddressID = ""; }
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
        loadingOn();
        var validationStatus;
        var url;
        var myData = {};
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
            $scope.Location = $scope.getLocation();
            
        }
        else{
            //Update Details - Denote the URL
            formData1 = $('#professionalLiabiltyRenewDiv' + index).find('form');
            url = "/Profile/ProfessionalLiability/RenewProfessionalLiabilityAsync?profileId=" + profileId;
            $scope.Location = $scope.getLocation();
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
                        data.professionalLiability.OriginalEffectiveDate = ConvertDateFormat(data.professionalLiability.OriginalEffectiveDate);
                        data.professionalLiability.EffectiveDate = ConvertDateFormat(data.professionalLiability.EffectiveDate);
                        data.professionalLiability.ExpirationDate = ConvertDateFormat(data.professionalLiability.ExpirationDate);
                        
                        if (!data.professionalLiability.InsuranceCarrierAddressID) { data.professionalLiability.InsuranceCarrierAddressID = ""; }

                        data.professionalLiability.InsuranceCarrierAddress = $scope.Location.filter(function (location) { return location.InsuranceCarrierAddressID == data.professionalLiability.InsuranceCarrierAddressID })[0];
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
                        myData = data;
                        $scope.IsProfLiabilityHasError = false;
                        $rootScope.tempObject = {};
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

        $rootScope.$broadcast('UpdateProfessionalLiabilityInfoDoc', myData);

        loadingOff();
    };


    function ResetProfessionalLiabiltyForm() {
        $('#newShowProfessionalLiabiltyDiv').find('.professionalLiabiltyForm')[0].reset();
        $('#newShowProfessionalLiabiltyDiv').find('span').html('');
    }

    $scope.initProfessionalLiabilityWarning = function (Li) {
        if (angular.isObject(Li)) {
            $scope.tempProfessionalLiability = Li;
        }
        $('#professionalLiabilityWarningModal').modal();
    };

    $scope.removeProfessionalLiability = function (ProfessionalLiabilityInfoes) {
        var validationStatus = false;
        var url = null;
        var myData = {};
        var $formData = null;
        $formData = $('#removeProfessionalLiability');
        url = "/Profile/ProfessionalLiability/RemoveProfessionalLiability?profileId=" + profileId;
        ResetFormForValidation($formData);
        validationStatus = $formData.valid();

        if (validationStatus) {
            //Simple POST request example (passing data) :
            $.ajax({
                url: url,
                type: 'POST',
                data: new FormData($formData[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    //console.log(data.status);
                    if (data.status == "true") {
                        var obj = $filter('filter')(ProfessionalLiabilityInfoes, { ProfessionalLiabilityInfoID: data.professionalLiability.ProfessionalLiabilityInfoID })[0];
                        ProfessionalLiabilityInfoes.splice(ProfessionalLiabilityInfoes.indexOf(obj), 1);
                        if ($scope.dataFetchedPL == true) {
                            obj.HistoryStatus = 'Deleted';
                            $scope.ProfessionalLiabilityInfoHistory.push(obj);
                        }
                        $('#professionalLiabilityWarningModal').modal('hide');
                        $rootScope.operateCancelControl('');
                        myData = data;
                        messageAlertEngine.callAlertMessage("addedNewProfessionalLiability", "Professional Liability Removed successfully.", "success", true);
                    } else {
                        $('#professionalLiabilityWarningModal').modal('hide');
                        messageAlertEngine.callAlertMessage("removeProfessionalLiability", data.status, "danger", true);
                        $scope.errorProfessionalLiability = "Sorry for Inconvenience !!!! Please Try Again Later...";
                    }
                },
                error: function (e) {

                }
            });
        }
        $rootScope.$broadcast('RemoveProfessionalLiabilityInfoDoc', myData);
    };

    $rootScope.ProfessionalLiabilityLoaded = true;
    $scope.dataLoaded = false;
    $rootScope.$on('ProfessionalLiability', function () {
        if (!$scope.dataLoaded) {
            $rootScope.ProfessionalLiabilityLoaded = false;
            //console.log("Getting data....");
            $http({
                method: 'GET',
                url: '/Profile/MasterProfile/GetProfessionalLiabilitiesProfileDataAsync?profileId=' + profileId
            }).success(function (data, status, headers, config) {
                //console.log(data);
                try {
                    for (key in data) {
                        //console.log(key);
                        $rootScope.$emit(key, data[key]);
                        //call respective controller to load data (PSP)
                    }
                    $rootScope.ProfessionalLiabilityLoaded = true;
                    $rootScope.$broadcast("LoadRequireMasterDataProfessionalLiability");
                } catch (e) {
                    //console.log("error getting data back");
                    $rootScope.ProfessionalLiabilityLoaded = true;
                }

            }).error(function (data, status, headers, config) {
                //console.log(status);
                $rootScope.ProfessionalLiabilityLoaded = true;
            });
            $scope.dataLoaded = true;
        }
    });

    //....................Professional Liability History............................//
    $scope.ProfessionalLiabilityInfoHistory = [];
    $scope.dataFetchedPL = false;

    $scope.showLiabilityHistory = function (loadingId) {
        if ($scope.ProfessionalLiabilityInfoHistory.length == 0) {
            $("#" + loadingId).css('display', 'block');
            var url = "/Profile/ProfileHistory/GetAllProfessionalLiabilityInfoHistory?profileId=" + profileId;
            $http.get(url).success(function (data) {
                $scope.ProfessionalLiabilityInfoHistory = data;
                for (var i = 0; i < $scope.ProfessionalLiabilityInfoHistory.length; i++) {
                    if ($scope.ProfessionalLiabilityInfoHistory[i].HistoryStatus == '' || !$scope.ProfessionalLiabilityInfoHistory[i].HistoryStatus) {
                        $scope.ProfessionalLiabilityInfoHistory[i].HistoryStatus = 'Renewed';
                    }
                }
                $scope.showLiabilityHistoryTable = true;
                $scope.dataFetchedPL = true;
                $("#" + loadingId).css('display', 'none');
            });
        }
        else {
            $scope.showLiabilityHistoryTable = true;
        }
    }

    $scope.cancelLiabilityHistory = function () {
        $scope.showLiabilityHistoryTable = false;
    }

}]);