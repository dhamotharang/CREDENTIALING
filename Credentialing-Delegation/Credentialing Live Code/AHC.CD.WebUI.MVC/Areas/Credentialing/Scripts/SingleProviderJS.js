//var initCredApp = angular.module('InitCredApp', []);

initCredApp.directive('searchdropdown', function () {
    return {
        restrict: 'AE',
        link: function (scope, element, attr) {
            element.bind('focus', function () {
                element.parent().find(".TemplateSelectAutoList").show();
            });
        }
    };
});
initCredApp.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

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

initCredApp.controller('singleProviderCtrl', function ($scope, $http, $q, $timeout, $location, $filter, ngTableParams, $rootScope, messageAlertEngine) {

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

    $scope.resetSelection = function (data) {
        var temp = [];
        for (var i in data) {
            temp[i] = false;
        }
        return temp;
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
        tempObject.PlanId = type.PlanID;
        tempObject.PlanName = type.PlanName;

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
        else if (condition == 2) {
            if ($scope.tableParams2 != null) {
                $scope.tableParams2.reload();
            }
            else {
                $scope.tableParams2 = new ngTableParams({
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
        else if (condition == 3) {
            if ($scope.tableParams3 != null) {
                $scope.tableParams3.reload();
                $scope.data = data;
            }
            else {
                $scope.tableParams3 = new ngTableParams({
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
        else if (condition == 4) {
            if ($scope.tableParams4 != null) {
                $scope.tableParams4.reload();
            }
            else {
                $scope.tableParams4 = new ngTableParams({
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
                        //alert($scope.planDetail);
                        var filteredData = params.filter() ?
                                $filter('filter')($scope.data, params.filter()) :
                                $scope.data;
                        var orderedData = params.sorting() ?
                                $filter('orderBy')(filteredData, params.orderBy()) :
                                $scope.data;

                        params.total(orderedData.length); // set total for recalc pagination
                        $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
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
        if (id == 1) {
            $('#decredsearch,#recredsearch').addClass("inactive");
            $('#credsearch').removeClass("inactive");
        }
        else if (id == 2) {
            $('#decredsearch,#credsearch').addClass("inactive");
            $('#recredsearch').removeClass("inactive");
        }
        else {
            $('#recredsearch,#credsearch').addClass("inactive");
            $('#decredsearch').removeClass("inactive");
        }
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
                    if ($scope.selectedAction == 2) {
                        //-----start prepare recred data for filter--------

                        for (var i = 0; i < resultData.length ; i++) {
                            $scope.data[i].ss = [];
                            $scope.data[i].ip = [];
                            $scope.data[i].Name = resultData[i].Profile.PersonalDetail.FirstName + ' ' + resultData[i].Profile.PersonalDetail.LastName;
                            $scope.data[i].NPINumber = resultData[i].Profile.OtherIdentificationNumber.NPINumber;

                            for (var j = 0; j < resultData[i].Profile.PersonalDetail.ProviderTitles.length ; j++) {
                                if (j == 0) {
                                    if (resultData[i].Profile.PersonalDetail.ProviderTitles[j].Status == "Active") {
                                        $scope.data[i].Title = resultData[i].Profile.PersonalDetail.ProviderTitles[j].ProviderType.Title;
                                    } else {
                                        $scope.data[i].Title = "";
                                    }
                                }
                                else if (resultData[i].Profile.PersonalDetail.ProviderTitles[j].Status == "Active") {
                                    if ($scope.data[i].Title == "") {
                                        $scope.data[i].Title += resultData[i].Profile.PersonalDetail.ProviderTitles[j].ProviderType.Title;
                                    } else {
                                        $scope.data[i].Title += (", " + resultData[i].Profile.PersonalDetail.ProviderTitles[j].ProviderType.Title);
                                    }
                                }
                            }

                            //resultData[i].Profile.SpecialtyDetails = $scope.squash(resultData[i].Profile.SpecialtyDetails);

                            for (var j = 0; j < resultData[i].Profile.SpecialtyDetails.length; j++) {
                                if (j == 0) {
                                    if (resultData[i].Profile.SpecialtyDetails[j].Status == "Active") {
                                        $scope.data[i].SpecialtyName = resultData[i].Profile.SpecialtyDetails[j].Specialty.Name;
                                        $scope.data[i].ss.push(resultData[i].Profile.SpecialtyDetails[j].Specialty.Name);

                                    } else {
                                        $scope.data[i].SpecialtyName = "";
                                        //$scope.data[i].ss.push(resultData[i].Profile.SpecialtyDetails[j].Specialty.Name);
                                    }
                                }
                                else if (resultData[i].Profile.SpecialtyDetails[j].Status == "Active") {
                                    if ($scope.data[i].SpecialtyName == "") {
                                        $scope.data[i].SpecialtyName += resultData[i].Profile.SpecialtyDetails[j].Specialty.Name;
                                        $scope.data[i].ss.push(resultData[i].Profile.SpecialtyDetails[j].Specialty.Name);

                                    } else {
                                        $scope.data[i].SpecialtyName += ", " + resultData[i].Profile.SpecialtyDetails[j].Specialty.Name;
                                        $scope.data[i].ss.push(resultData[i].Profile.SpecialtyDetails[j].Specialty.Name);

                                    }
                                }
                            }
                            $scope.data[i].SpecialtyName = "";
                            $scope.data[i].ss = $scope.squash($scope.data[i].ss);
                            for (var ll = 0; ll < $scope.data[i].ss.length; ll++) {
                                if (ll == 0) {
                                    $scope.data[i].SpecialtyName = "";
                                    $scope.data[i].SpecialtyName = $scope.data[i].ss[ll];
                                } else {
                                    $scope.data[i].SpecialtyName += ", " + $scope.data[i].ss[ll];
                                }
                            }
                            $scope.data[i].ss = "";


                            for (var j = 0; j < resultData[i].Profile.ContractInfoes.length; j++) {
                                for (var k = 0; k < resultData[i].Profile.ContractInfoes[j].ContractGroupInfoes.length; k++) {
                                    if (k == 0) {
                                        if (resultData[i].Profile.ContractInfoes[j].ContractGroupInfoes[k].Status != "Inactive") {
                                            $scope.data[i].GroupName = resultData[i].Profile.ContractInfoes[j].ContractGroupInfoes[k].PracticingGroup.Group.Name;
                                            $scope.data[i].ip.push(resultData[i].Profile.ContractInfoes[j].ContractGroupInfoes[k].PracticingGroup.Group.Name);

                                        }
                                        //else {
                                        //    $scope.data[i].GroupName = "";
                                        //    //$scope.data[i].ip.push(resultData[i].Profile.ContractInfoes[j].ContractGroupInfoes[k].PracticingGroup.Group.Name);
                                        //}
                                    }
                                    else if (resultData[i].Profile.ContractInfoes[j].ContractGroupInfoes[k].Status != "Inactive") {
                                        if ($scope.data[i].GroupName == "") {
                                            $scope.data[i].GroupName += resultData[i].Profile.ContractInfoes[j].ContractGroupInfoes[k].PracticingGroup.Group.Name;
                                            $scope.data[i].ip.push(resultData[i].Profile.ContractInfoes[j].ContractGroupInfoes[k].PracticingGroup.Group.Name);
                                        } else {
                                            $scope.data[i].GroupName += ", " + resultData[i].Profile.ContractInfoes[j].ContractGroupInfoes[k].PracticingGroup.Group.Name;
                                            $scope.data[i].ip.push(resultData[i].Profile.ContractInfoes[j].ContractGroupInfoes[k].PracticingGroup.Group.Name);
                                        }
                                    }

                                }
                            }
                            $scope.data[i].GroupName = "";
                            $scope.data[i].ip = $scope.squash($scope.data[i].ip);
                            for (var ll = 0; ll < $scope.data[i].ip.length; ll++) {
                                if (ll == 0) {
                                    $scope.data[i].GroupName = "";
                                    $scope.data[i].GroupName = $scope.data[i].ip[ll];
                                } else {
                                    $scope.data[i].GroupName += ", " + $scope.data[i].ip[ll];
                                }
                            }
                            $scope.data[i].ip = "";
                        }
                        $scope.init_table($scope.data, $scope.selectedAction);
                        if (localStorage.getItem('NavigateToInitiateRecredential') == 'true') {
                            //$scope.Req_Data = $scope.ChangeFormat($scope.Req_Data);
                            $scope.SelectProviderForReCredentialingInitiation($scope.Req_Data);
                            $scope.showPlanListReCred($scope.localdata.License.PlanID);

                        }
                        //-----start prepare recred data for filter--------
                    }
                    if ($scope.selectedAction == 3) {

                        for (var i = 0; i < resultData.length ; i++) {
                            var GroupName = '';
                            for (var m = 0; m < resultData[i].Profile.ContractInfoes.length; m++) {
                                //if (resultData[i].Profile.ContractInfoes[m].ContractGroupInfoes.length != 0) {
                                //    GroupName = resultData[i].Profile.ContractInfoes[m].ContractGroupInfoes[0].PracticingGroup.Group.Name;

                                //    for (var n = 1; n < resultData[i].Profile.ContractInfoes[m].ContractGroupInfoes.length; n++) {

                                //        GroupName = GroupName + ', ' + resultData[i].Profile.ContractInfoes[m].ContractGroupInfoes[n].PracticingGroup.Group.Name;

                                //    }

                                //}
                                // changed to fix the bug
                                for (var k = 0; k < resultData[i].Profile.ContractInfoes[m].ContractGroupInfoes.length; k++) {
                                    if (k == 0) {
                                        if (resultData[i].Profile.ContractInfoes[m].ContractGroupInfoes[k].Status != "Inactive") {
                                            GroupName = resultData[i].Profile.ContractInfoes[m].ContractGroupInfoes[k].PracticingGroup.Group.Name;

                                        }
                                    }
                                    else if (resultData[i].Profile.ContractInfoes[m].ContractGroupInfoes[k].Status != "Inactive") {
                                        if (GroupName == "") {
                                            GroupName += resultData[i].Profile.ContractInfoes[m].ContractGroupInfoes[k].PracticingGroup.Group.Name;
                                        } else {
                                            GroupName += ", " + resultData[i].Profile.ContractInfoes[m].ContractGroupInfoes[k].PracticingGroup.Group.Name;
                                        }
                                    }

                                }
                            }

                            resultData[i].GroupName = GroupName;

                            resultData[i].NPINumber = resultData[i].Profile.OtherIdentificationNumber.NPINumber;
                            //if (resultData[i].Profile.PersonalDetail.ProviderTitles.length != 0) {
                            //    resultData[i].Title = resultData[i].Profile.PersonalDetail.ProviderTitles[0].ProviderType.Title;
                            //    for (var j = 1; j < resultData[i].Profile.PersonalDetail.ProviderTitles.length ; j++) {

                            //        resultData[i].Title = resultData[i].Title + ", " + resultData[i].Profile.PersonalDetail.ProviderTitles[j].ProviderType.Title;

                            //    }
                            //}

                            if (resultData[i].Profile.PersonalDetail.ProviderTitles.length != 0) {
                                for (var k in resultData[i].Profile.PersonalDetail.ProviderTitles) {
                                    if (resultData[i].Profile.PersonalDetail.ProviderTitles[k].Status == 'Active') {
                                        var countfortitle = 0;
                                        resultData[i].Title = "";
                                        if (countfortitle == 0) {
                                            resultData[i].Title += resultData[i].Profile.PersonalDetail.ProviderTitles[k].ProviderType.Title;
                                        } else {
                                            resultData[i].Title += ", " + resultData[i].Profile.PersonalDetail.ProviderTitles[k].ProviderType.Title;
                                        }
                                        countfortitle++
                                    }
                                }
                            }

                            //if (resultData[i].Profile.SpecialtyDetails.length != 0) {
                            //    resultData[i].SpecialtyName = resultData[i].Profile.SpecialtyDetails[0].Specialty.Name;

                            //    for (var j = 1; j < resultData[i].Profile.SpecialtyDetails.length; j++) {

                            //        resultData[i].SpecialtyName = resultData[i].SpecialtyName + ", " + resultData[i].Profile.SpecialtyDetails[j].Specialty.Name;

                            //    }
                            //}
                            var count = 0;
                            if (resultData[i].Profile.SpecialtyDetails.length != 0) {
                                for (var k in resultData[i].Profile.SpecialtyDetails) {
                                    if (resultData[i].Profile.SpecialtyDetails[k].Status == 'Active') {
                                        if (count == 0) {
                                            resultData[i].SpecialtyName = "";
                                            resultData[i].SpecialtyName += resultData[i].Profile.SpecialtyDetails[k].Specialty.Name;
                                        } else {
                                            resultData[i].SpecialtyName += ", " + resultData[i].Profile.SpecialtyDetails[k].Specialty.Name;
                                        }
                                        count++
                                    }
                                }
                            }

                            resultData[i].Name = resultData[i].Profile.PersonalDetail.FirstName + ' ' + resultData[i].Profile.PersonalDetail.LastName;
                            //for (var j = 0; j < resultData[i].Profile.ContractInfoes.length; j++) {
                            //    for (var k = 0; k < resultData[i].Profile.ContractInfoes[j].ContractGroupInfoes.length; k++) {
                            //        if (k == 0) {

                            //            resultData[i].GroupName = resultData[i].Profile.ContractInfoes[j].ContractGroupInfoes[k].PracticingGroup.Group.Name;
                            //        } else {
                            //            resultData[i].GroupName = ", " + resultData[i].Profile.ContractInfoes[j].ContractGroupInfoes[k].PracticingGroup.Group.Name;
                            //        }
                            //    }
                            //}
                        }

                        for (var p = 0; p < resultData.length; p++) {
                            resultData[p].PlanName = resultData[p].Plan.PlanName;
                        }

                        if (resultData.length == 0) {

                            $scope.isFound = false;

                        }

                        $scope.init_table(resultData, $scope.selectedAction);

                    }
                    $scope.tempObject = "";
                    $scope.loadingAjax = false;
                    $('#credsearch, #recredsearch, #decredsearch').removeClass("inactive");
                }
                else {
                    $scope.loadingAjax = false;
                    $('#credsearch, #recredsearch, #decredsearch').removeClass("inactive");
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

    //var FormReset = function ($form) {

    //    // get validator object
    //    var $validator = $form.validate();

    //    // get errors that were created using jQuery.validate.unobtrusive
    //    var $errors = $form.find(".field-validation-error span");

    //    // trick unobtrusive to think the elements were successfully validated
    //    // this removes the validation messages
    //    $errors.each(function () {
    //        $validator.settings.success($(this));
    //    });
    //    // clear errors from validation
    //    $validator.resetForm();
    //};


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
        $scope.Specilities = provObj.Specialties;
        $scope.Groups = provObj.IPAGroupNames;


        $http.get(rootDir + '/MasterDataNew/GetAllPlans').
                 success(function (data, status, headers, config) {

                     try {

                         $scope.plans = data;

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
                            if (inititatedDate != null)
                                currObj.InitiationDate = $filter('date')(new Date(inititatedDate), 'MM-dd-yyyy');
                            else
                                currObj.InitiationDate = "";
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

    //$scope.SelectProvidersForReCredentialingInitiation = function (obj) {

    //    $scope.showInit = true;
    //    $scope.SearchProviderPanelToggleDown('InitiationPanel');
    //    $scope.initiateSuccess = true;
    //    $scope.reCredData = provObj;


    //};

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
                        $scope.initiateSuccess = true;
                        sessionStorage.setItem('CreListId', 1);
                        $scope.credinfoId = data.credentialingInfo.CredentialingInfoID;
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

    //Dummy Json data for reasons 
    //$scope.reasonsForDeCred = [{ reasonID: 1, reason: 'Due to professional libility' }, {reasonID: 2, reason: 'Due to Malpractice during his education' }];

    //$scope.reasonsForDeCred = [];
    //$http.get(rootDir + "/MasterDataNew/GetAllDecredentialingReasons").
    //     success(function (data, status, headers, config) {
    //         try {
    //             if (data != null) {
    //                 $scope.reasonsForDeCred = data;
    //             }                
    //         } catch (e) {
    //         }
    //     }).
    //     error(function (data, status, headers, config) {
    //     });

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
                if (files[i].size > 2000000) {
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

    //var availableTags = [];
    //$('.badge').removeAttr("style");
    //availableTags =
    //    [
    //        'tulasidhar.salla@pratian.com',
    //        'bindupriya.ambati@pratian.com',
    //        'manideep.innamuri@pratian.com',
    //        'sharath.km@pratian.com',
    //        'shalabh@pratian.com'
    //    ];

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

});

$(document).keypress(function (e) {
    if (e.which == 13) {
        $("#searchbtn").click();
    }
});
