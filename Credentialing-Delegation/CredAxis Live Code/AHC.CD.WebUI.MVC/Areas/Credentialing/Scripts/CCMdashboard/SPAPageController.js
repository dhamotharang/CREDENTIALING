CCMDashboard.controller("SPAIndexController", ["$rootScope", "$scope", "toaster", "$timeout", "$filter", "$http", "CCMDashboardService", "CCMDashboardFactory", function ($rootScope, $scope, toaster, $timeout, $filter, $http, CCMDashboardService, CCMDashboardFactory) {
    $scope.Status = "dfsdf";
    //$scope.showreusesignaturediv = true;
    $scope.showuploaddiv = false;
    //$scope.showsignaturediv = false;
    $scope.errormessageforsignature = false;
    $scope.SavingStatus = false;
    $scope.errormessageforuploadsignature = false;
    // $scope.validatesignature ="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAASwAAACMCAYAAADGFpQvAAAEGUlEQVR4Xu3UAQkAAAwCwdm/9HI83BLIOdw5AgQIRAQWySkmAQIEzmB5AgIEMgIGK1OVoAQIGCw/QIBARsBgZaoSlAABg+UHCBDICBisTFWCEiBgsPwAAQIZAYOVqUpQAgQMlh8gQCAjYLAyVQlKgIDB8gMECGQEDFamKkEJEDBYfoAAgYyAwcpUJSgBAgbLDxAgkBEwWJmqBCVAwGD5AQIEMgIGK1OVoAQIGCw/QIBARsBgZaoSlAABg+UHCBDICBisTFWCEiBgsPwAAQIZAYOVqUpQAgQMlh8gQCAjYLAyVQlKgIDB8gMECGQEDFamKkEJEDBYfoAAgYyAwcpUJSgBAgbLDxAgkBEwWJmqBCVAwGD5AQIEMgIGK1OVoAQIGCw/QIBARsBgZaoSlAABg+UHCBDICBisTFWCEiBgsPwAAQIZAYOVqUpQAgQMlh8gQCAjYLAyVQlKgIDB8gMECGQEDFamKkEJEDBYfoAAgYyAwcpUJSgBAgbLDxAgkBEwWJmqBCVAwGD5AQIEMgIGK1OVoAQIGCw/QIBARsBgZaoSlAABg+UHCBDICBisTFWCEiBgsPwAAQIZAYOVqUpQAgQMlh8gQCAjYLAyVQlKgIDB8gMECGQEDFamKkEJEDBYfoAAgYyAwcpUJSgBAgbLDxAgkBEwWJmqBCVAwGD5AQIEMgIGK1OVoAQIGCw/QIBARsBgZaoSlAABg+UHCBDICBisTFWCEiBgsPwAAQIZAYOVqUpQAgQMlh8gQCAjYLAyVQlKgIDB8gMECGQEDFamKkEJEDBYfoAAgYyAwcpUJSgBAgbLDxAgkBEwWJmqBCVAwGD5AQIEMgIGK1OVoAQIGCw/QIBARsBgZaoSlAABg+UHCBDICBisTFWCEiBgsPwAAQIZAYOVqUpQAgQMlh8gQCAjYLAyVQlKgIDB8gMECGQEDFamKkEJEDBYfoAAgYyAwcpUJSgBAgbLDxAgkBEwWJmqBCVAwGD5AQIEMgIGK1OVoAQIGCw/QIBARsBgZaoSlAABg+UHCBDICBisTFWCEiBgsPwAAQIZAYOVqUpQAgQMlh8gQCAjYLAyVQlKgIDB8gMECGQEDFamKkEJEDBYfoAAgYyAwcpUJSgBAgbLDxAgkBEwWJmqBCVAwGD5AQIEMgIGK1OVoAQIGCw/QIBARsBgZaoSlAABg+UHCBDICBisTFWCEiBgsPwAAQIZAYOVqUpQAgQMlh8gQCAjYLAyVQlKgIDB8gMECGQEDFamKkEJEDBYfoAAgYyAwcpUJSgBAgbLDxAgkBEwWJmqBCVAwGD5AQIEMgIGK1OVoAQIGCw/QIBARsBgZaoSlAABg+UHCBDICBisTFWCEiBgsPwAAQIZAYOVqUpQAgQMlh8gQCAjYLAyVQlKgMADe7MAjcEgBREAAAAASUVORK5CYII="
    $scope.validatesignature = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAPgAAABiCAYAAAB9LB4uAAACaklEQVR4Xu3TAREAAAgCMelf2h5/swFDdo4AgazAsskEI0DgDNwTEAgLGHi4XNEIGLgfIBAWMPBwuaIRMHA/QCAsYODhckUjYOB+gEBYwMDD5YpGwMD9AIGwgIGHyxWNgIH7AQJhAQMPlysaAQP3AwTCAgYeLlc0AgbuBwiEBQw8XK5oBAzcDxAICxh4uFzRCBi4HyAQFjDwcLmiETBwP0AgLGDg4XJFI2DgfoBAWMDAw+WKRsDA/QCBsICBh8sVjYCB+wECYQEDD5crGgED9wMEwgIGHi5XNAIG7gcIhAUMPFyuaAQM3A8QCAsYeLhc0QgYuB8gEBYw8HC5ohEwcD9AICxg4OFyRSNg4H6AQFjAwMPlikbAwP0AgbCAgYfLFY2AgfsBAmEBAw+XKxoBA/cDBMICBh4uVzQCBu4HCIQFDDxcrmgEDNwPEAgLGHi4XNEIGLgfIBAWMPBwuaIRMHA/QCAsYODhckUjYOB+gEBYwMDD5YpGwMD9AIGwgIGHyxWNgIH7AQJhAQMPlysaAQP3AwTCAgYeLlc0AgbuBwiEBQw8XK5oBAzcDxAICxh4uFzRCBi4HyAQFjDwcLmiETBwP0AgLGDg4XJFI2DgfoBAWMDAw+WKRsDA/QCBsICBh8sVjYCB+wECYQEDD5crGgED9wMEwgIGHi5XNAIG7gcIhAUMPFyuaAQM3A8QCAsYeLhc0QgYuB8gEBYw8HC5ohEwcD9AICxg4OFyRSNg4H6AQFjAwMPlikbAwP0AgbCAgYfLFY2AgfsBAmEBAw+XKxoBA/cDBMICBh4uVzQCBu4HCIQFDDxcrmgEHgEaAGNMPbq1AAAAAElFTkSuQmCC";
    $scope.SubmitApproval = function (ActionType) {
        $scope.Status = ActionType;
        $scope.SubmitApprovalStatus = ActionType == "Approve" ? 1 : (ActionType == "Reject") ? 2 : 3;
        CCMDashboardFactory.ResetFormForValidation($("#CCMAction"));

        if ($("#selectedOptionforSign").val() == "digitalsignature") {
            var canvas;
            canvas = document.getElementById('myCanvas');
            $scope.image = canvas.toDataURL("image/png");
            if ($scope.image == document.getElementById('blank').toDataURL() || $scope.image == $scope.validatesignature)
                $scope.errormessageforsignature = true;
            else {
                $scope.errormessageforsignature = false;
                if ($("#CCMAction").valid()) {
                    $('#StatusAction').modal({
                        backdrop: 'static'
                    });
                }
            }
        }
        if ($("#selectedOptionforSign").val() == "reusedigitalsignature") {
            $scope.image = $rootScope.SignaturePath;
        }
        if ($("#selectedOptionforSign").val() != "digitalsignature") {
            if ($("#CCMAction").valid() == false && $("#selectedOptionforSign").val() == "upload")
                $scope.errormessageforuploadsignature = true;

            if ($("#CCMAction").valid()) {
                $scope.errormessageforuploadsignature = false;
                $('#StatusAction').modal({
                    backdrop: 'static'
                });
            }
        }

    }


    //--------saving the status of appointment ----
    $scope.ccmAction = function (Form_Id) {
        $scope.SavingStatus = true;
        if ($scope.errormessageforsignature == false) {
            var formdata = new FormData();
            var form5 = angular.element("#" + Form_Id).serializeArray();
            for (var i in form5) {
                formdata.append(form5[i].name, form5[i].value);
            }
            formdata.append("profileId", $rootScope.ProfileId);
            formdata.append(angular.element("#CredentialingAppointmentResult_SignatureFile")[0].name, angular.element("#CredentialingAppointmentResult_SignatureFile")[0].files[0]);

            CCMDashboardService.SaveAppointmentResult(formdata).then(function (response) {
                console.log(response);
                //$('#StatusAction').modal('hide'); //to close the modal
                CCMDashboardFactory.modalDismiss('StatusAction');
                if (response.data.status == "true") {
                    var message = $scope.Status == "Approve" ? "Provider Approved Successfully" : $scope.Status == "Reject" ? "Provider Rejected Successfully" : "Provider On-Holded Successfully";
                    angular.element('#closeExpendedRow').triggerHandler('click'); // to close the expanded in the table
                    toaster.pop('Success', "Success", message);
                    $rootScope.MasterData();
                  
                }
                else {
                    toaster.pop('error', "error", response.data.status);
                }
                $scope.SavingStatus = false;
            }, function (error) {
                $scope.SavingStatus = false;
                toaster.pop('error', "error", "Something went wrong Please try again Later..!!");
            });
        }
    }




    $scope.cleardigitalsignature = function () {
        var canvas;
        canvas = document.getElementById('myCanvas');
        context = canvas.getContext('2d');
        context.clearRect(0, 0, canvas.width, canvas.height);
    }

    $scope.signaturefunction = function () {
        type = event.currentTarget.value;
        var canvas;
        canvas = document.getElementById('myCanvas');
        context = canvas.getContext('2d');
        if (type == 'upload') {
            $('#selectfile').trigger('click', function () { });
            $rootScope.showreusesignaturediv = false;
            $rootScope.showsignaturediv = false;
            $scope.showuploaddiv = true;
        } else if (type == 'digitalsignature') {
            $rootScope.showreusesignaturediv = false;
            $scope.showuploaddiv = false;
            $scope.errormessageforsignature = false;
            $rootScope.showsignaturediv = true;
        }
        else if (type == 'reusedigitalsignature') {
            $rootScope.showsignaturediv = false;
            $scope.showuploaddiv = false;
            $rootScope.showreusesignaturediv = true;
        }
        else { }
        if (type == 'upload' || type == 'reusedigitalsignature') {
            context.clearRect(0, 0, canvas.width, canvas.height);
        }
    }

    $scope.ConfirmAppointmentApproval = function () {
        $('#StatusAction').modal('hide');
        $('body').removeClass('modal-open');
        $('.modal-backdrop').remove();
    };

}]);