//-----------------------grid component------------------------------

$('#835ListfileTable').prtGrid({
    url: "/Billing/FileManagement/Get835TableListByIndex",
    dataLength: 50,
    columns: [{ type: 'text', name: 'PayerName', text: 'Payer Name', widthPercentage: 10, sortable: { isSort: true, defaultSort: null } },
    { type: 'text', name: 'PayeeName', text: 'Payee Name', widthPercentage: 20, sortable: { isSort: true, defaultSort: null } },
    { type: 'text', name: 'PayeeId', text: 'Payee ID', widthPercentage: 15, sortable: { isSort: true, defaultSort: null } },
    { type: 'date', name: 'Check Issue Date', text: 'CheckIssueDate', widthPercentage: 15, sortable: { isSort: true, defaultSort: 'ASC' } },
    { type: 'text', name: 'TotalPaymentAmount', text: 'Total Payment Amount', widthPercentage: 10, sortable: { isSort: true, defaultSort: null } },
    { type: 'text', name: 'CheckNumber', text: 'Check Number', widthPercentage: 10, sortable: { isSort: true, defaultSort: null } },
    { type: 'text', name: 'File Name', text: 'FileName', widthPercentage: 10, sortable: { isSort: true, defaultSort: null } },
    { type: 'none', name: '', text: '', widthPercentage: 10, sortable: { isSort: false } }],
    externalFactors: [{ name: 'Received835', type: 'radio' }, { name: 'Generated835', type: 'radio' }]
});

/** @description event for handling the header name
*/
$('.file835HeaderPart').off('click', '.normal-checkbox').on('click', '.normal-checkbox', function () {
    var count = 0;
    var CheckedValues = [];
    $('.file835HeaderPart .normal-checkbox').each(function () {
        if (this.checked === true) {
            count++;
            CheckedValues.push(this.value);
        }
    })
    if (count !== 0 && count === 2) {
        $('.file835HeaderLabel').html(CheckedValues[0] + " & " + CheckedValues[1]);
    } else if (count !== 0 && count < 2) {
        $('.file835HeaderLabel').html(CheckedValues[0]);
    } else {
        $('.file835HeaderLabel').html("");
    }
    $('.file835HeaderLabel').append(" 835 Files");
})

/** @description Getting the 835 Provider list.
 */
var ProviderCheckNumber;
var InterchangeKey;
$('#835ListfileTable').off('click', '.835viewBtn').on('click', '.835viewBtn', function () {
    $('#File835ListDiv').hide();
    InterchangeKey = $(this).closest('tr').attr('data-container');
    ProviderCheckNumber = $(this).closest('tr').attr('data-checknumber');
    $.ajax({
        type: 'GET',
        url: '/Billing/FileManagement/Get835ProviderList?InterKey=' + InterchangeKey + '&CheckNumber=' + ProviderCheckNumber,
        success: function (data) {
            $('#835ProviderList').html(data);
        }
    });
})

///** @description filters 835 list as Received or generated.
// */
//$('input[name="835filterList"]').on("click", function () {
//    var receivedinfo = $('#835received').is(':checked');
//    var generatedinfo = $('#835Generated').is(':checked');

//    if (receivedinfo && generatedinfo) {
//        getTableData835("true", "true")
//    }
//    else if (receivedinfo || generatedinfo) {
//        if (receivedinfo) {
//            getTableData835("true", "false")
//        }
//        if (generatedinfo) {
//            getTableData835("false", "true")
//        }
//    }
//    else {
//        getTableData835("false", "false")
//    }
//})
///** @description Getting 835 File List that has the specified rec and gen parameter.
// * @param {bool} rec type of files and gen type of files.
// */
//function getTableData835(rec, gen) {
//    $.ajax({
//        type: 'GET',
//        url: '/Billing/FileManagement/Get835TableList?Received=' + rec + '&Generated=' + gen,
//        success: function (data) {
//            $('#835fileTable').html(data);
//        }
//    });
//}

