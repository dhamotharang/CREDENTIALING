/** 
@description TO ADD THE NEW ROW IN THE TABLE.
 */
$('.CPTCodeAuditMainpanel_Edit').off('click', '.AddNewRowCPTTable_EditAudit').on('click', '.AddNewRowCPTTable_EditAudit', function () {
    var tr = '<tr>' +
                    '<td><input type="text" class="form-control input-xs"></td>' +
                    '<td><input type="text" class="form-control input-xs"></td>' +
                   '<td>' +
                                                        '<input class="form-control input-xs text-uppercase modifier_width small_text" placeholder="" type="text" value="">' +
                                                        '<input class="form-control input-xs text-uppercase modifier_width small_text" placeholder="" type="text" value="">' +
                                                        '<input class="form-control input-xs text-uppercase modifier_width small_text" placeholder="" type="text" value="">' +
                                                        '<input class="form-control input-xs text-uppercase modifier_width small_text" placeholder="" type="text" value="">' +
                                                    '</td>' +
                    '<td>' +
                                                        '<input class="form-control input-xs text-uppercase modifier_width small_text"  placeholder="" type="text" value="">' +
                                                        '<input class="form-control input-xs text-uppercase modifier_width small_text"  placeholder="" type="text" value="">' +
                                                        '<input class="form-control input-xs text-uppercase modifier_width small_text"  placeholder="" type="text" value="">' +
                                                        '<input class="form-control input-xs text-uppercase modifier_width small_text"  placeholder="" type="text" value="">' +
                                                    '</td>' +
                    '<td><input type="text" class="form-control input-xs"></td>' +
                    '<td><input type="radio" name="checkBoxRadioAdd" class="checkbox-radio c-individualCheckAcceptedClaims agreeCheck" / checked><label><span></span></label></td>' +
                    '<td>' +
                        '<input type="radio" name="checkBoxRadioAdd" class="checkbox-radio individualCheckAcceptedClaims disagreeCheck ">' +
                        '<label><span></span></label>' +
                    '</td>' +
                    '<td class="cptcategories">' +
                  '<input type="text" class="form-control input-xs text-uppercase read_only_field">' +
'</td>' +
'<td class="cptremarks">' +
' <textarea class="form-control input-xs non_mandatory_field_halo isAgree " placeholder="Remarks" disabled></textarea>' +
'</td>' +
'<td onclick="RemoveThisRow(this)"><button type="button" class="btn btn-xs btn-danger"><i class="fa fa fa-minus"></i></button></td>'+
    '</tr>'
    $('#AuditCptcodebody_Edit').append(tr)
    $('select[multiple]').multipleSelect();
})


/** 
@description TO REMOVE THE NEWLY ADDED ROW.
 */
var RemoveThisRow = function (ele) {
    $(ele).parent("tr").remove();
}
var addNewCPT = function () {
    var formData = new FormData();

    $("form#savedetailsForm input[name*='CPTCodes'").each(function () {
        var input = $(this); // This is the jquery object of the input, do what you will
        var name = input.attr('name');
        var value = input.val();
        formData.append(name, value);
    });

    $.ajax({
        type: 'POST',
        url: '/Coding/Coding/AddCPTCodeView',
        data: formData,
        contentType: false,
        processData: false,
        success: function (data) {
            $('#cptcodedetails').html(data);
        }
    });


}
var deleteCPT = function (index) {
    var formData = new FormData();
    $("form#savedetailsForm input[name*='CPTCodes'").each(function () {
        var input = $(this); // This is the jquery object of the input, do what you will
        var name = input.attr('name');
        var value = input.val();
        formData.append(name, value);
    });
    formData.append('index', index);
    $.ajax({
        type: 'POST',
        url: '/Coding/Coding/DeleteCPTCodeView',
        data: formData,
        contentType: false,
        processData: false,
        success: function (data) {
            $('#cptcodedetails').html(data);
        }
    });
}

///** 
//@description Appends the selected number of Categories and Remarks to CPT.
// */
//$('#AuditCptcodebody_Edit').off('click', '.isAgree').on('click', '.isAgree', function () {
//    var len = $(this).find('.selected').length;
//    var data = [];
//    var textArea = "";
//    var currentElement = $(this);
//    var $el = currentElement.parent().parent().find('.isAgree');
//    $el.find('option:selected').each(function () {
//        data.push({ value: $(this).val(), ele: $(this).text() });
//    });
//    if (data.length === 0) {
//        textArea = '<textarea class="form-control input-xs non_mandatory_field_halo isAgree icdremarks" placeholder="Remarks"></textarea>'
//    }
//    else {
//        for (var i in data) {
//            textArea = textArea + '<span class="Remarks-icd"><label>' + data[i].ele + '</label>' + '<textarea class="form-control input-xs non_mandatory_field_halo isAgree icdremarks" placeholder="' + data[i].ele + '"></textarea></span>';
//        }
//    }
//    $(this).closest('td').next('td').html(textArea)
//});