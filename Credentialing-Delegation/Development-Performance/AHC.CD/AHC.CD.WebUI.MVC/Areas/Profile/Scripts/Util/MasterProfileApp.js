// -------------------- Master Profile Manager Module -------------------------------
//--------------------- Author: KRGLV --------------------------------
//--------------- User Use Strict Section ---------------
define(['angularAMD'], function (angularAMD) {
    'use strict';
    var app = angular.module("MasterProfileApp", ['ui.router', 'ui.router.default', 'MasterProfileManager', 'smart-table', 'ui.bootstrap', 'timepickerPop', 'mgcrea.ngStrap', 'ahc.cd.autosearch', 'angular-svg-round-progressbar'])
        .config(function ($stateProvider, $urlRouterProvider, $datepickerProvider) {

            angular.extend($datepickerProvider.defaults, {
                startDate: 'today',
                autoclose: true,
                useNative: true
            });

            $stateProvider
                .state('Profile', angularAMD.route({
                    url: '/Profile',
                    abstract: '.Demographics',
                    templateUrl: rootDir + "/Profile/MasterProfile/GetPartialPage?PartialPageURL=~/Areas/Profile/Views/MasterProfile/_MasterProfile.cshtml",
                    controller: 'MasterProfileController',
                    controllerUrl: rootDir + "/Areas/" + areaName + '/Scripts/Profile/MasterProfileController.js'
                }))
                .state('Profile.Demographics',
                    angularAMD.route({
                        url: '',
                        resolve: {
                            ProfileSubData: ['httpq', '$rootScope',
                                function (httpq, $rootScope) {
                                    if (!$rootScope.MasterProfile.Demographics) {
                                        return httpq.get(rootDir + '/Profile/MasterProfile/GetDemographicsProfileDataAsync?profileId=' + profileId).then(function (data) {
                                            console.log(data);
                                            $rootScope.MasterProfile.Demographics = data;
                                            return $rootScope.MasterProfile.Demographics;
                                        });
                                    } else {
                                        return $rootScope.MasterProfile.Demographics;
                                    }
                                }
                            ]
                        },
                        templateUrl: rootDir + "/Profile/MasterProfile/GetPartialPage?PartialPageURL=~/Areas/Profile/Views/Demographic/_Demographics.cshtml",
                        controller: 'DemographicsController',
                        controllerUrl: rootDir + "/Areas/" + areaName + '/Scripts/Profile/DemographicsController.js'
                    })
                )
                .state('Profile.IdentificationLicense',
                    angularAMD.route({
                        url: '/IdentificationLicense',
                        resolve: {
                            ProfileSubData: ['httpq', '$rootScope',
                                function (httpq, $rootScope) {
                                    if (!$rootScope.MasterProfile.IdentificationLicense) {
                                        return httpq.get(rootDir + '/Profile/MasterProfile/GetIdentificationAndLicensesProfileDataAsync?profileId=' + profileId).then(function (data) {
                                            console.log(data);
                                            $rootScope.MasterProfile.IdentificationLicense = data;
                                            return $rootScope.MasterProfile.IdentificationLicense;
                                        });
                                    } else {
                                        return $rootScope.MasterProfile.IdentificationLicense;
                                    }
                                }
                            ]
                        },
                        templateUrl: rootDir + "/Profile/MasterProfile/GetPartialPage?PartialPageURL=~/Areas/Profile/Views/IdentificationAndLicense/_IdentificationLicensing.cshtml",
                        controller: 'IdentificationLicenseController',
                        controllerUrl: rootDir + "/Areas/" + areaName + '/Scripts/Profile/IdentificationLicenseController.js'
                    })
                )
                .state('Profile.EducationHistory',
                    angularAMD.route({
                        url: '/EducationHistory',
                        resolve: {
                            ProfileSubData: ['httpq', '$rootScope',
                                function (httpq, $rootScope) {
                                    if (!$rootScope.MasterProfile.EducationHistory) {
                                        return httpq.get(rootDir + '/Profile/MasterProfile/GetEducationHistoriesProfileDataAsync?profileId=' + profileId).then(function (data) {
                                            console.log(data);
                                            $rootScope.MasterProfile.EducationHistory = data;
                                            return $rootScope.MasterProfile.EducationHistory;
                                        });
                                    } else {
                                        return $rootScope.MasterProfile.EducationHistory;
                                    }
                                }
                            ]
                        },
                        templateUrl: rootDir + "/Profile/MasterProfile/GetPartialPage?PartialPageURL=~/Areas/Profile/Views/EducationHistory/_Education.cshtml",
                        controller: 'EducationController',
                        controllerUrl: rootDir + "/Areas/" + areaName + '/Scripts/Profile/EducationController.js'
                    })
                )
                .state('Profile.Specialty',
                    angularAMD.route({
                        url: '/Specialty',
                        resolve: {
                            ProfileSubData: ['httpq', '$rootScope',
                                function (httpq, $rootScope) {
                                    if (!$rootScope.MasterProfile.Specialty) {
                                        return httpq.get(rootDir + '/Profile/MasterProfile/GetBoardSpecialtiesProfileDataAsync?profileId=' + profileId).then(function (data) {
                                            console.log(data);
                                            $rootScope.MasterProfile.Specialty = data;
                                            return $rootScope.MasterProfile.Specialty;
                                        });
                                    } else {
                                        return $rootScope.MasterProfile.Specialty;
                                    }
                                }
                            ]
                        },
                        templateUrl: rootDir + "/Profile/MasterProfile/GetPartialPage?PartialPageURL=~/Areas/Profile/Views/BoardSpecialty/_Specialty.cshtml",
                        controller: 'SpecialtyController',
                        controllerUrl: rootDir + "/Areas/" + areaName + '/Scripts/Profile/SpecialtyController.js'
                    })
                )
                .state('Profile.PracticeLocation',
                    angularAMD.route({
                        url: '/PracticeLocation',
                        resolve: {
                            ProfileSubData: ['httpq', '$rootScope',
                                function (httpq, $rootScope) {
                                    if (!$rootScope.MasterProfile.PracticeLocation) {
                                        return httpq.get(rootDir + '/Profile/MasterProfile/GetPracticeLocationsProfileDataAsync?profileId=' + profileId).then(function (data) {
                                            console.log(data);
                                            $rootScope.MasterProfile.PracticeLocation = data;
                                            return $rootScope.MasterProfile.PracticeLocation;
                                        });
                                    } else {
                                        return $rootScope.MasterProfile.PracticeLocation;
                                    }
                                }
                            ]
                        },
                        templateUrl: rootDir + "/Profile/MasterProfile/GetPartialPage?PartialPageURL=~/Areas/Profile/Views/PracticeLocation/_PracticeLocationInformation.cshtml",
                        controller: 'PracticeLocationController',
                        controllerUrl: rootDir + "/Areas/" + areaName + '/Scripts/Profile/PracticeLocationController.js'
                    })
                )
                .state('Profile.HospitalPrivilege',
                    angularAMD.route({
                        url: '/HospitalPrivilege',
                        resolve: {
                            ProfileSubData: ['httpq', '$rootScope',
                                function (httpq, $rootScope) {
                                    if (!$rootScope.MasterProfile.HospitalPrivilege) {
                                        return httpq.get(rootDir + '/Profile/MasterProfile/GetHospitalPrivilegesProfileDataAsync?profileId=' + profileId).then(function (data) {
                                            console.log(data);
                                            $rootScope.MasterProfile.HospitalPrivilege = data;
                                            return $rootScope.MasterProfile.HospitalPrivilege;
                                        });
                                    } else {
                                        return $rootScope.MasterProfile.HospitalPrivilege;
                                    }
                                }
                            ]
                        },
                        templateUrl: rootDir + "/Profile/MasterProfile/GetPartialPage?PartialPageURL=~/Areas/Profile/Views/HospitalPrivilege/_HospitalPrivilegeInformation.cshtml",
                        controller: 'HospitalPrivilegeController',
                        controllerUrl: rootDir + "/Areas/" + areaName + '/Scripts/Profile/HospitalPrivilegeController.js'
                    })
                )
                .state('Profile.ProfessionalLiability',
                    angularAMD.route({
                        url: '/ProfessionalLiability',
                        resolve: {
                            ProfileSubData: ['httpq', '$rootScope',
                                function (httpq, $rootScope) {
                                    if (!$rootScope.MasterProfile.ProfessionalLiability) {
                                        return httpq.get(rootDir + '/Profile/MasterProfile/GetProfessionalLiabilitiesProfileDataAsync?profileId=' + profileId).then(function (data) {
                                            console.log(data);
                                            $rootScope.MasterProfile.ProfessionalLiability = data;
                                            return $rootScope.MasterProfile.ProfessionalLiability;
                                        });
                                    } else {
                                        return $rootScope.MasterProfile.ProfessionalLiability;
                                    }
                                }
                            ]
                        },
                        templateUrl: rootDir + "/Profile/MasterProfile/GetPartialPage?PartialPageURL=~/Areas/Profile/Views/Liability/_LiabilityInsurance.cshtml",
                        controller: 'ProfessionalLiabilityController',
                        controllerUrl: rootDir + "/Areas/" + areaName + '/Scripts/Profile/ProfessionalLiabilityController.js'
                    })
                )
                .state('Profile.WorkHistory',
                    angularAMD.route({
                        url: '/WorkHistory',
                        resolve: {
                            ProfileSubData: ['httpq', '$rootScope',
                                function (httpq, $rootScope) {
                                    if (!$rootScope.MasterProfile.WorkHistory) {
                                        return httpq.get(rootDir + '/Profile/MasterProfile/GetWorkHistoriesProfileDataAsync?profileId=' + profileId).then(function (data) {
                                            console.log(data);
                                            $rootScope.MasterProfile.WorkHistory = data;
                                            return $rootScope.MasterProfile.WorkHistory;
                                        });
                                    } else {
                                        return $rootScope.MasterProfile.WorkHistory;
                                    }
                                }
                            ]
                        },
                        templateUrl: rootDir + "/Profile/MasterProfile/GetPartialPage?PartialPageURL=~/Areas/Profile/Views/WorkHistory/_Workhistory.cshtml",
                        controller: 'WorkHistoryController',
                        controllerUrl: rootDir + "/Areas/" + areaName + '/Scripts/Profile/WorkHistoryController.js'
                    })
                )
                .state('Profile.ProfessionalReference',
                    angularAMD.route({
                        url: '/ProfessionalReference',
                        resolve: {
                            ProfileSubData: ['httpq', '$rootScope',
                                function (httpq, $rootScope) {
                                    if (!$rootScope.MasterProfile.ProfessionalReference) {
                                        return httpq.get(rootDir + '/Profile/MasterProfile/GetProfessionalReferencesProfileDataAsync?profileId=' + profileId).then(function (data) {
                                            console.log(data);
                                            $rootScope.MasterProfile.ProfessionalReference = data;
                                            return $rootScope.MasterProfile.ProfessionalReference;
                                        });
                                    } else {
                                        return $rootScope.MasterProfile.ProfessionalReference;
                                    }
                                }
                            ]
                        },
                        templateUrl: rootDir + "/Profile/MasterProfile/GetPartialPage?PartialPageURL=~/Areas/Profile/Views/ProfessionalReference/_ProfessionalReference.cshtml",
                        controller: 'ProfessionalReferenceController',
                        controllerUrl: rootDir + "/Areas/" + areaName + '/Scripts/Profile/ProfessionalReferenceController.js'
                    })
                )
                .state('Profile.ProfessionalAffiliation',
                    angularAMD.route({
                        url: '/ProfessionalAffiliation',
                        resolve: {
                            ProfileSubData: ['httpq', '$rootScope',
                                function (httpq, $rootScope) {
                                    if (!$rootScope.MasterProfile.ProfessionalAffiliation) {
                                        return httpq.get(rootDir + '/Profile/MasterProfile/GetProfessionalAffiliationsProfileDataAsync?profileId=' + profileId).then(function (data) {
                                            console.log(data);
                                            $rootScope.MasterProfile.ProfessionalAffiliation = data;
                                            return $rootScope.MasterProfile.ProfessionalAffiliation;
                                        });
                                    } else {
                                        return $rootScope.MasterProfile.ProfessionalAffiliation;
                                    }
                                }
                            ]
                        },
                        templateUrl: rootDir + "/Profile/MasterProfile/GetPartialPage?PartialPageURL=~/Areas/Profile/Views/ProfessionalAffiliation/_ProfessionalAffilation.cshtml",
                        controller: 'ProfessionalAffiliationController',
                        controllerUrl: rootDir + "/Areas/" + areaName + '/Scripts/Profile/ProfessionalAffiliationController.js'
                    })
                )
                .state('Profile.DisclosureQuestion',
                    angularAMD.route({
                        url: '/DisclosureQuestion',
                        resolve: {
                            ProfileSubData: ['httpq', '$rootScope',
                                function (httpq, $rootScope) {
                                    if (!$rootScope.MasterProfile.DisclosureQuestion) {
                                        return httpq.get(rootDir + '/Profile/MasterProfile/GetDisclosureQuestionsProfileDataAsync?profileId=' + profileId).then(function (data) {
                                            console.log(data);
                                            $rootScope.MasterProfile.DisclosureQuestion = data;
                                            return $rootScope.MasterProfile.DisclosureQuestion;
                                        });
                                    } else {
                                        return $rootScope.MasterProfile.DisclosureQuestion;
                                    }
                                }
                            ]
                        },
                        templateUrl: rootDir + "/Profile/MasterProfile/GetPartialPage?PartialPageURL=~/Areas/Profile/Views/DisclosureQuestion/_DisclosureQuestions.cshtml",
                        controller: 'DisclosureQuestionController',
                        controllerUrl: rootDir + "/Areas/" + areaName + '/Scripts/Profile/DisclosureQuestionController.js'
                    })
                )
                .state('Profile.ContractInformation',
                    angularAMD.route({
                        url: '/ContractInformation',
                        resolve: {
                            ProfileSubData: ['httpq', '$rootScope',
                                function (httpq, $rootScope) {
                                    if (!$rootScope.MasterProfile.ContractInformation) {
                                        return httpq.get(rootDir + '/Profile/MasterProfile/GetContractInfoProfileDataAsync?profileId=' + profileId).then(function (data) {
                                            console.log(data);
                                            $rootScope.MasterProfile.ContractInformation = data;
                                            return $rootScope.MasterProfile.ContractInformation;
                                        });
                                    } else {
                                        return $rootScope.MasterProfile.ContractInformation;
                                    }
                                }
                            ]
                        },
                        templateUrl: rootDir + "/Profile/MasterProfile/GetPartialPage?PartialPageURL=~/Areas/Profile/Views/ContractInformation/_ContractInformation.cshtml",
                        controller: 'ContractInformationController',
                        controllerUrl: rootDir + "/Areas/" + areaName + '/Scripts/Profile/ContractInformationController.js'
                    })
                )
                .state('Profile.DocumentRepository',
                    angularAMD.route({
                        url: '/DocumentRepository',
                        resolve: {
                            ProfileSubData: ['httpq', '$rootScope',
                                function (httpq, $rootScope) {
                                    if (!$rootScope.MasterProfile.DocumentRepository) {
                                        return httpq.get(rootDir + '/Profile/DocumentRepository/GetDocumentRepositoryProfileDataAsync?profileId=' + profileId).then(function (data) {
                                            console.log(data);
                                            $rootScope.MasterProfile.DocumentRepository = data;
                                            return $rootScope.MasterProfile.DocumentRepository;
                                        });
                                    } else {
                                        return $rootScope.MasterProfile.DocumentRepository;
                                    }
                                }
                            ]
                        },
                        templateUrl: rootDir + "/Profile/MasterProfile/GetPartialPage?PartialPageURL=~/Areas/Profile/Views/DocumentRepository/_DocumentRepository.cshtml",
                        controller: 'DocumentRepositoryController',
                        controllerUrl: rootDir + "/Areas/" + areaName + '/Scripts/Profile/DocumentRepositoryController.js'
                    })
                )
                .state('Profile.ProfileDashboard',
                    angularAMD.route({
                        url: '/ProfileDashboard',
                        resolve: {
                            ProfileSubData: ['httpq', '$rootScope',
                                function (httpq, $rootScope) {
                                    if (!$rootScope.MasterProfile.ProfileDashboard) {
                                        //return httpq.get(rootDir + '/Profile/DocumentRepository/GetDocumentRepositoryProfileDataAsync?profileId=' + profileId).then(function (data) {
                                        //    console.log(data);
                                        //    $rootScope.MasterProfile.ProfileDashboard = data;
                                        //    return $rootScope.MasterProfile.ProfileDashboard;
                                        //});
                                        return null;
                                    } else {
                                        return $rootScope.MasterProfile.ProfileDashboard;
                                    }
                                }
                            ]
                        },
                        templateUrl: rootDir + "/Profile/MasterProfile/GetPartialPage?PartialPageURL=~/Areas/Profile/Views/ProfileDashboard/_ProfileDashboard.cshtml",
                        controller: 'ProfileDashboardController',
                        controllerUrl: rootDir + "/Areas/" + areaName + '/Scripts/Profile/ProfileDashboardController.js'
                    })
                )
                .state('Profile.CustomField',
                    angularAMD.route({
                        url: '/CustomField',
                        resolve: {
                            ProfileSubData: ['httpq', '$rootScope',
                                function (httpq, $rootScope) {
                                    if (!$rootScope.MasterProfile.CustomField) {
                                        //return httpq.get(rootDir + '/Profile/DocumentRepository/GetDocumentRepositoryProfileDataAsync?profileId=' + profileId).then(function (data) {
                                        //    console.log(data);
                                        //    $rootScope.MasterProfile.CustomField = data;
                                        //    return $rootScope.MasterProfile.CustomField;
                                        //});
                                        return null;
                                    } else {
                                        return $rootScope.MasterProfile.CustomField;
                                    }
                                }
                            ]
                        },
                        templateUrl: rootDir + "/Profile/MasterProfile/GetPartialPage?PartialPageURL=~/Areas/Profile/Views/CustomFieldGeneration/_CustomFieldPartial.cshtml",
                        controller: 'CustomFieldController',
                        controllerUrl: rootDir + "/Areas/" + areaName + '/Scripts/Profile/CustomFieldController.js'
                    })
                )
                .state('Profile.DocumentationCheckList',
                    angularAMD.route({
                        url: '/DocumentationCheckList',
                        resolve: {
                            ProfileSubData: ['httpq', '$rootScope',
                                function (httpq, $rootScope) {
                                    if (!$rootScope.MasterProfile.DocumentationCheckList) {
                                        return httpq.get(rootDir + '/DocumentationCheckList/GetAllProfileDocuments?ProfileID=' + profileId).then(function (data) {
                                            console.log(data);
                                            $rootScope.MasterProfile.DocumentationCheckList = data;
                                            return $rootScope.MasterProfile.DocumentationCheckList;
                                        });
                                    } else {
                                        return $rootScope.MasterProfile.DocumentationCheckList;
                                    }
                                }
                            ]
                        },
                        templateUrl: rootDir + "/Profile/MasterProfile/GetPartialPage?PartialPageURL=~/Areas/Profile/Views/DocumentationCheckList/_DocumentationCheckList.cshtml",
                        controller: 'DocumentationCheckListController',
                        controllerUrl: rootDir + "/Areas/" + areaName + '/Scripts/Profile/DocumentationCheckListController.js'
                    })
                )
                .state('Profile.ContractGrid',
                    angularAMD.route({
                        url: '/ContractGrid',
                        resolve: {
                            ProfileSubData: ['httpq', '$rootScope',
                                function (httpq, $rootScope) {
                                    if (!$rootScope.MasterProfile.ContractGrid) {
                                        return httpq.get(rootDir + "/ContractGrid/GetAllContractGridinfoes?profileid=" + profileId).then(function (data) {
                                            console.log(data);
                                            $rootScope.MasterProfile.ContractGrid = data;
                                            return $rootScope.MasterProfile.ContractGrid;
                                        });
                                    } else {
                                        return $rootScope.MasterProfile.ContractGrid;
                                    }
                                }
                            ]
                        },
                        templateUrl: rootDir + "/Profile/MasterProfile/GetPartialPage?PartialPageURL=~/Areas/Profile/Views/ContractGrid/_ContractGrid.cshtml",
                        controller: 'ContractGridController',
                        controllerUrl: rootDir + "/Areas/" + areaName + '/Scripts/Profile/ContractGridController.js'
                    })
                )
                .state('Profile.Tasks',
                    angularAMD.route({
                        url: '/Tasks',
                        resolve: {
                            ProfileSubData: ['httpq', '$rootScope',
                                function (httpq, $rootScope) {
                                    if (!$rootScope.MasterProfile.Tasks) {
                                        return httpq.get(rootDir + '/TaskTracker/GetAllTasksByProfileId?profileid=' + profileId).then(function (data) {
                                            console.log(data);
                                            $rootScope.MasterProfile.Tasks = data;
                                            return $rootScope.MasterProfile.Tasks;
                                        });
                                    } else {
                                        return $rootScope.MasterProfile.Tasks;
                                    }
                                }
                            ]
                        },
                        templateUrl: rootDir + "/Profile/MasterProfile/GetPartialPage?PartialPageURL=~/Areas/Profile/Views/Tasks/_AllProviderTasks.cshtml",
                        controller: 'TasksController',
                        controllerUrl: rootDir + "/Areas/" + areaName + '/Scripts/Profile/TasksController.js'
                    })
                )
            .state('searchprovider', angularAMD.route({
                url: '/searchprovider',
                template: "<div ui-view>Please Wait, It will Redirect to search Provider page.....</div>",
                controller: ['$scope', '$stateParams', '$state',
                                function ($scope, $stateParams, $state) {
                                    // windows redirect to Search Provider Page if any thing wrong here //////
                                    //window.location.href = "sdkfgjdhsgkjhsdfk";
                                }
                ]
            }));

            $urlRouterProvider
                .when('/Profile', '/Profile')
                .when('/Profile/Demographics', '/Profile/Demographics')
                //.otherwise("/searchprovider");
                .otherwise("/Profile");
        })
        .run(
            ['$rootScope', '$state', '$stateParams', '$timeout', 'Manager', 'messageAlertEngine',
                function ($rootScope, $state, $stateParams, $timeout, Manager, messageAlertEngine) {
                    $rootScope.$state = $state;
                    $rootScope.$stateParams = $stateParams;
                    $rootScope.MasterProfile = {};
                    $rootScope.MasterData = {};

                    //----------------- Loading Sign for Data Change or state change ----------------------
                    $rootScope.$on('$stateChangeStart', function(event, toState, toParams, fromState, fromParams) {
                        //console.log("///////////////////// State Change Start " + toState.name + " //////////////////////////////////");

                        //console.log(event);
                        //console.log(toState);
                        //console.log(toParams);

                        //console.log("///////////////////// State Change Start //////////////////////////////////");

                        //if (toState.resolve) {
                        //    $("#LoadingModal").modal({
                        //        backdrop: "static",
                        //        show: true
                        //    })
                        //    $("body > div.modal-backdrop.fade.in").css("opacity", "0");
                        //}
                        $rootScope.LoadingDataTitle = "Loading Data.. Please Wait..";
                        $rootScope.LoadingData = true;
                        //if (toState.redirectTo) {
                        //    event.preventDefault();
                        //    $state.go(toState.redirectTo, toParams, {
                        //        location: false,
                        //        inherit: true,
                        //        relative: $state.$current,
                        //        notify: false
                        //    })
                        //}
                    });

                    $rootScope.$on('$stateChangeSuccess', function(event, toState, toParams, fromState, fromParams) {
                        //$("#LoadingModal").modal('hide');
                        ////------------------- Need To Remove Later -------------------
                        //$('.modal-backdrop').remove();

                        //$("#AuthInfoWithMouseOver").hide();
                        ////--------------- Fixed Tree Status ---------------
                        //$timeout(function () {
                        //    var treeheaight = 120 + $("#ApplicationActionTabDiv").height() - 46;
                        //    var contectheight = 60 + $("#ApplicationActionTabDiv").height() - 46;
                        //    //$("#MemberServiceTree").css("top", treeheaight + "px");
                        //    $("#MainDetailsContentUIRouter").css("padding-top", contectheight + "px");
                            
                        //}, 100);
                        $rootScope.LoadingData = false;

                    });

                    $rootScope.$on('$stateChangeError', function(event, toState, toParams, fromState, fromParams, error) {
                        //-------------- Error Handling and View Error Page ---------------
                        //console.log("State Change Errrrooooorrr..........................");
                        //$("#LoadingModal").modal('hide');
                        //$rootScope.showLoading = false;
                        $rootScope.LoadingData = false;
                    });

                    //----------------- Profile---------------
                    $rootScope.visibilityControl = "";
                    //Visibility of the div Control object to perform show and hide
                    $rootScope.randomsItems = [];
                    $rootScope.AllData = false;
                    $rootScope.filtered = [];
                    $rootScope.LoadForPdf = false;
                    $rootScope.alertMessage = "";

                    $rootScope.tempObject = {};
                    //Temp object to hold the form data so that the data gets revert once clicked cancel while add and edit


                    //Controls the View and Add feature on the page
                    $rootScope.operateViewAndAddControl = function (sectionValue) {
                        $rootScope.operateSecondCancelControl('');
                        $rootScope.closeAlertMessage();
                        $rootScope.tempObject = {};
                        $rootScope.buttonLabel = "Add"
                        $rootScope.visibilityControl = sectionValue;
                        $('[data-toggle="tooltip"]').tooltip();
                        $rootScope.tempObject.selectedLocation = {};
                        $rootScope.tempObject.selectedEduLocation = {};

                        try {
                            $('.fileinput-exists').find('a').trigger('click');
                        } catch (e) {

                        }
                    };

                    //Controls the Edit feature on the page
                    $rootScope.operateEditControl = function (sectionValue, obj) {
                        $rootScope.operateSecondCancelControl('');
                        $rootScope.closeAlertMessage();
                        $rootScope.tempObject = {};
                        $rootScope.selectedLocation = {};
                        $rootScope.tempObject.selectedEduLocation = {};
                        $rootScope.buttonLabel = "Update"
                        $rootScope.tempObject = angular.copy(obj);

                        $rootScope.visibilityControl = sectionValue;
                        $('[data-toggle="tooltip"]').tooltip();
                        try {
                            if ($rootScope.tempObject.City) {
                                $rootScope.tempObject.selectedLocation = { 'City': $rootScope.tempObject.City, 'State': $rootScope.tempObject.State, 'CountryCode': '' };
                            }

                            if ($rootScope.tempObject.SchoolInformation && $rootScope.tempObject.SchoolInformation.City) {
                                $rootScope.tempObject.selectedEduLocation = { 'City': $rootScope.tempObject.SchoolInformation.City, 'State': $rootScope.tempObject.SchoolInformation.State, 'CountryCode': '' };
                            }

                        } catch (e) { }
                        finally {
                            $rootScope.$broadcast("ArrayResize", $rootScope.tempObject);
                        }
                        try {
                            $('.fileinput-exists').find('a').trigger('click');
                        } catch (e) {

                        }
                    };

                    //Control the Renew feature on a page
                    $rootScope.operateRenewControl = function (sectionValue, obj) {
                        $rootScope.closeAlertMessage();
                        $rootScope.tempObject = {};
                        $rootScope.selectedLocation = {};
                        $rootScope.tempObject.selectedEduLocation = {};
                        $rootScope.buttonLabel = "Renew"
                        $rootScope.tempObject = angular.copy(obj);
                        $rootScope.visibilityControl = sectionValue;
                        $('[data-toggle="tooltip"]').tooltip();
                        //try {
                        //    if ($rootScope.tempObject.City) {
                        //        $rootScope.tempObject.selectedLocation = { 'City': $rootScope.tempObject.City, 'State': $rootScope.tempObject.State, 'CountryCode': '' };
                        //    }

                        //    if ($rootScope.tempObject.SchoolInformation && $rootScope.tempObject.SchoolInformation.City) {
                        //        $rootScope.tempObject.selectedEduLocation = { 'City': $rootScope.tempObject.SchoolInformation.City, 'State': $rootScope.tempObject.SchoolInformation.State, 'CountryCode': '' };
                        //    }
                        //} catch (e) { }

                        try {
                            $('.fileinput-exists').find('a').trigger('click');
                        } catch (e) {

                        }
                    };

                    //Controls the View and Add feature on the page
                    $rootScope.operateCancelControl = function (Form_Div_Id) {
                        $rootScope.dlinfoerror = false;
                        $rootScope.dlinfoerror1 = false;
                        //form = $('.militaryServiceInformationForm').find('form');
                        //FormReset(form);
                        $rootScope.operateSecondCancelControl('');

                        $rootScope.operateThirdCancelControl('');
                        $rootScope.closeAlertMessage();
                        $rootScope.tempObject = {};
                        $('.field-validation-error').removeClass('field-validation-error').addClass('field-validation-valid');
                        $('.input-validation-error').removeClass('input-validation-error').addClass('valid');
                        $rootScope.visibilityControl = "";
                        $rootScope.editGeneralCancel();
                    };

                    $rootScope.callAlert = function (alertName) {
                        $rootScope.alertMessage = alertName;
                        setTimeout(function () {
                            $rootScope.alertMessage = alertName;
                        }, 4000);
                    };

                    $rootScope.closeAlert = function (alertName) {
                        $rootScope.alertMessage = '';
                    };

                    //Second stage common controllers for view, edit, add, cancel
                    $rootScope.tempSecondObject = {};
                    //Temp object to hold the inner object form data so that the data gets revert once clicked cancel while add and edit

                    //Controls the View and Add feature on the page of object has an object
                    $rootScope.operateSecondViewAndAddControl = function (sectionValue) {
                        try {
                            $('.fileinput-exists').find('a').trigger('click');
                        } catch (e) {

                        }
                        $rootScope.tempSecondObject = {};
                        $('.field-validation-error').removeClass('field-validation-error').addClass('field-validation-valid');
                        $('.input-validation-error').removeClass('input-validation-error').addClass('valid');
                        $rootScope.visibilitySecondControl = sectionValue;
                    };

                    //Controls the Edit feature on the page of object has an object
                    $rootScope.operateSecondEditControl = function (sectionValue, obj) {
                        try {
                            $('.fileinput-exists').find('a').trigger('click');
                        } catch (e) {

                        }
                        $rootScope.tempSecondObject = {};
                        $('.field-validation-error').removeClass('field-validation-error').addClass('field-validation-valid');
                        $('.input-validation-error').removeClass('input-validation-error').addClass('valid');
                        $rootScope.tempSecondObject = angular.copy(obj);
                        $rootScope.visibilitySecondControl = sectionValue;
                        $rootScope.editGeneralCancel();

                    };

                    //----------------- is Needed for Practice Location i was defained thier ---------------
                    $rootScope.editGeneralCancel = function (PracticeLocationDetail) {
                        $rootScope.GeneralInformationEdit = false;
                    }

                    //Controls the  View and Add cancel feature on the page of object has an object
                    $rootScope.operateSecondCancelControl = function (Form_Div_Id) {
                        $rootScope.operateThirdCancelControl('');
                        $rootScope.tempSecondObject = {};
                        if (Form_Div_Id) {
                            //FormReset($("#" + Form_Div_Id).find("form"));
                        }
                        $('.field-validation-error').removeClass('field-validation-error').addClass('field-validation-valid');
                        $('.input-validation-error').removeClass('input-validation-error').addClass('valid');
                        $rootScope.visibilitySecondControl = "";
                    };



                    $rootScope.operateThirdViewAndAddControl = function (sectionValue) {
                        try {
                            $('.fileinput-exists').find('a').trigger('click');
                        } catch (e) {

                        }
                        $rootScope.tempThirdObject = {};
                        $('.field-validation-error').removeClass('field-validation-error').addClass('field-validation-valid');
                        $('.input-validation-error').removeClass('input-validation-error').addClass('valid');
                        $rootScope.visibilityThirdControl = sectionValue;
                    };

                    //Controls the Edit feature on the page of object has an object
                    $rootScope.operateThirdEditControl = function (sectionValue, obj) {
                        try {
                            $('.fileinput-exists').find('a').trigger('click');
                        } catch (e) {

                        }
                        $rootScope.tempThirdObject = {};
                        $('.field-validation-error').removeClass('field-validation-error').addClass('field-validation-valid');
                        $('.input-validation-error').removeClass('input-validation-error').addClass('valid');
                        $rootScope.tempThirdObject = angular.copy(obj);
                        $rootScope.visibilityThirdControl = sectionValue;
                    };

                    //Controls the  View and Add cancel feature on the page of object has an object
                    $rootScope.operateThirdCancelControl = function (Form_Div_Id) {
                        $rootScope.tempThirdObject = {};
                        if (Form_Div_Id) {
                            //FormReset($("#" + Form_Div_Id).find("form"));
                        }
                        $('.field-validation-error').removeClass('field-validation-error').addClass('field-validation-valid');
                        $('.input-validation-error').removeClass('input-validation-error').addClass('valid');
                        $rootScope.visibilityThirdControl = "";
                    };


                    $rootScope.saveOrUpdateData = function (urlVal, formDetail) {
                        $.ajax({
                            url: urlVal + profileId,
                            type: 'POST',
                            data: new FormData(formDetail),
                            async: false,
                            cache: false,
                            contentType: false,
                            processData: false,
                            success: function (data) {
                                return (data) ? "true" : "false";
                            },
                            error: function (e) {
                                return "error";
                            }
                        });
                    };

                    //Convert the date from database to normal
                    $rootScope.ConvertDateFormat = function (value) {
                        var returnValue = value;
                        try {
                            if (value.indexOf("/Date(") == 0) {
                                returnValue = new Date(parseInt(value.replace("/Date(", "").replace(")/", ""), 10));
                            }
                            return returnValue;
                        } catch (e) {
                            return returnValue;
                        }
                        return returnValue;
                    };

                    $rootScope.ConvertCity = function (city, zipcode) {
                        return { 'City': city, 'StateCode': '', 'Zipcode': zipcode };
                    };

                    //--------------- autohide on select ---------------------------
                    $rootScope.hideDiv = function () {
                        $('.ProviderTypeSelectAutoList').hide();
                    }

                    //--------------------auto fill date expiry - author : krglv --------------------
                    $rootScope.autoFillExpiryDate = function (objid, objstart, objend) {
                        if (!objid) {
                            return new Date(new Date(objstart).setMonth(new Date(objstart).getMonth() + 24));
                        } else {
                            return objend;
                        }
                    }

                }
            ]
        );

    angularAMD.bootstrap(app);
    return app;
});