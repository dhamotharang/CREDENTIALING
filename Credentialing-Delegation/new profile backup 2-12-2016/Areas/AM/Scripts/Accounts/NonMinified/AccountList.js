$(document).ready(function () {
   
    //$("#accounts").addClass("active");
    //fortest
    $("#users").addClass("active");
    $("#users").parent().addClass("work_menu_special_item")
   
    //------
    $("input:checkbox.switchCss").bootstrapSwitch();
});

//BreadCrumb Value
//$('.p_headername').text('AM');
//-------------------------------------------------------------------------------


//Initial View Setting 
//$(".Editable").hide();
//$("input:checkbox.switchCss").bootstrapSwitch();
//$("#BusinessProcessesData").hide();
$("#AllBPs").show();
$("#AllAccs").show();
$("#AllDepts").show();
//$("#AccountsListData").show();
//-------------------------------------------------------------------------------



//-------------------------------------------A C C O U N T S---------------------

//Ajax For Accounts Table View Data
var Members = [];
$.ajax({
    url: '/Areas/AM/Resources/JSONData/AccountListData.js',
    success: function (response) {
        Members = JSON.parse(response);
        //fortest
        DefaultUsersTabActive();
        //-------
        AccountsTableView()
        $("#ClaimsBPUsers").empty();
        $("#PMBPUsers").empty();
        $("#BMBPUsers").empty();
        $("#CapitationBPUsers").empty();
        $("#DMBPUsers").empty();
        $("#UMBPUsers").empty();
        $("#AdminAccounts").empty();
        $("#CMBPUsers").empty();
        $("#AccountListForNewTeam").empty();
        $("#AccountListForNewTeam").append('<option selected disabled>SELECT ACCOUNT</option>');
      
        $(".ListAllUsers").empty();
        $(".ListAllUsers").append('<option selected disabled>SELECT USER</option>');
        $("#MMBPUsers").empty();
        $.each(Members, function (index, value) {
            $("#AdminAccounts").append(
      '<option value="' + value.AccountInformation.AccountName.toUpperCase() + ' - ' + value.AccountInformation.AccountID + '">' +
          '</option>'
           )
            $("#AccountListForNewTeam").append(
      '<option value="' + value.AccountInformation.AccountName.toUpperCase() + ' - ' + value.AccountInformation.AccountID + '">' + value.AccountInformation.AccountName.toUpperCase() + ' - ' + value.AccountInformation.AccountID + '</option>'
           )
            $("#ClaimsBPUsers").append(
      '<option value="' + value.PersonalInformation.FirstName.toUpperCase() + ' - ' + value.PersonalInformation.LastName.toUpperCase() + '">'
          )
            $("#CapitationBPUsers").append(
    '<option value="' + value.PersonalInformation.FirstName.toUpperCase() + ' - ' + value.PersonalInformation.LastName.toUpperCase() + '">'
        )
            $("#PMBPUsers").append(
     '<option value="' + value.PersonalInformation.FirstName.toUpperCase() + ' - ' + value.PersonalInformation.LastName.toUpperCase() + '">'
         )
            $("#BMBPUsers").append(
    '<option value="' + value.PersonalInformation.FirstName.toUpperCase() + ' - ' + value.PersonalInformation.LastName.toUpperCase() + '">'
        )
            $("#DMBPUsers").append(
    '<option value="' + value.PersonalInformation.FirstName.toUpperCase() + ' - ' + value.PersonalInformation.LastName.toUpperCase() + '">'
        )
            $("#MMBPUsers").append(
    '<option value="' + value.PersonalInformation.FirstName.toUpperCase() + ' - ' + value.PersonalInformation.LastName.toUpperCase() + '">'
        )
            $("#UMBPUsers").append(
    '<option value="' + value.PersonalInformation.FirstName.toUpperCase() + ' - ' + value.PersonalInformation.LastName.toUpperCase() + '">'
        )
            $("#CMBPUsers").append(
     '<option value="' + value.PersonalInformation.FirstName.toUpperCase() + ' - ' + value.PersonalInformation.LastName.toUpperCase() + '">'
         )
            $("#ChildAccounts").append(
              '<option value="' + value.AccountInformation.AccountName.toUpperCase() + ' - ' + value.AccountInformation.AccountID + '">' +
              '</option>'
                  )
            $(".ListAllUsers").append(
       '<option value="' + value.PersonalInformation.ID + '" >' + value.PersonalInformation.FirstName.toUpperCase() + '-' + value.PersonalInformation.LastName.toUpperCase() + '</option>'
           )
        }
);
    }


});
//-------------------------------------------------------------------------------

//Ajax For Account Tree View Data
var AccountsTreeViewData = [];
$.ajax({
    url: '/Areas/AM/Resources/JSONData/AccountsData.js',
    success: function (response) {
        AccountsTreeViewData = JSON.parse(response);
    }
});
//-------------------------------------------------------------------------------

//Accounts Table View
function AccountsTableView() {
    $('#AccountListTreeData').empty();
    $('#TreeView').hide();
    $('#AccountsTableView').show();
    $('#AccountListTableData').empty();
    $('#AccountsAppendData').empty();
    $('#AccountsAppendData').append(' <li><input class="form-control input-xs" placeholder="SEARCH ACCOUNT" style="padding:11px"/></li>');
    $.each(Members, function (index, value) {
        if (value.AccountInformation.Status == 'Active') {
            $('#AccountListTableData').append(
       '<tr>' +
       '<td>' + value.AccountInformation.AccountID + '</td>' +
       '<td>' + value.AccountInformation.AccountName + '</td>' +
       '<td>' + value.AccountInformation.AccountType + '</td>' +
       '<td>' + value.AccountInformation.ReportTo + '</td>' +
       '<td>' + value.AccountInformation.Reportee + '</td>' +
       '<td class="' + value.AccountInformation.Status + '"> ' + value.AccountInformation.Status + '</td>' +
       '<td><a class="btn btn-primary btn-xs"  data-toggle="tooltip" data-placement="bottom" title="View Account" onclick="ViewAccount(\'' + value.AccountInformation.AccountID + '\')" ><i class="fa fa-eye"></i> View</a> &nbsp;&nbsp;' +
                '<a class="btn btn-primary btn-xs"  data-toggle="tooltip" data-placement="bottom" title="Edit Account" onclick="EditAccount(\'' + value.AccountInformation.AccountID + '\')" ><i class="fa fa-edit"></i> Edit</a> &nbsp;&nbsp;' +
                '<a class="btn btn-primary btn-xs"  data-toggle="tooltip" data-placement="bottom" title="Deactivate Account" onclick="DeactivateModal(\'' + value.AccountInformation.AccountID + '\')" ><i class="fa fa-close"></i> Deactivate</a>' + '</td>' +
    '</tr>'
    );
        }
        else {
            $('#AccountListTableData').append(
       '<tr>' +
       '<td>' + value.AccountInformation.AccountID + '</td>' +
       '<td>' + value.AccountInformation.AccountName + '</td>' +
       '<td>' + value.AccountInformation.AccountType + '</td>' +
       '<td>' + value.AccountInformation.ReportTo + '</td>' +
       '<td>' + value.AccountInformation.Reportee + '</td>' +
       '<td class="' + value.AccountInformation.Status + '"> ' + value.AccountInformation.Status + '</td>' +
         '<td><a class="btn btn-primary btn-xs"  data-toggle="tooltip" data-placement="bottom" title="View Account" onclick="ViewAccount(\'' + value.AccountInformation.AccountID + '\')" ><i class="fa fa-eye"></i> View</a> &nbsp;&nbsp;' +
      // '<td><a class="btn btn-primary btn-xs"  onclick="ViewAccount(\'' + value.AccountInformation.AccountID + '\')" ><i class="fa fa-eye"></i> View</a> &nbsp;&nbsp;' +
                '<a class="btn btn-primary btn-xs"  data-toggle="tooltip" data-placement="bottom" title="Edit Account" onclick="EditAccount(\'' + value.AccountInformation.AccountID + '\')" ><i class="fa fa-edit"></i> Edit</a> &nbsp;&nbsp;' +
                '<a class="btn btn-primary btn-xs" data-toggle="tooltip" data-placement="bottom" title="Deactivate Account" onclick="ActivateModal(\'' + value.AccountInformation.AccountID + '\')" ><i class="fa fa-check"></i> Activate &nbsp;&nbsp;&nbsp;</a>' + '</td>' +
    '</tr>'
    );
        }
        //'<td><a  class="btn btn-xs btn-primary  tab-navigation member-actions" data-toggle="tooltip" title="All Accounts" data-tab-val="Accounts" data-tab-action="Accounts"' +
        //      'data-tab-title="View ' + value.AccountInformation.AccountID + '" data-tab-container="fullBodyContainer" data-tab-autoflush="false" data-tab-path="~/Areas/AM/Views/AccountManagement/_ViewAccount.cshtml"' +
        //   'data-tab-floatmenu="true" data-tab-float-menu-path="~/Areas/AM/Views/Shared/_AMFloatMenu.cshtml"  onclick="ViewAccount(\'' + value.AccountInformation.AccountID + '\')" ><i class="fa fa-eye"></i> View</a> &nbsp;&nbsp;' +


        $('#AccountsAppendData').append(
            '<li><a style="padding:11px" onclick="ViewAccount(\'' + value.AccountInformation.AccountID + '\')" >' + value.AccountInformation.AccountName + ' - ' + value.AccountInformation.AccountID + '</a></li>'
            );
        $('[data-toggle="tooltip"]').tooltip();
        $("input:checkbox.switchCss").bootstrapSwitch();

    });
}
//-------------------------------------------------------------------------------
//Accounts Tree View
function AccountsTreeView() {
    $('#AccountListTreeData').empty();
    $('#AccountsTableView').hide();
    $('#TreeView').show();
    var width = 1000;
    var height = 600;
    var maxLabel = 150;
    var duration = 500;
    var radius = 5;

    var i = 0;
    var root;

    var tree = d3.layout.tree()
        .size([height, width]);

    var diagonal = d3.svg.diagonal()
        .projection(function (d) { return [d.y, d.x]; });

    var svg = d3.select("#AccountListTreeData").append("svg")
        .attr("width", width)
        .attr("height", height)
            .append("g")
            .attr("transform", "translate(" + maxLabel + ",0)");

    root = AccountsTreeViewData;
    root.x0 = height / 2;
    root.y0 = 0;

    root.children.forEach(collapse);

    function update(source) {

        // Compute the new tree layout.
        var nodes = tree.nodes(root).reverse(),
            links = tree.links(nodes);

        // Normalize for fixed-depth.
        nodes.forEach(function (d) { d.y = d.depth * 180; });

        // Update the nodes…
        var node = svg.selectAll("g.node")
            .data(nodes, function (d) { return d.id || (d.id = ++i); });

        // Enter any new nodes at the parent's previous position.
        var nodeEnter = node.enter().append("g")
            .attr("class", "node")
            .attr("transform", function () { return "translate(" + source.y0 + "," + source.x + ")"; })
        ;
        var div = d3.select("body").append("span")
          .attr("class", "tooltip")
          .style("opacity", 1e-6);

        nodeEnter.append("circle")
            .attr("r", 1e-6)
            .style("fill", function (d) { return d._children ? "lightsteelblue" : "rgb(68, 105, 166)"; })
            .on("click", click)
        .on("mouseover", mouseovered)
            .on("mousemove", function (d) { mousemoveed(d); })
            .on("mouseout", mouseouted);
        ;

        nodeEnter.append("text")
            .attr("x", function (d) { return d.children || d._children ? -10 : 10; })
            .attr("dy", ".35em")
            .attr("text-anchor", function (d) { return d.children || d._children ? "end" : "start"; })
            .text(function (d) { return d.name; })
            .style("fill-opacity", 1e-6)
           // .style("font-size", 13)
            .style("font-weight", "bold")
         .on("click", clicked)
        .on("mouseover", mouseover)
            .on("mousemove", function (d) { mousemove(d); })
            .on("mouseout", mouseout);
        // Transition nodes to their new position.
        var nodeUpdate = node.transition()
            .duration(duration)
            .attr("transform", function (d) { return "translate(" + d.y + "," + d.x + ")"; });

        nodeUpdate.select("circle")
            .attr("r", 4.5)
            .style("fill", function (d) { return d._children ? "lightsteelblue" : "#fff"; });

        nodeUpdate.select("text")
            .style("fill-opacity", 1);

        // Transition exiting nodes to the parent's new position.
        var nodeExit = node.exit().transition()
            .duration(duration)
            .attr("transform", function () { return "translate(" + source.y + "," + source.x + ")"; })
            .remove();

        nodeExit.select("circle")
            .attr("r", 1e-6);

        nodeExit.select("text")
            .style("fill-opacity", 1e-6);

        // Update the links…
        var link = svg.selectAll("path.link")
            .data(links, function (d) { return d.target.id; });

        // Enter any new links at the parent's previous position.
        link.enter().insert("path", "g")
            .attr("class", "link")
            .attr("d", function () {
                var o = { x: source.x, y: source.y };
                return diagonal({ source: o, target: o });
            });

        // Transition links to their new position.
        link.transition()
            .duration(duration)
            .attr("d", diagonal);

        // Transition exiting nodes to the parent's new position.
        link.exit().transition()
            .duration(duration)
            .attr("d", function () {
                var o = { x: source.x, y: source.y };
                return diagonal({ source: o, target: o });
            })
            .remove();
        function mouseover() {
            div.transition()
            .duration(300)
            .style("opacity", 1);
        }

        function mousemove(d) {
            div
            .text("View - " + d.name)
            .style("left", (d3.event.pageX) + "px")
            .style("top", (d3.event.pageY) + "px")
            .style("margin-left", "-40px")
            .style("margin-top", "-50px");
        }

        function mouseout() {
            div.transition()
            .duration(300)
            .style("opacity", 1e-6);
        }
        function mouseovered() {
            div.transition()
            .duration(300)
            .style("opacity", 1);
        }

        function mousemoveed(d) {
            div
            .text("Collapse / Expand - " + d.name)
            .style("left", (d3.event.pageX) + "px")
            .style("top", (d3.event.pageY) + "px")
            .style("margin-left", "-90px")
            .style("margin-top", "-50px");
        }

        function mouseouted() {
            div.transition()
            .duration(300)
            .style("opacity", 1e-6);
        }
    }


    function computeRadius(d) {
        if (d.children || d._children) return (radius + (radius * nbEndNodes(d) / 10));
        else return radius;
    }

    function nbEndNodes(n) {
        var nb = 0;
        if (n.children) {
            n.children.forEach(function (c) {
                nb += nbEndNodes(c);
            });
        }
        else if (n._children) {
            n._children.forEach(function (c) {
                nb += nbEndNodes(c);
            });
        }
        else nb++;

        return nb;
    }

    function click(d) {
        if (d.children) {
            d._children = d.children;
            d.children = null;
        }
        else {
            d.children = d._children;
            d._children = null;
        }
        update(d);
    }
    function clicked() {
        ViewAccount()
    }
    function mouseover() {
        d3.select(this).append("text")
            .attr("class", "hover")
            .attr('transform', function () {
                return 'translate(5, -10)';
            })
            .text("Click to view");
    }

    // Toggle children on click.

    function collapse(d) {
        if (d.children) {
            d._children = d.children;
            d._children.forEach(collapse);
            d.children = null;
        }
    }
    function expand(d) {
        var children = (d.children) ? d.children : d._children;
        if (d._children) {
            d.children = d._children;
            d._children = null;
        }
        if (children)
            children.forEach(expand);
    }

    function expandAll() {
        expand(root);
        update(root);
    }

    update(root);
    expandAll();
}
//-------------------------------------------------------------------------------
// Switching Between TreeView and TableView
$("#TreeTableView").change(function () {
    if ($("#TreeTableView").is(":checked")) {
        AccountsTreeView();
        $("#TableViewLabel").css("color", "gray")
        $("#TreeViewLabel").css("color", "black")
    }
    else {
        $("#TableViewLabel").css("color", "black")
        $("#TreeViewLabel").css("color", "gray")

        AccountsTableView();
    }

});
//--------------------------------------------------------------------------------
function AccountsActiveTab() {
    $("#TopMenu").show();
    $("#teams").parent().removeClass("work_menu_special_item")
    $("#users").parent().removeClass("work_menu_special_item")
    $("#accounts").parent().addClass("work_menu_special_item")
    $("#businessProcesses").parent().removeClass("work_menu_special_item")
    $("#departments").parent().removeClass("work_menu_special_item")
    $("#userroles").parent().removeClass("work_menu_special_item")
    $("#businessProcesses").removeClass("active");
    $("#userroles").removeClass("active");
    $("#users").removeClass("active");
    $("#departments").removeClass("active");
    $("#teams").removeClass("active");
    $("#IndividualUserView").hide();
    $("#viewUserDetails").hide();
    $("#editUserDetails").hide();
    $(".AddNewUser").hide();
    $("#accounts").addClass("active");
    $("#users").removeClass("active");
    $("#BusinessProcessesData").hide(); 
    $("#AccountsListData").show();
    $("#AddNewAccountBtn").show();
    $("#EditAccountData").hide();
    $("#ViewUserRolesData").hide();    
    $("#ViewAccountData").hide();
    $("#ViewDepartmentsData").hide();
    $("#ViewUsersData").hide();
    $("#ViewTeamsData").hide();
    $(".BusinessProcessesTopMenuR").css({ "opacity": "0.3" });
    $(".BusinessProcessesTopMenuL").css({ "opacity": "0.3", "box-shadow": "0px 0px 0px" });
    $(".AccountsTopMenuR").css({ "opacity": "0.3" });
    $(".AccountsTopMenuL").css({ "opacity": "0.3", "box-shadow": "0px 0px 0px" });
    $(".DepartmentsTopMenuR").css({ "opacity": "0.3" });
    $(".DepartmentsTopMenuL").css({ "opacity": "0.3", "box-shadow": "0px 0px 0px" });
    $(".TeamsTopMenuR").css({ "opacity": "0.3" });
    $(".TeamsTopMenuL").css({ "opacity": "0.3", "box-shadow": "0px 0px 0px" });
    $("#AllTeams").show();
    $("#AllBPs").show();
    $("#AllAccs").show();
    $("#AllDepts").show();
    $("#AllDeptsDropdown").hide();
    $("#AllBPsDropdown").hide();
    $("#AllTeamsDropdown").hide();
    $("#AllAccsDropdown").hide();
}
//Side Menu Accounts Click
$("#accounts").click(function () {
    AccountsActiveTab()
});
//--------------------------------------------------------------------------------

//Account View (Individual)-------------------------------------------------------
var IDForEditAccount;
function ViewAccount(idvalue) {

    if (!idvalue) {
        idvalue = "1001"
    }
    IDForEditAccount = idvalue;
    if (!($("#businessProcesses").hasClass("active") || $("#departments").hasClass("active") || $("#teams").hasClass("active"))) {
        
        $("#AccDetailsEdit").show();
        $("#SaveCancBtn").hide();
        $("#AllUsers").empty();
        $(".EditAccountDetails").hide();
        $(".ViewAccountDetails").show();
        $("#AccountsListData").hide();
        $("#ViewAccountData").show();
        //$("#EditAccountData").hide();
        $(".AccountsTopMenuL").css({ "opacity": "1", "box-shadow": "5px 5px 10px" });
        $(".AccountsTopMenuR").css({ "opacity": "1" });
        $("#AllAccs").hide();
        $("#AllAccsDropdown").show();
        $.each(Members, function (index, value) {
            $("#AllUsers").append(
        '<option value="' + value.PersonalInformation.FirstName.toUpperCase() + ' - ' + value.PersonalInformation.LastName.toUpperCase() + '">'
            )
            if (idvalue == value.AccountInformation.AccountID) {
                $("#topMenuAccText").text(value.AccountInformation.AccountName + " - " + value.AccountInformation.AccountID);
                $("#accountid").text(value.AccountInformation.AccountID);
                $("#accountname").text(value.AccountInformation.AccountName);
                $("#accounttype").text(value.AccountInformation.AccountType);
                $("#accountstatus").text(value.AccountInformation.Status);
                $("#accountcreatedby").text(value.AccountInformation.CreatedBy);
                $("#accountcreatedon").text(value.AccountInformation.CreatedOn);
                $("#accountlastmodifiedby").text(value.AccountInformation.LastModifiedBy);
                $("#accountlastmodifiedon").text(value.AccountInformation.LastModifiedOn);

                $("#accountholdername").text(value.PersonalInformation.FirstName);
                $("#accountholderaddress").text(value.AddressInformation.Address);
                $("#accountholderstreet").text(value.AddressInformation.Street);
                $("#accountholdercity").text(value.AddressInformation.City);
                $("#accountholdercounty").text(value.AddressInformation.County);
                $("#accountholderstate").text(value.AddressInformation.State);
                $("#accountholderpincode").text(value.AddressInformation.ZipCode);
                $("#accountholdertaxid").text(value.ContactInformation.TaxID);
                $("#accountholderfaxno").text(value.ContactInformation.FaxNumber);
                $("#accountholderemail").text(value.ContactInformation.email);
                $("#accountholdercontactno").text(value.ContactInformation.ContactNumber);
            }
        });
    }
    else {
        $.each(Members, function (index, value) {
            if (idvalue == value.AccountInformation.AccountID) {
                $("#topMenuAccText").text(value.AccountInformation.AccountName + " - " + value.AccountInformation.AccountID);
            }
        });
    }
}
//--------------------------------------------------------------------------------
$(".EditAccountDetails").hide();
//Account Holder Edit/Update/Cancel
function EditAccount(idvalue)
{
    if (!idvalue) {
        idvalue = IDForEditAccount;
    }
    if (!($("#businessProcesses").hasClass("active") || $("#departments").hasClass("active"))) {
        //$(".Editable").hide();
        //$(".Label").show();
        $(".EditAccountDetails").show();
        $(".ViewAccountDetails").hide();
        $("#SaveCancBtn").hide();
        $("#AllUsers").empty();
        $("#AccountsListData").hide();
        $("#ViewAccountData").show();
        //$("#EditAccountData").show();
        $(".AccountsTopMenuL").css({ "opacity": "1", "box-shadow": "5px 5px 10px" });
        $(".AccountsTopMenuR").css({ "opacity": "1" });
        $("#AllAccs").hide();
        $("#AllAccsDropdown").show();
        $.each(Members, function (index, value) {
            $("#AllUsers").append(
        '<option value="' + value.PersonalInformation.FirstName.toUpperCase() + ' - ' + value.PersonalInformation.LastName.toUpperCase() + '">'
            )
            if (idvalue == value.AccountInformation.AccountID) {
                $("#topMenuAccText").text(value.AccountInformation.AccountName + " - " + value.AccountInformation.AccountID);
                $("#editaccountid").val(value.AccountInformation.AccountID);
                $("#editaccountname").val(value.AccountInformation.AccountName);
                $("#editaccounttype").val(value.AccountInformation.AccountType);
                $("#editaccountstatus").val(value.AccountInformation.Status);
                $("#editaccountcreatedby").val(value.AccountInformation.CreatedBy);
                $("#editaccountcreatedon").val(value.AccountInformation.CreatedOn);
                $("#editaccountlastmodifiedby").val(value.AccountInformation.LastModifiedBy);
                $("#editaccountlastmodifiedon").val(value.AccountInformation.LastModifiedOn);                    

                $("#editaccountholdername").val(value.PersonalInformation.FirstName);
                $("#editaccountholderaddress").val(value.AddressInformation.Address);
                $("#editaccountholderstreet").val(value.AddressInformation.Street);
                $("#editaccountholdercity").val(value.AddressInformation.City);
                $("#editaccountholdercounty").val(value.AddressInformation.County);
                $("#editaccountholderstate").val(value.AddressInformation.State);
                $("#editaccountholderpincode").val(value.AddressInformation.ZipCode);
                $("#editaccountholdertaxid").val(value.ContactInformation.TaxID);
                $("#editaccountholderfaxno").val(value.ContactInformation.FaxNumber);
                $("#editaccountholderemail").val(value.ContactInformation.email);
                $("#editaccountholdercontactno").val(value.ContactInformation.ContactNumber);
            }
        });
    }
    else {
        $.each(Members, function (index, value) {
            if (idvalue == value.AccountInformation.AccountID) {
                $("#topMenuAccText").text(value.AccountInformation.AccountName + " - " + value.AccountInformation.AccountID);
            }
        });
    }
}


//function EditAccount(idvalue) {
//    $("#UpdateAccountBtn").show();
//    $("#editAccountTitle").show();
//    $("#mapAccount").show();
//    $("#availabilityCheckDiv").hide();
//    $("#addAccountTitle").hide();
//    $("#createAccount").hide();
//    $("#confirmEmailAdd").hide();
//    $("#map").prop('checked', true)
//    $("#mapAccountIcon").addClass("fa-chevron-up")
//    $("#mapAccountIcon").removeClass("fa-chevron-down")

//    $.each(Members, function (index, value) {
//        if (idvalue == value.AccountInformation.AccountID) {
//            $("#Email").val(value.OtherInformation.AdminEmail.toUpperCase());
//            $("#AccountName").val(value.AccountInformation.AccountName.toUpperCase());
//            $("#accType").val(value.AccountInformation.AccountType.toUpperCase());
//        }
//    });
//    showModal('AddAccountModal');
//}



$('#CancelAccountHolderEditBtn').click(function () {
    $(".Editable").hide();
    $(".Label").show();
    $("#AccDetailsEdit").show();
    $("#SaveCancBtn").hide();

})
$('#SaveAccountHolderBtn').click(function () {
    $(".Editable").hide();
    $(".Label").show();
    $("#AccDetailsEdit").show();
    $("#SaveCancBtn").hide();

})
//-------------------------------------------------------------------------------
function UpdateAccount() {
    AccountsActiveTab()
    new PNotify({
        title: "Account Updated Successfully",
        type: 'success',
        width: "23%",
        animate: {
            animate: true,
            in_class: "lightSpeedIn",
            out_class: "slideOutRight"
        }
    });

}
//Account Deactivation
var idForDeactivation;
function DeactivateModal(idvalue) {
    showModal('Deactivate');
    idForDeactivation = idvalue;
}
function AccountDeactivate() {
    new PNotify({
        title: "Deactivated Successfully",
        //text: 'Account ' + AccountName + ' Created Successfully',
        type: 'success',

        animate: {
            animate: true,
            in_class: "lightSpeedIn",
            out_class: "slideOutRight"
        }
    });
}
//-------------------------------------------------------------------

//Account Activation
var idForActivation;
function ActivateModal(idvalue) {
    showModal('Activate');
    idForActivation = idvalue;
}
function AccountActivate() {

    new PNotify({
        title: "Activated Successfully",
        //text: 'Account ' + AccountName + ' Created Successfully',
        type: 'success',

        animate: {
            animate: true,
            in_class: "lightSpeedIn",
            out_class: "slideOutRight"
        }
    });
}
//-------------------------------------------------------------------

//Add New Account Modal
function AddAccountModal() {
    
    $(".accmaps").remove();
    $("#noValue").hide();
    $("#notAvailable").hide();
    $("#available").hide();
    $("#availabilityCheckIcon").hide();
    $("#availabilityCheck").show();
    $("#UpdateAccountBtn").hide();
    $("#availabilityCheckDiv").show();
    $("#editAccountTitle").hide();
    $("#addAccountTitle").show();
    $("#createAccount").show();
    $("#confirmEmailAdd").show();
    $("#mapAccount").hide();
    $("#map").prop('checked', false)
    $("#mapAccountIcon").addClass("fa-chevron-down")
    $("#mapAccountIcon").removeClass("fa-chevron-up")
    showModal('AddAccountModal');
}
//-------------------------------------------------------------------

//Checking for Account Name Conflicts
var IsAvaalableForCreate = false;
$("#AccountName").blur(function () {
    $("#noValue").hide();
    $("#availabilityCheckDiv").show();
    $("#availabilityCheck").hide();
    $("#notAvailable").hide();
    $("#available").hide();
    $("#availabilityCheckIcon").show();
    var AccountName = $("#AccountName").val()
    var flag = 0;
    if (AccountName != "") {
        setTimeout(function () {
            $.each(Members, function (index, value) {
                if (value.AccountInformation.AccountName.toUpperCase() == AccountName.toUpperCase()) {
                    flag = 1;
                }
            });
            if (flag == 1) {
                $("#notAvailable").show();
                //$("#createAccount").attr("disabled", "disabled");
            }
            else {
                $("#available").show();
                IsAvaalableForCreate = true;
                //$("#createAccount").removeAttr("disabled");
            }
            $("#availabilityCheckIcon").hide();



        }, 1500);
    }
    else {
        $("#availabilityCheckIcon").show();
        setTimeout(function () {
            $("#availabilityCheckIcon").hide();
            $("#noValue").show();
        }, 1500);
    }
});
//-------------------------------------------------------------------

//Account Admin
$("#addNewAccountAdminBtn").click(function () {
    $("#addNewAccountAdmin").show()
});
$("#cancelAddNewAccountAdminBtn").click(function () {
    $("#addNewAccountAdmin").hide()
    $('#AdminEmail').val("")
    $('#AdminInput').val("")
    $('#AdminPhone').val("")
});
$("#AccountName").keyup(function () {
    //$("#createAccount").attr("disabled", "disabled");
    $("#noValue").hide();
    $("#availabilityCheck").show();
    $("#notAvailable").hide();
    $("#available").hide();
    $("#availabilityCheckIcon").hide();
});
$(".cancelcreateAccount").click(function () {

    $("#AddAccountModal").modal('hide')
    $("#mapAccount").hide();
    $("#mapAccountIcon").addClass("fa-chevron-down")
    $("#mapAccountIcon").removeClass("fa-chevron-up")
    $("#AccountName").val("")
    $("#Email").val("")
    $("#ConfirmEmail").val("")
    $("#ReportTo").val("")
    $("#Reportee").val("")
    $("#accType").val("CHOOSE TYPE")
    $("#noValue").hide();
    $("#notAvailable").hide();
    $("#available").hide();
    $("#availabilityCheckIcon").hide();
    $("#availabilityCheck").show();
});
function CreateAccount() {
    var AccountName = $("#AccountName").val()
    var ReportTo = $("#ReportTo").val()
    var Reportee = $("#Reportee").val()
    var AccountType = $("#accType").val()
    var AccountID = Math.floor(1000 + Math.random() * 9000);
    //var AccountInformation = { "AccountName": AccountName, "AccountType": AccountType, "Reportee": Reportee, "ReportTo": ReportTo, "Status": "Active", "AccountID": AccountID, "LastModifiedBy": "Barbara", "LastModifiedOn": "10/12/2016", "CreatedBy": "Barbara", "CreatedOn": "01/12/2016" }
    //Members.push(AccountInformation);
    // console.log(Members)
  //  if (IsAvaalableForCreate) {
        IsAvaalableForCreate = false;
        //$('#AccountListTableData').append('<tr>' +
        //          '<td>' + Math.floor(1000 + Math.random() * 9000) + '</td>' +
        //          '<td>' + AccountName + '</td>' +
        //          '<td>' + AccountType + '</td>' +
        //          '<td>' + ReportTo + '</td>' +
        //          '<td>' + Reportee + '</td>' +
        //          '<td> Active' +
        //          '</td>' +
        //   '<td><a class="btn btn-primary btn-xs" data-toggle="tooltip" data-placement="bottom" title="View Account"  onclick="ViewAccount(\'' + AccountID + '\')" ><i class="fa fa-eye"></i> View</a>' + ' &nbsp;&nbsp;' +
        //            '<a class="btn btn-primary btn-xs" data-toggle="tooltip" data-placement="bottom" title="Edit Account" onclick="EditAccount(\'' + AccountID + '\')" ><i class="fa fa-edit"></i> Edit</a>' + ' &nbsp;&nbsp;' +
        //            '<a class="btn btn-primary btn-xs" data-toggle="tooltip" data-placement="bottom" title="Deactivate Account"  onclick="DeactivateModal(\'' + AccountID + '\')" ><i class="fa fa-close"></i> Deactivate</a>' + '</td>' +

        //   '</tr>')
        new PNotify({
            title: "Created Successfully",
            text: 'Account ' + AccountName + ' Created Successfully',
            type: 'success',
            width: "22%",
            animate: {
                animate: true,
                in_class: "lightSpeedIn",
                out_class: "slideOutRight"
            }
        });
        $('[data-toggle="tooltip"]').tooltip();
        $("#mapAccount").hide();
        $("#mapAccountIcon").addClass("fa-chevron-down")
        $("#mapAccountIcon").removeClass("fa-chevron-up")
        $("#AccountName").val("")
        $("#Email").val("")
        $("#ConfirmEmail").val("")
        $("#ReportTo").val("")
        $("#Reportee").val("")
        $("#accType").val("ORGANIZATION")
        $("#noValue").hide();
        $("#notAvailable").hide();
        $("#available").hide();
        $("#availabilityCheckIcon").hide();
        $("#availabilityCheck").show();
        $("input:checkbox.switchCss").bootstrapSwitch();
        $("#AddAccountModal").modal('hide')
        //$("#createAccount").attr("disabled", "disabled");
 //   }
}
$('#map').change(function () {
    if ($(this).is(':checked')) {
        $("#mapAccount").show();
        $("#mapAccountIcon").removeClass("fa-chevron-down")
        $("#mapAccountIcon").addClass("fa-chevron-up")
    } else {
        $("#mapAccount").hide();
        $("#mapAccountIcon").addClass("fa-chevron-down")
        $("#mapAccountIcon").removeClass("fa-chevron-up")
    }
});
//-------------------------------------------------------------------
//backto acc list

//Adding/Removing Multiple Reportees for Account
function AddNewReporteeRow() {
    $('#mapAccount').append(
        '<div class="col-lg-12 accmaps"> <br /><div class="col-lg-3"></div><div class="col-lg-8"><span class="col-lg-6 zero-padding-left"><input type="text" list="ChildAccounts"  placeholder="REPORTEE ACCOUNT" class="form-control input-xs"></span><span class="col-lg-6"> <a class="red-button btn" onclick="RemoveReporteeRow(this)"><i class="fa fa-close margin_top_6px"></i></a> </span> </div></div>');
}
function AddNewReportToRow() {
    $('#ReportToMapping').append(
        '<div class="col-lg-12 accmaps"> <br /><div class="col-lg-3"></div><div class="col-lg-8"><span class="col-lg-6 zero-padding-left"><input type="text"list="AdminAccounts" placeholder="REPORT TO ACCOUNT" class="form-control input-xs"></span><span class="col-lg-6"> <a class="red-button btn" onclick="RemoveReporteeRow(this)"><i class="fa fa-close margin_top_6px"></i></a> </span> </div></div>');
}
function RemoveReporteeRow(id) {
    $(id).parent().parent().parent("div").remove();
}
// ------------------------------------------------------------------

//Checking for Existing User 
var UserFound = false;
$("#AdminInput").on('input', function () {
    $('#UserFound').show()
    $('#UserNotFound').hide()
    var val = this.value;
    if ($('#AllUsers option').filter(function () {
        return this.value === val;
    }).length) {
        var data = this.value;
        $.each(Members, function (index, value) {
            if ((value.PersonalInformation.FirstName + ' - ' + value.PersonalInformation.LastName).toUpperCase() == data.toUpperCase()) {
                $('#AdminEmail').val(value.ContactInformation.email.toUpperCase())
                $('#AdminPhone').val(value.ContactInformation.ContactNumber.toUpperCase())
                UserFound = true;
                $('#UserFound').show()
                $('#UserNotFound').hide()
            }

        });

    }
    else {
        $('#AdminEmail').val("")
        $('#AdminPhone').val("")
        UserFound = false;
    }
    if (!UserFound) {
        $('#UserFound').hide()
        $('#UserNotFound').show()
    }
});
//--------------------------------------------------------------------
// Adding Admin User For Account
function AddNewAdminUser() {
    AddUserModal();
}
function AddAdminUser() {
    $("#accountAdmins").append(
        '<tr><td>' + $("#AdminInput").val() + '</td><td>' + $("#AdminEmail").val() + '</td><td>' + $("#AdminPhone").val() + '</td><td><a class="btn btn-xs btn-primary" style="" onclick="ViewUser(\'1\')"><i class="fa fa-eye"></i> View</a> <a class="btn btn-xs btn-primary" style=""  onclick="EditUser(\'1\')"><i class="fa fa-edit"></i> Edit</a></td></tr>'
        )
    new PNotify({
        title: "Admin Added Successfully",
        text: 'Admin ' + $("#AdminInput").val() + ' Added Successfully',
        type: 'success',
        width: "22%",
        animate: {
            animate: true,
            in_class: "lightSpeedIn",
            out_class: "slideOutRight"
        }
    });
    $("#addNewAccountAdmin").hide()
    $('#AdminEmail').val("")
    $('#AdminInput').val("")
    $('#AdminPhone').val("")
}
//--------------------------------------------------------------------
function BackToAccounts() {
    AccountsActiveTab()
}
//Proceed To B.P
var BusinessProcesses = [];

$("#BPProceedBtn").click(function () {
    $("#TopMenu").show();
    $(".AddNewUser").hide();
    $("#ViewAccountData").hide();
    $("#EditAccountData").hide();
    $("#ViewDepartmentsData").hide();
    $("#ViewUsersData").hide();
    $("#accounts").removeClass("active");
    $("#teams").parent().removeClass("work_menu_special_item")
    $("#users").parent().removeClass("work_menu_special_item")
    $("#accounts").parent().removeClass("work_menu_special_item")
    $("#businessProcesses").parent().addClass("work_menu_special_item")
    $("#departments").parent().removeClass("work_menu_special_item")
    $("#userroles").parent().removeClass("work_menu_special_item")
    $("#ViewUserRolesData").hide();
    $("#userroles").removeClass("active");
    $("#departments").removeClass("active");
    $("#businessProcesses").addClass("active");
    $("#claimsManagement").addClass("active");
    $("#users").removeClass("active");
    $("#BusinessProcessesData").show();
    $("#AccountsListData").hide();
    $("#AddNewAccountBtn").hide();
    $(".BusinessProcessesTopMenuR").css({ "opacity": "0.3" });
    $(".BusinessProcessesTopMenuL").css({ "opacity": "0.3", "box-shadow": "0px 0px 0px" });
    $(".AccountsTopMenuR").css({ "opacity": "1" });
    $(".AccountsTopMenuL").css({ "opacity": "1", "box-shadow": "5px 5px 10px" });
    $("#AllBPs").show();
    $("#AllAccs").hide();
    $("#AllDepts").show();
    $("#AllDeptsDropdown").hide();
    $("#AllBPsDropdown").hide();
    $("#AllAccsDropdown").show();
    $.ajax({
        url: '/Areas/AM/Resources/JSONData/BusinessProcessesData.js',
        success: function (response) {
            BusinessProcesses = JSON.parse(response);
          
            $('#BPFloatMenu').empty();
            $('#BPFloatMenu').append('<li class="outer_menu_item_top">' +
             '<div class="inner-addon right-addon"  style="margin-top: 8px;margin-bottom: 8px;">' +
                    '<i class="fa fa-search"></i>' +
                    '<input type="text" class="form-control input-xs" placeholder="SEARCH ..." />' +
                '</div>');
            $('#BusinessProcessAppendData').empty();
            $('#BusinessProcessAppendData').append(' <li><input class="form-control input-xs" placeholder="SEARCH BUSINESS PROCESS" style="padding:11px"/></li>');

            $.each(BusinessProcesses, function (index, value) {
                $('#BPFloatMenu').append(
               '<li class="outer_menu_item_top"><a class="tab-navigation member-actions BPTab"  onclick="GoTOBP(\'' + value.ID + '\')"  style="padding:10px" id="' + value.ID + '"><span class="text_beside_icon">' + value.BusinessProcess + '</span><span class="label_on_collapsed floating_menu_label">' + value.BusinessProcess + '</span><div class="pull-right ' + value.ID + '" hidden style="margin-top:1px"><input type="radio" class="normal-radio" checked="checked"/><label><span></span></label></div></a></li>'
      );
                
                $('#BusinessProcessAppendData').append(
              ' <li><a href="#" class="' + value.ID + ' "  style="padding:11px" onclick="GoTOBP(\'' + value.ID + '\')" >' + value.BusinessProcess + '<div class="pull-right ' + value.ID + '" style="margin-top: -3px;" hidden><input type="radio" class="normal-radio" checked="checked"/><label><span></span></label></div></a></li> '
     );
                if (index == 0)
                    $("#" + value.ID).addClass("active")
            });
        }
    });

});
//--------------------------------------------------------------------


//-------------------------------------------E N D   A C C O U N T S------------------------------------------------------------

//-------------------------------------------D E P A R T M E N T S------------------------------------------------

//Ajax For Departments Data
var DepartmentsData = [];
$.ajax({
    url: '/Areas/AM/Resources/JSONData/DepartmentsData.js',
    success: function (response) {
        DepartmentsData = JSON.parse(response);
        $('#DepartmentsTableData').empty();
        
        $("#teamDepts").empty();
        $('#DepartmentsAppendData').empty();
        $('#DepartmentsAppendData').append(' <li><input class="form-control input-xs" placeholder="SEARCH DEPARTMENT" style="padding:11px"/></li>');
        $.each(DepartmentsData, function (index, value) {
            $('#DepartmentsTableData').append(
                 '<tr>' +
                      '<td class="theme_label_data">' + value.ID + '</td>' +
                      '<td class="theme_label_data">' + value.Name + '</td>' +
                      '<td class="theme_label_data">' +
                      '<div id="BP' + value.ID + '">' +
                      '</div>' +
                      '</td>' +
                      '<td class="theme_label_data">' + value.CreatedOn + '</td>' +
                      '<td class="text-center"><button class="btn btn-primary btn-xs"  onclick="ViewDepartMent(\'' + value.ID + '\')" ><i class="fa fa-eye"></i> View</button> &nbsp;&nbsp;<button class="btn btn-primary btn-xs"  onclick="EditDepartMent(\'' + value.ID + '\')" ><i class="fa fa-edit"></i> Edit</button> &nbsp;&nbsp;<button class="btn btn-success btn-xs"  onclick="GoToTeam(\'' + value.Name + '\')" > <i class="fa fa-arrow-circle-right"></i> Proceed</button>' +
                      '</td>' +
                '</tr>'
              );
            $("#teamDepts").append(
      '<option value="' + value.Name + '" >' + value.Name + '</option>'
          )
           
            $('#DepartmentsAppendData').append(
      ' <li><a href="#" class="' + value.ID + ' "  style="padding:11px" onclick="GoTODept(\'' + value.Name + '\')" >' + value.Name + '</a></li> '
        );
            var data="";
            $.each(value.BusinessProcesses, function (i, val) {
                if (value.BusinessProcesses.length > 1)
                {
                    data += val+"  ";
                }
                else
                $("#BP" + value.ID).append('<span data-container="body" data-toggle="popover" title="BUSINESS PROCESS" data-trigger="hover" data-content="' + val + '">' + val + ' </span>');
            })
            $("#BP" + value.ID).append('<span data-container="body" data-toggle="popover" title="BUSINESS PROCESS" data-trigger="hover" data-content="' + data + '">' + data.trimToLength(40) + ' </span>');
            $("[data-toggle=popover]").popover();
        });
        $("#teamDepts").multiselect({
            
            placeholder: 'Select Departments',
            column: 1,
            selectAll: true
            //enableFiltering: true

        });
    }
});
//----------------------------------------------------------------------------

function GoToTeam(name) {
    $("#topMenuDeptText").text(name)
    $("#TopMenu").show();
    $("#teams").parent().addClass("work_menu_special_item")
    $("#users").parent().removeClass("work_menu_special_item")
    $("#departments").parent().removeClass("work_menu_special_item")
    $("#userroles").parent().removeClass("work_menu_special_item")
    $("#accounts").parent().removeClass("work_menu_special_item")
    $("#businessProcesses").parent().removeClass("work_menu_special_item")
    $("#businessProcesses").removeClass("active");
    $("#accounts").removeClass("active");
    $("#userroles").removeClass("active");
    $("#departments").removeClass("active");
    $("#users").removeClass("active");
    $("#teams").addClass("active");
    $("#AccountsListData").hide();
    $("#BusinessProcessesData").hide();
    $(".AddNewUser").hide();
    $("#ViewAccountData").hide();
    $("#EditAccountData").hide();
    $("#ViewDepartmentsData").hide();
    $("#ViewUserRolesData").hide();
    $("#ViewUsersData").hide();
    $("#ViewTeamsData").show();

    $("#AllTeams").show();
    $("#AllTeamsDropdown").hide();
    $(".TeamsTopMenuR").css({ "opacity": "0.3" });
    $(".TeamsTopMenuL").css({ "opacity": "0.3", "box-shadow": "0px 0px 0px" });
    $("#AllDepts").hide();
    $("#AllDeptsDropdown").show();
    $(".DepartmentsTopMenuR").css({ "opacity": "1" });
    $(".DepartmentsTopMenuL").css({ "opacity": "1", "box-shadow": "5px 5px 10px" });

}
function GoTODept(name)
{
    $("#topMenuDeptText").text(name)
}
//Side Menu Department Click
$("#departments").click(function () {
    $("#TopMenu").show(); 
    $("#topMenuBPText").text("All");
    $("#topMenuAccText").text("All");
    $(".AddNewUser").hide();
    $("#teams").parent().removeClass("work_menu_special_item")
    $("#users").parent().removeClass("work_menu_special_item")
    $("#departments").parent().addClass("work_menu_special_item")
    $("#userroles").parent().removeClass("work_menu_special_item")
    $("#accounts").parent().removeClass("work_menu_special_item")
    $("#businessProcesses").parent().removeClass("work_menu_special_item")
    $("#businessProcesses").removeClass("active");
    $("#accounts").removeClass("active");
    $("#IndividualUserView").hide();
    $("#ViewTeamsData").hide();
    $("#viewUserDetails").hide();
    $("#editUserDetails").hide();
    $("#ViewUserRolesData").hide();
    $("#userroles").removeClass("active");
    $("#teams").removeClass("active");
    $("#departments").addClass("active");
    $("#users").removeClass("active");
    $("#BusinessProcessesData").hide();
   
    $("#AccountsListData").hide();
    $("#ViewUsersData").hide();
    $("#AddNewAccountBtn").hide();
    $("#ViewAccountData").hide();
    $("#EditAccountData").hide();
    $(".AccountsTopMenuL").css({ "opacity": "1", "box-shadow": "5px 5px 10px" });
    $(".AccountsTopMenuR").css({ "opacity": "1" });
    $("#AllAccs").hide();
    $("#AllAccsDropdown").show();
    $("#AllDepts").show();
    $("#AllDeptsDropdown").hide();
    $(".BusinessProcessesTopMenuR").css({ "opacity": "1" });
    $(".BusinessProcessesTopMenuL").css({ "opacity": "1", "box-shadow": "5px 5px 10px" });
    $("#AllBPs").hide();
    $("#AllBPsDropdown").show();
    $(".DepartmentsTopMenuR").css({ "opacity": "0.3" });
    $(".DepartmentsTopMenuL").css({ "opacity": "0.3", "box-shadow": "0px 0px 0px" });
    $("#AllTeams").show();
    $("#AllTeamsDropdown").hide();
    $(".TeamsTopMenuR").css({ "opacity": "0.3" });
    $(".TeamsTopMenuL").css({ "opacity": "0.3", "box-shadow": "0px 0px 0px" });
    $("#ViewDepartmentsData").show();

    
   
});
//---------------------------------------------------------------------------------

//Proceeding to Departments from BP
function DepartmentsInitial() {
    $("#departments").parent().addClass("work_menu_special_item")
    $("#businessProcesses").parent().removeClass("work_menu_special_item")
    $("#businessProcesses").removeClass("active");
    $("#accounts").removeClass("active");
    $(".BusinessProcessesTopMenuR").css({ "opacity": "1" });
    $(".BusinessProcessesTopMenuL").css({ "opacity": "1", "box-shadow": "5px 5px 10px" });
    $("#AllBPs").hide();
    $("#AllBPsDropdown").show();
    $("#ViewUserRolesData").hide();
    $("#userroles").removeClass("active");
    $("#teams").removeClass("active");
    $("#departments").addClass("active");
    $("#users").removeClass("active");
    $("#BusinessProcessesData").hide();
    $("#AccountsListData").hide();
    $("#AddNewAccountBtn").hide();
    $("#ViewAccountData").hide();
    $("#EditAccountData").hide();
    $("#AllDepts").show();
    $("#AllDeptsDropdown").hide();
    $(".DepartmentsTopMenuR").css({ "opacity": "0.3" });
    $(".DepartmentsTopMenuL").css({ "opacity": "0.3", "box-shadow": "0px 0px 0px" });



    $("#ViewDepartmentsData").show();
}
$(".ProceedBtn").click(function () {
    DepartmentsInitial();
});
//---------------------------------------------------------------------------------


//Modal for Adding New Department
function AddDepartmentModal() {
    //  $("#AddDepartmentModal").modal('hide')
    //  $("#mapDepartmentIcon").addClass("fa-chevron-down")
    //  $("#mapDepartmentIcon").removeClass("fa-chevron-up")
    $("#deptName").val("")
    $(".select2-search-choice").remove()
    $('#deptTeam').val("")
    $('#deptDate').val("")
    $("#deptIDField").hide();
    $("#addDepartmentTitle").show();
    $("#editDepartmentTitle").hide();
    $("#addDepartmentBtn").show();
    $("#editDepartmentBtn").hide();
    $('#deptBPs').empty();
  
    $.each(BusinessProcesses, function (index, value) {
        $('#deptBPs').append(
             '<option value="' + value.BusinessProcess.toUpperCase() + '">' + value.BusinessProcess.toUpperCase() + '</option>'
        );
    });
    $.each(AccountsData, function(index, value) {
            $("#ListAllAccount").append(
       '<option value="' +value.AccountName.toUpperCase() + '" >' +value.AccountName.toUpperCase() + '</option>'
           )
    });
    $("#ListAllAccount").multiselect({
        placeholder: "Account Name",
        selectAll: true,
        column: 1
        //enableFiltering: true

    });
    $("#deptBPs").multiselect({
        placeholder: "Bussiness Process",
        selectAll: true,
        column:1
        //enableFiltering: true

    });
    $("#AddDepartmentModal").modal('show')
    //  $("#deptID").val(Math.floor(1000 + Math.random() * 9000))
}
//----------------------------------------------------------------------------

//View/Add/Edit/Update/Cancel Department
function ViewDepartMent(idvalue) {
    $.each(DepartmentsData, function (index, value) {
        if (value.ID == idvalue) {
            $("#departmentTitle").text(value.Name.toUpperCase())
            $('#viewDeptID').text(value.ID.toUpperCase())
            $("#viewDeptName").text(value.Name.toUpperCase())
            $('#viewDeptBPs').empty();
            $.each(value.BusinessProcesses, function (i, v) {
                $('#viewDeptBPs').append('<span>' + v.toUpperCase() + '</span>' +
                    '<br>');

            });
        }
    });
    $("#ViewDepartmentModal").modal('show')
}

function EditDepartMent(idvalue) {
    //  $("#mapDepartmentIcon").removeClass("fa-chevron-down")
    //   $("#mapDepartmentIcon").addClass("fa-chevron-up")
    $("#addDepartmentTitle").hide();
    $("#editDepartmentTitle").show();
    $("#addDepartmentBtn").hide();
    $("#editDepartmentBtn").show();
    $("#deptIDField").show();

    $('#deptBPs').empty();
    $.each(BusinessProcesses, function (index, value) {
        $('#deptBPs').append(
             '<option value="' + value.BusinessProcess.toUpperCase() + '">' + value.BusinessProcess.toUpperCase() + '</option>'
        );
    });
    $.each(DepartmentsData, function (index, value) {
        var selectedBPValues = [];
        if (value.ID == idvalue) {
            $("#deptName").val(value.Name.toUpperCase())
            $('#deptID').val(value.ID.toUpperCase())
            $.each(value.BusinessProcesses, function (i, v) {
                $('#deptBPs option').filter('[value="' + v.toUpperCase() + '"]').prop('selected', true);
            });
            //$('#deptBPs').select2("val", selectedBPValues)
        }
    });

    $("#AddDepartmentModal").modal('show')
}

function UpdateDepartment() {
    new PNotify({
        title: "Department Updated Successfully",
        //  text: 'Department ' + $("#deptName").val() + ' Added Successfully',
        type: 'success',
        width: "25%",
        animate: {
            animate: true,
            in_class: "lightSpeedIn",
            out_class: "slideOutRight"
        }
    });
    $("#AddDepartmentModal").modal('hide')
}

function AddNewDepartment() {
    //var DeptId = Math.floor(1000 + Math.random() * 9000);
    //$("#DepartmentsTableData").append(
    //    '<tr><td class="theme_label_data">' + DeptId + '</td><td class="theme_label_data">' + $("#deptName").val() + '</td><td class="theme_label_data">' + $("#deptBPs").val() + '</td><td class="theme_label_data">' + $("#deptDate").val() + '</td><td class="text-center"><button class="btn btn-primary btn-xs"  onclick="ViewDepartMent(\'' + DeptId + '\')" ><i class="fa fa-eye"></i> View</button> &nbsp;&nbsp;<button class="btn btn-primary btn-xs"  onclick="EditDepartMent(\'' + DeptId + '\')" ><i class="fa fa-edit"></i> Edit</button></td></tr>'
    //    )
    new PNotify({
        title: "Department Added Successfully",
        //text: 'Department ' + $("#deptName").val() + ' Added Successfully',
        type: 'success',
        width: "24%",
        animate: {
            animate: true,
            in_class: "lightSpeedIn",
            out_class: "slideOutRight"
        }
    });
    $("#AddDepartmentModal").modal('hide')

    $("#deptName").val("")
    $(".select2-search-choice").remove()
    $('#deptTeam').val("")
    $('#deptDate').val("")
}

function CancelAddNewDepartment() {
    $("#AddDepartmentModal").modal('hide')

    $("#deptName").val("")
    $(".select2-search-choice").remove()
    $('#deptTeam').val("")
    $('#deptDate').val("")
}
// ------------------------------------------------------------------


//-------------------------------------------E N D   D E P A R T M E N T S ------------------------------------------------------------

//-------------------------------------------B U S I N E S S  P R O C E S S E S ------------------------------------------------
//Ajax for Business Processes Data

$.ajax({
    url: '/Areas/AM/Resources/JSONData/BusinessProcessesData.js',
    success: function (response) {
        BusinessProcesses = JSON.parse(response);
        $('.RoleBPs').empty();
        $("#BPListForNewTeam").empty();
        $("#BPListForNewTeam").append('<option selected disabled>SELECT BUSINESS PROCESS</option>');
        $('.RoleBPs').append(
      '<option selected disabled>SELECT BUSINESS PROCESS</option>'
 );
        $('#BPFloatMenu').empty();
        $('#BPFloatMenu').append('<li class="outer_menu_item_top">' +
             '<div class="inner-addon right-addon" style="margin-top: 8px;margin-bottom: 8px;">' +
                    '<i class="fa fa-search"></i>' +
                    '<input type="text" class="form-control input-xs" placeholder="SEARCH ..." />' +
                '</div>');
        $('#BusinessProcessAppendData').empty();
        $('#BusinessProcessAppendData').append(' <li><input  class="form-control input-xs" placeholder="SEARCH BUSINESS PROCESS" style="padding:11px"/></li>');

        $.each(BusinessProcesses, function (index, value) {
            $('#BPListForNewTeam').append(
              '<option value="' + value.BusinessProcess.toUpperCase() + '">' + value.BusinessProcess.toUpperCase() + '</option>'
     );
            $('.RoleBPs').append(
         '<option value="' + value.BusinessProcess.toUpperCase() + '">' + value.BusinessProcess.toUpperCase() + '</option>'
    );
            $('#BPFloatMenu').append(
           '<li class="outer_menu_item_top"><a class="tab-navigation member-actions BPTab"  onclick="GoTOBP(\'' + value.ID + '\')"  style="padding:10px" id="' + value.ID + '"><span class="text_beside_icon">' + value.BusinessProcess + '</span><span class="label_on_collapsed floating_menu_label">' + value.BusinessProcess + '</span><div class="pull-right ' + value.ID + '" hidden style="margin-top:1px"><input type="radio" class="normal-radio" checked="checked"/><label><span></span></label></div></a></li>'
     //      '<li class="outer_menu_item_top"><a class="tab-navigation row member-actions"  onclick="GoTOBP(\'' + value.ID + '\')"  style="width: 100%;margin-left:initial;padding:11px" id="' + value.ID + '"><span class="text_beside_icon">' + value.BusinessProcess + '</span><span class="label_on_collapsed floating_menu_label">' + value.BusinessProcess + '</span><span class="pull-right ' + value.ID + '" hidden style="margin-top:-3px"><input type="radio" class="normal-radio" checked="checked"/><label><span></span></label></span></a></li>'
             );
            $('#BusinessProcessAppendData').append(
          ' <li><a href="#" class="' + value.ID + ' "  style="padding:11px" onclick="GoTOBP(\'' + value.ID + '\')" >' + value.BusinessProcess + '<div class="pull-right ' + value.ID + '" style="margin-top: -3px;" hidden><input type="radio" class="normal-radio" checked="checked"/><label><span></span></label></div></a></li> '
            );
            //   $('#deptBPs').append(
            //' <option value="' + value.ID + '">' + value.BusinessProcess + '</option>'
            //   );
            if (index == 0)
                $("#" + value.ID).addClass("active")
        });
    }
});
//-------------------------------------------------------------------

//BP Side Menu Click
$("#businessProcesses").click(function () {
    $(".AddNewUser").hide();
    $("#TopMenu").show();
    $("#teams").parent().removeClass("work_menu_special_item")
    $("#users").parent().removeClass("work_menu_special_item")
    $("#accounts").parent().removeClass("work_menu_special_item")
    $("#businessProcesses").parent().addClass("work_menu_special_item")
    $("#departments").parent().removeClass("work_menu_special_item")
    $("#userroles").parent().removeClass("work_menu_special_item")
    if (!$("#businessProcesses").hasClass("active")) {
        if (!$("#topMenuAccText").text() || $("#topMenuAccText").text() == " ") {
            $("#topMenuAccText").text("All");
        }
        $("#IndividualUserView").hide();
        $("#viewUserDetails").hide();
        $("#editUserDetails").hide();
        $("#ViewAccountData").hide();
        $("#EditAccountData").hide();
        $("#ViewDepartmentsData").hide();
        $("#ViewUsersData").hide();
        //$("#topMenuBPText").text("Claims Management")
        $("#accounts").removeClass("active");
        $("#ViewTeamsData").hide();
        $("#ViewUserRolesData").hide();
        $("#userroles").removeClass("active");
        $("#teams").removeClass("active");
        $("#departments").removeClass("active");
        $("#businessProcesses").addClass("active");
        $("#claimsManagement").addClass("active");
        $("#users").removeClass("active");
        $("#BusinessProcessesData").show();
        $("#AccountsListData").hide();
        $("#AddNewAccountBtn").hide();
        $(".BusinessProcessesTopMenuR").css({ "opacity": "0.3" });
        $(".BusinessProcessesTopMenuL").css({ "opacity": "0.3", "box-shadow": "0px 0px 0px" });
        $(".AccountsTopMenuR").css({ "opacity": "1" });
        $(".AccountsTopMenuL").css({ "opacity": "1", "box-shadow": "5px 5px 10px" });
        $(".DepartmentsTopMenuR").css({ "opacity": "0.3" });
        $(".DepartmentsTopMenuL").css({ "opacity": "0.3", "box-shadow": "0px 0px 0px" });

        $("#AllTeams").show();
        $("#AllTeamsDropdown").hide();
        $(".TeamsTopMenuR").css({ "opacity": "0.3" });
        $(".TeamsTopMenuL").css({ "opacity": "0.3", "box-shadow": "0px 0px 0px" });

        $("#AllBPs").show();
        $("#AllAccs").hide();
        $("#AllDepts").show();
        $("#AllDeptsDropdown").hide();
        $("#AllBPsDropdown").hide();
        $("#AllAccsDropdown").show();

    }
});
//-------------------------------------------------------------------

//Admin Users for Each Business Processes
var ClaimsBPUserFound = false;
$("#ClaimsBPAdminInput").on('input', function () {
    $('#ClaimsBPUserFound').show()
    $('#ClaimsBPUserNotFound').hide()
    var val = this.value;
    if ($('#ClaimsBPUsers option').filter(function () {
        return this.value === val;
    }).length) {
        var data = this.value;
        $.each(Members, function (index, value) {
            if ((value.PersonalInformation.FirstName + ' - ' + value.PersonalInformation.LastName).toUpperCase() == data.toUpperCase()) {
                $('#ClaimsBPAdminEmail').val(value.ContactInformation.email.toUpperCase())
                $('#ClaimsBPAdminPhone').val(value.ContactInformation.ContactNumber.toUpperCase())
                ClaimsBPUserFound = true;
                $('#ClaimsBPUserFound').show()
                $('#ClaimsBPUserNotFound').hide()
            }

        });

    }
    else {
        $('#AdminEmail').val("")
        $('#AdminPhone').val("")
        ClaimsBPUserFound = false;
    }
    if (!ClaimsBPUserFound) {
        $('#ClaimsBPUserFound').hide()
        $('#ClaimsBPUserNotFound').show()
    }
});
var PMBPUserFound = false;
$("#PMBPAdminInput").on('input', function () {
    $('#PMBPUserFound').show()
    $('#PMBPUserNotFound').hide()
    var val = this.value;
    if ($('#PMBPUsers option').filter(function () {
        return this.value === val;
    }).length) {
        var data = this.value;
        $.each(Members, function (index, value) {
            if ((value.PersonalInformation.FirstName + ' - ' + value.PersonalInformation.LastName).toUpperCase() == data.toUpperCase()) {
                $('#PMBPAdminEmail').val(value.ContactInformation.email.toUpperCase())
                $('#PMBPAdminPhone').val(value.ContactInformation.ContactNumber.toUpperCase())
                PMBPUserFound = true;
                $('#PMBPUserFound').show()
                $('#PMBPUserNotFound').hide()
            }

        });

    }
    else {
        $('#AdminEmail').val("")
        $('#AdminPhone').val("")
        PMBPUserFound = false;
    }
    if (!PMBPUserFound) {
        $('#PMBPUserFound').hide()
        $('#PMBPUserNotFound').show()
    }
});
function AddPMBPAdminUser() {
    $("#PMBPAdmins").append(
        '<tr><td>' + $("#PMBPAdminInput").val() + '</td><td>' + $("#PMBPAdminEmail").val() + '</td><td>' + $("#PMBPAdminPhone").val() + '</td><td><a class="btn btn-xs btn-primary" style=""><i class="fa fa-eye"></i> View</a> <a class="btn btn-xs btn-primary" style=""><i class="fa fa-edit"></i> Edit</a></td></tr>'
        )
    new PNotify({
        title: "Admin Added Successfully",
        text: 'Admin ' + $("#PMBPAdminInput").val() + ' Added Successfully',
        type: 'success',
        animate: {
            animate: true,
            in_class: "lightSpeedIn",
            out_class: "slideOutRight"
        }
    });
    $("#addNewPMBPAdmin").hide()
    $('#PMBPAdminEmail').val("")
    $('#PMBPAdminInput').val("")
    $('#PMBPAdminPhone').val("")
}
$("#addNewPMBPAdminBtn").click(function () {
    $("#addNewPMBPAdmin").show()
});
$("#cancelAddNewPMBPAdminBtn").click(function () {
    $("#addNewPMBPAdmin").hide()
    $('#PMBPAdminEmail').val("")
    $('#PMBPAdminInput').val("")
    $('#PMBPAdminPhone').val("")
});
var BMBPUserFound = false;
$("#BMBPAdminInput").on('input', function () {
    $('#BMBPUserFound').show()
    $('#BMBPUserNotFound').hide()
    var val = this.value;
    if ($('#BMBPUsers option').filter(function () {
        return this.value === val;
    }).length) {
        var data = this.value;
        $.each(Members, function (index, value) {
            if ((value.PersonalInformation.FirstName + ' - ' + value.PersonalInformation.LastName).toUpperCase() == data.toUpperCase()) {
                $('#BMBPAdminEmail').val(value.ContactInformation.email.toUpperCase())
                $('#BMBPAdminPhone').val(value.ContactInformation.ContactNumber.toUpperCase())
                BMBPUserFound = true;
                $('#BMBPUserFound').show()
                $('#BMBPUserNotFound').hide()
            }

        });

    }
    else {
        $('#AdminEmail').val("")
        $('#AdminPhone').val("")
        BMBPUserFound = false;
    }
    if (!BMBPUserFound) {
        $('#BMBPUserFound').hide()
        $('#BMBPUserNotFound').show()
    }
});
function AddBMBPAdminUser() {
    $("#BMBPAdmins").append(
        '<tr><td>' + $("#BMBPAdminInput").val() + '</td><td>' + $("#BMBPAdminEmail").val() + '</td><td>' + $("#BMBPAdminPhone").val() + '</td><td><a class="btn btn-xs btn-primary" style=""><i class="fa fa-eye"></i> View</a> <a class="btn btn-xs btn-primary" style=""><i class="fa fa-edit"></i> Edit</a></td></tr>'
        )
    new PNotify({
        title: "Admin Added Successfully",
        text: 'Admin ' + $("#BMBPAdminInput").val() + ' Added Successfully',
        type: 'success',
        width: "22%",
        animate: {
            animate: true,
            in_class: "lightSpeedIn",
            out_class: "slideOutRight"
        }
    });
    $("#addNewBMBPAdmin").hide()
    $('#BMBPAdminEmail').val("")
    $('#BMBPAdminInput').val("")
    $('#BMBPAdminPhone').val("")
}
$("#addNewBMBPAdminBtn").click(function () {
    $("#addNewBMBPAdmin").show()
});
$("#cancelAddNewBMBPAdminBtn").click(function () {
    $("#addNewBMBPAdmin").hide()
    $('#BMBPAdminEmail').val("")
    $('#BMBPAdminInput').val("")
    $('#BMBPAdminPhone').val("")
});
var CapitationBPUserFound = false;
$("#CapitationBPAdminInput").on('input', function () {
    $('#CapitationBPUserFound').show()
    $('#CapitationBPUserNotFound').hide()
    var val = this.value;
    if ($('#CapitationBPUsers option').filter(function () {
        return this.value === val;
    }).length) {
        var data = this.value;
        $.each(Members, function (index, value) {
            if ((value.PersonalInformation.FirstName + ' - ' + value.PersonalInformation.LastName).toUpperCase() == data.toUpperCase()) {
                $('#CapitationBPAdminEmail').val(value.ContactInformation.email.toUpperCase())
                $('#CapitationBPAdminPhone').val(value.ContactInformation.ContactNumber.toUpperCase())
                CapitationBPUserFound = true;
                $('#CapitationBPUserFound').show()
                $('#CapitationBPUserNotFound').hide()
            }

        });

    }
    else {
        $('#AdminEmail').val("")
        $('#AdminPhone').val("")
        CapitationBPUserFound = false;
    }
    if (!CapitationBPUserFound) {
        $('#CapitationBPUserFound').hide()
        $('#CapitationBPUserNotFound').show()
    }
});
function AddCapitationBPAdminUser() {
    $("#CapitationBPAdmins").append(
        '<tr><td>' + $("#CapitationBPAdminInput").val() + '</td><td>' + $("#CapitationBPAdminEmail").val() + '</td><td>' + $("#CapitationBPAdminPhone").val() + '</td><td><a class="btn btn-xs btn-primary" style=""><i class="fa fa-eye"></i> View</a> <a class="btn btn-xs btn-primary" style=""><i class="fa fa-edit"></i> Edit</a></td></tr>'
        )
    new PNotify({
        title: "Admin Added Successfully",
        text: 'Admin ' + $("#CapitationBPAdminInput").val() + ' Added Successfully',
        type: 'success',
        animate: {
            animate: true,
            in_class: "lightSpeedIn",
            out_class: "slideOutRight"
        }
    });
    $("#addNewCapitationBPAdmin").hide()
    $('#CapitationBPAdminEmail').val("")
    $('#CapitationBPAdminInput').val("")
    $('#CapitationBPAdminPhone').val("")
}
$("#addNewCapitationBPAdminBtn").click(function () {
    $("#addNewCapitationBPAdmin").show()
});
$("#cancelAddNewCapitationBPAdminBtn").click(function () {
    $("#addNewCapitationBPAdmin").hide()
    $('#CapitationBPAdminEmail').val("")
    $('#CapitationBPAdminInput').val("")
    $('#CapitationBPAdminPhone').val("")
});
var DMBPUserFound = false;
$("#DMBPAdminInput").on('input', function () {
    $('#DMBPUserFound').show()
    $('#DMBPUserNotFound').hide()
    var val = this.value;
    if ($('#DMBPUsers option').filter(function () {
        return this.value === val;
    }).length) {
        var data = this.value;
        $.each(Members, function (index, value) {
            if ((value.PersonalInformation.FirstName + ' - ' + value.PersonalInformation.LastName).toUpperCase() == data.toUpperCase()) {
                $('#DMBPAdminEmail').val(value.ContactInformation.email.toUpperCase())
                $('#DMBPAdminPhone').val(value.ContactInformation.ContactNumber.toUpperCase())
                DMBPUserFound = true;
                $('#DMBPUserFound').show()
                $('#DMBPUserNotFound').hide()
            }

        });

    }
    else {
        $('#AdminEmail').val("")
        $('#AdminPhone').val("")
        DMBPUserFound = false;
    }
    if (!DMBPUserFound) {
        $('#DMBPUserFound').hide()
        $('#DMBPUserNotFound').show()
    }
});
function AddDMBPAdminUser() {
    $("#DMBPAdmins").append(
        '<tr><td>' + $("#DMBPAdminInput").val() + '</td><td>' + $("#DMBPAdminEmail").val() + '</td><td>' + $("#DMBPAdminPhone").val() + '</td><td><a class="btn btn-xs btn-primary" style=""><i class="fa fa-eye"></i> View</a> <a class="btn btn-xs btn-primary" style=""><i class="fa fa-edit"></i> Edit</a></td></tr>'
        )
    new PNotify({
        title: "Admin Added Successfully",
        text: 'Admin ' + $("#DMBPAdminInput").val() + ' Added Successfully',
        type: 'success',
        width: "22%",
        animate: {
            animate: true,
            in_class: "lightSpeedIn",
            out_class: "slideOutRight"
        }
    });
    $("#addNewDMBPAdmin").hide()
    $('#DMBPAdminEmail').val("")
    $('#DMBPAdminInput').val("")
    $('#DMBPAdminPhone').val("")
}
$("#addNewDMBPAdminBtn").click(function () {
    $("#addNewDMBPAdmin").show()
});
$("#cancelAddNewDMBPAdminBtn").click(function () {
    $("#addNewDMBPAdmin").hide()
    $('#DMBPAdminEmail').val("")
    $('#DMBPAdminInput').val("")
    $('#DMBPAdminPhone').val("")
});
var MMBPUserFound = false;
$("#MMBPAdminInput").on('input', function () {
    $('#MMBPUserFound').show()
    $('#MMBPUserNotFound').hide()
    var val = this.value;
    if ($('#MMBPUsers option').filter(function () {
        return this.value === val;
    }).length) {
        var data = this.value;
        $.each(Members, function (index, value) {
            if ((value.PersonalInformation.FirstName + ' - ' + value.PersonalInformation.LastName).toUpperCase() == data.toUpperCase()) {
                $('#MMBPAdminEmail').val(value.ContactInformation.email.toUpperCase())
                $('#MMBPAdminPhone').val(value.ContactInformation.ContactNumber.toUpperCase())
                MMBPUserFound = true;
                $('#MMBPUserFound').show()
                $('#MMBPUserNotFound').hide()
            }

        });

    }
    else {
        $('#AdminEmail').val("")
        $('#AdminPhone').val("")
        MMBPUserFound = false;
    }
    if (!MMBPUserFound) {
        $('#MMBPUserFound').hide()
        $('#MMBPUserNotFound').show()
    }
});
function AddMMBPAdminUser() {
    $("#MMBPAdmins").append(
        '<tr><td>' + $("#MMBPAdminInput").val() + '</td><td>' + $("#MMBPAdminEmail").val() + '</td><td>' + $("#MMBPAdminPhone").val() + '</td><td><a class="btn btn-xs btn-primary" style=""><i class="fa fa-eye"></i> View</a> <a class="btn btn-xs btn-primary" style=""><i class="fa fa-edit"></i> Edit</a></td></tr>'
        )
    new PNotify({
        title: "Admin Added Successfully",
        text: 'Admin ' + $("#MMBPAdminInput").val() + ' Added Successfully',
        type: 'success',
        width: "22%",
        animate: {
            animate: true,
            in_class: "lightSpeedIn",
            out_class: "slideOutRight"
        }
    });
    $("#addNewMMBPAdmin").hide()
    $('#MMBPAdminEmail').val("")
    $('#MMBPAdminInput').val("")
    $('#MMBPAdminPhone').val("")
}
$("#addNewMMBPAdminBtn").click(function () {
    $("#addNewMMBPAdmin").show()
});
$("#cancelAddNewMMBPAdminBtn").click(function () {
    $("#addNewMMBPAdmin").hide()
    $('#MMBPAdminEmail').val("")
    $('#MMBPAdminInput').val("")
    $('#MMBPAdminPhone').val("")
});
var UMBPUserFound = false;
$("#UMBPAdminInput").on('input', function () {
    $('#UMBPUserFound').show()
    $('#UMBPUserNotFound').hide()
    var val = this.value;
    if ($('#UMBPUsers option').filter(function () {
        return this.value === val;
    }).length) {
        var data = this.value;
        $.each(Members, function (index, value) {
            if ((value.PersonalInformation.FirstName + ' - ' + value.PersonalInformation.LastName).toUpperCase() == data.toUpperCase()) {
                $('#UMBPAdminEmail').val(value.ContactInformation.email.toUpperCase())
                $('#UMBPAdminPhone').val(value.ContactInformation.ContactNumber.toUpperCase())
                UMBPUserFound = true;
                $('#UMBPUserFound').show()
                $('#UMBPUserNotFound').hide()
            }

        });

    }
    else {
        $('#AdminEmail').val("")
        $('#AdminPhone').val("")
        UMBPUserFound = false;
    }
    if (!UMBPUserFound) {
        $('#UMBPUserFound').hide()
        $('#UMBPUserNotFound').show()
    }
});
function AddUMBPAdminUser() {
    $("#UMBPAdmins").append(
        '<tr><td>' + $("#UMBPAdminInput").val() + '</td><td>' + $("#UMBPAdminEmail").val() + '</td><td>' + $("#UMBPAdminPhone").val() + '</td><td><a class="btn btn-xs btn-primary" style=""><i class="fa fa-eye"></i> View</a> <a class="btn btn-xs btn-primary" style=""><i class="fa fa-edit"></i> Edit</a></td></tr>'
        )
    new PNotify({
        title: "Admin Added Successfully",
        text: 'Admin ' + $("#UMBPAdminInput").val() + ' Added Successfully',
        type: 'success',
        width: "22%",
        animate: {
            animate: true,
            in_class: "lightSpeedIn",
            out_class: "slideOutRight"
        }
    });
    $("#addNewUMBPAdmin").hide()
    $('#UMBPAdminEmail').val("")
    $('#UMBPAdminInput').val("")
    $('#UMBPAdminPhone').val("")
}
$("#addNewUMBPAdminBtn").click(function () {
    $("#addNewUMBPAdmin").show()
});
$("#cancelAddNewUMBPAdminBtn").click(function () {
    $("#addNewUMBPAdmin").hide()
    $('#UMBPAdminEmail').val("")
    $('#UMBPAdminInput').val("")
    $('#UMBPAdminPhone').val("")
});
var CMBPUserFound = false;
$("#CMBPAdminInput").on('input', function () {
    $('#CMBPUserFound').show()
    $('#CMBPUserNotFound').hide()
    var val = this.value;
    if ($('#CMBPUsers option').filter(function () {
        return this.value === val;
    }).length) {
        var data = this.value;
        $.each(Members, function (index, value) {
            if ((value.PersonalInformation.FirstName + ' - ' + value.PersonalInformation.LastName).toUpperCase() == data.toUpperCase()) {
                $('#CMBPAdminEmail').val(value.ContactInformation.email.toUpperCase())
                $('#CMBPAdminPhone').val(value.ContactInformation.ContactNumber.toUpperCase())
                CMBPUserFound = true;
                $('#CMBPUserFound').show()
                $('#CMBPUserNotFound').hide()
            }

        });

    }
    else {
        $('#AdminEmail').val("")
        $('#AdminPhone').val("")
        CMBPUserFound = false;
    }
    if (!CMBPUserFound) {
        $('#CMBPUserFound').hide()
        $('#CMBPUserNotFound').show()
    }
});
function AddCMBPAdminUser() {
    $("#CMBPAdmins").append(
        '<tr><td>' + $("#CMBPAdminInput").val() + '</td><td>' + $("#CMBPAdminEmail").val() + '</td><td>' + $("#CMBPAdminPhone").val() + '</td><td><a class="btn btn-xs btn-primary" style=""><i class="fa fa-eye"></i> View</a> <a class="btn btn-xs btn-primary" style=""><i class="fa fa-edit"></i> Edit</a></td></tr>'
        )
    new PNotify({
        title: "Admin Added Successfully",
        text: 'Admin ' + $("#CMBPAdminInput").val() + ' Added Successfully',
        type: 'success',
        width: "22%",
        animate: {
            animate: true,
            in_class: "lightSpeedIn",
            out_class: "slideOutRight"
        }
    });
    $("#addNewCMBPAdmin").hide()
    $('#CMBPAdminEmail').val("")
    $('#CMBPAdminInput').val("")
    $('#CMBPAdminPhone').val("")
}
$("#addNewCMBPAdminBtn").click(function () {
    $("#addNewCMBPAdmin").show()
});
$("#cancelAddNewCMBPAdminBtn").click(function () {
    $("#addNewCMBPAdmin").hide()
    $('#CMBPAdminEmail').val("")
    $('#CMBPAdminInput').val("")
    $('#CMBPAdminPhone').val("")
});
function AddClaimsBPAdminUser() {
    $("#ClaimsBPAdmins").append(
        '<tr><td>' + $("#ClaimsBPAdminInput").val() + '</td><td>' + $("#ClaimsBPAdminEmail").val() + '</td><td>' + $("#ClaimsBPAdminPhone").val() + '</td><td><a class="btn btn-xs btn-primary" style=""><i class="fa fa-eye"></i> View</a> <a class="btn btn-xs btn-primary" style=""><i class="fa fa-edit"></i> Edit</a></td></tr>'
        )
    new PNotify({
        title: "Admin Added Successfully",
        text: 'Admin ' + $("#ClaimsBPAdminInput").val() + ' Added Successfully',
        type: 'success',
        width: "22%",
        animate: {
            animate: true,
            in_class: "lightSpeedIn",
            out_class: "slideOutRight"
        }
    });
    $("#addNewClaimsBPAdmin").hide()
    $('#ClaimsBPAdminEmail').val("")
    $('#ClaimsBPAdminInput').val("")
    $('#ClaimsBPAdminPhone').val("")
}
$("#addNewClaimsBPAdminBtn").click(function () {
    $("#addNewClaimsBPAdmin").show()
});
$("#cancelAddNewClaimsBPAdminBtn").click(function () {
    $("#addNewClaimsBPAdmin").hide()
    $('#ClaimsBPAdminEmail').val("")
    $('#ClaimsBPAdminInput').val("")
    $('#ClaimsBPAdminPhone').val("")
});
//--------------------------------------------------------------------

//Show More/Less BP Description
function ShowMore(id) {
    $('#Detail' + id).slideToggle();
    $("#More" + id).hide();
    $("#Less" + id).show();
}
function ShowLess(id) {
    $('#Detail' + id).slideToggle();
    $("#Less" + id).hide();
    $("#More" + id).show();
}
//--------------------------------------------------------------------

//Business Process Tab Switching and Tab Sorting
function GoTOBP(id) {
    $.each(BusinessProcesses, function (index, value) {
        $("#" + value.ID).removeClass("active")
        $("#Tab" + value.ID).hide();
    });
    $("#" + id).addClass("active")
    $("#Tab" + id).show();
    $("#topMenuBPText").text("")
    $.each(BusinessProcesses, function (index, value) {
        if (value.ID == id) {
            $("#topMenuBPText").text(value.BusinessProcess)
        }

    });
    var BPForSorting = [];
    var Temp = [];
    var BPForRemaining = [];
    var BPActiveTab;
    var hasDisplay;
    $("li a.BPTab").each(function () {

        if ($(this).hasClass("active")) {
            BPActiveTab = this.outerText;
            hasDisplay = this.childNodes[2].className.indexOf("displayblock") >= 0 ? true : false;
        }
        if (($(this).hasClass("active")) && this.childNodes[2].className.indexOf("displayblock") >= 0 || this.childNodes[2].className.indexOf("displayblock") >= 0)
            BPForSorting.push(this.outerText);
        if ($(this).hasClass("active") || !(this.childNodes[2].className.indexOf("displayblock") >= 0))
            Temp.push(this.outerText);
    });
    Temp.sort();
    $.each(Temp, function (index, val) {
        $.each(BusinessProcesses, function (i, v) {
            if (val == v.BusinessProcess.toUpperCase()) {
                BPForRemaining.push(v)
            }
        });
    });
    BPForSorting.sort();

    if (BPForSorting.length > 0 && !(BPActiveTab && hasDisplay)) {
        $('#BPFloatMenu').empty();
        $('#BPFloatMenu').append('<li class="outer_menu_item_top">' +
            '<div class="inner-addon right-addon"  style="margin-top: 8px;margin-bottom: 8px;">' +
                   '<i class="fa fa-search"></i>' +
                   '<input type="text" class="form-control input-xs" placeholder="SEARCH ..." />' +
               '</div>');
        $('#BusinessProcessAppendData').empty();
        $('#BusinessProcessAppendData').append(' <li><input class="form-control input-xs" placeholder="SEARCH BUSINESS PROCESS" style="padding:11px"/></li>');

        $.each(BPForSorting, function (i, v) {
            $.each(BusinessProcesses, function (index, value) {
                if (v == value.BusinessProcess.toUpperCase() && !(BPActiveTab == value.BusinessProcess.toUpperCase())) {
                    $('#BPFloatMenu').append(
                     '<li class="outer_menu_item_top"><a class="tab-navigation member-actions BPTab"  onclick="GoTOBP(\'' + value.ID + '\')"  style="padding:10px" id="' + value.ID + '"><span class="text_beside_icon">' + value.BusinessProcess + '</span><span class="label_on_collapsed floating_menu_label">' + value.BusinessProcess + '</span><div class="pull-right displayblock ' + " " + ' ' + value.ID + '"  style="margin-top:1px"><input type="radio" class="normal-radio" checked="checked"/><label><span></span></label></div></a></li>'
                    );
                    $('#BusinessProcessAppendData').append(
                    '<li><a href="#" class="' + value.ID + ' "  style="padding:11px" onclick="GoTOBP(\'' + value.ID + '\')" >' + value.BusinessProcess + '<div class="pull-right displayblock' + " " + ' ' + value.ID + '" style="margin-top: -3px;"><input type="radio" class="normal-radio" checked="checked"/><label><span></span></label></div></a></li> '
                    );
                }
            });

        });
        $.each(BPForRemaining, function (index, value) {
            if (BPActiveTab == value.BusinessProcess.toUpperCase()) {
                $('#BPFloatMenu').append(
                 '<li class="outer_menu_item_top"><a class="tab-navigation active member-actions BPTab"  onclick="GoTOBP(\'' + value.ID + '\')"  style="padding:10px" id="' + value.ID + '"><span class="text_beside_icon">' + value.BusinessProcess + '</span><span class="label_on_collapsed floating_menu_label">' + value.BusinessProcess + '</span><div class="pull-right' + " " + ' ' + value.ID + '" hidden style="margin-top:1px"><input type="radio" class="normal-radio" checked="checked"/><label><span></span></label></div></a></li>'
                );
                $('#BusinessProcessAppendData').append(
                '<li><a href="#" class="' + value.ID + ' "  style="padding:11px" onclick="GoTOBP(\'' + value.ID + '\')" >' + value.BusinessProcess + '<div class="pull-right' + " " + ' ' + value.ID + '" style="margin-top: -3px;" hidden><input type="radio" class="normal-radio" checked="checked"/><label><span></span></label></div></a></li> '
                );
            }
            else {
                $('#BPFloatMenu').append(
                 '<li class="outer_menu_item_top"><a class="tab-navigation member-actions BPTab"  onclick="GoTOBP(\'' + value.ID + '\')"  style="padding:10px" id="' + value.ID + '"><span class="text_beside_icon">' + value.BusinessProcess + '</span><span class="label_on_collapsed floating_menu_label">' + value.BusinessProcess + '</span><div class="pull-right' + " " + ' ' + value.ID + '" hidden style="margin-top:1px"><input type="radio" class="normal-radio" checked="checked"/><label><span></span></label></div></a></li>'
                );
                $('#BusinessProcessAppendData').append(
                '<li><a href="#" class="' + value.ID + ' "  style="padding:11px" onclick="GoTOBP(\'' + value.ID + '\')" >' + value.BusinessProcess + '<div class="pull-right' + " " + ' ' + value.ID + '" style="margin-top: -3px;" hidden><input type="radio" class="normal-radio" checked="checked"/><label><span></span></label></div></a></li> '
                );
            }


        });
    }
}
//--------------------------------------------------------------------

//Subscribing to BPs
$(".SubscribeClaimsFeatures").click(function () {
    if ($(".ClaimsFeaturesCheckbox").is(":checked")) {
        $(".claimsmanagement").removeClass("hideblock")
        $(".claimsmanagement").addClass("displayblock")
    }
    else {
        $(".claimsmanagement").removeClass("displayblock")
        $(".claimsmanagement").addClass("hideblock")
    }
});
$(".SubscribePMFeatures").click(function () {
    if ($(".ProviderFeaturesCheckbox").is(":checked")) {
        $(".providermanagement").removeClass("hideblock")
        $(".providermanagement").addClass("displayblock")
    }
    else {
        $(".providermanagement").removeClass("displayblock")
        $(".providermanagement").addClass("hideblock")
    }
});
$(".SubscribeMMFeatures").click(function () {
    if ($(".MemberFeaturesCheckbox").is(":checked")) {
        $(".membermanagement").removeClass("hideblock")
        $(".membermanagement").addClass("displayblock")
    }
    else {
        $(".membermanagement").removeClass("displayblock")
        $(".membermanagement").addClass("hideblock")
    }
});
$(".SubscribeCMFeatures").click(function () {
    if ($(".CareFeaturesCheckbox").is(":checked")) {
        $(".caremanagement").addClass("displayblock")
        $(".caremanagement").removeClass("hideblock")
    }
    else {
        $(".caremanagement").removeClass("displayblock")
        $(".caremanagement").addClass("hideblock")
    }
});
$(".SubscribeUMFeatures").click(function () {
    if ($(".UtilizationFeaturesCheckbox").is(":checked")) {
        $(".utilizationmanagement").addClass("displayblock")
        $(".utilizationmanagement").removeClass("hideblock")
    }
    else {
        $(".utilizationmanagement").removeClass("displayblock")
        $(".utilizationmanagement").addClass("hideblock")
    }
});
$(".SubscribeDMFeatures").click(function () {
    if ($(".DeseaseFeaturesCheckbox").is(":checked")) {
        $(".diseasemanagement").addClass("displayblock")
        $(".diseasemanagement").removeClass("hideblock")
    }
    else {
        $(".diseasemanagement").removeClass("displayblock")
        $(".diseasemanagement").addClass("hideblock")
    }
});
$(".SubscribeCapitationFeatures").click(function () {
    if ($(".CapitationFeaturesCheckbox").is(":checked")) {
        $(".capitation").addClass("displayblock")
        $(".capitation").removeClass("hideblock")
    }
    else {
        $(".capitation").removeClass("displayblock")
        $(".capitation").addClass("hideblock")
    }
});
$(".SubscribeBMFeatures").click(function () {
    if ($(".BillingFeaturesCheckbox").is(":checked")) {
        $(".billingmanagement").addClass("displayblock")
        $(".billingmanagement").removeClass("hideblock")
    }
    else {
        $(".billingmanagement").removeClass("displayblock")
        $(".billingmanagement").addClass("hideblock")
    }
});
//-------------------------------------------------------------------

//Checking All Checkboxes of BPs (Category-wise)
function SelectAllClaimsFeatures(id, bp) {
    $("." + bp + "Category_" + id).prop('checked', $("." + bp + "Category_" + id + "Check").prop("checked"));
}

//-------------------------------------------------------------------
//-------------------------------------------E N D  B U S I N E S S  P R O C E S S E S ------------------------------------------------


//------------------------------------------- U S E R S ------------------------------------------------

//Ajax For Roles Data
var RolesData = [];
$.ajax({
    url: '/Areas/AM/Resources/JSONData/Roles.js',
    success: function (response) {
        RolesData = JSON.parse(response);
        $("#ListAllRoles").empty();
        $("#UserRoles").empty();
        $.each(RolesData, function (index, value) {
            $("#ListAllRoles").append(
       '<option value="' + value.RoleName.toUpperCase() + '" >' + value.RoleName.toUpperCase() + '</option>'
           )
            $("#UserRoles").append(
      '<option value="' + value.RoleName.toUpperCase() + '" >' + value.RoleName.toUpperCase() + '</option>')
        });
        $("#UserRoles").multiselect({
            selectAll: true,
            placeholder: 'USER ROLE',
            column: 1
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
        $("#TeamsTableData").empty();
        $('#TeamsAppendData').empty();
        $('#TeamsAppendData').append(' <li><input class="form-control input-xs" placeholder="SEARCH TEAM" style="padding:11px"/></li>');
        $.each(TeamsData, function (index, value) {
            $("#ListAllTeams").append(
       '<option value="' + value.TeamName.toUpperCase() + '" >' + value.TeamName.toUpperCase() + '</option>'
           )
            $('#TeamsTableData').append(
                '<tr>' +
                     '<td class="theme_label_data">' + value.ID + '</td>' +
                     '<td class="theme_label_data">' + value.TeamName + '</td>' +
                     '<td class="theme_label_data">' + value.CreatedOn + '</td>' +
                     '<td class="text-center"><button class="btn btn-primary btn-xs"  onclick="ViewTeam(\'' + value.ID + '\')" ><i class="fa fa-eye"></i> View</button> &nbsp;&nbsp;<button class="btn btn-primary btn-xs"  onclick="EditTeam(\'' + value.ID + '\')" ><i class="fa fa-edit"></i> Edit</button>' +
                     '</td>' +
               '</tr>'
             );
            $("[data-toggle=popover]").popover();
            $('#TeamsAppendData').append(
           '<li><a style="padding:11px"  >' + value.TeamName + '</a></li>'
           );
        });
    }
});
//-------------------------------------------------------------------------------
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

function UsersTabActive() {
    $("#teams").parent().removeClass("work_menu_special_item")
    $("#users").parent().addClass("work_menu_special_item")
    $("#accounts").parent().removeClass("work_menu_special_item")
    $("#businessProcesses").parent().removeClass("work_menu_special_item")
    $("#departments").parent().removeClass("work_menu_special_item")
    $("#userroles").parent().removeClass("work_menu_special_item")
    $("#businessProcesses").removeClass("active");
    $("#accounts").removeClass("active");
    $("#userroles").removeClass("active");
    $("#teams").removeClass("active");
    $(".AddNewUser").show();
    $("#departments").removeClass("active");
    $("#users").addClass("active");
    $(".DepartmentsTopMenuR").css({ "opacity": "0.3" });
    $(".DepartmentsTopMenuL").css({ "opacity": "0.3", "box-shadow": "0px 0px 0px" });
    $(".BusinessProcessesTopMenuR").css({ "opacity": "0.3" });
    $(".BusinessProcessesTopMenuL").css({ "opacity": "0.3", "box-shadow": "0px 0px 0px" });
    $(".AccountsTopMenuR").css({ "opacity": "0.3" });
    $(".AccountsTopMenuL").css({ "opacity": "0.3", "box-shadow": "0px 0px 0px" });
    $("#AllDepts").show();
    $("#AllBPs").show();
    $("#AllAccs").show();
    $("#AllDeptsDropdown").hide();
    $("#AllAccsDropdown").hide();
    $("#AllBPsDropdown").hide();
    $("#AccountsListData").hide();
    $("#BusinessProcessesData").hide();
    $("#ViewAccountData").hide();
    $("#EditAccountData").hide();
    $("#ViewDepartmentsData").hide();
    $("#ViewUserRolesData").hide();
    $("#ViewTeamsData").hide();
    $("#ViewUsersData").show();
}

//fortest
function DefaultUsersTabActive() {
    $("#UsersListTableData").empty();
    $(".ListAllUsers").empty();
    $(".ListAllUsers").append('<option selected disabled>SELECT USER</option>');
    $.each(Members, function (index, value) {
        if (value.AccountInformation.Status == 'Active') {
            $('#UsersListTableData').append(
       '<tr class="theme_label_data">' +
       '<td class="theme_label_data">' + value.PersonalInformation.ID + '</td>' +
       '<td class="theme_label_data">' + value.PersonalInformation.FirstName + '</td>' +
       '<td class="theme_label_data">' + value.PersonalInformation.LastName + '</td>' +
       '<td class="theme_label_data">' + value.AccountInformation.UserName + '</td>' +
       '<td class="' + value.AccountInformation.Status + '"> ' + value.AccountInformation.Status + '</td>' +
       '<td class="text-center"><a class="btn btn-primary btn-xs"  data-toggle="tooltip" data-placement="bottom" title="View User" onclick="ViewUser(\'' + value.PersonalInformation.ID + '\')" ><i class="fa fa-eye"></i> View</a> &nbsp;&nbsp;' +
                '<a class="btn btn-primary btn-xs"  data-toggle="tooltip" data-placement="bottom" title="Edit User" onclick="EditUser(\'' + value.PersonalInformation.ID + '\')" ><i class="fa fa-edit"></i> Edit</a> &nbsp;&nbsp;' +
                '<a class="btn btn-primary btn-xs"  data-toggle="tooltip" data-placement="bottom" title="Deactivate User" onclick="DeactivateUser(\'' + value.PersonalInformation.ID + '\')" ><i class="fa fa-close"></i> Deactivate</a>' + '</td>' +
    '</tr>'
    );
        }
        else {
            $('#UsersListTableData').append(
       '<tr>' +
       '<td class="theme_label_data">' + value.PersonalInformation.ID + '</td>' +
      '<td class="theme_label_data">' + value.PersonalInformation.FirstName + '</td>' +
       '<td class="theme_label_data">' + value.PersonalInformation.LastName + '</td>' +
       '<td class="theme_label_data">' + value.AccountInformation.UserName + '</td>' +
       '<td class="' + value.AccountInformation.Status + '"> ' + value.AccountInformation.Status + '</td>' +
       '<td class="text-center"><a class="btn btn-primary btn-xs"  data-toggle="tooltip" data-placement="bottom" title="View User" onclick="ViewUser(\'' + value.PersonalInformation.ID + '\')" ><i class="fa fa-eye"></i> View</a> &nbsp;&nbsp;' +
                '<a class="btn btn-primary btn-xs"  data-toggle="tooltip" data-placement="bottom" title="Edit User" onclick="EditUser(\'' + value.PersonalInformation.ID + '\')" ><i class="fa fa-edit"></i> Edit</a> &nbsp;&nbsp;' +
                '<a class="btn btn-primary btn-xs"  data-toggle="tooltip" data-placement="bottom" title="Activate User" onclick="ActivateUser(\'' + value.PersonalInformation.ID + '\')" ><i class="fa fa-close"></i> Activate &nbsp;&nbsp;&nbsp;</a>' + '</td>' +
    '</tr>'
    );
        }
        $(".ListAllUsers").append(
       '<option value="' + value.PersonalInformation.ID + '" >' + value.PersonalInformation.FirstName.toUpperCase() + '-' + value.PersonalInformation.LastName.toUpperCase()  + '</option>'
           )
        //$('#AccountsAppendData').append(
        //    '<li><a style="padding:11px" onclick="ViewAccount(\'' + value.AccountInformation.AccountID + '\')" >' + value.AccountInformation.AccountName + ' - ' + value.AccountInformation.AccountID + '</a></li>'
        //    );
        $('[data-toggle="tooltip"]').tooltip();
        $("input:checkbox.switchCss").bootstrapSwitch();

    });
}
//-------
//-------------------------------------------------------------------

//Side Menu Users Click
$("#users").click(function () {
    $("#teams").parent().removeClass("work_menu_special_item")
    $("#users").parent().addClass("work_menu_special_item")
    $("#accounts").parent().removeClass("work_menu_special_item")
    $("#businessProcesses").parent().removeClass("work_menu_special_item")
    $("#departments").parent().removeClass("work_menu_special_item")
    $("#userroles").parent().removeClass("work_menu_special_item")
    $("#businessProcesses").removeClass("active");
    $("#accounts").removeClass("active");
    $("#IndividualUserView").hide();
    $("#viewUserDetails").hide();
    $("#editUserDetails").hide();
    $(".AddNewUser").show();
    $("#ViewUserRolesData").hide();
    $("#userroles").removeClass("active");
    $("#teams").removeClass("active");
    $("#departments").removeClass("active");
    $("#users").addClass("active");
    $("#BusinessProcessesData").hide();
    $("#AccountsListData").hide();
    $("#ViewAccountData").hide();
    $("#EditAccountData").hide();
    $("#ViewDepartmentsData").hide();
    $("#ViewUsersData").show();
    $("#ViewTeamsData").hide();
    $(".DepartmentsTopMenuR").css({ "opacity": "0.3" });
    $(".DepartmentsTopMenuL").css({ "opacity": "0.3", "box-shadow": "0px 0px 0px" });
    $(".BusinessProcessesTopMenuR").css({ "opacity": "0.3" });
    $(".BusinessProcessesTopMenuL").css({ "opacity": "0.3", "box-shadow": "0px 0px 0px" });
    $(".AccountsTopMenuR").css({ "opacity": "0.3" });
    $(".AccountsTopMenuL").css({ "opacity": "0.3", "box-shadow": "0px 0px 0px" });
    $("#AllTeams").show();
    $("#AllTeamsDropdown").hide();
    $(".TeamsTopMenuR").css({ "opacity": "0.3" });
    $(".TeamsTopMenuL").css({ "opacity": "0.3", "box-shadow": "0px 0px 0px" });
    $("#AllDepts").show();
    $("#AllBPs").show();
    $("#AllAccs").show();

    $("#ListAllUsersView").show();
    $("#AllDeptsDropdown").hide();
    $("#AllAccsDropdown").hide();
    $("#AllBPsDropdown").hide();
    $("#UsersListTableData").empty();
    $(".ListAllUsers").empty();
    $(".ListAllUsers").append('<option selected disabled>SELECT USER</option>');
    $.each(Members, function (index, value) {
        if (value.AccountInformation.Status == 'Active') {
            $('#UsersListTableData').append(
       '<tr>' +
       '<td class="theme_label_data">' + value.PersonalInformation.ID + '</td>' +
        '<td class="theme_label_data">' + value.PersonalInformation.FirstName + '</td>' +
       '<td class="theme_label_data">' + value.PersonalInformation.LastName + '</td>' +
       '<td class="theme_label_data">' + value.AccountInformation.UserName + '</td>' +
       '<td class="' + value.AccountInformation.Status + '"> ' + value.AccountInformation.Status + '</td>' +
       '<td class="text-center"><a class="btn btn-primary btn-xs"  data-toggle="tooltip" data-placement="bottom" title="View User" onclick="ViewUser(\'' + value.PersonalInformation.ID + '\')" ><i class="fa fa-eye"></i> View</a> &nbsp;&nbsp;' +
                '<a class="btn btn-primary btn-xs"  data-toggle="tooltip" data-placement="bottom" title="Edit User" onclick="EditUser(\'' + value.PersonalInformation.ID + '\')" ><i class="fa fa-edit"></i> Edit</a> &nbsp;&nbsp;' +
                '<a class="btn btn-primary btn-xs"  data-toggle="tooltip" data-placement="bottom" title="Deactivate User" onclick="DeactivateUser(\'' + value.PersonalInformation.ID + '\')" ><i class="fa fa-close"></i> Deactivate</a>' + '</td>' +
    '</tr>'
    );
        }
        else {
            $('#UsersListTableData').append(
       '<tr>' +
       '<td class="theme_label_data">' + value.PersonalInformation.ID + '</td>' +
        '<td class="theme_label_data">' + value.PersonalInformation.FirstName + '</td>' +
       '<td class="theme_label_data">' + value.PersonalInformation.LastName + '</td>' +
       '<td class="theme_label_data">' + value.AccountInformation.UserName + '</td>' +
       '<td class="' + value.AccountInformation.Status + '"> ' + value.AccountInformation.Status + '</td>' +
       '<td class="text-center"><a class="btn btn-primary btn-xs"  data-toggle="tooltip" data-placement="bottom" title="View User" onclick="ViewUser(\'' + value.PersonalInformation.ID + '\')" ><i class="fa fa-eye"></i> View</a> &nbsp;&nbsp;' +
                '<a class="btn btn-primary btn-xs"  data-toggle="tooltip" data-placement="bottom" title="Edit User" onclick="EditUser(\'' + value.PersonalInformation.ID + '\')" ><i class="fa fa-edit"></i> Edit</a> &nbsp;&nbsp;' +
                '<a class="btn btn-primary btn-xs"  data-toggle="tooltip" data-placement="bottom" title="Activate User" onclick="ActivateUser(\'' + value.PersonalInformation.ID + '\')" ><i class="fa fa-close"></i> Activate &nbsp;&nbsp;&nbsp;</a>' + '</td>' +
    '</tr>'
    );
        }
        $(".ListAllUsers").append(
       '<option value="'  + value.PersonalInformation.ID + '" >' + value.PersonalInformation.FirstName.toUpperCase() + '-' + value.PersonalInformation.LastName.toUpperCase()  + '</option>'
           )
        //$('#AccountsAppendData').append(
        //    '<li><a style="padding:11px" onclick="ViewAccount(\'' + value.AccountInformation.AccountID + '\')" >' + value.AccountInformation.AccountName + ' - ' + value.AccountInformation.AccountID + '</a></li>'
        //    );
        $('[data-toggle="tooltip"]').tooltip();
        $("input:checkbox.switchCss").bootstrapSwitch();

    });

});


//---------------------------------------------------------------------------------
function MapUsers() {
    $(".AddUserTextBoxes").val("");
    $(".ListAllUsers").val("SELECT USER");
    showModal('UserUserMappingModal');
}
var UserIDForMapping;

$("#MapUserName").on('change', function () {
    var val = this.value;
    if ($('.ListAllUsers option').filter(function () {
        return this.value === val;
    }).length) {
        var fieldVal = $("#MapUserName").val();
        var data = fieldVal.split('-')
        UserIDForMapping = data[2];
        $("#MapUserName").val(data[0] + '-' + data[1] + '-' + data[2]);
        $.each(Members, function (index, value) {
            if (value.PersonalInformation.ID == data[2]) {
                $("#MapUserTeam").val(value.AccountInformation.Team.toUpperCase())
                $("#MapUserRole").val(value.PersonalInformation.UserRole.toUpperCase())
            }

        });
    }
});
function MapNewUser() {
    $("#EditUserMapping").append('<tr>' +
                                                '<td>' + $("#MapUserName").val().split('-')[0] + '</td>' +
                                                '<td>' + $("#MapUserTeam").val() + '</td>' +
                                                '<td>' + $("#MapUserRole").val() + '</td>' +
                                                '<td class="text-center">' +
                                                    '<a class="btn btn-xs btn-danger" style="" onclick="RemoveMappedUser(this)"><i class="fa fa-close"></i> Remove</a>' +
                                                '</td>' +
                                                '</tr>')
    $("#MapUserName").val("SELECT USER")



}



//$("#MappedUserName").on('input', function () {
//    var val = this.value;
//    if ($('#ListAllUsers option').filter(function () {
//        return this.value === val;
//    }).length) {
//        //var fieldVal = $("#MappedUserName").val();
//        //var data = fieldVal.split('-')
//        //UserIDForMapping = data[2];
//        //$("#MappedUserName").val(data[0] + '-' + data[1]);
//        $.each(Members, function (index, value) {
//            if (value.PersonalInformation.ID == val) {
//                $("#UserTeam").val(value.AccountInformation.Team.toUpperCase())
//                $("#UserRole").val(value.PersonalInformation.UserRole.toUpperCase())
//            }

//        });
//    }
//});
$("#MappedUsers").empty();
$("#MappedUsers").append('<tr><td class="text-center" colspan="4">NO USER MAPPED</td></tr>');
var UserTableClear = true;
function MapUser() {
    if (UserTableClear)
        $("#MappedUsers").empty();
    $.each(Members, function (index, value) {
        if (value.PersonalInformation.ID == UserIDForMapping) {
            $("#MappedUsers").append('<tr>' +
                                                '<td>' + value.PersonalInformation.FirstName + '</td>' +
                                                '<td>' + value.AccountInformation.Team + '</td>' +
                                                '<td>' + value.PersonalInformation.UserRole + '</td>' +
                                                '<td class="text-center">' +
                                                    '<a class="btn btn-xs btn-danger" style="" onclick="RemoveMappedUser(this)"><i class="fa fa-close"></i> REMOVE</a>' +
                                                '</td>' +
                                                '</tr>')
        }
    });
    UserTableClear = false;
    $("#UserTeam").val("")
    $("#UserRole").val("")
    $("#MappedUserName").val("SELECT USER")

}
//------------------
function RemoveMappedUser(id) {
    $(id).parent().parent("tr").remove();
    if ($('#MappedUsers tr').length == 0) {
        $("#MappedUsers").append('<tr><td class="text-center" colspan="4">NO USERS MAPPED</td></tr>');
        UserTableClear = true;
    }
}

//function GetDetailsForMapping(id) {
//    $.each(Members, function (index, value) {
//        if (value.PersonalInformation.ID == id) {
//            $("#UserTeam").val(value.AccountInformation.Team)
//            $("#UserRole").val(value.PersonalInformation.UserRole)
//        }

//    });

//}



//Add New User Modal
function AddUserModal(idvalue) {
    $(".AddUserTextBoxes").val("");
    $(".AddUserTextBoxes").select2("val", "");
    $("#MappedUsers").empty();
    $("#MappedUsers").append('<tr><td class="text-center" colspan="4">NO USER MAPPED</td></tr>');
    $("#UserMapping").hide();
    $("#UserMappingCheckboxIcon").addClass("fa-chevron-down")
    $("#UserMappingCheckboxIcon").removeClass("fa-chevron-up")
    $('#UserMappingCheckbox').prop('checked', false);
    ShowHide('ExpandModalBtn', 'CompressModalBtn')
    showModal('AddUserModal');
}
//-------------------------------------------------------------------
function ShowHide(showid, hideid) {
    $("." + hideid).hide();
    $("." + showid).show();
}

function FullScreenModal(id) {

    $("#" + id).css('top', '6%');
    $(".ModalDialog").addClass('modal-dialog-fullscreen');
    $(".ModalContent").addClass('modal-content-fullscreen');
   // $(".ModalContent").addClass('modal-content-fullscreen');
    $(".modal-body").css('max-height', '530px');
}

function CompressModal(id) {
    $("#" + id).css('top', '8%');
    $(".ModalDialog").removeClass('modal-dialog-fullscreen');
    $(".ModalContent").removeClass('modal-content-fullscreen');
    $(".modal-body").css('max-height', '420px');
}
function CloseModal(id) {
    $("#" + id).css('top', '8%');
    $(".ModalDialog").removeClass('modal-dialog-fullscreen');
    $(".ModalContent").removeClass('modal-content-fullscreen');
    $(".modal-body").css('max-height', '420px');


}

$('#UserMappingCheckbox').change(function () {
    if ($(this).is(':checked')) {
        $("#UserMapping").show();
        $("#UserMappingCheckboxIcon").removeClass("fa-chevron-down")
        $("#UserMappingCheckboxIcon").addClass("fa-chevron-up")
    } else {
        $("#UserMapping").hide();
        $("#UserMappingCheckboxIcon").addClass("fa-chevron-down")
        $("#UserMappingCheckboxIcon").removeClass("fa-chevron-up")
    }
});

//----------Create User
function CreateNewUser() {
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
    new PNotify({
        title: "User Created Successfully",
        width: "23%",
        //text: 'Account ' + AccountName + ' Created Successfully',
        type: 'success',

        animate: {
            animate: true,
            in_class: "lightSpeedIn",
            out_class: "slideOutRight"
        }
    });
}
//User Deactivation
function DeactivateUser(idvalue) {
    showModal('DeactivateUserModal');
}
function UserDeactivate() {
    new PNotify({
        title: "User Deactivated Successfully",
        //text: 'Account ' + AccountName + ' Created Successfully',
        type: 'success',
        width: "23%",

        animate: {
            animate: true,
            in_class: "lightSpeedIn",
            out_class: "slideOutRight"
        }
    });
}
//-------------------------------------------------------------------
//User View (Individual)-------------------------------------------------------
function ViewUser(idvalue) {
    UsersTabActive();
    $("#AccountsListData").hide();
    $("#ViewAccountData").hide();
     $("#EditAccountData").hide();
    $("#ListAllUsersView").hide();
    $("#IndividualUserView").show();
    $("#ViewUsersData").show();
    $("#viewUserDetails").show();
    $("#editUserDetails").hide();
    $('#EditAccount').show();
    $.each(Members, function (index, value) {

        if (idvalue == value.PersonalInformation.ID) {
            $("#useraccid").text(value.AccountInformation.AccountID);
            $("#useraccname").text(value.AccountInformation.AccountName);
            $("#userstatus").text(value.PersonalInformation.Status);
            $("#useracctype").text(value.AccountInformation.AccountType);
            $("#useraccstatus").text(value.AccountInformation.Status);

            $("#username").text(value.PersonalInformation.FirstName);
            $("#firstname").text(value.PersonalInformation.FirstName);
            $("#lastname").text(value.PersonalInformation.LastName);
            $("#middlename").text("-");
            $("#SSN").text("-");
            $("#DOB").text(value.PersonalInformation.DOB.split('T')[0]);
            $("#gender").text(value.PersonalInformation.Gender);
            $("#maritalstatus").text("-");
            $("#HomeNumber").text(value.ContactInformation.ContactNumber);
            $("#OfficeNumber").text(value.ContactInformation.ContactNumber);
            $("#EmailId").text(value.ContactInformation.email);
            $("#faxNumber").text(value.ContactInformation.FaxNumber)
            $("#Pcontact").text(value.ContactInformation.ContactNumber);
            $("#Pemail").text(value.ContactInformation.email);
            $("#Pfax").text(value.ContactInformation.FaxNumber)
            $("#address1").text(value.AddressInformation.Address);
            $("#address2").text(value.AddressInformation.Address);
            $("#street").text(value.AddressInformation.Street);
            $("#city").text(value.AddressInformation.City);
            $("#county").text(value.AddressInformation.County);
            $("#state").text(value.AddressInformation.State);
            $("#country").text(value.AddressInformation.Country);
            $("#zipcode").text(value.AddressInformation.ZipCode);


            $("#useraddress").text(value.AddressInformation.Address);
            $("#userstreet").text(value.AddressInformation.Street);
            $("#usercity").text(value.AddressInformation.City);
            $("#usercounty").text(value.AddressInformation.County);
            $("#usercountry").text("US");
            $('#EditAccount').html('<button class="btn btn-sm btn-primary" onclick="EditUser(' + value.PersonalInformation.ID + ')" style="position: relative;z-index: 1;"><span class="fa fa-edit"> Edit</span></button>')
            $("#userstate").text(value.AddressInformation.State);
            $("#userzipcode").text(value.AddressInformation.ZipCode);
            $("#userrole").text(value.PersonalInformation.UserRole);
            $("#userteam").text(value.AccountInformation.Team);
            $("#userfax").text(value.ContactInformation.FaxNumber);
            $("#useremail").text(value.ContactInformation.email);
            $("#usercontactno").text(value.ContactInformation.ContactNumber);
        }
    });
}
//--------------------------------------------------------------------------------
function BackToUserList(idvalue) {
    $("#ListAllUsersView").show();
    $("#IndividualUserView").hide();
}
//--------------------------------------------------------------------------------
//User View (Individual)-------------------------------------------------------
var TempIDForUserEdit;
function EditUser(idvalue) {
    TempIDForUserEdit = idvalue;
    UsersTabActive();
    $("#AccountsListData").hide();
    $("#ViewAccountData").hide();
     $("#EditAccountData").hide();
    $("#ListAllUsersView").hide();
    $("#IndividualUserView").show();
    $("#ViewUsersData").show();
    $("#viewUserDetails").hide();
    $("#editUserDetails").show();
    $.each(Members, function (index, value) {

        if (idvalue == value.PersonalInformation.ID) {
            $("#useraccidinputfield").val(value.AccountInformation.AccountID.toUpperCase());
            $("#useraccnameinputfield").val(value.AccountInformation.AccountName.toUpperCase());
            $("#userstatusinputfield").val(value.PersonalInformation.Status.toUpperCase());
            $("#useracctypeinputfield").val(value.AccountInformation.AccountType.toUpperCase());
            $("#useraccstatusinputfield").val(value.AccountInformation.Status.toUpperCase());
            $("input[name=Gender][value=" + value.PersonalInformation.Gender + "]").prop('checked', true);
            $("#eusername").val(value.PersonalInformation.FirstName);
            $("#efirstname").val(value.PersonalInformation.FirstName);
            $("#elastname").val(value.PersonalInformation.LastName);
            $("#emiddlename").val("-");
            $("#eSSN").val("-");
            $("#eDOB").val(value.PersonalInformation.DOB.split('T')[0]);
            $("#egender").val(value.PersonalInformation.Gender);
            $("#emaritalstatus").val("-");
            $("#eHomenumber").val(value.ContactInformation.ContactNumber);
            $("#eOfficeNumber").val(value.ContactInformation.ContactNumber);
            $("#eEmailId").val(value.ContactInformation.email);
            $("#efaxnumber").val(value.ContactInformation.FaxNumber)
            $("#ePcontact").val(value.ContactInformation.ContactNumber);
            $("#ePemail").val(value.ContactInformation.email);
            $("#ePfax").val(value.ContactInformation.FaxNumber)
            $("#eaddress1").val(value.AddressInformation.Address);
            $("#eaddress2").val(value.AddressInformation.Address);
            $("#estreet").val(value.AddressInformation.Street);
            $("#estate").val(value.AddressInformation.State);
            $("#ecity").val(value.AddressInformation.City);
            $("#ecounty").val(value.AddressInformation.County);
            $("#ecountry").val(value.AddressInformation.Country);
            $("#ezipcode").val(value.AddressInformation.ZipCode);
            $('#EditAccount').hide();

            $("#usernameinputfield").val(value.PersonalInformation.FirstName.toUpperCase());
            $("#useraddressinputfield").val(value.AddressInformation.Address.toUpperCase());
            $("#userstreetinputfield").val(value.AddressInformation.Street.toUpperCase());
            $("#usercityinputfield").val(value.AddressInformation.City.toUpperCase());
            $("#usercountyinputfield").val(value.AddressInformation.County.toUpperCase());
            $("#usercountryinputfield").val("US");
            $("#userstateinputfield").val(value.AddressInformation.State.toUpperCase());
            $("#userzipcodeinputfield").val(value.AddressInformation.ZipCode.toUpperCase());
            $("#userroleinputfield").val(value.PersonalInformation.UserRole.toUpperCase());
            $("#userteaminputfield").val(value.AccountInformation.Team.toUpperCase());
            $("#userfaxinputfield").val(value.ContactInformation.FaxNumber.toUpperCase());
            $("#useremailinputfield").val(value.ContactInformation.email.toUpperCase());
            $("#usercontactnoinputfield").val(value.ContactInformation.ContactNumber.toUpperCase());
        }
    });
}
//--------------------------------------------------------------------------------

function UpdateUserDetails() {
    BackToUserList();
    new PNotify({
        title: "User Updated Successfully",
        type: 'success',
        width: "23%",
        animate: {
            animate: true,
            in_class: "lightSpeedIn",
            out_class: "slideOutRight"
        }
    });
}

function CancelUpdateUserDetails() {
    BackToUserList();
}



//Account Activation
function ActivateUser(idvalue) {
    showModal('ActivateUserModal');
}
function UserActivate() {

    new PNotify({
        title: "User Activated Successfully",
        //text: 'Account ' + AccountName + ' Created Successfully',
        type: 'success',
        width: "23%",

        animate: {
            animate: true,
            in_class: "lightSpeedIn",
            out_class: "slideOutRight"
        }
    });
}
//-------------------------------------------------------------------

//------------------------------------------- U S E R  R O L E S ------------------------------------------------

$("#userroles").click(function () {
    $("#TopMenu").show();
    $("#teams").parent().removeClass("work_menu_special_item")
    $("#users").parent().removeClass("work_menu_special_item")
    $("#departments").parent().removeClass("work_menu_special_item")
    $("#userroles").parent().addClass("work_menu_special_item")
    $("#accounts").parent().removeClass("work_menu_special_item")
    $("#businessProcesses").parent().removeClass("work_menu_special_item")
    $("#businessProcesses").removeClass("active");
    $("#accounts").removeClass("active");
    $("#teams").removeClass("active");
    $("#departments").removeClass("active");
    $("#users").removeClass("active");
    $("#userroles").addClass("active");
    $("#BusinessProcessesData").hide();
    $("#IndividualUserView").hide();
    $("#viewUserDetails").hide();
    $("#editUserDetails").hide();
    $("#AccountsListData").hide();
    $("#ViewAccountData").hide();
    $("#EditAccountData").hide();
    $("#ViewDepartmentsData").hide();
    $(".AddNewUser").hide();
    $("#ViewUsersData").hide();
    $("#ViewTeamsData").hide();
    $("#AllDepts").show();
    $("#ViewUserRolesData").show();
    $("#AllDeptsDropdown").hide();
    $("#UserRolesListTableData").empty();
    $("#AllTeams").show();
    $("#AllTeamsDropdown").hide();
    $(".TeamsTopMenuR").css({ "opacity": "0.3" });
    $(".TeamsTopMenuL").css({ "opacity": "0.3", "box-shadow": "0px 0px 0px" });
    $(".DepartmentsTopMenuR").css({ "opacity": "0.3" });
    $(".DepartmentsTopMenuL").css({ "opacity": "0.3", "box-shadow": "0px 0px 0px" });
    $(".BusinessProcessesTopMenuR").css({ "opacity": "0.3" });
    $(".BusinessProcessesTopMenuL").css({ "opacity": "0.3", "box-shadow": "0px 0px 0px" });
    $(".AccountsTopMenuR").css({ "opacity": "0.3" });
    $(".AccountsTopMenuL").css({ "opacity": "0.3", "box-shadow": "0px 0px 0px" });
    $("#AllDepts").show();
    $("#AllBPs").show();
    $("#AllAccs").show();

    $.each(RolesData, function (index, value) {
        var data = value.Description.trimToLength(75);
        $('#UserRolesListTableData').append(
   '<tr>' +
   '<td class="theme_label_data">' + value.RoleName + '</td>' +
   '<td class="theme_label_data" id="Roles' + value.ID + '"></td>' +
   '<td class="theme_label_data"><span data-container="body"data-placement="bottom" data-toggle="popover" title="DESCRIPTION" data-trigger="hover" data-content="' + value.Description + '">' + data + ' </span></td>' +
   '<td class="text-center"><a class="btn btn-primary btn-xs"  data-toggle="tooltip" data-placement="bottom" title="View User Role" onclick="ViewUserRole(\'' + value.ID + '\')" ><i class="fa fa-eye"></i> View</a> &nbsp;&nbsp;' +
            '<a class="btn btn-primary btn-xs"  data-toggle="tooltip" data-placement="bottom" title="Edit User Role" onclick="EditUserRole(\'' + value.ID + '\')" ><i class="fa fa-edit"></i> Edit</a> &nbsp;&nbsp;' +
            '</td>' +
'</tr>'
);
        $.each(value.BusinessProcesses, function (i, v) {
            //var data = v.trimToLength(10);
            $("#Roles" + value.ID).append('<span>' + v + ' </span>');
        });
        $('[data-toggle="tooltip"]').tooltip();
        $("input:checkbox.switchCss").bootstrapSwitch();
        $("[data-toggle=popover]").popover();
    });

});

//-------------------------------------------------------------------------------
$("#PrivilegesSummaryTable").empty();
$("#PrivilegesSummaryTable").append('<tr><td colspan="3" class="text-center">NO PRIVILEGES SELECTED</td></tr>');
var IsPrivilegesSummaryTableClear = true;
function RemoveMappedPrivilages(id) {
    $(id).parent().parent("tr").remove();
    if ($('#PrivilegesSummaryTable tr').length == 0) {
        $("#PrivilegesSummaryTable").append('<tr><td colspan="3" class="text-center">NO PRIVILEGES SELECTED</td></tr>');
        IsPrivilegesSummaryTableClear = true;
    }
}

function RemoveFeature(id) {
    $(id).parent("div").remove();
}

function PrivilegesMappingForRole() {
    if (IsPrivilegesSummaryTableClear)
        $("#PrivilegesSummaryTable").empty();
    $("#PrivilegesSummaryTable").append('<tr><td>' + $("#RoleBPInputField").val() + '</td><td><div class="col-lg-4 col-md-4 col-xs-6" style="height: 23px;border : 1px solid lightgray;padding: 3px 6px;border-radius: 10px;margin-left: 13px;box-shadow: 1px 1px 5px lightgray;">CREATE AUTH<i class="fa fa-close pull-right" style="padding: 2px;cursor:pointer;color:red" onclick="RemoveFeature(this)"></i></div><div class="col-lg-4 col-md-4 col-xs-6" style="height: 23px;border : 1px solid lightgray;padding: 3px 6px;border-radius: 10px;margin-left: 13px;box-shadow: 1px 1px 5px lightgray;">VIEW AUTH<i class="fa fa-close pull-right" style="padding: 2px;cursor:pointer;color:red" onclick="RemoveFeature(this)"></i></div></td><td><a class="btn btn-xs btn-danger" style="" onclick="RemoveMappedPrivilages(this)"><i class="fa fa-close"></i> REMOVE</a></td></tr>');
    IsPrivilegesSummaryTableClear = false;
    $("#RoleBPInputField").val("SELECT BUSINESS PROCESS")
    $(".RolesFeaturesCheckbox").prop('checked', false)
    $("#PrivilagesForRole").hide();
}

function AddUserRoleModal(id) {
    //$("#RoleBPInputField").val("");
    CompressModal(id)
    ShowHide("ExpandModalBtn", "CompressModalBtn");
    $("#RoleBPInputField").val("SELECT BUSINESS PROCESS")
    $("#addUserRoleTitle").show();
    $("#addUserRoleBtn").show();
    $("#editUserRoleTitle").hide();
    $("#PrivilagesForRole").hide();
    $(".ClaimsFeaturesCheckbox").prop('checked', false)
    $("#editUserRoleBtn").hide();
    $(".AddUserRoleTextBoxes").val("");
    $("#PrivilegesSummaryTable").empty();
    showModal(id);
}

$(".RoleBPs").on('change', function () {
    var val = this.value;
    if ($('.RoleBPs option').filter(function () {
        return this.value === val;
    }).length) {
        $("#PrivilagesForRole").show();
    }
});

$(".RoleBPs").on('change', function () {
    var val = this.value;
    if ($('.RoleBPs option').filter(function () {
        return this.value === val;
    }).length) {
        $("#PrivilagesForEditUser").show();
    }
});
$("#AddUserBPInputField").on('input', function () {
    var val = this.value;
    if ($('.RoleBPs option').filter(function () {
        return this.value === val;
    }).length) {
        $("#PrivilagesForAddUser").show();
    }
});

$("#PrivilegesSummaryTableForAddUser").empty();
$("#PrivilegesSummaryTableForAddUser").append('<tr><td colspan="9" class="text-center">NO PRIVILEGES SELECTED</td></tr>');
var IsPrivilegesSummaryTableForAddClear = true;
function RemoveMappedPrivilagesForAdd(id) {
    $(id).parent().parent("tr").remove();
    if ($('#PrivilegesSummaryTableForAddUser tr').length == 0) {
        $("#PrivilegesSummaryTableForAddUser").append('<tr><td colspan="9" class="text-center">NO PRIVILEGES SELECTED</td></tr>');
        IsPrivilegesSummaryTableForAddClear = true;
    }
}
function PrivilegesMappingForAddUser() {
    if (IsPrivilegesSummaryTableForAddClear)
        $("#PrivilegesSummaryTableForAddUser").empty();
    $("#PrivilegesSummaryTableForAddUser").append('<tr><td>' + $(".RoleBPs").val() + '</td><td class="text-center"><i class="fa fa-times text-danger"></i></td><td class="text-center">' +
        '<i class="fa fa-check text-success"></i></td><td class="text-center"><i class="fa fa-times text-danger"></i></td><td class="text-center"><i class="fa fa-check text-success"></i></td><td class="text-center"><i class="fa fa-check text-success"></i></td><td class="text-center"><i class="fa fa-times text-danger"></i></td><td class="text-center"><i class="fa fa-check text-success"></i></td><td><a class="btn btn-xs btn-primary" style="" onclick="EditMappedPrivileges(this)"><i class="fa fa-edit"></i> Edit</a><a class="btn btn-xs btn-danger" style="" onclick="RemoveMappedPrivilages(this)"><i class="fa fa-close"></i> Remove</a></td></tr>');
    IsPrivilegesSummaryTableForAddClear = false;
    $(".RoleBPs").val("SELECT BUSINESS PROCESS")
    $(".AddUserFeaturesCheckbox").prop('checked', false)
    $("#PrivilagesForAddUser").hide();
}

function MapPrivilegesUser() {
    $("#PrivilagesForEditUser").hide();
    $("#PrivilageMapTable").append('<tr><td>' + $("#UsrBPInputField").val() + '</td><td><div class="col-lg-4 col-md-4 col-xs-6" style="height: 23px;border : 1px solid lightgray;padding: 3px 6px;border-radius: 10px;margin-left: 13px;box-shadow: 1px 1px 5px lightgray;">Create Auth<i class="fa fa-close pull-right" style="padding: 2px;cursor:pointer;color:red" onclick="RemoveFeature(this)"></i></div><div class="col-lg-4 col-md-4 col-xs-6" style="height: 23px;border : 1px solid lightgray;padding: 3px 6px;border-radius: 10px;margin-left: 13px;box-shadow: 1px 1px 5px lightgray;">View Auth<i class="fa fa-close pull-right" style="padding: 2px;cursor:pointer;color:red" onclick="RemoveFeature(this)"></i></div></td><td class=" text-center"><a class="btn btn-xs btn-danger" style="" onclick="RemoveMappedPrivilages(this)"><i class="fa fa-close"></i> Remove</a></td></tr>');
    $(".MapPrivilegesFeaturesCheckbox").prop('checked', false)
    $("#UsrBPInputField").val("SELECT BUSINESS PROCESS")
}

function AddNewUserRole() {
    new PNotify({
        title: "UserRole Created Successfully",
        type: 'success',
        width: "23%",
        animate: {
            animate: true,
            in_class: "lightSpeedIn",
            out_class: "slideOutRight"
        }
    });
}
function UpdateUserRole() {
    new PNotify({
        title: "UserRole Updated Successfully",
        type: 'success',
        width: "23%",
        animate: {
            animate: true,
            in_class: "lightSpeedIn",
            out_class: "slideOutRight"
        }
    });
}

function ViewUserRole(idvalue) {
    CompressModal(idvalue)
    ShowHide("ExpandModalBtn", "CompressModalBtn");
    $("#PrivilegesSummaryTableForRoleView").empty();
    $.each(RolesData, function (index, value) {
        if (value.ID == idvalue) {

            $("#rolenameforview").text(value.RoleName.toUpperCase());
            $("#roledescriptionforview").text(value.Description.toUpperCase());
            $.each(value.BusinessProcesses, function (i, v) {
                $("#PrivilegesSummaryTableForRoleView").append('<tr><td>' + v + '</td><td>CREATE AUTH , VIEW AUTH</td></tr>');
            });
        }
    });
    showModal('ViewUserRoleModal');
}
function EditUserRole(idvalue) {
    CompressModal(idvalue)
    ShowHide("ExpandModalBtn", "CompressModalBtn");
    $("#RoleBPInputField").val("SELECT BUSINESS PROCESS")
    $("#addUserRoleTitle").hide();
    $("#PrivilagesForRole").hide();
    $("#addUserRoleBtn").hide();
    $("#editUserRoleTitle").show();
    $("#editUserRoleBtn").show();
   // $("#RoleBPInputField").val("");
    $("#PrivilegesSummaryTable").empty();
    IsPrivilegesSummaryTableClear = false;
    $.each(RolesData, function (index, value) {
        if (value.ID == idvalue) {

            $("#RoleName").val(value.RoleName.toUpperCase());
            $("#RoleDescription").val(value.Description.toUpperCase());
            $.each(value.BusinessProcesses, function (i, v) {
                $("#PrivilegesSummaryTable").append('<tr><td>' + v + '</td><td><div class="col-lg-4 col-md-4 col-xs-6" style="height: 23px;border : 1px solid lightgray;padding: 3px 6px;border-radius: 10px;margin-left: 13px;box-shadow: 1px 1px 5px lightgray;">CREATE AUTH<i class="fa fa-close pull-right" style="padding: 2px;cursor:pointer;color:red" onclick="RemoveFeature(this)"></i></div><div class="col-lg-4 col-md-4 col-xs-6" style="height: 23px;border : 1px solid lightgray;padding: 3px 6px;border-radius: 10px;margin-left: 13px;box-shadow: 1px 1px 5px lightgray;">VIEW AUTH<i class="fa fa-close pull-right" style="padding: 2px;cursor:pointer;color:red" onclick="RemoveFeature(this)"></i></div></td><td><a class="btn btn-xs btn-danger" style="" onclick="RemoveMappedPrivilages(this)"><i class="fa fa-close"></i> REMOVE</a></td></tr>');

            });
        }
    });
    showModal('AddUserRoleModal');
}
function UserPrivilegesMappingModal() {
    $("#PrivilagesForEditUser").hide();
    $(".AddUserTextBoxes").val("");
    $("#UsrBPInputField").val("SELECT BUSINESS PROCESS")
   // $("#UsrBPInputField").val("");
    $(".ClaimsFeaturesCheckbox").prop('checked', false)
    showModal('UserPrivilegesMappingModal');

}

//------------------------------------ T E A M S-----------------------------------------


function TeamsTabActive() {
    $("#topMenuBPText").text("All");
    $("#topMenuAccText").text("All");
    $("#topMenuDeptText").text("All");
    $("#TopMenu").show();
    $("#teams").parent().addClass("work_menu_special_item")
    $("#users").parent().removeClass("work_menu_special_item")
    $("#departments").parent().removeClass("work_menu_special_item")
    $("#userroles").parent().removeClass("work_menu_special_item")
    $("#accounts").parent().removeClass("work_menu_special_item")
    $("#businessProcesses").parent().removeClass("work_menu_special_item")
    $("#businessProcesses").removeClass("active");
    $("#accounts").removeClass("active");
    $("#userroles").removeClass("active");
    $("#departments").removeClass("active");
    $("#users").removeClass("active");
    $("#teams").addClass("active");
    $("#AccountsListData").hide();
    $("#BusinessProcessesData").hide();
    $(".AddNewUser").hide();
    $("#ViewAccountData").hide();
    $("#EditAccountData").hide();
    $("#ViewDepartmentsData").hide();
    $("#ViewUserRolesData").hide();
    $("#ViewUsersData").hide();
    $("#ViewTeamsData").show();

    $(".AccountsTopMenuL").css({ "opacity": "1", "box-shadow": "5px 5px 10px" });
    $(".AccountsTopMenuR").css({ "opacity": "1" });
    $("#AllAccs").hide();
    $("#AllAccsDropdown").show();
    $(".BusinessProcessesTopMenuR").css({ "opacity": "1" });
    $(".BusinessProcessesTopMenuL").css({ "opacity": "1", "box-shadow": "5px 5px 10px" });
    $("#AllBPs").hide();
    $("#AllBPsDropdown").show();
    $("#AllTeams").show();
    $("#AllTeamsDropdown").hide();
    $(".TeamsTopMenuR").css({ "opacity": "0.3" });
    $(".TeamsTopMenuL").css({ "opacity": "0.3", "box-shadow": "0px 0px 0px" });
    $("#AllDepts").hide();
    $("#AllDeptsDropdown").show();
    $(".DepartmentsTopMenuR").css({ "opacity": "1" });
    $(".DepartmentsTopMenuL").css({ "opacity": "1", "box-shadow": "5px 5px 10px" });
}

$("#teams").click(function () {
    TeamsTabActive();
});

function AddTeamModal() {
    $("#addTeamTitle").show();
    $("#editTeamTitle").hide();
    $("#addTeamBtn").show();
    $("#editTeamBtn").hide();
    $("#mappedusrinfo").show();
    $(".TeamInputFields").val("")
    $("#BPListForNewTeam").val("SELECT BUSINESS PROCESS")
    $("#AccountListForNewTeam").val("SELECT ACCOUNT")
    $(".teaRadioBtn").prop("checked", false);
    $("#MappedUsersListTableData").empty();

    $("#MappedUsersListTableData").append('<tr><td class="text-center" colspan="4">NO USERS MAPPED</td></tr>');
    $(".ListAllUsers").val("SELECT USER");
    showModal('AddTeamModal');
}

function UpdateTeam() {
    new PNotify({
        title: "Team Updated Successfully",
        type: 'success',
        width: "23%",
        animate: {
            animate: true,
            in_class: "lightSpeedIn",
            out_class: "slideOutRight"
        }
    });
}
function AddNewTeam() {
    new PNotify({
        title: "Team Created Successfully",
        type: 'success',
        width: "23%",
        animate: {
            animate: true,
            in_class: "lightSpeedIn",
            out_class: "slideOutRight"
        }
    });
}

var UserNameForTeamMapping;
$(".ListAllUsers").on('change', function () {
    var val = this.value;
    if ($('.ListAllUsers option').filter(function () {
        return this.value === val;
    }).length) {
        $("#mappedusrinfo").show();
        //var fieldVal = val;
        //var data = fieldVal.split('-')
        UserNameForTeamMapping = val;

    }
});
$("#MappedUsersListTableData").empty();
$("#MappedUsersListTableData").append('<tr><td class="text-center" colspan="4">NO USERS MAPPED</td></tr>');
var MappedTeamUserTableClear = true;
function AddUserToTeam() {
    if ($("#MappedUsersListTableData tr").children().text() == "NO USERS MAPPED")
        $("#MappedUsersListTableData").empty();
    $.each(Members, function (index, value) {
        if (value.PersonalInformation.ID == UserNameForTeamMapping) {
            $("#MappedUsersListTableData").append('<tr><td>' + value.PersonalInformation.ID + '</td><td>' + value.PersonalInformation.FirstName.toUpperCase() + '</td><td>' + value.PersonalInformation.LastName.toUpperCase() + '</td><td class="text-center"><a class="btn btn-xs btn-danger" style="" onclick="RemoveMappedUserForTeam(this)"><i class="fa fa-close"></i> REMOVE</a></td></tr>');
        }

    });
   
    MappedTeamUserTableClear = false;
    $("#mappedusrinfo").show();
    $(".teaRadioBtn").prop("checked",false);
    $("#usridmap").val("")
    $(".ListAllUsers").val("SELECT USER")
    $("#UserRoles").val("SELECT ROLE")
    UserNameForTeamMapping = 0;
}
function RemoveMappedUserForTeam(id) {
    $(id).parent().parent("tr").remove();
    if ($('#MappedUsersListTableData tr').length == 0) {
        $("#MappedUsersListTableData").append('<tr><td class="text-center" colspan="4">NO USERS MAPPED</td></tr>');
        MappedTeamUserTableClear = true;
    }
}
var TeamIDForEdit;
function ViewTeam(idvalue) {
    TeamIDForEdit = idvalue;
    $("#ViewMappedUsersListTableData").empty();
    $.each(TeamsData, function (index, value) {

        if (idvalue == value.ID) {
            $("#viewteamname").text(value.TeamName.toUpperCase());
            $("#viewteamaccount").text(value.Account.toUpperCase());
            $("#viewteambp").text(value.BusinessProcess.toUpperCase());
            $("#viewteamdepartment").text(value.Department.toUpperCase());
            $("#viewteamcreatedon").text(value.CreatedOn);
            $.each(value.Users, function (ind, val) {
                $.each(Members, function (i, v) {
                    if (v.PersonalInformation.ID == val) {
                        $("#ViewMappedUsersListTableData").append('<tr><td>' + v.PersonalInformation.ID + '</td><td>' + v.PersonalInformation.FirstName.toUpperCase() + '</td><td>' + v.PersonalInformation.LastName.toUpperCase() + '</td><td class="text-center"><a class="btn btn-primary btn-xs"  data-toggle="tooltip" data-dismiss="modal" data-placement="left" title="View User" onclick="ViewUser(\'' + v.PersonalInformation.ID + '\')" ><i class="fa fa-eye"></i> View</a></td></tr>');
                    }
                    $("[data-toggle=tooltip]").tooltip();
                });
            });
        }
    });
    showModal('ViewTeamModal');
}

function EditTeam(idvalue) {
    if (!idvalue)
        idvalue = TeamIDForEdit
    TeamIDForEdit = 0;
    $("#addTeamTitle").hide();
    $("#mappedusrinfo").show();
    $("#editTeamTitle").show();
    $("#addTeamBtn").hide();
    $("#editTeamBtn").show();
    $(".TeamInputFields").val("");
    $("#MappedUsersListTableData").empty();
    $.each(TeamsData, function (index, value) {
        if (idvalue == value.ID) {
            $("#teamName").val(value.TeamName.toUpperCase());
            $("#teamName").val(value.TeamName.toUpperCase());
            $("#teamName").val(value.TeamName.toUpperCase());
            $("#teamName").val(value.TeamName.toUpperCase());
            $.each(value.Users, function (ind, val) {
                $.each(Members, function (i, v) {
                    if (v.PersonalInformation.ID == val) {
                        $("#MappedUsersListTableData").append('<tr><td>' + v.PersonalInformation.ID + '</td><td>' + v.PersonalInformation.FirstName.toUpperCase() + '</td><td>' + v.PersonalInformation.LastName.toUpperCase() + '</td><td class="text-center"><a class="btn btn-xs btn-danger" style="" onclick="RemoveMappedUserForTeam(this)"><i class="fa fa-close"></i> REMOVE</a></td></tr>');
                    }
                });
            });
        }
    });
    $(".ListAllUsers").val("SELECT USER");
    
    MappedTeamUserTableClear = false;
    showModal('AddTeamModal');
}
