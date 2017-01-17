
$(".SaveBtn").addClass("SaveContact");
 data = {};
 ContactDetails = [];
var index=0;
$(".SaveContact").on("click", function () {
     data = {
         Index:index++,
        Contactype: $("#Type").val(),
        ContactEntity: $("#Entity").val(),
        ContactName: $("#ContactName").val(),
        ContactNumber: $("#ContactNumber").val(),
        DateAndTime: $("#date").val(),
        Reason: $("#Reason").val(),
        OutCome: $("#outcome").val(),
        Direction: $("input[name='Direction']:checked").val(),
        OutComeType: $("input[name='OutcomeType']:checked").val(),
        Note: $("#note").val()
    };
     ContactDetails.push(data);
    var existingData = $('#contactTbody').html();
    $('#contactTbody').html("");

    var tr = '<tr><td name="ContactEntity' + data.Index + '">' + data.ContactEntity + '</td>' +
                        '<td name="ContactName' + data.Index + '">' + data.ContactName + '</td>' +
                        '<td name="ContactType' + data.Index + '">' + data.Contactype + '</td>' +
                        '<td name="ContactDetails' + data.Index + '">-</td>' +
                        '<td name="ContactDirection' + data.Index + '">' + data.Direction + '</td>' +
                        '<td name="ContactOutcomeType' + data.Index + '">' + data.OutComeType + '</td>' +
                        '<td name="ContactNote' + data.Index + '">' + data.Note + '</td>' +
                        '<td name="ContactDateTime' + data.Index + '">' + data.DateAndTime + '</td>' +
                        '<td name="ContactAddedBy' + data.Index + '">BARBARA JOYs</td>' +
                        '<td name="ContactIncLetter' + data.Index + '"><input type="checkbox" name="incLetter" class="form-control input-xs flat"></td>' +
                        '<td class="text-uppercase button-styles-xs">'+
                        '<input type="hidden" name="ContactReason" value="' + data.Reason + '">' +
                        '<input type="hidden" name="ContactOutcome"value="' + data.OutCome + '">' +
                        '<input type="hidden" name="ContactNumber" value="' + data.ContactNumber + '">' +
                        '<button onclick="EditContact(this);" class="copy-button editauthcontact"><i class="fa fa-pencil"></i></button> <button onclick="ShowModal(\'~/Views/Home/Modal/Contact/GetModelView.cshtml\', \'Add New Contact\');" class="light-green-button"><i class="fa fa-plus"></i></button></td>' +
                    '</tr>';
    
    var newData = existingData + tr;
    $('#contactTbody').html(newData);

    $(".SaveBtn").removeClass("SaveContact");
    $('.close_modal_btn').click();
    InitICheckFinal();

})