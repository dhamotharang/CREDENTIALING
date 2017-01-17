var LicenseNumberApp = angular.module('LicenseNumberApp', ['ui.bootstrap', 'angularUtils.directives.dirPagination']);

LicenseNumberApp.controller('LicenseNumberCtrl', function ($scope, $http) {

    $http.get('/IdentificationLicense/GetAllData').success(function (data) {

        console.log(data);
        FormatData(data);
    });

    $scope.TableData = [];
    var FormatData = function (data) {

        for (var i = 0; i < data.length; i++) {
            var currentObj = new Object();
            if (data[i].OtherIdentificationNumber != null) {
                if (data[i].OtherIdentificationNumber.NPINumber) {

                    currentObj.NPINumber = data[i].OtherIdentificationNumber.NPINumber;
                }
                else {
                    currentObj.NPINumber = '';
                }
                if (data[i].OtherIdentificationNumber.UPINNumber) {

                    currentObj.UPINNumber = data[i].OtherIdentificationNumber.UPINNumber;
                }
                else {
                    currentObj.UPINNumber = '';
                }
                if (data[i].OtherIdentificationNumber.USMLENumber) {

                    currentObj.USMLENumber = data[i].OtherIdentificationNumber.USMLENumber;
                }
                else {
                    currentObj.USMLENumber = '';
                }
                if (data[i].OtherIdentificationNumber.CAQHNumber) {

                    currentObj.CAQHNumber = data[i].OtherIdentificationNumber.CAQHNumber;
                }
                else {
                    currentObj.CAQHNumber = '';
                }
            }

            if (data[i].PersonalDetail != null) {
                currentObj.Name = data[i].PersonalDetail.FirstName + ' ' + data[i].PersonalDetail.LastName;
            }
            var stateLicense = '';
            if (data[i].StateLicenses.length != 0) {
                stateLicense = data[i].StateLicenses[0].LicenseNumber;
                for (var j = 1; j < data[i].StateLicenses.length; j++) {
                    if (data[i].StateLicenses[j].Status == 'Active')
                        stateLicense = stateLicense + ', ' + data[i].StateLicenses[j].LicenseNumber;
                }
                currentObj.StateLicenseNumber = stateLicense;
            }

            var DEANumber = '';
            if (data[i].FederalDEAInformations.length != 0) {
                DEANumber = data[i].FederalDEAInformations[0].DEANumber;
                for (var j = 1; j < data[i].FederalDEAInformations.length; j++) {

                    if (data[i].FederalDEAInformations[j].Status == 'Active')
                        DEANumber = DEANumber + ', ' + data[i].FederalDEAInformations[j].DEANumber;
                }
                currentObj.DEANumber = DEANumber;
            }


            var MedicaidNumber = '';
            if (data[i].MedicaidInformations.length != 0) {
                MedicaidNumber = data[i].MedicaidInformations[0].LicenseNumber;
                for (var j = 1; j < data[i].MedicaidInformations.length; j++) {

                    if (data[i].MedicaidInformations[j].Status == 'Active')
                        MedicaidNumber = MedicaidNumber + ', ' + data[i].MedicaidInformations[j].LicenseNumber;
                }
                currentObj.MedicaidNumber = MedicaidNumber;
            }

            var MedicareNumber = '';
            if (data[i].MedicareInformations.length != 0) {
                MedicareNumber = data[i].MedicareInformations[0].LicenseNumber;
                for (var j = 1; j < data[i].MedicareInformations.length; j++) {

                    if (data[i].MedicareInformations[j].Status == 'Active')
                        MedicareNumber = MedicareNumber + ', ' + data[i].MedicareInformations[j].LicenseNumber;
                }
                currentObj.MedicareNumber = MedicareNumber;
            }

            var CDSNumber = '';
            if (data[i].CDSCInformations.length != 0) {
                CDSNumber = data[i].CDSCInformations[0].CertNumber;
                for (var j = 1; j < data[i].CDSCInformations.length; j++) {

                    if (data[i].CDSCInformations[j].Status == 'Active')
                        CDSNumber = CDSNumber + ', ' + data[i].CDSCInformations[j].CertNumber;
                }
                currentObj.CDSNumber = CDSNumber;
            }

            $scope.TableData.push(currentObj);
        }

        console.log($scope.TableData);
    }


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
        if ($scope.Hospitals) {
            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.Hospitals[startIndex]) {
                    $scope.CurrentPage.push($scope.Hospitals[startIndex]);
                } else {
                    break;
                }
            }
        }
    });
    //-------------- License Scope Watch ---------------------
    $scope.$watchCollection('TableData', function (newValue, oldValue) {
        if (newValue) {
            $scope.bigTotalItems = newValue.length;

            $scope.CurrentPage = [];
            $scope.bigCurrentPage = 1;

            var startIndex = ($scope.bigCurrentPage - 1) * 10;
            var endIndex = startIndex + 9;

            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.TableData[startIndex]) {
                    $scope.CurrentPage.push($scope.TableData[startIndex]);
                } else {
                    break;
                }
            }
        }
    });
    $scope.filterData = function () {
        $scope.pageChanged(1);
    }

    $scope.IsValidIndex = function (index) {

        var startIndex = ($scope.bigCurrentPage - 1) * 10;
        var endIndex = startIndex + 9;

        if (index >= startIndex && index <= endIndex)
            return true;
        else
            return false;

    }


    //------------------- end ------------------
});