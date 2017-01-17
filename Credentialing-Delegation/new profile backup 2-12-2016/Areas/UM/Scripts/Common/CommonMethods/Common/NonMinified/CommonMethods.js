function setMemberHeaderData(userData) {
    if (typeof userData != 'undefined') {
        $("#MemberID").text(userData.Member.MemberMemberships[0].Membership.SubscriberID);
        $("#MemberName").text(userData.Member.PersonalInformation.FirstName + " " + userData.Member.PersonalInformation.LastName);
        (userData.Member.PersonalInformation.Gender.toLowerCase() == 'female') ? $("#MemberGender").text("F") : $("#MemberGender").text("M")
        $("#MemberDob").text(userData.Member.PersonalInformation.DOB.formatDate());
        if (userData.Member.PersonalInformation.DOB) $("#MemberAge").text(userData.Member.PersonalInformation.DOB.getAge());
        $("#MemberSubGrp").text("-");
        $("#MemberEffDate").text(userData.Member.MemberMemberships[0].Membership.MemberEffectiveDate.formatDate());
        $("#MemberEligibility").text("12/31/2018");
        $("#MemberPCP").text(userData.Member.MemberMemberships[0].Membership.MembershipProviderInformation[0].Provider.PersonalInformation.FirstName + " " + userData.Member.MemberMemberships[0].Membership.MembershipProviderInformation[0].Provider.PersonalInformation.LastName);
        $("#MemberPCPPhone").text(userData.Provider.PhoneNumber.formatTelephone());
    }
}