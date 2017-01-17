
//-------------------- author : krglv -------------------------------

//------------------ psv master data temp -----------------------------

var tempchecklistItem1 = [];

var psvMasteData = [
    {
        CheckListID: 1,
        Name: "Freedom PSV Checklist",
        Code: "FPC",
        CheckListItems: tempchecklistItem1
    },
    {
        CheckListID: 2,
        Name: "Ultimate PSV Checklist",
        Code: "UPC",
        CheckListItems: tempchecklistItem1
    },
    {
        CheckListID: 3,
        Name: "Coventry PSV Checklist",
        Code: "CPC",
        CheckListItems: tempchecklistItem1
    },
    {
        CheckListID: 4,
        Name: "Humana PSV Checklist",
        Code: "HPC",
        CheckListItems: tempchecklistItem1
    },
    {
        CheckListID: 5,
        Name: "Wellcare PSV Checklist",
        Code: "WPC",
        CheckListItems: tempchecklistItem1
    },
    {
        CheckListID: 6,
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
    $scope.PSVMessage = "";

    $scope.AddBE = function () {
        $('#ShowDataPlan').show();
        $("#ShowDataHist").hide();

        $scope.LoadData = [
            {
                BusinessEntity: "Business Entity 1",
                By: "Jeanine Martin",
                Loc: "Spring Hill",
                Date: "05-09-2014",
                Plan: "Blue Cross Blue Shield",
                Status: "Active",
                BY1: "Jeanine Martin",


            },
            {
                BusinessEntity: "Business Entity 2",
                By: "Jeanine Martin",
                Loc: "Spring Hill",
                Date: "09-09-2014",
                Plan: "Wellcare",
                Status: "Active",
                BY1: "Jeanine Martin",

            },
            {
                BusinessEntity: "Business Entity 1",
                By: "Jeanine Martin",
                Loc: "Spring Hill",
                Date: "05-05-2014",
                Plan: "Freedom",
                Status: "Active",
                BY1: "Jeanine Martin",

            }
        ];
    };
    $scope.ViewBE = function () {
        $('#ShowDataPlan').hide();
        $("#ShowDataHist").show();

        $scope.LoadData = [
            {
                BusinessEntity: "Business Entity 2",
                By: "Jeanine Martin",
                Loc: "Spring Hill",
                Date: "05-09-2014",
                Plan: "Freedom",
                Status: "Active",
                BY1: "Jeanine Martin",


            },
            {
                BusinessEntity: "Business Entity 1",
                By: "Jeanine Martin",
                Loc: "Spring Hill",
                Date: "09-09-2014",
                Plan: "Ultimate",
                Status: "Active",
                BY1: "Jeanine Martin",

            },
            {
                BusinessEntity: "Business Entity 1",
                By: "Jeanine Martin",
                Loc: "Spring Hill",
                Date: "05-05-2014",
                Plan: "Wellcare",
                Status: "Active",
                BY1: "Jeanine Martin",

            }
        ];
    };
    //-------------------- master data --------------------
    $scope.ChecklistItems = [
        "Freedom PSV Checklist", "Ultimate PSV Checklist",
        "Coventry PSV Checklist", "Humana PSV Checklist", "Wellcare PSV Checklist",
        "Tricare PSV Checklist", "Simply PSV Checklist"
    ]
    $scope.Plans = ["Freedom 0931", "Plan 2", "Plan 3"];
    $scope.PanTypes = ["Commercial", "Commercial 1", "HMO 1"];
    $scope.LOB = ["LOB1", "LOB2", "LOB3"]

    $scope.MappingData = [
        {
            PlanName: "Freedom 0931",
            PlanType: "Commercial",
            PSVCheckListName: "Simply PSV Checklist",
            LOB: "LOB1"
        },
        {
            PlanName: "Plan 2",
            PlanType: "Commercial",
            PSVCheckListName: "Tricare PSV Checklist",
            LOB: "LOB2"
        },
        {
            PlanName: "Plan 3",
            PlanType: "Commercial 1",
            PSVCheckListName: "Wellcare PSV Checklist",
            LOB: "LOB3"
        },
        {
            PlanName: "Freedom 0931",
            PlanType: "HMO 1",
            PSVCheckListName: "Humana PSV Checklist",
            LOB: "LOB1"
        }
    ];

    //------------------- remove psv ----------------
    $scope.RemovePSV = function (index, psv) {
        $scope.selectedPSV = psv;
        $scope.selectedIndex = index;
        $('#myModal').modal("show");
    };

    //------------------------- confirmation ------------------
    $scope.Confirmation = function (index) {
        $scope.MappingData.splice(index, 1);
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

    //--------------------- add ne check list item --------------------
    $scope.SavePSVChecklist = function (data) {
        $scope.PSVCheckListMasterData.push(angular.copy(data));
        $scope.PSVCheckListData = {};
        $scope.PSVMessage = "PSV Checklist Data Updated Successfully!!!";
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
            PSVCheckListName: $scope.selectedChecklist,
            LOB: $scope.selectedLOB
        };
        $scope.MappingData.push(angular.copy(temp));

        $scope.CancelMapping();

        $scope.PSVMessage = "PSV Checklist Plan Mapping Data Updated Successfully!!!";
        $scope.DataUpdated = true;

        $timeout(function () {
            $scope.DataUpdated = false;
        }, 5000);
    };
}]);
