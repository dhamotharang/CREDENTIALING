
//custom validation rule - Posted File Extension
$.validator.addMethod("postedfilesize", function (value, element, params) {
    if (value == "" && params.isrequiredproperty == "False")
        return true;

    var fileSize = $(element).closest("form").find(":input[data-val-postedfilesize-propertyname='" + params.propertyname + "']")[0].files[0].size;
    return fileSize <= params.allowedfilesize;
});

//Custom jQuery validation unobtrusive script and adapters for posted file size
$.validator.unobtrusive.adapters.add("postedfilesize", ["allowedfilesize", "isrequiredproperty", "propertyname"], function (options) {

    // Match parameters to the method to execute
    options.rules['postedfilesize'] = options.params;
    options.messages["postedfilesize"] = options.message;
});

//$.validator.addMethod("postedfilesize", function (value, element, params) {
//    var thisValue, otherValue;
//    if (this.optional(element)) { // No value is always valid
//        return true;
//    }

//    thisValue = parseInt(value);
//    otherValue = parseInt($(params).val());
//    return thisValue > otherValue;
//});

//function getModelPrefix(fieldName) {
//    return fieldName.substr(0, fieldName.lastIndexOf(".") + 1);
//}

//function appendModelPrefix(value, prefix) {
//    if (value.indexOf("*.") === 0) {
//        value = value.replace("*.", prefix);
//    }
//    return value;
//}

//$.validator.unobtrusive.adapters.add("postedfilesize", ["allowedfilesize"], function (options) {
//    var prefix = getModelPrefix(options.element.id),
//        other = options.params.allowedfilesize,
//        fullOtherName = appendModelPrefix(other, prefix),
//        element = $(options.form).find(":input[name=" + fullOtherName + "]")[0];

//    options.rules["postedfilesize"] = element;  // element becomes "params" in callback
//    if (options.message) {
//        options.messages["postedfilesize"] = options.message;
//    }
//});