
function appendTabContent(data, tabcontentid) {
    $('#' + tabcontentid).html(data);
}
$('.innerTabsArea li a').off('click').on('click', function () {
    var url_path = $(this).attr('data-tab-path');
    $.ajax({
        type: 'GET',
        url: url_path,
        success: function (data) {
            $("#queueTable").empty();
            appendTabContent(data, "queueTable");

            LoadPrestageLoggerTable();
            LoadFileTrackerTable();
        }
    })
})

function EdwDataBaseExportToExcel()
{
 $("#EdwDatabaseTable").table2excel({
        // exclude CSS class
       
        name: "EDW",
        filename: "EDW Database" //do not include extension
    });
}



function LoadTableLiveFileTracker(sel) {
   // $("#LiveTrackerInfoTable").empty();
    var url_path = "/ETL/EdwLogger/FileTrackerDataForProject";
    $.ajax({
        type: 'GET',
        url: url_path,
        data: { ProjectName: sel },
        success: function (data) {
            appendTabContent(data, "LiveTrackerInfoTable");

            LoadFileTrackerTable();

        }
    })
}

//$('#ProjectInfoTable #SelectProjDiv').off('change', '#SelectProjPrestageLoggerDD').on('change', '#SelectProjPrestageLoggerDD', function () {

//    var url_path = "/ETL/EdwLogger/PrestageLoggerDataForProject";
//    $.ajax({
//        type: 'GET',
//        url: url_path,
//        data: { ProjectName: sel },
//        success: function (data) {
//            appendTabContent(data, "ProjectInfoTable");

//            LoadPrestageLoggerTable();
//        }
//    })
//});


function LoadTable(sel) {


    var url_path = "/ETL/EdwLogger/PrestageLoggerDataForProject";
    $.ajax({
        type: 'GET',
        url: url_path,
        data: { ProjectName: sel },
        success: function (data) {
            appendTabContent(data, "ProjectInfoTable");

            LoadPrestageLoggerTable();
        }
    })

}


function LoadPrestageLoggerTable()
{

    $('table.display').DataTable(
        {
            "columns": [
    { "width": "10%" },
   { "width": "30%" },
   { "width": "10%" },
    { "width": "10%" },
   { "width": "10%" },
   { "width": "10%" },
    { "width": "10%" }
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


    $('#PrsetageLoggerTable tfoot th').each(function () {
        var title = $(this).text();
        $(this).html('<input type="text" placeholder="Search ' + title + '" />');
    });

    // Apply the search
    var table = $('table.display').DataTable();

    $('input').on('keyup', function () {
        table.search(this.value).draw();
        var info = $('table.display').DataTable().page.info();
        $('#DisplayFilteredCount').html(info.recordsDisplay);
    });

    table.columns().every(function () {



        var that = this;

        $('input', this.footer()).on('keyup change', function () {
            if (that.search() !== this.value) {
                that
                    .search(this.value)
                    .draw();

                var info = $('table.display').DataTable().page.info();
                $('#DisplayFilteredCount').html(info.recordsDisplay);
            }
        });
    });
}

function LoadFileTrackerTable()
{


    $('table.displayFileTrackerTable').DataTable(
{
    "columns": [
            { "width": "10%" },
{ "width": "30%" },
{ "width": "10%" },
{ "width": "10%" },
{ "width": "10%" },
{ "width": "10%" },
{ "width": "10%" },
    { "width": "10%" }
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


    $('#FileTrackerTable tfoot th').each(function () {
        var title = $(this).text();
        $(this).html('<input type="text" placeholder="Search ' + title + '" />');
    });

    // Apply the search
    var table = $('table.displayFileTrackerTable').DataTable();

    $('input').on('keyup', function () {
        table.search(this.value).draw();
        var info = $('table.displayFileTrackerTable').DataTable().page.info();
        $('#DisplayFilteredCount').html(info.recordsDisplay);
    });

    table.columns().every(function () {
        var that = this;

        $('input', this.footer()).on('keyup change', function () {
            if (that.search() !== this.value) {
                that
                    .search(this.value)
                    .draw();

                var info = $('table.displayFileTrackerTable').DataTable().page.info();
                $('#DisplayFilteredCount').html(info.recordsDisplay);
            }
        });
    });


}


$('.OpenEdwDatabaseModal').on('click', function () {
    TabManager.openFloatingModal('/ETL/EdwLogger/DisplayEdwDatabaseModalBody', '~/Areas/ETL/Views/EdwLogger/EdwDatabasesModal/_EdwDatabaseModalHeader.cshtml', '~/Areas/ETL/Views/EdwLogger/EdwDatabasesModal/_EdwDatabasesModalFooter.cshtml', '', '', '');
})



$(".queueTabs")
    .off("click", ".ipaQueueMenu")
    .on("click", ".ipaQueueMenu", function () {
        $(".ipaQueueMenu").removeClass("active");
        $(this).addClass("active");
        //var tabId = $(this).attr("id");
        //$("#" + tabId).find('a.innerTabsLinks').addClass("topminus3");
        var queueType = $(this).attr('data-queue-type');
        var queuTab = $(this).attr('data-queue-tab');
        var data = { QueueType: queueType, QueueTab: queuTab }
    });


function ExportToExcel() {
    $(".buttons-excel").trigger("click");
}