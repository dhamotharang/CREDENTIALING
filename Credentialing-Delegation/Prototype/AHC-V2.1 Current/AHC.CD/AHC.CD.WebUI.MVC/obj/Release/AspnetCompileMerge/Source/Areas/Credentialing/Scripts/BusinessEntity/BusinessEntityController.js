
//-------------------- author : krglv -------------------------------

//------------------ Business Entity and Plan --> Master Data Temp -----------------------------

var Plans = [
    {
        PlanID: 1,
        PlanCode: "Blue Cross Blue Shield",
        PlanTitle: "Blue Cross Blue Shield",
        IsDelegated: false,
        EmailID: "",
        Status: "Active",
        LastModifiedDate: new Date(2013, 04, 06)
    },
    {
        PlanID: 2,
        PlanCode: "PLAN2",
        PlanTitle: "Plan 2",
        IsDelegated: false,
        EmailID: "",
        Status: "Active",
        LastModifiedDate: new Date(2013, 04, 06)
    },
    {
        PlanID: 3,
        PlanCode: "PLAN3",
        PlanTitle: "Plan 3",
        IsDelegated: false,
        EmailID: "",
        Status: "Active",
        LastModifiedDate: new Date(2013, 04, 06)
    },
    {
        PlanID: 4,
        PlanCode: "PLAN4",
        PlanTitle: "Plan 4",
        IsDelegated: false,
        EmailID: "",
        Status: "Active",
        LastModifiedDate: new Date(2013, 04, 06)
    },
    {
        PlanID: 5,
        PlanCode: "PLAN5",
        PlanTitle: "Plan 5",
        IsDelegated: false,
        EmailID: "",
        Status: "Active",
        LastModifiedDate: new Date(2013, 04, 06)
    }
];

var Groups = [
    {
        GroupID: 1,
        Name: "Access",
        Description: "",
        Code: "",
        Status: "Active",
        LastModifiedDate: new Date(2013, 04, 06)
    },
    {
        GroupID: 2,
        Name: "Access2",
        Description: "",
        Code: "ACCESS2",
        Status: "Active",
        LastModifiedDate: new Date(2013, 04, 06)
    },
    {
        GroupID: 3,
        Name: "MIRRA",
        Description: "",
        Code: "MIRRA",
        Status: "Active",
        LastModifiedDate: new Date(2013, 04, 06)
    },
    {
        GroupID: 4,
        Name: "ACO",
        Description: "ACO",
        Code: "ACO",
        Status: "Active",
        LastModifiedDate: new Date(2013, 04, 06)
    }
];

var Users = ["Jeanine Martin", "Dr. V","Dr. Nitesh"];

var BEPlanMappings = [];

for (var i = 0; i < 10; i++) {
    var grouInt = _.random(0, 3);
    var planInt = _.random(0, 4);
    var temp = {
        MappingID: i,
        PlanID: grouInt,
        GroupID: planInt,
        Plan: Plans[planInt],
        Group: Groups[grouInt],
        LoggedBy: Users[_.random(0, 2)],
        ChangedBy: Users[_.random(0, 2)],
        Status: "Active",
        LastModifiedDate: new Date(_.random(2013, 2015), _.random(0, 11), _.random(1, 28))
    };
    BEPlanMappings.push(temp);
}

//--------------- angular module -----------------------
var BEApp = angular.module('BEApp', []);

//--------------------- Tool tip Directive ------------------------------
BEApp.directive('tooltip', function () {
    return function (scope, elem) {
        elem.tooltip();
    };
});

//-------------------- Controller ------------------
BEApp.controller("BEController", ["$scope", '$filter', "$timeout", function ($scope, $filter, $timeout) {

    //--------------------- master required Data ------------------------
    $scope.BEPlanMappings = BEPlanMappings;
    $scope.Plans = Plans;
    $scope.Groups = Groups;

    //-------------------- Add Edit Data for conditional filter -----------------------
    $scope.FilterData = {
        GroupID: 0,
        PlanID: 0
    };

    //------------- conditional variables -----------
    $scope.IsHistoryView = false;
    $scope.IsNewAdd = false;

    //------------------------ save new data ----------------------
    $scope.SaveData = function (data) {

        var group = $filter('filter')($scope.Groups, { GroupID: data.GroupID})[0];
        var plan = $filter('filter')($scope.Plans, { PlanID: data.PlanID})[0];

        $scope.BEPlanMappings.push({
            MappingID: $scope.BEPlanMappings.length,
            PlanID: data.GroupID,
            GroupID: data.PlanID,
            Plan: plan,
            Group: group,
            LoggedBy: Users[_.random(0, 2)],
            ChangedBy: Users[_.random(0, 2)],
            Status: "Active",
            LastModifiedDate: new Date(_.random(2013, 2015), _.random(0, 11), _.random(1, 28))
        });
        $scope.IsNewAdd = false;
        $scope.CancelAdd();
    };

    //------------------------ cancel add data ----------------------
    $scope.CancelAdd = function () {
        $scope.IsNewAdd = false;
        $scope.FilterData = {
            GroupID: 0,
            PlanID: 0
        };
    };

    $scope.HideHistory = function () {
        $scope.IsHistoryView = false;
    };

    $scope.RemoveBE = function (data) {
        $scope.SeletcedData = data;
        $("#BEConfirmation").modal('show');
    };

    $scope.Confirmation = function (data) {
        
        $scope.BEPlanMappings.splice($scope.BEPlanMappings.indexOf(data), 1);

        $scope.SeletcedData = {};

        $("#BEConfirmation").modal('hide');
    };

    $scope.CheckDuplicateData = function () {

    };

}]);
