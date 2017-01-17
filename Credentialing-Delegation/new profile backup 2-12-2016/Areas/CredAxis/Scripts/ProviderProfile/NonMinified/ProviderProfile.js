/// <reference path="WebWorker.js" />
var providerData;

//providerData = TabManager.getProviderData();
//if (providerData) setProviderHeaderData(providerData);
    // Initializing pop over
    $('[data-toggle="tooltip"]').tooltip();
    $('[data-toggle="popover"]').popover();
    $("#AddEditSpecialtyBoard").hide();
    $("#ProviderProfileTabView").off("mouseover", "fieldset.fsStyle").on("mouseover", "fieldset.fsStyle", function () {
        $('[data-toggle="popover"]').popover();
    });
//$(this).off("click", ".btn-success").on("click", ".btn-success", function (event) {
//    console.log("clicked: " + event.target);
//    console.log($(this).parent().parent().parent().parent().text());
//});
    $('#ProviderProfileTabView .tabScrollProviderCA:not(:first-child)').empty();
    $('#LicenseTabView').empty();

    $('#ProviderProfileTabView').off('click', '.yahanClickKar').on('click', '.yahanClickKar', function () {
        setTimeout(function () {
            $('[data-toggle="popover"]').popover();
        }, 500)

});
    //==============================================================Practice Location===============================================================================//
    //==============================================================Practice Location (OFFICE HOURS)===============================================================================//
    $('#PracticeLocationTabView').off('click', '.Add_OfficeHours_Btn').on('click', '.Add_OfficeHours_Btn', function () {
        //$(function () { $('select[startid=dropDown708003]').select2({ placeholder: 'START DAY' }); });
        //$(function () { $('select[endid=dropDown708002]').select2({ placeholder: 'END DAY' }); });
        var $row = $(this).parents('.TimeSlotRow');
        var childCount = parseInt($row.parent().children().length);
        var rowIndex = parseInt($row.index());
        var HTMLTime1 = $("#TimeSlotToAppend").html();
        var StartDayId = 'OffcHrsStartDay_' + childCount;
        var EndDayId = 'OffcHrsEndDay_' + childCount;
        var PerfectHTMLTime1 = HTMLTime1.replace('startid=""', 'startid=' + '"' + StartDayId + '"').replace('endid=""', 'endid=' + '"' + EndDayId + '"');
        //$(function () { $('select[startid=' + StartDayId).select2({ placeholder: 'START DAY' }); });
        //$(function () { $('select[endid=' + EndDayId).select2({ placeholder: 'END DAY' }); });
        $(PerfectHTMLTime1).insertAfter($row);
        setAddDeleteICDButtonsVisibility();
    });

    var setAddDeleteICDButtonsVisibility = function () {
        $('.Add_OfficeHours_Btn').addClass("hidden");
        $('.Add_OfficeHours_Btn').last().removeClass("hidden");
        if ($('.Delete_OfficeHours_Btn')) if ($('.Delete_OfficeHours_Btn').length > 2) $('.Delete_OfficeHours_Btn').removeClass("hidden");
        else $('.Delete_OfficeHours_Btn').addClass("hidden");
    };
    $('#PracticeLocationTabView').on('click', '.Delete_OfficeHours_Btn', function () {
        $(this).parents('.TimeSlotRow').remove();
        setAddDeleteICDButtonsVisibility();
    });
    $('#PracticeLocationTabView').off('click', '.EndDayToggle').on('click', '.EndDayToggle', function () {
        var $row = $(this).parent().siblings('.endDayClass').children('.theme_label_data').children();
        if ($row.attr('disabled')) {
            $(this).removeClass("btn-default");
            $(this).addClass("btn-info");
            $row.removeAttr('disabled');
        }
        else {
            $(this).addClass("btn-default");
            $(this).removeClass("btn-info");
            $row.attr('disabled', 'disabled');
        };
    });
    //==============================================================Practice Location END===============================================================================//

    //===================== Get the Print ========================================

    $('#ProviderProfileTabView').off('click', '.print_button_for_personal').on('click', '.print_button_for_personal', function () {
        var theLoc = $('#ViewPersonalDetail').children('.x_content').children();
        printThisProviderData(theLoc, 'DEMOGRAPHICS - PERSONAL DETAIL');
    }).off('click', '.print_button_for_ContactInfo').on('click', '.print_button_for_ContactInfo', function () {
        var theLoc = $('#ViewContactInfo').children('.x_content').children();
        printThisProviderData(theLoc, 'DEMOGRAPHICS - CONTACT INFORMATION');
    }).off('click', '.print_button_for_HomeAddr').on('click', '.print_button_for_HomeAddr', function () {
        var theLoc = $('#ViewHomeAddress').children('.x_content').children();
        printThisProviderData(theLoc, 'DEMOGRAPHICS - HOME ADDRESS');
    }).off('click', '.print_button_for_BirthInfo').on('click', '.print_button_for_BirthInfo', function () {
        var theLoc = $('#ViewBirthInfo').children('.x_content').children();
        printThisProviderData(theLoc, 'DEMOGRAPHICS - BIRTH INFORMATION');
    }).off('click', '.print_button_for_PersonalIdent').on('click', '.print_button_for_PersonalIdent', function () {
        var theLoc = $('#ViewPersonalInfo').children('.x_content').children();
        printThisProviderData(theLoc, 'DEMOGRAPHICS - PERSONAL IDENTIFICATION');
    }).off('click', '.print_button_for_Languages').on('click', '.print_button_for_Languages', function () {
        var theLoc = $('#ViewLanguagesInfo').children('.x_content').children();
        printThisProviderData(theLoc, 'DEMOGRAPHICS - LANGUAGES KNOWN');
    }).off('click', '.print_button_for_CitizenInfo').on('click', '.print_button_for_CitizenInfo', function () {
        var theLoc = $('#ViewCitizenshipInfo').children('.x_content').children();
        printThisProviderData(theLoc, 'DEMOGRAPHICS - CITIZENSHIP INFO');
    });


    $('#ProviderProfileTabView').off('click', '.print_button_for_stateLicense').on('click', '.print_button_for_stateLicense', function () {
        var theLoc = $('#ViewStateLicense').children('.x_content').children();
        printThisProviderData(theLoc, 'LICENSES - STATE LICENSE');
    }).off('click', '.print_button_for_FederalDEA').on('click', '.print_button_for_FederalDEA', function () {
        var theLoc = $('#ViewFederalDEA').children('.x_content').children();
        printThisProviderData(theLoc, 'LICENSES - FEDERAL DEA');
    }).off('click', '.print_button_for_Medicare').on('click', '.print_button_for_Medicare', function () {
        var theLoc = $('#ViewMedicareInfo').children('.x_content').children();
        printThisProviderData(theLoc, 'LICENSES - MEDICARE INFO');
    }).off('click', '.print_button_for_MedicaidInfo').on('click', '.print_button_for_MedicaidInfo', function () {
        var theLoc = $('#ViewMedicaidInfo').children('.x_content').children();
        printThisProviderData(theLoc, 'LICENSES - MEDICAID INFO');
    }).off('click', '.print_button_for_OtherIden').on('click', '.print_button_for_OtherIden', function () {
        var theLoc = $('#ViewOtherIdentification').children('.x_content').children();
        printThisProviderData(theLoc, 'LICENSES - OTHER IDENTIFICATION');
    }).off('click', '.print_button_for_CDSInfo').on('click', '.print_button_for_CDSInfo', function () {
        var theLoc = $('#ViewCDSInfo').children('.x_content').children();
        printThisProviderData(theLoc, 'LICENSES - CDS INFORMATION');
    });


    $('#ProviderProfileTabView').off('click', '.print_button_for_Specailty').on('click', '.print_button_for_Specailty', function () {
        var theLoc = $('#ViewSpecialtyBoard').children('.x_content').children();
        printThisProviderData(theLoc, 'SPECIALTY / BOARD DETAILS');
    });


    $('#ProviderProfileTabView').off('click', '.print_button_for_UnderGrad').on('click', '.print_button_for_UnderGrad', function () {
        var theLoc = $('#ViewUnderGraduateDetails').children('.x_content').children();
        printThisProviderData(theLoc, 'UNDER GRADUATE / PROFESSIONAL SCHOOL DETAILS');

    }).off('click', '.print_button_for_PostGrad').on('click', '.print_button_for_PostGrad', function () {
        var theLoc = $('#ViewPostGraduateDetails').children('.x_content').children();
        printThisProviderData(theLoc, 'POST GRADUATE TRAINING / CME DETAILS');
    }).off('click', '.print_button_for_Grad').on('click', '.print_button_for_Grad', function () {
        var theLoc = $('#ViewGraduateSchoolDetails').children('.x_content').children();
        printThisProviderData(theLoc, 'GRADUATE / MEDICAL SCHOOL DETAILS');
    }).off('click', '.print_button_for_Residency').on('click', '.print_button_for_Residency', function () {
        var theLoc = $('#ViewResidency').children('.x_content').children();
        printThisProviderData(theLoc, 'RESIDENCY / INTERNSHIP / FELLOWSHIP DETAILS');
    }).off('click', '.print_button_for_ECFMG').on('click', '.print_button_for_ECFMG', function () {
        var theLoc = $('#ViewECFMGDetail').children('.x_content').children();
        printThisProviderData(theLoc, 'ECFMG DETAILS');
    });

    $('#ProviderProfileTabView').off('click', '.print_button_for_ProfWorkExp').on('click', '.print_button_for_ProfWorkExp', function () {
        var theLoc = $('#ViewProfWorkExp').children('.x_content').children();
        printThisProviderData(theLoc, 'PROFESSIONAL WORK EXPERIENCE');
    }).off('click', '.print_button_for_MilitarySrvc').on('click', '.print_button_for_MilitarySrvc', function () {
        var theLoc = $('#ViewMilitaryServiceInfo').children('.x_content').children();
        printThisProviderData(theLoc, 'MILITARY SERVICE');
    }).off('click', '.print_button_for_PublicHealth').on('click', '.print_button_for_PublicHealth', function () {
        var theLoc = $('#ViewPublicHeathService').children('.x_content').children();
        printThisProviderData(theLoc, 'PUBLIC HEALTH SERVICE');
    }).off('click', '.print_button_for_WorkGap').on('click', '.print_button_for_WorkGap', function () {
        var theLoc = $('#ViewWorkGap').children('.x_content').children();
        printThisProviderData(theLoc, 'WORK GAP');
    });

    $('#ProviderProfileTabView').off('click', '.print_button_for_HospitalPrivilege').on('click', '.print_button_for_HospitalPrivilege', function () {
        var theLoc = $('#ViewHospitalPrev').children('.x_content').children();
        printThisProviderData(theLoc, 'HOSPITAL PREVILEGE DETAILS');
    });

    $('#ProviderProfileTabView').off('click', '.print_button_for_Affiliation').on('click', '.print_button_for_Affiliation', function () {
        var theLoc = $('#ViewProfAffiliation').children('.x_content').children();
        printThisProviderData(theLoc, 'PROFESSIONAL AFFILIATION');
    });


    $('#ProviderProfileTabView').off('click', '.print_button_for_Liability').on('click', '.print_button_for_Liability', function () {
        var theLoc = $('#ViewProfessionalLiability').children('.x_content').children();
        printThisProviderData(theLoc, 'PROFESSIONAL LIABILITY');
    });


    $('#ProviderProfileTabView').off('click', '.print_button_for_EmployeeInfo').on('click', '.print_button_for_EmployeeInfo', function () {
        var theLoc = $('#ViewEmpInfo').children('.x_content').children();
        printThisProviderData(theLoc, 'EMPLOYMENT INFORMATION');
    }).off('click', '.print_button_for_GroupInfo').on('click', '.print_button_for_GroupInfo', function () {
        var theLoc = $('#viewGrpInfo').children('.x_content').children();
        printThisProviderData(theLoc, 'GROUP INFORMATION');
    });


    $('#ProviderProfileTabView').off('click', '.print_button_for_Reference').on('click', '.print_button_for_Reference', function () {
        var theLoc = $('#ViewProfRef').children('.x_content').children();
        printThisProviderData(theLoc, 'PROFESSIONAL REFERENCES');
    });


    $('#ProviderProfileTabView').off('click', '.print_button_for_Disclosure').on('click', '.print_button_for_Disclosure', function () {
        var theLoc = $('#dis').children('.x_content').children();
        printThisProviderData(theLoc, 'DISCLOSURE QUESTION');
    });
    //setTimeout(function () {
    //    getPersonalDetails();
    //}, 2500);
    $('#ProviderProfileTabView').off('click', '.HostPrev').on('click', '.HostPrev', function () {
        var valofRadioForHospitalPrevilege = $(this).val();
        if (valofRadioForHospitalPrevilege == "NO") {
            $("#HospitalPrevEditForm").hide();
        }
       else if (valofRadioForHospitalPrevilege == "YES") {
            $("#HospitalPrevEditForm").show();
        }

    setTimeout(function () {
        //webWorker();
        startWorker();
    }, 50);

    });
    $('#ProviderProfileTabView').off('click', '.HostPrevAdd').on('click', '.HostPrevAdd', function () {
        var valofRadioForHospitalPrevilegeAdd = $(this).val();
        if (valofRadioForHospitalPrevilegeAdd == "NO") {
            $("#HospitalPrevAddForm").removeClass('show').addClass('hidden');
        }
        else if (valofRadioForHospitalPrevilegeAdd == "YES") {
            $("#HospitalPrevAddForm").removeClass('hidden').addClass('show');
        }
    });
//var getPersonalDetails = function () {
//    var items = $("#ViewPersonalDetail .vlaueStyling");
//    var i = 0;
//    $.each(DemographicsPersonalData, function (index, values) {
//        for (i ; i < items.length; i++) {
//            $("#ViewPersonalDetail").find('fieldset').parent().children('.x_content').find('.vlaueStyling:eq(' + i + ')').append(values);
//            break;
//        }
//        i++;
//    });
//}

var clearDiv = function (e) {
    //$(e).parent().empty();
    $(e).first().parents('fieldset:first').parent().empty()
};
$("#Add_SB").click(function () {
    $("#AddEditSpecialtyBoard").show();
    $("#ViewSpecialtyBoard").hide();
});

var setProviderHeaderData = function (providerData) {
    if (providerData.NPINumber) $('#ProviderID').text(providerData.NPINumber);
    $('#ProviderCAHQ').text("124734810");
    $('#ProviderDOB').text("12/04/1980");
    $('#ProviderSpeciality').text("Audiologist");
    $('#ProviderLocation').text("PRIMECARE,1930 - PORT SAINT LOUIS, FLORIDA");
    $('#ProviderLanguage').text("English");
    if (providerData.Titles) if (providerData.Titles.length > 0) $('#ProviderType').text(providerData.Titles[0]);
};

$("#ProviderProfileTabView").ready(function () {
    $('#lessMedicare').trigger('click');
  //  ShowHideAddView('lessMedicare', 'moreMedicare');
});

var theTempId = '';
$("#ProviderProfile").off('click', '.list-item').on('click', '.list-item', function (e) {
    //e.preventDefault();
    var MethodName = '';

    var url = $(this).data().tabPath;
    var theID = $(this).data().tabContainer;
    var theDataVal = $(this).parent().data().val;

    var theCurrentElem = $(this).parent();
    
    theTempId = theCurrentElem;
    //TabManager.getDynamicContent(url, "ProviderProfileTabView", MethodName, providerData);
    var target = $(e.target);
    if (target.is("button")) {
        TabManager.getDynamicContent(url, theID, MethodName, '');
    }
    else if(target.is("input")){
        TabManager.getDynamicContent(url, theID, MethodName, '');
    }
    else {
        ScrollForInitialLoadProfile(theDataVal, theID);
    }

    GetTheTabName(theCurrentElem);

setTimeout(function () {
        //$('.memberProfileTabsContainer').find("[data-tab-container=" + theTempId + "]").parent().addClass("current");
        //$('.memberProfileTabsContainer').find("[data-tab-container=" + theTempId + "]").parent().siblings().removeClass("current");
        GetTheTabName(theTempId);
    }, 50);
});
//setTimeout(function () {
//    $('#ProviderProfile .list-item').first().click();
//}, 1000);

function setMemberDetailsData(Member) {
    if (Member) {
        $('#memberDetailsAddress').text(Member.Member.ContactInformation[0].AddressInformation[0].AddressLine1);
        $('#memberDetailsCity').text(Member.Member.ContactInformation[0].AddressInformation[0].City);
        $('#memberDetailsState').text(Member.Member.ContactInformation[0].AddressInformation[0].State);
        $("#memberDetailsNumber").text(Member.Member.ContactInformation[0].PhoneInformation[0].ContactNumber.formatTelephone());
        $("#memberDetailsZip").text(Member.Member.ContactInformation[0].AddressInformation[0].ZipCode);
        $("#memberDetailsPlan").text(Member.Member.MemberMemberships[0].Membership.Plan.PlanName);
        $("#EffDate").text(Member.Member.MemberMemberships[0].Membership.MemberEffectiveDate.formatDate());
        $("#Status").text("active");
    }
};

function setProviderTabData(Member) {
    try {
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
    } catch (e) {

    }
}

function setOtherDemoTabData(Member) {
    if (Member.Member.CulturalInformation) {
        if (Member.Member.CulturalInformation.Ethnicity) $('#OtherDemoEthnicity').text(Member.Member.CulturalInformation.Ethnicity);
        if (Member.Member.CulturalInformation.Race) $('#OtherDemoRace').text(Member.Member.CulturalInformation.Race);
        if (Member.Member.CulturalInformation.Religion) $('#OtherDemoReligion').text(Member.Member.CulturalInformation.Religion);
    }
    if (Member.Member.PersonalInformation.MaritalStatus) $('#OtherDemoMaritalStatus').text(Member.Member.PersonalInformation.MaritalStatus);
    if (Member.Member.FamilyStatus) $('#OtherDemoFamilyStatus').text(Member.Member.FamilyStatus);
    if (Member.Member.ContactInformation[0].PhoneInformation[0].ContactNumber) $('#OtherDemoHomePhone').text(Member.Member.ContactInformation[0].PhoneInformation[0].ContactNumber.formatTelephone());
    if (Member.Member.ContactInformation[0].EmailInformation) if (Member.Member.ContactInformation[0].EmailInformation[0].EmailID) $('#OtherDemoEmail').text(Member.Member.ContactInformation[0].EmailInformation[0].EmailID);
    if (Member.Member.AlternatePhone) $('#OtherDemoAlternatePhone').text(Member.Member.AlternatePhone);
    if (Member.Member.ContactInformation[0].CommunicationPreference) $('#OtherDemoContactPref').text(Member.Member.ContactInformation[0].CommunicationPreference);
    if (Member.Member.ContactPreference) if (Member.Member.ContactPreference.DoNotCall) $('#OtherDemoDoNotCall').text(Member.Member.ContactPreference.DoNotCall);
    if (Member.Member.ResidentialStatus) $('#OtherDemoResidentialStatus').text(Member.Member.ResidentialStatus);
    if (Member.Member.PrefCallDays) $('#OtherDemoPrefCallDays').text(Member.Member.PrefCallDays);
    if (Member.Member.ContactAlert) $('#OtherDemoContactAlert').text(Member.Member.ContactAlert);
    if (Member.Member.ParentalConsentThru) $('#OtherDemoParentalConsentThru').text(Member.Member.ParentalConsentThru);
    if (Member.Member.ParentalConsentFrom) $('#OtherDemoParentalConsentFrom').text(Member.Member.ParentalConsentFrom);
    if (Member.Member.PerferredCallTimeFrom) $('#OtherDemoPrefCallTimeFrom').text(Member.Member.PerferredCallTimeFrom);
    if (Member.Member.PerferredCallTimeThru) $('#OtherDemoPrefCallTimeThru').text(Member.Member.PerferredCallTimeThru);
    if (Member.Member.Spoken1) $('#OtherDemoSpoken1').text(Member.Member.Spoken1);
    if (Member.Member.Spoken2) $('#OtherDemoSpoken2').text(Member.Member.Spoken2);
    if (Member.Member.Written1) $('#OtherDemoWritten1').text(Member.Member.Written1);
    if (Member.Member.Written2) $('#OtherDemoWritten2').text(Member.Member.Written2);
}

function EnrollmentMethod(Member) {
    if (Member.Member.PersonalInformation.FirstName) $('#Enrollment_MemberName').text(Member.Member.PersonalInformation.FirstName + " " + Member.Member.PersonalInformation.LastName);
    if (Member.Member.Relationship) $("#Enrollment_Relationship").text(Member.Member.Relationship);
    if (Member.Member.ConfComm) $('#ConfComm').text(Member.Member.ConfComm);
    if (Member.Member.isTermActive) { $('#Enrollment_Status').text("Active"); } else { $('#Enrollment_Status').text("Inactive"); }
    if (Member.Member.PersonalInformation.Gender) $('#Enrollment_Gender').text(Member.Member.PersonalInformation.Gender);
    if (Member.Member.PersonalInformation.DOB) $('#Enrollment_BirthDate').text(Member.Member.PersonalInformation.DOB.formatDate());
    if (Member.Member.PersonalInformation.SSN) $("#Enrollment_SSN").text(Member.Member.PersonalInformation.SSN);
    if (Member.Member.MemberMemberships[0].Membership.MemberEffectiveDate) $('#Enrollment_EffDate').text(Member.Member.MemberMemberships[0].Membership.MemberEffectiveDate.formatDate());
    if (Member.Member.PersonalIdentificationInformations[0].IdentificationNumber) $('#Enrollment_MedicareNo').text(Member.Member.PersonalIdentificationInformations[0].IdentificationNumber);
    if (Member.Member.FamilyLinkID) $('#Enrollment_FamilylinkId').text(Member.Member.FamilyLinkID);
    if (Member.Member.HistoryLinkID) $('#Enrollment_HistoryLinkId').text(Member.Member.HistoryLinkID);
    if (Member.Member.MemberMemberships[0].Membership.SubscriberID) $('#Enrollment_HealthId').text(Member.Member.MemberMemberships[0].Membership.SubscriberID);
    if (Member.Member.ExclusionaryPeriodCreditDays) $('#Enrollment_CreditDays').text(Member.Member.ExclusionaryPeriodCreditDays);
    if (Member.Member.HippaStartDate) $('#Enrollment_HipStartDate').text(Member.Member.HippaStartDate.formatDate());
    if (Member.Member.HippaEndDate) $('#Enrollment_HipEndDate').text(Member.Member.HippaEndDate.formatDate());
    if (Member.Member.PreExStartDate) $('#Enrollment_PreStartDate').text(Member.Member.PreExStartDate.formatDate());
    if (Member.Member.PreExEndDate) $('#Enrollment_PreEndDate').text(Member.Member.PreExEndDate.formatDate());
    if (Member.Member.PreCreditDate) $('#Enrollment_PreCreditDays').text(Member.Member.PreCreditDate.formatDate());
    if (Member.Member.ApplicantType) $('#Enrollment_ApplicantType').text(Member.Member.ApplicantType);
    if (Member.Member.Limit) $('#limits').text(Member.Member.Limit);
    if (Member.Member.EligibilityDate) $('#Enrollment_EligibilityDate').text(Member.Member.EligibilityDate);
    if (Member.Member.QualifyingEventDate) $('#Enrollment_QEventDate').text(Member.Member.QualifyingEventDate);
    if (Member.Member.EOITerminationDate) $('#Enrollment_TerminalDate').text(Member.Member.EOITerminationDate);
    if (Member.Member.NewSignatureDate) $('#Enrollment_SignatureDate').text(Member.Member.NewSignatureDate);
    if (Member.Member.PriorBillingIndicator) $('#Enrollment_PBillingIndicator').text(Member.Member.PriorBillingIndicator);
    if (Member.Member.PriorBillingEFFDate) $('#Enrollment_PBillingEffDate').text(Member.Member.PriorBillingEFFDate);
    if (Member.Member.RecordNo) $('#Enrollment_RecordNo').text(Member.Member.RecordNo);
    if (Member.Member.MemberLanguages) if (Member.Member.MemberLanguages[0].Language.Name) $('#Enrollment_language').text(Member.Member.MemberLanguages[0].Language.Name)
    if (Member.Member.ContactInformation[0].AddressInformation[0].AddressLine1) {
        if (Member.Member.ContactInformation[0].AddressInformation[0].AddressLine2) $('#home').text(Member.Member.ContactInformation[0].AddressInformation[0].AddressLine1 + "," + Member.Member.ContactInformation[0].AddressInformation[0].AddressLine2 + "," + Member.Member.ContactInformation[0].AddressInformation[0].City + "," + Member.Member.ContactInformation[0].AddressInformation[0].State + "," + Member.Member.ContactInformation[0].AddressInformation[0].ZipCode);
        else $('#home').text(Member.Member.ContactInformation[0].AddressInformation[0].AddressLine1 + "," + Member.Member.ContactInformation[0].AddressInformation[0].City + "," + Member.Member.ContactInformation[0].AddressInformation[0].State + "," + Member.Member.ContactInformation[0].AddressInformation[0].ZipCode);
    }
    if (Member.Member.Mailing) $('#Mailing').text(Member.Member.Mailing);
    if (Member.Member.Work) $('#work').text(Member.Member.Work);
    if (Member.Member.ContactInformation[0].PhoneInformation.CellNumber) $('#cell_phone').text(Member.Member.ContactInformation[0].PhoneInformation.CellNumber);
    if (Member.Member.Memo) $('#Memo').text(Member.Member.Memo);

}

function AddressMethod(Member) {
    var choice = Member.Member.ContactInformation[0].AddressInformation[0].Choice
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

function COBMethod(Member) {
    if (Member.Member.CoBs) {
        if (Member.Member.CoBs.InsuranceType) $("#COB_InsuranceType").text(Member.Member.CoBs.InsuranceType);
        if (Member.Member.CoBs.Order) $("#COB_Order").text(Member.Member.CoBs.Order);
        if (Member.Member.CoBs.SupplementalDrugType) $("#COB_DragType").text(Member.Member.CoBs.SupplementalDrugType);
        if (Member.Member.CoBs.Carrier) $("#COB_Carrier").text(Member.Member.CoBs.Carrier);
        if (Member.Member.CoBs.Effective) $("#COB_Effective").text(Member.Member.CoBs.Effective);
    } else {
        $("#COBTableBody").html('<tr><td colspan="12" class="theme_label_data" style="text-align:center !important">No Data Available.</td></tr>');
    }
}

function EligibilityMethod(Member) {
    var Member = Member.Member.MemberMemberships[0].Membership;
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

function MedicareMethod(Member) {
    if (Member.Member.Medicare) {
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
        $("#MedicareTableBody").html('<tr><td colspan="12" class="theme_label_data" style="text-align:center !important">No Data Available.</td></tr>');
    }
}

function ICEMethod(Member) {
    if (Member.Member.Profile) {
        if (Member.Member.Profile.ContactInformation.ContactPerson[0].GuarantorType) $("#Guarantor_Type").text(Member.Member.Profile.ContactInformation.ContactPerson[0].GuarantorType);
        if (Member.Member.Profile.ContactInformation.ContactPerson[0].ContactPersonID) $("#Member_id").text(Member.Member.Profile.ContactInformation.ContactPerson[0].ContactPersonID);
        if (Member.Member.Profile.ContactInformation.ContactPerson[0].FirstName) $("#first_name").text(Member.Member.Profile.ContactInformation.ContactPerson[0].FirstName);
        if (Member.Member.Profile.ContactInformation.ContactPerson[0].LastName) $("#last_name").text(Member.Member.Profile.ContactInformation.ContactPerson[0].LastName);
        if (Member.Member.Profile.ContactInformation.ContactPerson[0].Relationship) $("#Relationship").text(Member.Member.Profile.ContactInformation.ContactPerson[0].Relationship);
        if (Member.Member.ResponsiblePerson.Email) $("#Email").text(Member.Member.ResponsiblePerson.Email);
        if (Member.Member.ResponsiblePerson.PhoneNumber) $("#Phone").text(Member.Member.ResponsiblePerson.PhoneNumber.formatTelephone());
        if (Member.Member.ResponsiblePerson.CellNumber) $("#Cell").text(Member.Member.ResponsiblePerson.CellNumber);
        if (Member.Member.ResponsiblePerson.AddressInformation.Address1) $("#Address1").text(Member.Member.ResponsiblePerson.AddressInformation.Address1);
        if (Member.Member.ResponsiblePerson.AddressInformation.Address2) $("#Address2").text(Member.Member.ResponsiblePerson.AddressInformation.Address2);
        if (Member.Member.ResponsiblePerson.AddressInformation.City) $("#City").text(Member.Member.ResponsiblePerson.AddressInformation.City);
        if (Member.Member.ResponsiblePerson.AddressInformation.State) $("#State").text(Member.Member.ResponsiblePerson.AddressInformation.State);
        if (Member.Member.ResponsiblePerson.AddressInformation.Zip) $("#Zip").text(Member.Member.ResponsiblePerson.AddressInformation.Zip);
        if (Member.Member.ResponsiblePerson.AddressInformation.County) $("#County").text(Member.Member.ResponsiblePerson.AddressInformation.County);
    }

}

function RatingMethod(Member) {
    var RatingOverrides = Member.Member.RatingOverrides;
    if (Member.Member.RatingOverrides) {
        if (RatingOverrides.EffectiveDate) $("#Rating_effDate").text(RatingOverrides.EffectiveDate.formatDate());
        if (RatingOverrides.PlanID) $("#Rating_PlanId").text(RatingOverrides.PlanID);
        if (RatingOverrides.IdentificationType) $("#Rating_type").text(RatingOverrides.IdentificationType);
        if (RatingOverrides.Units) $("#Rating_units").text(RatingOverrides.Units);
        if (RatingOverrides.Factors) $("#Rating_factors").text(RatingOverrides.Factors);
        if (RatingOverrides.Amount) $("#Rating_amount").text(RatingOverrides.Amount);
    } else {
        $("#RatingOverrideTableBOdy").html('<tr><td colspan="12" class="theme_label_data" style="text-align:center !important">No Data Available.</td></tr>');
    }
}

function ImmuneMethod(Member) {
    var screening = Member.Member.HealthScreenings;
    if (Member.Member.HealthScreenings) {
        if (screening.PFX) $("#PFX").text(screening.PFX);
        if (screening.Code) $("#CODE").text(screening.Code);
        if (screening.SFX) $("#SFX").text(screening.SFX);
        if (screening.Last) $("#LAST").text(screening.Last);
        if (screening.Next) $("#NEXT").text(screening.Next);
        if (screening.Comment) $("#COMMENTS").text(screening.Comment);
    } else {
        $("#ImmuneHSTableBody").html('<tr><td colspan="12" class="theme_label_data" style="text-align:center !important">No Data Available.</td></tr>');
    }
}

function ResponsibleMethod(Member) {
    if (Member.Member.Profile) {
        if (Member.Member.Profile.ContactInformation.ContactPerson[0].GuarantorType) $("#Responsible_GaurantorType").text(Member.Member.Profile.ContactInformation.ContactPerson[0].GuarantorType);
        if (Member.Member.Profile.ContactInformation.ContactPerson[0].ContactPersonID) $("#Responsible_MemberId").text(Member.Member.Profile.ContactInformation.ContactPerson[0].ContactPersonID);
        if (Member.Member.Profile.ContactInformation.ContactPerson[0].FirstName) $("#Responsible_FirstName").text(Member.Member.Profile.ContactInformation.ContactPerson[0].FirstName);
        if (Member.Member.Profile.ContactInformation.ContactPerson[0].LastName) $("#Responsible_LastName").text(Member.Member.Profile.ContactInformation.ContactPerson[0].LastName);
        if (Member.Member.ResponsiblePerson.Email) $("#Responsible_Email").text(Member.Member.ResponsiblePerson.Email);
        if (Member.Member.ResponsiblePerson.PhoneNumber) $("#Responsible_Phone").text(Member.Member.ResponsiblePerson.PhoneNumber.formatTelephone());
        if (Member.Member.ResponsiblePerson.Cell) $("#Responnsible_Cell").text(Member.Member.ResponsiblePerson.Cell);
        if (Member.Member.ResponsiblePerson.AddressInformation.Address1) $("#Responsible_Address1").text(Member.Member.ResponsiblePerson.AddressInformation.Address1);
        if (Member.Member.ResponsiblePerson.AddressInformation.Address2) $("#Reponsible_Address2").text(Member.Member.ResponsiblePerson.AddressInformation.Address2);
        if (Member.Member.ResponsiblePerson.AddressInformation.City) $("#Responsible_City").text(Member.Member.ResponsiblePerson.AddressInformation.City);
        if (Member.Member.ResponsiblePerson.AddressInformation.State) $("#Responsible_State").text(Member.Member.ResponsiblePerson.AddressInformation.State);
        if (Member.Member.ResponsiblePerson.AddressInformation.Zip) $("#Responsible_Zip").text(Member.Member.ResponsiblePerson.AddressInformation.Zip);
        if (Member.Member.ResponsiblePerson.AddressInformation.County) $("#Responsible_County").text(Member.Member.ResponsiblePerson.AddressInformation.County);
    }
}

//======================================================= Get the Print ===============================================
var printThisProviderData = function (whatId, windowTitle) {
    var HiddenDivId = '#HiddenProviderProfilePrint';
    var content = "";

    $(HiddenDivId).empty();

    var mywindow = window.open('', $(HiddenDivId).html(), 'height=' + screen.height + ',width=' + screen.width);
    mywindow.document.write('<html><head><title>' + windowTitle + '</title>');
    mywindow.document.write('<link rel="stylesheet" href="~/Content/bootstrap.min.css" type="text/css" />');
    mywindow.document.write('<style>@page{size: auto;margin-bottom:0.5cm;margin-top:0.5cm;margin-left:0cm;margin-right:0cm;}</style>');
    mywindow.document.write('</head><body style="background-color:white;word-wrap: break-word;">');
    mywindow.document.write('<p class="col-lg-3 col-md-3 col-sm-3 col-xs-3 pull-left" style="margin-bottom:0px;"><img src="/Content/svg/new_1.png"></p><br/><br/>');
    mywindow.document.write('<div style="text-align: center; font-size: 20px; font-weight: 900;">' + windowTitle + '</div><br/><br/><br/>');
    if (whatId.length == 0) {
        mywindow.document.write('<span style="text-align: center; padding-left: 8px; font-weight: bold;">NO DATA AVAILABLE</span>');
    }
    else if (windowTitle === "UNDER GRADUATE / PROFESSIONAL SCHOOL DETAILS") {
        for (var i = 0; i < whatId.length; i++) {
            if (i !== 18 && i !== 37 && i !== 55) {
                content = content + '<span style="margin-left: 20px; font-weight: bold;">' + whatId[i].innerText + '</span><br/><br/>';
            }
        }
    }
    else {
        for (var i = 0; i < whatId.length; i++) {

            content = content + '<span style="margin-left: 20px; font-weight: bold;">' + whatId[i].innerText + '</span><br/><br/>';

        }
    }
    mywindow.document.write(content);
    mywindow.document.write('</body></html>');

    mywindow.document.close();
    mywindow.focus();

    $(mywindow).ready(function () {
        setTimeout(function () {
            mywindow.print();
            mywindow.close();
        }, 1500);
    });
}
//======================================================= /Get the Print ===============================================


//var theTracker = [];
//var temp = { id: '#SummaryTabView', pos: 0 };
//theTracker.push(temp);
//var i = 1;
var theProviderURL = [];

var UrlLoaded = function (url) {
    var index = theProviderURL.indexOf(url);
    if (index > -1) {
        theProviderURL.splice(index, 1);
    }
}

$(function () {


    setTimeout(function () {
        $('.ViewStateLicense').off('click', '.StateHistoryBtn').on('click', '.StateHistoryBtn', function () {
            $('.StateHistoryClass').removeAttr('id');
            $('.StateHistoryClass').html('');
            $('.StateHistoryClass').attr('id', this.dataset.tabContainer);
          if($('.StateSectionHistoryBtn').attr("aria-expanded")===true){
              $('#StateLicenseHistory').toggle();
}
        })
        $('.ViewStateLicense').off('click', '.StateSectionHistoryBtn').on('click', '.StateSectionHistoryBtn', function () {
            $('.StateHistoryClass').removeAttr('id');
            $('.StateHistoryClass').html('');           
        })
        //$('.ViewStateLicense').off('click', '.CDSHistoryBtn').on('click', '.CDSHistoryBtn', function () {
        //    $('.CDSHistoryClass').removeAttr('id');
        //    $('.CDSHistoryClass').html('');
        //    $('.CDSHistoryClass').attr('id', this.dataset.tabContainer);           
        //})
        //$('.ViewStateLicense').off('click', '.CDSSectionHistoryBtn').on('click', '.CDSSectionHistoryBtn', function () {
        //    $('.CDSHistoryClass').removeAttr('id');
        //    $('.CDSHistoryClass').html('');
        //})
    },500);
    //function Utils() { }

    //Utils.prototype = {
    //    constructor: Utils,
    //    isElementInView: function (element, fullyInView) {
    //        try {
    //            var pageTop = $(window).scrollTop();
    //            var pageBottom = pageTop + $(window).height();
    //            var elementTop = $(element).offset().top;
    //            var elementBottom = elementTop + $(element).height();

    //            if (fullyInView === true) {
    //                return ((pageTop < elementTop) && (pageBottom > elementBottom));
    //            } else {
    //                return ((elementBottom <= pageBottom) && (elementTop >= pageTop));
    //            }
    //        }
    //        catch (e) {

    //        }
    //    }
    //};

    //function chk_scroll(e) {
    //    var elem = $(e);
    //    if (elem[0].scrollHeight - elem.scrollTop() == elem.outerHeight() - 5) {
    //        return true;
    //    }
    //    return false;
    //}
    var lastId;
    var topMenu = $('#ProviderProfile').find('ul:first').find('li');
    var topMenuHeight = topMenu.outerHeight() + 15;
    // All list items
    var menuItems = topMenu.find("a");
    var scrollItems = menuItems.map(function () {
        var item = $('#' + $(this).attr("data-tab-container"));
        if (item.length) { return item; }
    });

    var theLength = $('#ProviderProfile').find('ul:first').find('li').length;
    for (var i = 0; i < theLength; i++) {
        theProviderURL[i] = { 'url': $('#ProviderProfile').find('ul:first').find('li').eq(i).find('a').data().tabPath, 'container': $('#ProviderProfile').find('ul:first').find('li').eq(i).find('a').data().tabContainer };
        }

    //var Utils = new Utils();
    //var tempinnercontenerheightflag = 0;
    //var position = $('#ProviderProfileTabView').scrollTop();
    $("#ProviderProfileTabView").off('scroll').on('scroll', function () {

        //if (Utils.isElementInView($('.target'), true)) {

        //}
        //if (Utils.isElementInView($('.smallheaderTab'), true)) {
        //$('#breadCrumbArea,#mainContent>.tab-container').show('slow', function () {
        //    if (tempinnercontenerheightflag == 0) {
        //        $('.innerContainerArea').css('min-height', $(window).outerHeight() + 100);
        //        $('.scrollable-container').css('min-height', $('.innerContainerArea').height() + 120);
        //        tempinnercontenerheightflag = 1;
        //    }
        //});
        //} else {
        //$('#breadCrumbArea,#mainContent>.tab-container').hide('slow', function () {
        //    jQuery.fx.off = true;
        //    if (tempinnercontenerheightflag == 1) {
        //        $('.innerContainerArea').css('min-height', $(window).outerHeight() + 100);
        //        $('.scrollable-container').css('min-height', $('.innerContainerArea').height() + 200);
        //        tempinnercontenerheightflag = 0;
        //    }
        //});
        //}

        //=========================================== Provider Profile Scroll (Get Data) =======================================================
        //var scroll = $(this).scrollTop();

        var fromTop = $(this).scrollTop() + topMenuHeight;

        if (fromTop < 480) {
            $('#breadCrumbArea,#mainContent>.tab-container').show('slow', function () {
                    $('.innerContainerArea').css('min-height', $(window).outerHeight() + 100);
                    $('.scrollable-container').css('min-height', $('.innerContainerArea').height() + 120);
            });
        }
        else {
            $('#breadCrumbArea,#mainContent>.tab-container').hide('slow', function () {
                    $('.innerContainerArea').css('min-height', $(window).outerHeight() + 100);
                    $('.scrollable-container').css('min-height', $('.innerContainerArea').height() + 200);
            });
        }
        //if ((fromTop >= 485 && fromTop < 555) || (fromTop >= 1650 && fromTop < 1675) || (fromTop >= 2750 && fromTop < 2840) ) 
        // Get id of current scroll item
        var cur = scrollItems.map(function () {
            //if ($(this).offset().top < fromTop)
            //    return this;
            if (fromTop < 485) {
                return scrollItems[0];
            }
            else if (fromTop >= 485 && fromTop < 1745) {
                return scrollItems[1];
            }
            else if (fromTop >= 1745 && fromTop < 2945) {
                return scrollItems[2];
            }
            else if (fromTop >= 2945 && fromTop < 3445) {
                return scrollItems[3];
            }
            else if (fromTop >= 3445 && fromTop < 5845) {
                return scrollItems[4];
            }
            else if (fromTop >= 5845 && fromTop < 7745) {
                return scrollItems[5];
            }
            else if (fromTop >= 7745 && fromTop < 8845) {
                return scrollItems[6];
            }
            else if (fromTop >= 8845 && fromTop < 9145) {
                return scrollItems[7];
            }
            else if (fromTop >= 9145 && fromTop < 9545) {
                return scrollItems[8];
            }
            else if (fromTop >= 9545 && fromTop < 9945) {
                return scrollItems[9];
            }
            else if (fromTop >= 9945 && fromTop < 10345) {
                return scrollItems[10];
            }
            else if (fromTop >= 10345 && fromTop < 11045) {
                return scrollItems[11];
            }
            else if (fromTop >= 11045) {
                return scrollItems[12];
            }
        });

        // Get the id of the current element
        cur = cur[cur.length - 1];
        var id = cur && cur.length ? cur[0].id : "";

        if (lastId !== id) {
            lastId = id;
            var theCurrElem = $('#ProviderProfile').find('ul:first').find('li').find("[data-tab-container='" + id + "']").parent();
            GetTheTabName(theCurrElem);
            //menuItems
            //    .end().removeClass("current")
            //    .end().find("[data-tab-container='" + id + "']").parent().addClass('current');
                }

        //if (this.scrollTop < 20) {
        //    jQuery.fx.off = true;
        //    $('#ProviderProfile').find('.tab-item-sec-1').addClass('current');
        //    $('#ProviderProfile').find('.tab-item-sec-1').siblings().removeClass("current");
        //    $('#ProfileSelectedTabName').text("SUMMARY");
        //    $('#breadCrumbArea,#mainContent>.tab-container').show(function () {
        //        if (tempinnercontenerheightflag == 0) {
        //            $('.innerContainerArea').css('min-height', $(window).outerHeight() + 100);
        //            $('.scrollable-container').css('min-height', $('.innerContainerArea').height() + 120);
        //            tempinnercontenerheightflag = 1;
        //        }
        //    });
        //}

        //if ($('#ProviderProfileTabView')[0].scrollHeight > 1700) {
        //    $('#ProviderProfileTabView').css("height", "900px");
        //    $('.theHideDiv').css("display", "none");
        //}

        //if (scroll > position) {
        //    var theInx = $('#ProviderProfile .current').index();
        //    var theDiv = $('#ProviderProfileTabView .tabScrollProviderCA:eq(' + (theInx + 1) + ')');
        //    if (theDiv.children().hasClass("clearfix") == true) {
        //        var winTop = $('#ProviderProfileTabView').scrollTop();
        //        var $divs = $('#ProviderProfileTabView .tabScrollProviderCA');
        //        var top = $.grep($divs, function (item) {
        //            return $(item).position().top <= winTop;
        //        });
        //        $divs.removeClass('active');
        //        $(top).addClass('active');

        //        $.each($('#ProviderProfile ul:first li'), function (items) {
        //            var theLi = $('#ProviderProfile ul:first li:eq(' + items + ')').find('a');
        //            if ($('#ProviderProfileTabView').find('.active:last').attr('id') == theLi.attr('data-tab-container')) {
        //                theLi.parent().siblings().removeClass('current');
        //                theLi.parent().addClass('current');
        //            }
        //        });


        //        var innerTabName = $('#ProviderProfile').find('ul:first').children('.current').text();
        //        $('#ProfileSelectedTabName').text(innerTabName);
        //    }
        //    else {
        //        if (chk_scroll(this)) {
        //            var MethodName = '';
        //            var url = $('#ProviderProfile').find('.current').next().find('a').data().tabPath;
        //            var theID = $('#ProviderProfile').find('.current').next().find('a').data().tabContainer;
        //            var theDataVal = $('#ProviderProfile').find('.current').next().find('a').parent().data().val;

        //            //ScrollForInitialLoadProfile(url, theDataVal, theID);
        //            //TabManager.getDynamicContent(url, theID, MethodName, '');
        //            $('#ProviderProfile').find('ul:first').children('.current').next().addClass("current");
        //            $('#ProviderProfile').find('ul:first').children('.current:last').siblings().removeClass("current");

        //            var innerTabName = $('#ProviderProfile').find('ul:first').children('.current').text();
        //            $('#ProfileSelectedTabName').text(innerTabName);
        //        }
        //        $('[data-toggle="popover"]').popover();
        //    }
        //}
        //else {
        //    var winTop = $('#ProviderProfileTabView').scrollTop();
        //    var $divs = $('#ProviderProfileTabView .tabScrollProviderCA');
        //    var theIndexCount = 0;
        //    var top = $.grep($divs, function (item) {
        //        if ($('#ProviderProfile ul:first li').next('.current').index() >= theIndexCount) {
        //            theIndexCount++;
        //            return $(item).position().top <= winTop;
        //        }
        //    });
        //    $divs.removeClass('active');
        //    $(top).addClass('active');

        //    $.each($('#ProviderProfile ul:first li'), function (items) {
        //        var theLi = $('#ProviderProfile ul:first li:eq(' + items + ')').find('a');
        //        if ($('#ProviderProfileTabView').find('.active:last').attr('id') == theLi.attr('data-tab-container')) {
        //            theLi.parent().siblings().removeClass('current');
        //            theLi.parent().addClass('current');
        //        }
        //    });
        //    var innerTabName = $('#ProviderProfile').find('ul:first').children('.current').text();
        //    $('#ProfileSelectedTabName').text(innerTabName);

        //    if ($('#ProviderProfileTabView')[0].scrollHeight == 6993) {
        //        alert("kyun");
        //    }
        //}

        //position = scroll;

        //========================================= /Provider Profile Scroll =======================================================


        //    for(var j=0; j< theTracker.length;j++){
        //        if ($(this).scrollTop() == theTracker[j].pos) {
        //            alert("================== this ====================");
        //        }
        //    }

    });
})

$('.float-nav').click(function () {
    $('.main-nav, .menu-btn').toggleClass('active');
});

//var offset = $('#profileTabs').offset();
//console.log(offset.left, offset.top);
//$(document).off('DOMMouseScroll onwheel mousewheel onmousewheel ontouchmove scroll', '#ProviderProfile').on('DOMMouseScroll onwheel mousewheel onmousewheel ontouchmove', '#innerTabContainer', function (event) {
//    //$("#ProviderProfile").scroll(function () {
//    var demooffset = $('#IDDemographics').offset();
//    $(".tab-item").parent().siblings().removeClass("current");

//    if (offset.top <= demooffset.top && demooffset.top <= offset.top + 400) {
//        var atPos = $(this).attr('id');
//        console.log(atPos);
//        //$(this).parent().siblings().removeClass("current");
//        var kref_this = $("ul.x-dtablist li").find(".tab-item.current").removeClass('current');
//        //$('ul.x-dtablist').find('li  .current').removeClass('current');
//        // console.log("success", ref_this);
//    }
//    console.log(demooffset.top);
//    //var demooffset = $('#IDDiscolsureQuestions').offset();
//    //console.log( demooffset.top);
//});  

$('#ScrollToDown').click(function () {
    try {
        $('#DisclosureScroll').click()
    }
    catch (e) { };
});
$('#ScrollToTop').click(function () {
    try {
        $('#SummaryScroll').click();
    }
    catch (e) { };
});
//$(".x-dtablist a").click(function () {
//    try {
//        var aTag = $(this).parent().attr("data-val");
//        var innerTabName = $(this).text();
//        $('#ProfileSelectedTabName').text(innerTabName);
//        var container = $('#ProviderProfileTabView');
//        var top = $("#" + aTag).position().top - 80,
//        currentScroll = container.scrollTop();
//        container.animate({
//            scrollTop: currentScroll + top
//        }, 500);
//    }
//    catch (e) { }
//})

//$(document).ready(function () {
//    $('#ProviderProfileTabView').on("scroll", onScroll);

//    //smoothscroll
//    $('.x-dtablist a').on('click', function (e) {
//        e.preventDefault();
//        $('#ProviderProfileTabView').off("scroll");

//        $('a').each(function () {
//            $(this).removeClass('active');
//        })
//        $(this).addClass('active');

//        //var target = this.hash,
//        //    menu = target;
//        var aTag = $(this).parent().attr("data-val");
//        $('#ProviderProfileTabView').stop().animate({
//            'scrollTop': $("#" + aTag).offset().top + 2
//        }, 500, 'swing', function () {

//            $('#ProviderProfileTabView').on("scroll", onScroll);
//        });
//    });
//});

//function onScroll(event) {
//    var scrollPos = $(document).scrollTop();
//    $('#menu-center a').each(function () {
//        var currLink = $(this);
//        var refElement = $(currLink.attr("href"));
//        if (refElement.position().top <= scrollPos && refElement.position().top + refElement.height() > scrollPos) {
//            $('#menu-center ul li a').removeClass("active");
//            currLink.addClass("active");
//        }
//        else {
//            currLink.removeClass("active");
//        }
//    });
//}

// Function for displaying the Add and view
function ShowHideAddView(ShowId, HideId) {
    $('#' + ShowId).removeClass('hidden');
    $('#' + HideId).addClass('hidden');
}

function ShowAddView(ShowId) {
    if ($('#' + ShowId).is(':hidden')) {
        $('#' + ShowId).removeClass('hidden');
    } else {
        $('#' + ShowId).addClass('hidden');
    }
}

function ShowHideAddViewClass(ShowClass, HideClass) {
    $('.' + ShowClass).removeClass('hidden');
    $('.' + HideClass).addClass('hidden');
}
// funciton for searching inside the profile----profile Search
$("#profileSearch").keypress(function (e) {
    if (e.which == 13 && $('#profileSearch').val() != '') {
        var theText = $('#profileSearch').val();

        $("#ProviderProfileTabView").each(function () {
            if ($(this).text().search(new RegExp(theText, "i")) > 0) {
                $("#ProfileProviderSearch").modal("show");
            }
            else {
                $("#ProfileProviderSearch").modal("show");
            }
        });

        //$("#ProfileProviderSearch").modal("show");
    }
});


//==============================================================ProviderBuilder===============================================================================//
var ShowProfileBuilder = function () {
    $(".floatSideBar").css("min-width", "573px");
    $(".builderToggle").css("right", "572px");
    $("#profileBuilder").toggle("slow");

}
var ToggleProfileBuilder = function () {
    //$(".floatSideBar").click(function () {
    $(".floatSideBar").css("min-width") === "0px" ? $(".floatSideBar").css("min-width", "573px") : $(".floatSideBar").css("min-width", "0px");
    $(".floatSideBar").css("min-width") === "0px" ? $(".builderToggle").css("right", "0px") : $(".builderToggle").css("right", "572px");
    //},500)
}
function ScrollForCompleteProfile(TagValue, TabName, Method, dataTabContainer, ActiveTabId) {
    $.ajax({
        url: "/CredAxis/" + Method + "/Index?ModeRequested=" + "EDIT",// the method we are calling
        success: function (result) {
            $("#" + dataTabContainer).html(result);
            try {
                ToggleProfileBuilder();
                var aTag = TagValue;//$(this).parent().attr("data-val");
                var innerTabName = TabName;
                $('#ProfileSelectedTabName').text(innerTabName);
                $('#' + ActiveTabId).addClass("current");
                $('#' + ActiveTabId).siblings().removeClass("current");
                var container = $('#ProviderProfileTabView');
                var top = $("#" + aTag).position().top - 80,
                currentScroll = container.scrollTop();
                container.animate({
                    scrollTop: currentScroll + top
                }, 500);
            }
            catch (e) { }
        },
        error: function (result) {
        }
    });
}

//var theIndex = 0;
//function ScrollForInitialLoadProfile(theUrl, TagValue, dataTabContainer) {
//    //var index = theProviderURL.indexOf(theUrl);
//    //if (index > -1) {
//    $.ajax({
//        //type: "POST",
//        //async: false,
//        url: theUrl,// the method we are calling
//        success: function (result) {
//            $("#" + dataTabContainer).html(result);
//            try {
//                var aTag = TagValue;//$(this).parent().attr("data-val");
//                //var innerTabName = TabName;
//                //$('#ProfileSelectedTabName').text(innerTabName);
//                var container = $('#ProviderProfileTabView');
//                var top = $("#" + aTag).position().top - 100,
//                currentScroll = container.scrollTop();
//                container.animate({
//                    scrollTop: currentScroll + top
//                }, 500);
//                //UrlLoaded(theUrl);
//                //theIndex++;
//                //ScrollForInitialLoadProfile(theProviderURL[theIndex]);
//            }
//            catch (e) { }
//        },
//        error: function (result) {


//        }

//    });
//}
//==============================================================ProviderBuilder END===============================================================================//

function ScrollForInitialLoadProfile(tagValue, dataTabContainer) {
    var aTag = tagValue;//$(this).parent().attr("data-val");
                //var innerTabName = TabName;
                //$('#ProfileSelectedTabName').text(innerTabName);
                var container = $('#ProviderProfileTabView');
                var top = $("#" + aTag).position().top - 100,
                currentScroll = container.scrollTop();
                container.animate({
                    scrollTop: currentScroll + top
                }, 500);
            }

function GetTheTabName(thisVal) {
    thisVal.addClass("current");
    thisVal.siblings().removeClass("current");
    var innerTabName = $('#ProviderProfile').find('ul:first').children('.current').text();
    $('#ProfileSelectedTabName').text(innerTabName);
}


var w;
function startWorker() {
    if (typeof (Worker) !== "undefined") {
        if (typeof (w) == "undefined") {
            w = new Worker("../Areas/CredAxis/Scripts/ProviderProfile/NonMinified/WebWorker.js");
        }
        w.onmessage = function (event) {
            document.getElementById("ProviderProfileTabView").innerHTML = event.data;
        };
        w.terminate();
        w = undefined;
    } else {
        document.getElementById("ProviderProfileTabView").innerHTML = "Sorry, your browser does not support this Application...";
        }
}

//Capture Selected Specialty ID
var SpecialtyID = 0;
function AssignSpecialtyID(id) {
    SpecialtyID = id;
}



//Remove a Specialty by ID
function RemoveSpecialtyInfo() {
    var action = "Remove";
    $.ajax({
        url: "/CredAxis/Speciality/RemoveSpecialty",// the method we are calling
        data: { ModeRequested: action, id: SpecialtyID },
        success: function (result) {
            $("#SpecialtyConfirmationId").hide();
            $("#SpecialityTabView").html(result);
        },
        error: function (result) {


        }

    });
}

//Select a billing address
function SelectBillingContact(val){
    if(val=="Yes"){
        $("#BillingContactSelectedID").show();
    } else if (val == "No") {
        $("#BillingContactSelectedID").hide();
    }
}

function groupmodalhistory(controllerName, MethodName, Value, DivId) {
    $("#groupmodalfooter").empty();
    $("#groupmodalfooter").append('<button data-dismiss="modal" class="btn btn-success close_modal_btn list-item"  data-tab-path="/CredAxis/' + controllerName + '/' + MethodName + '?value=' + Value + '" data-tab-container="' + DivId + '">YES</button>');
    $('#GroupInformationModal').modal('show');
}
function FacilityServiceModal() {
    $('#PracticeLocationServiceFacilityModal').modal('show');
}

