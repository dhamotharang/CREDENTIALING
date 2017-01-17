(function ($) {
    $.fn.showFilter = function () {
        currentElement = this;
        currentElement.on('click', function () {
            var slave_filter_chart = $(this).attr('data-chart-id');
            $('#f_container').remove();
            $('body').append('<div id="f_container" class="filter_container"><div class="filter_title clearfix"> \
<label class="pull-left">Search By</label>\
            <a class="pull-right" id="close_inner_filter_btn"><i class="fa fa-close"></i></a>\
        </div>\
        <div class="clearfix">\
            <div class="filter_seperater">\
                <table>\
                    <tr>\
                        <td>\
                            <label>Payer:</label>\
                        </td>\
                        <td>\
                            <input type="text" name="PayerSlaveFilter" />\
                        </td>\
                    </tr>\
                    <tr>\
                        <td>\
                            <label>Claim Type:</label>\
                        </td>\
                        <td>\
                            <input type="text" name="claimTypeSlaveFilter" />\
                        </td>\
                    </tr>\
                    <tr>\
                        <td>\
                            <label>Billing Team:</label>\
                        </td>\
                        <td>\
                            <input type="text" name="billingTeamSlaveFilter" />\
                        </td>\
                    </tr>\
                </table>\
            </div>\
            <div class="filter_seperater">\
                <table>\
                    <tr>\
                        <td>\
                            <label>Billing Provider:</label>\
                        </td>\
                        <td>\
                            <input type="text" name="billingProviderSlaveFilter" />\
                        </td>\
                    </tr>\
                    <tr>\
                        <td>\
                            <label>DOS From:</label>\
                        </td>\
                        <td>\
                            <input type="text" />\
                        </td>\
                    </tr>\
                    <tr>\
                        <td>\
                            <label>Biller:</label>\
                        </td>\
                        <td>\
                            <input type="text" name="billerSlaveFilter" />\
                        </td>\
                    </tr>\
                </table>\
            </div>\
            <div class="filter_seperater">\
                <table>\
                    <tr>\
                        <td>\
                            <label>Rendering Provider:</label>\
                        </td>\
                        <td>\
                            <input type="text" name="renderingProviderSlaveFilter" />\
                        </td>\
                    </tr>\
                    <tr>\
                        <td>\
                            <label>DOS To:</label>\
                        </td>\
                        <td>\
                            <input type="text" />\
                        </td>\
                    </tr>\
                </table>\
            </div>\
        </div>\
        <div id="slaveFilterResult">\
        </div>\
        <div class="filter_footer">\
            <a class="btn btn-success btn-xs" id="apply_inner_filter_btn">Apply</a>\
        </div>\
    </div>');
            $('#f_container #close_inner_filter_btn').on('click', function () {
                $('#f_container').remove();
                selectedSlaveFilterList = [];
                tooltipArray = [];
            })

            $('#f_container #apply_inner_filter_btn').on('click', function () {
              
                filterChartsWithAjaxdummy('', slave_filter_chart);

            })


            var windowWidth = $(window).width();
            var currentElementLeftOffset = $(this).offset().left;
            var filterContainerWidth = $('#f_container').width();
            if ((windowWidth - currentElementLeftOffset) < filterContainerWidth - 500) {
                $('#f_container').css({ top: $(this).offset().top - 150 + 'px', right: '10px' });
            } else {
                $('#f_container').css({ top: $(this).offset().top - 150 + 'px', left: $(this).offset().left - 300 + 'px' });
            }

            var payerObejct = [{ id: 1, ListName: 'Access2', IsChecked: false }, { id: 2, ListName: 'Access2 Tampa', IsChecked: false }, { id: 3, ListName: 'All American Phy', IsChecked: false }];
            $('input[name="PayerSlaveFilter"]').innerMultiselectSearch(payerObejct, 'Payer');


            var billingProviderObject = [{ id: 1, ListName: 'Dina Amundsen', IsChecked: false }, { id: 2, ListName: 'Sheila Trissel', IsChecked: false }, { id: 3, ListName: 'Tony Martin', IsChecked: false }, { id: 4, ListName: 'Marrisa Valenti', IsChecked: false }, { id: 5, ListName: 'Carpi Ritch', IsChecked: false }];
            $('input[name="billingProviderSlaveFilter"]').innerMultiselectSearch(billingProviderObject, 'Billing Provider');

            var renderingProviderObject = [{ id: 1, ListName: 'Aasma Riaz', IsChecked: false }, { id: 2, ListName: 'Rajyalakshmi Kolli', IsChecked: false }, { id: 3, ListName: 'Gary Merlino', IsChecked: false }, { id: 4, ListName: 'Bernard Kurecki III', IsChecked: false }];
            $('input[name="renderingProviderSlaveFilter"]').innerMultiselectSearch(renderingProviderObject, 'Rendering Provider');

            var claimTypeObject = [{ id: 1, ListName: 'CAP', IsChecked: false }, { id: 2, ListName: 'FFS', IsChecked: false }, { id: 3, ListName: 'UB04', IsChecked: false }];
            $('input[name="claimTypeSlaveFilter"]').innerMultiselectSearch(claimTypeObject, 'Claim Type');

            var billingTeamObject = [{ id: 1, ListName: 'Aasma Riaz', IsChecked: false }, { id: 2, ListName: 'Rajyalakshmi Kolli', IsChecked: false }, { id: 3, ListName: 'Gary Merlino', IsChecked: false }, { id: 4, ListName: 'Bernard Kurecki III', IsChecked: false }];
            $('input[name="billingTeamSlaveFilter"]').innerMultiselectSearch(billingTeamObject, 'Billing Team');

            var billerObject = [{ id: 1, ListName: 'Aasma Riaz', IsChecked: false }, { id: 2, ListName: 'Rajyalakshmi Kolli', IsChecked: false }, { id: 3, ListName: 'Gary Merlino', IsChecked: false }, { id: 4, ListName: 'Bernard Kurecki III', IsChecked: false }];
            $('input[name="billerSlaveFilter"]').innerMultiselectSearch(billerObject, 'Biller');



        });

        $(document).on('click', function (e) {
            var isClose = true;
            if ($(e.target).attr('id') == 'f_container' || $(e.target).hasClass('cancel_btn') || $(e.target).hasClass('select_btn') || currentElement.is($(e.target))) {
                isClose = false;
            } else {
                $(e.target).parents().each(function () {
                    if ($(this).attr('id') == 'f_container' || $(this).hasClass('cancel_btn') || $(this).hasClass('select_btn') || currentElement.is($(this))) {
                        isClose = false;
                    }
                })
            }

            if (isClose) {
                $('#f_container').remove();
                selectedSlaveFilterList = [];
                tooltipArray = [];
            }
        })


    };
}(jQuery));