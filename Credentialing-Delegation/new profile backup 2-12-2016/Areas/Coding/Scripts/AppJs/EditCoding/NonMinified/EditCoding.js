$('span[data-toggle="tooltip"]').tooltip();

var onSuccessFn = function (data) {
    TabManager.closeCurrentlyActiveTab();
    var tab = {
        "tabAction": "Coding List",
        "tabTitle": "Coding List",
        "tabPath": "/Coding/Coding/GetCodingList",
        "tabContainer": "fullBodyContainer"
    }
    TabManager.navigateToTab(tab);
}
$(document).off("submit", "form").on("submit", "form", function (event) {
    event.preventDefault();
    if (validateForm()) {
        var url = "/Coding/Coding/SaveDetails";
        var formData = new FormData(this);
        formData.append('Status', 'Closed');
        $.ajax({
            url: url,
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (data) {
               onSuccessFn(data);
            },
            error: function (data) {
                console.log(data);
            }
        });
    }
});
var validateForm = function () {
    var form = $('#SaveDetailsFormDiv').find('form');
    form.removeData("validator").removeData("unobtrusiveValidation");
    $.validator.unobtrusive.parse(form);
    if (form.valid()) {
        return true;
    }
    return false;

};
/** 
@description Removes the row from the table 
* @param {string} ele current element
 */
function RemoveRows(ele) {
    var tbody = $(ele).parent().parent();
    var trIndex = findRemovedElementIndex(ele);
    $(ele).parent("tr").remove();
    tbody.children().each(function (index) {
        if (index >= trIndex) {
            var inputs = $(this).find('input');
            inputs.each(function () {
                var id = $(this).attr("id");
                var newId = id.replace(/[0-9]+/, index);
                var name = $(this).attr("name");
                var newName = name.replace(/[0-9]+/, index);
                $(this).attr("id", newId);
                $(this).attr("name", newName);

            });
            var spans = $(this).find('span');
            spans.each(function () {
                var msg = $(this).attr("data-valmsg-for");
                var newmsg = msg.replace(/[0-9]+/, index);
                $(this).attr("data-valmsg-for", newmsg);

            });
        }
    });


}
function findRemovedElementIndex(ele) {
    var tr = $(ele).parent()[0];
    var trIndex;
    $(ele).parent().parent().children().each(function (index) {
        if (this === tr) {
            trIndex = index;
        }

    });
    return trIndex;
}



$('#uploadDocument').click(function (e) {
    
    var formData = new FormData();

    formData.append('DocumentName', $("form#savedetailsForm input[name='DocumentName'").val());
    formData.append('DocumentCategory', $("form#savedetailsForm input[name='DocumentCategory'").val());
    var file = $("form#savedetailsForm input[name='File'")[0].files[0];
    formData.append('File', file);
    $.ajax({
        type: 'POST',
        url: '/Coding/Coding/SaveDocument',
        data: formData,
        contentType: false,
        processData: false,
        success: function (data) {

        }
    });
    e.stopPropagation();
});



