$('.updateButton').click(function () {
    TabManager.closeCurrentlyActiveTab();
    TabManager.navigateToTab({
        tabAction: 'Encounter List',
        tabTitle: 'Encounters',
        tabPath: '/Encounters/EncounterList/ShowAllEncounters',
        tabContainer: 'fullBodyContainer'
    });
});

$('.cancelButton').click(function () {
    TabManager.closeCurrentlyActiveTab();
    TabManager.navigateToTab({
        tabAction: 'Encounter List',
        tabTitle: 'Encounters',
        tabPath: '/Encounters/EncounterList/ShowAllEncounters',
        tabContainer: 'fullBodyContainer'
    });
});

/******Select Rendering Provider******/
$('.searchrenderingprovtable').css('display', 'none');
$('.RenProvSearch').css('display', 'none');
function searchrenderingprovider() {
    $('.searchrenderingprovtable').slideToggle();
}

var renprovselected = function (name) {
    $('.RenProvSearch').css('display', 'none');
    $('#RenderingProvName').text(name);
    $('.RenderingProviderNameField').css('display', 'block');
    $('.renderprovelement').val(name);
    searchrenderingprovider();
}

$('.renprov_edit_link').on('click', function () {
    $('.RenderingProviderNameField').css('display', 'none');
    $('.RenProvSearch').css('display', 'block');
    searchrenderingprovider();
});

/******Select Referring Provider******/
$('.searchreferringprovtable').css('display', 'none');
$('.RefProvSearch').css('display', 'none');
function searchreferringprovider() {
    $('.searchreferringprovtable').slideToggle();
}

var refprovselected = function (name) {
    $('.RefProvSearch').css('display', 'none');
    $('#ReferringProvName').text(name);
    $('.ReferringProviderNameField').css('display', 'block');
    searchreferringprovider();
    $('.refprovelement').val(name);
}

$('.refprov_edit_link').on('click', function () {
    $('.ReferringProviderNameField').css('display', 'none');
    $('.RefProvSearch').css('display', 'block');
    searchreferringprovider();
});

/******Select Billing Provider******/
$('.searchbillingprovtable').css('display', 'none');
$('.BillProvSearch').css('display', 'none');
function searchbillingprovider() {
    $('.searchbillingprovtable').slideToggle();
}

var billprovselected = function (name) {
    $('.BillProvSearch').css('display', 'none');
    $('#BillingProvName').text(name);
    $('.BillingProviderNameField').css('display', 'block');
    searchbillingprovider();
    $('.billprovelement').val(name);
}

$('.billprov_edit_link').on('click', function () {
    $('.BillingProviderNameField').css('display', 'none');
    $('.BillProvSearch').css('display', 'block');
    searchbillingprovider();
});

$('.searchservicefacilitytable').css('display', 'none');
$('.servicefacilitySearch').css('display', 'none');
function searchservicefacility() {
    $('.searchservicefacilitytable').slideToggle();
}

var servfacilityselected = function (name) {
    $('.servicefacilitySearch').css('display', 'none');
    $('#ServiceFacName').text(name);
    $('.ServieFacilityNameField').css('display', 'block');
    searchservicefacility();
    $('.servicefacilityelement').val(name);
}

$('.servicefacility_edit_link').on('click', function () {
    $('.ServieFacilityNameField').css('display', 'none');
    $('.servicefacilitySearch').css('display', 'block');
    searchservicefacility();
});
//////////////////////////////////////////////////////////////////////////////////////



