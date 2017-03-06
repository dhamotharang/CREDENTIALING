//Author:Cred-Axis Team


PlanApp.controller("PlanController", ["$timeout", "$rootScope", "$scope", "$filter", "$log", "toaster", "PlanFactory", "PlanService", 'LOBValues', 'BEValues', 'AddingPlanDataInitialization', 'PreferredContacts', 'speech', 'Loadash', function ($timeout, $rootScope, $scope, $filter, $log, toaster, PlanFactory, PlanService, LOBValues, BEValues, AddingPlanDataInitialization, PreferredContacts, speech, Loadash) {


    
    //if ($rootScope.support) {
    //    speech.sayText("Welcome TO Plan Module", $rootScope.config);
    //}
    //var Test = [{
    //    age: 23,
    //    name:"santosh"
    //},
    //{
    //    age: 32,
    //    name: "Kumar"
    //},
    //{
    //    age: 12,
    //    name: "Senapati"
    //}];
    ////_.first([1,2,3], 2)
    //console.log(Loadash.first(Test, 2));
    //var array = [1];
    //var other = Loadash.concat(array, 2, [3], [[4]]);
    //var testdatat = Loadash.concat([3, 2, 1], [4, 2]);

    //console.log(other, Loadash.shuffle(testdatat));
    //================================= Variable Declaration Start====================================================================================
    $scope.ActiveStatus = true;
    $scope.temporarymaster = $filter('MasterDataResetforPage')('Master');
    $scope.LoadingStatus = true;
    $scope.savingStatus = false;
    $scope.PlanNameCheckLoading = false;
    $scope.PlanNameCheckMessage = false;
    $scope.Lobs = [];
    $scope.BE_LOB_Maps = [];
    $scope.tabStatus = 0;
    $scope.addTabStatus = 0;
    $scope.LOBCount = 0;
    $scope.CurrentPageMasterViewData = $filter('MasterDataResetforPage')('Master');
    $scope.tempPlanObject = {};
    $scope.tempPlanAddEditObject = {};
    $scope.MasterLOBs = [];
    $scope.tempObject1 = {};
    $scope.visibilityControl = "";
    $scope.PreferredContacts = [];
    $scope.PreferredContactTypesArray = [];
    $scope.tempPlanLOBObject = {};
    $scope.tempPlanContractObject = {};
    $scope.tableIDToScroll = "";
    $scope.selectedPlan = "";
    $scope.PlanIDForEdit = 0;
    $scope.CountryDailCodes = countryDailCodes;
    var ctrl = this;
    var tempPlanID = 0;
    this.displayed = [];

    //================================= Variable Declaration Ends====================================================================================



    //================================== Temporary function Declaration Start ===============================================================================

    this.callServer = function callServer(tableState) {
        ctrl.isLoading = true;
        var pagination = tableState.pagination;
        var start = pagination.start || 0;
        var number = pagination.number || 5;
        $scope.tableStateValue = tableState;

        PlanFactory.getPage(start, number, tableState, $scope.ActiveStatus).then(function (result) {
            ctrl.displayed = result.data;
            //ctrl.temp = ctrl.displayed;
            tableState.pagination.numberOfPages = result.numberOfPages;
            ctrl.isLoading = false;
        });
    };

    $scope.ViewHistory = function () {
        $scope.CurrentPageMasterViewData = $scope.ActiveStatus ? $filter('MasterDataResetforPage')('History') : $filter('MasterDataResetforPage')('Master');
        $scope.ActiveStatus = PlanFactory.getActiveStatus($scope.ActiveStatus);
        $scope.tableStateValue = PlanFactory.resetTableState($scope.tableStateValue);
        ctrl.callServer($scope.tableStateValue);
    }

    $scope.RemovePlanDataInitiated = function (PlanID, PlanName) {
        $scope.TempPlanName = PlanName;
        $scope.savingStatus = false;
        tempPlanID = PlanID;
    }
    $scope.ReactivePlanDataInitiated = function (PlanID) {
        $scope.savingStatus = false;
        tempPlanID = PlanID;
    }
    $scope.GroupCheck = false;
    $scope.myfun = function (data,value) {
            for (var i in $scope.BE_LOB_Maps) {
                for (var j in $scope.BE_LOB_Maps[i].BEs) {
                    if ($scope.BE_LOB_Maps[i].BEs[j].BE.GroupName == data.BE.GroupName)
                        $scope.BE_LOB_Maps[i].BEs[j].IsChecked = value;
            }
        }
    }

    $scope.AddPhones = function (obj, condition, preferedObj) {
        obj.push({
            PhoneDetailID: null,
            Number: "",
            CountryCode: "+1",
            PhoneTypeEnum: condition,
            Preference: "Secondary",
            PreferenceType: 2,
            Status: "Active",
            StatusType: 1
        });

        $scope.addPreferredContactTypesToArray(condition, preferedObj);
    };
    $scope.addPersonsName = function (obj) {
        console.log($scope.tempPlanAddEditObject);
        obj.push('');
    }
    $scope.ParsePlanContactPersonName = function (ContactPersonNames) {
        return ContactPersonNames == null ? null : JSON.parse(ContactPersonNames);
    }

    $scope.AddEmail = function (obj, preferedObj) {
        obj.push({
            EmailDetailID: null,
            EmailAddress: "",
            Preference: "Secondary",
            PreferenceType: 2,
            Status: "Active",
            StatusType: 1
        });
        $scope.addPreferredContactTypesToArray(4, preferedObj);

    };
    $scope.RemoveContactDetails = function (index, data) {
        data.splice(index, 1);
    };
    $scope.RemovePersonName = function (personArray, obj) {
        var index = personArray.indexOf(obj);
        personArray[index] = obj;
        personArray.splice(index,1)        
    }
    $scope.addPreferredContactTypesToArray = function (contactType, preferedObj) {
        var status = true;
        for (var i in preferedObj) {
            if (preferedObj[i].PreferredWrittenContactType == contactType) {
                preferedObj[i].StatusType = 1;
                status = false;
                break;
            }
        }
        if (status) {
            for (var i in $scope.tempSelectedDeactivatedPreferredContacts) {
                if ($scope.tempSelectedDeactivatedPreferredContacts[i].PreferredWrittenContactType == contactType) {
                    $scope.tempSelectedDeactivatedPreferredContacts[i].StatusType = 1;
                    preferedObj.push($scope.tempSelectedDeactivatedPreferredContacts[i]);
                    status = false;
                    break;
                }
            }
        }

        if (status) {
            preferedObj.push($scope.PreferredContacts[contactType - 1]);
        }
    };
    $scope.RemovePreferredContactTypes = function (contactType, PreferedObject) {
        for (var i in PreferedObject) {
            if (PreferedObject[i].PreferredWrittenContactType == contactType && PreferedObject[i].PreferredContactID) {
                PreferedObject[i].StatusType = 2;
                break;
            }
            if (PreferedObject[i].PreferredWrittenContactType == contactType && !PreferedObject[i].PreferredContactID) {
                PreferedObject.splice(PreferedObject.indexOf(PreferedObject[i]), 1);
                break;
            }
        }
    };
    $scope.ContactDetailsPhoneConditionFunction = function (obj, data, phoneType) {
        if (data.length > 0) {
            for (var i in data) {
                if (data[i].PhoneTypeEnum == phoneType) {
                    if (i == data.indexOf(obj)) {
                        switch (data[i].PreferenceType) {
                            case 1:
                                data[i].PreferenceType = 2;
                                break;
                            case 2:
                                data[i].PreferenceType = 1;
                                break;
                            default:
                                data[i].PreferenceType = 1;
                        }
                    } else {
                        data[i].PreferenceType = 2;
                    }
                }
            }
        }
    };
    $scope.ContactDetailsEmailConditionFunction = function (index, data) {
        if (data.length > 0) {
            for (var i in data) {
                if (i == index) {
                    switch (data[i].PreferenceType) {
                        case 1:
                            data[i].PreferenceType = 2;
                            break;
                        case 2:
                            data[i].PreferenceType = 1;
                            break;
                        default:
                            data[i].PreferenceType = 1;
                    }
                } else {
                    data[i].PreferenceType = 2;
                }
            }
        }

    };
    $scope.showConfirmation1 = function (arryData, obj, condition,PreferedObject) {
        obj.StatusType = $scope.changeStausType(obj.StatusType);
        var index = arryData.indexOf(obj);
        arryData[index] = obj;

        if (condition == "phone") {
            if (!obj.PhoneDetailID) {
                $scope.RemoveContactDetails(index, arryData);
            }
            var status = $scope.IsActiveContactDetails(arryData, obj);
            if (status) {
                $scope.addPreferredContactTypesToArray(obj.PhoneTypeEnum, PreferedObject);
            } else {
                //$scope.RemovePreferredContactTypes(obj.PhoneTypeEnum);
                for (var i in PreferedObject) {
                    if (PreferedObject[i].PreferredWrittenContactType == obj.PhoneTypeEnum && PreferedObject[i].PreferredContactID) {
                        PreferedObject[i].StatusType = 2;
                        break;
                    }
                    if (PreferedObject[i].PreferredWrittenContactType == obj.PhoneTypeEnum && !PreferedObject[i].PreferredContactID) {
                        PreferedObject.splice(PreferedObject.indexOf(PreferedObject[i]), 1);
                        break;
                    }
                }
            }
        } else if (condition == "email") {
            if (!obj.EmailDetailID) {
                $scope.RemoveContactDetails(index, arryData);
            }
            var status = $scope.IsActiveEmailIds(arryData);
            if (status) {
                $scope.addPreferredContactTypesToArray(4, PreferedObject);
            } else {
                $scope.RemovePreferredContactTypes(4,PreferedObject);
            }
        }

    };
    $scope.changeStausType = function (status) {
        if (status == 1) {
            status = 2;
        } else {
            status = 1;
        }
        return status;
    };
    $scope.selectedPrefferdContact = function (pc, index) {
        $scope.pcPriority = pc;
        $scope.pcIndex = index;
    };
    $scope.ChangePreferredContactsPriority = function (condition, preferedObj) {
        var index = preferedObj.indexOf($scope.pcPriority);
        if (condition == "increase" && index !== 0) {
            preferedObj[index].PreferredIndex = index;
            preferedObj[index - 1].PreferredIndex = index + 1;
            $scope.pcIndex = index - 1;
        } else if (condition == "decrease" && index !== preferedObj.length - 1) {
            preferedObj[index].PreferredIndex = index + 2;
            preferedObj[index + 1].PreferredIndex = index + 1;
            $scope.pcIndex = index + 1;
        }
        preferedObj.sort(function (a, b) {
            return a.PreferredIndex - b.PreferredIndex;
        });
    };
    $scope.IsActiveContactDetails = function (data, obj) {
        var status = false;
        for (var i in data) {
            if (obj.PhoneTypeEnum == data[i].PhoneTypeEnum && data[i].StatusType == 1) {
                status = true;
                break;
            }
        }
        return status;
    };
    $scope.IsActiveEmailIds = function (data) {
        var status = false;
        for (var i in data) {
            if (data[i].StatusType == 1) {
                status = true;
                break;
            }
        }
        return status;
    };
    $scope.showContryCodeDiv = function (div_Id) {
        changeVisibilityOfCountryCode();
        $("#" + div_Id).show();
    };
    $scope.removeFile = function () {
        PlanFactory.PlanLogoReset();
        $scope.tempPlanAddEditObject.PlanLogoPath = "";
    };
    $scope.addressHomeAutocomplete = function (location, pl) {
        $scope.resetAddressModels(pl);
        pl.City = location;
        if (location.length > 1 && !angular.isObject(location)) {
            PlanService.getLocations(location).then(function (val) {
                $scope.Locations = val.data;
                angular.element(".ProviderTypeSelectAutoList").show();
            });
        } else if (angular.isObject(location)) {
            $scope.setAddressModels(location, pl);
        }
    };
    $scope.resetAddressModels = function (pl) {
        pl.State = "";
        pl.Country = "";
    };
    $scope.setAddressModels = function (location, pl) {
        pl.City = location.City;
        pl.State = location.State;
        pl.Country = location.Country;
    };
    $scope.displayLocationTable = function () {
        angular.element(".ProviderTypeSelectAutoList").show();
    }
    $scope.selectedLocation = function (location, pl) {
        $scope.setAddressModels(location, pl);
        $(".ProviderTypeSelectAutoList").hide();
    };
    $scope.checkPlanName = function (PlanName) {
        $scope.PlanNameCheckLoading = true;
        if ($scope.CurrentPageMasterViewData.Type == 'Add') {
            $scope.PlanNameCheckMessage = $filter('PlanExist')(PlanName);
        }
        else {
            $scope.PlanNameCheckMessage = PlanFactory.LowerCaseTrimedData(PlanName) == PlanFactory.LowerCaseTrimedData($rootScope.PlanNameForCheck) ? false : $filter('PlanExist')(PlanName);
        }
        $timeout(function () { $scope.PlanNameCheckLoading = false }, 500);
    }
    $scope.EditLOB = function (condition, tempObject1, index) {
        if ($scope.IsNewLOB == false) {
            $scope.CancelLOBEdit();
        }
        
        $scope.lobindex = index;
        $scope.IsNewLOB = false;
        $scope.visibilityControl = condition;

        $scope.tempObject1 = angular.copy(tempObject1);
        $scope.tempObject1.LOBSDATA = {
            LOBs: $scope.Lobs,
        },
        $scope.tempObject1.SELECTEDLOB = {};
        $scope.ConditionLOB = 2;
        $scope.AddNewPlanLOB = true;
        $scope.pcPriority = null;
        
        $scope.GetAddEditLobData($scope.tempPlanAddEditObject.PlanLOBs, tempObject1);
        $scope.tempObject1.SELECTEDLOB = tempObject1.LOB;
    };
    $scope.CancelLOBEdit = function () {
        if ($scope.IsNewLOB == true) {
            $scope.tempPlanAddEditObject.PlanLOBs.pop();
            $scope.visibilityControl = "";
            $scope.tempObject1 = {};
        } else {
            $scope.visibilityControl = "";
            $scope.tempObject1 = {};
        }
        $scope.isPlanAdd = false;
    };
    $scope.LOBWarning = function (index) {
        $scope.LobIndexVal = index;
        $scope.providerCountForLOB = $scope.tempPlanAddEditObject.PlanLOBs[index].ProviderCount;
        $scope.ContractsCountforLOB = $scope.tempPlanAddEditObject.PlanLOBs[index].ContractsCount;
        $('#LobWarningModal').modal();
    }
    $scope.RemoveLOB = function (index) {
        $scope.LobIndexVal = index == undefined ? $scope.LobIndexVal : index;
        $scope.tempInactiveLOBs.push($scope.tempPlanAddEditObject.PlanLOBs[$scope.LobIndexVal]);
        $scope.tempPlanAddEditObject.PlanLOBs.splice($scope.LobIndexVal, 1);
        $scope.tempObject1 = {};
        $('#LobWarningModal').modal('hide')
        $('body').removeClass('modal-open');
        $('.modal-backdrop').remove();
    };
    $scope.isPlanAdd = false;
    $scope.AddNewLOB = function (condition) {
        $scope.isPlanAdd = true;
        var temp = {
            LOBID: 0,
            LOBSDATA: {
                LOBs:$scope.Lobs,
            },
            StatusType: 1,
            SubPlans: [{
                SubPlanId: 0,
                SubPlanName: "",
                SubPlanCode: "",
                SubPlanDescription: "",
                StatusType: 1,
            }],
            LOBAddressDetails: [{
                Street: "",
                City: "",
                Appartment: "",
                State: "",
                Country: "",
                County: "",
                StatusType: 1,
            }],
            LOBContactDetails: [{
                ContactPersonName: "",
                EmailAddress: "",
                PhoneNumber: "",
                FaxNumber: "",
                StatusType: 1,
            }]
        };
        $scope.tempPlanAddEditObject.PlanLOBs.push(temp);
        $scope.tempObject1 = angular.copy(temp);
        

        $scope.tempObject1.LOBAddressDetails[0] = angular.copy($scope.tempPlanAddEditObject.Locations[0]);
        $scope.tempObject1.LOBContactDetails[0] = angular.copy($scope.tempPlanAddEditObject.ContactDetails[0]);
        $scope.tempObject1.LOBContactDetails[0].ContactPersonName = "";
        $scope.IsNewLOB = true;
        $scope.visibilityControl = condition;
        $scope.GetAddEditLobData($scope.tempPlanAddEditObject.PlanLOBs, null);
        $scope.tempObject1.SELECTEDLOB = $scope.tempObject1.LOBSDATA.LOBs.length > 0 ? $scope.tempObject1.LOBSDATA.LOBs[0] : null
       
    };
    $scope.AddSubPlan = function () {
        $scope.tempObject1.SubPlans.push({
            SubPlanId: 0,
            SubPlanName: "",
            SubPlanCode: "",
            SubPlanDescription: "",
            Status: 1
        });
    };
    $scope.RemoveSubPlan = function (index) {
        $scope.tempObject1.SubPlans.splice(index, 1);
    };
    $scope.SaveLOB = function (FormID, tempObject1) {
        //for (var i in $scope.Lobs) {
        //    if (tempObject1.LOBID == $scope.Lobs[i].LOBID) {
        //        tempObject1.LOB = $scope.Lobs[i];
        //        break;
        //    }
        //}
        for (var i in $scope.tempInactiveLOBs) {
            if ($scope.tempInactiveLOBs[i].LOBID == tempObject1.SELECTEDLOB.LOBID) {
                tempObject1.PlanLOBID = $scope.tempInactiveLOBs[i].PlanLOBID;
                tempObject1.PlanID = $scope.tempInactiveLOBs[i].PlanID;
                tempObject1.LOBContactDetails[0].LOBContactDetailID = $scope.tempInactiveLOBs[i].LOBContactDetails[0].LOBContactDetailID;
                tempObject1.LOBContactDetails[0].ContactDetail.LOBContactDetailID = $scope.tempInactiveLOBs[i].LOBContactDetails[0].ContactDetail.LOBContactDetailID;
                tempObject1.LOBContactDetails[0].ContactDetail.ContactDetailID = $scope.tempInactiveLOBs[i].LOBContactDetails[0].ContactDetail.ContactDetailID;
                tempObject1.LOBAddressDetails[0].LOBAddressDetailID = $scope.tempInactiveLOBs[i].LOBAddressDetails[0].LOBAddressDetailID;
                break;
            }
        }
        tempObject1.LOBID = tempObject1.SELECTEDLOB.LOBID
        tempObject1.LOB = tempObject1.SELECTEDLOB;
        var formElement = angular.element("#" + FormID);
        PlanFactory.resetFormValidation(formElement);
        if (formElement.valid()) {
            if ($scope.IsNewLOB == true) {
                $scope.tempPlanAddEditObject.PlanLOBs[$scope.tempPlanAddEditObject.PlanLOBs.length - 1] = angular.copy(tempObject1);
            } else {
                $scope.tempPlanAddEditObject.PlanLOBs[$scope.lobindex] = angular.copy(tempObject1);
            }
            $scope.visibilityControl = "";
            $scope.tempObject1 = {};
        }
        $scope.isPlanAdd = false;
    };

    //----------------------------------------- get same Address ----------------------------
    $scope.GetAddress = function (condition) {
        if (condition) {
            $scope.tempObject1.LOBAddressDetails[0] = angular.copy($scope.tempPlanAddEditObject.Locations[0]);
        } else {
            $scope.tempObject1.LOBAddressDetails[0] = {
                Street: "",
                Appartment: "",
                City: "",
                State: "",
                ZipCode: "",
                Country: "",
                County: "",
            };
        }
        $scope.tempObject1.LOBAddressDetails[0].IsSameAddress = condition;
    };

    $scope.GetContactPerson = function (condition) {
        $scope.pcPriority = null;
        if (condition) {
            $scope.tempObject1.LOBContactDetails[0] = angular.copy($scope.tempPlanAddEditObject.ContactDetails[0]);
            $scope.tempObject1.LOBContactDetails[0].ContactPersonName = "";
        } else {
            $scope.tempObject1.LOBContactDetails[0] = {
                ContactDetail: {
                    PhoneDetails: [],
                    EmailIDs: [],
                    PreferredWrittenContacts: [],
                    PreferredContacts: []
                },
                PlanContactDetailID: 0,
                ContactPersonName: [],
                StatusType: 1,
            };
        }
        $scope.tempObject1.LOBContactDetails[0].IsSameContatcPerson = condition;
    };
    $scope.GetAddEditLobData = function (planLOBs, CurrentLob) {
        var pendingLOBs = [];

        for (var i in $scope.MasterLOBs) {
            var status = true;
            for (var j in planLOBs) {
                if ($scope.MasterLOBs[i].LOBID == planLOBs[j].LOBID) {
                    status = false;
                }
            }
            if (status) {
                pendingLOBs.push($scope.MasterLOBs[i]);
            }
        }
        if (CurrentLob && CurrentLob.LOB) {
            pendingLOBs.push(angular.copy(CurrentLob.LOB));
        }
        $log.log(angular.isObject($scope.tempObject1.LOBSDATA));
        $scope.tempObject1.LOBSDATA.LOBs = angular.copy(pendingLOBs);
        
    };
    $scope.getContactDetailsPhoneByPhoneTypeAndActiveStatus = function (data) {
        var temp = [];
        for (var i in data) {
            if (data[i].StatusType == 1) {
                temp.push(data[i]);
            }
        }
        return temp;
    };
    $scope.getInactivatedDataArray = function (dataArray) {
        var temInactivatedData = [];

        for (var i = 0; i < dataArray.length; i++) {
            if (dataArray[i].StatusType == 2) {
                temInactivatedData.push(dataArray[i]);
                dataArray.splice(i, 1);
                i--;
            }
        }

        return {
            dataArray: dataArray,
            temInactivatedData: temInactivatedData
        };
    };

   
    //================================== Temporary function Declaration End ===============================================================================



    //==============================  Plan Module CRUD Operations Start ======================================================================

    $scope.AddPlanData = function () {
        $scope.savingStatus = true;
        $scope.tempPlanAddEditObject.ContactDetails[0].ContactPersonName = JSON.stringify($scope.tempPlanAddEditObject.ContactDetails[0].ContactPersonName);
        var formdata = new FormData();
        var form5 = angular.element('#Plandata, #LOB_BE_Mapping').serializeArray();
        for (var i in form5) {
            formdata.append(form5[i].name, form5[i].value);
        }
        formdata.append(angular.element("#PlanLogoFile")[0].name, angular.element("#PlanLogoFile")[0].files[0]);
        if ($scope.CurrentPageMasterViewData.Type == "Add") {
            PlanService.AddPlanData(formdata).then(function (result) {
                if (result.data.Status == "true") {
                    result.data.PlanAddedDataByID.PlanDTO.PlanContactPersonName = $scope.ParsePlanContactPersonName(result.data.PlanAddedDataByID.PlanDTO.PlanContactPersonName);
                    $rootScope.ActivePlanData.unshift(result.data.PlanAddedDataByID.PlanDTO);
                    $scope.tableStateValue = PlanFactory.resetTableState($scope.tableStateValue);
                    ctrl.callServer($scope.tableStateValue);
                    $scope.selectedPlan = $scope.tempPlanAddEditObject.PlanName;
                    $scope.BE_LOB_Maps = angular.copy(PlanFactory.formatContractData(BEValues, result.data.PlanAddedDataByID.PLanLOB));
                    for (var i in $scope.BE_LOB_Maps) {
                        for (var j in $scope.BE_LOB_Maps[i].BEs) {
                            $scope.BE_LOB_Maps[i].BEs[j].IsChecked = false;
                        }
                    }                    
                    $scope.addTabStatus = 2;
                    $scope.tabStatus = 2;
                    $scope.savingStatus = false;
                    PlanFactory.modalDismiss();
                    toaster.pop('Success', "Success", 'Plan Data Added Successfully');
                    //if ($rootScope.support) {
                    //    speech.sayText("Plan Data Added Successfully", $rootScope.config);
                    //}
                }
                else {
                    $scope.savingStatus = false;
                    PlanFactory.modalDismiss();
                    $scope.CancelAdd();
                    
                    toaster.pop('error', "", 'Error occured while Adding a plan.. Please try after sometime');
                    //if ($rootScope.support) {
                    //    speech.sayText("Error occured while Adding a plan.. Please try after sometime", $rootScope.config);
                    //}
                }
            },
            function (error) {
                $scope.savingStatus = false;
                PlanFactory.modalDismiss();
                $scope.CancelAdd();
                toaster.pop('error', "", 'Error occured while Adding a plan.. Please try after sometime');
                //if ($rootScope.support) {
                //    speech.sayText("Error occured while Adding a plan.. Please try after sometime", $rootScope.config);
                //}
            });
        }
        else {
            PlanService.UpdatePlanData(formdata).then(function (result) {
                if (result.data.Status == "true") {
                    result.data.PlanUpdatedDataByID.PlanDTO.PlanContactPersonName = $scope.ParsePlanContactPersonName(result.data.PlanUpdatedDataByID.PlanDTO.PlanContactPersonName);
                    $rootScope.ActivePlanData[$rootScope.ActivePlanData.indexOf($rootScope.ActivePlanData.filter(function (ActivePlanData) { return ActivePlanData.PlanID == result.data.PlanUpdatedDataByID.PlanDTO.PlanID })[0])] = result.data.PlanUpdatedDataByID.PlanDTO;
                    //$scope.tableStateValue = PlanFactory.resetTableState($scope.tableStateValue);
                    ctrl.callServer($scope.tableStateValue);
                    $scope.selectedPlan = $scope.tempPlanAddEditObject.PlanName;
                    $scope.BE_LOB_Maps = angular.copy(PlanFactory.FormatContractDataForEdit(BEValues, result.data.PlanUpdatedDataByID.PLanLOB, result.data.PlanUpdatedDataByID.PLanContract));
                    $scope.addTabStatus = 2;
                    $scope.tabStatus = 2;
                    $scope.savingStatus = false;
                    PlanFactory.modalDismiss();
                    toaster.pop('Success', "Success", 'Plan Data Updated Successfully');
                    //if ($rootScope.support) {
                    //    speech.sayText("Plan Data Updated Successfully", $rootScope.config);
                    //}
                }
                else {
                    $log.log(result,status);
                    $scope.savingStatus = false;
                    PlanFactory.modalDismiss();
                    $scope.CancelAdd();
                    toaster.pop('error', "", 'Error occured while Updating a plan.. Please try after sometime');
                    //if ($rootScope.support) {
                    //    speech.sayText("Error occured while Updating a plan.. Please try after sometime", $rootScope.config);
                    //}
                }
            },
            function (error) {
                $log.log(error);
                $scope.savingStatus = false;
                PlanFactory.modalDismiss();
                $scope.CancelAdd();
                toaster.pop('error', "", 'Error occured while Updating a plan.. Please try after sometime');
                //if ($rootScope.support) {
                //    speech.sayText("Error occured while Updating a plan.. Please try after sometime", $rootScope.config);
                //}
            });
        }
    }

    $scope.ViewPlanData = function (ID, Operation) {
        $scope.LoadingStatus = false;
        PlanService.ViewPlanDataByID(ID).then(function (result) {
            $scope.tempViewObject = angular.copy(result.data.PlanData);
            $scope.ViewPlan = true;
            $scope.tempPlanObject = PlanFactory.FormatPlanData(result.data.PlanData.PlanData);
            $scope.tempPlanContractObject = PlanFactory.FormatContractData($scope.tempPlanObject.PlanName, result.data.PlanData.PlanContracts);
            $scope.tempPlanObject.ContactDetails[0].ContactPersonName = $scope.ParsePlanContactPersonName($scope.tempPlanObject.ContactDetails[0].ContactPersonName);
            $scope.tempPlanObject.PlanLOBs = jQuery.grep($scope.tempPlanObject.PlanLOBs, function (ele) { return ele.Status != "Inactive" });
            $scope.tabStatus = Operation == 'ViewPlancontract' ? 2 : 0;
            $scope.CurrentPageMasterViewData = $filter('MasterDataResetforPage')('View');
            $scope.LoadingStatus = true;
            //toaster.pop('Success', "Success", 'Please check console for Plan data');
        },
        function (error) {
            $scope.LoadingStatus = true;
            //toaster.pop('error', "", 'Unable to get the plan Data...  Please try after sometime');
        });
    }

    $scope.RemovePlanData = function () {
        $scope.savingStatus = true;
        PlanService.RemovePlanByID(tempPlanID).then(function (result) {
            var tempPlan = $rootScope.ActivePlanData.filter(function (items) { return items.PlanID == tempPlanID })[0];
            $rootScope.InactivePlanData.unshift(tempPlan);
            $rootScope.ActivePlanData.splice($rootScope.ActivePlanData.indexOf(tempPlan), 1);
            $scope.tableStateValue = PlanFactory.resetTableState($scope.tableStateValue);
            ctrl.callServer($scope.tableStateValue);
            $scope.savingStatus = false;
            PlanFactory.modalDismiss();
            toaster.pop('Success', "Success", 'Plan Data Removed Successfully');
            //if ($rootScope.support) {
            //    speech.sayText("Plan Data Removed Successfully", $rootScope.config);
            //}
        },
        function (error) {
            $scope.savingStatus = false;
            PlanFactory.modalDismiss();
            toaster.pop('error', "", 'Error occured while removing a plan.. Please try after sometime');
            //if ($rootScope.support) {
            //    speech.sayText("Error occured while removing a plan.. Please try after sometime", $rootScope.config);
            //}
        });

    }

    $scope.ReactivePlanData = function () {
        $scope.savingStatus = true;
        PlanService.ReactivePlanByID(tempPlanID).then(function (result) {
            var tempPlan = $rootScope.InactivePlanData.filter(function (items) { return items.PlanID == tempPlanID })[0];
            $rootScope.ActivePlanData.unshift(tempPlan);
            $rootScope.InactivePlanData.splice($rootScope.InactivePlanData.indexOf(tempPlan), 1);
            $scope.tableStateValue = PlanFactory.resetTableState($scope.tableStateValue);
            ctrl.callServer($scope.tableStateValue);
            $scope.savingStatus = false;
            PlanFactory.modalDismiss();
            toaster.pop('Success', "Success", 'Plan Data Reactivated Successfully');
            //if ($rootScope.support) {
            //    speech.sayText("Plan Data Reactivated Successfully", $rootScope.config);
            //}
        },
        function (error) {
            $scope.savingStatus = false;
            PlanFactory.modalDismiss();
            toaster.pop('error', "", 'Error occured while reactivating a plan data.. Please try after sometime');
            //if ($rootScope.support) {
            //    speech.sayText("Error occured while reactivating a plan data.. Please try after sometime", $rootScope.config);
            //}
        });
    }


    $scope.AddPlanContractData = function () {
        $scope.savingStatus = true;
        var PlanContracts = PlanFactory.FormatPlanContractDataTOBESaved($scope.BE_LOB_Maps);
        if ($scope.CurrentPageMasterViewData.Type == "Add") {
            PlanService.AddPlanContracts(PlanContracts).then(function (result) {
                $scope.CancelAddPlanContracts();
                $scope.savingStatus = false;
                PlanFactory.modalDismiss();
                
                toaster.pop('Success', "Success", 'Plan Contract data added successfully');
                //if ($rootScope.support) {
                //    speech.sayText("Plan Contract data added successfully", $rootScope.config);
                //}
            },
        function (error) {
           
            $scope.CancelAddPlanContracts();
            $scope.savingStatus = false;
            PlanFactory.modalDismiss();
            toaster.pop('error', "", 'Unable to add the Plan Contract Data...  Please try after sometime');
            //if ($rootScope.support) {
            //    speech.sayText("Unable to add the Plan Contract Data...  Please try after sometime", $rootScope.config);
            //}
        });
        }
        else {
            PlanService.UpdatePlanContracts($scope.PlanIDForEdit, PlanContracts).then(function (result) {
                
                $scope.CancelAddPlanContracts();
                $scope.savingStatus = false;
                PlanFactory.modalDismiss();
                toaster.pop('Success', "Success", 'Plan Contract data updated successfully');
                //if ($rootScope.support) {
                //    speech.sayText("Plan Contract data updated successfully", $rootScope.config);
                //}
            },
            function (error) {
                
                $scope.CancelAddPlanContracts();
                $scope.savingStatus = false;
                PlanFactory.modalDismiss();
                toaster.pop('error', "", 'Unable to update the Plan Contract Data...  Please try after sometime');
                //if ($rootScope.support) {
                //    speech.sayText("Unable to update the Plan Contract Data...  Please try after sometime", $rootScope.config);
                //}
            });
        }

    }
   
    $scope.UpdatePlanContractData = function (PlanID, PlanName) {
        $scope.TempPlanName = PlanName;
        $scope.LoadingStatus = false;
        $scope.PlanIDForEdit = PlanID;
        PlanService.GetPlanContarctDataByID(PlanID).then(function (result) {
            if (result.data.Status == "true") {
                $scope.CurrentPageMasterViewData = $filter('MasterDataResetforPage')('Edit');
                $scope.selectedPlan = PlanName;
                $scope.BE_LOB_Maps = angular.copy(PlanFactory.FormatContractDataForEdit(BEValues, result.data.PlanContractDataByID.PLanLOB, result.data.PlanContractDataByID.PLanContract));
                $scope.addTabStatus = 2;
                $scope.tabStatus = 2;
                $scope.LoadingStatus = true;

            }
            else {
                $scope.LoadingStatus = true;
                toaster.pop('error', "", 'Unable to get the Plan Contract Data...  Please try after sometime');
            }
        },
            function (error) {
                $scope.LoadingStatus = true;
                toaster.pop('error', "", 'Unable to get the Plan Contract Data...  Please try after sometime');
            });
    }


    //==============================  Plan Module CRUD Operations End ======================================================================




    //==============================  Master Data Loaded For Plan Start===========================================================================



    $scope.LoadMasterDataForPlan = function () {
        PlanService.MasterDataForPlan().then(function (result) {
            LOBValues = result.data.PlanMasterDataDTO.LOBs;
            $scope.Lobs = result.data.PlanMasterDataDTO.LOBs;
            $scope.MasterLOBs = result.data.PlanMasterDataDTO.LOBs;
            BEValues = result.data.PlanMasterDataDTO.BusinessEnities;
            //toaster.pop('Success', "Success", 'Master Data For Plan Loaded Successfully Check console log for the Master data');
        },
        function (error) {
            //toaster.pop('error', "", 'Error occured while Loading MasterData for Plans');
        })
    }

    //==============================  Master Data Loaded For Plan END===========================================================================




    //==============================  Location Data  For Plan START===========================================================================

    $scope.GetLocationsForPlan = function (cityName) {
        PlanService.getLocations(cityName).then(function (result) {
            toaster.pop('Success', "Success", 'Please check console for Location data');
        },
        function (error) {
            toaster.pop('error', "", 'No records found for this Location');
        })
    }

    //==============================  Location Date  For Plan END===========================================================================



    //==============================  File Upload Component Start ======================================================================

    var formdata = new FormData();
    $scope.getTheFiles = function ($files) {
        angular.forEach($files, function (value, key) {
            formdata.append(key, value);
        });
    };

    //==============================  File Upload Component End ======================================================================


    //==============================  Add Plan Operations Start  =====================================================================


    $scope.AddPlanDataInitiated = function () {
        $scope.TempPlanName = "";
        $scope.tempInactiveLOBs = [];
        $scope.PlanNameCheckMessage = false;
        $scope.tempPlanAddEditObject = angular.copy(AddingPlanDataInitialization);
        $scope.tempPlanAddEditObject.ContactDetails[0].ContactPersonName = [];
        $scope.PreferredContacts = angular.copy(PreferredContacts);
        $scope.Lobs = angular.copy(LOBValues);
        $scope.CurrentPageMasterViewData = $filter('MasterDataResetforPage')('Add');
    }
    $scope.EditPlanDataInitiated = function (ID) {
        $scope.TempPlanName = "";
        $scope.tempInactiveLOBs = [];
        $scope.LoadingStatus = false;
        $scope.PreferredContacts = angular.copy(PreferredContacts);
        PlanService.ViewPlanDataByID(ID).then(function (result) {
            $scope.tempPlanAddEditObject = PlanFactory.FormatPlanData(result.data.PlanData.PlanData);
            $scope.TempPlanName = $scope.tempPlanAddEditObject.PlanName;
            $scope.tempPlanAddEditObject.PlanLOBs = $scope.ConstructProvidersCountObject($scope.tempPlanAddEditObject.PlanLOBs, result.data.PlanData.ProvidersCount);
            $scope.tempPlanAddEditObject.ContactDetails[0].ContactPersonName = $scope.ParsePlanContactPersonName($scope.tempPlanAddEditObject.ContactDetails[0].ContactPersonName) == null ? [] : $scope.ParsePlanContactPersonName($scope.tempPlanAddEditObject.ContactDetails[0].ContactPersonName);
            $scope.tempInactiveLOBs = jQuery.grep($scope.tempPlanAddEditObject.PlanLOBs, function (ele) { return ele.Status == "Inactive" });
            $scope.tempPlanAddEditObject.PlanLOBs = jQuery.grep($scope.tempPlanAddEditObject.PlanLOBs, function (ele) { return ele.Status != "Inactive" });
            $log.log($scope.tempPlanAddEditObject);
            $rootScope.PlanNameForCheck = angular.copy($scope.tempPlanAddEditObject.PlanName);
            $scope.LOBCount = $scope.tempPlanAddEditObject.PlanLOBs.length;
            $scope.PlanIDForEdit = $scope.tempPlanAddEditObject.PlanID;
            $scope.tabStatus = 0;
            $scope.CurrentPageMasterViewData = $filter('MasterDataResetforPage')('Edit');
            $timeout(function () { PlanFactory.TablesHideOnLoad()});
            $scope.LoadingStatus = true;
            //toaster.pop('Success', "Success", 'Please check console for Plan data');
        },
        function (error) {
            $scope.LoadingStatus = true;
            //toaster.pop('error', "", 'Unable to get the plan Data...  Please try after sometime');
        });

    }

    $scope.ConstructProvidersCountObject = function (PlanLOBs, Providerscount) {
        jQuery.grep(PlanLOBs, function (ele) {
            for (var i in Providerscount) {
                if (ele.LOBID == Providerscount[i].LobId) { ele.ProviderCount = Providerscount[i].Providers; ele.ContractsCount = Providerscount[i].ContractsCount; break; }
            }
        });
        return PlanLOBs;
    }

    $scope.CancelAddPlanContracts = function () {
        $scope.tabStatus = 0;
        $scope.addTabStatus = 0;
        $scope.selectedPlan = "";
        $scope.BE_LOB_Maps = [];
        $scope.CurrentPageMasterViewData = angular.copy($scope.temporarymaster);
    }

    $scope.CancelAdd = function () {
        var formElement = angular.element("#Plandata");
        PlanFactory.resetFormValidation(formElement);
        $scope.CurrentPageMasterViewData = $scope.ActiveStatus ? $filter('MasterDataResetforPage')('Master') : $filter('MasterDataResetforPage')('History');
        $scope.tempPlanAddEditObject = null;
        $scope.tempObject1 = {};
        $scope.Lobs = {};
        $scope.PreferredContacts = [];
        $scope.PreferredContactTypesArray = [];
        $scope.tabStatus = 0;
        $scope.addTabStatus = 0;
        $scope.visibilityControl = "";
        PlanFactory.PlanLogoReset();

    }

    $scope.ProceedAdd = function () {
        $scope.TempPlanName = $scope.tempPlanAddEditObject.PlanName;
        var formElement = angular.element("#Plandata");
        PlanFactory.resetFormValidation(formElement);
        validationStatus = formElement.valid();
        var TempContactPersonArray = [];
        jQuery.grep($scope.tempPlanAddEditObject.ContactDetails[0].ContactPersonName, function (ele) { ele = ele.trim(); ele != "" ? TempContactPersonArray.push(ele) : "" });
        $scope.tempPlanAddEditObject.ContactDetails[0].ContactPersonName = [];
        $scope.tempPlanAddEditObject.ContactDetails[0].ContactPersonName = TempContactPersonArray;
        if (validationStatus && !$scope.PlanNameCheckMessage) {
            $scope.visibilityControl = "";
            if ($scope.addTabStatus == 0 || $scope.tabStatus == 0) {
                if ($scope.CurrentPageMasterViewData.Type == "Add") {
                    $scope.tempPlanAddEditObject.PlanLOBs = [];
                    for (var i in $scope.Lobs) {
                        $scope.tempPlanAddEditObject.PlanLOBs.push({
                            LOBID: $scope.Lobs[i].LOBID,
                            LOB: $scope.Lobs[i],
                            LOBSDATA: {
                                LOBs: $scope.Lobs,
                            },
                            StatusType: 1,
                            SubPlans: [{
                                SubPlanId: 0,
                                SubPlanName: "",
                                SubPlanCode: "",
                                SubPlanDescription: "",
                                StatusType: 1,
                            }],
                            LOBAddressDetails: angular.copy($scope.tempPlanAddEditObject.Locations),
                            LOBContactDetails: angular.copy($scope.tempPlanAddEditObject.ContactDetails),
                            SELECTEDLOB: {}
                        });
                        $scope.tempPlanAddEditObject.PlanLOBs[i].LOBContactDetails[0].ContactPersonName = "";
                        $scope.tempPlanAddEditObject.PlanLOBs[i].ReCredentialingDuration = ($scope.Lobs[i].LOBID == 1 || $scope.Lobs[i].LOBID == 2) ? 5 : 3;
                    }
                }
                else {
                    $scope.tempPlanAddEditObject.LOBSDATA = {
                        LOBs: $scope.Lobs,
                    },
                    $scope.tempPlanAddEditObject.SELECTEDLOB = {};
                    for(i in $scope.tempPlanAddEditObject.PlanLOBs){
                        if ($scope.tempPlanAddEditObject.PlanLOBs[i].LOBAddressDetails.length == 0) {
                            $scope.tempPlanAddEditObject.PlanLOBs[i].LOBAddressDetails.push({ LOBAddressDetailID: 0, Street: "", City: "", Appartment: "", State: "", Country: "", County: "", StatusType: 1 });
                        }
                        if ($scope.tempPlanAddEditObject.PlanLOBs[i].LOBContactDetails.length == 0) {
                            $scope.tempPlanAddEditObject.PlanLOBs[i].LOBContactDetails.push({ ContactDetail: { PhoneDetails: [], EmailIDs: [], PreferredWrittenContacts: [], PreferredContacts: [] }, LOBContactDetailID: 0, ContactPersonName: "", StatusType: 1 });
                        }
                    }
                }
            }
            $scope.addTabStatus = 1;
            $scope.tabStatus = 1;
        }
    }

    $scope.BackAdd = function () {
        $scope.tabStatus--;
    }

    $scope.tabAddSelect = function (tabStatus) {
        var formElement = angular.element("#Plandata");
        PlanFactory.resetFormValidation(formElement);
        validationStatus = formElement.valid();
        if (validationStatus && !$scope.PlanNameCheckMessage) {
            $scope.tabStatus = tabStatus;
        }
        
    }

    $scope.AddingPlanData = function () {
        $scope.TempPlanName = $scope.tempPlanAddEditObject.PlanName;
        $scope.savingStatus = false;
    }

    //==============================  Add Plan Operations Start  =====================================================================


    //==============================  View Plan Operations Start  ======================================================================

    $scope.ViewPLANLOBData = function (PlanLOBID, tablerowID) {
        if ($scope.tempPlanObject.PlanLOBs.filter(function (PlanLOBs) { return PlanLOBs.PlanLOBID == PlanLOBID })[0].tableStatus == true) {
            $scope.CancelLOBView();
            return;
        }
        $scope.tempPlanObject = PlanFactory.FormatPlanData($scope.tempPlanObject);
        $scope.tempPlanObject.PlanLOBs.filter(function (PlanLOBs) { return PlanLOBs.PlanLOBID == PlanLOBID })[0].tableStatus = true;
        $scope.tableIDToScroll = "#tableID" + tablerowID;

    }

    $scope.CancelLOBView = function () {
        $scope.tempPlanObject = PlanFactory.FormatPlanData($scope.tempPlanObject);
    }

    $scope.ProceedView = function () {
        if ($scope.tabStatus == 0 || $scope.tabStatus == 2) {
            $scope.CancelLOBView();
        }
        $scope.tabStatus++;
    }

    $scope.BackView = function () {
        if ($scope.tabStatus == 1 || $scope.tabStatus == 2) {
            $scope.CancelLOBView();
        }
        $scope.tabStatus--;
    }

    $scope.CancelView = function () {
        $scope.CurrentPageMasterViewData = $scope.ActiveStatus ? $filter('MasterDataResetforPage')('Master') : $filter('MasterDataResetforPage')('History');
        $scope.tabStatus = 0;
    }

    $scope.tabViewSelect = function (tabStatus) {
        if (tabStatus == 1) {
            $scope.CancelLOBView();
        }
        $scope.tabStatus = tabStatus;
    }


    //==============================  View Plan Operations End  ======================================================================


    angular.element(document).click(function (event) {
        if (!angular.element(event.target).hasClass("countryCodeClass") && angular.element(event.target).parents(".countryDailCodeContainer").length === 0) {
            angular.element(".countryDailCodeContainer").hide();
        }
        if (!angular.element(event.target).hasClass("form-control") && angular.element(event.target).parents(".ProviderTypeSelectAutoList").length === 0) {
            angular.element(".ProviderTypeSelectAutoList").hide();
        }
    });

    var changeVisibilityOfCountryCode = function () {
        angular.element(".countryDailCodeContainer").hide();
    };

    angular.element(document).ready(function () {
        angular.element(".countryDailCodeContainer").hide();
        angular.element(".ProviderTypeSelectAutoList").hide();
    });

}])