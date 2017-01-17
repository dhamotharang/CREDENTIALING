$(document).ready(function() {
    //$('.box-item').draggable({
    //    cursor: 'move',
    //    helper: "clone"
    //});

    //$("#container1").droppable({
    //    drop: function (event, ui) {
    //        var itemid = $(event.originalEvent.toElement).attr("itemid");
    //        $('.box-item').each(function () {
    //            if ($(this).attr("itemid") === itemid) {
    //                $(this).appendTo("#container1");
    //            }
    //        });
    //    }
    //});

    //$("#container2").droppable({
    //    drop: function (event, ui) {
    //        var itemid = $(event.originalEvent.toElement).attr("itemid");
    //        $('.box-item').each(function () {
    //            if ($(this).attr("itemid") === itemid) {
    //                $(this).appendTo("#container2");
    //            }
    //        });
    //    }
    //});

    $("#container1, #container2").sortable({
        connectWith: ".connectedSortable",
        cancel: ".no-darg",
        helper: function (e, li) {
            this.copyHelper = li.clone().insertAfter(li);

            $(this).data('copied', false);

            return li.clone();
        },
        stop:dropHandle
    }).disableSelection();

    function dropHandle(event,ui) {
        var copied = $(this).data('copied');

        if (!copied) {
            this.copyHelper.remove();
        }

        this.copyHelper = null;
    }

    $("#container2").sortable({
        receive: function (e, ui) {
            ui.sender.data('copied', true);
        }
    });

    $("#container1, #container3").sortable({
        connectWith: ".connectedSortable",
        cancel: ".no-darg",
        helper: function (e, li) {
            this.copyHelper = li.clone().insertAfter(li);

            $(this).data('copied', false);

            return li.clone();
        },
        stop:dropHandle
    }).disableSelection();

    $("#container3").sortable({
        receive: function (e, ui) {
            ui.sender.data('copied', true);
        }
    });
});

