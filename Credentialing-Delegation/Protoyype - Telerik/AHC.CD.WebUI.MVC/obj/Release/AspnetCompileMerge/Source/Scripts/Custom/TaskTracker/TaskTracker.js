//Module declaration
var trackerApp = angular.module('TrackerApp', ['ngTable', 'mgcrea.ngStrap', 'loadingInteceptor']);

trackerApp.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {
    
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

trackerApp.config(function ($datepickerProvider) {
    angular.extend($datepickerProvider.defaults, {
        startDate: 'today',
        autoclose: true,
        useNative: true
    });
})


//Controller declaration
trackerApp.controller('TrackerCtrl', function ($scope, $http, $q, $filter, ngTableParams, messageAlertEngine) {

    $scope.DailyTasks = [];
    $scope.Tasks = [];
    $scope.CDUsers = [];
    $scope.LoginUsers = [];
    $scope.Providers = [];
    $scope.Hospitals = [];
    $scope.users = [];
    $scope.userAuthID = -1;
    
    var GlobalHospitalData=[];


    function CDUsersData() {
        var defer = $q.defer();
        $http.get(rootDir + '/TaskTracker/GetAllCDUsers').
            success(function (data, status, headers, config) {
                $scope.CDUsers = data;
                console.log("CDUsers");
                console.log($scope.CDUsers);
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
            GlobalHospitalData=data;
            console.log("Hospitals");
            console.log($scope.Hospitals);
            defer.resolve(data);
        }).
        error(function (data, status, headers, config) {
            defer.reject();
        });
        return defer.promise;
    }

    function TasksData() {
        var defer = $q.defer();
        $http.get(rootDir + '/TaskTracker/GetAllTasksByUserId').
            success(function (data, status, headers, config) {
                $scope.Tasks = data;
                if ($scope.Tasks.length > 0) {
                    $scope.userAuthID = $scope.Tasks[0].AssignedTo.CDUserID;
                }
                console.log("Tasks");
                console.log($scope.Tasks);
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
                console.log("Providers");
                console.log($scope.Providers);
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
                console.log("LoginUsers");
                console.log($scope.LoginUsers);
                defer.resolve(data);
            }).
            error(function (data, status, headers, config) {
                defer.reject();
            });
        return defer.promise;
    }


    $scope.LoadData = function () {
        $scope.progressbar = false;
        $q.all([$q.when(CDUsersData()),
            $q.when(HospitalsData()),
            $q.when(TasksData()),
            $q.when(ProvidersData()),
            $q.when(LoginUsersData())
        ]).then(function (response) {
            $scope.SubSections = [
                              { Name: "State License", TabName: "#identification", SubSectionID: "#StateLicense" },
                              { Name: "Federal DEA Information", TabName: "#identification", SubSectionID: "#FederalDEA" },
                              { Name: "Medicare Information", TabName: "#identification", SubSectionID: "#MedicareInformation" },
                              { Name: "Medicaid Information", TabName: "#identification", SubSectionID: "#MedicaidInformation" },
                              { Name: "Provider Missing Items Documentation", TabName: "", SubSectionID: "" },
                              { Name: "Insurance Enrollment", TabName: "", SubSectionID: "" },
                              { Name: "Hospital Enrollment", TabName: "", SubSectionID: "" },
                              { Name: "Insurance Re-Enrollment", TabName: "", SubSectionID: "" },
                              { Name: "Hospital Re-Enrollment", TabName: "", SubSectionID: "" },
                              { Name: "CAQH Update", TabName: "", SubSectionID: "" },
                              { Name: "Location Change", TabName: "", SubSectionID: "" },
                              { Name: "Panel Closing", TabName: "", SubSectionID: "" },
                              { Name: "Provider Termination", TabName: "", SubSectionID: "" },
            ];

            $scope.Followups = [
                                      { Name: "Phone Call", Type: "PhoneCall" },
                                      { Name: "Email", Type: "Email" },
                                      { Name: "Online Form Completion", Type: "OnlineFormCompletion" },
                                      { Name: "Paper Form Completion", Type: "PaperFormCompletion" }
            ];
            for (var i = 0; i < $scope.LoginUsers.length; i++) {

                for (var j = 0; j < $scope.CDUsers.length; j++) {

                    if ($scope.LoginUsers[i].Id == $scope.CDUsers[j].AuthenicateUserId) {

                        $scope.users.push({ CDUserID: $scope.CDUsers[j].CDUserID, AuthenicateUserId: $scope.CDUsers[j].AuthenicateUserId, FullName: $scope.LoginUsers[i].FullName, Email: $scope.LoginUsers[i].Email, UserName: $scope.LoginUsers[i].UserName })
                    }
                }
            }
            console.log("AssignedTo");
            console.log($scope.users);
            for (var k = 0; k < $scope.Tasks.length; k++) {

                if ($scope.Tasks[k].ProfileID != null)
                    var ProviderName = $scope.Providers.filter(function (Providers) { return Providers.ProfileId == $scope.Tasks[k].ProfileID })[0].Name;

                if ($scope.Tasks[k].AssignedToId != null) {
                    var AssignedTo = $scope.users.filter(function (users) { return users.CDUserID == $scope.Tasks[k].AssignedToId })[0].UserName;
                }


                if ($scope.Tasks[k].HospitalID != null)
                    var HospitalName = $scope.Hospitals.filter(function (Hospitals) { return Hospitals.HospitalID == $scope.Tasks[k].HospitalID })[0].HospitalName;

                var tabID="";
                var sectionID = "";
                var Followups = "";
                for (var i in $scope.SubSections) {
                    if ($scope.SubSections[i].Name == $scope.Tasks[k].SubSectionName) {
                        tabID=$scope.SubSections[i].TabName;
                        sectionID = $scope.SubSections[i].SubSectionID;
                    }
                }
                for (var i in $scope.Followups) {
                    if ($scope.Followups[i].Type == $scope.Tasks[k].ModeOfFollowUp) {
                        Followups = $scope.Followups[i].Name;
                    }
                }
                $scope.DailyTasks.push({
                    TaskTrackerId: $scope.Tasks[k].TaskTrackerId, ProfileID: $scope.Tasks[k].ProfileID,
                    ProviderName: ProviderName, SubSectionName: $scope.Tasks[k].SubSectionName,
                    Subject: $scope.Tasks[k].Subject, NextFollowUpDate: $scope.Tasks[k].NextFollowUpDate = null ? "" : $scope.Tasks[k].NextFollowUpDate.split('T')[0].split('-')[1].split('-')[0] + "/" + $scope.Tasks[k].NextFollowUpDate.split('-')[2].split('T')[0] + "/" + $scope.Tasks[k].NextFollowUpDate.split('T')[0].split('-')[0],
                    ModeOfFollowUp: Followups, FollowUp: Followups,
                    AssignedToId: $scope.users.filter(function (users) { return users.CDUserID == $scope.Tasks[k].AssignedToId })[0].AuthenicateUserId, AssignedTo: AssignedTo, HospitalID: $scope.Tasks[k].HospitalID,
                    Hospital: HospitalName, Notes: $scope.Tasks[k].Notes, ModifiedDate: $scope.Tasks[k].LastModifiedDate,
                    TabID: tabID,
                    SubSectionID: sectionID,
                    CompleteStatus:"Open"
                });
            }
            $scope.init_table($scope.DailyTasks);
            console.log("Dailytasks");
            console.log($scope.DailyTasks);
            $scope.progressbar = true;
        }, function (error) {
            $scope.progressbar = true;
        });
    }



    



    //To Display the drop down div
    $scope.searchCumDropDown = function (divId) {
        $("#" + divId).show();
    };
    //$scope.$watch("task.ProfileID", function (oldv, newV) {
    //    if (oldv === newV || typeof newV == 'undefined') {
    //        return;
    //    }
    //    else {
    //        var providertemp = {};
    //        for (i in $scope.Providers) {
    //            if ($scope.Providers[i].ProfileId == $scope.task.ProfileID) {
    //                providertemp = $scope.Providers[i];
    //                break;
    //            }
    //        }
    //        var HospitalExist = providertemp.HospitalInfo.HasHospitalPrivilege;
    //        if (HospitalExist == "YES") {
    //            var TOBJ=[];
    //            for(j in providertemp.HospitalInfo.HospitalPrivilegeDetails){
    //                for (k in $scope.Hospitals) {
    //                    if(providertemp.HospitalInfo.HospitalPrivilegeDetails[j].HospitalID==$scope.Hospitals[k].HospitalID){
    //                        TOBJ.push($scope.Hospitals[k]);
    //                    }
    //                }
    //            }
    //            $scope.Hospitals=[];
    //            $scope.Hospitals=angular.copy(TOBJ);
    //        }
    //        else {
    //            $scope.Hospitals = [];
    //            $scope.Hospitals = GlobalHospitalData;
    //            }
    //    }
    //})
    $scope.SelectProvider = function (Provider) {
        $scope.task.ProfileID = Provider.ProfileId;
        $scope.task.ProviderName = Provider.Name;

        $(".ProviderTypeSelectAutoList").hide();
    }

    $scope.SelectUser = function (User) {
        $scope.task.AssignedToId = User.AuthenicateUserId;
        $scope.task.AssignedTo = User.UserName;
        $(".ProviderTypeSelectAutoList").hide();
    }
    $scope.SelectHospital = function (Hospital) {
        $scope.task.HospitalID = Hospital.HospitalID;
        $scope.task.Hospital = Hospital.HospitalName;
        $(".ProviderTypeSelectAutoList").hide();
    }

    $scope.SelectFollowup = function (Followup) {
        $scope.task.FollowUpDetail = $scope.Followups.filter(function (Followups) { return Followups.Name == Followup.Name })[0].Type;
        $scope.task.FollowUp = Followup.Name
        $(".ProviderTypeSelectAutoList").hide();
    }

    $scope.SelectSubSection = function (SubSection) {
        $scope.task.SubSectionName = SubSection.Name;
        $(".ProviderTypeSelectAutoList").hide();
    }
    $scope.addView = false;
    $scope.editView = false;

    $scope.showAddView = function () {
        $scope.task = {};
        $scope.detailView = false;
        $scope.addView = true;
        $scope.editView = false;
    }

    $scope.cancelAdd = function () {
        $scope.detailView = false;
        $scope.addView = false;
        $scope.editView = false;
        $scope.progressbar = true;
    }

    $scope.addNewTask = function (task) {
        $scope.progressbar = false;
        var validationStatus;
        var url;
        var $formData;
        $formData = $('#newTaskFormDiv');
        url = rootDir + "/TaskTracker/AddTask"
        ResetFormForValidation($formData);
        validationStatus = $formData.valid();

        if (validationStatus) {
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
                    if (data.AssignedToId == $scope.userAuthID) {
                        var tabID = "";
                        var sectionID = "";
                        var Followups = "";
                        for (var i in $scope.SubSections) {
                            if ($scope.SubSections[i].Name == data.SubSectionName) {
                                tabID = $scope.SubSections[i].TabName;
                                sectionID = $scope.SubSections[i].SubSectionID;
                            }
                        }
                        for (var i in $scope.Followups) {
                            if ($scope.Followups[i].Type == data.ModeOfFollowUp) {
                                Followups = $scope.Followups[i].Name;
                            }
                        }
                        $scope.data.push({
                            TaskTrackerId: data.TaskTrackerId, ProfileID: data.ProfileID,
                            ProviderName: $scope.Providers.filter(function (Providers) { return Providers.ProfileId == data.ProfileID })[0].Name, SubSectionName: data.SubSectionName,
                            Subject: data.Subject, NextFollowUpDate: data.NextFollowUpDate = null ? "" : data.NextFollowUpDate.split('T')[0].split('-')[1].split('-')[0] + "/" + data.NextFollowUpDate.split('-')[2].split('T')[0] + "/" + data.NextFollowUpDate.split('T')[0].split('-')[0],
                            ModeOfFollowUp: Followups, FollowUp: Followups,
                            AssignedToId: $scope.users.filter(function (users) { return users.CDUserID == data.AssignedToId })[0].AuthenicateUserId, AssignedTo: $scope.users.filter(function (users) { return users.CDUserID == data.AssignedToId })[0].UserName, HospitalID: data.HospitalID,
                            Hospital:data.HospitalID==null?null: $scope.Hospitals.filter(function (Hospitals) { return Hospitals.HospitalID == data.HospitalID })[0].HospitalName, Notes: data.Notes, ModifiedDate: data.LastModifiedDate,
                            TabID: tabID,
                            SubSectionID: sectionID,
                            CompleteStatus: "Open"
                        });
                    }
                    $scope.cancelAdd();
                    messageAlertEngine.callAlertMessage("successfullySaved", "Task successfully assingned", "success", true);
                    $scope.tableParams.reload();
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

    $scope.showEditview = function (task) {
        var Followups = "";
        for (var i in $scope.Followups) {
            if ($scope.Followups[i].Name == task.FollowUp) {
                Followups = $scope.Followups[i].Type;
            }
        }
        $scope.detailView = false;
        $scope.editView = true;
        $scope.addView = false;
        task.FollowUpDetail = Followups;
        $scope.task = task;
    }

    $scope.cancelEdit = function () {
        $scope.detailView = false;
        $scope.addView = false;
        $scope.editView = false;
        $scope.progressbar = true;

    }

    $scope.editTask = function (task) {
        $scope.progressbar = false;
        var validationStatus;
        var url;
        var $formData;
        $formData = $('#newTaskFormDiv');
        url = rootDir + "/TaskTracker/UpdateTask"

        ResetFormForValidation($formData);
        validationStatus = $formData.valid();

        if (validationStatus) {
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
                    if (data.AssignedToId == $scope.userAuthID) {
                        $scope.data.splice($scope.data.indexOf($scope.data.filter(function (datas) { return datas.TaskTrackerId == data.TaskTrackerId })[0]), 1);
                        var tabID = "";
                        var sectionID = "";
                        var Followups="";
                        for (var i in $scope.SubSections) {
                            if ($scope.SubSections[i].Name == data.SubSectionName) {
                                tabID = $scope.SubSections[i].TabName;
                                sectionID = $scope.SubSections[i].SubSectionID;
                            }
                        }
                        for (var i in $scope.Followups) {
                            if ($scope.Followups[i].Type == data.ModeOfFollowUp) {
                                Followups = $scope.Followups[i].Name;
                            }
                        }
                        var Date=data.NextFollowUpDate == null ? "" : data.NextFollowUpDate.split('T')[0].split('-')[1].split('-')[0] + "/" + data.NextFollowUpDate.split('-')[2].split('T')[0] + "/" + data.NextFollowUpDate.split('T')[0].split('-')[0];
                        $scope.data.push({
                            TaskTrackerId: data.TaskTrackerId, ProfileID: data.ProfileID,
                            ProviderName: $scope.Providers.filter(function (Providers) { return Providers.ProfileId == data.ProfileID })[0].Name == 'undefined' ? "" : $scope.Providers.filter(function (Providers) { return Providers.ProfileId == data.ProfileID })[0].Name, SubSectionName: data.SubSectionName,
                            Subject: data.Subject, NextFollowUpDate: Date,
                            ModeOfFollowUp: Followups, FollowUp: Followups,
                            AssignedToId: $scope.users.filter(function (users) { return users.CDUserID == data.AssignedToId })[0].AuthenicateUserId, AssignedTo: $scope.users.filter(function (users) { return users.CDUserID == data.AssignedToId })[0].UserName, HospitalID: data.HospitalID,
                            Hospital: data.HospitalID == null ? null : $scope.Hospitals.filter(function (Hospitals) { return Hospitals.HospitalID == data.HospitalID })[0].HospitalName, Notes: data.Notes, ModifiedDate: data.LastModifiedDate,
                            TabID: tabID,
                            SubSectionID: sectionID,
                            CompleteStatus: "Open"
                        });
                    }
                    else {

                        $scope.DailyTasks.splice($scope.DailyTasks.indexOf($scope.DailyTasks.filter(function (DailyTasks) { return DailyTasks.TaskTrackerId == data.TaskTrackerId })[0]), 1);

                        $scope.tableParams.reload();
                    }
                    $scope.cancelEdit();
                    messageAlertEngine.callAlertMessage("successfullySaved", "Task updated successfully", "success", true);
                    $scope.tableParams.reload();
                    $scope.progressbar = true;
                    document.getElementById("newTaskFormDiv").reset();

                },
                error: function (e) {
                    $scope.cancelEdit();
                    messageAlertEngine.callAlertMessage("errorInitiated", "Please try after sometime !!!!", "danger", true);
                    $scope.progressbar = true;
                    document.getElementById("newTaskFormDiv").reset();

                }
            });
        }
        $scope.progressbar = true;
    }

    $scope.detailView = false;

    $scope.showDetailView = function (task) {
        $scope.taskView = angular.copy(task);
        $scope.editView = false;
        $scope.addView = false;
        $scope.detailView = true;

    }

    $scope.cancelView = function () {
        $scope.detailView = false;
        $scope.addView = false;
        $scope.editView = false;
    }
    $scope.TemporaryTask = {};
    //To initiate Removal Confirmation Modal
    $scope.inactiveWarning = function (task) {
        if (angular.isObject(task)) {
            
            $scope.TemporaryTask = {};
            $scope.TemporaryTask = angular.copy(task);
        }
        $('#inactiveWarningModal').modal();
    };

    $scope.removeTask = function () {
        $scope.progressbar = false;

        $http.post(rootDir + '/TaskTracker/Inactivetask?taskTrackerID=' + $scope.TemporaryTask.TaskTrackerId).
           success(function (data, status, headers, config) {
               if(data=="true"){
                   messageAlertEngine.callAlertMessage("successfullySaved", "Task Closed successfully", "success", true);
               }
               $scope.DailyTasks.splice($scope.DailyTasks.indexOf($scope.DailyTasks.filter(function (DailyTasks) { return DailyTasks.TaskTrackerId == $scope.TemporaryTask.TaskTrackerId })[0]), 1);
              
               $scope.tableParams.reload();
           }).
           error(function (data, status, headers, config) {
               messageAlertEngine.callAlertMessage("errorInitiated", "Please try after sometime !!!!", "danger", true);
           });
        $scope.progressbar = true;
    }

    $scope.init_table = function (data) {

        $scope.data = data;
        var counts = [];

        if ($scope.data.length <= 10) {
            counts = [];
        }
        else if ($scope.data.length <= 25) {
            counts = [10, 25];
        }
        else if ($scope.data.length <= 50) {
            counts = [10, 25, 50];
        }
        else if ($scope.data.length <= 100) {
            counts = [10, 25, 50, 100];
        }
        else if ($scope.data.length > 100) {
            counts = [10, 25, 50, 100];
        }
        for (var i = 0; i < $scope.data.length; i++) {

        }
        $scope.tableParams = new ngTableParams({
            page: 1,            // show first page
            count: 10,          // count per page
            filter: {
                //name: 'M'       // initial filter
                //FirstName : ''
            },
            sorting: {
                //name: 'asc'     // initial sorting
            }
        }, {
            counts: counts,
            total: $scope.data.length, // length of data
            getData: function ($defer, params) {
                // use build-in angular filter
                var filteredData = params.filter() ?
                        $filter('filter')($scope.data, params.filter()) :
                        $scope.data;
                var orderedData = params.sorting() ?
                        $filter('orderBy')(filteredData, params.orderBy()) :
                        $scope.data;

                params.total(orderedData.length); // set total for recalc pagination
                $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
            }
        });

    };

    //if filter is on
    $scope.ifFilter = function () {
        try {
            var bar;
            var obj = $scope.tableParams.$params.filter;
            for (bar in obj) {
                if (obj[bar] != "") {
                    return false;
                }
            }
            return true;
        }
        catch (e) { return true; }
    }

    //Get index first in table
    $scope.getIndexFirst = function () {
        try {
            if ($scope.groupBySelected == 'none') {
                return ($scope.tableParams.$params.page * $scope.tableParams.$params.count) - ($scope.tableParams.$params.count - 1);
            }
        }
        catch (e) { }
    }

    //Get index Last in table
    $scope.getIndexLast = function () {
        try {
            if ($scope.groupBySelected == 'none') {
                return { true: ($scope.data.length), false: ($scope.tableParams.$params.page * $scope.tableParams.$params.count) }[(($scope.tableParams.$params.page * $scope.tableParams.$params.count) > ($scope.data.length))];
            }
        }
        catch (e) { }
    }



})

function ResetFormForValidation(form) {
    form.removeData('validator');
    form.removeData('unobtrusiveValidation');
    $.validator.setDefaults({ ignore: null });
    $.validator.unobtrusive.parse(form);

}
$(document).ready(function () {
    $(".ProviderTypeSelectAutoList").hide();
});
$(document).click(function (event) {
    if (!$(event.target).hasClass("form-control") && $(event.target).parents(".ProviderTypeSelectAutoList").length === 0) {
        $(".ProviderTypeSelectAutoList").hide();
    }
});