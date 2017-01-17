
//------------------------------- Provider License Service --------------------
dashboardApp.service('CCOLicenseService', function ($filter) {

    var ProviderState = [];
    var ProviderDEA = [];
    var ProviderCDS = [];
    var ProviderSpeciality = [];
    var ProviderHospital = [];
    var ProviderLiability = [];
    var ProviderWorker = [];


    //---------------- parse data in license have Licenses with calculate Left Day ----------------------
    for (var i in expiredLicenses) {
        if (expiredLicenses[i].StateLicenseExpiries && expiredLicenses[i].StateLicenseExpiries.length > 0) {
            for (var j in expiredLicenses[i].StateLicenseExpiries) {
                expiredLicenses[i].StateLicenseExpiries[j].ExpiryDate = ConvertDateFormat(expiredLicenses[i].StateLicenseExpiries[j].ExpiryDate);
                expiredLicenses[i].StateLicenseExpiries[j].dayLeft = GetRenewalDayLeft(expiredLicenses[i].StateLicenseExpiries[j].ExpiryDate);

                ProviderState.push({
                    EmailAddress: expiredLicenses[i].EmailAddress,
                    ExpiryNotificationDetailID: expiredLicenses[i].ExpiryNotificationDetailID,
                    FirstName: expiredLicenses[i].FirstName,
                    LastName: expiredLicenses[i].LastName,
                    MiddleName: expiredLicenses[i].MiddleName,
                    LastModifiedDate: expiredLicenses[i].LastModifiedDate,
                    NPINumber: expiredLicenses[i].NPINumber,
                    ProfileId: expiredLicenses[i].ProfileId,
                    ProviderLevel: expiredLicenses[i].ProviderLevel,
                    ProviderTitles: expiredLicenses[i].ProviderTitles,
                    ProviderTypes: expiredLicenses[i].ProviderTypes,
                    License: expiredLicenses[i].StateLicenseExpiries[j]
                });
            }
        }

        if (expiredLicenses[i].DEALicenseExpiries && expiredLicenses[i].DEALicenseExpiries.length > 0) {
            for (var j in expiredLicenses[i].DEALicenseExpiries) {
                expiredLicenses[i].DEALicenseExpiries[j].ExpiryDate = ConvertDateFormat(expiredLicenses[i].DEALicenseExpiries[j].ExpiryDate);
                expiredLicenses[i].DEALicenseExpiries[j].dayLeft = GetRenewalDayLeft(expiredLicenses[i].DEALicenseExpiries[j].ExpiryDate);

                ProviderDEA.push({
                    EmailAddress: expiredLicenses[i].EmailAddress,
                    ExpiryNotificationDetailID: expiredLicenses[i].ExpiryNotificationDetailID,
                    FirstName: expiredLicenses[i].FirstName,
                    LastName: expiredLicenses[i].LastName,
                    MiddleName: expiredLicenses[i].MiddleName,
                    LastModifiedDate: expiredLicenses[i].LastModifiedDate,
                    NPINumber: expiredLicenses[i].NPINumber,
                    ProfileId: expiredLicenses[i].ProfileId,
                    ProviderLevel: expiredLicenses[i].ProviderLevel,
                    ProviderTitles: expiredLicenses[i].ProviderTitles,
                    ProviderTypes: expiredLicenses[i].ProviderTypes,
                    License: expiredLicenses[i].DEALicenseExpiries[j]
                });
            }
        }

        if (expiredLicenses[i].CDSCInfoExpiries && expiredLicenses[i].CDSCInfoExpiries.length > 0) {
            for (var j in expiredLicenses[i].CDSCInfoExpiries) {
                expiredLicenses[i].CDSCInfoExpiries[j].ExpiryDate = ConvertDateFormat(expiredLicenses[i].CDSCInfoExpiries[j].ExpiryDate);
                expiredLicenses[i].CDSCInfoExpiries[j].dayLeft = GetRenewalDayLeft(expiredLicenses[i].CDSCInfoExpiries[j].ExpiryDate);

                ProviderCDS.push({
                    EmailAddress: expiredLicenses[i].EmailAddress,
                    ExpiryNotificationDetailID: expiredLicenses[i].ExpiryNotificationDetailID,
                    FirstName: expiredLicenses[i].FirstName,
                    LastName: expiredLicenses[i].LastName,
                    MiddleName: expiredLicenses[i].MiddleName,
                    LastModifiedDate: expiredLicenses[i].LastModifiedDate,
                    NPINumber: expiredLicenses[i].NPINumber,
                    ProfileId: expiredLicenses[i].ProfileId,
                    ProviderLevel: expiredLicenses[i].ProviderLevel,
                    ProviderTitles: expiredLicenses[i].ProviderTitles,
                    ProviderTypes: expiredLicenses[i].ProviderTypes,
                    License: expiredLicenses[i].CDSCInfoExpiries[j]
                });
            }
        }

        if (expiredLicenses[i].SpecialtyDetailExpiries && expiredLicenses[i].SpecialtyDetailExpiries.length > 0) {
            for (var j in expiredLicenses[i].SpecialtyDetailExpiries) {
                expiredLicenses[i].SpecialtyDetailExpiries[j].ExpiryDate = ConvertDateFormat(expiredLicenses[i].SpecialtyDetailExpiries[j].ExpiryDate);
                expiredLicenses[i].SpecialtyDetailExpiries[j].dayLeft = GetRenewalDayLeft(expiredLicenses[i].SpecialtyDetailExpiries[j].ExpiryDate);

                ProviderSpeciality.push({
                    EmailAddress: expiredLicenses[i].EmailAddress,
                    ExpiryNotificationDetailID: expiredLicenses[i].ExpiryNotificationDetailID,
                    FirstName: expiredLicenses[i].FirstName,
                    LastName: expiredLicenses[i].LastName,
                    MiddleName: expiredLicenses[i].MiddleName,
                    LastModifiedDate: expiredLicenses[i].LastModifiedDate,
                    NPINumber: expiredLicenses[i].NPINumber,
                    ProfileId: expiredLicenses[i].ProfileId,
                    ProviderLevel: expiredLicenses[i].ProviderLevel,
                    ProviderTitles: expiredLicenses[i].ProviderTitles,
                    ProviderTypes: expiredLicenses[i].ProviderTypes,
                    License: expiredLicenses[i].SpecialtyDetailExpiries[j]
                });
            }
        }

        if (expiredLicenses[i].HospitalPrivilegeExpiries && expiredLicenses[i].HospitalPrivilegeExpiries.length > 0) {
            for (var j in expiredLicenses[i].HospitalPrivilegeExpiries) {
                expiredLicenses[i].HospitalPrivilegeExpiries[j].AffiliationEndDate = ConvertDateFormat(expiredLicenses[i].HospitalPrivilegeExpiries[j].AffiliationEndDate);
                expiredLicenses[i].HospitalPrivilegeExpiries[j].dayLeft = GetRenewalDayLeft(expiredLicenses[i].HospitalPrivilegeExpiries[j].AffiliationEndDate);

                ProviderHospital.push({
                    EmailAddress: expiredLicenses[i].EmailAddress,
                    ExpiryNotificationDetailID: expiredLicenses[i].ExpiryNotificationDetailID,
                    FirstName: expiredLicenses[i].FirstName,
                    LastName: expiredLicenses[i].LastName,
                    MiddleName: expiredLicenses[i].MiddleName,
                    LastModifiedDate: expiredLicenses[i].LastModifiedDate,
                    NPINumber: expiredLicenses[i].NPINumber,
                    ProfileId: expiredLicenses[i].ProfileId,
                    ProviderLevel: expiredLicenses[i].ProviderLevel,
                    ProviderTitles: expiredLicenses[i].ProviderTitles,
                    ProviderTypes: expiredLicenses[i].ProviderTypes,
                    License: expiredLicenses[i].HospitalPrivilegeExpiries[j]
                });
            }
        }

        if (expiredLicenses[i].ProfessionalLiabilityExpiries && expiredLicenses[i].ProfessionalLiabilityExpiries.length > 0) {
            for (var j in expiredLicenses[i].ProfessionalLiabilityExpiries) {
                expiredLicenses[i].ProfessionalLiabilityExpiries[j].ExpirationDate = ConvertDateFormat(expiredLicenses[i].ProfessionalLiabilityExpiries[j].ExpirationDate);
                expiredLicenses[i].ProfessionalLiabilityExpiries[j].dayLeft = GetRenewalDayLeft(expiredLicenses[i].ProfessionalLiabilityExpiries[j].ExpirationDate);

                ProviderLiability.push({
                    EmailAddress: expiredLicenses[i].EmailAddress,
                    ExpiryNotificationDetailID: expiredLicenses[i].ExpiryNotificationDetailID,
                    FirstName: expiredLicenses[i].FirstName,
                    LastName: expiredLicenses[i].LastName,
                    MiddleName: expiredLicenses[i].MiddleName,
                    LastModifiedDate: expiredLicenses[i].LastModifiedDate,
                    NPINumber: expiredLicenses[i].NPINumber,
                    ProfileId: expiredLicenses[i].ProfileId,
                    ProviderLevel: expiredLicenses[i].ProviderLevel,
                    ProviderTitles: expiredLicenses[i].ProviderTitles,
                    ProviderTypes: expiredLicenses[i].ProviderTypes,
                    License: expiredLicenses[i].ProfessionalLiabilityExpiries[j]
                });
            }
        }

        if (expiredLicenses[i].WorkerCompensationExpiries && expiredLicenses[i].WorkerCompensationExpiries.length > 0) {
            for (var j in expiredLicenses[i].WorkerCompensationExpiries) {
                expiredLicenses[i].WorkerCompensationExpiries[j].ExpirationDate = ConvertDateFormat(expiredLicenses[i].WorkerCompensationExpiries[j].ExpirationDate);
                expiredLicenses[i].WorkerCompensationExpiries[j].dayLeft = GetRenewalDayLeft(expiredLicenses[i].WorkerCompensationExpiries[j].ExpirationDate);

                ProviderWorker.push({
                    EmailAddress: expiredLicenses[i].EmailAddress,
                    ExpiryNotificationDetailID: expiredLicenses[i].ExpiryNotificationDetailID,
                    FirstName: expiredLicenses[i].FirstName,
                    LastName: expiredLicenses[i].LastName,
                    MiddleName: expiredLicenses[i].MiddleName,
                    LastModifiedDate: expiredLicenses[i].LastModifiedDate,
                    NPINumber: expiredLicenses[i].NPINumber,
                    ProfileId: expiredLicenses[i].ProfileId,
                    ProviderLevel: expiredLicenses[i].ProviderLevel,
                    ProviderTitles: expiredLicenses[i].ProviderTitles,
                    ProviderTypes: expiredLicenses[i].ProviderTypes,
                    License: expiredLicenses[i].WorkerCompensationExpiries[j]
                });
            }
        }
    }

    //-------------------------- Customm array parse ---------------------
    var LicenseData = [];
    var GrandTotalLicenses = 0;

    if (ProviderState) {
        LicenseData.push({
            LicenseType: "State License",
            LicenseTypeCode: "StateLicense",
            Licenses: ProviderState,
            Show: true
        });
    }
    if (ProviderDEA) {
        LicenseData.push({
            LicenseType: "Federal DEA",
            LicenseTypeCode: "FederalDEA",
            Licenses: ProviderDEA,
            Show: true
        });
    }
    if (ProviderCDS) {
        LicenseData.push({
            LicenseType: "CDS Information",
            LicenseTypeCode: "CDSInformation",
            Licenses: ProviderCDS,
            Show: true
        });
    }
    if (ProviderSpeciality) {
        LicenseData.push({
            LicenseType: "Specialty/Board",
            LicenseTypeCode: "SpecialityBoard",
            Licenses: ProviderSpeciality,
            Show: true
        });
    }
    if (ProviderHospital) {
        LicenseData.push({
            LicenseType: "Hospital Privileges",
            LicenseTypeCode: "HospitalPrivilages",
            Licenses: ProviderHospital,
            Show: true
        });
    }
    if (ProviderLiability) {
        LicenseData.push({
            LicenseType: "Professional Liability",
            LicenseTypeCode: "ProfessionalLiability",
            Licenses: ProviderLiability,
            Show: true
        });
    }
    if (ProviderWorker) {
        LicenseData.push({
            LicenseType: "Worker Compensation",
            LicenseTypeCode: "WorkerCompensation",
            Licenses: ProviderWorker,
            Show: true
        });
    }

    //--------------- Master license Data for Static Data ---------------
    var MasterLicenseData = angular.copy(LicenseData);
    //---------------------- license status return -------------------
    this.GetLicenseStatus = function (data) {
        GrandTotalLicenses = 0;

        for (var i in data) {

            var ValidatedLicense = 0;
            var dayLeftLicense = 0;
            var ExpiredLicense = 0;

            var TotalLicenses = 0;

            for (var j in data[i].Licenses) {
                TotalLicenses++;
                if (data[i].Licenses[j].License.dayLeft < 0) {
                    ExpiredLicense++;
                    GrandTotalLicenses++;
                } else if (data[i].Licenses[j].License.dayLeft < 90) {
                    dayLeftLicense++;
                    GrandTotalLicenses++;
                }
                else if (data[i].Licenses[j].License.dayLeft < 180) {
                    ValidatedLicense++;
                    GrandTotalLicenses++;
                }
            }

            var orderBy = $filter('orderBy');
            data[i].Licenses = orderBy(data[i].Licenses, 'License.dayLeft', false);

            data[i].LicenseStatus = {
                ValidLicense: ValidatedLicense,
                PendingDaylicense: dayLeftLicense,
                ExpiredLicense: ExpiredLicense
            };
            data[i].TotalLicenses = TotalLicenses;
        }
    };

    //------------------ Grand Total Number of License return ---------------------
    this.GetGrandTotalLicenses = function () {
        return GrandTotalLicenses;
    };

    //----------------- simply return Licese List ---------------
    this.LicensesList = function () {
        this.GetLicenseStatus(LicenseData);
        return LicenseData;
    };
    //----------------- License by day lefts ------------------------
    this.GetLicenseByDayLeft = function (days) {
        if (days > 0) {
            var temp = [];
            for (var i in MasterLicenseData) {
                var licenses = [];
                for (var j in MasterLicenseData[i].Licenses) {
                    if (MasterLicenseData[i].Licenses[j].License.dayLeft <= days) {
                        licenses.push(MasterLicenseData[i].Licenses[j]);
                    }
                }
                temp.push({ LicenseType: MasterLicenseData[i].LicenseType, LicenseTypeCode: MasterLicenseData[i].LicenseTypeCode, Licenses: licenses });
            }
            this.GetLicenseStatus(temp);
            return temp
        }
        else {
            this.GetLicenseStatus(MasterLicenseData);
            return angular.copy(MasterLicenseData);
        }
    };

    //----------------- License by Provider Types lefts ------------------------
    this.GetLicenseByProviderType = function (providertype, providerlevel, masterLicenses) {
        //--------------- provider type undefined -----------
        if (typeof providertype === "undefined") {
            providertype = "";
        }
        //--------------- provider Level undefined -----------
        if (typeof providerlevel === "undefined") {
            providerlevel = "";
        }
        if (providertype != "" || providerlevel != "") {
            if (providertype != "" && providerlevel != "") {
                var temp = [];
                for (var i in masterLicenses) {
                    var status = false;
                    for (var j in masterLicenses[i].ProviderTitles) {
                        if (masterLicenses[i].ProviderTitles[j] == providertype && masterLicenses[i].ProviderLevel == providerlevel) {
                            status = true;
                        }
                    }
                    if (status) {
                        temp.push(masterLicenses[i]);
                    }
                }
                return temp;
            } else if (providertype != "" && providerlevel == "") {
                var temp = [];
                for (var i in masterLicenses) {
                    var status = false;
                    for (var j in masterLicenses[i].ProviderTitles) {
                        if (masterLicenses[i].ProviderTitles[j] == providertype) {
                            status = true;
                        }
                    }
                    if (status) {
                        temp.push(masterLicenses[i]);
                    }
                }
                return temp;
            } else if (providertype == "" && providerlevel != "") {
                var temp = [];
                for (var i in masterLicenses) {
                    if (masterLicenses[i].ProviderLevel == providerlevel) {
                        temp.push(masterLicenses[i]);
                    }
                }
                return temp;
            }
        }
        else {
            return masterLicenses;
        }
    };
});

//------------------- CCO Dashboard Controller --------------------
dashboardApp.controller("CCODashboardController", ["$scope", "$http", "$filter", "CCOLicenseService", function ($scope, $http, $filter, CCOLicenseService) {

    

    $scope.printData = function (str) {

        if (str == 'All') {
            $scope.printAllPagesDataById($scope.LicenseType);
        }
        else if (str == 'Current') {
            $scope.printCurrentPageDataById($scope.LicenseType);
        }
    }

    $scope.tempObj = [];

    $scope.newSelection = function (index, status) {

        var present = false;

        if ($scope.tempObj != null && $scope.tempObj.length != 0) {

            for (var i = 0; i < $scope.tempObj.length; i++) {

                if ($scope.tempObj[i] == index) {

                    present = true;
                    break;

                }

            }

        }

        if (!present) {

            $scope.tempObj.push(index);

        } else {

            $scope.tempObj.splice($scope.tempObj.indexOf(index), 1);

        }

    }

    $scope.saveIt = function () {

        //sessionStorage.setItem('myTabs', JSON.stringify($scope.LicenseData));
        $scope.tempObj = [];
        $scope.controlpanel = false;

    }

    $scope.revertIt = function () {

        if ($scope.tempObj != null && $scope.tempObj.length != 0) {

            for (var i = 0; i < $scope.tempObj.length; i++) {

                if ($scope.LicenseData[$scope.tempObj[i]].Show == true) {

                    $scope.LicenseData[$scope.tempObj[i]].Show = false;

                } else {

                    $scope.LicenseData[$scope.tempObj[i]].Show = true;

                }

            }

        }

        $scope.tempObj = [];

        $scope.controlpanel = false;

        //for (var i = 0; i < $scope.LicenseData.length; i++) {

        //    $scope.LicenseData[i].Show = true;
        //    $scope.controlpanel = false;

        //}


    }



    //$scope.exportToExcel = function () {

    //    if (str == 'All') {
    //        $scope.exportToExcelAllData($scope.LicenseType);
    //    }
    //    else if (str == 'Current') {
    //        $scope.printCurrentPageDataById($scope.LicenseType);
    //    }
    //}

    $scope.exportToExcel = function () {
       
        var divToPrint = document.getElementById($scope.LicenseType);
        $('#hiddenPrintDiv').empty();
        $('#hiddenPrintDiv').append("<table>"+divToPrint.innerHTML+"</table>");
        $('#hiddenPrintDiv thead:last-child,#hiddenPrintDiv th:last-child, #hiddenPrintDiv td:last-child').remove();
        $('#hiddenPrintDiv table').attr("id", "exportTable");
        $('#hiddenPrintDiv').attr("download", "ExportToExcel.xls");
        $('#exportTable').tableExport({ type: 'excel', escape: 'false' }, $scope.LicenseType);
    }

    $scope.printAllPagesDataById = function (id) {
        var divToPrint = document.getElementById(id);

        $('#hiddenPrintDiv').empty();
        
        $('#hiddenPrintDiv').append(divToPrint.innerHTML);
        // Removing the last column of the table
        $('#hiddenPrintDiv thead:last-child,#hiddenPrintDiv th:last-child, #hiddenPrintDiv td:last-child').remove();
        // Creating a window for printing
        var mywindow = window.open('', $('#hiddenPrintDiv').html(), 'height=800,width=800');
        mywindow.document.write('<center><b style="font-size:large">' + id + '</b></center></br>');
        mywindow.document.write('<html><head><title>'+id+'</title>');
        mywindow.document.write('<link rel="stylesheet" href="/Content/SharedCss/bootstrap.min.css" type="text/css" />');
        mywindow.document.write('<link rel="stylesheet" href="/Content/SharedCss/app.css" type="text/css" />');
        mywindow.document.write('<style>@page{size: auto;margin-bottom:5mm;margin-top:7mm;}th{text-align: center;}</style>');
        mywindow.document.write('</head><body style="background-color:white"><table class="table table-bordered"></td>');
        mywindow.document.write($('#hiddenPrintDiv').html());
        mywindow.document.write('</table></body></html>');      
        mywindow.document.close();
        mywindow.focus();
        setTimeout(function () {           
            mywindow.print();
            mywindow.close();
        }, 1000);
    }

    $scope.printCurrentPageDataById = function (id) {
        var divToPrint = document.getElementById(id);

        $('#hiddenPrintDiv').empty();

        $('#hiddenPrintDiv').append(divToPrint.innerHTML);
        // Removing the last column of the table
        $('#hiddenPrintDiv thead:last-child,#hiddenPrintDiv th:last-child, #hiddenPrintDiv td:last-child').remove();
        // Creating a window for printing
        var mywindow = window.open('', $('#hiddenPrintDiv').html(), 'height=800,width=800');
        mywindow.document.write('<center><b style="font-size:large">' + id + '</b></center></br>');
        mywindow.document.write('<html><head><title>' + id + '</title>');
        mywindow.document.write('<link rel="stylesheet" href="/Content/SharedCss/bootstrap.min.css" type="text/css" />');
        mywindow.document.write('<link rel="stylesheet" href="/Content/SharedCss/app.css" type="text/css" />');
        mywindow.document.write('<style>.ng-hide:not(.ng-hide-animate) {display: none !important;}@page{size: auto;margin-bottom:5mm;margin-top:7mm;}th{text-align: center;}</style>');
        mywindow.document.write('</head><body style="background-color:white"><table class="table table-bordered"></td>');
        mywindow.document.write($('#hiddenPrintDiv').html());
        mywindow.document.write('</table></body></html>');
        mywindow.document.close();
        mywindow.focus();
        setTimeout(function () {
            mywindow.print();
            mywindow.close();
        }, 1000);
    }

    //----------------------------------Download--------------------------------
    //$scope.downloadData = function () {
    //    $scope.downloadDataById($scope.LicenseType);
    //}

    //$scope.downloadDataById = function (id) {

    //    //--------First Method---------
    //    //var divToPrint = document.getElementById(id);
    //    //$('#hiddenPrintDiv').empty();
    //    //$('#hiddenPrintDiv').append(divToPrint.innerHTML);
    //    //$('#hiddenPrintDiv thead:last-child,#hiddenPrintDiv th:last-child, #hiddenPrintDiv td:last-child').remove();

    //    //var pdf = new jsPDF('p', 'pt', 'letter');
    //    //pdf.table(tableToJson(divToPrint));
    //    //pdf.save('sample-file.pdf');

    //    //function tableToJson(table) {
    //    //    var data = [];

    //    //    // first row needs to be headers
    //    //    var headers = [];
    //    //    for (var i = 0; i < table.rows[0].cells.length; i++) {
    //    //        headers[i] = table.rows[0].cells[i];
    //    //    }

    //    //    // go through cells
    //    //    for (var i = 1; i < table.rows.length; i++) {

    //    //        var tableRow = table.rows[i];
    //    //        var rowData = {};

    //    //        for (var j = 0; j < tableRow.cells.length; j++) {

    //    //            rowData[headers[j]] = tableRow.cells[j].innerHTML;

    //    //        }

    //    //        data.push(rowData);
    //    //    }
    //    //    return data;
    //    //}


    //    //-------Second Method-------
    //    var divToPrint = document.getElementById(id);
    //    $('#hiddenPrintDiv').empty();
    //    $('#hiddenPrintDiv').append(divToPrint.innerHTML);
    //    $('#hiddenPrintDiv thead:last-child,#hiddenPrintDiv th:last-child, #hiddenPrintDiv td:last-child').remove();

    //    var pdf = new jsPDF('p', 'pt', [594, 841]);
    //    var totalCols = $("#hiddenPrintDiv tr td").length;
    //    var cellWidth = 534 / totalCols;
    //    pdf.cellInitialize();
    //    pdf.setFontSize(10);
        
    //    $.each($('#hiddenPrintDiv tr'), function (i, row) {
    //        pdf.text(235, 30, id);
           
    //        $.each($(row).find("td, th"), function (j, cell) {
    //            var txt = $(cell).text().trim().split(" ").join("\n") || " ";
    //           // var width = (j == 4) ? 40 : 90; //make with column smaller
    //            var height = (i == 0) ? 40 : 30;
    //            pdf.cell(30, 50, cellWidth, height, txt, i);
    //        });
    //    });

    //    pdf.save(id+'.pdf');


        //--------Third Method--------
        //var divToPrint = document.getElementById(id);
        //var element = $(divToPrint).clone();
        //element.find('tr th:last-child, td:last-child').remove();
        //var source = element.html();
        //var pdf = new jsPDF('p', 'pt', 'letter');
        //var specialElementHandlers = {
        //    '#bypassme': function (element, renderer) {
        //        return true;
        //    }
        //};
        //margins = {
        //    top: 80,
        //    bottom: 60,
        //    left: 60,
        //    width: 700
        //};
        //pdf.text(235, 30, id);
        //pdf.fromHTML("<table>" + source + "</table>", // HTML string or DOM elem ref.
        //       margins.left, // x coord
        //       margins.top, { // y coord
        //           'width': margins.width, // max width of content on PDF
        //           'elementHandlers': specialElementHandlers
        //       },
        //       function (dispose) {

        //           pdf.save(id + '.pdf');
        //       },
        //    margins);

        

        //---------Fourth Method--------
        //var l = {
        //    orientation: 'l',
        //    unit: 'mm',
        //    format: 'a4',
        //    compress: true,
        //    fontSize: 8,
        //    lineHeight: 1,
        //    autoSize: false,
        //    printHeaders: true
        //}, pdf = new jsPDF(l, '', '', ''), i, j, margins = {
        //    top: 30,
        //    bottom: 10,
        //    left: 10,
        //    width: 25
        //};

        //var divToPrint = document.getElementById(id);
        //$('#hiddenPrintDiv').empty();
        //$('#hiddenPrintDiv').append(divToPrint.innerHTML);
        //$('#hiddenPrintDiv thead:last-child,#hiddenPrintDiv th:last-child, #hiddenPrintDiv td:last-child').remove();
        //var totalCols = $("#hiddenPrintDiv tr td").length;
        //var cellWidth = 534 / totalCols;
        ////initializing the cells
        //pdf.cellInitialize();

        //$.each($('#hiddenPrintDiv tr'), function (i, row) {
        //    $.each($(row).find("td, th"), function (j, cell) {
        //        var txt = $(cell).text().trim().split(" ").join("\n") || " ";
        //        // var width = (j == 4) ? 40 : 90; //make with column smaller
        //        var height = (i == 0) ? 40 : 30;
        //        pdf.cell(margins.left, margins.top, cellWidth, height, txt, i);
        //    });
        //});
        ////pdf.cell(margins.left, margins.top, 14, 8, lines, 0);

        //pdf.save('Te.pdf');

    







    var orderBy = $filter('orderBy');
    $scope.selectedSection = 6;

    //--------------- data show current page function -------------------
    $scope.GetCurrentPageData = function (data, pageNumber) {
        $scope.bigTotalItems = data.length;
        $scope.CurrentPageLicenses = [];
        $scope.bigCurrentPage = pageNumber;

        var startIndex = (pageNumber - 1) * 10;
        var endIndex = startIndex + 9;
        if (data) {
            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if (data[startIndex]) {
                    $scope.CurrentPageLicenses.push(data[startIndex]);
                } else {
                    break;
                }
            }
        }
    };

    $scope.IsValidIndex = function (index) {

        var startIndex = ($scope.currentPageNumber - 1) * 10;
        var endIndex = startIndex + 9;
        
        if (index >= startIndex && index <= endIndex)
            return true;
        else
            return false;

    }

    //------------------- Master data comes from database ------------------------

    $http.get(rootDir + '/Profile/MasterData/GetAllProviderTypes').
      success(function (data, status, headers, config) {
          $scope.ProviderTypes = data;
      }).
      error(function (data, status, headers, config) {
      });

    $http.get(rootDir + '/Profile/MasterData/GetAllProviderLevels').
      success(function (data, status, headers, config) {
          $scope.ProviderLevels = data;
      }).
      error(function (data, status, headers, config) {
      });

    $scope.LicenseData = CCOLicenseService.LicensesList();
    $scope.GrandTotalLicenses = CCOLicenseService.GetGrandTotalLicenses();

    if ($scope.LicenseData.length > 0) {
        $scope.Licenses = $scope.LicenseData[0].Licenses;
        $scope.LicenseType = $scope.LicenseData[0].LicenseType;
        $scope.LicenseTypeCode = $scope.LicenseData[0].LicenseTypeCode;
        $scope.MasterLicenses = angular.copy($scope.Licenses);
        $scope.GetCurrentPageData($scope.Licenses, 1);
    }


    $scope.CurrentPageLicenses = [];

    //-------------------------- angular bootstrap pagger with custom-----------------
    $scope.maxSize = 5;
    $scope.bigTotalItems = 0;
    $scope.bigCurrentPage = 1;

    //-------------------- page change action ---------------------
    $scope.pageChanged = function (pagnumber) {
        $scope.bigCurrentPage = pagnumber;
    };

    //-------------- current page change Scope Watch ---------------------
    $scope.$watch('bigCurrentPage', function (newValue, oldValue) {
        if ($scope.Licenses) {
            $scope.GetCurrentPageData($scope.Licenses, newValue);
            $scope.currentPageNumber = newValue;
        }
    });

    //------------------- get data according Selected days -----------------------
    $scope.getDataAccordingDays = function (days) {
        $scope.filterDays = days;
        $scope.selectedSection = 6;

        $scope.LicenseData = CCOLicenseService.GetLicenseByDayLeft(days);

        $scope.GrandTotalLicenses = CCOLicenseService.GetGrandTotalLicenses();

        if ($scope.LicenseData.length > 0) {
            $scope.providerType = "";
            $scope.providerLevel = "";
            $scope.LicenseTypeCode = $scope.LicenseData[0].LicenseTypeCode;
            $scope.Licenses = $scope.LicenseData[0].Licenses;
            $scope.LicenseType = $scope.LicenseData[0].LicenseType;
            $scope.MasterLicenses = angular.copy($scope.Licenses);
        }
        $scope.GetCurrentPageData($scope.Licenses, 1);
    };

    //-------------editied by pritam--------
    $scope.getUpcomingRenewals = function (licenseData)
    {
        var licenses = [];
        for (var i = 0; i < licenseData.Licenses.length; i++) {
            if (licenseData.Licenses[i].License.dayLeft > 90 && licenseData.Licenses[i].License.dayLeft < 180)
            {
                licenses.push(licenseData.Licenses[i]);
            }
        }
        $scope.Licenses = angular.copy(licenses);
        $scope.selectedSection = 6;
        $scope.LicenseType = licenseData.LicenseType;
        $scope.LicenseTypeCode = licenseData.LicenseTypeCode;
        $scope.MasterLicenses = angular.copy($scope.Licenses);
        $scope.GetCurrentPageData($scope.Licenses, 1);
    }

    $scope.getRenewalNeeded = function (licenseData) {
       var licenses = [];
        for (var i = 0; i < licenseData.Licenses.length; i++) {
            if (licenseData.Licenses[i].License.dayLeft > 0 && licenseData.Licenses[i].License.dayLeft < 90) {
                licenses.push(licenseData.Licenses[i]);
            }
        }
        $scope.Licenses = angular.copy(licenses);
        $scope.selectedSection = 6;
        $scope.LicenseType = licenseData.LicenseType;
        $scope.LicenseTypeCode = licenseData.LicenseTypeCode;
        $scope.MasterLicenses = angular.copy($scope.Licenses);
        $scope.GetCurrentPageData($scope.Licenses, 1);
    }

    $scope.getExpiredLicense = function (licenseData) {
        var licenses = [];
        for (var i = 0; i < licenseData.Licenses.length; i++) {
            if (licenseData.Licenses[i].License.dayLeft < 0) {
                licenses.push(licenseData.Licenses[i]);
            }
        }
        $scope.Licenses = angular.copy(licenses);
        $scope.selectedSection = 6;
        $scope.LicenseType = licenseData.LicenseType;
        $scope.LicenseTypeCode = licenseData.LicenseTypeCode;
        $scope.MasterLicenses = angular.copy($scope.Licenses);
        $scope.GetCurrentPageData($scope.Licenses, 1);
    }

    //-------------- License change method -------------------------------
    $scope.getLicensTypeData = function (licenseData) {
        $scope.selectedSection = 6;
        $scope.providerType = "";
        $scope.providerLevel = "";
        $scope.Licenses = licenseData.Licenses;
        $scope.LicenseType = licenseData.LicenseType;
        $scope.LicenseTypeCode = licenseData.LicenseTypeCode;
        $scope.MasterLicenses = angular.copy($scope.Licenses);
        $scope.GetCurrentPageData($scope.Licenses, 1);
    };

    //------------------- get data according Selected Provider Type and Provider Level-----------------------
    $scope.getDataAccordingProviderTypeAndProviderLevel = function (providertype, providerlevel) {
        $scope.selectedSection = 6;
        $scope.Licenses = CCOLicenseService.GetLicenseByProviderType(providertype, providerlevel, $scope.MasterLicenses);
        $scope.GetCurrentPageData($scope.Licenses, 1);
    };

    //-------------- ANgular sorting filter --------------
    $scope.order = function (predicate, reverse, section) {
        $scope.selectedSection = section;
        $scope.Licenses = orderBy($scope.Licenses, predicate, reverse);
        $scope.GetCurrentPageData($scope.Licenses, $scope.bigCurrentPage);
    };

    //if (sessionStorage.getItem('myTabs') != null) {

    //    $scope.LicenseData = JSON.parse(sessionStorage.getItem('myTabs'));

    ///}
}]);
