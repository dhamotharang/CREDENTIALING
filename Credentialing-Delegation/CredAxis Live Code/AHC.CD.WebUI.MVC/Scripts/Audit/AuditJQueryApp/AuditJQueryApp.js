var ServiceRepository = new JQueryServiceRepository();

var AuditLog = function (ServiceRepository) {
    this.ServiceRepoForAuditLog = ServiceRepository;
}

var AuditLogInstance = new AuditLog(ServiceRepository);

AuditLog.prototype.GetTableInstanceForAuditLog = function () {
    $('#AuditTable').prtGrid({
        url: "/Audit/GetAllAuditLOG",
        dataLength: 50,
        columns: [
            { type: 'text', name: 'Controller', text: 'Controller Name', sortable: { isSort: true, defaultSort: null } },
            { type: 'text', name: 'Action', text: 'Action Name', sortable: { isSort: true, defaultSort: null } },
            { type: 'text', name: 'DateTime', text: 'Performed Date', sortable: { isSort: true, defaultSort: 'DESC' } },
            { type: 'text', name: 'User', text: 'User Name', sortable: { isSort: true, defaultSort: null } },
            { type: 'text', name: 'URL', text: 'URL', sortable: { isSort: true, defaultSort: null } },
            { type: 'text', name: 'Category', text: 'Category', sortable: { isSort: true, defaultSort: null } },
            { type: 'text', name: 'IP', text: 'IP', sortable: { isSort: true, defaultSort: null } }
        ],
        externalFactors: [{ name: 'AuditCategory', type: 'hidden' }]
    });
    $("#LOGS").addClass("biscit_blue");
}

AuditLog.prototype.GetAllAuditBasedOnCategory = function () {
    AuditLogInstance.ShowLoading('AuditTable');
    $.when(AuditLogInstance.ServiceRepoForAuditLog.PostJSONDataToService('/Audit/GetAllAuditBasedOnCategory?AuditCategory=' + $("#AuditCategory").val())).then(function (response) {
        
        $("#tableWrapper").empty();
        $("#tableWrapper").html(response);
        $("#TotalLogCount").empty().text(totalcount);
        $("#InformationLogCount").empty().text(informationlogcount);
        $("#AlertLogCount").empty().text(alertlogcount);

        if ($("#AuditCategory").val() == "Total")
        {
            $("#LOGS").addClass("biscit_blue");
            $("#Information").removeClass("biscit_green");
            $("#Alert").removeClass("biscit_time");
        }
        else if ($("#AuditCategory").val() == "Information")
        {
            $("#Information").addClass("biscit_green");
            $("#LOGS").removeClass("biscit_blue");
            $("#Alert").removeClass("biscit_time");
        }
        else if ($("#AuditCategory").val() == "Alert")
        {
            $("#Alert").addClass("biscit_time");
            $("#Information").removeClass("biscit_green");
            $("#LOGS").removeClass("biscit_blue");
        }
            

        AuditLogInstance.RemoveLoading();
    },
    function (Error) {
        AuditLogInstance.RemoveLoading();
    })
}

AuditLog.prototype.SetHidenFieldValue = function (HiddenFieldId, HiddenFieldValue) {
    $('#' + HiddenFieldId).val(HiddenFieldValue);
}

AuditLog.prototype.ShowLoading = function (ID) {
    var $ob = $('#loadingSample');
    var $clon = $ob.clone().addClass("loadingArea").removeClass("hidden");
    $('#' + ID).prepend($clon);
}

AuditLog.prototype.RemoveLoading = function () {
    $('.loadingArea').fadeOut(function () {
        $(this).remove();
    });
}


$(document).ready(function () {
    AuditLogInstance.GetTableInstanceForAuditLog();
    $("#LOGS").click(function (Event) {
        Event.preventDefault();
        AuditLogInstance.SetHidenFieldValue("AuditCategory", "Total");
        AuditLogInstance.GetAllAuditBasedOnCategory();
    });
    $("#Information").click(function (Event) {
        Event.preventDefault();
        AuditLogInstance.SetHidenFieldValue("AuditCategory", "Information");
        AuditLogInstance.GetAllAuditBasedOnCategory();
    });
    $("#Alert").click(function (Event) {
        Event.preventDefault();
        AuditLogInstance.SetHidenFieldValue("AuditCategory", "Alert");
        AuditLogInstance.GetAllAuditBasedOnCategory();
    });
    $("#FilterButton").click(function (event) {
        $('table.AuditLogTable tr.filter-row').toggle('fade-out');
    });
})