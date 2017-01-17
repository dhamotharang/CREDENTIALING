

function toggleAttachmentView()
{
    ShowModal("/Home/_GetAttachmentModalView", "Add New Attachment");
    TitleNum = 1;
    TypeNum = 1;
}

var TitleOption = [
                    { id: 1, Name: "Hospital Records"},
                    { id: 2, Name: "Specialist Records"},
                    { id: 3, Name: "PCP Records" },
                    { id: 4, Name: "office Facesheet" },
                    { id: 5, Name: "plan Authorization" }
];


var TitleNum = 1;
function TitleMenu()
{
    if(TitleNum == 1)
    {
        $("#listid").append("<option>" + " " + "</option>");
        $.each(TitleOption, function (key, value) {
            $("#listid").append("<option>" + value.Name + "</option>");
        })
        TitleNum+=1;
    }
}

var TypeOption = [
                  { id: 1, Name: "Clinical" },
                  { id: 2, Name: "Progress Note" },
                  { id: 3, Name: "Fax" },
                  { id: 4, Name: "LOA" },
                  { id: 5, Name: "Plan Authorization" }
];

var TypeNum = 1;

function TypeMenu1()
{
    if(TypeNum == 1)
    {
        $("#Type").append("<option>" + " " + "</option>");
        $.each(TypeOption, function (key, value) {
            $("#Type").append("<option>" + value.Name + "</option>");
        })
        TypeNum += 1;
    }
}