$("#Attachment").ready(function () {
    setTimeout(function () {
        $('input.flat').iCheck({
            checkboxClass: 'icheckbox_square-green',
            radioClass: 'iradio_square-green'
        });
    }, 500)

});

//------------open_Doc_Modal----------------
$('#Attachment').off('click', '.open_Doc_Modal').on('click', '.open_Doc_Modal', function () {
    TabManager.openSideModal('~/Areas/UM/Views/Common/Attachments/Common/_AttachmentsForm.cshtml', 'Add New Attachment', 'both', '', '', '');
    return;
});
if ($("#AttachmentTBody").children().length==0) {
    $("#AttachmentTBody").html('<tr id="emptyContactTemplate"><td colspan="12" class="theme_label_data" style="text-align:center !important">No Data Available</td></tr>');
}

function PreviewDoc(key,name) {
    window.open('/MemberManage/PreviewDocument/' + '?DocKey=' + key +'&&FileName='+name , '', 'width=' + screen.width + ',height=' + screen.height);
    // -- preview was not working from ajax call , hence using the above code ----
//$.ajax({
//        type: 'GET',
//        url: '/MemberManage/PreviewDocument?DocKey=' + key,
//        processData: false,
//        contentType: 'application/octet-stream',
//        cache: false,
//        error: function () {
//        },
//        success: function (data) {
//            // alert('success');
//            window.open(data);
//            //window.open(data, '', 'width=' + screen.width + ',height=' + screen.height);
         
//        }
//    });
}