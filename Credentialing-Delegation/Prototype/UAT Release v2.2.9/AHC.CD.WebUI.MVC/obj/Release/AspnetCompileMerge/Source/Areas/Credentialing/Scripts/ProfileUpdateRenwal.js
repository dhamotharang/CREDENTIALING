var initCredApp = angular.module('InitCredApp', ['ui.bootstrap']);

initCredApp.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

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

initCredApp.controller('listController',['$scope', '$http', '$timeout', 'messageAlertEngine', function ($scope, $http, $timeout, messageAlertEngine) {

        //$scope.allProviders = [
        //    { "Type": "Update", "NPI": "1234567890", "FullName": "Dr. Pariksith Singh", "Section": "Demographics", "SubSection": "Personal Identification", "FieldName": "Driver's License", "For": "", "OldData": "123456", "NewData": "123459", "Date": "01-31-2015" },
        //    { "Type": "Renewal", "NPI": "1234567890", "FullName": "Dr. Pariksith Singh", "Section": "Demographics", "SubSection": "Personal Identification", "FieldName": "Driver's License", "For": "", "OldData": "123456", "NewData": "123459", "Date": "01-31-2015" },
        //    { "Type": "Renewal", "NPI": "4234233324", "FullName": "Dr. Maria Stone", "Section": " Identification & Licenses", "SubSection": "State License", "FieldName": "Issue Date & Expiry Date", "For": "License Number : ME98056", "OldData": "Issue Date: 11-15-2012, Expiry Date: 01-31-2015", "Date": "01-21-2015", "NewData": "Issue Date: 01-31-2015, Expiry Date: 01-31-2018" },
        //    { "Type": "Renewal", "NPI": "4234236624", "FullName": "Dr. K R", "Section": " Identification & Licenses", "SubSection": "State License", "FieldName": "Issue Date & Expiry Date", "For": "License Number : ME98056", "OldData": "Issue Date: 11-15-2012, Expiry Date: 01-31-2015", "Date": "01-21-2015", "NewData": "Issue Date: 01-31-2015, Expiry Date: 01-31-2018" },
        //    { "Type": "Renewal", "NPI": "4234237724", "FullName": "Dr. R S", "Section": " Identification & Licenses", "SubSection": "State License", "FieldName": "Issue Date & Expiry Date", "For": "License Number : ME98056", "OldData": "Issue Date: 11-15-2012, Expiry Date: 01-31-2015", "Date": "01-21-2015", "NewData": "Issue Date: 01-31-2015, Expiry Date: 01-31-2018" },
        //    { "Type": "Update", "NPI": "3423423453", "FullName": "Dr. Gaurav Mathur", "Section": "Demographics", "SubSection": "Personal Identification", "FieldName": "Driver's License", "For": "", "OldData": "123456", "NewData": "123459", "Date": "01-03-2015" },
        //    { "Type": "Update", "NPI": "6344545433", "FullName": "Dr. Nitesh Suresh", "Section": "Demographics", "SubSection": "Medicare Information", "FieldName": "Issue Date", "For": "Medicare Number : DSADSADSA", "OldData": "Issue Date: 01-31-2015", "NewData": "Issue Date: 01-31-2016", "Date": "01-29-2015" },
        //];

        //Convert the date from database to normal
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

        $scope.temp = '';
        $scope.FilterProviders = [];

        $http.get("/Credentialing/ProfileUpdates/GetAllUpdates").then(function (value) {
            console.log("Profile Updates");
            for (var i = 0; i < value.data.length ; i++) {
                if (value.data[i] != null) {
                    value.data[i].LastModifiedDate = $scope.ConvertDateFormat(value.data[i].LastModifiedDate);
                }
            }            
            $scope.allProviders = angular.copy(value.data);
            console.log($scope.allProviders);
            $scope.getByType('Update');
        });

        $scope.pushdata = function (data) {
            $scope.hideApprovalBtn = false;
            $scope.temp = data;
            $scope.chnagedData = [];
            $http.post("/Credentialing/ProfileUpdates/GetDataById?trackerId=" + data.ProfileUpdatesTrackerId).then(function (value) {
                console.log("CCO Approval");
                var date = /^(((0[1-9]|[12]\d|3[01])[\/\.-](0[13578]|1[02])[\/\.-]((19|[2-9]\d)\d{2})\s(0[0-9]|1[0-9]|2[0-3]):([0-5][0-9]):([0-5][0-9]))|((0[1-9]|[12]\d|30)[\/\.-](0[13456789]|1[012])[\/\.-]((19|[2-9]\d)\d{2})\s(0[0-9]|1[0-9]|2[0-3]):([0-5][0-9]):([0-5][0-9]))|((0[1-9]|1\d|2[0-8])[\/\.-](02)[\/\.-]((19|[2-9]\d)\d{2})\s(0[0-9]|1[0-9]|2[0-3]):([0-5][0-9]):([0-5][0-9]))|((29)[\/\.-](02)[\/\.-]((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))\s(0[0-9]|1[0-9]|2[0-3]):([0-5][0-9]):([0-5][0-9])))$/g;

                for (var i = 0; i < value.data.length ; i++) {
                    if (value.data[i].OldValue != null && date.test(value.data[i].OldValue)) {
                        value.data[i].OldValue = value.data[i].OldValue.split(" ")[0];
                    }
                    if (value.data[i].NewValue != null && date.test(value.data[i].NewValue)) {
                        value.data[i].NewValue = value.data[i].NewValue.split(" ")[0];
                    }
                }
                
                
                $scope.chnagedData = angular.copy(value.data);
                console.log($scope.chnagedData);
            });


            $scope.IsHowDetails = true;
        }
        $scope.HideDetails = function () {
            $scope.IsHowDetails = false;
            $scope.temp = {};
        };
        

        $scope.OneIsSelected = false;

        //------------- check atleast one checked ----------------
        $scope.anyOneChecked = function () {
            var status = false;
            for (var i in $scope.FilterProviders) {
                if ($scope.FilterProviders[i].IsChecked) {
                    status = true;
                    break;
                }
            }
            $scope.OneIsSelected = status;
        };

        //----------------------- select all ----------------------
        $scope.SelectAll = function (status) {
            if (status) {
                for (var i in $scope.FilterProviders) {
                    $scope.FilterProviders[i].IsChecked = true;
                }
            } else {
                for (var i in $scope.FilterProviders) {
                    $scope.FilterProviders[i].IsChecked = false;
                }
            }
            $scope.anyOneChecked();
        };


        $scope.UpdateSuccess = function (data) {
        
            var newData = angular.copy(data);         
           

            $.ajax({
                type: 'POST',
                url: data.Url + data.ProfileId,
                data: data.NewData,
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {

                    $.ajax({
                        type: "POST",
                        url: "/Credentialing/ProfileUpdates/SetApproval",
                        data: {
                            TrackerId: newData.ProfileUpdatesTrackerId, ApprovalStatus: "Approved", RejectionReason: ""
                        },

                    }).success(function (resultData) {
                        messageAlertEngine.callAlertMessage("approved", "The Submission is Successful!!!!", "success", true);

                        $scope.hideApprovalBtn = true;
                        $scope.allProviders.splice($scope.FilterProviders.indexOf(newData), 1);
                        $scope.getByType('Update');
                    }).error(function () { $scope.error_message = "An Error occured !! Please Try Again !!"; })

                                       
                }
            });
            
        };

        $scope.UpdateFail = function () {

            $scope.hideApprovalBtn = true;
            $scope.showReject = true;
            $scope.showHold = false;
            $("#updateFail").show();            
            
        };

        $scope.OnHold = function () {

            $scope.hideApprovalBtn = true;
            $scope.showHold = true;
            $scope.showReject = false;
            $("#updateFail").show();

        };

        $scope.SetReject = function (data, reason) {

            $.ajax({
                type: "POST",
                url: "/Credentialing/ProfileUpdates/SetApproval",
                data: {
                    TrackerId: data.ProfileUpdatesTrackerId, ApprovalStatus: "Rejected", RejectionReason: reason
                },

            }).success(function (resultData) {
                messageAlertEngine.callAlertMessage("rejected", "The Update/Renewal is rejected!!!!", "success", true);                
                $("#updateFail").hide();
                $scope.hideApprovalBtn = true;
                $scope.allProviders.splice($scope.FilterProviders.indexOf(data), 1);
                $scope.getByType('Update');
                
            }).error(function () { $scope.error_message = "An Error occured !! Please Try Again !!"; })

        };

        $scope.SetOnHold = function (data, reason) {

            $.ajax({
                type: "POST",
                url: "/Credentialing/ProfileUpdates/SetApproval",
                data: {
                    TrackerId: data.ProfileUpdatesTrackerId, ApprovalStatus: "OnHold", RejectionReason: reason
                },

            }).success(function (resultData) {
                messageAlertEngine.callAlertMessage("onHold", "The Update/Renewal is kept On hold!!!!", "success", true);
                $("#updateFail").hide();
                $scope.hideApprovalBtn = true;
                $scope.allProviders.splice($scope.allProviders.indexOf(data), 1);
                $scope.getByType('Update');

            }).error(function () { $scope.error_message = "An Error occured !! Please Try Again !!"; })

        };


        $scope.closedivUpdate = function () {
            $scope.hideApprovalBtn = false;
            $("#updateFail").hide();

        };

        //------------------ get type Type ----------------
        $scope.getByType = function (type) {
            var temp1 = [];
            $scope.SelectedType = type;
            for (var i in $scope.allProviders) {
                if ($scope.allProviders[i].Modification == type) {
                    temp1.push($scope.allProviders[i]);
                }
            }
            $scope.FilterProviders = temp1;
            $scope.IsHowDetails = false;
            $scope.SelectAll(false);
            $scope.IsAllChecked = false;
        };

        

        //--------------- accept Action ----------------------
        $scope.ActionMessage = "";

        $scope.ActionForChanges = function () {

            for (var i in $scope.FilterProviders) {
                if ($scope.FilterProviders[i].IsChecked) {
                    var newData = angular.copy($scope.FilterProviders[i]);
                    $.ajax({
                        type: 'POST',
                        url: $scope.FilterProviders[i].Url + $scope.FilterProviders[i].ProfileId,
                        data: $scope.FilterProviders[i].NewData,
                        dataType: 'json',
                        contentType: 'application/json; charset=utf-8',
                        success: function (data) {
                                                    
                        }
                    });
                    $scope.SetApproval(newData);
                    $scope.allProviders.splice($scope.FilterProviders.indexOf(newData), 1);
                }

            }
            messageAlertEngine.callAlertMessage("approved", "The Submission is Successful!!!!", "success", true);
            $scope.hideApprovalBtn = true;
            //$scope.getByType('Update');           
        };

        $scope.SetApproval = function (newData) {

            var data = angular.copy(newData);
            $.ajax({
                type: "POST",
                url: "/Credentialing/ProfileUpdates/SetApproval",
                data: {
                    TrackerId: newData.ProfileUpdatesTrackerId, ApprovalStatus: "Approved", RejectionReason: ""
                },

            }).success(function (resultData) {
                
            }).error(function () { $scope.error_message = "An Error occured !! Please Try Again !!"; })

            
        }

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
            if ($scope.FilterProviders) {
                for (startIndex; startIndex <= endIndex ; startIndex++) {
                    if ($scope.FilterProviders[startIndex]) {
                        $scope.CurrentPage.push($scope.FilterProviders[startIndex]);
                    } else {
                        break;
                    }
                }
            }
            //console.log($scope.CurrentPageProviders);
        });
        //-------------- License Scope Watch ---------------------
        $scope.$watchCollection('FilterProviders', function (newValue, oldValue) {
            if (newValue) {
                $scope.bigTotalItems = newValue.length;

                $scope.CurrentPage = [];
                $scope.bigCurrentPage = 1;

                var startIndex = ($scope.bigCurrentPage - 1) * 10;
                var endIndex = startIndex + 9;

                for (startIndex; startIndex <= endIndex ; startIndex++) {
                    if ($scope.FilterProviders[startIndex]) {
                        $scope.CurrentPage.push($scope.FilterProviders[startIndex]);
                    } else {
                        break;
                    }
                }
                //console.log($scope.CurrentPageProviders);
            }
        });
        //------------------- end ------------------
        
    }]);
