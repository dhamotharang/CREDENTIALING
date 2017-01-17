//Contract Information Controller Angular MOdule
//Author Santosh K.

$(document).ready(function () {

    $("#sidemenu").addClass("menu-in");
    $("#page-wrapper").addClass("menuup");
    $(".ProviderTypeSelectAutoList").hide();
    $(".ProviderTypeSelectAutoList1").hide();
});

var ContractInfo = angular.module("Contractinfo", ['ngAnimate', 'ngToast', 'smart-table', 'mgcrea.ngStrap'])
    .config(['ngToastProvider', function (ngToastProvider) {
        ngToastProvider.configure({
            additionalClasses: 'my-animation',
            dismissButton: true,
            dismissButtonHtml: '&times;',
            dismissOnClick: false,
            timeout: 2000
        });
    }
]);

//------------- angular tool tip recall directive ---------------------------
//ContractInfo.directive('tooltip', function () {
//    return function (scope, elem) {
//        elem.tooltip();
//    };
//});
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

ContractInfo.service("GetallContract", function ($http, $q, ContractGridID) {
    return {
        AllContracts: function () {
            var deferred = $q.defer();
            $http.get(rootDir + '/Credentialing/CnD/GetAllContractGridADO').success(function (data) {
                deferred.resolve(data);
            }).error(function (msg, code) {
                deferred.reject(msg);
                $log.error(msg, code);
            });
            return deferred.promise;
        },
    };
})

ContractInfo.service("GetallProfile", function ($http, ContractGridID) {
    return {
        AllProviders: $http.get(rootDir + '/TaskTracker/GetAllProviders'),
    };
})
ContractInfo.run(function ($rootScope, $q, $http, GetallProfile, GetallContract) {
    $rootScope.ExcelLoad = false;
    $rootScope.AllData = false;
    $rootScope.filtered = [];
    $rootScope.Loading = true;
    $rootScope.randomsItems = [];
    $rootScope.randomsItemsTemp = [];
    $rootScope.randomsItems = JSON.parse(contractData);
    $rootScope.randomsItemsTemp = angular.copy($rootScope.randomsItems);
    
    $rootScope.Loading = false;
    //$rootScope.randomsItemsTemp1 = JSON.parse(contractData);
    //GetallContract.AllContracts().then(function (response) {
    //    console.log(response);
    //    $rootScope.randomsItems = angular.copy(response);
    //    $rootScope.randomsItemsTemp = angular.copy(response);
    //    $rootScope.Loading = false;
    //}, function (error) {
    //    console.log(error)
    //})
    GetallProfile.AllProviders.then(function (response) {
        $rootScope.ProviderData = response.data;
        console.log($rootScope.ProviderData);

    },
     function (error) {

     })
})
ContractInfo.constant("Max_Size", 10);
ContractInfo.value("ContractGridID", -1);
ContractInfo.directive('stRatio', function () {
    return {
        link: function (scope, element, attr) {
            var ratio = +(attr.stRatio);

            element.css('width', ratio + '%');

        }
    };
});

ContractInfo.factory('Resource', ['$q', '$rootScope', '$filter', '$timeout', 'GetallContract', function ($q, $rootScope, $filter, $timeout, GetallContract) {

    function getPage(start, number, params) {

        var deferred = $q.defer();

        $rootScope.filtered = params.search.predicateObject ? $filter('filter')($rootScope.randomsItems, params.search.predicateObject) : $rootScope.randomsItems;

        if (params.sort.predicate) {
            $rootScope.filtered = $filter('orderBy')($rootScope.filtered, params.sort.predicate, params.sort.reverse);
        }

        var result = $rootScope.filtered.slice(start, start + number);
        console.log($rootScope.filtered.length);
        $timeout(function () {
            deferred.resolve({
                data: result,
                numberOfPages: Math.ceil($rootScope.filtered.length / number)
            });
        },2000);


        return deferred.promise;
    }

    return {
        getPage: getPage
    };


}]);
ContractInfo.controller("ContractinfoController", function ($scope, $rootScope, $filter, $timeout, $http, ngToast, Resource, GetallContract, GetallProfile, Max_Size, ContractGridID) {
    $scope.isView = false;
    $scope.isEdit = false;
    $scope.tempObject = {};
    $scope.AjaxLoading = false;
    var ctrl = this;

    this.displayed = [];
    var pipecall = 0;
    $rootScope.AllData = false;
    this.callServer = function callServer(tableState) {

        ctrl.isLoading = true;

        var pagination = tableState.pagination;

        console.log(pagination.number);

        var start = pagination.start || 0;
        var number = pagination.number || 5;
        $scope.t = tableState;
        Resource.getPage(start, number, tableState).then(function (result) {
            ctrl.displayed = result.data;
            ctrl.temp = ctrl.displayed;
            tableState.pagination.numberOfPages = result.numberOfPages;//set the number of pages so the pagination can update
            ctrl.isLoading = false;
        });
        
    };
    $scope.exportToExcel = function () {
        $rootScope.ExcelLoad = true;
        $rootScope.AllData = true;
        $timeout(function () {
            var divToPrint = document.getElementById("Contract_ContractInformation1");
            $('#hiddenPrintDiv').empty();
            $('#hiddenPrintDiv').append("<table>" + divToPrint.innerHTML + "</table>");
            $('#hiddenPrintDiv table').attr("id", "exportTable");
            $('#hiddenPrintDiv').attr("download", "ExportToExcel.xls");
            $('#exportTable').tableExport({ type: 'excel', escape: 'false' }, "Contract Grid Details");
            $rootScope.AllData = false;
            $rootScope.ExcelLoad = false;
        }, 3000)
        
    }
    $scope.searchCumDropDown = function (divId) {
        $(".ProviderTypeSelectAutoList1").hide();
        $("#" + divId).show();
    };
    $rootScope.printData = function (title) {
        $rootScope.ExcelLoad = true;
        $rootScope.AllData = true;
        $timeout(function () {
            var divToPrint = document.getElementById("Contract_ContractInformation1");
            $scope.eTempObject = {};
            $('.hidecontent').attr("style", "display:none");
            $('#hiddenPrintDiv').empty();
            //$('#hiddenPrintDiv').append(divToPrint.innerHTML);
            // Removing the last column of the table
            $('#hiddenPrintDiv .hideData').remove();
            $('#hiddenPrintDiv .filter').remove();

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
            mywindow.document.write($('#Contract_ContractInformation1').html());
            mywindow.document.write('</table></body></html>');
            mywindow.document.close();
            mywindow.focus();
            setTimeout(function () {
                mywindow.print();
                mywindow.close();
            }, 1000);
            $('.hidecontent').removeAttr("style");
            $rootScope.AllData = false;
            $rootScope.ExcelLoad = false;
            return true;
        }, 3000)
        

    }
    $scope.watchProfileID = -1;
    $scope.$watch("ProviderName", function (newValue, oldValue) {
        if (newValue === oldValue) {
            return;
        }
        else {
            if (newValue == "") {
                $rootScope.randomsItems = $rootScope.randomsItemsTemp;
                ctrl.callServer($scope.t);
            }
        }
    });
    $scope.$watch("watchProfileID", function (newValue, oldValue) {
        if (newValue === oldValue) {
            return;
        }
        else {
            $rootScope.randomsItems = $rootScope.randomsItemsTemp.filter(function (items) { return items.ProfileID == newValue })
            ctrl.callServer($scope.t);
            ctrl.temp = $rootScope.randomsItems
        }
    })
    $scope.SelectProvider = function (ProviderData) {
        $scope.watchProfileID = ProviderData.ProfileId;
        $scope.ProviderName = ProviderData.Name;
        $(".ProviderTypeSelectAutoList").hide();
    }
    $scope.RemoveReport = function () {
        $('#WarningModal').modal('hide');
        $http.post(rootDir + '/Credentialing/CnD/RemoveGrid?contractGridID=' + ContractGrid).
            success(function (data, status, headers, config) {
                ngToast.create({
                    className: 'danger',
                    content: 'Report Removed Successfully.'
                });
                $rootScope.randomsItems.splice($rootScope.randomsItems.indexOf($rootScope.randomsItems.filter(function (items) { return items.ContractGridID == ContractGrid })[0]), 1);
                ctrl.callServer($scope.t);

            }).
        error(function (data, status, headers, config) {

        });
    };

    $scope.viewContractGrid = function (ContractGridID) {
        $scope.AjaxLoading = true;
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
    }
    $scope.SelectStatus = function (status) {
        $scope.tempObject.Report.ParticipatingStatus = status;
        $(".ProviderTypeSelectAutoList1").hide();
    }
    var IdIndex = -1;
    $scope.EditContractGrid = function (ContractGridID) {
        $scope.AjaxLoading = true;
        IdIndex = $rootScope.randomsItems.indexOf($rootScope.randomsItems.filter(function (items) { return items.ContractGridID == ContractGridID })[0])
        $http.get(rootDir + '/Credentialing/CnD/GetContractGridById?ContractGridID=' + ContractGridID)
        .success(function (Data) {
            $scope.tempObject = angular.copy(Data);
            console.log($scope.tempObject);
            $scope.AjaxLoading = false;
            $scope.isEdit = true;
        })
        .error(function (EreorMsg) {

        })
    }
    $scope.cancelEdit = function () {
        $scope.tempObject = {};
        $scope.isEdit = false;
    }

    $scope.SaveReport = function () {

        var validationStatus = true;
        var url;
        var myData = {};
        var $formData;
        //  if ($scope.Visibility == ('editVisibility' + index)) {
        //Add Details - Denote the URL
        try {
            $formData = $('#PlanReportForm').find('form');
            url = rootDir + "/Credentialing/CnD/SaveReport";
        }
        catch (e)
        { };
        ResetFormForValidation($formData);
        validationStatus = $formData.valid();
        if (validationStatus) {
            $scope.AjaxLoading = true;
            $.ajax({
                url: url,
                type: 'POST',
                data: new FormData($formData[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    try {
                        if (data.status == "true") {
                            ngToast.create({
                                className: 'info',
                                content: 'Report Saved Successfully.'
                            });
                            $rootScope.randomsItems[IdIndex].ParticipatingStatus = data.dataContractGrid.Report.ParticipatingStatus;
                            $rootScope.randomsItems[IdIndex].ProviderID = data.dataContractGrid.Report.ProviderID;
                            $rootScope.randomsItems[IdIndex].GroupID = data.dataContractGrid.Report.GroupID;
                            $rootScope.randomsItems[IdIndex].InitiatedDate = $scope.ConvertDateFormat(data.dataContractGrid.Report.InitiatedDate);
                            $rootScope.randomsItems[IdIndex].TerminationDate = $scope.ConvertDateFormat(data.dataContractGrid.Report.TerminationDate);
                            $scope.AjaxLoading = false;
                            $scope.isEdit = false;
                            ctrl.callServer($scope.t);
                        }
                        else {

                        }
                    }
                    catch (e) {

                    };
                },
                error: function (e) {
                    try {

                    }
                    catch (e) {

                    };
                }
            });
        }
    };














    
    var ContractGrid = null
    $scope.initWarning = function (remove) {
        ContractGrid = angular.copy(remove);
        $('#WarningModal').modal();
    };
  

    $scope.setFiles = function (element) {
        $scope.$apply(function (scope) {
            if (element.files[0]) {
                $scope.tempObject.FileUploadPath = element.files[0];
            } else {
                $scope.tempObject.FileUploadPath = {};
            }
        });
    };



    $http.get(rootDir + '/Credentialing/CnD/GetAllParticipatingStatus').
  success(function (data, status, headers, config) {
      try {
          console.log('Participant Status List');
          console.log(data);
          $scope.ParticipatingStatusList = angular.copy(data);
      } catch (e) {

      }
  }).
  error(function (data, status, headers, config) {

  });

    $scope.ConvertDateFormat = function (value) {

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
                shortDate = monthString + '-' + dayString + '-' + year;
                returnValue = shortDate;
            }
            return returnValue;
        } catch (e) {
            return returnValue;
        }
        return returnValue;
    };


    $rootScope.Loading = false;
});

function setFileNameWith(file) {
    $(file).parent().parent().find(".jancyFileWrapTexts").find("span").width($(file).parent().parent().width() - 210);

};

ContractInfo.config(function ($datepickerProvider) {

    angular.extend($datepickerProvider.defaults, {
        startDate: 'today',
        autoclose: true,
        useNative: true
    });
});

function ResetFormForValidation(form) {
    form.removeData('validator');
    form.removeData('unobtrusiveValidation');
    $.validator.unobtrusive.parse(form);
};

$(document).click(function (event) {
    if (!$(event.target).hasClass("form-control") && $(event.target).parents(".ProviderTypeSelectAutoList").length === 0) {
        $(".ProviderTypeSelectAutoList").hide();
    }
    if (!$(event.target).hasClass("form-control") && $(event.target).parents(".ProviderTypeSelectAutoList1").length === 0) {
        $(".ProviderTypeSelectAutoList1").hide();
    }
});