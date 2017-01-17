
profileApp.controller('DocumentationchecklistController', ['$scope', '$http', '$rootScope', function ($scope, $http, $rootScope) {
    $scope.Documents = ['Driving License', 'SSN Certificate', 'Visa', 'Florida Home Address', 'State License', 'Board Specialty', 'DEA License', 'CDSC', 'ALCS', 'Under Graduation', 'Graduation', 'ECFMG Certificate', 'Residency', 'Internship', 'Fellowship', 'CME Certification', 'CV', 'Work Gap', 'PPD Results', 'Last Flu Shot', 'Hospital Privilege'];
    $scope.dataLoaded = false;
    $rootScope.DocCheckListLoaded = false;

    $rootScope.$on('DocumentationCheckList', function () {
        $scope.sections = [
         {
             name: 'Demographics',
             docs: { 'Driving License': false, 'SSN Certificate': false, 'Visa': false, 'Florida Home Address': false },
             length: 5
         },
         {
             name: 'Licensure And Certificates',
             docs: { 'State License': false, 'DEA License': false, 'CDSC': false, "Board Specialty": false, 'ALCS': false },
             length: 6
         },
        {
            name: 'Education Documents',
            docs: { 'ECFMG Certificate': false, 'Graduation': false, 'Under Graduation': false, 'Residency': false, 'Internship': false, 'Fellowship': false, 'CME Certification': false },
            length: 8
        },
         {
             name: 'Work History',
             docs: { 'CV': false, 'Work Gap': false },
             length: 3
         },
         {
             name: 'Hospital Privilege',
             docs: { 'PPD Results': false, 'Hospital Privilege': false, 'Last Flu Shot': false },
             length: 4
         },
        ];
        if (!$scope.dataLoaded) {
            $rootScope.DocCheckListLoaded = false;
                $http.get(rootDir + '/DocumentationCheckList/GetAllProfileDocuments?ProfileID=' + profileId)
               .success(function (data, status, headers, config) {
                   $scope.mydata = data.Result;
                   for (i = 0; i < $scope.Documents.length; i++) {
                       var title = $scope.Documents[i];
                       for (var j = 0; j < $scope.mydata.length; j++) {
                           if (title == $scope.mydata[j].Title) {
                               for (var k = 0; k < $scope.sections.length; k++) {
                                   if ($scope.sections[k].docs.hasOwnProperty(title)) {
                                       $scope.sections[k].docs[title] = true;
                                   }
                               }
                           }
                       }
                   }
                   $rootScope.DocCheckListLoaded = true;
               }).
                error(function (data, status, headers, config) {
                });
            
        }
    });

    $rootScope.DocCheckListLoaded = true;
}]);