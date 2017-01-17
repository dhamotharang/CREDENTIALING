//-----------------------grid component------------------------------
//$('#menu_toggle').trigger("click");
$('#fullBodyContainer').removeClass('pre-scrollable');
$('#QueueGrid').prtGrid({
    url: "/UM/Queue/GetQueueListByIndex",
    dataLength: 30,
    height:700,
    columns: [
        { type: 'none', name: '', text: '', widthPercentage:1, sortable: { isSort: false } },
        { type: 'none', name: '', text: '', widthPercentage: 2, sortable: { isSort: false } },
        { type: 'text', name: 'Abbrevation', text: 'ABV', widthPercentage:3, sortable: { isSort: true, defaultSort: 'ASC' } },
        { type: 'text', name: 'ReferenceNumber', text: 'REF', widthPercentage: 6, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'MemberID', text: 'MBRID', widthPercentage: 5, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'MemberName', text: 'MBRNAME', widthPercentage: 6, sortable: { isSort: true, defaultSort: null } },
        { type: 'date', name: 'FromDate', text: 'FROM', widthPercentage: 4, sortable: { isSort: true, defaultSort: null } },
        { type: 'date', name: 'ToDate', text: 'TO', widthPercentage: 4, sortable: { isSort: true, defaultSort: null } },
        //{ type: 'text', name: 'ProviderID', text: 'PRV ID', widthPercentage: 5.5, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'ProviderName', text: 'PROVIDER', widthPercentage: 6, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'FacilityName', text: 'FACILITY', widthPercentage: 5, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'SVCAttProviderName', text: 'SVCPROVIDER', widthPercentage: 6, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'RequestType', text: 'REQ', widthPercentage: 4, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'AuthorizationType', text: 'AUTH', widthPercentage: 3, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'TypeOfCare', text: 'TOC', widthPercentage: 3, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'DurationLeft', text: 'DURATION', widthPercentage: 5, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'ExpectedDOS', text: 'DOS', widthPercentage: 4, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'RequestedUnit', text: 'UNITS', widthPercentage: 3, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'AuthStatus', text: 'STATUS', widthPercentage: 6, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'PosAbb', text: 'POS', widthPercentage: 3, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'PrimaryDx', text: 'DX', widthPercentage: 6, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'Assigned', text: 'ASSIGNED', widthPercentage: 5, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'Entry', text: 'ENTRY', widthPercentage: 4, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'AuthNote', text: 'AUTH NOTE', widthPercentage: 4, sortable: { isSort: true, defaultSort: null } }
    ]
});

$('#facilityQueueGrid').prtGrid({
    url: "/UM/Queue/GetQueueListByIndex",
    dataLength: 30,
    height: 1000,
    columns: [
        { type: 'none', name: '', text: '', widthPercentage: 1, sortable: { isSort: false } },
        { type: 'none', name: 'Abbrevation', text: 'ABV', widthPercentage: 2.5, sortable: { isSort: true, defaultSort: 'ASC' } },
        { type: 'text', name: 'ReferenceNumber', text: 'REF', widthPercentage: 5.5, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'MemberID', text: 'MBRID', widthPercentage: 5, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'MemberName', text: 'MBRNAME', widthPercentage: 5, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'ExpectedDOA', text: 'EXPDOA', widthPercentage: 4, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'DateOfConversion', text: 'DOC', widthPercentage: 4, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'ReviewDate', text: 'REVIEWDT', widthPercentage: 4.2, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'ExpectedDcDate', text: 'EXPDC', widthPercentage: 4, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'ProviderName', text: 'PROVIDER', widthPercentage: 4.7, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'FacilityName', text: 'FACILITY', widthPercentage: 4.7, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'SvcAttProviderName', text: 'SVCPROVIDER', widthPercentage: 5.2, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'RequestType', text: 'REQUEST', widthPercentage: 3.5, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'AuthorizationType', text: 'AUTH', widthPercentage: 2.5, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'TypeOfCare', text: 'TOC', widthPercentage: 2.1, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'Review', text: 'REVIEW', widthPercentage: 3.2, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'ActualLOS', text: 'ACTLOS', widthPercentage: 3.4, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'DaysRequested', text: 'DAYSREQ', widthPercentage: 3, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'TotalAuthorized', text: 'TOTALDENIED', widthPercentage: 4.5, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'TotalDenied', text: 'TOTALAUTHED', widthPercentage: 4.2, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'AuthStatus', text: 'STATUS', widthPercentage: 4, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'PosAbb', text: 'POS', widthPercentage: 3, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'PrimaryDx', text: 'DX', widthPercentage: 5, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'AssignedTo', text: 'ASSIGNEDTo', widthPercentage: 4.4, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'Entry', text: 'ENTRY', widthPercentage: 3, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'AuthNote', text: 'AUTHNOTE', widthPercentage: 4, sortable: { isSort: true, defaultSort: null } }
    ]
});


