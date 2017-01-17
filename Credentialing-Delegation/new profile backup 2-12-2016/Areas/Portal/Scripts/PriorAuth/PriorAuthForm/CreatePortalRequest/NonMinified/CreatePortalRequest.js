//Root Object for Portal all initializations goes here
var PortalRequestObject = {
    currentAuthType: "Standard",
    currentCPT: "TYPE1",
    Type1CPT: ["InOffice", "FreeStandingFacility", "Observation", "Hospice", "SNF", "OutPatientHospital"], //All these are having CPT section with 4 fields 
    Type2CPT: ["InpatientHospital"],//All these are having CPT section with 3 fields 
    Type3CPT: ["Dialysis", "RehabFacility", "DME", "HomeHealth", "Therapy"] //All these are having CPT section with More than 4 fields 
};
//Root Object for Portal all initializations goes here

// Method that determine if Changes are to be done in CPT section based on POS ID
function CheckCPTSectionChanges(POSId)
{
    return PortalRequestObject.currentCPT === CPTType(POSId); // Compares previous CPT Type  with currently selected POS CPT Type
}
function UpdateCurrentCPT(POSId)
{
    PortalRequestObject.currentCPT = CPTType(POSId);
}
function CPTType(POSId) {
    if (PortalRequestObject.Type2CPT.indexOf(POSId) > -1) return "Type2";
    else if (PortalRequestObject.Type3CPT.indexOf(POSId) > -1) return "Type3";
    else return "Type1";
}


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

var today = new Date();
var todayDate = moment(today).format('L');
var todayDateAndTime = moment(today).format("MM/DD/YYYY HH:mm:ss");
var differenceBetweenFromAndToDate = 90;
var toDate = (moment(today).add(differenceBetweenFromAndToDate, 'days')).format('L');

$("input[name='FromDate']").val(todayDate);
$("input[name='ToDate']").val(toDate);

// TOOLTIPS:
$('[data-toggle="tooltip"]').tooltip();
$('[data-toggle="popover"]').popover();

//Functions that Get Data Based on Need goes here
function GetMemberInfoforPreAuth() {
    if (memberData) {
        $('#preAuthMBRID').val(memberData.Member.MemberMemberships[0].Membership.SubscriberID);
        $('#preAuthMBRLastNameID').val(memberData.Member.PersonalInformation.LastName);
        $('#preAuthMBRFirstNameID').val(memberData.Member.PersonalInformation.FirstName);
        $('#requestDateID').val(todayDateAndTime);
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
//$('#PreAuthRequestForm').off('click', '.req_preAuth_btn').on('click', '.req_preAuth_btn', function () {
var submitData=function()
{
//var formdata = $("#PreAuthRequestForm").serialize();
    //TabManager.openSideModal("/Home/PortalAuthorization", "PRIOR AUTHORIZATION", "cancel", "", "", "", formdata);
    //TabManager.openFloatingModal("~/Areas/Portal/Views/PriorAuth/PriorAuthPreview/PriorAuthRequestModal/_Body.cshtml", "~/Areas/Portal/Views/PriorAuth/PriorAuthPreview/PriorAuthRequestModal/_Header.cshtml", "~/Areas/Portal/Views/PriorAuth/PriorAuthPreview/PriorAuthRequestModal/_Footer.cshtml", "White");
    setTimeout(GetMemberInfoInViewforReqModal, 1000); 
    setTimeout(GetrequestingProviderDataforReqModal, 1000);
}
    
//});
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

    TabManager.openFloatingModal("~/Areas/Portal/Views/PriorAuth/PendAuth/_Body.cshtml", "~/Areas/Portal/Views/PriorAuth/PendAuth/_Header.cshtml", "~/Areas/Portal/Views/PriorAuth/PendAuth/_Footer.cshtml");
    $('.modal-content').removeClass('modal-lg');
});
//Check the Type of Authorization
$('#PreAuthRequestForm').off('click', '.AuthTypeCheck').on('click', '.AuthTypeCheck', function (e) {
    var authType = $(this).attr('id');
    if (authType === 'expediteId' ) {
        e.preventDefault();
        if (PortalRequestObject.currentAuthType === "Standard")
        {
            TabManager.openFloatingModal('~/Areas/Portal/Views/PriorAuth/ExpeditedCheckModal/_Body.cshtml', '~/Areas/Portal/Views/PriorAuth/ExpeditedCheckModal/_Header.cshtml', '~/Areas/Portal/Views/PriorAuth/ExpeditedCheckModal/_Footer.cshtml');
        }
    }
    else {
        PortalRequestObject.currentAuthType = this.value;
        if (!$("#expediteMessageId").hasClass('hidden')) $("#expediteMessageId").addClass('hidden');
    }
})

//---check Requesting Provider Type---//
$('#PreAuthReqProvider').off('click', '.IsPcpType').on('click', '.IsPcpType', function (e) {
    if (($(this).attr('id')) === 'Specialist') {
        $('#Specialist').attr('checked', 'checked');
        $('#showSpecialistOptions').show();
        resetValues("#PreAuthReqProvider");
        $("#PreAuthReqProvider #ReqProviderFirstName").removeAttr('readonly');
        $("#PreAuthReqProvider #ReqProviderLastName").removeAttr('readonly');
        $("#PreAuthReqProvider #ReqProviderNPI").removeAttr('readonly');
        $("#PreAuthReqProvider #ReqProviderFirstName").removeClass('read_only_field');
        $("#PreAuthReqProvider #ReqProviderLastName").removeClass('read_only_field');
        $("#PreAuthReqProvider #ReqProviderNPI").removeClass('read_only_field');
        if ($("#isNotPCPApproved").is(':checked')) {
            $("#isPCPApproved").prop('checked', true)
            $("#isNotPCPApproved").removeAttr("checked");
        }
    }
    else {
        GetRequestingProviderData();
        $('#pcp').attr('checked', 'checked');
        $('#showSpecialistOptions').hide();
        $("#PreAuthReqProvider #ReqProviderFirstName").attr('readonly');
        $("#PreAuthReqProvider #ReqProviderLastName").attr('readonly');
        $("#PreAuthReqProvider #ReqProviderNPI").attr('readonly');
        $("#PreAuthReqProvider #ReqProviderFirstName").addClass('read_only_field');
        $("#PreAuthReqProvider #ReqProviderLastName").addClass('read_only_field');
        $("#PreAuthReqProvider #ReqProviderNPI").addClass('read_only_field');
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
        $("#expediteId").prop('checked', true);
        PortalRequestObject.currentAuthType = $("#expediteId").val();
        if ($("#expediteMessageId").hasClass('hidden')) $("#expediteMessageId").removeClass('hidden');
    }
}

//To reset the values of input fields to null
var resetValues = function(id){
    $(id + " :input").val("");
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
        var POSID = $(this).attr('id');
        if (!CheckCPTSectionChanges(POSID))
        {
            UpdateCurrentCPT(POSID);
            $.ajax({
                type: 'post',
                url: '/PriorAuth/ChangeCPTSection',
                data: { type: PortalRequestObject.currentCPT },
                cache: false,
                error: function () {

                },
                success: function (data, textStatus, XMLHttpRequest) {                   
                    $('#CPTBodySection').html(data);
                }
            });
        }

        //if (servicerequested === 'InpatientHospital') {
        //    if (!$(".visits-label").hasClass('hidden') && !$(".visits-input").hasClass('hidden')) {
        //        $(".visits-label").addClass('hidden')
        //        $(".visits-input").addClass('hidden')
        //    }
        //    ManageTherapyChecks();
        //}
        //else if (servicerequested === 'HomeHealth' || servicerequested === 'RehabFacility' || servicerequested === 'Dialysis' || servicerequested === 'Therapy') {
        //    if (servicerequested === 'Therapy') {
        //        if ($(".therapychecks").hasClass('hidden')) {
        //            $(".therapychecks").removeClass('hidden')
        //        }
        //    }
        //    else {
        //        ManageTherapyChecks();
        //    }
        //    if (!$(".PreAuthCPTDefault").hasClass('hidden') && $(".PreAuthCPT2").hasClass('hidden')) {
        //        $(".PreAuthCPTDefault").addClass('hidden');
        //        $(".PreAuthCPT2").removeClass('hidden');
        //    }
        //}
        //else {
        //    if (!(servicerequested === 'Physical' || servicerequested === 'Occupational' || servicerequested === 'Speech')) {
        //        SetCPTVisibility();
        //        ManageTherapyChecks();
        //    }
        //}
    });
   
    //-----------------------------------------------------------------------------------------------------------//
    $(".AddCPTRangeBtn_PreAuth").on("click", function () {
        TabManager.openSideModal('~/Areas/UM/Views/Common/Modal/_GetAddCPTRangeModal.cshtml', 'CPT Range', 'both');
    });
    



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
                                        '<input class="form-control input-xs mandatory_field_halo" id="Name" name="Attachments[' + (rowIndex + 1) + '].Name" placeholder="DOCUMENT NAME" type="text" value="">' +
                                    '</div>' +
                                    '<div class="col-lg-2 col-md-2 col-sm-3 col-xs-2">' +
                                        '<select class="form-control input-xs mandatory_field_halo" id="AttachmentType" name="Attachments[' + (rowIndex + 1) + '].AttachmentType" tabindex="-1">' + getDocTypeOptions() + '</select>' +
                                    '</div>' +
                                    '<div class="col-lg-5 col-md-5 col-sm-3 col-xs-2">' +
                                        '<input class="form-control custom-file-input input-xs" type="file" name="Attachments[' + (rowIndex + 1) + '].Path" style="border:none;outline:none">' +
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
