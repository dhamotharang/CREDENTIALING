/* Checks if the form controls are valid or not.
   Returns true if valid or returns false */
function isValidFormData(formID) {
    return $("#" + formID).valid();
};

//This function is for controlling the panel of Quick Access.
//var openChildControlDiv = function (childId) {
//    $(".contolDiv").slideUp();
//    $("#" + childId).slideDown();
//}

//var openChildDiv = function (childId) {
//    $("#" + childId).slideDown();
//}
//var closeChildDiv = function (childId) {
//    $("#" + childId).slideUp();
//}
////End : This function is for controlling the panel of Quick Access.


///*This function is for opening a child and toggleing the bootstrap icon classes*/

//var toggleChildDiv = function (childId, thisObj) {

//    $("#" + childId).slideToggle();;

//    var $this = $(thisObj).children(":first")

//    if ($this.hasClass("fa fa-edit")) {
//        $this.removeClass("fa fa-edit").addClass("fa fa-caret-up");
//        return;
//    }
//    if ($this.hasClass("fa fa-caret-up")) {
//        $this.removeClass("fa fa-caret-up").addClass("fa fa-edit");
//        return;
//    }


//}

///*This function is for collapse all and expand all feature in individual page.*/

//var collapseAll = function (element) {
//    $(element).parent().parent().parent().find($("div[role='tabpanel']")).removeClass("in");
//    $(element).parent().parent().parent().find($(".glyphicon-minus")).removeClass("glyphicon-minus").addClass("glyphicon-plus");
//};

//var expandAll = function (element) {
//    $(element).parent().parent().parent().find($("div[role='tabpanel']")).addClass("in").removeAttr("style");
//    $(element).parent().parent().parent().find($(".glyphicon-plus")).removeClass("glyphicon-plus").addClass("glyphicon-minus");
//};

//$(document).ready(function ($) {
    
//    var $timeline_block = $('.cd-timeline-block');

//    //hide timeline blocks which are outside the viewport
//    $timeline_block.each(function () {
//        if ($(this).offset().top > $(window).scrollTop() + $(window).height() * 0.75) {
//            $(this).find('.cd-timeline-img, .cd-timeline-content').addClass('is-hidden');
//        }
//    });

//    //on scolling, show/animate timeline blocks when enter the viewport
//    $(window).on('scroll', function () {
//        $timeline_block.each(function () {
//            if ($(this).offset().top <= $(window).scrollTop() + $(window).height() * 0.75 && $(this).find('.cd-timeline-img').hasClass('is-hidden')) {
//                $(this).find('.cd-timeline-img, .cd-timeline-content').removeClass('is-hidden').addClass('bounce-in');
//            }
//        });
//    });
//});

$(document).ready(function () {
    console.log("collapsing menu");
    $("#sidemenu").addClass("menu-in");
    $("#page-wrapper").addClass("menuup");
    $(".datepicker").datepicker({
        changeMonth: true,
        changeYear: true
    });
});

//Remove the autocomplete feature of the browser
$('input').prop('autocomplete', 'off');

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