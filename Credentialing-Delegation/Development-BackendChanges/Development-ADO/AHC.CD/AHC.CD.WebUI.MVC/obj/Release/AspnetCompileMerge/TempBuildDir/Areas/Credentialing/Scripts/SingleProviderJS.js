
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

initCredApp.controller('singleProviderCtrl', function ($scope, $http, $timeout, $location, $filter, ngTableParams, $rootScope, messageAlertEngine) {


    $scope.data = []; //data in scope is declared
    $scope.progressbar = false;
    $scope.error_message = "";
    $scope.groupBySelected = "none";
    $scope.showInit = false;
    $scope.result = false;
    $scope.isSelected = true;
    $scope.isFound = true;

    $scope.clearAction = function () {
        $scope.SearchProviderPanelToggleDown('SearchProviderPanel');
        $scope.data = "";
        $scope.showInit = false;
        $scope.initiate = false;
    };




    $scope.resetSelection = function (data) {
        var temp = [];
        for (var i in data) {
            temp[i] = false;
        }
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

    //$scope.setActionID2(sessionStorage.getItem('key'));

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
        $scope.initiate = false;

    };

    $http.get(rootDir + '/Profile/MasterData/GetAllProviderTypes').
      success(function (data, status, headers, config) {

          $scope.masterProviderTypes = data;

      }).
      error(function (data, status, headers, config) {
          //console.log("Sorry internal master data cont able to fetch.");
      });



    $http.get(rootDir + '/Profile/MasterData/GetAllSpecialities').
     success(function (data, status, headers, config) {

         $scope.masterSpecialities = data;

     }).
     error(function (data, status, headers, config) {
         //console.log("Sorry internal master data cont able to fetch.");
     });





    $scope.searchCumDropDown = function (divId) {
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

    //============================= Data From Master Data Table  ======================    
    //----------------------------- Get List Of Groups --------------------------    
    $http.get(rootDir + '/MasterData/Organization/GetGroups').
      success(function (data, status, headers, config) {
          $scope.PracticingGroups = data;
      }).
      error(function (data, status, headers, config) {
          //console.log("Sorry internal master data can not able to fetch.");
      });

    //Created function to be called when data loaded dynamically



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
                    $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
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
                    $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                }
            });
        }
        else if (condition == 3) {

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
                    $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                }
            });

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


    //select all in De-cred
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
        $scope.data = [];
        $scope.error_message = "";
        $scope.loadingAjax = true;

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
            if (resultData != null) {
                $scope.SearchProviderPanelToggleDown('SearchProviderResultPanel');
                $scope.result = true;

                $scope.data = resultData;
                $scope.credInfo = resultData;
                $scope.sortedData = [];
                $scope.sortData(resultData);

                if ($scope.selectedAction == 1) {
                    $scope.init_table(resultData, $scope.selectedAction);
                }
                if ($scope.selectedAction == 2) {
                    //-----start prepare recred data for filter--------

                    for (var i = 0; i < resultData.length ; i++) {
                        $scope.data[i].ss = [];
                        $scope.data[i].ip = [];

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
                                    $scope.data[i].ss.push(resultData[i].Profile.SpecialtyDetails[j].Specialty.Name);
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

                        $scope.data[i].FirstName = resultData[i].Profile.PersonalDetail.FirstName;
                        $scope.data[i].LastName = resultData[i].Profile.PersonalDetail.LastName;

                        for (var j = 0; j < resultData[i].Profile.ContractInfoes.length; j++) {
                            for (var k = 0; k < resultData[i].Profile.ContractInfoes[j].ContractGroupInfoes.length; k++) {
                                if (k == 0) {
                                    if (resultData[i].Profile.ContractInfoes[j].ContractGroupInfoes[k].PracticingGroup.Status == "Active") {
                                        $scope.data[i].GroupName = resultData[i].Profile.ContractInfoes[j].ContractGroupInfoes[k].PracticingGroup.Group.Name;
                                        $scope.data[i].ip.push(resultData[i].Profile.ContractInfoes[j].ContractGroupInfoes[k].PracticingGroup.Group.Name);

                                    } else {
                                        $scope.data[i].GroupName = "";
                                        $scope.data[i].ip.push(resultData[i].Profile.ContractInfoes[j].ContractGroupInfoes[k].PracticingGroup.Group.Name);
                                    }
                                } else if (resultData[i].Profile.ContractInfoes[j].ContractGroupInfoes[k].PracticingGroup.Status == "Active") {
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

                    //-----start prepare recred data for filter--------
                }
                if ($scope.selectedAction == 3) {

                    for (var i = 0; i < resultData.length ; i++) {
                        var GroupName = '';
                        for (var m = 0; m < resultData[i].Profile.ContractInfoes.length; m++) {
                            for (var n = 0; n < resultData[i].Profile.ContractInfoes[m].ContractGroupInfoes.length; n++) {
                                GroupName = resultData[i].Profile.ContractInfoes[m].ContractGroupInfoes[n].PracticingGroup.Group.Name + ', ' + GroupName;
                            }

                        }

                        resultData[i].GroupName = GroupName;

                        resultData[i].NPINumber = resultData[i].Profile.OtherIdentificationNumber.NPINumber;

                        for (var j = 0; j < resultData[i].Profile.PersonalDetail.ProviderTitles.length ; j++) {
                            if (j == 0) {

                                if (resultData[i].Profile.PersonalDetail.ProviderTitles[j].ProviderType.Title != "Not Available") {

                                    resultData[i].Title = resultData[i].Profile.PersonalDetail.ProviderTitles[j].ProviderType.Title;

                                } else {

                                    resultData[i].Title = "";

                                }
                            }
                            else {

                                if (resultData[i].Profile.PersonalDetail.ProviderTitles[j].ProviderType.Title != "Not Available") {

                                    resultData[i].Title = ", " + resultData[i].Profile.PersonalDetail.ProviderTitles[j].ProviderType.Title;

                                } else {

                                    resultData[i].Title = ", " + "";

                                }
                            }
                        }

                        for (var j = 0; j < resultData[i].Profile.SpecialtyDetails.length; j++) {
                            if (j == 0) {
                                resultData[i].SpecialtyName = resultData[i].Profile.SpecialtyDetails[j].Specialty.Name;
                            }
                            else {
                                resultData[i].SpecialtyName = resultData[i].SpecialtyName + ", " + resultData[i].Profile.SpecialtyDetails[j].Specialty.Name;
                            }
                        }
                        resultData[i].FirstName = resultData[i].Profile.PersonalDetail.FirstName;
                        resultData[i].LastName = resultData[i].Profile.PersonalDetail.LastName;
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
            }
            else {
                $scope.loadingAjax = false;
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
        }).error(function () { $scope.loadingAjax = false; $scope.error_message = "An Error occured !! Please Try Again !!"; })

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

        //console.log($scope.sortedData);

    }

    $scope.SearchProviderPanelToggleDown = function (divId) {
        $(".closePanel").slideUp();
        $("#" + divId).slideToggle();
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

    $scope.SelectProviderForCredentialingInitiation = function (provObj) {
        $scope.showInit = true;

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

                     $scope.plans = data;
                     //console.log($scope.plans);
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
    $scope.NotSelected = [];
    $scope.toggleSelection = function toggleSelection(CredentialingContractRequests) {
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

    $scope.isHasError = false;
    $scope.PlanListID = null;
    $scope.showPlanListReCred = function (Plan) {
        //console.log(Plan);
        $scope.isHasError = false;
        $scope.loadingAjax1 = true;
        $scope.PlanListID = Plan;
        $scope.selection = [];
        $scope.CredentialingContractRequests = [];
        var ProviderID = $scope.ProviderIDLIST;

        $http.get(rootDir + '/Credentialing/Initiation/getCredentialingContractRequest?ProviderID=' + ProviderID + '&PlanID=' + Plan).
            success(function (data, status, headers, config) {
                if (data.status == true) {
                    $scope.Credinfo = angular.copy(data.data1);
                    //console.log($scope.Credinfo);
                    for (var i = 0; i < $scope.Credinfo.length; i++) {
                        for (var j = 0; j < $scope.Credinfo[i].CredentialingContractRequests.length; j++) {
                            if ($scope.Credinfo[i].CredentialingContractRequests[j].Status == 'Active' && $scope.Credinfo[i].CredentialingContractRequests[j].ContractRequestStatus == 'Active') {
                                $scope.CredentialingContractRequests.push($scope.Credinfo[i].CredentialingContractRequests[j]);
                            }
                        }
                    }

                    $scope.loadingAjax1 = false;
                    //console.log("OOOOHHH");
                    if ($scope.CredentialingContractRequests.length == 0) {
                        messageAlertEngine.callAlertMessage('errorInitiated', "", "danger", true);
                        $scope.errorInitiated = "Provider is not credentialed for any plan";
                    }
                    //console.log($scope.CredentialingContractRequests);
                    $scope.NotSelected = angular.copy($scope.CredentialingContractRequests);
                    //console.log($scope.CredentialingContractRequests);
                }
            }).error(function (data, status, headers, config) {

            });

    }
    $scope.ProviderIDLIST = null;
    $scope.CredentialingContractRequests = [];
    $scope.SelectProviderForReCredentialingInitiation = function (provObj) {
        $scope.CredentialingContractRequests = [];
        $scope.isHasError = false;
        $scope.showInit = true;
        $scope.SearchProviderPanelToggleDown('InitiationPanel');
        $scope.initiateSuccess = false;
        $scope.reCredData = provObj;
        //console.log($scope.reCredData);
        $scope.ProviderIDLIST = $scope.reCredData.ProfileID;
        var ProviderID = $scope.reCredData.ProfileID;
        var PlanID = $scope.reCredData.PlanID;
        $http.get(rootDir + '/Credentialing/Initiation/getPlanListforCredentialingContractRequest?ProviderID=' + ProviderID).
            success(function (data, status, headers, config) {
                if (data.status == true) {
                    $scope.PlanList = angular.copy(data.data1);
                    //console.log($scope.PlanList);
                }
            }).error(function (data, status, headers, config) {

            });



    };



    $scope.SelectProviderPlanDetail = function (obj) {

        // $scope.SearchProviderPanelToggleDown('ProfileDetailPanel');
        $scope.initiateSuccess = false;
        $scope.initiateComplete = false;
        $scope.IsMessage = false;
        $scope.initiate = true;
        $scope.profileDetail = [];
        //$scope.fillObj(obj);
        console.log('cred axis');
        console.log(obj);
        //edited by pritam
        for (var i = 0; i < obj.CredentialingContractRequests.length; i++) {
            for (var j = 0; j < obj.CredentialingContractRequests[i].ContractGrid.length; j++) {
                var selectedObj = obj.CredentialingContractRequests[i].ContractGrid[j];
                //$scope.profileDetail.push(selectedObj);
                var currObj = new Object();
                currObj.CredentialingInfoID = obj.CredentialingInfoID;
                currObj.CredentialingContractRequestID = obj.CredentialingContractRequests[i].CredentialingContractRequestID;
                currObj.PlanName = obj.Plan.PlanName;
                currObj.Speciality = selectedObj.ProfileSpecialty.Specialty.Name;
                currObj.LOB = selectedObj.LOB.LOBName;
                currObj.Location = selectedObj.ProfilePracticeLocation.Facility.FacilityName;
                currObj.GroupName = obj.GroupName;
                currObj.Check = false;
                currObj.InitiationDate = $scope.ConvertDateFormat(obj.InitiationDate);
                currObj.ContractGridID = selectedObj.ContractGridID;
                $scope.profileDetail.push(currObj);
            }
        }

        if ($scope.profileDetail.length != 0) {
            $scope.SearchProviderPanelToggleDown('ProfileDetailPanel');
            $scope.init_table($scope.profileDetail, 4);
        } else {

            messageAlertEngine.callAlertMessage('errorSpecialty', "No Plan Reports Available for the Given Option", "danger", true);
            $("body").animate({ scrollTop: $("#errorMsg").offset().top });
        }



        //var tabhighlight = "initCred";
        //$('a[href=#' + tabhighlight + ']').trigger('click');
        $scope.TwoDocuments = false;

    }

    //$scope.SelectProvidersForReCredentialingInitiation = function (obj) {

    //    $scope.showInit = true;
    //    $scope.SearchProviderPanelToggleDown('InitiationPanel');
    //    $scope.initiateSuccess = true;
    //    $scope.reCredData = provObj;


    //};
    $scope.profileDetail = [];

    $scope.CredInfoIdArray = [];
    $scope.CredContractIdArray = [];
    $scope.ContractGridID = [];

    $scope.createIdArray = function () {

        //console.log($scope.profileDetail);
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

        //console.log($scope.IdArray);

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

        //console.log($scope.profileDetail);

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
            }).
            error(function (data, status, headers, config) {
                //----------- error message -----------
                messageAlertEngine.callAlertMessage('errorInitiated', "", "danger", true);
                $scope.errorInitiated = "Sorry for Inconvenience !!!! Please Try Again Later...";
            });
    }

    //Convert the date from database to normal

    $scope.ConvertDateFormat = function (value) {
        ////console.log(value);
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
              $scope.data = data;
              for (var i = 0; i < $scope.data.length ; i++) {
                  if ($scope.data[i].CredentialingInfoID == $scope.tempID) {
                      $scope.ProfileID = $scope.data[i].ProfileID;
                      $scope.delegatePlan = $scope.data[i].Plan.DelegatedType;
                  }                
              }
              console.log('recred data');
              console.log($scope.data);
          }).
          error(function (data, status, headers, config) {
              console.log("Error fetching data");
          });
    }

    $scope.InitiateReCredentialing = function (provObj) {
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
        var NotSelectedID = [];
        $scope.CredentialingContractRequestsIntArray = [];
        $scope.NonCredentialingContractRequestsIntArray = [];
        for (var i = 0; i < $scope.selection.length; i++) {
            $scope.CredentialingContractRequestsIntArray[i] = $scope.selection[i].CredentialingContractRequestID;
        }
        for (var i = 0; i < $scope.NotSelected.length; i++) {
            var count = 0;
            for (var j = 0; j < $scope.selection.length; j++) {
                if ($scope.NotSelected[i].CredentialingContractRequestID == $scope.selection[j].CredentialingContractRequestID) {
                    count++;
                }
            }
            if (count == 0) {
                NotSelectedID.push($scope.NotSelected[i].CredentialingContractRequestID);
            }
        }
        //console.log($scope.CredentialingContractRequestsIntArray);
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
                    CredentialingContractRequestsArray: angular.copy($scope.CredentialingContractRequestsIntArray),
                    NonCredentialingContractRequestsArray: angular.copy(NotSelectedID)
                }
            }).success(function (data, status, headers, config) {
                //----------- success message -----------
                if (data.status == "true") {
                    $scope.initiateSuccess = true;
                    $scope.initiateComplete = true;
                    $scope.IsMessage = true;
                    sessionStorage.setItem('CreListId', 2);
                    $scope.tempID = data.ID;
                    $scope.setCredInfo($scope.tempID);

                    console.log('rreeccrreedd data');
                    console.log(data);
                    $scope.getReCredList();

                    

                    console.log($scope.tempID);
                    
                    messageAlertEngine.callAlertMessage('successfulInitiated', "ReCredentialing Initiated Successfully. !!!!", "success", true);
                    
                }
                else {
                    messageAlertEngine.callAlertMessage('errorInitiated', "", "danger", true);
                    $scope.errorInitiated = data.status.split(",");
                }
            }).error(function (data, status, headers, config) {
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

    $scope.checkMe = function (status) {

        if (status == false) {
            $scope.IsAllChecked = false;
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

    $scope.InitiateDeCredentialing = function () {
        $scope.createIdArray();
        var Infoid = $scope.CredInfoIdArray;
        var ContractId = $scope.CredContractIdArray;
        var GridId = $scope.ContractGridID;
        $scope.ContractGridID = [];
        $scope.CredContractIdArray = [];
        $scope.CredInfoIdArray = [];
        //var id = provObj.CredentialingInfoID;
        //var obj2 = provObj.LoadedContracts[0].LoadedContractID;
        //var obj2 = {
        //    //LoadedContractID: provObj.LoadedContracts[0].LoadedContractID,
        //    //LoadedByID: provObj.LoadedContracts[0].LoadedByID,
        //    //LoadedDate: $scope.ConvertDateFormat(provObj.LoadedContracts[0].LoadedDate),
        //    //CredentialingType: provObj.LoadedContracts[0].CredentialingType,
        //    //BusinessEntityID: provObj.LoadedContracts[0].BusinessEntityID,
        //    //SpecialtyID: provObj.LoadedContracts[0].SpecialtyID,
        //    //CredentialingRequestStatusType: provObj.LoadedContracts[0].CredentialingRequestStatusType,
        //    //LOBID: provObj.LoadedContracts[0].LOBID
        //    CredentialingType: provObj.CredentialingLogs[0].CredentialingType
        //};
        //console.log('profile Detail');
        //console.log(GridId);
        //$scope.tempobject.ffll = {};
        $http({
            method: "POST",
            url: rootDir + "/Credentialing/Initiation/InitiateDeCredentialing",
            data: {
                InfoidArray: Infoid,
                ContractidArray: ContractId,
                GrididArray: GridId,
            }
        }).success(function (data, status, headers, config) {
            //----------- success message -----------
            if (data.status == "true") {
                $scope.ProfileID = data.ID;
                $scope.initiateSuccess = true;
                $scope.initiateComplete = true;
                $scope.IsMessage = true;
                $timeout(function () {
                    $scope.IsMessage = false;
                }, 1000);

                //$scope.resetForm();
                //window.MyDocument.reset();
                //FormReset($form);

                messageAlertEngine.callAlertMessage('successfulInitiated', "DeCredentialing Initiated Successfully. !!!!", "success", true);
            }
            else {
                messageAlertEngine.callAlertMessage('errorInitiated', "", "danger", true);
                $scope.errorInitiated = data.status.split(",");
            }
        }).error(function (data, status, headers, config) {
            //----------- error message -----------
            messageAlertEngine.callAlertMessage('errorInitiated', "", "danger", true);
            $scope.errorInitiated = "Sorry for Inconvenience !!!! Please Try Again Later...";
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

    //===========================================================================================
    $scope.popCount = 0;

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

});