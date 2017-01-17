
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

for (var i = 1; i < 11; i++) {
    var grouInt = _.random(0, 3);
    var planInt = _.random(0, 4);
    var temp = {
        BEPlanMappingID: i,
        PlanID: Plans[planInt].PlanID,
        GroupID: Groups[grouInt].GroupID,
        Plan: Plans[planInt],
        Group: Groups[grouInt],
        MappedByID: 1,
        MappedBy: Users[_.random(0, 2)],
        ChangedByID: 1,
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
BEApp.controller("BEController", ["$scope", '$filter', '$http', "$timeout", function ($scope, $filter, $http, $timeout) {

    //--------------------- master required Data ------------------------
    $scope.BEPlanMappings = BEPlanMappings;
    $scope.Plans = Plans;
    $scope.Groups = Groups;

    //-------------------- Add Edit Data for conditional filter -----------------------
    $scope.FilterData = {
        GroupID: null,
        PlanID: null
    };

    //---------------------- get all Business Entity Plan Mapped  ----------------------------------
    $http.get(rootDir + '/Credentialing/BusinessEntity/GetAllBussinessEntityPlanMappings').
      success(function (data, status, headers, config) {
         
          //$scope.BEPlanMappings = data.BEPlanMappings;
      }).
      error(function (data, status, headers, config) {
          $scope.ShowMessage('Error', data.status);
      });

    //------------- conditional variables -----------
    $scope.IsHistoryView = false;
    $scope.IsNewAdd = false;
    $scope.IsDuplicate = false;
    $scope.IsSuccessSaved = false;

    //------------------------ save new data ----------------------
    $scope.SaveData = function (data) {
        var group = $filter('filter')($scope.Groups, { GroupID: data.GroupID})[0];
        var plan = $filter('filter')($scope.Plans, { PlanID: data.PlanID})[0];

        var temp = {
            BEPlanMappingID: 0,
            PlanID: data.PlanID,
            GroupID: data.GroupID,
            MappedByID: 1,
            ChangedByID: null,
            StatusType: 1
        };

        $http.post(rootDir + '/Credentialing/BusinessEntity/AddBEPlan', { BussinessEntityPlanMapping: temp }).
          success(function (data, status, headers, config) {
              try {

                  if (data.status == 'true') {
                      //----------- temp push controller back-end dependency -------------------
                      $scope.BEPlanMappings.push({
                          BEPlanMappingID: $scope.BEPlanMappings.length,
                          PlanID: data.PlanID,
                          GroupID: data.GroupID,
                          Plan: plan,
                          Group: group,
                          MappedByID: 1,
                          MappedBy: Users[_.random(0, 2)],
                          ChangedByID: 1,
                          ChangedBy: Users[_.random(0, 2)],
                          Status: "Active",
                          LastModifiedDate: new Date(_.random(2013, 2015), _.random(0, 11), _.random(1, 28))
                      });

                      $scope.CancelAdd();
                      $scope.ShowMessage('Success', 'Data Mapping Successfully !!!!!!!');
                  }
                  else {
                      $scope.ShowMessage('Error', data.status);
                  }
              } catch (e) {
               
              }
          }).
          error(function (data, status, headers, config) {
              $scope.ShowMessage('Error', data.status);
          });
    };

    //------------------------ cancel add data ----------------------
    $scope.CancelAdd = function () {
        $scope.IsNewAdd = false;
        $scope.IsDuplicate = false;
        $scope.FilterData = {
            GroupID: null,
            PlanID: null
        };
    };

    //------------------------------ View Business Entity Plan History Data ---------------------------
    $scope.HistoryViewed = false;
    $scope.ViewHistory = function () {
        $scope.IsHistoryView = true;
        $scope.HistoryViewed = true;

        $http.get(rootDir + '/Credentialing/BusinessEntity/GetAllBussinessEntityPlanMappingHistories').
      success(function (data, status, headers, config) {
        
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
        $http.post(rootDir + '/Credentialing/BusinessEntity/RemoveBEPlan', { BEPlanMappingID: data1.BEPlanMappingID }).
          success(function (data, status, headers, config) {
              try {

                  if (data.status == 'true') {
                      //----------- temp push controller back-end dependency -------------------
                      $scope.BEPlanMappings.splice($scope.BEPlanMappings.indexOf(data1), 1);
                      $scope.SeletcedData = {};
                      $("#BEConfirmation").modal('hide');
                      $scope.ShowMessage('Success', 'Data Removed Successfully !!!!!!!');

                      if ($scope.HistoryViewed) {

                          $scope.BEPlanMappingHistories.push(data.BussinessEntityPlanMapping);
                      }
                  }
                  else {
                      $scope.ShowMessage('Error', data.status);
                  }
              } catch (e) {
                 
              }
          }).
          error(function (data, status, headers, config) {
              $scope.ShowMessage('Error', data.status);
          });
    };

    //------------------------------ duplicate check data ---------------------------
    $scope.CheckDuplicateData = function (data) {
        var mappings = $filter('filter')($scope.BEPlanMappings, { GroupID: data.GroupID, PlanID: data.PlanID });
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
}]);
