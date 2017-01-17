/** @description upload/reset the file/form.
 */
$("#ClearForm").on('click', function (e) {
    e.preventDefault();
})
/** @description upload the file.
 */
//$("#Upload_ediFile_btn").on('click', function (e) {
   
//})

var onSuccessFn = function (data) {
   
}
$(document).ready(function(){
$("#file_upload_form").validate({
    onsubmit: false,
    submitHandler: function (form) {
        if ($(form).valid()) {
            form.submit();
        }
        return false; // prevent normal form posting
    }
});
});

    $(document).off("submit", "form").on("submit", "form", function (event) {
    var form = $(this);
    event.preventDefault();
    if (validateForm(form)) {
        var url = "/Billing/FileManagement/UploadFile";
        var formData = new FormData(this);
        
        $.ajax({
            url: url,
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (data) {
                onSuccessFn(data);
                $('#SharedModal').modal('toggle');
            },
            error: function (data) {
                console.log(data);
            }
        });
    }
});
var validateForm = function (form) {
   
    form.removeData("validator").removeData("unobtrusiveValidation");
    $.validator.unobtrusive.parse(form);
    if (form.valid()) {
        return true;
    }
    return false;

};