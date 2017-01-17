
profileApp.controller('WorkHistoryController', function ($scope, $http, dynamicFormGenerateService) {

    $scope.WorkHistories = [{
        EmployerName: "John Mathews",
        StartDate: "12-06-2009",
        EndDate: "15-05-2011"
    }, {
        EmployerName: "Henry Clarke",
        StartDate: "13-04-2004",
        EndDate: "16-04-2009"
    }, {
        EmployerName: "Chris Levenine",
        StartDate: "13-09-2001",
        EndDate: "12-10-2006"
    }
    ];

    $scope.WorkGaps = [{
        StartDate: "14-06-2007",
        EndDate: "17-09-2013"
    }];


    //=============== Work History Conditions ==================
    $scope.workHistoryFormStatus = false;
    $scope.newWorkHistoryForm = false;
    $scope.workGapFormStatus = false;
    $scope.newWorkGapForm = false;
    $scope.showingDetails = false;

    //====================== Work History ===================

    $scope.addWorkHistory = function () {
        $scope.workHistoryFormStatus = false;
        $scope.newWorkHistoryForm = true;
        $scope.workHistory = {};
        $("#newWorkHistoryFormDiv").html(dynamicFormGenerateService.getForm($scope, $("#workHistoryForm").html()));
    };

    $scope.addWorkGap = function () {
        $scope.workGapFormStatus = false;
        $scope.newWorkGapForm = true;
        $scope.workGap = {};
        $("#newWorkGapFormDiv").html(dynamicFormGenerateService.getForm($scope, $("#workGapForm").html()));
    };

    $scope.saveWorkHistory = function (workHistory) {
        //================== Save Here ============
        //$scope.WorkHistories.push(workHistory);
        //================== hide Show Condition ============
        $scope.workHistoryFormStatus = false;
        $scope.newWorkHistoryForm = false;
        $scope.workHistory = {};
    };

    $scope.saveWorkGap = function (workGap) {
        //================== Save Here ============
        //$scope.WorkGaps.push(workGap);
        //================== hide Show Condition ============
        $scope.workGapFormStatus = false;
        $scope.newWorkGapForm = false;
        $scope.workGap = {};
    };

    $scope.updateWorkHistory = function (workHistory) {
        $scope.showingDetails = false;
        $scope.workHistoryFormStatus = false;
        $scope.newWorkHistoryForm = false;
        $scope.workHistory = {};
    };

    $scope.updateWorkGap = function (workGap) {
        $scope.showingDetails = false;
        $scope.workGapFormStatus = false;
        $scope.newWorkGapForm = false;
        $scope.workGap = {};
    };

    $scope.editWorkHistory = function (index, workHistory) {
        $scope.showingDetails = true;
        $scope.workHistoryFormStatus = true;
        $scope.newWorkHistoryForm = false;
        $scope.workHistory = workHistory;
        $("#workHistoryEditDiv" + index).html(dynamicFormGenerateService.getForm($scope, $("#workHistoryForm").html()));
    };

    $scope.editWorkGap = function (index, workGap) {
        $scope.showingDetails = true;
        $scope.workGapFormStatus = true;
        $scope.newWorkGapForm = false;
        $scope.workGap = workGap;
        $("#workGapEditDiv" + index).html(dynamicFormGenerateService.getForm($scope, $("#workGapForm").html()));
    };

    $scope.cancelWorkHistory = function (condition) {
        if (condition == "editCancel") {
            $scope.showingDetails = false;
            $scope.workHistoryFormStatus = false;
            $scope.newWorkHistoryForm = false;
            $scope.workHistory = {};
        } else {
            $scope.showingDetails = false;
            $scope.workHistoryFormStatus = false;
            $scope.newWorkHistoryForm = false;
            $scope.workHistory = {};
        }
    };

    $scope.cancelWorkGap = function (condition) {
        if (condition == "editCancel") {
            $scope.showingDetails = false;
            $scope.workGapFormStatus = false;
            $scope.newWorkGapForm = false;
            $scope.workGap = {};
        } else {
            $scope.showingDetails = false;
            $scope.workGapFormStatus = false;
            $scope.newWorkGapForm = false;
            $scope.workGap = {};
        }
    };

    $scope.removeWorkHistory = function (index) {
        for (var i in $scope.WorkHistories) {
            if (index == i) {
                $scope.WorkHistories.splice(index, 1);
                break;
            }
        }
    };

    $scope.removeWorkGap = function (index) {
        for (var i in $scope.WorkGaps) {
            if (index == i) {
                $scope.WorkGaps.splice(index, 1);
                break;
            }
        }
    };
});