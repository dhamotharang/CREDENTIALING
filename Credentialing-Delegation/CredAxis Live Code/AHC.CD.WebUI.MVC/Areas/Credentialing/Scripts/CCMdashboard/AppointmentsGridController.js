CCMDashboard.controller("AppointmentGridController", ["$rootScope", "$scope", "toaster", "$timeout", "$filter", "$loadash", "CCMDashboardService", "CCMDashboardFactory", function ($rootScope, $scope, toaster, $timeout, $filter, $loadash, CCMDashboardService, CCMDashboardFactory) {
    //================================= Variable Declaration Satrt====================================================================================
    var $self = this;
    this.displayed = [];

    //================================= Variable Declaration End====================================================================================
    //================================== Temporary function Declaration Start ===============================================================================
    this.callServer = function callServer(tableState) {
        if (tableState === undefined) {
            return;
        }
        $self.isLoading = true;
        var pagination = tableState.pagination;
        var start = pagination.start || 0;
        var number = pagination.number || 5;
        $scope.tableStateValue = tableState;
        CCMDashboardFactory.getPage(start, number, tableState).then(function (result) {
            $self.displayed = result.data;
            tableState.pagination.numberOfPages = result.numberOfPages;
            $self.isLoading = false;
            $rootScope.ToHighLightRowObject = "";
        });
    };

    //------- Method to check the pending item is selected or not -----------
    this.CheckPendingSelected = function isPendingSelected(records) {
        return records.some(function (record) {
            return record.SelectStatus == true;
        })
    }

    $scope.selectPlan = function (PlanName) {
        $rootScope.CCMAppointments = $filter('CCMDashboardFilterByPlan')(PlanName);
    }


    $rootScope.$on('AppointmentsGrid', function (event, args) {
        $rootScope.TempObjectForStatus.CredentailingApprovalRequest = true;
        $rootScope.TempObjectForStatus.QuickApprovalAction = false;
        $rootScope.TempObjectForStatus.AppointmentDashboard = false;
        $rootScope.VisibilityControl = '';
        CCMDashboardFactory.ClearSelectRowStatus();
        if (args.RowObject !== null && args.RowObject !== undefined) {
            $rootScope.ToHighLightRowObject = args.RowObject;
            $scope.TempToHighLightRowObject = args.RowObject;

        }
        switch (args.type) {
            case "Appointments":
                //$rootScope.TempCCMAppointments = $filter('orderBy')($rootScope.CCMAppointments, 'AppointmentDate', true);  
                $rootScope.TempCCMAppointments = $rootScope.CCMAppointments;                
                $rootScope.tableCaption = "Appointments";
                break;
            case "Approved":
                $rootScope.TempCCMAppointments = $filter('filter')($rootScope.CCMAppointments, { Status: "Approved" });
                $rootScope.tableCaption = "Approved";
                break;
            case "Rejected":
                $rootScope.TempCCMAppointments = $filter('filter')($rootScope.CCMAppointments, { Status: "Rejected" });
                $rootScope.tableCaption = "Rejected";
                break;
            case "New":
                $rootScope.TempCCMAppointments = [];
                for (var i in $rootScope.CCMAppointments) {
                    if (($rootScope.CCMAppointments[i].Status == "New") || ($rootScope.CCMAppointments[i].Status == "Onhold")) {
                        $rootScope.TempCCMAppointments.push($rootScope.CCMAppointments[i]);
                    }
                }
                $rootScope.tableCaption = "Pending";
                break;
            case "Pending":
                $rootScope.TempCCMAppointments = [];
                //$rootScope.TempCCMAppointments = $filter('filter')($rootScope.CCMAppointments, { Status: "OnHold" }) || $filter('filter')($rootScope.CCMAppointments, { Status: "New" });
                for (var i in $rootScope.CCMAppointments) {
                    if (($rootScope.CCMAppointments[i].Status == "New") || ($rootScope.CCMAppointments[i].Status == "Onhold")) {
                        $rootScope.TempCCMAppointments.push($rootScope.CCMAppointments[i]);
                    }
                }
                $rootScope.tableCaption = "Pending";
                break;
            case "Onhold":
                $rootScope.TempCCMAppointments = [];
                //$rootScope.TempCCMAppointments = $filter('filter')($rootScope.CCMAppointments, { Status: "OnHold" }) || $filter('filter')($rootScope.CCMAppointments, { Status: "New" });
                for (var i in $rootScope.CCMAppointments) {
                    if (($rootScope.CCMAppointments[i].Status == "New") || ($rootScope.CCMAppointments[i].Status == "Onhold")) {
                        $rootScope.TempCCMAppointments.push($rootScope.CCMAppointments[i]);
                    }
                }
                $rootScope.tableCaption = "Pending";
                break;
        }
        $rootScope.TempCCMAppointments = $filter('orderBy')($rootScope.TempCCMAppointments, 'AppointmentDate', true);
        $scope.tableStateValue = CCMDashboardFactory.resetTableState($scope.tableStateValue);
        if (args.RowObject) $self.swapRow(args.RowObject);
        $self.callServer($scope.tableStateValue, args.RowObject);
        $timeout(countUp, 8000);
    });

    this.swapRow = function (obj) {
        try {
            var index = $rootScope.TempCCMAppointments.indexOf(obj);
            obj = $rootScope.TempCCMAppointments[index];
            if (index > -1) {
                $rootScope.TempCCMAppointments.splice(index, 1);
                $rootScope.TempCCMAppointments.splice(0, 0, obj);
            }
        } catch (e) {
            console.log(e);
        }


    }

    var countUp = function () {
        $scope.TempToHighLightRowObject = "";
    }
    $scope.OpenAppointmentDetailsControl = function (sectionValue) {
        $rootScope.VisibilityControl = sectionValue;
        $rootScope.TempObjectForStatus.SingleDetailedApprovalAction = true;
    }
    $scope.CloseAppointmentDetailsControl = function (sectionValue) {
        $rootScope.VisibilityControl = '';
        $rootScope.TempObjectForStatus.SingleDetailedApprovalAction = false;
    }

    //==================================================Report Approval Form Submission Script Start ======================================================
    //-----to clear digital sign-----
    $scope.cleardigitalsignature = function () {
        context.clearRect(0, 0, canvas.width, canvas.height);
    }

    //$scope.showreusesignaturediv = true;
    $rootScope.showuploaddiv1 = false;
    //$scope.showsignaturediv = false;
    $scope.errormessageforsignature = false;
    $scope.SavingStatus = false;
    $scope.errormessageforuploadsignature = false;
    $scope.RemarkForApprovalStatus = "";
    $scope.validatesignature = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAPgAAABiCAYAAAB9LB4uAAACaklEQVR4Xu3TAREAAAgCMelf2h5/swFDdo4AgazAsskEI0DgDNwTEAgLGHi4XNEIGLgfIBAWMPBwuaIRMHA/QCAsYODhckUjYOB+gEBYwMDD5YpGwMD9AIGwgIGHyxWNgIH7AQJhAQMPlysaAQP3AwTCAgYeLlc0AgbuBwiEBQw8XK5oBAzcDxAICxh4uFzRCBi4HyAQFjDwcLmiETBwP0AgLGDg4XJFI2DgfoBAWMDAw+WKRsDA/QCBsICBh8sVjYCB+wECYQEDD5crGgED9wMEwgIGHi5XNAIG7gcIhAUMPFyuaAQM3A8QCAsYeLhc0QgYuB8gEBYw8HC5ohEwcD9AICxg4OFyRSNg4H6AQFjAwMPlikbAwP0AgbCAgYfLFY2AgfsBAmEBAw+XKxoBA/cDBMICBh4uVzQCBu4HCIQFDDxcrmgEDNwPEAgLGHi4XNEIGLgfIBAWMPBwuaIRMHA/QCAsYODhckUjYOB+gEBYwMDD5YpGwMD9AIGwgIGHyxWNgIH7AQJhAQMPlysaAQP3AwTCAgYeLlc0AgbuBwiEBQw8XK5oBAzcDxAICxh4uFzRCBi4HyAQFjDwcLmiETBwP0AgLGDg4XJFI2DgfoBAWMDAw+WKRsDA/QCBsICBh8sVjYCB+wECYQEDD5crGgED9wMEwgIGHi5XNAIG7gcIhAUMPFyuaAQM3A8QCAsYeLhc0QgYuB8gEBYw8HC5ohEwcD9AICxg4OFyRSNg4H6AQFjAwMPlikbAwP0AgbCAgYfLFY2AgfsBAmEBAw+XKxoBA/cDBMICBh4uVzQCBu4HCIQFDDxcrmgEHgEaAGNMPbq1AAAAAElFTkSuQmCC";

    //--------- To Change the signature option ----
    $scope.ChangeSignatureOption = function () {
        type = event.currentTarget.value;
        var canvas;
        canvas = document.getElementById('SignatureCanvas');
        context = canvas.getContext('2d');
        if (type == 'upload') {
            $('#selectfile').trigger('click', function () { });
            $rootScope.showreusesignaturediv1 = false;
            $rootScope.showsignaturediv1 = false;
            $rootScope.showuploaddiv1 = true;
        } else if (type == 'digitalsignature') {
            $rootScope.showreusesignaturediv1 = false;
            $rootScope.showuploaddiv1 = false;
            $scope.errormessageforsignature = false;
            $rootScope.showsignaturediv1 = true;
        }
        else if (type == 'reusedigitalsignature') {
            $rootScope.showsignaturediv1 = false;
            $rootScope.showuploaddiv1 = false;
            $rootScope.showreusesignaturediv1 = true;
        }
      
        if (type == 'upload' || type == 'reusedigitalsignature') {
            context.clearRect(0, 0, canvas.width, canvas.height);
        }
        if (type != 'upload') {
            $("#SignatureFile").replaceWith($("#SignatureFile").val('').clone(true));
        }
    };

    //-----This method will validate the input --------
    $scope.SaveAppoinmentDecision = function (decision) {
        $scope.SavingStatus = true;
        $scope.image = "";
        try {
            CCMDashboardFactory.ResetFormForValidation(angular.element("#MultipleCCMAction"));
        } catch (e) {

        }

        if (angular.element("#signatureOption").val() == "digitalsignature") {
            var canvas;
            canvas = document.getElementById('SignatureCanvas');
            $scope.image = canvas.toDataURL("image/png");
            if ($scope.image == document.getElementById('blank').toDataURL() || $scope.image == $scope.validatesignature)
                $scope.errormessageforsignature = true;
            else {
                $scope.errormessageforsignature = false;
                $self.submitForm(decision);

            }
        }
        if (angular.element("#signatureOption").val() == "reusedigitalsignature") {
            $scope.image = $rootScope.SignaturePath;
        }
        if (angular.element("#signatureOption").val() != "digitalsignature") {
            if ($("#MultipleCCMAction").valid() == false && $("#signatureOption").val() == "upload")
                $scope.errormessageforuploadsignature = true;

            if ($("#MultipleCCMAction").valid()) {
                $scope.errormessageforuploadsignature = false;
                $self.submitForm(decision);
            }
        }

    }
    //------ To submit the form -----
    this.submitForm = function (decision, RowObject) {
        var selectedRows = $filter('filter')($rootScope.TempCCMAppointments, { SelectStatus: true });
        var QuickActionSet = selectedRows.map(function (a) { return { CredentialingInfoId: a.ProviderCredentialingInfoID, CredentialingAppointmentDetailID: a.CredentialingAppointmentDetailID, ProfileId: a.ProfileID, RecommendedLevel: a.RecommendedLevel } });
        var AppointmentsStatus = decision == "Approved" ? 1 : (decision == "Rejected") ? 2 : 3;
        var formdata = new FormData();
        var form5 = angular.element("#MultipleCCMAction").serializeArray();
        var Singanture = $scope.image;
        for (var i in form5) {
            formdata.append(form5[i].name, form5[i].value);
        }
        formdata.append('AppointmentsStatus', AppointmentsStatus);
        jQuery.each(QuickActionSet, function (key, value) {
            formdata.append('QuickActionSet[' + key + '][CredentialingAppointmentDetailID]', QuickActionSet[key]['CredentialingAppointmentDetailID']); formdata.append('QuickActionSet[' + key + '][CredentialingInfoId]', QuickActionSet[key]['CredentialingInfoId']); formdata.append('QuickActionSet[' + key + '][ProfileId]', QuickActionSet[key]['ProfileId']); formdata.append('QuickActionSet[' + key + '][RecommendedLevel]', QuickActionSet[key]['RecommendedLevel']);
        })
        formdata.append(angular.element("#SignatureFile")[0].name, angular.element("#SignatureFile")[0].files[0]);
        formdata.append('SignaturePath', Singanture); 
       
        CCMDashboardService.SaveQuickActionAppointments(formdata).then(function (response) {
            console.log(response);

            for (var i = 0; i < response.data.length; i++) {
                for (var j = 0; j < $rootScope.CCMAppointments.length; j++) {
                    if ($rootScope.CCMAppointments[j].CredentialingAppointmentDetailID == response.data[i].CCMRequestID)
                        $rootScope.CCMAppointments[j].Status = decision;
                }
            }

            var CredIDs = $loadash.map($loadash.filter(response.data, { CredRequestStatus: "true" }), 'CCMRequestID');

            //for (var j = 0; j < $rootScope.CCMAppointments.length; j++) {
            //    if ($rootScope.CCMAppointments[j].CredentialingAppointmentDetailID == 4002)
            //        $rootScope.CCMAppointments[j].Status = decision;
            //}

            $("#ActionModal").hide();
            angular.element('body').removeClass('modal-open');
            angular.element('.modal-backdrop').remove();

            if (decision == "Approved" || decision == "Rejected") 
                toaster.pop('Success', "Success", CredIDs.length + " Request(s) " + decision + " Successfully");
            else
                toaster.pop('Success', "Success", CredIDs.length + " Request(s) On-Hold Successfully");

            if (CredIDs.length != response.data.length) {
                toaster.pop('error', "", (response.data.length - CredIDs.length + 'Request(s) Failed ' + 'Please try after sometime !!!'));
            }

            $rootScope.$broadcast('AppointmentsGrid', { type: "Pending", RowObject: "" });
            $rootScope.BisuctCounts = CCMDashboardFactory.LoadCounts($rootScope.CCMAppointments);
            
        });
    }

    $scope.SwitchViews = function () {
        $rootScope.TempObjectForStatus.CredentailingApprovalRequest = false;
        $rootScope.TempObjectForStatus.AppointmentDashboard = true
        $scope.$parent.GridType = "";
        $rootScope.currentDate = $rootScope.selectedDate;
    }

    $scope.TableExport = function (type, tableID) {
        CCMDashboardFactory.exportToTable(type, tableID);
    }


}]);
