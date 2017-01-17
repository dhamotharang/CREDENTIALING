$(document).ready(function () {
    /*Open Document Cabin*/
    $('.DocCab-Expand').click(function () {
        $('.DocumentCabin').css("width", "83%");
        $('.DocCab-Collapse').css("display", "block");
        $('.DocCab-Expand').css("display", "none");
    });
    $('.DocCab-Collapse').click(function () {
        collapseCabin();
    });

    $('#officeFacesheet').click(function () {
        $('.Documents-List').hide();
        $('.Document-Preview').show();
    });

    $('.document-list-back').click(function () {
        $('.Documents-List').show();
        $('.Document-Preview').hide();
    })
    $('.close-cabin').click(function () {
        collapseCabin();
    })

    /* Close Document Cabin*/
    function collapseCabin() {
        $('.DocumentCabin').css("width", "0%");
        $('.DocCab-Collapse').css("display", "none");
        $('.DocCab-Expand').css("display", "block");
        $('.Documents-List').show();
        $('.Document-Preview').hide();
    }

});