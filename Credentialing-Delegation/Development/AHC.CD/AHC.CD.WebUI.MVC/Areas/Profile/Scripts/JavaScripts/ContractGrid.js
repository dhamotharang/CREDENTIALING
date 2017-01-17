
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


profileApp.controller("ContractGridController", function ($scope, $rootScope, $q, $timeout, $http, Resource1, messageAlertEngine) {

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
        $scope.activeTableState = {};
        $scope.inactiveTableState = {};
        $scope.printData = true;
        //if (!$scope.dataLoaded) {
        //    $rootScope.ContractGridLoaded = false;
        //    $rootScope.randomsItems = [];
        //    var url = rootDir + "/ContractGrid/GetAllActiveContractGridinfoes?profileid=" + profileId;
        //    $http.get(url).
        //      success(function (data) {

        //          ctrl.isLoading = true;
        //          //$scope.showLoading = false;
        //          $scope.names = angular.copy(data);
        //          for (var i = 0 ; i < $scope.names.length; i++) {
        //            //  $scope.names[i].LOBS = [];
        //              //if ($scope.names[i].LOB != null) {
        //              //    $scope.names[i].LOBSData = $scope.names[i].LOB.split(",");
        //              //    for (var j in $scope.names[i].LOBSData) {
        //              //        if ($scope.names[i].LOBSData[j] != '') {
        //              //            $scope.names[i].LOBS.push($scope.names[i].LOBSData[j]);
        //              //        }
        //              //    }
        //              //}

        //              if ($scope.names[i].EffectiveDate != null) {
        //                  $scope.names[i].EffectiveDate = $scope.ConvertDate($scope.names[i].EffectiveDate);
        //              }
        //              if ($scope.names[i].TerminationDate != null) {
        //                  $scope.names[i].TerminationDate = $scope.ConvertDate($scope.names[i].TerminationDate);
        //              }
        //          }
        //          $rootScope.randomsItems = angular.copy($scope.names);
        //          ctrl.callServer($scope.t);
        //          $rootScope.ContractGridLoaded = true;
        //      }).
        //          error(function (data) {
        //              $rootScope.ContractGridLoaded = true;
        //              //$scope.names = 'error';
        //          });
        //    ctrl.isLoading = true;
        //    $scope.dataLoaded = true;
        //}
        var active = $http.get(rootDir + "/ContractGrid/GetAllActiveContractGridinfoes?profileid=" + profileId).
             success(function (data) {
                 ctrl.isLoading = true;
                 $scope.activeContracts = angular.copy(data);
                 $scope.activeContractStatus = true;
                 for (var i = 0 ; i < $scope.activeContracts.length; i++) {

                     if ($scope.activeContracts[i].EffectiveDate != null) {
                         $scope.activeContracts[i].EffectiveDate = $scope.ConvertDate($scope.activeContracts[i].EffectiveDate);
                     }
                     if ($scope.activeContracts[i].TerminationDate != null) {
                         $scope.activeContracts[i].TerminationDate = $scope.ConvertDate($scope.activeContracts[i].TerminationDate);
                     }
                 }
             }).
                  error(function (data) {

                  });
        var inactive = $http.get(rootDir + "/ContractGrid/GetAllInActiveContractGridinfoes?profileid=" + profileId).
                  success(function (data) {
                      $scope.inactiveContracts = angular.copy(data);
                      for (var i = 0 ; i < $scope.inactiveContracts.length; i++) {
                          if ($scope.inactiveContracts[i].EffectiveDate != null) {
                              $scope.inactiveContracts[i].EffectiveDate = $scope.ConvertDate($scope.inactiveContracts[i].EffectiveDate);
                          }
                          if ($scope.inactiveContracts[i].TerminationDate != null) {
                              $scope.inactiveContracts[i].TerminationDate = $scope.ConvertDate($scope.inactiveContracts[i].TerminationDate);
                          }
                      }
                  }).
                       error(function (data) {

                       });
        var combinedPromise = $q.all({
            activeContract: active,
            inactiveContract: inactive
        }).then(function (response) {
            try {
                ctrl.isLoading = true;
                $rootScope.randomsItems = angular.copy($scope.activeContracts);
                $scope.t.pagination.start = 0;
                $scope.t.search.predicateObject = {};
                ctrl.callServer($scope.t);
            } catch (e) {

            }

        });
    });

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

    $scope.activeContractData = function () {
        $scope.tableView = true;
        $scope.isEdit = false;
        $scope.isView = false;
        $scope.activeContractStatus = true;
        $rootScope.randomsItems = angular.copy($scope.activeContracts);
        $scope.t.pagination.start = 0;
        $scope.t.search.predicateObject = {};
        for (var i in names) {
            if ($scope.activeTableState != null) {
                if ($scope.activeTableState.hasOwnProperty('search')) {
                    if ($scope.activeTableState.search.predicateObject.hasOwnProperty(names[i])) {
                        $scope.t.search.predicateObject = $scope.activeTableState.search.predicateObject;
                    }
                }
            }
        }
        ctrl.callServer($scope.t);
    }

    $scope.inactiveContractData = function () {
        $scope.tableView = true;
        $scope.isEdit = false;
        $scope.isView = false;
        $scope.activeContractStatus = false;
        $rootScope.randomsItems = angular.copy($scope.inactiveContracts);
        $scope.t.pagination.start = 0;
        $scope.t.search.predicateObject = {};
        $rootScope.ProfilePanelData = false;
        for (var i in names) {
            if ($scope.inactiveTableState != null) {
                if ($scope.inactiveTableState.hasOwnProperty('search')) {
                    if ($scope.inactiveTableState.search.predicateObject.hasOwnProperty(names[i])) {
                        $scope.t.search.predicateObject = $scope.inactiveTableState.search.predicateObject;
                    }
                }
            }
        }
        ctrl.callServer($scope.t);
    }
    $scope.GenInfo = true;
    var ctrl = this;
    this.displayed = [];
    $rootScope.AllData = false;
    this.callServer = function callServer(tableState) {
        ctrl.isLoading = true;
        $scope.t = tableState;
        var pagination = tableState.pagination;


        var start = pagination.start || 0;
        var number = pagination.number || 10;

        Resource1.getPage(start, number, tableState).then(function (result) {
            ctrl.displayed = result.data;
            tableState.pagination.numberOfPages = result.numberOfPages;//set the number of pages so the pagination can update
            ctrl.isLoading = false;
            if ($scope.activeContractStatus && sortData) {
                $scope.activeTableState = angular.copy(tableState);
            }
            if ($scope.activeContractStatus && !sortData) {
                if ($scope.activeTableState != null) {
                    if ($scope.activeTableState.hasOwnProperty('search')) {
                        $scope.activeTableState.search.predicateObject = {};
                    }
                }
            }
            if (!$scope.activeContractStatus && sortData) {
                $scope.inactiveTableState = angular.copy(tableState);
            }
            if (!$scope.activeContractStatus && !sortData) {
                if ($scope.inactiveTableState != null) {
                    if ($scope.inactiveTableState.hasOwnProperty('search')) {
                        $scope.inactiveTableState.search.predicateObject = {};
                    }
                }
                
            }
        });
    };
    $scope.searchCumDropDown = function (divId) {
        $(".ProviderTypeSelectAutoList1").hide();
        $("#" + divId).show();
    };
    $rootScope.printCGData = function () {
        $rootScope.AllData = true;
        $rootScope.LoadForPdf = true;
        $rootScope.ProfilePanelData = false;
        var title = '';
        $timeout(function () {
            var divToPrint = document.getElementById("Contract_Grid1");
            $('#hiddenPrintDiv').empty();
            $('#hiddenPrintDiv').append(divToPrint.innerHTML);
            $scope.eTempObject = {};
            $('#hideForPdf1').attr("style", "display:none");
            $('#hideForPdf2').attr("style", "display:none");
            // Removing the last column of the table

            // Creating a window for printing
            if ($scope.activeContractStatus) {
                title = "Contract Information";
            }
            else {
                title = "Inactive Contract Information";
            }
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
    $http.get(rootDir + '/Credentialing/CnD/GetAllParticipatingStatus').
  success(function (data, status, headers, config) {
      try {
          $scope.ParticipatingStatusList = angular.copy(data);
      } catch (e) {

      }
  }).
  error(function (data, status, headers, config) {

  });
    $scope.ProfileContractGrid = true;
    $scope.tableView = true;
    $scope.viewContractGrid = function (ContractGridID, PlanName, LOBName) {
        $scope.tableView = false;
        $scope.TempPlanName = PlanName;
        $scope.TempLOBName = LOBName;
        $scope.printData = false;
        // $rootScope.notEdit = false;
        $scope.AjaxLoading = true;
        $scope.tempNameObject = [];
        $scope.tempNameObject.ProviderName = $rootScope.FirstName + " " + $rootScope.MiddleName + " " + $rootScope.lastName;
        $scope.tempNameObject = $rootScope.randomsItems.filter(function (id) { return id.ContractGridID == ContractGridID })[0]
        $http.get(rootDir + '/Credentialing/CnD/GetContractGridById?ContractGridID=' + ContractGridID)
        .success(function (Data) {
            $scope.tempObject = angular.copy(Data);
            console.log($scope.tempObject);
            $scope.AjaxLoading = false;
            $scope.isView = true;
        })
        .error(function (ErrorMsg) {

        })
    }
    $scope.isEdit = false;
    $scope.isView = false;
    $scope.EditContractGrid = function (ContractGridID, PlanName, LOBName) {
        $scope.tableView = false;
        $scope.AjaxLoading = true;
        $scope.printData = false;
        // $rootScope.notEdit = false;
        $scope.PlanNameEdit = PlanName;
        $scope.LOBNameEdit = LOBName;
        $scope.tempNameObject = $rootScope.randomsItems.filter(function (items) { return items.ContractGridID == ContractGridID })[0];
        $scope.tempNameObject.ProviderName = $rootScope.FirstName + " " + "" + $rootScope.MiddleName + " " + $rootScope.lastName;
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
        .error(function (ErrorMsg) {

        })
    }
    $scope.SetRecredentialingDate = function (value) {
        if (value == null) {
            return
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

    $scope.cancelEdit = function () {
        $scope.tableView = true;
        $scope.printData = true;
        //$scope.AjaxLoading = true;
        // $rootScope.notEdit = true;
        $scope.tempObject = {};
        $('#PlanReportForm').find('form')[0].reset();
        $scope.isEdit = false;
        //$scope.AjaxLoading = false;
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
                success: function (datas) {
                    var data = JSON.parse(datas);
                    try {
                        if (data.status == "true") {
                            $rootScope.randomsItems[IdIndex].ParticiPationStatus = data.dataContractGrid.Report.ParticipatingStatus;
                            $rootScope.randomsItems[IdIndex].ProviderID = data.dataContractGrid.Report.ProviderID;
                            $rootScope.randomsItems[IdIndex].IndividualID = data.dataContractGrid.Report.ProviderID;
                            $rootScope.randomsItems[IdIndex].GroupID = data.dataContractGrid.Report.GroupID;
                            $rootScope.randomsItems[IdIndex].InitiatedDate = $scope.ConvertDateFormat(data.dataContractGrid.Report.InitiatedDate);
                            $rootScope.randomsItems[IdIndex].EffectiveDate = $scope.ConvertDateFormat(data.dataContractGrid.Report.InitiatedDate);
                            $rootScope.randomsItems[IdIndex].TerminationDate = $scope.ConvertDateFormat(data.dataContractGrid.Report.TerminationDate);
                            $scope.AjaxLoading = false;
                            messageAlertEngine.callAlertMessage("successfullySaved", "Contract successfully saved.", "success", true);
                            $scope.isEdit = false;
                            $scope.isView = true;
                            $rootScope.notEdit = true;
                            if (data.dataContractGrid.Report.ParticipatingStatus.toLowerCase() == 'termed') {
                                //$rootScope.randomsItems.splice($rootScope.randomsItems.indexOf($rootScope.randomsItems.filter(function (items) { return items.ContractGridID == data.dataContractGrid.ContractGridID })[0]), 1);
                                var removed;
                                removed = $scope.randomsItems.splice($scope.randomsItems.indexOf($scope.randomsItems.filter(function (items) { return items.ContractGridID == data.dataContractGrid.ContractGridID })[0]), 1);
                                $scope.activeContracts.splice($scope.activeContracts.indexOf($scope.activeContracts.filter(function (items) { return items.ContractGridID == data.dataContractGrid.ContractGridID })[0]), 1);
                                $scope.inactiveContracts.push(removed[0]);
                                messageAlertEngine.callAlertMessage("successfullySaved", "Contract Moved To Inactive Contracts.", "success", true);
                                $scope.cancelView();
                            }
                            ctrl.callServer($scope.t);
                            $formData[0].reset();
                        }
                        else {
                            messageAlertEngine.callAlertMessage("errorInitiated", "Please try after sometime !!!!", "danger", true);
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
    $scope.cancelView = function () {
        $scope.tableView = true;
        $scope.tempObject = {};
        $scope.isView = false;
        $scope.printData = true;
        // $rootScope.notEdit = true;

    }
    $scope.cancelViewWhenEdit = function () {
        $scope.tempObject = {};
        $scope.isView = false;
        $rootScope.notEdit = true;
    }
    var ContractGrid = null
    $scope.initWarning = function (remove) {
        ContractGrid = angular.copy(remove);
        $('#WarningModal').modal();
    };

    $scope.RemoveReport = function () {
        $('#WarningModal').modal('hide');
        $http.post(rootDir + '/Credentialing/CnD/RemoveGrid?contractGridID=' + ContractGrid).
            success(function (data, status, headers, config) {
                
                //$rootScope.randomsItems.splice($rootScope.randomsItems.indexOf($rootScope.randomsItems.filter(function (items) { return items.ContractGridID == ContractGrid })[0]), 1);
                //$rootScope.randomsItemsTemp.splice($rootScope.randomsItemsTemp.indexOf($rootScope.randomsItemsTemp.filter(function (items) { return items.ContractGridID == ContractGrid })[0]), 1);
                //$rootScope.randomsItemsTemp.splice($rootScope.randomsItemsTemp.indexOf($rootScope.randomsItemsTemp.filter(function (items) { return items.ContractGridID == ContractGrid })[0]), 1);
                $scope.removed; 
                $scope.removed = $scope.randomsItems.splice($scope.randomsItems.indexOf($scope.randomsItems.filter(function (items) { return items.ContractGridID == ContractGrid })[0]), 1);
                $scope.activeContracts.splice($scope.activeContracts.indexOf($scope.activeContracts.filter(function (items) { return items.ContractGridID == ContractGrid })[0]), 1);
                $scope.inactiveContracts.push($scope.removed[0]);
                messageAlertEngine.callAlertMessage("successfullySaved", "Contract Removed successfully...", "success", true);
                ctrl.callServer($scope.t);
                $scope.t.pagination.start = 0;
            }).
        error(function (data, status, headers, config) {
            messageAlertEngine.callAlertMessage("errorInitiated", "Please try after sometime !!!!", "danger", true);
        });
    };
    $scope.SelectStatus = function (status) {
        $scope.tempObject.Report.ParticipatingStatus = status;
        $(".ProviderTypeSelectAutoList1").hide();
    }
    var ContractGrid = null
    $scope.reactivateContractGrid = function (remove) {

        ContractGrid = angular.copy(remove);

        $scope.removedProvider = angular.copy(ContractGrid);
        $('#WarningModal1').modal();
    };
    $scope.ReactivateInactiveReport = function () {
        $('#WarningModal1').modal('hide');
        $http.post(rootDir + '/Credentialing/CnD/ReactivateGrid?contractGridID=' + ContractGrid).
            success(function (data, status, headers, config) {
                
                $scope.reactivated;
                $scope.reactivated = $scope.randomsItems.splice($scope.randomsItems.indexOf($scope.randomsItems.filter(function (items) { return items.ContractGridID == ContractGrid })[0]), 1);
                $scope.inactiveContracts.splice($scope.inactiveContracts.indexOf($scope.inactiveContracts.filter(function (items) { return items.ContractGridID == ContractGrid })[0]), 1);
                $scope.activeContracts.push($scope.reactivated[0]);
                messageAlertEngine.callAlertMessage("successfullySaved", "Contract Reactivated Successfully...", "success", true);
                ctrl.callServer($scope.t);
                $scope.t.pagination.start = 0;
            }).
        error(function (data, status, headers, config) {
            messageAlertEngine.callAlertMessage("errorInitiated", "Please try after sometime !!!!", "danger", true);
        });
    };

    $scope.exportToExcel1 = function () {
        $rootScope.LoadForPdf = true;
        $rootScope.AllData = true;
        $rootScope.ProfilePanelData = true;
        var excelTitle = '';
        if ($scope.activeContractStatus) {
            excelTitle = "Contract Information";
        }
        else {
            excelTitle = "Inactive Contract Information";
        }
        $timeout(function () {
            var divToPrint = document.getElementById("Contract_Grid1");
            $('#hiddenPrintDiv1').empty();
            $('#hiddenPrintDiv1').append("<table>" + divToPrint.innerHTML + "</table>");
            $('#hiddenPrintDiv1 table').attr("id", "exportTable1");
            $('#hiddenPrintDiv1').attr("download", "ExportToExcel.xls");
            $('#exportTable1').tableExport({ type: 'excel', escape: 'false' }, excelTitle);
            $rootScope.AllData = false;
            $rootScope.LoadForPdf = false;
        }, 3000)
    }
});

names = ['PlanName', 'LOBCode', 'ParticiPationStatus', 'GroupID', 'IndividualID', 'EffectiveDate', 'TerminationDate'];

profileApp.factory('Resource1', ['$q', '$rootScope', '$filter', '$timeout', function ($q, $rootScope, $filter, $timeout) {
    function getPage(start, number, params) {

        var deferred = $q.defer();
        //if($rootScope.)
        //if (activeContractStatus){
        //    $rootScope.filtered = params.search.predicateObject ? $filter('filter')($rootScope.activeContracts, params.search.predicateObject) : $rootScope.activeContracts;
        //}
        //else {
        //    $rootScope.filtered = params.search.predicateObject ? $filter('filter')($rootScope.inactiveContracts, params.search.predicateObject) : $rootScope.inactiveContracts;
        //}
        $rootScope.filtered = params.search.predicateObject ? $filter('filter')($rootScope.randomsItems, params.search.predicateObject) : $rootScope.randomsItems;

        for (var i in names) {

            sortData = ((typeof params.search.predicateObject != "undefined") && (params.search.predicateObject.hasOwnProperty(names[i]))) ? true : false;
            if (sortData == true) break;
        }

        if (params.sort.predicate) {
            $rootScope.filtered = $filter('orderBy')($rootScope.filtered, params.sort.predicate, params.sort.reverse);
        }
        var result = $rootScope.filtered.slice(start, start + number);
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
$(document).ready(function () {
    $(".ProviderTypeSelectAutoList").hide();
    $(".ProviderTypeSelectAutoList1").hide();
    $("#Add_New_Contract").hide();
});
$(document).click(function (event) {
    if (!$(event.target).hasClass("form-control") && $(event.target).parents(".ProviderTypeSelectAutoList").length === 0) {
        $(".ProviderTypeSelectAutoList").hide();
    }
    if (!$(event.target).hasClass("form-control") && $(event.target).parents(".ProviderTypeSelectAutoList1").length === 0) {
        $(".ProviderTypeSelectAutoList1").hide();
    }
});