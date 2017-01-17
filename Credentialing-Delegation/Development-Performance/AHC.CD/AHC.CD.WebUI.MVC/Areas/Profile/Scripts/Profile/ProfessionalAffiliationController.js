//--------------------- Author: KRGLV --------------------------------
//--------------- User Use Strict Section ---------------

define(['Util/MasterProfileApp'], function (app) {
    'use strict';
    app.register.controller('ProfessionalAffiliationController', ['$scope', '$rootScope', '$http', 'httpq', 'messageAlertEngine', '$filter', 'profileUpdates',
      function ($scope, $rootScope, $http, httpq, messageAlertEngine, $filter, profileUpdates) {

          $(function () {
                //-------------- No Need Implimentation Any ---------------
          });

          $scope.resetDates = function () {
              try {
                  $scope.tempObject.StartDate = new Date();
                  $scope.tempObject.EndDate = new Date();

              }
              catch (e)
              { }
          };

          //=============== Professional Affiliation Conditions ==================

          $scope.submitButtonText = "Add";

          

          //....................Professional Affiliation History............................//
          $scope.affiliationHistoryArray = [];
          $scope.dataFetchedPA = false;

          $scope.showAffiliationHistory = function (loadingId) {
              if ($scope.affiliationHistoryArray.length == 0) {
                  $("#" + loadingId).css('display', 'block');
                  var historuUrl = rootDir + "/Profile/ProfileHistory/GetAllProfessionalAffiliationHistory?profileId=" + profileId;
                  $http.get(historuUrl).success(function (data) {
                      try {
                          $scope.affiliationHistoryArray = data;
                          $scope.showAffiliationHistoryTable = true;
                          $scope.dataFetchedPA = true;
                          $("#" + loadingId).css('display', 'none');
                      } catch (e) {

                      }
                  });
              } else {
                  var historuUrl = rootDir + "/Profile/ProfileHistory/GetAllProfessionalAffiliationHistory?profileId=" + profileId;
                  $http.get(historuUrl).success(function (data) {
                      $scope.affiliationHistoryArray = data;
                      $scope.showAffiliationHistoryTable = true;
                  });
                  //  $scope.showAffiliationHistoryTable = true;
              }
          }

          $scope.cancelAffiliationHistory = function () {
              $scope.showAffiliationHistoryTable = false;
          }

          //-----------------------End---------------------------------------//

          $scope.saveProfessionalAffiliation = function (professionalAffiliation, index) {
              loadingOn();
              var validationStatus;
              var url;
              var formData1;

              if ($scope.visibilityControl == 'addpa') {
                  //Add Details - Denote the URL
                  formData1 = $('#newShowProfessionalAffiliationDiv').find('form');
                  url = rootDir + "/Profile/ProfessionalAffiliation/AddProfessionalAffiliation?profileId=" + profileId;
              }
              else if ($scope.visibilityControl == (index + '_editpa')) {
                  //Update Details - Denote the URL
                  formData1 = $('#professionalAffiliationEditDiv' + index).find('form');
                  url = rootDir + "/Profile/ProfessionalAffiliation/UpdateProfessionalAffiliation?profileId=" + profileId;
              }

              ResetFormForValidation(formData1);
              validationStatus = formData1.valid()


              if (validationStatus) {

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
                                  data.professionalAffiliation.StartDate = ConvertDateFormat(data.professionalAffiliation.StartDate);
                                  data.professionalAffiliation.EndDate = ConvertDateFormat(data.professionalAffiliation.EndDate);

                                  if ($scope.visibilityControl != (index + '_editpa')) {
                                      $rootScope.MasterProfile.ProfessionalAffiliation.ProfessionalAffiliationInfos.push(data.professionalAffiliation);
                                      $rootScope.operateCancelControl('');
                                      messageAlertEngine.callAlertMessage("addedNewProfessionalAffiliation", "Professional Affiliation saved successfully !!!!", "success", true);
                                  }
                                  else {
                                      $rootScope.MasterProfile.ProfessionalAffiliation.ProfessionalAffiliationInfos[index] = data.professionalAffiliation;
                                      $rootScope.operateViewAndAddControl(index + '_viewpa');
                                      $scope.ProfessionalAffiliationPendingRequest = true;
                                      messageAlertEngine.callAlertMessage("updatedProfessionalAffiliation" + index, "Professional Affiliation updated successfully !!!!", "success", true);
                                  }

                                  $scope.IsProfessionalAffiliationHasError = false;
                                  $scope.resetDates();
                                  FormReset(formData1);


                              } else {
                                  messageAlertEngine.callAlertMessage('errorProfessionalAffiliation' + index, "", "danger", true);
                                  $scope.errorProfessionalAffiliation = data.status.split(",");
                              }

                          } catch (e) {

                          }

                      },
                      error: function (e) {
                          messageAlertEngine.callAlertMessage('errorProfessionalAffiliation' + index, "", "danger", true);
                          $scope.errorProfessionalAffiliation = "Sorry for Inconvenience !!!! Please Try Again Later...";
                      }
                  });

              }
              loadingOff();
          };

          $scope.setFiles = function (file) {
              $(file).parent().parent().find(".jancyFileWrapTexts").find("span").width($(file).parent().parent().width() < 243 ? $(file).parent().parent().width() : 243);

          }

          function ResetProfessionalAffiliationForm() {
              $('#newShowProfessionalAffiliationDiv').find('.professionalAffiliationForm')[0].reset();
              $('#newShowProfessionalAffiliationDiv').find('span').html('');
          }

          $scope.initProfessionalAffiliationWarning = function (affiliation) {
              if (angular.isObject(affiliation)) {
                  $scope.tempAffiliation = affiliation;
              }
              $('#professionalAffiliationWarningModal').modal();
          };

          $scope.removeProfessionalAffiliation = function (ProfessionalAffiliations) {
              var validationStatus = false;
              var url = null;
              var $formData = null;
              $scope.isRemoved = true;
              $formData = $('#editProfessionalAffiliation');
              url = rootDir + "/Profile/ProfessionalAffiliation/RemoveProfessionalAffiliation?profileId=" + profileId;
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
                                  var obj = $filter('filter')(ProfessionalAffiliations, { ProfessionalAffiliationInfoID: data.professionalAffiliation.ProfessionalAffiliationInfoID })[0];
                                  ProfessionalAffiliations.splice(ProfessionalAffiliations.indexOf(obj), 1);
                                  if ($scope.dataFetchedPA == true) {
                                      obj.HistoryStatus = 'Deleted';
                                      $scope.affiliationHistoryArray.push(obj);
                                  }
                                  $scope.isRemoved = false;
                                  $('#professionalAffiliationWarningModal').modal('hide');
                                  $rootScope.operateCancelControl('');
                                  messageAlertEngine.callAlertMessage("addedNewProfessionalAffiliation", "Professional Affiliation Removed successfully.", "success", true);
                                  //$scope.affiliationHistoryArray.push(obj);
                              } else {
                                  $('#professionalAffiliationWarningModal').modal('hide');
                                  messageAlertEngine.callAlertMessage("removeProfessionalAffiliation", data.status, "danger", true);
                                  $scope.errorProfessionalAffiliation = "Sorry for Inconvenience !!!! Please Try Again Later...";
                              }
                          } catch (e) {

                          }
                      },
                      error: function (e) {

                      }
                  });
              }
          };

          $rootScope.ProfessionalAffiliationLoaded = true;
          $scope.dataLoaded = false;
          
      }]);
});