$(document).ready(function () {
    $('#fishArea').hide();
    var delICDBtn = '<button class="btn btn-danger btn-xs delete_icd_btn" type="button" tabindex="-1"><i class="fa fa-minus"></i></button>';

    var addICDBtn = '<button class="btn btn-success btn-xs plusButton add_icd_btn loser_field" type="button"><i class="fa fa-plus"></i></button>';

    var delAddICDBtn = '<button class="btn btn-danger btn-xs delete_icd_btn minusButton" type="button"><i class="fa fa-minus"></i></button>' +
           '<button class="btn btn-success btn-xs plusButton add_icd_btn loser_field" type="button"><i class="fa fa-plus"></i></button>';

    var delCPTBtn = '<button class="btn btn-danger btn-xs minusButton" type="button"><i class="fa fa-minus"></i></button>';

    var addCPTBtn = '<button class="btn btn-success btn-xs plusButton loser_field" type="button"><i class="fa fa-plus"></i></button>';

    var delAddCPTBtn = '  <button class="btn btn-danger btn-xs minusButton" type="button"><i class="fa fa-minus"></i></button>' +
                        '<button class="btn btn-success btn-xs plusButton loser_field" type="button"><i class="fa fa-plus"></i></button>';

    var updateICDMapping = function () {
        var $ICDCodes = $('.icdCode');
        var $ICDDescriptions = $('.icdDescription');
        var $ICDDelBtns = $('.delete_icd_btn');
        if ($ICDCodes.length > 0) {
            for (var p = 0; p < $ICDCodes.length; p++) {
                $ICDCodes[p].name = 'ICDCodes[' + p + '].ICDCode';
                $ICDCodes[p].id = 'ICDCodes_' + p + '__ICDCode';
                $ICDDescriptions[p].name = 'ICDCodes[' + p + '].ICDCodeDescription';
                $ICDDescriptions[p].id = 'ICDCodes_' + p + '__ICDCodeDescription';
                if ($ICDCodes.length == 1) {
                    $('.icdButtonArea').eq(p).html(addICDBtn);
                }
                else {
                    if (p > -1 && p <= ($ICDCodes.length - 2)) {
                        $('.icdButtonArea').eq(p).html(delICDBtn);
                    }
                    else {
                        $('.icdButtonArea').eq(p).html(delAddICDBtn);
                    }
                }
            }
        }
    };

    updateCPTMapping = function () {
        var $CPTCodes = $('.cptCodes');
        var $CPTDescriptions = $('.cptDescriptions');
        var $CPTModifiers = $('.cptModifier');
        var $CPTDisciplines = $('.cptDiscipline');
        var $CPTReqUnits = $('.cptReqUnits');
        var $CPTRange1 = $('.cptRange1');
        var $CPTRange2 = $('.cptRange2');
        var $CPTNoPer = $('.cptNoPer');
        var $CPTTotalUnits = $('.cptTotalUnits');
        var $IncLetter = $('.includeLetter');
        if ($CPTCodes.length > 0) {
            for (var p = 0; p < $CPTCodes.length; p++) {
                $CPTCodes[p].name = 'CPTCodes[' + p + '].CPTCode';
                $CPTCodes[p].id = 'CPTCodes_' + p + '__CPTCode';
                $CPTDescriptions[p].name = 'CPTCodes[' + p + '].CPTDesc';
                $CPTDescriptions[p].id = 'CPTCodes_' + p + '__CPTDesc';
                $CPTModifiers[p].name = 'CPTCodes[' + p + '].Modifier';
                $CPTModifiers[p].id = 'CPTCodes_' + p + '__Modifier';
                $CPTDisciplines[p].name = 'CPTCodes[' + p + '].Discipline';
                $CPTDisciplines[p].id = 'CPTCodes_' + p + '__Discipline';
                $CPTReqUnits[p].name = 'CPTCodes[' + p + '].RequestedUnits';
                $CPTReqUnits[p].id = 'CPTCodes_' + p + '__RequestedUnits';
                if (($('#PlaceOfService').val() == '21- IP HOSPITAL') || ($('#PlaceOfService').val() == '31- SNF')) {
                }
                else {
                    //$IncLetter[p].id = 'CPTCodes_' + p + '__IncludeLetter';
                    //$IncLetter[p].name = 'CPTCodes[' + p + '].IncludeLetter';
                }

                if ($CPTRange1.length > 0) {
                    if (typeof $CPTRange1[p] != 'undefined') {
                        $CPTRange1[p].name = 'CPTCodes[' + p + '].Range1';
                        $CPTRange1[p].id = 'CPTCodes_' + p + '__Range1';
                    }
                }
                if ($CPTRange2.length > 0) {
                    if (typeof $CPTRange2[p] != 'undefined') {
                        $CPTRange2[p].name = 'CPTCodes[' + p + '].Range2';
                        $CPTRange2[p].id = 'CPTCodes_' + p + '__Range2';
                    }
                }
                if ($CPTNoPer.length > 0) {
                    if (typeof $CPTNoPer[p] != 'undefined') {
                        $CPTNoPer[p].name = 'CPTCodes[' + p + '].NumberPer';
                        $CPTNoPer[p].id = 'CPTCodes_' + p + '__NumberPer';
                    }
                }
                if ($CPTTotalUnits.length > 0) {
                    if (typeof $CPTTotalUnits[p] != 'undefined') {
                        $CPTTotalUnits[p].name = 'CPTCodes[' + p + '].TotalUnits';
                        $CPTTotalUnits[p].id = 'CPTCodes_' + p + '__TotalUnits';
                    }
                }

                if ($CPTCodes.length == 1) {
                    $('.cptButtonArea').eq(p).html(addCPTBtn);
                }
                else {
                    if ((p > -1 && p <= ($CPTCodes.length - 2)) && ($CPTCodes.length > 1)) {
                        $('.cptButtonArea').eq(p).html(delCPTBtn);
                    }
                    else {
                        $('.cptButtonArea').eq(p).html(delAddCPTBtn);
                    }
                }
            }
        }
        p = 0
    };

    $('.dateTimePicker').datetimepicker(
        {
            widgetPositioning: {
                vertical: 'bottom'
            }
        });
    $('.datePicker').datetimepicker({
        format: 'MM/DD/YYYY',
        widgetPositioning: {
            vertical: 'bottom'
        }
    });
    /* ICD CODES MANAGEMENT */


    $('#ICDArea').off('click', '.add_icd_btn').on('click', '.add_icd_btn', function () {
        var $row = $(this).parents('.ICDRow');
        var childCount = parseInt($row.parent().children().length);
        var rowIndex = parseInt($row.index());
        var ICDRowTemplate = getICDRowTemplate(rowIndex);

        if (childCount == (rowIndex + 1)) {
            $row.parent().append(ICDRowTemplate);
        }
        else {
            $(ICDRowTemplate).insertAfter($row);
        }
        showFish();
        updateICDMapping();
    });

    getICDRowTemplate = function (rowIndex) {
        if (($('#PlaceOfService').val() == '12- PATIENT HOME') || ($('#PlaceOfService').val() == '62- CORF')) {
            var ICDRowTemplate = '<div class="ICDRow">' +
                  '<div class="col-lg-4 col-md-4 col-sm-4 col-xs-6  zero-padding-left-right">' +
                  '<input class="form-control input-xs mandatory_field_halo icdCode_search_cum_dropdown icdCode summaryDataChange" placeholder="ICD CODE" id="ICDCodes_' + (rowIndex + 1) + '__ICDCode" name="ICDCodes[' + (rowIndex + 1) + '].ICDCode" />' +
                  '</div>' +
                  '<div class="col-lg-4 col-md-5 col-sm-5 col-xs-6  zero-padding-left-right">' +
                  '<input class="form-control input-xs mandatory_field_halo icdDescription_search_cum_dropdown icdDescription summaryDataChange" placeholder="DESCRIPTION" id="ICDCodes_' + (rowIndex + 1) + '__ICDCodeDescription" name="ICDCodes[' + (rowIndex + 1) + '].ICDCodeDescription" />' +
                  '</div>' +
                  '<div class="col-lg-4 col-md-3 col-sm-3 col-xs-6 button-styles-xs zero-padding-left-right icdButtonArea"></div>' +
                  '<div class="clearfix"></div>' +
                  '</div>';
        }
        else {
            var ICDRowTemplate = '<div class="ICDRow">' +
                  '<div class="col-lg-3 col-md-4 col-sm-8 col-xs-4 zero-padding-left-right">' +
                  '<input class="form-control input-xs mandatory_field_halo icdCode_search_cum_dropdown icdCode loser_field summaryDataChange" placeholder="ICD CODE" id="ICDCodes_' + (rowIndex + 1) + '__ICDCode" name="ICDCodes[' + (rowIndex + 1) + '].ICDCode" />' +
                  '</div>' +
                  '<div class="col-lg-6 col-md-6 col-sm-12 col-xs-7 zero-padding-left-right">' +
                  '<input class="form-control input-xs mandatory_field_halo icdDescription_search_cum_dropdown icdDescription loser_field summaryDataChange" placeholder="DESCRIPTION" id="ICDCodes_' + (rowIndex + 1) + '__ICDCodeDescription" name="ICDCodes[' + (rowIndex + 1) + '].ICDCodeDescription" />' +
                  '</div>' +
                  '<div class="col-lg-3 col-md-2 col-sm-2 col-xs-1 button-styles-xs zero-padding-left-right icdButtonArea"></div>' +
                  '<div class="clearfix"></div>' +
                  '</div>';
        }
        return ICDRowTemplate;
    };

    $('#ICDArea').on('click', '.delete_icd_btn', function () {
        $(this).parents('.ICDRow').remove();
        updateICDMapping();
        showFish();
        authSummaryTable();//update auth summary table
    });
    /* /ICD CODES MANAGEMENT */


    /*CPT CODES MANAGEMENT*/
    $("#CPTAreaContainer").off("click", ".addcptrangebtn").on("click", ".addcptrangebtn", function () {
        TabManager.openFloatingModal('~/Areas/UM/Views/Common/Modal/_GetAddCPTRangeModal.cshtml', '~/Areas/UM/Views/Common/Modal_Header_Footer/AddCPTRange/_CPTRangeHeader.cshtml', '~/Areas/UM/Views/Common/Modal_Header_Footer/AddCPTRange/_CPTRangeFooter.cshtml', '', '');
    });

    /*GENERATE SEARCHCUMDROPDOWN FOR SELECTING CPT CODES*/
    var optn = '';
    //for (var i = 0; i < AllCPTCodes[0].length; i++) {
    //    optn = optn + '<option value="' + AllCPTCodes[0][i].Code + '-' + AllCPTCodes[0][i].ShortDescription + '">' + AllCPTCodes[0][i].Code + '-' + AllCPTCodes[0][i].ShortDescription + '</option>';
    //}
    $("#cptcodeselect0").append(optn);
    // $("#cptcodeselect0").customselect();
    /*------------------------------------------------------------------------------------*/
    CPTCodesDataObj = []; //CPT CODES DATA OBJECT



    /*ADD NEW ROW FOR CPT CODE SELECTION*/
    $('#CPTArea').off('click', '.plusButton').on('click', '.plusButton', function () {
        var $row = $(this).parents('.CPTRow');
        var childCount = parseInt($row.parent().children().length);
        var rowIndex = parseInt($row.index());
        $(this).addClass('hidden')
        $(".plain_language_btn").addClass('hidden');
        $(".smartIntelligenceBtn").addClass('hidden');
        $('.minusButton').removeClass("hidden");
        var CPTRowTemplate = getCPTTemplate(rowIndex);
        if (childCount == (rowIndex + 1)) {
            $row.parent().append(CPTRowTemplate);
        }
        else {
            $(CPTRowTemplate).insertAfter($row);
        }
        updateCPTMapping();
        $('.plain_language_btn').attr("disabled", "disabled");
        if ($('.includeLetter').length > 0) {
            for (var index = 0; index < $('.includeLetter').length; index++) {
                if ($('.includeLetter')[index].checked) {
                    $('.plain_language_btn').removeAttr("disabled");
                    return;
                }
            }
            $('.plain_language_btn').attr("disabled", "disabled");
        }
        showFish();
    });
    getCPTTemplate = function (rowIndex) {
        if (($('#PlaceOfService').val().split("-")[0] == '12') || ($('#PlaceOfService').val().split("-")[0] == '62')) {
            var CPTRowTemplate = '<div class="CPTRow" id="' + (rowIndex + 1) + '">' +
           '<div class="col-lg-1 theme_label_data zero-padding-left-right">' +
           '<input id="CPTCodes_' + (rowIndex + 1) + '__CPTCode" name="CPTCodes[' + (rowIndex + 1) + '].CPTCode" class="form-control cptCodes mandatory_field_halo cptCode_search_cum_dropdown input-xs summaryDataChange" placeholder="CPT CODE"/>' +
           '</div>' +
           '<div class="col-lg-1 theme_label_data zero-padding-left-right">' +
           '<input class="form-control input-xs cptModifier" id="CPTCodes_' + (rowIndex + 1) + '__Modifier" name="CPTCodes[' + (rowIndex + 1) + '].Modifier" placeholder="MODIFIER" type="text">' +
           '</div>' +
                                     '<div class="col-lg-1 theme_label_data zero-padding-left-right">' +
                                     '<input class="form-control input-xs mandatory_field_halo cptDescription_search_cum_dropdown cptDescriptions summaryDataChange" id="CPTCodes_' + (rowIndex + 1) + '__CPTDesc" name="CPTCodes[' + (rowIndex + 1) + '].CPTDesc" placeholder="DESCRIPTION" type="text">' +
                                     '</div>' +

                                     '<div class="col-lg-1 theme_label_data zero-padding-left-right">' +
                                     '<select class="form-control input-xs cptDiscipline" id="CPTCodes_' + (rowIndex + 1) + '__Disicipline" name="CPTCodes[' + (rowIndex + 1) + '].Disicipline"  placeholder="REQ UNITS" type="text"><option value="">Select</option></select>' +
                                     '</div>' +

                                     '<div class="col-lg-1 theme_label_data cptrequnits_rightmargin zero-padding-left-right">' +
                                     '<input class="form-control input-xs cptReqUnits cptUnits" id="CPTCodes_' + (rowIndex + 1) + '__RequestedUnits" name="CPTCodes[' + (rowIndex + 1) + '].RequestedUnits"  data-val="true" data-val-number="The field RequestedUnits must be a number." placeholder="REQ UNITS" type="text">' +
                                     '</div>' +


                                     '<div class="col-lg-1 theme_label_data  zero-padding-left-right">' +
                                     '<select class="form-control input-xs cptRange1 cptUnits" id="CPTCodes_' + (rowIndex + 1) + '__Range1"  name="CPTCodes[' + (rowIndex + 1) + '].Range1"  placeholder="Range 2" type="text"><option value="">Select</option></select>' +
                                     '</div>' +

                                     '<div class="col-lg-1 theme_label_data cptrequnits_rightmargin zero-padding-left-right">' +
                                     '<input class="form-control input-xs cptNoPer cptUnits" id="CPTCodes_' + (rowIndex + 1) + '__NumberPer" name="CPTCodes[' + (rowIndex + 1) + '].NumberPer" placeholder="No Per" type="text">' +
                                     '</div>' +

                                     '<div class="col-lg-1 theme_label_data zero-padding-left-right">' +
                                     '<select class="form-control input-xs cptRange2 cptUnits" id="CPTCodes_' + (rowIndex + 1) + '__Range2" name="CPTCodes[' + (rowIndex + 1) + '].Range2"  placeholder="Range 2" type="text"><option value="">Select</option></select>' +
                                     '</div>' +

                                     '<div class="col-lg-1 theme_label_data cptrequnits_rightmargin zero-padding-left-right">' +
                                     '<input class="form-control input-xs cptTotalUnits summaryDataChange" id="CPTCodes_' + (rowIndex + 1) + '__TotalUnits" name="CPTCodes[' + (rowIndex + 1) + '].TotalUnits" placeholder="Total Units" type="text">' +
                                     '</div>' +

                                     '<div class="col-lg-1 col-md-1 col-sm-2 col-xs-1 pull-left button-styles-xs row-action theme_label_data cptButtonArea zero-padding-left-right"></div>' +
                                     '<div class="col-lg-1 text-center theme_label_data zero-padding-left-right">' +
                                     //'<input type="checkbox" class="checkbox-radio includeLetter" id="CPTCodes_' + (rowIndex + 1) + '__IncludeLetter" tabindex="-1" name="CPTCodes[' + (rowIndex + 1) + '].IncludeLetter" value="IncLetter" disabled><label class="" for="CPTCodes_' + (rowIndex + 1) + '__IncludeLetter"><span></span></label>' +
                                     '<button class="btn btn-danger btn-xs smartIntelligenceBtn bold_text calypso_ai_btn" type="button"> CALYPSO AI</button>' +
                                     '</div>' +
                                     '<div class="clearfix"></div>' +
                                     '</div>';
        }
        else if (($('#PlaceOfService').val() == '21- IP HOSPITAL') || ($('#PlaceOfService').val() == '31- SNF')) {

            var CPTRowTemplate = '<div class="CPTRow">' +
                      '<div class="col-lg-2 theme_label_data zero-padding-left-right">' +
                      '<input id="CPTCodes_' + (rowIndex + 1) + '__CPTCode" name="CPTCodes[' + (rowIndex + 1) + '].CPTCode" class="form-control input-xs cptCode_search_cum_dropdown cptCodes summaryDataChange" placeholder="CPT CODE"/>' +
                      '</div>' +
                      '<div class="col-lg-1 theme_label_data zero-padding-left-right">' +
                      '<input class="form-control input-xs cptModifier" id="CPTCodes_' + (rowIndex + 1) + '__Modifier" name="CPTCodes[' + (rowIndex + 1) + '].Modifier" placeholder="MODIFIER" type="text">' +
                      '</div>' +
                      '<div class="col-lg-3 theme_label_data zero-padding-left-right">' +
                      '<input class="form-control input-xs cptDescription_search_cum_dropdown cptDescriptions summaryDataChange" id="CPTCodes_' + (rowIndex + 1) + '__CPTDesc" name="CPTCodes[' + (rowIndex + 1) + '].CPTDesc" placeholder="DESCRIPTION" type="text">' +
                      '<input class="form-control input-xs cptDiscipline" id="CPTCodes_' + (rowIndex + 1) + '__Discipline" name="CPTCodes[' + (rowIndex + 1) + '].Discipline" placeholder="DICIPLINE" type="text" hiddden style="Display:none">' +
                      '</div>' +
                      '<div class="col-lg-1 theme_label_data cptrequnits_rightmargin zero-padding-left-right">' +
                      '<input class="form-control input-xs cptReqUnits" id="CPTCodes_' + (rowIndex + 1) + '__RequestedUnits" name="CPTCodes[' + (rowIndex + 1) + '].RequestedUnits"  data-val="true" data-val-number="The field RequestedUnits must be a number." placeholder="REQ UNITS" type="text">' +
                      '</div>' +
                      '<div class="col-lg-2 col-md-2 col-sm-2 col-xs-1 pull-left button-styles-xs row-action theme_label_data cptButtonArea zero-padding-left-right"></div>' +
                      '<div class="col-lg-3 col-md-1 col-sm-2 col-xs-1 cptInc_pullleft zero-padding-left-right pull-left">' +
                      '<button class="btn btn-danger btn-xs smartIntelligenceBtn bold_text calypso_ai_btn" type="button"> CALYPSO AI</button>' +
                      '</div>' +
                      '<div class="clearfix"></div>' +
                      '</div>';
        }
        else {

            var CPTRowTemplate = '<div class="CPTRow">' +
                      '<div class="col-lg-2 theme_label_data zero-padding-left-right">' +
                      '<input id="CPTCodes_' + (rowIndex + 1) + '__CPTCode" name="CPTCodes[' + (rowIndex + 1) + '].CPTCode" class="form-control mandatory_field_halo input-xs loser_field cptCode_search_cum_dropdown cptCodes summaryDataChange" placeholder="CPT CODE"/>' +
                      '</div>' +
                      '<div class="col-lg-1 theme_label_data zero-padding-left-right">' +
                      '<input class="form-control input-xs cptModifier" id="CPTCodes_' + (rowIndex + 1) + '__Modifier"  name="CPTCodes[' + (rowIndex + 1) + '].Modifier" placeholder="MODIFIER" type="text">' +
                      '</div>' +
                      '<div class="col-lg-3 theme_label_data zero-padding-left-right">' +
                      '<input class="form-control input-xs mandatory_field_halo loser_field cptDescription_search_cum_dropdown cptDescriptions summaryDataChange" id="CPTCodes_' + (rowIndex + 1) + '__CPTDesc" name="CPTCodes[' + (rowIndex + 1) + '].CPTDesc" placeholder="DESCRIPTION" type="text">' +
                      '<input class="form-control input-xs cptDiscipline" id="CPTCodes_' + (rowIndex + 1) + '__Discipline" name="CPTCodes[' + (rowIndex + 1) + '].Discipline" placeholder="DICIPLINE" type="text" hiddden style="Display:none">' +
                      '</div>' +
                      '<div class="col-lg-1 theme_label_data cptrequnits_rightmargin zero-padding-left-right">' +
                      '<input class="form-control input-xs cptReqUnits" id="CPTCodes_' + (rowIndex + 1) + '__RequestedUnits" name="CPTCodes[' + (rowIndex + 1) + '].RequestedUnits"  data-val="true" data-val-number="The field RequestedUnits must be a number." placeholder="REQ UNITS" type="text">' +
                      '</div>' +
                      '<div class="col-lg-2 col-md-2 col-sm-2 col-xs-1 pull-left button-styles-xs row-action theme_label_data cptButtonArea zero-padding-left-right"></div>' +
                      '<div class="col-lg-3 col-md-1 col-sm-2 col-xs-1 cptInc_pullleft zero-padding-left-right pull-left">' +
                       //'<input type="checkbox" class="checkbox-radio includeLetter" id="CPTCodes_' + (rowIndex + 1) + '__IncludeLetter" tabindex="-1" name="CPTCodes[' + (rowIndex + 1) + '].IncludeLetter" value="IncLetter" disabled><label class="" for="CPTCodes_' + (rowIndex + 1) + '__IncludeLetter"><span></span></label>' +
                      '<button class="btn btn-danger btn-xs smartIntelligenceBtn bold_text loser_field calypso_ai_btn" type="button"> CALYPSO AI</button>' +
                      '</div>' +
                      '<div class="clearfix"></div>' +
                      '</div>';

        }
        return CPTRowTemplate;
    };
    /*------------------------------------------------------------------------------------*/
    /*DELETE ADDED ROW FOR CPT CODE SELECTED*/
    $('#CPTArea').off('click', '.minusButton').on('click', '.minusButton', function () {
        var $row = $(this).parents('.CPTRow');
        var childCount = parseInt($row.parent().children().length);
        var rowIndex = parseInt($row.index());
        if (childCount > 2) {
            if (childCount == (rowIndex + 1)) {
                $(this).parents('.CPTRow').prev().children().children('.plusButton').removeClass('hidden')
                $(this).parents('.CPTRow').prev().children().children('.plain_language_btn').removeClass('hidden')
                $(this).parents('.CPTRow').prev().children().children('.smartIntelligenceBtn').removeClass('hidden')
                $(this).parents('.CPTRow').remove();
            }
            else {
                $(this).parents('.CPTRow').remove();
            }
        }
        else {
            $(this).parents('.CPTRow').remove();
            $('.plain_language_btn').removeClass("hidden");
            $(".smartIntelligenceBtn").removeClass('hidden');
        }
        if ($('.includeLetter').length > 0) {
            for (var index = 0; index < $('.includeLetter').length; index++) {
                if ($('.includeLetter')[index].checked) {
                    $('.plain_language_btn').removeAttr("disabled");
                    return;
                }
            }
            $('.plain_language_btn').attr("disabled", "disabled");
        }
        authSummaryTable();//update auth summary table
        updateCPTMapping();
        showFish();

    });
    var fishTemplate = '<img class="square-small" src="/Areas/UM/Resources/Images/clownfish.gif" />';

    function goRight() {
        $('#fishArea')
          .find('.square-small').addClass("mirror")
          .animate({
              left: $('#fishArea').width() - 100
          },
            2000, 'linear'
          );

    }

    function goLeft() {
        $('#fishArea')
          .find('.square-small').removeClass("mirror")
          .animate({
              left: 0
          },
            2000, 'linear'
          );

    }
    var n = 1;
    var fishInt;
    showFish = function () {
        var cptCount = $('.CPTRow').length;
        var icdCount = $('.ICDRow').length;

        if (cptCount > icdCount) {
            var CPTDivHeight = $('#CPTAreaContainer').height();
            var ICDDivHeight = $('#ICDDiv').height();
            var FishDivHeight = CPTDivHeight - ICDDivHeight;
            var FishDivWidth = $('#ICDDiv').width();
            if ((cptCount - icdCount) > 2) {
                $('#fishArea').css({ "height": FishDivHeight, "width": FishDivWidth });
                $('#fishArea').show();
            }
            else {
                $('#fishArea').hide();
            }
        }

        else if (cptCount < icdCount) {
            var CPTDivHeight = $('#CPTAreaContainer').height();
            var ICDDivHeight = $('#ICDDiv').height();
            var FishDivHeight = ICDDivHeight - CPTDivHeight;
            var FishDivWidth = $('#CPTAreaContainer').width();
            if ((icdCount - cptCount) > 2) {
                $('#fishCPTArea').css({ "height": FishDivHeight, "width": FishDivWidth });
                $('#fishCPTArea').show();
            }
            else {
                $('#fishCPTArea').hide();
            }
        }
        else {
            $('#fishArea').hide();
            $('#fishCPTArea').hide();
        }
    };

    /*------------------------------------------------------------------------------------*/
    /*CHECKBOX CHECKED FOR SELECTED CPT CODE*/
    $("#CPTArea").on("ifClicked", '.includeletter', function () {
        var id = $(this).attr('id');
        if (!$("#" + id).is(':checked')) //if not checked
        {
            $("#" + id).iCheck('check');
            var rowdataobj = {}
            $("#" + id).parents(".CPTRow").children('div').each(function (i) {
                if ($(this).find('input').val()) {
                    var key = $(this).find('input').attr('key');
                    var value = $(this).find('input').val();
                    rowdataobj[key] = value;
                }
            })
            CPTCodesDataObj.push(rowdataobj);
        }
        else {
            $("#" + id).iCheck('uncheck');
        }
    })

    /*GET THE PLAIN LANGUAGE PARTIAL VIEW*/
    $("#CPTArea").off('click', '.plain_language_btn').on('click', '.plain_language_btn', function () {
        if (($('#PlaceOfService').val().split("-")[0] == '12') || ($('#PlaceOfService').val().split("-")[0] == '62')) {
            $('.PlainLangDesc_DummyDiv').remove();
            var cptdataobj = [];
            var PlainLang_Html = "";
            //for (var index = 0; index < $('.plain_language_Btn').length; index++) {
            for (var index = 0; index < $('#CPTArea')[0].children.length; index++) {
                if (document.getElementsByName('CPTCodes' + '[' + index + ']' + '.IncludeLetter')[0].checked == true) {

                    if ($('#CPTCodes_' + (index) + '__Discipline')[0].value != null && $('#CPTCodes_' + (index) + '__Discipline')[0].value != "" && $('#CPTCodes_' + (index) + '__Discipline')[0].value != "Select") {
                        if ($('#CPTCodes_' + (index) + '__RequestedUnits')[0].value != "" && $('#CPTCodes_' + (index) + '__NumberPer')[0].value != "" && $('#CPTCodes_' + (index) + '__Range1')[0].value != "" && $('#CPTCodes_' + (index) + '__Range2')[0].value != "") {

                            var sentence = $('#CPTCodes_' + (index) + '__Discipline')[0].value + " "
                                + $('#CPTCodes_' + (index) + '__RequestedUnits')[0].value + " " + " time(s) a "
                                + $('#CPTCodes_' + (index) + '__Range1')[0].value + " for "
                                + $('#CPTCodes_' + (index) + '__NumberPer')[0].value + " "
                                + $('#CPTCodes_' + (index) + '__Range2')[0].value + ", for a total of " +
                                $('#CPTCodes_' + (index) + '__TotalUnits')[0].value + " unit(s)";
                            cptdataobj.push(sentence);
                        }
                        else {
                            cptdataobj.push($('#CPTCodes_' + (index) + '__Discipline')[0].value);
                        }

                    }

                    else {
                        cptdataobj.push($('#CPTCodes_' + (index) + '__CPTDesc')[0].value);
                    }

                    PlainLang_Html = PlainLang_Html + '<div  class="hidden PlainLangDesc_DummyDiv">';
                    PlainLang_Html = PlainLang_Html + '<input type="text" name="CPTCodes[' + index + '].PlainLang" value="' + cptdataobj[index] + '" id="PlainLangDesc_Hidden[' + index + ']"/>' + '<br/>';
                    PlainLang_Html = PlainLang_Html + '</div>';
                }
                else {
                    cptdataobj.push("");
                }


            }
            $('#UM_auth_form').append(PlainLang_Html);

            //var $form = $("#UM_auth_form");
            //var formData = new FormData($form[0]);
            //$.ajax({
            //    type: 'POST',
            //    url: '/UM/Authorization/AddPlainlanguage?PlainLanguages=' + cptdataobj,
            //    data: formData,
            //    processData: false,
            //    contentType: false,
            //    cache: false,
            //    success: function (data) {

            //    }
            //})

            var cptdata_obj = $("#UM_auth_form").serialize();
            TabManager.openSideModal('/UM/Authorization/GetPlainLanguages', 'Plain Language', 'both', '', '', '', cptdata_obj);
        }
            // ---------------------
        else if (($('#PlaceOfService').val() == '21- IP HOSPITAL') || ($('#PlaceOfService').val() == '31- SNF')) {
        }
        else {
            $('.PlainLangDesc_DummyDiv').remove();
            var cptdataobj = [];
            var PlainLang_Html = "";
            //for (var index = 0; index < $('.plain_language_Btn').length; index++) {
            for (var index = 0; index < $('#CPTArea')[0].children.length; index++) {
                if (document.getElementsByName('CPTCodes' + '[' + index + ']' + '.IncludeLetter')[0].checked == true) {
                    cptdataobj.push($('#CPTCodes_' + (index) + '__CPTDesc')[0].value);
                    PlainLang_Html = PlainLang_Html + '<div  class="hidden PlainLangDesc_DummyDiv">';
                    PlainLang_Html = PlainLang_Html + '<input type="text" name="CPTCodes[' + index + '].PlainLang" value="' + cptdataobj[index] + '" id="PlainLangDesc_Hidden[' + index + ']"/>' + '<br/>';
                    PlainLang_Html = PlainLang_Html + '</div>';
                }
                else {
                    cptdataobj.push("");
                }
            }
            $('#UM_auth_form').append(PlainLang_Html);
            var cptdata_obj = $("#UM_auth_form").serialize();
            TabManager.openSideModal('/UM/Authorization/GetPlainLanguages', 'Plain Language', 'both', '', '', '', cptdata_obj);
        }
    })
    /* Function for POS - 12 plain language */
    //$("#CPTArea").off('click', '.plain_language_btn').on('click', '.plain_language_btn', function () {      
    //})

    $("#CPTArea").off('click', '.calypso_ai_btn').on('click', '.calypso_ai_btn', function () {
        var SubscriberID = $("#MemberID").text();
        var CptCodes = [];
        var CptInput = $(".cptCodes:input");
        if (CptInput && CptInput.length > 0) {
            for (var k = 0; k < CptInput.length; k++) {
                var value = CptInput[k].value;
                if (value)
                    CptCodes.push(value);
            }
        }
        var ServiceOrAttendingNPI = $("input[name='ServicingProvider.NPI']").val();
        var ServiceOrAttendingSpeciality = $("input[name='ServicingProvider.Specialty']").val();
        var FacilityNPI = $("input[name='Facility.FacilityNPI']").val();
        TabManager.openFloatingModal('/Authorization/CalypsoAIDuplicateCPTS?CptCodes=' + CptCodes + "&&ServiceOrAttendingNPI=" + ServiceOrAttendingNPI + "&&ServiceOrAttendingSpeciality=" + ServiceOrAttendingSpeciality + "&&FacilityNPI=" + FacilityNPI + "&&SubscriberID=" + SubscriberID, '~/Areas/UM/Views/Common/Modal_Header_Footer/CalypsoAI/Header/_AIHeader.cshtml', '~/Areas/UM/Views/Common/Modal_Header_Footer/CalypsoAI/Footer/_Cancel.cshtml', '', '', '');
    })

    /*------------------------------------------------------------------------------------*/
    $('#PlaceOfService').focus();
    $('.auth_form').on('change', '.loser_field', function () {
        $(this).removeClass("mandatory_field_halo");
        $(this).prop("placeholder", "");
    });


    $('.auth_form').on('click', '.loser_field', function () {
        lastTab = $(this).prop("id");
        $(this).removeClass("mandatory_field_halo");
        $(this).prop("placeholder", "");
    });
    $('.auth_form').on('keydown', '.loser_field', function () {
        $(this).removeClass("mandatory_field_halo");
        $(this).prop("placeholder", "");
        lastTab = $(this).prop("id");
        if ($(this).prop("id") == 'ExpectedDOS') {
            setTimeout(function () {
                $('.ICDRow').children().children().first().focus();
                return;
            }, 500);
        }
        var p = $(this);
        RecdDateModal(p);
    });

    $('#SharedFloatingModalContent').on('click', '.recd_Date_Close_Btn', function () {
        $('#' + lastTab).focus();
    });
});


//---icdCode_search_cum_dropdown
var icdCodevar = "";
var icdDesVar = "";
$(document).ready(function () {
    //----------Search-cum-dropDown-------------------//
    var ResultData = {};
    //----------------Invokes this function---------------------//
    $('#ICDArea').off('keyup', '.icdCode_search_cum_dropdown').on('keyup', '.icdCode_search_cum_dropdown', function () {
        $(this).attr('autocomplete', 'off');
        icdCodevar = "ICDCodes_" + this.id.split("_")[1] + "__ICDCode";
        icdDesVar = "ICDCodes_" + this.id.split("_")[1] + "__ICDCodeDescription";
        getSearchCodeValues($(this).val(), this);//getting result for provider
    })
    $('#ICDArea').off('keyup', '.icdDescription_search_cum_dropdown').on('keyup', '.icdDescription_search_cum_dropdown', function () {
        //$('.icdCode_search_cum_dropdown').keyup(function () {
        $(this).attr('autocomplete', 'off');
        icdCodevar = "ICDCodes_" + this.id.split("_")[1] + "__ICDCode";
        icdDesVar = "ICDCodes_" + this.id.split("_")[1] + "__ICDCodeDescription";
        getSearchDescriptionValues($(this).val(), this);//getting result for provider
    })
    //----------------------END-------------------------------//
    /* ---- functioin to check whether the plain language button should be disabled or not ----- */
    var CheckForDisability_PlainLanguageBtn = function () {
        if ($('.includeLetter').length > 0) {
            for (var index = 0; index < $('.includeLetter').length; index++) {
                if ($('.includeLetter')[index].checked) {
                    $('.plain_language_btn').removeAttr("disabled");
                    return;
                }
            }
            $('.plain_language_btn').attr("disabled", "disabled");
        }
    }

    // --- ends -----
    /*  -- On change function for inc letter --- */
    $('#CPTArea').off('change', '.includeLetter').on('change', '.includeLetter', function () {
        CheckForDisability_PlainLanguageBtn();
    })
    // --- ends---
    // --- Functions on change of code , desc for POS *** 11**** TYPES------------
    $('.plain_language_btn').attr("disabled", "disabled");
    $('.includeLetter').attr("disabled", "disabled");
    $('.CPTAreaPos11').off('keyup', '.cptCodes').on('keyup', '.cptCodes', function () {
        CheckForIncLetter_CPTCodePOS11($(this));
    })
    $('.CPTAreaPos11').off('click', '.cptCodes').on('click', '.cptCodes', function () {
        CheckForIncLetter_CPTCodePOS11($(this));
    })
    $('.CPTAreaPos11').off('blur', '.cptCodes').on('blur', '.cptCodes', function () {
        CheckForIncLetter_CPTCodePOS11($(this));
    })
    $('.CPTAreaPos11').off('change', '.cptCodes').on('change', '.cptCodes', function () {
        CheckForIncLetter_CPTCodePOS11($(this));
    })
    // check for inc letter functionality - disable and enable ---- 
    function CheckForIncLetter_CPTCodePOS11(e) {
        var $row = e.parents('.CPTRow');
        var rowIndex = parseInt($row.index());
        var ID = e[0].id;
        if (e[0].value != null && e[0].value != "") {
            document.getElementsByName('CPTCodes' + '[' + rowIndex + ']' + '.IncludeLetter')[0].checked = true;
            document.getElementsByName('CPTCodes' + '[' + rowIndex + ']' + '.IncludeLetter')[0].disabled = false;
        }
        else if (($('#CPTCodes_' + (rowIndex) + '__CPTDesc')[0].value != null) && ($('#CPTCodes_' + (rowIndex) + '__CPTDesc')[0].value != "")) {
            document.getElementsByName('CPTCodes' + '[' + rowIndex + ']' + '.IncludeLetter')[0].checked = true;
            document.getElementsByName('CPTCodes' + '[' + rowIndex + ']' + '.IncludeLetter')[0].disabled = false;
        }
        else {
            document.getElementsByName('CPTCodes' + '[' + rowIndex + ']' + '.IncludeLetter')[0].checked = false;
            document.getElementsByName('CPTCodes' + '[' + rowIndex + ']' + '.IncludeLetter')[0].disabled = true;
        }
        CheckForDisability_PlainLanguageBtn();
    }
    // -----on change blur click and key ups----------
    $('.CPTAreaPos11').off('keyup', '.cptDescriptions').on('keyup', '.cptDescriptions', function () {
        CheckForIncLetter_CPTDescPOS11($(this));
    })
    $('.CPTAreaPos11').off('click', '.cptDescriptions').on('click', '.cptDescriptions', function () {
        CheckForIncLetter_CPTDescPOS11($(this));
    })
    $('.CPTAreaPos11').off('blur', '.cptDescriptions').on('blur', '.cptDescriptions', function () {
        CheckForIncLetter_CPTDescPOS11($(this));
    })
    $('.CPTAreaPos11').off('change', '.cptDescriptions').on('change', '.cptDescriptions', function () {
        CheckForIncLetter_CPTDescPOS11($(this));
    })
    // check for inc letter functionality - disable and enable ---- 
    function CheckForIncLetter_CPTDescPOS11(e) {
        var $row = e.parents('.CPTRow');
        var rowIndex = parseInt($row.index());
        var ID = e[0].id;
        if (e[0].value != null && e[0].value != "") {
            document.getElementsByName('CPTCodes' + '[' + rowIndex + ']' + '.IncludeLetter')[0].checked = true;
            document.getElementsByName('CPTCodes' + '[' + rowIndex + ']' + '.IncludeLetter')[0].disabled = false;
        }
        else if (($('#CPTCodes_' + (rowIndex) + '__CPTCode')[0].value != null) && ($('#CPTCodes_' + (rowIndex) + '__CPTCode')[0].value != "")) {
            document.getElementsByName('CPTCodes' + '[' + rowIndex + ']' + '.IncludeLetter')[0].checked = true;
            document.getElementsByName('CPTCodes' + '[' + rowIndex + ']' + '.IncludeLetter')[0].disabled = false;
        }
        else {
            document.getElementsByName('CPTCodes' + '[' + rowIndex + ']' + '.IncludeLetter')[0].checked = false;
            document.getElementsByName('CPTCodes' + '[' + rowIndex + ']' + '.IncludeLetter')[0].disabled = true;
        }
        CheckForDisability_PlainLanguageBtn();
    }
    // ---- ends -- POS ****11****** TYPES-------


    /* --- On change functions for code , description, discipline to enable disable the inc letter --- This is for POS ***12***TYPES-----*/
    $('.CPTAreaPos12').off('change', '.cptDiscipline').on('change', '.cptDiscipline', function () {
        var $row = $(this).parents('.CPTRow');
        var rowIndex = parseInt($row.index());
        var ID = $(this)[0].id;
        if ($(this)[0].value != null && $(this)[0].value != "" && $(this)[0].value != "Select") {
            document.getElementsByName('CPTCodes' + '[' + rowIndex + ']' + '.IncludeLetter')[0].checked = true;
            document.getElementsByName('CPTCodes' + '[' + rowIndex + ']' + '.IncludeLetter')[0].disabled = false;
        }
        else if (($('#CPTCodes_' + (rowIndex) + '__CPTDesc')[0].value != null) && ($('#CPTCodes_' + (rowIndex) + '__CPTDesc')[0].value != "") && ($('#CPTCodes_' + (rowIndex) + '__CPTCode')[0].value != null) && ($('#CPTCodes_' + (rowIndex) + '__CPTCode')[0].value != "")) {
            document.getElementsByName('CPTCodes' + '[' + rowIndex + ']' + '.IncludeLetter')[0].checked = true;
            document.getElementsByName('CPTCodes' + '[' + rowIndex + ']' + '.IncludeLetter')[0].disabled = false;
        }
        else {
            document.getElementsByName('CPTCodes' + '[' + rowIndex + ']' + '.IncludeLetter')[0].checked = false;
            document.getElementsByName('CPTCodes' + '[' + rowIndex + ']' + '.IncludeLetter')[0].disabled = true;
        }
        CheckForDisability_PlainLanguageBtn();
    })

    // -----on change blur click and key ups----------
    $('.CPTAreaPos12').off('keyup', '.cptCodes').on('keyup', '.cptCodes', function () {
        CheckForIncLetter_CPTCode($(this));
    })
    $('.CPTAreaPos12').off('click', '.cptCodes').on('click', '.cptCodes', function () {
        CheckForIncLetter_CPTCode($(this));
    })
    $('.CPTAreaPos12').off('blur', '.cptCodes').on('blur', '.cptCodes', function () {
        CheckForIncLetter_CPTCode($(this));
    })
    $('.CPTAreaPos12').off('change', '.cptCodes').on('change', '.cptCodes', function () {
        CheckForIncLetter_CPTCode($(this));
    })
    // check for inc letter functionality - disable and enable ---- 
    function CheckForIncLetter_CPTCode(e) {
        var $row = e.parents('.CPTRow');
        var rowIndex = parseInt($row.index());
        var ID = e[0].id;
        if (e[0].value != null && e[0].value != "") {
            document.getElementsByName('CPTCodes' + '[' + rowIndex + ']' + '.IncludeLetter')[0].checked = true;
            document.getElementsByName('CPTCodes' + '[' + rowIndex + ']' + '.IncludeLetter')[0].disabled = false;
        }
        else if (($('#CPTCodes_' + (rowIndex) + '__CPTDesc')[0].value != null) && ($('#CPTCodes_' + (rowIndex) + '__CPTDesc')[0].value != "") && ($('#CPTCodes_' + (rowIndex) + '__Discipline')[0].value != null) && ($('#CPTCodes_' + (rowIndex) + '__Discipline')[0].value != "") && ($('#CPTCodes_' + (rowIndex) + '__Discipline')[0].value != "Select")) {
            document.getElementsByName('CPTCodes' + '[' + rowIndex + ']' + '.IncludeLetter')[0].checked = true;
            document.getElementsByName('CPTCodes' + '[' + rowIndex + ']' + '.IncludeLetter')[0].disabled = false;
        }
        else {
            document.getElementsByName('CPTCodes' + '[' + rowIndex + ']' + '.IncludeLetter')[0].checked = false;
            document.getElementsByName('CPTCodes' + '[' + rowIndex + ']' + '.IncludeLetter')[0].disabled = true;
        }
        CheckForDisability_PlainLanguageBtn();
    }
    // ----ends------
    // -----on change blur click and key ups----------
    $('.CPTAreaPos12').off('keyup', '.cptDescriptions').on('keyup', '.cptDescriptions', function () {
        CheckForIncLetter_CPTDesc($(this));
    })
    $('.CPTAreaPos12').off('click', '.cptDescriptions').on('click', '.cptDescriptions', function () {
        CheckForIncLetter_CPTDesc($(this));
    })
    $('.CPTAreaPos12').off('blur', '.cptDescriptions').on('blur', '.cptDescriptions', function () {
        CheckForIncLetter_CPTDesc($(this));
    })
    $('.CPTAreaPos12').off('change', '.cptDescriptions').on('change', '.cptDescriptions', function () {
        CheckForIncLetter_CPTDesc($(this));
    })
    // check for inc letter functionality - disable and enable ---- 
    function CheckForIncLetter_CPTDesc(e) {
        var $row = e.parents('.CPTRow');
        var rowIndex = parseInt($row.index());
        var ID = e[0].id;
        if (e[0].value != null && e[0].value != "") {
            document.getElementsByName('CPTCodes' + '[' + rowIndex + ']' + '.IncludeLetter')[0].checked = true;
            document.getElementsByName('CPTCodes' + '[' + rowIndex + ']' + '.IncludeLetter')[0].disabled = false;
        }
        else if (($('#CPTCodes_' + (rowIndex) + '__CPTCode')[0].value != null) && ($('#CPTCodes_' + (rowIndex) + '__CPTCode')[0].value != "") && ($('#CPTCodes_' + (rowIndex) + '__Discipline')[0].value != null) && ($('#CPTCodes_' + (rowIndex) + '__Discipline')[0].value != "") && ($('#CPTCodes_' + (rowIndex) + '__Discipline')[0].value != "Select")) {
            document.getElementsByName('CPTCodes' + '[' + rowIndex + ']' + '.IncludeLetter')[0].checked = true;
            document.getElementsByName('CPTCodes' + '[' + rowIndex + ']' + '.IncludeLetter')[0].disabled = false;
        }
        else {
            document.getElementsByName('CPTCodes' + '[' + rowIndex + ']' + '.IncludeLetter')[0].checked = false;
            document.getElementsByName('CPTCodes' + '[' + rowIndex + ']' + '.IncludeLetter')[0].disabled = true;
        }
        CheckForDisability_PlainLanguageBtn();
    }
    // ---- ends POS *****12***** TYPES-------
    $('.CPTAreaPos12').off('change', '.cptUnits').on('change', '.cptUnits', function () {
        index = $(this)[0].parentElement.parentElement.id;
        var $form = $("#UM_auth_form");
        var formData = new FormData($form[0]);
        $.ajax({
            type: 'POST',
            url: '/UM/Authorization/CalculatePlainlanguage?index=' + index,
            data: formData,
            processData: false,
            contentType: false,
            cache: false,
            success: function (data) {
                //console.log("total is :", data);
                $('#CPTCodes_' + (index) + '__TotalUnits')[0].value = data.toString();
            }
        })
    })


    function getSearchCodeValues(val, ob) {
        if ((typeof val != 'undefined') && (val != "")) {
            hideAllSearchCumDropDown();
            var icdCodeSearchCumDropDownWorker = new Worker('/Areas/UM/WebWorkers/AjaxWebWorker.js');
            icdCodeSearchCumDropDownWorker.postMessage({ url: '/UM/Authorization/IcdCodeData', singleParam: false, searchTerm: { version: $("input[name=ICDVersion]:checked").val(), Code: val, limit: 25 } });
            icdCodeSearchCumDropDownWorker.addEventListener('message', function (e) {
                if (e.data) {
                    ResultData = JSON.parse(e.data);
                    icdCodeSearchCumDropDownWorker.terminate();
                    ResultData = ResultData.data;
                    if (ResultData != null && ResultData.length > 0) {
                        var liData = "";
                        emptyExistElement($(ob).parent().find('ul'));
                        generateList(ResultData, $(ob)[0].id);
                    }
                    else {
                        emptyExistElement($(ob).parent().find('ul'));
                    }
                }
            });
        }
    }

    function getSearchDescriptionValues(val, ob) {
        if ((typeof val != 'undefined') && (val != "")) {
            hideAllSearchCumDropDown();
            var icdDescriptionSearchCumDropDownWorker = new Worker('/Areas/UM/WebWorkers/AjaxWebWorker.js');
            icdDescriptionSearchCumDropDownWorker.postMessage({ url: '/UM/Authorization/IcdDescriptionData', singleParam: false, searchTerm: { version: $("input[name=ICDVersion]:checked").val(), Desc: val, limit: 25 } });
            icdDescriptionSearchCumDropDownWorker.addEventListener('message', function (e) {
                if (e.data) {
                    ResultData = JSON.parse(e.data);
                    icdDescriptionSearchCumDropDownWorker.terminate();
                    ResultData = ResultData.data;
                    if (ResultData != null && ResultData.length > 0) {
                        var liData = "";
                        emptyExistElement($(ob).parent().find('ul'));
                        generateList(ResultData, $(ob)[0].id);
                    }
                    else {
                        emptyExistElement($(ob).parent().find('ul'));
                    }
                }
            });
        }
    }

    function emptyExistElement(ele) {
        $(ele).each(function (e) {

            $(this).remove();
        })
    }

    $('#ICDArea').off('click', '.DropDownListLi').on('click', '.DropDownListLi', function () {
        //$("#" + icdCodevar).html();
        //$("#" + icdDesVar).html();
        $("#" + icdCodevar).val($("#icdDataCode" + $(this).closest('li')[0].id).html());
        $("#" + icdDesVar).val($("#icdDataDescription" + $(this).closest('li')[0].id).html());
        $('.UMDropDownICDList').html('');
        $('.UMDropDownICDList').hide();
    });
    function generateList(eleData, InputId) {
        var liData = "";
        for (i in eleData) {
            liData = liData + "<li class='DropDownListLi' id='" + i + "' eleindex=" + i + "><span id='icdDataCode" + i + "'>" + eleData[i].ICDCode + "</span>-<span class='NPIcss' id='icdDataDescription" + i + "'>" + eleData[i].ICDCodeDescription + "</span></li>";
        }
        var element = "<ul class='UMDropDownList UMDropDownICDList dropdown-menu' id='" + InputId + "'><li>" + liData + "</li></ul>";
        $('#' + InputId).parent().last().after().append(element);
        SetCssForDropDown(InputId);//setting Css For UMDropDownICDList 
        $('#display').css("display", "block");
    }
    function SetCssForDropDown(CssID) {
        var top = ($('#' + CssID).position().top + 7 + $('#' + CssID)[0].offsetHeight) + "px";
        var left = $('#' + CssID).position().left + "px";
        $('.UMDropDownICDList').css({ 'top': top, 'left': left });

    }

    //-----------END---------------------------------//
    //------------cpt section-------START----
    var cptCodevar = "";
    var cptDesVar = "";
    $('#CPTArea').off('keyup', '.cptCode_search_cum_dropdown').on('keyup', '.cptCode_search_cum_dropdown', function () {
        $(this).attr('autocomplete', 'off');
        cptCodevar = this.id;
        cptCodevar = "CPTCodes_" + this.id.split("_")[1] + "__CPTCode";
        cptDesVar = "CPTCodes_" + this.id.split("_")[1] + "__CPTDesc";
        getSearchValuesForCPT($(this).val(), this);//getting result for CPT's
    });

    $('#CPTArea').off('keyup', '.cptDescription_search_cum_dropdown').on('keyup', '.cptDescription_search_cum_dropdown', function () {
        $(this).attr('autocomplete', 'off');
        cptCodevar = this.id;
        cptCodevar = "CPTCodes_" + this.id.split("_")[1] + "__CPTCode";
        cptDesVar = "CPTCodes_" + this.id.split("_")[1] + "__CPTDesc";
        getSearchValuesForCPTDesc($(this).val(), this);//getting result for provider
    });

    function getSearchValuesForCPT(val, ob) {
        if ((typeof val != 'undefined') && (val != "")) {
            hideAllSearchCumDropDown();
            var cptCodeSearchCumDropDownWorker = new Worker('/Areas/UM/WebWorkers/AjaxWebWorker.js');
            cptCodeSearchCumDropDownWorker.postMessage({ url: '/UM/Authorization/CptCodeData', singleParam: false, searchTerm: { Code: val, limit: 25 } });
            cptCodeSearchCumDropDownWorker.addEventListener('message', function (e) {
                if (e.data) {
                    ResultData = JSON.parse(e.data);
                    cptCodeSearchCumDropDownWorker.terminate();
                    ResultData = ResultData.data;
                    if (ResultData != null && ResultData.length > 0) {
                        var liData = "";
                        emptyExistElement($(ob).parent().find('ul'));
                        generateListForCPTs(ResultData, $(ob)[0].id);
                    }
                    else {
                        emptyExistElement($(ob).parent().find('ul'));
                    }
                }
            });
        }
    }

    function getSearchValuesForCPTDesc(val, ob) {
        if ((typeof val != 'undefined') && (val != "")) {
            hideAllSearchCumDropDown();
            var cptDescSearchCumDropDownWorker = new Worker('/Areas/UM/WebWorkers/AjaxWebWorker.js');
            cptDescSearchCumDropDownWorker.postMessage({ url: '/UM/Authorization/CptDescData', singleParam: false, searchTerm: { Code: val, limit: 25 } });
            cptDescSearchCumDropDownWorker.addEventListener('message', function (e) {
                if (e.data) {
                    ResultData = JSON.parse(e.data);
                    cptDescSearchCumDropDownWorker.terminate();
                    ResultData = ResultData.data;
                    if (ResultData != null && ResultData.length > 0) {
                        var liData = "";
                        emptyExistElement($(ob).parent().find('ul'));
                        generateListForCPTs(ResultData, $(ob)[0].id);
                    }
                    else {
                        emptyExistElement($(ob).parent().find('ul'));
                    }
                }
            });
        }
    }

    $('#CPTArea').off('click', '.DropDownListLi').on('click', '.DropDownListLi', function () {
        $("#" + cptCodevar).html();
        $("#" + cptCodevar).val($("#cptDataCode" + $(this).closest('li')[0].id).html());
        $("#" + cptDesVar).val($("#cptDataDescription" + $(this).closest('li')[0].id).html());
        $('.UMDropDownCPTList').html('');
        $('.UMDropDownCPTList').hide();
    });

    function generateListForCPTs(eleData, InputId) {
        var liData = "";
        for (i in eleData) {
            liData = liData + "<li class='DropDownListLi' id='" + i + "' eleindex=" + i + "><span id='cptDataCode" + i + "'>" + eleData[i].CPTCode + "</span>-<span class='NPIcss' id='cptDataDescription" + i + "'>" + eleData[i].CPTDesc + "</span></li>";
        }
        var element = "<ul class='UMDropDownList UMDropDownCPTList dropdown-menu' id='" + InputId + "'><li>" + liData + "</li></ul>";
        $('#' + InputId).parent().last().after().append(element);
        SetCssForDropDownForCPT(InputId);//setting Css For UMDropDownICDList 
        $('#display').css("display", "block");
    }

    function SetCssForDropDownForCPT(CssID) {
        var top = ($('#' + CssID).position().top + 7 + $('#' + CssID)[0].offsetHeight) + "px";
        var left = $('#' + CssID).position().left + "px";
        $('.UMDropDownCPTList').css({ 'top': top, 'left': left });
    }
})

//------------cpt section-----END------
setTimeout(function () {
    authSummaryTable();
}, 500);


$('#CPTArea').off("click", "#plainLanguageBtn").on("click", "#plainLanguageBtn", function () {
    TabManager.openFloatingModal('', '', '~/Areas/UM/Views/Common/Modal/_GetAddCPTRangeModal.cshtml', 'Plain Language', '', '', '');
});

/* ATTACHMENTS MANAGEMENT*/
$('#document_section').off('click', '.add_document_btn').on('click', '.add_document_btn', function () {
    var $row = $(this).parents('.doc_section');

    var childCount = parseInt($row.parent().children().length);

    var rowIndex = parseInt($row.index());


    $(this).parents('.doc_section').children('div:last').children('button:first').addClass('hidden');
    $(this).addClass('hidden');
    $('.delete_document_btn').removeClass("hidden");

    var doc_Template = '<div class="x_content doc_section zero-padding-left-right"  style="padding-bottom: 0;">' +
              '<div class="col-lg-3 col-md-3 col-sm-4 col-xs-2 zero-padding-left-right">' +
               '<select class="form-control input-xs mandatory_field_halo  loser_field documentName"name="Attachments[' + (rowIndex) + '].Name">' +
               '<option value="">Select</option>' +
               '</select>' +
              '</div>' +
              '<div class="col-lg-2 col-md-2 col-sm-3 col-xs-2">' +
              '<select class="form-control input-xs mandatory_field_halo loser_field documentTypeName" id="AttachmentType" name="Attachments[' + (rowIndex) + '].AttachmentTypeName">' +
              '<option value="">SELECT</option>' +
              '<option value="Clinical">CLINICAL</option>' +
            '<option value="Progress Notes">PROGRESS NOTES</option>' +
            '<option value="Fax">FAX</option>' +
                '<option value="LOA">LOA</option>' +
            '<option value="Plan Authorization">PLAN AUTHORIZATION</option>' +
                '</select>' +
              '</div>' +
              '<div class="col-lg-4 col-md-4 col-sm-3 col-xs-2">' +
              '<input class="form-control custom-file-input input-xs" type="file" name="Attachments[' + (rowIndex) + '].DocumentFile">' +
              '<div class="noFile">NO FILE CHOSEN</div>' +
              '</div>' +
              '<div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">' +
              '<button id="previewButton" class="btn btn-ghost btn-xs this_button_preview" data-toggle="tooltip" tabindex="-1" type="button"><img src="/Resources/Images/Icons/previewIcon.png" style="width: 28px;" /></button>' +
              '</div>' +
              '<div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">' +
              '<input type="checkbox" class="checkbox-radio includeFax" id="IncludeFax[' + (rowIndex) + ']" tabindex="-1" name="Attachments[' + (rowIndex) + '].IncludeFax" value="IncFax"><label class="includeFaxLabel" for="IncludeFax[' + (rowIndex) + ']"><span></span></label>' +
              '<input name="IncludeFax" type="hidden" value="false">' +
              '</div>' +
              '<div class="col-lg-1 col-md-1 col-sm-1 col-xs-1 pull-left">' +
              '<button class="btn btn-danger btn-xs delete_document_btn1 hidden" type="button" tabindex="-1"><i class="fa fa-minus"></i></button>' +
              '<button class="btn btn-danger btn-xs delete_document_btn" tabindex="-1" type="button"><i class="fa fa-minus"></i></button>' +
              '<button class="btn btn-success btn-xs add_document_btn loser_field" type="button"><i class="fa fa-plus"></i></button>' +
              '</div>' +
              '</div>';
    GetMasterDataDocumentNames();

    if (childCount == rowIndex) {
        $row.parent().append(doc_Template);
    }
    else {
        $(doc_Template).insertAfter($row);
    }

}).off('click', '.delete_document_btn').on('click', '.delete_document_btn', function () {


    var $row = $(this).parents('.doc_section');
    var childCount = parseInt($row.parent().children().length);
    var rowIndex = parseInt($row.index());
    if (childCount == 3 && $('.custom-file-input').val() != "") {
        if (rowIndex == 1) {
            $(this).parents('.doc_section').prev().children('div:last').children('button:first').removeClass('hidden');
            $('.delete_document_btn1').removeClass("hidden");
        }
        else {
            $(this).parents('.doc_section').prev().children('div:last').children('button:first').removeClass('hidden');
        }
    }
    if (childCount > 3) {
        if (childCount == (rowIndex + 1)) {
            $(this).parents('.doc_section').prev().children('div:last').children('button:last').removeClass('hidden');
            $(this).parents('.doc_section').remove();
        }
        else {
            $(this).parents('.doc_section').remove();
        }
    }
    else {
        $(this).parents('.doc_section').remove();
        $('.delete_document_btn').addClass("hidden");
        $('.add_document_btn').removeClass("hidden");
    }
    updateDocumentMapping();
}).off('click', '.delete_document_btn1').on('click', '.delete_document_btn1', function () {
    var $row = $(this).parents('.doc_section');
    var childCount = parseInt($row.parent().children().length);
    var rowIndex = parseInt($row.index());
    $('.add_document_btn').trigger('click');
    $(this).parents('.doc_section').remove();
    $('.delete_document_btn').addClass("hidden");
    $('.add_document_btn').removeClass("hidden");
    updateDocumentMapping();
}).off('click', '.this_button_preview').on('click', '.this_button_preview', function () {
    var $row = $(this).parents('#document_section');
    var $row_attach = $(this).parents('.doc_section');
    var rowIndex = parseInt($row_attach.index());
    var $this = $row.find('.custom-file-input')[rowIndex - 1];
    readURL($this);
});

/* Document Index for name, dynamically appended using this function  when we click on delete */
function updateDocumentMapping() {
    var $DocName = $('.documentName');
    var $DocTypeName = $('.documentTypeName');
    var $IncludeFax = $('.includeFax');
    var $IncludeFaxLabel = $('.includeFaxLabel');
    var $DocPath = $('.custom-file-input')
    if ($DocName.length > 0) {
        for (var p = 0; p < $DocName.length; p++) {
            $DocName[p].name = 'Attachments[' + p + '].Name';
            $DocTypeName[p].name = 'Attachments[' + p + '].AttachmentTypeName';
            $IncludeFax[p].name = 'Attachments[' + p + '].IncludeFax';
            $DocPath[p].name = 'Attachments[' + p + '].DocumentFile';
            $IncludeFax[p].id = 'IncludeFax[' + p + ']';
            $IncludeFaxLabel[p].attributes[1].value = 'IncludeFax[' + p + ']';

        }
    }
};

function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            window.open(e.target.result, '', 'width=' + screen.width + ',height=' + screen.height);
        }
        reader.readAsDataURL(input.files[0]);
    }
}

$(function () {

    InitICheckFinal();
    /*------------------------------------------------------------------------------------*/
    $('#QueueName').show().text('Intake');

    $('.noFile').show();

    $('#document_section').off('change', '.custom-file-input').on('change', '.custom-file-input', function () {
        var $row = $(this).parents('#document_section');
        var $row_attach = $(this).parents('.doc_section');
        var childCount = parseInt($row_attach.parent().children().length);
        if (childCount == 2) {
            $(this).parents('.doc_section').children('div:last').children('button:first').removeClass('hidden');
        }
        var rowIndex = parseInt($row_attach.index());
        var $this = $row.find('.custom-file-input')[rowIndex - 1];
        var fileName = $this.value;
        if (fileName == "") {
            return;
        }
        else {
            var aFile = fileName.split('fakepath')[1];
            var bFile = aFile.substring(aFile.indexOf(aFile) + 1);
            var file_text = bFile.toUpperCase();
            if ($(window).width() > 1800) {
                $(this).parents('.doc_section').find('.noFile').text(file_text.substr(0, 60));
                //  $(this).parents('.doc_section').find('.custom-file-input').prop('disabled', true);
            }
            else {
                $(this).parents('.doc_section').find('.noFile').text(file_text.substr(0, 36));
                // $(this).parents('.doc_section').find('.custom-file-input').prop('disabled', true);
            }
        }
    });
});

// --------Function used to get the data for document name and placing it in the search drop down-------

GetMasterDataDocumentNames();
//var DocNames = ["Hospital Records", "Specialist Records", "Office Facesheet", "Plan Authorization"];

function GetMasterDataDocumentNames() {
    var DocNames = [];
    $.ajax({
        type: 'GET',
        url: '/UM/Authorization/GetDocumentNames',
        error: function () {
        },
        success: function (info) {
            if (info.data.length > 0) {
                for (var index = 0; index < info.data.length; index++) {
                    DocNames.push(info.data[index].DocumentNameValue);
                }
            }
            $(".documentName").select2({
                data: DocNames
            });
        }
    });
}
/*CHECK TO OPEN Doc Area to Upload file*/
$('#UM_auth_form').off('click', '#AddNewDocCheck').on('click', '#AddNewDocCheck', function () {
    //$("#AddNewDocCheck").on("click", function () {
    if ($(this).is(':checked')) {
        if ($('#document_section').hasClass('hidden')) {
            $('#document_section').removeClass('hidden');
            var doc_Template = '<div class="x_content doc_section zero-padding-left-right"  style="padding-bottom: 0;">' +
          '<div class="col-lg-3 col-md-3 col-sm-4 col-xs-2 zero-padding-left-right">' +
           '<select class="form-control input-xs mandatory_field_halo  loser_field documentName"name="Attachments[0].Name">' +
           '<option value="">Select</option>' +
           '</select>' +

          '</div>' +
          '<div class="col-lg-2 col-md-2 col-sm-3 col-xs-2">' +
          '<select class="form-control input-xs mandatory_field_halo loser_field documentTypeName" id="AttachmentType" name="Attachments[0].AttachmentTypeName">' +
          '<option value="">SELECT</option>' +
          '<option value="Clinical">CLINICAL</option>' +
        '<option value="Progress Notes">PROGRESS NOTES</option>' +
        '<option value="Fax">FAX</option>' +
            '<option value="LOA">LOA</option>' +
        '<option value="Plan Authorization">PLAN AUTHORIZATION</option>' +
            '</select>' +
          '</div>' +
          '<div class="col-lg-4 col-md-4 col-sm-3 col-xs-2">' +
          '<input class="form-control custom-file-input input-xs" type="file" name="Attachments[0].DocumentFile">' +
          '<div class="noFile">NO FILE CHOSEN</div>' +
          '</div>' +
          '<div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">' +
          '<button id="previewButton" class="btn btn-ghost btn-xs this_button_preview" data-toggle="tooltip" tabindex="-1" type="button"><img src="/Resources/Images/Icons/previewIcon.png" style="width: 28px;" /></button>' +
          '</div>' +
          '<div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">' +
          '<input type="checkbox" class="checkbox-radio includeFax" id="IncludeFax[0]" tabindex="-1" name="Attachments[0].IncludeFax" value="IncFax"><label class="includeFaxLabel" for="IncludeFax[0]"><span></span></label>' +
          '<input name="IncludeFax" type="hidden" value="false">' +
          '</div>' +
          '<div class="col-lg-1 col-md-1 col-sm-1 col-xs-1 pull-left">' +
           '<button class="btn btn-danger btn-xs delete_document_btn1 hidden" tabindex="-1" type="button"><i class="fa fa-minus"></i></button>' +
            '<button class="btn btn-danger btn-xs delete_document_btn hidden " tabindex="-1" type="button"><i class="fa fa-minus"></i></button>' +
            '<button class="btn btn-success btn-xs add_document_btn loser_field" type="button"><i class="fa fa-plus"></i></button>' +
          '</div>' +
          '</div>';
            GetMasterDataDocumentNames();
            $(doc_Template).insertAfter("#documentLabels");
        }
    } else {
        $('#document_section').addClass('hidden');
        $('.doc_section').remove();
    }
});
//-----------------------------------------------------------------------------------------------------------//

/* /ATTACHMENTS MANAGEMNENT*/
