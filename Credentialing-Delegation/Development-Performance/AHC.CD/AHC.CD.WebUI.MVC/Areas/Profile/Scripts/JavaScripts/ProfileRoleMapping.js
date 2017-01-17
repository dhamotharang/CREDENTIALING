
//============================profile initiation Angular Module ==========================
var profileRoleMappingApp = angular.module("ProfileRoleMappingApp", []);


//============================Service for sucess/error mesages ============================
profileRoleMappingApp.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

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
profileRoleMappingApp.controller("ProfileRoleMappingController", ['$scope', '$timeout', '$http', 'messageAlertEngine', '$filter', function ($scope, $timeout, $http, messageAlertEngine, $filter) {


    //$scope.TeamLeads = [];

    //$http.get(rootDir + "/Profile/ProfileDelegation/GetAllTeamLeadsAsync").then(function (value) {
   
    //    for (var i = 0; i < value.data.length ; i++) {
    //        if (value.data[i] != null) {
    //            value.data[i].DateOfBirth = $scope.ConvertDateFormat(value.data[i].DateOfBirth);
    //        }
    //    }

    //    $scope.TeamLeads = angular.copy(value.data);
    //});
    $scope.profile = JSON.parse(ProfileData).Result;
    $scope.userRoles = JSON.parse(UserRoles).Result;
    $scope.roleId = '4';
    $scope.Roles = [];

    $http.get(rootDir + "/Profile/ProfileRoleMapping/GetAllRolesAsync").then(function (value) {
        $scope.Roles = angular.copy(value.data);
    });

    $scope.addCCMMember = function (profileId) {
        var profileId = profileId;
        var validationStatus = "true";
        if (validationStatus) {
            $http.post(rootDir + '/Profile/ProfileRoleMapping/AddRole?profileID=' + profileId + '&roleID=' + $scope.roleId).
                success(function (data, status, headers, config) {
                    try {
                        if (data.status == "true") {
                            var role = { CDRoleID: $scope.roleId, };
                            $scope.Roles.push();
                            messageAlertEngine.callAlertMessage("TL", "New Role Added To The Profile Successfully !!!!", "success", true);
                        }
                        else {
                            messageAlertEngine.callAlertMessage("TLError", data.status, "danger", true);
                        }
                    } catch (e) {
                        throw e;
                    }
                }).
                error(function (data, status, headers, config) {
                    messageAlertEngine.callAlertMessage("TLError", "Sorry Unable To Add Role !!!!", "danger", true);
                });
        }
    }

    //To Display the drop down div
    $scope.searchCumDropDown = function (divId) {
        $("#" + divId).show();
    };

    //Bind the Certificate name with model class to achieve search cum drop down
    $scope.addIntoTLDropDown = function (TL, div) {
        TL.DateOfBirth = $filter('date')(new Date(TL.DateOfBirth), 'MM/dd/yyyy');
        $scope.TL = TL;
        $scope.TL.NameDup = TL.Name;
        $("#" + div).hide();
    }

    $scope.setFiles = function (file) {
        $(file).parent().parent().find(".jancyFileWrapTexts").find("span").width($(file).parent().parent().width() < 243 ? $(file).parent().parent().width() : 243);

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

    }

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

