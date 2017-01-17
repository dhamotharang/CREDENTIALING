/// <reference path="../../../Views/ProviderProfile/ConfirmationModal/_TempProviderBody.cshtml" />
/// <reference path="../../../Views/ProviderProfile/ConfirmationModal/_TempProviderBody.cshtml" />
/// <reference path="../../../Views/ProviderProfile/ConfirmationModal/_TempProviderBody.cshtml" />
/// <reference path="../../../Views/ProviderProfile/ConfirmationModal/_TempProviderBody.cshtml" />
/// <reference path="../../../Views/ProviderProfile/ConfirmationModal/_TempProviderBody.cshtml" />
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
    $('[data-toggle="tooltip"]').tooltip();

    $('#PendingBtn').on('click', function () {
        //$('#radioButtons').css('display', '');
        $('#radioButtons').show();
    });

    //$('.ApprovedBtn1,.ApprovedBtn2').on('click', function () {
    //    //$('#radioButtons').css("display", "none");
    //    //$('#radioButtons').attr('style', 'display:none');
    //    $('#radioButtons').hide();
    //});

    $('.ApprovedBtn1,.ApprovedBtn2').off('click').on('click', function () {
        //$('#radioButtons').css("display", "none");
        //$('#radioButtons').attr('style', 'display:none');
        $('#radioButtons').hide();
    });

    $('#RejectedBtn').on('click', function () {
        //$('#radioButtons').css('display', '');
        $('#radioButtons').show();
    });
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

    //====================================================== CRED AXIS BRIDGE ==============================================
    /* REQUEST FILTERS */
    // ALL:
    $('.switch').off('click', '.total-all-pv').on('click', '.total-all-pv', function () {

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
        var queuTab = $(this).attr('data-tab-container');
        var data = { QueueType: queueType, QueueTab: queuTab }

        $('#bridgeQueueTable').empty(); // EMPTY QUEUE TABLE BEFORE SEARCH RESULT
        $.ajax({
            url: "/Portal/BridgeQueue/GetFilteredList?filterType=All&&ViewbagType=" + $('.ipaQueueMenu.active a .par').text(), // the method we are calling
            data: data,
            success: function (result) {

                $("#bridgeQueueTable").html(result);
            },
            error: function (result) {

            }

        });
    });
    // EXPEDITED:
    $('.switch').off('click', '.total-um-pv').on('click', '.total-um-pv', function () {

        $('.dropdown-btn').empty().append('All <span class="caret"></span>');
        $('div.scrollable-division').animate({ 'scrollLeft': $('div.scrollable-division').scrollLeft() - 1000 }, 100, 'swing');

        $('.pointing-arrow-standard').css({ 'border-bottom-color': 'rgba(255,255,255,0)' });
        // $('.total-standard').css({ 'color': '#319012', 'background-color': 'white' });

        $('.pointing-arrow-expedited').css({ 'border-bottom-color': 'rgba(255,255,255,1)' });
        // $('.total-expedited').css({ 'color': 'white', 'background-color': 'rgb(230, 28, 28)' });

        $('.pointing-arrow-all').css({ 'border-bottom-color': 'rgba(255,255,255,0)' });
        //  $('.total-count').css({ 'color': 'rgb(128, 128, 128)', 'background-color': 'white' });

        $('#bridgeQueueTable').empty();
        //$('#intakeQueueTable').find('tr').not('.filterRows').remove();
        var queueType = $(this).attr('data-queue-type');
        var queuTab = $(this).attr('data-tab-container');

        filterByRequestForPV(queueType, queuTab, "um");
    });
    // STANDARD:
    $('.switch').off('click', '.total-claims-pv').on('click', '.total-claims-pv', function () {

        $('.dropdown-btn').empty().append('All <span class="caret"></span>');
        $('div.scrollable-division').animate({ 'scrollLeft': $('div.scrollable-division').scrollLeft() - 1000 }, 100, 'swing');

        $('.pointing-arrow-standard').css({ 'border-bottom-color': 'rgba(255,255,255,1)' });
        //  $('.total-standard').css({ 'color': 'white', 'background-color': '#319012' });

        $('.pointing-arrow-expedited').css({ 'border-bottom-color': 'rgba(255,255,255,0)' });
        //    $('.total-expedited').css({ 'color': 'rgb(230, 28, 28)', 'background-color': 'white' });

        $('.pointing-arrow-all').css({ 'border-bottom-color': 'rgba(255,255,255,0)' });
        //   $('.total-count').css({ 'color': 'rgb(128, 128, 128)', 'background-color': 'white' });

        $('#bridgeQueueTable').empty();
        //$('#intakeQueueTable').find('tr').not('.filterRows').remove();
        var queueType = $(this).attr('data-queue-type');
        var queuTab = $(this).attr('data-tab-container');

        filterByRequestForPV(queueType, queuTab, "claims");

    });

    // POPULATE FILTERED TABLE FOR REQUEST:
    function filterByRequestForPV(queueType, queuTab, request) {
        //restoreSortIconOnFilteration(); //RESET SORT ICON
        var data = { QueueType: queueType, QueueTab: queuTab }

        $('#bridgeQueueTable').empty(); // EMPTY QUEUE TABLE BEFORE SEARCH RESULT
        $.ajax({
            url: "/Portal/BridgeQueue/GetFilteredList?filterType=" + request + "&&ViewbagType=" + $('.ipaQueueMenu.active a .par').text(), // the method we are calling
            data: data,
            success: function (result) {

                $("#bridgeQueueTable").html(result);
                $('body').off('click', '#selectallProv').on('click', '#selectallProv', function () {
                    $('#selectallProv').is(":checked") ? $('input:checkbox').prop('checked', true) : $('input:checkbox').prop('checked', false);
                });
            },
            error: function (result) {


            }

        });
    }
    //====================================================== /CRED AXIS BRIDGE ==============================================

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
    $('#fullBodyContainer').off('click', '#AddNewProviderFromBridge').on('click', '#AddNewProviderFromBridge', function () {

        TabManager.openFloatingModal("~/Areas/Portal/Views/ProviderBridge/BridgeProviderAccept/_ProviderBodyForAdd.cshtml", "~/Areas/Portal/Views/ProviderBridge/BridgeProviderAccept/_ProviderHeaderForAdd.cshtml", "~/Areas/Portal/Views/ProviderBridge/BridgeProviderAccept/_ProviderFooterForAdd.cshtml");
    })
    $('#fullBodyContainer').off('click', '.view_note_btn').on('click', '.view_note_btn', function () {
        TabManager.openFloatingModal("~/Areas/Portal/Views/ProviderBridge/BridgeProviderAccept/_ProviderBody.cshtml", "~/Areas/Portal/Views/ProviderBridge/BridgeProviderAccept/_ProviderHeader.cshtml", "~/Areas/Portal/Views/ProviderBridge/BridgeProviderAccept/_ProviderFooter.cshtml");
    })

    $('#TempProfile').off('click', '.MoveCredAxis').on('click', '.MoveCredAxis', function () {
        TabManager.openFloatingModal("~/Areas/Portal/Views/ProviderProfile/ConfirmationModal/_TempProviderBody.cshtml", "~/Areas/Portal/Views/ProviderProfile/ConfirmationModal/_TempProviderHeader.cshtml", "~/Areas/Portal/Views/ProviderProfile/ConfirmationModal/_TempProviderFooter.cshtml");
    })
    $('body').off('click', '.empyesbtn').on('click', '.tempyesbtn', function () {
        $('.temmessage').hide();
        $('#tempsuccess').removeClass('hidden');
        //$('.tempyesbtn').hide();
        $('.tempfooter').hide();
        setTimeout(searchpage, 2000);
    })
    function searchpage() {

        $('#closetemmodal').click();
        TabManager.navigateToTab(tabnavigation);
    }

    var tabnavigation =
        {
            "tabAction": "SEARCH PROVIDER",
            "tabTitle": "SEARCH PROVIDER",
            "tabPath": "/SearchProvider/_SearchProvider.cshtml",
            "tabContainer": "fullBodyContainer"
        }

    $('body').off('change', '#RejectRadio').on('change', '#RejectRadio', function () {
        // $("#RejectRadio").prop("checked")? $("#reasonElm").show() : $("#reasonElm").hide();
        $('.Proceed ').removeAttr('disabled');
        $("#reasonElm").show();
        $("#notesArea").show();
    })
    $('body').off('change', '#AcceptRadio').on('change', '#AcceptRadio', function () {
        // $("#RejectRadio").prop("checked")? $("#reasonElm").show() : $("#reasonElm").hide();
        $('.Proceed ').removeAttr('disabled');
        $("#reasonElm").hide();
        $("#notesArea").hide();

    })
    $('body').off('click', '.Proceed').on('click', '.Proceed', function () {
        if (!$("#RejectRadio").prop("checked")) {
            TabManager.openFloatingModal("~/Areas/Portal/Views/ProviderBridge/BridgeProviderAccept/ConformationToAddProfile/_ProviderBody.cshtml", "~/Areas/Portal/Views/ProviderBridge/BridgeProviderAccept/ConformationToAddProfile/_ProviderHeader.cshtml", "~/Areas/Portal/Views/ProviderBridge/BridgeProviderAccept/ConformationToAddProfile/_ProviderFooter.cshtml");
        }
        else {
            TabManager.openFloatingModal("~/Areas/Portal/Views/ProviderBridge/BridgeProviderAccept/ConformationToPendProfile/_ProviderBodyPend.cshtml", "~/Areas/Portal/Views/ProviderBridge/BridgeProviderAccept/ConformationToPendProfile/_ProviderHeaderPend.cshtml", "~/Areas/Portal/Views/ProviderBridge/BridgeProviderAccept/ConformationToAddProfile/_ProviderFooter.cshtml");
            //$('#closeModal').click();
        }
    })


    $('#queueBody').off('click', '.theAssign').on('click', '.theAssign', function () {
        TabManager.openFloatingModal("~/Areas/Portal/Views/ProviderBridge/BridgeProviderAccept/ReAssignToProfile/_ProviderBody.cshtml", "~/Areas/Portal/Views/ProviderBridge/BridgeProviderAccept/ReAssignToProfile/_ProviderHeader.cshtml", "~/Areas/Portal/Views/ProviderBridge/BridgeProviderAccept/ReAssignToProfile/_ProviderFooter.cshtml");
    }).off('click', '.theReassign').on('click', '.theReassign', function () {
        TabManager.openFloatingModal("~/Areas/Portal/Views/ProviderBridge/BridgeProviderAccept/ReAssignToProfile/_ProviderBody.cshtml", "~/Areas/Portal/Views/ProviderBridge/BridgeProviderAccept/ReAssignToProfile/_ProviderHeader.cshtml", "~/Areas/Portal/Views/ProviderBridge/BridgeProviderAccept/ReAssignToProfile/_ProviderFooter.cshtml");
    });


    $('.SelectAllProviderClass').off('click', '#selectallProv').on('click', '#selectallProv', function () {
        $('.searchProv').attr('checked', this.checked);
    });

    $('.checkboxproviderlabelData').off('click', '.searchProv').on('click', '.searchProv', function () {

        if ($(".searchProv").length == $(".searchProv:checked").length) {
            $("#selectallProv").attr("checked", "checked");
        } else {
            $("#selectallProv").removeAttr("checked");
        }
    });


    //$("#searchBox1").keyup(function () {
    //    var search_filter = $(this).val();
    //    $("#theRoleUserBucket .col-lg-3.col-md-3.theCol").each(function () {
    //        if ($(this).text().search(new RegExp(search_filter, "i")) < 0) {
    //            $(this).fadeOut();
    //        }
    //        else {
    //            $(this).show();
    //        }
    //    });
    //});


    $('.innerTabsArea').off('click', '#ApprovedListTab').on('click', '#ApprovedListTab', function () {
        $('.ParentSwitch').hide();
        $('.theAssignButtonS').hide();
        $('.theAssignButton').hide();
    });

    $('.innerTabsArea').off('click', '#PendingListTab').on('click', '#PendingListTab', function () {
        $('.ParentSwitch').show();
        $('.theAssignButtonS').show();
        $('.theAssignButton').show();
    });

    $('.theReassign').hide();
    $('.ParentSwitch').off('click', '#switch-y').on('click', '#switch-y', function () {
        $('.theAssign').show();
        $('.theReassign').hide();
        $('.theAssignButtonS .select2-selection__rendered').text('');
    }).off('click', '#switch-i').on('click', '#switch-i', function () {
        $('.theAssign').hide();
        $('.theReassign').show();
        $('.theAssignButtonS .select2-selection__rendered').text('');
    }).off('click', '#switch-n').on('click', '#switch-n', function () {
        $('.theAssign').hide();
        $('.theReassign').show();
        $('.theAssignButtonS .select2-selection__rendered').text('');
    });

});


$(function () {
    $("#selectallProv").click(function () {
        $('.searchProv').attr('checked', this.checked);
    });

    $(".searchProv").click(function () {
        if ($(".searchProv").length == $(".searchProv:checked").length) {
            $("#selectallProv").attr("checked", "checked");
        } else {
            $("#selectallProv").removeAttr("checked");
        }
    });
})

//Store Add Provider
var AddProviderFormData = {};
function StoreFormData(form_id) {
    var $form = $("#" + form_id).find("form");
    AddProviderFormData = $form.prevObject;
}

//Add New Provider
function AddProvider(form_id) {
    //var $form = $("#" + form_id).find("form").prevObject;
    //var form1 = $("#" + form_id).serialize();
        $.ajax({
            url: '/Portal/AddProvider/AddNewProvider',
            type: 'POST',
            //data: new FormData($form[0]),
            data: new FormData(AddProviderFormData[0]),
            async: false,
            cache: false,
            contentType: false,
            processData: false,
            success: function (data) {
                $("#bridgeQueueTable").html(data);
                new PNotify({
                    title: 'Provider Added Successfully',
                    text: 'A Provider has been added successfully',
                    type: 'success',
                    animate: {
                        animate: true,
                        in_class: "lightSpeedIn",
                        out_class: "slideOutRight"
                    }
                });
            }
        });
}