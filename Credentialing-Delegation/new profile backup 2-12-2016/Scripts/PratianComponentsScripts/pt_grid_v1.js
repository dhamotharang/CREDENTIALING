
(function ($) {
    $.fn.prtGrid = function (options) {
        // pk's default settings:
        var defaults = {
            url: "",
            dataLength: 50,
            columns: [],
            height: 300,
            filterable: true,
            externalFactors: []
        };

        var settings = $.extend({}, defaults, options);
        return this.each(function () {

            //---------------variable declaration-----------------------------
            var $table = $(this);
            var tableId = $table.attr('id');
            var $tbody = $table.find('tbody');
            var $thead = $table.find('thead');
            var $th = $thead.find('th');
            var $tr = $tbody.find('tr');
            var columnInfo = settings.columns;
            var columnCount = columnInfo.length;
            var primaryUrl = settings.url + '?CountOfList=' + settings.dataLength + '&';
            var index = 0;
            var $parent = $table.parent();
            var parentDivWidth = $parent.width();
            var theadTemplate = '<tr>';
            var hearderText;
            var sortIcon;
            var isApplied;
            var htmlObject;
            var externalFactors = settings.externalFactors;
            var timeoutEx = null;

            //--------------------external factors-------------------------

            function updateURLBasedOnExternalFactors(externalFactors, url) {
                var currentFactor;
                var currenturl = url;
                var currExternElement;
                for (var i = 0; i < externalFactors.length; i++) {
                    currentFactor = externalFactors[i];
                    if (currentFactor.type === "radio" || currentFactor.type === "checkbox") {
                        currExternElement = $("[name='" + currentFactor.name + "']:checked");
                    } else {
                        currExternElement = $("[name='" + currentFactor.name + "']");
                    }
                    currenturl = currenturl + currentFactor.name + '=' + currExternElement.val() + '&';
                }
                return currenturl;
            }

            function addClassToExternalFilterelements(externalFactors) {

                for (var i = 0; i < externalFactors.length; i++) {
                    currentFactor = externalFactors[i];
                    currExternElement = $("[name='" + currentFactor.name + "']");
                    currExternElement.addClass("external-filter-element-" + tableId);
                }
            }

            addClassToExternalFilterelements(externalFactors);

            // Listen for keystroke events
            $('.external-filter-element-' + tableId).on('change', function (e) {
                clearTimeout(timeoutEx);
                timeoutEx = setTimeout(function () {
                    CreateUrl();
                }, 1000);
            });


            //---------------add class to table------------------------
            $table.addClass("pt-table");
            if ($tbody.hasClass("pt-table-tbody")) {
                isApplied = true;
            } else {
                isApplied = false;
                $tbody.addClass("pt-table-tbody");
            }

            //-----------------add height to tbody------------------------
            $tbody.css({ 'max-height': settings.height });

            //-----------------sortable header-----------------------------
            $th.each(function (i) {
                hearderText = $(this).text();
                var currColumnInfo = columnInfo[i];
                if (currColumnInfo.sortable && currColumnInfo.sortable.isSort) {
                    if (currColumnInfo.sortable.defaultSort === null) {
                        $(this).html('<span data-container="' + columnInfo[i].name + '" class="sortable-th">' + hearderText + '<i class="fa fa-sort ptr-sort"></i></span>');
                    } else if (currColumnInfo.sortable.defaultSort === 'ASC') {
                        $(this).html('<span data-container="' + columnInfo[i].name + '" class="sortable-th">' + hearderText + '<i class="fa fa-sort-asc ptr-sort"></i></span>');
                    } else if (currColumnInfo.sortable.defaultSort === 'DESC') {
                        $(this).html('<span data-container="' + columnInfo[i].name + '" class="sortable-th">' + hearderText + '<i class="fa fa-sort-desc ptr-sort"></i></span>');
                    }
                }

            });
            $('.sortable-th').on('click', function () {

                $('input.filter-element').val('');
                sortIcon = $(this).find('.ptr-sort');
                // url = settings.url;
                if (sortIcon.hasClass('fa-sort')) {
                    sortIcon.removeClass('fa-sort').addClass('fa-sort-asc');
                    url = updateURLBasedOnExternalFactors(externalFactors, primaryUrl) + "index=" + index + "&sortingType=asc&sortBy=" + $(this).attr('data-container');
                }
                else if (sortIcon.hasClass('fa-sort-asc')) {
                    sortIcon.removeClass('fa-sort-asc').addClass('fa-sort-desc');
                    url = updateURLBasedOnExternalFactors(externalFactors, primaryUrl) + "index=" + index + "&sortingType=desc&sortBy=" + $(this).attr('data-container');
                }
                else {
                    sortIcon.removeClass('fa-sort-desc').addClass('fa-sort');
                    url = updateURLBasedOnExternalFactors(externalFactors, primaryUrl) + "index=" + index + "&sortingType=none&sortBy=" + $(this).attr('data-container');
                }

                GetData(url);
            });

            //--------------------ajax call--------------------------------
            function GetData(url) {
                StartLoading();
                index = 0;
                $.ajax({
                    url: url,
                    type: 'GET',
                    cache: false,
                    success: function (result) {
                        $tbody.html(result).find('.modal').modal({
                            show: true
                        });

                        SetTbodyWidthInTable();
                        StopLoading();
                        CheckForDataAvailability();
                    }
                });
            }

            //-----------------------------create filter input elements-----------------------------------
            //----------------------------configuration for filter inputs-------------------------------

            function GetFilterElementTemplate(columnInfo, colWidth, index) {

                //---------------------------create template of select element--------------------------------
                function CreateSelectFilterElement(columnInfo, colWidth, index) {
                    var selectTemplate = '<td style="width:' + columnInfo.widthPercentage + '%"><select id="filter_textfield_' + index + '" class="filter-element form-control" value="" >';
                    for (var i = 0; i < columnInfo.values.length; i++) {
                        var currentSelectValue = columnInfo.values[i];
                        selectTemplate = selectTemplate + '<option value="' + currentSelectValue.value + '">' + currentSelectValue.text + '</option>';
                    }
                    selectTemplate = selectTemplate + "</select></td>";
                    return selectTemplate;
                }

                //------------------------configuration object-------------------------------------
                var filterElementsConfig = {
                    "none": '<td style="width:' + columnInfo.widthPercentage + '%"></td>',
                    "text": '<td style="width:' + columnInfo.widthPercentage + '%"><input type="text" id="filter_textfield_' + index + '" placeholder="' + columnInfo.text + '" name="' + columnInfo.name + '" class="filter-element form-control" value="" /></td>',
                    "date": '<td style="width:' + columnInfo.widthPercentage + '%"><input type="text" id="filter_textfield_' + index + '" placeholder="' + columnInfo.text + '" name="' + columnInfo.name + '" class="filter-element date-element form-control" value="" /></td>',
                    "select": columnInfo.type === "select" ? CreateSelectFilterElement(columnInfo, colWidth, index) : ""
                };
                return filterElementsConfig[columnInfo.type];
            }


            if (!isApplied) {
                //---------------------- add filter box in thead -----------------------------
                if (settings.filterable) {
                    for (var i = 0; i < columnInfo.length; i++) {
                        colWidth = columnInfo[i].widthPercentage * parentDivWidth / 100;
                        theadTemplate = theadTemplate + GetFilterElementTemplate(columnInfo[i], colWidth, i);
                    }
                    theadTemplate = theadTemplate + '</tr>';
                    $thead.append(theadTemplate);

                    //--------------------------date pickers--------------------------------
                    $thead.find('.date-element').each(function () {
                        $(this).datetimepicker({ format: 'MM/DD/YYYY', useCurrent: false });
                        $(this).mask('99/99/9999');
                    });

                    //-----------------------------filtering--------------------------------
                    var timeout = null;

                    // Listen for keystroke events
                    $table.find('.filter-element').on('keyup', function (e) {
                        clearTimeout(timeout);
                        timeout = setTimeout(function () {
                            CreateUrl();
                        }, 1000);
                    });
                }


                function CreateUrl() {
                    url = updateURLBasedOnExternalFactors(externalFactors, primaryUrl);
                    var urlPath = url + "index=" + index;
                    for (var i = 0; i < columnInfo.length; i++) {
                        if (columnInfo[i].type != 'none')
                            urlPath = urlPath + "&" + columnInfo[i].name + "=" + $table.find('input.filter-element[name="' + columnInfo[i].name + '"]').val();
                    }
                    GetData(urlPath);
                }
                //------------------set th column width---------------------------------
                for (var i = 0; i < columnInfo.length; i++) {
                    $($th[i]).css({ 'width': columnInfo[i].widthPercentage + '%' });
                }

                //---------------------------scroll event----------------------------------
                $tbody.on('scroll', function () {
                    if ($(this).scrollTop() + $(this).innerHeight() >= $(this)[0].scrollHeight) {
                        url = updateURLBasedOnExternalFactors(externalFactors, primaryUrl);
                        var urlPath = url + "index=" + index;
                        for (var i = 0; i < columnInfo.length; i++) {
                            if (columnInfo[i].type != 'none')
                                urlPath = urlPath + "&" + columnInfo[i].name + "=" + $table.find('input.filter-element[name="' + columnInfo[i].name + '"]').val();
                        }
                        StartLoading();
                        $.ajax({
                            url: urlPath,
                            type: 'GET',
                            cache: false,
                            success: function (result) {

                                SetColumnWidthOnScroll(result);
                                index = index + htmlObject.length;
                                StopLoading();
                                CheckForDataAvailability();
                            }
                        });
                    }
                })
            }

            //----------------------set column width on scroll------------------------------
            function SetColumnWidthOnScroll(result) {
                htmlObject = $('<div></div>').html(result).children();
                htmlObject.each(function () {
                    $(this).find('td').each(function (i) {
                        $(this).css({ 'width': columnInfo[i].widthPercentage + '%' });
                    });
                });

                $tbody.append(htmlObject).find('.modal').modal({
                    show: true
                });

            }

            //---------------------------- set tbody width----------------------------------

            function SetTbodyWidthInTable() {
                $tr = $tbody.find('tr');
                $tr.each(function () {
                    $(this).find('td').each(function (i) {
                        $(this).css({ 'width': columnInfo[i].widthPercentage + '%' });
                    });
                });
            }
            SetTbodyWidthInTable();

            //-------------------------set column width in thead--------------------------
            function SetTheadWidthInTable() {
                $thead.find('tr').each(function () {
                    $(this).find('th').each(function (i) {
                        $(this).css({ 'width': columnInfo[i].widthPercentage + '%' });
                    });
                    if (settings.filterable) {
                        $(this).find('td').each(function (i) {
                            $(this).css({ 'width': columnInfo[i].widthPercentage + '%' });
                        });
                    }
                });
            }

            //----------------------------------footer of table----------------------------------
            function StartLoading() {
                $tbody.addClass('loading-tbody');
                $thead.find('td').addClass('loading-tbody');
                $table.append('<tfoot><tr><td colspan="' + columnCount + '">Loading...</td></tr></tfoot>');
            }

            function StopLoading() {
                $tbody.removeClass('loading-tbody');
                $thead.find('td').removeClass('loading-tbody');
                $table.find('tfoot').remove();
            }

            //----------------------------------data availability check--------------------

            function CheckForDataAvailability() {
                var countOfData = $tbody.find('tr').length;
                if (countOfData === 0) {
                    $tbody.append('<tr><td class="no-data-message" colspan="' + columnCount + '">No Data Available</td></tr>');
                }
            }


            //---------------------------------responsive--------------------------------------

            function resizeTable() {
                if (parentDivWidth != $parent.width()) {
                    parentDivWidth = $parent.width();
                    $table.css({ width: parentDivWidth + 'px' });
                    SetTheadWidthInTable();
                    SetTbodyWidthInTable();
                }
            }

            //$(window).resize(function () {
            //    resizeTable();
            //});

            //$('#menu_toggle').click(function () {
            //    resizeTable();
            //});
        });
    };

}(jQuery));