//Author : Rahul Teja

$('#QueueName').show().text('Portal');
var memberData = TabManager.getMemberData();  //Get the Current Member Data
setMemberHeaderData(memberData);

if ((typeof preAuthMBRID != 'undefined') && (typeof preAuthMBRLastNameID != 'undefined') && (typeof preAuthMBRFirstNameID != 'undefined') && (typeof requestDateID != 'undefined') && (typeof preAuthMBRPhoneNumberID != 'undefined') && (typeof preAuthMBRDOBID != 'undefined')) {
    var idArray = [preAuthMBRID,
               preAuthMBRLastNameID,
               preAuthMBRFirstNameID,
               requestDateID,
               preAuthMBRPhoneNumberID,
               preAuthMBRDOBID,
    ];
}

var MappingArray = [
"Member.MemberMemberships[0].Membership.SubscriberID",
"Member.PersonalInformation.LastName",
"Member.PersonalInformation.FirstName",
"TempObject.ReceivedDate",
"Member.ContactInformation[0].PhoneInformation[0].ContactNumber",
"Member.PersonalInformation.DOB"

]

$('.dateTimePicker').datetimepicker(
    {
        format: 'MM/DD/YYYY hh:mm:ss',
        widgetPositioning: {
            vertical: 'bottom'
        }
    });
$('.datePicker').datetimepicker({
    format: 'MM/DD/YYYY',
    widgetPositioning: {
        vertical: 'bottom'
    }
});

//Functions that Get Data Based on Need goes here
function GetMemberInfoforPreAuth() {
    if (memberData) {
        $('#preAuthMBRID').val(memberData.Member.MemberMemberships[0].Membership.SubscriberID);
        $('#preAuthMBRLastNameID').val(memberData.Member.PersonalInformation.LastName);
        $('#preAuthMBRFirstNameID').val(memberData.Member.PersonalInformation.FirstName);
        $('#requestDateID').val((new Date()).toLocaleString());
        $('#preAuthMBRPhoneNumberID').val(memberData.Member.ContactInformation[0].PhoneInformation[0].ContactNumber.formatTelephone());
        $('#preAuthMBRDOBID').val((new Date(memberData.Member.PersonalInformation.DOB)).toLocaleDateString());
    }
}

function GetRequestingProviderData() {
    if (memberData) {
        $("#ReqProviderFirstName").val(memberData.Provider.ContactName.split(' ')[0]);
        $("#ReqProviderLastName").val(memberData.Provider.ContactName.split(' ')[1]);
        $("#ReqProviderTaxID").val(memberData.Provider.GroupTaxId1);
        //$("#ReqProviderNPI").val()
        $("#ReqProviderContactName").val(memberData.Provider.ContactName);
        $("#ReqProviderPhoneNumber").val(memberData.Provider.PhoneNumber.formatTelephone());
        $("#ReqProviderFaxNumber").val(memberData.Provider.FaxNumber);
    }
}

function GetMemberInfoInViewforReqModal() {
    if (!$("#ReqTypeStandard").is(':checked')) {
        $("#ReqTypeStandard").prop('checked', true)
    }
    if (memberData) {
        $("#pavM_MBRID").text(memberData.Member.MemberMemberships[0].Membership.SubscriberID);
        $("#pavM_ReqDate").text((new Date()).toLocaleString());
        $("#pavM_LastName").text(memberData.Member.PersonalInformation.LastName);
        $("#pavM_FirstName").text(memberData.Member.PersonalInformation.FirstName);
        $("#pavM_PhoneNumber").text(memberData.Member.ContactInformation[0].PhoneInformation[0].ContactNumber.formatTelephone());
        $("#pavM_DOB").text((new Date(memberData.Member.PersonalInformation.DOB)).toLocaleDateString());
    }
}

function GetrequestingProviderDataforReqModal() {
    if (!$("#ReqProviderIsPCP").is(':checked')) {
        $("#ReqProviderIsPCP").prop('checked', true)
    }
    if (memberData) {
        $("#pavM_reqProvider_firstname").text(memberData.Provider.ContactName.split(' ')[0]);
        $("#pavM_reqProvider_lastname").text(memberData.Provider.ContactName.split(' ')[1]);
        $("#pavM_reqProvider_TaxID").text(memberData.Provider.GroupTaxId1);
        $("#pavM_reqProvider_contactName").text(memberData.Provider.ContactName);
        $("#pavM_reqProvider_phonenumber").text(memberData.Provider.PhoneNumber.formatTelephone());
        if (memberData.Provider.FaxNumber) $("#pavM_reqProvider_faxnumber").text(memberData.Provider.FaxNumber); else $("#pav_reqProvider_faxnumber").text('-');
    }
    if (!$("#ServiceReq_INOffice").is(':checked')) {
        $("#ServiceReq_INOffice").prop('checked', true)
    }
}


GetMemberInfoforPreAuth();
GetRequestingProviderData();

$('#QueueName').show().text('Portal');
/*Pre-Auth Request Function Called*/
$('#PreAuthRequestForm').off('click', '.req_preAuth_btn').on('click', '.req_preAuth_btn', function () {
    var formdata = $("#PreAuthRequestForm").serialize();
    //TabManager.openSideModal("/Home/PortalAuthorization", "PRIOR AUTHORIZATION", "cancel", "", "", "", formdata);
    TabManager.openFloatingModal("~/Areas/UM/Views/PortalAuth/PortalRequestModal/_Body.cshtml", "~/Areas/UM/Views/PortalAuth/PortalRequestModal/_Header.cshtml", "~/Areas/UM/Views/PortalAuth/PortalRequestModal/_Footer.cshtml", "White");
    setTimeout(GetMemberInfoInViewforReqModal, 1000);
    setTimeout(GetrequestingProviderDataforReqModal, 1000);
});
/*Pre-Auth Pend Function Called*/
$('#PreAuthRequestForm').off('click', '.pend_preAuth_btn').on('click', '.pend_preAuth_btn', function () {
    //$.ajax({
    //    type: 'post',
    //    url: '/Home/PendPoratlAuthorization',
    //    data: $('#UM_auth_form').serialize(),
    //    cache: false,
    //    error: function () {

    //    },
    //    success: function (data, textStatus, XMLHttpRequest) {
    //        $('#portalauth_preview_modal').html(data);
    //        showModal('PreAuthPendModal');
    //    }
    //});

    TabManager.openFloatingModal("~/Areas/UM/Views/PortalAuth/PortalPendModal/_Body.cshtml", "~/Areas/UM/Views/PortalAuth/PortalPendModal/_Header.cshtml", "~/Areas/UM/Views/PortalAuth/PortalPendModal/_Footer.cshtml");
    $('.modal-content').removeClass('modal-lg');
});
//Check the Type of Authorization
$('#PreAuthRequestForm').off('click', '.AuthTypeCheck').on('click', '.AuthTypeCheck', function (e) {
    var authType = $(this).attr('id');
    if (authType === 'expediteId') {
        e.preventDefault();
        TabManager.openFloatingModal('~/Areas/UM/Views/PortalAuth/ExpeditedCheckModal/_Body.cshtml', '~/Areas/UM/Views/PortalAuth/ExpeditedCheckModal/_Header.cshtml', '~/Areas/UM/Views/PortalAuth/ExpeditedCheckModal/_Footer.cshtml');
    }
    else {
        if (!$("#expediteMessageId").hasClass('hidden')) $("#expediteMessageId").addClass('hidden');
    }
})

//---check Requesting Provider Type---//
$('#PreAuthReqProvider').off('click', '.IsPcpType').on('click', '.IsPcpType', function (e) {
    if (($(this).attr('id')) === 'Specialist') {
        $('#Specialist').attr('checked', 'checked');
        $('#showSpecialistOptions').show();
    }
    else {
        $('#pcp').attr('checked', 'checked');
        $('#showSpecialistOptions').hide();
    }
})
//---End---//

$('body').on('click', '.close_modal_btn', function () {
    $modal_background = $('.modal_background'),
    $slide_modal = $modal_background.find('.slide_modal');
    $slide_modal.html('');
    $slide_modal.animate({ width: '0px' }, 400, 'swing', function () {
        $modal_background.remove();
    });
});

var ConfirmPreAuthRequest = function () {
    setTimeout(function () {
        TabManager.closeCurrentlyActiveSubTab();
        TabManager.navigateToTab({ "tabAction": "BJOY Queue", "tabTitle": "Barbara Joy Queue", "tabPath": "~/Areas/UM/Views/Queue/UMQueues/OwnQueue/_OwnSuperAdminQueue.cshtml", "tabContainer": "fullBodyContainer" });
    }, 1000);
}
var ConfirmExpedited = function () {
    if (!$("#expediteId").is(':checked')) {
        $("#expediteId").prop('checked', true)
        if ($("#expediteMessageId").hasClass('hidden')) $("#expediteMessageId").removeClass('hidden');
    }
}
//var CancelExpeditedConfirmation = function () {
//    if ($("#expediteId").is(':checked')) {
//        $("#expediteId").prop('checked', false);
//    };
//}


/*Document Upload Function*/
$(function () {
    $('#PreAuthDocumentArea').off('change', '.custom-file-input').on('change', '.custom-file-input', function () {
        var $row = $(this).parents('#PreAuthDocumentArea');
        var $row_attach = $(this).parents('.preAuthDocumentRow');
        var childCount = parseInt($row_attach.parent().children().length);
        if (childCount == 1) {
            $(this).parents('.preAuthDocumentRow').children('div:last').children('button:first').removeClass('hidden');
        }
        var rowIndex = parseInt($row_attach.index());
        $this = $row.find('.custom-file-input')[rowIndex];
        var fileName = $this.value;
        if (fileName == "") {
            return;
        }
        else {
            var aFile = fileName.split('fakepath')[1];
            var bFile = aFile.substring(aFile.indexOf(aFile) + 1);
            var file_text = bFile.toUpperCase();
            if ($(window).width() > 1800) {
                $(this).parents('.preAuthDocumentRow').find('.noFile').text(file_text.substr(0, 70));
                $(this).parents('.preAuthDocumentRow').find('.custom-file-input').prop('disabled', true);
            }
            else {
                $(this).parents('.preAuthDocumentRow').find('.noFile').text(file_text.substr(0, 50));
                $(this).parents('.preAuthDocumentRow').find('.custom-file-input').prop('disabled', true);
            }
            //$('.noFile').hide();
            //$(".custom-file-input[type='file']").css({'padding-right':'0%','text-transform':'uppercase'});
        }
    });
});

$(function () {

    //Code for Setting the Visibility of CPT Based on Service
    function SetCPTVisibility() {
        if ($(".PreAuthCPTDefault").hasClass('hidden') && !$(".PreAuthCPT2").hasClass('hidden')) {
            if ($(".visits-label").hasClass('hidden') && $(".visits-input").hasClass('hidden')) {
                $(".visits-label").removeClass('hidden')
                $(".visits-input").removeClass('hidden')
            }
            $(".PreAuthCPTDefault").removeClass('hidden');
            $(".PreAuthCPT2").addClass('hidden');
        } else {
            if (!$(".PreAuthCPTDefault").hasClass('hidden') && $(".PreAuthCPT2").hasClass('hidden')) {
                if ($(".visits-label").hasClass('hidden') && $(".visits-input").hasClass('hidden')) {
                    $(".visits-label").removeClass('hidden')
                    $(".visits-input").removeClass('hidden')
                }
            }
        }
    };

    //Code for Managing the Therapy Checks
    function ManageTherapyChecks() {
        if (!$(".therapychecks").hasClass('hidden')) {
            $(".therapychecks").children('div').each(function (i) {
                var checkbox = $(this).find('input.checkbox-radio')
                if (checkbox.is(':checked')) {
                    checkbox.prop('checked', false)
                }
            })
            $(".therapychecks").addClass('hidden')
        }
    };


    //HTML Template for DocumentType Options
    function getDocTypeOptions() {
        var optns = '<option value="">SELECT</option>' +
                    '<option value="CLINICAL">CLINICAL</option>' +
                    '<option value="PROGRESS NOTES">PROGRESS NOTES</option>' +
                    '<option value="FAX">FAX</option>' +
                    '<option value="LOA">LOA</option>' +
                    '<option value="PLAN AUTHORIZATION">PLAN AUTHORIZATION</option>'
        return optns;
    }



    $(".servicereqCheck").on('click', function () {
        var servicerequested = $(this).attr('id');
        if (servicerequested === 'InpatientHospital') {
            if (!$(".visits-label").hasClass('hidden') && !$(".visits-input").hasClass('hidden')) {
                $(".visits-label").addClass('hidden')
                $(".visits-input").addClass('hidden')
            }
            ManageTherapyChecks();
        }
        else if (servicerequested === 'HomeHealth' || servicerequested === 'RehabFacility' || servicerequested === 'Dialysis' || servicerequested === 'Therapy') {
            if (servicerequested === 'Therapy') {
                if ($(".therapychecks").hasClass('hidden')) {
                    $(".therapychecks").removeClass('hidden')
                }
            }
            else {
                ManageTherapyChecks();
            }
            if (!$(".PreAuthCPTDefault").hasClass('hidden') && $(".PreAuthCPT2").hasClass('hidden')) {
                $(".PreAuthCPTDefault").addClass('hidden');
                $(".PreAuthCPT2").removeClass('hidden');
            }
        }
        else {
            if (!(servicerequested === 'Physical' || servicerequested === 'Occupational' || servicerequested === 'Speech')) {
                SetCPTVisibility();
                ManageTherapyChecks();
            }
        }
    });
    /*ADD NEW ROW FOR PRIMARY DX*/
    $('#PrimaryDXArea').off('click', '.icd_add_btn').on('click', '.icd_add_btn', function () {
        var $row = $(this).parents('.PrimaryDXRow');
        var childCount = parseInt($row.parent().children().length);
        var rowIndex = parseInt($row.index());
        $(this).addClass('hidden')
        $('.icd_delete_btn').removeClass("hidden");
        var PrimaryDxRow = '<div class="PrimaryDXRow">' +
                           '<div class="col-lg-3 col-md-3 col-sm-8 col-xs-4">' +
                           '<input class="form-control input-xs icd_search_cum_dropdown icdCode mandatory_field_halo " placeholder="ICD CODE" name="ICDCodes[' + (rowIndex + 1) + '].ICDCode" />' +
                           '</div>' +
                           '<div class="col-lg-6 col-md-6 col-sm-12 col-xs-7">' +
                           '<input class="form-control input-xs icd_search_cum_dropdown icdDescription mandatory_field_halo " placeholder="DESCRIPTION" name="ICDCodes[' + (rowIndex + 1) + '].ICDDescription" />' +
                           '</div>' +
                           '<div class="col-lg-3 col-md-3 col-sm-2 col-xs-1 button-styles-xs">' +
                           '<button type="button" class="btn btn-danger btn-xs icd_delete_btn"><i class="fa fa-minus"></i></button>' +
                           '<button type="button" class="btn btn-success btn-xs icd_add_btn"><i class="fa fa-plus"></i></button>' +
                           '</div>' +
                           '<div class="clearfix"></div>' +
                           '</div>'
        if (childCount == rowIndex) {
            $row.parent().append(PrimaryDxRow);
        }
        else {
            $(PrimaryDxRow).insertAfter($row);
        }
    });
    /*DELETE ADDED ROW FOR PRIMARY DX*/
    function UpdateMapping(element, rowIndex) {
        $(element).each(function (i, e) {
            $(this).find('input,select').each(function (i, e) {
                var res = $(this)[0].name.split("[");
                res[0] = res[0] + "[";
                var index = parseInt(res[1].charAt(0));
                res[1] = res[1].substring(1);
                $(this)[0].name = (res[0] + (index - 1) + res[1]).toString();
            })
        })
    }
    //---Setting Add and Delete Button for CPT,ICD,Document----//
    var setAddDeleteButtonsVisibility = function (AddButton, DeleteButton, ParentID) {
        $($('#' + ParentID).find(AddButton)).addClass("hidden");
        $($('#' + ParentID).find(AddButton)).last().removeClass("hidden");
        if ($($('#' + ParentID).find(DeleteButton))) if ($($('#' + ParentID).find(DeleteButton)).length > 1) $($('#' + ParentID).find(DeleteButton)).removeClass("hidden");
        else $($('#' + ParentID).find(DeleteButton)).addClass("hidden");
    };
    $('#PrimaryDXArea').off('click', '.icd_delete_btn').on('click', '.icd_delete_btn', function () {
        var v = $(this).parents('.PrimaryDXRow').nextAll()
        var $row = $(this).parents('.PrimaryDXRow').remove();
        UpdateMapping(v);
        setAddDeleteButtonsVisibility(".icd_add_btn", ".icd_delete_btn", "PrimaryDXArea");
        //UpdateICDMapping();
        //var childCount = parseInt($row.parent().children().length);

        //if (childCount > 2) {
        //    if (childCount == (rowIndex + 1)) {
        //        $(this).parents('.PrimaryDXRow').prev().children('.button-styles-xs').children('.icd_add_btn').removeClass('hidden')
        //        $(this).parents('.PrimaryDXRow').remove();
        //    }
        //    else {
        //        $(this).parents('.PrimaryDXRow').remove();
        //    }
        //}
        //else {
        //    $(this).parents('.PrimaryDXRow').remove();
        //    $('.icd_delete_btn').addClass("hidden");
        //    $('.icd_add_btn').removeClass("hidden");
        //}
    });
    //-----------------------------------------------------------------------------------------------------------//
    $(".AddCPTRangeBtn_PreAuth").on("click", function () {
        TabManager.openSideModal('~/Areas/UM/Views/Common/Modal/_GetAddCPTRangeModal.cshtml', 'CPT Range', 'both');
    });
    /*ADD NEW ROW FOR CPT CODE SELECTION*/
    $('#PreAuthCPTArea').off('click', '.cpt_add_btn').on('click', '.cpt_add_btn', function () {
        var $row = $(this).parents('.PreAuthCPTRow');
        var childCount = parseInt($row.parent().children().length);
        var rowIndex = parseInt($row.index());
        $(this).addClass('hidden')
        $('.cpt_delete_btn').removeClass("hidden");

        var CPTRow = '<div class="col-lg-12 PreAuthCPTRow">' +
                                '<div class="col-lg-2 col-md-2">' +
                                    '<input class="form-control input-xs mandatory_field_halo " placeholder="CPT CODE" name="CPTCodes[' + (rowIndex + 1) + '].CPTCode">' +
                                '</div>' +
                                '<div class="col-lg-1 col-md-1">' +
                                    '<input class="form-control input-xs" placeholder="MODIFIER" name="CPTCodes[' + (rowIndex + 1) + '].Modifier">' +
                                '</div>' +
                                '<div class="col-lg-5 col-md-5">' +
                                    '<input class="form-control input-xs mandatory_field_halo " placeholder="DESCRIPTION" name="CPTCodes[' + (rowIndex + 1) + '].CPTDesc">' +
                                '</div>' +
                                '<div class="col-lg-2 col-md-2 visits-input">' +
                                    '<input class="form-control input-xs" placeholder="VISITS" name="CPTCodes[' + (rowIndex + 1) + '].RequestedUnits" value="1">' +
                                '</div>' +
                                '<div class="col-lg-2 col-md-2 button-styles-xs">' +
                                    '<button type="button" class="btn btn-danger btn-xs cpt_delete_btn"><i class="fa fa-minus"></i></button>' +
                                    '<button type="button" class="btn btn-success btn-xs cpt_add_btn"><i class="fa fa-plus"></i></button>' +
                                '</div>' +
                            '</div>'

        if (childCount == rowIndex) {
            $row.parent().append(CPTRow);
        }
        else {
            if ($row.children('.visits-input').hasClass('hidden')) {
                CPTRow = CPTRow.replace('visits-input', 'visits-input hidden');
            }
            $(CPTRow).insertAfter($row);
        }
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
                                        '<select class="form-control input-xs" name="CPTCodes[' + (rowIndex + 1) + '].RequestedUnits"><option value="">SELECT</option></select>' +
                                    '</div>' +
                                    '<div class="col-lg-1 col-md-1">' +
                                        '<input class="form-control input-xs" placeholder="NO PER" name="CPTCodes[' + (rowIndex + 1) + '].NumberPer">' +
                                    '</div>' +
                                    '<div class="col-lg-1 col-md-1">' +
                                        '<select class="form-control input-xs" name="CPTCodes[' + (rowIndex + 1) + '].NumberPerRange"><option value="">SELECT</option></select>' +
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
    /*CHECK TO OPEN Doc Area to Upload file*/
    $("#AddNewDocCheck").on("click", function () {
        if ($(this).is(':checked')) {
            if ($('.AddNewDocView').hasClass('hidden')) {
                $('.AddNewDocView').removeClass('hidden');
            }
        } else {
            $('.AddNewDocView').addClass('hidden');
        }
    });
    //-----------------------------------------------------------------------------------------------------------//

    /*Documents Functionality Goes Here*/
    $('#PreAuthDocumentArea').off('click', '.addDocumentBtn').on('click', '.addDocumentBtn', function () {
        var $row = $(this).parents('.preAuthDocumentRow');
        var childCount = parseInt($row.parent().children().length);
        var rowIndex = parseInt($row.index());
        $(this).addClass('hidden');
        $(this).parents('.preAuthDocumentRow').children('div:last').children('button:first').addClass('hidden');
        $('.deleteDocumentBtn').removeClass("hidden");
        var DocRow = '<div class="preAuthDocumentRow">' +
                                    '<div class="col-lg-3 col-md-3 col-sm-4 col-xs-2">' +
                                        '<input class="form-control input-xs mandatory_field_halo" id="Name" name="Name" placeholder="DOCUMENT NAME" type="text" value="">' +
                                    '</div>' +
                                    '<div class="col-lg-2 col-md-2 col-sm-3 col-xs-2">' +
                                        '<select class="form-control input-xs mandatory_field_halo" id="AttachmentType" name="AttachmentType" tabindex="-1">' + getDocTypeOptions() + '</select>' +
                                    '</div>' +
                                    '<div class="col-lg-5 col-md-5 col-sm-3 col-xs-2">' +
                                        '<input class="form-control custom-file-input input-xs" type="file" style="border:none;outline:none">' +
                                        '<div class="noFile hidden">NO FILE CHOSEN</div>' +
                                    '</div>' +
                                    '<div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">' +
                                        '<button id="previewButton" class="btn btn-ghost btn-xs this_button_preview" data-toggle="tooltip"><img src="/Resources/Images/Icons/previewIcon.png" style="width: 28px;"></button>' +
                                    '</div>' +
                                    '<div class="col-lg-1 col-md-1 col-sm-1 col-xs-1 pull-left button-styles-xs">' +
                                        '<button type="button" class="btn btn-danger btn-xs deleteDocumentBtn1 hidden"><i class="fa fa-minus"></i></button>' +
                                        '<button type="button" class="btn btn-danger btn-xs deleteDocumentBtn"><i class="fa fa-minus"></i></button>' +
                                        '<button type="button" class="btn btn-success btn-xs addDocumentBtn"><i class="fa fa-plus"></i></button>' +
                                    '</div>' +
                     '</div>'

        if (childCount == rowIndex) {
            $row.parent().append(DocRow);
        }
        else {
            $(DocRow).insertAfter($row);
        }
    }).off('click', '.deleteDocumentBtn').on('click', '.deleteDocumentBtn', function () {
        var $row = $(this).parents('.preAuthDocumentRow');
        var childCount = parseInt($row.parent().children().length);
        var rowIndex = parseInt($row.index());
        var v = $(this).parents('.preAuthDocumentRow').nextAll();//send all elements after Deleted element
        $(this).parents('.preAuthDocumentRow').remove();
        UpdateMapping(v);//Updating the Mappings
        setAddDeleteButtonsVisibility(".addDocumentBtn", ".deleteDocumentBtn", "PreAuthDocumentArea");//send Add button style and Delete Button Style For ADD and Minus button is visibility

        //if (childCount == 2 && $('.custom-file-input').val() != "") {
        //    if (rowIndex == 0) {
        //        $(this).parents('.preAuthDocumentRow').prev().children('div:last').children('button:first').removeClass('hidden');
        //        $('.deleteDocumentBtn1').removeClass("hidden");
        //    }
        //    else {
        //        $(this).parents('.preAuthDocumentRow').prev().children('div:last').children('button:first').removeClass('hidden');
        //    }
        //}
        //if (childCount > 2) {
        //    if (childCount == (rowIndex + 1)) {
        //        $(this).parents('.preAuthDocumentRow').prev().children('.button-styles-xs').children('.addDocumentBtn').removeClass('hidden')
        //        $(this).parents('.preAuthDocumentRow').remove();
        //    }
        //    else {
        //        $(this).parents('.preAuthDocumentRow').remove();
        //    }
        //}
        //else {
        //    $(this).parents('.preAuthDocumentRow').remove();
        //    $('.deleteDocumentBtn').addClass("hidden");
        //    $('.addDocumentBtn').removeClass("hidden");
        //}
    }).off('click', '.deleteDocumentBtn1').on('click', '.deleteDocumentBtn1', function () {
        var $row = $(this).parents('.preAuthDocumentRow');
        var childCount = parseInt($row.parent().children().length);
        var rowIndex = parseInt($row.index());
        $('.addDocumentBtn').trigger('click');
        $(this).parents('.preAuthDocumentRow').remove();
        $('.deleteDocumentBtn').addClass("hidden");
        $('.addDocumentBtn').removeClass("hidden");

    }).off('click', '.this_button_preview').on('click', '.this_button_preview', function () {
        var $row = $(this).parents('#PreAuthDocumentArea');
        var $row_attach = $(this).parents('.preAuthDocumentRow');
        var rowIndex = parseInt($row_attach.index());
        $this = $row.find('.custom-file-input')[rowIndex];
        readURL($this);

    });

    function readURL(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                window.open(e.target.result, '', 'width=' + screen.width + ',height=' + screen.height);
            }
            reader.readAsDataURL(input.files[0]);
        }
    }

    //-----------------------------------------------------------------------------------------------------------//
});
