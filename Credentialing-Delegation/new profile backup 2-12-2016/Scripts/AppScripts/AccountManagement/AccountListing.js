


$(document).ready(function () {
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

  
})

var CreateNewAccount = function () {

    $('.SaveBtn').hide();
    $('.close_modal_btn').hide();
}