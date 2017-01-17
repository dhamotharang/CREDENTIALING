$(function () {
    setTimeout(function () {
        $('#Realestatelabel').text("INTAKE");
    }, 500);
    
   
    $(".tabs-menu a").click(function (event) {
        event.preventDefault();
        $(this).parent().addClass("current");
        $(this).parent().siblings().removeClass("current");
        var tab = $(this).attr("href");
        $(".createAuthTabs > .tab-content").not(tab).css("display", "none");
        $(tab).fadeIn();
    });



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

});