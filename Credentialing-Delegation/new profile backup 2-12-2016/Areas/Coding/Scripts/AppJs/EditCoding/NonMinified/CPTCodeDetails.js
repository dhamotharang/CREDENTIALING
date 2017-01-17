var isCPTHistoryData = false;
var checkedCptList = [];
var cptCodes = [];


/** 
@description Gets the CPT Information and returns the CPT History View.
 */
$('.CPTCodeMainpanel').off('click', '.ActiveCPTCodes_Edit').on('click', '.ActiveCPTCodes_Edit', function () {
    if (!isCPTHistoryData) {
        $.ajax({
            type: 'GET',
            url: '/Coding/Coding/GetCPTCodeHistory',
            success: function (data) {
                $('#CPTCodeHistory_Edit').html(data);
                isCPTHistoryData = true;
            }
        });
    }
    $('#CPTCodeHistory_Edit').toggle();
});


/** 
@description This event triggers by clicking on the close button of Active Diagnosis Codes Panel. It closes the Active Diagnosis Codes Panel.
 */
$('.CPTCodeMainpanel').off('click', '.closeactiveCptCodes').on('click', '.closeactiveCptCodes', function () {
    $('#CPTCodeHistory_Edit').toggle();
});


/** 
@description Gets the only CPT Code History Data.
 */
$.ajax({
    type: 'GET',
    url: '/Coding/Coding/GetCptHistoryData',
    success: function (data) {
        cptCodes = data.Cptcodes;
    }
});


/** 
@description Appends the CPT CODES Information to the CPT Table, which are selected through the History.
 */
$('#CPTCodeHistory_Edit').off('click', '.cptCode').on('click', '.cptCode', function () {
    if ($(this).is(':checked')) {
        checkedCptList.push(this.value)
    } else {
        var removeItem = this.value;
        checkedCptList = jQuery.grep(checkedCptList, function (value) {
            return value != removeItem;
        });
    }
    var tableBodyTag = "";
    var trTag = '<tr>';
    var trEndTag = '</tr>';
    var tdTag = '<td>';
    var tdEndTag = '</td>';

    for (var i = 0; i < checkedCptList.length; i++) {
        for (var j = 0; j < cptCodes.length; j++) {
            if (checkedCptList[i] === cptCodes[j].Code) {
                tableBodyTag = tableBodyTag + trTag
            + tdTag + '<input class="form-control input-xs text-uppercase" placeholder="" type="text" value="' + cptCodes[j].Code + ' ">' + tdEndTag
                          + tdTag + '<input class="form-control input-xs text-uppercase" placeholder="" type="text" value="' + cptCodes[j].Description + '">' + tdEndTag
                          + tdTag + '<input class="form-control input-xs text-uppercase modifier_width" placeholder="" type="text" value=""><input class="form-control input-xs text-uppercase modifier_width" placeholder="" type="text" value=""><input class="form-control input-xs text-uppercase modifier_width" placeholder="" type="text" value=""><input class="form-control input-xs text-uppercase modifier_width" placeholder="" type="text" value="">' + tdEndTag
                          + tdTag + '<input class="form-control input-xs text-uppercase modifier_width" placeholder="" type="text" value=""><input class="form-control input-xs text-uppercase modifier_width" placeholder="" type="text" value=""><input class="form-control input-xs text-uppercase modifier_width" placeholder="" type="text" value=""><input class="form-control input-xs text-uppercase modifier_width" placeholder="" type="text" value=""><a class="btn btn-xs openDiagnosisModal"><i class=" fa fa-forward"></i></a>' + tdEndTag
                          + tdTag + '<label class="CPTdollar_label">$</label><input class="form-control input-xs text-uppercase CPTFee_width" placeholder="" type="text" value="' + cptCodes[j].Fee + '">' + tdEndTag
                          + '<td onclick="RemoveRows(this)"><button class="btn btn-xs btn-danger"><i class="fa fa fa-minus"></i></button></td>'
      + trEndTag
            }
        }
    }
    $('#CPTTablebody_Edit').html(tableBodyTag)
})


/** 
@description this event triggers by clicking on add. It will append a new row to the table body
 */
$('.CPTCodeMainpanel').off('click', '#AddNewCPTRow_Edit').on('click', '#AddNewCPTRow_Edit', function () {
    var tbody = $("form#savedetailsForm").find('#CPTTablebody_Edit');
    var lasttr = tbody[0].lastElementChild;
    var prevIndex = parseInt($(lasttr).find('input').attr('id').match(/\d+/)[0]);
    var templateIndex = ++prevIndex;
    var newCPTTr;
    $.ajax({
        type: 'GET',
        url: '/Coding/Coding/CreateCPT?index='+templateIndex,
        success: function (data) {
            newCPTTr = data;
            $('#CPTTablebody_Edit').append(newCPTTr);
        }
    });
   
    
    //$('#CPTTablebody_Edit').append(
    //    '<tr>' +
    //    '<td><input class="form-control input-xs text-uppercase" onchange="checkenmcode()" id="CPTCodes_' + templateIndex + '__Code" name="CPTCodes[' + templateIndex + '].Code" type="text" />' +
    //      '<span class="field-validation-valid" data-valmsg-for="CPTCodes[' + templateIndex + '].Code" data-valmsg-replace="true"></span></td>' +
    //    '<td><input class="form-control input-xs text-uppercase" placeholder="" type="text" value="" id="CPTCodes_' + templateIndex + '__Description" name="CPTCodes[' + templateIndex + '].Description">' +
    //    '<span class="field-validation-valid" data-valmsg-for="CPTCodes[' + templateIndex + '].Description" data-valmsg-replace="true"></span></td>'
    //    + '<td>'
    //       + '<input class="form-control input-xs text-uppercase modifier_width" placeholder="" id="CPTCodes_' + templateIndex + '_Modifier1" name="CPTCodes[' + templateIndex + '].Modifier1" type="text" value="">'
    //       + '<input class="form-control input-xs text-uppercase modifier_width" placeholder="" id="CPTCodes_' + templateIndex + '_Modifier2" name="CPTCodes[' + templateIndex + '].Modifier2" type="text" value="">'
    //       + '<input class="form-control input-xs text-uppercase modifier_width" placeholder="" id="CPTCodes_' + templateIndex + '_Modifier3" name="CPTCodes[' + templateIndex + '].Modifier3" type="text" value="">'
    //       + '<input class="form-control input-xs text-uppercase modifier_width" placeholder="" id="CPTCodes_' + templateIndex + '_Modifier4" name="CPTCodes[' + templateIndex + '].Modifier4" type="text" value="">'
    //    + '</td>'
    //    + '<td>'
    //       + '<input class="form-control input-xs text-uppercase Diapointer_width" placeholder="" id="CPTCodes_' + templateIndex + '_DiagnosisPointer1" name="CPTCodes[' + templateIndex + '].DiagnosisPointer1" type="text" value="">'
    //       + '<input class="form-control input-xs text-uppercase Diapointer_width" placeholder="" id="CPTCodes_' + templateIndex + '_DiagnosisPointer2" name="CPTCodes[' + templateIndex + '].DiagnosisPointer2" type="text" value="">'
    //       + '<input class="form-control input-xs text-uppercase Diapointer_width" placeholder="" id="CPTCodes_' + templateIndex + '_DiagnosisPointer3" name="CPTCodes[' + templateIndex + '].DiagnosisPointer3" type="text" value="">'
    //       + '<input class="form-control input-xs text-uppercase Diapointer_width" placeholder="" id="CPTCodes_' + templateIndex + '_DiagnosisPointer4" name="CPTCodes[' + templateIndex + '].DiagnosisPointer4" type="text" value="">'
    //       + '<a onclick="addDP(this)" class="btn btn-xs openDiagnosisModal"><i class=" fa fa-forward"></i></a>'
    //    + '</td>'
    //    + '<td><label class="CPTdollar_label">$</label><input class="form-control input-xs text-uppercase CPTFee_width" placeholder="" id="CPTCodes_' + templateIndex + '_DiagnosisPointer4" name="CPTCodes[' + templateIndex + '].Fee" type="text" value="">' +
    //    '<span class="field-validation-valid" data-valmsg-for="CPTCodes[' + templateIndex + '].Fee" data-valmsg-replace="true"></span></td>'
    //    + '<td onclick="RemoveRows(this)"><button class="btn btn-xs btn-danger"><i class="fa fa fa-minus"></i></button></td>'
    //    + '</tr>');

});

/** 
@description This event triggers by clicking on arrow mark besides to the Diagnosis Pointers in CPT Code table. It Opens a modal from where the User can selects the Diagnosis Pointers
 */
$('#ICDpointerModalvalues').off('click', 'button').on('click', 'button', function (e) {
    e.preventDefault();
    $(this).hasClass('btn-primary') ? toggleBtnClass($(this), 'btn-primary', 'btn-default') : toggleBtnClass($(this), 'btn-default', 'btn-primary');
});
function toggleBtnClass(ele, oldClass, newClass) {
    ele.removeClass(oldClass).addClass(newClass);
}

//$('#addDiagnPointer').ready(function () {

//    var icdCode = $('#ICDCodetbody_Edit').find('tr:first-child').find('td:first-child').find('input').val();
//    if (icdCode != "")
//        $('#addDiagnPointer').attr('disabled', false);
//    else
//        $('#addDiagnPointer').attr('disabled', true);
//});
$("#CPTCodeHistory_Edit input[Name*='.Code']").change(function () {
    console.log('CPTCode Changed');
    var selectedText = $(this).find("option:selected").text();
    var selectedValue = $(this).val();
    alert("Selected Text: " + selectedText + " Value: " + selectedValue);
});
function checkenmcode(ele) {
    console.log(ele);
    var Index = parseInt($(ele).parent().find('input:first-child').attr('id').match(/\d+/)[0]);
    var currIndex = Index + 1;
    $('#CPTTablebody_Edit').find('tr:nth-child(' + currIndex + ')').find('td:first-child').find('.field-validation-error').addClass('displayNone');
}

function addDP(ele) {
    var Index = parseInt($(ele).parent().find('input:first-child').attr('id').match(/\d+/)[0]);
    var currIndex = Index + 1;
    var CPTCodeElement = $('#CPTTablebody_Edit').find('tr:nth-child(' + currIndex + ')').find('td:first-child').find('input');
    var CPTCode = CPTCodeElement.val();
    if (CPTCode != "") {
        DPIndexes = [];
        var ICDCodes = [];
        $("form#savedetailsForm input[name*='.ICDCode']").each(function () {
            var input = $(this);
            var value = input.val();
            if (value != "")
                ICDCodes.push(value);
        });

        $("form#savedetailsForm input[name*='.Code']").each(function () {
            if ($(this).val() == CPTCode) {
                var Index = parseInt($(this).attr('id').match(/\d+/)[0]);

                DPIndexes.push(Index);
            }
        });
        DPs = [];
        $.each(DPIndexes, function (index,value) {


            var name = "[" + value + "].DiagnosisPointer";
            $("form#savedetailsForm input[name*='" + name + "']").each(function () {
                var input = $(this);
             
                var value = input.val();
                DPs.push(value);
            });

        });

        var templateHtml = "";
        ICDCodes.forEach(function (code) {
            if (checkIFCodeIsSelected(code, DPs)) {
                templateHtml += '<div class="col-lg-2"><button type="button" class="btn btn-xs btn-primary">' + code + '</button></div>';
            }
            else {
                templateHtml += '<div class="col-lg-2"><button type="button" class="btn btn-xs btn-default">' + code + '</button></div>';
            }

        });

        $("#ICDpointerModalvalues").html(templateHtml);
        showModal('DiagnosispointerModal');
    }
    else {
        CPTCodeElement.parent().find('.displayNone').removeClass('displayNone');
    }
};

function saveDP() {
    selectedDPs = [];
    $("#ICDpointerModalvalues").find('button').each(function () {
        if ($(this).hasClass('btn-primary')) {
            selectedDPs.push($(this).text());
        }
    });
    console.log(DPIndexes);
    console.log(DPs);

  
    var selectedLength = selectedDPs.length;
    var DPIndex = DPIndexes[0];
    var count = getLoopingIndex(selectedLength);
    for (var i = 0; i <= count; i++, DPIndex++) {

        var trNew = createTr(i);
        var childIndex = DPIndex + 1;
        var oldTr = $('#CPTTablebody_Edit tr:nth-child(' + childIndex + ')');
        if (DPIndexes.length >= childIndex) {
           
            oldTr[0].replaceWith(trNew[0]);
            modifyIndexes(DPIndex);
        }
        else {
           
            oldTr.after(trNew);
            modifyIndexes(DPIndex);
        }

    }
    if (DPIndexes.length > i) {
        var newTr = createTr(0);
        var childIndex1 = DPIndexes[0] + 1;
        if (DPIndexes.length >= childIndex1) {
            var oldTr = $('#CPTTablebody_Edit tr:nth-child(' + childIndex1 + ')');
            oldTr[0].replaceWith(newTr[0]);
            modifyIndexes(DPIndexes[0]);
        }

        var childIndex2 = DPIndexes[DPIndexes.length - 1] + 1;
        var tr = $('#CPTTablebody_Edit tr:nth-child(' + childIndex2 + ')');
        RemoveCPTRows(tr);


    }
}
function createTr(limitIndex) {
    var limit = (limitIndex) * 4;
    var DPIndex = DPIndexes[0];
    var tr = $('#CPTTablebody_Edit tr:nth-child(' + DPIndex + 1 + ')');
    var trNew = tr.clone();
    trNew.find("input[name *= 'DiagnosisPointer']").each(function () {
        if (limit < selectedDPs.length) {
            $(this).val(selectedDPs[limit++]);
        }
        else {
            $(this).val('');
        }
    });
    return trNew;
}
function getLoopingIndex(length) {
    if (length == 0)
        return 0;
    else if (length % 4 == 0)
        return Math.floor((length / 4)) - 1;
    else
        return Math.floor(length / 4);
}

function modifyIndexes(DPIndex) {
    var tbody = $('#CPTTablebody_Edit');
    tbody.children().each(function (index) {
        if (index >= DPIndex) {
            maintainIndexes(this);
        }
    });
}


function RemoveCPTRows(ele) {
    var tbody = $(ele).parent();
    var trIndex = findCPTRemovedElementIndex(ele[0]);
    $(ele).remove();
    tbody.children().each(function (index) {
        if (index >= trIndex) {
            maintainIndexes(this);
        }
    });


}
function findCPTRemovedElementIndex(tr) {

    var trIndex;
    $(tr).parent().children().each(function (index) {
        if (this === tr) {
            trIndex = index;
        }

    });
    return trIndex;
}

function checkIFCodeIsSelected(code, DPs) {
    var flag = 0;
    DPs.some(function (dp) {
        if (dp == code) {
            flag = 1;
            return true;
        }

    });
    if (flag == 1) {
        return true;
    }
   
    
    return false;
}
function maintainIndexes(ele) {
    var inputs = $(ele).find('input');
    inputs.each(function () {
        var id = $(ele).attr("id");
        var newId = id.replace(/[0-9]+/, index);
        var name = $(ele).attr("name");
        var newName = name.replace(/[0-9]+/, index);
        $(ele).attr("id", newId);
        $(ele).attr("name", newName);

    });
    var spans = $(ele).find('span');
    spans.each(function () {
        var msg = $(ele).attr("data-valmsg-for");
        if (typeof msg != 'undefined') {
            var newmsg = msg.replace(/[0-9]+/, index);
            $(ele).attr("data-valmsg-for", newmsg);
        }
    });
}