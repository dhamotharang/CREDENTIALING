
/** 
@description Getting provider result 
 */
var ProviderName;
var ProviderNPI;
var providerSelectFieldName = "Provider_name";

$('#provider_search_btn').on('click', function () {

    ProviderName = $('[name="Provider_name"]').val();
    ProviderNPI = $('[name="Provider_NPI"]').val();

    $.ajax({
        type: 'GET',
        url: '/Billing/CreateClaim/GetProviderResult?ProviderName=' + ProviderName + '&ProviderNPI=' + ProviderNPI,
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

    if ($('.provSearchField').attr('name') === 'Provider_name') {
        $('.provSearchField').attr('name', 'Provider_NPI');
    } else {
        $('.provSearchField').attr('name', 'Provider_name');
    }

});



