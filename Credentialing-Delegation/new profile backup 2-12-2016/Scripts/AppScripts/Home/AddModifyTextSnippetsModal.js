$(function () {
    //TABLE HEIGHT ONLOAD:
    $('#InsertTextDiv').css( 'height', ($(window).height() - 188));

    $('#addNewTextSnippetBtn').on('click', function () {
        TabManager.openCenterModal('/Home/AddNewInsertText', 'INSERT TEXT', '', '');
    });

    // ON TR CLICK:
    $('#textSnippetsTable tr').on('click', function () {
        $('#hiddenActionBtns').show();
    });

    $('#cancelTextSnippetBtn').on('click', function () {
        $('#hiddenActionBtns').hide();
    });

    // POPOVER:
    $('[data-toggle="popover"]').popover();

    // WINDOW RESIZE:
    $(window).resize(function(){
        $('#InsertTextDiv').css({ 'height': ($(window).height() - 188)});
    });
})
