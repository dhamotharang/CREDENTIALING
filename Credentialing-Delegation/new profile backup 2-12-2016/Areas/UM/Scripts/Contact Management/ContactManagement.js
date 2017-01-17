var ShowHideAddBtn = function () {
    if ($("#EntityPanel").is(":hidden")) {
        $('#AddBtn_ContactMgmt').hide();
    }
    else
        $('#AddBtn_ContactMgmt').show();
}
var ShowAddBtn = function(){
    $('#AddBtn_ContactMgmt').show();
}