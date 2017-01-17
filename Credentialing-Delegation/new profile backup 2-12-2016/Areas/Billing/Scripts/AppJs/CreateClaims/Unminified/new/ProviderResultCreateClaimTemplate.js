

/** 
@description Show search string on member select list
 */
$('#providers_title').html('YOU SEARCHED FOR "<b>' + searchString + '</b>" | <b>10</b> ' + searchType + ' RESULTS FOUND');

/** 
@description selecting provider based on selecting row of provider list grid
 */
$('#ProvidersResult tbody tr').on('click', function () {
    $('#' + resultDivId).html($(this).find('.ProviderName').text() + '<br>' + $(this).find('.ProviderAddress').text());
    $('#ProvidersResult').remove();
    $('#providers_title').remove();
    $('#' + currentLabel).show();
    $('#' + currentInput).hide();
    $('#' + CurrentSearchBtn).hide();
    $('.CreateClaimsTemplateEditButtons').show();
});