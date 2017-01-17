//--------------- Ready Action ----------------------
var opened = false;
$(document).click(function (event) {
    if (!$(event.target).hasClass("ringMyBell") && !$(event.target).hasClass("checkMe")) {
        $('#alertArea').hide();
        $('#alertArea').css("display", "");
        opened = true;
    } else {
        opened = true;
        $('#alertArea').show();
    }
});

function ViewAll() {
    $("#ViewAll").on("click", function () {
        var count = 0;
        $("#alertArea").html('');
        if ($(this).prop("checked") == true) {
            $("#alertArea").append('<li class="pull-right" style="background-color:#ddd"><input class="checkMe" type="checkbox" id="ViewAll" checked/>View All</li>');
            if (myData.length == 0) {
                $("#alertArea").append('<li id="alertNotifications"><div><p class="text-center">No New Notifications Available.</p></div></a><hr style="padding:0; margin:0"/></li>');
            } else {
                for (msg in myData) {
                    if (myData[msg].AcknowledgementStatus == "Unread") {
                        $("#alertArea").append('<li id="alertNotifications" style="background-color:#F1F1F1"><a href="/Profile/MasterProfile/ChangeNotificationStatus?dashboardNotificationID=' + myData[msg].UserDashboardNotificationID + '"><div><p>' + myData[msg].ActionPerformed + ' - ' + myData[msg].ActionPerformedByUser + '&nbsp;<i class="fa fa-clock-o"></i></p></div></a><hr style="padding:0; margin:0"/></li>');
                    } else {
                        $("#alertArea").append('<li id="alertNotifications" style="background-color:#F9F9H9"><a href="/Profile/MasterProfile/ChangeNotificationStatus?dashboardNotificationID=' + myData[msg].UserDashboardNotificationID + '"><div><p>' + myData[msg].ActionPerformed + ' - ' + myData[msg].ActionPerformedByUser + '&nbsp;<i class="fa fa-clock-o"></i></p></div></a><hr style="padding:0; margin:0"/></li>');
                    }
                };
            }
        } else {
            $("#alertArea").append('<li class="pull-right" style="background-color:#ddd"><input class="checkMe" type="checkbox" id="ViewAll"/>View All</li>');
            for (msg in myData) {
                if (myData[msg].AcknowledgementStatus == "Unread") {
                    count++;
                    $("#alertArea").append('<li id="alertNotifications" style="background-color:#F1F1F1"><a href="/Profile/MasterProfile/ChangeNotificationStatus?dashboardNotificationID=' + myData[msg].UserDashboardNotificationID + '"><div><p>' + myData[msg].ActionPerformed + ' - ' + myData[msg].ActionPerformedByUser + '&nbsp;<i class="fa fa-clock-o"></i></p></div></a><hr style="padding:0; margin:0"/></li>');
                }
            };
            if (count == 0) {
                $("#alertArea").append('<li id="alertNotifications"><div><p class="text-center">No New Notifications Available.</p></div></a><hr style="padding:0; margin:0"/></li>');
            }
        }
        $("#alertArea").append("<script>ViewAll();");
    });
};
var myData;
var cduserdata;

var Notify = function () {
    $.ajax({
        url: rootDir + '/Dashboard/GetMyNotification',
        data: {
            format: 'json'
        },
        error: function () {
            //$('#info').html('<p>An error has occurred</p>');
        },
        dataType: 'json',
        success: function (data) {
            console.log("data");
            console.log(data);
            cduserdata = data;
            myData = data.cdUser.DashboardNotifications;
            var cnt = 0;
            if (myData.length == 0) {
                $("#alertArea").append('<li id="alertNotifications"><div><p class="text-center">No New Notifications Available.</p></div></a><hr style="padding:0; margin:0"/></li>');
            } else {
                $("#alertArea").append('');
                for (msg in myData) {
                    if (data.cdUser.DashboardNotifications[msg].AcknowledgementStatus == "Unread") {
                        cnt++;
                        if (data.cdUser.DashboardNotifications[msg].Action == 'Daily task') {
                            $("#alertArea").append('<li id="alertNotifications" style="background-color:#F1F1F1"><a href="/Profile/MasterProfile/ChangeNotificationStatus?dashboardNotificationID=' + myData[msg].UserDashboardNotificationID + '"><div><p>' + myData[msg].ActionPerformed + ' - ' + myData[msg].ActionPerformedByUser + '&nbsp;<i class="fa fa-clock-o"></i></p></div></a><hr style="padding:0; margin:0"/></li>');
                        } else {
                            $("#alertArea").append('<li id="alertNotifications" style="background-color:#F1F1F1"><a href="/Profile/MasterProfile/ChangeNotificationStatus?dashboardNotificationID=' + myData[msg].UserDashboardNotificationID + '"><div><p>' + myData[msg].ActionPerformed + ' - ' + myData[msg].ActionPerformedByUser + '&nbsp;<i class="fa fa-clock-o"></i></p></div></a><hr style="padding:0; margin:0"/></li>');
                        }
                    }
                };
                if (cnt == 0) {
                    $("#alertArea").append('<li id="alertNotifications"><div><p class="text-center">No New Notifications Available.</p></div></a><hr style="padding:0; margin:0"/></li>');
                } else {
                    $("#numberOfNotifications").text(cnt);
                }
            }
        },
        type: 'GET'
    });
}
//---------------- END - Ready Action ------------------------

//----------- Start - Menu Toggle -------------------
var togglemenu = function () {
    $("#sidemenu").toggleClass("menu-in");
    $("#page-wrapper").toggleClass("menuup");
    $("#menu-toggle").toggleClass("btn-menuup");
}
//----------- End - Menu Toggle -------------------

$(function () {
    $('#side-menu').metisMenu();
});

//----------------------- Start - Collapse Menu Features -------------------
//Loads the correct sidebar on window load,
//collapses the sidebar on window resize.
// Sets the min-height of #page-wrapper to window size
$(function () {
    $(window).bind("load resize", function () {
        topOffset = 50;
        width = (this.window.innerWidth > 0) ? this.window.innerWidth : this.screen.width;
        if (width < 1025) {
            $('div.navbar-collapse').addClass('collapse')
            topOffset = 100; // 2-row-menu
        } else {
            $('div.navbar-collapse').removeClass('collapse')
        }

        height = (this.window.innerHeight > 0) ? this.window.innerHeight : this.screen.height;
        height = height - topOffset;
        if (height < 1) height = 1;
        if (height > topOffset) {
            $("#page-wrapper").css("min-height", (height) + "px");
        }
    });

    //------ author : krglv -----------
    //Check to see if the window is top if not then display button
    $(window).scroll(function () {
        if ($(this).scrollTop() > 100) {
            $('.scrollToTop').fadeIn();
        } else {
            $('.scrollToTop').fadeOut();
        }
    });
    //Click event to scroll to top
    $('.scrollToTop').click(function () {
        $('html, body').animate({ scrollTop: 0 }, 800);
        return false;
    });
});

//--------------- file name Wrap-text author : krglv ---------------
function setFileNameWith(file) {
    $(file).parent().parent().find(".jancyFileWrapTexts").find("span").width($(file).parent().parent().width() - 197);
}
//----------------------- END - Collapse Menu Features -------------------

//------------------ Start - Footer Data --------------------
$(function () {
    $('[data-toggle="popover"]').popover();
    $('[data-toggle="tooltip"]').tooltip();
})

$(document).ready(function () {
    console.log("changing size");
    var screen = $(window).width();

    if (screen < 1025) {
        $("#sidemenu").addClass("menu-in");
        $("#page-wrapper").addClass("menuup");
    }
});

//------------------ END - Footer Data --------------------

//--------------- Start - LogIn/LogOut Action ----------------------
$(document).ready(function () {
    var cnd = $.connection.cnDHub;

    cnd.client.logoutCall = function (userSessionId) {
        if ($('#userSessionId').val() == userSessionId) {
            //$('#logoutBtn').trigger('click');
            document.getElementById('logoutForm').submit();
        };
    };

    //-------------- need to check condition and oiptimized -----------------
    cnd.client.Notification = function () {
        Notify();
    }

    // Start the connection.
    $.connection.hub.start().done(function () {
        Notify();
        $('#logoutBtn').click(function () {
            cnd.server.logout($('#userSessionId').val());
            document.getElementById('logoutForm').submit();
        });
    });

    $('#alertArea').hide();
    $('#bell').click(function () {
        if (opened == true) {
            $('#alertArea').hide();
        }
        $('#alertArea').css("display", "");
        $('#alertArea').toggle();
    });
    ViewAll();
});
//---------------- END - LogIn/LogOut Action ------------------------