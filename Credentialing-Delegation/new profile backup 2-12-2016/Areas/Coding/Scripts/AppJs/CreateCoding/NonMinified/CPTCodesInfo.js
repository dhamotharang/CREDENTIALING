var isCPTHistoryData = false;
var checkedCptList = [];
var cptCodes = [];

if (!isCPTHistoryData) {
    $.ajax({
        type: 'GET',
        url: '/Coding/Coding/GetCPTCodeHistory',
        success: function (data) {
            $('#historyCptCodes').html(data);
            isCPTHistoryData = true;
        }
    });
}

$.ajax({
    type: 'GET',
    url: '/Coding/Coding/GetCptHistoryData',
    success: function (data) {
        cptCodes = data.Cptcodes;
    }
});


/** 
@description This event triggers by clicking on Active Diagnosis button. It Shows or hides the Active Diagnosis Panel.
 */
$('.CPTCodesMainPanel_Create').off('click', '.activeCptCodes').on('click', '.activeCptCodes', function () {
    $('#historyCptCodes').toggle();
})


/** 
@description This event triggers by clicking on the close button of Active Diagnosis Codes Panel. It Shows or hides the Active Diagnosis Panel.
 */
$('.CPTCodesMainPanel_Create').off('click', '.closeactiveCptCodes').on('click', '.closeactiveCptCodes', function () {
    $('#historyCptCodes').toggle();
})


/** 
@description This event triggers by clicking on arrow mark besides to the Diagnosis Pointers in CPT Code table. It Opens a modal from where the User can selects the Diagnosis Pointers
 */
$('.CPTCodesMainPanel_Create').off('click', '.openDiagnosisModal').on('click', '.openDiagnosisModal', function () {
    showModal('DiagnosisModal_createCoding');
});


/** 
@description this event triggers by clicking on add. It will append a new row to the table body
 */
var templateIndexCPT = 0;
$('.CPTCodesMainPanel_Create').off('click', '#AddNewRowInCPTTable').on('click', '#AddNewRowInCPTTable', function (e) {
    e.preventDefault();
    templateIndexCPT++;
    var templateHtml = '<tr>'+
                                '<td><input class="form-control input-xs text-uppercase" id="CPTCodes_' + templateIndexCPT + '__Code" name="CPTCodes[' + templateIndexCPT + '].Code" type="text" value=""></td>'+
                                '<td><input class="form-control input-xs text-uppercase" id="CPTCodes_' + templateIndexCPT + '__Description" name="CPTCodes[' + templateIndexCPT + '].Description" type="text" value=""></td>'+
                                '<td><input class="form-control input-xs text-uppercase modifier_width" id="CPTCodes_' + templateIndexCPT + '__Modifier1" name="CPTCodes[' + templateIndexCPT + '].Modifier1" type="text" value="">'+
                                '<input class="form-control input-xs text-uppercase modifier_width" id="CPTCodes_' + templateIndexCPT + '__Modifier2" name="CPTCodes[' + templateIndexCPT + '].Modifier2" type="text" value="">'+
                                '<input class="form-control input-xs text-uppercase modifier_width" id="CPTCodes_' + templateIndexCPT + '__Modifier3" name="CPTCodes[' + templateIndexCPT + '].Modifier3" type="text" value="">'+
                                '<input class="form-control input-xs text-uppercase modifier_width" id="CPTCodes_' + templateIndexCPT + '__Modifier4" name="CPTCodes[' + templateIndexCPT + '].Modifier4" type="text" value=""></td>'+
                                '<td><input class="form-control input-xs text-uppercase modifier_width" id="CPTCodes_' + templateIndexCPT + '__DiagnosisPointer1" name="CPTCodes[' + templateIndexCPT + '].DiagnosisPointer1" type="text" value="">'+
                                '<input class="form-control input-xs text-uppercase modifier_width" id="CPTCodes_' + templateIndexCPT + '__DiagnosisPointer2" name="CPTCodes[' + templateIndexCPT + '].DiagnosisPointer2" type="text" value="">'+
                                    '<input class="form-control input-xs text-uppercase modifier_width" id="CPTCodes_' + templateIndexCPT + '__DiagnosisPointer3" name="CPTCodes[' + templateIndexCPT + '].DiagnosisPointer3" type="text" value="">'+
                                    '<input class="form-control input-xs text-uppercase modifier_width" id="CPTCodes_' + templateIndexCPT + '__DiagnosisPointer4" name="CPTCodes[' + templateIndexCPT + '].DiagnosisPointer4" type="text" value="">'+
                                    '<input class="form-control input-xs text-uppercase modifier_width JustForDP1" placeholder="" type="text" value="">'+
                                    '<a class="btn btn-xs openDiagnosisModal"><i class=" fa fa-forward"></i></a></td>'+
                                '<td><input class="form-control input-xs text-uppercase" id="CPTCodes_' + templateIndexCPT + '__Fees" name="CPTCodes[' + templateIndexCPT + '].Fees" placeholder="" type="text" value=""></td>' +
                               '<td onclick="RemoveRows(this)"><button class="btn btn-xs btn-danger"><i class="fa fa-close"></i></button></td></tr>'
    $('#cptCodeBody').append(templateHtml);
});


/** 
@description Removes the row from the table 
* @param {string} ele current element
 */
function RemoveCPTCodes(ele) {
    $(ele).parent("tr").remove();
}