/// <reference path="../../../Views/ProviderBridge/BridgeProviderAccept/ConformationToAddProfile/_ProviderBody.cshtml" />
$(document).ready(function () {
    //TODO: Review line 3 to 54 and remove if it is not necessary
    $('#fullBodyContainer').css('overflow', 'hidden'); //DISABLE ADDITIONAL SCROLL
    $('.tableBodyDivision').css('height', ($(window).height() - 162)); // SET TABLE HEIGHT ACCORDING TO SCREEN RESOLUTION
    $('.total-count').css({ 'color': 'white', 'background-color': 'rgb(128, 128, 128)', 'transition': '0.4s' }); // CHECK ALL FILTER BUTTON ONLOAD
    $('.transparentLoadingDiv').hide();
    $(".leftPanelButtons").hide();
    $(".moveWork").show();
    $('#QueueName').hide().text('');


    // Controller Calling function


    //TODO: Review line below  for its significance and remove if it is not fulfilling any purpose
    $(".datePicker").datetimepicker({ format: 'MM/DD/YYYY' });

    // CALCULATE TOTAL, EXPEDITED & STANDARD REQUESTS:
    //TODO: Review the below code for its significance and remove it if it is not serving any purpose
    function calculateTotal(FacilityQueueJSON) {
        $("#total-count").empty();
        $('#total-count').append(Object.keys(FacilityQueueJSON).length); // ALL
        var expedited = 0, standard = 0, green = 0, yellow = 0, red = 0, grey = 0;
        $.each(FacilityQueueJSON, function (index, value) {
            if (value.REQUEST == 'Expedited'.toLowerCase()) {
                expedited++;
            }
            else if (value.REQUEST == 'Standard'.toLowerCase()) {
                standard++;
            }
            if (value.COLOR == "greentd") {
                green++
            }
            else if (value.COLOR == "yellowtd") {
                yellow++;
            }
            else if (value.COLOR == "redtd") {
                red++;
            }
            else if (value.COLOR == "greytd") {
                grey++;
            }
        });

        $("#total-expedited").empty();
        $("#total-standard").empty();
        $('#total-expedited').append(expedited); // EXPEDITED
        $('#total-standard').append(standard); // STANDARD
        $('.green-legend-description').append('(<b>' + green + '</b>)'); // GREEN
        $('.yellow-legend-description').append('(<b>' + yellow + '</b>)'); // YELLOW
        $('.red-legend-description').append('(<b>' + red + '</b>)'); // RED
        $('.grey-legend-description').append('(<b>' + grey + '</b>)'); // GREY
    };

    // TOOLTIPS:
    $('[data-toggle="tooltip"]').tooltip();


    //TODO: Review the below code for its significance and remove it if it is not serving any purpose
    // SET LEFT ON SCROLL FOR COLORED TD:
    $('.scrollable-division').scroll(function () {
        $('.greentdgradient, .redtdgradient, .greytdgradient, .yellowtdgradient, .whitetdgradient').css({
            'left': $(this).scrollLeft()
        });
    });

    // ADJUST TABLE HEIGHT ON RESIZING SCREEN RESOLUTION:
    $(window).resize(function () {
        $('.tableBodyDivision').css('height', ($(window).height() - 202));
    });
    /* REQUEST FILTERS */
    // ALL:
    $('.total-count').on('click', function () {

        $('.dropdown-btn').empty().append('All <span class="caret"></span>');
        $('div.scrollable-division').animate({ 'scrollLeft': $('div.scrollable-division').scrollLeft() - 1000 }, 100, 'swing');

        $('.pointing-arrow-standard').css({ 'border-bottom-color': 'rgba(255,255,255,0)' });
        // $('.total-standard').css({ 'color': '#319012', 'background-color': 'white' });

        $('.pointing-arrow-expedited').css({ 'border-bottom-color': 'rgba(255,255,255,0)' });
        //  $('.total-expedited').css({ 'color': 'rgb(230, 28, 28)', 'background-color': 'white' });

        $('.pointing-arrow-all').css({ 'border-bottom-color': 'rgba(255,255,255,1)' });
        //   $('.total-count').css({ 'color': 'white', 'background-color': 'rgb(128, 128, 128)' });

        restoreSortIconOnFilteration(); //RESET SORT ICON
        var queueType = $(this).attr('data-queue-type');
        var queuTab = $(this).attr('data-queue-tab');
        var data = { QueueType: queueType, QueueTab: queuTab }

        $('#intakeQueueTable').empty(); // EMPTY QUEUE TABLE BEFORE SEARCH RESULT
        $.ajax({
            url: "/UM/Queue/GetQueueDataonRequestType", // the method we are calling
            data: data,
            success: function (result) {

                $("#intakeQueueTable").html(result);
            },
            error: function (result) {


            }

        });
    });
    // EXPEDITED:
    $('.total-expedited').on('click', function () {

        $('.dropdown-btn').empty().append('All <span class="caret"></span>');
        $('div.scrollable-division').animate({ 'scrollLeft': $('div.scrollable-division').scrollLeft() - 1000 }, 100, 'swing');

        $('.pointing-arrow-standard').css({ 'border-bottom-color': 'rgba(255,255,255,0)' });
        // $('.total-standard').css({ 'color': '#319012', 'background-color': 'white' });

        $('.pointing-arrow-expedited').css({ 'border-bottom-color': 'rgba(255,255,255,1)' });
        // $('.total-expedited').css({ 'color': 'white', 'background-color': 'rgb(230, 28, 28)' });

        $('.pointing-arrow-all').css({ 'border-bottom-color': 'rgba(255,255,255,0)' });
        //  $('.total-count').css({ 'color': 'rgb(128, 128, 128)', 'background-color': 'white' });

        $('#intakeQueueTable').empty();
        //$('#intakeQueueTable').find('tr').not('.filterRows').remove();
        var queueType = $(this).attr('data-queue-type');
        var queuTab = $(this).attr('data-queue-tab');

        filterByRequest(queueType, queuTab, "Expedited");
    });
    // STANDARD:
    $('.total-standard').on('click', function () {

        $('.dropdown-btn').empty().append('All <span class="caret"></span>');
        $('div.scrollable-division').animate({ 'scrollLeft': $('div.scrollable-division').scrollLeft() - 1000 }, 100, 'swing');

        $('.pointing-arrow-standard').css({ 'border-bottom-color': 'rgba(255,255,255,1)' });
        //  $('.total-standard').css({ 'color': 'white', 'background-color': '#319012' });

        $('.pointing-arrow-expedited').css({ 'border-bottom-color': 'rgba(255,255,255,0)' });
        //    $('.total-expedited').css({ 'color': 'rgb(230, 28, 28)', 'background-color': 'white' });

        $('.pointing-arrow-all').css({ 'border-bottom-color': 'rgba(255,255,255,0)' });
        //   $('.total-count').css({ 'color': 'rgb(128, 128, 128)', 'background-color': 'white' });

        $('#intakeQueueTable').empty();
        //$('#intakeQueueTable').find('tr').not('.filterRows').remove();
        var queueType = $(this).attr('data-queue-type');
        var queuTab = $(this).attr('data-queue-tab');

        filterByRequest(queueType, queuTab, "Standard");

    });
    function appendTabContent(data, tabcontentid) {
        $('#' + tabcontentid).html(data);
        $('input.flat').iCheck({
            checkboxClass: 'icheckbox_square-green',
            radioClass: 'iradio_square-green'
        });
    }

    $('.innerTabsArea li a').on('click', function () {
        var url_path = $(this).attr('data-tab-path');
        $.ajax({
            type: 'GET',
            url: url_path,
            success: function (data) {
                appendTabContent(data, "queueTable");
            }
        })
    })
    // POPULATE FILTERED TABLE FOR REQUEST:
    function filterByRequest(queueType, queuTab, request) {
        restoreSortIconOnFilteration(); //RESET SORT ICON
        var data = { QueueType: queueType, QueueTab: queuTab }

        $('#intakeQueueTable').empty(); // EMPTY QUEUE TABLE BEFORE SEARCH RESULT
        $.ajax({
            url: "/UM/Queue/GetQueueDataonRequestType", // the method we are calling
            data: data,
            success: function (result) {

                $("#intakeQueueTable").html(result);
            },
            error: function (result) {


            }

        });
    }



    // RESET SORTING ICON ON FILTERING:
    function restoreSortIconOnFilteration() {
        $('.styled-input i').removeClass('fa-sort-desc fa-sort-asc').addClass('fa-sort').css('color', 'white');
    }

    // SORTING FUNCTION:
    function sortTable(f, n) {
        var rows = $('#intakeQueueTable  tr').get();
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
            $('#intakeQueueTable').append(row);
        });
        prev_n = n;
    }

    // SORTING EVENTS:
    var f_all = 1;
    var n = 0;
    var prev_n = 0;
    $(".facility-queue-sort").click(function () {
        f_all *= -1;
        n = $(this).parent().parent().prevAll().length; // CURRENT COLUMN POSITION
        if (prev_n != n) {
            f_all = -1;
            $(".facility-queue-sort").removeClass('fa-sort-asc fa-sort-desc').addClass('fa-sort').css('color', 'white'); //SORT ICONS
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


    //TODO: Review the below method if it is used or not
    // NEW FEATURES:
    function resetCSS() {
        // SCROLL BACK TO START:
        $('div.scrollable-division').animate({ 'scrollLeft': $('div.scrollable-division').scrollLeft() - 1000 }, 100, 'swing');

        // STANDARD CSS:
        $('.pointing-arrow-standard').css({ 'border-bottom-color': 'rgba(255,255,255,0)' });
        // $('.total-standard').css({ 'color': '#319012', 'background-color': 'white' });

        // EXPEDITED CSS:
        $('.pointing-arrow-expedited').css({ 'border-bottom-color': 'rgba(255,255,255,0)' });
        //$('.total-expedited').css({ 'color': 'rgb(230, 28, 28)', 'background-color': 'white' });

        // ALL CSS:
        $('.pointing-arrow-all').css({ 'border-bottom-color': 'rgba(255,255,255,0)' });
        //$('.total-count').css({ 'color': 'rgb(128, 128, 128)', 'background-color': 'white' });
    }







    //Filter Rows 
    //Showing and Hiding of filter rows
    $("#queueBody").off('click', '.filterButton').on('click', '.filterButton', function () {
        $(".filterRows").toggleClass("displayNone");
        $("#filterQueueArrow").toggleClass("fa-caret-up fa-caret-down");
    });





    $(".queueTabs")
        .off("click", ".ipaQueueMenu")
        .on("click", ".ipaQueueMenu", function () {
            $(".ipaQueueMenu").removeClass("active");
            $(this).addClass("active");
            var tabId = $(this).attr("id");
            $("#" + tabId).find('a.innerTabsLinks').addClass("topminus3");
            var queueType = $(this).attr('data-queue-type');
            var queuTab = $(this).attr('data-queue-tab');
            var data = { QueueType: queueType, QueueTab: queuTab }

            //$("#queueBody").empty(); // EMPTY QUEUE TABLE BEFORE SEARCH RESULT
            //$.ajax({
            //    url: "/UM/Queue/GetNewTab", // the method we are calling
            //    data: data,
            //    success: function (result) {

            //        $("#queueBody").html(result);
            //    },
            //    error: function (result) {


            //    }
            //});

        });
    $('#fullBodyContainer').off('click', '.view_note_btn').on('click', '.view_note_btn', function () {
        //TabManager.openFloatingModal("~/Areas/Portal/Views/FacilityBridge/BridgeFacilityAccept/_FacilityBody.cshtml", "~/Areas/Portal/Views/FacilityBridge/BridgeFacilityAccept/_FacilityHeader.cshtml", "~/Areas/Portal/Views/FacilityBridge/BridgeFacilityAccept/_FacilityFooter.cshtml");
        TabManager.openFloatingModal("~/Areas/Portal/Views/ProviderBridge/BridgeProviderAccept/ConformationToAddProfile/_ProviderBody.cshtml", "~/Areas/Portal/Views/ProviderBridge/BridgeProviderAccept/ConformationToAddProfile/_ProviderHeader.cshtml", "~/Areas/Portal/Views/ProviderBridge/BridgeProviderAccept/ConformationToAddProfile/_ProviderFooter.cshtml");
    })
    $('body').off('change', '#RejectRadio').on('change', '#RejectRadio', function () {
        // $("#RejectRadio").prop("checked")? $("#reasonElm").show() : $("#reasonElm").hide();
        $("#reasonElm").show();
        $("#notesArea").show();
    })
    $('body').off('change', '#AcceptRadio').on('change', '#AcceptRadio', function () {
        // $("#RejectRadio").prop("checked")? $("#reasonElm").show() : $("#reasonElm").hide();
        $("#reasonElm").hide();
        $("#notesArea").hide();

    })
    $('body').off('click', '.Proceed').on('click', '.Proceed', function () {
        if (!$("#RejectRadio").prop("checked")) {
            TabManager.openFloatingModal("~/Areas/Portal/Views/ProviderBridge/BridgeProviderAccept/ConformationToAddProfile/_ProviderBody.cshtml", "~/Areas/Portal/Views/ProviderBridge/BridgeProviderAccept/ConformationToAddProfile/_ProviderHeader.cshtml", "~/Areas/Portal/Views/ProviderBridge/BridgeProviderAccept/ConformationToAddProfile/_ProviderFooter.cshtml");
        }
        else {
            $('#closeModal').click();
        }
        })
  
    
});