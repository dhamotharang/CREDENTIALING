$(function () {
    $("#Newsortable").sortable({
        start: function (event, ui) {
            iBefore = ui.item.index();
        },
        update: function (event, ui) {
            iAfter = ui.item.index();
            evictee = $('#sortable li:eq(' + iAfter + ')');
            evictor = $('#sortable li:eq(' + iBefore + ')');

            evictee.replaceWith(evictor);
            if (iBefore > iAfter)
                evictor.after(evictee);
            else
                evictor.before(evictee);
        }
    });

    $("#sortable").sortable({
        start: function (event, ui) {
            iBefore = ui.item.index();
        },
        update: function (event, ui) {
            iAfter = ui.item.index();
            evictee = $('#Newsortable li:eq(' + iAfter + ')');
            evictor = $('#Newsortable li:eq(' + iBefore + ')');

            evictee.replaceWith(evictor);
            if (iBefore > iAfter)
                evictor.after(evictee);
            else
                evictor.before(evictee);
        }
    });
    //$("#Newsortable").disableSelection();
});

$('#Float_Settings').click(function () {
    event.stopPropagation();
    $('.Settings_Area').css("width", "300px");
})
$('#Close_Settings').click(function () {
    $('.Settings_Area').css("width", "0%");
})

function ToggleChart(hideThisCahrt) {
    $('#' + hideThisCahrt).toggle(500);
}

function toggleClass() {
    $('.drag_btn').toggle(100);
}


function toggleTileView() {
    $('#tile_list_rewamp').toggle();
    $('#tile_list_new').toggle();
}



$(window).click(function () {
    $('.Settings_Area').css("width", "0%");
});

$('.Settings_Area').click(function (event) {
    event.stopPropagation();
});