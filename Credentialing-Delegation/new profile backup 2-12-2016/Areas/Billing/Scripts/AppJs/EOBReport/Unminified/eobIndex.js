
$('.p_headername').html('Billing');

setTimeout(function () {
    $('input.flat').iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green'
    });

}, 100)
/** @description Changing Of Fields based on types selected in EOB Reports.
 */
$('input[type="radio"]').on('click', function (event) {
    var radioValue = event.target.value;
    if (radioValue == 'Rendering Provider') {
        $('.renderingContainer').show();
        $('.serviceLocationContainer').hide();
    } else if (radioValue == 'Service Location') {
        $('.serviceLocationContainer').show();
        $('.renderingContainer').hide();
    }
    if (radioValue == 'Date Of Service') {
        $('.DOSContainer').show();
        $('.PaymentDateContainer').hide();
    } else if (radioValue == 'Payment Date') {
        $('.PaymentDateContainer').show();
        $('.DOSContainer').hide();
    }
});
/** @description Getting result of Reports.
 */
$('#EOBSearch').click(function () {

    $('.form_Create_EOB').hide();
    $('.form_Preview_EOB').show();

    var TypeValue = "";
    var type = $('input[name="ReportType"]:checked').val();
    var DateType = $('input[name="DateType"]:checked').val();
    var DOSFrom = $('input[name="DOSFromEOB"]').val();
    var DOSTo = $('input[name="DOSToEOB"]').val();
    if (type == 'Rendering Provider') {
         TypeValue = $('input[name="RenderingProvider"]').val();
    } else if (type == 'Service Location') {
         TypeValue = $('input[name="ServiceLocation"]').val();
    }


    var spanStartTagMain = '<span class="padding_5px">';
    var spanTag = '<span class="text-initial">'
    var spanContentTag = '<span class="bolder uppercase">'
    var spanEndTag = '</span>'


    var content = spanStartTagMain + spanTag + type + ' - ' + spanEndTag + spanContentTag + TypeValue + spanEndTag + spanEndTag
    + spanStartTagMain + spanTag + DateType + ' - ' + spanEndTag + spanContentTag + DOSFrom + ' - ' + DOSTo + spanEndTag + spanEndTag
    + '<button class="btn btn-warning margin_left_12px btn-xs" onclick="editFormEOB()"><i class="fa fa-edit"></i> Edit</button>';

    $('.from_previewContent_EOB').html(content)


    /** @description Getting data based on selected types.
    */
    if (type == 'Rendering Provider' && DateType == 'Date Of Service') {
        getPartialForEOB('GetEobReportOfRenderingProviderByDos')
    } else if (type == 'Rendering Provider' && DateType == 'Payment Date') {
        getPartialForEOB('GetEobReportOfRenderingProviderByPaymentDate')
    } else if (type == 'Service Location' && DateType == 'Date Of Service') {
        getPartialForEOB('GetEobReportOfServiceLocationByDos')
    } else if (type == 'Service Location' && DateType == 'Payment Date') {
        getPartialForEOB('GetEobReportOfServiceLocationByPaymentDate')
    }


})

/** @description showing Type Selection widzard.
 */
function editFormEOB() {
    $('.form_Create_EOB').show();
    $('.form_Preview_EOB').hide();
}
/** @description Getting EOB Report that has the specified type parameter.
 * @param {String} view type of the Report. 
 */
function getPartialForEOB(view) {
    $.ajax({
        type: 'GET',
        url: '/Billing/EOBReport/' + view,
        success: function (data) {
            $('.eobTable').html(data);
        }
    });
}

/* for manually triggering the click for radio */
$('#renderingProvider').click();
$('#DateOfSeviceDateType').click();