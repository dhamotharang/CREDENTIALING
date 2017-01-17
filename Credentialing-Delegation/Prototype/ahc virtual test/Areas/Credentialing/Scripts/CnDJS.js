$(document).ready(function () {
    console.log("collapsing menu");
    $("#sidemenu").addClass("menu-in");
    $("#page-wrapper").addClass("menuup");
});

var toggleDiv = function (divId) {
    $('#' + divId).slideToggle();
};