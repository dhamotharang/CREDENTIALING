var EmailTemplateApp = angular.module("EmailTemplateApp", ["ngTable", "wysiwyg.module", 'colorpicker.module']);


EmailTemplateApp.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

    $rootScope.messageDesc = "";
    $rootScope.activeMessageDiv = "";
    $rootScope.messageType = "";

    var animateMessageAlertOff = function () {
        $rootScope.closeAlertMessage();
    };


    this.callAlertMessage = function (calledDiv, msg, msgType, dismissal) { ///messageAlertEngine.callAlertMessage('updateHospitalPrivilege' + IndexValue, "Data Updated Successfully !!!!", "success", true);                            
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
}])



EmailTemplateApp.controller("EmailTemplateController", function ($scope, $http,$filter, messageAlertEngine,ngTableParams) {
    $scope.AddNewEmailTemplate = false;
    $scope.isTrue = false;
    $scope.data = [];
    $scope.loadingAjax = true;
    $scope.GetAllEmailTemplate = function () {
        $http.get(rootDir + '/EmailTemplate/GetAllSaveEmailTemplate').
        success(function (data, status, headers, config) {
            try {
                for (var c = 0; c < data.length; c++) {
                    data[c].EditStatus = false;
                }
                $scope.data = angular.copy(data);
                $scope.init_table($scope.data);
                $scope.loadingAjax = false;
            } catch (e) {
             
            }
        }).
        error(function (data, status, headers, config) {
            messageAlertEngine.callAlertMessage('EmailTemplateMessage', "Please Try again later ....", "danger", true);
        });
    }
    $scope.GetAllEmailTemplate();
    $scope.AddOrSaveEmailTemplate = function (Form_Id) {
        $scope.tempObject = [];
        for (var c = 0; c < $scope.data.length; c++) {
            $scope.data[c].EditStatus = false;
        }
        $scope.tableParams.reload();
        $scope.AddNewEmailTemplate = true;
        
    }
    $scope.Cancle = function () {
        $scope.AddNewEmailTemplate = !$scope.AddNewEmailTemplate;
    }
    $scope.EditCancle = function (temp) {
        ResetFormForValidation($("#newEmailForm"));
        for (var c = 0; c <$scope.data.length; c++) {
            if ($scope.data[c].EmailTemplateID == temp.EmailTemplateID) {
                $scope.data[c].EditStatus = false;
                $scope.tableParams.reload();
                break;
            }
        }
        
    }
    $scope.AddingEmailTemplate = function (Form_Id,Edit) {
        ResetFormForValidation($("#" + Form_Id));
        if ($("#" + Form_Id).valid()) {

            var ltcharCheck = true;
            var gtcharCheck = true;
            while (gtcharCheck == true || ltcharCheck == true) {
                if ($('#Body').val().indexOf('<') > -1) {
                    $('#Body').val($('#Body').val().replace("<", "&lt;"));
                    ltcharCheck = true;
                }
                else {
                    ltcharCheck = false;
                }
                if ($('#Body').val().indexOf('>') > -1) {
                    $('#Body').val($('#Body').val().replace(">", "&gt;"));
                    gtcharCheck = true;
                }
                else {
                    gtcharCheck = false;
                }
            }            
            
            var $form = ($("#" + Form_Id)[0]);

            $.ajax({
                url: rootDir + '/EmailTemplate/SaveEmailTemplate',
                type: 'POST',
                data: new FormData($form),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    try {
                        if (data.status == "true") {
                            var count = 0;
                            data.emailTemplate.EditStatus = false;
                            for (var c = 0; c < $scope.data.length; c++) {
                                if ($scope.data[c].EmailTemplateID == data.emailTemplate.EmailTemplateID) {
                                    $scope.data[c] = data.emailTemplate;
                                    $scope.data[c].EditStatus = false;
                                    count++;
                                    break;
                                }
                            }
                            if (count == 0) {
                                $scope.data.push(data.emailTemplate);
                            }
                            $scope.tableParams.reload();
                            $scope.AddNewEmailTemplate = false;

                            if (Edit == 'true') {
                                messageAlertEngine.callAlertMessage('EmailTemplateMessage', "Email Template is Successfully Updated !!!", "success", true);
                            }
                            else {
                                messageAlertEngine.callAlertMessage('EmailTemplateMessage', "Email Template is Successfully Added !!!", "success", true);
                            }

                        }
                        else {
                            for (var c = 0; c < $scope.data.length; c++) {
                                if ($scope.data[c].EmailTemplateID == data.emailTemplate.EmailTemplateID) {
                                    $scope.data[c].EditStatus = false;
                                    $scope.tableParams.reload();
                                    break;
                                }
                            }
                            $scope.AddNewEmailTemplate = false;
                            messageAlertEngine.callAlertMessage('EmailTemplateMessage', data.status, "danger", true);
                        }
                    } catch (e) {
                      
                    }
                },
                error: function (data) {
                    $scope.AddNewEmailTemplate = true;
                    messageAlertEngine.callAlertMessage('EmailTemplateMessage', "Please Try again later ....", "danger", true);
                }
            });
        }
    }

    
    $scope.EditEmailTemplate = function (dataEdit) {
        if (dataEdit.EditStatus == false) {
            $scope.tempObject = [];
            for (var c = 0; c < $scope.data.length; c++) {
                $scope.data[c].EditStatus = false;
            }
            $scope.data[$scope.data.indexOf(dataEdit)].EditStatus = true;
            $scope.tableParams.reload();
            $scope.tempObject = angular.copy(dataEdit);
        }
        else {
            $scope.data[$scope.data.indexOf(dataEdit)].EditStatus = false;
            $scope.tableParams.reload();
        }
    }



    $scope.init_table = function (data, condition) {

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
        
        $scope.tableParams = new ngTableParams({
            page: 1,            // show first page
            count: 10,          // count per page
            filter: {
                //name: 'M'       // initial filter
                //FirstName : ''
            },
            sorting: {
                //name: 'asc'     /// initial sorting
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

    };

    //if filter is on
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

    //Get index first in table
    $scope.getIndexFirst = function () {
        try {
            if ($scope.groupBySelected == 'none') {
                return ($scope.tableParams1.$params.page * $scope.tableParams1.$params.count) - ($scope.tableParams1.$params.count - 1);
            }
        }
        catch (e) { }
    }

    ///Get index Last in table
    $scope.getIndexLast = function () {
        try {
            if ($scope.groupBySelected == 'none') {
                return { true: ($scope.data.length), false: ($scope.tableParams1.$params.page * $scope.tableParams1.$params.count) }[(($scope.tableParams1.$params.page * $scope.tableParams1.$params.count) > ($scope.data.length))];
            }
        }
        catch (e) { }
    }




});
function ResetFormForValidation(form) {
    form.removeData('validator');
    form.removeData('unobtrusiveValidation');
    $.validator.unobtrusive.parse(form);
};