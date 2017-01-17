$(function () {
    $('#queueBody').css('overflow', 'hidden')
    $('.tableBodyDivision').css('height', ($(window).height() - 204));
    populateTable();
    calculateTotal();
    $('[data-toggle="tooltip"]').tooltip(); //TOOLTIP
    $('[data-toggle="popover"]').popover(); //POPOVER

    // POPULATE TABLE:
    function populateTable() {
        $.each(QueueData, function (index, value) {
            $('#intakeQueueTable').append(
                        '<tr onclick="openViewAuth();">' +
                        '<td class="one ' + value.COLOR + 'left width_1 "><input type="checkbox" class="flat chkQueueRow"/></td>' +
                        '<td class="two">' + value.ABV + '</td>' +
                        '<td class="three">' + value.REFNO + '</td>' +
                        '<td class="four">' + value.MBRID + '</td>' +
                        '<td class="five"><div class="member-name" data-toggle="popover" title="Member Name" data-trigger="hover" data-content="' + value.MBRNAME + '">' + value.MBRNAME.trimToLength(10) + '</div></td>' +
                        '<td class="six">' + value.DOS + '</td>' +
                        '<td class="seven">' + value.REWDT + '</td>' +
                        '<td class="eight">' + value.EXPDC + '</td>' +
                        '<td class="nine"><div class="provider" data-toggle="popover" title="Provider" data-trigger="hover" data-content="' + value.PROVIDER + '">' + value.PROVIDER.trimToLength(10) + '</div></td>' +
                        '<td class="ten"><div class="facility" data-toggle="popover" title="Facility" data-trigger="hover" data-content="' + value.FACILITY + '">' + value.FACILITY.trimToLength(10) + '</td>' +
                        '<td class="eleven">' + value.SVCPROVIDER.trimToLength(10) + '</td>' +
                        'html before' + (value.REQUEST == 'Expedited' ?
                        '<td class="twelve"><div class="danger">' + value.REQUEST + '</div></td>' :
                        '<td class="twelve"><div class="ok">' + value.REQUEST + '</div></td>') + 'more html' +
                        '<td class="thirteen">' + value.AUTH + '</td>' +
                        '<td class="fourteen">' + value.TOC + '</td>' +
                        '<td class="fifteen">' + value.REVIEW + '</td>' +
                        '<td class="sixteen">' + value.ACTLOS + '</td>' +
                        '<td class="seventeen">' + value.DAYSREQ + '</td>' +
                        '<td class="eighteen">' + value.TOTALAUTHED + '</td>' +
                        '<td class="nineteen">' + value.TOTALDENIED + '</td>' +
                        '<td class="twenty">' + value.STATUS + '</td>' +
                        '<td class="twentyone">' + value.POS + '</td>' +
                        '<td class="twentytwo">' + value.DX.trimToLength(10) + '</td>' +
                        '<td class="twentythree">' + value.ASSIGNEDTO + '</td>' +
                        '<td class="twentyfour ' + value.COLOR + 'right">' + value.ENTRY + '</td>' +
                        '</tr>'
                        );
        });
        initIcheck();
    };

    // CALCULATE TOTAL EXPEDITED / STANDARD:
    function calculateTotal() {
        $('.total-count').append(Object.keys(QueueData).length);
        var expedited = 0, standard = 0;
        $.each(QueueData, function (index, value) {
            if (value.REQUEST == 'Expedited') {
                expedited += 1;
            }
            if (value.REQUEST == 'Standard') {
                standard++;
            }
        });
        $('.total-expedited').append(expedited);
        $('.total-standard').append(standard);
    };

    //SCROLLS TABLE LEFT:
    $('.left-scroll-button').click(function () {
        var currentHorizontalPosition = $('div.scrollable-division').scrollLeft();
        $('div.scrollable-division').animate({ 'scrollLeft': currentHorizontalPosition - 300 }, 250, 'swing');
    });

    //SCROLLS TABLE RIGHT:
    $('.right-scroll-button').click(function () {
        var currentHorizontalPosition = $('div.scrollable-division').scrollLeft();
        $('div.scrollable-division').animate({ 'scrollLeft': currentHorizontalPosition + 300 }, 250, 'swing');
    });

    // ADJUST COLUMN HEIGHT ACCORDING TO SCREEN RESOLUTION:
    $(window).resize(function () {
        $('.tableBodyDivision').css('height', ($(window).height() - 204));
    });

    // CHECK-ALL CHECKBOXES:
    $("#chkQueueHeader").on('ifChecked', function () {
        $('.chkQueueRow').iCheck('check');
    });
    $("#chkQueueHeader").on('ifUnchecked', function () {
        $('.chkQueueRow').iCheck('uncheck');
    });

    // LEGEND FILTERS:
    // GREEN:
    $('.small-legend-green').on('click', function () {
        $('#intakeQueueTable').empty();
        filterColor("greentd");
    });
    // YELLOW:
    $('.small-legend-yellow').on('click', function () {
        $('#intakeQueueTable').empty();
        filterColor("yellowtd");
    });
    // RED:
    $('.small-legend-red').on('click', function () {
        $('#intakeQueueTable').empty();
        filterColor("redtd");
    });
    // GREY:
    $('.small-legend-white').on('click', function () {
        $('#intakeQueueTable').empty();
        filterColor("greytd");
    });

    // POPULATE FILTERED TABLE FOR LEGEND:
    function filterColor(color) {
        $.each(QueueData, function (index, value) {
            if (value.COLOR == color) {
                $('#intakeQueueTable').append(
                        '<tr onclick="openViewAuth();">' +
                        '<td class="one ' + value.COLOR + 'left width_1 "><input type="checkbox" class="flat chkQueueRow"/></td>' +
                        '<td class="two">' + value.ABV + '</td>' +
                        '<td class="three">' + value.REFNO + '</td>' +
                        '<td class="four">' + value.MBRID + '</td>' +
                        '<td class="five"><div class="member-name" data-toggle="popover" title="Member Name" data-trigger="hover" data-content="' + value.MBRNAME + '">' + value.MBRNAME.trimToLength(10) + '</div></td>' +
                        '<td class="six">' + value.DOS + '</td>' +
                        '<td class="seven">' + value.REWDT + '</td>' +
                        '<td class="eight">' + value.EXPDC + '</td>' +
                        '<td class="nine"><div class="provider" data-toggle="popover" title="Provider" data-trigger="hover" data-content="' + value.PROVIDER + '">' + value.PROVIDER.trimToLength(10) + '</div></td>' +
                        '<td class="ten"><div class="facility" data-toggle="popover" title="Facility" data-trigger="hover" data-content="' + value.FACILITY + '">' + value.FACILITY.trimToLength(10) + '</td>' +
                        '<td class="eleven">' + value.SVCPROVIDER.trimToLength(10) + '</td>' +
                        'html before' + (value.REQUEST == 'Expedited' ?
                        '<td class="twelve"><div class="danger">' + value.REQUEST + '</div></td>' :
                        '<td class="twelve"><div class="ok">' + value.REQUEST + '</div></td>') + 'more html' +
                        '<td class="thirteen">' + value.AUTH + '</td>' +
                        '<td class="fourteen">' + value.TOC + '</td>' +
                        '<td class="fifteen">' + value.REVIEW + '</td>' +
                        '<td class="sixteen">' + value.ACTLOS + '</td>' +
                        '<td class="seventeen">' + value.DAYSREQ + '</td>' +
                        '<td class="eighteen">' + value.TOTALAUTHED + '</td>' +
                        '<td class="nineteen">' + value.TOTALDENIED + '</td>' +
                        '<td class="twenty">' + value.STATUS + '</td>' +
                        '<td class="twentyone">' + value.POS + '</td>' +
                        '<td class="twentytwo">' + value.DX.trimToLength(10) + '</td>' +
                        '<td class="twentythree">' + value.ASSIGNEDTO + '</td>' +
                        '<td class="twentyfour ' + value.COLOR + 'right">' + value.ENTRY + '</td>' +
                        '</tr>'
                        );
            }
        });
        initIcheck();
    }

    // REQUEST FILTER:
    // ALL:
    $('.total-count').on('click', function () {
        $('#intakeQueueTable').empty();
        populateTable();
    });
    // EXPEDITED:
    $('.total-expedited').on('click', function () {
        $('#intakeQueueTable').empty();
        filterRequest("Expedited");
    });
    // STANDARD:
    $('.total-standard').on('click', function () {
        $('#intakeQueueTable').empty();
        filterRequest("Standard");
    });
    // POPULATE FILTERED TABLE FOR REQUEST:
    function filterRequest(request) {
        $.each(QueueData, function (index, value) {
            if (value.REQUEST == request) {
                $('#intakeQueueTable').append(
                        '<tr onclick="openViewAuth();">' +
                        '<td class="one ' + value.COLOR + 'left width_1 "><input type="checkbox" class="flat chkQueueRow"/></td>' +
                        '<td class="two">' + value.ABV + '</td>' +
                        '<td class="three">' + value.REFNO + '</td>' +
                        '<td class="four">' + value.MBRID + '</td>' +
                        '<td class="five"><div class="member-name" data-toggle="popover" title="Member Name" data-trigger="hover" data-content="' + value.MBRNAME + '">' + value.MBRNAME.trimToLength(10) + '</div></td>' +
                        '<td class="six">' + value.DOS + '</td>' +
                        '<td class="seven">' + value.REWDT + '</td>' +
                        '<td class="eight">' + value.EXPDC + '</td>' +
                        '<td class="nine"><div class="provider" data-toggle="popover" title="Provider" data-trigger="hover" data-content="' + value.PROVIDER + '">' + value.PROVIDER.trimToLength(10) + '</div></td>' +
                        '<td class="ten"><div class="facility" data-toggle="popover" title="Facility" data-trigger="hover" data-content="' + value.FACILITY + '">' + value.FACILITY.trimToLength(10) + '</td>' +
                        '<td class="eleven">' + value.SVCPROVIDER.trimToLength(10) + '</td>' +
                        'html before' + (value.REQUEST == 'Expedited' ?
                        '<td class="twelve"><div class="danger">' + value.REQUEST + '</div></td>' :
                        '<td class="twelve"><div class="ok">' + value.REQUEST + '</div></td>') + 'more html' +
                        '<td class="thirteen">' + value.AUTH + '</td>' +
                        '<td class="fourteen">' + value.TOC + '</td>' +
                        '<td class="fifteen">' + value.REVIEW + '</td>' +
                        '<td class="sixteen">' + value.ACTLOS + '</td>' +
                        '<td class="seventeen">' + value.DAYSREQ + '</td>' +
                        '<td class="eighteen">' + value.TOTALAUTHED + '</td>' +
                        '<td class="nineteen">' + value.TOTALDENIED + '</td>' +
                        '<td class="twenty">' + value.STATUS + '</td>' +
                        '<td class="twentyone">' + value.POS + '</td>' +
                        '<td class="twentytwo">' + value.DX.trimToLength(10) + '</td>' +
                        '<td class="twentythree">' + value.ASSIGNEDTO + '</td>' +
                        '<td class="twentyfour ' + value.COLOR + 'right">' + value.ENTRY + '</td>' +
                        '</tr>'
                        );
            }
        });
        initIcheck();
    }

    // INITIALIZE ICHECK COMPONENT:
    function initIcheck() {
        if ($("input.flat")[0]) {
            $('input.flat').iCheck({
                checkboxClass: 'icheckbox_square-green',
                radioClass: 'iradio_flat-green'
            });
        }
    }
});