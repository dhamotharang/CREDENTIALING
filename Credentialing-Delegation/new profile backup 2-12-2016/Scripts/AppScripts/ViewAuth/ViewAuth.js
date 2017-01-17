/* Initializing Jquery */
if ($('#QueueName').text() == 'BJOY') {
    $('#QueueName').hide();
} else {
    $('#QueueName').show();
}
   // $('#QueueName').show().text('Nurse Review');

$(document).ready(function () {

    setTimeout(function () {
        $('#Realestatelabel').text("INTAKE");
    }, 500);

    //Function for Generating the Table Body
    function GenerateTableBody(TableBodyID, data) {
        $("#" + TableBodyID).html("");
        for (var i = 0; i < data.length; i++) {
            td = "";
            td = td + '<tr>';
            for (var prop in data[i]) {
                td = td + '<td>' + data[i][prop] + '</td>';
            }
            td = td + '</tr>';
            $("#" + TableBodyID).append(td);
        }
        $('input.flat').iCheck({
            checkboxClass: 'icheckbox_square-green',
            radioClass: 'iradio_square-green'
        });
    };

    /* View Auth Tabs Functionality */
    //$(".tabs-menu a").click(function (event) {
    //    event.preventDefault();
    //    $(this).parent().addClass("current");
    //    $(this).parent().siblings().removeClass("current");
    //    var tab = $(this).data("tabTarget");
    //    //$(this).parents('.viewAuthTabsContainer').siblings().children(".customtab-content").not(tab).css("display", "none");
    //    //$(this).parents('.viewAuthTabsContainer').siblings().children(tab).css("display", "block");
    //    // $(".customtab-content").not(tab).css("display", "none");
    //    $(this).parents('.viewAuthTabsContainer').siblings().children(tab).fadeIn();
    //    return false;
    //});

    function appendTabContent(data, tabcontentid) {
        $('#' + tabcontentid).html(data);
        $('input.flat').iCheck({
            checkboxClass: 'icheckbox_square-green',
            radioClass: 'iradio_square-green'
        });
    }

    function GetTabData(tabid, tabcontentid) {

        //event.preventDefault();
        //$.ajax({
        //    type: "GET",
        //    url: "/ViewAuth/GetTabData",
        //    data: { TabId: tabid },
        //    //contentType: "application/json",
        //    //async: false,
        //    dataType: "html",
        //    success: function (data, textStatus, XMLHttpRequest) {
        //        appendTabContent(data, tabcontentid);
        //        return false;
        //    }
        //});
    }
    $("#ViewAuthorizations").on('click', '.list-item', function (e) {
        e.preventDefault();
        var MethodName = '';
        var id = $(this).parent().attr('id');
        $(this).parent().addClass("current");
        $(this).parent().siblings().removeClass("current");
        var url = $(this).data("partial");
        TabManager.getDynamicContent(url, "ViewAuthorizationTabView", MethodName, "");
    });
    $('#ViewAuthorizations .list-item').first().click();

    /*Generating Notes-Table Data OnClick of NotesTab*/
    $('.list-item').on('click', function (event) {
        event.preventDefault();
        var targetid = $(event.target).attr('id');
        var count = ($(event.target).parent().index() + 1).toString();
        var tabcontentid = 'tab-1';
        switch (targetid) {
            case 'ReviewTab':
                GetTabData(targetid, tabcontentid);
                break;
            case 'NotesTab':
                GetTabData(targetid, tabcontentid);
                break;
            case 'ContactsTab':
                GetTabData(targetid, tabcontentid);
                break;
            case 'AttachmentsTab':
                GetTabData(targetid, tabcontentid);
                break;
            case 'LettersTab':
                GetTabData(targetid, tabcontentid);
                break;
            case 'ODAGTab':
                GetTabData(targetid, tabcontentid);
                break;
            case 'StatusTab':
                GetTabData(targetid, tabcontentid);
                break;
            default:
                GetTabData("SummaryTab", "tab-1");
                break;
        }

    })

    $('[data-toggle="tooltip"]').tooltip();

    var memberData = TabManager.getMemberData();
    setMemberHeaderData(memberData);
    $('.member-actions').data("tabUserid", $('#MemberID').text());

    $('.posexplanation').tooltip({ title: postitleSetter, trigger: "hover", html: true, placement: "top", container: 'body' });

    $('.authtrackingstep').tooltip({ title: steptitleSetter, trigger: "hover", placement: "top" });


    //====================================================//
    //var inithtml = '<span class="arrow-left">' + '<i class="fa fa-chevron-left fa-2x">' + '</i>' + '</span>' +
    //              '<span class="arrow-right">' + '<i class="fa fa-chevron-right fa-2x">' + '</i>' + '</span>' +
    //              '<div class="row offer-pg-cont" style="margin-top:10px;text-align:center">' +
    //                   '<span>No Documents Attached</span>' +
    //               '</div>';
    //var initCounthtml = "<span>0</span>";
    //$('.Docs_Div').html(inithtml);
    //$('.DocsCount_Div').html(initCounthtml);
    //=====================================================//
    //$('[data-toggle="tooltip"]').tooltip();


    /* ODAG Questionnaire Data Object */
    var ODAGQuestions = {
        "Questions": [
              { Code: null, Description: 'Has PCP Approved this request?', IsSelectedYes: false, OptionDate: '', Options: [{ OptionID: 1, Value: 'Y' }, { OptionID: 2, Value: 'N' }, { OptionID: 3, Value: 'I am PCP' }], QuestionID: 1, QuestionType: null },
              { Code: null, Description: 'Beneficiary Request Expedited', IsSelectedYes: false, OptionDate: '', Options: [{ OptionID: 1, Value: 'Y' }, { OptionID: 2, Value: 'N' }, { OptionID: 3, Value: 'NA' }], QuestionID: 2, QuestionType: null },
              { Code: null, Description: 'Provider Request Expedited', IsSelectedYes: false, OptionDate: '', Options: [{ OptionID: 1, Value: 'Y' }, { OptionID: 2, Value: 'N' }, { OptionID: 3, Value: 'NA' }], QuestionID: 3, QuestionType: null },
              { Code: null, Description: 'Process Expedited', IsSelectedYes: false, OptionDate: '', Options: [{ OptionID: 1, Value: 'Y' }, { OptionID: 2, Value: 'N' }, { OptionID: 3, Value: 'NA' }], QuestionID: 4, QuestionType: null },
              { Code: null, Description: 'Did Plan Extend Time Frame?', IsSelectedYes: false, OptionDate: '', Options: [{ OptionID: 1, Value: 'Y' }, { OptionID: 2, Value: 'N' }, { OptionID: 3, Value: 'NA' }], QuestionID: 4, QuestionType: null },
              { Code: null, Description: 'Was time frame extension taken?', IsSelectedYes: false, OptionDate: '', Options: [{ OptionID: 1, Value: 'Y' }, { OptionID: 2, Value: 'N' }, { OptionID: 3, Value: 'NA' }], QuestionID: 5, QuestionType: null },
              { Code: null, Description: 'Was member notified by Phone?', IsSelectedYes: false, OptionDate: '', Options: [{ OptionID: 1, Value: 'Y' }, { OptionID: 2, Value: 'N' }, { OptionID: 3, Value: 'NA' }], QuestionID: 6, QuestionType: null },
              { Code: null, Description: 'Was member notified by letter?', IsSelectedYes: false, OptionDate: '', Options: [{ OptionID: 1, Value: 'Y' }, { OptionID: 2, Value: 'N' }, { OptionID: 3, Value: 'NA' }], QuestionID: 7, QuestionType: null },
              { Code: null, Description: 'Case Disposition', OptionDate: '', IsSelectedYes: false, Options: [{ OptionID: 1, Value: 'Approved' }, { OptionID: 2, Value: 'Denied' }], QuestionID: 8, QuestionType: null },
              { Code: null, Description: 'Date and Time Disposition', IsSelectedYes: false, OptionDate: '', Options: [], QuestionID: 9, QuestionType: 'OnlyDateTime' },
              { Code: null, Description: 'Was request denied for LOMN?', IsSelectedYes: false, OptionDate: '', Options: [{ OptionID: 1, Value: 'Y' }, { OptionID: 2, Value: 'N' }, { OptionID: 3, Value: 'NA' }], QuestionID: 10, QuestionType: null },
              { Code: null, Description: 'Was Case received by Medical Doctor', IsSelectedYes: false, OptionDate: '', Options: [{ OptionID: 1, Value: 'Y' }, { OptionID: 2, Value: 'N' }, { OptionID: 3, Value: 'NA' }], QuestionID: 10, QuestionType: 'ObjectiveWithDateTime' },
              { Code: null, Description: 'Was reconsideration reviewed by Medical Director', IsSelectedYes: false, OptionDate: '', Options: [{ OptionID: 1, Value: 'Y' }, { OptionID: 2, Value: 'N' }, { OptionID: 3, Value: 'NA' }], QuestionID: 11, QuestionType: 'ObjectiveWithDateTime' },
              { Code: null, Description: 'Date and Time Effectuation', IsSelectedYes: false, OptionDate: '', Options: [], QuestionID: 12, QuestionType: 'OnlyDateTime' },
              { Code: null, Description: 'Member Notified Orally', IsSelectedYes: false, OptionDate: '', Options: [{ OptionID: 1, Value: 'Y' }, { OptionID: 2, Value: 'N' }, { OptionID: 3, Value: 'NA' }], QuestionID: 13, QuestionType: null },
              { Code: null, Description: 'Member Written Notification', IsSelectedYes: false, OptionDate: '', Options: [{ OptionID: 1, Value: 'Y' }, { OptionID: 2, Value: 'N' }, { OptionID: 3, Value: 'NA' }], QuestionID: 14, QuestionType: null },
              { Code: null, Description: 'AOR Receipt Date', IsSelectedYes: false, OptionDate: '', Options: [], QuestionID: 15, QuestionType: 'OnlyDateTime' },
              { Code: null, Description: 'Name of Plan', IsSelectedYes: false, OptionDate: '', Options: [], QuestionID: 15, QuestionType: 'Descriptive' }

        ]
    }
    var str = "";
    $.each(ODAGQuestions.Questions, function (index, value) {
        str = str + ' <b> ' + value.Description + '</b><br /> ' +
           + ' <div class="iradio_flat-green" style="position: relative;"><input type="radio" name="ICD" class="form-control input-xs flat" id="ver9" style="position: absolute; opacity: 0;"><ins class="iCheck-helper" style="position: absolute; top: 0%; left: 0%; display: block; width: 100%; height: 100%; margin: 0px; padding: 0px; border: 0px; opacity: 0; background: rgb(255, 255, 255);"></ins></div> ' +
           + '  <label for="ver9" class="">Y</label> ' +
           +  '   <div class="iradio_flat-green checked" style="position: relative;"><input type="radio" name="ICD" class="form-control input-xs flat" id="ver9" style="position: absolute; opacity: 0;"><ins class="iCheck-helper" style="position: absolute; top: 0%; left: 0%; display: block; width: 100%; height: 100%; margin: 0px; padding: 0px; border: 0px; opacity: 0; background: rgb(255, 255, 255);"></ins></div> ' +
        + '<label for="ver9" class="">N</label> ' +
        + ' <div class="iradio_flat-green" style="position: relative;"><input type="radio" name="ICD" class="form-control input-xs flat" id="ver9" style="position: absolute; opacity: 0;"><ins class="iCheck-helper" style="position: absolute; top: 0%; left: 0%; display: block; width: 100%; height: 100%; margin: 0px; padding: 0px; border: 0px; opacity: 0; background: rgb(255, 255, 255);"></ins></div>' +
      + '  <label for="ver9" class="">I AM THE PCP</label> ' +
       +'  <span><i class="fa fa-times"></i></span>'
    });

    $(function () {
        var authId = $('#AuthId').val();
        var screenHeight = $(window).height() - 138;
        $("#framecontainer").css("height", screenHeight + "px");
        $("#goneWithTheClick").click(function () {
            $('#inPageDocIframe').attr('src', "");
        });
    });
    $('#Refnum').html('7745319880');

    if ($('#activeBreadcrumb').text().search("History") > -1) {
        $('#UMRunningFooter').hide();
        $('#QueueName').show().text('History');
    }
    else {
        $('#UMRunningFooter').show();
    }
    /* Function for Opening Documents Container */
    var openDoc = function () {
        var authId = $('#AuthId').val();
        var params = [
        'height=' + screen.height,
        'width=' + screen.width
        ].join(',');
        // and any other options from
        if ($("#newurl").val() != "") {
            var popup = window.open(rootDir + '/Document/Index?authID=' + authId + $("#newurl").val(), 'popup_window', params);
        }
        else {
            var popup = window.open(rootDir + '/Document/Index?authID=' + authId, 'popup_window', params);
        }
        popup.moveTo(0, 0);
        $("#overlaydiv").hide();
        $("#framecontainer").hide();
    };

    fixBottomWidth();

    //var menuItems = $('.main-navigation li');

    //menuItems.on("click", function (event) {
    //    event.preventDefault();
    //    menuItems.removeClass("active");

    //    $(this).addClass("active");

    //    //$(".ViewAuthTabmain-content").css({
    //    //    "background": $(this).data("bg-color")
    //    //});


    //});

});


/* Fixing Width of Bottom Fixed Document Container */
function fixBottomWidth() {
    var percen = (($("span:contains('Utilization Management')").width() / $(".main_container").width()) * 100) + 1;
    var val = percen * $(".main_container").width() * 0.013;
    $('.bottomfixed').width(($(".main_container").width() - val - 63) + 21);
}

setRunningFooterWidth();

function steptitleSetter(idve) {
    var label = "0 Days 00:00:00 Hrs";
    return label;
}

function postitleSetter(idve) {
    var text = '<div>' + '<span class="pos_desc_title">POS DESC</span>' + '<br>' +
               '<div class="pos_desc">' +
               '<p>' + 'Location, other than a hospital, skilled nursing facility (SNF), military treatment facility, community health center, State or local public health clinic, or intermediate care facility (ICF), where the health professional routinely provides health examinations, diagnosis, and treatment of illness or injury on an ambulatory basis.'  + '</p>' +
               '</div>'+
               '</div>';
    return text;
}



var createAuthorizations = function () {
    $.ajax({
        type: 'post',
        url: '/Home/Authorization',
        data: $('#UM_auth_form').serialize(),
        cache: false,
        error: function () {
            //alert(" There is an error...");
        },
        success: function (data, textStatus, XMLHttpRequest) {
            $('#auth_preview_modal').html(data);
            showModal('authPreviewModal');
            //$('.thePOS').html(data.data.PlaceOfService);
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
        success: function (data, textStatus, XMLHttpRequest) {
            $('#auth_preview_modal').html(data);
            showModal('authPendModalCreate');
            //$('.thePOS').html(data.data.PlaceOfService);
        }
    });
};
$("#UM_auth_form").off('change', '.LevelRate').on('change', '.LevelRate', function () {
    if ($(".LevelRate").val() === "NEG FEE") {
        //TabManager.openSideModal('/Areas/UM/Views/Common/Modal/_NegFeeModal.cshtml', 'NEG FEE', 'both', '', '', '');
        TabManager.openCenterModal("/Areas/UM/Views/Common/Modal/_NegFeeModal.cshtml", "EDIT NEG FEE");
    }
});
