Cred_SPA_App.controller('PlanSummaryController',
    function ($scope, $rootScope, $timeout, $http, messageAlertEngine) {

        $scope.PlanDetail = {};
        $scope.PlanLOB = [];

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

        $scope.getPlanData = function (planID) {
            if (!$scope.dataLoaded) {
                $http({
                    method: 'GET',
                    url: '/Credentialing/CnD/GetPlanAsync?planId=' + planID
                }).success(function (data, status, headers, config) {
                    console.log("success");
                    console.log(data);
                    $scope.fillData(data);

                }).error(function (data, status, headers, config) {
                    console.log(status);
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
        $scope.AcitivityLogs = [];

        $scope.getCredentialingInfo = function (profileID, planID) {
            $scope.dataLoaded = false;
            if (!$scope.dataLoaded) {
                $http({
                    method: 'GET',
                    url: '/Credentialing/CnD/GetCredentialingInfoAsync?profileId=' + profileID + '&planId=' + planID
                }).success(function (data, status, headers, config) {
                    console.log("success");
                    console.log(data);
                    console.log("mine");
                    
                    if (data.credInfo != null) {
                        $scope.ProviderDetail(data.credInfo);
                        $scope.CredentialingInfo = data.credInfo;
                        $scope.getCurrentActivity(data.credInfo);
                    }
                    
                }).error(function (data, status, headers, config) {
                    console.log(status);
                });
                $scope.dataLoaded = true;
            }
        };

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

            }

            if (data.Plan != null) {

                $scope.planName = data.Plan.PlanName;

            }

        }

        $scope.getCurrentActivity = function (data) {

            if (data != null) {

                if (data.CredentialingLogs != null && data.CredentialingLogs.length != 0 && data.CredentialingLogs[data.CredentialingLogs.length - 1].CredentialingActivityLogs != null) {

                    $scope.AcitivityLog = data.CredentialingLogs[data.CredentialingLogs.length - 1].CredentialingActivityLogs;

                }

                if ($scope.AcitivityLog != null) {

                    $scope.checkForStatus($scope.AcitivityLog);

                }

            }

        }
        $scope.Initiated = "progtrckr-todo";
        $scope.isInitiated = false;
        $scope.Verified = "progtrckr-todo";
        $scope.isVerified = false;
        $scope.CCMDone = "progtrckr-todo";
        $scope.isCCMDone = false;
        $scope.Loaded = "progtrckr-todo";
        $scope.isLoaded = false;
        $scope.Completed = "progtrckr-todo";
        $scope.isCompleted = false;
        $scope.isCredentialed = false;
        $scope.isreCredentialed = false;
        $scope.isdeCredentialed = false;

        $scope.checkForStatus = function (data) {

            var FirstName = "";
            var MiddleName = "";
            var LastName = "";

            if (data != null && data.length != 0) {

                for (var i = 0; i < data.length ; i++) {

                    switch (data[i].Activity) {

                        case "Initiation":

                            if (data[i].ActivityStatus == "Completed") {

                                $scope.Initiated = "progtrckr-done";
                                $scope.isInitiated = true;

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

                                    //if (i == (data.length - 1)) {

                                    //}

                                }

                                $scope.Date = $scope.ConvertDateFormat(data[i].LastModifiedDate);

                                $scope.lastActivity = $scope.ConvertDateFormat(data[i].LastModifiedDate);

                            }

                            break;

                        case "PSV":

                            if (data[i].ActivityStatus == "Completed") {

                                $scope.Verified = "progtrckr-done";
                                $scope.isVerified = true;

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

                                    //if (i == (data.length - 1)) {

                                    //}

                                }

                                $scope.verifiedDate = $scope.ConvertDateFormat(data[i].LastModifiedDate);

                                $scope.lastActivity = $scope.ConvertDateFormat(data[i].LastModifiedDate);

                            }

                            break;

                        case "CCMAppointment":

                            if (data[i].ActivityStatus == "Completed") {

                                $scope.CCMDone = "progtrckr-done";
                                $scope.isCCMDone = true;

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

                                    //if (i == (data.length - 1)) {

                                    //}

                                }

                                $scope.CCMDate = $scope.ConvertDateFormat(data[i].LastModifiedDate);

                                $scope.lastActivity = $scope.ConvertDateFormat(data[i].LastModifiedDate);

                            }

                            break;

                        case "Loading":

                            if (data[i].ActivityStatus == "Completed") {

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

                                    //if (i == (data.length - 1)) {

                                    //}

                                }

                                $scope.loadedDate = $scope.ConvertDateFormat(data[i].LastModifiedDate);

                                $scope.lastActivity = $scope.ConvertDateFormat(data[i].LastModifiedDate);

                            }

                            break;

                        case "Completed":

                            if (data[i].ActivityStatus == "Completed") {

                                $scope.Completed = "progtrckr-done";
                                $scope.isCompleted = true;

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

                                    //if (i == (data.length - 1)) {

                                    //}

                                }

                                $scope.completedDate = $scope.ConvertDateFormat(data[i].LastModifiedDate);

                                $scope.lastActivity = $scope.ConvertDateFormat(data[i].LastModifiedDate);

                            }

                            break;

                    }

                }

            }

        }

    });