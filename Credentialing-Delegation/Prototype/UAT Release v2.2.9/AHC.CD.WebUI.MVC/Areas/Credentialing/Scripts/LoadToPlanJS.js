

Cred_SPA_App.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

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
}])

//---------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------check in test---------------------------------------------------------------
//---------------------------------------------------------------------------------------------------------------------------

Cred_SPA_App.controller('LoadToPlanController', function ($scope, $rootScope, $http, $filter, messageAlertEngine) {

    $scope.ShowVisibility = '';
    $scope.LoadedData = [];
    if ($scope.credentialingInfo.CredentialingContractRequests != null) {
        for (var i = 0; i < $scope.credentialingInfo.CredentialingContractRequests.length; i++) {
            if ($scope.credentialingInfo.CredentialingContractRequests[i].Status != 'Inactive') {
                $scope.LoadedData.push($scope.credentialingInfo.CredentialingContractRequests[i]);
            }
        }
    }

    for (var i = 0; i < $scope.LoadedData.length; i++) {

        $scope.LoadedData[i].GroupName = $scope.LoadedData[i].BusinessEntity.GroupName;
        for (var j = 0; j < $scope.LoadedData[i].ContractLOBs.length; j++) {
            if ($scope.LoadedData[i].LOBName == undefined)
            {
                $scope.LoadedData[i].LOBName = '';
                $scope.LoadedData[i].LOBName = $scope.LoadedData[i].ContractLOBs[j].LOB.LOBName;
            }
            else {
                $scope.LoadedData[i].LOBName = $scope.LoadedData[i].LOBName + ", " + $scope.LoadedData[i].ContractLOBs[j].LOB.LOBName;
            }
            
        }
        for (var k = 0; k < $scope.LoadedData[i].ContractSpecialties.length; k++) {
            if ($scope.LoadedData[i].SpecialtyName == undefined) {
                $scope.LoadedData[i].SpecialtyName = '';
                $scope.LoadedData[i].SpecialtyName = $scope.LoadedData[i].ContractSpecialties[k].ProfileSpecialty.Specialty.Name;
            }
            else {
                $scope.LoadedData[i].SpecialtyName = $scope.LoadedData[i].SpecialtyName + ', ' + $scope.LoadedData[i].ContractSpecialties[k].ProfileSpecialty.Specialty.Name;
            } 
        }
        for (var l = 0; l < $scope.LoadedData[i].ContractPracticeLocations.length; l++) {
            if ($scope.LoadedData[i].FacilityName == undefined) {
                $scope.LoadedData[i].FacilityName = '';
                $scope.LoadedData[i].FacilityName = $scope.LoadedData[i].ContractPracticeLocations[l].ProfilePracticeLocation.Facility.FacilityName;
            }
            else {
                $scope.LoadedData[i].FacilityName = $scope.LoadedData[i].FacilityName + ', ' + $scope.LoadedData[i].ContractPracticeLocations[l].ProfilePracticeLocation.Facility.FacilityName;
            } 
        }
    }

    console.log($scope.LoadedData);

    //console.log("profile");
    //console.log($scope.credentialingInfo.ProfileID);
    sessionStorage.setItem('ProfileID', $scope.credentialingInfo.ProfileID);

    $scope.ClearVisibility = function () {
        $scope.ShowVisibility = '';
    }

    $scope.SetVisibility = function (type, index) {
        if (type == 'Qedit') {
            $scope.qTempObject = angular.copy($scope.PlanReportList[index]);
            $scope.ShowVisibility = 'QeditVisibility' + index;
        }
        else if (type == 'edit') {
            $scope.eTempObject = angular.copy($scope.PlanReportList[index]);
            $scope.ShowVisibility = 'editVisibility' + index;
        }
        else if (type == 'view') {
            $scope.ShowVisibility = 'viewVisibility' + index;
        }
    };
    //Convert the date from database to normal
    $rootScope.ConvertDateFormat = function (value) {
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



    //Loaded object initialising
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

    $scope.GetValue = function (data) {
        //  console.log(data);
    }

    //multi select
    $scope.showContryCodeDiv = function (div_Id) {
        $("#LOBdiv").hide();
        $("#Specialitydiv").hide();
        $("#Locationsdiv").hide();
        $("#" + div_Id).show();
    };

   // $http.get('/MasterDataNew/GetAllLobs').
   //success(function (data, status, headers, config) {
   //    $scope.ContractLOBsList = angular.copy(data);
   //    $scope.MasterContractLOBslist = data;
   //}).
   //error(function (data, status, headers, config) {
   //    console.log("Sorry internal master data cont able to fetch.");
   //});
    
    $http.get('/MasterDataNew/GetAllLOBsOfPlanContractByPlanID?planID=' + $scope.credentialingInfo.PlanID).
   success(function (data, status, headers, config) {
       $scope.ContractLOBsList = angular.copy(data);
       $scope.MasterContractLOBslist = data;
   }).
   error(function (data, status, headers, config) {
       //console.log("Sorry internal master data cont able to fetch.");
   });

    //console.log($scope.credentialingInfo);
    $scope.ContractSpecialityList = [];
    for (var i = 0; i < $scope.credentialingInfo.Profile.SpecialtyDetails.length; i++) {
        if ($scope.credentialingInfo.Profile.SpecialtyDetails[i].Status != 'Inactive') {
            $scope.ContractSpecialityList.push($scope.credentialingInfo.Profile.SpecialtyDetails[i]);
        }
    }
    //$scope.ContractSpecialityList = angular.copy($scope.credentialingInfo.Profile.SpecialtyDetails);
    $scope.ContractLocationsList = [];
    for (var i = 0; i < $scope.credentialingInfo.Profile.PracticeLocationDetails.length; i++) {
        if ($scope.credentialingInfo.Profile.PracticeLocationDetails[i].Status != 'Inactive') {
            $scope.ContractLocationsList.push($scope.credentialingInfo.Profile.PracticeLocationDetails[i]);
        }
    }
    console.log("habala hubala habala hubala");
    console.log($scope.ContractLocationsList);
    //$scope.ContractLocationsList = angular.copy($scope.credentialingInfo.Profile.PracticeLocationDetails);

    $http.get('/MasterDataNew/GetAllOrganizationGroupAsync').
    success(function (data, status, headers, config) {
        $scope.BusinessEntities = angular.copy(data);
        $scope.MasterBusinessEntities = data;
    }).
    error(function (data, status, headers, config) {
        //console.log("Sorry internal master data cont able to fetch.");
    });

    //LOB
    $scope.ContractLOBsList = [];
    $scope.tempObject.ContractLOBs = [];
    $scope.SelectLOBName = function (c) {
        $scope.tempObject.ContractLOBs.push({
            LOBID: c.LOBID,
            LOBName: c.LOBName
        });
        $scope.ContractLOBsList.splice($scope.ContractLOBsList.indexOf(c), 1);
        $scope.LOBName = "";
    };
    $scope.RemoveCoveringPhysiciansType = function (c) {
        $scope.tempObject.ContractLOBs.splice($scope.tempObject.ContractLOBs.indexOf(c), 1)
        $scope.ContractLOBsList.push(c);
    };

    // Splty
    $scope.tempObject.ContractSpecialties = [];
    $scope.SelectSpecialityName = function (c) {
        $scope.tempObject.ContractSpecialties.push({
            ProfileSpecialtyID: c.SpecialtyDetailID,
            SpecialtyName: c.Specialty.Name
        });
        $scope.ContractSpecialityList.splice($scope.ContractSpecialityList.indexOf(c), 1);
        $scope.SpecialtyName = "";
    };
    $scope.RemoveContractSpecialties = function (c) {
        $scope.tempObject.ContractSpecialties.splice($scope.tempObject.ContractSpecialties.indexOf(c), 1)
        c.Specialty = {};
        c.Specialty.Name = c.SpecialtyName;
        $scope.ContractSpecialityList.push(c);
    };

    // Loc
    $scope.tempObject.ContractPracticeLocations = [];
    $scope.SelectLocationsName = function (c) {
        $scope.tempObject.ContractPracticeLocations.push({
            ProfilePracticeLocationID: c.PracticeLocationDetailID,
            LocationsName: c.Facility.FacilityName,
            StreetName: c.Facility.Street,
            CityName: c.Facility.City,
            StateName: c.Facility.State,
            CountryName: c.Facility.Country
        });
        $scope.ContractLocationsList.splice($scope.ContractLocationsList.indexOf(c), 1);
        $scope.LocationsName = "";
    };
    $scope.RemoveContractLocations = function (c) {
        $scope.tempObject.ContractPracticeLocations.splice($scope.tempObject.ContractPracticeLocations.indexOf(c), 1)
        c.Facility = {};
        c.Facility.FacilityName = c.LocationsName;
        c.Facility.Street = c.StreetName;
        c.Facility.City = c.CityName;
        c.Facility.State = c.StateName;
        c.Facility.Country = c.CountryName;
        $scope.ContractLocationsList.push(c);
    };

    $scope.isHasError = false;

    //console.log("data" + $scope.tempObject.ContractLOBs);

    //$scope.LoadedData = [
    //    BusinessEntity = "",
    //    LOBName = [],
    //    SpecialtyName = [],
    //    PLName = [],
    //];
    //for (var i = 0; i < $scope.credentialingInfo.CredentialingContractRequests.length; i++) {
    //    $scope.LoadedData[i].BusinessEntity = $scope.credentialingInfo.CredentialingContractRequests[i].BusinessEntity.GroupName;
    //    for (var j = 0; j < $scope.credentialingInfo.CredentialingContractRequests[i].ContractLOBs.length; j++) {
    //        $scope.LoadedData[i].LOBName.push($scope.credentialingInfo.CredentialingContractRequests[i].ContractLOBs[j].LOB.LOBName);
    //    }
    //    for (var j = 0; j < $scope.credentialingInfo.CredentialingContractRequests[i].ContractPracticeLocations.length; j++) {
    //        $scope.LoadedData[i].PLName.push($scope.credentialingInfo.CredentialingContractRequests[i].ContractPracticeLocations[j].ProfilePracticeLocation.Facility.FacilityName);
    //    }
    //    for (var j = 0; j < $scope.credentialingInfo.CredentialingContractRequests[i].ContractSpecialties.length; j++) {
    //        $scope.LoadedData[i].SpecialtyName.push($scope.credentialingInfo.CredentialingContractRequests[i].ContractSpecialties[j].ProfileSpecialty.Specialty.Name);
    //    }
    //}
    //console.log($scope.LoadedData);
    $scope.tempSecObject = {
        ContractGridID: 0,
        InitialCredentialingDate: new Date,
        Report: {},
    };
    //Quick Save
    $scope.QuickSave = function (c, index) {
        $scope.valid = true;
        $scope.tempSecObject.ContractGridID = c.ContractGridID;
        $scope.tempSecObject.InitialCredentialingDate = angular.copy(c.InitialCredentialingDate);
        $scope.tempSecObject.Report.ProviderID = c.Report.ProviderID;
        $scope.tempSecObject.Report.CredentialingContractInfoFromPlanID = c.Report.CredentialingContractInfoFromPlanID;
        if ($('#dataContainer' + index).find('#InitialCredentialingDate').val() != '' && typeof ($scope.tempSecObject.InitialCredentialingDate) == 'undefined') {
            $scope.valid = false;
        }

        if ($scope.valid == true) {
            $http.post('/Credentialing/CnD/QuickSaveReport', $scope.tempSecObject).
               success(function (data, status, headers, config) {
                   data.dataContractGrid.InitialCredentialingDate = $rootScope.ConvertDateFormat(data.dataContractGrid.InitialCredentialingDate);
                   if (data.dataContractGrid.Report != null) {
                       data.dataContractGrid.Report.CredentialedDate = $rootScope.ConvertDateFormat(data.dataContractGrid.Report.CredentialedDate);
                       data.dataContractGrid.Report.InitiatedDate = $rootScope.ConvertDateFormat(data.dataContractGrid.Report.InitiatedDate);
                       data.dataContractGrid.Report.TerminationDate = $rootScope.ConvertDateFormat(data.dataContractGrid.Report.TerminationDate);
                   }
                   $scope.PlanReportList[index] = angular.copy(data.dataContractGrid);
                   messageAlertEngine.callAlertMessage('ReportSaveSuccess' + index, "Plan Report Updated Successfully !!!", "success", true);
                   $scope.SetVisibility('view', index);
               }).
               error(function (data, status, headers, config) {
                   //console.log("Sorry internal master data cont able to fetch.");
               });
            $scope.ClearVisibility();
        }
        
    };

    $scope.ShowDetailTable = function (tempObject) {
        $scope.isHasError = false;
        $scope.tempObject.BusinessEntity = $('#BEID').find($("[name='BE'] option:selected")).text();
        if ($scope.credentialingInfo.CredentialingLogs != null) {
            if ($scope.credentialingInfo.CredentialingLogs[$scope.credentialingInfo.CredentialingLogs.length - 1].CredentialingAppointmentDetail != null) {
                if ($scope.credentialingInfo.CredentialingLogs[$scope.credentialingInfo.CredentialingLogs.length - 1].CredentialingAppointmentDetail.CredentialingAppointmentSchedule != null) {
                    $scope.LoadedData.InitialCredentialingDate = $scope.credentialingInfo.CredentialingLogs[$scope.credentialingInfo.CredentialingLogs.length - 1].CredentialingAppointmentDetail.CredentialingAppointmentSchedule.AppointmentDate;
                }
            }
        }        
        $scope.tempObject.InitialCredentialingDate = $scope.LoadedData.InitialCredentialingDate
        $('#LoadPlan').show();
        if ($scope.tempObject.BusinessEntityID == null || $scope.tempObject.ContractPracticeLocations.length == 0 || $scope.tempObject.ContractSpecialties.length == 0 || $scope.tempObject.ContractLOBs.length == 0) {
            $scope.isHasError = true;
        }

        if ($scope.isHasError == false) {
            $http.post('/Credentialing/CnD/AddLoadedData?credentialingInfoID=' + credId, tempObject).
            success(function (data, status, headers, config) {
                if (data.status == 'true') {
                    $scope.ContractSpecialityList = angular.copy($scope.credentialingInfo.Profile.SpecialtyDetails);
                    $scope.ContractLocationsList = angular.copy($scope.credentialingInfo.Profile.PracticeLocationDetails);
                    $scope.BusinessEntities = angular.copy($scope.MasterBusinessEntities);
                    $scope.ContractLOBsList = angular.copy($scope.MasterContractLOBslist);
                    $scope.tempObject = angular.copy($scope.MasterTemp);
                data.dataCredentialingContractRequest.InitialCredentialingDate = $rootScope.ConvertDateFormat(data.dataCredentialingContractRequest.InitialCredentialingDate);
                $scope.LoadedData.push(data.dataCredentialingContractRequest);
                //$scope.init_table();

                    for (var i = 0; i < data.dataCredentialingContractRequest.ContractGrid.length; i++) {
                    data.dataCredentialingContractRequest.ContractGrid[i].InitialCredentialingDate = $rootScope.ConvertDateFormat(data.dataCredentialingContractRequest.ContractGrid[i].InitialCredentialingDate);
                        $scope.PlanReportList.push(data.dataCredentialingContractRequest.ContractGrid[i]);
                    }
                    //console.log("Plan report");
                    //console.log($scope.PlanReportList);
                    
                    
                    messageAlertEngine.callAlertMessage('LoadedSuccess', "Contract Request Loaded to Plan Successfully !!!", "success", true);
                }
            }).
                error(function (data, status, headers, config) {
                    //console.log("Sorry internal master data cont able to fetch.");
                });
        }

    };

    //remove loaded request

    $scope.RemoveLoadedContract = function (c) {

        $.ajax({
            url: '/Credentialing/CnD/RemoveRequestAndGrid?credentialingContractRequestID=' + c.CredentialingContractRequestID,
            type: 'POST',
            data: null,
            async: false,
            cache: false,
            contentType: false,
            processData: false,
            success: function (data) {
                //console.log(data);
                try {
                    if (data.status == "true") {
                        var obj = $filter('filter')($scope.LoadedData, { CredentialingContractRequestID: data.credentialingContractRequestID })[0];
                        $scope.LoadedData.splice($scope.LoadedData.indexOf(obj), 1);
                        for (var i = 0; i < obj.ContractGrid.length; i++) {
                            for (var j = 0; j < $scope.PlanReportList.length; j++) {
                                if (obj.ContractGrid[i].ContractGridID == $scope.PlanReportList[j].ContractGridID) {
                                    var gridObj = $filter('filter')($scope.PlanReportList, { ContractGridID: $scope.PlanReportList[j].ContractGridID })[0];
                                    $scope.PlanReportList.splice($scope.PlanReportList.indexOf(gridObj), 1);
                                }
                            }
                        }
                        //$scope.LoadedData.splice($scope.LoadedData.indexOf(c), 1);
                        //$scope.credentialingInfo.CredentialingContractRequests.splice($scope.credentialingInfo.CredentialingContractRequests.indexOf(c), 1);
                        $('#loadedWarningModal').modal('hide');
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

    $scope.RemoveReport = function (c) {

        $.ajax({
            url: '/Credentialing/CnD/RemoveGrid?contractGridID=' + c.ContractGridID,
            type: 'POST',
            data: null,
            async: false,
            cache: false,
            contentType: false,
            processData: false,
            success: function (data) {
                //console.log(data);
                try {
                    if (data.status == "true") {
                        var obj = $filter('filter')($scope.PlanReportList, { ContractGridID: data.contractGridID })[0];
                        $scope.PlanReportList.splice($scope.PlanReportList.indexOf(obj), 1);
                        //$scope.PlanReportList.splice($scope.PlanReportList.indexOf(c), 1);
                        //  $scope.credentialingInfo.CredentialingContractRequests.splice($scope.credentialingInfo.CredentialingContractRequests.indexOf(c), 1);
                        $('#WarningModal').modal('hide');
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
    $scope.showerror = false;

    $scope.SetRequestId = function (obj) {

        $scope.templateList
        for (var i = 0; i < $scope.templateList.length; i++) {
            $scope.templateList[i].check = false;
        }

        $scope.selectedTemplate = null;
        sessionStorage.setItem('templateName', '');
        sessionStorage.setItem('templateCode', '');

        sessionStorage.setItem('CredentialingContractRequestID', obj.CredentialingContractRequestID);
        sessionStorage.setItem('InitialCredentialingDate', obj.InitialCredentialingDate);
        sessionStorage.setItem('ContractPracticeLocations', obj.ContractPracticeLocations);
        sessionStorage.setItem('ContractSpecialties', obj.ContractSpecialties);


    };

    $scope.initWarning = function (c, i) {
        $scope.tempRemoveReportData = angular.copy(c);
        $('#WarningModal').modal();
    };
    $scope.loadedWarning = function (c) {

        $scope.tempRemoveLoadedData = angular.copy(c);
        $('#loadedWarningModal').modal();
    };

    $scope.SaveReport = function (c, index) {
        var validationStatus = true;
        var url;
        var myData = {};
        var $formData;
        //  if ($scope.Visibility == ('editVisibility' + index)) {
        //Add Details - Denote the URL
        try {
            $formData = $('#PlanReportForm').find('form');
            url = "/Credentialing/CnD/SaveReport";
        }
        catch (e)
        { };
        //    }
        //validationStatus = $formData.valid();
        if (validationStatus) {
            //console.log(new FormData($formData[0]));

            //console.log($formDataStateLicense);
            $.ajax({
                url: url,
                type: 'POST',
                data: new FormData($formData[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    //console.log(data);
                    try {
                        if (data.status == "true") {
                            data.dataContractGrid.InitialCredentialingDate = $rootScope.ConvertDateFormat(data.dataContractGrid.InitialCredentialingDate);
                            if (data.dataContractGrid.Report != null) {
                                data.dataContractGrid.Report.CredentialedDate = $rootScope.ConvertDateFormat(data.dataContractGrid.Report.CredentialedDate);
                                data.dataContractGrid.Report.InitiatedDate = $rootScope.ConvertDateFormat(data.dataContractGrid.Report.InitiatedDate);
                                data.dataContractGrid.Report.TerminationDate = $rootScope.ConvertDateFormat(data.dataContractGrid.Report.TerminationDate);
                            }
                            $scope.PlanReportList[index] = data.dataContractGrid;
                            messageAlertEngine.callAlertMessage('ReportSaveSuccess' + index, "Plan Report Updated Successfully !!!", "success", true);
                            $scope.SetVisibility('view', index);
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

    $http.get('/Credentialing/CnD/GetContractGrid?credentialingInfoID=' + credId).
       success(function (data, status, headers, config) {
           $scope.PlanReportList = angular.copy(data);
           for (var i = 0; i < $scope.PlanReportList.length; i++) {
               $scope.PlanReportList[i].InitialCredentialingDate = $rootScope.ConvertDateFormat($scope.PlanReportList[i].InitialCredentialingDate);
               if ($scope.PlanReportList[i].Report != null) {
                   $scope.PlanReportList[i].Report.InitiatedDate = $rootScope.ConvertDateFormat($scope.PlanReportList[i].Report.InitiatedDate);
                   $scope.PlanReportList[i].Report.CredentialedDate = $rootScope.ConvertDateFormat($scope.PlanReportList[i].Report.CredentialedDate);
                   $scope.PlanReportList[i].Report.TerminationDate = $rootScope.ConvertDateFormat($scope.PlanReportList[i].Report.TerminationDate);
               }
           }
       }).
       error(function (data, status, headers, config) {
           //console.log("Sorry internal master data cont able to fetch.");
       });
    //----------------------------------
    $scope.SelectDocument = function (credprofile) {

        if ($scope.selectedTemplate != null) {
        sessionStorage.setItem('templateName', $scope.selectedTemplate.name);
        sessionStorage.setItem('templateCode', $scope.selectedTemplate.code);

        sessionStorage.setItem('selectDocumentBit', true);
        $scope.dismiss();
        var value = '/Credentialing/DelegationProfileReport/Index?profileId=' + credprofile.Profile.ProfileID;
        var open_link = window.open('', '_blank');
        open_link.location = value;

        }
        else {
            $scope.showerror = true;
        }

        //window.location.assign('/Credentialing/DelegationProfileReport/Index?profileId=' + credprofile.Profile.ProfileID);
    }

    $scope.viewDocument = function (CredentialingContractRequestID, credprofile) {

        $http.get('/Credentialing/DelegationProfileReport/GetDelegationProfileReport?CredContractRequestId=' + CredentialingContractRequestID).
      success(function (data, status, headers, config) {
        
          if (data.status == 'true') {
              sessionStorage.setItem('profileReport', JSON.stringify(data.profileReports[data.profileReports.length - 1]));
              sessionStorage.setItem('selectDocumentBit', false);

              var value = '/Credentialing/DelegationProfileReport/Index?profileId=' + credprofile.Profile.ProfileID;
              var open_link = window.open('', '_blank');
              open_link.location = value;

          }
      }).
      error(function (data, status, headers, config) {
          //console.log("Sorry internal master data cont able to fetch.");
      });

       
        //window.location.assign('/Credentialing/DelegationProfileReport/Index?profileId=' + credprofile.Profile.ProfileID);
    };

    $scope.templateList = [{ name: 'A2HC Provider Profile for Wellcare', code: 'A2HC', check: false }, { name: 'AHC Provider Profile for Wellcare', code: 'AHC', check: false }, ];
    //==================select template==============
    $scope.selectedTemplate = null;
    $scope.setSelectedTemplated = function (index) {
        if ($scope.templateList[index].check == true) {
            $scope.showerror = false;
            $scope.selectedTemplate = $scope.templateList[index];;
            for (var i = 0; i < $scope.templateList.length; i++) {
                if (index != i) {
                    $scope.templateList[i].check = false;
                }
            }
        }
            flag = 0;
            for (var i = 0; i < $scope.templateList.length; i++) {
                if ($scope.templateList[i].check == true) {
                    flag = 1;
                }
            }
            if (flag == 0) {
                $scope.selectedTemplate = null;
            }
    };
    var counts = [];

    //$scope.data = $scope.LoadedData;
    //$scope.init_table = function () {

    //    if ($scope.data.length <= 10) {
    //        counts = [];
    //    }
    //    else if ($scope.data.length <= 25) {
    //        counts = [10, 25];
    //    }
    //    else if ($scope.data.length <= 50) {
    //        counts = [10, 25, 50];
    //    }
    //    else if ($scope.data.length <= 100) {
    //        counts = [10, 25, 50, 100];
    //        }
    //    else if ($scope.data.length > 100) {
    //        counts = [10, 25, 50, 100];
    //    }
        
        
    //};
    //$scope.tableParams1 = new ngTableParams({
    //    page: 1,            // show first page
    //    count: 10,          // count per page
    //    filter: {
    //        //name: 'M'       // initial filter
    //        //FirstName : ''
    //    },
    //    sorting: {

    //        //name: 'asc'     // initial sorting
    //    }
    //}, {
    //    counts: counts,
    //    total: $scope.data.length, // length of data
    //    getData: function ($defer, params) {
    //        // use build-in angular filter
    //        var filteredData = params.filter() ?
    //                $filter('filter')($scope.data, params.filter()) :
    //                $scope.data;
    //        var orderedData = params.sorting() ?
    //                $filter('orderBy')(filteredData, params.orderBy()) :
    //                $scope.data;

    //        params.total(orderedData.length); // set total for recalc pagination
    //        $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
    //    }
    //});


});