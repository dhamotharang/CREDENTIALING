angular.module('Contractinfo')
    .directive('pageSelect', function () {
        return {
            restrict: 'E',
            template: '<input type="text" class="select-page" ng-model="inputPage" ng-change="PageResize(inputPage,numPages)">',
            controller: function ($scope) {
                $scope.$watch('inputPage', function (newV,oldV) {
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

