$('#provider_search_btn').on('click', function () {
    var data = {
        "key": $('.resultProviderSearch').text().trim().replace(/ /g, ''),
        "value": $('.provSearchField').val()
    }
    $.ajax({
        type: 'GET',
        url: '/Encounters/CreateEncounter/GetProviderResult?ProviderSearchParameter='+JSON.stringify(data),
        processData: false,
        contentType: false,
        success: function (result) {
            $('#ProviderResult').html(result);
            $('#provider_result_title').html('You searched for "<b>' + $('[name="Provider_name"]').val() + '</b>" | <b>10</b> Provider results found');
        }
    });
});

$(".providerSearchOptions").on('click', 'li a', function () {
    $(".resultProviderSearch").html($(this).text() + ' <span class="caret"></span>');
    $(".resultProviderSearch").val($(this).text());
    $('.provSearchField').attr('placeholder', $(this).text().toUpperCase());
});


