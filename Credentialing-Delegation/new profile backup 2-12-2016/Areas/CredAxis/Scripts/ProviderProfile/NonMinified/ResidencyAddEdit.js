function ToggleFields(state, toggleID) {
    //if (toggleID = "razor") {
    //    var id = $("span .nopadding").attr('id');
    //}
    //else { 
    if (state)
        $("#" + toggleID).show();
    else
        $("#" + toggleID).hide();
 //   }
}