//Initializing J query
$(function () {
    setTimeout(function () {
        $('#Realestatelabel').text("INTAKE");
    }, 500);

    $('.flat').on("ifClicked", function () {
        if ($(this).is(':checked')) {
            $(this).iCheck('uncheck');
        }
    });
    var p = TabManager.getMemberData();
    console.log("Member", p);
    //Tabs Functionality
    $(".tabs-menu a").click(function (event) {
        event.preventDefault();
        $(this).parent().addClass("current");
        $(this).parent().siblings().removeClass("current");
        var tab = $(this).attr("href");
        $(this).parents('.memberProfileTabsContainer').siblings().children(".custommembertab-content").not(tab).css("display", "none");
        $(this).parents('.memberProfileTabsContainer').siblings().children(tab).css("display", "block");
        // $(".customtab-content").not(tab).css("display", "none");
        $(this).parents('.memberProfileTabsContainer').siblings().children(tab).fadeIn();

        setTimeout(function () {
            $('input.flat').iCheck({
                checkboxClass: 'icheckbox_square-green',
                radioClass: 'iradio_square-green'
            });
        },100);
    });


    //Script for Bottom Fixed Documents Section
    $(function () {
        var authId = $('#AuthId').val();
        var screenHeight = $(window).height() - 138;
        $("#framecontainer").css("height", screenHeight + "px");
        $("#goneWithTheClick").click(function () {
            $('#inPageDocIframe').attr('src', "");
        });
    });

    //SCROLLS TABS LEFT:
    $('.left-scroll-button-tab').click(function () {
        var currentHorizontalPosition = $('div.memberProfileTabsContainer').scrollLeft();
        $('div.memberProfileTabsContainer').animate({ 'scrollLeft': currentHorizontalPosition - 300 }, 250, 'swing');
    });

    //SCROLLS TABS RIGHT:
    $('.right-scroll-button-tab').click(function () {
        var currentHorizontalPosition = $('div.memberProfileTabsContainer').scrollLeft();
        $('div.memberProfileTabsContainer').animate({ 'scrollLeft': currentHorizontalPosition + 300 }, 250, 'swing');
    });

    //Documents open
    $('.DocumentsBtn').click(function () {
         $('.DocumentContainer').toggleClass('ChangeWidthOfDocumentConainer');

    })

    //------------------//



    // Calling ticktock() every 1 second
    $(".bottomfixed").width(1576.31);
    $(window).resize(function () {
        var width = $(window).width() - $('.menu_section').width();
        $(".bottomfixed").width(width);
    });
})


