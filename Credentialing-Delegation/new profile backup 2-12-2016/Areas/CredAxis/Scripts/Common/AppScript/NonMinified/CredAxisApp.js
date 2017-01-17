$(document).ready(function () {
        $('[data-toggle="popover"]').popover();
        $('[data-toggle="tooltip"]').tooltip();
        $("[name='my-checkbox']").bootstrapSwitch();
  
    $('.tabs-menu li a').on('click', function () {
        $('.tabs-menu li a').removeClass('active-nav');
        $(this).addClass('active-nav');
        var divID = $(this).attr('href');
        $('html,body').animate({
            scrollTop: $(divID).offset().top - 100
        },
    'slow');
    });

    $('.tabs-menu li a').on('hover', function () {
        $('.navbar-menu li a').removeClass('active-nav');
        $(this).addClass('active-nav');
    })

    //$('#mainBody').addClass('NewMainBodyTabStyle');
    //$('#menuBar').addClass('NewSideMenuTabStyle');

    $(document).off('DOMMouseScroll onwheel mousewheel onmousewheel ontouchmove scroll', '#innerTabContainer').on('DOMMouseScroll onwheel mousewheel onmousewheel ontouchmove', '#innerTabContainer', function (event) {
        //if ($(window).scrollTop() + 300 >= $(this).offset().top) {
        //    var id = $(this).attr('id');
        //    $('tabs-menu li a').removeClass('active-nav');
        //    $('tabs-menu li a[href=#' + id + ']').addClass('active-nav');
        //    $('#Notes').click();
        //}
        if (($('#innerTabContainer').scrollTop() == 117) || (($('#innerTabContainer').scrollTop() > 46) && ($('#innerTabContainer').scrollTop() < 47))) {
            $li = $('.providerProfileTabsContainer > .tabs-menu > .tab-item.current');
            $li.next().click();
        }
    });


});

