
(function ($) {

    $.validator.addMethod("mandatory", function (value, element, params) {
        return params;
    }, 'Please enter this field');

    $.validator.addMethod("DependentRequired", function (value, element, params) {
        if (params) {
            return element.value == '';
        } else {
            return element.value != '';
        }

    }, 'Please enter this field');

    $.validator.addMethod("DependentRequired3", function (value, element, params) {
        return params
    }, 'Please enter this field');

    $.validator.addMethod("DependentRequired2", function (value, element, params) {
        return params;
    }, 'Please enter this field');


    $.validator.addMethod("PatientInfoRequired", function (value, element, params) {
        return params;
    }, 'Please enter this field');

    $.validator.addMethod("GenderRequired", function (value, element, params) {

        return (value == 'M' || value == 'F' || value == 'U');


    }, 'Please enter this field');



    $.validator.addMethod("SSN_Or_EIN_Required", function (value, element, params) {

        return (value == 'EI' || value == 'SY');


    }, 'Please enter this field');



    
  
    $("#cms1500Form").validate({
        rules: {
            'CodedEncounter.Encounter.DateOf_From': 'required ValidateDateFormatHCFAForm notFutureDate',
         
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
            GoToSelectMember();
        }
    });


})(jQuery);