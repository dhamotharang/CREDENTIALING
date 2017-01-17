// -------------------- CCM controller Angular Module-------------------------------
//---------- Author : KRGLV -------------

//------------------------- angular module ----------------------------
var CCMApp = angular.module('CCMApp', ['ngTable']);

//------------- angular tool tip recall directive ---------------------------
CCMApp.directive('tooltip', function () {
    return function (scope, elem) {
        elem.tooltip();
    };
});



CCMApp.service('FiltteredList', function ($http, $q) {
    this.getFiltteredList = function () {
        var deferred = $q.defer();
        $http({
            method: 'GET',
            url: rootDir + '/Credentialing/CCM/GetAllCredentialingFilterList',
        }).
         success(function (data, status, headers, config) {
             
             deferred.resolve(data);
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
CCMApp.run(['$rootScope', function ($rootScope) {
    //----------------- filter day left ranges --------------------
    //$rootScope.days = [5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 70, 80, 90, 180];
}]);



CCMApp.controller("CCMController", function ($scope, $filter, $http, ngTableParams, FiltteredList, $timeout) {
    //-------------- CCM Action Item Lists -----------------

    $scope.CCOAppointDataData = [];
    $scope.loadingAjax = true;
    $scope.initData = function () {
        
       
        FiltteredList.getFiltteredList().then(function (data) {

            $scope.CCOAppointDataData[0] = {};
            $scope.CCOAppointDataData[0].CredentialingList = [];
            for (var i = 0; i < data.length; i++) {
                $scope.CCOAppointDataData[0].CredentialingList.push({
                    ProviderID: data[i].CredentialingInfoID,
                    FirstName: data[i].Profile.PersonalDetail.FirstName,
                    LastName: data[i].Profile.PersonalDetail.LastName,
                    ProviderTitles: angular.copy(data[i].Profile.PersonalDetail.ProviderTitles),
                    Specialities: angular.copy(data[i].CredentialingLogs[0].CredentialingAppointmentDetail.CredentialingSpecialityLists),
                    CredentialingDate: $scope.ConvertDateTo1(data[i].InitiationDate),
                    Plan: data[i].Plan.PlanName,
                    RecommendedLevel: data[i].CredentialingLogs[0].CredentialingAppointmentDetail.RecommendedLevel,
                    AppointmentDate: $scope.ConvertDateTo1(data[i].CredentialingLogs[0].CredentialingAppointmentDetail.CredentialingAppointmentSchedule.AppointmentDate),
                    CredentialingInfoId: data[i].CredentialingInfoID,
                    ProfilePhoto: data[i].Profile.ProfilePhotoPath
                });
            }
            $scope.CCOAppointDataData[0].ActionID = 1;
            $scope.getDataByMenu($scope.CCOAppointDataData[0]);
            $scope.loadingAjax = false;
        },
        function () {

        });

        
    }
    $scope.initData();

    $scope.getDataByMenu = function (data) {
        $scope.selectedAction = angular.copy(data.ActionID);
        $scope.CreadentialingData = angular.copy(data.CredentialingList);
       
        $scope.data = $scope.CreadentialingData;
        $scope.init_table($scope.data);
        $scope.Plan = [];
        for (var i = 0; i < $scope.CreadentialingData.length; i++) {
            $scope.Plan.push($scope.CreadentialingData[i].Plan);
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
        $scope.Plans = angular.copy(squash($scope.Plan));
       
    };

    $scope.PlanNames = '';
    $scope.getValue = function (data) {

        data = data.trimRight();
        $scope.PlanNames = data;
    };


    //Created function to be called when data loaded dynamically
    $scope.init_table = function (data, condition) {

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
                if ($scope.data[i].FirstName == null) {
                    $scope.data[i].Name = "";
                }
                else {
                    $scope.data[i].Name = $scope.data[i].FirstName;
                }
                if ($scope.data[i].LastName == null) {
                    $scope.data[i].Name += "";
                }
                else {
                    $scope.data[i].Name +=" " + $scope.data[i].LastName;
                }
                if ($scope.data[i].ProviderTitles.length == 0) {
                    $scope.data[i].ProviderTitle = "";
                }
                else{
                    for (var j = 0; j < $scope.data[i].ProviderTitles.length; j++) {

                        if (j == 0) {
                            $scope.data[i].ProviderTitle = $scope.data[i].ProviderTitles[j].ProviderType.Title;
                        }
                        else {
                            $scope.data[i].ProviderTitle += "," + $scope.data[i].ProviderTitles[j].ProviderType.Title;
                        }
                    }
                }
                if ($scope.data[i].Specialities.length == 0) {
                    $scope.data[i].Specialty = "";
                }
                else {
                    for (var j = 0; j < $scope.data[i].Specialities.length; j++) {
                        if ($scope.data[i].Specialities[j].Status == 'Active') {
                            if (j == 0) {
                                $scope.data[i].Specialty = $scope.data[i].Specialities[j].Name;
                            }
                            else {
                                
                                    $scope.data[i].Specialty +=","+ $scope.data[i].Specialities[j].Name;
                                
                            }
                        }
                        else {
                            if (j == 0) {
                                $scope.data[i].Specialty = "";
                            }
                            else {
                                $scope.data[i].Specialty += "";
                            }
                        }
                    }
                }
        }
        $scope.tableParams1 = new ngTableParams({
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
            var obj = $scope.tableParams1.$params.filter;
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
                return ($scope.tableParams1.$params.page * $scope.tableParams1.$params.count) - ($scope.tableParams1.$params.count - 1);
            }
        }
        catch (e) { }
    }

    //Get index Last in table
    $scope.getIndexLast = function () {
        try {
            if ($scope.groupBySelected == 'none') {
                return { true: ($scope.data.length), false: ($scope.tableParams1.$params.page * $scope.tableParams1.$params.count) }[(($scope.tableParams1.$params.page * $scope.tableParams1.$params.count) > ($scope.data.length))];
            }
        }
        catch (e) { }
    }



    

    //$scope.ConvertDateTo = function (value) {
    //    var shortDate = null;
    //    if (value) {
    //        var regex = /-?\d+/;
    //        var matches = regex.exec(value);
    //        var dt = new Date(parseInt(matches[0]));
    //        var month = dt.getMonth() + 1;
    //        var monthString = month > 9 ? month : '0' + month;
    //        //var monthName = monthNames[month];
    //        var day = dt.getDate();
    //        var dayString = day > 9 ? day : '0' + day;
    //        var year = dt.getFullYear();
    //        shortDate = dayString + '/' + monthString + '/' + year;
    //        //shortDate = dayString + 'th ' + monthName + ',' + year;
    //    }
    //    return shortDate;
    //};

    $scope.ConvertDateTo1 = function (value) {
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
