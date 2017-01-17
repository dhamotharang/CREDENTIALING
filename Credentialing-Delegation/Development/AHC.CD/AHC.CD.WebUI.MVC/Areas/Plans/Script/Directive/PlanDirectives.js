/// <reference path="../../Views/PlanList/PlanModal.html" />
/// <reference path="../../Views/PlanList/PlanModal.html" />


PlanApp.directive('stRatio', function () {
    return {
        link: function (scope, element, attr) {
            var ratio = +(attr.stRatio);
            element.css('width', ratio + '%');
        }
    };
});

PlanApp.directive('pageSelect', function () {
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

PlanApp.directive('saving', function () {
    return {
        restrict: 'E',
        replace: true,
        priority: 1,
        scope: {
            ngModel: '=',
            loading:'='
        },
        template: '<div ng-show="loading" class="loading"><img src="/Content/Images/ajax-loader.gif" width="20" height="20" />Saving...</div>',
        link: function (scope, element, attr) {
            
        }
    }
});

PlanApp.directive('planModal', ["$http", "$compile", "$templateRequest", function ($http, $compile, $templateRequest) {
    return {
        replace: true,
        priority:2,
        restrict: 'EA',
        transclude: true,
        scope: {
            ngModel: '=',
            remove: '&',
            saving:'='
        },
        link: function (scope, el, attrs) {
            //scope.saving = attrs.savingStatus;
            scope.modalId = attrs.modalId;
            scope.ConfirmTitle = attrs.modalTitle;
            scope.modalSrc = attrs.modalSrc;

            scope.ConfirmMessage = attrs.modalMessage;
            $templateRequest(rootDir + scope.modalSrc).then(function (html) {
                var template = angular.element(html);
                el.append(template);
                $compile(template)(scope);
            });
        }
    }
}]);

PlanApp.directive('ngFiles', ['$parse', function ($parse) {
    return {
        link: function (scope, element, attrs) {
            var onChange = $parse(attrs.ngFiles);
            element.on('change', function (event) {
                onChange(scope, { $files: event.target.files });
            });
        }
    }
}]);

PlanApp.directive('ajaxLoading', ["$compile", "$templateRequest", function ($compile, $templateRequest) {
    return {
        replace: true,
        restrict: 'E',
        transclude: true,
        scope: {
            ngModel: '=',
        },
        link: function (scope, el, attrs) {
            scope.loadContent = attrs.loadContent;
            scope.loadingSrc = attrs.loadingSrc;
            $templateRequest(rootDir + scope.loadingSrc).then(function (html) {
                var template = angular.element(html);
                el.append(template);
                $compile(template)(scope);
            });
        }
    }
}]);

PlanApp.directive('toolTip', function () {
    return {
        restrict: 'A',
        link: function (scope, elem) {
            elem.tooltip();
        }
    }
});

PlanApp.directive('scrollIf', function () {
    return function (scope, element, attrs) {
        scope.$watch(attrs.scrollIf, function (value) {
            if (value!="") {
                $('html, body').animate({
                    scrollTop: $("#" + value).offset().top
                }, 1000);
            }
        });
    }
});

PlanApp.directive("planFileModel", ["$parse",function ($parse) {
    return {
        restrict: "A",
        link: function (scope, element, attrs) {
            var model = $parse(attrs.akFileModel);
            var modelSetter = model.assign;
            element.bind("change", function () {
                scope.$apply(function () {
                    modelSetter(scope, element[0].files[0]);
                });
            });
        }
    };
}]);





