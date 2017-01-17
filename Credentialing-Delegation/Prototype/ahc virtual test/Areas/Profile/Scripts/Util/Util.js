/* Checks if the form controls are valid or not.
   Returns true if valid or returns false */
function isValidFormData(formID) {
    return $("#" + formID).valid();
};

//$(function () {
//    $("#sidemenu").addClass("menu-in");
//    $("#page-wrapper").addClass("menuup");
//});

$(document).ready(function () {
    //console.log("collapsing menu");

    var calender = function () {
        $(".datepicker").datepicker({
            changeMonth: true,
            changeYear: true
        });
    };
    //Remove the autocomplete feature of the browser
    $('input').prop('autocomplete', 'off');

    $('select').keypress(function (event) { if (event.keyCode == 8) { return false; } });
    $('select').keydown(function (event) { if (event.keyCode == 8) { return false; } });
});



$(function () {
    $('[name=duallistbox_demo1]').bootstrapDualListbox();

    $('[role=tab]').on('click', function (e) {
        var tabname = $(e.target).text();
        $("#tabname").text(tabname);
    });
});



// Javascript to enable link to tab
var url = document.location.toString();
if (url.match('#/')) {
    var tabhighlight = url.split('#/')[1];
    $('li[role=presentation]').removeClass("active");
    $('a[href=#' + tabhighlight + ']').parent().addClass("active");
    $('div[role=tabpanel]').removeClass("active");
    $('#' + tabhighlight).addClass("active");
    var tabname = $('a[href=#' + tabhighlight + ']').text();
    $("#tabname").text(tabname);
}