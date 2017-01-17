/** 
@description Removes the row from the Coding Notes Table 
* @param {string} ele current element
 */
$('#CodingNotestbody').off('click', '.removeCodingNotesrow').on('click', '.removeCodingNotesrow', function () {
    $(this).parent().parent().remove();
});

/**
@description Adds new row to the Coding Notes Table 
 */
var addnewNotesrow=function(){
    var newrow='<tr>'
                            +'<td>'
                                +'<input class="form-control input-xs text-uppercase" id="CodingNotes_1__Category" name="CodingNotes[1].Category" type="text">'
                            +'</td>'
                            +'<td>'
                            +'    <input class="form-control input-xs text-uppercase" id="CodingNotes_1__Title" name="CodingNotes[1].Title" type="text">'
                            +'</td>'
                            +'<td>'
                            +'    <input class="form-control input-xs text-uppercase" id="CodingNotes_1__Description" name="CodingNotes[1].Description" type="text">'
                            +'</td>'
                            +'<td>'
                            +'    <input class="form-control input-xs text-uppercase" id="CodingNotes_1__Module" name="CodingNotes[1].Module" type="text">'
                            + '</td>'
                            +'<td>'
                                +'<input class="form-control input-xs text-uppercase" id="CodingNotes_1__Notify" name="CodingNotes[1].Notify" type="text">'
                            +'</td>'
                            +'<td>'
                            +'    <input class="form-control input-xs text-uppercase" id="CodingNotes_1__AddedBy" name="CodingNotes[1].AddedBy" type="text">'
                            +'</td>'
                            +'<td>'
                            +'    <input class="form-control input-xs text-uppercase" data-val="true" data-val-date="The field AddedOn must be a date." data-val-required="The AddedOn field is required." id="CodingNotes_1__AddedOn" name="CodingNotes[1].AddedOn" type="text">'
                            +'</td>'
                            +'<td>'
                            +'    <a class="removeCodingNotesrow pointer"><span class="fa fa-close"></span></a>'
                            +'</td>'
    '</tr>';
    $('#CodingNotestbody').append(newrow);
}