$(document).ready(function () {
    var AtTheRateData = ["Member Name", "Provider Name", "Facility Name", "Insurance Name", "Payee Name", "Payer Name", "Sender", "Receiver"];
    var HashTagData = ["Member ID", "NPI Number", "Ref Number", "Auth ID", "Claim Number", "Account No", "Payee ID", "Check Number", "Clearing House ID", "Taxonomy"];
    var listCount = false;

    $('#searchBox').click(function () {
        //$('.search_wrapper').css('border-bottom', '2px solid #01c101');
        $('.search_wrapper').addClass("search_wrapper_border");
        $('.search_wrapper').css('width', '35%');
        reArrangeSearch();
        $('.search_btn').show(100);
    })

    $('#searchBtn').click(function () {
        if ($('#searchBox').val() !== '') {
            SResultOpen();
        }
    })


    // when clicked other than input
    $("#searchBox").blur(function () {
        if ($('#searchBox').val() === '' && !$('.keyWord').is(':visible')) {
            $('.smart_search_word').hide();
            $('.keyWord').hide();
            $('.keyWord').html('');

            $('.search-result-overview').hide();
            $('.searchResult').hide("clip", 500);
            $('body').css('overflow-y', 'scroll');
            $('.search_btn').hide(100);
            $(".search_wrapper").css('width', '15%');
            //$(".search_wrapper").css('border-bottom', '1px solid rgba(255,255,255,0.80)');
            $('.search_wrapper').removeClass("search_wrapper_border");
            $('.search-arrow').css("display", "none");
            $('#searchBox').attr('placeholder', 'Search')
            resetscreen();
        } else {
            resetscreen();
            //$("#searchBox").focus();            
        }

    });

    $('#searchBox').on('keyup', function (e) {
        var $searchValue = $('#searchBox').val();
        var filter = $searchValue.slice(1).toUpperCase();
        try {
            var lis = document.getElementById("dropDown").getElementsByTagName("li");
            for (var i = 0; i < lis.length; i++) {
                var name = lis[i].innerHTML;
                if (name.toUpperCase().indexOf(filter) == 0) {
                    lis[i].style.display = 'list-item';

                }

                else {
                    lis[i].style.display = 'none';
                    lis[i].className = "";
                }

            }

        } catch (e) {

        }

        setTimeout(function () {
            if ($('#dropDown li:visible').hasClass("active")) {

            } else {
                $('#dropDown li.active').removeClass("active");
                $("#dropDown li:visible:first").addClass("active");
            }
        }, 0)


        var $searchValue = $('#searchBox').val();
        var $searchLength = $searchValue.length;
        if ($searchLength === 0) {
            //    $('.keyWord').hide();
            //    $('.keyWord').html('');
            //    $('.smart_search_word').hide();
            removeList();
            // $('#searchBox').attr('placeholder', 'Search')
            listCount = false;
        } else {

            var FirstChar = $searchValue.charAt(0);

            if (FirstChar === '@' || FirstChar === '#') {
                if (FirstChar === '@') {
                    if (!listCount) {
                        appendToList(AtTheRateData);
                        listCount = true;
                    }
                }
                else if (FirstChar === '#') {
                    if (!listCount) {
                        appendToList(HashTagData);
                        listCount = true;
                    }
                } else {
                    if ($('.keyWord').html() != '' && (jQuery.inArray($('.keyWord').html(), AtTheRateData) || jQuery.inArray($('.keyWord').html(), HashTagData))) {
                        $('.smart_search_word').show();

                        //$('.smart_search_word').html('');
                    }
                    //$('.smart_search_word').hide();
                }
                $('.keyWord').html('');
                $('.smart_search_word').show();
                $('.keyWord').hide();
                if ($('.keyWord').html() === '' && !$('#dropDown').is(":visible")) {
                    $('.smart_search_word').hide();
                } else if ($('.keyWord').html() !== '' && $('#dropDown').is(":visible")) {
                    $('.smart_search_word').show();

                }
            }
        }
    })

    $('#searchBox').on('keydown', function (e) {
        var $searchValue = $('#searchBox').val();

        var FirstChar = $searchValue.charAt(0);

        var $current = $('#dropDown li.active');
        var $next;
        // backspace
        if (event.keyCode === 8 && $searchValue === '') {
            $('.keyWord').hide();
            $('.smart_search_word').hide();
            $('.keyWord').val('');
            $('#searchBox').attr('placeholder', 'Search')
        }

        // enter key
        if (event.keyCode == 13 && (FirstChar === '@' || FirstChar === '#')) {
            event.preventDefault();
            $('.keyWord').html($current.text());
            changePlaceholder($current.text());
            $('.keyWord').show();
            $('.smart_search_word').show();
            $("#dropDown").remove();
            $('#searchBox').val('');
            reArrangeSearch();

        }
        //up arrow
        if (event.keyCode == 38 && (FirstChar === '@' || FirstChar === '#')) {
            event.preventDefault();

            $next = $current.prev();
        }
        // down arrow
        if (event.keyCode == 40 && (FirstChar === '@' || FirstChar === '#')) {
            event.preventDefault();
            do {
                $next = $current.next();
                if ($next.is(':hidden')) {
                    $current = $current.next();
                }
            } while ($next.is(':hidden'))
            //$next = $current.next();
        }


        //var SearchInput = $('#searchBox');
        //SearchInput.val(SearchInput.val());
        //var strLength = SearchInput.val().length;
        //SearchInput.focus();
        //SearchInput[0].setSelectionRange(strLength, strLength);

        try {
            if ($next.length > 0) {
                $('#dropDown li').removeClass('active');
                $next.addClass('active');
            }
        } catch (e) {

        }

        if ($("#dropDown").is(':visible') && $("#dropDown li:visible").hasClass("active")) {

        } else {
            $('#dropDown li.active').removeClass("active");
            $("#dropDown li:visible:first").addClass("active");
        }



    })

    $('.smart_search_word').delegate("ul#dropDown li", "mouseover", function () {
        $("li.active").removeClass("active");
        $(this).addClass("active");

    })

    $('.smart_search_word').delegate("ul#dropDown li.active", "click", function () {
        $('.keyWord').html($(this).text());
        changePlaceholder($(this).text());
        $('.keyWord').show();
        $('.smart_search_word').show();
        $("#dropDown").remove();
        $('#searchBox').focus();
        $('#searchBox').val('');

        reArrangeSearch();

    })



    function changePlaceholder(newPlaceHolder) {
        $('#searchBox').attr('placeholder', 'Search by ' + newPlaceHolder)
    }

    function appendToList(data) {
        var list = $(".smart_search_word").append('<ul id="dropDown"></ul>').find('ul');
        for (var i = 0; i < data.length; i++)
            list.append('<li>' + data[i] + '</li>');
        $('#dropDown li:first-child').addClass('active');

        //$('#dropDown li.active').removeClass("active");
        //$("#dropDown li:visible:first").addClass("active");
    }
    function removeList() {
        $('#dropDown').remove();
    }

    /*on load create table*/
    LoadTable();
  
    $('[data-toggle="tooltip"]').tooltip();//For tooltip
    // for displaying previous serach result

    $(".custom-cls-btn").click(function () {
        SResultClose();
    });
    // for buttons toogle
    //$('.menu-btn-toggle').click(function () {
    //    $(this).toggleClass('open');
    //    $('.option').toggleClass('scale-on');
    //});
    $(window).bind('keyup', function (event) {
        // esc event
        if (event.keyCode === 27) {
            $('.keyWord').hide();
            $('.keyWord').html('');
            SResultClose();
        }
        // enter event
        if (event.keyCode === 13) {
            if ($('#searchBox').val() != '' && $('#searchBox').val() !== undefined) {
                SResultOpen();

                // $('#serch-default-message').hide();


            }
        }
    });

    /* drop down selected value */
    $(".dropdown-menu li a").click(function () {
        $(this).parents(".search-arrow").find('.dropdown-toggle').html($(this).text() + ' <span class="caret"></span>');
        $(this).parents(".search-arrow").find('.dropdown-toggle').val($(this).data('value'));
    });



})
function LoadTable() {
    //var trTag = "<tr onclick='redirectToIndex();'>"
   
    var trTagEnd = "</tr>"
    //var tdTag = "<td class='col-lg-1'>";
    var tdTagEnd = "</span></td>"
    for (var i = 0; i < MemberData.length; i++) {
        var trTag = "<tr class='tab-navigation' onclick='SResultClose();' data-tab-action='Member Profile' data-tab-title='Profile' data-tab-floatMenu='true' data-tab-isSubTab='true' data-tab-defaultParentTab='Member' data-tab-isSubTab='true' data-tab-autoFlush='true' data-tab-userType='Member' data-tab-userId='" + MemberData[i].MemberID + "' data-tab-path='~/Views/MemberProfile/_MemberProfile.cshtml' data-tab-container='innerTabContainer' data-tab-parentContainer='partialBodyContainer' data-tab-parentTabTitle='" + MemberData[i].FirstName + " " + MemberData[i].LastName + "' data-tab-hasSubTabs='true'>"
        $('#searhcResultBody').append(trTag + "<td class='colBody-1'><span class='styled-input'>" + MemberData[i].MemberID + tdTagEnd
            + "<td class='colBody-2'><span class='styled-input'>" + MemberData[i].LastName + tdTagEnd
            + "<td class='colBody-3'><span class='styled-input'>" + MemberData[i].FirstName + tdTagEnd
            + "<td class='colBody-4'><span class='styled-input'>" + MemberData[i].DOB + tdTagEnd
            + "<td class='colBody-5'><span class='styled-input'>" + MemberData[i].Gender + tdTagEnd
            + "<td class='colBody-6'><span class='styled-input'>" + MemberData[i].PCP + tdTagEnd
            + "<td class='colBody-7'><span class='styled-input'>" + MemberData[i].PCPPhone + tdTagEnd
            + "<td class='colBody-8'><span class='styled-input'>" + MemberData[i].EffectiveDate + tdTagEnd
            + "<td class='colBody-9'><span class='styled-input'>" + MemberData[i].Address + tdTagEnd
            + "<td class='colBody-10'><span class='styled-input'>" + MemberData[i].City + tdTagEnd
            + "<td class='colBody-11'><span class='styled-input'>" + MemberData[i].State + tdTagEnd
            + "<td class='colBody-12'><span class='styled-input'>" + MemberData[i].Zip + tdTagEnd
            + trTagEnd);
    }
}

//Open Create Auth on <tr> Click
function redirectToIndex() {
    window.location.pathname = '/Home/Index';
};

// For Open Event
function SResultOpen() {
    $('#search-results').show();
    $('.search-word').html($('#searchBox').val());
    $('.searchResult').show("clip", 500);
    $("#searchbox").focus();
    $('#floatingmenu').show("puff", 500);
    $('body').css('overflow-y', 'hidden');
    $("#searchbox").css('width', '94%');
    $("#searchbox").css('border-bottom', '2px solid #01c101');
    $('.search-arrow').css("display", "block");
    $('.response-table').height($(window).height() - 150)
}
// For Close Event
function SResultClose() {
    $('#searchBox').val('');
    $('.search-result-overview').hide();
    $('.searchResult').hide("clip", 500);
    $('body').css('overflow-y', 'scroll');
    //$('#floatingmenu').hide();
    $("#searchBox").blur();
    $('.search_btn').hide(100);
    $(".search_wrapper").css('width', '15%');
    //$(".search_wrapper").css('border-bottom', '1px solid #4b6cb7');
    $('.search-arrow').css("display", "none");
}
function reArrangeSearch() {
    var totalAvailWidth = $('.search_wrapper').width();
    var SmartWordWidth = $('.smart_search_word').width();
    var SearchBtnWidth = $('.search_btn').width();
    $('.search_input').width(totalAvailWidth - (SmartWordWidth + SearchBtnWidth) - 5);
}


// for infinite scroll
jQuery(
  function ($) {
      $('#searchBodyDiv').on('scroll', function () {

          if ($(this).scrollTop() + $(this).innerHeight() >= $(this)[0].scrollHeight) {
             
              LoadTable();
          }
      })
  }
);