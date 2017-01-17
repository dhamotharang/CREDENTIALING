$('#Cms1500FormTab').show();
$(".tabs-menu a").click(function (event) {
    event.preventDefault();
    $(this).parent().addClass("current");
    $(this).parent().siblings().removeClass("current");
    var tab = $(this).attr("href");
    $('.claims_tab_container').find(".custommembertab-content").hide()
    $('.claims_tab_container').find(tab).show();
});
