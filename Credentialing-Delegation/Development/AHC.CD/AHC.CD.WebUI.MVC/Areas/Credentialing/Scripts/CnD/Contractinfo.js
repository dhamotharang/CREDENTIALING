/// <reference path="../../../../Scripts/Lib/Angular/angular.js" />
/// <reference path="../../../../Scripts/Lib/Angular/angular.js" />
/// <reference path="AddNewContract.js" />
//Contract Information Controller Angular MOdule
//Author Santosh K.

$(document).ready(function () {

    $("#sidemenu").addClass("menu-in");
    $("#page-wrapper").addClass("menuup");
    $(".ProviderTypeSelectAutoList").hide();
    $(".ProviderTypeSelectAutoList1").hide();
    $("body").tooltip({ selector: '[data-toggle=tooltip]' });
    $("#Add_New_Contract").hide();
});

var ContractInfo = angular.module("Contractinfo", ['ngAnimate', 'ngToast', 'ngTable', 'smart-table', 'mgcrea.ngStrap'])
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



ContractInfo.directive('keyBlocker', function ($parse) {
    return {
        restrict: 'A',
        link: function (scope, elm, attrs) {
            elm.bind('keydown keypress', function (e) {

                e.preventDefault();
                return false;
            });
        }
    }
});
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

ContractInfo.value("temporaryData", []);
ContractInfo.run(function ($rootScope, $q, $http, GetallProfile) {
    $rootScope.ExcelLoad = false;
    $rootScope.AllData = false;
    $rootScope.filtered = [];
    $rootScope.Loading = true;
    $rootScope.randomsItems = [];
    $rootScope.randomsItems1 = [];
    $rootScope.randomsItemsTemp = [];
    $rootScope.ProviderNames = "";
    //$rootScope.randomTempItem = [];

    $rootScope.randomsItems = JSON.parse(contractData);

    temporaryData = JSON.parse(contractData)
    for (var i = 0; i < $rootScope.randomsItems.length; i++) {
        if ($rootScope.randomsItems[i].TerminationDate != "") {
            $rootScope.randomsItems[i].TerminationDate = $rootScope.randomsItems[i].TerminationDate.split(' ')[0];
            temporaryData[i].TerminationDate = temporaryData[i].TerminationDate.split(' ')[0];
        }
        temporaryData[i].ProviderName = temporaryData[i].ProviderName.replace("  ", " ");
        $rootScope.randomsItems[i].ProviderName = $rootScope.randomsItems[i].ProviderName.replace("  ", " ");
        if ($rootScope.randomsItems[i].InitiatedDate != "") {
            $rootScope.randomsItems[i].InitiatedDate = $rootScope.randomsItems[i].InitiatedDate.split(' ')[0];
            temporaryData[i].InitiatedDate = temporaryData[i].InitiatedDate.split(' ')[0];
        }
    }
    //for (var j = 0; j < $rootScope.randomsItems.length; j++) {
    //    $rootScope.ProviderNames = $rootScope.randomsItems[j].ProviderFirstName + $rootScope.randomsItems[j].ProviderMiddleName + $rootScope.randomsItems[j].ProviderLastName;
    //    $rootScope.randomsItems[j].push({
    //        ProviderName:$rootScope.ProviderNames
    //    })
    //}
    //temporaryData = angular.copy($rootScope.randomsItems);
    $rootScope.randomsItemsTemp = angular.copy(temporaryData);
    //$rootScope.randomTempItem = angular.copy($rootScope.randomsItems);

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
        console.log("Provider data", $rootScope.ProviderData);

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
        //if ($rootScope.filtered.length > 0) {
        //    for (var i = 0; i < $rootScope.filtered.length; i++) {
        //        $rootScope.filtered[i].ProviderFullName = "";
        //        if ($rootScope.filtered[i].ProviderFirstName)
        //            $rootScope.filtered[i].ProviderFullName = $rootScope.filtered[i].ProviderFullName + $rootScope.filtered[i].ProviderFirstName + " ";
        //        if ($rootScope.filtered[i].ProviderMiddleName)
        //            $rootScope.filtered[i].ProviderFullName = $rootScope.filtered[i].ProviderFullName + $rootScope.filtered[i].ProviderMiddleName + " ";
        //        if ($rootScope.filtered[i].ProviderLastName)
        //            $rootScope.filtered[i].ProviderFullName = $rootScope.filtered[i].ProviderFullName + $rootScope.filtered[i].ProviderLastName;
        //    }
        //}
        if (params.sort.predicate) {
            $rootScope.filtered = $filter('orderBy')($rootScope.filtered, params.sort.predicate, params.sort.reverse);
        }

        var result = $rootScope.filtered.slice(start, start + number);
        //console.log($rootScope.filtered.length);
        $timeout(function () {
            deferred.resolve({
                data: result,
                numberOfPages: Math.ceil($rootScope.filtered.length / number)
            });
        });


        return deferred.promise;
    }

    return {
        getPage: getPage
    };


}]);
ContractInfo.controller("ContractinfoController", function ($scope, $window, $rootScope, $filter, $timeout, $http, ngToast, Resource, GetallContract, GetallProfile, Max_Size, ContractGridID) {
    $scope.AddNewContract = false;
    $scope.isView = false;
    $scope.isEdit = false;
    $rootScope.notEdit = true;
    $scope.tempObject = {};
    $scope.AjaxLoading = false;
    $scope.activeStatus = true;
    $scope.inactiveStatus = false;
    $scope.ProviderNameToPrint = "";
    var initval;
    initval = 0;
    $rootScope.PanelData = false;

    var ctrl = this;

    //$rootScope.$on('reactivateGridId', function () {
    //    ctrl.callServer();
    //})

    this.displayed = [];
    var pipecall = 0;
    $rootScope.AllData = false;
    this.callServer = function callServer(tableState) {

        ctrl.isLoading = true;

        var pagination = tableState.pagination;

        console.log(pagination.number);

        var start = pagination.start || 0;
        var number = pagination.number || 10;
        $scope.t = tableState;
        Resource.getPage(start, number, tableState).then(function (result) {
            ctrl.displayed = result.data;
            //if (result.data.length > 0) {
            //    for (var i = 0; i < result.data.length; i++) {
            //        result.data[i].ProviderFullName = "";
            //        if (result.data[i].ProviderFirstName)
            //            result.data[i].ProviderFullName = result.data[i].ProviderFullName + result.data[i].ProviderFirstName + " ";
            //        if (result.data[i].ProviderMiddleName)
            //            result.data[i].ProviderFullName = result.data[i].ProviderFullName + result.data[i].ProviderMiddleName + " ";
            //        if (result.data[i].ProviderLastName)
            //            result.data[i].ProviderFullName = result.data[i].ProviderFullName + result.data[i].ProviderLastName;
            //    }
            //}
            ctrl.temp = ctrl.displayed;
            tableState.pagination.numberOfPages = result.numberOfPages;//set the number of pages so the pagination can update
            ctrl.isLoading = false;
        });

    };



    $scope.SetRecredentialingDate = function (value) {
        if (value == null) {
            $scope.tempObject.Report.ReCredentialingDate = "";
        }
        else {
            if ($scope.tempObject != null && $scope.tempObject != {}) {
                var RecredentialingDuration = jQuery.grep($scope.tempObject.CredentialingInfo.Plan.PlanLOBs, function (ele) { return ele.LOBID == $scope.tempObject.LOBID })[0].ReCredentialingDuration;
                $scope.tempObject.Report.ReCredentialingDate = $scope.ConvertDateBy3OR5Years(value, RecredentialingDuration != null ? RecredentialingDuration : 3);
            }
        }
    }


    $scope.ConvertDateBy3OR5Years = function (date, RecredentialingDuration) {
        if (date != '' || date != 'null') {
            var dt = new Date(date);
            //var NoOfDays = checkLeapYear(dt, LOBID);
            var month = dt.getMonth() + 1;
            var monthString = month > 9 ? month : '0' + month;
            //var monthName = monthNames[month];
            var day = dt.getDate();
            var dayString = day > 9 ? day : '0' + day;
            var year = 0;
            //if (LOBID == 1 || LOBID == 2) {

            //    year = dt.getFullYear() + 5;
            //}
            //else {
            //    year = dt.getFullYear() + 3;
            //}
            year = dt.getFullYear() + RecredentialingDuration;
            shortDate = monthString + '/' + dayString + '/' + year;
            return shortDate;
        } return null;
    }

    $scope.exportToExcel = function () {
        $rootScope.ExcelLoad = true;
        $rootScope.PanelData = true;
        $rootScope.AllData = true;
        $timeout(function () {
            var divToPrint = document.getElementById("Contract_ContractInformation1");
            $('#hiddenPrintDiv').empty();

            $('#hiddenPrintDiv').append("<table>" + divToPrint.innerHTML + "</table>");
            $('#hiddenPrintDiv table').attr("id", "exportTable");
            $('#hiddenPrintDiv').attr("download", "ExportToExcel.xls");
            if ($scope.ProviderNameToPrint == "") {
                $('#exportTable').tableExport({ type: 'excel', escape: 'false' }, "Active Contract Grid Details");
            }
            else {
                $('#exportTable').tableExport({ type: 'excel', escape: 'false' }, $scope.ProviderNameToPrint + " Active Contract Grid Details", $scope.ProviderNameToPrint);
            }
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
        $rootScope.PanelData = false;
        $timeout(function () {
            //$('#hiddenPrintDiv').empty();
            var divToPrint = document.getElementById("Contract_ContractInformation1");
            $scope.eTempObject = {};
            $('.hidecontent').attr("style", "display:none");
            $('#hiddenPrintDiv').empty();
            //$('#hiddenPrintDiv').append(divToPrint.innerHTML);
            // Removing the last column of the table
            $('#hiddenPrintDiv .hideData').remove();
            $('#hiddenPrintDiv .filter').remove();
            // Creating a window for printing
            var mywindow = $window.open('', $('#hiddenPrintDiv').html(), 'height=800,width=800');
            mywindow.document.write('<center><b style="font-size:large">' + title + '</b></center></br>');
            mywindow.document.write('<html><head><title>' + title + '</title>');
            //mywindow.document.write('<link rel="stylesheet" href="/~/Scripts/Lib/Angular/angular.js" type="text/js" />');
            //mywindow.document.write('<html><head><button class="fa fa-print pull-right" style="width:8%" ng-click="PrintData()"></button>');
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

            $timeout(function () {
                mywindow.print();
                mywindow.close();
            }, 2000);
            //$('#PrintData').click = (function () {
            //    $timeout(function () {
            //        mywindow.print();
            //        mywindow.close();
            //    }, 2000);

            //});

            //$scope.PrintData = function () {
            //    $timeout(function () {
            //        mywindow.print();
            //        mywindow.close();
            //    }, 1000);

            //}

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
                $scope.ProviderNameToPrint = "";
                $rootScope.randomsItems = angular.copy($rootScope.randomsItemsTemp);
                ctrl.callServer($scope.t);
                $scope.watchProfileID = "";

            }
        }
    });
    $scope.$watch("watchProfileID", function (newValue, oldValue) {
        if (newValue === oldValue) {
            return;
        }
        if (newValue == "") {
            $rootScope.randomsItems = angular.copy($rootScope.randomsItemsTemp);
            ctrl.callServer($scope.t);
        }
        else {
            $rootScope.randomsItems = $rootScope.randomsItemsTemp.filter(function (items) { return items.ProfileID == newValue })
            //$scope.selectPage(1);

            ctrl.callServer($scope.t);
            $scope.t.pagination.start = 0;
            ctrl.temp = $rootScope.randomsItems;
            //$('.select-page').val("1");
        }

    })

    $scope.SelectProvider = function (ProviderData) {
        $scope.ProviderNameToPrint = angular.copy(ProviderData.Name);
        $scope.watchProfileID = ProviderData.ProfileId;
        $rootScope.ProviderName1 = ProviderData.Name;
        $scope.ProviderName = angular.copy($rootScope.ProviderName1);
        $(".ProviderTypeSelectAutoList").hide();
    }
    $scope.RemoveReport = function () {
        $('#WarningModal').modal('hide');
        $http.post(rootDir + '/Credentialing/CnD/RemoveGrid?contractGridID=' + ContractGrid).
            success(function (data, status, headers, config) {
                ngToast.create({
                    className: 'danger',
                    content: 'Contract Removed Successfully.'
                });
                $rootScope.randomsItems.splice($rootScope.randomsItems.indexOf($rootScope.randomsItems.filter(function (items) { return items.ContractGridID == ContractGrid })[0]), 1);
                $rootScope.randomsItemsTemp.splice($rootScope.randomsItemsTemp.indexOf($rootScope.randomsItemsTemp.filter(function (items) { return items.ContractGridID == ContractGrid })[0]), 1);
                //$rootScope.randomsItemsTemp.splice($rootScope.randomsItemsTemp.indexOf($rootScope.randomsItemsTemp.filter(function (items) { return items.ContractGridID == ContractGrid })[0]), 1);
                ctrl.callServer($scope.t);
                $scope.t.pagination.start = 0;
            }).
        error(function (data, status, headers, config) {

        });
    };

    $scope.setFiles1 = function (file) {
        $(file).parent().parent().find(".jancyFileWrapTexts").find("span").width($(file).parent().parent().width() < 243 ? $(file).parent().parent().width() : 243);

    }


    $scope.viewContractGrid = function (ContractGridID, PlanName, LOBName) {
        $scope.TempPlanName = PlanName;
        $scope.TempLOBName = LOBName;
        $rootScope.notEdit = false;
        $scope.AjaxLoading = true;
        $scope.tempNameObject = [];
        $scope.tempNameObject = $rootScope.randomsItems.filter(function (id) { return id.ContractGridID == ContractGridID })[0]
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
    //$('#providersName').on("SelectProviders", function () {
    //    alert();
    //    $('#providersName').val("");
    //    $scope.watchProfileID = $rootScope.ProviderData.ProfileId = 1;
    //    $scope.ProviderName = $rootScope.ProviderData.Name = "Alex Anthony Tambrini-1699768812";
    //});

    $scope.activeProviders = function () {
        //$scope.ProviderName = "";        
        $scope.activeStatus = true;
        $scope.inactiveStatus = false;
        $scope.cancelView();
        $scope.cancelEdit();

        //$rootScope.$on('reactivate', function () {
        if ((typeof $scope.ProviderName == "undefined") || ($scope.ProviderName == "")) {
            $rootScope.randomsItems = angular.copy($rootScope.randomsItemsTemp);
        }
        else {

            $rootScope.randomsItems = $rootScope.randomsItemsTemp.filter(function (items) { return items.ProfileID == $scope.watchProfileID })

            $scope.t.pagination.start = 0;
            ctrl.callServer($scope.t);

            //ctrl.temp = $rootScope.randomsItems;
        }
        //$rootScope.randomsItems = angular.copy($rootScope.randomsItemsTemp);
        ctrl.callServer($scope.t);
        //})              
        //ctrl.temp = $rootScope.randomsItemsTemp;
    }

    $scope.inactiveProviders = function () {

        initval++;
        $rootScope.initialValue = initval;
        $scope.activeStatus = false;
        $scope.inactiveStatus = true;

    }
    $rootScope.pushContract = function () {
        var tableState = {
            sort: {},
            search: {},
            pagination: {
                start: 0
            }
        };
        ctrl.callServer(tableState);
    }
    $scope.cancelView = function () {
        $scope.tempObject = {};
        $scope.isView = false;
        $rootScope.notEdit = true;

    }
    $scope.cancelViewWhenEdit = function () {
        $scope.tempObject = {};
        $scope.isView = false;
        $rootScope.notEdit = true;
    }
    $scope.SelectStatus = function (status) {
        $scope.tempObject.Report.ParticipatingStatus = status;
        $(".ProviderTypeSelectAutoList").hide();
    }

    $scope.dateConvert = function (value) {
        if (value) {
            if (value.indexOf('T') > -1) {
                return value.split('T')[0];
            }
        }

    }

    var IdIndex = -1;
    $scope.EditContractGrid = function (ContractGridID, PlanName, LOBName) {
        $scope.AjaxLoading = true;
        $rootScope.notEdit = false;
        $scope.PlanNameEdit = PlanName;
        $scope.LOBNameEdit = LOBName;
        $scope.tempNameObject = $rootScope.randomsItems.filter(function (items) { return items.ContractGridID == ContractGridID })[0]
        $scope.tempObject = {};
        IdIndex = $rootScope.randomsItems.indexOf($rootScope.randomsItems.filter(function (items) { return items.ContractGridID == ContractGridID })[0])
        $http.get(rootDir + '/Credentialing/CnD/GetContractGridById?ContractGridID=' + ContractGridID)
        .success(function (Data) {
            $scope.tempObject = angular.copy(Data);
            //$scope.tempObject.Report.InitiatedDate = $scope.dateConvert($scope.tempObject.Report.InitiatedDate);
            //$scope.tempObject.Report.TerminationDate = $scope.dateConvert($scope.tempObject.Report.TerminationDate);
            console.log($scope.tempObject);
            $scope.AjaxLoading = false;
            $scope.isEdit = true;
        })
        .error(function (EreorMsg) {

        })
    }
    $scope.cancelEdit = function () {
        $scope.AjaxLoading = true;
        $rootScope.notEdit = true;
        $scope.tempObject = {};
        $('#PlanReportForm').find('form')[0].reset();
        $scope.isEdit = false;
        $scope.AjaxLoading = false;
        $('.badge').removeAttr('style');
        $("#PlanReportForm .field-validation-valid").hide();

    }

    $scope.SaveReport = function () {
        $scope.TerStatus = true;
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
                success: function (datas) {
                    var data = JSON.parse(datas);
                    try {
                        if (data.status == "true") {
                            ngToast.create({
                                className: 'info',
                                content: 'Contract Saved Successfully.'
                            });                            
                            $rootScope.randomsItems[IdIndex].ParticipatingStatus = data.dataContractGrid.Report.ParticipatingStatus;
                            $rootScope.randomsItems[IdIndex].ProviderID = data.dataContractGrid.Report.ProviderID;
                            $rootScope.randomsItems[IdIndex].GroupID = data.dataContractGrid.Report.GroupID;
                            $rootScope.randomsItems[IdIndex].InitiatedDate = $scope.ConvertDateFormat(data.dataContractGrid.Report.InitiatedDate);
                            $rootScope.randomsItems[IdIndex].TerminationDate = $scope.ConvertDateFormat(data.dataContractGrid.Report.TerminationDate);
                            $scope.AjaxLoading = false;
                            $scope.isEdit = false;
                            $rootScope.notEdit = true;
                            if (data.dataContractGrid.Report.ParticipatingStatus.toLowerCase() == 'termed') {
                                $rootScope.randomsItems.splice($rootScope.randomsItems.indexOf($rootScope.randomsItems.filter(function (items) { return items.ContractGridID == data.dataContractGrid.ContractGridID })[0]), 1);
                                $rootScope.randomsItemsTemp.splice($rootScope.randomsItemsTemp.indexOf($rootScope.randomsItemsTemp.filter(function (items) { return items.ContractGridID == data.dataContractGrid.ContractGridID })[0]), 1);
                                ngToast.create({
                                    className: 'info',
                                    content: 'Contract Moved To Inactive Contracts.'
                                });
                            }
                            ctrl.callServer($scope.t);
                            $formData[0].reset();

                        }
                        else {
                            $scope.AjaxLoading = false;
                            if (data.status.includes("Report.TerminationDate")) {
                                $scope.TerStatus = false;
                                $scope.TerminationDateError = data.status;
                            }


                        }
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
            if (returnValue != null) {
                returnValue = new Date(returnValue);
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

    $rootScope.Loading = false;

    $scope.FlipingTabs = function (i, j) {
        var id_show = '#' + i;
        var id_hide = '#' + j;
        $(id_show).show();
        $(id_hide).hide();
    };

    $scope.changeTabs = function (id, selectedId, panel) {
        var sel = '#' + selectedId;
        var id = '#' + id;
        $("#id01").removeClass('active').addClass('fade');
        $("#id03").removeClass('active').addClass('fade');
        $("#id04").removeClass('active').addClass('fade');
        $(sel).addClass('active').removeClass('fade');
        $("#GenInfoTab").removeClass('active');
        $("#LobTab").removeClass('active');
        $("#PlanEnrollTab").removeClass('active');
        $(id).addClass('active');

        if (panel == "LOBPanel") {
            $scope.GenInfo = false;
            $scope.LOBPanel = true;
            $scope.PlanEnrollmentPanel = false;
        }
        if (panel == "GenInfo") {
            $scope.GenInfo = true;
            $scope.LOBPanel = false;
            $scope.PlanEnrollmentPanel = false;
        }
        if (panel == "PlanEnrollmentPanel") {
            $scope.GenInfo = false;
            $scope.LOBPanel = false;
            $scope.PlanEnrollmentPanel = true;
        }


    };

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