CCMDashboard.directive('biscuitTile', ["$http", "$compile", "$templateRequest", "$rootScope", function ($http, $compile, $templateRequest, $rootScope) {
    return {
        replace: true,
        restrict: 'EA',
        priority: 2,
        transclude: true,
        scope: {
            ngModel: '=',
            gridData: '&',
            biscuitValue: '='
        },
        link: function (scope, element, attrs, ngModel) {
            scope.biscuitId = attrs.biscuitId;
            scope.biscuitTitle = attrs.biscuitTitle;
            scope.biscuitColor = attrs.biscuitColor;
            scope.biscuitSymbol = attrs.biscuitSymbol;
            scope.biscuitValue = attrs.biscuitValue;
            scope.actionType = attrs.actionType;
            $templateRequest(rootDir + "/CustomHTML/Biscuit.html").then(function (html) {
                var template = angular.element(html);
                element.append(template);
                $compile(template)(scope);
            });
            element.bind('click', function () {
                $rootScope.VisibilityControl = '';
                $rootScope.TempObjectForStatus.SingleDetailedApprovalAction = false;
                $("#CCMGrid").find('.ViewDetail.hidden').siblings('.CloseDetail').addClass('hidden');
                $("#CCMGrid").find('.ViewDetail.hidden').removeClass('hidden');
                //ngModel.$setViewValue($rootScope.tableCaption);
                scope.ngModel = attrs.actionType;
                scope.$apply();
            })
        }
    }
}]);
CCMDashboard.directive('getSiblings', ["$http", "$compile", "$rootScope", function ($http, $compile, $rootScope) {
    return {
        require: '?ngModel',
        scope: {
            key: '='
        },
        link: function (scope, element, attrs) {
            element.bind('click', function () {
                if (element.hasClass('ViewDetail')) {
                    element.parents('table').find('.ViewDetail').removeClass('hidden');
                    element.parents('table').find('.CloseDetail').addClass('hidden');
                    element.siblings('.CloseDetail').removeClass('hidden');
                    element.addClass('hidden');
                    $rootScope.VisibilityControl = scope.key;
                    $rootScope.TempObjectForStatus.SingleDetailedApprovalAction = true;
                    scope.$apply();
                }
                if (element.hasClass('CloseDetail')) {
                    $rootScope.VisibilityControl = '';
                    $rootScope.TempObjectForStatus.SingleDetailedApprovalAction = false;
                    $("#CCMGrid").find('.ViewDetail.hidden').siblings('.CloseDetail').addClass('hidden');
                    $("#CCMGrid").find('.ViewDetail.hidden').removeClass('hidden');
                    scope.$apply();
                }
                if (element.hasClass('ApprovalSubmission')) {
                    element.parents('table').find('.ViewDetail').removeClass('hidden');
                    element.parents('table').find('.CloseDetail').addClass('hidden');
                    //ngModel.$setViewValue("");
                    $rootScope.VisibilityControl = "";
                    scope.$apply();
                }
            })
        },
    }
}]);
CCMDashboard.directive('resetSpa', ["$http", "$compile", "$rootScope", function ($http, $compile, $rootScope) {
    return {
        require: '?ngModel',
        priority: 1,
        scope: {
            ngModel: '=',
            key: '='
        },
        link: function (scope, element, attrs, ngModel) {
            element.bind('click', function () {
                $rootScope.VisibilityControl = '';
                $rootScope.TempObjectForStatus.SingleDetailedApprovalAction = false;
                $("#CCMGrid").find('.ViewDetail.hidden').siblings('.CloseDetail').addClass('hidden');
                $("#CCMGrid").find('.ViewDetail.hidden').removeClass('hidden');
                //ngModel.$setViewValue($rootScope.tableCaption);
                //$rootScope.tableCaption = scope.key;
                scope.ngModel = scope.key;
                scope.$apply();
            })
        },
    }
}]);
CCMDashboard.directive('selectRow', ["$rootScope", "$http", "$compile", "CCMDashboardFactory", "$filter", function ($rootScope, $http, $compile, CCMDashboardFactory, $filter) {
    return {
        require: '?ngModel',
        scope: {
            object: '='
        },
        link: function (scope, element, attrs, ngModel, $scope) {
            element.bind('click', function () {
                scope.object;
                if ((scope.object.Status == 'Pending') && !$rootScope.TempObjectForStatus.SingleDetailedApprovalAction) {
                    if ($filter('filter')($rootScope.TempCCMAppointments, { SelectStatus: true, Status: 'New' }).length == 0) {
                        $rootScope.TempCCMAppointments[$rootScope.TempCCMAppointments.indexOf(scope.object)].SelectStatus = !scope.object.SelectStatus;
                        if ($filter('filter')($rootScope.TempCCMAppointments, { SelectStatus: true }).length > 0) {
                            $rootScope.TempObjectForStatus.QuickApprovalAction = true;
                        } else {
                            $rootScope.TempObjectForStatus.QuickApprovalAction = false;
                        }
                    }
                }
                if ((scope.object.Status == 'New') && !$rootScope.TempObjectForStatus.SingleDetailedApprovalAction) {
                    if ($filter('filter')($rootScope.TempCCMAppointments, { SelectStatus: true, Status: 'Pending' }).length == 0) {
                        $rootScope.TempCCMAppointments[$rootScope.TempCCMAppointments.indexOf(scope.object)].SelectStatus = !scope.object.SelectStatus;
                        if ($filter('filter')($rootScope.TempCCMAppointments, { SelectStatus: true }).length > 0) {
                            $rootScope.TempObjectForStatus.QuickApprovalAction = true;
                        } else {
                            $rootScope.TempObjectForStatus.QuickApprovalAction = false;
                        }
                    }
                }
                scope.$apply();
            })
        },
    }
}]);
CCMDashboard.directive('pageSelect', function () {
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
})