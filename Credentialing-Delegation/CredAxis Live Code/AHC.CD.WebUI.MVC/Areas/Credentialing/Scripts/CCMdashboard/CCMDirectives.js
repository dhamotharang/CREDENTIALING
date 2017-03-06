CCMDashboard.directive('biscuitTile', ["$http", "$compile", "$templateRequest", function ($http, $compile, $templateRequest) {
    return {
        replace: true,
        restrict: 'EA',
        transclude: true,
        scope: {
            ngModel: '=',
            gridData: '&'
        },
        link: function (scope, el, attrs) {
            scope.biscuitId = attrs.biscuitId;
            scope.biscuitTitle = attrs.biscuitTitle;
            scope.biscuitColor = attrs.biscuitColor;
            scope.biscuitSymbol = attrs.biscuitSymbol;
            scope.biscuitValue = attrs.biscuitValue;
            scope.actionType = attrs.actionType;
            $templateRequest(rootDir + "/CustomHTML/Biscuit.html").then(function (html) {
                var template = angular.element(html);
                el.append(template);
                $compile(template)(scope);
            });
        }
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
