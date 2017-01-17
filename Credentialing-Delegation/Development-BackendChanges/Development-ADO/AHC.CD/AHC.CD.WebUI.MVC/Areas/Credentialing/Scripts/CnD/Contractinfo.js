//Contract Information Controller Angular MOdule
//Author Santosh K.

$(document).ready(function () {
    //console.log("collapsing menu");
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



ContractInfo.controller("ContractinfoController", function ($scope, $filter, $timeout, $http, ngTableParams) {
    $scope.loadingAjax = false;
    $scope.IsDetails = false;
    $scope.display = false;
    $scope.edit = false;
    $scope.editMe = true;
    $scope.updated = false;
    $scope.myIndex = 0;
    $scope.loadingAjax = true;

    $http.get(rootDir + '/Credentialing/CnD/GetAllContractGrid').
       success(function (data, status, headers, config) {

           if (data != null) {
               $scope.ContractGridList = data;
           }
           //console.log("Check");
           console.log("Contract Grid");
           console.log($scope.ContractGridList);

           $scope.ReArrange();

           $scope.init_table($scope.ContractGridList);

           $scope.loadingAjax = false;
       }).
       error(function (data, status, headers, config) {
           //console.log("Sorry internal master data cont able to fetch.");
       });

    $scope.ReArrange = function () {

        var newIndex = 0;
        $scope.todayDate = $filter('date')(new Date(Date.now()), 'dd/MM/yyyy');

        if ($scope.ContractGridList != null && $scope.ContractGridList.length != 0) {

            for (var i = 0; i < $scope.ContractGridList.length; i++) {

                if ($scope.ContractGridList[i].Report != null) {

                    if ($scope.ContractGridList[i].Report.TerminationDate != null) {

                        $scope.myDate = $filter('date')(new Date($scope.ContractGridList[i].Report.TerminationDate), 'dd/MM/yyyy');

                    }

                    if ($scope.ContractGridList[i].Report.Status == "Inactive" || $scope.ContractGridList[i].Report.CredentialingApprovalStatusType == "2" || $scope.myDate < $scope.todayDate) {

                        $scope.ContractGridList.splice($scope.ContractGridList.indexOf($scope.ContractGridList[i]), 1);
                        i--;
                        continue;

                    }

                }

                if ($scope.ContractGridList[i].CredentialingInfo != null) {

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

                    if ($scope.ContractGridList[i].CredentialingInfo.Profile != null && $scope.ContractGridList[i].CredentialingInfo.Profile.PersonalDetail != null) {

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

                $scope.ContractGridList[i].InitialCredentialingDate = $scope.ConvertDateFormat($scope.ContractGridList[i].InitialCredentialingDate);
                $scope.ContractGridList[i].EffectiveDate = $scope.ConvertDateFormat($scope.ContractGridList[i].LastModifiedDate);
                if ($scope.ContractGridList[i].Report != null) {
                    $scope.ContractGridList[i].Report.InitiatedDate = $scope.ConvertDateFormat($scope.ContractGridList[i].Report.InitiatedDate);
                    $scope.ContractGridList[i].Report.CredentialedDate = $scope.ConvertDateFormat($scope.ContractGridList[i].Report.CredentialedDate);
                    //$scope.ContractGridList[i].Report.TerminationDate = $scope.ConvertDateFormat($scope.ContractGridList[i].Report.TerminationDate);
                    $scope.ContractGridList[i].Report.ReCredentialingDate = $scope.ConvertDateFormat($scope.ContractGridList[i].Report.ReCredentialingDate);
                }

                if ($scope.ContractGridList[i].CredentialingInfo != null) {

                    if ($scope.ContractGridList[i].CredentialingInfo.Profile != null && $scope.ContractGridList[i].CredentialingInfo.Profile.PersonalDetail != null) {

                        $scope.ContractGridList[i].Name = $scope.ContractGridList[i].CredentialingInfo.Profile.PersonalDetail.Salutation + " " + FirstName + " " + MiddleName + " " + LastName;

                    }

                    if ($scope.ContractGridList[i].CredentialingInfo.Plan != null) {

                        $scope.ContractGridList[i].PlanName = $scope.ContractGridList[i].CredentialingInfo.Plan.PlanName;

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

                    }
                }

                if ($scope.ContractGridList[i].Report != null) {
                    $scope.ContractGridList[i].ProviderID = $scope.ContractGridList[i].Report.ProviderID;
                    $scope.ContractGridList[i].PanelStatus = $scope.ContractGridList[i].Report.PanelStatus;
                }

            }

        }

        $scope.data = $scope.ContractGridList;

    }

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
            //console.log(new FormData($formData[0]));

            //console.log($formDataStateLicense);
            $.ajax({
                url: url,
                type: 'POST',
                data: new FormData($formData[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    //console.log(data);
                    try {
                        if (data.status == "true") {
                            data.dataContractGrid.InitialCredentialingDate = $scope.ConvertDateFormat(data.dataContractGrid.InitialCredentialingDate);
                            if (data.dataContractGrid.Report != null) {
                                data.dataContractGrid.Report.CredentialedDate = $scope.ConvertDateFormat(data.dataContractGrid.Report.CredentialedDate);
                                data.dataContractGrid.Report.InitiatedDate = $scope.ConvertDateFormat(data.dataContractGrid.Report.InitiatedDate);
                                data.dataContractGrid.Report.TerminationDate = $scope.ConvertDateFormat(data.dataContractGrid.Report.TerminationDate);
                                data.dataContractGrid.Report.ReCredentialingDate = $scope.ConvertDateFormat(data.dataContractGrid.Report.ReCredentialingDate);
                                data.dataContractGrid.CredentialingInfo.InitiationDate = $scope.ConvertDateFormat(data.dataContractGrid.CredentialingInfo.InitiationDate);
                            }

                            if (data.dataContractGrid != null) {

                                $scope.Details = data.dataContractGrid;
                                $scope.ContractGridList[$scope.myIndex] = $scope.Details;

                            }

                            $scope.getAddress();
                            $scope.getManager();

                            $scope.ReArrange();
                            $scope.tableParams1.reload();
                            $scope.updated = true;
                            $timeout(function () {
                                $scope.updated = false;
                            }, 5000);
                            $scope.updated = false;
                            $scope.myDate = $filter('date')(new Date(data.dataContractGrid.Report.TerminationDate), 'dd/MM/yyyy');
                            if (data.dataContractGrid.Report.CredentialingApprovalStatusType != "2" || $scope.myDate < $scope.todayDate) {
                                $scope.display = false;
                            } else {
                                $scope.display = true;
                            }
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
                //console.log(data);
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
        ////console.log(value);
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
    }

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
        $scope.Details = data;
        $scope.getAddress();
        $scope.getManager();
        $scope.IsDetails = true;
        $scope.myIndex = $scope.data.indexOf(data);
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

        if (data != null) {

            $scope.Details = data;
            $scope.getAddress();
            $scope.getManager();

        }

        $scope.editMe = false;
        $scope.IsDetails = true;
        $scope.display = false;
        $scope.edit = true;
        $scope.myIndex = $scope.data.indexOf(data);

    }

    console.log($scope.ContractInformation);

    $scope.closeContractInfo = function () {

        $scope.IsDetails = false;

    }

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
                Name: 'desc'
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
                $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
            }
        });

    };

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