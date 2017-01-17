var onSuccessFn = function () {
    TabManager.closeCurrentlyActiveTab();
    var tab = {
        "tabAction": "Coding List",
        "tabTitle": "Coding List",
        "tabPath": "/Coding/Coding/GetCodingList",
        "tabContainer": "fullBodyContainer"
    }
    TabManager.navigateToTab(tab);
}
var validateForm = function () {
    var form = $('#createsavedetailsForm');
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
        if (this == tr) {
            trIndex = index;
        }

    });
    return trIndex;
}