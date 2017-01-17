
//custom validation rule - EndDate
$.validator.addMethod("enddate", function (value, element, params) {
    return Date.parse(value) > Date.parse($(element).closest("form").find("#" + params.endDateProperty).val())
});

//Custom jQuery validation unobtrusive script and adapters for EndDate
$.validator.unobtrusive.adapters.add("enddate", ["endDateProperty"], function (options) {

        options.rules["enddate"] = options.params;
        options.messages["enddate"] = options.message;
});