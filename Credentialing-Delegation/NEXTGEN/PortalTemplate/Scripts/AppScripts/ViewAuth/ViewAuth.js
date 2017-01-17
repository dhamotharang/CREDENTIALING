//Initializing J query
$(function () {
    setTimeout(function () {
        $('#Realestatelabel').text("INTAKE");
    }, 500);
   
    $('.flat').on("ifClicked", function () {        
        if ($(this).is(':checked')) {
            $(this).iCheck('uncheck');
        }
    });

    //Tabs Functionality
    $(".tabs-menu a").click(function (event) {
        event.preventDefault();
        $(this).parent().addClass("current");
        $(this).parent().siblings().removeClass("current");
        var tab = $(this).attr("href");
        $(this).parents('.viewAuthTabsContainer').siblings().children(".customtab-content").not(tab).css("display", "none");
        $(this).parents('.viewAuthTabsContainer').siblings().children(tab).css("display", "block");
       // $(".customtab-content").not(tab).css("display", "none");
        $(this).parents('.viewAuthTabsContainer').siblings().children(tab).fadeIn();
    });

    //Status Tab Activity Toggle Functionality
    var flag = false;
    $(".activityprint_button").hide();
    $("#flip").click(function () {
        if (flag == false) {
            $("#panel").slideDown('1000');
            $(".activityprint_button").show().slideDown('1000');
            var icon = $("#flip").find('i');
            if (icon.hasClass("fa fa-chevron-down")) {
                icon.removeClass("fa fa-chevron-down").addClass("fa fa-chevron-up")
            }
            flag = true;
        }
        else {
            $("#panel").slideUp('1000');
            $(".activityprint_button").hide().slideUp('1000');
            var icon = $("#flip").find('i');
            if (icon.hasClass("fa fa-chevron-up")) {
                icon.removeClass("fa fa-chevron-up").addClass("fa fa-chevron-down")
            }
            flag = false;
        }
    })

    //Auth Review Tab-Files Upload
    uploadedcount = 0;
    uploadedfiles = [];
    isPrevious = "";
    $('input[type=file]').change(function (e) {
        var doctypeObj = { type: "", files: [] };
        fileinputid = $(this).attr('id');
        doctypeObj.type = fileinputid;
        var docLabelId = $(this).parent().parent().children(".col-lg-9").children('.wrapWord').attr('id');
        if (fileinputid != isPrevious){
            uploadedcount = parseInt($("#" + docLabelId).html().split(' ')[0]);
            if (!isNaN(uploadedcount) && (function (x) { return (x | 0) === x; })(parseFloat(uploadedcount))) {
                uploadedcount = uploadedcount;
            } else {
                uploadedcount = 0;
            }
        }
        else
            uploadedcount = uploadedcount;

        var labelid = $(this).parent().children("label").attr('id');
        var label = "";
        if (uploadedcount === 0){
            $("#" + docLabelId).html(e.target.files.length + ' ' + 'Files Uploaded');
        }
        else
            $("#" + docLabelId).html(e.target.files.length + uploadedcount + ' ' + 'Files Uploaded');
            $.each(e.target.files, function (index, value) {
            e.target.files[index].Category = fileinputid;
            uploadedfiles.push(e.target.files[index]);
        })
        uploadedcount = e.target.files.length + uploadedcount;
        isPrevious = fileinputid;
    });

    //Hover On Files Count
    $('.filescount').tooltip({ title: titleSetter, trigger: "hover", html: true, placement: "top" });
    function titleSetter() {
        var id = $(this).parent().parent().children().find("input").attr('id');
        var label = "";
        $.each(uploadedfiles, function (index, value) {
            if (uploadedfiles[index].Category == id) {
                label = label + '<span><i class="fa fa-file-pdf-o"></i></span><label class="text-uppercase wrapWord filenamelabel">' + uploadedfiles[index].name + ' </label><br/>';
            }
            })
        return label;
        }


    $(".attachmentsgroup .btn").click(function () {
        if ($(this).attr("id") == "list") {
            $(".listview").removeClass("isInactive").addClass("isactive").show().slideDown('1000');
            $(".tableview").removeClass("isactive").hide().slideUp('1000');
        }
        else if ($(this).attr("id") == "Table") {
            $(".listview").removeClass("isactive").addClass("isInactive").hide().slideUp('1000');
            $(".tableview").addClass("isactive").show().slideDown('1000');
        }
    })


    //ODAG Questionnaire Data Object
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


    //$('.FrameContainer').on("click" , function(){
    //    setTimeout(function () {
    //        $("#overlaydiv").slideToggle();
    //        $("#inPageDocIframe").attr('src', 'about:blank');
    //        $('#framecontainer').slideToggle(function () {
    //            var authId = $('#AuthId').val();
    //            setTimeout(function () {
    //                $('#inPageDocIframe').attr('src', rootDir + '/Document/Index?authID=' + authId + $("#newurl").val());
    //            }, 200);
    //        });
    //    }, 200);
    //}) 
    //Script for Bottom Fixed Documents Section
    $(function () {
        var authId = $('#AuthId').val();
        var screenHeight = $(window).height() - 138;
        $("#framecontainer").css("height", screenHeight + "px");
        $("#goneWithTheClick").click(function () {
            $('#inPageDocIframe').attr('src', "");
        });
    });

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

    //Documents open
    $('.DocumentsBtn').click(function () {
        //if ($('.DocumentContainer').is(':visible')) {
        //    $('.DocumentContainer').hide('slide', { direction: 'right' }, 500);
        //} else {
        //    $('.DocumentContainer').show('slide', { direction: 'right' }, 500);
        //}
        $('.DocumentContainer').toggleClass('ChangeWidthOfDocumentConainer');
        
    })

    //------------------//
    var clock = document.querySelector('.timerClass');

    // But there is a little problem
    // we need to pad 0-9 with an extra
    // 0 on the left for hours, seconds, minutes

    var pad = function (x) {
        return x < 10 ? '0' + x : x;
    };

    var ticktock = function () {
        var d = new Date();

        var h = pad(d.getHours());
        var m = pad(d.getMinutes());
        var s = pad(d.getSeconds());

        var current_time = [h, m, s].join(':');

        clock.innerHTML = current_time;

    };

    ticktock();

    // Calling ticktock() every 1 second
    setInterval(ticktock, 1000);

 
})
