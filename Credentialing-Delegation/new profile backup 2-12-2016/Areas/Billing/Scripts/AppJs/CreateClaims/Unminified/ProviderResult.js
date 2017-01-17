
$('#ProceedToNextFromProToMem').on('click', function () {
    var SelectedList = [];
    $('.individualCheckProvider').each(function () {
        if ($(this).is(':checked')) {
            SelectedList.push(this.value);
        }
    })
    currentProgressBarData[2].parameters[0].value = SelectedList;
    MakeItActive(3, currentProgressBarData);
})
//select all checkboxes
$(".selectAllProvider").change(function () {  //"select all" change 
    $(".normal-checkbox").prop('checked', $(this).prop("checked")); //change all ".checkbox" checked status
    $('#ProceedToNextFromProToMem').removeAttr("disabled");
});
//".checkbox" change 
$('.normal-checkbox').change(function () {
    //uncheck "select all", if one of the listed checkbox item is unchecked
    if (false == $(this).prop("checked")) { //if this item is unchecked
        $(".selectAllProvider").prop('checked', false); //change "select all" checked status to false
        if ($('.normal-checkbox:checked').length < 1) {
            $('#ProceedToNextFromProToMem').attr("disabled", true);
        }
    } else {
        if ($('.normal-checkbox:checked').length >= 1) {
            $('#ProceedToNextFromProToMem').removeAttr("disabled");
        }
    }
    //check "select all" if all checkbox items are checked       
    if ($('.normal-checkbox:checked').length == ($('.normal-checkbox').length) - 1) {
        $(".selectAllProvider").prop('checked', true);
    }
});

$('#ProviderResult').prtGrid({
    url: "/Billing/CreateClaim/GetProviderResultByIndex",
    dataLength: 50,
    columns: [{ type: 'text', name: 'ProviderNPI', text: 'Provider NPI', widthPercentage: 20, sortable: { isSort: true, defaultSort: 'ASC' } },
    { type: 'text', name: 'ProviderFullName', text: 'Provider Name', widthPercentage: 30, sortable: { isSort: true, defaultSort: null } },
    { type: 'text', name: 'Specialty', text: 'Specialty', widthPercentage: 20, sortable: { isSort: true, defaultSort: null } },
     { type: 'text', name: 'Taxonomy', text: 'Taxonomy Code', widthPercentage: 20, sortable: { isSort: true, defaultSort: null } },
    { type: 'none', name: '', text: '', widthPercentage: 10 }]
});


$('#ProviderResult').find('tbody').find('tr').on('click', function () {
    $(this).find('.individualCheckProvider').trigger('click');

});