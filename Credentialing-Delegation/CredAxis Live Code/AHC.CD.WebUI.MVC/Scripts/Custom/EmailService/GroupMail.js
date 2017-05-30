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
    return function(input, predicate) {
        searchValue = predicate['$'];
        //console.log(searchValue);
        var customPredicate = function(value, index, array) {
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

EmailServiceApp.directive('pageSelect', function () {
    return {
        restrict: 'E',
        template: '<input type="text" class="select-page" ng-model="inputPage" ng-change="PageResize(inputPage,numPages)">',
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
EmailServiceApp.controller("GroupEmailController", function ($rootScope, $timeout, $scope, $http, $filter, $q, Resource1, messageAlertEngine, ngTableParams) {
    $('#searchIPA,#searchProviderLevel,#searchLastNameText,#searchProviderType').hide();
    $('.searchName').click(function () { $('#searchIPA,#searchProviderLevel,#searchLastNameText,#searchProviderType').hide("slow"); $('#searchNameText').animate({ width: 'toggle' }); });
    $('.searchLastName').click(function () { $('#searchIPA,#searchProviderLevel,#searchProviderType,#searchNameText').hide("slow"); $('#searchLastNameText').animate({ width: 'toggle' }); });
    $('.searchIPA').click(function () { $('#searchNameText,#searchProviderLevel,#searchLastNameText,#searchProviderType').hide("slow"); $('#searchIPA').animate({ width: 'toggle' }); });
    $('.searchProviderLevel').click(function () { $('#searchNameText,#searchIPA,#searchLastNameText,#searchProviderType').hide("slow"); $('#searchProviderLevel').animate({ width: 'toggle' }); });
    $('.searchProviderType').click(function () { $('#searchNameText,#searchIPA,#searchLastNameText,#searchProviderLevel').hide("slow"); $('#searchProviderType').animate({ width: 'toggle' }); });
    ///// declarations /////
    $rootScope.tempObject = [];
    $rootScope.tempObject.EmailObj = [];
    $scope.tempGroupMails = [];
    $scope.grpNameErrorMessage = false;
    $scope.descriptionErrorMessage = false;
    var tempId = '';
    $scope.allUsers = false;
    $scope.data = [];
    $scope.OtherEmail = "";
    $rootScope.currentCDuserId = "";
    $rootScope.tempEditGroupName = '';

    $rootScope.mails = function () {
        var promise = $scope.GetAllGroupMails().then(function () {
            $scope.t = {
                sort: {},
                search: {},
                pagination: {
                    start: 0
                }
            };
            ctrl.callServer($scope.t);
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
            ctrl.displayed = result.data;
            tableState.pagination.numberOfPages = result.numberOfPages;//set the number of pages so the pagination can update
            ctrl.isLoading = false;
        });
    };

    ctrl.fetchData = function (val) {
        //console.log(val);
        $http.get('data' + val + '.json').success(function (data) {
            ctrl.rowList = data;
            ctrl.displayed = [].concat(ctrl.rowList);
        });
    };

    //// Http calls //////////
    $scope.GetAllGroupMails = function () {
        var d = new $.Deferred();
        $http.get(rootDir + '/EmailService/GetAllGroupMails').
     success(function (data, status, headers, config) {
         $rootScope.groupMailList = data;
         for (var i = 0; i < $rootScope.groupMailList.length; i++) {
             $rootScope.groupMailList[i].EmailObj = [];
             for (var Key in $rootScope.groupMailList[i].Emails) {
                 $rootScope.groupMailList[i].EmailObj.push({ CDuserId: Key, EmailIds: $rootScope.groupMailList[i].Emails[Key], isChecked: true });
                 $rootScope.groupMailList[i].currentCDuserId = $rootScope.groupMailList[i].CurrentCDuserId;
             }
         }
         $rootScope.tempGroupMails = angular.copy($rootScope.groupMailList);
         d.resolve(data);
     }).
     error(function (data, status, headers, config) {
         return d.promise();
     });
        return d.promise();
    }
    $http.get(rootDir + '/MasterData/Organization/GetGroups').
      success(function (data, status, headers, config) {
          $scope.PracticingGroups = data;
      }).
      error(function (data, status, headers, config) {
      });

    $http.get(rootDir + '/Profile/MasterData/GetAllProviderLevels').
      success(function (data, status, headers, config) {
          $scope.ProviderLevels = data;
      }).
      error(function (data, status, headers, config) {
      });
    $scope.UpdateGroup = function () {
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
                dictionary[$rootScope.tempObject.EmailObj[i].EmailIds] = $rootScope.tempObject.EmailObj[i].EmailIds;
            }
            else { dictionary[$rootScope.tempObject.EmailObj[i].CDuserId] = $rootScope.tempObject.EmailObj[i].EmailIds; }
            //dictionary[$rootScope.tempObject.EmailObj[i].CDuserId] = $rootScope.tempObject.EmailObj[i].EmailIds = $rootScope.tempObject.EmailObj[i].EmailIds == null ? "" : $rootScope.tempObject.EmailObj[i].EmailIds;
        }
        var obj = {
            EmailGroupName: $rootScope.tempObject.EmailGroupName,
            LastUpdatedBy: $scope.LastUpdatedBy,
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
                           var filteredgrpmail = jQuery.grep($rootScope.groupMailList, function (ele) { return ele.EmailGroupId == $rootScope.tempObject.EmailGroupId });
                           $rootScope.groupMailList.splice($rootScope.groupMailList.indexOf(filteredgrpmail[0]), 1);
                           filteredgrpmail[0].EmailGroupName = $rootScope.tempObject.EmailGroupName;
                           filteredgrpmail[0].Description = $rootScope.tempObject.Description;
                           filteredgrpmail[0].EmailObj = [];
                           for (var Key in data.result) {
                               filteredgrpmail[0].EmailObj.push({ CDuserId: Key, EmailIds: data.result[Key], isChecked: true });
                           }
                           $rootScope.groupMailList.push(filteredgrpmail[0]);
                           ctrl.temp = $rootScope.groupMailList;
                           messageAlertEngine.callAlertMessage('grpsuccessMsgDiv', "Group updated successfully.", "success", true);
                           $rootScope.visibility = 'groupMailList';
                           $rootScope.tableView = true;
                           $rootScope.isView = false;
                           $rootScope.isEdit = false;
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
                dictionary[$rootScope.tempObject.EmailObj[i].EmailIds] = $rootScope.tempObject.EmailObj[i].EmailIds;
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
                               Status: 'Active',
                               currentCDuserId: data.obj.CreatedBy.CDUserID
                           }
                           for (var Key in data.obj.Emails) {
                               newObj.EmailObj.push({ CDuserId: Key, EmailIds: data.obj.Emails[Key], isChecked: true });
                           }
                           $('html, body').animate({ scrollTop: 0 }, 800);
                           $rootScope.groupMailList.push(newObj);
                           ctrl.temp = $rootScope.groupMailList;
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
    $scope.new_search = function () {
        $scope.data = [];
        $scope.error_message = "";
        $scope.showLoading = true;
        $http({
            method: "POST",
            url: rootDir + "    /SearchProvider/SearchUser",
            data: {
                NPINumber: null, FirstName: "a",
                LastName: null, ProviderRelationship: null, IPAGroupName: $('#searchIPA').val(),
                ProviderLevel: $('#searchProviderLevel').val(), ProviderType: null
            }
        }).success(function (resultData) {
            try {
                if (resultData.searchResults.length != 0) {
                    $scope.data = resultData.searchResults;
                    for (var i in $scope.data) {
                        $scope.data[i].FullName = $scope.data[i].FirstName + " " + $scope.data[i].LastName;
                        $scope.data[i].isChecked = false;
                        $scope.data[i].id = i;
                    }
                    $scope.init_table(resultData.searchResults);
                    $scope.searchProvider = "";
                    $scope.showLoading = false;
                }
                else {
                    $scope.showLoading = false;
                    $scope.data = "";
                }
            } catch (e) {
            }
        }).error(function () { $scope.showLoading = false; $scope.data = ""; })
    }
    $scope.getAllUsersMailIds = function () {
        $http.get(rootDir + '/EmailService/GetAllCDusers').
     success(function (data, status, headers, config) {
         $scope.userData = data;
         for (var i in $scope.userData) {
             $scope.userData[i].EmailIds = $scope.userData[i].EmailId;
         }
         //$scope.init_table($scope.userData);
         $rootScope.groupMailList = angular.copy($scope.userData);
         $scope.t = {
             sort: {},
             search: {},
             pagination: {
                 start: 0
             }
         };
         ctrl.callServer($scope.t);
         $scope.allUsers = true;
     }).
     error(function (data, status, headers, config) {
     });
    }

    $scope.selectAll = function () {
        if ($('#select-all').prop("checked") == true) {
            for (var i = 0; i < $scope.data.length; i++) {
                $scope.data[i].isChecked = true;
                $rootScope.tempObject.EmailObj.push($scope.data[i]);
            }
        }
        else {
            for (var i = 0; i < $scope.data.length; i++) {
                $scope.data[i].isChecked = false;
                $rootScope.tempObject.EmailObj.splice($rootScope.tempObject.EmailObj.indexOf($scope.data[i]), 1);
            }

        }
    }

    $scope.removeGroup = function () {
        $http.post(rootDir + '/EmailService/InactivateEmailGroup?EmailGroupId=' + tempId).
          success(function (data, status, headers, config) {
              if (data == "true") {
                  messageAlertEngine.callAlertMessage("successfullySaved", "Group removed successfully", "success", true);
                  //$rootScope.groupMailList.splice($rootScope.groupMailList.indexOf($rootScope.groupMailList.filter(function (items) { return items.EmailGroupId == tempId })[0]), 1);
                  $rootScope.groupMailList.filter(function (items) { return items.EmailGroupId == tempId })[0].Status = 'Inactive';
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
                  $rootScope.groupMailList.filter(function (items) { return items.EmailGroupId == tempId })[0].Status = 'Active';
                  ctrl.callServer($scope.t);
              }
          }).
          error(function (data, status, headers, config) {
              messageAlertEngine.callAlertMessage("errorInitiated", "Please try after sometime !!!!", "danger", true);
          });
    }

    //// Helper functions /////

    $scope.CheckGroupNameifExists = function (grpname) {
        var res = jQuery.grep($rootScope.groupMailList, function (ele) { return ele.EmailGroupName.toLowerCase() == grpname.toLowerCase(); });
        if (res.length > 0) { messageAlertEngine.callAlertMessage('grperrorMsgDiv', "A Group with this name already exists. Please try a different name...", "danger", true); return true; }
        else { return false };
    }

    $scope.CheckGroupNameifExistsForUpdate = function (grpname) {
        var res = jQuery.grep($rootScope.groupMailList, function (ele) { return ele.EmailGroupName.toLowerCase() == grpname.toLowerCase(); });
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
    $scope.Remove = function (id) {
        tempId = id;
        $('#inactivateWarningModal').modal();
    }
    $scope.ReActivateGroup = function (id) {
        tempId = id;
        $('#ActivateWarningModal').modal();
    }
    //$scope.ClearValue = function (id1, id2) {
    //    $('#' + id1 + ',#' + id2).val('');
    //}

    $scope.ClearValue = function (id1, id2, id3) {
        $('#' + id1 + ',#' + id2 + ',#' + id3).val('');
        if (id3 != "searchProviderType") {
            $('.searchProviderType').css("border-radius", "0 0 0 0");
            $('#searchProviderType').css("border-radius", "0 .5em .5em 0");
        }
        else {
            $('.searchProviderType').css("border-radius", "0 .5em .5em 0");
        }
    }

    $rootScope.selectedUsersGrid = false;

    $scope.pushEmail = function (data) {
        $rootScope.selectedUsersGrid = true;
        if ($rootScope.tempObject.EmailObj.indexOf(data) > -1) {
            $rootScope.tempObject.EmailObj.splice($rootScope.tempObject.EmailObj.indexOf(data), 1);
        } else {
            $rootScope.tempObject.EmailObj = jQuery.grep($rootScope.tempObject.EmailObj, function (ele) { return ele.CDuserId != data.CDuserId });
            $rootScope.tempObject.EmailObj.push(data);
        }
    }
    $scope.AddOtherEmail = function () {
        var regx1 = /^[a-z][a-zA-Z0-9_]*(\.[a-zA-Z][a-zA-Z0-9_]*)?@[a-z][a-zA-Z-0-9]*\.[a-z]+(\.[a-z]+)?$/;
        var emailids = $('#otherEmailId').val().split(';');
        for (var i = 0; i < emailids.length; i++) {
            if (emailids[i] != "") {
                if (regx1.test(emailids[i].toLowerCase()) == true) {
                    $scope.errMsgForotherEmail = false;
                }
                else { $scope.errMsgForotherEmail = true; }
            }
        }
        if ($scope.errMsgForotherEmail == false) {
            var obj = {
                EmailIds: $scope.OtherEmail,
                CDuserId: 0
            };
            $rootScope.tempObject.EmailObj.push(obj);
            $scope.OtherEmail = "";
        }
    }
    $scope.RemoveEmail = function (data) {
        $rootScope.tempObject.EmailObj.splice($rootScope.tempObject.EmailObj.indexOf(jQuery.grep($rootScope.tempObject.EmailObj, function (ele) { return ele.EmailIds == data.EmailIds })[0]), 1);
        jQuery.grep($scope.data, function (ele) { if (ele.CDuserId == data.CDuserId && ele.EmailIds == data.EmailIds) { ele.isChecked = false; } });
    }
    $rootScope.ShowAddView = function () {
        $rootScope.tempObject.EmailObj = [];
        $rootScope.visibility = "groupMail";
        //$scope.init_table($scope.ProviderLevels)
        $scope.getAllUsersMailIds();
        //$scope.init_table($scope.userData);
        //$scope.new_search();
        
    }
    $rootScope.CancelAddView = function () {
        $rootScope.visibility = 'groupMailList';
        $rootScope.currentCDuserId = $rootScope.groupMailList[0].CurrentCDuserId;
        $rootScope.tableView = true;
        $rootScope.isView = false;
        $rootScope.isEdit = false;
        $rootScope.groupMailList = angular.copy($rootScope.tempGroupMails);
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
    $scope.init_table = function (data) {
        $scope.data = data;
        var counts = [];

        if ($scope.data.length <= 10) {
            counts = [];
        }
        else if ($scope.data.length <= 25) {
            counts = [10, 25];
        }
        else if ($scope.data.length <= 50) {
            counts = [10, 25, 50];
        }
        else if ($scope.data.length <= 100) {
            counts = [10, 25, 50, 100];
        }
        else if ($scope.data.length > 100) {
            counts = [10, 25, 50, 100];
        }
        $scope.tableParams1 = new ngTableParams({
            page: 1,            // show first page
            count: 5,          // count per page
            filter: {
                //name: 'M'       // initial filter
                //FirstName : ''
            },
            sorting: {
                //name: 'asc'     // initial sorting
            }
        }, {
            counts: counts,
            total: $scope.data.length, // length of data
            getData: function ($defer, params) {
                // use build-in angular filter
                var filteredData = params.filter() ?
                        $filter('filter')($scope.data, params.filter()) :
                        $scope.data;
                var orderedData = params.sorting() ?
                        $filter('orderBy')(filteredData, params.orderBy()) :
                        $scope.data;

                params.total(orderedData.length); // set total for recalc pagination
                $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
            }
        });

    }
    $scope.ifFilter = function () {
        try {
            var bar;
            var obj = $scope.tableParams1.$params.filter;
            for (bar in obj) {
                if (obj[bar] != "") {
                    return false;
                }
            }
            return true;
        }
        catch (e) { return true; }

    }
    $scope.getIndexFirst = function () {
        try {
            if ($scope.groupBySelected == 'none') {
                return ($scope.tableParams1.$params.page * $scope.tableParams1.$params.count) - ($scope.tableParams1.$params.count - 1);
            }
        }
        catch (e) { }
    }
    $scope.getIndexLast = function () {
        try {
            if ($scope.groupBySelected == 'none') {
                return { true: ($scope.data.length), false: ($scope.tableParams1.$params.page * $scope.tableParams1.$params.count) }[(($scope.tableParams1.$params.page * $scope.tableParams1.$params.count) > ($scope.data.length))];
            }
        }
        catch (e) { }
    }
    $rootScope.viewGroup = function (GroupMail) {
        $rootScope.tempObject = angular.copy(GroupMail);
        //$rootScope.MailIds = ["tulasidhar@pratian.com", "bindu@pratian.com", "arya@pratian.com", "sharath@pratian.com", "preeti@pratian.com", "mani@pratian.com"];
        $rootScope.isView = true;
        $rootScope.tableView = false;
        $rootScope.isEdit = false;
    }
    $rootScope.EditGroup = function (GroupMail) {
        $rootScope.tempObject = angular.copy(GroupMail);
        $rootScope.tempEditGroupName = GroupMail.EmailGroupName;
        $rootScope.isView = false;
        $rootScope.tableView = false;
        $rootScope.isEdit = true;
    }
    $rootScope.cancelView = function () {
        $rootScope.tableView = true;
        $rootScope.isView = false;
        $rootScope.isEdit = false;
    }

    //-------------Prototype----------
    $('.searchselectpicker').selectpicker({
        //style: 'btn-info',
        style: 'border:none',
        multiple: true,
        tickIcon: '',
        size: false,
    });

    //$(function () {
    //    $("#team").on("changed.bs.select", function (e, clickedIndex, newValue, oldValue) {
    //        var selectedD = $(this).find('option:eq(' + clickedIndex + ')').text()
    //        $('#log').text('selectedD: ' + selectedD + '  newValue: ' + newValue + ' oldValue: ' + oldValue);
    //    });
    //});

    //$("select#team").on("change", function (value) {
    //    var This = $(this);
    //    var selectedD = $(this).val();
    //    console.log(selectedD);
    //});


    var countNew = 1;
    $scope.AddMore = function () {
        if (countNew <= 2) {
            countNew++;
            var temp = '';
            //temp = '<div class="row">' +
            //         '   <div class="col-lg-3 form-group zero-padding-left-right-div">' +
            //          '      <select class="form-control input-sm" data-val="true" id="SearchCriteria" ng-model="searchCriteria">' +                      
            //            '        <option>Provider Level</option>' +
            //            '<option>IPA</option>'+
            //                '<option>Provider Relationship</option>'+

            //             '   </select>' +
            //            '</div>' +
            //            '<div class="col-lg-8">' +
            //             '   <div class="right-inner-addon">' +
            //              '      <i class="icon-search"></i>' +
            //               '     <input class="form-control input-sm" name="GlobalSearch" autocomplete="off" ng-model="search" ng-focus="searchCumDropDown()" placeholder="Search">' +
            //               ' </div>' +


            //           ' </div>' +

            //            '<div class="col-lg-1 zero-padding-left-right-div">' +

            //            '</div>' +

            //       ' </div>';

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
//$(document).ready(function () {
//    //$('.selectpicker').selectpicker();
//    $('#selectGroup').hide();
//});