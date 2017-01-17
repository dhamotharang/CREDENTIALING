//=============== Angular regular expression Validation ===================
//====== for create provider regular expretion ====================
var a_emailPattern = /^[a-z]+[a-z0-9._]+@[a-z]+\.[a-z.]{2,5}$/;
var a_userNamePattern = /^[a-zA-Z]([a-zA-Z ,.-]{1,24})$/;
var a_userMiddleNamePattern = /^[a-zA-Z]([a-zA-Z ,.-]{0,24})$/;
var a_streetPattern = /^.{5,100}$/;
var a_zipCodePattern = /^[0-9]{5}$/;
var a_mobileNumberPattern = /^[1-9]([0-9]{9})$/;

//====== for create provider regular expretion strict input validation ====================
var emailPattern = /^[a-z]+[a-z0-9._]+@[a-z]+\.[a-z.]{2,5}$/;
var emailMinLenght = 8;
var emailMaxLenght = 35;
var userNamePattern = /^[a-zA-Z][a-zA-Z ,.-]*$/;
var userFirstNameMinLenght = userLastNameMinLenght = 2;
var userMiddleNameMinLenght = 2;
var userFirstNameMaxLenght = userMiddleNameMaxLenght = userLastNameMaxLenght = 25;
var streetMaxLenght = 100;
var zipCodePattern = /^[0-9]*$/;
var zipCodeMinLenght = zipCodeMaxLenght = 5;
var mobileNumberPattern = /^[1-9][0-9]*$/;
var moNoMinLength = moNoMaxLength = 10;

//======================= Input Validation ===================================
function inputValidation(obj, event, maxlength, pattern) {
    var key = event.which;
    if (key >= 33 || key == 13 || key == 32) {

        var length = obj.value.length;
        if (pattern.test(obj.value) == false) {
            obj.value = obj.value.substring(0, obj.value.length - 1);
        }

        if (length >= maxlength) {
            event.preventDefault();
        }
    }
}

//=============================== fixed restricted validation ===========================================
function limitLength(obj, event, maxlength) {
    var key = event.which;
    //all keys including return.
    if (key >= 33 || key == 13 || key == 32) {

        var length = obj.value.length;
        if (length >= maxlength) {
            event.preventDefault();
        }
    }
}
//======================= fixed input length ==============================
function limitText(obj, event, maxlength) {
    var key = event.which;
    //all keys including return.
    if (key >= 33 || key == 13 || key == 32) {

        var length = obj.value.length;
        if (length >= maxlength) {
            event.preventDefault();
            obj.value = obj.value.substring(0, maxlength);
        }
        //$("[name='limitCount']").val(maxlength - length);
    }
}