Cred_SPA_App.directive('searchdropdown', function () {
    return {
        restrict: 'AE',
        link: function (scope, element, attr) {
            element.bind('focus', function () {
                element.parent().find(".TemplateSelectAutoList").show();
            });
        }
    };
});


Cred_SPA_App.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

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

//---------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------check in test---------------------------------------------------------------
//---------------------------------------------------------------------------------------------------------------------------

Cred_SPA_App.controller('LoadToPlanController', function ($scope, $rootScope, $http, $filter, messageAlertEngine) {

    $http.get(rootDir + '../../../EmailService/GetAllEmailTemplates').
       success(function (data, status, headers, config) {

           if (data != null) {
               $scope.templates = data;
           }
           for (var i = 0; i < $scope.templates.length; i++) {
               $scope.templates[i].LastModifiedDate = $scope.ConvertDateFormat($scope.templates[i].LastModifiedDate);
           }

       }).
       error(function (data, status, headers, config) {
           //console.log("Sorry internal master data cont able to fetch.");
       });

    $scope.hideDiv = function () {
        $('.TemplateSelectAutoList').hide();
        $scope.errorMsg = false;
    }

    $scope.GenerateEmailPopUp = function (credReqID) {
        $scope.tempObject.CredReqID = credReqID;
    }

    $scope.EditCancle = function (temp) {
        //ResetFormForValidation($("#newEmailForm"));
        if ($scope.data != null && temp != null) {
            for (var c = 0; c < $scope.data.length; c++) {
                if ($scope.data[c].EmailTemplateID == temp.EmailTemplateID) {
                    $scope.data[c].EditStatus = false;
                    //$scope.tableParams.reload();
                    break;
                }
            }
        }
        $scope.compose = false;
        $scope.tempObject.To = "";
        $scope.tempObject.CC = "";
        $scope.tempObject.BCC = "";
        $scope.tempObject.Subject = "";
        $scope.tempObject.Body = "";
        $scope.tempObject.IsRecurrenceEnabledYesNoOption = "";
        $scope.tempObject.RecurrenceIntervalTypeCategory = "";
        $scope.tempObject.FromDate = "";
        $scope.tempObject.ToDate = "";
        $scope.tempObject.IntervalFactor = "";
    }

    $scope.AddOrSaveEmail = function (Form_Id) {
        ResetFormForValidation($("#" + Form_Id));
        if ($("#" + Form_Id).valid()) {

            var ltcharCheck = true;
            var gtcharCheck = true;
            while (gtcharCheck == true || ltcharCheck == true) {
                if ($('#Body').val().indexOf('<') > -1) {
                    $('#Body').val($('#Body').val().replace("<", "&lt;"));
                    ltcharCheck = true;
                }
                else {
                    ltcharCheck = false;
                }
                if ($('#Body').val().indexOf('>') > -1) {
                    $('#Body').val($('#Body').val().replace(">", "&gt;"));
                    gtcharCheck = true;
                }
                else {
                    gtcharCheck = false;
                }
            }

            var $form = ($("#" + Form_Id)[0]);
            $.ajax({
                url: rootDir + '/CnD/SendEmail?credRequestId=' + $scope.tempObject.CredReqID,
                type: 'POST',
                data: new FormData($form),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    if (data.status == "true") {
                        $scope.compose = false;
                        //$scope.tableParamsFollowUp.reload();
                        //$scope.tableParamsSent.reload();
                        messageAlertEngine.callAlertMessage('LoadedSuccess', "Email scheduled for sending.", "success", true);
                        //$scope.getTabData('Sent');
                    }
                    else {
                        messageAlertEngine.callAlertMessage('errorMsgDiv', data.status, "danger", true);
                        //$scope.errorMsg = data.status;
                    }
                },
                error: function (data) {
                    messageAlertEngine.callAlertMessage('errorMsgDiv', data.status, "danger", true);
                    //$scope.errorMsg = "Unable to schedule Email.";
                }
            });
            $('#composeMail').hide();
            $scope.tempObject.To = "";
            $scope.tempObject.CC = "";
            $scope.tempObject.BCC = "";
            $scope.tempObject.Subject = "";
            $scope.tempObject.Body = "";
            $scope.tempObject.IsRecurrenceEnabledYesNoOption = "";
            $scope.tempObject.RecurrenceIntervalTypeCategory = "";
            $scope.tempObject.FromDate = "";
            $scope.tempObject.ToDate = "";
        }
    }

    $scope.showContent = function () {
        $scope.templateSelected = true;
    }

    $scope.initActivityStatus = "";
    $scope.ccmStatus = false;
    $scope.credID = sessionStorage.getItem("credentialingInfoId");
    $scope.creddInfo = [];
    $scope.templateList = [{ name: 'A2HC Provider Profile for Wellcare', code: 'A2HC', check: false }, { name: 'AHC Provider Profile for Wellcare', code: 'AHC', check: false }, ];

    $scope.goToNextTab = function (tab) {
        $('.nav-tabs a[href="#' + tab + '"]').tab('show');
    };
    $scope.creddInfo = JSON.parse(credFilterInfo);
    

    //$scope.credInfos = [];
    //$http.get('/Credentialing/Initiation/GetCredInfo?id=' + $scope.credID).
    //   success(function (data, status, headers, config) {
    //       $scope.credInfos = angular.copy(data);
    //       for (var i = 0; i < $scope.credInfos.length; i++) {
    //           if ($scope.credInfos[i].CredentialingInfoID == $scope.credID) {
    //               $scope.creddInfo = angular.copy($scope.credInfos[i]);
    //           }
    //       }
    //       console.log("+++++jhhsdfjhfd++++++");
    //       console.log($scope.creddInfo);
    //       $scope.isCCM();
    //   }).
    //   error(function (data, status, headers, config) {
    //       //console.log("Sorry internal master data cont able to fetch.");
    //   });
    var CredID = JSON.parse(credFilterInfo);
    $scope.showerrorforpackage = false;
    $scope.GetAllPackage = [];
    $scope.PackageList = [];
    var TempPackageList = []
    $scope.frameView = '';
    $scope.$on('package', function (event, args) {
        $scope.GetAllPackage.push(args.Package);
        var d = $scope.PackageList.length + 1;
        args.Package.Name = 'Package ' + d;
        args.Package.Check = false;
        $scope.PackageList.push(args.Package);
        TempPackageList.push(args.Package);
    });
    $http.get(rootDir + '/Credentialing/CnD/GetAllDocuments?profileID=' + CredID.Profile.ProfileID).success(function (data) {
        if (data != null) {
            console.log(data);

            if (data.PackageGenerator != null) {
                for (var c = 0; c < data.PackageGenerator.length; c++) {
                    if (data.PackageGenerator[c].PlanID == CredID.Plan.PlanID) {
                        $scope.GetAllPackage.push(data.PackageGenerator[c]);
                    }
                }
            }
            $scope.PackageList = angular.copy($scope.GetAllPackage);
            for (var c = 0; c < $scope.PackageList.length; c++) {
                var d = c + 1;
                $scope.PackageList[c].Name = 'Package ' + d;
                $scope.PackageList[c].Check = false;
            }
            TempPackageList = angular.copy($scope.PackageList);

        }
    }).error(function () {

    })

    $scope.toggleSelection = function (data) {
        if ($scope.PackageTempList[$scope.PackageTempList.indexOf(data)].Check) {
            for (var c = 0; c < $scope.PackageTempList.length; c++) {
                $scope.PackageTempList[c].Check = false;
            }
            $scope.PackageTempList[$scope.PackageTempList.indexOf(data)].Check = true;
            $scope.frameView = data.PackageFilePath;
            $scope.showerrorforpackage = false;
        }
        else {
            $scope.frameView = '';
        }

    }
    $scope.cancelPackagePanel = function () {
        $scope.PackageTempList = [];
        $scope.PackageTempList = angular.copy(TempPackageList);
        $scope.frameView = '';
        $scope.showerrorforpackage = false;
    };
    $scope.getStatusL = function (data) {
        if (data.CredentialingActivityLogs != null && data.CredentialingActivityLogs.length != 0) {
            if (data.Credentialing == "Credentialing" || data.Credentialing == "ReCredentialing") {
                if (data.CredentialingActivityLogs[data.CredentialingActivityLogs.length - 1].Activity == "Initiation") {
                    if (data.CredentialingActivityLogs[data.CredentialingActivityLogs.length - 1].ActivityStatus == "Completed") {
                        $scope.initActivityStatus = "Initiated";
                    }
                } else if (data.CredentialingActivityLogs[data.CredentialingActivityLogs.length - 1].Activity == "PSV") {
                    if (data.CredentialingActivityLogs[data.CredentialingActivityLogs.length - 1].ActivityStatus == "Completed") {
                        $scope.initActivityStatus = "Verified";
                    } else {
                        $scope.initActivityStatus = "Initiated";
                    }
                } else if (data.CredentialingActivityLogs[data.CredentialingActivityLogs.length - 1].Activity == "CCMAppointment") {
                    if (data.CredentialingActivityLogs[data.CredentialingActivityLogs.length - 1].ActivityStatus == "Completed") {
                        $scope.initActivityStatus = "CCM";
                    } else {
                        $scope.initActivityStatus = "Verified";
                    }
                } else if (data.CredentialingActivityLogs[data.CredentialingActivityLogs.length - 1].Activity == "Loading") {
                    if (data.CredentialingActivityLogs[data.CredentialingActivityLogs.length - 1].ActivityStatus == "Completed") {
                        $scope.initActivityStatus = "Submitted";
                    } else {
                        $scope.initActivityStatus = "CCM";
                    }
                } else if (data.CredentialingActivityLogs[data.CredentialingActivityLogs.length - 1].Activity == "Report") {
                    if (data.CredentialingActivityLogs[data.CredentialingActivityLogs.length - 1].ActivityStatus == "Completed") {
                        $scope.initActivityStatus = "Completed";
                    } else {
                        $scope.initActivityStatus = "Submitted";
                    }
                }
            }
            else {
                if (data.CredentialingActivityLogs[data.CredentialingActivityLogs.length - 1].Activity == "Dropped") {
                    if (data.CredentialingActivityLogs[data.CredentialingActivityLogs.length - 1].ActivityStatus == "Completed") {
                        $scope.initActivityStatus = "Submitted";
                    }
                }
            }
        }
    }

    $scope.isCCM = function () {
        //console.log('abhiabhi');
        //console.log($scope.creddInfo);
        //alert($scope.creddInfo);
        //console.log($scope.creddInfo.CredentialingLogs[0]);
        //alert($scope.creddInfo.length);
        //if ($scope.creddInfo.length == 0) {
        //    $('#ccmtab').removeClass('active');
        //    $('#credentialing_action1').removeClass('active');
        //    $('#credentialing_action').removeClass('active');
        //    $('#psvtab').removeClass('active');
        //    $('#psv').removeClass('active');
        //    $('#summary2').addClass('active');
        //    $('#SUMMARY').addClass('active');
        //}
        var CredlogDataL = "";
        var flagL = 0;
        for (var c = 0; c < $scope.creddInfo.CredentialingLogs.length; c++) {
            if ($scope.creddInfo.CredentialingLogs[c].Credentialing == "Dropped") {
                CredlogDataL = $scope.creddInfo.CredentialingLogs[c];
                flagL = 1;
                break;
            }
        }
        if (flagL == 0) {
            for (var c = 0; c < $scope.creddInfo.CredentialingLogs.length; c++) {
                if ($scope.creddInfo.CredentialingLogs[c].Credentialing == "Credentialing" || $scope.creddInfo.CredentialingLogs[c].Credentialing == "ReCredentialing") {
                    CredlogDataL = $scope.creddInfo.CredentialingLogs[c];
                    flagL = 1;
                    break;
                }
            }
        }

        $scope.getStatusL(CredlogDataL);
        //console.log('abhi');
        //console.log($scope.initActivityStatus);
        //console.log($scope.creddInfo.Plan.DelegatedType);
        //console.log($scope.credID);

        if (($scope.initActivityStatus == "Initiated") && $scope.creddInfo.Plan.DelegatedType == "YES") {
            $('#summary2').removeClass('active');
            $('#credentialing_action').removeClass('active');
            $('#credentialing_action1').removeClass('active');
            $('#psvtab').addClass('active');
            $('#SUMMARY').removeClass('active');
            $('#psv').addClass('active');
        } else if ($scope.initActivityStatus == "Verified" && $scope.creddInfo.Plan.DelegatedType == "YES") {
            $('#summary2').removeClass('active');
            $('#credentialing_action1').addClass('active');
            $('#psvtab').removeClass('active');
            $('#ccmtab').addClass('active');
            $('#SUMMARY').removeClass('active');
            $('#psv').removeClass('active');
            $('#credentialing_action').addClass('active');
        } else if ($scope.initActivityStatus == "CCM" && $scope.creddInfo.Plan.DelegatedType == "YES" && $scope.credentialingFilterInfo.CredentialingLogs[$scope.credentialingFilterInfo.CredentialingLogs.length - 1].CredentialingAppointmentDetail.CredentialingAppointmentResult.ApprovalStatus != 'Rejected') {
            $('#summary2').removeClass('active');
            $('#psvtab').removeClass('active');
            $('#ccmtab').removeClass('active');
            $('#doctab').removeClass('active');
            $('#load_to_plan_tab').addClass('active');
            $('#SUMMARY').removeClass('active');
            $('#psv').removeClass('active');
            $('#credentialing_action').removeClass('active');
            $('#credentialing_action1').removeClass('active');
            $('#print').removeClass('active');
            $('#load_to_plan').addClass('active');
        } else if ($scope.initActivityStatus == "CCM" && $scope.creddInfo.Plan.DelegatedType == "YES" && $scope.credentialingFilterInfo.CredentialingLogs[$scope.credentialingFilterInfo.CredentialingLogs.length - 1].CredentialingAppointmentDetail.CredentialingAppointmentResult.ApprovalStatus == 'Rejected') {
            $('#summary2').removeClass('active');
            $('#psvtab').removeClass('active');
            $('#ccmtab').addClass('active');
            $('#SUMMARY').removeClass('active');
            $('#psv').removeClass('active');
            $('#credentialing_action').addClass('active');
            $('#credentialing_action1').addClass('active');
        } else if ($scope.initActivityStatus == "Submitted" || $scope.initActivityStatus == "Completed" || $scope.creddInfo.Plan.DelegatedType == "YES") {
            
            $('#ccmtab').removeClass('active');
            $('#credentialing_action1').removeClass('active');
            $('#credentialing_action').removeClass('active');
            $('#psvtab').removeClass('active');
            $('#psv').removeClass('active');
            $('#summary2').addClass('active');
            $('#SUMMARY').addClass('active');
        }
        //console.log("$scope.credInfo");
        //console.log($scope.credentialingFilterInfo);
        if ($scope.credentialingFilterInfo.CredentialingLogs[$scope.credentialingFilterInfo.CredentialingLogs.length - 1].CredentialingAppointmentDetail != null) {
            if (($scope.initActivityStatus == "Initiated" || $scope.initActivityStatus == "Verified" || $scope.credentialingFilterInfo.CredentialingLogs[$scope.credentialingFilterInfo.CredentialingLogs.length - 1].CredentialingAppointmentDetail.CredentialingAppointmentResult.ApprovalStatus == 'Rejected') && $scope.creddInfo.Plan.DelegatedType == "YES") {
                $scope.ccmStatus = true;
            }
        }
        //console.log('ccm stat');
        //console.log($scope.ccmStatus);
    }
    $scope.isCCM();
    console.log("Package Report");
    console.log($scope.credentialingInfo);

    $scope.ShowVisibility = '';
    $scope.LoadedData = [];
    if ($scope.credentialingInfo != null && $scope.credentialingInfo.CredentialingContractRequests != null) {
        for (var i = 0; i < $scope.credentialingInfo.CredentialingContractRequests.length; i++) {
            if ($scope.credentialingInfo.CredentialingContractRequests[i].Status != 'Inactive') {
                $scope.LoadedData.push($scope.credentialingInfo.CredentialingContractRequests[i]);
            }
        }
    }
    var TabStatusLTP1 = $scope.LoadedData.length > 0 ? true : false;
    $rootScope.$broadcast('LTPStatus', { StatusLTP: TabStatusLTP1 });
    if ($scope.LoadedData != null && $scope.LoadedData.length != 0) {

        for (var i = 0; i < $scope.LoadedData.length; i++) {

            if ($scope.LoadedData[i].BusinessEntity != null) {

                $scope.LoadedData[i].GroupName = $scope.LoadedData[i].BusinessEntity.GroupName;

            }

            if ($scope.LoadedData[i].ContractLOBs != null && $scope.LoadedData[i].ContractLOBs.length != 0) {

                for (var j = 0; j < $scope.LoadedData[i].ContractLOBs.length; j++) {
                    if ($scope.LoadedData[i].LOBName == undefined) {
                        $scope.LoadedData[i].LOBName = '';

                        if ($scope.LoadedData[i].ContractLOBs[j].LOB != null) {

                            $scope.LoadedData[i].LOBName = $scope.LoadedData[i].ContractLOBs[j].LOB.LOBName;

                        }
                    }
                    else {

                        if ($scope.LoadedData[i].ContractLOBs[j].LOB != null) {

                            $scope.LoadedData[i].LOBName = $scope.LoadedData[i].LOBName + ", " + $scope.LoadedData[i].ContractLOBs[j].LOB.LOBName;

                        }

                    }

                }

            }

            if ($scope.LoadedData[i].ContractSpecialties != null && $scope.LoadedData[i].ContractSpecialties.length != 0) {

                for (var k = 0; k < $scope.LoadedData[i].ContractSpecialties.length; k++) {
                    if ($scope.LoadedData[i].SpecialtyName == undefined) {
                        $scope.LoadedData[i].SpecialtyName = '';

                        if ($scope.LoadedData[i].ContractSpecialties[k].ProfileSpecialty != null && $scope.LoadedData[i].ContractSpecialties[k].ProfileSpecialty.Specialty != null) {

                            $scope.LoadedData[i].SpecialtyName = $scope.LoadedData[i].ContractSpecialties[k].ProfileSpecialty.Specialty.Name;

                        }

                    }
                    else {

                        if ($scope.LoadedData[i].ContractSpecialties[k].ProfileSpecialty != null && $scope.LoadedData[i].ContractSpecialties[k].ProfileSpecialty.Specialty != null) {

                            $scope.LoadedData[i].SpecialtyName = $scope.LoadedData[i].SpecialtyName + ', ' + $scope.LoadedData[i].ContractSpecialties[k].ProfileSpecialty.Specialty.Name;

                        }

                    }
                }

            }

            if ($scope.LoadedData[i].ContractPracticeLocations != null && $scope.LoadedData[i].ContractPracticeLocations.length != 0) {

                for (var l = 0; l < $scope.LoadedData[i].ContractPracticeLocations.length; l++) {
                    if ($scope.LoadedData[i].FacilityName == undefined) {
                        $scope.LoadedData[i].FacilityName = '';

                        if ($scope.LoadedData[i].ContractPracticeLocations[l].ProfilePracticeLocation != null && $scope.LoadedData[i].ContractPracticeLocations[l].ProfilePracticeLocation.Facility != null) {

                            $scope.LoadedData[i].FacilityName = $scope.LoadedData[i].ContractPracticeLocations[l].ProfilePracticeLocation.Facility.FacilityName;

                        }

                    }
                    else {

                        if ($scope.LoadedData[i].ContractPracticeLocations[l].ProfilePracticeLocation != null && $scope.LoadedData[i].ContractPracticeLocations[l].ProfilePracticeLocation.Facility != null) {

                            $scope.LoadedData[i].FacilityName = $scope.LoadedData[i].FacilityName + ', ' + $scope.LoadedData[i].ContractPracticeLocations[l].ProfilePracticeLocation.Facility.FacilityName;

                        }

                    }
                }

            }

        }

    }

    //console.log('credinfo');
    //console.log($scope.LoadedData);
    //console.log("profile");
    //console.log($scope.credentialingInfo.ProfileID);
    sessionStorage.setItem('ProfileID', $scope.credentialingInfo.ProfileID);

    $scope.ClearVisibility = function () {
        $scope.ShowVisibility = '';
    }
    //console.log("Kadu");
    //console.log($scope.PlanReportList);

    $scope.SetVisibility = function (type, index) {
        if (type == 'Qedit') {
            $scope.qTempObject = angular.copy($scope.PlanReportList[index]);
            $scope.ShowVisibility = 'QeditVisibility' + index;
        }
        else if (type == 'edit') {
            $scope.eTempObject = angular.copy($scope.PlanReportList[index]);
            if ($scope.eTempObject.InitialCredentialingDate != null) {
                if ($scope.eTempObject.Report.TerminationDate == '' || $scope.eTempObject.Report.TerminationDate == null) {
                    // $scope.eTempObject.Report.TerminationDate = $scope.ConvertDateBy3Years($scope.eTempObject.InitialCredentialingDate);
                }
                if ($scope.eTempObject.Report.ReCredentialingDate == '' || $scope.eTempObject.Report.ReCredentialingDate == null) {
                    $scope.eTempObject.Report.ReCredentialingDate = $scope.ConvertDateBy3Years($scope.eTempObject.InitialCredentialingDate);
                }
            }
            else {
                $scope.eTempObject.Report.ReCredentialingDate = null;
            }
            $scope.ShowVisibility = 'editVisibility' + index;
        }
        else if (type == 'view') {
            $scope.ShowVisibility = 'viewVisibility' + index;
        }
    };
    $scope.tempID = 0;
    $scope.AddPackageToContractRequest = function (temp) {
        $scope.showerrorforpackage = false;
        $scope.ContractRequest = temp;
        $scope.PackageTempList = angular.copy($scope.PackageList);
        var temporary = [];
        if ($scope.ContractRequest.PackageGeneratorReport != null) {
            for (var c = 0; c < $scope.ContractRequest.PackageGeneratorReport.PackageGeneratorReportCode.length; c++) {
                for (var d = 0; d < $scope.PackageTempList.length; d++) {
                    if ($scope.PackageTempList[d].PackageGeneratorID == $scope.ContractRequest.PackageGeneratorReport.PackageGeneratorReportCode[c].ID) {
                        temporary.push($scope.PackageTempList[d]);
                    }
                }
            }
        }
        for (var c = 0; c < temporary.length; c++) {
            $scope.PackageTempList.splice($scope.PackageTempList.indexOf(temporary[c]), 1)
        }
    }

    //$scope.L = [{ "id": 2, "p": 4 }{ "id": 3, "p": 5 }]
    //console.log("L");
    //alert(JSON.stringify($scope.L));
    for (var c = 0; c < $scope.LoadedData.length; c++) {
        if ($scope.LoadedData[c].PackageGeneratorReport != null) {

            $scope.LoadedData[c].PackageGeneratorReport.PackageGeneratorReportCode = JSON.parse($scope.LoadedData[c].PackageGeneratorReport.PackageGeneratorReportCode);
        }
    }
    //var a = [];
    ////a = JSON.parse('[' + $scope.LoadedData[0].PackageGeneratorReport.PackageGeneratorReportCode + ']');
    //console.log("LOADED DATA");
    //console.log(a);
    $scope.AddPackage = function () {
        var flag = 0;
        var tempData = [];
        for (var c = 0; c < $scope.PackageTempList.length; c++) {
            if ($scope.PackageTempList[c].Check == true) {
                flag = 1;
                for (var d = 0; d < $scope.GetAllPackage.length; d++) {
                    if ($scope.GetAllPackage[d].PackageGeneratorID == $scope.PackageTempList[c].PackageGeneratorID) {
                        console.log("LOADED DATA");
                        console.log($scope.LoadedData);
                        if ($scope.LoadedData[$scope.LoadedData.indexOf($scope.ContractRequest)].PackageGeneratorReport == null) {
                            tempData.push({ ID: $scope.GetAllPackage[d].PackageGeneratorID, FilePath: $scope.GetAllPackage[d].PackageFilePath });
                        }
                        else {
                            if ($scope.LoadedData[$scope.LoadedData.indexOf($scope.ContractRequest)].PackageGeneratorReport.PackageGeneratorReportCode == null) {
                                tempData.push({ ID: $scope.GetAllPackage[d].PackageGeneratorID, FilePath: $scope.GetAllPackage[d].PackageFilePath });
                            }
                            else {
                                tempData = angular.copy($scope.LoadedData[$scope.LoadedData.indexOf($scope.ContractRequest)].PackageGeneratorReport.PackageGeneratorReportCode);
                                tempData.push({ ID: $scope.GetAllPackage[d].PackageGeneratorID, FilePath: $scope.GetAllPackage[d].PackageFilePath });
                            }
                        }
                        break;
                    }
                }
                break;
            }
        }
        if (flag == 1) {
            $http({
                method: "POST",
                url: rootDir + '/Credentialing/CnD/AddPackageToContractRequest',
                params: {
                    ContractRequestID: $scope.ContractRequest.CredentialingContractRequestID,
                    PackageGeneratorReportCode: JSON.stringify(tempData)
                },
            }).success(function (data, status, headers, config) {
                if (data != null) {
                    //console.log(data);
                    //alert(JSON.parse(data.PackageGeneratorReportCode));
                    data.PackageGeneratorReportCode = JSON.parse(data.PackageGeneratorReportCode);
                    for (var c = 0; c < $scope.LoadedData.length; c++) {
                        if ($scope.LoadedData[c].CredentialingContractRequestID == $scope.ContractRequest.CredentialingContractRequestID) {
                            if ($scope.LoadedData[c].PackageGeneratorReport == null) {
                                $scope.LoadedData[c].PackageGeneratorReport = data;
                            }
                            else {
                                $scope.LoadedData[c].PackageGeneratorReport = data;
                            }
                        }
                    }


                    console.log("LOADED DATA");
                    console.log($scope.LoadedData);
                }
                else {

                }
                $('#selectPackage').modal('hide');
            }).error(function (data, status, headers, config) {

            })
        }
        else {
            $scope.showerrorforpackage = true;
        }
    }
    //Convert the date from database to normal
    $rootScope.ConvertDateFormat = function (value) {
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



    //Loaded object initialising
    $scope.tempObject =
        {
            BusinessEntity: "",
            ContractLOBs: [],
            ContractSpecialties: [],
            ContractPracticeLocations: [],
            AllSpecialtiesSelectedYesNoOption: 0,
            AllLOBsSelectedYesNoOption: 0,
            AllPracticeLocationsSelectedYesNoOption: 0,
        };

    $scope.MasterTemp = angular.copy($scope.tempObject);

    $scope.GetValue = function (data) {
        //  console.log(data);
    }

    //multi select
    $scope.showContryCodeDiv = function (div_Id) {
        $("#LOBdiv").hide();
        $("#Specialitydiv").hide();
        $("#Locationsdiv").hide();
        $("#" + div_Id).show();
    };

    // $http.get('/MasterDataNew/GetAllLobs').
    //success(function (data, status, headers, config) {
    //    $scope.ContractLOBsList = angular.copy(data);
    //    $scope.MasterContractLOBslist = data;
    //}).
    //error(function (data, status, headers, config) {
    //    console.log("Sorry internal master data cont able to fetch.");
    //});

    $http.get(rootDir + '/MasterDataNew/GetAllLOBsOfPlanContractByPlanID?planID=' + $scope.credentialingInfo.PlanID).
   success(function (data, status, headers, config) {
       $scope.ContractLOBsList = angular.copy(data);
       $scope.MasterContractLOBslist = data;
   }).
   error(function (data, status, headers, config) {
       //console.log("Sorry internal master data cont able to fetch.");
   });

    //console.log($scope.credentialingInfo);
    $scope.ContractSpecialityList = [];

    if ($scope.credentialingInfo.Profile != null && $scope.credentialingInfo.Profile.SpecialtyDetails != null && $scope.credentialingInfo.Profile.SpecialtyDetails.length != 0) {

        for (var i = 0; i < $scope.credentialingInfo.Profile.SpecialtyDetails.length; i++) {
            if ($scope.credentialingInfo.Profile.SpecialtyDetails[i].Status != 'Inactive') {
                $scope.ContractSpecialityList.push($scope.credentialingInfo.Profile.SpecialtyDetails[i]);
            }
        }

    }
    //$scope.ContractSpecialityList = angular.copy($scope.credentialingInfo.Profile.SpecialtyDetails);
    $scope.ContractLocationsList = [];

    if ($scope.credentialingInfo.Profile != null && $scope.credentialingInfo.Profile.PracticeLocationDetails != null && $scope.credentialingInfo.Profile.PracticeLocationDetails.length != 0) {

        for (var i = 0; i < $scope.credentialingInfo.Profile.PracticeLocationDetails.length; i++) {
            if ($scope.credentialingInfo.Profile.PracticeLocationDetails[i].Status != 'Inactive') {
                $scope.ContractLocationsList.push($scope.credentialingInfo.Profile.PracticeLocationDetails[i]);
            }
        }

    }
    //console.log("habala hubala habala hubala");
    //console.log($scope.ContractLocationsList);
    //$scope.ContractLocationsList = angular.copy($scope.credentialingInfo.Profile.PracticeLocationDetails);

    $http.get(rootDir + '/MasterDataNew/GetAllOrganizationGroupAsync').
    success(function (data, status, headers, config) {
        $scope.BusinessEntities = angular.copy(data);
        $scope.MasterBusinessEntities = data;
    }).
    error(function (data, status, headers, config) {
        //console.log("Sorry internal master data cont able to fetch.");
    });

    //LOB
    $scope.ContractLOBsList = [];
    $scope.tempObject.ContractLOBs = [];
    $scope.SelectLOBName = function (c, div) {

        if (c != null) {

            $scope.tempObject.ContractLOBs.push({
                LOBID: c.LOBID,
                LOBName: c.LOBName
            });
            $scope.ContractLOBsList.splice($scope.ContractLOBsList.indexOf(c), 1);
        }
        $scope.LOBName = "";
        $("#" + div).hide();
    };
    $scope.RemoveCoveringPhysiciansType = function (c) {

        if (c != null) {

            $scope.tempObject.ContractLOBs.splice($scope.tempObject.ContractLOBs.indexOf(c), 1)
            $scope.ContractLOBsList.push(c);
        }
    };

    // Splty
    $scope.tempObject.ContractSpecialties = [];
    $scope.SelectSpecialityName = function (c, div) {

        if (c != null) {

            $scope.tempObject.ContractSpecialties.push({
                ProfileSpecialtyID: c.SpecialtyDetailID,
                SpecialtyName: c.Specialty.Name
            });
            $scope.ContractSpecialityList.splice($scope.ContractSpecialityList.indexOf(c), 1);

        }

        $scope.SpecialtyName = "";
        $("#" + div).hide();
    };
    $scope.RemoveContractSpecialties = function (c) {

        if (c != null) {

            $scope.tempObject.ContractSpecialties.splice($scope.tempObject.ContractSpecialties.indexOf(c), 1)
            c.Specialty = {};
            c.Specialty.Name = c.SpecialtyName;
            $scope.ContractSpecialityList.push(c);

        }
    };

    // Loc
    $scope.tempObject.ContractPracticeLocations = [];
    $scope.SelectLocationsName = function (c, div) {

        if (c != null) {

            $scope.tempObject.ContractPracticeLocations.push({
                ProfilePracticeLocationID: c.PracticeLocationDetailID,
                LocationsName: c.Facility.FacilityName,
                StreetName: c.Facility.Street,
                CityName: c.Facility.City,
                StateName: c.Facility.State,
                CountryName: c.Facility.Country
            });
            $scope.ContractLocationsList.splice($scope.ContractLocationsList.indexOf(c), 1);

        }
        $scope.LocationsName = "";
        $("#" + div).hide();

    };
    $scope.RemoveContractLocations = function (c) {

        if (c != null) {

            $scope.tempObject.ContractPracticeLocations.splice($scope.tempObject.ContractPracticeLocations.indexOf(c), 1)
            c.Facility = {};
            c.Facility.FacilityName = c.LocationsName;
            c.Facility.Street = c.StreetName;
            c.Facility.City = c.CityName;
            c.Facility.State = c.StateName;
            c.Facility.Country = c.CountryName;
            $scope.ContractLocationsList.push(c);

        }
    };

    $scope.isHasError = false;

    //console.log("data" + $scope.tempObject.ContractLOBs);

    //$scope.LoadedData = [
    //    BusinessEntity = "",
    //    LOBName = [],
    //    SpecialtyName = [],
    //    PLName = [],
    //];
    //for (var i = 0; i < $scope.credentialingInfo.CredentialingContractRequests.length; i++) {
    //    $scope.LoadedData[i].BusinessEntity = $scope.credentialingInfo.CredentialingContractRequests[i].BusinessEntity.GroupName;
    //    for (var j = 0; j < $scope.credentialingInfo.CredentialingContractRequests[i].ContractLOBs.length; j++) {
    //        $scope.LoadedData[i].LOBName.push($scope.credentialingInfo.CredentialingContractRequests[i].ContractLOBs[j].LOB.LOBName);
    //    }
    //    for (var j = 0; j < $scope.credentialingInfo.CredentialingContractRequests[i].ContractPracticeLocations.length; j++) {
    //        $scope.LoadedData[i].PLName.push($scope.credentialingInfo.CredentialingContractRequests[i].ContractPracticeLocations[j].ProfilePracticeLocation.Facility.FacilityName);
    //    }
    //    for (var j = 0; j < $scope.credentialingInfo.CredentialingContractRequests[i].ContractSpecialties.length; j++) {
    //        $scope.LoadedData[i].SpecialtyName.push($scope.credentialingInfo.CredentialingContractRequests[i].ContractSpecialties[j].ProfileSpecialty.Specialty.Name);
    //    }
    //}
    //console.log($scope.LoadedData);
    $scope.tempSecObject = {
        ContractGridID: 0,
        InitialCredentialingDate: new Date,
        Report: {},
    };

    $scope.validityForQuickSave = function (c, index) {
        $scope.validRequired = true;
        $scope.validFormat = true;
    };

    //Quick Save
    $scope.QuickSave = function (c, index) {

        if (c != null) {

            $scope.validRequired = true;
            $scope.validFormat = true;
            $scope.tempSecObject.ContractGridID = c.ContractGridID;
            $scope.tempSecObject.InitialCredentialingDate = angular.copy(c.InitialCredentialingDate);
            $scope.tempSecObject.Report.ProviderID = c.Report.ProviderID;
            $scope.tempSecObject.Report.CredentialingContractInfoFromPlanID = c.Report.CredentialingContractInfoFromPlanID;

        }

        if ($('#dataContainer' + index).find('#InitialCredentialingDate').val() == '') {
            $scope.validRequired = false;
        }
        if ($('#dataContainer' + index).find('#InitialCredentialingDate').val() != '' && typeof ($scope.tempSecObject.InitialCredentialingDate) == 'undefined') {
            $scope.validFormat = false;
        }

        if ($scope.validFormat == true && $scope.validRequired == true) {
            $http.post(rootDir + '/Credentialing/CnD/QuickSaveReport', $scope.tempSecObject).
               success(function (data, status, headers, config) {
                   data.dataContractGrid.InitialCredentialingDate = $rootScope.ConvertDateFormat(data.dataContractGrid.InitialCredentialingDate);
                   if (data.dataContractGrid.Report != null) {
                       data.dataContractGrid.Report.CredentialedDate = $rootScope.ConvertDateFormat(data.dataContractGrid.Report.CredentialedDate);
                       data.dataContractGrid.Report.InitiatedDate = $rootScope.ConvertDateFormat(data.dataContractGrid.Report.InitiatedDate);
                       data.dataContractGrid.Report.TerminationDate = $rootScope.ConvertDateFormat(data.dataContractGrid.Report.TerminationDate);
                       data.dataContractGrid.Report.ReCredentialingDate = $rootScope.ConvertDateFormat(data.dataContractGrid.Report.ReCredentialingDate);
                   }
                   $scope.PlanReportList[index] = angular.copy(data.dataContractGrid);
                   messageAlertEngine.callAlertMessage('ReportSaveSuccess' + index, "Plan Report Updated Successfully !!!", "success", true);
                   $scope.SetVisibility('view', index);
               }).
               error(function (data, status, headers, config) {
                   //console.log("Sorry internal master data cont able to fetch.");
               });
            $scope.ClearVisibility();
        }
    };

    $scope.ConvertDateBy3Years = function (date) {
        var dt = new Date(date);
        var month = dt.getMonth() + 1;
        var monthString = month > 9 ? month : '0' + month;
        //var monthName = monthNames[month];
        var day = dt.getDate();
        var dayString = day > 9 ? day : '0' + day;
        var year = dt.getFullYear() + 3;
        shortDate = monthString + '/' + dayString + '/' + year;
        return shortDate;
    }
    $scope.ShowDetailTable = function (tempObject) {
        $scope.isHasError = false;
        $scope.tempObject.BusinessEntity = $('#BEID').find($("[name='BE'] option:selected")).text();
        for (var c = 0; c < $scope.credentialingInfo.CredentialingLogs.length; c++) {
            if ($scope.credentialingInfo.CredentialingLogs[c].Credentialing == "Credentialing" || $scope.credentialingInfo.CredentialingLogs[c].Credentialing == "ReCredentialing") {
                $scope.Log = $scope.credentialingInfo.CredentialingLogs[c];
                break;
            }
        }
        if ($scope.credentialingInfo.CredentialingLogs != null) {
            if ($scope.Log.CredentialingAppointmentDetail != null) {
                if ($scope.Log.CredentialingAppointmentDetail.CredentialingAppointmentSchedule != null) {
                    $scope.LoadedData.InitialCredentialingDate = $scope.Log.CredentialingAppointmentDetail.CredentialingAppointmentSchedule.AppointmentDate;
                }
            }
            else {
                $scope.LoadedData.InitialCredentialingDate = null;
            }
        }

        $scope.tempObject.InitialCredentialingDate = $scope.LoadedData.InitialCredentialingDate;


        $('#LoadPlan').show();
        if ($scope.tempObject.BusinessEntityID == null || $scope.tempObject.ContractPracticeLocations.length == 0 || $scope.tempObject.ContractSpecialties.length == 0 || $scope.tempObject.ContractLOBs.length == 0) {
            $scope.isHasError = true;
        }
        $rootScope.ccmstat = false;
        if ($scope.isHasError == false) {
            $scope.loadingAjax = true;
            $http.post(rootDir + '/Credentialing/CnD/AddLoadedData?credentialingInfoID=' + credId, tempObject).
            success(function (data, status, headers, config) {
                if (data.status == 'true') {
                    $scope.loadingAjax = false;
                    $rootScope.isLoaded = true;
                    $scope.ContractSpecialityList = [];
                    $scope.ContractLocationsList = [];
                    $scope.tempContractSpecialityList = angular.copy($scope.credentialingInfo.Profile.SpecialtyDetails);
                    for (var i = 0; i < $scope.tempContractSpecialityList.length; i++) {
                        if ($scope.tempContractSpecialityList[i].Status != 'Inactive') {
                            $scope.ContractSpecialityList.push($scope.tempContractSpecialityList[i]);
                        }
                    }
                    $scope.tempContractLocationsList = angular.copy($scope.credentialingInfo.Profile.PracticeLocationDetails);
                    for (var i = 0; i < $scope.tempContractLocationsList.length; i++) {
                        if ($scope.tempContractLocationsList[i].Status != 'Inactive') {
                            $scope.ContractLocationsList.push($scope.tempContractLocationsList[i]);
                        }
                    }
                    $scope.BusinessEntities = angular.copy($scope.MasterBusinessEntities);
                    $scope.ContractLOBsList = angular.copy($scope.MasterContractLOBslist);
                    $scope.tempObject = angular.copy($scope.MasterTemp);
                    if (data.dataCredentialingContractRequest.InitialCredentialingDate != null) {
                        var date = $rootScope.ConvertDateFormat(data.dataCredentialingContractRequest.InitialCredentialingDate);
                        data.dataCredentialingContractRequest.InitialCredentialingDate = $filter('date')(new Date(date), 'yyyy-MM-dd');
                    } else {
                        data.dataCredentialingContractRequest.InitialCredentialingDate = $rootScope.ConvertDateFormat(data.dataCredentialingContractRequest.InitialCredentialingDate);
                    }

                    for (var i = 0; i < data.dataCredentialingContractRequest.ContractSpecialties.length; i++) {
                        if (data.dataCredentialingContractRequest.ContractSpecialties[i].ProfileSpecialty.SpecialtyBoardCertifiedDetail != null) {
                            if (data.dataCredentialingContractRequest.ContractSpecialties[i].ProfileSpecialty.SpecialtyBoardCertifiedDetail.InitialCertificationDate != null) {
                                data.dataCredentialingContractRequest.ContractSpecialties[i].ProfileSpecialty.SpecialtyBoardCertifiedDetail.InitialCertificationDate = $rootScope.ConvertDateFormat(data.dataCredentialingContractRequest.ContractSpecialties[i].ProfileSpecialty.SpecialtyBoardCertifiedDetail.InitialCertificationDate);
                            }
                            if (data.dataCredentialingContractRequest.ContractSpecialties[i].ProfileSpecialty.SpecialtyBoardCertifiedDetail.ExpirationDate != null) {
                                data.dataCredentialingContractRequest.ContractSpecialties[i].ProfileSpecialty.SpecialtyBoardCertifiedDetail.ExpirationDate = $rootScope.ConvertDateFormat(data.dataCredentialingContractRequest.ContractSpecialties[i].ProfileSpecialty.SpecialtyBoardCertifiedDetail.ExpirationDate);
                            }
                        }


                    }


                    $scope.LoadedData.push(data.dataCredentialingContractRequest);
                    var TabStatusLTP2 = $scope.LoadedData.length > 0 ? true : false;
                    $scope.$broadcast('LTPStatus', { StatusLTP: TabStatusLTP2 });
                    $scope.PlanReportTabStatus = true;
                    //$scope.init_table();

                    for (var i = 0; i < data.dataCredentialingContractRequest.ContractGrid.length; i++) {
                        if (data.dataCredentialingContractRequest.ContractGrid[i].Report == null) {
                            data.dataCredentialingContractRequest.ContractGrid[i].Report = {};
                        }
                        data.dataCredentialingContractRequest.ContractGrid[i].InitialCredentialingDate = $rootScope.ConvertDateFormat(data.dataCredentialingContractRequest.InitialCredentialingDate);
                        //data.dataCredentialingContractRequest.ContractGrid[i].Report.TerminationDate = $scope.ConvertDateBy3Years($scope.LoadedData.InitialCredentialingDate);
                        data.dataCredentialingContractRequest.ContractGrid[i].Report.ReCredentialingDate = $scope.ConvertDateBy3Years($scope.LoadedData.InitialCredentialingDate);
                        $scope.PlanReportList.push(data.dataCredentialingContractRequest.ContractGrid[i]);
                    }
                    //console.log("Plan report");
                    //console.log($scope.PlanReportList);


                    messageAlertEngine.callAlertMessage('LoadedSuccess', "Contract Request Loaded to Plan Successfully !!!", "success", true);
                }
            }).
                error(function (data, status, headers, config) {
                    //console.log("Sorry internal master data cont able to fetch.");
                });
        }

    };



    //remove loaded request

    $scope.RemoveLoadedContract = function (c) {

        $.ajax({
            url: rootDir + '/Credentialing/CnD/RemoveRequestAndGrid?credentialingContractRequestID=' + c.CredentialingContractRequestID,
            type: 'POST',
            data: null,
            async: false,
            cache: false,
            contentType: false,
            processData: false,
            success: function (data) {
                //console.log(data);
                try {
                    if (data.status == "true") {
                        var obj = $filter('filter')($scope.LoadedData, { CredentialingContractRequestID: data.credentialingContractRequestID })[0];
                        $scope.LoadedData.splice($scope.LoadedData.indexOf(obj), 1);
                        var TabStatusLTP3 = $scope.LoadedData.length > 0 ? true : false;
                        $scope.$broadcast('LTPStatus', { StatusLTP: TabStatusLTP3 });
                        for (var i = 0; i < obj.ContractGrid.length; i++) {
                            for (var j = 0; j < $scope.PlanReportList.length; j++) {
                                if (obj.ContractGrid[i].ContractGridID == $scope.PlanReportList[j].ContractGridID) {
                                    var gridObj = $filter('filter')($scope.PlanReportList, { ContractGridID: $scope.PlanReportList[j].ContractGridID })[0];
                                    $scope.PlanReportList.splice($scope.PlanReportList.indexOf(gridObj), 1);
                                }
                            }
                        }
                        //$scope.LoadedData.splice($scope.LoadedData.indexOf(c), 1);
                        //$scope.credentialingInfo.CredentialingContractRequests.splice($scope.credentialingInfo.CredentialingContractRequests.indexOf(c), 1);
                        $('#loadedWarningModal').modal('hide');
                    }

                    else {

                    }
                }
                catch (e) { };


            },
            error: function (e) {
                try {
                    //$scope.SLError = "Sorry for Inconvenience !!!! Please Try Again Later...";
                    //messageAlertEngine.callAlertMessage('StateLicenseError', "", "danger", true);
                }
                catch (e) { };


            }

        });

    };

    $scope.RemoveReport = function (c) {

        $.ajax({
            url: rootDir + '/Credentialing/CnD/RemoveGrid?contractGridID=' + c.ContractGridID,
            type: 'POST',
            data: null,
            async: false,
            cache: false,
            contentType: false,
            processData: false,
            success: function (data) {
                //console.log(data);
                try {
                    if (data.status == "true") {
                        var obj = $filter('filter')($scope.PlanReportList, { ContractGridID: data.contractGridID })[0];
                        $scope.PlanReportList.splice($scope.PlanReportList.indexOf(obj), 1);
                        //$scope.PlanReportList.splice($scope.PlanReportList.indexOf(c), 1);
                        //  $scope.credentialingInfo.CredentialingContractRequests.splice($scope.credentialingInfo.CredentialingContractRequests.indexOf(c), 1);
                        $('#WarningModal').modal('hide');
                    }

                    else {

                    }
                }
                catch (e) { };


            },
            error: function (e) {
                try {
                    //$scope.SLError = "Sorry for Inconvenience !!!! Please Try Again Later...";
                    //messageAlertEngine.callAlertMessage('StateLicenseError', "", "danger", true);
                }
                catch (e) { };


            }

        });

    };
    $scope.showerror = false;

    $scope.selectedContractPracticeLocations = [];
    $scope.selectedContractSpecialties = [];

    $scope.SetRequestId = function (obj) {

        if (obj != null) {

            for (var i = 0; i < $scope.templateList.length; i++) {
                $scope.templateList[i].check = false;
            }

            $scope.selectedTemplate = null;
            sessionStorage.setItem('templateName', '');
            sessionStorage.setItem('templateCode', '');

            sessionStorage.setItem('CredentialingContractRequestID', obj.CredentialingContractRequestID);
            sessionStorage.setItem('InitialCredentialingDate', obj.InitialCredentialingDate);
            $scope.selectedContractPracticeLocations = obj.ContractPracticeLocations;
            $scope.selectedContractSpecialties = obj.ContractSpecialties;

        }

    };

    $scope.initWarning = function (c, i) {

        if (c != null) {

            $scope.tempRemoveReportData = angular.copy(c);

        }

        $('#WarningModal').modal();
    };
    $scope.loadedWarning = function (c) {

        if (c != null) {

            $scope.tempRemoveLoadedData = angular.copy(c);

        }

        $('#loadedWarningModal').modal();
    };

    $scope.SaveReport = function (c, index) {
        var validationStatus = true;
        var url;
        var myData = {};
        var $formData;
        //  if ($scope.Visibility == ('editVisibility' + index)) {
        //Add Details - Denote the URL
        try {
            $formData = $('#PlanReportForm').find('form');
            url = rootDir + "/Credentialing/CnD/SaveReport";
        }
        catch (e)
        { };
        //    }
        ResetFormForValidation($formData);
        validationStatus = $formData.valid();
        if (validationStatus) {
            //console.log(new FormData($formData[0]));

            //console.log($formDataStateLicense);
            $.ajax({
                url: url,
                type: 'POST',
                data: new FormData($formData[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    //console.log(data);
                    try {
                        if (data.status == "true") {
                            data.dataContractGrid.InitialCredentialingDate = $rootScope.ConvertDateFormat(data.dataContractGrid.InitialCredentialingDate);
                            if (data.dataContractGrid.Report != null) {
                                data.dataContractGrid.Report.CredentialedDate = $rootScope.ConvertDateFormat(data.dataContractGrid.Report.CredentialedDate);
                                data.dataContractGrid.Report.InitiatedDate = $rootScope.ConvertDateFormat(data.dataContractGrid.Report.InitiatedDate);
                                data.dataContractGrid.Report.TerminationDate = $rootScope.ConvertDateFormat(data.dataContractGrid.Report.TerminationDate);
                                data.dataContractGrid.Report.ReCredentialingDate = $rootScope.ConvertDateFormat(data.dataContractGrid.Report.ReCredentialingDate);
                            }
                            $scope.PlanReportList[index] = data.dataContractGrid;
                            messageAlertEngine.callAlertMessage('ReportSaveSuccess' + index, "Plan Report Updated Successfully !!!", "success", true);
                            $scope.SetVisibility('view', index);
                            $scope.PlanReportStatus = true;
                        }

                        else {
                            $scope.SLError = data.status.split(",");
                            messageAlertEngine.callAlertMessage('ReportError', "", "danger", true);
                        }
                    }
                    catch (e) { };


                },
                error: function (e) {
                    try {
                        $scope.SLError = "Sorry for Inconvenience !!!! Please Try Again Later...";
                        messageAlertEngine.callAlertMessage('StateLicenseError', "", "danger", true);
                    }
                    catch (e) { };


                }

            });
        }
    };
    $scope.loadingAjax = true;
    $http.get(rootDir + '/Credentialing/CnD/GetContractGrid?credentialingInfoID=' + credId).
       success(function (data, status, headers, config) {
           $scope.PlanReportList = angular.copy(data);
           //console.log("Check");
           //console.log(data);

           for (var i = 0; i < $scope.PlanReportList.length; i++) {
               $scope.PlanReportList[i].InitialCredentialingDate = $rootScope.ConvertDateFormat($scope.PlanReportList[i].InitialCredentialingDate);
               if ($scope.PlanReportList[i].Report != null) {
                   $scope.PlanReportList[i].Report.InitiatedDate = $rootScope.ConvertDateFormat($scope.PlanReportList[i].Report.InitiatedDate);
                   $scope.PlanReportList[i].Report.CredentialedDate = $rootScope.ConvertDateFormat($scope.PlanReportList[i].Report.CredentialedDate);
                   //$scope.PlanReportList[i].Report.TerminationDate = $rootScope.ConvertDateFormat($scope.PlanReportList[i].Report.TerminationDate);
                   $scope.PlanReportList[i].Report.ReCredentialingDate = $rootScope.ConvertDateFormat($scope.PlanReportList[i].Report.ReCredentialingDate);
               }
           }
           $scope.loadingAjax = false;
       }).
       error(function (data, status, headers, config) {
           //console.log("Sorry internal master data cont able to fetch.");
       });
    //----------------------------------
    $scope.SelectDocument = function (credprofile) {

        for (var i = 0; i < $scope.LoadedData.length; i++) {
            $scope.errorTemplate[i] = false;
        }
        //-----format practice location------------------
        var ProviderPracitceInfoBusinessModel = [];
        if ($scope.selectedContractPracticeLocations != null && $scope.selectedContractPracticeLocations.length != 0) {

            for (var i = 0; i < $scope.selectedContractPracticeLocations.length; i++) {
                var location = $scope.selectedContractPracticeLocations[i];
                obj = new Object();
                if (location.ProfilePracticeLocation.Facility != null) {
                    var facility = location.ProfilePracticeLocation.Facility;
                    obj.Address = (facility.Street || '') + ', ' + (facility.Building || '') + ', ' + (facility.City || '') + ', ' + (facility.State || '') + ', ' + (facility.Country || '') + ' - ' + (facility.ZipCode || '');
                    obj.PhoneNumber = facility.MobileNumber;
                    obj.FaxNumber = facility.FaxNumber;
                }

                if (location.ProfilePracticeLocation.BillingContactPerson != null) {
                    var billing = location.ProfilePracticeLocation.BillingContactPerson;
                    obj.BillingAddress = (billing.Street || '') + ', ' + (billing.Building || '') + ', ' + (billing.City || '') + ', ' + (billing.State || '') + ', ' + (billing.Country || '') + ' - ' + (billing.ZipCode || '');
                    obj.BillingPhoneNumber = billing.MobileNumber;
                    obj.BillingFaxNumber = billing.FaxNumber;
                }
                if (location.ProfilePracticeLocation.OfficeHour != null) {
                    var officeHour = location.ProfilePracticeLocation.OfficeHour.PracticeDays;
                    for (var j = 0; j < officeHour.length; j++) {
                        if (officeHour[j].DayName == 'Monday') {
                            obj.OfficeHourMonday = officeHour[j].DailyHours[0].StartTime + ' - ' + officeHour[j].DailyHours[0].EndTime;
                        }
                        if (officeHour[j].DayName == 'Tuesday') {
                            obj.OfficeHourTuesday = officeHour[j].DailyHours[0].StartTime + ' - ' + officeHour[j].DailyHours[0].EndTime;
                        }
                        if (officeHour[j].DayName == 'Wednesday') {
                            obj.OfficeHourWednesday = officeHour[j].DailyHours[0].StartTime + ' - ' + officeHour[j].DailyHours[0].EndTime;
                        }
                        if (officeHour[j].DayName == 'Thursday') {
                            obj.OfficeHourThursday = officeHour[j].DailyHours[0].StartTime + ' - ' + officeHour[j].DailyHours[0].EndTime;
                        }
                        if (officeHour[j].DayName == 'Friday') {
                            obj.OfficeHourFridayday = officeHour[j].DailyHours[0].StartTime + ' - ' + officeHour[j].DailyHours[0].EndTime;
                        }
                    }
                }

                var PracticeProviders = location.ProfilePracticeLocation.PracticeProviders;

                if (PracticeProviders != null) {
                    var CoveringPhysicians = [];
                    for (var i = 0; i < PracticeProviders.length; i++) {

                        if (PracticeProviders[i].Practice == 'CoveringColleague') {
                            CoveringPhysicians.push(PracticeProviders[i].FirstName + ' ' + PracticeProviders[i].LastName);
                        }
                    }
                    obj.CoveringPhysicians = CoveringPhysicians;
                }
                ProviderPracitceInfoBusinessModel.push(obj);
            }
        }

        //-------------------------format specialities-----------------------------
        var providerProfessionalDetailBusinessModel = [];

        if ($scope.selectedContractSpecialties != null && $scope.selectedContractSpecialties.length != 0) {

            for (var i = 0; i < $scope.selectedContractSpecialties.length; i++) {
                var obj = new Object();
                obj.PcpOrSpecialist = '';
                if ($scope.selectedContractSpecialties[i].ProfileSpecialty != null) {
                    obj.BoardCertified = $scope.selectedContractSpecialties[i].ProfileSpecialty.IsBoardCertified;
                    obj.Preference = $scope.selectedContractSpecialties[i].ProfileSpecialty.SpecialtyPreference;
                    if ($scope.selectedContractSpecialties[i].ProfileSpecialty.Specialty != null)
                        obj.Specialty = $scope.selectedContractSpecialties[i].ProfileSpecialty.Specialty.Name;
                    else
                        obj.Specialty = '';



                    if ($scope.selectedContractSpecialties[i].ProfileSpecialty.SpecialtyBoardCertifiedDetail != null) {
                        if ($scope.selectedContractSpecialties[i].ProfileSpecialty.SpecialtyBoardCertifiedDetail.SpecialtyBoard != null) {
                            obj.BoardName = $scope.selectedContractSpecialties[i].ProfileSpecialty.SpecialtyBoardCertifiedDetail.SpecialtyBoard.Name;
                        }
                        var InitialCertificationDate = $scope.selectedContractSpecialties[i].ProfileSpecialty.SpecialtyBoardCertifiedDetail.InitialCertificationDate;
                        var ExpirationDate = $scope.selectedContractSpecialties[i].ProfileSpecialty.SpecialtyBoardCertifiedDetail.ExpirationDate;

                        if (ExpirationDate == null) {
                            obj.SpecialtyEffectiveDates = 'Indefinitely';
                        } else {
                            if (InitialCertificationDate == null) {
                                obj.SpecialtyEffectiveDates = '-' + $filter('date')(new Date($scope.selectedContractSpecialties[i].ProfileSpecialty.SpecialtyBoardCertifiedDetail.ExpirationDate), 'MM/dd/yyyy');
                            }
                            else {
                                obj.SpecialtyEffectiveDates = $filter('date')(new Date($scope.selectedContractSpecialties[i].ProfileSpecialty.SpecialtyBoardCertifiedDetail.InitialCertificationDate), 'MM/dd/yyyy') + ' - ' + $filter('date')(new Date($scope.selectedContractSpecialties[i].ProfileSpecialty.SpecialtyBoardCertifiedDetail.ExpirationDate), 'MM/dd/yyyy');

                            }
                        }


                    } else {
                        obj.BoardName = '';
                        obj.SpecialtyEffectiveDates = '';
                    }


                }
                else {
                    obj.BoardCertified = '';
                    obj.Preference = '';
                    obj.Specialty = '';
                    obj.BoardName = '';
                    obj.SpecialtyEffectiveDates = '';
                }
                providerProfessionalDetailBusinessModel.push(obj);
            }

        }

        sessionStorage.setItem('ProviderPracitceInfoBusinessModel', JSON.stringify(ProviderPracitceInfoBusinessModel));
        sessionStorage.setItem('providerProfessionalDetailBusinessModel', JSON.stringify(providerProfessionalDetailBusinessModel));

        if ($scope.selectedTemplate != null) {
            sessionStorage.setItem('templateName', $scope.selectedTemplate.name);
            sessionStorage.setItem('templateCode', $scope.selectedTemplate.code);

            sessionStorage.setItem('selectDocumentBit', true);
            $scope.dismiss();
            var value = rootDir + '/Credentialing/DelegationProfileReport/Index?profileId=' + credprofile.Profile.ProfileID;
            var open_link = window.open('', '_blank');
            open_link.location = value;

        }
        else {
            $scope.showerror = true;
        }

        //window.location.assign('/Credentialing/DelegationProfileReport/Index?profileId=' + credprofile.Profile.ProfileID);
    }

    $scope.errorTemplate = [];
    for (var i = 0; i < $scope.LoadedData.length; i++) {
        $scope.errorTemplate.push(false);
    }





    $scope.DisableTab = function () {
        if ($scope.ccmStatus == false) {
            return false;
        }
    }



    $scope.viewPackage = function (CredentialingContractRequestID) {



    }



    $scope.viewDocument = function (CredentialingContractRequestID, credprofile, index) {



        $http.get(rootDir + '/Credentialing/DelegationProfileReport/GetDelegationProfileReport?CredContractRequestId=' + CredentialingContractRequestID).
      success(function (data, status, headers, config) {

          if (data.status == 'true') {
              if (data.profileReports[data.profileReports.length - 1] != null) {
                  for (var i = 0; i < $scope.LoadedData.length; i++) {
                      $scope.errorTemplate[i] = false;
                  }
                  sessionStorage.setItem('profileReport', JSON.stringify(data.profileReports[data.profileReports.length - 1]));
                  sessionStorage.setItem('selectDocumentBit', false);

                  var value = rootDir + '/Credentialing/DelegationProfileReport/Index?profileId=' + credprofile.Profile.ProfileID;
                  var open_link = window.open('', '_blank');
                  open_link.location = value;
              } else {
                  $scope.errorTemplate[index] = true;

              }
          }
      }).
      error(function (data, status, headers, config) {
          //console.log("Sorry internal master data cont able to fetch.");
      });


        //window.location.assign('/Credentialing/DelegationProfileReport/Index?profileId=' + credprofile.Profile.ProfileID);
    };




    //==================select template==============
    $scope.selectedTemplate = null;
    $scope.setSelectedTemplated = function (index) {
        if ($scope.templateList[index].check == true) {
            $scope.showerror = false;
            $scope.selectedTemplate = $scope.templateList[index];
            for (var i = 0; i < $scope.templateList.length; i++) {
                if (index != i) {
                    $scope.templateList[i].check = false;
                }
            }
        }
        flag = 0;
        for (var i = 0; i < $scope.templateList.length; i++) {
            if ($scope.templateList[i].check == true) {
                flag = 1;
            }
        }
        if (flag == 0) {
            $scope.selectedTemplate = null;
        }
    };
    var counts = [];

    //$scope.data = $scope.LoadedData;
    //$scope.init_table = function () {

    //    if ($scope.data.length <= 10) {
    //        counts = [];
    //    }
    //    else if ($scope.data.length <= 25) {
    //        counts = [10, 25];
    //    }
    //    else if ($scope.data.length <= 50) {
    //        counts = [10, 25, 50];
    //    }
    //    else if ($scope.data.length <= 100) {
    //        counts = [10, 25, 50, 100];
    //        }
    //    else if ($scope.data.length > 100) {
    //        counts = [10, 25, 50, 100];
    //    }


    //};
    //$scope.tableParams1 = new ngTableParams({
    //    page: 1,            // show first page
    //    count: 10,          // count per page
    //    filter: {
    //        //name: 'M'       // initial filter
    //        //FirstName : ''
    //    },
    //    sorting: {

    //        //name: 'asc'     // initial sorting
    //    }
    //}, {
    //    counts: counts,
    //    total: $scope.data.length, // length of data
    //    getData: function ($defer, params) {
    //        // use build-in angular filter
    //        var filteredData = params.filter() ?
    //                $filter('filter')($scope.data, params.filter()) :
    //                $scope.data;
    //        var orderedData = params.sorting() ?
    //                $filter('orderBy')(filteredData, params.orderBy()) :
    //                $scope.data;

    //        params.total(orderedData.length); // set total for recalc pagination
    //        $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
    //    }
    //});
    $scope.templateSelected = true;
    $scope.tempObject.UseExistingTemplate = 'NO';
    $scope.changeYesNoOption = function (value) {
        $("#newEmailForm .field-validation-error").remove();
        $scope.templateSelected = false;
        $scope.tempObject.Subject = "";
        $scope.tempObject.Body = "";

        if (value == 2) {
            $scope.templateSelected = true;
        }
    }

});