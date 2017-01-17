

/** 
@description Selecting member from member list by clicking on rows 
 */

$('#memberSearchResultCC #MemberResult tbody tr').on('click', function () {
    selectedMemberId = $(this).attr('data-container');
    currentProgressBarData[3].parameters[0].value = selectedMemberId;
    MakeItActive(4, currentProgressBarData);
   
})

$('#memberSearchResultCCM #MemberResult tbody tr').on('click', function () {
    selectedMemberId = $(this).attr('data-container');
    currentProgressBarData[2].parameters[0].value = selectedMemberId;
    MakeItActive(3, currentProgressBarData);
})

$('#providerMemberSelect #MemberResult tbody tr').on('click', function () {
    selectedMemberId = $(this).attr('data-container');
    currentProgressBarData[3].parameters[0].value = selectedMemberId;
    MakeItActive(4, currentProgressBarData);
})

//var memberSelected = function (subscriberID) {
//    currentProgressBarData[3].parameters[0].value = subscriberID;
//    MakeItActive(4, currentProgressBarData);
//    console.log(subscriberID);
//}


$('#MemberResult').prtGrid({
    url: "/Billing/CreateClaim/GetMemberResultByIndex",
    dataLength: 50,
    columns: [{ type: 'text', name: 'SubscriberID', text: 'Subscriber ID', widthPercentage: 10, sortable: { isSort: true, defaultSort: 'ASC' } },
    { type: 'text', name: 'MemberFullName', text: 'Member Name', widthPercentage: 15, sortable: { isSort: true, defaultSort: null } },
    { type: 'text', name: 'PatientBirthDate', text: 'DOB', widthPercentage: 10, sortable: { isSort: true, defaultSort: null } },
     { type: 'text', name: 'MemberFullAddress', text: 'Address', widthPercentage: 20, sortable: { isSort: true, defaultSort: null } },
     { type: 'text', name: 'PCP', text: 'PCP', widthPercentage: 10, sortable: { isSort: true, defaultSort: null } },
     { type: 'text', name: 'PayerName', text: 'PayerName', widthPercentage: 10, sortable: { isSort: true, defaultSort: null } },
     { type: 'text', name: 'StartDate', text: 'StartDate', widthPercentage: 10, sortable: { isSort: true, defaultSort: null } },
     { type: 'text', name: 'EndDate', text: 'EndDate', widthPercentage: 10, sortable: { isSort: true, defaultSort: null } },
    { type: 'none', name: '', text: '', widthPercentage: 5 }]
});