//--------------------- Angular Module ----------------------
var MasterDataVerificationLinks = angular.module("MasterDataVerificationLinks", ['ui.bootstrap']);

//Service for getting master data
MasterDataVerificationLinks.service('masterDataService', ['$http', '$q', function ($http, $q) {

    this.getMasterData = function (URL) {
        return $http.get(URL).then(function (value) { return value.data; });
    };

}]);

MasterDataVerificationLinks.factory('VerificationLinkFactory', function () {
    function modalDismiss() {
        angular.element('#verficationModal').modal('hide');
        $('body').removeClass('modal-open');
        $('.modal-backdrop').remove();
    }
    return {
        modalDismiss: modalDismiss
    };
})


MasterDataVerificationLinks.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

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
MasterDataVerificationLinks.directive('saving', function () {
    return {
        restrict: 'E',
        replace: true,
        priority: 1,
        scope: {
            ngModel: '=',
            loading: '='
        },
        template: '<div ng-show="loading" class="loading"><img src="/Content/Images/ajax-loader.gif" width="20" height="20" />Saving...</div>',
        link: function (scope, element, attr) {

        }
    }
});

MasterDataVerificationLinks.directive('verficationModal', ["$http", "$compile", "$templateRequest", function ($http, $compile, $templateRequest) {
    return {
        replace: true,
        priority: 2,
        restrict: 'EA',
        transclude: true,
        scope: {
            ngModel: '=',
            remove: '&',
            saving: '='
        },
        link: function (scope, el, attrs) {
            //scope.saving = attrs.savingStatus;
            scope.modalId = attrs.modalId;
            scope.ConfirmTitle = attrs.modalTitle;
            scope.modalSrc = attrs.modalSrc;

            scope.ConfirmMessage = attrs.modalMessage;
            $templateRequest(rootDir + scope.modalSrc).then(function (html) {
                var template = angular.element(html);
                el.append(template);
                $compile(template)(scope);
            });
        }
    }
}]);

//=========================== Controller declaration ==========================
MasterDataVerificationLinks.controller('VerificationLinksccontroller', ['$scope', '$http', '$filter', '$rootScope', 'masterDataService', 'messageAlertEngine', 'VerificationLinkFactory', function ($scope, $http, $filter, $rootScope, masterDataService, messageAlertEngine, VerificationLinkFactory) {

    $scope.VerificationLinks = [];

    $http.get(rootDir + "/MasterDataNew/GetAllVerificationLinks").success(function (value) {

        try {
            for (var i = 0; i < value.length ; i++) {
                if (value[i] != null) {
                    value[i].LastModifiedDate = ($scope.ConvertDateFormat(value[i].LastModifiedDate)).toString();
                }

            }

            $scope.VerificationLinks = angular.copy(value);
        } catch (e) {

        }
    });

    //Convert the date from database to normal
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


    $scope.tempVerificationLinks = {};
    $scope.VerificationLinks = [];

    //------------ gets the template to ng-include for a table row / item -------------------
    $scope.getTemplate = function (VerificationLinks) {

        if (VerificationLinks.VerificationLinkID === $scope.tempVerificationLinks.VerificationLinkID)
            return 'editVerificationLinkName';
        else
            return 'displayVerificationLinks';
    };

    //-------------------- Edit Verification Link ----------
    $scope.editVerificationLinks = function (Name) {
        $scope.tempVerificationLinks = angular.copy(Name);
        $scope.disableAdd = true;
    };

    //------------------- Add Verification link ---------------------
    $scope.addVerificationLink = function (VerificationLink) {
        $scope.error1 = "";
        $scope.disableEdit = true;
        $scope.disableAdd = true;
        var Month = new Date().getMonth() + 1;
        var _month = Month < 10 ? '0' + Month : Month;
        var _date = new Date().getDate() < 10 ? '0' + new Date().getDate() : new Date().getDate();
        var _year = new Date().getFullYear();
        var temp = {
            addVerificationLinks: 0,
            Name: "",
            Link: "",
            Status: "Active",
            LastModifiedDate: _month + "-" + _date + "-" + _year,
        };
        $scope.VerificationLinks.splice(0, 0, angular.copy(temp));
        $scope.tempVerificationLinks = angular.copy(temp);
    };

    //------------------- Save Verification Link ---------------------
    $scope.saveVerificationLink = function (idx) {

        var addData = {
            VerificationLinkID: 0,
            Name: $scope.tempVerificationLinks.Name,
            Link: $scope.tempVerificationLinks.Link,
            StatusType: 1,
        };



        var isExist = true;

        for (var i = 0; i < $scope.VerificationLinks.length; i++) {

            if (addData.Name && $scope.VerificationLinks[i].Name.toLowerCase().trim() == addData.Name.toLowerCase().trim()) {

                isExist = false;
                $scope.existErr = "This Verification Link Name already exists";
                break;
            }
            if ($scope.VerificationLinks[i].Link != null) {
                if (addData.Link && $scope.VerificationLinks[i].Link.toLowerCase().trim() == addData.Link.toLowerCase().trim()) {

                    isExist = false;
                    $scope.existErr1 = "This Verification Link already exists";
                    break;
                }
            }
        }


        if (!addData.Link) { $scope.error1 = "Please Enter the Link"; }
        if (!addData.Name) { $scope.error = "Please Enter the Name"; }
            

        if (addData.Name && addData.Link && isExist) {
            $http.post(rootDir + '/MasterDataNew/AddVerificationLink', addData).
                success(function (data, status, headers, config) {
                    try {
                        //----------- success message -----------
                        if (data.status == "true") {
                            messageAlertEngine.callAlertMessage("VerificationLink", "Verification Link Added Successfully !!!!", "success", true);
                            data.verificationLink.LastModifiedDate = $scope.ConvertDateFormat(data.verificationLink.LastModifiedDate);
                            $scope.VerificationLinks[idx] = angular.copy(data.verificationLink);
                            $scope.reset();
                            $scope.error = "";
                            $scope.existErr = "";
                        }
                        else {
                            messageAlertEngine.callAlertMessage("verificationlinkerror", "Sorry Unable To Add new Verification Link !!!!", "danger", true);
                            $scope.VerificationLinks.splice(idx, 1);
                        }
                    } catch (e) {

                    }
                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("verificationlinkerror", "Sorry Unable To Add new Verification Link !!!!", "danger", true);
                    $scope.VerificationLinks.splice(idx, 1);
                });
        }


    };

    //------------------- Update Verification Link ---------------------
    $scope.updateVerificationLinks = function (idx) {

        var updateData = {
            VerificationLinkID: $scope.tempVerificationLinks.VerificationLinkID,
            Name: $scope.tempVerificationLinks.Name,
            Link: $scope.tempVerificationLinks.Link,
            StatusType: 1,
        };

        var isExist = true;

        for (var i = 0; i < $scope.VerificationLinks.length; i++) {

            if (updateData.Name && $scope.VerificationLinks[i].VerificationLinkID != updateData.VerificationLinkID && $scope.VerificationLinks[i].Name.replace(" ", "").toLowerCase() == updateData.Name.replace(" ", "").toLowerCase()) {

                isExist = false;
                $scope.existErr = "This Verification Link Name already exists";
                break;
            }
            if (updateData.Link && $scope.VerificationLinks[i].VerificationLinkID != updateData.VerificationLinkID && $scope.VerificationLinks[i].Link.replace(" ", "").toLowerCase() == updateData.Link.replace(" ", "").toLowerCase()) {

                isExist = false;
                $scope.existErr1 = "This Verification Link already exists";
                break;
            }
        }
        if (!updateData.Name) { $scope.error = "Please Enter the Verification Link Name"; }
        if (!updateData.Link) { $scope.error = "Please Enter the Verification Link"; }



        if (updateData.Name && isExist) {
            $http.post(rootDir + '/MasterDataNew/UpdateVerificationLinks', updateData).
                success(function (data, status, headers, config) {
                    try {
                        //----------- success message -----------
                        if (data.status == "true") {
                            messageAlertEngine.callAlertMessage("verificationlink", "Verification link Details Updated Successfully !!!!", "success", true);
                            data.verificationlink.LastModifiedDate = $scope.ConvertDateFormat(data.verificationlink.LastModifiedDate);
                            $scope.VerificationLinks[idx] = angular.copy(data.verificationlink);
                            $scope.reset();
                            $scope.error = "";
                            $scope.existErr = "";
                        }
                    } catch (e) {

                    }

                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("verificationlinkerror", "Sorry Unable To Update Verification Link Details !!!!", "danger", true);
                });
        }


    };
    $scope.MasterVerificationlinkId = 0;
    $scope.savingStatus = false;
    $scope.InactivateVerificationLink = function (verificationlinkId) {        
        $scope.MasterVerificationlinkId = verificationlinkId;
    };
    $scope.RemoveVerificationLink = function () {

        $scope.savingStatus = true;
        $http.post(rootDir + '/MasterDataNew/InactivateVerificationLink/?verificationLinkID=' + $scope.MasterVerificationlinkId).
                success(function (data, status, headers, config) {
                    try {
                        //----------- success message -----------
                        if (data == "true") {
                            $scope.savingStatus = false;
                            VerificationLinkFactory.modalDismiss();
                            messageAlertEngine.callAlertMessage("VerificationLink", "Verification Link Deleted Successfully !!!!", "success", true);
                            for (var i in $scope.VerificationLinks) {
                                if ($scope.VerificationLinks[i].VerificationLinkID == $scope.MasterVerificationlinkId) {
                                    $scope.VerificationLinks.splice(i, 1);
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
                    VerificationLinkFactory.modalDismiss();
                    messageAlertEngine.callAlertMessage("verificationlinkerror", "Sorry Unable To Update Verification Link Details !!!!", "danger", true);
                });
    }
    //----------------- Verification Links new add cancel ---------------
    $scope.cancelAdd = function () {
        $scope.VerificationLinks.splice(0, 1);
        $scope.tempVerificationLinks = {};
        $scope.disableEdit = false;
        $scope.disableAdd = false;
        $scope.error = "";
        $scope.existErr = "";
    };

    //-------------------- Reset Verification Links ----------------------
    $scope.reset = function () {
        $scope.tempVerificationLinks = {};
        $scope.disableAdd = false;
        $scope.disableEdit = false;
        $scope.error = "";
        $scope.existErr = "";
    };

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
        if ($scope.VerificationLinks) {
            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.VerificationLinks[startIndex]) {
                    $scope.CurrentPage.push($scope.VerificationLinks[startIndex]);
                } else {
                    break;
                }
            }
        }
    });
    //-------------- License Scope Watch ---------------------
    $scope.$watchCollection('VerificationLinks', function (newValue, oldValue) {
        if (newValue) {
            $scope.bigTotalItems = newValue.length;

            $scope.CurrentPage = [];
            $scope.bigCurrentPage = 1;

            var startIndex = ($scope.bigCurrentPage - 1) * 10;
            var endIndex = startIndex + 9;

            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.VerificationLinks[startIndex]) {
                    $scope.CurrentPage.push($scope.VerificationLinks[startIndex]);
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
    //------------------- end ------------------

}]);
