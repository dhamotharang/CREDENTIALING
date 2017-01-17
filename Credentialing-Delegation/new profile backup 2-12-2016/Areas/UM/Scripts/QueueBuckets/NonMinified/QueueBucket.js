var framerate = 50;
var frame_interval = 1000 / framerate;
var start_x = null;
var start_y = null;
var v = 96.0;
var t = 0;
var angle = 45.0;
var g = 32.0;
var projectile = null;
var i = 0;


var bucketTimer, bucketTimeout;
$('.the_queue_bucket').off('click', '#QueueBucketBtnn').on('click', '#QueueBucketBtnn', function () {
    data = {}
    url = $(this)[0].dataset.path;                      // Controller URL
    targetContainer = $(this)[0].dataset.container;    // target Container where partial page should place
    data.userQueue = $(this)[0].dataset.role;
    data.currenUserRole = $(this)[0].dataset.role;
    $.ajax({
        type: "GET",
        url: url,
        data: data,
        dataType: 'html',
        success: function (result) {
            var chartData;
            var container = $("#" + targetContainer);
            container.empty().html(result + '<div class="clearfix"></div>');
            //$('input[name="UsersData"]').val();
            $.ajax({
                type: "POST",
                url: '/UM/AuthorizationAction/GetUsersGraph',
                data: data.userQueue,
                processData: false,
                contentType: false,
                cache: false,
                success: function (chartdata) {

                    var chartData = chartdata;

                    $('.theBox').children().each(function () {
                        var targetId = $(this)[0].id;
                        if (targetId) //.includes("queueUser_")
                            generateChart(targetId, chartData[targetId]);
                    });
                }
            });

            
        },
        error: function (result) {

        }
    });

    $(".QueueBuckets").find(".queue-bucket-active-btn").removeClass("queue-bucket-active-btn").addClass("queue-bucket-btn");
    $(this).removeClass("queue-bucket-btn").addClass("queue-bucket-active-btn");
});

$('.QueueBuckets').off('click', '.theBox').on('click', '.theBox', function () {
    var $form = $("#UM_auth_form");
    var formData = new FormData($form[0]);
    url = $(this)[0].dataset.path;                      // Controller URL
    targetContainer = $(this)[0].dataset.container;    // target Container where partial page should place

    formData.append('AuthorizationStatus.ReferFromUserRole', $(this)[0].dataset.currentuserrole);
    formData.append('AuthorizationStatus.ReferFromUserName', $(this)[0].dataset.currentuser);
    formData.append('AuthorizationStatus.ReferFromUserId', "");
    formData.append('AuthorizationStatus.ReferFromUserStandardCount', "61");
    formData.append('AuthorizationStatus.ReferFromUserExpeditedCount', "35");
    formData.append('AuthorizationStatus.ReferToUserRole', $(this)[0].dataset.selecteduserrole);
    formData.append('AuthorizationStatus.ReferToUserName', $(this)[0].dataset.selecteduser);
    formData.append('AuthorizationStatus.ReferToUserId', "");
    formData.append('AuthorizationStatus.ReferToUserStandardCount', $(this)[0].dataset.standardcount);
    formData.append('AuthorizationStatus.ReferToUserExpeditedCount', $(this)[0].dataset.expeditedcount);

    var tempData = formData

    $.ajax({
        type: "POST",
        url: url,
        data: formData,
        dataType:"Html",
        processData: false,
        contentType: false,
        cache: false,
        success: function (result) {
            var container = $("." + targetContainer);
            container.empty().html(result + '<div class="clearfix"></div>');
            //$('input[name="UsersData"]').val();

            //****************************** Logic to generate graph***********************//
            $.ajax({
                type: "POST",
                url: "/UM/AuthorizationAction/GetSelectedUsersGraph",
                data: tempData,
                dataType:"Html",
                processData: false,
                contentType: false,
                cache: false,
                success: function (chartdata) {
                    var chartData = JSON.parse(chartdata);
                    var targetId = [];
                    $('.theBox').children().each(function (index) {
                        if ($(this)[0].id)
                        targetId.push($(this)[0].id);
                    });
                    if (targetId && chartData.FromUser) //.includes("container")
                        generateChart(targetId[0], chartData.FromUser);
                    if (targetId && chartData.ToUser) //.includes("container")
                        generateChart(targetId[1], chartData.ToUser);
                    //******************************End Logic to generate graph***********************//

                    //****************************** Logic to fly airplane***********************//
                    //projectile = $("#airPlane");
                    //start_x = $('#the_Success_Member').offset().left;
                    //start_y = $('#the_Success_Referral').offset().left + 600;

                    //var bezier_params = {
                    //    start: {
                    //        x: start_x,
                    //        y: start_x,
                    //        angle: 10
                    //    },
                    //    end: {
                    //        x: start_y,
                    //        y: start_y,
                    //        angle: -10,
                    //        length: 1
                    //    }
                    //}

                    //var arc_params = {
                    //    center: [start_x, start_y],
                    //    radius: start_x - start_y,
                    //    start: start_x,
                    //    end: start_y,
                    //    dir: -1
                    //}

                    //$("#airPlane").animate({ path: new $.path.bezier(bezier_params) })
                    //$("").animate({ path: path }, 1000)


                    projectile = $("#airPlane");
                    start_x = projectile.offset().left - 1010;
                    start_y = projectile.offset().top - 340;
                    var anim_interval = setInterval(function () {
                        t = t + frame_interval;
                        updatePosition(t / 350, start_x, start_y);
                        if (t > 3500) {
                            clearInterval(anim_interval);
                        }
                    }, frame_interval);

                    //******************************end  Logic to fly airplane***********************//


                    $(".the_queue_bucket").css({ "display": "none" });
                    setTimeout(function () {
                        $(".doneSuccess").show();
                        setTimeout(function () {
                            $(".close_btn").trigger("click");
                            TabManager.closeCurrentlyActiveSubTab();
                            TabManager.navigateToTab({ "tabAction": "Search Member", "tabTitle": "Search Member", "dataTabMoveright": "false", "tabPath": "~/Areas/UM/Views/SearchMember/_SearchMember.cshtml", "tabContainer": "fullBodyContainer" });

                        }, 1500);
                    }, 3700);
                }
                });
        },
        error: function (result) {

        }
    });
});


//$('.close_modal_btn').click(function () {
//    $('#authChangeButtons .create-auth-btn').removeClass('disabled');
//});



$(document).on('mousemove', function (e) {

    if ($(window).width() > 1800) {
        $('#dataInBucket').css({
            left: e.pageX - 720,
            top: e.pageY - 2
        });
    }
    else {
        $('#dataInBucket').css({
            left: e.pageX - 540,
            top: e.pageY - 2
        });
    }
});


function updatePosition(t, start_x, start_y) {
    change_x = v * t * Math.cos(angle);
    change_y = (v * t * Math.sin(angle)) - (0.25 * g * Math.pow(t, 2));
    projectile.css({
        left: start_x + change_x + 'px',
        top: start_y - change_y + 'px'
    });
    i++;
    if (i % 15 == 0) {
        c = projectile.clone();
        c.css("opacity", "0.1");
        //$("#content").append(c);
    }
}

function generateChart(targetContainerId, data) {
    if (targetContainerId && data)
    {
        var chart = null;
        chart = c3.generate({
            size: {
                height: 80,
                width: 80
            },
            padding: {
                top: 0,
                bottom: 0,
                left: 0,
                right: 0,
            },
            data: {
                columns: [
                    ['Standard', data.StandardCount],
                    ['Expedited', data.ExpeditedCount],
                ],
                type: 'pie',
                colors: {
                    Standard: "#edb913",
                    Expedited: "#1779c0"
                },
                labels: false
            },
            legend: {
                show: false
            },
            tooltip: {
                show: false
            },
            area: {
                zerobased: false
            },
            pie:{
                label: {
                    show: false
                }
            }
        });
        $('#' + targetContainerId).append(chart.element);
    }
    
}


