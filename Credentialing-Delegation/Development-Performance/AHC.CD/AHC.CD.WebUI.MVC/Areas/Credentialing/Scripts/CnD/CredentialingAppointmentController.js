// -------------------- CCM controller Angular Module-------------------------------
//---------- Author : Santosh -------------
(function () {
    fetchData().then(bootstrapApplication);
    var CCOApp = angular.module("CCOApp", ['ui.bootstrap', 'ngTable']);
    CCOApp.value("ScheduledAppointment", []);
    CCOApp.value("NoAppointmentScheuled", []);
    CCOApp.value("EventDate", []);

    function fetchData() {
        var initInjector = angular.injector(["ng"]);
        var $http = initInjector.get("$http");
        var $rootScope = initInjector.get("$rootScope");
        return $http.get(rootDir + '/Credentialing/CnD/GetAllCredentialInfoListHistory').then(function (response) {
            CCOApp.value("ScheduledAppointment", response.data.AppointmentScheule);
            CCOApp.value("NoAppointmentScheuled", response.data.NoAppointmentScheule);
        }, function (errorResponse) {
            // Handle error case
        });
    }

    function bootstrapApplication() {
        angular.element(document).ready(function () {
            angular.bootstrap(document, ["CCOApp"]);
        });
    }



    //CCOApp.directive("mAppLoading", function ($rootScope) {
    //    return ({
    //        compile: compile,
    //        restrict: "C"
    //    });
        
    //    function compile(scope, element, attributes, rootScope) {
    //        rootScope.LOadingW = true;
    //    }
    //});
    CCOApp.directive('tooltip', function () {
        return function (scope, elem) {
            elem.tooltip();
        };
    });

    


    CCOApp.run(['$rootScope', 'ScheduledAppointment', 'NoAppointmentScheuled', function ($rootScope, ScheduledAppointment, NoAppointmentScheuled, EventDate) {
        //$rootScope.LOadingW = false;
        var squash = function (arr) {
            var tmp = [];
            for (var i = 0; i < arr.length; i++) {
                if (tmp.indexOf(arr[i]) == -1) {
                    tmp.push(arr[i]);
                }
            }
            return tmp;
        }
        $rootScope.LoadingAjax = true;
        $rootScope.tableHeader = false;
        $rootScope.tableHeaderForNonCredValue = false;
        var ConvertDateTo = function (value) {
            var shortDate = null;
            if (value) {
                var regex = /-?\d+/;
                var matches = regex.exec(value);
                var dt = new Date(parseInt(matches[0]));
                var month = dt.getMonth() + 1;
                var monthString = month > 9 ? month : '0' + month;
                //var monthName = monthNames[month];
                var day = dt.getDate();
                var dayString = day > 9 ? day : '0' + day;
                var year = dt.getFullYear();
                shortDate = monthString + '/' + dayString + '/' + year;
                //shortDate = dayString + 'th ' + monthName + ',' + year;
            }
            return shortDate;
        };
        $rootScope.DoneCreadentialing = [];
        $rootScope.NoCreadentialing = [];
        $rootScope.AppointmentDate = [];
        $rootScope.ForAppointmentDetail = [];
        $rootScope.Plans = [];
        for (var i = 0; i < NoAppointmentScheuled.length; i++) {
            $rootScope.NoCreadentialing.push({
                CredentialingInfoID: NoAppointmentScheuled[i].CredentialingInfoID,
                ProviderID: NoAppointmentScheuled[i].CredentialingLogs[0].CredentialingAppointmentDetail.CredentialingAppointmentDetailID,
                FirstName: NoAppointmentScheuled[i].Profile.PersonalDetail.FirstName,
                LastName: NoAppointmentScheuled[i].Profile.PersonalDetail.LastName,
                ProviderTitles: angular.copy(NoAppointmentScheuled[i].Profile.PersonalDetail.ProviderTitles),
                Specialities: angular.copy(NoAppointmentScheuled[i].CredentialingLogs[0].CredentialingAppointmentDetail.CredentialingSpecialityLists),
                CredentialingDate: ConvertDateTo(NoAppointmentScheuled[i].InitiationDate),
                Plan: NoAppointmentScheuled[i].Plan.PlanName,
                RecommendedLevel: NoAppointmentScheuled[i].CredentialingLogs[0].CredentialingAppointmentDetail.RecommendedLevel,
                Status: "Verified",
                ProfileID: NoAppointmentScheuled[i].Profile.ProfileID,
                statusForApp: sessionStorage.getItem('CredID') == null ? false : NoAppointmentScheuled[i].CredentialingInfoID == sessionStorage.getItem('CredID') ? true : false
            });
            $rootScope.Plans.push(NoAppointmentScheuled[i].Plan.PlanName)
        }
        for (var i in $rootScope.NoCreadentialing) {
            if ($rootScope.NoCreadentialing[i].statusForApp == true) {
                $rootScope.ForAppointmentDetail.push($rootScope.NoCreadentialing[i]);
                break;
            }
        }
        for (var i = 0; i < ScheduledAppointment.length; i++) {
            $rootScope.DoneCreadentialing.push({
                CredentialingInfoID: ScheduledAppointment[i].CredentialingInfoID,
                ProviderID: ScheduledAppointment[i].CredentialingLogs[0].CredentialingAppointmentDetail.CredentialingAppointmentDetailID,
                FirstName: ScheduledAppointment[i].Profile.PersonalDetail.FirstName,
                LastName: ScheduledAppointment[i].Profile.PersonalDetail.LastName,
                ProviderTitles: angular.copy(ScheduledAppointment[i].Profile.PersonalDetail.ProviderTitles),
                Specialities: angular.copy(ScheduledAppointment[i].CredentialingLogs[0].CredentialingAppointmentDetail.CredentialingSpecialityLists),
                CredentialingDate: ConvertDateTo(ScheduledAppointment[i].InitiationDate),
                Plan: ScheduledAppointment[i].Plan.PlanName,
                RecommendedLevel: ScheduledAppointment[i].CredentialingLogs[0].CredentialingAppointmentDetail.RecommendedLevel,
                AppointmentDate: ConvertDateTo(ScheduledAppointment[i].CredentialingLogs[0].CredentialingAppointmentDetail.CredentialingAppointmentSchedule.AppointmentDate),
                Status: "Verified",
                ProfileID: ScheduledAppointment[i].Profile.ProfileID,
                statusForApp: false
            });
            console.log(ConvertDateTo(ScheduledAppointment[i].CredentialingLogs[0].CredentialingAppointmentDetail.CredentialingAppointmentSchedule.AppointmentDate));
            $rootScope.AppointmentDate.push({ date: ConvertDateTo(ScheduledAppointment[i].CredentialingLogs[0].CredentialingAppointmentDetail.CredentialingAppointmentSchedule.AppointmentDate), status: "full" });
            $rootScope.Plans.push(ScheduledAppointment[i].Plan.PlanName);
        }
        $rootScope.AppointmentDate = squash($rootScope.AppointmentDate);
        $rootScope.Plans = angular.copy(squash($rootScope.Plans));
        for (var i = 0; i < $rootScope.NoCreadentialing.length; i++) {
            if ($rootScope.NoCreadentialing[i].FirstName == null) {
                $rootScope.NoCreadentialing[i].Name = "";
            }
            else {
                $rootScope.NoCreadentialing[i].Name = $rootScope.NoCreadentialing[i].FirstName;
            }
            if ($rootScope.NoCreadentialing[i].LastName == null) {
                $rootScope.NoCreadentialing[i].Name += "";
            }
            else {
                $rootScope.NoCreadentialing[i].Name += " " + $rootScope.NoCreadentialing[i].LastName;
            }
            if ($rootScope.NoCreadentialing[i].ProviderTitles.length == 0) {
                $rootScope.NoCreadentialing[i].ProviderTitle = "";
            }
            else {
                for (var j = 0; j < $rootScope.NoCreadentialing[i].ProviderTitles.length; j++) {
                    if (j == 0) {
                        $rootScope.NoCreadentialing[i].ProviderTitle = $rootScope.NoCreadentialing[i].ProviderTitles[j].ProviderType.Title;
                    }
                    else {
                        $rootScope.NoCreadentialing[i].ProviderTitle += "," + $rootScope.NoCreadentialing[i].ProviderTitles[j].ProviderType.Title;
                    }
                }
            }
            if ($rootScope.NoCreadentialing[i].Specialities.length == 0) {
                $rootScope.NoCreadentialing[i].Specialty = "";
            }
            else {
                for (var j = 0; j < $rootScope.NoCreadentialing[i].Specialities.length; j++) {
                    if ($rootScope.NoCreadentialing[i].Specialities[j].Status == 'Active') {
                        if (j == 0) {
                            $rootScope.NoCreadentialing[i].Specialty = $rootScope.NoCreadentialing[i].Specialities[j].Name;
                        }
                        else {
                            $rootScope.NoCreadentialing[i].Specialty += "," + $rootScope.NoCreadentialing[i].Specialities[j].Name;
                        }
                    }
                }
            }
        }
        $('#laodingdiv').remove();
    }]);

    CCOApp.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

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
                $timeout(animateMessageAlertOff, 2000);
            }
        }

        $rootScope.closeAlertMessage = function () {
            $rootScope.messageDesc = "";
            $rootScope.activeMessageDiv = "";
            $rootScope.messageType = "";
        }
    }]);



    CCOApp.filter('AddAppointment', function ($rootScope) {
        return function (check) {

            var filtered = [];

            return filtered;
        };
    });

    CCOApp.filter('FilterByDate', function ($rootScope) {
        var convert = function (str) {
            var date = new Date(str),
                mnth = ("0" + (date.getMonth() + 1)).slice(-2),
                day = ("0" + date.getDate()).slice(-2);
            return [mnth, day, date.getFullYear()].join("/");
        }
        return function (items, date, filterPlan) {

            var filtered = [];
            if (filterPlan == "") {
                for (var i in items) {
                    if (items[i].AppointmentDate === convert(date)) {
                        filtered.push(items[i]);

                    }
                }
            }
            else {
                for (var i in items) {
                    if (items[i].AppointmentDate === convert(date) && items[i].Plan == filterPlan) {
                        filtered.push(items[i]);

                    }
                }
            }
            $rootScope.tableHeader = filtered.length > 0 ? true : false;
            return filtered;
        };
    });
    CCOApp.filter('FilterByDateForNonCredValue', function ($rootScope) {
        var convert = function (str) {
            var date = new Date(str),
                mnth = ("0" + (date.getMonth() + 1)).slice(-2),
                day = ("0" + date.getDate()).slice(-2);
            return [mnth, day, date.getFullYear()].join("/");
        }
        return function (items, filterPlan) {

            var filtered = [];
            if (filterPlan == "") {
                for (var i in items) {
                    filtered.push(items[i]);
                }
            }
            else {
                for (var i in items) {
                    if (items[i].Plan.trimRight() == filterPlan) {
                        filtered.push(items[i]);
                    }
                }
            }
            $rootScope.tableHeaderForNonCredValue = filtered.length > 0 ? true : false;
            return filtered;
        };
    });


    CCOApp.controller("CCOController", function ($rootScope, $scope, $filter, $http, $timeout, messageAlertEngine) {
        $scope.loadingfunction = function () {
            $('#laodingdiv').hide();
            $('#laodingdiv').remove();
        }
        $rootScope.LoadingAjax = false;
        //$timeout(function () {
        //    $rootScope.LOadingW = true;
        //},1000)
        console.log($rootScope.NoCreadentialing);
        $scope.filterPlan = "";
        $scope.getValue = function (data) {
            $scope.filterPlan = data.trimRight();
        };
        $scope.getDayClass = function (date, mode) {
            if (mode === 'day') {
                //console.log(convert(date));
                for (var i in $rootScope.AppointmentDate) {
                    if ($rootScope.AppointmentDate[i].date === convert(date)) {
                        return $rootScope.AppointmentDate[i].status;
                    }
                }
            }
        }
        var squash = function (arr) {
            var tmp = [];
            for (var i = 0; i < arr.length; i++) {
                if (tmp.indexOf(arr[i]) == -1) {
                    tmp.push(arr[i]);
                }
            }
            return tmp;
        }
        var convertDateData = function (str) {
            var data = new Date(str);
            var month=data.getMonth()+1;
            var date=data.getDate();
            var year = data.getFullYear();
            return month + "/" + date + "/" + year;
        }
        var convert = function (str) {
            var date = new Date(str),
                mnth = ("0" + (date.getMonth() + 1)).slice(-2),
                day = ("0" + date.getDate()).slice(-2);
            return [mnth, day, date.getFullYear()].join("/");
        }
        $scope.getDataByMenu = function (data) {
            $scope.dt = new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate());
        };
        //$("#calendar").data("$datepickerController").refreshView()

        $scope.getDataByMenu($rootScope.DoneCreadentialing);


        $scope.ListForAppointment = function (data) {
            var id = $rootScope.ForAppointmentDetail.indexOf(data);
            var idx = $rootScope.NoCreadentialing.indexOf(data);
            if (id > -1) {
                $rootScope.ForAppointmentDetail.splice(id, 1);
                $rootScope.NoCreadentialing[idx].statusForApp = false;
            }
            else {
                $rootScope.NoCreadentialing[idx].statusForApp = true;
                $rootScope.ForAppointmentDetail.push(data);
            }
            //alert($rootScope.ForAppointmentDetail.length);
        }
        $scope.AddSelectedCredentialing = function () {
            $rootScope.LoadingAjax = true;
            var SelectedCredentialingInfoIDs = [];
            var SelectedProfileIDs = [];
            var AppDate = convertDateData($scope.dt);
            var AppointmentDateTObeSaved = convert($scope.dt);
            for (var i in $rootScope.ForAppointmentDetail) {
                SelectedCredentialingInfoIDs.push($rootScope.ForAppointmentDetail[i].CredentialingInfoID);
                SelectedProfileIDs.push($rootScope.ForAppointmentDetail[i].ProviderID);
            }
            var obj = {
                CredentialingInfoIDs: SelectedCredentialingInfoIDs,
                ProviderIDArray: SelectedProfileIDs,
                AppointmentDate: AppDate,
                AppointmentDateTOBESAVED: AppointmentDateTObeSaved
            }
            $http.post(rootDir + '/Credentialing/CnD/SetAppointment', obj).success(function (data, status, headers, config) {
                if (data.status = "true") {
                    for (var i in $rootScope.ForAppointmentDetail) {
                        $rootScope.NoCreadentialing.splice($rootScope.NoCreadentialing.indexOf($rootScope.ForAppointmentDetail[i]), 1);
                        $rootScope.DoneCreadentialing.push({
                            CredentialingInfoID: $rootScope.ForAppointmentDetail[i].CredentialingInfoID,
                            ProviderID: $rootScope.ForAppointmentDetail[i].ProviderID,
                            FirstName: $rootScope.ForAppointmentDetail[i].FirstName,
                            LastName: $rootScope.ForAppointmentDetail[i].LastName,
                            ProviderTitles: angular.copy($rootScope.ForAppointmentDetail[i].ProviderTitles),
                            Specialities: angular.copy($rootScope.ForAppointmentDetail[i].Specialities),
                            CredentialingDate: $rootScope.ForAppointmentDetail[i].CredentialingDate,
                            Plan: $rootScope.ForAppointmentDetail[i].Plan,
                            RecommendedLevel: $rootScope.ForAppointmentDetail[i].RecommendedLevel,
                            AppointmentDate: AppointmentDateTObeSaved,
                            Status: "Verified",
                            ProfileID: $rootScope.ForAppointmentDetail[i].ProfileID,
                            statusForApp: false
                        });
                    }
                    var flag = 0;
                    for (var i in $rootScope.AppointmentDate) {
                        if ($rootScope.AppointmentDate[i].date == AppointmentDateTObeSaved) {
                            flag = 1;
                            break;
                        }
                    }
                    if (flag == 0) {
                        $rootScope.AppointmentDate.push({ date: AppointmentDateTObeSaved, status: "full" });
                        $("#calendar").data("$datepickerController").refreshView()
                    }
                    $rootScope.ForAppointmentDetail = [];
                    messageAlertEngine.callAlertMessage("AppointmentSuccess", "Appointment Scheduled Successfully", "success", true);
                }
                else {
                    messageAlertEngine.callAlertMessage("Appointmentfailure", "Please try after sometime", "danger", true);
                }
                $rootScope.LoadingAjax = false;
            }).error(function (data, status, headers, config) {
                messageAlertEngine.callAlertMessage("Appointmentfailure", "Please try after sometime", "danger", true);
                $rootScope.LoadingAjax = false;

            });
        };

        $scope.RemoveAppoinment = function (index, cred) {
            $rootScope.LoadingAjax = true;

            if (cred.FirstName == null) {
                cred.Name = "";
            }
            else {
                cred.Name = cred.FirstName;
            }
            if (cred.LastName == null) {
                cred.Name += "";
            }
            else {
                cred.Name += " " + cred.LastName;
            }
            if (cred.ProviderTitles.length == 0) {
                cred.ProviderTitle = "";
            }
            else {
                for (var j = 0; j < cred.ProviderTitles.length; j++) {
                    if (j == 0) {
                        cred.ProviderTitle = cred.ProviderTitles[j].ProviderType.Title;
                    }
                    else {
                        cred.ProviderTitle += "," + cred.ProviderTitles[j].ProviderType.Title;
                    }
                }
            }
            if (cred.Specialities.length == 0) {
                cred.Specialty = "";
            }
            else {
                for (var j = 0; j < cred.Specialities.length; j++) {
                    if (cred.Specialities[j].Status == 'Active') {
                        if (j == 0) {
                            cred.Specialty = cred.Specialities[j].Name;
                        }
                        else {
                            cred.Specialty += "," + cred.Specialities[j].Name;
                        }
                    }
                }
            }
            var AppointmentDateTObeRemoved = convert($scope.dt);
            var AppDate = convertDateData($scope.dt); 
            var obj = {
                ProviderID: cred.ProviderID,
                ProfileID: cred.ProfileID,
                AppointmentDate: AppDate,
                AppointmentDateTOBEREMOVED: AppointmentDateTObeRemoved
            }
            $http.post(rootDir + '/Credentialing/CnD/RemoveAppointment', obj).success(function (data, status, headers, config) {
                if (data.status = "true") {
                    var idx = -1;
                    for (var i in $rootScope.DoneCreadentialing) {
                        if ($rootScope.DoneCreadentialing[i].CredentialingInfoID == cred.CredentialingInfoID) {
                            idx = i;
                            break;
                        }
                    }
                    $rootScope.NoCreadentialing.push({
                        CredentialingInfoID: $rootScope.DoneCreadentialing[idx].CredentialingInfoID,
                        ProviderID: $rootScope.DoneCreadentialing[idx].ProviderID,
                        FirstName: $rootScope.DoneCreadentialing[idx].FirstName,
                        LastName: $rootScope.DoneCreadentialing[idx].LastName,
                        ProviderTitles: angular.copy($rootScope.DoneCreadentialing[idx].ProviderTitles),
                        Specialities: angular.copy($rootScope.DoneCreadentialing[idx].Specialities),
                        CredentialingDate: $rootScope.DoneCreadentialing[idx].CredentialingDate,
                        Plan: $rootScope.DoneCreadentialing[idx].Plan,
                        RecommendedLevel: $rootScope.DoneCreadentialing[idx].RecommendedLevel,
                        Status: "Verified",
                        ProfileID: $rootScope.DoneCreadentialing[idx].ProfileID,
                        statusForApp: false,
                        Name: cred.Name,
                        ProviderTitle: cred.ProviderTitle,
                        Specialty: cred.Specialty
                    });

                    if (idx > -1) {
                        $rootScope.DoneCreadentialing.splice(idx, 1);
                    }
                    var flag = 0;
                    for (var i in $rootScope.DoneCreadentialing) {
                        if ($rootScope.DoneCreadentialing[i].AppointmentDate == AppointmentDateTObeRemoved) {
                            flag = 1;
                            break;
                        }
                    }
                    if (flag == 0) {
                        var idindex = -1;
                        for (var i in $rootScope.AppointmentDate) {
                            if ($rootScope.AppointmentDate[i].date == AppointmentDateTObeRemoved) {
                                idindex = i;
                                break;
                            }
                        }
                        if (idindex > -1) {
                            $rootScope.AppointmentDate.splice(idindex, 1);
                            $("#calendar").data("$datepickerController").refreshView()
                        } else {

                        }
                    }
                    //messageAlertEngine.callAlertMessage("AppointmentCancelSuccess", "Appointment Canceled Successfully", "success", true);
                }
                else {
                    //messageAlertEngine.callAlertMessage("AppointmentCancelfailure", "Please try after sometime", "danger", true);
                }
                $rootScope.LoadingAjax = false;
            }).error(function (data, status, headers, config) {
                messageAlertEngine.callAlertMessage("AppointmentCancelfailure", "Please try after sometime", "danger", true);
                $rootScope.LoadingAjax = false;
            });
        };
    })
}());
//------------------------- angular module ----------------------------




