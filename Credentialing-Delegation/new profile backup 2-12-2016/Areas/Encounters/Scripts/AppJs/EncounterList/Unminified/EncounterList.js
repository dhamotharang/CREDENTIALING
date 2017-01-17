$(function () {
    initialize();
});

function initialize() {
    $('.nav.nav-tabs.navul li.active a').click();
}

function showDeleteModal() {
    showModal('deleteModal');
}

function showRescheduleModal() {
    showModal('rescheduleModal');
}

function showNoshowModal() {
    showModal('noshowModal');
}

function showCancelModal() {
    showModal('cancelModal');
}

function cancelrescheduleModal() {
    showModal('reschedule_cancelModal');
}


$('.p_headername').html('Encounter');
$('#Schedules').show();

var tabArray = [''];
$('.EncounterListContainter').off('click', '.tabs-menu a').on('click', '.tabs-menu a', function (event) {
    //$(".tabs-menu a").click(function (event) {
    event.preventDefault();
    $(this).parent().addClass("current");
    $(this).parent().siblings().removeClass("current");
    var tab = $(this).attr("href");
    $('.EncounterTabContentContainer').find(".custommembertab-content").hide()
    $('.EncounterTabContentContainer').find(tab).show().html();
    var clickedId = $(this).attr('href');
    var targetUrl = $(this).attr('data-target-url');
    var isPresent = false;
    for (var i = 0; i < tabArray.length; i++) {
        if (tabArray[i] == clickedId) {
            isPresent = true;
            break;
        }
    }
    if (!isPresent) {
        GetEncounterList(clickedId, targetUrl);

    }
});


GetEncounterList("#Schedules", "/Encounters/EncounterList/GetScheduleEncounterList");

function GetEncounterList(targetId, targetUrl) {
    showLoaderSymbol(targetId.substr(1));
    $.ajax({
        type: 'GET',
        url: targetUrl,
        success: function (data) {
            $(targetId).html(data);
            removeLoaderSymbol();
        }
    });
}

$('.encounterAction').on('change', function () {
    var value = $('input[name=Encounteraction]:checked', '.encounterAction').val();
    (value != "e_Deactivate") ? $('.cancelreschedule').css('display', 'block') : $('.cancelreschedule').css('display', 'none');

});