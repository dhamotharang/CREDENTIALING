/** @description: progressBarDataForProvider and progressBarDataForMember variables are the wizard flow data
 */
var progressBarDataForProvider = [
{ StepNo: '1', StepText: 'Create Schedule by', RenderPageId: 'create_Schedule_by', ajaxType: 'GET', url: null, parameters: [], IsLoaded: false },
{ StepNo: '2', StepText: 'Select Providers', RenderPageId: 'select_provider', ajaxType: 'GET', url: '/Encounters/Schedule/GetProviderSelection', parameters: [], IsLoaded: false },
{ StepNo: '3', StepText: 'Select Member', RenderPageId: 'select_member', ajaxType: 'GET', url: '/Encounters/Schedule/GetSelectedProviderResult', parameters: [{ key: 'ProviderId', value: '' }], IsLoaded: false },
{ StepNo: '4', StepText: 'Schedule Details', RenderPageId: 'schedule_details', ajaxType: 'GET', url: '/Encounters/Schedule/GetScheduleDetails', parameters: [{ key: 'MemberId', value: '' }], IsLoaded: false },
{ StepNo: '5', StepText: 'Encounter Details', RenderPageId: 'encounter_details', ajaxType: 'GET', url: '/Encounters/Schedule/GetEncounterDetails', parameters: [], IsLoaded: false },
{ StepNo: '6', StepText: 'Coding Details', RenderPageId: 'Scheduler_Coding_details', ajaxType: 'GET', url: '/Encounters/CreateEncounter/GetCodingDetails', parameters: [], IsLoaded: false },
{ StepNo: '7', StepText: 'Auditing Details', RenderPageId: 'Scheduler_Auditing_details', ajaxType: 'GET', url: '/Encounters/CreateEncounter/GetAuditingDetails', parameters: [], postData: {}, IsLoaded: false }
];

var progressBarDataForMember = [
{ StepNo: '1', StepText: 'Create Schedule by', RenderPageId: 'create_Schedule_by', ajaxType: 'GET', url: null, parameters: [], IsLoaded: false },
{ StepNo: '2', StepText: 'Select Member', RenderPageId: 'select_member', ajaxType: 'GET', url: '/Encounters/Schedule/GetMemberForScheduler', parameters: [], IsLoaded: false },
{ StepNo: '3', StepText: 'Schedule Details', RenderPageId: 'schedule_details', ajaxType: 'GET', url: '/Encounters/Schedule/GetScheduleDetailsForMember', parameters: [{ key: 'MemberId', value: '' }], IsLoaded: false },
{ StepNo: '4', StepText: 'Encounter Details', RenderPageId: 'encounter_details', ajaxType: 'GET', url: '/Encounters/Schedule/GetEncounterDetailsForMember', parameters: [], IsLoaded: false },
{ StepNo: '5', StepText: 'Coding Details', RenderPageId: 'Scheduler_Coding_details', ajaxType: 'POST', url: '/Encounters/CreateEncounter/GetCodingDetails', parameters: [], IsLoaded: false },
{ StepNo: '6', StepText: 'Auditing Details', RenderPageId: 'Scheduler_Auditing_details', ajaxType: 'POST', url: '/Encounters/CreateEncounter/GetAuditingDetails', parameters: [], postData: {}, IsLoaded: false }

];

/** @description In wizard make move forward to the next step by hiding the current div
 * @param {$div,result} $div = div id where the result has to apply
 */
function appendDataToDiv($div, result) {
    $('.wizard_div').hide();
    $div.html(result);
    $div.show();
}

/** @description: this method makes particular process as active -> It will fetch the data based on url
 * @param: Step No to which it need to move and Progress Bar Data the variable name
 */
function MakeItActive(StepNo, progressBarData) {
    for (var i = 0; i < progressBarData.length; i++) {
        var $anchore = $('a[href="' + progressBarData[i].RenderPageId + '"]');
        var $div = $('div#' + progressBarData[i].RenderPageId);
        if (StepNo == progressBarData[i].StepNo) {
            $('a.selected').removeClass().addClass('done');
            $anchore.removeClass();
            $anchore.addClass('selected');
            var ajaxURL = progressBarData[i].url;
            if (progressBarData[i].ajaxType == 'GET') {
                if (progressBarData[i].parameters.length != 0) {
                    ajaxURL = ajaxURL + '?';
                    for (var j = 0; j < progressBarData[i].parameters.length; j++) {
                        var currentParameter = progressBarData[i].parameters[j];
                        ajaxURL = ajaxURL + currentParameter.key + '=' + currentParameter.value;
                        if (j < progressBarData[i].parameters.length - 1) {
                            ajaxURL = ajaxURL + '&';
                        }
                    }
                }
                $.ajax({
                    type: 'GET',
                    url: ajaxURL,
                    processData: false,
                    contentType: false,
                    success: function (result) {
                        appendDataToDiv($div, result);
                    }
                });
            } else {
                $.ajax({
                    type: 'POST',
                    url: ajaxURL,
                    data: progressBarData[i].postData,
                    processData: false,
                    contentType: false,
                    dataType: "html",
                    success: function (result) {
                        appendDataToDiv($div, result);
                    }
                });
            }
            break;
        }
    }
}

/** @description: this method creates the wizard based on the data provided in the format above
 * @param: Wizard Data provided in the format
 */
function CreateProgressSteppedBar(progressBarData) {
    var wizard_template = '<ul class="wizard_steps anchor">';
    for (var i = 0; i < progressBarData.length; i++) {
        wizard_template = wizard_template + '<li>'
                + '<a href="' + progressBarData[i].RenderPageId + '"data-step="' + progressBarData[i].StepNo + '" class="disabled doneDisabled">'
                    + '<span class="step_no">' + progressBarData[i].StepNo + '</span>'
                    + '<span class="step_descr"> '
                        + progressBarData[i].StepText
                    + '</span>'
                + '</a>'
            + '</li>';
    }
    wizard_template = wizard_template + '</ul>';
    $('#wizard_step_id').html(wizard_template);
    $('#wizard_step_id').find('.wizard_steps li a').on('click', function (e) {
        e.preventDefault();
        var $current_element = $(this);
        if ($current_element.hasClass('done')) {
            var $current_container = $('#' + $current_element.attr('href'));
            $('.wizard_container').find('.wizard_div').hide();
            $current_container.show();
            $current_element.removeClass();
            $('.wizard_steps li').find('.selected').removeClass().addClass('done');
            $current_element.addClass('selected');
        }
    });
}

/** @description: this method finds and returns data based on the step number and progress bar data
 * @param: step number and data to find
 * @return: data
 */
function FindElement(step_no, data) {
    for (var i = 0; i < data.length; i++) {
        if (step_no == data[i].StepNo) {
            return data[i];
        }
    }
}

/** @description: this method makes the step number provided visible if its data is already fetched
 * @param: step number and data
 * Note.: the step number provided should be fetched before calling. if not exist call MakeItActive()/MakeItVisibleWithDataFlush() method
 */
function MakeItVisible(StepNo, progressBarData) {
    $('.selected').removeClass().addClass('done');
    $('.wizard_div').hide();
    for (var i = 0; i < progressBarData.length; i++) {
        if (StepNo == progressBarData[i].StepNo) {
            var $anchore = $('a[href="' + progressBarData[i].RenderPageId + '"]');
            var $div = $('div#' + progressBarData[i].RenderPageId);
            $div.show();
            $anchore.removeClass();
            $anchore.addClass('selected');
        }
    }
}

/** @description: this method makes the step number provided visible based on data it flushes the data
 * @param: step number and data
 */
function MakeItVisibleWithDataFlush(StepNo, progressBarData) {
    for (var i = 0; i < progressBarData.length; i++) {
        var $anchore = $('a[href="' + progressBarData[i].RenderPageId + '"]');
        var $div = $('div#' + progressBarData[i].RenderPageId);
        if (StepNo == progressBarData[i].StepNo) {
            $div.show();
            $anchore.removeClass();
            $anchore.addClass('selected');
        } else if (StepNo < progressBarData[i].StepNo) {
            $div.html('');
            $anchore.removeClass();
            $anchore.addClass('disabled');
        }
    }
    if ($('input[name="CreateScheduleBy"]:checked').val() === "A single Claim" && StepNo === 2) {
        $('#memberSearchResultCCM').hide();
    }
}


var currentProgressBarData = null;
var createSchedulerByType = null;
var clickSchedulerCount = 0;
var previousValue = "";

/** @description: this event is for alert when change of radio button
 */
$('[type="radio"][name="CreateScheduleBy"]').on('change', function () {
    var currentChecked = this.value;
    clickSchedulerCount++;
    if (clickSchedulerCount >= 2) {
        (new PNotify({
            title: 'Confirmation Needed',
            text: 'Are you sure do you want to change?',
            hide: false,
            type: 'info',
            confirm: {
                confirm: true
            },
            buttons: {
                closer: false,
                sticker: false
            },
            history: {
                history: false
            },
            addclass: 'stack-modal',
            stack: {
                'dir1': 'down',
                'dir2': 'right',
                'modal': true
            }
        })).get().on('pnotify.confirm', function () {
            previousValue = currentChecked;
            FollowWizardStep()
        }).on('pnotify.cancel', function () {
            // check the revious checkbox
            $('[type="radio"][name="CreateScheduleBy"]').each(function () {
                if (this.value === previousValue) {
                    this.checked = true;
                }
            })
        });

    } else {
        previousValue = currentChecked;
        FollowWizardStep()
    }
});


/** @description: this method will make the progress bar reset and calls necessary methods
 */
$('.ScheduleEncounterContainer').off('click', '#Schedule_provider_search_btn').on('click', '#Schedule_provider_search_btn', function () {
    var data = {
        "key": $('.resultProviderSearch').text().trim().replace(/ /g, ''),
        "value": $('.provSearchField').val()
    }
    showLoaderSymbol("ScheduleProviderResult");
    $.ajax({
        type: 'GET',
        url: '/Encounters/Schedule/GetProviderResult?ProviderSearchParameter=' + JSON.stringify(data),
        processData: false,
        contentType: false,
        success: function (result) {
            $('#ScheduleProviderResult').html(result);
            $('#provider_result_title').html('You searched for "<b>' + $('[name="Provider_name"]').val() + '</b>"');
            removeLoaderSymbol();
        }
    });
});
$('.ScheduleEncounterContainer').off('click', '.providerSearchOptions').on('click', '.providerSearchOptions', function () {
    $(".resultProviderSearch").html($(this).text() + ' <span class="caret"></span>');
    $(".resultProviderSearch").val($(this).text());
    $('.provSearchField').attr('placeholder', $(this).text().toUpperCase());
});


/** @description: this method will make the progress bar reset and calls necessary methods
 */
function FollowWizardStep() {
    $('#wizard_step_id').show();
    createSchedulerByType = $('[type="radio"][name="CreateScheduleBy"]:checked').val();
    currentProgressBarData = progressBarDataForProvider;
    if (createSchedulerByType != 'Multiple claims for a Provider') {
        currentProgressBarData = progressBarDataForMember;
    }
    CreateProgressSteppedBar(currentProgressBarData);
    MakeItActive(2, currentProgressBarData);
}

/* @description this will navigate to schedules from encounters
*/
$('.ScheduleEncounterContainer').off('click', '#BackScheduleDetails').on('click', '#BackScheduleDetails', function () {
    MakeItVisible(3, currentProgressBarData);
});

/* @description this is edit button which will make it hide and show the search button
*/
$('.ScheduleEncounterContainer ').off('click', '.SchedulerServiceInfoEditBtn').on('click', '.SchedulerServiceInfoEditBtn', function (e) {
    $('.ProviderLabel').show();
    $(this).parent('.ProviderLabel').hide();
    $('.ProviderSearch').hide();
    var ContainerId = $(this).attr('data-url');
    $(ContainerId).show();
    $('#ProviderList').html('');
    $('#ProviderList').hide();
});

/* @description search button for referring provider in scheduler
*/
$('.ScheduleEncounterContainer ').off('click', '#Scheduler_ReferingProviderSearchBtn').on('click', '#Scheduler_ReferingProviderSearchBtn', function (e) {
    e.preventDefault();
    DisplayProviderList('/Encounters/CreateEncounter/GetReferingProviderList', $('[name="ReferringProvider"]').val(), 'ProviderName');
});

/* @description search button for Billing provider in scheduler
*/
$('.ScheduleEncounterContainer ').off('click', '#Scheduler_BillingProviderSearchBtn').on('click', '#Scheduler_BillingProviderSearchBtn', function (e) {
    e.preventDefault();
    DisplayProviderList('/Encounters/CreateEncounter/GetBillingProviderList', $('[name="BillingProvider"]').val(), 'ProviderName');
});

/* @description search button for Facility in scheduler
*/
$('.ScheduleEncounterContainer ').off('click', '#Scheduler_FacilitySearchBtn').on('click', '#Scheduler_FacilitySearchBtn', function (e) {
    e.preventDefault();
    DisplayProviderList('/Encounters/CreateEncounter/GetFacilityList', $('[name="Facility"]').val(), 'FacilityName');
});

/* @description search button for rendering provider in scheduler
*/
$('.ScheduleEncounterContainer ').off('click', '#Scheduler_RenderingProviderSearchBtn').on('click', '#Scheduler_RenderingProviderSearchBtn', function (e) {
    e.preventDefault();
    DisplayProviderList('/Encounters/CreateEncounter/GetRenderingProviderList', $('[name="RenderingProvider"]').val(), 'ProviderName');
});

/* @description this method will make call and get html of result of provider/facilty and display in id="ProviderList" div
*/
function DisplayProviderList(url, searchString, type) {
    var data = {
        "key": type.trim().replace(/ /g, ''),
        "value": searchString
    }
    $.ajax({
        type: 'GET',
        url: url + '?SearchParameter=' + JSON.stringify(data),
        processData: false,
        contentType: false,
        success: function (result) {
            $('#ProviderList').html(result);
            $('#ProviderList').show();
            $('#providers_title').html('YOU SEARCHED FOR "<b>' + searchString + '</b>" | <b>5</b> ' + type + ' RESULTS FOUND');
        }
    });

}

/* @description this event will move to encounters from create schedule
 */
$('.ScheduleEncounterContainer ').off('click', '.proceedToEncounter').on('click', '.proceedToEncounter', function () {
    if (createSchedulerByType == "Multiple claims for a Provider") {
        MakeItActive(5, currentProgressBarData);
    } else {
        MakeItActive(4, currentProgressBarData);
    }

});

/* @description this event will move to List when click on save & exit or exit in any wizard step
 */
$('.ScheduleEncounterContainer ').off('click', '.BackToList').on('click', '.BackToList', function () {
    TabManager.closeCurrentlyActiveTab();
    var tab = {
        "tabAction": "Encounter List",
        "tabTitle": "Encounter List",
        "tabPath": "/Encounters/EncounterList/ShowAllEncounters",
        "tabContainer": "fullBodyContainer"
    }
    TabManager.navigateToTab(tab);
})

/* @description this event is for creating a new schedule
 */
$('.ScheduleEncounterContainer ').off('click', '.Scheduler_CreateNew').on('click', '.Scheduler_CreateNew', function () {

    if (createSchedulerByType == "Multiple claims for a Member") {
        MakeItVisible(3, currentProgressBarData);
    } else {
        CreateProgressSteppedBar(currentProgressBarData);
        MakeItVisible(2, currentProgressBarData);
    }
});

$('.ScheduleEncounterContainer ').off('click', '.scheduleChangeProvider').on('click', '.scheduleChangeProvider', function () {
    MakeItVisible(2, currentProgressBarData);
})

$('.ScheduleEncounterContainer ').off('click', '.scheduleChangeMember').on('click', '.scheduleChangeMember', function () {
    MakeItVisible(3, currentProgressBarData);
})
/*=======================================================================================================================================================*/
/*===============================================================@ validation region @===================================================================*/
/*=======================================================================================================================================================*/
var ValidateScheduleDetails = function () {
    MakeItActive(currentProgressBarData.length, currentProgressBarData);
}
var validateForm = function () {
    var form = $('#saveScheduleDetailsForm');
    form.removeData("validator").removeData("unobtrusiveValidation");
    $.validator.unobtrusive.parse(form);
    if (form.valid()) {
        return true;
    }
    return false;

};


