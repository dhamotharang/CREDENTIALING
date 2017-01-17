
var userApp = angular.module("Resetpasswordapp", ['mgcrea.ngStrap']);

userApp.controller("ResetPasswordController", ['$scope', '$http', '$rootScope', function ($scope, $http, $rootScope) {

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
    $scope.role;
    $scope.testvariable = 0;
    $scope.mydata;
    $scope.errormessage;
    $scope.Getlist = function () {
        $http.get(rootDir + '/Account/SearchUser')
       .success(function (data, status, headers, config) {
           $scope.mydata = data;
       }).
        error(function (data, status, headers, config) {
        });
    }

    $scope.myfun = function (index) {
        var val = document.getElementsByTagName('select')[index].value;
        if (val == "" || val == $scope.mydata[index].Role) {
            $('#updatebutton' + index).attr('disabled', true);
        }
        else
            $('#updatebutton' + index).attr('disabled', false);
    }

    $scope.ChangeRole = function (newRole, Email) {
        $http.get(rootDir + '/Account/ChangeRole', { params: { "RoleCode": newRole, "Email": Email } })
      .success(function (data, status, headers, config) {
          $scope.Getlist();
          $scope.errormessage = "Role updated successfully for the user " + data.Email;
          $('#updatebutton').attr('disabled', true);
      }).
       error(function (data, status, headers, config) {
           $scope.errormessage = "An Error Occured while updating. Please try again later.";
       });
    }

    $scope.PasswordReset = function (Email) {
        $http.get(rootDir + "/Account/PasswordReset", { params: { "Email": Email } })
      .success(function (data) {
          $scope.errormessage = "Password Resetted successfully for the user " + data.Email;
      })
          .error(function (data, status, headers, config) {
              $scope.errormessage = "An Error Occured while Resetting password. Please try again later.";
          });
    }
}]);


