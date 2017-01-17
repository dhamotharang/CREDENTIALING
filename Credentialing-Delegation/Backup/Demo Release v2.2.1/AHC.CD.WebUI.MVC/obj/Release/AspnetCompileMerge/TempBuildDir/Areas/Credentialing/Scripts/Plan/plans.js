//---------------------------------- author : krglv --------------------------
function isNumber(n) {
    return !isNaN(parseFloat(n)) && isFinite(n);
}

//============================ Angular Module View Controller ==========================
var planApp = angular.module("planApp", ['ngTable']);

//---------------------- angular Saving Directive .................
planApp.directive('saving', function () {
    return {
        restrict: 'E',
        replace: true,
        template: '<div class="loading"><img src="/Content/Images/ajax-loader.gif" width="20" height="20" />Saving...</div>',
        link: function (scope, element, attr) {
            scope.$watch('saving', function (val) {
                if (val)
                    $(element).show();
                else
                    $(element).hide();
            });
        }
    }
});

//--------------------- angular loading directive ------------------
planApp.directive('loading', function () {
    return {
        restrict: 'E',
        replace: true,
        template: '<div class="loading"><img src="/Content/Images/ajax-loader.gif" width="20" height="20" />Loading...</div>',
        link: function (scope, element, attr) {
            scope.$watch('loading', function (val) {
                if (val)
                    $(element).show();
                else
                    $(element).hide();
            });
        }
    }
});

//---------------------- angular Saving Directive .................
planApp.directive('hideshowtoggle', function () {
    return {
        restrict: 'AE',
        link: function (scope, element, attr) {
            element.bind('focus', function () {
                element.parent().find(".ProviderTypeSelectAutoList").first().show();
            });
        }
    }
});

//============================= loading Container Directive =====================================
planApp.directive('loadingContainer', function () {
    return {
        restrict: 'A',
        scope: false,
        link: function (scope, element, attrs) {
            var loadingLayer = angular.element('<div class="loading"></div>');
            element.append(loadingLayer);
            element.addClass('loading-container');
            scope.$watch(attrs.loadingContainer, function (value) {
                loadingLayer.toggleClass('ng-hide', !value);
            });
        }
    };
});

//--------------------- Tool tip Directive ------------------------------
planApp.directive('tooltip', function () {
    return function (scope, elem) {
        elem.tooltip();
    };
});

//---------------------------- service for get location ----------------------
planApp.service('locationService', ['$http', function ($http) {
    this.getLocations = function (QueryString) {
        return $http.get("/Location/GetCities?city=" + QueryString)
        .then(function (response) { return response.data; });
    };
}]);


planApp.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

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
            $timeout(animateMessageAlertOff, 2000);
        }
    }

    $rootScope.closeAlertMessage = function () {
        $rootScope.messageDesc = "";
        $rootScope.activeMessageDiv = "";
        $rootScope.messageType = "";
    }
}])

//============================= Angular controller =====================================
planApp.controller('planController', function ($scope, $timeout, $filter, $location, ngTableParams, $compile, $http, $window, locationService, messageAlertEngine) {


    //============================= Data From Master Data Table Required For Plan ======================

    //------------- get List Of Plan ---------------
    $http.get('/MasterDataNew/GetAllPlans').
   success(function (data, status, headers, config) {
       console.log(data);
       $scope.Plans = data;
       $scope.data = data;
       console.log($scope.data);
          $scope.init_table(data);
      
       
   }).
   error(function (data, status, headers, config) {
       //console.log("Sorry internal master data cont able to fetch.");
   });

    $scope.MasterLOBs = [];

    //------------- get List Of Lobs ---------------
    $http.get('/MasterDataNew/GetAllLobs').
   success(function (data, status, headers, config) {
       $scope.Lobs = angular.copy(data);
       $scope.MasterLOBs = data;
   }).
   error(function (data, status, headers, config) {
       //console.log("Sorry internal master data cont able to fetch.");
   });

    //----------------------------- Get List Of Groups --------------------------
    $http.get('/MasterDataNew/GetAllGroups').
    success(function (data, status, headers, config) {
        $scope.BusinessEntities = data;
    }).
    error(function (data, status, headers, config) {
        //console.log("Sorry internal master data cont able to fetch.");
    });

    //------------- conditional variables -----------
    $scope.IsHistoryView = false;
    $scope.AddNewPlan = false;
    $scope.IsDuplicate = false;
    $scope.IsSuccessSaved = false;
    //$scope.PlanIsEdit = false;
    $scope.CountryDailCodes = countryDailCodes;
    $scope.Plans = [];
    $scope.data = [];
    $scope.visibilityControl = "";

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
        console.log("=========");
        console.log($scope.data);
        if (condition == 1) {
            for (var i = 0; i < $scope.data.length; i++) {
                $scope.data[i].FirstName = ""; //initialization of new property 
                $scope.data[i].LastName = "";
                $scope.data[i].NPINumber = "";
                $scope.data[i].FullTitles = "";
                $scope.data[i].FullRelations = "";

                $scope.data[i].FirstName = $scope.data[i].PersonalDetail.FirstName;  //set the data from nested obj into new property
                $scope.data[i].LastName = $scope.data[i].PersonalDetail.LastName;
                $scope.data[i].NPINumber = $scope.data[i].OtherIdentificationNumber.NPINumber;
                //------------------ for full titles ---------------------
                var fulltitles = "";
                for (var j in $scope.data[i].PersonalDetail.ProviderTitles) {
                    if (j > 0) {
                        fulltitles += ", ";
                    }
                    fulltitles += $scope.data[i].PersonalDetail.ProviderTitles[j].ProviderType.Title;
                }
                $scope.data[i].FullTitles = fulltitles;
                //-------------- for full relations ---------------
                var fullrelations = "";
                for (var j in $scope.data[i].ContractInfoes) {
                    if (j > 0) {
                        fullrelations += ", ";
                    }
                    fullrelations += $scope.data[i].ContractInfoes[j].ProviderRelationship;
                }
                $scope.data[i].FullRelations = fullrelations;
            }
        }
        for (var i = 0; i < $scope.data.length; i++) {
            if ($scope.data[i].ContactDetails.lenght > 0) {
                if ($scope.data[i].ContactDetails[0].ContactPersonName == null) {
                    $scope.data[i].ContactDetails[0].ContactPersonName = "";
                }
                $scope.data[i].ContactPersonName = $scope.data[i].ContactDetails[0].ContactPersonName;
            }
            
            
            if ($scope.data[i].Locations.length > 0) {
                if ($scope.data[i].Locations[0].City == null) {
                    $scope.data[i].Locations[0].City = "";
                }

                $scope.data[i].Address = $scope.data[i].Locations[0].City;

                if ($scope.data[i].Locations[0].Street == null) {
                    $scope.data[i].Locations[0].Street = "";
                }
                $scope.data[i].Address = $scope.data[i].Address + $scope.data[i].Locations[0].Street;

                if ($scope.data[i].Locations[0].ZipCode == null) {
                    $scope.data[i].Locations[0].ZipCode = "";
                }
                $scope.data[i].Address = $scope.data[i].Address + $scope.data[i].Locations[0].ZipCode;





                if ($scope.data[i].Locations[0].Country == null) {
                    $scope.data[i].Locations[0].Country = "";
                }
                $scope.data[i].Address = $scope.data[i].Address + $scope.data[i].Locations[0].Country;

                if ($scope.data[i].Locations[0].County == null) {
                    $scope.data[i].Locations[0].County = "";
                }
                $scope.data[i].Address = $scope.data[i].Address + $scope.data[i].Locations[0].County;

                if ($scope.data[i].Locations[0].Appartment == null) {
                    $scope.data[i].Locations[0].Appartment = "";
                }
                $scope.data[i].Address = $scope.data[i].Address + $scope.data[i].Locations[0].Appartment;

                if ($scope.data[i].Locations[0].State == null) {
                    $scope.data[i].Locations[0].State = "";
                }
                $scope.data[i].Address = $scope.data[i].Address + $scope.data[i].Locations[0].State;

            }

               


        }

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

    };

    //if filter is on
    $scope.ifFilter = function () {
        try {
            var bar;
            var obj = $scope.tableParams1.$params.filter;
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
                return ($scope.tableParams1.$params.page * $scope.tableParams1.$params.count) - ($scope.tableParams1.$params.count - 1);
            }
        }
        catch (e) { }
    }

    //Get index Last in table
    $scope.getIndexLast = function () {
        try {
            if ($scope.groupBySelected == 'none') {
                return { true: ($scope.data.length), false: ($scope.tableParams1.$params.page * $scope.tableParams1.$params.count) }[(($scope.tableParams1.$params.page * $scope.tableParams1.$params.count) > ($scope.data.length))];
            }
        }
        catch (e) { }
    }

    //================= Add New Plan button method present on plan listing page  ===================================================
    $scope.AddNewPlanFunction = function () {
        $scope.Lobs = angular.copy($scope.MasterLOBs);

        $scope.TempBE = angular.copy($scope.BusinessEntities);
        $scope.AddNewPlan = true;
        $scope.PlanIsEdit = false;
        $scope.HeadTitle = 'Add New Plan';
        $scope.tabStatus = 0;
        $scope.AddProviderFormTab0 = true;
        $scope.PlanSubmitButton = 'Save';
        $scope.IsAdd = true;

        //========= Defining structure of tempobject ============
        $scope.tempObject = {
            PlanID: 0,
            PlanCode: "",
            PlanName: "",
            PlanDescription: "",
            IsDelegated: "False",
            PlanLOBs: [],
            AttachedFormID: null,
            StatusType: 1,
            Locations: [{
                Street: "",
                City: "",
                Appartment: "",
                State: "",
                Country: "",
                County: "",
                StatusType: 1,
            }],
            ContactDetails: [{
                ContactPersonName: "",
                EmailAddress: "",
                PhoneNumber: "",
                FaxNumber: "",
                StatusType: 1,
            }]
        };
    };

    $scope.SaveLOBDetail = function () {
        $scope.AddNewPlan = true;
        $scope.saving = false;
        $scope.AddProviderFormTab0 = true;
        $scope.AddProviderFormTab1 = true;
        $scope.AddProviderFormTab2 = true;
        $scope.tabStatus = 2;
    }

    $scope.HeadTitle = "List Of Plans";

    //---------------------------- set selected file CV ---------------------
    $scope.setFiles = function (element) {
        $scope.$apply(function (scope) {
            if (element.files[0]) {
                $scope.selectedCV = element.files[0];
            } else {
                $scope.selectedCV = {};
            }
        });
    };

    $scope.removeFile = function (FileId) {
        if (FileId == "ContractInfo_CVFile") {
            $scope.selectedCV = {};
        } else if (FileId == "ContractInfoes_0__ContractFile") {
            $scope.selectedContract = {};
        }
    };

    //================================= LOB Init data ==============================
    $scope.TempBEsForLOB = [];

    $scope.ConditionLOB = 1;

    $scope.AddNewPlanLOB = false;

    //---------------------- BE select for Plan --------------------
    $scope.SelectBusinessEntity = function (be) {
        $scope.tempObject.PlanBEs.push({
            PlanBEID: 0,
            PlanID: 0,
            GroupID: be.GroupID,
            Plan: {},
            Group: be
            //StatusType: 1
        });
        $scope.TempBE.splice($scope.TempBE.indexOf(be), 1);
        $(".ProviderTypeSelectAutoList").hide();
    };
    //---------------- removed selected BE ----------------------------
    $scope.RemoveBusinessEntity = function (be) {
        $scope.tempObject.PlanBEs.splice($scope.tempObject.PlanBEs.indexOf(be), 1)
        $scope.TempBE.push(be.Group);
        $scope.TempBE.sort(function (a, b) {
            if (a.Name > b.Name) {
                return 1;
            }
            if (a.Name < b.Name) {
                return -1;
            }
            // a must be equal to b
            return 0;
        });
    };

    $scope.SetPlanLOB = function (LOBID, pLob) {
        for (var i in $scope.Lobs) {
            if (LOBID == $scope.Lobs[i].LOBID) {
                pLob.LOB = angular.copy($scope.Lobs[i]);
                break;
            }
        }
    };

    //---------------------- BE select for LOB --------------------
    $scope.SelectBusinessEntityForLOB = function (be) {
        $scope.tempObject1.BEs.push(be);
        $scope.TempBEsForLOB.splice($scope.TempBEsForLOB.indexOf(be), 1);
        $(".ProviderTypeSelectAutoList").hide();
    };
    //---------------- removed selected BE for LOB ----------------------------
    $scope.RemoveBusinessEntityForLOB = function (be) {
        $scope.tempObject1.BEs.splice($scope.tempObject1.BEs.indexOf(be), 1)

        for (var i in $scope.tempObject.PlanBEs) {
            if (be.GroupID == $scope.tempObject.PlanBEs[i].GroupID) {
                $scope.TempBEsForLOB.push(angular.copy($scope.tempObject.PlanBEs[i]));
            }
        }

        $scope.TempBEsForLOB.sort(function (a, b) {
            if (a.Name > b.Name) {
                return 1;
            }
            if (a.Name < b.Name) {
                return -1;
            }
            // a must be equal to b
            return 0;
        });
    };

    //----------------------- duplicate data remove , where i will use not confirm but i will use-------------------
    $scope.getUniqueData = function (parentData, childData) {
        for (var i = 0; i < parentData.lenght;) {
            var temp = false;
            for (var j in childData) {
                if (parentData[i].Group.GroupID == childData[j].Group.GroupID) {
                    temp = true;
                }
            }
            if (temp) {
                parentData.splice(i, 1);
            } else {
                i++;
            }
        }
    };

    //----------------------------------------- get same Address ----------------------------
    $scope.GetAddress = function (condition) {
        if (condition) {
            $scope.tempObject1.AddressDetails[0] = angular.copy($scope.tempObject.Locations[0]);
        } else {
            $scope.tempObject1.AddressDetails[0] = {};
        }
        $scope.tempObject1.AddressDetails[0].IsSameAddress = condition;
    };

    //----------------------------------------- get same Contact Details ----------------------------
    $scope.GetContactPerson = function (condition) {
        if (condition) {
            $scope.tempObject1.ContactDetails[0] = angular.copy($scope.tempObject.ContactDetails[0]);

        } else {
            $scope.tempObject1.ContactDetails[0] = {};
        }
        $scope.tempObject1.ContactDetails[0].IsSameContatcPerson = condition;
    };

    //------------------------------------ add sub plan -----------------
    $scope.AddSubPlan = function () {
        $scope.tempObject1.SubPlans.push({
            SubPlanId: 0,
            SubPlanName: "",
            SubPlanCode: "",
            SubPlanDescription: "",
            Status: 1
        });
    };
    //---------------------------------- remove subPlan --------------------------
    $scope.RemoveSubPlan = function (index) {
        $scope.tempObject1.SubPlans.splice(index, 1);
    };


    //------------------------------------ add plan contact -----------------
    $scope.AddPlanContact = function () {
        $scope.tempObject.ContactDetails.push({
            ContactPersonName: "",
            EmailAddress: "",
            PhoneNumber: "",
            FaxNumber: ""
        });
    };
    //---------------------------------- remove plan contact --------------------------
    $scope.RemovePlanContact = function (index) {
        $scope.tempObject.ContactDetails.splice(index, 1);
    };

    //------------------------------------ add plan address -----------------
    $scope.AddPlanAddress = function () {
        $scope.tempObject.Locations.push({
            Street: "",
            City: "",
            Appartment: "",
            State: "",
            Country: "",
            County: ""
        });
    };
    //---------------------------------- remove plan address --------------------------
    $scope.RemovePlanAddress = function (index) {
        $scope.tempObject.Locations.splice(index, 1);
    };

    //------------------------------------ add lob contact -----------------
    $scope.AddLobContact = function () {
        $scope.tempObject1.ContactDetails.push({
            ContactPersonName: "",
            EmailAddress: "",
            PhoneNumber: "",
            FaxNumber: ""
        });
    };
    //---------------------------------- remove lob contact --------------------------
    $scope.RemoveLobContact = function (index) {
        $scope.tempObject1.ContactDetails.splice(index, 1);
    };

    //------------------------------------ add lob address -----------------
    $scope.AddLobAddress = function () {
        $scope.tempObject1.AddressDetails.push({
            Street: "",
            City: "",
            Appartment: "",
            State: "",
            Country: "",
            County: ""
        });
    };
    //---------------------------------- remove lob address --------------------------
    $scope.RemoveLobAddress = function (index) {
        $scope.tempObject1.AddressDetails.splice(index, 1);
    };

    //------------------ add new LOB --------------------
    $scope.AddNewLOB = function (condition) {
        var temp = {
            LOBID: 0,
            StatusType: 1,
            SubPlans: [{
                SubPlanId: 0,
                SubPlanName: "",
                SubPlanCode: "",
                SubPlanDescription: "",
                StatusType: 1,
            }],
            AddressDetails: [{
                Street: "",
                City: "",
                Appartment: "",
                State: "",
                Country: "",
                County: "",
                StatusType: 1,
            }],
            ContactDetails: [{
                ContactPersonName: "",
                EmailAddress: "",
                PhoneNumber: "",
                FaxNumber: "",
                StatusType: 1,
            }]
        };
        $scope.tempObject.PlanLOBs.push(temp);
        $scope.tempObject1 = angular.copy(temp);
        $scope.tempObject1.AddressDetails[0] = angular.copy($scope.tempObject.Locations[0]);
        $scope.tempObject1.ContactDetails[0] = angular.copy($scope.tempObject.ContactDetails[0]);
        $scope.IsNewLOB = true;
        $scope.visibilityControl = condition;
        $scope.GetAddEditLobData($scope.tempObject.PlanLOBs, null);
    };

    //------------------ Edit new LOB --------------------
    $scope.EditLOB = function (condition, tempObject1, index) {
        $scope.lobindex = index;
        $scope.IsNewLOB = false;
        $scope.visibilityControl = condition;
        $scope.tempObject1 = angular.copy(tempObject1);
        $scope.ConditionLOB = 2;
        $scope.AddNewPlanLOB = true;
        $scope.GetAddEditLobData($scope.tempObject.PlanLOBs, tempObject1);
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
        $scope.Lobs = angular.copy(pendingLOBs);

    };

    //------------------View new LOB--------------------------
    $scope.ViewLOB = function (condition, tempObject1, index) {
        $scope.lobindex = index;
        $scope.IsNewLOB == false;
        $scope.visibilityControl = condition;
        $scope.tempObjectPLOB = angular.copy(tempObject1);
        $scope.ConditionLOB = 2;
        $scope.AddNewPlanLOB = true;
    };

    $scope.CancelNewLOBEdit = function () {
        if ($scope.IsNewLOB == true) {
            $scope.tempObject.PlanLOBs.pop();
        }
        $scope.visibilityControl = "";
        $scope.tempObject1 = {};
        $scope.IsNewLOB = false;
    };

    $scope.CancelLOBEdit = function () {
        if ($scope.IsNewLOB == true) {
            $scope.tempObject.PlanLOBs.pop();
            $scope.visibilityControl = "";
            $scope.tempObject1 = {};
        } else {
            $scope.visibilityControl = "";
            $scope.tempObject1 = {};
        }
    };

    $scope.SaveLOB = function (FormID, tempObject1) {
        console.log(tempObject1);

        for (var i in $scope.Lobs) {
            if (tempObject1.LOBID == $scope.Lobs[i].LOBID) {
                tempObject1.LOB = $scope.Lobs[i];
                break;
            }
        }

        $formData = $("#" + FormID);
        ResetFormForValidation($formData);
        if ($formData.valid()) {
            if ($scope.IsNewLOB == true) {
                $scope.tempObject.PlanLOBs[$scope.tempObject.PlanLOBs.length - 1] = angular.copy(tempObject1);
            } else {
                $scope.tempObject.PlanLOBs[$scope.lobindex] = angular.copy(tempObject1);
            }
            $scope.visibilityControl = "";
            $scope.tempObject1 = {};
        }
    };

    //------------------ Remove new LOB --------------------
    $scope.RemoveLOB = function (index) {
        $scope.tempObject.PlanLOBs.splice(index, 1);
        $scope.tempObject1 = {};
    };

    //------------------------- Selecting of Group Information --------------------------
    $scope.SelectPracticingGroup = function (practicingGroup) {
        $scope.ContractGroupInfoes.push({
            ContractGroupInfoId: null,
            PracticingGroupId: practicingGroup.PracticingGroupID,
            PracticingGroup: practicingGroup,
            StatusType: 1
        });
        $scope.PracticingGroups.splice($scope.PracticingGroups.indexOf(practicingGroup), 1);
        $scope.searchGroupName = "";
    };
    //---------------- removed selected Group Information ----------------------------
    $scope.RemoveContractGroupInfo = function (contractGroupInfo) {
        $scope.ContractGroupInfoes.splice($scope.ContractGroupInfoes.indexOf(contractGroupInfo), 1)
        $scope.PracticingGroups.push(contractGroupInfo.PracticingGroup);
    };
    //----------------------------------------- address data fetch from database --------------------
    $scope.addressHomeAutocomplete = function (location, pl) {
        console.log(location);
        pl.City = location;
        if (location.length > 1 && !angular.isObject(location)) {
            locationService.getLocations(location).then(function (val) {
                $scope.Locations = val;
            });
        } else if (angular.isObject(location)) {
            $scope.setAddressModels(location, pl);
        }
    };
    $scope.selectedLocation = function (location, pl) {
        $scope.setAddressModels(location, pl);
        $(".ProviderTypeSelectAutoList").hide();
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

    //========================== View Popover Contact Country Code ============================
    $scope.showContryCodeDiv = function (div_Id) {
        $("#" + div_Id).show();
    };

    //================================= Tab Flow Validation Method ==============================
    $scope.nextForm = function (tabid) {
        $scope.tabStatus = tabid;
    };
    //================================= Primary info form view ===================================
    $scope.primaryInfoForm = function (Form_Id) {
        if ($scope.tempObject.PlanLOB && $scope.tempObject.PlanLOB.length > 0) {
            $scope.AddNewPlanLOB = false;
        } else {
            $scope.AddNewPlanLOB = false;
            //$scope.AddNewLOB();
            //--------------------------- New Requirement 10/06/2015, According Data Model UI ---------------------------
            for (var i in $scope.Lobs) {
                $scope.tempObject.PlanLOBs.push({
                    LOBID: $scope.Lobs[i].LOBID,
                    LOB: $scope.Lobs[i],
                    BEs: angular.copy($scope.tempObject.PlanBEs),
                    Locations: angular.copy($scope.tempObject.Locations),
                    ContactDetails: angular.copy($scope.tempObject.ContactDetails)
                });
            }
        }
        console.log($scope.tempObject);
        $scope.AddProviderFormTab1 = true;
        $scope.tabStatus = 1;
    };

    //================================= Contract Information ===================================
    $scope.ContractInfoForm = function (Form_Id) {
        if ($scope.ConditionLOB == 1) {
            $scope.tempObject.PlanLOBs.push(angular.copy($scope.tempObject1));

        } else if ($scope.ConditionLOB == 2) {
            $scope.tempObject.PlanLOBs[$scope.SelectedLOBindex] = angular.copy($scope.tempObject1);
        }

        $scope.AddNewPlanLOB = false;
        $scope.tempObject1 = {};
        $scope.ConditionLOB == 0;
        console.log($scope.tempObject.PlanLOBs);

        $('body,html').animate({
            scrollTop: 0
        });
    };

    //================================= Create Provider Profile Preview Method ==============================
    $scope.previewAddProvider = function (Form_Id) {
        $scope.AddProviderFormTab2 = true;
        $scope.tabStatus = 2;
    };

    //============================ Add - confirmation method for submit ===========================
    $scope.confirmationAddPlan = function (formStates) {
        $scope.ConfirmTitle = 'Confirm - Add New Plan';
        $scope.ConfirmMessage = 'Are you sure, you want to Add New Plan?';
        $('#myModal').modal({
            backdrop: 'static'
        });
    };


    $scope.confirmationAddPlanContract = function (formStates) {
        $scope.ConfirmTitle1 = 'Confirm - Add New Plan Contract';
        $scope.ConfirmMessage1 = 'Are you sure, you want to Add New Plan Contract?';
        $('#myModal1').modal({
            backdrop: 'static'
        });
    };

    //============================ Update - confirmation method for submit ===========================
    $scope.confirmationUpdatePlan = function (formStates) {
        $scope.ConfirmTitle = 'Confirm - Update Plan';
        $scope.ConfirmMessage = 'Are you sure, you want to Update Plan?';
        $('#myModal').modal({
            backdrop: 'static'
        });
    };

    $scope.confirmationRemovePlan = function (plan) {
        $('#planListModal').modal({
            backdrop: 'static'
        });
        $scope.planid = plan.PlanID;
    };

    $scope.PlanContract = function (plandata) {
        console.log(plandata);
        console.log(plandata.PlanID);
        $http.post('/Credentialing/Plan/GetPlanContractForPlan', { PlanId: plandata.PlanID }).
        success(function (data, status, headers, config) {
            console.log(data);
            $scope.plancontractviewdata = data;
            $scope.formatDataForView($scope.BusinessEntities, plandata.PlanLOBs, $scope.plancontractviewdata);
        }).
   error(function (data, status, headers, config) {
       //console.log("Sorry internal master data cont able to fetch.");
   });

    };

    //----------- method for proceed button -----------------
    $scope.ProceedAdd = function (plandatacheckforplancode) {
        $formData = $('#PlanForm');
        ResetFormForValidation($formData);
        validationStatus = $formData.valid();
        if (validationStatus) {

            $http.post('/Credentialing/Plan/IsPlanCodeExist', { planCode: plandatacheckforplancode.PlanCode }).
        success(function (data, status, headers, config) {
            console.log(data);
            if (data == "False") {
                $scope.AddNewPlan = true;
                $scope.saving = false;
                $scope.AddProviderFormTab0 = false;
                $scope.AddProviderFormTab1 = true;
                $scope.AddProviderFormTab2 = false;
                $scope.tabStatus = 1;

                // initialising Master Lobs in Add new plan
                for (var i in $scope.Lobs) {
                    $scope.tempObject.PlanLOBs.push({
                        LOBID: $scope.Lobs[i].LOBID,
                        LOB: $scope.Lobs[i],
                        StatusType: 1,
                        SubPlans: [{
                            SubPlanId: 0,
                            SubPlanName: "",
                            SubPlanCode: "",
                            SubPlanDescription: "",
                            StatusType: 1,
                        }],
                        AddressDetails: angular.copy($scope.tempObject.Locations),
                        ContactDetails: angular.copy($scope.tempObject.ContactDetails)
                    });
                }
            }
            else {
                messageAlertEngine.callAlertMessage('PlanCodeSame', "Plan Code Already Exist. !!!!", "success", true);
            }

        }).
   error(function (data, status, headers, config) {
       //console.log("Sorry internal master data cont able to fetch.");
   });



        }
    }

    $scope.ProceedUpdate = function () {
        $scope.AddNewPlan = true;
        $scope.saving = false;
        $scope.AddProviderFormTab0 = false;
        $scope.AddProviderFormTab1 = true;
        $scope.AddProviderFormTab2 = false;
        $scope.tabStatus = 1;
    }

    $scope.ProceedView = function () {
        $scope.AddProviderFormTab0 = false;
        $scope.AddProviderFormTab1 = true;
        $scope.AddProviderFormTab2 = false;
        $scope.tabStatus = 1;
    }


    $scope.ProceedViewContract = function () {
        $scope.AddProviderFormTab0 = true;
        $scope.AddProviderFormTab1 = true;
        $scope.AddProviderFormTab2 = true;
        $scope.tabStatus = 2;
        $scope.showDiv = true;
    }


    $scope.ViewPlanContract = function (plan) {
        $scope.ViewSelectedPlan(plan);
        $scope.AddProviderFormTab0 = false;
        $scope.AddProviderFormTab1 = false;
        $scope.AddProviderFormTab2 = true;
        $scope.tabStatus = 2;
        $scope.showDiv = true;
    }


    //============================ modal confirmation action And Add new Plan Method ===========================
    $scope.PlanLOBs = [];
    $scope.confirmPlan = function () {
        $.ajax({
            type: "POST",
            url: "/Credentialing/Plan/AddPlan",
            data: JSON.stringify({ planViewModel: $scope.tempObject }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                console.log(data);
                $scope.PlanLOBs = data.LobDetail.Data;
                console.log(data.LobDetail.Data);
                $('#myModal').modal('hide');
                if (data.status == "true") {
                    $timeout(function () {
                        $scope.AddNewPlan = true;
                        $scope.saving = false;
                        $scope.AddProviderFormTab0 = true;
                        $scope.AddProviderFormTab1 = true;
                        $scope.AddProviderFormTab2 = true;
                        $scope.tabStatus = 2;
                        $scope.showDiv = true;
                        $scope.IsAdd = true;
                        $scope.formatData($scope.BusinessEntities, $scope.PlanLOBs);
                    }, 1000);
                }
                messageAlertEngine.callAlertMessage('PlanAdded', "Plan Information Added Successfully. !!!!", "success", true);
            }
        });


    };

    //=========== To remove a plan =============
    $scope.RemoveSelectedPlan = function () {
        console.log($scope.planid);
        $.ajax({
            type: "POST",
            url: "/Credentialing/Plan/RemovePlan",
            data: JSON.stringify({ planid: $scope.planid }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                console.log(data);
                $('#myModal').modal('hide');
                if (data.status == "true") {
                    $window.location.reload();

                }
            },
            failure: function (errMsg) {
                console.log(data);
            }
        });

    };


    //------------------ time out function call -------------------------
    var NextFunctionCall = function () {
        $('#myModal').modal('hide');
        $scope.CancelAdd();
    }

    //------------------------------ return is array check status ----------------------------
    $scope.dataIsArray = function (data) {
        return angular.isArray(data);
    };


    //-------------- loading profile URL --------------------
    $scope.ViewProfile = function () {
        $scope.loading = true;
        $timeout(Redirect, 2000);
    }

    //----------------- Redirect URL ------------------
    function Redirect() {
        window.location = $scope.profileURL;
    }

    //------------------------ save new data ----------------------
    //$scope.SaveData = function (data) {
    //    var group = $filter('filter')($scope.Groups, {
    //        GroupID: data.GroupID
    //    })[0];
    //    var plan = $filter('filter')($scope.Plans, {
    //        PlanID: data.PlanID
    //    })[0];

    //    var temp = {
    //        BEPlanMappingID: 0,
    //        PlanID: data.PlanID,
    //        GroupID: data.GroupID,
    //        MappedByID: 1,
    //        ChangedByID: null,
    //        StatusType: 1
    //    };

    //    $http.post('/Credentialing/BusinessEntity/AddBEPlan', {
    //        BussinessEntityPlanMapping: temp
    //    }).
    //    success(function (data, status, headers, config) {
    //        console.log(data);
    //        if (data.status == 'true') {
    //            //----------- temp push controller back-end dependency -------------------
    //            $scope.BEPlanMappings.push({
    //                BEPlanMappingID: $scope.BEPlanMappings.length,
    //                PlanID: data.PlanID,
    //                GroupID: data.GroupID,
    //                Plan: plan,
    //                Group: group,
    //                MappedByID: 1,
    //                MappedBy: Users[_.random(0, 2)],
    //                ChangedByID: 1,
    //                ChangedBy: Users[_.random(0, 2)],
    //                Status: "Active",
    //                LastModifiedDate: new Date(_.random(2013, 2015), _.random(0, 11), _.random(1, 28))
    //            });
    //            $scope.ShowMessage('Success', 'Data Mapping Successfully !!!!!!!');
    //            $scope.CancelAdd();
    //        } else {
    //            $scope.ShowMessage('Error', data.status);
    //        }
    //    }).
    //    error(function (data, status, headers, config) {
    //        $scope.ShowMessage('Error', data.status);
    //    });
    //};

    //------------------------ cancel add data ----------------------
    $scope.CancelAdd = function () {
        $scope.AddNewPlan = false;
        $scope.saving = false;
        $scope.EditPlan = false;
        $scope.AddProviderFormTab0 = true;
        $scope.AddProviderFormTab1 = false;
        $scope.AddProviderFormTab2 = false;
        //$scope.AddProviderFormTab3 = false;
        //$scope.AddProviderFormTab4 = false;
        $scope.tabStatus = 0;
    };

    //------------ cancel Lob Information --------------
    $scope.cancelNewPlanLOB = function (index) {
        $scope.AddNewPlan = true;
        $scope.saving = false;
        $scope.AddProviderFormTab0 = true;
        $scope.AddProviderFormTab1 = false;
        $scope.AddProviderFormTab2 = false;
        //$scope.AddProviderFormTab3 = false;
        //$scope.AddProviderFormTab4 = false;
        $scope.tabStatus = 0;
       // $scope.tempObject.PlanLOBs = {};
        $scope.Lobs = {}
    };

    $scope.cancelViewPlanLOB = function (index) {
        $scope.AddProviderFormTab0 = true;
        $scope.AddProviderFormTab1 = false;
        $scope.AddProviderFormTab2 = false;
        $scope.tabStatus = 0;
    };

    //------------------------------ View Business Entity Plan History Data ---------------------------
    $scope.HistoryViewed = false;
    $scope.ViewHistory = function () {
        $scope.IsHistoryView = true;
        $scope.HistoryViewed = true;

        $http.get('/Credentialing/BusinessEntity/GetAllBussinessEntityPlanMappingHistories').
        success(function (data, status, headers, config) {
            console.log(data);
            //$scope.BEPlanMappingHistories = data.BEPlanMappingHistories;
            $scope.BEPlanMappingHistories = [];
        }).
        error(function (data, status, headers, config) {
            $scope.ShowMessage('Error', data.status);
        });
    };

    //------------------------------ Hide History function ---------------------------
    $scope.HideHistory = function () {
        $scope.IsHistoryView = false;
    };

    //------------------------------ Remove Modal Show data ---------------------------
    $scope.RemoveBE = function (data) {
        $scope.SeletcedData = data;
        $("#BEConfirmation").modal('show');
    };

    //------------------------------ Remove Confirmation data ---------------------------
    $scope.Confirmation = function (data1) {
        $http.post('/Credentialing/BusinessEntity/RemoveBEPlan', {
            BEPlanMappingID: data1.BEPlanMappingID
        }).
        success(function (data, status, headers, config) {
            console.log(data);
            if (data.status == 'true') {
                //----------- temp push controller back-end dependency -------------------
                $scope.BEPlanMappings.splice($scope.BEPlanMappings.indexOf(data1), 1);
                $scope.SeletcedData = {};
                $("#BEConfirmation").modal('hide');
                $scope.ShowMessage('Success', 'Data Removed Successfully !!!!!!!');

                if ($scope.HistoryViewed) {

                    $scope.BEPlanMappingHistories.push(data.BussinessEntityPlanMapping);
                }
            } else {
                $scope.ShowMessage('Error', data.status);
            }
        }).
        error(function (data, status, headers, config) {
            $scope.ShowMessage('Error', data.status);
        });
    };

    //------------------------------ duplicate check data ---------------------------
    $scope.CheckDuplicateData = function (data) {
        var mappings = $filter('filter')($scope.BEPlanMappings, {
            GroupID: data.GroupID,
            PlanID: data.PlanID
        });
        if (mappings.length > 0) {
            $scope.IsDuplicate = true;
        } else {
            $scope.IsDuplicate = false;
        }
    };

    //----------------- show success message --------------------
    $scope.ShowMessage = function (messageType, message) {
        $scope.IsSuccessSaved = true;
        $scope.BEPlanMappingMessageType = messageType;
        $scope.BEPlanMappingMessage = message;

        $timeout(function () {
            $scope.IsSuccessSaved = false;
        }, 5000);
    };


    //======================================= Edit plan starts=================================
    $scope.EditPlan = false;
    $scope.tempEditObject = null;
    $scope.EditSelectedPlan = function (plan) {
        console.log(plan);
        $scope.tempObject = plan;
        $scope.tempEditObject = plan;

        $scope.PlanIsEdit = true;
        $scope.AddNewPlan = true;
        $scope.HeadTitle = 'Edit Plan';
        $scope.tabStatus = 0;
        $scope.AddProviderFormTab0 = true;

        //$scope.HeadTitle = "Edit Plan";
        $scope.ViewPlan = false;
        $scope.EditPlan = true;
    };

    $scope.CalcelEditing = function () {
        $scope.EditPlan = false;
    };

    $scope.UpdatePlan = function (plan) {

        $.ajax({
            type: "POST",
            url: "/Credentialing/Plan/UpdatePlanAsync",
            data: JSON.stringify({ planViewModel: plan }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                console.log(data);
                $('#myModal').modal('hide');
                if (data.status == "true") {
                    $scope.PlanContract(plan);
                    $scope.IsAdd = false;
                    $scope.AddProviderFormTab0 = false;
                    $scope.AddProviderFormTab1 = false;
                    $scope.AddProviderFormTab2 = true;
                    $scope.tabStatus = 2;
                    $scope.showDiv = true;
                }
                messageAlertEngine.callAlertMessage('PlanUpdated', "Plan Information Updated Successfully. !!!!", "success", true);
            },
            failure: function (errMsg) {
                console.log(data);
            }
        });

        //$scope.EditPlan = false;
    };

    //======================================= Edit plan Ends=================================

    //$scope.PlanLOBs = [];
    //$scope.selectPlan = function (planid) {
    //    for (var i in $scope.Plans) {
    //        if (planid == $scope.Plans[i].PlanID) {
    //            $scope.SelectedPlan = $scope.Plans[i];
    //            break;
    //        }
    //    }
    //    $.ajax({
    //        type: "POST",
    //        url: "/Credentialing/Plan/GetPlanForEdit",
    //        data: JSON.stringify({ PlanId: planid }),
    //        contentType: "application/json; charset=utf-8",
    //        dataType: "json",
    //        async: false,
    //        success: function (data) {
    //            $scope.PlanLOBs = data;
    //            console.log(data);
    //            $scope.showDiv = true;
    //        },
    //        failure: function (errMsg) {
    //            console.log(data);
    //        }
    //    });

    //    $scope.formatData($scope.BusinessEntities, $scope.PlanLOBs);
    //};



    //--------------------- Grid View Method for LOB BE Mapping Controlls -------------------------
    $scope.formatData = function (BE, LOB) {
        $scope.BE_LOB_Maps = [];
        for (var i = 0; i < LOB.length; i++) {
            var bes = [];
            for (var j = 0; j < BE.length; j++) {
                bes.push({ BE: BE[j], IsChecked: false });
            }
            $scope.BE_LOB_Maps.push({ PlanLOB: LOB[i], BEs: bes, IsChecked: false });
        }
    };



    $scope.formatDataForView = function (BE, LOB, planContracts) {
        $scope.BE_LOB_Maps = [];
        for (var i = 0; i < LOB.length; i++) {
            var bes = [];
            for (var j = 0; j < BE.length; j++) {

                var status = false;

                for (var k in planContracts) {
                    if (planContracts[k].PlanLOB.LOBID == LOB[i].LOBID && planContracts[k].OrganizationGroupID == BE[j].GroupID) {
                        status = true;
                    }
                }
                if (status) {
                    bes.push({ BE: BE[j], IsChecked: true });
                } else {
                    bes.push({ BE: BE[j], IsChecked: false });
                }
            }
            $scope.BE_LOB_Maps.push({ PlanLOB: LOB[i], BEs: bes });
        }
    };



    //-------------------------- Save Plan LOB BE Mapping --------------------------
    $scope.SaveData = function () {
        var PlanContractDetails = [];
        var LObs = [];
        for (var i in $scope.BE_LOB_Maps) {
            var BEs = [];
            for (var j in $scope.BE_LOB_Maps[i].BEs) {
                if ($scope.BE_LOB_Maps[i].BEs[j].IsChecked) {
                    BEs.push($scope.BE_LOB_Maps[i].BEs[j].BE.GroupID);
                }
            }
            LObs.push(BEs);
        }

        for (var i in $scope.BE_LOB_Maps) {
            for (var j in LObs[i]) {
                PlanContractDetails.push({
                    PlanContractDetailID: 0,
                    PlanLOBID: $scope.BE_LOB_Maps[i].PlanLOB.PlanLOBID,
                    OrganizationGroupID: LObs[i][j]
                });
            }
        }

        console.log(PlanContractDetails);

        $http.post('/Credentialing/Plan/AddPlanContracts', { planContracts: PlanContractDetails }).
          success(function (data, status, headers, config) {
              $('#myModal1').modal('hide');
              messageAlertEngine.callAlertMessage('PlanContractAdded', "Plan Contract Information Added Successfully. !!!!", "success", true);
              console.log(data);
              $window.location.reload();
          }).
          error(function (data, status, headers, config) {
              console.log(data);
          });

    };


    //======================================= View Plan Starts ==============================

    $scope.ViewPlan = false;
    $scope.tempViewObject = null;
    $scope.plan1 = null;
    $scope.ViewSelectedPlan = function (plan) {
        $scope.PlanContract(plan);
        $scope.tempViewObject = plan;
        console.log($scope.tempViewObject);
        $scope.HeadTitle = "View Plan";
        $scope.plan1 = plan;
        $scope.ViewPlan = true;
        $scope.tabStatus = 0;
        $scope.AddProviderFormTab0 = true;

    };
    console.log($scope.tempViewObject);
    $scope.BackToList = function () {
        $scope.AddProviderFormTab0 = false;
        $scope.AddProviderFormTab1 = false;
        $scope.AddProviderFormTab2 = false;
        $scope.ViewPlan = false;
        $scope.EditPlan = false;
        $scope.AddNewPlan = false;


    };
});
//======================================= View Plan Ends ==============================

//================================= Hide All country code popover =========================
$(document).click(function (event) {
    if (!$(event.target).hasClass("countryCodeClass") && $(event.target).parents(".countryDailCodeContainer").length === 0) {
        $(".countryDailCodeContainer").hide();
    }
    if (!$(event.target).hasClass("form-control") && $(event.target).parents(".ProviderTypeSelectAutoList").length === 0) {
        $(".ProviderTypeSelectAutoList").hide();
    }
});

$(document).ready(function () {
    $(".countryDailCodeContainer").hide();
    $(".ProviderTypeSelectAutoList").hide();
});

function ResetFormForValidation(form) {
    form.removeData('validator');
    form.removeData('unobtrusiveValidation');
    $.validator.unobtrusive.parse(form);
}

// ----------------- Mouse Up & Mouse Down Action for Use ------------------------
function PasswordVisibleMouseDown(InputId) {
    $('#' + InputId).attr('type', 'text');
}

function PasswordVisibleMouseUp(InputId) {
    $('#' + InputId).attr('type', 'password');
}

function showLocationList(ele) {
    $(ele).parent().find(".ProviderTypeSelectAutoList").first().show();
}

//--------------- file name Wrap-text author : krglv ---------------
function setFileNameWith(file) {
    $(file).parent().parent().find(".jancyFileWrapTexts").find("span").width($(file).parent().parent().width() - 197);
}