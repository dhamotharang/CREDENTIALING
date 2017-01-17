
$('#RefferingProvidersResult').prtGrid({
    url: "/Billing/CreateClaim/GetReferringProviderResultByIndex",
    dataLength: 50,
    columns: [{ type: 'text', name: 'ProviderNPI', text: 'NPI', widthPercentage: 20, sortable: { isSort: true, defaultSort: 'ASC' } },
    { type: 'text', name: 'ProviderFullName', text: 'Name', widthPercentage: 25, sortable: { isSort: true, defaultSort: null } },
    { type: 'text', name: 'ProviderFullAddress', text: 'Address', widthPercentage: 30, sortable: { isSort: true, defaultSort: null } },
    { type: 'text', name: 'ProviderTaxonomy', text: 'Tax Id', widthPercentage: 25, sortable: { isSort: true, defaultSort: null } }]
});


$('#RefferingProvidersResult').find('tbody').on('click', 'tr', function () {

    var providerId = $(this).attr('data-container');
    $.ajax({
        type: 'GET',
        url: "/Billing/CreateClaim/GetSelectedReferringProvider?ProviderId=" + providerId,
        success: function (response) {
            var data = response.data;
            $("[name='ReferringProvider.ReferringProviderLastName']").val(data.ReferringProviderLastName);
            $("[name='ReferringProvider.ReferringProviderMiddleName']").val(data.ReferringProviderMiddleName);
            $("[name='ReferringProvider.ReferringProviderFirstName']").val(data.ReferringProviderFirstName);
            $("[name='ReferringProvider.ReferringProviderFirstAddress']").val(data.ReferringProviderFirstAddress);
            $("[name='ReferringProvider.ReferringProviderSecondAddress']").val(data.ReferringProviderSecondAddress);
            $("[name='ReferringProvider.ReferringProviderCity']").val(data.ReferringProviderCity);
            $("[name='ReferringProvider.ReferringProviderState']").val(data.ReferringProviderState);
            $("[name='ReferringProvider.ReferringProviderZip']").val(data.ReferringProviderZip);
            $("[name='ReferringProvider.ReferringProviderPhoneNo']").val(data.ReferringProviderPhoneNo);
            $("[name='ReferringProvider.ReferringProviderIdentifier']").val(data.ReferringProviderIdentifier);
            $("[name='ReferringProvider.ReferringProviderTaxonomy']").val(data.ReferringProviderTaxonomy);

            $('#CreateClaimsTempForProviderReferringProviderLabel').text(data.ReferringProviderFullName + ' ' + data.ReferringProviderFullAddress);
            $('#RefferingProvidersResult').remove();
        }
    });

})