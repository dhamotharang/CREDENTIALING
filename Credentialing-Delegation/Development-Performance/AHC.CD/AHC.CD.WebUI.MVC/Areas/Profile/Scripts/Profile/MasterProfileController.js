//--------------------- Author: KRGLV --------------------------------
//--------------- User Use Strict Section ---------------

define(['Util/MasterProfileApp'], function (app) {
    'use strict';
    app.register.controller('MasterProfileController', ['$scope', '$rootScope', '$http', '$state', '$stateParams', 'messageAlertEngine', '$timeout',
      function ($scope, $rootScope, $http, $state, $stateParams, messageAlertEngine, $timeout) {
          $rootScope.ccoprofile = false;
          $http.get(rootDir + '/Profile/CustomFieldGeneration/getRole').
              success(function (data, status, headers, config) {
                  $rootScope.ccoprofile = data.role;
              }).
              error(function (data, status, headers, config) {

              });
          $scope.getTabData = function (tabName, state) {
              //----------------------- State Go Action Render Page ----------------
              $state.go(state);
              //if (tabName != "Demographics") {
              //    //$timeout(function () {
              //    //    //$rootScope.$broadcast(tabName);
              //    //},500);
              //}
              //if (tabName == 'DocumentationCheckList') {
              //    $("#tabname").text("Document CheckList");
              //    $('#tabDocumentationChecklist').css({ 'pointer-events': 'none' });
              //}
              //else { $('#tabDocumentationChecklist').css({ 'pointer-events': 'auto' }); }
              //if (tabName == 'ContractGrid') {
              //    $('#tabContractGrid').css({ 'pointer-events': 'none' });
              //} else { $('#tabContractGrid').css({ 'pointer-events': 'auto' }); }
          }

          $scope.dontReloadTab = false;
          //$rootScope.loadAllData = function () {
          //    if ($scope.dontReloadTab == false) {
          //        $rootScope.$broadcast("IdentificationLicenses");
          //        $rootScope.$broadcast("EducationHistory");
          //        $rootScope.$broadcast("Specialty");
          //        $rootScope.$broadcast("HospitalPrivilege");
          //        $rootScope.$broadcast("ProfessionalLiability");
          //        $rootScope.$broadcast("ProfessionalReference");
          //        $rootScope.$broadcast("ProfessionalAffiliation");
          //        $rootScope.$broadcast("DisclosureQuestion");
          //        $rootScope.$broadcast("ContractInformation");
          //        $rootScope.$broadcast("WorkHistory");
          //        $rootScope.$broadcast("PracticeLocation");
          //        if ($rootScope.ccoprofile == true) {
          //            $rootScope.$broadcast("CustomField");
          //            $rootScope.$broadcast("DocumentationCheckList");
          //        }
          //        $rootScope.$broadcast("ContractGrid");
          //        $rootScope.$broadcast("Tasks");
          //        $scope.dontReloadTab = true;
          //    };

          //};

          $rootScope.$on('PersonalDetail', function (event, val) {
              $scope.Provider = val;
          });

          $rootScope.getSpecilityForThisUser = function () {
              //return $scope.Specialties;
              return null;
          };

          $rootScope.changeTimeAmPm = function (value) {
              if (value == 'Not Available' || value == 'Invalid Date' || value == 'Day Off') { return 'Day Off'; }
              if (!value) { return ''; }
              if (angular.isDate(value)) {
                  value = value.getHours() + ":" + value.getMinutes();
              }

              var time = value.split(":");
              var hours = time[0];
              var minutes = time[1];
              var ampm = hours >= 12 ? 'PM' : 'AM';
              hours = hours % 12;
              hours = hours ? hours : 12; // the hour '0' should be '12'

              minutes = minutes.length == 1 ? minutes < 10 ? '0' + minutes : minutes : minutes;

              //minutes = minutes < 9 ? '00' : minutes;
              var strTime = hours + ':' + minutes + ' ' + ampm;
              return strTime;
          }

          $scope.PrintPDF = function () {
              $rootScope.generatePDF = true;
              try {
                  var url = rootDir + "/PDFProfileDataGenerator/GetProfileData?profileId=" + profileId;
                  $http.get(url)     //locationService makes an ajax call to fetch all the cities which have relevent names as the query string.
             .then(function (response) {
                 var newPage = rootDir + "/Document/View?path=/ApplicationDocument/GeneratedPdf/" + response.data.path;
                 if (response.data.status == 'true') {
                     var win = window.open(newPage, '_blank');
                     win.focus();
                 }
                 else {
                     messageAlertEngine.callAlertMessage("showErrorPDF", "Sorry for the Inconvenience, PDF cannot be generated. Please try again later !!", "danger", true);
                     //  $("#showErrorPDF").show();
                     //$scope.showErrorPDF = true;
                 }
                 $rootScope.generatePDF = false;
                 //  $scope.States = response.data;
             });
              }
              catch (e) {
                  $rootScope.generatePDF = false;
                  messageAlertEngine.callAlertMessage("showErrorPDF", "Sorry for the Inconvenience, PDF cannot be generated. Please try again later !!", "danger", true);

              };
          };
      }]);
});