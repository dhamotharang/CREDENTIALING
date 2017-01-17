var validateForm = function (form) {

    form.removeData("validator").removeData("unobtrusiveValidation");
    $.validator.unobtrusive.parse(form);
    if (form.valid()) {
        return true;
    }
    return false;

};
$.validator.addMethod("checklengthrange", function (value, element, param) {
    return /^.{2,80}$/i.test(value);
}, "Length Should be more than 2 and less than 80.");
$.validator.unobtrusive.adapters.add("checklengthrange", null, function (options) {
    options.rules["checklengthrange"] = options.element;
    options.messages["checklengthrange"] = options.message;
});

/** 
@description Notification of claims successfully generated
 */
function GoToSelectMember() {
    
    TabManager.showLoadingSymbol("fullBodyContainer");
        $.ajax({
            type: 'POST',
            url: "/Billing/CreateClaim/SaveCMS1500Form",
            data: new FormData($('#cms1500Form')[0]),
            processData: false,
            contentType: false,
            dataType: "html",
            success: function (result) {
                new PNotify({
                    title: 'Claim Created Successfully',
                    text: 'Claim has been created successfully with Claim ID - P20160922C37',
                    type: 'success',
                    animate: {
                        animate: true,
                        in_class: "lightSpeedIn",
                        out_class: "slideOutRight"
                    }
                });


                if (createClaimByType == 'Multiple claims for a Provider' || createClaimByType == 'Multiple claims for a Member') {
                    MakeItVisibleWithDataFlush(3, currentProgressBarData);
                } else {
                    MakeItVisibleWithDataFlush(2, currentProgressBarData);
                }
            }
        });
    


}

//$('#SaveCMS1500Btn').on('click', GoToSelectMember);

/** 
@description Notification of claims successfully generated
 */
$('#SaveAndExitCMS1500Btn').on('click', function () {
    new PNotify({
        title: 'Claim Created Successfully',
        text: 'Claim has been created successfully with Claim ID - P20160922C37',
        type: 'success',
        animate: {
            animate: true,
            in_class: "lightSpeedIn",
            out_class: "slideOutRight"
        }
    });
});

var TableCount = 1;

/** 
@description Adding service line in cms1500 form
 */

//$('#AddNewRowInForm').click(function (e) {
//    e.preventDefault();
//    if (TableCount == 1) {
//        TableCount = 0;
//        $('#TableRowData').append('<td onclick="RemoveThisRow(this)"><i class="fa fa-close"></i></td>');
//    }

//    var trTag = '<tr>';
//    var trEndTag = '</tr>';
//    var trContent = '<tr> <td> <input class="form-control input-xs non_mandatory_field_halo" id="CurrentServiceFrom" name="CurrentServiceFrom" type="text" value=""> <span class="field-validation-valid" data-valmsg-for="CurrentServiceFrom" data-valmsg-replace="true"></span> </td> <td> <input class="form-control input-xs non_mandatory_field_halo" id="CurrentServiceTo" name="CurrentServiceTo" type="text" value=""> <span class="field-validation-valid" data-valmsg-for="CurrentServiceTo" data-valmsg-replace="true"></span> </td> <td> <input class="form-control input-xs non_mandatory_field_halo" id="PlaceOfService" name="PlaceOfService" type="text" value=""> <span class="field-validation-valid" data-valmsg-for="PlaceOfService" data-valmsg-replace="true"></span> </td> <td> <input class="form-control input-xs non_mandatory_field_halo" id="item_EMG" name="item.EMG" type="text" value=""> <span class="field-validation-valid" data-valmsg-for="item.EMG" data-valmsg-replace="true"></span> </td> <td> <input class="form-control input-xs non_mandatory_field_halo" id="item_claimsProcedure" name="item.claimsProcedure" type="text" value=""> <span class="field-validation-valid" data-valmsg-for="item.Modifier1" data-valmsg-replace="true"></span> </td> <td> <input class="form-control input-xs non_mandatory_field_halo" id="item_Modifier1" name="item.Modifier1" type="text" value=""> <span class="field-validation-valid" data-valmsg-for="item.Modifier1" data-valmsg-replace="true"></span> </td> <td> <input class="form-control input-xs non_mandatory_field_halo" id="item_Modifier2" name="item.Modifier2" type="text" value=""> <span class="field-validation-valid" data-valmsg-for="item.Modifier2" data-valmsg-replace="true"></span> </td> <td> <input class="form-control input-xs non_mandatory_field_halo" id="item_Modifier3" name="item.Modifier3" type="text" value=""> <span class="field-validation-valid" data-valmsg-for="item.Modifier3" data-valmsg-replace="true"></span> </td> <td> <input class="form-control input-xs non_mandatory_field_halo" id="item_Modifier4" name="item.Modifier4" type="text" value=""> <span class="field-validation-valid" data-valmsg-for="item.Modifier4" data-valmsg-replace="true"></span> </td> <td> <input class="form-control input-xs non_mandatory_field_halo" id="item_DiagnosisPointer1" name="item.DiagnosisPointer1" type="text" value=""> <span class="field-validation-valid" data-valmsg-for="item.DiagnosisPointer1" data-valmsg-replace="true"></span> </td> <td> <input class="form-control input-xs non_mandatory_field_halo" id="item_DiagnosisPointer2" name="item.DiagnosisPointer2" type="text" value=""> <span class="field-validation-valid" data-valmsg-for="item.DiagnosisPointer2" data-valmsg-replace="true"></span> </td> <td> <input class="form-control input-xs non_mandatory_field_halo" id="item_DiagnosisPointer3" name="item.DiagnosisPointer3" type="text" value=""> <span class="field-validation-valid" data-valmsg-for="item.DiagnosisPointer3" data-valmsg-replace="true"></span> </td> <td> <input class="form-control input-xs non_mandatory_field_halo" id="item_DiagnosisPointer4" name="item.DiagnosisPointer4" type="text" value=""> <span class="field-validation-valid" data-valmsg-for="item.DiagnosisPointer4" data-valmsg-replace="true"></span> </td> <td> <input class="form-control input-xs non_mandatory_field_halo" data-val="true" data-val-number="The field UnitCharges must be a number." data-val-required="The UnitCharges field is required." id="item_UnitCharges" name="item.UnitCharges" type="text" value=""> <span class="field-validation-valid" data-valmsg-for="item.UnitCharges" data-valmsg-replace="true"></span> </td> <td> <input class="form-control input-xs non_mandatory_field_halo" data-val="true" data-val-number="The field Unit must be a number." data-val-required="The Unit field is required." id="item_Unit" name="item.Unit" type="text" value=""> <span class="field-validation-valid" data-valmsg-for="item.Unit" data-valmsg-replace="true"></span> </td> <td> <input class="form-control input-xs non_mandatory_field_halo" id="item_UnitCharges" name="item.UnitCharges" type="text" value=""> <span class="field-validation-valid" data-valmsg-for="item.UnitCharges" data-valmsg-replace="true"></span> </td> <td> <input class="form-control input-xs non_mandatory_field_halo" id="item_EPSDT" name="item.EPSDT" type="text" value=""> <span class="field-validation-valid" data-valmsg-for="item.EPSDT" data-valmsg-replace="true"></span> </td> <td> NPI </td> <td> <input class="form-control input-xs non_mandatory_field_halo" id="RenderingProviderNPI" name="RenderingProviderNPI" type="text" value=""> <span class="field-validation-valid" data-valmsg-for="RenderingProviderNPI" data-valmsg-replace="true"></span> </td><td onclick="RemoveThisRow(this)"><i class="fa fa-close"></i></td> </tr>'

//    $('#DOStableBody').append(trTag + trContent + trEndTag);

//    $(".removeRow").trigger("click");
//})

$('#fullBodyContainer').off('click', '#AddNewRowInForm').on('click', '#AddNewRowInForm',function(){
    event.preventDefault();
    var tbody = $("form#cms1500Form").find('#DOStableBody');
    var lasttr = tbody[0].lastElementChild;
    var prevIndex = parseInt($(lasttr).find('td:eq(5)').find('input').attr('id').match(/\d+/)[0]);
    var templateIndex = ++prevIndex;
    $.ajax({
        type: 'GET',

        url: '/Billing/CreateClaim/CreateServiceLineInCms1500Form?index=' + templateIndex,
        success: function (data) {
            $('#DOStableBody').append(data);
        }
    });
})

$('#AddNewRowInForm').click(function (e) {})

/** 
@description Removing service line in cms1500 form
 */

function RemoveThisRow(ele) {
    $(ele).closest("tr").remove();
}

/** 
@description Adjusting height of each container in cms 1500 form
 */

//$('#cms1500Form').ready(function () {
//    $('.row').each(function () {
//        var current_row = $(this);
//        var row_height = current_row.height();
//        current_row.find('.cms_div').each(function () {
//            $(this).height(row_height);
//        });
//    });
//});

/** 
@description Navigating to claims info page
 */

$('#GoToClaimsInfoBtn').on('click', function () {
    if (createClaimByType != 'Multiple claims for a Provider') {
        MakeItVisible(4, currentProgressBarData);
    } else {
        MakeItVisible(5, currentProgressBarData);
    }
});



/** 
@description copying patient information to subscriber
 */

$('#CMS1500CopyPatient').on('click', function () {

    var flag = $('#CMS1500CopyPatient').is(':checked');
    if (flag) {
        $('#SubscriberLastOrOrganizationName').val($('[type=text][name="PatientLastOrOrganizationName"]').val());
        $('#SubscriberMiddleName').val($('[type=text][name="PatientMiddleName"]').val());
        $('#SubscriberFirstName').val($('[type=text][name="PatientFirstName"]').val());
    } else {
        $('#SubscriberLastOrOrganizationName').val('');
        $('#SubscriberMiddleName').val('');
        $('#SubscriberFirstName').val('');
    }

});


$('#fullBodyContainer').off('keyup', '.UnitCharges').on('keyup', '.UnitCharges', function () {

    var mul = [];
    $(this).closest("tr").find(".UnitCharges").each(function () {
        mul.push(this.value)
    });

    $(this).closest("tr").find(".UnitChargesValues").val((mul[0] * mul[1]).toFixed(2));

});




/*@description NDC show and hide
*/
$('#fullBodyContainer').off('change', 'input[name="IsNDC"]').on('change', 'input[name="IsNDC"]', function () {
    if (this.value === "show") {
        $('.NDCTr').show();
    } else { $('.NDCTr').hide(); }
})
