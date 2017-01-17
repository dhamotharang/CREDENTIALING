//--------------------- Angular Module ----------------------
var billingContactApp = angular.module("BillingContactApp", ['ui.bootstrap']);

//Service for getting master data
billingContactApp.service('masterDataService', ['$http', '$q', function ($http, $q) {

    this.getMasterData = function (URL) {
        return $http.get(URL).then(function (value) { return value.data; });
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
billingContactApp.controller('BillingContactController', function ($scope, masterDataService, messageAlertEngine, $rootScope) {

    $scope.BillingContacts = [];
    masterDataService.getMasterData(rootDir + "/MasterDataNew/GetAllMasterBillingContactPerson").then(function (BillingContacts) {
        $scope.BillingContacts = BillingContacts;

    });

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
                    $scope.tempSecondObject = null;
                    $scope.ListMode = true;
                    $scope.AddMode = false;
                    $scope.EditMode = false;
                    $scope.ViewMode = false;
                    if (data.status == "true") {
                        $scope.BillingContacts.push(data.billingContact);
                        messageAlertEngine.callAlertMessage("billingContactSuccessMsg", "Billing Contact Information Added successfully.", "success", true);
                    }
                    else {
                        messageAlertEngine.callAlertMessage("alertBillingContact", data.status, "danger", true);
                    }
                },
                error: function (e) {

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
                    if (data.status == "true") {
                        $scope.BillingContacts[$scope.currentIndex] = data.billingContact;
                        messageAlertEngine.callAlertMessage("billingContactSuccessMsg", "Billing Contact Information Updated successfully.", "success", true);
                    }
                    else {
                        messageAlertEngine.callAlertMessage("alertBillingContact", data.status, "danger", true);
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
        ////console.log($scope.CurrentPageProviders);
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
            ////console.log($scope.CurrentPageProviders);
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