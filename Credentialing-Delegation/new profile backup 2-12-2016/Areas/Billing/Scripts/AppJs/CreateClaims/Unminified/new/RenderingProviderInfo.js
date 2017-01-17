$('#RenderingProviderResult').prtGrid({
    //url: "/Billing/CreateClaim/GetProviderResult",
    dataLength: 50,
    columns: [{ type: 'text', name: 'ProviderFullName', text: 'Name', widthPercentage: 30, sortable: { isSort: true, defaultSort: 'ASC' } },
    { type: 'text', name: 'ProviderNPI', text: 'NPI', widthPercentage: 30, sortable: { isSort: true, defaultSort: null } },
    { type: 'text', name: 'Taxonomy', text: 'Taxonomy', widthPercentage: 20, sortable: { isSort: true, defaultSort: null } },
     { type: 'text', name: 'Specialty', text: 'Specialty', widthPercentage: 20, sortable: { isSort: true, defaultSort: null } }]
});