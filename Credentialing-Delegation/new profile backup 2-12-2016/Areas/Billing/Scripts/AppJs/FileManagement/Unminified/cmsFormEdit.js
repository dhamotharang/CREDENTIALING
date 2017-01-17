InitICheckFinal();

/** @description Navigate to claim List.
 */
$('.backToClaimList').click(function () {
    $('#claimInfo').hide();
    $('#claimInfo').html('');
    $('#claimListTable').show();
})
/** @description adding new row for DOS.
 */
$('#AddNewRowForDOS').click(function (e) {
    e.preventDefault();
    template = '<tr> <td> <input class="form-control input-xs non_mandatory_field_halo" id="CurrentServiceFrom" name="CurrentServiceFrom" type="text" value=""> <span class="field-validation-valid" data-valmsg-for="CurrentServiceFrom" data-valmsg-replace="true"></span> </td> <td> <input class="form-control input-xs non_mandatory_field_halo" id="CurrentServiceTo" name="CurrentServiceTo" type="text" value=""> <span class="field-validation-valid" data-valmsg-for="CurrentServiceTo" data-valmsg-replace="true"></span> </td> <td> <input class="form-control input-xs non_mandatory_field_halo" id="PlaceOfService" name="PlaceOfService" type="text" value=""> <span class="field-validation-valid" data-valmsg-for="PlaceOfService" data-valmsg-replace="true"></span> </td> <td> <input class="form-control input-xs non_mandatory_field_halo" id="item_EMG" name="item.EMG" type="text" value=""> <span class="field-validation-valid" data-valmsg-for="item.EMG" data-valmsg-replace="true"></span> </td> <td> <input class="form-control input-xs non_mandatory_field_halo" id="item_claimsProcedure" name="item.claimsProcedure" type="text" value=""> <span class="field-validation-valid" data-valmsg-for="item.Modifier1" data-valmsg-replace="true"></span> </td> <td> <input class="form-control input-xs non_mandatory_field_halo" id="item_Modifier1" name="item.Modifier1" type="text" value=""> <span class="field-validation-valid" data-valmsg-for="item.Modifier1" data-valmsg-replace="true"></span> </td> <td> <input class="form-control input-xs non_mandatory_field_halo" id="item_Modifier2" name="item.Modifier2" type="text" value=""> <span class="field-validation-valid" data-valmsg-for="item.Modifier2" data-valmsg-replace="true"></span> </td> <td> <input class="form-control input-xs non_mandatory_field_halo" id="item_Modifier3" name="item.Modifier3" type="text" value=""> <span class="field-validation-valid" data-valmsg-for="item.Modifier3" data-valmsg-replace="true"></span> </td> <td> <input class="form-control input-xs non_mandatory_field_halo" id="item_Modifier4" name="item.Modifier4" type="text" value=""> <span class="field-validation-valid" data-valmsg-for="item.Modifier4" data-valmsg-replace="true"></span> </td> <td> <input class="form-control input-xs non_mandatory_field_halo" id="item_DiagnosisPointer1" name="item.DiagnosisPointer1" type="text" value=""> <span class="field-validation-valid" data-valmsg-for="item.DiagnosisPointer1" data-valmsg-replace="true"></span> </td> <td> <input class="form-control input-xs non_mandatory_field_halo" id="item_DiagnosisPointer2" name="item.DiagnosisPointer2" type="text" value=""> <span class="field-validation-valid" data-valmsg-for="item.DiagnosisPointer2" data-valmsg-replace="true"></span> </td> <td> <input class="form-control input-xs non_mandatory_field_halo" id="item_DiagnosisPointer3" name="item.DiagnosisPointer3" type="text" value=""> <span class="field-validation-valid" data-valmsg-for="item.DiagnosisPointer3" data-valmsg-replace="true"></span> </td> <td> <input class="form-control input-xs non_mandatory_field_halo" id="item_DiagnosisPointer4" name="item.DiagnosisPointer4" type="text" value=""> <span class="field-validation-valid" data-valmsg-for="item.DiagnosisPointer4" data-valmsg-replace="true"></span> </td> <td> <input class="form-control input-xs non_mandatory_field_halo" data-val="true" data-val-number="The field UnitCharges must be a number." data-val-required="The UnitCharges field is required." id="item_UnitCharges" name="item.UnitCharges" type="text" value=""> <span class="field-validation-valid" data-valmsg-for="item.UnitCharges" data-valmsg-replace="true"></span> </td> <td> <input class="form-control input-xs non_mandatory_field_halo" data-val="true" data-val-number="The field Unit must be a number." data-val-required="The Unit field is required." id="item_Unit" name="item.Unit" type="text" value=""> <span class="field-validation-valid" data-valmsg-for="item.Unit" data-valmsg-replace="true"></span> </td> <td> <input class="form-control input-xs non_mandatory_field_halo" id="item_UnitCharges" name="item.UnitCharges" type="text" value=""> <span class="field-validation-valid" data-valmsg-for="item.UnitCharges" data-valmsg-replace="true"></span> </td> <td> <input class="form-control input-xs non_mandatory_field_halo" id="item_EPSDT" name="item.EPSDT" type="text" value=""> <span class="field-validation-valid" data-valmsg-for="item.EPSDT" data-valmsg-replace="true"></span> </td> <td> NPI </td> <td> <input class="form-control input-xs non_mandatory_field_halo" id="RenderingProviderNPI" name="RenderingProviderNPI" type="text" value=""> <span class="field-validation-valid" data-valmsg-for="RenderingProviderNPI" data-valmsg-replace="true"></span> </td> </tr>'
    $('#DOSTablePart').append(template);    
})
/** @description Adjusting Height of the containers in CMS1500 forms.
 */
$('.row').each(function () {
    var current_row = $(this);
    var row_height = current_row.height();
    current_row.find('.cms_div').each(function () {
        $(this).height(row_height);
    });
});
/** @description Navigating to next/previous Claim.
 */
$('#claimInfo').off('click', '.nextCumPrevious').on('click', '.nextCumPrevious', function () {
    $('#claimInfo').slideToggle(300);
    $('#claimInfo').slideToggle(300);
})