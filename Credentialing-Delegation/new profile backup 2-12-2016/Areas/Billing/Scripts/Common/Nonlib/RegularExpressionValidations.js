$.validator.addMethod("EMAIL", function (value, element) {
    return this.optional(element) || /^[a-zA-Z0-9._-]+@[a-zA-Z0-9-]+\.[a-zA-Z.]{2,5}$/i.test(value);
}, "Email Address is invalid: Please enter a valid email address.");

$.validator.addMethod("Phone", function (value, element) {
    return this.optional(element) || /^[0-9]{1,10}$/.test(value);
}, "Phone Number is invalid: Please enter a valid Phone.");

//^(\+?1-?)?(\([2-9]\d{2}\)|[2-9]\d{2})-?[2-9]\d{2}-?\d{4}$

$.validator.addMethod("Numeric", function (value, element) {
    return this.optional(element) || /^[0-9]*$/i.test(value);
}, " Numeric data Expected.");

$.validator.addMethod("Alphabetic", function (value, element) {
    return this.optional(element) || /^[ a-zA-z]*$/i.test(value);
}, "only alphabets.");
$.validator.addMethod("DotsAndSlashes", function (value, element) {
    return this.optional(element) || /^[\.\/]*$/i.test(value);
}, "Not valid Charchters.");

$.validator.addMethod("AlphaNumeric", function (value, element) {
    return this.optional(element) || /^[ a-zA-z0-9]*$/i.test(value);
}, "Enter Only Alphabets[A-Z] or Numbers[0-9] Character.");

$.validator.addMethod("Lenght2to15", function (value, element) {
    return this.optional(element) || /^.{2,15}$/i.test(value);
}, "Length Range Not Expected.");

$.validator.addMethod("Lenght1to15", function (value, element) {
    return this.optional(element) || /^.{1,15}$/i.test(value);
}, "Length Range Not Expected.");

$.validator.addMethod("MultipleDashes", function (value, element) {
    return this.optional(element) || /^[0-9]+[0-9-]*[0-9]+$/i.test(value);
}, "Only data of type [0-9][0-9-]*[0-9] Expected.");

$.validator.addMethod("Decimal", function (value, element) {

    return this.optional(element) || /^([1-9][0-9]*)?([0]?)(\.[0-9][0-9]?)?$/i.test(value);
}, "Only Decimal is acceted.");
$.validator.addMethod("TwoDecimal", function (value, element) {
    return this.optional(element) || /^(?!0.00)[0-9]*[.]?[0-9]+?$/i.test(value);
}, "Only Numerals are allowed and amount less than penny claim amount i.e., (0.01) are not accepted");

//$.validator.addMethod("DecimalExceptZero", function (value, element) {
//    return this.optional(element) || /(^([1-9][1-9]*)?([0]?)(\.[0-9][0-9]?)?$)|(^\+?(\d*[1-9]\d*\.?|\d*\.\d*[1-9]\d*)$)/i.test(value);
//}, "Only Decimal more Then Zero is acceted.");

$.validator.addMethod("Character", function (value, element) {
    return this.optional(element) || /^.*$/i.test(value);
}, "Length Range Not Expected.");

$.validator.addMethod("LengthRange1to256", function (value, element) {
    return this.optional(element) || /^.{1,256}$/i.test(value);
}, "Length Range Not Expected.");


$.validator.addMethod("LengthRange1to25", function (value, element) {
    return this.optional(element) || /^.{1,25}$/i.test(value);
}, "Length Range Not Expected.");


$.validator.addMethod("LengthRange1to10", function (value, element) {
    return this.optional(element) || /^.{1,10}$/i.test(value);
}, "Length Range Not Expected.");
$.validator.addMethod("Length10", function (value, element) {
    return this.optional(element) || /^.{10}$/i.test(value);
}, "Length Should Be 10.");


$.validator.addMethod("LengthRange1to55", function (value, element) {
    return this.optional(element) || /^.{1,55}$/i.test(value);
}, "Length Range Not Expected.");

$.validator.addMethod("LengthRange2to30", function (value, element) {
    return this.optional(element) || /^.{2,30}$/i.test(value);
}, "Length Range Not Expected.");

$.validator.addMethod("LengthRange1to60", function (value, element) {
    return this.optional(element) || /^.{1,60}$/i.test(value);
}, "Length Range Not Expected.");

$.validator.addMethod("LengthRange1to50", function (value, element) {
    return this.optional(element) || /^.{1,50}$/i.test(value);
}, "Length Range Not Expected.");

$.validator.addMethod("LengthRange2to80", function (value, element) {
    return this.optional(element) || /^.{2,80}$/i.test(value);
}, "Length Range Not Expected.");

$.validator.addMethod("LengthRange3to15", function (value, element) {
    return this.optional(element) || /^.{3,15}$/i.test(value);
}, "Length Range Not Expected.");

$.validator.addMethod("LengthRange1to500", function (value, element) {
    return this.optional(element) || /^.{1,500}$/i.test(value);
}, "Length Range Not Expected.");

$.validator.addMethod("LengthRange1to30", function (value, element) {
    return this.optional(element) || /^.{1,30}$/i.test(value);
}, "Length Range Not Expected.");

$.validator.addMethod("LengthRange2to30", function (value, element) {
    return this.optional(element) || /^.{2,30}$/i.test(value);
}, "Length Range Not Expected.");

$.validator.addMethod("LengthRange1to35", function (value, element) {
    return this.optional(element) || /^.{1,35}$/i.test(value);
}, "Length Range Not Expected.");

$.validator.addMethod("LengthRange1to38", function (value, element) {
    return this.optional(element) || /^.{1,38}$/i.test(value);
}, "Length Range Not Expected.");

$.validator.addMethod("LengthRange5to15", function (value, element) {
    return this.optional(element) || /^.{5,15}$/i.test(value);
}, "Length Range Not Expected.");

$.validator.addMethod("NofirstLastDash", function (value, element) {
    return this.optional(element) || /^[a-zA-Z0-9][0-9a-zA-Z-]*[a-zA-Z0-9]$/i.test(value);
}, "no dashes at first and last.");


$.validator.addMethod("LengthRange5to9", function (value, element) {
    return this.optional(element) || /^.{5,9}$/i.test(value);
}, "Length Range Not Expected.");

$.validator.addMethod("LengthRangeonly2", function (value, element) {
    return this.optional(element) || /^.{2,2}$/i.test(value);
}, "maximum and minimum length is only 2.");

$.validator.addMethod("SingleDash", function (value, element) {
    return this.optional(element) || /^[0-9]+[-]{0,1}[0-9]+$/i.test(value);
}, "not valid data");

$.validator.addMethod("OnlyLettersNumbersSpecialCharacters1", function (value, element) {
    return this.optional(element) || /^[0-9a-zA-Z]+[0-9a-zA-Z, -]*$/i.test(value);
}, "Length Range Not Expected.");

$.validator.addMethod("OnlyLettersNumbersSpecialCharacters2", function (value, element) {
    return this.optional(element) || /^[0-9a-zA-Z]+[0-9a-zA-Z, -\*]*$/i.test(value);
}, "Length Range Not Expected.");

$.validator.addMethod("OnlyLettersNumbersSpecialCharacters3", function (value, element) {
    return this.optional(element) || /^[0-9a-zA-Z]+[0-9a-zA-Z, -\*&]*$/i.test(value);
}, "Length Range Not Expected.");

$.validator.addMethod("OnlyLettersNumbersSpecialCharacters4", function (value, element) {
    return this.optional(element) || /^[0-9a-zA-Z]+[0-9a-zA-Z -\/']*$/i.test(value);
}, "Length Range Not Expected.");

$.validator.addMethod("OnlyLettersNumbersSpecialCharacters5", function (value, element) {
    return this.optional(element) || /^[a-zA-Z]+[a-zA-Z -\.']*$/i.test(value);
}, "Length Range Not Expected.");


$.validator.addMethod("Restict10", function (value, element) {
    return this.optional(element) || /^[a-z]{0,10}$/.test(value);
}, "Please enter less than 10 letters");


$.validator.addMethod("OnlyNumbers", function (value, element) {
    return this.optional(element) || /^[0-9]*$/.test(value);
}, "Acceptable Characters in Field Numbers only.");

$.validator.addMethod("OnlyNumbersandLetters", function (value, element) {
    return this.optional(element) || /^[A-Z0-9 _]*$/.test(value);
}, "Acceptable Characters in Field Numbers and Characters  only.");

jQuery.validator.addMethod("required", function (value, element) {
    if (element.parentNode.children[1] != undefined) {
        $('.tooltip').remove();
    }
    return !(element.value.trim() === '');
}, 'Please enter this field');

jQuery.validator.addMethod("requiredGender", function (value, element) {
    //alert(element.value);
    return (value == 'M' || value == 'F' || value == 'U');
}, 'Please select a Gender');

jQuery.validator.addMethod("requiredPlan", function (value, element) {
    return true;
    //return (value == 'MB' || value == 'MC' || value == 'Tricare' || value == 'CHAMPVA' || value == 'Group Health Plan' || value == 'FECA BLK LUNG' || value == 'CI');
}, 'Please select an Insurance Plan');

jQuery.validator.addMethod("OtherHealthInsurancePlanChoice", function (value, element) {

    return (value == 'true' || value == 'false');
}, 'Please select an option');

jQuery.validator.addMethod("Dropdownrequired", function (value, element) {
    return !(value === '' || value === '? object:null ?');
}, 'Please enter this field');



$.validator.addMethod("ZipCode", function (value, element) {
    return this.optional(element) || /^(\d{5}|\d{9})$/.test(value);
}, "Invalid Zip Code.Please Enter 5 or 9 Digit Number");

$.validator.addMethod("ZipCodeOfFiveDigits", function (value, element) {
    return this.optional(element) || /^[0-9]{9}$/.test(value);
}, "Invalid Zip Code.Please Enter 9 Digit Number");

$.validator.addMethod("OnlyLettersNumbersSpecialCharacters6", function (value, element) {
    return this.optional(element) || /^[ A-Za-z0-9\.\/\\'",-]*$/.test(value);
}, "Acceptable Characters in Field should contain only letters, numbers, spaces, dashes, dots, slashes, apostrophes, commas.");

$.validator.addMethod("OnlyLettersNumbersSpecialCharacters6A", function (value, element) {
    return this.optional(element) || /^[ A-Za-z0-9\*\&\.\/\\'",-]*$/.test(value);
}, "Acceptable Characters in Field should contain only letters, numbers, spaces, dashes, dots, slashes, apostrophes, commas Ampersent and Astrik.");



$.validator.addMethod("OnlyLettersNumbersSpecialCharacters8", function (value, element) {
    return this.optional(element) || /^[ A-Za-z0-9\\/'"-\.]*$/.test(value);
}, "Allowed Character Types in this Field should be letters, numbers, spaces, dashes, slashes, apostrophes.");


$.validator.addMethod("OnlyLettersNumbersSpecialCharacters9", function (value, element) {
    return this.optional(element) || /^[ A-Za-z0-9\\/'",-]*$/.test(value);
}, "Allowed Character Types in this Field should be letters, numbers, spaces, dashes, slashes, commas, apostrophes.");


$.validator.addMethod("OnlyLettersNumbersSpecialCharacters10", function (value, element) {
    return this.optional(element) || /^[ A-Za-z0-9\.\\/'"-_]*$/.test(value);
}, "Allowed Character Types in this Field should be letters, numbers, spaces, dashes, dots, slashes, apostrophes.");


$.validator.addMethod("OnlyNumbersWithOptionaldash", function (value, element) {
    return this.optional(element) || /^[0-9]*-{0,1}[0-9]*$/.test(value);
}, "Allowed Character Types in this Field should be number,one optional dash.");


$.validator.addMethod("ALPHANUMERIC_DASHES", function (value, element) {
    return this.optional(element) || /^[a-z0-9-]+$/i.test(value);
}, "DATA is invalid: Please enter a valid AlphaNumeric Data.");

$.validator.addMethod("NPI", function (value, element) {
    return this.optional(element) || /^\d{10}$/.test(value);
}, "NPI is invalid: Please enter a 10 Digit Numbers.");

$.validator.addMethod("TENDIGIT_DECIMAL", function (value, element) {
    return this.optional(element) || /^[0-9]+(\.[0-9]{1,2})?$/.test(value);
}, "DATA is invalid: Please enter a Valid Decimal Numbers.");

$.validator.addMethod("ALPHANUMERIC_50", function (value, element) {
    return this.optional(element) || /^([a-z0-9-]){1,50}$/i.test(value);
}, "DATA is invalid: Please enter a Valid Decimal Numbers.");

$.validator.addMethod("ALPHANUMERIC_10", function (value, element) {
    return this.optional(element) || /^([a-z0-9-]){1,10}$/i.test(value);
}, "DATA is invalid: Please enter a Valid Decimal Numbers.");
$.validator.addMethod('validAge', function (value, element, params) {
    //alert(value.length, params[0])
    return (value.length > params[0] && value.length < params[1]);
}, "enter the valid range");

$.validator.addMethod("LettersNumbersDecAt4thOr5th", function (value, element) {
    //  value = $.trim(value);
    // return this.optional(element) || /^[a-zA-Z0-9]{3,4}\.{0,1}[a-zA-Z0-9]*$/i.test(value);
    return true
}, " Please enter Letters and numbers only. Decimal should be at 4th or 5th position");

$.validator.addMethod("NotRequired_1to60AllowedChars", function (value, element) {
    return this.optional(element) || /^.{1,60}$/i.test(value);
}, "Email Address is invalid: Please enter characters less than 60.");
$.validator.addMethod("Prassana", function (value, element) {
    return this.optional(element) || /^[0-9a-zA-Z]+[a-zA-Z -\.#\\\/,']*$/i.test(value);
}, "Length Range Not Expected.");
$.validator.addMethod("CharactersBetween1To38", function (value, element) {
    return this.optional(element) || /^.{1,38}$/i.test(value);
}, "Input Length Should Be Between 1-38.");
$.validator.addMethod("CharactersBetween5To15", function (value, element) {
    return this.optional(element) || /^.{5,15}$/i.test(value);
}, "Input Length Should Be Between 5-35.");
$.validator.addMethod("CharactersBetween1To50", function (value, element) {
    return this.optional(element) || /^.{1,50}$/i.test(value);
}, "Input Length Should Be Between 5-9.");

$.validator.addMethod("CharactersBetween5To9", function (value, element) {
    return this.optional(element) || /^.{5,9}$/i.test(value);
}, "Input Length Should Be Between 5-9.");
$.validator.addMethod("Decimalpointonlyone", function (value, element) {
    return this.optional(element) || /^[0-9]*\.{0,1}[0-9]$/i.test(value);
}, "The Data should have 1 digit after decemial  ");
$.validator.addMethod("CharactersBetween1To48", function (value, element) {
    return this.optional(element) || /^.{1,48}$/i.test(value);
}, "Input Length Should Be Between 1-48.");

$.validator.addMethod("AllowingHyphenInBetweenTheNumbers", function (value, element) {
    return this.optional(element) || /^[0-9]+[-]+[0-9]+$/i.test(value);
}, "Input should Contain Hyphen");

$.validator.addMethod("ValidateDateFormatClaimInfo", function (value, element) {
    return this.optional(element) || /^(0[1-9]|1[0-2])\/(0[1-9]|1\d|2\d|3[01])\/(19|20)\d{2}$/i.test(value);
}, "Input Date inCorrect Format MM-DD-YYYY");

//$.validator.addMethod("ValidateDateFormat", function (value, element) {
//    return this.optional(element) || / ^((0?[13578]|10|12)(-|\/)(([1-9])|(0[1-9])|([12])([0-9]?)|(3[01]?))(-|\/)((19)([2-9])(\d{1})|(20)([01])(\d{1})|([8901])(\d{1}))|(0?[2469]|11)(-|\/)(([1-9])|(0[1-9])|([12])([0-9]?)|(3[0]?))(-|\/)((19)([2-9])(\d{1})|(20)([01])(\d{1})|([8901])(\d{1})))$/i.test(value);
//}, "Input Date in Correct Format MM-DD-YYYY");


$.validator.addMethod("ValidateDateFormatHCFAForm", function (value, element) {
    return this.optional(element) || /^(0[1-9]|1[0-2])\/(0[1-9]|1\d|2\d|3[01])\/(19|20)\d{2}$/i.test(value);
}, "Input Date inCorrect Format MM-DD-YYYY");

$.validator.addMethod("DiagonosisPointer", function (value, element) {
    // console.log(element.parentNode.children[1]);
    if (element.parentNode.children[1] != undefined) {
        //  element.parentNode.children[1].addClass('Destroythis');
        $('.tooltip').remove();
    }

    return this.optional(element) || /^([1-9]|1[012])$/i.test(value);
}, "Diagnosis Pointer Must Be From 1 To 12 Only");



$.validator.addMethod("greaterThan",
function (value, element, params) {

    if (!/Invalid|NaN/.test(new Date(value))) {
         
        return new Date(value) >= new Date($("[name='" + params + "']").val());
    }

    return isNaN(value) && isNaN($("[name='" + params + "']").val())
        || (Number(value) >= Number($("[name='" + params + "']").val()));
}, 'Must be greater than From Date');

$.validator.addMethod("lessThanDate",
function (value, element, params) {

    if ($("[name='" + params + "']").val() != null) {
        if (!/Invalid|NaN/.test(new Date(value))) {
            return new Date(value) <= new Date($("[name='" + params + "']").val());
        }
    }
    return isNaN(value) && isNaN($("[name='" + params + "']").val())
        || (Number(value) <= Number($("[name='" + params + "']").val()));
}, 'Must be less than From Date');

$.validator.addMethod("checkForRepeatedValue1233",
function (value, element, params) {
    return false;
}, 'Must be less than or equal to current date');

$.validator.addMethod("notFutureDate",
function (value, element, params) {
    if (!/Invalid|NaN/.test(new Date(value))) {
        return new Date(value) <= new Date();
    }

    return isNaN(value) && isNaN($("[name='" + params + "']").val())
        || (Number(value) <= new Date());
}, 'Must be less than or equal to current date');


