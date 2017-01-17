var rootDir = '';

$(function () {
    $("#ptGrid").ptGrid({
        source: {
            type: "post",
            dataType: "json",
            url: rootDir + '/Grid/GetFilteredClaimListDetails',
            //dataFields: [
            //        { name: 'MemberLastName', type: 'string' },
            //        { name: 'ProviderLastName', type: 'string' },
            //        { name: 'ClaimNumber', type: 'float' },
            //        { name: 'MemberFirstName', type: 'string' }
            //],
            initRowDetails: null,
            pageSize: 20,
            serverPaging: true,
            serverFiltering: true,
            serverSorting: true,
        },
        columns: [
        { text: 'Claim Number', dataField: 'ClaimNumber', width: 100 },
        { text: 'Member Name', dataField: 'MemberName', width: 100 },
        { text: 'Provider Name', dataField: 'ProviderName', width: 100 },
        { text: 'Payer Name', dataField: 'PayerName', width: 200 },
        { text: 'Date Of Service', dataField: 'DOS', width: 140 },
        { text: 'Date Created', dataField: 'DateCreated', width: 130 },
        { text: 'Account Name', dataField: 'AccountName', width: 120 },
        { text: 'Status', dataField: 'ActivationStatus', width: 100, editable: false },
        { text: 'Age Doc', dataField: 'Age_DOC', width: 120, cellsAlign: 'right', align: 'right' },
        { text: 'Age DOS', dataField: 'Age_DOS', width: 100, cellsAlign: 'right', align: 'right', cellsFormat: 'c2' },
        { text: 'Total Charges', dataField: 'TotalCharges', width: 120, cellsAlign: 'right', align: 'right', cellsFormat: 'c2' }
        ],
        width: 1355,
        
    });
});