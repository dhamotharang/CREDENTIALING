
var userApp = angular.module("Resetpasswordapp", ['ngAnimate', 'toaster','ui.bootstrap', 'mgcrea.ngStrap']);

userApp.controller("ResetPasswordController", ['$scope', '$http', '$rootScope', 'toaster', function ($scope, $http, $rootScope, toaster) {
    $scope.showLoading = false;
    
    $scope.Roles = [];
    $scope.Roles = [
        { Code: "ADM", Name: "Admin" },
        { Code: "CCM", Name: "Credentialing Committee" },
        { Code: "CCO", Name: "Credentialing Coordinator" },
        { Code: "CRA", Name: "Credentialing Admin" },
        { Code: "HR", Name: "Humman Resource" },
        { Code: "MGT", Name: "Management" },
        { Code: "TL", Name: "Team Lead" },
    ];
    $scope.role;
    $scope.testvariable = 0;
    $scope.mydata;
    $scope.UsersPagination = [];
    $scope.errormessage;
    $scope.Getlist = function () {
        var d = new $.Deferred();
        $scope.showLoading = true;
        $http.get(rootDir + '/Account/SearchUser')
       .success(function (data, status, headers, config) {
           $scope.mydata = angular.copy(data);
           for (var i in $scope.mydata)
           {
               if ($scope.mydata[i].Name == null) { $scope.mydata[i].Name = "";}
           }
           if ($scope.mydata.length > 9) {
               for (i = 0; i < 10; i++) {
                   $scope.UsersPagination[i] = $scope.mydata[i];
               }
               $scope.bigTotalItems = $scope.mydata.length;

               $scope.CurrentPage = [];
               $scope.bigCurrentPage = 1;

               var startIndex = ($scope.bigCurrentPage - 1) * 10;
               var endIndex = startIndex + 9;

               for (startIndex; startIndex <= endIndex ; startIndex++) {
                   if ($scope.mydata[startIndex]) {
                       $scope.CurrentPage.push($scope.mydata[startIndex]);
                   } else {
                       break;
                   }
               }
           }
           
           else {
               $scope.UsersPagination = angular.copy($scope.mydata);
               $scope.bigTotalItems = $scope.mydata.length;
           }
           $scope.showLoading = false;
           d.resolve();
           
           //$('#mydiv').show();
       }).
        error(function (data, status, headers, config) {
            $scope.showLoading = false;
            //return d.promise();
        });
        return d.promise();
    }

    $scope.changerolefun = function (authid, index) {
        var value = [];
        for (var i in $scope.mydata)
        {
            if($scope.mydata[i].AuthenticateUserId == authid)
            {
                value.push($scope.mydata[i].Role);
            }
        }
        var val = document.getElementsByClassName('selectchangerole')[index].value;
        if (value.length == 1) {
            if (val == "" || val == $scope.mydata[index].Role) {
                $('#updatebutton' + index).attr('disabled', true);
            }
            else
                $('#updatebutton' + index).attr('disabled', false);
        }else
        {
            for(var j in value)
            {
                if (val == "" || val == value[0]) {
                    $('#updatebutton' + index).attr('disabled', true);
                }
                else
                    $('#updatebutton' + index).attr('disabled', false);
            }
            }
        }
    
    $scope.addrolefun = function (authid, index) {
        var value = [];
        for (var i in $scope.mydata) {
            if ($scope.mydata[i].AuthenticateUserId == authid) {
                value.push($scope.mydata[i].Role);
            }
        }
        var val = document.getElementsByClassName('selectaddrole')[index].value;
        if (value.length == 1) {
            if (val == "" || val == $scope.mydata[index].Role) {
                $('#addbutton' + index).attr('disabled', true);
            }
            else
                $('#addbutton' + index).attr('disabled', false);
        } else {
            for (var j in value) {
                if (val == "" || val == value[0]) {
                    $('#addbutton' + index).attr('disabled', true);
                }
                else
                    $('#addbutton' + index).attr('disabled', false);
            }
        }


    }

    $scope.ChangeRole = function (newRole, Email, authid, oldrole) {
        $scope.showLoading = true;
        $http.get(rootDir + '/Account/ChangeRole', { params: { "NewRoleCode": newRole, "Email": Email, "authId": authid, "OldRoleCode": oldrole } })
      .success(function (data, status, headers, config) {
          var promise = $scope.Getlist().then(function () { 
              toaster.pop('Success', "Success", 'Role Changed successfully');
              $('.tulasi').attr('disabled', true);
          });
      }).
       error(function (data, status, headers, config) {
           toaster.pop('error', "", 'Error occured while changing Role.. Please try after sometime');
           $scope.showLoading = false;
       });
    }

    $scope.PasswordReset = function (Email) {
        $scope.showLoading = true;
        $http.get(rootDir + "/Account/PasswordReset", { params: { "Email": Email } })
      .success(function (data) {
          $scope.showLoading = false;
          toaster.pop('Success', "Success", 'Password has been resetted successfully');
      })
          .error(function (data, status, headers, config) {
              toaster.pop('error', "", 'Error occured while resetting Password.. Please try after sometime');
              $scope.showLoading = false;
          });
    }

    $scope.RemoveRole = function(role,email,authid)
    {
        $scope.showLoading = true;
        $http.get(rootDir + '/Account/RemoveRoleofaUser', { params: { "role": role, "email": email, "authid": authid } })
      .success(function (data, status, headers, config) {
          if (data == 'True') {
             var promise = $scope.Getlist().then(function () {
                  toaster.pop('Success', "Success", 'Role removed successfully');
              });
          }
          else
          {
              $scope.showLoading = false;
          }
      }).
       error(function (data, status, headers, config) {
           toaster.pop('error', "", 'Error occured while removing Role.. Please try after sometime');
           $scope.showLoading = false;
       });
    }

    $scope.AddRole = function (newRole, Email, authid) {
        $scope.showLoading = true;
        $http.get(rootDir + '/Account/AddRoleforaUser', { params: { "role": newRole, "email": Email, "authid": authid } })
      .success(function (data, status, headers, config) {
         var promise = $scope.Getlist().then(function () {
              toaster.pop('Success', "Success", 'Role added successfully');
              $('.tulasi').attr('disabled', true);
          });
      }).
       error(function (data, status, headers, config) {
           toaster.pop('error', "", 'Error occured while adding Role.. Please try after sometime');
           $scope.showLoading = false;
       });
    }

    //pagination

    $scope.CurrentPage = [];
    $scope.maxSize = 5;
    $scope.bigTotalItems;// = $scope.mydata.length;
    $scope.bigCurrentPage = 1;

    //-------------------- page change action ---------------------
    $scope.pageChanged = function (pagnumber) {
        $scope.bigCurrentPage = pagnumber;
    };

    //-------------- current page change Scope Watch ---------------------
    $scope.$watch('bigCurrentPage', function (newValue, oldValue) {
        $scope.UsersPagination = [];
        $scope.CurrentPage = [];
        var startIndex = (newValue - 1) * 10;
        var endIndex = startIndex + 9;
        if ($scope.mydata) {
            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.mydata[startIndex]) {
                    $scope.CurrentPage.push($scope.mydata[startIndex]);
                    $scope.UsersPagination.push($scope.mydata[startIndex]);
                } else {
                    break;
                }
            }
        }
    });
}]);


