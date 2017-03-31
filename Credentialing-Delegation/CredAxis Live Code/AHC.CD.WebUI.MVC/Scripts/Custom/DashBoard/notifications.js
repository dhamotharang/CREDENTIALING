
$(document).ready(function () {
    var cnd = $.connection.cnDHub;

    cnd.client.logoutCall = function (userSessionId) {

    };
    // Start the connection.
    $.connection.hub.start().done(function () {
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

    $("#EndingNotificationArea").hide();
    $("#ImportantNotificationArea").hide();
    $(".dropdown-menu").click(function (e) {

        e.stopPropagation();
    });


    Tasknotify();
});
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
            }
            else {
                for (msg in myData) {
                    if (myData[msg].AcknowledgementStatus == "Unread") {

                        $("#alertArea").append('<li id="alertNotifications" style="background-color:#F1F1F1"><a href="/Profile/MasterProfile/ChangeNotificationStatus?dashboardNotificationID=' + myData[msg].UserDashboardNotificationID + '"><div><p>' + myData[msg].ActionPerformed + ' - ' + myData[msg].ActionPerformedByUser + '&nbsp;<i class="fa fa-clock-o"></i></p></div></a><hr style="padding:0; margin:0"/></li>');


                    } else {

                        $("#alertArea").append('<li id="alertNotifications" style="background-color:#F9F9H9"><a href="/Profile/MasterProfile/ChangeNotificationStatus?dashboardNotificationID=' + myData[msg].UserDashboardNotificationID + '"><div><p>' + myData[msg].ActionPerformed + ' - ' + myData[msg].ActionPerformedByUser + '&nbsp;<i class="fa fa-clock-o"></i></p></div></a><hr style="padding:0; margin:0"/></li>');

                    }

                };

            }
        }
        else {
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
var id = 2;

//sessionStorage.setItem("DataStatus", "false");
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
            if (!localStorage.hasOwnProperty('expired_Task')) {

                //if (sessionStorage.getItem("DataStatus")=="false") {
                $.ajax({
                    url: rootDir + '/Dashboard/GetTaskExpiryCounts?cdUserID=' + cduserdata.cdUser.CDUserID,
                    //data: {
                    //    format:  'json'
                    //},
                    error: function () {
                        //$('#info').html('<p>An error has occurred</p>');
                    },
                    dataType: 'json',
                    success: function (data) {
                        var expired = data.Result.ExpiredCount;
                        var expiringToday = data.Result.ExpiringTodayCount;
                        ExpiredTaskCount = data.Result.ExpiredCount;
                        ExpiringTaskCount = data.Result.ExpiringTodayCount;
                        localStorage.setItem("UserID", cduserdata.cdUser.CDUserID);
                        localStorage.setItem("expired_Task", expired);
                        localStorage.setItem("expiring_Task", expiringToday);
                        localStorage.setItem("DataStatus", "true");
                        $("#expired_Task").append(expired);
                        $("#expiring_Task").append(expiringToday);
                    },
                })
            }
            else {
                $("#expired_Task").append(localStorage.getItem("expired_Task"));
                $("#expiring_Task").append(localStorage.getItem("expiring_Task"));
            }
            var cnt = 0;
            if (myData.length == 0) {
                $("#alertArea").append('<li id="alertNotifications"><div><p class="text-center">No New Notifications Available.</p></div></a><hr style="padding:0; margin:0"/></li>');
            }
            else {
                $("#alertArea").append('');
                for (msg in myData) {


                    if (data.cdUser.DashboardNotifications[msg].AcknowledgementStatus == "Unread") {
                        cnt++;
                        if (data.cdUser.DashboardNotifications[msg].Action == 'Daily task') {
                            $("#alertArea").append('<li id="alertNotifications" style="background-color:#F1F1F1"><a href="/Profile/MasterProfile/ChangeNotificationStatus?dashboardNotificationID=' + myData[msg].UserDashboardNotificationID + '"><div><p>' + myData[msg].ActionPerformed + ' - ' + myData[msg].ActionPerformedByUser + '&nbsp;<i class="fa fa-clock-o"></i></p></div></a><hr style="padding:0; margin:0"/></li>');
                        }
                            //else if (data.cdUser.DashboardNotifications[msg].Action == 'Task Expired')
                            //{
                            //    $("#alertArea").append('<li id="alertNotifications" style="background-color:#F1F1F1"><a href="/Profile/MasterProfile/ChangeNotificationStatus?dashboardNotificationID=' + myData[msg].UserDashboardNotificationID + '"><div><p>' + myData[msg].ActionPerformed  + '&nbsp;<i class="fa fa-clock-o"></i></p></div></a><hr style="padding:0; margin:0"/></li>');

                            //}
                        else {
                            $("#alertArea").append('<li id="alertNotifications" style="background-color:#F1F1F1"><a href="/Profile/MasterProfile/ChangeNotificationStatus?dashboardNotificationID=' + myData[msg].UserDashboardNotificationID + '"><div><p>' + myData[msg].ActionPerformed + ' - ' + myData[msg].ActionPerformedByUser + '&nbsp;<i class="fa fa-clock-o"></i></p></div></a><hr style="padding:0; margin:0"/></li>');
                        }
                        //$("#alertArea").append('<li id="alertNotifications"><a href="' + myData[msg].RedirectURL + '"><div><p>' + myData[msg].ActionPerformed + ' - ' + myData[msg].ActionPerformedByUser + '<i class="fa fa-bell fa-fw"></i></p></div></a><hr style="padding:0; margin:0"/></li>');
                    }

                };
                if (cnt == 0) {
                    $("#alertArea").append('<li id="alertNotifications"><div><p class="text-center">No New Notifications Available.</p></div></a><hr style="padding:0; margin:0"/></li>');

                } else {
                    $("#numberOfNotifications").text(cnt);

                }
            }


            //$("#alertArea").append('<li class="divider"></li><li><a class="text-center" href="#"><strong>See All Alerts</strong><i class="fa fa-angle-right"></i></a></li>');
            //var $title = $('<h1>').text(data.talks[0].talk_title);
            //var $description = $('<p>').text(data.talks[0].talk_description);
            //$('#info')
            //   .append($title)
            //   .append($description);
        },
        type: 'GET'
    });
}

//-----------------Function for saving the notification status-------------Dv--

//window.onbeforeunload = function () {
//    localStorage.clear();
//    return '';
//};

var TaskCount = 0;
var TaskData;
var ExpiredTask = [];
var EndingTasks = [];
var ImportantTask = [];
var Allproviders;
var del, flag, read, pin;
var NotifyData = [];

//-----------------New Implementation Getting notification-----------Dv
var TaskExpiredCount = 0;
var TaskEndingTodayCount = 0;
//var newGetTaskNotification = function () {
//    $.ajax({
//        url: rootDir + '/TaskTracker/GetAllTasksByUserId',
//        data: {
//            format: 'json'
//        },
//        error: function () {
//            //$('#info').html('<p>An error has occurred</p>');
//        },
//        dataType: 'json',
//        success: function (data) {
//            console.log(data);
//            TaskDataNew = data;
//            for (task in TaskDataNew) {
//                TaskDataNew[task].NewDate = ConvertdateFormat(TaskDataNew[task].NextFollowUpDate);
//                TaskDataNew[task].DayLeft = getDaysLeft(TaskDataNew[task].NewDate);
//                if (TaskDataNew[task].DayLeft < 0) {
//                    TaskExpiredCount++;
//                }
//                if (TaskDataNew[task].DayLeft == 0) {
//                    TaskEndingTodayCount++;
//                }
//            }
//            $("#expired_Task").append(TaskExpiredCount);
//            $("#expiring_Task").append(TaskEndingTodayCount);
//        },
//        type: 'GET'
//    });
//};


var callServer = function (data) {
    console.log("Inside Call Server");
    var newAjax = true;

    if (!newAjax) {
        //do processing
        newAjax = false;
    }
};
var DelClicked = function (data, This) {
    data.notificationStatus.del = !data.notificationStatus.del;
    $(This).parent().parent().parent().parent().remove();
    $(This).toggleClass("readIcon");
    callServer(data);
};
var FlagClicked = function (data, This) {
    data.notificationStatus.flag = !data.notificationStatus.flag;
    $(This).toggleClass("readIcon");
    callServer(data);
};
var PinClicked = function (data, This) {
    data.notificationStatus.pin = !data.notificationStatus.pin;
    $(This).toggleClass("readIcon");
    callServer(data);
};
var ReadClicked = function (data, This) {
    data.notificationStatus.read = !data.notificationStatus.read;
    $(This).parent().parent().parent().toggleClass("pinnedTask");
    $(This).toggleClass("readIcon");

    callServer(data);
};
var navigateToTask = function (data, This) {
    data.notificationStatus.read = !data.notificationStatus.read;
    $(This).parent().parent().removeClass("pinnedTask");
    window.location.href = "/TaskTracker/Index";
};
var pinnedTask = function (data) {
    var pintask = [];
    var totalTask = [];
    for (var i in data) {
        if (data[i].notificationStatus.pin) {
            pintask.push(data[i]);
        }
        else {
            totalTask.push(data[i]);
        }
    }
    for (var j in totalTask) {
        pintask.push(totalTask[j]);
    }
    return pintask;

};
var parseDate = function (input) {
    var parts = input.match(/(\d+)/g);
    // new Date(year, month [, date [, hours[, minutes[, seconds[, ms]]]]])
    return new Date(parts[0], parts[1] - 1, parts[2]); // months are 0-based
}
var getDaysLeft = function (datevalue) {
    // parse a date in yyyy-mm-dd format
    datevalue = parseDate(datevalue)
    if (datevalue) {
        var oneDay = 24 * 60 * 60 * 1000; // hours*minutes*seconds*milliseconds

        var currentdate = new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate());

        var secondDate = new Date(2008, 01, 22);

        return Math.round((datevalue.getTime() - currentdate.getTime()) / (oneDay));
    }
    return null;
};
var ConvertdateFormat = function (value) {
    var today = new Date(value);
    var dd = today.getDate();
    var mm = today.getMonth() + 1;
    var yyyy = today.getFullYear();
    if (dd < 10) { dd = '0' + dd }
    if (mm < 10) { mm = '0' + mm }
    var today = yyyy + '-' + mm + '-' + dd;
    return today;
};
var Expired_Ending_Tasks = function (id) {
    $("#NoData").hide();
    if (id == "Expired") {
        $("#ExpiredTask").addClass("active");
        $("#EndingSoon").removeClass("active");
        $("#AllNotification").addClass("active");
        $("#ImpNotification").removeClass("active");
        $("#EndingNotificationArea").hide();
        $("#ExpiredNotificationArea").show();
        $("#ImportantNotificationArea").hide();
        $("#Ending_Expired_Tab").removeClass("hide");
        if (ExpiredTask.length == 0) {
            $("#NoData").show();
        }
    }
    else if (id == "Ending") {
        $("#ExpiredTask").removeClass("active");
        $("#EndingSoon").addClass("active");
        $("#AllNotification").addClass("active");
        $("#ImpNotification").removeClass("active");
        $("#EndingNotificationArea").show();
        $("#ExpiredNotificationArea").hide();
        $("#ImportantNotificationArea").hide();
        if (EndingTasks.length == 0) {
            $("#NoData").show();
        }
    }
    else if (id == "Imp") {
        $("#AllNotification").removeClass("active");
        $("#ImpNotification").addClass("active");
        $("#EndingNotificationArea").hide();
        $("#ExpiredNotificationArea").hide();
        $("#ImportantNotificationArea").show();
        $("#Ending_Expired_Tab").addClass("hide");
        if (ImportantTask.length == 0) {
            $("#NoData").show();
        }

    }
};
var getdifferentObjects = function (data) {
    var date = new Date($.now());
    var dateString = (date.getFullYear() + '-'
    + ('0' + (date.getMonth() + 1)).slice(-2)
    + '-' + ('0' + (date.getDate())).slice(-2));
    for (var i in data) {
        var x = data[i].NextFollowUpDate.split('T');
        var y = x[0];

        if (data[i].notificationStatus.flag) {
            ImportantTask.push(data[i]);
        }
        if (data[i].DayLeft <= 0) {
            TaskCount++;
            ExpiredTask.push(data[i]);
        }
        if (data[i].DayLeft > 0 && data[i].DayLeft <= 30) {
            EndingTasks.push(data[i]);
        }
    }
}
var createLiItem = function (data, id) {
    var Id = '#' + id + "NotificationArea";
    data = pinnedTask(data);
    for (var i in data) {
        if (!data[i].notificationStatus.del) {
            NotifyData = data[i];
            $(Id).append(
                '<li  id ="note' + data[i].TaskTrackerId + '">' +
                   '<div   class="innerDrop TrackerItem pinnedTask">' +
                   '     <span> ' +
                   '         &nbsp;<i class="fa fa-hourglass"></i>&nbsp;<span onclick="navigateToTask(NotifyData,this)">' + data[i].ProviderName + '</span>' +
                   '         <span style="float:right;">' +
                   '              <i id="Del" onclick="DelClicked(NotifyData,this)" class="fa fa-trash icon activeShow" data-toggle="tooltip" data-placement="bottom" title="Delete"></i>&nbsp;' +
                   '              <i id="Flag" onclick="FlagClicked(NotifyData,this)" class="fa fa-flag icon activeShow" data-toggle="tooltip" data-placement="bottom" title="Mark as Important"></i>&nbsp;' +
                   '              <i id="Read" onclick="ReadClicked(NotifyData,this)" class="fa fa-folder icon activeShow" data-toggle="tooltip" data-placement="bottom" title="Unread"></i>&nbsp;' +
                   '              <i id="Pin" onclick="PinClicked(NotifyData,this)" class="fa fa-thumb-tack icon activeShow" data-toggle="tooltip" data-placement="bottom" title="Pin this task"></i>' +
                   '              &nbsp;' +
                   '        </span><br />' +
                   '        <span class="small">' + data[i].Subject + '</span>' +
                   '     </span>' +
                   '</div>' +
                   '</li>'
            );
        }

    }
};
var callingfunc = function (id) {
    if (id == "Imp") {
        createLiItem(ImportantTask, "Important");
    }
    else {
        if (id == "Expired") {
            createLiItem(ExpiredTask, "Expired");
        }
        else
            createLiItem(EndingTasks, "Ending");
    }
    Expired_Ending_Tasks(id);

}
var Tasknotify = function () {
    $.ajax({
        url: rootDir + '/TaskTracker/GetAllTasksByUserId',
        data: {
            format: 'json'
        },
        error: function () {
            //$('#info').html('<p>An error has occurred</p>');
        },
        dataType: 'json',
        success: function (data) {
            TaskData = data;
            for (var task in TaskData) {
                TaskData[task].notificationStatus = {
                    del: false,
                    pin: false,
                    read: false,
                    flag: false
                };
            }
            $.ajax({
                url: rootDir + '/TaskTracker/GetAllProviders',
                data: {
                    format: 'json'
                },
                error: function () {
                    //$('#info').html('<p>An error has occurred</p>');
                },
                dataType: 'json',
                success: function (data) {
                    Allproviders = data;
                    for (task in TaskData) {
                        //TaskCount++;
                        if (TaskData[task].TaskTrackerId == 4) {
                            TaskData[task].notificationStatus.pin = true;
                            TaskData[task].notificationStatus.flag = true;
                        }
                        TaskData[task].NewDate = ConvertdateFormat(TaskData[task].NextFollowUpDate);
                        TaskData[task].DayLeft = getDaysLeft(TaskData[task].NewDate);
                        //-----------getting the providers name---------
                        for (var pro in Allproviders) {
                            if (TaskData[task].ProfileID == Allproviders[pro].ProfileId) {
                                var temp = Allproviders[pro].Name;
                                temp = temp.split('-');
                                TaskData[task].ProviderName = temp[0];

                            }
                        }
                    }
                    getdifferentObjects(TaskData);
                    callingfunc("Imp");
                    callingfunc("Ending");
                    callingfunc("Expired");
                    if (TaskCount == TaskCount) {
                        $("#count").append(
                         '<span class="badge badge-notify">' + TaskCount + '</span>'
                         );
                    }
                    $(".TrackerItem").find(".activeShow").addClass("hide");
                    $(".TrackerItem").hover(function () {
                        $(this).find(".activeShow").removeClass("hide");
                    }, function () {
                        $(this).find(".activeShow").addClass("hide");
                    });

                },
                type: 'GET'
            });
        },
        type: 'GET'
    });

};

var TaskNotificationReminder = function () {
    $.ajax({
        url: rootDir + '/Prototypes/GetReminderNotification',
        type: "GET",
        dataType: "html",
        success: function (response) {

            var taskStored = JSON.parse(localStorage.getItem("TaskReminders"));

            var HTMLString = '';
            if (taskStored != null) {

                if (taskStored.taskCount != 0) {
                    for (var i in taskStored.tasks) {
                        if (i % 2 == 0) {
                            HTMLString += '<div class="col-lg-5 col-xs-5" style="background-color:#e8e8e8"><b>' + taskStored.tasks[i].Subject + '</b></div>' +
                            '<div class="col-lg-7 col-xs-7" style="background-color:#e8e8e8"> Dr. ' + taskStored.tasks[i].ProviderName + '</div>'

                        }
                        else {
                            HTMLString += '<div class="col-lg-5 col-xs-5"><b>' + taskStored.tasks[i].Subject + '</b></div>' +
                           '<div class="col-lg-7 col-xs-7"> Dr. ' + taskStored.tasks[i].ProviderName + '</div>'
                        }
                    }

                    // var timeBlinkString = '<div class="circle blink_me"><b>' + taskStored.remainingTime + '</b><br /><b></b></div>';

                    var reminderHead = '<b class="pull-left" style="font-size:large">🔔</b>' +
                    '<div class="col-lg-6 col-xs-6"> <b style="font-size: large;">' + taskStored.reminderDate + '</b> at 5:15PM </div>' +
                    '<div class="col-lg-5 col-xs-5 pull-right"><span class="badge" style="background-color: #ccddff;color:black">' + taskStored.taskCount + '-Task(s)</span> Reminder </div>';




                    $('body').append(response);
                    $('#TaskList').html(HTMLString);
                    //$('#timeBlinker').html(timeBlinkString);
                    $('#reminderHeader').html(reminderHead);



                    // Update the count down every 1 second
                    var x = setInterval(function () {

                        // Get todays date and time
                        var now = new Date().getTime();

                        // Find the distance between now an the count down date
                        var distance = new Date(taskStored.reminderDateTime) - now;

                        // Time calculations for days, hours, minutes and seconds
                        var days = Math.floor(distance / (1000 * 60 * 60 * 24));
                        var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
                        var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
                        var seconds = Math.floor((distance % (1000 * 60)) / 1000);

                        // Display the result and append to html"
                        var timeBlinkString = '<div class="circle "><b class="blink_me">' + (days > 0 ? days + 'd ' : '') + (hours > 0 ? hours + 'h ' : '') + minutes + 'm  ' + seconds + 's' + '</b><br /><b></b></div>';
                        $('#timeBlinker').html(timeBlinkString);

                        // If the count down is finished,
                        if (distance < 0) {
                            clearInterval(x);
                            
                        }
                    }, 1000);

                }
            }
        },
        error: function (error) {
            alert("Sorry, there was a problem!");
        }
    });
}

$(document).ready(function () {

    //$(document).click(function () {
    //    $(".RemainderBody").addClass('show');

    //});
    var StaticVariable = new Date();
    TaskNotificationReminder();

})


