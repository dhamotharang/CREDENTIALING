//--------------------- Author: KRGLV --------------------------------
//--------------- User Use Strict Section ---------------

define(['Util/MasterProfileApp'], function (app) {
    'use strict';
    app.register.controller('ContractInformationController', ['$scope', '$rootScope', 'masterDataService', '$http', 'httpq', 'messageAlertEngine', '$filter',
      function ($scope, $rootScope, masterDataService, $http, httpq, messageAlertEngine, $filter) {

          $(function () {
              _.forEach($rootScope.MasterProfile.ContractInformation.ContractInfoes, function (n) {
                  n.ContractGroupInfoes = _.filter(n.ContractGroupInfoes, function (o) { return o.Status != 'Inactive'; });
              });
              
              //--------------- need to put in cshtml page of that controller ----------------
              $("#AddContractButton").prop("disabled", true);
              $("#AddContractButton").css("cursor", "not-allowed");
          });

          $(function () {
              if (!$rootScope.MasterData.Groups) {
                  httpq.get(rootDir + "/MasterData/Organization/GetGroups").then(function (data) {
                      $rootScope.MasterData.Groups = data;
                      //$scope.masterGroups = data;
                  });
              }
          });

          $scope.EnableContract = function () {
              try {
                  if ($scope.contractInfoes == "undefined") {

                      return false;
                  }
                  else if ($scope.contractInfoes != "undefined") {
                      if ($rootScope.MasterProfile.ContractInformation.ContractInfoes[0] != "undefined") {

                          if ($rootScope.MasterProfile.ContractInformation.ContractInfoes[0].ContractStatus == 'Inactive')
                              return false;
                      }
                  }
                  return true;
              }
              catch (e) {


              }
          }

          //....................Group Information History............................//
          $scope.groupHistoryArray = [];

          $scope.showGroupHistory = function (loadingId) {
              if ($scope.groupHistoryArray.length == 0) {
                  $("#" + loadingId).css('display', 'block');
                  var url = rootDir + "/Profile/ProfileHistory/GetAllContractGroupInfoHistory?profileId=" + profileId;
                  $http.get(url).success(function (data) {
                      try {
                          $scope.groupHistoryArray = data;
                          $scope.showGroupHistoryTable = true;
                          $("#" + loadingId).css('display', 'none');
                      } catch (e) {

                      }
                  });
              }
              else {
                  $scope.showGroupHistoryTable = true;
              }

          }

          $scope.cancelGroupHistory = function () {

              $scope.showGroupHistoryTable = false;
          }

          //-----------------------End---------------------------------------//

          $scope.EditGroup = function (groupEditDivId, groupInfo, ContractInfoID) {
              $rootScope.tempObject = "";
              $scope.ContractInfoID = ContractInfoID;
              $rootScope.visibilityControl = groupEditDivId;
              $rootScope.tempObject = angular.copy(groupInfo);

          };

          $scope.AddContract = function (contractAddDiv) {

              $rootScope.operateViewAndAddControl(contractAddDiv);
              $scope.providerStatus = true;
              $scope.showAddGroup = true;
          }

          $scope.operateEditControl = function (sectionValue, obj) {
              $rootScope.tempObject = {};
              $rootScope.visibilityControl = sectionValue;
              $rootScope.tempObject = obj;
          }

          $scope.EditContract = function (contractEditDiv, contractEditData) {
              $rootScope.tempObject = {};
              $rootScope.tempObject = angular.copy(contractEditData);
              $rootScope.visibilityControl = contractEditDiv;
              $scope.providerStatus = false;
              $scope.showAddGroup = true;
          };

          ////----------------------------------saving Contract Data----------------------------

          $scope.showAddGroup = true;

          $scope.AddGroup = function (groupAddDivId, ContractInfoID) {
              $scope.cancelGroup();
              $scope.ContractInfoID = ContractInfoID;
              $rootScope.visibilityControl = groupAddDivId;
              $scope.showAddGroup = false;
          }

          //$("#AddContractButton").attr("disabled", "disabled");

          $scope.cancelGroup = function () {
              // $scope.tempObject = "";
              $scope.showAddGroup = true;
              $rootScope.operateCancelControl('');
          }
          $scope.GetContractOption = function (option) {
              var Option = option;
              if (Option == "1") {

                  //$("#AddContractButton").attr("disabled", "disabled");
                  $("#AddContractButton").css("cursor", "not-allowed");
                  $("#AddContractButton").prop("disabled", true);
              } else if (Option == "2") {
                  $("#AddContractButton").css("cursor", "not-allowed");
                  $("#AddContractButton").prop("disabled", true);
                  //$("#AddContractButton").attr("disabled", "disabled");

              } else {

                  $("#AddContractButton").css("cursor", "pointer");
                  $("#AddContractButton").prop("disabled", false);
              }
          }

          $scope.saveContractInformation = function (contractInformation, index) {
              loadingOn();
              var validationStatus;
              var url;
              var myData = {};
              var formData1;
              var tempGroups;


              tempGroups = contractInformation.ContractGroupInfoes;

              if ($scope.visibilityControl == 'addci') {
                  //Add Details - Denote the URL
                  formData1 = $('#ContractInformationAddDiv').find('form');
                  url = rootDir + "/Profile/Contract/AddContractInformation?profileId=" + profileId;
              }
              else if ($scope.visibilityControl == ('_editci')) {
                  //Update Details - Denote the URL
                  formData1 = $('#ContractInformationEditDiv').find('form');
                  url = rootDir + "/Profile/Contract/UpdateContractInformation?profileId=" + profileId;
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
                                  data.contractInformation.JoiningDate = ConvertDateFormat(data.contractInformation.JoiningDate);
                                  data.contractInformation.ExpiryDate = ConvertDateFormat(data.contractInformation.ExpiryDate);
                                  myData = data;

                                  if ($scope.visibilityControl != ('_editci')) {
                                      $rootScope.MasterProfile.ContractInformation.ContractInfoes[0] = data.contractInformation;
                                      $rootScope.operateCancelControl('');
                                      messageAlertEngine.callAlertMessage("addedNewContractInformation", "Employment Information added successfully !!!!", "success", true);
                                  }


                                  else {
                                      $rootScope.MasterProfile.ContractInformation.ContractInfoes[0] = data.contractInformation;
                                      $rootScope.MasterProfile.ContractInformation.ContractInfoes[0].ContractGroupInfoes = tempGroups;
                                      $rootScope.operateCancelControl('');
                                      messageAlertEngine.callAlertMessage("updatedContractInformation", "Employment Information updated successfully !!!!", "success", true);

                                  }
                                  FormReset(formData1);
                              }
                              else {
                                  messageAlertEngine.callAlertMessage('errorContractInformation', "", "danger", true);
                                  $scope.errorContractInformation = data.status.split(",");

                              }
                          } catch (e) {

                          }
                      },
                      error: function (e) {
                          messageAlertEngine.callAlertMessage('errorContractInformation' + index, "", "danger", true);
                          $scope.errorContractInformation = "Sorry for Inconvenience !!!! Please Try Again Later...";
                      }
                  });

              }

              //$rootScope.$broadcast('UpdateContractInfoeDoc', myData);

              loadingOff();
          };

          //----------------------------------End----------------------------

          ////----------------------------------saving Group Data----------------------------

          $scope.saveGroupInformation = function (contractGroupInformation, editDivId, index) {
              loadingOn();
              var validationStatus;
              var url;
              var formData1;
              var tempPractisingGroups;
              for (var i = 0; i < $rootScope.MasterData.Groups.length; i++) {

                  if ($rootScope.MasterData.Groups[i].PracticingGroupID == parseInt(contractGroupInformation.PracticingGroupId)) {
                      tempPractisingGroups = angular.copy($rootScope.MasterData.Groups[i]);
                  }
              }

              if ($scope.visibilityControl == 'addgi') {
                  //Add Details - Denote the URL
                  formData1 = $('#newContractInformationDiv').find('form');
                  url = rootDir + "/Profile/Contract/AddContractGroupInformation?profileId=" + profileId + "&contractInfoId=" + $scope.ContractInfoID;
              }
              else if ($scope.visibilityControl == (index + '_editgi')) {
                  //Update Details - Denote the URL
                  formData1 = $('#ContractGroupInformationEditDiv' + index).find('form');
                  url = rootDir + "/Profile/Contract/UpdateContractGroupInformation?contractInfoId=" + $scope.ContractInfoID + "&profileId=" + profileId;
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

                                  data.contractGroupInformation.JoiningDate = ConvertDateFormat(data.contractGroupInformation.JoiningDate);
                                  data.contractGroupInformation.ExpiryDate = ConvertDateFormat(data.contractGroupInformation.ExpiryDate);

                                  data.contractGroupInformation.PracticingGroup = tempPractisingGroups;

                                  if ($scope.visibilityControl != (index + '_editgi')) {
                                      $rootScope.MasterProfile.ContractInformation.ContractInfoes[0].ContractGroupInfoes.push(data.contractGroupInformation);
                                      $rootScope.operateCancelControl('');
                                      messageAlertEngine.callAlertMessage("addedNewContractGroupInformation", "Employment Group Information saved successfully !!!!", "success", true);
                                  }

                                  else {
                                      $rootScope.MasterProfile.ContractInformation.ContractInfoes[0].ContractGroupInfoes[index] = data.contractGroupInformation;
                                      $rootScope.operateViewAndAddControl(index + '_viewgi');
                                      //$rootScope.operateCancelControl('');
                                      messageAlertEngine.callAlertMessage("updatedContractGroupInformation" + index, "Employment Group Information updated successfully !!!!", "success", true);
                                  }
                                  FormReset(formData1);
                              } else {
                                  messageAlertEngine.callAlertMessage('errorContractGroupInformation', "", "danger", true);
                                  $scope.errorContractGroupInformation = data.status.split(",");

                              }
                          } catch (e) {

                          }

                      },
                      error: function (e) {
                          messageAlertEngine.callAlertMessage('errorContractGroupInformation', "", "danger", true);
                          $scope.errorContractGroupInformation = "Sorry for Inconvenience !!!! Please Try Again Later...";
                      }
                  });

              }
              loadingOff();
          };

          $scope.initGroupInfoWarning = function (gi) {
              $($('#groupInfoWarningModal').find('button')[2]).attr('disabled', false);
              if (angular.isObject(gi)) {
                  $scope.tempGroupInfo = gi;
              }
              $('#groupInfoWarningModal').modal();
          };

          $scope.removeGroupInfo = function (groupInfo, ContractInfoID) {
              var validationStatus = false;
              $($('#groupInfoWarningModal').find('button')[2]).attr('disabled', true);
              var url = null;
              var myData = {};
              var $formData = null;
              $scope.isRemoved = true;
              $formData = $('#removeGroupInfo');
              url = rootDir + "/Profile/Contract/RemoveContractGroupInformationAsync?profileId=" + profileId + "&contractInfoId=" + ContractInfoID;
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
                                  var obj = $filter('filter')($rootScope.MasterProfile.ContractInformation.ContractInfoes[0].ContractGroupInfoes, { ContractGroupInfoId: data.groupInfo.ContractGroupInfoId })[0];
                                  $rootScope.MasterProfile.ContractInformation.ContractInfoes[0].ContractGroupInfoes.splice($rootScope.MasterProfile.ContractInformation.ContractInfoes[0].ContractGroupInfoes.indexOf(obj), 1);
                                  if ($scope.groupHistoryArray.length != 0) {
                                      obj.HistoryStatus = "Deleted";
                                      $scope.groupHistoryArray.push(obj);
                                  }
                                  $scope.isRemoved = false;
                                  $('#groupInfoWarningModal').modal('hide');
                                  $rootScope.operateCancelControl('');
                                  myData = data;
                                  $scope.RemovedMsg = true;
                                  messageAlertEngine.callAlertMessage("removeContractGroupInformation", "Group Information Removed successfully.", "success", true);

                              } else {
                                  $('#groupInfoWarningModal').modal('hide');
                                  messageAlertEngine.callAlertMessage("addedNewContractGroupInformation", data.status, "danger", true);
                                  $scope.errorContractGroupInformation = "Sorry for Inconvenience !!!! Please Try Again Later...";
                              }
                              $scope.showAddGroup = true;

                          } catch (e) {

                          }
                      },
                      error: function (e) {

                      }

                  });
              }
              //$rootScope.$broadcast('RemoveContractInfoeDoc', myData);
          };

          
          $scope.setFiles = function (file) {
              $(file).parent().parent().find(".jancyFileWrapTexts").find("span").width($(file).parent().parent().width() < 243 ? $(file).parent().parent().width() : 243);

          }

      }]);
});