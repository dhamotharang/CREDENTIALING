
    var onSuccessProviderType = function (data) {

    var status = " ";

    if (data.Result.StatusType == 'Active') {
        status = "<td><i class='fa fa-check-circle text-success'></i></td>";
    }
    else {
        status = "<td><i class='fa fa-times text-danger'></i></td>";
    }

    var HTMLString = "<tr><td class='maxWidthXLem mdm_title'>" + data.Result.Name + "</td>" +
                     "<td class='maxWidthXLem mdm_title'>" + data.Result.Code + "</td>" +
                     status +
                     "<td class='maxWidthXLem mdm_title'>" + data.Result.Date_Modified + "</td>" +
                     "<td class='maxWidthXLem'><div class='btn-group' role='group'>" +
                     "<button type='button' name='edit' id='@index'  class='btn btn-xs btn-primary editclass'>" +
                     "<i class='fa fa-edit'></i></button>"
    "</div></td></tr>";
    $("#ProviderTypeTableContent").prepend(HTMLString);
    $("#ProviderTypeTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);

    /*--------------- Enable the Edit Button---------------------------*/
    $('button:button.editclass').prop('disabled', false);


    /*--------- Clear the input text----------------------------------*/
    $(".addeditinputtitle").val('');
    $(".addeditinputcode").val('');
    $(".addeditinputdesc").val('');

    $("span.addtitle").text("ADD New Provider Type");

    /*-------------Remove Blur or Inactive Table Row--------------------*/
    $("td").parent(".deactivate").remove();


    /*--------------Remove form Background color------------------------*/
    $("#AddEditDiv").removeClass("formColor")
    /*-------- Remove the deactivate class from table row--------------*/
    $('*').removeClass('deactivate');
    /*-------- Remove the border class from table row--------------*/
    $('.addeditinputtitle').removeClass('border');
    $(".addeditinputcode").removeClass('border');
    $(".addeditinputdesc").removeClass('border');
    $('.alert-success').text("YOUR DATA IS SUCCESSFULLY ! ADDED.")

    $('.alert-success').prop('hidden', false);

    setTimeout(function () {

        $('.alert-success').prop('hidden', true);

    }, 5000);

    $.ajax({
        url: "/ProviderType/Reload",
        success: function (data) {
            // your data could be a View or Json or what ever you returned in your action method 
            // parse your data here
            $("#ProviderTypeList").replaceWith("<div class='row' id='ProviderTypeList'>" + data + "</div>")

        }
    });

};
