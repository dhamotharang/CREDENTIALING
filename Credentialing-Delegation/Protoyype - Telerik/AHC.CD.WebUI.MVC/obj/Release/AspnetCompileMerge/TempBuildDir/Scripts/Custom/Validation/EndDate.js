
//custom validation rule - EndDate
$.validator.addMethod("enddate", function (value, element, params) {

    if (value == "" && params.isrequiredproperty == "False")
        return true;

    var startDate = Date.parse($(element).closest("form").find("#" + params.endDateProperty).val());

    if (!startDate && Date.parse(value) <= Date.parse(params.maxdate))
        return true;

    if (params.isgreaterthan == "True")
        return Date.parse(value) > startDate && Date.parse(value) <= Date.parse(params.maxdate);


    return Date.parse(value) >= startDate && Date.parse(value) <= Date.parse(params.maxdate);
});

//Custom jQuery validation unobtrusive script and adapters for EndDate
$.validator.unobtrusive.adapters.add("enddate", ["endDateProperty", "isrequiredproperty", "maxdate", "isgreaterthan"], function (options) {

        options.rules["enddate"] = options.params;
        options.messages["enddate"] = options.message;
});