$(".memberSearchOptions").on('click', 'li a', function () {
    $(".resultMemberSearch").html($(this).text() + ' <span class="caret"></span>');
    $(".resultMemberSearch").val($(this).text());
    $('.memSearchField').attr('placeholder', $(this).text().toUpperCase());
    if ($('.memSearchField').attr('name') === 'Member_Name') {
        $('.memSearchField').attr('name', 'Subscriber_ID');
    } else {
        $('.memSearchField').attr('name', 'Member_Name');
    }
});


var MemberName;
var SubscriberID;

$('#member_search_btnCCM').click(function () {
    //try {
    //    // for showing from next time the div which was hiding when clicked on save and create new
    //    $('#memberSearchResultCCM').show();
    //} catch (e) {

    //}

    MemberName = $('[name="Member_Name"]').val();
    SubscriberID = $('[name="Subscriber_ID"]').val();
    /** 
    @description Get member result based on selected string
     */
    $.ajax({
        type: 'GET',
        url: '/Billing/CreateClaim/GetMemberResult?SubscriberID=' + SubscriberID + '&MemberName=' + MemberName +'>',
        success: function (data) {
            var spanEle = '<span class="text-upper">You searched for "<b>' + $('[name="memberSearchM"]').val() + '</b>" | <b>10</b> Member results found</span>'
            $('#memberSearchResultCCM').html(spanEle + data);
        }
    });
})



//$(".providerSearchOptions").on('click', 'li a', function () {
//    $(".resultProviderSearch").html($(this).text() + ' <span class="caret"></span>');
//    $(".resultProviderSearch").val($(this).text());
//    $('.provSearchField').attr('placeholder', $(this).text().toUpperCase());

//    if ($('.provSearchField').attr('name') === 'Provider_name') {
//        $('.provSearchField').attr('name', 'Provider_NPI');
//    } else {
//        $('.provSearchField').attr('name', 'Provider_name');
//    }

//});