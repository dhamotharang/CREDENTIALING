// Hello.
//
// This is JSHint, a tool that helps to detect errors and potential
// problems in your JavaScript code.
/*
 * ptGrid jQuery Plugin v0.1
 *
 * Copyright (c) 2016 Pratian Technologies(KRGLV)
 * Licensed under the ****.
 *
 * Require jQuery Library >= v1.9.0 http://jquery.com
 * + aciPlugin >= v1.5.1 https://github.com/dragosu/jquery-aciPlugin
 */

/*
 * Description ---------
 */

(function ($) {


    // add a few more default options
    var options = {
        source: {
            localData: null, //then data type : array
            type: 'post',
            dataType: "json",//array, jsonp, csv,
            url: '',
            dataFields: [],
            //dataFields: [
            //        { name: 'countryName', type: 'string' },
            //        { name: 'name', type: 'string' },
            //        { name: 'population', type: 'float' },
            //        { name: 'continentCode', type: 'string' }
            //],
            initRowDetails: null,
            pageSize: 10,
            serverPaging: true,
            serverFiltering: true,
            serverSorting: true,
        },
        filter: {
            search: [],
            sort: { key: '', order: '' },
            pageNumber: 1,
        },
        width: 850,
        pageable: true,
        pagerButtonsCount: 10,

        columnsResize: true,
        columns: [],
        //columns: [
        //  { text: 'Name', dataField: 'firstname', width: 200 },
        //  { text: 'Last Name', dataField: 'lastname', width: 200 },
        //  { text: 'Product', editable: false, dataField: 'productname', width: 180 },
        //  { text: 'Quantity', dataField: 'quantity', width: 80, cellsAlign: 'right', align: 'right' },
        //  { text: 'Unit Price', dataField: 'price', width: 90, cellsAlign: 'right', align: 'right', cellsFormat: 'c2' },
        //  { text: 'Total', dataField: 'total', cellsAlign: 'right', align: 'right', cellsFormat: 'c2' }
        //],
        //columns: [
        //          { text: 'Supplier Name', cellsAlign: 'center', align: 'center', dataField: 'SupplierName', width: 200 },
        //          { text: 'Name', columngroup: 'ProductDetails', cellsAlign: 'center', align: 'center', dataField: 'ProductName', width: 200 },
        //          { text: 'Quantity', columngroup: 'ProductDetails', dataField: 'Quantity', cellsFormat: 'd', cellsAlign: 'center', align: 'center', width: 80 },
        //          { text: 'Freight', columngroup: 'OrderDetails', dataField: 'Freight', cellsFormat: 'd', cellsAlign: 'center', align: 'center', width: 100 },
        //          { text: 'Order Date', columngroup: 'OrderDetails', cellsAlign: 'center', align: 'center', cellsFormat: 'd', dataField: 'OrderDate', width: 100 },
        //          { text: 'Order Address', columngroup: 'OrderDetails', cellsAlign: 'center', align: 'center', dataField: 'OrderAddress', width: 100 },
        //          { text: 'Price', columngroup: 'ProductDetails', dataField: 'Price', cellsFormat: 'c2', align: 'center', cellsAlign: 'center', width: 70 },
        //          { text: 'Address', columngroup: 'Location', cellsAlign: 'center', align: 'center', dataField: 'Address', width: 120 },
        //          { text: 'City', columngroup: 'Location', cellsAlign: 'center', align: 'center', dataField: 'City', width: 80 }
        //],
        //columnGroups: 
        //[
        //  { text: 'Product Details', align: 'center', name: 'ProductDetails' },
        //  { text: 'Order Details', parentGroup: 'ProductDetails', align: 'center', name: 'OrderDetails' },
        //  { text: 'Location', align: 'center', name: 'Location' }
        //],
        lastLeftScroll: 0,
        leftLlastPos: 0,

        title: '',
        // export
        exportData: [],
        // the AJAX options (see jQuery.ajax) where the `success` and `error` are overridden by ptGrid
        //ajax: {
        //    url: null, // URL from where to take the data, something like `path/script?nodeId=` (the node ID value will be added for each request)
        //    dataType: 'json'
        //},
        dataSource: null, // a list of data sources to be used (each entry in `ptGrid.options.ajax` format)
        rootData: null, // initial ROOT data for the ptGrid (if NULL then one initial AJAX request is made on init)
    };

    var ptGrid_core = {
        //------------------ Temp for Helper and handller ---------
        scrollDiff: 293,

        init: function () {
            this._super();

            this._createContainer1();

            this.util();

            // call on success
            var success = this.proxy(function () {
                // call the parent
                this._super();
                this._private.locked = false;
                this._trigger(null, 'init', options);
                this._success(null, options);
            });
            // call on fail
            var fail = this.proxy(function () {
                // call the parent
                this._super();
                this._private.locked = false;
                this._trigger(null, 'initfail', options);
                this._fail(null, options);
            });

            if (this._instance.options.rootData) {
                // the rootData was set, use it to init the ptgrid
                this.loadData(null, this._inner(options, {
                    success: success,
                    fail: fail,
                    itemData: this._instance.options.rootData
                }));
            } else if (this._instance.options.source.url) {

                if (this._instance.options.source.localData) {

                } else {
                    // the AJAX url was set, init with AJAX
                    this.ajaxLoad(null, this._inner(options, {
                        success: success,
                        fail: fail
                    }));
                }
            } else {
                success.apply(this);
            }

            // destroy it cause there is no need for it anymore (the job was done)
            //this.destroy();
        },
        // trigger the ptGrid events on the ptgrid container
        _trigger: function (item, eventName, options) {
            var event = $.Event('ptgrid');
            if (!options) {
                options = this._options();
            }
            this._instance.jQuery.trigger(event, [this, item, eventName, options]);
            return !event.isDefaultPrevented();
        },
        // call on success
        _success: function (item, options) {
            if (options && options.success) {
                options.success.call(this, item, options);
            }
        },
        // call on fail
        _fail: function (item, options) {
            if (options && options.fail) {
                options.fail.call(this, item, options);
            }
        },
        // call on notify (should be same as `success` but called when already in the requested state)
        _notify: function (item, options) {
            if (options && options.notify) {
                options.notify.call(this, item, options);
            }
        },
        _dataSource: function (item) {
            var sorts = [];
            if (this._instance.options.filter.sort.order) {
                sorts = [
                    {
                        Direction: this._instance.options.filter.sort.order,
                        Key: this._instance.options.filter.sort.key
                    }
                ];
            }
            var data = JSON.stringify({
                option: { pageNumber: this._instance.options.filter.pageNumber, pageSize: this._instance.options.source.pageSize },
                filters: this._instance.options.filter.search,
                sorts: sorts
            });
            var ajaxoption = {
                url: this._instance.options.source.url,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                cache: false,
                dataType: this._instance.options.source.dataType,
                type: this._instance.options.source.type,
                data: data//this._instance.options.source.type
            };
            return ajaxoption;
        },
        ajaxLoad: function (item, options, reload) {
            //--------- sort filter clear action -------------
            if (reload) {
                this._instance.jQuery.find('#ptGrid_records').find('tr').remove('.ptGridRow');
                var loader = '<div class="pt-loading" style="display: block;"></div><div class="pt-loading-msg" style="display: block;"><div class="pt-spinner"></div>Loading...</div>';
                this._instance.jQuery.find('#main').append(loader);
            }
            // ensure we work on a copy of the dataSource object
            var settings = $.extend({
            }, this._dataSource(item));
            // call the `ptGrid.options.ajaxHook`
            //this._instance.options.ajaxHook.call(this, item, settings);
            // loaded data need to be array of item objects
            settings.success = this.proxy(function (itemList) {
                //RTJ
                //console.log(itemList);
                //itemList = JSON.parse(itemList);
                //domApi.removeClass(this._instance.jQuery[0], 'ptGridLoad');

                if (itemList && (itemList instanceof Array) && itemList.length) {
                    // the AJAX returned some items
                    var process = function () {
                        this._createTable(item, this._inner(options, {
                            success: function () {
                                this._success(item, options);
                                complete();
                            },
                            fail: function () {
                                this._fail(item, options);
                                complete();
                            },
                            itemData: itemList
                        }));
                    };
                    process.apply(this);
                } else {
                    // the AJAX response was not just right (or not a inode)
                    //var process = function () {
                    //    this._fail(item, options);
                    //    complete();
                    //};
                    //if (!item || this.isLeaf(item)) {
                    //    process.apply(this);
                    //}
                }
                this.refresh();
            });
            settings.error = this.proxy(function () {
                // AJAX failed
                this._fail(item, options);
                complete();
            });
            $.ajax(settings);
        },
        // update filter search field
        _updateFilter: function (search) {

            var flag = false;
            this._instance.options.filter.search.forEach(function (s) {
                if (s.PropertyName === search.PropertyName) {
                    s.Value = search.Value;
                    flag = true;
                }
            });
            if (!flag) {
                this._instance.options.filter.search.push(search);
            }
            var temp = [];
            this._instance.options.filter.search.forEach(function (s) {
                if (s.Value != '') {
                    temp.push(s);
                }
            });

            this._instance.options.filter.search = temp;
            this._instance.options.filter.pageNumber = 1;
            this.ajaxLoad(this._instance.jQuery, this._instance.options, true);
        },

        // helper function to extend the `options` object
        // `object` the initial options object
        // _success, _fail, _notify are callbacks or string (the event name to be triggered)
        // `item` is the item to trigger events for
        _options: function (object, _success, _fail, _notify, item) {
            // options object (need to be in this form for all API functions
            // that have the `options` parameter, not all properties are required)
            var options = $.extend({
                uid: 'ui',
                success: null, // success callback
                fail: null, // fail callback
                notify: null, // notify callback (internal use for when already in the requested state)
                expand: this._instance.options.expand, // propagate (on open)
                collapse: this._instance.options.collapse, // propagate (on close)
                unique: this._instance.options.unique, // keep a single branch open (on open)
                unanimated: false, // unanimated (open/close/toggle)
                itemData: {
                } // items data (object) or a list (array) of them (used when creating branches)
            },
            object);
            var success = _success ? ((typeof _success == 'string') ? function () {
                this._trigger(item, _success, options);
            } : _success) : null;
            var fail = _fail ? ((typeof _fail == 'string') ? function () {
                this._trigger(item, _fail, options);
            } : _fail) : null;
            var notify = _notify ? ((typeof _notify == 'string') ? function () {
                this._trigger(item, _notify, options);
            } : _notify) : null;
            if (success) {
                // success callback
                if (object && object.success) {
                    options.success = function () {
                        success.apply(this, arguments);
                        object.success.apply(this, arguments);
                    };
                } else {
                    options.success = success;
                }
            }
            if (fail) {
                // fail callback
                if (object && object.fail) {
                    options.fail = function () {
                        fail.apply(this, arguments);
                        object.fail.apply(this, arguments);
                    };
                } else {
                    options.fail = fail;
                }
            }
            if (notify) {
                // notify callback
                if (object && object.notify) {
                    options.notify = function () {
                        notify.apply(this, arguments);
                        object.notify.apply(this, arguments);
                    };
                } else if (!options.notify && object && object.success) {
                    options.notify = function () {
                        notify.apply(this, arguments);
                        object.success.apply(this, arguments);
                    };
                } else {
                    options.notify = notify;
                }
            } else if (!options.notify && object && object.success) {
                // by default, run success callback
                options.notify = object.success;
            }
            return options;
        },
        // create children container
        _createContainer: function (item) {
            return $(this._instance.jQuery.find('#ptGrid_records'));
        },
        // Create main Container
        _createContainer1: function () {
            var template = this.getTemplate();
            this._instance.jQuery.append(template);
            var loader = '<div class="pt-loading" style="display: block;"></div><div class="pt-loading-msg" style="display: block;"><div class="pt-spinner"></div>Loading...</div>';
            this._instance.jQuery.find('#main').append(loader);
        },
        getTemplate: function (data) {

            var loader = '<div class="ptGrid-ajax-load"></div>';
            var colhead = ''; // Header Columns
            var colgroup = ''; // Record Col Group Style

            var searchFeild = '';

            var columns = this._instance.options.columns;
            if (columns.length > 0) {
                columns.forEach(function (col) {
                    if (col.width && (typeof col.width === 'string' || col.width instanceof String) && col.width.includes("%")) {
                        colhead += '<td col="0" class="pt-head " style="width: ' + col.width + '">' +
                            '<div class="pt-resizer" name="0" style="height: 25px; margin-left: ' + (col.width - 4) + 'px;"></div>';
                        colgroup += '<td class="pt-grid-data" col="0" style="height: 0px; width: ' + col.width + '"></td>';
                    } else if (col.width) {
                        colhead += '<td col="0" class="pt-head " style="width: ' + col.width + 'px;">' +
                            '<div class="pt-resizer" name="0" style="height: 25px; margin-left: ' + (col.width - 4) + 'px;"></div>';
                        colgroup += '<td class="pt-grid-data" col="0" style="height: 0px; width: ' + col.width + 'px;"></td>';
                    } else {
                        colhead += '<td col="0" class="pt-head ">' +
                            '<div class="pt-resizer" name="0" style="height: 25px; margin-left: 0px;"></div>';
                        colgroup += '<td class="pt-grid-data" col="0" style="height: 0px;"></td>';
                    }
                    colhead += '<div class="pt-col-header" data-key="' + col.dataField + '">' +
                                    '<div class="" role="sort" style="position: absolute;right: 1px;"></div>' +
                                    col.text +
                                '</div>' +
                                '<span class="pt-grid-search" style="width: 100%;padding: 3px 0px 3px 3px;display: none;border-top: 1px solid #ccc;"><input type="text" name="' + col.dataField + '" class="pt-grid-search-text" /></span>' +
                            '</td>';
                });
            }
            else {
                //$.each(data, function (key, value) {
                //    colhead += '<div role="ptGridheader" class="ptGrid-column-header">' +
                //            '<div style="height: 100%; width: 100%;">' +
                //                '<div><span style="text-overflow: ellipsis; cursor: default;">' + key + '</span></div>' +
                //                '<div class="ptGridIcons">' +
                //                '</div>' +
                //           '</div>' +
                //        '</div>';
                //});                
            }
            var header = '<div class="ptGrid-header">' +
                    '<div id="ptGridColumns">' +
                        colhead +
                     '</div>' +
                     '</div>';

            var content = '<div class="ptGridData">' +
                    '<div id="ptGridContent">' +
                     '</div>' +
                     '</div>';

            var toolbar = '<span id="pt-search-button" class="pt-grid-toolbar-tool" data-parent="' + this._instance.index + '" title="Advance Search">Search...</span>';

            var temmm = '<div id="main" style="width: 100%; height: 400px;" name="grid" class="pt-default pt-grid">' +
    '<div style="width: ' + (this._instance.options.width - 2) + 'px; height: 398px;">' +
        '<div id="ptGrid_header" data-parent="' + this._instance.index + '" class="pt-grid-header" style="display: none;"></div>' +
        '<div id="ptGrid_toolbar" data-parent="' + this._instance.index + '" class="pt-grid-toolbar pt-default pt-toolbar" name="grid_toolbar" style="top: 0px; left: 0px; right: 0px;">' +
        toolbar +
        '</div>' +
        '<div id="ptGrid_body" class="pt-grid-body" style="top: 38px; bottom: 24px; left: 0px; right: 0px; height: 336px;">' +
            '<div id="ptGrid_records" class="pt-grid-records" data-parent="' + this._instance.index + '" style="top: 26px; overflow: auto;">' +
                '<table>' +
                    '<tbody>' +
                        '<tr line="0">' +
                        colgroup +
                            //'<td class="pt-grid-data" col="0" style="height: 0px; width: 50px;"></td>'+
                            //'<td class="pt-grid-data" col="1" style="height: 0px; width: 140px;"></td>'+
                            //'<td class="pt-grid-data-last" style="height: 0px; width: 0px;"></td>'+
                        '</tr>' +
                        '<tr id="ptGrid_rec_top" line="top" style="height: 0px">' +
                            '<td colspan="200"></td>' +
                        '</tr>' +
                        //'<tr id="ptGrid_rec_1" recid="1" line="1" class="pt-odd" onclick="pt[&apos;grid&apos;].click(&apos;1&apos;, event);" oncontextmenu="pt[&apos;grid&apos;].contextMenu(&apos;1&apos;, event);" style="height: 24px; ">' +
                        //    '<td class="pt-grid-data" col="0" style="">'+
                        //        '<div title="1" style="">1</div>'+
                        //    '</td>'+
                        //    '<td class="pt-grid-data" col="1" style="">'+
                        //        '<div title="Amy" style="">Amy</div>'+
                        //    '</td>'+
                        //    '<td class="pt-grid-data-last"></td>'+
                        //'</tr>'+
                        '<tr id="ptGrid_rec_bottom" line="bottom" style="height: 0px">' +
                            '<td colspan="200"></td>' +
                        '</tr>' +
                        '<tr id="ptGrid_rec_more" style="display: none">' +
                            '<td colspan="200" class="pt-load-more"></td>' +
                        '</tr>' +
                    '</tbody>' +
                '</table>' +
            '</div>' +
            '<div id="ptGrid_columns" data-parent="' + this._instance.index + '" class="pt-grid-columns">' +
                '<table>' +
                    '<tbody>' +
                        '<tr>' +
                            colhead +
                            '<td class="pt-head pt-head-last" style="width: 17px;">' +
                                '<div>&nbsp;</div>' +
                            '</td>' +
                        '</tr>' +
                    '</tbody>' +
                '</table>' +
            '</div>' +
        '</div>' +
        '<div id="ptGrid_summary" data-parent="' + this._instance.index + '" class="pt-grid-body pt-grid-summary" style="display: none;"></div>' +
        '<div id="ptGrid_footer" data-parent="' + this._instance.index + '" class="pt-grid-footer" style="bottom: 0px; left: 0px; right: 0px;">' +
            '<div>' +
                '<div class="pt-footer-left">Server Response </div>' +
                '<div class="pt-footer-right">Total 3,700 (buffered <span id="bufferCount"></span>)</div>' +
                '<div class="pt-footer-center"></div>' +
            '</div>' +
        '</div>' +
    '</div>' +
'</div>';

            var body = temmm;//loader + '<div><div id="ptGridWrapper"><div>' + header + content + '</div></div></div>';
            return body;
        },
        // helper for passing `options` object to inner methods
        // the callbacks are removed and `override` can be used to update properties
        _inner: function (options, override) {
            // removing success/fail/notify from options
            return $.extend({
            }, options, {
                success: null,
                fail: null,
                notify: null
            },
            override);
        },
        // Cretae Table Content with Data 
        _createTable: function (item, options) {
            var process = this.proxy(function (node, itemList) {
                this.append(node, this._inner(options, {
                    fail: options.fail,
                    itemData: itemList
                }));
            });
            process(item, options.itemData);
        },
        // create item by `itemData`
        // `level` is the #0 based item level
        _createItem: function (itemData, level) {

            //var tt = '<div role="row" id="">' +
            //        '<div role="ptGridCell" class="">' +
            //        '<div class="pt-grid-cell-left-align">' + 'sddddddd' + '</div>' +
            //        '</div></div>';

            var trcounts = this._instance.jQuery.find('#ptGrid_records').find('tr.ptGridRow').length;

            var row = window.document.createElement('tr');
            row.setAttribute('role', 'row');
            //row.className = 'pt-row';
            row.style.width = '100%';

            //var widthcounter = 0;
            if (this._instance.options.columns.length > 0) {
                for (var i in this._instance.options.columns) {
                    //console.log('dataField');
                    var line = window.document.createElement('td');
                    row.appendChild(line);
                    line.setAttribute('role', 'ptGridCell');
                    line.setAttribute('title', itemData[this._instance.options.columns[i].dataField]);
                    line.className = 'pt-grid-cell';
                    if (((trcounts + 1) % 2) == 0) {
                        line.className += ' pt-grid-cell-alt';
                    }
                    //line.style.width = this._instance.options.columns[i].width + 'px';
                    //line.style.float = 'left';
                    //widthcounter += this._instance.options.columns[i].width;
                    //line.style.position = 'absolute !important';
                    var text = window.document.createElement('DIV');
                    line.appendChild(text);
                    text.className = 'pt-grid-cell-left-align';
                    text.innerHTML = itemData[this._instance.options.columns[i].dataField];
                }
            } else {
                $.each(itemData, function (key, value) {
                    if (!$.isArray(value)) {
                        //console.log(i);
                        //console.log('dataField');
                        var line = window.document.createElement('td');
                        row.appendChild(line);
                        line.setAttribute('role', 'ptGridCell');
                        line.setAttribute('title', value);
                        line.className = 'pt-grid-cell';
                        if (((trcounts + 1) % 2) == 0) {
                            line.className += ' pt-grid-cell-alt';
                        }
                        //line.style.width = 100 + 'px';
                        //line.style.float = 'left';
                        //widthcounter += this._instance.options.columns[i].width;
                        //line.style.position = 'absolute !important';
                        var text = window.document.createElement('DIV');
                        line.appendChild(text);
                        text.className = 'pt-grid-cell-left-align';
                        text.innerHTML = value;
                    }
                });
            }
            row.className = 'ptGridRow' + (itemData.inode || (itemData.inode === null) ? (itemData.inode || (itemData.branch && itemData.branch.length) ? ' ptGridInode' : ' ptGridInodeMaybe') : ' ptGridLeaf') + ' ptGridLevel' + level + (itemData.disabled ? ' ptGridDisabled' : '');

            var $li = $(row);
            $li.data('itemData' + this._instance.nameSpace, $.extend({
            }, itemData, {
                branch: itemData.branch && itemData.branch.length
            }));
            return $li;

        },
        // create & add one or more items
        // `ul`, `before` and `after` are set depending on the caller
        // `itemData` need to be array of objects or just an object (one item)
        // `level` is the #0 based level
        // `callback` function (items) is called at the end of the operation
        _createItems: function (ul, before, after, itemData, level, callback) {
            var items = [], fragment = window.document.createDocumentFragment();
            items = $(items);
            if (items.length) {
                // add the new items
                if (ul) {
                    $(ul).find('tbody')[0].appendChild(fragment);
                } else if (before) {
                    before[0].parentNode.insertBefore(fragment, before[0]);
                } else if (after) {
                    after[0].parentNode.insertBefore(fragment, after[0].nextSibling);
                }
            }
            callback.call(this, items);
            if (itemData) {
                //this._loader(true);
                var parent;
                if (itemData instanceof Array) {
                    // this is a list of items
                    for (var i = 0; i < itemData.length; i++) {
                        var item = this._createItem(itemData[i], level);
                        $(ul).find('tbody')[0].appendChild(item[0]);
                    }
                } else {
                    var item = this._createItem(itemData, level);
                    fragment.appendChild(item[0]);
                    items.push(item[0]);
                }
            }
        },
        // append one or more items to item
        // `options.itemData` can be a item object or array of item objects
        // `options.items` will keep a list of added items
        append: function (item, options) {
            options = this._options(options, 'appended', 'appendfail', null, item);
            if (item) {
                var container = this._createContainer();
                //var last = this.last();
                this._createItems(container, null, null, options.itemData, 0, function (list) {
                    if (list.length) {
                        // some items created, update states
                        //this._setFirstLast(null, last);
                        domApi.addListClass(list.toArray(), 'ptGridVisible');
                        this._setOddEven();
                        // trigger `added` for each item
                        list.each(this.proxy(function (element) {
                            this._trigger($(element), 'added', options);
                        }, true));
                        this._animate(null, true, !this._instance.options.animateRoot || options.unanimated);
                    }
                    options.items = list;
                    this._success(item, options);
                });
            } else {
                var container = this._createContainer();
                //var last = this.last();
                this._createItems(container, null, null, options.itemData, 0, function (list) {
                    if (list.length) {
                        // some items created, update states
                        //this._setFirstLast(null, last);
                        domApi.addListClass(list.toArray(), 'ptGridVisible');
                        this._setOddEven();
                        // trigger `added` for each item
                        list.each(this.proxy(function (element) {
                            this._trigger($(element), 'added', options);
                        }, true));
                        this._animate(null, true, !this._instance.options.animateRoot || options.unanimated);
                    }
                    options.items = list;
                    this._success(item, options);
                });
            }
        },

        //------------- On Data Change or Load Update Refresh Action ------------------
        refresh: function () {
            //------------------------- hover Action ------------------------
            this._instance.jQuery.find('#ptGrid_records tr td').off("mouseenter mouseleave").hover(
                function () {
                    if (!$(this).hasClass('pt-grid-cell-selected')) {
                        $(this).parent('tr').find('td').each(function () {
                            $(this).addClass('pt-grid-cell-hover');
                        });
                    }
                }, function () {
                    $(this).parent('tr').find('td').each(function () {
                        $(this).removeClass('pt-grid-cell-hover');
                    });
                });

            //------------------------- On Row Selection ------------------------
            this._instance.jQuery.find('#ptGrid_records tr td').off('click' + this._instance.nameSpace).on('click' + this._instance.nameSpace, function () {
                $(this).parent().parent('tbody').find('tr').each(function () {
                    $(this).find('td').each(function () {
                        $(this).removeClass('pt-grid-cell-selected');
                    });
                });
                $(this).parent('tr').find('td').each(function () {
                    $(this).addClass('pt-grid-cell-selected');
                });
            });

            //------------------------- Loading Icon Update ------------------------
            this._instance.jQuery.find('.pt-loading').remove();
            this._instance.jQuery.find('.pt-loading-msg').remove();
            this._instance.jQuery.find('.pt-footer-center').html('');
            this._instance.jQuery.find('#bufferCount').html(this._instance.jQuery.find('#ptGrid_records').find('tr.ptGridRow').length);

            //------------ scoller differ handller -----------
            //this.scrollDiff = this._instance.jQuery.find('#ptGrid_records').find('table').height() - this._instance.jQuery.find('#ptGrid_records').scrollTop();
        },

        //------------- Common Util Function ------------------
        util: function () {

            //------------------------- Default Width vs User Defined ------------------------
            if (this._instance.options.width) {
                this._instance.jQuery.css('width', this._instance.options.width + 'px');
            }

            //------------------------- Infinite Load on Scoll ------------------------
            this._instance.jQuery.addClass('ptGrid ptGrid' + this._instance.index).find('#ptGrid_records')
            .on('scroll' + this._instance.nameSpace, function (e) {
                var api = $('.ptGrid' + $(e.target).data('parent')).ptGrid('api');

                if (api._instance.options.lastLeftScroll == $(e.target).scrollLeft()) {
                    //console.log('Vertical')
                    //console.log($(e.target).find('table').height());
                    //console.log($(e.target).scrollTop());
                    if ($(e.target).scrollTop() == $(e.target).find('table').height() - api.scrollDiff) {
                        //console.log('Equal re baba...........');
                        //--- Laod More Data ----------
                        api._instance.jQuery.find('.pt-footer-center').html('<span class="pt-spinner" style="width: 20px; height: 20px;"></span>');
                        api._instance.options.filter.pageNumber++;
                        api.ajaxLoad(api._instance.jQuery, api._instance.options);
                    }
                }
                else {
                    //console.log('Horizontal')
                    api._instance.jQuery.find('#ptGrid_columns').scrollLeft($(e.target).scrollLeft());
                }
                //oldScrollTop = $(window).scrollTop();
                //oldScrollLeft = $(window).scrollLeft();
                var currPos = $(e.target).scrollLeft();
                api._instance.options.lastLeftScroll = $(e.target).scrollLeft();

                if (api._instance.options.leftLlastPos < currPos) {
                    //$('#current').html('Right');
                    //console.log('Right');
                }
                if (api._instance.options.leftLlastPos > currPos) {
                    //$('#current').html('Left');
                    //console.log('left');
                }
                api._instance.options.leftLlastPos = currPos;
            });

            //------------------------- Sorting on click column ------------------------
            this._instance.jQuery.find('#ptGrid_columns')
            .on('click' + this._instance.nameSpace, function (e) {

                var $parent = $(e.target).closest("#ptGrid_columns");
                var api = $('.ptGrid' + $parent.data('parent')).ptGrid('api');

                if ($(e.target).hasClass("pt-col-header")) {
                    //---------- sort action only here ------
                    var oldclass = '';
                    if (api._instance.options.filter.sort.key == $(e.target).data('key')) {
                        oldclass = $(e.target).find('div[role="sort"]')[0].className;
                        api._instance.options.filter.sort.order = (api._instance.options.filter.sort.order == 'desc') ? 'asc' : 'desc';
                    } else {
                        api._instance.options.filter.sort.order = 'asc';
                    }
                    $parent.find('.pt-col-header').each(function () {
                        $(this).find('div[role="sort"]').html('');
                        $(this).find('div[role="sort"]').removeClass('pt-col-sort-asc');
                        $(this).find('div[role="sort"]').removeClass('pt-col-sort-desc');
                    });
                    var $sort = $(e.target).find('div[role="sort"]');
                    if (oldclass == 'pt-col-sort-asc') {
                        $sort.removeClass('pt-col-sort-asc');
                        $sort.addClass('pt-col-sort-desc');
                        $sort.html('&#9660');
                    } else {
                        $sort.removeClass('pt-col-sort-desc');
                        $sort.addClass('pt-col-sort-asc');
                        $sort.html('&#9650');
                    }
                    api._instance.options.filter.pageNumber = 1;
                    api._instance.options.filter.sort.key = $(e.target).data('key');
                    api.ajaxLoad(api._instance.jQuery, api._instance.options, true);
                }
            });

            //------------------------- tool bar search Button Click Action ------------------------
            this._instance.jQuery.find('#pt-search-button').on('click' + this._instance.nameSpace, function (e) {
                var api = $('.ptGrid' + $(e.target).data('parent')).ptGrid('api');
                api._instance.jQuery.find("#ptGrid_columns").find('.pt-grid-search').each(function () {
                    if ($(this).css('display') == 'none') {
                        $(this).css('display', 'inline-block');
                        api._instance.jQuery.find('#ptGrid_records').css('top', '58px');
                        //$(this).animate({ 'display': 'inline-block' }, 'slow');
                        //api._instance.jQuery.find('#ptGrid_records').animate({ 'top': '58px' }, 'slow');
                        api.scrollDiff = 261;
                    } else {
                        $(this).css('display', 'none');
                        api._instance.jQuery.find('#ptGrid_records').css('top', '26px');
                        api.scrollDiff = 293;
                    }
                });
            });


            this._instance.jQuery.find("#ptGrid_columns").find('.pt-grid-search-text').on('keydown' + this._instance.nameSpace, $.debounce(500, function (e) {

                var api = $('.ptGrid' + $(e.target).closest('#ptGrid_columns').data('parent')).ptGrid('api');
                var $target = $(e.target);
                var filter = { Value: $target.val(), PropertyName: $target.attr('name'), Operation: "Contains" };
                api._updateFilter(filter);
            }));
        },
    };

    // extend the base aciPluginUi class and store into aciPluginClass.plugins
    aciPluginClass.plugins.ptGrid = aciPluginClass.aciPluginUi.extend(ptGrid_core, 'ptGridCore');

    // publish the plugin & the default options
    aciPluginClass.publish('ptGrid', options);

})(jQuery);