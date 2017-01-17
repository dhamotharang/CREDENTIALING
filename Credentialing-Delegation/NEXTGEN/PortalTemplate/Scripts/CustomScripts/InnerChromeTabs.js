var $chromeTabsExampleShell = $('.chrome-tabs-shell')
if(typeof chromeTabs != 'undefined')chromeTabs.init({
    $shell: $chromeTabsExampleShell,
    minWidth: 0,
    maxWidth: 100,
});
$chromeTabsExampleShell.bind('chromeTabRender', function () {
    var $currentTab = $chromeTabsExampleShell.find('.chrome-tab-current');
    if ($currentTab.length && window['console'] && console.log) {
      
        //console.log('Current tab index', $currentTab.index(), 'title', $.trim($currentTab.text()), 'data', $currentTab.data('tabData').data);
        var newtabreq = $currentTab.data('tabData').data.id;
        $(".contenttab").removeAttr("style");
        $(".contenttab").removeClass("active in");

        $("#" + newtabreq).addClass("active in");        
    }
    
    $("a[data-tab-val='" + newtabreq + "']").parents("ul.memberinnermenu").children("li.anchorselected").removeClass("anchorselected").children("a.active").removeClass("active")
    $("a[data-tab-val='" + newtabreq + "']").addClass("active").parent().addClass("anchorselected");
    $("#Realestatelabel").empty().text($("a[data-tab-val='" + newtabreq + "']").attr('data-tab-realestate'));
});

//$("div.tabcontrol").click(function () {
//    var newtabid = $(this).attr("id");
//    $("div[role='tabpanel']").removeClass("active in");
//    $("#" + newtabid).addClass("active in");

//})

//$('button').click(function () {
//    if ($chromeTabsExampleShell.hasClass('chrome-tabs-dark-theme')) {
//        $chromeTabsExampleShell.removeClass('chrome-tabs-dark-theme');
//    } else {
//        $chromeTabsExampleShell.addClass('chrome-tabs-dark-theme');
//    }
//});

//for changing page title once page is loaded
document.title = "CREATE NEW AUTH| AHC";