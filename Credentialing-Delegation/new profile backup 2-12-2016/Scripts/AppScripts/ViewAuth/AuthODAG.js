$(document).ready(function () {
    /* Unchecking icheck Radio Buttons in ODAG Tab */
    $('.flat').on("ifClicked", function () {
        if ($(this).is(':checked')) {
            $(this).iCheck('uncheck');
        }
    })
})