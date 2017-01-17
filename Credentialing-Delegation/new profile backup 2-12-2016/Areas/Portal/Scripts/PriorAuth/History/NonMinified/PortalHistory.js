
$(function () {
    //  $('#innerTabContainer').css('overflow', 'hidden');
    $('#QueueName').show().text('Portal');
    $("[title=Filter]").addClass('MemberSearchfilterBtn');
    $('#historyBody').css('overflow', 'hidden')
    $('.historyTableBodyDivision').css('height', ($(window).height() - 162));
    populateHistoryTable();
    var urlLink = "/Resources/Data/jsonForPortalHistory.txt";
    var Url = "/Resources/Data/PortalAuthHistory.txt";
    //Accessing the Data
    var PortalAuthHistoryJSON = [];
    $.ajax({
        url: urlLink,
        success: function (response) {
            PortalAuthHistoryJSON = JSON.parse(response);
            populateHistoryTable(PortalAuthHistoryJSON);
        }
    });

    var SubAuthHistory = [];
    $.ajax({
        url: Url,
        success: function (response) {
            SubAuthHistory = JSON.parse(response);
        }
    });


    //SHOW FILTERS
    $('.MemberSearchfilterBtn').on('click', function () {
        ShowSearchFilters('MemberSearchfilterBtn', 'AuthHistoryHeader');
    })

    $("#historyListExpand1").hide();

    $('.historyTableBodyDivision').off('click', '.subTableBody1').on('click', '.subTableBody1', function () {
        var row_index = $(this).parent().index();

        if ($(this).find("i").hasClass('fa-arrow-down')) {
            $(this).closest('tr').next().remove();
            $(this).closest('tr').removeAttr("style");
            //$(this).closest("tr").next().children().children().children().children().children().children().closest(".cols-1").css("background-color", "#708bb8");
        }
        else {
            var theHistoryDataList = '<tr class="text-uppercase">' +
                                       '<td colspan="17" class="tablePadding">' +
                                           '<table class="table table-striped custom-table-striped margin_bottom_zero margin_left_zero theme1_table custom-thead-back custom-thead-font custom-tbody tableLayer tablestyles">' +
                                               '<thead class="theme_thead"><tr>' +
                                                   '<th class="withfilter cols-1-1" rowspan="1" colspan="1"> <span class="styled-input"><input class="filer-search" type="text" required /> <label>APP DATE</label>     <span></span> </span> </th>' +
                                                   '<th class="withfilter cols-2-1" rowspan="1" colspan="1"> <span class="styled-input"><input class="filer-search" type="text" required /> <label>FROM DATE</label>   <span></span> </span> </th>' +
                                                   '<th class="withfilter cols-3-1" rowspan="1" colspan="1"> <span class="styled-input"><input class="filer-search" type="text" required /> <label>TO DATE</label>     <span></span> </span> </th>' +
                                                   '<th class="withfilter cols-4-1" rowspan="1" colspan="1"> <span class="styled-input"><input class="filer-search" type="text" required /> <label>SVC PROVIDER</label><span></span> </span> </th>' +
                                                   '<th class="withfilter cols-5-1" rowspan="1" colspan="1"> <span class="styled-input"><input class="filer-search" type="text" required /> <label>PRIMARY DX</label>  <span></span> </span> </th>' +
                                                   '<th class="withfilter cols-6-1" rowspan="1" colspan="1"> <span class="styled-input"><input class="filer-search" type="text" required /> <label>DX DESC</label>     <span></span> </span> </th>' +
                                                   '<th class="withfilter cols-6-1" rowspan="1" colspan="1"> <span class="styled-input"><input class="filer-search" type="text" required /> <label>SVC REQUESTED</label><span></span> </span> </th>' +
                                                   '<th class="withfilter cols-8-1" rowspan="1" colspan="1"> <span class="styled-input"><input class="filer-search" type="text" required /> <label>PROC CODE</label>   <span></span> </span> </th>' +
                                                   '<th class="withfilter cols-9-1" rowspan="1" colspan="1"><span class="styled-input"><input class="filer-search" type="text" required /> <label>PROC DESC</label>    <span></span> </span> </th>' +
                                                   '<th class="withfilter cols-10-1" rowspan="1" colspan="1"><span class="styled-input"><input class="filer-search" type="text" required /> <label>REQ UNITS</label>   <span></span> </span> </th>' +
                                       '</tr></thead>' +
                                       '<tbody class="background_white" id="authHistoryListData' + row_index + '">' +
                                       '</tbody>' +
                                       '</table>' +
                                       '</td>' +
                                   '</tr>'

            var indexOf = $(this).index();
            //$(this).closest("tr").css({ 'border-left': '3px solid #f67a71', 'border-bottom': '3px outset rgba(140, 147, 152, 0.54)', 'border-left': '3px groove rgba(140, 147, 152, 0.55)' });
            $(this).closest('tr').after(theHistoryDataList);
            if ($(this).find("i").hasClass('fa-arrow-right')) {
                populateHistoryTableList(row_index);
            }
            $(this).closest('tr').next().css("background-color", "transparent");
            $(this).closest('tr').next().children().find('tbody').css("background-color", "white !important");
        }
        $(this).find("i").toggleClass('fa-arrow-down fa-arrow-right');
        $(this).find("span").toggleClass('btn-success btn-danger');
    })

    // POPULATE TABLE:
    function populateHistoryTable(AuthHistoryData) {
        $.each(AuthHistoryData, function (index, value) {
            $('#PortalAuthHistoryTable').append(
                      '<tr>' +
                        '<td class="cols1 td-blue subTableBody1"><span class="btn  btn-success btn-xs"><i class="fa fa-arrow-right"></i></span></td>' +
                        '<td class="cols2">' + '<label class="label label-primary abvStyleClass">' + value.ABV + '</label>' + '</td>' +
                        '<td class="cols3">' + '-' + '</td>' +
                        '<td class="cols4">' + value.REFNO + '</td>' +
                        '<td class="cols5">' + value.FROM + '</td>' +
                        '<td class="cols4">' + value.TO + '</td>' +
                        '<td class="cols7">' + '09/07/1967' + '</td>' +
                        '<td class="cols8">' + value.PROVIDER + '</td>' +
                        '<td class="cols6">' + value.PRVID + '</td>' +
                        '<td class="cols10">' + value.MBRID + '</td>' +
                        '<td class="cols6">' + value.MBRNAME + '</td>' +
                        'html before' + (value.REQUEST == 'EXPEDITED' ?
                            '<td class="cols7"><div class="danger expeditedStyle">' + value.REQUEST + '</div></td>' :
                            '<td class="cols7"><div class="ok">' + value.REQUEST + '</div></td>') + 'more html' +
                        '<td class="cols13 text-center">' + value.POS + '</td>' +
                        '<td class="cols14">' + value.DX + '</td>' +
                        '<td class="cols13">' + value.UNITS + '</td>' +
                        '<td class="cols12">' + value.STATUS + '</td>' +
                        '<td class="cols15"> <input type="button" value="VIEW"  class="btn btn-primary btn-xs tab-navigation member-actions" data-tab-val="viewAuth"  data-tab-action="View Portal Auth" data-tab-title="View Portal Auth" data-tab-parentcontainer="partialBodyContainer" data-tab-floatmenu="true" data-tab-container="innerTabContainer" data-tab-autoflush="false" data-tab-issubtab="true" data-tab-path="~/Areas/UM/Views/PortalAuth/_ViewPortalAuth.cshtml" data-tab-defaultparenttab="Member" data-tab-userid="' + $('#MemberID').text() + '" data-tab-usertype="Member"/> <input type="submit" value="COPY" class="btn btn-primary btn-xs"/></td>' +
                      '</tr>'
               );
        });
        setTimeout(function () {
            $('[data-toggle="popover"]').popover(); // CREATE POPOVERS
        }, 600);
    };

    function populateHistoryTableList(indx) {
        $.each(SubAuthHistory, function (index, values) {
            $('#authHistoryListData' + indx).append(
                      '<tr>' +
                        '<td class="cols1-1">' + '09/07/16' + '</td>' +
                        '<td class="cols2-1">' + values.FROM + '</td>' +
                        '<td class="cols3-1">' + values.TO + '</td>' +
                        '<td class="cols4-1">' + values.PROVIDER + '</td>' +
                        '<td class="cols5-1">' + values.DX + '</td>' +
                        '<td class="cols6-1">' + "FELTY'S SYNDROME" + '</td>' +
                        '<td class="cols6-1">' + 'INOFFICE' + '</td>' +
                        '<td class="cols8-1">' + '0108T' + '</td>' +
                        '<td class="cols9-1">' + 'INFECTIOUS DIS HCV' + '</td>' +
                        '<td class="cols10-1">' + '1' + '</td>' +
                      '</tr>'
              );
        });
    };

    //AJAX CALL TO A PARTIAL VIEW
    $('.callViews').click(function () {
        openContainer(this, 'viewAuthHistory', 'VIEW HISTORY');
        $.ajax({
            url: '/Home/GetView',
            data: {},
            cache: false,
            type: "POST",
            dataType: "html",
            success: function (data, textStatus, XMLHttpRequest) {
                SetData(data);
            }
        });
    });

    // ADJUST COLUMN HEIGHT ACCORDING TO SCREEN RESOLUTION:
    $(window).resize(function () {
        $('.historyTableBodyDivision').css('height', ($(window).height() - 162));
    });
    // REDIRECTS TO INDEX PAGE FOR HOME:
    function redirectToHomeIndex() {
        window.location.pathname = '/Home/Index';
    };
});

$('#UMRunningFooter').hide();

var memberData = TabManager.getMemberData();
setMemberHeaderData(memberData);
//-----------------Search history-----------------------------------------
$(document).ready(function () {
    var AtTheRateData = ["Member Name", "Provider Name", "Facility Name", "Insurance Name", "Payee Name", "Payer Name", "Sender", "Receiver"];
    var HashTagData = ["Member ID", "NPI Number", "Ref Number", "Auth ID", "Claim Number", "Account No", "Payee ID", "Check Number", "Clearing House ID", "Taxonomy"];
    var listCount = false;

    $('#searchBox1').click(function () {
        //$('.search_wrapper').css('border-bottom', '2px solid #01c101');
        $('.search_wrapper1').addClass("search_wrapper_border1");
        //$('.search_wrapper1').css('width', '53%');
        $('.search_wrapper1').css('border-bottom', '4px solid rgb(9, 65, 122)');
        //$('.search_btn1').show(100);
    })

    // when clicked other than input
    $("#searchBox1").blur(function () {
        if ($('#searchBox1').val() === '' && !$('.keyWord1').is(':visible')) {
            $('.smart_search_word1').hide();
            $('.keyWord1').hide();
            $('.keyWord1').html('');

            $('.search-result-overview1').hide();
            $('.searchResult1').hide("clip", 500);
            //$('body').css('overflow-y', 'scroll');
            $('.search_btn1').hide(100);
            //$(".search_wrapper1").css('width', '50%');
            $('.search_wrapper1').css('border-bottom', '1px solid rgb(9, 65, 122)');
            //$(".search_wrapper").css('border-bottom', '1px solid rgba(255,255,255,0.80)');
            $('.search_wrapper1').removeClass("search_wrapper_border1");
            $('.search-arrow1').css("display", "none");
            $('#searchBox1').attr('placeholder', 'Search History')
        } else {
            //$("#searchBox").focus();            
        }

    });
    //setSideMenu();
    setTimeout(function () {
        $('#refnum').hide();
        $('#reftitle').hide();
    }, 1500)
});