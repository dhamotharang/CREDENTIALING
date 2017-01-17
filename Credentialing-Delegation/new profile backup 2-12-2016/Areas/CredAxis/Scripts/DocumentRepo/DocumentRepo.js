//================== Main Script ===========================
$(document).ready(function () {
    //setTimeout(function () {
    //    $(".ui-layout-resizer.ui-layout-resizer-west.ui-layout-resizer-open.ui-layout-resizer-west-open").attr('id', 'theCursor');
    //    $(".ui-layout-west.ui-layout-pane.ui-layout-pane-west").attr('id', 'leftBlock');
    //    $(".ui-layout-center.ui-layout-pane.ui-layout-pane-center").attr('id', 'rightBlock');
    //    $("#rightBlock").css({ "left": "60%", "width": "50%" });
    //    $("#leftBlock").css({ "width": "60%" });
    //    $("#theCursor").css({ "left": "60%" });
    //}, 400);

    //$('#container').layout();

    $('.btn-group').off('click', '.btn').on('click', '.btn', function(){
        $('.btn-group .btn').removeClass('active');
        $(this).addClass('active');
    });
});

var sideBarOpen = true;
var openModal = function () {
    $('#DocModal').modal('show');
}
var perview = function (docPath) {
    $("#preView").toggleClass("showPreview");
    $('iframe').attr('src', docPath);

};

$('body').off('click', '#AddNewModal').on('click', '#AddNewModal', function () {
    // TabManager.openFloatingModal('~/Views/DocumentRepo/AddNewDocument/_DocumentBody.cshtml', '~/Views/DocumentRepo/AddNewDocument/_DocumentHeader.cshtml', '~/Views/DocumentRepo/AddNewDocument/_DocumentFooter.cshtml', ' ', ' ',' ');
    // pmfFileupload.init();
    ShowModal('~/Areas/CredAxis/Views/DocumentRepository/View/ModalforAddEdit/_DocumentBody.cshtml', 'Add New Document', '', '');
    var s = $(this).attr("class").split(" ");
    var name = s[2];
    setTimeout(function () {
        $.ajax({
            url: "/CredAxis/Documents/GetAddNewDocument",
            type: 'POST',
            data: { DocumentName: name },
            success: function (data) {
                $("#AddContent").append(data);
                $("#StateButtons").hide();
            }
        })
    }, 1000);
});
//$("#div1").resizable();
//$('#div1').resize(function () {
//    $('#div2').width($("#parent").width() - $("#div1").width());
//});
//$(window).resize(function () {
//    $('#div2').width($("#parent").width() - $("#div1").width());
//    $('#div1').height($("#parent").height());
//});


$('[data-toggle="popover"]').popover();

$('body').off("click", ".collapse-link").on("click", ".collapse-link", function (event) {
    $(this).find('i').toggleClass("fa-chevron-down")
    $(this).closest('.x_panel').find('.x_content').slideToggle("slow", function () {
    });
});

$('.theDocRepoHeader').off('click', '.thePreviewMailPanel').on('click', '.thePreviewMailPanel', OpenPreviewPanel)
    .off('click', '.thePreviewMailPanelClose').on('click', '.thePreviewMailPanelClose', ClosePreviewPanel);

function OpenPreviewPanel(path) {
    $('.ui-layout-west').removeClass('col-lg-12').addClass('col-lg-6');
    $('.ui-layout-right').removeClass('hidden');
    $('.thePreviewMailPanel').addClass('hidden');
    $('.thePreviewMailPanelClose').removeClass('hidden');
}


function ClosePreviewPanel() {
    $('.ui-layout-west').removeClass('col-lg-6').addClass('col-lg-12');
    $('.ui-layout-right').addClass('hidden');
    $('.thePreviewMailPanel').removeClass('hidden');
    $('.thePreviewMailPanelClose').addClass('hidden');
}

function toggleSidebar() {
    if (sideBarOpen) {
        $('#div2').hide(1000);
        $('#div1').css('width', '100%');
        sideBarOpen = false;
    }
    else {
        $('#div2').show(1000);
        $('#div1').css('width', '50%');
        sideBarOpen = true;
    }
}
