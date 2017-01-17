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
    if (!$(this).prop("checked")) { //if this item is unchecked
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