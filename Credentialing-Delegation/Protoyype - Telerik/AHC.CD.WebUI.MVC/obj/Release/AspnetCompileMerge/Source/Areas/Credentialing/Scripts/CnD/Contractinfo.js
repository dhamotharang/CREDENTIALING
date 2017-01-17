//Contract Information Controller Angular MOdule
//Author Santosh K.

$(document).ready(function () {
    
    $("#sidemenu").addClass("menu-in");
    $("#page-wrapper").addClass("menuup");

    //var specialElementHandlers = {
    //    '#editor': function (element, renderer) {
    //        return true;
    //    }
    //};

});

var ContractInfo = angular.module("Contractinfo", ['ngTable', 'mgcrea.ngStrap']);

//------------- angular tool tip recall directive ---------------------------
//ContractInfo.directive('tooltip', function () {
//    return function (scope, elem) {
//        elem.tooltip();
//    };
//});



ContractInfo.controller("ContractinfoController", function ($scope, $rootScope, $filter, $timeout, $http, ngTableParams) {

    $rootScope.printData = function (id, title) {
        var divToPrint = document.getElementById(id);

        $('#hiddenPrintDiv').empty();
        $('#hiddenPrintDiv').append(divToPrint.innerHTML);
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
        mywindow.document.write('</head><body style="background-color:white"><table class="table table-bordered" ></td>');
        mywindow.document.write($('#hiddenPrintDiv').html());
        mywindow.document.write('</table></body></html>');
        mywindow.document.close();
        mywindow.focus();
        setTimeout(function () {
            mywindow.print();
            mywindow.close();
        }, 1000);
        return true;
    }
    $scope.loadingAjax = false;
    $scope.IsDetails = false;
    $scope.display = false;
    $scope.edit = false;
    $scope.editMe = true;
    $scope.updated = false;
    $scope.myIndex = 0;
    $scope.loadingAjax = true;
    $scope.isChange = false;

    $http.get(rootDir + '/Credentialing/CnD/GetAllContractGrid').
       success(function (data, status, headers, config) {
           try {
               $scope.Credinfo = [];
               $scope.CredentialingContractRequests = [];
               if (data != null) {
                   $scope.Credinfo = angular.copy(data);
               }
               for (var i = 0; i < $scope.Credinfo.length; i++) {
                   for (var j = 0; j < $scope.Credinfo[i].CredentialingContractRequests.length; j++) {
                       if ($scope.Credinfo[i].CredentialingContractRequests[j].Status == 'Active' && $scope.Credinfo[i].CredentialingContractRequests[j].ContractRequestStatus == 'Active') {

                           var ContractGrids = [];
                           var ContractGridTableStatus = false;
                           for (var k = 0; k < $scope.Credinfo[i].CredentialingContractRequests[j].ContractGrid.length; k++) {
                               if ($scope.Credinfo[i].CredentialingContractRequests[j].ContractGrid[k].Status == 'Inactive') {
                                   ContractGrids.push($scope.Credinfo[i].CredentialingContractRequests[j].ContractGrid[k]);
                               }

                           }
                           for (var k = 0; k < ContractGrids.length; k++) {
                               $scope.Credinfo[i].CredentialingContractRequests[j].ContractGrid.splice($scope.Credinfo[i].CredentialingContractRequests[j].ContractGrid.indexOf(ContractGrids[k]), 1);
                           }

                           var counter = 0;
                           var gridLength = $scope.Credinfo[i].CredentialingContractRequests[j].ContractGrid.length;
                           if (gridLength > 0) {
                               for (var k = 0; k < $scope.Credinfo[i].CredentialingContractRequests[j].ContractGrid.length; k++) {
                                   if ($scope.Credinfo[i].CredentialingContractRequests[j].ContractGrid[k].Status == 'Active') {
                                       if ($scope.Credinfo[i].CredentialingContractRequests[j].ContractGrid[k].Report != null) {
                                           //if ($scope.Credinfo[i].CredentialingContractRequests[j].ContractGrid[k].Report.AdminFee == null && $scope.Credinfo[i].CredentialingContractRequests[j].ContractGrid[k].Report.CAP == null && $scope.Credinfo[i].CredentialingContractRequests[j].ContractGrid[k].Report.ContractDocumentPath == null && $scope.Credinfo[i].CredentialingContractRequests[j].ContractGrid[k].Report.CredentialedDate == null && $scope.Credinfo[i].CredentialingContractRequests[j].ContractGrid[k].Report.CredentialingApprovalStatus == null && $scope.Credinfo[i].CredentialingContractRequests[j].ContractGrid[k].Report.GroupID == null && $scope.Credinfo[i].CredentialingContractRequests[j].ContractGrid[k].Report.InitiatedDate == null && $scope.Credinfo[i].CredentialingContractRequests[j].ContractGrid[k].Report.PanelStatus == null && $scope.Credinfo[i].CredentialingContractRequests[j].ContractGrid[k].Report.PercentageOfRisk == null && $scope.Credinfo[i].CredentialingContractRequests[j].ContractGrid[k].Report.ProviderID == null && $scope.Credinfo[i].CredentialingContractRequests[j].ContractGrid[k].Report.ReCredentialingDate == null && $scope.Credinfo[i].CredentialingContractRequests[j].ContractGrid[k].Report.Status == null && $scope.Credinfo[i].CredentialingContractRequests[j].ContractGrid[k].Report.StopLossFee == null && $scope.Credinfo[i].CredentialingContractRequests[j].ContractGrid[k].Report.TerminationDate == null && $scope.Credinfo[i].CredentialingContractRequests[j].ContractGrid[k].Report.WelcomeLetterPath == null) {
                                           counter++;
                                           //}
                                       }
                                   }
                               }
                           }

                           var counter1 = 0;
                           var gridLength1 = $scope.Credinfo[i].CredentialingContractRequests[j].ContractGrid.length;
                           if (gridLength1 > 0) {
                               for (var k = 0; k < $scope.Credinfo[i].CredentialingContractRequests[j].ContractGrid.length; k++) {
                                   if ($scope.Credinfo[i].CredentialingContractRequests[j].ContractGrid[k].Status == 'Active') {
                                       if ($scope.Credinfo[i].CredentialingContractRequests[j].ContractGrid[k].Report != null) {
                                           if ($scope.Credinfo[i].CredentialingContractRequests[j].ContractGrid[k].Report.CredentialingApprovalStatus == 'Rejected') {
                                               counter1++;
                                           }
                                       }
                                   }
                               }
                           }

                           var counter2 = 0;
                           var gridLength2 = $scope.Credinfo[i].CredentialingContractRequests[j].ContractGrid.length;
                           if (gridLength2 > 0) {
                               for (var k = 0; k < $scope.Credinfo[i].CredentialingContractRequests[j].ContractGrid.length; k++) {
                                   if ($scope.Credinfo[i].CredentialingContractRequests[j].ContractGrid[k].Status == 'Active') {
                                       if ($scope.Credinfo[i].CredentialingContractRequests[j].ContractGrid[k].Report != null) {

                                           if ($scope.Credinfo[i].CredentialingContractRequests[j].ContractGrid[k].Report.TerminationDate != null && (new Date(parseInt($scope.Credinfo[i].CredentialingContractRequests[j].ContractGrid[k].Report.TerminationDate.replace("/Date(", "").replace(")/", ""), 10)).getTime() < new Date().getTime())) {
                                               counter2++;
                                           }
                                       }
                                   }
                               }
                           }

                           if (gridLength == counter && gridLength > 0) {

                               ContractGridTableStatus = true;
                               $scope.Credinfo[i].CredentialingContractRequests[j].TableRowStatus = false;
                               $scope.Credinfo[i].CredentialingContractRequests[j].credID = $scope.Credinfo[i].CredentialingInfoID;
                               $scope.Credinfo[i].CredentialingContractRequests[j].PersonalDetail = $scope.Credinfo[i].Profile.PersonalDetail;
                               $scope.Credinfo[i].CredentialingContractRequests[j].Plan = $scope.Credinfo[i].Plan;
                               $scope.CredentialingContractRequests.push($scope.Credinfo[i].CredentialingContractRequests[j]);


                           }
                           else if (gridLength1 == counter1 && gridLength1 > 0) {
                               continue;
                           }
                           else if (gridLength2 == counter2 && gridLength2 > 0) {
                               continue;
                           }
                           else {
                               var ContractGridCount = $scope.Credinfo[i].CredentialingContractRequests[j].ContractGrid.length;
                               if (ContractGridCount > 0) {
                                   //var ContractGridReportsData = [];
                                   //for (var k = 0; k < $scope.Credinfo[i].CredentialingContractRequests[j].ContractGrid.length; k++) {
                                   //    if ($scope.Credinfo[i].CredentialingContractRequests[j].ContractGrid[k].Report != null) {
                                   //        if ($scope.Credinfo[i].CredentialingContractRequests[j].ContractGrid[k].Report.CredentialingApprovalStatus != '' && $scope.Credinfo[i].CredentialingContractRequests[j].ContractGrid[k].Report.TerminationDate != null && $scope.Credinfo[i].CredentialingContractRequests[j].ContractGrid[k].Report.CredentialingApprovalStatus != null) {
                                   //            continue;
                                   //        }
                                   //        else {
                                   //            ContractGridReportsData.push($scope.Credinfo[i].CredentialingContractRequests[j].ContractGrid[k]);
                                   //        }
                                   //    }
                                   //    else {
                                   //        ContractGridReportsData.push($scope.Credinfo[i].CredentialingContractRequests[j].ContractGrid[k]);
                                   //    }

                                   //}
                                   //for (var k = 0; k < ContractGridReportsData.length; k++) {
                                   //    $scope.Credinfo[i].CredentialingContractRequests[j].ContractGrid.splice($scope.Credinfo[i].CredentialingContractRequests[j].ContractGrid.indexOf(ContractGridReportsData[k]), 1);
                                   //}
                                   if (ContractGridCount == $scope.Credinfo[i].CredentialingContractRequests[j].ContractGrid.length) {
                                       $scope.Credinfo[i].CredentialingContractRequests[j].TableRowStatus = true;
                                   }
                                   else {
                                       $scope.Credinfo[i].CredentialingContractRequests[j].TableRowStatus = false;
                                   }
                                   $scope.Credinfo[i].CredentialingContractRequests[j].credID = $scope.Credinfo[i].CredentialingInfoID;
                                   $scope.Credinfo[i].CredentialingContractRequests[j].PersonalDetail = $scope.Credinfo[i].Profile.PersonalDetail;
                                   $scope.Credinfo[i].CredentialingContractRequests[j].Plan = $scope.Credinfo[i].Plan;
                                   $scope.CredentialingContractRequests.push($scope.Credinfo[i].CredentialingContractRequests[j]);
                               }

                           }
                       }
                   }
               }
               $scope.ContractGridList = [];
               for (var c = 0; c < $scope.CredentialingContractRequests.length; c++) {
                   for (var d = 0; d < $scope.CredentialingContractRequests[c].ContractGrid.length; d++) {
                       $scope.ContractGridList.push($scope.CredentialingContractRequests[c].ContractGrid[d]);
                       $scope.ContractGridList[$scope.ContractGridList.length - 1].PersonalDetail = $scope.CredentialingContractRequests[c].PersonalDetail;
                       $scope.ContractGridList[$scope.ContractGridList.length - 1].Plan = $scope.CredentialingContractRequests[c].Plan;
                   }
               }


               // Provider Name,Plan,Provider ID, Panel Status,Plan Report Last Modified Date  



               $scope.ReArrange();

               //$scope.ProContractGridList = [];

               //for (var i = 0; i < $scope.ContractGridList.length; i++){
               //    if ($scope.ContractGridList[i].Report != null) {
               //        if ($scope.ContractGridList[i].Report.CredentialingApprovalStatus == "Approved") {
               //            $scope.ProContractGridList.push($scope.ContractGridList[i]);
               //            $scope.condition = 2;
               //            $scope.init_table($scope.ProContractGridList);
               //        }
               //    }
               //}



               $scope.CredentialingApprovalStatusType = ["Approved", "Rejected"];

               //$scope.ContractGridList = [];
               //$scope.condition = 1;

               $scope.init_table($scope.ContractGridList);

               $scope.loadingAjax = false;
           } catch (e) {
             
           }
       }).
       error(function (data, status, headers, config) {
           
       });

    $scope.ReArrange = function () {

        var newIndex = 0;
        $scope.todayDate = $filter('date')(new Date(Date.now()), 'dd/MM/yyyy');

        if ($scope.ContractGridList != null && $scope.ContractGridList.length != 0) {

            for (var i = 0; i < $scope.ContractGridList.length; i++) {

                //if ($scope.ContractGridList[i].Report != null) {

                //    if ($scope.ContractGridList[i].Report.TerminationDate != null) {

                //        $scope.myDate = $filter('date')(new Date($scope.ContractGridList[i].Report.TerminationDate), 'dd/MM/yyyy');

                //    }

                //    if ($scope.ContractGridList[i].Report.Status == "Inactive" || $scope.ContractGridList[i].Report.CredentialingApprovalStatusType == "2" || $scope.myDate < $scope.todayDate) {

                //        $scope.ContractGridList.splice($scope.ContractGridList.indexOf($scope.ContractGridList[i]), 1);
                //        i--;
                //        continue;

                //    }

                //}

                //if ($scope.ContractGridList[i].CredentialingInfo.CredentialingLogs != null) {

                //    if ($scope.ContractGridList[i].CredentialingInfo.CredentialingLogs.Credentialing == "ReCredentialing") {

                //        newIndex = i;

                //        for (var i = 0; i < newIndex; i++) {

                //            if ($scope.ContractGridList[i].CredentialingInfo.ProfileID == $scope.ContractGridList[newIndex].CredentialingInfo.ProfileID && $scope.ContractGridList[i].CredentialingInfo.PlanID == $scope.ContractGridList[newIndex].CredentialingInfo.PlanID) {

                //                $scope.ContractGridList.splice($scope.ContractGridList.indexOf($scope.ContractGridList[i]), 1);
                //                i--;
                //                continue;

                //            }

                //        }

                //    }

                //}

                if ($scope.ContractGridList[i].PersonalDetail != null) {

                    if ($scope.ContractGridList[i].PersonalDetail.FirstName != null) {

                        FirstName = $scope.ContractGridList[i].PersonalDetail.FirstName;

                    } else {

                        FirstName = "";

                    }

                    if ($scope.ContractGridList[i].PersonalDetail.MiddleName != null) {

                        MiddleName = $scope.ContractGridList[i].PersonalDetail.MiddleName;

                    } else {

                        MiddleName = "";

                    }

                    if ($scope.ContractGridList[i].PersonalDetail.LastName != null) {

                        LastName = $scope.ContractGridList[i].PersonalDetail.LastName;

                    } else {

                        LastName = "";

                    }

                }

                if ($scope.ContractGridList[i].CredentialingInfo != null) {

                    if ($scope.ContractGridList[i].CredentialingInfo.Profile != null) {

                        if ($scope.ContractGridList[i].CredentialingInfo.Profile.PersonalDetail != null) {

                            if ($scope.ContractGridList[i].CredentialingInfo.Profile.PersonalDetail.FirstName != null) {

                                FirstName = $scope.ContractGridList[i].CredentialingInfo.Profile.PersonalDetail.FirstName;

                            } else {

                                FirstName = "";

                            }

                            if ($scope.ContractGridList[i].CredentialingInfo.Profile.PersonalDetail.MiddleName != null) {

                                MiddleName = $scope.ContractGridList[i].CredentialingInfo.Profile.PersonalDetail.MiddleName;

                            } else {

                                MiddleName = "";

                            }

                            if ($scope.ContractGridList[i].CredentialingInfo.Profile.PersonalDetail.LastName != null) {

                                LastName = $scope.ContractGridList[i].CredentialingInfo.Profile.PersonalDetail.LastName;

                            } else {

                                LastName = "";

                            }

                        }
                    }

                }

                $scope.ContractGridList[i].InitialCredentialingDate = $scope.ConvertDateFormat($scope.ContractGridList[i].InitialCredentialingDate);
                if ($scope.ContractGridList[i].LastModifiedDate != null) {
                    $scope.ContractGridList[i].EffectiveDate = $scope.ConvertDateFormat($scope.ContractGridList[i].InitialCredentialingDate);
                } else {
                    $scope.ContractGridList[i].EffectiveDate = "";
                }
                if ($scope.ContractGridList[i].Report != null) {
                    $scope.ContractGridList[i].Report.InitiatedDate = $scope.ConvertDateFormat($scope.ContractGridList[i].Report.InitiatedDate);
                    if ($scope.ContractGridList[i].Report.InitiatedDate == "01-01-1970") {
                        $scope.ContractGridList[i].Report.InitiatedDate = null;
                    }
                    $scope.ContractGridList[i].Report.CredentialedDate = $scope.ConvertDateFormat($scope.ContractGridList[i].Report.CredentialedDate);
                    if ($scope.ContractGridList[i].Report.CredentialedDate == "01-01-1970") {
                        $scope.ContractGridList[i].Report.CredentialedDate = null;
                    }
                    //$scope.ContractGridList[i].Report.TerminationDate = $scope.ConvertDateFormat($scope.ContractGridList[i].Report.TerminationDate);
                    if ($scope.ContractGridList[i].Report.ReCredentialingDate != null) {
                        $scope.ContractGridList[i].Report.ReCredentialingDate = $scope.ConvertDateFormat($scope.ContractGridList[i].Report.ReCredentialingDate);
                    } else {
                        $scope.ContractGridList[i].Report.ReCredentialingDate = $scope.ConvertDateBy3Years($scope.ContractGridList[i].InitialCredentialingDate);
                    }

                }

                if ($scope.ContractGridList[i].CredentialingInfo != null) {

                    if ($scope.ContractGridList[i].CredentialingInfo.Profile != null) {

                        if ($scope.ContractGridList[i].CredentialingInfo.Profile.PersonalDetail != null) {

                            if ($scope.ContractGridList[i].CredentialingInfo.Profile.PersonalDetail.Salutation == "Not Available") {
                                Salutation = "";
                            } else {
                                Salutation = $scope.ContractGridList[i].CredentialingInfo.Profile.PersonalDetail.Salutation;
                            }

                            $scope.ContractGridList[i].Name = Salutation + " " + FirstName + " " + MiddleName + " " + LastName;

                        }

                    }

                }

                if ($scope.ContractGridList[i].PersonalDetail != null) {

                    if ($scope.ContractGridList[i].PersonalDetail.Salutation == "Not Available") {
                        Salutation = "";
                    } else {
                        Salutation = $scope.ContractGridList[i].PersonalDetail.Salutation;
                    }

                    $scope.ContractGridList[i].Name = Salutation + " " + FirstName + " " + MiddleName + " " + LastName;

                }

                if ($scope.ContractGridList[i].CredentialingInfo != null) {

                    if ($scope.ContractGridList[i].CredentialingInfo.Plan != null) {

                        $scope.ContractGridList[i].PlanName = $scope.ContractGridList[i].CredentialingInfo.Plan.PlanName;

                    }

                }

                if ($scope.ContractGridList[i].Plan != null) {

                    $scope.ContractGridList[i].PlanName = $scope.ContractGridList[i].Plan.PlanName;

                }

                if ($scope.ContractGridList[i].LOB != null) {

                    $scope.ContractGridList[i].LOBName = $scope.ContractGridList[i].LOB.LOBName;

                }

                if ($scope.ContractGridList[i].ProfilePracticeLocation != null && $scope.ContractGridList[i].ProfilePracticeLocation.Facility != null) {

                    if ($scope.ContractGridList[i].ProfilePracticeLocation.Facility.FacilityName != null) {

                        if ($scope.ContractGridList[i].ProfilePracticeLocation.Facility.Street != null) {

                            FacilityName = $scope.ContractGridList[i].ProfilePracticeLocation.Facility.FacilityName + ", ";

                        } else {

                            FacilityName = $scope.ContractGridList[i].ProfilePracticeLocation.Facility.FacilityName;

                        }

                    } else {

                        FacilityName = "";

                    }

                    if ($scope.ContractGridList[i].ProfilePracticeLocation.Facility.Street != null) {

                        if ($scope.ContractGridList[i].ProfilePracticeLocation.Facility.Building != null) {

                            Street = $scope.ContractGridList[i].ProfilePracticeLocation.Facility.Street + ", ";

                        } else {

                            Street = $scope.ContractGridList[i].ProfilePracticeLocation.Facility.Street;

                        }

                    } else {

                        Street = "";

                    }

                    if ($scope.ContractGridList[i].ProfilePracticeLocation.Facility.Building != null) {

                        Building = $scope.ContractGridList[i].ProfilePracticeLocation.Facility.Building;

                    } else {

                        Building = "";

                    }

                    $scope.ContractGridList[i].Location = FacilityName + " " + Street + " " + Building;

                    $scope.ContractGridList[i].Phone = $scope.ContractGridList[i].ProfilePracticeLocation.Facility.MobileNumber;

                }

                if ($scope.ContractGridList[i].Report != null) {
                    if ($scope.ContractGridList[i].Report.ProviderID != null) {
                        $scope.ContractGridList[i].ProviderID = $scope.ContractGridList[i].Report.ProviderID;
                    } else {
                        $scope.ContractGridList[i].ProviderID = "";
                    }
                    $scope.ContractGridList[i].PanelStatus = $scope.ContractGridList[i].Report.PanelStatus;
                }

                if (!$scope.isPanelChanged) {

                    if ($scope.ContractGridList[i].ContractGridHistory != null && $scope.ContractGridList[i].ContractGridHistory.length != 0) {

                        var index = $scope.ContractGridList[i].ContractGridHistory.length - 1;

                        $scope.isPanelChanged = true;
                        $scope.ContractGridList[i].panelChangedDate = $scope.ContractGridList[i].ContractGridHistory[index].LastModifiedDate;
                        $scope.ContractGridList[i].ReasonForPanelChange = $scope.ContractGridList[i].ContractGridHistory[index].ReasonForPanelChange;

                    }

                }

            }

        }

        $scope.data = $scope.ContractGridList;

    }

    $scope.setCredentialingApprovalStatus = function (status) {
        if (status == "Approved") {
            $scope.Details.Report.CredentialingApprovalStatusType = 1;
        } else if (status == "Rejected") {
            $scope.Details.Report.CredentialingApprovalStatusType = 2;
        } else {
            $scope.Details.Report.CredentialingApprovalStatusType = null;
        }
    }

    $scope.changePanelStatus = function () {
        $scope.isChange = true;
    }

    $scope.isPanelChanged = false;
    $scope.TempObj = {};

    $scope.SaveReport = function (c, Div) {
        var validationStatus = true;
        var url;
        var myData = {};
        var $formData;
        //  if ($scope.Visibility == ('editVisibility' + index)) {
        //Add Details - Denote the URL
        try {
            $formData = $('#ContractForm').find('form');
            url = rootDir + "/Credentialing/CnD/SaveReport";
        }
        catch (e)
        { };
        //    }
        ResetFormForValidation($formData);
        validationStatus = $formData.valid();
        if (validationStatus) {
            
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
                            data.dataContractGrid.InitialCredentialingDate = $scope.ConvertDteFormat(data.dataContractGrid.InitialCredentialingDate);
                            if (data.dataContractGrid.Report != null) {
                                data.dataContractGrid.Report.CredentialedDate = $scope.ConvertDteFormat(data.dataContractGrid.Report.CredentialedDate);
                                data.dataContractGrid.Report.InitiatedDate = $scope.ConvertDteFormat(data.dataContractGrid.Report.InitiatedDate);
                                data.dataContractGrid.Report.TerminationDate = $scope.ConvertDteFormat(data.dataContractGrid.Report.TerminationDate);
                                if (data.dataContractGrid.Report.ReCredentialingDate != null) {
                                    data.dataContractGrid.Report.ReCredentialingDate = $scope.ConvertDteFormat(data.dataContractGrid.Report.ReCredentialingDate);
                                } else {
                                    data.dataContractGrid.Report.ReCredentialingDate = $scope.ConvertDateBy3Years(data.dataContractGrid.InitialCredentialingDate);
                                }
                                data.dataContractGrid.CredentialingInfo.InitiationDate = $scope.ConvertDteFormat(data.dataContractGrid.CredentialingInfo.InitiationDate);
                                data.dataContractGrid.LastModifiedDate = $scope.ConvertDteFormat(data.dataContractGrid.LastModifiedDate);
                            }

                            if (data.dataContractGrid != null) {

                                $scope.Details = angular.copy(data.dataContractGrid);
                                $scope.ContractGridList[$scope.myIndex] = $scope.Details;

                            }

                            if ($scope.Details != null) {
                                if ($scope.Details.CredentialingInfo != null) {
                                    if ($scope.Details.CredentialingInfo.Plan != null) {
                                        $scope.Details.PlanName = $scope.Details.CredentialingInfo.Plan.PlanName;
                                    }
                                }
                            }

                            if ($scope.TempObj.ReasonForPanelChange != null) {

                                $scope.isPanelChanged = true;
                                $scope.Details.PanelChangedDate = $filter('date')(new Date(Date.now()), 'MM-dd-yyyy');
                                $scope.Details.ReasonForPanelChange = $scope.TempObj.ReasonForPanelChange;
                                $scope.TempObj.ReasonForPanelChange = "";
                            }

                            $scope.isPanelChanged = false;

                            $scope.getAddress();
                            $scope.getManager();

                            $scope.ReArrange();
                            $scope.tableParams1.reload();
                            $scope.updated = true;
                            $timeout(function () {
                                $scope.updated = false;
                            }, 5000);
                            //$scope.myDate = $filter('date')(new Date(data.dataContractGrid.Report.TerminationDate), 'dd/MM/yyyy');
                            //if (data.dataContractGrid.Report.CredentialingApprovalStatusType != "2" || $scope.myDate < $scope.todayDate) {
                            //    $scope.display = true;
                            //} else {
                            //    $scope.display = false;
                            //}
                            $scope.isChange = false;
                            $scope.display = true;
                            $scope.edit = false;
                            $scope.editMe = true;
                            //$scope.PlanReportList[index] = data.dataContractGrid;
                            //messageAlertEngine.callAlertMessage('ReportSaveSuccess' + index, "Plan Report Updated Successfully !!!", "success", true);
                            //$scope.SetVisibility('view', index);
                        }

                        else {
                            //$scope.SLError = data.status.split(",");
                            messageAlertEngine.callAlertMessage('ReportError', "", "danger", true);
                        }
                    }
                    catch (e) { };


                },
                error: function (e) {
                    try {
                        //$scope.SLError = "Sorry for Inconvenience !!!! Please Try Again Later...";
                        messageAlertEngine.callAlertMessage('StateLicenseError', "", "danger", true);
                    }
                    catch (e) { };

                }

            });
        }
    };

    $scope.RemoveReport = function (c) {

        $.ajax({
            url: rootDir + '/Credentialing/CnD/RemoveGrid?contractGridID=' + c.ContractGridID,
            type: 'POST',
            data: null,
            async: false,
            cache: false,
            contentType: false,
            processData: false,
            success: function (data) {
                
                try {
                    if (data.status == "true") {
                        //var obj = $filter('filter')($scope.ContractGridList, { ContractGridID: data.contractGridID })[0];
                        //$scope.ContractGridList.splice($scope.ContractGridList.indexOf(obj), 1);
                        $scope.data.splice($scope.data.indexOf($scope.data[$scope.myIndex]), 1);
                        $scope.tableParams1.reload();
                        $scope.IsDetails = false;
                        //$scope.ReArrange();
                        //$scope.PlanReportList.splice($scope.PlanReportList.indexOf(c), 1);
                        //  $scope.credentialingInfo.CredentialingContractRequests.splice($scope.credentialingInfo.CredentialingContractRequests.indexOf(c), 1);
                        $('#WarningModal').modal('hide');
                    }

                    else {

                    }
                }
                catch (e) { };


            },
            error: function (e) {
                try {
                    //$scope.SLError = "Sorry for Inconvenience !!!! Please Try Again Later...";
                    //messageAlertEngine.callAlertMessage('StateLicenseError', "", "danger", true);
                }
                catch (e) { };


            }

        });

    };

    //---------------- PDF Generate -----------------
    //---------Required Data for Generate to PDF ----------
    var columns = [
    { title: "Name", key: "Name" },
    { title: "Plan", key: "PlanName" },
    { title: "Location", key: "Location" },
    { title: "Provider ID", key: "ProviderID" },
    { title: "Panel Status", key: "PanelStatus" },
    { title: "Effective Date", key: "LastModifiedDate" }
    ];

    $scope.GeneratePDF = function () {

        var PDFData = angular.copy($scope.data);
        for (var i in PDFData) {
            //if (PDFData[i].LastModifiedDate instanceof Date) {
            //    PDFData[i].LastModifiedDate = ((PDFData[i].LastModifiedDate).getMonth()) + "/" + (PDFData[i].LastModifiedDate).getDate() + "/" + (PDFData[i].LastModifiedDate).getFullYear();
            //}
            PDFData[i].LastModifiedDate = $filter('date')(new Date(PDFData[i].LastModifiedDate), 'dd/MM/yyyy');
        }

        var doc = new jsPDF('p', 'pt');
        //doc.autoTable(columns, $scope.ProviderList, { overflowColumns: [] });
        doc.autoTable(columns, PDFData, {
            margins: { horizontal: 10, top: 40, bottom: 40 }, overflow: 'linebreak', overflowColumns: ['Name', 'Plan', 'Location', 'Provider ID', 'Panel Status', 'Effective Date']
        });
        doc.save('Contract_Grid_List_Report.pdf');
    };

    //$scope.ConvertDate = function (value) {
    //    var today = new Date(value);
    //    var dd = today.getDate();
    //    var mm = today.getMonth() + 1;
    //    var yyyy = today.getFullYear();
    //    if (dd < 10) { dd = '0' + dd }
    //    if (mm < 10) { mm = '0' + mm }
    //    var today = mm + '-' + dd + '-' + yyyy;
    //    return today;
    //};

    $scope.ConvertDateFormat = function (value) {
        var today = new Date(value);
        var dd = today.getDate();
        var mm = today.getMonth() + 1;
        var yyyy = today.getFullYear();
        if (dd < 10) { dd = '0' + dd }
        if (mm < 10) { mm = '0' + mm }
        var today = mm + '-' + dd + '-' + yyyy;
        return today;
    };

    $scope.ConvertDteFormat = function (value) {
      
        var returnValue = value;
        try {
            if (value.indexOf("/Date(") == 0) {
                returnValue = new Date(parseInt(value.replace("/Date(", "").replace(")/", ""), 10));
            }
            return returnValue;
        } catch (e) {
            return returnValue;
        }
        return returnValue;
    };

    $scope.ConvertDateBy3Years = function (date) {
        var dt = new Date(date);
        var month = dt.getMonth() + 1;
        var monthString = month > 9 ? month : '0' + month;
        //var monthName = monthNames[month];
        var day = dt.getDate();
        var dayString = day > 9 ? day : '0' + day;
        var year = dt.getFullYear() + 3;
        shortDate = monthString + '/' + dayString + '/' + year;
        return shortDate;
    }

    $scope.initWarning = function (c, i) {

        if (c != null) {

            $scope.tempRemoveReportData = angular.copy(c);

        }
        $scope.myIndex = $scope.data.indexOf(c);
        $('#WarningModal').modal();
    };

    //$scope.ContractInformation = [
    //    {
    //        Name: "Mukesh Satodiya",
    //        Title: "Medical Doctor",
    //        Plan: "Wellcare-Medicare",
    //        Location: "402 Lake In the Woods",
    //        ProviderID: 101,
    //        CredentialingStatus: "Approved",
    //        EffectiveDate: "10/08/2015",
    //        LocationDetails: {
    //            Address: "402 Lake In the Woods",
    //            Plan: "Wellcare-Medicare",
    //            BE: ["Access", "Access2"],
    //            LOB: ["Medicare", "Medicaid", "Commercial Exchange"],
    //            Speciality: ["Internal Medicine"],
    //            GroupID: 2,
    //            ProviderID: 101,
    //            PanelStatus: "Open",
    //            Phone: "+1-9938395826",
    //            Fax: "+1-9938395834",
    //            OfficeManager:"Laura Lofsten"
    //        },
    //        FeeDetails: {
    //            FeeSchedule: "Aetna PPO FS A1",
    //            EffectiveDate: "10/08/2015",
    //            TermDate: "26/12/2015",
    //            SupportingDocument:true
    //        },
    //        OtherDetails: {
    //            InitiatedDate: "14/08/2015",
    //            InitialCredentialingDate: "26/10/2015",
    //            RecredentialingDate: "26/10/2018",
    //            WelcomeLetterMailedDate: "29/10/2018",
    //            WelcomeLetterDoc:true
    //        },
    //        ViewStatus: false,
    //        EditStatus: false
    //    },
    //    {
    //        Name: "Usha Agrawal",
    //        Title: "certified Registered Nurse",
    //        Plan: "Humana",
    //        Location: "482 Lake In the Woods",
    //        ProviderID: 102,
    //        CredentialingStatus: "Pending",
    //        EffectiveDate: "20/08/2015",
    //        LocationDetails: {
    //            Address: "482 Lake In the Woods",
    //            Plan: "Humana",
    //            BE: ["Access"],
    //            LOB: ["Medicare", "Medicaid", "Commercial Exchange"],
    //            Speciality: ["Internal Medicine"],
    //            GroupID: 2,
    //            ProviderID: 102,
    //            PanelStatus: "Open",
    //            Phone: "+1-9938395826",
    //            Fax: "+1-9938395834",
    //            OfficeManager: "Laura Lofsten"
    //        },
    //        FeeDetails: {
    //            FeeSchedule: "Aetna PPO FS A1",
    //            EffectiveDate: "20/08/2015",
    //            TermDate: "26/12/2016",
    //            SupportingDocument: true
    //        },
    //        OtherDetails: {
    //            InitiatedDate: "14/08/2015",
    //            InitialCredentialingDate: "26/10/2015",
    //            RecredentialingDate: "26/10/2018",
    //            WelcomeLetterMailedDate: "29/10/2018",
    //            WelcomeLetterDoc: true
    //        },
    //        ViewStatus: false,
    //        EditStatus: false
    //    },
    //    {
    //        Name: "Prakash Rana",
    //        Title: "Medical Doctor",
    //        Plan: "Ultimate",
    //        Location: "Tampa-4008 North America",
    //        ProviderID: 193,
    //        CredentialingStatus: "Approved",
    //        EffectiveDate: "10/08/2016",
    //        LocationDetails: {
    //            Address: "Tampa-4008 North America",
    //            Plan: "Ultimate",
    //            BE: ["Access"],
    //            LOB: ["Medicare", "Medicaid", "Commercial Exchange"],
    //            Speciality: ["Internal Medicine"],
    //            GroupID: 2,
    //            ProviderID: 193,
    //            PanelStatus: "Open",
    //            Phone: "+1-9938395826",
    //            Fax: "+1-9938395834",
    //            OfficeManager: "Laura Lofsten"
    //        },
    //        FeeDetails: {
    //            FeeSchedule: "Aetna PPO FS A1",
    //            EffectiveDate: "10/08/2016",
    //            TermDate: "26/12/2016",
    //            SupportingDocument: true
    //        },
    //        OtherDetails: {
    //            InitiatedDate: "14/08/2015",
    //            InitialCredentialingDate: "26/10/2015",
    //            RecredentialingDate: "26/10/2018",
    //            WelcomeLetterMailedDate: "29/10/2018",
    //            WelcomeLetterDoc: true
    //        },
    //        ViewStatus: false,
    //        EditStatus: false
    //    },
    //    {
    //        Name: "Monica Singh",
    //        Title: "certified Registered Nurse",
    //        Plan: "Wellcare-Medicare",
    //        Location: "456 Lake In the Woods",
    //        ProviderID: 221,
    //        CredentialingStatus: "Pending",
    //        EffectiveDate: "10/12/2015",
    //        LocationDetails: {
    //            Address: "456 Lake In the Woods",
    //            Plan: "Wellcare-Medicare",
    //            BE: ["Access"],
    //            LOB: ["Medicare", "Medicaid", "Commercial Exchange"],
    //            Speciality: ["Internal Medicine"],
    //            GroupID: 2,
    //            ProviderID: 221,
    //            PanelStatus: "Close",
    //            Phone: "+1-9938395826",
    //            Fax: "+1-9938395834",
    //            OfficeManager: "Laura Lofsten"
    //        },
    //        FeeDetails: {
    //            FeeSchedule: "Aetna PPO FS A1",
    //            EffectiveDate: "10/12/2015",
    //            TermDate: "26/12/2015",
    //            SupportingDocument: false
    //        },
    //        OtherDetails: {
    //            InitiatedDate: "14/08/2015",
    //            InitialCredentialingDate: "26/10/2015",
    //            RecredentialingDate: "26/10/2018",
    //            WelcomeLetterMailedDate: "29/10/2018",
    //            WelcomeLetterDoc: true
    //        },
    //        ViewStatus: false,
    //        EditStatus: false
    //    },
    //    {
    //        Name: "Jude A Pierre",
    //        Title: "Medical Doctor",
    //        Plan: "Humana",
    //        Location: "Tampa-4144 North America,Tampa Florida",
    //        ProviderID: 105,
    //        CredentialingStatus: "Approved",
    //        EffectiveDate: "19/08/2015",
    //        LocationDetails: {
    //            Address: "Tampa-4144 North America,Tampa Florida",
    //            Plan: "Humana",
    //            BE: ["Access"],
    //            LOB: ["Medicare", "Medicaid", "Commercial Exchange"],
    //            Speciality: ["Internal Medicine"],
    //            GroupID: 2,
    //            ProviderID: 105,
    //            PanelStatus: "Open",
    //            Phone: "+1-9938395826",
    //            Fax: "+1-9938395834",
    //            OfficeManager: "Laura Lofsten"
    //        },
    //        FeeDetails: {
    //            FeeSchedule: "Aetna PPO FS A1",
    //            EffectiveDate: "19/08/2015",
    //            TermDate: "26/12/2015",
    //            SupportingDocument: true
    //        },
    //        OtherDetails: {
    //            InitiatedDate: "14/08/2015",
    //            InitialCredentialingDate: "26/10/2015",
    //            RecredentialingDate: "26/10/2018",
    //            WelcomeLetterMailedDate: "29/10/2018",
    //            WelcomeLetterDoc: true
    //        },
    //        ViewStatus: false,
    //        EditStatus: false
    //    },
    //];
    $scope.removeContractInfo = function (data) {
        var id = $scope.data.indexOf(data);
        if (id > -1) {
            $scope.data.splice(id, 1);
            $scope.tableParams1.reload();
        }
        $scope.isChange = false;
        $scope.TempObj.ReasonForPanelChange = "";
    }
    $scope.tempvariable = '';
    $scope.viewContractInfo = function (data, index) {
        for (var i = 0; i < $scope.data.length; i++) {
            if ($scope.data[i] != data) {
                $scope.data[i].ViewStatus = false;
            }
        }
        $scope.tableParams1.reload();
        data.ViewStatus = !data.ViewStatus;
        $scope.editMe = true;
        $scope.display = true;
        $scope.edit = false;
        $scope.isChange = false;
        $scope.Details = angular.copy(data);
        $scope.tempvariable = data.Report.CredentialingApprovalStatusType;
        $scope.getAddress();
        $scope.getManager();
        $scope.IsDetails = true;
        $scope.myIndex = $scope.data.indexOf(data);
        $scope.TempObj.ReasonForPanelChange = "";
    }

    $scope.getAddress = function () {

        if ($scope.Details.ProfilePracticeLocation.Facility != null) {

            if ($scope.Details.ProfilePracticeLocation.Facility.FacilityName != null) {

                if ($scope.Details.ProfilePracticeLocation.Facility.Street != null) {

                    FacilityName = $scope.Details.ProfilePracticeLocation.Facility.FacilityName + ", ";

                } else {

                    FacilityName = $scope.Details.ProfilePracticeLocation.Facility.FacilityName;

                }

            } else {

                FacilityName = "";

            }

            if ($scope.Details.ProfilePracticeLocation.Facility.Building != null) {

                if ($scope.Details.ProfilePracticeLocation.Facility.Street != null) {

                    Building = $scope.Details.ProfilePracticeLocation.Facility.Building + ", ";

                } else {

                    Building = $scope.Details.ProfilePracticeLocation.Facility.Building;

                }

            } else {

                Building = "";

            }

            if ($scope.Details.ProfilePracticeLocation.Facility.Street != null) {

                if ($scope.Details.ProfilePracticeLocation.Facility.Country != null) {

                    Street = $scope.Details.ProfilePracticeLocation.Facility.Street + ", ";

                } else {

                    Street = $scope.Details.ProfilePracticeLocation.Facility.Street;

                }

            } else {

                Street = "";

            }

            if ($scope.Details.ProfilePracticeLocation.Facility.Country != null) {

                if ($scope.Details.ProfilePracticeLocation.Facility.State != null) {

                    Country = $scope.Details.ProfilePracticeLocation.Facility.Country + ", ";

                } else {

                    Country = $scope.Details.ProfilePracticeLocation.Facility.Country;

                }

            } else {

                Country = "";

            }

            if ($scope.Details.ProfilePracticeLocation.Facility.State != null) {

                State = $scope.Details.ProfilePracticeLocation.Facility.State;

            } else {

                State = "";

            }

            if ($scope.Details.ProfilePracticeLocation.Facility.County != null) {

                County = ", " + $scope.Details.ProfilePracticeLocation.Facility.County;

            } else {

                County = "";

            }

            if ($scope.Details.ProfilePracticeLocation.Facility.City != null) {

                City = ", " + $scope.Details.ProfilePracticeLocation.Facility.City;

            } else {

                City = "";

            }

            if ($scope.Details.ProfilePracticeLocation.Facility.ZipCode != null) {

                ZipCode = ", " + $scope.Details.ProfilePracticeLocation.Facility.ZipCode;

            } else {

                ZipCode = "";

            }

            $scope.Details.Address = FacilityName + Building + Street + Country + State;

        }

    }

     $scope.getManager = function () {

        if ($scope.Details != null && $scope.Details.ProfilePracticeLocation != null && $scope.Details.ProfilePracticeLocation.BusinessOfficeManagerOrStaff != null) {

            if ($scope.Details.ProfilePracticeLocation.BusinessOfficeManagerOrStaff.FirstName != null) {

                FirstName = $scope.Details.ProfilePracticeLocation.BusinessOfficeManagerOrStaff.FirstName;

            } else {

                FirstName = "";

            }

            if ($scope.Details.ProfilePracticeLocation.BusinessOfficeManagerOrStaff.MiddleName != null) {

                MiddleName = $scope.Details.ProfilePracticeLocation.BusinessOfficeManagerOrStaff.MiddleName;

            } else {

                MiddleName = "";

            }

            if ($scope.Details.ProfilePracticeLocation.BusinessOfficeManagerOrStaff.LastName != null) {

                LastName = $scope.Details.ProfilePracticeLocation.BusinessOfficeManagerOrStaff.LastName;

            } else {

                LastName = "";

            }

            $scope.Details.OfficeManager = FirstName + " " + MiddleName + " " + LastName;

        }

    }

    $scope.editContractInfo = function (data, index) {
        $('#selectstatus').val($scope.tempvariable).attr('selected', 'selected');
        if (data != null) {
            $scope.myIndex = $scope.data.indexOf(data);
        }

        $scope.editMe = false;
        $scope.IsDetails = true;
        $scope.display = false;
        $scope.edit = true;
        $scope.isChange = false;
        $scope.Details = angular.copy($scope.data[$scope.myIndex]);
        $scope.getAddress();
        $scope.getManager();
        $scope.TempObj.ReasonForPanelChange = "";
    }

   

    $scope.closeContractInfo = function () {

        $scope.IsDetails = false;
        $scope.isChange = false;
        $scope.TempObj.ReasonForPanelChange = "";
    }


    $scope.currentPage = 0;
    $scope.currentCount = 0;
    $scope.params = null;

    //Created function to be called when data loaded dynamically
    $scope.init_table = function (data) {

        $scope.data = data;
        var counts = [];

        if ($scope.data.length <= 10) {
            counts = [];
        }
        else if ($scope.data.length <= 25) {
            counts = [10, 25];
        }
        else if ($scope.data.length <= 50) {
            counts = [10, 25, 50];
        }
        else if ($scope.data.length <= 100) {
            counts = [10, 25, 50, 100];
        }
        else if ($scope.data.length > 100) {
            counts = [10, 25, 50, 100];
        }

        $scope.tableParams1 = new ngTableParams({
            page: 1,            // show first page
            count: 10,          // count per page
            filter: {
                //name: 'M'       // initial filter
                //FirstName : ''
            },
            sorting: {
                //Name: 'desc'
                //name: 'asc'     // initial sorting
            }
        }, {
            counts: counts,
            total: $scope.data.length, // length of data
            getData: function ($defer, params) {
                // use build-in angular filter
                var filteredData = params.filter() ?
                        $filter('filter')($scope.data, params.filter()) :
                        $scope.data;
                var orderedData = params.sorting() ?
                        $filter('orderBy')(filteredData, params.orderBy()) :
                        $scope.data;

                params.total(orderedData.length); // set total for recalc pagination
                $scope.currentPage = params.page();
                $scope.currentCount = params.count();
                $scope.params = params;
                $defer.resolve(orderedData);
            }
        });

    };

    $scope.IsValidIndex = function (index) {

        if (index >= (($scope.currentPage - 1) * $scope.currentCount) && index < ($scope.currentPage * $scope.currentCount))
            return true;
        else
            return false;
    }

    $scope.filterData = function () {
        $scope.params.page(1);
    }


    //if filter is on
    $scope.ifFilter = function () {
        try {
            var bar;
            var obj = $scope.tableParams1.$params.filter;
            for (bar in obj) {
                if (obj[bar] != "") {
                    return false;
                }
            }
            return true;
        }
        catch (e) { return true; }
    }

    //Get index first in table
    $scope.getIndexFirst = function () {
        try {
            if ($scope.groupBySelected == 'none') {
                return ($scope.tableParams1.$params.page * $scope.tableParams1.$params.count) - ($scope.tableParams1.$params.count - 1);
            }
        }
        catch (e) { }
    }

    //Get index Last in table
    $scope.getIndexLast = function () {
        try {
            if ($scope.groupBySelected == 'none') {
                return { true: ($scope.data.length), false: ($scope.tableParams1.$params.page * $scope.tableParams1.$params.count) }[(($scope.tableParams1.$params.page * $scope.tableParams1.$params.count) > ($scope.data.length))];
            }
        }
        catch (e) { }
    }

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