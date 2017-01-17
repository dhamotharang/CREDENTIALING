
profileApp.controller('OrgInfoAppCtrl', ['$scope','$rootScope','masterDataService','$http','messageAlertEngine', function ($scope,$rootScope,masterDataService,$http,messageAlertEngine) {
    
    $rootScope.$on('ContractInfoes', function (event, val) {
        $scope.contractInfoes = val;
            
        //if ((typeof $scope.contractInfoes[0] != "undefined" ) && $scope.contractInfoes[0].JoiningDate != null) {
        //    $scope.contractInfoes[0].JoiningDate = ConvertDateFormat($scope.contractInfoes[0].JoiningDate);
        //    $scope.contractInfoes[0].ExpiryDate = ConvertDateFormat($scope.contractInfoes[0].ExpiryDate);

        //    //for (var i = 0; i < $scope.contractInfoes[0].ContractGroupInfoes.length; i++) {

        //    //    $scope.contractInfoes[0].ContractGroupInfoes[i].JoiningDate = ConvertDateFormat($scope.contractInfoes[0].ContractGroupInfoes[i].JoiningDate);
        //    //    $scope.contractInfoes[0].ContractGroupInfoes[i].ExpiryDate = ConvertDateFormat($scope.contractInfoes[0].ContractGroupInfoes[i].ExpiryDate);
        //    //}

        //}
            //console.log("List of Contract Details Here......................");

        //console.log(val);
    });


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


    //============== Group Master Data=======================
    $rootScope.$on("LoadRequireMasterData", function () {
        masterDataService.getMasterData("/MasterData/Organization/GetGroups").then(function (masterGroups) {
            $scope.masterGroups = masterGroups;
        });
    });
    //====================End=====================

    $scope.EditGroup = function (groupEditDivId, groupInfo, ContractInfoID) {
        $scope.tempObject = "";
        $scope.ContractInfoID = ContractInfoID;
        $rootScope.visibilityControl = groupEditDivId;
        $scope.tempObject = angular.copy(groupInfo);

    };

    $scope.AddContract = function (contractAddDiv) {
   
        $rootScope.operateViewAndAddControl(contractAddDiv);
        $scope.providerStatus = true;
        $scope.showAddGroup = true;
    }

    $scope.operateEditControl = function (sectionValue, obj) {
        $scope.tempObject = "";
        $rootScope.visibilityControl = sectionValue;
        $scope.tempObject = obj;
    }


    $scope.EditContract = function (contractEditDiv, contractEditData) {
     
        $rootScope.operateEditControl(contractEditDiv, contractEditData);
           $scope.providerStatus = false;
    };

    ////----------------------------------saving Contract Data----------------------------

    $scope.showAddGroup = true;

    $scope.AddGroup = function (groupAddDivId, ContractInfoID) {
        $scope.tempObject = "";
        $scope.ContractInfoID = ContractInfoID;
        $rootScope.visibilityControl = groupAddDivId;
        $scope.showAddGroup = false;
    }


    $scope.cancelGroup = function () {
        $scope.tempObject = "";
        $scope.showAddGroup = true;
        $rootScope.operateCancelControl('');
    
      
    }



    $scope.saveContractInformation = function (contractInformation, index) {
        loadingOn();
        var validationStatus;
        var url;
        var formData1;
        var tempGroups;
         

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
                            messageAlertEngine.callAlertMessage("updatedContractGroupInformation" + index, "Contract Group Information updated successfully !!!!", "success", true);                     
                        }


                         
                         FormReset(formData1);


                    } else {
                        messageAlertEngine.callAlertMessage('errorContractGroupInformation', "", "danger", true);
                        $scope.errorContractGroupInformation = data.status.split(",");

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





    //----------------------------------End----------------------------


  

}]);

