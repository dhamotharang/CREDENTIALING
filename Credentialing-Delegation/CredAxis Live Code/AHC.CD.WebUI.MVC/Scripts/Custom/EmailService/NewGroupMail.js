EmailServiceApp.directive('stRatio', function () {
    return {
        link: function (scope, element, attr) {
            var ratio = +(attr.stRatio);

            element.css('width', ratio + '%');

        }
    };
});

EmailServiceApp.filter('myCustomFilter', ['$filter', function ($filter) {

    // function that's invoked each time Angular runs $digest()
    return function (input, predicate) {
        searchValue = predicate['$'];
        //console.log(searchValue);
        var customPredicate = function (value, index, array) {
            console.log(value);

            // if filter has no value, return true for each element of the input array
            if (typeof searchValue === 'undefined') {
                return true;
            }

            var p0 = value['FullName'].toLowerCase().indexOf(searchValue.toLowerCase());
            var p1 = value['Ipa'].toLowerCase().indexOf(searchValue.toLowerCase());
            if (p0 > -1 || p1 > -1) {
                return true;
            } else {
                return false;
            }
        }

        //console.log(customPredicate);
        return $filter('filter')(input, customPredicate, false);
    }
}])

EmailServiceApp.directive('stSelectAll', function () {
    return {
        restrict: 'E',
        template: '<input type="checkbox" ng-model="isAllSelected" id="checkboxAll" />',
        scope: {
            all: '='
        },
        link: function (scope, element, attr) {

            scope.$watch('isAllSelected', function () {
                scope.all.forEach(function (val) {
                    val.isChecked = scope.isAllSelected;
                })
            });

            scope.$watch('all', function (newVal, oldVal) {
                if (oldVal) {
                    oldVal.forEach(function (val) {
                        val.isChecked = false;
                    });
                }

                scope.isAllSelected = false;
            });
        }
    }
});

EmailServiceApp.directive('pageSelect', function () {
    return {
        restrict: 'E',
        template: '<input id="paginationId" type="text" class="select-page" ng-model="inputPage" ng-change="PageResize(inputPage,numPages)">',
        controller: function ($scope) {
            $scope.$watch('inputPage', function (newV, oldV) {
                if (newV === oldV) {
                    return;
                }
                else if (newV >= oldV) {
                    $scope.inputPage = newV;
                    $scope.selectPage(newV);
                    //$scope.currentPage = newV;
                }
                else {
                    $scope.selectPage(newV);
                }

            });
            $scope.PageResize = function (currentPage, maxPage) {
                if (currentPage >= maxPage) {
                    $scope.inputPage = maxPage;
                    $scope.selectPage(maxPage);
                }
                else {
                    $scope.selectPage(currentPage);
                }
            }
        },
        link: function (scope, element, attrs) {
            scope.$watch('currentPage', function (c) {
                scope.inputPage = c;
            });
        }
    }
});

//EmailServiceApp.directive('bootstrapDropdown', ['$timeout',
//    function ($timeout) {
//        return {
//            restrict: 'A',
//            require: '?ngModel',
//            link: function (scope, element, attrs, ngModel) {
//                $timeout(function () {
//                    element.selectpicker();
//                });
//            }
//        };
//    }
//]);

EmailServiceApp.factory('Resource1', ['$q', '$rootScope', '$filter', '$timeout', function ($q, $rootScope, $filter, $timeout) {
    function getPage(start, number, params) {

        var deferred = $q.defer();

        $rootScope.filtered = params.search.predicateObject ? $filter('filter')($rootScope.groupMailList, params.search.predicateObject) : $rootScope.groupMailList;

        if (params.sort.predicate) {
            $rootScope.filtered = $filter('orderBy')($rootScope.filtered, params.sort.predicate, params.sort.reverse);
        }
        var result = $rootScope.filtered.slice(start, start + number);
        $timeout(function () {
            //note, the server passes the information about the data set size
            deferred.resolve({
                data: result,
                numberOfPages: Math.ceil($rootScope.filtered.length / number)
            });
        }, 500);
        return deferred.promise;
    }
    return {
        getPage: getPage
    };

}]);

EmailServiceApp.value("optionValues", []);
//EmailServiceApp.value("providerData", []);
EmailServiceApp.value("ownerName", []);
EmailServiceApp.value("tempSearchDataVal", []);
EmailServiceApp.value("tempUserSearchDataVal", []);
EmailServiceApp.value("TabName", '');
EmailServiceApp.value("View", [{ tabName: "Provider", providerLevel: true, IPA: true, Role: false, ProviderRelationship: true, NPI: true },
                               { tabName: "User", providerLevel: false, IPA: false, Role: true, ProviderRelationship: false, NPI: false },
                               { tabName: "Others", providerLevel: false, IPA: false, Role: false, ProviderRelationship: false, NPI: false },
                               { tabName: "All", providerLevel: false, IPA: false, Role: false, ProviderRelationship: false, NPI: false },
                               { tabName: "ADDEditUser", providerLevel: false, IPA: false, Role: true, ProviderRelationship: false, NPI: false }])

EmailServiceApp.filter("Status", function (View) {
    return function (status) {
        return View.filter(function (data) { return data.tabName == status })[0];
    }
})

EmailServiceApp.controller("GroupEmailController", function ($rootScope, $timeout, $scope, $http, $filter, $q, Resource1, messageAlertEngine, ngTableParams, toaster) {

    ///// declarations /////

    //$rootScope.tempObject.EmailObj = [];    
    //$rootScope.tempGroupMails = [];

    $scope.grpNameErrorMessage = false;
    $scope.descriptionErrorMessage = false;
    $rootScope.Alldatascope =false;
    var tempId = '';
    $scope.allUsers = false;
    $scope.data = [];
    $rootScope.selectedProviderData = [];
    $scope.OtherEmail = "";
    $scope.OtherPersonName = "";
    $rootScope.currentCDuserId = "";
    optionValues = { ProviderLevel: [], IPA: [], ProviderRelationship: [] }
    $scope.optionVal = optionValues;
    $scope.addNewMember = false;
    $scope.columnStatus = [];

    $rootScope.mails = function () {
        //$q.all([$q.when(getLoginUsers()), $q.when(getCurrentCdUser())]);
        //$scope.getOwnerName();
        $rootScope.groupMailList = [];
        tempSearchDataVal = [];
        tempUserSearchDataVal = [];
        $rootScope.tempSelectedObject = [];
        $rootScope.tempSelectedObject.EmailObj = [
            $scope.AllSelectStatus=false
        ];
        $rootScope.tempObject = [];
        $rootScope.tempObject.EmailObj = [];
        $rootScope.ProviderData = [];
        $rootScope.UsersData = [];
        $rootScope.otherUsersData = [];
        $rootScope.UserDataResult = [];
        $rootScope.otherUserBox = false;
        $scope.loading = true;
        $rootScope.tempEditGroupName = '';
        $rootScope.searchDataVal = JSON.parse(searchData);
        //tempSearchDataVal = angular.copy($rootScope.searchDataVal);
        $rootScope.users = false;
        TabName = 'activeGroups';
        for (var i = 0; i < $rootScope.searchDataVal.length; i++) {
            $scope.loading = true;
            $rootScope.searchDataVal[i].isChecked = false;
            if ($rootScope.searchDataVal[i].UserType == "Provider") {
                tempSearchDataVal.push($rootScope.searchDataVal[i]);
            }
            else {
                $rootScope.UserDataResult.push($rootScope.searchDataVal[i]);
            }
        }
        $rootScope.searchDataVal = angular.copy(tempSearchDataVal);
        tempUserSearchDataVal = angular.copy($rootScope.UserDataResult);

        var promise = $scope.GetAllGroupMails().then(function () {
            $scope.t = {
                sort: {},
                search: {},
                pagination: {
                    start: 0
                }
            };
            ctrl.callServer($scope.t);
            $scope.loading = false;
        });
    }

    var ctrl = this;
    this.displayed = [];
    this.callServer = function callServer(tableState) {
        ctrl.isLoading = true;
        $scope.t = tableState;
        var pagination = tableState.pagination;

        var start = pagination.start || 0;
        var number = pagination.number || 10;

        Resource1.getPage(start, number, tableState).then(function (result) {
            //if(ctrl.displayed.length!=0)
            ctrl.displayed = result.data;
            ctrl.temp = ctrl.displayed;
            console.log($('#GlobalSearchboxid').val());
            if ($('#GlobalSearchboxid').val() == 0) {
                if (ctrl.temp.length == 0)
                    $rootScope.GlobalSearchbox = true;
                else
                    $rootScope.GlobalSearchbox = false;
            }
            tableState.pagination.numberOfPages = result.numberOfPages;//set the number of pages so the pagination can update            
            ctrl.isLoading = false;
        });
    };

    $scope.initPop = function () {
        $('[data-toggle="popover"]').popover();
    };

    $scope.getOwnerName = function () {
        $q.all([$q.when(getLoginUsers()), $q.when(getCurrentCdUser())]);
    }

    $scope.getTabData = function (tabName) {
        $rootScope.$broadcast(tabName);
        TabName = tabName;
    }

    $rootScope.$on('activeGroups', function () {
        $rootScope.groupMailList = angular.copy($rootScope.activeGroups);
        $scope.t = {
            sort: {},
            search: {},
            pagination: {
                start: 0
            }
        };
        ctrl.callServer($scope.t);
    });

    $rootScope.$on('inactiveGroups', function () {
        $rootScope.groupMailList = angular.copy($rootScope.inactiveGroups);
        $scope.t = {
            sort: {},
            search: {},
            pagination: {
                start: 0
            }
        };
        ctrl.callServer($scope.t);
    });

    $scope.fetchData = function (val) {
        //console.log(val);
        //$http.get('data' + val + '.json').success(function (data) {
        //    ctrl.rowList = data;
        //    ctrl.displayed = [].concat(ctrl.rowList);
        //});
        //$http.get(rootDir + '/Profile/MasterData/GetAllProviderLevels').success(function (data) {
        //    $rootScope.groupMailList = data;
        //    ctrl.displayed = [];
        //    ctrl.temp = angular.copy(data);
        //});       

    };

    $scope.ConvertDate = function (date) {
        var followupdateforupdatetask = "";
        if (date !== null || date !== "") {
            var date5 = date.split('T');
            var date5 = date5[0].split('-');
            followupdateforupdatetask = date5[1] + "/" + date5[2] + "/" + date5[0];
        }
        return followupdateforupdatetask;
    }

    //// Http calls //////////
    $scope.GetAllGroupMails = function () {
        var d = new $q.defer();
        $http.get(rootDir + '/EmailService/GetAllGroupMails').
     success(function (data, status, headers, config) {
         $scope.loading = true;
         $rootScope.tempGroupMails = data;
         $rootScope.activeGroups = [];
         $rootScope.inactiveGroups = [];
         for (var i = 0; i < $rootScope.tempGroupMails.length; i++) {
             if ($rootScope.tempGroupMails[i].CreatedOn != null) {
                 $rootScope.tempGroupMails[i].CreatedOn = $scope.ConvertDate($rootScope.tempGroupMails[i].CreatedOn);
             }
             $rootScope.tempGroupMails[i].CreatedByEmail = $rootScope.tempGroupMails[i].CreatedBy.EmailId;
             $rootScope.tempGroupMails[i].GroupMailUserDetailslength = $rootScope.tempGroupMails[i].GroupMailUserDetails.length;
             $rootScope.tempGroupMails[i].EmailObj = [];
             for (var Key in $rootScope.tempGroupMails[i].Emails) {
                 $rootScope.tempGroupMails[i].EmailObj.push({ CDuserId: Key, EmailIds: $rootScope.tempGroupMails[i].Emails[Key], isChecked: true });
                 $rootScope.tempGroupMails[i].currentCDuserId = $rootScope.tempGroupMails[i].CurrentCDuserId;
             }
             if ($rootScope.tempGroupMails[i].Status == "Active") {
                 $rootScope.activeGroups.push($rootScope.tempGroupMails[i]);
             }
             else {
                 $rootScope.inactiveGroups.push($rootScope.tempGroupMails[i])
             }
         }
         $rootScope.groupMailList = angular.copy($rootScope.activeGroups);
         TabName = 'activeGroups';
         $scope.loading = true;
         d.resolve(data);
     }).
     error(function (data, status, headers, config) {
         d.reject();
     });
        return d.promise;
    }
    $http.get(rootDir + '/MasterData/Organization/GetGroups').
      success(function (data, status, headers, config) {
          optionValues.IPA = data;
      }).
      error(function (data, status, headers, config) {
      });

    $http.get(rootDir + '/Profile/MasterData/GetAllProviderLevels').
      success(function (data, status, headers, config) {
          optionValues.ProviderLevel = data;
      }).
      error(function (data, status, headers, config) {
      });
    $scope.UpdateGroup = function () {
        $rootScope.editgrid = false;
        if ($rootScope.tempObject.EmailObj.length == 0) {
            messageAlertEngine.callAlertMessage('grperrorMsgDiv', "Please add atleast one Email Id ....", "danger", true);
        }

        if ($rootScope.tempObject.EmailGroupName == undefined || $rootScope.tempObject.EmailGroupName == "" || $rootScope.tempObject.EmailGroupName == null) {
            $scope.grpNameErrorMessage = true;
        }
        if ($rootScope.tempObject.Description == undefined || $rootScope.tempObject.Description == "" || $rootScope.tempObject.Description == null) {
            $scope.descriptionErrorMessage = true;
        }
        var dictionary = new Object();
        for (i = 0 ; i < $rootScope.tempObject.EmailObj.length; i++) {
            if ($rootScope.tempObject.EmailObj[i].CDuserId == 0) {
                dictionary[$rootScope.tempObject.EmailObj[i].FullName] = $rootScope.tempObject.EmailObj[i].EmailIds;
            }
            else { dictionary[$rootScope.tempObject.EmailObj[i].CDuserId] = $rootScope.tempObject.EmailObj[i].EmailIds; }
            //dictionary[$rootScope.tempObject.EmailObj[i].CDuserId] = $rootScope.tempObject.EmailObj[i].EmailIds = $rootScope.tempObject.EmailObj[i].EmailIds == null ? "" : $rootScope.tempObject.EmailObj[i].EmailIds;
        }
        var obj = {
            EmailGroupName: $rootScope.tempObject.EmailGroupName,
            LastUpdatedBy: $scope.LastUpdatedBy == undefined ? '' : $scope.LastUpdatedBy,
            Description: $rootScope.tempObject.Description,
            EmailIds: JSON.stringify(dictionary),
            EmailGroupID: $rootScope.tempObject.EmailGroupId
        }
        if (!$scope.grpNameErrorMessage && !$scope.descriptionErrorMessage && $rootScope.tempObject.EmailObj.length != 0 && !$scope.CheckGroupNameifExistsForUpdate($rootScope.tempObject.EmailGroupName)) {
            $http.post(rootDir + '/EmailService/UpdateGroupMail', obj).
               success(function (data, status, headers, config) {
                   try {
                       if (data.status == "true") {
                           $scope.grpNameErrorMessage = false;
                           $scope.grpMailIdsErrorMessage = false;
                           //var filteredgrpmail = jQuery.grep($rootScope.groupMailList, function (ele) { return ele.EmailGroupId == $rootScope.tempObject.EmailGroupId });
                           try {
                               var filteredgrpmail = jQuery.grep($rootScope.activeGroups, function (ele) { return ele.EmailGroupId == $rootScope.tempObject.EmailGroupId });
                               $rootScope.activeGroups.splice($rootScope.activeGroups.indexOf(filteredgrpmail[0]), 1);
                               filteredgrpmail[0].EmailGroupName = $rootScope.tempObject.EmailGroupName;
                               filteredgrpmail[0].Description = $rootScope.tempObject.Description;
                               filteredgrpmail[0].EmailObj = [];
                               filteredgrpmail[0].GroupMailUserDetails = [];
                               for (var Key in data.result) {
                                   filteredgrpmail[0].EmailObj.push({ CDuserId: Key, EmailIds: data.result[Key], isChecked: true });
                               }
                               for (var i = 0; i < $rootScope.tempObject.EmailObj.length; i++) {
                                   for (var j = 0; j < data.result.OtherUsers.length; j++) {
                                       if ($rootScope.tempObject.EmailObj[i].EmailIds == data.result.OtherUsers[j].EmailId) {
                                           $rootScope.tempObject.EmailObj[i].CDuserId = data.result.OtherUsers[j].CDuserId;
                                       }
                                   }
                               }
                               filteredgrpmail[0].GroupMailUserDetails = angular.copy($rootScope.tempObject.EmailObj);
                               var otherUserObjForUpdate = [];
                               for (var i = 0; i < data.result.OtherUsers.length; i++) {
                                   otherUserObjForUpdate.push({
                                       CDuserId: data.result.OtherUsers[i].CDuserId,
                                       EmailIds: data.result.OtherUsers[i].EmailId,
                                       FullName: data.result.OtherUsers[i].FullName,
                                       ProviderLevel: null,
                                       UserType: "OtherUser",
                                       Roles: null,
                                       isChecked: false
                                   })
                                   $rootScope.UserDataResult.push(otherUserObjForUpdate[i]);
                                   tempUserSearchDataVal.push(otherUserObjForUpdate[i]);
                               }
                           } catch (e) {

                           }

                           $rootScope.tableView = true;
                           $rootScope.isView = false;
                           $rootScope.isEdit = false;
                           $scope.groupMail = false;
                           $rootScope.activeGroups.push(filteredgrpmail[0]);
                           $rootScope.groupMailList = angular.copy($rootScope.activeGroups);
                           ctrl.temp = $rootScope.groupMailList;
                           messageAlertEngine.callAlertMessage('grpsuccessMsgDiv', "Group updated successfully.", "success", true);
                           $rootScope.visibility = 'groupMailList';

                       } else if (data.status == "Group Email already Exists") {
                           messageAlertEngine.callAlertMessage('grperrorMsgDiv', "Group Mail already exists.", "danger", true);
                       } else {
                           messageAlertEngine.callAlertMessage('grperrorMsgDiv', "Please Try again later ....", "danger", true);
                       }
                   } catch (e) {

                   }
               }).
               error(function (data, status, headers, config) {
               });
        }
        else {
            $('html, body').animate({ scrollTop: 0 }, 800);
        }
    }

    $scope.showAddDetails = function () {
        $scope.addNewMember = true;
    }

    $scope.saveGroup = function (Form_Id) {
        $scope.grpNameErrorMessage = false;
        $scope.descriptionErrorMessage = false;
        if ($rootScope.tempObject.EmailObj.length == 0) {
            messageAlertEngine.callAlertMessage('grperrorMsgDiv', "Please add atleast one Email Id ....", "danger", true);
        }
        if ($rootScope.tempObject.EmailGroupName == undefined || $rootScope.tempObject.EmailGroupName == "" || $rootScope.tempObject.EmailGroupName == null) {
            $scope.grpNameErrorMessage = true;
        }
        if ($rootScope.tempObject.Description == undefined || $rootScope.tempObject.Description == "" || $rootScope.tempObject.Description == null) {
            $scope.descriptionErrorMessage = true;
        }
        var dictionary = new Object();
        for (i = 0 ; i < $rootScope.tempObject.EmailObj.length; i++) {
            if ($rootScope.tempObject.EmailObj[i].CDuserId == 0) {
                dictionary[$rootScope.tempObject.EmailObj[i].FullName] = $rootScope.tempObject.EmailObj[i].EmailIds;
                $rootScope.tempObject.EmailObj[i].isChecked = true;
            }
            else { dictionary[$rootScope.tempObject.EmailObj[i].CDuserId] = $rootScope.tempObject.EmailObj[i].EmailIds; }
        }
        var obj = {
            EmailGroupName: $rootScope.tempObject.EmailGroupName,
            CreatedBy: $scope.CreatedBy,
            LastUpdatedBy: $scope.LastUpdatedBy,
            Description: $rootScope.tempObject.Description,
            CduserIds: dictionary,
            EmailIds: JSON.stringify(dictionary)
        }
        if (!$scope.grpNameErrorMessage && !$scope.descriptionErrorMessage && $rootScope.tempObject.EmailObj.length != 0 && !$scope.CheckGroupNameifExists($rootScope.tempObject.EmailGroupName)) {
            $http.post(rootDir + '/EmailService/AddGroupEmail', obj).
               success(function (data, status, headers, config) {
                   try {
                       if (data.status == "true") {
                           $scope.grpNameErrorMessage = false;
                           $scope.grpMailIdsErrorMessage = false;
                           var newObj = {
                               //CreatedBy: $scope.CreatedBy,
                               CreatedBy: data.obj.CreatedBy,
                               Description: $rootScope.tempObject.Description,
                               EmailGroupId: data.obj.EmailGroupId,
                               EmailGroupName: $rootScope.tempObject.EmailGroupName,
                               LastUpdatedBy: $scope.LastUpdatedBy,
                               EmailObj: [],
                               GroupMailUserDetails: [],
                               Status: 'Active',
                               currentCDuserId: data.obj.CreatedBy.CDUserID
                           }
                           for (var Key in data.obj.Emails) {
                               newObj.EmailObj.push({ CDuserId: Key, EmailIds: data.obj.Emails[Key], isChecked: true });
                           }
                           for (var i = 0; i < $rootScope.tempObject.EmailObj.length; i++) {
                               for (var j = 0; j < data.obj.OtherUsers.length; j++) {
                                   if ($rootScope.tempObject.EmailObj[i].EmailIds == data.obj.OtherUsers[j].EmailId) {
                                       $rootScope.tempObject.EmailObj[i].CDuserId = data.obj.OtherUsers[j].CDuserId;
                                   }
                               }
                           }
                           newObj.GroupMailUserDetails.push($rootScope.tempObject.EmailObj);
                           newObj.GroupMailUserDetails = Array.prototype.concat.apply([], newObj.GroupMailUserDetails);
                           $('html, body').animate({ scrollTop: 0 }, 800);
                           $rootScope.activeGroups.push(newObj);
                           var otherUserObj = [];
                           for (var i = 0; i < data.obj.OtherUsers.length; i++) {

                               otherUserObj.push({
                                   CDuserId: data.obj.OtherUsers[i].CDuserId,
                                   EmailIds: data.obj.OtherUsers[i].EmailId,
                                   FullName: data.obj.OtherUsers[i].FullName,
                                   ProviderLevel: null,
                                   UserType: "OtherUser",
                                   Roles: null,
                                   isChecked: false
                               })
                               $rootScope.UserDataResult.push(otherUserObj[i]);
                               tempUserSearchDataVal.push(otherUserObj[i]);
                           }

                           $rootScope.groupMailList = angular.copy($rootScope.activeGroups);
                           //$rootScope.groupMailList.push(newObj);
                           //ctrl.temp = $rootScope.groupMailList;
                           $scope.t = {
                               sort: {},
                               search: {},
                               pagination: {
                                   start: 0
                               }
                           };
                           ctrl.callServer($scope.t);
                           $rootScope.visibility = 'groupMailList';
                           messageAlertEngine.callAlertMessage('grpsuccessMsgDiv', "New Group Mail Created.", "success", true);
                           $scope.groupMail = false;
                       } else if (data.status == "Group Email already Exists") {
                           messageAlertEngine.callAlertMessage('grperrorMsgDiv', "Group Mail already exists.", "danger", true);
                       } else {
                           messageAlertEngine.callAlertMessage('grperrorMsgDiv', "Please Try again later ....", "danger", true);
                       }
                   } catch (e) {
                   }
               }).
               error(function (data, status, headers, config) {
               });
        }
        else {
            $('html, body').animate({ scrollTop: 0 }, 800);
        }
    }

    $scope.resetTable = function () {
        var resetVal = {
            sort: {},
            search: {},
            pagination: {
                start: 0
            }
        }
        return resetVal;
    }

    $scope.new_search = function () {
        $scope.data = [];
        $scope.error_message = "";
        $scope.showLoading = true;
        $http({
            method: "POST",
            url: rootDir + "    /SearchProvider/SearchUserForGroupMail",
            //data: {
            //    NPINumber: null, FirstName: "a",
            //    LastName: null, ProviderRelationship: null, IPAGroupName: $('#searchIPA').val(),
            //    ProviderLevel: $('#searchProviderLevel').val(), ProviderType: null
            //}
        }).success(function (resultData) {
            try {
                var tempData = [];
                tempData = resultData.searchResults;
                for (var i = 0; i < tempData.length; i++) {
                    if (tempData[i].Roles.length != 0) {
                        tempData[i].RolePresent = 'Yes';
                    }
                    else {
                        tempData[i].RolePresent = 'No';
                    }
                    tempData[i].isChecked = false;
                    tempData[i].tempGroupId = i;
                }

            } catch (e) {
            }
            $rootScope.searchDataVal = angular.copy(tempData);
            $rootScope.groupMailList = angular.copy($rootScope.searchDataVal);
            $scope.t = {
                sort: {},
                search: {},
                pagination: {
                    start: 0
                }
            };
            ctrl.callServer($scope.t);
        }).error(function (err) {
            $scope.showLoading = false; $scope.data = "";
            console.log(err);
            alert();
        })
    }
    $scope.getAllUsersMailIds = function () {
        $http.get(rootDir + '/EmailService/GetAllCDusers').
     success(function (data, status, headers, config) {
         $scope.userData = data;
         //$scope.init_table($scope.userData);         
         $scope.allUsers = true;
     }).
     error(function (data, status, headers, config) {
     });

        $http.get(rootDir + '/TaskTracker/GetAllUsers').
            success(function (data, status, headers, config) {
                $rootScope.LoginUsers = data;
            }).
            error(function (data, status, headers, config) {
            });

        $http.get(rootDir + '/Dashboard/GetMyNotification').
        success(function (data, status, headers, config) {
            currentcduserdata = data;
            for (var i = 0; i < $rootScope.LoginUsers.length; i++) {
                if ($rootScope.LoginUsers[i].Id == currentcduserdata.cdUser.AuthenicateUserId) {
                    ownerName = $rootScope.LoginUsers[i].UserName;
                }
            }
        })
        .error(function (data, status, headers, config) {
        });
    }

    function getLoginUsers() {
        var d = $q.defer();
        $http.get(rootDir + '/TaskTracker/GetAllUsers').
             success(function (data, status, headers, config) {
                 $scope.LoginUsers = data;
                 d.resolve(data);
             }).
              error(function (data, status, headers, config) {
                  d.reject();
              });
        return d.promise;
    }

    function getCurrentCdUser() {
        var defer = $q.defer();
        $http.get(rootDir + '/Dashboard/GetMyNotification').
        success(function (data, status, headers, config) {
            currentcduserdata = data;
            for (var i = 0; i < $scope.LoginUsers.length; i++) {
                if ($scope.LoginUsers[i].Id == currentcduserdata.cdUser.AuthenicateUserId) {
                    ownerName = $scope.LoginUsers[i].UserName;
                }
            }
            defer.resolve(data);
        })
        .error(function (data, status, headers, config) {
            defer.reject();
        });
        return defer.promise;
    }

    $scope.selectAll = function () {
        if ($('#checkboxAll').prop("checked") == true) {
            for (var i = 0; i < $rootScope.filtered.length; i++) {
                try {
                    if ($rootScope.searchDataVal.filter(function (names) { return names.CDuserId == $rootScope.filtered[i].CDuserId })[0] != undefined) {
                        $rootScope.searchDataVal.filter(function (names) { return names.CDuserId == $rootScope.filtered[i].CDuserId })[0].isChecked = true;
                    }
                    if ($rootScope.UserDataResult.filter(function (names) { return names.CDuserId == $rootScope.filtered[i].CDuserId })[0] != undefined) {
                        $rootScope.UserDataResult.filter(function (names) { return names.CDuserId == $rootScope.filtered[i].CDuserId })[0].isChecked = true;
                    }
                } catch (e) {
                    continue;
                }
                $rootScope.filtered[i].isChecked = true;
                $rootScope.tempObject.EmailObj.push($rootScope.filtered[i]);
                //$rootScope.tempSelectedObject.EmailObj = angular.copy($rootScope.tempObject.EmailObj);
            }
            toaster.pop('Success', ($rootScope.filtered.length == 0 ? " " : $rootScope.filtered.length) + " " + 'Selected');
            $rootScope.tempObject.EmailObj = $rootScope.tempObject.EmailObj.unique();
            $rootScope.tempSelectedObject.EmailObj = angular.copy($rootScope.tempObject.EmailObj);
        }
        else {
            for (var i = 0; i < $rootScope.filtered.length; i++) {
                try {
                    if ($rootScope.searchDataVal.filter(function (names) { return names.CDuserId == $rootScope.filtered[i].CDuserId })[0] != undefined) {
                        $rootScope.searchDataVal.filter(function (names) { return names.CDuserId == $rootScope.filtered[i].CDuserId })[0].isChecked = false;
                    }
                    if ($rootScope.UserDataResult.filter(function (names) { return names.CDuserId == $rootScope.filtered[i].CDuserId })[0] != undefined) {
                        $rootScope.UserDataResult.filter(function (names) { return names.CDuserId == $rootScope.filtered[i].CDuserId })[0].isChecked = false;
                    }
                } catch (e) {
                    continue;
                }
                $rootScope.filtered[i].isChecked = false;
                //$rootScope.tempObject.EmailObj.splice($rootScope.tempObject.EmailObj.indexOf($rootScope.filtered[i]), 1);
                $rootScope.tempObject.EmailObj.splice($rootScope.tempObject.EmailObj.indexOf($rootScope.tempObject.EmailObj.filter(function (tasks) { return tasks.CDuserId == $rootScope.filtered[i].CDuserId })[0]), 1);
                //$rootScope.tempObject.EmailObj.splice($rootScope.tempObject.EmailObj.indexOf(jQuery.grep($rootScope.tempObject.EmailObj, function (ele) { return ele.CDuserId == $rootScope.filtered[i].CDuserId })[0]), 1);
                $rootScope.tempSelectedObject.EmailObj = angular.copy($rootScope.tempObject.EmailObj);
            }
            toaster.pop('Success', "", $rootScope.filtered.length + " " + 'Deselected');
        }
        $scope.separateData($rootScope.tempSelectedObject.EmailObj);
        $rootScope.groupMailList = angular.copy($rootScope.filtered);
        $scope.t = {
            sort: {},
            search: {},
            pagination: {
                start: 0
            }
        };
        ctrl.callServer($scope.t);
    }

    Array.prototype.unique = function () {
        var r = new Array();
        o: for (var i = 0, n = this.length; i < n; i++) {
            for (var x = 0, y = r.length; x < y; x++) {
                if (r[x].CDuserId == this[i].CDuserId) {
                    continue o;  //Avoiding Duplicate
                }
            }
            r[r.length] = this[i];
        }
        return r;
    }

    $scope.getSpecificDataForView = function (type) {
        $scope.viewSpecificData(type);
        if (type == 'All') {
            $rootScope.groupMailList = angular.copy($rootScope.tempObject.GroupMailUserDetails);
            $scope.columnStatus = $filter('Status')('All');
        }
        else if (type == 'Provider') {
            $rootScope.groupMailList = angular.copy($rootScope.ProviderData);
            $scope.columnStatus = $filter('Status')('Provider');
        }
        else if (type == 'Users') {
            $rootScope.groupMailList = angular.copy($rootScope.UsersData);
            $scope.columnStatus = $filter('Status')('User');
        }
        else if (type == 'Others') {
            $rootScope.groupMailList = angular.copy($rootScope.otherUsersData);
            $scope.columnStatus = $filter('Status')('Others');
        }
        var scope = angular.element(document.getElementById('paginationId')).scope();

        $scope.t = {
            sort: {},
            search: {},
            pagination: {
                start: 0
            }
        };
        ctrl.callServer($scope.t);
        scope.selectPage(1);
    }

    $scope.viewSpecificData = function (name) {
        switch (name) {
            case "All":
                $rootScope.tempSelectedObject.EmailObj = angular.copy($rootScope.tempObject.EmailObj);
                $rootScope.Alldatascope = true;
                $scope.SelectedMembersColumnStatus = $filter('Status')('All');
                break;
            case "Provider":
                $rootScope.tempSelectedObject.EmailObj = angular.copy($rootScope.ProviderData);
                $rootScope.Alldatascope = false;
                $scope.SelectedMembersColumnStatus = $filter('Status')('Provider');
                break;
            case "Users":
                $rootScope.tempSelectedObject.EmailObj = [];
                $rootScope.Alldatascope = false;
                $rootScope.tempSelectedObject.EmailObj = angular.copy($rootScope.UsersData);
                $scope.SelectedMembersColumnStatus = $filter('Status')('User');
                break;
            case "Others":
                $rootScope.tempSelectedObject.EmailObj = [];
                $rootScope.Alldatascope = false;
                $rootScope.tempSelectedObject.EmailObj = angular.copy($rootScope.otherUsersData);
                $scope.SelectedMembersColumnStatus = $filter('Status')('Others');
                break;
        }
    }

    $scope.removeGroup = function () {
        $http.post(rootDir + '/EmailService/InactivateEmailGroup?EmailGroupId=' + tempId).
          success(function (data, status, headers, config) {
              if (data == "true") {
                  messageAlertEngine.callAlertMessage("successfullySaved", "Group removed successfully", "success", true);
                  //$rootScope.groupMailList.splice($rootScope.groupMailList.indexOf($rootScope.groupMailList.filter(function (items) { return items.EmailGroupId == tempId })[0]), 1);
                  //$rootScope.groupMailList.filter(function (items) { return items.EmailGroupId == tempId })[0].Status = 'Inactive';
                  var closed = $rootScope.activeGroups.splice($rootScope.activeGroups.indexOf($rootScope.activeGroups.filter(function (group) { return group.EmailGroupId == tempId })[0]), 1);
                  closed[0].Status = "Inactive";
                  $rootScope.inactiveGroups.push(closed[0]);
                  $rootScope.groupMailList = angular.copy($rootScope.activeGroups);
                  $scope.t = {
                      sort: {},
                      search: {},
                      pagination: {
                          start: 0
                      }
                  };
                  ctrl.callServer($scope.t);
              }
          }).
          error(function (data, status, headers, config) {
              messageAlertEngine.callAlertMessage("errorInitiated", "Please try after sometime !!!!", "danger", true);
          });
    }
    $scope.ActivateGroup = function () {
        $http.post(rootDir + '/EmailService/ActivateEmailGroup?EmailGroupId=' + tempId).
          success(function (data, status, headers, config) {
              if (data == "true") {
                  messageAlertEngine.callAlertMessage("successfullySaved", "Group Activated successfully", "success", true);
                  //$rootScope.groupMailList.splice($rootScope.groupMailList.indexOf($rootScope.groupMailList.filter(function (items) { return items.EmailGroupId == tempId })[0]), 1);
                  //$rootScope.groupMailList.filter(function (items) { return items.EmailGroupId == tempId })[0].Status = 'Active';
                  var open = $rootScope.inactiveGroups.splice($rootScope.inactiveGroups.indexOf($rootScope.inactiveGroups.filter(function (group) { return group.EmailGroupId == tempId })[0]), 1);
                  open[0].Status = "Active";
                  $scope.activeGroups.push(open[0]);
                  $rootScope.groupMailList = angular.copy($rootScope.inactiveGroups);
                  $scope.t = {
                      sort: {},
                      search: {},
                      pagination: {
                          start: 0
                      }
                  };
                  ctrl.callServer($scope.t);
              }
          }).
          error(function (data, status, headers, config) {
              messageAlertEngine.callAlertMessage("errorInitiated", "Please try after sometime !!!!", "danger", true);
          });
    }

    //// Helper functions /////

    $scope.CheckGroupNameifExists = function (grpname) {
        var res = jQuery.grep($rootScope.tempGroupMails, function (ele) { return ele.EmailGroupName.toLowerCase() == grpname.toLowerCase(); });
        if (res.length > 0) { messageAlertEngine.callAlertMessage('grperrorMsgDiv', "A Group with this name already exists. Please try a different name...", "danger", true); return true; }
        else { return false };
    }

    $scope.CheckGroupNameifExistsForUpdate = function (grpname) {
        var res = jQuery.grep($rootScope.tempGroupMails, function (ele) { return ele.EmailGroupName.toLowerCase() == grpname.toLowerCase(); });
        //try {
        if (res.length > 0 && grpname == $rootScope.tempEditGroupName) { return false }
        else if (res.length > 0) {
            messageAlertEngine.callAlertMessage('grperrorMsgDiv', "A Group with this name already exists. Please try a different name...", "danger", true); return true;
        };
        //} catch (e) {
        //    throw e;
        //}
        //for (var i = 0; i < res.length;res++){}
        //if (res.length > 0 && res.EmailGroupName == $rootScope.tempObject.EmailGroupName) { return false }
        //else if (res.length > 0) {
        //    messageAlertEngine.callAlertMessage('grperrorMsgDiv', "A Group with this name already exists. Please try a different name...", "danger", true); return true;
        //};
    }

    $scope.CheckIfOtherMemberExists = function (mailId) {
        var res = jQuery.grep($rootScope.UserDataResult, function (ele) { return ele.EmailIds == mailId });
        if (res.length > 0) {
            //messageAlertEngine.callAlertMessage('grperrorMsgDiv', "A member with this email id already exists.", "danger", true); return true;
            $('#otherMemberWarningModal').modal();
            $scope.errMsgForotherEmailID = true;
        };
    }

    $scope.AddMember = function () {
        $scope.errMsgForotherEmailID = false;
        if ($scope.errMsgForotherEmail == false && $scope.errMsgForotherEmailID == false) {
            var obj = {
                EmailIds: $scope.OtherEmail,
                FullName: $scope.OtherPersonName,
                CDuserId: 0
            };
            toaster.pop('Success', "", 'Other Member' + ($scope.OtherPersonName == null ? " " : $scope.OtherPersonName) + " " + "with email id" + " " + $scope.OtherEmail + " " + 'Added');
            $rootScope.tempObject.EmailObj.push(obj);
            $rootScope.tempSelectedObject.EmailObj.push(obj);
            $rootScope.otherUsersData.push(obj);
            $scope.OtherEmail = "";
            $scope.OtherPersonName = "";
        }
    }

    $scope.Remove = function (id) {
        tempId = id;
        $('#inactivateWarningModal').modal();
    }
    $scope.ReActivateGroup = function (id) {
        tempId = id;
        $('#ActivateWarningModal').modal();
    }

    $scope.pushEmail = function (data) {
        var msg = '';
        var status;
        if ((jQuery.grep($rootScope.tempObject.EmailObj, function (ele) { return ele.CDuserId == data.CDuserId })).length > 0) {
            //if ($rootScope.tempObject.EmailObj.indexOf(data) > -1) {
            $rootScope.tempObject.EmailObj.splice($rootScope.tempObject.EmailObj.indexOf(jQuery.grep($rootScope.tempObject.EmailObj, function (ele) { return ele.CDuserId == data.CDuserId })[0]), 1);
            //$rootScope.tempObject.EmailObj.splice($rootScope.tempObject.EmailObj.indexOf(data), 1);
            $scope.isPush = false;
            msg = "Deselected";
            status = false;
            //toaster.pop('Success', "", (data.FullName == null ? " " : data.FullName) + " " + 'Deselected');
        } else {
            $rootScope.tempObject.EmailObj = jQuery.grep($rootScope.tempObject.EmailObj, function (ele) { return ele.CDuserId != data.CDuserId });
            $rootScope.tempObject.EmailObj.push(data);
            $scope.isPush = true;
            msg = "Selected";
            status = true;
        }

        $rootScope.selectedUsersGrid = true;

        if ((data.hasOwnProperty("UserType"))) {
            if (data.UserType == "Provider") {
                if ($rootScope.searchDataVal.filter(function (names) { return names.CDuserId == data.CDuserId })[0] != undefined) {
                    $rootScope.searchDataVal.filter(function (names) { return names.CDuserId == data.CDuserId })[0].isChecked = status;
                }
                toaster.pop('Success', "", 'Provider' + " " + (data.FullName == null ? " " : data.FullName) + " " + msg);
            }
            else if (data.UserType == "User" || data.UserType == "OtherUser") {
                if ($rootScope.UserDataResult.filter(function (names) { return names.CDuserId == data.CDuserId })[0] != undefined) {
                    $rootScope.UserDataResult.filter(function (names) { return names.CDuserId == data.CDuserId })[0].isChecked = status;
                }
                toaster.pop('Success', "", 'User' + " " + (data.FullName == null ? " " : data.FullName) + " " + msg);
            }
        }

        $scope.SelectedMembersColumnStatus = $filter('Status')('All');
        $rootScope.tempSelectedObject.EmailObj = angular.copy($rootScope.tempObject.EmailObj);
        $scope.separateData($rootScope.tempSelectedObject.EmailObj);
    }

    $scope.removeSelected = function (data) {
        $rootScope.tempObject.EmailObj.splice($rootScope.tempObject.EmailObj.indexOf(jQuery.grep($rootScope.tempObject.EmailObj, function (ele) { return ele.CDuserId == data.CDuserId })[0]), 1);
        $rootScope.tempSelectedObject.EmailObj = angular.copy($rootScope.tempObject.EmailObj);
        $scope.separateData($rootScope.tempSelectedObject.EmailObj);

        try {

            if ($rootScope.filtered.filter(function (names) { return names.CDuserId == data.CDuserId })[0] != undefined) {
                $rootScope.filtered.filter(function (names) { return names.CDuserId == data.CDuserId })[0].isChecked = false;
                $scope.t = {
                    sort: {},
                    search: {},
                    pagination: {
                        start: 0
                    }
                };
                ctrl.callServer($scope.t);
            }
            if (data.UserType == "User") {
                if ($rootScope.searchDataVal.filter(function (names) { return names.CDuserId == data.CDuserId })[0] != undefined) {
                    $rootScope.searchDataVal.filter(function (names) { return names.CDuserId == data.CDuserId })[0].isChecked = false;
                }
                $rootScope.tempSelectedObject.EmailObj = angular.copy($rootScope.UsersData);
                toaster.pop('Success', "", 'User' + (data.FullName == null ? " " : data.FullName) + 'Removed');
            }
            else if (data.UserType == "Provider") {
                if ($rootScope.searchDataVal.filter(function (names) { return names.CDuserId == data.CDuserId })[0] != undefined) {
                    $rootScope.searchDataVal.filter(function (names) { return names.CDuserId == data.CDuserId })[0].isChecked = false;
                }
                $rootScope.tempSelectedObject.EmailObj = angular.copy($rootScope.ProviderData);
                toaster.pop('Success', "", 'Provider' + (data.FullName == null ? " " : data.FullName) + 'Removed');
                if ($rootScope.Alldatascope== true) {
                  $scope.viewSpecificData("All");
                 }
            }
            else if (data.UserType == "OtherUser") {
                if ($rootScope.UserDataResult.filter(function (names) { return names.CDuserId == data.CDuserId })[0] != undefined) {
                    $rootScope.UserDataResult.filter(function (names) { return names.CDuserId == data.CDuserId })[0].isChecked = false;
                }
                $rootScope.tempSelectedObject.EmailObj = angular.copy($rootScope.otherUsersData);
                toaster.pop('Success', "", 'Other Member' + " " + (data.FullName == null ? " " : data.FullName) + 'Removed');
                if ($rootScope.Alldatascope== true) {
                   $scope.viewSpecificData("All");
                }
            }
        } catch (e) {

        }
        //$rootScope.tempObject.EmailObj.splice($rootScope.tempObject.EmailObj.indexOf($rootScope.filtered[i]), 1);
        //$rootScope.tempSelectedObject = angular.copy($rootScope.tempObject);

    }

    $scope.$watch("searchBox", function (newV, oldV) {
        if (newV === oldV) {
            return;
        }
        else if (newV == "") {
            if ($rootScope.searchResult.length == 0) {
                $rootScope.searchResult = angular.copy($rootScope.UserDataResult);
            }
            $rootScope.groupMailList = angular.copy($rootScope.searchResult);
            $scope.t = {
                sort: {},
                search: {},
                pagination: {
                    start: 0
                }
            };
            ctrl.callServer($scope.t);
        }
    })

    $scope.separateData = function (data) {
        try {
            $rootScope.ProviderData = [];
            $rootScope.UsersData = [];
            $rootScope.otherUsersData = [];
            for (var i = 0; i < data.length; i++) {
                if (data[i].hasOwnProperty("UserType")) {
                    if (data[i].UserType == "Provider") {
                        $rootScope.ProviderData.push(data[i]);
                    }

                    if (data[i].UserType == "User") {
                        $rootScope.UsersData.push(data[i]);
                    }

                    if (data[i].UserType == "OtherUser") {
                        $rootScope.otherUsersData.push(data[i]);
                    }
                    //if ((data[i].ProviderLevel == null) && (data[i].Roles.length == 0)) {
                    //    $rootScope.otherUsersData.push(data[i]);
                    //}
                }
                else {
                    $rootScope.otherUsersData.push(data[i]);
                }
            }
        } catch (e) {
            return;
        }

    }

    $rootScope.selectedUsersGrid = false;
    $scope.AddOtherMember = function () {
        
        var regx1 = /^[a-z][a-zA-Z0-9_]*(\.[a-zA-Z][a-zA-Z0-9_]*)?@[a-z][a-zA-Z-0-9]*\.[a-z]+(\.[a-z]+)?$/;
        var emailids = $('#otherEmailId').val().split(';');
        for (var i = 0; i < emailids.length; i++) {
            if (emailids[i] != "") {
                if (regx1.test(emailids[i].toLowerCase()) == true) {
                    $scope.errMsgForotherEmail = false;
                    $rootScope.selectedUsersGrid = true;
                }
                else { $scope.errMsgForotherEmail = true; }
            }
        }
        $scope.errMsgForotherEmailID = false;
        $scope.CheckIfOtherMemberExists($scope.OtherEmail);
        if ($scope.errMsgForotherEmail == false && $scope.errMsgForotherEmailID == false) {
            var obj = {
                EmailIds: $scope.OtherEmail,
                FullName: $scope.OtherPersonName,
                CDuserId: 0,
                UserType:"OtherUser"
            };
            toaster.pop('Success', "", 'Other Member' + ($scope.OtherPersonName == null ? " " : $scope.OtherPersonName) + " " + "with email id" + " " + $scope.OtherEmail + " " + 'Added');
            $rootScope.tempObject.EmailObj.push(obj);
            $rootScope.tempSelectedObject.EmailObj.push(obj);
            $rootScope.otherUsersData.push(obj);
            $scope.OtherEmail = "";
            $scope.OtherPersonName = "";
            UserType = "";
        }
    }

    $rootScope.ShowAddView = function () {
        $rootScope.tempSelectedObject = [];
        $rootScope.tempSelectedObject.EmailObj = [];
        $rootScope.tempObject = [];
        $rootScope.tempObject.EmailObj = [];
        $rootScope.ProviderData = [];
        $rootScope.UsersData = [];
        $rootScope.otherUsersData = [];
        $scope.AddEditColumnStatus = $filter('Status')('All');
        $rootScope.visibility = "groupMail";
        $rootScope.searchDataVal = angular.copy(tempSearchDataVal);
        $rootScope.UserDataResult = angular.copy(tempUserSearchDataVal);
        //$scope.init_table($scope.ProviderLevels)
        //$scope.getAllUsersMailIds();
        //$scope.init_table($scope.userData);
        //$scope.getAllUsersMailIds();

        //$scope.new_search();

        $rootScope.groupMailList = [];
        $scope.t = {
            sort: {},
            search: {},
            pagination: {
                start: 0
            }
        };
        ctrl.callServer($scope.t);
    }
    $rootScope.CancelAddView = function () {
        $rootScope.editgrid = false;
        $rootScope.visibility = 'groupMailList';
        $rootScope.tableView = true;
        $rootScope.isView = false;
        $rootScope.isEdit = false;
        if (TabName == 'activeGroups') {
            $rootScope.groupMailList = angular.copy($rootScope.activeGroups);
        }
        else {
            $rootScope.groupMailList = angular.copy($rootScope.inactiveGroups);
        }
        $rootScope.currentCDuserId = $rootScope.groupMailList[0].CurrentCDuserId;
        $scope.t = {
            sort: {},
            search: {},
            pagination: {
                start: 0
            }
        };
        ctrl.callServer($scope.t);
        //$rootScope.mails();
    }

    $rootScope.viewGroup = function (GroupMail) {
        $rootScope.tempObject = angular.copy(GroupMail);
        $rootScope.tempSelectedObject.EmailObj = [];
        $rootScope.tempSelectedObject.EmailObj = angular.copy($rootScope.tempObject.GroupMailUserDetails);
        //$rootScope.MailIds = ["tulasidhar@pratian.com", "bindu@pratian.com", "arya@pratian.com", "sharath@pratian.com", "preeti@pratian.com", "mani@pratian.com"];
        $rootScope.isView = true;
        $rootScope.tableView = false;
        $scope.columnStatus = [];
        $scope.columnStatus = $filter('Status')('All');
        $rootScope.isEdit = false;
        //obj.push($rootScope.tempObject.GroupMailUserDetails);
        $scope.separateData($rootScope.tempObject.GroupMailUserDetails);
        $rootScope.groupMailList = angular.copy($rootScope.tempObject.GroupMailUserDetails);
        $scope.t = {
            sort: {},
            search: {},
            pagination: {
                start: 0
            }
        };
        ctrl.callServer($scope.t);
    }
    $rootScope.EditGroup = function (GroupMail) {
        $rootScope.editgrid = true;
        $rootScope.tempObject = angular.copy(GroupMail);
        $rootScope.tempObject.EmailObj = angular.copy(GroupMail.GroupMailUserDetails)
        $rootScope.tempSelectedObject.EmailObj = angular.copy($rootScope.tempObject.GroupMailUserDetails);
        $rootScope.tempEditGroupName = GroupMail.EmailGroupName;
        $rootScope.groupMailList = [];
        //$scope.new_search();
        
        $rootScope.isView = false;
        $rootScope.tableView = false;
        $rootScope.isEdit = true;
        $scope.addNewMember = true;
        $rootScope.otherUserBox = false;
        $rootScope.users = false;
        $rootScope.searchDataVal = angular.copy(tempSearchDataVal);
        $rootScope.UserDataResult = angular.copy(tempUserSearchDataVal);
        //var result = $scope.getAddedUsers(GroupMail);
        //$rootScope.tempObject.EmailObj = angular.copy(result);
        //$rootScope.tempSelectedObject.EmailObj = angular.copy(result);
        $scope.selectedEmails(GroupMail.GroupMailUserDetails);
        $scope.t = {
            sort: {},
            search: {},
            pagination: {
                start: 0
            }
        };
        ctrl.callServer($scope.t);
    }

    $scope.selectedEmails = function (data) {
        for (var i = 0; i < data.length; i++) {
            if (data[i].UserType == "Provider") {
                $rootScope.searchDataVal.filter(function (val) { return val.CDuserId == data[i].CDuserId })[0].isChecked = true;
            }
            else {
                $rootScope.UserDataResult.filter(function (val) { return val.CDuserId == data[i].CDuserId })[0].isChecked = true;
            }
        }
        $scope.separateData($rootScope.tempObject.GroupMailUserDetails);
    }

    //$scope.getAddedUsers = function (groupMail) {
    //    //var arr = [];
    //    //arr.push(groupMail);
    //    //var filterRes = [];
    //    //for (var i = 0; i < groupMail.EmailObj.length; i++) {
    //    //    if (groupMail.EmailObj[i].hasOwnProperty("CDuserId")) {
    //    //        filterRes.push($rootScope.searchDataVal.filter(function (items) { return items.CDuserId == groupMail.EmailObj[i].CDuserId }));
    //    //    }
    //    //}
    //    //filterRes = Array.prototype.concat.apply([], filterRes);
    //    $scope.separateData(filterRes);
    //    return filterRes;
    //}

    $rootScope.cancelView = function () {
        $rootScope.tableView = true;
        $rootScope.isView = false;
        $rootScope.isEdit = false;
        if (TabName == 'activeGroups') {
            $rootScope.groupMailList = angular.copy($rootScope.activeGroups);
        }
        else {
            $rootScope.groupMailList = angular.copy($rootScope.inactiveGroups);
        }
        $scope.t = {
            sort: {},
            search: {},
            pagination: {
                start: 0
            }
        };
        ctrl.callServer($scope.t);
    }

    //SelectPicker related functions
    $('.searchselectpicker').selectpicker({
        style: 'btn-info',
        //style: 'border:none',
        multiple: true,
        //tickIcon: '',
        size: false
    });

    //$(function () {
    //    $("#team").on("changed.bs.select", function (e, clickedIndex, newValue, oldValue) {
    //        var selectedD = $(this).find('option:eq(' + clickedIndex + ')').text()
    //        $('#log').text('selectedD: ' + selectedD + '  newValue: ' + newValue + ' oldValue: ' + oldValue);
    //    });
    //});

    $(".bs-user").on('click', function (val) {
        //$rootScope.UserDataResult = [];
        $rootScope.users = true;
        $scope.addNewMember = false;
        $('#checkboxAll').prop('checked', false);
        $scope.AddEditColumnStatus = $filter('Status')('ADDEditUser');
        $rootScope.otherUserBox = true;
        $('.bootstrap-select.open').removeClass('open');
    })
    //$(".bs-deselect-all").on('click', function (val) {
    //    $rootScope.UserDataResult = [];
    //    $rootScope.users = false;
    //})
    $(".bs-provider").on('click', function (val) {
        $rootScope.users = false;
        $scope.addNewMember = false;
        $('#checkboxAll').prop('checked', false);
        $scope.AddEditColumnStatus = $filter('Status')('Provider');
        $rootScope.groupMailList = $rootScope.searchDataVal;
        $scope.t = {
            sort: {},
            search: {},
            pagination: {
                start: 0
            }
        };
        $scope.t.pagination.numberOfPages = 0;
        ctrl.callServer($scope.t);
    })


    $(".searchselectpicker").on("change", function (value) {
        var This = $(this);
        var selected = $(this).val();
        $('#checkboxAll').prop('checked', false);
        var scope = angular.element(document.getElementById('paginationId')).scope();
        $scope.addNewMember = false;
        $rootScope.selectedProviderData = [];
        $rootScope.selectedIPAData = [];
        $rootScope.selectedRelationshipData = [];
        $rootScope.searchResult = [];
        $rootScope.otherUserBox = true;
        if (selected != null) {
            $rootScope.users = false;
            $scope.AddEditColumnStatus = $filter('Status')('Provider');
            for (var i = 0; i < selected.length; i++) {
                switch (selected[i].split(" ")[0]) {
                    case "Provider":
                        $rootScope.selectedProviderData.push($filter('filter')($rootScope.searchDataVal, selected[i].split(" ")[1]));
                        $rootScope.selectedProviderData = Array.prototype.concat.apply([], $rootScope.selectedProviderData);
                        break;
                    case "IPA":
                        if ($rootScope.selectedProviderData.length == 0) {
                            $rootScope.selectedIPAData.push($filter('filter')($rootScope.searchDataVal, selected[i].split(" ")[1]));
                        }
                        else {
                            $rootScope.selectedIPAData.push($filter('filter')($rootScope.selectedProviderData, selected[i].split(" ")[1]));
                        }
                        $rootScope.selectedIPAData = Array.prototype.concat.apply([], $rootScope.selectedIPAData);
                        break;
                    case "Relationship":
                        if (($rootScope.selectedProviderData.length == 0) && ($rootScope.selectedIPAData.length == 0)) {
                            $rootScope.selectedRelationshipData.push($filter('filter')($rootScope.searchDataVal, selected[i].split(" ")[1]));
                        }
                        else if ($rootScope.selectedProviderData.length == 0) {
                            $rootScope.selectedRelationshipData.push($filter('filter')($rootScope.selectedIPAData, selected[i].split(" ")[1]));
                        }
                        else if ($rootScope.selectedIPAData.length == 0) {
                            $rootScope.selectedRelationshipData.push($filter('filter')($rootScope.selectedProviderData, selected[i].split(" ")[1]));
                        }
                        else {
                            $rootScope.selectedRelationshipData.push($filter('filter')($rootScope.selectedIPAData, selected[i].split(" ")[1]));
                        }
                        $rootScope.selectedRelationshipData = Array.prototype.concat.apply([], $rootScope.selectedRelationshipData);
                        break;

                }
            }
            if ($rootScope.selectedRelationshipData.length == 0 && $rootScope.selectedIPAData.length == 0 && $rootScope.selectedProviderData.length == 0) {
                $rootScope.searchResult = [];
                $scope.t = {
                    sort: {},
                    search: {},
                    pagination: {
                        start: 0
                    }
                };
                $scope.t.pagination.numberOfPages = 0;
                ctrl.displayed = angular.copy($rootScope.searchResult);
                ctrl.callServer($scope.t);

            }
            if ($rootScope.selectedRelationshipData.length != 0) {
                $rootScope.groupMailList = angular.copy($rootScope.selectedRelationshipData);
                $rootScope.searchResult = angular.copy($rootScope.selectedRelationshipData);
                $scope.t = {
                    sort: {},
                    search: {},
                    pagination: {
                        start: 0
                    }
                };
                $scope.t.pagination.numberOfPages = 0;
                ctrl.displayed = [].concat($rootScope.selectedRelationshipData);
                ctrl.callServer($scope.t);

            }
            else if ($rootScope.selectedIPAData.length != 0) {
                $rootScope.groupMailList = angular.copy($rootScope.selectedIPAData);
                $rootScope.searchResult = angular.copy($rootScope.selectedIPAData);
                $scope.t = {
                    sort: {},
                    search: {},
                    pagination: {
                        start: 0
                    }
                };
                $scope.t.pagination.numberOfPages = 0;
                ctrl.displayed = [].concat($rootScope.selectedIPAData);
                ctrl.callServer($scope.t);
            }
            else if ($rootScope.selectedProviderData.length != 0) {
                $rootScope.groupMailList = angular.copy($rootScope.selectedProviderData);
                $rootScope.searchResult = angular.copy($rootScope.selectedProviderData);
                $scope.t = {
                    sort: {},
                    search: {},
                    pagination: {
                        start: 0
                    }
                };
                $scope.t.pagination.numberOfPages = 0;
                ctrl.displayed = [].concat($rootScope.selectedProviderData);
                ctrl.callServer($scope.t);
            }
        }
        else {
            if (!$rootScope.users) {
                $rootScope.groupMailList = angular.copy($rootScope.searchDataVal);
                $rootScope.searchResult = angular.copy($rootScope.searchDataVal);
                $rootScope.users = false;
                $scope.t = {
                    sort: {},
                    search: {},
                    pagination: {
                        start: 0
                    }
                };
                $scope.t.pagination.numberOfPages = 0;
                ctrl.displayed = [].concat($rootScope.searchDataVal);
                ctrl.callServer($scope.t);
            }
            else {
                $rootScope.users = true;
                $rootScope.groupMailList = angular.copy($rootScope.UserDataResult);
                $scope.t = {
                    sort: {},
                    search: {},
                    pagination: {
                        start: 0
                    }
                };
                $scope.t.pagination.numberOfPages = 0;
                ctrl.displayed = [].concat($rootScope.UserDataResult);
                ctrl.callServer($scope.t);
            }
        }
        scope.$apply(function () {
            scope.selectPage(1);
        });
    });


    var countNew = 1;
    $scope.AddMore = function () {
        if (countNew <= 2) {
            countNew++;
            var temp = '';

            temp = '<div class="row">' +
                        '<div class="col-lg-11" style="margin-left: -2.5%">' +
                                '<div class="input-group">' +
                                    '<span class="input-group-addon" style="width:30%;padding:0px">' +
                                        '<select class="form-control input-sm" data-val="true" id="SearchCriteria" ng-model="searchCriteria">' +
                                            '<option selected value="">All</option>' +
                                            '<option>Provider Level</option>' +
                                            '<option>IPA</option>' +
                                            '<option>Provider Relationship</option>' +
                                        '</select>' +
                                    '</span>' +
                                    '<span><input class="form-control input-sm" name="GlobalSearch" autocomplete="off" ng-model="search" ng-focus="searchCumDropDown()" placeholder="Search" style="width:116%"></span>' +
                                '</div>' +

                        '</div>' +

                    '</div>';
        }

        $("#AddMoreData").append(temp);
    };
});