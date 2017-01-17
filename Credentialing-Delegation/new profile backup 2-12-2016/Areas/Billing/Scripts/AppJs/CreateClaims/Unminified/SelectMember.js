//Hide & show of Provider Divs 
$(document).ready(function () {
    //edit the value of BillingProvider
    $('#SelectedMemberBillingProvider').hide();
    $('#SelectedMemberBillingProviderAddButtons').hide();
    $("#SelectedMemberBillingProviderEditButtons").click(function () {
        $('#SelectedMemberBillingProviderLabel').hide("slow");
        $('#SelectedMemberBillingProvider').show("slow");
        $('#SelectedMemberBillingProviderEditButtons').hide();
        $('#SelectedMemberBillingProviderAddButtons').show();
    });
    //update the value of BillingProvider
    $("#SelectedMemberBillingProviderAddButtons").click(function () {
        $('#SelectedMemberBillingProviderLabel').html("");
        $('#SelectedMemberBillingProviderLabel').append($('#SelectedMemberBillingProvider').val());
        $('#SelectedMemberBillingProviderLabel').show("slow");
        $('#SelectedMemberBillingProvider').hide("slow");
        $('#SelectedMemberBillingProviderEditButtons').show();
        $('#SelectedMemberBillingProviderAddButtons').hide();
    });
    //to edit the value of RenderingProvider
    $('#SelectedMemberRenderingProvider').hide();
    $('#SelectedMemberRenderingProviderAddButtons').hide();
    $("#SelectedMemberRenderingProviderEditButtons").click(function () {
        $('#SelectedMemberRenderingProviderLabel').hide("slow");
        $('#SelectedMemberRenderingProvider').show("slow");
        $('#SelectedMemberRenderingProviderEditButtons').hide();
        $('#SelectedMemberRenderingProviderAddButtons').show();
    });
    //update the value of RenderingProvider
    $("#SelectedMemberRenderingProviderAddButtons").click(function () {
        $('#SelectedMemberRenderingProviderLabel').html("");
        $('#SelectedMemberRenderingProviderLabel').append($('#SelectedMemberRenderingProvider').val());
        $('#SelectedMemberRenderingProviderLabel').show("slow");
        $('#SelectedMemberRenderingProvider').hide("slow");
        $('#SelectedMemberRenderingProviderEditButtons').show();
        $('#SelectedMemberRenderingProviderAddButtons').hide();
    });
    //edit the value of ReferringProvider
    $('#SelectedMemberReferringProvider').hide();
    $('#SelectedMemberReferringProviderAddButtons').hide();
    $("#SelectedMemberReferringProviderEditButtons").click(function () {
        $('#SelectedMemberReferringProviderLabel').hide("slow");
        $('#SelectedMemberReferringProvider').show("slow");
        $('#SelectedMemberReferringProviderEditButtons').hide();
        $('#SelectedMemberReferringProviderAddButtons').show();
    });
    //update the value of ReferringProvider
    $("#SelectedMemberReferringProviderAddButtons").click(function () {
        $('#SelectedMemberReferringProviderLabel').html("");
        $('#SelectedMemberReferringProviderLabel').append($('#SelectedMemberReferringProvider').val());
        $('#SelectedMemberReferringProviderLabel').show("slow");
        $('#SelectedMemberReferringProvider').hide("slow");
        $('#SelectedMemberReferringProviderEditButtons').show();
        $('#SelectedMemberReferringProviderAddButtons').hide();
    });
    //edit the value of Facility
    $('#SelectedMemberFacility').hide();
    $('#SelectedMemberFacilityAddButtons').hide();
    $("#SelectedMemberFacilityEditButtons").click(function () {
        $('#SelectedMemberFacilityLabel').hide("slow");
        $('#SelectedMemberFacility').show("slow");
        $('#SelectedMemberFacilityEditButtons').hide();
        $('#SelectedMemberFacilityAddButtons').show();
    });
    //update the value of Facility
    $("#SelectedMemberFacilityAddButtons").click(function () {
        $('#SelectedMemberFacilityLabel').html("");
        $('#SelectedMemberFacilityLabel').append($('#SelectedMemberFacility').val());
        $('#SelectedMemberFacilityLabel').show("slow");
        $('#SelectedMemberFacility').hide("slow");
        $('#SelectedMemberFacilityEditButtons').show();
        $('#SelectedMemberFacilityAddButtons').hide();
    });
    //edit the value of SupervisingProvider
    $('#SelectedMemberSupervisingProvider').hide();
    $('#SelectedMemberSupervisingProviderAddButtons').hide();
    $("#SelectedMemberSupervisingProviderEditButtons").click(function () {
        $('#SelectedMemberSupervisingProviderLabel').hide("slow");
        $('#SelectedMemberSupervisingProvider').show("slow");
        $('#SelectedMemberSupervisingProviderEditButtons').hide();
        $('#SelectedMemberSupervisingProviderAddButtons').show();
    });
    //update the value of SupervisingProvider
    $("#SelectedMemberSupervisingProviderAddButtons").click(function () {
        $('#SelectedMemberSupervisingProviderLabel').html("");
        $('#SelectedMemberSupervisingProviderLabel').append($('#SelectedMemberSupervisingProvider').val());
        $('#SelectedMemberSupervisingProviderLabel').show("slow");
        $('#SelectedMemberSupervisingProvider').hide("slow");
        $('#SelectedMemberSupervisingProviderEditButtons').show();
        $('#SelectedMemberSupervisingProviderAddButtons').hide();
    });
});