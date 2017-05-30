    
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
        $scope.PaymentRemittances1 = PaymentRemittances;

        for (var i = 0; i < PaymentRemittances.length; i++) {
            if (PaymentRemittances[i].BillingDepartment == null) {
                PaymentRemittances[i].BillingDepartment = "";
            }
            if (PaymentRemittances[i].PaymentAndRemittancePerson.MiddleName == null) {
                PaymentRemittances[i].PaymentAndRemittancePerson.MiddleName = "";
            }
            if (PaymentRemittances[i].PaymentAndRemittancePerson.Country == null) {
                PaymentRemittances[i].PaymentAndRemittancePerson.Country = "";
            }
            if (PaymentRemittances[i].PaymentAndRemittancePerson.County == null) {
                PaymentRemittances[i].PaymentAndRemittancePerson.County = "";
            }           
            if (PaymentRemittances[i].PaymentAndRemittancePerson.Street == null) {
                PaymentRemittances[i].PaymentAndRemittancePerson.Street = "";
            }
            if (PaymentRemittances[i].PaymentAndRemittancePerson.Building == null) {
                PaymentRemittances[i].PaymentAndRemittancePerson.Building = "";
            }
            if (PaymentRemittances[i].PaymentAndRemittancePerson.ZipCode == null) {
                PaymentRemittances[i].PaymentAndRemittancePerson.ZipCode = "";
            }
            if (PaymentRemittances[i].PaymentAndRemittancePerson.Telephone == null) {
                PaymentRemittances[i].PaymentAndRemittancePerson.Telephone = "";
            }
            if (PaymentRemittances[i].PaymentAndRemittancePerson.Fax == null) {
                PaymentRemittances[i].PaymentAndRemittancePerson.Fax = "";
            }
            if (PaymentRemittances[i].PaymentAndRemittancePerson.EmailAddress == null) {
                PaymentRemittances[i].PaymentAndRemittancePerson.EmailAddress = "";
            }
            var tempaddress = [];
            tempaddress.push(PaymentRemittances[i].PaymentAndRemittancePerson.Street,PaymentRemittances[i].PaymentAndRemittancePerson.Building,PaymentRemittances[i].PaymentAndRemittancePerson.City ,PaymentRemittances[i].PaymentAndRemittancePerson.State , PaymentRemittances[i].PaymentAndRemittancePerson.Country , PaymentRemittances[i].PaymentAndRemittancePerson.County ,PaymentRemittances[i].PaymentAndRemittancePerson.ZipCode);

            $scope.PaymentRemittances.push({
                MasterPracticePaymentRemittancePersonID: PaymentRemittances[i].MasterPracticePaymentRemittancePersonID,
                MasterEmployeeID: PaymentRemittances[i].PaymentAndRemittancePerson.MasterEmployeeID,
                Office: PaymentRemittances[i].Office,
                CheckPayableTo: PaymentRemittances[i].CheckPayableTo,
                ElectronicBillingCapability: PaymentRemittances[i].ElectronicBillingCapability,
                ElectronicBillingCapabilityYesNoOption: PaymentRemittances[i].ElectronicBillingCapabilityYesNoOption,
                BillingDepartment: PaymentRemittances[i].BillingDepartment,
                FirstName: PaymentRemittances[i].PaymentAndRemittancePerson.FirstName,
                MiddleName: PaymentRemittances[i].PaymentAndRemittancePerson.MiddleName,
                LastName: PaymentRemittances[i].PaymentAndRemittancePerson.LastName,
                Name: PaymentRemittances[i].PaymentAndRemittancePerson.FirstName + " " + PaymentRemittances[i].PaymentAndRemittancePerson.MiddleName + " " + PaymentRemittances[i].PaymentAndRemittancePerson.LastName,
                Telephone: PaymentRemittances[i].PaymentAndRemittancePerson.Telephone,
                CountryCodeFax: PaymentRemittances[i].PaymentAndRemittancePerson.CountryCodeFax,
                CountryCodeTelephone: PaymentRemittances[i].PaymentAndRemittancePerson.CountryCodeTelephone,
                Fax: PaymentRemittances[i].PaymentAndRemittancePerson.Fax,
                Street: PaymentRemittances[i].PaymentAndRemittancePerson.Street,
                Building: PaymentRemittances[i].PaymentAndRemittancePerson.Building,
                City: PaymentRemittances[i].PaymentAndRemittancePerson.City,
                State: PaymentRemittances[i].PaymentAndRemittancePerson.State,
                Country: PaymentRemittances[i].PaymentAndRemittancePerson.Country,
                County: PaymentRemittances[i].PaymentAndRemittancePerson.County,
                ZipCode: PaymentRemittances[i].PaymentAndRemittancePerson.ZipCode,
                //Address: PaymentRemittances[i].PaymentAndRemittancePerson.Street + " " 
                //    + PaymentRemittances[i].PaymentAndRemittancePerson.Building + " " 
                //    + PaymentRemittances[i].PaymentAndRemittancePerson.City + " " 
                //    + PaymentRemittances[i].PaymentAndRemittancePerson.State + " " 
                //    + PaymentRemittances[i].PaymentAndRemittancePerson.Country + " "
                //    +PaymentRemittances[i].PaymentAndRemittancePerson.County+" " 
                //    + PaymentRemittances[i].PaymentAndRemittancePerson.ZipCode,
                Address : $scope.ConstructAddress(tempaddress),
                EmailAddress: PaymentRemittances[i].PaymentAndRemittancePerson.EmailAddress
            })
        }
        console.log($scope.PaymentRemittances1);
        console.log($scope.PaymentRemittances);
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
    $scope.addPaymentRemittances = function () {

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
                        FormReset($formData);
                        if (data.status == "true") {
                            if (data.paymentRemittancePerson.PaymentAndRemittancePerson.MiddleName == null) {
                                data.paymentRemittancePerson.PaymentAndRemittancePerson.MiddleName = "";
                            }
                            if (data.paymentRemittancePerson.PaymentAndRemittancePerson.Country == null) {
                                data.paymentRemittancePerson.PaymentAndRemittancePerson.Country = "";
                            }
                            if (data.paymentRemittancePerson.PaymentAndRemittancePerson.County == null) {
                                data.paymentRemittancePerson.PaymentAndRemittancePerson.County = "";
                            }
                           
                            if (data.paymentRemittancePerson.PaymentAndRemittancePerson.Street == null) {
                                data.paymentRemittancePerson.PaymentAndRemittancePerson.Street = "";
                            }
                            if (data.paymentRemittancePerson.PaymentAndRemittancePerson.Building == null) {
                                data.paymentRemittancePerson.PaymentAndRemittancePerson.Building = "";
                            }
                            if (data.paymentRemittancePerson.PaymentAndRemittancePerson.ZipCode == null) {
                                data.paymentRemittancePerson.PaymentAndRemittancePerson.ZipCode = "";
                            }
                            if (data.paymentRemittancePerson.PaymentAndRemittancePerson.Telephone == null) {
                                data.paymentRemittancePerson.PaymentAndRemittancePerson.Telephone = "";
                            }
                            if (data.paymentRemittancePerson.PaymentAndRemittancePerson.Fax == null) {
                                data.paymentRemittancePerson.PaymentAndRemittancePerson.Fax = "";
                            }
                            if (data.paymentRemittancePerson.PaymentAndRemittancePerson.EmailAddress == null) {
                                data.paymentRemittancePerson.PaymentAndRemittancePerson.EmailAddress = "";
                            }
                            //$scope.PaymentRemittances.push(data.paymentRemittancePerson);
                            $scope.tempSecondObject = { "Country": "United States", "CountryCodeTelephone": "+1", "CountryCodeFax": "+1" };
                            var tempaddress = [];
                            tempaddress.push(data.paymentRemittancePerson.PaymentAndRemittancePerson.Street,data.paymentRemittancePerson.PaymentAndRemittancePerson.Building,data.paymentRemittancePerson.PaymentAndRemittancePerson.City ,data.paymentRemittancePerson.PaymentAndRemittancePerson.State , data.paymentRemittancePerson.PaymentAndRemittancePerson.Country , data.paymentRemittancePerson.PaymentAndRemittancePerson.County ,data.paymentRemittancePerson.PaymentAndRemittancePerson.ZipCode);
                            $scope.PaymentRemittances.push({
                                MasterPracticePaymentRemittancePersonID: data.paymentRemittancePerson.MasterPracticePaymentRemittancePersonID,
                                MasterEmployeeID: data.paymentRemittancePerson.PaymentAndRemittancePerson.MasterEmployeeID,
                                Office: data.paymentRemittancePerson.Office,
                                CheckPayableTo: data.paymentRemittancePerson.CheckPayableTo,
                                ElectronicBillingCapability: data.paymentRemittancePerson.ElectronicBillingCapability,
                                ElectronicBillingCapabilityYesNoOption: data.paymentRemittancePerson.ElectronicBillingCapabilityYesNoOption,
                                BillingDepartment: data.paymentRemittancePerson.BillingDepartment,
                                FirstName: data.paymentRemittancePerson.PaymentAndRemittancePerson.FirstName,
                                MiddleName: data.paymentRemittancePerson.PaymentAndRemittancePerson.MiddleName,
                                LastName: data.paymentRemittancePerson.PaymentAndRemittancePerson.LastName,
                                Name: data.paymentRemittancePerson.PaymentAndRemittancePerson.FirstName + " " + data.paymentRemittancePerson.PaymentAndRemittancePerson.MiddleName + " " + data.paymentRemittancePerson.PaymentAndRemittancePerson.LastName,
                                Telephone: data.paymentRemittancePerson.PaymentAndRemittancePerson.Telephone,
                                CountryCodeFax: data.paymentRemittancePerson.PaymentAndRemittancePerson.CountryCodeFax,
                                CountryCodeTelephone: data.paymentRemittancePerson.PaymentAndRemittancePerson.CountryCodeTelephone,
                                Fax: data.paymentRemittancePerson.PaymentAndRemittancePerson.Fax,
                                Street: data.paymentRemittancePerson.PaymentAndRemittancePerson.Street,
                                Building: data.paymentRemittancePerson.PaymentAndRemittancePerson.Building,
                                City: data.paymentRemittancePerson.PaymentAndRemittancePerson.City,
                                State: data.paymentRemittancePerson.PaymentAndRemittancePerson.State,
                                Country: data.paymentRemittancePerson.PaymentAndRemittancePerson.Country,
                                County: data.paymentRemittancePerson.PaymentAndRemittancePerson.County,
                                ZipCode: data.paymentRemittancePerson.PaymentAndRemittancePerson.ZipCode,
                                //Address: data.paymentRemittancePerson.PaymentAndRemittancePerson.Street + " " + data.paymentRemittancePerson.PaymentAndRemittancePerson.Building + " " + data.paymentRemittancePerson.PaymentAndRemittancePerson.City + " " + data.paymentRemittancePerson.PaymentAndRemittancePerson.State + " " + data.paymentRemittancePerson.PaymentAndRemittancePerson.Country +" "+data.paymentRemittancePerson.PaymentAndRemittancePerson.County+ " " + data.paymentRemittancePerson.PaymentAndRemittancePerson.ZipCode,
                                Address: $scope.ConstructAddress(tempaddress),
                                EmailAddress: data.paymentRemittancePerson.PaymentAndRemittancePerson.EmailAddress
                            });
                            messageAlertEngine.callAlertMessage("paymentRemittancePersonSuccessMsg", "Payment and Remittance Information Added successfully.", "success", true);
                        }
                        else {
                            messageAlertEngine.callAlertMessage("alertPaymentRemittancePerson", data.status, "danger", true);
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
    $scope.tempSecondObject = { "Country": "United States", "CountryCodeTelephone": "+1", "CountryCodeFax": "+1" };


    $scope.viewPaymentRemittances = function (obj) {

        $scope.tempSecondObject = obj;
        $scope.ViewMode = true;
        $scope.AddMode = false;
        $scope.ListMode = false;
        $scope.EditMode = false;


    }

    //---------------------edit office Manager ------------------------------------

    $scope.editPaymentRemittances = function (obj, index) {

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
                        FormReset($formData);
                        if (data.status == "true") {
                            if (data.paymentRemittancePerson.PaymentAndRemittancePerson.MiddleName == null) {
                                data.paymentRemittancePerson.PaymentAndRemittancePerson.MiddleName = "";
                            }
                            if (data.paymentRemittancePerson.PaymentAndRemittancePerson.Country == null) {
                                data.paymentRemittancePerson.PaymentAndRemittancePerson.Country = "";
                            }
                            if (data.paymentRemittancePerson.PaymentAndRemittancePerson.County == null) {
                                data.paymentRemittancePerson.PaymentAndRemittancePerson.County = "";
                            }
                            if (data.paymentRemittancePerson.PaymentAndRemittancePerson.Street == null) {
                                data.paymentRemittancePerson.PaymentAndRemittancePerson.Street = "";
                            }
                            if (data.paymentRemittancePerson.PaymentAndRemittancePerson.Building == null) {
                                data.paymentRemittancePerson.PaymentAndRemittancePerson.Building = "";
                            }
                            if (data.paymentRemittancePerson.PaymentAndRemittancePerson.ZipCode == null) {
                                data.paymentRemittancePerson.PaymentAndRemittancePerson.ZipCode = "";
                            }
                            if (data.paymentRemittancePerson.PaymentAndRemittancePerson.Telephone == null) {
                                data.paymentRemittancePerson.PaymentAndRemittancePerson.Telephone = "";
                            }
                            if (data.paymentRemittancePerson.PaymentAndRemittancePerson.Fax == null) {
                                data.paymentRemittancePerson.PaymentAndRemittancePerson.Fax = "";
                            }
                            if (data.paymentRemittancePerson.PaymentAndRemittancePerson.EmailAddress == null) {
                                data.paymentRemittancePerson.PaymentAndRemittancePerson.EmailAddress = "";
                            }
                            //$scope.PaymentRemittances[$scope.currentIndex] = data.paymentRemittancePerson;
                            var Payment = $scope.PaymentRemittances.findIndex(id =>  id.MasterEmployeeID == data.paymentRemittancePerson.PaymentAndRemittancePerson.MasterEmployeeID );
                            $scope.tempSecondObject = { "Country": "United States", "CountryCodeTelephone": "+1", "CountryCodeFax": "+1" };
                            var tempaddress = [];
                            tempaddress.push(data.paymentRemittancePerson.PaymentAndRemittancePerson.Street,data.paymentRemittancePerson.PaymentAndRemittancePerson.Building,data.paymentRemittancePerson.PaymentAndRemittancePerson.City ,data.paymentRemittancePerson.PaymentAndRemittancePerson.State , data.paymentRemittancePerson.PaymentAndRemittancePerson.Country , data.paymentRemittancePerson.PaymentAndRemittancePerson.County ,data.paymentRemittancePerson.PaymentAndRemittancePerson.ZipCode);
                            $scope.PaymentRemittances[Payment] = {
                                MasterPracticePaymentRemittancePersonID: data.paymentRemittancePerson.MasterPracticePaymentRemittancePersonID,
                                MasterEmployeeID: data.paymentRemittancePerson.PaymentAndRemittancePerson.MasterEmployeeID,
                                Office: data.paymentRemittancePerson.Office,
                                CheckPayableTo: data.paymentRemittancePerson.CheckPayableTo,
                                ElectronicBillingCapability: data.paymentRemittancePerson.ElectronicBillingCapability,
                                ElectronicBillingCapabilityYesNoOption: data.paymentRemittancePerson.ElectronicBillingCapabilityYesNoOption,
                                BillingDepartment: data.paymentRemittancePerson.BillingDepartment,
                                FirstName: data.paymentRemittancePerson.PaymentAndRemittancePerson.FirstName,
                                MiddleName: data.paymentRemittancePerson.PaymentAndRemittancePerson.MiddleName,
                                LastName: data.paymentRemittancePerson.PaymentAndRemittancePerson.LastName,
                                Name: data.paymentRemittancePerson.PaymentAndRemittancePerson.FirstName + " " + data.paymentRemittancePerson.PaymentAndRemittancePerson.MiddleName + " " + data.paymentRemittancePerson.PaymentAndRemittancePerson.LastName,
                                Telephone: data.paymentRemittancePerson.PaymentAndRemittancePerson.Telephone,
                                CountryCodeFax: data.paymentRemittancePerson.PaymentAndRemittancePerson.CountryCodeFax,
                                CountryCodeTelephone: data.paymentRemittancePerson.PaymentAndRemittancePerson.CountryCodeTelephone,
                                Fax: data.paymentRemittancePerson.PaymentAndRemittancePerson.Fax,
                                Street: data.paymentRemittancePerson.PaymentAndRemittancePerson.Street,
                                Building: data.paymentRemittancePerson.PaymentAndRemittancePerson.Building,
                                City: data.paymentRemittancePerson.PaymentAndRemittancePerson.City,
                                State: data.paymentRemittancePerson.PaymentAndRemittancePerson.State,
                                Country: data.paymentRemittancePerson.PaymentAndRemittancePerson.Country,
                                County: data.paymentRemittancePerson.PaymentAndRemittancePerson.County,
                                ZipCode: data.paymentRemittancePerson.PaymentAndRemittancePerson.ZipCode,
                                //Address: data.paymentRemittancePerson.PaymentAndRemittancePerson.Street + " " + data.paymentRemittancePerson.PaymentAndRemittancePerson.Building + " " + data.paymentRemittancePerson.PaymentAndRemittancePerson.City + " " + data.paymentRemittancePerson.PaymentAndRemittancePerson.State + " " + data.paymentRemittancePerson.PaymentAndRemittancePerson.Country + " " + data.paymentRemittancePerson.PaymentAndRemittancePerson.County + " " + data.paymentRemittancePerson.PaymentAndRemittancePerson.ZipCode,
                                Address: $scope.ConstructAddress(tempaddress),
                                EmailAddress: data.paymentRemittancePerson.PaymentAndRemittancePerson.EmailAddress
                            };
                            messageAlertEngine.callAlertMessage("paymentRemittancePersonSuccessMsg", "Payment and Remittance Information Updated successfully.", "success", true);
                        }
                        else {
                            messageAlertEngine.callAlertMessage("alertPaymentRemittancePerson", data.status, "danger", true);
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
    $scope.ConstructAddress = function(addressArray)
    {
        var Address = "";
        jQuery.grep(addressArray,function(ele, index){ Address += (ele.trim() == "" || ele == null) ? "" : index !=0 ? ", " + ele : ele; });
        return Address;
    }
    //------------------------cancel ------------------------------
    $scope.cancel = function () {
        var $formData = $('#PaymentRemittance');
        FormReset($formData);
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