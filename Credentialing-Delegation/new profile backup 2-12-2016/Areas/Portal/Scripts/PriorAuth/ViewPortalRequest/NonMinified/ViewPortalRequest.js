var memberData = TabManager.getMemberData();
$('#QueueName').show().text('Portal');
var GetMemberInfoInView = function () {
    if (memberData) {
        $("#pav_MBRID").text(memberData.Member.MemberMemberships[0].Membership.SubscriberID);
        $("#pav_ReqDate").text((new Date()).toLocaleString());
        $("#pav_LastName").text(memberData.Member.PersonalInformation.LastName);
        $("#pav_FirstName").text(memberData.Member.PersonalInformation.FirstName);
        $("#pav_PhoneNumber").text(memberData.Member.ContactInformation[0].PhoneInformation[0].ContactNumber.formatTelephone());
        $("#pav_DOB").text((new Date(memberData.Member.PersonalInformation.DOB)).toLocaleDateString());
    }
}

var GetrequestingProviderDate = function () {
    if (memberData) {
        $("#pav_reqProvider_firstname").text(memberData.Provider.ContactName.split(' ')[0]);
        $("#pav_reqProvider_lastname").text(memberData.Provider.ContactName.split(' ')[1]);
        $("#pav_reqProvider_TaxID").text(memberData.Provider.GroupTaxId1);
        $("#pav_reqProvider_contactName").text(memberData.Provider.ContactName);
        $("#pav_reqProvider_phonenumber").text(memberData.Provider.PhoneNumber.formatTelephone());
        if (memberData.Provider.FaxNumber) $("#pav_reqProvider_faxnumber").text(memberData.Provider.FaxNumber); else $("#pav_reqProvider_faxnumber").text('-');
    }
}

GetMemberInfoInView();
GetrequestingProviderDate();