var MemberProfileData = TabManager.getMemberData();
$("#MemberProfile").ready(function () {
    //setMemberDetailsData(MemberProfileData);
  //  setMemberHeaderData(MemberProfileData);
    $('#QueueName').hide();
});

$("#MemberProfile").on('click', '.list-item', function (e) {
    e.preventDefault();
    var MethodName = '';
    var id = $(this).parent().attr('id');
    switch (id) {
        case "Provider":
            MethodName = "setProviderTabData";
            break;
        case "Notes":
            MethodName = "NotesMethod";
            break;
        case "Contacts":
            MethodName = "ContactMethod";
            break;
        case "Attachments":
            Methodname = "AttachmentMethod";
            break;
        case "Other_Demo":
            MethodName = "setOtherDemoTabData";
            break;
        case "Enrollment":
            MethodName = "EnrollmentMethod";
            break;
        case "Address":
            MethodName = "AddressMethod";
            break;
        case "Eligibility":
            MethodName = "EligibilityMethod";
            break;
        case "Medicare":
            MethodName = "MedicareMethod";
            break;
        case "COB":
            MethodName = "COBMethod";
            break;
        case "Rating":
            MethodName = "RatingMethod";
            break;
        case "Immune":
            MethodName = "ImmuneMethod";
            break;
        case "Responsible":
            MethodName = "ResponsibleMethod";
            break;
        case "ICE":
            MethodName = "ICEMethod";
            break;
        case "MemberInformation":
            MethodName = "MemberMethod";
            break;
        default:
            break;
    }
    //console.log(MethodName);
    $(this).parent().addClass("current");
    $(this).parent().siblings().removeClass("current");
    var url = $(this).data("partial");
    //TabManager.getDynamicContent(url, "MemberProfileTabView", 'SetOtherDemo', Member);
    TabManager.getDynamicContent(url, "MemberProfileTabView", MethodName, MemberProfileData);

});
setTimeout(function () {
    $('#MemberProfile .list-item').first().click();
}, 1000);


function setMemberDetailsData(Member) {
    //if (Member) {
    //$('#memberDetailsAddress').text(Member.Member.ContactInformation[0].AddressInformation[0].AddressLine1);
    //$('#memberDetailsCity').text(Member.Member.ContactInformation[0].AddressInformation[0].City);
    //$('#memberDetailsState').text(Member.Member.ContactInformation[0].AddressInformation[0].State);
    //$("#memberDetailsNumber").text(Member.Member.ContactInformation[0].PhoneInformation[0].ContactNumber.formatTelephone());
    //$("#memberDetailsCell").text(Member.Member.ContactInformation[0].PhoneInformation[0].ContactNumber.formatTelephone());
    //$("#memberDetailsEmail").text(Member.Member.PersonalInformation.FirstName +'@gmail.com');
    //$("#memberDetailsZip").text(Member.Member.ContactInformation[0].AddressInformation[0].ZipCode);
    //$("#memberDetailsPlan").text(Member.Member.MemberMemberships[0].Membership.Plan.PlanName);
    //$("#memberDetailsSubGroup").text("H2962 PREMIER 002");
    //$("#EffDate").text(Member.Member.MemberMemberships[0].Membership.MemberEffectiveDate.formatDate());
    //$("#Status").text("active");
    //}
};



function setProviderTabData(Member) {
    var PROVIDERINFO = Member.Member.MemberMemberships[0].Membership.MembershipProviderInformation[0];
    var PRACTICELOCATIONADDRESS = Member.Provider.ProviderPracticeLocationAddress;

    var providername = "Dr. " +
                        (PROVIDERINFO.Provider.PersonalInformation.FirstName ? PROVIDERINFO.Provider.PersonalInformation.FirstName + ' ' : '') +
                        (PROVIDERINFO.Provider.PersonalInformation.LastName ? PROVIDERINFO.Provider.PersonalInformation.LastName + ' ' : '');

    var npi = PROVIDERINFO.Provider.NPI;

    var building = Member.Provider.ProviderPracticeLocationAddress.Building;
    var street = PRACTICELOCATIONADDRESS.Street;
    var city = PRACTICELOCATIONADDRESS.City;
    var county = PRACTICELOCATIONADDRESS.County;
    var state = PRACTICELOCATIONADDRESS.State;
    var zipcode = PRACTICELOCATIONADDRESS.ZipCode;
    var address = (building ? building + ' ' : '') +
                  (street ? street + ' ' : '') +
                  (city ? city + ' ' : '') +
                  (county ? county + ' ' : '') +
                  (state ? state + ' ' : '') +
                  (zipcode ? zipcode + ' ' : '');

    if (Member.Provider.CenterNo) var centerno = Member.Provider.CenterNo;

    if (PROVIDERINFO.Provider.PersonalInformation) var pcp = "Yes"
    else var pcp = "No";

    var efffrom = PROVIDERINFO.PCPEffectiveDate.formatDate();

    var effto = PROVIDERINFO.PCPTermDate.formatDate();

    if (PRACTICELOCATIONADDRESS.MobileNumber) var phone = PRACTICELOCATIONADDRESS.MobileNumber.formatTelephone();

    if (providername) $('#ProviderInformationPhysicianName').text(providername);
    if (npi) $('#ProviderInformationNPI').text(npi);
    if (address) $('#ProviderInformationAddress').text(address);
    if (centerno) $('#ProviderInformationCenterNo').text(centerno);
    if (pcp) $('#ProviderInformationPCP').text(pcp);
    if (efffrom) $('#ProviderInformationEffFrom').text(efffrom);
    if (effto) $('#ProviderInformationEffTo').text(effto);
    if (phone) $('#ProviderInformationPhone').text(phone);
}

function setOtherDemoTabData(Member) {
   // if (Member.Member.CulturalInformation) {
   //     if (Member.Member.CulturalInformation.Ethnicity) $('#OtherDemoEthnicity').text(Member.Member.CulturalInformation.Ethnicity);
   //     if (Member.Member.CulturalInformation.Race) $('#OtherDemoRace').text(Member.Member.CulturalInformation.Race);
   //     if (Member.Member.CulturalInformation.Religion) $('#OtherDemoReligion').text(Member.Member.CulturalInformation.Religion);
   //}   
   // if(Member.Member.PersonalInformation.MaritalStatus)$('#OtherDemoMaritalStatus').text(Member.Member.PersonalInformation.MaritalStatus);
   // if(Member.Member.FamilyStatus)$('#OtherDemoFamilyStatus').text(Member.Member.FamilyStatus);
   // if(Member.Member.ContactInformation[0].PhoneInformation[0].ContactNumber)$('#OtherDemoHomePhone').text(Member.Member.ContactInformation[0].PhoneInformation[0].ContactNumber.formatTelephone());
   // //if (Member.Member.ContactInformation[0].EmailInformation) if (Member.Member.ContactInformation[0].EmailInformation[0].EmailID) $('#OtherDemoEmail').text(Member.Member.ContactInformation[0].EmailInformation[0].EmailID);
   // $('#OtherDemoEmail').text(Member.Member.PersonalInformation.FirstName +'@gmail.com');
   // // if (Member.Member.AlternatePhone) $('#OtherDemoAlternatePhone').text(Member.Member.AlternatePhone);
   // $('#OtherDemoAlternatePhone').text(Member.Member.ContactInformation[0].PhoneInformation[0].ContactNumber.formatTelephone());
   // if(Member.Member.ContactInformation[0].CommunicationPreference)$('#OtherDemoContactPref').text(Member.Member.ContactInformation[0].CommunicationPreference);
   // if(Member.Member.ContactPreference)if(Member.Member.ContactPreference.DoNotCall)$('#OtherDemoDoNotCall').text(Member.Member.ContactPreference.DoNotCall);
   // if(Member.Member.ResidentialStatus)$('#OtherDemoResidentialStatus').text(Member.Member.ResidentialStatus);
   // if(Member.Member.PrefCallDays)$('#OtherDemoPrefCallDays').text(Member.Member.PrefCallDays);
   // if(Member.Member.ContactAlert)$('#OtherDemoContactAlert').text(Member.Member.ContactAlert);
   // if(Member.Member.ParentalConsentThru)$('#OtherDemoParentalConsentThru').text(Member.Member.ParentalConsentThru);
   // if(Member.Member.ParentalConsentFrom)$('#OtherDemoParentalConsentFrom').text(Member.Member.ParentalConsentFrom);
   // if(Member.Member.PerferredCallTimeFrom)$('#OtherDemoPrefCallTimeFrom').text(Member.Member.PerferredCallTimeFrom);
   // if(Member.Member.PerferredCallTimeThru)$('#OtherDemoPrefCallTimeThru').text(Member.Member.PerferredCallTimeThru);
   // if(Member.Member.Spoken1)$('#OtherDemoSpoken1').text(Member.Member.Spoken1);
   // if(Member.Member.Spoken2)$('#OtherDemoSpoken2').text(Member.Member.Spoken2);
   // if(Member.Member.Written1)$('#OtherDemoWritten1').text(Member.Member.Written1);
   // if (Member.Member.Written2) $('#OtherDemoWritten2').text(Member.Member.Written2);
}

function EnrollmentMethod(Member) {
    //if (Member.Member.PersonalInformation.FirstName) $('#Enrollment_MemberName').text(Member.Member.PersonalInformation.FirstName + " " + Member.Member.PersonalInformation.LastName);
    //if (Member.Member.Relationship) $("#Enrollment_Relationship").text(Member.Member.Relationship);
    //if (Member.Member.ConfComm) $('#ConfComm').text(Member.Member.ConfComm);
    //if (Member.Member.isTermActive) { $('#Enrollment_Status').text("Active"); } else { $('#Enrollment_Status').text("Inactive"); }
    //if (Member.Member.PersonalInformation.Gender) $('#Enrollment_Gender').text(Member.Member.PersonalInformation.Gender);
    //if (Member.Member.PersonalInformation.DOB) $('#Enrollment_BirthDate').text(Member.Member.PersonalInformation.DOB.formatDate());
    //if (Member.Member.PersonalInformation.SSN) $("#Enrollment_SSN").text(Member.Member.PersonalInformation.SSN);
    //if (Member.Member.MemberMemberships[0].Membership.MemberEffectiveDate) $('#Enrollment_EffDate').text(Member.Member.MemberMemberships[0].Membership.MemberEffectiveDate.formatDate());
    //if (Member.Member.PersonalIdentificationInformations[0].IdentificationNumber) $('#Enrollment_MedicareNo').text(Member.Member.PersonalIdentificationInformations[0].IdentificationNumber);
    //if (Member.Member.FamilyLinkID) $('#Enrollment_FamilylinkId').text(Member.Member.FamilyLinkID);
    //if (Member.Member.HistoryLinkID) $('#Enrollment_HistoryLinkId').text(Member.Member.HistoryLinkID);
    //if (Member.Member.MemberMemberships[0].Membership.SubscriberID) $('#Enrollment_HealthId').text(Member.Member.MemberMemberships[0].Membership.SubscriberID);
    //if (Member.Member.ExclusionaryPeriodCreditDays) $('#Enrollment_CreditDays').text(Member.Member.ExclusionaryPeriodCreditDays);
    //if (Member.Member.HippaStartDate) $('#Enrollment_HipStartDate').text(Member.Member.HippaStartDate.formatDate());
    //if (Member.Member.HippaEndDate) $('#Enrollment_HipEndDate').text(Member.Member.HippaEndDate.formatDate());
    //if (Member.Member.PreExStartDate) $('#Enrollment_PreStartDate').text(Member.Member.PreExStartDate.formatDate());
    //if (Member.Member.PreExEndDate) $('#Enrollment_PreEndDate').text(Member.Member.PreExEndDate.formatDate());
    //if (Member.Member.PreCreditDate) $('#Enrollment_PreCreditDays').text(Member.Member.PreCreditDate.formatDate());
    //if (Member.Member.ApplicantType) $('#Enrollment_ApplicantType').text(Member.Member.ApplicantType);
    //if (Member.Member.Limit) $('#limits').text(Member.Member.Limit);
    //if (Member.Member.EligibilityDate) $('#Enrollment_EligibilityDate').text(Member.Member.EligibilityDate);
    //if (Member.Member.QualifyingEventDate) $('#Enrollment_QEventDate').text(Member.Member.QualifyingEventDate);
    //if (Member.Member.EOITerminationDate) $('#Enrollment_TerminalDate').text(Member.Member.EOITerminationDate);
    //if (Member.Member.NewSignatureDate) $('#Enrollment_SignatureDate').text(Member.Member.NewSignatureDate);
    //if (Member.Member.PriorBillingIndicator) $('#Enrollment_PBillingIndicator').text(Member.Member.PriorBillingIndicator);
    //if (Member.Member.PriorBillingEFFDate) $('#Enrollment_PBillingEffDate').text(Member.Member.PriorBillingEFFDate);
    //if (Member.Member.RecordNo) $('#Enrollment_RecordNo').text(Member.Member.RecordNo);
    //if (Member.Member.MemberLanguages)if (Member.Member.MemberLanguages[0].Language.Name) $('#Enrollment_language').text(Member.Member.MemberLanguages[0].Language.Name)
    //if (Member.Member.ContactInformation[0].AddressInformation[0].AddressLine1) {
    //    if (Member.Member.ContactInformation[0].AddressInformation[0].AddressLine2) $('#home').text(Member.Member.ContactInformation[0].AddressInformation[0].AddressLine1 + "," + Member.Member.ContactInformation[0].AddressInformation[0].AddressLine2 + "," + Member.Member.ContactInformation[0].AddressInformation[0].City + "," + Member.Member.ContactInformation[0].AddressInformation[0].State + "," + Member.Member.ContactInformation[0].AddressInformation[0].ZipCode);
    //    else $('#home').text(Member.Member.ContactInformation[0].AddressInformation[0].AddressLine1 + "," + Member.Member.ContactInformation[0].AddressInformation[0].City + "," + Member.Member.ContactInformation[0].AddressInformation[0].State + "," + Member.Member.ContactInformation[0].AddressInformation[0].ZipCode);
    //}
    ////if (Member.Member.Mailing) $('#Mailing').text(Member.Member.Mailing);
    //$('#Mailing').text(Member.PersonalInformation.FirstName +'@gmail.com');
    //if (Member.Member.Work) $('#work').text(Member.Member.Work);
    //if (Member.Member.ContactInformation[0].PhoneInformation.CellNumber) $('#cell_phone').text(Member.Member.ContactInformation[0].PhoneInformation.CellNumber);
    //if (Member.Member.Memo) $('#Memo').text(Member.Member.Memo);

}

function AddressMethod(Member)
{
    var choice=Member.Member.ContactInformation[0].AddressInformation[0].Choice
    if (choice == 1) { $("#type").text("Primary"); } else if (choice == 2) { $("#type").text("Secondary"); } else if (choice == 3) { $("#type").text("Tertiary"); } else { $("#type").text("-"); }
    if (Member.Member.ContactInformation[0].AddressInformation[0].AddressLine1) {
        if (Member.Member.ContactInformation[0].AddressInformation[0].AddressLine2) $("#address").text(Member.Member.ContactInformation[0].AddressInformation[0].AddressLine1 + " , " + Member.Member.ContactInformation[0].AddressInformation[0].AddressLine2);
        else $("#address").text(Member.Member.ContactInformation[0].AddressInformation[0].AddressLine1);
    }
    if (Member.Member.ContactInformation[0].AddressInformation[0].City) $("#city").text(Member.Member.ContactInformation[0].AddressInformation[0].City);
    if (Member.Member.ContactInformation[0].AddressInformation[0].State) $("#state").text(Member.Member.ContactInformation[0].AddressInformation[0].State);
    if (Member.Member.ContactInformation[0].AddressInformation[0].ZipCode) $("#zip").text(Member.Member.ContactInformation[0].AddressInformation[0].ZipCode);
    if (MemberProfileData.Member.ContactInformation[0].PhoneInformation[0].ContactNumber) $("#phone").text(MemberProfileData.Member.ContactInformation[0].PhoneInformation[0].ContactNumber.formatTelephone());
    if (Member.Member.ContactInformation[0].PhoneInformation[0].Fax) $("#Fax").text(Member.Member.ContactInformation[0].PhoneInformation[0].Fax);
}

function COBMethod(Member)
{
    //if(Member.Member.CoBs)
    //{
    //    if (Member.Member.CoBs.InsuranceType) $("#COB_InsuranceType").text(Member.Member.CoBs.InsuranceType);
    //    if (Member.Member.CoBs.Order) $("#COB_Order").text(Member.Member.CoBs.Order);
    //    if (Member.Member.CoBs.SupplementalDrugType) $("#COB_DragType").text(Member.Member.CoBs.SupplementalDrugType);
    //    if (Member.Member.CoBs.Carrier) $("#COB_Carrier").text(Member.Member.CoBs.Carrier);
    //    if (Member.Member.CoBs.Effective) $("#COB_Effective").text(Member.Member.CoBs.Effective);
    //} else {
    //    $("#COBTableBody").html('<tr><td colspan="12" class="theme_label_data" style="text-align:center !important">No Data Available</td></tr>');
    //}
}

function EligibilityMethod(Member)
{
    var Member=Member.Member.MemberMemberships[0].Membership;
    if (Member.MemberEffectiveDate) $("#Eligibility_EffDate").text(Member.MemberEffectiveDate.formatDate());
    if (Member.TermDate) $("#Eligibility_TermDate").text(Member.TermDate.formatDate());
    if (Member.Category) $("#Eligibility_Category").text(Member.Category)
     $("#Eligibility_PlanId").text(Member.Plan.PlanID);
    if (Member.Plan.PlanName) $("#Eligibility_PlanName").text(Member.Plan.PlanName);
    if (Member.Reason) $("#Eligibility_Reason").text(Member.Reason);
    if (Member.Subsidy) $("#Eligibility_Subsidy").text(Member.Subsidy);
    if (Member.Explanation) $("#Eligibility_Explanation").text(Member.Explanation);
    if (Member.Event) $("#Eligibility_Event").text(Member.Event);
    if (Member.VoidEvent) $("#Eligibility_VoidEvent").text(Member.VoidEvent);
}

function MedicareMethod(Member)
{
    if(Member.Member.Medicare)
    {
        if (Member.Member.Medicare.event) $("#Medicare_Event").text(Member.Member.Medicare.event);
        if (Member.Member.Medicare.Effective) $("#Medicare_Effective").text(Member.Member.Medicare.Effective);
        if (Member.Member.Medicare.HICN) $("#Medicare_HICN").text(Member.Member.Medicare.HICN);
        if (Member.Member.Medicare.State) $("#Medicare_State").text(Member.Member.Medicare.State);
        if (Member.Member.Medicare.County) $("#Medicare_County").text(Member.Member.Medicare.County);
        if (Member.Member.Medicare.PbpID) $("#Medicare_PBPID").text(Member.Member.Medicare.PbpID);
        if (Member.Member.Medicare.From) $("#Medicare_From").text(Member.Member.Medicare.From);
        if (Member.Member.Medicare.VerifiedDate) $("#Medicare_Event").text(Member.Member.Medicare.VerifiedDate);
        if (Member.Member.Medicare.event) $("#Medicare_Event").text(Member.Member.Medicare.event);
        if (Member.Member.Medicare.event) $("#Medicare_Event").text(Member.Member.Medicare.event);
        if (Member.Member.Medicare.event) $("#Medicare_Event").text(Member.Member.Medicare.event);
        if (Member.Member.Medicare.event) $("#Medicare_Event").text(Member.Member.Medicare.event);
    } else {
        $("#MedicareTableBody").html('<tr><td colspan="12" class="theme_label_data" style="text-align:center !important">No Data Available</td></tr>');
    }
}

function ICEMethod(Member)
{
    //if (Member.Member.Profile)
    //{
    //    if (Member.Member.Profile.ContactInformation.ContactPerson[0].GuarantorType) $("#Guarantor_Type").text(Member.Member.Profile.ContactInformation.ContactPerson[0].GuarantorType);
    //    if (Member.Member.Profile.ContactInformation.ContactPerson[0].ContactPersonID) $("#Member_id").text(Member.Member.Profile.ContactInformation.ContactPerson[0].ContactPersonID);
    //    if (Member.Member.Profile.ContactInformation.ContactPerson[0].FirstName) $("#first_name").text(Member.Member.Profile.ContactInformation.ContactPerson[0].FirstName);
    //    if (Member.Member.Profile.ContactInformation.ContactPerson[0].LastName) $("#last_name").text(Member.Member.Profile.ContactInformation.ContactPerson[0].LastName);
    //    if (Member.Member.Profile.ContactInformation.ContactPerson[0].Relationship) $("#Relationship").text(Member.Member.Profile.ContactInformation.ContactPerson[0].Relationship);
    //    if (Member.Member.ResponsiblePerson.Email) $("#Email").text(Member.Member.ResponsiblePerson.Email);
    //    if (Member.Member.ResponsiblePerson.PhoneNumber) $("#Phone").text(Member.Member.ResponsiblePerson.PhoneNumber.formatTelephone());
    //    if (Member.Member.ResponsiblePerson.CellNumber) $("#Cell").text(Member.Member.ResponsiblePerson.CellNumber);
    //    if (Member.Member.ResponsiblePerson.AddressInformation.Address1) $("#Address1").text(Member.Member.ResponsiblePerson.AddressInformation.Address1);
    //    if (Member.Member.ResponsiblePerson.AddressInformation.Address2) $("#Address2").text(Member.Member.ResponsiblePerson.AddressInformation.Address2);
    //    if (Member.Member.ResponsiblePerson.AddressInformation.City) $("#City").text(Member.Member.ResponsiblePerson.AddressInformation.City);
    //    if (Member.Member.ResponsiblePerson.AddressInformation.State) $("#State").text(Member.Member.ResponsiblePerson.AddressInformation.State);
    //    if (Member.Member.ResponsiblePerson.AddressInformation.Zip) $("#Zip").text(Member.Member.ResponsiblePerson.AddressInformation.Zip);
    //    if (Member.Member.ResponsiblePerson.AddressInformation.County) $("#County").text(Member.Member.ResponsiblePerson.AddressInformation.County);
    //}
    
}

function RatingMethod(Member)
{
    //var RatingOverrides = Member.Member.RatingOverrides;
    //if (Member.Member.RatingOverrides)
    //{
    //    if (RatingOverrides.EffectiveDate) $("#Rating_effDate").text(RatingOverrides.EffectiveDate.formatDate());
    //    if (RatingOverrides.PlanID) $("#Rating_PlanId").text(RatingOverrides.PlanID);
    //    if (RatingOverrides.IdentificationType) $("#Rating_type").text(RatingOverrides.IdentificationType);
    //    if (RatingOverrides.Units) $("#Rating_units").text(RatingOverrides.Units);
    //    if (RatingOverrides.Factors) $("#Rating_factors").text(RatingOverrides.Factors);
    //    if (RatingOverrides.Amount) $("#Rating_amount").text(RatingOverrides.Amount);
    //} else {
    //    $("#RatingOverrideTableBOdy").html('<tr><td colspan="12" class="theme_label_data" style="text-align:center !important">No Data Available</td></tr>');
    //}
}

function ImmuneMethod(Member)
{
    //var screening = Member.Member.HealthScreenings;
    //if (Member.Member.HealthScreenings) {
    //    if (screening.PFX) $("#PFX").text(screening.PFX);
    //    if (screening.Code) $("#CODE").text(screening.Code);
    //    if (screening.SFX) $("#SFX").text(screening.SFX);
    //    if (screening.Last) $("#LAST").text(screening.Last);
    //    if (screening.Next) $("#NEXT").text(screening.Next);
    //    if (screening.Comment) $("#COMMENTS").text(screening.Comment);
    //} else {
    //    $("#ImmuneHSTableBody").html('<tr><td colspan="12" class="theme_label_data" style="text-align:center !important">No Data Available</td></tr>');
    //}
}

function ResponsibleMethod(Member)
{
    //if (Member.Member.Profile)
    //{
    //    if (Member.Member.Profile.ContactInformation.ContactPerson[0].GuarantorType) $("#Responsible_GaurantorType").text(Member.Member.Profile.ContactInformation.ContactPerson[0].GuarantorType);
    //    if (Member.Member.Profile.ContactInformation.ContactPerson[0].ContactPersonID) $("#Responsible_MemberId").text(Member.Member.Profile.ContactInformation.ContactPerson[0].ContactPersonID);
    //    if (Member.Member.Profile.ContactInformation.ContactPerson[0].FirstName) $("#Responsible_FirstName").text(Member.Member.Profile.ContactInformation.ContactPerson[0].FirstName);
    //    if (Member.Member.Profile.ContactInformation.ContactPerson[0].LastName) $("#Responsible_LastName").text(Member.Member.Profile.ContactInformation.ContactPerson[0].LastName);
    //    if (Member.Member.ResponsiblePerson.Email) $("#Responsible_Email").text(Member.Member.ResponsiblePerson.Email);
    //    if (Member.Member.ResponsiblePerson.PhoneNumber) $("#Responsible_Phone").text(Member.Member.ResponsiblePerson.PhoneNumber.formatTelephone());
    //    if (Member.Member.ResponsiblePerson.Cell) $("#Responnsible_Cell").text(Member.Member.ResponsiblePerson.Cell);
    //    if (Member.Member.ResponsiblePerson.AddressInformation.Address1) $("#Responsible_Address1").text(Member.Member.ResponsiblePerson.AddressInformation.Address1);
    //    if (Member.Member.ResponsiblePerson.AddressInformation.Address2) $("#Reponsible_Address2").text(Member.Member.ResponsiblePerson.AddressInformation.Address2);
    //    if (Member.Member.ResponsiblePerson.AddressInformation.City) $("#Responsible_City").text(Member.Member.ResponsiblePerson.AddressInformation.City);
    //    if (Member.Member.ResponsiblePerson.AddressInformation.State) $("#Responsible_State").text(Member.Member.ResponsiblePerson.AddressInformation.State);
    //    if (Member.Member.ResponsiblePerson.AddressInformation.Zip) $("#Responsible_Zip").text(Member.Member.ResponsiblePerson.AddressInformation.Zip);
    //    if (Member.Member.ResponsiblePerson.AddressInformation.County) $("#Responsible_County").text(Member.Member.ResponsiblePerson.AddressInformation.County);
    //}
}

