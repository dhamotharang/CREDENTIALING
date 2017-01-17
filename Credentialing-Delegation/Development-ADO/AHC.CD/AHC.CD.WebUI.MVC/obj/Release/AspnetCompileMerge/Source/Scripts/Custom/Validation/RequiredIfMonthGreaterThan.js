//custom validation rule - StartDate
$.validator.addMethod("requiredifmonthgreaterthan", function (value, element, params) {
    if (value == "" && params.isrequiredproperty == "False")
        return true;

    var startDate = new Date($(element).closest("form").find("#" + params.startdateproperty).val());
    var endDate = new Date($(element).closest("form").find("#" + params.enddateproperty).val());
    return value != '' && ((((endDate.getYear() - startDate.getYear()) * 12) + endDate.getMonth() - startDate.getMonth()) >= params.range);
});

//Custom jQuery validation unobtrusive script and adapters for StartDate
$.validator.unobtrusive.adapters.add("requiredifmonthgreaterthan", ["startdateproperty", "enddateproperty", "range", "isrequiredproperty"], function (options) {
    options.rules["requiredifmonthgreaterthan"] = options.params;
    options.messages["requiredifmonthgreaterthan"] = options.message;
});