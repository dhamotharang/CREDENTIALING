$(document).ready(function () {
    var providerData = TabManager.getProviderData();
    if (providerData) setProviderHeaderData(providerData);
    var fullName = providerData.FirstName +' '+ providerData.LastName;
    $('#ProviderFullNameForSearchModal').text(fullName);
});

var setProviderHeaderData = function (providerData) {
    if (providerData.NPINumber) $('#ProviderID').text(providerData.NPINumber);
    $('#ProviderCAHQ').text("124734810");
    $('#ProviderDOB').text("12/04/1980");
    $('#ProviderSpeciality').text("Audiologist");
    $('#ProviderLocation').text("PRIMECARE,1930 - PORT SAINT LOUIS, FLORIDA");
    $('#ProviderLanguage').text("English");
    if (providerData.Titles) if (providerData.Titles.length > 0) $('#ProviderType').text(providerData.Titles[0]);
};