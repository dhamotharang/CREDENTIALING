$(document).ready(function () {
    $("[title=Filter]").addClass('MemberSearchfilterBtn');

    var AtTheRateData = ["Member Name", "Provider Name", "Facility Name", "Insurance Name", "Payee Name", "Payer Name", "Sender", "Receiver"];
    var HashTagData = ["Member ID", "NPI Number", "Ref Number", "Auth ID", "Claim Number", "Account No", "Payee ID", "Check Number", "Clearing House ID", "Taxonomy"];
    var listCount = false;

    $('.fa').css('cursor', 'pointer');

    //SHOW FILTERS
    $('.MemberSearchfilterBtn').on('click', function () {
        ShowSearchFilters('MemberSearchfilterBtn', 'resultTableHeaderGlobal');
    })

    // FILTERING:
    $("#resultTableHeaderGlobal .filer-search").keyup(function () {
        var n = $(this).parent().parent().prevAll().length;
        var data = this.value.toUpperCase().split(" ");
        var allRows = $("#searhcResultBody").find("tr");
        if (this.value == "") {
            allRows.show();
            $('#resultRealCount').empty().append(getCountOfResultOnFilter(allRows));
            return;
        }
        allRows.hide();
        var filteredRows = allRows.filter(function (i, v) {
            for (var d = 0; d < data.length; ++d) {
                if ($(this).children('td').eq(n).text().toUpperCase().indexOf(data[d]) > -1) {
                    return true;
                }
            }
            return false;
        });
        filteredRows.show();
        $('#resultRealCount').empty().append(getCountOfResultOnFilter(filteredRows));
    });
    function getCountOfResultOnFilter(allRows) {
        return allRows.length;
    }

    // SORTING:
    var f_all = 1;
    var n = 0;
    var prev_n = 0;
    $("#resultTableHeaderGlobal .fa").click(function () {
        f_all *= -1;
        n = $(this).parent().parent().prevAll().length; // CURRENT COLUMN POSITION
        if (prev_n != n) {
            f_all = -1;
            $("#resultTableHeaderGlobal .fa").removeClass('fa-sort-asc fa-sort-desc').addClass('fa-sort').css('color', 'white'); //SORT ICONS
        }
        sortTable(f_all, n);
        if (n == prev_n) {
            if ($(this).hasClass('fa-sort')) {
                $(this).removeClass('fa-sort').addClass('fa-sort-asc').css('color', 'black');
            }
            else if ($(this).hasClass('fa-sort-asc')) {
                $(this).removeClass('fa-sort-asc').addClass('fa-sort-desc').css('color', 'black');
            }
            else {
                $(this).removeClass('fa-sort-desc').addClass('fa-sort-asc').css('color', 'black');
            }
        }
    });

    function sortTable(f, n) {
        var rows = $('#searhcResultBody tr').get();
        rows.sort(function (a, b) {
            var A = getVal(a);
            var B = getVal(b);
            if (A > B) {
                return -1 * f;
            }
            if (A < B) {
                return 1 * f;
            }
            return 0;
        });
        function getVal(elm) {
            var v = $(elm).children('td').eq(n).text().toUpperCase();
            if ($.isNumeric(v)) {
                v = parseInt(v, 10);
            }
            return v;
        }
        $.each(rows, function (index, row) {
            $('#searhcResultBody').append(row);
        });
        prev_n = n;
    }

    $('#searchBox').click(function () {
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
            $('.search_wrapper').removeClass("search_wrapper_border");
            $('.search-arrow').css("display", "none");
            $('#searchBox').attr('placeholder', 'Search Provider,Plan,Group/IPA');
            resetscreen();
        } else {
            resetscreen();
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
            removeList();
            listCount = false;
        }
        else {
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
                    }
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
        }
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
    }
    function removeList() {
        $('#dropDown').remove();
    }

    /*on load create table*/


    $('[data-toggle="tooltip"]').tooltip();//For tooltip
    // for displaying previous serach result

    $(".custom-cls-btn").click(function () {
        SResultClose();
    });
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
            }
        }
    });

    /* drop down selected value */
    $(".dropdown-menu li a").click(function () {
        $(this).parents(".search-arrow").find('.dropdown-toggle').html($(this).text() + ' <span class="caret"></span>');
        $(this).parents(".search-arrow").find('.dropdown-toggle').val($(this).data('value'));
    });

    $('.dropdown-submenu a.test').on("click", function (e) {
        $(this).next('ul').toggle();
        e.stopPropagation();
        e.preventDefault();
    });

})

function hasNumbers(t) {
    return /\d/.test(t);
};

function LoadTable() {
    var val = $('#searchBox').val();
    var isMemberId = false;
    if (val.length > 0) val = val.trim();
    if (val.length >= 2) {
        if (val.substring(0, 2).toLowerCase() == "ul") {
            isMemberId = true;
        }
    }
    if (hasNumbers(val) || isMemberId) {
        var field = "MemberId";
    }
    else {
        var field = "LastName";
    }
    SearchGlobalMembersByFactors(field, val);
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
    LoadTable();
}
// For Close Event
function SResultClose() {
    $('#searchBox').val('');
    $('.search-result-overview').hide();
    $('.searchResult').hide("clip", 500);
    $('body').css('overflow-y', 'scroll');
    $("#searchBox").blur();
    $('.search_btn').hide(100);
    $(".search_wrapper").css('width', '15%');
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

function SearchGlobalMembersByFactors(field, val) {
    TabManager.showLoadingSymbol('search-results');
    var filteredResult;
    if (field == "MemberId") {
        filteredResult = filterMembersByParticularField(val, "SubscriberID");
    }
    if (field == "LastName") {
        filteredResult = filterMembersByParticularField(val, "LastName");
    }
}
var AllMembers = [];
function filterMembersByParticularField(input, field) {

    $.ajax({
        url: '/Areas/CredAxis/Resources/ServiceData/ProvidersData.js',
        success: function (response) {
            AllMembers = JSON.parse(response);
            //for (var mem in AllMembers) {
            //    if (AllMembers[mem].Provider && AllMembers[mem].Provider.ProviderPracticeLocationAddress) {
            //        if (AllMembers[mem].Provider.ProviderPracticeLocationAddress.MobileNumber) {
            //            AllMembers[mem].Provider.ProviderPracticeLocationAddress.MobileNumber = AllMembers[mem].Provider.ProviderPracticeLocationAddress.MobileNumber.replace(/\D/g, '');
            //            AllMembers[mem].Provider.PhoneNumber = AllMembers[mem].Provider.ProviderPracticeLocationAddress.MobileNumber.slice(-10);
            //        }
            //    }
            //}
            var AllProviders = [];
            for (mem in AllMembers) {
                var prov = {};
                prov.NPI = AllMembers[mem].NPINumber;
                prov.LastName = AllMembers[mem].LastName;
                prov.FirstName = AllMembers[mem].FirstName;
                if (AllMembers[mem].ProviderLevel) prov.Level = AllMembers[mem].ProviderLevel;
                else prov.Level = "-";
                prov.Type = AllMembers[mem].Titles[0];

                AllProviders.push(prov);
            }
            processProviderResults(input, field, AllProviders);
        }
    });
}

function processProviderResults(input, field, list) {
    var filteredResult = [];
    //if (field == "SubscriberID") {
    //    $.each(list, function (key, value) {
    //        var pi = value.Member.MemberMemberships[0].Membership;
    //        $.each(pi, function (k, v) {
    //            if (k == field && v != null) {
    //                if (v.toUpperCase().indexOf(input.toUpperCase()) > -1) {
    //                    filteredResult.push(value);
    //                }
    //            }
    //        });
    //    });
    //}
    //if (field == "LastName" || field == "FirstName" || field == "DOB") {
    //    $.each(list, function (key, value) {
    //        var pi = value.Member.PersonalInformation;
    //        $.each(pi, function (k, v) {
    //            if (k == field && v != null) {
    //                if (v.toUpperCase().indexOf(input.toUpperCase()) > -1) {
    //                    filteredResult.push(value);
    //                }
    //            }
    //        });
    //    });
    //}
    exportMemberResults(list);
}

function exportMemberResults(filteredResult) {
    $("#resultTableHeaderGlobal .fa").removeClass('fa-sort-asc fa-sort-desc').addClass('fa-sort').css('color', 'white'); //SORT ICONS
    $('#resultRealCount').text(filteredResult.length);
    if (filteredResult.length == 0) {
        $('#noRealResultFound').show();
        $('#resultsTable').hide();
    }
    else {
        $('#noRealResultFound').hide();
        $('#resultsTable').show();
    }
    $('#searhcResultBody').html("");
    var trTagEnd = "</tr>"
    var tdTagEnd = "</span></td>"
    for (var i = 0; i < filteredResult.length; i++) {
        var trTag = '<tr class="tab-navigation" onclick="SResultClose();" data-tab-action="Provider Profile" data-tab-title="Profile" data-tab-floatmenu="true" data-tab-issubtab="true" data-tab-defaultparenttab="Provider" data-tab-autoflush="true" data-tab-usertype="Provider" data-tab-userid="' + filteredResult[i].NPI + '" data-tab-path="~/Areas/CredAxis/Views/ProviderProfile/_ProviderProfile.cshtml" data-tab-container="innerTabContainer" data-tab-parentcontainer="partialBodyContainer" data-tab-parenttabtitle="' + filteredResult[i].FirstName + ' ' + filteredResult[i].LastName + '" data-tab-hassubtabs="false" data-tab-parenttabpath="~/Areas/CredAxis/Views/ProviderProfile/_ProviderHeader.cshtml"  data-tab-float-menu-path="~/Areas/CredAxis/Views/Shared/_FloatMenu.cshtml" data-tab-parenttabrenderingarea="headerArea" data-tab-memberdata=\'' + JSON.stringify(filteredResult[i]) + '\'>'
        //var trTag = '<tr class="tab-navigation" onclick="SResultClose();" data-tab-action="Summary" data-tab-title="Summary" data-tab-floatmenu="true" data-tab-issubtab="true" data-tab-defaultparenttab="Provider" data-tab-autoflush="true" data-tab-usertype="Provider" data-tab-userid="' + filteredResult[i].NPI + '" data-tab-path="~/Areas/CredAxis/Views/ProviderProfile/OtherTabs/_summary.cshtml" data-tab-container="innerTabContainer" data-tab-parentcontainer="partialBodyContainer" data-tab-parenttabtitle="' + filteredResult[i].FirstName + ' ' + filteredResult[i].LastName + '" data-tab-hassubtabs="false" data-tab-parenttabpath="~/Areas/CredAxis/Views/ProviderProfile/_ProviderHeader.cshtml"  data-tab-float-menu-path="~/Areas/CredAxis/Views/Shared/_FloatMenu.cshtml" data-tab-parenttabrenderingarea="headerArea" data-tab-memberdata=\'' + JSON.stringify(filteredResult[i]) + '\'>'
        $('#searhcResultBody').append(trTag + "<td class='colBody-1'><span class='styled-input'>" + filteredResult[i].NPI + tdTagEnd
            + "<td class='colBody-2'><span class='styled-input'>" + filteredResult[i].LastName + tdTagEnd
            + "<td class='colBody-3'><span class='styled-input'>" + filteredResult[i].FirstName + tdTagEnd
            + "<td class='colBody-1'><span class='styled-input'>" + filteredResult[i].Level + tdTagEnd
            + "<td class='colBody-2'><span class='styled-input'>" + filteredResult[i].Type + tdTagEnd
            //+ "<td class='colBody-2'><span class='styled-input'>" + filteredResult[i].Type + tdTagEnd
            //+ "<td class='colBody-3'><span class=''><span class='profileActionColor'>Profile</span>&nbsp;&nbsp;<span class='profileActionColor'>PSV</span>&nbsp;&nbsp;<span class='profileActionColor'>Credentialing</span>" + tdTagEnd
);
    };
    $('#searchBodyDiv td').css('font-weight', 'bold');
    $('#searchBodyDiv td span').css({
        'overflow': 'hidden',
        'text-overflow': 'ellipsis',
        'white-space': 'nowrap'
    });
    TabManager.hideLoadingSymbol();
}