

/** 
@description geting payers list
 */
$("input:checkbox.switchCss").bootstrapSwitch();
$.ajax({
    type: 'GET',
    url: '/Billing/PayerDrilldownReport/GetPayerClaimsCountReport',
    success: function (data) {
        $('#payer_count_report').html(data);


    }
});

var breadcrumList = $('ul.inner_breadcrum li a');
var selectedPayerId = null;
var selectedBillingProviderId = null;
var currentPage = 'PayerReport';
var currentReportType = 'NoOfClaims';

var selectedPayerSearchString = null;
var selectedBillingProviderSearchString = null;
var selectedRenderingProviderSearchString = null;


/** 
@description Object for crating pages of payer to provider report
 */
var pageDetails = [{ pageName: 'PayerReport', pageId: 'payer_report', searchResult: [], CountReport: { subPageId: 'payer_count_report', url: '/Billing/PayerDrilldownReport/GetPayerClaimsCountReport', parameters: [], isLoaded: true }, AmountReport: { subPageId: 'payer_amount_report', url: '/PayerDrilldownReport/GetPayerClaimsAmountReport', parameters: [], isLoaded: false } },
{ pageName: 'BillingProviderReport', pageId: 'billingProvider_report', searchResult: [], CountReport: { subPageId: 'billingProvider_count_report', url: '/Billing/PayerDrilldownReport/GetBillingClaimsCountReport', parameters: [{}], isLoaded: false }, AmountReport: { subPageId: 'billingProvider_amount_report', url: '/PayerDrilldownReport/GetBillingClaimsAmountReport', parameters: [{}], isLoaded: false } },
{ pageName: 'RenderingProviderReport', pageId: 'renderingProvider_report', searchResult: [], CountReport: { subPageId: 'renderingProvider_count_report', url: '/Billing/PayerDrilldownReport/GetRenderingProviderCountReport', parameters: [{}], isLoaded: false }, AmountReport: { subPageId: 'renderingProvider_amount_report', url: '/PayerDrilldownReport/GetRenderingProviderAmountReport', parameters: [{}], isLoaded: false } },
{ pageName: 'MemberReport', pageId: 'member_report', searchResult: [], CountReport: { subPageId: 'member_count_report', url: '/Billing/PayerDrilldownReport/GetMemberClaimsCountReport', parameters: [{}], isLoaded: false }, AmountReport: { subPageId: 'member_amount_report', url: '/PayerDrilldownReport/GetMemberClaimsAmountReport', parameters: [{}], isLoaded: false } }];



/** 
@description navigating to page specified based on radio button value
*@param {string} radioValue value of radio button 
 */
function RenderPage(radioValue) {
    currentReportType = radioValue;
    var isActiveBreadcrum = false;
    for (var i = 0; i < pageDetails.length; i++) {
        var ajaxURL = "";
        var currentParameter = "";
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
                        for (var k = 0; k < currentObj.CountReport.parameters.length; k++) {
                             currentParameter = currentObj.CountReport.parameters[k];
                            ajaxURL = ajaxURL + currentParameter.key + '=' + currentParameter.value;
                            if (k < currentObj.CountReport.parameters.length - 1) {
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