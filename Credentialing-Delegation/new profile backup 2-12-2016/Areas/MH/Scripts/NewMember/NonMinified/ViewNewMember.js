//var memberData = TabManager.getMemberData();  //Get the Current Member Data
//var tabTitle = memberData.Member.PersonalInformation.FirstName + ' ' + memberData.Member.PersonalInformation.LastName;
//var memberId = memberData.Member.MemberMemberships[0].Membership.SubscriberID;
$("#ViewMemberContainer").off('click', ".editMemberDetails").on('click', ".editMemberDetails", function () {
    //TabManager.closeCurrentlyActiveTab();

    var e = $("#mainTabsArea").find('#outerTabsArea').children('.outertabs.active').children()[0];
    var memberName = e.innerText.replace("VIEW", "EDIT");


    var tab = {
        "tabAction": "Edit Member",
        "tabTitle":  memberName,
        //"tabUserid": memberId,
        "tabPath": "~/Areas/MH/Views/NewMember/Edit/_EditNewMember.cshtml",
        "tabContainer": "fullBodyContainer",
        //"tabMemberdata": JSON.stringify(memberData)
    };
    TabManager.navigateToTab(tab);
    //$("#outerTabsArea").children().last().siblings().removeClass('active');
    //$("#outerTabsArea").children().last().addClass('active');
    //$("#outerTabsArea").children().last().trigger("click");
}).off('click', '.closeViewMember').on('click', '.closeViewMember', function () {
    TabManager.closeCurrentlyActiveTab();
});