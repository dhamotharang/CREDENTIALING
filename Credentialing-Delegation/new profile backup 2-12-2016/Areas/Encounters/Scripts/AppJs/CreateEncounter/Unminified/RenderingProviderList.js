$('table').find('tbody#RenderingProviders').find('tr').on('click', function () {
    $('#ProviderList').html('');
    $('#ProviderList').hide();
    $('.ProviderLabel').show();
    $('#SelectedRenderingProvider').html($(this).find('.ProviderName').text());
    $('.ProviderSearch').hide();
});