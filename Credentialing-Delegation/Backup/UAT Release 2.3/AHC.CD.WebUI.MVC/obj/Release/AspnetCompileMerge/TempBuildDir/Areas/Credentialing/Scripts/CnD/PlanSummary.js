Cred_SPA_App.controller('PlanSummaryController',
    function ($scope, $rootScope, $timeout, $http, messageAlertEngine) {

        $scope.PlanDetail = {};
        $scope.PlanLOB = [];
        $scope.IsMessage = false;

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
                    url: rootDir + '/Credentialing/CnD/GetPlanAsync?planId=' + planID
                }).success(function (data, status, headers, config) {
                    //console.log("success");
                    //console.log(data);
                    $scope.fillData(data);

                }).error(function (data, status, headers, config) {
                    //console.log(status);
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
                    url: rootDir + '/Credentialing/CnD/GetCredentialingInfoAsync?credInfoID=' + $scope.credentialingInfoID
                }).success(function (data, status, headers, config) {
                    //console.log("success");
                    //console.log(data);
                    //console.log("mine");

                    if (data.credInfo != null) {
                        $scope.ProviderDetail(data.credInfo);
                        $scope.CredentialingInfo = data.credInfo;
                        $scope.getCurrentActivity(data.credInfo);
                    }

                }).error(function (data, status, headers, config) {
                    //console.log(status);
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

                    $scope.AcitivityLog = data.CredentialingLogs[0].CredentialingActivityLogs;

                }

                $scope.lastActivity = data.CredentialingLogs[data.CredentialingLogs.length - 1].CredentialingActivityLogs;
                var index = $scope.lastActivity.length - 1;

                if ($scope.lastActivity[index].Activity == "Dropped") {

                    if ($scope.lastActivity[index].ActivityStatus == "Completed") {

                        $scope.isDroped = true;
                        $scope.lastActivity = $scope.ConvertDateFormat($scope.lastActivity[index].LastModifiedDate);
                        $scope.SubmitSPA($scope.credentialingInfoID);

                    }

                }

                if ($scope.AcitivityLog != null) {

                    $scope.checkForStatus($scope.AcitivityLog);

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
        $scope.timelineActivity = [];

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

                        case "Closure":

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

                        case "Timeline":

                            if (data[i].ActivityStatus == "Completed") {

                                $scope.timelineActivity = data[i].Activities;

                            }

                            break;

                    }

                }

            }

        }

        $scope.SaveActivity = function (Form_Div_Id) {
            var $form = $("#" + Form_Div_Id).find("form");
            var credInfoId = $scope.credentialingInfoID;
            ResetFormForValidation($form);
            $scope.isError = false;
            
            if ($form.valid()) {
                $.ajax({
                    url: rootDir + '/CnD/AddTimelineActivity?credInfoId=' + credInfoId,
                    type: 'POST',
                    data: new FormData($form[0]),
                    async: false,
                    cache: false,
                    contentType: false,
                    processData: false,
                    success: function (data) {
                        //console.log(data);
                        if (data.status == "true") {

                            $scope.TempObj.Activity = "";
                            $scope.timelineActivity.push(data.timelineActivity);
                            $('#ConfirmationModal').modal('hide');
                            $scope.IsMessage = true;

                            $timeout(function () {
                                $scope.IsMessage = false;
                            }, 5000);

                        } else {

                        }
                    }
                });
            } else {

            }
        }

        $scope.initWarning = function () {

            $('#ConfirmationModal').modal();
        };

    });

function ResetFormForValidation(form) {
    form.removeData('validator');
    form.removeData('unobtrusiveValidation');
    $.validator.unobtrusive.parse(form);
}