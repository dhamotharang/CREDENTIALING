//EmailApp.service("mailData", function ($http, $q) {
//    return {
//        Allmails: function () {
//            var outbox = "/EmailJson.txt";
//            $q.defer();
//            $http.get(outbox).success(function (data) {
//               $q.defer().resolve(data);
//                //$rootScope.outboxMail1 = data.Outbox;
//                //$rootScope.sentMail1 = data.Sent;
//            }).error(function (ErrorMsg) {
//                $q.defer().reject(ErrorMsg);
//            });
//            return $q.defer().promise;
//        },
//    };
//})
EmailApp.service("FollowUpData", function ($http, $q) {
    return {
        AllTemplates: function () {
            var def = $q.defer();
            $http.get(rootDir + '/EmailService/GetAllEmailTemplates').
       success(function (data) {
           def.resolve(data);
        
       }).error(function (msg) {
           def.reject(msg);
       })
            return def.promise;
        },
    };


})