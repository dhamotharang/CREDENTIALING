
(function ($) {
    $.fn.prtGrid = function (options) {
        // pk's default settings:
        var defaults = {
            url: "",
            dataLength: 50,
            columns: [],
            height: 'auto',
            filterable: true,
            scrollable: true,
            viewFilter: true,
            externalFactors: []
        };

        var settings = $.extend({}, defaults, options);


        //Variable declaration

        var $table = $(this);//Current Table
        var tableId = $table.attr('id'); //Table Id
        var $tbody = $($table.find('tbody')[0]); //boody of table
        var $thead = $table.first().find('thead'); //head of table
        var $th = $thead.first().find('th');//th of first row in head of table
        var columnInfo = settings.columns;//Column info property 
        var columnCount = columnInfo.length;//No of columns
        var primaryUrl = settings.url + '?CountOfList=' + settings.dataLength + '&';//initial url
        var index = 0;//index
        var $parent = $table.parent();//parent element of table
        var parentDivWidth = $parent.width();//Width of parent element
        var theadTemplate = '<tr class="filter-row">';//template used to create row of filter
        var hearderText;//Used for creating sortable column
        var sortIcon;//used to create Sorting icon
        var externalFactors = settings.externalFactors;//External factors object
        var timeoutEx = null;//Timer used for firing change event on external input parameter
        var isDataPresent = true;//True - if data is present
        var currentSortType;//Used in sorting
        var scrollable = settings.scrollable;//True- table is scrollable
        var $tfoot;//foot of the table

        //Append tfoot into table
        if ($table.find('tfoot').length == 0)
            $table.append('<tfoot class="tablefooterloading hidden" id="tableFooter"></tfoot>');
        //find footer of table
        $tfoot = $table.find('tfoot');
        //Setting Height of table
        function SetHeightOfTable() {
            if (typeof (settings.height) === 'string' && settings.height === 'auto') {
                //Auto height based on screen size
                $('.fullBodyContainer').animate({ scrollTop: 0 }, 'fast', function () {
                    $tbody.css({ 'max-height': $(window).height() - $tbody.offset().top - 10 });
                });
            } else {
                //Setting defined height to table
                $tbody.css({ 'max-height': settings.height });
            }
        }
        //Setting height
        SetHeightOfTable();
        //Checking for firefox
        var isFirefox = typeof InstallTrigger !== 'undefined';
        if (isFirefox) {
            //Hiding scroll of table
            $table.parent().css({ 'overflow-x': 'hidden' });
            //Setting margin in -ve which is equal to width of scroll to hide scroll
            $tbody.css({ 'margin-right': $tbody[0].scrollWidth - $tbody[0].offsetWidth + 'px' });
        } else {


        }
        //Checking for IE
        function GetIEVersion() {
            var sAgent = window.navigator.userAgent;
            var Idx = sAgent.indexOf("MSIE");

            // If IE, return version number.
            if (Idx > 0)
                return parseInt(sAgent.substring(Idx + 5, sAgent.indexOf(".", Idx)));

                // If IE 11 then look for Updated user agent string.
            else if (!!navigator.userAgent.match(/Trident\/7\./))
                return 11;
            else
                return 0;
        }

        //Checking if there are external factors
        if (externalFactors.length > 0) {
            //Adding Class to external elements
            (function (externalFactors) {
                for (var i = 0; i < externalFactors.length; i++) {
                    currentFactor = externalFactors[i];
                    //Find external element
                    currExternElement = $("[name='" + currentFactor.name + "'][type='" + currentFactor.type + "']");
                    //Add class to external element
                    currExternElement.addClass("external-filter-element-" + tableId);
                }
            })(externalFactors);

            //Create URL based on external parameters
            function updateURLBasedOnExternalFactors(externalFactors, filterObject) {
                var currentFactor;
                var currExternElement;
                //Create JSON object of external parameters
                for (var i = 0; i < externalFactors.length; i++) {
                    currentFactor = externalFactors[i];
                    //If external input element is check box or radio button
                    if (currentFactor.type === "radio" || currentFactor.type === "checkbox") {
                        currExternElement = $("[name='" + currentFactor.name + "']:checked");
                        //If external input element is text box or hidden field
                    } else if (currentFactor.type === "text" || currentFactor.type === "hidden") {
                        currExternElement = $("[name='" + currentFactor.name + "'][type='" + currentFactor.type + "']");
                    } else {
                        currExternElement = $("[name='" + currentFactor.name + "']");
                    }
                    //Create JSON object of external parameters
                    filterObject[currentFactor.name] = currExternElement.val();
                }
                return filterObject;
            }

            // Change event of external input elements
            $('.external-filter-element-' + tableId).off('change').on('change', function (e) {
                clearTimeout(timeoutEx);
                //Detecting 1000ms delay
                timeoutEx = setTimeout(function () {
                    //Remove class Next-set-load
                    RemoveNextSetLoadClass();
                    //Show Loading symbol
                    StartLoading();
                    $.when(GetDataFromService(0, primaryUrl)).then(
                            function (response) {
                                //Scrolling to top
                                $tbody.scrollTop(0);
                                //Add Html code
                                $tbody.html(response);
                                //Check for data availability and show appropriate message
                                CheckForDataAvailability();
                                //Increment index by count of rows
                                index = index + CalculateCountOfTr(ConvertStringToHtml(response));
                                //Hide loading symbol
                                StopLoading();
                            }, function (error) {
                                //Hide loading symbol
                                StopLoading();
                            });
                }, 1000);
            });

        }
        //Sorting feature
        //Adding sorting symbol for all column of table in header
        function SetSortingSymbol() {
            $th.each(function (i) {
                //Get header text of column
                hearderText = $(this).text();
                //n th column
                var currColumnInfo = columnInfo[i];
                if (typeof currColumnInfo != 'undefined') {
                    //Check if sortable is true
                    if (currColumnInfo.sortable && currColumnInfo.sortable.isSort) {
                        if (currColumnInfo.sortable.defaultSort === null) {
                            //No sorting
                            $(this).html('<span data-container="' + columnInfo[i].name + '" class="sortable-th">' + hearderText + '<i class="fa fa-sort ptr-sort"></i></span>');
                        } else if (currColumnInfo.sortable.defaultSort === 'ASC') {
                            //Soring ascending 
                            $(this).html('<span data-container="' + columnInfo[i].name + '" class="sortable-th">' + hearderText + '<i class="fa fa-sort-asc ptr-sort"></i></span>');
                        } else if (currColumnInfo.sortable.defaultSort === 'DESC') {
                            //Sorting descending
                            $(this).html('<span data-container="' + columnInfo[i].name + '" class="sortable-th">' + hearderText + '<i class="fa fa-sort-desc ptr-sort"></i></span>');
                        }
                    }
                }
            });
        }
        SetSortingSymbol();


        //Add sorting click event on table
        $table.find('.sortable-th').off('click').on('click', function () {
            //Start loading
            StartLoading();
            //Get sort icon
            sortIcon = $(this).find('.ptr-sort');
            //Checking if No sort
            if (sortIcon.hasClass('fa-sort')) {
                currentSortType = null;
            }
                //Checking if ascending sort
            else if (sortIcon.hasClass('fa-sort-asc')) {
                currentSortType = 'asc';
            }
                //Checking if descending sort
            else {
                currentSortType = 'desc';
            }

            //Remove asc and desc class from all columns
            $table.find('.sortable-th').find('.ptr-sort').removeClass('fa-sort-asc').removeClass('fa-sort-desc').addClass('fa-sort');
            //Change the icon based on current sort feature
            //Check if no sort
            if (currentSortType == null) {
                //Make it ascending sort
                sortIcon.removeClass('fa-sort').removeClass('fa-sort-desc').addClass('fa-sort-asc');
                //Check if ascending sort
            } else if (currentSortType == 'asc') {
                //Make it descending sort
                sortIcon.removeClass('fa-sort-asc').removeClass('fa-sort').addClass('fa-sort-desc');
                //Check if descending sort
            } else {
                //Make it no sort
                sortIcon.removeClass('fa-sort-desc').removeClass('fa-sort-asc').addClass('fa-sort');
            }
            //Remove class Next-set-load
            RemoveNextSetLoadClass();
            //Set data present bit to true
            isDataPresent = true;
            //Get sorted data from service
            $.when(GetDataFromService(0, primaryUrl)).then(
                function (response) {
                    //Scrolling to top
                    $tbody.scrollTop(0);
                    //Append HTML
                    $tbody.html(response);
                    //Check if any data is present
                    CheckForDataAvailability();
                    //Change index to count of data obtained 
                    index = CalculateCountOfTr(ConvertStringToHtml(response));
                    //Stop Loading
                    StopLoading();
                }, function (error) {
                    //Stop Loading
                    StopLoading();
                });
        });

        //Get final URL for calling service
        function GetServiceURL(index, primaryUrl) {
            //Set index property
            index = index;
            //Append index to URL
            primaryUrl = primaryUrl + 'index=' + index + '&';
            //Add sorting parameters
            primaryUrl = AddSortParameters(primaryUrl);
            //Post data object
            var postData = {};
            //Add filter parameters
            postData = CreateURLForFilter(columnInfo, postData);
            try {
                //Add external parameters if present
                postData = updateURLBasedOnExternalFactors(externalFactors, postData);
            } catch (e) {

            }
            //Return final URL
            return { url: primaryUrl, data: postData };

        }

        //Method to add parameters of sorting
        function AddSortParameters(url) {
            var sortParameters = $thead.find('.sortable-th');
            for (var i = 0; i < sortParameters.length; i++) {
                if ($(sortParameters[i]).find('.ptr-sort').hasClass('fa-sort-asc')) {
                    url = url + '&sortBy=' + $(sortParameters[i]).attr('data-container') + '&sortingType=asc';
                    break;
                } else if ($(sortParameters[i]).find('.ptr-sort').hasClass('fa-sort-desc')) {
                    url = url + '&sortBy=' + $(sortParameters[i]).attr('data-container') + '&sortingType=desc';
                    break;
                }
            }
            return url;
        }

        //Method for added filter pameters in url
        function CreateURLForFilter(columnInfo, filterObject) {
            for (var i = 0; i < columnInfo.length; i++) {
                if (columnInfo[i].type != 'none')
                    filterObject[columnInfo[i].name] = $table.find('input.filter-element[name="' + columnInfo[i].name + '"]').val();
            }
            return filterObject;
        }

        //Async method Ajax call to get data from service
        function GetDataFromService(index, url) {
            //Get URL and post data to call service
            var serviceObject = GetServiceURL(index, url);
            var dfd = $.Deferred();
            $.ajax({
                type: 'POST',
                url: serviceObject.url,
                data: JSON.stringify(serviceObject.data),
                processData: false,
                contentType: "application/json",
                success: function (result) {
                    //On success
                    dfd.resolve(result);
                },
                error: function (error) {
                    //On error
                    dfd.reject(error);
                }
            });
            return dfd.promise();
        }



        function GetFilterElementTemplate(columnInfo, index) {
            //Create template of select element
            function CreateSelectFilterElement(columnInfo, index) {
                var selectTemplate = '<th><select id="filter_textfield_' + index + '" class="filter-element form-control" value="" >';
                for (var i = 0; i < columnInfo.values.length; i++) {
                    var currentSelectValue = columnInfo.values[i];
                    selectTemplate = selectTemplate + '<option value="' + currentSelectValue.value + '">' + currentSelectValue.text + '</option>';
                }
                selectTemplate = selectTemplate + "</select></th>";
                return selectTemplate;
            }

            //Filter Configuration object
            var filterElementsConfig = {
                "none": '<th></th>',
                "text": '<th><input type="text" id="filter_textfield_' + index + '" placeholder="' + columnInfo.text + '" name="' + columnInfo.name + '" class="filter-element form-control" value="' + ((!columnInfo.value) ? "" : columnInfo.value) + '" /></th>',
                "number": '<th><input type="number" id="filter_textfield_' + index + '" placeholder="' + columnInfo.text + '" name="' + columnInfo.name + '" class="filter-element form-control" value="' + ((!columnInfo.value) ? "" : columnInfo.value) + '" /></th>',
                "date": '<th><input type="text" id="filter_textfield_' + index + '" placeholder="' + columnInfo.text + '" name="' + columnInfo.name + '" class="filter-element date-element form-control" value="' + ((!columnInfo.value) ? "" : columnInfo.value) + '" /></th>',
                "select": columnInfo.type === "select" ? CreateSelectFilterElement(columnInfo, index) : "",
                "integer": '<th><input type="text" id="filter_textfield_' + index + '" placeholder="' + columnInfo.text + '" name="' + columnInfo.name + '" class="only-integers filter-element form-control" value="' + ((!columnInfo.value) ? "" : columnInfo.value) + '" /></th>'
            };
            return filterElementsConfig[columnInfo.type];
        }
        //Check if filterable it true and if filters are already present
        if (settings.filterable) {
            if ($thead.find('.filter-row').length == 0) {
                for (var i = 0; i < columnInfo.length; i++) {
                    //Create template of filter elements 
                    theadTemplate = theadTemplate + GetFilterElementTemplate(columnInfo[i], i);
                }
                theadTemplate = theadTemplate + '</tr>';
                $thead.first().append(theadTemplate);
            }

            //Hide and show filter based on viewFilter value
            if (!settings.viewFilter) {
                $thead.find('tr').eq(1).hide();
            }

            //Copy class name for filter
            $thead.find('.filter-row').find('th').each(function (index) {
                var self = $(this);
                //Copy class name from above cell
                self.addClass(self.parent().prev().find('th:eq(' + index + ')').attr('Class'));
            });

            //Date picker setting date format-MM/DD/yyyy
            $thead.find('.date-element').each(function () {
                $(this).datetimepicker({ format: 'MM/DD/YYYY', useCurrent: false, widgetPositioning: { horizontal: 'auto', vertical: 'bottom' } });
                $(this).mask('99/99/9999');
            });

            //Regular expression for checking for integer
            var regexpOnlyInt = new RegExp("^[0-9]*$");
            //Timer object
            var timeout = null;
            //Variable to store previous value
            var prevValue = '';
            // Listen for keystroke events
            $table.find('[type="text"].filter-element:not(.date-element),[type="number"].filter-element:not(.date-element)').off(GetIEVersion() > 0 ? 'keyup' : 'input').on(GetIEVersion() > 0 ? 'keyup' : 'input', function (e) {

                //current input element
                var $self = $(this);
                //current value of input element
                var currValue = $self.val();
                //Checking if input element has class - only-integers and it matches regular expression -only integers
                if (($self.hasClass("only-integers") && !regexpOnlyInt.test(currValue))) {
                    $self.val(prevValue);
                } else {
                    clearTimeout(timeout);
                    //Detect 1000ms delay
                    timeout = setTimeout(function () {
                        //Show Loading symbol
                        StartLoading();
                        //Set data present bit to true
                        isDataPresent = true;
                        $.when(GetDataFromService(0, primaryUrl)).then(
                            function (response) {
                                //Scrolling to top
                                $tbody.scrollTop(0);
                                //Append HTML Code
                                $tbody.html(response);
                                //Check if data is available or else show message
                                CheckForDataAvailability();
                                //Change index to count of new rows
                                index = CalculateCountOfTr(ConvertStringToHtml(response));
                                //Stop Loading
                                StopLoading();
                                //Assigning current value to previous value
                                prevValue = currValue;
                            }, function (error) {
                                //Stop Loading
                                StopLoading();
                            });
                    }, 1000);
                }



            });
            $table.find('[type="text"].date-element').off('dp.change').on('dp.change', function (e) {
                clearTimeout(timeout);
                //Detect 1000ms delay
                timeout = setTimeout(function () {
                    //Show Loading symbol
                    StartLoading();
                    //Set data present bit to true
                    isDataPresent = true;
                    $.when(GetDataFromService(0, primaryUrl)).then(
                         function (response) {
                             //Scrolling to top
                             $tbody.scrollTop(0);
                             //Append HTML Code
                             $tbody.html(response);
                             //Check if data is available or else show message
                             CheckForDataAvailability();
                             //Change index to count of new rows
                             index = CalculateCountOfTr(ConvertStringToHtml(response));
                             //Stop Loading
                             StopLoading();
                         }, function (error) {
                             //Stop Loading
                             StopLoading();
                         });
                }, 1000);
            });
        };


        function Utils() { }

        Utils.prototype = {
            constructor: Utils,
            isElementInView: function (element, fullyInView) {
                try {
                    //Height of window scrolled top
                    var pageTop = $(window).scrollTop();
                    //Total height of page
                    var pageBottom = pageTop + $(window).height();
                    //Measuring element top position
                    var elementTop = $table.find(element).offset().top;
                    //Measuring element bottom position
                    var elementBottom = elementTop + $table.find(element).height();

                    if (fullyInView === true) {
                        return ((pageTop < elementTop) && (pageBottom > elementBottom));
                    } else {
                        return ((elementTop <= pageBottom) && (elementBottom >= pageTop));
                    }
                } catch (e) {

                }

            }
        };
        var Utils = new Utils();

        //Scroll event
        if (scrollable) {
            $tbody.off('scroll').on('scroll', function () {
                var isElementInView = Utils.isElementInView($('.load-next-set'), false);
                if (isElementInView && isDataPresent) {
                    //Remove Next-set-load class
                    RemoveNextSetLoadClass();
                    //Show Loading symbol
                    StartLoading();
                    var countTrs = $(this).closest('tbody').find('tr').length; // Added by Adarsh on 5th Feb 2017
                    index = countTrs;                                           //Getting index from the table itself instead of storing it anywhere
                    $.when(GetDataFromService(index, primaryUrl)).then(
                         function (response) {
                             //Append HTML in tbody
                             $tbody.append(response);
                             //Calculate count of rows
                             // var countTrs = $(this).closest('tbody').find('tr').length;
                             //// var countTrs = CalculateCountOfTr(ConvertStringToHtml(response));
                             // //Increment index by count of rows
                             //// index = index + countTrs;
                             // index = countTrs;
                             // //Check if data available
                             CheckForDataAvailability();
                             //Check if count is 0
                             if (countTrs == 0) {
                                 //Set isDataPresent to false indicating show not load net set of data
                                 isDataPresent = false;
                             }
                             //Hide loading symbol
                             StopLoading();
                         }, function (error) {
                             //Hide loading symbol
                             StopLoading();
                         });
                }
            })
        }

        //function to remove class-load-next-set
        function RemoveNextSetLoadClass() {
            $tbody.find('.load-next-set').removeClass('load-next-set');
        }

        //function to convert string to html DOM
        function ConvertStringToHtml(objectString) {
            return $.parseHTML($.trim(objectString));
        }

        //Calculate count of trs
        function CalculateCountOfTr(htmlObject) {
            //Calculate count of trs eliminating other objects
            return jQuery.grep(htmlObject, function (obj, index) {
                return $(obj).is('tr');
            }).length;
        }

        //Function to show loading 
        function StartLoading() {
            $tfoot.removeClass('hidden').html('<tr><td id="lodingtabledata" colspan="0"></td></tr>');
            showFooterLoading($tfoot.attr("id"));
        }

        //Function to hide loading 
        function StopLoading() {
            $tfoot.addClass('hidden').empty();
            removeFooterLoader();
        }
        var showFooterLoading = function (id) {
            var $ob = $('#footerloadingSample');
            var $clon = $ob.clone().addClass("footerloadingArea").removeClass("hidden");
            $('#' + id).prepend($clon);
        };

        var removeFooterLoader = function () {
            $('.footerloadingArea').fadeOut(function () {
                $(this).remove();
            });
        };


        //Check if data is present or not
        function CheckForDataAvailability() {
            //Calculate count of rows
            var countOfData = $tbody.find('tr').length;
            //Check if no rows found
            if (countOfData === 0) {
                //Add appropriate message indicating data inavailability
                $tbody.html('<tr class="no-data-message-tr"><td class="no-data-message" colspan="' + columnCount + '">No Data Available</td></tr>');
            }
        }
        CheckForDataAvailability();

        //Set Initial index
        (function () {
            //Calculate count of rows
            index = $tbody.find('tr').length;
        })();

        //Adding tooltip for overflowed data
        (function () {
            //When mouse enters inside
            $tbody.off('mouseenter').on('mouseenter', 'td', function () {
                var $this = $(this);
                //Check if text is overflowed or not
                if (this.offsetWidth < this.scrollWidth && $this.text() == $this.html()) {
                    if (!$this.attr('data-content')) {
                        //Add text
                        $this.attr('data-content', $this.text().trim());
                        //Alignment of popover - top
                        $this.attr('data-placement', 'top');
                    }
                    //Show popover
                    $this.popover('show');
                }
            });
            $tbody.off('mouseleave').on('mouseleave', 'td', function () {
                var $this = $(this);
                //Check if text is overflowed or not
                if (this.offsetWidth < this.scrollWidth && $this.text() == $this.html()) {
                    //Hide popover
                    $this.popover('hide');
                }
            });
        })();
        //Scroll down if u click down arrow
        (function () {
            $(document).keydown(function (e) {
                if (e.keyCode == 36) { // home key maps to keycode `36` to scroll to Beginning of grid
                    $tbody.animate({ scrollTop: 0 }, 1000);
                }
                if (e.keyCode == 35) { // end key maps to keycode `35` to scroll to End of grid                    
                    $tbody.animate({ scrollTop: $tbody.prop("scrollHeight") }, 1000);
                }
                if (e.keyCode == 38) { // Up Arrow key maps to keycode `38` to scroll UP in grid
                    $tbody.scrollTop($tbody.scrollTop() - 50);
                }
                if (e.keyCode == 40) { // Down Arrow key maps to keycode `40` to scroll DOWN in grid
                    $tbody.scrollTop($tbody.scrollTop() + 50);
                }
            });
        })();

        //binding reset method which resets table 
        settings._resetTable = function () {
            //Set index to 0
            index = 0;
            //Clear filter data
            ClearFilterData();
            //Set sorting symbol
            SetSortingSymbol();
            //Call Ajax to get data from service
            //Show Loading symbol
            StartLoading();
            //Set data present bit to true
            isDataPresent = true;
            $.when(GetDataFromService(0, primaryUrl)).then(
                function (response) {
                    //Scrolling to top
                    $tbody.scrollTop(0);
                    //Append HTML Code
                    $tbody.html(response);
                    //Check if data is available or else show message
                    CheckForDataAvailability();
                    //Change index to count of new rows
                    index = CalculateCountOfTr(ConvertStringToHtml(response));
                    //Stop Loading
                    StopLoading();
                }, function (error) {
                    //Stop Loading
                    StopLoading();
                });
        }
        //function to clear all filters
        function ClearFilterData() {
            $thead.find('.filter-element').val('');
        }
        //function to reset sorting
        function ResetSorting() {
            //Get all sorting symbol and remove classes used for ascending and descending
            $thead.find('.ptr-sort').removeClass('fa-sort-asc').removeClass('fa-sort-desc').addClass('fa-sort');
        }

        //Window resize event
        $(window).resize(function () {
            SetHeightOfTable();
        });

        //function to resize table 
        settings._resizeTable = SetHeightOfTable;

        //function to empty table
        settings._emptyTable = function () {
            //Delete row from table
            $tbody.html('');
            //Add No data available message
            $tbody.html('<tr class="no-data-message-tr"><td class="no-data-message" colspan="' + columnCount + '">No Data Available</td></tr>');
            //Cleare filter data
            ClearFilterData();
            //Remove Sorting 
            ResetSorting();
        }


        return settings;
    };


}(jQuery));