$(document).ready(function () {
    /*Open Notifications*/
    $('.Notify-Expand').click(function () {
        $('.NotifyArea').css("width", "270px");
        $('.Notify-Collapse').show(100);
        $('.Notify-Expand').fadeOut(100);
        $('body').css('overflow-y', 'hidden');

    });
    $('.Notify-Collapse').click(function () {
        collapseCabin();
    });
    /* Close Notifications*/
    function collapseCabin() {
        $('.NotifyArea').css("width", "0%");
        $('.Notify-Collapse').hide();
        $('.Notify-Expand').css("display", "block");
        $('.Documents-List').show();
        $('.Document-Preview').hide();
        $('body').css('overflow-y', 'scroll');
        $('.bd-flow').hide();
    }
    $(window).bind('keyup', function (event) {
        // esc event
        if (event.keyCode === 27) {
            if ($('.NotifyArea').css('display') === 'block') {
                collapseCabin();
            }
        }

    });


    $('.NotifyArea').click(function () {
        event.stopPropagation();// for stoping default event to occur
    })
    $('.Notify-Expand').click(function () {
        event.stopPropagation(); // for stoping default event to occur
    })
    $(document).click(function (e) {
        if ($('.NotifyArea').width()) {
            $('.NotifyArea').css("width", "0%");
            $('.Notify-Collapse').hide();
            $('.Notify-Expand').css("display", "block");
            $('.Documents-List').show();
            $('.Document-Preview').hide();
            $('body').css('overflow-y', 'scroll');
        }
    })
});