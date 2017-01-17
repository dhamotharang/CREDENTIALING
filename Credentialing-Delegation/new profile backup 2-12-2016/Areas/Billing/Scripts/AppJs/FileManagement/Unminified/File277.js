//-----------------------grid component------------------------------

$('#277FileList').prtGrid({
    url: "/Billing/FileManagement/Get277TableListByIndex",
    dataLength: 50,
    columns: [{ type: 'text', name: 'ControlNumber', text: 'Control Number', widthPercentage: 10, sortable: { isSort: true, defaultSort: null } },
    { type: 'text', name: 'FileName', text: 'File Name', widthPercentage: 20, sortable: { isSort: true, defaultSort: null } },
    { type: 'text', name: 'Sender', text: 'Sender', widthPercentage: 15, sortable: { isSort: true, defaultSort: null } },
    { type: 'text', name: 'Receiver', text: 'Receiver', widthPercentage: 15, sortable: { isSort: true, defaultSort: null } },
    { type: 'date', name: 'DateOfCreation', text: 'Date Of Creation', widthPercentage: 10, sortable: { isSort: true, defaultSort: 'ASC' } },
    { type: 'text', name: 'RecievedDate', text: 'RecievedDate', widthPercentage: 10, sortable: { isSort: true, defaultSort: null } },
    { type: 'text', name: 'Status', text: 'Status', widthPercentage: 10, sortable: { isSort: true, defaultSort: null } },
    { type: 'none', name: '', text: '', widthPercentage: 10, sortable: { isSort: false } }],
    externalFactors: [{ name: 'ClaimAkg277', type: 'radio' }, { name: 'ClaimPending277', type: 'radio' }]
});

/** @description event for handling the header name
*/
$('.file277HeaderPart').off('click', '.normal-checkbox').on('click', '.normal-checkbox', function () {
    var count = 0;
    var CheckedValues = [];
    $('.file277HeaderPart .normal-checkbox').each(function () {
        if (this.checked === true) {
            count++;
            CheckedValues.push(this.value);
        }
    })
    if (count !== 0 && count === 2) {
        $('.file277HeaderLabel').html(CheckedValues[0] + " & " + CheckedValues[1]);
    } else if (count !== 0 && count < 2) {
        $('.file277HeaderLabel').html(CheckedValues[0]);
    } else {
        $('.file277HeaderLabel').html("");
    }
    $('.file277HeaderLabel').append(" 277 Files");
})