//select all checkboxes
$(".SelectAllAcceptedClaims").change(function () {  //"select all" change 
    $(".normal-checkbox").prop('checked', $(this).prop("checked")); //change all ".checkbox" checked status
});
//".checkbox" change 
$('.normal-checkbox').change(function () {
    //uncheck "select all", if one of the listed checkbox item is unchecked
    if (false == $(this).prop("checked")) { //if this item is unchecked
        $(".SelectAllAcceptedClaims").prop('checked', false); //change "select all" checked status to false
        if ($('.normal-checkbox:checked').length < 1) {
      
        }
    } 
    //check "select all" if all checkbox items are checked       
    if ($('.normal-checkbox:checked').length == ($('.normal-checkbox').length) - 1) {
        $(".SelectAllAcceptedClaims").prop('checked', true);
    }
});


/** 
@description change event of radio button ,based on what wizard steps will change.
 */
$('#AccClaimListGenerateBTN').on('click', function () {
    //var currentChecked = this.value;
    //clickCount++;
    //if (clickCount >= 2) {
    (new PNotify({
        title: 'Confirmation Needed',
        text: "The following payers Doesn't have a clearing House please map the payers to the corresponding clearing house ultimate Note:If you want to continue generating files having clearing houses please click on continue else click on stop",
        hide: false,
        type: 'info',
        confirm: {
            confirm: true
        },
        buttons: {
            closer: false,
            sticker: false
        },
        history: {
            history: false
        },
        addclass: 'stack-modal',
        stack: {
            'dir1': 'down',
            'dir2': 'right',
            'modal': true
        }
    })).get().on('pnotify.confirm', function () {
        //previousValue = currentChecked;
        //FollowWizardStep()
    }).on('pnotify.cancel', function () {
        // check the revious checkbox

        //$('[type="radio"][name="CreateClaimsBy"]').each(function () {
        //    if (this.value === previousValue) {
        //        this.checked = true;
        //    }
        //})
    });

    //} else {
    //    previousValue = currentChecked;
    //    FollowWizardStep()
    //}
});