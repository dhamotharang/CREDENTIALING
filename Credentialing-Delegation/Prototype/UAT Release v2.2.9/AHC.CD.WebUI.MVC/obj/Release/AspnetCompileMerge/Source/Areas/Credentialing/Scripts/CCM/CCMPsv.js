

CCMActionApp.controller('psvController', function ($scope, $http, $filter) {

    $scope.toggleDiv = function (divId) {
        $('#' + divId).slideToggle();
    };

    $scope.ConvertDateFormat1 = function (value) {
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

    //================http ==============
    $scope.checkForHttp = function (value) {
        if (value.indexOf('http') > -1) {
            value = value;
        } else {
            value = 'http://' + value;
        }
        var open_link = window.open('', '_blank');
        open_link.location = value;
    }

    //=========================GetAllProfileVerificationParameter======================
    $scope.GetProfileVerificationParameter = function () {

        $http.get('/Profile/MasterData/GetAllProfileVerificationParameter').
        success(function (data, status, headers, config) {


            for (var i = 0; i < data.length; i++) {
                if (data[i].Code == 'SL') {
                    $scope.StateLicenseParameterID = data[i].ProfileVerificationParameterId;
                }
                if (data[i].Code == 'BC') {
                    $scope.BoardCertificationParameterID = data[i].ProfileVerificationParameterId;
                }
                if (data[i].Code == 'DEA') {
                    $scope.DEAParameterID = data[i].ProfileVerificationParameterId;
                }
                if (data[i].Code == 'CDS') {
                    $scope.CDSParameterID = data[i].ProfileVerificationParameterId;
                }
                if (data[i].Code == 'NPDB') {
                    $scope.NPDBParameterID = data[i].ProfileVerificationParameterId;
                }
                if (data[i].Code == 'MOPT') {
                    $scope.MOPTParameterID = data[i].ProfileVerificationParameterId;
                }
                if (data[i].Code == 'OIG') {
                    $scope.OIGParameterID = data[i].ProfileVerificationParameterId;
                }
            }

        }).
        error(function (data, status, headers, config) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
        });
    };

    $scope.FormattedData = [];
   
    $scope.loadData = function (id) {

        $http.get('/Credentialing/Verification/GetPSVReport?credinfoId='+id).
        success(function (data, status, headers, config) {

            if (data.status == "true") {
                $scope.showPsvError = false;
                $scope.FormatData(data.psvReport);
            }
            else {
                $scope.showPsvError = true;
                //messageAlertEngine.callAlertMessage("psvReportError", data.status, "danger", true);
            }          
            
            
        }).
        error(function (data, status, headers, config) {
            messageAlertEngine.callAlertMessage("psvReportError", "Sorry Unable To get PSV Report !!!!", "danger", true);
        });
    };

    //--------------------data format------------------------
    $scope.FormatData = function (data) {

        var formattedData = [];
        for (var i in data) {
            var VerificationData=new Object();
            if (data[i].VerificationData!=null)
                VerificationData = jQuery.parseJSON(data[i].VerificationData);
            var VerificationDate = $scope.ConvertDateFormat1(data[i].VerificationDate)
            
            formattedData.push({ Id: data[i].ProfileVerificationParameterId, info: { ProfileVerficationParameterObj: data[i].ProfileVerificationParameter, VerificationResultObj: data[i].VerificationResult, VerificationData: VerificationData, VerificationDate: VerificationDate } });
            //formattedData.push({ Id: data[i].ProfileVerificationParameterId, info: { ProfileVerficationParameterObj: data[i].ProfileVerificationParameter, VerificationResultObj: data[i].VerificationResult, VerificationData: data[i].VerificationData } });
        }

        var UniqueIds = [];
        UniqueIds.push(formattedData[0].Id);
        for (var i = 1; i < formattedData.length; i++) {

            var CurrObj = formattedData[i];
            var flag = 0;
            for (var j = 0; j < UniqueIds.length; j++) {
                if (CurrObj.Id == UniqueIds[j])
                {
                    flag = 1;
                }
            }
            if (flag == 0) {
                UniqueIds.push(CurrObj.Id);
            }
        }
        

        for (var i = 0; i < UniqueIds.length; i++) {
            var info=[];
            for (var j = 0; j < formattedData.length; j++) {
                if (UniqueIds[i] == formattedData[j].Id) {
                    info.push(formattedData[j].info);
                }

            }
            $scope.FormattedData.push({ Id: UniqueIds[i], Info: info });
        }
    };

    //=======================PSV ends=========================================





});
