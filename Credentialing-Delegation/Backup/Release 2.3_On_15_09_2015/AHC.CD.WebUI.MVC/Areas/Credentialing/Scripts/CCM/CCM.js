var CCMActionApp = angular.module("CCMActionApp", ['mgcrea.ngStrap']);

CCMActionApp.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

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
}])

CCMActionApp.controller("CCMActionController", function ($scope, $window, messageAlertEngine) {
    $scope.LoadCredInfoData = function (data) {
        $scope.temp = data;
        console.log($scope.temp);
        $scope.tempObject = angular.copy($scope.temp.CredentialingLogs[0].CredentialingAppointmentDetail);
        $scope.tempObject.CredentialingAppointmentResult = {};
        $scope.tempObject.CredentialingAppointmentResult.SignedDate = $scope.currentDate();
        $scope.tempObject.CredentialingAppointmentResult.CredentialingLevel = $scope.tempObject.RecommendedCredentialingLevel;
        $scope.tempObject.CredentialingAppointmentResult.SignedByID = $scope.temp.InitiatedByID;
        $scope.tempObject.CredentialingAppointmentResult.SignatureFile = null;
        console.log($scope.tempObject);
        //alert($scope.tempObject.CredentialingAppointmentResult.SignaturePath);
    }

    $scope.$watch('tempObject.RecommendedCredentialingLevel', function () {
        //alert($scope.tempObject.RecommendedCredentialingLevel);
        $scope.tempObject.CredentialingAppointmentResult.CredentialingLevel = $scope.tempObject.RecommendedCredentialingLevel;
        //console.log($scope.tempObject);
    })

    $scope.currentDate = function () {
        var today = new Date();
        var dd = today.getDate();
        var mm = today.getMonth() + 1; //January is 0!

        var yyyy = today.getFullYear();
        if (dd < 10) {
            dd = '0' + dd
        }
        if (mm < 10) {
            mm = '0' + mm
        }
        var today = mm + '/' + dd + '/' + yyyy;
        return today;
    }

    $scope.confirmationAddCCMAction = function () {
        console.log($scope.tempObject.CredentialingAppointmentResult.SignatureFile);
        ResetFormForValidation($("#CCMAction"));
        if ($("#CCMAction").valid()) {
            $scope.ConfirmTitle = 'Confirm Submission ';
            if ($scope.approvalStatusType==1) {
                $scope.ConfirmMessage = 'Do you want to Approve?';
            }
            else {
                $scope.ConfirmMessage = 'Do you want to Reject?';
            }
            $('#myModal').modal({
                backdrop: 'static'
            });
        }
    }
    
    $('#myModal').modal('hide');
    $scope.ccmAction = function (Form_Id) {
        var credAppointmentId=$scope.tempObject.CredentialingAppointmentDetailID;
        ResetFormForValidation($("#" + Form_Id));
        if ($("#" + Form_Id).valid()) {

            var $form = $("#" + Form_Id)[0];
            console.log($form);
            $.ajax({
                url: rootDir + '/Credentialing/CCM/CCMActionUploadAsync?profileId=' + $scope.temp.ProfileID,
                type: 'POST',
                data: new FormData($form),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    $('#myModal').modal('hide');
                    if (data.status == "true") {

                        if ($("#approvestat option:selected").text() == 'Approved') {
                        messageAlertEngine.callAlertMessage('ccmAccepted', 'Appointment is confirmed', "success", true);
                        } else {
                            messageAlertEngine.callAlertMessage('ccmAccepted', 'Appointment is rejected', "danger", true);
                        }

                        //messageAlertEngine.callAlertMessage('ccmAccepted', 'Appointment is confirmed', "success", true);
                        $("body").animate({ scrollTop: $("#errorCCM").offset().top });
                        
                        var delay = 2000; //2 seconds
                        setTimeout(function () {
                            $window.location = '/credentialing/ccm/index';
                        }, delay);
                       
                    }
                }
            });
        }
    }
});
//----------------- Calender hide on select data configuration ------------------
CCMActionApp.config(function ($datepickerProvider) {

    angular.extend($datepickerProvider.defaults, {
        startDate: 'today',
        autoclose: true,
        useNative: true
    });
})
$(document).ready(function () {
    console.log("collapsing menu");
    $("#sidemenu").addClass("menu-in");
    $("#page-wrapper").addClass("menuup");
});
function ResetFormForValidation(form) {
    form.removeData('validator');
    form.removeData('unobtrusiveValidation');
    $.validator.unobtrusive.parse(form);
};
var toggleDiv = function (divId) {
    $('#' + divId).slideToggle();
};