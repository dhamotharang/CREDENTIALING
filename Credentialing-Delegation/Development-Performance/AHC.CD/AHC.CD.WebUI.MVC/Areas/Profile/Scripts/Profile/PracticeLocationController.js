//--------------------- Author: KRGLV --------------------------------
//--------------- User Use Strict Section ---------------

define(['Util/MasterProfileApp'], function (app) {
    'use strict';
    app.register.controller('PracticeLocationController', ['$scope', '$rootScope', '$http', 'httpq', '$filter', 'masterDataService', 'locationService', 'messageAlertEngine', 'profileUpdates',
      function ($scope, $rootScope, $http, httpq, $filter, masterDataService, locationService, messageAlertEngine, profileUpdates) {

          $scope.PracticeLocationPendingRequest = profileUpdates.getUpdates('Practice Location', 'Facility') || profileUpdates.getUpdates('Practice Location', 'Practice Location Detail') || profileUpdates.getUpdates('Practice Location', 'Office Manager') || profileUpdates.getUpdates('Practice Location', 'Billing Contact') || profileUpdates.getUpdates('Practice Location', 'Payment and Remittance') || profileUpdates.getUpdates('Practice Location', 'Supervisor') || profileUpdates.getUpdates('Practice Location', 'CoveringColleague') || profileUpdates.getUpdates('Practice Location', 'workers Compensation Information') || profileUpdates.getUpdates('Practice Location', 'Open Practice Status') || profileUpdates.getUpdates('Practice Location', 'Office Hours') || profileUpdates.getUpdates('Practice Location', 'Credentialing Contact');
          var billingsTempo = [];
          var paymentsTempo = [];
          $scope.GeneralInformationEdit = false;
          // Toggle the view of passed DOM Element by ID
          $scope.PracticeLocationToggle = false;

          $scope.hideDiv = function (obj) {
              $('.ProviderTypeSelectAutoList1').hide();
              $('.ProviderTypeSelectAutoList').hide();
              $scope.FacilityName = "";
              $scope.errorMsg = false;

              //$scope.tempSecondObject = obj;
              $scope.fillCorporateName(obj.FacilityID)

              //$scope.$apply(function () {
              //    $scope.displayMe = obj.FacilityName + "-" + obj.Street + "," + obj.Building + "," + obj.City + "," + obj.State;
              //});
              //$scope.$digest();

              if (obj.FacilityName == null) {

                  FacilityName = "";

              } else {

                  FacilityName = obj.FacilityName;
                  $scope.tempSecondObject.FacilityID = obj.FacilityID;

              }

              if (obj.Street == null) {

                  Street = "";

              } else {

                  Street = obj.Street;

              }

              if (obj.Building == null) {

                  Building = "";

              } else {

                  Building = obj.Building;

              }

              if (obj.City == null) {

                  City = "";

              } else {

                  City = obj.City;

              }

              if (obj.State == null) {

                  State = "";

              } else {

                  State = obj.State;

              }

              $scope.displayMe = FacilityName + "-" + Street + "," + Building + "," + City + "," + State;
          }

          //for office hours. By Reshma
          $scope.changeOtherTwo = function (isYesOption, questionNumber) {
              if (questionNumber == 'one' && isYesOption == '1') {
                  $scope.tempSecondObject.VoiceMailToAnsweringServiceYesNoOption = '2';
                  $scope.tempSecondObject.VoiceMailOtherYesNoOption = '2';
              }
                  //else if (questionNumber == 'two' && isYesOption == '1') {
                  //    $scope.tempSecondObject.AnsweringServiceYesNoOption = '2';
                  //    $scope.tempSecondObject.VoiceMailOtherYesNoOption = '2';
                  //}
                  //else if (questionNumber == 'three' && isYesOption == '1') {
                  //    $scope.tempSecondObject.AnsweringServiceYesNoOption = '2';
                  //    $scope.tempSecondObject.VoiceMailToAnsweringServiceYesNoOption = '2';
                  //}
              else if (isYesOption == '2' && questionNumber == 'AnyTimePhoneCoverageYesNoOption') {
                  $scope.tempSecondObject.AnsweringServiceYesNoOption = '';
                  $scope.tempSecondObject.VoiceMailToAnsweringServiceYesNoOption = '';
                  $scope.tempSecondObject.VoiceMailOtherYesNoOption = '';

              }
          };


          $scope.ToggleScript = function () {
              if ($scope.PracticeLocationToggle) {
                  $scope.PracticeLocationToggle = false;
              } else {
                  $scope.PracticeLocationToggle = true;

              }
          };

          $scope.PanelToggle = function (id) {
              $rootScope.operateCancelControl('');
              $('#' + id).toggle();
          };

          $scope.getOrganizationById = function (OrganizationById) {
              for (org in $scope.organizations) {
                  if ($scope.organizations[org].OrganizationID === parseInt(OrganizationById)) {
                      return $scope.organizations[org];
                  }
              }
              return null;
          };

          $scope.facilities = [];
          $rootScope.$on("ArrayResize", function (event, args) {
              if (args != null && angular.isObject(args.FacilityDetail)) {
                  var TemporaryOBJ = [];
                  for (i in $scope.masterServiceQuestions) {

                      for (j in args.FacilityDetail.Service.FacilityServiceQuestionAnswers) {
                          if ($scope.masterServiceQuestions[i].FacilityServiceQuestionID == args.FacilityDetail.Service.FacilityServiceQuestionAnswers[j].FacilityServiceQuestionId) {
                              TemporaryOBJ.push(args.FacilityDetail.Service.FacilityServiceQuestionAnswers[j]);
                          }
                      }

                  }
                  args.FacilityDetail.Service.FacilityServiceQuestionAnswers = [];

                  args.FacilityDetail.Service.FacilityServiceQuestionAnswers = angular.copy(TemporaryOBJ);
              }
          })

          $scope.copyMasterSpecialties = [];
          $scope.copyMasterProviderTypes = [];

          $(function () {
              if (!$rootScope.MasterData.AccessibilityQuestions) {
                  httpq.get(rootDir + "/Profile/MasterData/GetAllAccessibilityQuestions").then(function (data) {
                      $rootScope.MasterData.AccessibilityQuestions = data;
                      //$scope.masterAccessibilityQuestions = data;
                  });
              }
              if (!$rootScope.MasterData.ServiceQuestions) {
                  httpq.get(rootDir + "/Profile/MasterData/GetAllServiceQuestions").then(function (data) {
                      $rootScope.MasterData.masterServiceQuestions = data;
                      //$scope.masterAccessibilityQuestions = data;
                  });
              }
              if (!$rootScope.MasterData.PracticeTypes) {
                  httpq.get(rootDir + "/Profile/MasterData/GetAllPracticeTypes").then(function (data) {
                      $rootScope.MasterData.PracticeTypes = data;
                      //$scope.masterPracticeTypes = data;
                  });
              }
              if (!$rootScope.MasterData.Organizations) {
                  httpq.get(rootDir + "/Profile/MasterData/GetAllOrganizations").then(function (data) {
                      $rootScope.MasterData.Organizations = data;
                      //$scope.organizations = data;
                  });
              }
              if (!$rootScope.MasterData.Facilities) {
                  httpq.get(rootDir + "/Profile/MasterData/GetAllFacilities").then(function (data) {
                      $rootScope.MasterData.Facilities = data;
                      //$scope.facilities = data;
                      
                      for (var i = 0; i < $rootScope.MasterData.Facilities.length; i++) {
                          var tempMidLevels = [];
                          for (var j = 0; j < $rootScope.MasterData.Facilities[i].FacilityDetail.FacilityPracticeProviders.length; j++) {
                              if ($rootScope.MasterData.Facilities[i].FacilityDetail.FacilityPracticeProviders[j].Status != 'Inactive') {
                                  tempMidLevels.push($rootScope.MasterData.Facilities[i].FacilityDetail.FacilityPracticeProviders[j]);
                              }
                          }
                          $rootScope.MasterData.Facilities[i].FacilityDetail.FacilityPracticeProviders = tempMidLevels;
                      }
                  });
              }
              if (!$rootScope.MasterData.OpenPracticeStatusQuestions) {
                  httpq.get(rootDir + "/Profile/MasterData/GetAllOpenPracticeStatusQuestions").then(function (data) {
                      $rootScope.MasterData.OpenPracticeStatusQuestions = data;
                      //$scope.masterOpenPracticeStatusQuestions = data;
                  });
              }
              if (!$rootScope.MasterData.Groups) {
                  httpq.get(rootDir + "/MasterData/Organization/GetGroups").then(function (data) {
                      $rootScope.MasterData.Groups = data;
                      //$scope.groups = data;
                  });
              }
              if (!$rootScope.MasterData.BusinessContactPerson) {
                  httpq.get(rootDir + "/MasterDataNew/GetAllMasterBusinessContactPerson").then(function (data) {
                      $rootScope.MasterData.BusinessContactPerson = data;
                      //$scope.managers = data;
                  });
              }
              if (!$rootScope.MasterData.BillingContactPerson) {
                  httpq.get(rootDir + "/MasterDataNew/GetAllMasterBillingContactPerson").then(function (data) {
                      $rootScope.MasterData.BillingContactPerson = data;
                      //$scope.billings = data;
                      billingsTempo = angular.copy(data);
                  });
              }
              if (!$rootScope.MasterData.PaymentRemittancePerson) {
                  httpq.get(rootDir + "/MasterDataNew/GetAllMasterPaymentRemittancePerson").then(function (data) {
                      $rootScope.MasterData.PaymentRemittancePerson = data;
                      //$scope.payments = data;
                      paymentsTempo = angular.copy(data);
                  });
              }
              if (!$rootScope.MasterData.CredentialingContactPerson) {
                  httpq.get(rootDir + "/MasterDataNew/GetAllMasterCredentialingContactPerson").then(function (data) {
                      $rootScope.MasterData.CredentialingContactPerson = data;
                      //$scope.credentialingContact = data;
                  });
              }
              if (!$rootScope.MasterData.Specialties) {
                  httpq.get(rootDir + "/Profile/MasterData/GetAllSpecialities").then(function (data) {
                      $rootScope.MasterData.Specialties = data;
                      //$scope.masterSpecialties = data;
                      $scope.copyMasterSpecialties = angular.copy(data);
                  });
              }
              if (!$rootScope.MasterData.ProviderTypes) {
                  httpq.get(rootDir + "/Profile/MasterData/GetAllProviderTypes").then(function (data) {
                      $rootScope.MasterData.ProviderTypes = data;
                      //$scope.masterProviderTypes = data;
                      //$scope.ProviderTypes = Providertypes;
                      $scope.copyMasterProviderTypes = angular.copy(data);
                  });
              }
          });

          $rootScope.getPractitionerData = function () {

              masterDataService.getPractitioners(rootDir + "/Profile/MasterData/GetPractitionersByProviderLevel", "Mid-Level", profileId).then(function (MidLevelPractitioners) {

                  $scope.midLevelPractitioners = MidLevelPractitioners;
              });

              masterDataService.getPractitioners(rootDir + "/Profile/MasterData/GetPractitionersByProviderLevel", "Doctor", profileId).then(function (SupervisingPractitioners) {

                  $scope.supervisingProviders = SupervisingPractitioners;
              });


              masterDataService.getProviderLevels(rootDir + "/Profile/MasterData/GetAllProviderLevelByProfileId", profileId).then(function (ProviderLevel) {
                  $scope.providerLevel = ProviderLevel;
              });
          };

          $scope.append = function (data) { $rootScope.tempObject.Name = data; }

          $scope.IsRenew = false;

          $scope.setFacilities = function (organizationID) {
              if (organizationID === "") {
                  $scope.facilities = [];
              }
              $scope.facilities = $scope.getOrganizationById(organizationID).Facilities;
          };

          $scope.addressAutocomplete = function (location) {
              if (location.length == 0) {
                  $scope.resetAddressModels();
              }

              $scope.tempObject.City = location;
              if (location.length > 1 && !angular.isObject(location)) {
                  locationService.getLocations(location).then(function (val) {
                      $scope.Locations = val;
                  });
              } else if (angular.isObject(location)) {
                  $scope.setAddressModels(location);
              }
          };

          $scope.selectedLocation = function (location) {
              $scope.setAddressModels(location);
              $(".ProviderTypeSelectAutoList").hide();
          };

          $scope.resetAddressModels = function () {
              $scope.tempObject.City = "";
              $scope.tempObject.State = "";
              $scope.tempObject.Country = "";
          };

          $scope.setAddressModels = function (location) {
              $scope.tempObject.City = location.City;
              $scope.tempObject.State = location.State;
              $scope.tempObject.Country = location.Country;

          }


          // Practice Location Details Variable Initialization

          $scope.PracticeLocationDetails = [];

          /* Practice Location Information client side controller actions */

          // Controls the View practice location feature
          $scope.operateViewControlPracLoc = function (sectionValue) {
              $rootScope.tempObject = {}; //resets temp object
              $scope.visibilityControlPracLoc = sectionValue;
          };
          // Controls the add practice location
          $scope.operateAddControlPracLoc = function (sectionValue) {
              $scope.displayMe = "";
              $scope.errorMsg = false;
              $rootScope.tempObject = {};
              $rootScope.tempSecondObject = {};
              //$scope.tempSecondObject.StartDate = "";
              //$scope.tempSecondObject.GeneralCorrespondenceYesNoOption = "";
              //$scope.tempSecondObject.PracticeExclusivelyYesNoOption = "";
              //$scope.tempSecondObject.PrimaryTax = "";
              //$scope.tempSecondObject.PrimaryYesNoOption = "";
              //$scope.tempSecondObject.CurrentlyPracticingYesNoOption = "";
              $scope.visibilityControlPracLoc = sectionValue;
              $('.field-validation-error').removeClass('field-validation-error').addClass('field-validation-valid');
              $('.input-validation-error').removeClass('input-validation-error').addClass('valid');
              $rootScope.tempObject.FacilityDetail = { Language: { NonEnglishLanguages: [] } };
              $rootScope.tempObject.PracticeOfficeHours = {};
          }
          $scope.CancelObjectForBillingContact = function () {
              $rootScope.tempSecondObject = {};
              $scope.tempSecondObject = {};
              $scope.tempSecondObject = angular.copy(TempSecondObjforBillingContact);
              $rootScope.tempSecondObject = angular.copy(TempSecondObjforBillingContact);
          }
          $scope.CancelObjectForPayment = function () {
              $rootScope.tempSecondObject = {};
              $scope.tempSecondObject = {};
              $scope.tempSecondObject = angular.copy(TempSecondObjforPayment);
              $rootScope.tempSecondObject = angular.copy(TempSecondObjforPayment);
          }
          $scope.CancelObjectForOfficeHour = function () {
              if (indexOfOfficeHour != -1) {
                  $scope.PracticeLocationDetails[indexOfOfficeHour].OfficeHour = angular.copy(TempObjforOfficeHour);
              }
          }
          var TempSecondObjforPayment = {};

          var TempSecondObjforBillingContact = {};


          var TempObjforOfficeHour = {};

          var indexOfOfficeHour = -1;

          var count = 0;

          var TimeConversionForOfficeHour = function (memeberData) {

              var changeTimeForStartTime = [];
              var changeTimeForEndTime = [];
              var i;

              for (i in memeberData.PracticeDays) {
                  var changeTimeForEndTimeTemp = [];
                  var changeTimeForStartTimeTemp = [];
                  if (memeberData.PracticeDays[i].DayOff == 'YES') {
                      changeTimeForStartTimeTemp.push('Not Available');
                      changeTimeForEndTimeTemp.push('Not Available')
                  }
                  else {
                      for (j in memeberData.PracticeDays[i].DailyHours) {
                          var tempdata = new Date();
                          tempdata.setTime(tempdata.getTime() - tempdata.getTimezoneOffset() * 60 * 1000);
                          if (angular.isDate(memeberData.PracticeDays[i].DailyHours[j].EndTime)) {
                              memeberData.PracticeDays[i].DailyHours[j].EndTime = memeberData.PracticeDays[i].DailyHours[j].EndTime.getHours() + ":" + memeberData.PracticeDays[i].DailyHours[j].EndTime.getMinutes();
                          }
                          var completetime = memeberData.PracticeDays[i].DailyHours[j].EndTime.split(":");
                          tempdata.setHours(completetime[0]);
                          tempdata.setMinutes(completetime[1]);
                          changeTimeForEndTimeTemp.push(tempdata);
                          tempdata = new Date();
                          tempdata.setTime(tempdata.getTime() - tempdata.getTimezoneOffset() * 60 * 1000);
                          if (angular.isDate(memeberData.PracticeDays[i].DailyHours[j].StartTime)) {
                              memeberData.PracticeDays[i].DailyHours[j].StartTime = memeberData.PracticeDays[i].DailyHours[j].StartTime.getHours() + ":" + memeberData.PracticeDays[i].DailyHours[j].StartTime.getMinutes();
                          }
                          completetime = memeberData.PracticeDays[i].DailyHours[j].StartTime.split(":");
                          tempdata.setHours(completetime[0]);
                          tempdata.setMinutes(completetime[1]);
                          changeTimeForStartTimeTemp.push(tempdata);
                      }
                  }

                  changeTimeForStartTime.push({ StratTime: changeTimeForStartTimeTemp });
                  changeTimeForEndTime.push({ EndTime: changeTimeForEndTimeTemp });
              }
              for (i in memeberData.PracticeDays) {
                  for (j in memeberData.PracticeDays[i].DailyHours) {
                      memeberData.PracticeDays[i].DailyHours[j].EndTime = changeTimeForEndTime[i].EndTime[j];
                      memeberData.PracticeDays[i].DailyHours[j].StartTime = changeTimeForStartTime[i].StratTime[j];
                  }

              }
              return memeberData;

          }


          $scope.TemporaryOfficeHourcheck = function (data) {
              $scope.PracticeLocationtemporary = {};
              if (data.OfficeHour == null) {
                  var tempa = data.Facility.FacilityDetail.PracticeOfficeHour;
                  tempa.PracticeDays = [];
                  tempa.PracticeDays = $scope.OriginalPracticeDays;
                  $scope.PracticeLocationtemporary = angular.copy(TimeConversionForOfficeHour(tempa));
                  $scope.PracticeLocationtemporary.PracticeDays = [];
                  $scope.PracticeLocationtemporary.PracticeDays = $scope.OriginalPracticeDays;
              }
              else {
                  $scope.PracticeLocationtemporary = data.OfficeHour;
              }
          }
          $scope.TemporaryDataForOfficeHour = function (data, memeber) {
              TempObjforOfficeHour = angular.copy(memeber);
              //memeber = TimeConversionForOfficeHour(memeber);
              //memeberData.PracticeDays.DailyHours=angular.copy(changeTime);

              $scope.PracticeLocationtemporary = angular.copy(TimeConversionForOfficeHour(memeber));
              indexOfOfficeHour = $scope.PracticeLocationDetails.indexOf(data);

          }
          $scope.TemporaryDataForBillingContact = function (data) {
              TempSecondObjforBillingContact = data;

              $scope.tempSecondObject = angular.copy(data);
          }

          $scope.TemporaryDataForPayment = function (data) {
              TempSecondObjforPayment = data;
              $scope.tempSecondObject = angular.copy(data);
          }

          $scope.TemporaryDataForofficeManager = function (data) {
              TempSecondObjforOfficeManager = data;
              $scope.tempSecondObject = angular.copy(data);
          }

          $scope.resetData = function () {
              $scope.searchSpecialty = "";
              $scope.searchproviderType = "";
              $scope.tempSecondObject = $rootScope.tempSecondObject;
          }

          $scope.editGeneralInfo = function (PracticeLocationDetail) {
              //$scope.tempSecondObject = {};

              $rootScope.operateSecondEditControl(null, PracticeLocationDetail);
              $scope.GeneralInformationEdit = true;
              $scope.tempSecondObject = $rootScope.tempSecondObject;
              $scope.tempSecondObject.PracticeLocationCorporateName = $rootScope.tempSecondObject.PracticeLocationCorporateName;
              $scope.tempSecondObject.GroupName = $rootScope.tempSecondObject.GroupName;
              $scope.tempSecondObject.PrimaryYesNoOption = $rootScope.tempSecondObject.PrimaryYesNoOption;
              $scope.tempSecondObject.CurrentlyPracticingYesNoOption = $rootScope.tempSecondObject.CurrentlyPracticingYesNoOption;
              $scope.tempSecondObject.StartDate = $rootScope.tempSecondObject.StartDate;
              $scope.tempSecondObject.PrimaryTax = $rootScope.tempSecondObject.PrimaryTax;
              $scope.tempSecondObject.GeneralCorrespondenceYesNoOption = $rootScope.tempSecondObject.GeneralCorrespondenceYesNoOption;
              $scope.tempSecondObject.PracticeExclusivelyYesNoOption = $rootScope.tempSecondObject.PracticeExclusivelyYesNoOption;
              if ($rootScope.tempSecondObject.Facility.FacilityName == null) {
                  FacilityName = "";
              } else {
                  FacilityName = $rootScope.tempSecondObject.Facility.FacilityName;
                  $scope.tempSecondObject.FacilityID = $rootScope.tempSecondObject.Facility.FacilityID;
              }
              if ($rootScope.tempSecondObject.Facility.Street == null) {
                  Street = "";
              } else {
                  Street = $rootScope.tempSecondObject.Facility.Street;
              }
              if ($rootScope.tempSecondObject.Facility.Building == null) {
                  Building = "";
              } else {
                  Building = $rootScope.tempSecondObject.Facility.Building;
              }
              if ($rootScope.tempSecondObject.Facility.City == null) {

                  City = "";

              } else {

                  City = $rootScope.tempSecondObject.Facility.City;

              }

              if ($rootScope.tempSecondObject.Facility.State == null) {

                  State = "";

              } else {

                  State = $rootScope.tempSecondObject.Facility.State;

              }

              $scope.displayMe = FacilityName + "-" + Street + "," + Building + "," + City + "," + State;
              $scope.tempSecondObject.FacilityID = PracticeLocationDetail.FacilityId;
          }

          //....................Practice Location History............................//
          $scope.PracticeLocationDetailsHistory = [];
          $scope.dataFetched = false;

          $scope.setFiles = function (file) {
              $(file).parent().parent().find(".jancyFileWrapTexts").find("span").width($(file).parent().parent().width() < 243 ? $(file).parent().parent().width() : 243);

          }

          $scope.showPLHistory = function (loadingId) {
              if ($scope.PracticeLocationDetailsHistory.length == 0) {
                  $("#" + loadingId).css('display', 'block');
                  var url = rootDir + "/Profile/ProfileHistory/GetAllPracticeLocationDetailHistory?profileId=" + profileId;
                  $http.get(url).success(function (data) {
                      $scope.PracticeLocationDetailsHistory = data;
                      $scope.dataFetched = true;
                      for (var i = 0; i < $scope.PracticeLocationDetailsHistory.length; i++) {
                          if ($scope.PracticeLocationDetailsHistory[i].HistoryStatus == '' || !$scope.PracticeLocationDetailsHistory[i].HistoryStatus) {
                              $scope.PracticeLocationDetailsHistory[i].HistoryStatus = 'Renewed';
                          }
                          $scope.PracticeLocationDetailsHistory[i].SupervisingProviders = [];
                          $scope.PracticeLocationDetailsHistory[i].MidlevelPractioners = [];
                          $scope.PracticeLocationDetailsHistory[i].PracticeColleagues = [];
                          for (var j = 0; j < $scope.PracticeLocationDetailsHistory[i].PracticeProviders.length; j++) {
                              if ($scope.PracticeLocationDetailsHistory[i].PracticeProviders[j].Practice == 'Supervisor' && $scope.PracticeLocationDetailsHistory[i].PracticeProviders[j].Status == 'Active') {
                                  $scope.PracticeLocationDetailsHistory[i].SupervisingProviders.push($scope.PracticeLocationDetailsHistory[i].PracticeProviders[j]);
                              }
                              if ($scope.PracticeLocationDetailsHistory[i].PracticeProviders[j].Practice == 'Midlevel' && $scope.PracticeLocationDetailsHistory[i].PracticeProviders[j].Status == 'Active') {
                                  $scope.PracticeLocationDetailsHistory[i].MidlevelPractioners.push($scope.PracticeLocationDetailsHistory[i].PracticeProviders[j]);
                              }
                              if ($scope.PracticeLocationDetailsHistory[i].PracticeProviders[j].Practice == 'CoveringColleague' && $scope.PracticeLocationDetailsHistory[i].PracticeProviders[j].Status == 'Active') {
                                  $scope.PracticeLocationDetailsHistory[i].PracticeColleagues.push($scope.PracticeLocationDetailsHistory[i].PracticeProviders[j]);
                                  for (var k = 0; k < $scope.PracticeLocationDetailsHistory[i].PracticeColleagues.length; k++) {
                                      tempPracticeProviderSpecialties = [];
                                      tempPracticeProviderTypes = [];
                                      for (var l = 0; l < $scope.PracticeLocationDetailsHistory[i].PracticeColleagues[k].PracticeProviderSpecialties.length; l++) {
                                          if ($scope.PracticeLocationDetailsHistory[i].PracticeColleagues[k].PracticeProviderSpecialties[l].StatusType == '1') {
                                              tempPracticeProviderSpecialties.push($scope.PracticeLocationDetailsHistory[i].PracticeColleagues[k].PracticeProviderSpecialties[l]);
                                          }
                                      }
                                      $scope.PracticeLocationDetailsHistory[i].PracticeColleagues[k].PracticeProviderSpecialties = tempPracticeProviderSpecialties;
                                      for (var l = 0; l < $scope.PracticeLocationDetailsHistory[i].PracticeColleagues[k].PracticeProviderTypes.length; l++) {
                                          if ($scope.PracticeLocationDetailsHistory[i].PracticeColleagues[k].PracticeProviderTypes[l].StatusType == '1') {
                                              tempPracticeProviderTypes.push($scope.PracticeLocationDetailsHistory[i].PracticeColleagues[k].PracticeProviderTypes[l]);
                                          }
                                      }
                                      $scope.PracticeLocationDetailsHistory[i].PracticeColleagues[k].PracticeProviderTypes = tempPracticeProviderTypes;
                                  }
                              }
                          }
                      }
                      $scope.showPracticeLocationHistoryTable = true;
                      $("#" + loadingId).css('display', 'none');
                  });
              }

              else {
                  $scope.showPracticeLocationHistoryTable = true;
              }

          }

          $scope.cancelPLHistory = function () {
              $scope.showPracticeLocationHistoryTable = false;
          }
          var convertTEmp = function (data) {
              if (!angular.isDate(data)) {
                  var TimeTempo11 = new Date();
                  //tempdata.setTime(tempdata.getTime() - tempdata.getTimezoneOffset() * 60 * 1000);
                  if (data == "Day Off" || data == "Not Available") {
                      return TimeTempo11;
                  }
                  var a = data.split(":");
                  TimeTempo11.setHours(a[0]);
                  TimeTempo11.setMinutes(a[1]);
                  return TimeTempo11;
              }
              return data;
          }


          var bubbleSort = function (arr) {
              var len = arr.length;
              for (var i = len - 1; i >= 0; i--) {
                  for (var j = 1; j <= i; j++) {
                      if (parseInt(arr[j - 1].StartTime.split(':')[0], 10) > parseInt(arr[j].StartTime.split(':')[0], 10)) {
                          var temp = arr[j - 1];
                          arr[j - 1] = arr[j];
                          arr[j] = temp;
                      }
                  }
              }
              return arr;
          }


          // rootScoped on emitted value catches the value for the model and insert to get the old data
          //calling the method using $on(PSP-public subscriber pattern)


          $rootScope.$on('PracticeLocationDetails', function (event, val) {

              $scope.PracticeLocationDetails = val;
              if (val) {
                  for (var i = 0; i < $scope.PracticeLocationDetails.length ; i++) {
                      if (!$scope.PracticeLocationDetails[i].PracticingGroupId) { $scope.PracticeLocationDetails[i].PracticingGroupId = ""; }
                      $scope.PracticeLocationDetails[i].MidlevelPractioners = [];
                      $scope.PracticeLocationDetails[i].SupervisingProviders = [];
                      $scope.PracticeLocationDetails[i].PracticeColleagues = [];
                      /* Parsing the date format in client side */
                      //if ($scope.PracticeLocationDetails[i].StartDate)
                      //$scope.PracticeLocationDetails[i].StartDate = ConvertDateFormat($scope.PracticeLocationDetails[i].StartDate);
                      //if ($scope.PracticeLocationDetails[i].WorkersCompensationInformation) {
                      //    $scope.PracticeLocationDetails[i].WorkersCompensationInformation.IssueDate = ConvertDateFormat($scope.PracticeLocationDetails[i].WorkersCompensationInformation.IssueDate);
                      //    $scope.PracticeLocationDetails[i].WorkersCompensationInformation.ExpirationDate = ConvertDateFormat($scope.PracticeLocationDetails[i].WorkersCompensationInformation.ExpirationDate);
                      //}
                      //var tempArray = [];
                      //if (!$scope.PracticeLocationDetails[i].Facility.FacilityDetail.PracticeOfficeHour) {
                      //    for (var j in $scope.PracticeLocationDetails[i].Facility.FacilityDetail.PracticeOfficeHour.PracticeDays) {
                      //        tempArray.push($scope.PracticeLocationDetails[i].Facility.FacilityDetail.PracticeOfficeHour.PracticeDays[j].PracticeDailyHourID);
                      //    }
                      //    $scope.PracticeLocationDetails[i].Facility.FacilityDetail.PracticeOfficeHour = {};
                      //    $scope.PracticeLocationDetails[i].Facility.FacilityDetail.PracticeOfficeHour.PracticeDays = angular.copy($scope.OriginalPracticeDays);
                      //    for (var k in tempArray) {
                      //        $scope.PracticeLocationDetails[i].Facility.FacilityDetail.PracticeOfficeHour.PracticeDays[k] = tempArray[k];
                      //    }
                      //}
                      //else {
                      //    for (temporary in $scope.PracticeLocationDetails[i].Facility.FacilityDetail.PracticeOfficeHour.PracticeDays) {
                      //        for (d in $scope.PracticeLocationDetails[i].Facility.FacilityDetail.PracticeOfficeHour.PracticeDays[temporary].DailyHours) {
                      //            $scope.PracticeLocationDetails[i].Facility.FacilityDetail.PracticeOfficeHour.PracticeDays[temporary].DailyHours[d].StartTime = convertTEmp($scope.PracticeLocationDetails[i].Facility.FacilityDetail.PracticeOfficeHour.PracticeDays[temporary].DailyHours[d].StartTime);
                      //            $scope.PracticeLocationDetails[i].Facility.FacilityDetail.PracticeOfficeHour.PracticeDays[temporary].DailyHours[d].EndTime = convertTEmp($scope.PracticeLocationDetails[i].Facility.FacilityDetail.PracticeOfficeHour.PracticeDays[temporary].DailyHours[d].EndTime);
                      //        }
                      //    }
                      //}
                      var a = [];
                      if (!$scope.PracticeLocationDetails[i].OfficeHour) {
                          //for (temporary in $scope.PracticeLocationDetails[i].OfficeHour.PracticeDays) {
                          //    for (d in $scope.PracticeLocationDetails[i].OfficeHour.PracticeDays[temporary].DailyHours) {
                          //        $scope.PracticeLocationDetails[i].OfficeHour.PracticeDays[temporary].DailyHours[d].StartTime = convertTEmp($scope.PracticeLocationDetails[i].OfficeHour.PracticeDays[temporary].DailyHours[d].StartTime);
                          //        $scope.PracticeLocationDetails[i].OfficeHour.PracticeDays[temporary].DailyHours[d].EndTime = convertTEmp($scope.PracticeLocationDetails[i].OfficeHour.PracticeDays[temporary].DailyHours[d].EndTime);
                          //    }
                          //}
                          var tData = angular.copy($scope.PracticeLocationDetails[i].Facility.FacilityDetail.PracticeOfficeHour);
                          $scope.PracticeLocationDetails[i].OfficeHour = tData;
                          $scope.PracticeLocationDetails[i].OfficeHour.PracticeDays = [];
                          $scope.PracticeLocationDetails[i].OfficeHour.PracticeDays = $scope.OriginalPracticeDays;
                      }

                      else {
                          for (var l = 0; l < $scope.PracticeLocationDetails[i].OfficeHour.PracticeDays.length; l++) {
                              if ($scope.PracticeLocationDetails[i].OfficeHour.PracticeDays[l].DayOff == 'NO') {
                                  $scope.PracticeLocationDetails[i].OfficeHour.PracticeDays[l].DailyHours = bubbleSort($scope.PracticeLocationDetails[i].OfficeHour.PracticeDays[l].DailyHours);
                              }
                          }
                      }
                      //for (k in $scope.PracticeLocationDetails[i].OfficeHour.PracticeDays) {
                      //    for (j in $scope.PracticeLocationDetails[i].OfficeHour.PracticeDays[k].DailyHours) {
                      //        $scope.PracticeLocationDetails[i].OfficeHour.PracticeDays[k].DailyHours[j].StartTime = $rootScope.changeTimeAmPm($scope.PracticeLocationDetails[i].OfficeHour.PracticeDays[k].DailyHours[j].StartTime);
                      //        $scope.PracticeLocationDetails[i].OfficeHour.PracticeDays[k].DailyHours[j].EndTime = $rootScope.changeTimeAmPm($scope.PracticeLocationDetails[i].OfficeHour.PracticeDays[k].DailyHours[j].EndTime)
                      //    }
                      //}
                      var FacilityMidLevels = [];
                      for (var j = 0; j < $scope.PracticeLocationDetails[i].Facility.FacilityDetail.FacilityPracticeProviders.length; j++) {
                          if ($scope.PracticeLocationDetails[i].Facility.FacilityDetail.FacilityPracticeProviders[j].Status != 'Inactive') {
                              FacilityMidLevels.push($scope.PracticeLocationDetails[i].Facility.FacilityDetail.FacilityPracticeProviders[j]);
                          }
                      }
                      $scope.PracticeLocationDetails[i].Facility.FacilityDetail.FacilityPracticeProviders = FacilityMidLevels;
                      for (var j = 0; j < $scope.PracticeLocationDetails[i].PracticeProviders.length; j++) {
                          if ($scope.PracticeLocationDetails[i].PracticeProviders[j].Practice == 'Supervisor' && $scope.PracticeLocationDetails[i].PracticeProviders[j].Status != 'Inactive') {
                              $scope.PracticeLocationDetails[i].SupervisingProviders.push($scope.PracticeLocationDetails[i].PracticeProviders[j]);
                          }
                          if ($scope.PracticeLocationDetails[i].PracticeProviders[j].Practice == 'Midlevel' && $scope.PracticeLocationDetails[i].PracticeProviders[j].Status != 'Inactive') {
                              $scope.PracticeLocationDetails[i].MidlevelPractioners.push($scope.PracticeLocationDetails[i].PracticeProviders[j]);
                          }
                          if ($scope.PracticeLocationDetails[i].PracticeProviders[j].Practice == 'CoveringColleague' && $scope.PracticeLocationDetails[i].PracticeProviders[j].Status != 'Inactive') {
                              $scope.PracticeLocationDetails[i].PracticeColleagues.push($scope.PracticeLocationDetails[i].PracticeProviders[j]);
                              for (var k = 0; k < $scope.PracticeLocationDetails[i].PracticeColleagues.length; k++) {
                                  tempPracticeProviderSpecialties = [];
                                  tempPracticeProviderTypes = [];
                                  for (var l = 0; l < $scope.PracticeLocationDetails[i].PracticeColleagues[k].PracticeProviderSpecialties.length; l++) {
                                      if ($scope.PracticeLocationDetails[i].PracticeColleagues[k].PracticeProviderSpecialties[l].StatusType == '1') {
                                          tempPracticeProviderSpecialties.push($scope.PracticeLocationDetails[i].PracticeColleagues[k].PracticeProviderSpecialties[l]);
                                      }
                                  }
                                  $scope.PracticeLocationDetails[i].PracticeColleagues[k].PracticeProviderSpecialties = tempPracticeProviderSpecialties;
                                  for (var l = 0; l < $scope.PracticeLocationDetails[i].PracticeColleagues[k].PracticeProviderTypes.length; l++) {
                                      if ($scope.PracticeLocationDetails[i].PracticeColleagues[k].PracticeProviderTypes[l].StatusType == '1') {
                                          tempPracticeProviderTypes.push($scope.PracticeLocationDetails[i].PracticeColleagues[k].PracticeProviderTypes[l]);
                                      }
                                  }
                                  $scope.PracticeLocationDetails[i].PracticeColleagues[k].PracticeProviderTypes = tempPracticeProviderTypes;
                              }
                          }
                      }
                  }
              }
          });

          //To get country code and to show the div
          $scope.CountryDialCodes = countryDailCodes;

          $scope.showContryCodeDiv = function (countryCodeDivId) {
              changeVisibilityOfCountryCode();
              $("#" + countryCodeDivId).show();
          };
          var TempSecondObj = {};
          //var PaymentTempSecondObj = {};

          //To clear data and error message on change event
          $scope.changemade = function (data) {
              if (data == "YES") {
                  $scope.billings = angular.copy(billingsTempo);
                  $scope.payments = angular.copy(paymentsTempo);
                  TempSecondObj = angular.copy($scope.tempSecondObject);
                  $scope.billing = "NO";
                  $scope.remittance = "NO";
              }
              if (data == "NO") {
                  $scope.billings = [];
                  $scope.payments = [];
                  $scope.tempSecondObject = angular.copy(TempSecondObj);
                  $scope.billing = "YES";
                  $scope.remittance = "YES";
              }
          };
          var tempopenpracticedata = [];
          $scope.Showanswersonedit = function (openpracticedata) {
              tempopenpracticedata = angular.copy(openpracticedata);
          }
          $scope.ResetAnswersonCancel = function (openpracticeid) {
              for (var i in $scope.PracticeLocationDetails) {
                  if ($scope.PracticeLocationDetails[i].OpenPracticeStatus != null && $scope.PracticeLocationDetails[i].OpenPracticeStatus.OpenPracticeStatusID == openpracticeid) {
                      $scope.PracticeLocationDetails[i].OpenPracticeStatus = angular.copy(tempopenpracticedata);
                  }
              }
          }



          $scope.clear = function () {
              $scope.PracticeSpecialties = [];
              $scope.PracticeProviderTypes = [];
              $scope.tempSecondObject.FirstName = "";
              $scope.tempSecondObject.MiddleName = "";
              $scope.tempSecondObject.LastName = "";
              $scope.tempSecondObject.Telephone = "";
              $scope.tempSecondObject.Fax = "";
              $scope.tempSecondObject.EmailAddress = "";
              $scope.tempSecondObject.CountryCodeTelephone = "";
              $scope.tempSecondObject.CountryCodeFax = "";
          }

          $scope.clearBilling = function () {
              $scope.tempSecondObject.FirstName = "";
              $scope.tempSecondObject.MiddleName = "";
              $scope.tempSecondObject.LastName = "";
              $scope.tempSecondObject.Telephone = "";
              $scope.tempSecondObject.Fax = "";
              $scope.tempSecondObject.EmailAddress = "";
              $scope.tempSecondObject.Street = "";
              $scope.tempSecondObject.City = "";
              $scope.tempSecondObject.State = "";
              $scope.tempSecondObject.ZipCode = "";
              $scope.tempSecondObject.Country = "";
              $scope.tempSecondObject.County = "";
              $scope.tempSecondObject.Building = "";
              $scope.tempSecondObject.CountryCodeTelephone = "";
              $scope.tempSecondObject.CountryCodeFax = "";
          }

          $scope.resetWorkerData = function () {
              $scope.IsRenew = false;
              $scope.tempSecondObject = $rootScope.tempSecondObject;
          }
          //}
          //$scope.clear = function () {

          //    $scope.tempSecondObject.FirstName = "";
          //    $scope.tempSecondObject.MiddleName = "";
          //    $scope.tempSecondObject.LastName = "";
          //    $scope.tempSecondObject.Telephone = "";
          //    $scope.tempSecondObject.Fax = "";
          //    $scope.tempSecondObject.EmailAddress = "";

          //}

          //$scope.clearBilling = function () {
          //    $scope.tempSecondObject.FirstName = "";
          //    $scope.tempSecondObject.MiddleName = "";
          //    $scope.tempSecondObject.LastName = "";
          //    $scope.tempSecondObject.Telephone = "";
          //    $scope.tempSecondObject.Fax = "";
          //    $scope.tempSecondObject.EmailAddress = "";
          //    $scope.tempSecondObject.Street = "";
          //    $scope.tempSecondObject.City = "";
          //    $scope.tempSecondObject.State = "";
          //    $scope.tempSecondObject.ZipCode = "";
          //    $scope.tempSecondObject.Country = "";
          //    $scope.tempSecondObject.County = "";
          //    $scope.tempSecondObject.Building = "";
          //}



          $scope.clearPayment = function () {
              //$('#paymentRemittance').val('-1').attr("selected", "selected");

              $scope.tempSecondObject.ElectronicBillingCapabilityYesNoOption = "";
              $scope.tempSecondObject.BillingDepartment = "";
              $scope.tempSecondObject.CheckPayableTo = "";
              $scope.tempSecondObject.Office = "";
              $scope.tempSecondObject.PaymentAndRemittancePerson.FirstName = "";
              $scope.tempSecondObject.PaymentAndRemittancePerson.MiddleName = "";
              $scope.tempSecondObject.PaymentAndRemittancePerson.LastName = "";
              $scope.tempSecondObject.PaymentAndRemittancePerson.Telephone = "";
              $scope.tempSecondObject.PaymentAndRemittancePerson.EmailAddress = "";
              $scope.tempSecondObject.PaymentAndRemittancePerson.Fax = "";
              $scope.tempSecondObject.PaymentAndRemittancePerson.CountryCodeFax = "";
              $scope.tempSecondObject.PaymentAndRemittancePerson.CountryCodeTelephone = "";
              $scope.tempSecondObject.PaymentAndRemittancePerson.County = "";
              $scope.tempSecondObject.PaymentAndRemittancePerson.Country = "";
              $scope.tempSecondObject.PaymentAndRemittancePerson.POBoxAddress = "";
              $scope.tempSecondObject.PaymentAndRemittancePerson.State = "";
              $scope.tempSecondObject.PaymentAndRemittancePerson.Street = "";
              $scope.tempSecondObject.PaymentAndRemittancePerson.City = "";
              $scope.tempSecondObject.PaymentAndRemittancePerson.Building = "";
              $scope.tempSecondObject.PaymentAndRemittancePerson.ZipCode = "";

          }

          //For filling data for selected office Manager/Billing contact from drop down
          $scope.provideData = function (empId, array, PracticeLocationDetail) {
              var data = $filter('filter')(array, { MasterEmployeeID: empId })[0];
              $scope.tempSecondObject.FirstName = data.FirstName;
              $scope.tempSecondObject.MiddleName = data.MiddleName;
              $scope.tempSecondObject.LastName = data.LastName;
              $scope.tempSecondObject.Telephone = data.Telephone;
              $scope.tempSecondObject.EmailAddress = data.EmailAddress;
              $scope.tempSecondObject.Fax = data.Fax;
              $scope.tempSecondObject.CountryCodeFax = data.CountryCodeFax;
              $scope.tempSecondObject.CountryCodeTelephone = data.CountryCodeTelephone;
              $scope.tempSecondObject.POBoxAddress = data.POBoxAddress;
              $scope.tempSecondObject.Country = data.Country;
              $scope.tempSecondObject.County = data.County;
              $scope.tempSecondObject.State = data.State;
              $scope.tempSecondObject.Street = data.Street;
              $scope.tempSecondObject.City = data.City;
              $scope.tempSecondObject.Building = data.Building;
              $scope.tempSecondObject.ZipCode = data.ZipCode;
          }

          //For filling data for selected payment and remittance from drop down
          $scope.paymentData = function (empId, array) {
              var data = $filter('filter')(array, { MasterPracticePaymentRemittancePersonID: empId })[0]
              $scope.tempSecondObject.ElectronicBillingCapabilityYesNoOption = data.ElectronicBillingCapabilityYesNoOption;
              $scope.tempSecondObject.BillingDepartment = data.BillingDepartment;
              $scope.tempSecondObject.CheckPayableTo = data.CheckPayableTo;
              $scope.tempSecondObject.Office = data.Office;
              $scope.tempSecondObject.PaymentAndRemittancePerson = {};
              $scope.tempSecondObject.PaymentAndRemittancePerson.FirstName = data.PaymentAndRemittancePerson.FirstName;
              $scope.tempSecondObject.PaymentAndRemittancePerson.MiddleName = data.PaymentAndRemittancePerson.MiddleName;
              $scope.tempSecondObject.PaymentAndRemittancePerson.LastName = data.PaymentAndRemittancePerson.LastName;
              $scope.tempSecondObject.PaymentAndRemittancePerson.Telephone = data.PaymentAndRemittancePerson.Telephone;
              $scope.tempSecondObject.PaymentAndRemittancePerson.EmailAddress = data.PaymentAndRemittancePerson.EmailAddress;
              $scope.tempSecondObject.PaymentAndRemittancePerson.Fax = data.PaymentAndRemittancePerson.Fax;
              $scope.tempSecondObject.PaymentAndRemittancePerson.CountryCodeFax = data.PaymentAndRemittancePerson.CountryCodeFax;
              $scope.tempSecondObject.PaymentAndRemittancePerson.CountryCodeTelephone = data.PaymentAndRemittancePerson.CountryCodeTelephone;
              $scope.tempSecondObject.PaymentAndRemittancePerson.County = data.PaymentAndRemittancePerson.County;
              $scope.tempSecondObject.PaymentAndRemittancePerson.State = data.PaymentAndRemittancePerson.State;
              $scope.tempSecondObject.PaymentAndRemittancePerson.Street = data.PaymentAndRemittancePerson.Street;
              $scope.tempSecondObject.PaymentAndRemittancePerson.City = data.PaymentAndRemittancePerson.City;
              $scope.tempSecondObject.PaymentAndRemittancePerson.Building = data.PaymentAndRemittancePerson.Building;
              $scope.tempSecondObject.PaymentAndRemittancePerson.Country = data.PaymentAndRemittancePerson.Country;
              $scope.tempSecondObject.PaymentAndRemittancePerson.ZipCode = data.PaymentAndRemittancePerson.ZipCode;
          }



          /*************************************************************************************************/
          /*********************************** Office Manager **********************************************/
          /*************************************************************************************************/

          // Save office manager Details
          $scope.saveOfficeManager = function (PracticeLocationDetail, index) {

              var validationStatus = null;
              var url = null;
              var $formData = null;

              $formData = $('#BusinessOfficeContactPersonForm' + index);
              ResetFormForValidation($formData);
              validationStatus = $formData.valid();
              url = rootDir + "/Profile/PracticeLocation/AddOfficeManagerAsync?profileId=" + profileId;

              if (validationStatus) {
                  $.ajax({
                      url: url,
                      type: 'POST',
                      data: new FormData($formData[0]),
                      async: false,
                      cache: false,
                      contentType: false,
                      processData: false,
                      success: function (data) {
                          if (data.status == "true") {

                              PracticeLocationDetail.BusinessOfficeManagerOrStaff = data.officemanager;
                              //$scope.PracticeLocationDetails.push(data.practiceLocationDetail);
                              //$scope.managers.push(data.officemanager);
                              $rootScope.operateSecondCancelControl('');
                              $scope.PracticeLocationPendingRequest = true;
                              messageAlertEngine.callAlertMessage("businessManagerSuccessMsg", "Office manager/Business Office Staff Contact Information Updated successfully.", "success", true);

                          }
                          else {
                              messageAlertEngine.callAlertMessage("alertOfficeManager", data.status, "danger", true);
                          }
                      },
                      error: function (e) {

                      }
                  });
              }
          };


          /*************************************************************************************************/
          /*********************************** Billing Contact **********************************************/
          /*************************************************************************************************/

          // Save Billing Contact Details
          $scope.saveBillingContact = function (PracticeLocationDetail, index) {

              var validationStatus = null;
              var url = null;
              var $formData = null;

              $formData = $('#BillingContactForm' + index);
              ResetFormForValidation($formData);
              validationStatus = $formData.valid();
              url = rootDir + "/Profile/PracticeLocation/AddBillingContactAsync?profileId=" + profileId;

              if (validationStatus) {
                  $.ajax({
                      url: url,
                      type: 'POST',
                      data: new FormData($formData[0]),
                      async: false,
                      cache: false,
                      contentType: false,
                      processData: false,
                      success: function (data) {
                          if (data.status == "true") {
                              PracticeLocationDetail.BillingContactPerson = data.billingcontact;
                              //$scope.PracticeLocationDetails.push(data.practiceLocationDetail);
                              //$scope.billings.push(data.billingcontact);
                              $rootScope.operateSecondCancelControl('');
                              $scope.PracticeLocationPendingRequest = true;
                              messageAlertEngine.callAlertMessage("billingContactSuccessMsg", "Billing Contact Information Updated successfully.", "success", true);
                          }
                          else {
                              messageAlertEngine.callAlertMessage("alertBillingContact", data.status, "danger", true);
                          }
                      },
                      error: function (e) {

                      }
                  });
              }
          };

          //For Address
          $scope.addressAutocomplete1 = function (location1) {
              if (location1.length == 0) {
                  $scope.resetAddressModels1();
              }
              $scope.tempSecondObject.City = location1;
              if (location1.length > 1 && !angular.isObject(location1)) {
                  locationService.getLocations(location1).then(function (val) {
                      $scope.Locations = val;
                  });
              } else if (angular.isObject(location1)) {
                  $scope.setAddressModels1(location1);
              }
          };

          $scope.selectedLocation1 = function (location1) {
              $scope.setAddressModels1(location1);
              $(".ProviderTypeSelectAutoList").hide();
          };

          $scope.resetAddressModels1 = function () {
              $scope.tempSecondObject.City = "";
              $scope.tempSecondObject.State = "";
              $scope.tempSecondObject.Country = "";
          };

          $scope.setAddressModels1 = function (location1) {

              $scope.tempSecondObject.City = location1.City;
              $scope.tempSecondObject.State = location1.State;
              $scope.tempSecondObject.Country = location1.Country;

          };






          //===================================address for credentialing contact================

          $scope.addressAutocomplete4 = function (location4) {
              if (location4.length == 0) {
                  $scope.resetAddressModels4();
              }
              $rootScope.tempObject.City = location4;
              if (location4.length > 1 && !angular.isObject(location4)) {
                  locationService.getLocations(location4).then(function (val) {
                      $scope.Locations = val;
                  });
              } else if (angular.isObject(location4)) {
                  $scope.setAddressModels4(location4);
              }
          };

          $scope.selectedLocation4 = function (location4) {
              $scope.setAddressModels4(location4);
              $(".ProviderTypeSelectAutoList").hide();
          };

          $scope.resetAddressModels4 = function () {
              $rootScope.tempObject.City = "";
              $rootScope.tempObject.State = "";
              $rootScope.tempObject.Country = "";
          };

          $scope.setAddressModels4 = function (location4) {

              $rootScope.tempObject.City = location4.City;
              $rootScope.tempObject.State = location4.State;
              $rootScope.tempObject.Country = location4.Country;

          };

          //====================================================================================

          /*************************************************************************************************/
          /*********************************** Payment And Remittance **********************************************/
          /*************************************************************************************************/

          // Save Payment and remittance Details
          $scope.savePayment = function (PracticeLocationDetail, index) {

              var validationStatus = null;
              var url = null;
              var $formData = null;

              $formData = $('#PaymentForm' + index);
              ResetFormForValidation($formData);
              validationStatus = $formData.valid();
              url = rootDir + "/Profile/PracticeLocation/AddPaymentAndRemittanceAsync?profileId=" + profileId;

              if (validationStatus) {
                  $.ajax({
                      url: url,
                      type: 'POST',
                      data: new FormData($formData[0]),
                      async: false,
                      cache: false,
                      contentType: false,
                      processData: false,
                      success: function (data) {
                          if (data.status == "true") {
                              if (PracticeLocationDetail.PaymentAndRemittance != null) {
                                  $scope.PracticeLocationPendingRequest = true;
                              }
                              PracticeLocationDetail.PaymentAndRemittance = data.paymentandremittance;

                              $rootScope.operateSecondCancelControl('');

                              messageAlertEngine.callAlertMessage("paymentSuccessMsg", "Payment and Remittance Information Updated successfully.", "success", true);

                          }
                          else {
                              messageAlertEngine.callAlertMessage("alertPayment", data.status, "danger", true);
                          }
                      },
                      error: function (e) {

                      }
                  });
              }
          };

          //For Address
          $scope.addressAutocomplete2 = function (location2) {
              if (location2.length == 0) {
                  $scope.resetAddressModels2();
              }
              $scope.tempSecondObject.City = location2;
              if (location2.length > 1 && !angular.isObject(location2)) {
                  locationService.getLocations(location2).then(function (val) {
                      $scope.Locations = val;
                  });
              } else if (angular.isObject(location2)) {
                  $scope.setAddressModels2(location2);
              }
          };

          $scope.selectedLocation2 = function (location2) {
              $scope.setAddressModels2(location2);
              $(".ProviderTypeSelectAutoList").hide();
          };

          $scope.resetAddressModels2 = function () {
              $scope.tempSecondObject.PaymentAndRemittancePerson.City = "";
              $scope.tempSecondObject.PaymentAndRemittancePerson.State = "";
              $scope.tempSecondObject.PaymentAndRemittancePerson.Country = "";
          };

          $scope.setAddressModels2 = function (location2) {

              $scope.tempSecondObject.PaymentAndRemittancePerson.City = location2.City;
              $scope.tempSecondObject.PaymentAndRemittancePerson.State = location2.State;
              $scope.tempSecondObject.PaymentAndRemittancePerson.Country = location2.Country;

          };



          /* Facility Information Action Methods Start*/

          $scope.saveFacilityInformaton = function (typeOfSave, index) {

              var validationStatus = false;
              var url = null;
              var $formData = null;

              $formData = $('#newFacilityDataForm' + index);
              url = rootDir + "/Profile/PracticeLocation/AddFacilityAsync?profileId=" + profileId;
              if (typeOfSave == 'Update') {
                  url = rootDir + "/Profile/PracticeLocation/UpdateFacilityAsync?profileId=" + profileId;
              }

              ResetFormForValidation($formData);


              validationStatus = $formData.valid();

              if (validationStatus) {
                  //Simple POST request example (passing data) :
                  $.ajax({
                      url: url,
                      type: 'POST',
                      data: new FormData($formData[0]),
                      async: false,
                      cache: false,
                      contentType: false,
                      processData: false,
                      success: function (data) {
                          if (data.status == "true") {


                              //var organization = $scope.getOrganizationById($scope.OrganizationID);

                              //if (organization.Facilities == null) {
                              //    organization.Facilities = new Array();
                              //}

                              if (typeOfSave == 'Update') {
                                  $scope.PracticeLocationDetails[index].Facility = data.facility;
                                  $scope.operateAddControlPracLoc(index + '_viewPracticeLOcation');
                                  for (var i = 0; i < $scope.PracticeLocationDetails.length; i++) {
                                      if ($scope.PracticeLocationDetails[i].FacilityId == data.facility.FacilityID) {
                                          $scope.PracticeLocationDetails[i].Facility = data.facility;
                                          if (typeof $scope.PracticeLocationDetails[i].OfficeHour.AnyTimePhoneCoverage == 'undefined') {
                                              $scope.PracticeLocationDetails[i].OfficeHour.PracticeDays = data.facility.FacilityDetail.PracticeOfficeHour.PracticeDays;

                                              $scope.PracticeLocationtemporary = angular.copy(TimeConversionForOfficeHour($scope.PracticeLocationDetails[i].OfficeHour));
                                              $scope.PracticeLocationtemporary.PracticeDays = [];
                                              $scope.PracticeLocationtemporary.PracticeDays = $scope.OriginalPracticeDays;

                                          }
                                      }
                                  }

                                  count1 = 0;
                                  $scope.operateCancelControl('');
                                  messageAlertEngine.callAlertMessage("updatedFacility", "Facility Information updated successfully !!!!", "success", true);
                              }
                              else {
                                  count1 = 0;
                                  $scope.facilities.push(data.facility);
                                  $scope.operateAddControlPracLoc('addPracticeLocation');
                                  messageAlertEngine.callAlertMessage("addedNewFacility", "New Facility Information saved successfully !!!!", "success", true);

                                  $scope.tempSecondObject.FacilityId = data.facility.FacilityID;
                                  $scope.tempSecondObject.PracticeLocationCorporateName = data.facility.Name;
                              }
                              //$scope.resetDates();
                              //$scope.resetPracticeDaysList();
                              FormReset($formData);
                          } else {
                              messageAlertEngine.callAlertMessage("facilityDataErrorMsg", data.status, "danger", true);
                          }
                      },
                      error: function (e) {

                      }
                  });


              }
          };

          /* Facility Information Action Methods End*/

          $scope.savePracticeLocationDetailInformaton = function () {

              var validationStatus = false;
              var url = null;
              var $formData = null;

              $formData = $('#addPracticeLocationDetails');


              url = rootDir + "/Profile/PracticeLocation/savePracticeLocationDetailInformaton?profileId=" + profileId;


              ResetFormForValidation($formData);

              validationStatus = $formData.valid();

              if (!$scope.tempSecondObject.FacilityID) {

                  $scope.errorMsg = true;
                  validationStatus = false;

              }

              if (validationStatus) {
                  //Simple POST request example (passing data) :
                  $.ajax({
                      url: url,
                      type: 'POST',
                      data: new FormData($formData[0]),
                      async: false,
                      cache: false,
                      contentType: false,
                      processData: false,
                      success: function (data) {

                          if (data.status == "true") {
                              if ($scope.PracticeLocationDetails == null) {

                                  $scope.PracticeLocationDetails = new Array();
                              }
                              if (!data.PracticingGroupId) { data.PracticingGroupId = ""; }
                              data.practiceLocationDetail.StartDate = ConvertDateFormat(data.practiceLocationDetail.StartDate);
                              data.practiceLocationDetail.Facility = $scope.getFacilityById(data.practiceLocationDetail.FacilityId);
                              var groupName = $($formData[0]).find($("[name='PracticingGroupId'] option:selected")).text();
                              data.practiceLocationDetail.Group = {};
                              data.practiceLocationDetail.Group.Group = {};
                              if (groupName != '-- Select IPA --') {
                                  data.practiceLocationDetail.Group.Group.Name = groupName;
                              } else {
                                  data.practiceLocationDetail.Group.Group.Name = '';
                              }

                              if (!data.practiceLocationDetail.OfficeHour) {
                                  data.practiceLocationDetail.OfficeHour = data.practiceLocationDetail.Facility.FacilityDetail.PracticeOfficeHour;
                              }

                              $scope.PracticeLocationDetails.push(data.practiceLocationDetail);
                              $scope.PracticeLocationtemporary = angular.copy(TimeConversionForOfficeHour(data.practiceLocationDetail.OfficeHour));
                              $scope.operateAddControlPracLoc(($scope.PracticeLocationDetails.length - 1) + '_viewPracticeLOcation');
                              messageAlertEngine.callAlertMessage("addedNewPracticeLocation", "New Practice Location Information saved successfully !!!!", "success", true);
                              $formData[0].reset();
                          } else {
                              messageAlertEngine.callAlertMessage("addedNewPracticeLocation", data.status, "danger", true);
                              $formData[0].reset();
                          }
                      },
                      error: function (e) {

                      }
                  });


              }
          };

          //------------------------------------------------------------------------------------------------------------------//
          //-------------------------------------------------- Office Hours --------------------------------------------------//
          //------------------------------------------------------------------------------------------------------------------//
          var TimeTempo1 = new Date();
          var TimeTempo2 = new Date();
          TimeTempo1.setHours(8);
          TimeTempo1.setMinutes(30);
          var tempStartTime = TimeTempo1;
          TimeTempo2.setHours(16);
          TimeTempo2.setMinutes(30);
          var tempEndTime = TimeTempo2;

          $scope.OriginalPracticeDays = [
              { DayName: "Monday", DayOfWeek: 0, DayOff: 'NO', DailyHours: [{ StartTime: tempStartTime, EndTime: tempEndTime }] },
              { DayName: "Tuesday", DayOfWeek: 1, DayOff: 'NO', DailyHours: [{ StartTime: tempStartTime, EndTime: tempEndTime }] },
              { DayName: "Wednesday", DayOfWeek: 2, DayOff: 'NO', DailyHours: [{ StartTime: tempStartTime, EndTime: tempEndTime }] },
              { DayName: "Thursday", DayOfWeek: 3, DayOff: 'NO', DailyHours: [{ StartTime: tempStartTime, EndTime: tempEndTime }] },
              { DayName: "Friday", DayOfWeek: 4, DayOff: 'NO', DailyHours: [{ StartTime: tempStartTime, EndTime: tempEndTime }] },
              { DayName: "Saturday", DayOfWeek: 5, DayOff: 'YES', DailyHours: [{ StartTime: tempStartTime, EndTime: tempEndTime }] },
              { DayName: "Sunday", DayOfWeek: 6, DayOff: 'YES', DailyHours: [{ StartTime: tempStartTime, EndTime: tempEndTime }] }
          ];

          $scope.resetPracticeDaysList = function () {
              $rootScope.tempObject.PracticeDays = $scope.OriginalPracticeDays;

          };

          $scope.setFacilityPracticeDays = function (practiceLocationDetail, index) {
              //
              var tempArray = [];
              if (!$scope.PracticeLocationDetails[index].Facility.FacilityDetail.PracticeOfficeHour) {
                  for (var j in $scope.PracticeLocationDetails[index].Facility.FacilityDetail.PracticeOfficeHour.PracticeDays) {
                      tempArray.push($scope.PracticeLocationDetails[index].Facility.FacilityDetail.PracticeOfficeHour.PracticeDays[j].PracticeDailyHourID);
                  }
                  $scope.PracticeLocationDetails[index].Facility.FacilityDetail.PracticeOfficeHour = {};
                  $scope.PracticeLocationDetails[index].Facility.FacilityDetail.PracticeOfficeHour.PracticeDays = angular.copy($scope.OriginalPracticeDays);
                  for (var k in tempArray) {
                      $scope.PracticeLocationDetails[index].Facility.FacilityDetail.PracticeOfficeHour.PracticeDays[k] = tempArray[k];
                  }
              }
              else {
                  for (temporary in $scope.PracticeLocationDetails[index].Facility.FacilityDetail.PracticeOfficeHour.PracticeDays) {
                      for (d in $scope.PracticeLocationDetails[index].Facility.FacilityDetail.PracticeOfficeHour.PracticeDays[temporary].DailyHours) {
                          $scope.PracticeLocationDetails[index].Facility.FacilityDetail.PracticeOfficeHour.PracticeDays[temporary].DailyHours[d].StartTime = convertTEmp($scope.PracticeLocationDetails[index].Facility.FacilityDetail.PracticeOfficeHour.PracticeDays[temporary].DailyHours[d].StartTime);
                          $scope.PracticeLocationDetails[index].Facility.FacilityDetail.PracticeOfficeHour.PracticeDays[temporary].DailyHours[d].EndTime = convertTEmp($scope.PracticeLocationDetails[index].Facility.FacilityDetail.PracticeOfficeHour.PracticeDays[temporary].DailyHours[d].EndTime);
                      }
                  }
              }
              $rootScope.tempObject.PracticeDays = practiceLocationDetail.Facility.FacilityDetail.PracticeOfficeHour.PracticeDays;

          };

          $scope.addDailyHour = function (DailyHours) {
              var startTime = new Date();
              var endTime = new Date();
              DailyHours.push({ StartTime: startTime, EndTime: endTime });

          };
          $scope.addDailyHourMain = function (id) {
              var startTime = new Date();
              var endTime = new Date();

              //var DailyHourIndex=$scope.PracticeLocationtemporary.PracticeDays.findIndex(x => x.DayOfWeek==id.DayOfWeek)
              //$scope.PracticeLocationtemporary.PracticeDays[DailyHourIndex].DailyHours.push({ StartTime: startTime, EndTime: endTime });

              $scope.PracticeLocationtemporary.PracticeDays.filter(function (PracticeDays) { return PracticeDays.DayOfWeek == id.DayOfWeek })[0].DailyHours.push({ StartTime: startTime, EndTime: endTime });

          };
          $scope.removeDailyHour = function (DailyHours, index) {
              DailyHours.splice(index, 1);
          };
          //$scope.addDailyHourForMain = function (DailyHours,index) {
          //    var startTime = new Date();
          //    var endTime = new Date();
          //    $scope.PracticeLocationtemporary.PracticeDays[index-1].DailyHours.push({ StartTime: startTime, EndTime: endTime });

          //};
          //$scope.removeDailyHourForMain = function (DailyHours,majorIndex, index) {
          //    $scope.PracticeLocationtemporary.PracticeDays[majorIndex-1].DailyHours.splice(index, 1);
          //};
          $scope.dayOffToggel = function (PracticeDay) {

              var changeTimeForStartTime = [];
              var changeTimeForEndTime = [];

              PracticeDay.DayOff = PracticeDay.DayOff == 'YES' ? 'NO' : 'YES';
              if (PracticeDay.DayOff == 'YES') {
                  PracticeDay.DailyHours.splice(1, PracticeDay.DailyHours.length);
              }
              if ((PracticeDay.DayOff == 'NO' && PracticeDay.DailyHours[0].StartTime == 'Invalid Date') || (PracticeDay.DayOff == 'NO' && PracticeDay.DailyHours[0].StartTime == 'Day Off') || (PracticeDay.DayOff == 'NO' && PracticeDay.DailyHours[0].StartTime == 'Not Available')) {

                  var newDate = new Date();
                  PracticeDay.DailyHours[0].EndTime = newDate;
                  PracticeDay.DailyHours[0].StartTime = newDate;

              }
              else if (PracticeDay.DayOff == 'NO' && PracticeDay.DailyHours[0].StartTime != 'Invalid Date') {
                  return;
              }



          };
          $scope.Convert42HoursTimeFormat = function (value, data) {
              if (data == 'NO') {
                  var Hour = value.getHours() < 10 ? '0' + value.getHours() : value.getHours();
                  var Min = value.getMinutes() < 10 ? '0' + value.getMinutes() : value.getMinutes();
                  return Hour + ":" + Min;
              }

              return 'Day Off';
          }

          $scope.validateDailyHours = function (PracticeDays, parent, subsection, index, typeOfSave) {
              $scope.tempLanguages = [];
              $scope.tempLanguages = angular.copy(Languages);
              $scope.hideClock();
              var status = true;

              for (practiceDay in PracticeDays) {
                  var prevStartHour = "";
                  var prevStartMin = "";
                  var prevEndHour = "";
                  var prevEndMin = ""
                  for (dailyHour in PracticeDays[practiceDay].DailyHours) {
                      if (!$('#startTime_' + practiceDay + dailyHour).prop('disabled') || !$('#endTime_' + practiceDay + dailyHour).prop('disabled')) {
                          //var startTime1 = $('#startTime_' + practiceDay + dailyHour); //PracticeDays[practiceDay].DailyHours[dailyHour].StartTime;
                          //var startTime = PracticeDays[practiceDay].DailyHours[dailyHour].StartTime == 'Invalid Date'||PracticeDays[practiceDay].DailyHours[dailyHour].StartTime == 'Day Off' ? 'Day Off' : ((PracticeDays[practiceDay].DailyHours[dailyHour].StartTime.getHours() < 10 ? ('0' + PracticeDays[practiceDay].DailyHours[dailyHour].StartTime.getHours()) : (PracticeDays[practiceDay].DailyHours[dailyHour].StartTime.getHours())) + ":" + (PracticeDays[practiceDay].DailyHours[dailyHour].StartTime.getMinutes() < 10 ? ('0'+PracticeDays[practiceDay].DailyHours[dailyHour].StartTime.getMinutes()) : (PracticeDays[practiceDay].DailyHours[dailyHour].StartTime.getMinutes())));
                          //var endTime1 = $('#endTime_' + practiceDay + dailyHour); //PracticeDays[practiceDay].DailyHours[dailyHour].EndTime;
                          //var endTime = PracticeDays[practiceDay].DailyHours[dailyHour].EndTime == 'Invalid Date' || PracticeDays[practiceDay].DailyHours[dailyHour].EndTime == 'Day Off' ? 'Day Off' : ((PracticeDays[practiceDay].DailyHours[dailyHour].EndTime.getHours() < 10 ? ('0' + PracticeDays[practiceDay].DailyHours[dailyHour].EndTime.getHours()) : (PracticeDays[practiceDay].DailyHours[dailyHour].EndTime.getHours())) + ":" + (PracticeDays[practiceDay].DailyHours[dailyHour].EndTime.getMinutes() < 10 ? ('0' + PracticeDays[practiceDay].DailyHours[dailyHour].EndTime.getMinutes()) : (PracticeDays[practiceDay].DailyHours[dailyHour].EndTime.getMinutes())));
                          var startTime = $scope.Convert42HoursTimeFormat(PracticeDays[practiceDay].DailyHours[dailyHour].StartTime, PracticeDays[practiceDay].DayOff);
                          var endTime = $scope.Convert42HoursTimeFormat(PracticeDays[practiceDay].DailyHours[dailyHour].EndTime, PracticeDays[practiceDay].DayOff);
                          if (startTime != 'Day Off' && endTime != 'Day Off') {
                              var startHour = parseInt(startTime.split(':')[0]);
                              var startMin = parseInt(startTime.split(':')[1]);

                              var endHour = parseInt(endTime.split(':')[0]);
                              var endMin = parseInt(endTime.split(':')[1]);



                              if (!startTime.match(":") || !endTime.match(":") || startTime.indexOf(":") == 0 || endTime.indexOf(":") == 0) {
                                  $('#msg_' + practiceDay + dailyHour).text("Please Enter a Valid Time.");
                                  status = false;
                              }
                              else if ((startHour == endHour && startMin > endMin) || startHour > endHour) {
                                  $('#msg_' + practiceDay + dailyHour).text("Start Time Should Not Be Greater than End Time.");
                                  status = false;
                              }
                              else if (startHour == endHour && startMin == endMin) {
                                  $('#msg_' + practiceDay + dailyHour).text("Start Time And End Time Should Not Be Same.");
                                  status = false;
                              }
                              else if (dailyHour > 0) {
                                  if ((prevEndHour == startHour && prevEndMin > startMin) || prevEndHour > startHour) {
                                      $('#msg_' + practiceDay + dailyHour).text("Start Time Should not be Less than Previous End Time.");
                                      status = false;
                                  }
                                  else {
                                      $('#msg_' + practiceDay + dailyHour).text("");
                                  }
                              }
                              else {
                                  $('#msg_' + practiceDay + dailyHour).text("");
                              }
                              prevStartHour = startHour;
                              prevStartMin = startMin;
                              prevEndHour = endHour;
                              prevEndMin = endMin;
                          }
                      }
                  }
              }

              if (status && subsection == 'facility') {
                  $scope.saveFacilityInformaton(typeOfSave, index);
              }
              else if (status && subsection == 'ProviderOfficeHour') {
                  $scope.updateOfficeHours(parent, index);
              }
              $scope.PracticeLocationPendingRequest = true;
          };


          $scope.updateOfficeHours = function (PracticeLocationDetail, index) {
              $scope.hideClock();
              var validationStatus = false;
              var url = null;
              var $formData = null;

              $formData = $('#OfficeHourForm' + index);

              url = rootDir + "/Profile/PracticeLocation/updateOfficeHours?profileId=" + profileId;

              ResetFormForValidation($formData);

              validationStatus = $formData.valid();

              if (validationStatus) {
                  //Simple POST request example (passing data) :
                  $.ajax({
                      url: url,
                      type: 'POST',
                      data: new FormData($formData[0]),
                      async: false,
                      cache: false,
                      contentType: false,
                      processData: false,
                      success: function (data) {
                          if (data.status == "true") {

                              $scope.PracticeLocationDetails[index].OfficeHour = data.providerPracticeOfficeHours;
                              $rootScope.operateSecondCancelControl('');

                              messageAlertEngine.callAlertMessage("providerPracticeOfficeHoursSuccessMsg", "Office Hour Updated successfully.", "success", true);
                          } else {
                              messageAlertEngine.callAlertMessage("providerPracticeOfficeHoursErrorMsg", data.status, "danger", true);
                          }

                      },
                      error: function (e) {

                      }
                  });


              }
          };

          $scope.hideClock = function () {
              $(".clockface").hide();
              //$("#providerofficehoursdiv").find(".samay").each(function () {
              //    //try {
              //    //    if ($(this).hasClass("samay")) {
              //            $(this).clockface('hide');
              //    //    }
              //    //}
              //    //catch(err){}

              //})
          };

          //------------------------------------------------------------------------------------------------------------------//
          //----------------------------------------------- Facility Languages -----------------------------------------------//
          //------------------------------------------------------------------------------------------------------------------//


          $scope.tempLanguages = angular.copy(Languages);


          $scope.showLanguageList = function (divToBeDisplayed) {
              $("#" + divToBeDisplayed).show();
          };

          $scope.SelectLanguage = function (selectedLanguage) {

              $rootScope.tempObject.FacilityDetail.Language.NonEnglishLanguages.push({
                  NonEnglishLanguageID: null,
                  Language: selectedLanguage.name,
                  InterpretersAvailableYesNoOption: 1,
                  StatusType: 1
              });
              $scope.tempLanguages.splice($scope.tempLanguages.indexOf(selectedLanguage), 1);
              $scope.searchLang = "";
              $(".LanguageSelectAutoList").hide();
          };


          $scope.DeselectLanguage = function (language) {
              $rootScope.tempObject.FacilityDetail.Language.NonEnglishLanguages.splice($rootScope.tempObject.FacilityDetail.Language.NonEnglishLanguages.indexOf(language), 1);

              for (var i in Languages) {
                  if (Languages[i].name == language.Language) {
                      $scope.tempLanguages.push(Languages[i]);
                  }
              }

              $scope.tempLanguages.sort(function (a, b) {
                  if (a.name > b.name) {
                      return 1;
                  }
                  if (a.name < b.name) {
                      return -1;
                  }
                  // a must be equal to b
                  return 0;
              });

          };


          //$scope.saveFacilityLanguage = function () {

          //    var validationStatus = false;
          //    var url = null;
          //    var $formData = null;

          //    $formData = $('#languageInfoForm');

          //    url = "/Profile/PracticeLocation/saveFacilityLanguage";

          //    // ResetFormForValidation($formData);

          //    validationStatus = true; //$formData.valid();

          //    if (validationStatus) {
          //        //Simple POST request example (passing data) :
          //        $.ajax({
          //            url: url,
          //            type: 'POST',
          //            data: new FormData($formData[0]),
          //            async: false,
          //            cache: false,
          //            contentType: false,
          //            processData: false,
          //            success: function (data) {

          //            },
          //            error: function (e) {

          //            }
          //        });


          //    }
          //};

          //--------------------------------------------------------------------------------------------------------------------//
          //----------------------------------------------- Workers Compensation -----------------------------------------------//
          //--------------------------------------------------------------------------------------------------------------------//

          $scope.RenewDiv = function () {
              $scope.IsRenew = true;
              $scope.tempSecondObject = $rootScope.tempSecondObject;
          };
          $scope.RenewHide = function () {
              $scope.IsRenew = false;
          };
          $scope.updateWorkersCompensationInformation = function (PracticeLocationDetail, index) {

              // the implementation has to be changed to angularjs http.post to incorporate the one to many relationship which cant be captured in form data

              var validationStatus = false;
              var url = null;
              var $formData = null;

              $formData = $('#WorkerCompensationForm' + index);

              if ($scope.IsRenew == false) {
                  url = rootDir + "/Profile/PracticeLocation/UpdateWorkersCompensationInformationAsync?profileId=" + profileId;
              }
              else {
                  url = rootDir + "/Profile/PracticeLocation/RenewWorkersCompensationInformationAsync?profileId=" + profileId;
              }

              ResetFormForValidation($formData);


              validationStatus = $formData.valid();

              if (validationStatus) {
                  //Simple POST request example (passing data) :
                  $.ajax({
                      url: url,
                      type: 'POST',
                      data: new FormData($formData[0]),
                      async: false,
                      cache: false,
                      contentType: false,
                      processData: false,
                      success: function (data) {

                          if (data.status == "true") {
                              data.workersCompensationInformation.IssueDate = ConvertDateFormat(data.workersCompensationInformation.IssueDate);
                              data.workersCompensationInformation.ExpirationDate = ConvertDateFormat(data.workersCompensationInformation.ExpirationDate);
                              if (PracticeLocationDetail.WorkersCompensationInformation != null) {
                                  $scope.PracticeLocationPendingRequest = true;
                              }
                              PracticeLocationDetail.WorkersCompensationInformation = data.workersCompensationInformation;
                              $rootScope.operateSecondCancelControl('');

                              messageAlertEngine.callAlertMessage("workersCompensationInformationSuccessMsg", "Worker's Compensation Information Updated successfully.", "success", true);
                          } else {
                              messageAlertEngine.callAlertMessage("workersCompensationInformationErrorMsg", data.status, "danger", true);
                          } $scope.IsRenew == false
                      },
                      error: function (e) {

                      }
                  });


              }
          };


          //--------------------------------------------------------------------------------------------------------------------//
          //----------------------------------------------- Open Practice Status -----------------------------------------------//
          //--------------------------------------------------------------------------------------------------------------------//

          $scope.saveOpenPracticeStatus = function (PracticeLocationDetail, index) {

              var validationStatus = false;
              var url = null;
              var $formData = null;

              $formData = $('#practiceOpenStatusForm' + index);
              url = rootDir + "/Profile/PracticeLocation/UpdateOpenPracticeStatusAsync?profileId=" + profileId;
              ResetFormForValidation($formData);
              validationStatus = $formData.valid();

              if (validationStatus) {
                  //Simple POST request example (passing data) :
                  $.ajax({
                      url: url,
                      type: 'POST',
                      data: new FormData($formData[0]),
                      async: false,
                      cache: false,
                      contentType: false,
                      processData: false,
                      success: function (data) {
                          if (data.status == "true") {
                              PracticeLocationDetail.OpenPracticeStatus = data.openPracticeStatus;
                              $scope.PracticeLocationPendingRequest = true;
                              $rootScope.operateSecondCancelControl('');
                              messageAlertEngine.callAlertMessage("openPracticeStatusSuccessMsg", "Open Practice Status Updated successfully.", "success", true);
                          } else {
                              messageAlertEngine.callAlertMessage("openPracticeStatusErrorMsg", data.status, "danger", true);
                          }
                      },
                      error: function (e) {

                      }
                  });
              }
          };

          $scope.validateForAge = function (index) {
              $formData = $('#practiceOpenStatusForm' + index);
              ResetFormForValidation($formData);
              validationStatus = $formData.valid();
          }

          

          // Utility function to get facility by ID

          $scope.getFacilityById = function (facilityId) {

              for (fac in $scope.facilities) {
                  if ($scope.facilities[fac].FacilityID == parseInt(facilityId)) {
                      return $scope.facilities[fac];
                  }
              }

              return null;
          };

          $scope.fillCurrentlyPracticingYesNoOption = function (value) {
              if (value == '1') {
                  $scope.tempSecondObject.CurrentlyPracticingYesNoOption = '1';
              }
          }

          $scope.updatePracticeLocationDetailInformaton = function (index) {

              var validationStatus = false;
              var url = null;
              var $formData = null;

              $formData = $('#updatePracticeLocationDetails' + index);


              url = rootDir + "/Profile/PracticeLocation/updatePracticeLocationDetailInformaton?profileId=" + profileId;


              ResetFormForValidation($formData);

              validationStatus = $formData.valid();
              if (validationStatus) {
                  //Simple POST request example (passing data) :
                  $.ajax({
                      url: url,
                      type: 'POST',
                      data: new FormData($formData[0]),
                      async: false,
                      cache: false,
                      contentType: false,
                      processData: false,
                      success: function (data) {
                          if (data.status == "true") {
                              $scope.PracticeLocationDetails[index].StartDate = ConvertDateFormat(data.practiceLocationDetail.StartDate);
                              $scope.PracticeLocationDetails[index].IsPrimary = data.practiceLocationDetail.IsPrimary;
                              $scope.PracticeLocationDetails[index].GroupName = data.practiceLocationDetail.GroupName;
                              $scope.PracticeLocationDetails[index].PrimaryYesNoOption = data.practiceLocationDetail.PrimaryYesNoOption;
                              $scope.PracticeLocationDetails[index].PracticeExclusively = data.practiceLocationDetail.PracticeExclusively;
                              $scope.PracticeLocationDetails[index].PracticeExclusivelyYesNoOption = data.practiceLocationDetail.PracticeExclusivelyYesNoOption;
                              $scope.PracticeLocationDetails[index].CurrentlyPracticingAtThisAddress = data.practiceLocationDetail.CurrentlyPracticingAtThisAddress;
                              $scope.PracticeLocationDetails[index].CurrentlyPracticingYesNoOption = data.practiceLocationDetail.CurrentlyPracticingYesNoOption;
                              $scope.PracticeLocationDetails[index].SendGeneralCorrespondence = data.practiceLocationDetail.SendGeneralCorrespondence;
                              $scope.PracticeLocationDetails[index].GeneralCorrespondenceYesNoOption = data.practiceLocationDetail.GeneralCorrespondenceYesNoOption;
                              $scope.PracticeLocationDetails[index].PrimaryTaxId = data.practiceLocationDetail.PrimaryTaxId;
                              $scope.PracticeLocationDetails[index].PrimaryTax = data.practiceLocationDetail.PrimaryTax;
                              $scope.PracticeLocationDetails[index].PracticingGroupId = data.practiceLocationDetail.PracticingGroupId;
                              $scope.PracticeLocationDetails[index].PracticeLocationCorporateName = data.practiceLocationDetail.PracticeLocationCorporateName;


                              // updating the visibility control

                              $scope.GeneralInformationEdit = false;
                              $scope.PracticeLocationPendingRequest = true;

                              messageAlertEngine.callAlertMessage("UpdatePracticeLocation", "Practice Location Information updated successfully !!!!", "success", true);
                          } else {
                              messageAlertEngine.callAlertMessage("otherInformationErrorMsg", data.status, "danger", true);
                          }
                      },
                      error: function (e) {
                          messageAlertEngine.callAlertMessage("otherInformationErrorMsg", "Sorry for Inconvenience !!!! Please Try Again Later...", "danger", true);
                      }
                  });


              }
          };

          // Save Mid level Details
          $scope.saveMidLevel = function (PracticeLocationDetailID) {

              var validationStatus = null;
              var url = null;
              var $formData = null;
              $formData = $('#addnewMidLevel');

              validationStatus = $formData.valid();
              url = rootDir + "/Profile/PracticeLocation/addMidLevelAsync?practiceLocationDetailID=" + PracticeLocationDetailID + "&profileId=" + profileId;

              if (validationStatus) {
                  //Simple POST request example (passing data) :
                  $.ajax({
                      url: url,
                      type: 'POST',
                      data: new FormData($formData[0]),
                      async: false,
                      cache: false,
                      contentType: false,
                      processData: false,
                      success: function (data) {
                          if (data.status == "true") {
                              if ($scope.PracticeLocationDetails === null) { $scope.PracticeLocationDetails = [] };
                              $scope.PracticeLocationDetails.push(data.practiceLocationDetail);
                          } else {

                          }
                      },
                      error: function (e) {

                      }
                  });

              }
          };


          $scope.MidLevelProviderPractitioner = {};

          $scope.operateMidlevelAddControl = function (viewSwitch) {

              $scope.addmidlevelNew = true;
              if (viewSwitch == 'edit')
                  $scope.addmidlevelNew = false;
          };

          $scope.$watch('MidLevelProviderPractitioner.FirstName', function (newV, oldV) {
              if (newV == oldV) return;
              if (newV != "") {
                  $('#FnameMsg').hide();
              }
              else {
                  $('#FnameMsg').show();
              }
          })
          $scope.$watch('MidLevelProviderPractitioner.LastName', function (newV, oldV) {
              if (newV == oldV) return;
              if (newV != "") {
                  $('#LnameMsg').hide();
              }
              else {
                  $('#LnameMsg').show();
              }
          })
          $scope.validateMidLevelProviderPractitioner = function (dataObject, dataScope, action) {
              var status = true;

              var fname = '#FnameMsg';
              var mname = '#MnameMsg';
              var lname = '#LnameMsg';
              var npinumber = '#NPINumberMsg';

              if (dataScope == 'inner') {
                  fname = '#FnameMsgInner';
                  mname = '#MnameMsgInner';
                  lname = '#LnameMsgInner';
                  npinumber = '#NPINumberMsgInner';
              }

              $(fname).text("");
              $(mname).text("");
              $(lname).text("");
              $(npinumber).text("");

              var nameRegx = "^[a-zA-Z ,-.]*$";


              if (dataObject.FirstName == null || dataObject.FirstName == "") {
                  status = false;
                  $(fname).text("Please enter First Name.");
              }
              //else if (dataObject.FirstName.match(nameRegx) == null && (dataObject.FirstName != null || dataObject.FirstName != "")) {
              //    status = false;
              //    $(fname).text("Please enter valid First Name. Only alphabets, spaces, comma, hyphen and dot accepted.");
              //}
              //else if (dataObject.FirstName.length < 2 || dataObject.FirstName.length > 50) {
              //    status = false;
              //    $(fname).text("First Name should be between 2 to 50 characters in length.");
              //}


              if (dataObject.MiddleName == null || dataObject.MiddleName == "") {
                  $(mname).text("");
              }
              //else if (dataObject.MiddleName.match(nameRegx) == null && (dataObject.MiddleName != null || dataObject.MiddleName != "")) {
              //    status = false;
              //    $(mname).text("Please enter valid Middle Name. Only alphabets, spaces, comma, hyphen and dot accepted.");
              //}
              //else if (dataObject.MiddleName != null && (dataObject.MiddleName.length < 2 || dataObject.MiddleName.length > 50)) {
              //    status = false;
              //    $(mname).text("Middle Name should be between 2 to 50 characters in length.");
              //}



              if (dataObject.LastName == null || dataObject.LastName == "") {
                  status = false;
                  $(lname).text("Please enter Last Name.");
              }
              //else if (dataObject.LastName.match(nameRegx) == null && (dataObject.LastName != null || dataObject.LastName != "")) {
              //    status = false;
              //    $(lname).text("Please enter valid Last Name. Only alphabets, spaces, comma, hyphen and dot accepted.");
              //}
              //else if (dataObject.LastName.length < 2 || dataObject.LastName.length > 50) {
              //    status = false;
              //    $(lname).text("Last Name should be between 2 to 50 characters in length.");
              //}


              //if (dataObject.NPINumber == null || dataObject.NPINumber == "") {
              //    status = false;
              //    $(npinumber).text("Please enter NPI Number.");
              //        }
              if (isNaN(dataObject.NPINumber) && (dataObject.NPINumber != null || dataObject.NPINumber != "")) {
                  status = false;
                  $(npinumber).text("Please enter valid NPI Number. Only digits accepted.");
              }
              //else if ((dataObject.NPINumber != "" && dataObject.NPINumber.length != 10)||(dataObject.NPINumber != "null" && dataObject.NPINumber.length != 10)) {
              //    status = false;
              //    $(npinumber).text("NPI Number should be of 10 digits.");
              //}
              //else if (action == 'add' && $filter('filter')($rootScope.tempObject.FacilityDetail.FacilityPracticeProviders, { NPINumber: dataObject.NPINumber })[0] != null) {
              //    status = false;
              //    $(npinumber).text("Mid-Level Practioner with this NPI Number already added.");
              //}
              //else if (action == 'update' && $filter('filter')($rootScope.tempObject.FacilityDetail.FacilityPracticeProviders, { NPINumber: dataObject.NPINumber }).length > 1) {
              //    status = false;
              //    $(npinumber).text("Mid-Level Practioner with this NPI Number already added.");
              //}

              return status;
          };
          var TemporayFacilityObject = {};
          var TemporayFacilityObjectIndex = -1;
          var count1 = 0;
          $scope.RevertBackTemp = function (data, parentData) {
              if (count1 == 0) {
                  TemporayFacilityObject = angular.copy(data);
                  TemporayFacilityObjectIndex = $scope.PracticeLocationDetails.indexOf(parentData);
                  count1++;
              }
          };
          $scope.CancelFacilityForOfficeHour = function () {
              if (TemporayFacilityObjectIndex != -1) {
                  $scope.PracticeLocationDetails[TemporayFacilityObjectIndex].Facility = angular.copy(TemporayFacilityObject);
              }
          }


          $scope.addFacilityMidlevelPractioners = function (dataObject, dataScope) {
              if (!angular.isDefined($rootScope.tempObject.FacilityDetail.FacilityPracticeProviders)) {
                  $rootScope.tempObject.FacilityDetail.FacilityPracticeProviders = [];
              }
              if ($scope.validateMidLevelProviderPractitioner(dataObject, dataScope, 'add')) {
                  $rootScope.tempObject.FacilityDetail.FacilityPracticeProviders.push({
                      'FacilityPracticeProviderID': $scope.MidLevelProviderPractitioner.FacilityPracticeProviderID,
                      'PracticeType': 'Midlevel',
                      'RelationType': $scope.MidLevelProviderPractitioner.RelationType,
                      'FirstName': $scope.MidLevelProviderPractitioner.FirstName,
                      'MiddleName': $scope.MidLevelProviderPractitioner.MiddleName,
                      'LastName': $scope.MidLevelProviderPractitioner.LastName,
                      'NPINumber': $scope.MidLevelProviderPractitioner.NPINumber,
                      'StatusType': 'Active'
                  });

                  $scope.resetMidLevelProviderPractitioner();
              }

          };

          $scope.updateFacilityMidlevelPractioners = function (midLevels, tempThirdObject, index, dataScope) {
              if ($scope.validateMidLevelProviderPractitioner(tempThirdObject, dataScope, 'update')) {
                  midLevels[index] = tempThirdObject;

                  $rootScope.operateThirdCancelControl();
              }
          };

          $scope.setFacilityMidLevelPractitioner = function (practitioner) {
              if (angular.isObject(practitioner)) {
                  $scope.MidLevelProviderPractitioner.FirstName = practitioner.PersonalDetail.FirstName;
                  $scope.MidLevelProviderPractitioner.MiddleName = practitioner.PersonalDetail.MiddleName;
                  $scope.MidLevelProviderPractitioner.LastName = practitioner.PersonalDetail.LastName;
                  $scope.MidLevelProviderPractitioner.NPINumber = practitioner.OtherIdentificationNumber.NPINumber;
                  $(".ProviderTypeSelectAutoList").hide();
                  $scope.disableBit = true;
              }
          };

          $scope.removeMidLevelProviderPractitioner = function (practitioner, visibilityControlPracLoc) {

              if (visibilityControlPracLoc == "addPracticeLocationNew") {
                  $rootScope.tempObject.FacilityDetail.FacilityPracticeProviders.splice($rootScope.tempObject.FacilityDetail.FacilityPracticeProviders.indexOf(practitioner), 1);
              }
              else {
                  //practitioner.StatusType = "Inactive";
                  practitioner.StatusType = 2;
                  $rootScope.tempObject.FacilityDetail.FacilityPracticeProviders.splice($rootScope.tempObject.FacilityDetail.FacilityPracticeProviders.indexOf(practitioner), 1);
              }
              if ($rootScope.tempObject.FacilityDetail.FacilityPracticeProviders.length < 1) {
                  $scope.resetMidLevelProviderPractitioner();
              }
          };
          $scope.resetmidlevel = function () {
              $scope.addmidlevelNew = false;
          }
          $scope.resetMidLevelProviderPractitioner = function () {

              $rootScope.visibilityThirdControl = "";
              $scope.MidLevelProviderPractitioner = {};
              $scope.addmidlevelNew ? $scope.addmidlevelNew = false : $scope.addmidlevelNew = true;

          };

          //======================================Primary Credentialing Contact=================



          $scope.radioOption1 = function (index) {

              $scope.credDropDown = false;
              if ($scope.PracticeLocationDetails[index].BusinessOfficeManagerOrStaff != null) {

                  $rootScope.tempObject.FirstName = $scope.PracticeLocationDetails[index].BusinessOfficeManagerOrStaff.FirstName;
                  $rootScope.tempObject.MiddleName = $scope.PracticeLocationDetails[index].BusinessOfficeManagerOrStaff.MiddleName;
                  $rootScope.tempObject.LastName = $scope.PracticeLocationDetails[index].BusinessOfficeManagerOrStaff.LastName;
                  $rootScope.tempObject.Telephone = $scope.PracticeLocationDetails[index].BusinessOfficeManagerOrStaff.Telephone;
                  $rootScope.tempObject.CountryCodeTelephone = $scope.PracticeLocationDetails[index].BusinessOfficeManagerOrStaff.CountryCodeTelephone;
                  $rootScope.tempObject.CountryCodeFax = $scope.PracticeLocationDetails[index].BusinessOfficeManagerOrStaff.CountryCodeFax;
                  $rootScope.tempObject.Fax = $scope.PracticeLocationDetails[index].BusinessOfficeManagerOrStaff.Fax;
                  $rootScope.tempObject.EmailAddress = $scope.PracticeLocationDetails[index].BusinessOfficeManagerOrStaff.EmailAddress;
                  $rootScope.tempObject.Building = $scope.PracticeLocationDetails[index].Facility.Building;
                  $rootScope.tempObject.City = $scope.PracticeLocationDetails[index].Facility.City;
                  $rootScope.tempObject.Country = $scope.PracticeLocationDetails[index].Facility.Country;
                  $rootScope.tempObject.Street = $scope.PracticeLocationDetails[index].Facility.Street;
                  $rootScope.tempObject.ZipCode = $scope.PracticeLocationDetails[index].Facility.ZipCode;
                  $rootScope.tempObject.State = $scope.PracticeLocationDetails[index].Facility.State;
                  $rootScope.tempObject.County = $scope.PracticeLocationDetails[index].Facility.County;

              }

          }

          $scope.radioOption3 = function () {

              $scope.credDropDown = false;

              $rootScope.tempObject.FirstName = "";
              $rootScope.tempObject.MiddleName = "";
              $rootScope.tempObject.LastName = "";
              $rootScope.tempObject.Telephone = "";
              $rootScope.tempObject.Fax = "";
              $rootScope.tempObject.EmailAddress = "";
              $rootScope.tempObject.Building = "";
              $rootScope.tempObject.City = "";
              $rootScope.tempObject.Country = "";
              $rootScope.tempObject.Street = "";
              $rootScope.tempObject.ZipCode = "";
              $rootScope.tempObject.State = "";
              $rootScope.tempObject.County = "";
              $rootScope.tempObject.CountryCodeTelephone = "";
              $rootScope.tempObject.CountryCodeFax = "";
          }




          $scope.radioOption2 = function () {

              //$('#credContact').val('-1').attr("selected", "selected");               
              $rootScope.tempObject.FirstName = "";
              $rootScope.tempObject.MiddleName = "";
              $rootScope.tempObject.LastName = "";
              $rootScope.tempObject.Telephone = "";
              $rootScope.tempObject.Fax = "";
              $rootScope.tempObject.EmailAddress = "";
              $rootScope.tempObject.Building = "";
              $rootScope.tempObject.City = "";
              $rootScope.tempObject.Country = "";
              $rootScope.tempObject.Street = "";
              $rootScope.tempObject.ZipCode = "";
              $rootScope.tempObject.State = "";
              $rootScope.tempObject.County = "";
              $rootScope.tempObject.CountryCodeTelephone = "";
              $rootScope.tempObject.CountryCodeFax = "";
              $scope.credDropDown = true;
          }

          $scope.getDropDownValue = function (empObj) {
              empObj = $filter('filter')($scope.credentialingContact, { MasterEmployeeID: empObj })[0];

              $rootScope.tempObject.FirstName = empObj.FirstName;
              $rootScope.tempObject.MiddleName = empObj.MiddleName;
              $rootScope.tempObject.LastName = empObj.LastName;
              $rootScope.tempObject.Telephone = empObj.Telephone;
              $rootScope.tempObject.CountryCodeTelephone = empObj.CountryCodeTelephone;
              $rootScope.tempObject.Fax = empObj.Fax;
              $rootScope.tempObject.CountryCodeFax = empObj.CountryCodeFax;
              $rootScope.tempObject.EmailAddress = empObj.EmailAddress;
              $rootScope.tempObject.Building = empObj.Building;
              $rootScope.tempObject.City = empObj.City;
              $rootScope.tempObject.Country = empObj.Country;
              $rootScope.tempObject.Street = empObj.Street;
              $rootScope.tempObject.ZipCode = empObj.ZipCode;
              $rootScope.tempObject.State = empObj.State;
              $rootScope.tempObject.County = empObj.County;


          }

          $scope.cancelCredendialingContact = function () {

              $scope.credDropDown = false;
              $rootScope.operateCancelControl('');

          }


          $scope.saveCredentialingContact = function (index) {

              var validationStatus = false;
              var url = null;
              var formData = null;
              $scope.credDropDown = false;

              if ($scope.visibilityControl == 'addCc') {
                  //Add Details - Denote the URL
                  formData = $('#newShowCredentialingContactDiv').find('form');
                  url = rootDir + "/Profile/PracticeLocation/AddCredentialingContact?profileId=" + profileId;
              }
              else if ($scope.visibilityControl == ('editCc')) {
                  //Update Details - Denote the URL
                  formData = $('#CredentialingContactEditDiv').find('form');
                  url = rootDir + "/Profile/PracticeLocation/UpdateCredentialingContact?profileId=" + profileId;
              }

              ResetFormForValidation(formData);
              validationStatus = formData.valid()


              if (validationStatus) {

                  $.ajax({
                      url: url,
                      type: 'POST',
                      data: new FormData(formData[0]),
                      async: false,
                      cache: false,
                      contentType: false,
                      processData: false,
                      success: function (data) {


                          if (data.status == "true") {


                              if ($scope.visibilityControl == 'addCc') {

                                  $scope.PracticeLocationDetails[index].PrimaryCredentialingContactPerson = data.credentialingContact;
                                  $rootScope.operateCancelControl('');
                                  messageAlertEngine.callAlertMessage("addedNewCredentialingContactInformation", "Primary Credentialing Contact added successfully !!!!", "success", true);
                              }


                              else {
                                  $scope.PracticeLocationPendingRequest = true;
                                  $scope.PracticeLocationDetails[index].PrimaryCredentialingContactPerson = data.credentialingContact;
                                  $rootScope.operateCancelControl('');
                                  messageAlertEngine.callAlertMessage("updatedCredentialingContactInformation", "Primary Credentialing Contact updated successfully !!!!", "success", true);

                              }

                              FormReset(formData);

                          }



                          else {
                              messageAlertEngine.callAlertMessage('errorCredentialingContractInformation', "", "danger", true);
                              $scope.errorCredentialingContractInformation = data.status.split(",");

                          }

                      },

                      error: function (e) {
                          messageAlertEngine.callAlertMessage('errorCredentialingContractInformation' + index, "", "danger", true);
                          $scope.errorCredentialingContractInformation = "Sorry for Inconvenience !!!! Please Try Again Later...";
                      }

                  });

              }
              loadingOff();

          };











          //-----------------------------------------------------------------------------------------------------------------------------//
          //------------------------------------------------------ Mid Level Practitioners ----------------------------------------------//
          //-----------------------------------------------------------------------------------------------------------------------------//

          $scope.tempMidLevelPractitionersList = [];

          $scope.addMidLevelToTempList = function (practitioner) {
              $scope.tempMidLevelPractitionersList.push(practitioner);
          };

          $scope.removeFromMidLevelTempList = function (practitioner) {
              $scope.tempMidLevelPractitionersList.splice($scope.tempMidLevelPractitionersList.indexOf(practitioner), 1);
          };

          //$scope.setMidLevelPractitioner = function (practitioner) {
          //    if (angular.isObject(practitioner)) {
          //        $scope.tempThirdObject.FirstName = practitioner.PersonalDetail.FirstName;
          //        $scope.tempThirdObject.MiddleName = practitioner.PersonalDetail.MiddleName;
          //        $scope.tempThirdObject.LastName = practitioner.PersonalDetail.LastName;
          //        $scope.tempThirdObject.NPINumber = practitioner.OtherIdentificationNumber.NPINumber;
          //        $(".ProviderTypeSelectAutoList").hide();
          //    }
          //};

          $scope.setMidLevelPractitioner = function (practitioner) {
              if (angular.isObject(practitioner)) {
                  $scope.tempThirdObject.FirstName = practitioner.FirstName;
                  $scope.tempThirdObject.MiddleName = practitioner.MiddleName;
                  $scope.tempThirdObject.LastName = practitioner.LastName;
                  $scope.tempThirdObject.NPINumber = practitioner.NPINumber;
                  $(".ProviderTypeSelectAutoList").hide();
              }
          };

          $scope.resetMidLevelPractitioner = function () {
              $scope.MidLevelPractitioner = {};
              $scope.tempMidLevelPractitionersList = [];
          };

          $scope.initWarning = function (practitioner) {
              $($('#warningModal').find('button')[2]).attr('disabled', false);
              if (angular.isObject(practitioner)) {
                  $scope.tempMidLevelpractitioner = practitioner;
              }
              $('#warningModal').modal();
          };

          $scope.addMultipleMidlevelPractioners = function (PracticeLocationDetail, index) {
              var validationStatus = false;
              var url = null;
              var $formData = null;

              $formData = $('#MidLevelPractitionerForm' + index);

              url = rootDir + "/Profile/PracticeLocation/AddPracticeProvidersAsync";

              ResetFormForValidation($formData);

              validationStatus = $formData.valid();

              if (validationStatus) {
                  //Simple POST request example (passing data) :
                  $.ajax({
                      url: url,
                      type: 'POST',
                      data: new FormData($formData[0]),
                      async: false,
                      cache: false,
                      contentType: false,
                      processData: false,
                      success: function (data) {
                          if (data.status == "true") {
                              if (PracticeLocationDetail.MidlevelPractioners == null)
                                  PracticeLocationDetail.MidlevelPractioners = [];

                              PracticeLocationDetail.MidlevelPractioners = data.practiceProviders;
                              $rootScope.operateSecondCancelControl('');

                              $scope.resetMidLevelPractitioner();

                              messageAlertEngine.callAlertMessage("midlevelPractionersSuccessMsg", "Supervising Practitioners Added successfully.", "success", true);


                          } else {
                              messageAlertEngine.callAlertMessage("midlevelPractionersErrorMsg", data.status, "danger", true);
                          }
                          $scope.tempMidLevelPractitionersList = [];
                      },
                      error: function (e) {

                      }
                  });


              }
          };


          $scope.addMidlevelPractioners = function (PracticeLocationDetail, index) {
              var validationStatus = false;
              var url = null;
              var $formData = null;

              $formData = $('#MidLevelPractitionerForm' + index);

              url = rootDir + "/Profile/PracticeLocation/AddPracticeProviderAsync";
              if ($scope.tempThirdObject.PracticeProviderID) {
                  url = rootDir + "/Profile/PracticeLocation/UpdatePracticeProviderAsync?profileId=" + profileId;
              }

              ResetFormForValidation($formData);

              validationStatus = $formData.valid();

              if (validationStatus) {
                  //Simple POST request example (passing data) :
                  $.ajax({
                      url: url,
                      type: 'POST',
                      data: new FormData($formData[0]),
                      async: false,
                      cache: false,
                      contentType: false,
                      processData: false,
                      success: function (data) {
                          if (data.status == "true") {
                              if ($scope.tempThirdObject.PracticeProviderID) {
                                  PracticeLocationDetail.MidlevelPractioners[index] = data.practiceProvider;
                                  $rootScope.operateThirdViewAndAddControl(index + '_ViewMidLevelPractitioner');
                                  messageAlertEngine.callAlertMessage("midlevelPractionersEditSuccessMsg", "Supervising Practitioner Updated successfully.", "success", true);
                              } else {
                                  if (PracticeLocationDetail.MidlevelPractioners == null)
                                      PracticeLocationDetail.MidlevelPractioners = [];


                                  PracticeLocationDetail.MidlevelPractioners.push(data.practiceProvider);
                                  $rootScope.operateSecondCancelControl('');

                                  $scope.resetMidLevelPractitioner();

                                  messageAlertEngine.callAlertMessage("midlevelPractionersSuccessMsg", "Mid-level Practitioner Added successfully.", "success", true);
                              }

                          } else {
                              messageAlertEngine.callAlertMessage("midlevelPractionersErrorMsg", data.status, "danger", true);
                          }
                      },
                      error: function (e) {

                      }
                  });


              }
          };


          $scope.removeMidLevelPractitioner = function (PracticeLocationDetail) {
              var validationStatus = false;
              $($('#warningModal').find('button')[2]).attr('disabled', true);
              var url = null;
              var $formData = null;

              $formData = $('#editMidlevelPractioner');

              url = rootDir + "/Profile/PracticeLocation/RemovePracticeProviderAsync";

              ResetFormForValidation($formData);

              validationStatus = $formData.valid();

              if (validationStatus) {
                  //Simple POST request example (passing data) :
                  $.ajax({
                      url: url,
                      type: 'POST',
                      data: new FormData($formData[0]),
                      async: false,
                      cache: false,
                      contentType: false,
                      processData: false,
                      success: function (data) {
                          if (data.status == "true") {
                              var obj = $filter('filter')(PracticeLocationDetail.MidlevelPractioners, { PracticeProviderID: data.practiceProvider.PracticeProviderID })[0];
                              PracticeLocationDetail.MidlevelPractioners.splice(PracticeLocationDetail.MidlevelPractioners.indexOf(obj), 1);
                              $('#warningModal').modal('hide');
                              PracticeLocationDetail.MidlevelPractioners.splice();
                              $rootScope.operateThirdCancelControl('');
                              messageAlertEngine.callAlertMessage("midlevelPractionersSuccessMsg", "Supervising Practitioner Removed successfully.", "success", true);
                          } else {
                              messageAlertEngine.callAlertMessage("midlevelPractionersRemoveErrorMsg", data.status, "danger", true);
                          }
                      },
                      error: function (e) {

                      }
                  });


              }
          };

          $scope.toggleList = function () {
              $(".ProviderTypeSelectAutoList").show();
          };




          //-----------------------------------------------------------------------------------------------------------------------------//
          //------------------------------------------------------ Supervising Providers ----------------------------------------------//
          //-----------------------------------------------------------------------------------------------------------------------------//


          $scope.setSupervisingProvider = function (practitioner) {
              if (angular.isObject(practitioner)) {
                  $scope.tempThirdObject.FirstName = practitioner.PersonalDetail.FirstName;
                  $scope.tempThirdObject.MiddleName = practitioner.PersonalDetail.MiddleName;
                  $scope.tempThirdObject.LastName = practitioner.PersonalDetail.LastName;
                  $scope.tempThirdObject.NPINumber = practitioner.OtherIdentificationNumber.NPINumber;
                  $(".ProviderTypeSelectAutoList").hide();
              }
          };

          $scope.resetSupervisingProvider = function () {
              $scope.SupervisingProvider = {};
          };

          $scope.initSupervisingWarning = function (practitioner) {
              $($('#myModalLabel').find('button')[2]).attr('disabled', false);

              if (angular.isObject(practitioner)) {
                  $scope.tempSupervisingProvider = practitioner;
              }
              $('#supervisingWarningModal').modal();
          };


          $scope.addSupervisingProvider = function (PracticeLocationDetail, index) {
              var validationStatus = false;
              var url = null;
              var $formData = null;

              $formData = $('#SupervisingProvider' + index);

              url = rootDir + "/Profile/PracticeLocation/AddPracticeProviderAsync";
              if ($scope.tempThirdObject.PracticeProviderID) {
                  url = rootDir + "/Profile/PracticeLocation/UpdatePracticeProviderAsync?profileId=" + profileId;
              }

              ResetFormForValidation($formData);

              validationStatus = $formData.valid();

              if (validationStatus) {
                  //Simple POST request example (passing data) :
                  $.ajax({
                      url: url,
                      type: 'POST',
                      data: new FormData($formData[0]),
                      async: false,
                      cache: false,
                      contentType: false,
                      processData: false,
                      success: function (data) {
                          if (data.status == "true") {
                              if ($scope.tempThirdObject.PracticeProviderID) {
                                  PracticeLocationDetail.SupervisingProviders[index] = data.practiceProvider;
                                  $rootScope.operateThirdViewAndAddControl(index + '_ViewSupervisingPractitioner');
                                  messageAlertEngine.callAlertMessage("supervisingProvidersEditSuccessMsg", "Supervising Provider Updated successfully.", "success", true);
                              } else {
                                  if (PracticeLocationDetail.SupervisingProviders == null)
                                      PracticeLocationDetail.SupervisingProviders = [];


                                  PracticeLocationDetail.SupervisingProviders.push(data.practiceProvider);
                                  $rootScope.operateSecondCancelControl('');

                                  $scope.resetSupervisingProvider();

                                  messageAlertEngine.callAlertMessage("supervisingProvidersSuccessMsg", "Supervising Provider Added successfully.", "success", true);
                              }
                          } else {
                              messageAlertEngine.callAlertMessage("supervisingProvidersErrorMsg", data.status, "danger", true);
                          }
                      },
                      error: function (e) {

                      }
                  });


              }
          };


          $scope.removeSupervisingProvider = function (PracticeLocationDetail) {
              var validationStatus = false;
              $($('#myModalLabel').find('button')[2]).attr('disabled', true);
              var url = null;
              var $formData = null;

              $formData = $('#editSupervisingPractioner');

              url = rootDir + "/Profile/PracticeLocation/RemovePracticeProviderAsync";

              ResetFormForValidation($formData);

              validationStatus = $formData.valid();

              if (validationStatus) {
                  //Simple POST request example (passing data) :
                  $.ajax({
                      url: url,
                      type: 'POST',
                      data: new FormData($formData[0]),
                      async: false,
                      cache: false,
                      contentType: false,
                      processData: false,
                      success: function (data) {
                          if (data.status == "true") {
                              var obj = $filter('filter')(PracticeLocationDetail.SupervisingProviders, { PracticeProviderID: data.practiceProvider.PracticeProviderID })[0];
                              PracticeLocationDetail.SupervisingProviders.splice(PracticeLocationDetail.SupervisingProviders.indexOf(obj), 1);
                              $('#supervisingWarningModal').modal('hide');
                              PracticeLocationDetail.SupervisingProviders.splice();
                              $rootScope.operateThirdCancelControl('');
                              messageAlertEngine.callAlertMessage("supervisingProvidersSuccessMsg", "Supervising Provider Removed successfully.", "success", true);
                          } else {
                              $('#supervisingWarningModal').modal('hide');
                              messageAlertEngine.callAlertMessage("supervisingProvidersRemoveErrorMsg", data.status, "danger", true);
                          }
                      },
                      error: function (e) {

                      }
                  });


              }
          };



          //---------------------------------------------------------------------------------------------------------------------------------------------------//

          $scope.fillCorporateName = function (val) {
              var selectedFacility = $filter('filter')($scope.facilities, { FacilityID: val })[0];
              $scope.tempSecondObject.PracticeLocationCorporateName = selectedFacility.Name;
          }

          $scope.assignCityComponentForFacility = function () {
              $scope.tempSecondObject.City = $rootScope.tempObject.City;
              $scope.tempSecondObject.State = $rootScope.tempObject.State;
              $scope.tempSecondObject.Country = $rootScope.tempObject.Country;
          }


          //-----------------------------------------------------------------------------------------------------------------------------//
          //------------------------------------------------------ Covering Colleagues ----------------------------------------------//
          //-----------------------------------------------------------------------------------------------------------------------------//


          //$scope.tempSecondObject = {};

          //To Display the drop down div
          $scope.searchCumDropDown = function (event) {
              $(event.target).parent().find(".ProviderTypeSelectAutoList").first().show();
          };

          $scope.showContryCodeDivForColleague = function (countryCodeDivId) {
              changeVisibilityOfCountryCode();
              $("#" + countryCodeDivId).show();
          };

          //Bind the supervisor with model class to achieve search cum drop down
          $scope.addIntoSupervisorDropDown = function (supervisor, $event) {
              $scope.tempSecondObject.NPINumber = supervisor.OtherIdentificationNumber.NPINumber;
              $scope.tempSecondObject.FirstName = supervisor.PersonalDetail.FirstName;
              $scope.tempSecondObject.MiddleName = supervisor.PersonalDetail.MiddleName;
              $scope.tempSecondObject.LastName = supervisor.PersonalDetail.LastName;

              $scope.PracticeSpecialties = [];

              for (var i = 0; i < supervisor.SpecialtyDetails.length; i++) {
                  $scope.PracticeSpecialties.push({
                      PracticeProviderSpecialtyId: null,
                      Specialty: supervisor.SpecialtyDetails[i].Specialty,
                      SpecialtyID: supervisor.SpecialtyDetails[i].SpecialtyID,
                      StatusType: 1
                  });
              }

              $scope.PracticeProviderTypes = [];

              for (var i = 0; i < supervisor.PersonalDetail.ProviderTitles.length; i++) {
                  $scope.PracticeProviderTypes.push({
                      PracticeProviderTypeId: null,
                      ProviderType: supervisor.PersonalDetail.ProviderTitles[i].ProviderType,
                      ProviderTypeID: supervisor.PersonalDetail.ProviderTitles[i].ProviderTypeId,
                      StatusType: 1

                  });
              }

              for (var i = 0; i < supervisor.PracticeLocationDetails.length; i++) {
                  if (supervisor.PracticeLocationDetails[i].PrimaryYesNoOption == 1) {
                      $scope.tempSecondObject.Street = supervisor.PracticeLocationDetails[i].Facility.Street;
                      $scope.tempSecondObject.Building = supervisor.PracticeLocationDetails[i].Facility.Building;
                      $scope.tempSecondObject.City = supervisor.PracticeLocationDetails[i].Facility.City;
                      $scope.tempSecondObject.State = supervisor.PracticeLocationDetails[i].Facility.State;
                      $scope.tempSecondObject.ZipCode = supervisor.PracticeLocationDetails[i].Facility.ZipCode;
                      $scope.tempSecondObject.Country = supervisor.PracticeLocationDetails[i].Facility.Country;
                      $scope.tempSecondObject.County = supervisor.PracticeLocationDetails[i].Facility.County;
                      $scope.tempSecondObject.CountryCodeTelephone = supervisor.PracticeLocationDetails[i].Facility.CountryCodeTelephone;
                      $scope.tempSecondObject.Telephone = supervisor.PracticeLocationDetails[i].Facility.Telephone;
                  }
              }


              $(".ProviderTypeSelectAutoList").hide();
          }

          //$scope.setSupervisor = function (practitioner) {
          //    if (angular.isObject(practitioner)) {
          //        $scope.tempSecondObject = practitioner;
          //        $(".ProviderTypeSelectAutoList").hide();
          //    }
          //};



          //$scope.resetSupervisor = function () {
          //    $scope.tempSecondObject = {};
          //};

          //For Address
          $scope.addressAutocomplete3 = function (location3) {
              if (location3.length == 0) {
                  $scope.resetAddressModels3();
              }
              $scope.tempSecondObject.City = location3;
              if (location3.length > 1 && !angular.isObject(location3)) {
                  locationService.getLocations(location3).then(function (val) {
                      $scope.Locations = val;
                  });
              } else if (angular.isObject(location3)) {
                  $scope.setAddressModels3(location3);
              }
          };

          $scope.selectedLocation3 = function (location3) {
              $scope.setAddressModels3(location3);
              $(".ProviderTypeSelectAutoList").hide();
          };

          $scope.resetAddressModels3 = function () {
              $scope.tempSecondObject.City = "";
              $scope.tempSecondObject.State = "";
              $scope.tempSecondObject.Country = "";
          };

          $scope.setAddressModels3 = function (location3) {

              $scope.tempSecondObject.City = location3.City;
              $scope.tempSecondObject.State = location3.State;
              $scope.tempSecondObject.Country = location3.Country;

          };


          //Selecting multiple provider types    

          $scope.showProviderTypeList = function (event) {
              $(event.target).parent().find(".ProviderTypeSelectAutoList").first().show();
          };

          $scope.PracticeProviderTypes = [];

          $scope.SelectProviderType = function (providertype) {

              $scope.PracticeProviderTypes.push({
                  PracticeProviderTypeId: null,
                  ProviderType: providertype,
                  ProviderTypeID: providertype.ProviderTypeID,
                  StatusType: 1
              });

              $(".ProviderTypeSelectAutoList").hide();
              $scope.masterProviderTypes.splice($scope.masterProviderTypes.indexOf(providertype), 1);
          };

          //------------------------------------- UN-select Provider type -----------------------------------------
          $scope.ActionProviderType = function (providertype) {

              for (var i = 0; i < $scope.PracticeProviderTypes.length; i++) {
                  if ($scope.PracticeProviderTypes[i].ProviderTypeID == providertype.ProviderTypeID) {
                      $scope.PracticeProviderTypes[i].StatusType = 2;
                      $scope.masterProviderTypes.push(providertype);
                  }
              }

              //$scope.PracticeProviderTypes.splice($scope.PracticeProviderTypes.indexOf(providertype), 1);


          };

          $scope.PracticeSpecialties = [];
          //$scope.tempMasterSpecialties = [];
          //$scope.tempMasterSpecialties = angular.copy($scope.masterSpecialties);

          $scope.SelectSpecalties = function (specialty) {

              $scope.PracticeSpecialties.push({
                  PracticeProviderSpecialtyId: null,
                  Specialty: specialty,
                  SpecialtyID: specialty.SpecialtyID,
                  StatusType: 1
              });
              $(".ProviderTypeSelectAutoList").hide();
              $scope.masterSpecialties.splice($scope.masterSpecialties.indexOf(specialty), 1);
          };

          //------------------------------------- UN-select Provider type -----------------------------------------
          $scope.ActionSpecialty = function (specialty) {

              for (var i = 0; i < $scope.PracticeSpecialties.length; i++) {
                  if ($scope.PracticeSpecialties[i].SpecialtyID == specialty.SpecialtyID) {
                      $scope.PracticeSpecialties[i].StatusType = 2;
                      $scope.masterSpecialties.push(specialty);
                  }
              }

              //$scope.PracticeSpecialties.splice($scope.PracticeSpecialties.indexOf(specialty), 1);

          };

          $scope.setValue = function (partner) {

              $scope.PracticeSpecialties = [];
              $scope.PracticeProviderTypes = [];

              for (var i = 0; i < partner.PracticeProviderSpecialties.length; i++) {
                  if (partner.PracticeProviderSpecialties[i].StatusType == 1) {
                      $scope.PracticeSpecialties.push({
                          PracticeProviderSpecialtyId: partner.PracticeProviderSpecialties[i].PracticeProviderSpecialtyId,
                          Specialty: partner.PracticeProviderSpecialties[i].Specialty,
                          SpecialtyID: partner.PracticeProviderSpecialties[i].SpecialtyID,
                          StatusType: partner.PracticeProviderSpecialties[i].StatusType
                      });
                  }

              }

              for (var i = 0; i < partner.PracticeProviderTypes.length; i++) {
                  if (partner.PracticeProviderTypes[i].StatusType == 1) {
                      $scope.PracticeProviderTypes.push({
                          PracticeProviderTypeId: partner.PracticeProviderTypes[i].PracticeProviderTypeId,
                          ProviderType: partner.PracticeProviderTypes[i].ProviderType,
                          ProviderTypeID: partner.PracticeProviderTypes[i].ProviderTypeID,
                          StatusType: partner.PracticeProviderTypes[i].StatusType

                      });
                  }


              }

          }

          $scope.clearAll = function () {
              $scope.PracticeSpecialties = [];
              $scope.PracticeProviderTypes = [];
          }

          // Save Covering Colleagues Details
          $scope.savePatner = function (PracticeLocationDetail, index) {
              //$scope.tempMasterSpecialties = [];
              //$scope.tempMasterSpecialties = angular.copy($scope.masterSpecialties);
              var validationStatus = true;
              var url = null;
              var $formData = null;

              $formData = $('#newShowPartnersDiv').find('form');
              url = rootDir + "/Profile/PracticeLocation/AddPracticeProviderAsync";

              if ($scope.tempSecondObject.PracticeProviderID) {
                  $formData = $('#partnersEditDiv' + index).find('form');
                  url = rootDir + "/Profile/PracticeLocation/UpdatePracticeProviderAsync?profileId=" + profileId;
              }

              ResetFormForValidation($formData);

              validationStatus = $formData.valid();

              if (validationStatus) {

                  //Simple POST request example (passing data) :
                  $.ajax({
                      url: url,
                      type: 'POST',
                      data: new FormData($formData[0]),
                      async: false,
                      cache: false,
                      contentType: false,
                      processData: false,
                      success: function (data) {
                          if (data.status == "true") {
                              if ($scope.tempSecondObject.PracticeProviderID) {
                                  tempPracticeProviderSpecialties = [];
                                  for (var i = 0; i < $scope.PracticeSpecialties.length; i++) {
                                      if ($scope.PracticeSpecialties[i].StatusType == '1') {
                                          tempPracticeProviderSpecialties.push($scope.PracticeSpecialties[i]);
                                          //data.practiceProvider.PracticeProviderSpecialties[i].Specialty = $scope.PracticeSpecialties[i].Specialty;
                                      }
                                  }
                                  data.practiceProvider.PracticeProviderSpecialties = tempPracticeProviderSpecialties;

                                  tempPracticeProviderTypes = [];
                                  for (var i = 0; i < $scope.PracticeProviderTypes.length; i++) {
                                      if ($scope.PracticeProviderTypes[i].StatusType == '1') {
                                          tempPracticeProviderTypes.push($scope.PracticeProviderTypes[i]);
                                          //data.practiceProvider.PracticeProviderTypes[i].ProviderType = $scope.PracticeProviderTypes[i].ProviderType;
                                      }
                                  }
                                  data.practiceProvider.PracticeProviderTypes = tempPracticeProviderTypes;

                                  $scope.masterSpecialties = [];
                                  $scope.masterSpecialties = angular.copy($scope.copyMasterSpecialties);
                                  $scope.masterProviderTypes = [];
                                  $scope.masterProviderTypes = angular.copy($scope.copyMasterProviderTypes);
                                  PracticeLocationDetail.PracticeColleagues[index] = data.practiceProvider;
                                  $rootScope.operateSecondViewAndAddControl(index + '_viewPartner');
                                  $scope.PracticeLocationPendingRequest = true;
                                  messageAlertEngine.callAlertMessage("updatedPartnerDetails" + index, "Covering Colleagues information Updated successfully.", "success", true);
                              } else {
                                  if (PracticeLocationDetail.PracticeColleagues == null)
                                      PracticeLocationDetail.PracticeColleagues = [];

                                  var obj = $filter('filter')(PracticeLocationDetail.PracticeColleagues, { PracticeProviderID: data.practiceProvider.PracticeProviderID })[0];

                                  for (var i = 0; i < $scope.PracticeSpecialties.length; i++) {
                                      if ($scope.PracticeSpecialties[i].StatusType == 1) {
                                          try {
                                              data.practiceProvider.PracticeProviderSpecialties[i].Specialty = $scope.PracticeSpecialties[i].Specialty;
                                          }
                                          catch (e) {
                                          }
                                      }

                                  }

                                  for (var i = 0; i < $scope.PracticeProviderTypes.length; i++) {
                                      if ($scope.PracticeProviderTypes[i].StatusType == 1) {
                                          data.practiceProvider.PracticeProviderTypes[i].ProviderType = $scope.PracticeProviderTypes[i].ProviderType;
                                      }

                                  }
                                  $scope.masterSpecialties = [];
                                  $scope.masterSpecialties = angular.copy($scope.copyMasterSpecialties);
                                  $scope.masterProviderTypes = [];
                                  $scope.masterProviderTypes = angular.copy($scope.copyMasterProviderTypes);
                                  PracticeLocationDetail.PracticeColleagues.push(data.practiceProvider);
                                  $rootScope.operateSecondCancelControl('');
                                  //$scope.tempSecondObject = {};
                                  $scope.PracticeSpecialties = [];
                                  $scope.PracticeProviderTypes = [];
                                  messageAlertEngine.callAlertMessage("addedPartnerDetails", "Covering Colleagues Information Added successfully.", "success", true);
                              }
                          } else {
                              messageAlertEngine.callAlertMessage("ErrorInPartners" + index, data.status, "danger", true);
                          }
                      },
                      error: function (e) {

                      }
                  });

              }
          };


          $scope.initWarningMethod = function (practitioner) {
              $($('#warningModalMethod').find('button')[2]).attr('disabled', false);
              if (angular.isObject(practitioner)) {
                  $scope.tempPracticeColleague = practitioner;
              }
              $('#warningModalMethod').modal();
          };


          // remove Covering Colleagues Details    
          $scope.removePatner = function (PracticeLocationDetail) {

              var validationStatus = false;
              $($('#warningModalMethod').find('button')[2]).attr('disabled', true);
              var url = null;
              var $formData = null;

              $formData = $('#editPracticeColleague');

              url = rootDir + "/Profile/PracticeLocation/RemovePracticeProviderAsync";

              ResetFormForValidation($formData);

              validationStatus = $formData.valid();

              if (validationStatus) {
                  //Simple POST request example (passing data) :
                  $.ajax({
                      url: url,
                      type: 'POST',
                      data: new FormData($formData[0]),
                      async: false,
                      cache: false,
                      contentType: false,
                      processData: false,
                      success: function (data) {
                          if (data.status == "true") {
                              var obj = $filter('filter')(PracticeLocationDetail.PracticeColleagues, { PracticeProviderID: data.practiceProvider.PracticeProviderID })[0];
                              PracticeLocationDetail.PracticeColleagues.splice(PracticeLocationDetail.PracticeColleagues.indexOf(obj), 1);
                              $('#warningModalMethod').modal('hide');
                              PracticeLocationDetail.PracticeColleagues.splice();
                              $rootScope.operateSecondCancelControl('');
                              messageAlertEngine.callAlertMessage("addedPartnerDetails", "Covering Colleague Removed successfully.", "success", true);
                          } else {
                              messageAlertEngine.callAlertMessage("ErrorInPartners", data.status, "danger", true);
                          }
                      },
                      error: function (e) {
                      }
                  });
              }
          };

          //----clear textboxes when clicked on NO for Add Mid-Level Practitioner
          $scope.cleartextboxes = function () {
              $scope.MidLevelProviderPractitioner.FirstName = "";
              $scope.MidLevelProviderPractitioner.LastName = "";
              $scope.MidLevelProviderPractitioner.MiddleName = "";
              $scope.MidLevelProviderPractitioner.NPINumber = "";
          }

          //---------------------------------Removal of Practice Location--------------------------------

          $scope.initPracticeLocationWarning = function (PracticeLocationDetail) {
              $($('#practiceLocationWarningModal').find('button')[2]).attr('disabled', false);
              if (angular.isObject(PracticeLocationDetail)) {
                  $scope.tempPracticeLocation = PracticeLocationDetail;
              }
              $('#practiceLocationWarningModal').modal();
          };
          $scope.removePracticeLocation = function (PracticeLocationDetails) {
              var validationStatus = false;
              $($('#practiceLocationWarningModal').find('button')[2]).attr('disabled', true);
              var url = null;
              var $formData = null;
              $formData = $('#removePracticeLocation');
              url = rootDir + "/Profile/PracticeLocation/RemovePracticeLocationAsync?profileId=" + profileId;
              ResetFormForValidation($formData);
              validationStatus = $formData.valid();

              if (validationStatus) {
                  //Simple POST request example (passing data) :
                  $.ajax({
                      url: url,
                      type: 'POST',
                      data: new FormData($formData[0]),
                      async: false,
                      cache: false,
                      contentType: false,
                      processData: false,
                      success: function (data) {
                          $scope.showLoading = false;
                          if (data.status == "true") {
                              var obj = $filter('filter')(PracticeLocationDetails, { PracticeLocationDetailID: data.practiceLocation.PracticeLocationDetailID })[0];
                              PracticeLocationDetails.splice(PracticeLocationDetails.indexOf(obj), 1);
                              if ($scope.PracticeLocationDetailsHistory.length != 0) {
                                  obj.HistoryStatus = 'Deleted';

                                  $scope.PracticeLocationDetailsHistory.push(obj);
                                  //$scope.PracticeLocationDetailsHistory.Facility.FacilityDetail.PracticeOfficeHour = angular.copy(TimeConversionForOfficeHour(obj.Facility.FacilityDetail.PracticeOfficeHour));
                              }
                              $('#practiceLocationWarningModal').modal('hide');
                              $scope.operateViewControlPracLoc('');
                              messageAlertEngine.callAlertMessage("addedNewFacility", "Practice Location Detail Removed successfully.", "success", true);
                          } else {
                              $('#practiceLocationWarningModal').modal('hide');
                              messageAlertEngine.callAlertMessage("removePracticeLocation", data.status, "danger", true);
                              $scope.errorPracticeLocation = "Sorry for Inconvenience !!!! Please Try Again Later...";
                          }
                      },
                      error: function (e) {
                      }
                  });
              }
          };

          $rootScope.PracticeLocationLoaded = true;
          $scope.dataLoaded = false;
          //$rootScope.$on('PracticeLocation', function () {
          //    if (!$scope.dataLoaded) {
          //        $rootScope.PracticeLocationLoaded = false;
          //        $http({
          //            method: 'GET',
          //            url: rootDir + '/Profile/MasterProfile/GetPracticeLocationsProfileDataAsync?profileId=' + profileId
          //        }).success(function (data, status, headers, config) {
          //            try {
          //                for (key in data) {
          //                    $rootScope.$emit(key, data[key]);
          //                    //call respective controller to load data (PSP)
          //                }

          //                $rootScope.PracticeLocationLoaded = true;
          //                $rootScope.$broadcast("LoadRequireMasterDataPracticeLocation");
          //            } catch (e) {
          //                $rootScope.PracticeLocationLoaded = true;
          //            }

          //        }).error(function (data, status, headers, config) {
          //            $rootScope.PracticeLocationLoaded = true;
          //        });
          //        $scope.dataLoaded = true;
          //    }
          //});

          //$rootScope.$broadcast("PracticeLocation");
      }
    ]);
});