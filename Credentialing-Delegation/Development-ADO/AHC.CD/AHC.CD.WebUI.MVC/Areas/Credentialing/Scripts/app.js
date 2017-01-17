/// <reference path="../Views/CCM/_CredentialingCheckList.cshtml" />

// Make sure to include the `ui.router` module as a dependency
angular.module('CnDRouter', ['CnDRouter.applicationForm',
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
          templateUrl: "/../../AHCUtil/GetPartial?partialUrl=~/Areas/Credentialing/Views/CnD/_Summary.cshtml"

        })
        //.state('application_form', {
        //    url: '/application_form',
        //    templateUrl: "/../../AHCUtil/GetPartial?partialUrl=~/Areas/Credentialing/Views/CnD/_ViewForm.cshtml"
        //})
        .state("plan_data", {
            url: "/plan_data",
            templateUrl: "/../../AHCUtil/GetPartial?partialUrl=~/Areas/Credentialing/Views/CnD/_PlanData.cshtml"

        })
        //.state('disclosure_question', {
        //    url: '/disclosure_question',
        //    templateUrl: "/../../AHCUtil/GetPartial?partialUrl=~/Areas/Credentialing/Views/CnD/_DisclosureQuestions.cshtml"
        //})
        .state("documents", {
            url: "/documents",
            templateUrl: "/../../AHCUtil/GetPartial?partialUrl=~/Areas/Credentialing/Views/Shared/_UploadedDocs.cshtml",
            controller: "DocumentController"

        })
        .state('psv', {
            url: '/psv',
            templateUrl: "/../../AHCUtil/GetPartial?partialUrl=~/Areas/Credentialing/Views/Shared/_VerificationReport.cshtml"
        })
        .state("credentialing_action", {
            url: "/CCM_action",
            templateUrl: "/../../AHCUtil/GetPartial?partialUrl=~/Areas/Credentialing/Views/CCM/_CredentialingCheckList.cshtml"

        })
          .state("printChecklist", {
              url: "/printChecklist",
              templateUrl: "/../../AHCUtil/GetPartial?partialUrl=~/Areas/Credentialing/Views/Shared/PrintCheckList.cshtml"

          })
        .state('plan_report', {
            url: '/plan_report',
            templateUrl: "/../../AHCUtil/GetPartial?partialUrl=~/Areas/Credentialing/Views/CnD/_PlanReport.cshtml"
        })
          .state('search', {
              url: '/search',
              templateUrl: "/../../AHCUtil/GetPartial?partialUrl=~/Areas/Credentialing/Views/CnD/_SearchResults.cshtml"
          })
          .state('load_to_plan', {
              url: '/load_to_plan',
              templateUrl: "/../../AHCUtil/GetPartial?partialUrl=~/Areas/Credentialing/Views/CnD/_load_to_plan.cshtml",
              controller: ['$scope', '$state',
                function ($scope, $state) {
                    $scope.LoadData = LoadData;
                }]
          })

      
    }
  ]
);
