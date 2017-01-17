var isICDHistoryData = false;
var checkedIcdList = [];
var IcdCodeHistoryData = [];

if (!isICDHistoryData) {
    $.ajax({
        type: 'GET',
        url: '/Coding/Coding/GetICDCodeHistory',
        success: function (data) {
            $('#HistoryDiagnosisICD').html(data);
            isICDHistoryData = true;
        }
    });
}

$.ajax({
    type: 'GET',
    url: '/Coding/Coding/GetIcdHistoryData',
    success: function (data) {
        IcdCodeHistoryData = data.Icdcodes;
    }
});


/** 
@description This event triggers by clicking on the Active Diagnosis Button. It shows or hides the Active Diagnosis Codes Panel.
 */
$('.ICDCodesMainPanel').off('click', '.DiagnosisActiveHistory').on('click', '.DiagnosisActiveHistory', function () {
    $('#HistoryDiagnosisICD').toggle();
});


/** 
@description This event triggers by clicking on the close button of Active Diagnosis Codes Panel. It closes the Active Diagnosis Codes Panel.
 */
$('.ICDCodesMainPanel').off('click', '.closeDiagnosisHistory').on('click', '.closeDiagnosisHistory', function () {
    $('#HistoryDiagnosisICD').toggle();
});


/** 
@description This event triggers by selecting ICD Code from Active Diagnosis Codes. It binds the selected ICD Code information to the ICD Coding Table from the Active Diagnosis History 
 */
//$('#HistoryDiagnosisICD').off('click', '.icdCode').on('click', '.icdCode', function () {
//    if ($(this).is(':checked')) {
//        checkedIcdList.push(this.value)
//    } else {
//        removeItem = this.value;
//        checkedIcdList = jQuery.grep(checkedIcdList, function (value) {
//            return value != removeItem;
//        });
//    }
//    var tableBodyTag = "";
//    var trTag = '<tr>';
//    var trEndTag = '</tr>';
//    var tdTag = '<td>';
//    var tdEndTag = '</td>';
//    for (var i = 0; i < checkedIcdList.length; i++) {
//        for (var j = 0; j < IcdCodeHistoryData.length; j++) {
//            if (IcdCodeHistoryData[j].Code === checkedIcdList[i]) {
//                if (IcdCodeHistoryData[j].HCCCode.length > 1) {
//                    rowspanLength = IcdCodeHistoryData[j].HCCCode.length;
//                    var tdRowSpanTag = '<td rowspan="' + rowspanLength + '">'
//                    for (var innerHCC = 0; innerHCC < rowspanLength; innerHCC++) {
//                        if (innerHCC == 0) {
//                            tableBodyTag = tableBodyTag + trTag
//                         + tdRowSpanTag + '<input class="form-control input-xs text-uppercase" placeholder="" type="text" value="' + IcdCodeHistoryData[j].Code + '">' + tdEndTag
//                         + tdRowSpanTag + '<input class="form-control input-xs text-uppercase" placeholder="" type="text" value="' + IcdCodeHistoryData[j].Description + '">' + tdEndTag
//                         + tdTag + '<input class="form-control input-xs text-uppercase" placeholder="" type="text" value="' + IcdCodeHistoryData[j].HCCCode[0] + '">' + tdEndTag
//                         + tdTag + '<input class="form-control input-xs text-uppercase" placeholder="" type="text" value="' + IcdCodeHistoryData[j].HCCType[0] + '">' + tdEndTag
//                         + tdTag + '<input class="form-control input-xs text-uppercase" placeholder="" type="text" value="' + IcdCodeHistoryData[j].HCCVersion[0] + '">' + tdEndTag
//                         + tdTag + '<input class="form-control input-xs text-uppercase" placeholder="" type="text" value="' + IcdCodeHistoryData[j].HCCDescription[0] + '">' + tdEndTag
//                         + tdTag + '<input class="form-control input-xs text-uppercase" placeholder="" type="text" value="' + IcdCodeHistoryData[j].HCCWeight[0] + '">' + tdEndTag
//                         + '<td onclick="RemoveThisRowAndNext(this)" rowspan="' + rowspanLength + '"><button class="btn btn-xs btn-danger"><i class="fa fa fa-minus"></i></button></td>'
//                         + trEndTag;
//                        } else {
//                            tableBodyTag = tableBodyTag + trTag
//                         + tdTag + '<input class="form-control input-xs text-uppercase" placeholder="" type="text" value="' + IcdCodeHistoryData[j].HCCCode[innerHCC] + '">' + tdEndTag
//                         + tdTag + '<input class="form-control input-xs text-uppercase" placeholder="" type="text" value="' + IcdCodeHistoryData[j].HCCType[0] + '">' + tdEndTag
//                         + tdTag + '<input class="form-control input-xs text-uppercase" placeholder="" type="text" value="' + IcdCodeHistoryData[j].HCCVersion[innerHCC] + '">' + tdEndTag
//                         + tdTag + '<input class="form-control input-xs text-uppercase" placeholder="" type="text" value="' + IcdCodeHistoryData[j].HCCDescription[innerHCC] + '">' + tdEndTag
//                         + tdTag + '<input class="form-control input-xs text-uppercase" placeholder="" type="text" value="' + IcdCodeHistoryData[j].HCCWeight[innerHCC] + '">' + tdEndTag
//                         + trEndTag;
//                        }
//                    }
//                } else {
//                    tableBodyTag = tableBodyTag + trTag
//                            + tdTag + '<input class="form-control input-xs text-uppercase" placeholder="" type="text" value="' + IcdCodeHistoryData[j].Code + '">' + tdEndTag
//                            + tdTag + '<input class="form-control input-xs text-uppercase" placeholder="" type="text" value="' + IcdCodeHistoryData[j].Description + '">' + tdEndTag
//                            + tdTag + '<input class="form-control input-xs text-uppercase" placeholder="" type="text" value="' + IcdCodeHistoryData[j].HCCCode[0] + '">' + tdEndTag
//                            + tdTag + '<input class="form-control input-xs text-uppercase" placeholder="" type="text" value="' + IcdCodeHistoryData[j].HCCType[0] + '">' + tdEndTag
//                            + tdTag + '<input class="form-control input-xs text-uppercase" placeholder="" type="text" value="' + IcdCodeHistoryData[j].HCCVersion[0] + '">' + tdEndTag
//                            + tdTag + '<input class="form-control input-xs text-uppercase" placeholder="" type="text" value="' + IcdCodeHistoryData[j].HCCDescription[0] + '">' + tdEndTag
//                            + tdTag + '<input class="form-control input-xs text-uppercase" placeholder="" type="text" value="' + IcdCodeHistoryData[j].HCCWeight[0] + '">' + tdEndTag
//                            + '<td onclick="RemoveThisRow(this)"><button class="btn btn-xs btn-danger"><i class="fa fa fa-minus"></i></button></td>'
//                            + trEndTag;
//                }
//            }
//        }
//    }
//    $('#icdCodeBody').html(tableBodyTag)
//})


/** 
@description this event triggers by clicking on add. It will append a new row to the table body
 */
var templateIndex = 0;
$('.ICDCodesMainPanel').off('click', '#AddNewRowInICDTable').on('click', '#AddNewRowInICDTable', function (e) {
    e.preventDefault();
    templateIndex++;
    var templateHtml = '<tr>'+
                        '<td>'+
                         '<input class="form-control input-xs non_mandatory_field_halo uppercase" id="ICDCodes_' + templateIndex + '__Code" name="ICDCodes[' + templateIndex + '].Code" type="text" value="">'+
                         '<span class="field-validation-valid" data-valmsg-for="ICDCodes[' + templateIndex + '].Code" data-valmsg-replace="true"></span>'+
                        '</td>'+
                        '<td>'+
                         '<input class="form-control input-xs non_mandatory_field_halo uppercase" id="ICDCodes_' + templateIndex + '__Description" name="ICDCodes[' + templateIndex + '].Description" type="text" value="">'+
                         '<span class="field-validation-valid" data-valmsg-for="ICDCodes[' + templateIndex + '].Description" data-valmsg-replace="true"></span>' +
                         '</td>' +
                        '<td>\
                            <input class="form-control input-xs non_mandatory_field_halo uppercase" id="Hcccode_Code" name="Hcccode.Code" type="text" value="">\
                        </td>\
                        <td>\
                            <input class="form-control input-xs non_mandatory_field_halo uppercase" id="Hcccode_Type" name="Hcccode.Type" type="text" value="">\
                        </td>\
                        <td>\
                            <input class="form-control input-xs non_mandatory_field_halo uppercase" id="Hcccode_Version"  name="Hcccode.Version" type="text" value="">\
                        </td>\
                        <td>\
                            <input class="form-control input-xs non_mandatory_field_halo uppercase" id="Hcccode_Description" name="Hcccode.Description" type="text" value="">\
                        </td>\
                        <td>\
                            <input class="form-control input-xs non_mandatory_field_halo uppercase" id="Hcccode_Weight" name="Hcccode.Weight" type="text" value="">\
                        </td>\
                        <td onclick="RemoveRows(this)"><button class="btn btn-xs btn-danger"><i class="fa fa-close"></i></button></td>\
                    </tr>';
    $('#icdCodeBody').append(templateHtml);
});

/** 
@description Removes the row from the table 
* @param {string} ele current element
 */
function RemoveICDCodes(ele) {
    var tbody = $(ele).parent().parent();
    var trIndex = findRemovedElementIndex(ele);
    $(ele).parent("tr").remove();
    tbody.children().each(function (index) {
        if (index >= trIndex) {
            var inputs = $(this).find('input');
            inputs.each(function () {
                var id = $(this).attr("id");
                var newId = id.replace(/[0-9]+/, index);
                var name = $(this).attr("name");
                var newName = name.replace(/[0-9]+/, index);
                $(this).attr("id", newId);
                $(this).attr("name",newName);
              
            });
            var spans = $(this).find('span');
            spans.each(function () {
                var msg = $(this).attr("data-valmsg-for");
                var newmsg = msg.replace(/[0-9]+/, index);
                $(this).attr("data-valmsg-for",newmsg);
               
            });
        }
    });

    
}
function findRemovedElementIndex(ele) {
    var tr = $(ele).parent()[0];
    var trIndex;
    $(ele).parent().parent().children().each(function (index) {
        if (this == tr) {
            trIndex = index;
        }

    });
    return trIndex;
}

$('span[data-toggle="tooltip"]').tooltip();