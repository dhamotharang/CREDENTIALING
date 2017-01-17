$("#Contacts").ready(function () {
   
    for (var i = 0; i < ContactData.length; i++)
    {
        if (ContactData[i].key == "ContactEntity") {
            $("#Entity").val(ContactData[i].value);
           
        }
        if (ContactData[i].key == "ContactType") {
            $("#Type").val(ContactData[i].value);
            
        }
        if (ContactData[i].key == "ContactName") {
            $("#ContactName").val(ContactData[i].value);
          
        }
        if (ContactData[i].key == "ContactNote") {
            $("#note").val(ContactData[i].value);
         
        }
        if (ContactData[i].key == "ContactDateTime") {
            $("#date").val(ContactData[i].value);
        
        }
        if (ContactData[i].key == "ContactDirection")
        {
            if(ContactData[i].value=="OutBound")
            {
                var checked = $("#ver9").parent();
                checked.addClass("checked");
            }
            else
            {
                var checked = $("#ver10").parent();
                checked.addClass("checked");
            }
        }
        if(ContactData[i].key == "ContactReason")
        {
            $("#Reason").val(ContactData[i].value);
        }
        if(ContactData[i].key=="ContactNumber")
        {
            $("#ContactNumber").val(ContactData[i].value);
        }
        if(ContactData[i].key=="ContactOutcome")
        {
            $("#outcome").val(ContactData[i].value);
        }
    }
})
function EditNote(tr,index) {
    $tr = tr;
    console.log($tr);
}