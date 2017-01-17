profileApp.controller('WorkerCompensationCtrl', function ($scope, $http, dynamicFormGenerateService) {

    $scope.WorkersCompensationInformation = {
        WorkersCompensationNumber: "",
        CertificationStatus: "",
        IssueDate: "",
        ExpirationDate: ""
    };

    $scope.SaveWorkersCompensationInformation = function (workersCompensationInformation) {

        console.log(workersCompensationInformation);

        url = "/Profile/PracticeLocation/UpdateWorkersCompensationInformationAsync?profileId=1";

        $http.post(url, workersCompensationInformation).
         success(function (data, status, headers, config) {

             alert("Success");

             workersCompensationInformation = {};

         }).
         error(function (data, status, headers, config) {
             alert(data);
         });


    };

})