/** 
@description Get rendering provider list 
 */
$("input:checkbox.switchCss").bootstrapSwitch();
$.ajax({
    type: 'GET',
    url: '/ProviderDrilldownReport/GetRenderingProviderCountReport',
    success: function (data) {
        $('#renderingProvider_count_report').html(data);
    }
});

var breadcrumList = $('ul.inner_breadcrum li a');
var selectedRenderingProviderId = null;
var selectedBillingProvider = null;
var currentPage = 'RenderingProviderReport';
var currentReportType = 'NoOfClaims';

var selectedPayerSearchString = null;
var selectedBillingProviderSearchString = null;
var selectedRenderingProviderSearchString = null;
var ajaxURL;
var currentParameter;
/** 
@description Object for crating pages of provider to payer report
 */

var pageDetails = [{ pageName: 'RenderingProviderReport', pageId: 'renderingProvider_report', searchResult: [], CountReport: { subPageId: 'renderingProvider_count_report', url: '/ProviderDrilldownReport/GetRenderingProviderCountReport', parameters: [{}], isLoaded: true }, AmountReport: { subPageId: 'renderingProvider_amount_report', url: '/ProviderDrilldownReport/GetRenderingProviderAmountReport', parameters: [{}], isLoaded: false } },
    { pageName: 'BillingProviderReport', pageId: 'billingProvider_report', searchResult: [], CountReport: { subPageId: 'billingProvider_count_report', url: '/ProviderDrilldownReport/GetBillingClaimsCountReport', parameters: [{}], isLoaded: false }, AmountReport: { subPageId: 'billingProvider_amount_report', url: '/ProviderDrilldownReport/GetBillingClaimsAmountReport', parameters: [{}], isLoaded: false } },
    { pageName: 'PayerReport', pageId: 'payer_report', searchResult: [], CountReport: { subPageId: 'payer_count_report', url: '/ProviderDrilldownReport/GetPayerClaimsCountReport', parameters: [], isLoaded: false }, AmountReport: { subPageId: 'payer_amount_report', url: '/ProviderDrilldownReport/GetPayerClaimsAmountReport', parameters: [], isLoaded: false } },
{ pageName: 'MemberReport', pageId: 'member_report', searchResult: [], CountReport: { subPageId: 'member_count_report', url: '/ProviderDrilldownReport/GetMemberClaimsCountReport', parameters: [{}], isLoaded: false }, AmountReport: { subPageId: 'member_amount_report', url: '/ProviderDrilldownReport/GetMemberClaimsAmountReport', parameters: [{}], isLoaded: false } }];

/** 
@description navigating to page specified based on radio button value
*@param {string} radioValue value of radio button 
 */
function RenderPage(radioValue) {
    currentReportType = radioValue;
    var isActiveBreadcrum = false;
    for (var i = 0; i < pageDetails.length; i++) {
        $(breadcrumList[i]).removeClass().addClass('active');

        var currentObj = pageDetails[i];
        if (currentPage == currentObj.pageName) {
            isActiveBreadcrum = true;
            $('#' + currentObj.pageId).show();
            var searchResultTemplate = '';
            for (var k = 0; k < currentObj.searchResult.length; k++) {
                searchResultTemplate = searchResultTemplate + '<label class="search_key">' + currentObj.searchResult[k].key + ': </label><label class="search_value">' + currentObj.searchResult[k].value + '</label>';
            }
            $('#search_result_div').html(searchResultTemplate);
            if (radioValue == 'ClaimedAmount') {
                $('#' + currentObj.CountReport.subPageId).hide();
                $('#' + currentObj.AmountReport.subPageId).show();
                if (!currentObj.AmountReport.isLoaded) {
                    ajaxURL = currentObj.AmountReport.url;
                    if (currentObj.AmountReport.parameters.length != 0) {
                        ajaxURL = ajaxURL + '?';
                        for (var j = 0; j < currentObj.AmountReport.parameters.length; j++) {
                            currentParameter = currentObj.AmountReport.parameters[j];
                            ajaxURL = ajaxURL + currentParameter.key + '=' + currentParameter.value;
                            if (j < currentObj.AmountReport.parameters.length - 1) {
                                ajaxURL = ajaxURL + '&';
                            }
                        }
                    }
                    $.ajax({
                        type: 'GET',
                        url: ajaxURL,
                        async: false,
                        success: function (data) {
                            currentObj.AmountReport.isLoaded = true;
                            $('#' + currentObj.AmountReport.subPageId).html(data);


                        }
                    });
                }
            } else {
                $('#' + currentObj.AmountReport.subPageId).hide();
                $('#' + currentObj.CountReport.subPageId).show();
                if (!currentObj.CountReport.isLoaded) {
                    ajaxURL = currentObj.CountReport.url;
                    if (currentObj.CountReport.parameters.length != 0) {
                        ajaxURL = ajaxURL + '?';
                        for (var j = 0; j < currentObj.CountReport.parameters.length; j++) {
                            currentParameter = currentObj.CountReport.parameters[j];
                            ajaxURL = ajaxURL + currentParameter.key + '=' + currentParameter.value;
                            if (j < currentObj.CountReport.parameters.length - 1) {
                                ajaxURL = ajaxURL + '&';
                            }
                        }
                    }
                    $.ajax({
                        type: 'GET',
                        url: ajaxURL,
                        async: false,
                        success: function (data) {
                            currentObj.CountReport.isLoaded = true;
                            $('#' + currentObj.CountReport.subPageId).html(data);

                        }
                    });
                }
            }


        } else {

            if (!isActiveBreadcrum) {
                $(breadcrumList[i]).removeClass().addClass('enable');
            } else {
                $(breadcrumList[i]).removeClass().addClass('disabled');
            }
            $('#' + currentObj.pageId).hide();

        }
    }
}

$('[type="radio"][name="ClaimReport"]').on('change', function (event) {
    var radioValue = event.target.value;

    RenderPage(radioValue);

});


$('.inner_breadcrum').on('click', 'a', function () {
    currentPage = $(this).attr('data-page');
    RenderPage(currentReportType);
});

$('#NoOfClaims').click();