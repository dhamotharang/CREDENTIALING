var NotesTemplates = angular.module("NotesTemplates", ['ui.bootstrap']);
NotesTemplates.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

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
NotesTemplates.controller('NotesTemplateController', ['$scope', '$http', '$filter', '$rootScope', 'messageAlertEngine', function ($scope, $http, $filter, $rootScope, messageAlertEngine) {
    $scope.NotesTemplates = [];
    $scope.disableAdd = false;
    $scope.disableEdit = false;
    $scope.disableDelete = false;
    $scope.disablecode = false;
    $http.get(rootDir + "/MasterDataNew/GetAllNotesTemplates").success(function (value) {
        try {

            for (var i = 0; i < value.length ; i++) {
                if (value[i] != null) {
                    value[i].LastModifiedDate = ($scope.ConvertDateFormat(value[i].LastModifiedDate)).toString();
                    value[i].CreatedDate = ($scope.ConvertDateFormat(value[i].CreatedDate)).toString();

                }

            }

            $scope.NotesTemplates = angular.copy(value);
        } catch (e) {

        }
    });
    $scope.saveNotesTemplates = function (idx) {

        var addData = {
            NotesTemplateID: 0,
            Code: $scope.tempNotesTemplates.Code,
            Description: $scope.tempNotesTemplates.Description,

        };

        var isExist = true;

        for (var i = 0; i < $scope.NotesTemplates.length; i++) {

            if (addData.Code) {
                if ($scope.NotesTemplates[i].Code == addData.Code) {

                    isExist = false;
                    $scope.existErr = "The Code Already Exist";
                    break;
                }
            }

        }
        if (!addData.Code) { $scope.error1 = "Please Enter the Code"; }
        if (!addData.Description) { $scope.error = "Please Enter the Description"; }


        if (addData.Code && addData.Description && isExist) {

            $http.post(rootDir + '/MasterDataNew/AddNotesTemplate', addData).
                success(function (data, status, headers, config) {
                    try {
                        //----------- success message -----------

                        messageAlertEngine.callAlertMessage("NotesTemplates", "New Notes Template Details Added Successfully !!!!", "success", true);
                        data.NotesTemplate.LastModifiedDate = $scope.ConvertDateFormat(data.NotesTemplate.LastModifiedDate);
                        data.NotesTemplate.CreatedDate = $scope.ConvertDateFormat(data.NotesTemplate.CreatedDate);
                        $scope.NotesTemplates[idx] = angular.copy(data.NotesTemplate);
                        $scope.disableAdd = false;
                        $scope.disableEdit = false;
                        $scope.disableDelete = false;
                        $scope.reset();

                        $scope.error = "";
                        $scope.existErr = "";


                    } catch (e) {

                    }
                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("NotesTemplatesError", "Sorry Unable To Add NotesTemplate !!!!", "danger", true);
                    $scope.NotesTemplates.splice(idx, 1);
                    $scope.disableAdd = false;
                });
        }


    };

    $scope.addNotesTemplate = function (NotesTemplate) {
        $scope.disableEdit = true;
        $scope.disableDelete = true;
        $scope.disableAdd = true;
        var Month = new Date().getMonth() + 1;
        var _month = Month < 10 ? '0' + Month : Month;
        var _date = new Date().getDate() < 10 ? '0' + new Date().getDate() : new Date().getDate();
        var _year = new Date().getFullYear();
        var temp = {
            NotesTemplateID: 0,
            Code: "",
            Description: "",
           
            CreatedDate: _month + "-" + _date + "-" + _year
        };
        $scope.NotesTemplates.splice(0, 0, angular.copy(temp));
        $scope.tempNotesTemplates = angular.copy(temp);
    };
    $scope.ConvertDateFormat = function (value) {
        var returnValue = value;
        var dateData = "";
        try {
            if (value.indexOf("/Date(") == 0) {
                returnValue = new Date(parseInt(value.replace("/Date(", "").replace(")/", ""), 10));
                var day = returnValue.getDate() < 10 ? '0' + returnValue.getDate() : returnValue.getDate();
                var tempo = returnValue.getMonth() + 1;
                var month = tempo < 10 ? '0' + tempo : tempo;
                var year = returnValue.getFullYear();
                dateData = month + "-" + day + "-" + year;
            }
            return dateData;
        } catch (e) {
            return dateData;
        }
        return dateData;
    };
    $scope.tempNotesTemplates = {};
    //------------ gets the template to ng-include for a table row / item -------------------
    $scope.getTemplate = function (NotesTemplates) {
        if (NotesTemplates.NotesTemplateID === $scope.tempNotesTemplates.NotesTemplateID) return 'editNotesTemplates';
        return 'displayNotesTemplates';
    };
    $scope.cancelAdd = function () {
        $scope.NotesTemplates.splice(0, 1);
        $scope.reset();
        $scope.disableEdit = false;
        $scope.disableDelete = false;
        $scope.disableAdd = false;
        $scope.error = "";
        $scope.existErr = "";
    };

    $scope.reset = function () {
        $scope.tempNotesTemplates = {};
        $scope.disableAdd = false;
        $scope.disablecode = false;
        $scope.disableEdit = false;
        $scope.disableDelete = false;
        $scope.error = "";
        $scope.existErr = "";
    };
    //-------------------- Edit VisaStatus ----------
    $scope.editNotesTemplates = function (NotesTemplates) {
        $scope.tempNotesTemplates = angular.copy(NotesTemplates);
        $scope.disableAdd = true;
        $scope.disablecode = true;
    };
    $scope.updateNotesTemplate = function (idx) {
        var UpdateData = {
            NotesTemplateID: $scope.tempNotesTemplates.NotesTemplateID,
            Code: $scope.tempNotesTemplates.Code,
            Description: $scope.tempNotesTemplates.Description,

        };
        for (var i = 0; i < $scope.NotesTemplates.length; i++) {
            if ($scope.NotesTemplates[i].NotesTemplateID == UpdateData.NotesTemplateID) {
                idx = i;
            }
        }
        if (!UpdateData.Description) { $scope.error = "Please Enter the Description"; }
        if (UpdateData.Description) {
            $http.post(rootDir + '/MasterDataNew/UpdateNotesTemplate', UpdateData).
            success(function (data, status, headers, config) {
                if (data.status == true) {
                    messageAlertEngine.callAlertMessage("NotesTemplates", "Notes Template Updated Successfully !!!!", "success", true);
                    data.NotesTemplate.LastModifiedDate = ($scope.ConvertDateFormat(data.NotesTemplate.LastModifiedDate)).toString();
                    data.NotesTemplate.CreatedDate = ($scope.ConvertDateFormat(data.NotesTemplate.CreatedDate)).toString();
                    $scope.NotesTemplates[idx] = angular.copy(data.NotesTemplate);

                    $scope.error = "";
                    $scope.existErr = "";
                }
                else {
                    messageAlertEngine.callAlertMessage("NotesTemplatesError", "Sorry Unable To Update Notes Template !!!!", "danger", true);
                }

            }).error(function (data, status, headers, config) {
                //----------- error message -----------
                $scope.savingStatus = false;

                messageAlertEngine.callAlertMessage("NotesTemplatesError", "Sorry Unable To Update Notes Template !!!!", "danger", true);
            });
            $scope.reset();
        }
    }
    //$scope.InactivateNotesTemplate = function (NotesTemplateId) {
    //    $scope.NotesTemplateIdf = NotesTemplateId;
    //};
    $scope.RemoveNotesTemplate = function (NotesTemplateID) {

        $scope.savingStatus = true;
        $http.post(rootDir + '/MasterDataNew/InactivateNotesTemplate?NotesTemplateID=' + NotesTemplateID).
                success(function (data, status, headers, config) {
                    try {
                        //----------- success message -----------
                        if (data.status == "true") {
                            $scope.savingStatus = false;

                            messageAlertEngine.callAlertMessage("NotesTemplates", "Notes Template Deleted Successfully !!!!", "success", true);
                            for (var i in $scope.NotesTemplates) {
                                if ($scope.NotesTemplates[i].NotesTemplateID == NotesTemplateID) {
                                    $scope.NotesTemplates.splice(i, 1);
                                    break;
                                }
                            }
                            $scope.reset();
                            $scope.error = "";
                            $scope.existErr = "";
                        }
                    } catch (e) {

                    }

                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    $scope.savingStatus = false;

                    messageAlertEngine.callAlertMessage("NotesTemplatesError", "Sorry Unable To Delete Notes Template !!!!", "danger", true);
                });
    }
    $scope.$watch('bigCurrentPage', function (newValue, oldValue) {
        $scope.CurrentPage = [];
        var startIndex = (newValue - 1) * 10;
        var endIndex = startIndex + 9;
        if ($scope.NotesTemplates) {
            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.NotesTemplates[startIndex]) {
                    $scope.CurrentPage.push($scope.NotesTemplates[startIndex]);
                } else {
                    break;
                }
            }
        }
    });
    $scope.CurrentPage = [];

    //-------------------------- angular bootstrap pagger with custom-----------------
    $scope.maxSize = 5;
    $scope.bigTotalItems = 0;
    $scope.bigCurrentPage = 1;

    //-------------------- page change action ---------------------
    $scope.pageChanged = function (pagnumber) {
        $scope.bigCurrentPage = pagnumber;
    };
    //-------------- current page change Scope Watch ---------------------
    $scope.$watch('bigCurrentPage', function (newValue, oldValue) {
        $scope.CurrentPage = [];
        var startIndex = (newValue - 1) * 10;
        var endIndex = startIndex + 9;
        if ($scope.NotesTemplates) {
            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.NotesTemplates[startIndex]) {
                    $scope.CurrentPage.push($scope.NotesTemplates[startIndex]);
                } else {
                    break;
                }
            }
        }
    });
    //-------------- License Scope Watch ---------------------
    $scope.$watchCollection('NotesTemplates', function (newValue, oldValue) {
        if (newValue) {
            $scope.bigTotalItems = newValue.length;

            $scope.CurrentPage = [];
            $scope.bigCurrentPage = 1;

            var startIndex = ($scope.bigCurrentPage - 1) * 10;
            var endIndex = startIndex + 9;

            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.NotesTemplates[startIndex]) {
                    $scope.CurrentPage.push($scope.NotesTemplates[startIndex]);
                } else {
                    break;
                }
            }
        }
    });
    $scope.filterData = function () {
        $scope.pageChanged(1);
    }







    $scope.IsValidIndex = function (index) {

        var startIndex = ($scope.bigCurrentPage - 1) * 10;
        var endIndex = startIndex + 9;

        if (index >= startIndex && index <= endIndex)
            return true;
        else
            return false;

    }
}]);
