AuditDashboard.directive('biscuitTile', ["$http", "$compile", "$templateRequest", function ($http, $compile, $templateRequest) {
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
            $templateRequest(rootDir + "/CustomHTML/BiscuitTile.html").then(function (html) {
                var template = angular.element(html);
                el.append(template);
                $compile(template)(scope);
            });
        }
    }
}]);


