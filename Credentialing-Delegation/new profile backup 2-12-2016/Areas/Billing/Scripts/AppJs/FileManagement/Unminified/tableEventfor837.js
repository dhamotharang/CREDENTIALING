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


/** @description event for handling the header name
*/
$('.file837HeaderPart').off('click', '.normal-checkbox').on('click', '.normal-checkbox', function () {
    var count = 0;
    var CheckedValues = [];
    $('.file837HeaderPart .normal-checkbox').each(function () {
        if (this.checked === true) {
            count++;
            CheckedValues.push(this.value);
        }
    })
    if (count !== 0 && count === 2) {
        $('.file837HeaderLabel').html(CheckedValues[0] + " & " + CheckedValues[1]);
    } else if (count !== 0 && count < 2) {
        $('.file837HeaderLabel').html(CheckedValues[0]);
    } else {
        $('.file837HeaderLabel').html("");
    }
    $('.file837HeaderLabel').append(" 837 Files");
})

/** @description Getting the ClaimList.
 */
var IncomingFileIdFor837List;
var FileNameFor837;
var CNLabel837ClaimList;
$('.837Viewbtn').click(function () {
    
    IncomingFileIdFor837List = $(this).closest('tr').attr("data-container");
    CNLabel837ClaimList = $(this).closest('tr').find('td:nth-child(1)').text();
    FileNameFor837 = $(this).closest('tr').find('td:nth-child(2)').text();
    $('#Table837List').hide();    
    $.ajax({
        type: 'GET',
        url: '/Billing/FileManagement/GetClaimList?IncomeFileLoggerID=' + IncomingFileIdFor837List,
        success: function (data) {
            $('#837claimList').html(data);
        }
    });
})
/** @description filters 837 list as Received or dispatched.
 */
//$('input[name="filterList"]').on("click", function () {
//    var receivedinfo = $('#received').is(':checked');
//    var dispachedinfo = $('#dispatched').is(':checked');
//    if (receivedinfo && dispachedinfo) {
//        getTableData837("true", "true")
//    }
//    else if (receivedinfo || dispachedinfo) {
//        if (receivedinfo) {
//            getTableData837("true", "false")
//        }
//        if (dispachedinfo) {
//            getTableData837("false", "true")
//        }
//    }
//    else {
//        getTableData837("false", "false")
//    }
//})

/** @description Getting 837 File List that has the specified rec and dis parameter.
 * @param {bool} rec type of files and dis type of files.
 */
function getTableData837(rec, dis) {
    $.ajax({
        type: 'GET',
        url: '/Billing/FileManagement/Get837TableList?Received=' + rec + '&Dispatched=' + dis,
        success: function (data) {
            $('.Table837').html(data);
        }
    });
}
