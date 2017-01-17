
//-------------------- author : krglv -------------------------------

//------------------ Document master data temp -----------------------------

var tempchecklistItem1 = [];

var DocumentMasteData = [
    {
        CheckListID: 1,
        Name: "Freedom Document Checklist",
        Code: "FPC",
        CheckListItems: tempchecklistItem1
    },
    {
        CheckListID: 2,
        Name: "Ultimate Document Checklist",
        Code: "UPC",
        CheckListItems: tempchecklistItem1
    },
    {
        CheckListID: 3,
        Name: "Coventry Document Checklist",
        Code: "CPC",
        CheckListItems: tempchecklistItem1
    },
    {
        CheckListID: 4,
        Name: "Humana Document Checklist",
        Code: "HPC",
        CheckListItems: tempchecklistItem1
    },
    {
        CheckListID: 5,
        Name: "Wellcare Document Checklist",
        Code: "WPC",
        CheckListItems: tempchecklistItem1
    },
    {
        CheckListID: 6,
        Name: "Tricare Document Checklist",
        Code: "TPC",
        CheckListItems: tempchecklistItem1
    },
    {
        Name: "Simply Document Checklist",
        Code: "SPC",
        CheckListItems: tempchecklistItem1
    }
];

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
    $scope.DocumentMessage = "";

    //-------------------- master data --------------------
    $scope.ChecklistItems = [
        "Freedom Document Checklist", "Ultimate Document Checklist",
        "Coventry Document Checklist", "Humana Document Checklist", "Wellcare Document Checklist",
        "Tricare Document Checklist", "Simply Document Checklist"
    ]
    $scope.Plans = ["Freedom 0931", "Plan 2", "Plan 3"];
    $scope.PanTypes = ["Commercial", "Commercial 1", "HMO 1"];
    $scope.LOB = ["LOB1", "LOB2", "LOB3"]

    $scope.MappingData = [
        {
            PlanName: "Freedom 0931",
            PlanType: "Commercial",
            DocumentCheckListName: "Simply Document Checklist",
            LOB: "LOB1"
        },
        {
            PlanName: "Plan 2",
            PlanType: "Commercial",
            DocumentCheckListName: "Tricare Document Checklist",
            LOB: "LOB2"
        },
        {
            PlanName: "Plan 3",
            PlanType: "Commercial 1",
            DocumentCheckListName: "Wellcare Document Checklist",
            LOB: "LOB3"
        },
        {
            PlanName: "Freedom 0931",
            PlanType: "HMO 1",
            DocumentCheckListName: "Humana Document Checklist",
            LOB: "LOB1"
        }
    ];

    //------------------- remove Document ----------------
    $scope.RemoveDocument = function (index, Document) {
        $scope.selectedDocument = Document;
        $scope.selectedIndex = index;
        $('#myModal').modal("show");
    };

    //------------------------- confirmation ------------------
    $scope.Confirmation = function (index) {
        $scope.MappingData.splice(index, 1);
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

    //--------------------- add ne check list item --------------------
    $scope.SaveDocumentChecklist = function (data) {
        $scope.DocumentCheckListMasterData.push(angular.copy(data));
        $scope.DocumentCheckListData = {};
        $scope.DocumentMessage = "Document Checklist Data Updated Successfully!!!";
        $scope.DataUpdated = true;

        $timeout(function () {
            $scope.DataUpdated = false;
        }, 5000);
    };

    //------------------- cancel mmaping -----------------
    $scope.CancelMapping = function () {
        $scope.selectedPlan = "";
        $scope.selectedPlanType = "";
        $scope.selectedChecklist = "";
        $scope.selectedLOB = "";
    };

    //------------------- Save  mmaping -----------------
    $scope.SaveMapping = function () {
        var temp = {
            PlanName: $scope.selectedPlan,
            PlanType: $scope.selectedPlanType,
            DocumentCheckListName: $scope.selectedChecklist,
            LOB: $scope.selectedLOB
        };
        $scope.MappingData.push(angular.copy(temp));

        $scope.CancelMapping();

        $scope.DocumentMessage = "Document Checklist Plan Mapping Data Updated Successfully!!!";
        $scope.DataUpdated = true;

        $timeout(function () {
            $scope.DataUpdated = false;
        }, 5000);
    };
}]);
