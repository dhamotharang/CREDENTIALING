var openChildControlDiv = function (childId) {
    $(".contolDiv").slideUp();
    $("#" + childId).slideDown();
}

var openChildDiv = function (childId) {
    $("#" + childId).slideDown();
}
var closeChildDiv = function (childId) {
    $("#" + childId).slideUp();
}



/*This function is for opening achild and toggleing the bootstrap icon classes*/

var toggleChildDiv = function (childId, thisObj) {

    $("#" + childId).slideToggle();;

    var $this = $(thisObj).children(":first")

    if ($this.hasClass("fa fa-edit")) {
        $this.removeClass("fa fa-edit").addClass("fa fa-caret-up");
        return;
    }
    if ($this.hasClass("fa fa-caret-up")) {
        $this.removeClass("fa fa-caret-up").addClass("fa fa-edit");
        return;
    }


}

/*This function is for collapse all and expand all feature in individual page.*/

var collapseAll = function (element) {
    $(element).parent().parent().parent().find($("div[role='tabpanel']")).removeClass("in");
    $(element).parent().parent().parent().find($(".glyphicon-minus")).removeClass("glyphicon-minus").addClass("glyphicon-plus");
};

var expandAll = function (element) {
    $(element).parent().parent().parent().find($("div[role='tabpanel']")).addClass("in").removeAttr("style");
    $(element).parent().parent().parent().find($(".glyphicon-plus")).removeClass("glyphicon-plus").addClass("glyphicon-minus");
};

$(document).ready(function ($) {
    var $timeline_block = $('.cd-timeline-block');

    //hide timeline blocks which are outside the viewport
    $timeline_block.each(function () {
        if ($(this).offset().top > $(window).scrollTop() + $(window).height() * 0.75) {
            $(this).find('.cd-timeline-img, .cd-timeline-content').addClass('is-hidden');
        }
    });

    //on scolling, show/animate timeline blocks when enter the viewport
    $(window).on('scroll', function () {
        $timeline_block.each(function () {
            if ($(this).offset().top <= $(window).scrollTop() + $(window).height() * 0.75 && $(this).find('.cd-timeline-img').hasClass('is-hidden')) {
                $(this).find('.cd-timeline-img, .cd-timeline-content').removeClass('is-hidden').addClass('bounce-in');
            }
        });
    });
});