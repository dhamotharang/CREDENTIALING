

function showLocationList(ele) {
    $(ele).parent().find(".ProviderTypeSelectAutoList").first().show();
}
//--------------------- Angular Module ----------------------
var billingContactApp = angular.module("BillingContactApp", ['ui.bootstrap']);

//Service for getting master data
billingContactApp.service('masterDataService', ['$http', '$q', function ($http, $q) {

    this.getMasterData = function (URL) {
        return $http.get(URL).then(function (value) { return value.data; });
    };

}]);

billingContactApp.service('locationService', ['$http', function ($http) {
    this.getLocations = function (QueryString) {
        return $http.get(rootDir + "/Location/GetCities?city=" + QueryString)
        .then(function (response) { return response.data; });
    };
}]);


billingContactApp.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

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



//-------------------- Angulat controller----------------------
billingContactApp.controller('BillingContactController', function ($scope, masterDataService, messageAlertEngine, $rootScope, locationService) {

    $scope.BillingContacts = [];
    masterDataService.getMasterData(rootDir + "/MasterDataNew/GetAllMasterBillingContactPerson").then(function (BillingContacts) {
        $scope.BillingContacts = BillingContacts;
        for (var i = 0; i < $scope.BillingContacts.length; i++) {
            if ($scope.BillingContacts[i].MiddleName == null) {
                $scope.BillingContacts[i].MiddleName = "";
            }
            if ($scope.BillingContacts[i].Telephone == null) {
                $scope.BillingContacts[i].Telephone = "";
            }
            if ($scope.BillingContacts[i].Fax == null) {
                $scope.BillingContacts[i].Fax = "";
            }
            if ($scope.BillingContacts[i].EmailAddress == null) {
                $scope.BillingContacts[i].EmailAddress = "";
            }
            if ($scope.BillingContacts[i].Country == null) {
                $scope.BillingContacts[i].Country = "";
            }
            if ($scope.BillingContacts[i].County == null) {
                $scope.BillingContacts[i].County = "";
            }
            if ($scope.BillingContacts[i].Building == null) {
                $scope.BillingContacts[i].Building = "";
            }
            if ($scope.BillingContacts[i].ZipCode == null) {
                $scope.BillingContacts[i].ZipCode = "";
            }            
            $scope.BillingContacts[i].Name = $scope.BillingContacts[i].FirstName + " " + $scope.BillingContacts[i].MiddleName + " " + $scope.BillingContacts[i].LastName;
            $scope.BillingContacts[i].Address = $scope.BillingContacts[i].Street + " " + $scope.BillingContacts[i].Building + " " + $scope.BillingContacts[i].City + " " + $scope.BillingContacts[i].State + " " + $scope.BillingContacts[i].Country + " " + $scope.BillingContacts[i].County + " " + $scope.BillingContacts[i].ZipCode;
        }
    });


    //------------------------city--------------------------------------
    $scope.addressHomeAutocomplete = function (location) {
        $scope.resetAddressModels();

        $scope.tempSecondObject.City = location;
        if (location.length > 1 && !angular.isObject(location)) {
            locationService.getLocations(location).then(function (val) {
                $scope.Locations = val;
            });
        } else if (angular.isObject(location)) {
            $scope.setAddressModels(location);
        }
    };

    $scope.setAddressModels = function (location) {
        $scope.tempSecondObject.City = location.City;
        $scope.tempSecondObject.State = location.State;
        $scope.tempSecondObject.Country = location.Country;

    };
    $scope.selectedLocation1 = function (location) {
        $scope.setAddressModels(location);
        $(".ProviderTypeSelectAutoList").hide();
    };

    $scope.resetAddressModels = function () {
        $scope.tempSecondObject.State = "";
        $scope.tempSecondObject.Country = "";
    };
    //------------------------city---------------------------------------------

    $scope.AddMode = false;
    $scope.EditMode = false;
    $scope.ViewMode = false;
    $scope.ListMode = true;
    $scope.currentIndex = null;

    //-------------------- Add office managers ----------------------------------------
    $scope.addBillingContacts = function () {

        $scope.ListMode = false;
        $scope.AddMode = true;
    }

    var FormReset = function ($form) {

        // get validator object
        var $validator = $form.validate();

        // get errors that were created using jQuery.validate.unobtrusive
        var $errors = $form.find(".field-validation-error span");

        // trick unobtrusive to think the elements were successfully validated
        // this removes the validation messages
        $errors.each(function () {
            $validator.settings.success($(this));
        });
        // clear errors from validation
        $validator.resetForm();
    };

    $scope.saveBillingContacts = function () {

        $formData = $('#BillingContactPersonForm');
        //ResetFormForValidation($formData);
        url = rootDir + "/MasterDataNew/AddBillingContact";

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
                        FormReset($formData);
                        if (data.status == "true") {
                            if (data.billingContact.MiddleName == null) {
                                data.billingContact.MiddleName = "";
                            }
                            if (data.billingContact.Telephone == null) {
                                data.billingContact.Telephone = "";
                            }
                            if (data.billingContact.Fax == null) {
                                data.billingContact.Fax = "";
                            }
                            if (data.billingContact.EmailAddress == null) {
                                data.billingContact.EmailAddress = "";
                            }
                            if (data.billingContact.Country == null) {
                                data.billingContact.Country = "";
                            }
                            if (data.billingContact.County == null) {
                                data.billingContact.County = "";
                            }
                            if (data.billingContact.Building == null) {
                                data.billingContact.Building = "";
                            }
                            if (data.billingContact.ZipCode == null) {
                                data.billingContact.ZipCode = "";
                            }
                            data.billingContact.Name = data.billingContact.FirstName + " " + data.billingContact.MiddleName + " " + data.billingContact.LastName;
                            data.billingContact.Address = data.billingContact.Street + " " + data.billingContact.Building + " " + data.billingContact.City + " " + data.billingContact.State + " " + data.billingContact.Country +  " " + data.billingContact.County + " " + data.billingContact.ZipCode;

                            $scope.BillingContacts.push(data.billingContact);
                            messageAlertEngine.callAlertMessage("billingContactSuccessMsg", "Billing Contact Information Added successfully.", "success", true);
                            
                        }
                        else {
                            messageAlertEngine.callAlertMessage("alertBillingContact", data.status, "danger", true);
                        }
                    } catch (e) {
                        
                    }
                },
                error: function (e) {
                    FormReset($formData);
                }
            });
        }
    }

    //-----------------------view office managers ----------------------------
    $scope.tempSecondObject = null;

    $scope.viewBillingContacts = function (obj) {

        $scope.tempSecondObject = obj;
        $scope.ViewMode = true;
        $scope.AddMode = false;
        $scope.ListMode = false;
        $scope.EditMode = false;


    }

    //---------------------edit office Manager ------------------------------------

    $scope.editBillingContacts = function (obj,index) {

        $scope.tempSecondObject = angular.copy(obj);
        $scope.currentIndex = index;
        $scope.ViewMode = false;
        $scope.AddMode = false;
        $scope.ListMode = false;
        $scope.EditMode = true;
    }

    $scope.updateBillingContacts = function () {

        $formData = $('#BillingContactPersonForm');
        //ResetFormForValidation($formData);
        url = rootDir + "/MasterDataNew/UpdateBillingContact";

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

                    $scope.EditMode = false;
                    $scope.AddMode = false;
                    $scope.ViewMode = false;
                    $scope.ListMode = true;
                    $scope.tempSecondObject = null;
                    FormReset($formData);
                    if (data.status == "true") {
                        if (data.billingContact.MiddleName == null) {
                            data.billingContact.MiddleName = "";
                        }
                        if (data.billingContact.Telephone == null) {
                            data.billingContact.Telephone = "";
                        }
                        if (data.billingContact.Fax == null) {
                            data.billingContact.Fax = "";
                        }
                        if (data.billingContact.EmailAddress == null) {
                            data.billingContact.EmailAddress = "";
                        }
                        if (data.billingContact.Country == null) {
                            data.billingContact.Country = "";
                        }
                        if (data.billingContact.County == null) {
                            data.billingContact.County = "";
                        }
                        if (data.billingContact.ZipCode == null) {
                            data.billingContact.ZipCode = "";
                        }
                        data.billingContact.Name = data.billingContact.FirstName + " " + data.billingContact.MiddleName + " " + data.billingContact.LastName;
                        data.billingContact.Address = data.billingContact.Street + " " + data.billingContact.Building + " " + data.billingContact.City + " " + data.billingContact.State + " " + data.billingContact.Country +" " +data.billingContact.County + " " + data.billingContact.ZipCode;
                        
                        var curIndex = $scope.BillingContacts.findIndex(id =>  id.MasterEmployeeID == data.billingContact.MasterEmployeeID );
                        $scope.BillingContacts[curIndex] = data.billingContact;
                        messageAlertEngine.callAlertMessage("billingContactSuccessMsg", "Billing Contact Information Updated successfully.", "success", true);
                    }
                    else {
                        messageAlertEngine.callAlertMessage("alertBillingContact", data.status, "danger", true);
                    }
                },
                error: function (e) {
                    FormReset($formData);
                }
            });
        }
        //$formData[0].reset();
    }

    //------------------------cancel ------------------------------
    $scope.cancel = function () {
        var $formData = $('#BillingContactPersonForm');
        FormReset($formData);
        $scope.tempSecondObject = null;
        $scope.tempSecondObject={"Country":"United States","CountryCodeTelephone":"+1","CountryCodeFax":"+1"};
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
        if ($scope.BillingContacts) {
            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.BillingContacts[startIndex]) {
                    $scope.CurrentPage.push($scope.BillingContacts[startIndex]);
                } else {
                    break;
                }
            }
        }
    });
    //-------------- License Scope Watch ---------------------
    $scope.$watchCollection('BillingContacts', function (newValue, oldValue) {
        if (newValue) {
            $scope.bigTotalItems = newValue.length;

            $scope.CurrentPage = [];
            $scope.bigCurrentPage = 1;

            var startIndex = ($scope.bigCurrentPage - 1) * 10;
            var endIndex = startIndex + 9;

            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.BillingContacts[startIndex]) {
                    $scope.CurrentPage.push($scope.BillingContacts[startIndex]);
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
    ///------------------- end ------------------

});