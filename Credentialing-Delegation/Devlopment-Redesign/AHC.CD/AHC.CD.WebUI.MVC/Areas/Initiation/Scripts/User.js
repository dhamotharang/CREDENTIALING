
//============================profile initiation Angular Module ==========================
var userApp = angular.module("UserApp", ['mgcrea.ngStrap']);


//============================Service for sucess/error mesages ============================
userApp.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

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

//============================= profile initiation Angular controller ==========================================
userApp.controller("userController", ['$scope', '$timeout', '$http', 'messageAlertEngine', '$filter', '$rootScope', function ($scope, $timeout, $http, messageAlertEngine, $filter, $rootScope) {

    $scope.Roles = [];
    $scope.Roles = [
        { Code: "ADM",  Name:"Admin" },
        { Code: "CCM", Name: "Credentialing Committee" },
        { Code: "CCO", Name: "Credentialing Coordinator" },
        { Code: "CRA", Name: "Credentialing Admin" },
        { Code: "HR", Name: "Humman Resource" },
        { Code: "MGT", Name: "Management" },        
        { Code: "TL", Name: "Team Lead" },

    ]

    //------------------------------- Country Code Popover Show by Id ---------------------------------------
    $scope.CountryDialCodes = countryDailCodes;

    $scope.showContryCodeDiv = function (countryCodeDivId) {
        changeVisibilityOfCountryCode();
        $("#" + countryCodeDivId).show();
    };

    $scope.hideDiv = function () {
        $(".countryDailCodeContainer").hide();
    }

    //Convert the date from database to normal
    $scope.ConvertDateFormat = function (value) {
       
        var returnValue = value;
        try {
            if (value.indexOf("/Date(") == 0) {
                returnValue = new Date(parseInt(value.replace("/Date(", "").replace(")/", ""), 10));
            }
            return returnValue;
        } catch (e) {
            return returnValue;
        }
        return returnValue;
    };

    //------------------- Form Reset Function ------------------------
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


    function ResetFormForValidation(form) {
        form.removeData('validator');
        form.removeData('unobtrusiveValidation');
        $.validator.unobtrusive.parse(form);

    }

    $scope.clear = function () {
        var telcode = $scope.user.PhoneCountryCode;
        $scope.user = {};
        $scope.user.PhoneCountryCode = telcode;     
    }

    $scope.addUser = function (user) {
        
        var validationStatus;
        var $formData;

        $formData = $('#newUser').find('form');

        ResetFormForValidation($formData);
        validationStatus = $formData.valid();
       
        if (validationStatus)
        {
            if ($.inArray(DateOfBirth, user) != -1) {
                user.DateOfBirth = $filter('date')(new Date(user.DateOfBirth), 'MM/dd/yyyy');
            } 

            $http.post(rootDir + '/Initiation/InitiateUser/Index', user).
                success(function (data, status, headers, config) {
                    try {
                        if (data.status == "true") {
                            $scope.user = {};
                            messageAlertEngine.callAlertMessage("User", "New User Added Successfully !!!!", "success", true);
                        }
                        else {
                            messageAlertEngine.callAlertMessage("UserError", data.status, "danger", true);
                        }
                    } catch (e) {
                      
                    }
                }).
                error(function (data, status, headers, config) {
                    messageAlertEngine.callAlertMessage("UserError", "Sorry Unable To Add User !!!!", "danger", true);
                });
        }
    }

}]);

//================================= Hide All country code popover =========================
$(document).click(function (event) {
    if (!$(event.target).hasClass("countryCodeClass") && $(event.target).parents(".countryDailCodeContainer").length === 0) {
        $(".countryDailCodeContainer").hide();
    }
   
});

//Method to change the visiblity of country code popover
var changeVisibilityOfCountryCode = function () {
    $(".countryDailCodeContainer").hide();
    // method will close any other country code div already open.
};

$(document).ready(function () {
    $(".countryDailCodeContainer").hide();
    $(".ProviderTypeSelectAutoList").hide();

});

