
//InitICheckFinal();


/** @description Navigate to 835 List.
 */
function GoTo835ListBtn() {
    $('#File835ListDiv').show();
    $('#835ProviderList').html("");
}
$('#GoTo835ListBtn').on('click', GoTo835ListBtn);

/** @description Navigate to Generated 835 List.
 */
function GoToGeneratedListBtn() {
    showLoaderSymbol('fullBodyContainer');
    setTimeout(function () {
        removeLoaderSymbol();
    }, 300);
    var NPI = [];
    $('.ProviderCheckbox').each(function () {
        console.log(this.value);
        NPI.push("'" + this.value + "'");
    })

    //var InterchangeKey = $(this).closest('tr').attr('data-container');

    $.ajax({
        type: 'GET',
        url: '/Billing/FileManagement/Generate835?CheckNumber=' + ProviderCheckNumber + '&InterchangeKey=' + InterchangeKey + '&NPI=' + NPI,
        success: function (data) {
            $('#835EOBList').html(data);
        }
    });

    $('#File835ListDiv').show();
    $('#835ProviderList').html("");
}
$('#GoToGeneratedListBtn').on('click', GoToGeneratedListBtn);

/** @description View 835EOB List.
 */
$('.viewEOBBtn').click(function () {
    $('#835ProviderList').hide();
    var Interkey = "2";
    var HeaderKey = "3";
    var NPI = "1073560124";
    $.ajax({
        type: 'GET',
        url: '/Billing/FileManagement/Get835EobList?InterKey=' + Interkey + '&HeaderKey=' + HeaderKey + '&NPI=' + NPI,
        success: function (data) {
            $('#835EOBList').html(data);
        }
    });
})

