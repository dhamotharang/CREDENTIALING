$(document).ready(function () {

    //$('.claimlist_table tbody tr').bind('click', function () {
    //    $('.x_modal').fadeIn();
    //});


    //$('.claimlist_table tbody tr').bind('click', function () {
        
    //    $('#toggle_div').remove();
    //    $('tr').removeClass('active_sl_row');
    //    $(this).addClass('active_sl_row');
    //    $(this).after('<tr id="toggle_div"><td colspan="7" class="col-lg-12">' + $('#hidden_div').html() + '</td></tr>');
    //    $('.claimdetails_close_btn').bind('click', function () {
    //        $('tr').removeClass('active_sl_row');
    //        $('#toggle_div').remove();
    //    });

    //});

   


    $('.x_modal-close-btn').bind('click', function () {
        $('.x_modal').fadeOut();
    });


    $('#graphical_view_btn').bind('click', function () {
        $('#table_view').fadeOut();
        $('#graph_view').fadeIn();
    });

    $('#tabular_view_btn').bind('click', function () {
        $('#graph_view').fadeOut();
        $('#table_view').fadeIn();
    });

    //$('[name="selectyear"]').change(function () {
      
    //    chart.load({
    //        columns: [
    //                         ['x', "January", "February", "March", "April", "May", "June",
    //             "July", "August", "September", "October", "November", "December"],
    //                        ['CAP', 60, 30, 100, 90, 200, 150, 50, 60, 30, 100, 90, 45],
    //                         ['FFS', 130, 80, 200, 250, 400, 150, 250, 130, 80, 200, 250, 234],
    //        ]

    //    });
    //});

    //------------------scrollable table---------------------------

    // Pend Auth Summary Chart//


});