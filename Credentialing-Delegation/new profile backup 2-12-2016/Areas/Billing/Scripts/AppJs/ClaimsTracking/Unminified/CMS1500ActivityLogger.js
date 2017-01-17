$('#ActivityLoggerCMS1500Form').find('.row').each(function () {
    var current_row = $(this);
    var row_height = current_row.height();
    current_row.find('.cms_div').each(function () {
        $(this).height(row_height);
    });
});