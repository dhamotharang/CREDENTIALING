$('#CloseICDHistoryClaimInfo').click(function () {
    $('#ViewActiveHistoryICD').slideToggle();
})


/** 
@description Selecting ICD code from history
 */
$('.IcdCOde').click(function () {
    if ($(this).is(':checked')) {
        ICDHistoryList.push(this.value)
    } else {
         removeItem = this.value;
         ICDHistoryList = jQuery.grep(ICDHistoryList, function (value) {
            return value != removeItem;
        });
        for (var i = 1; i <= ICDHistoryList.length + 1; i++) {
            $('#claimsNatureOfIllness' + i).val("");
        }
    }
    if (ICDHistoryList.length <= 12) {

        for (var j = 1; j < ICDHistoryList.length + 1 ; j++) {
            $('#claimsNatureOfIllness' + j).val(ICDHistoryList[j - 1])
        }


    }
    event.stopPropagation();
}) 