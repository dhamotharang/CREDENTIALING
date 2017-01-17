/* ATTACHMENTS MANAGEMNENT*/
$('.document_modal').off('change', '.custom-file-input').on('change', '.custom-file-input', function () {
        var fileName = $(this).val();
        if (fileName == "") {
            return;
        }
        else {
            var aFile = fileName.split('fakepath')[1];
            var bFile = aFile.substring(aFile.indexOf(aFile) + 1);
            var file_text = bFile.toUpperCase();
            if ($(window).width() > 1800) {
                $(this).parents('.doc_section').find('.noFile').text(file_text.substr(0, 60));
                $(this).parents('.doc_section').find('.custom-file-input').prop('disabled', true);
            }
            else {
                $(this).parents('.doc_section').find('.noFile').text(file_text.substr(0, 40));
                $(this).parents('.doc_section').find('.custom-file-input').prop('disabled', true);
            }
        }
    });

$(".SaveBtn").addClass("saveDoc");
$(".saveDoc").on("click", function () {
        $("#emptyContactTemplate").html(null);
    var p = document.getElementById("DocumentFile");
    var doc_Template = '<tr>' +
    '<td>' + dateFormat(new Date()) + '</td>' +
    '<td>' + $("#DocumentName").val() + '</td>' +
    '<td>' + $("#DocumenType").val() + '</td>' +
    '<td>BARBARA JOY</td>' +
    '<td><button class="preview copy-button" data-filepath="' + URL.createObjectURL(p.files[0]) + "#" + p.files[0].name + '"><i class="fa fa-file-archive-o"></i></button></td>' +
    '</tr>';
    $("#AttachmentTBody").append(doc_Template);
    $(".SaveBtn").removeClass("saveDoc");
    $('.close_modal_btn').click();
});

//----Attachment table add btn-----
$('#attachmentDiv').off('click', '.add_attachment_btn').on('click', '.add_attachment_btn', function () {
    TabManager.openFloatingModal('~/Areas/UM/Views/Common/Attachments/Common/_AttachmentsForm.cshtml', '~/Areas/UM/Views/ViewAuth/Attachments/_Attachment_Header.cshtml', '~/Areas/UM/Views/ViewAuth/Attachments/_Attachment_Footer.cshtml', '', '');
    return;
});


$('#AttachmentTBody').off('click', '.preview').on('click', '.preview', function () {
    if ($(this).data("filepath")) {
        var filepath = $(this).data("filepath");
        window.open(filepath, '', 'width=' + screen.width + ',height=' + screen.height);
    }
});

var dateFormat = function (d) {
    var dd = d.getDate();
    var mm = d.getMonth() + 1; //January is 0!
    if (dd < 10) {
        dd = '0' + dd
    }
    if (mm < 10) {
        mm = '0' + mm
    }
    return dd + '/' + mm + '/' + d.getFullYear() + " " + d.getHours() + ":" + d.getMinutes() + ":" + d.getSeconds();
};