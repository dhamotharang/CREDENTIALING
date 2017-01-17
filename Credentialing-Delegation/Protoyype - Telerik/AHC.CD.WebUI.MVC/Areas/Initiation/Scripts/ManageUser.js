var managerUserApp = angular.module("managerUserApp", ['ngAnimate', 'toaster', 'ui.bootstrap', 'mgcrea.ngStrap']);

//============================= Author : Tulasidhar ==========================================
managerUserApp.controller("ManageUserController", ['$scope', '$timeout', '$http', '$filter', '$rootScope', 'toaster', function ($scope, $timeout, $http, $filter, $rootScope, toaster) {
    $scope.ApplicationUserObj = [];
    $scope.Users = [];
    $scope.Getlist = function () {
        var promise = $scope.Getlist1().then(function () {
            $scope.ApplicationUsers().then(function () {
                console.log($scope.ApplicationUserObj);
                console.log($scope.mydata);
                for (var i in $scope.mydata) {
                    var email = "";
                    for (var j in $scope.ApplicationUserObj) {
                        if ($scope.mydata[i].AuthId == $scope.ApplicationUserObj[j].Id) {
                            email = $scope.ApplicationUserObj[j].Email;
                            $scope.Users.push({
                                Email: email,
                                Name: $scope.ApplicationUserObj[j].FullName == null ? "" : $scope.ApplicationUserObj[j].FullName,
                                status: $scope.mydata[i].Status,
                                Id: $scope.mydata[i].AuthId
                            });
                            break;
                        }
                    }
                }
                $scope.showLoading = false;
                if ($scope.Users.length > 9) {
                    for (i = 0; i < 10; i++) {
                        $scope.UsersPagination[i] = $scope.Users[i];
                    }
                    $scope.bigTotalItems = $scope.Users.length;

                    $scope.CurrentPage = [];
                    $scope.bigCurrentPage = 1;

                    var startIndex = ($scope.bigCurrentPage - 1) * 10;
                    var endIndex = startIndex + 9;

                    for (startIndex; startIndex <= endIndex ; startIndex++) {
                        if ($scope.Users[startIndex]) {
                            $scope.CurrentPage.push($scope.Users[startIndex]);
                        } else {
                            break;
                        }
                    }
                }

                else {
                    $scope.UsersPagination = angular.copy($scope.mydata);
                    $scope.bigTotalItems = $scope.mydata.length;
                }
            })
        });
    }

    $scope.$on('updateUsers', function (event, val) {
        $scope.Users = angular.copy(val);
        if ($scope.Users.length > 9) {
            for (i = 0; i < 10; i++) {
                $scope.UsersPagination[i] = $scope.Users[i];
            }
            $scope.bigTotalItems = $scope.Users.length;

            $scope.CurrentPage = [];
            $scope.bigCurrentPage = 1;

            var startIndex = ($scope.bigCurrentPage - 1) * 10;
            var endIndex = startIndex + 9;

            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.Users[startIndex]) {
                    $scope.CurrentPage.push($scope.Users[startIndex]);
                } else {
                    break;
                }
            }
        }

        else {
            $scope.UsersPagination = angular.copy($scope.mydata);
            $scope.bigTotalItems = $scope.mydata.length;
        }
    })

    $scope.ApplicationUsers = function () {
        var d1 = new $.Deferred();
        $scope.showLoading = true;
        $http.get(rootDir + '/Initiation/InitiateUser/GetAllApplicationUsers')
       .success(function (data, status, headers, config) {
           $scope.ApplicationUserObj = angular.copy(data);
           d1.resolve(data);
       }).
        error(function (data, status, headers, config) {
            $scope.showLoading = false;
            return d1.promise();
        });
        return d1.promise();
    }

    $scope.Getlist1 = function () {
        var d = new $.Deferred();
        $scope.showLoading = true;
        $http.get(rootDir + '/Initiation/InitiateUser/GetAllUserList')
       .success(function (data, status, headers, config) {
           $scope.mydata = angular.copy(data);
           $scope.showLoading = false;
           d.resolve(data);
       }).
        error(function (data, status, headers, config) {
            $scope.showLoading = false;
            return d.promise();
        });
        return d.promise();
    }

    $scope.ActivateUser = function (user, index) {
        $http.get(rootDir + '/Initiation/InitiateUser/ActivateUser', { params: { "AuthenticateUserId": user.Id } })
       .success(function (data, status, headers, config) {
           if (data.status == 'true') {
               $scope.UsersPagination[index].status = "Active";
               for (var i in $scope.Users) {
                   if ($scope.UsersPagination[index].Id == $scope.Users[i].Id) {
                       $scope.Users[i].status = "Active";
                   }
               }
               //$scope.$broadcast('updateUsers', $scope.Users);
               toaster.pop('Success', "Success", 'User Activated successfully');
           } else {
               toaster.pop('error', "", 'Error occured while Activating Role.. Please try after sometime');
           }
       })
       .error(function (data, status, headers, config) {
           $scope.showLoading = false;
       });
    }

    $scope.DeactivateUser = function (user, index) {
        $http.get(rootDir + '/Initiation/InitiateUser/DeactivateUser', { params: { "AuthenticateUserId": user.Id } })
       .success(function (data, status, headers, config) {
           if (data.status == 'true') {
               $scope.UsersPagination[index].status = "Inactive";
               for (var i in $scope.Users) {
                   if($scope.UsersPagination[index].Id == $scope.Users[i].Id)
                   {
                       $scope.Users[i].status = "Inactive";
                   }
               }
              // $scope.$broadcast('updateUsers', $scope.Users);
               toaster.pop('Success', "Success", 'User Deactivated successfully');
           } else {
               toaster.pop('error', "", 'Error occured while changing Role.. Please try after sometime');
           }
       })
       .error(function (data, status, headers, config) {
           $scope.showLoading = false;
           toaster.pop('error', "", 'Error occured while Deactivating Role.. Please try after sometime');
       });
    }

    $scope.CurrentPage = [];
    $scope.maxSize = 5;
    $scope.bigTotalItems;// = $scope.mydata.length;
    $scope.bigCurrentPage = 1;

    //-------------------- page change action ---------------------
    $scope.pageChanged = function (pagnumber) {
        $scope.bigCurrentPage = pagnumber;
    };

    //-------------- current page change Scope Watch ---------------------
    $scope.$watch('bigCurrentPage', function (newValue, oldValue) {
        $scope.UsersPagination = [];
        $scope.CurrentPage = [];
        var startIndex = (newValue - 1) * 10;
        var endIndex = startIndex + 9;
        if ($scope.Users) {
            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.Users[startIndex]) {
                    $scope.CurrentPage.push($scope.Users[startIndex]);
                    $scope.UsersPagination.push($scope.Users[startIndex]);
                } else {
                    break;
                }
            }
        }
    });
}]);