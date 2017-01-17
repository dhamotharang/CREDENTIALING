/**
    @desctription This overrides the Name of the top wizard.
*/
$('.p_headername').html('Auditing');


/**
    @desctription By default it makes the Open Coding List as Enable
*/$('#Coder').show();


/**
    @type {array}
*/
var tabArray = [''];


/** 
@description The Event triggered  by clicking on the tabs, which internally calls the controller method to fetch the data. This event also makes the tabs active and inactive.
*/
$('.AuditingListContainter').off('click', '.tabs-menu a').on('click', '.tabs-menu a', function (event) {
    //$(".tabs-menu a").click(function (event) {
    event.preventDefault();
    $(this).parent().addClass("current");
    $(this).parent().siblings().removeClass("current");
    var tab = $(this).attr("href");
    $('.AuditTabContentContainer').find(".custommembertab-content").hide()
    $('.AuditTabContentContainer').find(tab).show().html();
    var clickedId = $(this).attr('href');
    var targetUrl = $(this).attr('data-target-url');
    var isPresent = false;
    for (var i = 0; i < tabArray.length; i++) {
        if (tabArray[i] == clickedId) {
            isPresent = true;
            break;
        }
    }
    if (!isPresent) {
        GetAuditingList(clickedId, targetUrl);
    }
});

/** 
@description By default the method calls the controller to get the data for Open Auditing List
* @param {string} #Coder appends the fetched data to the container, {string} /Auditing/Auditing/GetCoderList controller and method name which returns the data list.
*/
GetAuditingList("#Coder", "/Auditing/Auditing/GetCoderList");


/** 
@description By default the method calls the controller to get the data for Open Auditing List
* @param {string} targetId appends the fetched data to the container, {string} targetUrl controller and method name which returns the data list.
*/
function GetAuditingList(targetId, targetUrl) {
    $.ajax({
        type: 'GET',
        url: targetUrl,
        success: function (data) {
            $(targetId).html(data);

        }
    });
}