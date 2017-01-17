$('table').find('tbody#Facilities').find('tr').on('click', function () {
    $('#ProviderList').html('');
    $('#ProviderList').hide();
    $('.ProviderLabel').show();
    $('#SelectedFacility').html($(this).find('.ProviderName').text());
    $('.ProviderSearch').hide();
});