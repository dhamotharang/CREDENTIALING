var isICDHistoryData = false;
var checkedIcdList = [];
var IcdCodeHistoryData = [];


/** 
@description TO GET ICD HISTORY INFORMATION.
 */
$('.EditCodingMainPanel').off('click', '.DiagnosisActiveHistory_Edit').on('click', '.DiagnosisActiveHistory_Edit', function () {
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
    $('#HistoryDiagnosisICD').toggle();
});


/** 
@description This event triggers by clicking on the close button of Active Diagnosis Codes Panel. It closes the Active Diagnosis Codes Panel.
 */
$('.EditCodingMainPanel').off('click', '.closeDiagnosisHistory').on('click', '.closeDiagnosisHistory', function () {
    $('#HistoryDiagnosisICD').toggle();
});


/** 
@description TO GET ONLY ICD CODES FROM HISTORY.
 */
$.ajax({
    type: 'GET',
    url: '/Coding/Coding/GetIcdHistoryData',
    success: function (data) {
        IcdCodeHistoryData = data.Icdcodes;
    }
});



$('.ICDCodeTable').off('click', '#AddNewIcdrow_Edit').on('click', '#AddNewIcdrow_Edit', function (e) {
    e.preventDefault();
    
    var tbody = $("form#savedetailsForm").find('#ICDCodetbody_Edit');
    var lasttr = tbody[0].lastElementChild;
    var prevIndex = parseInt($(lasttr).find('input').attr('id').match(/\d+/)[0]);
    var templateIndex = ++prevIndex;
    var newICDTr;
    $.ajax({
        type: 'GET',
        url: '/Coding/Coding/CreateICD?index=' + templateIndex,
        success: function (data) {
            newICDTr = data;
            $('#ICDCodetbody_Edit').append(newICDTr);
        }
    });
    //var templateHtml = '<tr>' +
    //                    '<td>' +
    //                     '<input class="form-control input-xs non_mandatory_field_halo uppercase" id="ICDCodes_' + templateIndex + '__Code" name="ICDCodes[' + templateIndex + '].ICDCode" type="text" value="">' +
    //                     '<span class="field-validation-valid" data-valmsg-for="ICDCodes[' + templateIndex + '].ICDCode" data-valmsg-replace="true"></span>' +
    //                    '</td>' +
    //                    '<td>' +
    //                     '<input class="form-control input-xs non_mandatory_field_halo uppercase" id="ICDCodes_' + templateIndex + '__Description" name="ICDCodes[' + templateIndex + '].Description" type="text" value="">' +
    //                     '<span class="field-validation-valid" data-valmsg-for="ICDCodes[' + templateIndex + '].Description" data-valmsg-replace="true"></span>' +
    //                     '</td>' +
    //                    '<td>'+
    //                        '<input class="form-control input-xs non_mandatory_field_halo uppercase" id="ICDCodes_' + templateIndex + 'HCCCodes__Code" name="ICDCodes[' + templateIndex + '].HCCCodes[0].Code" type="text" value="">' +
    //                    '</td>'+
    //                    '<td>'+
    //                        '<input class="form-control input-xs non_mandatory_field_halo uppercase" id="ICDCodes_' + templateIndex + 'HCCCodes__Type" name="ICDCodes[' + templateIndex + '].HCCCodes[0].Type" type="text" value="">'+
    //                    '</td>'+
    //                    '<td>'+
    //                        '<input class="form-control input-xs non_mandatory_field_halo uppercase" id="ICDCodes_' + templateIndex + 'HCCCodes__Version" name="ICDCodes[' + templateIndex + '].HCCCodes[0].Version" type="text" value="">' +
    //                    '</td>'+
    //                    '<td>'+
    //                        '<input class="form-control input-xs non_mandatory_field_halo uppercase" id="ICDCodes_' + templateIndex + 'HCCCodes__Description" name="ICDCodes[' + templateIndex + '].HCCCodes[0].Description" type="text" value="">' +
    //                    '</td>'+
    //                    '<td>'+
    //                        '<input class="form-control input-xs non_mandatory_field_halo uppercase" id="ICDCodes_' + templateIndex + 'HCCCodes__Weight" name="ICDCodes[' + templateIndex + '].HCCCodes[0].Weight" type="text" value="">' +
    //                    '</td>'+
    //                    '<td onclick="RemoveRows(this)"><button class="btn btn-xs btn-danger"><i class="fa fa-minus"></i></button></td>'+
    //                '</tr>';
   //$('#ICDCodetbody_Edit').append(templateHtml);
});

/** 
@description Removes the row from the table 
* @param {string} ele current element
 */

var RemovecurrentICDrow = function (ele) {
     $(ele).parent("tr").remove();
    var trCount = $(ele).parent().parent().find('tr').length;
    if (trCount <= 2) {
        $(ele).parent().parent().find('tr:first-child').find('td:last-child').remove()
    }
 }





/**
* @description appends selected icd codes from history to Main Table
*/
$('#HistoryDiagnosisICD').off('click', '.activeIcdCode').on('click', '.activeIcdCode', function () {
    var formData = new FormData();
    $("form#savedetailsForm input[name*='ICDCodes'").each(function () {
        var input = $(this); // This is the jquery object of the input, do what you will
        var name = input.attr('name');
        var value = input.val();
        formData.append(name, value);

    });

    $.ajax({
        type: 'POST',
        url: '/Coding/Coding/AddICDFromHistory',
        data: formData,
        contentType: false,
        processData: false,
        success: function (data) {
            $('#icdCodecontainer').html(data);
        }
    });
});



$('span[data-toggle="tooltip"]').tooltip();



