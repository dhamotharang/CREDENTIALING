/// <reference path="../../../Views/AccountManagement/_CreateNewAccount.cshtml" />
$(document).ready(function () {
    //function appendData(data) {
    //    var id = document.getElementById("AccountsList");
    //    $("#" + id).html(data);
    //}
    $('[data-toggle="tooltip"]').tooltip();
    function GetAccountsListingData() {
        $.ajax({
            type: "GET",
            url: "/AccountManagement/GetAccountsListingData",
            //data: { TabId: tabid },
            //contentType: "application/json",
            //async: false,
            dataType: "html",
            success: function (data, textStatus, XMLHttpRequest) {
                $('#AccountsList').html(data);
            }
        })
    }
    $('.settings-list').on("click", function () {
        try {
            event.preventDefault();
        } catch (e) {

        }
       
       // GetAccountsListingData();
        //alert();
    })
    $('.toDeactivatebtn').on('click', function () {
        //showLoaderSymbol(id);
        new PNotify({
            title: "Account Deactivated Successfully",
            text: false,
            type: 'success',
            //addclass: "stack-custom",
            animation: "fade",
            animate_speed: "slow",
            position_animate_speed: 500,
            delay: 1000,
            hide: true,
            opacity: 1,
            icon: true,
            stack: { "dir1": "up", "dir2": "right", "push": "top", "spacing1": 25, "spacing2": 25, "context": $("body"), "modal": false },
            styling: 'brighttheme',
            shadow: true,
        })
        $(this).removeClass('.red-button').addClass('.default-button');
    })
    $('.toActivatebtn').on("click", function () {
        $(this).removeClass('.default-button').addClass('.light-green-button');
        new PNotify({
            title: "Account Activated Successfully",
            text: false,
            type: 'success',
            //addclass: "stack-custom",
            animation: "fade",
            animate_speed: "slow",
            position_animate_speed: 500,
            delay: 1000,
            hide: true,
            opacity: 1,
            icon: true,
            stack: { "dir1": "up", "dir2": "right", "push": "top", "spacing1": 25, "spacing2": 25, "context": $("body"), "modal": false },
            styling: 'brighttheme',
            shadow: true,
        })
    })
})


function saveAccDetails(event, slidemodal, modalbackground, id, successmsg) {

    showLoaderSymbol(id);
    //var stack_modal = {"dir1": "down", "dir2": "right", "push": "top", "modal": true, "overlay_close": true};
    new PNotify({
        title: successmsg,
        text: false,
        type: 'success',
        //addclass: "stack-custom",
        animation: "fade",
        animate_speed: "slow",
        position_animate_speed: 500,
        delay: 1000,
        hide: true,
        opacity: 1,
        icon: true,
        stack: { "dir1": "up", "dir2": "right", "push": "top", "spacing1": 25, "spacing2": 25, "context": $("body"), "modal": false },
        styling: 'brighttheme',
        shadow: true,
    })
    event.preventDefault();
    setTimeout(function () {
        $('.' + slidemodal).html('');
        $('.' + slidemodal).animate({ width: '0px' }, 400, 'swing', function () {
            $('.' + modalbackground).remove();
        });
    }, 1000)
}

function CloseAccModal(slidemodal, modalbackground) {
    event.preventDefault();
    setTimeout(function () {
        $('.' + slidemodal).html('');
        $('.' + slidemodal).animate({ width: '0px' }, 400, 'swing', function () {
            $('.' + modalbackground).remove();
        });
    }, 1000)
}