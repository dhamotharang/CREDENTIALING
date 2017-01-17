profileApp.controller('CustomFieldController', ['$scope', '$rootScope', '$timeout', '$http', 'messageAlertEngine',
    function ($scope, $rootScope, $timeout, $http, messageAlertEngine) {
        $rootScope.CustomFieldLoaded = false;
        $scope.NoCustomField = false;
        $scope.dataLoaded=false;
        $scope.Nodata = [];
        $scope.transactionID = 0;
        
        $rootScope.$on('CustomField', function () {
            if (!$scope.dataLoaded) {
            $rootScope.CustomFieldLoaded = false;
            $http.get(rootDir + '/Profile/CustomFieldGeneration/GetCustomField').
            success(function (data, status, headers, config) {
                if (data.status == "true") {
                    if (data.ListOfCustomFields.length == 0) {
                        $scope.NoCustomField = true;
                    }
                }
                else {
                    messageAlertEngine.callAlertMessage("CustomFieldError", data.status, "danger", true);
                }
            }).
            error(function (data, status, headers, config) {

            });
            
                if (!$scope.NoCustomField){
                    $http.get(rootDir + '/Profile/CustomFieldGeneration/CustomFieldTransaction?ProfileID=' + profileId).
                    success(function (data, status, headers, config) {
                        if (data.status == "true") {
                            console.log("======CustomFieldTransaction======");
                            console.log(data.CustomFieldTransaction);
                            if (data.CustomFieldTransaction == null) {
                                $http.get(rootDir + '/Profile/CustomFieldGeneration/GetCustomField').
                                success(function (data, status, headers, config) {
                                    if (data.status == "true") {
                                        for (var c = 0; c < data.ListOfCustomFields.length; c++) {
                                            data.ListOfCustomFields[c].displayStatus = false;
                                            data.ListOfCustomFields[c].TitleValue = "";
                                        
                                        }
                                        $scope.ListOfCustomField = angular.copy(data.ListOfCustomFields);
                                        $rootScope.CustomFieldLoaded = true;
                                        console.log($scope.ListOfCustomField);
                                    }
                                    else {
                                        messageAlertEngine.callAlertMessage("CustomFieldError", data.status, "danger", true);
                                    }
                                }).
                                error(function (data, status, headers, config) {

                                });
                            }
                            else {
                                $scope.transactionID = data.CustomFieldTransaction.CustomFieldTransactionID;
                                $http.get(rootDir + '/Profile/CustomFieldGeneration/GetCustomField').
                                success(function (data1, status, headers, config) {
                                    if (data1.status == "true") {
                                        for (var c = 0; c < data1.ListOfCustomFields.length; c++) {
                                            data1.ListOfCustomFields[c].displayStatus = false;
                                            data1.ListOfCustomFields[c].TitleValue = "";
                                        }
                                        for (var i = 0; i < data.CustomFieldTransaction.CustomFieldTransactionDatas.length; i++) {
                                            for (var c = 0; c < data1.ListOfCustomFields.length; c++) {
                                                if (data1.ListOfCustomFields[c].CustomFieldID == data.CustomFieldTransaction.CustomFieldTransactionDatas[i].CustomFieldID) {
                                                    data1.ListOfCustomFields[c].TitleValue = data.CustomFieldTransaction.CustomFieldTransactionDatas[i].CustomFieldTransactionDataValue;
                                                    //data.CustomFieldTransaction.CustomFieldTransactionDatas[i].CustomFieldTransactionDataID = 0;
                                                    $scope.Nodata.push(data.CustomFieldTransaction.CustomFieldTransactionDatas[i]);
                                                    break;
                                                }
                                            }
                                        }
                                        $scope.ListOfCustomField = angular.copy(data1.ListOfCustomFields);
                                        $rootScope.CustomFieldLoaded = true;
                                        console.log($scope.ListOfCustomField);
                                    }
                                    else {
                                        messageAlertEngine.callAlertMessage("CustomFieldError", data.status, "danger", true);
                                    }
                                }).
                                error(function (data, status, headers, config) {

                                });
                            
                            }
                        }
                        else {
                            messageAlertEngine.callAlertMessage("CustomFieldError", data.status, "danger", true);
                        }
                    }).
                    error(function (data, status, headers, config) {

                    });
                }
                
                $scope.dataLoaded = true;
            }
        });
        $scope.AddingCustomFieldValue = function (data) {
            for (var c = 0; c < $scope.ListOfCustomField.length; c++) {
                $scope.ListOfCustomField[c].displayStatus = false;
            }
            
            if ($scope.Nodata.length > 0) {
                for (var i = 0; i < $scope.Nodata.length; i++) {
                    for (var j = 0; j < $scope.ListOfCustomField.length;j++){
                        if ($scope.ListOfCustomField[j].CustomFieldID == $scope.Nodata[i].CustomFieldID) {
                            $scope.ListOfCustomField[j].TitleValue = $scope.Nodata[i].CustomFieldTransactionDataValue;
                            break;
                        }
                    }
                    
                }
            }
            else {
                for (var j = 0; j < $scope.ListOfCustomField.length; j++) {
                        $scope.ListOfCustomField[j].TitleValue = "";
                }
            }
            $scope.ListOfCustomField[$scope.ListOfCustomField.indexOf(data)].displayStatus = true;
        }
        $scope.Cancel = function (data) {
            $scope.ListOfCustomField[$scope.ListOfCustomField.indexOf(data)].displayStatus = false;
            var count = 0;
            if($scope.Nodata.length>0){
                for(var i=0;i<$scope.Nodata.length;i++){
                    if ($scope.ListOfCustomField[$scope.ListOfCustomField.indexOf(data)].CustomFieldID == $scope.Nodata[i].CustomFieldID) {
                        $scope.ListOfCustomField[$scope.ListOfCustomField.indexOf(data)].TitleValue = $scope.Nodata[i].CustomFieldTransactionDataValue;
                        count++;
                        break;
                    }
                }
                if (count==0) {
                    $scope.ListOfCustomField[$scope.ListOfCustomField.indexOf(data)].TitleValue = "";
                }
            }
            else {
                $scope.ListOfCustomField[$scope.ListOfCustomField.indexOf(data)].TitleValue = "";
            }
        }
        $scope.AddCustomField = function (data, CustomFieldID,dataValue) {
            var count=0;
            for(var c=0;c<$scope.Nodata.length;c++){
                if ($scope.Nodata[c].CustomFieldID == CustomFieldID) {
                    $scope.Nodata[c].CustomFieldTransactionDataValue = data;
                    count++;
                    break;
                }
            }
            
            
            
            if (count == 0) {
                var CustomFieldTransactionDataViewModel = {
                    CustomFieldID: CustomFieldID,
                    CustomFieldTransactionDataValue: data,
                    StatusType: 'Active'
                }
                $scope.Nodata.push(CustomFieldTransactionDataViewModel);
            }
            
            
            var customFieldTransactionViewModel = {
                CustomFieldTransactionID: $scope.transactionID,
                CustomFieldTransactionDatas: angular.copy($scope.Nodata)
            }
            $http({
                method: "POST",
                url: rootDir + '/Profile/CustomFieldGeneration/AddCustomFieldTansaction',
                data: {
                    ProfileID: profileId,
                    CustomFieldTransactionViewModel: customFieldTransactionViewModel
                }
            }).
                success(function (data1, status, headers, config) {
                    if (data1.status == "true") {
                        $scope.ListOfCustomField[$scope.ListOfCustomField.indexOf(dataValue)].TitleValue = data;
                        $scope.ListOfCustomField[$scope.ListOfCustomField.indexOf(dataValue)].displayStatus = false;
                        $scope.transactionID = data1.ID;
                        messageAlertEngine.callAlertMessage('CustomFieldSaveSuccess', dataValue.CustomFieldTitle+" Updated Successfully. !!!!", "success", true);

                    }
                    else {
                        messageAlertEngine.callAlertMessage("CustomFieldError", data1.status, "danger", true);
                    }
                }).
                error(function (data, status, headers, config) {

                });


        }
        $rootScope.CustomFieldLoaded = true;
    }])