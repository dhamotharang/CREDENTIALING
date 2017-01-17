//$(function () {

//    InitICheckFinal();
//    /*------------------------------------------------------------------------------------*/
//    $('#QueueName').show().text('Intake');

//    $('.noFile').show();

//    $('#document_section').off('change', '.custom-file-input').on('change', '.custom-file-input', function () {
//        var $row = $(this).parents('#document_section');
//        var $row_attach = $(this).parents('.doc_section');
//        var childCount = parseInt($row_attach.parent().children().length);
//        if (childCount == 2) {
//            $(this).parents('.doc_section').children('div:last').children('button:first').removeClass('hidden');
//        }
//        var rowIndex = parseInt($row_attach.index());
//        var $this = $row.find('.custom-file-input')[rowIndex - 1];
//        var fileName = $this.value;
//        if (fileName == "") {
//            return;
//        }
//        else {
//            var aFile = fileName.split('fakepath')[1];
//            var bFile = aFile.substring(aFile.indexOf(aFile) + 1);
//            var file_text = bFile.toUpperCase();
//            if ($(window).width() > 1800) {
//                $(this).parents('.doc_section').find('.noFile').text(file_text.substr(0, 60));
//                //  $(this).parents('.doc_section').find('.custom-file-input').prop('disabled', true);
//            }
//            else {
//                $(this).parents('.doc_section').find('.noFile').text(file_text.substr(0, 36));
//                // $(this).parents('.doc_section').find('.custom-file-input').prop('disabled', true);
//            }
//       }
//    });
//});

// --------Function used to get the data for document name and placing it in the search drop down-------

//GetMasterDataDocumentNames();
////var DocNames = ["Hospital Records", "Specialist Records", "Office Facesheet", "Plan Authorization"];

//function GetMasterDataDocumentNames() {
//    var DocNames = [];
//    $.ajax({
//        type: 'GET',
//        url: '/UM/Authorization/GetDocumentNames',
//        error: function () {
//        },
//        success: function (info) {
//            if (info.data.length > 0) {
//                for (var index = 0; index < info.data.length; index++) {
//                    DocNames.push(info.data[index].DocumentNameValue);
//                }
//            }
//            $(".documentName").select2({
//                data: DocNames
//});
//        }
//    });
//}

$('#UM_auth_form').off('blur', 'input[name=FromDate]').on('blur', 'input[name=FromDate]', function () {
    var FromDate = $('input[name = FromDate]').val();
    var toDate = FromDate ? (moment(FromDate).add(90, 'days')).format('L') : '';
    $("input[name='ToDate']").val(toDate);
});

$('#UM_auth_form').off('blur', 'input[name=ReceivedDate]').on('blur', 'input[name=ReceivedDate]', function () {
    $("input[name='Admission.AdmissionRequestedDate']").val($('input[name = ReceivedDate]').val());
});

$('#UM_auth_form').off('blur', 'input[name=ExpectedDOS]').on('blur', 'input[name=ExpectedDOS]', function () {
    var add = FindAddByPOS();
    ExpDOSRelatedDate(add);
});

$('#UM_auth_form').off('blur', 'input[name=NextReviewDate]').on('blur', 'input[name=NextReviewDate]', function () {
    var NextReviewDate = $("input[name='NextReviewDate']").val();
    var AdmDischargeDate = NextReviewDate ? (NextReviewDate + ' ' + getCurrentTime()) : '';
    $("input[name='Discharge.ExpectedDischargeDate']").val(AdmDischargeDate);
    calculatereqlos();
});

$('#UM_auth_form').off("blur", "input[name='Admission.AdmissionFromDate']").on("blur", "input[name='Admission.AdmissionFromDate']", function () {
    calculatereqlos();
    calculateactuallos();
});

$('#UM_auth_form').off("blur", "input[name='Discharge.ExpectedDischargeDate']").on("blur", "input[name='Discharge.ExpectedDischargeDate']", function () {
    calculatereqlos();
});

function ExpDOSRelatedDate(add)
{
    var ExpDOS = $('input[name = ExpectedDOS]').val();
    var OPType = $("select[name='OutPatientType']").val();
    if (OPType && (OPType.toUpperCase() == "OP PROCEDURE" || OPType.toUpperCase() == "OP DIAGNOSTIC"))
    {
        $("input[name='NextReviewDate']").val('');
        $("input[name='Admission.AdmissionFromDate']").val('');
        $("input[name='Discharge.ExpectedDischargeDate']").val('');
        $("input[name='TotalRequestedLOS']").val('');
        $("input[name='TotalActualLOS']").val('');
        $("input[name='Admission.AdmissionRequestedDate']").val('');
    }
    else
    {
        var NextReviewDate = ExpDOS ? (moment(ExpDOS).add(add, 'days')).format('L') : '';
        $("input[name='NextReviewDate']").val(NextReviewDate);
        var AdmFromDate = ExpDOS ? (moment(ExpDOS).format("L") + ' ' + getCurrentTime()) : '';
        $("input[name='Admission.AdmissionFromDate']").val(AdmFromDate);
        var AdmDischargeDate = NextReviewDate ? (NextReviewDate + ' ' + getCurrentTime()) : '';
        $("input[name='Discharge.ExpectedDischargeDate']").val(AdmDischargeDate);
        calculatereqlos();
        calculateactuallos();
    }
}

function ExpDOSRelatedDateWithToDayDate(add) {
    var ExpDOS = moment(new Date()).format("L");
    $('input[name = ExpectedDOS]').val(ExpDOS)
    var OPType = $("select[name='OutPatientType']").val();
    if (OPType && (OPType.toUpperCase() == "OP PROCEDURE" || OPType.toUpperCase() == "OP DIAGNOSTIC")) {
        $("input[name='NextReviewDate']").val('');
        $("input[name='Admission.AdmissionFromDate']").val('');
        $("input[name='Discharge.ExpectedDischargeDate']").val('');
        $("input[name='TotalRequestedLOS']").val('');
        $("input[name='TotalActualLOS']").val('');
        $("input[name='Admission.AdmissionRequestedDate']").val('');
    }
    else {
        var NextReviewDate = ExpDOS ? (moment(ExpDOS).add(add, 'days')).format('L') : '';
        $("input[name='NextReviewDate']").val(NextReviewDate);
        var AdmFromDate = ExpDOS ? (moment(ExpDOS).format("L") + ' ' + getCurrentTime()) : '';
        $("input[name='Admission.AdmissionFromDate']").val(AdmFromDate);
        var AdmDischargeDate = NextReviewDate ? (NextReviewDate + ' ' + getCurrentTime()) : '';
        $("input[name='Discharge.ExpectedDischargeDate']").val(AdmDischargeDate);
        calculatereqlos();
        calculateactuallos();
    }
}


function UpdateAllDates()
{
    var FromDate = $('input[name = FromDate]').val();
    if(!FromDate)
    {
        $('input[name = FromDate]').val(moment(new Date()).format('L'));
        FromDate = $('input[name = FromDate]').val();
        var toDate = FromDate ? (moment(FromDate).add(90, 'days')).format('L') : '';
        $("input[name='ToDate']").val(toDate);
    }
    var OPType = $("select[name='OutPatientType']").val();
    if (OPType && (OPType.toUpperCase() == "OP PROCEDURE" || OPType.toUpperCase() == "OP DIAGNOSTIC"))
    {
        $("#AdmissionSection22Type :input").attr("readonly", "readonly");
        document.getElementById("coMagChk").style.opacity = "0";
        $("input[name='Admission.AdmissionRequestedDate']").val('');
    }
    else
    {
        $("input[name='Admission.AdmissionRequestedDate']").val($('input[name = ReceivedDate]').val());
        $("#AdmissionSection22Type :input").removeAttr('readonly');
    }
    var add = FindAddByPOS();
    ExpDOSRelatedDateWithToDayDate(add);
}

function getCurrentTime()
{
    var today = moment(new Date()).format('MM/DD/YYYY HH:mm:ss');
    return today.substring(11);
}

function FindAddByPOS()
{
    var pos = $("select[name='PlaceOfService']").val().substring(0, 2);
    if (pos == "31" || pos == "61")
        return 7;
    else if (pos == "22")
        return 2;
    else
        return 3;
}

function calculatereqlos()
{
    var AdmFromDate = $("input[name='Admission.AdmissionFromDate']").val();
    var ExpDischargeDate = $("input[name='Discharge.ExpectedDischargeDate']").val();
    if(AdmFromDate && ExpDischargeDate)
    {
        AdmFromDate = moment(AdmFromDate).format("L");
        ExpDischargeDate = moment(ExpDischargeDate).format("L");
        var diffDays = GetDateDifference(AdmFromDate, ExpDischargeDate);
        diffDays = (diffDays > 0) ? diffDays : 0;
        $("input[name='TotalRequestedLOS']").val(diffDays);
    }
    else
        $("input[name='TotalRequestedLOS']").val('');
}

function calculateactuallos() {
    var AdmFromDate = $("input[name='Admission.AdmissionFromDate']").val();
    if (AdmFromDate) {
        AdmFromDate = moment(AdmFromDate).format("L");
        var currentDate = moment(new Date()).format("L");;
        var diffDays = GetDateDifference(AdmFromDate, currentDate);
        diffDays = (diffDays > 0) ? diffDays : 0;
        $("input[name='TotalActualLOS']").val(diffDays);
    }
    else
        $("input[name='TotalActualLOS']").val('');
}

function GetDateDifference(d1,d2)
{
    var date1 = new Date(d1);
    var date2 = new Date(d2);
    var timeDiff = date2.getTime() - date1.getTime();
    var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24));
    return diffDays;
}
   
//-------------ends-------------------
///*CHECK TO OPEN Doc Area to Upload file*/
//$('#UM_auth_form').off('click', '#AddNewDocCheck').on('click', '#AddNewDocCheck', function () {
////$("#AddNewDocCheck").on("click", function () {
//    if ($(this).is(':checked')) {
//        if ($('#document_section').hasClass('hidden')) {
//            $('#document_section').removeClass('hidden');
//            var doc_Template = '<div class="x_content doc_section zero-padding-left-right"  style="padding-bottom: 0;">' +
//          '<div class="col-lg-3 col-md-3 col-sm-4 col-xs-2 zero-padding-left-right">' +
//           '<select class="form-control input-xs mandatory_field_halo  loser_field documentName"name="Attachments[0].Name">' +
//           '<option value="">Select</option>' +
//           '</select>' +
         
//          '</div>' +
//          '<div class="col-lg-2 col-md-2 col-sm-3 col-xs-2">' +
//          '<select class="form-control input-xs mandatory_field_halo loser_field documentTypeName" id="AttachmentType" name="Attachments[0].AttachmentTypeName">' +
//          '<option value="">SELECT</option>' +
//          '<option value="Clinical">CLINICAL</option>' +
//        '<option value="Progress Notes">PROGRESS NOTES</option>' +
//        '<option value="Fax">FAX</option>' +
//            '<option value="LOA">LOA</option>' +
//        '<option value="Plan Authorization">PLAN AUTHORIZATION</option>' +
//            '</select>' +
//          '</div>' +
//          '<div class="col-lg-4 col-md-4 col-sm-3 col-xs-2">' +
//          '<input class="form-control custom-file-input input-xs" type="file" name="Attachments[0].DocumentFile">' +
//          '<div class="noFile">NO FILE CHOSEN</div>' +
//          '</div>' +
//          '<div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">' +
//          '<button id="previewButton" class="btn btn-ghost btn-xs this_button_preview" data-toggle="tooltip" tabindex="-1"><img src="/Resources/Images/Icons/previewIcon.png" style="width: 28px;" /></button>' +
//          '</div>' +
//          '<div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">' +
//          '<input type="checkbox" class="checkbox-radio includeFax" id="IncludeFax[0]" tabindex="-1" name="Attachments[0].IncludeFax" value="IncFax"><label class="includeFaxLabel" for="IncludeFax[0]"><span></span></label>' +
//          '<input name="IncludeFax" type="hidden" value="false">' +
//          '</div>' +
//          '<div class="col-lg-1 col-md-1 col-sm-1 col-xs-1 pull-left">' +
//           '<button class="btn btn-danger btn-xs delete_document_btn1 hidden" tabindex="-1"><i class="fa fa-minus"></i></button>'+
//            '<button class="btn btn-danger btn-xs delete_document_btn hidden " tabindex="-1"><i class="fa fa-minus"></i></button>'+
//            '<button class="btn btn-success btn-xs add_document_btn loser_field"><i class="fa fa-plus"></i></button>'+
//          '</div>' +
//          '</div>';
//            GetMasterDataDocumentNames();
//            $(doc_Template).insertAfter("#documentLabels");
//        }
//    } else {
//        $('#document_section').addClass('hidden');
//        $('.doc_section').remove();
//    }
//});
////-----------------------------------------------------------------------------------------------------------//

//TRIGGER CLOSING OF MODAL
function CloseModalManually(slidemodal, modalbackground) {
    event.preventDefault();
    setTimeout(function () {
        $('.' + slidemodal).html('');
        $('.' + slidemodal).animate({ width: '0px' }, 400, 'swing', function () {
            $('.' + modalbackground).remove();
        });
    }, 500)
}

/*NEG FEE Modal- SETTING MIN-WIDTH */
function SetNegFeeModalWidth() {
    $("#SharedFloatingModalContent").css({
        "min-width": "1100px"
    });
  //  $('#ReqLosForDrgOrDiem').val(RequestedLos);

}

$("#UM_auth_form").off('change', '.LevelRate').on('change', '.LevelRate', function () {
    if ($(".LevelRate").val() === "NEG FEE") {
        var pos = $("#PlaceOfService").val().split('-')[0];
       // var RequestedLos = $('#TotalRequestedLOS').val();
        //  TabManager.openCenterModal("/UM/Authorization/GetNegFeePartial", "NEG FEE");
        TabManager.openFloatingModal('/UM/Authorization/GetNegFeePartial?pos=' + pos, '~/Areas/UM/Views/Common/Modal_Header_Footer/NegFee/Header/_NegFeeHeader.cshtml', '~/Areas/UM/Views/Common/Modal_Header_Footer/NegFee/Footer/_NegFeeFooter.cshtml', ' ', 'SetNegFeeModalWidth', '');
    }
    var LevelRate_Value = $('#LevelRate').val();
    if (LevelRate_Value != "NEG FEE") {
        $('#negFeeArea').empty();
    }
});

/*------------------------------------------------------------------------------------*/

var memberData = TabManager.getMemberData();
//setMemberHeaderData(memberData);

InitICheckFinal();
setAllFieldsWidth();
(function () {
    $('.dateTimePicker').datetimepicker(
        {
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
    $('#UM_auth_form').on('change', '#FromDate', function () {
    
    });
    function initCreateAuth() {
        //Closure to write all the initial setup Code in CreateAuth
        //variables to assign value
        var today = new Date();
        var todayDate = moment(today).format('L');
        var todayDateAndTime = moment(today).format("MM/DD/YYYY HH:mm:ss");
        var differenceBetweenFromAndToDate = 90;
        var toDate = (moment(today).add(differenceBetweenFromAndToDate, 'days')).format('L');

        //Initializing PCP data 
       // $("#PCP_FullName").val(memberData.Provider.ContactName);
       

        $("input[name='ReceivedDate']").val(todayDateAndTime);
        $("input[name='FromDate']").val(todayDate);
        $("input[name='ToDate']").val(toDate);
    }
    initCreateAuth();

    //-----authTypeCms--typeOfCareCms--unSvcGrpCms--levelOfCareCms--documentNameCms

  
    $('#UM_auth_form').off('click', '.authTypeCms').on('click', '.authTypeCms', function () {
        TabManager.openCenterModal('/CMSData/AuthorizationType', 'Authorization Type');
    });
    $('#UM_auth_form').off('click', '.typeOfCareCms').on('click', '.typeOfCareCms', function () {
        TabManager.openCenterModal('/CMSData/TypeOfCares', 'Type Of Care');
    });
    $('#UM_auth_form').off('click', '.unSvcGrpCms').on('click', '.unSvcGrpCms', function () {
        TabManager.openCenterModal('/CMSData/UMServiceGroup', 'UM Service Group');
    });
    $('#UM_auth_form').off('click', '.levelOfCareCms').on('click', '.levelOfCareCms', function () {
        TabManager.openCenterModal('/CMSData/LevelOfCare', 'Level Of Care');
    });
    $('#UM_auth_form').off('click', '.roomTypeCms').on('click', '.roomTypeCms', function () {
        TabManager.openCenterModal('/CMSData/POSRoomTypes', 'POS Room Types');
    });
    $('#document_section').off('click', '.documentNameCms').on('click', '.documentNameCms', function () {
        TabManager.openCenterModal('/CMSData/DocumentName', 'Document Name');
    });
    $('#document_section').off('click', '.documentTypeCms').on('click', '.documentTypeCms', function () {
        TabManager.openCenterModal('/CMSData/DocumentType', 'Document Type');
    });
   


    (function listenerStubs() {
        var oneSecond = 1000;
        $('#ODAGArea').off('click', '#collapse-link-odag').on('click', '#collapse-link-odag', function () {
            $("#ODAGmainDiv").toggleClass("displayNone");
            $("#ODAGChevron").removeClass();
            if ($("#ODAGmainDiv").is(':visible'))
                $("#ODAGChevron").addClass('fa fa-chevron-up');
            else
                $("#ODAGChevron").addClass('fa fa-chevron-down');

            //Clearing the odag data
            $(".closedateElement").click(function () {
                var element = this.id.split("_")[0];
                $("input[name='" + element+"']").val(null);
            });
            $(".closeFieldElement").click(function () {
                var element = this.id.split("_")[0];
                $("input[name='" + element + "']").last().click();
            });
            $(".closeFieldWithDateElement").click(function () {
                var element = this.id.split("_")[0];
                $("input[name='" + element + "']").last().click();
            });
            //End of logic of clearing the odag data
            //Setting the plan name in create
            //in future need to put the last element programatically not by just number 17
            $("input[name='ODAGs[17].OptionAnswer']").val($("#member-plan-name-um").html())
            //end of settign the plan name in create

            $('#odagQuestionContent input[type="radio"]').change(function () {
                if (this.value == "Y") {
                    $("input[name='" + this.name.split(".")[0] + ".OptionDate']").removeClass("displayNone");
                } else {
                    $("input[name='" + this.name.split(".")[0] + ".OptionDate']").val(null);
                    $("input[name='" + this.name.split(".")[0] + ".OptionDate']").addClass("displayNone");
                }
            });
        });
        $('#providerPanel').off('click', '.pcpLabelReqProvider').on('click', '.pcpLabelReqProvider', function () {
            $("#RequestingProvider_FullName").val(memberData.Provider.ContactName);
        });
        $('#providerPanel').off('click', '.mbrLabelReqProvider').on('click', '.mbrLabelReqProvider', function () {
            $("#RequestingProvider_FullName").val(memberData.Member.PersonalInformation.FirstName + " " + memberData.Member.PersonalInformation.LastName);
        });
        $('#providerPanel').off('click', '.pcpLabelSVCProvider').on('click', '.pcpLabelSVCProvider', function () {
            $("#ServiceProvider_FullName").val(memberData.Provider.ContactName);
        });



    })()

})();



/* CONTACTS MANAGEMNENT*/

//-------Plain Language Letter Preview---Start--
var cptdataobj = {};
var cptPreviewLetterBtn = function () {
    cptdataobj = $("#CPTCodes").children().children().find('.plainLang').text().trim().split("MG");
    TabManager.openSideModal('~/Areas/UM/Views/Common/Letter/_ApprovalLetter.cshtml', 'Approval Letter', 'both', 'print', '', '');
};

// ------- Preview Authorization -----------
$('#authChangeButtons').off('click', '.referapprove_auth_button').on('click', '.referapprove_auth_button', function () {
    if (!$('#authChangeButtons .create-auth-btn').hasClass('disabled')) {
        $('#authChangeButtons .create-auth-btn').addClass('disabled');
        var $form = $("#UM_auth_form");
        var formData = new FormData($form[0]);
        var AuthorizationEvent = $(this).attr('value');
        // var form = $("#UM_auth_form").serialize();
        //var formData = new FormData($form[0]);

        //TabManager.openSideModal('/UM/AuthorizationPreview/SetAuthPreviewData', 'Plain Language', 'both', '', '', '', form);
        //TabManager.openFloatingModal('/UM/AuthorizationPreview/SetAuthPreviewData', '~/Areas/UM/Views/Common/AuthPreview/_AuthPreviewModalHeader.cshtml', '~/Areas/UM/Views/Common/Modal_Header_Footer/NegFee/Footer/_NegFeeFooter.cshtml', '', '', form);
        $.ajax({
            type: 'POST',
            url: '/AuthorizationPreview/SetCreateAuthPreview?CreateEvent=' + AuthorizationEvent,
            data:formData,
            processData: false,
            contentType: false,
            cache: false,
            error: function () {
            },
            success: function (data) {
                $('#auth_preview_modal').html(data);
                showModal('authPreviewModal');
            }
           
        }).always(function(){
        
                $('#authChangeButtons .create-auth-btn').removeClass('disabled');
        
        });

    }
});

$('#authChangeButtons').off('click', '.pend_auth-button').on('click', '.pend_auth-button', function () {
    if (!$('#authChangeButtons .create-auth-btn').hasClass('disabled')) {
        $('#authChangeButtons .create-auth-btn').addClass('disabled');
        TabManager.openFloatingModal('~/Areas/UM/Views/Common/AuthPendModal/_AuthPendModalBody.cshtml', '~/Areas/UM/Views/Common/AuthPendModal/_AuthPendModalHeader.cshtml', '~/Areas/UM/Views/Common/AuthPendModal/_AuthPendModalFooter.cshtml', '', '');
        $('#authChangeButtons .create-auth-btn').removeClass('disabled');
        setTimeout(function () {
            setAllFieldsWidth();
        }, 1000);
    }
});

//$('#authChangeButtons').off('click', '.create-auth-btn').on('click', '.create-auth-btn', function () {
//    $('#authChangeButtons .create-auth-btn').addClass('disabled');
 
//});
//$('.modal-header, .modal-footer').off('click', '.cancel-createauth-btn').on('click', '.cancel-createauth-btn', function () {
//    $('#authChangeButtons .create-auth-btn').removeClass('disabled');
//});



//-------Plain Language Letter Preview---End----

/* /ICD CODES MANAGEMENT */
var createAuthorizations = function () {
    //var form = $('#UM_auth_form');
    //form.removeData("validator").removeData("unobtrusiveValidation");
    //$.validator.unobtrusive.parse(form);
    //if (form.valid()) {
    var $form = $("#UM_auth_form");
    var formData = new FormData($form[0]);
    $.ajax({
        type: 'POST',
        url: '/UM/Authorization/Authorization',
        data: formData,
        processData: false,
        contentType: false,
        cache: false,
        error: function () {
        },
        success: function (data) {
            $('#auth_preview_modal').html(data);
            showModal('authPreviewModal');
        }
    });
    //}
};

$("#authChangeButtons").off("click", "#createapprovebutton").on("click", "#createapprovebutton", function () {

    var $form = $("#UM_auth_form");
    var formData = new FormData($form[0]);
    $.ajax({
        type: 'POST',
        url: '/UM/Authorization/Authorization',
        data: formData,
        processData: false,
        contentType: false,
        cache: false,
        error: function () {
        },
        success: function (data) {
            $('#auth_preview_modal').html(data);
            showModal('authPreviewModal');
        }
    });

})






/* ATTACHMENTS MANAGEMENT*/
//$('#document_section').off('click', '.add_document_btn').on('click', '.add_document_btn', function () {
//    var $row = $(this).parents('.doc_section');
    
//    var childCount = parseInt($row.parent().children().length);
    
//    var rowIndex = parseInt($row.index());


//    $(this).parents('.doc_section').children('div:last').children('button:first').addClass('hidden');
//    $(this).addClass('hidden');
//    $('.delete_document_btn').removeClass("hidden");

//    var doc_Template = '<div class="x_content doc_section zero-padding-left-right"  style="padding-bottom: 0;">' +
//              '<div class="col-lg-3 col-md-3 col-sm-4 col-xs-2 zero-padding-left-right">' +
//               '<select class="form-control input-xs mandatory_field_halo  loser_field documentName"name="Attachments[' + (rowIndex) + '].Name">' +
//               '<option value="">Select</option>'+
//               '</select>'+
//              '</div>' +
//              '<div class="col-lg-2 col-md-2 col-sm-3 col-xs-2">' +
//              '<select class="form-control input-xs mandatory_field_halo loser_field documentTypeName" id="AttachmentType" name="Attachments[' + (rowIndex) + '].AttachmentTypeName">' +
//              '<option value="">SELECT</option>' +
//              '<option value="Clinical">CLINICAL</option>' +
//            '<option value="Progress Notes">PROGRESS NOTES</option>' +
//            '<option value="Fax">FAX</option>' +
//                '<option value="LOA">LOA</option>' +
//            '<option value="Plan Authorization">PLAN AUTHORIZATION</option>' +
//                '</select>' +
//              '</div>' +
//              '<div class="col-lg-4 col-md-4 col-sm-3 col-xs-2">' +
//              '<input class="form-control custom-file-input input-xs" type="file" name="Attachments[' + (rowIndex) + '].DocumentFile">' +
//              '<div class="noFile">NO FILE CHOSEN</div>' +
//              '</div>' +
//              '<div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">' +
//              '<button id="previewButton" class="btn btn-ghost btn-xs this_button_preview" data-toggle="tooltip" tabindex="-1"><img src="/Resources/Images/Icons/previewIcon.png" style="width: 28px;" /></button>' +
//              '</div>' +
//              '<div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">' +
//              '<input type="checkbox" class="checkbox-radio includeFax" id="IncludeFax[' + (rowIndex) + ']" tabindex="-1" name="Attachments[' + (rowIndex) + '].IncludeFax" value="IncFax"><label class="includeFaxLabel" for="IncludeFax[' + (rowIndex) + ']"><span></span></label>' +
//              '<input name="IncludeFax" type="hidden" value="false">' +
//              '</div>' +
//              '<div class="col-lg-1 col-md-1 col-sm-1 col-xs-1 pull-left">' +
//              '<button class="btn btn-danger btn-xs delete_document_btn1 hidden" tabindex="-1"><i class="fa fa-minus"></i></button>' +
//              '<button class="btn btn-danger btn-xs delete_document_btn" tabindex="-1"><i class="fa fa-minus"></i></button>' +
//              '<button class="btn btn-success btn-xs add_document_btn loser_field"><i class="fa fa-plus"></i></button>' +
//              '</div>' +
//              '</div>';
//    GetMasterDataDocumentNames();

//    if (childCount == rowIndex) {
//        $row.parent().append(doc_Template);
//    }
//    else {
//        $(doc_Template).insertAfter($row);
//    }

//}).off('click', '.delete_document_btn').on('click', '.delete_document_btn', function () {
//    var $row = $(this).parents('.doc_section');
//    var childCount = parseInt($row.parent().children().length);
//    var rowIndex = parseInt($row.index());
//    if (childCount == 3 && $('.custom-file-input').val() != "") {
//        if (rowIndex == 1) {
//            $(this).parents('.doc_section').prev().children('div:last').children('button:first').removeClass('hidden');
//            $('.delete_document_btn1').removeClass("hidden");
//        }
//        else {
//            $(this).parents('.doc_section').prev().children('div:last').children('button:first').removeClass('hidden');
//        }
//    }
//    if (childCount > 3) {
//        if (childCount == (rowIndex + 1)) {
//            $(this).parents('.doc_section').prev().children('div:last').children('button:last').removeClass('hidden');
//            $(this).parents('.doc_section').remove();
//        }
//        else {
//            $(this).parents('.doc_section').remove();
//        }
//    }
//    else {
//        $(this).parents('.doc_section').remove();
//        $('.delete_document_btn').addClass("hidden");
//        $('.add_document_btn').removeClass("hidden");
//    }
//    updateDocumentMapping();
//}).off('click', '.delete_document_btn1').on('click', '.delete_document_btn1', function () {
//    var $row = $(this).parents('.doc_section');
//    var childCount = parseInt($row.parent().children().length);
//    var rowIndex = parseInt($row.index());
//    $('.add_document_btn').trigger('click');
//    $(this).parents('.doc_section').remove();
//    $('.delete_document_btn').addClass("hidden");
//    $('.add_document_btn').removeClass("hidden");
//    updateDocumentMapping();
//}).off('click', '.this_button_preview').on('click', '.this_button_preview', function () {
//    var $row = $(this).parents('#document_section');
//    var $row_attach = $(this).parents('.doc_section');
//    var rowIndex = parseInt($row_attach.index());
//    var $this = $row.find('.custom-file-input')[rowIndex - 1];
//    readURL($this);
//});

/* Document Index for name, dynamically appended using this function  when we click on delete */
//function updateDocumentMapping() {
//    var $DocName = $('.documentName');
//    var $DocTypeName = $('.documentTypeName');
//    var $IncludeFax = $('.includeFax');
//    var $IncludeFaxLabel = $('.includeFaxLabel');
//    var $DocPath = $('.custom-file-input')
//    if ($DocName.length > 0) {
//        for (var p = 0; p < $DocName.length; p++) {
//            $DocName[p].name = 'Attachments[' + p + '].Name';
//            $DocTypeName[p].name = 'Attachments[' + p + '].AttachmentTypeName';
//            $IncludeFax[p].name = 'Attachments[' + p + '].IncludeFax';
//            $DocPath[p].name = 'Attachments[' + p + '].DocumentFile';
//            $IncludeFax[p].id = 'IncludeFax[' + p + ']';
//            $IncludeFaxLabel[p].attributes[1].value = 'IncludeFax[' + p + ']';
           
//        }
//    }
//};

//function readURL(input) {
//    if (input.files && input.files[0]) {
//        var reader = new FileReader();

//        reader.onload = function (e) {
//            window.open(e.target.result, '', 'width=' + screen.width + ',height=' + screen.height);
//        }
//        reader.readAsDataURL(input.files[0]);
//    }
//}
/* /ATTACHMENTS MANAGEMNENT*/

$('#PlaceOfService').focus();

var recDate = 0;
$('#UM_auth_form').on('click', '.form-control', function () {
    var p = $(this);
    RecdDateModal(p);
});

$('.auth_form').on('click', '#ReceivedDate', function () {
});

var SaveAuthorization = function () {
    var $form = $("#UM_auth_form");
    var formData = new FormData($form[0]);
    formData.append("ReasonDescription", $('#ReasontextID').val());
    formData.append("AuthorizationStatus.ActionPerformed", "Pend");
    $.ajax({
        type: 'POST',
        url: '/UM/AuthorizationAction/SaveAuthorization',
        data: formData,
        processData: false,
        contentType: false,
        cache: false,
        success: function (data) {
            $('.close_modal_btn').click();
            TabManager.closeCurrentlyActiveSubTab();
        }
    });
}

function RecdDateModal(p) {
        if ((recDate == 0) && (p.prop("id") != 'PlaceOfService') && (p.prop("id") != 'RequestType') && (p.prop("id") != 'AuthorizationType') && (p.prop("id") != 'TypeOfCare')) {
            TabManager.openFloatingModal('~/Areas/UM/Views/Authorization/ReceivedDate/_ModalBody.cshtml', '~/Areas/UM/Views/Authorization/ReceivedDate/_ModalHeader.cshtml', '~/Areas/UM/Views/Authorization/ReceivedDate/_ModalFooter.cshtml', '', '');
            //TabManager.openFloatingModal('/UM/Authorization/GetReceivedDateModal', '~/Areas/UM/Views/Authorization/ReceivedDate/_ModalHeader.cshtml', '~/Areas/UM/Views/Authorization/ReceivedDate/_ModalFooter.cshtml', '', '');
            setTimeout(function () {
                $("#receivedDate").text($('#ReceivedDate').val());
                $('#recd_Date_Close_Btn').focus();
            }, 500);
            recDate = 1;
    }
}

/* /NOTES MANAGEMNENT*/
function SwitchSummary(pos){

    switch (pos.toUpperCase()) {
        case "11(A)":
            pos = "11";
            break;
        case "OP PROCEDURE":
        case "OP DIAGNOSTIC":
            pos = "22";
            break;
        case "OP OBSERVATION":
        case "OP IN A BED":
            pos = "222";
            break;
    }
    $(".authSummarySection").addClass("displayNone");
    $("#POS" + pos + "AuthSummary").removeClass("displayNone");
}
$('#UM_auth_form').off('change', '#PlaceOfService').on('change', '#PlaceOfService', function () {
    var pos = $(this).val().split("-")[0];
    // ----- For Documents--------
    var $form = $("#UM_auth_form");
    var formData = new FormData($form[0]);
    // --- ends------
    //----CreateAuthCommon
    $.ajax({
        url: '/UM/Authorization/POSAreaSelector',
        //data: $('#UM_auth_form').serialize(),
        data: formData,
        data: formData,
        processData: false,
        contentType: false,
        cache: false,
        type: "POST",
        //cache: false,
        // dataType: "html",
        success: function (data) {
            document.getElementById('dynamicSection').innerHTML = data;
            TabManager.loadOrReloadScriptsUsingHtml(data);
            setAllFieldsWidth();
            UpdateAllDates();
            SwitchSummary(pos);
            setTimeout(function () {
                $('.summaryDataChange').watch('value', function () {
                    $(this).change();
                });
            }, 500);

            PrefillValues(pos);
        }
    });
});

function OPTypeChange() {
    var $form = $("#UM_auth_form");
    var formData = new FormData($form[0]);
    $.ajax({
        url: '/UM/Authorization/OPTypeChange',
        data: formData,
        data: formData,
        processData: false,
        contentType: false,
        cache: false,
        type: "POST",
        success: function (data) {
            document.getElementById('dynamicSection').innerHTML = data;
            TabManager.loadOrReloadScriptsUsingHtml(data);
            setAllFieldsWidth();
        
            var OPType = $("select[name='OutPatientType']").val();
            if (OPType && (OPType.toUpperCase() == "OP OBSERVATION" || OPType.toUpperCase() == "OP IN A BED"))
            {
                $("#AdmissionSection22Type :input").removeAttr("readonly");
                $('#AuthorizationType')[0].value = "Concurrent Initial";
                $('#TypeOfCare')[0].value = "Emergency";
                $('#RoomType')[0].value = "Med";
                $('#ReviewType')[0].value = "Observation";
            }
            else
            {
                $("#AdmissionSection22Type :input").attr("readonly", "readonly");
                $('#AuthorizationType')[0].value = "PreService";     
                $('#TypeOfCare')[0].value = "Elective";
            }
            UpdateAllDates();
            SwitchSummary(OPType);
            setTimeout(function () {
                $('.summaryDataChange').watch('value', function () {
                    $(this).change();
                });
            }, 500);
        }
    });
        }

/* Prefill the value on change of POS --- For time being --- Write all the values to be prefilled*/
PrefillPOS11Values();
function PrefillPOS11Values() {
$('#RequestType')[0].value = "Standard";
$('#AuthorizationType')[0].value = "PreService";
    $('#LevelOfCare')[0].value = "Medical";
    $('#TypeOfCare')[0].value = "Elective";

}

function PrefillValues(pos) {
    PrefillPOS11Values();
    if(pos == "21"){
        $('#AuthorizationType')[0].value = "Concurrent Initial";
        $('#TypeOfCare')[0].value = "Emergency";
        $('#RoomType')[0].value = "Med";
        $('#ReviewType')[0].value = "Admission";
}
if(pos == "31"){
    $('#AuthorizationType')[0].value = "Concurrent Initial";
    $('#LevelOfCare')[0].value = "Rehab";
    $('#LevelRate')[0].value = "Level 1";
    $('#ReviewType')[0].value = "Concurrent Initial";
}
if (pos == "62") {
    $('#LevelOfCare')[0].value = "Rehab";
}
if (pos == "24") {
    $('#LevelOfCare')[0].value = "Surgical";
}
}

/* --- Ends ----------- */

// ---- Methods related to validations ----------

function ValidateCreateDetails () {
    console.log("Success")
}
function validateForm () {
    var form = $('#UM_auth_form');
    form.removeData("validator").removeData("unobtrusiveValidation");
    $.validator.unobtrusive.parse(form);
    if (form.valid()) {
        return true;
    //      var $form = $("#UM_auth_form");
    //var formData = new FormData($form[0]);
    //$.ajax({
    //    type: 'POST',
    //    url: '/UM/Authorization/Authorization',
    //    data: formData,
    //    processData: false,
    //    contentType: false,
    //    cache: false,
    //    error: function () {
    //    },
    //    success: function (data) {
    //        $('#auth_preview_modal').html(data);
    //        showModal('authPreviewModal');
    //    }
    //});
    }
    else
    {
     return false;
    }
   

};

// -------- Ends--------------

//------UMServiceGroup
$('#UM_auth_form').off('change', '#UMServiceGroup').on('change', '#UMServiceGroup', function () {
    
    //if ($('#PlaceOfService').val() == "11(a)- OFFICE") {
    //    var POSID = 1;
    //} else if ($('#PlaceOfService').val() == "12- PATIENT HOME") {
    //    var POSID = 2;
    //} else if ($('#PlaceOfService').val() == "21- IP HOSPITAL") {
    //    var POSID = 3;
    //} else {
    //    var POSID = 4;
    //}


    //$.ajax({
    //    url: '/UM/Authorization/UMServiceGroupCPT',
    //    data: { PlaceOfServiceID: POSID },
    //    type: "POST",
    //    cache: false,
    //    dataType: "html",
    //    success: function (data) {
    //        RangeResultData = data.data;
    //        UMServiceCPT(RangeResultData);
    //    }
    //});
    if ($(this).val() == '') {
        $("#CPTArea").html(getCPTTemplate(-1));
        updateCPTMapping();
    }
    else {
        var umSvcGrpWorker = new Worker('/Areas/UM/WebWorkers/AjaxWebWorker.js');
        umSvcGrpWorker.postMessage({ url: '/UM/Authorization/UMServiceGroupCPT', singleParam: false, searchTerm: { UMServiceGrpID: $("#UMServiceGroup").val() } });
        umSvcGrpWorker.addEventListener('message', function (e) {
            if (e.data) {
                RangeResultData = JSON.parse(e.data);
                RangeResultData = RangeResultData.data;
                umSvcGrpWorker.terminate();
                UMServiceCPT(RangeResultData);
            }
        });
        //$.ajax({
        //    url: '/UM/Authorization/UMServiceGroupCPT',
        //    data: { PlaceOfServiceID: POSID },
        //    type: "POST",
        //    cache: false,
        //    dataType: "html",
        //    success: function (data) {
        //        RangeResultData = data.data;
        //        UMServiceCPT(RangeResultData);
        //    }
        //});
    }
    showFish();

});

var UMServiceCPT = function (RangeResultData) {
    var CPTRowTemplate = "";
    var innerIndex = 0;
    $("#CPTArea").html('');
    for (var rowIndex = 0; rowIndex < RangeResultData.length; rowIndex++) {
       
            $(".plain_language_btn").addClass('hidden');
            $(".smartIntelligenceBtn").addClass('hidden');
            if (($('#PlaceOfService').val() == '12- PATIENT HOME') || ($('#PlaceOfService').val() == '62- CORF')) {
                CPTRowTemplate = '<div class="CPTRow">' +
               '<div class="col-lg-1 theme_label_data zero-padding-left-right">' +
               '<input id="CPTCodes_' + innerIndex + '__CPTCode" name="CPTCodes[' + innerIndex + '].CPTCode" class="form-control cptCodes mandatory_field_halo cptCode_search_cum_dropdown input-xs" value="' + RangeResultData[rowIndex].CPTCode + '" placeholder="CPT CODE"/>' +
               '</div>' +
               '<div class="col-lg-1 theme_label_data zero-padding-left-right">' +
               '<input class="form-control input-xs cptModifier" id="CPTCodes_' + innerIndex + '_Modifier" name="CPTCodes[' + innerIndex + '].Modifier" placeholder="MODIFIER" type="text">' +
               '</div>' +
                                         '<div class="col-lg-1 theme_label_data zero-padding-left-right">' +
                                         '<input class="form-control input-xs mandatory_field_halo cptDescription_search_cum_dropdown cptDescriptions" id="CPTCodes_' + innerIndex + '___CPTDesc" name="CPTCodes[' + innerIndex + '].CPTDesc" value="' + RangeResultData[rowIndex].CPTDesc + '" placeholder="DESCRIPTION" type="text">' +
                                         '</div>' +

                                         '<div class="col-lg-1 theme_label_data zero-padding-left-right">' +
                                         '<select class="form-control input-xs cptDiscipline" id="CPTCodes_' + innerIndex + '_Disicipline" name="CPTCodes[' + innerIndex + '].Disicipline"  placeholder="REQ UNITS" type="text"><option value="">Select</option></select>' +
                                         '</div>' +

                                         '<div class="col-lg-1 theme_label_data cptrequnits_rightmargin zero-padding-left-right">' +
                                         '<input class="form-control input-xs cptReqUnits" id="CPTCodes_' + innerIndex + '_RequestedUnits" name="CPTCodes[' + innerIndex + '].RequestedUnits"  data-val="true" data-val-number="The field RequestedUnits must be a number." placeholder="REQ UNITS" type="text">' +
                                         '</div>' +

                                         '<div class="col-lg-1 theme_label_data  zero-padding-left-right">' +
                                         '<select class="form-control input-xs cptRange1" id="CPTCodes_' + innerIndex + '_Range1"  name="CPTCodes[' + innerIndex + '].Range1"  placeholder="Range 2" type="text"><option value="">Select</option></select>' +
                                         '</div>' +

                                         '<div class="col-lg-1 theme_label_data cptrequnits_rightmargin zero-padding-left-right">' +
                                         '<input class="form-control input-xs cptNoPer" id="CPTCodes_' + innerIndex + '_NumberPer" name="CPTCodes[' + innerIndex + '].NumberPer" placeholder="No Per" type="text">' +
                                         '</div>' +

                                         '<div class="col-lg-1 theme_label_data zero-padding-left-right">' +
                                         '<select class="form-control input-xs cptRange2" id="CPTCodes_' + innerIndex + '_Range2" name="CPTCodes[' + innerIndex + '].Range2"  placeholder="Range 2" type="text"><option value="">Select</option></select>' +
                                         '</div>' +

                                         '<div class="col-lg-1 theme_label_data cptrequnits_rightmargin zero-padding-left-right">' +
                                         '<input class="form-control input-xs cptTotalUnits" id="CPTCodes_' + innerIndex + '__TotalUnits" name="CPTCodes[' + innerIndex + '].TotalUnits" placeholder="Total Units" type="text">' +
                                         '</div>' +

                                         '<div class="col-lg-1 col-md-1 col-sm-2 col-xs-1 pull-left button-styles-xs row-action theme_label_data cptButtonArea zero-padding-left-right"></div>' +
                                         '<div class="col-lg-1 text-center theme_label_data zero-padding-left-right">' +
                                         //'<input type="checkbox" class="checkbox-radio includeLetter" id="CPTCodes_' + (innerIndex) + '__IncludeLetter" tabindex="-1" name="CPTCodes[' + (innerIndex) + '].IncludeLetter" value="IncLetter" ><label class="" for="CPTCodes_' + (innerIndex) + '__IncludeLetter"><span></span></label>' +
                                         '<button class="btn btn-danger btn-xs smartIntelligenceBtn bold_text calypso_ai_btn" type="button"> CALYPSO AI</button>' +
                                         '</div>' +
                                         '<div class="clearfix"></div>' +
                                         '</div>';
            }
            else if (($('#PlaceOfService').val() == '21- IP HOSPITAL') || ($('#PlaceOfService').val() == '31- SNF')) {

                CPTRowTemplate = '<div class="CPTRow">' +
                          '<div class="col-lg-2 theme_label_data zero-padding-left-right">' +
                          '<input id="CPTCodes_' + innerIndex + '__CPTCode" name="CPTCodes[' + innerIndex + '].CPTCode" class="form-control input-xs cptCode_search_cum_dropdown cptCodes" value="' + RangeResultData[rowIndex].CPTCode + '" placeholder="CPT CODE"/>' +
                          '</div>' +
                          '<div class="col-lg-1 theme_label_data zero-padding-left-right">' +
                          '<input class="form-control input-xs cptModifier" id="CPTCodes_' + innerIndex + '_Modifier" name="CPTCodes[' + innerIndex + '].Modifier" placeholder="MODIFIER" type="text">' +
                          '</div>' +
                          '<div class="col-lg-3 theme_label_data zero-padding-left-right">' +
                          '<input class="form-control input-xs cptDescription_search_cum_dropdown cptDescriptions" id="CPTCodes_' + innerIndex + '__CPTDesc" name="CPTCodes[' + innerIndex + '].CPTDesc" value="' + RangeResultData[rowIndex].CPTDesc + '" placeholder="DESCRIPTION" type="text">' +
                          '<input class="form-control input-xs cptDiscipline" id="CPTCodes_' + innerIndex + '_Discipline" name="CPTCodes[' + innerIndex + '].Discipline" placeholder="DICIPLINE" type="text" hiddden style="Display:none">' +
                          '</div>' +
                          '<div class="col-lg-1 theme_label_data cptrequnits_rightmargin zero-padding-left-right">' +
                          '<input class="form-control input-xs cptReqUnits" id="CPTCodes_' + innerIndex + '_RequestedUnits" name="CPTCodes[' + innerIndex + '].RequestedUnits"  data-val="true" data-val-number="The field RequestedUnits must be a number." placeholder="REQ UNITS" type="text">' +
                          '</div>' +
                          '<div class="col-lg-2 col-md-2 col-sm-2 col-xs-1 pull-left button-styles-xs row-action theme_label_data cptButtonArea zero-padding-left-right"></div>' +
                          '<div class="col-lg-3 col-md-1 col-sm-2 col-xs-1 cptInc_pullleft zero-padding-left-right pull-left">' +
                          '<button class="btn btn-danger btn-xs smartIntelligenceBtn bold_text calypso_ai_btn" type="button"> CALYPSO AI</button>' +
                          '</div>' +
                          '<div class="clearfix"></div>' +
                          '</div>';
            }
            else {

                CPTRowTemplate = '<div class="CPTRow">' +
                          '<div class="col-lg-2 theme_label_data zero-padding-left-right">' +
                          '<input id="CPTCodes_' + innerIndex + '__CPTCode" name="CPTCodes[' + innerIndex + '].CPTCode" class="form-control mandatory_field_halo input-xs loser_field cptCode_search_cum_dropdown cptCodes" value="' + RangeResultData[rowIndex].CPTCode + '" placeholder="CPT CODE"/>' +
                          '</div>' +
                          '<div class="col-lg-1 theme_label_data zero-padding-left-right">' +
                          '<input class="form-control input-xs cptModifier" id="CPTCodes_' + innerIndex + '_Modifier"  name="CPTCodes[' + innerIndex + '].Modifier" placeholder="MODIFIER" type="text">' +
                          '</div>' +
                          '<div class="col-lg-3 theme_label_data zero-padding-left-right">' +
                          '<input class="form-control input-xs mandatory_field_halo loser_field cptDescription_search_cum_dropdown cptDescriptions" id="CPTCodes_' + innerIndex + '__CPTDesc" name="CPTCodes[' + innerIndex + '].CPTDesc" value="' + RangeResultData[rowIndex].CPTDesc + '" placeholder="DESCRIPTION" type="text">' +
                          '<input class="form-control input-xs cptDiscipline" id="CPTCodes_' + innerIndex + '_Discipline" name="CPTCodes[' + innerIndex + '].Discipline" placeholder="DICIPLINE" type="text" hiddden style="Display:none">' +
                          '</div>' +
                          '<div class="col-lg-1 theme_label_data cptrequnits_rightmargin zero-padding-left-right">' +
                          '<input class="form-control input-xs cptReqUnits" id="CPTCodes_' + innerIndex + '_RequestedUnits" name="CPTCodes[' + innerIndex + '].RequestedUnits"  data-val="true" data-val-number="The field RequestedUnits must be a number." placeholder="REQ UNITS" type="text">' +
                          '</div>' +
                          '<div class="col-lg-2 col-md-2 col-sm-2 col-xs-1 pull-left button-styles-xs row-action theme_label_data cptButtonArea zero-padding-left-right"></div>' +
                          '<div class="col-lg-3 col-md-1 col-sm-2 col-xs-1 cptInc_pullleft zero-padding-left-right pull-left">' +
                          //'<input type="checkbox" class="checkbox-radio includeLetter" id="CPTCodes_' + (innerIndex) + '__IncludeLetter" tabindex="-1" name="CPTCodes[' + (innerIndex) + '].IncludeLetter" value="IncLetter" ><label class="" for="CPTCodes_' + (innerIndex) + '__IncludeLetter"><span></span></label>' +
                          '<button class="btn btn-danger btn-xs smartIntelligenceBtn bold_text loser_field calypso_ai_btn" type="button"> CALYPSO AI</button>' +
                          '</div>' +
                          '<div class="clearfix"></div>' +
                          '</div>';
            }
            innerIndex++;
        

        $("#CPTArea").append(CPTRowTemplate);
        CPTRowTemplate = "";
    }
    updateCPTMapping();
    $("#SharedFloatingModal").modal('hide');
    //$(".CPTArea").html(CPTRowTemplate);
    //updateCPTMapping();
    showFish();
};
$('body').off('click', '.cancel-createauth-btn').on('click', '.cancel-createauth-btn', function () {
    $('.create-auth-btn').removeClass("disabled");
});
//$(document).click(function () {
//    $(".UMDropDownICDList").hide();
//    $(".UMDropDownCPTList").hide();
//})

$('#UM_auth_form').off('keyup', '.drgcodesdropdown').on('keyup', '.drgcodesdropdown', function () {
    $(this).attr('autocomplete', 'off');
    Codevar = "DRGCode";
    DescVar = "DRGDescription";
    getSearchValues($(this).val(), this, '/UM/Authorization/DRGCodeData', 'drg');
});

$('#UM_auth_form').off('keyup', '.drgdescdropdown').on('keyup', '.drgdescdropdown', function () {
    $(this).attr('autocomplete', 'off');
    Codevar = "DRGCode";
    DescVar = "DRGDescription";
    getSearchValues($(this).val(), this, '/UM/Authorization/DRGDescData', 'drg');
});

$('#UM_auth_form').off('keyup', '.mdccodesdropdown').on('keyup', '.mdccodesdropdown', function () {
    $(this).attr('autocomplete', 'off');
    Codevar = "MDCCode";
    DescVar = "MDCDescription";
    getSearchValues($(this).val(), this, '/UM/Authorization/MDCCodeData', 'mdc');
});

$('#UM_auth_form').off('keyup', '.mdcdescdropdown').on('keyup', '.mdcdescdropdown', function () {
    $(this).attr('autocomplete', 'off');
    Codevar = "MDCCode";
    DescVar = "MDCDescription";
    getSearchValues($(this).val(), this, '/UM/Authorization/MDCDescData', 'mdc');
});

function getSearchValues(val, ob, url,action) {
    if ((typeof val != 'undefined') && (val != "")) {
        hideAllSearchCumDropDown();
        var SearchCumDropDownWorker = new Worker('/Areas/UM/WebWorkers/AjaxWebWorker.js');
        SearchCumDropDownWorker.postMessage({ url: url, singleParam: false, searchTerm: { Code: val, limit: 25 } });
        SearchCumDropDownWorker.addEventListener('message', function (e) {
            if (e.data) {
                ResultData = JSON.parse(e.data);
                SearchCumDropDownWorker.terminate();
                ResultData = ResultData.data;
                if (ResultData != null && ResultData.length > 0) {
                    var liData = "";
                    emptyExistElement($(ob).parent().find('ul'));
                    if(action=='drg')
                        generateDRGList(ResultData, $(ob)[0].id);
                    else if(action=='mdc')
                        generateMDCList(ResultData, $(ob)[0].id);
                }
                else {
                    emptyExistElement($(ob).parent().find('ul'));
                }
            }
        });
    }
}

function emptyExistElement(ele) {
    $(ele).each(function (e) {
        $(this).remove();
    })
}

function generateDRGList(eleData, InputId) {
    var liData = "";
    for (i in eleData) {
        liData = liData + "<li class='DropDownListLi' id='" + i + "' eleindex=" + i + "><span id='DataCode" + i + "'>" + eleData[i].DRGCode + "</span>-<span class='NPIcss' id='DataDescription" + i + "'>" + eleData[i].DRGDescription + "</span></li>";
    }
    var element = "<ul class='UMDropDownList UMDropDownDRGList dropdown-menu' id='" + InputId + "'><li>" + liData + "</li></ul>";
    $('#' + InputId).parent().last().after().append(element);
    SetCssForDropDown(InputId);//setting Css For UMDropDownICDList 
    $('#display').css("display", "block");
}

function generateMDCList(eleData, InputId) {
    var liData = "";
    for (i in eleData) {
        liData = liData + "<li class='DropDownListLi' id='" + i + "' eleindex=" + i + "><span id='DataCode" + i + "'>" + eleData[i].MDCCode + "</span>-<span class='NPIcss' id='DataDescription" + i + "'>" + eleData[i].MDCDescription + "</span></li>";
    }
    var element = "<ul class='UMDropDownList UMDropDownMDCList dropdown-menu' id='" + InputId + "'><li>" + liData + "</li></ul>";
    $('#' + InputId).parent().last().after().append(element);
    SetCssForDropDown(InputId);//setting Css For UMDropDownICDList 
    $('#display').css("display", "block");
}

function SetCssForDropDown(CssID) {
    var top = ($('#' + CssID).position().top + 7 + $('#' + CssID)[0].offsetHeight) + "px";
    var left = $('#' + CssID).position().left + "px";
    $('.UMDropDownList').css({ 'top': top, 'left': left });
}

$('#UM_auth_form').off('click', '.DropDownListLi').on('click', '.DropDownListLi', function () {
    $("#" + Codevar).val($("#DataCode" + $(this).closest('li')[0].id).html());
    $("#" + DescVar).val($("#DataDescription" + $(this).closest('li')[0].id).html());
    $('.UMDropDownList').html('');
    $('.UMDropDownList').hide();
});

$('#UM_auth_form').off('change', '.LevelRatePOS31').on('change', '.LevelRatePOS31', function () {
    var LevelRate = $("select[name='LevelRate']").val();
    if(LevelRate && LevelRate.toUpperCase()=="RUG")
    {
        $(".drgcodesdropdown").removeAttr('readonly');
        $(".drgdescdropdown").removeAttr('readonly');
    }
    else
    {
        $(".drgcodesdropdown").attr('readonly', 'readonly');
        $(".drgdescdropdown").attr('readonly', 'readonly');
        $(".drgcodesdropdown").val('');
        $(".drgdescdropdown").val('');
    }
});

/* Received Date on change function  */ 
$('#UM_auth_form').off('click', 'input[name=ReceivedDate]').on('click', 'input[name=ReceivedDate]', function () {
    ShowReceivedDateModal();
})
function ShowReceivedDateModal() {
    TabManager.openFloatingModal('~/Areas/UM/Views/Authorization/ReceivedDate/_ModalBodyReceiveDate.cshtml', '~/Areas/UM/Views/Authorization/ReceivedDate/_ModalHeader.cshtml', '~/Areas/UM/Views/Authorization/ReceivedDate/_ModalFooter.cshtml', '', '');
    setTimeout(function () {
        $("input[name='ReceivedDate']").val($('input[name = ReceivedDate]').val());
    }, 500);
}
/* It ends */