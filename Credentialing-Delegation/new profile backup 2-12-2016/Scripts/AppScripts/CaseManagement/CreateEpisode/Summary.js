
// Modal Data for Add List of Problems
var demo2 = $('.ProblemList').bootstrapDualListbox({
    nonSelectedListLabel: 'Problem List',
    selectedListLabel: 'Selected Problems',
    preserveSelectionOnMove: 'moved',
    moveOnSelect: true
});


$("#SubmitProblemList").click(function () {
    var ProblemList = [];
    ProblemList = $('[name="duallistbox_demo2"]').val();

    
    var problems = '';
    for (var i = 0; i < ProblemList.length; i++) {   
        problems += '<li> <span class="col-lg-12"> <span style="padding-left: 0px;" class="col-lg-8"> &nbsp; ' + ProblemList[i] + '</span> <span class="col-lg-4"> <label class="btn btn-xs btn-danger pull-left RemoveProblem"><i class="fa fa-close"></i> </label></span> </span> </li>'
    }

    $('.AddProblemList').html(problems);
    console.log(problems);
    CloseModalManually("slide_modal", "modal_background");

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

    $('.AddProblemList .RemoveProblem').click(function () {        
        $(this).closest('li').remove();
    });
});

// Modal Data for Add List of Diseases
var demo3 = $('.DiseaseList').bootstrapDualListbox({
    nonSelectedListLabel: 'Diseases List',
    selectedListLabel: 'Selected Diseases',
    preserveSelectionOnMove: 'moved',
    moveOnSelect: true
});


$("#SubmitDiseaseList").click(function () {
    var ProblemList = [];
    DiseaseList = $('[name="duallistbox_demo3"]').val();


    var diseases = '';
    for (var i = 0; i < DiseaseList.length; i++) {
        diseases += '<li> <span class="col-lg-12"> <span style="padding-left: 0px;" class="col-lg-8"> &nbsp; ' + DiseaseList[i] + '</span> <span class="col-lg-4"> <label class="btn btn-xs btn-danger pull-left RemoveDisease"><i class="fa fa-close"></i> </label></span> </span> </li>'
    }

    $('.AddDiseasesList').html(diseases);
    console.log(diseases);
    CloseModalManually("slide_modal", "modal_background");

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

    $('.AddDiseasesList .RemoveDisease').click(function () {
        $(this).closest('li').remove();
    });
});
