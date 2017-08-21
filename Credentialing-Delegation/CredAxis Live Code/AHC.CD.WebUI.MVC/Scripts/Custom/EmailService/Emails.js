var EmailServiceApp = angular.module("EmailServiceApp", ["ui.bootstrap", 'mgcrea.ngStrap', 'ngAnimate', 'toaster', "wysiwyg.module", "smart-table", "ngTable", 'colorpicker.module']);

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

EmailServiceApp.factory('Resource', ['$q', '$rootScope', '$filter', '$timeout', function ($q, $rootScope, $filter, $timeout) {
    function getPage(start, number, params) {
        var deferred = $q.defer();

        $rootScope.filtered = params.search.predicateObject ? $filter('filter')($rootScope.emailItems, params.search.predicateObject) : $rootScope.emailItems;

        if (params.sort.predicate) {
            $rootScope.filtered = $filter('orderBy')($rootScope.filtered, params.sort.predicate, params.sort.reverse);
        }
        var result = $rootScope.filtered.slice(start, start + number);
        $timeout(function () {
            deferred.resolve({
                data: result,
                numberOfPages: Math.ceil($rootScope.filtered.length / number)
            });
        });
        return deferred.promise;
    }

    return {
        getPage: getPage
    };
}])

EmailServiceApp.controller("EmailServiceController", function ($rootScope, $timeout, $scope, $http, $filter, messageAlertEngine, $sce, $filter, Resource) {
    $scope.tomorrow = new Date(new Date().getTime() + 24 * 60 * 60 * 1000);
    $scope.today = new Date();
    $scope.Emails = [];
    $scope.list = true;
    $scope.compose = false;
    $rootScope.tempObject = {};
    $scope.loadingInboxAjax = true;
    $scope.loadingSentboxAjax = true;
    $scope.LOadingID = "disabled";
    $scope.loadingOutboxAjax = true;
    $rootScope.visibility = "mail";
    $scope.MailIdsforGroup = [];
    $scope.GroupMailsForAutosuggest = [];
    $rootScope.groupMailList = [];
    $scope.groupNames = [];
    $scope.globalSearchBoxData = '';
    $rootScope.emailItems = [];
    $rootScope.messageTabName = 'Sent';
    $rootScope.GlobalSearchbox = true;
    var ctrl = this;
    this.displayed = [];
    var pipecall = 0;
    this.callServer = function callServer(tableState) {
        ctrl.isLoading = true;
        var pagination = tableState.pagination;
        var start = pagination.start || 0;
        var number = pagination.number || 10;
        $scope.t = tableState;
        Resource.getPage(start, number, tableState).then(function (result) {
            ctrl.displayed = result.data;
            ctrl.temp = ctrl.displayed;
            tableState.pagination.numberOfPages = result.numberOfPages;//set the number of pages so the pagination can update
            ctrl.isLoading = false;
        });
    };

    $scope.GetAllEmailIds = function () {
        var d1 = new $.Deferred();
        $http.get(rootDir + '/EmailService/GetAllEmailIds').
          success(function (data, status, headers, config) {
              try {
                  $scope.EmailsIds = angular.copy(data);
                  //$scope.MailIdsforGroup = angular.copy(data);
                  d1.resolve(data);
              } catch (e) {
              }
          }).
          error(function (data, status, headers, config) {
              return d1.promise();
          });
        return d1.promise();
    }

    //$http.get(rootDir + '/EmailService/GetAllGroupMailNames').
    //  success(function (data, status, headers, config) {

    //      try {
    //          $scope.groupNames = data;
    //      } catch (e) {
    //          throw e;
    //      }
    //  }).
    //  error(function (data, status, headers, config) {
    //  });
    $scope.GetAllGroupMails = function () {
        var d2 = new $.Deferred();
        $http.get(rootDir + '/EmailService/GetAllGroupMailNames').
          success(function (data, status, headers, config) {
              try {
                  $scope.groupNames = data;
                  d2.resolve(data);
              } catch (e) {
                  throw e;
              }
          }).
          error(function (data, status, headers, config) {
              return d2.promise();
          });
        return d2.promise();
    }

    var promise = $scope.GetAllEmailIds().then(function () {
        $scope.GetAllGroupMails().then(function () {
            for (i = 0; i < $scope.groupNames.length; i++) {
                $scope.EmailsIds.push($scope.groupNames[i]);
            }
        });
    });

    $http.get(rootDir + '/EmailService/GetAllEmails').
       success(function (data, status, headers, config) {

           try {
               if (data != null) {
                   $scope.Emails = data;
               }
               for (var i = 0; i < $scope.Emails.length; i++) {
                   $scope.Emails[i].LastModifiedDate = $scope.ConvertDateFormat($scope.Emails[i].LastModifiedDate);
                   $scope.Emails[i].Date = $scope.ConvertDateFormatForEmails($scope.Emails[i].LastModifiedDate);
               }
               $scope.searchBoxData($scope.Emails);
               $scope.loadingSentboxAjax = false;
               $scope.LOadingID = "";
               $scope.condition = 2;
               $rootScope.emailItems = angular.copy($scope.Emails);
               $scope.t = resetTableState();
               ctrl.callServer($scope.t);
               $rootScope.messageTabName = 'Sent';
               //$scope.init_table($scope.Emails, $scope.condition);
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
                   $rootScope.followUpEmails = data;
               }
               for (var i = 0; i < $rootScope.followUpEmails.length; i++) {
                   $rootScope.followUpEmails[i].LastModifiedDate = $scope.ConvertDateFormat($rootScope.followUpEmails[i].LastModifiedDate);
                   $scope.followUpEmails[i].Date = $scope.ConvertDateFormatForEmails($scope.followUpEmails[i].LastModifiedDate);
               }
               $scope.loadingOutboxAjax = false;
               $scope.searchBoxData($rootScope.followUpEmails);
               $scope.condition = 4;
               //$scope.init_table($scope.followUpEmails, $scope.condition);
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
               $scope.searchBoxData($scope.recievedEmails);
               $scope.condition = 1;
               $scope.init_table($scope.recievedEmails, $scope.condition);
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

    $scope.searchCumDropDown = function (divId) {
        $(".ProviderTypeSelectAutoList1").hide();
        $("#" + divId).show();
    }
    //$scope.tempGroupMailIDS = angular.copy(MailIdsforGroup);
    $scope.showView = function () {
        $rootScope.visibility = "groupMailList";
        $scope.grpNameErrorMessage = false;
        $scope.grpMailIdsErrorMessage = false;
        $rootScope.tableView = true;
        $rootScope.isView = false;
        $rootScope.isEdit = false;
        //$scope.groupMail = true;
        //$scope.groupMailList = true;
        $scope.selectedMail = [];
        $scope.tempGroupMailIDS = angular.copy($scope.MailIdsforGroup);
        $scope.GroupEmailIds = "";
        $rootScope.tableView = true;
        $scope.groupMailID = "";
        $rootScope.mails();
    }

    $scope.groupMailID;
    $scope.cancelGroupView = function () {
        //$scope.groupMail = false;
        $rootScope.visibility = "mail";
        //$scope.groupMailList = false;
        $scope.groupMailID = "";
        $rootScope.tableView = false;
        $rootScope.isView = false;
        $rootScope.isEdit = false;
    }
    $scope.GroupEmailIds = "";
    $scope.grpNameErrorMessage = false;
    $scope.grpMailIdsErrorMessage = false;
    $scope.saveGroup = function (Form_Id) {
        var regx1 = /^[a-z][a-zA-Z0-9_]*(\.[a-zA-Z][a-zA-Z0-9_]*)?@[a-z][a-zA-Z-0-9]*\.[a-z]+(\.[a-z]+)?$/;
        $scope.grpNameErrorMessage = false;
        $scope.grpMailIdsErrorMessage = false;
        $scope.InvalidgrpNameErrorMessage = false;
        if ($scope.groupMailID == "" || $scope.groupMailID == null) {
            $scope.grpNameErrorMessage = true;
        }
        else {
            if (regx1.test($scope.groupMailID.toLowerCase()) && $scope.grpNameErrorMessage != true) { $scope.InvalidgrpNameErrorMessage = false; } else {
                $scope.grpNameErrorMessage = false;
                $scope.InvalidgrpNameErrorMessage = true;
            }
        }
        if ($scope.selectedMail.length == 0 || $scope.selectedMail == []) { $scope.grpMailIdsErrorMessage = true; }
        //$scope.InvalidgrpNameErrorMessage = regx1.test($scope.groupMailID.toLowerCase()) ? true && ($scope.grpNameErrorMessage = false) : false;

        $scope.GroupEmailIds = JSON.stringify($scope.selectedMail);
        var obj = {
            EmailGroupName: $scope.groupMailID,
            EmailIds: $scope.GroupEmailIds
        }
        if (!$scope.grpNameErrorMessage && !$scope.grpMailIdsErrorMessage) {
            $http.post(rootDir + '/EmailService/AddGroupEmail', obj).
               success(function (data, status, headers, config) {
                   try {
                       if (data.status == "true") {
                           $scope.grpNameErrorMessage = false;
                           $scope.grpMailIdsErrorMessage = false;
                           messageAlertEngine.callAlertMessage('grpsuccessMsgDiv', "New Group Mail Created.", "success", true);
                           $scope.groupMail = false;
                       } else if (data.status == "Group Email already Exists") {
                           messageAlertEngine.callAlertMessage('grperrorMsgDiv', "Group Mail already exists.", "danger", true);
                       } else {
                           messageAlertEngine.callAlertMessage('grperrorMsgDiv', "Please Try again later ....", "danger", true);
                       }
                   } catch (e) {

                   }
               }).
               error(function (data, status, headers, config) {
                   //----------- error message -----------
               });
        }
    }
    $scope.selectedMail = [];
    $scope.mailList = [];
    $scope.Selectmail = function (mail) {
        $scope.selectedMail.push(mail);

        $scope.tempGroupMailIDS = $.grep($scope.tempGroupMailIDS, function (m) {
            return m != mail;
        })
        $(".ProviderTypeSelectAutoList").hide();
    }

    $scope.removeMail = function (mail) {
        $scope.tempGroupMailIDS.push(mail);
        $scope.selectedMail = $.grep($scope.selectedMail, function (m) {
            return m != mail;
        })
    }

    $scope.searchData = [];

    //$scope.searchBoxData = function () {
    //    var isPresent = false;
    //    $scope.searchData.push($scope.Emails[0]);
    //    for (var i = 1; i < $scope.Emails.length; i++) {
    //        for (var j = 0; j < $scope.searchData.length; j++) {
    //            if ($scope.Emails[i].From != "" && $scope.searchData[j]) {
    //                if ($scope.Emails[i].From == $scope.searchData[j].From) {
    //                    isPresent = true;
    //                    break;
    //                }
    //            }
    //        }
    //        if (!isPresent) {
    //            $scope.searchData.push($scope.Emails[i]);
    //        }
    //        isPresent = false;
    //    }
    //}

    Array.prototype.contains = function (v) {
        for (var i = 0; i < this.length; i++) {
            if (this[i].ToList === v) return true;
        }
        return false;
    };


    $scope.clearFunction = function () {
        $scope.tempObject.From = '';
    }
    //Array.prototype.unique = function () {
    //    var arr = this;
    //    for (var i = 0; i < this.length; i++) {
    //        for (var j = 0; j < arr.length; j++) { 
    //            if (!arr[j].contains(this[i].ToList)) {
    //                arr.push(this[i]);
    //            }
    //        }
    //    }
    //    return arr;
    //}

    Array.prototype.unique = function () {
        var arr = [];
        for (var i = 0; i < this.length; i++) {
            if (!arr.contains(this[i].ToList)) {
                arr.push(this[i]);
            }
        }
        return arr;
    }

    $scope.globalSearch = function (val) {
        console.log($scope.searchData);
        //$scope.globalSearchBoxData = val;
        var res = $filter('filter')($scope.Emails, val);
        //$scope.tempObject.From = res[0].From;        

        //if ($scope.condition == 2) {
        //    //$scope.tableParamsSent.reload();
        //    $scope.tableParamsSent = {};    
        //    $scope.data = [];
        //    $scope.init_table(res, $scope.condition);
        //    //$scope.$apply;
        //    $scope.tableParamsSent.reload();

        //}
        //if ($scope.condition == 1) {
        //    $scope.init_table(res, $scope.condition);
        //    $scope.tableParamsInbox.reload();

        //}
        // if ($scope.condition == 4) {
        //    $scope.init_table(res, $scope.condition);
        //    $scope.tableParamsFollowUp.reload();

        //}
        //if ($scope.condition == 3) {
        //    $scope.init_table(res, $scope.condition);
        //    $scope.tableParamsAll.reload();

        //}
        //if ($scope.condition == 1) {
        //    var obj = $scope.tableParamsInbox.$params.filter;
        //} else if ($scope.condition == 2) {
        //    var obj = $scope.tableParamsSent.$params.filter;
        //} else if ($scope.condition == 3) {
        //    var obj = $scope.tableParamsAll.$params.filter;
        //} else if ($scope.condition == 4) {
        //    var obj = $scope.tableParamsFollowUp.$params.filter;
        //}
        //$scope.data = [];
        //$scope.refreshTable();
        //$scope.tableParamsSent.total = '';



        //$scope.tableParamsSent.reload();
        //$scope.init_table(res, $scope.condition);
        //$scope.tableParamsInbox.reload();
        //$scope.tableParamsFollowUp.reload();

    }

    //$scope.refreshTable = function () {
    //    console.log('\n\n refreshing table')
    //    $scope['tableParamsSent'] = {
    //        reload: function () { },
    //        settings: function () {
    //            return {}
    //        }
    //    };
    //    $timeout(setTable, 100)
    //};

    $scope.searchBoxData = function (data) {
        var j;
        for (var i = 0; i < data.length; i++) {

            //for (var j in data[i].EmailRecipients) {
            for (j = 0; j < data[i].EmailRecipients.length; j++) {
                if (data[i].EmailRecipients[j].RecipientType == "To")
                    data[i].ToList = data[i].EmailRecipients[j].Recipient;
                $scope.searchData.push({ "ToList": data[i].ToList, "EmailInfoID": data[i].EmailInfoID });
                //$scope.searchData = $.grep($scope.searchData, function (element) {
                //    return element.Name != Followup.Name;
                //});
            }
        }
        $scope.searchData = $scope.searchData.unique();
        //console.log($scope.searchData);
        //var uniques = $scope.searchData.unique();
        //console.log(uniques);
        //$scope.
    }

    $scope.myfunction = function (type) {
        $scope.tempObject.From = type.ToList;
        $scope.tempObject.EmailInfoID = type.EmailInfoID;
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
                        var obj = $filter('filter')($rootScope.followUpEmails, { EmailInfoID: data.email.EmailInfoID })[0];
                        for (var i = 0; i < obj.EmailRecipients.length; i++) {
                            if (obj.EmailRecipients[i].EmailRecipientDetailID == recipientID) {
                                var recipient = obj.EmailRecipients[i];
                                obj.EmailRecipients.splice(obj.EmailRecipients.indexOf(recipient), 1);
                            }
                        }
                        if (data.email.Status == 'Active') {

                            messageAlertEngine.callAlertMessage('successMsgDiv', "Follow Up stopped for the requested recipient.", "success", true);
                        }
                        if (data.email.Status == 'Inactive') {
                            $rootScope.followUpEmails.splice($rootScope.followUpEmails.indexOf(obj), 1);
                            messageAlertEngine.callAlertMessage('successMsgDiv', "Only one recipient remained. Follow Up stopped.", "success", true);
                        }
                        $scope.list = true;
                        //$scope.tableParamsFollowUp.reload();
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

    $scope.FilesizeError = false;

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
    $scope.removeFile = function (index, fileObj) {
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
            // alert("Failed");
        }

        function uploadCanceled(evt) {

            //alert("Canceled");
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
                        //var obj = $filter('filter')($scope.data, { EmailInfoID: data.email.EmailInfoID })[0];
                        //$scope.data.splice($scope.data.indexOf(obj), 1);
                        //ngTableParams.reload;

                        $scope.followUpEmails.splice($scope.followUpEmails.indexOf(jQuery.grep($scope.followUpEmails, function (ele) { return ele.EmailInfoID == data.email.EmailInfoID })[0]), 1);
                        $scope.list = true;
                        $rootScope.emailItems = angular.copy($rootScope.followUpEmails);
                        $scope.t = resetTableState();
                        ctrl.callServer($scope.t);

                        messageAlertEngine.callAlertMessage('successMsgDiv', "Follow Up stopped.", "success", true);
                        //$scope.tableParamsFollowUp.reload(); 

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
        if (tabName == 'Sent') {
            $scope.loadingOutboxAjax = false;
            $scope.loadingSentboxAjax = true;
        }
        else if (tabName == 'outbox') {
            $scope.loadingSentboxAjax = false;
            $scope.loadingOutboxAjax = true;
        }
        $rootScope.$broadcast(tabName);
        $rootScope.messageTabName = tabName;
    }

    $scope.renderHtml = function (htmlCode) {
        return $sce.trustAsHtml(htmlCode);
    };

    $scope.closeList = function () {
        $scope.list = true;
    }

    $scope.SentStatus = false;
    var resetTableState = function () {
        var tableState = {
            sort: {},
            search: {},
            pagination: {
                start: 0
            }
        };
        return tableState;
    }

    $rootScope.$on("Inbox", function () {
        //============================= Data From Master Data Table Required For Inbox ======================
        $scope.list = true;
        $scope.condition = 1;
        //$scope.init_table();
        //$scope.init_table($scope.Emails);
        $rootScope.emailItems = angular.copy($scope.Emails);
        $scope.t = resetTableState;
        ctrl.callServer($scope.t);
        

    });

    $rootScope.$on("outbox", function () {
        //============================= Data From Master Data Table Required For Inbox ======================
        $scope.tempObject.From = '';
        $scope.list = true;
        $scope.condition = 4;
        $rootScope.emailItems = angular.copy($rootScope.followUpEmails);
        $scope.t = resetTableState();
        ctrl.callServer($scope.t);
        $scope.loadingOutboxAjax = false;
        //$scope.init_table($scope.followUpEmails, $scope.condition);

        //$scope.init_table($scope.Emails);
    });

    $rootScope.$on("Sent", function () {
        //============================= Data From Master Data Table Required For Sent Mails ======================
        $scope.tempObject.From = '';
        $scope.list = true;
        $scope.SentStatus = true;
        $scope.condition = 2;
        $rootScope.emailItems = angular.copy($scope.Emails);
        $scope.t = resetTableState();
        ctrl.callServer($scope.t);
        $scope.loadingSentboxAjax = false;
        //$scope.init_table($scope.Emails, $scope.condition);
        //$scope.init_table($scope.Emails);
    });

    $rootScope.$on("All", function () {
        //============================= Data From Master Data Table Required For All Mails ======================
        $scope.list = true;
        //$scope.init_table($scope.Emails);
        $rootScope.emailItems = angular.copy($scope.Emails);
        $scope.t = resetTableState();
        ctrl.callServer($scope.t);
    });

    $scope.viewDetails = function (data) {
        $scope.list = false;
        $scope.ToList = [];
        $scope.CcList = [];
        $scope.BccList = [];
        $scope.Details = angular.copy(data);
        for (var i in $scope.Details.EmailRecipients) {
            if ($scope.Details.EmailRecipients[i].Status == 'Active') {
                if ($scope.Details.EmailRecipients[i].RecipientType == 'To') {
                    $scope.ToList.push($scope.Details.EmailRecipients[i]);
                }
                else if ($scope.Details.EmailRecipients[i].RecipientType == 'CC') {
                    $scope.CcList.push($scope.Details.EmailRecipients[i]);
                }
                else {
                    $scope.BccList.push($scope.Details.EmailRecipients[i]);
                }
            }
        }
    }
    $scope.$watchCollection("Details", function () {

        $timeout(function () {

            $(".bodyForMail").find("a").each(function () {
                $(this).attr("target", "_blank");
            })
        }, 1000)

    })

    $scope.ConvertDateFormatForEmails = function (value) {
        var returnValue = value;
        try {
            if (value.indexOf("/Date(") != 0) {
                returnValue = new Date(value).toString();                
            }
        } catch (e) {
            return returnValue;
        }
        try {
            if (value.indexOf("/Date(") == 0) {
                returnValue = new Date(parseInt(value.replace("/Date(", "").replace(")/", ""), 10));
            }
            return returnValue;
        } catch (e) {
            return returnValue;
        }
        
        return returnValue;
    }

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
        availableTags = angular.copy($scope.EmailsIds);
        $('.badge').removeAttr("style");
        //availableTags =
        //    [
        //        'tulasidhar.salla@pratian.com',
        //        'bindupriya.ambati@pratian.com',
        //        'manideep.innamuri@pratian.com',
        //        'sharath.km@pratian.com',
        //        'shalabh@pratian.com'
        //    ];
        ////$("#newEmailForm .field-validation-error").remove();
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
    $scope.FileUploadProgress = false;

    $scope.AddOrSaveEmail = function (Form_Id) {
        $scope.FileUploadProgress = false;
        $scope.errmsgforBCC = false;
        $scope.errmsgforCC = false;
        var AttachmentsSize = 0;
        for (i = 0; i < $scope.fileList.length; i++) {
            AttachmentsSize += $scope.fileList[i].File[0].file.size;
            if ($scope.fileList[i].File[0].relativePath == "" || $scope.fileList[i].File[0].relativePath === undefined) {
                $scope.FileUploadProgress = true;
                messageAlertEngine.callAlertMessage('warningdiv', "File Upload is in Progress", "info", true);
                break;
            }
        }
        if (AttachmentsSize > 15728640) {
            messageAlertEngine.callAlertMessage('warningdiv', 'Files exceeded the size limit!', "info", true);
        }
        else {
        }
        //$scope.ResetFormForValidation($("#" + Form_Id));
        if ($('#tags').val() == "") {
            $scope.errmsg = true;
            $scope.errmsgforTo = false;
        }
        else {
            $scope.errmsg = false;
            var regx1 = /^[a-z][a-zA-Z0-9_]*(\.[a-zA-Z][a-zA-Z0-9_]*)?@[a-z][a-zA-Z-0-9]*\.[a-z]+(\.[a-z]+)?$/;

            var emailData = $('#tags').val().split(';');
            for (i = 0; i < emailData.length; i++) {
                if (emailData[i] != "") {
                    try {
                        if ($scope.EmailsIds != undefined)
                        var Tores = jQuery.grep($scope.EmailsIds, function (ele) { return ele.toLowerCase() == emailData[0].toLowerCase(); });
                    } catch (e) {
                        throw e;
                    }
                    if (regx1.test(emailData[i].toLowerCase())) {
                        $scope.errmsgforTo = false;
                    }
                    else if (Tores.length != 0) {
                        $scope.errmsgforTo = false;
                    }

                        //else if ($scope.GroupMailsForAutosuggest.indexOf(emailData[i]) > -1) {
                        //    $scope.errmsgforTo = false;
                        //}
                    else { $scope.errmsgforTo = true; }
                }
            }

            var emailDataCC = $('#tagsCC').val().split(';');
            for (i = 0; i < emailDataCC.length; i++) {
                if (emailDataCC[i] != "") {
                    try {
                        var CCres = jQuery.grep($scope.EmailsIds, function (ele) { return ele.toLowerCase() == emailDataCC[0].toLowerCase(); });
                    } catch (e) {
                        throw e;
                    }
                    if (regx1.test(emailDataCC[i].toLowerCase())) {
                        $scope.errmsgforCC = false;
                    }
                    else if (CCres.length != 0) {
                        $scope.errmsgforCC = false;
                    }
                    else { $scope.errmsgforCC = true; }
                }
            }

            var emailDataBCC = $('#tagsBCC').val().split(';');
            for (i = 0; i < emailDataBCC.length; i++) {
                if (emailDataBCC[i] != "") {
                    try {
                        var BCCres = jQuery.grep($scope.EmailsIds, function (ele) { return ele.toLowerCase() == emailDataBCC[0].toLowerCase(); });
                    } catch (e) {
                        throw e;
                    }
                    if (regx1.test(emailDataBCC[i].toLowerCase())) {
                        $scope.errmsgforBCC = false;
                    }
                    else if (BCCres.length != 0) {
                        $scope.errmsgforBCC = false;
                    }
                    else { $scope.errmsgforBCC = true; }
                }
            }
        }
        var checkVariable = false;
        if (!$scope.errmsg && !$scope.errmsgforBCC && !$scope.errmsgforCC && !$scope.errmsgforTo) { checkVariable = true; }

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
                            location.reload(false);
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
            //location.reload(false);
            //$scope.tempObject.To = "";
            //$scope.tempObject.CC = "";
            //$scope.tempObject.BCC = "";
            //$scope.tempObject.Subject = "";
            //$scope.tempObject.Body = "";
            //$scope.tempObject.IsRecurrenceEnabledYesNoOption = "";
            //$scope.tempObject.RecurrenceIntervalTypeCategory = "";
            //$scope.tempObject.FromDate = "";
            //$scope.tempObject.ToDate = "";
            //$scope.tempObject.IntervalFactor = "";


        }
        else {
            //$("#newEmailForm .field-validation-error").show();
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

        $scope.errmsgforTo = false;
        $scope.errmsgforCC = false;
        $scope.errmsgforBCC = false;
        $scope.errmsg = false;
        $scope.fileList = [];
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
                            //$scope.tableParams.reload();
                            $scope.AddNewEmailTemplate = false;
                            messageAlertEngine.callAlertMessage('EmailTemplateMessage', "Email Template is Successfully Added !!!", "success", true);
                        }
                        else {
                            for (var c = 0; c < $scope.data.length; c++) {
                                if ($scope.data[c].EmailTemplateID == data.emailTemplate.EmailTemplateID) {
                                    $scope.data[c].EditStatus = false;
                                    //$scope.tableParams.reload();
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
            //$scope.tableParams.reload();
            $scope.tempObject = angular.copy(dataEdit);
        }
        else {
            $scope.data[$scope.data.indexOf(dataEdit)].EditStatus = false;
            //$scope.tableParams.reload();
        }
    }

    ///Created function to be called when data loaded dynamically
    //$scope.init_table = function (data, condition) {

    //    $scope.data = data;
    //    var counts = [];

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
    //    }
    //    else if ($scope.data.length > 100) {
    //        counts = [10, 25, 50, 100];
    //    }
    //    if (condition == 1) {
    //        $scope.tableParamsInbox = new ngTableParams({
    //            page: 1,            // show first page
    //            count: 10,          // count per page
    //            filter: {
    //                //name: 'M'       // initial filter
    //                //FirstName : ''
    //            },
    //            sorting: {
    //                //LastModifiedDate: 'desc'
    //                //Name: 'desc'
    //                //name: 'asc'     // initial sorting
    //            }
    //        }, {
    //            counts: counts,
    //            total: $scope.data.length, // length of data
    //            getData: function ($defer, params) {
    //                if (params.settings().$scope == null) {
    //                    params.settings().$scope = $scope;
    //                }
    //                // use build-in angular filter
    //                var filteredData = params.filter() ?
    //                        $filter('filter')($scope.data, params.filter()) :
    //                        $scope.data;
    //                var orderedData = params.sorting() ?
    //                        $filter('orderBy')(filteredData, params.orderBy()) :
    //                        $scope.data;

    //                params.total(orderedData.length); // set total for recalc pagination
    //                $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
    //            }
    //        });
    //    } else if (condition == 2) {
    //        $scope.tableParamsSent = new ngTableParams({
    //            page: 1,            // show first page
    //            count: 10,          // count per page
    //            filter: {
    //                //name: 'M'       // initial filter
    //                //FirstName : ''
    //            },
    //            sorting: {
    //                LastModifiedDate: 'desc'
    //                //Name: 'desc'
    //                //name: 'asc'     // initial sorting
    //            }
    //        }, {
    //            counts: counts,
    //            total: $scope.data.length, // length of data
    //            getData: function ($defer, params) {
    //                if (params.settings().$scope == null) {
    //                    params.settings().$scope = $scope;
    //                }
    //                // use build-in angular filter
    //                var filteredData = params.filter() ?
    //                        $filter('filter')($scope.data, params.filter()) :
    //                        $scope.data;
    //                var orderedData = params.sorting() ?
    //                        $filter('orderBy')(filteredData, params.orderBy()) :
    //                        $scope.data;

    //                params.total(orderedData.length); // set total for recalc pagination
    //                $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
    //            }
    //        });
    //    } else if (condition == 3) {
    //        $scope.tableParamsAll = new ngTableParams({
    //            page: 1,            // show first page
    //            count: 10,          // count per page
    //            filter: {
    //                //name: 'M'       // initial filter
    //                //FirstName : ''
    //            },
    //            sorting: {
    //                //Name: 'desc'
    //                //name: 'asc'     // initial sorting
    //            }
    //        }, {
    //            counts: counts,
    //            total: $scope.data.length, // length of data

    //            getData: function ($defer, params) {
    //                if (params.settings().$scope == null) {
    //                    params.settings().$scope = $scope;
    //                }
    //                // use build-in angular filter
    //                var filteredData = params.filter() ?
    //                        $filter('filter')($scope.data, params.filter()) :
    //                        $scope.data;
    //                var orderedData = params.sorting() ?
    //                        $filter('orderBy')(filteredData, params.orderBy()) :
    //                        $scope.data;

    //                params.total(orderedData.length); // set total for recalc pagination
    //                $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
    //            }
    //        });
    //    } else if (condition == 4) {
    //        $scope.tableParamsFollowUp = new ngTableParams({
    //            page: 1,            // show first page
    //            count: 10,          // count per page
    //            filter: {
    //                //name: 'M'       // initial filter
    //                //FirstName : ''
    //            },
    //            sorting: {
    //                LastModifiedDate: 'desc'
    //                //Name: 'desc'
    //                //name: 'asc'     // initial sorting
    //            }
    //        }, {
    //            counts: counts,
    //            total: $scope.data.length, // length of data
    //            getData: function ($defer, params) {
    //                if (params.settings().$scope == null) {
    //                    params.settings().$scope = $scope;
    //                }
    //                // use build-in angular filter
    //                var filteredData = params.filter() ?
    //                        $filter('filter')($scope.data, params.filter()) :
    //                        $scope.data;
    //                var orderedData = params.sorting() ?
    //                        $filter('orderBy')(filteredData, params.orderBy()) :
    //                        $scope.data;

    //                params.total(orderedData.length); // set total for recalc pagination
    //                $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
    //            }
    //        });
    //    }

    //};

    ////if filter is on
    //$scope.ifFilter = function () {
    //    try {
    //        var bar;
    //        //var obj = $scope.tableParamsInbox.$params.filter;
    //        if ($scope.condition == 1) {
    //            var obj = $scope.tableParamsInbox.$params.filter;
    //        } else if ($scope.condition == 2) {
    //            var obj = $scope.tableParamsSent.$params.filter;
    //        } else if ($scope.condition == 3) {
    //            var obj = $scope.tableParamsAll.$params.filter;
    //        } else if ($scope.condition == 4) {
    //            var obj = $scope.tableParamsFollowUp.$params.filter;
    //        }
    //        for (bar in obj) {
    //            if (obj[bar] != "") {
    //                return false;
    //            }
    //        }
    //        return true;
    //    }
    //    catch (e) { return true; }
    //}

    ////Get index first in table
    //$scope.getIndexFirst = function () {
    //    try {
    //        if ($scope.groupBySelected == 'none') {
    //            //return ($scope.tableParamsInbox.$params.page * $scope.tableParamsInbox.$params.count) - ($scope.tableParamsInbox.$params.count - 1);
    //            if ($scope.condition == 1) {
    //                return ($scope.tableParamsInbox.$params.page * $scope.tableParamsInbox.$params.count) - ($scope.tableParamsInbox.$params.count - 1);
    //            } else if ($scope.condition == 2) {
    //                return ($scope.tableParamsSent.$params.page * $scope.tableParamsSent.$params.count) - ($scope.tableParamsSent.$params.count - 1);
    //            } else if ($scope.condition == 3) {
    //                return ($scope.tableParamsAll.$params.page * $scope.tableParamsAll.$params.count) - ($scope.tableParamsAll.$params.count - 1);
    //            } else if ($scope.condition == 4) {
    //                return ($scope.tableParamsFollowUp.$params.page * $scope.tableParamsFollowUp.$params.count) - ($scope.tableParamsFollowUp.$params.count - 1);
    //            }
    //        }
    //    }
    //    catch (e) { }
    //}

    ////Get index Last in table
    //$scope.getIndexLast = function () {
    //    try {
    //        if ($scope.groupBySelected == 'none') {
    //            //return { true: ($scope.data.length), false: ($scope.tableParamsInbox.$params.page * $scope.tableParamsInbox.$params.count) }[(($scope.tableParamsInbox.$params.page * $scope.tableParamsInbox.$params.count) > ($scope.data.length))];
    //            if ($scope.condition == 1) {
    //                return { true: ($scope.data.length), false: ($scope.tableParamsInbox.$params.page * $scope.tableParamsInbox.$params.count) }[(($scope.tableParamsInbox.$params.page * $scope.tableParamsInbox.$params.count) > ($scope.data.length))];
    //            } else if ($scope.condition == 2) {
    //                return { true: ($scope.data.length), false: ($scope.tableParamsSent.$params.page * $scope.tableParamsSent.$params.count) }[(($scope.tableParamsSent.$params.page * $scope.tableParamsSent.$params.count) > ($scope.data.length))];
    //            } else if ($scope.condition == 3) {
    //                return { true: ($scope.data.length), false: ($scope.tableParamsAll.$params.page * $scope.tableParamsAll.$params.count) }[(($scope.tableParamsAll.$params.page * $scope.tableParamsAll.$params.count) > ($scope.data.length))];
    //            } else if ($scope.condition == 4) {
    //                return { true: ($scope.data.length), false: ($scope.tableParamsFollowUp.$params.page * $scope.tableParamsFollowUp.$params.count) }[(($scope.tableParamsFollowUp.$params.page * $scope.tableParamsFollowUp.$params.count) > ($scope.data.length))];
    //            }
    //        }
    //    }
    //    catch (e) { }
    //}

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
    var tomorrow = new Date(new Date().getTime() + 24 * 60 * 60 * 1000);
    var today = new Date();
  //  $scope.tomorrow = new Date(new Date().getTime() + 24 * 60 * 60 * 1000);
    angular.extend($datepickerProvider.defaults, {
        minDate: today,
        startDate: tomorrow,
        autoclose: true,
    });
});

function ResetFormForValidation(form) {
    form.removeData('validator');
    form.removeData('unobtrusiveValidation');
    $.validator.unobtrusive.parse(form);
};
$(document).ready(function () {
    $(".ProviderTypeSelectAutoList").hide();
    $("body").tooltip({ selector: '[data-toggle=tooltip]' });
});

$(document).click(function (event) {
    if (!$(event.target).hasClass("form-control") && $(event.target).parents(".ProviderTypeSelectAutoList").length === 0) {
        $(".ProviderTypeSelectAutoList").hide();
    }
});