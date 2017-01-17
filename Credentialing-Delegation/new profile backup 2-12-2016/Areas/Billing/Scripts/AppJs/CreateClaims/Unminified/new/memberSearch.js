

$(".memberSearchOptions").on('click', 'li a', function () {
    $(".resultMemberSearch").html($(this).text() + ' <span class="caret"></span>');
    $(".resultMemberSearch").val($(this).text());
    $('.memSearchField').attr('placeholder', $(this).text().toUpperCase());
});


/** 
@description Get member result based on selected string
 */
$.ajax({
    type: 'GET',
    url: '/Billing/CreateClaim/GetMemberResult',
    success: function (data) {
        
        $('#memberSearchResultCC').html(data);
    }
});

/** 
@description Navigating to search provider page
 */
$('#search_provider_btn').on('click', function () {
    MakeItVisible(2, currentProgressBarData);
});


/** 
@description Showing date of service member filter
 */
$('#FilterBtn .export-button').on('click', function () {
    $('#memberFilterCreateClaim').slideToggle();
})
