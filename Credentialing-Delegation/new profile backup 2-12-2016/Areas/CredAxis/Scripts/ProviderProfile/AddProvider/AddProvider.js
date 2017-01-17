$(document).ready(function () {
    $('#message').hide();
    $('body').off('change', '#providerNpi').on('change', '#providerNpi', function () {
        if ($('#providerNpi').val() == "123456789") {
            $('#message').show();
        } else {
            $('#message').hide();
        }
    });


    //$('.input-tags3').selectize({
    //    plugins: ['remove_button'],
    //    delimiter: ',',
    //    persist: false,
    //    create: function (input) {
    //        return {
    //            value: input,
    //            text: input
    //        }
    //    }
    //});

    //$('.input-tags3').selectize({
    //    plugins: ['remove_button'],
    //    delimiter: ',',
    //    persist: false,
    //    create: function (input) {
    //        return {
    //            value: input,
    //            text: input
    //        }
    //    }
    //});

    $(".planMutiple").select2({
        tags: true,
        placeholder: 'Specialty',
        allowClear: true
    })

    
});

$("#AddAnotherLocation").on("click", function () {

    $("#AddNewLocationSection").show(1000);

    $(".AddLocation").hide();
    

});

$("#CloseAddLocation").on("click", function () {

    $("#AddNewLocationSection").hide(1000);
    $(".AddLocation").show();

});