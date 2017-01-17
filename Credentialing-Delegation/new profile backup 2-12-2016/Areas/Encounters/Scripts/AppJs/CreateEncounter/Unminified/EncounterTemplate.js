$('.proceedToCoding').on('click', function () {
    if (createClaimByType == 'Multiple claims for a Provider') {

        currentProgressBarData[4].postData = new FormData($('#EncounterForm')[0]);

        MakeItActive(5, currentProgressBarData);

    } else {
        currentProgressBarData[3].postData = new FormData($('#EncounterForm')[0]);
        MakeItActive(4, currentProgressBarData);
    }
})

$('#DocumentHistoryBtn').on('click', function (e) {
    e.preventDefault();
    $.ajax({
        type: 'GET',
        url: '/Encounters/CreateEncounter/GetDocumentHistory',
        processData: false,
        contentType: false,
        success: function (result) {
            $('#DocumentHistoryContainer').html(result);
        }
    });
});


$('#revertProviderEncounterPage').on('click', function () {
    MakeItVisible(2, currentProgressBarData);
});

$('#revertMemberEncounterPage').on('click', function () {
    if (createClaimByType == 'Multiple claims for a Provider') {
        MakeItVisible(3, currentProgressBarData);
    } else {
        MakeItVisible(2, currentProgressBarData);
    }

});
$('.mainContent').off('click', '.ProviderEditBtn').on('click', '.ProviderEditBtn', function () {
    $('.ProviderLabel').show();
    $(this).parent('.ProviderLabel').hide();
    $('.ProviderSearch').hide();
    var ContainerId = $(this).attr('data-url');
    $(ContainerId).show();
    $('#ProviderList').html('');
    $('#ProviderList').hide();
});

function DisplayProviderList(url, searchString, type) {
    $.ajax({
        type: 'GET',
        url: url,
        processData: false,
        contentType: false,
        success: function (result) {
            $('#ProviderList').html(result);
            $('#ProviderList').show();
            $('#providers_title').html('YOU SEARCHED FOR "<b>' + searchString + '</b>" | <b>5</b> ' + type + ' RESULTS FOUND');
        }
    });

}

$('.mainContent ').off('click', '#ReferingProviderSearchBtn').on('click', '#ReferingProviderSearchBtn', function (e) {
    e.preventDefault();
    DisplayProviderList('/Encounters/CreateEncounter/GetReferingProviderList', $('[name="ReferringProvider"]').val(), 'REFERRING PROVIDER');
});

$('.mainContent ').off('click', '#BillingProviderSearchBtn').on('click', '#BillingProviderSearchBtn', function (e) {
    e.preventDefault();
    DisplayProviderList('/Encounters/CreateEncounter/GetBillingProviderList', $('[name="BillingProvider"]').val(), 'BILLING PROVIDER');
});

$('.mainContent ').off('click', '#FacilitySearchBtn').on('click', '#FacilitySearchBtn', function (e) {
    e.preventDefault();
    DisplayProviderList('/Encounters/CreateEncounter/GetFacilityList', $('[name="Facility"]').val(), 'FACILITY');
});

$('.mainContent ').off('click', '#RenderingProviderSearchBtn').on('click', '#RenderingProviderSearchBtn', function (e) {
    e.preventDefault();
    DisplayProviderList('/Encounters/CreateEncounter/GetRenderingProviderList', $('[name="RenderingProvider"]').val(), 'RENDERING PROVIDER');
});



function openClaimHistoryModal() {
    TabManager.openCenterModal('/Encounters/CreateEncounter/GetClaimsHistory', 'Claim History', '', '')
}

$('#ClaimHistoryBtn').on('click', function () {
    openClaimHistoryModal();
});



var templateIndexFile = 0;
$('.addFileButton').on('click', function (e) {
    e.preventDefault();
    templateIndexFile++;
    var templateHtml = '<div class="row">'
                       + '<div class="col-lg-3">'
                                            + 'Document Name'
                                            + '<input class="form-control input-xs non_mandatory_field_halo uppercase" id="DocumentDetails_' + templateIndexFile + '__DocumentName" name="DocumentDetails[' + templateIndexFile + '].DocumentName" type="text" value="">'
                                        + '</div>'
                                        + '<div class="col-lg-3">'
                                          + 'Document Category'
                                            + '<input class="form-control input-xs non_mandatory_field_halo uppercase" id="DocumentDetails_' + templateIndexFile + '__DocumentName" list="DocumentCategory" name="DocumentDetails[' + templateIndexFile + '].DocumentName" type="text" value="">'
                                            + '<datalist id="DocumentCategory">'
                                                + '<option value="PROGRESS NOTES"></option>'
                                                + '<option value="LAB REPORT"></option>'
                                                + '<option value="IMAGE"></option>'
                                                + '<option value="CONSULTATION"></option>'
                                                + '<option value="HOSPITAL"></option>'
                                                + '<option value="CHART"></option>'
                                                + '<option value="PRESCRIPTION"></option>'
                                                + '<option value="REFERENCE NOTES"></option>'
                                                + '<option value="HOSPITAL DISCHARGE"></option>'
                                                + '<option value="ECW PHYSICIAN TO PHYSICIAN"></option>'
                                                + '<option value="SPECIALITY FORMS"></option>'
                                                + '<option value="MISCELLANEOUS"></option>'
                                                + '<option value="EXAMINATION"></option>'
                                                + '<option value="PHYSICAL EXAM DRAWING"></option>'
                                                + '<option value="PROCEDURE DRAWING"></option>'
                                                + '<option value="PROCEDURE DOCUMENT"></option>'
                                                + '<option value="EOB"></option>'
                                                + '<option value="ADVANCE DIRECTORY"></option>'
                                                + '<option value="OTHERS"></option>'
                                            + '</datalist>'
                                        + '</div>'
                                        + '<div class="col-lg-3 docTypeSelector">'
                                            + '<div>'
                                                + '<input type="radio" name="FileTypeUploadCreate-' + templateIndexFile + '" class="normal-radio" id="uploaddata-' + templateIndexFile + '" checked="checked">'
                                                + '<label for="uploaddata-' + templateIndexFile + '" style="font-weight:normal;"><span></span>Upload</label>'
                                                + '<input type="radio" name="FileTypeUploadCreate-' + templateIndexFile + '" class="normal-radio" value="0-1" id="createfile-' + templateIndexFile + '">'
                                                + '<label for="createfile-' + templateIndexFile + '" style="font-weight:normal;"><span></span>Create</label>'
                                            + '</div>'
                                            + '<div class="fileBox" data-stampid="' + templateIndexFile + '" style="margin-top: -5px;">'
                                                + '<input class="form-control input-xs non_mandatory_field_halo" type="file" name="DocumentDetails[' + templateIndexFile + '].Document">'
                                            + '</div>'
                                            + '<div class="textBox" data-stampid="' + templateIndexFile + '" style="margin-top: -5px; display: none;">'
                                                + '<textarea class="expandableNotesBox form-control input-xs non_mandatory_field_halo textToPdfFile"></textarea>'
                                            + '</div>'
                                        + '</div>'
                                        + '<div class="col-lg-3">'
                                            + 'Actions'
                                            + '<div class="Actions">'
                                            + '<button class="btn btn-xs btn-primary"><i class="fa fa-upload"></i> Upload</button>'
                                            + '<a class="btn btn-xs btn-danger deleteButton" data-stampid="1" onclick="DelectFile(this)"><i class="fa fa-minus"></i></a>'
                                            + '</div>'
                                        + '</div>'
                                    + '</div>';
    $('#DocumentList').prepend(templateHtml);
});

function DelectFile(ele) {
    ele.parent().parent().parent().remove();
}


$('#DocumentHistoryContainer').on('click', '#CloseDocHistoryBtn', function () {

    $('#DocumentHistoryContainer').html('');
});

function openDocumentViewer() {
    $('#DocumentViewer').show();
}