
//custom validation rule - Posted File Extension
$.validator.addMethod("postedfileextension", function (value, element, params) {
   
    if (value == "" && params.isrequiredproperty == "False")
        return true;

    var extension = getFileExtension(value);

    var validExtension = $.inArray(extension, params.fileextensions) !== -1;
    return validExtension;
});

//Custom jQuery validation unobtrusive script and adapters for posted file size
$.validator.unobtrusive.adapters.add("postedfileextension", ["allowedfileextensions", "isrequiredproperty"], function (options) {

    // Set up test parameters
    var params = {
        fileextensions: options.params.allowedfileextensions.split(','),
        isrequiredproperty: options.params.isrequiredproperty
    };

    // Match parameters to the method to execute
    options.rules['postedfileextension'] = params;
    options.messages["postedfileextension"] = options.message;
});

function getFileExtension(fileName) {
    var extension = (/[.]/.exec(fileName)) ? /[^.]+$/.exec(fileName) : undefined;
    if (extension != undefined) {
        return extension[0];
    }
    return extension;
};