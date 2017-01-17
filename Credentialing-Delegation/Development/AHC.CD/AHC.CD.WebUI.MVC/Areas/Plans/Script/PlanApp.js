
//Author: Santosh Kumar Senapati

var PlanApp = angular.module("PlanApp", ['ngAnimate', 'toaster', 'smart-table', 'loadingInteceptor', 'AngularSpeach']);
PlanApp.value("LOBValues", []);
PlanApp.value("BEValues", []);
PlanApp.constant("Loadash",_),
PlanApp.value("MasterDataForCrudOperation", [{ Type: "Master", HeaderTitle: "Plan", AddButtonStatus: true, EditButtonStatus: false, ViewHistoryButtonStatus: true },
                  { Type: "Add", HeaderTitle: "Add Plan Data", AddButtonStatus: false, EditButtonStatus: false, ViewHistoryButtonStatus: false },
                  { Type: "View", HeaderTitle: "View Plan Data", AddButtonStatus: false, EditButtonStatus: true, ViewHistoryButtonStatus: false },
                  { Type: "Edit", HeaderTitle: "Edit Plan Data", AddButtonStatus: false, EditButtonStatus: false, ViewHistoryButtonStatus: false },
                  { Type: "History", HeaderTitle: "Plans History", AddButtonStatus: false, EditButtonStatus: false, ViewHistoryButtonStatus: false }]);
PlanApp.value("AddingPlanDataInitialization", {
                     PlanID: 0, PlanCode: "", PlanName: "", PlanDescription: "", IsDelegated: "2", PlanLogoPath: "", PlanLogoFile: "", PlanLOBs: [], AttachedFormID: null, StatusType: 1,
                     Locations: [{ PlanAddressID: 0, Street: "", City: "", Appartment: "", State: "", Country: "", County: "", StatusType: 1 }],
                     ContactDetails: [{ ContactDetail: { PhoneDetails: [], EmailIDs: [], PreferredWrittenContacts: [], PreferredContacts: [] },ContactPersonName: [], PlanContactDetailID: 0, StatusType: 1 }] //, ContactPersonName: { Name: [{Names:''}]}
});

PlanApp.value("PreferredContacts", [{ PreferredContactID: null, ContactType: "Office Phone", PreferredWrittenContactType: 1, StatusType: 1, PreferredIndex: 1 },
                                    { PreferredContactID: null, ContactType: "Fax", PreferredWrittenContactType: 2, StatusType: 1, PreferredIndex: 2 },
                                    { PreferredContactID: null, ContactType: "Mobile", PreferredWrittenContactType: 3, StatusType: 1, PreferredIndex: 3 },
                                    { PreferredContactID: null, ContactType: "Email", PreferredWrittenContactType: 4, StatusType: 1, PreferredIndex: 4 },
                                    { PreferredContactID: null, ContactType: "Pager", PreferredWrittenContactType: 5, StatusType: 1, PreferredIndex: 5}]);



PlanApp.run(["$rootScope", "$timeout", "$window", "speech", function ($rootScope, $timeout, $window, speech) {
    $rootScope.support = false;
    $rootScope.ActivePlanData=[];
    $rootScope.InactivePlanData = [];
    $rootScope.PlanNameForCheck = "";
    //$rootScope._ = window._;
    var planData = PlanData;
    if ($window.speechSynthesis) {
        $rootScope.support = true;
        $timeout(function () {
            $rootScope.voices = speech.getVoices();
        }, 500);
    }
    $rootScope.config = {
        rate: 1,
        pitch: 1,
        volume: 1
    };
    if (angular.isObject(planData.ActivePlanRecords)) {
        $rootScope.ActivePlanData = angular.copy(planData.ActivePlanRecords);
        jQuery.grep($rootScope.ActivePlanData, function (ele) { ele.PlanContactPersonName = (ele.PlanContactPersonName != null && ele.PlanContactPersonName != "") ? JSON.parse(ele.PlanContactPersonName) : [] });
    }
    if (angular.isObject(planData.InactivePlanRecords)) {
        $rootScope.InactivePlanData = angular.copy(planData.InactivePlanRecords);
        jQuery.grep($rootScope.InactivePlanData, function (ele) { ele.PlanContactPersonName = (ele.PlanContactPersonName != null && ele.PlanContactPersonName != "") ? JSON.parse(ele.PlanContactPersonName) : [] });
    }
}])