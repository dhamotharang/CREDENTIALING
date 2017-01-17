
profileApp.controller('DocumentationchecklistController', ['$scope', '$http', '$rootScope', '$timeout', function ($scope, $http, $rootScope, $timeout) {
    $scope.Documents = ["Driver's License", "Social Security Card", "Visa or Passport", "Florida Home Address", "State Medical License(Florida and any additional states)", "Board Certificate", "DEA Certificate", "CDS Certificate", "ACLS/BLS/CPR PALS(Must be current)", "Undergraduate(BS,MS,AS,AA)", "Graduate Medical School", "ECFMG", "Residency(ALL)", "Internship(ALL)", "Fellowship(ALL)", "CME Certificates(Last 24 Months)", "CV Must include current employer(Access Health Care Physicians LLC) all entries must include MM/YYYY-MM/YYYY Start/End", "Work Gap Explanation must include explanation of any gap in employment for a period of 3 months.Must contain MM/YYYY-MM/YYYY Start and End dates", "PPD Results(Last 12 months)", "Last Flu Shot(Last 12 months)", "Hospital Privilege Letters for all current facilities"];
    $scope.dataLoaded = false;
    $rootScope.DocCheckListLoaded = false;

    $rootScope.$on('DocumentationCheckList', function () {
        $scope.sections = [
         {
             name: 'Demographic Documentation Needed(Current clear copies needed)',
             docs: { "Driver's License": false, "Social Security Card": false, "Visa or Passport": false, "Florida Home Address": true },
             length: 5
         },
         {
             name: 'Licensure And Certificates(Current clear copies needed)',
             docs: { "State Medical License(Florida and any additional states)": false, "DEA Certificate": false, "CDS Certificate": false, "Board Certificate": false, "ACLS/BLS/CPR PALS(Must be current)": true },
             length: 6
         },
        {
            name: 'Education Documentation(Current clear copies needed)',
            docs: { "ECFMG": false, "Graduate Medical School": false, "Undergraduate(BS,MS,AS,AA)": false, "Residency(ALL)": false, "Internship(ALL)": false, "Fellowship(ALL)": false, "CME Certificates(Last 24 Months)": false },
            length: 8
        },
         {
             name: 'Work History',
             docs: { "CV Must include current employer(Access Health Care Physicians LLC) all entries must include MM/YYYY-MM/YYYY Start/End": false, "Work Gap Explanation must include explanation of any gap in employment for a period of 3 months.Must contain MM/YYYY-MM/YYYY Start and End dates": false },
             length: 3
         },
         {
             name: 'Hospital Privilege Documentation/Information',
             docs: { "PPD Results(Last 12 months)": true, "Hospital Privilege Letters for all current facilities": false, "Last Flu Shot(Last 12 months)": true },
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
    $rootScope.printDCData = function (title) {
        $rootScope.LoadForDCPdf = true;
        $timeout(function () {
            var divToPrint = document.getElementById("Documentation");
            $('#hiddenPrintDiv1').empty();
            $('#hiddenPrintDiv1').append(divToPrint.innerHTML);
            $scope.eTempObject = {};
            //$('#hiddenPrintDiv1').empty();
            // Creating a window for printing
            var mywindow = window.open('', $('#hiddenPrintDiv1').html(), 'height=800,width=800');
            mywindow.document.write('<center><b style="font-size:large">' + title + '</b></center></br>');
            mywindow.document.write('<html><head><title>' + title + '</title>');
            mywindow.document.write('<link rel="stylesheet" href="/Content/SharedCss/bootstrap.min.css" type="text/css" />');
            mywindow.document.write('<link rel="stylesheet" href="/Content/SharedCss/app.css" type="text/css" />');
            mywindow.document.write('<link rel="stylesheet" href="/Content/SharedFonts/font-awesome-4.1.0/css/font-awesome.min.css" type="text/css" />');
            mywindow.document.write('<style>.ng-hide:not(.ng-hide-animate) {display: none !important;}@page{size: auto;margin-bottom: 5mm;margin-top:7mm;}th{text-align:center;}</style>');
            mywindow.document.write('<style>table { table-layout: fixed; } table th, table td { overflow: hidden; word-wrap: break-word; }</style>');
            mywindow.document.write('</head><body style="background-color:white"><table class="table table-bordered" >');
            mywindow.document.write($('#Documentation').html());
            mywindow.document.write('</table></body></html>');
            mywindow.document.close();
            mywindow.focus();
            setTimeout(function () {
                mywindow.print();
                mywindow.close();
            }, 1000);
            $rootScope.LoadForDCPdf = false;
            return true;
        }, 3000)
    }
    $rootScope.DocCheckListLoaded = true;
}]);