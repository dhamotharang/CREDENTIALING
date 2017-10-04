var rootDir = "";
trackerApp.controller('TrackerCtrl', function ($scope, $rootScope, $filter, Helpers, Resource, DOMManager) {
    var ctrl = this;
    this.displayed = [];
    $rootScope.HistoryTasks = [];
    $scope.Tempfollowup = [];
    $scope.CurrentTask = {};
    $scope.TableState = {};
    $rootScope.PreviousTab = "MyDailyTasks";
    $scope.IsAdmin = isAdmin;
    $scope.DOMManager = DOMManager; //Calls made to Service from HTML directly rather than to controller and then to service.
    $rootScope.Tasks = DOMManager.TabChanged($rootScope.PreviousTab, Tasks);
    DOMManager.ViewHandler('Table');
    $scope.Followups = [{ Name: "Phone Call", Type: "PhoneCall" }, { Name: "Email", Type: "Email" }, { Name: "Online Form Completion", Type: "OnlineFormCompletion" }, { Name: "Paper Form Completion", Type: "PaperFormCompletion" }];
    $scope.GetAllMasterData = function () {
        Helpers.Get("/Tasks/GetAllMasterDataForTaskTrackerAsync").then(function (response) {
            $scope.NotesTemplates = response.NotesTemplates;
            $scope.Users = response.Users;
            $scope.Hospitals = response.Hospitals;
            $scope.PlanNames = response.Plans;
            $scope.Providers = response.Providers;
            $scope.SubSections = response.ProfileSubSections;
            ctrl.IsTableLoading = false;
        });
    };
    this.callServer = function callServer(tableState) {
        ctrl.IsTableLoading = true;
        var pagination = tableState.pagination ? tableState.pagination : null;
        var start = pagination.start || 0;
        var number = pagination.number || 5;
        $scope.TableState = tableState;
        Resource.getPage(start, number, tableState).then(function (result) {
            ctrl.displayed = result.data;
            tableState.pagination.numberOfPages = result.numberOfPages;//set the number of pages so the pagination can update
            ctrl.IsTableLoading = false;
        });
    };
    $scope.TabFacade = function (name) {
        $rootScope.PreviousTab = name == 'HistoryTasks' ? $rootScope.PreviousTab : name;
        DOMManager.ViewHandler('Table');
        ctrl.IsTableLoading = true;
        $rootScope.Tasks = DOMManager.TabChanged(name, Tasks);
        ctrl.callServer(Helpers.resetTableState($scope.TableState));
    };
    $scope.ViewTaskById = function (Id) {
        DOMManager.ViewHandler('View');
        ctrl.IsTableLoading = true;
        Helpers.Get("/Tasks/GetTaskInfoById?taskId=" + Id).then(function (response) {
            $scope.CurrentTask = response;
            $scope.CurrentTask.ModeOfFollowUp = JSON.parse($scope.CurrentTask.ModeOfFollowUp);
            ctrl.IsTableLoading = false;
        });
    };
    $scope.ViewAllHistoriesOfATask = function (Id) {
        ctrl.IsTableLoading = true;
        $rootScope.DangerStyle = false;
        $scope.ControlHistoryTab(true);
        Helpers.Get("/Tasks/GetAllHistoriesForATask?taskId=" + Id).then(function (response) {
            if (!$rootScope.DangerStyle) {
                $rootScope.Tasks = response;
                for (var i = 0; i < $rootScope.Tasks.length; i++) {
                    $rootScope.Tasks[i].LastModifiedDate = Helpers.ConvertDate($rootScope.Tasks[i].LastModifiedDate);
                    $rootScope.Tasks[i].ModeOfFollowUp = JSON.parse($rootScope.Tasks[i].FollowUps);
                }
                $rootScope.HistoryTasks = angular.copy($rootScope.Tasks);
                ctrl.callServer(Helpers.resetTableState($scope.TableState));
            }
        });
    };
    $scope.ControlHistoryTab = function (showHistory) {
        if (showHistory) {
            $scope.showHistoryTab = true;
            DOMManager.ModifyTabConfiguration('HistoryTasks');
            angular.element('.nav-tabs a:last').tab('show');
        }
        else {
            $scope.showHistoryTab = false;
            angular.element('.nav-tabs li').removeClass('active');
            $scope.TabFacade($rootScope.PreviousTab);
        }
    };
    $scope.AddSelectedValue = function (propName, propValue, errorClassName) {
        for (var i = 0; i < propName.length; i++) {
            $scope.CurrentTask[propName[i]] = propValue[i];
        }
        angular.element(".ProviderTypeSelectAutoList").hide();
        angular.element("." + errorClassName).hide();
    };
    $scope.OpenAddScreen = function () {
        $scope.TempNotes = Helpers.GetNotesSignatureForAdd();
        DOMManager.ViewHandler('Add');
        $scope.CurrentTask = {};
        if ($scope.Users === undefined) {
            ctrl.IsTableLoading = true;
        }
    };
    $scope.ManageFollowUps = function (obj, isRemove) {
        $scope.StringifiedFollowUps = "";
        if (!isRemove) {
            $scope.Tempfollowup.push(obj);
            $scope.StringifiedFollowUps = JSON.stringify(angular.copy($scope.Tempfollowup));
            $scope.Followups = $.grep($scope.Followups, function (element) { return element.Name != obj.Name; });
            $scope.AddSelectedValue([], [], 'FollowUpModeError');
        }
        else {
            $scope.Followups.push(obj);
            $scope.Tempfollowup = $.grep($scope.Tempfollowup, function (element) { return element.Name != obj.Name; });
            $scope.StringifiedFollowUps = JSON.stringify(angular.copy($scope.Tempfollowup));
        }
    };
    $scope.searchCumDropDown = function (divId) {
        angular.element(".ProviderTypeSelectAutoList").hide();
        angular.element("#" + divId).show();
    };
    angular.element('#modeofflwup').datepicker({
        autoclose: true,
        todayHighlight: true,
        orientation: 'auto',
        format: "mm/dd/yyyy",
        startDate: new Date()
    });

    $scope.showHistory = function (task) {
        $rootScope.tab = $rootScope.tabNames;
        $rootScope.tabNames = "History";
        $scope.selTask = [];
        for (var t = 0; t < $scope.selTaskIDs.length; t++) {
            if ($scope.selTaskIDs[t].SelectStatus != null && $scope.selTaskIDs[t].SelectStatus != undefined)
                $scope.selTaskIDs[t].SelectStatus = false;
        }
        $rootScope.previousTabName = $rootScope.tab;
        $scope.TaskHistory = [];
        $('.nav-tabs a:last').tab('show');
        $scope.history = true;
        var tabID = [];
        var sectionID = [];
        var histories = [];
        $scope.historyMsg = false;
        if ($scope.TaskHistory.length == 0) {
            $scope.historyMsg = true;
        }

        $rootScope.trackerItems = ($scope.TaskHistory).reverse();
        $rootScope.$broadcast('History');
    }
    $scope.validateproviderName = function (providername) {
        var providernameresult = "";
        for (var p in $scope.Providers) {
            if (providername == $scope.Providers[p].Name) {
                providernameresult = $scope.Providers[p].Name;
            }
        }
        return providernameresult;
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
            if (Insurancename == $scope.InsuranceCompanies[p].PlanName) {
                Insurancecompanyresult = $scope.InsuranceCompanies[p].PlanName
            }
        }
        return Insurancecompanyresult;
    }
    $scope.validateAssignedTo = function (assignedto) {
        var assignedtoresult = "";
        for (var p in $scope.Users) {
            if (assignedto == $scope.Users[p].UserName) {
                assignedtoresult = $scope.Users[p].UserName;
            }
        }
        return assignedtoresult;
    }
    $scope.addNewTask = function (task) {
        $scope.notetask = false;
        $scope.errormessage = false;
        $scope.errormessageforAssignedto = false;
        $scope.errormessageforprovider = false;
        $scope.errormessageforsubsection = false;
        $scope.errormessageforHospitalName = false;
        $scope.errormessageforInsurancecompany = false;
        $scope.progressbar = false;
        var validationCount = 0;
        var validationStatus;
        var url;
        var $formData;
        $formData = $('#newTaskFormDiv');
        var validatemodeoffollowup = $($formData[0]).find($("[name='ModeOfFollowUp']")).val();
        var validateprovidername = $($formData[0]).find($("[name='ProviderName']")).val();
        var validatesubsectionname = $($formData[0]).find($("[name='SubSectionName']")).val();
        var validatehospitalname = $($formData[0]).find($("[name='HospitalName']")).val();
        var validateinsurancecompanyname = $($formData[0]).find($("[name='InsuranceCompany']")).val();
        var validateassignedto = $($formData[0]).find($("[name='AssignedTo']")).val();
        var assignedtoresult = $scope.validateAssignedTo(validateassignedto);
        if (assignedtoresult == "") {
            $scope.errormessageforAssignedto = true;
            validationCount++;
        }
        var providernameresult = $scope.validateproviderName(validateprovidername);
        if (providernameresult == "") {
            $scope.errormessageforprovider = true;
            validationCount++;
        }
        subsectionresult = $scope.validatesubsectionName(validatesubsectionname);
        if (subsectionresult == "") {
            $scope.errormessageforsubsection = true;
            validationCount++;
        }
        var hospitalnameresult = $scope.validateHospitalName(validatehospitalname);
        if (hospitalnameresult == "invalid") {
            $scope.errormessageforHospitalName = true;
            validationCount++;
        }
        var insurancecompanyresult = $scope.validateInsuranceCompany(validateinsurancecompanyname)
        if (insurancecompanyresult == "invalid") {
            $scope.errormessageforInsurancecompany = true;
            validationCount++;
        }

        url = rootDir + "/TaskTracker/AddTask"
        ResetFormForValidation($formData);
        validationStatus = $formData.valid();
        if (validatemodeoffollowup != "" && validatemodeoffollowup != null && validatemodeoffollowup != "[]") {
            if (validationStatus && validationCount == 0) {
                $scope.Tempfollowup1 = "";
                //Simple POST request example (passing data) :
                $.ajax({
                    url: url,
                    type: 'POST',
                    data: new FormData($formData[0]),
                    async: false,
                    cache: false,
                    contentType: false,
                    processData: false,
                    success: function (data) {
                        data = JSON.parse(data);
                        data.daysleft = $filter("TimeDiff")(data.NextFollowUpDate);
                        data.ModeOfFollowUp = JSON.parse(data.ModeOfFollowUp);
                        //data.InsuaranceCompanyNames = JSON.parse(data.InsuaranceCompanyNames);
                        var tabID = "";
                        var sectionID = "";
                        var Followups = "";
                        for (var i in $scope.SubSections) {
                            if ($scope.SubSections[i].SubSectionName == data.SubSectionName) {
                                //tabID = $scope.SubSections[i].TabName;
                                //sectionID = $scope.SubSections[i].SubSectionID;
                            }
                        }
                        for (var i in $scope.Followups) {
                            if ($scope.Followups[i].Type == data.ModeOfFollowUp) {
                                Followups = $scope.Followups[i].Name;
                            }
                        }
                        var followupdateforaddtask = $scope.ConvertDate(data.NextFollowUpDate);

                        var Hospitalforaddtask = "";
                        for (var h in $scope.Hospitals) {
                            if (data.HospitalID == $scope.Hospitals[h].HospitalID) {
                                Hospitalforaddtask = $scope.Hospitals[h].HospitalName;
                            }
                        }
                        var Insurancecompanyforaddtask = [];
                        if (data.InsuaranceCompanyNameID != null) {
                            for (var i in $scope.InsuranceCompanies) {
                                if (data.InsuaranceCompanyNameID == $scope.InsuranceCompanies[i].InsuaranceCompanyNameID) {
                                    Insurancecompanyforaddtask = $scope.InsuranceCompanies[i].CompanyName;
                                }
                            }
                        }
                        else {
                            Insurancecompanyforaddtask = "";
                        }
                        var PlanNameforaddtask = [];
                        if (data.PlanID != null) {
                            for (var i in $scope.PlanNames) {
                                if (data.PlanID == $scope.PlanNames[i].PlanID) {
                                    PlanNameforaddtask = $scope.PlanNames[i].PlanName;
                                }
                            }
                        }
                        else {
                            PlanNameforaddtask = "";
                        }
                        var AssignedTo = "";
                        var Assignedtoid = "";
                        for (var i in $scope.Users) {
                            if ($scope.Users[i].CDUserID == data.AssignedToId) {
                                AssignedTo = $scope.Users[i].UserName;
                                Assignedtoid = $scope.Users[i].AuthenicateUserId;
                            }
                        }
                        var Providername = "";
                        for (var i in $scope.Providers) {
                            if (data.ProfileID == $scope.Providers[i].ProfileId) {
                                Providername = $scope.Providers[i].Name;
                            }
                        }
                        //var AssignedTo = $scope.Users.filter(function (users) { return users.CDUserID == $scope.AllTasks[k].AssignedToId })[0].UserName;
                        if (data.AssignedToId != currentcduserdata.cdUser.CDUserID) {
                            var DailyTasksforaddedtask = {
                                TaskTrackerId: data.TaskTrackerId, ProfileID: data.ProfileID,
                                //ProviderName: $scope.Providers.filter(function (Providers) { return Providers.ProfileId == data.ProfileID })[0].Name, SubSectionName: data.SubSectionName,
                                ProviderName: Providername, SubSectionName: data.SubSectionName,
                                //Subject: data.Subject, NextFollowUpDate: data.NextFollowUpDate = null ? "" : data.NextFollowUpDate,
                                Subject: data.Subject, NextFollowUpDate: followupdateforaddtask,
                                ModeOfFollowUp: data.ModeOfFollowUp, FollowUp: Followups,
                                //InsuranceCompanyName: Insurancecompanyforaddtask,
                                InsuranceCompanyName: Insurancecompanyforaddtask,
                                PlanName: PlanNameforaddtask != "" ? PlanNameforaddtask : "Not Available",
                                AssignedById: data.AssignedById,
                                //AssignedToId: $scope.Users.filter(function (users) { return users.CDUserID == data.AssignedToId })[0].AuthenicateUserId, AssignedTo: $scope.Users.filter(function (users) { return users.CDUserID == data.AssignedToId })[0].UserName, HospitalID: data.HospitalID,
                                AssignedToId: Assignedtoid, AssignedTo: AssignedTo, HospitalID: data.HospitalID,
                                Hospital: Hospitalforaddtask, Notes: data.Notes, ModifiedDate: data.LastModifiedDate,
                                TabID: tabID,
                                TaskTrackerHistories: data.TaskTrackerHistories,
                                SubSectionID: sectionID,
                                CompleteStatus: "OPEN",
                                LastUpdatedBy: data.LastUpdatedBy,
                                daysleft: data.daysleft
                            };
                            $scope.RemainingTasks.splice(0, 0, DailyTasksforaddedtask);
                            $rootScope.$broadcast("event", $scope.RemainingTasks);
                        }
                        if (data.AssignedToId == currentcduserdata.cdUser.CDUserID) {
                            var TasksAssignedforaddedtask = {
                                TaskTrackerId: data.TaskTrackerId, ProfileID: data.ProfileID,
                                //ProviderName: $scope.Providers.filter(function (Providers) { return Providers.ProfileId == data.ProfileID })[0].Name, SubSectionName: data.SubSectionName,
                                ProviderName: Providername, SubSectionName: data.SubSectionName,
                                //Subject: data.Subject, NextFollowUpDate: data.NextFollowUpDate = null ? "" : data.NextFollowUpDate,
                                Subject: data.Subject, NextFollowUpDate: followupdateforaddtask,
                                ModeOfFollowUp: data.ModeOfFollowUp, FollowUp: Followups,
                                InsuranceCompanyName: Insurancecompanyforaddtask,
                                PlanName: PlanNameforaddtask != "" ? PlanNameforaddtask : "Not Available",
                                AssignedById: data.AssignedById,
                                //AssignedToId: $scope.Users.filter(function (users) { return users.CDUserID == data.AssignedToId })[0].AuthenicateUserId, AssignedTo: $scope.Users.filter(function (users) { return users.CDUserID == data.AssignedToId })[0].UserName, HospitalID: data.HospitalID,
                                AssignedToId: Assignedtoid, AssignedTo: AssignedTo, HospitalID: data.HospitalID,
                                //Hospital: data.HospitalID == null ? "" : $scope.Hospitals.filter(function (Hospitals) { return Hospitals.HospitalID == data.HospitalID })[0].HospitalName, Notes: data.Notes, ModifiedDate: data.LastModifiedDate,
                                Hospital: Hospitalforaddtask, Notes: data.Notes, ModifiedDate: data.LastModifiedDate,
                                TabID: tabID,
                                TaskTrackerHistories: data.TaskTrackerHistories,
                                SubSectionID: sectionID,
                                CompleteStatus: "OPEN",
                                LastUpdatedBy: data.LastUpdatedBy,
                                daysleft: data.daysleft
                            };
                            $scope.TasksAssigned.splice(0, 0, TasksAssignedforaddedtask);
                            $rootScope.$broadcast("evnt", $scope.TasksAssigned);
                            $scope.RemainingTasks.splice(0, 0, TasksAssignedforaddedtask);
                            $rootScope.$broadcast("event", $scope.RemainingTasks);
                            //$("#numberOfNotifications").text(parseInt($("#numberOfNotifications")[0].textContent) + 1);
                        }


                        $scope.cancelAdd();
                        //var cnd = $.connection.cnDHub;
                        //cnd.server.notificationMessage();
                        messageAlertEngine.callAlertMessage("successfullySaved", "Task successfully assigned", "success", true);
                        $scope.progressbar = true;
                        document.getElementById("newTaskFormDiv").reset();
                    },
                    error: function (e) {
                        $scope.cancelAdd();
                        messageAlertEngine.callAlertMessage("errorInitiated", "Please try after sometime !!!!", "danger", true);
                        $scope.progressbar = true;
                        document.getElementById("newTaskFormDiv").reset();
                    }
                });
            }

        }
        else {
            $scope.errormessage = true;
        }
        //$scope.validationfunction = false;
    }
    $scope.editTask = function () {
        $scope.CancelReminder();//for closing reminder data
        $scope.flag = true;
        var d1 = new $.Deferred();
        $scope.errormessage = false;
        $scope.errormessageforAssignedto = false;
        $scope.errormessageforprovider = false;
        $scope.errormessageforsubsection = false;
        $scope.errormessageforHospitalName = false;
        $scope.errormessageforInsurancecompany = false;
        $scope.progressbar = false;
        var validationStatus;
        var validationCount = 0;
        var url;
        var $formData;
        $formData = $('#newTaskFormDiv');
        url = rootDir + "/TaskTracker/UpdateTask"
        var validatemodeoffollowupforedit = $($formData[0]).find($("[name='ModeOfFollowUp']")).val();
        var validateprovidernameforedit = $($formData[0]).find($("[name='ProviderName']")).val();
        var validatesubsectionnameforedit = $($formData[0]).find($("[name='SubSectionName']")).val();
        var validatehospitalnameforedit = $($formData[0]).find($("[name='HospitalName']")).val();
        var validateinsurancecompanynameforedit = $($formData[0]).find($("[name='InsuranceCompany']")).val();

        var validateassignedtoforedit = $($formData[0]).find($("[name='AssignedTo']")).val();
        var assignedtoresultforedit = $scope.validateAssignedTo(validateassignedtoforedit);
        if (assignedtoresultforedit == "") {
            $scope.errormessageforAssignedto = true;
            validationCount++;
        }
        var providernameresultforedit = $scope.validateproviderName(validateprovidernameforedit);
        if (providernameresultforedit == "") {
            $scope.errormessageforprovider = true;
            validationCount++;
        }
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
                        data.daysleft = $filter("TimeDiff")($scope.task.NextFollowUpDate);
                        data.ModeOfFollowUp = JSON.parse(data.ModeOfFollowUp);
                        var editedTask = [];
                        var tabID = "";
                        var sectionID = "";
                        var Followups = "";
                        $scope.Tempfollowup = data.ModeOfFollowUp;
                        for (var i in $scope.SubSections) {
                            if ($scope.SubSections[i].SubSectionName == data.SubSectionName) {
                            }
                        }
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
                        var AssignedTo = "";
                        var Assignedtoid = "";
                        for (var i in $scope.Users) {
                            if ($scope.Users[i].CDUserID == data.AssignedToId) {
                                AssignedTo = $scope.Users[i].UserName;
                                Assignedtoid = $scope.Users[i].AuthenicateUserId;
                            }
                        }
                        var Providername = "";
                        for (var i in $scope.Providers) {
                            if (data.ProfileID == $scope.Providers[i].ProfileId) {
                                Providername = $scope.Providers[i].Name;
                            }
                        }
                        var TasksAssignedplanname = "";
                        for (var p in $scope.PlanNames) {
                            if (data.PlanID == $scope.PlanNames[p].PlanID) {
                                TasksAssignedplanname = $scope.PlanNames[p].PlanName;
                            }
                        }
                        if ($rootScope.tabNames == 'DailyTask') {
                            $scope.TasksAssigned[$scope.TasksAssigned.indexOf($scope.TasksAssigned.filter(function (updates) { return updates.TaskTrackerId == data.TaskTrackerId })[0])].TaskTrackerHistories = (data.TaskTrackerHistories);
                        }
                        else if ($rootScope.tabNames == 'AllTask') {
                            $scope.RemainingTasks[$scope.RemainingTasks.indexOf($scope.RemainingTasks.filter(function (updates) { return updates.TaskTrackerId == data.TaskTrackerId })[0])].TaskTrackerHistories = (data.TaskTrackerHistories);
                        }
                        var updateddate = $scope.ConvertDate(data.NextFollowUpDate);
                        if (($scope.VisibilityControl == 'editViewforTab') && ($rootScope.tabNames == 'DailyTask')) {
                            if (data.AssignedToId == currentcduserdata.cdUser.CDUserID) {
                                $scope.TasksAssigned.splice($scope.TasksAssigned.indexOf($scope.TasksAssigned.filter(function (datas) { return datas.TaskTrackerId == data.TaskTrackerId })[0]), 1);
                                var TasksAssignedforedittask = {
                                    TaskTrackerId: data.TaskTrackerId, ProfileID: data.ProfileID,
                                    ProviderName: Providername, SubSectionName: data.SubSectionName,
                                    Subject: data.Subject, NextFollowUpDate: updateddate,
                                    ModeOfFollowUp: data.ModeOfFollowUp, FollowUp: Followups,
                                    InsuranceCompanyName: updatedinsurancecompany,
                                    PlanName: TasksAssignedplanname != "" ? TasksAssignedplanname : "Not Available",
                                    AssignedToId: Assignedtoid, AssignedTo: AssignedTo, HospitalID: data.HospitalID,
                                    AssignedById: data.AssignedById,
                                    Hospital: taskassignedhospital, Notes: data.Notes, ModifiedDate: data.LastModifiedDate,
                                    TaskTrackerHistories: data.TaskTrackerHistories,
                                    TabID: tabID,
                                    SubSectionID: sectionID,
                                    CompleteStatus: "Open",
                                    LastUpdatedBy: data.LastUpdatedBy,
                                    daysleft: data.daysleft
                                };
                                editedTask = angular.copy(TasksAssignedforedittask);
                                $scope.TasksAssigned.splice(0, 0, TasksAssignedforedittask);
                                $rootScope.$broadcast("evnt", $scope.TasksAssigned);
                                var task = $scope.RemainingTasks.indexOf($scope.RemainingTasks.filter(function (datas) { return datas.TaskTrackerId == data.TaskTrackerId })[0]);
                                $scope.RemainingTasks.splice(task, 1);
                                $scope.RemainingTasks.splice(0, 0, TasksAssignedforedittask);
                                $rootScope.$broadcast("event", $scope.RemainingTasks);
                            }
                            else {
                                $scope.TasksAssigned.splice($scope.TasksAssigned.indexOf($scope.TasksAssigned.filter(function (datas) { return datas.TaskTrackerId == data.TaskTrackerId })[0]), 1);
                                $rootScope.$broadcast("evnt", $scope.TasksAssigned);
                                $rootScope.tabNames = 'AllTask';
                                $rootScope.deepu = true;
                            }

                            if (data.AssignedToId != currentcduserdata.cdUser.CDUserID) {
                                var DailyTasksforedittask = {
                                    TaskTrackerId: data.TaskTrackerId, ProfileID: data.ProfileID,
                                    ProviderName: Providername, SubSectionName: data.SubSectionName,
                                    Subject: data.Subject, NextFollowUpDate: updateddate,
                                    ModeOfFollowUp: data.ModeOfFollowUp, FollowUp: Followups,
                                    InsuranceCompanyName: updatedinsurancecompany,
                                    PlanName: TasksAssignedplanname != "" ? TasksAssignedplanname : "Not Available",
                                    AssignedToId: Assignedtoid, AssignedTo: AssignedTo, HospitalID: data.HospitalID,
                                    AssignedById: data.AssignedById,
                                    Hospital: taskassignedhospital, Notes: data.Notes, ModifiedDate: data.LastModifiedDate,
                                    TaskTrackerHistories: data.TaskTrackerHistories,
                                    TabID: tabID,
                                    SubSectionID: sectionID,
                                    CompleteStatus: "Open",
                                    LastUpdatedBy: data.LastUpdatedBy,
                                    daysleft: data.daysleft
                                };
                                editedTask = angular.copy(DailyTasksforedittask);
                                var task = $scope.RemainingTasks.indexOf($scope.RemainingTasks.filter(function (datas) { return datas.TaskTrackerId == data.TaskTrackerId })[0]);
                                $scope.RemainingTasks.splice(task, 1);
                                $scope.RemainingTasks.splice(0, 0, DailyTasksforedittask);
                                $rootScope.$broadcast("event", $scope.RemainingTasks);
                            }
                        }
                        else if (($scope.VisibilityControl == "editViewforTab") && ($rootScope.tabNames == 'AllTask')) {
                            if (data.AssignedToId == currentcduserdata.cdUser.CDUserID) {
                                var TasksAssignedforedittask = {
                                    TaskTrackerId: data.TaskTrackerId, ProfileID: data.ProfileID,
                                    ProviderName: Providername, SubSectionName: data.SubSectionName,
                                    Subject: data.Subject, NextFollowUpDate: updateddate,
                                    ModeOfFollowUp: data.ModeOfFollowUp, FollowUp: Followups,
                                    InsuranceCompanyName: updatedinsurancecompany,
                                    PlanName: TasksAssignedplanname != "" ? TasksAssignedplanname : "Not Available",
                                    AssignedToId: Assignedtoid, AssignedTo: AssignedTo, HospitalID: data.HospitalID,
                                    AssignedById: data.AssignedById,
                                    Hospital: taskassignedhospital, Notes: data.Notes, ModifiedDate: data.LastModifiedDate,
                                    TabID: tabID,
                                    SubSectionID: sectionID,
                                    CompleteStatus: "Open",
                                    LastUpdatedBy: data.LastUpdatedBy,
                                    daysleft: data.daysleft,
                                    TaskTrackerHistories: data.TaskTrackerHistories,
                                };
                                editedTask = angular.copy(TasksAssignedforedittask);
                                var task = $scope.TasksAssigned.indexOf($scope.TasksAssigned.filter(function (datas) { return datas.TaskTrackerId == data.TaskTrackerId })[0]);
                                if (task != null && TasksAssignedforedittask.AssignedTo == currentcduserdata.cdUser.EmailId) {
                                    $scope.TasksAssigned.splice(task, 1);
                                }
                                $scope.TasksAssigned.splice(0, 0, TasksAssignedforedittask);
                                $rootScope.$broadcast("evnt", $scope.TasksAssigned);
                                var remtask = $scope.RemainingTasks.indexOf($scope.RemainingTasks.filter(function (datas) { return datas.TaskTrackerId == data.TaskTrackerId })[0]);
                                if (remtask != null && TasksAssignedforedittask.AssignedTo == currentcduserdata.cdUser.EmailId) {
                                    $scope.RemainingTasks.splice(remtask, 1);
                                }
                                $scope.RemainingTasks.splice(0, 0, TasksAssignedforedittask);
                                $rootScope.$broadcast("event", $scope.RemainingTasks);
                            }
                            if (data.AssignedToId != currentcduserdata.cdUser.CDUserID) {
                                var AllTasksforedittask = {
                                    TaskTrackerId: data.TaskTrackerId, ProfileID: data.ProfileID,
                                    ProviderName: Providername, SubSectionName: data.SubSectionName,
                                    Subject: data.Subject, NextFollowUpDate: updateddate,
                                    ModeOfFollowUp: data.ModeOfFollowUp, FollowUp: Followups,
                                    InsuranceCompanyName: updatedinsurancecompany,
                                    PlanName: TasksAssignedplanname != "" ? TasksAssignedplanname : "Not Available",
                                    AssignedToId: Assignedtoid, AssignedTo: AssignedTo, HospitalID: data.HospitalID,
                                    AssignedById: data.AssignedById,
                                    Hospital: taskassignedhospital, Notes: data.Notes, ModifiedDate: data.LastModifiedDate,
                                    TaskTrackerHistories: data.TaskTrackerHistories,
                                    TabID: tabID,
                                    SubSectionID: sectionID,
                                    CompleteStatus: "Open",
                                    LastUpdatedBy: data.LastUpdatedBy,
                                    daysleft: data.daysleft
                                };
                                var task = $scope.TasksAssigned.indexOf($scope.TasksAssigned.filter(function (datas) { return datas.TaskTrackerId == data.TaskTrackerId })[0]);;
                                if (task != null && task != -1 && AllTasksforedittask.AssignedTo != currentcduserdata.cdUser.EmailId) {
                                    $scope.TasksAssigned.splice(task, 1);
                                }
                                var remtask = $scope.RemainingTasks.indexOf($scope.RemainingTasks.filter(function (datas) { return datas.TaskTrackerId == data.TaskTrackerId })[0]);;
                                editedTask = angular.copy(AllTasksforedittask);
                                $scope.RemainingTasks.splice(remtask, 1);
                                $scope.RemainingTasks.splice(0, 0, AllTasksforedittask);
                                ctrl.temp = $scope.RemainingTasks;
                            }
                            $rootScope.$broadcast('AllTasks');
                        }
                        $scope.canceleditupdate();
                        messageAlertEngine.callAlertMessage("successfullySaved", "Task updated successfully", "success", true);
                        $scope.progressbar = true;
                        $scope.showDetailViewforTab1(editedTask);
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
            else {
                $scope.flag = false;
                $scope.progressbar = true;
            }
            return d1.promise();
            ctrl.callServer($scope.t);
        }
        else {
            $scope.errormessage = true;
        }
    }
    $scope.removeTask = function () {
        var TaskData = JSON.parse(localStorage.getItem("TaskReminders"));
        for (var task = 0; task < TaskData.length; task++) {
            var remainderstring = "TaskTrackerId" + ":" + $scope.TemporaryTask.TaskTrackerId;
            if ((TaskData[task].ReminderInfo).includes($scope.TemporaryTask.TaskTrackerId)) {
                $scope.remainderid = TaskData[task].TaskReminderID;
            }
        }
        DismissSingleTaskforremoveTask($scope.remainderid);
        $scope.progressbar = false;
        var closed = [];
        $http.post(rootDir + '/TaskTracker/Inactivetask?taskTrackerID=' + $scope.TemporaryTask.TaskTrackerId).
            success(function (data, status, headers, config) {
                if (data == "true") {
                    messageAlertEngine.callAlertMessage("successfullySaved", "Task closed successfully", "success", true);
                }
                for (var j in $scope.TasksAssigned) {
                    if ($scope.TasksAssigned[j].TaskTrackerId == $scope.TemporaryTask.TaskTrackerId) {
                        $scope.TasksAssigned[j].CompleteStatus = "CLOSED";
                        if ($rootScope.tabNames != 'DailyTask') {
                            $scope.TasksAssigned.splice(j, 1);
                        }
                    }
                }
                if ($rootScope.tabNames == 'DailyTask') {
                    if ($scope.TasksAssigned.filter(function (tasks) { return tasks.TaskTrackerId == $scope.TemporaryTask.TaskTrackerId })[0].SelectStatus != undefined)
                        $scope.TasksAssigned.filter(function (tasks) { return tasks.TaskTrackerId == $scope.TemporaryTask.TaskTrackerId })[0].SelectStatus = false;
                    closed = $scope.TasksAssigned.splice($scope.TasksAssigned.indexOf($scope.TasksAssigned.filter(function (tasks) { return tasks.TaskTrackerId == $scope.TemporaryTask.TaskTrackerId })[0]), 1);
                    var tempclosed = $scope.RemainingTasks.splice($scope.RemainingTasks.indexOf($scope.RemainingTasks.filter(function (tasks) { return tasks.TaskTrackerId == $scope.TemporaryTask.TaskTrackerId })[0]), 1);
                    $scope.ClosedTasks.push(closed[0]);
                }
                for (var i in $scope.RemainingTasks) {
                    if ($scope.RemainingTasks[i].TaskTrackerId == $scope.TemporaryTask.TaskTrackerId) {
                        $scope.RemainingTasks[i].CompleteStatus = "CLOSED";
                    }
                }
                if ($rootScope.tabNames == 'DailyTask') {
                    $rootScope.$broadcast('DailyTasks');
                }
                else if ($rootScope.tabNames == 'AllTask') {
                    if ($scope.RemainingTasks.filter(function (tasks) { return tasks.TaskTrackerId == $scope.TemporaryTask.TaskTrackerId })[0].SelectStatus != undefined)
                        $scope.RemainingTasks.filter(function (tasks) { return tasks.TaskTrackerId == $scope.TemporaryTask.TaskTrackerId })[0].SelectStatus = false;
                    closed = $scope.RemainingTasks.splice($scope.RemainingTasks.indexOf($scope.RemainingTasks.filter(function (tasks) { return tasks.TaskTrackerId == $scope.TemporaryTask.TaskTrackerId })[0]), 1);
                    $scope.ClosedTasks.push(closed[0]);
                    $rootScope.$broadcast('AllTasks');
                }
            }).
            error(function (data, status, headers, config) {
                messageAlertEngine.callAlertMessage("errorInitiated", "Please try after sometime !!!!", "danger", true);
            });
        $scope.progressbar = true;
        $scope.selTask = [];
        $scope.showOnClose = false;
    }
    $scope.reopenTask = function () {
        $scope.progressbar = false;
        var opened = [];
        $http.post(rootDir + '/TaskTracker/Reactivetask?taskTrackerID=' + $scope.TemporaryTask.TaskTrackerId).
            success(function (data, status, headers, config) {
                if (data == "true") {
                    messageAlertEngine.callAlertMessage("successfullySaved", "Task reopened successfully", "success", true);
                }
                if ($rootScope.tabNames == 'CloseTasks') {
                    opened = $scope.ClosedTasks.splice($scope.ClosedTasks.indexOf($scope.ClosedTasks.filter(function (tasks) { return tasks.TaskTrackerId == $scope.TemporaryTask.TaskTrackerId })[0]), 1);
                    //Added By Manideep Innamuri
                    opened[0].AssignedTo = opened[0].LastUpdatedBy;
                    opened[0].SetReminder = false;
                    $scope.showOnClose = false;
                    $scope.TasksAssigned.push(opened[0]);
                    $scope.RemainingTasks.push(opened[0]);
                    for (var j in $scope.TasksAssigned) {
                        if ($scope.TasksAssigned[j].TaskTrackerId == $scope.TemporaryTask.TaskTrackerId) {
                            $scope.TasksAssigned[j].CompleteStatus = "Open";
                        }
                    }
                    $rootScope.$broadcast('ClosedTasks');
                }
                for (var i in $scope.RemainingTasks) {
                    if ($scope.RemainingTasks[i].TaskTrackerId == $scope.TemporaryTask.TaskTrackerId) {
                        $scope.RemainingTasks[i].CompleteStatus = "Open";
                    }
                }
                if ($rootScope.tabNames == 'DailyTask') {
                    $rootScope.$broadcast('DailyTasks');
                }
                else if ($rootScope.tabNames == 'AllTask') {
                    $rootScope.$broadcast('AllTasks');
                }
            }).
            error(function (data, status, headers, config) {
                messageAlertEngine.callAlertMessage("errorInitiated", "Please try after sometime !!!!", "danger", true);
            });
        $scope.progressbar = true;
    };
    $scope.CloseTask = function () {
        var promise = $scope.editTask().then(function () {
            $scope.removeTask();
            $scope.detailViewfortab = false;
        });
    }
    $scope.inactiveWarning = function (task) {
        $scope.errormessage = false;
        $scope.errormessageforAssignedto = false;
        $scope.errormessageforprovider = false;
        $scope.errormessageforsubsection = false;
        $scope.errormessageforHospitalName = false;
        $scope.errormessageforInsurancecompany = false;
        var $formData;
        $formData = $('#newTaskFormDiv');
        var validationCount = 0;
        var validatemodeoffollowupforedit = $($formData[0]).find($("[name='ModeOfFollowUp']")).val();
        var validateprovidernameforedit = $($formData[0]).find($("[name='ProviderName']")).val();
        var validatesubsectionnameforedit = $($formData[0]).find($("[name='SubSectionName']")).val();
        var validatehospitalnameforedit = $($formData[0]).find($("[name='HospitalName']")).val();
        var validateinsurancecompanynameforedit = $($formData[0]).find($("[name='InsuranceCompany']")).val();
        var validateassignedtoforedit = $($formData[0]).find($("[name='AssignedTo']")).val();
        var assignedtoresultforedit = $scope.validateAssignedTo(validateassignedtoforedit);
        if (assignedtoresultforedit == "") {
            $scope.errormessageforAssignedto = true;
            validationCount++;
        }
        var providernameresultforedit = $scope.validateproviderName(validateprovidernameforedit);
        if (providernameresultforedit == "") {
            $scope.errormessageforprovider = true;
            validationCount++;
        }
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
        if (angular.isObject(task)) {
            $scope.TemporaryTask = {};
            $scope.TemporaryTask = angular.copy(task);
        }
        if ($scope.Followups.length == 4) {
            $scope.errormessage = true;
        }
        ResetFormForValidation($formData);
        validationStatus = $formData.valid();
        if (validationCount == 0 && !$scope.errormessage && validationStatus)
            $('#inactiveWarningModal').modal();
    }
    $scope.QuickRemove = function (task) {
        if (angular.isObject(task)) {
            $scope.TemporaryTask = {};
            $scope.TemporaryTask = angular.copy(task);
        }
        $('#inactiveWarningModal1').modal();
    }
    $scope.Quickreopen = function (task) {
        if (angular.isObject(task)) {
            $scope.TemporaryTask = {};
            $scope.TemporaryTask = angular.copy(task);
        }
        $('#reactiveWarningModal2').modal();
    }
    $scope.hideDropDown = function () {
        $(".ProviderTypeSelectAutoList").hide();
    }
    $scope.initPop = function () {
        $('[data-toggle="popover"]').popover();
    };
    $scope.selectRemTask = function (event, provider) {
        if (!$scope.showOnClose) {
            return;
        }
        if (provider.SelectStatus == true) {
            provider.SelectStatus = false;
            $scope.selTask.splice($scope.selTask.indexOf(provider), 1);
            $scope.selTaskIDs.splice($scope.selTaskIDs.indexOf(provider), 1);
            if ($scope.selTask.length == 0) {
                $scope.CancelReminder();
            }
        }
        else {
            provider.SelectStatus = true;
            $scope.selTask.push(provider);
            $scope.selTaskIDs.push(provider);
        }
        $scope.taskList.tasks = angular.copy($scope.selTask);
    }
    $scope.setInitialTaskData = function (data) {
        $.each(data, function (Mkey, Mvalue) {
            Mvalue["SetReminder"] = false;
        });
        if (localStorage.getItem("TaskReminders") != [] || localStorage.getItem("TaskReminders") != "" || localStorage.getItem("TaskReminders") != undefined) {
            var StoredReminderData = JSON.parse(localStorage.getItem("TaskReminders"));
            var taskDataFromApp = [];
            $.each(StoredReminderData, function (k, v) {
                v = JSON.parse(v.ReminderInfo);
                taskDataFromApp.push(v);
            });
            if ((data != [] || data != "") && (taskDataFromApp != [] || taskDataFromApp != "" || taskDataFromApp != undefined)) {
                $.each(data, function (Mkey, Mvalue) {
                    $.each(taskDataFromApp, function (Skey, Svalue) {
                        if (Mvalue.TaskTrackerId == Svalue.TaskTrackerId) {
                            Mvalue["SetReminder"] = true;
                            if (Svalue.ScheduledDateTime != null && Svalue.ScheduledDateTime != undefined)
                                Mvalue["ScheduledDateTime"] = new Date(parseInt(Svalue.ScheduledDateTime.replace("/Date(", "").replace(")/", ""), 10));
                        }
                    });
                });
            }
        }
        else {
            if ((data != [] || data != "")) {
                $.each(data, function (key, value) {
                    value["SetReminder"] = false;
                });
            }
        }
        return data;
    };
    $scope.SetReminderModal = function () {
        $scope.showSetReminder = true;
        $scope.ShowAddButton = true;
    }
    $scope.ReSetReminderModal = function (id) {
        $scope.showSetReminder = true;
        $scope.ShowAddButton = false;
        $scope.RescheduleID = id;
    }
    $scope.taskList = {
        tasks: [],
        remainingTime: '',
        reminderDate: '',
        reminderDateTime: '',
        taskCount: 0
    };
    $scope.CancelReminder = function () {
        $scope.showSetReminder = false;
        $scope.ResetRemData();
    }
    $scope.ReSetReminder = function () {
        $scope.showSetReminder = false;
        var TaskData = JSON.parse(localStorage.getItem("TaskReminders"));
        var IDToReschedule;
        $.each(TaskData, function (key, value) {
            var temp = JSON.parse(value.ReminderInfo);
            if (temp.TaskTrackerId == $scope.RescheduleID) {
                IDToReschedule = value.TaskReminderID;
            }
        })
        var ReminderDateTime = new Date($('#datetimepicker3').val());
        var DateToReschedule = ReminderDateTime.getUTCFullYear() + '/' + (ReminderDateTime.getMonth() + 1) + '/' + ReminderDateTime.getDate() + ' ' + ReminderDateTime.getHours() + ':' + ReminderDateTime.getMinutes() + ':' + ReminderDateTime.getSeconds();
        $.ajax({
            url: rootDir + '/TaskTracker/RescheduleReminder',
            data: JSON.stringify({ 'taskID': IDToReschedule, 'scheduledDateTime': DateToReschedule }),
            type: "POST",
            contentType: "application/json",
            success: function (response) {
                if (response) {
                    $(".RemainderBody").addClass('show');
                    $('#ReminderFullBody').remove();
                    clearInterval(reminderInterval);
                    TaskNotificationReminder();
                }
            },
            error: function (error) {
            }
        });
    };
    $scope.SetReminder = function () {
        $scope.showSetReminder = false;
        $scope.TaskReminder = [];
        var ReminderDateTime = new Date($('#datetimepicker3').val());
        for (var i = 0; i < $scope.taskList.tasks.length; i++) {
            $scope.TaskReminder.push({ ReminderInfo: JSON.stringify($scope.taskList.tasks[i]), CreatedDate: new Date(), ScheduledDateTime: ReminderDateTime });
            $scope.taskList.reminderDateTime = ReminderDateTime;
        }
        $http.post(rootDir + '/TaskTracker/SetReminder', $scope.TaskReminder).success(function (data) {
            toaster.pop('Success', "Success", 'Reminder Set Successfully');
            $.each(ctrl.displayed, function (Mkey, Mvalue) {
                $.each($scope.taskList.tasks, function (Skey, Svalue) {
                    if (Mvalue.TaskTrackerId == Svalue.TaskTrackerId) {
                        Mvalue["SetReminder"] = true;
                    }
                });
            });
            $scope.ResetRemData();
            TaskNotificationReminder();
            if ($rootScope.tabNames == "AllTask")
                $scope.clerCheckBoxForAllTasks();
            else if ($rootScope.tabNames == "DailyTask")
                $scope.clerCheckBoxForDailyTasks();
        }).error(function (data) {
            toaster.pop('error', "error", 'Some Error Occured, please try again later');
        });
        $scope.taskList.reminderDateTime = ReminderDateTime;
        localStorage.setItem("TaskReminders", JSON.stringify($scope.taskList));
        $scope.taskStored = JSON.parse(localStorage.getItem("TaskReminders"));
    }
    $scope.ResetRemData = function () {
        angular.forEach($scope.selTask, function (value, key) {
            value.SelectStatus = false;
        });
        $scope.selTask = [];
        $scope.taskList.tasks = angular.copy($scope.selTask);
    };
    $scope.clerCheckBoxForAllTasks = function () {
        for (var i = 0; i < $scope.RemainingTasks.length; i++) {
            $scope.RemainingTasks[i].selected = false;
        }
    }
    $scope.clerCheckBoxForDailyTasks = function () {
        for (var i = 0; i < $scope.TasksAssigned.length; i++) {
            $scope.TasksAssigned[i].selected = false;
        }
    }
    $scope.getCheckedTask = function (evt, task, checkedStatus) {
        $scope.changeClass();
    }

    //////////////////////////////////////////////////////////////////////////////////////
    $scope.validateForm = function () {
        angular.element('#newTaskFormDiv input').each(function (index, ele) {
            if (ele.classList != 0 && ele.classList.contains('TaskFormValidator')) {
                console.log(ele.name);
                if (ele.value == "") {
                    angular.element(ele).after('<span class="' + ele.name + 'Error error text-danger"> Please Select ' + ele.name + '</span>');
                }
                else {
                    switch (ele.name) {
                        case "ProviderName":
                            if ($scope.Providers.filter(function (e) { return e.Name == ele.value; }).length == 0) {
                                angular.element(ele).after('<span class="error text-danger"> Please Select ' + ele.name + '</span>');
                            }
                            break;
                        case "SubSectionName":
                            if ($scope.SubSections.filter(function (e) { return e.SubSectionName == ele.value; }).length == 0) {
                                angular.element(ele).after('<span class="error text-danger"> Please Select ' + ele.name + '</span>');
                            }
                            break;
                        case "FollowUpMode":
                            break;
                        case "AssignedTo":
                            if ($scope.Users.filter(function (e) { return e.UserName == ele.value; }).length == 0) {
                                angular.element(ele).after('<span class="error text-danger"> Please Select ' + ele.name + '</span>');
                            }
                            break;
                        case "HospitalName":
                            if ($scope.Hospitals.filter(function (e) { return e.HospitalName == ele.value; }).length == 0) {
                                angular.element(ele).after('<span class="error text-danger"> Please Select ' + ele.name + '</span>');
                            }
                            break;
                        case "PlanName":
                            if ($scope.PlanNames.filter(function (e) { return e.PlanName == ele.value; }).length == 0) {
                                angular.element(ele).after('<span class="error text-danger"> Please Select ' + ele.name + '</span>');
                            }
                            break;
                    }
                }
            }
        });
        $scope.AddTask();
    };
    $scope.AddTask = function () {
        return;
        $.ajax({
            url: '/Tasks/AddNewTask',
            type: 'POST',
            data: new FormData($('#newTaskFormDiv')[0]),
            async: true,
            cache: false,
            contentType: false,
            processData: false,
            success: function (data) {
                console.log(data);
            },
            error: function (e) {
            }
        });
    };
    $scope.ResetForm = function () {
        $('#newTaskFormDiv')[0].reset();
        $('.error').remove();
    };
    //angular.element(".nav-tabs a").on("click", function (e) {
    //    if ($(this).hasClass("disabled")) {
    //        e.preventDefault();
    //        return false;
    //    }
    //});
});
function ResetFormForValidation(form) {
    form.removeData('validator');
    form.removeData('unobtrusiveValidation');
    $.validator.setDefaults({ ignore: null });
    $.validator.unobtrusive.parse(form);
}
function DismissSingleTaskforremoveTask(remainderid) {
    $.ajax({
        url: rootDir + '/TaskTracker/DismissReminder?taskID=' + remainderid,
        type: "POST",
        success: function (response) {
            if (response) {
                var TaskData = JSON.parse(localStorage.getItem("TaskReminders"));
                $.each(TaskData, function (key, value) {
                    if (value.TaskReminderID == SelectedTaskID) {
                        taskToDismiss = value;
                        TaskData.splice(key, 1);
                        TaskNotificationReminder();

                    }
                })
            }
        },
        error: function (error) {
        }
    });

};
var TaskNotificationReminder = function () {
    $.ajax({
        url: rootDir + '/TaskTracker/GetReminders',
        type: "GET",
        success: function (response) {
            var taskStored = response.reminders;
            localStorage.setItem("TaskReminders", JSON.stringify(response.reminders)); // setting the reminder objects into local storage
            view = response.responseView;
            getReminderView(taskStored);
            reminderInterval = setInterval(function () { CheckReminder() }, 30000); // calling function repeatedly to check reminder
        },
        error: function (error) {
        }
    });
}
$(document).ready(function () {
    $(".ProviderTypeSelectAutoList").hide();
    $("body").tooltip({ selector: '[data-toggle=tooltip]' });
    $("#sidemenu").addClass("menu-in");
    $("#page-wrapper").addClass("menuup");
});
$(document).click(function (event) {
    if (!$(event.target).hasClass("form-control") && $(event.target).parents(".ProviderTypeSelectAutoList").length === 0) {
        $(".ProviderTypeSelectAutoList").hide();
    }
});