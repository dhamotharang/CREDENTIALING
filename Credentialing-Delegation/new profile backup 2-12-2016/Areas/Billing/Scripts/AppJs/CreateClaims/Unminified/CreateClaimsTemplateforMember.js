var resultDivId = null;
var searchString = null;
var searchType = null;
var currentLabel = null;
var currentInput = null;
var CurrentSearchBtn = null;

//edit the value of RenderingProvider
//$('#CreateClaimsTempRenderingProviderInput').hide();
//$('#CreateClaimsTempForProviderRenderingProviderSearchButtons').hide();
//$("#CreateClaimsTempForProviderRenderingProviderEditButtons").click(function () {
//    $('#CreateClaimsTempForProviderRenderingProviderLabel').hide();
//    $('#CreateClaimsTempRenderingProviderInput').show();
//    $('#CreateClaimsTempForProviderBillingProviderEditButtons').hide();
//    $('#CreateClaimsTempForProviderRenderingProviderEditButtons').hide();
//    $('#CreateClaimsTempForProviderReferringProviderEditButtons').hide();
//    $('#CreateClaimsTempForMemberSupervisingProviderEditButtons').hide();
//    $('#CreateClaimsTempForMemberFacilityEditButtons').hide();
//    $('#CreateClaimsTempForProviderRenderingProviderSearchButtons').show();
//});
//update the value of RenderingProvider
//$("#CreateClaimsTempForProviderRenderingProviderSearchButtons").click(function () {
//    GetResult('/Billing/CreateClaim/GetRenderingProviderResult', 'CreateClaimsTempForProviderRenderingProviderLabel', 'CreateClaimsTempProvidersSearchResult');
//    searchString = $('#CreateClaimsTempRenderingProvider').val();
//    searchType = "RENDERING PROVIDER";
//    currentLabel = 'CreateClaimsTempForProviderRenderingProviderLabel';
//    currentInput = 'CreateClaimsTempRenderingProviderInput';
//    CurrentSearchBtn = 'CreateClaimsTempForProviderRenderingProviderSearchButtons';

   
//});

//edit the value of BillingProvider
$('#CreateClaimsTempForProviderBillingProviderEditButtons').on('click', function (e) {
    e.preventDefault();
    GetResult('/Billing/CreateClaim/GetBillingProviderResult', 'CreateClaimsTempProvidersSearchResult');
});

$('#CreateClaimsTempForProviderRenderingProviderEditButtons').on('click', function (e) {
    e.preventDefault();
    GetResult('/Billing/CreateClaim/GetRenderingProviderResult', 'CreateClaimsTempProvidersSearchResult');
});

$('#CreateClaimsTempForProviderReferringProviderEditButtons').on('click', function (e) {
    e.preventDefault();
    GetResult('/Billing/CreateClaim/GetReferringProviderResult', 'CreateClaimsTempProvidersSearchResult');
});

$('#CreateClaimsTempForMemberSupervisingProviderEditButtons').on('click', function (e) {
    e.preventDefault();
    GetResult('/Billing/CreateClaim/GetSupervisingProviderResult', 'CreateClaimsTempSupervisingProvidersSearchResult');
});


//edit the value of ReferringProvider
//$('#CreateClaimsTempReferringProviderInput').hide();
//$('#CreateClaimsTempForProviderReferringProviderSearchButtons').hide();
//$("#CreateClaimsTempForProviderReferringProviderEditButtons").click(function () {
//    $('#CreateClaimsTempForProviderReferringProviderLabel').hide();
//    $('#CreateClaimsTempReferringProviderInput').show();
//    $('#CreateClaimsTempForProviderBillingProviderEditButtons').hide();
//    $('#CreateClaimsTempForProviderRenderingProviderEditButtons').hide();
//    $('#CreateClaimsTempForProviderReferringProviderEditButtons').hide();
//    $('#CreateClaimsTempForMemberSupervisingProviderEditButtons').hide();
//    $('#CreateClaimsTempForMemberFacilityEditButtons').hide();

//    $('#CreateClaimsTempForProviderReferringProviderSearchButtons').show();
//});
//update the value of ReferringProvider
//$("#CreateClaimsTempForProviderReferringProviderSearchButtons").click(function () {
//    GetResult('/Billing/CreateClaim/GetReferringProviderResult', 'CreateClaimsTempForProviderReferringProviderLabel', 'CreateClaimsTempProvidersSearchResult');
//    searchString = $('#CreateClaimsTempReferringProvider').val();
//    searchType = "REFERRING PROVIDER";
//    currentLabel = 'CreateClaimsTempForProviderReferringProviderLabel';
//    currentInput = 'CreateClaimsTempReferringProviderInput';
//    CurrentSearchBtn = 'CreateClaimsTempForProviderReferringProviderSearchButtons';

//});
//edit the value of SupervisingProvider
//$('#CreateClaimsTempMemberSupervisingProviderInput').hide();
//$('#CreateClaimsTempForProviderSupervisingMemberSearchButtons').hide();
//$("#CreateClaimsTempForMemberSupervisingProviderEditButtons").click(function () {
//    $('#CreateClaimsTempForMemberSupervisingProviderLabel').hide();
//    $('#CreateClaimsTempMemberSupervisingProviderInput').show();
//    $('#CreateClaimsTempForProviderBillingProviderEditButtons').hide();
//    $('#CreateClaimsTempForProviderRenderingProviderEditButtons').hide();
//    $('#CreateClaimsTempForProviderReferringProviderEditButtons').hide();
//    $('#CreateClaimsTempForMemberSupervisingProviderEditButtons').hide();
//    $('#CreateClaimsTempForMemberFacilityEditButtons').hide();

//    $('#CreateClaimsTempForProviderSupervisingMemberSearchButtons').show();
//});
//update the value of SupervisingProvider
//$("#CreateClaimsTempForProviderSupervisingMemberSearchButtons").click(function () {
//    GetResult('/Billing/CreateClaim/GetSupervisingProviderResult', 'CreateClaimsTempForMemberSupervisingProviderLabel', 'CreateClaimsTempSupervisingProvidersSearchResult');
//    searchString = $('#CreateClaimsTempMemberSupervisingProvider').val();
//    searchType = "SUPERVISING PROVIDER";
//    currentLabel = 'CreateClaimsTempForMemberSupervisingProviderLabel';
//    currentInput = 'CreateClaimsTempMemberSupervisingProviderInput';
//    CurrentSearchBtn = 'CreateClaimsTempForProviderSupervisingMemberSearchButtons';

   
//});

//edit the value of Facility
//$('#CreateClaimsTempMemberFacilityInput').hide();
//$('#CreateClaimsTempForMemberFacilitySearchButtons').hide();
//$("#CreateClaimsTempForMemberFacilityEditButtons").click(function () {
//    $('#CreateClaimsTempForMemberFacilityLabel').hide();
//    $('#CreateClaimsTempMemberFacilityInput').show();
//    $('#CreateClaimsTempForProviderBillingProviderEditButtons').hide();
//    $('#CreateClaimsTempForProviderRenderingProviderEditButtons').hide();
//    $('#CreateClaimsTempForProviderReferringProviderEditButtons').hide();
//    $('#CreateClaimsTempForMemberSupervisingProviderEditButtons').hide();
//    $('#CreateClaimsTempForMemberFacilityEditButtons').hide();

//    $('#CreateClaimsTempForMemberFacilitySearchButtons').show();
//});
//update the value of Facility
//$("#CreateClaimsTempForMemberFacilitySearchButtons").click(function () {
//    GetResult('/Billing/CreateClaim/GetFacilityResult', 'CreateClaimsTempForMemberFacilityLabel', 'CreateClaimsTempFacilitySearchResult');
//    searchString = $('#CreateClaimsTempMemberFacility').val();
//    searchType = "FACILITY";
//    currentLabel = 'CreateClaimsTempForMemberFacilityLabel';
//    currentInput = 'CreateClaimsTempMemberFacilityInput';
//    CurrentSearchBtn = 'CreateClaimsTempForMemberFacilitySearchButtons';

   
//});

/** 
@description used to get result of billing provider,rendering provider, supervising provider, facility based on search string.

 */
function GetResult(link,resultDivID) {

    $.ajax({
        type: 'GET',
        url: link ,
        success: function (data) {

            $('#'+resultDivID).html(data);
        }
    });
}

/** 
@description navigating to create claims template.
 */

$('#create_claim_template_member_btn').on('click', function () {

    currentProgressBarData[3].postData = new FormData($('#CreateClaimTemplateForm')[0]);
   // currentProgressBarData[3].parameters[0].value = { MemberId: 1, BillingProviderId: 1, FacilityId: 1, RenderingProviderId: 1, RefferingProviderId: 1, SupervisingProviderId: 1 };
    MakeItActive(4, currentProgressBarData);
});


/** 
@description navigating to search member page.
 */
$('#search_member_btn').on('click', function () {
    MakeItVisibleWithDataFlush(2, currentProgressBarData);
});


/** 
@description navigation to create claims template page
 */
$('#GoToSelectProviderMember_member_Btn').on('click', function () {
    if (createClaimByType != 'Multiple claims for a Provider') {
        MakeItVisible(2, currentProgressBarData);
    } else {
        MakeItVisible(3, currentProgressBarData);
    }
});