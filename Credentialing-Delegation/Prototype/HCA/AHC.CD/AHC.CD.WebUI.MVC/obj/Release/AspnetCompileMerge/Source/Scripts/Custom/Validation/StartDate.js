//custom validation rule - StartDate
$.validator.addMethod("startdate", function (value, element, params) {
    return Date.parse(value) >= Date.parse(params.mindate) && Date.parse(value) <= Date.parse(params.maxdate);
});

//Custom jQuery validation unobtrusive script and adapters for StartDate
$.validator.unobtrusive.adapters.add("startdate", ["maxdate", "mindate"], function (options) {
        options.rules["startdate"] = options.params;
        options.messages["startdate"] = options.message;
});
