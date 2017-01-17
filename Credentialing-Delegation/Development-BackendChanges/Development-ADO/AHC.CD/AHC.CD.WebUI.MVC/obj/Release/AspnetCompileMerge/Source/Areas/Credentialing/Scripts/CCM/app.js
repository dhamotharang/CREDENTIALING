// Make sure to include the `ui.router` module as a dependency
angular.module('CnDRouter', ['CnDRouter.applicationForm', 'CnDRouter.Disclosure',
  'ui.router'
])
.run(
  [          '$rootScope', '$state', '$stateParams',
    function ($rootScope,   $state,   $stateParams) {
    $rootScope.$state = $state;
    $rootScope.$stateParams = $stateParams;
    }
  ]
)

.config(
  [          '$stateProvider', '$urlRouterProvider',
    function ($stateProvider,   $urlRouterProvider) {

        $urlRouterProvider

          .when('/c?id', '/application_form/:id')
            .when('/user/:id', '/contacts/:id')
          // If the url is ever invalid, e.g. '/asdf', then redirect to '/' aka the home state
          .otherwise('/');

      $stateProvider
        .state("summary", {
          url: "/",
          templateUrl: "/../../AHCUtil/GetPartial?partialUrl=~/Areas/Credentialing/Views/CCM/_CredentialingCheckList.cshtml"

        })
        
    }
  ]
);
