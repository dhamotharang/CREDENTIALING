$('table').find('tbody#ReferringProviders').find('tr').on('click', function () {
    $('#ProviderList').html('');
    $('#ProviderList').hide();
    $('.ProviderLabel').show();
    $('#SelectedReferingProvider').html($(this).find('.ProviderName').html());
    console.log($(this).find('.ProviderName').html());
    $('.ProviderSearch').hide();
});