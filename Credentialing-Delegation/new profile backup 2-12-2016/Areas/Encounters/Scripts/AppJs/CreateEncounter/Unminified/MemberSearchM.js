$(".memberSearchOptions").on('click', 'li a', function () {
    $(".resultMemberSearch").html($(this).text() + ' <span class="caret"></span>');
    $(".resultMemberSearch").val($(this).text());
    $('.memSearchField').attr('placeholder', $(this).text().toUpperCase());
});


$('#member_search_btnCCM').click(function () {
 
    var data= {
        "key": $('.resultMemberSearch').text().trim().replace(/ /g, ''),
        "value":$('.memSearchField').val()
        }
    $.ajax({
        type: 'GET',
        url: '/Encounters/CreateEncounter/GetMemberResult?MemberSearchParameter=' + JSON.stringify(data),
        success: function (data) {
            $('#memberSearchResultCCM').show();
            var spanEle = '<span class="text-upper">You searched for "<b>' + $('[name="memberSearchM"]').val() + '</b>" | <b>10</b> Member results found</span>'
            $('#memberSearchResultCCM').html(spanEle + data);
        }
    });
})