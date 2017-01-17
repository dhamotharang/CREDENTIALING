var ahcApp = angular.module('ahcApp', []);

sampleApp.config(['$routeProvider',
  function ($routeProvider) {
      $routeProvider.
        when('/', {
            templateUrl: '/Home/About',
            controller: 'createProvider'
        }).
        when('/viewproviders', {
            templateUrl: '/Home/ViewProviders',
            controller: 'viewProviders'
        }).
        otherwise({
            redirectTo: '/'
        });
  }]);