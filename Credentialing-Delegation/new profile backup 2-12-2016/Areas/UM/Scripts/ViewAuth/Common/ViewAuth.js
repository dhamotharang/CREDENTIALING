/* Initializing Jquery */
if ($('#QueueName').text() === 'BJOY') {
    $('#QueueName').hide();
} else {
    $('#QueueName').show();
}
$(document).ready(function () {
    setTimeout(function () {
        $('#Realestatelabel').text("INTAKE");
    }, 500);
    function appendTabContent(data, tabcontentid) {
        $('#' + tabcontentid).html(data);
        $('input.flat').iCheck({
            checkboxClass: 'icheckbox_square-green',
            radioClass: 'iradio_square-green'
        });
    }

    $("#ViewAuthorizations").on('click', '.list-item', function () {
        var id = $(this).parent().attr('id');
        $(this).parent().addClass("current");
        $(this).parent().siblings().removeClass("current");
        $.ajax({
            type: "GET",
            url: "/ViewAuthorization/GetTabData",
            data: { TabId: id+"Tab" ,AuthID:"20"},
            dataType: "html",
            success: function (data) {
                appendTabContent(data, "ViewAuthorizationTabView");
                return false;
            }
        });
    });
    $('#ViewAuthorizations .list-item').first().click();

    $('[data-toggle="tooltip"]').tooltip();

    var memberData = TabManager.getMemberData();
    setMemberHeaderData(memberData);
    $('.member-actions').data("tabUserid", $('#MemberID').text());

    $('.posexplanation').tooltip({ title: postitleSetter, trigger: "hover", html: true, placement: "top", container: 'body' });

    $('.authtrackingstep').tooltip({ title: steptitleSetter, trigger: "hover", placement: "top" });

    $(function () {
        var screenHeight = $(window).height() - 138;
        $("#framecontainer").css("height", screenHeight + "px");
        $("#goneWithTheClick").click(function () {
            $('#inPageDocIframe').attr('src', "");
        });
    });
    $('#Refnum').html('7745319880');
    if ($('#activeBreadcrumb').text().search("History") > -1) {
        $('#UMRunningFooter').hide();
        $('.view_auth_FooterBtn').hide();
        $('#QueueName').show().text('History');
    }
    else {
        $('.view_auth_FooterBtn').show();
        $('#UMRunningFooter').show();
    }
    fixBottomWidth();
});

/* Fixing Width of Bottom Fixed Document Container */
function fixBottomWidth() {
    var percen = (($("span:contains('Utilization Management')").width() / $(".main_container").width()) * 100) + 1;
    var val = percen * $(".main_container").width() * 0.013;
    $('.bottomfixed').width(($(".main_container").width() - val - 63) + 21);
}

setRunningFooterWidth();

function steptitleSetter() {
    var label = "0 Days 00:00:00 Hrs";
    return label;
}

function postitleSetter() {
    var text = '<div>' + '<span class="pos_desc_title">POS DESC</span>' + '<br>' +
               '<div class="pos_desc">' +
               '<p>' + 'Location, other than a hospital, skilled nursing facility (SNF), military treatment facility, community health center, State or local public health clinic, or intermediate care facility (ICF), where the health professional routinely provides health examinations, diagnosis, and treatment of illness or injury on an ambulatory basis.' + '</p>' +
               '</div>' +
               '</div>';
    return text;
}



var createAuthorizations = function () {
    $.ajax({
        type: 'post',
        url: '/Authorization/Authorization',
        data: $('#UM_auth_form').serialize(),
        cache: false,
        error: function () {
        },
        success: function (data) {
            $('#auth_preview_modal').html(data);
            showModal('authPreviewModal');
        }
    });
};

var pendAuthorizations = function () {
    $.ajax({
        type: 'post',
        url: '/Home/PendAuthorization',
        data: $('#UM_auth_form').serialize(),
        cache: false,
        error: function () {
        },
        success: function (data) {
            $('#auth_preview_modal').html(data);
            showModal('authPendModalCreate');
        }
    });
};
$("#UM_auth_form").off('change', '.LevelRate').on('change', '.LevelRate', function () {
    if ($(".LevelRate").val() === "NEG FEE") {
        TabManager.openCenterModal("/Areas/UM/Views/Common/Modal/_NegFeeModal.cshtml", "EDIT NEG FEE");
    }
});
