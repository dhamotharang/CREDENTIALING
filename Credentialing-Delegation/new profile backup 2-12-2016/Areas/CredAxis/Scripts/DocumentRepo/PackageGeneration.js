$("#container1, #selectedContainer, .container2, .container2").sortable({
    connectWith: ".connectedSortable",
    cancel: ".no-darg",
    helper: function (e, li) {
        this.copyHelper = li.clone().insertAfter(li);

        $(this).data('copied', false);

        return li.clone();
    },
    stop: dropHandle
}).disableSelection();

function dropHandle(event, ui) {
    var copied = $(this).data('copied');

    if (!copied) {
        this.copyHelper.remove();
    }

    this.copyHelper = null;
}

$("#selectedContainer").sortable({
    receive: function (e, ui) {
        ui.sender.data('copied', true);
    }
});