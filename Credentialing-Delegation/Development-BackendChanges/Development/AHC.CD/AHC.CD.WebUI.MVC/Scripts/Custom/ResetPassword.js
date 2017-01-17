
//============================profile initiation Angular Module ==========================
var userApp = angular.module("Resetpasswordapp", ['mgcrea.ngStrap']);

//============================Service for sucess/error mesages ============================
userApp.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

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
}]);

//============================= profile initiation Angular controller ==========================================
userApp.controller("ResetPasswordController", ['$scope', '$timeout', '$http', 'messageAlertEngine', '$filter', '$rootScope', function ($scope, $timeout, $http, messageAlertEngine, $filter, $rootScope) {

    $scope.Roles = [];
    $scope.Roles = [
        { Code: "ADM", Name: "Admin" },
        { Code: "CCM", Name: "Credentialing Committee" },
        { Code: "CCO", Name: "Credentialing Coordinator" },
        { Code: "CRA", Name: "Credentialing Admin" },
        { Code: "HR", Name: "Humman Resource" },
        { Code: "MGT", Name: "Management" },
        { Code: "TL", Name: "Team Lead" },
    ]

    $scope.testvariable = 0;
    $scope.mydata;
    $scope.Getlist = function () {
        //$("#para1").hide();
        //$('#userstable').hide();

        $http.get(rootDir + '/Initiation/InitiateUser/SearchUser')
       .success(function (data, status, headers, config) {
           $scope.mydata = data;
           console.log($scope.mydata);
           if ($scope.mydata.Name == null && $scope.mydata.Email == null && $scope.mydata.Role == null) {
               //$('#userstable').fadeOut(10);
               //$("#para1").show();
           }
           else {
               //$('#emailid').val("");
               ////$('#searchbutton').attr('disabled', true);
               //$("#para1").hide();
               //$('#userstable').fadeIn();
           }
           //$scope.testvariable = 1;
       }).
        error(function (data, status, headers, config) {
        });
        //$("#para").hide();
    }

    $('#selectid').change(function () {
        var value = $(this).val();
        if (value == "" || value == $scope.mydata.Role)
            $('#updatebutton').attr('disabled', true);
        else
            $('#updatebutton').attr('disabled', false);
    });

    $scope.ChangeRole = function (newRole, Email) {
        $http.get(rootDir + '/Initiation/InitiateUser/ChangeRole', { params: { "RoleCode": newRole, "Email": Email } })
      .success(function (data, status, headers, config) {
          $scope.mydata = data;
          $scope.currentdata = data;
          $('#successpara').show().css({ "display": "block" });
          setTimeout(function () {
              $('#successpara').css({ "display": "none" });
          }, 5000);
          $('#updatebutton').attr('disabled', true);
      }).
       error(function (data, status, headers, config) {
           $('#failurepara').show();
       });
    }
}]);