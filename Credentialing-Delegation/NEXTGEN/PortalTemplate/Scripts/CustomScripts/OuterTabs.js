var tabs = $('.tabs > li');

tabs.on("click", function () {
    tabs.removeClass('active');
    $(this).addClass('active');
});

$(function () {
    //chromeTabs.addNewTab($chromeTabsExampleShell, {
    //    title: "AUTH HISTORY",
    //    data: {
    //        'id': "authHistory"
    //    }
    //});
    //innerTabs.push("authHistory");
    openContainer(this, 'createauth', 'CREATE NEW AUTH');
})