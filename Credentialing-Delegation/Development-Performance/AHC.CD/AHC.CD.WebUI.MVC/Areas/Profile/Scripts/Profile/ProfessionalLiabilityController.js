//--------------------- Author: KRGLV --------------------------------
//--------------- User Use Strict Section ---------------

define(['Util/MasterProfileApp'], function (app) {
    'use strict';
    app.register.controller('ProfessionalLiabilityController', ['$scope', '$rootScope', '$http', 'httpq', 'masterDataService', 'messageAlertEngine', '$filter', 'profileUpdates',
      function ($scope, $rootScope, $http, httpq, masterDataService, messageAlertEngine, $filter, profileUpdates) {
          
          $scope.submitButtonText = "Add";

          $(function () {
              _.forEach($rootScope.MasterProfile.ProfessionalLiability.ProfessionalLiabilityInfoes, function (n) {
                  n.InsuranceCarrierAddressID = n.InsuranceCarrierAddressID ? n.InsuranceCarrierAddressID : "";
              });
              
              $scope.ProfessionalLiabilityPendingRequest = profileUpdates.getUpdates('Professional Liability', 'Professional Liability Info');

              if (!$rootScope.MasterData.InsuranceCarriers) {
                  httpq.get(rootDir + "/Profile/MasterData/GetAllInsuranceCarriers").then(function (data) {
                      $rootScope.MasterData.InsuranceCarriers = data;
                      //$rootScope.MasterData.InsuranceCarriers = data;
                  });
              }
          });
          
          //------------------ author : krglv --------------------
          // ---------------------- clear on change ----------------------
          $scope.clearInsuranceCarrierLevel = function () {
              //$scope.Location = [];
              $rootScope.tempObject.InsuranceCarrierAddress = {};
          };

          $scope.setFiles = function (file) {
              $(file).parent().parent().find(".jancyFileWrapTexts").find("span").width($(file).parent().parent().width() < 243 ? $(file).parent().parent().width() : 243);

          }

          $scope.getLocation = function () {
              $rootScope.tempObject.InsuranceCarrierAddressID = "";
              var temp = $rootScope.MasterData.InsuranceCarriers.filter(function (masterInsuranceCarriers) { return masterInsuranceCarriers.InsuranceCarrierID == $rootScope.tempObject.InsuranceCarrierID })[0];
              return temp.InsuranceCarrierAddresses;
          };

          $scope.getAddress = function () {
              $scope.LocationAddress = $scope.Location.filter(function (location) { return location.InsuranceCarrierAddressID == $rootScope.tempObject.InsuranceCarrierAddressID })[0];
              $rootScope.tempObject.InsuranceCarrierAddress = $scope.LocationAddress;
          };

          //To Display the drop down div
          $scope.searchCumDropDown = function (divId) {
              $("#" + divId).show();
          };

          $scope.addIntoInsuranceDropDown = function (inscurance, div) {
              $rootScope.tempObject.InsuranceCarrier = angular.copy(inscurance);
              $rootScope.tempObject.InsuranceCarrierAddress = {};
              $scope.Location = angular.copy(inscurance.InsuranceCarrierAddresses);
              $("#" + div).hide();
          }

          $scope.addIntoAddressDropDown = function (insuranceCarrierAddress, div) {
              $rootScope.tempObject.InsuranceCarrierID = insuranceCarrierAddress.InsuranceCarrierAddressID;
              $rootScope.tempObject.InsuranceCarrierAddress = angular.copy(insuranceCarrierAddress);
              $("#" + div).hide();
          }

          //to show renew div
          $scope.ShowRenewDiv = false;

          $scope.RenewDiv = function (Li) {
              if (Li.ExpirationDate == null)
              { $scope.ShowRenewDiv = false; }
              else
              {
                  $scope.Location = $rootScope.MasterData.InsuranceCarriers.filter(function (masterInsuranceCarriers) { return masterInsuranceCarriers.InsuranceCarrierID == Li.InsuranceCarrierID })[0];
                  $scope.ShowRenewDiv = true;
              }
          };

          //====================== Professional Liability ===================
          $scope.CountryDialCodes = countryDailCodes;

          $scope.showContryCodeDiv = function (countryCodeDivId) {
              changeVisibilityOfCountryCode();
              $("#" + countryCodeDivId).show();
          };
          $scope.reseterrormessages = function () {
              $scope.isErrorInsuranceCarrier = false;
          }
          $scope.cancelProfessionalLiabilty = function (condition) {
              setProfessionalLiabiltyCancelParameters();
          };

          $scope.isErrorInsuranceCarrier = false;
          $scope.setErrorBit = function (name) {
              if (name == "") {
                  $scope.isErrorInsuranceCarrier = true;
              } else {
                  $scope.isErrorInsuranceCarrier = false;
              }
          };
          $scope.editliabilityisurance = function (data) {
              $scope.Location = [];
              $scope.Location = $rootScope.MasterData.InsuranceCarriers.filter(function (masterInsuranceCarriers) { return masterInsuranceCarriers.InsuranceCarrierID == data.InsuranceCarrierID })[0].InsuranceCarrierAddresses;
              //$scope.Location.push(angular.copy(data.InsuranceCarrierAddress));
          }

          $scope.saveProfessionalLiabilty = function (professionalLiabilty, index) {
              loadingOn();
              var validationStatus;
              var url;
              var myData = {};
              var formData1;

              if ($scope.visibilityControl == 'addLi') {
                  //Add Details - Denote the URL
                  formData1 = $('#newShowProfessionalLiabiltyDiv').find('form');
                  url = rootDir + "/Profile/ProfessionalLiability/AddProfessionalLiabilityAsync?profileId=" + profileId;
              }
              else if ($scope.visibilityControl == (index + '_editLi')) {
                  //Update Details - Denote the URL
                  formData1 = $('#professionalLiabiltyEditDiv' + index).find('form');
                  url = rootDir + "/Profile/ProfessionalLiability/UpdateProfessionalLiabilityAsync?profileId=" + profileId;
                  //$scope.Location = $scope.getLocation();

              }
              else {
                  //Update Details - Denote the URL
                  formData1 = $('#professionalLiabiltyRenewDiv' + index).find('form');
                  url = rootDir + "/Profile/ProfessionalLiability/RenewProfessionalLiabilityAsync?profileId=" + profileId;
                  $scope.Location = $scope.getLocation();
              }
              $scope.setErrorBit($(formData1).find($('[name=Name]')).val());
              ResetFormForValidation(formData1);
              validationStatus = formData1.valid();

              if (validationStatus && !$scope.isErrorInsuranceCarrier) {
                  // Simple POST request example (passing data) :
                  $.ajax({
                      url: url,
                      type: 'POST',
                      data: new FormData(formData1[0]),
                      async: false,
                      cache: false,
                      contentType: false,
                      processData: false,
                      success: function (data) {
                          try {
                              if (data.status == "true") {
                                  data.professionalLiability.OriginalEffectiveDate = ConvertDateFormat(data.professionalLiability.OriginalEffectiveDate);
                                  data.professionalLiability.EffectiveDate = ConvertDateFormat(data.professionalLiability.EffectiveDate);
                                  data.professionalLiability.ExpirationDate = ConvertDateFormat(data.professionalLiability.ExpirationDate);

                                  if (!data.professionalLiability.InsuranceCarrierAddressID) { data.professionalLiability.InsuranceCarrierAddressID = ""; }

                                  data.professionalLiability.InsuranceCarrierAddress = $scope.Location.filter(function (location) { return location.InsuranceCarrierAddressID == data.professionalLiability.InsuranceCarrierAddressID })[0];
                                  for (var i = 0; i < $rootScope.MasterData.InsuranceCarriers.length; i++) {
                                      if ($rootScope.MasterData.InsuranceCarriers[i].InsuranceCarrierID == data.professionalLiability.InsuranceCarrierID) {

                                          data.professionalLiability.InsuranceCarrier = angular.copy($rootScope.MasterData.InsuranceCarriers[i]);
                                          break;
                                      }
                                  }

                                  //data.professionalLiability

                                  if ($scope.visibilityControl == (index + '_editLi')) {
                                      $scope.ProfessionalLiabilityPendingRequest = true;
                                      $rootScope.MasterProfile.ProfessionalLiability.ProfessionalLiabilityInfoes[index] = data.professionalLiability;
                                      $rootScope.operateViewAndAddControl(index + '_viewLi');
                                      messageAlertEngine.callAlertMessage("updatedProfessionalLiability" + index, "Professional Liability updated successfully !!!!", "success", true);

                                  }
                                  else if ($scope.visibilityControl == (index + '_RenewLiability')) {
                                      $scope.ProfessionalLiabilityPendingRequest = true;
                                      $rootScope.MasterProfile.ProfessionalLiability.ProfessionalLiabilityInfoes[index] = data.professionalLiability;
                                      $rootScope.operateViewAndAddControl(index + '_viewLi');
                                      messageAlertEngine.callAlertMessage("renewedProfessionalLiability" + index, "Professional Liability information renewed successfully !!!!", "success", true);
                                  }
                                  else {
                                      $rootScope.MasterProfile.ProfessionalLiability.ProfessionalLiabilityInfoes.push(data.professionalLiability);
                                      $rootScope.operateCancelControl('');
                                      messageAlertEngine.callAlertMessage("addedNewProfessionalLiability", "Professional Liability saved successfully !!!!", "success", true);
                                  }
                                  myData = data;
                                  $scope.IsProfLiabilityHasError = false;
                                  $rootScope.tempObject = {};
                                  FormReset(formData1);

                              } else {
                                  messageAlertEngine.callAlertMessage('errorProfessionalLiability' + index, "", "danger", true);
                                  $scope.errorProfessionalLiability = data.status.split(",");
                              }
                          } catch (e) {

                          }
                      },
                      error: function (e) {
                          messageAlertEngine.callAlertMessage('errorProfessionalLiability' + index, "", "danger", true);
                          $scope.errorProfessionalLiability = "Sorry for Inconvenience !!!! Please Try Again Later...";
                      }
                  });
              } else if ($scope.isErrorInsuranceCarrier) {
                  $scope.isErrorInsuranceCarrier = true;
              } else {

              }

              //$rootScope.$broadcast('UpdateProfessionalLiabilityInfoDoc', myData);

              loadingOff();
          };


          function ResetProfessionalLiabiltyForm() {
              $('#newShowProfessionalLiabiltyDiv').find('.professionalLiabiltyForm')[0].reset();
              $('#newShowProfessionalLiabiltyDiv').find('span').html('');
          }

          $scope.initProfessionalLiabilityWarning = function (Li) {
              if (angular.isObject(Li)) {
                  $scope.tempProfessionalLiability = Li;
              }
              $('#professionalLiabilityWarningModal').modal();
          };

          $scope.removeProfessionalLiability = function (ProfessionalLiabilityInfoes) {
              var validationStatus = false;
              var url = null;
              var myData = {};
              var $formData = null;
              $scope.isRemoved = true;
              $formData = $('#removeProfessionalLiability');
              url = rootDir + "/Profile/ProfessionalLiability/RemoveProfessionalLiability?profileId=" + profileId;
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
                                  var obj = $filter('filter')(ProfessionalLiabilityInfoes, { ProfessionalLiabilityInfoID: data.professionalLiability.ProfessionalLiabilityInfoID })[0];
                                  ProfessionalLiabilityInfoes.splice(ProfessionalLiabilityInfoes.indexOf(obj), 1);
                                  if ($scope.dataFetchedPL == true) {
                                      obj.HistoryStatus = 'Deleted';
                                      $scope.ProfessionalLiabilityInfoHistory.push(obj);
                                  }
                                  $scope.isRemoved = false;
                                  $('#professionalLiabilityWarningModal').modal('hide');
                                  $rootScope.operateCancelControl('');
                                  myData = data;
                                  messageAlertEngine.callAlertMessage("addedNewProfessionalLiability", "Professional Liability Removed successfully.", "success", true);
                              } else {
                                  $('#professionalLiabilityWarningModal').modal('hide');
                                  messageAlertEngine.callAlertMessage("removeProfessionalLiability", data.status, "danger", true);
                                  $scope.errorProfessionalLiability = "Sorry for Inconvenience !!!! Please Try Again Later...";
                              }
                          } catch (e) {

                          }
                      },
                      error: function (e) {

                      }
                  });
              }
              //$rootScope.$broadcast('RemoveProfessionalLiabilityInfoDoc', myData);
          };

          $rootScope.ProfessionalLiabilityLoaded = true;
          $scope.dataLoaded = false;

          //....................Professional Liability History............................//
          $scope.ProfessionalLiabilityInfoHistory = [];
          $scope.dataFetchedPL = false;

          $scope.showLiabilityHistory = function (loadingId) {
              if ($scope.ProfessionalLiabilityInfoHistory.length == 0) {
                  $("#" + loadingId).css('display', 'block');
                  var url = rootDir + "/Profile/ProfileHistory/GetAllProfessionalLiabilityInfoHistory?profileId=" + profileId;
                  $http.get(url).success(function (data) {
                      try {
                          $scope.ProfessionalLiabilityInfoHistory = data;
                          for (var i = 0; i < $scope.ProfessionalLiabilityInfoHistory.length; i++) {
                              if ($scope.ProfessionalLiabilityInfoHistory[i].HistoryStatus == '' || !$scope.ProfessionalLiabilityInfoHistory[i].HistoryStatus) {
                                  $scope.ProfessionalLiabilityInfoHistory[i].HistoryStatus = 'Renewed';
                              }
                          }
                          $scope.showLiabilityHistoryTable = true;
                          $scope.dataFetchedPL = true;
                          $("#" + loadingId).css('display', 'none');
                      } catch (e) {

                      }
                  });
              }
              else {
                  $scope.showLiabilityHistoryTable = true;
              }
          }

          $scope.cancelLiabilityHistory = function () {
              $scope.showLiabilityHistoryTable = false;
          }

      }]);
});