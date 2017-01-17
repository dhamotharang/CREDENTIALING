$(function () {
    // ON VALUE CHANGE OF CHECKBOXES AND RADIO BUTTONS:
    $('[value=denial]').prop('checked', true);

    $('#NoteForm').css({ 'height': ($(window).height() - 50),'max-height':'auto'});

    $('[value=approval]').on('click', function () {
        $('#mdReviewchk').prop('checked', true);
        $('#nurseReviewchk').prop('checked', false);
        $("#DenialDecisionShow").hide();
        $("#ApproveDecisionShow").show();
    });
    $('#nurseReviewchk').on('change', function () {
        if (this.checked) {
            $('[value=denial]').prop('checked', true);
            $("#DenialDecisionShow").show();
            $("#ApproveDecisionShow").hide();
        }
    });
    $('[value=denial]').on('change', function () {
        $("#DenialDecisionShow").show();
        $("#ApproveDecisionShow").hide();
    });

    // ADDING MEDICAL SERVICE ROWS: 
    var countOfMSRows = 1;
    var listOfMedicalServices = ['listOfMedicalService_1'];
    $('#NoteForm').on('click', '.plusButton', function () {
        var currentElement = $(this).parent().parent();
        var currentRowId = currentElement.attr('id');
        if (currentRowId == listOfMedicalServices[0] && listOfMedicalServices.length == 1) {
            $(this).parent().append(removeButtonForNote());
        }
        $('#NoteForm').find('.plusButton').remove();
        listOfMedicalServices.push('listOfMedicalService_' + (++countOfMSRows));
        $('#NoteForm #listOfMedicalServices').append(addMedicalServiceRow(countOfMSRows));
        $("#SharedModalBody").animate({ scrollTop: $(document).height() }, 500, 'swing');
    });
    $('#NoteForm').on('click', '.minusButton', function () {
        var currentElement = $(this).parent().parent();
        var currentRowId = currentElement.attr('id');
        if (listOfMedicalServices.length > 2) {
            if (currentRowId == listOfMedicalServices[listOfMedicalServices.length - 1]) {
                $('#' + listOfMedicalServices[listOfMedicalServices.length - 2]).find('.button-styles-xs').append(addButtonForNote());
            }
            currentElement.remove();
        }
        else if (listOfMedicalServices.length = 2) {
            currentElement.remove();
            if (currentRowId == listOfMedicalServices[0]) {
                $('#' + listOfMedicalServices[1]).find('.button-styles-xs').empty().append(addButtonForNote());
            }
            else if (currentRowId == listOfMedicalServices[1]) {
                $('#' + listOfMedicalServices[0]).find('.button-styles-xs').empty().append(addButtonForNote());
            }
        }
        listOfMedicalServices.splice(listOfMedicalServices.indexOf(currentRowId), 1);
    });

    function addButtonForNote() {
        return '<button class="btn btn-xs btn-success plusButton"><i class="fa fa-plus"></i></button>';
    }
    function removeButtonForNote() {
        return '<button class="btn btn-xs btn-danger minusButton"><i class="fa fa-minus"></i></button>';
    }
    function addMedicalServiceRow(countOfMSRows) {
        return ('<div class="animate fadeIn" id="listOfMedicalService_' + (countOfMSRows) + '">' +
                    '<div class="col-lg-11 padding-left-zero"><input type="text" placeholder="MEDICAL SERVICES" class="form-control input-xs" /></div>' +
                    '<div class="col-lg-1 padding-left-zero button-styles-xs">' + removeButtonForNote() + addButtonForNote() + '</div>' +
                '</div>');
    }
});