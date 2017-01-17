//--------------------- Author: KRGLV --------------------------------
//--------------- User Use Strict Section ---------------

define(['Util/MasterProfileApp'], function (app) {
    'use strict';
    app.register.controller('DisclosureQuestionController', ['$scope', '$rootScope', '$http', 'httpq', 'masterDataService', 'messageAlertEngine', 'profileUpdates',
      function ($scope, $rootScope, $http, httpq, masterDataService, messageAlertEngine, profileUpdates) {
          $scope.TempOBJ = {};
          $scope.EditTemp = function (data) {
              //$("#disclosureForm")[0].reset();
              $scope.TempOBJ = angular.copy(data);
          }
          $scope.CancelTemp = function (data) {
              $rootScope.MasterProfile.DisclosureQuestion.ProfileDisclosure = angular.copy(data);
          }
          
          // rootScoped on emitted value catches the value for the model and insert to get the old data for Practice Interest
          $scope.setFiles = function (file) {
              $(file).parent().parent().find(".jancyFileWrapTexts").find("span").width($(file).parent().parent().width() < 243 ? $(file).parent().parent().width() : 243);

          }

          $scope.max = 100;
          $scope.current = 0;
          $scope.setAllNo = function () {
              $scope.isDataPresent = true;
              $('input:radio[class^=selectNo][value=2]').trigger("click");
              $('input:radio[class^=selectNo][value=2]').trigger("click");
          }


          $(function () {

              $scope.ProfileDisclosurePendingRequest = profileUpdates.getUpdates('Disclosure Question', 'Profile Disclosure');

          });

          $(function () {
              if (!$rootScope.MasterData.Questions) {
                  httpq.get(rootDir + "/Profile/MasterData/getAllQuestions").then(function (data) {
                      $rootScope.MasterData.Questions = data;
                      if ($rootScope.MasterData.Questions.length > 0) {
                          $rootScope.MasterData.Questions = $rootScope.MasterData.Questions.filter(function (masterQuestions) { return masterQuestions.QuestionCategoryId != null })
                          for (var i in $rootScope.MasterData.Questions) {
                              $rootScope.MasterData.Questions[i].TableStatus = false;
                          }
                      }
                      //$rootScope.MasterData.Questions = data;
                  });
              }
              if (!$rootScope.MasterData.QuestionCategories) {
                  httpq.get(rootDir + "/Profile/MasterData/getAllQuestionCategories").then(function (data) {
                      $rootScope.MasterData.QuestionCategories = data;
                      //$scope.masterQuestionCategories = data;
                  });
              }
          });

          var QID = 0;
          var index1 = -1;
          $scope.addingDocument = function (index, QuestionID, QData) {
              index1 = $rootScope.MasterData.Questions.indexOf(QData);
              QID = QuestionID;
              $('#file' + index).click();
          }

          $scope.fileList = [];
          $scope.curFile;
          $scope.ImageProperty = {
              file: '',
              QID: -1,
              FileListID: -1,
              FileID: -1,
              FileStatus: ''
          }
          $scope.removeFile = function (fileObj, QID, QData) {
              var index = -1;
              for (var i in $scope.fileList) {
                  if ($scope.fileList[i].QuestionID == QID) {
                      index = i;
                      break;
                  }
              }
              $scope.fileList[index].File.splice($scope.fileList[index].File.indexOf(fileObj), 1);
              if ($scope.fileList[index].File.length == 0) {
                  $rootScope.MasterData.Questions[$rootScope.MasterData.Questions.indexOf(QData)].TableStatus = false;
              }
          }

          $scope.getStyle = function () {
              var transform = ($scope.isSemi ? '' : 'translateY(-50%) ') + 'translateX(-50%)';

              return {
                  'top': $scope.isSemi ? 'auto' : '50%',
                  'bottom': $scope.isSemi ? '5%' : 'auto',
                  'left': '35%',
                  'transform': transform,
                  '-moz-transform': transform,
                  '-webkit-transform': transform,
                  'font-size': $scope.radius / 3.5 + 'px'
              };
          };

          $scope.setFile = function (element) {
              var count = 0;
              var TempArray = [];
              var index = -1;
              var files = [];
              var files = element.files;
              for (var i in $scope.fileList) {
                  if ($scope.fileList[i].QuestionID == QID) {
                      index = i;
                      count++;
                      break;
                  }
              }
              if (count == 0) {
                  for (var i = 0; i < files.length; i++) {
                      $scope.ImageProperty.file = files[i];
                      $scope.ImageProperty.QID = QID;
                      $scope.ImageProperty.FileStatus = 'Active';
                      $scope.ImageProperty.FileListID = $scope.fileList.length;
                      $scope.ImageProperty.FileID = i;
                      TempArray.push($scope.ImageProperty);
                      $scope.fileList.push({ QuestionID: QID, File: TempArray });
                      $rootScope.MasterData.Questions[index1].TableStatus = true;
                      $scope.ImageProperty = {};
                      $scope.$apply();
                  }
              }
              else {
                  for (var i = 0; i < files.length; i++) {
                      $scope.ImageProperty.file = files[i];
                      $scope.ImageProperty.QID = QID;
                      $scope.ImageProperty.FileStatus = 'Active';
                      $scope.ImageProperty.FileListID = index;
                      $scope.ImageProperty.FileID = $scope.fileList[index].File.length + i;
                      $scope.fileList[index].File.push($scope.ImageProperty);
                      $rootScope.MasterData.Questions[index1].TableStatus = true;
                      $scope.ImageProperty = {};
                      $scope.$apply();
                  }
              }
              $scope.UploadFile();

          }

          $scope.UploadFile = function () {

              for (var i = 0; i < $scope.fileList.length; i++) {
                  for (var j = 0; j < $scope.fileList[i].File.length; j++) {
                      if ($scope.fileList[i].File[j].FileStatus == 'Active') {
                          $scope.UploadFileIndividual($scope.fileList[i].File[j].file,
                                              $scope.fileList[i].File[j].file.name,
                                              $scope.fileList[i].File[j].file.type,
                                              $scope.fileList[i].File[j].file.size,
                                              $scope.fileList[i].File[j].QID,
                                              $scope.fileList[i].File[j].FileListID,
                                              $scope.fileList[i].File[j].FileID
                                              );
                          $scope.fileList[i].File[j].FileStatus = 'Inactive';
                      }
                  }
              }
          }

          $scope.UploadFileIndividual = function (fileToUpload, name, type, size, Qindex, FLindex, Findex) {
              $scope.current = 0;
              var reqObj = new XMLHttpRequest();
              reqObj.upload.addEventListener("progress", uploadProgress, false)
              reqObj.addEventListener("load", uploadComplete, false)
              reqObj.addEventListener("error", uploadFailed, false)
              reqObj.addEventListener("abort", uploadCanceled, false)
              reqObj.open("POST", rootDir + "/Profile/DisclosureQuestion/FileUpload", true);
              reqObj.setRequestHeader("Content-Type", "multipart/form-data");
              reqObj.setRequestHeader('X-File-Name', name);
              reqObj.setRequestHeader('X-File-Type', type);
              reqObj.setRequestHeader('X-File-Size', size);


              reqObj.send(fileToUpload);

              function uploadProgress(evt) {
                  if (evt.lengthComputable) {

                      var uploadProgressCount = Math.round(evt.loaded * 100 / evt.total);
                      $scope.current = uploadProgressCount;

                      if (uploadProgressCount == 100) {
                          $scope.current = uploadProgressCount;
                      }

                  }
              }

              function uploadComplete(evt) {


                  $scope.NoOfFileSaved++;
                  $scope.$apply();
              }

              function uploadFailed(evt) {
                  alert("Failed");
              }

              function uploadCanceled(evt) {

                  alert("Canceled");
              }

          }


          //END

          $scope.enableSave = function (obj, value) {
              if (value == 2) {
                  obj.Reason = '';
              }
              $scope.isDataPresent = true;
          }
          $scope.disableSave = function () {
              $scope.isDataPresent = false;
          }

          //To save, add and update Disclosure Questions.
          $scope.saveQuestions = function (disclosureQuestions, type) {
              loadingOn();
              var validationStatus;
              var url;
              var $formData;

              validationStatus = $('#editShowDisclosureQuestions').find('form').valid();
              //validationStatus = $('#disclosureForm').find('form').valid();
              $formData = $('#editShowDisclosureQuestions').find('form');
              //$formData = $('#disclosureForm').find('form');
              url = rootDir + "/Profile/DisclosureQuestion/UpdateDisclosureQuestionAsync?profileId=" + profileId;

              $scope.typeOfSaveForDisclosureQuestions = type;

              ResetFormForValidation($formData);
              validationStatus = $formData.valid();

              if (validationStatus) {
                  //Simple POST request example (passing data) :
                  $.ajax({
                      url: url,
                      type: 'POST',
                      data: new FormData($formData[0]),
                      //params: { profileId: disclosureQuestions.ProfileDisclosureID, disclosureQuestion: { ProfileDisclosureID: tempObject.ProfileDisclosureID, ProfileDisclosureQuestionAnswers: tempObject.ProfileDisclosureQuestionAnswers } },
                      async: false,
                      cache: false,
                      contentType: false,
                      processData: false,
                      success: function (data) {
                          try {
                              if (data.status == "true") {
                                  $rootScope.MasterProfile.DisclosureQuestion.ProfileDisclosure = data.disclosureQuestion;
                                  console.log('dq');
                                  console.log($rootScope.MasterProfile.DisclosureQuestion.ProfileDisclosure);
                                  $scope.ErrorInDisclosureQuestions = false;
                                  $rootScope.operateCancelControl('');
                                  if ($scope.typeOfSaveForDisclosureQuestions == "Add") {
                                      $rootScope.visibilityControl = "addedNewDisclosureQuestions";
                                      messageAlertEngine.callAlertMessage('addedNewDisclosureQuestions', "Disclosure Questions saved successfully !!!!", "success", true);
                                  } else {
                                      $scope.ProfileDisclosurePendingRequest = true;
                                      $rootScope.visibilityControl = "updatedDisclosureQuestions";
                                      messageAlertEngine.callAlertMessage('updatedDisclosureQuestions', "Disclosure Questions updated successfully !!!!", "success", true);
                                  }
                              } else {
                                  messageAlertEngine.callAlertMessage('errorDisclosureQuestions', "", "danger", true);
                                  $scope.errorDisclosureQuestions = data.status.split(",");
                              }
                          } catch (e) {

                          }
                      },
                      error: function (e) {
                          messageAlertEngine.callAlertMessage('errorDisclosureQuestions', "", "danger", true);
                          $scope.errorDisclosureQuestions = "Sorry for Inconvenience !!!! Please Try Again Later...";
                      }
                  });
              }
              loadingOff();
          }
          $rootScope.DisclosureQuestionLoaded = true;
          $scope.dataLoaded = false;
          
      }]);
});