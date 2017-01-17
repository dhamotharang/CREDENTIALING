$(function () {

    //LoadStagingTrackerTable();
    $('table.displayTableStagingTracker').DataTable(
       {
           "columns": [
                        { "width": "8%" },
                        { "width": "9%" },
                        { "width": "14%" },
                        { "width": "9%" },
                        { "width": "9%" },
                        { "width": "9%" },
                        { "width": "9%" },
                        { "width": "9%" },
                        { "width": "7%" },
                        { "width": "8%" },
                        { "width": "9%" }
           ],
           language: {
               search: "_INPUT_",
               searchPlaceholder: "Search"
           },
           dom: 'Bfrtip',
           buttons: [

               'excel'
           ],
           fixedColumns: true,
           responsive: true
       });

    $('#StagingTracker tfoot th').each(function () {
        var title = $(this).text();
        $(this).html('<input type="text" class"inputStagingTracker" placeholder="Search ' + title + '" />');
    });


    // DataTable
    var table = $('table.displayTableStagingTracker').DataTable();
    $('input').on('keyup', function () {
        table.search(this.value).draw();
        var info = $('table.displayTableStagingTracker').DataTable().page.info();
        $('#DisplayFilteredCount').html(info.recordsDisplay);
    });
    table.columns().every(function () {
        var that = this;

        $('input', this.footer()).on('keyup change', function () {
            if (that.search() !== this.value) {
                that
                    .search(this.value)
                    .draw();
                var info = $('table.displayTableStagingTracker').DataTable().page.info();
                $('#DisplayFilteredCount').html(info.recordsDisplay);
            }
        });
    });


});

function ExportToExcel() {
    $(".buttons-excel").trigger("click");
}