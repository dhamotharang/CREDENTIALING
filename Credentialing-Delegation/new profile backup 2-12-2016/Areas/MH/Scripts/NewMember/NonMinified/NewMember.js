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

/**
 * Overwrites obj1's values with obj2's and adds obj2's if non existent in obj1
 * @param obj1
 * @param obj2
 * @returns obj3 a new object based on obj1 and obj2
 */
function merge_options(obj1, obj2, obj3) {
    var obj4 = {};
    for (var attrname in obj1) { obj4[attrname] = obj1[attrname]; }
    for (var attrname in obj2) { obj4[attrname] = obj2[attrname]; }
    for (var attrname in obj3) { obj4[attrname] = obj3[attrname]; }
    return obj4;
}
function setInsurerDetails(data) {
    var $InsurerDetails = $('.InsurerDetails');
    var personalInfo = data.Insurance.MembershipInformation.Subscriber.PersonalInformation;
    var addressInfo = data.Insurance.MembershipInformation.Subscriber.AddressInformation[0];
    var contactInfo = data.Insurance.MembershipInformation.Subscriber.ContactInformation[0];
    var flatObject = merge_options(personalInfo, addressInfo, contactInfo);
    if ($InsurerDetails.length > 0) {
        for (var i = 0; i < $InsurerDetails.length; i++) {
            var prop = $InsurerDetails[i].name.split('.');
            var property = prop[prop.length - 1];
            if (flatObject[property]) {
                $InsurerDetails[i].value = flatObject[property];
            }
        }
    }
}
function resetInsurerDetails() {
    var $InsurerDetails = $('.InsurerDetails');
    if ($InsurerDetails.length > 0) {
        for (var i = 2; i < 15; i++) {
            if ($InsurerDetails[i].value) $InsurerDetails[i].value = null;
        }
    }
    //Additional Settings
    var s = $("#InsurerGender")[0];
    s[0].selected = true;
}
$("#copyMembertoInsurer").on("click", function () {

   // var formData1 = JSON.stringify(jQuery("#AddNewMemberForm").serializeArray()); // store json string
    //var formData2 = JSON.parse(JSON.stringify(jQuery("#AddNewMemberForm").serializeArray())) // store json object
     
    if ($(this).is(':checked')) {
        var formdata = $("#AddNewMemberForm").serialize();
                $.ajax({
                    method: "POST",
                    url: "/MH/AddMember/CopyMemberDetails",
                    data: formdata,
                    success: function (data) {
                       setInsurerDetails(data)
                }
                })
    } else {
        resetInsurerDetails();
    }
});

//---End---//

$(".closeNewMember").on('click', function () {
    TabManager.closeCurrentlyActiveTab()
})

var goToMemberProfile = function () {
    TabManager.closeCurrentlyActiveTab();
    setTimeout(function () {
        var tab = {
            "tabAction": "View Member",
            "tabTitle": "Added Member",
            //"tabUserid": memberId,
            "tabPath": "~/Areas/MH/Views/NewMember/View/_ViewNewMember.cshtml",
            "tabContainer": "fullBodyContainer",
            //"tabMemberdata": JSON.stringify(memberData)
        };
        TabManager.navigateToTab(tab);
        $("#mainTabsArea").find('#outerTabsArea').children().last().trigger('click');
    },1000)
}