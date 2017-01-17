// -------------------- Dashbooard controller Angular Module-------------------------------
//---------- Author KRGLV -------------

//------------------------- angular module ----------------------------
var dashboardApp = angular.module('dashboardApp', ['ui.bootstrap']);

// -------------------- Directive for generate custome table according to selected Licenses ----------------------

//--------------------- Angular Directive for Country Code ------------------------
dashboardApp.directive('licensedetailstable', ['$compile', function ($compile) {
    return {
        restrict: 'AE',
        link: function (scope, element, attr) {
            var contentTr = "";

            scope.$watch("LicenseType", function (newValue) {
                
                if (newValue == "State License") {
                    contentTr = angular.element(StateLicenseHTMLTemplate);
                } else if (newValue == "Federal DEA") {
                    contentTr = angular.element(FederalDEAHTMLTemplate);
                } else if (scope.LicenseType == "CDS Information") {
                    contentTr = angular.element(CDSInformationHTMLTemplate);
                } else if (scope.LicenseType == "Specialty/Board") {
                    contentTr = angular.element(SpecialityBoardHTMLTemplate);
                } else if (scope.LicenseType == "Hospital Privileges") {
                    contentTr = angular.element(HospitalPrivilagesHTMLTemplate);
                } else if (scope.LicenseType == "Professional Liability") {
                    contentTr = angular.element(ProfessionalLiabilityHTMLTemplate);
                } else if (scope.LicenseType == "Worker Compensation") {
                    contentTr = angular.element(WorkerCompensationHTMLTemplate);
                } else {
                    contentTr = angular.element(StateLicenseHTMLTemplate);
                }

                element.html(contentTr);
                $compile(contentTr)(scope);
            });
        }
    };
}]);

// ------------------ dashboard root scope ------------------
dashboardApp.run(['$rootScope', function ($rootScope) {
    $rootScope.Role = "PRO";
    //----------------- temp method for role base toggle --------------------
    $rootScope.ToggleRoles = function () {
        if ($rootScope.Role == "PRO") {
            $rootScope.Role = "CCO";
        } else {
            $rootScope.Role = "PRO";
        }
    };
}]);

// --------------------- dashboard Angular template ------------------

var StateLicenseHTMLTemplate = "<table class='table table-striped small'>" +
    "<thead><tr><th>License Number</th><th>State</th><th>Expiry Date</th><th>Day Left</th><th>Action</th></tr></thead>" +
    "<tbody><tr ng-repeat='license in CurrentPageLicenses' ng-class='{danger:license.dayLeft < -1, warning:license.dayLeft < 90, info:license.dayLeft < 180}'><td>{{license.LicenseNumber}}</td><td>{{license.IssueState}}</td><td>{{license.ExpiryDate | date:'MM/dd/yyyy'}}</td><td>{{license.dayLeft}}</td>" +
    "<td><button class='btn btn-xs btn-default' data-toggle='tooltip' data-placement='right' title='Renew'><i class='fa fa-repeat'></i></button></td></tr>" +
    "</tbody></table>";

var FederalDEAHTMLTemplate = "<table class='table table-striped small'>" +
    "<thead><tr><th>DEA Number</th><th>State of Register</th><th>Expiry Date</th><th>Day Left</th><th>Action</th></tr></thead>" +
    "<tbody><tr ng-repeat='license in CurrentPageLicenses' ng-class='{danger:license.dayLeft < -1, warning:license.dayLeft < 90, info:license.dayLeft < 180}'><td>{{license.DEANumber}}</td><td>{{license.StateOfReg}}</td><td>{{license.ExpiryDate | date:'MM/dd/yyyy'}}</td><td>{{license.dayLeft}}</td>" +
    "<td><button class='btn btn-xs btn-default' data-toggle='tooltip' data-placement='right' title='Renew'><i class='fa fa-repeat'></i></button></td></tr>" +
    "</tbody></table>";

var CDSInformationHTMLTemplate = "<table class='table table-striped small'>" +
    "<thead><tr><th>CDS Number</th><th>State</th><th>Expiry Date</th><th>Day Left</th><th>Action</th></tr></thead>" +
    "<tbody><tr ng-repeat='license in CurrentPageLicenses' ng-class='{danger:license.dayLeft < -1, warning:license.dayLeft < 90, info:license.dayLeft < 180}'><td>{{license.CertNumber}}</td><td>{{license.State}}</td><td>{{license.ExpiryDate | date:'MM/dd/yyyy'}}</td><td>{{license.dayLeft}}</td>" +
    "<td><button class='btn btn-xs btn-default' data-toggle='tooltip' data-placement='right' title='Renew'><i class='fa fa-repeat'></i></button></td></tr>" +
    "</tbody></table>";

var SpecialityBoardHTMLTemplate = "<table class='table table-striped small'>" +
    "<thead><tr><th>License Number</th><th>State</th><th>Expiry Date</th><th>Day Left</th><th>Action</th></tr></thead>" +
    "<tbody><tr ng-repeat='license in CurrentPageLicenses' ng-class='{danger:license.dayLeft < -1, warning:license.dayLeft < 90, info:license.dayLeft < 180}'><td>{{license.DEANumber}}</td><td>{{license.StateOfReg}}</td><td>{{license.ExpiryDate | date:'MM/dd/yyyy'}}</td><td>{{license.dayLeft}}</td>" +
    "<td><button class='btn btn-xs btn-default' data-toggle='tooltip' data-placement='right' title='Renew'><i class='fa fa-repeat'></i></button></td></tr>" +
    "</tbody></table>";

var HospitalPrivilagesHTMLTemplate = "<table class='table table-striped small'>" +
    "<thead><tr><th>License Number</th><th>Expiry Date</th><th>Day Left</th><th>Action</th></tr></thead>" +
    "<tbody><tr ng-repeat='license in CurrentPageLicenses' ng-class='{danger:license.dayLeft < -1, warning:license.dayLeft < 90, info:license.dayLeft < 180}'><td>{{license.DEANumber}}</td><td>{{license.AffilicationEndDate | date:'MM/dd/yyyy'}}</td><td>{{license.dayLeft}}</td>" +
    "<td><button class='btn btn-xs btn-default' data-toggle='tooltip' data-placement='right' title='Renew'><i class='fa fa-repeat'></i></button></td></tr>" +
    "</tbody></table>";

var ProfessionalLiabilityHTMLTemplate = "<table class='table table-striped small'>" +
    "<thead><tr><th>License Number</th><th>State</th><th>Expiry Date</th><th>Day Left</th><th>Action</th></tr></thead>" +
    "<tbody><tr ng-repeat='license in CurrentPageLicenses' ng-class='{danger:license.dayLeft < -1, warning:license.dayLeft < 90, info:license.dayLeft < 180}'><td>{{license.DEANumber}}</td><td>{{license.StateOfReg}}</td><td>{{license.ExpiryDate | date:'MM/dd/yyyy'}}</td><td>{{license.dayLeft}}</td>" +
    "<td><button class='btn btn-xs btn-default' data-toggle='tooltip' data-placement='right' title='Renew'><i class='fa fa-repeat'></i></button></td></tr>" +
    "</tbody></table>";

var WorkerCompensationHTMLTemplate = "<table class='table table-striped small'>" +
    "<thead><tr><th>License Number</th><th>Expiry Date</th><th>Day Left</th><th>Action</th></tr></thead>" +
    "<tbody><tr ng-repeat='license in CurrentPageLicenses' ng-class='{danger:license.dayLeft < -1, warning:license.dayLeft < 90, info:license.dayLeft < 180}'><td>{{license.WorkersCompensationNumber}}</td><td>{{license.ExpirationDate | date:'MM/dd/yyyy'}}</td><td>{{license.dayLeft}}</td>" +
    "<td><button class='btn btn-xs btn-default' data-toggle='tooltip' data-placement='right' title='Renew'><i class='fa fa-repeat'></i></button></td></tr>" +
    "</tbody></table>";
