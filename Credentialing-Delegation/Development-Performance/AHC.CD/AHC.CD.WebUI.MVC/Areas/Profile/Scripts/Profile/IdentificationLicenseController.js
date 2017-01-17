//--------------------- Author: KRGLV --------------------------------
//--------------- User Use Strict Section ---------------

define(['Util/MasterProfileApp'], function (app) {
    'use strict';
    app.register.controller('IdentificationLicenseController', ['$scope', '$rootScope', '$http', 'httpq', 'masterDataService', 'locationService', 'messageAlertEngine', '$filter', '$timeout', 'profileUpdates',
      function ($scope, $rootScope, $http, httpq, masterDataService, locationService, messageAlertEngine, $filter, $timeout, profileUpdates) {
          ////////////////////////// JAI MATA DI /////////////////////////

          $(function () {
              _.forEach($rootScope.MasterProfile.IdentificationLicense.StateLicenses, function (n) {
                  n.ProviderTypeID = n.ProviderTypeID ? n.ProviderTypeID : "";
                  n.StateLicenseStatusID = n.StateLicenseStatusID ? n.StateLicenseStatusID : "";
                  n.IssueState = n.IssueState ? n.IssueState : "";
              });

              _.forEach($rootScope.MasterProfile.IdentificationLicense.MedicaidInformations, function (n) {
                  n.isDeclined = (n.State == 'state') ? true : false;
              });

              $scope.StateLicensePendingRequest = profileUpdates.getUpdates('Identification And License', 'State License');
              $scope.FederalDEAPendingRequest = profileUpdates.getUpdates('Identification And License', 'Federal DEA');
              $scope.MedicaidInformationPendingRequest = profileUpdates.getUpdates('Identification And License', 'Medicaid Information');
              $scope.MedicareInformationPendingRequest = profileUpdates.getUpdates('Identification And License', 'Medicare Information');
              $scope.CDSInformationPendingRequest = profileUpdates.getUpdates('Identification And License', 'CDS Information');
              $scope.OtherIdentificationNumbersPendingRequest = profileUpdates.getUpdates('Identification And License', 'Other Identification Number');
          });

          //---------------------- Set File -------------------
          $scope.setFiles = function (file) {
              $(file).parent().parent().find(".jancyFileWrapTexts").find("span").width($(file).parent().parent().width() < 243 ? $(file).parent().parent().width() : 243);

          }

          $scope.ShowRenewDivStateLicense = false;
          $scope.RenewDivSL = function (StateLicenseInformation) {
              if (StateLicenseInformation.ExpiryDate == null)
              { $scope.ShowRenewDivStateLicense = false; }
              else
              {
                  $scope.ShowRenewDivStateLicense = true;
              }
          };
          //DEA
          $scope.ShowRenewDivDEA = false;
          $scope.RenewDivDEA = function (DEAInformation) {
              if (DEAInformation.ExpiryDate == null)
              { $scope.ShowRenewDivDEA = false; }
              else
              {
                  $scope.ShowRenewDivDEA = true;
              }

              //$timeout(function () {
              //    $rootScope.visibilityControl = "";
              //}, 2000);

          };
          //CDS
          $scope.ShowRenewDivCDS = false;
          $scope.RenewDivCDS = function (CDSCInformation) {
              if (CDSCInformation.ExpiryDate == null)
              { $scope.ShowRenewDivCDS = false; }
              else
              {
                  $scope.ShowRenewDivCDS = true;
              }
          };

          $(function () {
              if (!$rootScope.MasterData.States) {
                  httpq.get(rootDir + "/Location/GetStates").then(function (data) {
                      $rootScope.MasterData.States = data;
                      //$scope.States = data;
                  });
              }
              if (!$rootScope.MasterData.ProviderTypes) {
                  httpq.get(rootDir + "/Profile/MasterData/GetAllProviderTypes").then(function (data) {
                      $rootScope.MasterData.ProviderTypes = data;
                      //$scope.ProviderTypes = data;
                  });
              }
              if (!$rootScope.MasterData.StateLicenseStatuses) {
                  httpq.get(rootDir + "/Profile/MasterData/GetAllLicenseStatus").then(function (data) {
                      $rootScope.MasterData.StateLicenseStatuses = data;
                      //$scope.StateLicenseStatuses = data;
                  });
              }
              if (!$rootScope.MasterData.Schedules) {
                  httpq.get(rootDir + "/Profile/MasterData/GetAllDEASchedules").then(function (data) {
                      $rootScope.MasterData.Schedules = data;
                      //$scope.Schedules = data;
                  });
              }
          });

          $scope.hideDiv = function () {
              $('.ProviderTypeSelectAutoList1').hide();
              $('.ProviderTypeSelectAutoList').hide();
              $scope.Errormessage = '';
          }

          $scope.HideErrorMessages = function () {
              $scope.ErrormessageforCDSstate = false;
              $scope.Errormessage = '';
          }
          //====================== State License ===================

          

          $scope.showStates = function (event) {
              $(event.target).parent().find(".ProviderTypeSelectAutoList").first().show();
          };
          // rootScoped on emitted value catches the value for the model and insert to get the old data
          //calling the method using $on(PSP-public subscriber pattern)
          

          $scope.IndexValue1 = '';

          //=================Delete for Federal Dea=====================//

          $scope.initFederalDEAWarning = function (FederalDea) {

              if (angular.isObject(FederalDea)) {
                  $scope.tempReference = FederalDea;
              }

              $('#federalDeaWarningModal').modal();
          }


          $scope.removeFederalDEAInformation = function (FederalDea) {
              var validationStatus = false;
              var url = null;
              var myData = {};
              var $formData = null;
              $scope.isRemoved = true;
              $formData = $('#editFederalDea');
              url = rootDir + "/Profile/IdentificationAndLicense/RemoveFederalDEALicense?profileId=" + profileId;
              ResetFormForValidation($formData);

              validationStatus = $formData.valid();

              if (validationStatus) {
                  //Simple POST request example (passing data) :
                  $.ajax({
                      url: url,
                      type: 'POST',
                      data: new FormData($formData[0]),
                      cache: false,
                      contentType: false,
                      processData: false,
                      success: function (data) {
                          try {

                              if (data.status == "true") {
                                  var obj = $filter('filter')(FederalDea, { FederalDEAInformationID: data.federalDea.FederalDEAInformationID })[0];
                                  $rootScope.MasterProfile.IdentificationLicense.FederalDEAInformations.splice($rootScope.MasterProfile.IdentificationLicense.FederalDEAInformations.indexOf(obj), 1);
                                  if ($scope.dataFetchedFederalDEA == true) {
                                      obj.FederalDEADocumentPath = data.federalDea.DEALicenceCertPath;
                                      obj.HistoryStatus = 'Deleted';
                                      obj.DEAScheduleInfoHistory = angular.copy(obj.DEAScheduleInfoes);
                                      $scope.FederalDEAHistory.push(obj);
                                  }
                                  $scope.isRemoved = false;
                                  $('#federalDeaWarningModal').modal('hide');
                                  $rootScope.operateCancelControl('');
                                  myData = data;
                                  messageAlertEngine.callAlertMessage("addedDEA", "Federal DEA Removed successfully.", "success", true);
                              } else {
                                  $('#federalDeaWarningModal').modal('hide');
                                  messageAlertEngine.callAlertMessage("DEAError", data.status, "danger", true);
                              }
                          } catch (e) {

                          }
                      },
                      error: function (e) {

                      }
                  });
              }

              $scope.FederalDEAPendingRequest = profileUpdates.getUpdates('Identification And License', 'Federal DEA');

          };


          //------------------Remove Medicaid------------------//
          $scope.initMedicaidInformation = function (MedicaidInfo) {

              if (angular.isObject(MedicaidInfo)) {
                  $scope.tempReference = MedicaidInfo;

              }
              $('#medicaidWarningModal').modal();
          }

          $scope.Statechange = function (state) {
              $scope.tempObject.State = state;

          }
          $scope.removeMedicaidInformation = function (MedicaidInfos) {
              var validationStatus = false;
              var url = null;
              var myData = {};
              var $formData = null;
              $scope.isRemoved = true;
              $formData = $('#editMedicaidInfo');
              url = rootDir + "/Profile/IdentificationAndLicense/RemoveMedicaidInformation?profileId=" + profileId;
              ResetFormForValidation($formData);

              validationStatus = $formData.valid();

              if (validationStatus) {
                  //Simple POST request example (passing data) :
                  $.ajax({
                      url: url,
                      type: 'POST',
                      data: new FormData($formData[0]),
                      cache: false,
                      contentType: false,
                      processData: false,
                      success: function (data) {

                          try {
                              if (data.status == "true") {
                                  var obj = $filter('filter')(MedicaidInfos, { MedicaidInformationID: data.MedicaidInfo.MedicaidInformationID })[0];
                                  $rootScope.MasterProfile.IdentificationLicense.MedicaidInformations.splice($rootScope.MasterProfile.IdentificationLicense.MedicaidInformations.indexOf(obj), 1);
                                  if ($scope.dataFetchedMedicaid == true) {
                                      obj.HistoryStatus = 'Deleted';
                                      if (obj.State != 'state')
                                          $scope.MedicaidInformationsHistory.push(obj);
                                  }
                                  $scope.isRemoved = false;
                                  $('#medicaidWarningModal').modal('hide');
                                  $rootScope.operateCancelControl('');
                                  myData = data;
                                  messageAlertEngine.callAlertMessage("addedMedicaid", "Medicaid Information Removed successfully.", "success", true);
                              } else {
                                  $('#medicaidWarningModal').modal('hide');
                                  messageAlertEngine.callAlertMessage("MedicaidError", data.status, "danger", true);
                              }
                          } catch (e) {

                          }
                      },
                      error: function (e) {

                      }
                  });
              }

              $scope.MedicaidInformationPendingRequest = profileUpdates.getUpdates('Identification And License', 'Medicaid Information');

          };


          //--------------------------End----------------------//


          //--------------------Remove Medicare--------------------//

          $scope.initMedicareInformation = function (MedicareInfo) {

              if (angular.isObject(MedicareInfo)) {
                  $scope.tempReference = MedicareInfo;

              }
              $('#medicareWarningModal').modal();
          }


          $scope.removeMedicareInformation = function (MedicareInfos) {
              var validationStatus = false;
              var url = null;
              var myData = {};
              var $formData = null;
              $scope.isRemoved = true;
              $formData = $('#editMedicareInfo');
              url = rootDir + "/Profile/IdentificationAndLicense/RemoveMedicareInformation?profileId=" + profileId;
              ResetFormForValidation($formData);

              validationStatus = $formData.valid();

              if (validationStatus) {
                  //Simple POST request example (passing data) :
                  $.ajax({
                      url: url,
                      type: 'POST',
                      data: new FormData($formData[0]),
                      cache: false,
                      contentType: false,
                      processData: false,
                      success: function (data) {

                          try {
                              if (data.status == "true") {
                                  var obj = $filter('filter')(MedicareInfos, { MedicareInformationID: data.MedicareInfo.MedicareInformationID })[0];
                                  $rootScope.MasterProfile.IdentificationLicense.MedicareInformations.splice($rootScope.MasterProfile.IdentificationLicense.MedicareInformations.indexOf(obj), 1);
                                  if ($scope.dataFetchedMedicare == true) {
                                      obj.HistoryStatus = 'Deleted';
                                      $scope.MedicareInformationsHistory.push(obj);
                                  }
                                  $timeout(function () {
                                      $scope.isRemoved = false;
                                  }, 5000);
                                  $('#medicareWarningModal').modal('hide');
                                  $rootScope.operateCancelControl('');
                                  myData = data;
                                  messageAlertEngine.callAlertMessage("addedMedicare", "Medicare Information Removed successfully.", "success", true);
                              } else {
                                  $('#medicareWarningModal').modal('hide');
                                  messageAlertEngine.callAlertMessage("MedicareError", data.status, "danger", true);
                              }
                          } catch (e) {

                          }
                      },
                      error: function (e) {

                      }
                  });
              }

              $scope.MedicareInformationPendingRequest = profileUpdates.getUpdates('Identification And License', 'Medicare Information');

          };

          //--------------------End--------------------------------//


          //------------SAVE--------------
          $scope.saveStateLicense = function (stateLicense, index) {

              //loadingOn();

              var validationStatus;
              var url;
              var myData = {};
              var $formDataStateLicense;
              var tempProviderType;
              var providerTypeobj;
              var tempLicenseStatus;
              var LicenseStatusobj;
              $scope.SLError = '';
              tempProviderType = stateLicense.ProviderTypeID;
              tempLicenseStatus = stateLicense.StateLicenseStatusID;
              $scope.IndexValue1 = index;

              for (var sls in $rootScope.MasterData.StateLicenseStatuses) {
                  try {
                      if ($rootScope.MasterData.StateLicenseStatuses[sls].StateLicenseStatusID == tempLicenseStatus) {
                          LicenseStatusobj = angular.copy($rootScope.MasterData.StateLicenseStatuses[sls]);
                          break;
                      }
                  }
                  catch (e)
                  { };

              }

              if ($scope.visibilityControl == 'addStateLicenseInformation') {
                  //Add Details - Denote the URL
                  try {
                      $formDataStateLicense = $('#newStateLicenseDiv').find('form');
                      url = rootDir + "/Profile/IdentificationAndLicense/AddStateLicenseAsync?profileId=" + profileId;
                  }
                  catch (e)
                  { };
              }
              else if ($scope.visibilityControl == (index + '_editStateLicenseInformation')) {
                  //Update Details - Denote the URL
                  try {
                      $formDataStateLicense = $('#stateLicenseEditDiv' + index).find('form');
                      url = rootDir + "/Profile/IdentificationAndLicense/UpdateStateLicenseAsync?profileId=" + profileId;
                  }
                  catch (e)
                  { };
              }
              else if ($scope.visibilityControl == (index + '_renewStateLicenseInformation')) {
                  //Update Details - Denote the URL
                  try {
                      $formDataStateLicense = $('#stateLicenseRenewDiv' + index).find('form');
                      url = rootDir + "/Profile/IdentificationAndLicense/RenewStateLicenseAsync?profileId=" + profileId;
                  }
                  catch (e)
                  { };
              }
              if ($('#providertype').val() == '') {
                  $($formDataStateLicense).find($("[name='ProviderTypeID']")).val('');
              } else {
                  for (var type in $rootScope.MasterData.ProviderTypes) {
                      try {
                          if ($rootScope.MasterData.ProviderTypes[type].ProviderTypeID == tempProviderType) {
                              providerTypeobj = angular.copy($rootScope.MasterData.ProviderTypes[type]);
                              break;
                          }
                      }
                      catch (e) { };
                  }
              }


              ResetFormForValidation($formDataStateLicense);
              validationStatus = $formDataStateLicense.valid();
              if (validationStatus) {


                  $.ajax({
                      url: url,
                      type: 'POST',
                      data: new FormData($formDataStateLicense[0]),
                      async: false,
                      cache: false,
                      contentType: false,
                      processData: false,
                      success: function (data) {

                          try {
                              if (data.status == "true") {
                                  data.stateLicense.ProviderType = providerTypeobj;
                                  data.stateLicense.StateLicenseStatus = LicenseStatusobj;
                                  data.stateLicense.IssueDate = ConvertDateFormat(data.stateLicense.IssueDate);
                                  data.stateLicense.ExpiryDate = ConvertDateFormat(data.stateLicense.ExpiryDate);
                                  data.stateLicense.CurrentIssueDate = ConvertDateFormat(data.stateLicense.CurrentIssueDate);
                                  myData = data;

                                  if ($scope.visibilityControl == 'addStateLicenseInformation') {
                                      $rootScope.MasterProfile.IdentificationLicense.StateLicenses.push(data.stateLicense);
                                      for (var i = 0; i < $rootScope.MasterProfile.IdentificationLicense.StateLicenses.length ; i++) {
                                          if (!$rootScope.MasterProfile.IdentificationLicense.StateLicenses[i].ProviderTypeID) { $rootScope.MasterProfile.IdentificationLicense.StateLicenses[i].ProviderTypeID = ""; }
                                          if (!$rootScope.MasterProfile.IdentificationLicense.StateLicenses[i].StateLicenseStatusID) { $rootScope.MasterProfile.IdentificationLicense.StateLicenses[i].StateLicenseStatusID = ""; }
                                          if (!$rootScope.MasterProfile.IdentificationLicense.StateLicenses[i].IssueState) { $rootScope.MasterProfile.IdentificationLicense.StateLicenses[i].IssueState = ""; }
                                      }
                                      $rootScope.operateCancelControl('');
                                      messageAlertEngine.callAlertMessage("addedStateLicense", "State License Information Saved Successfully!!!!", "success", true);
                                  }
                                  else if ($scope.visibilityControl == (index + '_editStateLicenseInformation')) {
                                      $scope.StateLicensePendingRequest = true;
                                      $rootScope.MasterProfile.IdentificationLicense.StateLicenses[index] = data.stateLicense;
                                      for (var i = 0; i < $rootScope.MasterProfile.IdentificationLicense.StateLicenses.length ; i++) {
                                          if (!$rootScope.MasterProfile.IdentificationLicense.StateLicenses[i].ProviderTypeID) { $rootScope.MasterProfile.IdentificationLicense.StateLicenses[i].ProviderTypeID = ""; }
                                          if (!$rootScope.MasterProfile.IdentificationLicense.StateLicenses[i].StateLicenseStatusID) { $rootScope.MasterProfile.IdentificationLicense.StateLicenses[i].StateLicenseStatusID = ""; }
                                          if (!$rootScope.MasterProfile.IdentificationLicense.StateLicenses[i].IssueState) { $rootScope.MasterProfile.IdentificationLicense.StateLicenses[i].IssueState = ""; }
                                      }
                                      $rootScope.operateViewAndAddControl(index + '_viewStateLicenseInformation');
                                      messageAlertEngine.callAlertMessage("updatedStateLicense" + index, "State License Information Updated Successfully!!!!", "success", true);
                                  }
                                  else {
                                      $scope.StateLicensePendingRequest = true;
                                      $rootScope.MasterProfile.IdentificationLicense.StateLicenses[index] = data.stateLicense;
                                      for (var i = 0; i < $rootScope.MasterProfile.IdentificationLicense.StateLicenses.length ; i++) {
                                          if (!$rootScope.MasterProfile.IdentificationLicense.StateLicenses[i].ProviderTypeID) { $rootScope.MasterProfile.IdentificationLicense.StateLicenses[i].ProviderTypeID = ""; }
                                          if (!$rootScope.MasterProfile.IdentificationLicense.StateLicenses[i].StateLicenseStatusID) { $rootScope.MasterProfile.IdentificationLicense.StateLicenses[i].StateLicenseStatusID = ""; }
                                          if (!$rootScope.MasterProfile.IdentificationLicense.StateLicenses[i].IssueState) { $rootScope.MasterProfile.IdentificationLicense.StateLicenses[i].IssueState = ""; }
                                      }
                                      $rootScope.operateViewAndAddControl(index + '_viewStateLicenseInformation');
                                      messageAlertEngine.callAlertMessage("renewedStateLicense" + index, "State License Information Renewed Successfully!!!!", "success", true);

                                  }

                                  FormReset($formDataStateLicense);
                                  $scope.resetDates();
                              }

                              else {
                                  $scope.SLError = data.status.split(",");
                                  messageAlertEngine.callAlertMessage('StateLicenseError', "", "danger", true);
                              }
                          }
                          catch (e) { };


                      },
                      error: function (e) {
                          try {
                              $scope.SLError = "Sorry for Inconvenience !!!! Please Try Again Later...";
                              messageAlertEngine.callAlertMessage('StateLicenseError', "", "danger", true);
                          }
                          catch (e) { };


                      }

                  });
              }
              $scope.StateLicensePendingRequest = profileUpdates.getUpdates('Identification And License', 'State License');
              //loadingOff();
          };

          //To initiate Removal Confirmation Modal
          $scope.initStateLicenseWarning = function (StateLicenseInformation) {
              if (angular.isObject(StateLicenseInformation)) {
                  $scope.tempStateLicense = StateLicenseInformation;
              }
              $('#stateLicenseWarningModal').modal();
          };

          $scope.isRemoved = false;

          $scope.removeStateLicense = function (StateLicenses) {
              var validationStatus = false;
              var url = null;
              var myData = {};
              var $formData = null;
              $scope.isRemoved = true;
              $formData = $('#removeStateLicense');
              url = rootDir + "/Profile/IdentificationAndLicense/RemoveStateLicenseAsync?profileId=" + profileId;
              ResetFormForValidation($formData);
              validationStatus = $formData.valid();

              if (validationStatus) {
                  //Simple POST request example (passing data) :
                  $.ajax({
                      url: url,
                      type: 'POST',
                      data: new FormData($formData[0]),
                      cache: false,
                      contentType: false,
                      processData: false,
                      success: function (data) {
                          try {
                              if (data.status == "true") {
                                  var obj = $filter('filter')(StateLicenses, { StateLicenseInformationID: data.stateLicense.StateLicenseInformationID })[0];
                                  StateLicenses.splice(StateLicenses.indexOf(obj), 1);
                                  if ($scope.dataFetched == true) {
                                      obj.HistoryStatus = 'Deleted';
                                      $scope.StateLicensesHistory.push(obj);
                                  }
                                  $scope.isRemoved = false;
                                  $('#stateLicenseWarningModal').modal('hide');
                                  $rootScope.operateCancelControl('');
                                  myData = data;
                                  messageAlertEngine.callAlertMessage("addedStateLicense", "State License Removed successfully.", "success", true);
                              } else {
                                  $('#stateLicenseWarningModal').modal('hide');
                                  messageAlertEngine.callAlertMessage("removeStateLicense", data.status, "danger", true);
                                  $scope.errorStateLicense = "Sorry for Inconvenience !!!! Please Try Again Later...";
                              }
                          } catch (e) {

                          }
                      },
                      error: function (e) {

                      }
                  });
              }

              $scope.StateLicensePendingRequest = profileUpdates.getUpdates('Identification And License', 'State License');
          };

          //....................State Licenses History............................//
          $scope.StateLicensesHistory = [];
          $scope.dataFetched = false;

          $scope.showStateLicenseHistory = function (loadingId) {
              if ($scope.StateLicensesHistory.length == 0) {
                  $("#" + loadingId).css('display', 'block');
                  var url = rootDir + "/Profile/ProfileHistory/GetAllStateLicensesHistory?profileId=" + profileId;
                  $http.get(url).success(function (data) {
                      try {
                          $scope.StateLicensesHistory = data;
                          $scope.dataFetched = true;
                          for (var i = 0; i < $scope.StateLicensesHistory.length; i++) {
                              if ($scope.StateLicensesHistory[i].HistoryStatus == '' || !$scope.StateLicensesHistory[i].HistoryStatus) {
                                  $scope.StateLicensesHistory[i].HistoryStatus = 'Renewed';
                              }
                          }
                          $scope.showStateLicenseHistoryTable = true;
                          $("#" + loadingId).css('display', 'none');
                      } catch (e) {

                      }
                  });
              }

              else {
                  $scope.showStateLicenseHistoryTable = true;
              }
          }

          $scope.cancelStateLicenseHistory = function () {
              $scope.showStateLicenseHistoryTable = false;
          }

          //****************************************Federal DEA Info********************************************
          //=============== Federal DEA License Conditions ==================

          $scope.resetScheduleOption = function () {
              for (var schedule in $rootScope.tempObject.DEAScheduleInfoes) {
                  schedule.YesNoOption = 2;
              }
          };

          $scope.$watch('tempObject.StateOfReg', function (newV, oldV) {
              if (newV == oldV) return;
              if (newV == "") {
                  $scope.Deastateerrormsg = true;
              }
              else {
                  $scope.Deastateerrormsg = false;
              }
          });
          $scope.reseterrormessages = function () {
              $scope.Deastateerrormsg = false;
          }
          $scope.IndexValue2 = '';
          //====================== Federal DEA License Save ===================
          $scope.Deastateerrormsg = false;

          $scope.saveFederalDEALicense = function (FederalDEALicense, index) {
              //loadingOn();
              //$scope.Deastateerrormsg = false;
              $scope.IndexValue2 = index;
              var myData = {};
              var validationStatus;
              var url;
              var $formDataDEA;
              if ($scope.visibilityControl == 'addDEAInformation') {
                  try {
                      $formDataDEA = $('#newShowFederalDEALicenseDiv').find('form');
                      url = rootDir + "/Profile/IdentificationAndLicense/AddFederalDEALicenseAsync?profileId=" + profileId;
                  }
                  catch (e) { };
                  //Add Details - Denote the URL

              }
              else if ($scope.visibilityControl == (index + '_editDEAInformation')) {
                  try {
                      $formDataDEA = $('#FederalDEALicenseEditDiv' + index).find('form');
                      url = rootDir + "/Profile/IdentificationAndLicense/UpdateFederalDEALicenseAsync?profileId=" + profileId;
                  }
                  catch (e) { };
                  //Update Details - Denote the URL

              }
              else if ($scope.visibilityControl == (index + '_renewDEAInformation')) {
                  try {
                      $formDataDEA = $('#DEAInformationRenewDiv' + index).find('form');
                      url = rootDir + "/Profile/IdentificationAndLicense/RenewFederalDEALicenseAsync?profileId=" + profileId;
                  }
                  catch (e) { };
                  //Update Details - Denote the URL

              }


              ResetFormForValidation($formDataDEA);
              validationStatus = $formDataDEA.valid();
              if (typeof $scope.tempObject.StateOfReg == "undefined" || $scope.tempObject.StateOfReg == "") {
                  $scope.Deastateerrormsg = true;
              }
              $scope.DEAError = '';

              if (validationStatus && !$scope.Deastateerrormsg) {

                  $.ajax({
                      url: url,
                      type: 'POST',
                      data: new FormData($formDataDEA[0]),
                      async: false,
                      cache: false,
                      contentType: false,
                      processData: false,
                      success: function (data) {
                          try {
                              if (data.status == "true") {
                                  data.federalDea.IssueDate = ConvertDateFormat(data.federalDea.IssueDate);
                                  data.federalDea.ExpiryDate = ConvertDateFormat(data.federalDea.ExpiryDate);
                                  myData = data;
                                  if ($scope.visibilityControl == 'addDEAInformation') {
                                      $rootScope.MasterProfile.IdentificationLicense.FederalDEAInformations.push(data.federalDea);
                                      $rootScope.operateCancelControl('');
                                      messageAlertEngine.callAlertMessage("addedDEA", "DEA information saved successfully. !!!!", "success", true);
                                  }
                                  else if ($scope.visibilityControl == (index + '_editDEAInformation')) {
                                      $scope.FederalDEAPendingRequest = true;
                                      $rootScope.MasterProfile.IdentificationLicense.FederalDEAInformations[index] = data.federalDea;
                                      $rootScope.operateViewAndAddControl(index + '_viewDEAInformation');
                                      messageAlertEngine.callAlertMessage("updatedDEA" + index, "DEA Information Updated Successfully!!!!", "success", true);
                                  }
                                  else {
                                      $scope.FederalDEAPendingRequest = true;
                                      $rootScope.MasterProfile.IdentificationLicense.FederalDEAInformations[index] = data.federalDea;
                                      $rootScope.operateViewAndAddControl(index + '_viewDEAInformation');
                                      messageAlertEngine.callAlertMessage("renewedDEA" + index, "DEA Information Renewed Successfully!!!!", "success", true);
                                  }
                                  FormReset($formDataDEA);
                                  $scope.resetDates();
                              } else {
                                  $scope.DEAError = data.status.split(",");
                                  messageAlertEngine.callAlertMessage('DEAError', "", "danger", true);

                              }
                          }
                          catch (e) { };


                      },
                      error: function (e) {
                          try {
                              $scope.DEAError = "Sorry for Inconvenience !!!! Please Try Again Later...";
                              messageAlertEngine.callAlertMessage('DEAError', "", "danger", true);
                          }
                          catch (e) { };

                      }
                  });
              }

              $scope.FederalDEAPendingRequest = profileUpdates.getUpdates('Identification And License', 'Federal DEA');

              //loadingOff();
          };

          //....................Federal DEA History............................//
          $scope.FederalDEAHistory = [];
          $scope.dataFetchedFederalDEA = false;

          $scope.showFederalDEALicenseHistory = function (loadingId) {
              if ($scope.FederalDEAHistory.length == 0) {
                  $("#" + loadingId).css('display', 'block');
                  var url = rootDir + "/Profile/ProfileHistory/GetAllFederalDEALicensesHistory?profileId=" + profileId;
                  $http.get(url).success(function (data) {
                      try {
                          $scope.FederalDEAHistory = data;

                          $scope.dataFetchedFederalDEA = true;
                          for (var i = 0; i < $scope.FederalDEAHistory.length; i++) {
                              if ($scope.FederalDEAHistory[i].HistoryStatus == '' || !$scope.FederalDEAHistory[i].HistoryStatus) {
                                  $scope.FederalDEAHistory[i].HistoryStatus = 'Renewed';

                              }
                          }
                          $scope.showFederalDEALicenseHistoryTable = true;
                          $("#" + loadingId).css('display', 'none');
                      } catch (e) {

                      }


                  });
              }
              else {
                  $scope.showFederalDEALicenseHistoryTable = true;
              }
          }

          $scope.cancelFederalDEALicenseHistory = function () {
              $scope.showFederalDEALicenseHistoryTable = false;
          }

          //****************************************CDSC Information********************************************

          //=============== CDSC Information Conditions ==================

          //   $scope.submitButtonText = "Add";

          $scope.CDSCError = '';
          $scope.ErrormessageforCDSstate = false;
          $scope.IndexValue5 = '';
          //====================== CDSC Information Save===================

          $scope.$watch('tempObject.State', function (newVal, oldVal) {
              if (newVal == oldVal) { return; }
              if (newVal == "") {
                  $scope.ErrormessageforCDSstate = true;
              } else { $scope.ErrormessageforCDSstate = false; }
          })

          $scope.saveCDSCInformation = function (cDSCInformation, index) {
              //loadingOn();
              $scope.ErrormessageforCDSstate = false;
              if (typeof $scope.tempObject.State == "undefined" || $scope.tempObject.State == "") {
                  $scope.ErrormessageforCDSstate = true;
              }
              $scope.IndexValue5 = index;

              var validationStatus;
              var url;
              var myData = {};
              var $formDataCDSC;
              if ($scope.visibilityControl == 'addCDSCInformation') {
                  try {
                      $formDataCDSC = $('#newShowCDSCInformationDiv').find('form');
                      url = rootDir + "/Profile/IdentificationAndLicense/AddCDSCLicenseAsync?profileId=" + profileId;
                  }
                  catch (e) { };
                  //Add Details - Denote the URL

              }
              else if ($scope.visibilityControl == (index + '_editCDSCInformation')) {
                  try {
                      $formDataCDSC = $('#cDSCInformationEditDiv' + index).find('form');
                      url = rootDir + "/Profile/IdentificationAndLicense/UpdateCDSCLicenseAsync?profileId=" + profileId;
                  }
                  catch (e) { };
                  //Update Details - Denote the URL

              }
              else if ($scope.visibilityControl == (index + '_renewCDSCInformation')) {
                  try {
                      $formDataCDSC = $('#CDSCInformationRenewDiv' + index).find('form');
                      url = rootDir + "/Profile/IdentificationAndLicense/RenewCDSCLicenseAsync?profileId=" + profileId;
                  }
                  catch (e) { };
                  //Update Details - Denote the URL

              }
              ResetFormForValidation($formDataCDSC);
              validationStatus = $formDataCDSC.valid();

              if (validationStatus && !$scope.ErrormessageforCDSstate) {

                  $.ajax({
                      url: url,
                      type: 'POST',
                      data: new FormData($formDataCDSC[0]),
                      async: false,
                      cache: false,
                      contentType: false,
                      processData: false,
                      success: function (data) {
                          try {
                              if (data.status == "true") {
                                  data.CDSCInformation.IssueDate = ConvertDateFormat(data.CDSCInformation.IssueDate);
                                  data.CDSCInformation.ExpiryDate = ConvertDateFormat(data.CDSCInformation.ExpiryDate);

                                  if ($scope.visibilityControl == 'addCDSCInformation') {
                                      $rootScope.MasterProfile.IdentificationLicense.CDSCInformations.push(data.CDSCInformation);
                                      $rootScope.operateCancelControl('');
                                      messageAlertEngine.callAlertMessage("addedCDS", "CDS information saved successfully. !!!!", "success", true);
                                  }
                                  else if ($scope.visibilityControl == (index + '_editCDSCInformation')) {
                                      $scope.CDSInformationPendingRequest = true;
                                      $rootScope.MasterProfile.IdentificationLicense.CDSCInformations[index] = data.CDSCInformation;
                                      $rootScope.operateViewAndAddControl(index + '_viewCDSCInformation');
                                      messageAlertEngine.callAlertMessage("updatedCDS" + index, "CDS Information Updated Successfully!!!!", "success", true);
                                  }
                                  else {
                                      $scope.CDSInformationPendingRequest = true;
                                      $rootScope.MasterProfile.IdentificationLicense.CDSCInformations[index] = data.CDSCInformation;
                                      $rootScope.operateViewAndAddControl(index + '_viewCDSCInformation');
                                      messageAlertEngine.callAlertMessage("renewedCDS" + index, "CDS Information Renewed Successfully!!!!", "success", true);
                                  }
                                  myData = data;
                                  FormReset($formDataCDSC);
                                  $scope.resetDates();
                              } else {
                                  //$scope.Errormessage = 'Please enter Issue State *.';
                                  if (data.status.indexOf(",") > -1) {
                                      $scope.CDSCError = data.status.split(",");
                                      $scope.Errormessage = $scope.CDSCError;
                                  }
                                  else {
                                      $scope.CDSCError = data.status;
                                      $scope.Errormessage = $scope.CDSCError;
                                  }
                                  //messageAlertEngine.callAlertMessage('CDSCError', "", "danger", true);
                              }
                          }
                          catch (e) { };
                      },
                      error: function (e) {
                          try {
                              $scope.CDSCError = "Sorry for Inconvenience !!!! Please Try Again Later...";
                              //messageAlertEngine.callAlertMessage('CDSCError', "", "danger", true);
                          }
                          catch (e) { };

                      }
                  });
              }

              $scope.CDSInformationPendingRequest = profileUpdates.getUpdates('Identification And License', 'CDS Information');

              //loadingOff();
          };

          //To initiate Removal Confirmation Modal
          $scope.initCDSCWarning = function (CDSCInformation) {
              if (angular.isObject(CDSCInformation)) {
                  $scope.tempCDSC = CDSCInformation;
              }
              $('#cDSCWarningModal').modal();
          };

          $scope.removeCDSC = function (CDSCInformations) {
              var validationStatus = false;
              var url = null;
              var myData = {};
              var $formData = null;
              $scope.isRemoved = true;
              $formData = $('#removeCDSC');
              url = rootDir + "/Profile/IdentificationAndLicense/RemoveCDSCLicenseAsync?profileId=" + profileId;
              ResetFormForValidation($formData);
              validationStatus = $formData.valid();

              if (validationStatus) {
                  //Simple POST request example (passing data) :
                  $.ajax({
                      url: url,
                      type: 'POST',
                      data: new FormData($formData[0]),
                      cache: false,
                      contentType: false,
                      processData: false,
                      success: function (data) {
                          try {
                              if (data.status == "true") {
                                  var obj = $filter('filter')(CDSCInformations, { CDSCInformationID: data.cDSCInformation.CDSCInformationID })[0];
                                  CDSCInformations.splice(CDSCInformations.indexOf(obj), 1);
                                  if ($scope.dataFetchedCDSC) {
                                      obj.HistoryStatus = 'Deleted';
                                      $scope.CDSCInformationsHistory.push(obj);
                                  }
                                  $scope.isRemoved = false;
                                  $('#cDSCWarningModal').modal('hide');
                                  $rootScope.operateCancelControl('');
                                  myData = data;
                                  messageAlertEngine.callAlertMessage("addedCDS", "CDS Information Removed successfully.", "success", true);
                              } else {
                                  $('#cDSCWarningModal').modal('hide');
                                  messageAlertEngine.callAlertMessage("removeCDS", data.status, "danger", true);
                                  $scope.errorCDS = "Sorry for Inconvenience !!!! Please Try Again Later...";
                              }
                          } catch (e) {

                          }
                      },
                      error: function (e) {

                      }
                  });
              }

              $scope.CDSInformationPendingRequest = profileUpdates.getUpdates('Identification And License', 'CDS Information');

          };

          //....................CDSC History............................//
          $scope.CDSCInformationsHistory = [];
          $scope.dataFetchedCDSC = false;

          $scope.showCDSCHistory = function (loadingId) {
              if ($scope.CDSCInformationsHistory.length == 0) {
                  $("#" + loadingId).css('display', 'block');
                  var url = rootDir + "/Profile/ProfileHistory/GetAllCDSCInformationHistory?profileId=" + profileId;
                  $http.get(url).success(function (data) {
                      try {
                          $scope.CDSCInformationsHistory = data;
                          $scope.dataFetchedCDSC = true;
                          for (var i = 0; i < $scope.CDSCInformationsHistory.length; i++) {
                              if ($scope.CDSCInformationsHistory[i].HistoryStatus == '' || !$scope.CDSCInformationsHistory[i].HistoryStatus) {
                                  $scope.CDSCInformationsHistory[i].HistoryStatus = 'Renewed';
                              }
                          }
                          $scope.showCDSCInformationHistoryTable = true;
                          $("#" + loadingId).css('display', 'none');
                      } catch (e) {

                      }

                  });
              }
              else {
                  $scope.showCDSCInformationHistoryTable = true;
              }
          }

          $scope.cancelCDSCHistory = function () {
              $scope.showCDSCInformationHistoryTable = false;
          }

          //=============== Medicare Information Conditions ==================

          $scope.IndexValue3 = '';

          //====================== Medicare Information ===================

          $scope.saveMedicareInformation = function (MedicareInformation, index) {
              //loadingOn();
              $scope.IndexValue3 = index;
              var validationStatus;
              var url;
              var myData = {};
              var $formDataMedicare;
              if ($scope.visibilityControl == 'addMedicareInformation') {
                  try {
                      $formDataMedicare = $('#newShowMedicareInformationDiv').find('form');
                      url = rootDir + "/Profile/IdentificationAndLicense/AddMedicareInformationAsync?profileId=" + profileId;
                  }
                  catch (e) { };
                  //Add Details - Denote the URL

              }
              else if ($scope.visibilityControl == (index + '_editMedicareInformation')) {
                  try {
                      $formDataMedicare = $('#MedicareInformationEditDiv' + index).find('form');
                      url = rootDir + "/Profile/IdentificationAndLicense/UpdateMedicareInformationAsync?profileId=" + profileId;
                  }
                  catch (e) { };
                  //Update Details - Denote the URL

              }

              ResetFormForValidation($formDataMedicare);
              validationStatus = $formDataMedicare.valid();
              $scope.MedicareError = '';

              if (validationStatus) {
                  $.ajax({
                      url: url,
                      type: 'POST',
                      data: new FormData($formDataMedicare[0]),
                      async: false,
                      cache: false,
                      contentType: false,
                      processData: false,
                      success: function (data) {
                          try {
                              if (data.status == "true") {
                                  data.MedicareInformation.IssueDate = ConvertDateFormat(data.MedicareInformation.IssueDate);
                                  // data.MedicareInformation.ExpiryDate = ConvertDateFormat(data.MedicareInformation.ExpiryDate);
                                  myData = data;
                                  if ($scope.visibilityControl == 'addMedicareInformation') {
                                      $rootScope.MasterProfile.IdentificationLicense.MedicareInformations.push(data.MedicareInformation);
                                      $rootScope.operateCancelControl('');
                                      messageAlertEngine.callAlertMessage("addedMedicare", "Medicare information saved successfully. !!!!", "success", true);
                                  }
                                  else {
                                      $scope.MedicareInformationPendingRequest = true;
                                      $rootScope.MasterProfile.IdentificationLicense.MedicareInformations[index] = data.MedicareInformation;
                                      $rootScope.operateViewAndAddControl(index + '_viewMedicareInformation');
                                      messageAlertEngine.callAlertMessage("updatedMedicare" + index, "Medicare Information Updated Successfully!!!!", "success", true);
                                  }

                                  FormReset($formDataMedicare);
                                  $scope.resetDates();
                              } else {
                                  $scope.MedicareError = data.status.split(",");
                                  messageAlertEngine.callAlertMessage('MedicareError', "", "danger", true);

                              }
                          }
                          catch (e) { };


                      },
                      error: function (e) {
                          try {
                              $scope.MedicareError = "Sorry for Inconvenience !!!! Please Try Again Later...";
                              messageAlertEngine.callAlertMessage('MedicareError', "", "danger", true);
                          }
                          catch (e) { };

                      }
                  });
              }

              $scope.MedicareInformationPendingRequest = profileUpdates.getUpdates('Identification And License', 'Medicare Information');

              //loadingOff();
          };

          //....................Medicare History............................//
          $scope.MedicareInformationsHistory = [];
          $scope.dataFetchedMedicare = false;

          $scope.showMedicareHistory = function (loadingId) {
              if ($scope.MedicareInformationsHistory.length == 0) {
                  $("#" + loadingId).css('display', 'block');
                  var url = rootDir + "/Profile/ProfileHistory/GetAllMedicareInformationHistory?profileId=" + profileId;
                  $http.get(url).success(function (data) {
                      try {
                          $scope.MedicareInformationsHistory = data;
                          $scope.dataFetchedMedicare = true;
                          for (var i = 0; i < $scope.MedicareInformationsHistory.length; i++) {
                              if ($scope.MedicareInformationsHistory[i].HistoryStatus == '' || !$scope.MedicareInformationsHistory[i].HistoryStatus) {
                                  $scope.MedicareInformationsHistory[i].HistoryStatus = 'Renewed';
                              }
                          }
                          $scope.showMedicareHistoryTable = true;
                          $("#" + loadingId).css('display', 'none');
                      } catch (e) {

                      }

                  });
              }
              else {
                  $scope.showMedicareHistoryTable = true;
              }
          }

          $scope.cancelMedicareHistory = function () {
              $scope.showMedicareHistoryTable = false;
          }

          //****************************************Medicaid Information********************************************

          //=============== Medicaid Information Conditions ==================

          //  $scope.submitButtonText = "Add";
          $scope.IndexValue4 = '';

          //====================== Medicaid Information ===================


          $scope.isDeclined = function () {

              if ($scope.tempObject.isDeclined == true) {

                  $scope.tempObject.LicenseNumber = $rootScope.lastName;
                  $scope.tempObject.IssueDate = new Date();
                  $scope.tempObject.State = 'state';
              }
              else if ($scope.tempObject.isDeclined == false) {

                  $scope.tempObject.LicenseNumber = '';
                  $scope.tempObject.IssueDate = '';
                  $scope.tempObject.State = '';
                  $scope.tempObject.CertificatePath = '';
              }

              //$scope.States = ["Florida", "Texas", "California", "Alaska"];

          }

          $scope.saveMedicaidInformation = function (MedicaidInformation, index) {
              //if ($scope.tempObject.isDeclined) {
              //    $scope.tempObject.LicenseNumber = $scope.OtherIdentificationNumbers.NPINumber;
              //}
              //loadingOn();
              $scope.IndexValue4 = index;
              var validationStatus;
              var url;
              var myData = {};
              var $formDataMedicaid;
              if ($scope.visibilityControl == 'addMedicaidInformation') {
                  try {
                      $formDataMedicaid = $('#newShowMedicaidInformationDiv').find('form');
                      url = rootDir + "/Profile/IdentificationAndLicense/AddMedicaidInformationAsync?profileId=" + profileId;
                  }
                  catch (e) { };
                  //Add Details - Denote the URL

              }
              else if ($scope.visibilityControl == (index + '_editMedicaidInformation')) {
                  try {
                      $formDataMedicaid = $('#MedicaidInformationEditDiv' + index).find('form');
                      url = rootDir + "/Profile/IdentificationAndLicense/UpdateMedicaidInformationAsync?profileId=" + profileId;
                  }
                  catch (e) { };
                  //Update Details - Denote the URL

              }

              ResetFormForValidation($formDataMedicaid);
              validationStatus = $formDataMedicaid.valid();
              $scope.MedicaidError = '';

              if (validationStatus) {
                  $.ajax({
                      url: url,
                      type: 'POST',
                      data: new FormData($formDataMedicaid[0]),
                      async: false,
                      cache: false,
                      contentType: false,
                      processData: false,
                      success: function (data) {
                          try {
                              if (data.status == "true") {
                                  data.MedicaidInformation.IssueDate = ConvertDateFormat(data.MedicaidInformation.IssueDate);
                                  // data.MedicaidInformation.ExpiryDate = ConvertDateFormat(data.MedicaidInformation.ExpiryDate);
                                  myData = data;
                                  if ($scope.visibilityControl == 'addMedicaidInformation') {
                                      $rootScope.MasterProfile.IdentificationLicense.MedicaidInformations.push(data.MedicaidInformation);
                                      if ($scope.tempObject.isDeclined) {
                                          $rootScope.MasterProfile.IdentificationLicense.MedicaidInformations[$rootScope.MasterProfile.IdentificationLicense.MedicaidInformations.length - 1].isDeclined = true;
                                          $rootScope.visibilityControl = "";
                                      } else {
                                          $rootScope.MasterProfile.IdentificationLicense.MedicaidInformations[$rootScope.MasterProfile.IdentificationLicense.MedicaidInformations.length - 1].isDeclined = false;
                                      }

                                      $rootScope.operateCancelControl('');
                                      messageAlertEngine.callAlertMessage("addedMedicaid", "Medicaid information saved successfully. !!!!", "success", true);
                                  }
                                  else {
                                      $scope.MedicaidInformationPendingRequest = true;
                                      $rootScope.MasterProfile.IdentificationLicense.MedicaidInformations[index] = data.MedicaidInformation;
                                      if ($scope.tempObject.isDeclined) {
                                          $rootScope.MasterProfile.IdentificationLicense.MedicaidInformations[index].isDeclined = true;
                                          $rootScope.visibilityControl = "";
                                      } else {
                                          $rootScope.MasterProfile.IdentificationLicense.MedicaidInformations[index].isDeclined = false;
                                          $rootScope.operateViewAndAddControl(index + '_viewMedicaidInformation');
                                      }

                                      messageAlertEngine.callAlertMessage("updatedMedicaid" + index, "Medicaid Information Updated Successfully. !!!!", "success", true);
                                  }

                                  FormReset($formDataMedicaid);
                                  $scope.resetDates();
                              } else {
                                  $scope.MedicaidError = data.status.split(",");
                                  messageAlertEngine.callAlertMessage('MedicaidError', "", "danger", true);
                              }

                          }
                          catch (e) { };

                      },
                      error: function (e) {
                          try {
                              $scope.MedicaidError = "Sorry for Inconvenience !!!! Please Try Again Later...";
                              messageAlertEngine.callAlertMessage('MedicaidError', "", "danger", true);
                          }
                          catch (e) { };

                      }
                  });
              }

              $scope.MedicaidInformationPendingRequest = profileUpdates.getUpdates('Identification And License', 'Medicaid Information');

              //loadingOff();
          };

          //....................Medicaid History............................//
          $scope.MedicaidInformationsHistory = [];
          $scope.dataFetchedMedicaid = false;

          $scope.showMedicaidHistory = function (loadingId) {
              if ($scope.MedicaidInformationsHistory.length == 0) {
                  $("#" + loadingId).css('display', 'block');
                  var url = rootDir + "/Profile/ProfileHistory/GetAllMedicaidInformationHistory?profileId=" + profileId;
                  $http.get(url).success(function (data) {
                      $scope.MedicaidInformationsHistory = data;
                      $scope.dataFetchedMedicaid = true;
                      for (var i = 0; i < $scope.MedicaidInformationsHistory.length ; i++) {
                          if ($scope.MedicaidInformationsHistory[i].HistoryStatus == '' || !$scope.MedicaidInformationsHistory[i].HistoryStatus) {
                              $scope.MedicaidInformationsHistory[i].HistoryStatus = 'Renewed';
                          }
                          if ($scope.MedicaidInformationsHistory[i].State == 'state') {
                              $scope.MedicaidInformationsHistory.splice(i, 1);
                              i--;
                          }
                      }
                      $scope.showMedicaidHistoryTable = true;
                      $("#" + loadingId).css('display', 'none');

                  });
              }
              else {
                  $scope.showMedicaidHistoryTable = true;
              }
          }

          $scope.cancelMedicaidHistory = function () {
              $scope.showMedicaidHistoryTable = false;
          }

          //****************************************Other Identification Numbers********************************************

          

          $scope.clearCAQHCredentials = function (value) {
              try {
                  if (value == "") {
                      $scope.tempObject.CAQHUserName = "";
                      $scope.tempObject.CAQHPassword = "";

                  }
              }
              catch (e) { };

          };

          $scope.showOtherId = false;
          $scope.OtherIdError = '';
          //====================== Other Identification Numbers ===================

          $scope.saveOtherIdentificationNumber = function (Form_Id, otherIdentificationNumber) {
              //    //loadingOn();
              if (otherIdentificationNumber.OtherIdentificationNumberID != 0) {
                  $scope.typeOfSave = "Edit";
              } else {
                  $scope.typeOfSave = "Add";
              }

              if ($("#" + Form_Id).valid()) {
                  $.ajax({
                      url: rootDir + '/Profile/IdentificationAndLicense/UpdateOtherIdentificationNumberAsync?profileId=' + profileId,
                      type: 'POST',
                      data: new FormData($("#" + Form_Id)[0]),
                      async: false,
                      cache: false,
                      contentType: false,
                      processData: false,
                      success: function (data) {
                          try {
                              if (data.status == "true") {

                                  otherIdentificationNumber.OtherIdentificationNumberID = data.OtherIdentificationNumber.OtherIdentificationNumberID;
                                  $rootScope.MasterProfile.IdentificationLicense.OtherIdentificationNumber = angular.copy(otherIdentificationNumber);
                                  //  $scope.OtherIdentificationNumbers = data.OtherIdentificationNumber;
                                  // FormReset($("#" + Form_Id));
                                  $rootScope.tempObject = {};

                                  if ($scope.typeOfSave == "Add") {
                                      messageAlertEngine.callAlertMessage("addedID", "Other Identification information saved successfully. !!!!", "success", true);
                                  }
                                  else {
                                      $scope.OtherIdentificationNumbersPendingRequest = true;
                                      messageAlertEngine.callAlertMessage("updatedID", "Other Identification information updated successfully. !!!!", "success", true);
                                  }
                                  $rootScope.visibilityControl = '';

                              }
                              else {
                                  $scope.OtherIdError = data.status.split(",");
                                  messageAlertEngine.callAlertMessage('OtherIdError', "", "danger", true);
                              }
                          }
                          catch (e) { };
                      },
                      error: function (e) {
                          try {
                              $scope.OtherIdError = "Sorry for Inconvenience !!!! Please Try Again Later...";
                              messageAlertEngine.callAlertMessage('OtherIdError', "", "danger", true);
                          }
                          catch (e) { };


                      }
                  });
              }
              //  //loadingOff();
          };


          $scope.AutoFillNextAttestationDate = function (objstart) {
              //if (objstart != null && objstart != "")
              //{
              //    $scope.tempObject.NextAttestationDate = new Date(new Date(objstart).setDate(new Date(objstart).getDate() + 120));

              //}
              //else {
              //    $scope.hasError = true;
              //    $scope.tempObject.NextAttestationDate = "";
              //    //$scope.tempObject.NextAttestationDate = new Date(new Date(objstart).setMonth(new Date(objstart).getMonth() + 24));
              //    //$scope.tempObject.NextAttestationDate = new Date(new Date(objstart).setDate(new Date(objstart).getDate() + 120));
              //    //return $scope.tempObject.NextAttestationDate;
              //}

          }

          $scope.$watch("tempObject.LastCAQHAttestationDate", function (newV, oldV) {
              if (newV === oldV) {
                  return;
              } else if (newV != null && newV != "") {
                  $scope.tempObject.NextAttestationDate = new Date(new Date(newV).setDate(new Date(newV).getDate() + 120));
              }
              else {
                  $scope.hasError = true;
                  $scope.tempObject.NextAttestationDate = "";
              }

          })

          $scope.resetDates = function () {
              try {
                  $scope.ErrormessageforCDSstate = false;
                  $scope.Errormessage = '';
                  $scope.tempObject.IssueDate = new Date();
                  $scope.tempObject.ExpiryDate = new Date();
                  $scope.tempObject.CurrentIssueDate = new Date();
              }
              catch (e)
              { }
          };

          //$rootScope.IdentificationLicensesLoaded = true;
          //$scope.dataLoaded = false;
          //$rootScope.$on('IdentificationLicenses', function () {
          //    if (!$scope.dataLoaded) {
          //        $rootScope.IdentificationLicensesLoaded = false;
          //        //var data = JSON.parse(identificationLicenses);
          //        //try {            
          //        //    for (key in data) {
          //        //        $rootScope.$emit(key, data[key]);
          //        //        //call respective controller to load data (PSP)
          //        //    }
          //        //    $rootScope.IdentificationLicensesLoaded = true;
          //        //    //$rootScope.$broadcast("LoadRequireMasterData");
          //        //} catch (e) {
          //        //    $rootScope.IdentificationLicensesLoaded = true;
          //        //}
          //        $http({
          //            method: 'GET',
          //            url: rootDir + '/Profile/MasterProfile/GetIdentificationAndLicensesProfileDataAsync?profileId=' + profileId
          //        }).success(function (data, status, headers, config) {
          //            try {
          //                for (var key in data) {
          //                    $rootScope.$emit(key, data[key]);
          //                    //call respective controller to load data (PSP)
          //                }
          //                $rootScope.IdentificationLicensesLoaded = true;
          //                $rootScope.$broadcast("LoadRequireMasterDataIdentificationLicenses");
          //            } catch (e) {
          //                $rootScope.IdentificationLicensesLoaded = true;
          //            }

          //        }).error(function (data, status, headers, config) {
          //            $rootScope.IdentificationLicensesLoaded = true;
          //        });
          //        $scope.dataLoaded = true;
          //    }
          //});

          //$rootScope.$broadcast("IdentificationLicenses");
          ///////////////////////////---------------------------- End Controller ------------------
      }
    ]);
});