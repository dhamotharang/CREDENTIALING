//var umCtrl = angular.module("UMApp", []);
//console.log(homeApp);
//homeApp.controller('UMCtrl', ['$scope', '$filter', '$compile', function ($scope, $filter, $compile) {

//    $scope.UMObject = {
//        PlaceOfServiceID: [],
//        Facility: '',
//        primaryDX: 'world'
//    };

//    $scope.yo = "Hello";

//    alert("yo");
//    console.log($scope.UMObject);
//}]);

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

    $(".addcptrangebtn").on("click", function () {
        ShowModal('~/Views/Home/_GetAddCPTRangeModal.cshtml', 'Plain Language');
    })
   

    $("#cptcodeselect").customselect();
    $("#plRows").on('change', '.cptcodedropdown', function () {
        var codeArr = $(this).val().split('-');
        var code = codeArr[0];
        var description = codeArr[1];
        var selectid = $(this).attr('id');
        var index = parseInt(selectid.substring(13, 14));      
        if (!isNaN(index) && (function (x) { return (x | 0) === x; })(parseFloat(index))) {
            $(this).parent().parent().parent().children().find('#CPTDesc' + index).val(description);
            $(this).parent().parent().parent().children().find('#RequestedUnits' + index).val(1);
            //if (!$(this).parent().parent().parent().children().find('#IncludeLetter' + index).is(':checked')) {
            //    $('#IncludeLetter' + index).iCheck('check');
            //}
        } else {
            $(this).parent().parent().parent().children().find('#CPTDesc').val(description);
            $(this).parent().parent().parent().children().find('#RequestedUnits').val(1);
            if (!$(this).parent().parent().parent().children().find('#IncludeLetter').is(':checked')) {
                $("#IncludeLetter").iCheck('check');
            }
        }
        $(this).val(code);
    });
    //$("#demo").customselect({
    //    "csclass":"custom-select",  // Class to match
    //    "search": true,            // Is searchable?
    //    "numitems":     4,    // Number of results per page
    //    "searchblank":  false,// Search blank value options?
    //    "showblank":    true, // Show blank value options?
    //    "searchvalue":  false,// Search option values?
    //    "hoveropen":    false,// Open the select on hover?
    //    "emptytext":    "",   // Change empty option text to a set value
    //    "showdisabled": false,// Show disabled options
    //})
    $(".plain_language_btn").on("click", function () {
        ShowModal('~/Views/Home/_GetPlainLanguageModal.cshtml', 'Plain Language');
    })
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

    $('.collapse-link').click(function () {
        var x_panel = $(this).closest('div.x_panel');
        var button = $(this).find('i');
        var content = x_panel.find('div.x_content');
        content.slideToggle(200);
        (x_panel.hasClass('fixed_height_390') ? x_panel.toggleClass('').toggleClass('fixed_height_390') : '');
        (x_panel.hasClass('fixed_height_320') ? x_panel.toggleClass('').toggleClass('fixed_height_320') : '');
        button.toggleClass('fa-chevron-up').toggleClass('fa-chevron-down');
        setTimeout(function () {
            x_panel.resize();
        }, 50);
    });

    $('.queue-redirect').on("click", function () {
        TabManager.closeCurrentlyActiveSubTab();
        TabManager.navigateToTab({ "tabAction": "Facility Queue", "tabTitle": "Facility Queue", "tabPath": "~/Views/Queue/_Queue.cshtml", "tabContainer": "fullBodyContainer" });
    })
    var redirectToQueue = function () {

    };


    //var createAuthorizations = function () {
    //    $.ajax({
    //        url: '/Home/Authorization',
    //        data: $('#UM_auth_form').serialize,
    //        cache: false,
    //        type: "POST",
    //        dataType: "html",
    //        success: function (data, textStatus, XMLHttpRequest) {
    //            showModal('authPreviewModal');
    //        }
    //    });
    //}


    //$('.auth_form').on("click", ".addNoteBtn", function () {
    //    ShowModal("~/Views/Home/_GetAuthNoteModalView.cshtml", 'Add New Note');
        
    //    SubjectNum = 1;
    //    TypeNum = 1;
    //});
    InitICheckFinal();

    GetNoteTemplate();

    var ShowNoteModal = function () {
        ShowModal("~/Views/Home/_GetAuthNoteModalView.cshtml", 'Add New Note');
    }

    var arrOfPL = ['plrow0'];
    // ADD ROW:
    $('#plRows').on('click', '.plusButton', function () {
        if ($(this).parent().parent().attr('id') == 'plrow0') {
            $(this).replaceWith(deleteButton());
        }
        else {
            $(this).remove();
        }
        var id = arrOfPL.length;
        arrOfPL.push('plrow' + id);
        console.log(arrOfPL);
        $('#plRows').append(AddPLRow('plrow' + id, id));
        $("#" + "cptcodeselect" + id).customselect();
        InitICheckFinal();
    });
    // REMOVE ROW:
    $('#plRows').on('click', '.minusButton', function () {
        var thisRow = $(this).parent().parent();
        var thisId = thisRow.attr('id');
        if (thisId == arrOfPL[arrOfPL.length - 1]) {
            var lastRow = $('#' + arrOfPL[arrOfPL.length - 2]);
            lastRow.find($('.row-action')).append(addButton());
        }
        if (thisId == 'plrow0') {
            $('#plrow0').empty().append(addButton());
        }
        thisRow.remove();
        arrOfPL.splice(arrOfPL.indexOf(thisId), 1);
    });
    // ADD ROW:
    function AddPLRow(plrowid, id) {
        var cptselectid = 'cptcodeselect' + id;
        var cptmodifierid = 'Modifier' + id;
        var cptcodedescid = 'CPTDesc' + id;
        var cptrequnits = 'RequestedUnits' + id;
        var cptIncletter = 'IncludeLetter' + id;
        return '<div class="x_content" id="' + plrowid + '">' +
        '<div class="col-lg-2"><select id="' + cptselectid + '" name="standard" class="form-control input-xs mandatory_field_halo custom-select cptcodedropdown"><option value="">None - Please Select</option><option value="0001F-Heart Failure Composite">0001F-Heart Failure Composite</option>  <option value="0001M-Infectious Disc HCV6 Assays">0001M-Infectious Disc HCV6 Assays</option> <option value="0002M-Liver Dis 10 Assays Wash">0002M-Liver Dis 10 Assays Wash</option>  <option value="00215-Anesth Skull Repair/Fract">00215-Anesth Skull Repair/Fract</option></select></div>' +
        '<div class="col-lg-1"><input class="form-control input-xs mandatory_field_halo" id="' + cptmodifierid + '" name="' + cptmodifierid + '" placeholder="MODIFIER" type="text"></div>' +
        '<div class="col-lg-3"><input class="form-control input-xs mandatory_field_halo" id="' + cptcodedescid + '" name="' + cptcodedescid + '" placeholder="DESCRIPTION" type="text"></div>' +
        '<div class="col-lg-1"><input class="form-control input-xs non_mandatory_field_halo"  id="' + cptrequnits + '" name="' + cptrequnits + '"  data-val="true" data-val-number="The field RequestedUnits must be a number." placeholder="REQ UNITS" type="text"></div>' +
        '<div class="col-lg-1 text-center button-styles-xs row-action">&nbsp;' + deleteButton() + addButton() + '</div>' +
        '<div class="col-lg-1 text-center pull_to_left"><input type="checkbox" class="flat" id="' + cptIncletter + '" name="' + cptIncletter + '"/></div>' +
        '</div>'
    }
    // MINUS BUTTON:
    function deleteButton() {
        return '<button class="red-button minusButton"><i class="fa fa-minus "></i></button>'
    }
    // PLUS BUTTON:
    function addButton() {
        return '<button class="light-green-button plusButton"><i class="fa fa-plus "></i></button>'
    }
    ContactData = [];
    $("#contactTbody").on('click', '.editauthcontact', function (event) {
        $tr = $(this).closest('tr');
        $firstChild = $tr.children().first();

        while ($firstChild.next()) {
            if ($firstChild.attr("name")) {
                var name = $firstChild.attr("name");
                generateContactRow($firstChild.attr("name"), $firstChild.text());
                $firstChild = $firstChild.next();
            }
            else {
                break;
            }
        }


        function generateContactRow(key, value) {
            ContactData.push({ "key": key, "value": value });
        }
        console.log(ContactData);
    });
