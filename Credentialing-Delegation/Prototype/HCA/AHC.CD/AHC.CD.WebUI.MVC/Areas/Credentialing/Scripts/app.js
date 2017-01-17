/// <reference path="../Views/CCM/_CredentialingCheckList.cshtml" />

// Make sure to include the `ui.router` module as a dependency
angular.module('CnDRouter', ['CnDRouter.applicationForm',
  'ui.router', 'mwl.calendar', 'ui.bootstrap'
])
.run(
  ['$rootScope', '$state', '$stateParams',
    function ($rootScope, $state, $stateParams) {
        $rootScope.$state = $state;
        $rootScope.$stateParams = $stateParams;
        $rootScope.PSVStatus = false;
        $rootScope.SummaryTabStatus = true;
        $rootScope.PSVTabStatus = false;
        $rootScope.CheckListTabStatus = false;
        $rootScope.CredAppointmentTabStatus = false;
        $rootScope.PackageGeneratorTabStatus = false;
        $rootScope.LoadToPlanStatus = false;
        $rootScope.PlanEnrollMentTabStatus = false;
        $rootScope.psvStatusAll = true;
    }
  ]
)

.config(
  ['$stateProvider', '$urlRouterProvider',
    function ($stateProvider, $urlRouterProvider) {

        $urlRouterProvider

          .when('/c?id', '/application_form/:id')
            .when('/user/:id', '/contacts/:id')
          // If the url is ever invalid, e.g. '/asdf', then redirect to '/' aka the home state
          .otherwise('/');

        $stateProvider
          .state("summary", {
              url: "/",
              templateUrl: "/../../AHCUtil/GetPartial?partialUrl=~/Areas/Credentialing/Views/CnD/_Summary.cshtml",
              controller: ['$scope', '$state', '$rootScope',
                function ($scope, $state, $rootScope) {
                    $rootScope.SummaryTabStatus = true;
                    $rootScope.TabStatus = 0;
                }]

          })
          .state("initiation", {
              url: "/initiation",
              templateUrl: "/../../AHCUtil/GetPartial?partialUrl=~/Areas/Credentialing/Views/CnD/_Initiation.cshtml",
              controller: ['$scope', '$state', '$timeout','$rootScope',
              function ($scope, $state, $timeout, $rootScope) {

                  $scope.msgAlert = false;
                  $scope.initiateSuccess = false;
                  $scope.Npi = "1417989625";
                  $scope.Title = "Dr";
                  $scope.Firstname = "Pariksith";
                  $scope.Middlename = "";
                  $scope.Lastname = "Singh";
                  $scope.Type = "Medical Doctor (MD)";
                  $scope.Specilities = ["Internal Medicine"];
                  $scope.PlanList = [{ PlanID: 1, PlanName: "VIP SAVINGS" }, { PlanID: 2, PlanName: "SAVINGS PLAN RX" }, { PlanID: 3, PlanName: "SAVINGS COPD" },{ PlanID: 4, PlanName: "Wellcare" }];
                  $scope.Groups = ["American Medical Student Association"];
                  $scope.CredentialingContractRequests = [
                      {
                          BusinessEntity: "Access",
                          ContractLOBs: ["Medicare", "Medicaid"],
                          ContractSpecialties: ["Dentist", "Acupuncturist"],
                          ContractPracticeLocations: ["United States"],
                          TableRowStatus: true

                      },
                      {
                          BusinessEntity: "ACO",
                          ContractLOBs: ["WellCare", "Medicare"],
                          ContractSpecialties: ["Dentist", "Acupuncturist"],
                          ContractPracticeLocations: ["United States"],
                          TableRowStatus: false
                      },
                      {
                          BusinessEntity: "Access2",
                          ContractLOBs: ["Medicare", "Humana"],
                          ContractSpecialties: ["Dental Medicine"],
                          ContractPracticeLocations: ["United States"],
                          TableRowStatus: true
                      }
                  ];
                  $scope.ContractGrid = [
                      {
                          BusinessEntity: "ACO",
                          ContractLOBs: "WellCare",
                          ContractSpecialties: "Acupuncturist",
                          ContractPracticeLocations: "United States",
                          TableRowStatus: false
                      },
                      {
                          BusinessEntity: "Access2",
                          ContractLOBs: "Humana",
                          ContractSpecialties: "Dental Medicine",
                          ContractPracticeLocations: "United States",
                          TableRowStatus: true
                      },
                      {
                          BusinessEntity: "Access",
                          ContractLOBs: "WellCare",
                          ContractSpecialties: "Acupuncturist",
                          ContractPracticeLocations: "United States",
                          TableRowStatus: true
                      },
                      {
                          BusinessEntity: "ACO",
                          ContractLOBs: "Medicare",
                          ContractSpecialties: "Acupuncturist",
                          ContractPracticeLocations: "United States",
                          TableRowStatus: false
                      }
                  ];
                  $scope.setActionID2 = function (id) {
                      $scope.selectedAction = id;
                  };
                  $scope.setActionID2(1);
                  $scope.tableShowStatus = false;
                  $scope.tableShow = function () {
                      $scope.tableShowStatus = true;

                  };
                  $scope.allItemsSelected = false;
                  $scope.selection = [];
                  //$scope.NotSelected = [];
                  $scope.toggleSelection = function toggleSelection(CredentialingContractRequests) {
                      var idx = $scope.selection.indexOf(CredentialingContractRequests);
                      if (idx > -1) {
                          $scope.selection.splice(idx, 1);
                      }
                      else {
                          $scope.selection.push(CredentialingContractRequests);
                      }
                  }
                  $scope.selectAll = function () {

                      if ($scope.allItemsSelected == true) {
                          $scope.selection = [];
                          for (var i = 0; i < $scope.CredentialingContractRequests.length; i++) {
                              $scope.selection.push($scope.CredentialingContractRequests[i]);
                              $scope.CredentialingContractRequests[i].isChecked = $scope.allItemsSelected;
                          }
                      }
                      else {
                          $scope.selection = [];
                          for (var i = 0; i < $scope.CredentialingContractRequests.length; i++) {
                              $scope.CredentialingContractRequests[i].isChecked = $scope.allItemsSelected;
                          }
                      }
                  }
                  $scope.showPlanListReCred = function (Plan) {
                      //console.log(Plan);
                      $scope.isHasError = false;
                      $scope.loadingAjax1 = true;
                      $scope.PlanListID = Plan;
                      $timeout(Loading, 2000);
                  }
                  var Loading = function () {
                      $scope.loadingAjax1 = false;
                      $scope.tableShowStatus = true;
                  };
                  var resetMsg = function () {
                      $scope.msgAlert = false;
                  };

                  $scope.InitiateCredentialing = function () {
                      $scope.msgAlert = true;
                      $scope.initiateSuccess = true;
                      $timeout(resetMsg, 5000);
                  };

                  $scope.clearAction = function () {
                      $scope.msgAlert = false;
                      $scope.initiateSuccess = false;
                      $scope.tableShowStatus = false;
                      $scope.selection = [];
                      $scope.PlanList = angular.copy($scope.PlanList);
                  }
                  $scope.PSVSTATUSR = function () {
                      $rootScope.PSVStatus = false;
                  }

              }]
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
              templateUrl: "/../../AHCUtil/GetPartial?partialUrl=~/Areas/Credentialing/Views/Shared/_UploadedDocs.cshtml"

          })
        
          .state('psv', {
              url: '/psv',
              templateUrl: "/../../AHCUtil/GetPartial?partialUrl=~/Areas/Credentialing/Views/Shared/_VerificationReport.cshtml",
              controller: ['$scope', '$state','$rootScope',
                function ($scope, $state,$rootScope) {
                    
                    $scope.AllVerfied = function () {
                        $rootScope.PSVStatus = true;
                    }
                  $scope.PSV = function () {
                      $rootScope.PSVStatus = false;
                      $rootScope.psvStatusAll = false;
                  }
                  $scope.LinkStatus = function () {
                      $rootScope.PSVTabStatus = true;
                  }
              }]
          })
          .state("credentialing_action", {
              url: "/CCM_action",
              templateUrl: "/../../AHCUtil/GetPartial?partialUrl=~/Areas/Credentialing/Views/CCM/_CredentialingCheckList.cshtml",
              controller: ['$scope', '$state','$rootScope',
               function ($scope, $state,$rootScope) {
                   
                   $scope.Appointment = function () {
                       $rootScope.CheckListTabStatus = true;
                   }
                   $scope.PSV = function () {
                       $rootScope.PSVStatus = true;
                   }
               }]
          })
             .state("credentialing_appointment", {
                 url: "/credentialing_appointment",
                 templateUrl: "/../../AHCUtil/GetPartial?partialUrl=~/Areas/Credentialing/Views/CnD/_CredentialingAppointment.cshtml",
                 controller: ['$scope', '$state','$rootScope',
                function ($scope, $state,$rootScope) {
                    
                  
                    //-------------- CCM Action Item Lists -----------------
                    $scope.Actions = [
                        {
                            ActionID: 1,
                            ActionName: "Credentialing"
                        },
                        {
                            ActionID: 2,
                            ActionName: "Re-Credentialing"
                        },
                        {
                            ActionID: 3,
                            ActionName: "De-Credentialing"
                        }
                    ];

                    //-------------------- CCM Required Data List -----------------------
                    $scope.CCOAppointDataData = [
                        {
                            ActionID: 1,
                            CredentialingList: [
                                {
                                    FirstName: " PARIKSITH",
                                    LastName: "SINGH",
                                    PersonalDetailID: 91,
                                    ProviderTitles: "MD",
                                    Speciality: "Chiropractor",
                                    Plan: "ULTIMATE HEALTH PLANS",
                                    CredentialingDate: new Date(2014, 02, 09),
                                    Status: "Verified"
                                },
                                
                            ]
                        },
                        {
                            ActionID: 2,
                            CredentialingList: [
                                
                                {
                                    FirstName: " PARIKSITH",
                                    LastName: "SINGH",
                                    PersonalDetailID: 92,
                                    ProviderTitles: "MD",
                                    Speciality: "Chiropractor",
                                    Plan: "ULTIMATE HEALTH PLANS",
                                    CredentialingDate: new Date(2014, 02, 09),
                                    Status: "Verified"
                                }
                            ]
                        },
                        {
                            ActionID: 3,
                            CredentialingList: [
                                {
                                    FirstName: " PARIKSITH",
                                    LastName: "SINGH",
                                    PersonalDetailID: 91,
                                    ProviderTitles: "MD",
                                    Speciality: "Chiropractor",
                                    Plan: "ULTIMATE HEALTH PLANS",
                                    CredentialingDate: new Date(2014, 02, 09),
                                    Status: "Verified"
                                }
                               
                            ]
                        }
                    ];

                    //---------------- already scheduled data -----------------------
                    $scope.selectedCredentialingForDate = [
                        {
                            ActionID: 1,
                            CredentialingList: [
                                {
                                    AppointmentDate: new Date(2015, 07, 10),
                                    Credentialings: [
                                        {
                                            FirstName: " PARIKSITH",
                                            LastName: "SINGH",
                                            PersonalDetailID: 91,
                                            ProviderTitles: "MD",
                                            Speciality: "Chiropractor",
                                            Plan: "ULTIMATE HEALTH PLANS",
                                            CredentialingDate: new Date(2014, 02, 09),
                                            Status: "Verified"
                                        }
                                    ]
                                },
                                
                            ]
                        },
                        {
                            ActionID: 2,
                            CredentialingList: [
                                {
                                    AppointmentDate: new Date(2015, 07, 10),
                                    Credentialings: []
                                },
                                {
                                    AppointmentDate: new Date(2015, 08, 10),
                                    Credentialings: []
                                },
                                {
                                    AppointmentDate: new Date(2015, 06, 10),
                                    Credentialings: []
                                }
                            ]
                        },
                        {
                            ActionID: 3,
                            CredentialingList: [
                                {
                                    AppointmentDate: new Date(2015, 07, 10),
                                    Credentialings: []
                                },
                                {
                                    AppointmentDate: new Date(2015, 08, 10),
                                    Credentialings: []
                                },
                                {
                                    AppointmentDate: new Date(2015, 06, 10),
                                    Credentialings: []
                                }
                            ]
                        }
                    ];

                    $scope.resetSelection = function (data) {
                        var temp = [];
                        for (var i in data) {
                            temp[i] = false;
                        }
                        return temp;
                    };

                    //----------------- Filter Data According to Required -----------------------
                    $scope.getDataByMenu = function (data) {
                        $scope.selectedAction = angular.copy(data.ActionID);
                        $scope.CreadentialingData = angular.copy(data.CredentialingList);
                        $scope.SelectedDetails = $scope.resetSelection($scope.CreadentialingData);
                        $scope.DoneCreadentialing = [];

                        $scope.dt = new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate())
                    };

                    //------------------ init --------------
                    $scope.DoneCreadentialing = [];
                    $scope.SelectedDetails = [];

                    //-------------- selection ---------------
                    $scope.getDataByMenu($scope.CCOAppointDataData[0]);

                    $scope.AddSelectedCredentialing = function () {
                        var selectedCredentialing = [];
                        selectedCredentialing = angular.copy($scope.DoneCreadentialing);
                        var NotSeletedCreadentialing = [];

                        for (var i in $scope.SelectedDetails) {
                            if ($scope.SelectedDetails[i]) {
                                selectedCredentialing.push(angular.copy($scope.CreadentialingData[i]));
                            }
                            if (!$scope.SelectedDetails[i]) {
                                NotSeletedCreadentialing.push(angular.copy($scope.CreadentialingData[i]));
                            }
                        }
                        $scope.DoneCreadentialing = selectedCredentialing;
                        if ($scope.DoneCreadentialing.length>0) {
                            $rootScope.CredAppointmentTabStatus = true;
                        }
                        $scope.CreadentialingData = NotSeletedCreadentialing;
                        $scope.SelectedDetails = $scope.resetSelection($scope.CreadentialingData);
                    };

                }]
             })
            .state("printChecklist", {
                url: "/printChecklist",
                templateUrl: "/../../AHCUtil/GetPartial?partialUrl=~/Areas/Credentialing/Views/Shared/PrintCheckList.cshtml"

            })
          .state('plan_enrollment', {
              url: '/plan_enrollment',
              templateUrl: "/../../AHCUtil/GetPartial?partialUrl=~/Areas/Credentialing/Views/CnD/_PlanReport.cshtml",
              controller: ['$scope', '$state', '$rootScope',
                  function ($scope, $state, $rootScope) {
                      $scope.planReportComplete = function () {
                          $rootScope.PlanEnrollMentTabStatus = true;
                      }
                      
                      $scope.LoadData = LoadData;
                  }]
          })
            .state('plan_report_recred', {
                url: '/plan_report_recred',
                templateUrl: "/../../AHCUtil/GetPartial?partialUrl=~/Areas/Credentialing/Views/ReCredentialing/_PlanReport.cshtml"
            })
            .state('search', {
                url: '/search',
                templateUrl: "/../../AHCUtil/GetPartial?partialUrl=~/Areas/Credentialing/Views/CnD/_SearchResults.cshtml"
            })
            .state('load_to_plan', {
                url: '/load_to_plan',
                templateUrl: "/../../AHCUtil/GetPartial?partialUrl=~/Areas/Credentialing/Views/CnD/_load_to_plan.cshtml",
                controller: ['$scope', '$state','$rootScope',
                  function ($scope, $state, $rootScope) {
                      $scope.LoadToPlan = false;
                      $scope.ShowDetailTable = function () {
                          $scope.LoadToPlan = true;
                          $rootScope.LoadToPlanStatus = true;
                      }
                      $scope.LoadData = LoadData;
                  }]
            })
        .state('load_to_planrecred', {
            url: '/load_to_planrecred',
            templateUrl: "/../../AHCUtil/GetPartial?partialUrl=~/Areas/Credentialing/Views/ReCredentialing/_load_to_plan.cshtml",
            controller: ['$scope', '$state',
              function ($scope, $state) {
                  $scope.LoadData = LoadData;
              }]
        })

        .state('application_repo', {
            url: '/application_repo',
            templateUrl: "/../../AHCUtil/GetPartial?partialUrl=~/Areas/Credentialing/Views/CnD/_ApplicationRepository.cshtml",
            controller: ['$scope', '$state',
              function ($scope, $state) {
                  //$scope.LoadData = LoadData;
              }]
        })
        .state('package_generator', {
            url: '/package_generator',
            templateUrl: "/../../AHCUtil/GetPartial?partialUrl=~/Areas/Credentialing/Views/CnD/_Package_Generator.cshtml",
            controller: ['$scope', '$state','$rootScope',
              function ($scope, $state, $rootScope) {
                  $scope.AppRepoStatus = false;
                  $scope.DocumentRepoStatus = false;
                  $scope.PackageListStatus = false;
                  $scope.AppRepoList = ["GHI", "ATENA", "TRICARE", "FREEDOM", "OPTIMUM"];
                  $scope.DocumentRepoList = ["Birth Certificate", "Contract Doc", "SSN Doc", "State License", "Driving License"];

                  $scope.PackageList = [];
                  $scope.GeneratedList = [];
                  $scope.ShowApplicationDocument = function () {
                      $scope.AppRepoStatus = true;
                      $scope.DocumentRepoStatus = true;
                      

                  }
                  $scope.ToggleSelection = function (data) {
                      var id = $scope.PackageList.indexOf(data);
                      if (id > -1) {
                          $scope.PackageList.splice(id, 1);
                      }
                      else {
                          $scope.PackageList.push(data);
                      }
                  }
                  $scope.Generate = function (data) {
                      $scope.PackageList = [];
                      
                      $scope.AppRepoStatus = false;
                      $scope.DocumentRepoStatus = false;
                      $scope.PackageListStatus = false;
                      $scope.GeneratedList.push(data);
                      $rootScope.PackageGeneratorTabStatus = true;

                  }
              }]
        })

    }
  ]
);
