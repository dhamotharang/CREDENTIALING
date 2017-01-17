//--------------------- Angular Module ----------------------
var masterDataMilitaryRanks = angular.module("masterDataMilitaryRanks", ['ui.bootstrap']);


masterDataMilitaryRanks.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

    $rootScope.messageDesc = "";
    $rootScope.activeMessageDiv = "";
    $rootScope.messageType = "";

    var animateMessageAlertOff = function () {
        $rootScope.closeAlertMessage();
    };


    this.callAlertMessage = function (calledDiv, msg, msgType, dismissal) { //messageAlertEngine.callAlertMessage('updateHospitalPrivilege' + IndexValue, "Data Updated Successfully !!!!", "success", true);                            
        $rootScope.activeMessageDiv = calledDiv;
        $rootScope.messageDesc = msg;
        $rootScope.messageType = msgType;
        if (dismissal) {
            $timeout(animateMessageAlertOff, 5000);
        }
    }

    $rootScope.closeAlertMessage = function () {
        $rootScope.messageDesc = "";
        $rootScope.activeMessageDiv = "";
        $rootScope.messageType = "";
    }
}])


//=========================== Controller declaration ==========================
masterDataMilitaryRanks.controller('masterDataMilitaryRanksController', ['$scope', '$http', '$filter', 'messageAlertEngine', function ($scope, $http, $filter, messageAlertEngine) {

    $http.get(rootDir + "/MasterDataNew/GetAllMilitaryBranches").then(function (value) {
        
        for (var i = 0; i < value.data.length ; i++) {
            if (value.data[i] != null) {
                value.data[i].LastModifiedDate = $scope.ConvertDateFormat(value.data[i].LastModifiedDate);
            }
        }

        $scope.MilitaryBranchs = angular.copy(value.data);
    });

    $http.get(rootDir + "/MasterDataNew/GetAllMilitaryRanks").then(function (value) {
       
        for (var i = 0; i < value.data.length ; i++) {
            if (value.data[i] != null) {
                value.data[i].LastModifiedDate = $scope.ConvertDateFormat(value.data[i].LastModifiedDate);
            }
        }

        $scope.MilitaryRanks = angular.copy(value.data);
    });

    //Convert the date from database to normal
    $scope.ConvertDateFormat = function (value) {
        var returnValue = value;
        var dateData = "";

        try {
            if (value.indexOf("/Date(") == 0) {
                returnValue = new Date(parseInt(value.replace("/Date(", "").replace(")/", ""), 10));
                var day = returnValue.getDate() < 10 ? '0' + returnValue.getDate() : returnValue.getDate();
                var tempo = returnValue.getMonth() + 1;
                var month = tempo < 10 ? '0' + tempo : tempo;
                var year = returnValue.getFullYear();
                dateData = month + "-" + day + "-" + year;
            }
            return dateData;
        } catch (e) {
            return dateData;
        }
        return dateData;
    };

    

    $scope.tempMilitaryRanks = {};
    $scope.tempID = 0;
    //------------ gets the template to ng-include for a table row / item -------------------
    $scope.getTemplate = function (MilitaryRanks) {

        if ($scope.tempID == 0) {
        if (MilitaryRanks.MilitaryRankID === $scope.tempMilitaryRanks.MilitaryRankID) return 'editMilitaryRanks';
        else return 'displayMilitaryRanks';

        }
        else {
            if (MilitaryRanks.MilitaryRankID === $scope.tempMilitaryRanks.MilitaryRankID && MilitaryRanks.MilitaryBranchID === $scope.tempMilitaryRanks.MilitaryBranchID) return 'editMilitaryRanks';
            else return 'displayMilitaryRanks';
        }

        
    };

    //-------------------- Edit MilitaryRank ----------
    $scope.editMilitaryRanks = function (MilitaryRank) {
        $scope.disableAdd = true;
        $scope.hide = true;
        $scope.tempMilitaryRanks = angular.copy(MilitaryRank);
        $scope.tempID = 1;
    };

    //------------------- Add MilitaryRank ---------------------
    $scope.addMilitaryRanks = function (MilitaryRank) {
        $scope.disableEdit = true;        
        $scope.disableAdd = true;
        $scope.hide = false;
        var temp = {
            MilitaryRankID: 0,
            MilitaryBranchID: 0,            
            MilitaryRankTitle: "",
            MilitaryBranchTitle: "",
            Status: "Active",
            LastModifiedDate: new Date()
        };
        $scope.MilitaryRanks.splice(0, 0, angular.copy(temp));
        $scope.tempMilitaryRanks = angular.copy(temp);
        $scope.tempID = 0;
    };

    //------------------- Save MilitaryRank ---------------------
    $scope.saveMilitaryRanks = function (idx) {

        var addData = {
            MilitaryRankID: 0,            
            Title: $scope.tempMilitaryRanks.MilitaryRankTitle,
            MilitaryBranch:{
                MilitaryBranchID: $scope.tempMilitaryRanks.MilitaryBranchID,
                Title:$scope.tempMilitaryRanks.MilitaryBranchTitle
            },
            StatusType:1,
            LastModifiedDate: new Date()
        };
        $scope.rankError = "";
        $scope.rankbranchError = "";
        $scope.existErr = "";
       
        var isExist = true;
       
        for (var i = 0; i < $scope.MilitaryRanks.length; i++) {

            if (addData.Title && $scope.MilitaryRanks[i].MilitaryRankTitle.replace(" ", "").toLowerCase() == addData.Title.replace(" ", "").toLowerCase()) {

                isExist = false;
                $scope.existErr = "The Title with same Branch Name is Exist";
                break;
            }
          
        }



        if (!addData.Title) {
            $scope.rankError = "Please enter the Title";
            
        }
        else if (!addData.MilitaryBranchTitle)
        {
            $scope.rankbranchError = "Please select the Branch";

       }
        else if(addData.Title)
        {
            $http.post(rootDir + '/MasterDataNew/AddMilitaryRank', addData).
            success(function (data, status, headers, config) {
                try {
                    //----------- success message -----------
                    if (data.status == "true") {
                        messageAlertEngine.callAlertMessage("Rank", "New Military Rank Details Added Successfully !!!!", "success", true);
                        data.militaryRankDetails.LastModifiedDate = $scope.ConvertDateFormat(data.militaryRankDetails.LastModifiedDate);
                        for (var i = 0; i < $scope.MilitaryRanks.length; i++) {

                            if ($scope.MilitaryRanks[i].MilitaryRankID == 0) {
                                //$scope.MilitaryRanks[i] = angular.copy(data.militaryRankDetails);
                                $scope.MilitaryRanks[i].MilitaryRankID = data.militaryRankDetails.MilitaryRankID;
                                $scope.MilitaryRanks[i].MilitaryBranchID = data.militaryRankDetails.MilitaryBranches[0].MilitaryBranchID;
                                $scope.MilitaryRanks[i].MilitaryRankTitle = data.militaryRankDetails.Title;
                                $scope.MilitaryRanks[i].MilitaryBranchTitle = data.militaryRankDetails.MilitaryBranches[0].Title;
                                $scope.MilitaryRanks[i].Status = data.militaryRankDetails.Status;
                                $scope.MilitaryRanks[i].StatusType = data.militaryRankDetails.StatusType;
                                $scope.MilitaryRanks[i].LastModifiedDate = data.militaryRankDetails.LastModifiedDate;
                                break;

                            }
                        }
                        $scope.reset();
                    }
                } catch (e) {
                  
                }
            }).
            error(function (data, status, headers, config) {
                //----------- error message -----------
                messageAlertEngine.callAlertMessage("RankError", "Sorry Unable To Add Military Rank !!!!", "danger", true);
            });
        }
    };

    //------------------- Update MilitaryRank ---------------------
    $scope.UpdateMilitaryRanks = function (idx) {

        var updateData = {
            MilitaryRankID: $scope.tempMilitaryRanks.MilitaryRankID,
            MilitaryBranch: {},            
            Title: $scope.tempMilitaryRanks.MilitaryRankTitle,
            StatusType: 1,            
        };
        $scope.rankError = "";
        $scope.rankbranchError = "";
        $scope.existErr = "";
        $http.post(rootDir + '/MasterDataNew/UpdateMilitaryRank', updateData).
            success(function (data, status, headers, config) {
                //----------- success message -----------
                try {
                    if (data.status == "true") {
                        messageAlertEngine.callAlertMessage("Rank", "Military Rank Details Updated Successfully !!!!", "success", true);
                        data.militaryRankDetails.LastModifiedDate = $scope.ConvertDateFormat(data.militaryRankDetails.LastModifiedDate);
                        for (var i = 0; i < $scope.MilitaryRanks.length; i++) {

                            if ($scope.MilitaryRanks[i].MilitaryRankID == data.militaryRankDetails.MilitaryRankID) {
                                //$scope.MilitaryRanks[i] = angular.copy(data.militaryRankDetails);
                                $scope.MilitaryRanks[i].MilitaryRankTitle = data.militaryRankDetails.Title;
                                $scope.MilitaryRanks[i].Status = data.militaryRankDetails.Status;
                                $scope.MilitaryRanks[i].StatusType = data.militaryRankDetails.StatusType;
                                $scope.MilitaryRanks[i].LastModifiedDate = data.militaryRankDetails.LastModifiedDate;
                            }
                        }
                        $scope.reset();
                    }
                } catch (e) {
                    throw e;
                }
            }).
            error(function (data, status, headers, config) {
                //----------- error message -----------
                messageAlertEngine.callAlertMessage("RankError", "Sorry Unable To Update Military Rank !!!!", "danger", true);
            });
    };

    //----------------- MilitaryRank new add cancel ---------------
    $scope.cancelAdd = function () {
        $scope.disableAdd = false;
        $scope.disableEdit = false;
        $scope.MilitaryRanks.splice(0, 1);
        $scope.tempMilitaryRanks = {};
        $scope.hide = false;
        $scope.rankError = "";
        $scope.rankbranchError = "";
        $scope.existErr = "";
    };

    //-------------------- Reset MilitaryRank ----------------------
    $scope.reset = function () {
        $scope.disableAdd = false;
        $scope.disableEdit = false;
        $scope.tempMilitaryRanks = {};
        $scope.hide = false;
        $scope.rankError = "";
        $scope.rankbranchError = "";
        $scope.existErr = "";
    };

    //----------------- get Military Branch ---------------------
    $scope.getMilitaryBranch = function (branchID) {        

        var obj = $filter('filter')($scope.MilitaryBranchs, { MilitaryBranchID: branchID })[0];
        $scope.tempMilitaryRanks.MilitaryBranchTitle = obj.Title;
        $scope.tempMilitaryRanks.MilitaryBranchID = obj.MilitaryBranchID;
    };


    $scope.CurrentPage = [];

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
        $scope.CurrentPage = [];
        var startIndex = (newValue - 1) * 10;
        var endIndex = startIndex + 9;
        if ($scope.MilitaryRanks) {
            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.MilitaryRanks[startIndex]) {
                    $scope.CurrentPage.push($scope.MilitaryRanks[startIndex]);
                } else {
                    break;
                }
            }
        }
    });
    //-------------- License Scope Watch ---------------------
    $scope.$watchCollection('MilitaryRanks', function (newValue, oldValue) {
        if (newValue) {
            $scope.bigTotalItems = newValue.length;

            $scope.CurrentPage = [];
            $scope.bigCurrentPage = 1;

            var startIndex = ($scope.bigCurrentPage - 1) * 10;
            var endIndex = startIndex + 9;

            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.MilitaryRanks[startIndex]) {
                    $scope.CurrentPage.push($scope.MilitaryRanks[startIndex]);
                } else {
                    break;
                }
            }
        }
    });
    //------------------- end ------------------

}]);
