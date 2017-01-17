$(document).ready(function () {
    var providerData = TabManager.getProviderData();
    if (providerData) setProviderHeaderData(providerData);
});

var setProviderHeaderData = function (providerData) {
    if (providerData.NPINumber) $('#ProviderID').text(providerData.NPINumber);
    $('#ProviderCAHQ').text("124734810");
    $('#ProviderDOB').text("12/04/1980");
    $('#ProviderSpeciality').text("Audiologist");
    $('#ProviderLocation').text("PRIMECARE,1930 - PORT SAINT LOUIS, FLORIDA");
    $('#ProviderLanguage').text("English");
    if (providerData.Titles) if (providerData.Titles.length > 0) $('#ProviderType').text(providerData.Titles[0]);
};

$(document).ready(function () {
    //$('[data-toggle="popover"]').popover();
    $('[data-toggle="tooltip"]').tooltip();
    $("#IntiateCredentails").on('click', '.list-item', function (e) {
        e.preventDefault();
        var MethodName = '';
        var id = $(this).parent().attr('id');
        $(this).parent().addClass("current");
        $(this).parent().siblings().removeClass("current");
        var url = $(this).data("partial");
        TabManager.getDynamicContent(url, "ViewCredentials", MethodName, "");
    });
    $('#IntiateCredentails .list-item').first().click();
    $("#Queues").on('click', '.list-item', function (e) {
        e.preventDefault();
        var MethodName = '';
        var id = $(this).parent().attr('id');
        $(this).parent().addClass("current");
        $(this).parent().siblings().removeClass("current");
        var url = $(this).data("partial");
        TabManager.getDynamicContent(url, "ViewQueues", MethodName, "");
    });
    $('#Queues .list-item').first().click();



});