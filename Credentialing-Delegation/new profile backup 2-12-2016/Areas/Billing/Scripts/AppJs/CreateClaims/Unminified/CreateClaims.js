var selectedMemberId = null;

/** 
@description Show member list based on search string provided
 */

function GetMemberResult() {
    TabManager.showLoadingSymbol("search_result");
    $.ajax({
        type: 'GET',
        url: '/Billing/CreateClaim/GetMemberResult',
        success: function (data) {
            $('#search_result').html(data);
        }
    });
}


/** 
@description Member search click event
 */
$('#member_search_btn').on('click', function () {
    GetMemberResult();
});

/** 
@description Show provider list based on search string provided
 */
function GetProviderResult() {
    TabManager.showLoadingSymbol("search_result");
    $.ajax({
        type: 'GET',
        url: '/Billing/CreateClaim/GetProviderResult',
        success: function (data) {
            $('#search_result').html(data);
        }
    });
}


/** 
@description Provider search click event
 */
$('#provider_search_btn').on('click', function () {
    GetProviderResult();
});



/** 
@description Object for wizards of create claims 
 */
var progressBarDataForProvider = [
{ StepNo: '1', StepText: 'Create Claim by', RenderPageId: 'create_claim_by', ajaxType: 'GET', url: null, parameters: [], IsLoaded: false },
{ StepNo: '2', StepText: 'Select Provider', RenderPageId: 'select_provider', ajaxType: 'GET', url: '/Billing/CreateClaim/GetProviderSelection', parameters: [], IsLoaded: false },
{ StepNo: '3', StepText: 'Select Member', RenderPageId: 'select_member', ajaxType: 'GET', url: '/Billing/CreateClaim/GetSelectedProviderResult', parameters: [{ key: 'ProviderId', value: '' }], IsLoaded: false },
{ StepNo: '4', StepText: 'Create Claim Template', RenderPageId: 'create_claim_template', ajaxType: 'GET', url: '/Billing/CreateClaim/GetCreateClaimTemplate', parameters: [{ key: 'MemberId', value: '' }], IsLoaded: false },
{ StepNo: '5', StepText: 'Claim Info', RenderPageId: 'claim_info', ajaxType: 'POST', url: '/Billing/CreateClaim/GetClaimInfo', parameters: [], postData: {}, IsLoaded: false },
{ StepNo: '6', StepText: 'Preview(CMS 1500)', RenderPageId: 'preview', ajaxType: 'POST', url: '/Billing/CreateClaim/GetCms1500Form', parameters: [], postData: null, IsLoaded: false }
];

var progressBarDataForMember = [
{ StepNo: '1', StepText: 'Create Claim by', RenderPageId: 'create_claim_by', ajaxType: 'GET', url: null, parameters: [], IsLoaded: false },
{ StepNo: '2', StepText: 'Select Member', RenderPageId: 'select_member', ajaxType: 'GET', url: '/Billing/CreateClaim/GetMember', parameters: [], IsLoaded: false },
{ StepNo: '3', StepText: 'Create Claim Template', RenderPageId: 'create_claim_template', ajaxType: 'GET', url: '/Billing/CreateClaim/GetCreateClaimTemplateForMember', parameters: [{ key: 'MemberId', value: '' }], IsLoaded: false },
{ StepNo: '4', StepText: 'Claim Info', RenderPageId: 'claim_info', ajaxType: 'POST', url: '/Billing/CreateClaim/GetClaimInfo', parameters: [], postData: {}, IsLoaded: false },
{ StepNo: '5', StepText: 'Preview(CMS 1500)', RenderPageId: 'preview', ajaxType: 'POST', url: '/Billing/CreateClaim/GetCms1500Form', parameters: [], postData: {}, IsLoaded: false }
];


/** 
@description Activating wizards steps and loading from backend 
* @param {string} StepNo you want to make active, {string} progressBarData current progress bar data selected based on radio buttons.
 */
function MakeItActive(StepNo, progressBarData) {
    for (var i = 0; i < progressBarData.length; i++) {
        var $anchore = $('a[href="' + progressBarData[i].RenderPageId + '"]');
        var $div = $('div#' + progressBarData[i].RenderPageId);
        if (StepNo == progressBarData[i].StepNo) {
            try {
                TabManager.showLoadingSymbol("fullBodyContainer");
            } catch (e) {
                console.log(e);
            }
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
                        ShowPage(result, $div);
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
                        ShowPage(result, $div);
                    }
                });
            }
            break;
        }
    }
}


function ShowPage(result, $div) {
    TabManager.loadOrReloadScriptsUsingHtml(result);
    $('.wizard_div').hide();
    TabManager.loadOrReloadScriptsUsingHtml(result);
    $div.html(result);
   
    $div.show();
}

/** 
@description Creating progress bar 
 */
function CreateProgressSteppedBar(progressBarData) {
    var wizard_template = '<ul class="wizard_steps anchor">';

    for (var i = 0; i < progressBarData.length; i++) {
        wizard_template = wizard_template + '<li>' +
                '<a href="' + progressBarData[i].RenderPageId + '"data-step="' + progressBarData[i].StepNo + '" class="disabled doneDisabled">' +
                    '<span class="step_no">' + progressBarData[i].StepNo + '</span>' +
        '<span class="step_descr">' + progressBarData[i].StepText +
        '</span>' +
                '</a>' +
            '</li>';
    }
    wizard_template = wizard_template + '</ul>';

    $('#wizard_step_id').html(wizard_template);

    $('#wizard_step_id').find('.wizard_steps li a').on('click', function (e) {
        e.preventDefault();
        var $current_element = $(this);
        if ($current_element.hasClass('done')) {
            var step_no = $current_element.attr('data-step');
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



/** 
@description Activating wizards steps which are already progressed
* @param {string} StepNo you want to make active, {string} progressBarData current progress bar data selected based on radio buttons.
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

/** 
@description Activating wizards steps without flushing data
* @param {string} StepNo you want to make active, {string} progressBarData current progress bar data selected based on radio buttons.
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
    if ($('input[name="CreateClaimsBy"]:checked').val() === "A single Claim" && StepNo === 2) {
        $('#memberSearchResultCCM').hide();
    }

}

var currentProgressBarData = null;

var createClaimByType = null;
var clickCount = 0;
var previousValue = "";


/** 
@description change event of radio button ,based on what wizard steps will change.
 */
$('[type="radio"][name="CreateClaimsBy"]').on('change', function () {
    FollowWizardStep()
    //var currentChecked = this.value;
    //clickCount++;
    //if (clickCount >= 2) {
        //(new PNotify({
        //    title: 'Confirmation Needed',
        //    text: 'Are you sure do you want to change?',
        //    hide: false,
        //    type: 'info',
        //    confirm: {
        //        confirm: true
        //    },
        //    buttons: {
        //        closer: false,
        //        sticker: false
        //    },
        //    history: {
        //        history: false
        //    },
        //    addclass: 'stack-modal',
        //    stack: {
        //        'dir1': 'down',
        //        'dir2': 'right',
        //        'modal': true
        //    }
        //})).get().on('pnotify.confirm', function () {
        //    previousValue = currentChecked;
        //    FollowWizardStep()
        //}).on('pnotify.cancel', function () {
        //    // check the revious checkbox

        //    $('[type="radio"][name="CreateClaimsBy"]').each(function () {
        //        if (this.value === previousValue) {
        //            this.checked = true;
        //        }
        //    })
        //});

    //} else {
    //    previousValue = currentChecked;
    //    FollowWizardStep()
    //}
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



//---------------------scroll functions-----------------------------


//log(eTop - $(window).scrollTop());

$('.fullBodyContainer').bind('scroll', chk_scroll);

var eTop = $('#top_div').offset().top;
var eleft;
var eWidth = $('#wizard_step_id').width();
var eHeight;
var scrollTop;

function chk_scroll() {
    eleft = $('#top_div').offset().left;
    scrollTop = $(this).scrollTop();
    eHeight = $('#wizard_step_id').height();
    var currentTop = $('#wizard_step_id').offset().top;
    if (eHeight <= scrollTop) {
        //--------------------code to change css of div---------------------------
        $('#wizard_step_id').addClass('fixed-wizard');
        $('#wizard_step_id').css({ top: eTop - 5 + 'px', left: eleft - 8 + 'px', width: eWidth + 2 + 'px' });
        $('.wizard_div').css({ 'margin-top': eHeight + 'px' });
    } else {
        $('#wizard_step_id').removeClass('fixed-wizard');
        $('#wizard_step_id').css({ top: 'auto', left: 'auto', width: 'auto' });
        $('.wizard_div').css({ 'margin-top': 'auto' });
    }
}



$('#CPTCodeDescDiv').click(function () {
    alert();
    console.log('csslled re');
});


/*
Loading
*/

//$(document).ajaxStart(function () {
//    TabManager.showLoadingSymbol("fullBodyContainer");
//});

$(document).ajaxComplete(function (event, request, settings) {
    TabManager.hideLoadingSymbol();
});