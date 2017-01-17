$("#NoteModal").ready(function () {

    setTimeout(function () {
        $('input.flat').iCheck({
            checkboxClass: 'icheckbox_square-green',
            radioClass: 'iradio_square-green'
        });
        $('input[name="Direction"]').on('ifChecked', function (event) {
            TypeChange();
        })

    }, 500)

    $("#Type").append("<option>" + "Select" + "</option>");
    $.each(TypeOptions, function (key, value) {
        $("#Type").append("<option value="+value.Name.split(" ")[0]+">" + value.Name + "</option>");
    })

    $("#Subject").append("<option>" + "Select" + "</option>");
    $.each(SubjectOption, function (key, value) {
        $("#Subject").append("<option  value=" + value.Name.split(" ")[0] + ">" + value.Name + "</option>");
    })
})

var TypeOptions = [{ id: 1, Name: "Nurse Review" },
                   { id: 2, Name: "MD Review" },
                   { id: 3, Name: "Additional Information" },
                   { id: 4, Name: "other" },
                   { id: 5, Name: "Auth Note" },
                   { id: 6, Name: "PreAuth" }
];


$(".SaveBtn").addClass("SaveNote");

var SaveNoteData = function () {
 
}

$("#NoteModal").on('change', '#Type,#Subject,#DenialRadio,#ApproveRadio', function () {
    TypeChange();
});

function TypeChange() {
    if ($("#Type").val() != "MD") {
        $("#MDReview").hide();
    }
    $("#textareas").hide();
    if ($("#Type").val() == "Nurse" && $("#Subject").val() == "UM") {
        $("#MDReview").hide();
        $("#AddedNotes").hide();
        $("#textareas").show();
        return;
    }
    if ($("#Type").val() == "MD") {
        $("#AddedNotes").hide();
        $("#textareas").hide();
        $("#MDReview").show();
        if ($("#Subject").val() == "UM" && $("#ApproveRadio").val() == "on") {
            $("#UM_Note").show();
            return;
        }
        else if ($("#Subject").val() == "UM" && $("input[name='Direction']:checked").val() == "on") {
            $("#UM_Note").hide();
            $("#textareas").show();
            return;
        }
    }
    $("#AddedNotes").show();
}

Number.prototype.padLeft = function (base, chr) {
    var len = (String(base || 10).length - String(this).length) + 1;
    return len > 0 ? new Array(len).join(chr || '0') + this : this;
}


var dateFormat=function (d) {
        dformat = [(d.getMonth() + 1).padLeft(),
                    d.getDate().padLeft(),
                    d.getFullYear()].join('/') +
                    ' ' +
                  [d.getHours().padLeft(),
                    d.getMinutes().padLeft(),
                    d.getSeconds().padLeft()].join(':');
    return dformat;
}



$('.SaveNote').on("click", function () {
    var ApproveCheck = false;
    var DenialCheck = false;
    $('input').on('ifChecked', function (event) {
         ApproveCheck = true;
    });
    $('input').on('ifChecked', function (event) {
        DenialCheck = true;
    });
    //--------Normal note
    if ($("#Normal-Note").val()) {
        NoteData.push({
            DateTime: dateFormat(new Date()),
            USER: "BARBARA JOY",
            INC: false,
            TYPE: $("#Type").val(),
            SUBJECT: $("#Subject").val(),
            NOTE: $("#Normal-Note").val(),
            RATIONALE: "",
            ALTERNATEPLAN: "",
            CRITERIAUSED: "",
            SERVICESUBJECTTONOTICE: "",
            LISTOFMEDICALSERVICES: "",
            Approve: ApproveCheck,
            Denial: DenialCheck,
            Flag:"NORMAL"
        })
    }

   

    //------um--note
    if ($("#UMNote-Note").val()) {
        NoteData.push({
            DateTime: dateFormat(new Date()),
            USER: "BARBARA JOY",
            INC: false,
            TYPE: $("#Type").val(),
            SUBJECT: $("#Subject").val(),
            NOTE: $("#UMNote-Note").val(),
            RATIONALE: "",
            ALTERNATEPLAN: "",
            CRITERIAUSED: $("#UMNote-Criteria").val(),
            SERVICESUBJECTTONOTICE: "",
            LISTOFMEDICALSERVICES: "",
            Approve: ApproveCheck,
            Denial: DenialCheck,
            Flag: "UMNOTE"
        })
    }
    //--MD--Nurse
    if ($("#NurseMD-Note").val()) {
        NoteData.push({
            DateTime: dateFormat(new Date()),
            USER: "BARBARA JOY",
            INC: false,
            TYPE: $("#Type").val(),
            SUBJECT: $("#Subject").val(),
            NOTE: $("#NurseMD-Note").val(),
            RATIONALE: $("#NurseMD-RATIONALE").val(),
            ALTERNATEPLAN: $("#NurseMD-ALTERNATE").val(),
            CRITERIAUSED: $("#NurseMD-CRITERIA").val(),
            SERVICESUBJECTTONOTICE: $("#NurseMD-SERVICE").val(),
            LISTOFMEDICALSERVICES: $("#NurseMD-MEDICAL").val(),
            Approve: ApproveCheck,
            Denial: DenialCheck,
            Flag: "NURSEMD"
        })
    }
    GetNoteTemplate();
    $(".SaveBtn").removeClass("SaveNote");
    $(".close_modal_btn").click();
})

var SubjectOption = [ { id: 1, Name: "Claims Review"},
                      { id: 2, Name: "Denc" },
                      { id: 3, Name: "Negotiate rate"},
                      { id: 4, Name: "Nomnc"},
                      { id: 5, Name: "OON"},
                      { id: 6, Name: "UM Note"}
];



$('.EditNote').on("click", function () {
    index = $("#modalSaveBtn").data("index");
    var ApproveCheck = false;
    var DenialCheck = false;
    //$('input').on('ifChecked', function (event) {
    //    ApproveCheck = true;
    //});
    //$('input').on('ifChecked', function (event) {
    //    DenialCheck = true;
    //});

    //--------Normal note
    if ($("#Normal-Note").val()) {
            NoteData[index].DateTime= dateFormat(new Date());
            NoteData[index].USER= "BARBARA JOY";
            NoteData[index].INC= false;
            NoteData[index].TYPE= $("#Type").val();
            NoteData[index].SUBJECT= $("#Subject").val();
            NoteData[index].NOTE= $("#Normal-Note").val();
            NoteData[index].RATIONALE= "";
            NoteData[index].ALTERNATEPLAN= "";
            NoteData[index].CRITERIAUSED= "";
            NoteData[index].SERVICESUBJECTTONOTICE= "";
            NoteData[index].LISTOFMEDICALSERVICES= "";
            NoteData[index].Approve= ApproveCheck;
            NoteData[index].Denial= DenialCheck;
            NoteData[index].Flag= "NORMAL";
    }
    //------um--note
    if ($("#UMNote-Note").val()) {
            NoteData[index].DateTime = dateFormat(new Date());
            NoteData[index].USER= "BARBARA JOY";
            NoteData[index].INC= false;
            NoteData[index].TYPE= $("#Type").val();
            NoteData[index].SUBJECT= $("#Subject").val();
            NoteData[index].NOTE= $("#UMNote-Note").val();
            NoteData[index].RATIONALE= "";
            NoteData[index].ALTERNATEPLAN= "";
            NoteData[index].CRITERIAUSED= $("#UMNote-Criteria").val();
            NoteData[index].SERVICESUBJECTTONOTICE= "";
            NoteData[index].LISTOFMEDICALSERVICES= "";
            NoteData[index].Approve= ApproveCheck;
            NoteData[index].Denial= DenialCheck;
            NoteData[index].Flag = "UMNOTE";
    }
    //--MD--Nurse
    if ($("#NurseMD-Note").val()) {
            NoteData[index].DateTime= dateFormat(new Date());
            NoteData[index].USER= "BARBARA JOY";
            NoteData[index].INC= false;
            NoteData[index].TYPE= $("#Type").val();
            NoteData[index].SUBJECT= $("#Subject").val();
            NoteData[index].NOTE= $("#NurseMD-Note").val();
            NoteData[index].RATIONALE= $("#NurseMD-RATIONALE").val();
            NoteData[index].ALTERNATEPLAN= $("#NurseMD-ALTERNATE").val();
            NoteData[index].CRITERIAUSED= $("#NurseMD-CRITERIA").val();
            NoteData[index].SERVICESUBJECTTONOTICE= $("#NurseMD-SERVICE").val();
            NoteData[index].LISTOFMEDICALSERVICES= $("#NurseMD-MEDICAL").val();
            NoteData[index].Approve= ApproveCheck;
            NoteData[index].Denial= DenialCheck;
            NoteData[index].Flag = "NURSEMD";
    }
   
    GetNoteTemplate();
    $(".SaveBtn").removeClass("EditNote");
    $(".close_modal_btn").click();
});