
//============================profile initiation Angular Module ==========================
var delegationApp = angular.module("DelegationApp", []);


//============================Service for sucess/error mesages ============================
delegationApp.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

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
delegationApp.controller("delegationController", ['$scope', '$timeout', '$http', 'messageAlertEngine', '$filter', function ($scope, $timeout, $http, messageAlertEngine, $filter) {

    $scope.ConvertDate = function (date) {
        var returndate = "";
        if (date != null) {
            var newdate = date.split('T')[0].split('-');
            returndate = newdate[1] + "/" + newdate[2] + "/" + newdate[0];
        }
        return returndate;
    }
    $scope.TeamLeads = [];
    if (TeamLeads.length > 0)
        $scope.TeamLeads = angular.copy(TeamLeads);

    $http.get(rootDir + "/Profile/ProfileDelegation/GetAllTeamLeadsAsync").then(function (value) {
        $scope.TeamLeads = angular.copy(value.data);
        for (var i = 0; i < $scope.TeamLeads.length ; i++) {
            if ($scope.TeamLeads[i].Gender == 0)
                $scope.TeamLeads[i].Gender = "Not Available";
            if ($scope.TeamLeads[i] != null && $scope.TeamLeads[i].DateOfBirth != null) {
                $scope.TeamLeads[i].DateOfBirth = $scope.ConvertDate($scope.TeamLeads[i].DateOfBirth);
            }
        }
    });





    //To Display the drop down div
    $scope.searchCumDropDown = function (divId) {
        $("#" + divId).show();
    };

    $scope.setFiles = function (file) {
        $(file).parent().parent().find(".jancyFileWrapTexts").find("span").width($(file).parent().parent().width() < 243 ? $(file).parent().parent().width() : 243);

    }

    //Bind the Certificate name with model class to achieve search cum drop down
    $scope.addIntoTLDropDown = function (TL, div) {
        if (TL.DateOfBirth != null && TL.DateOfBirth != "Not Available") {
            TL.DateOfBirth = $filter('date')(new Date(TL.DateOfBirth), 'MM/dd/yyyy');
        }
        else {
            TL.DateOfBirth = 'Not Available'
        }
        $scope.TL = TL;
        $scope.TL.NameDup = TL.Name;
        $("#" + div).hide();
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

        $scope.lead = {};
        $scope.TL = {};

    }

    $scope.searchCumDropDown = function (divId) {
        $(".ProviderTypeSelectAutoList").hide();
        $("#" + divId).show();
    };

    $scope.addUser = function (profileId, profileUserId) {

        var profileUserId = profileUserId;
        var profileId = profileId;

        var validationStatus = "true";

        if (validationStatus) {
            $http.post(rootDir + '/Profile/ProfileDelegation/AssignProfile?profileId=' + profileId + '&profileUserId=' + profileUserId).
                success(function (data, status, headers, config) {
                    try {
                        if (data.status == "true") {
                            $scope.lead = {};
                            $scope.TL = {};
                            messageAlertEngine.callAlertMessage("TL", "New User Added Successfully !!!!", "success", true);
                        }
                        else {
                            messageAlertEngine.callAlertMessage("TLError", data.status, "danger", true);
                        }
                    } catch (e) {

                    }
                }).
                error(function (data, status, headers, config) {
                    messageAlertEngine.callAlertMessage("TLError", "Sorry Unable To Add User !!!!", "danger", true);
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

$(document).click(function (event) {
    if (!$(event.target).hasClass("form-control") && $(event.target).parents(".ProviderTypeSelectAutoList").length === 0) {
        $(".ProviderTypeSelectAutoList").hide();
    }
});