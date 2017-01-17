$('#List837s').show();
var tabArray = [''];
var previousTargetID = "";
/** @description tab navigation for Reports.
 */
$(".tabs-menu a").click(function (event) {
    event.preventDefault();
    $(this).parent().addClass("current");
    $(this).parent().siblings().removeClass("current");
    var tab = $(this).attr("href");
    $('.claims_tab_container').find(".custommembertab-content").hide()
    $('.claims_tab_container').find(tab).show().html();
       var clickedId = $(this).attr('href');
    var targetUrl = $(this).attr('data-target-url');
   
    $(previousTargetID).html('')
        GetEDIFileList(clickedId, targetUrl);

});
GetEDIFileList("#List837s", "/Billing/EOBReport/GetEobReport");
/** @description Getting EDI File List that has the specified Idand URL parameter.
 * @param {number} ID of the file and URL of the file. 
 */
function GetEDIFileList(targetId, targetUrl) {
    $.ajax({
        type: 'GET',
        url: targetUrl,
        success: function (data) {
            $(targetId).html(data);
            previousTargetID = targetId;
            TabManager.loadOrReloadScriptsUsingHtml(data);
        }
    });
}


