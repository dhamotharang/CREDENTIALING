var ClientCheck = function () {
    if (window.screen.availHeight < 700 || window.screen.availWidth < 1024) {
        $('.screencheckalert').show(function () {
            var msg = "Screen Resolution Recommended is 1024 x 720 Pixels Minimum. At Present Size is " + window.screen.availWidth + " x " + window.screen.availHeight;
            $("#alertMsgScreen").text(msg);
        });
    }
    else {
        $('.screencheckalert').hide();
    }
}

var BrowserWindowCheck = function () {
    if ($(window).height() < 600 || $(window).width() < 1024) {
        $('.browsercheckalert').show(function () {
            var msg = "The Browser Screen Recommended is 1024 x 720 Pixels Minimum. At Present Size is " + $(window).width() + " x " + $(window).height();
            $("#alertMsgBrowser").text(msg);
        });
    }
    else {
        $('.browsercheckalert').hide();
    }
}
$(function () {
    ClientCheck();
    BrowserWindowCheck();
})
$(window).resize(function () {
    BrowserWindowCheck();
});