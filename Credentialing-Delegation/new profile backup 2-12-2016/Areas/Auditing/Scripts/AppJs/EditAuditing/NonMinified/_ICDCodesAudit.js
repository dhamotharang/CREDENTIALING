/** 
@description TO ADD THE NEW ROW IN THE TABLE.
 */
$('.IcdAuditMailPanel_Edit').off('click', '.AddNewRowInICDTable_EditAuditing').on('click', '.AddNewRowInICDTable_EditAuditing', function () {
    var tr = '<tr>' +
                    '<td><input class="form-control input-xs text-uppercase read_only_field" type="text"></td>' +
                    '<td><input type="text" class="form-control input-xs text-uppercase read_only_field"></td>' +
                    '<td><input type="text" class="form-control input-xs text-uppercase read_only_field"></td>' +
                    '<td><input type="text" class="form-control input-xs text-uppercase read_only_field"></td>' +
                    '<td><input type="text" class="form-control input-xs text-uppercase read_only_field"></td>' +
                    '<td><input type="text" class="form-control input-xs text-uppercase read_only_field"></td>' +
                    '<td><input type="text" class="form-control input-xs text-uppercase read_only_field"></td>' +
                    '<td><input type="checkbox" class="normal-checkbox c-individualCheckAcceptedClaims agreeCheck" checked /><label><span></span></label></td>' +
                    '<td>' +
                        '<input type="checkbox" class="normal-checkbox individualCheckAcceptedClaims disagreeCheck" /><label><span></span></label>' +
                    '</td>' +
                    '<td class="icdcategories">' +
                        '<input type="text" class="form-control input-xs text-uppercase read_only_field">' +
                    '</td>' +
                    '<td class="icdremarks">' +
                        '<textarea class="form-control input-xs non_mandatory_field_halo isAgree" placeholder="Remarks" disabled></textarea>' +
                    '</td>' +
                    '<td onclick="RemoveThisRow(this)"><button type="button" class="btn btn-xs btn-danger"><i class="fa fa fa-minus"></i></button></td>'+
    '</tr>'
    $('#ICDAudittbody_Edit').append(tr)
    $('select[multiple]').multipleSelect();
})


/** 
@description TO REMOVE THE NEWLY ADDED ROW.
 */
var RemoveThisRow = function (ele) {
    $(ele).parent("tr").remove();
}


///** 
//@description When User wants to Agree only single ICD Code.
// */
//$('.IcdAuditMailPanel_Edit').off('click', 'input[type="checkbox"].agreeCheck').on('click', 'input[type="checkbox"].agreeCheck', function () {
//    var currentElement = $(this);
//    if (currentElement.is(':checked')) {
//        currentElement.parent().parent().find('.disagreeCheck').checked = false;
//        currentElement.parent().parent().find('.isAgree').find('.ms-choice').addClass('disabled');
//    }
//    else
//    {
//        currentElement.parent().parent().find('.disagreeCheck').checked = true;
//        currentElement.checked = false;
//        currentElement.parent().parent().find('.isAgree').find('.ms-choice').removeClass('disabled');
//    }
//})


///** 
//@description When User wants to Disagree only single ICD Code.
// */
//$('.IcdAuditMailPanel_Edit').off('click', 'input[type="checkbox"].disagreeCheck').on('click', 'input[type="checkbox"].disagreeCheck', function () {
//    var currentElement = $(this);
//    if (currentElement.is(':checked')) {
//        currentElement.parent().parent().find('.agreeCheck').checked = false;
//        currentElement.parent().parent().find('.isAgree').find('.ms-choice').removeClass('disabled');
//    }
//    else {
//        currentElement.parent().parent().find('.agreeCheck').checked = true;
//        currentElement.checked = false;
//        currentElement.parent().parent().find('.isAgree').find('.ms-choice').addClass('disabled');
//    }
//})


///** 
//@description When User wants to Agree all ICD Codes.
// */
//$('.IcdAuditMailPanel_Edit').off('click', 'input[type="checkbox"].EditAgreeICD').on('click', 'input[type="checkbox"].EditAgreeICD', function () {
//    var currentelement = $(this);
//    if (currentElement.is(':checked')) {
//        currentelement.parent().parent().parent().parent().find('.agreeCheck').each(function () {
//            this.checked = true;
//        })
//        currentelement.parent().parent().parent().parent().find('.disagreeCheck').each(function () {
//            this.checked = false;
//        })
//        currentElement.parent().parent().find('.isAgree').find('.ms-choice').addClass('disabled');
//        currentelement.checked = true;
//        currentelement.parent().parent().parent().parent().find('.EditDisagreeICD').checked = false;
//    }
//    else
//    {
//        currentelement.parent().parent().parent().parent().find('.agreeCheck').each(function () {
//            this.checked = false;
//        })
//        currentelement.parent().parent().parent().parent().find('.disagreeCheck').each(function () {
//            this.checked = true;
//        })
//        currentElement.parent().parent().find('.isAgree').find('.ms-choice').removeClass('disabled');

//        currentelement.checked = false;
//        currentelement.parent().parent().parent().parent().find('.EditDisagreeICD').checked = true;
//    } 
//})


///** 
//@description When User wants to Disagree all ICD Codes.
// */
//$('.IcdAuditMailPanel_Edit').off('click', 'input[type="checkbox"].EditDisagreeICD').on('click', 'input[type="checkbox"].EditDisagreeICD', function () {
//    var currentelement = $(this);
//    if (currentElement.is(':checked')) {
//        currentelement.parent().parent().parent().parent().find('.agreeCheck').each(function () {
//            this.checked = false;
//        })
//        currentelement.parent().parent().parent().parent().find('.disagreeCheck').each(function () {
//            this.checked = true;
//        })
//        currentElement.parent().parent().find('.isAgree').find('.ms-choice').removeClass('disabled');

//        currentelement.checked = false;
//        currentelement.parent().parent().parent().parent().find('.EditDisagreeICD').checked = true;
//    }
//    else {
//        currentelement.parent().parent().parent().parent().find('.agreeCheck').each(function () {
//            this.checked = true;
//        })
//        currentelement.parent().parent().parent().parent().find('.disagreeCheck').each(function () {
//            this.checked = false;
//        })
//        currentElement.parent().parent().find('.isAgree').find('.ms-choice').addClass('disabled');
//        currentelement.checked = true;
//        currentelement.parent().parent().parent().parent().find('.EditDisagreeICD').checked = false;
//    }
//})


///** 
//@description Displays the 'n' number of Remarks textarea for the selection of 'n' number of Categories.
// */
//$('#ICDAudittbody_Edit').off('click', '.isAgree').on('click', '.isAgree', function () {
//    var len = $(this).find('.selected').length;
//    var data = [];
//    var textArea = "";

//    var currentElement = $(this);
//    var $el = currentElement.parent().parent().find('.isAgree');

//    $el.find('option:selected').each(function () {
//        data.push({ value: $(this).val(), ele: $(this).text() });
//    });
//    if (data.length === 0) {
//        textArea = '<textarea class="form-control input-xs non_mandatory_field_halo isAgree cptremarks" placeholder="Remarks"></textarea>'
//    }
//    else {
//        for (var i in data) {
//            textArea = textArea + '<label>' + data[i].ele + '</label>' + '<textarea class="form-control input-xs non_mandatory_field_halo isAgree cptremarks" placeholder="' + data[i].ele + '"></textarea>';
//        }
//    }
//    $(this).closest('td').next('td').html(textArea)
//});

$('.IcdAuditMailPanel_Edit').off('change', "input[name*='IsAgree']").on('change', "input[name*='IsAgree']", function () {
    if ($(this).val() === "False") {
        showModal('icdcategorymodal')
    }
});

$('#icdcategorymodal').off('click', '.enableRemarks-Icd').on('click', '.enableRemarks-Icd', function () {
    if ($(this)[0].checked) {
        $(this).parent().parent().find('.icdremarkscolumn').append('<textarea class="form-control input-xs non_mandatory_field_halo icdRemarks"></textarea>');
    }
    else {
        $(this).parent().parent().find('.icdremarkscolumn').html('');
    }
});


var IcdCatgRemaDetailedbiew=function(index){
    showModal('viewicdcategorymodal')
}

//$('input[name=ICDCodes[0].IsAgree]').click(function () {
//    if ($('input[name=ICDCodes[0].IsAgree]:checked').val() == "Yes")
//    {
//        console.log("checked");
//    }
//    else
//    {
//        console.log("notchecked");
//    }
//});
