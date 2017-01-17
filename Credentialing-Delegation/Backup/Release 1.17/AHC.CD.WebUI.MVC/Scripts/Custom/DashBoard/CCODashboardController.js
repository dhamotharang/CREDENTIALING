//------------------- CCO Dashboard Controller --------------------

dashboardApp.controller("CCODashboardController", ["$scope", "$http", function ($scope, $http) {

    //------------------- Master data comes from database ------------------------

    $http.get('/Profile/MasterData/GetAllProviderTypes').
      success(function (data, status, headers, config) {
          $scope.ProviderTypes = data;
          console.log("Provider Types");
          console.log(data);
      }).
      error(function (data, status, headers, config) {
          console.log("Sorry internal master data cont able to fetch.");
      });

    var licenses = [{

    }];
}]);
