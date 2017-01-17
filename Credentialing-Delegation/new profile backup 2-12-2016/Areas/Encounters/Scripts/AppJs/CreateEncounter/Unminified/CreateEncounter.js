var progressBarDataForProvider = [
{ StepNo: '1', StepText: 'Create Encounter by', RenderPageId: 'create_claim_by', ajaxType: 'GET', url: null, parameters: [], IsLoaded: false },
{ StepNo: '2', StepText: 'Select Provider', RenderPageId: 'select_provider', ajaxType: 'GET', url: '/Encounters/CreateEncounter/GetProviderSelection', parameters: [], IsLoaded: false },
{ StepNo: '3', StepText: 'Select Member', RenderPageId: 'select_member', ajaxType: 'GET', url: '/Encounters/CreateEncounter/GetSelectedProviderResult', parameters: [{ key: 'ProviderId', value: '' }], IsLoaded: false },
{ StepNo: '4', StepText: 'Encounter Details', RenderPageId: 'create_claim_template', ajaxType: 'GET', url: '/Encounters/CreateEncounter/GetEncounterDetails', parameters: [{ key: 'MemberId', value: '' }], IsLoaded: false },
{ StepNo: '5', StepText: 'Coding Details', RenderPageId: 'claim_info', ajaxType: 'POST', url: '/Encounters/CreateEncounter/GetCodingDetails', parameters: [], IsLoaded: false },
{ StepNo: '6', StepText: 'Auditing Details', RenderPageId: 'preview', ajaxType: 'POST', url: '/Encounters/CreateEncounter/GetAuditingDetails', parameters: [], postData: {}, IsLoaded: false }
];

var progressBarDataForMember = [
{ StepNo: '1', StepText: 'Create Claim by', RenderPageId: 'create_claim_by', ajaxType: 'GET', url: null, parameters: [], IsLoaded: false },
{ StepNo: '2', StepText: 'Select Member', RenderPageId: 'select_member', ajaxType: 'GET', url: '/Encounters/CreateEncounter/GetMember', parameters: [], IsLoaded: false },
{ StepNo: '3', StepText: 'Encounter Details', RenderPageId: 'create_claim_template', ajaxType: 'GET', url: '/Encounters/CreateEncounter/GetEncounterDetailsForMember', parameters: [{ key: 'MemberId', value: '' }], IsLoaded: false },
{ StepNo: '4', StepText: 'Coding Details', RenderPageId: 'claim_info', ajaxType: 'POST', url: '/Encounters/CreateEncounter/GetCodingDetails', parameters: [], postData: {}, IsLoaded: false },
{ StepNo: '5', StepText: 'Auditing Details', RenderPageId: 'preview', ajaxType: 'POST', url: '/Encounters/CreateEncounter/GetAuditingDetails', parameters: [], postData: {}, IsLoaded: false }
];

/** @description In wizard make move forward to the next step by hiding the current div
 * @param {$div,result} $div = div id where the result has to apply
 */
function appendDataToDiv($div, result) {
    $('.wizard_div').hide();
    $div.html(result);
    $div.show();
}


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
                if (progressBarData[i].parameters.length !== 0) {
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


function CreateProgressSteppedBar(progressBarData) {
    var wizard_template = '<ul class="wizard_steps anchor">';
    for (var i = 0; i < progressBarData.length; i++) {
        wizard_template = wizard_template + '<li>'
            + '<a href="' + progressBarData[i].RenderPageId + '"data-step="' + progressBarData[i].StepNo + '" class="disabled doneDisabled">'
                    + '<span class="step_no">' + progressBarData[i].StepNo + '</span>'
                    + '<span class="step_descr">'
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

function FindElement(step_no, data) {
    for (var i = 0; i < data.length; i++) {
        if (step_no == data[i].StepNo) {
            return data[i];
        }
    }
}



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
    if ($('input[name="CreateClaimsBy"]:checked').val() === "A single Claim" && StepNo === 2) {
        $('#memberSearchResultCCM').hide();
    }
}

var currentProgressBarData = null;
var createClaimByType = null;
var clickCount = 0;
var previousValue = "";

$('[type="radio"][name="CreateClaimsBy"]').on('change', function () {
    var currentChecked = this.value;
    clickCount++;
    if (clickCount >= 2) {
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
            FollowWizardStep();
        }).on('pnotify.cancel', function () {
            // check the revious checkbox

            $('[type="radio"][name="CreateClaimsBy"]').each(function () {
                if (this.value === previousValue) {
                    this.checked = true;
                }
            });
        });

    } else {
        previousValue = currentChecked;
        FollowWizardStep();
    }
});

function FollowWizardStep() {
    $('#wizard_step_id').show();
    createClaimByType = $('[type="radio"][name="CreateClaimsBy"]:checked').val();
    currentProgressBarData = progressBarDataForProvider;
    if (createClaimByType != 'Multiple claims for a Provider') {
        currentProgressBarData = progressBarDataForMember;
    }
    CreateProgressSteppedBar(currentProgressBarData);
    MakeItActive(2, currentProgressBarData);
}

function openDocumentViewer() {
    $('#DocumentViewer').show();
}