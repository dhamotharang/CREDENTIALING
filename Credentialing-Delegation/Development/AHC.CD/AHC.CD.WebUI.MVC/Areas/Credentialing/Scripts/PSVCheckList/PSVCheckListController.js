
//-------------------- author : krglv -------------------------------

//------------------ psv master data temp -----------------------------


  

var tempchecklistItem1 = [
        {
            SectionID: 4,
            SectionName: "Demographics",
            IsSelected: true,
            Expanded: true,
            Fields: [
               {
                   FieldName: "Personal Information",
                   IsSelected: true,
                   Expanded: true,
                   Fields: [
                       {
                           FieldName: "Salutation",
                           IsSelected: true
                       },
                       {
                           FieldName: "First Name",
                           IsSelected: true
                       },
                       {
                           FieldName: "Middle Name",
                           IsSelected: false
                       },
                       {
                           FieldName: "Last Name",
                           IsSelected: true
                       },
                       {
                           FieldName: "Suffix (JR,III)",
                           IsSelected: false
                       },
                       {
                           FieldName: "Gender",
                           IsSelected: true
                       },
                       {
                           FieldName: "Maiden Name",
                           IsSelected: false
                       },
                       {
                           FieldName: "Marital Status",
                           IsSelected: false
                       },
                       {
                           FieldName: "Spouse Name",
                           IsSelected: false
                       },
                       {
                           FieldName: "Provider Level",
                           IsSelected: true
                       },
                       {
                           FieldName: "Title",
                           IsSelected: true
                       },
                   ]
               }
            ]
        },
        {
            SectionID: 1,
            SectionName: "State License",
            IsSelected: true,
            Expanded: true,
            Fields: [
                 {
                     FieldName: "State License Number",
                     IsSelected: true
                 },
                 {
                     FieldName: "State License Type",
                     IsSelected: true
                 },
                 {
                     FieldName: "State License Status",
                     IsSelected: false
                 },
                 {
                     FieldName: "Issue Date",
                     IsSelected: true
                 },
                 {
                     FieldName: "Current Issue Date",
                     IsSelected: true
                 },
                 {
                     FieldName: "Expiration Date",
                     IsSelected: true
                 },
                 {
                     FieldName: "Issue State",
                     IsSelected: true
                 },
                 {
                     FieldName: "Are you currently practicing in this state?",
                     IsSelected: false
                 },
                 {
                     FieldName: "License in good standing?",
                     IsSelected: false
                 }
            ]
        },
        {
            SectionID: 2,
            SectionName: "DEA License",
            IsSelected: true,
            Expanded: true,
            Fields: [
               {
                   FieldName: "DEA Number",
                   IsSelected: true
               },
               {
                   FieldName: "State of Registration",
                   IsSelected: true
               },
               {
                   FieldName: "Issue Date",
                   IsSelected: true
               },
               {
                   FieldName: "Expiry Date",
                   IsSelected: true
               },
               {
                   FieldName: "License in good standing",
                   IsSelected: true
               }
            ]
        },
        {
            SectionID: 3,
            SectionName: "CDS Information",
            IsSelected: true,
            Expanded: true,
            Fields: [
               {
                   FieldName: "CDS Number",
                   IsSelected: true
               },
               {
                   FieldName: "Issue State",
                   IsSelected: true
               },
               {
                   FieldName: "Issue Date",
                   IsSelected: true
               },
               {
                   FieldName: "Expiry Date",
                   IsSelected: true
               }
            ]
        }
];

var psvMasteData = [
    {
        Name: "Freedom PSV Checklist",
        Code: "FPC",
        CheckListItems: tempchecklistItem1
    },
    {
        Name: "Ultimate PSV Checklist",
        Code: "UPC",
        CheckListItems: tempchecklistItem1
    },
    {
        Name: "Coventry PSV Checklist",
        Code: "CPC",
        CheckListItems: tempchecklistItem1
    },
    {
        Name: "Humana PSV Checklist",
        Code: "HPC",
        CheckListItems: tempchecklistItem1
    },
    {
        Name: "Wellcare PSV Checklist",
        Code: "WPC",
        CheckListItems: tempchecklistItem1
    },
    {
        Name: "Tricare PSV Checklist",
        Code: "TPC",
        CheckListItems: tempchecklistItem1
    },
    {
        Name: "Simply PSV Checklist",
        Code: "SPC",
        CheckListItems: tempchecklistItem1
    }
];

var newPSVDefaultData = {
    Name: "",
    Code: "",
    CheckListItems: tempchecklistItem1
};

// Make sure to include the `ui.router` module as a dependency
var psv = angular.module('psvCheckList', []);

//--------------------- Tool tip Directive ------------------------------
psv.directive('tooltip', function () {
    return function (scope, elem) {
        elem.tooltip();
    };
});

//-------------------- Controller for View PSV checklist ------------------

psv.controller("viewPSVController", ["$scope", "$timeout", function ($scope, $timeout) {
    //------------------------ PSV Check list Item list --------------------

    $scope.PSVCheckListMasterData = psvMasteData;
    $scope.DataUpdated = false;
    $scope.AddNewPSV = false;
    $scope.PSVMessage = "";

    //------------------- remove psv ----------------
    $scope.RemovePSV = function (index, psv) {
        $scope.selectedPSV = psv;
        $scope.selectedIndex = index;
        $('#myModal').modal("show");
    };

    //------------------------- confirmation ------------------
    $scope.Confirmation = function (index) {
        $scope.PSVCheckListMasterData.splice(index, 1);
        $scope.selectedPSV = {};
        $scope.selectedIndex = null;
        $scope.PSVMessage = "Data Removed Success fully!!!";
        $('#myModal').modal("hide");
        $scope.DataUpdated = true;

        $timeout(function () {
            $scope.DataUpdated = false;
        }, 3000);


    };
    //----------------- cancel Remove PSV --------------------------
    $scope.CancelRemove = function () {
        $scope.selectedPSV = {};
        $scope.selectedIndex = null;
    };

    
    //---------------------- add/Edit PSV Form ----------------------
    $scope.AddNewPSVData = function (data) {
        $scope.AddNewPSV = true;
        $scope.DataUpdated = false;
        $scope.PSVMessage = "";
        if (data) {
            $scope.PSVCheckListData = angular.copy(data);
        } else {
            $scope.PSVCheckListData = newPSVDefaultData;
        }
    };

    //------------------------ edit PSV change ----------------------
    $scope.TempPSVCheckListData = [];
    $scope.ChangePSVItem = function () {
        $scope.TempPSVCheckListData = angular.copy($scope.PSVCheckListData);
    };

    //----------------------------- action data submit -------------------
    $scope.ActionSubmit = function (data, condition) {
        if (condition == 1) {
            $scope.PSVCheckListData.CheckListItems = angular.copy(data.CheckListItems);
            $scope.TempPSVCheckListData = [];
        } else if (condition == 2) {
            $scope.TempPSVCheckListData = [];
        }
    }

    //--------------------- add ne check list item --------------------
    $scope.SavePSVChecklist = function (data) {
        $scope.PSVCheckListMasterData.push(angular.copy(data));
        $scope.PSVCheckListData = {};
        $scope.PSVMessage = "PSV Checklist Data Updated Successfully!!!";
        $scope.DataUpdated = true;
        $scope.AddNewPSV = false;

        $timeout(function () {
            $scope.DataUpdated = false;
        }, 3000);
    };

    //------------------------ cancel ------------------
    $scope.cancelAll = function () {
        $scope.DataUpdated = false;
        $scope.AddNewPSV = false;
    };

}]);
