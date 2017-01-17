//---Setting Add and Delete Button for CPT,ICD,Document----//
function setAddDeleteButtonsVisibility(AddButton, DeleteButton, ParentID)
{
    $($('#' + ParentID).find(AddButton)).addClass("hidden");
    $($('#' + ParentID).find(AddButton)).last().removeClass("hidden");
    if ($($('#' + ParentID).find(DeleteButton)))
    {
        if ($($('#' + ParentID).find(DeleteButton)).length > 1) 
        {
            $($('#' + ParentID).find(DeleteButton)).removeClass("hidden");
        }
        else 
        {
            $($('#' + ParentID).find(DeleteButton)).addClass("hidden");
        }
    }
};