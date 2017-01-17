profileApp.controller('OrgInfoAppCtrl', ['$scope', '$rootScope', 'masterDataService', '$http', 'messageAlertEngine', '$filter', function ($scope, $rootScope, masterDataService, $http, messageAlertEngine, $filter) {
    
    $rootScope.$on('ContractInfoes', function (event, val) {
        $scope.contractInfoes = val;
        var tempGroups = [];
        for (var i = 0; i < $scope.contractInfoes[0].ContractGroupInfoes.length; i++) {
            if ($scope.contractInfoes[0].ContractGroupInfoes[i].Status != 'Inactive') {
                tempGroups.push($scope.contractInfoes[0].ContractGroupInfoes[i]);
            }
        }
        $scope.contractInfoes[0].ContractGroupInfoes = tempGroups;
        //console.log(val);

        //if ((typeof $scope.contractInfoes[0] != "undefined" ) && $scope.contractInfoes[0].JoiningDate != null) {
        //    //$scope.contractInfoes[0].JoiningDate = ConvertDateFormat($scope.contractInfoes[0].JoiningDate);
        //    //$scope.contractInfoes[0].ExpiryDate = ConvertDateFormat($scope.contractInfoes[0].ExpiryDate);
        //
        //    for (var i = 0; i < $scope.contractInfoes[0].ContractGroupInfoes.length; i++) {
        //
        //        $scope.contractInfoes[0].ContractGroupInfoes[i].JoiningDate = ConvertDateFormat($scope.contractInfoes[0].ContractGroupInfoes[i].JoiningDate);
        //        $scope.contractInfoes[0].ContractGroupInfoes[i].ExpiryDate = ConvertDateFormat($scope.contractInfoes[0].ContractGroupInfoes[i].ExpiryDate);
        //    }
        //}
    });

    //Get all the data for the ContractInfo on document on ready
    //$(document).ready(function () {
    //    $("#Loading_Message").text("Gathering Profile Data...");
    //    //console.log("Getting data....");
    //    $http({
    //        method: 'GET',
    //        url: '/Profile/MasterProfile/GetContractInfoProfileDataAsync?profileId=' + profileId
    //    }).success(function (data, status, headers, config) {
    //        //console.log(data);
    //        try {
    //            for (key in data) {
    //                //console.log(key);
    //                $rootScope.$emit(key, data[key]);
    //                //call respective controller to load data (PSP)
    //            }
                
    //            $rootScope.ContractInformationLoaded = true;
    //            //$rootScope.$broadcast("LoadRequireMasterData");
    //        } catch (e) {
    //            //console.log("error getting data back");
    //            $rootScope.ContractInformationLoaded = true;
    //        }

    //    }).error(function (data, status, headers, config) {
    //        //console.log(status);
    //        $rootScope.ContractInformationLoaded = true;
    //    });
    //});


    $scope.EnableContract = function(){
        try{
            if ($scope.contractInfoes == "undefined") {

                return false;
            }
            else if ($scope.contractInfoes != "undefined") {
                if ($scope.contractInfoes[0] != "undefined") {

                    if($scope.contractInfoes[0].ContractStatus == 'Inactive')
                        return false;
                }
            }
            return true;
        }
        catch(e){
        
        
        }
}


    //....................Group Information History............................//
    $scope.groupHistoryArray = [];

    $scope.showGroupHistory = function (loadingId) {
        if ($scope.groupHistoryArray.length == 0) {
            $("#" + loadingId).css('display', 'block');
            var url = "/Profile/ProfileHistory/GetAllContractGroupInfoHistory?profileId=" + profileId;
            $http.get(url).success(function (data) {
                $scope.groupHistoryArray = data;
                $scope.showGroupHistoryTable = true;
                $("#" + loadingId).css('display', 'none');
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

    //============== Group Master Data=======================
    $rootScope.$on("LoadRequireMasterDataContractInformation", function () {
        masterDataService.getMasterData("/MasterData/Organization/GetGroups").then(function (masterGroups) {
            $scope.masterGroups = masterGroups;
        });
    });
    //====================End=====================

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
        $scope.ContractInfoID = ContractInfoID;
        $rootScope.visibilityControl = groupAddDivId;
        $scope.showAddGroup = false;
    }


    $scope.cancelGroup = function () {
       // $scope.tempObject = "";
        $scope.showAddGroup = true;
        $rootScope.operateCancelControl('');
    }


    $scope.saveContractInformation = function (contractInformation, index) {
        loadingOn();
        var validationStatus;
        var url;
        var formData1;
        var tempGroups;

        //console.log(contractInformation);

        tempGroups = contractInformation.ContractGroupInfoes;
        
            if ($scope.visibilityControl == 'addci') {
                //Add Details - Denote the URL
                formData1 = $('#ContractInformationAddDiv').find('form');
                url = "/Profile/Contract/AddContractInformation?profileId=" + profileId;
            }
            else if ($scope.visibilityControl == ( '_editci')) {
                //Update Details - Denote the URL
                formData1 = $('#ContractInformationEditDiv').find('form');
                url = "/Profile/Contract/UpdateContractInformation?profileId=" + profileId;
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

                    if (data.status == "true") {
                        data.contractInformation.JoiningDate = ConvertDateFormat(data.contractInformation.JoiningDate);
                        data.contractInformation.ExpiryDate = ConvertDateFormat(data.contractInformation.ExpiryDate);

                        if ($scope.visibilityControl != ('_editci')) {
                            $scope.contractInfoes[0] = data.contractInformation;
                            $rootScope.operateCancelControl('');
                            messageAlertEngine.callAlertMessage("addedNewContractInformation", "Contract  Information added successfully !!!!", "success", true);
                        }


                        else {
                            $scope.contractInfoes[0] = data.contractInformation;
                            $scope.contractInfoes[0].ContractGroupInfoes = tempGroups;
                            $rootScope.operateCancelControl('');
                            messageAlertEngine.callAlertMessage("updatedContractInformation", "Contract Information updated successfully !!!!", "success", true);

                        }
                        FormReset(formData1);
                    }
                       else {
                        messageAlertEngine.callAlertMessage('errorContractInformation', "", "danger", true);
                        $scope.errorContractInformation = data.status.split(",");
    
                    }
                },
                error: function (e) {
                    messageAlertEngine.callAlertMessage('errorContractInformation' + index, "", "danger", true);
                    $scope.errorContractInformation = "Sorry for Inconvenience !!!! Please Try Again Later...";
                }
            });

        }
        loadingOff();
    };

    //----------------------------------End----------------------------

    ////----------------------------------saving Group Data----------------------------

    $scope.saveGroupInformation = function (contractGroupInformation,editDivId, index) {
        loadingOn();
        var validationStatus;
        var url;
        var formData1;
        var tempPractisingGroups;
        for (var i =0;i<$scope.masterGroups.length;i++) {

            if ($scope.masterGroups[i].PracticingGroupID == parseInt(contractGroupInformation.PracticingGroupId)) {
                tempPractisingGroups = $scope.masterGroups[i];
            }
        }

        if ($scope.visibilityControl == 'addgi') {
            //Add Details - Denote the URL
            formData1 = $('#newContractInformationDiv').find('form');
            url = "/Profile/Contract/AddContractGroupInformation?profileId=" + profileId + "&contractInfoId=" + $scope.ContractInfoID;
        }
        else if ($scope.visibilityControl == (index + '_editgi')) {
            //Update Details - Denote the URL
            formData1 = $('#ContractGroupInformationEditDiv' + index).find('form');
            url = "/Profile/Contract/UpdateContractGroupInformation?profileId=" + profileId + "&contractInfoId=" + $scope.ContractInfoID;
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

                    if (data.status == "true") {

                        data.contractGroupInformation.JoiningDate = ConvertDateFormat(data.contractGroupInformation.JoiningDate);
                        data.contractGroupInformation.ExpiryDate = ConvertDateFormat(data.contractGroupInformation.ExpiryDate);
                    
                        data.contractGroupInformation.PracticingGroup = tempPractisingGroups;
                     
                        if ($scope.visibilityControl != (index + '_editgi')) {
                            $scope.contractInfoes[0].ContractGroupInfoes.push(data.contractGroupInformation);
                            $rootScope.operateCancelControl('');
                            messageAlertEngine.callAlertMessage("addedNewContractGroupInformation", "Contract Group Information saved successfully !!!!", "success", true);
                        }

                        else {
                            $scope.contractInfoes[0].ContractGroupInfoes[index] = data.contractGroupInformation;
                            $rootScope.operateViewAndAddControl(index + '_viewgi');
                            //$rootScope.operateCancelControl('');
                            messageAlertEngine.callAlertMessage("updatedContractGroupInformation" + index, "Contract Group Information updated successfully !!!!", "success", true);
                        }
                        FormReset(formData1);
                    } else {
                        messageAlertEngine.callAlertMessage('errorContractGroupInformation', "", "danger", true);
                        $scope.errorContractGroupInformation = data.status.split(",");

                    }
                    //console.log($scope.contractInfoes);

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
        if (angular.isObject(gi)) {
            $scope.tempGroupInfo = gi;
        }
        $('#groupInfoWarningModal').modal();
    };

    $scope.removeGroupInfo = function (groupInfo, ContractInfoID) {
        var validationStatus = false;
        var url = null;
        var $formData = null;
        $formData = $('#removeGroupInfo');
        url = "/Profile/Contract/RemoveContractGroupInformationAsync?profileId=" + profileId + "&contractInfoId=" + ContractInfoID;
        ResetFormForValidation($formData);
        //console.log($formData);
        validationStatus = $formData.valid();

        if (validationStatus) {
            //Simple POST request example (passing data) :
            $.ajax({
                url: url,
                type: 'POST',
                data: new FormData($formData[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    //console.log(data.status);
                    if (data.status == "true") {
                        var obj = $filter('filter')($scope.contractInfoes[0].ContractGroupInfoes, { ContractGroupInfoId: data.groupInfo.ContractGroupInfoId })[0];
                        $scope.contractInfoes[0].ContractGroupInfoes.splice($scope.contractInfoes[0].ContractGroupInfoes.indexOf(obj), 1);
                        if ($scope.groupHistoryArray.length != 0) {
                            obj.HistoryStatus = "Deleted";
                            $scope.groupHistoryArray.push(obj);
                        }
                        $('#groupInfoWarningModal').modal('hide');
                        $rootScope.operateCancelControl('');
                        messageAlertEngine.callAlertMessage("addedNewContractGroupInformation", "Group Information Removed successfully.", "success", true);
                       
                    } else {
                        $('#groupInfoWarningModal').modal('hide');
                        messageAlertEngine.callAlertMessage("removeContractGroupInformation", data.status, "danger", true);
                        $scope.errorContractGroupInformation = "Sorry for Inconvenience !!!! Please Try Again Later...";
                    }
                    $scope.showAddGroup = true;
                },
                error: function (e) {

                }
            });
        }
        $scope.masterGroups;
    };


    //----------------------------------End----------------------------
    $rootScope.ContractInformationLoaded = true;
    $scope.dataLoaded = false;
    $rootScope.$on('ContractInformation', function () {
        if (!$scope.dataLoaded) {
            $rootScope.ContractInformationLoaded = false;
            //console.log("Getting data....");
            $http({
                method: 'GET',
                url: '/Profile/MasterProfile/GetContractInfoProfileDataAsync?profileId=' + profileId
            }).success(function (data, status, headers, config) {
                //console.log(data);
                try {
                    for (key in data) {
                        //console.log(key);
                        $rootScope.$emit(key, data[key]);
                        //call respective controller to load data (PSP)
                    }

                    $rootScope.ContractInformationLoaded = true;
                    $rootScope.$broadcast("LoadRequireMasterDataContractInformation");
                } catch (e) {
                    //console.log("error getting data back");
                    $rootScope.ContractInformationLoaded = true;
                }

            }).error(function (data, status, headers, config) {
                //console.log(status);
                $rootScope.ContractInformationLoaded = true;
            });
            $scope.dataLoaded = true;
        }
    });

    
}]);

