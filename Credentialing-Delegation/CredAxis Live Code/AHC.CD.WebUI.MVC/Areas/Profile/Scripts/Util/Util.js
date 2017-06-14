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

    var calender = function () {
        $(".datepicker").datepicker({
            changeMonth: true,
            changeYear: true
        });
    };
    //Remove the autocomplete feature of the browser
    $('input').prop('autocomplete', 'off');
});



$(function () {
    $('[name=duallistbox_demo1]').bootstrapDualListbox();

    $('[role=tab]').on('click', function (e) {
        var tabname = $(e.target).text();
        $("#tabname").text(tabname);
    });
});



// Javascript to enable link to tab
$(document).ready(function () {
    var url = document.location.toString();
    if (url.match('#')) {
        var tabhighlight = url.split('#/')[1];
        $('a[href=#' + tabhighlight + ']').trigger("click");
        setTimeout(function () {
            var sectionhighlight = url.split('#/')[2];
            if (sectionhighlight) {
                $('html, body').animate({
                    scrollTop: $('#' + sectionhighlight).offset().top
                }, 1000);
            }
        }, 1000);
    }
})