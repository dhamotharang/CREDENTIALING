//=====================================================================================================================================================
//==================================================== S C H E D U L E R ==============================================================================
//=====================================================================================================================================================
//====================================================== AUTHOR: Sayak ================================================================================
//=====================================================================================================================================================

//Get Difference In Minutes
var getDifferenceInMinutes = function (dateObj1, dateObj2) {
    var timediff = dateObj2.getTime() - dateObj1.getTime();
    var mins = timediff / (60 * 1000);
    return mins;
}

//Add Minutes
var addMinutes = function (date, mins) {
    date.setTime(date.getTime() + (mins * 60000));
}

function pCalendarController(divSelector) {
    //=================================================================================================
    //====================   VARIABLE DECLARATIONS AND USER CONTROL   =================================
    //=================================================================================================
    var divName = divSelector;

    //---------- Deprecated --------------
    var daysInMonth = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];

    var daysInFull = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
    var daysInShort = ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'];
    var monthsInFull = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
    var monthsShort = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    var selectedMonth = (new Date().getMonth()) + 1, selectedYear = (new Date()).getFullYear(), selectedDay = (new Date()).getDate();
    var selectedDate = new Date('9/10/2016');
    var calendarType = 'daily';
    var statusFilter = 'off';

    var filteredEvents = [];
    var StartTime = new Date(selectedYear, selectedMonth - 1, selectedDay, 9, 0, 0);
    var EndTime = new Date(selectedYear, selectedMonth - 1, selectedDay, 17, 0, 0);
    var slotInterval = 15;

    //Intialize The Main Table and Start The Application
    $(divName).empty().append('<table class="pCalendar-Main"></table>');
    initializeApplication();

    //=================================================================================================
    //===================  UI EVENT HANDLERS   ========================================================
    //=================================================================================================

    //------(Deprecated)-------
    //When The User Increases The Selected Month
    $(divName).off('click', '.pCalendar-Controls .pCalendar-button-sm.next').on('click', '.pCalendar-Controls .pCalendar-button-sm.next', function (evt) {
        if (calendarType == 'monthly') {
            if (selectedMonth < 12)
                ++selectedMonth;
            else {
                selectedMonth = 1;
                ++selectedYear;
            }
        }
        if (calendarType == 'yearly') {
            ++selectedYear;
        }
        if (calendarType == 'daily') {
            selectedDate.setDate(selectedDate.getDate() + 1);
        }
        if (calendarType == 'weekly') {
            selectedYear++;
        }
        $(divSelector).trigger('dateChanged');
    });

    //------(Deprecated)-------
    //When The User Decreases The Selected Month
    $(divSelector).off('click', '.pCalendar-Controls .pCalendar-button-sm.prev').on('click', '.pCalendar-Controls .pCalendar-button-sm.prev', function (evt) {
        if (calendarType == 'monthly') {
            if (selectedMonth > 0)
                selectedMonth--;
            else {
                selectedMonth = 11;
                selectedYear--;
            }
        }
        if (calendarType == 'yearly') {
            --selectedYear;
        }
        if (calendarType == 'daily') {
            selectedDate.setDate(selectedDate.getDate() - 1);
        }
        if (calendarType == 'weekly') {
            selectedYear--;
        }
        $(divName).trigger('dateChanged');
    });

    //When The User Changes The Calendar Type (Calendar Type Nav Buttons)
    $(divName).off('click', '.pCalendar-Controls .pCalendar-modeSelector ul li a.tablinks').on('click', '.pCalendar-Controls .pCalendar-modeSelector ul li a.tablinks', function (evt) {
        evt.preventDefault();

        var buttonType = $(evt.target).attr('calendarType');
        if (buttonType == 'Yearly')
            calendarType = 'yearly';
        if (buttonType == 'Monthly')
            calendarType = 'monthly';
        if (buttonType == 'Daily')
            calendarType = 'daily';
        if (buttonType == 'Weekly')
            calendarType = 'weekly';
        initializeApplication();
    });

    //When User Selects A Month
    $(divName).off('change', '.pCalendar-Main .pCalendar-Selectors td div .monthSelector').on('change', '.pCalendar-Main .pCalendar-Selectors td div .monthSelector', function (evt) {
        selectedMonth = $(evt.target).val();
        $(divName).trigger('dateChanged');
    });

    //When User Selectes a Year
    $(divName).off('change', '.pCalendar-Main .pCalendar-Selectors td div .yearSelector').on('change', '.pCalendar-Main .pCalendar-Selectors td div .yearSelector', function (evt) {
        selectedYear = $(evt.target).val();
        $(divName).trigger('dateChanged');
    });

    //When User Selects A Date From The DatePicker Control
    $(divName).off('change', '.pCalendar-Main .pCalendar-Selectors td div .dateSelector').on('change', '.pCalendar-Main .pCalendar-Selectors td div .dateSelector', function (evt) {
        var selDt = new Date($(this).val());
        selectedYear = selDt.getFullYear();
        selectedMonth = parseInt(selDt.getMonth()) + 1;
        selectedDay = selDt.getDate();
        $(divName).trigger('dateChanged');
    });

    //When User Clicks On A Table Cell
    $(divName).off('click', '.pCalendar-Main .pCalendar-bodyRow td').on('click', '.pCalendar-Main .pCalendar-bodyRow td', function (evt) {
        if ($(this).hasClass('pCalendar-active')) {
            if (calendarType == 'yearly') {
                var month = this.dataset.cellid;
                selectedMonth = parseInt(month) + 1;
                calendarType = 'monthly';
                $(divName).trigger('dateChanged');
                return;
            }
            if (calendarType == 'monthly') {
                var date = this.dataset.cellid;
                selectedDay = date;
                calendarType = 'daily';
                $(divName).trigger('dateChanged');
                return;
            }
            if (calendarType == 'weekly') {
                var date = this.dataset.cellid;
                selectedDay = date;
                calendarType = 'daily';
                $(divName).trigger('dateChanged');
                return;
            }
        }

        if (calendarType == 'daily') {
            if (!$(this).hasClass('pCalendar-active')) {
                TabManager.navigateToTab({
                    "tabAction": "Encounter Scheduler",
                    "tabTitle": "Create Schedule",
                    "tabContainer": "fullBodyContainer",
                    "tabAutoFlush": "false",
                    "tabPath": "~/Areas/Encounters/Views/Schedule/_CreateSchedule.cshtml"
                });
            }
            else {
                TabManager.navigateToTab({
                    "tabAction": "Encounter Scheduler",
                    "tabTitle": "Create Schedule",
                    "tabContainer": "fullBodyContainer",
                    "tabAutoFlush": "false",
                    "tabPath": "~/Areas/Encounters/Views/Schedule/CreateScheduleAutofilled.cshtml"
                });
            }
        }
    });

    //When User Clicks On Summary Badges For Filtering The Data
    $(divName).off('click', '.pCalendar-eventSummary span').on('click', '.pCalendar-eventSummary span', function (evt) {
        if (calendarType == 'daily') {
            statusFilter = $(this).data('val');
            renderDailyCalendar();
        }
        if (calendarType == 'monthly') {
            statusFilter = $(this).data('val');
            renderMonthlyCalendar();
        }
        if (calendarType == 'yearly') {
            statusFilter = $(this).data('val');
            renderYearlyCalendar();
        }
        if (calendarType == 'weekly') {
            statusFilter = $(this).data('val');
            renderWeeklyCalendar();
        }
    });

    //=================================================================================================
    //===================   VIEW HELPERS   ============================================================
    //=================================================================================================

    //Render The Calendar Controls Depending On The Selected Calendar Type
    function renderControls(calendarType) {
        if (calendarType == 'weekly') {
            var monthOptionList = '';
            var daysOptionList = '';
            var headRowContent = '';
            var yearOptionList = '';
            var selectedDate = new Date(selectedYear, selectedMonth - 1, selectedDay);
            var startDayIndex = selectedDay - selectedDate.getDay();
            if (selectedDate.getDay() == 0) {
                startDayIndex -= 7;
            }

            var endDayIndex = parseInt(startDayIndex) + parseInt('7');
            for (i = startDayIndex; i < endDayIndex ; i++) {
                var date = new Date(selectedYear, selectedMonth - 1, (i + 1));
                headRowContent += '<td>' + date.toDateString() + '</td>';
            }

            $(divName + ' .pCalendar-Main').empty();
            $(divName + ' .pCalendar-Main').append(
                '<tr class="pCalendar-Controls">' +
                 '   <td colspan="3" class="pCalendar-navigator take-left">' +

                 '   </td>' +
                   ' <td colspan="4" class="pCalendar-modeSelector clear">' +
                    '<ul class="tab take-right" style="margin-bottom: 0px;">' +
                      '<li><a href="#" class="tablinks" calendarType="Daily">Daily</a></li>' +
                      '<li><a href="#" class="tablinks theme_thead" calendarType="Weekly">Weekly</a></li>' +
                      '<li><a href="#" class="tablinks" calendarType="Monthly">Monthly</a></li>' +
                      '<li><a href="#" class="tablinks" calendarType="Yearly">Yearly</a></li>' +
                    '</ul>' +
                   ' </td>' +

             '   </tr>' +
               '<tr class="pCalendar-NoBorder pCalendar-Selectors theme_thead">' +
                '<td colSpan="1">' +
                    '<div class="form-group">' +
                        '<label>Choose Start Date: </label>' +
                        '<input type="text" class="form-control input-xs non_mandatory_field_halo dateSelector"/>' +
                    '</div>' +
                '</td>' +
                '<td colSpan="2">' +
                    '<label>Summary: </label>' +
                    '<div class="pCalendar-eventSummary">' +
                    GetSummaryOfEvents(new Date(selectedYear, selectedMonth - 1, startDayIndex + 1), new Date(selectedYear, selectedMonth - 1, startDayIndex + 8)) +
                    '</div>' +
                '</td>' +
                '<td colSpan="4" class="pCalendar-largeFont">Currently Showing: <br/><span class="pCalendar-boldFont">' + (new Date(selectedYear, parseInt(selectedMonth) - 1, parseInt(startDayIndex) + 1)).toDateString().replace(' ', ',') + '</span> to <span class="pCalendar-boldFont">' + (new Date(selectedYear, parseInt(selectedMonth) - 1, parseInt(endDayIndex))).toDateString().replace(' ', ',') + '</span></td>' +
             '</tr>' +
             '<tr class="pCalendar-headRow theme_thead">' +
                headRowContent +
               ' </tr>'
                );
        }
        if (calendarType == 'monthly') {
            var date = monthsInFull[selectedMonth - 1] + ',' + selectedYear;
            var yearOptionList = '';
            var monthOptionList = '';
            for (i = 2000; i < 2050; i++) {
                if (selectedYear == i)
                    yearOptionList += '<option value="' + i + '" selected="selected">' + i + '</option>';
                else
                    yearOptionList += '<option value="' + i + '">' + i + '</option>';
            }
            for (i = 0; i < 12; i++) {
                if (parseInt(selectedMonth) - 1 == i)
                    monthOptionList += '<option value="' + (parseInt(i) + 1) + '" selected="selected">' + monthsInFull[i] + '</option>';
                else
                    monthOptionList += '<option value="' + (parseInt(i) + 1) + '">' + monthsInFull[i] + '</option>';
            }

            $(divName + ' .pCalendar-Main').empty();
            $(divName + ' .pCalendar-Main').append(
                '<tr class="pCalendar-Controls">' +
                 '   <td colspan="3" class="pCalendar-navigator take-left">' +

                 '   </td>' +
                   ' <td colspan="4" class="pCalendar-modeSelector clear">' +
                    '<ul class="tab take-right" style="margin-bottom: 0px;">' +
                      '<li><a href="#" class="tablinks" calendarType="Daily">Daily</a></li>' +
                      '<li><a href="#" class="tablinks" calendarType="Weekly">Weekly</a></li>' +
                      '<li><a href="#" class="tablinks theme_thead" calendarType="Monthly">Monthly</a></li>' +
                      '<li><a href="#" class="tablinks" calendarType="Yearly">Yearly</a></li>' +
                    '</ul>' +
                   ' </td>' +

             '   </tr>' +
               '<tr class="pCalendar-NoBorder pCalendar-Selectors theme_thead">' +
                '<td colSpan="1">' +
                    '<div class="form-group">' +
                    '<label>Choose Year: </label>' +
                    '<select class="form-control input-xs yearSelector">' +
                        yearOptionList +
                    '</select>' +
                    '</div>' +
                '</td>' +
                '<td colSpan="1">' +
                    '<div class="form-group">' +
                    '<label>Choose Month: </label>' +
                    '<select class="form-control input-xs monthSelector">' +
                        monthOptionList +
                    '</select>' +
                    '</div>' +
                '</td>' +
                 '<td colSpan="1">' +
                    '<div class="form-group">' +
                    '<label>Choose Provider</label>' +
                    '<input list="BillProviders" placeholder="Provider.." class="form-control input-xs non_mandatory_field_halo">' +
                                        '<datalist id="BillProviders">' +
                                            '<option value="Pariksith Singh">' +
                                            '<option value="Nishat Seema">' +
                                            '<option value="Ali Syed">' +
                                        '</datalist>' +
                    '</div>' +
                '</td>' +
                '<td colSpan="1">' +
                '<div class="form-group">' +
                    '<label>Choose Facility: </label>' +
                   '<input list="facilities" placeholder="Facility.." class="form-control input-xs non_mandatory_field_halo">' +
                                        '<datalist id="facilities">' +
                                            '<option value="1094 North Cliffe Dlve">' +
                                            '<option value="10441 Quality Dr">' +
                                            '<option value="10494 Internal Med">' +
                                            '<option value="10045 Cortez Blvd">' +
                                            '<option value="13235 State Road Blvd">' +
                                            '<option value="14690 Spring Hill Dr">' +
                                        '</datalist>' +
                    '</div>' +
                '</td>' +
                 '<td colSpan="1">' +
                '<div class="form-group">' +
                    '<label>Choose Services: </label>' +
                   '<input list="facilities" placeholder="Services.." class="form-control input-xs non_mandatory_field_halo">' +
                                        '<datalist id="facilities">' +
                                            '<option value="1094 North Cliffe Dlve">' +
                                            '<option value="10441 Quality Dr">' +
                                            '<option value="10494 Internal Med">' +
                                            '<option value="10045 Cortez Blvd">' +
                                            '<option value="13235 State Road Blvd">' +
                                            '<option value="14690 Spring Hill Dr">' +
                                        '</datalist>' +
                    '</div>' +
                '</td>' +
                 '<td colSpan="1">' +
                    '<label>Summary: </label>' +
                    '<div class="pCalendar-eventSummary">' +
                    GetSummaryOfEvents(new Date(selectedYear, selectedMonth - 1, 1), new Date(selectedYear, selectedMonth - 1, daysInMonth[selectedMonth - 1])) +
                    '</div>' +
                '</td>' +
                '<td colSpan="1" class="pCalendar-largeFont">Currently Showing:<br/> <span class="pCalendar-boldFont">' + monthsInFull[selectedMonth - 1] + ',' + selectedYear + '</span></td>' +
             '   <tr class="pCalendar-headRow theme_thead">' +
                 '   <td>Sun</td>' +
                  '  <td>Mon</td>' +
                  '  <td>Tue</td>' +
                 '   <td>Wed</td>' +
                 '   <td>Thu</td>' +
                 '   <td>Fri</td>' +
                 '   <td>Sat</td>' +
               ' </tr>'
                );
        }
        if (calendarType == 'yearly') {
            var yearOptionList = '';
            for (i = 2000; i < 2050; i++) {
                if (selectedYear == i)
                    yearOptionList += '<option value="' + i + '" selected="selected">' + i + '</option>';
                else
                    yearOptionList += '<option value="' + i + '">' + i + '</option>';
            }
            $(divName + ' .pCalendar-Main').empty();
            $(divName + ' .pCalendar-Main').append(
                '<tr class="pCalendar-Controls">' +
                  '   <td colspan="3" class="pCalendar-navigator">' +

                 '   </td>' +
                  ' <td colspan="3" class="pCalendar-modeSelector">' +
                      '<ul class="tab take-right" style="margin-bottom: 0px;">' +
                      '<li><a href="#" class="tablinks" calendarType="Daily">Daily</a></li>' +
                      '<li><a href="#" class="tablinks" calendarType="Weekly">Weekly</a></li>' +
                      '<li><a href="#" class="tablinks" calendarType="Monthly">Monthly</a></li>' +
                      '<li><a href="#" class="tablinks theme_thead" calendarType="Yearly">Yearly</a></li>' +
                    '</ul>' +
                   ' </td>' +

             '   </tr>' +
             '<tr class="pCalendar-NoBorder pCalendar-Selectors theme_thead">' +
                '<td colSpan="1">' +
                    '<div class="form-group">' +
                    '<label>Choose Year: </label>' +
                    '<select class="form-control input-xs yearSelector">' +
                        yearOptionList +
                    '</select>' +
                    '</div>' +
                '</td>' +
                '<td colSpan="1">' +
                    '<label>Summary: </label>' +
                    '<div class="pCalendar-eventSummary">' +
                    GetSummaryOfEvents(new Date(selectedYear, 0, 1), new Date(selectedYear, 11, 31)) +
                    '</div>' +
                '</td>' +
                '<td colSpan="4" class="pCalendar-largeFont">Currently Showing:<br/> <span class="pCalendar-boldFont">' + selectedYear + '</span></td>' +
              '</tr>' +
             '   <tr class="pCalendar-headRow theme_thead" data-row="1">' +
                 '   <td>Jan</td>' +
                  '  <td>Feb</td>' +
                  '  <td>Mar</td>' +
                 '   <td>Apr</td>' +
                 '   <td>May</td>' +
                 '   <td>June</td>' +
               ' </tr>' +
               '<tr class="pCalendar-headRow theme_thead" data-row="2">' +
                '   <td>July</td>' +
                 '   <td>Aug</td>' +
                 '   <td>Sep</td>' +
                 '   <td>Oct</td>' +
                 '   <td>Nov</td>' +
                 '   <td>Dec</td>' +
                '</tr>'
                );
        }
        if (calendarType == 'daily') {
            StartTime = new Date(selectedYear, selectedMonth - 1, selectedDay, 9, 0, 0);
            EndTime = new Date(selectedYear, selectedMonth - 1, selectedDay, 17, 0, 0);
            var adjustment = calculateAdjustmentIndex(StartTime, EndTime, slotInterval);
            var rows = adjustment.MaximumRows + 1;
            var colspan = parseInt(rows / 2);

            $(divName + ' .pCalendar-Main').empty();
            $(divName + ' .pCalendar-Main').append(
                '<tr class="pCalendar-Controls">' +
                   '   <td colspan="' + colspan + '" class="pCalendar-navigator">' +

                 '   </td>' +
                   ' <td colspan="' + (rows - colspan) + '" class="pCalendar-modeSelector">' +
                    '<ul class="tab take-right" style="margin-bottom: 0px;">' +
                      '<li><a href="#" class="tablinks theme_thead" calendarType="Daily">Daily</a></li>' +
                       '<li><a href="#" class="tablinks" calendarType="Weekly">Weekly</a></li>' +
                      '<li><a href="#" class="tablinks" calendarType="Monthly">Monthly</a></li>' +
                      '<li><a href="#" class="tablinks" calendarType="Yearly">Yearly</a></li>' +
                    '</ul>' +
                   ' </td>' +

             '   </tr>' +
             '<tr class="pCalendar-NoBorder pCalendar-Selectors theme_thead">' +
              '<td colSpan="1">' +
                 '<div class="form-group">' +
                        '<label>Date: </label>' +
                        '<input type="text" class="form-control input-xs non_mandatory_field_halo dateSelector"/>' +
                    '</div>' +
                '</td>' +
                '<td colSpan="1">' +
                    '<div class="form-group">' +
                    '<label>Search By Provider</label>' +
                    '<input list="BillProviders" placeholder="Select Provider" class="form-control input-xs non_mandatory_field_halo">' +
                                        '<datalist id="BillProviders">' +
                                            '<option value="Pariksith Singh">' +
                                            '<option value="Nishat Seema">' +
                                            '<option value="Ali Syed">' +
                                        '</datalist>' +
                    '</div>' +
                '</td>' +
                '<td colSpan="1">' +
                '<div class="form-group">' +
                    '<label>Search By Facility: </label>' +
                   '<input list="facilities" placeholder="Select Facility" class="form-control input-xs non_mandatory_field_halo">' +
                                        '<datalist id="facilities">' +
                                            '<option value="1094 North Cliffe Dlve">' +
                                            '<option value="10441 Quality Dr">' +
                                            '<option value="10494 Internal Med">' +
                                            '<option value="10045 Cortez Blvd">' +
                                            '<option value="13235 State Road Blvd">' +
                                            '<option value="14690 Spring Hill Dr">' +
                                        '</datalist>' +
                    '</div>' +
                '</td>' +
                '<td colSpan="1">' +
                    '<label>Summary: </label>' +
                    '<div class="pCalendar-eventSummary">' +
                        GetSummaryOfEventsForDay(new Date(selectedYear, selectedMonth - 1, selectedDay)) +
                    '</div>' +
                '</td>' +
                '<td colSpan="' + (rows - 4) + '" class="pCalendar-largeFont">' +
                'Currently Showing: <br/><span class="pCalendar-boldFont">' + (new Date(selectedYear, selectedMonth - 1, selectedDay)).toDateString().replace(' ', ',') + '</span>' +
                '</td>' +
             '</tr>' + GetSlottedTimeIntervals(StartTime, EndTime, slotInterval)
                );
        }
        $('.dateSelector').datepicker().datepicker('setDate', new Date(selectedYear, selectedMonth - 1, selectedDay));
    }

    //Render The Monthly Calendar
    function renderMonthlyCalendar() {
        renderControls('monthly');
        var firstDay = GetDayNameIndex(selectedYear, selectedMonth - 1, 1);
        var daysInPreviousMonth = new Date(selectedYear, selectedMonth - 1, 0).getDate();
        var daysInSelectedMonth = new Date(selectedYear, selectedMonth, 0).getDate();
        var currentDay = 1;
        var i = 0;
        var j = 0;
        for (i = 0; i < 6; i++) {
            var days;
            var row = '<tr class="pCalendar-bodyRow">';
            for (j = i * 7; j < (i + 1) * 7; j++) {
                if (j < firstDay) {
                    row += '<td class="pCalendar-disabled"><span class="pCalendar-dayNumber">' + (daysInPreviousMonth - firstDay + j + 1) + '</span></td>';
                }
                else if (currentDay > daysInSelectedMonth) {
                    row += '<td class="pCalendar-disabled"><span class="pCalendar-dayNumber">' + (j - daysInSelectedMonth - firstDay + 1) + '</span></td>';
                }
                else {
                    row += '<td class="pCalendar-active" data-CellID="' + currentDay + '">' +
                                   '<span class="pCalendar-dayNumber">' + currentDay + '</span>' +
                                   '<div class="pCalendar-eventInfo">' +
                                  GetCalendarEvents(new Date(selectedYear, selectedMonth - 1, currentDay)) +
                               '</div>' +
                           '</td>';
                    currentDay++;
                }
            }
            $(divName + ' .pCalendar-Main').append(row + '</tr>');
            $('span[data-toggle="tooltip"]').tooltip();
        }
    }

    //Render The Yearly Calendar
    function renderYearlyCalendar() {
        var j = 0;
        renderControls('yearly');
        var row = '<tr class="pCalendar-bodyRow">';
        for (j = 0; j < 6; j++) {
            row += '<td class="pCalendar-active pCalendar-highPadding" data-CellID="' + j + '">' +
                           '<div class="pCalendar-eventInfo">' +
                          GetCalendarEventsForMonth(selectedYear, j) +
                       '</div>' +
                   '</td>';
        }
        $(divName + ' .pCalendar-Main tr[data-row="1"]').after(row + '</tr>');

        row = '<tr class="pCalendar-bodyRow">';
        for (j = 6; j < 12; j++) {
            row += '<td class="pCalendar-active" data-CellID="' + j + '">' +
                           '<div class="pCalendar-eventInfo">' +
                            GetCalendarEventsForMonth(selectedYear, j) +
                       '</div>' +
                   '</td>';
        }
        $(divName + ' .pCalendar-Main tr[data-row="2"]').after(row + '</tr>');
        $('span[data-toggle="tooltip"]').tooltip();
    }

    //Render The Daily Calendar
    function renderDailyCalendar() {
        renderControls('daily');
        var dayEvents = GetEventsForDay(new Date(selectedYear, selectedMonth - 1, selectedDay));
        for (i in dayEvents) {
            var encStatusHtml = '';
            var encStatusBullet = '';
            var row = dayEvents[i];
            var slot = row.Slot;
            if (row.Status == 'Active') {
                //tdClass = 'pCalendar-encOpen';
                encStatusHtml = '<span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-active"><i class="fa fa-check"></i></span><span class="pCalendar-showOnHover pCalendar-greenFont">Active</span>';
                encStatusBullet = '<span class="pCalendar-bullet pCalendar-bullet-xs pCalendar-badge-active"><i class="fa fa-check"></i></span>';
            }

            if (row.Status == 'NoShow') {
                //tdClass = 'pCalendar-encClosed';
                encStatusHtml = '<span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-noShow"><i class="fa fa-remove"></i></span><span class="pCalendar-showOnHover pCalendar-redFont">No Show</span>';
                encStatusBullet = '<span class="pCalendar-bullet pCalendar-bullet-xs pCalendar-badge-noShow"><i class="fa fa-remove"></i></span>';
            }

            if (row.Status == 'Scheduled') {
                encStatusHtml = '<span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-scheduled"><i class="fa fa-clock-o"></i></span><span class="pCalendar-showOnHover pCalendar-blueFont">Scheduled</span>';
                encStatusBullet = '<span class="pCalendar-bullet pCalendar-bullet-xs pCalendar-badge-scheduled"><i class="fa fa-clock-o"></i></span>';
            }
            if (row.Status == 'Rescheduled') {
                encStatusHtml = '<span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-rescheduled"><i class="fa fa-reply"></i></span><span class="pCalendar-showOnHover pCalendar-blueFont">Rescheduled</span>';
                encStatusBullet = '<span class="pCalendar-bullet pCalendar-bullet-xs pCalendar-badge-rescheduled"><i class="fa fa-reply"></i></span>';
            }
            if (row.Status == 'ReadyToCode') {
                encStatusHtml = '<span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-readyToCode"><i class="fa fa-check"></i></span><span class="pCalendar-showOnHover pCalendar-yellowFont">Ready To Code</span>';
                encStatusBullet = '<span class="pCalendar-bullet pCalendar-bullet-xs pCalendar-badge-readyToCode"><i class="fa fa-check"></i></span>';
            }

            if(row.Status == 'Open'){
                encStatusHtml = '<span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-open"><i class="fa fa-hourglass-2"></i></span><span class="pCalendar-showOnHover pCalendar-yellowFont">Open</span>';
                encStatusBullet = '<span class="pCalendar-bullet pCalendar-bullet-xs pCalendar-badge-open"><i class="fa fa-hourglass-2"></i></span>';
            }

            if (row.Status == 'Draft') {
                encStatusHtml = '<span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-draft"><i class="fa fa-file-o"></i></span><span class="pCalendar-showOnHover pCalendar-yellowFont">Draft</span>';
                encStatusBullet = '<span class="pCalendar-bullet pCalendar-bullet-xs pCalendar-badge-draft"><i class="fa fa-file-o"></i></span>';
            }

            $('.pCalendar-Main .pCalendar-bodyRow[data-slot="' + slot + '"]').append(
                    '<td rowSpan="' + row.Span + '" class="pCalendar-dailyTableRow pCalendar-active pCalendar-hoverable">' +
                        '<div class="pCalendar-tileInfo">' +
                            '<div class="memberOnly">' +
                                '<span class="pCalendar-boldFont">' + row.Description.MemberName + '</span>' +
                                encStatusBullet +
                                //'<span>Provider: </span>' + '<span class="pCalendar-boldFont">' + row.Description.ProviderName + '</span>' + '<br/>' +
                                //'<span>Facility: </span>' + '<span class="pCalendar-boldFont">' + row.Description.Facility + '</span><br/>' +
                                //'<span>Chief Complaint: </span>' + '<span class="pCalendar-boldFont">' + row.Description.ChiefComplaint + '</span>' + '<br/>' +
                            '</div>' +
                            '<div class="allDetails">' +
                                '<div class="pCalendar-encounterStatus">' +
                                    encStatusHtml +
                                '</div>' +
                                '<span>Member: </span>' + '<span class="pCalendar-boldFont">' + row.Description.MemberName + '</span>' + '<br/>' +
                                '<span>Provider: </span>' + '<span class="pCalendar-boldFont">' + row.Description.ProviderName + '</span>' + '<br/>' +
                                '<span>Facility: </span>' + '<span class="pCalendar-boldFont">' + row.Description.Facility + '</span><br/>' +
                                '<span>Chief Complaint: </span>' + '<span class="pCalendar-boldFont">' + row.Description.ChiefComplaint + '</span>' + '<br/>' +
                            '</div>' +
                        '</div>' +
                    '</td>'
                );
            $('.allDetails').css('display', 'none');
        }
        adjustTable(dayEvents);
    }

    //Render The Weekly Calendar
    function renderWeeklyCalendar() {
        var i = 0;
        var selectedDate = new Date(selectedYear, selectedMonth - 1, selectedDay);
        var day = selectedDate.getDay();
        var startDay = selectedDay - day + 1;
        if (day == 0) {
            startDay -= 7;
        }
        renderControls('weekly');
        var bodyRow = '<tr class="pCalendar-bodyRow">';
        for (i = 0; i < 7; i++) {
            bodyRow += '<td class="pCalendar-active" data-cellid="' + (parseInt(startDay) + i) + '">' +
                            '<div class="pCalendar-eventInfo">' +
                                GetCalendarEvents(new Date(selectedYear, parseInt(selectedMonth) - 1, (parseInt(startDay) + i))) +
                             '</div>' +
                         '</td>';
        }
        bodyRow += '</tr>';
        $(divName + ' .pCalendar-Main').append(bodyRow);
        $('span[data-toggle="tooltip"]').tooltip();
    }

    //Get The Bullets For Event Summary As an HTML DOM String Using The Summary Template
    function GetEventsAsDOMString(eventSummaryModel) {
        var m = 0;
        var totalElementsToBeDisplayed = 0;
        var outerDiv = '<div class="pCalendar-Badges">';
        for (m in eventSummaryModel) {
            if (eventSummaryModel[m].Count > 0) {
                totalElementsToBeDisplayed++;
            }
        }
        if (totalElementsToBeDisplayed <= 4) {
            outerDiv += '</br>';
            var counter = 0;
            for (m in eventSummaryModel) {
                if (eventSummaryModel[m].Count > 0) {
                    outerDiv += eventSummaryModel[m].UIElement(eventSummaryModel[m].Count);
                    counter++;
                }
            }
        }
        else {
            var counter = 0;
            for (m in eventSummaryModel) {
                if (eventSummaryModel[m].Count > 0) {
                    if (counter < 4) {
                        outerDiv += eventSummaryModel[m].UIElement(eventSummaryModel[m].Count);
                    }
                    else if (counter == 4) {
                        outerDiv += '</br>' + eventSummaryModel[m].UIElement(eventSummaryModel[m].Count);
                    }
                    else {
                        outerDiv += eventSummaryModel[m].UIElement(eventSummaryModel[m].Count);
                    }
                    counter++;
                }
            }
        }
        return outerDiv + '</div>';
    }

    //=================================================================================================
    //===================   CUSTOM EVENT HANDLERS AND FUNCTIONS  ======================================
    //=================================================================================================

    //Fires When The Selected Date Has Been Changed
    $(divName).off('dateChanged').on('dateChanged', function (event) {
        initializeApplication();
    });

    //Hover Events For Daily Calendar
    $(divName).off('mouseenter', '.pCalendar-Main .pCalendar-bodyRow .pCalendar-active').on('mouseenter', '.pCalendar-Main .pCalendar-bodyRow .pCalendar-active', function (evt) {
        $(this).find('.pCalendar-tileInfo').css('position', 'absolute').css('top', '0px').css('left', '0px');
        $(this).find('.allDetails').css('display', 'block');
    });
    $(divName).off('mouseleave', '.pCalendar-Main .pCalendar-bodyRow .pCalendar-active').on('mouseleave', '.pCalendar-Main .pCalendar-bodyRow .pCalendar-active', function (evt) {
        $(this).find('.pCalendar-tileInfo').css('position', 'relative');
        $(this).find('.allDetails').css('display', 'none');
    });

    //Filter The Array By Using The Status Flag
    function ImplementStatusFilter() {
        if (statusFilter != 'off') {
            for (i in dailyCalData) {
                if (dailyCalData[i].Status == statusFilter) {
                    filteredEvents.push(jQuery.extend(true, {}, dailyCalData[i]));
                }
            }
        }
        else {
            filteredEvents = jQuery.extend(true, {}, dailyCalData);
        }
    }

    //Get The Day Name Of a Given Date
    function GetDayNameIndex(year, month, day) {
        var requiredDate = new Date(year, month, day);
        var dateString = requiredDate.toString();
        var index = 0;
        var date = dateString.split(' ')[0];
        for (i in daysInShort) {
            if (date == daysInShort[i]) {
                index = i;
                break;
            }
        }
        return parseInt(index);
    }

    //Intitialize The Calendar
    function initializeApplication() {
        if (calendarType == 'yearly'){
            filteredEvents = getEventsByYearFromService(selectedYear);
            renderYearlyCalendar();
        }
            
        if (calendarType == 'monthly'){
            filteredEvents = getEventsByMonthFromService(selectedYear, selectedMonth);
            renderMonthlyCalendar();
        }
            
        if (calendarType == 'daily') {
            filteredEvents = getEventsByDateFromService(selectedYear, selectedMonth, selectedDay);
            renderDailyCalendar();
        }
        if (calendarType == 'weekly'){
            filteredEvents = getEventsByMonthFromService(selectedYear, selectedMonth);
            renderWeeklyCalendar();
        }
           

        $(divName).find('.pCalendar-eventInfo.desc').hide();
    }

    //Function To Calculate The Adjustment Index Of The Table
    function calculateAdjustmentIndex(StartTime, EndTime, DifferenceInMins, events) {
        var arraySize = (EndTime.getTime() - StartTime.getTime()) / (DifferenceInMins * 60000);
        var adjIndex = Array.apply(null, Array(arraySize)).map(Number.prototype.valueOf, 0);
        var max = 5;

        if (events == null) {
            events = GetEventsForDay(new Date(selectedYear, selectedMonth - 1, selectedDay, 0, 0, 0));
        }

        for (i in events) {
            for (j = 0; j < events[i].Span; j++) {
                adjIndex[events[i].Slot + j]++;
            }
        }
        for (i in adjIndex) {
            if (adjIndex[i] > max)
                max = adjIndex[i];
        }
        return {
            "MaximumRows": max,
            "AdjustmentIndex": adjIndex
        }
    }

    //Function To Adjust The Table as a Calendar
    function adjustTable(events) {
        var adjustment = calculateAdjustmentIndex(StartTime, EndTime, slotInterval, events);
        var adjustmentIndex = adjustment.AdjustmentIndex;
        var max = adjustment.MaximumRows;

        var htmlTdString = '';
        for (i = 0; i < adjustmentIndex.length; i++) {
            htmlTdString = '';
            for (j = 0; j < max - adjustmentIndex[i]; j++) {
                htmlTdString += '<td class="pCalendar-dailyTableRow"></td>';
            }
            $('.pCalendar-Main .pCalendar-bodyRow[data-slot="' + i + '"]').append(htmlTdString);
        }
    }

    //Function To Get The Calendar Events
    function GetCalendarEvents(date) {
        var dayEventList = GetEventsByDate(date);
        var eventSummaryModel = BuildEventSummary(dayEventList);
        return GetEventsAsDOMString(eventSummaryModel);
        //var today = new Date();
        //if(statusFilter != 'off'){
        //    switch (statusFilter) {
        //        case 'NoShow':
        //            return '<div class="pCalendar-Badges"><br/><span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-noShow" data-toggle="tooltip" data-placement="top" title="No Show: 10">10</span></div>';
        //        case 'Draft':
        //            return '<div class="pCalendar-Badges"><br/><span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-draft" data-toggle="tooltip" data-placement="top" title="Draft: 10">10</span></div>';
        //        case 'Active':
        //            return '<div class="pCalendar-Badges"><br/><span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-active" data-toggle="tooltip" data-placement="top" title="Active: 10">10</span></div>';
        //        case 'Scheduled':
        //            return '<div class="pCalendar-Badges"><br/><span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-scheduled" data-toggle="tooltip" data-placement="top" title="Scheduled: 10">10</span></div>';
        //        case 'Rescheduled':
        //            return '<div class="pCalendar-Badges"><br/><span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-rescheduled" data-toggle="tooltip" data-placement="top" title="Rescheduled: 10">10</span></div>';
        //        case 'Open':
        //            return '<div class="pCalendar-Badges"><br/><span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-open" data-toggle="tooltip" data-placement="top" title="Open: 10">10</span></div>';
        //        case 'ReadyToCode':
        //            return '<div class="pCalendar-Badges"><br/><span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-readyToCode" data-toggle="tooltip" data-placement="top" title="Ready To Code: 10">10</span></div>';
        //    }
        //}
        ////Past Events
        //if (checkDates(date, today) == 1) {
        //    return '<div class="pCalendar-Badges">' + '<br/>' +
        //                      '<span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-noShow" data-toggle="tooltip" data-placement="top" title="No Show: 10">10</span>' +
        //                      '<span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-open" data-toggle="tooltip" data-placement="top" title="Open: 10">10</span>' +
        //                      '<span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-draft" data-toggle="tooltip" data-placement="top" title="Draft: 10">10</span>' +
        //                      '<span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-readyToCode" data-toggle="tooltip" data-placement="top" title="Ready To Code: 10">10</span>' +
        //                '</div>';
        //}

        ////Future Events
        //if (checkDates(date, today) == -1) {
        //    return '<div class="pCalendar-Badges">' + '<br/>' +
        //                     '<span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-scheduled" data-toggle="tooltip" data-placement="top" title="Scheduled: 10">10</span>' +
        //           '</div>';
        //}

        ////Today Events
        //if (checkDates(date, today) == 0) {
        //    return '<div class="pCalendar-Badges">' +
        //                           '<span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-noShow" data-toggle="tooltip" data-placement="top" title="No Show: 10">10</span>' +
        //                           '<span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-active" data-toggle="tooltip" data-placement="top" title="Active: 10">10</span>' +
        //                           '<span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-scheduled" data-toggle="tooltip" data-placement="top" title="Scheduled: 10">10</span>' +
        //                           '<span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-rescheduled" data-toggle="tooltip" data-placement="top" title="Rescheduled: 10">10</span>' + '<br/>' +
        //                           '<span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-open" data-toggle="tooltip" data-placement="top" title="Open: 10">10</span>' +
        //                           '<span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-draft" data-toggle="tooltip" data-placement="top" title="Drafts: 10">10</span>' +
        //                                           '<span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-readyToCode" data-toggle="tooltip" data-placement="top" title="Ready To Code: 10">10</span>' +

        //         '</div>' +
        //    '</div>';
        //}
    }

    //Get The Event String For A Month
    function GetCalendarEventsForMonth(year, month) {
        var i = 0;
        var allEvents = [];
        for (i in filteredEvents) {
            if (filteredEvents[i].StartTime.getFullYear() == year && filteredEvents[i].StartTime.getMonth() == month)
                allEvents.push($.extend(true, {}, filteredEvents[i]));
        }

        var summaryModel = BuildEventSummary(allEvents);
        return GetEventsAsDOMString(summaryModel);
    }

    //Build the Summary Template With the Supplied Events
    function BuildEventSummary(events) {
        var structureTemplate = $.extend(true, {}, eventSummaryTemplate);
        for (i in events) {
            for (j in structureTemplate) {
                if (events[i].Status == structureTemplate[j].EventTitle) {
                    structureTemplate[j].Count++;
                    break;
                }
            }
        }

        return structureTemplate;
    }

    //Get The Events in a Particular Date
    function GetEventsByDate(date) {
        var calData = [];
        for (i in filteredEvents) {
            if (checkDates(filteredEvents[i].StartTime, date) == 0) {
                var dataItem = new Object();
                dataItem.Description = $.extend(true, {}, filteredEvents[i].Description);
                dataItem.Status = filteredEvents[i].Status;
                dataItem.StartTime = filteredEvents[i].StartTime;
                dataItem.EndTime = filteredEvents[i].EndTime;
                calData.push(dataItem);
            }
        }
        return calData;
    }

    //Get The Events String For A Day
    function GetEventsForDay(date) {
        var calData = [];

        StartTime = new Date(selectedYear, selectedMonth - 1, selectedDay, 9, 0, 0);
        //filteredEvents = [];
        //if (statusFilter != 'off') {
        //    for (i in dailyCalData) {
        //        if (dailyCalData[i].Status == statusFilter)
        //            filteredEvents.push(dailyCalData[i]);
        //    }
        //}
        //else {
        //    filteredEvents = dailyCalData;
        //}
        //var selectedDate = new Date(selectedYear, selectedMonth - 1, selectedDay, 0, 0, 0);
        

        for (i in filteredEvents) {
            if (checkDates(filteredEvents[i].StartTime, date) == 0) {
                var dataItem = new Object();
                dataItem.Description = filteredEvents[i].Description;
                dataItem.Status = filteredEvents[i].Status;

                //Calculate The Slot Value For Element
                var startIndex = parseInt(getDifferenceInMinutes(StartTime, filteredEvents[i].StartTime) / slotInterval);
                dataItem.Slot = startIndex;

                //Calculate The Span Value For The Element
                var slotTime = new Date(StartTime.getTime());
                addMinutes(slotTime, slotInterval * dataItem.Slot);
                var endIndex = parseInt(getDifferenceInMinutes(slotTime, filteredEvents[i].EndTime) / slotInterval);
                dataItem.Span = endIndex + 1;

                calData.push(dataItem);
            }
        }

        return calData;
    }

    //Function To Compare Two Dates (>, < and = available)
    function checkDates(date1, date2) {
        var val = 0;
        if (date2 > date1) {
            val = 1;
        }
        if (date2 < date1) {
            val = -1;
        }
        if ((date2.getFullYear() == date1.getFullYear()) && (date2.getMonth() == date1.getMonth()) && (date2.getDate() == date1.getDate())) {
            val = 0;
        }
        return val;
    }

    //Get The Summary Of Events From A Start Date To an End Date
    function GetSummaryOfEvents(startDate, endDate) {
        var calData = [];
        for (i in filteredEvents) {
            if (checkDates(startDate, filteredEvents[i].StartTime) == 1 && checkDates(filteredEvents[i].StartTime, endDate) == 1)
                calData.push($.extend(true, {}, filteredEvents[i]));
        }

        var summaryModel = BuildEventSummary(calData);
        return GetEventsAsDOMString(summaryModel);
        //var currentDate = startDate;
        //var hasPastDays = false;
        //var hasFutureDays = false;
        //var today = new Date();
        //while (currentDate < endDate) {
        //    if (checkDates(currentDate, today) == 1)
        //        hasPastDays = true;
        //    if (checkDates(currentDate, today) == -1)
        //        hasFutureDays = true;

        //    currentDate = new Date(currentDate.getFullYear(), currentDate.getMonth(), currentDate.getDate() + 1);
        //}
        //if (endDate != null) {
        //    if (hasPastDays && !hasFutureDays) {
        //        return '<span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-noShow" data-val="NoShow" data-toggle="tooltip" data-placement="top" title="No Show: 10">10</span>' +
        //               '<span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-open" data-val="Open" data-toggle="tooltip" data-placement="top" title="Open: 10">10</span>' +
        //               '<span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-draft" data-val="Draft" data-toggle="tooltip" data-placement="top" title="Draft: 10">10</span>' +
        //               '<span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-readyToCode" data-val="ReadyToCode" data-toggle="tooltip" data-placement="top" title="Ready To Code: 10">10</span>';
        //    }
        //    if (!hasPastDays && hasFutureDays) {
        //        return '<span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-scheduled" data-val="Scheduled" data-toggle="tooltip" data-placement="top" title="Scheduled: 10">10</span>';
        //    }
        //    if (hasPastDays && hasFutureDays) {
        //        return '<span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-noShow" data-val="NoShow" data-toggle="tooltip" data-placement="top" title="No Show: 10">10</span>' +
        //               '<span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-active" data-val="Active" data-toggle="tooltip" data-placement="top" title="Active: 10">10</span>' +
        //               '<span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-scheduled" data-val="Scheduled" data-toggle="tooltip" data-placement="top" title="Scheduled: 10">10</span>' +
        //               '<span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-rescheduled" data-val="Rescheduled" data-toggle="tooltip" data-placement="top" title="Rescheduled: 10">10</span>' + '<br/>' +
        //               '<span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-readyToCode" data-val="ReadyToCode" data-toggle="tooltip" data-placement="top" title="Ready To Code: 10">10</span>' +
        //               '<span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-open" data-val="Open" data-toggle="tooltip" data-placement="top" title="Open: 10">10</span>' +
        //               '<span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-draft" data-val="Draft" data-toggle="tooltip" data-placement="top" title="Drafts: 10">10</span>';
        //    }
        //}
        //else {
        //    if (checkDates(startDate, today) == 1)
        //        return '<span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-noShow" data-val="NoShow" data-toggle="tooltip" data-placement="top" title="No Show: 10">10</span>' +
        //               '<span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-open" data-val="Open" data-toggle="tooltip" data-placement="top" title="Open: 10">10</span>' +
        //               '<span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-draft" data-val="Draft" data-toggle="tooltip" data-placement="top" title="Draft: 10">10</span>' +
        //               '<span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-readyToCode" data-val="ReadyToCode" data-toggle="tooltip" data-placement="top" title="Ready To Code: 10">10</span>';
        //    if (checkDates(startDate, today) == -1)
        //        return '<span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-scheduled" data-val="Scheduled" data-toggle="tooltip" data-placement="top" title="Scheduled: 10">10</span>';
        //    if (checkDates(startDate, today) == 0)
        //        return '<span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-noShow" data-val="NoShow" data-toggle="tooltip" data-placement="top" title="No Show: 10">10</span>' +
        //               '<span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-active" data-val="Active" data-toggle="tooltip" data-placement="top" title="Active: 10">10</span>' +
        //               '<span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-scheduled" data-val="Scheduled" data-toggle="tooltip" data-placement="top" title="Scheduled: 10">10</span>' +
        //               '<span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-rescheduled" data-val="Rescheduled" data-toggle="tooltip" data-placement="top" title="Rescheduled: 10">10</span>' + '<br/>' +
        //               '<span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-readyToCode" data-val="ReadyToCode" data-toggle="tooltip" data-placement="top" title="Ready To Code: 10">10</span>' +
        //               '<span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-open" data-val="Open" data-toggle="tooltip" data-placement="top" title="Open: 10">10</span>' +
        //               '<span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-draft" data-val="Draft" data-toggle="tooltip" data-placement="top" title="Drafts: 10">10</span>';
        //}
    }

    //Get The Event Summary For Daily View
    function GetSummaryOfEventsForDay(date) {
        var calData = GetEventsByDate(date);
        var summaryModel = BuildEventSummary(calData);
        return GetEventsAsDOMString(summaryModel);
    }

    //-------------------- Deprecated --------------------------
    function getEncounterList() {
        var result;
        $.ajax({
            url: '/Encounters/EncounterList/GetEncounterListNoActionsPartial?type=active',
            method: 'GET',
            async: false
        }).done(function (response) {
            result = response;
        });

        return result;
    }

    //Generate The Time Intervals For The Daily Calendar View
    function GetSlottedTimeIntervals(StartTime, EndTime, SlotInterval) {
        var progressCounter = 0;
        var slottedTimeString = "";
        var current = new Date(StartTime.getTime());
        while (current < EndTime) {
            slottedTimeString += '<tr class="pCalendar-bodyRow" data-slot="' + progressCounter + '"><td class="timeHolder pCalendar-dailyTableRow">' + formatDateForDailyCalendar(current) + '</td></tr>';
            addMinutes(current, 15);
            progressCounter++;
        }
        return slottedTimeString;
    }

    //Format The Time String To be Displayed in the Daily Calendar 
    function formatDateForDailyCalendar(date) {
        var hrs24 = date.getHours();
        var hrs12 = hrs24 > 12 ? hrs24 % 12 : hrs24;
        var min = date.getMinutes();
        var tod = hrs24 > 12 ? 'PM' : 'AM'
        min = min.toString();
        if (min.length == 1)
            min = '0' + min;
        var dateString = hrs12 + ':' + min + ' ' + tod;
        
        return dateString;
    }
}

//=================================================================================================
//================================   SERVICE CALLS  ===============================================
//=================================================================================================
function getEventsByDateFromService(year, month, day) {
    var i = 0;
    var calData = [];
    $.ajax({
        url: '/Encounters/Scheduler/GetEventsForDay?year=' + year + '&month=' + month + '&day=' + day,
        async: false,
        method: 'GET'
    }).done(function (response) {
        calData = response;
    });
    return FormatAJAXEventCollection(calData);
}

function getEventsByMonthFromService(year, month) {
    var i = 0;
    var calData = [];
    $.ajax({
        url: '/Encounters/Scheduler/GetEventsForMonth?year=' + year + '&month=' + month,
        async: false,
        method: 'GET'
    }).done(function (response) {
        calData = response;
    });
    return FormatAJAXEventCollection(calData);
}

function getEventsByYearFromService(year) {
    var i = 0;
    var calData = [];
    $.ajax({
        url: '/Encounters/Scheduler/GetEventsForYear?year=' + year,
        async: false,
        method: 'GET'
    }).done(function (response) {
        calData = response;
    });
    return FormatAJAXEventCollection(calData);
}

//=================================================================================================
//===================   CUSTOM EVENT HANDLERS AND FUNCTIONS  ======================================
//=================================================================================================

function convertDotNetDateTime(dateTime) {
    var milli = dateTime.replace(/\/Date\((-?\d+)\)\//, '$1');
    return new Date(parseInt(milli));
}

function FormatAJAXEventCollection(events) {
    for (i in events) {
        events[i].StartTime = convertDotNetDateTime(events[i].StartTime);
        events[i].EndTime = convertDotNetDateTime(events[i].EndTime);
    }
    return events;
}

//----------------------------------------------------------------------------------------------------------------------------
//----------------------------------- SUMMARY MANAGER TEMPLATES --------------------------------------------------------------
//----------------------------------------------------------------------------------------------------------------------------

//Bullet Styles Customized By Value Length
var bulletStyle_oneDigit = 'padding-left: 5px !important; padding-right: 5px !important; padding-top: 1px !important; padding-bottom: 1px !important';
var bulletStyle_twoDigits = 'padding: 2px !important';

//Template For Calculating And Generating The Summary Bullets
var eventSummaryTemplate = [
            {
                "EventTitle": "NoShow",
                "Count": 0,
                "UIElement": function (value) {
                    var style = '';
                    if (value.toString().length == 1)
                        style = bulletStyle_oneDigit;
                    else
                        style = bulletStyle_twoDigits;
                    return value > 0 ? '<span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-noShow" style="' + style + '" data-toggle="tooltip" data-placement="top" title="No Shows">' + value + '</span>' : '';
                }
            },
            {
                "EventTitle": "ReadyToCode",
                "Count": 0,
                "UIElement": function (value) {
                    var style = '';
                    if (value.toString().length == 1)
                        style = bulletStyle_oneDigit;
                    else
                        style = bulletStyle_twoDigits;
                    return value > 0 ? '<span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-readyToCode" style="' + style + '" data-toggle="tooltip" data-placement="top" title="Ready To Code">' + value + '</span>' : '';
                }
            },
            {
                "EventTitle": "Active",
                "Count": 0,
                "UIElement": function (value) {
                    var style = '';
                    if (value.toString().length == 1)
                        style = bulletStyle_oneDigit;
                    else
                        style = bulletStyle_twoDigits;
                    return value > 0 ? '<span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-active" style="' + style + '" data-toggle="tooltip" data-placement="top" title="Active">' + value + '</span>' : '';
                }

            },
            {
                "EventTitle": "Scheduled",
                "Count": 0,
                "UIElement": function (value) {
                    var style = '';
                    if (value.toString().length == 1)
                        style = bulletStyle_oneDigit;
                    else
                        style = bulletStyle_twoDigits;
                    return value > 0 ? '<span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-scheduled" style="' + style + '" data-toggle="tooltip" data-placement="top" title="Scheduled">' + value + '</span>' : '';
                }
            },
            {
                "EventTitle": "Rescheduled",
                "Count": 0,
                "UIElement": function (value) {
                    var style = '';
                    if (value.toString().length == 1)
                        style = bulletStyle_oneDigit;
                    else
                        style = bulletStyle_twoDigits;
                    return value > 0 ? '<span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-rescheduled" style="' + style + '" data-toggle="tooltip" data-placement="top" title="Rescheduled">' + value + '</span>' : '';
                }
            },
            {
                "EventTitle": "Open",
                "Count": 0,
                "UIElement": function (value) {
                    var style = '';
                    if (value.toString().length == 1)
                        style = bulletStyle_oneDigit;
                    else
                        style = bulletStyle_twoDigits;
                    return value > 0 ? '<span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-open" style="' + style + '" data-toggle="tooltip" data-placement="top" title="Open">' + value + '</span>' : '';
                }
            },
            {
                "EventTitle": "Draft",
                "Count": 0,
                "UIElement": function (value) {
                    var style = '';
                    if (value.toString().length == 1)
                        style = bulletStyle_oneDigit;
                    else
                        style = bulletStyle_twoDigits;
                    return value > 0 ? '<span class="pCalendar-badge pCalendar-badge-sm pCalendar-badge-draft" style="' + style + '" data-toggle="tooltip" data-placement="top" title="Drafts">' + value + '</span>' : '';
                }
            }
];


