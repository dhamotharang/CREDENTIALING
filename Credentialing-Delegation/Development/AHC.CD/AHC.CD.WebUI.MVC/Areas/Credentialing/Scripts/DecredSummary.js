$(document).ready(function () {
   
    $("#sidemenu").addClass("menu-in");
    $("#page-wrapper").addClass("menuup");


});

DeCred_SPA_App.controller('DecredSummaryController',
    function ($scope, $timeout, $http) {

        //$scope.id = Profileid;
       
        $scope.CredentialingInfo = [];
        $scope.progressbar = true;

        $scope.ConvertDateFormat = function (value) {
            
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

        $scope.getCredentialingInfo = function () {
            $scope.dataLoaded = false;
            if (!$scope.dataLoaded) {
                $http({
                    method: 'GET',
                    url: rootDir + '/Credentialing/DeCredentialing/GetDeCredentialingInfoAsync?ProfileID=' + Profileid
                }).success(function (data, status, headers, config) {
                   
                    try {

                        if (data.credInfo != null) {

                            $scope.CredentialingInfo = data.credInfo;

                            $scope.fillDecred(data.credInfo);
                            $scope.fillPlan(data.credInfo);
                            $scope.fillProfile(data.credInfo);
                            $scope.fillLob($scope.DecredInfo);

                            $scope.progressbar = false;
                            //$scope.getCurrentActivity(data.credInfo);
                        }

                    } catch (e) {
                     
                    }
                }).error(function (data, status, headers, config) {
                   
                });
                $scope.dataLoaded = true;
            }
        };

        $scope.ProfileData = {};

        $scope.fillProfile = function (data) {

            if (data != null && data.length != 0) {

                $scope.ProfileData = data[0].Profile;
               

            }

        }

        $scope.DecredInfo = [];

        $scope.fillDecred = function (data) {

            if (data != null && data.length != 0) {

                $scope.ProfileID = data.ProfileID;

                for (var i = 0; i < data.length; i++) {

                    if (data[i].CredentialingContractRequests != null && data[i].CredentialingContractRequests != 0) {

                        for (var j = 0; j < data[i].CredentialingContractRequests.length; j++) {

                            if (data[i].CredentialingContractRequests[j].ContractGrid != null && data[i].CredentialingContractRequests[j].ContractGrid != 0) {

                                for (var k = 0; k < data[i].CredentialingContractRequests[j].ContractGrid.length; k++) {

                                    if (data[i].CredentialingContractRequests[j].ContractGrid[k].Report != null) {

                                        $scope.DecredInfo.push(data[i].CredentialingContractRequests[j].ContractGrid[k]);
                                        $scope.DecredInfo[$scope.DecredInfo.length - 1].FirstName = data[i].FirstName;
                                        $scope.DecredInfo[$scope.DecredInfo.length - 1].LastName = data[i].LastName;
                                        $scope.DecredInfo[$scope.DecredInfo.length - 1].CredentialingInfoID = data[i].CredentialingInfoID;
                                        $scope.DecredInfo[$scope.DecredInfo.length - 1].CredentialingContractRequestID = data[i].CredentialingContractRequests[j].CredentialingContractRequestID;
                                        $scope.DecredInfo[$scope.DecredInfo.length - 1].Plan = data[i].Plan;
                                        $scope.DecredInfo[$scope.DecredInfo.length - 1].Check = false;
                                        $scope.DecredInfo[$scope.DecredInfo.length - 1].InitiationDate = $scope.ConvertDateFormat(data[i].InitiationDate);
                                        $scope.DecredInfo[$scope.DecredInfo.length - 1].Report.TerminationDate = $scope.ConvertDateFormat($scope.DecredInfo[$scope.DecredInfo.length - 1].Report.TerminationDate);

                                    }

                                }

                            }

                        }

                    }

                    //$scope.profileDetail.push($scope.data[i]);

                }

            }

            

        }

        $scope.planInfo = [];

        $scope.fillPlan = function (data) {

            if (data != null && data.length != 0) {

                for (var i = 0; i < data.length; i++) {

                    if (data[i].Plan != null) {

                        $scope.planInfo.push(data[i].Plan);

                    }

                }

            }

        }

        $scope.LobInfo = [];

        $scope.fillLob = function (data) {

            if (data != null && data.length != 0) {

                for (var i = 0; i < data.length; i++) {

                    if (data[i].LOB != null) {

                        if ($scope.LobInfo.length == 0) {

                            $scope.LobInfo.push(data[i].LOB);

                        }

                        if ($scope.LobInfo[$scope.LobInfo.length - 1].LOBID != data[i].LOB.LOBID) {

                            $scope.LobInfo.push(data[i].LOB);

                        }

                    }

                }

            }

        }

        $scope.cnt = 0;

        $scope.Count = function () {

            $scope.cnt++;

        }

    });