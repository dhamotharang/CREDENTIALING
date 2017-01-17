
/** 
@description Navigating to select member page
 */
function GoToSelectMember() {

    MakeItActive(5, currentProgressBarData);
}
$('#GoToSelectMemberBtn').on('click', GoToSelectMember);

/** 
@description Navigating to cms1500 form
 */
function GoToCMS1500Form(e) {
   // e.preventDefault();
    if (createClaimByType != 'Multiple claims for a Provider') {
        currentProgressBarData[4].postData = new FormData($('#claimInformationForm')[0]);
        MakeItActive(5, currentProgressBarData);
    } else {
        currentProgressBarData[5].postData = new FormData($('#claimInformationForm')[0]);
        MakeItActive(6, currentProgressBarData);
    }

}
//$('#GoToCMS1500Btn').on('click', GoToCMS1500Form);
$("#claimInformationForm").validate({
    rules: {
        'CodedEncounter.Encounter.DateOf_From':  "notFutureDate",
        'CodedEncounter.Encounter.DateOf_To': {

            greaterThan: "CodedEncounter.Encounter.DateOf_From",
            notFutureDate: "CodedEncounter.Encounter.DateOf_From",
        },
        'CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.Member.ContactInfo.Address.AddressLine1': {
            DependentRequired: function (element) {

                return (!($("#spouse").is(":checked")))
            }
        },

        'CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.Member.Subscriber[0].Plan.PlanName': "required AlphaNumeric",
        'CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.Member.Subscriber[0].Plan.PlanUniqueId': "required LengthRange2to80 AlphaNumeric",



        'CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.Member.ContactInfo.Address.AddressLine1': 'required OnlyLettersNumbersSpecialCharacters9 LengthRange1to55',

        'CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.Member.ContactInfo.Address.City': 'required LengthRange2to30 OnlyLettersNumbersSpecialCharacters9',
        'CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.Member.ContactInfo.Address.AddressLine2': 'LengthRange1to55 OnlyLettersNumbersSpecialCharacters8',
        'CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.Member.ContactInfo.Address.ZipCode': 'required ZipCode',
        'CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.Member.ContactInfo.Phone': 'Phone',

        'CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.Member.Subscriber[0].Plan.Address.City': 'required LengthRange2to30 OnlyLettersNumbersSpecialCharacters8',
        'CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.Member.Subscriber[0].Plan.Address.AddressLine1': 'required OnlyLettersNumbersSpecialCharacters8 LengthRange1to55',
        'CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.Member.Subscriber[0].Plan.Address.AddressLine2': 'OnlyLettersNumbersSpecialCharacters8 LengthRange1to55',

        'CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.Member.ContactInfo.Address.City': {
            PatientInfoRequired: function (element) {

                var RelationshipCode = $("[name='CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.Member.PatientRelationShipCode']:checked").val();
                var status = false;
                if (RelationshipCode == '18') {
                    status = true;
                } else {
                    if (element.value.trim() != '')
                        status = true;
                }
                return status;
            }
        },

        'CodedEncounter.Diagnosis[0].ICDCode': 'required LettersNumbersDecAt4thOr5th LengthRange1to30',
        'CodedEncounter.Diagnosis[1].ICDCode': 'LettersNumbersDecAt4thOr5th LengthRange1to30',
        'CodedEncounter.Diagnosis[2].ICDCode': 'LettersNumbersDecAt4thOr5th LengthRange1to30',
        'CodedEncounter.Diagnosis[3].ICDCode': 'LettersNumbersDecAt4thOr5th LengthRange1to30',
        'CodedEncounter.Diagnosis[4].ICDCode': 'LettersNumbersDecAt4thOr5th LengthRange1to30',
        'CodedEncounter.Diagnosis[5].ICDCode': 'LettersNumbersDecAt4thOr5th LengthRange1to30',
        'CodedEncounter.Diagnosis[6].ICDCode': 'LettersNumbersDecAt4thOr5th LengthRange1to30',
        'CodedEncounter.Diagnosis[7].ICDCode': 'LettersNumbersDecAt4thOr5th LengthRange1to30',
        'CodedEncounter.Diagnosis[8].ICDCode': 'LettersNumbersDecAt4thOr5th LengthRange1to30',
        'CodedEncounter.Diagnosis[9].ICDCode': 'LettersNumbersDecAt4thOr5th LengthRange1to30',
        'CodedEncounter.Diagnosis[10].ICDCode': 'LettersNumbersDecAt4thOr5th LengthRange1to30',
        'CodedEncounter.Diagnosis[11].ICDCode': 'LettersNumbersDecAt4thOr5th LengthRange1to30',

        'CodedEncounter.Encounter.DateOf_From': 'required ValidateDateFormatHCFAForm',

        DOB: 'required ValidateDateFormatClaimInfo',

        'CodedEncounter.Encounter.EncounterType': 'requiredPlan',

        'item.Amount': 'required TwoDecimal',

        'item.Modifier1': "LengthRangeonly2",
        'item.Modifier2': "LengthRangeonly2",
        'item.Modifier3': "LengthRangeonly2",
        'item.Modifier4': "LengthRangeonly2",


        'CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.Facility.FacilityName': 'required AlphaNumeric',
        'CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.Facility.Address.AddressLine1': 'required LengthRange1to55',

        'HospitalisationAdmissionDateFrom': 'ValidateDateFormatClaimInfo',
        'HospitalisationAdmissionDateTo': 'ValidateDateFormatClaimInfo',
        'LatestConsultationDate': 'ValidateDateFormatClaimInfo',
        'DateOfInitialTreatment': 'ValidateDateFormatClaimInfo notFutureDate',

        'CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.Facility.Address.AddressLine2': 'LengthRange1to55 AlphaNumeric',

        'CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.BillingProvider.NPI': 'AlphaNumeric'

    },


    //showErrors: function (errorMap, errorList) {

    //    // Clean up any tooltips for valid elements

    //    //$.each(this.validElements(), function (index, element) {
    //    //    var $element = $(element);
    //    //    $element.data("title", "") // Clear the title - there is no error associated anymore
    //    //        .removeClass("field-validation-error")
    //    //        .tooltip("destroy");
    //    //});
    //    // Create new tooltips for invalid elements
    //    $.each(errorList, function (index, error) {
    //        var $element = $(error.element);
    //        $element
    //            .addClass("field-validation-error");



    //        // Create a new tooltip based on the error messsage we just set in the title
    //    });
    //},
    submitHandler: function (form) {
        // form.submit();
        GoToCMS1500Form();
    }
});

/** 
@description Add new service lines
 */
$('#claimInformationForm').off('click', '#AddNewServiceLineBtn').on('click', '#AddNewServiceLineBtn', function () {

    event.preventDefault();
    var tbody = $("form#claimInformationForm").find('#ServiceLineTbody');
    var lasttr = tbody[0].lastElementChild;
    var prevIndex = parseInt($(lasttr).find('input').attr('id').match(/\d+/)[0]);
    var templateIndex = ++prevIndex;
    $.ajax({
        type: 'GET',

        url: '/Billing/CreateClaim/CreateServiceLine?index=' + templateIndex,
        success: function (data) {
            $('#ServiceLineTbody').append(data);
            SetColoumnNameForTable()
        }
    });
})

function SetColoumnNameForTable() {
    var sNo = 0;
    $('#ServiceLineTbody tr').each(function () {
        if ($(this).closest('tr').next().length)
            $(this).children("td:last").html('<td><button class="btn btn-warning btn-xs pull-right RemoveRow"><i class="fa fa-minus"></i></button></td>');
        $(this).children("td:first").text(++sNo);
    })
}

//var rowIndex = 1;
//var TableCountCT = 1;
//$('#AddNewServiceLineBtn').on('click', function (e) {
//    e.preventDefault();
//    if (TableCountCT == 1) {
//        TableCountCT = 0;
//        $('#ServiceLineTbody tr').append('<td onclick="RemoveThisRow(this)"><i class="fa fa-close"></i></td>');
//    }
//    template = '<tr>'+
//                        '<td>' + (rowIndex + 1) + '</td>'+
//                        '<td>'+
//                            '<input class="form-control input-xs non_mandatory_field_halo" id="ServiceLines_' + rowIndex + '__claimsProcedure" name="ServiceLines[' + rowIndex + '].claimsProcedure" type="text" value="">'+
//                       '</td>'+
//                        '<td>'+
//                            '<input class="form-control input-xs non_mandatory_field_halo" id="ServiceLines_' + rowIndex + '__Modifier1" name="ServiceLines[' + rowIndex + '].Modifier1" type="text" value="">'+
//                        '</td>'+
//                        '<td>'+
//                            '<input class="form-control input-xs non_mandatory_field_halo" id="ServiceLines_' + rowIndex + '__Modifier2" name="ServiceLines[' + rowIndex + '].Modifier2" type="text" value="">'+
//                        '</td>'+
//                        '<td>'+
//                            '<input class="form-control input-xs non_mandatory_field_halo" id="ServiceLines_' + rowIndex + '__Modifier3" name="ServiceLines[' + rowIndex + '].Modifier3" type="text" value="">'+
//                        '</td>'+
//                        '<td>'+
//                            '<input class="form-control input-xs non_mandatory_field_halo" id="ServiceLines_' + rowIndex + '__Modifier4" name="ServiceLines[' + rowIndex + '].Modifier4" type="text" value="">'+
//                        '</td>'+
//                        '<td>'+
//                            '<input class="form-control input-xs non_mandatory_field_halo" id="ServiceLines_' + rowIndex + '__DiagnosisPointer1" name="ServiceLines[' + rowIndex + '].DiagnosisPointer1" type="text" value="">'+
//                        '</td>'+
//                        '<td>'+
//                            '<input class="form-control input-xs non_mandatory_field_halo" id="ServiceLines_' + rowIndex + '__DiagnosisPointer2" name="ServiceLines[' + rowIndex + '].DiagnosisPointer2" type="text" value="">'+
//                        '</td>'+
//                        '<td>'+
//                            '<input class="form-control input-xs non_mandatory_field_halo" id="ServiceLines_' + rowIndex + '__DiagnosisPointer3" name="ServiceLines[' + rowIndex + '].DiagnosisPointer3" type="text" value="">'+
//                        '</td>'+
//                        '<td>'+
//                            '<input class="form-control input-xs non_mandatory_field_halo" id="ServiceLines_' + rowIndex + '__DiagnosisPointer4" name="ServiceLines[' + rowIndex + '].DiagnosisPointer4" type="text" value="">'+
//                        '</td>'+
//                        '<td>'+
//                            '<input class="form-control input-xs non_mandatory_field_halo" data-val="true" data-val-number="The field UnitCharges must be a number." data-val-required="The UnitCharges field is required." id="ServiceLines_' + rowIndex + '__UnitCharges" name="ServiceLines[' + rowIndex + '].UnitCharges" type="text" value="">'+
//                        '</td>'+
//                        '<td>'+
//                            '<input class="form-control input-xs non_mandatory_field_halo" data-val="true" data-val-number="The field Unit must be a number." data-val-required="The Unit field is required." id="ServiceLines_' + rowIndex + '__Unit" name="ServiceLines[' + rowIndex + '].Unit" type="text" value="">'+
//                        '</td>'+
//                        '<td>'+
//                            '<input class="form-control input-xs non_mandatory_field_halo" id="ServiceLines_' + rowIndex + '__UnitCharges" name="ServiceLines[' + rowIndex + '].UnitCharges" type="text" value="">'+
//                        '</td>'+
//                        '<td>'+
//                            '<input class="form-control input-xs non_mandatory_field_halo" id="ServiceLines_' + rowIndex + '__EMG" name="ServiceLines[' + rowIndex + '].EMG" type="text" value="">'+
//                        '</td>'+
//                        '<td>'+
//                            '<input class="form-control input-xs non_mandatory_field_halo" id="ServiceLines_' + rowIndex + '__EPSDT" name="ServiceLines[' + rowIndex + '].EPSDT" type="text" value="">'+
//                        '</td>'+
//                        '<td onclick="RemoveThisRow(this)"><i class="fa fa-close"></i></td>'+
//                    '</tr>';

//    $('#ServiceLineTbody').append(template);
//    rowIndex++;
//})


/** 
@description remove service line
 * @param {ele} row element to be removed
 */
$('#claimInformationForm').off('click', '.RemoveRow').on('click', '.RemoveRow', function () {
    var ele = $(this).closest("tr");
    var tbody = $(this).closest("tbody");
    var trIndex = findRemovedElementIndex(ele[0]);

    $(this).closest("tr").remove();
    SetColoumnNameForTable();
  
    //$(ele).remove();
    tbody.children().each(function (index) {
        if (index >= trIndex) {
            maintainIndexes(this,index);
        }
    });

})



/** 
@description navigation based on tab clicked
 */
$('#ClaimsInfoTab').show();
$(".tabs-menu a").click(function (event) {
    event.preventDefault();
    $(this).parent().addClass("current");
    $(this).parent().siblings().removeClass("current");
    var tab = $(this).attr("href");
    $('.claims_tab_container').find(".custommembertab-content").hide()
    $('.claims_tab_container').find(tab).css('display', 'inline-block');


});



/** 
@description navigation to create claims template page
 */
$('#GoToCreateClaimsTempBtn').on('click', function () {

    if (createClaimByType != 'Multiple claims for a Provider') {
        MakeItVisible(3, currentProgressBarData);
    } else {
        MakeItVisible(4, currentProgressBarData);
    }
});


//function RemoveCPTRows(ele) {
//    var tbody = $(ele).parent();
//    var trIndex = findCPTRemovedElementIndex(ele[0]);
//    $(ele).remove();
//    tbody.children().each(function (index) {
//        if (index >= trIndex) {
//            maintainIndexes(this);
//        }
//    });


//}
function findRemovedElementIndex(tr) {

    var trIndex;
    $(tr).parent().children().each(function (index) {
        if (this === tr) {
            trIndex = index;
        }

    });
    return trIndex;
}

function maintainIndexes(ele,index) {
    var inputs = $(ele).find('input');
    inputs.each(function () {
        var id = $(this).attr("id");
        var newId = id.replace(/[0-9]+/, index);
        var name = $(this).attr("name");
        var newName = name.replace(/[0-9]+/, index);
        $(this).attr("id", newId);
        $(this).attr("name", newName);

    });
    var spans = $(ele).find('span');
    spans.each(function () {
        var msg = $(this).attr("data-valmsg-for");
        if (typeof msg != 'undefined') {
            var newmsg = msg.replace(/[0-9]+/, index);
            $(this).attr("data-valmsg-for", newmsg);
        }
    });



}

