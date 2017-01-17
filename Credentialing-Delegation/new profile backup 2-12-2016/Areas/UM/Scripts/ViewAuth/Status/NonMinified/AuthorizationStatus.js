$(document).ready(function () {
    /* Activity-Panel Toggle Functionality in Status Tab */
    var flag = false;
    $(".activitybtns").hide();
    $("#panel").hide();
    $("#flip").click(function () {
        if (flag == false) {
            $("#panel").slideDown('1000');
            $(".activitybtns").show().slideDown('1000');
            var icon = $("#flip").find('i');
            if (icon.hasClass("fa fa-chevron-down")) {
                icon.removeClass("fa fa-chevron-down").addClass("fa fa-chevron-up")
            }
            flag = true;
        }
        else {
            $("#panel").slideUp('1000');
            $(".activitybtns").hide().slideUp('1000');
            var icon = $("#flip").find('i');
            if (icon.hasClass("fa fa-chevron-up")) {
                icon.removeClass("fa fa-chevron-up").addClass("fa fa-chevron-down")
            }
            flag = false;
        }
    })
})