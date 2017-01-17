//InitICheckFinal();

/** @description Popover.
 */
$(document).ready(function () {
    $('[data-toggle="popover"]').popover();
});

/** @description Navigate to 835 Provider List.
 */
function GoTo835ProviderListBtn() {
    $('#835ProviderList').show();
    $('#835EOBList').html("");
}
$('#GoTo835ProviderListBtn').on('click', GoTo835ProviderListBtn);