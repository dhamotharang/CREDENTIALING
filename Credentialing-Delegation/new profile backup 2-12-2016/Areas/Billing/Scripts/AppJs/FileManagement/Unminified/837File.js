

setTimeout(function () {
    $('input.flat').iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green'
    });


}, 100)

//-----------------------grid component------------------------------

$('#837FileList').prtGrid({
    url: "/Billing/FileManagement/Get837TableListByIndex",
    dataLength: 50,
    columns: [{ type: 'text', name: 'ControlNumber', text: 'Control Number', widthPercentage: 10, sortable: { isSort: true, defaultSort: null } },
    { type: 'text', name: 'FileName', text: 'File Name', widthPercentage: 20, sortable: { isSort: true, defaultSort: null } },
    { type: 'text', name: 'Sender', text: 'Sender', widthPercentage: 15, sortable: { isSort: true, defaultSort: null } },
    { type: 'text', name: 'Receiver', text: 'Receiver', widthPercentage: 15, sortable: { isSort: true, defaultSort: null } },
    { type: 'date', name: 'DateOfCreation', text: 'Date Of Creation', widthPercentage: 10, sortable: { isSort: true, defaultSort: 'ASC' } },
    { type: 'text', name: 'AgeInDays', text: 'Age(Days)', widthPercentage: 10, sortable: { isSort: true, defaultSort: null } },
    { type: 'text', name: 'Status', text: 'Status', widthPercentage: 10, sortable: { isSort: true, defaultSort: null } },
    { type: 'none', name: '', text: '', widthPercentage: 10, sortable: { isSort: false } }],
    externalFactors: [{ name: 'ReceivedList837', type: 'radio' }, { name: 'DispatchedList837', type: 'radio' }]
});

