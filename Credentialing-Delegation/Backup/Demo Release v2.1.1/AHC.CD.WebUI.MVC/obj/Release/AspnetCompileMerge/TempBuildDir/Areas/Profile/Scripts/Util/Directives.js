//============================ Directive for return Template ===========================
//============================ Dynamic Form Generator ==================================
profileApp.service('dynamicFormGenerateService', function ($compile) {
    this.getForm = function (scope, formContain) {
        return $compile(formContain)(scope);
    };
});

//------------------- tooltip directive for dynamic data change apply function in angular -----------
profileApp.directive('tooltip', function () {
    return function (scope, elem) {
        elem.tooltip();
    };
});

//------------------- popover directive for dynamic data change apply function in angular -----------
profileApp.directive('popover', function () {
    return function (scope, elem) {
        elem.popover();
    };
});

profileApp.directive('samayTimePicker', function () {
    return function (scope, elem) {
        elem.clockface({
            format: 'HH:mm'
        });
    };
});

profileApp.directive('samayToggel', ['$compile', function ($compile) {
    return {
        restrict: 'AE',
        link: function (scope, element, attr) {
            element.bind('click', function (e) {
                e.stopPropagation();
                $(".clockface").hide();
                element.parent().parent().find(".samay").clockface('toggle');
            });
        }
    };
}]);