
//-------------------- author : krglv -------------------------------

//------------------ Document master data temp -----------------------------

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

var DocumentMasteData = [
    {
        Name: "Freedom Document Checklist",
        Code: "FDC",
        CheckListItems: tempchecklistItem1
    },
    {
        Name: "Ultimate Document Checklist",
        Code: "UDC",
        CheckListItems: tempchecklistItem1
    },
    {
        Name: "Coventry Document Checklist",
        Code: "CDC",
        CheckListItems: tempchecklistItem1
    },
    {
        Name: "Humana Document Checklist",
        Code: "HDC",
        CheckListItems: tempchecklistItem1
    },
    {
        Name: "Wellcare Document Checklist",
        Code: "WDC",
        CheckListItems: tempchecklistItem1
    },
    {
        Name: "Tricare Document Checklist",
        Code: "TDC",
        CheckListItems: tempchecklistItem1
    },
    {
        Name: "Simply Document Checklist",
        Code: "SDC",
        CheckListItems: tempchecklistItem1
    }
];

var newDocumentDefaultData = {
    Name: "",
    Code: "",
    CheckListItems: tempchecklistItem1
};

// Make sure to include the `ui.router` module as a dependency
var Document = angular.module('DocumentCheckList', []);

//--------------------- Tool tip Directive ------------------------------
Document.directive('tooltip', function () {
    return function (scope, elem) {
        elem.tooltip();
    };
});

//-------------------- Controller for View Document checklist ------------------

Document.controller("viewDocumentController", ["$scope", "$timeout", function ($scope, $timeout) {
    //------------------------ Document Check list Item list --------------------

    $scope.DocumentCheckListMasterData = DocumentMasteData;
    $scope.DataUpdated = false;
    $scope.AddNewDocument = false;
    $scope.DocumentMessage = "";

    //------------------- remove Document ----------------
    $scope.RemoveDocument = function (index, Document) {
        $scope.selectedDocument = Document;
        $scope.selectedIndex = index;
        $('#myModal').modal("show");
    };

    //------------------------- confirmation ------------------
    $scope.Confirmation = function (index) {
        $scope.DocumentCheckListMasterData.splice(index, 1);
        $scope.selectedDocument = {};
        $scope.selectedIndex = null;
        $scope.DocumentMessage = "Data Removed Success fully!!!";
        $('#myModal').modal("hide");
        $scope.DataUpdated = true;

        $timeout(function () {
            $scope.DataUpdated = false;
        }, 3000);


    };
    //----------------- cancel Remove Document --------------------------
    $scope.CancelRemove = function () {
        $scope.selectedDocument = {};
        $scope.selectedIndex = null;
    };


    //---------------------- add/Edit Document Form ----------------------
    $scope.AddNewDocumentData = function (data) {
        $scope.AddNewDocument = true;
        $scope.DataUpdated = false;
        $scope.DocumentMessage = "";
        if (data) {
            $scope.DocumentCheckListData = angular.copy(data);
        } else {
            $scope.DocumentCheckListData = newDocumentDefaultData;
        }
    };

    //------------------------ edit Document change ----------------------
    $scope.TempDocumentCheckListData = [];
    $scope.ChangeDocumentItem = function () {
        $("#ShowAdd").show();
        $scope.TempDocumentCheckListData = angular.copy($scope.DocumentCheckListData);
    };

    //----------------------------- action data submit -------------------
    $scope.ActionSubmit = function (data, condition) {
        $("#ShowAdd").hide();

        if (condition == 1) {
            $scope.DocumentCheckListData.CheckListItems = angular.copy(data.CheckListItems);
            $scope.TempDocumentCheckListData = [];
        } else if (condition == 2) {
            $scope.TempDocumentCheckListData = [];
        }
    }

    //--------------------- add ne check list item --------------------
    $scope.SaveDocumentChecklist = function (data) {
        $scope.DocumentCheckListMasterData.push(angular.copy(data));
        $scope.DocumentCheckListData = {};
        $scope.DocumentMessage = "Document Checklist Data Updated Successfully!!!";
        $scope.DataUpdated = true;
        $scope.AddNewDocument = false;

        $timeout(function () {
            $scope.DataUpdated = false;
        }, 3000);
    };

    //------------------------ cancel ------------------
    $scope.cancelAll = function () {
        $scope.DataUpdated = false;
        $scope.AddNewDocument = false;
    };

}]);
