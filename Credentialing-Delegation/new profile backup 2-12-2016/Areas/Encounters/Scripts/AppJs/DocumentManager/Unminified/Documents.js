function openClaimHistoryModal() {
    TabManager.openCenterModal('~/Areas/Encounters/Views/Common/_ClaimHistoryModal.cshtml', 'Claim History', '', '')
}
function toggleDocumentHistoryPanel(){
    $('.Viewmemdochistory').slideToggle();
}

//Add Documents Functionality
$('.textBox').hide();
$('.documentUploads').on('ifChecked', 'input[type="radio"]', function (event) {
    handleFileTypeRadioEvent(event.target.value);
});

var noOfFilesAdded = 1;
function addFile_Click() {
    var row = '<div class="fileUploadItem col-lg-12" data-stampid="' + noOfFilesAdded + '" style="margin-bottom: 5px;">' +
                '<div class="col-lg-3">' +
                    '<label>Document Name</label>' +
                    '<input disabled="true" data-stampid="' + noOfFilesAdded + '" class="form-control input-xs non_mandatory_field_halo DocumentName" placeholder="Document Name" type="text"/>  ' +
                '</div>' +
                '<div class="col-lg-2">' +
                    '<label>Document Category</label>' +
                    '<select data-stampid="' + noOfFilesAdded + '" class="form-control input-xs non_mandatory_field_halo documentCategorySelector">' +
                       '<option>PROGRESS NOTES</option>' +
                       '<option>LAB REPORT</option>' +
                       '<option>IMAGE</option>' +
                       '<option>CONSULTATION</option>' +
                       '<option>HOSPITAL</option>' +
                       '<option>CHART</option>' +
                       '<option>PRESCRIPTION</option>' +
                       '<option>REFERENCE NOTES</option>' +
                       '<option>HOSPITAL DISCHARGE</option>' +
                       '<option>ECW PHYSICIAN TO PHYSICIAN</option>' +
                       '<option>SPECIALITY FORMS</option>' +
                       '<option>MISCELLANEOUS</option>' +
                       '<option>EXAMINATION</option>' +
                       '<option>PHYSICAL EXAM DRAWING</option>' +
                       '<option>PROCEDURE DRAWING</option>' +
                       '<option>PROCEDURE DOCUMENT</option>' +
                       '<option>EOB</option>' +
                       '<option>ADVANCE DIRECTORY</option>' +
                       '<option>OTHERS</option>' +
                    '</select>' +
                '</div>' +
                 '<div class="col-lg-4 docTypeSelector">' +
                   ' <div>' +
                    '    <input type="radio" name="FileTypeUploadCreate' + noOfFilesAdded + '" class="normal-radio" id="e_upload-' + noOfFilesAdded + '" value="' + noOfFilesAdded + '-0" checked="checked">' +
                   '     <label for="e_upload-' + noOfFilesAdded + '" style="text-transform:none"><span></span>Upload</label>' +

                   '     <input type="radio" name="FileTypeUploadCreate' + noOfFilesAdded + '" class="normal-radio" value="' + noOfFilesAdded + '-1" id="e_create-' + noOfFilesAdded + '">' +
                  '      <label for="e_create-' + noOfFilesAdded + '" style="text-transform:none"><span></span>Create</label>' +
                 '   </div>' +

                  '  <div class="fileBox" data-stampid="' + noOfFilesAdded + '">' +
                   '     <input class="form-control input-xs non_mandatory_field_halo" type="file" />' +
                  '  </div>' +
                  '  <div class="textBox" data-stampid="' + noOfFilesAdded + '">' +
                   '     <textarea class="expandableNotesBox form-control input-xs non_mandatory_field_halo textToPdfFile"></textarea>' +
                  '  </div>' +
             '   </div>' +
                '<div class="col-lg-3">' +
                   ' <label>Actions</label>' +
                    '<div>' +
                     '   <button class="btn btn-xs btn-primary"><i class="fa fa-upload"></i> Upload</button>' +
                    '    <button class="btn btn-xs btn-danger deleteButton" data-stampid="' + noOfFilesAdded + '"><i class="fa fa-minus"></i></button>' +
               '     </div>       ' +
             '   </div>' +
         '   </div>';

    $('.fileUploadContainer').prepend(row);
    $('.textBox[data-stampid="' + noOfFilesAdded + '"]').hide();
    $('input[type="radio"]').on('ifChecked', function (event) {
        handleFileTypeRadioEvent(event.target.value);
    });
    noOfFilesAdded++;
}

$('.documentUploads').on('change', '.documentCategorySelector', function (event) {
    event.preventDefault();
    var stampid = this.dataset.stampid;
    var element = $('input.DocumentName[data-stampid="' + stampid + '"]');
    if ($(this).val() == 'OTHERS') {
        element.removeAttr('disabled');
    }
    else {
        element.attr('disabled', 'true');
    }
});

function handleFileTypeRadioEvent(val) {
    var valMatrix = val.split('-');
    var stampid = valMatrix[0];
    var radioValue = valMatrix[1];

    var fileBoxes = $('.fileBox');
    var textBoxes = $('.textBox');

    $.each(fileBoxes, function (index, fileBox) {

        if (fileBox.dataset.stampid == stampid) {
            if (radioValue == '0') {
                $(fileBoxes[index]).show();
                $(textBoxes[index]).hide();
            }
            else {
                $(fileBoxes[index]).hide();
                //$(textBoxes[index]).show();
                showModal('createFileModal');
                setModalId('createFileModal', stampid);
            }
        }
    });

}

function setModalId(modalId, id) {
    $('#' + modalId).data("modalId", id);
}


$('.fileUploadContainer').on('click', '.deleteButton', function (evt) {
    evt.preventDefault();
    var stampid = this.dataset.stampid;
    var boxes = $('.fileUploadItem');
    for (var i in boxes) {
        if (boxes[i].dataset.stampid == stampid) {
            $(boxes[i]).remove();
        }
    }
});