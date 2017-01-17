


var ContactType = [];
var ContactEntity = [];
var ContactReason = [];
var ContactOutCome = [];
var Reasonmapping = [];
var OutComeMapping = [];
var ContactDirection = [];
var ContactOutComeType=[];
$("#Contacts").ready(function () {

    
    $.ajax({
        url: '/Resources/Data/UM/MasterData/Contacts/ContactTypes.js',
        success: function (data, textStatus, XMLHttpRequest) {
            for (var i = 0; i < data.length; i++) {
                ContactType.push(ContactTypes[i]);
            }

            $("#Type").append("<option>" + "Select" + "</option>");
            $.each(ContactType, function (key, value) {
               if(value!=null) $("#Type").append('<option>' + value.ContactTypeName + '</option>');
            })
        }
    });

    $.ajax({
        url: '/Resources/Data/UM/MasterData/Contacts/ContactEntities.js',
        success: function (data, textStatus, XMLHttpRequest) {
            for (var i = 0; i < ContactEntities.length; i++) {
                ContactEntity.push(ContactEntities[i]);
         
            }
            $("#Entity").append("<option>" + "Select" + "</option>");
            $.each(ContactEntity, function (key, value) {
                $("#Entity").append("<option>" + value.EntityName + "</option>");
            })
        }
    });
 
    $.ajax({
        url: '/Resources/Data/UM/MasterData/Contacts/ContactReasons.js',
        success: function (data, textStatus, XMLHttpRequest) {
            for(var i=0;i<=ContactReasons.length;i++)
            {
                ContactReason.push(ContactReasons[i]);
            }
        }
    });

    $.ajax({
        url: '/Resources/Data/UM/MasterData/Contacts/ContactOutcome.js',
        success: function (data, textStatus, XMLHttpRequest) {
            for (var i = 0; i <= ContactOutcomes.length; i++) {
                ContactOutCome.push(ContactOutcomes[i]);
                
            }
          
        }
    });

   

    $.ajax({
        url: '/Resources/Data/UM/MasterData/Contacts/ReasonMapping.js',
        success:function(data,textStatus, XMLHttpRequest)
        {
            for(var i=0;i<ReasonMapping.length;i++)
            {
                Reasonmapping.push(ReasonMapping[i]);
                
            }
           
        }
    });

    $.ajax({
        url: '/Resources/Data/UM/MasterData/Contacts/OutComeMapping.js',
        success: function (data, textStatus, XMLHttpRequest) {
            for (var i = 0; i < OutcomeMapping.length; i++) {
                OutComeMapping.push(OutcomeMapping[i]);
            }
          
        }
    });

    $.ajax({
        url: '/Resources/Data/UM/MasterData/Contacts/ContactDirection.js',
        success:function(data,textStatus,XMLHttpRequest)
        {
            for(var i=0;i<ContactDirections.length;i++)
            {
                ContactDirection.push(ContactDirections[i]);
            }
        }
    });

    $.ajax({
        url: '/Resources/Data/UM/MasterData/Contacts/ContactOutComeType.js',
        success: function (data, textStatus, XMLHttpRequest) {
            for (var i = 0; i < ContactOutcomeType.length; i++) {

                ContactOutComeType.push(ContactOutcomeType[i]);

            }
        }
    });
    $("#Reason").append("<option>" + "Select" + "</option>");
    $("#outcome").append("<option>" + "Select" + "</option>");
});

function ReasonOutcome()
{
    var tempContactReason = [];
    var tempContactOutCome = [];
    var ContactTypeID='';
    var ContactEntityID = '';
    var ContactDirectionID = '';
    var ContactOutComeTypeID = '';
    for(var i=0;i<ContactType.length;i++)
    {
        if (ContactType[i].ContactTypeName == $("#Type").val())
        {
            ContactTypeID = ContactType[i].ContactTypeID;
            break;
        }
    }
    for(var i=0;i<ContactEntity.length;i++)
    {
        if(ContactEntity[i].EntityName==$("#Entity").val())
        {
            ContactEntityID = ContactEntity[i].ContactEntityID;
            break;
        }
    }

    for (var i = 0; i < ContactDirection.length; i++)
    {
        if(ContactDirection[i].Direction==$("input[name='Direction']:checked").val())
        {
            ContactDirectionID = ContactDirection[i].ContactDirectionID;
            break;
        }
    }
    
    for (var i = 0; i < ContactOutComeType.length; i++) {

        if (ContactOutComeType[i].OutcomeTypeName == $("input[name='OutcomeType']:checked").val())
        {

            ContactOutComeTypeID = ContactOutComeType[i].OutcomeTypeID;
            break;
        }
    }

        if ((ContactTypeID != null && ContactTypeID != '') && (ContactEntityID != null && ContactEntityID != '') && (ContactDirectionID != null && ContactDirectionID!=''))
        {
            $("#Reason").empty();
            ReasonMap(ContactTypeID, ContactEntityID, ContactDirectionID);
        }
            
        if ((ContactTypeID != null && ContactTypeID != '') && (ContactEntityID != null && ContactEntityID != '') && (ContactOutComeTypeID != null && ContactOutComeTypeID != ''))
        {
            $("#outcome").empty();
            OutComeMap(ContactTypeID, ContactEntityID, ContactOutComeTypeID);
        }

 }   
var ReasonID = '';
var flag = '';
var OutComeID = '';
var flag1 = '';
var ReasonMap =function(typeid,entityid,directionid)
{
    for(var i=0;i<Reasonmapping.length;i++)
    {
        if (Reasonmapping[i].ContactTypeID == typeid && Reasonmapping[i].ContactEntityTypeID == entityid && Reasonmapping[i].ContactDirectionID == directionid)
        {
            ReasonID = Reasonmapping[i].ReasonID;
           
            flag = 1;
            try {
                for (var j = 0; j < ContactReason.length; j++) {
                    if (ContactReason[j].ReasonID == ReasonID && flag == 1) {
                        $("#Reason").append("<option>" + ContactReason[j].ReasonDescription + "</option>");
                        flag = 0;
                    }
                }
            } catch (e) {

            }
        }
    }
}

var OutComeMap=function(typeid,entityid,outcometypeid)
{
    for(var i=0;i<OutComeMapping.length;i++)
    {
        if(OutComeMapping[i].ContactTypeID==typeid && OutComeMapping[i].ContactEntityTypeID==entityid && OutComeMapping[i].OutcomeTypeID== outcometypeid)
        {
            OutComeID = OutComeMapping[i].OutComeID;
            
            flag1 = 1;
            try
            {
                for(var j=0;j<ContactOutCome.length;j++)
                {
                    if(ContactOutCome[j].OutcomeID==OutComeID && flag1==1)
                    {
                        $("#outcome").append("<option>" + ContactOutCome[j].OutcomeType + "</option>");
                        flag1 = 0;
                    }
                }
            }
            catch(e)
            {

            }
        }
    }
}


$('.ContactsModal').on('click', '[name="Direction"]', function () {
    ReasonOutcome();
})
$('.ContactsModal').on('click', '[name="OutcomeType"]', function () {
    ReasonOutcome();
})
//function EditContact(e) {
//    ShowModal('~/Views/Home/Modal/Contact/_EditContactModal.cshtml', 'Edit Contact');
//    //EditNote($(e).closest('tr'), $(e).closest('tr').index());
//    //console.log("tableRow:"+$(e).closest('tr'));    
//}

