Cred_SPA_App.controller('PlanSummaryController',
    function ($scope, $q, $rootScope, $timeout, $filter, $http, messageAlertEngine) {
        $scope.ActivityStatusType = false;
        $scope.$on('LTPStatus', function (event, args) {

            $scope.PlanReportTabStatus = args.StatusLTP;
            //if ($scope.PlanReportTabStatus) {
            $scope.Loaded = "progtrckr-done";
            //}
            //else {
            //    $scope.Loaded = "progtrckr-todo";
            //}
        });

        //$scope.PlanReportTabStatus = $scope.$parent.LoadedData.length > 0 ? true : false;
        $scope.$on('event', function (event, args) {
            if (args == 'true') {
                $scope.isCompleted = true;
                $scope.completedDate = new Date();
                $scope.Completed = 'progtrckr-done';
                $scope.updatedDateForCompleted = $scope.changeTimeAmPm($scope.completedDate);
                for (var i in $scope.LoginUsers) {
                    if ($scope.LoginUsers[i].Id == cduserdata.cdUser.AuthenicateUserId) {
                        if ($scope.LoginUsers[i].FullName != null) {
                            $scope.updatedByForCompleted = $scope.LoginUsers[i].FullName;
                        }
                        else {
                            $scope.updatedByForCompleted = $scope.LoginUsers[i].Email;
                        }
                    }
                }

            }
        });
        $scope.PlanDetail = {};
        $scope.PlanLOB = [];
        $scope.IsMessage = false;

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

        //function LoginUsersData() {
        //    var defer = $q.defer();
        //    $http.get(rootDir + '/Credentialing/CnD/GetAllApplicationUsers').
        //        success(function (data, status, headers, config) {
        //            $scope.LoginUsers = data;
        //            defer.resolve(data);
        //        }).
        //        error(function (data, status, headers, config) {
        //            defer.reject();
        //        });
        //    return defer.promise;
        //}

        $scope.getPlanData = function (planID) {
            if (!$scope.dataLoaded) {
                $http({
                    method: 'GET',
                    url: rootDir + '/Credentialing/CnD/GetPlanAsync?planId=' + planID
                }).success(function (data, status, headers, config) {
                    try {
                        $scope.fillData(data);
                    } catch (e) {
                    }
                }).error(function (data, status, headers, config) {
                });
                $scope.dataLoaded = true;
            }
        };

        $scope.fillData = function (data) {
            if ($scope.PlanDetail != null) {
                $scope.PlanDetail = data.PlanDetail;
            }
            if ($scope.PlanLOB != null) {
                $scope.PlanLOB = data.PlanLob;
            }
        }
        $scope.CredentialingInfo = {};
        $scope.ActivityLogs = [];
        $scope.testCredentialing = "";
        $scope.getCredentialingInfo = function (profileID, planID) {
         $scope.dataLoaded = false;
         $scope.GetCredStatus = "";

            if (!$scope.dataLoaded) {
                $http({
                    method: 'GET',
                    url: rootDir + '/Credentialing/CnD/GetCredentialingInfoAsync?credInfoID=' + $scope.credentialingInfoID
                }).success(function (data, status, headers, config) {
                    try {
                        $rootScope.timelineActivity = [];
                        if (data.credInfo != null) {
                            $scope.ProviderDetail(data.credInfo);
                            $scope.CredentialingInfo = data.credInfo;
                            $scope.GetCredStatus = data.credInfo.CredentialingLogs[0].Credentialing;
                            console.log("CredentialingInfo" + data.credInfo.CredentialingLogs[0].Credentialing);
                            $scope.testCredentialing = angular.copy(data.credInfo.CredentialingLogs[0].Credentialing);
                            $scope.getCurrentActivity(data.credInfo);
                        }
                    } catch (e) {
                    }
                }).error(function (data, status, headers, config) {
                });
                $scope.dataLoaded = true;
            }
        };

        //$scope.GetAllPlansWhichAreCredentialed = function (profileID) {
        //    $scope.dataLoaded = false;
        //    if (!$scope.dataLoaded) {
        //        $http({
        //            method: 'GET',
        //            url: rootDir + '/Credentialing/CnD/GetAllCredentialingPlansForAProvider?profileID=' + profileID
        //        }).success(function (data, status, headers, config) {
                    
        //        }).error(function (data, status, headers, config) {
        //        });
        //        $scope.dataLoaded = true;
        //    }
        //};

        $scope.ProviderDetail = function (data) {
            var FirstName = "";
            var MiddleName = "";
            var LastName = "";
            if (data.Profile != null && data.Profile.PersonalDetail != null) {
                if (data.Profile.PersonalDetail.FirstName != null) {
                    FirstName = data.Profile.PersonalDetail.FirstName;
                } else {
                    FirstName = "";
                }
                if (data.Profile.PersonalDetail.MiddleName != null) {
                    MiddleName = data.Profile.PersonalDetail.MiddleName;
                } else {
                    MiddleName = "";
                }
                if (data.Profile.PersonalDetail.LastName != null) {
                    LastName = data.Profile.PersonalDetail.LastName;
                } else {
                    LastName = "";
                }
                $scope.providerName = data.Profile.PersonalDetail.Salutation + " " + FirstName + " " + MiddleName + " " + LastName;
                $rootScope.providerfullName = data.Profile.PersonalDetail.Salutation + " " + FirstName + " " + MiddleName + " " + LastName;
            }
            if (data.Plan != null) {
                $scope.planName = data.Plan.PlanName;
                $rootScope.planNameUniv = data.Plan.PlanName;
            }
            $rootScope.tempActivity = $scope.providerName + " Load to Plan done for " + $scope.planName;
        }

        $scope.getCurrentActivity = function (data) {
            //LoginUsersData();
            if (data != null) {
                if (data.CredentialingLogs != null && data.CredentialingLogs.length != 0) {
                    for (var c = 0; c < data.CredentialingLogs.length; c++) {
                        if (data.CredentialingLogs[c].Credentialing == "Credentialing" || data.CredentialingLogs[c].Credentialing == "ReCredentialing") {
                            $scope.ActivityLog = data.CredentialingLogs[c].CredentialingActivityLogs;
                        }
                    }
                    for (var c = 0; c < data.CredentialingLogs.length; c++) {
                        if (data.CredentialingLogs[c].Credentialing == "Dropped") {
                            $scope.ActivityLog = [];
                            $scope.ActivityLog = data.CredentialingLogs[c].CredentialingActivityLogs;
                            $scope.ActivityStatusType = true;
                        }
                    }
                }
                if ($scope.ActivityStatusType == true) {
                    $scope.isDroped = true;
                    $scope.lastActivity = $scope.ConvertDateFormat($scope.ActivityLog[$scope.ActivityLog.length - 1].LastModifiedDate);
                    $scope.credtype = $rootScope.isrecredentialing;
                    $scope.SubmitSPA($scope.credentialingInfoID, $scope.credtype);
                }
                if ($scope.ActivityLog != null) {
                    $scope.checkForStatus($scope.ActivityLog);
                }
            }
        }
        $scope.SubmitSPA = function (credId) {
            $scope.$parent.ViewOnlyMode = false;
            sessionStorage.setItem('ViewOnlyMode', $scope.$parent.ViewOnlyMode);
            sessionStorage.setItem('Droped', $scope.isDroped);
        }

        $scope.Initiated = "progtrckr-todo";
        $scope.isInitiated = false;
        $scope.Verified = "progtrckr-todo";
        $scope.isVerified = false;
        $scope.CCMDone = "progtrckr-todo";
        $scope.isCCMDone = false;
        $scope.Loaded = "progtrckr-todo";
        $rootScope.isLoaded = false;
        $scope.Completed = "progtrckr-todo";
        $scope.isCompleted = false;
        $scope.isCredentialed = false;
        $scope.isreCredentialed = false;
        $scope.isdeCredentialed = false;
        $scope.isDroped = false;
        $rootScope.timelineActivity = [];
        $scope.timelineTimeStamp = [];
        $scope.stamp = '';
        $scope.checkForStatus = function (data) {
            var FirstName = "";
            var MiddleName = "";
            var LastName = "";
            $rootScope.isrecredentialing = angular.copy($scope.testCredentialing);
            console.log("recredentialing" + $rootScope.isrecredentialing);
            $scope.typeOfCredentialing = angular.copy($rootScope.isrecredentialing);
            if (data != null && data.length != 0) {
                $rootScope.timelineActivity = [];
                data = $filter('orderBy')(data, 'LastModifiedDate', false);
                for (var i = 0; i < data.length ; i++) {
                    switch (data[i].Activity) {
                        case "Initiation":
                            if (data[i].ActivityStatus == "Completed" && $rootScope.isrecredentialing == "Credentialing") {
                                $scope.Initiated = "progtrckr-done";
                                $scope.isInitiated = true;
                                $scope.$parent.PSVTabStatus = true;
                                if (data[i].ActivityBy != null && data[i].ActivityBy.Profile != null && data[i].ActivityBy.Profile.PersonalDetail != null) {
                                    if (data[i].ActivityBy.Profile != null && data[i].ActivityBy.Profile.PersonalDetail != null) {
                                        if (data[i].ActivityBy.Profile.PersonalDetail.FirstName != null) {
                                            FirstName = data[i].ActivityBy.Profile.PersonalDetail.FirstName;
                                        } else {
                                            FirstName = "";
                                        }
                                        if (data[i].ActivityBy.Profile.PersonalDetail.MiddleName != null) {
                                            MiddleName = data[i].ActivityBy.Profile.PersonalDetail.MiddleName;
                                        } else {
                                            MiddleName = "";
                                        }
                                        if (data[i].ActivityBy.Profile.PersonalDetail.LastName != null) {
                                            LastName = data[i].ActivityBy.Profile.PersonalDetail.LastName;
                                        } else {
                                            LastName = "";
                                        }
                                    }
                                    $scope.InitiatedBy = FirstName + " " + MiddleName + " " + LastName;
                                }
                                if (data[i].ActivityBy != null) {
                                    for (var j = 0; j < $scope.LoginUsers.length; j++) {
                                        if (data[i].ActivityBy.AuthenicateUserId == $scope.LoginUsers[j].Id) {
                                            //$scope.timeStamp.push({ updateBy: $scope.LoginUsers[j].Email })
                                            if ($scope.LoginUsers[j].FullName != null) {

                                                $scope.updatedByForInitiation = $scope.LoginUsers[j].FullName;
                                            }
                                            else {
                                                $scope.updatedByForInitiation = $scope.LoginUsers[j].Email;
                                            }
                                            //$scope.updatedDateForInitiation = $scope.ConvertDateFormat(data[i].ActivityBy.LastModifiedDate);
                                        }
                                    }
                                }
                                $scope.updatedDateForInitiation = $rootScope.changeDateTime(data[i].LastModifiedDate);
                                $scope.Date = $scope.ConvertDateFormat(data[i].LastModifiedDate);
                                $scope.lastActivity = $scope.ConvertDateFormat(data[i].LastModifiedDate);
                                var tempactivity = {
                                    Activity: $scope.providerName + " was Credentialing Initiated for " + $scope.planName,
                                    ActivityBy: data[i].ActivityBy,
                                    ActivityByID: data[i].ActivityByID,
                                    ActivityStatus: data[i].ActivityStatus,
                                    ActivityStatusType: data[i].ActivityStatusType,
                                    ActivityByName: $scope.updatedByForInitiation,
                                    LastModifiedDate: $scope.updatedDateForInitiation
                                };                                
                                $rootScope.timelineActivity.push(tempactivity);
                            }
                            else if (data[i].ActivityStatus == "Completed" && $rootScope.isrecredentialing == "ReCredentialing") {
                                $scope.Initiated = "progtrckr-done";
                                $scope.isdeCredentialed = true;
                                $scope.$parent.PSVTabStatus = true;
                                if (data[i].ActivityBy != null && data[i].ActivityBy.Profile != null && data[i].ActivityBy.Profile.PersonalDetail != null) {
                                    if (data[i].ActivityBy.Profile != null && data[i].ActivityBy.Profile.PersonalDetail != null) {
                                        if (data[i].ActivityBy.Profile.PersonalDetail.FirstName != null) {
                                            FirstName = data[i].ActivityBy.Profile.PersonalDetail.FirstName;
                                        } else {
                                            FirstName = "";
                                        }
                                        if (data[i].ActivityBy.Profile.PersonalDetail.MiddleName != null) {
                                            MiddleName = data[i].ActivityBy.Profile.PersonalDetail.MiddleName;
                                        } else {
                                            MiddleName = "";
                                        }
                                        if (data[i].ActivityBy.Profile.PersonalDetail.LastName != null) {
                                            LastName = data[i].ActivityBy.Profile.PersonalDetail.LastName;
                                        } else {
                                            LastName = "";
                                        }
                                    }
                                    $scope.InitiatedBy = FirstName + " " + MiddleName + " " + LastName;
                                }
                                if (data[i].ActivityBy != null) {
                                    for (var j = 0; j < $scope.LoginUsers.length; j++) {
                                        if (data[i].ActivityBy.AuthenicateUserId == $scope.LoginUsers[j].Id) {
                                            //$scope.timeStamp.push({ updateBy: $scope.LoginUsers[j].Email })
                                            if ($scope.LoginUsers[j].FullName != null) {

                                                $scope.updatedByForReCredentialing = $scope.LoginUsers[j].FullName;
                                            }
                                            else {
                                                $scope.updatedByForReCredentialing = $scope.LoginUsers[j].Email;
                                            }
                                        }
                                    }
                                }
                                $scope.updatedDateForReCredentialing = $rootScope.changeDateTime(data[i].LastModifiedDate);
                                $scope.decredentialedDate = $scope.ConvertDateFormat(data[i].LastModifiedDate);
                                $scope.lastActivity = $scope.ConvertDateFormat(data[i].LastModifiedDate);
                                var tempactivity = {
                                    Activity: $scope.providerName + " was Re-Credentialing Initiated for " + $scope.planName,
                                    ActivityBy: data[i].ActivityBy,
                                    ActivityByID: data[i].ActivityByID,
                                    ActivityStatus: data[i].ActivityStatus,
                                    ActivityStatusType: data[i].ActivityStatusType,
                                    ActivityByName: $scope.updatedByForReCredentialing,
                                    LastModifiedDate: $scope.updatedDateForInitiation
                                };
                                $rootScope.timelineActivity.push(tempactivity);
                            }
                            break;

                        case "PSV":
                            if (data[i].ActivityStatus == "Completed") {
                                $scope.Verified = "progtrckr-done";
                                $scope.isVerified = true;
                                $scope.$parent.CCMTabStatus = true;
                                if (data[i].ActivityBy != null && data[i].ActivityBy.Profile != null && data[i].ActivityBy.Profile.PersonalDetail != null) {
                                    if (data[i].ActivityBy.Profile != null && data[i].ActivityBy.Profile.PersonalDetail != null) {
                                        if (data[i].ActivityBy.Profile.PersonalDetail.FirstName != null) {
                                            FirstName = data[i].ActivityBy.Profile.PersonalDetail.FirstName;
                                        } else {
                                            FirstName = "";
                                        }
                                        if (data[i].ActivityBy.Profile.PersonalDetail.MiddleName != null) {
                                            MiddleName = data[i].ActivityBy.Profile.PersonalDetail.MiddleName;
                                        } else {
                                            MiddleName = "";
                                        }
                                        if (data[i].ActivityBy.Profile.PersonalDetail.LastName != null) {
                                            LastName = data[i].ActivityBy.Profile.PersonalDetail.LastName;
                                        } else {
                                            LastName = "";
                                        }
                                    }
                                    $scope.verifiedInitiatedBy = FirstName + " " + MiddleName + " " + LastName;
                                }
                                if (data[i].ActivityBy != null) {
                                    for (var j = 0; j < $scope.LoginUsers.length; j++) {
                                        if (data[i].ActivityBy.AuthenicateUserId == $scope.LoginUsers[j].Id) {
                                            //$scope.timeStamp.push({ updateBy: $scope.LoginUsers[j].Email })
                                            if ($scope.LoginUsers[j].FullName != null) {

                                                $scope.updatedByForPSV = $scope.LoginUsers[j].FullName;
                                            }
                                            else {
                                                $scope.updatedByForPSV = $scope.LoginUsers[j].Email;
                                            }
                                        }
                                    }
                                }
                                $scope.updatedDateForPSV = $rootScope.changeDateTime(data[i].LastModifiedDate);
                                $scope.verifiedDate = $scope.ConvertDateFormat(data[i].LastModifiedDate);
                                $scope.lastActivity = $scope.ConvertDateFormat(data[i].LastModifiedDate);
                                var tempactivity = {
                                    Activity: $scope.providerName + " PSV done for " + $scope.planName,
                                    ActivityBy: data[i].ActivityBy,
                                    ActivityByID: data[i].ActivityByID,
                                    ActivityStatus: data[i].ActivityStatus,
                                    ActivityStatusType: data[i].ActivityStatusType,
                                    ActivityByName: $scope.updatedByForPSV,
                                    LastModifiedDate: $scope.updatedDateForPSV
                                };
                                $rootScope.timelineActivity.push(tempactivity);
                            }
                            break;

                        case "CCMAppointment":
                            if (data[i].ActivityStatus == "Completed") {
                                $scope.CCMDone = "progtrckr-done";
                                $scope.isCCMDone = true;
                                $scope.$parent.PackageGeneratorTabStatus = true;
                                if (data[i].ActivityBy != null && data[i].ActivityBy.Profile != null && data[i].ActivityBy.Profile.PersonalDetail != null) {
                                    if (data[i].ActivityBy.Profile != null && data[i].ActivityBy.Profile.PersonalDetail != null) {
                                        if (data[i].ActivityBy.Profile.PersonalDetail.FirstName != null) {
                                            FirstName = data[i].ActivityBy.Profile.PersonalDetail.FirstName;
                                        } else {
                                            FirstName = "";
                                        }
                                        if (data[i].ActivityBy.Profile.PersonalDetail.MiddleName != null) {
                                            MiddleName = data[i].ActivityBy.Profile.PersonalDetail.MiddleName;
                                        } else {
                                            MiddleName = "";
                                        }
                                        if (data[i].ActivityBy.Profile.PersonalDetail.LastName != null) {
                                            LastName = data[i].ActivityBy.Profile.PersonalDetail.LastName;
                                        } else {
                                            LastName = "";
                                        }
                                    }
                                    $scope.CCMInitiatedBy = FirstName + " " + MiddleName + " " + LastName;
                                }
                                $scope.CCMDate = $scope.ConvertDateFormat(data[i].LastModifiedDate);
                                $scope.lastActivity = $scope.ConvertDateFormat(data[i].LastModifiedDate);
                                $scope.updatedDateForCCM = $rootScope.changeDateTime(data[i].LastModifiedDate);
                                if (data[i].ActivityBy != null) {
                                    for (var j = 0; j < $scope.LoginUsers.length; j++) {
                                        if (data[i].ActivityBy.AuthenicateUserId == $scope.LoginUsers[j].Id) {
                                            //$scope.timeStamp.push({ updateBy: $scope.LoginUsers[j].Email })
                                            if ($scope.LoginUsers[j].FullName != null) {

                                                $scope.updatedByForCCM = $scope.LoginUsers[j].FullName;
                                            }
                                            else {
                                                $scope.updatedByForCCM = $scope.LoginUsers[j].Email;
                                            }
                                        }
                                    }
                                }
                                var tempactivity = {
                                    Activity: $scope.providerName + " CCM Action done for " + $scope.planName,
                                    ActivityBy: data[i].ActivityBy,
                                    ActivityByID: data[i].ActivityByID,
                                    ActivityStatus: data[i].ActivityStatus,
                                    ActivityStatusType: data[i].ActivityStatusType,
                                    ActivityByName: $scope.updatedByForCCM,
                                    LastModifiedDate: $scope.updatedDateForCCM
                                };

                                
                                $rootScope.timelineActivity.push(tempactivity);
                            }
                            break;

                        case "Loading":
                            if (data[i].ActivityStatus == "Completed" && $rootScope.isrecredentialing == "Credentialing") {
                                $scope.Loaded = "progtrckr-done";
                                $scope.isLoaded = true;
                                if (data[i].ActivityBy != null && data[i].ActivityBy.Profile != null && data[i].ActivityBy.Profile.PersonalDetail != null) {
                                    if (data[i].ActivityBy.Profile != null && data[i].ActivityBy.Profile.PersonalDetail != null) {
                                        if (data[i].ActivityBy.Profile.PersonalDetail.FirstName != null) {
                                            FirstName = data[i].ActivityBy.Profile.PersonalDetail.FirstName;
                                        } else {
                                            FirstName = "";
                                        }
                                        if (data[i].ActivityBy.Profile.PersonalDetail.MiddleName != null) {
                                            MiddleName = data[i].ActivityBy.Profile.PersonalDetail.MiddleName;
                                        } else {
                                            MiddleName = "";
                                        }
                                        if (data[i].ActivityBy.Profile.PersonalDetail.LastName != null) {
                                            LastName = data[i].ActivityBy.Profile.PersonalDetail.LastName;
                                        } else {
                                            LastName = "";
                                        }
                                    }
                                    $scope.loadedInitiatedBy = FirstName + " " + MiddleName + " " + LastName;
                                }
                                for (var j = 0; j < $scope.LoginUsers.length; j++) {
                                    if (data[i].ActivityBy.AuthenicateUserId == $scope.LoginUsers[j].Id) {
                                        //$scope.timeStamp.push({ updateBy: $scope.LoginUsers[j].Email })
                                        if ($scope.LoginUsers[j].FullName != null) {

                                            $scope.updatedByForLoading = $scope.LoginUsers[j].FullName;
                                        }
                                        else {
                                            $rootScope.updatedByForLoading = $scope.LoginUsers[j].Email;
                                        }
                                        //$rootScope.initLoad = false;
                                        //$rootScope.initLoad1 = true;
                                    }
                                }
                                $rootScope.updatedDateForLoading = $rootScope.changeDateTime(data[i].LastModifiedDate);
                                $scope.loadedDate = $scope.ConvertDateFormat(data[i].LastModifiedDate);
                                for (var j in data) {
                                    if (data[j].ActivityStatus == "") {
                                        $scope.Date = $scope.ConvertDateFormat(data[j].LastModifiedDate);
                                    }
                                    else { $scope.Date = $scope.ConvertDateFormat(data[i].LastModifiedDate); }
                                    if ($scope.credentialingInfo.IsDelegated == 'NO') {
                                        $scope.Initiated = "progtrckr-done";
                                    }
                                }
                                //$scope.Date = $scope.ConvertDateFormat(data[i].LastModifiedDate)
                                $scope.lastActivity = $scope.ConvertDateFormat(data[i].LastModifiedDate);
                                var tempactivity = {
                                    Activity: $scope.providerName + " Load to Plan done for " + $scope.planName,
                                    ActivityBy: data[i].ActivityBy,
                                    ActivityByID: data[i].ActivityByID,
                                    ActivityStatus: data[i].ActivityStatus,
                                    ActivityStatusType: data[i].ActivityStatusType,
                                    ActivityByName: $scope.updatedByForLoading,
                                    LastModifiedDate: $scope.updatedDateForLoading
                                };
                                $rootScope.timelineActivity.push(tempactivity);
                            }
                            else if (data[i].ActivityStatus == "Completed" && $rootScope.isrecredentialing == "ReCredentialing") {
                                $scope.isdeCredentialed = true;
                                $scope.Loaded = "progtrckr-done";
                                $scope.isLoaded = true;
                                if (data[i].ActivityBy != null && data[i].ActivityBy.Profile != null && data[i].ActivityBy.Profile.PersonalDetail != null) {
                                    if (data[i].ActivityBy.Profile != null && data[i].ActivityBy.Profile.PersonalDetail != null) {
                                        if (data[i].ActivityBy.Profile.PersonalDetail.FirstName != null) {
                                            FirstName = data[i].ActivityBy.Profile.PersonalDetail.FirstName;
                                        } else {
                                            FirstName = "";
                                        }
                                        if (data[i].ActivityBy.Profile.PersonalDetail.MiddleName != null) {
                                            MiddleName = data[i].ActivityBy.Profile.PersonalDetail.MiddleName;
                                        } else {
                                            MiddleName = "";
                                        }
                                        if (data[i].ActivityBy.Profile.PersonalDetail.LastName != null) {
                                            LastName = data[i].ActivityBy.Profile.PersonalDetail.LastName;
                                        } else {
                                            LastName = "";
                                        }
                                    }
                                    $scope.loadedInitiatedBy = FirstName + " " + MiddleName + " " + LastName;
                                }
                                $scope.loadedDate = $scope.ConvertDateFormat(data[i].LastModifiedDate);
                                for (var j in data) {
                                    if (data[j].ActivityStatus == "") {
                                        $scope.Date = $scope.ConvertDateFormat(data[j].LastModifiedDate);
                                        $scope.decredentialedDate = $scope.ConvertDateFormat(data[j].LastModifiedDate);
                                    }
                                    else {
                                        $scope.Date = $scope.ConvertDateFormat(data[i].LastModifiedDate);
                                        $scope.decredentialedDate = $scope.ConvertDateFormat(data[j].LastModifiedDate);
                                    }
                                    if ($scope.credentialingInfo.IsDelegated == 'NO') {
                                        $scope.Initiated = "progtrckr-done";
                                    }
                                }
                                //$scope.Date = $scope.ConvertDateFormat(data[i].LastModifiedDate)
                                $scope.lastActivity = $scope.ConvertDateFormat(data[i].LastModifiedDate);
                                for (var j = 0; j < $scope.LoginUsers.length; j++) {
                                    if (data[i].ActivityBy.AuthenicateUserId == $scope.LoginUsers[j].Id) {
                                        //$scope.timeStamp.push({ updateBy: $scope.LoginUsers[j].Email })
                                        if ($scope.LoginUsers[j].FullName != null) {

                                            $scope.updatedByForReCredentialing = $scope.LoginUsers[j].FullName;
                                        }
                                        else {

                                            $scope.updatedByForReCredentialing = $scope.LoginUsers[j].Email;
                                        }
                                    }
                                }
                                $scope.updatedDateForReCredentialing = $rootScope.changeDateTime(data[i].LastModifiedDate);
                                //for (var j = 0; j < $scope.LoginUsers.length; j++) {
                                //    if (data[i].ActivityBy.AuthenicateUserId == $scope.LoginUsers[j].Id) {
                                //        //$scope.timeStamp.push({ updateBy: $scope.LoginUsers[j].Email })
                                //        $scope.updatedByFordeCredentialing = $scope.LoginUsers[j].Email;
                                //    }
                                //}
                                var tempactivity = {
                                    Activity: $scope.providerName + " was Re-Credentialing Initiated for " + $scope.planName,
                                    ActivityBy: data[i].ActivityBy,
                                    ActivityByID: data[i].ActivityByID,
                                    ActivityStatus: data[i].ActivityStatus,
                                    ActivityStatusType: data[i].ActivityStatusType,
                                    ActivityByName: $scope.updatedByForReCredentialing,
                                    LastModifiedDate: $scope.updatedDateForReCredentialing
                                };
                                $rootScope.timelineActivity.push(tempactivity);
                                
                            }
                            break;

                        case "Closure":
                            var tempactivity
                            if (data[i].ActivityStatus == "Completed") {
                                $scope.Completed = "progtrckr-done";
                                $scope.isCompleted = true;
                                //$scope.$parent.PlanReportTabStatus = true;
                                $scope.$parent.ViewOnlyMode = false;
                                $scope.isDroped = false;
                                if (data[i].ActivityBy != null && data[i].ActivityBy.Profile != null && data[i].ActivityBy.Profile.PersonalDetail != null) {
                                    if (data[i].ActivityBy.Profile != null && data[i].ActivityBy.Profile.PersonalDetail != null) {
                                        if (data[i].ActivityBy.Profile.PersonalDetail.FirstName != null) {
                                            FirstName = data[i].ActivityBy.Profile.PersonalDetail.FirstName;
                                        } else {
                                            FirstName = "";
                                        }
                                        if (data[i].ActivityBy.Profile.PersonalDetail.MiddleName != null) {
                                            MiddleName = data[i].ActivityBy.Profile.PersonalDetail.MiddleName;
                                        } else {
                                            MiddleName = "";
                                        }
                                        if (data[i].ActivityBy.Profile.PersonalDetail.LastName != null) {
                                            LastName = data[i].ActivityBy.Profile.PersonalDetail.LastName;
                                        } else {
                                            LastName = "";
                                        }
                                    }
                                    $scope.completedInitiatedBy = FirstName + " " + MiddleName + " " + LastName;
                                }
                                for (var j = 0; j < $scope.LoginUsers.length; j++) {
                                    if (data[i].ActivityBy.AuthenicateUserId == $scope.LoginUsers[j].Id) {
                                        //$scope.timeStamp.push({ updateBy: $scope.LoginUsers[j].Email })
                                        if ($scope.LoginUsers[j].FullName != null) {

                                            $scope.updatedByForCompleted = $scope.LoginUsers[j].FullName;
                                        }
                                        else {
                                            $scope.updatedByForCompleted = $scope.LoginUsers[j].Email;
                                        }
                                    }
                                }
                                $scope.updatedDateForCompleted = $rootScope.changeDateTime(data[i].LastModifiedDate);
                                $scope.completedDate = $scope.ConvertDateFormat(data[i].LastModifiedDate);
                                $scope.lastActivity = $scope.ConvertDateFormat(data[i].LastModifiedDate);
                                var tempactivity = {
                                    Activity: $scope.providerName + " Credentialing Process Completed for " + $scope.planName,
                                    ActivityBy: data[i].ActivityBy,
                                    ActivityByID: data[i].ActivityByID,
                                    ActivityStatus: data[i].ActivityStatus,
                                    ActivityStatusType: data[i].ActivityStatusType,
                                    ActivityByName: $scope.updatedByForCompleted,
                                    LastModifiedDate: $scope.updatedDateForCompleted
                                };
                                
                                if ($rootScope.isrecredentialing == "ReCredentialing") {
                                tempactivity.Activity = $scope.providerName +" "+ " Re-Credentialing Process Completed for " + $scope.planName;
                                } else if ($rootScope.isrecredentialing == "Credentialing") {
                                    tempactivity.Activity = $scope.providerName + " Credentialing Process Completed for " + $scope.planName;
                                }
                            $rootScope.timelineActivity.push(tempactivity);
                            }
                            break;

                        case "Report":
                            if (data[i].ActivityStatus == "Completed") {
                                //$scope.Loaded = "progtrckr-done";
                                //$scope.isLoaded = true;
                                $scope.$parent.PlanReportStatus = true;
                                //if (data[i].ActivityBy != null && data[i].ActivityBy.Profile != null && data[i].ActivityBy.Profile.PersonalDetail != null) {
                                //    if (data[i].ActivityBy.Profile != null && data[i].ActivityBy.Profile.PersonalDetail != null) {
                                //        if (data[i].ActivityBy.Profile.PersonalDetail.FirstName != null) {
                                //            FirstName = data[i].ActivityBy.Profile.PersonalDetail.FirstName;
                                //        } else {
                                //            FirstName = "";
                                //        }
                                //        if (data[i].ActivityBy.Profile.PersonalDetail.MiddleName != null) {
                                //            MiddleName = data[i].ActivityBy.Profile.PersonalDetail.MiddleName;
                                //        } else {
                                //            MiddleName = "";
                                //        }
                                //        if (data[i].ActivityBy.Profile.PersonalDetail.LastName != null) {
                                //            LastName = data[i].ActivityBy.Profile.PersonalDetail.LastName;
                                //        } else {
                                //            LastName = "";
                                //        }
                                //    }
                                //    $scope.loadedInitiatedBy = FirstName + " " + MiddleName + " " + LastName;
                                //    //if (i == (data.length - 1)) {
                                //    //}
                                //}
                                //$scope.loadedDate = $scope.ConvertDateFormat(data[i].LastModifiedDate);
                                //$scope.lastActivity = $scope.ConvertDateFormat(data[i].LastModifiedDate);
                            }
                            break;

                        case "Timeline":
                            if (data[i].ActivityStatus == "Completed") {
                                var TimeLineActivityLogs = [];
                                for (var t = 0; t < data[i].Activities.length; t++) {
                                    TimeLineActivityLogs.push(data[i].Activities[t]);
                                }
                                for (var z = 0; z < TimeLineActivityLogs.length; z++) {
                                    TimeLineActivityLogs[z].LastModifiedDate = $rootScope.changeDateTime(TimeLineActivityLogs[z].LastModifiedDate)
                                }
                                for (var k = 0; k < TimeLineActivityLogs.length; k++) {
                                    for (var j = 0; j < $scope.LoginUsers.length; j++) {
                                        if (TimeLineActivityLogs[k].ActivityBy.AuthenicateUserId == $scope.LoginUsers[j].Id) {
                                            //$scope.timeStamp.push({ updateBy: $scope.LoginUsers[j].Email })
                                            if ($scope.LoginUsers[j].FullName != null) {

                                                TimeLineActivityLogs[k].ActivityByName = $scope.LoginUsers[j].FullName;
                                            }
                                            else {
                                                TimeLineActivityLogs[k].ActivityByName = $scope.LoginUsers[j].Email;
                                            }
                                        }
                                    }
                                    $rootScope.timelineActivity.reverse();
                                    $rootScope.timelineActivity.push(TimeLineActivityLogs[k]);
                                    $rootScope.timelineActivity = $rootScope.timelineActivity.unique();
                                }                                
                                $scope.$parent.PlanReportStatus = true;
                                //for (var a= 0; a < $scope.ActivityLog.length; a++) {
                                //    for (var j = 0; j < $scope.LoginUsers.length; j++) {
                                //        if ($scope.ActivityLog[a].ActivityBy.AuthenicateUserId == $scope.LoginUsers[j].Id) {
                                //            $scope.timeStamp.push({ updateBy: $scope.LoginUsers[j].Email })
                                //        }
                                //    }
                                //}
                            }
                            break;
                    }
                }
            }

            $scope.sortarray($rootScope.timelineActivity);
            $rootScope.timelineActivity = $rootScope.timelineActivity.unique();
            //$scope.sortarray($rootScope.timelineActivity);
            
               
        }

        //Tulasidhar
        //Sort the Timeline array based on Date Time
        $scope.sortarray = function (timelineActivity) {
            timelineActivity.sort(function (a, b) {
                return new Date(b.LastModifiedDate) - new Date(a.LastModifiedDate);
            });
        }

        //Sharath 
        //Remove duplicate 
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
       
        $scope.changeTimeAmPm = function (value) {
            var returnValue = value.toDateString();
            if (value == 'Not Available' || value == 'Invalid Date' || value == 'Day Off') { return 'Day Off'; }
            if (!value) { return ''; }
            if (angular.isDate(value)) {
                value = value.getHours() + ":" + value.getMinutes();
            }
            var time = value.split(":");
            var hours = time[0];
            var minutes = time[1];
            var ampm = hours >= 12 ? 'PM' : 'AM';
            hours = hours % 12;
            hours = hours ? hours : 12; // the hour '0' should be '12'
            minutes = minutes.length == 1 ? minutes < 10 ? '0' + minutes : minutes : minutes;
            var strTime = returnValue + " " + hours + ':' + minutes + ' ' + ampm;
            return strTime;
        }

        $scope.SaveActivity = function (Form_Div_Id) {
            //LoginUsersData();
            $($('#ConfirmationModal').find('button')[2]).attr('disabled', true);
            var $form = $("#" + Form_Div_Id).find("form");
            var credInfoId = $scope.credentialingInfoID;
            var FORMDATA = new FormData();
            var other_data = $form.serializeArray();
            $.each(other_data, function (key, input) {
                FORMDATA.append(input.name, input.value);
            });
            //FORMDATA.append("credInfoId", credInfoId);
            ResetFormForValidation($form);
            $scope.isError = false;
            $scope.isDisabled = true;
            if ($form.valid()) {
                $.ajax({
                    url: rootDir + '/Credentialing/CnD/AddTimelineActivity?credInfoId=' + credInfoId,
                    type: 'POST',
                    data: FORMDATA,
                    async: false,
                    cache: false,
                    contentType: false,
                    processData: false,
                    success: function (data) {
                        try {
                            data = JSON.parse(data);
                            if (data.status == "true") {
                                data.timelineActivity.ActivityBy = cduserdata;
                                $scope.TempObj.Activity = "";
                                data.timelineActivity.LastModifiedDate = $rootScope.changeDateTime(data.timelineActivity.LastModifiedDate);
                                $rootScope.timelineActivity.unshift(data.timelineActivity);
                                for (var j = 0; j < $scope.LoginUsers.length; j++) {
                                    if (data.timelineActivity.ActivityBy.cdUser.AuthenicateUserId == $scope.LoginUsers[j].Id) {
                                        //$scope.timeStamp.push({ updateBy: $scope.LoginUsers[j].Email })
                                        $rootScope.timelineActivity[($rootScope.timelineActivity.length - 1)].ActivityByName = $scope.LoginUsers[j].FullName;
                                    }
                                }
                                // $rootScope.timelineActivity[i].LastModifiedDate = $scope.changeDateTime($rootScope.timelineActivity[$rootScope.timelineActivity.length-1].LastModifiedDate)
                                //$scope.timelineTimeStamp.push({TimeStamp:$scope.stamp});

                                $('#ConfirmationModal').modal('hide');
                                $scope.IsMessage = true;
                                $timeout(function () {
                                    $scope.IsMessage = false;
                                }, 5000);
                            } else {
                            }
                        } catch (e) {
                        }
                    }
                });
            } else {
            }
        }

        function convertToEasternTimeZone(val) {

            //EST
            offset = -5.0

            clientDate = new Date(val);
            utc = clientDate.getTime() + (clientDate.getTimezoneOffset() * 60000);  //Convert to millisecond. 1000 milliseconds = 1 second, and 1 minute = 60 

            easternDate = new Date(utc + (3600000 * offset));
            return easternDate;
        }

        //$scope.SaveActivity = function (Form_Div_Id) {
        //    //LoginUsersData();
        //    var $form = $("#" + Form_Div_Id).find("form");
        //    var credInfoId = $scope.credentialingInfoID;
        //    ResetFormForValidation($form);
        //    $scope.isError = false;
        //    $scope.isDisabled = true;
        //    if ($form.valid()) {
        //        $http({
        //            method: 'POST',
        //            url: rootDir + '/Credentialing/CnD/AddTimelineActivity?credInfoId=' + credInfoId,
        //            data: new FormData($form[0])
        //        }).success(function (data, status, headers, config) {

        //            try {
        //                if (data.status == "true") {
        //                    data.timelineActivity.ActivityBy = cduserdata;
        //                    $scope.TempObj.Activity = "";
        //                    $rootScope.timelineActivity.push(data.timelineActivity);
        //                    for (var i = 0; i < $rootScope.timelineActivity.length; i++) {
        //                        $rootScope.timelineActivity[i].LastModifiedDate = $scope.changeDateTime($rootScope.timelineActivity[i].LastModifiedDate)
        //                    }
        //                    //$scope.timelineTimeStamp.push({TimeStamp:$scope.stamp});
        //                    $('#ConfirmationModal').modal('hide');
        //                    $scope.IsMessage = true;

        //                    $timeout(function () {
        //                        $scope.IsMessage = false;
        //                    }, 5000);

        //                } else {

        //                }

        //            } catch (e) {

        //            }
        //        }).error(function (data, status, headers, config) {

        //        });
        //    } else {

        //    }

        //}

        //String.prototype.replaceAll = function (s, r) { return this.split(s).join(r) }

        $rootScope.changeDateTime = function (values) {
            values = convertToEasternTimeZone(values);
            if (!values) { return ''; }
            var returnValue = values;
            var format;
            try {
                if (values.indexOf("/Date(") == 0) {
                    returnValue = new Date(parseInt(values.replace("/Date(", "").replace(")/", ""), 10));
                }
            } catch (e) {
                returnValue = returnValue;
            }
            if (angular.isDate(returnValue)) {
                //value = returnValue.setTime(returnValue.getTime() - returnValue.getTimezoneOffset() * 60 * 1000);
                value = returnValue.getHours() + ":" + returnValue.getMinutes();
            }
            else {
                var formatDate
                returnValue = values.split('T')[1];
                formatDate = new Date(values.split('T')[0]);
                value = returnValue;
                returnValue = formatDate;
            }
            var time = value.split(":");
            var hours = time[0];
            var minutes = time[1];
            var ampm = hours >= 12 ? 'PM' : 'AM';
            hours = hours % 12;
            hours = hours ? hours : 12; // the hour '0' should be '12'
            minutes = minutes.length == 1 ? minutes < 10 ? '0' + minutes : minutes : minutes;
            var d = returnValue.toString();
            var stampDate = d.split(' ');
            var strTime = stampDate[1] + ' ' + stampDate[2] + ' ' + stampDate[3] + ' ' + hours + ':' + minutes + ' ' + ampm;
            //minutes = minutes < 9 ? '00' : minutes;
            //if (format == true) {
            //    var strTime = stampDate[1] + ' ' + stampDate[2] + ' ' + stampDate[3] + ' ' + hours + ':' + minutes + ' ' + ampm;
            //}
            //else {
            //    stampDate = formatDate.toDateString();
            //    var strTime = stampDate + ' ' + hours + ':' + minutes + ' ' + ampm;
            //}
            return strTime;
        }

        $scope.isDisabled = true;
        $scope.check = function () {
            if ($scope.TempObj.Activity != "") {
                $scope.isDisabled = false;
            } else {
                $scope.isDisabled = true;
            }
        }

        $scope.initWarning = function () {
            $($('#ConfirmationModal').find('button')[2]).attr('disabled', false);
            $('#ConfirmationModal').modal();
        };
    });

function ResetFormForValidation(form) {
    form.removeData('validator');
    form.removeData('unobtrusiveValidation');
    $.validator.unobtrusive.parse(form);
}