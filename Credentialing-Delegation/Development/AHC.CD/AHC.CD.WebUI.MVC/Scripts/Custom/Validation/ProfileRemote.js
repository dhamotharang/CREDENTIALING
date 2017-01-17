//Custom jQuery validation unobtrusive script and adapters for EndDate
$.validator.unobtrusive.adapters.add("profileremote", ["url", "parametername", "isrequiredproperty"], function (options) {

    //options.rules["profileremote"] = options.params;
    //options.messages["profileremote"] = options.message;

    var value = {
        url: options.params.url,
        type: options.params.type || "GET",
        data: {}
    }, prefix = options.element.name.substr(0, options.element.name.lastIndexOf(".") + 1);

    $.each(splitAndTrim(options.params.parametername), function (i, fieldName) {

        if (fieldName == "profileId") {
            if (typeof profileId !== 'undefined') {
                value.data[fieldName] = profileId;
                additionalFieldValue = profileId;
            }
        } else {
            var paramName = removeModelPrefix(fieldName);
            var attrName = appendModelPrefix(fieldName, prefix);

            //var paramValue = $(options.form).find(":input").filter("[name='" + escapeAttributeValue(attrName) + "']").val();
            //if (typeof paramValue !== 'undefined')
            //{
            //    value.data[paramName] = paramValue;
            //}

            value.data[paramName] = function () {
                if (typeof $(options.form).find(":input").filter("[name='" + escapeAttributeValue(attrName) + "']").val() !== 'undefined') {
                    return $(options.form).find(":input").filter("[name='" + escapeAttributeValue(attrName) + "']").val();
                }
                return 0;
            };
        }
    });

    var paramName = removeModelPrefix(options.element.name);

    value.data[paramName] = function () {
        return $(options.form).find(":input").filter("[name='" + options.element.name + "']").val();
    };

    setValidationValues(options, "remote", value);
});

function appendModelPrefix(value, prefix) {
    if (value.indexOf(".") != 0) {
        value = value.split(".").pop();
    }
    return prefix+value;
}

function removeModelPrefix(value) {
    if (value.indexOf(".") != 0) {
        value = value.split(".").pop();
    }
    return value;
}

function splitAndTrim(value) {
    return value.replace(/^\s+|\s+$/g, "").split(/\s*,\s*/g);
}

function setValidationValues(options, ruleName, value) {
    options.rules[ruleName] = value;
    if (options.message) {
        options.messages[ruleName] = options.message;
    }
}

function escapeAttributeValue(value) {
    // As mentioned on http://api.jquery.com/category/selectors/
    return value.replace(/([!"#$%&'()*+,./:;<=>?@\[\\\]^`{|}~])/g, "\\$1");
}