$(".memberSearchOptions").on('click', 'li a', function () {
    $(".resultMemberSearch").html($(this).text() + ' <span class="caret"></span>');
    $(".resultMemberSearch").val($(this).text());
    $('.memSearchField').attr('placeholder', $(this).text().toUpperCase());
});

$.ajax({
    type: 'GET',
    url: '/Billing/CreateClaim/GetMemberResult',
    success: function (data) {
        $('#memberSearchResultCC').html(data);
    }
});

$('.ScheduleEncounterContainer').off('click', '#search_provider_btn').on('click', '#search_provider_btn', function () {
    MakeItVisible(2, currentProgressBarData);
});

$('.ScheduleEncounterContainer').off('click', '#change_provider_btn').on('click', '#change_provider_btn', function () {
    MakeItVisible(2, currentProgressBarData);
});

$('.ScheduleEncounterContainer').off('click', '#change_member_btn').on('click', '#change_member_btn', function () {
    MakeItVisible(3, currentProgressBarData);
});


$('#FilterBtn .export-button').on('click', function () {
    $('#memberFilterCreateClaim').slideToggle();
})
