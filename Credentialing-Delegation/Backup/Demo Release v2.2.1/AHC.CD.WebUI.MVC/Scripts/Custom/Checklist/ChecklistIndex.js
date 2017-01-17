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




var psvModule = angular.module('primarySourceApp', []);

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

psvModule.controller('profileAppCtrl', function ($scope, $http, messageAlertEngine) {

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
    //==============================================================
    $scope.ProfileVerificationDetailViewModel = [];
    $scope.enableVerified = true;
    $scope.ProfileObj = null;

    $scope.ConvertDateFormat = function (value) {
        ////console.log(value);
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

    //---------------------------- set selected file Contract ---------------------
    //$scope.setFiles = function (element) {
    //    $scope.$apply(function (scope) {
    //        if (element.files[0]) {
    //            $scope.selectedCV = element.files[0];
    //        } else {
    //            $scope.selectedCV = {};
    //        }
    //    });
    //};



    //======================state License===================
    $scope.stateLicenses = [];
    $scope.AddStateLicenseProfileVerificationDetail = function (index) {
        var $formData;
        

        $formData = $('#StateLicenseForm' + index);

        $.ajax({
            url: '/Credentialing/Verification/AddPSVDetail?credVerificationInfoId=' + $scope.verificationId + '&profileId=' + $scope.ProfileObj.ProfileID,
            type: 'POST',
            data: new FormData($formData[0]),
            async: false,
            cache: false,
            contentType: false,
            processData: false,
            success: function (data) {
                if (data.status == "true") {
                    data.verificationDetail.VerificationDate = $scope.ConvertDateFormat(data.verificationDetail.VerificationDate);
                    //$scope.verifiedDate = data.verificationDetail;
                    $scope.stateLicenses[index].ProfileVerificationDetail = data.verificationDetail;
                    $scope.enableVerified = false;
                    messageAlertEngine.callAlertMessage("statePSV" + index, "State License information verified Successfully !!!!", "success", true);
                }

            },
            error: function (e) {

                messageAlertEngine.callAlertMessage("statePSVError" + index, "Unable to save verified data !!!!", "danger", true);
            }
        });

    };
    $scope.UpdateStateLicenseProfileVerificationDetail = function (index) {
        var $formdata;        

        $formData = $('#StateLicenseForm' + index);

        $.ajax({
            url: '/Credentialing/Verification/UpdatePSVDetail?credVerificationInfoId=' + $scope.verificationId + '&profileId=' + $scope.ProfileObj.ProfileID,
            type: 'POST',
            data: new FormData($formData[0]),
            async: false,
            cache: false,
            contentType: false,
            processData: false,
            success: function (data) {
                if (data.status == "true") {
                    data.verificationDetail.VerificationDate = $scope.ConvertDateFormat(data.verificationDetail.VerificationDate);
                    //$scope.verifiedDate = data.verificationDetail;
                    $scope.LicenseList[index].ProfileVerificationDetail = data.verificationDetail;
                    $scope.enableVerified = false;
                    messageAlertEngine.callAlertMessage("statePSV" + index, "State License information verified Successfully !!!!", "success", true);
                }

            },
            error: function (e) {

                messageAlertEngine.callAlertMessage("statePSVError" + index, "Unable to update verified data !!!!", "danger", true);
            }
        });

    };

    //=======================Board Certification==================

    $scope.BoradCertifications = [];
    $scope.AddBoardCertificationProfileVerificationDetail = function (index) {


        var $formdata;
        //var data = $('#BoardCertificationForm' + index).serializeArray();
        //for (var i in data) {
        //    formdata.append(data[i].name, data[i].value);
        //}
        //formdata.append($("#VerificationResult_VerificationResultDocument")[0].name, $("#VerificationResult_VerificationResultDocument")[0].files[0]);

        $formData = $('#BoardCertificationForm' + index);

        $.ajax({
            url: '/Credentialing/Verification/AddPSVDetail?credVerificationInfoId=' + $scope.verificationId + '&profileId=' + $scope.ProfileObj.ProfileID,
            type: 'POST',
            data: new FormData($formData[0]),
            async: false,
            cache: false,
            contentType: false,
            processData: false,
            success: function (data) {
                if (data.status == "true") {
                    console.log(data);
                    data.verificationDetail.VerificationDate = $scope.ConvertDateFormat(data.verificationDetail.VerificationDate);
                    //$scope.verifiedDate = data.verificationDetail;

                    $scope.BoradCertifications[index].ProfileVerificationDetail = data.verificationDetail;
                    $scope.enableVerified = false;
                    messageAlertEngine.callAlertMessage("boardPSV" + index, "Borad Certifications information verified Successfully !!!!", "success", true);
                }

            },
            error: function (e) {
                messageAlertEngine.callAlertMessage("boardPSVError" + index, "Unable to save verified data !!!!", "danger", true);
            }
        });
    }
    $scope.UpdateBoardCertificationProfileVerificationDetail = function (index) {


        var $formdata;
        //var data = $('#BoardCertificationForm' + index).serializeArray();
        //for (var i in data) {
        //    formdata.append(data[i].name, data[i].value);
        //}
        //formdata.append($("#VerificationResult_VerificationResultDocument")[0].name, $("#VerificationResult_VerificationResultDocument")[0].files[0]);
        $formData = $('#BoardCertificationForm' + index);
        $.ajax({
            url: '/Credentialing/Verification/UpdatePSVDetail?credVerificationInfoId=' + $scope.verificationId + '&profileId=' + $scope.ProfileObj.ProfileID,
            type: 'POST',
            data: new FormData($formData[0]),
            async: false,
            cache: false,
            contentType: false,
            processData: false,
            success: function (data) {
                if (data.status == "true") {
                    console.log(data);
                    data.verificationDetail.VerificationDate = $scope.ConvertDateFormat(data.verificationDetail.VerificationDate);
                    //$scope.verifiedDate = data.verificationDetail;

                    $scope.BoradCertifications[index].ProfileVerificationDetail = data.verificationDetail;
                    $scope.enableVerified = false;
                    messageAlertEngine.callAlertMessage("boardPSV" + index, "Borad Certifications information verified Successfully !!!!", "success", true);
                }

            },
            error: function (e) {
                messageAlertEngine.callAlertMessage("boardPSVError" + index, "Unable to update verified data !!!!", "danger", true);
            }
        });
    }
    //========================CDS================================
    $scope.CDSs = [];
    $scope.AddCDSProfileVerificationDetail = function (index) {

        var $formdata;
        
        $formData = $('#CDSForm' + index);
        $.ajax({
            url: '/Credentialing/Verification/AddPSVDetail?credVerificationInfoId=' + $scope.verificationId + '&profileId=' + $scope.ProfileObj.ProfileID,
            type: 'POST',
            data: new FormData($formData[0]),
            async: false,
            cache: false,
            contentType: false,
            processData: false,
            success: function (data) {
                if (data.status == "true") {
                    data.verificationDetail.VerificationDate = $scope.ConvertDateFormat(data.verificationDetail.VerificationDate);
                    //$scope.verifiedDate = data.verificationDetail;
                    $scope.enableVerified = false;
                    $scope.CDSs[index].ProfileVerificationDetail = data.verificationDetail;
                    messageAlertEngine.callAlertMessage("cdsPSV" + index, "CDS information verified Successfully !!!!", "success", true);
                }

            },
            error: function (e) {
                messageAlertEngine.callAlertMessage("cdsPSVError" + index, "Unable to save verified data !!!!", "danger", true);
            }
        });
    }
    $scope.UpdateCDSProfileVerificationDetail = function (index) {

        var $formdata;
        //var data = $('#CDSForm' + index).serializeArray();
        //for (var i in data) {
        //    formdata.append(data[i].name, data[i].value);
        //}
        //formdata.append($("#VerificationResult_VerificationResultDocument")[0].name, $("#VerificationResult_VerificationResultDocument")[0].files[0]);

        $formData = $('#CDSForm' + index);

        $.ajax({
            url: '/Credentialing/Verification/UpdatePSVDetail?credVerificationInfoId=' + $scope.verificationId + '&profileId=' + $scope.ProfileObj.ProfileID,
            type: 'POST',
            data: new FormData($formData[0]),
            async: false,
            cache: false,
            contentType: false,
            processData: false,
            success: function (data) {
                if (data.status == "true") {
                    data.verificationDetail.VerificationDate = $scope.ConvertDateFormat(data.verificationDetail.VerificationDate);
                    //$scope.verifiedDate = data.verificationDetail;
                    $scope.enableVerified = false;
                    $scope.CDSs[index].ProfileVerificationDetail = data.verificationDetail;
                    messageAlertEngine.callAlertMessage("cdsPSV" + index, "CDS information verified Successfully !!!!", "success", true);
                }

            },
            error: function (e) {
                messageAlertEngine.callAlertMessage("cdsPSVError" + index, "Unable to update verified data !!!!", "danger", true);
            }
        });
    }
    //=======================DEA=================================
    $scope.DEAs = [];
    $scope.AddDEAProfileVerificationDetail = function (index) {

        var $formdata;
        
        $formData = $('#DEAForm' + index);
        $.ajax({
            url: '/Credentialing/Verification/AddPSVDetail?credVerificationInfoId=' + $scope.verificationId + '&profileId=' + $scope.ProfileObj.ProfileID,
            type: 'POST',
            data: new FormData($formData[0]),
            async: false,
            cache: false,
            contentType: false,
            processData: false,
            success: function (data) {
                if (data.status == "true") {
                    data.verificationDetail.VerificationDate = $scope.ConvertDateFormat(data.verificationDetail.VerificationDate);
                    //$scope.verifiedDate = data.verificationDetail;
                    $scope.enableVerified = false;
                    $scope.DEAs[index].ProfileVerificationDetail = data.verificationDetail;
                    messageAlertEngine.callAlertMessage("deaPSV" + index, "DEA information verified Successfully !!!!", "success", true);
                }

            },
            error: function (e) {
                messageAlertEngine.callAlertMessage("deaPSVError" + index, "Unable to save verified data !!!!", "danger", true);
            }
        });
    }
    $scope.UpdateDEAProfileVerificationDetail = function (index) {

        var $formdata;
        
        $formData = $('#DEAForm' + index);
        $.ajax({
            url: '/Credentialing/Verification/UpdatePSVDetail?credVerificationInfoId=' + $scope.verificationId + '&profileId=' + $scope.ProfileObj.ProfileID,
            type: 'POST',
            data: new FormData($formData[0]),
            async: false,
            cache: false,
            contentType: false,
            processData: false,
            success: function (data) {
                if (data.status == "true") {
                    data.verificationDetail.VerificationDate = $scope.ConvertDateFormat(data.verificationDetail.VerificationDate);
                    //$scope.verifiedDate = data.verificationDetail;
                    $scope.enableVerified = false;
                    $scope.DEAs[index].ProfileVerificationDetail = data.verificationDetail;
                    messageAlertEngine.callAlertMessage("deaPSV" + index, "DEA information verified Successfully !!!!", "success", true);
                }

            },
            error: function (e) {
                messageAlertEngine.callAlertMessage("deaPSVError" + index, "Unable to update verified data !!!!", "danger", true);
            }
        });
    }
    //======================OIG==============================

    $scope.AddOIGProfileVerificationDetail = function () {

        var $formdata;
        
        $formData = $('#OIGForm');
        $.ajax({
            url: '/Credentialing/Verification/AddPSVDetail?credVerificationInfoId=' + $scope.verificationId + '&profileId=' + $scope.ProfileObj.ProfileID,
            type: 'POST',
            data: new FormData($formData[0]),
            async: false,
            cache: false,
            contentType: false,
            processData: false,
            success: function (data) {
                if (data.status == "true") {
                    data.verificationDetail.VerificationDate = $scope.ConvertDateFormat(data.verificationDetail.VerificationDate);
                    //$scope.verifiedDate = data.verificationDetail;
                    $scope.enableVerified = false;
                    $scope.OIG.ProfileVerificationDetail = data.verificationDetail;
                    messageAlertEngine.callAlertMessage("oigPSV", "Information verified Successfully !!!!", "success", true);
                }


            },
            error: function (e) {
                messageAlertEngine.callAlertMessage("oigPSVError", "Unable to save verified data !!!!", "danger", true);
            }
        });
    };
    $scope.UpdateOIGProfileVerificationDetail = function () {

        var $formdata;
       
        $formData = $('#OIGForm');
        $.ajax({
            url: '/Credentialing/Verification/UpdatePSVDetail?credVerificationInfoId=' + $scope.verificationId + '&profileId=' + $scope.ProfileObj.ProfileID,
            type: 'POST',
            data: new FormData($formData[0]),
            async: false,
            cache: false,
            contentType: false,
            processData: false,
            success: function (data) {
                if (data.status == "true") {
                    data.verificationDetail.VerificationDate = $scope.ConvertDateFormat(data.verificationDetail.VerificationDate);
                    //$scope.verifiedDate = data.verificationDetail;
                    $scope.enableVerified = false;
                    $scope.OIG.ProfileVerificationDetail = data.verificationDetail;
                    messageAlertEngine.callAlertMessage("oigPSV", "Information verified Successfully !!!!", "success", true);
                }


            },
            error: function (e) {
                messageAlertEngine.callAlertMessage("oigPSVError", "Unable to update verified data !!!!", "danger", true);
            }
        });
    };

    //======================NPDB==============================
    $scope.AddNPDBProfileVerificationDetail = function (id) {

        var $formdata;
        
        $formData = $('#NPDBForm');
        $.ajax({
            url: '/Credentialing/Verification/AddPSVDetail?credVerificationInfoId=' + $scope.verificationId + '&profileId=' + $scope.ProfileObj.ProfileID,
            type: 'POST',
            data: new FormData($formData[0]),
            async: false,
            cache: false,
            contentType: false,
            processData: false,
            success: function (data) {
                if (data.status == "true") {
                    data.verificationDetail.VerificationDate = $scope.ConvertDateFormat(data.verificationDetail.VerificationDate);
                    //$scope.verifiedDate = data.verificationDetail;
                    $scope.enableVerified = false;
                    $scope.NPDB.ProfileVerificationDetail = data.verificationDetail;
                    messageAlertEngine.callAlertMessage("npdbPSV", "Information verified Successfully !!!!", "success", true);
                }


            },
            error: function (e) {
                messageAlertEngine.callAlertMessage("npdbPSVError", "Unable to save verified data !!!!", "danger", true);
            }
        });
    };
    $scope.UpdateNPDBProfileVerificationDetail = function (id) {

        var $formdata = new FormData();
        
        $formData = $('#NPDBForm');
        $.ajax({
            url: '/Credentialing/Verification/UpdatePSVDetail?credVerificationInfoId=' + $scope.verificationId + '&profileId=' + $scope.ProfileObj.ProfileID,
            type: 'POST',
            data: new FormData($formData[0]),
            async: false,
            cache: false,
            contentType: false,
            processData: false,
            success: function (data) {
                if (data.status == "true") {
                    data.verificationDetail.VerificationDate = $scope.ConvertDateFormat(data.verificationDetail.VerificationDate);
                    //$scope.verifiedDate = data.verificationDetail;
                    $scope.enableVerified = false;
                    $scope.NPDB.ProfileVerificationDetail = data.verificationDetail;
                    messageAlertEngine.callAlertMessage("npdbPSV", "Information verified Successfully !!!!", "success", true);
                }


            },
            error: function (e) {
                messageAlertEngine.callAlertMessage("npdbPSVError", "Unable to update verified data !!!!", "danger", true);
            }
        });
    };

    //======================Medicare OPT==============================
    $scope.AddMedicareOPTProfileVerificationDetail = function (id) {

        var $formdata;
        
        $formData = $('#medicareOPT');
        $.ajax({
            url: '/Credentialing/Verification/AddPSVDetail?credVerificationInfoId=' + $scope.verificationId + '&profileId=' + $scope.ProfileObj.ProfileID,
            type: 'POST',
            data: new FormData($formData[0]),
            async: false,
            cache: false,
            contentType: false,
            processData: false,
            success: function (data) {
                if (data.status == "true") {
                    data.verificationDetail.VerificationDate = $scope.ConvertDateFormat(data.verificationDetail.VerificationDate);
                    //$scope.verifiedDate = data.verificationDetail;
                    $scope.enableVerified = false;
                    $scope.MedicareOPT.ProfileVerificationDetail = data.verificationDetail;
                    messageAlertEngine.callAlertMessage("optPSV", "Information verified Successfully !!!!", "success", true);
                }


            },
            error: function (e) {
                messageAlertEngine.callAlertMessage("optPSVError", "Unable to save verified data !!!!", "danger", true);
            }
        });
    };
    $scope.UpdateMedicareOPTProfileVerificationDetail = function (id) {

        var $formdata;
        
        $formData = $('#medicareOPT');
        $.ajax({
            url: '/Credentialing/Verification/UpdatePSVDetail?credVerificationInfoId=' + $scope.verificationId + '&profileId=' + $scope.ProfileObj.ProfileID,
            type: 'POST',
            data: new FormData($formData[0]),
            async: false,
            cache: false,
            contentType: false,
            processData: false,
            success: function (data) {
                if (data.status == "true") {
                    data.verificationDetail.VerificationDate = $scope.ConvertDateFormat(data.verificationDetail.VerificationDate);
                    //$scope.verifiedDate = data.verificationDetail;
                    $scope.enableVerified = false;
                    $scope.MedicareOPT.ProfileVerificationDetail = data.verificationDetail;
                    messageAlertEngine.callAlertMessage("optPSV", "Information verified Successfully !!!!", "success", true);
                }


            },
            error: function (e) {
                messageAlertEngine.callAlertMessage("optPSVError", "Unable to update verified data !!!!", "danger", true);
            }
        });
    };


    $scope.setAllVerified = function () {

        $http.get('/Credentialing/Verification/SetAllVerified?credinfoId=' + $scope.credentialingInfoId + '&credVerificationId=' + $scope.verificationId).
        success(function (data, status, headers, config) {

            //$scope.FormatData(data);
            // this callback will be called asynchronously
            // when the response is available
        }).
        error(function (data, status, headers, config) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
        });
    }

    $scope.LoadVerification = function (id) {
        $scope.verificationId = id;
    }

    $scope.LoadCredInfo = function (id) {
        $scope.credentialingInfoId = id;
    }

    $scope.loadObjFormat = function (data) {

        
        //=============StateLicenses==================================
        
            for (var i = 0; i < data.Profile.StateLicenses.length; i++) {


                var VerificationData = JSON.stringify(data.Profile.StateLicenses[i]);
                //var VerificationData = null;
                if ($scope.pendingPSV == null) {
                    $scope.stateLicenses.push({ ViewDiv: false, EditDiv: false, AddDiv: true, LincensesInfo: data.Profile.StateLicenses[i], ProfileVerificationDetail: { ProfileVerificationDetailId: '', ProfileVerificationParameterId: '', VerificationResult: { VerificationResultId: '', VerificationResultStatusType: '', Source: '', Remark: '', VerificationResultDocumentPath: '', VerificationResultDocument: '' }, VerificationData: VerificationData, VerifiedById: '', VerificationDate: '' } });
                }
                else {
                    $scope.stateLicenses.push({ ViewDiv: true, EditDiv: false, AddDiv: false, LincensesInfo: data.Profile.StateLicenses[i], ProfileVerificationDetail: { ProfileVerificationDetailId: '', ProfileVerificationParameterId: '', VerificationResult: { VerificationResultId: '', VerificationResultStatusType: '', Source: '', Remark: '', VerificationResultDocumentPath: '', VerificationResultDocument: '' }, VerificationData: VerificationData, VerifiedById: '', VerificationDate: '' } });

                }
            }
            if ($scope.StateLicensesParameter.length > 0) {
                for (var j in $scope.StateLicensesParameter) {
                    var VerificationData = jQuery.parseJSON($scope.StateLicensesParameter[j].VerificationData);

                    for (var k in $scope.stateLicenses) {
                        if (VerificationData.StateLicenseInformationID == $scope.stateLicenses[k].LincensesInfo.StateLicenseInformationID) {

                            $scope.stateLicenses[k].ProfileVerificationDetail = $scope.StateLicensesParameter[j];
                        }

                    }
                }
            }
        
       


        //=============================================================

        //======================Board Certification====================
        // var boardCertificationInfo = [];
       

        //if (data.Profile.SpecialtyDetails.length == 0) {

        //    if ($scope.pendingPSV == null) {
        //        $scope.BoradCertifications.push({ ViewDiv: false, EditDiv: false, AddDiv: true, LincensesInfo: '', ProfileVerificationDetail: { ProfileVerificationDetailId: '', ProfileVerificationParameterId: '', VerificationResult: { VerificationResultId: '', VerificationResultStatusType: '', Source: '', Remark: '', VerificationResultDocumentPath: '', VerificationResultDocument: '' }, VerificationData: '', VerifiedById: '', VerificationDate: '' } });
        //    }
        //    else {
        //        $scope.BoradCertifications.push({ ViewDiv: true, EditDiv: false, AddDiv: false, LincensesInfo: '', ProfileVerificationDetail: { ProfileVerificationDetailId: '', ProfileVerificationParameterId: '', VerificationResult: { VerificationResultId: '', VerificationResultStatusType: '', Source: '', Remark: '', VerificationResultDocumentPath: '', VerificationResultDocument: '' }, VerificationData: '', VerifiedById: '', VerificationDate: '' } });

        //    }

        //} else {
            for (var i = 0; i < data.Profile.SpecialtyDetails.length; i++) {

                var VerificationData = JSON.stringify(data.Profile.SpecialtyDetails[i]);
                if ($scope.pendingPSV == null) {
                    $scope.BoradCertifications.push({ ViewDiv: false, EditDiv: false, AddDiv: true, LincensesInfo: data.Profile.SpecialtyDetails[i], ProfileVerificationDetail: { ProfileVerificationDetailId: '', ProfileVerificationParameterId: '', VerificationResult: { VerificationResultId: '', VerificationResultStatusType: '', Source: '', Remark: '', VerificationResultDocumentPath: '', VerificationResultDocument: '' }, VerificationData: VerificationData, VerifiedById: '', VerificationDate: '' } });
                } else {
                    $scope.BoradCertifications.push({ ViewDiv: true, EditDiv: false, AddDiv: false, LincensesInfo: data.Profile.SpecialtyDetails[i], ProfileVerificationDetail: { ProfileVerificationDetailId: '', ProfileVerificationParameterId: '', VerificationResult: { VerificationResultId: '', VerificationResultStatusType: '', Source: '', Remark: '', VerificationResultDocumentPath: '', VerificationResultDocument: '' }, VerificationData: VerificationData, VerifiedById: '', VerificationDate: '' } });

                }
            }
            if ($scope.BoradCertificationsParameter.length > 0) {

                for (var j in $scope.BoradCertificationsParameter) {

                    var VerificationData = jQuery.parseJSON($scope.BoradCertificationsParameter[j].VerificationData);

                    for (var k in $scope.BoradCertifications) {
                        if (VerificationData.SpecialtyDetailID == $scope.BoradCertifications[k].LincensesInfo.SpecialtyDetailID) {
                            $scope.BoradCertifications[k].ProfileVerificationDetail = $scope.BoradCertificationsParameter[j];
                        }

                    }
                }
            }
        //}

        
        //=============================================================

        //========================CDS==================================
        //var CDSInfo = [];


        //if (data.Profile.CDSCInformations.length == 0) {

        //    if ($scope.pendingPSV == null) {
        //        $scope.CDSs.push({ ViewDiv: false, EditDiv: false, AddDiv: true, LincensesInfo: '', ProfileVerificationDetail: { ProfileVerificationDetailId: '', ProfileVerificationParameterId: '', VerificationResult: { VerificationResultId: '', VerificationResultStatusType: '', Source: '', Remark: '', VerificationResultDocumentPath: '', VerificationResultDocument: '' }, VerificationData: '', VerifiedById: '', VerificationDate: '' } });
        //    }
        //    else {
        //        $scope.CDSs.push({ ViewDiv: true, EditDiv: false, AddDiv: false, LincensesInfo: '', ProfileVerificationDetail: { ProfileVerificationDetailId: '', ProfileVerificationParameterId: '', VerificationResult: { VerificationResultId: '', VerificationResultStatusType: '', Source: '', Remark: '', VerificationResultDocumentPath: '', VerificationResultDocument: '' }, VerificationData: '', VerifiedById: '', VerificationDate: '' } });

        //    }

        //} else {
            for (var i = 0; i < data.Profile.CDSCInformations.length; i++) {

                var VerificationData = JSON.stringify(data.Profile.CDSCInformations[i]);
                if ($scope.pendingPSV == null) {
                    $scope.CDSs.push({ ViewDiv: false, EditDiv: false, AddDiv: true, LincensesInfo: data.Profile.CDSCInformations[i], ProfileVerificationDetail: { ProfileVerificationDetailId: '', ProfileVerificationParameterId: '', VerificationResult: { VerificationResultId: '', VerificationResultStatusType: '', Source: '', Remark: '', VerificationResultDocumentPath: '', VerificationResultDocument: '' }, VerificationData: VerificationData, VerifiedById: '', VerificationDate: '' } });
                } else {
                    $scope.CDSs.push({ ViewDiv: true, EditDiv: false, AddDiv: false, LincensesInfo: data.Profile.CDSCInformations[i], ProfileVerificationDetail: { ProfileVerificationDetailId: '', ProfileVerificationParameterId: '', VerificationResult: { VerificationResultId: '', VerificationResultStatusType: '', Source: '', Remark: '', VerificationResultDocumentPath: '', VerificationResultDocument: '' }, VerificationData: VerificationData, VerifiedById: '', VerificationDate: '' } });

                }
            }
            if ($scope.CDSsParameter.length > 0) {
                for (var j in $scope.CDSsParameter) {

                    var VerificationData = jQuery.parseJSON($scope.CDSsParameter[j].VerificationData);
                    for (var k in $scope.CDSs) {

                        if (VerificationData.CDSCInformationID == $scope.CDSs[k].LincensesInfo.CDSCInformationID) {
                            $scope.CDSs[k].ProfileVerificationDetail = $scope.CDSsParameter[j];
                        }

                    }
                }
            }
        //}
        

        //$scope.CDSs = { tabId: 'tabId1', info: CDSInfo };
        //============================================================

        //============================DEA==============================
        //var DEAInfo = [];


        

        //if (data.Profile.FederalDEAInformations.length == 0) {

        //    if ($scope.pendingPSV == null) {
        //        $scope.DEAs.push({ ViewDiv: false, EditDiv: false, AddDiv: true, LincensesInfo: '', ProfileVerificationDetail: { ProfileVerificationDetailId: '', ProfileVerificationParameterId: '', VerificationResult: { VerificationResultId: '', VerificationResultStatusType: '', Source: '', Remark: '', VerificationResultDocumentPath: '', VerificationResultDocument: '' }, VerificationData: '', VerifiedById: '', VerificationDate: '' } });
        //    }
        //    else {
        //        $scope.DEAs.push({ ViewDiv: true, EditDiv: false, AddDiv: false, LincensesInfo: '', ProfileVerificationDetail: { ProfileVerificationDetailId: '', ProfileVerificationParameterId: '', VerificationResult: { VerificationResultId: '', VerificationResultStatusType: '', Source: '', Remark: '', VerificationResultDocumentPath: '', VerificationResultDocument: '' }, VerificationData: '', VerifiedById: '', VerificationDate: '' } });

        //    }

        //} else {
            for (var i = 0; i < data.Profile.FederalDEAInformations.length; i++) {

                var VerificationData = JSON.stringify(data.Profile.FederalDEAInformations[i]);
                if ($scope.pendingPSV == null) {
                    $scope.DEAs.push({ ViewDiv: false, EditDiv: false, AddDiv: true, LincensesInfo: data.Profile.FederalDEAInformations[i], ProfileVerificationDetail: { ProfileVerificationDetailId: '', ProfileVerificationParameterId: '', VerificationResult: { VerificationResultId: '', VerificationResultStatusType: '', Source: '', Remark: '', VerificationResultDocumentPath: '', VerificationResultDocument: '' }, VerificationData: VerificationData, VerifiedById: '', VerificationDate: '' } });
                } else {
                    $scope.DEAs.push({ ViewDiv: true, EditDiv: false, AddDiv: false, LincensesInfo: data.Profile.FederalDEAInformations[i], ProfileVerificationDetail: { ProfileVerificationDetailId: '', ProfileVerificationParameterId: '', VerificationResult: { VerificationResultId: '', VerificationResultStatusType: '', Source: '', Remark: '', VerificationResultDocumentPath: '', VerificationResultDocument: '' }, VerificationData: VerificationData, VerifiedById: '', VerificationDate: '' } });

                }
            }
            if ($scope.DEAsParameter.length > 0) {
                for (var j in $scope.DEAsParameter) {

                    var VerificationData = jQuery.parseJSON($scope.DEAsParameter[j].VerificationData);
                    for (var k in $scope.DEAs) {

                        if (VerificationData.FederalDEAInformationID == $scope.DEAs[k].LincensesInfo.FederalDEAInformationID) {
                            $scope.DEAs[k].ProfileVerificationDetail = $scope.DEAsParameter[j];
                        }

                    }
                }
            }
        //}
       
        //$scope.DEAs = { tabId: 'tabId2', info: DEAInfo };
        //========================NPDB=====================================

        if ($scope.pendingPSV == null) {
            $scope.NPDB = { ViewDiv: false, EditDiv: false, AddDiv: true, ProfileVerificationDetail: { ProfileVerificationDetailId: '', ProfileVerificationParameterId: '', VerificationResult: { VerificationResultId: '', VerificationResultStatusType: '', Source: '', Remark: '', VerificationResultDocumentPath: '', VerificationResultDocument: '' } } };
        } else {
            $scope.NPDB = { ViewDiv: true, EditDiv: false, AddDiv: false, ProfileVerificationDetail: { ProfileVerificationDetailId: '', ProfileVerificationParameterId: '', VerificationResult: { VerificationResultId: '', VerificationResultStatusType: '', Source: '', Remark: '', VerificationResultDocumentPath: '', VerificationResultDocument: '' } } };

        }
        $scope.NPDB.ProfileVerificationDetail = $scope.NPDBsParameter;
        //========================MedicareOPT=====================================
        if ($scope.pendingPSV == null) {
            $scope.MedicareOPT = { ViewDiv: false, EditDiv: false, AddDiv: true, ProfileVerificationDetail: { ProfileVerificationDetailId: '', ProfileVerificationParameterId: '', VerificationResult: { VerificationResultId: '', VerificationResultStatusType: '', Source: '', Remark: '', VerificationResultDocumentPath: '', VerificationResultDocument: '' } } };
        } else {
            $scope.MedicareOPT = { ViewDiv: true, EditDiv: false, AddDiv: false, ProfileVerificationDetail: { ProfileVerificationDetailId: '', ProfileVerificationParameterId: '', VerificationResult: { VerificationResultId: '', VerificationResultStatusType: '', Source: '', Remark: '', VerificationResultDocumentPath: '', VerificationResultDocument: '' } } };

        }
        $scope.MedicareOPT.ProfileVerificationDetail = $scope.MedicareOPTsParameter;

        //========================OIG=====================================
        if ($scope.pendingPSV == null) {
            $scope.OIG = { ViewDiv: false, EditDiv: false, AddDiv: true, ProfileVerificationDetail: { ProfileVerificationDetailId: '', ProfileVerificationParameterId: '', VerificationResult: { VerificationResultId: '', VerificationResultStatusType: '', Source: '', Remark: '', VerificationResultDocumentPath: '', VerificationResultDocument: '' } } };
        } else {
            $scope.OIG = { ViewDiv: true, EditDiv: false, AddDiv: false, ProfileVerificationDetail: { ProfileVerificationDetailId: '', ProfileVerificationParameterId: '', VerificationResult: { VerificationResultId: '', VerificationResultStatusType: '', Source: '', Remark: '', VerificationResultDocumentPath: '', VerificationResultDocument: '' } } };

        }
        $scope.OIG.ProfileVerificationDetail = $scope.OIGsParameter;

    };

    $scope.Load = function (data) {

        $scope.ProfileObj = angular.copy(data);
        $scope.ProfileId = angular.copy(data.Profile.ProfileId);
       
        
        // $scope.GetProfileVerificationParameter(); 

    };
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

        $http.get('/Profile/MasterData/GetAllProfileVerificationParameter').
        success(function (data, status, headers, config) {

            for (var i = 0; i < data.length; i++) {
                if (data[i].Code == 'SL')
                {
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
            $scope.assignParameter();
            $scope.loadObjFormat($scope.ProfileObj);
            
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
});

//--------------- file name Wrap-text author : krglv ---------------
function setFileNameWith(file) {
    $(file).parent().parent().find(".jancyFileWrapTexts").find("span").width($(file).parent().parent().width() - 197);
}