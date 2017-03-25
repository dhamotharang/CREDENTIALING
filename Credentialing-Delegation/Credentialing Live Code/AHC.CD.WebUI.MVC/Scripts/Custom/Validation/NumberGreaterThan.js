

//custom validation rule - EndDate
$.validator.addMethod("numbergreaterthan", function (value, element, params) {

    if (value == "" && params.isrequiredproperty == "False")
        return true;

    var dependentProperty = $(element).closest("form").find("#" + params.dependentproperty).val();

    if (value != "" && dependentProperty == "")
        return true;

    return (parseInt(value) >= parseInt(dependentProperty));
});

//Custom jQuery validation unobtrusive script and adapters for EndDate
$.validator.unobtrusive.adapters.add("numbergreaterthan", ["dependentproperty", "isrequiredproperty"], function (options) {

    options.rules["numbergreaterthan"] = options.params;
    options.messages["numbergreaterthan"] = options.message;
});