/**
    @desctription This overrides the Name of the top wizard.
*/
$('.p_headername').html('Coding');




/**
    @desctription By default it makes the Open Coding List as Enable
*/
$('#Open').show();


/**
    @type {array}
*/
var tabArray = [''];


/** 
@description The Event triggered  by clicking on the tabs, which internally calls the controller method to fetch the data. This event also makes the tabs active and inactive.
*/
$('.CodingListContainter').off('click', '.tabs-menu a').on('click', '.tabs-menu a', function (event) {
    //$(".tabs-menu a").click(function (event) {
    event.preventDefault();
    $(this).parent().addClass("current");
    $(this).parent().siblings().removeClass("current");
    var tab = $(this).attr("href");
    $('.CodingTabContentContainer').find(".custommembertab-content").hide()
    $('.CodingTabContentContainer').find(tab).show().html();
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
        GetCodingList(clickedId, targetUrl);
    }
});



/** 
@description By default the method calls the controller to get the data for Open Coding List
* @param {string} #Open appends the fetched data to the container, {string} /Coding/Coding/GetOpenCodingList controller and method name which returns the data list.
*/
GetCodingList("#Open", "/Coding/Coding/GetOpenCodingList");


/** 
@description Fetches the data from Controller 
* @param {string} targetId appends the fetched data to the container, {string} targetUrl controller and method name which returns the data list.
*/
function GetCodingList(targetId, targetUrl) {
    $.ajax({
        type: 'GET',
        url: targetUrl,
        success: function (data) {
            $(targetId).html(data);
        }
    });
}

/** 
@description To Open the Confirmation modal for Deactivating the Encounter
* @param {object} CodingListViewModel.
*/
var showcodingDeleteModal = function (currentobject) {
    $('#DeactivateForm').load('/Coding/Coding/OpenDeactivateModal', currentobject);
    showModal('DeleteCodingListData');
}

/** 
@description Deactivate an Encounter
* @param {string} EncounterID.
*/
var DeactivateCurrentRecord = function (EncounterId) {
    $.ajax({
        type: 'POST',
        url: '/Coding/Coding/DeactivateCodedEncounter',
        data: { 'EncounterID': EncounterId },
    });
}

/** 
@description To Open the Confirmation modal for Deactivating the Encounter
* @param {object} CodingListViewModel.
*/
var ReactivateDeletedEncounter = function (EncounterID) {
    var ModalFooter = '<button type="button" class="btn btn-sm btn-primary pull-right" data-dismiss="modal" onclick="ReactivateCurrentRecord(' + EncounterID + ')">Yes</button>'
                    + '<button type="button" class="btn btn-sm btn-warning pull-left" data-dismiss="modal">No</button>';
    $('#ReactivateModalFooter').append(ModalFooter);
    showModal('ReactiveCodedEncounterModal');
}

/** 
@description Reactivate an Encounter
* @param {string} EncounterID.
*/
var ReactivateCurrentRecord = function (EncounterID) {
    $.ajax({
        type: 'POST',
        url: '/Coding/Coding/ReactivateCodedEncounter',
        data: { 'EncounterID': EncounterID },
    });
}