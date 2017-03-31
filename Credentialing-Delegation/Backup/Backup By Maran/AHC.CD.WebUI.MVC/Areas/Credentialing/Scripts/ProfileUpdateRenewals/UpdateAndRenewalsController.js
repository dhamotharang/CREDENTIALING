UpdateAndRenewalsApp.controller("UpdateAndRenewalsController", ["$rootScope", "$scope", "toaster", "$timeout", "$filter", "$loadash", "UpdateAndRenewalsService", "UpdateAndRenewalsFactory", "tracking", function ($rootScope, $scope, toaster, $timeout, $filter, $loadash, UpdateAndRenewalsService, UpdateAndRenewalsFactory, tracking) {

     //=================================== Variable Declaration Start ===================================

    var $self = this;
    $scope.popoverIsVisible = false;
    $scope.HistorySwitchButton = true;
    $scope.CredRequestSwitchButton = true;
    $scope.SelectAllButton = false;
    $scope.LoadingStatus = false;
    $scope.SavingStatus = false;
    $scope.RejectStatus = false;
    $scope.ProfileUpdateandRenewalFilter = false;
    $scope.CredentialingReqFilter = false;
    $scope.ApproveAllStatus = false;
    $scope.RejectionType = '';
    $scope.tableStateValueUpdateAndHistory = {};
    $scope.tableStateValueCredentialingRequest = {};
    $scope.displayDataForProfileUpdate = {};
    $scope.BicuitValue = { UpdateCount: 0, RenewalCount: 0, CredRequestCount: 0, HistoryCount: 0 };
    $self.displayedUpdateAndRenewals = [];
    $self.displayedCredentialingRequest = [];
    $scope.SelectedData = [];
    $scope.MasterSettings = $filter("MasterSettingFiltter")("Updates");
    $scope.Traffic = tracking;

    //=================================== Variable Declaration End ===================================



    //==================================== Smart Table update Start ======================================

    this.callServerUpdateAndHistory = function callServerUpdateAndHistory(tableState) {
        $self.isLoadingUpdateAndRenewals = true;
        if (tableState === undefined) {
            $self.displayedUpdateAndRenewals = [];
            return;
        }

        var pagination = tableState.pagination;
        var start = pagination.start || 0;
        var number = pagination.number || 5;
        $scope.tableStateValueUpdateAndHistory = tableState;
        UpdateAndRenewalsFactory.getPageForUpdateAndHistory(start, number, tableState).then(function (result) {
            $self.displayedUpdateAndRenewals = result.data;
            tableState.pagination.numberOfPages = result.numberOfPages;
            $self.isLoadingUpdateAndRenewals = false
        });
    }
    this.callServerCredentialingRequest = function callServerCredentialingRequest(tableState) {
        if (tableState === undefined) {
            $self.displayedCredentialingRequest = [];
            return;
        }
        $self.isLoadingCredentialingRequest = true;
        var pagination = tableState.pagination;
        var start = pagination.start || 0;
        var number = pagination.number || 5;
        $scope.tableStateValueCredentialingRequest = tableState;
        UpdateAndRenewalsFactory.getPageCredentialingRequest(start, number, tableState).then(function (result) {
            $self.displayedCredentialingRequest = result.data;
            tableState.pagination.numberOfPages = result.numberOfPages;
            $self.isLoadingCredentialingRequest = false;
        });
    }

    //==================================== Smart Table update  End ======================================



    //==================================== Temporary Methods Start ========================================

    $scope.HistoryChange = function () {
        $scope.HistorySwitchButton = !$scope.HistorySwitchButton;
        if ($scope.HistorySwitchButton) {
            $rootScope.TempProfileUpdates = $filter('filter')($rootScope.ProfileUpdates, { Modification: "Update" });
        }
        else {
            $rootScope.TempProfileUpdates = $filter('filter')($rootScope.ProfileUpdates, { Modification: "Renewal" });
        }
        $scope.tableStateValueUpdateAndHistory = UpdateAndRenewalsFactory.resetTableState($scope.tableStateValueUpdateAndHistory);
        $self.callServerUpdateAndHistory($scope.tableStateValueUpdateAndHistory);
    }
    $scope.CredRequestChange = function () {
        $scope.CredRequestSwitchButton = !$scope.CredRequestSwitchButton;
        if ($scope.CredRequestSwitchButton) {
            $rootScope.TempCredentialingRequests = $filter('filter')($rootScope.CredentialingRequests, { CurrentStatus: "Active" });
        }
        else {
            $rootScope.TempCredentialingRequests = $rootScope.CredentialingRequests.filter(function (CredentialingRequest) { return CredentialingRequest.CurrentStatus == "Approved" || CredentialingRequest.CurrentStatus == "Rejected" || CredentialingRequest.CurrentStatus == "Dropped" });
        }
        $scope.tableStateValueCredentialingRequest = UpdateAndRenewalsFactory.resetTableState($scope.tableStateValueCredentialingRequest);
        $self.callServerCredentialingRequest($scope.tableStateValueCredentialingRequest);
    }
    $scope.CheckStatus = function ($event, profileUpdates) {
        if (IsProvider) {
            return;
        }
        if ($event.target.className.indexOf('skip') > -1) {
            return;
        }
        if (profileUpdates.ApprovalStatus == 'Pending' || profileUpdates.ApprovalStatus == 'OnHold') {
            if (profileUpdates.IsSelected == "false") {

                $rootScope.TempProfileUpdates[$rootScope.TempProfileUpdates.indexOf(profileUpdates)].IsSelected = "true";
                $scope.SelectedData.push($rootScope.TempProfileUpdates[$rootScope.TempProfileUpdates.indexOf(profileUpdates)]);

            } else {
                $scope.SelectedData.splice($scope.SelectedData.indexOf($rootScope.TempProfileUpdates[$rootScope.TempProfileUpdates.indexOf(profileUpdates)]), 1)
                $rootScope.TempProfileUpdates[$rootScope.TempProfileUpdates.indexOf(profileUpdates)].IsSelected = "false";
            }
        }
    }
    $scope.SelectAll = function () {
        $scope.SelectedData = [];
        $scope.SelectAllButton = !$scope.SelectAllButton;
        if ($scope.SelectAllButton) {
            for (i in $rootScope.TempProfileUpdates) {
                $rootScope.TempProfileUpdates[i].IsSelected = "true";
            }
            $scope.SelectedData = angular.copy($rootScope.TempProfileUpdates);
        }
        else {
            for (i in $rootScope.TempProfileUpdates) {
                $rootScope.TempProfileUpdates[i].IsSelected = "false";
            }
            $scope.SelectedData = [];
        }
    }
    $scope.GridData = function (type) {
        switch (type) {
            case "Updates":
                $scope.GetAllUpdatesAndRenewals(type);
                break;
            case "Renewals":
                $scope.GetAllUpdatesAndRenewals(type);
                break;
            case "CredentialingRequest":
                $scope.GetAllCredentialingRequests();
                break;
            case "ViewHistory":
                $scope.GetAllUpdatesAndRenewalsHistory();
                break;
        }
        $scope.tableStateValue = UpdateAndRenewalsFactory.resetTableState($scope.tableStateValue);
        $self.callServerUpdateAndHistory($scope.tableStateValue);
    }
    $scope.RejectAction = function () {
        $scope.RejectStatus = !$scope.RejectStatus;
    }
    $scope.RejectActionForUpdate = function (Type) {
        $scope.RejectionType = Type;
        $scope.RejectStatus = !$scope.RejectStatus;
    }
    $scope.TableExport = function (type, tableID) {
        UpdateAndRenewalsFactory.exportToTable(type, tableID);
        toaster.pop('Success', "Success", 'Exporting data into ' + type + ' successfull');
    }
    $scope.FilterForProfileUpdateandRequest = function () {
        if ($scope.MasterSettings.TableType == 1) {
            $scope.ProfileUpdateandRenewalFilter = !$scope.ProfileUpdateandRenewalFilter;
        }
        else {
            $scope.CredentialingReqFilter = !$scope.CredentialingReqFilter;
        }
    }

    //==================================== Temporary Methods End ========================================



    //==================================== Crud Operations Start =========================================

    $scope.GetAllUpdatesAndRenewals = function (Type) {
        $scope.SelectAllButton = false;
        $scope.ProfileUpdateandRenewalFilter = false;
        $self.displayedUpdateAndRenewals = [];
        $scope.SelectedData = [];
        UpdateAndRenewalsService.GetAllUpdatesAndRenewals().then(function (result) {
            $scope.tableStateValueUpdateAndHistory = UpdateAndRenewalsFactory.resetTableState($scope.tableStateValueUpdateAndHistory);
            $rootScope.ProfileUpdates = result.data.PROFILEUPDATERENEWAL;
            $scope.BicuitValue = {
                UpdateCount: result.data.UPDATECOUNT,
                RenewalCount: result.data.RENEWALCOUNT,
                CredRequestCount: result.data.REQUESTCOUNT,
                HistoryCount: result.data.HISTORYCOUNT
            };
            if (Type == 'Updates') {
                $rootScope.TempProfileUpdates = $filter('filter')($rootScope.ProfileUpdates, { Modification: "Update" });
                $scope.MasterSettings = $filter("MasterSettingFiltter")("Updates");
            }
            else {
                $rootScope.TempProfileUpdates = $filter('filter')($rootScope.ProfileUpdates, { Modification: "Renewal" });
                $scope.MasterSettings = $filter("MasterSettingFiltter")("Renewals");
            }

            $self.callServerUpdateAndHistory($scope.tableStateValueUpdateAndHistory);
        }, function (error) {
            toaster.pop('error', "", 'Please try after sometime !!!');
        })
    }
    $scope.GetAllUpdatesAndRenewalsHistory = function () {
        $scope.HistorySwitchButton = true;
        $scope.ProfileUpdateandRenewalFilter = false;
        $scope.SelectedData = [];
        UpdateAndRenewalsService.GetAllUpdatesAndRenewalsHistory().then(function (result) {
            $scope.tableStateValueUpdateAndHistory = UpdateAndRenewalsFactory.resetTableState($scope.tableStateValueUpdateAndHistory);
            $rootScope.ProfileUpdates = result.data.PROFILEUPDATEHISTORY;
            $scope.BicuitValue = {
                UpdateCount: result.data.UPDATECOUNT,
                RenewalCount: result.data.RENEWALCOUNT,
                CredRequestCount: result.data.REQUESTCOUNT,
                HistoryCount: result.data.HISTORYCOUNT
            };
            $rootScope.TempProfileUpdates = $filter('filter')($rootScope.ProfileUpdates, { Modification: "Update" });
            $scope.MasterSettings = $filter("MasterSettingFiltter")("History");
            $self.callServerUpdateAndHistory($scope.tableStateValueUpdateAndHistory);
        }, function (error) {
            toaster.pop('error', "", 'Please try after sometime !!!');
        })
    }
    $scope.GetAllCredentialingRequests = function () {
        //$scope.CredRequestSwitchButton = true;
        $scope.CredentialingReqFilter = false;
        UpdateAndRenewalsService.GetAllCredentialingRequests().then(function (result) {
            $scope.tableStateValueCredentialingRequest = UpdateAndRenewalsFactory.resetTableState($scope.tableStateValueCredentialingRequest);
            $rootScope.CredentialingRequests = result.data.CREDENTIALINGREQUEST;
            $scope.BicuitValue = {
                UpdateCount: result.data.UPDATECOUNT,
                RenewalCount: result.data.RENEWALCOUNT,
                CredRequestCount: result.data.REQUESTCOUNT,
                HistoryCount: result.data.HISTORYCOUNT
            };
            $rootScope.TempCredentialingRequests = $filter('filter')($rootScope.CredentialingRequests, { CurrentStatus: "Active" });
            $scope.MasterSettings = $filter("MasterSettingFiltter")("Requests");
            $scope.tableStateValueCredentialingRequest = UpdateAndRenewalsFactory.resetTableState($scope.tableStateValueCredentialingRequest);
            $self.callServerCredentialingRequest($scope.tableStateValueCredentialingRequest);
        }, function (error) {
            toaster.pop('error', "", 'Please try after sometime !!!');
        })
    }
    $scope.GetProfileUpdateDataByID = function (Data) {
        $scope.LoadingStatus = true;
        $scope.RejectStatus = false;
        $rootScope.TemporaryObject = angular.copy(Data);
        $rootScope.TemporaryObject.UniqueData = JSON.parse($rootScope.TemporaryObject.UniqueData);
        var data = {
            trackerId: Data.ProfileUpdatesTrackerId,
            status: Data.ApprovalStatus,
            modificationType: Data.Modification
        };
        UpdateAndRenewalsService.GetProfileUpdateDataByID(data).then(function (result) {
            $scope.displayDataForProfileUpdate = UpdateAndRenewalsFactory.getValue(result.data);
            $scope.LoadingStatus = false;
        }, function (error) {
            UpdateAndRenewalsFactory.modalDismiss();
            $scope.LoadingStatus = false;
            toaster.pop('error', "", 'Please try after sometime !!!');
        })
    }
    $scope.GetCredRequestDataByID = function (CredRequestID, StatusType) {
        $scope.LoadingStatus = true;
        $scope.RejectStatus = false;
        $rootScope.TemporaryObject = {};
        UpdateAndRenewalsService.GetCredRequestDataByID(CredRequestID, StatusType).then(function (result) {
            $rootScope.TemporaryObject = angular.copy(result.data);
            $scope.LoadingStatus = false;
        }, function (error) {
            UpdateAndRenewalsFactory.modalDismiss();
            $scope.LoadingStatus = false;
            toaster.pop('error', "", 'Please try after sometime !!!');
        })
    }
    $scope.SetDecesionForCredRequestByID = function (ID, ApprovalType) {

        $scope.SavingStatus = true;
        var data = {
            ID: ID,
            ApprovalType: ApprovalType,
            Reason: $rootScope.TemporaryObject.RejectionReason === undefined ? "" : $rootScope.TemporaryObject.RejectionReason,
        }
        UpdateAndRenewalsService.SetDecesionForCredRequestByID(data).then(function (result) {
            $rootScope.CredentialingRequests[$rootScope.CredentialingRequests.indexOf($filter('filter')($rootScope.CredentialingRequests, { CredentialingRequestID: ID })[0])].CurrentStatus = ApprovalType;
            $rootScope.TempCredentialingRequests.splice($rootScope.TempCredentialingRequests.indexOf($filter('filter')($rootScope.TempCredentialingRequests, { CredentialingRequestID: ID })[0]), 1);
            $scope.tableStateValueCredentialingRequest = UpdateAndRenewalsFactory.resetTableState($scope.tableStateValueCredentialingRequest);
            $self.callServerCredentialingRequest($scope.tableStateValueCredentialingRequest);
            $scope.SavingStatus = false;
            UpdateAndRenewalsFactory.modalDismiss();
            toaster.pop('Success', "Success", ApprovalType);

        }, function (error) {
            UpdateAndRenewalsFactory.modalDismiss();
            toaster.pop('error', "", 'Please try after sometime !!!');
        })
    }
    $scope.SetRejectionDropAndOnHoldForProfileUpdatesAndRenewal = function (Data, ApprovalStatus) {
        $scope.SavingStatus = true;
        var data = {
            tracker: {
                TrackerId: Data.ProfileUpdatesTrackerId,
                ApprovalStatus: ApprovalStatus,
                RejectionReason: $rootScope.TemporaryObject.RejectionReason === undefined ? "" : $rootScope.TemporaryObject.RejectionReason
            },
            modification: Data.Modification,
            approvedStatus: Data.ApprovalStatus
        };
        UpdateAndRenewalsService.SetApprovalForProfileUpdatesAndRenewal(data).then(function (result) {
            $rootScope.ProfileUpdates[$rootScope.ProfileUpdates.indexOf($filter('filter')($rootScope.ProfileUpdates, { ProfileUpdatesTrackerId: Data.ProfileUpdatesTrackerId })[0])].ApprovalStatus = ApprovalStatus;
            if (ApprovalStatus == "Rejected" || ApprovalStatus == "Dropped") {
                $scope.BicuitValue.HistoryCount += 1
                if (Data.Modification == "Update") {
                    $scope.BicuitValue.UpdateCount -= 1;

                }
                else {
                    $scope.BicuitValue.RenewalCount -= 1;

                }
                $rootScope.TempProfileUpdates.splice($rootScope.TempProfileUpdates.indexOf($filter('filter')($rootScope.TempProfileUpdates, { ProfileUpdatesTrackerId: Data.ProfileUpdatesTrackerId })[0]), 1);
            }
            else {
                $rootScope.TempProfileUpdates[$rootScope.TempProfileUpdates.indexOf($filter('filter')($rootScope.TempProfileUpdates, { ProfileUpdatesTrackerId: Data.ProfileUpdatesTrackerId })[0])].ApprovalStatus = ApprovalStatus;
            }
            $scope.tableStateValueUpdateAndHistory = UpdateAndRenewalsFactory.resetTableState($scope.tableStateValueUpdateAndHistory);
            $self.callServerUpdateAndHistory($scope.tableStateValueUpdateAndHistory);
            $scope.SavingStatus = false;
            UpdateAndRenewalsFactory.modalDismiss();
            toaster.pop('Success', "Success", ApprovalStatus);
        }, function (error) {
            $scope.SavingStatus = false;
            UpdateAndRenewalsFactory.modalDismiss();
            toaster.pop('error', "", 'Please try after sometime !!!');
        })
    }
    $scope.SetApprovalForProfileUpdatesAndRenewal = function (Data, ApprovalStatus) {
        $scope.SavingStatus = true;
        var Approvaldata = UpdateAndRenewalsFactory.CreateTrackerObject(Data, ApprovalStatus, $rootScope.TemporaryObject)
        //Data = UpdateAndRenewalsFactory.ResizeTheConvertedData(Data);
        UpdateAndRenewalsService.ApprovalServices(Data).then(function (result) {
            UpdateAndRenewalsService.SetApprovalForProfileUpdatesAndRenewal(Approvaldata).then(function (result) {
                $rootScope.ProfileUpdates[$rootScope.ProfileUpdates.indexOf($filter('filter')($rootScope.ProfileUpdates, { ProfileUpdatesTrackerId: Data.ProfileUpdatesTrackerId })[0])].ApprovalStatus = ApprovalStatus;
                $scope.BicuitValue.HistoryCount += 1
                if (Data.Modification == "Update") {
                    $scope.BicuitValue.UpdateCount -= 1;

                }
                else {
                    $scope.BicuitValue.RenewalCount -= 1;

                }
                $rootScope.TempProfileUpdates.splice($rootScope.TempProfileUpdates.indexOf($filter('filter')($rootScope.TempProfileUpdates, { ProfileUpdatesTrackerId: Data.ProfileUpdatesTrackerId })[0]), 1);
                $scope.tableStateValueUpdateAndHistory = UpdateAndRenewalsFactory.resetTableState($scope.tableStateValueUpdateAndHistory);
                $self.callServerUpdateAndHistory($scope.tableStateValueUpdateAndHistory);
                $scope.SavingStatus = false;
                UpdateAndRenewalsFactory.modalDismiss();
                toaster.pop('Success', "Success", 'Approved');
            }, function (error) {
                $scope.SavingStatus = false;
                UpdateAndRenewalsFactory.modalDismiss();
                toaster.pop('error', "", 'Please try after sometime !!!');
            })
        }, function (error) {
            $scope.SavingStatus = false;
            UpdateAndRenewalsFactory.modalDismiss();
            toaster.pop('error', "", 'Please try after sometime !!!');
        })

    }
    $scope.SetApprovalForAll = function () {
        if ($scope.ApproveAllStatus) {
            return;
        }
        $scope.ApproveAllStatus = true;
        UpdateAndRenewalsService.MultipleAsyncServices($scope.SelectedData).then(function (Mainresults) {
            var ProfileUpdaetIDs = $loadash.map($loadash.filter(Mainresults, { Status: true }), 'ID');
            if (ProfileUpdaetIDs.length > 0) {
                var obj = { ProfileUpdatesTrackerIds: ProfileUpdaetIDs }
                UpdateAndRenewalsService.SetMultipleApproval(obj).then(function (results) {
                    $scope.ApproveAllStatus = false;
                    $scope.BicuitValue.HistoryCount += ProfileUpdaetIDs.length
                    if ($scope.SelectedData[0].Modification == "Update") {
                        $scope.BicuitValue.UpdateCount -= ProfileUpdaetIDs.length;

                    }
                    else {
                        $scope.BicuitValue.RenewalCount -= ProfileUpdaetIDs.length;

                    }
                    for (i in ProfileUpdaetIDs) {
                        $rootScope.ProfileUpdates[$rootScope.ProfileUpdates.indexOf($filter('filter')($rootScope.ProfileUpdates, { ProfileUpdatesTrackerId: ProfileUpdaetIDs[i] })[0])].ApprovalStatus = "Approved";
                        $rootScope.TempProfileUpdates[$rootScope.TempProfileUpdates.indexOf($filter('filter')($rootScope.TempProfileUpdates, { ProfileUpdatesTrackerId: ProfileUpdaetIDs[i] })[0])].ApprovalStatus = "Approved";
                        $rootScope.TempProfileUpdates.splice($rootScope.TempProfileUpdates.indexOf($filter('filter')($rootScope.TempProfileUpdates, { ProfileUpdatesTrackerId: ProfileUpdaetIDs[i] })[0]), 1);
                    }
                    for (i in $rootScope.TempProfileUpdates) {
                        $rootScope.TempProfileUpdates[i].IsSelected = "false";
                    }
                    $scope.tableStateValueUpdateAndHistory = UpdateAndRenewalsFactory.resetTableState($scope.tableStateValueUpdateAndHistory);
                    $self.callServerUpdateAndHistory($scope.tableStateValueUpdateAndHistory);
                    toaster.pop('Success', "Success", ProfileUpdaetIDs.length+" Appprovd");
                    if (ProfileUpdaetIDs.length != Mainresults.length) {
                        toaster.pop('error', "", (Mainresults.length - ProfileUpdaetIDs.length + ' Failed ' + 'Please try after sometime !!!'));
                    }

                }, function (errors) {
                    for (i in $rootScope.TempProfileUpdates) {
                        $rootScope.TempProfileUpdates[i].IsSelected = "false";
                    }
                    $scope.ApproveAllStatus = false;
                    $scope.SelectedData = [];
                })
            }
            else {
                for (i in $rootScope.TempProfileUpdates) {
                    $rootScope.TempProfileUpdates[i].IsSelected = "false";
                }
                $scope.SelectedData = [];
                $scope.ApproveAllStatus = false;
                toaster.pop('error', "", (Mainresults.length - ProfileUpdaetIDs.length + ' Failed ' + 'Please try after sometime !!!'));
            }
        },
        function (errors) {
            for (i in $rootScope.TempProfileUpdates) {
                $rootScope.TempProfileUpdates[i].IsSelected = "false";
            }
            $scope.SelectedData = [];
            $scope.ApproveAllStatus = false;
            toaster.pop('error', "", 'Please try after sometime !!!');
        })
    }

    //==================================== Crud Operations Start =========================================

    //==================================== Real time Updates Start ==============================================

    // Declare a proxy to reference the hub.
    //var cnd = $.connection.cnDHub;

    //$.connection.hub.start().done(function () {
    //    //cnd.server.notifyPendingRequest();
    //});

    //cnd.client.NotifyPendingRequestData = function () {

    //    if ($scope.MasterSettings.ActionType == "Updates") {
    //        $scope.GetAllUpdatesAndRenewals($scope.MasterSettings.ActionType);
    //    } else if ($scope.MasterSettings.ActionType == "Renewals") {
    //        $scope.GetAllUpdatesAndRenewals($scope.MasterSettings.ActionType);
    //    }
    //    else if ($scope.MasterSettings.ActionType == "Requests") {
    //        GetAllCredentialingRequests($scope.MasterSettings.ActionType);
    //    }
    //}

    //==================================== Real time Updates end =============================================
}]);