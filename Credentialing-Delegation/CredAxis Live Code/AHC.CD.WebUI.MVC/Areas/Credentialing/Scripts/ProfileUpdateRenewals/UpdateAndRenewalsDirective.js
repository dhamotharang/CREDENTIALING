UpdateAndRenewalsApp.directive('biscuitTile', ["$http", "$compile", "$templateRequest", function ($http, $compile, $templateRequest) {
    return {
        replace: true,
        restrict: 'EA',
        transclude: true,
        scope: {
            ngModel: '=',
            gridData: '&',
            biscuitValue: '='
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

UpdateAndRenewalsApp.directive('tooltipApp', function ($compile, $sce) {
    return {
        restrict: 'A',
        scope: {
            content: '=tooltipContent'
        },
        link: function (scope, element, attrs) {
            scope.displayTooltip = false;
            scope.updateTootipPosition = function (top, left) {
                tooltip.css({
                    top: top + 'px',
                    left: left + 'px',
                });
            };
            scope.getSafeContent = function (content) {
                return $sce.trustAsHtml(content);
            };
            var tooltip = angular.element(
                '<div ng-show="displayTooltip" style="position:absolute;z-index:500;border:1px solid;height:100px;width:100px">\
        	    <span>{{content | json}}</span>\
                </div>'
            );
            angular.element(document.querySelector('body')).append(tooltip);
            element.on('mouseenter', function (event) {
                scope.displayTooltip = true;
                scope.$digest();
            });
            element.on('mousemove', function (event) {
                scope.updateTootipPosition(event.clientY + 15, event.clientX + 15);
            });
            element.on('mouseleave', function () {
                scope.displayTooltip = false;
                scope.$digest();
            });
            $compile(tooltip)(scope);
        }
    };

});

UpdateAndRenewalsApp.directive('draggable', ['$document', function ($document) {
    return {
        restrict: 'A',
        link: function (scope, elm, attrs) {
            var startX, startY, initialMouseX, initialMouseY;
            elm.css({ position: 'absolute' });

            elm.bind('mousedown', function ($event) {
                startX = elm.prop('offsetLeft');
                startY = elm.prop('offsetTop');
                initialMouseX = $event.clientX;
                initialMouseY = $event.clientY;
                $document.bind('mousemove', mousemove);
                $document.bind('mouseup', mouseup);
                return false;
            });

            function mousemove($event) {
                var dx = $event.clientX - initialMouseX;
                var dy = $event.clientY - initialMouseY;
                elm.css({
                    top: startY + dy + 'px',
                    left: startX + dx + 'px'
                });
                return false;
            }

            function mouseup() {
                $document.unbind('mousemove', mousemove);
                $document.unbind('mouseup', mouseup);
            }
        }
    };
}]);

UpdateAndRenewalsApp.directive('resizer', function ($document) {
    return function($scope, $element, $attrs) {
        $element.on('mousedown', function(event) {
            event.preventDefault();
            $document.on('mousemove', mousemove);
            $document.on('mouseup', mouseup);
        });
        function mousemove(event) {
            if ($attrs.resizer == 'vertical') {
                // Handle vertical resizer
                var x = event.pageX;
                if ($attrs.resizerMax && x > $attrs.resizerMax) {
                    x = parseInt($attrs.resizerMax);
                }
                if ($attrs.resizerMax && x < $attrs.resizerMin) {
                    x = parseInt($attrs.resizerMin);
                }
                $element.css({
                    left: x + 'px'
                });
                document.getElementById($attrs.resizerLeft.substring(1)).style.width = x + 'px'
                document.getElementById($attrs.resizerRight.substring(1)).style.left = (x + parseInt($attrs.resizerWidth)) + 'px'
            } else {
                // Handle horizontal resizer
                var y = window.innerHeight - event.pageY;
                $element.css({
                    bottom: y + 'px'
                });
                document.getElementById($attrs.resizerTop.substring(1)).style.bottom = y + 'px'
                document.getElementById($attrs.resizerBottom.substring(1)).style.height = (y + parseInt($attrs.resizerHeight)) + 'px'
            }
        }
        function mouseup() {
            $document.unbind('mousemove', mousemove);
            $document.unbind('mouseup', mouseup);
        }
    };
});