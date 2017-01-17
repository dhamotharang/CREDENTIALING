//-----contactTypeCms--contactEntityCms
$('.ContactsModal').off('click', '.contactTypeCms').on('click', '.contactTypeCms', function () {
    TabManager.openCenterModal('/CMSData/ContactType', 'Contact Type');
});
$('.ContactsModal').off('click', '.contactEntityCms').on('click', '.contactEntityCms', function () {
    TabManager.openCenterModal('/CMSData/ContactEntity', '  Contact Entity');
});
$('.ContactsModal').off('click', '.directionCms').on('click', '.directionCms', function () {
    TabManager.openCenterModal('/CMSData/ContactDirection', 'Contact Direction');
});
$('.ContactsModal').off('click', '.reasonCms').on('click', '.reasonCms', function () {
    TabManager.openCenterModal('/CMSData/Reason', 'Reason');
});
$('.ContactsModal').off('click', '.outcomeTypeCms').on('click', '.outcomeTypeCms', function () {
    TabManager.openCenterModal('/CMSData/OutcomeType', 'Outcome Type');
});
$('.ContactsModal').off('click', '.outcomeCms').on('click', '.outcomeCms', function () {
    TabManager.openCenterModal('/CMSData/Outcome', 'Outcome');
});

$('.ContactsModal').off('change', '#Type').on('change', '#Type', function () {
    ContactNumberDynamic($("#Type").val());
    setDropdownData();
});

$('.ContactsModal').off('change', '#Entity').on('change', '#Entity', function () {
    setDropdownData();
});

$('.ContactsModal').off('change', '.contactDirection').on('change', '.contactDirection', function () {
    setReasonDropdownData();
});

$('.ContactsModal').off('change', '.contactOutcomeType').on('change', '.contactOutcomeType', function () {
    setOucomeDropdownData();
});

function ContactNumberDynamic(type)
{
    $("#ContactDetails div").css('display', 'none');
    if (type == "TELEPHONE CALL") {
        $("#contactNum").prependTo("#ContactDetails");
        $("#contactNum").css('display', '');
    }
    else if (type == "EMAIL") {
        $("#contactemail").prependTo("#ContactDetails");
        $("#contactemail").css('display', '');
    }
    else if (type == "FAX COMMUNICATION") {
        $("#contactfax").prependTo("#ContactDetails");
        $("#contactfax").css('display', '');
    }
    else if (type == "LETTER")
        $("#contactletter").prependTo("#ContactDetails");
    else {
        $("#contactother").prependTo("#ContactDetails");
        $("#contactother").css('display', '');
    }
}

function setReasonDropdownData()
{
    var type = $("#Type").val();
    var entity = $("#Entity").val();
    var direction = $("input[name='Direction']:checked").val();
    $("#Reason option").remove();
    $.ajax({
        type: 'GET',
        url: '/UM/Authorization/FilterContactReasons?ContactType=' + type + '&&ContactEntity=' + entity + '&&Direction=' + direction,
        error: function () {
        },
        success: function (info) {
            item = info.data;
            $('#Reason').append($('<option>', {
                value: "",
                text: "--SELECT--"
            }));
            if (item.length > 0) {
                $.each(item, function (i, item) {
                    $('#Reason').append($('<option>', {
                        value: item.ReasonDescription.toUpperCase(),
                        text: item.ReasonDescription
                    }));
                });
            }
        }
    });
}

function setOucomeDropdownData()
{
    var type = $("#Type").val();
    var entity = $("#Entity").val();
    var outcometype = $("input[name='OutcomeType']:checked").val();
    $("#outcome option").remove();
    $.ajax({
        type: 'GET',
        url: '/UM/Authorization/FilterContactOutcomes?ContactType=' + type + '&&ContactEntity=' + entity + '&&OutcomeType=' + outcometype,
        error: function () {
        },
        success: function (info) {
            item = info.data;
            $('#outcome').append($('<option>', {
                value: "",
                text: "--SELECT--"
            }));
            if (item.length > 0) {
                $.each(item, function (i, item) {
                    $('#outcome').append($('<option>', {
                        value: item.OutcomeType.toUpperCase(),
                        text: item.OutcomeType
                    }));
                });
            }
        }
    });
}

function setDropdownData()
{
    $("#ContactNumber").val('');
    $("#ContactName").val('');
    setReasonDropdownData();
    setOucomeDropdownData();
    var type = $("#Type").val();
    var entity = $("#Entity").val();
    if (entity && entity.toUpperCase() == "MEMBER")
    {
        var MemberName = $("#MemberName").text();
        if(MemberName!="-" && MemberName!="")
            $("#ContactName").val(MemberName);
        if (type && type.toUpperCase() == "TELEPHONE CALL")
        {
            var MemberPhone = $("#MemberPhone").text();
            if (MemberPhone != "-" && MemberPhone != "")
                $("#ContactNumber").val(MemberPhone);
        }
    }
    else if (entity && entity.toUpperCase() == "PCP") {
        $("#ContactName").val($("input[name='PCP.FullName']").val());
        if(type)
        {
            if (type.toUpperCase() == "TELEPHONE CALL") 
                $("#ContactNumber").val(FormatPhone($("input[name='PCP.PhoneNumber']").val()));
            else if(type.toUpperCase() == "FAX COMMUNICATION")
                $("#ContactNumber").val(FormatPhone($("input[name='PCP.FaxNumber']").val()));
        }
    }
    else if (entity && entity.toUpperCase() == "SPECIALIST") {
        $("#ContactName").val($("input[name='ServicingProvider.FullName']").val());
        if (type) {
            if (type.toUpperCase() == "TELEPHONE CALL")
                $("#ContactNumber").val(FormatPhone($("input[name='ServicingProvider.PhoneNumber']").val()));
            else if (type.toUpperCase() == "FAX COMMUNICATION")
                $("#ContactNumber").val(FormatPhone($("input[name='ServicingProvider.FaxNumber']").val()));
        }
    }
    else if (entity && entity.toUpperCase() == "REQUESTING PROVIDER") {
        $("#ContactName").val($("input[name='RequestingProvider.FullName']").val());
        if (type) {
            if (type.toUpperCase() == "TELEPHONE CALL")
                $("#ContactNumber").val(FormatPhone($("input[name='RequestingProvider.PhoneNumber']").val()));
            else if (type.toUpperCase() == "FAX COMMUNICATION")
                $("#ContactNumber").val(FormatPhone($("input[name='RequestingProvider.FaxNumber']").val()));
        }
    }
    else if (entity && entity.toUpperCase() == "ATTENDING PROVIDER") {
        $("#ContactName").val($("input[name='AttendingProvider.FullName']").val());
        if (type) {
            if (type.toUpperCase() == "TELEPHONE CALL")
                $("#ContactNumber").val(FormatPhone($("input[name='AttendingProvider.PhoneNumber']").val()));
            else if (type.toUpperCase() == "FAX COMMUNICATION")
                $("#ContactNumber").val(FormatPhone($("input[name='AttendingProvider.FaxNumber']").val()));
        }
    }
    else if (entity && entity.toUpperCase() == "ADMITTING PROVIDER") {
        $("#ContactName").val($("input[name='AdmittingProvider.FullName']").val());
        if (type) {
            if (type.toUpperCase() == "TELEPHONE CALL")
                $("#ContactNumber").val(FormatPhone($("input[name='AdmittingProvider.PhoneNumber']").val()));
            else if (type.toUpperCase() == "FAX COMMUNICATION")
                $("#ContactNumber").val(FormatPhone($("input[name='AdmittingProvider.FaxNumber']").val()));
        }
    }
    else if (entity && entity.toUpperCase() == "SURGEON") {
        $("#ContactName").val($("input[name='SurgeonProvider.FullName']").val());
        if (type) {
            if (type.toUpperCase() == "TELEPHONE CALL")
                $("#ContactNumber").val(FormatPhone($("input[name='SurgeonProvider.PhoneNumber']").val()));
            else if (type.toUpperCase() == "FAX COMMUNICATION")
                $("#ContactNumber").val(FormatPhone($("input[name='SurgeonProvider.FaxNumber']").val()));
        }
    }
}

function getRows(text) {
    if (text && text.length>0)
    {
        var cols = 120;
        var arraytxt = text.split('\n');
        var rows = arraytxt.length;
        for (var i = 0; i < arraytxt.length; i++)
            rows += parseInt(arraytxt[i].length / cols);
        if (rows < 4)
            rows = 4;
        return rows;
    }
}

function FormatPhone(phone)
{
    if(phone && phone!="null" && phone.length==10)
    {
        var FormatPhone =  "(" + phone.substr(0, 3) + ") " + phone.substr(3, 3) + '-' + phone.substr(6, 4);
        return FormatPhone;
    }
    return phone;
}

function PrefillContactFormData(contact) {
    for (var k in contact)
        contact[k] = (contact[k] != null && isNaN(contact[k])) ? (contact[k].toUpperCase()) : contact[k];
    ContactNumberDynamic(contact.ContactType);
    for (var i in contact) {
        $("input[type = text][name='" + i + "']").val(contact[i]);
        $("textarea[name='" + i + "']").val(contact[i]);
        $("textarea[name='" + i + "']").attr('rows', getRows(contact[i]));
        $("select[name='" + i + "'] option").removeAttr('selected').filter('[value="' + contact[i] + '"]').attr('selected', true);
        $('input:radio[name="' + i + '"][value="' + contact[i] + '"]').attr('checked', true);
        if (contact[i] == "TRUE")
        {
            $('input:checkbox[name="' + i + '"]').attr('checked', true);
            $('input:checkbox[name="' + i + '"]').val(true);
        }
        else if (contact[i] == "FALSE")
            $('input:checkbox[name="' + i + '"]').val(false);
    }
    setReasonDropdownData();
    setOucomeDropdownData();
    setTimeout(
    function () {
        for (var i in contact) 
            $("select[name='" + i + "'] option").removeAttr('selected').filter('[value="' + contact[i] + '"]').attr('selected', true);
    }, 400);
}

IndexAdd = function (contact) {
    $("#ContactSaveCancelButton .SaveBtn").addClass("SaveCreateContact");
    var CallDateTime = moment(new Date()).format('MM/DD/YYYY HH:mm:ss');
    $("input[name='CallDateTime']").val(CallDateTime);
    $("#outbound").attr('checked', true);
    $("#success").attr('checked', true);
};

// Save Contact 
$("#ContactSaveCancelButton").off("click", ".SaveCreateContact").on("click", ".SaveCreateContact", function () {
    $.ajax({
        url: '/Authorization/SaveContact',
        data: $('#contact_form').serialize(),
        type: "POST",
        cache: false,
        dataType: "html",
        success: function (data) {
            $("#emptyContactTemplate").remove();
            data = JSON.parse(data);
            var contact = JSON.parse(data.data);
            if (contact.CallDateTime)
                contact.CallDateTime = moment(contact.CallDateTime).format('MM/DD/YYYY HH:mm:ss');
            contact.CreatedBy = "BARBARA JOY";
            var hiddeninputdata = "";
            for (var k in contact) {
                contact[k] = (contact[k] == null) ? '' : contact[k];
                hiddeninputdata = hiddeninputdata + ' <input type="hidden" name="Contacts[0].' + k + '" value="' + contact[k] + '" /> ';
                contact[k] = (contact[k] == '') ? '-' : contact[k];
            }

            if (contact.Description != '-')
                var desc = '<span>' + contact.Description.substr(0, 10) + '</span> <span style="cursor:pointer" data-container="body" data-toggle="popover" title="Note" data-trigger="hover" data-placement="top" data-html="true" data-content="' + contact.Description + '<br/>Reason: ' + contact.Reason + '<br/>Outcome: ' + contact.Outcome + '" onmouseover="initPop()"><b>...</b></span>';
            else
                var desc = '<span style="cursor:pointer" data-container="body" data-toggle="popover" title="Note" data-trigger="hover" data-placement="top" data-html="true" data-content="' + contact.Description + '<br/>Reason: ' + contact.Reason + '<br/>Outcome: ' + contact.Outcome + '" onmouseover="initPop()"><b>...</b></span>';

            var tr = '<tr> <td class="theme_label_data"> ' + contact.CallDateTime + hiddeninputdata + ' </td>' +
            '<td class="theme_label_data">' + contact.ContactEntity + '</td>' +
            '<td class="theme_label_data">' + contact.ContactName + '</td>' +
            '<td class="theme_label_data">' + contact.ContactType + '</td>' +
            '<td class="theme_label_data">' + contact.EMailFaxOther + '</td>' +
            '<td class="theme_label_data">' + contact.Direction + '</td>' +
            '<td class="theme_label_data">' + contact.OutcomeType + '</td>' +
            '<td class="theme_label_data">' + desc + '</td>' +
            '<td class="theme_label_data">' + contact.CreatedBy + '</td>' +
            '<td class="theme_label_data"> <input type="checkbox" class="checkbox-radio contactincfax" name="Contacts[0].IncludeFax" value="false"/> <label><span></span></label> </td>' +
            '<td class="theme_label_data"> <button type="button" class="btn btn-xs btn-success" onclick="ViewContact(this)"> <i class="fa fa-eye"></i> </button> <button type="button" class="btn btn-xs btn-primary" onclick="EditContact(this)"> <i class="fa fa-pencil"></i> </button> <button type="button" class="btn btn-xs btn-danger" onclick="DeleteContact(this)"> <i class="fa fa-trash-o"></i> </button> </td>'
            '</tr>';
            $('#contactTbody').prepend(tr);
            $(".close_modal_btn").click();
            updateContactMapping();
        }
    });
});

//Prefill data for Edit Contact
IndexEdit = function (contact) {
    $("#ContactSaveCancelButton .SaveBtn").addClass("SaveEditContact");
    PrefillContactFormData(contact);
};

//Save Contact in Edit COntact
$("#ContactSaveCancelButton").off("click", ".SaveEditContact").on("click", ".SaveEditContact", function () {
    var id = $("input[name=tempContactID]").val();
    id = parseInt(id);
    $("#contactTbody > tr").eq(id).remove();
    $.ajax({
        url: '/Authorization/SaveContact',
        data: $('#contact_form').serialize(),
        type: "POST",
        cache: false,
        dataType: "html",
        success: function (data) {
            data = JSON.parse(data);
            var contact = JSON.parse(data.data);
            if (contact.CallDateTime)
                contact.CallDateTime = moment(contact.CallDateTime).format('MM/DD/YYYY HH:mm:ss');
            contact.CreatedBy = "BARBARA JOY";
            var index = id;
            var hiddeninputdata = "";
            for (var k in contact) {
                contact[k] = (contact[k] == null) ? '' : contact[k];
                hiddeninputdata = hiddeninputdata + ' <input type="hidden" name="Contacts[' + index + '].' + k + '" value="' + contact[k] + '" /> ';
                contact[k] = (contact[k] === '') ? '-' : contact[k];
            }
            if (contact.Description != '-')
                var desc = '<span>' + contact.Description.substr(0, 10) + '</span> <span style="cursor:pointer" data-container="body" data-toggle="popover" title="Note" data-trigger="hover" data-placement="top" data-html="true" data-content="' + contact.Description + '<br/>Reason: ' + contact.Reason + '<br/>Outcome: ' + contact.Outcome + '" onmouseover="initPop()"><b>...</b></span>';
            else
                var desc = '<span style="cursor:pointer" data-container="body" data-toggle="popover" title="Note" data-trigger="hover" data-placement="top" data-html="true" data-content="' + contact.Description + '<br/>Reason: ' + contact.Reason + '<br/>Outcome: ' + contact.Outcome + '" onmouseover="initPop()"><b>...</b></span>';
            if (contact.IncludeFax == true)
                var incfax = '<input type="checkbox" class="checkbox-radio contactincfax" name="Contacts[' + index + '].IncludeFax" value="true" checked="checked"/> <label><span></span></label>';
            else
                var incfax = '<input type="checkbox" class="checkbox-radio contactincfax" name="Contacts[' + index + '].IncludeFax" value="false"/> <label><span></span></label>';
            var tr = '<tr> <td class="theme_label_data"> ' + contact.CallDateTime + hiddeninputdata + ' </td>' +
            '<td class="theme_label_data">' + contact.ContactEntity + '</td>' +
            '<td class="theme_label_data">' + contact.ContactName + '</td>' +
            '<td class="theme_label_data">' + contact.ContactType + '</td>' +
            '<td class="theme_label_data">' + contact.EMailFaxOther + '</td>' +
            '<td class="theme_label_data">' + contact.Direction + '</td>' +
            '<td class="theme_label_data">' + contact.OutcomeType + '</td>' +
            '<td class="theme_label_data">' + desc + '</td>' +
            '<td class="theme_label_data">' + contact.CreatedBy + '</td>' +
            '<td class="theme_label_data">' + incfax + '</td>' +
            '<td class="theme_label_data"> <button type="button" class="btn btn-xs btn-success" onclick="ViewContact(this)"> <i class="fa fa-eye"></i> </button> <button type="button" class="btn btn-xs btn-primary" onclick="EditContact(this)"> <i class="fa fa-pencil"></i> </button> <button type="button" class="btn btn-xs btn-danger" onclick="DeleteContact(this)"> <i class="fa fa-trash-o"></i> </button> </td>'
            '</tr>';
            if (id == 0)
                $('#contactTbody').prepend(tr);
            else
                $('#contactTbody > tr').eq(id - 1).after(tr);

            $(".close_modal_btn").click();
        }
    });
});

//Prefill data for View Contact
IndexView = function (contact) {
    PrefillContactFormData(contact);
    //To disable all input fields for View Contact
    $(".ContactsModal :input").attr("disabled", true);
};

//Resizing Textarea
function do_resize10(textbox) {
    var maxrows = 4;
    var txt = textbox.value;
    var rows1 = textbox.rows;
    var cols = textbox.cols;
    var arraytxt = txt.split('\n');
    var rows = arraytxt.length;
    for (i = 0; i < arraytxt.length; i++)
        rows += parseInt(arraytxt[i].length / cols);
    if (rows > maxrows) textbox.rows = rows;
    else textbox.rows = maxrows;
}