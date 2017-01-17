
$('#SupervisingProvidersResult').prtGrid({
    url: "/Billing/CreateClaim/GetSupervisingProviderResultByIndex",
    dataLength: 50,
    columns: [{ type: 'text', name: 'ProviderNPI', text: 'NPI', widthPercentage: 20, sortable: { isSort: true, defaultSort: 'ASC' } },
    { type: 'text', name: 'ProviderFullName', text: 'Name', widthPercentage: 25, sortable: { isSort: true, defaultSort: null } },
    { type: 'text', name: 'ProviderFullAddress', text: 'Address', widthPercentage: 30, sortable: { isSort: true, defaultSort: null } },
    { type: 'text', name: 'ProviderTaxonomy', text: 'Tax Id', widthPercentage: 25, sortable: { isSort: true, defaultSort: null } }]
});


$('#SupervisingProvidersResult').find('tbody').on('click', 'tr', function () {

    var providerId = $(this).attr('data-container');
    $.ajax({
        type: 'GET',
        url: "/Billing/CreateClaim/GetSelectedSupervisingProvider?ProviderId=" + providerId,
        success: function (response) {
            var data = response.data;
            $("[name='SupervisingProvider.SupervisingProviderLastName']").val(data.SupervisingProviderLastName);
            $("[name='SupervisingProvider.SupervisingProviderMiddleName']").val(data.SupervisingProviderMiddleName);
            $("[name='SupervisingProvider.SupervisingProviderFirstName']").val(data.SupervisingProviderFirstName);
            $("[name='SupervisingProvider.SupervisingProviderFirstAddress']").val(data.SupervisingProviderFirstAddress);
            $("[name='SupervisingProvider.SupervisingProviderSecondAddress']").val(data.SupervisingProviderSecondAddress);
            $("[name='SupervisingProvider.SupervisingProviderCity']").val(data.SupervisingProviderCity);
            $("[name='SupervisingProvider.SupervisingProviderState']").val(data.SupervisingProviderState);
            $("[name='SupervisingProvider.SupervisingProviderZip']").val(data.SupervisingProviderZip);
            $("[name='SupervisingProvider.SupervisingProviderPhoneNo']").val(data.SupervisingProviderPhoneNo);
            $("[name='SupervisingProvider.SupervisingProviderIdentifier1']").val(data.SupervisingProviderIdentifier1);
            $("[name='SupervisingProvider.SupervisingProviderIdentifier2']").val(data.SupervisingProviderIdentifier2);
            $("[name='SupervisingProvider.SupervisingProviderTaxonomy']").val(data.SupervisingProviderTaxonomy);

            $('#CreateClaimsTempForMemberSupervisingProviderLabel').text(data.SupervisingProviderFullName + ' ' + data.SupervisingProviderFullAddress);
            $('#SupervisingProvidersResult').remove();
        }
    });

})