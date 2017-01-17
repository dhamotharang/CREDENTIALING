//-----------------------grid component------------------------------
$('#QueueGrid').prtGrid({
    url: "/Portal/PriorAuthQueue/GetQueueListByIndex",
    dataLength: 30,
    height: 700,
    columns: [
        { type: 'none', name: '', text: '', widthPercentage: 1, sortable: { isSort: false } },
        { type: 'none', name: '', text: '', widthPercentage: 2, sortable: { isSort: false } },
        { type: 'text', name: 'Abbrevation', text: 'ABV', widthPercentage: 3, sortable: { isSort: true, defaultSort: 'ASC' } },
        { type: 'text', name: 'ReferenceNumber', text: 'REF', widthPercentage: 5.5, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'MemberID', text: 'MBRID', widthPercentage: 5, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'MemberName', text: 'MBRNAME', widthPercentage: 5, sortable: { isSort: true, defaultSort: null } },
        { type: 'date', name: 'FromDate', text: 'FROM', widthPercentage: 4, sortable: { isSort: true, defaultSort: null } },
        { type: 'date', name: 'ToDate', text: 'TO', widthPercentage: 4, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'ProviderID', text: 'PRV ID', widthPercentage: 5.5, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'ProviderName', text: 'PROVIDER', widthPercentage: 5, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'FacilityName', text: 'FACILITY', widthPercentage: 5, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'SVCAttProviderName', text: 'SVCPROVIDER', widthPercentage: 5, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'RequestType', text: 'REQ', widthPercentage: 4, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'AuthorizationType', text: 'AUTH', widthPercentage: 3, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'TypeOfCare', text: 'TOC', widthPercentage: 3, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'DurationLeft', text: 'DURATION', widthPercentage: 5, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'ExpectedDOS', text: 'DOS', widthPercentage: 4, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'RequestedUnit', text: 'UNITS', widthPercentage: 3, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'AuthStatus', text: 'STATUS', widthPercentage: 6, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'PosAbb', text: 'POS', widthPercentage: 3, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'PrimaryDx', text: 'DX', widthPercentage: 6, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'Assigned', text: 'ASSIGNED', widthPercentage: 4, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'Entry', text: 'ENTRY', widthPercentage: 4, sortable: { isSort: true, defaultSort: null } },
        { type: 'text', name: 'AuthNote', text: 'AUTH NOTE', widthPercentage: 4, sortable: { isSort: true, defaultSort: null } }
    ]
});
