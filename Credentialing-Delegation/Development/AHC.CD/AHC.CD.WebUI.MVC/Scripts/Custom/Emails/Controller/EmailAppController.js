EmailApp.controller("EmailController", function ($scope, $q, $http, $rootScope, $filter, $timeout, Resource) {
    $scope.list = true;
  
    $scope.sentLoad = true;
    $scope.outboxLoad = false;
    //var outbox = "/EmailJson.txt";
    //$http.get(outbox).success(function (data) {
    //    console.log(data);
    //    $rootScope.emailItems = angular.copy(data.Sent);
    //    $rootScope.outboxMail1 = data.Outbox;
    //    $rootScope.sentMail1 = data.Sent;
    //    ctrl.callServer($scope.t);
    //}).error(function (ErrorMsg) {
    //    var err = ErrorMsg;
    //});
    $rootScope.emailItems = $rootScope.SentEmails;

    $rootScope.$on('Sent', function () {
        $scope.list = true;
        $rootScope.loadingSentboxAjax = false;
        $rootScope.emailItems = $rootScope.SentEmails;
        $scope.t.pagination.start = 0;
        ctrl.callServer($scope.t);
        ctrl.temp = $rootScope.emailItems;
        $scope.sentLoad = true;
        $scope.outboxLoad = false;
    });

    $rootScope.$on('outbox', function () {
        $scope.list = true;
        $rootScope.loadingOutboxAjax = false;
        $scope.OutboxList = false;
        
        //for (var i = 0; i < $rootScope.FollowUpEmails.length; i++) {
        //    if ($rootScope.FollowUpEmails[i].NextFollowUpDate != "") {
        //        $rootScope.FollowUpEmails[i].NextFollowUpDate = $scope.ConvertDateFormat($rootScope.FollowUpEmails[i].NextFollowUpDate);
        //    }
        //    if ($rootScope.FollowUpEmails[i].SendingDate != "") {
        //        $rootScope.FollowUpEmails[i].SendingDate = $scope.ConvertDateFormat($rootScope.FollowUpEmails[i].SendingDate);
        //    }
        //}
        $rootScope.emailItems = $rootScope.FollowUpEmails;
        $scope.t.pagination.start = 0;
        ctrl.callServer($scope.t);
        ctrl.temp = $rootScope.emailItems;
        $scope.outboxLoad = true;
        $scope.sentLoad = false;
    })    

    var ctrl = this;
    this.displayed = [];
    var pipecall = 0;
    this.callServer = function callServer(tableState) {
        ctrl.isLoading = true;
        var pagination = tableState.pagination;
        var start = pagination.start || 0;
        var number = pagination.number || 10;
        $scope.t = tableState;
        Resource.getPage(start, number, tableState).then(function (result) {
            ctrl.displayed = result.data;
            ctrl.temp = ctrl.displayed;
            tableState.pagination.numberOfPages = result.numberOfPages;//set the number of pages so the pagination can update
            ctrl.isLoading = false;
        });
    };
    $scope.showCompose = function () {       
        //$("#newEmailForm .field-validation-error").hide();
        $scope.compose = true;
    }
    $scope.templateSelected = true;
    $scope.changeYesNoOption = function (value) {
        $("#newEmailForm .field-validation-error").remove();
        $scope.templateSelected = false;
        $scope.tempObject.Subject = "";
        $scope.tempObject.Body = "";

        if (value == 2) {
            $scope.templateSelected = true;
        } else { $scope.tempObject.Title = ""; }

    }

    $scope.showContent = function () {
        $scope.templateSelected = true;

    }
    $scope.mailDetails = function (mail) {
        $scope.list = false;
        $scope.Details = angular.copy(mail);
    };
    $scope.closeList = function () {
        $scope.list = true;
    }
    $scope.stopMailFollowUP = function (emailInfoID) {
        var obj = $filter('filter')($rootScope.FollowUpEmails, { EmailInfoID: emailInfoID })[0];
        $rootScope.FollowUpEmails.splice($rootScope.FollowUpEmails.indexOf(obj), 1);
        $scope.list = true;
        ctrl.callServer($scope.t);
    };
    $scope.cancelControl = function (temp) {
        var formdata = $('#newEmailForm');
        formdata[0].reset();
        $scope.compose = false;     
    }
    $scope.hideDiv = function () {
        $('.TemplateSelectAutoList').hide();
        $scope.errorMsg = false;
    }
    //$(document).click(function (event) {
    //    if (!$(event.target).hasClass("form-control") && $(event.target).parents(".TemplateSelectAutoList").length === 0) {
    //        $(".TemplateSelectAutoList").hide();
    //    }
    //});
})