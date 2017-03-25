

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

Cred_SPA_App.directive('keyBlocker', function ($parse) {
    return {
        restrict: 'A',
        link: function (scope, elm, attrs) {
            elm.bind('keydown keypress', function (e) {

                e.preventDefault();
                return false;
            });
        }
    }
});
//Cred_SPA_App.config(function ($datepickerProvider) {
//    var today = new Date();
//    angular.extend($datepickerProvider.defaults, {
//        //startDate: 'today',
//        minDate: today,
//        startDate: 'today',
//        autoclose: true,
//        useNative: true
//    });
//});

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

Cred_SPA_App.controller('LoadToPlanController', function ($scope, $rootScope, $http, $filter, $timeout, messageAlertEngine) {


    $scope.tomorrow = new Date();
    //  $scope.tomorrow.setDate($scope.tomorrow.getDate() + 1);
    $scope.emailDisable = true;
    $http.get(rootDir + '../../../EmailService/GetAllEmailTemplates').
       success(function (data, status, headers, config) {

           try {
               if (data != null) {
                   $scope.templates = data;
               }
               for (var i = 0; i < $scope.templates.length; i++) {
                   $scope.templates[i].LastModifiedDate = $scope.ConvertDateFormat($scope.templates[i].LastModifiedDate);
               }
           } catch (e) {

           }

       }).
       error(function (data, status, headers, config) {

       });

    $http.get(rootDir + '/EmailService/GetAllEmailIds').
                 success(function (data, status, headers, config) {
                     try {
                         $scope.EmailsIds = angular.copy(data);
                     } catch (e) {
                     }
                 }).
                 error(function (data, status, headers, config) {
                 });

    $scope.hideDiv = function () {
        $("#templatelist").hide();
        $scope.errorMsg = false;
    }
    $scope.isdelegatedPlan;

    $scope.makeAllPackagesSelected = function (value) {
        for (var c = 0; c < $scope.PackageTempList.length; c++) {
            $scope.PackageTempList[c].Check = value;
        }

    }
    $scope.makeAllContractsSelected = function (value) {
        for (var i = 0; i < $scope.LoadedData.length; i++) {
            if ($scope.credentialingInfo.IsDelegated == 'YES') {
                if ($scope.LoadedData[i].DocumentPath != null)
                    $scope.LoadedData[i].isSelected = value;
            }
            else {
                if ($scope.LoadedData[i].PackageGeneratorReport != null)
                    $scope.LoadedData[i].isSelected = value;
            }

        }
    };
    $scope.checkForEmailDisability = function () {
        var flag = 0;
        for (var i = 0; i < $scope.LoadedData.length; i++) {
            if ($scope.LoadedData[i].isSelected == true) {
                $scope.emailDisable = false;
                flag = 1;
                break;
            }
        }
        if (flag != 1) {
            $scope.emailDisable = true;
        }
    }
    $scope.GenerateEmailPopUp = function (isdelegated) {

        var documents = [];
        var credIDs = [];
        for (var i = 0; i < $scope.LoadedData.length; i++) {
            if ($scope.LoadedData[i].isSelected) {
                if ($scope.LoadedData[i].PackageGeneratorReport != null) {
                    for (var j = 0; j < $scope.LoadedData[i].PackageGeneratorReport.PackageGeneratorReportCode.length; j++) {
                        documents.push($scope.LoadedData[i].PackageGeneratorReport.PackageGeneratorReportCode[j].FilePath);
                    }
                }

                credIDs.push($scope.LoadedData[i].CredentialingContractRequestID);
            }
        }

        $("#newEmailForm .field-validation-error").hide();
        $scope.isdelegatedPlan = isdelegated;
        $scope.packages = [];
        if (documents.length > 0) {

            for (var i = 0; i < documents.length; i++) {
                $scope.packages.push({ FilePath: documents[i] });
            }

        }


        $scope.errmsg = false;
        $scope.errmsgforTo = false;
        $scope.errmsgforCC = false;
        $scope.errmsgforBCC = false;
        $scope.fileList = [];
        var formdata = $('#newEmailForm');
        formdata[0].reset();
        $scope.tempObject.CredReqID = credIDs;
        $scope.tempObject.UseExistingTemplate = "NO";
    }
    $scope.GenerateEmailPopUp1 = function (isdelegated) {
        var documents = [];
        var credIDs = [];
        for (var i = 0; i < $scope.LoadedData.length; i++) {
            if ($scope.LoadedData[i].isSelected) {
                documents.push($scope.LoadedData[i].DocumentPath);
                credIDs.push($scope.LoadedData[i].CredentialingContractRequestID);
            }
        }

        $("#newEmailForm .field-validation-error").hide();
        $scope.isdelegatedPlan = isdelegated;
        $scope.packages = [];
        if (documents.length > 0) {

            for (var i = 0; i < documents.length; i++) {
                $scope.packages.push({ FilePath: documents[i] });
            }

        }

        $scope.errmsgforTo = false;
        $scope.errmsg = false;
        $scope.errmsgforCC = false;
        $scope.errmsgforBCC = false;
        $scope.fileList = [];
        var formdata = $('#newEmailForm');
        formdata[0].reset();
        $scope.tempObject.CredReqID = credIDs;
        $scope.tempObject.UseExistingTemplate = "NO";
    }

    $scope.EditCancle = function (temp) {
        //ResetFormForValidation($("#newEmailForm"));
        $scope.errmsgforTo = false;
        $scope.errmsgforCC = false;
        $scope.errmsgforBCC = false;
        $scope.fileList = [];
        var formdata = $('#newEmailForm');
        formdata[0].reset();
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
        $scope.tempObject.UseExistingTemplate = "NO";
        $scope.templateSelected = true;
        $('.badge').removeAttr('style');
        $("#newEmailForm .field-validation-error").hide();
        $scope.tempObject.SaveAsTemplateYesNoOption = 2;

    }

    //Code for multiple file upoloads
    $scope.FilesizeError = false;
    //Multiple Document ADD to DisClosure Question Start

    var QID = 0;
    var index1 = -1;
    $scope.addingDocument = function () {
        $('#file').click();
    }

    $scope.fileList = [];
    $scope.curFile;
    $scope.ImageProperty = {
        file: '',
        FileListID: -1,
        FileID: -1,
        FileStatus: ''
    }
    $scope.removeFile = function (index) {
        $scope.fileList.splice(index, 1)
    }

    $scope.getStyle = function () {
        var transform = ($scope.isSemi ? '' : 'translateY(-50%) ') + 'translateX(-50%)';

        return {
            'top': $scope.isSemi ? 'auto' : '50%',
            'bottom': $scope.isSemi ? '5%' : 'auto',
            'left': '35%',
            'transform': transform,
            '-moz-transform': transform,
            '-webkit-transform': transform,
            'font-size': $scope.radius / 3.5 + 'px'
        };
    };
    var files = [];
    var tempIndex = 0;
    var tempmultiplefilelength;
    $scope.setFile = function (element) {
        var count = 0;
        tempIndex = 0;
        var index = -1;
        var totalAttachmentSize = 0;
        files = [];
        files = element.files;
        for (var i = 0; i < $scope.fileList.length; i++) {
            totalAttachmentSize += $scope.fileList[i].File[0].file.size;
        }
        for (var j = 0; j < files.length; j++) {
            totalAttachmentSize += files[j].size;
        }
        var totalfilesize = 0;
        tempmultiplefilelength = $scope.fileList.length;
        if (count == 0 && totalAttachmentSize < 15728640) {
            for (var i = 0; i < files.length; i++) {

                $('.badge').removeAttr("style");
                totalfilesize += files[i].size;
                var TempArray = [];
                $scope.ImageProperty.file = files[i];
                $scope.ImageProperty.FileStatus = 'Active';
                $scope.ImageProperty.FileListID = $scope.fileList.length;
                $scope.ImageProperty.FileID = i;
                TempArray.push($scope.ImageProperty);
                $scope.fileList.push({ File: TempArray });
                $scope.ImageProperty = {};

                if (!$scope.$$fetch)
                    $scope.$apply();

            }
            $scope.UploadFile();

        }
        else {
            $('#file').val("");
            $('.badge').attr("style", "background-color:white;color:indianred");

        }

    }

    $scope.UploadFile = function () {

        for (var i = 0; i < $scope.fileList.length; i++) {
            for (var j = 0; j < $scope.fileList[i].File.length; j++) {
                if ($scope.fileList[i].File[j].UploadDone != true) $scope.fileList[i].File[j].UploadDone = false;
                if ($scope.fileList[i].File[j].FileStatus == 'Active') {
                    $scope.UploadFileIndividual($scope.fileList[i].File[j].file,
                                        $scope.fileList[i].File[j].file.name,
                                        $scope.fileList[i].File[j].file.type,
                                        $scope.fileList[i].File[j].file.size,
                                        $scope.fileList[i].File[j].FileListID,
                                        $scope.fileList[i].File[j].FileID
                                        );
                    $scope.fileList[i].File[j].FileStatus = 'Inactive';
                }
            }
        }
    }

    $scope.UploadFileIndividual = function (fileToUpload, name, type, size, Qindex, FLindex, Findex) {
        $scope.current = 0;
        var reqObj = new XMLHttpRequest();
        reqObj.upload.addEventListener("progress", uploadProgress, false)
        reqObj.addEventListener("load", uploadComplete, false)
        reqObj.addEventListener("error", uploadFailed, false)
        reqObj.addEventListener("abort", uploadCanceled, false)
        reqObj.open("POST", rootDir + "/Profile/DisclosureQuestion/FileUpload", true);
        reqObj.setRequestHeader("Content-Type", "multipart/form-data");
        reqObj.setRequestHeader('X-File-Name', name);
        reqObj.setRequestHeader('X-File-Type', type);
        reqObj.setRequestHeader('X-File-Size', size);


        reqObj.send(fileToUpload);

        function uploadProgress(evt) {
            if (evt.lengthComputable) {

                var uploadProgressCount = Math.round(evt.loaded * 100 / evt.total);
                $scope.current = uploadProgressCount;

                if (uploadProgressCount == 100) {
                    $scope.current = uploadProgressCount;
                }

            }
        }

        function uploadComplete(evt) {
            var resultdata = JSON.parse(evt.currentTarget.responseText);
            $scope.Attachments.push(resultdata.FilePath);
            if (files.length == 1) {
                $scope.fileList[$scope.fileList.length - 1].File[0].path = resultdata.FilePath;
                $scope.fileList[$scope.fileList.length - 1].File[0].relativePath = resultdata.RelativePath;
                $scope.fileList[$scope.fileList.length - 1].File[0].UploadDone = true;
            } else if (files.length != 1 && tempmultiplefilelength != 0) {
                $scope.fileList[tempmultiplefilelength].File[0].path = resultdata.FilePath;
                $scope.fileList[tempmultiplefilelength].File[0].relativePath = resultdata.RelativePath;
                $scope.fileList[tempmultiplefilelength].File[0].UploadDone = true;
                tempmultiplefilelength++;
            } else {
                $scope.fileList[tempIndex].File[0].path = resultdata.FilePath;
                $scope.fileList[tempIndex].File[0].relativePath = resultdata.RelativePath;
                $scope.fileList[tempIndex].File[0].UploadDone = true;
                tempIndex++;
            }
            $scope.NoOfFileSaved++;
            $scope.$apply();
            $('#file').val("");
        }

        function uploadFailed(evt) {
        }

        function uploadCanceled(evt) {
        }

    }

    $scope.Attachments = [];
    //END
    $scope.errmsgforTo = false;
    $scope.errmsgforCC = false;
    $scope.errmsgforBCC = false;
    $scope.FileUploadProgress = false;

    $scope.AddOrSaveEmail = function (Form_Id) {
        $scope.FileUploadProgress = false;
        var AttachmentsSize = 0;
        for (i = 0; i < $scope.fileList.length; i++) {
            AttachmentsSize += $scope.fileList[i].File[0].file.size;
            if ($scope.fileList[i].File[0].relativePath == "" || $scope.fileList[i].File[0].relativePath === undefined) {
                $scope.FileUploadProgress = true;
                messageAlertEngine.callAlertMessage('warningdiv', "File Upload is in Progress", "info", true);
                //$('#composeMail').animate({ scrollBottom: 0 }, 'medium');
                break;
            }
        }                     
        if (AttachmentsSize > 15728640) {
            messageAlertEngine.callAlertMessage('warningdiv', 'Files exceeded the size limit!', "info", true);
        }
        else {
        }
        var regx1 = /^[a-z][a-zA-Z0-9_]*(\.[a-zA-Z][a-zA-Z0-9_]*)?@[a-z][a-zA-Z-0-9]*\.[a-z]+(\.[a-z]+)?$/;
        ResetFormForValidation($("#" + Form_Id));
        var emailids = $('#tags').val().split(';');
        for (var i = 0; i < emailids.length ; i++) {
            if (emailids[i] != "") {
                $scope.errmsg = false;
                if (regx1.test(emailids[i].toLowerCase())) {
                    $scope.errmsgforTo = false;
                }
                else {
                    if (i == 0)
                        $scope.errmsgforTo = true;
                }
            }
            else {
                if (i == 0)
                    $scope.errmsg = true;
            }

        }
        if ($scope.tempObject.ToDate && $scope.tempObject.FromDate) {
            if ($scope.tempObject.ToDate < $scope.tempObject.FromDate)

                $scope.errmsgforToDate = true;

            else
                $scope.errmsgforToDate = false;

        }
        var emailDataCC = $('#tagsCC').val().split(';');
        for (var i = 0; i < emailDataCC.length;i++) {
            if (emailDataCC[i] != "") {
                if (regx1.test(emailDataCC[i].toLowerCase())) {
                    $scope.errmsgforCC = false;
                }
                else { $scope.errmsgforCC = true; }
            }
        }

        var emailDataBCC = $('#tagsBCC').val().split(';');
        for (var i = 0; i < emailDataBCC.length; i++) {
            if (emailDataBCC[i] != "") {
                if (regx1.test(emailDataBCC[i].toLowerCase())) {
                    $scope.errmsgforBCC = false;
                }
                else { $scope.errmsgforBCC = true; }
            }
        }
        var checkVariable = false;
        if (!$scope.errmsgforBCC && !$scope.errmsgforCC && !$scope.errmsgforTo && !$scope.errmsg && !$scope.errmsgforToDate) { checkVariable = true; }

        if ($("#" + Form_Id).valid() && checkVariable && !$scope.FileUploadProgress && AttachmentsSize < 15728640) {


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
            var CredentialingContractRequestID = sessionStorage.getItem('CredentialingContractRequestID');




            var $form = ($("#" + Form_Id)[0]);
            var FORMDATA = new FormData($form);
            FORMDATA.append("credRequestId", JSON.stringify($scope.tempObject.CredReqID));
            FORMDATA.append("isDelegated", $scope.isdelegatedPlan);


            $.ajax({
                url: rootDir + '/Credentialing/CnD/SendEmail',
                type: 'POST',
                data: FORMDATA,
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {

                    try {
                        if (data.status == "true") {
                            $('#composeMail').modal('toggle');
                            $scope.compose = false;
                            //$scope.tableParamsFollowUp.reload();
                            //$scope.tableParamsSent.reload();
                            $scope.tempObject.IntervalFactor = null;
                            $scope.tempObject.Body = "";
                            $scope.tempObject.Subject = "";
                            $scope.tempObject.SaveAsTemplateYesNoOption = "2";
                            $scope.emailsfill = "";
                            $scope.ccfill = "";
                            for (var i = 0; i < $scope.LoadedData.length; i++) {
                                $scope.LoadedData[i].isSelected = false;
                            }
                            $scope.checkForEmailDisability();
                            messageAlertEngine.callAlertMessage('LoadedSuccess', "Email scheduled for sending.", "success", true);


                            //$scope.getTabData('Sent');
                        }
                        else {
                            messageAlertEngine.callAlertMessage('errorMsgDiv', data.status, "danger", true);
                            //$scope.errorMsg = data.status;
                        }
                    } catch (e) {

                    }
                    //finally {

                    //    $scope.tempObject.To = "";
                    //    $scope.tempObject.CC = "";
                    //    $scope.tempObject.BCC = "";
                    //    $scope.tempObject.Subject = "";
                    //    $scope.tempObject.Body = "";
                    //    $scope.tempObject.IsRecurrenceEnabledYesNoOption = "";
                    //    $scope.tempObject.RecurrenceIntervalTypeCategory = "";
                    //    $scope.tempObject.FromDate = "";
                    //    $scope.tempObject.ToDate = "";
                    //    $scope.tempObject.IntervalFactor = "";

                    //}
                },
                error: function (data) {
                    messageAlertEngine.callAlertMessage('errorMsgDiv', data.status, "danger", true);
                    //$scope.errorMsg = "Unable to schedule Email.";
                }

            });


        }
        else {
            $("#newEmailForm .field-validation-error").show();
        }
    }
    $scope.GenerateDeletePackagesPopUp = function (data) {
        $scope.ContractRequest = data;
    }

    $scope.DeletePackagesforaContract = function () {
        var tempData = [];

        $http({
            method: "POST",
            url: rootDir + '/Credentialing/CnD/AddPackageToContractRequest',
            params: {
                ContractRequestID: $scope.ContractRequest.CredentialingContractRequestID,
                PackageGeneratorReportCode: JSON.stringify(tempData)
            },
        }).success(function (data, status, headers, config) {
            try {
                if (data != null) {

                    data.PackageGeneratorReportCode = JSON.parse(data.PackageGeneratorReportCode);
                    if (data.PackageGeneratorReportCode.length != 0) {
                        for (var c = 0; c < $scope.LoadedData.length; c++) {
                            if ($scope.LoadedData[c].CredentialingContractRequestID == $scope.ContractRequest.CredentialingContractRequestID) {
                                if ($scope.LoadedData[c].PackageGeneratorReport == null) {
                                    $scope.LoadedData[c].PackageGeneratorReport = data;
                                }
                                else {
                                    $scope.LoadedData[c].PackageGeneratorReport = data;
                                }
                                $scope.LoadedData[c].isSelected = false;
                            }
                        }
                    }
                    else {
                        for (var c = 0; c < $scope.LoadedData.length; c++) {
                            if ($scope.LoadedData[c].CredentialingContractRequestID == $scope.ContractRequest.CredentialingContractRequestID) {
                                $scope.LoadedData[c].PackageGeneratorReport = null;
                                $scope.LoadedData[c].isSelected = false;
                            }
                        }

                    }
                    $scope.checkForEmailDisability();


                }
                else {

                }
                $('#loadedDeletePackagesWarningModal').modal('hide');
            } catch (e) {

            }
        }).error(function (data, status, headers, config) {

        });



    }



    $scope.showContent = function () {
        $scope.templateSelected = true;
    }

    $scope.initActivityStatus = "";
    $scope.ccmStatus = false;
    $scope.credID = sessionStorage.getItem("credentialingInfoId");
    $scope.creddInfo = [];
    $scope.templateList = [{ name: 'Provider Profile Template 1', code: 'A2HC', check: false }, { name: 'Provider Profile Template 2', code: 'AHC', check: false }, ];

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

    //       $scope.isCCM();
    //   }).
    //   error(function (data, status, headers, config) {

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
        try {
            if (data != null) {


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
        } catch (e) {

        }
    }).error(function () {

    })

    $scope.toggleSelection = function (data) {
        if ($scope.PackageTempList[$scope.PackageTempList.indexOf(data)].Check) {

            $scope.frameView = data.PackageFilePath;
            $scope.showerrorforpackage = false;
        }
        else {
            $scope.frameView = '';
        }

    }
    $scope.viewDocumentInFrame = function (data) {
        $scope.frameView = data.PackageFilePath;
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

        //alert($scope.creddInfo);

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

        $('#credentialing_action1').removeClass('active');
        $('#credentialing_action').removeClass('active');
        $('#psvtab').removeClass('active');
        $('#psv').removeClass('active');
        $('#summary2').addClass('active');
        $('#SUMMARY').addClass('active');
        var CredlogDataL = "";
        var CredlogDataForLoading = {};
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
                    CredlogDataForLoading = $scope.creddInfo.CredentialingLogs[c];
                    flagL = 1;
                    break;
                }
            }
        }

        $scope.getStatusL(CredlogDataL);

        if (($scope.initActivityStatus == "Initiated") && $scope.creddInfo.Plan.DelegatedType == "NO") {
            $('#credentialing_action1').removeClass('active');
            $('#credentialing_action').removeClass('active');
            $('#psvtab').removeClass('active');
            $('#psv').removeClass('active');
            $('#summary2').addClass('active');
            $('#SUMMARY').addClass('active');
        }
        else if (($scope.initActivityStatus == "Initiated") && $scope.creddInfo.Plan.DelegatedType == "YES") {
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
        } else if ($scope.initActivityStatus == "CCM" && $scope.creddInfo.Plan.DelegatedType == "YES" && CredlogDataForLoading.CredentialingAppointmentDetail.CredentialingAppointmentResult.ApprovalStatus != 'Rejected') {
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
        } else if ($scope.initActivityStatus == "Submitted" || $scope.initActivityStatus == "Completed" || $scope.creddInfo.Plan.DelegatedType == "YES") {

            $('#ccmtab').removeClass('active');
            $('#credentialing_action1').removeClass('active');
            $('#credentialing_action').removeClass('active');
            $('#psvtab').removeClass('active');
            $('#psv').removeClass('active');
            $('#summary2').addClass('active');
            $('#SUMMARY').addClass('active');
        }

        if (CredlogDataForLoading.CredentialingAppointmentDetail != null) {
            if ($scope.creddInfo.Plan.DelegatedType == "YES") {

                if (CredlogDataForLoading.CredentialingAppointmentDetail.CredentialingAppointmentResult != null) {
                    if (CredlogDataForLoading.CredentialingAppointmentDetail.CredentialingAppointmentResult.ApprovalStatus == 'Rejected') {
                        $scope.ccmStatus = true;

                    }
                }
                else {
                    $scope.ccmStatus = true;
                }
            }
        }
        else {
            if ($scope.creddInfo.Plan.DelegatedType == "YES") {
                $scope.ccmStatus = true;
            }
        }

    }
    $scope.isCCM();




    $scope.ShowVisibility = '';
    $scope.LoadedData = [];
    if ($scope.credentialingInfo != null && $scope.credentialingInfo.CredentialingContractRequests != null) {

        for (var i = 0; i < $scope.credentialingInfo.CredentialingContractRequests.length; i++) {
            if ($scope.credentialingInfo.CredentialingContractRequests[i].Status != 'Inactive') {
                $scope.LoadedData.push($scope.credentialingInfo.CredentialingContractRequests[i]);
            }
        }
        for (var i = 0; i < $scope.LoadedData.length; i++) {
            $scope.LoadedData[i].isSelected = false;
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


    sessionStorage.setItem('ProfileID', $scope.credentialingInfo.ProfileID);

    $scope.ClearVisibility = function () {
        $scope.validRequired = true;
        $scope.validFormat = true;
        $scope.ShowVisibility = '';
    }


    $scope.SetVisibility = function (type, index) {
        if (type == 'Qedit') {
            $scope.qTempObject = angular.copy($scope.PlanReportList[index]);
            $scope.ShowVisibility = 'QeditVisibility' + index;
        }
        else if (type == 'edit') {
            $scope.eTempObject = angular.copy($scope.PlanReportList[index]);
            if ($scope.eTempObject.InitialCredentialingDate != null) {
                $scope.eTempObject.InitialCredentialingDate = $rootScope.ConvertDateFormat($scope.eTempObject.InitialCredentialingDate);
                if ($scope.eTempObject.Report.TerminationDate == '' || $scope.eTempObject.Report.TerminationDate == null) {
                    // $scope.eTempObject.Report.TerminationDate = $scope.ConvertDateBy3Years($scope.eTempObject.InitialCredentialingDate);
                }
                //if ($scope.eTempObject.Report.ReCredentialingDate == '' || $scope.eTempObject.Report.ReCredentialingDate == null) {
                //    $scope.eTempObject.Report.ReCredentialingDate = $scope.ConvertDateBy3Years($scope.eTempObject.InitialCredentialingDate);
                //}
                }

                //if ($scope.eTempObject.Report.CredentialingApprovalStatusType == '' || $scope.eTempObject.Report.CredentialingApprovalStatusType == null) {
                //    $scope.eTempObject.Report.CredentialingApprovalStatusType = 'Rejected';
                //}
            
            //else {
            //    $scope.eTempObject.Report.ReCredentialingDate = null;
            //}
            $scope.ShowVisibility = 'editVisibility' + index;
        }
        else if (type == 'view') {
            $scope.ShowVisibility = 'viewVisibility' + index;
            if ($scope.PlanReportList[index].InitialCredentialingDate != null && ($scope.PlanReportList[index].Report.ReCredentialingDate == '' || $scope.PlanReportList[index].Report.ReCredentialingDate == null)) {
                //$scope.PlanReportList[index].Report.ReCredentialingDate = $scope.ConvertDateBy3Years($scope.PlanReportList[index].InitialCredentialingDate);
            }
        }
    };

    $scope.datesplitter = function (date) {
        returndate = "";
        if (date != null) {
            var newdate = date.split('T')[0].split('-');
            returndate = newdate[1] + "/" + newdate[2] + "/" + newdate[0];
        }
        return returndate;
    }

    $scope.tempID = 0;
    $scope.AddPackageToContractRequest = function (temp) {
        $scope.showerrorforpackage = false;
        $scope.ContractRequest = temp;
        $scope.PackageTempList = angular.copy($scope.PackageList);
        $scope.selectAllPackages = false;
        var temporary = [];
        if ($scope.ContractRequest.PackageGeneratorReport != null) {
            for (var c = 0; c < $scope.ContractRequest.PackageGeneratorReport.PackageGeneratorReportCode.length; c++) {
                for (var d = 0; d < $scope.PackageTempList.length; d++) {
                    if ($scope.PackageTempList[d].PackageGeneratorID == $scope.ContractRequest.PackageGeneratorReport.PackageGeneratorReportCode[c].ID) {
                        $scope.PackageTempList[d].Check = true;
                    }
                }
            }
        }
        var flag = 0;
        for (var d = 0; d < $scope.PackageTempList.length; d++) {
            if (!$scope.PackageTempList[d].Check) {
                flag = 1;
                break;
            }
        }
        if (flag == 0)
            $scope.selectAllPackages = true;
        else
            $scope.selectAllPackages = false;
        $scope.frameView = '';
        //for (var c = 0; c < temporary.length; c++) {
        //    $scope.PackageTempList.splice($scope.PackageTempList.indexOf(temporary[c]), 1)
        //}
    }

    //$scope.L = [{ "id": 2, "p": 4 }{ "id": 3, "p": 5 }]

    //alert(JSON.stringify($scope.L));
    for (var c = 0; c < $scope.LoadedData.length; c++) {
        if ($scope.LoadedData[c].PackageGeneratorReport != null) {

            $scope.LoadedData[c].PackageGeneratorReport.PackageGeneratorReportCode = JSON.parse($scope.LoadedData[c].PackageGeneratorReport.PackageGeneratorReportCode);
        }
    }
    //var a = [];
    ////a = JSON.parse('[' + $scope.LoadedData[0].PackageGeneratorReport.PackageGeneratorReportCode + ']');


    $scope.SetRecredentialingDate = function (value) {
        if (value == null) {
            $scope.eTempObject.Report.ReCredentialingDate = "";
        }
        else {
            if ($scope.eTempObject != null && $scope.eTempObject != {}) {
                var RecredentialingDuration = jQuery.grep($scope.eTempObject.CredentialingInfo.Plan.PlanLOBs, function (ele) { return ele.LOBID == $scope.eTempObject.LOB.LOBID })[0].ReCredentialingDuration;
                $scope.eTempObject.Report.ReCredentialingDate = $scope.ConvertDateBy3OR5Years(value, RecredentialingDuration != null ? RecredentialingDuration : 3);
            }
        }
    }

    //$scope.$watch('eTempObject.InitialCredentialingDate', function (newValue, oldValue) {

    //    if (newValue === oldValue || newValue == null) {
    //        return
    //    }
    //    else {
    //        if ($scope.eTempObject != null && $scope.eTempObject != {}) {
    //            $scope.eTempObject.Report.ReCredentialingDate = $scope.ConvertDateBy3OR5Years($scope.eTempObject.InitialCredentialingDate, $scope.eTempObject.LOB.LOBID);
    //        }
    //    }
    //})
    $scope.AddPackage = function () {
        var flag = 0;
        var tempData = [];


        for (var c = 0; c < $scope.PackageTempList.length; c++) {
            if ($scope.PackageTempList[c].Check == true) {
                flag = 1;

                tempData.push({ ID: $scope.PackageTempList[c].PackageGeneratorID, FilePath: $scope.PackageTempList[c].PackageFilePath });


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
                try {
                    if (data != null) {

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


                    }
                    else {

                    }
                    $('#selectPackage').modal('hide');
                } catch (e) {

                }
            }).error(function (data, status, headers, config) {

            })
        }
        else {
            $scope.showerrorforpackage = true;
        }
    }
    //Convert the date from database to normal
    $rootScope.ConvertDateFormat = function (value) {

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

    //});

    $http.get(rootDir + '/MasterDataNew/GetAllLOBsOfPlanContractByPlanID?planID=' + $scope.credentialingInfo.PlanID).
   success(function (data, status, headers, config) {
       try {
           $scope.ContractLOBsList = angular.copy(data);
           $scope.MasterContractLOBslist = data;
       } catch (e) {

       }
   }).
   error(function (data, status, headers, config) {

   });

    // Participating Status List Start

    $http.get(rootDir + '/Credentialing/CnD/GetAllParticipatingStatus').
  success(function (response, status, headers, config) {
      try {
          console.log('Participant Status List');
          console.log(response);
          $scope.ParticipatingStatusList = angular.copy(response);
      } catch (e) {

      }
  }).
  error(function (response, status, headers, config) {

  });

    // Participating Status List End

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

    //$scope.ContractLocationsList = angular.copy($scope.credentialingInfo.Profile.PracticeLocationDetails);

    $http.get(rootDir + '/MasterDataNew/GetAllOrganizationGroupAsync').
    success(function (data, status, headers, config) {
        try {
            $scope.BusinessEntities = angular.copy(data);
            $scope.MasterBusinessEntities = data;
        } catch (e) {

        }
    }).
    error(function (data, status, headers, config) {

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
            c.SpecialtyDetailID = c.ProfileSpecialtyID;
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
            $scope.PlanReportStatus = true;

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
                   try {
                       //data = JSON.parse(data);
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
                   } catch (e) {

                   }
               }).
               error(function (data, status, headers, config) {

               });
            $scope.ClearVisibility();
        }
    };

    $scope.ConvertDateBy3Years = function (date) {
        if (date != '' || date != 'null') {
            var dt = new Date(date);
            var month = dt.getMonth() + 1;
            var monthString = month > 9 ? month : '0' + month;
            //var monthName = monthNames[month];
            var day = dt.getDate();
            var dayString = day > 9 ? day : '0' + day;
            var year = dt.getFullYear() + 3;
            shortDate = monthString + '/' + dayString + '/' + year;
            return shortDate;
        } return null;
    }
    var checkLeapYear = function (dt, LOBID) {
        var NoofDays = 0;
        if (LOBID == 1 || LOBID == 2) {
            NoofDays
        }
        else {
            NoofDays
        }
        return NoofDays
    }
    $scope.ConvertDateBy3OR5Years = function (date, RecredentialingDuration) {
        if (date != '' || date != 'null') {
            var dt = new Date(date);
            //var NoOfDays = checkLeapYear(dt, LOBID);
            var month = dt.getMonth() + 1;
            var monthString = month > 9 ? month : '0' + month;
            //var monthName = monthNames[month];
            var day = dt.getDate();
            var dayString = day > 9 ? day : '0' + day;
            var year = 0;
            //if (LOBID == 1 || LOBID == 2) {

            //    year = dt.getFullYear() + 5;
            //}
            //else {
            //    year = dt.getFullYear() + 3;
            //}
            year = dt.getFullYear() + RecredentialingDuration;
            shortDate = monthString + '/' + dayString + '/' + year;
            return shortDate;
        } return null;
    }

    //function convertToEasternTimeZone(val) {

    //    //EST
    //    offset = -5.0

    //    clientDate = new Date(val);
    //    utc = clientDate.getTime() + (clientDate.getTimezoneOffset() * 60000);  //Convert to millisecond. 1000 milliseconds = 1 second, and 1 minute = 60 

    //    easternDate = new Date(utc + (3600000 * offset));
    //    return easternDate;
    //}

    //$scope.changeDateTime = function (values) {
    //    values = convertToEasternTimeZone(values);
    //    if (!values) { return ''; }
    //    var returnValue = values;
    //    var format;
    //    try {
    //        if (values.indexOf("/Date(") == 0) {
    //            returnValue = new Date(parseInt(values.replace("/Date(", "").replace(")/", ""), 10));
    //        }
    //    } catch (e) {
    //        returnValue = returnValue;
    //    }
    //    if (angular.isDate(returnValue)) {
    //        //value = returnValue.setTime(returnValue.getTime() - returnValue.getTimezoneOffset() * 60 * 1000);
    //        value = returnValue.getHours() + ":" + returnValue.getMinutes();
    //    }
    //    else {
    //        var formatDate
    //        returnValue = values.split('T')[1];
    //        formatDate = new Date(values.split('T')[0]);
    //        value = returnValue;
    //        returnValue = formatDate;
    //    }
    //    var time = value.split(":");
    //    var hours = time[0];
    //    var minutes = time[1];
    //    var ampm = hours >= 12 ? 'PM' : 'AM';
    //    hours = hours % 12;
    //    hours = hours ? hours : 12; // the hour '0' should be '12'
    //    minutes = minutes.length == 1 ? minutes < 10 ? '0' + minutes : minutes : minutes;
    //    var d = returnValue.toString();
    //    var stampDate = d.split(' ');
        //    var strTime = stampDate[1] + ' ' + stampDate[2] + ' ' + stampDate[3] + ' ' + hours + ':' + minutes + ' ' + ampm;
    //    //minutes = minutes < 9 ? '00' : minutes;
    //    //if (format == true) {
    //    //    var strTime = stampDate[1] + ' ' + stampDate[2] + ' ' + stampDate[3] + ' ' + hours + ':' + minutes + ' ' + ampm;
    //    //}
    //    //else {
    //    //    stampDate = formatDate.toDateString();
    //    //    var strTime = stampDate + ' ' + hours + ':' + minutes + ' ' + ampm;
    //    //}
    //    return strTime;
        //}

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
                    if ($scope.Log.CredentialingAppointmentDetail.CredentialingAppointmentResult != null) {
                        $scope.LoadedData.InitialCredentialingDate = $scope.Log.CredentialingAppointmentDetail.CredentialingAppointmentResult.SignedDate;
                    }
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
                try {
                    if (data.status == 'true') {
                        data.dataCredentialingContractRequest.isSelected = false;
                        $scope.loadingAjax = false;
                        $rootScope.isLoaded = true;
                        $rootScope.loadedDate = new Date();
                        $scope.PlanReportStatus = true;

                        $scope.ContractSpecialityList = [];
                        $scope.ContractLocationsList = [];
                        $scope.loadID = data.dataCredentialingContractRequest.ContractGrid[0].CredentialingInfo.InitiatedByID;
                       for (var j = 0; j < $scope.users.length; j++) {
                            if ($scope.users[j].CDUserID == $scope.loadID) {
                               if ($scope.users[j].FullName != null) {
                                   $rootScope.updatedByForLoading = $scope.users[j].FullName;
                               }
                               else {
                                $rootScope.updatedByForLoading = $scope.users[j].Email;
                               }


                            }
                        }
                        $rootScope.updatedDateForLoading = $rootScope.changeDateTime(data.dataCredentialingContractRequest.ContractGrid[0].LastModifiedDate);
                        try {
                            var tempactivity = {
                                Activity: $rootScope.tempActivity,
                                ActivityByName: $rootScope.updatedByForLoading,
                                LastModifiedDate: $rootScope.updatedDateForLoading
                            };
                            $rootScope.timelineActivity.unshift(tempactivity);
                            $rootScope.timelineActivity = $rootScope.timelineActivity.unique();
                        } catch (e) {

                        }
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
                            //data.dataCredentialingContractRequest.ContractGrid[i].Report.ReCredentialingDate = $scope.ConvertDateBy3Years($scope.LoadedData.InitialCredentialingDate);
                            $scope.PlanReportList.push(data.dataCredentialingContractRequest.ContractGrid[i]);
                        }

                        //$timeout(function () {
                        //    $rootScope.$broadcast('CompletebuttonEvent', true);
                        //}, 1500)
                        //CompleteButtonSTATUS.changeCompleteButtonStatus(true);
                        //$rootScope.PlanCompleted = true;
                        $rootScope.DisableCompleteButton = false;
                        messageAlertEngine.callAlertMessage('LoadedSuccess', "Contract Request Loaded to Plan Successfully !!!", "success", true);


                    }
                } catch (e) {

                }
            }).
                error(function (data, status, headers, config) {

                });
        }

    };

    Array.prototype.unique = function () {
        var r = new Array();
        o: for (var i = 0, n = this.length; i < n; i++) {
            for (var x = 0, y = r.length; x < y; x++) {
                if (r[x].Activity == this[i].Activity) {
                    continue o;  //Avoiding Duplicate
                }
            }
            r[r.length] = this[i];
        }
        return r;
    }

    //$scope.ParticipatingStatusList = ["par", "Non-par", "Approved", "Rejected"];
    $scope.SelectStatus = function (status) {
        $(".ProviderTypeSelectAutoList").hide();
        $scope.eTempObject.Report.ParticipatingStatus = status;
    }

    $scope.searchCumDropDown = function (divId) {
        $(".ProviderTypeSelectAutoList").hide();
        $("#" + divId).show();
    };

    //remove loaded request

    $scope.RemoveLoadedContract = function (c) {
        $($('#loadedWarningModal').find('button')[2]).attr('disabled', true);
        $.ajax({
            url: rootDir + '/Credentialing/CnD/RemoveRequestAndGrid?credentialingContractRequestID=' + c.CredentialingContractRequestID,
            type: 'POST',
            data: null,
            async: false,
            cache: false,
            contentType: false,
            processData: false,
            success: function (data) {

                try {
                    if (data.status == "true") {
                        var obj = $filter('filter')($scope.LoadedData, { CredentialingContractRequestID: data.credentialingContractRequestID })[0];
                        $scope.LoadedData.splice($scope.LoadedData.indexOf(obj), 1);
                        var TabStatusLTP3 = $scope.LoadedData.length > 0 ? true : false;
                        $scope.checkForEmailDisability();

                        $scope.$broadcast('LTPStatus', { StatusLTP: TabStatusLTP3 });
                        for (var i = 0; i < obj.ContractGrid.length; i++) {
                            for (var j = 0; j < $scope.PlanReportList.length; j++) {
                                if (obj.ContractGrid[i].ContractGridID == $scope.PlanReportList[j].ContractGridID) {
                                    var gridObj = $filter('filter')($scope.PlanReportList, { ContractGridID: $scope.PlanReportList[j].ContractGridID })[0];
                                    $scope.PlanReportList.splice($scope.PlanReportList.indexOf(gridObj), 1);
                                }
                            }
                        }
                        $rootScope.DisableCompleteButton = $scope.PlanReportList.length > 0 ? false : true;
                        if ($scope.LoadedData.length == 0) {
                            //$timeout(function () {
                            //    $rootScope.$broadcast('CompletebuttonEvent', false);
                            //}, 1500)
                            //CompleteButtonSTATUS.changeCompleteButtonStatus(false);
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

    $scope.RemoveLoadToPlan = function (c) {
        $($('#WarningModal').find('button')[2]).attr('disabled', true);
        $.ajax({
            url: rootDir + '/Credentialing/CnD/RemoveLoadPlan?contractGridID=' + c.ContractGridID,
            type: 'POST',
            data: null,
            async: false,
            cache: false,
            contentType: false,
            processData: false,
            success: function (data) {

                try {
                    if (data.status == "true") {
                        var obj = $filter('filter')($scope.PlanReportList, { ContractGridID: data.contractGridID })[0];
                        $scope.PlanReportList.splice($scope.PlanReportList.indexOf(obj), 1);
                        $rootScope.DisableCompleteButton = $scope.PlanReportList.length > 0 ? false : true;
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

    $scope.checkIfTemplateAdded = function () {
        var ccrID = sessionStorage.getItem('CredentialingContractRequestID');

        if (ccrID) {
            $http.get(rootDir + '/Credentialing/CnD/GetContractRequestDoc?reqID=' + ccrID).success(function (data) {

                for (var i = 0; i < $scope.LoadedData.length; i++) {
                    if ($scope.LoadedData[i].CredentialingContractRequestID == ccrID) {
                        $scope.LoadedData[i].DocumentPath = data;
                    }

                }
            });


        }
    }

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
            //sessionStorage.setItem('PracticeLocations', obj.ContractPracticeLocations);

        }
        for (var i = 0; i < $scope.AvailableTemplates.length; i++) {
            $scope.AvailableTemplates[i].show = false;
        }

    };

    $scope.initWarning = function (c, i) {
        $($('#WarningModal').find('button')[2]).attr('disabled', false);
        if (c != null) {

            $scope.tempRemoveReportData = angular.copy(c);

        }

        $('#WarningModal').modal();
    };
    $scope.loadedWarning = function (c) {
        $($('#loadedWarningModal').find('button')[2]).attr('disabled', false);
        if (c != null) {

            $scope.tempRemoveLoadedData = angular.copy(c);

        }

        $('#loadedWarningModal').modal();
    };

    $scope.setFiles = function (file) {
        $(file).parent().parent().find(".jancyFileWrapTexts").find("span").width($(file).parent().parent().width() < 243 ? $(file).parent().parent().width() : 243);

    }

    $scope.SaveReport = function (c, index) {

        var validationStatus = true;
        var url;
        var myData = {};
        var $formData;
        //if ($('#fileexists').text() == "" && $scope.eTempObject.Report.WelcomeLetterPath != null)
        //{
        //    $scope.tempObject.Report.WelcomeLetterPath = $scope.eTempObject.Report.WelcomeLetterPath;
        //}
        //  if ($scope.Visibility == ('editVisibility' + index)) {
        //Add Details - Denote the URL
        try {
            $formData = $('#PlanReportForm').find('form');
            url = rootDir + "/Credentialing/CnD/SaveReport";
        }
        catch (e)
        { };
        ResetFormForValidation($formData);
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
                        data = JSON.parse(data);
                        if (data.status == "true") {
                            console.log(data);
                            data.dataContractGrid.InitialCredentialingDate = $rootScope.ConvertDateFormat(data.dataContractGrid.InitialCredentialingDate);
                            if (data.dataContractGrid.Report != null) {
                                data.dataContractGrid.Report.CredentialedDate = $rootScope.ConvertDateFormat(data.dataContractGrid.Report.CredentialedDate);
                                data.dataContractGrid.Report.InitiatedDate = $rootScope.ConvertDateFormat(data.dataContractGrid.Report.InitiatedDate);
                                data.dataContractGrid.Report.TerminationDate = $rootScope.ConvertDateFormat(data.dataContractGrid.Report.TerminationDate);
                                data.dataContractGrid.Report.ReCredentialingDate = $rootScope.ConvertDateFormat(data.dataContractGrid.Report.ReCredentialingDate);
                                $scope.PlanReportStatus = true;
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
           try {
               $scope.PlanReportList = angular.copy(data);
               for (var i = 0; i < $scope.PlanReportList.length; i++) {
                   $scope.PlanReportList[i].InitialCredentialingDate = $rootScope.ConvertDateFormat($scope.PlanReportList[i].InitialCredentialingDate);
                   //$scope.PlanReportList[i].InitialCredentialingDate = $scope.datesplitter($scope.PlanReportList[i].InitialCredentialingDate);
                   if ($scope.PlanReportList[i].Report != null) {
                       $scope.PlanReportList[i].Report.InitiatedDate = $rootScope.ConvertDateFormat($scope.PlanReportList[i].Report.InitiatedDate);
                       $scope.PlanReportList[i].Report.CredentialedDate = $rootScope.ConvertDateFormat($scope.PlanReportList[i].Report.CredentialedDate);
                       //$scope.PlanReportList[i].Report.TerminationDate = $rootScope.ConvertDateFormat($scope.PlanReportList[i].Report.TerminationDate);
                       $scope.PlanReportList[i].Report.ReCredentialingDate = $rootScope.ConvertDateFormat($scope.PlanReportList[i].Report.ReCredentialingDate);
                   }
               }
               $scope.loadingAjax = false;
           } catch (e) {

           }
       }).
       error(function (data, status, headers, config) {
       });


    //----------------------------------
    $scope.SelectDocument = function (credprofile) {

        for (var m = 0; m < $scope.LoadedData.length; m++) {
            $scope.errorTemplate[m] = false;
        }
        //-----format practice location------------------
        var ProviderPracitceInfoBusinessModel = [];
        if ($scope.selectedContractPracticeLocations != null && $scope.selectedContractPracticeLocations.length != 0) {

            for (var i = 0; i < $scope.selectedContractPracticeLocations.length; i++) {
                //for(var i in $scope.selectedContractPracticeLocations){
                var location = $scope.selectedContractPracticeLocations[i];
                obj = new Object();
                if (location.ProfilePracticeLocation.Facility != null) {
                    var facility = location.ProfilePracticeLocation.Facility;
                    obj.FacilityID = location.ProfilePracticeLocation.FacilityId;
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
                    for (var k = 0; k < PracticeProviders.length; k++) {

                        if (PracticeProviders[k].Practice == 'CoveringColleague') {
                            CoveringPhysicians.push(PracticeProviders[k].FirstName + ' ' + PracticeProviders[k].LastName);
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

        localStorage.setItem('ProviderPracitceInfoBusinessModel', JSON.stringify(ProviderPracitceInfoBusinessModel));
        localStorage.setItem('providerProfessionalDetailBusinessModel', JSON.stringify(providerProfessionalDetailBusinessModel));
        //sessionStorage.setItem('ProviderPracitceInfoBusinessModel', JSON.stringify(ProviderPracitceInfoBusinessModel));
        //sessionStorage.setItem('providerProfessionalDetailBusinessModel', JSON.stringify(providerProfessionalDetailBusinessModel));

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

          try {
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
          } catch (e) {

          }
      }).
      error(function (data, status, headers, config) {
      });


        //window.location.assign('/Credentialing/DelegationProfileReport/Index?profileId=' + credprofile.Profile.ProfileID);
    };


    $scope.AvailableTemplates = [{ path: "/Content/Document/Provider Profile for Wellcare.pdf", show: false }, { path: "/Content/Document//Provider Profile for Wellcare - BLANK.pdf", show: false }]

    //==================select template==============
    $scope.selectedTemplate = null;

    $scope.setSelectedTemplated = function (index) {
        for (var i = 0; i < $scope.AvailableTemplates.length; i++) {
            $scope.AvailableTemplates[i].show = false;
        }
        if ($scope.templateList[index].check == true) {
            $scope.AvailableTemplates[index].show = true;
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
    //$scope.tempObject.UseExistingTemplate = 'NO';
    $scope.changeYesNoOption = function (value) {
        $("#newEmailForm .field-validation-error").remove();
        $scope.templateSelected = false;
        $scope.tempObject.Subject = "";
        $scope.tempObject.Body = "";

        if (value == 2) {
            $scope.templateSelected = true;
        }
    }

    var availableTags = [];
    $('.badge').removeAttr("style");
    availableTags = angular.copy($scope.EmailsIds);
    //availableTags =
    //    [
    //        'tulasidhar.salla@pratian.com',
    //        'bindupriya.ambati@pratian.com',
    //        'manideep.innamuri@pratian.com',
    //        'sharath.km@pratian.com',
    //        'shalabh@pratian.com'
    //    ];

    $scope.emailsAutoFill = function () {
        $(function () {

            //availableTags = $scope.EmailsIds;

            function split(val) {
                return val.split(/;\s*/);
            }
            function extractLast(term) {
                return split(term).pop();
            }

            $("#tags,#tagsCC,#tagsBCC")
                // don't navigate away from the field on tab when selecting an item
                .bind("keydown", function (event) {
                    if (event.keyCode === $.ui.keyCode.TAB &&
                            $(this).autocomplete("instance").menu.active) {
                        event.preventDefault();
                    }
                })
                .autocomplete({
                    minLength: 0,
                    source: function (request, response) {
                        // delegate back to autocomplete, but extract the last term
                        response($.ui.autocomplete.filter(
                            availableTags, extractLast(request.term)));
                    },
                    focus: function () {
                        // prevent value inserted on focus
                        return false;
                    },
                    select: function (event, ui) {
                        var terms = split(this.value);
                        // remove the current input
                        terms.pop();
                        // add the selected item
                        terms.push(ui.item.value);
                        // add placeholder to get the comma-and-space at the end
                        terms.push("");
                        this.value = terms.join(";");
                        return false;
                    }
                });
        });
    }


    $scope.showCompose = function () {
        //$("#newEmailForm .field-validation-error").remove();
        $("#newEmailForm .field-validation-error").hide();
        $scope.compose = true;
    }
    $scope.closeCompose = function () {
        $scope.compose = false;
    }
    $(document).click(function (event) {
        if (!$(event.target).hasClass("form-control") && $(event.target).parents(".TemplateSelectAutoList").length === 0) {
            $(".TemplateSelectAutoList").hide();
        }
    });

    $(document).ready(function () {
        $(".TemplateSelectAutoList").hide();

    });

    function showTemplateList(ele) {
        $(ele).parent().find(".TemplateSelectAutoList").first().show();
    }


  
});
function ResetFormForValidation(form) {
    form.removeData('validator');
    form.removeData('unobtrusiveValidation');
    $.validator.unobtrusive.parse(form);
};