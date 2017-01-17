/*!
 * RadioCheckBox-plugin v1.0 (component 14)
 * Copyright 2016-2017 Pratian Technologies Pvt Limited.
 * Licensed under ----
 */
/*! Author: Pavan Kota */

// Do not modify without prior intimation
(function ($)
{
    $.fn.applyRadioStyles = function (options)
    {
        var defaults = {
            RadioClasses: ['normal-radio', 'checkbox-radio']
        };
        //options && options.RadioClasses ? $.merge(defaults.RadioClasses, options.RadioClasses) : $.noop();
        var settings = $.extend({}, defaults, options);
        /*Method is written in such a way that it does not influence normal radio buttons*/
        if (isRadioClassPresent(this.attr("class"))) {
            if (this.attr("checked") === 'checked') {
                this[0].checked = false;
                $('[name=' + this.attr("name") + ']').removeAttr('checked');
            }
            else {
                $('[name=' + this.attr("name") + ']').removeAttr('checked');
                this[0].checked = true;
                this.attr("checked", "checked");
            }
        }
        /*Ensures that the class defined in html matches with the classes present in settings:
         Returns true if class matches
             otherwise false         
        */
        function isRadioClassPresent(className)
        {
            className = jQuery.type(className) === "string" ? className.trim() : className;
            for (index in settings.RadioClasses) {
                if (spaceCheck(className).indexOf(jQuery.type(settings.RadioClasses[index]) === "string" ? spaceCheck(settings.RadioClasses[index]) : settings.RadioClasses[index]) >= 0) {
                    return true;
                }
            }
            return false;
        };
        /*Ensures that any other class name that matches our plugin classes is properly handled and neglected*/
        function spaceCheck(str)
        {
            return jQuery.type(str) === "string" ? ' '.concat(str.trim().concat(' ')) : str;
        }
    };
}(jQuery));