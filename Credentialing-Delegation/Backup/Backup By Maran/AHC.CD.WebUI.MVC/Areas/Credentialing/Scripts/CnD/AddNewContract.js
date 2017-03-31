/// <reference path="Contractinfo.js" />

ContractInfo.directive('searchdropdown', function () {
    return {
        restrict: 'AE',
        link: function (scope, element, attr) {
            element.bind('focus', function () {
                element.parent().find(".TemplateSelectAutoList").show();
            });
        }
    };
});
ContractInfo.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

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

    //$rootScope.selectedAction1 = 1;
    this.setActionID1 = function (id) {

        sessionStorage.setItem('key', id);
    };

}])

ContractInfo.controller('singleProviderCtrl', function ($scope, $http, $q, $timeout, $location, $filter, ngTableParams, $rootScope, messageAlertEngine) {
    $scope.ErrMsg = 'Please Wait while Loading Plan Enrollment';
    $scope.firstPalnLob = false;
    localStorage.setItem("CreListId", "1");
    sessionStorage.setItem("CreListId", "1");
    $scope.data = []; //data in scope is declared
    $scope.progressbar = false;
    $scope.error_message = "";
    $scope.groupBySelected = "none";
    $scope.showInit = false;
    $scope.result = false;
    $scope.isSelected = true;
    $scope.isFound = true;
    $scope.fileset = false;
    $scope.decredButton = false;
    var availableTags = [];
    $scope.UploadingDocument = '';
    $scope.reasonforDecred = '';
    $scope.tempFileList = [];
    $scope.tempObject = [];
    $scope.PlanReportList = [];
    $scope.ShowVisibility = '';
    $scope.clearAction = function () {
        $scope.SearchProviderPanelToggleDown('SearchProviderPanel');
        $scope.data = "";
        $scope.showInit = false;
        $scope.initiate = false;

    };
    if (localStorage.getItem('NavigateToInitiateRecredential') == 'true') {
        $scope.NavigateToInitiateRecredential = true;
    };

    //$scope.searchCumDropDown = function (divId) {
    //    $(".TemplateSelectAutoList").hide();
    //    $("#" + divId).show();
    //};
    $scope.FlipingTabs = function (i, j) {
        var id_show = '#' + i;
        var id_hide = '#' + j;
        $(id_show).show();
        $(id_hide).hide();
    };


    $scope.resetSelection = function (data) {
        var temp = [];
        for (var i in data) {
            temp[i] = false;
        }
        return temp;
    };

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

    $scope.ChangeFormat = function (data) {
        var temp = {
            Profile: {
                OtherIdentificationNumber: {
                    NPINumber: data.NPINumber,

                },
                PersonalDetail: {
                    FirstName: data.FirstName,
                    LastName: data.LastName,
                    ProviderTitles: data.ProviderTitles,
                },
                SpecialtyDetails: {},
                ContractInfoes: {},
            },

        };
        return temp;
    };

    $scope.selectedAction = 1;
    $scope.setActionID2 = function (id) {
        $scope.selectedAction = id;


    };
    //alert(sessionStorage.getItem('key'));

    if (sessionStorage.getItem('key') == 1 || sessionStorage.getItem('key') == 2 || sessionStorage.getItem('key') == 3) {
        $scope.selectedAction = sessionStorage.getItem('key');
        $scope.setActionID2(sessionStorage.getItem('key'));
    } else {
        $scope.setActionID2(1);
    }



    $scope.setActionID = function (id) {
        //$scope.selectedAction = angular.copy(id);
        $rootScope.selectedAction1 = id;
        $scope.selectedAction = $rootScope.selectedAction1;

        //$scope.SearchProviderPanelToggleDown('SearchProviderPanel');
        //$scope.data = "";
        //    $scope.new_search(id);
        //    $scope.SelectedDetails = $scope.resetSelection($scope.CreadentialingData);
        //    $scope.DoneCreadentialing = [];

        //  $scope.dt = new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate())
    };

    $scope.squash = function (arr) {
        var tmp = [];
        for (var i = 0; i < arr.length; i++) {
            if (tmp.indexOf(arr[i]) == -1) {
                tmp.push(arr[i]);
            }
        }
        return tmp;
    }

    $scope.setCredInfo = function (id) {

        sessionStorage.setItem('credentialingInfoId', id);

    };

    //-------------- selection ---------------



    $scope.SearchProviderPanelToggle = function (divId) {

        $("#" + divId).slideToggle();
        // $scope.initiate = false;

    };

    $http.get(rootDir + '/Profile/MasterData/GetAllProviderTypes').
      success(function (data, status, headers, config) {

          try {
              $scope.masterProviderTypes = data;

          } catch (e) {

          }
      }).
      error(function (data, status, headers, config) {

      });



    $http.get(rootDir + '/Profile/MasterData/GetAllSpecialities').
     success(function (data, status, headers, config) {

         try {
             $scope.masterSpecialities = data;
         } catch (e) {

         }
     }).
     error(function (data, status, headers, config) {

     });


    $scope.GetAllEmailIds = function () {
        var d1 = new $.Deferred();
        $http.get(rootDir + '/EmailService/GetAllEmailIds').
          success(function (data, status, headers, config) {
              try {
                  $scope.EmailsIds = angular.copy(data);
                  //$scope.MailIdsforGroup = angular.copy(data);
                  d1.resolve(data);
              } catch (e) {
              }
          }).
          error(function (data, status, headers, config) {
              return d1.promise();
          });
        return d1.promise();
    }

    $scope.GetAllGroupMails = function () {
        var d2 = new $.Deferred();
        $http.get(rootDir + '/EmailService/GetAllGroupMailNames').
          success(function (data, status, headers, config) {
              try {
                  $scope.groupNames = data;
                  d2.resolve(data);
              } catch (e) {
                  throw e;
              }
          }).
          error(function (data, status, headers, config) {
              return d2.promise();
          });
        return d2.promise();
    }

    var promise = $scope.GetAllEmailIds().then(function () {
        $scope.GetAllGroupMails().then(function () {
            for (i = 0; i < $scope.groupNames.length; i++) {
                $scope.EmailsIds.push($scope.groupNames[i]);
            }
        });
    });


    $scope.searchCumDropDown = function (divId) {
        $(".TemplateSelectAutoList").hide();
        $("#" + divId).show();
    };

    //Bind the IPA name with model class to achieve search cum drop down
    $scope.addIntoIPADropDown = function (ipa, div) {

        // $scope.tempObject.IPAGroupNameDup = ipa;
        $scope.tempObject.IPAGroupName = ipa;

        $("#" + div).hide();
    }

    //Bind the IPA name with model class to achieve search cum drop down
    $scope.addIntoSpecialtyDropDown = function (s, div) {

        //$scope.tempObject.SpecialtyDup = s;
        $scope.tempObject.Specialty = s;

        $("#" + div).hide();
    }

    //Bind the IPA name with model class to achieve search cum drop down
    $scope.addIntoTypeDropDown = function (type, div) {
        $scope.tempObject.ProviderType = type.Title;
        //$scope.tempObject.ProviderTypeDup = type.Title;
        $("#" + div).hide();
    }

    $scope.addIntoPlanDropDown = function (type, tempObject, div) {
        $scope.ShowPlanName = type.PlanName
        tempObject.PlanId = type.PlanID;
        tempObject.PlanName = type.PlanName;
        $scope.showbutton = true;
        //$scope.tempObject.ProviderTypeDup = type.Title;
        $("#" + div).hide();
    }

    $scope.$watch('tempObject.PlanName', function (newv, oldv) {
        if (newv == oldv) return;
        if (newv == "") {
            $scope.tempObject.PlanId = '';
        }
        else {
            var count = 0;
            for (var i in $scope.plans) {
                if ($scope.plans[i].PlanName == newv) {
                    count++;
                }
            }
            if (count == 0) {
                $scope.tempObject.PlanId = '';
            }
        }
    });

    //============================= Data From Master Data Table  ======================    
    //----------------------------- Get List Of Groups --------------------------    

    $http.get(rootDir + '/MasterData/Organization/GetGroups').
      success(function (data, status, headers, config) {
          try {
              $scope.PracticingGroups = data;
          } catch (e) {

          }

      }).
      error(function (data, status, headers, config) {

      });

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
            if ($scope.tableParams1 != null) {
                $scope.tableParams1.reload();
            }
            else {
                $scope.tableParams1 = new ngTableParams({
                    page: 1,            // show first page
                    count: 10,          // count per page
                    filter: {
                        //name: 'M'       // initial filter
                        //FirstName : ''
                    },
                    sorting: {
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


    //select all in De-cred


    $scope.tempo = {};
    $scope.tempo.allItemsSelectedForDecred = false;
    $scope.selectionForDecred = [];
    $scope.toggleSelectionForDecred = function (CredentialingContractGrid) {
        var idxForDecred = $scope.selectionForDecred.indexOf(CredentialingContractGrid);
        if (idxForDecred > -1) {
            $scope.selectionForDecred.splice(idxForDecred, 1);
        }
        else {
            $scope.selectionForDecred.push(CredentialingContractGrid);
        }
    }
    $scope.selectAllForDecred = function () {
        if ($scope.tempo.allItemsSelectedForDecred == true) {
            $scope.selectionForDecred = [];
            for (var i = 0; i < $scope.data.length; i++) {
                $scope.selectionForDecred.push($scope.data[i]);
                $scope.data[i].Check = $scope.tempo.allItemsSelectedForDecred;
            }
        }
        else {
            $scope.selectionForDecred = [];
            for (var i = 0; i < $scope.data.length; i++) {
                $scope.data[i].Check = $scope.tempo.allItemsSelectedForDecred;
            }
        }
        $scope.tableParams4.reload();
    }






    $scope.SelectAll = function (mastercheck, condition) {
        if (condition == 4) {
            if (mastercheck == true) {
                $scope.isSelected = false;
            }
            else {
                $scope.isSelected = true;
            }
            for (var i = 0; i < $scope.data.length; i++) {
                $scope.data[i].Check = mastercheck;
            }
        }
    }

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
                else if ($scope.selectedAction == 2 || $scope.selectedAction == 3) {
                    return ($scope.tableParams2.$params.page * $scope.tableParams2.$params.count) - ($scope.tableParams2.$params.count - 1);
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
                else if ($scope.selectedAction == 2 || $scope.selectedAction == 3) {
                    return { true: ($scope.data.length), false: ($scope.tableParams2.$params.page * $scope.tableParams2.$params.count) }[(($scope.tableParams2.$params.page * $scope.tableParams2.$params.count) > ($scope.data.length))];
                }
            }
        }
        catch (e) { }
    }

    //Get data on basis of the parameters ajax call
    $scope.new_search = function (id) {

        $scope.isFound = true;
        $scope.data = [];
        $scope.error_message = "";
        $scope.loadingAjax = true;

        $scope.localdata = JSON.parse(localStorage.getItem('dataToStore'));
        if (localStorage.getItem('NavigateToInitiateRecredential') == 'true') {
            $scope.tempObject.NPINumber = $scope.localdata.NPINumber;
            $scope.tempObject.FirstName = $scope.localdata.FirstName;
            $scope.tempObject.LastName = $scope.localdata.LastName;
        }
        $http({
            method: "POST",
            url: rootDir + "/Credentialing/Initiation/SearchProviderJson?id=" + id,

            data: {
                NPINumber: $scope.tempObject.NPINumber,
                FirstName: $scope.tempObject.FirstName,
                LastName: $scope.tempObject.LastName,
                CAQH: $scope.tempObject.CAQH,
                IPAGroupName: $scope.tempObject.IPAGroupName,
                Specialty: $scope.tempObject.Specialty,
                ProviderType: $scope.tempObject.ProviderType
            }

        }).success(function (resultData) {
            try {
                if (resultData != null) {
                    $scope.SearchProviderPanelToggleDown('SearchProviderResultPanel');
                    $scope.result = true;

                    $scope.data = resultData;
                    $scope.Req_Data = resultData;
                    $scope.credInfo = resultData;
                    $scope.sortedData = [];
                    $scope.sortData(resultData);

                    if ($scope.selectedAction == 1) {

                        for (var i = 0; i < resultData.length; i++) {
                            resultData[i].Name = resultData[i].FirstName + ' ' + resultData[i].LastName;

                            //-----------------title------------------------
                            if (resultData[i].Titles.length > 0) {
                                resultData[i].Title = resultData[i].Titles[0];
                                for (var j = 1; j < resultData[i].Titles.length; j++) {
                                    resultData[i].Title = resultData[i].Title + ', ' + resultData[i].Titles[j];
                                }
                            } else {

                                resultData[i].Title = ' ';
                            }

                            //-----------------specilities------------------------
                            if (resultData[i].Specialties.length > 0) {
                                resultData[i].Speciality = resultData[i].Specialties[0];
                                for (var j = 1; j < resultData[i].Specialties.length; j++) {
                                    resultData[i].Speciality = resultData[i].Speciality + ', ' + resultData[i].Specialties[j];
                                }
                            } else {

                                resultData[i].Speciality = ' ';
                            }


                            //-----------------IPA------------------------

                            if (resultData[i].IPAGroupNames.length > 0) {
                                resultData[i].Groups = resultData[i].IPAGroupNames[0];
                                for (var j = 1; j < resultData[i].IPAGroupNames.length; j++) {
                                    resultData[i].Groups = resultData[i].Groups + ', ' + resultData[i].IPAGroupNames[j];
                                }
                            } else {
                                resultData[i].Groups = ' ';
                            }

                        }



                        $scope.init_table(resultData, $scope.selectedAction);

                    }


                    $scope.tempObject = "";
                    $scope.loadingAjax = false;
                    //   $('#credsearch, #recredsearch, #decredsearch').removeClass("inactive");
                }
                else {
                    $scope.loadingAjax = false;
                    //    $('#credsearch, #recredsearch, #decredsearch').removeClass("inactive");
                    //if (resultData.searchResults.length == 0)
                    //{
                    //    messageAlertEngine.callAlertMessage('noProviderDetails', "Provider is already DeCredentialed or does not exit", "danger", true);
                    //}
                    //else 
                    if (id == 1) {
                        messageAlertEngine.callAlertMessage('noProviderDetails', "No Record Available for the Given Option", "danger", true);
                    }
                    else if (id == 2 || id == 3) {

                        messageAlertEngine.callAlertMessage('noProviderDetails', "Credentialing not yet initiated for Provider", "danger", true);
                    }
                    $scope.data = "";
                }
            } catch (e) {

            }
        }).error(function (resultData) { $scope.loadingAjax = false; $scope.error_message = "An Error occured !! Please Try Again !!"; })
        //$('#credsearch', '#recredsearch', '#decredsearch').removeClass("inactive");
    }

    $scope.sortedData = [];
    $scope.profileIdArray = [];

    $scope.sortData = function (data) {

        var count = 0;
        var report = 0;

        for (var i = 0; i < data.length; i++) {

            if (data[i].CredentialingContractRequests != null && data[i].CredentialingContractRequests.length != 0) {

                for (var j = 0; j < $scope.data[i].CredentialingContractRequests.length; j++) {

                    report = 0;

                    if (data[i].CredentialingContractRequests[j].ContractGrid != null && data[i].CredentialingContractRequests[j].ContractGrid.length != 0) {

                        for (var k = 0; k < $scope.data[i].CredentialingContractRequests[j].ContractGrid.length; k++) {

                            if ($scope.data[i].CredentialingContractRequests[j].ContractGrid[k].Report != null) {

                                report++;

                            }

                        }

                        if ($scope.data[i].CredentialingContractRequests[j].ContractGrid.length != report && $scope.sortedData != null && $scope.sortedData.length == 0) {

                            $scope.sortedData.push(data[i]);

                        }

                    }

                }

            } else {

                count = 1;

            }

            //count = 0;

            for (var j = 0; j < $scope.sortedData.length; j++) {

                //for (var k = 0; k < $scope.profileIdArray.length; k++) {

                //    if (data[i].ProfileID == $scope.profileIdArray[k]) {

                //        count++;

                //    }

                //}

                if (data[i].ProfileID == $scope.sortedData[j].ProfileID) {

                    count++;

                }

            }

            if (count == 0) {

                $scope.sortedData.push(data[i]);

            }

            count = 0;

        }


    }

    $scope.SearchProviderPanelToggleDown = function (divId) {
        $scope.tempObject = {};
        $(".closePanel").slideUp();
        $("#" + divId).slideToggle();
        $scope.fileset = false;
        $scope.fileList = [];
        $scope.reasonforDecred = '';
        //$scope.resetForm();
        //window.MyDocument.reset();
        //FormReset($form);
    };




    $scope.temploadingvariable = false;
    $scope.SelectProviderForCredentialingInitiation = function (provObj) {
        $scope.showInit = true;
        $scope.temploadingvariable = true;
        $scope.SearchProviderPanelToggleDown('InitiationPanel');
        $scope.initiateSuccess = false;
        $scope.ProfileID = provObj.ProfileID;
        $scope.NPINumber = provObj.NPINumber;
        $scope.CAQH = provObj.CAQH;
        $scope.Firstname = provObj.FirstName;
        $scope.Lastname = provObj.LastName;
        $scope.Type = provObj.Titles;
        $scope.Specilities_New = provObj.Specialties;
        $scope.Groups = provObj.IPAGroupNames;


        $http.get(rootDir + '/MasterDataNew/GetAllPlans').
                success(function (data, status, headers, config) {
                    var temp = [];
                    try {
                        for (var i in data) {
                            if (data[i].IsDelegated == "2") {
                                temp.push(data[i]);
                            }
                        }
                        $scope.plans = angular.copy(temp);
                        $scope.temploadingvariable = false;
                        $('#loadingid').remove();
                    } catch (e) {

                    }

                }).
                error(function (data, status, headers, config) {
                });
    };
    $scope.findIsDelegated = function (id) {
        for (i = 0; i < $scope.plans.length; i++) {
            if ($scope.plans[i].PlanID == id) {
                $scope.tempObject.IsDelegatedYesNoOption = $scope.plans[i].DelegatedType;
            }
        }
    };
    //$scope.viewMode = false;
    $scope.allItemsSelected = false;
    $scope.selection = [];
    //$scope.NotSelected = [];
    $scope.toggleSelection = function (CredentialingContractRequests) {
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
    $scope.loadingmessage = false;
    $scope.isHasError = false;
    $scope.PlanListID = null;
    $scope.showPlanListReCred = function (Plan) {
        $scope.CredentialingContractRequests = [];
        $scope.isHasError = false;
        $scope.loadingmessage = true;
        $scope.loadingAjax1 = true;
        $scope.PlanListID = Plan;
        $scope.selection = [];
        var ProviderID = $scope.ProviderIDLIST;

        $http.get(rootDir + '/Credentialing/Initiation/getCredentialingContractRequest?ProviderID=' + ProviderID + '&PlanID=' + Plan).
            success(function (data, status, headers, config) {
                try {
                    if (data.status == true) {
                        $scope.CredContractRequest = angular.copy(data.data1);
                        for (var i = 0; i < $scope.CredContractRequest.length; i++) {
                            if (localStorage.getItem('NavigateToInitiateRecredential') == 'true') {
                                for (var j = 0; j < $scope.CredContractRequest[i].CredentialingContractRequests.ContractGrid.length; j++) {
                                    if ($scope.localdata.License.ContractGridID == $scope.CredContractRequest[i].CredentialingContractRequests.ContractGrid[j].ContractGridID) {
                                        $scope.CredContractRequest[i].CredentialingContractRequests.TableRowStatus = $scope.CredContractRequest[i].TableRowStatus;
                                        $scope.CredContractRequest[i].CredentialingContractRequests.credID = $scope.CredContractRequest[i].credID;
                                        $scope.CredentialingContractRequests.push($scope.CredContractRequest[i].CredentialingContractRequests);
                                    }
                                }
                            }
                            else {
                                $scope.CredContractRequest[i].CredentialingContractRequests.TableRowStatus = $scope.CredContractRequest[i].TableRowStatus;
                                $scope.CredContractRequest[i].CredentialingContractRequests.credID = $scope.CredContractRequest[i].credID;
                                $scope.CredentialingContractRequests.push($scope.CredContractRequest[i].CredentialingContractRequests);
                            }

                        }
                        $scope.loadingmessage = false;
                        $scope.loadingAjax1 = false;
                        $scope.SelectAllCheckBox = 0;
                        if ($scope.CredentialingContractRequests.length == 0) {
                            messageAlertEngine.callAlertMessage('errorInitiated', "", "danger", true);
                            $scope.errorInitiated = "Provider is not credentialed for any plan";
                        }

                        if ($scope.CredentialingContractRequests.length > 0) {
                            for (var z = 0; z < $scope.CredentialingContractRequests.length; z++) {
                                if ($scope.CredentialingContractRequests[z].TableRowStatus == true) {
                                    $scope.SelectAllCheckBox++;
                                }
                            }
                        }

                        //$scope.NotSelected = angular.copy($scope.CredentialingContractRequests);
                        localStorage.removeItem('NavigateToInitiateRecredential');
                        localStorage.removeItem('dataToStore');
                    }
                } catch (e) {

                }
            }).error(function (data, status, headers, config) {
                $scope.loadingmessage = false;
            });

    }
    $scope.ProviderIDLIST = null;
    $scope.CredentialingContractRequests = [];
    $scope.SelectProviderForReCredentialingInitiation = function (provObj) {
        if (localStorage.getItem('NavigateToInitiateRecredential') == 'true') {
            var tempObj = {};
            tempObj = provObj[0];
            provObj = {};
            provObj = tempObj;
        }
        $scope.disble = false;
        $scope.CredentialingContractRequests = [];
        $scope.isHasError = false;
        $scope.showInit = true;
        $scope.SearchProviderPanelToggleDown('InitiationPanel');
        $scope.initiateSuccess = false;
        $scope.reCredData = provObj;

        $scope.ProviderIDLIST = $scope.reCredData.ProfileID;
        var ProviderID = $scope.reCredData.ProfileID;
        var PlanID = $scope.reCredData.PlanID;

        $http.get(rootDir + '/Credentialing/Initiation/getPlanListforCredentialingContractRequest?ProviderID=' + ProviderID).
            success(function (data, status, headers, config) {
                try {
                    if (data.status == true) {
                        $scope.PlanList = angular.copy(data.data1);
                    }
                } catch (e) {

                }
            }).error(function (data, status, headers, config) {

            });
    };

    $scope.PlanListForDeCred = [];
    $scope.initiateSuccessForDeCred = false;
    $scope.SelectProviderPlanDetail = function (obj) {
        $scope.ContractGridDetailForDeCred = [];

        $scope.initiateSuccessForDeCred = false;
        $scope.deCredData = obj;
        $scope.isHasError = false;
        $scope.ProviderIDForDeCred = $scope.deCredData.ProfileID;
        $http.get(rootDir + '/Credentialing/Initiation/getPlanListforCredentialingContractRequest?ProviderID=' + $scope.ProviderIDForDeCred).
            success(function (data, status, headers, config) {
                try {
                    if (data.status == true) {
                        $scope.PlanListForDeCred = angular.copy(data.data1);

                    }
                } catch (e) {

                }
            }).error(function (data, status, headers, config) {

            });
        $scope.initiate = true;


        if (true) {
            $scope.SearchProviderPanelToggleDown('ProfileDetailPanel');
            //$scope.init_table($scope.profileDetail, 4);
        } else {

            messageAlertEngine.callAlertMessage('errorSpecialty', "No Plan Reports Available for the Given Option", "danger", true);
            $("body").animate({ scrollTop: $("#errorMsg").offset().top });
        }

        $scope.reasonsForDeCred = [];
        $http.get(rootDir + "/MasterDataNew/GetAllDecredentialingReasons").
             success(function (data, status, headers, config) {
                 try {
                     if (data != null) {
                         $scope.reasonsForDeCred = data;
                     }
                 } catch (e) {
                 }
             }).
             error(function (data, status, headers, config) {
             });



    }
    $scope.loadingmessagefordecred = false;
    $scope.ContractGridDetailForDeCred = [];
    $scope.showPlanListDeCred = function (PlanID) {
        $scope.loadingmessage = false;
        $scope.tempo.allItemsSelectedForDecred = false;
        $scope.selectionForDecred = [];
        $scope.ContractGridDetailForDeCred = [];
        var planForDeCredID = PlanID;
        $scope.loadingAjax1 = true;
        $scope.loadingmessagefordecred = true;
        $http.get(rootDir + '/Credentialing/Initiation/getCredentialingContractGridForDecred?ProviderID=' + $scope.ProviderIDForDeCred + '&PlanID=' + planForDeCredID).
            success(function (data, status, headers, config) {
                try {
                    $scope.loadingmessagefordecred = false;
                    for (var i = 0; i < data.length; i++) {
                        if (data[i].ContractGridObject != null) {
                            //var selectedObj = obj.CredentialingContractRequests[i].ContractGrid[j];
                            //$scope.profileDetail.push(selectedObj);
                            var currObj = new Object();
                            currObj.CredentialingInfoID = data[i].CredInfoID;
                            currObj.CredentialingContractRequestID = data[i].CredRequestID;
                            if (data[i].ContractGridObject.ProfileSpecialty == null) { currObj.Speciality = "Not Available"; }
                            else {
                                currObj.Speciality = data[i].ContractGridObject.ProfileSpecialty.Specialty.Name;
                            }
                            if (data[i].ContractGridObject.LOB == null) { currObj.LOB = "Not Available"; }
                            else {
                                currObj.LOB = data[i].ContractGridObject.LOB.LOBName;
                            }
                            if (data[i].ContractGridObject.ProfilePracticeLocation == null) { currObj.Location = "Not Available"; }
                            else {
                                currObj.Location = data[i].ContractGridObject.ProfilePracticeLocation.Facility.FacilityName + "-" + data[i].ContractGridObject.ProfilePracticeLocation.Facility.Street + "," + data[i].ContractGridObject.ProfilePracticeLocation.Facility.City + "," + data[i].ContractGridObject.ProfilePracticeLocation.Facility.State + "," + data[i].ContractGridObject.ProfilePracticeLocation.Facility.Country;
                                currObj.Location = $scope.TrimNUllValue(currObj.Location);
                            }
                            if (data[i].ContractGridObject.BusinessEntity == null) { currObj.GroupName = "Not Available"; }
                            else {
                                currObj.GroupName = data[i].ContractGridObject.BusinessEntity.GroupName;
                            }
                            currObj.Check = false;
                            var inititatedDate = $scope.ConvertDateFormat(data[i].ContractGridObject.InitialCredentialingDate);
                            currObj.InitiationDate = $filter('date')(new Date(inititatedDate), 'MM-dd-yyyy');
                            currObj.ContractGridID = data[i].ContractGridObject.ContractGridID;
                            $scope.ContractGridDetailForDeCred.push(currObj);
                        }
                    }
                    if ($scope.ContractGridDetailForDeCred.length > 0) {
                        $scope.init_table($scope.ContractGridDetailForDeCred, 4);

                    }
                    else if ($scope.ContractGridDetailForDeCred.length == 0) {
                        messageAlertEngine.callAlertMessage('NOData', "No Plan Report Found for Decredentialing", "danger", true);
                    }

                    $scope.loadingAjax1 = false;
                } catch (e) {
                    $scope.loadingAjax1 = false;
                }
            }).error(function (data, status, headers, config) {
                $scope.loadingmessagefordecred = false;
                messageAlertEngine.callAlertMessage('Error', "Please Try Again Later", "danger", true);
                $scope.loadingAjax1 = false;
            })
    }


    $scope.TrimNUllValue = function (data) {

        var d = data.replace(/null/g, "");


        d = d.replace(",,,", ",");
        d = d.replace(",,", ",");


        if (d.charAt(d.length - 1) == ',') {
            d = d.slice(0, d.length - 2);
        }

        if (d.charAt(0) == ',') {
            d = d.slice(1);
        }

        if (d.charAt(0) == '-') {
            d = d.slice(1);
        }
        return d;

    };

    $scope.profileDetail = [];

    $scope.CredInfoIdArray = [];
    $scope.CredContractIdArray = [];
    $scope.ContractGridID = [];

    $scope.createIdArray = function () {


        var count = 0;

        for (var i = 0; i < $scope.profileDetail.length; i++) {

            if ($scope.profileDetail[i].Check == true) {

                $scope.CredInfoIdArray.push($scope.profileDetail[i].CredentialingInfoID);
                $scope.CredContractIdArray.push($scope.profileDetail[i].CredentialingContractRequestID);
                $scope.ContractGridID.push($scope.profileDetail[i].ContractGridID);

            }

        }

        for (var i = 0; i < $scope.data.length; i++) {

            if ($scope.data[i].Check == true) {

                $scope.data.splice($scope.data.indexOf($scope.data[i]), 1);

            }

        }

        if ($scope.data.length == 0) {

            for (var j = 0; j < $scope.data.length; j++) {

                if ($scope.data[j].ProfileID == $scope.ProfileID) {

                    $scope.data.splice($scope.data.indexOf($scope.data[j]), 1);

                }

            }

        }



    }

    $scope.fillObj = function (data) {

        $scope.ProfileID = data.ProfileID;
        $scope.profileDetail = [];

        for (var i = 0; i < $scope.credInfo.length; i++) {

            if ($scope.credInfo[i].ProfileID == data.ProfileID) {

                if ($scope.credInfo[i].CredentialingContractRequests != null && $scope.credInfo[i].CredentialingContractRequests != 0) {

                    for (var j = 0; j < $scope.credInfo[i].CredentialingContractRequests.length; j++) {

                        if ($scope.credInfo[i].CredentialingContractRequests[j].ContractGrid != null && $scope.credInfo[i].CredentialingContractRequests[j].ContractGrid != 0) {

                            for (var k = 0; k < $scope.credInfo[i].CredentialingContractRequests[j].ContractGrid.length; k++) {

                                if ($scope.credInfo[i].CredentialingContractRequests[j].ContractGrid[k].Report == null) {

                                    $scope.profileDetail.push($scope.credInfo[i].CredentialingContractRequests[j].ContractGrid[k]);
                                    $scope.profileDetail[$scope.profileDetail.length - 1].CredentialingInfoID = $scope.credInfo[i].CredentialingInfoID;
                                    $scope.profileDetail[$scope.profileDetail.length - 1].CredentialingContractRequestID = $scope.credInfo[i].CredentialingContractRequests[j].CredentialingContractRequestID;
                                    $scope.profileDetail[$scope.profileDetail.length - 1].PlanName = $scope.credInfo[i].Plan.PlanName;
                                    $scope.profileDetail[$scope.profileDetail.length - 1].Speciality = $scope.credInfo[i].CredentialingContractRequests[j].ContractGrid[k].ProfileSpecialty.Specialty.Name;
                                    $scope.profileDetail[$scope.profileDetail.length - 1].LOB = $scope.credInfo[i].CredentialingContractRequests[j].ContractGrid[k].LOB.LOBName;
                                    $scope.profileDetail[$scope.profileDetail.length - 1].Location = $scope.credInfo[i].CredentialingContractRequests[j].ContractGrid[k].ProfilePracticeLocation.Facility.FacilityName;
                                    $scope.profileDetail[$scope.profileDetail.length - 1].GroupName = $scope.credInfo[i].CredentialingContractRequests[j].ContractGrid[k].BusinessEntity.GroupName;
                                    $scope.profileDetail[$scope.profileDetail.length - 1].Check = false;
                                    $scope.profileDetail[$scope.profileDetail.length - 1].InitiationDate = $scope.ConvertDateFormat($scope.credInfo[i].InitiationDate);

                                }

                            }

                        }

                    }

                }

                //$scope.profileDetail.push($scope.data[i]);

            }
        }



    }

    $scope.clearSearch = function () {
        $scope.tempObject = "";
        $scope.data = "";

        //$scope.allProviders = "";
        //$('a[href=#SearchResult]').trigger('click');
        // $scope.Npi = null;
    }

    $scope.ClearInit = function () {
        $scope.showInit = false;

    }
    $scope.msgAlert = false;
    $scope.InitiateCredentialing = function () {
        var obj = {
            ProfileID: $scope.ProfileID,
            NPINumber: $scope.NPINumber,
            CAQHNumber: $scope.CAQH,
            FirstName: $scope.Firstname,
            LastName: $scope.Lastname,
            PlanID: $scope.tempObject.PlanId,
            IsDelegatedYesNoOption: $scope.tempObject.IsDelegatedYesNoOption,
            StatusType: 1
        };

        $http.post(rootDir + '/Credentialing/Initiation/InitiateCredentialing', obj).
            success(function (data, status, headers, config) {
                try {
                    //----------- success message -----------
                    if (data.status == "true") {
                        $scope.credinfoId = data.credentialingInfo.CredentialingInfoID;
                        $scope.credID = $scope.credinfoId;
                        $http.post(rootDir + '/Credentialing/CnD/InitiateCredentialingfromContractGrid?CredentialingInfoId=' + $scope.credinfoId)
                          .success(function (data, status, headers, config) {
                              $rootScope.LoadPlanDataJs = true;
                              //    $scope.PlanReportList = $.makeArray(data);
                              $scope.ContractLocationsList = [];
                              $scope.Specilities = [];
                              $scope.NewProviderName = obj.FirstName + ' ' + obj.LastName;
                              $scope.ProfileId = obj.ProfileID;
                              $scope.NewNPINumber = obj.NPINumber;
                              $scope.credentialingInfo = data;
                              //   $scope.masterPracticeLocation = data.Profile.PracticeLocationDetails;
                              if ($scope.credentialingInfo.Profile != null && $scope.credentialingInfo.Profile.PracticeLocationDetails != null && $scope.credentialingInfo.Profile.PracticeLocationDetails.length != 0) {

                                  for (var i = 0; i < $scope.credentialingInfo.Profile.PracticeLocationDetails.length; i++) {
                                      if ($scope.credentialingInfo.Profile.PracticeLocationDetails[i].Status != 'Inactive') {
                                          $scope.ContractLocationsList.push($scope.credentialingInfo.Profile.PracticeLocationDetails[i]);
                                      }
                                  }

                              }
                              if ($scope.credentialingInfo.Profile != null && $scope.credentialingInfo.Profile.SpecialtyDetails != null && $scope.credentialingInfo.Profile.SpecialtyDetails.length != 0) {

                                  for (var i = 0; i < $scope.credentialingInfo.Profile.SpecialtyDetails.length; i++) {
                                      if ($scope.credentialingInfo.Profile.SpecialtyDetails[i].Status != 'Inactive') {
                                          $scope.Specilities.push($scope.credentialingInfo.Profile.SpecialtyDetails[i]);
                                      }
                                  }

                              }
                              $scope.ContractSpecialityList = $scope.Specilities;
                              $scope.LoadPlan();
                              $scope.AssignData();
                              $scope.Show_LOB_Green = true;
                              $("#LobTab").removeClass('disabledTab');
                              $scope.changeTabs('LobTab', 'id03', 'LOBPanel');
                              $scope.goingBack = true;
                              $scope.firstPalnLob = false;
                              $scope.btnName = 'Initiate Credentialing For New Provider';
                              //   $scope.isHasError = true;
                          })
                          .error(function () { });
                        $scope.initiateSuccess = true;
                        sessionStorage.setItem('CreListId', 1);

                        messageAlertEngine.callAlertMessage('successfulInitiated', "Credentialing Initiated Successfully. !!!!", "success", true);
                    }
                    else {
                        messageAlertEngine.callAlertMessage('errorInitiated', "", "danger", true);
                        $scope.errorInitiated = data.status.split(",");
                    }
                } catch (e) {

                }
            }).
            error(function (data, status, headers, config) {
                //----------- error message -----------
                messageAlertEngine.callAlertMessage('errorInitiated', "", "danger", true);
                $scope.errorInitiated = "Sorry for Inconvenience !!!! Please Try Again Later...";
            });
    }

    //Convert the date from database to normal

    $scope.ConvertDateTo = function (value) {
        var shortDate = null;
        if (value) {
            var regex = /-?\d+/;
            var matches = regex.exec(value);
            var dt = new Date(parseInt(matches[0]));
            var month = dt.getMonth() + 1;
            var monthString = month > 9 ? month : '0' + month;
            //var monthName = monthNames[month];
            var day = dt.getDate();
            var dayString = day > 9 ? day : '0' + day;
            var year = dt.getFullYear();
            shortDate = monthString + '/' + dayString + '/' + year;
            //shortDate = dayString + 'th ' + monthName + ',' + year;
        }
        return shortDate;
    };


    $scope.ConvertDateFormat = function (value) {

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

    $scope.getReCredList = function () {
        $http.get(rootDir + '/Credentialing/Initiation/GetAllReCredentialings').
          success(function (data, status, headers, config) {
              try {
                  $scope.data = data;
                  for (var i = 0; i < $scope.data.length ; i++) {
                      if ($scope.data[i].CredentialingInfoID == $scope.tempID) {
                          $scope.ProfileID = $scope.data[i].ProfileID;
                          $scope.delegatePlan = $scope.data[i].Plan.DelegatedType;
                      }
                  }
              } catch (e) {

              }

          }).
          error(function (data, status, headers, config) {

          });
    }
    $scope.disble = false;
    $scope.InitiateReCredentialing = function (provObj) {
        $scope.disble = true;
        var id = provObj.CredentialingInfoID;
        var obj2 = {
            ProfileID: provObj.ProfileID,
            NPINumber: provObj.Profile.OtherIdentificationNumber.NPINumber,
            CAQHNumber: provObj.Profile.OtherIdentificationNumber.CAQH,
            FirstName: provObj.Profile.PersonalDetail.Firstname,
            LastName: provObj.Profile.PersonalDetail.Lastname,
            PlanID: $scope.PlanListID,
            IsDelegatedYesNoOption: provObj.IsDelegatedYesNoOption,
            StatusType: 1
        };
        //var NotSelectedID = [];
        $scope.CredentialingContractRequestsIntArray = [];
        $scope.NonCredentialingContractRequestsIntArray = [];
        for (var i = 0; i < $scope.selection.length; i++) {
            $scope.CredentialingContractRequestsIntArray[i] = $scope.selection[i].CredentialingContractRequestID;
        }
        //for (var i = 0; i < $scope.NotSelected.length; i++) {
        //    var count = 0;
        //    for (var j = 0; j < $scope.selection.length; j++) {
        //        if ($scope.NotSelected[i].CredentialingContractRequestID == $scope.selection[j].CredentialingContractRequestID) {
        //            count++;
        //        }
        //    }
        //    if (count == 0) {
        //        NotSelectedID.push($scope.NotSelected[i].CredentialingContractRequestID);
        //    }
        //}

        $scope.isHasError = false;
        if ($scope.PlanListID == null) {
            $scope.isHasError = true;
        }

        if ($scope.isHasError == false) {
            $http({
                method: "POST",
                url: rootDir + "/Credentialing/Initiation/InitiateReCredentialing?id=" + id,
                data: {
                    credentialingInitiationInfo: obj2,
                    CredentialingContractRequestsArray: angular.copy($scope.CredentialingContractRequestsIntArray)
                    //NonCredentialingContractRequestsArray: angular.copy(NotSelectedID)
                }
            }).success(function (data, status, headers, config) {
                //----------- success message -----------
                try {
                    if (data.status == "true") {
                        $scope.initiateSuccess = true;
                        $scope.initiateComplete = true;
                        $scope.IsMessage = true;
                        sessionStorage.setItem('CreListId', 2);
                        $scope.tempID = data.ID;
                        $scope.setCredInfo($scope.tempID);

                        $scope.getReCredList();
                        $scope.disble = false;




                        messageAlertEngine.callAlertMessage('successfulInitiated', "ReCredentialing Initiated Successfully. !!!!", "success", true);

                    }
                    else {
                        $scope.disble = false;
                        messageAlertEngine.callAlertMessage('errorInitiated', "", "danger", true);
                        $scope.errorInitiated = data.status.split(",");
                    }
                } catch (e) {

                }
            }).error(function (data, status, headers, config) {
                $scope.disble = false;
                //----------- error message -----------
                messageAlertEngine.callAlertMessage('errorInitiated', "", "danger", true);
                $scope.errorInitiated = "Sorry for Inconvenience !!!! Please Try Again Later...";
            });
        }
    }

    $scope.selectAllDetails = function () {

        for (var i = 0 ; i < $scope.profileDetail.length ; i++) {

            $scope.profileDetail[i].Check = true;

        }

    }
    $scope.temp = {
        IsAllChecked: false
    }
    $scope.checkMe = function (status) {

        if (status == false) {
            $scope.temp.IsAllChecked = false;
        }
        var count = 0;

        for (var i = 0; i < $scope.profileDetail.length; i++) {

            if ($scope.profileDetail[i].Check == true) {

                $scope.isSelected = false;

                count++;

            }

        }

        if (count == 0) {

            $scope.isSelected = true;

        }

    }

    $scope.initiateComplete = false;

    $scope.DecredentialingWarning = function () {
        try {
            if ($scope.reasonforDecred == undefined || $scope.reasonforDecred == "") {
                $scope.hasDecredError = true;
            }
            else {
                $('#decredWarningModal').modal();
            }
        } catch (e) {
            $scope.hasDecredError = true;
        }
    }

    $scope.InitiateDeCredentialing = function () {
        $scope.createIdArray();
        var Infoid = [];
        var ContractId = [];
        var GridId = [];

        for (var i = 0; i < $scope.selectionForDecred.length; i++) {
            Infoid.push($scope.selectionForDecred[i].CredentialingInfoID);
            ContractId.push($scope.selectionForDecred[i].CredentialingContractRequestID);
            GridId.push($scope.selectionForDecred[i].ContractGridID);
        }


        //$http({
        //    method: "POST",
        //    url: rootDir + "/Credentialing/Initiation/mymethod",
        //    data: {
        //        doc: $('#file').file()
        //    }
        //}).success(function (data, status, headers, config) {
        //    try {
        //        //----------- success message -----------
        //        if (data.status == "true") {
        //            alert();
        //                            }
        //        else {

        //        }
        //    } catch (e) {

        //    }
        //}).error(function (data, status, headers, config) {
        //    //----------- error message -----------

        //});


        //$formData = $('#DecredentialingDoc');
        //var $form = $("#DecredentialingDoc");
        //formdata = new FormData($form);
        //formdata.append('DecredentialingDocument', $("#DecredentialingDoc"))
        //var FORMDATA = new FormData($form[0]);
        //var other_data = $('#DecredentialingDoc').serializeArray();
        //$.each(other_data, function (key, input) {
        //    FORMDATA.append(input.name, input.value);
        //});
        //FORMDATA.append("DecredentialingDocument", $('#supDoc').val());
        // var $form = new FormData($('#buildingBlockForm')[0]);
        $http({
            method: "POST",
            url: rootDir + "/Credentialing/Initiation/InitiateDeCredentialing",
            data: {

                InfoidArray: Infoid,
                ContractidArray: ContractId,
                GrididArray: GridId,
                reasonId: $scope.reasonforDecredId,
                decredDoc: $scope.tempAttachments
            }
        }).success(function (data, status, headers, config) {
            try {
                //----------- success message -----------
                if (data.status == "true") {
                    $scope.loadjsfile("AddNewLoadToPlanJs.js");
                    $scope.initiateSuccessForDeCred = true;
                    $scope.ContractGridDetailForDeCred = [];

                    messageAlertEngine.callAlertMessage('LoadedSuccess', "DeCredentialing Initiated Successfully. !!!!", "success", true);
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


    //To keep the selected reason
    $scope.selectedReason = function (data) {
        for (var i = 0; i < $scope.reasonsForDeCred.length; i++) {
            if ($scope.reasonsForDeCred[i].DecredentialingReasonId == data) {
                $scope.reasonforDecred = $scope.reasonsForDeCred[i].Reason;
                $scope.reasonforDecredId = $scope.reasonsForDeCred[i].DecredentialingReasonId;
            }
        }
        $scope.hasDecredError = false;
        $scope.decredButton = true;
    }


    //for email pop
    function GetTemplates() {
        $http.get(rootDir + '../../../EmailService/GetAllEmailTemplates').
         success(function (data, status, headers, config) {

             try {
                 if (data != null) {
                     $scope.templates = data;
                 }
                 for (var i = 0; i < $scope.templates.length; i++) {
                     $scope.templates[i].LastModifiedDate = $scope.ConvertDateFormat($scope.templates[i].LastModifiedDate);
                 }
             } catch (e) {

             }

         }).
         error(function (data, status, headers, config) {

         });
    }
    $scope.templateSelected = true;
    //$scope.tempObject.UseExistingTemplate = 'NO';
    $scope.changeYesNoOption = function (value) {
        $("#newEmailForm .field-validation-error").remove();
        $scope.templateSelected = false;
        //$scope.tempObject.Subject = "";
        //$scope.tempObject.Body = "";

        if (value == 2) {
            $scope.templateSelected = true;
            $scope.tempObject.Subject = "";
            $scope.tempObject.Body = "";
        }
        $scope.hideDiv();
    }

    $scope.hideDiv = function () {
        $("#templatelist").hide();
        $scope.errorMsg = false;
        $("#newEmailForm .text-danger").hide();
    }


    $scope.MailWithDecredentialing = function () {
        if ($scope.hasDecredError == false) {
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();

            availableTags = angular.copy($scope.EmailsIds);
            $('.badge').removeAttr("style");
            //if ($scope.fileList.length > 0) {
            //    $scope.decredSupportingDoc = true;
            //}
            //else {
            //    $scope.decredSupportingDoc = false;
            //}            
            if ($scope.fileList.length > 0) {
                $scope.fileList[0].File.fileType = "SupportingDocument";
            }
            $scope.tempFileList = angular.copy($scope.fileList);
            var promise = $scope.GenerateEmailPopUp();
        }
    }

    $scope.SendMailAndInitiateDecred = function (id) {
        //var AddOrSave = $scope.AddOrSaveEmail(id);
        //if($scope.mailSuccess){
        //    var InitiateDecred = $scope.InitiateDeCredentialing();
        //}
        //var combinedPromise = $q.all({
        //    mail: AddOrSave,
        //    initiate: InitiateDecred
        //});

        var pro = $scope.AddOrSaveEmail(id)
        if ($scope.mailSuccess) {
            $scope.InitiateDeCredentialing();
        }
    }

    $scope.GenerateEmailPopUp = function () {
        $("#newEmailForm .text-danger").hide();
        //var availableTags = [];        
        //availableTags = angular.copy($scope.EmailsIds);
        //$('.badge').removeAttr("style");
        //$scope.tempObject.UseExistingTemplate = '';
        GetTemplates();
        $scope.errmsgforTo = false;
        $scope.errmsgforCC = false;
        $scope.errmsgforBCC = false;
        //$scope.fileList = [];
        var formdata = $('#newEmailForm');
        formdata[0].reset();
        $scope.tempObject.SaveAsTemplateYesNoOption = '';
        $scope.tempObject.SaveAsTemplateYesNoOption = 2;
        $scope.tempObject.Title = '';
        $scope.templateSelected = true;
        $scope.tempObject.Body = "";
        //$scope.tempObject.To = $scope.deCredData.Profile.ContactDetail.EmailIDs[0].EmailAddress;
        //$scope.tempObject.Subject = 'Initiated for Decredentiling ';
        var groupName = '';
        var LOB = '';
        var Location = '';
        var speciality = '';
        //$('#composeMail').appendTo("body");
        //$("#composeMail").modal("show");
        //$("#composeMail").css("z-index", "1000");        
        //$('body').addClass('modal-open');
        if ($scope.selectionForDecred.length > 1) {
            for (var i = 0; i < $scope.selectionForDecred.length; i++) {
                if (($scope.selectionForDecred.length > 2) && i != (($scope.selectionForDecred.length) - 2)) {
                    groupName = groupName + $scope.selectionForDecred[i].GroupName + ', \n ';
                    LOB = LOB + $scope.selectionForDecred[i].LOB + ', \n ';
                    Location = Location + $scope.selectionForDecred[i].Location + ', \n ';
                    speciality = speciality + $scope.selectionForDecred[i].Speciality + ', \n '
                }
                if (i == ($scope.selectionForDecred.length) - 2) {
                    groupName = groupName + $scope.selectionForDecred[i].GroupName + ' & ';
                    LOB = LOB + $scope.selectionForDecred[i].LOB + ' & ';
                    Location = Location + $scope.selectionForDecred[i].Location + ' & ';
                    speciality = speciality + $scope.selectionForDecred[i].Speciality + ' & '
                }
            }
        } else {
            groupName = $scope.selectionForDecred[0].GroupName + '  ';
            LOB = $scope.selectionForDecred[0].LOB + ' ';
            Location = $scope.selectionForDecred[0].Location + ' ';
            speciality = $scope.selectionForDecred[0].Speciality + ' '

        }

        //var body = 'Hi ' + $scope.deCredData.Name + ' ,   \n' + "\t your Licences has been intiated for decredentialing of plan's " + $scope.deCredData.PlanName +
        //    " with group Name's " + groupName + ' under this LOB\'s ' + LOB + ' in this practice Location ' + Location + ' for this speciality ' + speciality + '  ' + $scope.reasonforDecred;
        //$scope.tempObject.Body = body;
        $scope.tempObject.File = $scope.fileinput;
        //$scope.tempObject.IsRecurrenceEnabledYesNoOption = 2;
        $("#composeMail").on('shown.bs.modal', function () {
            $('body').addClass('modal-open');
        });
    }

    $scope.EditCancle = function (temp) {
        //ResetFormForValidation($("#newEmailForm"));
        $scope.errmsgforTo = false;
        $scope.errmsgforCC = false;
        $scope.errmsgforBCC = false;
        //$scope.fileList = [];
        var formdata = $('#newEmailForm');
        formdata[0].reset();
        if ($scope.data != null && temp != null) {
            for (var c = 0; c < $scope.data.length; c++) {
                if ($scope.data[c].EmailTemplateID == temp.EmailTemplateID) {
                    $scope.data[c].EditStatus = false;
                    //$scope.tableParams.reload();
                    break;
                }
            }
        }
        $scope.compose = false;
        $scope.tempObject.To = "";
        $scope.tempObject.CC = "";
        $scope.tempObject.BCC = "";
        $scope.tempObject.Subject = "";
        $scope.tempObject.Body = "";
        $scope.tempObject.IsRecurrenceEnabledYesNoOption = "";
        $scope.tempObject.RecurrenceIntervalTypeCategory = "";
        $scope.tempObject.FromDate = "";
        $scope.tempObject.ToDate = "";
        $scope.tempObject.IntervalFactor = "";
        $("#newEmailForm .field-validation-error").hide();
        $scope.tempObject.SaveAsTemplateYesNoOption = 2;
        //$scope.fileList = [];
        //$scope.fileList = $scope.tempFileList;
        //try {
        //    if ($scope.fileList.length == 0) {
        //        $scope.fileset = false;
        //    }
        //    else {
        //        $scope.fileset = true;
        //    }
        //} catch (e) {
        //    throw e;
        //}
        try {
            if ($scope.fileList.length != 0 && $scope.fileList[0].File.hasOwnProperty('fileType')) {
                if ($scope.fileList.length > 1) {
                    $scope.fileList.splice(1, $scope.fileList.length - 1)
                    $scope.fileset = true;
                }
                else {
                    $scope.fileset = true;
                }
            }
            else {
                $scope.fileList = [];
                $scope.fileset = false;
            }
        } catch (e) {
            $scope.fileList = [];
            $scope.fileset = false;
        }

    }

    $scope.ResetFormForValidation = function (form) {
        form.removeData('validator');
        form.removeData('unobtrusiveValidation');
        $.validator.unobtrusive.parse(form);
    }

    //Code for multiple file upoloads
    $scope.FilesizeError = false;
    //Multiple Document ADD to DisClosure Question Start

    var QID = 0;
    var index1 = -1;
    $scope.addingDocument = function () {
        $('#file').click();
    }

    $scope.fileList = [];
    $scope.curFile;
    $scope.ImageProperty = {
        file: '',
        FileListID: -1,
        FileID: -1,
        FileStatus: ''
    }
    $scope.removeFile = function (index, fileObj) {
        $scope.fileList.splice(index, 1)
        //if($scope.tempFileList)
        $scope.fileset = false;
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
    var files = [];
    var tempIndex = 0;
    var tempmultiplefilelength;
    $scope.setFile = function (element, doctype) {
        var count = 0;
        tempIndex = 0;
        var index = -1;
        files = [];
        files = element.files;
        var totalfilesize = 0;
        tempmultiplefilelength = $scope.fileList.length;
        if (count == 0) {
            for (var i = 0; i < files.length; i++) {
                if (files[i].size > 15728640) {
                    //$('#Filesizeerror').show();
                    //$scope.FilesizeError = true;
                    $('.badge').attr("style", "background-color:white;color:indianred");
                    $scope.fileset = false;
                    // $('.badge').attr("style", "color:red");
                    //$scope.fileList = [];
                    break;
                } else {
                    //$('#Filesizeerror').hide();
                    $('.badge').removeAttr("style");
                    totalfilesize += files[i].size;
                    var TempArray = [];
                    $scope.ImageProperty.file = files[i];
                    $scope.ImageProperty.FileStatus = 'Active';
                    $scope.ImageProperty.FileListID = $scope.fileList.length;
                    $scope.ImageProperty.FileID = i;
                    TempArray.push($scope.ImageProperty);
                    $scope.fileList.push({ File: TempArray });
                    $scope.ImageProperty = {};
                    $scope.fileset = true;
                    if (!$scope.$$fetch)
                        $scope.$apply();
                }
            }
        }
        $scope.UploadFile(doctype);
    }

    $scope.UploadFile = function (uploadDoctype) {
        $scope.UploadingDocument = uploadDoctype;
        for (var i = 0; i < $scope.fileList.length; i++) {
            for (var j = 0; j < $scope.fileList[i].File.length; j++) {
                if ($scope.fileList[i].File[j].UploadDone != true) $scope.fileList[i].File[j].UploadDone = false;
                if ($scope.fileList[i].File[j].FileStatus == 'Active') {
                    $scope.UploadFileIndividual($scope.fileList[i].File[j].file,
                                        $scope.fileList[i].File[j].file.name,
                                        $scope.fileList[i].File[j].file.type,
                                        $scope.fileList[i].File[j].file.size,
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
            var resultdata = JSON.parse(evt.currentTarget.responseText);
            $scope.Attachments.push(resultdata.FilePath);
            if ($scope.UploadingDocument == "SupportingDoc") {
                $scope.tempAttachments = resultdata.FilePath;
            }
            if (files.length == 1) {
                $scope.fileList[$scope.fileList.length - 1].File[0].path = resultdata.FilePath;
                $scope.fileList[$scope.fileList.length - 1].File[0].relativePath = resultdata.RelativePath;
                $scope.fileList[$scope.fileList.length - 1].File[0].UploadDone = true;
            } else if (files.length != 1 && tempmultiplefilelength != 0) {
                $scope.fileList[tempmultiplefilelength].File[0].path = resultdata.FilePath;
                $scope.fileList[tempmultiplefilelength].File[0].relativePath = resultdata.RelativePath;
                $scope.fileList[tempmultiplefilelength].File[0].UploadDone = true;
                tempmultiplefilelength++;
            } else {
                $scope.fileList[tempIndex].File[0].path = resultdata.FilePath;
                $scope.fileList[tempIndex].File[0].relativePath = resultdata.RelativePath;
                $scope.fileList[tempIndex].File[0].UploadDone = true;
                tempIndex++;
            }
            $scope.NoOfFileSaved++;
            $scope.$apply();
            $('#file').val("");
        }

        function uploadFailed(evt) {
        }

        function uploadCanceled(evt) {
        }

    }

    $scope.Attachments = [];
    //END
    $scope.errmsgforTo = false;
    $scope.errmsgforCC = false;
    $scope.errmsgforBCC = false;
    $scope.FileUploadProgress = false;

    $scope.AddOrSaveEmail = function (Form_Id) {
        $scope.FileUploadProgress = false;
        var AttachmentsSize = 0;
        for (i = 0; i < $scope.fileList.length; i++) {
            AttachmentsSize += $scope.fileList[i].File[0].file.size;
            if ($scope.fileList[i].File[0].relativePath == "" || $scope.fileList[i].File[0].relativePath === undefined) {
                $scope.FileUploadProgress = true;
                messageAlertEngine.callAlertMessage('warningdiv', "File Upload is in Progress", "info", true);
                //$('#composeMail').animate({ scrollBottom: 0 }, 'medium');
                break;
            }
        }
        if (AttachmentsSize > 15728640) {
            messageAlertEngine.callAlertMessage('warningdiv', 'Files exceeded the size limit!', "info", true);
        }
        else {
        }
        var regx1 = /^[a-z][a-zA-Z0-9_]*(\.[a-zA-Z][a-zA-Z0-9_]*)?@[a-z][a-zA-Z-0-9]*\.[a-z]+(\.[a-z]+)?$/;
        ResetFormForValidation($("#" + Form_Id));


        var emailids = $('#tags').val().split(';');
        for (var i in emailids) {
            if (emailids[i] != "") {
                $scope.errmsg = false;
                if (regx1.test(emailids[i].toLowerCase())) {
                    $scope.errmsgforTo = false;
                }
                else {
                    if (i == 0)
                        $scope.errmsgforTo = true;
                }
            }
            else {
                if (i == 0)
                    $scope.errmsg = true;
            }

        }
        if ($scope.tempObject.ToDate && $scope.tempObject.FromDate) {
            if ($scope.tempObject.ToDate < $scope.tempObject.FromDate)

                $scope.errmsgforToDate = true;

            else
                $scope.errmsgforToDate = false;

        }
        var emailDataCC = $('#tagsCC').val().split(';');
        for (var i in emailDataCC) {
            if (emailDataCC[i] != "") {
                if (regx1.test(emailDataCC[i].toLowerCase())) {
                    $scope.errmsgforCC = false;
                }
                else { $scope.errmsgforCC = true; }
            }
        }

        var emailDataBCC = $('#tagsBCC').val().split(';');
        for (var i in emailDataBCC) {
            if (emailDataBCC[i] != "") {
                if (regx1.test(emailDataBCC[i].toLowerCase())) {
                    $scope.errmsgforBCC = false;
                }
                else { $scope.errmsgforBCC = true; }
            }
        }


        var checkVariable = false;
        if (!$scope.errmsgforBCC && !$scope.errmsgforCC && !$scope.errmsgforTo && !$scope.errmsgforToDate && !$scope.errmsg) { checkVariable = true; }

        if ($("#" + Form_Id).valid() && checkVariable && !$scope.FileUploadProgress && AttachmentsSize < 15728640) {


            var ltcharCheck = true;
            var gtcharCheck = true;
            while (gtcharCheck == true || ltcharCheck == true) {
                if ($('#Body').val().indexOf('<') > -1) {
                    $('#Body').val($('#Body').val().replace("<", "&lt;"));
                    ltcharCheck = true;
                }
                else {
                    ltcharCheck = false;
                }
                if ($('#Body').val().indexOf('>') > -1) {
                    $('#Body').val($('#Body').val().replace(">", "&gt;"));
                    gtcharCheck = true;
                }
                else {
                    gtcharCheck = false;
                }
            }

            var $form = ($("#newEmailForm")[0]);

            $.ajax({
                url: rootDir + '/EmailService/AddEmail',
                type: 'POST',
                data: new FormData($form),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {

                    try {
                        if (data.status != "Unable to Send Email") {
                            $('#composeMail').modal('toggle');
                            $scope.compose = false;
                            $scope.mailSuccess = true;
                            //$scope.tableParamsFollowUp.reload();
                            //$scope.tableParamsSent.reload();
                            $scope.tempObject.IntervalFactor = null;
                            messageAlertEngine.callAlertMessage('LoadedSuccess', "Email Sent Successfully.", "success", true);
                            //$scope.getTabData('Sent');
                        }
                        else {
                            $('#composeMail').modal('toggle');

                            messageAlertEngine.callAlertMessage('LoadedUnSuccess', data.status, "danger", true);
                            //$scope.errorMsg = data.status;
                        }
                    } catch (e) {

                    }
                    finally {

                        $scope.tempObject.To = "";
                        $scope.tempObject.CC = "";
                        $scope.tempObject.BCC = "";
                        $scope.tempObject.Subject = "";
                        $scope.tempObject.Body = "";
                        $scope.tempObject.IsRecurrenceEnabledYesNoOption = "";
                        $scope.tempObject.RecurrenceIntervalTypeCategory = "";
                        $scope.tempObject.FromDate = "";
                        $scope.tempObject.ToDate = "";
                        $scope.tempObject.IntervalFactor = "";

                    }
                },
                error: function (data) {
                    messageAlertEngine.callAlertMessage('errorMsgDiv', data.status, "danger", true);
                    //$scope.errorMsg = "Unable to schedule Email.";
                }

            });



        }
        else {
            $("#newEmailForm .field-validation-error").show();
            $scope.mailSuccess = false;
        }
    }

    $scope.showContent = function () {
        $scope.templateSelected = true;
        $("#newEmailForm .text-danger").hide();
    }

    $scope.initPop = function () {
        $('[data-toggle="popover"]').popover();
    };


    $rootScope.DashboardRedirect = function () {


        var data = JSON.parse(localStorage.getItem('dataToStore'));
        $rootScope.sample = 'TRUE';
        var tempdata = data;
        if (localStorage.getItem('NavigateToInitiateRecredential') == 'true') {
            $scope.setActionID2(2);
            $scope.new_search(2);

        }

    }


    $scope.emailsAutoFill = function () {
        $(function () {

            //availableTags = $scope.EmailsIds;

            function split(val) {
                return val.split(/;\s*/);
            }
            function extractLast(term) {
                return split(term).pop();
            }

            $("#tags,#tagsCC,#tagsBCC")
                // don't navigate away from the field on tab when selecting an item
                .bind("keydown", function (event) {
                    if (event.keyCode === $.ui.keyCode.TAB &&
                            $(this).autocomplete("instance").menu.active) {
                        event.preventDefault();
                    }
                })
                .autocomplete({
                    minLength: 0,
                    source: function (request, response) {
                        // delegate back to autocomplete, but extract the last term
                        response($.ui.autocomplete.filter(
                            availableTags, extractLast(request.term)));
                    },
                    focus: function () {
                        // prevent value inserted on focus
                        return false;
                    },
                    select: function (event, ui) {
                        var terms = split(this.value);
                        // remove the current input
                        terms.pop();
                        // add the selected item
                        terms.push(ui.item.value);
                        // add placeholder to get the comma-and-space at the end
                        terms.push("");
                        this.value = terms.join(";");
                        return false;
                    }
                });
        });
    }

    //$scope.resetForm = function () {
    //    $scope.MyDocument.$setPristine();
    //};

    //================================= Hide All search by type popover =========================

    $scope.showSearchByTypeDiv = function (div_Id) {
        changeVisibilityOfSearchByType();
        $("#" + div_Id).show();
    };
    var changeVisibilityOfSearchByType = function () {
        $(".ProviderTypeSelectAutoList1").hide();
        // method will close any other country code div already open.
    };
    if ((localStorage.getItem('NavigateToInitiateRecredential') != undefined) && (localStorage.getItem('NavigateToInitiateRecredential') == 'true')) {
        $rootScope.DashboardRedirect();
    }
    //===========================================================================================
    $scope.popCount = 0;

    $(document).click(function (event) {
        if (!$(event.target).hasClass("form-control") && $(event.target).parents(".ProviderTypeSelectAutoList").length === 0) {
            $(".ProviderTypeSelectAutoList").hide();
        }
        if (!$(event.target).attr("data-searchdropdown") && $(event.target).parents(".ProviderTypeSelectAutoList1").length === 0) {
            $(".ProviderTypeSelectAutoList1").hide();
        }
        if (!$(event.target).hasClass("ringMyBell") && !$(event.target).hasClass("checkMe")) {
            $('#alertArea').hide();

            $('#alertArea').css("display", "");
        } else {
            $('#alertArea').show();
        }
    });

    $(document).ready(function () {
        $(".ProviderTypeSelectAutoList").hide();
        $(".ProviderTypeSelectAutoList1").hide();
        $scope.SearchProviderPanelToggleDown('SearchProviderPanel');
        //if ((localStorage.getItem('NavigateToInitiateRecredential') != undefined) && (localStorage.getItem('NavigateToInitiateRecredential') == 'true')) {
        //    $rootScope.DashboardRedirect();
        //}
        //else {
        //    $scope.ProviderTypesData();
        //    $scope.SpecialtyData();
        //    $scope.Groups();
        //}
    });
    function ResetFormForValidation(form) {
        form.removeData('validator');
        form.removeData('unobtrusiveValidation');
        $.validator.unobtrusive.parse(form);
    }
    function showLocationList(ele) {
        $(ele).parent().find(".ProviderTypeSelectAutoList").first().show();
    }

    $scope.loadjsfile = function (filetype) {
        //if filename is a external JavaScript file
        var fileref = document.createElement('script')
        fileref.setAttribute("type", "text/javascript")
        fileref.setAttribute("src", filename)

    }

    $scope.LoadPlan = function () {
        //----------LOB data-----------
        $http.get(rootDir + '/MasterDataNew/GetAllLOBsOfPlanContractByPlanID?planID=' + $scope.credentialingInfo.PlanID).
 success(function (data, status, headers, config) {
     try {
         $scope.ContractLOBsList = angular.copy(data);
         $scope.MasterContractLOBslist = data;
     } catch (e) {

     }
 }).
 error(function (data, status, headers, config) {

 });

        //------------------Business Entity---------------
        $http.get(rootDir + '/MasterDataNew/GetAllOrganizationGroupAsync').
        success(function (data, status, headers, config) {
            try {
                $scope.BusinessEntities = angular.copy(data);
                $scope.MasterBusinessEntities = data;
            } catch (e) {

            }
        }).
        error(function (data, status, headers, config) {

        });


        //-------------Data Locations-------------



    };

    $scope.AssignData = function () {
        $scope.tempObject =
       {
           BusinessEntity: "",
           ContractLOBs: [],
           ContractSpecialties: [],
           ContractPracticeLocations: [],
           AllSpecialtiesSelectedYesNoOption: 0,
           AllLOBsSelectedYesNoOption: 0,
           AllPracticeLocationsSelectedYesNoOption: 0,
       };

        $scope.MasterTemp = angular.copy($scope.tempObject);

    };

    $scope.showContryCodeDiv = function (div_Id) {
        $("#LOBdiv").hide();
        $("#Specialitydiv").hide();
        $("#Locationsdiv").hide();
        $("#" + div_Id).show();
    };

    //  $scope.ContractLOBsList = [];
    $scope.tempObject.ContractLOBs = [];
    $scope.SelectLOBName = function (c, div) {

        if (c != null) {

            $scope.tempObject.ContractLOBs.push({
                LOBID: c.LOBID,
                LOBName: c.LOBName
            });
            $scope.ContractLOBsList.splice($scope.ContractLOBsList.indexOf(c), 1);
        }
        $scope.LOBName = "";
        $("#" + div).hide();
    };
    $scope.RemoveCoveringPhysiciansType = function (c) {

        if (c != null) {

            $scope.tempObject.ContractLOBs.splice($scope.tempObject.ContractLOBs.indexOf(c), 1)
            $scope.ContractLOBsList.push(c);
        }
    };

    // Splty
    $scope.tempObject.ContractSpecialties = [];
    $scope.SelectSpecialityName = function (c, div) {

        if (c != null) {

            $scope.tempObject.ContractSpecialties.push({
                ProfileSpecialtyID: c.SpecialtyDetailID,
                SpecialtyName: c.Specialty.Name
            });
            $scope.ContractSpecialityList.splice($scope.ContractSpecialityList.indexOf(c), 1);

        }

        $scope.SpecialtyName = "";
        $("#" + div).hide();
    };
    $scope.RemoveContractSpecialties = function (c) {

        if (c != null) {

            $scope.tempObject.ContractSpecialties.splice($scope.tempObject.ContractSpecialties.indexOf(c), 1)
            c.Specialty = {};
            c.Specialty.Name = c.SpecialtyName;
            c.SpecialtyDetailID = c.ProfileSpecialtyID;
            $scope.ContractSpecialityList.push(c);

        }
    };

    // Loc
    $scope.tempObject.ContractPracticeLocations = [];
    $scope.SelectLocationsName = function (c, div) {

        if (c != null) {

            $scope.tempObject.ContractPracticeLocations.push({
                ProfilePracticeLocationID: c.PracticeLocationDetailID,
                LocationsName: c.Facility.FacilityName,
                StreetName: c.Facility.Street,
                CityName: c.Facility.City,
                StateName: c.Facility.State,
                CountryName: c.Facility.Country
            });
            $scope.ContractLocationsList.splice($scope.ContractLocationsList.indexOf(c), 1);

        }
        $scope.LocationsName = "";
        $("#" + div).hide();

    };
    $scope.RemoveContractLocations = function (c) {

        if (c != null) {

            $scope.tempObject.ContractPracticeLocations.splice($scope.tempObject.ContractPracticeLocations.indexOf(c), 1)
            c.Facility = {};
            c.Facility.FacilityName = c.LocationsName;
            c.Facility.Street = c.StreetName;
            c.Facility.City = c.CityName;
            c.Facility.State = c.StateName;
            c.Facility.Country = c.CountryName;
            $scope.ContractLocationsList.push(c);

        }
    };

    $scope.isHasError = false;
    $scope.LoadedData = [];
    $scope.ShowDetailTable = function (tempObject) {
        $scope.isHasError = false;
        //  $scope.tempObject.BusinessEntity = $('#BEID').find($("[name='BE'] option:selected")).text();
        for (var c = 0; c < $scope.credentialingInfo.CredentialingLogs.length; c++) {
            if ($scope.credentialingInfo.CredentialingLogs[c].Credentialing == "Credentialing" || $scope.credentialingInfo.CredentialingLogs[c].Credentialing == "ReCredentialing") {
                $scope.Log = $scope.credentialingInfo.CredentialingLogs[c];
                break;
            }
        }
        if ($scope.credentialingInfo.CredentialingLogs != null) {
            if ($scope.Log.CredentialingAppointmentDetail != null) {
                if ($scope.Log.CredentialingAppointmentDetail.CredentialingAppointmentSchedule != null) {
                    if ($scope.Log.CredentialingAppointmentDetail.CredentialingAppointmentResult != null) {
                        $scope.LoadedData.InitialCredentialingDate = $scope.Log.CredentialingAppointmentDetail.CredentialingAppointmentResult.SignedDate;
                    }
                }
            }
            else {
                $scope.LoadedData.InitialCredentialingDate = null;
            }
        }

        $scope.tempObject.InitialCredentialingDate = $scope.LoadedData.InitialCredentialingDate;


        $('#LoadPlan').show();
        if ($scope.tempObject.BusinessEntityID == null || $scope.tempObject.ContractPracticeLocations.length == 0 || $scope.tempObject.ContractSpecialties.length == 0 || $scope.tempObject.ContractLOBs.length == 0) {
            $scope.isHasError = true;
        }
        $rootScope.ccmstat = false;
        if ($scope.isHasError == false) {
            $scope.loadingAjax = true;
            $http.post(rootDir + '/Credentialing/CnD/AddLoadedData?credentialingInfoID=' + $scope.credID, tempObject).
            success(function (data, status, headers, config) {
                try {
                    if (data.status == 'true') {
                        data.dataCredentialingContractRequest.isSelected = false;
                        $scope.loadingAjax = false;
                        $rootScope.isLoaded = true;
                        $rootScope.loadedDate = new Date();
                        $scope.PlanReportStatus = true;

                        $scope.ContractSpecialityList = [];
                        $scope.ContractLocationsList = [];
                        $scope.loadID = data.dataCredentialingContractRequest.ContractGrid[0].CredentialingInfo.InitiatedByID;


                        $scope.tempContractSpecialityList = angular.copy($scope.credentialingInfo.Profile.SpecialtyDetails);
                        for (var i = 0; i < $scope.tempContractSpecialityList.length; i++) {
                            if ($scope.tempContractSpecialityList[i].Status != 'Inactive') {
                                $scope.ContractSpecialityList.push($scope.tempContractSpecialityList[i]);
                            }
                        }
                        $scope.tempContractLocationsList = angular.copy($scope.credentialingInfo.Profile.PracticeLocationDetails);
                        for (var i = 0; i < $scope.tempContractLocationsList.length; i++) {
                            if ($scope.tempContractLocationsList[i].Status != 'Inactive') {
                                $scope.ContractLocationsList.push($scope.tempContractLocationsList[i]);
                            }
                        }
                        $scope.BusinessEntities = angular.copy($scope.MasterBusinessEntities);
                        $scope.ContractLOBsList = angular.copy($scope.MasterContractLOBslist);
                        $scope.tempObject = angular.copy($scope.MasterTemp);
                        if (data.dataCredentialingContractRequest.InitialCredentialingDate != null) {
                            var date = $rootScope.ConvertDateFormat(data.dataCredentialingContractRequest.InitialCredentialingDate);
                            data.dataCredentialingContractRequest.InitialCredentialingDate = $filter('date')(new Date(date), 'yyyy-MM-dd');
                        } else {
                            //    data.dataCredentialingContractRequest.InitialCredentialingDate = $rootScope.ConvertDateFormat(data.dataCredentialingContractRequest.InitialCredentialingDate);
                        }

                        for (var i = 0; i < data.dataCredentialingContractRequest.ContractSpecialties.length; i++) {
                            if (data.dataCredentialingContractRequest.ContractSpecialties[i].ProfileSpecialty.SpecialtyBoardCertifiedDetail != null) {
                                if (data.dataCredentialingContractRequest.ContractSpecialties[i].ProfileSpecialty.SpecialtyBoardCertifiedDetail.InitialCertificationDate != null) {
                                    data.dataCredentialingContractRequest.ContractSpecialties[i].ProfileSpecialty.SpecialtyBoardCertifiedDetail.InitialCertificationDate = $rootScope.ConvertDateFormat(data.dataCredentialingContractRequest.ContractSpecialties[i].ProfileSpecialty.SpecialtyBoardCertifiedDetail.InitialCertificationDate);
                                }
                                if (data.dataCredentialingContractRequest.ContractSpecialties[i].ProfileSpecialty.SpecialtyBoardCertifiedDetail.ExpirationDate != null) {
                                    data.dataCredentialingContractRequest.ContractSpecialties[i].ProfileSpecialty.SpecialtyBoardCertifiedDetail.ExpirationDate = $rootScope.ConvertDateFormat(data.dataCredentialingContractRequest.ContractSpecialties[i].ProfileSpecialty.SpecialtyBoardCertifiedDetail.ExpirationDate);
                                }
                            }


                        }

                        $scope.LoadedData.push(data.dataCredentialingContractRequest);
                        var TabStatusLTP2 = $scope.LoadedData.length > 0 ? true : false;
                        //  $scope.$broadcast('LTPStatus', { StatusLTP: TabStatusLTP2 });
                        $scope.PlanReportTabStatus = true;
                        //$scope.init_table();

                        for (var i = 0; i < data.dataCredentialingContractRequest.ContractGrid.length; i++) {
                            if (data.dataCredentialingContractRequest.ContractGrid[i].Report == null) {
                                data.dataCredentialingContractRequest.ContractGrid[i].Report = {};
                            }
                            //   data.dataCredentialingContractRequest.ContractGrid[i].InitialCredentialingDate = $rootScope.ConvertDateFormat(data.dataCredentialingContractRequest.InitialCredentialingDate);
                            //data.dataCredentialingContractRequest.ContractGrid[i].Report.TerminationDate = $scope.ConvertDateBy3Years($scope.LoadedData.InitialCredentialingDate);
                            //data.dataCredentialingContractRequest.ContractGrid[i].Report.ReCredentialingDate = $scope.ConvertDateBy3Years($scope.LoadedData.InitialCredentialingDate);
                            $scope.PlanReportList.push(data.dataCredentialingContractRequest.ContractGrid[i]);

                        }


                        //   messageAlertEngine.callAlertMessage('LoadedSuccess', "Contract Request Loaded to Plan Successfully !!!", "success", true);

                        $scope.Show_PlanEnroll_Green = true;
                        $scope.ShowVisibility = '';
                        $("#PlanEnrollTab").removeClass('disabledTab');
                    }
                } catch (e) {

                }
            }).
            error(function (data, status, headers, config) {
                $scope.ErrMsg = 'Failed to enroll plan, Try again....!!!';
            });
            $scope.changeTabs('PlanEnrollTab', 'id04', 'PlanEnrollmentPanel');
        }

    };
    $scope.changeDateTime = function (values) {
        values = convertToEasternTimeZone(values);
        if (!values) { return ''; }
        var returnValue = values;
        var format;
        try {
            if (values.indexOf("/Date(") == 0) {
                returnValue = new Date(parseInt(values.replace("/Date(", "").replace(")/", ""), 10));
            }
        } catch (e) {
            returnValue = returnValue;
        }
        if (angular.isDate(returnValue)) {
            //value = returnValue.setTime(returnValue.getTime() - returnValue.getTimezoneOffset() * 60 * 1000);
            value = returnValue.getHours() + ":" + returnValue.getMinutes();
        }
        else {
            var formatDate
            returnValue = values.split('T')[1];
            formatDate = new Date(values.split('T')[0]);
            value = returnValue;
            returnValue = formatDate;
        }
        var time = value.split(":");
        var hours = time[0];
        var minutes = time[1];
        var ampm = hours >= 12 ? 'PM' : 'AM';
        hours = hours % 12;
        hours = hours ? hours : 12; // the hour '0' should be '12'
        minutes = minutes.length == 1 ? minutes < 10 ? '0' + minutes : minutes : minutes;
        var d = returnValue.toString();
        var stampDate = d.split(' ');
        var strTime = stampDate[1] + ' ' + stampDate[2] + ' ' + stampDate[3] + ' ' + hours + ':' + minutes + ' ' + ampm;
        //minutes = minutes < 9 ? '00' : minutes;
        //if (format == true) {
        //    var strTime = stampDate[1] + ' ' + stampDate[2] + ' ' + stampDate[3] + ' ' + hours + ':' + minutes + ' ' + ampm;
        //}
        //else {
        //    stampDate = formatDate.toDateString();
        //    var strTime = stampDate + ' ' + hours + ':' + minutes + ' ' + ampm;
        //}
        return strTime;
    }

    $scope.ClearVisibility = function () {
        $scope.validRequired = true;
        $scope.validFormat = true;
        $scope.ShowVisibility = '';
    }

    $scope.tempSecObject = {
        ContractGridID: 0,
        InitialCredentialingDate: new Date,
        Report: {},
    };

    $scope.SetVisibility = function (type, index) {
        if (type == 'Qedit') {
            $scope.qTempObject = angular.copy($scope.PlanReportList[index]);
            $scope.ShowVisibility = 'QeditVisibility' + index;
        }
        else if (type == 'edit') {
            $scope.eTempObject = angular.copy($scope.PlanReportList[index]);
            if ($scope.eTempObject.InitialCredentialingDate != null) {
                $scope.eTempObject.InitialCredentialingDate = $rootScope.ConvertDateFormat($scope.eTempObject.InitialCredentialingDate);
                if ($scope.eTempObject.Report.TerminationDate == '' || $scope.eTempObject.Report.TerminationDate == null) {
                    // $scope.eTempObject.Report.TerminationDate = $scope.ConvertDateBy3Years($scope.eTempObject.InitialCredentialingDate);
                }
                //if ($scope.eTempObject.Report.ReCredentialingDate == '' || $scope.eTempObject.Report.ReCredentialingDate == null) {
                //    $scope.eTempObject.Report.ReCredentialingDate = $scope.ConvertDateBy3Years($scope.eTempObject.InitialCredentialingDate);
                //}
            }

                //if ($scope.eTempObject.Report.CredentialingApprovalStatusType == '' || $scope.eTempObject.Report.CredentialingApprovalStatusType == null) {
                //    $scope.eTempObject.Report.CredentialingApprovalStatusType = 'Rejected';
                //}

            else {
                $scope.eTempObject.Report.ReCredentialingDate = null;
            }
            $scope.ShowVisibility = 'editVisibility' + index;
        }
        else if (type == 'view') {
            $scope.ShowVisibility = 'viewVisibility' + index;
            if ($scope.PlanReportList[index].InitialCredentialingDate != null && ($scope.PlanReportList[index].Report.ReCredentialingDate == '' || $scope.PlanReportList[index].Report.ReCredentialingDate == null)) {
                //$scope.PlanReportList[index].Report.ReCredentialingDate = $scope.ConvertDateBy3Years($scope.PlanReportList[index].InitialCredentialingDate);
            }
        }
    };

    $scope.initWarning = function (c, i) {
        $($('#WarningModal_Remove').find('button')[2]).attr('disabled', false);
        if (c != null) {

            $scope.tempRemoveReportData = angular.copy(c);

        }

        $('#WarningModal_Remove').modal();
    };

    $scope.RemoveLoadToPlan = function (c) {
        $($('#WarningModal_Remove').find('button')[2]).attr('disabled', true);
        $.ajax({
            url: rootDir + '/Credentialing/CnD/RemoveLoadPlan?contractGridID=' + c.ContractGridID,
            type: 'POST',
            data: null,
            async: false,
            cache: false,
            contentType: false,
            processData: false,
            success: function (data) {

                try {
                    if (data.status == "true") {
                        var obj = $filter('filter')($scope.PlanReportList, { ContractGridID: data.contractGridID })[0];
                        $scope.PlanReportList.splice($scope.PlanReportList.indexOf(obj), 1);
                        //$scope.PlanReportList.splice($scope.PlanReportList.indexOf(c), 1);
                        //  $scope.credentialingInfo.CredentialingContractRequests.splice($scope.credentialingInfo.CredentialingContractRequests.indexOf(c), 1);
                        $('#WarningModal_Remove').modal('hide');
                    }

                    else {

                    }
                }
                catch (e) { };


            },
            error: function (e) {
                try {
                    //$scope.SLError = "Sorry for Inconvenience !!!! Please Try Again Later...";
                    //messageAlertEngine.callAlertMessage('StateLicenseError', "", "danger", true);
                }
                catch (e) { };


            }

        });

    };
    $scope.SaveReport = function (c, index) {

        var validationStatus = true;
        var url;
        var myData = {};
        var $formData;
        //if ($('#fileexists').text() == "" && $scope.eTempObject.Report.WelcomeLetterPath != null)
        //{
        //    $scope.tempObject.Report.WelcomeLetterPath = $scope.eTempObject.Report.WelcomeLetterPath;
        //}
        //  if ($scope.Visibility == ('editVisibility' + index)) {
        //Add Details - Denote the URL
        try {
            $formData = $('#PlanReportForm_AddContractPage').find('form');
            url = rootDir + "/Credentialing/CnD/SaveReport";
        }
        catch (e)
        { };
        ResetFormForValidation($formData);
        validationStatus = $formData.valid();
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
                    try {
                        data = JSON.parse(data);
                        if (data.status == "true") {
                            console.log(data);
                            data.dataContractGrid.InitialCredentialingDate = $rootScope.ConvertDateFormat(data.dataContractGrid.InitialCredentialingDate);
                            if (data.dataContractGrid.Report != null) {
                                data.dataContractGrid.Report.CredentialedDate = $rootScope.ConvertDateFormat(data.dataContractGrid.Report.CredentialedDate);
                                data.dataContractGrid.Report.InitiatedDate = $rootScope.ConvertDateFormat(data.dataContractGrid.Report.InitiatedDate);
                                data.dataContractGrid.Report.TerminationDate = $rootScope.ConvertDateFormat(data.dataContractGrid.Report.TerminationDate);
                                data.dataContractGrid.Report.ReCredentialingDate = $rootScope.ConvertDateFormat(data.dataContractGrid.Report.ReCredentialingDate);
                                $scope.PlanReportStatus = true;
                            }
                            $scope.PlanReportList[index] = data.dataContractGrid;

                            $scope.showcontracts = true;
                            messageAlertEngine.callAlertMessage('ReportSaveSuccess' + index, "Plan Report Updated Successfully !!!", "success", true);
                            $scope.SetVisibility('view', index);
                            $scope.PlanReportStatus = true;
                        }
                        else {
                            $scope.SLError = data.status.split(",");
                            messageAlertEngine.callAlertMessage('ReportError', "", "danger", true);
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
    };

    $scope.QuickSave = function (c, index) {

        if (c != null) {

            $scope.validRequired = true;
            $scope.validFormat = true;
            $scope.tempSecObject.ContractGridID = c.ContractGridID;
            $scope.tempSecObject.InitialCredentialingDate = angular.copy(c.InitialCredentialingDate);
            $scope.tempSecObject.Report.ProviderID = c.Report.ProviderID;
            $scope.tempSecObject.Report.CredentialingContractInfoFromPlanID = c.Report.CredentialingContractInfoFromPlanID;
            $scope.PlanReportStatus = true;

        }

        if ($('#dataContainer' + index).find('#InitialCredentialingDate').val() == '') {
            $scope.validRequired = false;
        }
        if ($('#dataContainer' + index).find('#InitialCredentialingDate').val() != '' && typeof ($scope.tempSecObject.InitialCredentialingDate) == 'undefined') {
            $scope.validFormat = false;
        }

        if ($scope.validFormat == true && $scope.validRequired == true) {
            $http.post(rootDir + '/Credentialing/CnD/QuickSaveReport', $scope.tempSecObject).
               success(function (data, status, headers, config) {
                   try {
                       //data = JSON.parse(data);
                       data.dataContractGrid.InitialCredentialingDate = $rootScope.ConvertDateFormat(data.dataContractGrid.InitialCredentialingDate);
                       //if (data.dataContractGrid.Report != null) {
                       //    data.dataContractGrid.Report.CredentialedDate = $rootScope.ConvertDateFormat(data.dataContractGrid.Report.CredentialedDate);
                       //    data.dataContractGrid.Report.InitiatedDate = $rootScope.ConvertDateFormat(data.dataContractGrid.Report.InitiatedDate);
                       //    data.dataContractGrid.Report.TerminationDate = $rootScope.ConvertDateFormat(data.dataContractGrid.Report.TerminationDate);
                       //    data.dataContractGrid.Report.ReCredentialingDate = $rootScope.ConvertDateFormat(data.dataContractGrid.Report.ReCredentialingDate);
                       //}
                       $scope.PlanReportList[index] = angular.copy(data.dataContractGrid);
                       messageAlertEngine.callAlertMessage('ReportSaveSuccess' + index, "Plan Report Updated Successfully !!!", "success", true);
                       $scope.SetVisibility('view', index);
                   } catch (e) {

                   }
               }).
               error(function (data, status, headers, config) {

               });
            $scope.ClearVisibility();
        }
    };

    $scope.LoadFunction = function () {
        location.reload();

    };

    //------------creating the data to be displayed in contract Grid---------
    $scope.createDisplaydata = function (data) {
        if (data.Report.InitiatedDate != null) {
            var date = $rootScope.ConvertDateFormat(data.Report.InitiatedDate);
            data.Report.InitiatedDate = $filter('date')(new Date(date), 'MM/dd/yyyy');
        }

        if (data.Report.TerminationDate != null) {
            var date = $rootScope.ConvertDateFormat(data.Report.TerminationDate);
            data.Report.TerminationDate = $filter('date')(new Date(date), 'MM/dd/yyyy');
        }

        var temp = {
            BE: data.BusinessEntity.GroupName || '',
            ContractGridStatus: data.Status || '',
            ContractGridID: data.ContractGridID || '',
            PlanName: data.CredentialingInfo.Plan.PlanName || '',
            //    EffectiveDate :$scope.PlanReportList.,
            GroupID: data.Report.GroupID || '',
            //    IndividualID: $scope.PlanReportList.,
            InitiatedDate: data.Report.InitiatedDate || '',
            LOBCode: data.LOB.LOBCode || '',
            LOBName: data.LOB.LOBName || '',
            NPINumber: $scope.NewNPINumber,
            PanelStatus: data.Report.PanelStatus || '',
            ParticipatingStatus: data.Report.ParticipatingStatus || '',
            //PracticeLocationCorporateName: data.ProfilePracticeLocation.PracticeLocationCorporateName || '',
            FacilityName: data.ProfilePracticeLocation.Facility.FacilityName,
            FacilityStreet: data.ProfilePracticeLocation.Facility.Street,
            FacilityCity: data.ProfilePracticeLocation.Facility.City,
            FacilityState: data.ProfilePracticeLocation.Facility.State,
            FacilityCountry: data.ProfilePracticeLocation.Facility.Country,
            ProviderID: data.Report.ProviderID || '',
            ProfileID: $scope.ProfileId,
            //    ProviderFirstName :$scope.PlanReportList. ,
            //    ProviderLastName:$scope.PlanReportList. , 
            //    ProviderMiddleName:$scope.PlanReportList. , 
            ProviderName: $scope.NewProviderName,
            TerminationDate: data.Report.TerminationDate || '',
        }
        return temp;
    };


    $scope.SetRecredentialingDate = function (value) {
        if (value == null) {
            return
        }
        else {
            if ($scope.eTempObject != null && $scope.eTempObject != {}) {
                var RecredentialingDuration = $.grep($scope.eTempObject.CredentialingInfo.Plan.PlanLOBs, function (ele) { return ele.LOBID == $scope.eTempObject.LOB.LOBID })[0].ReCredentialingDuration;
                $scope.eTempObject.Report.ReCredentialingDate = $scope.ConvertDateBy3OR5Years(value, RecredentialingDuration != null ? RecredentialingDuration : 3);
            }
        }
    }

    $scope.ConvertDateBy3OR5Years = function (date, RecredentialingDuration) {
        if (date != '' || date != 'null') {
            var dt = new Date(date);
            //var NoOfDays = checkLeapYear(dt, LOBID);
            var month = dt.getMonth() + 1;
            var monthString = month > 9 ? month : '0' + month;
            //var monthName = monthNames[month];
            var day = dt.getDate();
            var dayString = day > 9 ? day : '0' + day;
            var year = 0;
            //if (LOBID == 1 || LOBID == 2) {

            //    year = dt.getFullYear() + 5;
            //}
            //else {
            //    year = dt.getFullYear() + 3;
            //}
            year = dt.getFullYear() + RecredentialingDuration;
            shortDate = monthString + '/' + dayString + '/' + year;
            return shortDate;
        } return null;
    }

    $scope.Add_ContractToGrid = function (data) {
        for (var i in data) {
            var newTempObject = $scope.createDisplaydata(data[i]);
            newTempObject.PracticeLocation = data[i].ProfilePracticeLocation.Facility.FacilityName + '-' + data[i].ProfilePracticeLocation.Facility.Street + ',' + data[i].ProfilePracticeLocation.Facility.City + ',' + data[i].ProfilePracticeLocation.Facility.State + ',' + data[i].ProfilePracticeLocation.Facility.Country;
            $rootScope.randomsItems.push(newTempObject);
            $rootScope.randomsItemsTemp.push(newTempObject);
        }

        $rootScope.pushContract();
        $scope.resetAllData();
    };

    $scope.resetAllData = function () {
        $scope.changeTabs('GenInfoTab', 'id01', 'SearchProviderPanel');
        $("#LobTab").addClass('disabledTab');
        $("#PlanEnrollTab").addClass('disabledTab');
        $scope.Show_LOB_Green = false;
        $scope.Show_PlanEnroll_Green = false;
        $scope.result = false;
        $scope.initiate = false;
        //$scope.initiateSuccessForDeCred = true;
        $scope.showInit = false;
        $scope.SearchProviderPanelToggle('SearchProviderPanel');
        $scope.goingBack = false;
        $scope.PlanReportList = [];
        $scope.btnName = 'Back To Search Result';
    };
    $scope.btnName = 'Back to Search Result';

    $scope.ResettingLOBTab = function () {
        $scope.tempObject.PlanId = '';
        $scope.goingBack = false;
        $scope.Show_LOB_Green = false;
        $scope.showbutton = false;
        $scope.Show_PlanEnroll_Green = false;
        $("#LobTab").addClass('disabledTab');
        $("#PlanEnrollTab").addClass('disabledTab');
        $scope.PlanReportList = [];
        $scope.btnName = 'Back To Search Result';
    };

    $scope.SelectStatus = function (status) {
        $(".ProviderTypeSelectAutoList").hide();
        $scope.eTempObject.Report.ParticipatingStatus = status;
    }
});
function ResetFormForValidation(form) {
    form.removeData('validator');
    form.removeData('unobtrusiveValidation');
    $.validator.unobtrusive.parse(form);
};
