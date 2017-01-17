$('#List837s').show();
$('.p_headername').html('Billing');

var tabArray = ['#List837s'];
/** @description tab Navigation.
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
    var isPresent = false;
    for (var i = 0; i < tabArray.length; i++) {
        if (tabArray[i] == clickedId) {
            isPresent = true;
            break;
        }
    }
    if (!isPresent) {
        GetEDIFileList(clickedId, targetUrl);

    }




});
//GetEDIFileList("#List837s", "/Billing/FileManagement/Get837FileList");

/** @description Getting EDI File List that has the specified Idand URL parameter.
 * @param {number} ID of the file and URL of the file.
 
 */
function GetEDIFileList(targetId, targetUrl) {
    TabManager.showLoadingSymbol(targetId.substring(1, targetId.lenght));
    $.ajax({
        type: 'GET',
        url: targetUrl,
        success: function (data) {
            $(targetId).html(data);
            tabArray.push(targetId);
            TabManager.hideLoadingSymbol();
        }
    });
}
/** @description Navigating to File upload screen.
 */
$('#file_upload_btn').on('click', function () {
    TabManager.openCenterModal("~/Areas/Billing/Views/FileManagement/_FileUploadForm.cshtml", "Upload File", '', '');
    TabManager.setCenterModalHeight();
});
/** @description Reset the form.
 */
$("#ClearForm").click(function (event) {
    event.preventDefault();

});

/** @description Uploads the file.
 */
$("#Upload_ediFile_btn").click(function (event) {
    event.preventDefault();

});

/** @description for calling the download method
*/
//$('body').off('click', '.BillingFMDownload').on('click', '.BillingFMDownload', function () {
   
//    var targetPath = $(this).attr("data-downloadurl");
//    var link = document.createElement("a");
//  //  link.download = "vsqxg.txt";
//    link.href = targetPath;
//    link.click();
//})