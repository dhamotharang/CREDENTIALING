profileApp.controller('DocumentationchecklistController', ['$scope', '$http', '$rootScope', '$timeout', function ($scope, $http, $rootScope, $timeout) {
    $scope.Documents = ["Driver's License", "Social Security Card", "Visa or Passport", "Florida Home Address", "State Medical License(Florida and any additional states)", "Board Certificate", "DEA Certificate", "CDS Certificate", "ACLS/BLS/CPR PALS(Must be current)", "Undergraduate(BS,MS,AS,AA)", "Graduate Medical School", "ECFMG", "Residency(ALL)", "Internship(ALL)", "Fellowship(ALL)", "CME Certificates(Last 24 Months)", "CV Must include current employer(Access Health Care Physicians LLC) all entries must include MM/YYYY-MM/YYYY Start/End", "Work Gap Explanation must include explanation of any gap in employment for a period of 3 months.Must contain MM/YYYY-MM/YYYY Start and End dates", "PPD Results(Last 12 months)", "Last Flu Shot(Last 12 months)", "Hospital Privilege Letters for all current facilities"];
    $scope.dataLoaded = false;
    $rootScope.DocCheckListLoaded = false;
    $scope.$watch("sections", function (newValue, oldValue) {
        if (newValue === oldValue) {
            return;
        }
        else {
            //alert();
        }
    }, true)
    $rootScope.$on('DocumentationCheckList', function () {
        $scope.loadData = false;
        $scope.sections = [
         {
             name: 'Demographic Documentation Needed(Current clear copies needed)',
             docs: [{ title: "Driver's License", value: true }, { title: "Florida Home Address", value: false }, { title: "Social Security Card", value: true }, { title: "Visa or Passport", value: true }],
             length: 5
         },
         {
             name: 'Licensure And Certificates(Current clear copies needed)',
             docs: [{ title: "ACLS/BLS/CPR PALS(Must be current)", value: false }, { title: "Board Certificate", value: true }, { title: "CDS Certificate", value: true }, { title: "DEA Certificate", value: true }, { title: "State Medical License(Florida and any additional states)", value: true }],
             length: 6
         },
{
    name: 'Education Documentation(Current clear copies needed)',
    docs: [{ title: "CME Certificates(Last 24 Months)", value: true }, { title: "ECFMG", value: true }, { title: "Fellowship(ALL)", value: true }, { title: "Graduate Medical School", value: true }, { title: "Internship(ALL)", value: true }, { title: "Residency(ALL)", value: true }, { title: "Undergraduate(BS,MS,AS,AA)", value: true }],
    length: 8
},
{
    name: 'Work History',
    docs: [{ title: "CV Must include current employer(Access Health Care Physicians LLC) all entries must include MM/YYYY-MM/YYYY Start/End", value: true }, { title: "Work Gap Explanation must include explanation of any gap in employment for a period of 3 months.Must contain MM/YYYY-MM/YYYY Start and End dates", value: true }],
    length: 3
},
{
    name: 'Hospital Privilege Documentation/Information',
    docs: [{ title: "Hospital Privilege Letters for all current facilities", value: true }, { title: "Last Flu Shot(Last 12 months)", value: false }, { title: "PPD Results(Last 12 months)", value: false }],
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
                               for (var l = 0; l < $scope.sections[k].docs.length; l++) {
                                   if ($scope.sections[k].docs[l].title == title) {
                                       $scope.sections[k].docs[l].value = false;
                                   }
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

    //$rootScope.printDCData = function (title) {
    //    var content = document.getElementById('Documentation').innerHTML;
    //    var win = window.open();
    //    win.document.write(content);
    //    win.print();    // JavaScript Print Function
    //    //win.close();    //It will close window after Print.
    //}

    $rootScope.printDCData = function (title) {
        $rootScope.LoadForDCPdf = true;
        $scope.loadData = true;
        $timeout(function () {
            //var divToPrint = document.getElementById("Documentation");
            //$('#hiddenPrintDiv1').empty();
            //$('#hiddenPrintDiv1').append(divToPrint.innerHTML);
            $scope.eTempObject = {};
            //$('#hiddenPrintDiv1').empty();
            // Creating a window for printing

            var mywindow = window.open('', document.getElementById("hiddenPrintDiv1").innerHTML, 'height=800,width=800');

            mywindow.document.write('<center><b style="font-size:large">' + title + '</b></center></br>');
            mywindow.document.write('<html><head><title>' + title + '</title>');
            mywindow.document.write('<link rel="stylesheet" href="/Content/SharedCss/bootstrap.min.css" type="text/css" />');
            mywindow.document.write('<link rel="stylesheet" href="/Content/SharedCss/app.css" type="text/css" />');
            mywindow.document.write('<link rel="stylesheet" href="/Content/SharedFonts/font-awesome-4.1.0/css/font-awesome.min.css" type="text/css" />');
            mywindow.document.write('<style>.ng-hide:not(.ng-hide-animate) {display: none !important;}@page{size: auto;margin-bottom: 5mm;margin-top:7mm;}th{text-align:center;}</style>');
            mywindow.document.write('<style>table { table-layout: fixed; } table th, table td { overflow: hidden; word-wrap: break-word; }</style>');
            mywindow.document.write('</head><body style="background-color:white"><table class="table table-bordered" >');
            mywindow.document.write($('#hiddenPrintDiv1').html());
            mywindow.document.write('</table></body></html>');
            $scope.loadData = false;
            mywindow.document.close();
            mywindow.focus();
            setTimeout(function () {
                mywindow.print();
                mywindow.close();
            },1000);
            $rootScope.LoadForDCPdf = false;
            $scope.loadData = false;
            return true;
        },3000)
    }
    $rootScope.DocCheckListLoaded = true;
}]);
