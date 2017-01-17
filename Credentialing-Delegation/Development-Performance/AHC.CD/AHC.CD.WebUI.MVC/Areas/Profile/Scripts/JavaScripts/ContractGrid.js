
profileApp.directive('ngMouseWheel', ['$document', function ($document) {
    return function (scope, element, attrs) {
        element.bind("DOMMouseScroll onwheel mousewheel onmousewheel", function (event) {

            // cross-browser wheel delta
            var event = window.event || event; // old IE support
            var delta = Math.max(-1, Math.min(1, (event.wheelDelta || -event.detail)));

            if (delta > 0) {
                scope.$apply(function () {
                    scope.scrollup();
                });
            }
            else {
                scope.$apply(function () {
                    scope.scrolldown();
                });
            }
            // for IE
            event.returnValue = false;
            // for Chrome and Firefox
            if (event.preventDefault) {
                event.preventDefault();
            }
        });
    };
}]);

profileApp.constant("Max_Size", 10);

profileApp.directive('stRatio', function () {
    return {
        link: function (scope, element, attr) {
            var ratio = +(attr.stRatio);

            element.css('width', ratio + '%');

        }
    };
});


profileApp.controller("ContractGridController", ["$scope", "$rootScope", "$timeout", "$http", "Resource1", function ($scope, $rootScope, $timeout, $http, Resource1) {

    $scope.ConvertDate = function (value) {
        var returnValue = value;
        try {
            if (value.indexOf("/Date(") == 0) {
                returnValue = new Date(parseInt(value.replace("/Date(", "").replace(")/", ""), 10));
                var shortDate = null;
                var month = returnValue.getMonth() + 1;
                var monthString = month > 9 ? month : '0' + month;
                var day = returnValue.getDate();
                var dayString = day > 9 ? day : '0' + day;
                var year = returnValue.getFullYear();
                shortDate = monthString + '/' + dayString + '/' + year;
                returnValue = shortDate;
            }
            return returnValue;
        } catch (e) {
            return returnValue;
        }
        return returnValue;
    };
    $rootScope.ContractGridLoaded = true;
    $scope.dataLoaded = false;
    $rootScope.$on('ContractGrid', function () {
        if (!$scope.dataLoaded) {
            $rootScope.ContractGridLoaded = false;
            $rootScope.randomsItems = [];
            var url = rootDir + "/ContractGrid/GetAllContractGridinfoes?profileid=" + profileId;
            $http.get(url).
              success(function (data) {
                  
                  ctrl.isLoading = true;
                  console.log(data);
                  //$scope.showLoading = false;
                  $scope.names = angular.copy(data);
                  for (var i = 0 ; i < $scope.names.length; i++) {
                      $scope.names[i].LOBS = [];
                      if ($scope.names[i].LOB != null) {
                          $scope.names[i].LOBSData = $scope.names[i].LOB.split(",");
                          for (var j in $scope.names[i].LOBSData) {
                              if ($scope.names[i].LOBSData[j] != '') {
                                  $scope.names[i].LOBS.push($scope.names[i].LOBSData[j]);
                              }
                          }
                      }

                      if ($scope.names[i].EffectiveDate != null) {
                          $scope.names[i].EffectiveDate = $scope.ConvertDate($scope.names[i].EffectiveDate);
                      }
                      if ($scope.names[i].TerminationDate != null) {
                          $scope.names[i].TerminationDate = $scope.ConvertDate($scope.names[i].TerminationDate);
                      }
                  }
                  $rootScope.randomsItems = angular.copy($scope.names);
                  ctrl.callServer($scope.t);
                  $rootScope.ContractGridLoaded = true;
              }).
                  error(function (data) {
                      $rootScope.ContractGridLoaded = true;
                      //$scope.names = 'error';
                  });
            ctrl.isLoading = true;
            $scope.dataLoaded = true;
        }
        
    });
    var ctrl = this;
    this.displayed = [];
    $rootScope.AllData = false;
    this.callServer = function callServer(tableState) {
        ctrl.isLoading = true;
        $scope.t = tableState;
        var pagination = tableState.pagination;

        console.log(pagination.number);

        var start = pagination.start || 0;
        var number = pagination.number || 5;

        Resource1.getPage(start, number, tableState).then(function (result) {
            ctrl.displayed = result.data;
            console.log(result.data);
            tableState.pagination.numberOfPages = result.numberOfPages;//set the number of pages so the pagination can update
            ctrl.isLoading = false;
        });
    };

    $rootScope.printCGData = function (title) {
        $rootScope.AllData = true;
        $rootScope.LoadForPdf = true;
        $timeout(function () {
            var divToPrint = document.getElementById("Contract_Grid1");
            $('#hiddenPrintDiv').empty();
           $('#hiddenPrintDiv').append(divToPrint.innerHTML);
            $scope.eTempObject = {};
            $('#hideForPdf1').attr("style", "display:none");
            $('#hideForPdf2').attr("style", "display:none");
            // Removing the last column of the table
            
            // Creating a window for printing
            var mywindow = window.open('', $('#hiddenPrintDiv').html(), 'height=800,width=800');
            mywindow.document.write('<center><b style="font-size:large">' + title + '</b></center></br>');
            mywindow.document.write('<html><head><title>' + title + '</title>');
            mywindow.document.write('<link rel="stylesheet" href="/Content/SharedCss/bootstrap.min.css" type="text/css" />');
            mywindow.document.write('<link rel="stylesheet" href="/Content/SharedCss/app.css" type="text/css" />');
            mywindow.document.write('<link rel="stylesheet" href="/Content/SharedFonts/font-awesome-4.1.0/css/font-awesome.min.css" type="text/css" />');
            mywindow.document.write('<style>.ng-hide:not(.ng-hide-animate) {display: none !important;}@page{size: auto;margin-bottom: 5mm;margin-top:7mm;}th{text-align:center;}</style>');
            mywindow.document.write('<style>table { table-layout: fixed; } table th, table td { overflow: hidden; word-wrap: break-word; }</style>');
            mywindow.document.write('</head><body style="background-color:white"><table class="table table-bordered" >');
            mywindow.document.write($('#Contract_Grid1').html());
            mywindow.document.write('</table></body></html>');
            mywindow.document.close();
            mywindow.focus();
            setTimeout(function () {
                mywindow.print();
                mywindow.close();
            }, 1000);
            $('#hideForPdf1').removeAttr("style");
            $('#hideForPdf2').removeAttr("style");
            $rootScope.AllData = false;
            $rootScope.LoadForPdf = false;
            return true;
        }, 3000)
    }

    $scope.exportToExcel1 = function () {
        $rootScope.LoadForPdf = true;
        $rootScope.AllData = true;
        $timeout(function () {
            var divToPrint = document.getElementById("Contract_Grid1");
            $('#hiddenPrintDiv1').empty();
            $('#hiddenPrintDiv1').append("<table>" + divToPrint.innerHTML + "</table>");
            $('#hiddenPrintDiv1 table').attr("id", "exportTable1");
            $('#hiddenPrintDiv1').attr("download", "ExportToExcel.xls");
            $('#exportTable1').tableExport({ type: 'excel', escape: 'false' }, "Contract Grid Details");
            $rootScope.AllData = false;
            $rootScope.LoadForPdf = false;
        }, 3000)
    }
}]);

profileApp.factory('Resource1', ['$q', '$rootScope', '$filter', '$timeout', function ($q, $rootScope, $filter, $timeout) {
    function getPage(start, number, params) {

        var deferred = $q.defer();

       $rootScope.filtered = params.search.predicateObject ? $filter('filter')($rootScope.randomsItems, params.search.predicateObject) : $rootScope.randomsItems;

        if (params.sort.predicate) {
            $rootScope.filtered = $filter('orderBy')($rootScope.filtered, params.sort.predicate, params.sort.reverse);
        }
        var result = $rootScope.filtered.slice(start, start + number);
        console.log($rootScope.filtered.length);
        console.log($rootScope.filtered);
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