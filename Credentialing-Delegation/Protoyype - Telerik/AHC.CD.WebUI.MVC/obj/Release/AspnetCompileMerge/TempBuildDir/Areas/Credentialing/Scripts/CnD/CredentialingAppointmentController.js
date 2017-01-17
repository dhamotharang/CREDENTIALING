// -------------------- CCM controller Angular Module-------------------------------
//---------- Author : KRGLV -------------

//------------------------- angular module ----------------------------
var CCOApp = angular.module('CCOApp', ['ui.bootstrap', 'ngTable']);

//------------- angular tool tip recall directive ---------------------------
CCOApp.directive('tooltip', function () {
    return function (scope, elem) {
        elem.tooltip();
    };
});

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
            $timeout(animateMessageAlertOff, 5000);
        }
    }

    $rootScope.closeAlertMessage = function () {
        $rootScope.messageDesc = "";
        $rootScope.activeMessageDiv = "";
        $rootScope.messageType = "";
    }
}]);


CCOApp.service('FiltteredList', function ($http, $q) {
    this.getFiltteredList = function () {
        var deferred = $q.defer();
        $http({
            method: 'GET',
            url: rootDir + '/Credentialing/CnD/GetAllCredentialInfoListHistory'
        }).
         success(function (data, status, headers, config) {
            
             deferred.resolve(data)
         }).
         error(function (data, status) {
             deferred.reject(data);
         });

        return deferred.promise;
    }
});



//------------------------------- convert date format -----------------------------
var ConvertDateFormat = function (value) {
    if (value) {
        return new Date(parseInt(value.replace("/Date(", "").replace(")/", ""), 10));
    } else {
        return value;
    }
};

// ------------------ dashboard root scope ------------------
CCOApp.run(['$rootScope', function ($rootScope) {
    //----------------- filter day left ranges --------------------
    //$rootScope.days = [5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 70, 80, 90, 180];
}]);

CCOApp.controller("CCOController", function ($scope, $filter, $http, FiltteredList, ngTableParams, $timeout) {
    //-------------- CCM Action Item Lists -----------------
    $scope.Actions = [
        {
            ActionID: 1,
            ActionName: "Credentialing"
        },
        {
            ActionID: 2,
            ActionName: "Re-Credentialing"
        },
        {
            ActionID: 3,
            ActionName: "De-Credentialing"
        }
    ];
    $scope.Plan = [];
    $scope.ProfileID = 0;
    $scope.initData = function () {

        FiltteredList.getFiltteredList().then(function (data) {
            $scope.data = data;
            for (var i = 0; i < data.length; i++) {
                $scope.DoneCreadentialing.push({
                    ProviderID: data[i].CredentialingLogs[0].CredentialingAppointmentDetail.CredentialingAppointmentDetailID,
                    FirstName: data[i].Profile.PersonalDetail.FirstName,
                    LastName: data[i].Profile.PersonalDetail.LastName,
                    ProviderTitles: angular.copy(data[i].Profile.PersonalDetail.ProviderTitles),
                    Specialities: angular.copy(data[i].CredentialingLogs[0].CredentialingAppointmentDetail.CredentialingSpecialityLists),
                    CredentialingDate: $scope.ConvertDateTo(data[i].InitiationDate),
                    Plan: data[i].Plan.PlanName,
                    RecommendedLevel: data[i].CredentialingLogs[0].CredentialingAppointmentDetail.RecommendedLevel,
                    AppointmentDate: $scope.ConvertDateTo(data[i].CredentialingLogs[0].CredentialingAppointmentDetail.CredentialingAppointmentSchedule.AppointmentDate),
                    Status: "Verified",
                    ProfileID: data[i].Profile.ProfileID
                });
                $scope.Plan.push(data[i].Plan.PlanName);
            }

            $scope.Plans = angular.copy($scope.squash($scope.Plan));
           
        },
        function () {

        });

       
    }

    //$scope.toggleMin = function () {
    //    $scope.minDate = $scope.minDate ? null : new Date();
    //};
    //$scope.toggleMin();
    $scope.resetSelection = function (data) {
        var temp = [];
        for (var i in data) {
            temp[i] = false;
        }
        return temp;
    };


    $scope.ForAppointmentDetail = [];
    $scope.ListForAppointment = function (data) {
        var id = $scope.ForAppointmentDetail.indexOf(data);
        var idx = $scope.CreadentialingData.indexOf(data);
        if (id > -1) {
            $scope.ForAppointmentDetail.splice(id, 1);
            $scope.CreadentialingData[idx].statusForApp = false;
        }
        else {
            $scope.CreadentialingData[idx].statusForApp = true;
            $scope.ForAppointmentDetail.push(data);
        }
      
    }

    $scope.notification = false;

    //----------------- Filter Data According to Required -----------------------
    $scope.getDataByMenu = function (data) {
        $scope.selectedAction = angular.copy(data.ActionID);
        $scope.CreadentialingData = angular.copy(data.CredentialingList);
        $scope.SelectedDetails = $scope.resetSelection($scope.CreadentialingData);
        

        $scope.initData();

        var count = 0;

        for (var i = 0; i < $scope.CreadentialingData.length; i++) {
            $scope.Plan.push($scope.CreadentialingData[i].Plan);
        }
        for (var i = 0; i < $scope.CreadentialingData.length; i++) {
            if ($scope.CreadentialingData[i].FirstName == null) {
                $scope.CreadentialingData[i].Name = "";
            }
            else {
                $scope.CreadentialingData[i].Name = $scope.CreadentialingData[i].FirstName;
            }
            if ($scope.CreadentialingData[i].LastName == null) {
                $scope.CreadentialingData[i].Name += "";
            }
            else {
                $scope.CreadentialingData[i].Name += " " + $scope.CreadentialingData[i].LastName;
            }
            if ($scope.CreadentialingData[i].ProviderTitles.length == 0) {
                $scope.CreadentialingData[i].ProviderTitle = "";
            }
            else {
                for (var j = 0; j < $scope.CreadentialingData[i].ProviderTitles.length; j++) {
                    if (j == 0) {
                        $scope.CreadentialingData[i].ProviderTitle = $scope.CreadentialingData[i].ProviderTitles[j].ProviderType.Title;
                    }
                    else {
                        $scope.CreadentialingData[i].ProviderTitle += "," + $scope.CreadentialingData[i].ProviderTitles[j].ProviderType.Title;
                    }
                }
            }
            if ($scope.CreadentialingData[i].Specialities.length == 0) {
                $scope.CreadentialingData[i].Specialty = "";
            }
            else {
                for (var j = 0; j < $scope.CreadentialingData[i].Specialities.length; j++) {
                    if ($scope.CreadentialingData[i].Specialities[j].Status == 'Active') {
                        if (j == 0) {
                            $scope.CreadentialingData[i].Specialty = $scope.CreadentialingData[i].Specialities[j].Name;
                        }
                        else {
                            $scope.CreadentialingData[i].Specialty += "," + $scope.CreadentialingData[i].Specialities[j].Name;
                        }
                    }
                }
            }
            var obj = parseInt(sessionStorage.getItem('CredInfoId'));
            if ($scope.CreadentialingData[i].CredentialingInfoID == parseInt(sessionStorage.getItem('CredInfoId'))) {
                $scope.CreadentialingData[i].statusForApp = true;
                $scope.ForAppointmentDetail.push($scope.CreadentialingData[i]);
                count = 1;
            }

        }

        if (count == 0 && $scope.CreadentialingData.length != 0) {

            $scope.notification = true;
            $timeout(function () {
                $scope.notification = false;
            }, 5000);

        }

        // $scope.init_table($scope.CreadentialingData);
        sessionStorage.setItem('CredInfoId', null);
        
        $scope.squash = function (arr) {
            var tmp = [];
            for (var i = 0; i < arr.length; i++) {
                if (tmp.indexOf(arr[i]) == -1) {
                    tmp.push(arr[i]);
                }
            }
            return tmp;
        }

        $scope.dt = new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate());
       


    };

    
    $scope.PlanNames = '';
    $scope.getValue = function (data) {

        data = data.trimRight();
        $scope.PlanNames = data;
        $scope.checkLength(data);
       

       
    };


    //code dirty/
    $scope.lastplanvalue = "";
    $scope.showerrormessageforemptydata = false;
    $scope.checkLength = function (data)
    {
        f = 0;
        if (data == "") {
            $scope.showerrormessageforemptydata = false;
        }

        else {

            for (i = 0; i < $scope.CreadentialingData.length; i++) {
                
                if ($scope.CreadentialingData[i].Plan == data)
                {
                    f = 1;
                   
                    break;
                }
            }

            if (f == 1) {
                $scope.showerrormessageforemptydata = false;
            } else {
                $scope.showerrormessageforemptydata = true;
            }
        }

        $scope.lastplanvalue = data;
        
    }
    //end dirty

    //------------------ init --------------
    $scope.DoneCreadentialing = [];
    $scope.SelectedDetails = [];
    $scope.SelectedDetailsID = [];
    $scope.SelectedDetailsRemoveID = null;

    //-------------- selection ---------------


    $scope.convert = function (str) {
        var date = new Date(str),
            mnth = ("0" + (date.getMonth() + 1)).slice(-2),
            day = ("0" + date.getDate()).slice(-2);
        return [mnth, day, date.getFullYear()].join("/");
    }
    
    $scope.AddSelectedCredentialing = function () {
        $scope.NoDataFound1 = false;
        var selectedCredentialing = [];
        $scope.SelectedCredentialingInfoIDs = [];
        selectedCredentialing = angular.copy($scope.DoneCreadentialing);
        
        var NotSeletedCreadentialing = [];
        for (var i = 0; i < $scope.CreadentialingData.length; i++) {
            if ($scope.CreadentialingData[i].statusForApp) {
                $scope.CreadentialingData[i].AppointmentDate = $scope.convert($scope.dt);
                selectedCredentialing.push($scope.CreadentialingData[i]);
                $scope.SelectedDetailsID.push($scope.CreadentialingData[i].ProviderID);
                $scope.SelectedCredentialingInfoIDs.push($scope.CreadentialingData[i].CredentialingInfoID)

            }
            if (!$scope.CreadentialingData[i].statusForApp || $scope.CreadentialingData[i].statusForApp == null) {
                $scope.CreadentialingData[i].statusForApp = false;
                $scope.CreadentialingData[i].AppointmentDate = "";
                NotSeletedCreadentialing.push(angular.copy($scope.CreadentialingData[i]));
            }
        }
        $scope.ForAppointmentDetail = [];
        $scope.DoneCreadentialing = selectedCredentialing;
        $scope.CreadentialingData = NotSeletedCreadentialing;
        //$scope.SelectedDetails = $scope.resetSelection($scope.CreadentialingData);
        $scope.SelectedDetailsRemoveID = null;

        var obj = {
            CredentialingInfoIDs: $scope.SelectedCredentialingInfoIDs,
            ProviderIDArray: $scope.SelectedDetailsID,
            AppointmentDate: $scope.convert($scope.dt)
        }
        $http.post(rootDir + '/Credentialing/CnD/SetAppointment', obj).success(function (data, status, headers, config) {
            //----------- success message -----------
            $scope.SelectedDetailsID.splice(0, $scope.SelectedDetailsID.length);

        }).error(function (data, status, headers, config) {

        });



    };
  
    $scope.Data12 = [];
    $scope.$watch('dt', function () {
        $scope.DoneCreadentialing1 = [];

        $scope.NoDataFound1 = true;
       
        $scope.dt1 = $scope.convert($scope.dt);
        FiltteredList.getFiltteredList().then(function (data) {
            $scope.data = data;
            for (var i = 0; i < data.length; i++) {
                $scope.DoneCreadentialing1.push({
                    ProviderID: data[i].CredentialingLogs[0].CredentialingAppointmentDetail.CredentialingAppointmentDetailID,
                    FirstName: data[i].Profile.PersonalDetail.FirstName,
                    LastName: data[i].Profile.PersonalDetail.LastName,
                    ProviderTitles: angular.copy(data[i].Profile.PersonalDetail.ProviderTitles),
                    Specialities: angular.copy(data[i].CredentialingLogs[0].CredentialingAppointmentDetail.CredentialingSpecialityLists),
                    CredentialingDate: $scope.ConvertDateTo(data[i].InitiationDate),
                    Plan: data[i].Plan.PlanName,
                    RecommendedLevel: data[i].CredentialingLogs[0].CredentialingAppointmentDetail.RecommendedLevel,
                    AppointmentDate: $scope.ConvertDateTo(data[i].CredentialingLogs[0].CredentialingAppointmentDetail.CredentialingAppointmentSchedule.AppointmentDate),
                    Status: "Verified"
                });
            }

            for (var i = 0; i < $scope.DoneCreadentialing1.length; i++) {
                if (($scope.dt1 == $scope.DoneCreadentialing1[i].AppointmentDate && $scope.PlanNames == '') || ($scope.dt1 == $scope.DoneCreadentialing1[i].AppointmentDate && $scope.DoneCreadentialing1[i].Plan == $scope.PlanNames)) {
                    $scope.NoDataFound1 = false;
                }
            }
            var selectCount = 0
            for (var i = 0; i < $scope.DoneCreadentialing1.length; i++) {

                if ($scope.dt1 == $scope.DoneCreadentialing1[i].AppointmentDate && ($scope.PlanNames == $scope.DoneCreadentialing1[i].Plan || $scope.PlanNames == '')) {
                    selectCount++;
                }
            }
            if (selectCount == 0) {
                $scope.NoDataFound1 = true;
            }
            else {
                $scope.NoDataFound1 = false;
            }

        },
        function () {
        });

    });

    $scope.$watch('PlanNames', function () {
        $scope.DoneCreadentialing2 = [];

        $scope.NoDataFound1 = true;
        FiltteredList.getFiltteredList().then(function (data) {
            $scope.data = data;
            for (var i = 0; i < data.length; i++) {
                $scope.DoneCreadentialing2.push({
                    ProviderID: data[i].CredentialingLogs[0].CredentialingAppointmentDetail.CredentialingAppointmentDetailID,
                    FirstName: data[i].Profile.PersonalDetail.FirstName,
                    LastName: data[i].Profile.PersonalDetail.LastName,
                    ProviderTitles: angular.copy(data[i].Profile.PersonalDetail.ProviderTitles),
                    Specialities: angular.copy(data[i].CredentialingLogs[0].CredentialingAppointmentDetail.CredentialingSpecialityLists),
                    CredentialingDate: $scope.ConvertDateTo(data[i].InitiationDate),
                    Plan: data[i].Plan.PlanName,
                    RecommendedLevel: data[i].CredentialingLogs[0].CredentialingAppointmentDetail.RecommendedLevel,
                    AppointmentDate: $scope.ConvertDateTo(data[i].CredentialingLogs[0].CredentialingAppointmentDetail.CredentialingAppointmentSchedule.AppointmentDate),
                    Status: "Verified"
                });
            }
            for (var i = 0; i < $scope.DoneCreadentialing2.length; i++) {
                if (($scope.dt1 == $scope.DoneCreadentialing2[i].AppointmentDate && $scope.PlanNames == '') || ($scope.dt1 == $scope.DoneCreadentialing2[i].AppointmentDate && $scope.DoneCreadentialing2[i].Plan == $scope.PlanNames)) {
                    $scope.NoDataFound1 = false;
                }
            }
            var selectCount = 0
            for (var i = 0; i < $scope.DoneCreadentialing2.length; i++) {

                if ($scope.dt1 == $scope.DoneCreadentialing2[i].AppointmentDate && ($scope.PlanNames == $scope.DoneCreadentialing2[i].Plan || $scope.PlanNames == '')) {
                    selectCount++;
                }
            }
            if (selectCount == 0) {
                $scope.NoDataFound1 = true;
            }
            else {
                $scope.NoDataFound1 = false;
            }
        },
        function () {
          
        });

    });


    $scope.RemoveAppoinment = function (index, cred) {
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
        cred.statusForApp = false;
        $scope.CreadentialingData.push(angular.copy(cred));

        $scope.SelectedDetailsRemoveID = $scope.DoneCreadentialing[index].ProviderID;
        $scope.ProfileDataID = $scope.DoneCreadentialing[index].ProfileID;
        $scope.AppDate=$scope.DoneCreadentialing[index].AppointmentDate;
        $scope.DoneCreadentialing.splice(index, 1);
        var selectCount = 0
        for (var i = 0; i < $scope.DoneCreadentialing.length; i++) {

            if ($scope.dt1 == $scope.DoneCreadentialing[i].AppointmentDate && ($scope.PlanNames == $scope.DoneCreadentialing[i].Plan || $scope.PlanNames == '')) {
                selectCount++;
            }
        }
        if (selectCount == 0) {
            $scope.NoDataFound1 = true;
        }
        else {
            $scope.NoDataFound1 = false;
        }
        $scope.SelectedDetailsID.splice(index, 1);
        $scope.SelectedDetails = $scope.resetSelection($scope.CreadentialingData);

        var obj = {
            ProviderID: $scope.SelectedDetailsRemoveID,
            ProfileID: $scope.ProfileDataID,
            AppointmentDate: $scope.AppDate
        }
        $http.post(rootDir + '/Credentialing/CnD/RemoveAppointment', obj).success(function (data, status, headers, config) {
            //----------- success message -----------

        }).error(function (data, status, headers, config) {

        });

        $scope.checkLength($scope.lastplanvalue);
    };
    $scope.CCOAppointDataData = [];
    $scope.GetCredentialInfoList = function () {

        $http.get(rootDir + '/Credentialing/CnD/GetAllCredentialInfoList').
        success(function (data, status, headers, config) {
            try {

                $scope.CCOAppointDataData[0] = {};
                $scope.CCOAppointDataData[0].CredentialingList = [];
                for (var i = 0; i < data.length; i++) {
                    $scope.CCOAppointDataData[0].CredentialingList.push({
                        CredentialingInfoID: data[i].CredentialingInfoID,
                        ProviderID: data[i].CredentialingLogs[0].CredentialingAppointmentDetail.CredentialingAppointmentDetailID,
                        FirstName: data[i].Profile.PersonalDetail.FirstName,
                        LastName: data[i].Profile.PersonalDetail.LastName,
                        ProviderTitles: angular.copy(data[i].Profile.PersonalDetail.ProviderTitles),
                        Specialities: angular.copy(data[i].CredentialingLogs[0].CredentialingAppointmentDetail.CredentialingSpecialityLists),
                        CredentialingDate: $scope.ConvertDateTo(data[i].InitiationDate),
                        Plan: data[i].Plan.PlanName,
                        RecommendedLevel: data[i].CredentialingLogs[0].CredentialingAppointmentDetail.RecommendedLevel,
                        Status: "Verified"
                    });
                }
                $scope.CCOAppointDataData[0].ActionID = 1;
                $scope.getDataByMenu($scope.CCOAppointDataData[0]);
            } catch (e) {
               
            }
          
        }).
        error(function (data, status, headers, config) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
        });
    };


    $scope.GetCredentialInfoList();
    

    $scope.ConvertDateTo = function (value) {
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

    $scope.init_table = function (data) {
        $scope.data = data;
       
        var counts = [];

        if ($scope.data.length <= 5) {
            counts = [];
        }
        else if ($scope.data.length <= 10) {
            counts = [5, 10];
        }
        else if ($scope.data.length <= 20) {
            counts = [5, 10, 20];
        }
        else if ($scope.data.length <= 40) {
            counts = [5, 10, 20, 40];
        }
        else if ($scope.data.length > 40) {
            counts = [5, 10, 20, 40];
        }

        $scope.tableParams1 = new ngTableParams({
            page: 1,            // show first page
            count: 5,          // count per page
            filter: {
                //name: 'M'       // initial filter
                //FirstName : ''
            },
            sorting: {

                InitiationDate: 'desc'
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
    }

});
