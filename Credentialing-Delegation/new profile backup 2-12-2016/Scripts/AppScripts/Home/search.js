$(document).ready(function () {
    /*on load create table*/
    var trTag = "<tr onclick='redirectToIndex();'>"
    //var trTag = "<tr onclick='getTabContent(" + '"Create New Auth"' + ");'>"
    var trTagEnd = "</tr>"
    var tdTag = "<td class='col-lg-1'>";
    var tdTagEnd = "</td>"
    for (var i = 0; i < MemberData.length; i++) {
        $('#searhcResultBody').append(trTag + tdTag + MemberData[i].MemberID + tdTagEnd
            + tdTag + MemberData[i].LastName + tdTagEnd
            + tdTag + MemberData[i].FirstName + tdTagEnd
            + tdTag + MemberData[i].DOB + tdTagEnd
            + tdTag + MemberData[i].Gender + tdTagEnd
            + tdTag + MemberData[i].PCP + tdTagEnd
            + tdTag + MemberData[i].PCPPhone + tdTagEnd
            + tdTag + MemberData[i].EffectiveDate + tdTagEnd
             + tdTag + MemberData[i].Address + tdTagEnd
            + tdTag + MemberData[i].State + tdTagEnd
            + trTagEnd);
    }

    $('[data-toggle="tooltip"]').tooltip();//For tooltip
    // for displaying previous serach result
    var diplaySearch = false;
    // search box on click

    $("#searchbox").click(function () {
        $("#searchbox").focus();
        $("#searchbox").css('width', '94%');
        $("#searchbox").css('border-bottom', '2px solid #fff');
        if (diplaySearch) {
            SResultOpen();
        }
    });
    $(".custom-cls-btn").click(function () {
        SResultClose();
    });
    // for buttons toogle
    $('.menu-btn-toggle').click(function () {
        $(this).toggleClass('open');
        $('.option').toggleClass('scale-on');
    });
    $(window).bind('keyup', function (event) {
        // esc event
        if (event.keyCode === 27) {
            SResultClose();
        }
        // enter event
        if (event.keyCode === 13) {
            if ($('#searchbox').val() != '') {
                SResultOpen();

                // $('#serch-default-message').hide();
                $('#search-results').show();
                $('.search-word').html($('#searchbox').val());
            }
        }
    });

    /* drop down selected value */
    $(".dropdown-menu li a").click(function () {
        $(this).parents(".search-arrow").find('.dropdown-toggle').html($(this).text() + ' <span class="caret"></span>');
        $(this).parents(".search-arrow").find('.dropdown-toggle').val($(this).data('value'));
    });

});

//Open Create Auth on <tr> Click
function redirectToIndex() {
    window.location.pathname = '/Home/Index';
};

// For Open Event
function SResultOpen() {
    $('.searchResult').show("clip", 500);
    $("#searchbox").focus();
    $('#floatingmenu').show("puff", 500);
    $('body').css('overflow-y', 'hidden');
    $("#searchbox").css('width', '94%');
    $("#searchbox").css('border-bottom', '2px solid #01c101');
    $('.search-arrow').css("display", "block");
}
// For Close Event
function SResultClose() {
    $('#searchbox').val('');
    $('.search-result-overview').hide();
    $('.searchResult').hide("clip", 500);
    $('body').css('overflow-y', 'scroll');
    $('#floatingmenu').hide();
    $("#searchbox").blur();
    $("#searchbox").css('width', '40%');
    $("#searchbox").css('border-bottom', '1px solid #999eaa');
    $('.search-arrow').css("display", "none");
}