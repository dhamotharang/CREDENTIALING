
//--------------------- Angular Module ----------------------
var masterPlans = angular.module("verificationApp", []);

masterPlans.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {


}])

//=========================== Controller declaration ==========================
masterPlans.controller('verificationController', ['$scope', '$http', '$filter', '$rootScope', 'messageAlertEngine', function ($scope, $http, $filter, $rootScope, messageAlertEngine) {

    $scope.verificationData = [{
        Name: "Florida Medical License verification link",
        MailTo: 'https://appsmqa.doh.state.fl.us/IRM00PRAES/PRASLIST.ASP',
    }, {
        Name: "Amercian Board of Family Medicine cert verification link",
        MailTo: 'https://www.theabfm.org/diplomate/verify.aspx',
    }, {
        Name: "American Board of Internal Medicine cert verification link",
        MailTo: 'http://www.abim.org/services/verify-a-physician.aspx',
    }, {
        Name: "CAQH Universal Provider Data source link",
        MailTo: 'https://proview.caqh.org/Login/Index?ReturnUrl=%2f',
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
        MailTo: 'https://bccactus.baycare.org/iresponse/ApplicationSpecific/login.asp',
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
        MailTo: 'https://pecos.cms.hhs.gov/pecos/login.do#headingLv1',
    },{
        Name: "AOA certification verification link",
        MailTo: 'http://www.osteopathic.org/osteopathic-health/Pages/find-a-do-search.aspx',
    },{
        Name: "Medical License Verification",
        MailTo: 'https://appsmqa.doh.state.fl.us/IRM00PRAES/PRASLIST.ASP',
    },{
        Name: "SAM.gov",
        MailTo: 'https://www.sam.gov/portal/SAM/?portal:componentId=16bb71aa-8903-43f5-99de-b6b2cd6234d6&interactionstate=JBPNS_rO0ABXc0ABBfanNmQnJpZGdlVmlld0lkAAAAAQATL2pzZi9mdW5jdGlvbmFsLmpzcAAHX19FT0ZfXw**&portal:type=action#1',
    }, {
        Name: "American Medical Assn",
        MailTo: 'https://profiles.ama-assn.org/amaprofiles/',
    }];
}]);
