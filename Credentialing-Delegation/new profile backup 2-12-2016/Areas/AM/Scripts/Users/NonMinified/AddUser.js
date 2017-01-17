$(document).ready(function () {

    $('[data-toggle="tooltip"]').tooltip();
    //  $('.demo').fSelect();
    //$('.bootstrapmultiselect').multiselect({
    //    buttonWidth: '100%',
    //    enableCaseInsensitiveFiltering: true,
    //    numberDisplayed: 1,
    //buttonClass: 'btn btn-xs btn-default',
    //includeSelectAllOption: true,
    //nonSelectedText: 'Select Role',


    //});

});

$("#TopMenu").hide();
var Members = [];
$.ajax({
    url: '/Areas/AM/Resources/JSONData/AccountListData.js',
    success: function (response) {
        Members = JSON.parse(response);
        $(".ListAllUsers").empty();
        $("#UsersListForMapUser").empty();

        $("#UsersListForMapUser").append(
    ' <option selected disabled>SELECT USER TO COPY MAPPING DETAILS</option>'
        )
        $(".ListAllUsers").append(
     '<option selected disabled>SELECT USER</option>'
         )
        $.each(Members, function (index, value) {
            $(".ListAllUsers").append(
      '<option value="' + value.PersonalInformation.ID + '">' + value.PersonalInformation.FirstName.toUpperCase() + '-' + value.PersonalInformation.LastName.toUpperCase() + '</option>'
          )
            $("#UsersListForMapUser").append(
          '<option value="' + value.PersonalInformation.ID + '">' + value.PersonalInformation.FirstName.toUpperCase() + '-' + value.PersonalInformation.LastName.toUpperCase() + '</option>'
              )
        });
    }
});
$("#MappedUsers").empty();
$("#MappedUsers").append('<tr><td class="text-center" colspan="4">NO USER MAPPED</td></tr>');
$("#PrivilegesSummaryTableForAddUser").empty();
$("#PrivilegesSummaryTableForAddUser").append('<tr><td colspan="9" class="text-center">NO PRIVILEGES SELECTED</td></tr>');
//Ajax For Accounts Data
var AccountsData = [];
$.ajax({
    url: '/Areas/AM/Resources/JSONData/Accounts.js',
    success: function (response) {
        AccountsData = JSON.parse(response);
        $("#ListAllAccounts").empty();
        $.each(AccountsData, function (index, value) {
            $("#ListAllAccounts").append(
       '<option value="' + value.AccountName.toUpperCase() + '" >' + value.AccountName.toUpperCase() + '</option>'
           )
        });
    }
});
//-------------------------------------------------------------------------------

//Ajax For Teams Data
var TeamsData = [];
$.ajax({
    url: '/Areas/AM/Resources/JSONData/Teams.js',
    success: function (response) {
        TeamsData = JSON.parse(response);
        $("#ListAllTeams").empty();
        $.each(TeamsData, function (index, value) {
            $("#ListAllTeams").append(
       '<option value="' + value.TeamName.toUpperCase() + '" >' + value.TeamName.toUpperCase() + '</option>'
           )
        });
    }
});

//Ajax For Roles Data
var RolesData = [];
$.ajax({
    url: '/Areas/AM/Resources/JSONData/Roles.js',
    success: function (response) {
        RolesData = JSON.parse(response);
        $(".ListAllRoles").empty();
        $.each(RolesData, function (index, value) {
            $(".ListAllRoles").append(
       '<option value="' + value.RoleName.toUpperCase() + '" >' + value.RoleName.toUpperCase() + '</option>'
           )
        });
        $(".multiselect-plugin").multiselect({
            placeholder: "SELECT ROLES",
            selectAll: true,
            column: 1
            //enableFiltering: true

        });
    }
});
//-------------------------------------------------------------------------------

var UserIDForMapping;
$("#MappedUserName").on('change', function () {
    var val = this.value;
    if ($('#MappedUserName option').filter(function () {
        return this.value === val;
    }).length) {
        var fieldVal = $("#MappedUserName").val();
        var data = fieldVal.split('-')
        UserIDForMapping = val;
        //$("#MappedUserName").val(data[0] + '-' + data[1]);
        //$("#UserTeam").empty();
        //$("#UserRole").empty();
        //$("#UserTeam").multiselect('refresh');
        //$("#UserRole").multiselect('refresh');
        $("#UserTeamForAddUser").empty()
        $("#UserRoleForAddUser").empty()
        $("#UserTeamForAddUser").multiselect('destroy');
        $("#UserRoleForAddUser").multiselect('destroy');
        $.each(Members, function (index, value) {
            if (value.PersonalInformation.ID == UserIDForMapping) {
                $.each(value.AccountInformation.Team, function (i, v) {
                    $("#UserTeamForAddUser").append('<option value="' + v + '">' + v.toUpperCase() + '</option>')
                });
                $.each(value.PersonalInformation.UserRole, function (i, v) {
                    $("#UserRoleForAddUser").append('<option value="' + v + '">' + v.toUpperCase() + '</option>')
                });

            }

        });


        $("#UserTeamForAddUser").multiselect({
            placeholder: "Select Teams", selectAll: true, column: 2//enableFiltering: true
        });
        $("#UserRoleForAddUser").multiselect({
            placeholder: "Select Teams", selectAll: true, column: 2//enableFiltering: true
        });

    }

});
//Ajax for Business Processes Data
var RolesForAddNewUser = []
function MapRolesForNewUser() {
    $('.multiselect-plugin :selected').each(function (i, sel) {
        RolesForAddNewUser.push($(sel).val())
    });
    RolesForAddNewUser = $.unique(RolesForAddNewUser)
    $("#RolesForAddNewUser").empty();
    $("#RolesForAddNewUser").append('<option disabled selected>SELECT ROLE</option>');
    $.each(RolesForAddNewUser, function (index, value) {
        $("#RolesForAddNewUser").append('<option>' + value + '</option>');
    });
    RolesForAddNewUser = [];

}

$.ajax({
    url: '/Areas/AM/Resources/JSONData/BusinessProcessesData.js',
    success: function (response) {
        BusinessProcesses = JSON.parse(response);
        $('.RoleBPs').empty();
        $('.RoleBPs').append(
        '<option selected disabled>SELECT BUSINESS PROCESS</option>'
   );
        $.each(BusinessProcesses, function (index, value) {
            $('.RoleBPs').append(
         '<option value="' + value.BusinessProcess.toUpperCase() + '">' + value.BusinessProcess.toUpperCase() + '</option>'
    );

        });
    }
});
//-------------------------------------------------------------------

$("#RolesForAddNewUser").on('change', function () {
    var val = this.value;
    if ($('#RolesForAddNewUser option').filter(function () {
        return this.value === val;
    }).length) {
        $("#PrivilagesForAddUser").show();
    }
});
$("#cloneUserDetails").on('click', function () {
    if ($("#cloneUserDetails").is(":checked")) {
        $("#UsersListForMapUser").removeAttr("disabled")
        $("#UsersListForMapUser").empty();

        $("#UsersListForMapUser").append(
    ' <option selected disabled>SELECT USER TO COPY MAPPING DETAILS</option>'
        )
        $.each(Members, function (index, value) {

            $("#UsersListForMapUser").append(
          '<option value="' + value.PersonalInformation.ID + '">' + value.PersonalInformation.FirstName.toUpperCase() + '-' + value.PersonalInformation.LastName.toUpperCase() + '</option>'
              )
        });
    }
    else {
        $("#MapUserDetailsForAddUser").hide()
        $("#ExistingUserDetailsForAddUser").show()
        $("#UsersListForMapUser").prop("disabled", "disabled")
        $("#UsersListForMapUser").empty();

        $("#UsersListForMapUser").append(
    ' <option selected disabled>SELECT USER TO COPY MAPPING DETAILS</option>'
        )
    }

});
$("#UsersListForMapUser").on('change', function () {
    if ($("#cloneUserDetails").is(":checked")) {
        $("#MapUserDetailsForAddUser").show()
        $("#ExistingUserDetailsForAddUser").hide()
    }
    else {
        $("#MapUserDetailsForAddUser").hide()
        $("#ExistingUserDetailsForAddUser").show()
    }
});


function CancelCreateNewUser() {
    TabManager.closeCurrentlyActiveTab();
    var tab = {
        "tabAction": "Users",
        "tabTitle": "Users",
        "tabFloatMenu": "true",
        "tabFloatMenuPath": "~/Areas/AM/Views/Shared/_AMFloatMenu.cshtml",
        "tabPath": "/Areas/AM/Views/AccountManagement/_Accounts.cshtml",
        "tabContainer": "fullBodyContainer"
    }
    TabManager.navigateToTab(tab);
}
$("#AvailabilityCheckIconForAddUser").hide();
$("#NotAvailableIconForAddUser").hide();
$("#AvailabilityInfoIconForAddUser").show();
$("#AvailabilityCheckIconForAddUser").hide();
$("#AvailableIconForAddUser").hide();
function CheckUserNameAvailability() {
    if ($("#UniqueUserName").val() != "") {
        $("#AvailabilityInfoIconForAddUser").hide();
        $("#NotAvailableIconForAddUser").hide();
        $("#AvailableIconForAddUser").hide();
        $("#AvailabilityCheckIconForAddUser").show();
        setTimeout(function () {
            $.each(Members, function (index, value) {
                if (value.AccountInformation.UserName.toUpperCase() == $("#UniqueUserName").val().toUpperCase()) {
                    flag = 1;
                }
            });
            if (flag == 1) {
                $("#NotAvailableIconForAddUser").show();
            }
            else {
                $("#AvailableIconForAddUser").show();
            }
            $("#AvailabilityCheckIconForAddUser").hide();
        }, 1500);
    }
    else {
        $("#AvailabilityCheckIconForAddUser").hide();
        $("#NotAvailableIconForAddUser").hide();
        $("#AvailabilityInfoIconForAddUser").show();
        $("#AvailabilityCheckIconForAddUser").hide();
        $("#AvailableIconForAddUser").hide();

    }
}
var MultipleTeamRowIndex = 1;
var MultipleDataRowIndex = 1;
$(".AccountMappingDiv").empty()
$(".AccountMappingDiv").append(
                                    '<div class="row">' +
                                                '<div class="col-md-12">' +
                                                    '<div class="col-lg-4 col-md-4 col-xs-12 margin-bottom-10px">' +
                                                        '<div class="viewelements col-lg-8 zero-padding-left-right">' +
                                                            '<input type="text" list="ListAllTeams" placeholder="SELECT TEAM" class="form-control input-xs AddUserTextBoxes">' +
                                                        '</div>' +
                                                        '<div class="col-lg-4 col-md-4">' +
                                                            '<a class="btn btn-success input-xs AddMultipleTeam' + MultipleTeamRowIndex + '" @*id="AddMultipleTeamRow"*@ onclick="AddMultipleTeamRow(' + MultipleTeamRowIndex + ')"><i class="fa fa-plus margin_top_6px"></i></a>' +
                                                            '<a hidden class="btn btn-danger input-xs RemoveMultipleTeam' + MultipleTeamRowIndex + ' " @*id="RemoveAddMultipleTeamRow"*@ onclick="RemoveAddMultipleTeamRow(this,' + MultipleTeamRowIndex + ')"><i class="fa fa-minus"></i></a>' +
                                                        '</div>' +
                                                    '</div>' +
                                                    '<div class="col-lg-4 col-md-4 col-xs-12 margin-bottom-10px">' +
'<div class="viewelements">' +
                                                            '<input id="TeamLead' + MultipleDataRowIndex + '" type="radio" name="RoleTypeForAddUser' + MultipleDataRowIndex + '" class="normal-radio">' +
                                                            '<label class="theme_label_data" for="TeamLead' + MultipleDataRowIndex + '"><span></span>Team Lead</label>&nbsp;&nbsp;&nbsp;' +
                                                            '<input id="TeamMember' + MultipleDataRowIndex + '" type="radio" name="RoleTypeForAddUser' + MultipleDataRowIndex + '" class="normal-radio">' +
                                                            '<label class="theme_label_data" for="TeamMember' + MultipleDataRowIndex++ + '"><span></span>Team Member</label>' +
                                                        '</div>' +
                                                    '</div>' +
                                                    '<div class="col-lg-4 col-md-4 col-xs-12 margin-bottom-10px">' +
                                                        '<div class="viewelements">' +
                                                            '<select class="multiselect-plugin AddUserTextBoxes ListAllRoles" multiple id="ListAllRoles" onchange="MapRolesForNewUser()" data-placeholder="SELECT ROLES" style="width:100%;height:22px"></select>' +
'</div>' +
                                                    '</div>' +
                                                '</div>' +
                                            '</div>'
      )
function AddMultipleTeamRow(value) {
    $(".AccountMappingDiv").append(
                                      '<div class="row">' +
                                            '<div class="col-md-12">' +
                                                '<div class="col-lg-4 col-md-4 col-xs-12 margin-bottom-10px">' +

                                                    '<div class="viewelements col-lg-8 zero-padding-left-right">' +
                                                        '<input type="text" list="ListAllTeams" placeholder="SELECT TEAM" class="form-control input-xs AddUserTextBoxes">' +
                                                        '<datalist id="ListAllTeams"></datalist>' +
                                                    '</div >' +
                                                    '<div class="col-lg-4 col-md-4">' +
                                                        '<a class="btn btn-danger input-xs RemoveMultipleTeam' + MultipleTeamRowIndex + '" onclick="RemoveAddMultipleTeamRow(this,' + MultipleTeamRowIndex + ')"><i class="fa fa-minus margin_top_6px"></i></a>' +
                                '<a class="btn btn-success input-xs AddMultipleTeam' + MultipleTeamRowIndex + '" onclick="AddMultipleTeamRow(' + MultipleTeamRowIndex + ')"><i class="fa fa-plus margin_top_6px"></i></a>' +
                                                        '</div >' +
                                                '</div >' +
                                                '<div class="col-lg-4 col-md-4 col-xs-12 margin-bottom-10px">' +
                                                    '<div class="viewelements">' +
                                                        '<input id="TeamLead' + MultipleDataRowIndex + '" type="radio" name="RoleTypeForAddUser' + MultipleDataRowIndex + '" class="normal-radio">' +
                                                            '<label class="theme_label_data" for="TeamLead' + MultipleDataRowIndex + '"><span></span>Team Lead</label>&nbsp;&nbsp;&nbsp;' +
                                                            '<input id="TeamMember' + MultipleDataRowIndex + '" type="radio" name="RoleTypeForAddUser' + MultipleDataRowIndex + '" class="normal-radio">' +
                                                            '<label class="theme_label_data" for="TeamMember' + MultipleDataRowIndex++ + '"><span></span>Team Member</label>' +
                                                    '</div >' +
                                                '</div >' +
                                                '<div class="col-lg-4 col-md-4 col-xs-12 margin-bottom-10px">' +
                                                    '<div class="viewelements">' +
                                                        '<select class="multiselect-plugin AddUserTextBoxes ListAllRoles" multiple id=""  onchange="MapRolesForNewUser()" data-placeholder="SELECT ROLES" style="width:100%;height:22px"></select>' +
                                                    '</div >' +
                                                '</div >' +
                                            '</div >' +
                                        '</div >'

        )
    $(".ListAllRoles").empty();
    $.each(RolesData, function (index, value) {
        $(".ListAllRoles").append(
   '<option value="' + value.RoleName.toUpperCase() + '" >' + value.RoleName.toUpperCase() + '</option>'
       )
    });
    $(".multiselect-plugin").multiselect({
        placeholder: "SELECT ROLES",
        selectAll: true,
        column: 1
        //enableFiltering: true

    });
    $(".AddMultipleTeam" + value).hide();
    $(".RemoveMultipleTeam" + value).show();
    $(".AddMultipleTeam" + value).last().show()
}
function RemoveAddMultipleTeamRow(id, value) {
    $(id).parent().parent().parent("div").remove();
    $(".AddMultipleTeam" + value).hide();
    $(".RemoveMultipleTeam" + value).show();
    $(".AddMultipleTeam" + value).last().show()
    if ($(".AddMultipleTeam" + value).length == 1)
        $(".RemoveMultipleTeam" + value).hide();

}
var index = 2;
var AccountDataForNewUser = $("#AccountDataForAddUser").html();
function MapNewAccountForNewUser() {

    $("#outerTabsArea.outerTabsArea li").removeClass("active")
    $("#AccountTabDataForAddUser div.AccountTabDataForAddUser").removeClass("active in ")
    $("#outerTabsArea.outerTabsArea").append(' <li class="outertabs active"><a href="#Account' + index + '" class="outerTabsLinks tab" role="tab" data-toggle="tab" aria-expanded="true" id="Account' + index + 'Tab" data-tabtype="true"><b>Account Mapping-' + index + '</b></a><span class="custombadge outertabclose outertabclose-red" onclick="RemoveAccountTabInAddUser(this)" data-parentid="AddNewUser"><i class="fa fa-times"></i></span><span class="hidden tabs-popover" data-parenttabid="AddNewUser"></span> </li>');
    $("#AccountTabDataForAddUser").append('<div id="Account' + index++ + '" class="tab-pane fade in active p_background AccountTabDataForAddUser">' + AccountDataForNewUser + '</div>')
    MultipleTeamRowIndex++;
}
function RemoveAccountTabInAddUser(id) {
    if ($(id).parent('li').parent().children('li').length > 1)
        $(id).parent('li').remove();
}



// Adding New textFields to HomeContactNumber

var countOfHRows = 1;
var ListOfHomeNumber = ['listofHomeNumber_1']
$(".AddNewUser").on('click', '.plusButton', function () {
    var currentElement = $(this).parent().parent();
    var currentRowId = currentElement.attr('id');
    if (currentRowId = ListOfHomeNumber[0] && ListOfHomeNumber.length == 1) {
        $(this).parent().append(RemoveButton());
    }
    $('.AddNewUser').find('.plusButton').remove();
    ListOfHomeNumber.push('listofHomeNumber_' + (++countOfHRows));
    $(' #listOfHomeNumber').append(AddNewHomeNumber(countOfHRows))
    //for(var i=1;i<=ListOfOfficeNumber.length;i++)
    //{
    //    if(ListOfOfficeNumber.length!=countOfHRows)
    //    {
    //        $('#listofHomeNumber_' + (i)).removeClass('col-lg-7').addClass('col-lg-6')
    //    }
    //}
})

$('.AddNewUser').on('click', '.minusButton', function () {
    var currentElement = $(this).parent().parent();

    var currentRowId = currentElement.attr('id');
    if (ListOfHomeNumber.length > 2) {
        if (currentRowId == ListOfHomeNumber[ListOfHomeNumber.length - 1]) {
            $('#' + ListOfHomeNumber[ListOfHomeNumber.length - 2]).find('.HomeButtons').append(AddNewButton());
        }
        currentElement.remove();
    }
    else if (ListOfHomeNumber.length == 2) {
        currentElement.remove();
        if (currentRowId == ListOfHomeNumber[0]) {
            $('#' + ListOfHomeNumber[1]).find('.HomeButtons').empty().append(AddNewButton());
        }
        else if (currentRowId == ListOfHomeNumber[1]) {
            $('#' + ListOfHomeNumber[0]).find('.HomeButtons').empty().append(AddNewButton());
        }
    }
    ListOfHomeNumber.splice(ListOfHomeNumber.indexOf(currentRowId), 1);
})

function RemoveButton() {
    return '<button class="btn-danger minusButton" style="margin-left:3px;"><span class="fa fa-minus"></span></button>';
}

function AddNewButton() {
    return '<button class="btn-success plusButton" style="margin-left:3px;"><i class="fa fa-plus"></i></button>';
}
function AddNewHomeNumber(count) {
    return '<div class="col-lg-10" id="listofHomeNumber_' + count + '" style="display:inline-flex;margin-top:4px">' +
          '<input type="text" placeholder="HOME NUMBER" class="form-control input-xs AddUserTextBoxes">' +
          '<div class="col-lg-2 HomeButtons" style="display:inline-flex">' + RemoveButton() + AddNewButton() + '</div>' +
      '</div>';
}


// Adding New textFields to Office Number

var countOfORows = 1;
var ListOfOfficeNumber = ['listofOfficeNumber_1'];

$('.AddNewUser').on('click', '.OfficeplusButton', function () {
    var currentElement = $(this).parent().parent();
    var currentRowId = currentElement.attr('id');
    if (currentRowId = ListOfOfficeNumber[0] && ListOfOfficeNumber.length == 1) {
        $(this).parent().append(MinusButton());
    }
    $('.AddNewUser').find('.OfficeplusButton').remove();
    ListOfOfficeNumber.push('listofOfficeNumber_' + (++countOfORows));
    $('#listofOfficeNumber').append(AddNewOfficeNumber(countOfORows));

});

$('.AddNewUser').on('click', '.OfficeminusButton', function () {
    var currentElement = $(this).parent().parent();
    console.log(currentElement);
    var currentRowId = currentElement.attr('id');
    if (ListOfOfficeNumber.length > 2) {
        if (currentRowId == ListOfOfficeNumber[ListOfOfficeNumber.length - 1]) {
            $('#' + ListOfOfficeNumber[ListOfOfficeNumber.length - 2]).find('.OfficeButtons').append(PlusButton());
        }
        currentElement.remove();
    }
    else if (ListOfOfficeNumber.length == 2) {
        currentElement.remove();
        if (currentRowId == ListOfOfficeNumber[0]) {
            $('#' + ListOfOfficeNumber[1]).find('.OfficeButtons').empty().append(PlusButton());
        }
        else if (currentRowId == ListOfOfficeNumber[1]) {
            $('#' + ListOfOfficeNumber[0]).find('.OfficeButtons').empty().append(PlusButton());
        }
    }
    ListOfOfficeNumber.splice(ListOfOfficeNumber.indexOf(currentRowId), 1);
});

function PlusButton() {
    return '<button class="btn-success OfficeplusButton" style="margin-left:3px;"><i class="fa fa-plus"></i></button>';
}
function MinusButton() {
    return '<button class="btn-danger OfficeminusButton" style="margin-left:3px;"><span class="fa fa-minus"></span></button>';
}

function AddNewOfficeNumber(count) {
    return '<div class="col-lg-10" id="listofOfficeNumber_' + count + '" style="display:inline-flex;margin-top:4px">' +
            '<input type="text" placeholder="OFFICE NUMBER" class="form-control input-xs AddUserTextBoxes">' +
            '<div class="col-lg-2 OfficeButtons" style="display:inline-flex;">' + MinusButton() + PlusButton()
    '</div>' +
    '</div>';
}

//Adding Textfields for Email Id

var countOfMRows = 1;
var ListOfEmail = ['listofEmail_1'];

$('.AddNewUser').on('click', '.MailplusButton', function () {
    var currentElement = $(this).parent().parent();
    var currentRowId = currentElement.attr('id');
    if (currentRowId = ListOfEmail[0] && ListOfEmail.length == 1) {
        $(this).parent().append(EMinusButton());
    }
    $('.AddNewUser').find('.MailplusButton').remove();
    ListOfEmail.push('listofEmail_' + (++countOfMRows));
    $('#listOfEmail').append(AddNewEmail(countOfMRows));

});

$('.AddNewUser').on('click', '.MailminusButton', function () {
    var currentElement = $(this).parent().parent();
    var currentRowId = currentElement.attr('id');
    if (ListOfEmail.length > 2) {
        if (currentRowId == ListOfEmail[ListOfEmail.length - 1]) {
            $('#' + ListOfEmail[ListOfEmail.length - 2]).find('.MailButtons').append(EPlusButton());
        }
        currentElement.remove();
    }
    else if (ListOfEmail.length == 2) {
        currentElement.remove();
        if (currentRowId == ListOfEmail[0]) {
            $('#' + ListOfEmail[1]).find('.MailButtons').empty().append(EPlusButton());
        }
        else if (currentRowId == ListOfEmail[1]) {
            $('#' + ListOfEmail[0]).find('.MailButtons').empty().append(EPlusButton());
        }
    }
    ListOfEmail.splice(ListOfEmail.indexOf(currentRowId), 1);
});

function EPlusButton() {
    return '<button class="btn-success MailplusButton" style="margin-left:3px;"><i class="fa fa-plus"></i></button>';
}
function EMinusButton() {
    return '<button class="btn-danger MailminusButton" style="margin-left:3px;"><span class="fa fa-minus"></span></button>';
}

function AddNewEmail(count) {
    return '<div class="col-lg-10" id="listofEmail_' + count + '" style="display:inline-flex;margin-top:4px">' +
            '<input type="text" placeholder="EMAIL ID" class="form-control input-xs AddUserTextBoxes">' +
            '<div class="col-lg-2 MailButtons" style="display:inline-flex;">' + EMinusButton() + EPlusButton()
    '</div>' +
    '</div>';
}

//Adding TextFields for Fax

var countOfFRows = 1;
var ListOfFax = ['listOfFax_1'];

$('.AddNewUser').on('click', '.FaxplusButton', function () {
    var currentElement = $(this).parent().parent();
    var currentRowId = currentElement.attr('id');
    if (currentRowId = ListOfFax[0] && ListOfFax.length == 1) {
        $(this).parent().append(FMinusButton());
    }
    $('.AddNewUser').find('.FaxplusButton').remove();
    ListOfFax.push('listOfFax_' + (++countOfFRows));
    $('#listOfFax').append(AddNewFax(countOfFRows));

});

$('.AddNewUser').on('click', '.FaxminusButton', function () {
    var currentElement = $(this).parent().parent();
    var currentRowId = currentElement.attr('id');
    if (ListOfFax.length > 2) {
        if (currentRowId == ListOfFax[ListOfFax.length - 1]) {
            $('#' + ListOfFax[ListOfFax.length - 2]).find('.FaxButtons').append(FPlusButton());
        }
        currentElement.remove();
    }
    else if (ListOfFax.length == 2) {
        currentElement.remove();
        if (currentRowId == ListOfFax[0]) {
            $('#' + ListOfFax[1]).find('.FaxButtons').empty().append(FPlusButton());
        }
        else if (currentRowId == ListOfFax[1]) {
            $('#' + ListOfFax[0]).find('.FaxButtons').empty().append(FPlusButton());
        }
    }
    ListOfFax.splice(ListOfFax.indexOf(currentRowId), 1);
});

function FPlusButton() {
    return '<button class="btn-success FaxplusButton" style="margin-left:3px;"><i class="fa fa-plus"></i></button>';
}
function FMinusButton() {
    return '<button class="btn-danger FaxminusButton" style="margin-left:3px;"><span class="fa fa-minus"></span></button>';
}

function AddNewFax(count) {
    return '<div class="col-lg-10" id="listOfFax_' + count + '" style="display:inline-flex;margin-top:4px">' +
            '<input type="text" placeholder="FAX NUMBER" class="form-control input-xs AddUserTextBoxes">' +
            '<div class="col-lg-2 FaxButtons" style="display:inline-flex;">' + FMinusButton() + FPlusButton()
    '</div>' +
    '</div>';
}

//---------------------- CLONE USER
$(".CloneAccountMappingDiv").empty()
$(".CloneAccountMappingDiv").append(
                                    '<div class="row">' +
                                                '<div class="col-md-12">' +
                                                    '<div class="col-lg-4 col-md-4 col-xs-12 margin-bottom-10px">' +
                                                        '<div class="viewelements col-lg-8 zero-padding-left-right">' +
                                                            '<input type="text" list="ListAllTeams" value="TEAM2" placeholder="SELECT TEAM" class="form-control input-xs AddUserTextBoxes">' +
                                                        '</div>' +
                                                        '<div class="col-lg-4 col-md-4">' +
                                                            '<a class="btn btn-success input-xs CloneAddMultipleTeam' + MultipleTeamRowIndex + '" @*id="CloneAddMultipleTeamRow"*@ onclick="CloneAddMultipleTeamRow(' + MultipleTeamRowIndex + ')"><i class="fa fa-plus margin_top_6px"></i></a>' +
                                                            '<a hidden class="btn btn-danger input-xs CloneRemoveMultipleTeam' + MultipleTeamRowIndex + ' " @*id="RemoveCloneAddMultipleTeamRow"*@ onclick="CloneRemoveAddMultipleTeamRow(this,' + MultipleTeamRowIndex + ')"><i class="fa fa-minus"></i></a>' +
                                                        '</div>' +
                                                    '</div>' +
                                                    '<div class="col-lg-4 col-md-4 col-xs-12 margin-bottom-10px">' +
'<div class="viewelements">' +
                                                            '<input checked id="CloneTeamLead' + MultipleDataRowIndex + '" type="radio" name="RoleTypeForAddUser' + MultipleDataRowIndex + '" class="normal-radio">' +
                                                            '<label class="theme_label_data" for="CloneTeamLead' + MultipleDataRowIndex + '"><span></span>Team Lead</label>&nbsp;&nbsp;&nbsp;' +
                                                            '<input id="CloneTeamMember' + MultipleDataRowIndex + '" type="radio" name="RoleTypeForAddUser' + MultipleDataRowIndex + '" class="normal-radio">' +
                                                            '<label class="theme_label_data" for="CloneTeamMember' + MultipleDataRowIndex++ + '"><span></span>Team Member</label>' +
                                                        '</div>' +
                                                    '</div>' +
                                                    '<div class="col-lg-4 col-md-4 col-xs-12 margin-bottom-10px">' +
                                                        '<div class="viewelements">' +
                                                            '<select class="AddUserTextBoxes CloneListAllRoles" multiple id="CloneListAllRoles" onchange="CloneMapRolesForNewUser()" data-placeholder="SELECT ROLES" style="width:100%;height:22px"></select>' +
'</div>' +
                                                    '</div>' +
                                                '</div>' +
                                            '</div>'
      )
$("#CloneListAllRoles").append('<option selected value="INTAKE COORDINATOR">INTAKE COORDINATOR</option><option value="INTAKE COORDINATOR LEAD">INTAKE COORDINATOR LEAD</option><option value="NURSE">NURSE</option><option value="FACILITY NURSE">FACILITY NURSE</option><option value="MD">MD</option><option value="REFERRAL COORDINATOR">REFERRAL COORDINATOR</option><option value="REFERRAL COORDINATOR LEAD">REFERRAL COORDINATOR LEAD</option>')
$("#CloneListAllRoles").multiselect({
    placeholder: "SELECT ROLES",
    selectAll: true,
    column: 1
});
$("#CloneRolesForAddNewUser").append('<option value="INTAKE COORDINATOR">INTAKE COORDINATOR</option>');
$("#CloneRolesForAddNewUser").change(function () {
    $("#ClonePrivilagesForAddUser").show();
})
function CloneMapUser() {
    $.each(Members, function (index, value) {
        if (value.PersonalInformation.ID == $("#CloneMappedUserName").val()) {
            $("#CloneMappedUsers").append('<tr>' +
                                                '<td>' + value.PersonalInformation.FirstName + '</td>' +
                                                '<td>' + value.AccountInformation.Team + '</td>' +
                                                '<td>' + value.PersonalInformation.UserRole + '</td>' +
                                                '<td class="text-center">' +
                                                    '<a class="btn btn-xs btn-danger" style="" onclick="RemoveMappedUser(this)"><i class="fa fa-close"></i> REMOVE</a>' +
                                                '</td>' +
                                                '</tr>')
        }
    });
}
function CloneAddMultipleTeamRow(value) {
    $(".CloneAccountMappingDiv").append(
                                      '<div class="row">' +
                                            '<div class="col-md-12">' +
                                                '<div class="col-lg-4 col-md-4 col-xs-12 margin-bottom-10px">' +

                                                    '<div class="viewelements col-lg-8 zero-padding-left-right">' +
                                                        '<input type="text" list="ListAllTeams" placeholder="SELECT TEAM" class="form-control input-xs AddUserTextBoxes">' +
                                                    '</div >' +
                                                    '<div class="col-lg-4 col-md-4">' +
                                                        '<a class="btn btn-danger input-xs CloneRemoveMultipleTeam' + MultipleTeamRowIndex + '" onclick="CloneRemoveAddMultipleTeamRow(this,' + MultipleTeamRowIndex + ')"><i class="fa fa-minus margin_top_6px"></i></a>' +
                                '<a class="btn btn-success input-xs CloneAddMultipleTeam' + MultipleTeamRowIndex + '" onclick="CloneAddMultipleTeamRow(' + MultipleTeamRowIndex + ')"><i class="fa fa-plus margin_top_6px"></i></a>' +
                                                        '</div >' +
                                                '</div >' +
                                                '<div class="col-lg-4 col-md-4 col-xs-12 margin-bottom-10px">' +
                                                    '<div class="viewelements">' +
                                                        '<input id="CloneTeamLead' + MultipleDataRowIndex + '" type="radio" name="CloneRoleTypeForAddUser' + MultipleDataRowIndex + '" class="normal-radio">' +
                                                            '<label class="theme_label_data" for="CloneTeamLead' + MultipleDataRowIndex + '"><span></span>Team Lead</label>&nbsp;&nbsp;&nbsp;' +
                                                            '<input id="CloneTeamMember' + MultipleDataRowIndex + '" type="radio" name="CloneRoleTypeForAddUser' + MultipleDataRowIndex + '" class="normal-radio">' +
                                                            '<label class="theme_label_data" for="CloneTeamMember' + MultipleDataRowIndex++ + '"><span></span>Team Member</label>' +
                                                    '</div >' +
                                                '</div >' +
                                                '<div class="col-lg-4 col-md-4 col-xs-12 margin-bottom-10px">' +
                                                    '<div class="viewelements">' +
                                                        '<select class="multiselect-plugin AddUserTextBoxes CloneListAllRoles" multiple id=""  onchange="CloneMapRolesForNewUser()" data-placeholder="SELECT ROLES" style="width:100%;height:22px"></select>' +
                                                    '</div >' +
                                                '</div >' +
                                            '</div >' +
                                        '</div >'

        )
    $(".CloneListAllRoles").empty();
    $.each(RolesData, function (index, value) {
        $(".CloneListAllRoles").append(
   '<option value="' + value.RoleName.toUpperCase() + '" >' + value.RoleName.toUpperCase() + '</option>'
       )
    });
    $(".multiselect-plugin").multiselect({
        placeholder: "SELECT ROLES",
        selectAll: true,
        column: 1
        //enableFiltering: true

    });
    $(".CloneAddMultipleTeam" + value).hide();
    $(".CloneRemoveMultipleTeam" + value).show();
    $(".CloneAddMultipleTeam" + value).last().show()
}
function CloneRemoveAddMultipleTeamRow(id, value) {
    $(id).parent().parent().parent("div").remove();
    $(".CloneAddMultipleTeam" + value).hide();
    $(".CloneRemoveMultipleTeam" + value).show();
    $(".CloneAddMultipleTeam" + value).last().show()
    if ($(".CloneAddMultipleTeam" + value).length == 1)
        $(".CloneRemoveMultipleTeam" + value).hide();
}

var CloneRolesForAddNewUser = []
function CloneMapRolesForNewUser() {
    $('.CloneListAllRoles :selected').each(function (i, sel) {
        CloneRolesForAddNewUser.push($(sel).val())
    });
    CloneRolesForAddNewUser = $.unique(CloneRolesForAddNewUser)
    $("#CloneRolesForAddNewUser").empty();
    $("#CloneRolesForAddNewUser").append('<option disabled selected>SELECT ROLE</option>');
    $.each(CloneRolesForAddNewUser, function (index, value) {
        $("#CloneRolesForAddNewUser").append('<option>' + value + '</option>');
    });
    CloneRolesForAddNewUser = [];

}