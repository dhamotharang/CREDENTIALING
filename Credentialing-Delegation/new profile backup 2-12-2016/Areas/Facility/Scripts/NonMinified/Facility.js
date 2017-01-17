$(document).ready(function () {
    $("[data-toggle=tooltip]").tooltip();
});
//Check All and Uncheck for table
function CheckForAllCheckboxSelection(current,classValue,id) {
    $("." + classValue).prop('checked', $(current).prop("checked"));//change all ".checkbox" checked status
}

function CheckForCheckboxSelection(current, classValue, id) {
    if (false == $(current).prop("checked")) { //if this item is unchecked
        $("#" + id).prop('checked', false); //change "select all" checked status to false
    }
    //check "select all" if all checkbox items are checked
    if ($('.' + classValue + ':checked').length == $('.' + classValue).length) {
        $("#" + id).prop('checked', true);
    }
}

//-------------------------------------------
var IDforEditFacility;

// Hide content by ID
function Hide(id) {
    $("#" + id).hide();
}
// Show content by ID
function Show(id) {
    $("#" + id).show();
}

// Toggle 2 views
function ShowHide(showId, hideId) {
    $("#" + showId).show();
    $("#" + hideId).hide();
}

function ShowAssignTo(id) {
    if ($("."+id).is(":checked")) {
        Show('AssignToDiv')
    }
    else
        Hide('AssignToDiv')

}


function EditFacility(id) {
    TabManager.openFloatingModal("/Facility/FacilityBridge/GetQueueDataForEdit?id=" + id, "~/Areas/Facility/Views/AddFacility/_FacilityEditHeader.cshtml", "~/Areas/Facility/Views/AddFacility/_FacilityEditFooter.cshtml");

}
function ViewFacility(id) {
    IDforEditFacility = id;
    TabManager.openFloatingModal("/Facility/FacilityBridge/GetQueueDataForView?id=" + id, "~/Areas/Facility/Views/AddFacility/_FacilityViewHeader.cshtml", "~/Areas/Facility/Views/AddFacility/_FacilityViewFooter.cshtml");  
}
function AssignFacility() {
    TabManager.openFloatingModal("~/Areas/Facility/Views/ConfirmationPrompts/_AssignPromptBody.cshtml", "~/Areas/Facility/Views/ConfirmationPrompts/_AssignPromptHeader.cshtml", "~/Areas/Facility/Views/ConfirmationPrompts/_AssignPromptFooter.cshtml");

}
$('#fullBodyContainer').off('click', '#AddFacilityModal').on('click', '#AddFacilityModal', function () {
    TabManager.openFloatingModal("~/Areas/Facility/Views/AddFacility/_FacilityBody.cshtml", "~/Areas/Facility/Views/AddFacility/_FacilityAddHeader.cshtml", "~/Areas/Facility/Views/AddFacility/_FacilityAddFooter.cshtml");
})

function SwitchViewToEdit() {
    EditFacility(IDforEditFacility)
}
function CloseModal(id, type,approveID,pendingID) {
    if (type != "Add" )
    {
        if ($("#" + approveID).is(":checked")) {
            TabManager.openFloatingModal("~/Areas/Facility/Views/ConfirmationPrompts/_ApprovePromptBody.cshtml", "~/Areas/Facility/Views/ConfirmationPrompts/_ApprovePromptHeader.cshtml", "~/Areas/Facility/Views/ConfirmationPrompts/_ApprovePromptFooter.cshtml");
        }
        else if ($("#" + pendingID).is(":checked")) {
            TabManager.openFloatingModal("~/Areas/Facility/Views/ConfirmationPrompts/_PendingPromptBody.cshtml", "~/Areas/Facility/Views/ConfirmationPrompts/_PendingPromptHeader.cshtml", "~/Areas/Facility/Views/ConfirmationPrompts/_PendingPromptFooter.cshtml");
        }
        else
            $("#" + id).modal('hide');
    }
    else
         $("#" + id).modal('hide');

}

function CheckForRequestStatus(approve, pending) {
    if ($("#" + approve).is(":checked")) {
        $("#ConfirmBtn").prop("disabled",false)
    }
    else if ($("#" + pending).is(":checked")) {
        $("#ConfirmBtn").prop("disabled", false)
    }
    else {
        $("#ConfirmBtn").prop("disabled", true)
    }
}


