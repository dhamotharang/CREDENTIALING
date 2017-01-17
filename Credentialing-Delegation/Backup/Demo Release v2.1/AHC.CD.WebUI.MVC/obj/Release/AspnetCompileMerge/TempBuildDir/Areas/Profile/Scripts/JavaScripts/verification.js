
//--------------------- Angular Module ----------------------
var masterPlans = angular.module("verificationApp", []);

masterPlans.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {


}])

//=========================== Controller declaration ==========================
masterPlans.controller('verificationController', ['$scope', '$http', '$filter', '$rootScope', 'messageAlertEngine', function ($scope, $http, $filter, $rootScope, messageAlertEngine) {

    $scope.verificationData = [{
        Name: "Florida license verification link",
        MailTo: 'https://ww2.doh.state.fl.us/irm00Praes/PRASLIST.ASP?ACTION=RETURN',
    }, {
        Name: "Amercian Board of Family Medicine cert verification link",
        MailTo: 'https://www.theabfm.org/diplomate/verify.aspx',
    }, {
        Name: "American Board of Internal Medicine cert verification link",
        MailTo: 'http://www.abim.org/services/verify-a-physician.aspx',
    }, {
        Name: "CAQH Universal Provider Data source link",
        MailTo: 'https://upd.caqh.org/oas/',
    }, {
        Name: "DEA verification link",
        MailTo: 'https://www.deadiversion.usdoj.gov/webforms/validateLogin.jsp',
    }, {
        Name: "DEA Duplicate Certificate link",
        MailTo: 'https://www.deadiversion.usdoj.gov/webforms/dupeCertLogin.jsp',
    }, {
        Name: "HCA Hospital Privilege verification link",
        MailTo: 'https://hcacredentialing.app.medcity.net/iResponse/ApplicationSpecific/login.asp',
    }, {
        Name: "Baycare Hospital Privilege verification link",
        MailTo: 'https://bccactus.baycare.org/iResponse/ApplicationSpecific/login.asp',
    }, {
        Name: "Medicare Opt Out List link",
        MailTo: 'http://medicare.fcso.com/Opt_out/150610.pdf',
    },{
        Name: "NCCPA  certification verification link",
        MailTo: 'https://www.nccpa.net/CredentialPublic/CredentialPublic.aspx',
    },{
        Name: "NPI Registry Search link",
        MailTo: 'https://npiregistry.cms.hhs.gov/NPPESRegistry/NPIRegistryHome.do',
    },{
        Name: "OIG search link",
        MailTo: 'http://exclusions.oig.hhs.gov/',
    },{
        Name: "NPDB report link",
        MailTo: 'https://www.npdb.hrsa.gov/',
    },{
        Name: "PECOS search link",
        MailTo: 'http://www.ecorpnet.com/PecosSearch.aspx',
    },{
        Name: "AOA certification verification link",
        MailTo: 'http://www.osteopathic.org/osteopathic-health/Pages/find-a-do-search.aspx',
    }];
}]);
