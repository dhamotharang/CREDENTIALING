//--------------------- Author: KRGLV --------------------------------
//--------------- User Use Strict Section ---------------

define(['Util/MasterProfileApp'], function (app) {
    'use strict';
    app.register.controller('ContractGridController', ["$scope", "$rootScope", "$timeout", "httpq", "Resource1", "ProfileSubData",
      function ($scope, $rootScope, $timeout, httpq, Resource1, ProfileSubData) {
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

          $scope.callServer = function callServer(tableState) {
              $scope.isLoading = true;
              $scope.t = tableState;
              if (tableState && tableState.pagination) {
                  var pagination = tableState.pagination;

                  console.log(pagination.number);

                  var start = pagination.start || 0;
                  var number = pagination.number || 5;

                  Resource1.getPage(start, number, tableState).then(function (result) {
                      $scope.displayed = result.data;
                      console.log(result.data);
                      tableState.pagination.numberOfPages = result.numberOfPages;//set the number of pages so the pagination can update
                      $scope.isLoading = false;
                  });
              }
          };

          $(function () {
              
              //$scope.showLoading = false;
              $scope.names = angular.copy(ProfileSubData);
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
              $scope.callServer($scope.t);
          });
          
          $scope.displayed = [];
          $rootScope.AllData = false;
          

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
});