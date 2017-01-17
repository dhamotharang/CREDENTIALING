$(function () {
    $('[data-toggle="tooltip"]').tooltip();
    var $AllInputFields = $('#fId, #fNameInput, #fTypeInput, #taxIdInput, #orgNameInput, #phoneInput');
    $('#fullBodyContainer').css('overflow', 'hidden');
    $AllInputFields.on('keyup', function () {
        var empty = checkIfAllInputFieldsAreEmpty($AllInputFields);
        if (empty == true) {
            $('#searchFacilityButton').prop("disabled", true);
            $('#clearSearchButton').prop("disabled", true);
        }
        else {
            $('#searchFacilityButton').prop("disabled", false);
            $('#clearSearchButton').prop("disabled", false);
        }
    });
    $('#inputFieldsTable').keypress(function (e) {
        if (e.which == 13) {
            if (checkIfAllInputFieldsAreEmpty($AllInputFields) == false) {
                initSearch();
            }
        }
    });
    $('#clearSearchButton').on('click', function () {
        $('#tableWrapper').hide();
        $('#resultCountSection').hide();
        $AllInputFields.val("");
        $('#searchFacilityButton').prop("disabled", true);
        $('#clearSearchButton').prop("disabled", true);
        $("#resultTableHeader .fa").removeClass('fa-sort-asc fa-sort-desc').addClass('fa-sort').css('color', 'white');
    });
    $('.MemberSearchfilterBtn').on('click', function () {
        ShowSearchFilters('MemberSearchfilterBtn', 'resultTableHeader');
    })
});
$('#QueueName').hide();
var searchParam = [];
function checkIfAllInputFieldsAreEmpty(AllInputFields) {
    var empty = true;
    $.each(AllInputFields, function (index, element) {
        if (element.value != "") {
            empty = false;
        }
    });
    return empty;
}

function initSearch() {
    var fId = $('#fId').val();
    var fNameInput = $('#fNameInput').val();
    var fTypeInput = $('#fTypeInput').val();
    var taxIdInput = $('#taxIdInput').val();
    var orgNameInput = $('#orgNameInput').val();
    var phoneInput = $('#phoneInput').val();
    SearchMembersByFactors(fId, fNameInput, fTypeInput, taxIdInput, orgNameInput, phoneInput);
}

function SearchMembersByFactors(mbrid, lastname, firstname, hicn, medicaid, phone) {
    var filteredResult;
    filterResult = {
        "SubscriberID": mbrid,
        "LastName": lastname,
        "FirstName": firstname,
        "HICN": hicn,
        "MEDICAID": medicaid,
        "Phone": phone
    };

    filterByParticularField(filterResult);
}

function filterByParticularField(fields) {
    $('#tableWrapper').show();
    $.ajax({
        type: "post",
        url: '/Member/SearchMembers',
        data: fields,
        success: function (response) {

            $("#searchResultGrid").empty();
            $("#searchResultGrid").html(response);
        }
    });
}
function EditFacility(id) {
    TabManager.openFloatingModal("/Facility/FacilityBridge/GetQueueDataForEdit?id=" + id, "~/Areas/Facility/Views/AddFacility/_FacilityEditHeader.cshtml", "~/Areas/Facility/Views/AddFacility/_FacilityEditFooter.cshtml");

}
var IDforEditFacility;
function ViewFacility(id) {
    IDforEditFacility = id;
    TabManager.openFloatingModal("/Facility/FacilityBridge/GetQueueDataForView?id=" + id, "~/Areas/Facility/Views/AddFacility/_FacilityViewHeader.cshtml", "~/Areas/Facility/Views/AddFacility/_FacilityViewFooter.cshtml");
}
// Hide content by ID
function Hide(id) {
    $("#" + id).hide();
}
// Show content by ID
function Show(id) {
    $("#" + id).show();
}
function SwitchViewToEdit() {
    EditFacility(IDforEditFacility)
}
$('#fullBodyContainer').off('click', '#AddFacilityModal').on('click', '#AddFacilityModal', function () {
    TabManager.openFloatingModal("~/Areas/Facility/Views/AddFacility/_FacilityBody.cshtml", "~/Areas/Facility/Views/AddFacility/_FacilityAddHeader.cshtml", "~/Areas/Facility/Views/AddFacility/_FacilityAddFooter.cshtml");
})
function ActivateFacility() {
    TabManager.openFloatingModal("~/Areas/Facility/Views/ConfirmationPrompts/_ActivateFacilityPromptBody.cshtml", "~/Areas/Facility/Views/ConfirmationPrompts/_ActivateFacilityPromptHeader.cshtml", "~/Areas/Facility/Views/ConfirmationPrompts/_ActivateFacilityPromptFooter.cshtml");
}
function DeactivateFacility() {
    TabManager.openFloatingModal("~/Areas/Facility/Views/ConfirmationPrompts/_DeactivateFacilityPromptBody.cshtml", "~/Areas/Facility/Views/ConfirmationPrompts/_DeactivateFacilityPromptHeader.cshtml", "~/Areas/Facility/Views/ConfirmationPrompts/_DeactivateFacilityPromptFooter.cshtml");
}
