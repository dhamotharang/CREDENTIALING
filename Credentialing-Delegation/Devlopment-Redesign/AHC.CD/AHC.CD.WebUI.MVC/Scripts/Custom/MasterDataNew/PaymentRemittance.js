
function showLocationList(ele) {
    $(ele).parent().find(".ProviderTypeSelectAutoList").first().show();
}

//--------------------- Angular Module ----------------------
var paymentRemittanceApp = angular.module("PaymentRemittanceApp", ['ui.bootstrap']);

///Service for getting master data
paymentRemittanceApp.service('masterDataService', ['$http', '$q', function ($http, $q) {

    this.getMasterData = function (URL) {
        return $http.get(URL).then(function (value) { return value.data; });
    };

}]);

paymentRemittanceApp.service('locationService', ['$http', function ($http) {
    this.getLocations = function (QueryString) {
        return $http.get(rootDir + "/Location/GetCities?city=" + QueryString)
        .then(function (response) { return response.data; });
    };
}]);

paymentRemittanceApp.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

    $rootScope.messageDesc = "";
    $rootScope.activeMessageDiv = "";
    $rootScope.messageType = "";

    var animateMessageAlertOff = function () {
        $rootScope.closeAlertMessage();
    };

    this.callAlertMessage = function (calledDiv, msg, msgType, dismissal) { //messageAlertEngine.callAlertMessage('updateHospitalPrivilege' + IndexValue, "Data Updated Successfully !!!!", "success", true);                            
        $rootScope.activeMessageDiv = calledDiv;
        $rootScope.messageDesc = msg;
        $rootScope.messageType = msgType;
        if (dismissal) {
            $timeout(animateMessageAlertOff, 5000);
        }
    }

    $rootScope.closeAlertMessage = function () {
        $rootScope.messageDesc = "";
        $rootScope.activeMessageDiv = "";
        $rootScope.messageType = "";
    }
}]);




//-------------------- Angular controller----------------------
paymentRemittanceApp.controller('PaymentRemittanceController', function ($scope, masterDataService, messageAlertEngine, locationService) {

    $scope.PaymentRemittances = [];
    masterDataService.getMasterData(rootDir + "/MasterDataNew/GetAllMasterPaymentRemittancePerson").then(function (PaymentRemittances) {
        $scope.PaymentRemittances = PaymentRemittances;
        console.log($scope.PaymentRemittances);
    });

    //------------------------city--------------------------------------
    $scope.addressHomeAutocomplete = function (location) {
        $scope.resetAddressModels();

        $scope.tempSecondObject.PaymentAndRemittancePerson.City = location;
        if (location.length > 1 && !angular.isObject(location)) {
            locationService.getLocations(location).then(function (val) {
                $scope.Locations = val;
            });
        } else if (angular.isObject(location)) {
            $scope.setAddressModels(location);
        }
    };

    $scope.setAddressModels = function (location) {
        $scope.tempSecondObject.PaymentAndRemittancePerson.City = location.City;
        $scope.tempSecondObject.PaymentAndRemittancePerson.State = location.State;
        $scope.tempSecondObject.PaymentAndRemittancePerson.Country = location.Country;

    };
    $scope.selectedLocation1 = function (location) {
        $scope.setAddressModels(location);
        $(".ProviderTypeSelectAutoList").hide();
    };

    $scope.resetAddressModels = function () {
        $scope.tempSecondObject.PaymentAndRemittancePerson.State = "";
        $scope.tempSecondObject.PaymentAndRemittancePerson.Country = "";
    };
    //------------------------city---------------------------------------------
    $scope.AddMode = false;
    $scope.EditMode = false;
    $scope.ViewMode = false;
    $scope.ListMode = true;
    $scope.currentIndex = null;
    //-------------------- Add office managers ----------------------------------------
    $scope.addPaymentRemittances = function () {

        $scope.ListMode = false;
        $scope.AddMode = true;
    }

    $scope.savePaymentRemittances = function () {

        $formData = $('#PaymentRemittance');
        //ResetFormForValidation($formData);
        url = rootDir + "/MasterDataNew/AddPaymentRemittancePerson";

        validationStatus = $formData.valid();

        if (validationStatus) {
            $.ajax({
                url: url,
                type: 'POST',
                data: new FormData($formData[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    try {
                        $scope.tempSecondObject = null;
                        $scope.ListMode = true;
                        $scope.AddMode = false;
                        $scope.EditMode = false;
                        $scope.ViewMode = false;
                        if (data.status == "true") {
                            $scope.PaymentRemittances.push(data.paymentRemittancePerson);
                            messageAlertEngine.callAlertMessage("paymentRemittancePersonSuccessMsg", "PaymentRemittancePerson Information Added successfully.", "success", true);
                        }
                        else {
                            messageAlertEngine.callAlertMessage("alertPaymentRemittancePerson", data.status, "danger", true);
                        }
                    } catch (e) {
                       
                    }
                },
                error: function (e) {

                }
            });
        }
    }

    //-----------------------view office managers ----------------------------
    $scope.tempSecondObject = null;
    $scope.tempSecondObject = { "Country": "United States", "CountryCodeTelephone": "+1", "CountryCodeFax": "+1" };


    $scope.viewPaymentRemittances = function (obj) {

        $scope.tempSecondObject = obj;
        $scope.ViewMode = true;
        $scope.AddMode = false;
        $scope.ListMode = false;
        $scope.EditMode = false;


    }

    //---------------------edit office Manager ------------------------------------

    $scope.editPaymentRemittances = function (obj,index) {

        $scope.currentIndex = index;

        $scope.tempSecondObject = angular.copy(obj);
        $scope.ViewMode = false;
        $scope.AddMode = false;
        $scope.ListMode = false;
        $scope.EditMode = true;
    }

    $scope.updatePaymentRemittances = function () {

        $formData = $('#PaymentRemittance');
        //ResetFormForValidation($formData);
        url = rootDir + "/MasterDataNew/UpdatePaymentRemittancePerson";

        validationStatus = $formData.valid();

        if (validationStatus) {
            $.ajax({
                url: url,
                type: 'POST',
                data: new FormData($formData[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    try {
                        $scope.EditMode = false;
                        $scope.AddMode = false;
                        $scope.ViewMode = false;
                        $scope.ListMode = true;
                        $scope.tempSecondObject = null;
                        if (data.status == "true") {
                            $scope.PaymentRemittances[$scope.currentIndex] = data.paymentRemittancePerson;
                            messageAlertEngine.callAlertMessage("paymentRemittancePersonSuccessMsg", "PaymentRemittancePerson Information Updated successfully.", "success", true);
                        }
                        else {
                            messageAlertEngine.callAlertMessage("alertPaymentRemittancePerson", data.status, "danger", true);
                        }
                    } catch (e) {
                      
                    }
                },
                error: function (e) {

                }
            });
        }
    }

    //------------------------cancel ------------------------------
    $scope.cancel = function () {

        $scope.tempSecondObject = null;
        $scope.tempSecondObject = { "Country": "United States", "CountryCodeTelephone": "+1", "CountryCodeFax": "+1" };

        $scope.EditMode = false;
        $scope.AddMode = false;
        $scope.ViewMode = false;
        $scope.ListMode = true;
    }

    //------------------------------- Country Code Popover Show by Id ---------------------------------------
    $(document).click(function (event) {
        if (!$(event.target).hasClass("countryCodeClass") && $(event.target).parents(".countryDailCodeContainer").length === 0) {
            $(".countryDailCodeContainer").hide();
        }
        if (!$(event.target).hasClass("form-control") && $(event.target).parents(".LanguageSelectAutoList").length === 0) {
            $(".LanguageSelectAutoList").hide();
        }
        if (!$(event.target).hasClass("form-control") && $(event.target).parents(".ProviderTypeSelectAutoList").length === 0) {
            $(".ProviderTypeSelectAutoList").hide();
        }
    });

    $(".countryDailCodeContainer").hide();
    $scope.CountryDialCodes = countryDailCodes;


    $scope.showContryCodeDiv = function (countryCodeDivId) {
        changeVisibilityOfCountryCode();
        $("#" + countryCodeDivId).show();
    };

    $scope.hideDiv = function () {
        $(".countryDailCodeContainer").hide();
    }

    var changeVisibilityOfCountryCode = function () {
        $(".countryDailCodeContainer").hide();
        // method will close any other country code div already open.
    };


    //--------------------table-----------------------------------------------------
    $scope.CurrentPage = [];

    //-------------------------- angular bootstrap pagger with custom-----------------
    $scope.maxSize = 5;
    $scope.bigTotalItems = 0;
    $scope.bigCurrentPage = 1;

    //-------------------- page change action ---------------------
    $scope.pageChanged = function (pagnumber) {
        $scope.bigCurrentPage = pagnumber;
    };

    //-------------- current page change Scope Watch ---------------------
    $scope.$watch('bigCurrentPage', function (newValue, oldValue) {
        $scope.CurrentPage = [];
        var startIndex = (newValue - 1) * 10;
        var endIndex = startIndex + 9;
        if ($scope.PaymentRemittances) {
            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.PaymentRemittances[startIndex]) {
                    $scope.CurrentPage.push($scope.PaymentRemittances[startIndex]);
                } else {
                    break;
                }
            }
        }
    });
    //-------------- License Scope Watch ---------------------
    $scope.$watchCollection('PaymentRemittances', function (newValue, oldValue) {
        if (newValue) {
            $scope.bigTotalItems = newValue.length;

            $scope.CurrentPage = [];
            $scope.bigCurrentPage = 1;

            var startIndex = ($scope.bigCurrentPage - 1) * 10;
            var endIndex = startIndex + 9;

            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.PaymentRemittances[startIndex]) {
                    $scope.CurrentPage.push($scope.PaymentRemittances[startIndex]);
                } else {
                    break;
                }
            }
        }
    });
    $scope.filterData = function () {
        $scope.pageChanged(1);
    }

    $scope.IsValidIndex = function (index) {

        var startIndex = ($scope.bigCurrentPage - 1) * 10;
        var endIndex = startIndex + 9;

        if (index >= startIndex && index <= endIndex)
            return true;
        else
            return false;

    }
    //------------------- end ------------------

});