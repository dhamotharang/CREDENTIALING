/*ADD NEW ROW FOR CPT CODE SELECTION*/
$('#PreAuthCPTArea').off('click', '.cpt_add_btn').on('click', '.cpt_add_btn', function () {
    var $row = $(this).parents('.PreAuthCPTRow');
    var childCount = parseInt($row.parent().children().length);
    var rowIndex = parseInt($row.index());
    $(this).addClass('hidden')
    $('.cpt_delete_btn').removeClass("hidden");
    $.ajax({
        type: 'post',
        url: '/PriorAuthAddRow/AddNewRow',
        data: { index: rowIndex + 1, url: "~/Areas/Portal/Views/PriorAuth/PriorAuthForm/Common/_CPTRow" + PortalRequestObject.currentCPT + ".cshtml" },
        cache: false,
        error: function () {
        },
        success: function (data, textStatus, XMLHttpRequest) {
            CPTRow = data;
            if (childCount == rowIndex) {
                $row.parent().append(CPTRow);
            }
            else {
                $(CPTRow).insertAfter($row);
            }
        }
    });


});
/*DELETE ADDED ROW FOR CPT CODE SELECTED*/
$('#PreAuthCPTArea').off('click', '.cpt_delete_btn').on('click', '.cpt_delete_btn', function () {
    var v = $(this).parents('.PreAuthCPTRow').nextAll();
    var $row = $(this).parents('.PreAuthCPTRow').remove();
    UpdateMapping(v);
    setAddDeleteButtonsVisibility(".cpt_add_btn", ".cpt_delete_btn", "PreAuthCPTArea");
});
//-----------------------------------------------------------------------------------------------------------//
/*ADD NEW ROW FOR CPT CODE SELECTION for----HomeHealth,RehabFacility,Dialysis,Therapy*/
$('#PreAuthCPTArea2').off('click', '.cpt_add_btn').on('click', '.cpt_add_btn', function () {
    var $row = $(this).parents('.PreAuthCPTRowSecondary');
    var childCount = parseInt($row.parent().children().length);
    var rowIndex = parseInt($row.index());
    $(this).addClass('hidden')
    $('.cpt_delete_btn').removeClass("hidden");
    var CPTRow = '<div class="col-lg-12 PreAuthCPTRowSecondary">' +
                                '<div class="col-lg-1 col-md-1">' +
                                    '<input class="form-control input-xs mandatory_field_halo" placeholder="CPT CODE" name="CPTCodes[' + (rowIndex + 1) + '].CPTCode">' +
                                '</div>' +
                                '<div class="col-lg-1 col-md-1">' +
                                    '<input class="form-control input-xs" placeholder="MODIFIER" name="CPTCodes[' + (rowIndex + 1) + '].Modifier">' +
                                '</div>' +
                                '<div class="col-lg-3 col-md-3">' +
                                    '<input class="form-control input-xs mandatory_field_halo" placeholder="DESCRIPTION" name="CPTCodes[' + (rowIndex + 1) + '].CPTDesc">' +
                                '</div>' +
                                '<div class="col-lg-1 col-md-1">' +
                                    '<select class="form-control input-xs" name="CPTCodes[' + (rowIndex + 1) + '].Discipline"><option value="">SELECT</option></select>' +
                                '</div>' +
                                '<div class="col-lg-1 col-md-1">' +
                                    '<input class="form-control input-xs" placeholder="REQ UNITS" name="CPTCodes[' + (rowIndex + 1) + '].RequestedUnits">' +
                                '</div>' +
                                '<div class="col-lg-1 col-md-1">' +
                                    '<select class="form-control input-xs" name="CPTCodes[' + (rowIndex + 1) + '].Range1"><option value="">SELECT</option></select>' +
                                '</div>' +
                                '<div class="col-lg-1 col-md-1">' +
                                    '<input class="form-control input-xs" placeholder="NO PER" name="CPTCodes[' + (rowIndex + 1) + '].NumberPer">' +
                                '</div>' +
                                '<div class="col-lg-1 col-md-1">' +
                                    '<select class="form-control input-xs" name="CPTCodes[' + (rowIndex + 1) + '].Range2"><option value="">SELECT</option></select>' +
                                '</div>' +
                                '<div class="col-lg-1 col-md-1">' +
                                    '<input class="form-control input-xs" placeholder="TOTAL UNITS" name="CPTCodes[' + (rowIndex + 1) + '].TotalUnits">' +
                                '</div>' +
                                '<div class="col-lg-1 col-md-1 button-styles-xs">' +
                                    '<button type="button" class="btn btn-danger btn-xs cpt_delete_btn"><i class="fa fa-minus"></i></button>' +
                                    '<button type="button" class="btn btn-success btn-xs cpt_add_btn"><i class="fa fa-plus"></i></button>' +
                                '</div>' +
                            '</div>'

    if (childCount == rowIndex) {
        $row.parent().append(CPTRow);
    }
    else {
        $(CPTRow).insertAfter($row);
    }
});
/*DELETE ADDED ROW FOR CPT CODE SELECTED for----HomeHealth,RehabFacility,Dialysis,Therapy*/
$('#PreAuthCPTArea2').off('click', '.cpt_delete_btn').on('click', '.cpt_delete_btn', function () {
    var v = $(this).parents('.PreAuthCPTRowSecondary').nextAll();//send all elements after Deleted element
    $(this).parents('.PreAuthCPTRowSecondary').remove();
    UpdateMapping(v);//Updating the Mappings
    setAddDeleteButtonsVisibility(".cpt_add_btn", ".cpt_delete_btn", "PreAuthCPTArea2");//send Add button style and Delete Button Style For ADD and Minus button is visibility
});
//-----------------------------------------------------------------------------------------------------------//