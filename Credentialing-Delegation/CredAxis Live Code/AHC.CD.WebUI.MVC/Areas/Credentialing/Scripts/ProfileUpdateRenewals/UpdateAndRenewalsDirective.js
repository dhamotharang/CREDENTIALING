UpdateAndRenewalsApp.directive('biscuitTile', ["$http", "$compile", "$templateRequest", function ($http, $compile, $templateRequest) {
    return {
        replace: true,
        restrict: 'EA',
        transclude: true,
        scope: {
            ngModel: '=',
            gridData: '&',
            biscuitValue:'='
        },
        link: function (scope, el, attrs) {
            scope.biscuitId = attrs.biscuitId;
            scope.biscuitTitle = attrs.biscuitTitle;
            scope.biscuitColor = attrs.biscuitColor;
            scope.biscuitSymbol = attrs.biscuitSymbol;
            scope.actionType = attrs.actionType;
            $templateRequest(rootDir + "/CustomHTML/Biscuit.html").then(function (html) {
                var template = angular.element(html);
                el.append(template);
                $compile(template)(scope);
            });
        }
    }
}]);

UpdateAndRenewalsApp.directive('pageSelect', function () {
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

UpdateAndRenewalsApp.directive('sglclick', ['$parse', function ($parse) {
    return {
        restrict: 'A',
        link: function (scope, element, attr) {
            var fn = $parse(attr['sglclick']);
            var delay = 200, clicks = 0, timer = null;
            element.on('click', function (event) {
                clicks++;  //count clicks
                if (clicks === 1) {
                    timer = setTimeout(function () {
                        scope.$apply(function () {
                            fn(scope, { $event: event });
                        });
                        clicks = 0;             //after action performed, reset counter
                    }, delay);
                } else {
                    clearTimeout(timer);    //prevent single-click action
                    clicks = 0;             //after action performed, reset counter
                }
            });
        }
    };
}])

UpdateAndRenewalsApp.directive('saving', function () {
    return {
        restrict: 'E',
        replace: true,
        scope: {
            ngModel: '=',
            loading: '='
        },
        template: '<button class="btn btn-default" ng-show="loading" ng-disabled="loading" class="loading"><img src="/Content/Images/ajax-loader.gif" width="20" height="20" />Saving...</button>',
        link: function (scope, element, attr) {

        }
    }
});
