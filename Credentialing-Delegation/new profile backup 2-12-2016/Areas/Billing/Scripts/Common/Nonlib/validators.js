// Add data annotation client-side validation adapters.
$.validator.addMethod("enforcetrue", function (value, element, param) {
    return element.checked;
});
$.validator.unobtrusive.adapters.addBool("enforcetrue");

$.validator.addMethod("greaterthanzero", function (value, element, param) {
    return $(element).val() > 0;
});
$.validator.unobtrusive.adapters.addBool("greaterthanzero");

$.validator.addMethod("validatefile", function (value, element, param) {
    var regexTest =/\.(txt)$/.test(value);
    if (!regexTest) {
        this.settings.messages.EdiFile.validatefile = param.exterrormsg;
        return false;
    }
    else
    {
        return true;
    }
  
    
});
$.validator.unobtrusive.adapters.add("validatefile", ["otherpropertyname", "sizeerrormsg", "exterrormsg"], function (options) {
    options.rules["validatefile"] = { otherElementID: "#" + options.params.otherpropertyname, sizeerrormsg: options.params.sizeerrormsg, exterrormsg: options.params.exterrormsg };
    options.messages["validatefile"] = options.message;
});
