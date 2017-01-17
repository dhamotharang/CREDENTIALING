
angular.module('CnDRouter.applicationForm', [
  'ui.router'
])
.config(
  ['$stateProvider', '$urlRouterProvider',
    function ($stateProvider, $urlRouterProvider) {
        $stateProvider
          .state('application_form', {

              //abstract: true,

              url: '/application_form',

              templateUrl: '/../../AHCUtil/GetPartial?partialUrl=~/Areas/Credentialing/Views/CnD/_ViewForm.cshtml',

              //resolve: {
              //    applicationForms: applicationForms
              //},

              controller: ['$scope', '$state',
                function ($scope, $state) {

                    $scope.ApplicationForms = applicationForms;

                    $state.go('application_form.detail', { FormID: $scope.ApplicationForms[0].id });
                }]
          })
        
        .state('application_form.list', {
            url: '',
            templateUrl: '/../../AHCUtil/GetPartial?partialUrl=~/Areas/Credentialing/Views/CnD/_ViewForm.cshtml',

            controller: ['$scope', '$state',
                function ($scope, $state) {

                    $scope.ApplicationForms = applicationForms;

                    $state.go('application_form.detail', { FormID: $scope.ApplicationForms[0].id });
                }]
        })
          .state('application_form.detail', {

              url: '/{FormID:[0-9]{1,4}}',

              views: {

                  '': {
                      templateUrl: function ($stateParams) {
                          var url = "";
                          for (var i in applicationForms) {
                              if ($stateParams.FormID == applicationForms[i].id) {
                                  url = applicationForms[i].templateurl;
                                  break;
                              }
                          }
                          if (url != "") {
                              return '/../../AHCUtil/GetPartial?partialUrl=' + url;
                          } else {
                              return '/../../AHCUtil/GetPartial?partialUrl=' + applicationForms[0].templateurl;
                              //$stateParams.FormID = applicationForms[0].id;
                          }
                      },
                      controller: ['$scope', '$stateParams',
                        function ($scope, $stateParams) {
                            //$scope.contact = utils.findById($scope.contacts, $stateParams.contactId);
                        }]
                  }
              }
          });
    }
  ]
);


var applicationForms = [
    {
        id: 1,
        templateurl: "~/Areas/Credentialing/Views/Shared/Profile/_ViewDemographics.cshtml",
        tagName: "Demographics"
    },
    {
        id: 2,
        templateurl: "~/Areas/Credentialing/Views/Shared/Profile/_ViewIdentificationLicensing.cshtml",
        tagName: "Identification & Licenses"
    },
    {
        id: 3,
        templateurl: "~/Areas/Credentialing/Views/Shared/Profile/_ViewEducation.cshtml",
        tagName: "Education History"
    },
    {
        id: 4,
        templateurl: "~/Areas/Credentialing/Views/Shared/Profile/_ViewSpecialty.cshtml",
        tagName: "Specialty/Board"
    },
    {
        id: 5,
        templateurl: "~/Areas/Credentialing/Views/Shared/Profile/_ViewPracticeLocationInformation.cshtml",
        tagName: "Practice Location"
    },
    {
        id: 6,
        templateurl: "~/Areas/Credentialing/Views/Shared/Profile/_ViewHospitalPrivilegeInformation.cshtml",
        tagName: "Hospital Privilege"
    },
    {
        id: 7,
        templateurl: "~/Areas/Credentialing/Views/Shared/Profile/_ViewLiability.cshtml",
        tagName: "Professional Liability"
    },
    {
        id: 8,
        templateurl: "~/Areas/Credentialing/Views/Shared/Profile/_ViewWorkhistory.cshtml",
        tagName: "Work History"
    },
    {
        id: 9,
        templateurl: "~/Areas/Credentialing/Views/Shared/Profile/_ViewProfessionalReference.cshtml",
        tagName: "Professional Reference"
    },
    {
        id: 10,
        templateurl: "~/Areas/Credentialing/Views/Shared/Profile/_ViewProfessionalAffilation.cshtml",
        tagName: "Professional Affiliation"
    },
    {
        id: 11,
        templateurl: "~/Areas/Credentialing/Views/Shared/Profile/_ViewContract.cshtml",
        tagName: "Contract Information"
    }
];