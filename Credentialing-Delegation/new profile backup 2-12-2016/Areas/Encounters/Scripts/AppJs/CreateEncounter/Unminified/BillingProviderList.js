$('table').find('tbody#BillingProviders').find('tr').on('click', function () {
    $('#ProviderList').html('');
    $('#ProviderList').hide();
    $('.ProviderLabel').show();
    $('#SelectedBillingProvider').html($(this).find('.ProviderName').text());
    $('.ProviderSearch').hide();
});