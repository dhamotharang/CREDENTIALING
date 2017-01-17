/*ADD NEW ROW FOR PRIMARY DX*/
$('#PrimaryDXArea').off('click', '.icd_add_btn').on('click', '.icd_add_btn', function () {
    var $row = $(this).parents('.PrimaryDXRow');
    var childCount = parseInt($row.parent().children().length);
    var rowIndex = parseInt($row.index());
    $(this).addClass('hidden');
    $('.icd_delete_btn').removeClass("hidden");
    $.ajax({
        type: 'post',
        url: '/PriorAuthAddRow/AddNewRow',
        data: { index: rowIndex + 1, url: "~/Areas/Portal/Views/PriorAuth/PriorAuthForm/Common/_ICDRow.cshtml" },
        cache: false,
        error: function () {

        },
        success: function (data, textStatus, XMLHttpRequest) {
            PrimaryDxRow = data;
            if (childCount == rowIndex) {
                $row.parent().append(PrimaryDxRow);
            }
            else {
                $(PrimaryDxRow).insertAfter($row);
            }
        }
    });

});
/*DELETE ADDED ROW FOR PRIMARY DX*/
function UpdateMapping(element, rowIndex) {//Getting all The elements After deleted row
    $(element).each(function (i, e) {//looping over Parent elements
        $(this).find('input,select').each(function (i, e) {//Looping Inside The child elements of Parent to Find Input tag and Update the Index
            var res = $(this)[0].name.split("[");//splitting based on "[" so we get name divide into eg:-"IcdCode[" and "].code"
            res[0] = res[0] + "[";//adding [
            var index = parseInt(res[1].charAt(0));//Getting the Index
            res[1] = res[1].substring(1);//removing the previous index  
            $(this)[0].name = (res[0] + (index - 1) + res[1]).toString();//adding the updated index
        })
    })
}

$('#PrimaryDXArea').off('click', '.icd_delete_btn').on('click', '.icd_delete_btn', function () {
    var v = $(this).parents('.PrimaryDXRow').nextAll()
    var $row = $(this).parents('.PrimaryDXRow').remove();
    UpdateMapping(v);
    setAddDeleteButtonsVisibility(".icd_add_btn", ".icd_delete_btn", "PrimaryDXArea");
});