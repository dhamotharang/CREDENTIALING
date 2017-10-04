$(document).ready(function () {
    var opened = false;
    var myData;
    var cduserdata;
    var id = 2;
    var TaskCount = 0;
    var TaskData;
    var ExpiredTask = [];
    var EndingTasks = [];
    var ImportantTask = [];
    var Allproviders;
    var del, flag, read, pin;
    var NotifyData = [];
    var TaskExpiredCount = 0;
    var TaskEndingTodayCount = 0;
    var SelectedTaskID = null;
    var reminderInterval = null;
    var SelectedTaskObject = null;
    var view = null;
    var TaskCountToShow = 0;
    var cnd = $.connection.cnDHub;
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
    //Tasknotify();
});
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
var Notify = function () {
    $.ajax({
        url: rootDir + '/Dashboard/GetMyNotification',
        data: {
            format: 'json'
        },
        error: function () {
        },
        dataType: 'json',
        success: function (data) {
            cduserdata = data;
            myData = data.cdUser.DashboardNotifications;
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
                        else {
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
var Notify1 = function () {
    $.ajax({
        url: rootDir + '/Tasks/GetDashBoardNotificationsAsync',
        data: {
            format: 'json'
        },
        error: function () {
        },
        dataType: 'json',
        success: function (data) {
            data.User.DashboardNotifications = data.DashboardNotifications;
            cduserdata = data.User;
            myData = data.User.DashboardNotifications;
            skipCountforNotifications += 20;
            BuildNotifications(data.UnreadNotificationsCount);
        },
        type: 'GET'
    });
};
var skipCountforNotifications = 0;
var LoadMoreNotifications = function () {
    $.ajax({
        url: rootDir + '/Tasks/LoadMoreNotificationsAsync?skipRecords=' + skipCountforNotifications,
        data: {
            format: 'json'
        },
        error: function () {
        },
        dataType: 'json',
        success: function (data) {
            Array.prototype.push.apply(myData, data.DashboardNotifications);
            skipCountforNotifications += 20;
            BuildNotifications(data.UnreadNotificationsCount);
        },
        type: 'GET'
    });
};
var BuildNotifications = function (UnreadNotificationsCount) {
    if (myData.length == 0) {
        $("#alertArea").append('<li id="alertNotifications"><div><p class="text-center">No New Notifications Available.</p></div></a><hr style="padding:0; margin:0"/></li>');
    }
    else {
        $("#alertArea").html('');
        $("#alertArea").append('<li class="pull-right" style="background-color:#ddd; position:fixed; top:361px;"><button class="btn btn-sm" onclick="LoadMoreNotifications();" style="width:290px;background-color: lightgrey;color: black;">Load More</button>');
        for (var i = 0; i < myData.length; i++) {
            var str = i == myData.length - 1 ? "margin-bottom: 15px;" : "";
            $("#alertArea").append('<li id="alertNotifications" style="background-color:#F1F1F1;' + str + '"><a href="/Profile/MasterProfile/ChangeNotificationStatus?dashboardNotificationID=' + myData[i].UserDashboardNotificationID + '"><div><p>' + myData[i].ActionPerformed + ' - ' + myData[i].ActionPerformedByUser + '&nbsp;<i class="fa fa-clock-o"></i></p></div></a><hr style="padding:0; margin:0"/></li>');
        };
        if (UnreadNotificationsCount != 0) {
            $("#numberOfNotifications").text(UnreadNotificationsCount);
        }
    }
};
var callServer = function (data) {
    console.log("Inside Call Server");
    var newAjax = true;
    if (!newAjax) {
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
var checkReminderTime = function (time) {
    time = parseInt(time);
    var now = new Date().getTime();
    var millisecondsPerMinute = 1000 * 60;
    var millisBetween = time - now;
    var minutes = millisBetween / millisecondsPerMinute;
    return minutes;
}
var ShowMsgForReminder = function (mins) {
    if (mins > 0) {
        return Math.round(mins) + ' minutes';
    }
    else {
        mins = Math.abs(mins);
        if (0 < mins && mins < 60) {
            return Math.round(mins) + ' minutes overdue';
        }
        else if (60 < mins && mins < 1440) {
            return (Math.round(mins / 60) + ' hours ' + Math.round(mins % 60) + ' minutes overdue');
        }
        else if (1440 < mins) {
            return (Math.round(mins / (60 * 24)) + ' day(s) ' + Math.round(mins / 60) + ' hours overdue');
        }
    }
};
var GetTheTaskDate = function (DateTime) {
    var d = new Date(parseInt(DateTime));
    var month = d.getMonth() + 1;
    var day = d.getDate();
    var output = (('' + month).length < 2 ? '0' : '') + month + '/' +
        (('' + day).length < 2 ? '0' : '') + day + '/' +
        d.getFullYear();
    return output;
};
var GetTheTaskTime = function (DateTime) {
    var dt = new Date(parseInt(DateTime));
    var hours = dt.getHours();
    if (hours > 12) {
        hours = hours - 12;
    }
    if (hours == 0) {
        hours = 12;
    }
    var minutes = dt.getMinutes();
    if (minutes < 10) {
        minutes = 0 + "" + minutes;
    }
    var output = hours + ":" + minutes + (dt.getHours() >= 12 ? ' PM' : ' AM');
    return output;
};
var UpperHeader = function (DateTime, task) {
    var Rem_CountToShow = TaskCountToShow + ' Reminder(s)';
    $('#Rem_count').html(Rem_CountToShow);
    if (task.ReminderInfo.Subject == undefined) {
        task.ReminderInfo = JSON.parse(task.ReminderInfo);
    }
    var TaskDate = GetTheTaskDate(DateTime);
    var TaskTime = GetTheTaskTime(DateTime);
    var remheader = '<div><b style="font-size: 17px;">' + task.ReminderInfo.Subject + '</b></div>' +
        '<div style="line-height: 0.9;"><b style="font-size: 12px;font-weight:500;color: #337ab7;">' + TaskDate + '-' + TaskTime + '</b></div>';
    return remheader;
};
var getellSub = function (sub) {
    var ret = sub.substring(0, 15) + '...';
    if (sub.length >= 16) {
        return ret;
    }
    else {
        return sub;
    }
}
var getellpname = function (pname) {
    var ret = pname.substring(0, 20) + '...';
    if (pname.length >= 20) {
        return ret;
    }
    else {
        return pname;
    }
}
var getReminderView = function (taskStored) {
    var TaskCount = 1;
    TaskCountToShow = 0;
    var HTMLString = ''; //html strign declaration
    if (taskStored != null) {
        if (taskStored.length != 0) {
            for (var i in taskStored) {
                taskStored[i].ReminderInfo = JSON.parse(taskStored[i].ReminderInfo);
                taskStored[i].ScheduledDateTime = taskStored[i].ScheduledDateTime.replace("/Date(", '').replace(")/", '');
                var minsToRemind = checkReminderTime(taskStored[i].ScheduledDateTime);
                if (minsToRemind <= 0) {
                    var TaskMsg = ShowMsgForReminder(minsToRemind);
                    TaskCountToShow += 1;
                    if (TaskCount == 1) {
                        $('#ReminderFullBody').remove();
                        $('body').append(view);
                        TaskCount += 1;
                    }
                    var reminderHead = UpperHeader(taskStored[i].ScheduledDateTime, taskStored[i]);
                    $('#Rem_header_bar').html(reminderHead);
                    HTMLString += '<div class="row" id="taskelement_' + i + '" onclick="AssignselectedItem(' + taskStored[i].TaskReminderID + ',taskelement_' + i + ')">' +
                        '<div class="col-lg-6"><i class="fa fa-calendar"></i>&nbsp; ' + taskStored[i].ReminderInfo.ProviderName + '</div>' +
                        ' <div class="col-lg-6">' + TaskMsg + '</div>' +
                        '</div>';
                    $('#TaskList').html(HTMLString);
                }
            }
            $('#TaskList').find('div:first').click();
        }
    }
};
var AssignselectedItem = function (taskID, id) {
    SelectedTaskID = taskID;
    var TaskData = JSON.parse(localStorage.getItem("TaskReminders"));
    $.each(TaskData, function (key, value) {
        if (value.TaskReminderID == SelectedTaskID) {
            SelectedTaskObject = value;
        }
    })
    StoredTime = SelectedTaskObject.ScheduledDateTime.replace("/Date(", '').replace(")/", '');
    var reminderHeader = UpperHeader(StoredTime, SelectedTaskObject);
    $('#Rem_header_bar').html(reminderHeader);
    $('#ReminderFullBody').find('.SelectedTaskStyle').removeClass('SelectedTaskStyle');
    $(id).addClass('SelectedTaskStyle');
    $(id).find('div').addClass('SelectedTaskStyle');
};
var TaskNotificationReminder = function () {
    $.ajax({
        url: rootDir + '/TaskTracker/GetReminders',
        type: "GET",
        success: function (response) {
            var taskStored = response.reminders;
            localStorage.setItem("TaskReminders", JSON.stringify(response.reminders)); // setting the reminder objects into local storage
            view = response.responseView;
            getReminderView(taskStored);
            reminderInterval = setInterval(function () { CheckReminder() }, 30000); // calling function repeatedly to check reminder
        },
        error: function (error) {
        }
    });
}
var CheckReminder = function () {
    var taskStoreddata = JSON.parse(localStorage.getItem("TaskReminders"));
    if (taskStoreddata != null || taskStoreddata != []) {
        getReminderView(taskStoreddata);
    }
};
var Snoozereminder = function () {
    taskObj = SelectedTaskObject;
    var currentDate = new Date();
    var currentDateTime = currentDate.getUTCFullYear() + '/' + (currentDate.getMonth() + 1) + '/' + currentDate.getDate() + ' ' + currentDate.getHours() + ':' + currentDate.getMinutes() + ':' + currentDate.getSeconds();
    var snoozeTime = $('#snoozeInterval').val();
    var currentDateInMilliSec = Date.parse(currentDateTime);
    var date = new Date(parseInt(currentDateInMilliSec) + parseInt(snoozeTime));
    var dataToShow = date.getUTCFullYear() + '/' + (date.getMonth() + 1) + '/' + date.getDate() + ' ' + date.getHours() + ':' + date.getMinutes() + ':' + date.getSeconds();
    $.ajax({
        url: rootDir + '/TaskTracker/RescheduleReminder',
        data: JSON.stringify({ 'taskID': taskObj.TaskReminderID, 'scheduledDateTime': parseInt(snoozeTime) }),
        type: "POST",
        contentType: "application/json",
        success: function (response) {
            if (response) {
                $(".RemainderBody").addClass('show');
                $('#ReminderFullBody').remove();
                clearInterval(reminderInterval);
                TaskNotificationReminder();
            }
        },
        error: function (error) {
        }
    });
};
function DismissAllTasks() {
    var TaskData = JSON.parse(localStorage.getItem("TaskReminders"));
    var TaskIDList = [];
    $.each(TaskData, function (key, value) {
        TaskIDList.push(value.TaskReminderID);
    })
    $.ajax({
        url: rootDir + '/TaskTracker/DismissAllReminder',
        type: "POST",
        data: JSON.stringify({ 'taskIDs': TaskIDList }),
        contentType: "application/json",
        success: function (response) {
            if (response) {
                clearInterval(reminderInterval);
                TaskNotificationReminder();
                setTimeout(clearReminderIcon, 3000);
            }
        },
        error: function () { }
    });
}
function DismissSingleTask() {
    $.ajax({
        url: rootDir + '/TaskTracker/DismissReminder?taskID=' + SelectedTaskID,
        type: "POST",
        success: function (response) {
            if (response) {
                var TaskData = JSON.parse(localStorage.getItem("TaskReminders"));
                $.each(TaskData, function (key, value) {
                    if (value.TaskReminderID == SelectedTaskID) {
                        taskToDismiss = value;
                        TaskData.splice(key, 1);
                        TaskNotificationReminder();
                        setTimeout(clearReminderIcon, 3000);
                    }
                })
            }
        },
        error: function (error) {
        }
    });
};
function clearReminderIcon() {
    if ($('#maintrackerdiv').scope() != undefined) {
        var data = $('#maintrackerdiv').scope().trackerItems;
        $('#maintrackerdiv').scope().setInitialTaskData(data);
        $('#maintrackerdiv').scope().$digest();
    }
    if ($('#profiletrackermaindiv').scope() != undefined) {
        var profiledata = $('#profiletrackermaindiv').scope().trackerItems1;
        $('#profiletrackermaindiv').scope().setInitialTaskData(profiledata);
        $('#profiletrackermaindiv').scope().$digest();
    }
};
$(document).ready(function () {
    var StaticVariable = new Date();
    TaskNotificationReminder();
});