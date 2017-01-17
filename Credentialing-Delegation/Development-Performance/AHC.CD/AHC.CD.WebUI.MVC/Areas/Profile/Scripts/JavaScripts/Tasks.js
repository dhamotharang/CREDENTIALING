//manideep
profileApp.factory('Resource', ['$q', '$rootScope', '$filter', '$timeout', function ($q, $rootScope, $filter, $timeout) {

    function getPage(start, number, params) {

        var deferred = $q.defer();

        $rootScope.filtered = params.search.predicateObject ? $filter('filter')($rootScope.trackerItems1, params.search.predicateObject) : $rootScope.trackerItems1;

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

}]);


$(document).ready(function () {
    $("#sidemenu").addClass("menu-in");
    $("#page-wrapper").addClass("menuup");
});

var TempFollowupHelper = [];
profileApp.controller('ProviderTasksController', ['$scope', '$http', '$q', '$rootScope', 'Resource', 'messageAlertEngine', function ($scope, $http, $q, $rootScope, Resource, messageAlertEngine) {
    $rootScope.TasksLoaded = false;
    $scope.TableView = true;
    var ctrl = this;
    this.displayed = [];

    $scope.users = [];
    $scope.Tasks = [];


    function CDUsersData() {
        var defer = $q.defer();
        $http.get(rootDir + '/TaskTracker/GetAllCDUsers').
            success(function (data, status, headers, config) {
                $scope.CDUsers = data;
                defer.resolve(data);
            }).
            error(function (data, status, headers, config) {
                defer.reject();
            });
        return defer.promise;
    }
    function HospitalsData() {
        var defer = $q.defer();
        $http.get(rootDir + '/Profile/MasterData/GetAllHospitals').
        success(function (data, status, headers, config) {
            $scope.Hospitals = data;
            defer.resolve(data);
        }).
        error(function (data, status, headers, config) {
            defer.reject();
        });
        return defer.promise;
    }
    function InsuranceCompaniesData() {
        var defer = $q.defer();
        $http.get(rootDir + '/TaskTracker/GetAllInsuranceCompanies').
        success(function (data, status, headers, config) {
            $scope.InsuranceCompanies = data;

            defer.resolve(data);
        }).
        error(function (data, status, headers, config) {
            defer.reject();
        });
        return defer.promise;
    }
    function ProvidersData() {
        var defer = $q.defer();
        $http.get(rootDir + '/TaskTracker/GetAllProviders').
            success(function (data, status, headers, config) {
                $scope.Providers = data;
                defer.resolve(data);
            }).
            error(function (data, status, headers, config) {
                defer.reject();
            });
        return defer.promise;
    }
    function LoginUsersData() {
        var defer = $q.defer();
        $http.get(rootDir + '/TaskTracker/GetAllUsers').
            success(function (data, status, headers, config) {
                $scope.LoginUsers = data;
                defer.resolve(data);
            }).
            error(function (data, status, headers, config) {
                defer.reject();
            });
        return defer.promise;
    }
    function SubSectionData() {
        var defer = $q.defer();
        $http.get(rootDir + "/MasterDataNew/GetAllProfileSubSections").
            success(function (data, status, headers, config) {
                $scope.SubSections = data;
                defer.resolve(data);
            }).
            error(function (data, status, headers, config) {
                defer.reject();
            });
        return defer.promise;
    }


    $rootScope.TasksLoaded = true;
    $scope.dataLoaded = false;
    $rootScope.$on("Tasks", function () {
        if (!$scope.dataLoaded) {
            $rootScope.TasksLoaded = false;
            $http.get(rootDir + '/TaskTracker/GetAllTasksByProfileId?profileid=' + profileId).
                success(function (data, status, headers, config) {
                    $scope.Tasks = angular.copy(data);
                    
                }).
            error(function (data, status, headers, config) {
                $rootScope.TasksLoaded = true;
            });
            $scope.LoadData();
            $scope.dataLoaded = true;
        }
        
    });


    $scope.LoadData = function () {
        $scope.progressbar = false;
        $q.all([$q.when(CDUsersData()),
            $q.when(HospitalsData()),
            $q.when(ProvidersData()),
            $q.when(InsuranceCompaniesData()),
            $q.when(LoginUsersData()),
            $q.when(SubSectionData())
        ]).then(function (response) {
            $scope.Followups = [{ Name: "Phone Call", Type: "PhoneCall" }, { Name: "Email", Type: "Email" },
                                { Name: "Online Form Completion", Type: "OnlineFormCompletion" }, { Name: "Paper Form Completion", Type: "PaperFormCompletion" }];
            $scope.FollowupHelper = angular.copy($scope.Followups);
            TempFollowupHelper = angular.copy($scope.Followups);
            for (var i = 0; i < $scope.LoginUsers.length; i++) {
                for (var j = 0; j < $scope.CDUsers.length; j++) {
                    if ($scope.LoginUsers[i].Id == cduserdata.cdUser.AuthenicateUserId) {
                        $scope.ProviderNameforNotes = $scope.LoginUsers[i].UserName;
                    }
                    if ($scope.LoginUsers[i].Id == $scope.CDUsers[j].AuthenicateUserId) {
                        $scope.users.push({ CDUserID: $scope.CDUsers[j].CDUserID, AuthenicateUserId: $scope.CDUsers[j].AuthenicateUserId, FullName: $scope.LoginUsers[i].FullName, Email: $scope.LoginUsers[i].Email, UserName: $scope.LoginUsers[i].UserName })
                    }
                }
            }
            for (var i in $scope.Tasks) {
                if ($scope.Tasks[i].ModeOfFollowUp != "") {
                    $scope.Tasks[i].ModeOfFollowUp = JSON.parse($scope.Tasks[i].ModeOfFollowUp);
                    $scope.Tasks[i].Status = ($scope.Tasks[i].Status == "Active") ? "Open" : "Closed";
                }
                for (var j in $scope.LoginUsers) {
                    if ($scope.Tasks[i].AssignedTo.AuthenicateUserId == $scope.LoginUsers[j].Id)
                        $scope.Tasks[i].AssignedToName = $scope.LoginUsers[j].Email;
                }
                var converteddate = $scope.ConvertDate($scope.Tasks[i].NextFollowUpDate);
                $scope.Tasks[i].NextFollowUpDate = converteddate;
            }
            $rootScope.trackerItems1 = angular.copy($scope.Tasks);
            $scope.t = { sort: {}, search: {}, pagination: { start: 0 } }
            ctrl.callServer($scope.t);
            $rootScope.TasksLoaded = true;
        });
    }

    $scope.cancelEditViewforTab1 = function () {
        $('#addButton').show();
        $scope.errormessage = false;
        $scope.errormessageforAssignedto = false;
        $scope.errormessageforprovider = false;
        $scope.errormessageforsubsection = false;
        $scope.forHospitalName = false;
        $scope.errormessageforInsurancecompany = false;
        $scope.taskViewforTab1 = false;
        $scope.editViewforTab3 = false;
        $scope.editViewforTab2 = false;
        $scope.VisibilityControl = "";
        $scope.progressbar = false;
        $scope.Tempfollowup = [];
        $scope.task = angular.copy(currentTask);
        for (var i in $scope.DailyTasks) {
            if (currentTask.TaskTrackerId == $scope.DailyTasks[i].TaskTrackerId) {
                $scope.DailyTasks[i] = angular.copy(currentTask);
            }
        }
        $scope.Followup = angular.copy($scope.FollowupHelper);
        $scope.addView = false;
        $scope.TableView = true;
        $scope.progressbar = true;
    }


    this.callServer = function callServer(tableState) {
        ctrl.isLoading = true;
        var pagination = tableState.pagination;
        var start = pagination.start || 0;
        var number = pagination.number || 10;
        $scope.t = tableState;
        Resource.getPage(start, number, tableState).then(function (result) {
            ctrl.displayed = result.data;
            ctrl.temp = ctrl.displayed;
            tableState.pagination.numberOfPages = result.numberOfPages;
            ctrl.isLoading = false;
        });
    };
    $scope.SelectFollowup = function (Followup) {
        $scope.errormessage = false;
        $scope.Tempfollowup1 = "";

        $scope.Tempfollowup.push(Followup);
        $scope.Tempfollowup1 = JSON.stringify(angular.copy($scope.Tempfollowup));
        $scope.Followups = $.grep($scope.Followups, function (element) {
            return element.Name != Followup.Name;
        });
        //$scope.task.FollowUpDetail = $scope.Followups.filter(function (Followups) { return Followups.Name == Followup.Name })[0].Type;
        //$scope.task.FollowUp = Followup.Name
        $(".ProviderTypeSelectAutoList").hide();
    }
    $scope.RemoveFollowup = function (followup) {
        $scope.Tempfollowup1 = "";
        $scope.Followups.push(followup);
        $scope.Tempfollowup = $.grep($scope.Tempfollowup, function (element) {
            return element.Name != followup.Name;
        });
        $scope.Tempfollowup1 = JSON.stringify(angular.copy($scope.Tempfollowup));

    }
    $scope.searchCumDropDown = function (divId) {
        $(".ProviderTypeSelectAutoList").hide();
        $("#" + divId).show();
    };
    $scope.SelectInsurance = function (InsuranceCompany) {
        $scope.task.InsuaranceCompanyNameID = InsuranceCompany.InsuaranceCompanyNameID;
        $scope.task.InsuranceCompanyName = angular.copy(InsuranceCompany);
        $(".ProviderTypeSelectAutoList").hide();
    }
    $scope.SelectHospital = function (Hospital) {
        $scope.task.HospitalID = Hospital.HospitalID;
        $scope.task.Hospital = Hospital.HospitalName;
        $(".ProviderTypeSelectAutoList").hide();
    }
    $scope.SelectSubSection = function (SubSection) {
        $scope.task.SubSectionName = SubSection.SubSectionName;
        $(".ProviderTypeSelectAutoList").hide();
    }
    $scope.showeditView = function (task, editviewfortab) {
        $('#addButton').hide();
        $scope.errormessage = false;
        $scope.errormessageforAssignedto = false;
        $scope.errormessageforprovider = false;
        $scope.errormessageforsubsection = false;
        $scope.errormessageforHospitalName = false;
        $scope.errormessageforInsurancecompany = false;
        $scope.Followups = angular.copy(TempFollowupHelper);
        $scope.Tempfollowup = [];
        $scope.Tempfollowup1 = "";
        if (task.ModeOfFollowUp != "" && task.ModeOfFollowUp != null) {
            $scope.Tempfollowup = angular.copy(task.ModeOfFollowUp);
            $scope.Tempfollowup1 = JSON.stringify(angular.copy(task.ModeOfFollowUp));
            for (var i in $scope.Tempfollowup) {
                for (var j in $scope.Followups)
                    if ($scope.Tempfollowup[i].Name == $scope.Followups[j].Name) {
                        $scope.Followups.splice(j, 1);
                    }
            }
        }
        else {
            $scope.Followups = angular.copy($scope.FollowupHelper);
            $scope.Tempfollowup = [];
        }
        for (var i = 0; i < $scope.LoginUsers.length; i++) {
            if ($scope.LoginUsers[i].Id == task.AssignedTo.AuthenicateUserId) {
                task.AssignedTo.Email = $scope.LoginUsers[i].Email;
            }
        }
        for (var i = 0; i < $scope.Hospitals.length; i++) {
            if (task.HospitalID == $scope.Hospitals[i].HospitalID) {
                task.Hospital = $scope.Hospitals[i].HospitalName;
            }
        }

        for (var i = 0; i < $scope.InsuranceCompanies.length; i++) {
            if (task.InsuaranceCompanyNameID == $scope.InsuranceCompanies[i].InsuaranceCompanyNameID) {
                task.InsuranceCompanyName.CompanyName = $scope.InsuranceCompanies[i].CompanyName;
            }
        }

        $scope.TableView = false;
        $scope.VisibilityControl = editviewfortab;
        $scope.task = angular.copy(task);
        currentTask = angular.copy(task);
    }
    $scope.SelectUser = function (User) {
        $scope.task.AssignedToId = User.AuthenicateUserId;
        $scope.task.AssignedTo.Email = User.Email;
        $(".ProviderTypeSelectAutoList").hide();
    }
    $scope.QuickRemove = function (task) {
        if (angular.isObject(task)) {
            $scope.TemporaryTask = {};
            $scope.TemporaryTask = angular.copy(task);
        }
        $('#inactiveWarningModal1').modal();
    }

    $scope.removeTask = function () {
        $scope.progressbar = false;
        $http.post(rootDir + '/TaskTracker/Inactivetask?taskTrackerID=' + $scope.TemporaryTask.TaskTrackerId).
   success(function (data, status, headers, config) {
       if (data == "true") {
           messageAlertEngine.callAlertMessage("successfullySaved", "Task closed successfully", "success", true);
       }
       for (var j in $scope.Tasks) {
           if ($scope.Tasks[j].TaskTrackerId == $scope.TemporaryTask.TaskTrackerId) {
               $scope.Tasks[j].Status = "Closed";
           }
       }
       var updateddate = $scope.ConvertDate(data.NextFollowUpDate);

       for (var j in $scope.Tasks) {
           if ($scope.Tasks[j].TaskTrackerId == data.TaskTrackerId) {
               $scope.Tasks[j].Subject = data.Subject;
               $scope.Tasks[j].SubSectionName = data.SubSectionName;
               $scope.Tasks[j].NextFollowUpDate = updateddate;
               $scope.Tasks[j].Status = data.Status;
               $scope.Tasks[j].InsuranceCompanyName.CompanyName = updatedinsurancecompany;
               $scope.Tasks[j].ModeOfFollowUp = data.ModeOfFollowUp;
               $scope.Tasks[j].AssignedToName = assignedtoName;
           }

       }

       $rootScope.trackerItems1 = angular.copy($scope.Tasks);
       ctrl.temp = $scope.Tasks;


   }).
   error(function (data, status, headers, config) {
       messageAlertEngine.callAlertMessage("errorInitiated", "Please try after sometime !!!!", "danger", true);
   });
        $scope.progressbar = true;
    }

    $scope.Quickreopen = function (task) {
        if (angular.isObject(task)) {
            $scope.TemporaryTask = {};
            $scope.TemporaryTask = angular.copy(task);
        }
        $('#reactiveWarningModal2').modal();
    }

    $scope.reopenTask = function () {
        $scope.progressbar = false;
        var opened = [];
        $http.post(rootDir + '/TaskTracker/Reactivetask?taskTrackerID=' + $scope.TemporaryTask.TaskTrackerId).
           success(function (data, status, headers, config) {
               if (data == "true") {
                   messageAlertEngine.callAlertMessage("successfullySaved", "Task reopened successfully", "success", true);
               }

               for (var j in $scope.Tasks) {
                   if ($scope.Tasks[j].TaskTrackerId == $scope.TemporaryTask.TaskTrackerId) {
                       $scope.Tasks[j].Status = "Open";
                   }
               }

               var updateddate = $scope.ConvertDate(data.NextFollowUpDate);

               for (var j in $scope.Tasks) {
                   if ($scope.Tasks[j].TaskTrackerId == data.TaskTrackerId) {
                       $scope.Tasks[j].Subject = data.Subject;
                       $scope.Tasks[j].SubSectionName = data.SubSectionName;
                       $scope.Tasks[j].NextFollowUpDate = updateddate;
                       $scope.Tasks[j].Status = data.Status;
                       $scope.Tasks[j].InsuranceCompanyName.CompanyName = updatedinsurancecompany;
                       $scope.Tasks[j].ModeOfFollowUp = data.ModeOfFollowUp;
                       $scope.Tasks[j].AssignedToName = assignedtoName;
                   }

               }

           }).


           error(function (data, status, headers, config) {
               messageAlertEngine.callAlertMessage("errorInitiated", "Please try after sometime !!!!", "danger", true);
           });
        $scope.progressbar = true;
    }

    $scope.ConvertDate = function (date) {
        var followupdateforupdatetask = "";
        if (date !== null || date !== "") {
            var date5 = date.split('T');
            var date5 = date5[0].split('-');
            followupdateforupdatetask = date5[1] + "/" + date5[2] + "/" + date5[0];
        }
        return followupdateforupdatetask;
    }

    $scope.validatesubsectionName = function (subsectionname) {
        var subsectionnameresult = "";
        for (var p in $scope.SubSections) {
            if (subsectionname == $scope.SubSections[p].SubSectionName) {
                subsectionnameresult = $scope.SubSections[p].SubSectionName;
            }
        }
        return subsectionnameresult;
    }

    $scope.validateHospitalName = function (Hospitalname) {
        var Hospitalnameresult = "invalid";
        if (Hospitalname == null || $.trim(Hospitalname) == "") {
            Hospitalnameresult = "valid";
        }
        for (var p in $scope.Hospitals) {
            if (Hospitalname == $scope.Hospitals[p].HospitalName) {
                Hospitalnameresult = $scope.Hospitals[p].HospitalName;
            }
        }
        return Hospitalnameresult;
    }

    $scope.validateInsuranceCompany = function (Insurancename) {
        var Insurancecompanyresult = "invalid";
        if (Insurancename == null || $.trim(Insurancename) == "") {
            Insurancecompanyresult = "valid";
        }
        for (var p in $scope.InsuranceCompanies) {
            if (Insurancename == $scope.InsuranceCompanies[p].CompanyName) {
                Insurancecompanyresult = $scope.InsuranceCompanies[p].CompanyName
            }
        }
        return Insurancecompanyresult;
    }

    $scope.canceleditupdate = function () {
        $scope.errormessage = false;
        $scope.errormessageforAssignedto = false;
        $scope.errormessageforprovider = false;
        $scope.errormessageforsubsection = false;
        $scope.errormessageforHospitalName = false;
        $scope.errormessageforInsurancecompany = false;
        $scope.progressbar = false;
        $scope.addView = false;
        $scope.editView = false;
        $scope.VisibilityControl = "";
        $scope.editViewforTab1 = false;
        $scope.editViewforTab2 = false;
        $scope.editViewforTab3 = false;
        $scope.Tempfollowup = [];
        $scope.TableView = true;
        $scope.progressbar = true;
    }

    $scope.editTask = function () {
        var d1 = new $.Deferred();
        $scope.errormessage = false;
        $scope.progressbar = false;
        var validationStatus;
        var validationCount = 0;
        var url;
        var $formData;
        $formData = $('#newTaskFormDiv');
        url = rootDir + "/TaskTracker/UpdateTask"
        var validatemodeoffollowupforedit = $($formData[0]).find($("[name='ModeOfFollowUp']")).val();
        var validatesubsectionnameforedit = $($formData[0]).find($("[name='SubSectionName']")).val();
        var validatehospitalnameforedit = $($formData[0]).find($("[name='HospitalName']")).val();
        var validateinsurancecompanynameforedit = $($formData[0]).find($("[name='InsuranceCompany']")).val();
        var validateassignedtoforedit = $($formData[0]).find($("[name='AssignedTo']")).val();
        var subsectionresultforedit = $scope.validatesubsectionName(validatesubsectionnameforedit);
        if (subsectionresultforedit == "") {
            $scope.errormessageforsubsection = true;
            validationCount++;
        }
        var hospitalnameresultforedit = $scope.validateHospitalName(validatehospitalnameforedit);
        if (hospitalnameresultforedit == "invalid") {
            $scope.errormessageforHospitalName = true;
            validationCount++;
        }
        var insurancecompanyresultforedit = $scope.validateInsuranceCompany(validateinsurancecompanynameforedit)
        if (insurancecompanyresultforedit == "invalid") {
            $scope.errormessageforInsurancecompany = true;
            validationCount++;
        }

        ResetFormForValidation($formData);
        validationStatus = $formData.valid();
        if (validatemodeoffollowupforedit != "" && validatemodeoffollowupforedit != null && validatemodeoffollowupforedit != "[]") {
            if (validationStatus && validationCount == 0) {
                $scope.Tempfollowup1 = "";
                $.ajax({
                    url: url,
                    type: 'POST',
                    data: new FormData($formData[0]),
                    async: false,
                    cache: false,
                    contentType: false,
                    processData: false,
                    success: function (data) {
                        $('#addButton').show();
                        data = JSON.parse(data);
                        data.ModeOfFollowUp = JSON.parse(data.ModeOfFollowUp);
                        var tabID = "";
                        var sectionID = "";
                        var Followups = "";
                        $scope.Tempfollowup = data.ModeOfFollowUp;
                        for (var i in $scope.Followups) {
                            if ($scope.Followups[i].Type == data.ModeOfFollowUp) {
                                Followups = $scope.Followups[i].Name;
                            }
                        }
                        var taskassignedhospital = "";
                        for (var h in $scope.Hospitals) {
                            if (data.HospitalID == $scope.Hospitals[h].HospitalID) {
                                taskassignedhospital = $scope.Hospitals[h].HospitalName;
                            }
                        }
                        var updatedinsurancecompany = "";
                        for (var p in $scope.InsuranceCompanies) {
                            if (data.InsuaranceCompanyNameID == $scope.InsuranceCompanies[p].InsuaranceCompanyNameID) { updatedinsurancecompany = $scope.InsuranceCompanies[p].CompanyName; }
                        }
                        var Providername = "";
                        for (var i in $scope.Providers) {
                            if (data.ProfileID == $scope.Providers[i].ProfileId) {
                                Providername = $scope.Providers[i].Name;
                            }
                        }
                        var assignedtoName = "";
                        for (var i in $scope.users) {
                            if (data.AssignedToId == $scope.users[i].CDUserID) { assignedtoName = $scope.users[i].Email }
                        }
                        var updateddate = $scope.ConvertDate(data.NextFollowUpDate);
                        var newStatus = (data.Status == "Active") ? "Open" : "Closed";
                        for (var j in $scope.Tasks) {
                            if ($scope.Tasks[j].TaskTrackerId == data.TaskTrackerId) {
                                $scope.Tasks[j].Subject = data.Subject;
                                $scope.Tasks[j].SubSectionName = data.SubSectionName;
                                $scope.Tasks[j].NextFollowUpDate = updateddate;
                                $scope.Tasks[j].Status = newStatus;
                                $scope.Tasks[j].InsuranceCompanyName.CompanyName = updatedinsurancecompany;
                                $scope.Tasks[j].ModeOfFollowUp = data.ModeOfFollowUp;
                                $scope.Tasks[j].AssignedToName = assignedtoName;
                            }

                        }

                        $rootScope.trackerItems1 = angular.copy($scope.Tasks);
                        ctrl.temp = $scope.Tasks;
                        $scope.canceleditupdate();
                        messageAlertEngine.callAlertMessage("successfullySaved", "Task updated successfully", "success", true);
                        $scope.progressbar = true;
                        $scope.task = {};
                        document.getElementById("newTaskFormDiv").reset();
                        d1.resolve(data);
                    },
                    error: function (e) {
                        $scope.canceleditupdate();
                        messageAlertEngine.callAlertMessage("errorInitiated", "Please try after sometime !!!!", "danger", true);
                        $scope.progressbar = true;
                        document.getElementById("newTaskFormDiv").reset();

                    }
                });
            }
            return d1.promise();

            // $formData.reset();
        }

        else {
            $scope.errormessage = true;
        }
    }

    function ProvidersData() {
        var defer = $q.defer();
        $http.get(rootDir + '/TaskTracker/GetAllProviders').
            success(function (data, status, headers, config) {
                $scope.Providers = data;
                defer.resolve(data);
            }).
            error(function (data, status, headers, config) {
                defer.reject();
            });
        return defer.promise;
    }

    function SubSectionData() {
        var defer = $q.defer();
        $http.get(rootDir + "/MasterDataNew/GetAllProfileSubSections").
            success(function (data, status, headers, config) {
                $scope.SubSections = data;
                defer.resolve(data);
            }).
            error(function (data, status, headers, config) {
                defer.reject();
            });
        return defer.promise;
    }

    $scope.TasksData = function () {
        $http.get(rootDir + '/TaskTracker/GetAllUsers').
           success(function (data, status, headers, config) {
               $scope.LoginUsers = data;
           }).
           error(function (data, status, headers, config) {
           });
    }

    $scope.showDetailViewforTab1 = function (task) {
        $scope.TableView = false;
        $scope.detailViewfortab = true;
        $scope.taskView = angular.copy(task);
    }

    $scope.cancelViewforTab1 = function () {
        $scope.detailViewfortab = false;
        $scope.TableView = true;
    }
}])

