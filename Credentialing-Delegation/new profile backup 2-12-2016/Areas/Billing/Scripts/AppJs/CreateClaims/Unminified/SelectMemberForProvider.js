
/** 
@description Hiding and showing provider divs
 */
$('#SelectedMemberForProviderBillingProvider').hide();
$('#SelectedMemberForProviderBillingProviderAddButtons').hide();
$("#SelectedMemberForProviderBillingProviderEditButtons").click(function () {
    $('#SelectedMemberForProviderBillingProviderLabel').hide("slow");
    $('#SelectedMemberForProviderBillingProvider').show("slow");
    $('#SelectedMemberForProviderBillingProviderEditButtons').hide();
    $('#SelectedMemberForProviderBillingProviderAddButtons').show();
});


$('#SelectedMemberForProviderReferringProvider').hide();
$('#SelectedMemberForProviderReferringProviderAddButtons').hide();
$("#SelectedMemberForProviderReferringProviderEditButtons").click(function () {
    $('#SelectedMemberForProviderReferringProviderLabel').hide("slow");
    $('#SelectedMemberForProviderReferringProvider').show("slow");
    $('#SelectedMemberForProviderReferringProviderEditButtons').hide();
    $('#SelectedMemberForProviderReferringProviderAddButtons').show();
});

$('#SelectedMemberForProviderFacility').hide();
$('#SelectedMemberForProviderFacilityAddButtons').hide();
$("#SelectedMemberForProviderFacilityEditButtons").click(function () {
    $('#SelectedMemberForProviderFacilityLabel').hide("slow");
    $('#SelectedMemberForProviderFacility').show("slow");
    $('#SelectedMemberForProviderFacilityEditButtons').hide();
    $('#SelectedMemberForProviderFacilityAddButtons').show();
});

$('#SelectedMemberForProviderSupervisingProvider').hide();
$('#SelectedMemberForProviderSupervisingProviderAddButtons').hide();
$("#SelectedMemberForProviderSupervisingProviderEditButtons").click(function () {
    $('#SelectedMemberForProviderSupervisingProviderLabel').hide("slow");
    $('#SelectedMemberForProviderSupervisingProvider').show("slow");
    $('#SelectedMemberForProviderSupervisingProviderEditButtons').hide();
    $('#SelectedMemberForProviderSupervisingProviderAddButtons').show();
});
