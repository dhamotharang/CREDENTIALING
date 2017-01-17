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
        return $http.get(rootDir + "/Location/GetCities?city=" + QueryString)
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
            $timeout(animateMessageAlertOff, 5000);
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
    $http.get(rootDir + '/MasterDataNew/GetAllPlans').
   success(function (data, status, headers, config) {
       console.log("All Plans");
       console.log(data);
       $scope.Plans = data;
       $scope.data = data;
       $scope.init_table(data);
       $scope.progressbar = false;
   }).
   error(function (data, status, headers, config) {
       //console.log("Sorry internal master data cont able to fetch.");
   });


    $http.get(rootDir + '/MasterDataNew/GetAllInactivePlans').
  success(function (data, status, headers, config) {
      console.log("All Plans");
      console.log(data);
      $scope.InactivePlanData = data;
      $scope.init_table1($scope.InactivePlanData);
      $scope.progressbar1 = false;
  }).
  error(function (data, status, headers, config) {
      //console.log("Sorry internal master data cont able to fetch.");
  });

    $scope.MasterLOBs = [];

    //------------- get List Of Lobs ---------------
    $http.get(rootDir + '/MasterDataNew/GetAllLobs').
   success(function (data, status, headers, config) {
       $scope.Lobs = angular.copy(data);
       $scope.MasterLOBs = data;
   }).
   error(function (data, status, headers, config) {
       //console.log("Sorry internal master data cont able to fetch.");
   });

    $http.get(rootDir + '/MasterDataNew/GetAllOrganizationGroupAsync').
   success(function (data, status, headers, config) {
       console.log(data);
       console.log("===organisationgroup====");
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
    $scope.InactiveData = [];
    $scope.visibilityControl = "";
    $scope.planckeck = true;
    $scope.planckeckcondition = true;
    $scope.progressbar = true;
    $scope.progressbar1 = true;
    $scope.LogoCheck = false;

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
        for (var i = 0; i < $scope.data.length; i++) {

            if ($scope.data[i].ContactDetails.length > 0) {

                if ($scope.data[i].ContactDetails[0].ContactPersonName == null) {
                    $scope.data[i].ContactPersonName = "Not Available";
                }
                else {
                    $scope.data[i].ContactPersonName = $scope.data[i].ContactDetails[0].ContactPersonName;
                }

                if ($scope.data[i].ContactDetails[0].ContactDetail && $scope.data[i].ContactDetails[0].ContactDetail.EmailIDs.length > 0) {
                    for (var j = 0; j < $scope.data[i].ContactDetails[0].ContactDetail.EmailIDs.length; j++) {
                        if ($scope.data[i].ContactDetails[0].ContactDetail.EmailIDs[j].EmailAddress == null) {
                            $scope.data[i].EmailAddress = "";
                        }
                        else {
                            $scope.data[i].EmailAddress = $scope.data[i].ContactDetails[0].ContactDetail.EmailIDs[j].EmailAddress;
                        }
                    }
                }
            }

            if ($scope.data[i].Locations.length > 0) {
                if ($scope.data[i].Locations[0].City == null) {
                    $scope.data[i].Address  = "Not Available";
                }
                else {
                    $scope.data[i].Address = $scope.data[i].Locations[0].City;
                }

                if ($scope.data[i].Locations[0].Street == null) {
                    $scope.data[i].Address = "Not Available";
                }
                else {
                    $scope.data[i].Address = $scope.data[i].Address + $scope.data[i].Locations[0].Street;
                }

                if ($scope.data[i].Locations[0].ZipCode == null) {
                    $scope.data[i].Address = "Not Available";
                }
                else {
                    $scope.data[i].Address = $scope.data[i].Address + $scope.data[i].Locations[0].ZipCode;
                }
                
                if ($scope.data[i].Locations[0].Country == null) {
                    $scope.data[i].Address = "Not Available";
                }
                else {
                    $scope.data[i].Address = $scope.data[i].Address + $scope.data[i].Locations[0].Country;
                }

                if ($scope.data[i].Locations[0].County == null) {
                    $scope.data[i].Address = "Not Available";
                }
                else {
                    $scope.data[i].Address = $scope.data[i].Address + $scope.data[i].Locations[0].County;
                }
                
                if ($scope.data[i].Locations[0].Appartment == null) {
                    $scope.data[i].Address = "Not Available";
                }
                else {
                    $scope.data[i].Address = $scope.data[i].Address + $scope.data[i].Locations[0].Appartment;
                }

                if ($scope.data[i].Locations[0].State == null) {
                    $scope.data[i].Address = "Not Available";
                }
                else {
                    $scope.data[i].Address = $scope.data[i].Address + $scope.data[i].Locations[0].State;
                }
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

    $scope.init_table1 = function (InactiveData, condition) {

        $scope.InactiveData = InactiveData;

        var counts = [];

        if ($scope.InactiveData.length <= 10) {
            counts = [];
        }
        else if ($scope.InactiveData.length <= 25) {
            counts = [10, 25];
        }
        else if ($scope.InactiveData.length <= 50) {
            counts = [10, 25, 50];
        }
        else if ($scope.InactiveData.length <= 100) {
            counts = [10, 25, 50, 100];
        }
        else if ($scope.InactiveData.length > 100) {
            counts = [10, 25, 50, 100];
        }
        for (var i = 0; i < $scope.InactiveData.length; i++) {

            if ($scope.InactiveData[i].ContactDetails.length > 0) {

                if ($scope.InactiveData[i].ContactDetails[0].ContactPersonName == null) {
                    $scope.InactiveData[i].ContactPersonName = "Not Available";
                }
                else {
                    $scope.InactiveData[i].ContactPersonName = $scope.InactiveData[i].ContactDetails[0].ContactPersonName;
                }

                if ($scope.InactiveData[i].ContactDetails[0].ContactDetail && $scope.InactiveData[i].ContactDetails[0].ContactDetail.EmailIDs.length > 0) {
                    for (var j = 0; j < $scope.InactiveData[i].ContactDetails[0].ContactDetail.EmailIDs.length; j++) {
                        if ($scope.InactiveData[i].ContactDetails[0].ContactDetail.EmailIDs[j].EmailAddress == null) {
                            $scope.InactiveData[i].EmailAddress = "";
                        }
                        else {
                            $scope.InactiveData[i].EmailAddress = $scope.InactiveData[i].ContactDetails[0].ContactDetail.EmailIDs[j].EmailAddress;
                        }
                    }
                }
            }

            if ($scope.InactiveData[i].Locations.length > 0) {
                if ($scope.InactiveData[i].Locations[0].City == null) {
                    $scope.InactiveData[i].Address = "Not Available";
                }
                else {
                    $scope.InactiveData[i].Address = $scope.InactiveData[i].Locations[0].City;
                }

                if ($scope.InactiveData[i].Locations[0].Street == null) {
                    $scope.InactiveData[i].Address = "Not Available";
                }
                else {
                    $scope.InactiveData[i].Address = $scope.InactiveData[i].Address + $scope.InactiveData[i].Locations[0].Street;
                }

                if ($scope.InactiveData[i].Locations[0].ZipCode == null) {
                    $scope.InactiveData[i].Address = "Not Available";
                }
                else {
                    $scope.InactiveData[i].Address = $scope.InactiveData[i].Address + $scope.InactiveData[i].Locations[0].ZipCode;
                }

                if ($scope.InactiveData[i].Locations[0].Country == null) {
                    $scope.InactiveData[i].Address = "Not Available";
                }
                else {
                    $scope.InactiveData[i].Address = $scope.InactiveData[i].Address + $scope.InactiveData[i].Locations[0].Country;
                }

                if ($scope.InactiveData[i].Locations[0].County == null) {
                    $scope.InactiveData[i].Address = "Not Available";
                }
                else {
                    $scope.InactiveData[i].Address = $scope.InactiveData[i].Address + $scope.InactiveData[i].Locations[0].County;
                }

                if ($scope.InactiveData[i].Locations[0].Appartment == null) {
                    $scope.InactiveData[i].Address = "Not Available";
                }
                else {
                    $scope.InactiveData[i].Address = $scope.InactiveData[i].Address + $scope.InactiveData[i].Locations[0].Appartment;
                }

                if ($scope.InactiveData[i].Locations[0].State == null) {
                    $scope.InactiveData[i].Address = "Not Available";
                }
                else {
                    $scope.InactiveData[i].Address = $scope.InactiveData[i].Address + $scope.InactiveData[i].Locations[0].State;
                }
            }
        }
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
            total: $scope.InactiveData.length, // length of data
            getData: function ($defer, params) {
                // use build-in angular filter
                var filteredData = params.filter() ?
                        $filter('filter')($scope.InactiveData, params.filter()) :
                        $scope.InactiveData;
                var orderedData = params.sorting() ?
                        $filter('orderBy')(filteredData, params.orderBy()) :
                        $scope.InactiveData;

                params.total(orderedData.length); // set total for recalc pagination
                $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
            }
        });
    };

    $scope.GoToPlanListHistory = function () {
        $scope.AddProviderFormTab0 = false;
        $scope.AddProviderFormTab1 = false;
        $scope.AddProviderFormTab2 = false;
        $scope.ViewPlan = false;
        $scope.EditPlan = false;
        $scope.AddNewPlan = false;
        //$window.location.reload();
        $scope.HeadTitle = 'Plan History';
        $scope.planckeck = false;
    }

    $scope.BackToPlanList = function () {
        $scope.planckeck = true;
        $scope.AddProviderFormTab0 = false;
        $scope.AddProviderFormTab1 = false;
        $scope.AddProviderFormTab2 = false;
        $scope.ViewPlan = false;
        $scope.EditPlan = false;
        $scope.AddNewPlan = false;
        //$window.location.reload();
    }

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


    //if filter is on
    $scope.ifFilter1 = function () {
        try {
            var bar;
            var obj = $scope.tableParams2.$params.filter;
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
    $scope.getIndexFirst1 = function () {
        try {
            if ($scope.groupBySelected == 'none') {
                return ($scope.tableParams2.$params.page * $scope.tableParams2.$params.count) - ($scope.tableParams2.$params.count - 1);
            }
        }
        catch (e) { }
    }

    //Get index Last in table
    $scope.getIndexLast1 = function () {
        try {
            if ($scope.groupBySelected == 'none') {
                return { true: ($scope.InactiveData.length), false: ($scope.tableParams2.$params.page * $scope.tableParams2.$params.count) }[(($scope.tableParams2.$params.page * $scope.tableParams2.$params.count) > ($scope.InactiveData.length))];
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
        $scope.LogoCheck = true;

        //========= Defining structure of tempobject ============
        $scope.tempObject = {
            PlanID: 0,
            PlanCode: "",
            PlanName: "",
            PlanDescription: "",
            IsDelegated: "2",
            PlanLogoPath: "",
            PlanLogoFile: "",
            PlanLOBs: [],
            AttachedFormID: null,
            StatusType: 1,
            Locations: [{
                PlanAddressID: 0,
                Street: "",
                City: "",
                Appartment: "",
                State: "",
                Country: "",
                County: "",
                StatusType: 1,
            }],
            ContactDetails: [{
                ContactDetail: {
                    PhoneDetails: [],
                    EmailIDs: [],
                    PreferredWrittenContacts: [],
                    PreferredContacts: []
                },
                PlanContactDetailID: 0,
                ContactPersonName: "",
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
            $scope.LogoCheck = true;
            if (element.files.lenght == 0) {
                tempObject.PlanLogoPath = "";
            }
        });
    };

    $scope.removeFile = function () {
        $scope.LogoCheck = false;
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
            $scope.tempObject1.LOBAddressDetails[0] = angular.copy($scope.tempObject.Locations[0]);
        } else {
            $scope.tempObject1.LOBAddressDetails[0] = {};
        }
        $scope.tempObject1.LOBAddressDetails[0].IsSameAddress = condition;
    };

    //----------------------------------------- get same Contact Details ----------------------------
    $scope.GetContactPerson = function (condition) {
        if (condition) {
            $scope.tempObject1.LOBContactDetails[0] = angular.copy($scope.tempObject.ContactDetails[0]);
        } else {
            $scope.tempObject1.LOBContactDetails[0] = {
                ContactDetail: {
                    PhoneDetails: [],
                    EmailIDs: [],
                    PreferredWrittenContacts: [],
                    PreferredContacts: []
                },
                PlanContactDetailID: 0,
                ContactPersonName: "",
                StatusType: 1,
            };
        }
        $scope.tempObject1.LOBContactDetails[0].IsSameContatcPerson = condition;
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
        $scope.tempObject1.LOBContactDetails.push({
            ContactPersonName: "",
            EmailAddress: "",
            PhoneNumber: "",
            FaxNumber: ""
        });
    };
    //---------------------------------- remove lob contact --------------------------
    $scope.RemoveLobContact = function (index) {
        $scope.tempObject1.LOBContactDetails.splice(index, 1);
    };

    //------------------------------------ add lob address -----------------
    $scope.AddLobAddress = function () {
        $scope.tempObject1.LOBAddressDetails.push({
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
        $scope.tempObject1.LOBAddressDetails.splice(index, 1);
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
        $scope.tempObject.PlanLOBs.push(temp);
        $scope.tempObject1 = angular.copy(temp);
        $scope.tempObject1.LOBAddressDetails[0] = angular.copy($scope.tempObject.Locations[0]);
        $scope.tempObject1.LOBContactDetails[0] = angular.copy($scope.tempObject.ContactDetails[0]);
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

        for (var i in $scope.Lobs) {
            if (tempObject1.LOBID == $scope.Lobs[i].LOBID) {
                tempObject1.LOB = $scope.Lobs[i];
                break;
            }
        }
       // tempObject1.ContactDetail.PreferredContacts = angular.copy($scope.PreferredContactTypesArray);

        console.log(tempObject1);

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
    
    //----------------------------------------- address data fetch from database --------------------
    $scope.addressHomeAutocomplete = function (location, pl) {
        $scope.resetAddressModels(pl);
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
        changeVisibilityOfCountryCode();
        $("#" + div_Id).show();
    };

    //================================= Tab Flow Validation Method ==============================
    $scope.nextForm = function (tabid) {
        if (tabid == 0)
        {
            $scope.tabStatus = tabid;
            $scope.Lobs = {}
        }
        if (tabid == 1)
        {
            $formData = $('#PlanForm');
            ResetFormForValidation($formData);
            validationStatus = $formData.valid();
            if (validationStatus)
            {
                $scope.tabStatus = tabid;
            }
        }
        if (tabid == 2) {
            $scope.tabStatus = tabid;
        }
        
    };
    //================================= Primary info form view ===================================
    $scope.primaryInfoForm = function (Form_Id) {
        if ($scope.tempObject.PlanLOB && $scope.tempObject.PlanLOB.length > 0) {
            $scope.AddNewPlanLOB = false;
        } else {
            $scope.AddNewPlanLOB = false;
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
        $scope.ConfirmMessage = 'Are you sure, you want to add a Plan?';
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

    $scope.confirmationUpdatePlanContract = function (formStates) {
        $scope.ConfirmTitle1 = 'Confirm - Update Plan Contract';
        $scope.ConfirmMessage1 = 'Are you sure, you want to Update Plan Contract?';
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
        $http.post(rootDir + '/Credentialing/Plan/GetPlanContractForPlan', { PlanId: plandata.PlanID }).
        success(function (data, status, headers, config) {
            $scope.plancontractviewdata = data;
            $scope.formatDataForView($scope.BusinessEntities, plandata.PlanLOBs, $scope.plancontractviewdata);
        }).
   error(function (data, status, headers, config) {
       //console.log("Sorry internal master data cont able to fetch.");
   });

    };

    //----------- method for proceed button -----------------
    $scope.ProceedAdd = function (plandatacheckforplanname) {

        console.log($scope.tempObject);

        $formData = $('#PlanForm');
        ResetFormForValidation($formData);
        validationStatus = $formData.valid();
        if (validationStatus) {

            $http.post(rootDir + '/Credentialing/Plan/IsPlanNameExist', { planName: plandatacheckforplanname.PlanName }).
        success(function (data, status, headers, config) {
            if (data == "False") {
                $scope.AddNewPlan = true;
                $scope.saving = false;
                $scope.AddProviderFormTab0 = true;
                $scope.AddProviderFormTab1 = true;
                $scope.AddProviderFormTab2 = false;
                $scope.tabStatus = 1;

                for (var i in $scope.tempObject.ContactDetails) {
                    $scope.tempObject.ContactDetails[i].ContactDetail.PreferredContacts = $scope.PreferredContactTypesArray;
                }

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
                        LOBAddressDetails: angular.copy($scope.tempObject.Locations),
                        LOBContactDetails: angular.copy($scope.tempObject.ContactDetails)
                    });
                }
            }
            else {
                messageAlertEngine.callAlertMessage('PlanCodeSame', "Plan Name Already Exist. !!!!", "success", true);
            }

        }).
   error(function (data, status, headers, config) {
       //console.log("Sorry internal master data cont able to fetch.");
   });
        }
    }

    $scope.ProceedUpdate = function (plandatacheckforplancode) {

        $formData = $('#PlanForm');
        ResetFormForValidation($formData);
        validationStatus = $formData.valid();
        if (validationStatus) {

           // $http.post('/Credentialing/Plan/IsPlanCodeExist', { planCode: plandatacheckforplancode.PlanCode }).
           //success(function (data, status, headers, config) {
           //    console.log(data);
            //    if (data == "False") {
                   $scope.tempObject.PlanLogoPath = $scope.planlogopath;
                   $scope.AddNewPlan = true;
                   $scope.saving = false;
                   $scope.AddProviderFormTab0 = true;
                   $scope.AddProviderFormTab1 = true;
                   $scope.AddProviderFormTab2 = false;
                   $scope.tabStatus = 1;
      //         }
      //         else {
      //             messageAlertEngine.callAlertMessage('PlanCodeSame', "Plan Code Already Exist. !!!!", "success", true);
      //         }

      //     }).
      //error(function (data, status, headers, config) {
      //    //console.log("Sorry internal master data cont able to fetch.");
      //});
        }
    }

    $scope.ProceedView = function () {
        $scope.AddProviderFormTab0 = true;
        $scope.AddProviderFormTab1 = true;
        $scope.AddProviderFormTab2 = true;
        $scope.tabStatus = 1;
        //$scope.tabStatus = 2;
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

        var formdata = new FormData();

        var form5 = $('#PlanForm, #LOB_BE_Mapping').serializeArray();

        for (var i in form5) {
            formdata.append(form5[i].name, form5[i].value);
        }

        formdata.append($("#PlanLogoFile")[0].name, $("#PlanLogoFile")[0].files[0]);

        $.ajax({
            type: "POST",
            url: rootDir + "/Credentialing/Plan/AddPlan",
            data: formdata,
            async: true,
            cache: false,
            contentType: false,
            processData: false,
            success: function (data) {
                $scope.PlanLOBs = data.LobDetail.Data;
                $('#myModal').modal('hide');
                if (data.status == "true") {
                    $timeout(function () {
                        $scope.AddNewPlan = true;
                        $scope.saving = false;
                        $scope.AddProviderFormTab0 = false;
                        $scope.AddProviderFormTab1 = false;
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
        $.ajax({
            type: "POST",
            url: rootDir + "/Credentialing/Plan/RemovePlan",
            data: JSON.stringify({ planid: $scope.planid }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                $('#myModal').modal('hide');
                if (data.status == "true") {
                    $window.location.reload();

                }
            },
            failure: function (errMsg) {
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

    
    //------------------------ cancel add data ----------------------
    $scope.CancelAdd = function () {
        $window.location.reload();
        $scope.AddNewPlan = false;
        $scope.saving = false;
        $scope.EditPlan = false;
        $scope.AddProviderFormTab0 = true;
        $scope.AddProviderFormTab1 = false;
        $scope.AddProviderFormTab2 = false;
        $scope.tabStatus = 0;
        $scope.PreferredContactTypesArray = [];        
    };

    //------------ cancel Lob Information --------------
    $scope.cancelNewPlanLOB = function (index) {
        $scope.AddNewPlan = true;
        $scope.saving = false;
        $scope.AddProviderFormTab0 = true;
        $scope.AddProviderFormTab1 = true;
        $scope.AddProviderFormTab2 = false;
        $scope.tabStatus = 0;
        $scope.Lobs = {}
    };

    $scope.cancelViewPlanLOB = function (index) {
        $scope.AddProviderFormTab0 = true;
        $scope.AddProviderFormTab1 = true;
        $scope.AddProviderFormTab2 = true;
        $scope.tabStatus = 0;
    };

    //------------------------------ View Business Entity Plan History Data ---------------------------
    $scope.HistoryViewed = false;
    $scope.ViewHistory = function () {
        $scope.IsHistoryView = true;
        $scope.HistoryViewed = true;

        $http.get(rootDir + '/Credentialing/BusinessEntity/GetAllBussinessEntityPlanMappingHistories').
        success(function (data, status, headers, config) {
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
    $scope.Confirmation = function (InactiveData) {
        $http.post(rootDir + '/Credentialing/BusinessEntity/RemoveBEPlan', {
            BEPlanMappingID: InactiveData.BEPlanMappingID
        }).
        success(function (data, status, headers, config) {
            if (data.status == 'true') {
                //----------- temp push controller back-end dependency -------------------
                $scope.BEPlanMappings.splice($scope.BEPlanMappings.indexOf(InactiveData), 1);
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
        $scope.PlanIsEdit = true;
        $scope.AddNewPlan = true;
        $scope.HeadTitle = 'Edit Plan';
        $scope.tabStatus = 0;
        $scope.AddProviderFormTab0 = true;
        $scope.AddProviderFormTab1 = true;
        $scope.AddProviderFormTab2 = false;
        //$scope.HeadTitle = "Edit Plan";
        $scope.ViewPlan = false;
        $scope.EditPlan = true;
        $scope.EditPlanID = plan.PlanID;
        if (plan.PlanLogoPath == "~/Content/Images/PlanLogo/") {
            $scope.LogoCheck = true;
        }
        else
        {
            $scope.LogoCheck = false;
        }
        
        //$scope.PreferredContactTypesArray = [];
        $scope.tempObject = angular.copy(plan);
        for (i = 0; i < $scope.tempObject.ContactDetails.length ; i++) {
        if ($scope.tempObject.ContactDetails[i].ContactDetail == null) {
            $scope.tempObject.ContactDetails[i].ContactDetail = {
                PhoneDetails: [],
                EmailIDs: [],
                PreferredWrittenContacts: [],
                PreferredContacts: []
            };
        } else {
            if ($scope.tempObject.ContactDetails[i].ContactDetail.PhoneDetails.length > 0) {
                var filterd = $scope.getInactivatedDataArray($scope.tempObject.ContactDetails[i].ContactDetail.PhoneDetails);
                $scope.tempObject.ContactDetails[i].ContactDetail.PhoneDetails = filterd.dataArray;
                $scope.tempSelectedDeactivatedPhonesDetails = filterd.temInactivatedData;
            }
            if ($scope.tempObject.ContactDetails[i].ContactDetail.EmailIDs.length > 0) {
                var filterd = $scope.getInactivatedDataArray($scope.tempObject.ContactDetails[i].ContactDetail.EmailIDs);
                $scope.tempObject.ContactDetails[i].ContactDetail.EmailIDs = filterd.dataArray;
                $scope.tempSelectedDeactivatedEmailIds = filterd.temInactivatedData;
            }
            if ($scope.tempObject.ContactDetails[i].ContactDetail.PreferredContacts.length > 0) {
                var filterd = $scope.getInactivatedDataArray($scope.tempObject.ContactDetails[i].ContactDetail.PreferredContacts);
                $scope.tempObject.ContactDetails[i].ContactDetail.PreferredContacts = filterd.dataArray;
                $scope.tempSelectedDeactivatedPreferredContacts = filterd.temInactivatedData;
            }
            $scope.PreferredContactTypesArray = $scope.tempObject.ContactDetails[i].ContactDetail.PreferredContacts;
        }
        }

        for (i = 0; i < $scope.tempObject.PlanLOBs.length ; i++) {
            if ($scope.tempObject.PlanLOBs[i].LOBContactDetails != null) { 
            for (j = 0; j < $scope.tempObject.PlanLOBs[i].LOBContactDetails.length ; j++) {
                if ($scope.tempObject.PlanLOBs[i].LOBContactDetails[j].ContactDetail == null) {
                    $scope.tempObject.PlanLOBs[i].LOBContactDetails[j].ContactDetail = {
                        PhoneDetails: [],
                        EmailIDs: [],
                        PreferredWrittenContacts: [],
                        PreferredContacts: []
                    };
                } else {
                    if ($scope.tempObject.PlanLOBs[i].LOBContactDetails[j].ContactDetail.PhoneDetails.length > 0) {
                        var filterd = $scope.getInactivatedDataArray($scope.tempObject.PlanLOBs[i].LOBContactDetails[j].ContactDetail.PhoneDetails);
                        $scope.tempObject.PlanLOBs[i].LOBContactDetails[j].ContactDetail.PhoneDetails = filterd.dataArray;
                        $scope.tempSelectedDeactivatedPhonesDetails = filterd.temInactivatedData;
                    }
                    if ($scope.tempObject.PlanLOBs[i].LOBContactDetails[j].ContactDetail.EmailIDs.length > 0) {
                        var filterd = $scope.getInactivatedDataArray($scope.tempObject.PlanLOBs[i].LOBContactDetails[j].ContactDetail.EmailIDs);
                        $scope.tempObject.PlanLOBs[i].LOBContactDetails[j].ContactDetail.EmailIDs = filterd.dataArray;
                        $scope.tempSelectedDeactivatedEmailIds = filterd.temInactivatedData;
                    }
                    if ($scope.tempObject.PlanLOBs[i].LOBContactDetails[j].ContactDetail.PreferredContacts.length > 0) {
                        var filterd = $scope.getInactivatedDataArray($scope.tempObject.PlanLOBs[i].LOBContactDetails[j].ContactDetail.PreferredContacts);
                        $scope.tempObject.PlanLOBs[i].LOBContactDetails[j].ContactDetail.PreferredContacts = filterd.dataArray;
                        $scope.tempSelectedDeactivatedPreferredContacts = filterd.temInactivatedData;
                    }
                    $scope.PreferredContactTypesArray = $scope.tempObject.PlanLOBs[i].LOBContactDetails[j].ContactDetail.PreferredContacts;
                }
            }
            }
        }
        $scope.planlogopath = plan.PlanLogoPath;
        $scope.tempEditObject = plan;
       
    };

    $scope.CalcelEditing = function () {
        $scope.EditPlan = false;
    };

    $scope.UpdatePlan = function (plan) {

        var formdata = new FormData();

        var form5 = $('#PlanForm, #LOB_BE_Mapping').serializeArray();

        

        for (var i in form5) {
            formdata.append(form5[i].name, form5[i].value);
        }

        if ($scope.LogoCheck == false) {
            $("#PlanLogoFile")[0].files[0] = "";
            $("#PlanLogoPath")[0].value = "";
            $("#PlanLogoPath")[0].name = "";
        }

        formdata.append($("#PlanLogoFile")[0].name, $("#PlanLogoFile")[0].files[0]);
        formdata.append($("#PlanLogoPath")[0].name, $("#PlanLogoPath")[0].value);

        console.log(formdata);
        console.log("===Data Sending===");

        $.ajax({
            type: "POST",
            url: rootDir + "/Credentialing/Plan/UpdatePlanAsync",
            data: formdata,
            async: true,
            cache: false,
            contentType: false,
            processData: false,
            success: function (data) {
                $('#myModal').modal('hide');
                if (data.status == "true") {
                    $timeout(function () {
                        $scope.PlanContract(plan);
                        $scope.IsAdd = false;
                        $scope.AddProviderFormTab0 = true;
                        $scope.AddProviderFormTab1 = true;
                        $scope.AddProviderFormTab2 = true;
                        $scope.tabStatus = 2;
                        $scope.showDiv = true;
                        $scope.formatData($scope.BusinessEntities, $scope.PlanLOBs);
                    }, 1000);
                }
                messageAlertEngine.callAlertMessage('PlanUpdated', "Plan Information Updated Successfully. !!!!", "success", true);
            }
        });
    };

    //--------------------- Grid View Method for LOB BE Mapping Controlls -------------------------
    $scope.formatData = function (BE, LOB) {
        $scope.BE_LOB_Maps = [];
        for (var i = 0; i < LOB.length; i++) {
            var bes = [];
            for (var j = 0; j < BE.length; j++) {
                bes.push({ BE: BE[j], IsChecked: true });
            }
            $scope.BE_LOB_Maps.push({ PlanLOB: LOB[i], BEs: bes, IsChecked: true });
        }
        console.log($scope.BE_LOB_Maps);
    };

    $scope.formatDataForView = function (BE, LOB, planContracts) {
        $scope.BE_LOB_Maps = [];
        for (var i = 0; i < LOB.length; i++) {
            var bes = [];
            for (var j = 0; j < BE.length; j++) {

                var status = false;

                for (var k in planContracts) {
                    if (planContracts[k].PlanLOB.LOBID == LOB[i].LOBID && planContracts[k].OrganizationGroupID == BE[j].OrganizationGroupID) {
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



    //-------------------------- Save Plan Contract Mapping --------------------------
    $scope.SaveData = function () {
        var PlanContractDetails = [];
        var LObs = [];
        for (var i in $scope.BE_LOB_Maps) {
            var BEs = [];
            for (var j in $scope.BE_LOB_Maps[i].BEs) {
                if ($scope.BE_LOB_Maps[i].BEs[j].IsChecked) {
                    BEs.push($scope.BE_LOB_Maps[i].BEs[j].BE.OrganizationGroupID);
                }
            }
            LObs.push(BEs);
        }

        for (var i in $scope.BE_LOB_Maps) {
            for (var j in LObs[i]) {
                PlanContractDetails.push({
                    PlanContractDetailID: 0,
                    PlanLOBID: $scope.BE_LOB_Maps[i].PlanLOB.PlanLOBID,
                    OrganizationGroupID: LObs[i][j],
                    StatusType: "Active"
                });
            }
        }

        $http.post(rootDir + '/Credentialing/Plan/AddPlanContracts', { planContracts: PlanContractDetails, async: false }).

          success(function (data, status, headers, config) {
              $('#myModal1').modal('hide');
              messageAlertEngine.callAlertMessage('PlanContractAdded', "Plan Contract Information Added Successfully. !!!!", "success", true);
              
          }).
          error(function (data, status, headers, config) {
          });
        $window.location.reload();
    };

    $scope.UpdatePlanContract = function () {
        var PlanContractDetails = [];
        var LObs = [];
        for (var i in $scope.BE_LOB_Maps) {
            var BEs = [];
            for (var j in $scope.BE_LOB_Maps[i].BEs) {
                if ($scope.BE_LOB_Maps[i].BEs[j].IsChecked) {
                    BEs.push($scope.BE_LOB_Maps[i].BEs[j].BE.OrganizationGroupID);
                }
            }
            LObs.push(BEs);
        }

        for (var i in $scope.BE_LOB_Maps) {
            for (var j in LObs[i]) {
                PlanContractDetails.push({
                    PlanContractDetailID: 0,
                    PlanLOBID: $scope.BE_LOB_Maps[i].PlanLOB.PlanLOBID,
                    OrganizationGroupID: LObs[i][j],
                    StatusType: "Active"
                });
            }
        }

        $http.post(rootDir + '/Credentialing/Plan/UpdatePlanContracts', { PlanID: $scope.EditPlanID, planContracts: PlanContractDetails, async: false }).

          success(function (data, status, headers, config) {
              $('#myModal1').modal('hide');
              messageAlertEngine.callAlertMessage('PlanContractUpdated', "Plan Contract Information Updated Successfully. !!!!", "success", true);
          }).
          error(function (data, status, headers, config) {
          });
        $window.location.reload();
    };

    //======================================= View Plan Starts ==============================

    $scope.ViewPlan = false;
    $scope.tempViewObject = null;
    $scope.plan1 = null;
    $scope.ViewSelectedPlan = function (plan) {
        $scope.PlanContract(plan);
        $scope.tempViewObject = angular.copy(plan);
        $scope.HeadTitle = "View Plan";
        $scope.plan1 = plan;
        $scope.ViewPlan = true;
        $scope.planckeck = true;
        $scope.planckeckcondition = true;
        $scope.tabStatus = 0;
        $scope.AddProviderFormTab0 = true;
        $scope.AddProviderFormTab1 = true;
        $scope.AddProviderFormTab2 = true;
        $scope.showDiv = true;
    };

    $scope.ViewInactiveSelectedPlan = function (plan) {
        $scope.PlanContract(plan);
        $scope.tempViewObject = angular.copy(plan);
        $scope.HeadTitle = "View Plan";
        $scope.plan1 = plan;
        $scope.ViewPlan = true;
        $scope.planckeck = true;
        $scope.planckeckcondition = false;
        $scope.tabStatus = 0;
        $scope.AddProviderFormTab0 = true;
        $scope.AddProviderFormTab1 = true;
        $scope.AddProviderFormTab2 = true;
        $scope.showDiv = true;
    };

    $scope.BackToList = function () {
        $scope.AddProviderFormTab0 = false;
        $scope.AddProviderFormTab1 = false;
        $scope.AddProviderFormTab2 = false;
        $scope.ViewPlan = false;
        $scope.EditPlan = false;
        $scope.AddNewPlan = false;
        $window.location.reload();
    };

    $scope.PreferredContacts = [{
        PreferredContactID: null,
        ContactType: "Office Phone",
        PreferredWrittenContactType: 1,
        StatusType: 1,
        PreferredIndex: 1
    }, {
        PreferredContactID: null,
        ContactType: "Fax",
        PreferredWrittenContactType: 2,
        StatusType: 1,
        PreferredIndex: 2
    }, {
        PreferredContactID: null,
        ContactType: "Mobile",
        PreferredWrittenContactType: 3,
        StatusType: 1,
        PreferredIndex: 3
    }, {
        PreferredContactID: null,
        ContactType: "Email",
        PreferredWrittenContactType: 4,
        StatusType: 1,
        PreferredIndex: 4
    }, {
        PreferredContactID: null,
        ContactType: "Pager",
        PreferredWrittenContactType: 5,
        StatusType: 1,
        PreferredIndex: 5
    }];

    $scope.getContactDetailsPhoneByPhoneTypeAndActiveStatus = function (data, phoneTypeEnum, Status) {
        var temp = [];
        for (var i in data) {
            if (data[i].PhoneTypeEnum == phoneTypeEnum && data[i].StatusType == 1) {
                temp.push(data[i]);
            }
        }
        return temp;
    };

    // ---------------------- contact details custom toggle function -----------------------
    $scope.ContactDetailsToggle = function (condition) {
        if (condition == 'EditContactDetails') {
            $rootScope.visibilityControl = condition;
            $scope.getTempContactDetailsForEdit();
        } else {
            $rootScope.visibilityControl = condition;
            $scope.ContactDetailsEmptyError = false;
        }
    };
    //----------------------- Get Temp Data for Edit contact details data ---------------------------
    $scope.getTempContactDetailsForEdit = function () {
        $scope.TempContactDetailsForEdit = angular.copy($scope.Provider.ContactDetails);
        $scope.TempPreferredWrittenContacts = angular.copy($scope.PreferredWrittenContacts);
        $scope.TempPreferredContacts = angular.copy($scope.PreferredContacts);

        if ($scope.TempContactDetailsForEdit == null) {
            $scope.TempContactDetailsForEdit = {
                PhoneDetails: [],
                EmailIDs: [],
                PreferredWrittenContacts: [],
                PreferredContacts: []
            };
        } else {
            if ($scope.TempContactDetailsForEdit.PhoneDetails.length > 0) {
                var filterd = $scope.getInactivatedDataArray($scope.TempContactDetailsForEdit.PhoneDetails);
                $scope.TempContactDetailsForEdit.PhoneDetails = filterd.dataArray;
                $scope.tempSelectedDeactivatedPhonesDetails = filterd.temInactivatedData;
            }
            if ($scope.TempContactDetailsForEdit.EmailIDs.length > 0) {
                var filterd = $scope.getInactivatedDataArray($scope.TempContactDetailsForEdit.EmailIDs);
                $scope.TempContactDetailsForEdit.EmailIDs = filterd.dataArray;
                $scope.tempSelectedDeactivatedEmailIds = filterd.temInactivatedData;
            }
            if ($scope.TempContactDetailsForEdit.PreferredContacts.length > 0) {
                var filterd = $scope.getInactivatedDataArray($scope.TempContactDetailsForEdit.PreferredContacts);
                $scope.TempContactDetailsForEdit.PreferredContacts = filterd.dataArray;
                $scope.tempSelectedDeactivatedPreferredContacts = filterd.temInactivatedData;
            }
            $scope.PreferredContactTypesArray = $scope.TempContactDetailsForEdit.PreferredContacts;
        }
    };

    // ------------------------------ Method For Get Inactivated Data list for Temp to Edit ---------------------
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
    //--------------------- Modal confirmation for Inactive database Data ----------------------
    $scope.changeStausType = function (status) {
        if (status == 1) {
            status = 2;
        } else {
            status = 1;
        }
        return status;
    };
    
    //---------------------------- Prefered Type Action ----------------------------------------

    //Method for adding unique contact types to an array
    $scope.PreferredContactTypesArray = [];
    $scope.addPreferredContactTypesToArray = function (contactType) {
        var status = true;
        for (var i in $scope.PreferredContactTypesArray) {
            if ($scope.PreferredContactTypesArray[i].PreferredWrittenContactType == contactType) {
                $scope.PreferredContactTypesArray[i].StatusType = 1;
                status = false;
                break;
            }
        }
        if (status) {
            for (var i in $scope.tempSelectedDeactivatedPreferredContacts) {
                if ($scope.tempSelectedDeactivatedPreferredContacts[i].PreferredWrittenContactType == contactType) {
                    $scope.tempSelectedDeactivatedPreferredContacts[i].StatusType = 1;
                    $scope.PreferredContactTypesArray.push($scope.tempSelectedDeactivatedPreferredContacts[i]);
                    status = false;
                    break;
                }
            }
        }

        if (status) {
            $scope.PreferredContactTypesArray.push($scope.PreferredContacts[contactType - 1]);
        }
    };
    //----------------------------- Remove or deactivate contact details preferred method -----------------------
    $scope.RemovePreferredContactTypesFromArray = function (contactType) {
        for (var i in $scope.PreferredContactTypesArray) {
            if ($scope.PreferredContactTypesArray[i].PreferredWrittenContactType == contactType && $scope.PreferredContactTypesArray[i].PreferredContactID) {
                $scope.PreferredContactTypesArray[i].StatusType = 2;
                break;
            }
            if ($scope.PreferredContactTypesArray[i].PreferredWrittenContactType == contactType && !$scope.PreferredContactTypesArray[i].PreferredContactID) {
                $scope.PreferredContactTypesArray.splice($scope.PreferredContactTypesArray.indexOf($scope.PreferredContactTypesArray[i]), 1);
                break;
            }
        }
    };
    //method to set prefferd contact priority and preffered contact index
    $scope.selectedPrefferdContact = function (pc, index) {
        $scope.pcPriority = pc;
        $scope.pcIndex = index;
    };

    //--------------------------------- Method to change the priority of contact ------------------------------
    $scope.ChangePreferredContactsPriority = function (condition) {
        var index = $scope.PreferredContactTypesArray.indexOf($scope.pcPriority);
        if (condition == "increase" && index !== 0) {
            $scope.PreferredContactTypesArray[index].ProficiencyIndex = index;
            $scope.PreferredContactTypesArray[index - 1].ProficiencyIndex = index + 1;
            $scope.pcIndex = index - 1;
        } else if (condition == "decrease" && index !== $scope.PreferredContactTypesArray.length - 1) {
            $scope.PreferredContactTypesArray[index].ProficiencyIndex = index + 2;
            $scope.PreferredContactTypesArray[index + 1].ProficiencyIndex = index + 1;
            $scope.pcIndex = index + 1;
        }
        $scope.PreferredContactTypesArray.sort(function (a, b) {
            return a.ProficiencyIndex - b.ProficiencyIndex;
        });
    };
    //---------------------------- End Preffered Type ------------------------------------------

    //-------------------------------- Is Active and Remove condition ----------------------------

    //--------------------- Modal confirmation for Inactive database Data ----------------------
    $scope.changeStausType = function (status) {
        if (status == 1) {
            status = 2;
        } else {
            status = 1;
        }
        return status;
    };

    $scope.showConfirmation = function (arryData, obj, condition) {

        obj.StatusType = $scope.changeStausType(obj.StatusType);
        var index = arryData.indexOf(obj);
        arryData[index] = obj;

        if (condition == "phone") {
            if (!obj.PhoneDetailID) {
                $scope.RemoveContactDetails(index, arryData);
            }
            var status = $scope.IsActiveContactDetails(arryData, obj);
            if (status) {
                $scope.addPreferredContactTypesToArray(obj.PhoneTypeEnum);
            } else {
                $scope.RemovePreferredContactTypesFromArray(obj.PhoneTypeEnum);
            }
        } else if (condition == "email") {
            if (!obj.EmailDetailID) {
                $scope.RemoveContactDetails(index, arryData);
            }
            var status = $scope.IsActiveEmailIds(arryData);
            if (status) {
                $scope.addPreferredContactTypesToArray(4);
            } else {
                $scope.RemovePreferredContactTypesFromArray(4);
            }
        }

    };
    //-------------------- Modal Confirmation Hide and Remove from Array ------------------------
    $scope.InactiveContactDetails = function () {
        $('#ConfirmationContactDetails').modal('hide');
    };

    //------------------------------- Active inactive for preferred contact details ---------------------
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
    //------------------------------- Get Active Inactive Status For Preferred Contact ------------------
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

    //------------------------- NG Change for duplicate mobile number for provider deactivated own data only --------------------------
    $scope.checkDuplicateData = function (obj, condition) {
        if (condition == "phone" && $scope.tempSelectedDeactivatedPhonesDetails) {
            for (var i = 0; i < $scope.tempSelectedDeactivatedPhonesDetails.length; i++) {
                if (obj.CountryCode == $scope.tempSelectedDeactivatedPhonesDetails[i].CountryCode && obj.Number == $scope.tempSelectedDeactivatedPhonesDetails[i].Number) {
                    obj.PhoneDetailID = $scope.tempSelectedDeactivatedPhonesDetails[i].PhoneDetailID;
                    obj.StatusType = 1;
                }
            }
        } else if (condition == "email") {

            obj.EmailAddress = angular.lowercase(obj.EmailAddress);

            if ($scope.tempSelectedDeactivatedEmailIds) {
                for (var i = 0; i < $scope.tempSelectedDeactivatedEmailIds.length; i++) {
                    if (obj.EmailAddress == $scope.tempSelectedDeactivatedEmailIds[i].EmailAddress) {
                        obj.EmailDetailID = $scope.tempSelectedDeactivatedEmailIds[i].EmailDetailID;
                        obj.StatusType = 1;
                    }
                }
            }
        }
    };

    //---------------- Remove Method for Contact Details -----------------------
    $scope.RemoveContactDetails = function (index, data) {
        data.splice(index, 1);
    };

    //----------------------------- End Is Active condition ------------------------------------


    $scope.AddPhones = function (obj, condition) {
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

        $scope.addPreferredContactTypesToArray(condition);
    };
    $scope.AddEmail = function (obj) {
        obj.push({
            EmailDetailID: null,
            EmailAddress: "",
            Preference: "Secondary",
            PreferenceType: 2,
            Status: "Active",
            StatusType: 1
        });
        $scope.addPreferredContactTypesToArray(4);

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

var changeVisibilityOfCountryCode = function () {
    $(".countryDailCodeContainer").hide();
    // method will close any other country code div already open.
};
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


