/*---------------------------------------------------------/
/                AUTHOR: ARNAB SAHA                        /
/---------------------------------------------------------*/

$(function () {
    $('#historyBody').css('overflow', 'hidden')
    $('.historyTableBodyDivision').css('height', ($(window).height() - 162));
    populateHistoryTable();

    $("#historyListExpand1").hide();

    $('.subTableBody1').click(function () {
        var row_index = $(this).parent().index();

        if ($(this).find("i").hasClass('fa-minus')) {
            $(this).closest('tr').next().remove();
            $(this).closest('tr').removeAttr("style");
            //$(this).closest("tr").next().children().children().children().children().children().children().closest(".cols-1").css("background-color", "#708bb8");
        }
        else {
            var theHistoryDataList = '<tr class="text-uppercase">' +
                                       '<td colspan="15" class="tablePadding">' +
                                           '<table class="table table-condensed table-striped wrap-words pre-scrollable tableLayer tablestyles">' +
                                               '<thead class="thead_access_blue"><tr>' +
                                                   '<th class="withfilter cols-1-1" rowspan="1" colspan="1"> <span class="styled-input"><input class="filer-search" type="text" required /> <label>EXP DOS</label>     <span></span> <i class="fa fa-sort"></i></span> </th>' +
                                                   '<th class="withfilter cols-2-1" rowspan="1" colspan="1"> <span class="styled-input"><input class="filer-search" type="text" required /> <label>FROM DATE</label>   <span></span> <i class="fa fa-sort"></i></span> </th>' +
                                                   '<th class="withfilter cols-3-1" rowspan="1" colspan="1"> <span class="styled-input"><input class="filer-search" type="text" required /> <label>TO DATE</label>     <span></span> <i class="fa fa-sort"></i></span> </th>' +
                                                   '<th class="withfilter cols-4-1" rowspan="1" colspan="1"> <span class="styled-input"><input class="filer-search" type="text" required /> <label>SVC PROVIDER</label><span></span> <i class="fa fa-sort"></i></span> </th>' +
                                                   '<th class="withfilter cols-5-1" rowspan="1" colspan="1"> <span class="styled-input"><input class="filer-search" type="text" required /> <label>PRIMARY DX</label>  <span></span> <i class="fa fa-sort"></i></span> </th>' +
                                                   '<th class="withfilter cols-6-1" rowspan="1" colspan="1"> <span class="styled-input"><input class="filer-search" type="text" required /> <label>DX DESC</label>     <span></span> <i class="fa fa-sort"></i></span> </th>' +
                                                   '<th class="withfilter cols-7-1" rowspan="1" colspan="1"> <span class="styled-input"><input class="filer-search" type="text" required /> <label>POS</label>         <span></span> <i class="fa fa-sort"></i></span> </th>' +
                                                   '<th class="withfilter cols-8-1" rowspan="1" colspan="1"> <span class="styled-input"><input class="filer-search" type="text" required /> <label>PROC CODE</label>   <span></span> <i class="fa fa-sort"></i></span> </th>' +
                                                   '<th class="withfilter cols-9-1" rowspan="1" colspan="1"><span class="styled-input"><input class="filer-search" type="text" required /> <label>PROC DESC</label>   <span></span> <i class="fa fa-sort"></i></span> </th>' +
                                                   '<th class="withfilter cols-10-1" rowspan="1" colspan="1"><span class="styled-input"><input class="filer-search" type="text" required /> <label>REQ UNITS</label>   <span></span> <i class="fa fa-sort"></i></span> </th>' +
                                                   '<th class="withfilter cols-11-1" rowspan="1" colspan="1"><span class="styled-input"><input class="filer-search" type="text" required /> <label>AUTH UNITS</label>  <span></span> <i class="fa fa-sort"></i></span> </th>' +
                                       '</tr></thead>' +
                                       '<tbody id="authHistoryListData' + row_index + '">' +
                                       '</tbody>' +
                                       '</table>' +
                                       '</td>' +
                                   '</tr>'

            var indexOf = $(this).index();
            $(this).closest("tr").css({ 'border-left': '3px solid #f67a71', 'border-bottom': '3px outset rgba(140, 147, 152, 0.54)', 'border-left': '3px groove rgba(140, 147, 152, 0.55)' });
            $(this).closest('tr').after(theHistoryDataList);
            if ($(this).find("i").hasClass('fa-plus-circle')) {
                populateHistoryTableList(row_index);
            }
            $(this).closest('tr').next().css("background-color", "transparent");
            $(this).closest('tr').next().children().find('tbody').css("background-color", "white !important");
        }
        $(this).find("i").toggleClass('fa-minus fa-plus-circle');

    })

    // POPULATE TABLE:
    function populateHistoryTable() {
        $.each(AuthHistoryData, function (index, value) {
            $('#authHistoryTable').append(
                      '<tr>' +
                        '<td class="cols1 td-blue subTableBody1"><span class="btn btn-xs btn-default"><i class="fa fa-plus-circle"></i></span></td>' +
                        '<td class="cols2">' + '<label class="label label-primary">' + value.ABV + '</label>' + '</td>' +
                        '<td class="cols3">' + value.REFNO + '</td>' +
                        '<td class="cols4">' + value.FROM + '</td>' +
                        '<td class="cols5">' + value.TO + '</td>' +
                        '<td class="cols6">' + value.SVC + '</td>' +
                        'html before' + (value.REQUEST == 'EXPEDITED' ?
                            '<td class="cols7"><div class="danger">' + value.REQUEST + '</div></td>' :
                            '<td class="cols7"><div class="ok">' + value.REQUEST + '</div></td>') + 'more html' +
                        '<td class="cols8">' + value.FACILITY + '</td>' +
                        '<td class="cols9">' + value.AUTH + '</td>' +
                        '<td class="cols10">' + value.DOS + '</td>' +
                        '<td class="cols11">' + value.UNITS + '</td>' +
                        '<td class="cols12">' + value.STATUS + '</td>' +
                        '<td class="cols13">' + value.POS + '</td>' +
                        '<td class="cols14">' + value.DX + '</td>' +
                        '<td class="cols15"> <input type="button" value="VIEW" class="view-button" /> <input type="submit" value="COPY" class="copy-button"/> <input type="submit" value="EDIT" class="edit-button"/></td>' +
                      '</tr>'
               );
        });
    };

    function populateHistoryTableList(indx) {
        $.each(ViewAuthHistoryData, function (index, values) {
            $('#authHistoryListData' + indx).append(
                      '<tr>' +
                        '<td class="cols1-1">' + values.EXPDOS + '</td>' +
                        '<td class="cols2-1">' + values.FROM + '</td>' +
                        '<td class="cols3-1">' + values.TO + '</td>' +
                        '<td class="cols4-1">' + values.SVC + '</td>' +
                        '<td class="cols5-1">' + values.PRIMARYDX + '</td>' +
                        '<td class="cols6-1">' + values.DXDESC + '</td>' +
                        '<td class="cols7-1">' + values.POS + '</td>' +
                        '<td class="cols8-1">' + values.PROCCODE + '</td>' +
                        '<td class="cols9-1">' + values.PROCDESC + '</td>' +
                        '<td class="cols10-1">' + values.REQUNITS + '</td>' +
                        '<td class="cols11-1">' + values.AUTHUNITS + '</td>' +
                      '</tr>'
              );
        });
    };

    //AJAX CALL TO A PARTIAL VIEW
    $('.view-button').click(function () {
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
    },1500) 
})