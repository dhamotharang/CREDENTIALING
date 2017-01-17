var getICDdata = false;
var getMedicalReview = false;
var getCPTCodeHistory = false;


/** 
@description Getting medical reviewed ICD codes
 */
$('#viewMedicalReview').click(function () {
    event.preventDefault();
    if (!getMedicalReview) {
        $('#MedicalReviewICDCodespanel').load('/Billing/CreateClaim/GetMedicalReview')
        showModal('selectMedicalReviewCodes')
        getMedicalReview = true;
    }
    else {
        showModal('selectMedicalReviewCodes')
    }
})

$('#fullBodyContainer').off('keyup', '.UnitChargesInform').on('keyup', '.UnitChargesInform', function () {

    var mul = [];
    $(this).closest("tr").find(".UnitChargesInform").each(function () {
        mul.push(this.value)
    });

    $(this).closest("tr").find(".UnitChargesValuesInform").val((mul[0] * mul[1]).toFixed(2));

});

$('#viewCptCodesHistory').click(function () {
    event.preventDefault();
    if (!getCPTCodeHistory) {
        $('#CPTCodesHistorypanel').load('/Billing/CreateClaim/GetCPTCodeHistory')
        TabManager.loadOrReloadScriptsUsingHtml();
        showModal('SelectCPTCodesFromHistory')
        getCPTCodeHistory = true;
    }
    else {
        showModal('SelectCPTCodesFromHistory')
    }
});

var ICDHistoryList = [];


$('#SelectCPTCodesFromHistory').off('click', '#CPTCodeDescDiv').on('click', '#CPTCodeDescDiv', function () {
    $(this).find('.cpt-span').addClass('fa fa-check-circle-o CPTselectspan');
});


//////////////////////////////////////Active Diagnosis Codes////////////////////////////////////////////
/** 
@description Getting ICD history
 */
var icdindexes = [];
var icdcodevalues = [];
$('#viewICDHistory').click(function () {
    event.preventDefault();
    if (!getICDdata) {
        $('#MemberActiveICDCodespanel').load('/Billing/CreateClaim/GetIcdHistory')
        showModal('selectMemberActiveCodes')
        getICDdata = true;
    }
    else {
        showModal('selectMemberActiveCodes')
    }
    var index = 0;
    var codeindex = 0;
    for (var i = 0; i <= 11; i++) {
        if (($(".dignosis_line input[name='CodedEncounter.Diagnosis[" + i + "].ICDCode'").val() === "")) {
            icdindexes[index] = i;
            index++;
        }
        else {
            icdcodevalues[codeindex] = $(".dignosis_line input[name='CodedEncounter.Diagnosis[" + i + "].ICDCode'").val();
            codeindex++;
        }
    }
})
//click on each code
//$('#selectMemberActiveCodes').off('click', '.selectActiveICDCode').on('click', '.selectActiveICDCode', function () {
//    var maximunpossibleselection = 12 - icdindexes.length;
//    $(this).each(function () {
//        if (this.checked) {
//            selectedICDHistoryCodes.push(this.value);
//        }
//    });
//});

//save codes
$('#selectMemberActiveCodes').off('click', '#appendICDfromHistory').on('click', '#appendICDfromHistory', function () {
    var selectedICDHistoryCodes = [];
    $(this).parent().parent().find('.selectActiveICDCode').each(function () {
        if (this.checked) {
            selectedICDHistoryCodes.push(this.value);
        }
    });
    var icdfieldindex = 0;
    for (var i = 0; i < selectedICDHistoryCodes.length; i++) {
        var IsPresent = false;
        for (j = 0; j < icdcodevalues.length; j++) {
            if (selectedICDHistoryCodes[i].toLowerCase() === icdcodevalues[j].toLowerCase()) {
                IsPresent = true;
            }
        }
        if (IsPresent === false) {
            $(".dignosis_line input[name='CodedEncounter.Diagnosis[" + icdindexes[icdfieldindex] + "].ICDCode'").val(selectedICDHistoryCodes[i]);
            icdfieldindex++;
        }
    }
});
//////////////////////////////////////Active Diagnosis Codes////////////////////////////////////////////



//$('.ClaimsInformationPanel').off('change', '.Dateofserviceto').on('change', '.Dateofserviceto', function () {
//    $(this).val();
//    alert();
//    console.log('ffhjkljo');
//});


var setICDIndicator = function () {
    //var ICDIndicator="icd10";
    var DOSFrom = $('.DateofserviceFrom').val();
    var DOSTo = $('.DateofserviceTo').val();
    var ICDDate = '10/01/2015';
    //var formatedDate = new Date(ICDDate);
    if (DOSTo >= ICDDate) {
        //ICDIndicator = "icd10";
    }
    else
    {
        //ICDIndicator = "icd9";
    }
}