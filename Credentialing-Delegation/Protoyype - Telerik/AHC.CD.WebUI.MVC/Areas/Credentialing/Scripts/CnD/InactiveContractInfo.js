
ContractInfo.directive('ngMouseWheel', ['$document', function ($document) {
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

ContractInfo.constant("Max_Size", 10);

ContractInfo.directive('stRatio', function () {
    return {
        link: function (scope, element, attr) {
            var ratio = +(attr.stRatio);

            element.css('width', ratio + '%');

        }
    };
});

//ContractInfo.service("GetallProfiles", function ($http, ContractGridID) {
//    return {
//        AllProviders: $http.get(rootDir + '/TaskTracker/GetAllProviders'),
//    };
//})
//ContractInfo.run(function ($rootScope, $q, $http, GetallProfile, GetallContract) {

//    //GetallProfile.AllProviders.then(function (response) {
//    //    $rootScope.ProvidersData = response.data;
//    //    console.log($rootScope.ProvidersData);

//    //},
//    // function (error) {

//    // })
//   // $rootScope.ProvidersData = GetallProfile.AllProviders;

//})
ContractInfo.controller("InactiveContractinfoController", function ($scope, $rootScope, $timeout, $http, Resource1, ngToast) {
    $scope.isView = false;
    $scope.AjaxLoading = false;
    $rootScope.AllData = false;
    $rootScope.notEdit = true;
    $rootScope.randomsItems1 = [];
    $rootScope.tempItems = [];

    $http.get(rootDir + '/Credentialing/CnD/GetAllInactiveContractGridADO')
        .success(function (Data) {
            $scope.tempObject = angular.copy(Data);
            console.log($scope.tempObject);
            
            $rootScope.randomsItems1 = angular.copy($scope.tempObject);
            $rootScope.tempItems = angular.copy($rootScope.randomsItems1);
            for (var i = 0; i < $rootScope.randomsItems1.length; i++) {
                if ($rootScope.randomsItems1[i].TerminationDate != "") {
                    $rootScope.randomsItems1[i].TerminationDate = $rootScope.randomsItems1[i].TerminationDate.split(' ')[0];
                }
                if ($rootScope.randomsItems1[i].InitiatedDate != "") {
                    $rootScope.randomsItems1[i].InitiatedDate = $rootScope.randomsItems1[i].InitiatedDate.split(' ')[0];
                }
            }
            $scope.AjaxLoading = false;
            $scope.isView = false;
            ctrl.callServer($scope.t);
        })
        .error(function (EreorMsg) {

        })
    //ctrl.isLoading = true;

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



    $rootScope.printInactiveInfo = function (title) {
        $rootScope.AllData = true;
        $rootScope.ExcelLoad = true;
        
        //$rootScope.LoadForPdf = true;
        $timeout(function () {
            $('#hiddenPrintDiv1').empty();
            var divToPrint = document.getElementById("InactiveContractInformation1");
            $scope.eTempObject = {};
            $('.hidecontent').attr("style", "display:none");
            $('#hiddenPrintDiv1').empty();
            //$('#hiddenPrintDiv1').append(divToPrint.innerHTML);
            // Removing the last column of the table
            $('#hiddenPrintDiv1 .hideData').remove();
            $('#hiddenPrintDiv1 .filter').remove();


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
            mywindow.document.write($('#InactiveContractInformation1').html());
            mywindow.document.write('</table></body></html>');
            mywindow.document.close();
            mywindow.focus();
            setTimeout(function () {
                mywindow.print();
                mywindow.close();
            }, 2000);
            $('#hideForPdf1').removeAttr("style");
            $('#hideForPdf2').removeAttr("style");
            $('.hidecontent').removeAttr("style");
            $rootScope.AllData = false;
            $rootScope.ExcelLoad = false;
            //$rootScope.LoadForPdf = false;
            return true;
        }, 3000)
    }

    $rootScope.exportToExcel1 = function () {
        $rootScope.ExcelLoad = true;
        $rootScope.AllData = true;
        $timeout(function () {
            var divToPrint = document.getElementById("InactiveContractInformation1");
            $('#hiddenPrintDiv1').empty();
            $('#hiddenPrintDiv1').append("<table>" + divToPrint.innerHTML + "</table>");
            $('#hiddenPrintDiv1 table').attr("id", "exportTable1");
            $('#hiddenPrintDiv1').attr("download", "ExportToExcel.xls");
            $('#exportTable1').tableExport({ type: 'excel', escape: 'false' }, "Contract Grid Details");
            $rootScope.AllData = false;
            $rootScope.ExcelLoad = false;
        }, 3000)

    }
    //$http.get(rootDir + '/TaskTracker/GetAllProviders').success(function (data) {
    //    $rootScope.ProvidersData = data;
    //})

    $scope.watchProfileID = -1;
    $scope.$watch("ProvidersName", function (newValue, oldValue) {
        if (newValue === oldValue) {
            return;
        }
        else {
            if (newValue == "") {
                $rootScope.randomsItems1 = $rootScope.tempItems;
                ctrl.callServer($scope.t);
            }
        }
    });
    $scope.$watch("watchProfileID", function (newValue, oldValue) {
        if (newValue === oldValue) {
            return;
        }
        else {
            $rootScope.randomsItems1 = $rootScope.randomsItems1.filter(function (items) { return items.ProfileID == newValue })
            ctrl.callServer($scope.t);
            ctrl.temp = $rootScope.randomsItems1
        }
    })
    $scope.SelectProvider = function (ProvidersData) {
        $scope.watchProfileID = ProvidersData.ProfileId;
        $scope.ProvidersName = ProvidersData.Name;
        $(".ProviderTypeSelectAutoList1").hide();
    }
    $scope.searchCumDropDown = function (divId) {
        $(".ProviderTypeSelectAutoList").show();
        //$('#alertArea').css("display", "");
        $("#" + divId).show();
    };

    $scope.viewContractGrid = function (ContractGridID) {
        $scope.AjaxLoading = true;
        $rootScope.notEdit = false;
        $http.get(rootDir + '/Credentialing/CnD/GetContractGridById?ContractGridID=' + ContractGridID)
        .success(function (Data) {
            $scope.tempObject = angular.copy(Data);
            console.log($scope.tempObject);
            $scope.AjaxLoading = false;
            $scope.isView = true;
        })
        .error(function (EreorMsg) {

        })
    }
    $scope.cancelView = function () {
        $scope.tempObject = {};
        $scope.isView = false;
        $rootScope.notEdit = true;

    }
    $scope.ReactivateInactiveReport = function () {
        $('#WarningModal1').modal('hide');
        $http.post(rootDir + '/Credentialing/CnD/ReactivateGrid?contractGridID=' + ContractGrid).
            success(function (data, status, headers, config) {
                ngToast.create({
                    className: 'info',
                    content: 'Report Reactivated Successfully.'
                });
                $scope.reactivated;
                $scope.reactivated=$rootScope.randomsItems1.splice($rootScope.randomsItems1.indexOf($rootScope.randomsItems1.filter(function (items) { return items.ContractGridID == ContractGrid })[0]), 1);                                
                //$rootScope.randomsItems1.splice($rootScope.randomsItems1.indexOf($rootScope.randomsItems1.filter(function (items) { return items.ContractGridID == ContractGrid })[0]), 1);
                ctrl.callServer($scope.t);
                $rootScope.randomsItemsTemp.push($scope.reactivated[0]);
                //$rootScope.randomsItems.push($rootScope.randomsItems1.splice($rootScope.randomsItems1.indexOf($rootScope.randomsItems1.filter(function (items) { return items.ContractGridID == ContractGrid })[0]), 1));
                //$rootScope.$broadcast(reactivateGridId);

            }).
        error(function (data, status, headers, config) {

        });
    };
    //$scope.SelectProvider = function (ProviderData) {
    //    $scope.watchProfileID = ProviderData.ProfileId;
    //    $scope.ProvidersName = ProviderData.Name;
    //    $(".ProviderTypeSelectAutoList").hide();
    //}
    var ContractGrid = null
    $scope.reactivateContractGrid = function (remove) {
        
        ContractGrid = angular.copy(remove);
       
        $scope.removedProvider = angular.copy(ContractGrid);
        $('#WarningModal1').modal();
    };


});

ContractInfo.factory('Resource1', ['$q', '$rootScope', '$filter', '$timeout', function ($q, $rootScope, $filter, $timeout) {
    function getPage(start, number, params) {

        var deferred = $q.defer();

        $rootScope.filtered = params.search.predicateObject ? $filter('filter')($rootScope.randomsItems1, params.search.predicateObject) : $rootScope.randomsItems1;

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
$(document).click(function (event) {
    if (!$(event.target).hasClass("form-control") && $(event.target).parents(".ProviderTypeSelectAutoLists").length === 0) {
        $(".ProviderTypeSelectAutoLists").hide();
    }
    if (!$(event.target).hasClass("form-control") && $(event.target).parents(".ProviderTypeSelectAutoList1").length === 0) {
        $(".ProviderTypeSelectAutoList1").hide();
    }
});