
//var memberData = TabManager.getMemberData();  //Get the Current Member Data
//console.log(memberData)

//---Check Member has Other Insurance?---//
$('.OtherInsuranceFieldset').hide();
$('.MemberInsuranceDetails').off('click', '.checkOtherInsurance').on('click', '.checkOtherInsurance', function (e) {
    if (($(this).attr('id')) === 'haveOtherInsurance') {
        $('#haveOtherInsurance').attr('checked', 'checked');
        $('.OtherInsuranceFieldset').show();
    }
    else {
        $('#pcp').attr('checked', 'checked');
        $('.OtherInsuranceFieldset').hide();
    }
})
//---End---//

//$("#EditMemberContainer").off('click', ".closeNewMember").on('click', ".closeNewMember", function () {
//    TabManager.closeCurrentlyActiveTab()
//})