//--------------------- Angular Module ----------------------
var officeManagerApp = angular.module("OfficeManagerApp", ['ui.bootstrap']);

//Service for getting master data
officeManagerApp.service('masterDataService', ['$http', '$q', function ($http, $q) {

    this.getMasterData = function (URL) {
        return $http.get(URL).then(function (value) { return value.data; });
    };

}]);

officeManagerApp.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

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



///-------------------- Angulat controller----------------------
officeManagerApp.controller('OfficeManagerController', function ($scope, masterDataService, messageAlertEngine) {

    $scope.OfficeManagers = [];
    masterDataService.getMasterData(rootDir + "/MasterDataNew/GetAllMasterBusinessContactPerson").then(function (OfficeManagers) {
        $scope.OfficeManagers = OfficeManagers;
        for (var i = 0; i < $scope.OfficeManagers.length; i++) {
            if ($scope.OfficeManagers[i].MiddleName == null) {
                $scope.OfficeManagers[i].MiddleName = "";
            }
            if ($scope.OfficeManagers[i].Telephone==null)
            {
                $scope.OfficeManagers[i].Telephone = "";
            }
            if ($scope.OfficeManagers[i].Fax == null) {
                $scope.OfficeManagers[i].Fax = "";
            }
            if ($scope.OfficeManagers[i].EmailAddress == null) {
                $scope.OfficeManagers[i].EmailAddress = "";
            }
           
            $scope.OfficeManagers[i].Name = $scope.OfficeManagers[i].FirstName + " " + $scope.OfficeManagers[i].MiddleName + " " + $scope.OfficeManagers[i].LastName;
            
            
        }

    });

    $scope.AddMode = false;
    $scope.EditMode = false;
    $scope.ViewMode = false;
    $scope.ListMode = true;
    $scope.currentIndex = null;
    //-------------------- Add office managers ----------------------------------------
    $scope.addOfficeManagers = function () {
        $scope.tempSecondObject = [];
        $scope.tempSecondObject.CountryCodeTelephone = '+1';
        $scope.tempSecondObject.CountryCodeFax = '+1';
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
    $scope.saveOfficeManager = function (index) {

        $formData = $('#BusinessOfficeContactPersonForm');
        //ResetFormForValidation($formData);
        url = rootDir + "/MasterDataNew/AddOfficeManager";

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
                    FormReset($formData);
                    try {
                        $scope.tempSecondObject = null;
                        $scope.ListMode = true;
                        $scope.AddMode = false;
                        $scope.EditMode = false;
                        $scope.ViewMode = false;
                        if (data.status == "true") {
                           
                            if (data.officeManager.MiddleName == null) {
                                data.officeManager.MiddleName = "";                            }
                            
                            data.officeManager.Name = data.officeManager.FirstName + " " + data.officeManager.MiddleName + " " + data.officeManager.LastName;
                            $scope.OfficeManagers.push(data.officeManager);


                            messageAlertEngine.callAlertMessage("businessManagerSuccessMsg", "Office manager/Business Office Staff Contact Information Updated successfully.", "success", true);
                        }
                        else {
                            messageAlertEngine.callAlertMessage("alertOfficeManager", data.status, "danger", true);
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


    $scope.viewOfficeManager = function (obj) {

        $scope.tempSecondObject = obj;
        $scope.ViewMode = true;
        $scope.AddMode = false;
        $scope.ListMode = false;
        $scope.EditMode = false;


    }

    //---------------------edit office Manager ------------------------------------

    $scope.editOfficeManager = function (obj, index) {

        $scope.currentIndex = index;
        $scope.tempSecondObject = angular.copy(obj);
        $scope.ViewMode = false;
        $scope.AddMode = false;
        $scope.ListMode = false;
        $scope.EditMode = true;
    }

    $scope.updateOfficeManager = function () {

        $formData = $('#BusinessOfficeContactPersonForm');
        //ResetFormForValidation($formData);
        url = rootDir + "/MasterDataNew/UpdateOfficeManager";

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
                    FormReset($formData);
                    try {
                        $scope.EditMode = false;
                        $scope.AddMode = false;
                        $scope.ViewMode = false;
                        $scope.ListMode = true;
                        $scope.tempSecondObject = null;
                        if (data.status == "true") {
                            if (data.officeManager.MiddleName == null) {
                                data.officeManager.MiddleName = "";
                            }

                            data.officeManager.Name = data.officeManager.FirstName + " " + data.officeManager.MiddleName + " " + data.officeManager.LastName;
                            var officeIndex=$scope.OfficeManagers.findIndex(id => id.MasterEmployeeID == data.officeManager.MasterEmployeeID);
                            //$scope.OfficeManagers.push(data.officeManager);

                            $scope.OfficeManagers[officeIndex] = data.officeManager;
                            messageAlertEngine.callAlertMessage("businessManagerSuccessMsg", "Office manager/Business Office Staff Contact Information Updated successfully.", "success", true);
                        }
                        else {
                            messageAlertEngine.callAlertMessage("alertOfficeManager", data.status, "danger", true);
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

    //------------------------cancel ------------------------------
    $scope.cancel = function () {
        var $formData = $('#BusinessOfficeContactPersonForm');
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
        if ($scope.OfficeManagers) {
            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.OfficeManagers[startIndex]) {
                    $scope.CurrentPage.push($scope.OfficeManagers[startIndex]);
                } else {
                    break;
                }
            }
        }
    });
    //-------------- License Scope Watch ---------------------
    $scope.$watchCollection('OfficeManagers', function (newValue, oldValue) {
        if (newValue) {
            $scope.bigTotalItems = newValue.length;

            $scope.CurrentPage = [];
            $scope.bigCurrentPage = 1;

            var startIndex = ($scope.bigCurrentPage - 1) * 10;
            var endIndex = startIndex + 9;

            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.OfficeManagers[startIndex]) {
                    $scope.CurrentPage.push($scope.OfficeManagers[startIndex]);
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