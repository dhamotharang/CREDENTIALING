function showLocationList(ele) {
    $(ele).parent().find(".ProviderTypeSelectAutoList").first().show();
}
//--------------------- Angular Module ----------------------
var credentialingContactApp = angular.module("CredentialingContactApp", ['ui.bootstrap']);

//Service for getting master data
credentialingContactApp.service('masterDataService', ['$http', '$q', function ($http, $q) {

    this.getMasterData = function (URL) {
        return $http.get(URL).then(function (value) { return value.data; });
    };

}]);

credentialingContactApp.service('locationService', ['$http', function ($http) {
    this.getLocations = function (QueryString) {
        return $http.get(rootDir + "/Location/GetCities?city=" + QueryString)
        .then(function (response) { return response.data; });
    };
}]);



credentialingContactApp.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

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
credentialingContactApp.controller('CredentialingContactController', function ($scope, masterDataService, messageAlertEngine, locationService) {

    $scope.CredentialingContacts = [];
    masterDataService.getMasterData(rootDir + "/MasterDataNew/GetAllMasterCredentialingContactPerson").then(function (CredentialingContacts) {
        $scope.CredentialingContacts = CredentialingContacts;
        for (var i = 0; i < $scope.CredentialingContacts.length; i++) {
            if ($scope.CredentialingContacts[i].MiddleName == null) {
                $scope.CredentialingContacts[i].MiddleName = "";
            }
            if ($scope.CredentialingContacts[i].Telephone == null) {
                $scope.CredentialingContacts[i].Telephone = "";
            }
            if ($scope.CredentialingContacts[i].Fax == null) {
                $scope.CredentialingContacts[i].Fax = "";
            }
            if ($scope.CredentialingContacts[i].EmailAddress == null) {
                $scope.CredentialingContacts[i].EmailAddress = "";
            }
            if ($scope.CredentialingContacts[i].Country == null) {
                $scope.CredentialingContacts[i].Country = "";
            }
            if ($scope.CredentialingContacts[i].County == null){
                $scope.CredentialingContacts[i].County = "";
            }
            if ($scope.CredentialingContacts[i].ZipCode == null) {
                $scope.CredentialingContacts[i].ZipCode = "";
            }
            if ($scope.CredentialingContacts[i].Building == null) {
                $scope.CredentialingContacts[i].Building = "";
            }
            if ($scope.CredentialingContacts[i].County == null) {
                $scope.CredentialingContacts[i].County = "";
            }
            $scope.CredentialingContacts[i].Name = $scope.CredentialingContacts[i].FirstName + " " + $scope.CredentialingContacts[i].MiddleName + " " + $scope.CredentialingContacts[i].LastName;
            $scope.CredentialingContacts[i].Address = $scope.CredentialingContacts[i].Street + " " + $scope.CredentialingContacts[i].Building + " " + $scope.CredentialingContacts[i].City + " " + $scope.CredentialingContacts[i].State + " " + $scope.CredentialingContacts[i].Country + " "+ $scope.CredentialingContacts[i].County + " " + $scope.CredentialingContacts[i].ZipCode;
            

        }

    });

    $scope.AddMode = false;
    $scope.EditMode = false;
    $scope.ViewMode = false;
    $scope.ListMode = true;
    $scope.currentIndex = null;


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
    //------------country code--------------
    
    $scope.showContryCodeDiv = function (countryCodeDivId) {
        changeVisibilityOfCountryCode();
        $("#" + countryCodeDivId).show();
    };

    //-------------------- Add office managers ----------------------------------------
    $scope.addCredentialingContacts = function () {

        $scope.ListMode = false;
        $scope.AddMode = true;
    }

    $scope.saveCredentialingContacts = function () {

        $formData = $('#CredentialingContactPersonForm');
        //ResetFormForValidation($formData);
        url = rootDir + "/MasterDataNew/AddCredentialingContact";

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
                            if (data.credentialingContact.MiddleName == null) {
                                data.credentialingContact.MiddleName = "";
                            }
                            if (data.credentialingContact.Telephone == null) {
                                data.credentialingContact.Telephone = "";
                            }
                            if (data.credentialingContact.Fax == null) {
                                data.credentialingContact.Fax = "";
                            }
                            if (data.credentialingContact.EmailAddress == null) {
                                data.credentialingContact.EmailAddress = "";
                            }
                            if (data.credentialingContact.Country == null) {
                                data.credentialingContact.Country = "";
                            }
                            
                            if (data.credentialingContact.ZipCode == null) {
                                data.credentialingContact.ZipCode = "";
                            }
                            if (data.credentialingContact.Building == null) {
                                data.credentialingContact.Building = "";
                            }
                            if (data.credentialingContact.County == null) {
                                data.credentialingContact.County = "";
                            }
                            data.credentialingContact.Name = data.credentialingContact.FirstName + " " + data.credentialingContact.MiddleName + " " + data.credentialingContact.LastName;
                            data.credentialingContact.Address = data.credentialingContact.Street + " " + data.credentialingContact.Building + " " + data.credentialingContact.City + " " + data.credentialingContact.State + " " + data.credentialingContact.Country + " " + data.credentialingContact.County +" " + data.credentialingContact.ZipCode;

                            $scope.CredentialingContacts.push(data.credentialingContact);
                            messageAlertEngine.callAlertMessage("credentialingContactSuccessMsg", "Credentialing Contact Information Added successfully.", "success", true);
                        }
                        else {
                            messageAlertEngine.callAlertMessage("alertCredentialingContact", data.status, "danger", true);
                        }
                        FormReset($formData[0]);
                    } catch (e) {
              
                    }
                },
                error: function (e) {
                    FormReset($formData[0]);
                }
            });
        }
    }

    $scope.updateCredentialingContacts = function () {

        $formData = $('#CredentialingContactPersonForm');
        //ResetFormForValidation($formData);
        url = rootDir + "/MasterDataNew/UpdateCredentialingContact";

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
                            if (data.credentialingContact.MiddleName == null) {
                                data.credentialingContact.MiddleName = "";
                            }
                            if (data.credentialingContact.Telephone == null) {
                                data.credentialingContact.Telephone = "";
                            }
                            if (data.credentialingContact.Fax == null) {
                                data.credentialingContact.Fax = "";
                            }
                            if (data.credentialingContact.EmailAddress == null) {
                                data.credentialingContact.EmailAddress = "";
                            }
                            if (data.credentialingContact.Country == null) {
                                data.credentialingContact.Country = "";
                            }

                            if (data.credentialingContact.ZipCode == null) {
                                data.credentialingContact.ZipCode = "";
                            }
                            if (data.credentialingContact.Building == null) {
                                data.credentialingContact.Building = "";
                            }
                            if (data.credentialingContact.County == null) {
                                data.credentialingContact.County = "";
                            }
                            data.credentialingContact.Name = data.credentialingContact.FirstName + " " + data.credentialingContact.MiddleName + " " + data.credentialingContact.LastName;
                            data.credentialingContact.Address = data.credentialingContact.Street + " " + data.credentialingContact.Building + " " + data.credentialingContact.City + " " + data.credentialingContact.State + " " + data.credentialingContact.Country + " " + data.credentialingContact.County + " " + data.credentialingContact.ZipCode;

                            //$scope.CredentialingContacts[$scope.tempIndex]= data.credentialingContact;
                            for (var i in $scope.CredentialingContacts) {
                                if ($scope.CredentialingContacts[i].MasterEmployeeID == $scope.tempid) {
                                    $scope.CredentialingContacts[i] = {};
                                    $scope.CredentialingContacts[i] = data.credentialingContact;
                                }
                            }
                            messageAlertEngine.callAlertMessage("credentialingContactSuccessMsg", "Credentialing Contact Information Updated successfully.", "success", true);
                        }
                        else {
                            messageAlertEngine.callAlertMessage("alertCredentialingContact", data.status, "danger", true);
                        }
                        FormReset($formData[0]);
                    } catch (e) {

                    }
                },
                error: function (e) {
                    FormReset($formData[0]);
                }
            });
        }
    }

    //-----------------------view office managers ----------------------------
    $scope.tempSecondObject = null;
    $scope.tempSecondObject = { "Country": "United States", "CountryCodeTelephone": "+1", "CountryCodeFax": "+1" };


    $scope.viewCredentialingContacts = function (obj) {

        $scope.tempSecondObject = obj;
        $scope.ViewMode = true;
        $scope.AddMode = false;
        $scope.ListMode = false;
        $scope.EditMode = false;


    }

    //---------------------edit office Manager ------------------------------------

    $scope.editCredentialingContacts = function (obj, index) {
        $scope.tempid = obj.MasterEmployeeID;
        $scope.currentIndex = index;
        $scope.tempSecondObject = angular.copy(obj);
        $scope.ViewMode = false;
        $scope.AddMode = false;
        $scope.ListMode = false;
        $scope.EditMode = true;
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
    //------------------------cancel ------------------------------
    $scope.cancel = function () {
        FormReset($('#CredentialingContactPersonForm'));
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
        if ($scope.CredentialingContacts) {
            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.CredentialingContacts[startIndex]) {
                    $scope.CurrentPage.push($scope.CredentialingContacts[startIndex]);
                } else {
                    break;
                }
            }
        }
    });
    //-------------- License Scope Watch ---------------------
    $scope.$watchCollection('CredentialingContacts', function (newValue, oldValue) {
        if (newValue) {
            $scope.bigTotalItems = newValue.length;

            $scope.CurrentPage = [];
            $scope.bigCurrentPage = 1;

            var startIndex = ($scope.bigCurrentPage - 1) * 10;
            var endIndex = startIndex + 9;

            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.CredentialingContacts[startIndex]) {
                    $scope.CurrentPage.push($scope.CredentialingContacts[startIndex]);
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