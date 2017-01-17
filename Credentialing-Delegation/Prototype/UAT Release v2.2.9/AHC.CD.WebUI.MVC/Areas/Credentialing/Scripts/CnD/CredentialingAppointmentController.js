// -------------------- CCM controller Angular Module-------------------------------
//---------- Author : KRGLV -------------

//------------------------- angular module ----------------------------
var CCOApp = angular.module('CCOApp', ['ui.bootstrap']);

//------------- angular tool tip recall directive ---------------------------
CCOApp.directive('tooltip', function () {
    return function (scope, elem) {
        elem.tooltip();
    };
});

CCOApp.service('FiltteredList', function ($http, $q) {
    this.getFiltteredList = function () {
        var deferred = $q.defer();
        $http({
            method: 'GET',
            url: '/Credentialing/CnD/GetAllCredentialInfoListHistory'
        }).
         success(function (data, status, headers, config) {
             //console.log(data);
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

CCOApp.controller("CCOController", function ($scope, $filter, $http, FiltteredList) {
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
                    Status: "Verified"
                });
                $scope.Plan.push(data[i].Plan.PlanName);
            }
            
            $scope.Plans = angular.copy($scope.squash($scope.Plan));
            console.log($scope.Plan);
            console.log($scope.DoneCreadentialing);
        },
        function(){
        });

        //console.log($scope.ftlr.$$state);
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

    //----------------- Filter Data According to Required -----------------------
    $scope.getDataByMenu = function (data) {
        $scope.selectedAction = angular.copy(data.ActionID);
        $scope.CreadentialingData = angular.copy(data.CredentialingList);
        $scope.SelectedDetails = $scope.resetSelection($scope.CreadentialingData);
        console.log($scope.CreadentialingData);

        $scope.initData();
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
                    if ($scope.CreadentialingData[i].Specialities[j].Status=='Active') {
                        if (j == 0) {
                            $scope.CreadentialingData[i].Specialty = $scope.CreadentialingData[i].Specialities[j].Name;
                        }
                        else {
                            $scope.CreadentialingData[i].Specialty += "," + $scope.CreadentialingData[i].Specialities[j].Name;
                        }
                    }
                }
            }
        }
        
        //console.log($scope.Plan);
        $scope.squash=function(arr) {
            var tmp = [];
            for (var i = 0; i < arr.length; i++) {
                if (tmp.indexOf(arr[i]) == -1) {
                    tmp.push(arr[i]);
                }
            }
            return tmp;
        }
        
        $scope.dt = new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate());
        console.log($scope.DoneCreadentialing);


    };
   


    


    $scope.PlanNames = '';
    $scope.getValue = function (data) {
        
        data = data.trimRight();
        $scope.PlanNames = data;
       
    };

    //------------------ init --------------
    $scope.DoneCreadentialing = [];
    $scope.SelectedDetails = [];
    $scope.SelectedDetailsID = [];
    $scope.SelectedDetailsRemoveID=null;
    
    //-------------- selection ---------------
    

    $scope.convert=function(str) {
        var date = new Date(str),
            mnth = ("0" + (date.getMonth() + 1)).slice(-2),
            day = ("0" + date.getDate()).slice(-2);
        return [mnth, day, date.getFullYear()].join("/");
    }
    //console.log($scope.DoneCreadentialing);
    $scope.AddSelectedCredentialing = function () {
        $scope.NoDataFound1 = false;
        var selectedCredentialing = [];
        selectedCredentialing = angular.copy($scope.DoneCreadentialing);
        var NotSeletedCreadentialing = [];
        for (var i in $scope.SelectedDetails) {
            if ($scope.SelectedDetails[i]) {
                $scope.CreadentialingData[i].AppointmentDate= $scope.convert($scope.dt);
                selectedCredentialing.push($scope.CreadentialingData[i]);
                $scope.SelectedDetailsID.push($scope.CreadentialingData[i].ProviderID);
            }
            if (!$scope.SelectedDetails[i]) {
                $scope.CreadentialingData[i].AppointmentDate = "";
                NotSeletedCreadentialing.push(angular.copy($scope.CreadentialingData[i]));
            }
        }
        $scope.DoneCreadentialing=selectedCredentialing;
        $scope.CreadentialingData = NotSeletedCreadentialing;
        $scope.SelectedDetails = $scope.resetSelection($scope.CreadentialingData);
        $scope.SelectedDetailsRemoveID = null;
        
        var obj = {
            ProviderIDArray: $scope.SelectedDetailsID,
            AppointmentDate: $scope.convert($scope.dt)
        }
        $http.post('/Credentialing/CnD/SetAppointment', obj).success(function (data, status, headers, config) {
            //----------- success message -----------
            $scope.SelectedDetailsID.splice(0, $scope.SelectedDetailsID.length);

        }).error(function (data, status, headers, config) {

        });



    };
    $scope.Data12 = [];
    $scope.$watch('dt', function () {
        $scope.DoneCreadentialing1 = [];

        $scope.NoDataFound1 = true;
        console.log($scope.DoneCreadentialing);
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

                if ($scope.dt1==$scope.DoneCreadentialing2[i].AppointmentDate && ($scope.PlanNames == $scope.DoneCreadentialing2[i].Plan || $scope.PlanNames=='')) {
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
                if (cred.Specialities[j].Status=='Active') {
                    if (j == 0) {
                        cred.Specialty = cred.Specialities[j].Name;
                    }
                    else {
                        cred.Specialty += "," + cred.Specialities[j].Name;
                    }
                }
            }
        }
        $scope.CreadentialingData.push(angular.copy(cred));

        $scope.SelectedDetailsRemoveID = $scope.DoneCreadentialing[index].ProviderID;
        $scope.DoneCreadentialing.splice(index, 1);
        var selectCount=0
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
            ProviderID: $scope.SelectedDetailsRemoveID
        }
        $http.post('/Credentialing/CnD/RemoveAppointment', obj).success(function (data, status, headers, config) {
            //----------- success message -----------

        }).error(function (data, status, headers, config) {

        });
    };
    $scope.CCOAppointDataData = [];
    $scope.GetCredentialInfoList = function () {

        $http.get('/Credentialing/CnD/GetAllCredentialInfoList').
        success(function (data, status, headers, config) {
            //console.log(data);
            $scope.CCOAppointDataData[0] = {};
            $scope.CCOAppointDataData[0].CredentialingList = [];
            for (var i = 0; i < data.length; i++) {
                $scope.CCOAppointDataData[0].CredentialingList.push({
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
});
