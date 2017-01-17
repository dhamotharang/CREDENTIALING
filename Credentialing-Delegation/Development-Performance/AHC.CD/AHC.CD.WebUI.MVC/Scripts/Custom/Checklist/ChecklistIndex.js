$(document).ready(function () {
    $('#createMSCC').hide();
    $('#createCC').hide();
});

function showMSCCchecklist() {
    $('#detailsMSCC').hide();
    $('#createMSCC').show();
};

function showCCchecklist() {
    $('#detailsCC').hide();
    $('#createCC').show();
};

function hideMSCCchecklist() {
    $('#detailsMSCC').show();
    $('#createMSCC').hide();
};

function hideCCchecklist() {
    $('#detailsCC').show();
    $('#createCC').hide();
};



var psvModule = angular.module('primarySourceApp', ['mgcrea.ngStrap']);

psvModule.config(function ($datepickerProvider) {

    angular.extend($datepickerProvider.defaults, {
        startDate: 'today',
        autoclose: true,
        useNative: true
    });
});
psvModule.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

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

psvModule.directive('ngFocus', ['$parse', function ($parse) {
    return function (scope, element, attr) {
        var fn = $parse(attr['ngFocus']);
        element.bind('focus', function (event) {
            scope.$apply(function () {
                fn(scope, { $event: event });
            });
        });
    }
}]);

psvModule.directive('ngBlur', ['$parse', function ($parse) {
    return function (scope, element, attr) {
        var fn = $parse(attr['ngBlur']);
        element.bind('blur', function (event) {
            scope.$apply(function () {
                fn(scope, { $event: event });
            });
        });
    }
}]);
psvModule.controller('profileAppCtrl', function ($scope, $http, $filter, messageAlertEngine) {

    function ResetFormForValidation(form) {
        form.removeData('validator');
        form.removeData('unobtrusiveValidation');
        $.validator.unobtrusive.parse(form);
    };

    $(function () {
        $('.progress').hide();
        $('.file').hide();

        annimateelem = function (val) {
            $('.prog' + val).parent().show(1)
            $('.prog' + val).animate({
                width: "100%"
            }, 3000);
            setTimeout(function(){
                $('.prog' + val).parent().hide(100, function () {                    
                    $('.verifybtn' + val).effect('explode',1000);
                    $('.file' + val).show(1000);
                    $('.successcount').text(val);
                });
            },4000)
            
        };

        verifyall = function () {
            $('.progress').hide();
            $('.file').hide();            
            $('.progress-bar').css('width', '0px');
            

            (function next(counter, maxLoops) {
                if (counter == 0) {
                    time = 1;
                }
                else { time = 5000; }

                if (counter == 7) {
                    $('#verfyall').effect('explode');
                }
                // break if maxLoops has been reached
                if (counter++ >= maxLoops) return;

                setTimeout(function () {
                    annimateelem(counter);
                    $('.verfcount').text(counter);                    
                    // call next() recursively
                    next(counter, maxLoops);
                }, time);
            })(0, 7);
        };
    });


    $scope.todayDate = $filter('date')(new Date(), 'MM/dd/yyyy');
    $scope.ConvertDateFormat1 = function (value) {
        var shortDate = null;
        if (value) {
            var regex = /-?\d+/;
            var matches = regex.exec(value);
            var dt = new Date(parseInt(matches[0]));
            var month = dt.getMonth() + 1;
            var monthString = month > 9 ? month : '0' + month;
            var monthName = monthNames[month];
            var day = dt.getDate();
            var dayString = day > 9 ? day : '0' + day;
            var year = dt.getFullYear();
            shortDate = monthString + '/' + dayString + '/' + year;
            //shortDate = dayString + 'th ' + monthName + ',' + year;
        }
        return shortDate;
    };

    //==============================================================
    $scope.LicenseList = [];
    $scope.ProfileVerificationDetailViewModel = [];
    $scope.enableVerified = true;
    $scope.ProfileObj = null;

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

    //============================= psv history===========================
    $scope.StateLicenseHistory = [];
    $scope.BoardCertificationHistory = [];
    $scope.DEAHistory = [];
    $scope.CDSHistory = [];
    $scope.NPDBHistory = [];
    $scope.MedicareOPTHistory = [];
    $scope.OIGHistory = [];

  
    //function myfun() {
    //    $('#persmsoigInfo').toggle();
    //}
    


    $scope.closeDiv = function (id, className) {

        $('.' + className).hide();
        $('#' + id).show();
    };

    $scope.closeDivOnCancel = function (className) {
        $('.' + className).hide();
    };
    //============================= psv history===========================


    //---------------- set data value -------------
    $scope.ShowDivDropDOwn = function (divId) {
        $("#" + divId).show();
    };

    //---------------- set data value -------------
    $scope.setValue = function (arg, value) {

        arg.Source = value;


        $(".ProviderTypeSelectAutoList1").hide();
    };

    $scope.checkForHttp = function (value) {
        if (value.indexOf('http') > -1) {
            value = value;
        } else {
            value = 'http://' + value;
        }
        var open_link = window.open('', '_blank');
        open_link.location = value;
    }
    //======================state License===================
    $scope.stateLicenses = [];
    $scope.AddStateLicenseProfileVerificationDetail = function (index, lisence) {

        var $formData;
        $formData = $('#StateLicenseForm' + index);
        ResetFormForValidation($formData);
        validationStatus = $formData.valid();
        if (validationStatus == true) {
            $.ajax({
                url: rootDir + '/Credentialing/Verification/AddPSVDetail?credVerificationInfoId=' + $scope.verificationId + '&profileId=' + $scope.ProfileObj.ProfileID,
                type: 'POST',
                data: new FormData($formData[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    try {
                        if (data.status == "true") {
                            lisence.AddDiv = false;
                            lisence.ViewDiv = true;

                            data.verificationDetail.VerificationDate = $scope.ConvertDateFormat(data.verificationDetail.VerificationDate);
                            //$scope.verifiedDate = data.verificationDetail;
                            $scope.stateLicenses[index].ProfileVerificationDetail = data.verificationDetail;
                            $scope.copystateLicenses[index] = angular.copy($scope.stateLicenses[index]);
                            $scope.enableVerified = false;
                            messageAlertEngine.callAlertMessage("statePSV" + index, "State License information verified Successfully !!!!", "success", true);
                        }
                    } catch (e) {
                       
                    }

                },
                error: function (e) {

                    messageAlertEngine.callAlertMessage("statePSVError" + index, "Unable to save verified data !!!!", "danger", true);
                }
            });
        }

    };
    $scope.UpdateStateLicenseProfileVerificationDetail = function (index, lisence) {

        var $formdata;
        $formData = $('#StateLicenseForm' + index);
        var obj = $scope.copystateLicenses[index];

        if (obj.ProfileVerificationDetail.VerificationDate != '') {
            $scope.StateLicenseHistory.push({
                ProfileVerificationDetailId: obj.ProfileVerificationDetail.ProfileVerificationDetailId,
                ProfileVerificationParameterId: obj.ProfileVerificationDetail.ProfileVerificationParameterId,
                ProfileVerificationParameter: obj.ProfileVerificationDetail.ProfileVerificationParameter,
                VerificationResultId: obj.ProfileVerificationDetail.VerificationResultId,
                VerificationResult: obj.ProfileVerificationDetail.VerificationResult,
                VerificationData: jQuery.parseJSON(obj.ProfileVerificationDetail.VerificationData),
                Status: '',
                VerifiedById: '',
                VerificationDate: obj.ProfileVerificationDetail.VerificationDate,
                LastModifiedDate: obj.ProfileVerificationDetail.LastModifiedDate
            });
        }

        ResetFormForValidation($formData);
        validationStatus = $formData.valid();
        if (validationStatus == true) {
            $.ajax({
                url: rootDir + '/Credentialing/Verification/UpdatePSVDetail?credVerificationInfoId=' + $scope.verificationId + '&profileId=' + $scope.ProfileObj.ProfileID,
                type: 'POST',
                data: new FormData($formData[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    try {
                        if (data.status == "true") {
                            lisence.EditDiv = false;
                            lisence.ViewDiv = true;

                            data.verificationDetail.VerificationDate = $scope.ConvertDateFormat(data.verificationDetail.VerificationDate);
                            $scope.stateLicenses[index].ProfileVerificationDetail = data.verificationDetail;
                            $scope.enableVerified = false;
                            $scope.copystateLicenses[index] = angular.copy($scope.stateLicenses[index]);
                            messageAlertEngine.callAlertMessage("statePSV" + index, "State License information verified Successfully !!!!", "success", true);
                        }
                    } catch (e) {
                       
                    }

                },
                error: function (e) {

                    messageAlertEngine.callAlertMessage("statePSVError" + index, "Unable to update verified data !!!!", "danger", true);
                }
            });
        }

    };
    $scope.cancelState = function (index) {

        $scope.copystateLicenses[index].EditDiv = false;
        $scope.copystateLicenses[index].ViewDiv = true;
        $scope.copystateLicenses[index].AddDiv = false;
        $scope.stateLicenses[index] = angular.copy($scope.copystateLicenses[index]);
    }

    $scope.editStateLicense = function (index) {
        $scope.copystateLicenses[index].EditDiv = true;
        $scope.copystateLicenses[index].ViewDiv = false;
    }

    //=======================Board Certification==================

    $scope.BoradCertifications = [];
    $scope.AddBoardCertificationProfileVerificationDetail = function (index, lisence) {


        var $formdata;

        $formData = $('#BoardCertificationForm' + index);
        ResetFormForValidation($formData);
        validationStatus = $formData.valid();
        if (validationStatus == true) {
            $.ajax({
                url: rootDir + '/Credentialing/Verification/AddPSVDetail?credVerificationInfoId=' + $scope.verificationId + '&profileId=' + $scope.ProfileObj.ProfileID,
                type: 'POST',
                data: new FormData($formData[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    try {
                        if (data.status == "true") {
                            lisence.AddDiv = false;
                            lisence.ViewDiv = true;
                            data.verificationDetail.VerificationDate = $scope.ConvertDateFormat(data.verificationDetail.VerificationDate);
                            //$scope.verifiedDate = data.verificationDetail;

                            $scope.BoradCertifications[index].ProfileVerificationDetail = data.verificationDetail;
                            $scope.copyBoradCertifications[index] = angular.copy($scope.BoradCertifications[index]);
                            $scope.enableVerified = false;
                            messageAlertEngine.callAlertMessage("boardPSV" + index, "Board Certifications information verified Successfully !!!!", "success", true);
                        }
                    } catch (e) {
                       
                    }

                },
                error: function (e) {
                    messageAlertEngine.callAlertMessage("boardPSVError" + index, "Unable to save verified data !!!!", "danger", true);
                }
            });
        }
    }
    $scope.UpdateBoardCertificationProfileVerificationDetail = function (index, lisence) {

        var obj = $scope.copyBoradCertifications[index];
        if (obj.ProfileVerificationDetail.VerificationDate != '') {

            $scope.BoardCertificationHistory.push({
                ProfileVerificationDetailId: obj.ProfileVerificationDetail.ProfileVerificationDetailId,
                ProfileVerificationParameterId: obj.ProfileVerificationDetail.ProfileVerificationParameterId,
                ProfileVerificationParameter: obj.ProfileVerificationDetail.ProfileVerificationParameter,
                VerificationResultId: obj.ProfileVerificationDetail.VerificationResultId,
                VerificationResult: obj.ProfileVerificationDetail.VerificationResult,
                VerificationData: jQuery.parseJSON(obj.ProfileVerificationDetail.VerificationData),
                Status: '',
                VerifiedById: '',
                VerificationDate: obj.ProfileVerificationDetail.VerificationDate,
                LastModifiedDate: obj.ProfileVerificationDetail.LastModifiedDate
            });
        }
        var $formdata;

        $formData = $('#BoardCertificationForm' + index);

        ResetFormForValidation($formData);
        validationStatus = $formData.valid();
        if (validationStatus == true) {
            $.ajax({
                url: rootDir + '/Credentialing/Verification/UpdatePSVDetail?credVerificationInfoId=' + $scope.verificationId + '&profileId=' + $scope.ProfileObj.ProfileID,
                type: 'POST',
                data: new FormData($formData[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    if (data.status == "true") {
                        lisence.EditDiv = false;
                        lisence.ViewDiv = true;
                        data.verificationDetail.VerificationDate = $scope.ConvertDateFormat(data.verificationDetail.VerificationDate);
                        //$scope.verifiedDate = data.verificationDetail;

                        $scope.BoradCertifications[index].ProfileVerificationDetail = data.verificationDetail;
                        $scope.copyBoradCertifications[index] = angular.copy($scope.BoradCertifications[index]);
                        $scope.enableVerified = false;
                        messageAlertEngine.callAlertMessage("boardPSV" + index, "Board Certifications information verified Successfully !!!!", "success", true);
                    }

                },
                error: function (e) {
                    messageAlertEngine.callAlertMessage("boardPSVError" + index, "Unable to update verified data !!!!", "danger", true);
                }
            });
        }
    }
    $scope.cancelBoard = function (index) {

        $scope.copyBoradCertifications[index].EditDiv = false;
        $scope.copyBoradCertifications[index].ViewDiv = true;
        $scope.copyBoradCertifications[index].AddDiv = false;
        $scope.BoradCertifications[index] = angular.copy($scope.copyBoradCertifications[index]);
    }

    $scope.editBoardCertificate = function (index) {
        $scope.copyBoradCertifications[index].EditDiv = true;
        $scope.copyBoradCertifications[index].ViewDiv = false;
    }

    //========================CDS================================
    $scope.CDSs = [];
    $scope.AddCDSProfileVerificationDetail = function (index, lisence) {

        var $formdata;

        $formData = $('#CDSForm' + index);
        ResetFormForValidation($formData);
        validationStatus = $formData.valid();
        if (validationStatus == true) {
            $.ajax({
                url: rootDir + '/Credentialing/Verification/AddPSVDetail?credVerificationInfoId=' + $scope.verificationId + '&profileId=' + $scope.ProfileObj.ProfileID,
                type: 'POST',
                data: new FormData($formData[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    try {
                        if (data.status == "true") {
                            lisence.AddDiv = false;
                            lisence.ViewDiv = true;
                            data.verificationDetail.VerificationDate = $scope.ConvertDateFormat(data.verificationDetail.VerificationDate);
                            //$scope.verifiedDate = data.verificationDetail;
                            $scope.enableVerified = false;
                            $scope.CDSs[index].ProfileVerificationDetail = data.verificationDetail;
                            $scope.copyCDSs[index] = angular.copy($scope.CDSs[index]);
                            messageAlertEngine.callAlertMessage("cdsPSV" + index, "CDS information verified Successfully !!!!", "success", true);
                        }
                    } catch (e) {
                       
                    }

                },
                error: function (e) {
                    messageAlertEngine.callAlertMessage("cdsPSVError" + index, "Unable to save verified data !!!!", "danger", true);
                }
            });
        }
    }
    $scope.UpdateCDSProfileVerificationDetail = function (index, lisence) {

        var obj = $scope.copyCDSs[index];
        if (obj.ProfileVerificationDetail.VerificationDate != '') {

            $scope.CDSHistory.push({
                ProfileVerificationDetailId: obj.ProfileVerificationDetail.ProfileVerificationDetailId,
                ProfileVerificationParameterId: obj.ProfileVerificationDetail.ProfileVerificationParameterId,
                ProfileVerificationParameter: obj.ProfileVerificationDetail.ProfileVerificationParameter,
                VerificationResultId: obj.ProfileVerificationDetail.VerificationResultId,
                VerificationResult: obj.ProfileVerificationDetail.VerificationResult,
                VerificationData: jQuery.parseJSON(obj.ProfileVerificationDetail.VerificationData),
                Status: '',
                VerifiedById: '',
                VerificationDate: obj.ProfileVerificationDetail.VerificationDate,
                LastModifiedDate: obj.ProfileVerificationDetail.LastModifiedDate
            });
        }
        var $formdata;
        $formData = $('#CDSForm' + index);
        ResetFormForValidation($formData);
        validationStatus = $formData.valid();
        if (validationStatus == true) {
            $.ajax({
                url: rootDir + '/Credentialing/Verification/UpdatePSVDetail?credVerificationInfoId=' + $scope.verificationId + '&profileId=' + $scope.ProfileObj.ProfileID,
                type: 'POST',
                data: new FormData($formData[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    try {
                        if (data.status == "true") {
                            lisence.EditDiv = false;
                            lisence.ViewDiv = true;
                            data.verificationDetail.VerificationDate = $scope.ConvertDateFormat(data.verificationDetail.VerificationDate);
                            //$scope.verifiedDate = data.verificationDetail;
                            $scope.enableVerified = false;
                            $scope.CDSs[index].ProfileVerificationDetail = data.verificationDetail;
                            $scope.copyCDSs[index] = angular.copy($scope.CDSs[index]);
                            messageAlertEngine.callAlertMessage("cdsPSV" + index, "CDS information verified Successfully !!!!", "success", true);
                        }
                    } catch (e) {
                      
                    }

                },
                error: function (e) {
                    messageAlertEngine.callAlertMessage("cdsPSVError" + index, "Unable to update verified data !!!!", "danger", true);
                }
            });
        }
    }
    $scope.cancelCDS = function (index) {

        $scope.copyCDSs[index].EditDiv = false;
        $scope.copyCDSs[index].ViewDiv = true;
        $scope.copyCDSs[index].AddDiv = false;

        $scope.CDSs[index] = angular.copy($scope.copyCDSs[index]);
    }
    $scope.editCDS = function (index) {
        $scope.copyCDSs[index].EditDiv = true;
        $scope.copyCDSs[index].ViewDiv = false;
    }

    //=======================DEA=================================
    $scope.DEAs = [];
    $scope.AddDEAProfileVerificationDetail = function (index, lisence) {

        var $formdata;

        $formData = $('#DEAForm' + index);
        ResetFormForValidation($formData);
        validationStatus = $formData.valid();
        if (validationStatus == true) {
            $.ajax({
                url: rootDir + '/Credentialing/Verification/AddPSVDetail?credVerificationInfoId=' + $scope.verificationId + '&profileId=' + $scope.ProfileObj.ProfileID,
                type: 'POST',
                data: new FormData($formData[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    try {
                        if (data.status == "true") {
                            lisence.AddDiv = false;
                            lisence.ViewDiv = true;
                            data.verificationDetail.VerificationDate = $scope.ConvertDateFormat(data.verificationDetail.VerificationDate);
                            //$scope.verifiedDate = data.verificationDetail;
                            $scope.enableVerified = false;
                            $scope.DEAs[index].ProfileVerificationDetail = data.verificationDetail;
                            $scope.copyDEAs[index] = angular.copy($scope.DEAs[index]);
                            messageAlertEngine.callAlertMessage("deaPSV" + index, "DEA information verified Successfully !!!!", "success", true);
                        }
                    } catch (e) {
                       
                    }

                },
                error: function (e) {
                    messageAlertEngine.callAlertMessage("deaPSVError" + index, "Unable to save verified data !!!!", "danger", true);
                }
            });
        }
    }
    $scope.UpdateDEAProfileVerificationDetail = function (index, lisence) {
        var obj = $scope.copyDEAs[index];
        if (obj.ProfileVerificationDetail.VerificationDate != '') {

            $scope.DEAHistory.push({
                ProfileVerificationDetailId: obj.ProfileVerificationDetail.ProfileVerificationDetailId,
                ProfileVerificationParameterId: obj.ProfileVerificationDetail.ProfileVerificationParameterId,
                ProfileVerificationParameter: obj.ProfileVerificationDetail.ProfileVerificationParameter,
                VerificationResultId: obj.ProfileVerificationDetail.VerificationResultId,
                VerificationResult: obj.ProfileVerificationDetail.VerificationResult,
                VerificationData: jQuery.parseJSON(obj.ProfileVerificationDetail.VerificationData),
                Status: '',
                VerifiedById: '',
                VerificationDate: obj.ProfileVerificationDetail.VerificationDate,
                LastModifiedDate: obj.ProfileVerificationDetail.LastModifiedDate
            });
        }
        var $formdata;

        $formData = $('#DEAForm' + index);
        ResetFormForValidation($formData);
        validationStatus = $formData.valid();
        if (validationStatus == true) {
            $.ajax({
                url: rootDir + '/Credentialing/Verification/UpdatePSVDetail?credVerificationInfoId=' + $scope.verificationId + '&profileId=' + $scope.ProfileObj.ProfileID,
                type: 'POST',
                data: new FormData($formData[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    try {
                        if (data.status == "true") {
                            lisence.EditDiv = false;
                            lisence.ViewDiv = true;
                            data.verificationDetail.VerificationDate = $scope.ConvertDateFormat(data.verificationDetail.VerificationDate);
                            //$scope.verifiedDate = data.verificationDetail;
                            $scope.enableVerified = false;
                            $scope.DEAs[index].ProfileVerificationDetail = data.verificationDetail;
                            $scope.copyDEAs[index] = angular.copy($scope.DEAs[index]);
                            messageAlertEngine.callAlertMessage("deaPSV" + index, "DEA information verified Successfully !!!!", "success", true);
                        }
                    } catch (e) {
                      
                    }

                },
                error: function (e) {
                    messageAlertEngine.callAlertMessage("deaPSVError" + index, "Unable to update verified data !!!!", "danger", true);
                }
            });
        }
    }
    $scope.cancelDEA = function (index) {

        $scope.copyDEAs[index].EditDiv = false;
        $scope.copyDEAs[index].ViewDiv = true;
        $scope.copyDEAs[index].AddDiv = false;

        $scope.DEAs[index] = angular.copy($scope.copyDEAs[index]);
    }

    $scope.editDEA = function (index) {
        $scope.copyDEAs[index].EditDiv = true;
        $scope.copyDEAs[index].ViewDiv = false;
    }
    //======================OIG==============================

    $scope.AddOIGProfileVerificationDetail = function (lisence) {

        var $formdata;

        $formData = $('#OIGForm');
        ResetFormForValidation($formData);
        validationStatus = $formData.valid();
        if (validationStatus == true) {
            $.ajax({
                url: rootDir + '/Credentialing/Verification/AddPSVDetail?credVerificationInfoId=' + $scope.verificationId + '&profileId=' + $scope.ProfileObj.ProfileID,
                type: 'POST',
                data: new FormData($formData[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    try {
                        if (data.status == "true") {
                            lisence.AddDiv = false;
                            lisence.ViewDiv = true;
                            data.verificationDetail.VerificationDate = $scope.ConvertDateFormat(data.verificationDetail.VerificationDate);
                            //$scope.verifiedDate = data.verificationDetail;
                            $scope.enableVerified = false;
                            $scope.OIG.ProfileVerificationDetail = data.verificationDetail;
                            $scope.copyOIG = angular.copy($scope.OIG);
                            messageAlertEngine.callAlertMessage("oigPSV", "OIG Information verified Successfully !!!!", "success", true);
                        }

                    } catch (e) {
                     
                    }

                },
                error: function (e) {
                    messageAlertEngine.callAlertMessage("oigPSVError", "Unable to save verified data !!!!", "danger", true);
                }
            });
        }
    };
    $scope.UpdateOIGProfileVerificationDetail = function (lisence) {

        var obj = $scope.copyOIG;
        
        if (obj.ProfileVerificationDetail.VerificationDate != '') {
            $scope.OIGHistory.push({
                ProfileVerificationDetailId: obj.ProfileVerificationDetail.ProfileVerificationDetailId,
                ProfileVerificationParameterId: obj.ProfileVerificationDetail.ProfileVerificationParameterId,
                ProfileVerificationParameter: obj.ProfileVerificationDetail.ProfileVerificationParameter,
                VerificationResultId: obj.ProfileVerificationDetail.VerificationResultId,
                VerificationResult: obj.ProfileVerificationDetail.VerificationResult,
                VerificationData: jQuery.parseJSON(obj.ProfileVerificationDetail.VerificationData),
                Status: '',
                VerifiedById: '',
                VerificationDate: obj.ProfileVerificationDetail.VerificationDate,
                LastModifiedDate: obj.ProfileVerificationDetail.LastModifiedDate
            });
        }
        var $formdata;

        $formData = $('#OIGForm');
        ResetFormForValidation($formData);
        validationStatus = $formData.valid();
        if (validationStatus == true) {
            $.ajax({
                url: rootDir + '/Credentialing/Verification/UpdatePSVDetail?credVerificationInfoId=' + $scope.verificationId + '&profileId=' + $scope.ProfileObj.ProfileID,
                type: 'POST',
                data: new FormData($formData[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    try {
                        if (data.status == "true") {
                            lisence.EditDiv = false;
                            lisence.ViewDiv = true;
                            data.verificationDetail.VerificationDate = $scope.ConvertDateFormat(data.verificationDetail.VerificationDate);
                            //$scope.verifiedDate = data.verificationDetail;
                            $scope.enableVerified = false;
                            $scope.OIG.ProfileVerificationDetail = data.verificationDetail;
                            $scope.copyOIG = angular.copy($scope.OIG);
                            messageAlertEngine.callAlertMessage("oigPSV", "OIG Information verified Successfully !!!!", "success", true);
                        }
                    } catch (e) {
                       
                    }


                },
                error: function (e) {
                    messageAlertEngine.callAlertMessage("oigPSVError", "Unable to update verified data !!!!", "danger", true);
                }
            });
        }
    };
    $scope.cancelOIG = function () {
        $scope.OIG = angular.copy($scope.copyOIG);
    }

    //======================NPDB==============================
    $scope.AddNPDBProfileVerificationDetail = function (lisence) {

        var $formdata;

        $formData = $('#NPDBForm');
        ResetFormForValidation($formData);
        validationStatus = $formData.valid();
        if (validationStatus == true) {
            $.ajax({
                url: rootDir + '/Credentialing/Verification/AddPSVDetail?credVerificationInfoId=' + $scope.verificationId + '&profileId=' + $scope.ProfileObj.ProfileID,
                type: 'POST',
                data: new FormData($formData[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    try {
                        if (data.status == "true") {
                            lisence.AddDiv = false;
                            lisence.ViewDiv = true;
                            data.verificationDetail.VerificationDate = $scope.ConvertDateFormat(data.verificationDetail.VerificationDate);
                            //$scope.verifiedDate = data.verificationDetail;
                            $scope.enableVerified = false;
                            $scope.NPDB.ProfileVerificationDetail = data.verificationDetail;
                            $scope.copyNPDB = angular.copy($scope.NPDB);
                            messageAlertEngine.callAlertMessage("npdbPSV", "NPDB Information verified Successfully !!!!", "success", true);
                        }
                    } catch (e) {
                      
                    }


                },
                error: function (e) {
                    messageAlertEngine.callAlertMessage("npdbPSVError", "Unable to save verified data !!!!", "danger", true);
                }
            });
        }
    };
    $scope.UpdateNPDBProfileVerificationDetail = function (lisence) {

        var obj = $scope.copyNPDB;
        if (obj.ProfileVerificationDetail.VerificationDate != '') {

            $scope.NPDBHistory.push({
                ProfileVerificationDetailId: obj.ProfileVerificationDetail.ProfileVerificationDetailId,
                ProfileVerificationParameterId: obj.ProfileVerificationDetail.ProfileVerificationParameterId,
                ProfileVerificationParameter: obj.ProfileVerificationDetail.ProfileVerificationParameter,
                VerificationResultId: obj.ProfileVerificationDetail.VerificationResultId,
                VerificationResult: obj.ProfileVerificationDetail.VerificationResult,
                VerificationData: jQuery.parseJSON(obj.ProfileVerificationDetail.VerificationData),
                Status: '',
                VerifiedById: '',
                VerificationDate: obj.ProfileVerificationDetail.VerificationDate,
                LastModifiedDate: obj.ProfileVerificationDetail.LastModifiedDate
            });
        }

        //alert();
        var $formdata = new FormData();

        $formData = $('#NPDBForm');
        ResetFormForValidation($formData);
        validationStatus = $formData.valid();
        if (validationStatus == true) {
            $.ajax({
                url: rootDir + '/Credentialing/Verification/UpdatePSVDetail?credVerificationInfoId=' + $scope.verificationId + '&profileId=' + $scope.ProfileObj.ProfileID,
                type: 'POST',
                data: new FormData($formData[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    try {
                        if (data.status == "true") {
                            lisence.EditDiv = false;
                            lisence.ViewDiv = true;
                            data.verificationDetail.VerificationDate = $scope.ConvertDateFormat(data.verificationDetail.VerificationDate);
                            //$scope.verifiedDate = data.verificationDetail;
                            $scope.enableVerified = false;
                            $scope.NPDB.ProfileVerificationDetail = data.verificationDetail;
                            $scope.copyNPDB = angular.copy($scope.NPDB);
                            messageAlertEngine.callAlertMessage("npdbPSV", "NPDB Information verified Successfully !!!!", "success", true);
                        }
                    } catch (e) {
                       
                    }


                },
                error: function (e) {
                    messageAlertEngine.callAlertMessage("npdbPSVError", "Unable to update verified data !!!!", "danger", true);
                }
            });
        }
    };
    $scope.cancelNPDB = function () {
        $scope.NPDB = angular.copy($scope.copyNPDB);
    }

    //======================Medicare OPT==============================
    $scope.AddMedicareOPTProfileVerificationDetail = function (lisence) {

        var $formdata;

        $formData = $('#medicareOPT');
        ResetFormForValidation($formData);
        validationStatus = $formData.valid();
        if (validationStatus == true) {
            $.ajax({
                url: rootDir + '/Credentialing/Verification/AddPSVDetail?credVerificationInfoId=' + $scope.verificationId + '&profileId=' + $scope.ProfileObj.ProfileID,
                type: 'POST',
                data: new FormData($formData[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    try {
                        if (data.status == "true") {
                            lisence.AddDiv = false;
                            lisence.ViewDiv = true;
                            data.verificationDetail.VerificationDate = $scope.ConvertDateFormat(data.verificationDetail.VerificationDate);
                            //$scope.verifiedDate = data.verificationDetail;
                            $scope.enableVerified = false;
                            $scope.MedicareOPT.ProfileVerificationDetail = data.verificationDetail;
                            $scope.copyMedicareOPT = angular.copy($scope.MedicareOPT);
                            messageAlertEngine.callAlertMessage("optPSV", "Medicare Opt OUT Information verified Successfully !!!!", "success", true);
                        }
                    } catch (e) {
                       
                    }


                },
                error: function (e) {
                    messageAlertEngine.callAlertMessage("optPSVError", "Unable to save verified data !!!!", "danger", true);
                }
            });
        }
    };
    $scope.UpdateMedicareOPTProfileVerificationDetail = function (lisence) {

        var obj = $scope.copyMedicareOPT;
        if (obj.ProfileVerificationDetail.VerificationDate != '') {
            $scope.MedicareOPTHistory.push({
                ProfileVerificationDetailId: obj.ProfileVerificationDetail.ProfileVerificationDetailId,
                ProfileVerificationParameterId: obj.ProfileVerificationDetail.ProfileVerificationParameterId,
                ProfileVerificationParameter: obj.ProfileVerificationDetail.ProfileVerificationParameter,
                VerificationResultId: obj.ProfileVerificationDetail.VerificationResultId,
                VerificationResult: obj.ProfileVerificationDetail.VerificationResult,
                VerificationData: jQuery.parseJSON(obj.ProfileVerificationDetail.VerificationData),
                Status: '',
                VerifiedById: '',
                VerificationDate: obj.ProfileVerificationDetail.VerificationDate,
                LastModifiedDate: obj.ProfileVerificationDetail.LastModifiedDate
            });
        }

        var $formdata;

        $formData = $('#medicareOPT');
        ResetFormForValidation($formData);
        validationStatus = $formData.valid();
        if (validationStatus == true) {
            $.ajax({
                url: rootDir + '/Credentialing/Verification/UpdatePSVDetail?credVerificationInfoId=' + $scope.verificationId + '&profileId=' + $scope.ProfileObj.ProfileID,
                type: 'POST',
                data: new FormData($formData[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    try {
                        if (data.status == "true") {
                            lisence.EditDiv = false;
                            lisence.ViewDiv = true;
                            data.verificationDetail.VerificationDate = $scope.ConvertDateFormat(data.verificationDetail.VerificationDate);
                            //$scope.verifiedDate = data.verificationDetail;
                            $scope.enableVerified = false;
                            $scope.MedicareOPT.ProfileVerificationDetail = data.verificationDetail;
                            $scope.copyMedicareOPT = angular.copy($scope.MedicareOPT);
                            messageAlertEngine.callAlertMessage("optPSV", "Medicare Opt OUT Information verified Successfully !!!!", "success", true);
                        }
                    } catch (e) {
                     
                    }

                },
                error: function (e) {
                    messageAlertEngine.callAlertMessage("optPSVError", "Unable to update verified data !!!!", "danger", true);
                }
            });
        }
    };
    $scope.cancelMedicare = function () {
        $scope.MedicareOPT = angular.copy($scope.copyMedicareOPT);
    }


    $scope.VerificationDetailsId = [];


    $scope.setAllVerified = function (credentialingInfoId, verificationId) {

        for (var i = 0; i < $scope.stateLicenses.length; i++) {
            if ($scope.stateLicenses[i].ProfileVerificationDetail.ProfileVerificationDetailId)
                $scope.VerificationDetailsId.push($scope.stateLicenses[i].ProfileVerificationDetail.ProfileVerificationDetailId);
        }

        for (var i = 0; i < $scope.BoradCertifications.length; i++) {
            if ($scope.BoradCertifications[i].ProfileVerificationDetail.ProfileVerificationDetailId)
                $scope.VerificationDetailsId.push($scope.BoradCertifications[i].ProfileVerificationDetail.ProfileVerificationDetailId);
        }

        for (var i = 0; i < $scope.CDSs.length; i++) {
            if ($scope.CDSs[i].ProfileVerificationDetail.ProfileVerificationDetailId)
                $scope.VerificationDetailsId.push($scope.CDSs[i].ProfileVerificationDetail.ProfileVerificationDetailId);
        }

        for (var i = 0; i < $scope.DEAs.length; i++) {
            if ($scope.DEAs[i].ProfileVerificationDetail.ProfileVerificationDetailId)
                $scope.VerificationDetailsId.push($scope.DEAs[i].ProfileVerificationDetail.ProfileVerificationDetailId);
        }

        if ($scope.OIG.ProfileVerificationDetail != null)
            $scope.VerificationDetailsId.push($scope.OIG.ProfileVerificationDetail.ProfileVerificationDetailId);

        if ($scope.NPDB.ProfileVerificationDetail != null)
            $scope.VerificationDetailsId.push($scope.NPDB.ProfileVerificationDetail.ProfileVerificationDetailId);

        if ($scope.MedicareOPT.ProfileVerificationDetail != null)
            $scope.VerificationDetailsId.push($scope.MedicareOPT.ProfileVerificationDetail.ProfileVerificationDetailId);



        $http.post(rootDir + '/Credentialing/Verification/SetAllVerified?credinfoId=' + credentialingInfoId + '&credVerificationId=' + verificationId, $scope.VerificationDetailsId).
        success(function (data, status, headers, config) {
            try {
                if (data.status == "true") {

                    sessionStorage.setItem('credentialingInfoId', credentialingInfoId);
                    sessionStorage.setItem('ButtonCLickName', "2");
                    window.location.assign(rootDir + '/Credentialing/CnD/Application?id=' + credentialingInfoId);
                }
            } catch (e) {
               
            }
        }).
         error(function (data, status, headers, config) {

         });
    }

    $scope.LoadVerification = function (id) {
        $scope.verificationId = id;
    }

    $scope.LoadCredInfo = function (id) {
        $scope.credentialingInfoId = id;
    }

    $scope.Load = function (planPsvdata) {
        if (planPsvdata != null) {

            $scope.ProfileObj = angular.copy(planPsvdata);
            $scope.ProfileData = angular.copy(planPsvdata.Profile);
            $scope.ProfileId = angular.copy(planPsvdata.ProfileID);
            $scope.getPSVHistory($scope.ProfileId);
            $scope.PlanPsv = true;
        }



        // $scope.GetProfileVerificationParameter(); 

    };

    $scope.LoadProfile = function (profilePsvData) {
        if (profilePsvData != null) {

            $scope.ProfileObj = angular.copy(profilePsvData);
            $scope.ProfileData = angular.copy(profilePsvData);
            $scope.ProfileId = angular.copy(profilePsvData.ProfileID);
            $scope.getPSVHistory($scope.ProfileId);
            $scope.PlanPsv = false;
        }

        // $scope.GetProfileVerificationParameter(); 

    };

    $scope.loadObjFormat = function (data) {


        //=============StateLicenses==================================

        for (var i = 0; i < data.StateLicenses.length; i++) {


            var VerificationData = JSON.stringify(data.StateLicenses[i]);
            //var VerificationData = null;
            if ($scope.pendingPSV == null) {
                $scope.stateLicenses.push({ ViewDiv: false, EditDiv: false, AddDiv: true, LincensesInfo: data.StateLicenses[i], ProfileVerificationDetail: { ProfileVerificationDetailId: '', ProfileVerificationParameterId: '', VerificationResult: { VerificationResultId: '', VerificationResultStatusType: '', Source: '', Remark: '', VerificationResultDocumentPath: '', VerificationResultDocument: '' }, VerificationData: VerificationData, VerifiedById: '', VerificationDate: '' } });

            }
            else {
                $scope.stateLicenses.push({ ViewDiv: true, EditDiv: false, AddDiv: false, LincensesInfo: data.StateLicenses[i], ProfileVerificationDetail: { ProfileVerificationDetailId: '', ProfileVerificationParameterId: '', VerificationResult: { VerificationResultId: '', VerificationResultStatusType: '', Source: '', Remark: '', VerificationResultDocumentPath: '', VerificationResultDocument: '' }, VerificationData: VerificationData, VerifiedById: '', VerificationDate: '' } });

            }
        }

        if ($scope.StateLicensesParameter.length > 0) {
            for (var j in $scope.StateLicensesParameter) {
                var VerificationData = jQuery.parseJSON($scope.StateLicensesParameter[j].VerificationData);

                for (var k in $scope.stateLicenses) {
                    if (VerificationData != null) {
                        if (VerificationData.StateLicenseInformationID == $scope.stateLicenses[k].LincensesInfo.StateLicenseInformationID) {

                            $scope.stateLicenses[k].ProfileVerificationDetail = $scope.StateLicensesParameter[j];

                        }
                    }
                }
            }
        }

        $scope.copystateLicenses = angular.copy($scope.stateLicenses);


        //=============================================================

        //======================Board Certification====================
        
        for (var i = 0; i < data.SpecialtyDetails.length; i++) {

            if (data.SpecialtyDetails[i].BoardCertifiedYesNoOption == 1) {
                var VerificationData = JSON.stringify(data.SpecialtyDetails[i]);
                if ($scope.pendingPSV == null) {
                    $scope.BoradCertifications.push({ ViewDiv: false, EditDiv: false, AddDiv: true, LincensesInfo: data.SpecialtyDetails[i], ProfileVerificationDetail: { ProfileVerificationDetailId: '', ProfileVerificationParameterId: '', VerificationResult: { VerificationResultId: '', VerificationResultStatusType: '', Source: '', Remark: '', VerificationResultDocumentPath: '', VerificationResultDocument: '' }, VerificationData: VerificationData, VerifiedById: '', VerificationDate: '' } });

                } else {
                    $scope.BoradCertifications.push({ ViewDiv: true, EditDiv: false, AddDiv: false, LincensesInfo: data.SpecialtyDetails[i], ProfileVerificationDetail: { ProfileVerificationDetailId: '', ProfileVerificationParameterId: '', VerificationResult: { VerificationResultId: '', VerificationResultStatusType: '', Source: '', Remark: '', VerificationResultDocumentPath: '', VerificationResultDocument: '' }, VerificationData: VerificationData, VerifiedById: '', VerificationDate: '' } });

                }
            }
        }
        if ($scope.BoradCertificationsParameter.length > 0) {

            for (var j in $scope.BoradCertificationsParameter) {

                var VerificationData = jQuery.parseJSON($scope.BoradCertificationsParameter[j].VerificationData);

                for (var k in $scope.BoradCertifications) {
                    if (VerificationData != null) {
                        if (VerificationData.SpecialtyDetailID == $scope.BoradCertifications[k].LincensesInfo.SpecialtyDetailID) {
                            $scope.BoradCertifications[k].ProfileVerificationDetail = $scope.BoradCertificationsParameter[j];
                        }
                    }
                }
            }
        }

        $scope.copyBoradCertifications = angular.copy($scope.BoradCertifications);
        //=============================================================

        //========================CDS==================================

        for (var i = 0; i < data.CDSCInformations.length; i++) {

            var VerificationData = JSON.stringify(data.CDSCInformations[i]);
            if ($scope.pendingPSV == null) {
                $scope.CDSs.push({ ViewDiv: false, EditDiv: false, AddDiv: true, LincensesInfo: data.CDSCInformations[i], ProfileVerificationDetail: { ProfileVerificationDetailId: '', ProfileVerificationParameterId: '', VerificationResult: { VerificationResultId: '', VerificationResultStatusType: '', Source: '', Remark: '', VerificationResultDocumentPath: '', VerificationResultDocument: '' }, VerificationData: VerificationData, VerifiedById: '', VerificationDate: '' } });
            } else {
                $scope.CDSs.push({ ViewDiv: true, EditDiv: false, AddDiv: false, LincensesInfo: data.CDSCInformations[i], ProfileVerificationDetail: { ProfileVerificationDetailId: '', ProfileVerificationParameterId: '', VerificationResult: { VerificationResultId: '', VerificationResultStatusType: '', Source: '', Remark: '', VerificationResultDocumentPath: '', VerificationResultDocument: '' }, VerificationData: VerificationData, VerifiedById: '', VerificationDate: '' } });

            }
        }
        if ($scope.CDSsParameter.length > 0) {
            for (var j in $scope.CDSsParameter) {

                var VerificationData = jQuery.parseJSON($scope.CDSsParameter[j].VerificationData);
                for (var k in $scope.CDSs) {
                    if(VerificationData != null){
                        if (VerificationData.CDSCInformationID == $scope.CDSs[k].LincensesInfo.CDSCInformationID) {
                            $scope.CDSs[k].ProfileVerificationDetail = $scope.CDSsParameter[j];
                        }
                    }
                }
            }
        }

        $scope.copyCDSs = angular.copy($scope.CDSs);
        //============================================================

        //============================DEA==============================

        for (var i = 0; i < data.FederalDEAInformations.length; i++) {

            var VerificationData = JSON.stringify(data.FederalDEAInformations[i]);
            if ($scope.pendingPSV == null) {
                $scope.DEAs.push({ ViewDiv: false, EditDiv: false, AddDiv: true, LincensesInfo: data.FederalDEAInformations[i], ProfileVerificationDetail: { ProfileVerificationDetailId: '', ProfileVerificationParameterId: '', VerificationResult: { VerificationResultId: '', VerificationResultStatusType: '', Source: '', Remark: '', VerificationResultDocumentPath: '', VerificationResultDocument: '' }, VerificationData: VerificationData, VerifiedById: '', VerificationDate: '' } });
            } else {
                $scope.DEAs.push({ ViewDiv: true, EditDiv: false, AddDiv: false, LincensesInfo: data.FederalDEAInformations[i], ProfileVerificationDetail: { ProfileVerificationDetailId: '', ProfileVerificationParameterId: '', VerificationResult: { VerificationResultId: '', VerificationResultStatusType: '', Source: '', Remark: '', VerificationResultDocumentPath: '', VerificationResultDocument: '' }, VerificationData: VerificationData, VerifiedById: '', VerificationDate: '' } });

            }
        }
        if ($scope.DEAsParameter.length > 0) {
            for (var j in $scope.DEAsParameter) {

                var VerificationData = jQuery.parseJSON($scope.DEAsParameter[j].VerificationData);
                for (var k in $scope.DEAs) {
                    if (VerificationData != null) {
                        if (VerificationData.FederalDEAInformationID == $scope.DEAs[k].LincensesInfo.FederalDEAInformationID) {
                            $scope.DEAs[k].ProfileVerificationDetail = $scope.DEAsParameter[j];
                        }
                    }
                }
            }
        }
        $scope.copyDEAs = angular.copy($scope.DEAs);

        //========================NPDB=====================================

        if ($scope.pendingPSV == null) {
            $scope.NPDB = { ViewDiv: false, EditDiv: false, AddDiv: true, ProfileVerificationDetail: { ProfileVerificationDetailId: '', ProfileVerificationParameterId: '', VerificationResult: { VerificationResultId: '', VerificationResultStatusType: '', Source: '', Remark: '', VerificationResultDocumentPath: '', VerificationResultDocument: '' }, VerificationDate: '' } };
        } else {
            $scope.NPDB = { ViewDiv: true, EditDiv: false, AddDiv: false, ProfileVerificationDetail: { ProfileVerificationDetailId: '', ProfileVerificationParameterId: '', VerificationResult: { VerificationResultId: '', VerificationResultStatusType: '', Source: '', Remark: '', VerificationResultDocumentPath: '', VerificationResultDocument: '' }, VerificationDate: '' } };

        }

        if ($scope.NPDBsParameter != null)
            $scope.NPDB.ProfileVerificationDetail = $scope.NPDBsParameter;

        $scope.copyNPDB = angular.copy($scope.NPDB);

        //========================MedicareOPT=====================================
        if ($scope.pendingPSV == null) {
            $scope.MedicareOPT = { ViewDiv: false, EditDiv: false, AddDiv: true, ProfileVerificationDetail: { ProfileVerificationDetailId: '', ProfileVerificationParameterId: '', VerificationResult: { VerificationResultId: '', VerificationResultStatusType: '', Source: '', Remark: '', VerificationResultDocumentPath: '', VerificationResultDocument: '' }, VerificationDate: '' } };
        } else {
            $scope.MedicareOPT = { ViewDiv: true, EditDiv: false, AddDiv: false, ProfileVerificationDetail: { ProfileVerificationDetailId: '', ProfileVerificationParameterId: '', VerificationResult: { VerificationResultId: '', VerificationResultStatusType: '', Source: '', Remark: '', VerificationResultDocumentPath: '', VerificationResultDocument: '' }, VerificationDate: '' } };

        }

        if ($scope.MedicareOPTsParameter != null)
            $scope.MedicareOPT.ProfileVerificationDetail = $scope.MedicareOPTsParameter;

        $scope.copyMedicareOPT = angular.copy($scope.MedicareOPT);


        //========================OIG=====================================
        if ($scope.pendingPSV == null) {
            $scope.OIG = { ViewDiv: false, EditDiv: false, AddDiv: true, ProfileVerificationDetail: { ProfileVerificationDetailId: '', ProfileVerificationParameterId: '', VerificationResult: { VerificationResultId: '', VerificationResultStatusType: '', Source: '', Remark: '', VerificationResultDocumentPath: '', VerificationResultDocument: '' }, VerificationDate: '' } };
        } else {
            $scope.OIG = { ViewDiv: true, EditDiv: false, AddDiv: false, ProfileVerificationDetail: { ProfileVerificationDetailId: '', ProfileVerificationParameterId: '', VerificationResult: { VerificationResultId: '', VerificationResultStatusType: '', Source: '', Remark: '', VerificationResultDocumentPath: '', VerificationResultDocument: '' }, VerificationDate: '' } };

        }
        if ($scope.OIGsParameter != null)
            $scope.OIG.ProfileVerificationDetail = $scope.OIGsParameter;

        $scope.copyOIG = angular.copy($scope.OIG);


    };

    
   
    $scope.getPSVHistory = function (profileId) {

        //var profileId = profile.ProfileID;;
        $http.get(rootDir + '/Credentialing/Verification/GetPSVHistory?profileId=' + profileId).
       success(function (data, status, headers, config) {

           
           try {
               if (data.status == 'true') {
                   var data = data.psvHistory;
                   for (var i = 0; i < data.length; i++) {
                       data[i].VerificationData = jQuery.parseJSON(data[i].VerificationData);
                       data[i].VerificationDate = $scope.ConvertDateFormat1(data[i].VerificationDate);
                       if (data[i].ProfileVerificationParameter.Code == 'SL') {
                           $scope.StateLicenseHistory.push(data[i]);
                       }
                       if (data[i].ProfileVerificationParameter.Code == 'BC') {
                           $scope.BoardCertificationHistory.push(data[i]);
                       }
                       if (data[i].ProfileVerificationParameter.Code == 'DEA') {
                           $scope.DEAHistory.push(data[i]);
                       }
                       if (data[i].ProfileVerificationParameter.Code == 'CDS') {
                           $scope.CDSHistory.push(data[i]);
                       }
                       if (data[i].ProfileVerificationParameter.Code == 'NPDB') {
                           $scope.NPDBHistory.push(data[i]);
                       }
                       if (data[i].ProfileVerificationParameter.Code == 'MOPT') {
                           $scope.MedicareOPTHistory.push(data[i]);
                       }
                       if (data[i].ProfileVerificationParameter.Code == 'OIG') {
                           $scope.OIGHistory.push(data[i]);
                       }
                   }
               }
           } catch (e) {
              
           }
       }).
         error(function (data, status, headers, config) {

         });
    }
    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    $scope.StateLicensesParameter = [];
    $scope.BoradCertificationsParameter = [];
    $scope.DEAsParameter = [];
    $scope.CDSsParameter = [];

    $scope.assignParameter = function () {

        for (var p in $scope.pendingPSV) {

            if ($scope.pendingPSV[p].ProfileVerificationParameterId == $scope.StateLicenseParameterID) {
                $scope.pendingPSV[p].VerificationDate = $scope.ConvertDateFormat($scope.pendingPSV[p].VerificationDate);
                $scope.StateLicensesParameter.push($scope.pendingPSV[p]);
            }
            if ($scope.pendingPSV[p].ProfileVerificationParameterId == $scope.BoardCertificationParameterID) {
                $scope.pendingPSV[p].VerificationDate = $scope.ConvertDateFormat($scope.pendingPSV[p].VerificationDate);
                $scope.BoradCertificationsParameter.push($scope.pendingPSV[p]);
            }
            if ($scope.pendingPSV[p].ProfileVerificationParameterId == $scope.DEAParameterID) {
                $scope.pendingPSV[p].VerificationDate = $scope.ConvertDateFormat($scope.pendingPSV[p].VerificationDate);
                $scope.DEAsParameter.push($scope.pendingPSV[p]);
            }
            if ($scope.pendingPSV[p].ProfileVerificationParameterId == $scope.CDSParameterID) {
                $scope.pendingPSV[p].VerificationDate = $scope.ConvertDateFormat($scope.pendingPSV[p].VerificationDate);
                $scope.CDSsParameter.push($scope.pendingPSV[p]);
            }
            if ($scope.pendingPSV[p].ProfileVerificationParameterId == $scope.NPDBParameterID) {
                $scope.pendingPSV[p].VerificationDate = $scope.ConvertDateFormat($scope.pendingPSV[p].VerificationDate);
                $scope.NPDBsParameter = $scope.pendingPSV[p];
            }
            if ($scope.pendingPSV[p].ProfileVerificationParameterId == $scope.MOPTParameterID) {
                $scope.pendingPSV[p].VerificationDate = $scope.ConvertDateFormat($scope.pendingPSV[p].VerificationDate);
                $scope.MedicareOPTsParameter = $scope.pendingPSV[p];
            }
            if ($scope.pendingPSV[p].ProfileVerificationParameterId == $scope.OIGParameterID) {
                $scope.pendingPSV[p].VerificationDate = $scope.ConvertDateFormat($scope.pendingPSV[p].VerificationDate);
                $scope.OIGsParameter = $scope.pendingPSV[p];
            }
        }
    };

    $scope.LoadPendingPSV = function (data) {
        if (data.length > 0) {
            $scope.enableVerified = false;
        }
        else {
            $scope.enableVerified = true;
        }
        $scope.pendingPSV = data;
    }

    //=========================GetAllProfileVerificationParameter======================
    $scope.GetProfileVerificationParameter = function () {

        $http.get(rootDir + '/Profile/MasterData/GetAllProfileVerificationParameter').
        success(function (data, status, headers, config) {

            try {
                for (var i = 0; i < data.length; i++) {
                    if (data[i].Code == 'SL') {
                        $scope.StateLicenseParameterID = data[i].ProfileVerificationParameterId;
                    }
                    if (data[i].Code == 'BC') {
                        $scope.BoardCertificationParameterID = data[i].ProfileVerificationParameterId;
                    }
                    if (data[i].Code == 'DEA') {
                        $scope.DEAParameterID = data[i].ProfileVerificationParameterId;
                    }
                    if (data[i].Code == 'CDS') {
                        $scope.CDSParameterID = data[i].ProfileVerificationParameterId;
                    }
                    if (data[i].Code == 'NPDB') {
                        $scope.NPDBParameterID = data[i].ProfileVerificationParameterId;
                    }
                    if (data[i].Code == 'MOPT') {
                        $scope.MOPTParameterID = data[i].ProfileVerificationParameterId;
                    }
                    if (data[i].Code == 'OIG') {
                        $scope.OIGParameterID = data[i].ProfileVerificationParameterId;
                    }
                }
            } catch (e) {
              
            }
            $scope.assignParameter();
            $scope.loadObjFormat($scope.ProfileData);

        }).
        error(function (data, status, headers, config) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
        });
    };

    //=========================formatData=============

    $scope.FormatData = function (data) {

        for (var i = 0; i < data.length; i++) {

            var curData = data[i];
            $scope.LicenseList.push({ id: curData.ProfileVerificationParameterId, title: curData.Title, tabId: 'tabId' + i, tabPanelId: 'tabPanelId' + i });

        }
    };

    //============date=================================
    var monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun",
"Jul", "Aug", "Sep", "Oct", "Nov", "Dec"
    ];

    $scope.reformatDate = function (dateStr) {

        if (dateStr != null) {
            dArr1 = dateStr.split("T");//2015-06-26T00:00:00
            dArr2 = dArr1[0].split("-");

            // return dArr2[2] + 'th ' + monthNames[dArr2[1] - 1] + ',' + dArr2[0];

            return dArr2[1] + '/' + dArr2[2] + '/' + dArr2[0];
        }

    }


    $scope.getVerificationLinks = function () {

        $http.get(rootDir + '/Profile/MasterData/GetAllVerificationLinks').
      success(function (data, status, headers, config) {

          $scope.Sources = data;
      }).
      error(function (data, status, headers, config) {
          // called asynchronously if an error occurs
          // or server returns response with an error status.
      });
    }

    //$scope.Sources = [{ VerificationLinkID: 1, Name: 'Florida Medical License verification link', Link: 'https://appsmqa.doh.state.fl.us/IRM00PRAES/PRASLIST.ASP' }, { VerificationLinkID: 2, Name: 'Amercian Board of Family Medicine cert verification link', Link: 'https://www.theabfm.org/diplomate/verify.aspx' }, { VerificationLinkID: 3, Name: 'American Board of Internal Medicine cert verification link', Link: 'http://www.abim.org/services/verify-a-physician.aspx' }, { VerificationLinkID: 4, Name: 'CAQH Universal Provider Data source link', Link: 'https://upd.caqh.org/oas/' }, { VerificationLinkID: 4, Name: 'CAQH Universal Provider Data source link', Link: 'https://upd.caqh.org/oas/' }, { VerificationLinkID: 4, Name: 'CAQH Universal Provider Data source link', Link: 'https://upd.caqh.org/oas/' }, { VerificationLinkID: 4, Name: 'CAQH Universal Provider Data source link', Link: 'https://upd.caqh.org/oas/' }, { VerificationLinkID: 4, Name: 'CAQH Universal Provider Data source link', Link: 'https://upd.caqh.org/oas/' }];


});

//--------------- file name Wrap-text author : krglv ---------------
function setFileNameWith(file) {
    $(file).parent().parent().find(".jancyFileWrapTexts").find("span").width($(file).parent().parent().width() - 197);
}

$(document).ready(function () {
    $(".ProviderTypeSelectAutoList1").hide();
    $('.popover').on('click', function () {

        $('.popover').hide();
    })
});


//================================= Hide All country code popover =========================
$(document).click(function (event) {
    if (!$(event.target).hasClass("form-control") && $(event.target).parents(".ProviderTypeSelectAutoList1").length === 0) {
        $(".ProviderTypeSelectAutoList1").hide();
    }
});
