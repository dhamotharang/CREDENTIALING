var managerUserApp = angular.module("managerUserApp", ['ngAnimate', 'toaster', 'smart-table', 'ui.bootstrap', 'mgcrea.ngStrap']);

managerUserApp.run(function ($rootScope) {
    $rootScope.userData = [];
});


managerUserApp.directive('pageSelect', function () {
    return {
        restrict: 'E',
        template: '<input type="text" class="select-page" ng-model="inputPage" ng-change="PageResize(inputPage,numPages)">',
        controller: function ($scope) {
            $scope.$watch('inputPage', function (newV, oldV) {
                if (newV === oldV) {
                    return;
                }
                else if (newV >= oldV) {
                    $scope.inputPage = newV;
                    $scope.selectPage(newV);
                    //$scope.currentPage = newV;
                }
                else {
                    $scope.selectPage(newV);
                }

            });
            $scope.PageResize = function (currentPage, maxPage) {
                if (currentPage >= maxPage) {
                    $scope.inputPage = maxPage;
                    $scope.selectPage(maxPage);
                }
                else {
                    $scope.selectPage(currentPage);
                }
            }
        },
        link: function (scope, element, attrs) {
            scope.$watch('currentPage', function (c) {
                scope.inputPage = c;
            });
        }
    }
});

managerUserApp.factory('Resource', ['$q', '$rootScope', '$filter', '$timeout', function ($q, $rootScope, $filter, $timeout) {

    function getPage(start, number, params) {

        var deferred = $q.defer();

        $rootScope.filtered = params.search.predicateObject ? $filter('filter')($rootScope.userData, params.search.predicateObject) : $rootScope.userData;

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


//============================= Author : Tulasidhar ==========================================
managerUserApp.controller("ManageUserController", ['$scope', '$timeout', '$http', '$filter', '$rootScope', 'toaster', 'Resource', function ($scope, $timeout, $http, $filter, $rootScope, toaster, Resource) {
    $scope.ApplicationUserObj = [];
    $scope.Users = [];
    var ctrl = this;
    this.displayed = [];
    var pipecall = 0;

    this.callServer = function callServer(tableState) {
        ctrl.isLoading = true;
        var pagination = tableState.pagination ? tableState.pagination : null;
        var start = pagination.start || 0;
        var number = pagination.number || 5;
        $scope.t = tableState;
        Resource.getPage(start, number, tableState).then(function (result) {
            ctrl.displayed = result.data;
            ctrl.temp = ctrl.displayed;
            tableState.pagination.numberOfPages = result.numberOfPages;//set the number of pages so the pagination can update
            ctrl.isLoading = false;
        });
    };

    $scope.Getlist = function () {
        var promise = $scope.Getlist1().then(function () {
            $scope.ApplicationUsers().then(function () {
                for (var i in $scope.mydata) {
                    var email = "";
                    for (var j in $scope.ApplicationUserObj) {
                        if ($scope.mydata[i].AuthId == $scope.ApplicationUserObj[j].Id) {
                            email = $scope.ApplicationUserObj[j].Email;
                            $scope.Users.push({
                                Email: email,
                                Name: $scope.ApplicationUserObj[j].FullName == null ? "" : $scope.ApplicationUserObj[j].FullName,
                                status: $scope.mydata[i].Status,
                                Id: $scope.mydata[i].AuthId,
                                Roles : $scope.constructRoles($scope.mydata[i].Roles)
                            });
                            break;
                        }
                    }
                }
                $scope.showLoading = false;
                $rootScope.userData = angular.copy($scope.Users);
                ctrl.callServer($scope.t);
                ctrl.temp = $rootScope.userData;
            })
        });
    }
    $scope.constructRoles = function(RolesArray)
    {
        var Roles = "";
        jQuery.grep(RolesArray,function(ele,index){ Roles +=  index !=0 ? ", " + ele : ele; });
        return Roles;
    }
    $scope.$on('updateUsers', function (event, val) {
        $scope.Users = angular.copy(val);
        $scope.UsersPagination = angular.copy($scope.userData);
        $scope.userData = angular.copy($scope.Users);
        ctrl.callServer($scope.t);
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
               for (var j in $scope.userData) {
                   if (user.Id == $scope.userData[j].Id) {
                       $scope.userData[j].status = "Active";
                   }
               }
               toaster.pop('Success', "Success", 'User Activated successfully');
               ctrl.callServer();
           } else {
               toaster.pop('error', "", 'Error occured while Activating User.. Please try after sometime');
           }
       })
       .error(function (data, status, headers, config) {
           $scope.showLoading = false;
           toaster.pop('error', "", 'Error occured while Activating User.. Please try after sometime');
       });
    }

    $scope.DeactivateUser = function (user, index) {
        $http.get(rootDir + '/Initiation/InitiateUser/DeactivateUser', { params: { "AuthenticateUserId": user.Id } })
       .success(function (data, status, headers, config) {
           if (data.status == 'true') {
               for (var j in $scope.userData) {
                   if (user.Id == $scope.userData[j].Id) {
                       $scope.userData[j].status = "Inactive";
                   }
               }
               toaster.pop('Success', "Success", 'User Deactivated successfully');
               ctrl.callServer();
           } else {
               toaster.pop('error', "", 'Error occured while changing User.. Please try after sometime');
           }
       })
       .error(function (data, status, headers, config) {
           $scope.showLoading = false;
           toaster.pop('error', "", 'Error occured while Deactivating User.. Please try after sometime');
       });
    }
}]);