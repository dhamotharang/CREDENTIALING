$(document).ready(function () {
    //-------------------TO enable or Disable Row in Neg fee pop up--------------------------//
    $("#NEG_FEE_MODAL").off('click', '.NegFeeCheckbox').on('click', '.NegFeeCheckbox', function () {
        var parentId = $(this).parent().parent().parent().attr("id");
        //----- If checkbox value is true enabling the textbox and buttons--------------//
        if ($(this).is(':checked')) {
            $('#' + parentId).find('.Disable_Enable').removeAttr("disabled");
        }
            //----- If checkbox value is false disabling the textbox and buttons--------------//
        else {
            // if check box value is false get  the parent parent id of that div
            var SuperParent = $(this).parent().parent().parent().attr("id");
            //Send the parent parent id to resetAllValuesForParticularAreaWhenCheckboxIsUnchecked which will in turn clear the values of all the text fields inside the that id.
            resetAllValuesForParticularAreaWhenCheckboxIsUnchecked(SuperParent);
            $('#' + parentId).find('.Disable_Enable').attr("disabled", "disabled");
        }
    });
    //---------------------------------------------------Upadte id and name when a row is removed.----------------------------------//
    //Update the parent parent id and the id of the particular field and name when a row is removed by clicking -.
    function UpdateMapping(element, rowIndex) {
        $(element).each(function (ind, ele) {
            //Update parent parent id .
            UpdateID(element, ind)
            $(this).find('input,select').each(function (i, e) {
                //Update particular name and id.
                var OldId = $(this).attr('id').split('___');
                var NewIndex = parseInt(OldId[1]) - 1;
                var NewID = OldId[0] + "___" + NewIndex;
                $(this).attr('id', NewID);
                var oldStringName = $(this).attr('name');
                var FirstPartOfString = oldStringName.substring(0, oldStringName.lastIndexOf("[") + 1);
                var SecondPartOfString = oldStringName.replace(FirstPartOfString, "");
                var NameIndexWithName = FirstPartOfString + (parseInt(SecondPartOfString.charAt(0)) - 1).toString() + SecondPartOfString.substring(1)
                $(this).attr('name', NameIndexWithName);
            })
        })
    }
    //Update parent parent id when a row is removed .
    function UpdateID(element, index) {
        var OldId = $(element[index]).attr('id').split('___');
        var NewIndex = parseInt(OldId[1]) - 1;
        var NewID = OldId[0] + "___" + NewIndex;
        $(element[index]).attr('id', NewID);
    }
    //--------------------------------------------------- End of Upadte id and name when a row is removed.----------------------------------//
    //---Setting Add and Delete Button ----//
    //-------------------Add or delete new row in NegFee Pop up modal------------------------//
    $("#NEG_FEE_MODAL").off('click', '.NegFeePlusButton').on('click', '.NegFeePlusButton', function () {
        var RowId = $(this).parent().attr('id');//getting parent row
        var len = $('#' + RowId + 'Area').find('.' + RowId + 'Row').length;//total Div in parent id
        AddTemplate(len, RowId);
        SetButtonHideView(RowId);
    });
    $("#NEG_FEE_MODAL").off('click', '.NegFeeMinusButton').on('click', '.NegFeeMinusButton', function () {
        //Get the parent parent row id so that we can deduct the calculated money for that row
        var ParentRowID = $(this).parent().parent().attr('id');
        //Id for Total Cost for row text field
        var TotalCostID = $('#' + ParentRowID).children().find('#TOTALCOST' + ParentRowID)[0].id
        //Value for Total Cost for row text field
        //Pass parent id of the row to method which will deduct that row amount from grand total amount
        DeductAmountFromGrandTotal($('#' + TotalCostID).val());
        var RowId = $(this).parent().attr('id');//getting parent row
        var v = $(this).parents('.' + RowId + 'Row').nextAll();//getting all the rows after delete
        $(this).parents('.' + RowId + 'Row').remove();
        UpdateMapping(v);
        SetButtonHideView(RowId);

    });
    //------------------Function to Add Template to Neg Fee Pop up-----------------------------//
    function AddTemplate(length, ParentRow) {
        var ValueToView = (ParentRow === "Medications") ? "MEDICATION" : ((ParentRow === "SpecialtyBedMats") ? "SPECIALTY BED/MATS" : ((ParentRow === "WoundCares") ? "WOUND CARE" : ((ParentRow === "LifeVests") ? "LIFE VEST" : ((ParentRow === "Others") ? "OTHER" : ValueToView = ""))));
        var NegFeeTemplate = "<div class='col-md-12 " + ParentRow + "Row Margin_top_carveout_negFee' id='_" + ParentRow + "___" + length + "'>" +
                   "<div class='col-md-1'>" +
                   "<input type='checkbox' name='LOSs[0].NEGFeeDetail." + ParentRow + "[" + length + "]" + ".IsActive'  id='CHECKBOX_" + ParentRow + "___" + length + "' class='form-control NegFeeCheckbox " + ParentRow + "CheckBox checkbox-radio'><label><span></span></label>" +
                    "</div>" +
                    "<div class='col-md-1'>" +
                       "<input  id='NAME_" + ParentRow + "___" + length + "' name='LOSs[0].NEGFeeDetail." + ParentRow + "[" + length + "]" + ".CarveOutType' type='hidden' value='" + ParentRow + "'>" +
                       "<span class='theme_label'><label>" + ValueToView + "</label></span>" +
                    "</div>" +
                    "<div class='col-md-1'>" +
                      "<input class='form-control Disable_Enable text-uppercase input-xs'  id='REASON_" + ParentRow + "___" + length + "' name='LOSs[0].NEGFeeDetail." + ParentRow + "[" + length + "]" + ".Reason' placeholder='REASON' type='text' value=''>" +
                    "</div>" +
                    "<div class='col-md-1'>" +
                       "<input class='form-control Disable_Enable ValueToRight removeUpDownControl CalculateCost input-xs' data-val='true' data-val-number='The field Cost must be a number.' id='COST_" + ParentRow + "___" + length + "' name='LOSs[0].NEGFeeDetail." + ParentRow + "[" + length + "]" + ".Cost' placeholder='COST' type='number' value=''>" +
                       " <span class='DollarInput'><b>$</b></span>" +
                    "</div>" +

                    "<div class='col-md-1'>" +
                       "<input class='form-control Disable_Enable removeUpDownControl input-xs' data-val='true' data-val-number='The field Dose must be a number.'  id='DOSE_" + ParentRow + "___" + length + "' name='LOSs[0].NEGFeeDetail." + ParentRow + "[" + length + "]" + ".Dose' placeholder='DOSE' type='number' value=''>" +
                    "</div>" +
                   "<div class='col-md-1'>" +
                    "<select class='form-control Disable_Enable CalculateCost input-xs text-uppercase' id='RANGE1_" + ParentRow + "___" + length + "' name='LOSs[0].NEGFeeDetail." + ParentRow + "[" + length + "]" + ".Range1'><option value=''>SELECT</option>" +
                       "<option value='DAY'>DAY</option>" +
                       "<option value='DAYS'>DAYS</option>" +
                       "<option value='WEEK'>WEEK</option>" +
                       "<option value='WEEKS'>WEEKS</option>" +
                       "<option value='MONTH'>MONTH</option>" +
                       "<option value='MONTHS'>MONTHS</option>" +
                       "</select>" +
                    "</div>" +
                    "<div class='col-md-1'>" +
                        "<input class='form-control Disable_Enable removeUpDownControl input-xs' data-val='true' data-val-number='The field Unit must be a number.' data-val-required='The Unit field is required.' id='UNITS_" + ParentRow + "___" + length + "' name='LOSs[0].NEGFeeDetail." + ParentRow + "[" + length + "]" + ".Unit' placeholder='UNITS' type='number' value=''>" +
                    "</div>" +
                    "<div class='col-md-1'>" +
                        "<select class='form-control Disable_Enable CalculateCost input-xs text-uppercase' id='RANGE2_" + ParentRow + "___" + length + "' name='LOSs[0].NEGFeeDetail." + ParentRow + "[" + length + "]" + ".Range2'><option value=''>SELECT</option>" +
                       "<option value='DAY'>DAY</option>" +
                       "<option value='DAYS'>DAYS</option>" +
                       "<option value='WEEK'>WEEK</option>" +
                       "<option value='WEEKS'>WEEKS</option>" +
                       "<option value='MONTH'>MONTH</option>" +
                       "<option value='MONTHS'>MONTHS</option>" +
                       "</select>" +
                    "</div>" +
                    "<div class='col-md-1'>" +
                       "<input class='form-control input-xs  datePicker DateTimeField Disable_Enable' id='FROM_DATE_" + ParentRow + "___" + length + "' data-val='true' data-val-date='The field FROM DATE must be a date.' name='LOSs[0].NEGFeeDetail." + ParentRow + "[" + length + "]" + ".FromDate' placeholder='MM/DD/YYYY' type='text'>" +
                    "</div>" +
                    "<div class='col-md-1'>" +
                       "<input class='form-control input-xs datePicker  Disable_Enable' id='TO_DATE_" + ParentRow + "___" + length + "' data-val='true' data-val-date='The field TO DATE must be a date.' name='LOSs[0].NEGFeeDetail." + ParentRow + "[" + length + "]" + ".ToDate' placeholder='MM/DD/YYYY' type='text'>" +
                    "</div>" +
                    "<div class='col-md-1'>" +
                       "<input class='form-control input-xs removeUpDownControl ValueToRight TOTAL_AMOUNT Disable_Enable' data-val='true' data-val-number='The field TOTAL COST must be a number.' id='TOTALCOST_" + ParentRow + "___" + length + "' name='LOSs[0].NEGFeeDetail." + ParentRow + "[" + length + "]" + ".TotalCost' placeholder='TOTAL COST' type='number' value=''>" +
                 "<span class='DollarInput'><b>$</b></span>" + "</div>" +
                    "<div class='col-md-1' id='" + ParentRow + "'>" +
                        "<button type='button' class='btn btn-danger btn-xs NegFeeMinusButton " + ParentRow + "MinusButton Disable_Enable hidden'><i class='fa fa-minus'></i></button>" +
                        "<button type='button' class='btn btn-success btn-xs Disable_Enable NegFeePlusButton " + ParentRow + "PlusButton'><i class='fa fa-plus'></i></button>" +
                    "</div>" +
                "</div>";


        $('#' + ParentRow + "Area").append(NegFeeTemplate);
        $('.datePicker').datetimepicker({
            format: 'MM/DD/YYYY',
            widgetPositioning: {
                vertical: 'bottom'
            }
        });
    }

    //------------------END of Adding Template to neg fee pop up-------------------------------//

    //-----------------To Hide and View Plus And minus button---------------------------------//
    function SetButtonHideView(ParentRow) {

        $('#' + ParentRow + 'Area .' + ParentRow + 'Row').find('.' + ParentRow + 'PlusButton').addClass('hidden');
        $('#' + ParentRow + 'Area .' + ParentRow + 'Row').last().find('.' + ParentRow + 'PlusButton').removeClass('hidden');
        if ($('#' + ParentRow + 'Area .' + ParentRow + 'Row .' + ParentRow + 'MinusButton').length > 1) {
            $('#' + ParentRow + 'Area .' + ParentRow + 'Row .' + ParentRow + 'MinusButton').removeClass('hidden');
        }
        else {
            $('#' + ParentRow + 'Area .' + ParentRow + 'Row .' + ParentRow + 'MinusButton').addClass('hidden');
        }

        //-----for checkbox--------//
        $('#' + ParentRow + 'Area .' + ParentRow + 'Row').find('.' + ParentRow + 'CheckBox').next().addClass('hidden');
        $('#' + ParentRow + 'Area .' + ParentRow + 'Row').first().find('.' + ParentRow + 'CheckBox').next().removeClass('hidden');
        $('#' + ParentRow + 'Area .' + ParentRow + 'Row').first().find('.' + ParentRow + 'CheckBox')[0].checked = true;
        //-------end---------------//
    }
    //------------------END of Hide and View Plus And minus button-----------------------------//


    //--------------------To calculate Cost For Each row--------------------------------------//

    // on keyup in text field inside id=NEG_FEE_MODAL get the parent-parent id and pass to GetValues() and CalculateDate()
    $(document).on('keyup', "#NEG_FEE_MODAL input[type='number']", function () {
        var ParentID = $(this).parent().parent().attr('id');
        GetValues(ParentID);
        CalculateDate(ParentID);
    });
    // on change of dropdown get the parent-parent id and pass to GetValues()
    $(document).on('change', "#NEG_FEE_MODAL select", function () {
        var ParentID = $(this).parent().parent().attr('id');
        GetValues(ParentID);
        CalculateDate(ParentID);
    });

    //Get the id first and from that id  get the values of the text field
    function GetValues(ParentID) {
        //Id for Cost text field
        var CostID = $('#' + ParentID).children().find('#COST' + ParentID)[0].id
        //Value for Cost text field
        var Cost = $('#' + CostID).val();
        //Id for Range1 text field
        var Range1ID = $('#' + ParentID).children().find('#RANGE1' + ParentID)[0].id
        //Value for Range1 text field
        var Range1 = $('#' + Range1ID).val();
        //Id for Units text field
        var UNITSID = $('#' + ParentID).children().find('#UNITS' + ParentID)[0].id
        //Value for Units text field
        var Units = $('#' + UNITSID).val();
        //Id for Range2 text field
        var Range2ID = $('#' + ParentID).children().find('#RANGE2' + ParentID)[0].id
        //Value for Range2 text field
        var Range2 = $('#' + Range2ID).val();



        //Calculate the Total cost row wise.
        var TotalCost = Cost * Units;
        if (Range1 === 'DAY' && Range2 === 'DAY') {
            TotalCost = Cost * Units;
        }
        else if (Range1 === 'DAY' && Range2 === 'DAYS') {
            TotalCost = Cost * Units;
        }
        else if (Range1 === 'DAY' && Range2 === 'WEEK') {
            TotalCost = 7 * Cost;
        }
        else if (Range1 === 'DAY' && Range2 === 'WEEKS') {
            TotalCost = 7 * Cost * Units;
        }
        else if (Range1 === 'DAY' && Range2 === 'MONTH') {
            TotalCost = 30 * Cost;
        }
        else if (Range1 === 'DAY' && Range2 === 'MONTHS') {
            TotalCost = 30 * Cost * Units;
        }
        else if (Range1 === 'WEEK' && Range2 === 'WEEK') {
            TotalCost = Cost * Units;
        }
        else if (Range1 === 'WEEK' && Range2 === 'WEEKS') {
            TotalCost = Cost * Units;
        }
        else if (Range1 === 'WEEK' && Range2 === 'MONTH') {
            TotalCost = Cost * (parseInt((Units * 30) / 7));
        }
        else if (Range1 === 'WEEK' && Range2 === 'MONTHS') {
            TotalCost = Cost * (parseInt((Units * 30) / 7));
        }
        else if (Range1 === 'MONTH' && Range2 === 'MONTH') {
            TotalCost = Cost * Units;
        }
        else if (Range1 === 'MONTH' && Range2 === 'MONTHS') {
            TotalCost = Cost * Units;
        }

        //Get the id of the total cost input field and add Total cost for particular row
        var TotalCostID = $('#' + ParentID).children().find('#TOTALCOST' + ParentID)[0].id
        $('#' + TotalCostID).val(TotalCost);

        //get the value of the pos
        var pos = $('#PlaceOfService').val().split('-')[0];
        if (pos === "31") {
            //Call the function to calculate the grand total amount
            CalculateGrandTotalAmountForPos31();
        }
        else {
            CalculateGrandTotalAmountForPos61();
        }
    }



    //-----------------------End of Calculating Cost------------------------------------------//

    //--------------- To Calculate To Date in NEG FEE---------------------------------------//

    // on change of date get the parent-parent id and pass to CalculateDate()
    $(document).on('blur', "#NEG_FEE_MODAL .DateTimeField", function () {
        var ParentID = $(this).parent().parent().attr('id');
        CalculateDate(ParentID)
    });


    function CalculateDate(ParentID) {
        //Id for UNITS text field
        var UNITSID = $('#' + ParentID).children().find('#UNITS' + ParentID)[0].id
        //Value for Units text field
        var Units = $('#' + UNITSID).val();
        //Id for Range2 text field
        var Range2ID = $('#' + ParentID).children().find('#RANGE2' + ParentID)[0].id
        //Value for Range2 text field
        var Range2 = $('#' + Range2ID).val();
        //Id for FROM_DATE text field
        var FromdateID = $('#' + ParentID).children().find('#FROM_DATE' + ParentID)[0].id
        //Value for FROM_DATE text field
        var FromDate = $('#' + FromdateID).val();

        var date = new Date(FromDate);
        var dayCount = Units;
        if (Range2 === 'WEEK' || Range2 === 'WEEKS') {
            dayCount = 7 * Units;
        }
        else if (Range2 === 'MONTH' || Range2 === 'MONTHS') {
            dayCount = 30 * Units;
        }

        var toDate = date.setDate(date.getDate(date) + parseInt(dayCount));
        toDate = moment(toDate).format('MM/DD/YYYY');

        if (Range2 && Units && FromDate) {
            var ToDateID = $('#' + ParentID).children().find('#TO_DATE' + ParentID)[0].id
            $('#' + ToDateID).val(toDate);
        }
        else {
            // do nothing
            //  console.log("null");
        }


    }

    //--------------- End of Calculating To Date in NEG FEE---------------------------------------//

    // Calculate neg based on level

    //---------------Calculate Neg Fee Based on Levels which is in the top----------------------//
    $(document).on('keyup change', ".LevelFields", function () {
        CalculateGrandTotalAmountForPos31();

    });

    //---------------End of Calculating  Neg Fee Based on Levels which is in the top----------------------//

    //--------------------- Calculate Grand Total by calculating each Total cost for rows and Neg fee based on levels----------//
    function CalculateGrandTotalAmountForPos31() {
        //Value for Level Cost
        var LevelCost = $('#LevelCost').val();
        //Value for Level Range
        var LevelRange = $('#LevelRange').val();
        //Value for Level Range
        var LevelReqLoss = $('#LevelReqLoss').val();
        var TotalCostForLevels = 0;
        if (LevelRange === 'DAY') {
            TotalCostForLevels = (LevelCost * LevelReqLoss);
        }
        if (LevelRange === 'WEEK') {
            TotalCostForLevels = (LevelCost * LevelReqLoss / 7);
        }
        if (LevelRange === 'MONTH') {
            TotalCostForLevels = (LevelCost * LevelReqLoss / 30);
        }
        // cal the function and get the total value 
        var CalculatedTotalSum = calculateTotalCostOfEachRow();
        var TotalNegFeeIncludingLevelFee = (TotalCostForLevels + CalculatedTotalSum).toFixed(2);
        GrandTotalValue(TotalNegFeeIncludingLevelFee);
    }
    function calculateTotalCostOfEachRow() {
        // calculate the total amount from each total cost field 
        var calculated_total_sum = 0;
        $("#NEG_FEE_MODAL .TOTAL_AMOUNT").each(function () {
            var get_textbox_value = $(this).val();
            if ($.isNumeric(get_textbox_value)) {
                calculated_total_sum += parseFloat(get_textbox_value);
            }
        });
        return calculated_total_sum;
    }

    //--------------------- End of Calculating  Grand Total by calculating each Total cost for rows and Neg fee based on levels----------//

    //----- save the neg fee in expected charge text field and also  validate the cost field---------------------//

    $(".NegFeeSaveButton").click(function () {
        var LevelCostValue = (($("#PlaceOfService").val().split('-')[0]) === "31") ? $('#LevelCost').val() : $('#BaseCostDrugOrDiem').val()
        if (!(LevelCostValue === "")) {
            //SaveNegFee();
            $form = $("#Neg_Fee_Details");
            $('#negFeeArea').html($form.html());
            var TotalChargeExpected = $('#Total_Amount').val();
            $('#ExpectedCharge').val(TotalChargeExpected);
            $("#NegFeeCancelButton").trigger("click");
            // $($form).insertAfter("#dynamicSection");
            //var $form = $("#Neg_Fee_Details");
            //var formData = new FormData($form[0]);
            //$.ajax({
            //    type: 'POST',
            //    url: '/UM/Authorization/SaveNegFee',
            //    data: formData,
            //    processData: false,
            //    contentType: false,
            //    cache: false,
            //    error: function () {
            //    },
            //    success: function (data) {

            //    }
            //});

        }
        else {
            //log is temporary. have to implement validation .
            console.log("please fill cost");
        }

    });

    // Deduct Amount From Grand Total when a row is removed by clicking on minus button
    function DeductAmountFromGrandTotal(valueOfRowTotalCost) {

        var ValueOfGrandTotalField = $('#Total_Amount').val();
        GrandTotalValue(ValueOfGrandTotalField - valueOfRowTotalCost);
    }

    // function to append grand total value in total amount field.
    function GrandTotalValue(Value) {
        $("#Total_Amount").val(Value);
    }
    // clear fields when checkbox is unchecked AND change the total amount when the checkbok is unchecked
    function resetAllValuesForParticularAreaWhenCheckboxIsUnchecked(SuperParent) {
        var _total_sum = 0;

        $("#" + SuperParent).find('.TOTAL_AMOUNT').each(function () {
            var get_textbox_value = $(this).val();
            if ($.isNumeric(get_textbox_value)) {
                _total_sum += parseFloat(get_textbox_value);
            }
        });
        var ValueOfGrandTotalField = $('#Total_Amount').val();
        GrandTotalValue(ValueOfGrandTotalField - _total_sum);
        $('#' + SuperParent).find('input:text').val('');
        $('#' + SuperParent).find('select').prop('selectedIndex', 0);
    }



    //--------------------Neg fee pos 61------------------------------//
    // get the value og requested los and put the value in req los
    $('#ReqLosForDrgOrDiem').val($('#TotalRequestedLOS').val());

    // on change of drg/rate
    $(document).on('change', "#DrgRateDropDown", function () {
        $('#BaseCostDrugOrDiem').val(25000);
        $('#percentageOfDrug').val("");
        $('#RangeDrugOrDiem').val('');
        if ($(this).val() === "DRG") {
            $('#percentageOfDrug').prop("disabled", false);
            $('#RangeDrugOrDiem').prop("disabled", true);

        }
        else if ($(this).val() === "PERDIEM") {
            $('#percentageOfDrug').val('');
            $('#percentageOfDrug').prop("disabled", true);
            $('#RangeDrugOrDiem').prop("disabled", false);
        }
        else {

            $('#RangeDrugOrDiem').prop("disabled", true);
            $('#percentageOfDrug').prop("disabled", true);
            $('#BaseCostDrugOrDiem').val('');

        }
        //Get the total amount by iterating through each row 
        var TotalAmountOfEachRow = calculateTotalCostOfEachRow();
        //pass the grand total to GrandTotalValue so that it will be visible in Total cost
        GrandTotalValue(TotalAmountOfEachRow)
    });
    // keyup or change in top any field of pos 61 
    $(document).on('keyup change', ".DrgRateField", function () {
        CalculateGrandTotalAmountForPos61();
    });

    //if user wants to change the cost 
    $(document).on('keyup', "#BaseCostDrugOrDiem", function () {
        var ValueOfDropDownFieldOfDrgRate = $('#DrgRateDropDown').val();
        if (ValueOfDropDownFieldOfDrgRate === "DRG") {
            var TotalAmountOfEachRow = calculateTotalCostOfEachRow();
            var valueOfDrg = $(this).val();
            valueOfDrg = (valueOfDrg == "") ? 0 : valueOfDrg;
            GrandTotalValue(TotalAmountOfEachRow + parseInt(valueOfDrg));
        }
        else if ((ValueOfDropDownFieldOfDrgRate === "PERDIEM")) {
            CalculateGrandTotalAmountForPos61();
        }
    });
    // called when drop down value iS CHANGED
    function CalculateGrandTotalAmountForPos61() {
        var TotalDrugValue = 0;
        var ValueOfDropDownFieldOfDrgRate = $('#DrgRateDropDown').val();
        if (ValueOfDropDownFieldOfDrgRate === "DRG") {
            TotalDrugValue = CalculateCostForDrg();
        }
        else if (ValueOfDropDownFieldOfDrgRate === "PERDIEM") {
            TotalDrugValue = CalculateCostForPerDiem();

        }
        else {
            TotalDrugValue = 0;
        }
        // cal the function and get the total value 
        var CalculatedTotalSum = calculateTotalCostOfEachRow();
        var TotalNegFee = (TotalDrugValue + CalculatedTotalSum).toFixed(2);
        GrandTotalValue(TotalNegFee);
    }
    // WHEN DROPDOWN VALUE IS DRG AND WILL RETURN THE COST
    function CalculateCostForDrg() {
        var Drugvalue = $('#percentageOfDrug').val();
        Drugvalue = (Drugvalue === "" || null) ? 0 : Drugvalue;
        var TotalDrugCost = parseInt(Drugvalue) * 25000 / 100;
        var valueForBaseCost = $('#BaseCostDrugOrDiem').val(TotalDrugCost);
        return TotalDrugCost;
    }
    // WHEN DROP DOWN VALUE IS PER DIEM  AND WILL RETURN THE COST
    function CalculateCostForPerDiem() {
        var TotalCostForPerDiemValue = 0;
        var Basecost = $('#BaseCostDrugOrDiem').val();
        var RangePerDiem = $('#RangeDrugOrDiem').val();
        var ReqLos = $('#ReqLosForDrgOrDiem').val();
        if (Basecost != "" && ReqLos != "") {
            if (RangePerDiem === "DAY") {
                TotalCostForPerDiemValue = parseInt(Basecost) * parseInt(ReqLos);
            }
            else if (RangePerDiem === "WEEK") {
                TotalCostForPerDiemValue = (parseInt(Basecost) * parseInt(ReqLos)) / 7;
            }
            else if (RangePerDiem === "MONTH") {
                TotalCostForPerDiemValue = (parseInt(Basecost) * parseInt(ReqLos)) / 30;
            }
        }


        return TotalCostForPerDiemValue;
    }
})





