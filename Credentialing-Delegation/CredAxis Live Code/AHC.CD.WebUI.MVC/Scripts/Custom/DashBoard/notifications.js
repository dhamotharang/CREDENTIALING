
$(document).ready(function () {
    var cnd = $.connection.cnDHub;

    //cnd.client.logoutCall = function (userSessionId) {

    //};

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
            //if (!localStorage.hasOwnProperty('expired_Task')) {

            //    //if (sessionStorage.getItem("DataStatus")=="false") {
            //    $.ajax({
            //        url: rootDir + '/Dashboard/GetTaskExpiryCounts?cdUserID=' + cduserdata.cdUser.CDUserID,
            //        //data: {
            //        //    format:  'json'
            //        //},
            //        error: function () {
            //            //$('#info').html('<p>An error has occurred</p>');
            //        },
            //        dataType: 'json',
            //        success: function (data) {
            //            var expired = data.Result.ExpiredCount;
            //            var expiringToday = data.Result.ExpiringTodayCount;
            //            ExpiredTaskCount = data.Result.ExpiredCount;
            //            ExpiringTaskCount = data.Result.ExpiringTodayCount;
            //            localStorage.setItem("UserID", cduserdata.cdUser.CDUserID);
            //            localStorage.setItem("expired_Task", expired);
            //            localStorage.setItem("expiring_Task", expiringToday);
            //            localStorage.setItem("DataStatus", "true");
            //            $("#expired_Task").append(expired);
            //            $("#expiring_Task").append(expiringToday);
            //        },
            //    })
            //}
            //else {
            //    $("#expired_Task").append(localStorage.getItem("expired_Task"));
            //    $("#expiring_Task").append(localStorage.getItem("expiring_Task"));
            //}
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

var tempJsonData = {
    "tasks": [
        {
            "TaskTrackerId": 2,
            "ProfileID": 1,
            "ProviderName": "Dv Sahu-45454545454",
            "SubSectionName": "Sub section 01",
            "Subject": "get plan details",
            "NextFollowUpDate": "05/23/2017",
            "ModeOfFollowUp": [
                {
                    "Name": "Email",
                    "Type": "Email",
                    "$$hashKey": "object:34"
                },
                {
                    "Name": "Phone Call",
                    "Type": "PhoneCall",
                    "$$hashKey": "object:35"
                }
            ],
            "FollowUp": "",
            "InsuranceCompanyName": "",
            "PlanName": "health plan",
            "AssignedToId": "91b92cfc-478a-4138-9739-42f1845547f8",
            "AssignedTo": "mani@ahcpllc.com",
            "HospitalID": 2,
            "Hospital": "All Children's Hospital",
            "Notes": "5/23/2017~1:07 PM~jmartin@accesshealthcarellc.net~ If you are allowing it then don't complain about it. You deserve what you have settled for...",
            "ModifiedDate": "2017-05-23T13:09:35.70406",
            "TaskTrackerHistories": [

            ],
            "AssignedBy": {
                "CDRoles": [
                    {
                        "CDUserRoleID": 1,
                        "CDUserId": 2,
                        "CDRoleId": 1,
                        "CDRole": null,
                        "LastModifiedDate": "2017-05-22T14:51:47.6546746"
                    }
                ],
                "Profile": null,
                "UserRelation": null,
                "CDUserID": 2,
                "AuthenicateUserId": "46e66bd0-491b-4c89-8b27-138b97064e7c",
                "ProfileId": null,
                "EmailId": null,
                "DashboardNotifications": null,
                "Status": "Active",
                "StatusType": 1,
                "LastModifiedDate": "2017-05-22T14:51:45.7511437"
            },
            "AssignedById": 2,
            "TabID": "",
            "SubSectionID": "",
            "CompleteStatus": "OPEN",
            "LastUpdatedBy": "jmartin@accesshealthcarellc.net",
            "daysleft": -1,
            "$$hashKey": "object:20",
            "checked": true
        },
        {
            "TaskTrackerId": 1002,
            "ProfileID": 1,
            "ProviderName": "Dv Sahu-45454545454",
            "SubSectionName": "Sub section 01",
            "Subject": "asdsadasdad",
            "NextFollowUpDate": "05/24/2017",
            "ModeOfFollowUp": [
                {
                    "Name": "Phone Call",
                    "Type": "PhoneCall",
                    "$$hashKey": "object:48"
                }
            ],
            "FollowUp": "",
            "InsuranceCompanyName": "",
            "PlanName": "A plan for good life",
            "AssignedToId": "46e66bd0-491b-4c89-8b27-138b97064e7c",
            "AssignedTo": "jmartin@accesshealthcarellc.net",
            "HospitalID": 2,
            "Hospital": "All Children's Hospital",
            "Notes": "5/24/2017~3:04 PM~dv@asia.com~ asgf werfwerw",
            "ModifiedDate": "2017-05-24T15:04:22.2538006",
            "TaskTrackerHistories": [

            ],
            "AssignedBy": null,
            "AssignedById": 14,
            "TabID": "",
            "SubSectionID": "",
            "CompleteStatus": "OPEN",
            "LastUpdatedBy": "dv@asia.com",
            "daysleft": 0,
            "$$hashKey": "object:21",
            "checked": true
        }
    ],
    "remainingTime": "",
    "reminderDate": "",
    "reminderDateTime": "2017-06-22T20:30:00.000Z",
    "taskCount": 2
}

var SelectedTaskID = null;
var reminderInterval = null;
var SelectedTaskObject = null;
var view = null;
var TaskCountToShow = 0;
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
        //var TimeToshow = new Date(new Date().getTime() + new Date(mins * 60 * 1000));
        //var duedate = new Date(mins * 60 * 1000);
        //var days = Math.floor(mins / (1000 * 60 * 60 * 24));
        //var hours = Math.floor((mins % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
        //var minutes = Math.floor((mins % (1000 * 60 * 60)) / (1000 * 60));

        //var retstr =( days > 0 ? (days + 'days(s)') : '' )+( hours > 0 ? (hours + 'hour(s)') : '') + (minutes > 0 ? (minutes + 'minute(s) overdue') :'');
        //return retstr;
        if (0 < mins && mins < 60) {
            return Math.round(mins) + ' minutes overdue';
        }
        else if (60 < mins && mins < 1440) {
            return (Math.round(mins / 60) + ' hours ' + Math.round(mins % 60) + ' minutes overdue');
        }
        else if (1440 < mins) {
            //return (Math.round(mins / (60 * 24)) + ' day(s) ' + Math.round(mins / 60) + ' hours ' + Math.round(mins % 60) + ' minutes overdue');
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

    var output = dt.getHours() + ":" + dt.getMinutes() + ":" + dt.getSeconds();

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
        '<div style="line-height: 0.3;"><b style="font-size: 12px;font-weight:500;color: #337ab7;">' + TaskDate + '-' + TaskTime+'</b></div>';
    return remheader;

    //var reminderHead = '<b class="pull-left" style="font-size:large"> </b>' +
   //     '<div class="col-g-1"><b><i class="fa fa-calendar fa-2x"></i></b></div><div class="col-lg-8 col-xs-6"> <b style="font-size: large;">' + TaskDate + '</b> at ' + TaskTime + ' </div>'+
    //    '<div class="col-lg-5 col-xs-5 pull-right"><span class="badge" style="background-color: #ccddff;color:black">' + TaskCountToShow + '-Task(s)</span> Reminder </div>';
    //return reminderHead;
       
};
var getellSub = function(sub){
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
                if (minsToRemind <= 30) {
                    var TaskMsg = ShowMsgForReminder(minsToRemind);
                    TaskCountToShow += 1;
                    if (TaskCount == 1) {
                        $('#ReminderFullBody').remove();
                        $('body').append(view);
                        TaskCount += 1;
                    }

                    var reminderHead = UpperHeader(taskStored[i].ScheduledDateTime, taskStored[i]);
                    $('#Rem_header_bar').html(reminderHead);
                   // var sbjct = getellSub(taskStored[i].ReminderInfo.Subject);
                   // var pname = getellpname(taskStored[i].ReminderInfo.ProviderName);
                   // HTMLString += '<div id="taskelement_' + i + '" onclick="AssignselectedItem(' + taskStored[i].TaskReminderID + ',taskelement_' + i + ')">' +
                    //    '<div data-toggle="popover" data-trigger="hover" data-content="' + taskStored[i].ReminderInfo.Subject +'" class="col-lg-3 col-xs-3 Padding_5_px SelectedTaskStyle_' + i + '" ><b>' + sbjct + '</b></div>' +
                   //     '<div data-toggle="popover" data-trigger="hover" data-content="' + taskStored[i].ReminderInfo.ProviderName + '" class="col-lg-4 col-xs-4 Padding_5_px SelectedTaskStyle_' + i + '"  >' + pname + '</div>' +
                   //     '<div class="col-lg-5 col-xs-5 Padding_5_px SelectedTaskStyle_' + i + '" >' + TaskMsg + ' </div>' +
                   //     '</div>'
                    HTMLString += '<div class="row" id="taskelement_' + i + '" onclick="AssignselectedItem(' + taskStored[i].TaskReminderID + ',taskelement_' + i + ')">' +
                    '<div class="col-lg-6"><i class="fa fa-calendar"></i>&nbsp; ' + taskStored[i].ReminderInfo.ProviderName + '</div>' +
                        ' <div class="col-lg-6">' + TaskMsg + '</div>' +
                        '</div>';
                    $('#TaskList').html(HTMLString);
                }
            }
            $('#TaskList').find('div:first').click();
           // $('[data-toggle="popover"]').popover(); 
        }
    }
};

var AssignselectedItem = function (taskID, id) {
    SelectedTaskID = taskID;
    //  SelectedTaskObject = taskobj;
    var TaskData = JSON.parse(localStorage.getItem("TaskReminders"));
    //var TaskIDList = [];
    $.each(TaskData, function (key, value) {
        if (value.TaskReminderID == SelectedTaskID) {
            SelectedTaskObject = value;
        }
    })
    StoredTime = SelectedTaskObject.ScheduledDateTime.replace("/Date(", '').replace(")/", '');
    var reminderHeader = UpperHeader(StoredTime, SelectedTaskObject);
    $('#Rem_header_bar').html(reminderHeader);
    $('#ReminderFullBody').find('.SelectedTaskStyle').removeClass('SelectedTaskStyle');
    // $('#TaskList').find('div:first div').addClass('SelectedTaskStyle');
    $(id).addClass('SelectedTaskStyle');
    $(id).find('div').addClass('SelectedTaskStyle');
};

var TaskNotificationReminder = function () {
    $.ajax({
        //url: rootDir + '/Prototypes/GetReminderNotification',
        url: rootDir + '/TaskTracker/GetReminders',
        type: "GET",
        success: function (response) {
            //var taskStored = JSON.parse(localStorage.getItem("TaskReminders"));
            var taskStored = response.reminders;
            localStorage.setItem("TaskReminders", JSON.stringify(response.reminders)); // setting the reminder objects into local storage
            view = response.responseView;
            getReminderView(taskStored);

            reminderInterval = setInterval(function () { CheckReminder() }, 240000); // calling function repeatedly to check reminder
        },
        error: function (error) {
            //alert("Sorry, there is some problem!");
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
    taskObj.ScheduledDateTime = taskObj.ScheduledDateTime.replace("/Date(", '').replace(")/", '');
    var snoozeTime = $('#snoozeInterval').val();
    var date = new Date(parseInt(taskObj.ScheduledDateTime) + parseInt(snoozeTime));
    var dataToShow = date.getUTCFullYear() + '/' + (date.getMonth()+1) + '/' + date.getDate() + ' ' + date.getHours() + ':' + date.getMinutes() + ':' + date.getSeconds();
    $.ajax({
        url: rootDir + '/TaskTracker/RescheduleReminder',
        data: JSON.stringify({ 'taskID': taskObj.TaskReminderID, 'scheduledDateTime': dataToShow }),
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
            //alert("Sorry, there is some problem!");
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

                    }
                })
                //localStorage.setItem("TaskReminders", JSON.stringify(TaskData));
            }
        },
        error: function (error) {
            //alert("Sorry, there is some problem!");
        }
    });

};
$(document).ready(function () {

    //$(document).click(function () {
    //    $(".RemainderBody").addClass('show');

    //});
    var StaticVariable = new Date();
    TaskNotificationReminder();


})


