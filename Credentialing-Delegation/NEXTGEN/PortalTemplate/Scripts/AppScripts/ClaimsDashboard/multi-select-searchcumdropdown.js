(function ($) {
  
    $.fn.multiselectSearch = function (object, param) {
        var currentElement = this;

        currentElement.on('focus', function () {
            var dropdownTemplate = '<ul class="multiselect_list">';
            for (var i = 0; i < object.length; i++) {
                if (object[i].IsChecked) {
                    dropdownTemplate = dropdownTemplate + '<li><input type="checkbox" id="checkid' + i + '" checked/><label for="checkid' + i + '">' + object[i].ListName + '</label></li>'
                } else {
                    dropdownTemplate = dropdownTemplate + '<li><input type="checkbox" id="checkid' + i + '" /><label for="checkid' + i + '">' + object[i].ListName + '</label></li>'

                }
            }
            dropdownTemplate = dropdownTemplate + '<li><a class="btn btn-xs btn-warning pull-left cancel_btn">Cancel</a><a class="btn btn-xs btn-success pull-right select_btn">Select</a></li></ul>';
            currentElement.after(dropdownTemplate);

            $('.cancel_btn').on('click', function () {
                currentElement.next('.multiselect_list').remove();
            });

            $('.select_btn').on('click', function () {
                var internalFilterList = [];
                var checkedElements = currentElement.next('.multiselect_list').find('li input[type="checkbox"]:checked').next('label');
                for (var i = 0; i < checkedElements.length; i++) {
                    internalFilterList.push($(checkedElements[i]).text());
                }

                selectedFilterList.push({ labelName: param, items: internalFilterList });
                var dropTemplate = '<div class="clearfix">';
                for (var i = 0; i < selectedFilterList.length; i++) {
                    dropTemplate = dropTemplate + '<span> ' + selectedFilterList[i].labelName + ' :</span><span>';
                  
                    for (var j = 0; j < selectedFilterList[i].items.length; j++) {
                       
                        dropTemplate = dropTemplate + '<span  class="filter_text_badge">' + selectedFilterList[i].items[j] + '</span>'

                      
                       
                    }
                    dropTemplate = dropTemplate + '</span>';
                }
                dropTemplate = dropTemplate + '<a class="pull-right btn" id="fav_filter_add"><i style="font-size: 15px" class="fa fa-heartbeat"></i></a></div>';
                $('#search_by_titles').html(dropTemplate);
                $('#search_by_titles').show();
                currentElement.next('.multiselect_list').remove();

                $('#fav_filter_add').on('click', function () {

                    // var top = $(this).offset().top;
                    var left = $(this).offset().left - 400;
                    $('.add_fav_filter_container').remove();
                    $('#fav_filter_add').after("<div style='top:-16px;left:" + left + "px' class='add_fav_filter_container'><div><h6>Favorite</h6></div><div class='fav_new_filter_field_container'><label>Filter Name: &nbsp;</label><input type='text' name='addNewFilterName' /></div><div class='clearfix'><a class='btn btn-xs btn-success pull-right' id='saveNewFavAdd'>Save</a><a class='btn btn-xs btn-warning pull-left' id='closeNewFavAdd'>Cancel</a></div></div>");

                    $('#closeNewFavAdd').on('click', function () {
                        $('.add_fav_filter_container').remove();
                    })


                    $('#saveNewFavAdd').on('click', function () {
                        $('#search_by_titles').prepend('<label class="filterName_label"><i class="fa fa-heartbeat"></i> <span id="filterName_span">' + $('input[name="addNewFilterName"]').val() + '</span></label>');
                        $('.add_fav_filter_container').remove();
                        $('#fav_filter_add').remove();
                        $('.tracking-filter-form').hide();
                    })

                });
            });

        });




        $(document).on('click', function (e) {
            var isClose = true;
            if ($(e.target).hasClass('multiselect_list') || $(e.target).hasClass('cancel_btn') || currentElement.is($(e.target))) {
                isClose = false;
            } else {
                $(e.target).parents().each(function () {
                    if ($(this).hasClass('multiselect_list') || $(this).hasClass('cancel_btn') || currentElement.is($(this))) {
                        isClose = false;
                    }
                })
            }

            if (isClose) {
                currentElement.next('.multiselect_list').remove();
            }
        })

    }

    $.fn.innerMultiselectSearch = function (object, param) {
        var currentElement = this;

        currentElement.on('focus', function () {
            var dropdownTemplate = '<ul class="multiselect_list_small">';
            for (var i = 0; i < object.length; i++) {
                if (object[i].IsChecked) {
                    dropdownTemplate = dropdownTemplate + '<li><input type="checkbox" id="checkid' + i + '" checked/><label for="checkid' + i + '">' + object[i].ListName + '</label></li>'
                } else {
                    dropdownTemplate = dropdownTemplate + '<li><input type="checkbox" id="checkid' + i + '" /><label for="checkid' + i + '">' + object[i].ListName + '</label></li>'

                }
            }
            dropdownTemplate = dropdownTemplate + '<li><a class="btn btn-xs btn-warning pull-left cancel_btn">Cancel</a><a class="btn btn-xs btn-success pull-right select_btn">Select</a></li></ul>';
            currentElement.after(dropdownTemplate);

            $('.multiselect_list_small .cancel_btn').on('click', function () {
                currentElement.next('.multiselect_list_small').remove();
            });

            $('.multiselect_list_small .select_btn').on('click', function () {
                var internalFilterList = [];
                var checkedElements = currentElement.next('.multiselect_list_small').find('li input[type="checkbox"]:checked').next('label');
                var checkedElementsCount = checkedElements.length;
                var dropTemplate = '';
                
                var tooltipTemplate = '';
                for (var i = 0; i < checkedElements.length; i++) {
                    tooltipTemplate = tooltipTemplate + $(checkedElements[i]).text()+', ';
                }
                tooltipArray.push(tooltipTemplate);
                selectedSlaveFilterList.push({ Count: checkedElementsCount, param: param });
                for (var i = 0; i < selectedSlaveFilterList.length; i++) {
                    dropTemplate = dropTemplate + "<span class='filter_text_badge' data-toggle='tooltip' title='" + tooltipArray[i] + "'>" + selectedSlaveFilterList[i].Count + " " + selectedSlaveFilterList[i].param + "s Selected...</span> ";
                    if ((i + 1) % 5 == 0) {
                        dropTemplate = dropTemplate + '</br>';
                    }
                }
                $('#slaveFilterResult').html(dropTemplate);
                $('[data-toggle="tooltip"]').tooltip();
                currentElement.next('.multiselect_list_small').remove();

            });

        });

        $(document).on('click', function (e) {

            var isClose = true;
            if ($(e.target).hasClass('multiselect_list_small') || $(e.target).hasClass('select_btn') || $(e.target).hasClass('cancel_btn') || currentElement.is($(e.target))) {
                isClose = false;
            } else {
                $(e.target).parents().each(function () {
                    if ($(this).hasClass('multiselect_list_small') || $(e.target).hasClass('select_btn') || $(this).hasClass('cancel_btn') || currentElement.is($(this))) {
                        isClose = false;
                    }
                })
            }

            if (isClose) {
                currentElement.next('.multiselect_list_small').remove();
            }
        })

    }

    $.fn.detailedMultiselectSearch = function (object, param) {
        var currentElement = this;

        currentElement.on('focus', function () {
            var dropdownTemplate = '<ul class="multiselect_list">';
            for (var i = 0; i < object.length; i++) {
                if (object[i].IsChecked) {
                    dropdownTemplate = dropdownTemplate + '<li><input type="checkbox" id="checkid' + i + '" checked/><label for="checkid' + i + '">' + object[i].ListName + '</label></li>'
                } else {
                    dropdownTemplate = dropdownTemplate + '<li><input type="checkbox" id="checkid' + i + '" /><label for="checkid' + i + '">' + object[i].ListName + '</label></li>'

                }
            }
            dropdownTemplate = dropdownTemplate + '<li><a class="btn btn-xs btn-warning pull-left cancel_btn">Cancel</a><a class="btn btn-xs btn-success pull-right select_btn">Select</a></li></ul>';
            currentElement.after(dropdownTemplate);

            $('.cancel_btn').on('click', function () {
                currentElement.next('.multiselect_list').remove();
            });

            $('.select_btn').on('click', function () {
                var internalFilterList = [];
                var checkedElements = currentElement.next('.multiselect_list').find('li input[type="checkbox"]:checked').next('label');
                for (var i = 0; i < checkedElements.length; i++) {
                    internalFilterList.push($(checkedElements[i]).text());
                }

                selectedFilterList.push({ labelName: param, items: internalFilterList });
                var dropTemplate = '<div class="clearfix">';
                for (var i = 0; i < selectedFilterList.length; i++) {
                    dropTemplate = dropTemplate + '<span> ' + selectedFilterList[i].labelName + ' :</span><span>'
                    for (var j = 0; j < selectedFilterList[i].items.length; j++) {
                        dropTemplate = dropTemplate + '<span  class="filter_text_badge">' + selectedFilterList[i].items[j] + '</span><span>/</span>'
                    }
                    dropTemplate = dropTemplate + '</span>';
                }
                dropTemplate = dropTemplate + '<a class="pull-right btn" id="fav_filter_add"><i style="font-size: 15px" class="fa fa-heartbeat"></i></a></div>';
                $('#search_by_titles_detailed').html(dropTemplate);
                $('#search_by_titles_detailed').show();
                currentElement.next('.multiselect_list').remove();

                $('#fav_filter_add').on('click', function () {

                    // var top = $(this).offset().top;
                    var left = $(this).offset().left - 200;
                    $('.add_fav_filter_container').remove();
                    $('#fav_filter_add').after("<div style='top:-16px;left:" + left + "px' class='add_fav_filter_container'><div><h6>Favorite</h6></div><div class='fav_new_filter_field_container'><label>Filter Name: &nbsp;</label><input type='text' name='addNewFilterName' /></div><div class='clearfix'><a class='btn btn-xs btn-success pull-right' id='saveNewFavAdd'>Save</a><a class='btn btn-xs btn-warning pull-left' id='closeNewFavAdd'>Cancel</a></div></div>");

                    $('#closeNewFavAdd').on('click', function () {
                        $('.add_fav_filter_container').remove();
                    })


                    $('#saveNewFavAdd').on('click', function () {
                        $('#search_by_titles_detailed').prepend('<label class="filterName_label"><i class="fa fa-heartbeat"></i> <span id="filterName_span">' + $('input[name="addNewFilterName"]').val() + '</span></label>');
                        $('.add_fav_filter_container').remove();
                        $('#fav_filter_add').remove();
                        $('.tracking-filter-form').hide();
                    })

                });
            });

        });




        $(document).on('click', function (e) {
            var isClose = true;
            if ($(e.target).hasClass('multiselect_list') || $(e.target).hasClass('cancel_btn') || currentElement.is($(e.target))) {
                isClose = false;
            } else {
                $(e.target).parents().each(function () {
                    if ($(this).hasClass('multiselect_list') || $(this).hasClass('cancel_btn') || currentElement.is($(this))) {
                        isClose = false;
                    }
                })
            }

            if (isClose) {
                currentElement.next('.multiselect_list').remove();
            }
        })

    }


}(jQuery));