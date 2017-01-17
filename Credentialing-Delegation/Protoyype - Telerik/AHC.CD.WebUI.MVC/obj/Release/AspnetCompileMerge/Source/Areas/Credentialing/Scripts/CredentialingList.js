
//--------------------- Angular Module ----------------------
var credentialingList = angular.module('CredentialingList', ['InitCredApp', 'ngTable']);

credentialingList.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

    $rootScope.messageDesc = "";
    $rootScope.activeMessageDiv = "";
    $rootScope.messageType = "";

    var animateMessageAlertOff = function () {
        $rootScope.closeAlertMessage();
    };


    this.callAlertMessage = function (calledDiv, msg, msgType, dismissal) { //messageAlertEngine.callAlertMessage('updateHospitalPrivilege' + IndexValue, "Data Updated Successfully !!!!", "success", true);                            
        $rootScope.activeMessageDiv = calledDiv;
        $rootScope.messageDesc = msg;
        $rootScope.messageType = msgType;
        if (dismissal) {
            $timeout(animateMessageAlertOff, 5000);
        }
    }

    $rootScope.closeAlertMessage = function () {
        $rootScope.messageDesc = "";
        $rootScope.activeMessageDiv = "";
        $rootScope.messageType = "";
    }
}]);

//=========================== Controller declaration ==========================
credentialingList.controller('credentialingListController', ['$scope', '$http', '$filter', 'ngTableParams', '$rootScope', 'messageAlertEngine', '$timeout', function ($scope, $http, $filter, ngTableParams, $rootScope, messageAlertEngine, $timeout) {
    $scope.data = [];



    //if (localStorage.getItem("CreListId") == null)
    //{
    localStorage.setItem("CreListId", "1");
    sessionStorage.setItem("CreListId", "1");
    //}



    $scope.groupBySelected = "none";
    $scope.progressbar = false;
    $scope.error_message = "";
    $scope.showInit = false;
    $scope.status = [];

    $scope.isInitiated = false;
    $scope.isVerified = false;
    $scope.isCCMDone = false;
    $scope.isLoaded = false;
    $scope.isreCredentialed = false;
    $scope.isdeCredentialed = false;
    $scope.isCompleted = false;
    $scope.title = "Action";

    $scope.getStatus = function (data, index) {

        $scope.data[index].isStatusDropped = false;

        if (data.CredentialingActivityLogs != null && data.CredentialingActivityLogs.length != 0) {
            if (data.Credentialing == "Credentialing" || data.Credentialing == "ReCredentialing") {
                for (var i = 0; i < data.CredentialingActivityLogs.length; i++) {
                    if (data.CredentialingActivityLogs[i].Activity == "Initiation") {
                        if (data.CredentialingActivityLogs[i].ActivityStatus == "Completed") {

                            //$scope.status.push("Initiated");
                            $scope.data[index].CurrentStatus = "Initiated";
                            $scope.isInitiated = true;
                            $scope.credInitDate = data.CredentialingActivityLogs[i].LastModifiedDate;
                        }

                    } else if (data.CredentialingActivityLogs[i].Activity == "PSV") {

                        if (data.CredentialingActivityLogs[i].ActivityStatus == "Completed") {

                            //$scope.status.push("Verified");
                            $scope.data[index].CurrentStatus = "Verified";
                            $scope.isVerified = true;
                            $scope.psvInitDate = data.CredentialingActivityLogs[i].LastModifiedDate;

                        } else {

                            //$scope.status.push("Initiated");
                            $scope.data[index].CurrentStatus = "Initiated";

                        }

                    } else if (data.CredentialingActivityLogs[i].Activity == "CCMAppointment") {

                        if (data.CredentialingActivityLogs[i].ActivityStatus == "Completed") {

                            //$scope.status.push("CCM");
                            $scope.data[index].CurrentStatus = "CCM";
                            $scope.isCCMDone = true;
                            $scope.ccmInitDate = data.CredentialingActivityLogs[i].LastModifiedDate;

                        } else {

                            //$scope.status.push("Verified");
                            $scope.data[index].CurrentStatus = "Verified";

                        }

                    } else if (data.CredentialingActivityLogs[i].Activity == "Loading") {

                        if (data.CredentialingActivityLogs[i].ActivityStatus == "Completed") {

                            //$scope.status.push("Submitted");
                            $scope.data[index].CurrentStatus = "Submitted";
                            $scope.isLoaded = true;
                            $scope.loadToPlanInitDate = data.CredentialingActivityLogs[i].LastModifiedDate;

                        } else {

                            //$scope.status.push("CCM");    
                            $scope.data[index].CurrentStatus = "CCM";

                        }

                    } else if (data.CredentialingActivityLogs[i].Activity == "Report") {

                        if (data.CredentialingActivityLogs[i].ActivityStatus == "Completed") {

                            //$scope.status.push("Completed");
                            $scope.data[index].CurrentStatus = "Completed";
                            $scope.data[index].isStatusDropped = true;

                        } else {

                            //$scope.status.push("Submitted");
                            $scope.data[index].CurrentStatus = "Submitted";

                        }

                    } else if (data.CredentialingActivityLogs[i].Activity == "Closure") {

                        if (data.CredentialingActivityLogs[i].ActivityStatus == "Completed") {

                            //$scope.status.push("Completed");
                            $scope.data[index].CurrentStatus = "Closed";
                            $scope.data[index].isStatusDropped = true;

                        }

                    }

                }
            }
            else {
                for (var i = 0; i < data.CredentialingActivityLogs.length; i++) {
                    if (data.CredentialingActivityLogs[i].Activity == "Dropped") {
                        if (data.CredentialingActivityLogs[i].ActivityStatus == "Completed") {
                            $scope.data[index].CurrentStatus = "Dropped";
                            $scope.data[index].isStatusDropped = true;
                        }
                    }
                }
            }
        }
        else {
            if (data.Credentialing == "DeCredentialingInitiated") {
                $scope.data[index].CurrentStatus = "Initiated";
            }
            if (data.Credentialing == "DeCredentialing") {
                $scope.data[index].CurrentStatus = "Completed";
            }

        }

    }

    $scope.initWarning = function () {

        $('#WarningModal').modal();

    };

    $scope.tempId = [];
    $scope.tempObj = [];

    $scope.SelectData = function () {

        $scope.title = "";
        $scope.drop = true;
        $scope.disable = true;

    }

    $scope.revert = function () {

        $scope.title = "";
        $scope.drop = false;
        $scope.disable = false;
        $scope.disableMe = false;
        //$scope.Check.isAllChecked = false;
        for (var i = 0; i < $scope.data.length; i++) {

            $scope.data[i].check = false;

        }
        $scope.tempId = [];
        $scope.tempObj = [];

    }

    //$scope.Check = {

    //    isAllChecked: false

    //};

    //$scope.selectAll = function () {

    //    for (var i = 0; i < $scope.data.length; i++) {

    //        if ($scope.Check.isAllChecked == false) {

    //            $scope.data[i].check = false;
    //            $scope.tempId.splice($scope.tempId.indexOf($scope.data[i].CredentialingInfoID), 1);
    //            $scope.tempObj.splice($scope.tempObj.indexOf($scope.data[i]), 1);

    //        } else {

    //            $scope.data[i].check = true;
    //            $scope.tempId.push($scope.data[i].CredentialingInfoID);
    //            $scope.tempObj.push($scope.data[i]);

    //        }

    //    }

    //    if ($scope.Check.isAllChecked == false) {

    //        $scope.disable = true;

    //    } else {

    //        $scope.disable = false;

    //    }

    //}

    $scope.ConvertDateFormat = function (value) {
        var today = new Date(value);
        var dd = today.getDate();
        var mm = today.getMonth() + 1;
        var yyyy = today.getFullYear();
        if (dd < 10) { dd = '0' + dd }
        if (mm < 10) { mm = '0' + mm }
        var today = mm + '-' + dd + '-' + yyyy;
        return today;
    };

    $scope.setCredInfo = function (id) {
        sessionStorage.setItem('credentialingInfoId', id);
        //sessionStorage.setItem('ButtonCLickName', "1");
    };

    $scope.currentTabStatus = function (status) {
        sessionStorage.setItem('tabStatus', status);
        $scope.tb = sessionStorage.getItem('tabStatus');
    }

    $scope.setCreListId = function (id) {
        //$scope.currentTabStatus(stat);
        sessionStorage.setItem('CreListId', id);
        localStorage.setItem("CreListId", id);
        $scope.selectedAction = id;
    }


    $scope.clearAction = function () {
        $scope.SearchProviderPanelToggleDown('SearchProviderPanel');
        $scope.data = "";
        $scope.showInit = false;
    };

    $scope.resetSelection = function (data) {
        var temp = [];
        for (var i in data) {
            temp[i] = false;
        }
        return temp;
    };
    $scope.setActionID = function (id) {

        //$scope.selectedAction = id;
        //messageAlertEngine.setActionID1(id);
        //$rootScope.selectedAction1 = id;

        $scope.revert();
        $scope.selectedAction = id;
        $scope.setCreListId(id);
        //alert($scope.selectedAction);
        sessionStorage.setItem('key', id);

    };
    $scope.setCredData = function () {

        $scope.loadingAjax = true;
        $http.get(rootDir + '/Credentialing/Initiation/GetAllCredentialings').
         success(function (data, status, headers, config) {

             try {
                 $scope.data = data;

                 for (var i = 0; i < $scope.data.length ; i++) {
                     $scope.data[i].InitiationDate = $scope.ConvertDateFormat($scope.data[i].InitiationDate);
                     $scope.data[i].FirstName = $scope.data[i].Profile.PersonalDetail.FirstName;
                     $scope.data[i].LastName = $scope.data[i].Profile.PersonalDetail.LastName;
                     $scope.data[i].PlanName = $scope.data[i].Plan.PlanName;
                     $scope.getStatus($scope.data[i].CredentialingLogs, i);
                     if ($scope.data[i].Status == "Inactive" && $scope.data[i].CurrentStatus != "Dropped") {
                         $scope.data[i].CurrentStatus = "Completed";
                     }
                     $scope.data[i].initType = $scope.data[i].CredentialingLogs[$scope.data[i].CredentialingLogs.length - 1].Credentialing;

                 }
                 $scope.loadingAjax = false;
                 $scope.init_table($scope.data, $scope.selectedAction);
             } catch (e) {

             }
         }).
         error(function (data, status, headers, config) {

         });
    };

    //-------------- selection ---------------


    $scope.SearchProviderPanelToggle = function (divId) {

        $("#" + divId).slideToggle();
    };

    //==========credentialing list start===================
   
    $scope.tabID = 1;
    $scope.getCredList = function (loadingid) {
        $scope.tabID = 1;
        $scope.loadingAjax = true;
        if (loadingid == 1)
            $scope.showLoading = false;
        else {
            $scope.showLoading = true;    
        }
        $http.get(rootDir + '/Credentialing/Initiation/GetAllCredentialings').
         success(function (data, status, headers, config) {
             try {
                 $scope.data = [];
                 $scope.data = data;
                 for (var i = 0; i < $scope.data.length ; i++) {
                     $scope.data[i].InitiationDate = $scope.ConvertDateFormat($scope.data[i].InitiationDate);
                     $scope.data[i].FirstName = $scope.data[i].Profile.PersonalDetail.FirstName;
                     $scope.data[i].LastName = $scope.data[i].Profile.PersonalDetail.LastName;
                     $scope.data[i].PlanName = $scope.data[i].Plan.PlanName;
                     var CredlogData = "";
                     var flag = 0;
                     for (var c = 0; c < $scope.data[i].CredentialingLogs.length; c++) {
                         if ($scope.data[i].CredentialingLogs[c].Credentialing == "Dropped") {
                             CredlogData = $scope.data[i].CredentialingLogs[c];
                             flag = 1;
                             break;
                         }
                     }
                     if (flag == 0) {
                         for (var c = 0; c < $scope.data[i].CredentialingLogs.length; c++) {
                             if ($scope.data[i].CredentialingLogs[c].Credentialing == "Credentialing") {
                                 CredlogData = $scope.data[i].CredentialingLogs[c];
                                 flag = 1;
                                 break;
                             }
                         }
                     }
                     $scope.data[i].initType = CredlogData.Credentialing;
                     $scope.getStatus(CredlogData, i);
                     $scope.data[i].check = false;
                 }
                 $scope.loadingAjax = false;
                
                 $scope.tableParams1 = null;
                 $scope.init_table($scope.data, $scope.selectedAction);
                 $scope.copyData = $scope.data;
                 $scope.showLoading = false;
                 $('#mydiv').show();

             } catch (e) {

             }
         }).
         error(function (data, status, headers, config) {
             $scope.showLoading = false;
         }); 
    }

    $scope.getReCredList = function () {
        $scope.tabID = 2;
        $scope.loadingAjax = true;
        $http.get(rootDir + '/Credentialing/Initiation/GetAllReCredentialings').
          success(function (data, status, headers, config) {
              try {
                  $scope.data = [];
                  $scope.data = data;
                  for (var i = 0; i < $scope.data.length ; i++) {
                      //$scope.data[i].InitiationDate = ($scope.data[i].InitiationDate).getMonth() + 1 + "/" + ($scope.data[i].InitiationDate).getDate() + "/" + ($scope.data[i].InitiationDate).getYear();
                      $scope.data[i].InitiationDate = $scope.ConvertDateFormat($scope.data[i].InitiationDate);
                      //$scope.data[i].InitiationDate =$scope.data[i].InitiationDate.toDateString();
                      $scope.data[i].FirstName = $scope.data[i].Profile.PersonalDetail.FirstName;
                      $scope.data[i].LastName = $scope.data[i].Profile.PersonalDetail.LastName;
                      $scope.data[i].PlanName = $scope.data[i].Plan.PlanName;
                      var CredlogData = "";
                      var flag = 0;
                      for (var c = 0; c < $scope.data[i].CredentialingLogs.length; c++) {
                          if ($scope.data[i].CredentialingLogs[c].Credentialing == "Dropped") {
                              CredlogData = $scope.data[i].CredentialingLogs[c];
                              flag = 1;
                              break;
                          }
                      }
                      if (flag == 0) {
                          for (var c = 0; c < $scope.data[i].CredentialingLogs.length; c++) {
                              if ($scope.data[i].CredentialingLogs[c].Credentialing == "ReCredentialing") {
                                  CredlogData = $scope.data[i].CredentialingLogs[c];
                                  flag = 1;
                                  break;
                              }
                          }
                      }
                      $scope.data[i].initType = CredlogData.Credentialing;
                      $scope.getStatus(CredlogData, i);
                      $scope.data[i].check = false;
                      //$scope.data[i].initType = $scope.data[i].CredentialingLogs[$scope.data[i].CredentialingLogs.length - 1].Credentialing;
                  }

                  $scope.loadingAjax = false;
                  $scope.tableParams2 = null;
                  $scope.init_table($scope.data, $scope.selectedAction);

              } catch (e) {

              }
          }).
          error(function (data, status, headers, config) {

          });
    }

    $scope.checkMe = function (status, obj) {

        //if (status == false) {
        //    $scope.Check.isAllChecked = false;
        //}
        var count = 0;
        var index = 0;

        for (var i = 0; i < $scope.data.length; i++) {

            if ($scope.data[i].CredentialingInfoID == obj.CredentialingInfoID) {

                index = i;

            }

        }

        //var index = $scope.data.indexOf(obj);

        //for (var i = 0; i < $scope.data.length; i++) {

        //    if ($scope.data[i].check == true) {

        //        $scope.disable = false;

        //        count++;

        //    }

        //}

        //if (count == 0) {

        //    $scope.disable = true;

        //}

        //if (count == $scope.data.length) {

        //    $scope.Check.isAllChecked = true;

        //}

        var present = false;

        if ($scope.tempId != null && $scope.tempId.length != 0) {

            for (var i = 0; i < $scope.tempId.length; i++) {

                if ($scope.tempId[i] == $scope.data[index].CredentialingInfoID) {

                    present = true;
                    break;

                }

            }

        }

        if (!present) {

            $scope.tempId.push($scope.data[index].CredentialingInfoID);
            $scope.tempObj.push($scope.data[index]);

        } else {

            $scope.tempId.splice($scope.tempId.indexOf($scope.data[index].CredentialingInfoID), 1);
            $scope.tempObj.splice($scope.tempObj.indexOf($scope.data[index]), 1);

        }

        if ($scope.tempId.length == 0) {

            $scope.disable = true;

        } else {

            $scope.disable = false;

        }

    }
    $scope.maxSize = 5;
    $scope.bigTotalItems = 0;
    $scope.bigCurrentPage = 1;

    $scope.pageChanged = function (pagnumber) {
        $scope.bigCurrentPage = pagnumber;
    };
    $scope.deCredList = [];
    $scope.getDeCredList = function () {
        $scope.tabID = 3;
        $scope.loadingAjax = true;
        $http.get(rootDir + '/Credentialing/Initiation/GetAllDeCredentialings').
         success(function (data, status, headers, config) {
             try {
                 $scope.deCredList = angular.copy(data);
                 $scope.data = data;
                 $scope.Title = "";
                 for (var i = 0; i < $scope.data.length ; i++) {
                     $scope.data[i].InitiationDate = $scope.ConvertDateFormat($scope.data[i].InitiationDate);
                     $scope.data[i].FirstName = $scope.data[i].Profile.PersonalDetail.FirstName;
                     $scope.data[i].LastName = $scope.data[i].Profile.PersonalDetail.LastName;
                     $scope.data[i].PlanName = $scope.data[i].Plan.PlanName;
                     $scope.data[i].LastModifiedDate = $scope.ConvertDateFormat($scope.data[i].LastModifiedDate);
                     $scope.data[i].Title = "";
                     for (var j = 0; j < $scope.data[i].Profile.PersonalDetail.ProviderTitles.length; j++) {
                         if ($scope.data[i].Title == "")
                             $scope.data[i].Title = $scope.data[i].Profile.PersonalDetail.ProviderTitles[j].ProviderType.Title;
                         else
                             $scope.data[i].Title += ", " + $scope.data[i].Profile.PersonalDetail.ProviderTitles[j].ProviderType.Title;

                     }
                     var CredlogData = "";
                     var flag = 0;
                     for (var c = 0; c < $scope.data[i].CredentialingLogs.length; c++) {
                         if ($scope.data[i].CredentialingLogs[c].Credentialing == "DeCredentialing") {
                             CredlogData = $scope.data[i].CredentialingLogs[c];
                             flag = 1;
                             break;
                         }
                     }
                     if (flag == 0) {
                         for (var c = 0; c < $scope.data[i].CredentialingLogs.length; c++) {
                             if ($scope.data[i].CredentialingLogs[c].Credentialing == "DeCredentialingInitiated") {
                                 CredlogData = $scope.data[i].CredentialingLogs[c];
                                 flag = 1;
                                 break;
                             }
                         }
                     }
                     $scope.getStatus(CredlogData, i);
                 }
                 // $scope.fillData(data);
                 $scope.loadingAjax = false;
                 $scope.init_table($scope.data, $scope.selectedAction);
             } catch (e) {

             }
         }).
         error(function (data, status, headers, config) {

         });
    }

    if (sessionStorage.getItem('CreListId') == 1 || sessionStorage.getItem('CreListId') == 2 || sessionStorage.getItem('CreListId') == 3) {
        $scope.selectedAction = sessionStorage.getItem('CreListId');
        if (sessionStorage.getItem('CreListId') == 1) {
            $scope.getCredList();
        }
        else if (sessionStorage.getItem('CreListId') == 2) {
            $scope.getReCredList();
        }
        else if (sessionStorage.getItem('CreListId') == 3) {
            $scope.getDeCredList();
        }
    } else {
        $scope.setActionID(1);
        $scope.getCredList();
    }


    $scope.profileInfo = [];

    $scope.fillData = function (data) {

        $scope.ProfileID = data.ProfileID;

        for (var i = 0; i < $scope.data.length; i++) {

            if ($scope.data[i].CredentialingContractRequests != null && $scope.data[i].CredentialingContractRequests != 0) {

                for (var j = 0; j < $scope.data[i].CredentialingContractRequests.length; j++) {

                    if ($scope.data[i].CredentialingContractRequests[j].ContractGrid != null && $scope.data[i].CredentialingContractRequests[j].ContractGrid != 0) {

                        for (var k = 0; k < $scope.data[i].CredentialingContractRequests[j].ContractGrid.length; k++) {

                            if ($scope.data[i].CredentialingContractRequests[j].ContractGrid[k].Report != null) {

                                $scope.profileInfo.push($scope.data[i].CredentialingContractRequests[j].ContractGrid[k]);
                                $scope.profileInfo[$scope.profileInfo.length - 1].FirstName = $scope.data[i].FirstName;
                                $scope.profileInfo[$scope.profileInfo.length - 1].LastName = $scope.data[i].LastName;
                                $scope.profileInfo[$scope.profileInfo.length - 1].CredentialingInfoID = $scope.data[i].CredentialingInfoID;
                                $scope.profileInfo[$scope.profileInfo.length - 1].CredentialingContractRequestID = $scope.data[i].CredentialingContractRequests[j].CredentialingContractRequestID;
                                $scope.profileInfo[$scope.profileInfo.length - 1].PlanName = $scope.data[i].Plan.PlanName;
                                $scope.profileInfo[$scope.profileInfo.length - 1].Speciality = $scope.data[i].CredentialingContractRequests[j].ContractGrid[k].ProfileSpecialty.Specialty.Name;
                                $scope.profileInfo[$scope.profileInfo.length - 1].LOB = $scope.data[i].CredentialingContractRequests[j].ContractGrid[k].LOB.LOBName;
                                $scope.profileInfo[$scope.profileInfo.length - 1].Location = $scope.data[i].CredentialingContractRequests[j].ContractGrid[k].ProfilePracticeLocation.Facility.FacilityName;
                                $scope.profileInfo[$scope.profileInfo.length - 1].GroupName = $scope.data[i].CredentialingContractRequests[j].ContractGrid[k].BusinessEntity.GroupName;
                                $scope.profileInfo[$scope.profileInfo.length - 1].Check = false;
                                $scope.profileInfo[$scope.profileInfo.length - 1].InitiationDate = $scope.ConvertDateFormat($scope.data[i].InitiationDate);

                            }

                        }

                    }

                }

            }

            //$scope.profileDetail.push($scope.data[i]);

        }

        $scope.data = $scope.profileInfo;
        $scope.profileInfo = [];

    }

    //==========credential list end========================


    //==============Plan Report===========================
    $scope.planReportList = [];
    $scope.planReportObj = [];
    $scope.getPlanReport = function (obj) {

        //edited by pritam
        $scope.credentialingInfo = obj;
        $scope.FullName = obj.Profile.PersonalDetail.FirstName + ' ' + obj.Profile.PersonalDetail.LastName;
        $scope.planName = obj.Plan.PlanName;

        for (var i = 0; i < obj.CredentialingContractRequests.length; i++) {
            for (var j = 0; j < obj.CredentialingContractRequests[i].ContractGrid.length; j++) {
                if (obj.CredentialingContractRequests[i].ContractGrid[j].Status == 'Inactive') {
                    $scope.planReportList.push(obj.CredentialingContractRequests[i].ContractGrid[j]);
                }
            }
        }

        for (var i = 0; i < obj.CredentialingLogs.length; i++) {
            $scope.getStatus(obj.CredentialingLogs[i], i);
        }

    }

    $scope.IsMessage = false;

    $scope.initiateDrop = function () {

        var idArray = $scope.tempId;

        $http({
            method: "POST",
            url: rootDir + "/Credentialing/Initiation/InitiateDrop",
            data: {
                InfoidArray: idArray,
            }
        }).success(function (data, status, headers, config) {
            try {
                //----------- success message -----------
                if (data.status == "true") {

                    for (var i = 0; i < $scope.tempObj.length; i++) {

                        //$scope.data.splice($scope.data.indexOf($scope.tempObj[i]), 1);
                        for (var j = 0; j < $scope.data.length; j++) {

                            if ($scope.data[j].CredentialingInfoID == $scope.tempObj[i].CredentialingInfoID) {

                                $scope.data[j].CurrentStatus = "Dropped";
                                $scope.data[j].isStatusDropped = true;

                            }

                        }

                    }

                    if ($scope.selectedAction == 1) {

                        $scope.tableParams1.reload();

                    }

                    if ($scope.selectedAction == 2) {

                        $scope.tableParams2.reload();

                    }

                    $('#WarningModal').modal('hide');

                    $scope.SuccessMessage = "Provider Dropped Successfully";

                    $scope.revert();

                    $scope.IsMessage = true;

                    $timeout(function () {
                        $scope.IsMessage = false;
                    }, 2000);

                    messageAlertEngine.callAlertMessage('successfulInitiated', "Drop Initiated Successfully. !!!!", "success", true);
                }
                else {
                    messageAlertEngine.callAlertMessage('errorInitiated', "", "danger", true);
                    $scope.errorInitiated = data.status.split(",");
                }
            } catch (e) {

            }
        }).error(function (data, status, headers, config) {
            //----------- error message -----------
            messageAlertEngine.callAlertMessage('errorInitiated', "", "danger", true);
            $scope.errorInitiated = "Sorry for Inconvenience !!!! Please Try Again Later...";
        });

    }

    //Created function to be called when data loaded dynamically
    $scope.currentPage = 0;
    $scope.currentCount = 0;
    $scope.params = null;
    $scope.init_table = function (data, condition) {

        $scope.data = data;
        var counts = [];

        if ($scope.data.length <= 10) {
            counts = [];
        }
        else if ($scope.data.length <= 25) {
            counts = [10, 25];
        }
        else if ($scope.data.length <= 50) {
            counts = [10, 25, 50];
        }
        else if ($scope.data.length <= 100) {
            counts = [10, 25, 50, 100];
        }
        else if ($scope.data.length > 100) {
            counts = [10, 25, 50, 100];
        }

        if (condition == 1) {
            $scope.tableParams1 = new ngTableParams({
                page: 1,            // show first page
                count: 10,          // count per page
                filter: {
                    //name: 'M'       // initial filter
                    //FirstName : ''
                },
                sorting: {

                    //InitiationDate: 'desc'
                    //name: 'asc'     // initial sorting
                }
            }, {
                counts: counts,
                total: $scope.data.length, // length of data
                getData: function ($defer, params) {
                    // use build-in angular filter
                    var filteredData = params.filter() ?
                            $filter('filter')($scope.data, params.filter()) :
                            $scope.data;
                    var orderedData = params.sorting() ?
                            $filter('orderBy')(filteredData, params.orderBy()) :
                            $scope.data;

                    params.total(orderedData.length); // set total for recalc pagination
                    $scope.currentPage = params.page();
                    $scope.currentCount = params.count();
                    $scope.params = params;
                    $defer.resolve(orderedData);
                }
            });
        }
        else if (condition == 2) {
            $scope.tableParams2 = new ngTableParams({
                page: 1,            // show first page
                count: 10,          // count per page
                filter: {
                    //name: 'M'       // initial filter
                    //FirstName : ''
                },
                sorting: {
                    InitiationDate: 'desc'
                    //name: 'asc'     // initial sorting
                }
            }, {
                counts: counts,
                total: $scope.data.length, // length of data
                getData: function ($defer, params) {
                    // use build-in angular filter
                    var filteredData = params.filter() ?
                            $filter('filter')($scope.data, params.filter()) :
                            $scope.data;
                    var orderedData = params.sorting() ?
                            $filter('orderBy')(filteredData, params.orderBy()) :
                            $scope.data;

                    params.total(orderedData.length); // set total for recalc pagination
                    $scope.currentPage = params.page();
                    $scope.currentCount = params.count();
                    $scope.params = params;
                    $defer.resolve(orderedData);
                }
            });
        }
        else if (condition == 3) {
            if ($scope.tableParams3 == null) {
                $scope.tableParams3 = new ngTableParams({
                    page: 1,            // show first page
                    count: 10,          // count per page
                    filter: {
                        //name: 'M'       // initial filter
                        //FirstName : ''
                    },
                    sorting: {
                        LastModifiedDate: 'desc'
                        //name: 'asc'     // initial sorting
                    }
                }, {
                    counts: counts,
                    total: $scope.data.length, // length of data
                    getData: function ($defer, params) {
                        // use build-in angular filter
                        var filteredData = params.filter() ?
                                $filter('filter')($scope.data, params.filter()) :
                                $scope.data;
                        var orderedData = params.sorting() ?
                                $filter('orderBy')(filteredData, params.orderBy()) :
                                $scope.data;

                        params.total(orderedData.length); // set total for recalc pagination
                        $scope.currentPage = params.page();
                        $scope.currentCount = params.count();
                        $scope.params = params;
                        $defer.resolve(orderedData);
                    }
                });
            } else {
                $scope.tableParams3.reload();
            }
        }
    };
    $scope.IsValidIndex = function (index) {

        if (index >= (($scope.currentPage - 1) * $scope.currentCount) && index < ($scope.currentPage * $scope.currentCount))
            return true;
        else
            return false;
    }

    $scope.filterData = function () {
        $scope.params.page(1);
    }

    //$scope.isFilter = false;

    //$scope.filterData = function (searchBy, searchFor) {

    //    $scope.isFilter = true;
    //    var j = 0;
    //    $scope.filteredData = [];

    //    if ($scope.copyData.length >= 0) {
    //        if (searchBy != "") {
    //            if (searchFor == "FirstName") {
    //                if ($scope.filteredData.length == 0) {
    //                    for (var i = 0; i < $scope.copyData.length; i++) {
    //                        if ($scope.copyData[i].FirstName.toLowerCase().indexOf(searchBy.toLowerCase()) >= 0) {
    //                            $scope.filteredData.push($scope.copyData[i]);
    //                        }
    //                    }
    //                }
    //                else {
    //                    for (var i = 0; i < $scope.filteredData.length; i++) {
    //                        if (!($scope.filteredData[i].FirstName.toLowerCase().indexOf(searchBy.toLowerCase()) >= 0)) {
    //                            $scope.filteredData.pop($scope.filteredData[i]);
    //                        }
    //                    }
    //                }
    //            }
    //            else if (searchFor == "LastName") {
    //                if ($scope.filteredData.length == 0) {
    //                    for (var i = 0; i < $scope.copyData.length; i++) {
    //                        if ($scope.copyData[i].LastName.toLowerCase().indexOf(searchBy.toLowerCase()) >= 0) {
    //                            $scope.filteredData.push($scope.copyData[i]);
    //                        }
    //                    }
    //                }       
    //                else {
    //                    for (var i = 0; i < $scope.filteredData.length; i++) {
    //                        if (!($scope.filteredData[i].LastName.toLowerCase().indexOf(searchBy.toLowerCase()) >= 0)) {
    //                            $scope.filteredData.pop($scope.filteredData[i]);
    //                        }
    //                    }
    //                }
    //            }


    //            $scope.data = $scope.filteredData;

    //        } else {
    //            $scope.data = $scope.copyData;
    //            $scope.filteredData = [];
    //        }

    //        $scope.tableParams1.reload();

    //    }
    //}

    //if filter is on
    $scope.ifFilter = function () {
        try {
            var bar;
            var obj;
            if ($scope.selectedAction == 1) {
                obj = $scope.tableParams1.$params.filter;
            }
            else if ($scope.selectedAction == 2 || $scope.selectedAction == 3) {
                obj = $scope.tableParams2.$params.filter;
            }
            for (bar in obj) {
                if (obj[bar] != "") {
                    return false;
                }
            }
            return true;
        }
        catch (e) { return true; }

    }

    //Get index first in table
    $scope.getIndexFirst = function () {
        try {
            if ($scope.groupBySelected == 'none') {
                if ($scope.selectedAction == 1) {
                    return ($scope.tableParams1.$params.page * $scope.tableParams1.$params.count) - ($scope.tableParams1.$params.count - 1);
                }
                else if ($scope.selectedAction == 2) {
                    return ($scope.tableParams2.$params.page * $scope.tableParams2.$params.count) - ($scope.tableParams2.$params.count - 1);
                }
                else if ($scope.selectedAction == 3) {
                    return ($scope.tableParams3.$params.page * $scope.tableParams3.$params.count) - ($scope.tableParams3.$params.count - 1);
                }
            }
        }
        catch (e) { }
    }
    //Get index Last in table
    $scope.getIndexLast = function () {
        try {
            if ($scope.groupBySelected == 'none') {
                if ($scope.selectedAction == 1) {
                    return { true: ($scope.data.length), false: ($scope.tableParams1.$params.page * $scope.tableParams1.$params.count) }[(($scope.tableParams1.$params.page * $scope.tableParams1.$params.count) > ($scope.data.length))];
                }
                else if ($scope.selectedAction == 2) {
                    return { true: ($scope.data.length), false: ($scope.tableParams2.$params.page * $scope.tableParams2.$params.count) }[(($scope.tableParams2.$params.page * $scope.tableParams2.$params.count) > ($scope.data.length))];
                }
                else if ($scope.selectedAction == 3) {
                    return { true: ($scope.data.length), false: ($scope.tableParams3.$params.page * $scope.tableParams3.$params.count) }[(($scope.tableParams3.$params.page * $scope.tableParams3.$params.count) > ($scope.data.length))];
                }
            }
        }
        catch (e) { }
    }

    $scope.SearchProviderPanelToggleDown = function (divId) {
        $(".closePanel").slideUp();
        $("#" + divId).slideToggle();
    };

    $scope.clearSearch = function () {
        $scope.tempObject = "";
        $scope.data = "";
        $scope.allProviders = null;
        //$('a[href=#SearchResult]').trigger('click');
        // $scope.Npi = null;
    }
    $scope.ClearInit = function () {
        $scope.showInit = false;

    }
    $scope.msgAlert = false;

    $scope.title = "Action";

    //================================= Hide All country code popover =========================
    $(document).click(function (event) {
        if (!$(event.target).hasClass("form-control") && $(event.target).parents(".ProviderTypeSelectAutoList").length === 0) {
            $(".ProviderTypeSelectAutoList").hide();
        }
        if (!$(event.target).attr("data-searchdropdown") && $(event.target).parents(".ProviderTypeSelectAutoList1").length === 0) {
            $(".ProviderTypeSelectAutoList1").hide();
        }
    });

    $(document).ready(function () {
        $(".ProviderTypeSelectAutoList").hide();
        $(".ProviderTypeSelectAutoList1").hide();
        $scope.SearchProviderPanelToggleDown('SearchProviderPanel');

    });

    function showLocationList(ele) {
        $(ele).parent().find(".ProviderTypeSelectAutoList").first().show();
    }

}]);