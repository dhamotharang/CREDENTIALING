
$('#RenderingProvidersResult').prtGrid({
    url: "/Billing/CreateClaim/GetRenderingProviderResultByIndex",
    dataLength: 50,
    columns: [{ type: 'text', name: 'ProviderNPI', text: 'NPI', widthPercentage: 20, sortable: { isSort: true, defaultSort: 'ASC' } },
    { type: 'text', name: 'ProviderFullName', text: 'Name', widthPercentage: 25, sortable: { isSort: true, defaultSort: null } },
    { type: 'text', name: 'ProviderFullAddress', text: 'Address', widthPercentage: 30, sortable: { isSort: true, defaultSort: null } },
    { type: 'text', name: 'ProviderTaxonomy', text: 'Tax Id', widthPercentage: 25, sortable: { isSort: true, defaultSort: null } }]
});


$('#RenderingProvidersResult').find('tbody').on('click', 'tr', function () {

    var providerId = $(this).attr('data-container');
    $.ajax({
        type: 'GET',
        url: "/Billing/CreateClaim/GetSelectedRenderingProvider?ProviderId=" + providerId,
        success: function (response) {
            var data = response.data;
            $("[name='RenderingProvider.ProviderFirstName']").val(data.ProviderFirstName);
            $("[name='RenderingProvider.ProviderMiddleName']").val(data.ProviderMiddleName);
            $("[name='RenderingProvider.ProviderLastName']").val(data.ProviderLastName);
            $("[name='RenderingProvider.ProviderFirstAddress']").val(data.ProviderFirstAddress);
            $("[name='RenderingProvider.ProviderSecondAddress']").val(data.ProviderSecondAddress);
            $("[name='RenderingProvider.ProviderCity']").val(data.ProviderCity);
            $("[name='RenderingProvider.ProviderState']").val(data.ProviderState);
            $("[name='RenderingProvider.ProviderZip']").val(data.ProviderZip);
            $("[name='RenderingProvider.Speciality']").val(data.Speciality);
            $("[name='RenderingProvider.Taxonomy']").val(data.Taxonomy);
            $("[name='RenderingProvider.TaxId']").val(data.TaxId);

            $('#CreateClaimsTempForProviderRenderingProviderLabel').text(data.ProviderFullName + ' ' + data.ProviderFullAddress);
            $('#RenderingProvidersResult').remove();
        }
    });

})