

var checkListApp = angular.module('checkListApp', []);

checkListApp.controller('checkListController', function ($scope) {



});


$(document).ready(function () {
    $('#createMSCC').hide();
    $('#createCC').hide();
});

function showMSCCchecklist() {
    $('#detailsMSCC').hide();
    $('#createMSCC').show();
};

function showCCchecklist() {
    $('#detailsCC').hide();
    $('#createCC').show();
};

function hideMSCCchecklist() {
    $('#detailsMSCC').show();
    $('#createMSCC').hide();
};

function hideCCchecklist() {
    $('#detailsCC').show();
    $('#createCC').hide();
};

