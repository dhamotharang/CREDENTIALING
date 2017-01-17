var cptCodevar = "";
var cptDesVar = "";
$('.addCptRangeMainContainer').off('keyup', '.cptRangeCode_dropdown').on('keyup', '.cptRangeCode_dropdown', function () {
    $(this).attr('autocomplete', 'off');
    cptCodevar = this.id;
    cptDesVar = this.id + "Desc";
    getSearchValuesForCPTRange($(this).val(), this);
});

function getSearchValuesForCPTRange(val, ob) {
    if ((typeof val != 'undefined') && (val != "")) {
        var SearchValuesForCPTRangeWorker = new Worker('/Areas/UM/WebWorkers/AjaxWebWorker.js');
        SearchValuesForCPTRangeWorker.postMessage({ url: '/UM/Authorization/CptCodeData', singleParam: false, searchTerm: { Code: val, limit: 25 } });
        SearchValuesForCPTRangeWorker.addEventListener('message', function (e) {
            if (e.data) {
                ResultData = JSON.parse(e.data);
                SearchValuesForCPTRangeWorker.terminate();
                ResultData = ResultData.data;
                var liData = "";
                emptyExistRangeElement($(ob).parent().find('ul'));
                generateListForCPTRange(ResultData, $(ob)[0].id);

            }
            else {
                emptyExistRangeElement($(ob).parent().find('ul'));
            }

            if ($("#" + ob.id).val().length > 0) {
                $(".searchCptRangeBtn").prop("disabled", false);
            } else {
                $(".searchCptRangeBtn").prop("disabled", true);
            }
        })
    }
};

$('.addCptRangeMainContainer').off('click', '.DropDownListLi').on('click', '.DropDownListLi', function () {
    $("#" + cptCodevar).html();
    $("#" + cptCodevar).val($("#cptRangeDataCode" + $(this).closest('li')[0].id).html());
    $("#" + cptDesVar).val($("#cptRangeDataDescription" + $(this).closest('li')[0].id).html());
    $('.UMDropDownCPTList').html('');
    $('.UMDropDownCPTList').hide();
});

function generateListForCPTRange(eleData, InputId) {
    var liData = "";
    for (i in eleData) {
        liData = liData + "<li class='DropDownListLi' id='" + i + "' eleindex=" + i + "><span id='cptRangeDataCode" + i + "'>" + eleData[i].CPTCode + "</span>-<span class='NPIcss' id='cptRangeDataDescription" + i + "'>" + eleData[i].CPTDesc + "</span></li>";
    }
    var element = "<ul class='UMDropDownCPTList dropdown-menu' id='" + InputId + "'><li>" + liData + "</li></ul>";
    $('#' + InputId).parent().last().after().append(element);
    SetCssForDropDownForCPT(InputId);//setting Css For UMDropDownICDList 
    $('#display').css("display", "block");
}

function emptyExistRangeElement(ele) {
    $(ele).each(function (e) {
        $(this).remove();
    })
}

function SetCssForDropDownForCPT(CssID) {
    var top = ($('#' + CssID).position().top + 7 + $('#' + CssID)[0].offsetHeight) + "px";
    var left = $('#' + CssID).position().left + "px";
    $('.UMDropDownCPTList').css({ 'top': top, 'left': left });
}

var CPTToCode = "";
var CPTFromCode = "";
var RangeResultData = {};
$(".addCptRangeMainContainer").off("click", ".searchCptRangeBtn").on("click", ".searchCptRangeBtn", function () {
    CPTToCode = $("#cpt_Range_Code").val();
    CPTFromCode = $("#cpt_Range_Code1").val();
    var SwitchCode = "";
    if (CPTToCode < CPTFromCode) {
        SwitchCode = CPTFromCode;
        CPTFromCode = CPTToCode;
        CPTToCode = SwitchCode;
    }
    getCPTRangeData(CPTFromCode, CPTToCode);
});

var getCPTRangeData = function (FromCode, ToCode) {
    if ((typeof FromCode != 'undefined') && (FromCode != "") && (typeof ToCode != 'undefined') && (ToCode != "")) {
        var cptRangeDataWorker = new Worker('/Areas/UM/WebWorkers/AjaxWebWorker.js');
        cptRangeDataWorker.postMessage({ url: '/UM/Authorization/CptRangeData', singleParam: false, searchTerm: { FromCode: FromCode, ToCode: ToCode } });
        cptRangeDataWorker.addEventListener('message', function (e) {
            if (e.data) {
                RangeResultData = JSON.parse(e.data);
                cptRangeDataWorker.terminate();
                RangeResultData = RangeResultData.data;
                $(".cptrangetablebody").html(GenerateCPTCodesRangeTable(RangeResultData));
                $(".cptRangeTableDisplayDiv").removeClass('plnotvisible').addClass('plvisible');
            }
        });
    }
};


function GenerateCPTCodesRangeTable(SelectedRangeCPTCodes) {
    var tr = "";
    try {
        if (SelectedRangeCPTCodes.length > 0) {
            for (var i = 0; i < SelectedRangeCPTCodes.length; i++) {
                tr = tr + '<tr>' +
                      '<td><input type="checkbox" id="chkCptRange' + i + '" class="form-control input-xs cptRangeTableCheckBox normal-checkbox" style="position: absolute; opacity: 0;"><label for="chkCptRange"' + i + '"><span></span></label></td>' +
                      '<td>' + SelectedRangeCPTCodes[i].CPTCode + '</td>' +
                      '<td>' + SelectedRangeCPTCodes[i].CPTDesc + '</td>' +
                      '</tr>';
            }
            return tr;
        } else throw "CPT Codes Range Not Selected";
    } catch (err) {
        console.log(err);
    }
}

var SaveCPTRange = function () {
    var CPTRowTemplate = "";
    var innerIndex = 0;
    $("#CPTArea").html('');
    for (var rowIndex = 0; rowIndex < $(".cptrangetablebody").children().length; rowIndex++) {
        if ($(".cptrangetablebody").children()[rowIndex].children[0].children[0].checked) {
            $(".plain_language_btn").addClass('hidden');
            $(".smartIntelligenceBtn").addClass('hidden');
            $(".plusButton").addClass('hidden');
            if (($('#PlaceOfService').val() == '12- PATIENT HOME') || ($('#PlaceOfService').val() == '62- CORF')) {
                CPTRowTemplate = '<div class="CPTRow">' +
               '<div class="col-lg-1 theme_label_data zero-padding-left-right">' +
               '<input id="CPTCodes_' + innerIndex + '__CPTCode" name="CPTCodes[' + innerIndex + '].CPTCode" class="form-control cptCodes mandatory_field_halo cptCode_search_cum_dropdown input-xs summaryDataChange" value="' + RangeResultData[rowIndex].CPTCode + '" placeholder="CPT CODE"/>' +
               '</div>' +
               '<div class="col-lg-1 theme_label_data zero-padding-left-right">' +
               '<input class="form-control input-xs cptModifier" id="CPTCodes_' + innerIndex + '_Modifier" name="CPTCodes[' + innerIndex + '].Modifier" placeholder="MODIFIER" type="text">' +
               '</div>' +
                                         '<div class="col-lg-1 theme_label_data zero-padding-left-right">' +
                                         '<input class="form-control input-xs mandatory_field_halo cptDescription_search_cum_dropdown cptDescriptions summaryDataChange" id="CPTCodes_' + innerIndex + '___CPTDesc" name="CPTCodes[' + innerIndex + '].CPTDesc" value="' + RangeResultData[rowIndex].CPTDesc + '" placeholder="DESCRIPTION" type="text">' +
                                         '</div>' +

                                         '<div class="col-lg-1 theme_label_data zero-padding-left-right">' +
                                         '<select class="form-control input-xs cptDiscipline" id="CPTCodes_' + innerIndex + '_Disicipline" name="CPTCodes[' + innerIndex + '].Disicipline"  placeholder="REQ UNITS" type="text"><option value="">Select</option></select>' +
                                         '</div>' +

                                         '<div class="col-lg-1 theme_label_data cptrequnits_rightmargin zero-padding-left-right">' +
                                         '<input class="form-control input-xs cptReqUnits" id="CPTCodes_' + innerIndex + '_RequestedUnits" name="CPTCodes[' + innerIndex + '].RequestedUnits"  data-val="true" data-val-number="The field RequestedUnits must be a number." placeholder="REQ UNITS" type="text">' +
                                         '</div>' +

                                         '<div class="col-lg-1 theme_label_data  zero-padding-left-right">' +
                                         '<select class="form-control input-xs cptRange1" id="CPTCodes_' + innerIndex + '_Range1"  name="CPTCodes[' + innerIndex + '].Range1"  placeholder="Range 2" type="text"><option value="">Select</option></select>' +
                                         '</div>' +

                                         '<div class="col-lg-1 theme_label_data cptrequnits_rightmargin zero-padding-left-right">' +
                                         '<input class="form-control input-xs cptNoPer" id="CPTCodes_' + innerIndex + '_NumberPer" name="CPTCodes[' + innerIndex + '].NumberPer" placeholder="No Per" type="text">' +
                                         '</div>' +

                                         '<div class="col-lg-1 theme_label_data zero-padding-left-right">' +
                                         '<select class="form-control input-xs cptRange2" id="CPTCodes_' + innerIndex + '_Range2" name="CPTCodes[' + innerIndex + '].Range2"  placeholder="Range 2" type="text"><option value="">Select</option></select>' +
                                         '</div>' +

                                         '<div class="col-lg-1 theme_label_data cptrequnits_rightmargin zero-padding-left-right">' +
                                         '<input class="form-control input-xs cptTotalUnits summaryDataChange" id="CPTCodes_' + innerIndex + '__TotalUnits" name="CPTCodes[' + innerIndex + '].TotalUnits" placeholder="Total Units" type="text">' +
                                         '</div>' +

                                         '<div class="col-lg-1 col-md-1 col-sm-2 col-xs-1 pull-left button-styles-xs row-action theme_label_data cptButtonArea zero-padding-left-right"></div>' +
                                         '<div class="col-lg-1 text-center theme_label_data zero-padding-left-right">' +
                                         //'<input type="checkbox" class="checkbox-radio includeLetter" id="CPTCodes_' + (innerIndex) + '__IncludeLetter" tabindex="-1" name="CPTCodes[' + (innerIndex) + '].IncludeLetter" value="IncLetter" ><label class="" for="CPTCodes_' + (innerIndex) + '__IncludeLetter"><span></span></label>' +
                                         '<button class="btn btn-danger btn-xs smartIntelligenceBtn bold_text calypso_ai_btn" type="button"> CALYPSO AI</button>' +
                                         '</div>' +
                                         '<div class="clearfix"></div>' +
                                         '</div>';
            }
            else if (($('#PlaceOfService').val() == '21- IP HOSPITAL') || ($('#PlaceOfService').val() == '31- SNF')) {

                CPTRowTemplate = '<div class="CPTRow">' +
                          '<div class="col-lg-2 theme_label_data zero-padding-left-right">' +
                          '<input id="CPTCodes_' + innerIndex + '__CPTCode" name="CPTCodes[' + innerIndex + '].CPTCode" class="form-control input-xs cptCode_search_cum_dropdown cptCodes summaryDataChange" value="' + RangeResultData[rowIndex].CPTCode + '" placeholder="CPT CODE"/>' +
                          '</div>' +
                          '<div class="col-lg-1 theme_label_data zero-padding-left-right">' +
                          '<input class="form-control input-xs cptModifier" id="CPTCodes_' + innerIndex + '_Modifier" name="CPTCodes[' + innerIndex + '].Modifier" placeholder="MODIFIER" type="text">' +
                          '</div>' +
                          '<div class="col-lg-3 theme_label_data zero-padding-left-right">' +
                          '<input class="form-control input-xs cptDescription_search_cum_dropdown cptDescriptions summaryDataChange" id="CPTCodes_' + innerIndex + '__CPTDesc" name="CPTCodes[' + innerIndex + '].CPTDesc" value="' + RangeResultData[rowIndex].CPTDesc + '" placeholder="DESCRIPTION" type="text">' +
                          '<input class="form-control input-xs cptDiscipline" id="CPTCodes_' + innerIndex + '_Discipline" name="CPTCodes[' + innerIndex + '].Discipline" placeholder="DICIPLINE" type="text" hiddden style="Display:none">' +
                          '</div>' +
                          '<div class="col-lg-1 theme_label_data cptrequnits_rightmargin zero-padding-left-right">' +
                          '<input class="form-control input-xs cptReqUnits" id="CPTCodes_' + innerIndex + '_RequestedUnits" name="CPTCodes[' + innerIndex + '].RequestedUnits"  data-val="true" data-val-number="The field RequestedUnits must be a number." placeholder="REQ UNITS" type="text">' +
                          '</div>' +
                          '<div class="col-lg-2 col-md-2 col-sm-2 col-xs-1 pull-left button-styles-xs row-action theme_label_data cptButtonArea zero-padding-left-right"></div>' +
                          '<div class="col-lg-3 col-md-1 col-sm-2 col-xs-1 cptInc_pullleft zero-padding-left-right pull-left">' +
                          '<button class="btn btn-danger btn-xs smartIntelligenceBtn bold_text calypso_ai_btn" type="button"> CALYPSO AI</button>' +
                          '</div>' +
                          '<div class="clearfix"></div>' +
                          '</div>';
            }
            else {

                CPTRowTemplate = '<div class="CPTRow">' +
                          '<div class="col-lg-2 theme_label_data zero-padding-left-right">' +
                          '<input id="CPTCodes_' + innerIndex + '__CPTCode" name="CPTCodes[' + innerIndex + '].CPTCode" class="form-control mandatory_field_halo input-xs loser_field cptCode_search_cum_dropdown cptCodes summaryDataChange" value="' + RangeResultData[rowIndex].CPTCode + '" placeholder="CPT CODE"/>' +
                          '</div>' +
                          '<div class="col-lg-1 theme_label_data zero-padding-left-right">' +
                          '<input class="form-control input-xs cptModifier" id="CPTCodes_' + innerIndex + '_Modifier"  name="CPTCodes[' + innerIndex + '].Modifier" placeholder="MODIFIER" type="text">' +
                          '</div>' +
                          '<div class="col-lg-3 theme_label_data zero-padding-left-right">' +
                          '<input class="form-control input-xs mandatory_field_halo loser_field cptDescription_search_cum_dropdown cptDescriptions summaryDataChange" id="CPTCodes_' + innerIndex + '__CPTDesc" name="CPTCodes[' + innerIndex + '].CPTDesc" value="' + RangeResultData[rowIndex].CPTDesc + '" placeholder="DESCRIPTION" type="text">' +
                          '<input class="form-control input-xs cptDiscipline" id="CPTCodes_' + innerIndex + '_Discipline" name="CPTCodes[' + innerIndex + '].Discipline" placeholder="DICIPLINE" type="text" hiddden style="Display:none">' +
                          '</div>' +
                          '<div class="col-lg-1 theme_label_data cptrequnits_rightmargin zero-padding-left-right">' +
                          '<input class="form-control input-xs cptReqUnits" id="CPTCodes_' + innerIndex + '_RequestedUnits" name="CPTCodes[' + innerIndex + '].RequestedUnits"  data-val="true" data-val-number="The field RequestedUnits must be a number." placeholder="REQ UNITS" type="text">' +
                          '</div>' +
                          '<div class="col-lg-2 col-md-2 col-sm-2 col-xs-1 pull-left button-styles-xs row-action theme_label_data cptButtonArea zero-padding-left-right"></div>' +
                          '<div class="col-lg-3 col-md-1 col-sm-2 col-xs-1 cptInc_pullleft zero-padding-left-right pull-left">' +
                         // '<input type="checkbox" class="checkbox-radio includeLetter" id="CPTCodes_' + (innerIndex) + '__IncludeLetter" tabindex="-1" name="CPTCodes[' + (innerIndex) + '].IncludeLetter" value="IncLetter" ><label class="" for="CPTCodes_' + (innerIndex) + '__IncludeLetter"><span></span></label>' +
                          '<button class="btn btn-danger btn-xs smartIntelligenceBtn bold_text loser_field calypso_ai_btn" type="button"> CALYPSO AI</button>' +
                          '</div>' +
                          '<div class="clearfix"></div>' +
                          '</div>';
            }
            innerIndex++;
        }

        $("#CPTArea").append(CPTRowTemplate);
        CPTRowTemplate = "";
    }
    $("#SharedFloatingModal").modal('hide');
    authSummaryTable();//update auth summary table
    //$(".CPTArea").html(CPTRowTemplate);
    updateCPTMapping();
    showFish();
};

$('#cptRangeTableDiv').off('click', '.selectAllCPTRange').on('click', '.selectAllCPTRange', function () {
    for (var CPTVar = 0; CPTVar < $(".cptrangetablebody").children().length; CPTVar++) {
        $(".cptrangetablebody").children()[CPTVar].children[0].children[0].checked = true;
    }
    chkBoxCptRange();
});
$('#cptRangeTableDiv').off('click', '.UnSelectAllCPTRange').on('click', '.UnSelectAllCPTRange', function () {
    for (var CPTVar = 0; CPTVar < $(".cptrangetablebody").children().length; CPTVar++) {
        $(".cptrangetablebody").children()[CPTVar].children[0].children[0].checked = false;
    }
    chkBoxCptRange();
});

//--------saveCPTRangeButton
$('#cptRangeTableDiv').off('click', '.cptRangeTableCheckBox').on('click', '.cptRangeTableCheckBox', function () {
    chkBoxCptRange();
});
chkBoxCptRange = function () {
    for (var CPTVar = 0; CPTVar < $(".cptrangetablebody").children().length; CPTVar++) {
        if ($(".cptrangetablebody").children()[CPTVar].children[0].children[0].checked) {
            $(".saveCPTRangeButton").prop("disabled", false);
            break;
        } else {
            $(".saveCPTRangeButton").prop("disabled", true);
        }
    }
}