var EmailApp = angular.module("EmailApp", ['smart-table']);
EmailApp.run(function ($rootScope, $http, FollowUpData) {
    $rootScope.emailItems = [];
    $rootScope.sentMail = [];
    $rootScope.outboxMail = [];    
    $rootScope.loadingSentboxAjax = true;
    $rootScope.loadingOutboxAjax = true;
    var tempItems;
    var tempItems1;

    //$rootScope.ConvertMailDateFormat = function (value) {
    //    var returnValue = value;
    //    try {
    //        if (value.indexOf("/Date(") == 0) {
    //            returnValue = new Date(parseInt(value.replace("/Date(", "").replace(")/", ""), 10));
    //            var shortDate = null;
    //            var month = returnValue.getMonth() + 1;
    //            var monthString = month > 9 ? month : '0' + month;
    //            var day = returnValue.getDate();
    //            var dayString = day > 9 ? day : '0' + day;
    //            var year = returnValue.getFullYear();
    //            shortDate = monthString + '/' + dayString + '/' + year;
    //            returnValue = shortDate;
    //        }
    //        return returnValue;
    //    } catch (e) {
    //        return returnValue;
    //    }
    //    return returnValue;
    //};

    //$rootScope.SentEmails = angular.copy(SentEmails);
    tempItems = angular.copy(FollowUpEmails);
    tempItems1 = angular.copy(SentEmails);

    for (var i = 0; i < tempItems.length; i++) {
        if (tempItems[i].NextFollowUpDate != "") {
            tempItems[i].NextFollowUpDate = tempItems[i].NextFollowUpDate.split(' ')[0];
            //$rootScope.ConvertMailDateFormat(tempItems[i].NextFollowUpDate);
        }
        if (tempItems[i].SendingDate != "") {
            tempItems[i].SendingDate = tempItems[i].SendingDate.split(' ')[0];
        }
    }
    for (var i = 0; i < tempItems1.length; i++) {
        if (tempItems1[i].NextFollowUpDate != "") {
            tempItems1[i].NextFollowUpDate = tempItems1[i].NextFollowUpDate.split(' ')[0];
            //$rootScope.ConvertMailDateFormat(tempItems1[i].NextFollowUpDate);
        }
        if (tempItems1[i].SendingDate != "") {
            tempItems1[i].SendingDate = tempItems1[i].SendingDate.split(' ')[0];
        }
    }
    
    $rootScope.FollowUpEmails = angular.copy(tempItems);
    $rootScope.SentEmails = angular.copy(tempItems1);

    $rootScope.getTabData = function (tabName) {
        $rootScope.$broadcast(tabName);
    };

    FollowUpData.AllTemplates().then(function (data) {
        $rootScope.templateData = data;
    })

})