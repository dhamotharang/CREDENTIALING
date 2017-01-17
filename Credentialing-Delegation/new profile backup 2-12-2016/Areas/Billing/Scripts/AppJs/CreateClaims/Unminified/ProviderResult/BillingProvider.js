
$('#BillingProvidersResult').prtGrid({
    url: "/Billing/CreateClaim/GetBillingProviderResultByIndex",
    dataLength: 50,
    columns: [{ type: 'text', name: 'ProviderNPI', text: 'NPI', widthPercentage: 20, sortable: { isSort: true, defaultSort: 'ASC' } },
    { type: 'text', name: 'ProviderFullName', text: 'Name', widthPercentage: 25, sortable: { isSort: true, defaultSort: null } },
    { type: 'text', name: 'ProviderFullAddress', text: 'Address', widthPercentage: 30, sortable: { isSort: true, defaultSort: null } },
    { type: 'text', name: 'ProviderTaxonomy', text: 'Tax Id', widthPercentage: 25, sortable: { isSort: true, defaultSort: null } }]
});


$('#BillingProvidersResult').find('tbody').on('click', 'tr', function () {

    var providerId = $(this).attr('data-container');
    $.ajax({
        type: 'GET',
        url: "/Billing/CreateClaim/GetSelectedBillingProvider?ProviderId=" + providerId,
        success: function (response) {
            var data = response.data;
            $("[name='BillingProvider.BillingProviderFirstName']").val(data.BillingProviderFirstName);
            $("[name='BillingProvider.BillingProviderMiddleName']").val(data.BillingProviderMiddleName);
            $("[name='BillingProvider.BillingProviderLastOrOrganizationName']").val(data.BillingProviderLastOrOrganizationName);
            $("[name='BillingProvider.BillingProviderFirstAddress']").val(data.BillingProviderFirstAddress);
            $("[name='BillingProvider.BillingProviderSecondAddress']").val(data.BillingProviderSecondAddress);
            $("[name='BillingProvider.BillingProviderCity']").val(data.BillingProviderCity);
            $("[name='BillingProvider.BillingProviderState']").val(data.BillingProviderState);
            $("[name='BillingProvider.BillingProviderZip']").val(data.BillingProviderZip);
            $("[name='BillingProvider.BillingGroupNPI']").val(data.BillingGroupNPI);

            $('#CreateClaimsTempForProviderBillingProviderLabel').text(data.BillingProviderFullName + ' ' + data.BillingProviderFullAddress);
            $('#BillingProvidersResult').remove();
        }
    });

})