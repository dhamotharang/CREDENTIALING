var initCredApp = angular.module('InitCredApp', ['ngTable', 'colorpicker.module','mgcrea.ngStrap', "wysiwyg.module"]);

$(document).ready(function () {
    //$("#sidemenu").addClass("menu-in");
    //$("#page-wrapper").addClass("menuup");
    $('#specialities').select2();
    $('#groups').select2();
    $('#plans').select2();
    $('#plansnew').select2();
    $('#types').select2();
});


