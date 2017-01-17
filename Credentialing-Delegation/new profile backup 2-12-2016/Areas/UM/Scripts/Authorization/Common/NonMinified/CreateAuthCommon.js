var AddPLRow = function (plrowid, id) {
    var cptselectid = 'cptcodeselect' + id;
    var cptmodifierid = 'Modifier' + id;
    var cptcodedescid = 'CPTDesc' + id;
    var cptrequnits = 'RequestedUnits' + id;
    var cptIncletter = 'IncludeLetter' + id;
    return '<div class="x_content" id="' + plrowid + '">' +
    '<div class="col-lg-2"><select id="' + cptselectid + '" name="standard" class="form-control input-xs mandatory_field_halo custom-select cptcodedropdown"><option value="">None - Please Select</option><option value="0001F-Heart Failure Composite">0001F-Heart Failure Composite</option>  <option value="0001M-Infectious Disc HCV6 Assays">0001M-Infectious Disc HCV6 Assays</option> <option value="0002M-Liver Dis 10 Assays Wash">0002M-Liver Dis 10 Assays Wash</option>  <option value="00215-Anesth Skull Repair/Fract">00215-Anesth Skull Repair/Fract</option></select></div>' +
    '<div class="col-lg-1"><input class="form-control input-xs mandatory_field_halo" id="' + cptmodifierid + '" name="' + cptmodifierid + '" placeholder="MODIFIER" type="text"></div>' +
    '<div class="col-lg-3"><input class="form-control input-xs mandatory_field_halo" id="' + cptcodedescid + '" name="' + cptcodedescid + '" placeholder="DESCRIPTION" type="text"></div>' +
    '<div class="col-lg-1"><input class="form-control input-xs non_mandatory_field_halo"  id="' + cptrequnits + '" name="' + cptrequnits + '"  data-val="true" data-val-number="The field RequestedUnits must be a number." placeholder="REQ UNITS" type="text"></div>' +
    '<div class="col-lg-1 text-center button-styles-xs row-action">&nbsp;' + deleteButton() + addButton() + '</div>' +
    '<div class="col-lg-1 text-center pull_to_left"><input type="checkbox" class="flat" id="' + cptIncletter + '" name="' + cptIncletter + '"/></div>' +
    '</div>'
};
$('#Refnum').html('-');

//------Adding New Provider Area------//
$('#UM_auth_form').off('click', '.Add_New_Provider_Btn').on('click', '.Add_New_Provider_Btn', function () {
    TabManager.openFloatingModal("~/Areas/Portal/Views/ProviderBridge/AddProvider/_ProviderBody.cshtml", "~/Areas/Portal/Views/ProviderBridge/AddProvider/_ProviderHeader.cshtml", "~/Areas/Portal/Views/ProviderBridge/AddProvider/_ProviderFooter.cshtml");
});
$('#UM_auth_form').off('click', '.Add_New_Facility_Btn').on('click', '.Add_New_Facility_Btn', function () {
    TabManager.openFloatingModal("~/Areas/Portal/Views/FacilityBridge/AddFacility/_FacilityBody.cshtml", "~/Areas/Portal/Views/FacilityBridge/AddFacility/_FacilityHeader.cshtml", "~/Areas/Portal/Views/FacilityBridge/AddFacility/_FacilityFooter.cshtml");
});
//End Of Adding New Provider Area-----//

//-----------------Requesting Provider Section common for all Pos----------------------------//

$('#UM_auth_form').off('click', '.options_label').on('click', '.options_label', function (evt) {
    evt.stopPropagation();
    evt.preventDefault();
    if (!$(this).hasClass("btn-success")) {
        if ($(this).find('input[type=checkbox]')[0].id == 'RequestingProvider_IsUseMember') {
            GetMemberData($('#MemberID')[0].innerText, this);

        }
        else if ($(this).find('input[type=checkbox]')[0].id == 'RequestingProvider_IsUsePCP') {
            GetPcpData($('#MemberID')[0].innerText, this);
            AddRemoveClass('RequestingProvider_IsUseMember', 'btn-ghost', 'btn-success', false);
        }
        else {
            GetPcpData($('#MemberID')[0].innerText, this);
        }
        RemoveAddNewProviderButton($(this).parent().find('input[type=text]')[0].id);
    }
    else {
        $(this).addClass('btn-ghost');
        $(this).removeClass('btn-success');
        RemoveReadonly($(this).parent().find('input[type=text]'), false);
        RemoveChecked($(this).parent().find('input[type=checkbox]'))
        if($(this).parent().find('input[type=text]')[0].id=='AttendingProvider_FullName')
        {
            AddRemoveClass('AdmittingProvider_IsUsePCP', 'btn-ghost', 'btn-success', false);
            RemoveReadonly($('#AdmittingProvider_FullName'), false);
        }
    }
})

function RemoveChecked(ID)
{
    $(ID)[0].checked = false;
}

function AddRemoveClass(ID, addCssClass, removeCssClass, val) {
    $('#' + ID).parent().addClass(addCssClass);
    $('#' + ID).parent().removeClass(removeCssClass);
    $('#' + ID)[0].checked = val;
}

hideAllSearchCumDropDown = function () {
    $('.UMDropDownList').remove();
}


function SetReadonly(TextBox, val) {
    $(TextBox)[0].readOnly = val;
    $(TextBox).addClass('read_only_field');
}
function RemoveReadonly(TextBox, val) {
    $(TextBox)[0].readOnly = val;
    $(TextBox)[0].value = "";
    $(TextBox).removeClass('read_only_field');
    RemoveExistingHiddenInput($(TextBox).parent().find('input[type=text]')[0].id)
}
function setClassAndReadonly(ob)
{
 AddRemoveClass($(ob).find('input[type=checkbox]')[0].id, 'btn-success', 'btn-ghost', true);
 SetReadonly($(ob).parent().find('input[type=text]'), true);

 if ($(ob).parent().find('input[type=text]')[0].id == 'AttendingProvider_FullName')
 {
     AddRemoveClass('AdmittingProvider_IsUsePCP', 'btn-success', 'btn-ghost', true);
     SetReadonly($('#AdmittingProvider_FullName'), true);
 }
}
//---------------------Getting PCP Data---------------------------------------------------//
var MemberPcpData = {};
var MemberData = {};
function GetPcpData(SubscriberID, ob) {
    if ((MemberPcpData && MemberPcpData.PCP && MemberPcpData.SubscriberID == SubscriberID)) {
        SetValue(MemberPcpData.PCP, $(ob).parent().find('input[type=text]')[0].id);
        setClassAndReadonly($(ob));
    }
    else {
        var providerDataWorker = new Worker('/Areas/UM/WebWorkers/AjaxWebWorker.js');
        providerDataWorker.postMessage({ url: '/UM/Authorization/GetPcpData', singleParam: false, searchTerm: { SubscriberID: SubscriberID } });
        providerDataWorker.addEventListener('message', function (e) {
            if (e.data) {
                MemberData = JSON.parse(e.data);
                providerDataWorker.terminate();
                MemberPcpData = MemberData;
                SetValue(MemberPcpData.PCP, $(ob).parent().find('input[type=text]')[0].id);
                setClassAndReadonly($(ob));
            }
        });
    }
}

function GetMemberData(SubscriberID, ob) {
    if ((MemberData && MemberData.SubscriberID == SubscriberID)) {
        SetValue(MemberData, $(ob).parent().find('input[type=text]')[0].id);
        AddRemoveClass($(ob).find('input[type=checkbox]')[0].id, 'btn-success', 'btn-ghost', true);
        AddRemoveClass('RequestingProvider_IsUsePCP', 'btn-ghost', 'btn-success', false);
        SetReadonly($(ob).parent().find('input[type=text]'), true);
    }
    else {
        var memberDataWorker = new Worker('/Areas/UM/WebWorkers/AjaxWebWorker.js');
        memberDataWorker.postMessage({ url: '/UM/Authorization/GetPcpData', singleParam: false, searchTerm: { SubscriberID: SubscriberID } });
        memberDataWorker.addEventListener('message', function (e) {
            if (e.data) {
                data = JSON.parse(e.data);
                memberDataWorker.terminate();
                MemberData = data.MemberContact;
                MemberData.SubscriberID = data.SubscriberID;
                MemberData.FirstName = data.FirstName;
                MemberData.LastName = data.LastName;
                MemberData.FullName = data.FullName;
                SetValue(MemberData, $(ob).parent().find('input[type=text]')[0].id);
                AddRemoveClass($(ob).find('input[type=checkbox]')[0].id, 'btn-success', 'btn-ghost', true);
                AddRemoveClass('RequestingProvider_IsUsePCP', 'btn-ghost', 'btn-success', false);
                SetReadonly($(ob).parent().find('input[type=text]'), true);
            }
        });
    }
}

//-------------------------End of Getting Pcp Data----------------------------------------//



//----------------------Rules for CO-ManageMent Provider--------------------------------//

$('#UM_auth_form').off('click', '#coMagChk').on('click', '#coMagChk', function () {
    if(!($(this).is(':checked')))
    {
        $('#coMagProvider').val('');
        $('#coMagProvider').parent().find('input[type="hidden"]').remove();
        $('#coMagProvider').parent().find('.INOONLabel').remove();
    }
})
//--------------------------------------------------------------------------------------//
