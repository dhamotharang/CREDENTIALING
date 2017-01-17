$(function () {
    $("#IndividualTaxIdType").change(function () { 
        $("#IndividualTaxIdType").prop("checked") ? $("#IndividualTaxIDView").show() : $("#IndividualTaxIDView").hide();
    });
    $("#GroupTaxIdType").change(function () { $("#GroupTaxIdType").prop("checked") ? $("#GroupTaxIDView").show() : $("#GroupTaxIDView").hide(); });
    $("#PhysicianGroupName").change(function () { $("#GroupContactName")[0].placeholder = "Jessica H" })
    $(".planMutiple").select2({
        tags: true,
        placeholder: 'PLAN NAME',
        allowClear: true
    });

    $(".groupContact").select2({
        tags: true,
        placeholder: 'GROUP CONTACT NAME',
        allowClear: true
    });
    $(".FacilityMultiple").select2({
        tags: true,
        placeholder: 'FACILITY',
        allowClear: true
    });
    $(".SpecialtyMultiple").select2({
        tags: true,
        placeholder: 'SPECIALTY',
        allowClear: true
    });

    $('#kyunBhai').off('click', '.hola').on('click', '.hola', function () {
        var intId = $("#GroupData div").length + 1;
        var fieldWrapper = $("<div class=\"fieldwrapper row col-lg-12 col-md-12 \"style=\"margin-left: 0px; margin-bottom: 6px;\" id=\"field" + intId + "\"/>");
        var fName = $("<input type=\"text\" class=\"fieldname\" />");
        var GroupNameDiv = $("<div  class=\"col-md-2 col-lg-2 col-sm-2 zero-padding-left-right\" />");
        var PlanNameDiv = $("<div  class=\"col-md-5 col-lg-5 col-sm-5 zero-padding-left-right\" />");
        var GroupTaxIDDiv = $("<div  class=\"col-md-2 col-lg-2 col-sm-2 zero-padding-left-right\" />");
        var GroupContactNameDiv = $("<div  class=\"col-md-3 col-lg-3 col-sm-3 zero-padding-left-right\" />");
        //var RemoveDiv = $("<div class=\"col-md-1 col-lg-1 col-sm-1\" />");

        var GroupName = $("<input type=\"text\" class=\"form-control input-sm Add_Provider\" placeholder=\"GROUP NAME\" />");
        var PlanName = $("<select style=\"width: 100%; \" id=\"planInput\" class=\"form-control  text-uppercase planMutiple marginTop-7\" multiple=\"multiple\"<option>ULTIMATE</option><option>WELLCARE</option><option>HUMANA</option><option>FREEDOM</option></select>");
        var GroupTaxID = $("<input type=\"text\" class=\"form-control input-sm Add_Provider\" placeholder=\"TAX ID\" />");
        var GroupContactName = $("<span class=\"col-md-10 col-lg-10 col-sm-10 zero-padding-left-right\"><input type=\"text\" class=\"form-control input-sm Add_Provider\" placeholder=\"GROUP CONTACT NAME\" /></span>");
        var RemoveButton = $("<span class=\"col-md-2 col-lg-2 col-sm-2 zero-padding-left-right\"><button style=\"padding:0px;\" class=\"btn btn-xs btn-danger\"><i class=\"fa fa-minus hola\" style=\"color:white; margin: 6px;cursor:pointer\"></i></button></span>")

        var fType = $("<select class=\"fieldtype\"><option value=\"checkbox\">Checked</option><option value=\"textbox\">Text</option><option value=\"textarea\">Paragraph</option></select>");
        //var removeButton = $("<input type=\"button\" class=\"remove\" value=\"-\" />");

        RemoveButton.click(function () {
         $(this).parent().parent().remove();
        });
        GroupNameDiv.append(GroupName)
        PlanNameDiv.append(PlanName)
        GroupTaxIDDiv.append(GroupTaxID)
        GroupContactNameDiv.append(GroupContactName)
        GroupContactNameDiv.append(RemoveButton)

        fieldWrapper.append(GroupNameDiv);
        fieldWrapper.append(PlanNameDiv);
        fieldWrapper.append(GroupTaxIDDiv);
        fieldWrapper.append(GroupContactNameDiv);
        //fieldWrapper.append(RemoveDiv);
        //fieldWrapper.append();
        $(function () {
            $(".planMutiple").select2({
                tags: true,
                placeholder: 'PLAN NAME',
                allowClear: true
            })
        });
        $("#GroupData").append(fieldWrapper);
    });

    //$('#PhysicianGroupName').select2({
    //    data:["Access Health Care Physician LLC","Prime Care","Physician LLC","All American Physicians"],
    //    tags: true,
    //    tokenSeparators: [','],
    //    placeholder: ""
    //});

    //Add new Provider

    //function AddProvider(form_id) {
    //    var $form = $("#" + form_id).find("form");
    //    ResetFormForValidation($form);
    //    if ($form.valid()) {
    //        $.ajax({
    //            url: rootDir + '/Portal/AddProvider/AddNewProvider',
    //            type: 'POST',
    //            data: new FormData($form[0]),
    //            async: false,
    //            cache: false,
    //            contentType: false,
    //            processData: false,
    //            success: function (data) {

    //            }
    //        });
    //    } else {

    //    }
    //}
})