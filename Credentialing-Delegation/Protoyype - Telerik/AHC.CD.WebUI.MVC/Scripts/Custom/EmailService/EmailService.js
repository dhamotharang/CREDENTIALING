var EmailServiceApp = angular.module("EmailServiceApp", ["ngTable", 'mgcrea.ngStrap', "wysiwyg.module", 'colorpicker.module']);

EmailServiceApp.directive('searchdropdown', function () {
    return {
        restrict: 'AE',
        link: function (scope, element, attr) {
            element.bind('focus', function () {
                element.parent().find(".TemplateSelectAutoList").show();
            });
        }
    };
});

EmailServiceApp.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

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

EmailServiceApp.controller("EmailServiceController", function ($rootScope,$timeout, $scope, $http, $filter, messageAlertEngine, ngTableParams, $sce, $filter) {

    $scope.Emails = [];
    $scope.list = true;
    $scope.compose = false;
    $rootScope.tempObject = {};
    $scope.loadingInboxAjax = true;
    $scope.loadingSentboxAjax = true;
    $scope.loadingOutboxAjax = true;

    $http.get(rootDir + '/EmailService/GetAllEmailIds').
      success(function (data, status, headers, config) {
          try {
              $scope.EmailsIds = data;
          } catch (e) {

          }
      }).
      error(function (data, status, headers, config) {
      });

    $http.get(rootDir + '/EmailService/GetAllEmails').
       success(function (data, status, headers, config) {

           try {
               if (data != null) {
                   $scope.Emails = data;
               }
               for (var i = 0; i < $scope.Emails.length; i++) {
                   $scope.Emails[i].LastModifiedDate = $scope.ConvertDateFormat($scope.Emails[i].LastModifiedDate);
               }
               $scope.searchBoxData();
               $scope.loadingSentboxAjax = false;
               $scope.condition = 2;
               $scope.init_table($scope.Emails);
           } catch (e) {
               throw e;
           }
          }).
       error(function (data, status, headers, config) {
       });

    
    $http.get(rootDir + '/EmailService/GetAllActiveFollowUpEmails').
       success(function (data, status, headers, config) {
           try {

               if (data != null) {
                   $scope.followUpEmails = data;
               }
               for (var i = 0; i < $scope.followUpEmails.length; i++) {
                   $scope.followUpEmails[i].LastModifiedDate = $scope.ConvertDateFormat($scope.followUpEmails[i].LastModifiedDate);
               }
               $scope.loadingOutboxAjax = false;
               $scope.searchBoxData();
               $scope.condition = 4;
               $scope.init_table($scope.followUpEmails);
           } catch (e) {
              
           }
          

       }).
       error(function (data, status, headers, config) {
       });


    $http.get(rootDir + '/EmailService/GetAllInboxEmails').
       success(function (data, status, headers, config) {

           try {
               if (data != null) {
                   $scope.recievedEmails = data;
               }
               for (var i = 0; i < $scope.recievedEmails.length; i++) {
                   $scope.recievedEmails[i].LastModifiedDate = $scope.ConvertDateFormat($scope.recievedEmails[i].LastModifiedDate);
               }
               $scope.loadingInboxAjax = false;
               $scope.searchBoxData();
               $scope.condition = 1;
               $scope.init_table($scope.recievedEmails);
           } catch (e) {
             
           }

       }).
       error(function (data, status, headers, config) {
       });

    $http.get(rootDir + '/EmailService/GetAllEmailTemplates').
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


   
    $scope.searchData = [];

    $scope.searchBoxData = function () {
        var isPresent = false;
        $scope.searchData.push($scope.Emails[0]);
        for (var i = 1; i < $scope.Emails.length; i++){
            for (var j = 0; j < $scope.searchData.length; j++) {
                if ($scope.Emails[i].From != "" && $scope.searchData[j]) {
                    if ($scope.Emails[i].From == $scope.searchData[j].From) {
                        isPresent = true;
                        break;
                    }
                }                
            }
            if (!isPresent) {
                $scope.searchData.push($scope.Emails[i]);
            }
            isPresent = false;
        }
    }

    $scope.stopFollowUpForRecipient = function (emailInfoID, recipientID) {
        $.ajax({
            url: rootDir + '/EmailService/StopFollowUpEmailForSelectReceiversAsync?emailInfoID=' + emailInfoID + '&recipientID=' + recipientID,
            type: 'POST',
            async: false,
            cache: false,
            contentType: false,
            processData: false,
            success: function (data) {
                
                try {
                    if (data.status == "true") {
                        var obj = $filter('filter')($scope.followUpEmails, { EmailInfoID: data.email.EmailInfoID })[0];
                        for (var i = 0; i < obj.EmailRecipients.length; i++) {
                            if (obj.EmailRecipients[i].EmailRecipientDetailID == recipientID) {
                                var recipient = obj.EmailRecipients[i];
                                obj.EmailRecipients.splice(obj.EmailRecipients.indexOf(recipient), 1);
                            }
                        }
                        if (data.email.Status == 'Inactive') {
                            $scope.followUpEmails.splice($scope.followUpEmails.indexOf(obj), 1);
                            messageAlertEngine.callAlertMessage('successMsgDiv', "Follow Up stopped for the requested recipient.", "success", true);
                        }
                        else {
                            messageAlertEngine.callAlertMessage('successMsgDiv', "Only one recipient remained. Follow Up stopped.", "success", true);
                        }
                        $scope.list = true;
                        $scope.tableParamsFollowUp.reload();
                    } else {
                        messageAlertEngine.callAlertMessage('errorMsgDiv', data.status, "danger", true);
                        $scope.errorMsg = data.status;
                    }
                } catch (e) {
                   
                }
            },
            error: function (e) {
                messageAlertEngine.callAlertMessage('errorMsgDiv', data.status, "danger", true);
                $scope.errorMsg = "Unable to stop Follow Up.";
            }
        });
    };


    //Multiple Document ADD to DisClosure Question Start

    var QID = 0;
    var index1 = -1;
    $scope.addingDocument = function () {
        //index1 = $scope.masterQuestions.indexOf(QData);
        //QID = QuestionID;
        $('#file').click();
    }

    $scope.fileList = [];
    $scope.curFile;
    $scope.ImageProperty = {
        file: '',
        //QID: -1,
        FileListID: -1,
        FileID: -1,
        FileStatus: ''
    }
    $scope.removeFile = function (index,fileObj) {  
        $scope.fileList.splice(index,1)
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
        files = [];
        files = element.files;
        tempmultiplefilelength = $scope.fileList.length;
        //for (var i in $scope.fileList) {
        //    if ($scope.fileList[i].QuestionID == QID) {
        //        index = i;
        //        count++;
        //        break;
        //    }
        //}
        if (count == 0) {
            for (var i = 0; i < files.length; i++) {
                var TempArray = [];
                $scope.ImageProperty.file = files[i];
                //$scope.ImageProperty.QID = QID;
                $scope.ImageProperty.FileStatus = 'Active';
                $scope.ImageProperty.FileListID = $scope.fileList.length;
                $scope.ImageProperty.FileID = i;
                TempArray.push($scope.ImageProperty);
                $scope.fileList.push({ File: TempArray });
                //$scope.masterQuestions[index1].TableStatus = true;
                $scope.ImageProperty = {};
                $scope.$apply();
            }
        }
        else {
            for (var i = 0; i < files.length; i++) {
                $scope.ImageProperty.file = files[i];
                //$scope.ImageProperty.QID = QID;
                $scope.ImageProperty.FileStatus = 'Active';
                $scope.ImageProperty.FileListID = index;
                $scope.ImageProperty.FileID = $scope.fileList[index].File.length + i;
                $scope.fileList[index].File.push($scope.ImageProperty);
                //$scope.masterQuestions[index1].TableStatus = true;
                $scope.ImageProperty = {};
                $scope.$apply();
            }
        }
        $scope.UploadFile();

    }

    $scope.UploadFile = function () {

        for (var i = 0; i < $scope.fileList.length; i++) {
            for (var j = 0; j < $scope.fileList[i].File.length; j++) {
                if ($scope.fileList[i].File[j].FileStatus == 'Active') {
                    $scope.UploadFileIndividual($scope.fileList[i].File[j].file,
                                        $scope.fileList[i].File[j].file.name,
                                        $scope.fileList[i].File[j].file.type,
                                        $scope.fileList[i].File[j].file.size,
                                        //$scope.fileList[i].File[j].QID,
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
            } else if (files.length != 1 && tempmultiplefilelength != 0) {
                $scope.fileList[tempmultiplefilelength].File[0].path = resultdata.FilePath;
                $scope.fileList[tempmultiplefilelength].File[0].relativePath = resultdata.RelativePath;
                tempmultiplefilelength++;
            } else {
                $scope.fileList[tempIndex].File[0].path = resultdata.FilePath;
                $scope.fileList[tempIndex].File[0].relativePath = resultdata.RelativePath;
                tempIndex++;
            }
            $scope.NoOfFileSaved++;
            $scope.$apply();
        }

        function uploadFailed(evt) {
            alert("Failed");
        }

        function uploadCanceled(evt) {

            alert("Canceled");
        }

    }

    $scope.Attachments = [];
    //END

    $scope.stopFollowUP = function (emailInfoID) {
        $.ajax({
            url: rootDir + '/EmailService/StopFollowUpEmail?emailInfoID=' + emailInfoID,
            type: 'POST',
            async: false,
            cache: false,
            contentType: false,
            processData: false,
            success: function (data) {
               
                try {
                    if (data.status == "true") {
                        var obj = $filter('filter')($scope.followUpEmails, { EmailInfoID: data.email.EmailInfoID })[0];
                        $scope.followUpEmails.splice($scope.followUpEmails.indexOf(obj), 1);
                        $scope.list = true;
                        $scope.tableParamsFollowUp.reload();
                        messageAlertEngine.callAlertMessage('successMsgDiv', "Follow Up stopped.", "success", true);
                    } else {
                        messageAlertEngine.callAlertMessage('errorMsgDiv', data.status, "danger", true);
                        $scope.errorMsg = data.status;
                    }
                } catch (e) {
                
                }
            },
            error: function (e) {
                messageAlertEngine.callAlertMessage('errorMsgDiv', data.status, "danger", true);
                $scope.errorMsg = "Unable to stop Follow Up.";
            }
        });
    };

    $scope.templateSelected = true;
    $scope.changeYesNoOption = function (value) {
        $("#newEmailForm .field-validation-error").remove();
            $scope.templateSelected = false;
            $scope.tempObject.Subject = "";
            $scope.tempObject.Body = "";

            if (value == 2) {
                $scope.templateSelected = true;
            } else { $scope.tempObject.Title = ""; }

    }

    $scope.showContent = function () {
        $scope.templateSelected = true;

    }

    $scope.getTabData = function (tabName) {
        $rootScope.$broadcast(tabName);
    }

    $scope.renderHtml = function (htmlCode) {
        return $sce.trustAsHtml(htmlCode);
    };

    $scope.closeList = function() {
        $scope.list = true;
    }

    $scope.SentStatus = false;
    $rootScope.$on("Inbox", function () {
        //============================= Data From Master Data Table Required For Inbox ======================
        $scope.list = true;
        $scope.condition = 1;
        $scope.init_table();
        //$scope.init_table($scope.Emails);
    });

    $rootScope.$on("outbox", function () {
        //============================= Data From Master Data Table Required For Inbox ======================
        $scope.list = true;
        $scope.condition = 4;
        $scope.init_table($scope.followUpEmails);
       
        //$scope.init_table($scope.Emails);
    });

    $rootScope.$on("Sent", function () {
        //============================= Data From Master Data Table Required For Sent Mails ======================
        $scope.list = true;
        $scope.SentStatus = true;
        $scope.condition = 2;
        $scope.init_table($scope.Emails);
        //$scope.init_table($scope.Emails);
    });

    $rootScope.$on("All", function () {
        //============================= Data From Master Data Table Required For All Mails ======================
        $scope.list = true;
        //$scope.init_table($scope.Emails);
    });

    $scope.viewDetails = function (data) {
        $scope.list = false;
        $scope.Details = angular.copy(data);
       }
    $scope.$watchCollection("Details", function () {
        
        $timeout(function () {
            
            $(".bodyForMail").find("a").each(function () {
                $(this).attr("target", "_blank");
            })
        }, 1000)

    })
   


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

    //$http.get(rootDir + '/EmailService/GetAllEmails').
    //   success(function (data, status, headers, config) {
    //       try {
    //           $scope.Emails = data;
    //       } catch (e) {

    //       }


    //   }).
    //   error(function (data, status, headers, config) {
    //   });
    var availableTags = [];
    $scope.showCompose = function () {
        //availableTags = angular.copy($scope.EmailsIds);
        availableTags =
            [
                'tulasidhar.salla@pratian.com',
                'bindupriya.ambati@pratian.com',
                'manideep.innamuri@pratian.com',
                'sharath.km@pratian.com'

            ];
        //$("#newEmailForm .field-validation-error").remove();
        $("#newEmailForm .field-validation-error").hide();
        $scope.compose = true;
    }

    $scope.closeCompose = function () {
        $scope.compose = false;
    }

    $scope.hideDiv = function () {
        $('.TemplateSelectAutoList').hide();
        $scope.errorMsg = false;
    }

    $scope.AddNewEmailTemplate = false;
    $scope.isTrue = false;
    $scope.data = [];
    $scope.loadingAjax = true;
    //$scope.GetAllEmailTemplate = function () {
    //    $http.get(rootDir + '/EmailTemplate/GetAllSaveEmailTemplate').
    //    success(function (data, status, headers, config) {
    //        for (var c = 0; c < data.length; c++) {
    //            data[c].EditStatus = false;
    //        }
    //        $scope.data = angular.copy(data);
    //        $scope.init_table($scope.data);
    //        $scope.loadingAjax = false;
    //    }).
    //    error(function (data, status, headers, config) {
    //        messageAlertEngine.callAlertMessage('EmailTemplateMessage', "Please Try again later ....", "danger", true);
    //    });
    //}
    //$scope.GetAllEmailTemplate();

    //function ResetFormForValidation(form) {
    //    form.removeData('validator');
    //    form.removeData('unobtrusiveValidation');
    //    $.validator.unobtrusive.parse(form);
    //}

    $scope.ResetFormForValidation = function (form) {
        form.removeData('validator');
        form.removeData('unobtrusiveValidation');
        $.validator.unobtrusive.parse(form);
    }
    
    $scope.errmsgforTo = false;
    $scope.errmsgforCC = false;
    $scope.errmsgforBCC = false;
    $scope.errmsg = false;

    $scope.AddOrSaveEmail = function (Form_Id) {
        $scope.ResetFormForValidation($("#" + Form_Id));
        if ($('#tags').val() == "") {
            $scope.errmsg = true;
            $scope.errmsgforTo = false;
        }
        else {
            $scope.errmsg = false;
            var regx1 = /^[a-z][a-zA-Z0-9_]*(\.[a-zA-Z][a-zA-Z0-9_]*)?@[a-z][a-zA-Z-0-9]*\.[a-z]+(\.[a-z]+)?$/;
            var emailData = $('#tags').val().split(';');
            for (var i in emailData) {
                if (emailData[i] != "") {
                    if (regx1.test(emailData[i].toLowerCase())) {
                        $scope.errmsgforTo = false;
                    }
                    else { $scope.errmsgforTo = true; }
                }
            }

            var emailDataCC = $('#tagsCC').val().split(';');
            for (var i in emailDataCC) {
                if (emailDataCC[i] != "") {
                    if (regx1.test(emailDataCC[i].toLowerCase())) {
                        $scope.errmsgforCC = false;
                    }
                    else { $scope.errmsgforCC = true; }
                }
            }

            var emailDataBCC = $('#tagsBCC').val().split(';');
            for (var i in emailDataBCC) {
                if (emailDataBCC[i] != "") {
                    if (regx1.test(emailDataBCC[i].toLowerCase())) {
                        $scope.errmsgforBCC = false;
                    }
                    else { $scope.errmsgforBCC = true; }
                }
            }
        }
        if ($("#" + Form_Id).valid() &&!$scope.errmsg && !$scope.errmsgforBCC && !$scope.errmsgforCC && !$scope.errmsgforTo) {

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
                url: rootDir + '/EmailService/AddEmail',
                type: 'POST',
                data: new FormData($form),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    try {
                        if (data.status == "true") {
                            $scope.compose = false;
                            //$scope.tableParamsFollowUp.reload();
                            //$scope.tableParamsSent.reload();
                            messageAlertEngine.callAlertMessage('successMsgDiv', "Email scheduled for sending.", "success", true);
                            $scope.getTabData('Sent');
                        }
                        else {
                            messageAlertEngine.callAlertMessage('errorMsgDiv', data.status, "danger", true);
                            $scope.errorMsg = data.status;
                        }
                    } catch (e) {
                      
                    }
                },
                error: function (data) {
                    messageAlertEngine.callAlertMessage('errorMsgDiv', data.status, "danger", true);
                    $scope.errorMsg = "Unable to schedule Email.";
                }
            });
            location.reload(false);
            $scope.tempObject.To ="";
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
        else
        {
            $("#newEmailForm .field-validation-error").show();
        }


        //$scope.tempObject = [];
        //for (var c = 0; c < $scope.data.length; c++) {
        //    $scope.data[c].EditStatus = false;
        //}
        //$scope.tableParams.reload();
        //$scope.AddNewEmailTemplate = !$scope.AddNewEmailTemplate;

    }
    $scope.Cancle = function () {
        $scope.AddNewEmailTemplate = !$scope.AddNewEmailTemplate;
       
    }
    $scope.EditCancle = function (temp) {
        var formdata = $('#newEmailForm');
        formdata[0].reset();
        $scope.tempObject.SaveAsTemplateYesNoOption = 2;
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
    $scope.AddingEmailTemplate = function (Form_Id) {
        ResetFormForValidation($("#" + Form_Id));
        if ($("#" + Form_Id).valid()) {

            alert($('#Body').val());
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

            alert($('#Body').val());

            var $form = ($("#" + Form_Id)[0]);

            $.ajax({
                url: rootDir + '/EmailTemplate/SaveEmailTemplate',
                type: 'POST',
                data: new FormData($form),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    try {
                        if (data.status == "true") {
                            var count = 0;
                            $scope.tempObject.IntervalFactor = "";
                            data.emailTemplate.EditStatus = false;
                            for (var c = 0; c < $scope.data.length; c++) {
                                if ($scope.data[c].EmailTemplateID == data.emailTemplate.EmailTemplateID) {
                                    $scope.data[c] = data.emailTemplate;
                                    $scope.data[c].EditStatus = false;
                                    count++;
                                    break;
                                }
                            }
                            if (count == 0) {
                                $scope.data.push(data.emailTemplate);
                            }
                            $scope.tableParams.reload();
                            $scope.AddNewEmailTemplate = false;
                            messageAlertEngine.callAlertMessage('EmailTemplateMessage', "Email Template is Successfully Added !!!", "success", true);
                        }
                        else {
                            for (var c = 0; c < $scope.data.length; c++) {
                                if ($scope.data[c].EmailTemplateID == data.emailTemplate.EmailTemplateID) {
                                    $scope.data[c].EditStatus = false;
                                    $scope.tableParams.reload();
                                    break;
                                }
                            }
                            $scope.AddNewEmailTemplate = false;
                            messageAlertEngine.callAlertMessage('EmailTemplateMessage', data.status, "danger", true);
                        }
                    } catch (e) {
                       
                    }
                },
                error: function (data) {
                    $scope.AddNewEmailTemplate = true;
                    messageAlertEngine.callAlertMessage('EmailTemplateMessage', "Please Try again later ....", "danger", true);
                }
            });
        }
    }

    //$scope.myfunc = function () {
    $('#tags').keypress(function () {
            $('#ui-id-1').css({ 'overflow-y': 'scroll' });
            $("#ui-id-1").css("overflow", "scroll");
        });
        
        
    //}

    $scope.EditEmailTemplate = function (dataEdit) {
        if (dataEdit.EditStatus == false) {
            $scope.tempObject = [];
            for (var c = 0; c < $scope.data.length; c++) {
                $scope.data[c].EditStatus = false;
            }
            $scope.data[$scope.data.indexOf(dataEdit)].EditStatus = true;
            $scope.tableParams.reload();
            $scope.tempObject = angular.copy(dataEdit);
        }
        else {
            $scope.data[$scope.data.indexOf(dataEdit)].EditStatus = false;
            $scope.tableParams.reload();
        }
    }

    ///Created function to be called when data loaded dynamically
    $scope.init_table = function (data) {

        $scope.data = data;
        var counts = [];

        if ($scope.data.length <= 10) {
            counts = [];
        }
        else if ($scope.data.length <= 25) {
            counts = [10, 25];
        }
        else if ($scope.data.length <= 50) {
            counts = [10, 25, 50];
        }
        else if ($scope.data.length <= 100) {
            counts = [10, 25, 50, 100];
        }
        else if ($scope.data.length > 100) {
            counts = [10, 25, 50, 100];
        }
        if ($scope.condition == 1) {
            $scope.tableParamsInbox = new ngTableParams({
                page: 1,            // show first page
                count: 10,          // count per page
                filter: {
                    //name: 'M'       // initial filter
                    //FirstName : ''
                },
                sorting: {
                    //LastModifiedDate: 'desc'
                    //Name: 'desc'
                    //name: 'asc'     // initial sorting
                }
            }, {
                counts: counts,
                total: $scope.data.length, // length of data
                getData: function ($defer, params) {
                    // use build-in angular filter
                    var filteredData = params.filter() ?
                            $filter('filter')($scope.data, params.filter()) :
                            $scope.data;
                    var orderedData = params.sorting() ?
                            $filter('orderBy')(filteredData, params.orderBy()) :
                            $scope.data;

                    params.total(orderedData.length); // set total for recalc pagination
                    $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                }
            });
        } else if ($scope.condition == 2) {
            $scope.tableParamsSent = new ngTableParams({
                page: 1,            // show first page
                count: 10,          // count per page
                filter: {
                    //name: 'M'       // initial filter
                    //FirstName : ''
                },
                sorting: {
                    LastModifiedDate: 'desc'
                    //Name: 'desc'
                    //name: 'asc'     // initial sorting
                }
            }, {
                counts: counts,
                total: $scope.data.length, // length of data
                getData: function ($defer, params) {
                    // use build-in angular filter
                    var filteredData = params.filter() ?
                            $filter('filter')($scope.data, params.filter()) :
                            $scope.data;
                    var orderedData = params.sorting() ?
                            $filter('orderBy')(filteredData, params.orderBy()) :
                            $scope.data;

                    params.total(orderedData.length); // set total for recalc pagination
                    $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                }
            });
        } else if ($scope.condition == 3) {
            $scope.tableParamsAll = new ngTableParams({
                page: 1,            // show first page
                count: 10,          // count per page
                filter: {
                    //name: 'M'       // initial filter
                    //FirstName : ''
                },
                sorting: {
                    //Name: 'desc'
                    //name: 'asc'     // initial sorting
                }
            }, {
                counts: counts,
                total: $scope.data.length, // length of data
                getData: function ($defer, params) {
                    // use build-in angular filter
                    var filteredData = params.filter() ?
                            $filter('filter')($scope.data, params.filter()) :
                            $scope.data;
                    var orderedData = params.sorting() ?
                            $filter('orderBy')(filteredData, params.orderBy()) :
                            $scope.data;

                    params.total(orderedData.length); // set total for recalc pagination
                    $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                }
            });
        } else if ($scope.condition == 4) {
            $scope.tableParamsFollowUp = new ngTableParams({
                page: 1,            // show first page
                count: 10,          // count per page
                filter: {
                    //name: 'M'       // initial filter
                    //FirstName : ''
                },
                sorting: {
                    LastModifiedDate: 'desc'
                    //Name: 'desc'
                    //name: 'asc'     // initial sorting
                }
            }, {
                counts: counts,
                total: $scope.data.length, // length of data
                getData: function ($defer, params) {
                    // use build-in angular filter
                    var filteredData = params.filter() ?
                            $filter('filter')($scope.data, params.filter()) :
                            $scope.data;
                    var orderedData = params.sorting() ?
                            $filter('orderBy')(filteredData, params.orderBy()) :
                            $scope.data;

                    params.total(orderedData.length); // set total for recalc pagination
                    $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                }
            });
        }

    };

    //if filter is on
    $scope.ifFilter = function () {
        try {
            var bar;
            //var obj = $scope.tableParamsInbox.$params.filter;
            if ($scope.condition == 1) {
                var obj = $scope.tableParamsInbox.$params.filter;
            } else if ($scope.condition == 2) {
                var obj = $scope.tableParamsSent.$params.filter;
            } else if ($scope.condition == 3) {
                var obj = $scope.tableParamsAll.$params.filter;
            } else if ($scope.condition == 4) {
                var obj = $scope.tableParamsFollowUp.$params.filter;
            }
            for (bar in obj) {
                if (obj[bar] != "") {
                    return false;
                }
            }
            return true;
        }
        catch (e) { return true; }
    }

    //Get index first in table
    $scope.getIndexFirst = function () {
        try {
            if ($scope.groupBySelected == 'none') {
                //return ($scope.tableParamsInbox.$params.page * $scope.tableParamsInbox.$params.count) - ($scope.tableParamsInbox.$params.count - 1);
                if ($scope.condition == 1) {
                    return ($scope.tableParamsInbox.$params.page * $scope.tableParamsInbox.$params.count) - ($scope.tableParamsInbox.$params.count - 1);
                } else if ($scope.condition == 2) {
                    return ($scope.tableParamsSent.$params.page * $scope.tableParamsSent.$params.count) - ($scope.tableParamsSent.$params.count - 1);
                } else if ($scope.condition == 3) {
                    return ($scope.tableParamsAll.$params.page * $scope.tableParamsAll.$params.count) - ($scope.tableParamsAll.$params.count - 1);
                } else if ($scope.condition == 4) {
                    return ($scope.tableParamsFollowUp.$params.page * $scope.tableParamsFollowUp.$params.count) - ($scope.tableParamsFollowUp.$params.count - 1);
                }
            }
        }
        catch (e) { }
    }

    //Get index Last in table
    $scope.getIndexLast = function () {
        try {
            if ($scope.groupBySelected == 'none') {
                //return { true: ($scope.data.length), false: ($scope.tableParamsInbox.$params.page * $scope.tableParamsInbox.$params.count) }[(($scope.tableParamsInbox.$params.page * $scope.tableParamsInbox.$params.count) > ($scope.data.length))];
                if ($scope.condition == 1) {
                    return { true: ($scope.data.length), false: ($scope.tableParamsInbox.$params.page * $scope.tableParamsInbox.$params.count) }[(($scope.tableParamsInbox.$params.page * $scope.tableParamsInbox.$params.count) > ($scope.data.length))];
                } else if ($scope.condition == 2) {
                    return { true: ($scope.data.length), false: ($scope.tableParamsSent.$params.page * $scope.tableParamsSent.$params.count) }[(($scope.tableParamsSent.$params.page * $scope.tableParamsSent.$params.count) > ($scope.data.length))];
                } else if ($scope.condition == 3) {
                    return { true: ($scope.data.length), false: ($scope.tableParamsAll.$params.page * $scope.tableParamsAll.$params.count) }[(($scope.tableParamsAll.$params.page * $scope.tableParamsAll.$params.count) > ($scope.data.length))];
                } else if ($scope.condition == 4) {
                    return { true: ($scope.data.length), false: ($scope.tableParamsFollowUp.$params.page * $scope.tableParamsFollowUp.$params.count) }[(($scope.tableParamsFollowUp.$params.page * $scope.tableParamsFollowUp.$params.count) > ($scope.data.length))];
                }
            }
        }
        catch (e) { }
    }

    $(document).click(function (event) {
        if (!$(event.target).hasClass("form-control") && $(event.target).parents(".TemplateSelectAutoList").length === 0) {
            $(".TemplateSelectAutoList").hide();
        }
    });

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
    

    $(document).ready(function () {
        $(".TemplateSelectAutoList").hide();
        //$(function () {

        ////    //availableTags = $scope.EmailsIds;

        //    function split(val) {
        //        return val.split(/;\s*/);
        //    }
        //    function extractLast(term) {
        //        return split(term).pop();
        //    }

        //    $("#tags,#tagsCC,#tagsBCC")
        //        // don't navigate away from the field on tab when selecting an item
        //        .bind("keydown", function (event) {
        //            if (event.keyCode === $.ui.keyCode.TAB &&
        //                    $(this).autocomplete("instance").menu.active) {
        //                event.preventDefault();
        //            }
        //        })
        //        .autocomplete({
        //            minLength: 0,
        //            source: function (request, response) {
        //                // delegate back to autocomplete, but extract the last term
        //                response($.ui.autocomplete.filter(
        //                    availableTags, extractLast(request.term)));
        //            },
        //            focus: function () {
        //                // prevent value inserted on focus
        //                return false;
        //            },
        //            select: function (event, ui) {
        //                var terms = split(this.value);
        //                // remove the current input
        //                terms.pop();
        //                // add the selected item
        //                terms.push(ui.item.value);
        //                // add placeholder to get the comma-and-space at the end
        //                terms.push("");
        //                this.value = terms.join(";");
        //                return false;
        //            }
        //        });
        //});

    });

    function showTemplateList(ele) {
        $(ele).parent().find(".TemplateSelectAutoList").first().show();
    }
});

EmailServiceApp.config(function ($datepickerProvider) {

    angular.extend($datepickerProvider.defaults, {
        startDate: 'today',
        autoclose: true,
        useNative: true
    });
});

function ResetFormForValidation(form) {
    form.removeData('validator');
    form.removeData('unobtrusiveValidation');
    $.validator.unobtrusive.parse(form);
};
