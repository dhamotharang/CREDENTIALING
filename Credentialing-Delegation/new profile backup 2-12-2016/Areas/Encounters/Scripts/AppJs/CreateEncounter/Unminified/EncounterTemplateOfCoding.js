$('#CDocumentHistoryBtn').on('click', function (e) {
    e.preventDefault();
    $.ajax({
        type: 'GET',
        url: '/Encounters/CreateEncounter/GetDocumentHistory',
        processData: false,
        contentType: false,
        success: function (result) {
            $('#CDocumentHistoryContainer').html(result);
        }
    });
});


$('#CRevertProviderEncounterPage').on('click', function () {
    MakeItVisible(2, currentProgressBarData);
});

$('#CRevertMemberEncounterPage').on('click', function () {
    if (createClaimByType == 'Multiple claims for a Provider') {
        MakeItVisible(3, currentProgressBarData);
    } else {
        MakeItVisible(2, currentProgressBarData);
    }

});

$('.CProviderEditBtn').on('click', function () {
    $('.ProviderLabel').show();
    $(this).parent('.ProviderLabel').hide();
    $('.ProviderSearch').hide();
    var ContainerId = $(this).attr('data-url');
    $(ContainerId).show();
    $('#CProviderList').html('');
    $('#CProviderList').hide();
});

function DisplayProviderList(url, searchString, type) {
    $.ajax({
        type: 'GET',
        url: url,
        processData: false,
        contentType: false,
        success: function (result) {
            $('#CProviderList').html(result);
            $('#CProviderList').show();
            $('#providers_title').html('YOU SEARCHED FOR "<b>' + searchString + '</b>" | <b>5</b> ' + type + ' RESULTS FOUND');
        }
    });

}

$('#CReferingProviderSearchBtn').on('click', function (e) {
    e.preventDefault();
    DisplayProviderList('/Encounters/CreateEncounter/GetReferingProviderList', $('[name="ReferringProvider"]').val(), 'REFERRING PROVIDER');
});

$('#CBillingProviderSearchBtn').on('click', function (e) {
    e.preventDefault();
    DisplayProviderList('/Encounters/CreateEncounter/GetBillingProviderList', $('[name="BillingProvider"]').val(), 'BILLING PROVIDER');
});

$('#CFacilitySearchBtn').on('click', function (e) {
    e.preventDefault();
    DisplayProviderList('/Encounters/CreateEncounter/GetFacilityList', $('[name="Facility"]').val(), 'FACILITY');
});


$('#RenderingProviderSearchBtn').on('click', function (e) {
    e.preventDefault();
    DisplayProviderList('/Encounters/CreateEncounter/GetRenderingProviderList', $('[name="RenderingProvider"]').val(), 'RENDERING PROVIDER');
});



function openClaimHistoryModal() {
    TabManager.openCenterModal('/Encounters/CreateEncounter/GetClaimsHistory', 'Claim History', '', '')
}

$('#CClaimHistoryBtn').on('click', function () {
    openClaimHistoryModal();
});